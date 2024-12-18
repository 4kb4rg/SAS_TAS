Imports System
Imports System.Data
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction


Public Class PU_SuppDet : Inherits Page

    Protected WithEvents txtSuppCode As TextBox
    Protected WithEvents txtName As TextBox
    Protected WithEvents txtContactPerson As TextBox
    Protected WithEvents txtAddress As HtmlTextArea
    Protected WithEvents txtTown As TextBox
    Protected WithEvents txtState As TextBox
    Protected WithEvents txtPostCode As TextBox
    Protected WithEvents ddlCountry As DropDownList
    Protected WithEvents txtTelNo As TextBox
    Protected WithEvents txtFaxNo As TextBox
    Protected WithEvents txtMobileTel As TextBox
    Protected WithEvents txtEmail As TextBox
    Protected WithEvents txtFinAccCode As TextBox

    Protected WithEvents txtCreditTerm As TextBox
    Protected WithEvents ddlTermType As DropDownList
    Protected WithEvents txtCreditLimit As TextBox
    Protected WithEvents ddlBankCode As DropDownList
    Protected WithEvents txtBankAccName As TextBox
    Protected WithEvents txtBankAccNo As TextBox
    Protected WithEvents ddlAccCode As DropDownList
    Protected WithEvents SuppCode As HtmlInputHidden
    Protected WithEvents ddlSuppType As DropDownList
    Protected WithEvents txtPOTerms As HtmlTextArea
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdateBy As Label

    Protected WithEvents TxtSKBNo As TextBox
    Protected WithEvents TxtSKBDate As TextBox
    Protected WithEvents TxtSKBDateEnd As TextBox

    Protected WithEvents ChkSKB As CheckBox

    Protected WithEvents lblIssueAccCode As Label
    Protected WithEvents lblErrAccCode As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblErrDup As Label
    Protected WithEvents lblErrAddress As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblSelectList As Label
    Protected WithEvents lblErrSelect As Label
    Protected WithEvents lblErrPOTerms As Label

    Protected WithEvents btnNew As ImageButton
    Protected WithEvents btnSave As ImageButton
    Protected WithEvents btnDelete As ImageButton
    Protected WithEvents btnUnDelete As ImageButton
    Protected WithEvents btnBack As ImageButton
    Protected WithEvents PotSortasi As HtmlTableRow
    Protected WithEvents MaxSortasi As HtmlTableRow
    Protected WithEvents txtPotSortasi As TextBox
    Protected WithEvents txtMaxSortasi As TextBox
    Protected WithEvents txtBankAddress As TextBox
    Protected WithEvents txtBankTown As TextBox
    Protected WithEvents txtBankState As TextBox
    Protected WithEvents ddlBankCountry As DropDownList

    Protected WithEvents txtNPWPName As TextBox
    Protected WithEvents txtNPWPNo As TextBox
    Protected WithEvents txtNPWPAddress As HtmlTextArea
    Protected WithEvents txtPKPNo As TextBox
    Protected WithEvents txtPKPDate As TextBox
    Protected WithEvents lblDate As Label
    Protected WithEvents lblFmt As Label
    Protected WithEvents txtNPWPDate As TextBox
    Protected WithEvents lblDate2 As Label
    Protected WithEvents lblFmt2 As Label
    Protected WithEvents chkPKP As CheckBox

    Protected WithEvents SuppCat As HtmlTableRow
    Protected WithEvents ddlSuppCat As DropDownList
    Protected WithEvents lblErrSuppCat As Label

    Protected WithEvents lblerrBankCode As Label
    Protected WithEvents lblErrBankAccName As Label
    Protected WithEvents lblerrBankAccNo As Label
    Protected WithEvents dgLineDet As DataGrid
    Protected WithEvents hidBankAccNo As HtmlInputHidden
    Protected WithEvents hidStatus As HtmlInputHidden
    Protected WithEvents ddlSuppGroup As DropDownList
    Protected WithEvents lblErrSuppGrp As Label
    Protected WithEvents txtBankRemark As TextBox
    Protected WithEvents hidSuppGrp As HtmlInputHidden

    Protected WithEvents chkPPN As CheckBox
    Protected WithEvents chkPPH22 As CheckBox

    Protected WithEvents ddlPTKPStatus As DropDownList
    Protected WithEvents dgViewJournal As DataGrid
    Protected WithEvents ddlyear As DropDownList

    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objPU As New agri.PU.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCountry As New agri.Admin.clsCountry()
    Dim objHR As New agri.HR.clsSetup()
    Dim objGL As New agri.GL.clsSetup()
    Dim objSuppDs As New Object()
    Dim objCountryDs As New Object()
    Dim objBankDs As New Object()
    Dim objAccountDs As New Object()
    Dim objLangCapDs As New Object()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objGLTrx As New agri.GL.ClsTrx()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()


    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intPUAR As Integer
    Dim strAcceptFormat As String
    Dim strSelectedSupp As String = ""
    Dim strSortExpression As String = "SupplierCode"
    Dim strLocType As String
    Dim intLocLevel As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PUACCMONTH")
        strAccYear = Session("SS_PUACCYEAR")
        intPUAR = Session("SS_PUAR")
        strLocType = Session("SS_LOCTYPE")
        intLocLevel = Session("SS_LOCLEVEL")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUSupplier), intPUAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrAccCode.Visible = False
            lblDate.Visible = False
            lblDate2.Visible = False
            lblErrSuppCat.Visible = False
            lblerrBankCode.Visible = False
            lblErrBankAccName.Visible = False
            lblerrBankAccNo.Visible = False
            lblErrSuppGrp.Visible = False

            strSelectedSupp = Trim(IIf(Request.QueryString("SuppCode") <> "", Request.QueryString("SuppCode"), Request.Form("SuppCode")))
            onload_GetLangCap()
            If Not IsPostBack Then
                BindAccYear(Session("SS_SELACCYEAR"))
                If strSelectedSupp <> "" Then
                    BindPTKPStatus("")
                    onLoad_Display()
                    onLoad_DisplayLn(strSelectedSupp)
                Else
                    TxtSKBDate.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Now)
                    TxtSKBDateEnd.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Now)
                    txtPKPDate.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Now)
                    txtNPWPDate.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Now)


                    BindPTKPStatus("")
                    chkPKP_Change(Sender, E)
                    chkSKB_Change(Sender, E)
                    onLoad_NewDisplay()
                End If
            End If

            If intLocLevel = objAdminLoc.EnumLocLevel.HQ Then
                btnNew.Visible = True
                btnSave.Visible = True
                btnDelete.Visible = True
                btnUnDelete.Visible = True
                'If strCompany <> "KAS" Then
                '    btnNew.Visible = False
                '    btnSave.Visible = False
                '    btnDelete.Visible = False
                '    btnUnDelete.Visible = False
                'Else
                '    btnNew.Visible = True
                '    btnSave.Visible = True
                '    btnDelete.Visible = True
                '    btnUnDelete.Visible = True
                'End If
            Else
                btnSave.Visible = True
                btnDelete.Visible = True
                btnUnDelete.Visible = True
            End If
        End If
    End Sub

    Sub onLoad_Display()
        Dim strOpCd_Supplier As String = "PU_CLSSETUP_SUPPLIER_GET"
        Dim strOpCd_Country As String = "ADMIN_CLSCOUNTRY_COUNTRY_LIST_GET"
        Dim strOpCd_Bank As String = "HR_CLSSETUP_BANK_LIST_GET"
        Dim strOpCd_Account As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intCountryIndex As Integer = 0
        Dim intBankIndex As Integer = 0
        Dim intAccountIndex As Integer = 0
        Dim intSuppTypeIndex As Integer = 0
        Dim intTermTypeIndex As Integer = 0
        Dim intSuppGrpIndex As Integer = 0
        Dim intSuppCatIndex As Integer = 0
        Dim dr As DataRow
        Dim intBankCountryIndex As Integer = 0

        txtSuppCode.Text = strSelectedSupp
        SuppCode.Value = strSelectedSupp

        strParam = strSelectedSupp & "||||" & strSortExpression & "||"
        strParam = strParam & "|" & IIf(Session("SS_COACENTRALIZED") = "1", "", " A.AccCode in (SELECT AccCode FROM GL_Account WHERE LocCode='" & Trim(strLocation) & "') ")

        Try
            intErrNo = objPU.mtdGetSupplier(strOpCd_Supplier, strParam, objSuppDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_SUPPLIER&errmesg=" & lblErrMessage.Text & "&redirect=pu/setup/PU_setup_SuppList.aspx")
        End Try

        If objSuppDs.Tables(0).Rows.Count > 0 Then

            objSuppDs.Tables(0).Rows(0).Item("SupplierCode") = Trim(objSuppDs.Tables(0).Rows(0).Item("SupplierCode"))
            objSuppDs.Tables(0).Rows(0).Item("Name") = Trim(objSuppDs.Tables(0).Rows(0).Item("Name"))
            objSuppDs.Tables(0).Rows(0).Item("ContactPerson") = Trim(objSuppDs.Tables(0).Rows(0).Item("ContactPerson"))
            objSuppDs.Tables(0).Rows(0).Item("Address") = Trim(objSuppDs.Tables(0).Rows(0).Item("Address"))
            objSuppDs.Tables(0).Rows(0).Item("Town") = Trim(objSuppDs.Tables(0).Rows(0).Item("Town"))
            objSuppDs.Tables(0).Rows(0).Item("State") = Trim(objSuppDs.Tables(0).Rows(0).Item("State"))
            objSuppDs.Tables(0).Rows(0).Item("Postcode") = Trim(objSuppDs.Tables(0).Rows(0).Item("PostCode"))
            objSuppDs.Tables(0).Rows(0).Item("CountryCode") = Trim(objSuppDs.Tables(0).Rows(0).Item("CountryCode"))
            objSuppDs.Tables(0).Rows(0).Item("TelNo") = Trim(objSuppDs.Tables(0).Rows(0).Item("TelNo"))
            objSuppDs.Tables(0).Rows(0).Item("FaxNo") = Trim(objSuppDs.Tables(0).Rows(0).Item("FaxNo"))
            objSuppDs.Tables(0).Rows(0).Item("MobileTel") = Trim(objSuppDs.Tables(0).Rows(0).Item("MobileTel"))
            objSuppDs.Tables(0).Rows(0).Item("Email") = Trim(objSuppDs.Tables(0).Rows(0).Item("Email"))
            objSuppDs.Tables(0).Rows(0).Item("FinAccCode") = Trim(objSuppDs.Tables(0).Rows(0).Item("FinAccCode"))
            objSuppDs.Tables(0).Rows(0).Item("CreditTerm") = Trim(objSuppDs.Tables(0).Rows(0).Item("CreditTerm"))
            objSuppDs.Tables(0).Rows(0).Item("TermType") = Trim(objSuppDs.Tables(0).Rows(0).Item("TermType"))
            objSuppDs.Tables(0).Rows(0).Item("CreditLimit") = Trim(objSuppDs.Tables(0).Rows(0).Item("CreditLimit"))
            'objSuppDs.Tables(0).Rows(0).Item("BankCode") = Trim(objSuppDs.Tables(0).Rows(0).Item("BankCode"))
            'objSuppDs.Tables(0).Rows(0).Item("BankAccName") = Trim(objSuppDs.Tables(0).Rows(0).Item("BankAccName"))
            'objSuppDs.Tables(0).Rows(0).Item("BankAccNo") = Trim(objSuppDs.Tables(0).Rows(0).Item("BankAccNo"))
            objSuppDs.Tables(0).Rows(0).Item("AccCode") = Trim(objSuppDs.Tables(0).Rows(0).Item("AccCode"))
            objSuppDs.Tables(0).Rows(0).Item("SuppType") = Trim(objSuppDs.Tables(0).Rows(0).Item("SuppType"))
            objSuppDs.Tables(0).Rows(0).Item("Status") = Trim(objSuppDs.Tables(0).Rows(0).Item("Status"))
            objSuppDs.Tables(0).Rows(0).Item("CreateDate") = Trim(objSuppDs.Tables(0).Rows(0).Item("CreateDate"))
            objSuppDs.Tables(0).Rows(0).Item("UpdateDate") = Trim(objSuppDs.Tables(0).Rows(0).Item("UpdateDate"))
            objSuppDs.Tables(0).Rows(0).Item("UpdateID") = Trim(objSuppDs.Tables(0).Rows(0).Item("UpdateID"))
            objSuppDs.Tables(0).Rows(0).Item("UserName") = Trim(objSuppDs.Tables(0).Rows(0).Item("UserName"))

            objSuppDs.Tables(0).Rows(0).Item("SKBNo") = Trim(objSuppDs.Tables(0).Rows(0).Item("SKBNo"))
            objSuppDs.Tables(0).Rows(0).Item("SKBDate") = Trim(objSuppDs.Tables(0).Rows(0).Item("SKBDate"))
            objSuppDs.Tables(0).Rows(0).Item("SKBDateEnd") = Trim(objSuppDs.Tables(0).Rows(0).Item("SKBDateEnd"))

            'objSuppDs.Tables(0).Rows(0).Item("BankAddress") = Trim(objSuppDs.Tables(0).Rows(0).Item("BankAddress"))
            'objSuppDs.Tables(0).Rows(0).Item("BankTown") = Trim(objSuppDs.Tables(0).Rows(0).Item("BankTown"))
            'objSuppDs.Tables(0).Rows(0).Item("BankState") = Trim(objSuppDs.Tables(0).Rows(0).Item("BankState"))
            'objSuppDs.Tables(0).Rows(0).Item("BankCountry") = Trim(objSuppDs.Tables(0).Rows(0).Item("BankCountry"))

            If IsDBNull(objSuppDs.Tables(0).Rows(0).Item("TermCond")) = False Then
                objSuppDs.Tables(0).Rows(0).Item("TermCond") = Trim(objSuppDs.Tables(0).Rows(0).Item("TermCond"))
            End If


            Try
                intErrNo = objSysCountry.mtdGetCountryList(strOpCd_Country, objCountryDs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_ONLOAD_COUNTRYLIST&errmesg=" & lblErrMessage.Text & "&redirect=pu/setup/PU_setup_SuppList.aspx")
            End Try

            If objCountryDs.Tables(0).Rows.Count > 0 Then
                For intCnt = 0 To objCountryDs.Tables(0).Rows.Count - 1
                    objCountryDs.Tables(0).Rows(intCnt).Item(0) = Trim(objCountryDs.Tables(0).Rows(intCnt).Item(0))
                    objCountryDs.Tables(0).Rows(intCnt).Item(1) = objCountryDs.Tables(0).Rows(intCnt).Item(0) & " (" & Trim(objCountryDs.Tables(0).Rows(intCnt).Item(1)) & ")"
                    If objCountryDs.Tables(0).Rows(intCnt).Item("CountryCode") = objSuppDs.Tables(0).Rows(0).Item("CountryCode") Then
                        intCountryIndex = intCnt + 1
                    End If
                    If objCountryDs.Tables(0).Rows(intCnt).Item("CountryCode") = objSuppDs.Tables(0).Rows(0).Item("BankCountry") Then
                        intBankCountryIndex = intCnt + 1
                    End If
                Next intCnt
            End If

            dr = objCountryDs.Tables(0).NewRow()
            dr("CountryCode") = ""
            dr("CountryDesc") = "Select Country Code"
            objCountryDs.Tables(0).Rows.InsertAt(dr, 0)

            ddlCountry.DataSource = objCountryDs.Tables(0)
            ddlCountry.DataTextField = "CountryDesc"
            ddlCountry.DataValueField = "CountryCode"
            ddlCountry.DataBind()
            ddlCountry.SelectedIndex = intCountryIndex

            ddlBankCountry.DataSource = objCountryDs.Tables(0)
            ddlBankCountry.DataTextField = "CountryDesc"
            ddlBankCountry.DataValueField = "CountryCode"
            ddlBankCountry.DataBind()
            ddlBankCountry.SelectedIndex = intBankCountryIndex

            strParam = "|"
            Try
                intErrNo = objHR.mtdGetMasterList(strOpCd_Bank, strParam, objHR.EnumHRMasterType.Bank, objBankDs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_ONLOAD_BANKLIST&errmesg=" & lblErrMessage.Text & "&redirect=pu/setup/PU_setup_SuppList.aspx")
            End Try

            If objBankDs.Tables(0).Rows.Count > 0 Then
                For intCnt = 0 To objBankDs.Tables(0).Rows.Count - 1
                    objBankDs.Tables(0).Rows(intCnt).Item(0) = Trim(objBankDs.Tables(0).Rows(intCnt).Item(0))
                    objBankDs.Tables(0).Rows(intCnt).Item(1) = objBankDs.Tables(0).Rows(intCnt).Item(0) & " (" & Trim(objBankDs.Tables(0).Rows(intCnt).Item(1)) & ")"
                    If objBankDs.Tables(0).Rows(intCnt).Item("BankCode") = objSuppDs.Tables(0).Rows(0).Item("BankCode") Then
                        intBankIndex = intCnt + 1
                    End If
                Next intCnt
            End If

            dr = objBankDs.Tables(0).NewRow()
            dr("BankCode") = ""
            dr("Description") = "Select Bank Code"
            objBankDs.Tables(0).Rows.InsertAt(dr, 0)

            ddlBankCode.DataSource = objBankDs.Tables(0)
            ddlBankCode.DataTextField = "Description"
            ddlBankCode.DataValueField = "BankCode"
            ddlBankCode.DataBind()
            ddlBankCode.SelectedIndex = intBankIndex

            strParam = "Order By ACC.AccCode|And ACC.Status = '" & objGL.EnumAccountCodeStatus.Active & "' " & _
                        " And ACC.AccType = '" & objGL.EnumAccountType.BalanceSheet & "' " & _
                        " And ACC.NurseryInd = '" & objGL.EnumNurseryAccount.No & "' " 
			'			" AND LocCode = '" & Trim(strLocation) & "' "
           strParam = strParam & IIf(Session("SS_COACENTRALIZED") = "1", "", " AND LocCode = '" & Trim(strLocation) & "' ")

            Try
                intErrNo = objGL.mtdGetMasterList(strOpCd_Account, strParam, objGL.EnumGLMasterType.AccountCode, objAccountDs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_ONLOAD_ACCOUNTLIST&errmesg=" & lblErrMessage.Text & "&redirect=pu/setup/PU_setup_SuppList.aspx")
            End Try

            If objAccountDs.Tables(0).Rows.Count > 0 Then
                For intCnt = 0 To objAccountDs.Tables(0).Rows.Count - 1
                    objAccountDs.Tables(0).Rows(intCnt).Item(0) = Trim(objAccountDs.Tables(0).Rows(intCnt).Item(0))
                    objAccountDs.Tables(0).Rows(intCnt).Item(1) = objAccountDs.Tables(0).Rows(intCnt).Item(0) & " (" & Trim(objAccountDs.Tables(0).Rows(intCnt).Item(1)) & ")"

                    If objAccountDs.Tables(0).Rows(intCnt).Item("AccCode") = objSuppDs.Tables(0).Rows(0).Item("AccCode") Then
                        intAccountIndex = intCnt + 1
                    End If
                Next intCnt
            End If

            dr = objAccountDs.Tables(0).NewRow()
            dr("AccCode") = ""
            dr("Description") = lblSelectList.Text & lblIssueAccCode.Text
            objAccountDs.Tables(0).Rows.InsertAt(dr, 0)

            ddlAccCode.DataSource = objAccountDs.Tables(0)
            ddlAccCode.DataTextField = "Description"
            ddlAccCode.DataValueField = "AccCode"
            ddlAccCode.DataBind()
            ddlAccCode.SelectedIndex = intAccountIndex
            BindTypeList()

            txtSuppCode.ReadOnly = True
            txtSuppCode.Text = strSelectedSupp
            txtName.Text = objSuppDs.Tables(0).Rows(0).Item("Name")
            txtContactPerson.Text = objSuppDs.Tables(0).Rows(0).Item("ContactPerson")
            txtAddress.Value = objSuppDs.Tables(0).Rows(0).Item("Address")
            txtTown.Text = objSuppDs.Tables(0).Rows(0).Item("Town")
            txtState.Text = objSuppDs.Tables(0).Rows(0).Item("State")
            txtPostCode.Text = objSuppDs.Tables(0).Rows(0).Item("PostCode")
            txtTelNo.Text = objSuppDs.Tables(0).Rows(0).Item("TelNo")
            txtFaxNo.Text = objSuppDs.Tables(0).Rows(0).Item("FaxNo")
            txtMobileTel.Text = objSuppDs.Tables(0).Rows(0).Item("MobileTel")
            txtEmail.Text = objSuppDs.Tables(0).Rows(0).Item("Email")
            txtFinAccCode.Text = objSuppDs.Tables(0).Rows(0).Item("FinAccCode")
            txtCreditTerm.Text = objSuppDs.Tables(0).Rows(0).Item("CreditTerm")
            ddlAccCode.SelectedItem.Value = objSuppDs.Tables(0).Rows(0).Item("AccCode")
            txtCreditLimit.Text = objGlobal.DisplayForEditCurrencyFormat(objSuppDs.Tables(0).Rows(0).Item("CreditLimit"))
            ''txtBankAccName.Text = objSuppDs.Tables(0).Rows(0).Item("BankAccName")
            ''txtBankAccNo.Text = objSuppDs.Tables(0).Rows(0).Item("BankAccNo")
            lblStatus.Text = objPU.mtdGetSuppStatus(objSuppDs.Tables(0).Rows(0).Item("Status"))
            lblDateCreated.Text = objGlobal.GetLongDate(objSuppDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objSuppDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdateBy.Text = objSuppDs.Tables(0).Rows(0).Item("UserName")
            ''txtBankAddress.Text = objSuppDs.Tables(0).Rows(0).Item("BankAddress")
            ''txtBankTown.Text = objSuppDs.Tables(0).Rows(0).Item("BankTown")
            ''txtBankState.Text = objSuppDs.Tables(0).Rows(0).Item("BankState")
            txtNPWPName.Text = Trim(objSuppDs.Tables(0).Rows(0).Item("NPWPName"))
            txtNPWPNo.Text = Trim(objSuppDs.Tables(0).Rows(0).Item("NPWPNo"))
            txtNPWPAddress.Value = Trim(objSuppDs.Tables(0).Rows(0).Item("NPWPAddress"))
            txtPKPNo.Text = Trim(objSuppDs.Tables(0).Rows(0).Item("PKPNo"))
            txtPKPDate.Text = Date_Validation(objSuppDs.Tables(0).Rows(0).Item("PKPDate"), True)
            txtNPWPDate.Text = Date_Validation(objSuppDs.Tables(0).Rows(0).Item("NPWPDate"), True)

            TxtSKBNo.Text = Trim(objSuppDs.Tables(0).Rows(0).Item("SKBNo"))
            TxtSKBDate.Text = Date_Validation(objSuppDs.Tables(0).Rows(0).Item("SKBDate"), True)
            TxtSKBDateEnd.Text = Date_Validation(objSuppDs.Tables(0).Rows(0).Item("SKBDateEnd"), True)

            hidStatus.Value = CInt(Trim(objSuppDs.Tables(0).Rows(0).Item("Status")))

            For intCnt = 0 To ddlSuppGroup.Items.Count - 1
                If ddlSuppGroup.Items(intCnt).Value = objSuppDs.Tables(0).Rows(0).Item("SuppGrp") Then
                    intSuppGrpIndex = intCnt
                End If
            Next

            'to capture supplier unit STA that have not fixed yet.
            hidSuppGrp.Value = objSuppDs.Tables(0).Rows(0).Item("SuppGrp")
            If hidSuppGrp.Value = 9 Then
                intSuppGrpIndex = 2
            End If
            ddlSuppGroup.SelectedIndex = intSuppGrpIndex
            ddlSuppGroup.Enabled = False

            If Trim(objSuppDs.Tables(0).Rows(0).Item("PKPInd")) = "0" Then
                chkPKP.Checked = False
                chkPKP.Text = "  No"
                txtPKPNo.Enabled = False
                'txtPKPDate.Enabled = False
            Else
                chkPKP.Checked = True
                chkPKP.Text = "  Yes"
                txtPKPNo.Enabled = True
                'txtPKPDate.Enabled = True
            End If

            If Trim(objSuppDs.Tables(0).Rows(0).Item("PPNInit")) = "0" Then
                chkPPN.Checked = False
                chkPPN.Text = "  No"
            Else
                chkPPN.Checked = True
                chkPPN.Text = "  Yes"
            End If

            If Trim(objSuppDs.Tables(0).Rows(0).Item("PPH22Init")) = "0" Then
                chkPPH22.Checked = False
                chkPPH22.Text = "  No"
            Else
                chkPPH22.Checked = True
                chkPPH22.Text = "  Yes"
            End If


            If Trim(objSuppDs.Tables(0).Rows(0).Item("SKBIsActivation")) = "0" Then
                ChkSKB.Checked = False
                ChkSKB.Text = "  No"
            Else
                ChkSKB.Checked = True
                ChkSKB.Text = "  Yes"
            End If

            ddlPTKPStatus.SelectedValue = Trim(objSuppDs.Tables(0).Rows(0).Item("PTKPStatus"))

            For intCnt = 0 To ddlTermType.Items.Count - 1
                If ddlTermType.Items(intCnt).Value = objSuppDs.Tables(0).Rows(0).Item("TermType") Then
                    intTermTypeIndex = intCnt
                End If
            Next
            ddlTermType.SelectedIndex = intTermTypeIndex

            For intCnt = 0 To ddlSuppType.Items.Count - 1
                If ddlSuppType.Items(intCnt).Value = objSuppDs.Tables(0).Rows(0).Item("SuppType") Then
                    intSuppTypeIndex = intCnt
                End If
            Next
            ddlSuppType.SelectedIndex = intSuppTypeIndex

            If ddlSuppType.SelectedItem.Value = objPU.EnumSupplierType.FFBSupplier Then
                PotSortasi.Visible = True
                MaxSortasi.Visible = True
                txtPotSortasi.Text = objSuppDs.Tables(0).Rows(0).Item("PotonganSortasi")
                txtMaxSortasi.Text = objSuppDs.Tables(0).Rows(0).Item("MaxSortasi")
                SuppCat.Visible = True

                For intCnt = 0 To ddlSuppCat.Items.Count - 1
                    If ddlSuppCat.Items(intCnt).Value = objSuppDs.Tables(0).Rows(0).Item("SuppCat") Then
                        intSuppCatIndex = intCnt
                    End If
                Next
                ddlSuppCat.SelectedIndex = intSuppCatIndex
            Else
                PotSortasi.Visible = False
                MaxSortasi.Visible = False
                SuppCat.Visible = False
            End If

            If IsDBNull(objSuppDs.Tables(0).Rows(0).Item("TermCond")) Then
                txtPOTerms.Value = ""
            Else
                txtPOTerms.Value = objSuppDs.Tables(0).Rows(0).Item("TermCond")
            End If

            ViewData()
        End If

        Select Case Trim(lblStatus.Text)
            Case objPU.mtdGetSuppStatus(objPU.EnumSuppStatus.Active)
                txtSuppCode.ReadOnly = True
                txtName.ReadOnly = False
                txtContactPerson.ReadOnly = False
                txtAddress.Disabled = False
				txtAddress.Disabled = False
                txtTown.ReadOnly = False
                txtState.ReadOnly = False
                txtPostCode.ReadOnly = False
                ddlCountry.Enabled = True
                txtTelNo.ReadOnly = False
                txtFaxNo.ReadOnly = False
                txtMobileTel.ReadOnly = False
                txtEmail.ReadOnly = False
                txtFinAccCode.ReadOnly = False
                txtCreditTerm.ReadOnly = False
                ddlTermType.Enabled = True
                txtCreditLimit.ReadOnly = False
                ddlBankCode.Enabled = True
                txtBankAccName.ReadOnly = False
                txtBankAccNo.ReadOnly = False
                ddlAccCode.Enabled = True
                ddlSuppType.Enabled = True
                btnSave.Visible = True
                btnDelete.Visible = True
                btnDelete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                btnUnDelete.Visible = False
                txtPOTerms.Disabled = False
                txtPOTerms.Disabled = False
                txtSuppCode.Enabled = True
                txtName.Enabled = True
                txtContactPerson.Enabled = True
                txtTown.Enabled = True
                txtState.Enabled = True
                txtPostCode.Enabled = True
                ddlCountry.Enabled = True
                txtTelNo.Enabled = True
                txtFaxNo.Enabled = True
                txtMobileTel.Enabled = True
                txtEmail.Enabled = True
                txtFinAccCode.Enabled = True
                txtCreditTerm.Enabled = True
                txtCreditLimit.Enabled = True
                txtBankAccName.Enabled = True
                txtBankAccNo.Enabled = True
                txtBankAddress.Enabled = True
                txtBankTown.Enabled = True
                txtBankState.Enabled = True
                ddlBankCountry.Enabled = True
                'ddlBankCountry.SelectedValue = "ID"

            Case objPU.mtdGetSuppStatus(objPU.EnumSuppStatus.Deleted)
                txtSuppCode.ReadOnly = True
                txtName.ReadOnly = True
                txtContactPerson.ReadOnly = True
                txtAddress.Disabled = True
                txtTown.ReadOnly = True
                txtState.ReadOnly = True
                txtPostCode.ReadOnly = True
                ddlCountry.Enabled = False
                txtTelNo.ReadOnly = True
                txtFaxNo.ReadOnly = True
                txtMobileTel.ReadOnly = True
                txtEmail.ReadOnly = True
                txtFinAccCode.ReadOnly = True
                txtCreditTerm.ReadOnly = True
                ddlTermType.Enabled = False
                txtCreditLimit.ReadOnly = True
                ddlBankCode.Enabled = False
                txtBankAccName.ReadOnly = True
                txtBankAccNo.ReadOnly = True
                ddlAccCode.Enabled = False
                btnSave.Visible = False
                btnDelete.Visible = False
                btnUnDelete.Visible = True
                ddlSuppType.Enabled = False
                txtPOTerms.Disabled = True
                txtSuppCode.Enabled = False
                txtName.Enabled = False
                txtContactPerson.Enabled = False
                txtTown.Enabled = False
                txtState.Enabled = False
                txtPostCode.Enabled = False
                ddlCountry.Enabled = False
                txtTelNo.Enabled = False
                txtFaxNo.Enabled = False
                txtMobileTel.Enabled = False
                txtEmail.Enabled = False
                txtFinAccCode.Enabled = False
                txtCreditTerm.Enabled = False
                txtCreditLimit.Enabled = False
                txtBankAccName.Enabled = False
                txtBankAccNo.Enabled = False
                btnSave.Visible = False
                btnDelete.Visible = False
                btnUnDelete.Visible = True
                ddlSuppType.Enabled = False
                txtBankAddress.Enabled = False
                txtBankTown.Enabled = False
                txtBankState.Enabled = False
                ddlBankCountry.Enabled = False
        End Select

        txtBankAccName.Text = ""
        txtBankAccNo.Text = ""
        txtBankAddress.Text = ""
        txtBankTown.Text = ""
        txtBankState.Text = ""
        txtBankRemark.Text = ""
    End Sub

    Sub onLoad_NewDisplay()
        Dim strOpCd_Country As String = "ADMIN_CLSCOUNTRY_COUNTRY_LIST_GET"
        Dim strOpCd_Bank As String = "HR_CLSSETUP_BANK_LIST_GET"
        Dim strOpCd_Account As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        Dim strParam As String = strSelectedSupp
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow

        Try
            intErrNo = objSysCountry.mtdGetCountryList(strOpCd_Country, objCountryDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_COUNTRYLIST&errmesg=" & lblErrMessage.Text & "&redirect=pu/setup/PU_setup_SuppList.aspx")
        End Try

        If objCountryDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objCountryDs.Tables(0).Rows.Count - 1
                objCountryDs.Tables(0).Rows(intCnt).Item(0) = Trim(objCountryDs.Tables(0).Rows(intCnt).Item(0))
                objCountryDs.Tables(0).Rows(intCnt).Item(1) = objCountryDs.Tables(0).Rows(intCnt).Item(0) & " (" & Trim(objCountryDs.Tables(0).Rows(intCnt).Item(1)) & ")"
            Next intCnt
        End If

        dr = objCountryDs.Tables(0).NewRow()
        dr("CountryCode") = ""
        dr("CountryDesc") = "Select Country Code"
        objCountryDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlCountry.DataSource = objCountryDs.Tables(0)
        ddlCountry.DataTextField = "CountryDesc"
        ddlCountry.DataValueField = "CountryCode"
        ddlCountry.DataBind()

        ddlBankCountry.DataSource = objCountryDs.Tables(0)
        ddlBankCountry.DataTextField = "CountryDesc"
        ddlBankCountry.DataValueField = "CountryCode"
        ddlBankCountry.DataBind()

        strParam = "|"
        Try
            intErrNo = objHR.mtdGetMasterList(strOpCd_Bank, strParam, objHR.EnumHRMasterType.Bank, objBankDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_BANKLIST&errmesg=" & lblErrMessage.Text & "&redirect=pu/setup/PU_setup_SuppList.aspx")
        End Try

        If objBankDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objBankDs.Tables(0).Rows.Count - 1
                objBankDs.Tables(0).Rows(intCnt).Item(0) = Trim(objBankDs.Tables(0).Rows(intCnt).Item(0))
                objBankDs.Tables(0).Rows(intCnt).Item(1) = objBankDs.Tables(0).Rows(intCnt).Item(0) & " (" & Trim(objBankDs.Tables(0).Rows(intCnt).Item(1)) & ")"
            Next intCnt
        End If

        dr = objBankDs.Tables(0).NewRow()
        dr("BankCode") = ""
        dr("Description") = "Select Bank Code"
        objBankDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlBankCode.DataSource = objBankDs.Tables(0)
        ddlBankCode.DataTextField = "Description"
        ddlBankCode.DataValueField = "BankCode"
        ddlBankCode.DataBind()


        strParam = "Order By ACC.AccCode|And ACC.Status = '" & _
                   objGL.EnumAccountCodeStatus.Active & _
                   "' And ACC.AccType = '" & objGL.EnumAccountType.BalanceSheet & "'"
        strParam = strParam & IIf(Session("SS_COACENTRALIZED") = "1", "", " AND LocCode = '" & Trim(strLocation) & "' ")
        Try
            intErrNo = objGL.mtdGetMasterList(strOpCd_Account, strParam, objGL.EnumGLMasterType.AccountCode, objAccountDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_ONLOAD_ACCOUNTLIST&errmesg=" & lblErrMessage.Text & "&redirect=pu/setup/PU_setup_SuppList.aspx")
        End Try

        If objAccountDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objAccountDs.Tables(0).Rows.Count - 1
                objAccountDs.Tables(0).Rows(intCnt).Item(0) = Trim(objAccountDs.Tables(0).Rows(intCnt).Item(0))
                objAccountDs.Tables(0).Rows(intCnt).Item(1) = objAccountDs.Tables(0).Rows(intCnt).Item(0) & " (" & Trim(objAccountDs.Tables(0).Rows(intCnt).Item(1)) & ")"
            Next intCnt
        End If

        dr = objAccountDs.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("Description") = lblSelectList.Text & lblIssueAccCode.Text
        objAccountDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlAccCode.DataSource = objAccountDs.Tables(0)
        ddlAccCode.DataTextField = "Description"
        ddlAccCode.DataValueField = "AccCode"
        ddlAccCode.DataBind()
        BindTypeList()
        btnSave.Visible = True
        btnDelete.Visible = False
        btnUnDelete.Visible = False
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblIssueAccCode.Text = GetCaption(objLangCap.EnumLangCap.Account) & lblCode.Text
        lblErrAccCode.Text = lblErrSelect.Text & lblIssueAccCode.Text
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_SETUP_SUPPDET_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=pu/setup/PU_setup_SuppList.aspx")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

        For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If strLocType = objAdminLoc.EnumLocType.Mill Then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                Else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                End If
                Exit For
            End If
        Next
    End Function

    Sub BindTypeList()
        ddlSuppType.Items.Clear()
        ddlSuppType.Items.Add(New ListItem("Select Supplier type", ""))
        ddlSuppType.Items.Add(New ListItem(objPU.mtdGetSupplierType(objPU.EnumSupplierType.Associate), objPU.EnumSupplierType.Associate))
        ddlSuppType.Items.Add(New ListItem(objPU.mtdGetSupplierType(objPU.EnumSupplierType.Contractor), objPU.EnumSupplierType.Contractor))
        ddlSuppType.Items.Add(New ListItem(objPU.mtdGetSupplierType(objPU.EnumSupplierType.External), objPU.EnumSupplierType.External))
        ddlSuppType.Items.Add(New ListItem(objPU.mtdGetSupplierType(objPU.EnumSupplierType.Internal), objPU.EnumSupplierType.Internal))
        ddlSuppType.Items.Add(New ListItem(objPU.mtdGetSupplierType(objPU.EnumSupplierType.FFBSupplier), objPU.EnumSupplierType.FFBSupplier))
        ddlSuppType.Items.Add(New ListItem(objPU.mtdGetSupplierType(objPU.EnumSupplierType.FreightForwarder), objPU.EnumSupplierType.FreightForwarder))
    End Sub

    Sub btnSave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "PU_CLSSETUP_SUPPLIER_UPD"
        Dim strOpCd_Add As String = "PU_CLSSETUP_SUPPLIER_ADD_AUTO"
        Dim strOpCd_Get As String = "PU_CLSSETUP_SUPPLIER_GET"
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strSrchSupp As String
        Dim strName As String = Request.Form("txtName")
        Dim strContactPerson As String = Request.Form("txtContactPerson")
        Dim strAddress As String = Request.Form("txtAddress")
        Dim strTown As String = Request.Form("txtTown")
        Dim strState As String = Request.Form("txtState")
        Dim strPostCode As String = Request.Form("txtPostCode")
        Dim strCountry As String = IIf(Request.Form("ddlCountry") = "", "ID", Request.Form("ddlCountry"))
        Dim strTelNo As String = Request.Form("txtTelNo")
        Dim strFaxNo As String = Request.Form("txtFaxNo")
        Dim strMobileTel As String = Request.Form("txtMobileTel")
        Dim strEmail As String = Request.Form("txtEmail")
        Dim strFinAccCode As String = Request.Form("txtFinAccCode")
        Dim strCreditTerm As String = Request.Form("txtCreditTerm")
        Dim strTermType As String = Request.Form("ddlTermType")
        Dim strCreditLimit As String = Request.Form("txtCreditLimit")
        Dim strBankCode As String = Request.Form("ddlBankCode")
        Dim strBankAccName As String = Request.Form("txtBankAccName")
        Dim strBankAccNo As String = Request.Form("txtBankAccNo")
        Dim strAccCode As String = Request.Form("ddlAccCode")

        'default account by request medan
        'If Mid(txtSuppCode.Text, 1, 6) = "330.11" Then
        '    strAccCode = "33.1.2"
        'End If
        'If ddlSuppGroup.SelectedItem.Value = "1" Then
        '    strAccCode = "33.1.2"
        'Else
        '    strAccCode = "DUMMY"
        'End If

        Dim strSupptype As String = Request.Form("ddlSuppType")
        Dim strPOTerms As String = Request.Form("txtPOTerms")
        Dim strPotSortasi As String = IIf(ddlSuppType.SelectedItem.Value = objPU.EnumSupplierType.FFBSupplier, Request.Form("txtPotSortasi"), 0)
        Dim strMaxSortasi As String = IIf(ddlSuppType.SelectedItem.Value = objPU.EnumSupplierType.FFBSupplier, Request.Form("txtMaxSortasi"), 0)
        Dim strBankAddress As String = Request.Form("txtBankAddress")
        Dim strBankTown As String = Request.Form("txtBankTown")
        Dim strBankState As String = Request.Form("txtBankState")
        Dim strBankCountry As String = IIf(Request.Form("ddlBankCountry") = "", "ID", Request.Form("ddlBankCountry"))
        Dim strNPWPName As String = Request.Form("txtNPWPName")
        Dim strNPWPNo As String = Request.Form("txtNPWPNo")
        Dim strNPWPAddress As String = Request.Form("txtNPWPAddress")
        Dim strPKPNo As String = Request.Form("txtPKPNo")
        Dim strPKPDate As String = Date_Validation(txtPKPDate.Text, False)
        Dim strPKPInd As String = IIf(chkPKP.Checked = True, "1", "0")
        Dim strNPWPDate As String = Date_Validation(txtNPWPDate.Text, False)
        Dim strSuppCat As String = IIf(ddlSuppType.SelectedItem.Value = objPU.EnumSupplierType.FFBSupplier, Request.Form("ddlSuppCat"), 0)
        Dim strSuppGrp As String = Trim(ddlSuppGroup.SelectedItem.Value)
        Dim strPPNInit As String = IIf(chkPPN.Checked = True, "1", "0")
        Dim strPPH22Init As String = IIf(chkPPH22.Checked = True, "1", "0")

        Dim strSKBDate As String = Date_Validation(TxtSKBDate.Text, False)
        Dim strSKBDateEnd As String = Date_Validation(TxtSKBDateEnd.Text, False)
        Dim strSKBInit As String = IIf(ChkSKB.Checked = True, "1", "0")

        Dim strPTKPStatus As String = Request.Form("ddlPTKPStatus")

        Dim strParamName As String
        Dim strParamValue As String
        Dim objSPLDs As New Object

        lblErrPOTerms.Visible = False

        If strSuppGrp = "0" Then
            lblErrSuppGrp.Visible = True
            Exit Sub
        End If
        If Len(strAddress) > 512 Then
            lblErrAddress.Visible = True
            Exit Sub
        ElseIf Len(strPOTerms) > 512 Then
            lblErrPOTerms.Visible = True
            Exit Sub
        ElseIf strAccCode = "" Then
            lblErrAccCode.Visible = True
            Exit Sub
        End If

        If strCreditLimit = "" Then
            strCreditLimit = "0"
        End If

        If txtSuppCode.Text = "" Then
            If ddlSuppGroup.SelectedItem.Value = "1" Then
                strAccCode = "310.02"
            Else
                strAccCode = "DUMMY"
            End If

            strParamName = "LOCCODE|SUPPLIERCODE|NAME|CONTACTPERSON|ADDRESS|TOWN|STATE|POSTCODE|COUNTRYCODE|TELNO|FAXNO|EMAIL|FINACCCODE|CREDITTERM|TERMTYPE|CREDITLIMIT|" & _
                            "BANKCODE|BANKACCNAME|BANKACCNO|ACCCODE|STATUS|SUPPTYPE|CREATEDATE|UPDATEDATE|UPDATEID|TERMCOND|" & _
                            "MOBILETEL|POTONGANSORTASI|MAXSORTASI|BANKADDRESS|BANKTOWN|BANKSTATE|BANKCOUNTRY|NPWPNAME|NPWPNO|NPWPADDRESS|PKPNO|PKPDATE|NPWPDATE|PKPIND|SUPPCAT|SUPPGRP|PPNINIT|" & _
                            "SKBNO|SKBDATE|SKBISACTIVATION|SKBDATEEND|PTKPSTATUS|PPH22INIT"

            strParamValue = strLocation & "|" & txtSuppCode.Text & "|" & UCase(Replace(strName, "'", "''")) & "|" & strContactPerson & "|" & strAddress & "|" & _
                             strTown & "|" & strState & "|" & strPostCode & "|" & strCountry & "|" & strTelNo & "|" & strFaxNo & "|" & strEmail & "|" & strFinAccCode & "|" & strCreditTerm & "|" & strTermType & "|" & strCreditLimit & "|" & _
                             strBankCode & "|" & strBankAccName & "|" & strBankAccNo & "|" & strAccCode & "|" & objPU.EnumSuppStatus.Active & "|" & strSupptype & "|" & Now() & "|" & Now() & "|" & strUserId & "|" & strPOTerms & "|" & _
                             strMobileTel & "|" & strPotSortasi & "|" & strMaxSortasi & "|" & _
                             strBankAddress & "|" & strBankTown & "|" & strBankState & "|" & strBankCountry & "|" & _
                             strNPWPName & "|" & strNPWPNo & "|" & strNPWPAddress & "|" & strPKPNo & "|" & strPKPDate & "|" & _
                             strNPWPDate & "|" & strPKPInd & "|" & strSuppCat & "|" & IIf(hidSuppGrp.Value = "9", "9", strSuppGrp) & "|" & _
                             strPPNInit & "|" & TxtSKBNo.Text & "|" & strSKBDate & "|" & strSKBInit & "|" & strSKBDateEnd & "|" & Trim(strPTKPStatus) & "|" & strPPH22Init

            Try
                intErrNo = objGLTrx.mtdGetDataCommon(strOpCd_Add, _
                                                    strParamName, _
                                                    strParamValue, _
                                                    objSPLDs)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try

            If objSPLDs.Tables(0).Rows.Count > 0 Then
                txtSuppCode.Text = Trim(objSPLDs.Tables(0).Rows(0).Item("SupplierCode"))
            End If

        Else
            'strParamValue = txtSuppCode.Text & "|" & UCase(Replace(strName, "'", "''")) & "|" & strContactPerson & "|" & strAddress & "|" & _
            '     strTown & "|" & strState & "|" & strPostCode & "|" & strCountry & "|" & strTelNo & "|" & _
            '     strFaxNo & "|" & strEmail & "|" & strFinAccCode & "|" & strCreditTerm & "|" & _
            '     strTermType & "|" & strCreditLimit & "|" & strBankCode & "|" & strBankAccName & "|" & _
            '     strBankAccNo & "|" & strAccCode & "|" & objPU.EnumSuppStatus.Active & "|" & strSupptype & "|" & _
            '     strPOTerms & "|" & strMobileTel & "|" & strPotSortasi & "|" & strMaxSortasi & "|" & _
            '     strBankAddress & "|" & strBankTown & "|" & strBankState & "|" & strBankCountry & "|" & _
            '     strNPWPName & "|" & strNPWPNo & "|" & strNPWPAddress & "|" & strPKPNo & "|" & strPKPDate & "|" & _
            '     strNPWPDate & "|" & strPKPInd & "|" & strSuppCat & "|" & IIf(hidSuppGrp.Value = "9", "9", strSuppGrp) & "|" & _
            '     strPPNInit & "|" & TxtSKBNo.Text & "|" & strSKBDate & "|" & strSKBInit

            strParamName = vbNullString
            strParamValue = vbNullString

            strParamName = "SUPPLIERCODE|NAME|CONTACTPERSON|ADDRESS|TOWN|STATE|POSTCODE|COUNTRYCODE|TELNO|FAXNO|EMAIL|FINACCCODE|CREDITTERM|TERMTYPE|CREDITLIMIT|" & _
                            "BANKCODE|BANKACCNAME|BANKACCNO|ACCCODE|STATUS|SUPPTYPE|CREATEDATE|UPDATEDATE|UPDATEID|TERMCOND|" & _
                            "MOBILETEL|POTONGANSORTASI|MAXSORTASI|BANKADDRESS|BANKTOWN|BANKSTATE|BANKCOUNTRY|NPWPNAME|NPWPNO|NPWPADDRESS|PKPNO|PKPDATE|NPWPDATE|PKPIND|SUPPCAT|SUPPGRP|PPNINIT|" & _
                            "SKBNO|SKBDATE|SKBISACTIVATION|SKBDATEEND|PTKPSTATUS|PPH22INIT"

            strParamValue = txtSuppCode.Text & "|" & UCase(Replace(strName, "'", "''")) & "|" & strContactPerson & "|" & strAddress & "|" & _
                             strTown & "|" & strState & "|" & strPostCode & "|" & strCountry & "|" & strTelNo & "|" & strFaxNo & "|" & strEmail & "|" & strFinAccCode & "|" & strCreditTerm & "|" & strTermType & "|" & strCreditLimit & "|" & _
                             strBankCode & "|" & strBankAccName & "|" & strBankAccNo & "|" & strAccCode & "|" & objPU.EnumSuppStatus.Active & "|" & strSupptype & "|" & Now() & "|" & Now() & "|" & strUserId & "|" & strPOTerms & "|" & _
                             strMobileTel & "|" & strPotSortasi & "|" & strMaxSortasi & "|" & _
                             strBankAddress & "|" & strBankTown & "|" & strBankState & "|" & strBankCountry & "|" & _
                             strNPWPName & "|" & strNPWPNo & "|" & strNPWPAddress & "|" & strPKPNo & "|" & strPKPDate & "|" & _
                             strNPWPDate & "|" & strPKPInd & "|" & strSuppCat & "|" & IIf(hidSuppGrp.Value = "9", "9", strSuppGrp) & "|" & _
                             strPPNInit & "|" & TxtSKBNo.Text & "|" & strSKBDate & "|" & strSKBInit & "|" & strSKBDateEnd & "|" & Trim(strPTKPStatus) & "|" & strPPH22Init

            Try

                intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd_Upd, _
                                    strParamName, _
                                    strParamValue)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_UPD_SUPPLIER&errmesg=" & lblErrMessage.Text & "&redirect=pu/setup/PU_setup_SuppList.aspx")
            End Try
        End If

        strSelectedSupp = txtSuppCode.Text

        If intLocLevel = objAdminLoc.EnumLocLevel.HQ Then
            Supplier_Synchronized(strSelectedSupp)
        End If

        onLoad_Display()
        onLoad_DisplayLn(strSelectedSupp)

        'strParamValue = txtSuppCode.Text & "|" & UCase(Replace(strName, "'", "''")) & "|" & strContactPerson & "|" & strAddress & "|" & _
        '           strTown & "|" & strState & "|" & strPostCode & "|" & strCountry & "|" & strTelNo & "|" & _
        '           strFaxNo & "|" & strEmail & "|" & strFinAccCode & "|" & strCreditTerm & "|" & _
        '           strTermType & "|" & strCreditLimit & "|" & strBankCode & "|" & strBankAccName & "|" & _
        '           strBankAccNo & "|" & strAccCode & "|" & objPU.EnumSuppStatus.Active & "|" & strSupptype & "|" & _
        '           strPOTerms & "|" & strMobileTel & "|" & strPotSortasi & "|" & strMaxSortasi & "|" & _
        '           strBankAddress & "|" & strBankTown & "|" & strBankState & "|" & strBankCountry & "|" & _
        '           strNPWPName & "|" & strNPWPNo & "|" & strNPWPAddress & "|" & strPKPNo & "|" & strPKPDate & "|" & _
        '           strNPWPDate & "|" & strPKPInd & "|" & strSuppCat & "|" & strSuppGrp

        'If strSelectedSupp <> "" Then
        '    Try
        '        intErrNo = objPU.mtdUpdSupplier(strOpCd_Add, _
        '                                        strOpCd_Upd, _
        '                                        strCompany, _
        '                                        strLocation, _
        '                                        strUserId, _
        '                                        strParam, _
        '                                        True)
        '    Catch Exp As System.Exception
        '        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_UPD_SUPPLIER&errmesg=" & lblErrMessage.Text & "&redirect=pu/setup/PU_setup_SuppList.aspx")
        '    End Try
        'Else
        '    strSrchSupp = txtSuppCode.Text & "||||" & strSortExpression & "||Add"
        '    strSrchSupp = strSrchSupp & "|" & IIf(Session("SS_COACENTRALIZED") = "1", "", " A.AccCode in (SELECT AccCode FROM GL_Account WHERE LocCode='" & Trim(strLocation) & "') ")

        '    Try
        '        intErrNo = objPU.mtdGetSupplier(strOpCd_Get, strSrchSupp, objSuppDs)
        '    Catch Exp As System.Exception
        '        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_SUPPLIER&errmesg=" & lblErrMessage.Text & "&redirect=pu/setup/PU_setup_SuppList.aspx")
        '    End Try

        '    If objSuppDs.Tables(0).Rows.Count = 0 Then
        '        Try
        '            intErrNo = objPU.mtdUpdSupplier(strOpCd_Add, _
        '                                            strOpCd_Upd, _
        '                                            strCompany, _
        '                                            strLocation, _
        '                                            strUserId, _
        '                                            strParam, _
        '                                            False)
        '        Catch Exp As System.Exception
        '            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_ADD_SUPPLIER&errmesg=" & lblErrMessage.Text & "&redirect=pu/setup/PU_setup_SuppList.aspx")
        '        End Try

        '    Else
        '        lblErrDup.Visible = True
        '    End If
        'End If


    End Sub

    Sub btnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "PU_CLSSETUP_SUPPLIER_UPD_STATUS"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim strParam As String = ""
        Dim intErrNo As Integer

        strParamName = "SUPPLIERCODE|STATUS"

        strParamValue = txtSuppCode.Text & "|" & objPU.EnumSuppStatus.Deleted

        Try
            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd_Upd, _
                                strParamName, _
                                strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_UPD_SUPPLIER&errmesg=" & lblErrMessage.Text & "&redirect=pu/setup/PU_setup_SuppList.aspx")
        End Try

        'strParam = strSelectedSupp & "|||||||||||||||||||" & objPU.EnumSuppStatus.Deleted & "||||||||||||||||||||"

        'Try
        '    intErrNo = objPU.mtdUpdSupplier(strOpCd_Add, _
        '                                    strOpCd_Upd, _
        '                                    strCompany, _
        '                                    strLocation, _
        '                                    strUserId, _
        '                                    strParam, _
        '                                    True)
        'Catch Exp As System.Exception
        '    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_DEL_SUPPLIER&errmesg=" & lblErrMessage.Text & "&redirect=pu/setup/PU_setup_SuppList.aspx")
        'End Try

        If intLocLevel = objAdminLoc.EnumLocLevel.HQ Then
            Supplier_Synchronized(strSelectedSupp)
        End If

        onLoad_Display()
        onLoad_DisplayLn(strSelectedSupp)
    End Sub

    Sub btnUnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "PU_CLSSETUP_SUPPLIER_UPD"
        Dim strOpCd_Add As String = ""
        Dim strParam As String = ""
        Dim intErrNo As Integer

        strParam = strSelectedSupp & "|||||||||||||||||||" & objPU.EnumSuppStatus.Active & "|||||||||||||||||||"

        Try
            intErrNo = objPU.mtdUpdSupplier(strOpCd_Add, _
                                            strOpCd_Upd, _
                                            strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strParam, _
                                            True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_UNDEL_SUPPLIER&errmesg=" & lblErrMessage.Text & "&redirect=pu/setup/PU_setup_SuppList.aspx")
        End Try

        If intLocLevel = objAdminLoc.EnumLocLevel.HQ Then
            Supplier_Synchronized(strSelectedSupp)
        End If

        onLoad_Display()
        onLoad_DisplayLn(strSelectedSupp)
    End Sub

    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PU_setup_SuppList.aspx")
    End Sub

    Sub SupTypeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If ddlSuppType.SelectedItem.Value = objPU.EnumSupplierType.FFBSupplier Then
            PotSortasi.Visible = True
            MaxSortasi.Visible = True
            SuppCat.Visible = True
        Else
            PotSortasi.Visible = False
            MaxSortasi.Visible = False
            SuppCat.Visible = False
        End If
    End Sub

    Function Date_Validation(ByVal pv_strInputDate As String, ByVal pv_blnIsShortDate As Boolean) As String
        Dim objSysCfgDs As New Object
        Dim objActualDate As New Object
        Dim strDateFormat As String
        Dim strParam As String
        Dim intErrNo As Integer

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

    Sub chkPKP_Change(ByVal Sender As Object, ByVal E As EventArgs)
        If chkPKP.Checked = True Then
            chkPKP.Text = "  Yes"
            txtPKPNo.Enabled = True
            txtPKPDate.Enabled = True
        Else
            chkPKP.Text = "  No"
            txtPKPNo.Enabled = False
            txtPKPDate.Enabled = False
        End If
    End Sub

    Sub chkPPN_Change(ByVal Sender As Object, ByVal E As EventArgs)
        If chkPPN.Checked = True Then
            chkPPN.Text = "  Yes"
        Else
            chkPPN.Text = "  No"
        End If
    End Sub

    Sub chkPPH22_Change(ByVal Sender As Object, ByVal E As EventArgs)
        If chkPPH22.Checked = True Then
            chkPPH22.Text = "  Yes"
        Else
            chkPPH22.Text = "  No"
        End If
    End Sub

    Sub chkSKB_Change(ByVal Sender As Object, ByVal E As EventArgs)
        If ChkSKB.Checked = True Then
            ChkSKB.Text = "  Yes"
            TxtSKBDate.Enabled = True
            TxtSKBDateEnd.Enabled = True
            TxtSKBNo.Enabled = True
        Else
            ChkSKB.Text = "  No"
            TxtSKBDate.Enabled = False
            TxtSKBDateEnd.Enabled = False
            TxtSKBNo.Enabled = False
            TxtSKBNo.Text = vbNullString
        End If
    End Sub

    Sub AddBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "PU_CLSSETUP_SUPPLIERBANK_UPDATE"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objEmp As New Object()

        If txtSuppCode.Text = "" Then
            lblErrDup.Visible = True
            lblErrDup.Text = "Please save supplier data first."
            Exit Sub
        End If

        If ddlBankCode.SelectedItem.Value = "" Then
            lblerrBankCode.Visible = True
            Exit Sub
        End If

        If txtBankAccName.Text = "" Then
            lblErrBankAccName.Visible = True
            Exit Sub
        End If

        'If txtBankAccNo.Text = "" Then
        '    lblerrBankAccNo.Visible = True
        '    Exit Sub
        'End If

        strParamName = "SUPPLIERCODE|BANKCODE|BANKACCNAME|BANKACCNO|BANKADDRESS|BANKTOWN|BANKSTATE|BANKCOUNTRY|BANKREMARK|STATUS"
        strParamValue = strSelectedSupp & "|" & Trim(ddlBankCode.SelectedItem.Value) & "|" & UCase(Trim(txtBankAccName.Text)) & _
                       "|" & Trim(txtBankAccNo.Text) & "|" & Trim(txtBankAddress.Text) & "|" & Trim(txtBankTown.Text) & "|" & Trim(txtBankState.Text) & _
                       "|" & IIf(ddlBankCountry.SelectedItem.Value = "", "ID", Trim(ddlBankCountry.SelectedItem.Value)) & "|" & Trim(txtBankRemark.Text) & "|" & objPU.EnumSuppStatus.Active

        Try
            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd_Upd, strParamName, strParamValue)


        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        hidBankAccNo.Value = ""
        If intLocLevel = objAdminLoc.EnumLocLevel.HQ Then
            Supplier_Synchronized(strSelectedSupp)
        End If

        onLoad_Display()
        onLoad_DisplayLn(strSelectedSupp)
    End Sub

    Sub onLoad_DisplayLn(ByVal pv_strSplCode As String)
        Dim strOpCd As String = "PU_CLSSETUP_SUPPLIERBANK_GET"
        Dim intErrNo As Integer
        Dim strParamName As String
        Dim strParamValue As String
        Dim intCnt As Integer = 0
        Dim DelButton As LinkButton
        Dim EdtButton As LinkButton
        Dim CanButton As LinkButton
        Dim objTaxDs As New Object

        strSelectedSupp = IIf(pv_strSplCode = "", "", pv_strSplCode)
        strParamName = "SUPPLIERCODE"
        strParamValue = strSelectedSupp

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objTaxDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        dgLineDet.DataSource = objTaxDs.Tables(0)
        dgLineDet.DataBind()

        If objTaxDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objTaxDs.Tables(0).Rows.Count - 1
                EdtButton = dgLineDet.Items.Item(intCnt).FindControl("lbEdit")
                DelButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                CanButton = dgLineDet.Items.Item(intCnt).FindControl("lbCancel")
                DelButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"

                Select Case Trim(lblStatus.Text)
                    Case objPU.mtdGetSuppStatus(objPU.EnumSuppStatus.Active)
                        If hidBankAccNo.Value = "" Then
                            EdtButton.Visible = True
                            DelButton.Visible = True
                            CanButton.Visible = False
                        Else
                            EdtButton.Visible = False
                            DelButton.Visible = False
                            CanButton.Visible = True
                        End If
                    Case objPU.mtdGetSuppStatus(objPU.EnumSuppStatus.Deleted)
                        EdtButton.Visible = False
                        DelButton.Visible = False
                        CanButton.Visible = False
                End Select
            Next intCnt
        End If
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim lbl As Label
        Dim strOpCd_Upd As String = "PU_CLSSETUP_SUPPLIERBANK_UPDATE"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim lbBankCode As String
        Dim lbBankAccName As String
        Dim lbBankAddress As String
        Dim lbBankTown As String
        Dim lbBankState As String
        Dim lbBankCountry As String

        dgLineDet.EditItemIndex = CInt(E.Item.ItemIndex)

        lbl = E.Item.FindControl("lblBankCode")
        lbBankCode = lbl.Text.Trim
        lbl = E.Item.FindControl("lblBankAccNo")
        hidBankAccNo.Value = lbl.Text.Trim
        lbl = E.Item.FindControl("lblBankAccName")
        lbBankAccName = lbl.Text.Trim
        lbl = E.Item.FindControl("lblBankAddress")
        lbBankAddress = lbl.Text.Trim
        lbl = E.Item.FindControl("lblBankTown")
        lbBankTown = lbl.Text.Trim
        lbl = E.Item.FindControl("lblBankState")
        lbBankState = lbl.Text.Trim
        lbl = E.Item.FindControl("lblBankCountry")
        lbBankCountry = lbl.Text.Trim

        strParamName = "SUPPLIERCODE|BANKCODE|BANKACCNAME|BANKACCNO|BANKADDRESS|BANKTOWN|BANKSTATE|BANKCOUNTRY|STATUS"
        strParamValue = strSelectedSupp & "|" & Trim(lbBankCode) & "|" & Trim(lbBankAccName) & "|" & Trim(hidBankAccNo.Value) & _
                        "|" & Trim(lbBankAddress) & "|" & Trim(lbBankTown) & "|" & Trim(lbBankState) & _
                        "|" & Trim(lbBankCountry) & "|" & objPU.EnumSuppStatus.Deleted

        Try
            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd_Upd, strParamName, strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If intLocLevel = objAdminLoc.EnumLocLevel.HQ Then
            Supplier_Synchronized(strSelectedSupp)
        End If

        onLoad_Display()
        onLoad_DisplayLn(strSelectedSupp)
    End Sub

    Sub DEDR_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        hidBankAccNo.Value = ""
        onLoad_Display()
        onLoad_DisplayLn(strSelectedSupp)
    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOpCd_Bank As String = "HR_CLSSETUP_BANK_LIST_GET"
        Dim strOpCd_Country As String = "ADMIN_CLSCOUNTRY_COUNTRY_LIST_GET"
        Dim lbl As Label
        Dim btn As LinkButton
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim intBankIndex As Integer = 0
        Dim intBankCountryIndex As Integer = 0
        Dim lbBankCode As String

        dgLineDet.EditItemIndex = CInt(E.Item.ItemIndex)

        lbl = E.Item.FindControl("lblBankCode")
        ddlBankCode.SelectedValue = lbl.Text.Trim
        lbl = E.Item.FindControl("lblBankCountry")
        ddlBankCountry.SelectedValue = lbl.Text.Trim

        lbl = E.Item.FindControl("lblBankAccNo")
        txtBankAccNo.Text = lbl.Text.Trim
        hidBankAccNo.Value = lbl.Text.Trim
        lbl = E.Item.FindControl("lblBankAccName")
        txtBankAccName.Text = lbl.Text.Trim
        lbl = E.Item.FindControl("lblBankAddress")
        txtBankAddress.Text = lbl.Text.Trim
        lbl = E.Item.FindControl("lblBankTown")
        txtBankTown.Text = lbl.Text.Trim
        lbl = E.Item.FindControl("lblBankState")
        txtBankState.Text = lbl.Text.Trim
        lbl = E.Item.FindControl("lblBankRemark")
        txtBankState.Text = lbl.Text.Trim

        btn = E.Item.FindControl("lbDelete")
        btn.Visible = False
        btn = E.Item.FindControl("lbEdit")
        btn.Visible = False
        btn = E.Item.FindControl("lbCancel")
        btn.Visible = True

        txtBankAccNo.Enabled = False
        ddlBankCode.Enabled = False
    End Sub

    Sub Supplier_Synchronized(ByVal pv_strSplCode As String)
        Dim strOpCd_Upd As String = "PU_CLSSETUP_SUPPLIER_SYNCHRONIZED"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objEmp As New Object()

        If intLocLevel = objAdminLoc.EnumLocLevel.HQ Then
            strSelectedSupp = Trim(pv_strSplCode)
            strParamName = "SUPPLIERCODE|ORICOMPCODE"
            strParamValue = strSelectedSupp & "|" & Trim(strCompany)

            Try
                intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd_Upd, strParamName, strParamValue)


            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try


            hidBankAccNo.Value = ""
            onLoad_Display()
            onLoad_DisplayLn(strSelectedSupp)
        End If
    End Sub

    Sub BindPTKPStatus(ByVal pv_strallwStatus As String)
        Dim strOpCd_ICType As String = "PR_PR_STP_TANGGUNGAN_LIST_GET"
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectIndex As Integer = 0
        Dim dr As DataRow
        Dim objICTypeDs As New Object()

        ParamName = "SEARCH|SORT"
        ParamValue = "WHERE Status='1'|ORDER By TGCode"

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd_ICType, ParamName, ParamValue, objICTypeDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_TANGGUNGAN_LIST_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objICTypeDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objICTypeDs.Tables(0).Rows.Count - 1
                objICTypeDs.Tables(0).Rows(intCnt).Item("TGCode") = Trim(objICTypeDs.Tables(0).Rows(intCnt).Item("TGCode"))
                objICTypeDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objICTypeDs.Tables(0).Rows(intCnt).Item("TGCode")) & " (" & _
                                                                         Trim(objICTypeDs.Tables(0).Rows(intCnt).Item("Description")) & ")"

                If objICTypeDs.Tables(0).Rows(intCnt).Item("TGCode") = pv_strallwStatus Then
                    intSelectIndex = intCnt + 1
                End If

            Next
        End If

        dr = objICTypeDs.Tables(0).NewRow()
        dr("TGCode") = "-"
        dr("Description") = "Select PTKP Status"
        objICTypeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlPTKPStatus.DataSource = objICTypeDs.Tables(0)
        ddlPTKPStatus.DataTextField = "Description"
        ddlPTKPStatus.DataValueField = "TGCode"
        ddlPTKPStatus.DataBind()
        ddlPTKPStatus.SelectedIndex = intSelectIndex
    End Sub

    Sub btnNew_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PU_setup_SuppDet.aspx")
    End Sub



    Sub dgViewJournal_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='DarkSeaGreen'")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")

            End If
        End If
    End Sub

    Private Sub dgViewJournal_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgViewJournal.ItemDataBound
        If e.Item.ItemType = ListItemType.Header Then
            Dim dgItem As DataGridItem

            dgItem = New DataGridItem(0, 0, ListItemType.Header)
        End If
    End Sub

    Sub ViewData()
        Dim strOpCd As String = "TX_CLSTRX_GENERATE_PPH21_NONKARYAWAN_GETLIST"
        Dim objDataSet As New Object()
        Dim intErrNo As Integer
        Dim ParamName As String = ""
        Dim ParamValue As String = ""

        ParamName = "LOCCODE|SUPPLIERCODE|ACCYEAR"
        ParamValue = strLocation & "|" & Trim(txtSuppCode.Text) & "|" & Trim(ddlyear.SelectedItem.Value)

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, ParamName, ParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MthEnd_PPH21_Estate&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objDataSet.Tables(0).Rows.Count > 0 Then
            dgViewJournal.DataSource = objDataSet
            dgViewJournal.DataBind()
        Else
            dgViewJournal.DataSource = Nothing
            dgViewJournal.DataBind()
        End If

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

        ddlyear.DataSource = objAccYearDs.Tables(0)
        ddlyear.DataValueField = "AccYear"
        ddlyear.DataTextField = "UserName"
        ddlyear.DataBind()
        ddlyear.SelectedIndex = intSelectedIndex - 1
    End Sub

    Sub YearChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ViewData()
    End Sub
End Class
