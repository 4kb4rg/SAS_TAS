
Imports System
Imports System.Data
Imports System.Collections 
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction

Public Class HR_setup_StaffList : Inherits Page

    Protected WithEvents dgList As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblErrMessage As Label

    Protected WithEvents srchName As TextBox
    Protected WithEvents ddlType As DropDownList
    Protected WithEvents srchStatusList As DropDownList
    Protected WithEvents srchUpdateBy As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lstDropList As DropDownList

    Protected objWM As New agri.WM.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGLtrx As New agri.GL.ClsTrx()


    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intWMAR As Integer
    Dim objTransDs As New DataSet()

    Dim strParamName As String
    Dim strParamValue As String


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intWMAR = Session("SS_WMAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
            'ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWMAccessRights.WMTransporterSetup), intWMAR) = False Then
            '    Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.Text = "" Then
                SortExpression.Text = "TransporterCode"
            End If

            If Not Page.IsPostBack Then
                BindSearchList()
                BindGrid()
            End If
        End If
    End Sub

    Sub BindSearchList()

        srchStatusList.Items.Add(New ListItem(objWM.mtdGetTransporterStatus(objWM.EnumTransporterStatus.All), objWM.EnumTransporterStatus.All))
        srchStatusList.Items.Add(New ListItem(objWM.mtdGetTransporterStatus(objWM.EnumTransporterStatus.Active), objWM.EnumTransporterStatus.Active))
        srchStatusList.Items.Add(New ListItem(objWM.mtdGetTransporterStatus(objWM.EnumTransporterStatus.Deleted), objWM.EnumTransporterStatus.Deleted))

        srchStatusList.SelectedIndex = 1
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgList.CurrentPageIndex = 0
        dgList.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub BindGrid()
        Dim lbButton As LinkButton
        Dim intCnt As Integer
        Dim Status As Label
        Dim strStatus As String
        Dim dsData As DataSet

        dsData = LoadData()
        dgList.DataSource = dsData
        dgList.DataBind()
        
        For intCnt = 0 To dgList.Items.Count - 1
            Status = dgList.Items.Item(intCnt).FindControl("lblStatus")
            strStatus = Status.Text

            Select Case strStatus
                Case objWM.mtdGetTransporterStatus(objWM.EnumTransporterStatus.Active)
                    lbButton = dgList.Items.Item(intCnt).FindControl("Delete")
                    lbButton.Visible = True
                    lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Case objWM.mtdGetTransporterStatus(objWM.EnumTransporterStatus.Deleted)
                    lbButton = dgList.Items.Item(intCnt).FindControl("Delete")
                    lbButton.Visible = False
            End Select
        Next

    End Sub

    Protected Function LoadData() As DataSet
        Dim strOpCode As String = "HR_CLSSETUP_STAFF_GET"
        Dim strSrchName As String
        Dim strSrchType As String
        Dim strSrchStatus As String
        Dim strSrchLastUpdate As String
        Dim strSearch As String = " "
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objData As New Object

        If srchName.Text <> "" Then
            strSearch = strSearch & " AND Name LIKE '%" & Trim(srchName.Text) & "%' "
        End If
        If ddlType.SelectedItem.Value <> "0" Then
            strSearch = strSearch & " AND StaffType = '" & Trim(srchName.Text) & "' "
        End If
        If srchStatusList.SelectedItem.Value <> objWM.EnumTransporterStatus.All Then
            strSearch = strSearch & " AND A.Status = '" & Trim(srchStatusList.SelectedItem.Value) & "' "
        End If
        If srchUpdateBy.Text <> "" Then
            strSearch = strSearch & " AND UpdateID LIKE '%" & Trim(srchUpdateBy.Text) & "%' "
        End If

        If Not strSearch = "" Then
            If Right(strSearch, 4) = "AND " Then
                strSearch = Left(strSearch, Len(strSearch) - 4)
            End If
        End If

        strParamName = "STRSEARCH"
        strParamValue = strSearch

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                objData)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CASHBANKTLIST_GET_LIST&errmesg=" & lblErrMessage.Text & "&redirect=CB/trx/CB_trx_CashBanklist.aspx")
        End Try

        Return objData
    End Function

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        dgList.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOpCd_Transporter_Upd As String = "WM_CLSSETUP_TRANSPORTER_UPD"
        Dim strOpCd_Transporter_Add As String = ""
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim strSelectedTrans As String
        Dim lblTransCode As Label
        Dim arrParam As Array
        Dim strTType As String

        dgList.EditItemIndex = CInt(E.Item.ItemIndex)
        lblTransCode = dgList.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblTransCode")
        arrParam = Split(Trim(lblTransCode.Text), "|")
        strSelectedTrans = arrParam(0)
        strTType = arrParam(1)

        strParam = strSelectedTrans & "||||" & objWM.EnumTransporterStatus.Deleted & "|" & strTType
        Try
            intErrNo = objWM.mtdUpdTransporter(strOpCd_Transporter_Add, _
                                                strOpCd_Transporter_Upd, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WM_SETUP_TRANSPORTER_LIST_DEL&errmesg=" & lblErrMessage.Text & "&redirect=WM/Setup/WM_setup_TransporterList.aspx")
        End Try

        dgList.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub NewTransBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("HR_setup_StaffDet.aspx")
    End Sub

End Class
