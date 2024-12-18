Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information
Imports System.Xml
Imports System.Web.Services

Public Class WM_StdRpt_Daily_FFBRcv_Summ : Inherits Page

    Protected RptSelect As UserControl

    Dim objWM As New agri.WM.clsReport()
    Dim objWMTrx As New agri.WM.clsTrx()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objSysCfg As New agri.PWSystem.clsConfig()

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Protected WithEvents lblErrDateInMsg As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents lstAccType As DropDownList
    Protected WithEvents cbExcel As CheckBox
    Protected WithEvents dgTicketList As DataGrid

    Protected WithEvents srchDateIn As TextBox
    Protected WithEvents srchDateTo As TextBox
    Protected WithEvents btnSelDateFrom As Image
    Protected WithEvents btnSelDateTo As Image
    Protected WithEvents lblTo As Label
    Protected WithEvents txtSupplier As TextBox

    Dim objOk As New agri.GL.ClsTrx()
    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strLocType as String
    Dim intErrNo As Integer

    Dim tempAccMth As DropDownList
    Dim tempAccYr As DropDownList
    Dim strddlAccMth As String
    Dim strddlAccYr As String


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_PMACCMONTH")
        strAccYear = Session("SS_PMACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        strLocType = Session("SS_LOCTYPE")


        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
                srchDateIn.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Now)
                srchDateTo.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Now)

                onload_GetLangCap()
            End If
        End If

    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim ucTrMthYr As HtmlTableRow

        ucTrMthYr = RptSelect.FindControl("TrMthYr")
        ucTrMthYr.Visible = True

    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        lblLocation.Text = GetCaption(objLangCap.EnumLangCap.Location)
    End Sub

    Sub GetEntireLangCap()
        Dim strOpCode_BussTerm As String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
        Dim strParam As String
        Dim intErrNo As Integer

        strParam = strLangCode
        Try
            intErrNo = objLangCap.mtdGetBussTerm(strOpCode_BussTerm, _
                                                 strCompany, _
                                                 strLocation, _
                                                 strUserId, _
                                                 strAccMonth, _
                                                 strAccYear, _
                                                 objLangCapDs, _
                                                 strParam)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=WM_STDRPT_DAILYFFBRCVSUM_CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/WM_StdRpt_Selection.aspx")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

        For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If strLocType = objAdminLoc.EnumLocType.Mill then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                Else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                End If
                Exit For
            End If
        Next
    End Function

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
  
        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String
        Dim strType As String
        Dim strExportToExcel As String = ""
        Dim strSuppCode As String = ""

        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden
        Dim indDate As String = ""

        tempAccMth = RptSelect.FindControl("lstAccMonth")
        strddlAccMth = Trim(tempAccMth.SelectedItem.Value)
        tempAccYr = RptSelect.FindControl("lstAccYear")
        strddlAccYr = Trim(tempAccYr.SelectedItem.Value)
        tempRpt = RptSelect.FindControl("lstRptName")
        strRptID = Trim(tempRpt.SelectedItem.Value)
        strRptName = Trim(tempRpt.SelectedItem.Text)
        tempUserLoc = RptSelect.FindControl("hidUserLoc")
        strUserLoc = Trim(tempUserLoc.Value)
        tempDec = RptSelect.FindControl("lstDecimal")
        strDec = Trim(tempDec.SelectedItem.Value)
        strType = lstAccType.SelectedItem.Value

        strSuppCode = txtSupplier.Text.Trim
        If strUserLoc = "" Then
            templblUL = RptSelect.FindControl("lblUserLoc")
            templblUL.Visible = True
            Exit Sub
        Else
            If Left(strUserLoc, 3) = "','" Then
                strUserLoc = Right(strUserLoc, Len(strUserLoc) - 3)
            ElseIf Right(strUserLoc, 3) = "','" Then
                strUserLoc = Left(strUserLoc, Len(strUserLoc) - 3)
            End If
        End If

        If cbExcel.Checked = True Then
            strExportToExcel = "1"
        Else
            strExportToExcel = "0"
        End If

        If lstAccType.SelectedItem.Value = "1" Then

            If CheckDate(srchDateIn.Text.Trim(), indDate) = False Then
                lblErrDateInMsg.Visible = True
                Exit Sub
            End If

            If CheckDate(srchDateTo.Text.Trim(), indDate) = False Then
                lblErrDateInMsg.Visible = True
                Exit Sub
            End If


            BindGrid()

        Else

            Response.Write("<Script Language=""JavaScript"">window.open(""WM_StdRpt_Daily_FFBRcv_Summary_ReportPreview.aspx?Type=Print&CompName=" & strCompany & _
                            "&Location=" & strUserLoc & "&RptID=" & strRptID & _
                            "&RptName=" & strRptName & _
                            "&RptType=" & strType & _
                            "&SupCode=" & strSuppCode & _
                            "&ExportToExcel=" & strExportToExcel & _
                           "&strddlAccMth=" & strddlAccMth & "&strddlAccYr=" & strddlAccYr & "&Decimal=" & strDec & "&lblLocation=" & lblLocation.Text & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
        End If
    End Sub

    Sub BindGrid() 
        Dim PageCount As Integer
        Dim dsData As DataSet
        Dim intCnt As Integer

        dsData = LoadData()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgTicketList.PageSize)

        dgTicketList.DataSource = dsData
        If dgTicketList.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgTicketList.CurrentPageIndex = 0
            Else
                dgTicketList.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgTicketList.DataBind()


        For intCnt = 0 To dgTicketList.Items.Count - 1
            CType(dgTicketList.Items.Item(intCnt).FindControl("lblNoUrut"), Label).Text = intCnt + 1
        Next


        Dim cAccMonth As String = IIf(Len(tempAccMth.SelectedItem.Value) = 1, "0" & Trim(tempAccYr.SelectedItem.Value), Trim(tempAccMth.SelectedItem.Value))
        strAccYear = tempAccYr.SelectedItem.Value

        If cbExcel.Checked = True Then
            Response.Clear()
            Response.AddHeader("content-disposition", "attachment;filename=WM_TICKET_NOMINAL-" & Trim(strLocation) & "-" & Trim(cAccMonth) & Trim(strAccYear) & ".xls")
            Response.Charset = ""
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.ContentType = "application/vnd.xls"

            Dim stringWrite = New System.IO.StringWriter()
            Dim htmlWrite = New HtmlTextWriter(stringWrite)

            dgTicketList.RenderControl(htmlWrite)
            Response.Write(stringWrite.ToString())
            Response.End()
        End If
    End Sub

    Sub lstAccType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If lstAccType.SelectedItem.Value = "0" Or lstAccType.SelectedItem.Value = "2" Then
            srchDateIn.Visible = False
            srchDateTo.Visible = False
            btnSelDateFrom.Visible = False
            btnSelDateTo.Visible = False
            lblTo.Visible = False
            dgTicketList.DataSource = Nothing
            dgTicketList.DataBind()
        Else
            srchDateIn.Visible = True
            srchDateTo.Visible = True
            btnSelDateFrom.Visible = True
            btnSelDateTo.Visible = True
            lblTo.Visible = True

        End If
    End Sub

    Sub dgTicketList_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='DarkSeaGreen'")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")

            End If
        End If
    End Sub

    Private Sub dgTicketList_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgTicketList.ItemCreated
        If e.Item.ItemType = ListItemType.Header Then
            Dim dgItem As DataGridItem
            Dim dgCell As TableCell
            dgItem = New DataGridItem(0, 0, ListItemType.Header)

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "NO"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "SUPPLIER/CUSTOMER"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "TANGGAL"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 16
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "DATA TIMBANGAN"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 4
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "SUB TOTAL"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "ONGKOS BONGKAR"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 3
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "INSENTIF"
            dgCell.HorizontalAlign = HorizontalAlign.Center
 

            dgCell = New TableCell()
            dgCell.ColumnSpan = 6
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "BAYAR"
            dgCell.HorizontalAlign = HorizontalAlign.Center
            dgItem.Font.Bold = True
            dgTicketList.Controls(0).Controls.AddAt(0, dgItem)
        End If
    End Sub

    Private Sub dgTicketList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgTicketList.ItemDataBound
        If e.Item.ItemType = ListItemType.Header Then
            Dim dgItem As DataGridItem

            dgItem = New DataGridItem(0, 0, ListItemType.Header)
            e.Item.Cells(0).Visible = False
            e.Item.Cells(1).Visible = False
            e.Item.Cells(2).Visible = False
            e.Item.Cells(24).Visible = False
        End If
    End Sub

    Protected Function LoadData() As DataSet
        Dim strOppCode_Get As String = "WM_WM_STDRPT_TICKET_DETAIL_GET"
        Dim strSrchTicketNo As String
        Dim strSrchDeliveryNo As String
        Dim strSrchDateIn As String
        Dim strSrchCustomer As String
        Dim strSrchProduct As String
        Dim strSrchStatus As String
        Dim strSearch As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objFormatDate As String
        Dim objActualDate As String
        Dim strParamName As String
        Dim strParamValue As String
        Dim objTicketDs As New DataSet()

        Dim strDate As String = Date_Validation(srchDateIn.Text, False)
        Dim strDateTo As String = Date_Validation(srchDateTo.Text, False)

        tempAccMth = RptSelect.FindControl("lstAccMonth")
        strddlAccMth = Trim(tempAccMth.SelectedItem.Value)
        tempAccYr = RptSelect.FindControl("lstAccYear")
        strddlAccYr = Trim(tempAccYr.SelectedItem.Value)

        'If Not srchDateIn.Text = "" Then
        '    If objGlobal.mtdValidInputDate(strDateFormat, Trim(srchDateIn.Text), objFormatDate, objActualDate) = False Then
        '        lblErrDateIn.Visible = True
        '        lblErrDateIn.Text = lblErrDateInMsg.Text & objFormatDate
        '        Exit Function
        '    Else
        '        strSrchDateIn = objActualDate
        '    End If
        'End If

        strParamName = "LOCCODE|DFROM|DTO|SUPCODE"
        strParamValue = strLocation & "|" & strDate & "|" & strDateTo & "|" & txtSupplier.Text

        Try
            intErrNo = objOk.mtdGetDataCommon(strOppCode_Get, strParamName, strParamValue, objTicketDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=wm_ticket&errmesg=" & Exp.Message & "&redirect=")
        End Try

        dgTicketList.DataSource = objTicketDs
        dgTicketList.DataBind()

        Return objTicketDs

    End Function

    Function CheckDate(ByVal pv_strDate As String, ByRef pr_strDate As String) As Boolean
        Dim strDateFormatCode As String = Session("SS_DATEFMT")
        Dim strDateFormat As String

        pr_strDate = ""
        CheckDate = True
        If Not pv_strDate = "" Then
            If objGlobal.mtdValidInputDate(strDateFormatCode, pv_strDate, strDateFormat, pr_strDate) = False Then
                'lblFmt.Text = strDateFormat
                pr_strDate = ""
                CheckDate = False
            End If
        End If
    End Function

    Function Date_Validation(ByVal pv_strInputDate As String, ByVal pv_blnIsShortDate As Boolean) As String
        Dim objSysCfgDs As New Object
        Dim objActualDate As New Object
        Dim strDateFormat As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim strAcceptFormat As String

        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"

        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CB_DEPOSIT_GET_CONFIG&errmesg=" & Exp.Message & "&redirect=CB/trx/cb_trx_DepositList.aspx")
        End Try

        strDateFormat = objSysCfg.mtdGetDateFormat(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt").Trim())

        If pv_blnIsShortDate Then
            Date_Validation = objGlobal.GetShortDate(strDateFormat, pv_strInputDate)
        Else
            If objGlobal.mtdValidInputDate(strDateFormat, _
                                           pv_strInputDate, _
                                           strAcceptFormat, _
                                           objActualDate) = True Then
                Date_Validation = objActualDate
            Else
                Date_Validation = ""
            End If
        End If
    End Function

End Class

