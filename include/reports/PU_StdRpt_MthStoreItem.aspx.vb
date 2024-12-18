Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information
Imports System.Xml
Imports System.Web.Services

Public Class PU_StdRpt_MthStoreItem : Inherits Page

    Protected RptSelect As UserControl

    Dim objPU As New agri.PU.clsReport()
    Dim objPUTrx As New agri.PU.clsTrx()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objINSetup As New agri.IN.clsSetup()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Protected WithEvents lblDate As Label
    Protected WithEvents lblDateFormat As Label
    Protected WithEvents lblTracker As Label

    Protected WithEvents lblError As Label
    Protected WithEvents lblAccCode As Label
    Protected WithEvents lblLocation As Label

    Protected WithEvents txtSupplier As TextBox
    Protected WithEvents txtRcvNoteIDFrom As TextBox
    Protected WithEvents txtRcvNoteIDTo As TextBox
    Protected WithEvents txtRcvNoteDateFrom As TextBox
    Protected WithEvents txtRcvNoteDateTo As TextBox
    Protected WithEvents btnSelFromDate As Image
    Protected WithEvents btnSelToDate As Image
    Protected WithEvents txtFromItemCode As TextBox
    Protected WithEvents txtToItemCode As TextBox
    Protected WithEvents lstStatus As DropDownList
    Protected WithEvents lstItemType As DropDownList

    Protected WithEvents TRDocDateFromTo As HtmlTableRow
    Protected WithEvents TrBlkGrp As HtmlTableRow
    Protected WithEvents TrBlk As HtmlTableRow
    Protected WithEvents TrSubBlk As HtmlTableRow
    Protected WithEvents PrintPrev As ImageButton

    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfigsetting As Integer

    Dim dr As DataRow
    Dim intCnt As Integer
    Dim intErrNo As Integer
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_PUACCMONTH")
        strAccYear = Session("SS_PUACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")

        lblDate.Visible = False
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
                onload_GetLangCap()  
                BindStatus() 
                BindItemType()            
            End If           
        End If
    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim UCTrDocDateFromTo As HtmlTableRow
        Dim ucTrMthYr As HtmlTableRow

        UCTrDocDateFromTo = RptSelect.FindControl("TRDocDateFromTo")
        UCTrDocDateFromTo.Visible = False

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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_STDRPT_GOODSRCV_CLSLANGCAP_BUSSTERM_GET&errmesg=" & Exp.ToString() & "&redirect=../en/reports/PU_StdRpt_Selection.aspx")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

        For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If strLocType = objAdminLoc.EnumLocType.Mill then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                end if
                Exit For
            End If
        Next
    End Function

    Sub BindStatus()

        lstStatus.Items.Add(New ListItem(objPUTrx.mtdGetGRStatus(objPUTrx.EnumGRStatus.All), objPUTrx.EnumGRStatus.All))
        lstStatus.Items.Add(New ListItem(objPUTrx.mtdGetGRStatus(objPUTrx.EnumGRStatus.Active), objPUTrx.EnumGRStatus.Active))
        lstStatus.Items.Add(New ListItem(objPUTrx.mtdGetGRStatus(objPUTrx.EnumGRStatus.Cancelled), objPUTrx.EnumGRStatus.Cancelled))
        lstStatus.Items.Add(New ListItem(objPUTrx.mtdGetGRStatus(objPUTrx.EnumGRStatus.Closed), objPUTrx.EnumGRStatus.Closed))
        lstStatus.Items.Add(New ListItem(objPUTrx.mtdGetGRStatus(objPUTrx.EnumGRStatus.Confirmed), objPUTrx.EnumGRStatus.Confirmed))
        lstStatus.Items.Add(New ListItem(objPUTrx.mtdGetGRStatus(objPUTrx.EnumGRStatus.Deleted), objPUTrx.EnumGRStatus.Deleted))

        lstStatus.SelectedIndex = 4

    End Sub

    Sub BindItemType()

        lstItemType.Items.Add(New ListItem(objINSetup.mtdGetInventoryItemType(objINSetup.EnumInventoryItemType.Stock), objINSetup.EnumInventoryItemType.Stock))
        lstItemType.Items.Add(New ListItem(objINSetup.mtdGetInventoryItemType(objINSetup.EnumInventoryItemType.DirectCharge), objINSetup.EnumInventoryItemType.DirectCharge))
        lstItemType.Items.Add(New ListItem(objINSetup.mtdGetInventoryItemType(objINSetup.EnumInventoryItemType.CanteenItem), objINSetup.EnumInventoryItemType.CanteenItem))
        lstItemType.Items.Add(New ListItem(objINSetup.mtdGetInventoryItemType(objINSetup.EnumInventoryItemType.WorkshopItem), objINSetup.EnumInventoryItemType.WorkshopItem))
        lstItemType.Items.Add(New ListItem(objINSetup.mtdGetInventoryItemType(objINSetup.EnumInventoryItemType.MiscItem), objINSetup.EnumInventoryItemType.MiscItem))
        lstItemType.Items.Add(New ListItem(objINSetup.mtdGetInventoryItemType(objINSetup.EnumInventoryItemType.FixedAssetItem), objINSetup.EnumInventoryItemType.FixedAssetItem))
        lstItemType.Items.Add(New ListItem("All",0))

        lstItemType.SelectedIndex = 0

    End Sub

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strSupplier As String
        Dim strRcvNoteIDFrom As String
        Dim strRcvNoteIDTo As String
        Dim strRcvNoteDateFrom As String
        Dim strRcvNoteDateTo As String
        Dim strFromItemCode As String
        Dim strToItemCode As String
        
        Dim strddlAccMth As String
        Dim strddlAccYr As String
        Dim strDocDateFrom As String
        Dim strDocDateTo As String
        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String
        Dim strStatus As String
        Dim strItemType As String

        Dim tempAccMth As DropDownList
        Dim tempAccYr As DropDownList
        Dim tempDocDateFrom As TextBox
        Dim tempDocDateTo As TextBox
        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden

        Dim strParam As String
        Dim strDateSetting As String
        Dim objSysCfgDs As New Object()
        Dim objDateFormat As New Object()
        Dim objDocDateFrom As String
        Dim objDocDateTo As String
        Dim objRcvNoteDateFrom As String
        Dim objRcvNoteDateTo As String

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

        If txtSupplier.Text = "" Then
            strSupplier = ""
        Else
            strSupplier = Trim(txtSupplier.Text)
        End If

        If txtRcvNoteIDFrom.Text = "" Then
            strRcvNoteIDFrom = ""
        Else
            strRcvNoteIDFrom = Trim(txtRcvNoteIDFrom.Text)
        End If

        If txtRcvNoteIDTo.Text = "" Then
            strRcvNoteIDTo = ""
        Else
            strRcvNoteIDTo = Trim(txtRcvNoteIDTo.Text)
        End If

        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"
        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_STDRPT_GOODSRCV_GET_CONFIG_DATE&errmesg=" & Exp.ToString() & "&redirect=../../en/reports/PU_StdRpt_Selection.aspx")
        End Try

        strDateSetting = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))

        If txtRcvNoteDateFrom.Text = "" AndAlso txtRcvNoteDateTo.Text <> "" Then
            lblError.Visible = True
            lblError.Text = "Please enter Received Note From Date"
            Exit Sub
        ElseIf txtRcvNoteDateFrom.Text <> "" AndAlso txtRcvNoteDateTo.Text = "" Then
            lblError.Visible = True
            lblError.Text = "Please enter Received Note To Date"
            Exit Sub      
        Else
            lblError.Visible = False  
        End If

        If txtRcvNoteDateFrom.Text = "" Then
            strRcvNoteDateFrom = ""
        Else
            If objGlobal.mtdValidInputDate(strDateSetting, txtRcvNoteDateFrom.Text, objDateFormat, objRcvNoteDateFrom) = True Then
                strRcvNoteDateFrom = Trim(objRcvNoteDateFrom)
            Else
                lblDateFormat.Text = objDateFormat & "."
                lblDate.Visible = True
                lblDateFormat.Visible = True
                Exit Sub
            End If            
        End If

        If txtRcvNoteDateTo.Text = "" Then
            strRcvNoteDateTo = ""
        Else
            If objGlobal.mtdValidInputDate(strDateSetting, txtRcvNoteDateTo.Text, objDateFormat, objRcvNoteDateTo) = True Then
                strRcvNoteDateTo = Trim(objRcvNoteDateTo)
            Else
                lblDateFormat.Text = objDateFormat & "."
                lblDate.Visible = True
                lblDateFormat.Visible = True
                Exit Sub
            End If                    
        End If

        If txtFromItemCode.Text = "" Then
            strFromItemCode = ""
        Else
            strFromItemCode = Trim(txtFromItemCode.Text)
        End If

        If txtToItemCode.Text = "" Then
            strToItemCode = ""
        Else
            strToItemCode = Trim(txtToItemCode.Text)
        End If



        strStatus = Trim(lstStatus.SelectedItem.Value)
        strSupplier = Server.UrlEncode(strSupplier)
        strRcvNoteIDFrom = Server.UrlEncode(strRcvNoteIDFrom)
        strRcvNoteIDTo = Server.UrlEncode(strRcvNoteIDTo)
        strRcvNoteDateFrom = Server.UrlEncode(strRcvNoteDateFrom)
        strRcvNoteDateTo = Server.UrlEncode(strRcvNoteDateTo)
        strFromItemCode = Server.UrlEncode(strFromItemCode)
        strToItemCode = Server.UrlEncode(strToItemCode)      
        strItemType = Trim(lstItemType.SelectedItem.Value)

        Response.Write("<Script Language=""JavaScript"">window.open(""PU_StdRpt_MthStoreItemPreview.aspx?CompName=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & strDec & _
                       "&DDLAccMth=" & strddlAccMth & "&DDLAccYr=" & strddlAccYr & "&lblLocation=" & lblLocation.Text & "&Supplier=" & strSupplier & "&RcvNoteIDFrom=" & strRcvNoteIDFrom & "&RcvNoteIDTo=" & strRcvNoteIDTo & "&RcvNoteDateFrom=" & strRcvNoteDateFrom & "&RcvNoteDateTo=" & strRcvNoteDateTo & _
                       "&FromItemCode=" & strFromItemCode & "&ToItemCode=" & strToItemCode & "&Status=" & strStatus & "&ItemType=" & strItemType & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

End Class
