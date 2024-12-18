Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports System.Xml
Imports System.Web.Services

Public Class WM_StdRpt_WeighBridge_Ticket_Listing : Inherits Page

    Protected RptSelect As UserControl

    Dim objWM As New agri.WM.clsReport()
    Dim objWMSetup As New agri.WM.clsSetup()
    Dim objWMTrx As New agri.WM.clsTrx()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Protected WithEvents lblDate As Label
    Protected WithEvents lblDateFormat As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblBillParty1 As Label
    Protected WithEvents lblBillParty2 As Label
    Protected WithEvents lblBlockTag As Label
    Protected WithEvents lblErrTimeInHrFrom As Label
    Protected WithEvents lblErrTimeInMinFrom As Label
    Protected WithEvents lblErrTimeInHrTo As Label
    Protected WithEvents lblErrTimeInMinTo As Label
    Protected WithEvents lblErrTimeOutHrFrom As Label
    Protected WithEvents lblErrTimeOutMinFrom As Label
    Protected WithEvents lblErrTimeOutHrTo As Label
    Protected WithEvents lblErrTimeOutMinTo As Label

    Protected WithEvents lblLocation As Label
    Protected WithEvents lblVehicle As Label

    Protected WithEvents lstStatus As DropDownList
    Protected WithEvents lstInAMPM As DropDownList
    Protected WithEvents lstOutAMPM As DropDownList
    Protected WithEvents lstInAMPMTo As DropDownList
    Protected WithEvents lstOutAMPMTo As DropDownList
    Protected WithEvents lstTransactionType As DropDownList
    Protected WithEvents lstProduct As DropDownList

    Protected WithEvents txtTicketNo As TextBox
    Protected WithEvents txtSuppBillParty As TextBox
    Protected WithEvents txtDocRefNo As TextBox
    Protected WithEvents txtDeliveryNoteNo As TextBox
    Protected WithEvents txtPL3No As TextBox
    Protected WithEvents txtTransporter As TextBox
    Protected WithEvents txtVehicle As TextBox
    Protected WithEvents txtDriverName As TextBox
    Protected WithEvents txtDriverICNo As TextBox
    Protected WithEvents txtPlantingYear As TextBox
    Protected WithEvents txtBlock As TextBox
    Protected WithEvents txtDateIn As TextBox
    Protected WithEvents txtInHour As TextBox
    Protected WithEvents txtInMinute As TextBox    
    Protected WithEvents txtDateOut As TextBox
    Protected WithEvents txtOutHour As TextBox
    Protected WithEvents txtOutMinute As TextBox        
    Protected WithEvents txtDateRcv As TextBox    
    Protected WithEvents txtDateInTo As TextBox
    Protected WithEvents txtInHourTo As TextBox
    Protected WithEvents txtInMinuteTo As TextBox
    Protected WithEvents txtDateOutTo As TextBox
    Protected WithEvents txtOutHourTo As TextBox
    Protected WithEvents txtOutMinuteTo As TextBox
    Protected WithEvents txtDateRcvTo As TextBox

    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strDateFormat As String
    Dim strLocType as String
    Dim intErrNo As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_PMACCMONTH")
        strAccYear = Session("SS_PMACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        strDateFormat = Session("SS_DATEFMT")
        strLocType = Session("SS_LOCTYPE")

        lblDate.Visible = False
        lblDateFormat.Visible = False
        lblErrTimeInHrFrom.Visible = False
        lblErrTimeInMinFrom.Visible = False
        lblErrTimeInHrTo.Visible = False
        lblErrTimeInMinTo.Visible = False
        lblErrTimeOutHrFrom.Visible = False
        lblErrTimeOutMinFrom.Visible = False
        lblErrTimeOutHrTo.Visible = False
        lblErrTimeOutMinTo.Visible = False

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else

            If Not Page.IsPostBack Then
                onload_GetLangCap()
                BindStatusList()
                BindTransType()
                BindProduct()
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
        lblVehicle.Text = GetCaption(objLangCap.EnumLangCap.Vehicle)
        lblBillParty1.Text = GetCaption(objLangCap.EnumLangCap.BillParty)
        lblBillParty2.Text = GetCaption(objLangCap.EnumLangCap.BillParty)
        lblBlockTag.Text = GetCaption(objLangCap.EnumLangCap.Block)
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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=WM_STDRPT_WEIGHBRIDGETRXLIST_CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/WM_StdRpt_Selection.aspx")
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

    Sub BindProduct()
        lstProduct.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketProduct(objWMTrx.EnumWeighBridgeTicketProduct.ALL), objWMTrx.EnumWeighBridgeTicketProduct.ALL))
        lstProduct.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketProduct(objWMTrx.EnumWeighBridgeTicketProduct.CPO), objWMTrx.EnumWeighBridgeTicketProduct.CPO))
        lstProduct.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketProduct(objWMTrx.EnumWeighBridgeTicketProduct.FFB), objWMTrx.EnumWeighBridgeTicketProduct.FFB))
        lstProduct.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketProduct(objWMTrx.EnumWeighBridgeTicketProduct.PK), objWMTrx.EnumWeighBridgeTicketProduct.PK))
        '17 June 2008
        lstProduct.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketProduct(objWMTrx.EnumWeighBridgeTicketProduct.EFB), objWMTrx.EnumWeighBridgeTicketProduct.EFB))
        lstProduct.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketProduct(objWMTrx.EnumWeighBridgeTicketProduct.Shell), objWMTrx.EnumWeighBridgeTicketProduct.Shell))
        lstProduct.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketProduct(objWMTrx.EnumWeighBridgeTicketProduct.Others), objWMTrx.EnumWeighBridgeTicketProduct.Others))

        lstProduct.SelectedIndex = 0

    End Sub


    Sub BindTransType()
        lstTransactionType.Items.Add(New ListItem("All", "0"))
        lstTransactionType.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketTransType(objWMTrx.EnumWeighBridgeTicketTransType.Purchase), objWMTrx.EnumWeighBridgeTicketTransType.Purchase))
        lstTransactionType.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketTransType(objWMTrx.EnumWeighBridgeTicketTransType.Sales), objWMTrx.EnumWeighBridgeTicketTransType.Sales))
        '17 June 2008
        lstTransactionType.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketTransType(objWMTrx.EnumWeighBridgeTicketTransType.Usage), objWMTrx.EnumWeighBridgeTicketTransType.Usage))
        lstTransactionType.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketTransType(objWMTrx.EnumWeighBridgeTicketTransType.Others), objWMTrx.EnumWeighBridgeTicketTransType.Others))

        lstTransactionType.SelectedIndex = 0
    End Sub


    Sub BindStatusList()

        lstStatus.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketStatus(objWMTrx.EnumWeighBridgeTicketStatus.All), objWMTrx.EnumWeighBridgeTicketStatus.All))
        lstStatus.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketStatus(objWMTrx.EnumWeighBridgeTicketStatus.Active), objWMTrx.EnumWeighBridgeTicketStatus.Active))
        lstStatus.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketStatus(objWMTrx.EnumWeighBridgeTicketStatus.Deleted), objWMTrx.EnumWeighBridgeTicketStatus.Deleted))

    End Sub


    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strddlAccMth As String
        Dim strddlAccYr As String
        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String

        Dim tempAccMth As DropDownList
        Dim tempAccYr As DropDownList
        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden

        Dim strTicketNo As String
        Dim strTransactionType As String
        Dim strProduct As String
        Dim strSuppBillParty As String
        Dim strDocRefNo As String
        Dim strDeliveryNoteNo As String
        Dim strPL3No As String
        Dim strTransporter As String
        Dim strVehicle As String
        Dim strDriverName As String
        Dim strDriverICNo As String
        Dim strPlantingYear As String
        Dim strBlock As String
        Dim strDateIn As String
        Dim strDateInTo As String
        Dim strInHour As String
        Dim strInHourTo As String
        Dim strInMinute As String
        Dim strInMinuteTo As String
        Dim strInAMPM As String
        Dim strInAMPMTo As String
        Dim strDateOut As String
        Dim strDateOutTo As String
        Dim strOutHour As String
        Dim strOutHourTo As String
        Dim strOutMinute As String
        Dim strOutMinuteTo As String
        Dim strOutAMPM As String
        Dim strOutAMPMTo As String
        Dim strDateRcv As String
        Dim strDateRcvTo As String
        Dim strStatus As String

        Dim objDateFormat As New Object()
        Dim objDateIn As String
        Dim objDateInTo As String
        Dim objDateOut As String
        Dim objDateOutTo As String
        Dim objDateRcv As String
        Dim objDateRcvTo As String

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

        strTicketNo = Server.UrlEncode(txtTicketNo.Text.Trim)
        strTransactionType = Trim(lstTransactionType.SelectedItem.Value)
        strProduct = Trim(lstProduct.SelectedItem.Value)
        strSuppBillParty = Server.UrlEncode(txtSuppBillParty.Text.Trim)
        strDocRefNo = Server.UrlEncode(txtDocRefNo.Text.Trim)
        strDeliveryNoteNo = Server.UrlEncode(txtDeliveryNoteNo.Text.Trim)
        strPL3No = Server.UrlEncode(txtPL3No.Text.Trim)
        strTransporter = Server.UrlEncode(txtTransporter.Text.Trim)
        strVehicle = Server.UrlEncode(txtVehicle.Text.Trim)
        strDriverName = Server.UrlEncode(txtDriverName.Text.Trim)
        strDriverICNo = Server.UrlEncode(txtDriverICNo.Text.Trim)
        strPlantingYear = Server.UrlEncode(txtPlantingYear.Text.Trim)
        strBlock = Server.UrlEncode(txtBlock.Text.Trim)
        strDateIn = Trim(txtDateIn.Text)
        strDateInTo = Trim(txtDateInTo.Text)
        strInHour = Server.UrlEncode(txtInHour.Text.Trim)
        strInMinute = Server.UrlEncode(txtInMinute.Text.Trim)
        strInAMPM = Trim(lstInAMPM.SelectedItem.Text)
        strInHourTo = Server.UrlEncode(txtInHourTo.Text.Trim)
        strInMinuteTo = Server.UrlEncode(txtInMinuteTo.Text.Trim)
        strInAMPMTo = Trim(lstInAMPMTo.SelectedItem.Text)
        strDateOut = Trim(txtDateOut.Text)
        strDateOutTo = Trim(txtDateOutTo.Text)
        strOutHour = Server.UrlEncode(txtOutHour.Text.Trim)
        strOutMinute = Server.UrlEncode(txtOutMinute.Text.Trim)
        strOutAMPM = Trim(lstOutAMPM.SelectedItem.Text)
        strOutHourTo = Server.UrlEncode(txtOutHourTo.Text.Trim)
        strOutMinuteTo = Server.UrlEncode(txtOutMinuteTo.Text.Trim)
        strOutAMPMTo = Trim(lstOutAMPMTo.SelectedItem.Text)
        strDateRcv = Trim(txtDateRcv.Text)
        strDateRcvTo = Trim(txtDateRcvTo.Text)
        strStatus = Trim(lstStatus.SelectedItem.Text)

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

        If txtInHour.Text.Trim <> "" And txtInMinute.Text.Trim = "" Then
            lblErrTimeInMinFrom.Visible = True
            Exit Sub
        ElseIf txtInHour.Text.Trim = "" And txtInMinute.Text.Trim <> "" Then
            lblErrTimeInHrFrom.Visible = True
            Exit Sub
        End If

        If txtInHourTo.Text.Trim <> "" And txtInMinuteTo.Text.Trim = "" Then
            lblErrTimeInMinTo.Visible = True
            Exit Sub
        ElseIf txtInHourTo.Text.Trim = "" And txtInMinuteTo.Text.Trim <> "" Then
            lblErrTimeInHrTo.Visible = True
            Exit Sub
        End If

        If txtOutHour.Text.Trim <> "" And txtOutMinute.Text.Trim = "" Then
            lblErrTimeOutMinFrom.Visible = True
            Exit Sub
        ElseIf txtOutHour.Text.Trim = "" And txtOutMinute.Text.Trim <> "" Then
            lblErrTimeOutHrFrom.Visible = True
            Exit Sub
        End If

        If txtOutHourTo.Text.Trim <> "" And txtOutMinuteTo.Text.Trim = "" Then
            lblErrTimeOutMinTo.Visible = True
            Exit Sub
        ElseIf txtOutHourTo.Text.Trim = "" And txtOutMinuteTo.Text.Trim <> "" Then
            lblErrTimeOutHrTo.Visible = True
            Exit Sub
        End If

        If Not (strDateIn = "" And strDateInTo = "") Or Not (strDateOut = "" And strDateOutTo = "") Or Not (strDateRcv = "" And strDateRcvTo = "") Then
            If (objGlobal.mtdValidInputDate(strDateFormat, strDateIn, objDateFormat, objDateIn) = True And objGlobal.mtdValidInputDate(strDateFormat, strDateInTo, objDateFormat, objDateInTo) = True) Or (objGlobal.mtdValidInputDate(strDateFormat, strDateOut, objDateFormat, objDateOut) = True And objGlobal.mtdValidInputDate(strDateFormat, strDateOutTo, objDateFormat, objDateOutTo) = True) Or (objGlobal.mtdValidInputDate(strDateFormat, strDateRcv, objDateFormat, objDateRcv) = True And objGlobal.mtdValidInputDate(strDateFormat, strDateRcvTo, objDateFormat, objDateRcvTo) = True) Then
                Response.Write("<Script Language=""JavaScript"">window.open(""WM_StdRpt_WeighBridge_Ticket_ListingPreview.aspx?Type=Print&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & strDec & _
                                "&lblLocation=" & lblLocation.Text & "&lblVehicle=" & lblVehicle.Text & "&lblBlockTag=" & lblBlockTag.Text & "&lblBillParty=" & lblBillParty1.Text & _
                                "&ddlAccMth=" & strddlAccMth & "&ddlAccYr=" & strddlAccYr & _
                                "&TicketNo=" & strTicketNo & "&TransactionType=" & strTransactionType & "&Product=" & strProduct & _
                                "&SuppBillParty=" & strSuppBillParty & "&DocRefNo=" & strDocRefNo & _
                                "&DeliveryNoteNo=" & strDeliveryNoteNo & "&PL3No=" & strPL3No & _
                                "&Transporter=" & strTransporter & "&Vehicle=" & strVehicle & _
                                "&DriverName=" & strDriverName & "&DriverICNo=" & strDriverICNo & _
                                "&PlantingYear=" & strPlantingYear & "&Block=" & strBlock & _
                                "&DateInFrom=" & objDateIn & "&DateInTo=" & objDateInTo & _
                                "&InHour=" & strInHour & "&InMinute=" & strInMinute & "&InAMPM=" & strInAMPM & _
                                "&InHourTo=" & strInHourTo & "&InMinuteTo=" & strInMinuteTo & "&InAMPMTo=" & strInAMPMTo & _
                                "&DateOutFrom=" & objDateOut & "&DateOutTo=" & objDateOutTo & _
                                "&OutHour=" & strOutHour & "&OutMinute=" & strOutMinute & "&OutAMPM=" & strOutAMPM & _
                                "&OutHourTo=" & strOutHourTo & "&OutMinuteTo=" & strOutMinuteTo & "&OutAMPMTo=" & strOutAMPMTo & _
                                "&DateRcv=" & objDateRcv & "&DateRcvTo=" & objDateRcvTo & _
                                "&Status=" & strStatus & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

            Else
                lblDateFormat.Text = objDateFormat & "."
                lblDate.Visible = True
                lblDateFormat.Visible = True
            End If
        Else
            Response.Write("<Script Language=""JavaScript"">window.open(""WM_StdRpt_WeighBridge_Ticket_ListingPreview.aspx?Type=Print&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & strDec & _
                            "&lblLocation=" & lblLocation.Text & "&lblVehicle=" & lblVehicle.Text & "&lblBlockTag=" & lblBlockTag.Text & "&lblBillParty=" & lblBillParty1.Text & _
                            "&ddlAccMth=" & strddlAccMth & "&ddlAccYr=" & strddlAccYr & _
                            "&TicketNo=" & strTicketNo & "&TransactionType=" & strTransactionType & "&Product=" & strProduct & _
                            "&SuppBillParty=" & strSuppBillParty & "&DocRefNo=" & strDocRefNo & _
                            "&DeliveryNoteNo=" & strDeliveryNoteNo & "&PL3No=" & strPL3No & _
                            "&Transporter=" & strTransporter & "&Vehicle=" & strVehicle & _
                            "&DriverName=" & strDriverName & "&DriverICNo=" & strDriverICNo & _
                            "&PlantingYear=" & strPlantingYear & "&Block=" & strBlock & _
                            "&DateInFrom=" & objDateIn & "&DateInTo=" & objDateInTo & _
                            "&InHour=" & strInHour & "&InMinute=" & strInMinute & "&InAMPM=" & strInAMPM & _
                            "&InHourTo=" & strInHourTo & "&InMinuteTo=" & strInMinuteTo & "&InAMPMTo=" & strInAMPMTo & _
                            "&DateOutFrom=" & objDateOut & "&DateOutTo=" & objDateOutTo & _
                            "&OutHour=" & strOutHour & "&OutMinute=" & strOutMinute & "&OutAMPM=" & strOutAMPM & _
                            "&OutHourTo=" & strOutHourTo & "&OutMinuteTo=" & strOutMinuteTo & "&OutAMPMTo=" & strOutAMPMTo & _
                            "&DateRcv=" & objDateRcv & "&DateRcvTo=" & objDateRcvTo & _
                            "&Status=" & strStatus & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
        End If
    End Sub

End Class
 
