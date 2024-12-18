Imports System
Imports System.Data
Imports System.IO 
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Information
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction
Imports Microsoft.VisualBasic

Imports agri.GlobalHdl.clsGlobalHdl


Public Class PD_trx_MPOBPriceDet : Inherits Page

    Protected WithEvents ddlAccMonth As Dropdownlist
    Protected WithEvents ddlAccYear As Dropdownlist
    Protected WithEvents lblAccMonth As Label
    Protected WithEvents lblAccYear As Label
    Protected WithEvents lblProduct As Label
    Protected WithEvents lblProdCode As Label
    Protected WithEvents ddlProduct As Dropdownlist
    Protected WithEvents txtPrice As Textbox

    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label

    Protected WithEvents tbcode As HtmlInputHidden
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents TrWarning As HtmlTableRow
    Protected WithEvents GenDNCNBtn As ImageButton

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrDup As Label
    Protected WithEvents lblErrAddress As Label
    Protected WithEvents lblHidPrice As Label
    Protected WithEvents lblGenDNCNStatus As Label
    Protected WithEvents lblHidAccMonth As Label
    Protected WithEvents lblHidAccYear As Label

    Dim objPDTrx As New agri.PD.clsTrx()
    Dim objCMTrx As New agri.CM.clsTrx()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objHRSetup As New agri.HR.clsSetup()

    Dim objMPOBDs As New Object()
    Dim objCheckDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intPDAR As Integer
    Dim intCMAR As Integer
    Dim blnIsMillware As Boolean
    Dim strCostLevel As String
    Dim strYieldLevel As String

    Dim strCompositKey As String = ""
    Dim intStatus As Integer
    Dim intMaxLen As Integer = 0

	
	Protected WithEvents txtDocDate As TextBox
	Protected WithEvents lblDateError As Label
	

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PDACCMONTH")
        strAccYear = Session("SS_PDACCYEAR")
        intPDAR = Session("SS_PDAR")
        intCMAR = Session("SS_CMAR")
        blnIsMillware = Session("PW_ISMILLWARE")
        strCostLevel = Session("SS_COSTLEVEL")
        strYieldLevel = Session("SS_YIELDLEVEL")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDMPOBPrice), intPDAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrDup.Visible = False
            lblGenDNCNStatus.Visible = False

            strCompositKey = Trim(IIf(Request.QueryString("tbcode") <> "", Request.QueryString("tbcode"), Request.Form("tbcode")))
            intStatus = CInt(lblHiddenSts.Text)
            If Not IsPostBack Then
                If strCompositKey <> "" Then
                    tbcode.Value = strCompositKey
                    onLoad_Display()
                    onLoad_BindButton()
                Else
                    BindAccMonthList("")
                    BindAccYearList("")
                    BindProductList("")
                    onLoad_BindButton()
					txtDocDate.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Now()) 
                End If
            End If
        End If
    End Sub


    Sub BindProductList(ByVal pv_strProdCode As String)
        Dim intCnt As Integer

        If ddlProduct.Items.Count = 0 Then
            ddlProduct.Items.Add(New ListItem(objPDTrx.mtdGetProductType(objPDTrx.EnumProductType.CPO), objPDTrx.EnumProductType.CPO))
            ddlProduct.Items.Add(New ListItem(objPDTrx.mtdGetProductType(objPDTrx.EnumProductType.PK), objPDTrx.EnumProductType.PK))
            ddlProduct.Items.Add(New ListItem(objPDTrx.mtdGetProductType(objPDTrx.EnumProductType.FFB), objPDTrx.EnumProductType.FFB))
        End If

        If Trim(pv_strProdCode) <> "" Then
            For intCnt=0 To ddlProduct.Items.Count - 1
                If ddlProduct.Items(intCnt).Value = pv_strProdCode Then
                    exit for
                End If
            Next
            ddlProduct.SelectedIndex = intCnt
        Else
            ddlProduct.SelectedIndex = 0
        End If

    End Sub

    Sub BindAccMonthList(ByVal pv_strAccMonth As String)
        Dim intCnt As Integer
        Dim CurrDate As Date
        Dim CurrMonth As Integer

        CurrDate = Today
        CurrMonth = Month(CurrDate)

        For intCnt=0 To ddlAccMonth.Items.Count - 1
            If pv_strAccMonth = "" Then
                If ddlAccMonth.Items(intCnt).text = CStr(CurrMonth) Then
                    ddlAccMonth.SelectedIndex = intCnt
                End If
            Else
                If ddlAccMonth.Items(intCnt).value = pv_strAccMonth Then
                    ddlAccMonth.SelectedIndex = intCnt
                End If
            End If            
        Next


    End Sub

    Sub BindAccYearList(ByVal pv_strAccYear As String)
        
        Dim intCnt As Integer
        Dim CurrDate As Date
        Dim CurrYear As Integer
        Dim intCntAddYr As Integer = 1
        Dim intCntMinYr As Integer = 5
        Dim NewAddCurrYear As Integer
        Dim NewMinCurrYear As Integer
        Dim intCntddlYr As Integer = 0

        CurrDate = Today
        CurrYear = Year(CurrDate)

        While intCntMinYr <> 0
            intCntMinYr = intCntMinYr - 1
            NewMinCurrYear = CurrYear - intCntMinYr
            ddlAccYear.Items.Add(NewMinCurrYear)
        End While

        For intCntAddYr = 0 To 5
            NewAddCurrYear = CurrYear + intCntAddYr
            ddlAccYear.Items.Add(NewAddCurrYear)
        Next

        For intCntddlYr = 0 To ddlAccYear.Items.Count - 1
            If Trim(pv_strAccYear) = "" Then
                If ddlAccYear.Items(intCntddlYr).text = CurrYear Then
                    ddlAccYear.SelectedIndex = intCntddlYr
                End If
            Else
                If ddlAccYear.Items(intCntddlYr).text = pv_strAccYear Then
                    ddlAccYear.SelectedIndex = intCntddlYr
                End If
            End If
        Next
    End Sub

    Sub onLoad_Display()
        Dim strOpCd As String = "CM_CLSTRX_MPOB_GET"
        Dim strParam As String        
        Dim intErrNo As Integer
        Dim strSearch As String
        
        strSearch = "and rtrim(mo.LocCode) + rtrim(mo.AccMonth) + rtrim(mo.AccYear) + rtrim(mo.ProductCode) = '" & strCompositKey & "' "
        strParam = strSearch & "|" & ""

        Try
            intErrNo = objPDTrx.mtdGetMPOBPrice(strOpCd, strParam, 0, objMPOBDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PD_TRX_MPOBPRICEDET_GET&errmesg=" & lblErrMessage.Text & "&redirect=CM/trx/CM_Trx_MPOBPriceList.aspx")
        End Try

        lblProduct.text = objPDTrx.mtdGetProductType(Trim(objMPOBDs.Tables(0).Rows(0).Item("ProductCode")))
        lblProdCode.text = Trim(objMPOBDs.Tables(0).Rows(0).Item("ProductCode"))

        lblAccMonth.text = objGlobal.GetShortMonth(Trim(objMPOBDs.Tables(0).Rows(0).Item("AccMonth"))) & " "
        lblAccYear.text = Trim(objMPOBDs.Tables(0).Rows(0).Item("AccYear"))
        lblHidAccMonth.text = Trim(objMPOBDs.Tables(0).Rows(0).Item("AccMonth"))
        lblHidAccYear.text = Trim(objMPOBDs.Tables(0).Rows(0).Item("AccYear"))

        txtPrice.Text = objGlobal.DisplayForEditCurrencyFormat(objMPOBDs.Tables(0).Rows(0).Item("Price"))
        lblHidPrice.text = Trim(objMPOBDs.Tables(0).Rows(0).Item("Price"))

        intStatus = CInt(Trim(objMPOBDs.Tables(0).Rows(0).Item("Status")))
        lblHiddenSts.Text = Trim(objMPOBDs.Tables(0).Rows(0).Item("Status"))
        lblStatus.Text = objPDTrx.mtdGetMPOBPriceStatus(Trim(objMPOBDs.Tables(0).Rows(0).Item("Status")))

        lblDateCreated.Text = objGlobal.GetLongDate(objMPOBDs.Tables(0).Rows(0).Item("CreateDate"))
        lblLastUpdate.Text = objGlobal.GetLongDate(objMPOBDs.Tables(0).Rows(0).Item("UpdateDate"))
        lblUpdatedBy.Text = Trim(objMPOBDs.Tables(0).Rows(0).Item("UserName"))
        
		
		txtDocDate.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), objMPOBDs.Tables(0).Rows(0).Item("DocumentDate")) 
		
    End Sub


    Sub onLoad_BindButton()
        ddlAccMonth.Visible = False
        ddlAccYear.Visible = False
        lblAccMonth.Visible = False
        lblAccYear.Visible = False
        ddlProduct.Visible = False
        lblProduct.Visible = False
        txtPrice.Enabled = False
        SaveBtn.Visible = False
        DelBtn.Visible = False
        UnDelBtn.Visible = False
        TrWarning.Visible = False
        GenDNCNBtn.Visible = False

        Select Case intStatus
            Case objPDTrx.EnumMPOBPriceStatus.Active
                lblAccMonth.Visible = True
                lblAccYear.Visible = True
                lblProduct.Visible = True
                txtPrice.Enabled = True
                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"

                If blnIsMillware = True And objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMGenDNCN), intCMAR)=True Then
                    TrWarning.Visible = True
                    GenDNCNBtn.Visible = True
                End If
            Case objPDTrx.EnumMPOBPriceStatus.Deleted
                lblAccMonth.Visible = True
                lblAccYear.Visible = True
                lblProduct.Visible = True
                UnDelBtn.Visible = True
            Case Else
                ddlAccMonth.Visible = True
                ddlAccYear.Visible = True
                ddlProduct.Visible = True
                ddlProduct.Enabled = True
                txtPrice.Enabled = True
                SaveBtn.Visible = True
        End Select        
    End Sub


    Sub Button_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "CM_CLSTRX_MPOB_UPD"
        Dim strOpCd_Get As String = "CM_CLSTRX_MPOB_GET"
        Dim strOpCd_Add As String = "CM_CLSTRX_MPOB_ADD"
        Dim strOpCd_GetContract As String = "CM_CLSTRX_CONTRACT_REG_GET"
        Dim strOpCd_UpdContract As String = "CM_CLSTRX_CONTRACT_REG_UPD"

        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument        
        Dim intErrNo As Integer
        Dim blnIsUpdate As Boolean
        Dim strParam As String = ""
        Dim strMPOBMonth As String 
        Dim strMPOBYear As String
        Dim strProductCode As String
        Dim strPrice As String
        Dim blnDupKey As Boolean

		
		Dim strDocDate As String 

		lblDateError.Visible = False
		If Not Trim(txtDocDate.Text) = "" Then
			strDocDate = CheckDate(txtDocDate.Text)

			If Trim(strDocDate) = "" Then
				Exit Sub
			End If
		End If
		

        If Trim(lblAccMonth.text) = "" Then
            strMPOBMonth = Trim(ddlAccMonth.SelectedItem.Value)
        Else
            strMPOBMonth = lblHidAccMonth.text
        End If

        If Trim(lblAccYear.text) = "" Then
            strMPOBYear = Trim(ddlAccYear.SelectedItem.Value)
        Else
            strMPOBYear = lblHidAccYear.text
        End If

        If Trim(lblProdCode.text) = "" Then
            strProductCode = Trim(ddlProduct.SelectedItem.value)
        Else
            strProductCode = Trim(lblProdCode.text)
        End If

        strPrice = Trim(txtPrice.text)
        strCompositKey = Trim(strLocation) & Trim(strMPOBMonth) & Trim(strMPOBYear) & Trim(strProductCode)

        If strCmdArgs = "Save" Then 
            blnIsUpdate = IIf(intStatus = 0, False, True)
            tbcode.Value = strCompositKey  
            strParam = strLocation & Chr(9) & _
                       strMPOBMonth & Chr(9) & _
                       strMPOBYear & Chr(9) & _
                       strProductCode & Chr(9) & _
                       strPrice & Chr(9) & _
                       objPDTrx.EnumMPOBPriceStatus.Active & Chr(9) & _
					   strDocDate 
            Try
                intErrNo = objPDTrx.mtdUpdMPOBPrice(strOpCd_Get, _
                                                    strOpCd_Add, _
                                                    strOpCd_Upd, _
                                                    strOpCd_GetContract, _
                                                    strOpCd_UpdContract, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    blnDupKey, _
                                                    blnIsUpdate)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PD_TRX_MPOBPRICEDET_SAVE&errmesg=" & lblErrMessage.Text & "&redirect=pd/trx/pd_trx_mpobpricedet.aspx?tbcode=" & strCompositKey)
            End Try

            If blnDupKey = True Then
                lblErrDup.text = "Price for the MPOB Month is already existed. "
                lblErrDup.Visible = True
                Exit Sub
            End If

        ElseIf strCmdArgs = "Del" Then
            strParam = strLocation & Chr(9) & _
                       strMPOBMonth & Chr(9) & _
                       strMPOBYear & Chr(9) & _
                       strProductCode & Chr(9) & Chr(9) & objPDTrx.EnumMPOBPriceStatus.Deleted & Chr(9) & _
					   strDocDate 
            Try
                intErrNo = objPDTrx.mtdUpdMPOBPrice(strOpCd_Get, _
                                                    strOpCd_Add, _
                                                    strOpCd_Upd, _
                                                    "", _
                                                    "", _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    False, _
                                                    True)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PD_TRX_MPOBPRICEDET_DEL&errmesg=" & lblErrMessage.Text & "&redirect=pd/trx/pd_trx_mpobpricedet.aspx?tbcode=" & strCompositKey)
            End Try
        ElseIf strCmdArgs = "UnDel" Then

            strParam = strLocation & Chr(9) & _
                       strMPOBMonth & Chr(9) & _
                       strMPOBYear & Chr(9) & _
                       strProductCode & Chr(9) & Chr(9) & objPDTrx.EnumMPOBPriceStatus.Active & Chr(9) & _
					   strDocDate 
            Try
                intErrNo = objPDTrx.mtdUpdMPOBPrice(strOpCd_Get, _
                                                    strOpCd_Add, _
                                                    strOpCd_Upd, _
                                                    "", _
                                                    "", _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    False, _ 
                                                    True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PD_TRX_MPOBPRICEDET_UNDEL&errmesg=" & lblErrMessage.Text & "&redirect=pd/trx/pd_trx_mpobpricedet.aspx?tbcode=" & strCompositKey)
            End Try
        End If

        If strCompositKey <> "" Then
            onLoad_Display()
            onLoad_BindButton()
        End If

    End Sub

    Sub onClick_GenDNCN(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCdGetContract As String = "CM_CLSTRX_MPOB_GET_NUMBER_OF_DNCN"
        Dim strOpCdTGetMatchLn As String = "CM_CLSTRX_CONTRACTMATCH_LN_GET"
        Dim strOpCdAddDebit As String = "CM_CLSTRX_MPOB_DEBITNOTE_ADD"
        Dim strOpCdAddDebitLn As String = "CM_CLSTRX_MPOB_DEBITNOTE_ADDLINE"
        Dim strOpCdAddCredit As String = "CM_CLSTRX_MPOB_CREDITNOTE_ADD"
        Dim strOpCdAddCreditLn As String = "CM_CLSTRX_MPOB_CREDITNOTE_ADDLINE"
        Dim strOpCdUpdMatchLn As String = "CM_CLSTRX_MPOB_CONTRACTMATCHLN_UPD"
        Dim intErrNo As Integer
        Dim strProductCode As String
        Dim strMPOBMonth As String
        Dim strMPOBYear As String
        Dim decMPOBPrice As String
        Dim strParam As String
		
		Dim strDocDate As String 

		lblDateError.Visible = False
		If Not Trim(txtDocDate.Text) = "" Then
			strDocDate = CheckDate(txtDocDate.Text)

			If Trim(strDocDate) = "" Then
				Exit Sub
			End If
		Else
			lblDateError.Visible = True
			Exit Sub
		End If
		
            
        If Trim(lblAccMonth.text) = "" Then
            strMPOBMonth = Trim(ddlAccMonth.SelectedItem.Value)
        Else
            strMPOBMonth = lblHidAccMonth.text
        End If

        If Trim(lblAccYear.text) = "" Then
            strMPOBYear = Trim(ddlAccYear.SelectedItem.Value)
        Else
            strMPOBYear = lblHidAccYear.text
        End If

        If Trim(lblProdCode.text) = "" Then
            strProductCode = Trim(ddlProduct.SelectedItem.value)
        Else
            strProductCode = Trim(lblProdCode.text)
        End If

        decMPOBPrice = lblHidPrice.text

        strParam = strProductCode & Chr(9) & _
                   strMPOBMonth & Chr(9) & _
                   strMPOBYear & Chr(9) & _
                   strAccMonth & Chr(9) & _
                   strAccYear & Chr(9) & _
                   Cstr(decMPOBPrice) & Chr(9) & _
				   strDocDate 

        Try
            intErrNo = objCMTrx.mtdGenerateDNCN(strOpCdGetContract, _
                                                strOpCdTGetMatchLn, _
                                                strOpCdAddDebit, _
                                                strOpCdAddDebitLn, _
                                                strOpCdAddCredit, _
                                                strOpCdAddCreditLn, _
                                                strOpCdUpdMatchLn, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PD_TRX_MPOBPRICEDET_GENDNCN&errmesg=" & lblErrMessage.Text & "&redirect=pd/trx/pd_trx_mpobpricedet.aspx?tbcode=" & strCompositKey)
        End Try

        If intErrNo <> 0 Then 
            lblGenDNCNStatus.text = "<br>Failed to generate Debit/Credit Notes."
            lblGenDNCNStatus.visible = True
        Else
            lblGenDNCNStatus.text = "<br>Debit/Credit Notes are generated successfully."
            lblGenDNCNStatus.visible = True 
        End If
    End Sub


    Sub BackBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("PD_trx_MPOBPriceList.aspx")
    End Sub

	
	Protected Function CheckDate(ByVal strInvoiceDate As String) As String
        Dim strDateSetting As String
        Dim objSysCfgDs As DataSet
        Dim objDateFormat As String
        Dim strValidDate As String
		Dim strDateFormat As String

		strDateFormat = Session("SS_DATEFMT")
		strInvoiceDate = Trim(strInvoiceDate)

        If Not strInvoiceDate = "" Then
            If objGlobal.mtdValidInputDate(strDateFormat, strInvoiceDate, objDateFormat, strValidDate) = True Then
                Return strValidDate
            Else
                return ""
            End If
        End If
    End Function

	Sub DateValidation(source As Object, args As ServerValidateEventArgs)
		Dim strDate As String = args.Value
		Dim strValidatedDate As String
        
		Try
			strValidatedDate = CheckDate(strDate)
			If Trim(strValidatedDate) = "" Then
				args.IsValid = false
			Else
				args.IsValid = true
			End If
        Catch ex As System.Exception 
            args.IsValid = false
        End Try
    End Sub
	


End Class
