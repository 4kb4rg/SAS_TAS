
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

Public Class PM_ProducedKernelQualityList : Inherits Page

    Protected WithEvents dgProducedKernelQualityList As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblErrMessage As Label

    Protected WithEvents srchStatusList As DropDownList
    Protected WithEvents srchProcessingLine As DropDownList
    Protected WithEvents srchMachine As DropDownList
    Protected WithEvents lblFmt As Label
    Protected WithEvents lblDate As Label
    Protected WithEvents lblDupMsg As Label
    Protected WithEvents txtDate As TextBox


    Protected WithEvents srchUpdateBy As TextBox    
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lstDropList As DropDownList

    Protected objPMTrx As New agri.PM.clsTrx()
    Protected objPMSetup As New agri.PM.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objOk As New agri.GL.ClsTrx()

    Dim strCompany As String
    Dim strLocation As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intPMAR As Integer
    Dim objProducedKernelQuality As New DataSet()
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

        'NewBtn.visible = False
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMProducedKernel), intPMAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.Text = "" Then
                SortExpression.Text = "TransDate"
            End If

            If Not Page.IsPostBack Then
                BindSearchList()
                BindGrid()
                BindPageList()
            End If
        End If
    End Sub

    Sub BindSearchList()

        srchStatusList.Items.Add(New ListItem(objPMTrx.mtdGetProducedKernelQualityStatus(objPMTrx.EnumProducedKernelQualityStatus.Active), objPMTrx.EnumProducedKernelQualityStatus.Active))
        srchStatusList.Items.Add(New ListItem(objPMTrx.mtdGetProducedKernelQualityStatus(objPMTrx.EnumProducedKernelQualityStatus.Deleted), objPMTrx.EnumProducedKernelQualityStatus.Deleted))
        srchStatusList.Items.Add(New ListItem(objPMTrx.mtdGetProducedKernelQualityStatus(objPMTrx.EnumProducedKernelQualityStatus.All), objPMTrx.EnumProducedKernelQualityStatus.All))

    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgProducedKernelQualityList.CurrentPageIndex = 0
        dgProducedKernelQualityList.EditItemIndex = -1
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
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgProducedKernelQualityList.PageSize)
        
        dgProducedKernelQualityList.DataSource = dsData
        If dgProducedKernelQualityList.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgProducedKernelQualityList.CurrentPageIndex = 0
            Else
                dgProducedKernelQualityList.CurrentPageIndex = PageCount - 1
            End If
        End If
        
        dgProducedKernelQualityList.DataBind()
        BindPageList()
        PageNo = dgProducedKernelQualityList.CurrentPageIndex + 1
        lblTracker.Text="Page " & pageno & " of " & dgProducedKernelQualityList.PageCount

        intStatus = CInt(srchStatusList.SelectedItem.Value)

        For intCnt = 0 To dgProducedKernelQualityList.Items.Count - 1
            Select Case intStatus
                Case objPMTrx.EnumDailyProdStatus.Active
                    lbButton = dgProducedKernelQualityList.Items.Item(intCnt).FindControl("Delete")
                    lbButton.Visible = True
                    lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Case objPMTrx.EnumDailyProdStatus.Deleted
                    lbButton = dgProducedKernelQualityList.Items.Item(intCnt).FindControl("Delete")
                    lbButton.Visible = False
            End Select
        Next

    End Sub

    Sub BindPageList()
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgProducedKernelQualityList.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While

        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgProducedKernelQualityList.CurrentPageIndex

    End Sub

    Protected Function LoadData() As DataSet
        Dim strOppCode_Get As String = "PM_CLSTRX_PRODUCED_KERNEL_QUALITY_GET"
        Dim strSrchStatus As String
        Dim strSrchLastUpdate As String
        Dim strSrchProcessingLine As String
        Dim strSrchMachine As String
        Dim strParam As String
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strDate As String

        strDate = IIf(txtDate.Text = "", "", CheckDate())
        strSrchStatus = IIf(srchStatusList.SelectedItem.Value = objPMTrx.EnumProducedKernelQualityStatus.All, "", srchStatusList.SelectedItem.Value)
        strSrchLastUpdate = IIf(srchUpdateBy.Text = "", "", srchUpdateBy.Text)
        strOppCode_Get = "PM_CLSTRX_PRODUCED_KERNEL_QUALITY_LIST_GET"
         
        
        If strDate <> "" Then
            strParam = "AND DateProd = '" & strDate & "'"
        End If


        strParamName = "SEARCHSTR"
        strParamValue = "Where LocCode='" & strLocation & "'" & strParam

        Try
            intErrNo = objOk.mtdGetDataCommon(strOppCode_Get, _
                                                strParamName, _
                                                strParamValue, _
                                                objProducedKernelQuality)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & Exp.Message & "&redirect=")
        End Try
         
         
        Return objProducedKernelQuality
    End Function

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgProducedKernelQualityList.CurrentPageIndex = 0
            Case "prev"
                dgProducedKernelQualityList.CurrentPageIndex = _
                Math.Max(0, dgProducedKernelQualityList.CurrentPageIndex - 1)
            Case "next"
                dgProducedKernelQualityList.CurrentPageIndex = _
                Math.Min(dgProducedKernelQualityList.PageCount - 1, dgProducedKernelQualityList.CurrentPageIndex + 1)
            Case "last"
                dgProducedKernelQualityList.CurrentPageIndex = dgProducedKernelQualityList.PageCount - 1
        End Select

        lstDropList.SelectedIndex = dgProducedKernelQualityList.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgProducedKernelQualityList.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgProducedKernelQualityList.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        dgProducedKernelQualityList.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub


    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOpCd_ProducedKernelQuality_Upd As String = "PM_CLSTRX_PRODUCED_KERNEL_QUALITY_UPD"
        Dim strOpCd_ProducedKernelQuality_Add As String = ""
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim strSelectedLocCode As String
        Dim strSelectedTransDate As String
        Dim lblLocCode As Label
        Dim lblTransDate As Label
        

        dgProducedKernelQualityList.EditItemIndex = CInt(E.Item.ItemIndex)
        lblLocCode = dgProducedKernelQualityList.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblLocCode")
        strSelectedLocCode = lblLocCode.Text
        lblTransDate = dgProducedKernelQualityList.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblTransDate")
        strSelectedTransDate = lblTransDate.Text
        
        strParam = strSelectedTransDate & "|||||" & _
                   "||||2"

        Try
            intErrNo = objPMTrx.mtdUpdProducedKernelQuality(strOpCd_ProducedKernelQuality_Add, _
                                                strOpCd_ProducedKernelQuality_Upd, _
                                                strLocation, _
                                                strUserId, _
                                                strAccMonth, _
                                                strAccYear, _
                                                strParam, _
                                                True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_OIL_LOSS_DEL&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/pm_trx_ProducedKernelQuality_list.aspx")
        End Try

        dgProducedKernelQualityList.EditItemIndex = -1
        BindGrid()
    End Sub


    Sub NewBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PM_trx_ProducedKernelQuality_Det.aspx")
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

        dgProducedKernelQualityList.EditItemIndex = CInt(E.Item.ItemIndex)
        lblLocCode = dgProducedKernelQualityList.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblLocCode")
        strSelectedLocCode = lblLocCode.Text
        lblTransDate = dgProducedKernelQualityList.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblTransDate")
        strSelectedTransDate = lblTransDate.Text


        QString = "?LocCode=" & Server.UrlEncode(Trim(strSelectedLocCode)) & _
                  "&TransDate=" & Server.UrlEncode(Trim(objGlobal.GetLongDate(strSelectedTransDate))) & _
                  "&Edit=True"
        Response.Redirect("PM_trx_ProducedKernelQuality_Det.aspx" & QString)
    End Sub

    Protected Function CheckDate() As String

        Dim strDateSetting As String
        Dim objSysCfgDs As DataSet
        Dim objDateFormat As String
        Dim strValidDate As String
        lblFmt.Visible = False
        lblDate.Visible = False
        lblFmt.Visible = False
        If Not txtDate.Text = "" Then
            If objGlobal.mtdValidInputDate(strDateFormat, txtDate.Text, objDateFormat, strValidDate) = True Then
                Return strValidDate
            Else
                lblFmt.Visible = True
                lblFmt.Text = objDateFormat & "."
                lblDate.Visible = True
                lblFmt.Visible = True

            End If
        End If
    End Function

End Class
