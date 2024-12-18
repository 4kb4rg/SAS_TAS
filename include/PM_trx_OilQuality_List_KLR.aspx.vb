
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


Public Class PM_trx_OilQuality_List_KLR : Inherits Page

    'Protected WithEvents dgList As DataGrid
    'Protected WithEvents lblTracker As Label
    'Protected WithEvents lblErrMessage As Label


    'Protected WithEvents srchUpdateBy As TextBox
    'Protected WithEvents SortExpression As Label
    'Protected WithEvents SortCol As Label
    'Protected WithEvents lstDropList As DropDownList
    'Protected WithEvents lblFmt As Label
    'Protected WithEvents lblDate As Label
    'Protected WithEvents lblDupMsg As Label
    'Protected WithEvents txtDate As TextBox

    Protected objPMTrx As New agri.PM.clsTrx()
    Protected objGLTrx As New agri.GL.clsTrx()

    Protected objPMSetup As New agri.PM.clsSetup()

    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()

    Dim strCompany As String
    Dim strLocation As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intPMAR As Integer
    Dim objData As New DataSet()

    Dim strDateFormat As String


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strAccMonth = Session("SS_PMACCMONTH")
        strAccYear = Session("SS_PMACCYEAR")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intPMAR = Session("SS_PMAR")
        strDateFormat = Session("SS_DATEFMT")
        NewBtn.Visible = False
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMOilQuality), intPMAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.Text = "" Then
                SortExpression.Text = "TransDate"
            End If

            If Not Page.IsPostBack Then
                lstAccMonth.Text = Session("SS_SELACCMONTH")
                BindAccYear(Session("SS_SELACCYEAR"))

                BindGrid()
                BindPageList()
            End If
        End If
    End Sub


    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgList.CurrentPageIndex = 0
        dgList.EditItemIndex = -1
        BindGrid()
        BindPageList()
    End Sub

    Sub BindGrid()
        Dim lbButton As LinkButton
        Dim intCnt As Integer
        Dim intStatus As Integer

        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadData
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgList.PageSize)

        dgList.DataSource = dsData
        If dgList.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgList.CurrentPageIndex = 0
            Else
                dgList.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgList.DataBind()
        BindPageList()
        PageNo = dgList.CurrentPageIndex + 1
        lblTracker.Text = "Page " & pageno & " of " & dgList.PageCount
         
    End Sub

    Sub BindPageList()
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgList.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While

        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgList.CurrentPageIndex

    End Sub

    Protected Function LoadData() As DataSet

        Dim strOpCode As String = "PM_CLSTRX_OILQUALITY_KLR_SEARCH"

        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim strSearch As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strDate As String
        Dim strSrchLastUpdate As String

       
        strParamName = "STRSEARCH"
        strParamValue = "Where Month(t.Indate)='" & lstAccMonth.SelectedItem.Value & "' And year(t.InDate)='" & lstAccYear.SelectedItem.Value & "' AND t.LocCode='" & strLocation & "'"

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCode, strParamName, strParamValue, objData)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PM_GET_DATA&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/PM_trx_OilQuality_List_KLR.aspx")
        End Try


        For intCnt = 0 To objData.Tables(0).Rows.Count - 1
            objData.Tables(0).Rows(intCnt).Item("UpdateDate") = Trim(objData.Tables(0).Rows(intCnt).Item("UpdateDate"))
            objData.Tables(0).Rows(intCnt).Item("Status") = Trim(objData.Tables(0).Rows(intCnt).Item("Status"))
            objData.Tables(0).Rows(intCnt).Item("UpdateID") = Trim(objData.Tables(0).Rows(intCnt).Item("UpdateID"))
        Next

        Return objData
    End Function

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgList.CurrentPageIndex = 0
            Case "prev"
                dgList.CurrentPageIndex = _
                Math.Max(0, dgList.CurrentPageIndex - 1)
            Case "next"
                dgList.CurrentPageIndex = _
                Math.Min(dgList.PageCount - 1, dgList.CurrentPageIndex + 1)
            Case "last"
                dgList.CurrentPageIndex = dgList.PageCount - 1
        End Select

        lstDropList.SelectedIndex = dgList.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgList.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgList.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        dgList.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub


    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)

        Dim strOpCode As String = "PM_CLSTRX_OILQUALITY_KLR_DEL"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim intErrNo As Integer
        Dim strSelectedTransDate As String
        Dim lblLocCode As Label
        Dim lblTransDate As Label

        lblTransDate = dgList.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblTransDate")
        strSelectedTransDate = lblTransDate.Text

        strParamName = "TRANSDATE|LOCCODE"
        strParamValue = strSelectedTransDate & "|" & strLocation

        Try
            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCode, _
                                                   strParamName, _
                                                   strParamValue)


        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=DELETE&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/PM_trx_WaterQuality_List_KLR.aspx")
        End Try

        dgList.EditItemIndex = -1
        BindGrid()
    End Sub


    Sub NewBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PM_trx_OilQuality_Det_KLR.aspx")
    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strSelectedLocCode As String
        Dim strSelectedTransDate As String
        Dim strSelectedProcessingNo As String
        Dim strSelectedMachineCode As String
        Dim QString As String

        Dim lblLocCode As Label
        Dim lblTransDate As Label
        Dim lblProcessingLnNo As Label
        Dim lblMachineCode As Label

        dgList.EditItemIndex = CInt(E.Item.ItemIndex)
        lblLocCode = dgList.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblLocCode")
        strSelectedLocCode = lblLocCode.Text
        lblTransDate = dgList.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblTransDate")
        strSelectedTransDate = lblTransDate.Text


        QString = "?LocCode=" & Server.UrlEncode(Trim(strSelectedLocCode)) & _
                  "&TransDate=" & Server.UrlEncode(Trim(objGlobal.GetLongDate(strSelectedTransDate))) & _
                  "&Edit=True"
        Response.Redirect("PM_trx_OilQuality_Det_KLR.aspx" & QString)

    End Sub


    Sub BindAccYear(ByVal pv_strAccYear As String)
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim intSelectedIndex As Integer
        Dim objAccYearDs As New Object
        Dim strOpCd As String = "ADMIN_CLSACCPERIOD_CONFIG_GET"

        strParamName = "LOCCODE|SEARCHSTR|SORTEXP"
        strParamValue = strLocation & "||Order By HD.AccYear"

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objAccYearDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & Exp.Message & "&redirect=")
        End Try

        For intCnt = 0 To objAccYearDs.Tables(0).Rows.Count - 1
            objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear") = Trim(objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear"))
            objAccYearDs.Tables(0).Rows(intCnt).Item("UserName") = Trim(objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear"))
            If objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear") = pv_strAccYear Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        lstAccYear.DataSource = objAccYearDs.Tables(0)
        lstAccYear.DataValueField = "AccYear"
        lstAccYear.DataTextField = "UserName"
        lstAccYear.DataBind()
        lstAccYear.SelectedIndex = intSelectedIndex - 1
    End Sub

    Protected Function CheckDate() As String

        Dim strDateSetting As String
        Dim objSysCfgDs As DataSet
        Dim objDateFormat As String
        Dim strValidDate As String
        lblFmt.Visible = False
        lblDate.Visible = False
        lblFmt.Visible = False
       
    End Function

End Class
