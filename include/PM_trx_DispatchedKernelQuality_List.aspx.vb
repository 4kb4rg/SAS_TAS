
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

Public Class PM_DispatchedKernelQualityList : Inherits Page

    'Protected WithEvents dgDispatchedKernelQualityList As DataGrid
    'Protected WithEvents lblTracker As Label
    'Protected WithEvents lblErrMessage As Label

    'Protected WithEvents srchStatusList As DropDownList
    'Protected WithEvents srchProcessingLine As DropDownList
    'Protected WithEvents srchMachine As DropDownList
    'Protected WithEvents lblFmt As Label
    'Protected WithEvents lblDate As Label
    'Protected WithEvents lblDupMsg As Label
    'Protected WithEvents txtDate As TextBox

    'Protected WithEvents srchUpdateBy As TextBox
    'Protected WithEvents SortExpression As Label
    'Protected WithEvents SortCol As Label
    'Protected WithEvents lstDropList As DropDownList

    Protected objPMTrx As New agri.PM.clsTrx()
    Protected objPMSetup As New agri.PM.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Protected objGLTrx As New agri.GL.ClsTrx()

    Dim strCompany As String
    Dim strLocation As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intPMAR As Integer
    Dim objDispatchedKernelQuality As New DataSet()
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
        NewBtn.Visible = False

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMDispatchedKernel), intPMAR) = False Then
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

        srchStatusList.Items.Add(New ListItem(objPMTrx.mtdGetDispatchedKernelQualityStatus(objPMTrx.EnumDispatchedKernelQualityStatus.Active), objPMTrx.EnumDispatchedKernelQualityStatus.Active))
        srchStatusList.Items.Add(New ListItem(objPMTrx.mtdGetDispatchedKernelQualityStatus(objPMTrx.EnumDispatchedKernelQualityStatus.Deleted), objPMTrx.EnumDispatchedKernelQualityStatus.Deleted))
        srchStatusList.Items.Add(New ListItem(objPMTrx.mtdGetDispatchedKernelQualityStatus(objPMTrx.EnumDispatchedKernelQualityStatus.All), objPMTrx.EnumDispatchedKernelQualityStatus.All))

    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgDispatchedKernelQualityList.CurrentPageIndex = 0
        dgDispatchedKernelQualityList.EditItemIndex = -1
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
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgDispatchedKernelQualityList.PageSize)
        
        dgDispatchedKernelQualityList.DataSource = dsData
        If dgDispatchedKernelQualityList.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgDispatchedKernelQualityList.CurrentPageIndex = 0
            Else
                dgDispatchedKernelQualityList.CurrentPageIndex = PageCount - 1
            End If
        End If
        
        dgDispatchedKernelQualityList.DataBind()
        BindPageList()
        PageNo = dgDispatchedKernelQualityList.CurrentPageIndex + 1
        lblTracker.Text="Page " & pageno & " of " & dgDispatchedKernelQualityList.PageCount

    End Sub

    Sub BindPageList()
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgDispatchedKernelQualityList.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While

        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgDispatchedKernelQualityList.CurrentPageIndex

    End Sub

    Protected Function LoadData() As DataSet
        Dim strOppCode_Get As String = "PM_CLSTRX_DISPATCHED_KERNEL_QUALITY_LIST_GET"
     
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer


        strParamName = "ACCMONTH|ACCYEAR|STRSEARCH"
        strParamValue = lstAccMonth.SelectedItem.Value & "|" & lstAccYear.SelectedItem.Value & "|" & strLocation

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOppCode_Get, strParamName, strParamValue, objDispatchedKernelQuality)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PM_GET_DATA&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/PM_trx_OilQuality_List_KLR.aspx")
        End Try

        Return objDispatchedKernelQuality
    End Function

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgDispatchedKernelQualityList.CurrentPageIndex = 0
            Case "prev"
                dgDispatchedKernelQualityList.CurrentPageIndex = _
                Math.Max(0, dgDispatchedKernelQualityList.CurrentPageIndex - 1)
            Case "next"
                dgDispatchedKernelQualityList.CurrentPageIndex = _
                Math.Min(dgDispatchedKernelQualityList.PageCount - 1, dgDispatchedKernelQualityList.CurrentPageIndex + 1)
            Case "last"
                dgDispatchedKernelQualityList.CurrentPageIndex = dgDispatchedKernelQualityList.PageCount - 1
        End Select

        lstDropList.SelectedIndex = dgDispatchedKernelQualityList.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgDispatchedKernelQualityList.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgDispatchedKernelQualityList.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        dgDispatchedKernelQualityList.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub


    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOpCd_DispatchedKernelQuality_Upd As String = "PM_CLSTRX_DISPATCHED_KERNEL_QUALITY_UPD"
        Dim strOpCd_DispatchedKernelQuality_Add As String = ""
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim strSelectedLocCode As String
        Dim strSelectedTransDate As String
        Dim lblLocCode As Label
        Dim lblTransDate As Label


        dgDispatchedKernelQualityList.EditItemIndex = CInt(E.Item.ItemIndex)
        lblLocCode = dgDispatchedKernelQualityList.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblLocCode")
        strSelectedLocCode = lblLocCode.Text
        lblTransDate = dgDispatchedKernelQualityList.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblTransDate")
        strSelectedTransDate = lblTransDate.Text

        strParam = strSelectedTransDate & "|||||" & _
                   "||||2"

        Try
            intErrNo = objPMTrx.mtdUpdDispatchedKernelQuality(strOpCd_DispatchedKernelQuality_Add, _
                                                strOpCd_DispatchedKernelQuality_Upd, _
                                                strLocation, _
                                                strUserId, _
                                                strAccMonth, _
                                                strAccYear, _
                                                strParam, _
                                                True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_OIL_LOSS_DEL&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/pm_trx_DispatchedKernelQuality_list.aspx")
        End Try

        dgDispatchedKernelQualityList.EditItemIndex = -1
        BindGrid()
    End Sub


    Sub NewBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PM_trx_DispatchedKernelQuality_Det.aspx")
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

        dgDispatchedKernelQualityList.EditItemIndex = CInt(E.Item.ItemIndex)
        lblLocCode = dgDispatchedKernelQualityList.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblLocCode")
        strSelectedLocCode = lblLocCode.Text
        lblTransDate = dgDispatchedKernelQualityList.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblTransDate")
        strSelectedTransDate = lblTransDate.Text


        QString = "?LocCode=" & Server.UrlEncode(Trim(strSelectedLocCode)) & _
                  "&TransDate=" & Server.UrlEncode(Trim(objGlobal.GetLongDate(strSelectedTransDate))) & _
                  "&Edit=True"
        Response.Redirect("PM_trx_DispatchedKernelQuality_Det.aspx" & QString)
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
        lblFmt.Visible = false
        lblDate.Visible = False
        lblFmt.Visible = False
        'If Not txtdate.Text = "" Then
        '    If objGlobal.mtdValidInputDate(strDateFormat, txtdate.Text, objDateFormat, strValidDate) = True Then
        '        Return strValidDate
        '    Else
        '        lblFmt.Visible = true
        '        lblFmt.Text = objDateFormat & "."
        '        lblDate.Visible = True
        '        lblFmt.Visible = True

        '    End If
        'End If
    End Function

End Class
