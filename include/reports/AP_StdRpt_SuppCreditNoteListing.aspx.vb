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

Public Class AP_StdRpt_SuppCreditNoteListing : Inherits Page

    Protected RptSelect As UserControl
    Dim objAP As New agri.AP.clsReport()
    Dim objAP1 As New agri.AP.clsTrx()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminShare As New agri.Admin.clsShare()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAdminLoc As New agri.Admin.clsLoc()



    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label

    Protected WithEvents lblLocation As Label
    Protected WithEvents lblVehicleExp As Label
    Protected WithEvents lblVehicle As Label
    Protected WithEvents lblBlock As Label
    Protected WithEvents lblCOACode As Label
    Protected WithEvents lblErrSuppCreditNoteRefDateFrom As Label
    Protected WithEvents lblVehicleType As Label
    Protected WithEvents lblBlkType As Label
    Protected WithEvents lblBlkGrp As Label
    Protected WithEvents lblBlkCode As Label
    Protected WithEvents lblSubBlkCode As Label
    Protected WithEvents lblDate As Label
    Protected WithEvents lblDateFormat As Label

    Protected WithEvents txtSupplier As TextBox
    Protected WithEvents txtSuppCreditNoteRefNo As TextBox

    Protected WithEvents txtBlkGrp As TextBox
    Protected WithEvents txtBlkCode As TextBox
    Protected WithEvents txtSubBlkCode As TextBox
    Protected WithEvents txtSuppCreditNoteIDFrom As TextBox
    Protected WithEvents txtSuppCreditNoteIDTo As TextBox
    Protected WithEvents txtSuppCreditNoteRefDateFrom As TextBox
    Protected WithEvents txtSuppCreditNoteRefDateTo As TextBox
    Protected WithEvents txtCOACode As TextBox
    Protected WithEvents txtBlock As TextBox
    Protected WithEvents txtVehicleType As TextBox
    Protected WithEvents txtVehicle As TextBox
    Protected WithEvents txtVehicleExp As TextBox
    Protected WithEvents txtSuppCode As TextBox

    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents lstBlkType As DropDownList

    Protected WithEvents TrBlkGrp As HtmlTableRow
    Protected WithEvents TrBlk As HtmlTableRow
    Protected WithEvents TrSubBlk As HtmlTableRow

    Protected WithEvents PrintPrev As ImageButton

    Protected WithEvents lblInvoiceRcvRefNo As Label
    Protected WithEvents txtInvoiceRcvRefNo As TextBox

    Dim objLangCapDs As New Object()
    Dim objTermTypeDs As New Object()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfigsetting As String
    Dim strBlock As String


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
        strAccMonth = Session("SS_APACCMONTH")
        strAccYear = Session("SS_APACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")

        lblDate.Visible = False
        lblDateFormat.Visible = False


        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
                onload_GetLangCap()
                BindStatus()
                BlkTypeList()
            End If

            If lstBlkType.SelectedItem.Value = "BlkCode" Then
                TrBlkGrp.Visible = False
                TrBlk.Visible = True
                TrSubBlk.Visible = False
            ElseIf lstBlkType.SelectedItem.Value = "BlkGrp" Then
                TrBlkGrp.Visible = True
                TrBlk.Visible = False
                TrSubBlk.Visible = False
            ElseIf lstBlkType.SelectedItem.Value = "SubBlkCode" Then
                TrBlkGrp.Visible = False
                TrBlk.Visible = False
                TrSubBlk.Visible = True
            End If

        End If
    End Sub


    Sub BindStatus()

        ddlStatus.Items.Add(New ListItem("All", 0))
        ddlStatus.Items.Add(New ListItem(objAP1.mtdGetCreditNoteStatus(objAP1.EnumInvoiceRcvStatus.Active), objAP1.EnumInvoiceRcvStatus.Active))
        ddlStatus.Items.Add(New ListItem(objAP1.mtdGetCreditNoteStatus(objAP1.EnumInvoiceRcvStatus.Confirmed), objAP1.EnumInvoiceRcvStatus.Confirmed))
        ddlStatus.Items.Add(New ListItem(objAP1.mtdGetCreditNoteStatus(objAP1.EnumInvoiceRcvStatus.Deleted), objAP1.EnumInvoiceRcvStatus.Deleted))
        ddlStatus.Items.Add(New ListItem(objAP1.mtdGetCreditNoteStatus(objAP1.EnumInvoiceRcvStatus.Cancelled), objAP1.EnumInvoiceRcvStatus.Cancelled))
        ddlStatus.Items.Add(New ListItem(objAP1.mtdGetCreditNoteStatus(objAP1.EnumInvoiceRcvStatus.WriteOff), objAP1.EnumInvoiceRcvStatus.WriteOff))
        ddlStatus.Items.Add(New ListItem(objAP1.mtdGetCreditNoteStatus(objAP1.EnumInvoiceRcvStatus.Closed), objAP1.EnumInvoiceRcvStatus.Closed))

    End Sub



    Sub BlkTypeList()

        Dim strBlkGrp As String
        Dim strBlk As String
        Dim strSubBlk As String

        strBlkGrp = Left(lblBlkGrp.Text, Len(lblBlkGrp.Text) - 2)
        strBlk = Left(lblBlkCode.Text, Len(lblBlkCode.Text) - 2)
        strSubBlk = Left(lblSubBlkCode.Text, Len(lblSubBlkCode.Text) - 2)

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
            lstBlkType.Items.Add(New ListItem(strBlk, "BlkCode"))
            lstBlkType.Items.Add(New ListItem(strBlkGrp, "BlkGrp"))
        Else
            lstBlkType.Items.Add(New ListItem(strSubBlk, "SubBlkCode"))
            lstBlkType.Items.Add(New ListItem(strBlk, "BlkCode"))
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
        lblVehicleExp.Text = GetCaption(objLangCap.EnumLangCap.VehExpense)
        lblVehicle.Text = GetCaption(objLangCap.EnumLangCap.Vehicle)
        lblCOACode.Text = GetCaption(objLangCap.EnumLangCap.Account)
        lblVehicleType.Text = GetCaption(objLangCap.EnumLangCap.VehType)
        lblBlkType.Text = GetCaption(objLangCap.EnumLangCap.Block)
        lblBlkGrp.Text = GetCaption(objLangCap.EnumLangCap.Block) & " Group :"
        lblBlkCode.Text = GetCaption(objLangCap.EnumLangCap.Block) & " Code :"
        lblSubBlkCode.Text = GetCaption(objLangCap.EnumLangCap.SubBlock) & " Code :"
        lblInvoiceRcvRefNo.Text = GetCaption(objLangCap.EnumLangCap.InvoiceReceive) & " Ref. No."
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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=AP_STDRPT_STMTOFACC_LIST_CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/AP_StdRpt_Selection.aspx")
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



    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strSupplier As String
        Dim strStmtType As String

        Dim strddlAccMth As String
        Dim strddlAccYr As String
        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String
        Dim strDateFrom As String
        Dim strDateTo As String
        Dim strDateSetting As String


        Dim tempAccMth As DropDownList
        Dim tempAccYr As DropDownList
        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden

        Dim strParam As String
        Dim objSysCfgDs As New Object()
        Dim objDateFormat As New Object()
        Dim objDateFrom As String
        Dim objDateTo As String

        Dim strInvoiceRcvRefNo As String

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

        If txtBlkGrp.Visible = True Then strBlock = Trim(txtBlkGrp.Text)
        If txtBlkCode.Visible = True Then strBlock = Trim(txtBlkCode.Text)
        If txtSubBlkCode.Visible = True Then strBlock = Trim(txtSubBlkCode.Text)


        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"
        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=IN_STDRPT_STKISSUE_GET_CONFIG_DATE&errmesg=" & lblErrMessage.Text & "&redirect=../../en/reports/IN_StdRpt_Selection.aspx")
        End Try

        strInvoiceRcvRefNo = txtInvoiceRcvRefNo.Text.Trim()

        strDateFrom = Trim(txtSuppCreditNoteRefDateFrom.Text)
        strDateTo = Trim(txtSuppCreditNoteRefDateTo.Text)

        strDateSetting = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))

        If Not (strDateFrom = "" And strDateTo = "") Then
            If objGlobal.mtdValidInputDate(strDateSetting, strDateFrom, objDateFormat, objDateFrom) = True And objGlobal.mtdValidInputDate(strDateSetting, strDateTo, objDateFormat, objDateTo) = True Then
                Response.Write("<Script Language=""JavaScript"">window.open(""AP_StdRpt_SuppCreditNoteListingPreview.aspx?CompName=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & strDec & _
                            "&DDLAccMth=" & strddlAccMth & "&DDLAccYr=" & strddlAccYr & "&lblLocation=" & lblLocation.Text & _
                            "&txtSuppCreditNoteRefNo=" & Server.UrlEncode(Trim(txtSuppCreditNoteRefNo.Text)) & "&txtSuppCreditNoteRefDateFrom=" & objDateFrom & "&txtSuppCreditNoteRefDateTo=" & objDateTo & _
                            "&txtCOACode=" & Server.UrlEncode(Trim(txtCOACode.Text)) & "&txtBlock=" & Server.UrlEncode(strBlock) & "&txtVehicle=" & Server.UrlEncode(Trim(txtVehicle.Text)) & _
                            "&txtVehicleExp=" & Server.UrlEncode(Trim(txtVehicleExp.Text)) & "&txtStatus=" & Trim(ddlStatus.SelectedItem.Value) & "&txtVehicleType=" & Server.UrlEncode(Trim(txtVehicleType.Text)) & _
                            "&lblCOACode=" & Trim(lblCOACode.Text) & "&lblVehicle=" & Trim(lblVehicle.Text) & "&lblVehicleType=" & Trim(lblVehicleType.Text) & "&lblVehicleExp=" & Trim(lblVehicleExp.Text) & _
                            "&lstBlkType=" & Trim(lstBlkType.SelectedItem.Value) & "&txtBlkGrp=" & Server.UrlEncode(Trim(txtBlkGrp.Text)) & "&txtBlkCode=" & Server.UrlEncode(Trim(txtBlkCode.Text)) & "&txtSubBlkCode=" & Server.UrlEncode(Trim(txtSubBlkCode.Text)) & _
                            "&lblBlkType=" & Trim(lblBlkType.Text) & "&lblBlkGrp=" & Trim(lblBlkGrp.Text) & "&lblBlkCode=" & Trim(lblBlkCode.Text) & "&lblSubBlkCode=" & Trim(lblSubBlkCode.Text) & "&txtSuppCode=" & Server.UrlEncode(Trim(txtSuppCode.Text)) & _
                            "&txtSuppCreditNoteIDFrom=" & Server.UrlEncode(Trim(txtSuppCreditNoteIDFrom.Text)) & "&txtSuppCreditNoteIDTo=" & Server.UrlEncode(Trim(txtSuppCreditNoteIDTo.Text)) & _
                            "&lblInvoiceRcvRefNo=" & Server.UrlEncode(lblInvoiceRcvRefNo.Text) & "&InvoiceRcvRefNo=" & Server.UrlEncode(strInvoiceRcvRefNo) & _
                            """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
            Else
                lblDateFormat.Text = objDateFormat & "."
                lblDate.Visible = True
                lblDateFormat.Visible = True
            End If
        Else
            Response.Write("<Script Language=""JavaScript"">window.open(""AP_StdRpt_SuppCreditNoteListingPreview.aspx?CompName=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & strDec & _
                            "&DDLAccMth=" & strddlAccMth & "&DDLAccYr=" & strddlAccYr & "&lblLocation=" & lblLocation.Text & _
                            "&txtSuppCreditNoteRefNo=" & Server.UrlEncode(Trim(txtSuppCreditNoteRefNo.Text)) & "&txtSuppCreditNoteRefDateFrom=" & objDateFrom & "&txtSuppCreditNoteRefDateTo=" & objDateTo & _
                            "&txtCOACode=" & Server.UrlEncode(Trim(txtCOACode.Text)) & "&txtBlock=" & Server.UrlEncode(strBlock) & "&txtVehicle=" & Server.UrlEncode(Trim(txtVehicle.Text)) & _
                            "&txtVehicleExp=" & Server.UrlEncode(Trim(txtVehicleExp.Text)) & "&txtStatus=" & Trim(ddlStatus.SelectedItem.Value) & "&txtVehicleType=" & Server.UrlEncode(Trim(txtVehicleType.Text)) & _
                            "&lblCOACode=" & Trim(lblCOACode.Text) & "&lblVehicle=" & Trim(lblVehicle.Text) & "&lblVehicleType=" & Trim(lblVehicleType.Text) & "&lblVehicleExp=" & Trim(lblVehicleExp.Text) & _
                            "&lstBlkType=" & Trim(lstBlkType.SelectedItem.Value) & "&txtBlkGrp=" & Server.UrlEncode(Trim(txtBlkGrp.Text)) & "&txtBlkCode=" & Server.UrlEncode(Trim(txtBlkCode.Text)) & "&txtSubBlkCode=" & Server.UrlEncode(Trim(txtSubBlkCode.Text)) & _
                            "&lblBlkType=" & Trim(lblBlkType.Text) & "&lblBlkGrp=" & Trim(lblBlkGrp.Text) & "&lblBlkCode=" & Trim(lblBlkCode.Text) & "&lblSubBlkCode=" & Trim(lblSubBlkCode.Text) & "&txtSuppCode=" & Server.UrlEncode(Trim(txtSuppCode.Text)) & _
                            "&txtSuppCreditNoteIDFrom=" & Server.UrlEncode(Trim(txtSuppCreditNoteIDFrom.Text)) & "&txtSuppCreditNoteIDTo=" & Server.UrlEncode(Trim(txtSuppCreditNoteIDTo.Text)) & _
                            "&lblInvoiceRcvRefNo=" & Server.UrlEncode(lblInvoiceRcvRefNo.Text) & "&InvoiceRcvRefNo=" & Server.UrlEncode(strInvoiceRcvRefNo) & _
                            """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
        End If

    End Sub

End Class
