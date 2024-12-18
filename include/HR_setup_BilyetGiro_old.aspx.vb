
Imports System
Imports System.Data
Imports System.Collections 
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.PWSystem.clsLangCap


Public Class HR_setup_BilyetGiro : Inherits Page

    Protected WithEvents lblErrMesage As Label
    Protected WithEvents dgLine As DataGrid
    Protected WithEvents lbDelete As LinkButton
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents srchFPNo As TextBox
    Protected WithEvents srchDocID As TextBox
    Protected WithEvents srchSupplier As TextBox
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblErrMessageNo As Label
    Protected WithEvents lstAccMonth As DropDownList
    Protected WithEvents lstAccYear As DropDownList
    Protected WithEvents intRec As HtmlInputHidden
    Protected WithEvents PrintDoc As ImageButton
    Protected WithEvents hidStatus As HtmlInputHidden

    Protected WithEvents ddlVATType As DropDownList

    Protected WithEvents ddlMonth As DropDownList
    Protected WithEvents ddlYear As DropDownList
    Protected WithEvents txtEffDate As TextBox
    Protected WithEvents lblDate As Label
    Protected WithEvents lblFmt As Label
    Protected WithEvents ddlBank As DropDownList
    Protected WithEvents ddlDocType As DropDownList
    Protected WithEvents txtPrefix As TextBox
    Protected WithEvents txtNoFrom As TextBox
    Protected WithEvents txtNoTo As TextBox
    Protected WithEvents dgLineGen As DataGrid
    Protected WithEvents lblErrSequence As Label

    Protected WithEvents GenerateBtn As ImageButton
    Protected WithEvents PostingBtn As ImageButton
    Protected WithEvents RollbackBtn As ImageButton
    Protected WithEvents GenerateNoBtn As ImageButton
    Protected WithEvents PostingNoBtn As ImageButton
    Protected WithEvents RollbackNoBtn As ImageButton

    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objGLTrx As New agri.GL.ClsTrx()
    Protected objCTSetup As New agri.CT.clsSetup()
    Protected objCBTrx As New agri.CB.clsTrx()
    Dim objHRSetup As New agri.HR.clsSetup()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intCTAR As Long

    Dim ObjTaxDs As New Object()
    Dim objConfigDs As New Object()
    Dim objLangCapDs As New Object()

    Dim strTXDescr As String
    Dim strDocID As String
    Dim strSupplierCode As String
    Dim strAccCode As String

    Dim strTaxObjectCodeTag As String
    Dim strDescTag As String
    Dim strActCodeTag As String
    Dim strTitleTag As String
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String
    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim strAcceptDateFormat As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        'intCTAR = Session("SS_CTAR")
		intCTAR = Session("SS_HRAR")
        strLocType = Session("SS_LOCTYPE")
        strSelAccMonth = Session("SS_SELACCMONTH")
        strSelAccYear = Session("SS_SELACCYEAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        'ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTMaster), intCTAR) = False Then
		ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRBankFormat), intCTAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.Text = "" Then
                SortExpression.Text = ""
            End If

            'to avoid double click, on aspx add this : UseSubmitBehavior="false"
            GenerateNoBtn.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(GenerateNoBtn).ToString())
            PostingNoBtn.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(PostingNoBtn).ToString())
            RollbackNoBtn.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(RollbackNoBtn).ToString())

            lblErrMesage.Visible = False
            lblErrMessageNo.Visible = False
            lblDate.Visible = False
            lblFmt.Visible = False
            lblErrSequence.Visible = False 

            If Not Page.IsPostBack Then
                txtEffDate.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Now)

                If Session("SS_FILTERPERIOD") = "0" Then
                    ddlMonth.SelectedValue = strAccMonth
                    BindAccYear(strAccYear)
                Else
                    ddlMonth.SelectedValue = strSelAccMonth
                    BindAccYear(strSelAccYear)
                End If

                BindBankCode("")
            End If
			RollbackNoBtn.Attributes("onclick") = "javascript:return ConfirmAction('rollback or delete cheque/giro number ');"
        End If
    End Sub

    Sub BindBankCode(ByVal pv_strBankCode As String)
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objBankDs As New DataSet()
        Dim intSelectedBankIndex As Integer = 0
        Dim strDate As String = Date_Validation(txtEffDate.Text, False)
        Dim strOpCodeBank As String
        Dim strParamName As String
        Dim strParamValue As String

        strAccYear = Year(strDate)
        strAccMonth = Month(strDate)
        strOpCodeBank = "HR_CLSSETUP_BANK_LOCATION_GET_ONLY"

        strParamName = "LOCCODE|ACCYEAR|ACCMONTH|TRXDATE|ACCCODE"
        strParamValue = strLocation & "|" & strAccYear & "|" & strAccMonth & "|" & strDate & "|"

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCodeBank, _
                                                strParamName, _
                                                strParamValue, _
                                                objBankDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ID_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objBankDs.Tables(0).Rows.Count - 1
            If Trim(objBankDs.Tables(0).Rows(intCnt).Item("BankCode")) = Trim(pv_strBankCode) Then
                intSelectedBankIndex = intCnt + 1
                Exit For
            End If
        Next intCnt


        Dim dr As DataRow
        dr = objBankDs.Tables(0).NewRow()
        dr("BankCode") = ""
        dr("_Description") = "Please select bank code"
        objBankDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlBank.DataSource = objBankDs.Tables(0)
        ddlBank.DataValueField = "BankCode"
        ddlBank.DataTextField = "_Description"
        ddlBank.DataBind()
        ddlBank.SelectedIndex = intSelectedBankIndex
    End Sub

    Sub BindAccYear(ByVal pv_strAccYear As String)
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim intSelectedIndex As Integer
        Dim objAccYearDs As New Object
        Dim dr As DataRow
        Dim strOpCd As String = "ADMIN_CLSACCPERIOD_CONFIG_GET"

        strParamName = "LOCCODE|SEARCHSTR|SORTEXP"
        strParamValue = strLocation & "||Order By HD.AccYear"

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objAccYearDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objAccYearDs.Tables(0).Rows.Count - 1
            objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear") = Trim(objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear"))
            objAccYearDs.Tables(0).Rows(intCnt).Item("UserName") = Trim(objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear"))
            If objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear") = pv_strAccYear Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        ddlYear.DataSource = objAccYearDs.Tables(0)
        ddlYear.DataValueField = "AccYear"
        ddlYear.DataTextField = "UserName"
        ddlYear.DataBind()
        ddlYear.SelectedIndex = intSelectedIndex - 1
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
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=INVOICERCV_GET_CONFIG&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_polist.aspx")
        End Try

        strDateFormat = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))

        If pv_blnIsShortDate Then
            Date_Validation = objGlobal.GetShortDate(strDateFormat, pv_strInputDate)
        Else
            If objGlobal.mtdValidInputDate(strDateFormat, _
                                            pv_strInputDate, _
                                            strAcceptDateFormat, _
                                            objActualDate) = True Then
                Date_Validation = objActualDate
            Else
                Date_Validation = ""
            End If
        End If
    End Function
	
	Sub BindGrid()
		Dim strOpCd_GET As String
        Dim intErrNo As Integer
        Dim strParamName As String
        Dim strParamValue As String
        Dim xCnt As Integer = 0
        Dim strEffDate As String = Date_Validation(txtEffDate.Text, False)
        Dim strBank As String
        Dim strBankAccCode As String
        Dim strBankAccNo As String
        Dim arrParam As Array
		Dim DeleteButton As LinkButton

        If ddlMonth.SelectedItem.Value = "0" Then
            strAccMonth = "1','2','3','4','5','6','7','8','9','10','11','12"
        Else
            strAccMonth = ddlMonth.SelectedItem.Value
        End If
        strAccYear = ddlYear.SelectedItem.Value


        strOpCd_GET = "HR_CLSSETUP_BANK_BILYETGIRO_GET"
        strParamName = "LOCCODE|STRSEARCH"

        If ddlBank.SelectedItem.Value = "" Then
            strBank = ""
            strBankAccCode = ""
            strBankAccNo = ""

            strParamValue = strLocation & "|" & _
                        " AND AccMonth IN ('" & strAccMonth & "') AND AccYear = '" & strAccYear & "' AND DocType = '" & ddlDocType.SelectedItem.Value & "' "
        Else
            arrParam = Split(Trim(ddlBank.SelectedItem.Value), "|")
            strBank = Trim(arrParam(0))
            strBankAccCode = Trim(arrParam(1))
            strBankAccNo = Trim(arrParam(2))

            strParamValue = strLocation & "|" & _
                       " AND AccMonth IN ('" & strAccMonth & "') AND AccYear = '" & strAccYear & "' AND DocType = '" & ddlDocType.SelectedItem.Value & "' " & _
                       " AND BankCode = '" & strBank & "' AND BankAccNo = '" & strBankAccNo & "' "
        End If

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd_GET, _
                                                strParamName, _
                                                strParamValue, _
                                                ObjTaxDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectList_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        dgLineGen.DataSource = ObjTaxDs
        dgLineGen.DataBind()

        hidStatus.Value = ObjTaxDs.Tables(0).Rows(0).Item("Status")

        For intCnt = 0 To ObjTaxDs.Tables(0).Rows.Count - 1
            Select Case hidStatus.Value
                Case objCBTrx.EnumCashBankStatus.Active
                    DeleteButton = dgLineGen.Items.Item(intCnt).FindControl("lbDelete")
                    DeleteButton.Visible = True
                    DeleteButton.Attributes("onclick") = "javascript:return ConfirmAction('set invalid');"
                
                Case objCBTrx.EnumCashBankStatus.Deleted
                    DeleteButton = dgLineGen.Items.Item(intCnt).FindControl("lbDelete")
                    DeleteButton.Visible = False
            End Select
		Next intCnt
	End Sub

    Sub RefreshNoBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_GET As String
        Dim intErrNo As Integer
        Dim strParamName As String
        Dim strParamValue As String
        Dim xCnt As Integer = 0
        Dim strEffDate As String = Date_Validation(txtEffDate.Text, False)
        Dim strBank As String
        Dim strBankAccCode As String
        Dim strBankAccNo As String
        Dim arrParam As Array
		Dim DeleteButton As LinkButton

        If ddlMonth.SelectedItem.Value = "0" Then
            strAccMonth = "1','2','3','4','5','6','7','8','9','10','11','12"
        Else
            strAccMonth = ddlMonth.SelectedItem.Value
        End If
        strAccYear = ddlYear.SelectedItem.Value


        strOpCd_GET = "HR_CLSSETUP_BANK_BILYETGIRO_GET"
        strParamName = "LOCCODE|STRSEARCH"

        If ddlBank.SelectedItem.Value = "" Then
            strBank = ""
            strBankAccCode = ""
            strBankAccNo = ""

            strParamValue = strLocation & "|" & _
                        " AND AccMonth IN ('" & strAccMonth & "') AND AccYear = '" & strAccYear & "' AND DocType = '" & ddlDocType.SelectedItem.Value & "' "
        Else
            arrParam = Split(Trim(ddlBank.SelectedItem.Value), "|")
            strBank = Trim(arrParam(0))
            strBankAccCode = Trim(arrParam(1))
            strBankAccNo = Trim(arrParam(2))

            strParamValue = strLocation & "|" & _
                       " AND AccMonth IN ('" & strAccMonth & "') AND AccYear = '" & strAccYear & "' AND DocType = '" & ddlDocType.SelectedItem.Value & "' " & _
                       " AND BankCode = '" & strBank & "' AND BankAccNo = '" & strBankAccNo & "' "
        End If

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd_GET, _
                                                strParamName, _
                                                strParamValue, _
                                                ObjTaxDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectList_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        dgLineGen.DataSource = ObjTaxDs
        dgLineGen.DataBind()

        hidStatus.Value = ObjTaxDs.Tables(0).Rows(0).Item("Status")

        For intCnt = 0 To ObjTaxDs.Tables(0).Rows.Count - 1
            Select Case hidStatus.Value
                Case objCBTrx.EnumCashBankStatus.Active
                    DeleteButton = dgLineGen.Items.Item(intCnt).FindControl("lbDelete")
                    DeleteButton.Visible = True
                    DeleteButton.Attributes("onclick") = "javascript:return ConfirmAction('set invalid');"
                
                Case objCBTrx.EnumCashBankStatus.Deleted
                    DeleteButton = dgLineGen.Items.Item(intCnt).FindControl("lbDelete")
                    DeleteButton.Visible = False
            End Select
		Next intCnt
    End Sub
		
    Sub GenerateNoBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim objTax As New Object
        Dim strEffDate As String = Date_Validation(txtEffDate.Text, False)
        Dim strOpCd As String
        Dim NoAwal As Integer
        Dim NoAkhir As Integer
        Dim NoTotal As Integer
        Dim strBank As String
        Dim strBankAccCode As String
        Dim strBankAccNo As String
        Dim arrParam As Array

        If ddlBank.SelectedItem.Value = "" Then
            strBank = ""
            strBankAccCode = ""
            strBankAccNo = ""
			lblErrMessageNo.Visible = True
            lblErrMessageNo.Text = "Please select bank"
            Exit Sub
        Else
            arrParam = Split(Trim(ddlBank.SelectedItem.Value), "|")
            strBank = Trim(arrParam(0))
            strBankAccCode = Trim(arrParam(1))
            strBankAccNo = Trim(arrParam(2))
        End If

        If ddlMonth.SelectedItem.Value = "0" Then
            lblErrMessageNo.Text = "Please select only 1 account period to proceed."
            lblErrMessageNo.Visible = True
            Exit Sub
        End If
        If Month(strEffDate) <> Trim(ddlMonth.SelectedItem.Value) Then
            lblDate.Text = "Period and month of effective date not equal."
            lblDate.Visible = True
            Exit Sub
        End If

        If txtNoFrom.Text = "" Or txtNoTo.Text = "" Then
            lblErrSequence.Text = "Sequence no. cannot be empty"
            lblErrSequence.Visible = True
            Exit Sub
        End If
        If Len(txtNoFrom.Text) < 6 Or Len(txtNoTo.Text) < 6 Or Len(txtNoFrom.Text) > 6 Or Len(txtNoTo.Text) > 6 Then
            lblErrSequence.Text = "Sequence no. cannot less/more that 6 digit"
            lblErrSequence.Visible = True
            Exit Sub
        End If
        If txtNoFrom.Text <> "" Or txtNoTo.Text <> "" Then
            NoAwal = CInt(txtNoFrom.Text)
            NoAkhir = CInt(txtNoTo.Text)
            NoTotal = NoAkhir - NoAwal
            If NoTotal < 0 Then
                lblErrSequence.Text = "First number cannot be less than second number"
                lblErrSequence.Visible = True
                Exit Sub
            End If
        End If

        ''cek effective date
        'strOpCd = "TX_CLSTRX_TAXASSIGNMENT_GET"
        'strParamName = "LOCCODE|STRSEARCH"
        'strParamValue = strLocation & "|" & _
        '                " AND FPDate = '" & strEffDate & "' AND FPNo LIKE '" & Trim(ddlTaxTrx.SelectedItem.Value) + Trim(ddlTaxStatus.SelectedItem.Value) + "." + Trim(ddlTaxBranch.SelectedItem.Value) + "." + Trim(txtTaxYear.Text) & "%' "

        'Try
        '    intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
        '                                        strParamName, _
        '                                        strParamValue, _
        '                                        ObjTaxDs)

        'Catch Exp As System.Exception
        '    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectList_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        'End Try

        'If ObjTaxDs.Tables(0).Rows.Count > 0 Then
        '    lblDate.Text = "Effective date with tax format already exist, please use refresh button to review"
        '    lblDate.Visible = True
        '    Exit Sub
        'End If

        strAccMonth = ddlMonth.SelectedItem.Value
        strAccYear = ddlYear.SelectedItem.Value
        strOpCd = "HR_CLSSETUP_BANK_BILYETGIRO_GENERATE_DUMMY"

        strParamName = "LOCCODE|ACCMONTH|ACCYEAR|UPDATEID|BANKCODE|BANKACCNO|DOCTYPE|PREFIX|NOFROM|NOTO|EFFDATE"
        strParamValue = strLocation & "|" & strAccMonth & "|" & strAccYear & "|" & strUserId & "|" & _
                        strBank & "|" & _
                        strBankAccNo & "|" & _
                        ddlDocType.SelectedItem.Value & "|" & _
                        Trim(txtPrefix.Text) & "|" & _
                        txtNoFrom.Text & "|" & txtNoTo.Text & "|" & _
                        strEffDate 

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                ObjTaxDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If Trim(ObjTaxDs.Tables(0).Rows(0).Item("InitMsg")) = 1 Then
            dgLineGen.DataSource = ObjTaxDs.Tables(1)
            dgLineGen.DataBind()
        Else
            lblErrMessageNo.Visible = True
            lblErrMessageNo.Text = Trim(ObjTaxDs.Tables(0).Rows(0).Item("Msg"))
        End If
     
    End Sub

    Sub PostingNoBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim ObjTaxDs As New Object
        Dim strEffDate As String = Date_Validation(txtEffDate.Text, False)
        Dim strOpCd As String
        Dim NoAwal As Integer
        Dim NoAkhir As Integer
        Dim NoTotal As Integer
        Dim strBank As String
        Dim strBankAccCode As String
        Dim strBankAccNo As String
        Dim arrParam As Array

        If ddlBank.SelectedItem.Value = "" Then
            strBank = ""
            strBankAccCode = ""
            strBankAccNo = ""
			lblErrMessageNo.Visible = True
            lblErrMessageNo.Text = "Please select bank"
            Exit Sub
        Else
            arrParam = Split(Trim(ddlBank.SelectedItem.Value), "|")
            strBank = Trim(arrParam(0))
            strBankAccCode = Trim(arrParam(1))
            strBankAccNo = Trim(arrParam(2))
        End If

        If ddlMonth.SelectedItem.Value = "0" Then
            lblErrMessageNo.Text = "Please select only 1 account period to proceed."
            lblErrMessageNo.Visible = True
            Exit Sub
        End If
        If Month(strEffDate) <> Trim(ddlMonth.SelectedItem.Value) Then
            lblDate.Text = "Period and month of effective date not equal."
            lblDate.Visible = True
            Exit Sub
        End If
        
        If txtNoFrom.Text = "" Or txtNoTo.Text = "" Then
            lblErrSequence.Text = "Sequence no. cannot be empty"
            lblErrSequence.Visible = True
            Exit Sub
        End If
        If Len(txtNoFrom.Text) < 6 Or Len(txtNoTo.Text) < 6 Or Len(txtNoFrom.Text) > 6 Or Len(txtNoTo.Text) > 6 Then
            lblErrSequence.Text = "Sequence no. cannot less/more that 6 digit"
            lblErrSequence.Visible = True
            Exit Sub
        End If
        If txtNoFrom.Text <> "" Or txtNoTo.Text <> "" Then
            NoAwal = CInt(txtNoFrom.Text)
            NoAkhir = CInt(txtNoTo.Text)
            NoTotal = NoAkhir - NoAwal
            If NoTotal < 0 Then
                lblErrSequence.Text = "First number cannot be less than second number"
                lblErrSequence.Visible = True
                Exit Sub
            End If
        End If

        ''cek effective date
        'strOpCd = "TX_CLSTRX_TAXASSIGNMENT_GET"
        'strParamName = "LOCCODE|STRSEARCH"
        'strParamValue = strLocation & "|" & _
        '                " AND FPDate = '" & strEffDate & "' AND FPNo LIKE '" & Trim(ddlTaxTrx.SelectedItem.Value) + Trim(ddlTaxStatus.SelectedItem.Value) + "." + Trim(ddlTaxBranch.SelectedItem.Value) + "." + Trim(txtTaxYear.Text) & "%' "

        'Try
        '    intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
        '                                        strParamName, _
        '                                        strParamValue, _
        '                                        ObjTaxDs)

        'Catch Exp As System.Exception
        '    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectList_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        'End Try

        'If ObjTaxDs.Tables(0).Rows.Count > 0 Then
        '    lblDate.Text = "Effective date with tax format already exist, please use refresh button to review"
        '    lblDate.Visible = True
        '    Exit Sub
        'End If

        strAccMonth = ddlMonth.SelectedItem.Value
        strAccYear = ddlYear.SelectedItem.Value
        strOpCd = "HR_CLSSETUP_BANK_BILYETGIRO_GENERATE"

        strParamName = "LOCCODE|ACCMONTH|ACCYEAR|UPDATEID|BANKCODE|BANKACCNO|DOCTYPE|PREFIX|NOFROM|NOTO|EFFDATE"
        strParamValue = strLocation & "|" & strAccMonth & "|" & strAccYear & "|" & strUserId & "|" & _
                        strBank & "|" & _
                        strBankAccNo & "|" & _
                        ddlDocType.SelectedItem.Value & "|" & _
                        Trim(txtPrefix.Text) & "|" & _
                        txtNoFrom.Text & "|" & txtNoTo.Text & "|" & _
                        strEffDate 

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                ObjTaxDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        lblErrMessageNo.Visible = True
        lblErrMessageNo.Text = Trim(ObjTaxDs.Tables(0).Rows(0).Item("Msg"))
        RefreshNoBtn_Click(Sender, E)

        'dgLineGen.DataSource = ObjTaxDs
        'dgLineGen.DataBind()
    End Sub

    Sub RollbackNoBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim ObjTaxDs As New Object
        Dim strEffDate As String = Date_Validation(txtEffDate.Text, False)
        Dim strOpCd As String
        Dim NoAwal As Integer
        Dim NoAkhir As Integer
        Dim NoTotal As Integer
        Dim strBank As String
        Dim strBankAccCode As String
        Dim strBankAccNo As String
        Dim arrParam As Array

        If ddlBank.SelectedItem.Value = "" Then
            strBank = ""
            strBankAccCode = ""
            strBankAccNo = ""
        Else
            arrParam = Split(Trim(ddlBank.SelectedItem.Value), "|")
            strBank = Trim(arrParam(0))
            strBankAccCode = Trim(arrParam(1))
            strBankAccNo = Trim(arrParam(2))
        End If

        If ddlMonth.SelectedItem.Value = "0" Then
            lblErrMessageNo.Text = "Please select only 1 account period to proceed."
            lblErrMessageNo.Visible = True
            Exit Sub
        End If
        If Month(strEffDate) <> Trim(ddlMonth.SelectedItem.Value) Then
            lblDate.Text = "Period and month of effective date not equal."
            lblDate.Visible = True
            Exit Sub
        End If

        strAccMonth = ddlMonth.SelectedItem.Value
        strAccYear = ddlYear.SelectedItem.Value
        strOpCd = "HR_CLSSETUP_BANK_BILYETGIRO_ROLLBACK"

        strParamName = "LOCCODE|ACCMONTH|ACCYEAR|UPDATEID|BANKCODE|BANKACCNO|DOCTYPE|EFFDATE"
        strParamValue = strLocation & "|" & strAccMonth & "|" & strAccYear & "|" & strUserId & "|" & _
                        strBank & "|" & _
                        strBankAccNo & "|" & _
                        ddlDocType.SelectedItem.Value & "|" & _
                        strEffDate

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                ObjTaxDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        lblErrMessageNo.Visible = True
        lblErrMessageNo.Text = Trim(ObjTaxDs.Tables(0).Rows(0).Item("Msg"))
        RefreshNoBtn_Click(Sender, E)

        'dgLineGen.DataSource = ObjTaxDs
        'dgLineGen.DataBind()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim lbl As Label
        Dim DocNo As String
        Dim ItemCode As String
        Dim strParam As String
        Dim StrTxParam As String
        Dim TxID As String
        Dim ErrorChk As Integer = 0
		Dim intErrNo As Integer
        Dim strParamName As String
        Dim strParamValue As String
		Dim ObjTaxDs As New Object
        Dim strOpcode As String = "HR_CLSSETUP_BANK_BILYETGIRO_UPDATE"

        lbl = E.Item.FindControl("lblDocNo")
        DocNo = lbl.Text

        strParamName = "STATUS|UPDATEID|DOCNO"
        strParamValue = "3" & "|" & strUserId & "|" & trim(DocNo)

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpcode, _
                                                strParamName, _
                                                strParamValue, _
                                                ObjTaxDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ID_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        BindGrid()
    End Sub
End Class
