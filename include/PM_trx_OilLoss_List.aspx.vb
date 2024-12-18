
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


Public Class PM_OilLossList : Inherits Page

    'Protected WithEvents dgOilLossList As DataGrid
    'Protected WithEvents lblTracker As Label
    'Protected WithEvents lblErrMessage As Label

    'Protected WithEvents srchStatusList As DropDownList
    'Protected WithEvents srchProcessingLine As DropDownList
    'Protected WithEvents srchMachine As DropDownList

    'Protected WithEvents srchUpdateBy As TextBox
    'Protected WithEvents SortExpression As Label
    'Protected WithEvents SortCol As Label
    'Protected WithEvents lstDropList As DropDownList
    'Protected WithEvents lblFmt As Label
    'Protected WithEvents lblDate As Label
    'Protected WithEvents lblDupMsg As Label
    'Protected WithEvents txtDate As TextBox

    Dim objOk As New agri.GL.ClsTrx()

    Protected objPMTrx As New agri.PM.clsTrx()
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
    Dim objOilLoss As New DataSet()
    Dim objProcessingLine As New DataSet()
    Dim objMachine As New DataSet()
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
                BindSearchList()
                BindGrid()
                BindPageList()
            End If
        End If
    End Sub

    Sub BindSearchList()

        srchStatusList.Items.Add(New ListItem(objPMTrx.mtdGetOilLossStatus(objPMTrx.EnumOilLossStatus.Active), objPMTrx.EnumOilLossStatus.Active))
        srchStatusList.Items.Add(New ListItem(objPMTrx.mtdGetOilLossStatus(objPMTrx.EnumOilLossStatus.Deleted), objPMTrx.EnumOilLossStatus.Deleted))
        srchStatusList.Items.Add(New ListItem(objPMTrx.mtdGetOilLossStatus(objPMTrx.EnumOilLossStatus.All), objPMTrx.EnumOilLossStatus.All))

    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgOilLossList.CurrentPageIndex = 0
        dgOilLossList.EditItemIndex = -1
        BindGrid()
        BindPageList()
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
            intErrNo = objOk.mtdGetDataCommon(strOpCd, _
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

    Sub BindGrid()
        Dim lbButton As LinkButton
        Dim intCnt As Integer
        Dim intStatus As Integer

        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadData()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgOilLossList.PageSize)

        dgOilLossList.DataSource = dsData
        If dgOilLossList.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgOilLossList.CurrentPageIndex = 0
            Else
                dgOilLossList.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgOilLossList.DataBind()
        BindPageList()
        PageNo = dgOilLossList.CurrentPageIndex + 1
        lblTracker.Text = "Page " & PageNo & " of " & dgOilLossList.PageCount
         
    End Sub

    Sub BindPageList()
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgOilLossList.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While

        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgOilLossList.CurrentPageIndex

    End Sub

    Protected Function LoadData() As DataSet
        Dim strOppCode_Get As String = "PM_CLSTRX_OIL_LOSSES_LIST_GET"
 
        Dim intErrNo As Integer
         

        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim objProd As New DataSet() 

        strParamName = "AM|AY|LOC|SEARCHSTR"
        strParamValue = lstAccMonth.SelectedItem.Value & "|" & lstAccYear.SelectedItem.Value & "|" & strLocation & "|" & ""

        Try
            intErrNo = objOk.mtdGetDataCommon(strOppCode_Get, strParamName, strParamValue, objOilLoss)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=wm_ticket&errmesg=" & Exp.Message & "&redirect=")
        End Try



        Return objOilLoss
    End Function

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgOilLossList.CurrentPageIndex = 0
            Case "prev"
                dgOilLossList.CurrentPageIndex = _
                Math.Max(0, dgOilLossList.CurrentPageIndex - 1)
            Case "next"
                dgOilLossList.CurrentPageIndex = _
                Math.Min(dgOilLossList.PageCount - 1, dgOilLossList.CurrentPageIndex + 1)
            Case "last"
                dgOilLossList.CurrentPageIndex = dgOilLossList.PageCount - 1
        End Select

        lstDropList.SelectedIndex = dgOilLossList.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgOilLossList.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgOilLossList.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        dgOilLossList.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub


    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOpCd_OilLoss_Upd As String = "PM_CLSTRX_OIL_QUALITY_UPD"
        Dim strOpCd_OilLoss_Add As String = ""
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim strSelectedLocCode As String
        Dim strSelectedTransDate As String
        Dim lblLocCode As Label
        Dim lblTransDate As Label

        dgOilLossList.EditItemIndex = CInt(E.Item.ItemIndex)
        lblLocCode = dgOilLossList.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblLocCode")
        strSelectedLocCode = lblLocCode.Text
        lblTransDate = dgOilLossList.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblTransDate")
        strSelectedTransDate = lblTransDate.Text

        strParam = strSelectedTransDate & _
                   "||||2"

        Try
            intErrNo = objPMTrx.mtdUpdOilLosses(strOpCd_OilLoss_Add, _
                                                strOpCd_OilLoss_Upd, _
                                                strLocation, _
                                                strUserId, _
                                                strAccMonth, _
                                                strAccYear, _
                                                strParam, _
                                                True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_OIL_LOSSES_DEL&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/pm_trx_OilQuality_list.aspx")
        End Try

        dgOilLossList.EditItemIndex = -1
        BindGrid()
    End Sub


    Sub NewBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PM_trx_OilLoss_Det.aspx")
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

        dgOilLossList.EditItemIndex = CInt(E.Item.ItemIndex)
        lblLocCode = dgOilLossList.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblLocCode")
        strSelectedLocCode = lblLocCode.Text
        lblTransDate = dgOilLossList.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblTransDate")
        strSelectedTransDate = lblTransDate.Text


        QString = "?LocCode=" & Server.UrlEncode(Trim(strSelectedLocCode)) & _
                  "&TransDate=" & Server.UrlEncode(Trim(objGlobal.GetLongDate(strSelectedTransDate))) & _
                  "&Edit=True"
        Response.Redirect("PM_trx_OilLoss_Det.aspx" & QString)
    End Sub

     

End Class
