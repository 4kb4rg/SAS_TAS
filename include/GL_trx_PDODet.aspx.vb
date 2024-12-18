Imports System
Imports System.Collections
Imports System.Data
Imports System.Data.SqlClient
Imports System.Math
Imports System.Text
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic

Public Class GL_trx_PDODet : Inherits Page


    
    
	Protected WithEvents ddlCodeGrpJob As DropDownList

    ' Protected WithEvents txtDate As TextBox
    ' Protected WithEvents lblDate As Label
    ' Protected WithEvents lblFmt As Label
    ' Protected WithEvents lblPDOStep As Label

    ' Protected WithEvents txtDescLn As TextBox
    ' Protected WithEvents txtDPPAmount As TextBox

    'Protected WithEvents Add As ImageButton
    Protected WithEvents Update As ImageButton
    'Protected WithEvents lblTotalAmount As Label
    'Protected WithEvents btnPrint As ImageButton

    ' Protected WithEvents lblBPErr As Label
    ' Protected WithEvents lblLocCodeErr As Label
    ' Protected WithEvents NewBtn As ImageButton
    ' Protected WithEvents Save As ImageButton
    ' Protected WithEvents Delete As ImageButton
    ' Protected WithEvents Undelete As ImageButton
    ' Protected WithEvents Print As ImageButton
    ' Protected WithEvents Back As ImageButton


    ' Protected WithEvents lblAccPeriod As Label
    ' Protected WithEvents Status As Label
    ' Protected WithEvents CreateDate As Label
    ' Protected WithEvents UpdateDate As Label
    ' Protected WithEvents UpdateBy As Label
    ' Protected WithEvents lblPrintDate As Label
    ' Protected WithEvents lblDescErr As Label

    Protected objGLtx As New agri.GL.ClsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objINstp As New agri.IN.clsSetup()
    Dim objGLset As New agri.GL.clsSetup()
    Dim objHRTrx As New agri.HR.clsTrx()
    Dim objGLtrx As New agri.GL.ClsTrx()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objINTrx As New agri.IN.clsTrx()
    Dim objPUSetup As New agri.PU.clsSetup()
    Dim objPU As New agri.PU.clsTrx()
    Dim ObjOk As New agri.GL.ClsTrx()

    Dim objDataSet As New DataSet()
    Dim dsGrid As New DataSet()
    Dim objLangCapDs As New Object()
    Dim objSuppDs As New Object()

    Dim strCompany As String
	Dim strCompName As String
    Dim strLocation As String
	Dim strLocName As String
	
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intGLAR As Integer
    Dim strDateFMT As String
    Dim intConfigsetting As Integer

    Dim strAccountTag As String
    Dim strVehTag As String
    Dim strBlockTag As String
    Dim strVehExpTag As String

    Dim intErrNo As Integer
    Dim strParam As String = ""

    Dim dblTotDocAmt As Double

    Dim strOpCdPDOTxLine_ADD As String = "GL_CLSTRX_PDO_HEADER_ADD"

    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim strLocType As String
    Dim intLevel As Integer
    Dim strLocLevel As String

    Dim TaxLnID As String = ""
    Dim TaxRate As Double = 0
    Dim DPPAmount As Double = 0

    Dim strSelectedPOId As String = ""
    Dim strTxType As String
    Dim strRcvFrom As String

    Dim strParamName As String
    Dim strParamValue As String
    Dim objPODs As New Object()

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
		
		strCompName = Session("SS_COMPANYNAME")
		strLocName = Session("SS_LOCATIONNAME")
		
		
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_SELACCMONTH")
        strAccYear = Session("SS_SELACCYEAR")
        strDateFMT = Session("SS_DATEFMT")
        intGLAR = Session("SS_GLAR")
       
        strLocType = Session("SS_LOCTYPE")
        strSelAccMonth = Session("SS_SELACCMONTH")
        strSelAccYear = Session("SS_SELACCYEAR")
        intLevel = Session("SS_USRLEVEL")
        strLocLevel = Session("SS_LOCLEVEL")


        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLJournal), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            'to avoid double click, on aspx add this : UseSubmitBehavior="false"
            Add.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Add).ToString())
            NewBtn.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(NewBtn).ToString())
            Save.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Save).ToString())
            Delete.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Delete).ToString())

            Back.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Back).ToString())
            Undelete.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Undelete).ToString())

            If Not Page.IsPostBack Then
                Trace.Warn("postback")
                txtDate.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Now)
                lblTxID.Text = Request.QueryString("Id")

                BindAccYear(strAccYear)
                BindAccMonth(strAccMonth)
                BindStep("")
               
                BindkategoriPDO("")
				BindUOM("")
				If lblTxID.Text.Trim() <> "" Then
					LoadPDOHeader()
                    BindGrid(False, 0)
                    BindGrid_BankUse(False, 0)
                Else

                    TxtTo.Text = "KANTOR PUSAT PONTIANAK - " + strCompName
                    TxtFrom.Text = strLocName
                    TxtSubject.Text = "PERMINTAAN DANA OPERASIONAL"

                end if
				
				PageControl()
            End If
        End If
    End Sub

    Function lCDbl(ByVal pcVal As String) As Double
        If IsNumeric(pcVal) Then lCDbl = CDbl(pcVal) Else lCDbl = 0
    End Function

    Sub PageControl()
        txtDesc.Enabled = False
        lstMonth.Enabled = False
        lstYear.Enabled = False
        txtDate.Enabled = False
        txtDescLn.Enabled = False
        txtBalEndUse.ReadOnly = True
        txtBalEndUse.Font.Bold = True
        txtBalEndAct.ReadOnly = True
        txtPDDTrf.ReadOnly = True
        txtBalEnd.ReadOnly = True
        TxtTo.Enabled = True
        TxtFrom.Enabled = True
        TxtSubject.Enabled = True

        Add.Enabled = False
        Save.Visible = False

        Delete.Visible = False
        Undelete.Visible = False

        Select Case lblStsHid.Text
            Case CStr(objGLtx.EnumJournalStatus.Posted), _
                 CStr(objGLtx.EnumJournalStatus.Closed)


            Case CStr(objGLtx.EnumJournalStatus.Deleted)
                Undelete.Visible = True

            Case Else
                txtDesc.Enabled = True
                lstYear.Enabled = True
                lstMonth.Enabled = True
                txtDate.Enabled = True

                txtDescLn.Enabled = True

                Add.Enabled = True
                Save.Visible = True
                If lblTxID.Text.Trim() <> "" Then
                    Delete.Visible = True
                    txtDate.Enabled = False
                    lstStep.Enabled = False

                End If

        End Select

        lSum()
    End Sub

    Sub lSum()
        Dim vSaldoBankPrimer As Double
        Dim vNom As Double
        vNom = 0

        For intCnt = 0 To dgPDDBank.Items.Count - 1
            If lCDbl(CType(dgPDDBank.Items(intCnt).FindControl("lblTotalBankhiden"), Label).Text) > 0 Then
                vNom = vNom + lCDbl(CType(dgPDDBank.Items(intCnt).FindControl("lblTotalBankhiden"), Label).Text)

            End If
        Next

        txtBalEndUse.Text = 0
        txtBalEndUse.Text = vNom

        txtBalEndAct.Text = 0
        txtBalEndAct.Text = lCDbl(txtBalEnd.Text) - lCDbl(txtBalEndUse.Text)

        vSaldoBankPrimer = lCDbl(txtBalEndAct.Text) - 1000000

        If lCDbl(txtBalEndAct.Text) > 1000000 Then
            txtPDDTrf.Text = lCDbl(txtPDDManual.Text) - vSaldoBankPrimer
        Else
            txtPDDTrf.Text = lCDbl(txtPDDManual.Text)
        End If
    End Sub

    Sub DataGrid_ItemCreated(ByVal Sender As Object, ByVal e As DataGridItemEventArgs)

        If dgTx.Enabled = True Then
            Select Case e.Item.ItemType
                Case ListItemType.Item, ListItemType.AlternatingItem
                    Dim lbl As Label
                    Dim lblAmt As Label
                    lbl = e.Item.FindControl("lblIdx")
                    lbl.Text = e.Item.ItemIndex.ToString + 1

                    Dim DeleteButton As LinkButton
                    Dim EditButton As LinkButton
                    Select Case Status.Text.Trim
                        Case objGLtrx.mtdGetJournalStatus(objGLtrx.EnumJournalStatus.Active)
                            DeleteButton = e.Item.FindControl("Delete")
                            DeleteButton.Visible = True
                            DeleteButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                            EditButton = e.Item.FindControl("Edit")
                            EditButton.Visible = True
                        Case objGLtrx.mtdGetJournalStatus(objGLtrx.EnumJournalStatus.Deleted), _
                              objGLtrx.mtdGetJournalStatus(objGLtrx.EnumJournalStatus.Posted), _
                              objGLtrx.mtdGetJournalStatus(objGLtrx.EnumJournalStatus.Closed)
                            DeleteButton = e.Item.FindControl("Delete")
                            DeleteButton.Visible = False
                            EditButton = e.Item.FindControl("Edit")
                            EditButton.Visible = False
                    End Select

                    lblAmt = e.Item.FindControl("lblAmount")
                    lbl = e.Item.FindControl("lblAccTx")

                    If Sign(CDbl(Replace(Replace(lblAmt.Text.Trim, ".", ""), ",", "."))) = 1 Then
                        lbl.Text = "DR"
                    ElseIf Sign(CDbl(Replace(Replace(lblAmt.Text.Trim, ".", ""), ",", "."))) = -1 Then
                        lbl.Text = "CR"
                    End If
                Case ListItemType.EditItem
                    Dim lbl As Label
                    Dim lblAmt As Label
                    lbl = e.Item.FindControl("lblIdx")
                    lbl.Text = e.Item.ItemIndex.ToString + 1

                    lbl = e.Item.FindControl("lblQty")
					
                    lbl = e.Item.FindControl("lblAmt")
					

                   
            End Select

        Else
            Select Case e.Item.ItemType
                Case ListItemType.Item, ListItemType.AlternatingItem
                    Dim lbl As Label
                    lbl = e.Item.FindControl("lblIdx")
                    lbl.Text = e.Item.ItemIndex.ToString + 1

                    Dim DeleteButton As LinkButton
                    DeleteButton = e.Item.FindControl("Delete")
                    DeleteButton.Enabled = False
            End Select
        End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue'")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")
            End If
        End If

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
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
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

        lstYear.DataSource = objAccYearDs.Tables(0)
        lstYear.DataValueField = "AccYear"
        lstYear.DataTextField = "UserName"
        lstYear.DataBind()
        lstYear.SelectedIndex = intSelectedIndex - 1
    End Sub

    Sub BindAccMonth(ByVal pMonth As String)
        With lstMonth
            .Items.Clear()
            .Items.Add("1")
            .Items.Add("2")
            .Items.Add("3")
            .Items.Add("4")
            .Items.Add("5")
            .Items.Add("6")
            .Items.Add("7")
            .Items.Add("8")
            .Items.Add("9")
            .Items.Add("10")
            .Items.Add("11")
            .Items.Add("12")

            If Not Trim(pMonth) = "" Then
                .SelectedIndex = .Items.IndexOf(.Items.FindByValue(Trim(pMonth)))
            Else
                .SelectedIndex = -1
            End If
        End With
    End Sub

    Sub BindStep(ByVal pStep As String)
          lstStep.Items.Clear()
		  lstStep.Items.Add(New ListItem("Tahap 1 (Pinjaman)","1"))
		  lstStep.Items.Add(New ListItem("Tahap 2 (Gaji Besar)","2"))
		  
            If Not Trim(pStep) = "" Then
                lstStep.SelectedIndex = lstStep.Items.IndexOf(lstStep.Items.FindByValue(Trim(pStep)))
            Else
                lstStep.SelectedIndex = 1
            End If
       
    End Sub

   

    Sub Initialize()
        'lstAccCode.SelectedIndex = 0
        'lstBlock.SelectedIndex = 0
        'lstVehCode.SelectedIndex = 0
        'lstVehExp.SelectedIndex = 0
        'ddlChargeLevel.SelectedIndex = Session("SS_BLOCK_CHARGE_DEFAULT")
        'RowChargeLevel.Visible = Session("SS_BLOCK_CHARGE_VISIBLE")
        'ToggleChargeLevel()
    End Sub

    Sub LoadPDOHeader()
        Dim strOpCd_TransDiv As String = "GL_CLSTRX_PDO_HEADER_SEARCH"
        Dim intErrNo As Integer
        Dim objTransDs As New Object()
        Dim e As New System.EventArgs()

        strParamName = "TRANSID"
        strParamValue = lblTxID.Text

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_TransDiv, strParamName, strParamValue, objTransDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSTRX_PDO_HEADER_SEARCH&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objTransDs.Tables(0).Rows.Count <> 0 Then
            lblTxID.Text = objTransDs.Tables(0).Rows(0).Item("TransactionID")
            lblAccPeriod.Text = objTransDs.Tables(0).Rows(0).Item("AccMonth") & "/" & objTransDs.Tables(0).Rows(0).Item("AccYear")
            CreateDate.Text = Format(objTransDs.Tables(0).Rows(0).Item("CreateDate"), "dd-MM-yyyy HH:mm:ss")
            UpdateDate.Text = Format(objTransDs.Tables(0).Rows(0).Item("UpdateDate"), "dd-MM-yyyy HH:mm:ss")
            UpdateBy.Text = objTransDs.Tables(0).Rows(0).Item("UpdateID")
            txtDate.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Trim(objTransDs.Tables(0).Rows(0).Item("PDODate")))
            txtDesc.Text = RTrim(objTransDs.Tables(0).Rows(0).Item("Description"))
            lblStsHid.Text = RTrim(objTransDs.Tables(0).Rows(0).Item("Status"))
            Status.Text = objGLtx.mtdGetJournalStatus(Trim(objTransDs.Tables(0).Rows(0).Item("Status")))
            TxtTo.Text = RTrim(objTransDs.Tables(0).Rows(0).Item("ToCmpy"))
            TxtFrom.Text = RTrim(objTransDs.Tables(0).Rows(0).Item("FromCmpy"))
            TxtSubject.Text = RTrim(objTransDs.Tables(0).Rows(0).Item("Subject"))
            txtBalEnd.Text = RTrim(objTransDs.Tables(0).Rows(0).Item("BalEnd"))
            txtBalEndAct.Text = RTrim(objTransDs.Tables(0).Rows(0).Item("BalEndAct"))
            txtPDDManual.Text = RTrim(objTransDs.Tables(0).Rows(0).Item("PDDManualAmount"))
            txtPDDTrf.Text = RTrim(objTransDs.Tables(0).Rows(0).Item("PDDTrfAmount"))
            txtDesc.Text = RTrim(objTransDs.Tables(0).Rows(0).Item("Description"))
            BindStep(Trim(objTransDs.Tables(0).Rows(0).Item("Step")))
            BindAccYear(Trim(objTransDs.Tables(0).Rows(0).Item("AccYear")))
            BindAccMonth(Trim(objTransDs.Tables(0).Rows(0).Item("AccMonth")))
        End If
    End Sub

    Sub BindGrid(ByVal pEditRow As Boolean, ByVal pIndex As Integer)
        Dim dsData As DataSet

        dsData = LoadDataGrid()

        dgTx.DataSource = dsData
        dgTx.DataBind()
        dgTx.Dispose()
    End Sub

    Sub BindGrid_BankUse(ByVal pEditRow As Boolean, ByVal pIndex As Integer)
        Dim dsData As DataSet

        dsData = LoadDataGrid_BankUse()

        dgPDDBank.DataSource = dsData
        dgPDDBank.DataBind()
        dgPDDBank.Dispose()


    End Sub

    Sub lClearDetail()
        With Me
		    .lstgrppdo.selectedValue = ""
		    .txtQty.Text = ""
			.ddluom.selectedValue = ""
            .txtDPPAmount.Text = ""
            .txtDescLn.Text = ""
            .TxtAddNote.Text = ""
            .lblPDOGroup.Text = ""
            .lblTyEmp.Text = ""
        End With
    End Sub
	
	Function LoadkategoriPDO() as DataSet
		Dim strOpCd_DivId As String = "GL_STP_PDO_LIST"
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objJobGroup As New Object

        Dim ParamNama As String
        Dim ParamValue As String

        ParamNama = "STRSEARCH"
        ParamValue = ""
        
		Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_DivId, ParamNama, ParamValue, objJobGroup)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_STP_PDO_HEADER_LIST&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objJobGroup.Tables(0).Rows.Count - 1
            objJobGroup.Tables(0).Rows(intCnt).Item("PDOCode") = Trim(objJobGroup.Tables(0).Rows(intCnt).Item("PDOCode"))
            objJobGroup.Tables(0).Rows(intCnt).Item("Desr") = Trim(objJobGroup.Tables(0).Rows(intCnt).Item("Desr"))

        Next

        dr = objJobGroup.Tables(0).NewRow()
        dr("PDOCode") = ""
        dr("Desr") = "Select Biaya"
        objJobGroup.Tables(0).Rows.InsertAt(dr, 0)
		
		Return objJobGroup
	
	End Function
	
	Sub BindkategoriPDO(ByVal ptipe As String)
	    Dim objJobGroup As DataSet
		objJobGroup = LoadkategoriPDO()
		
		lstgrppdo.DataSource = objJobGroup.Tables(0)
        lstgrppdo.DataValueField = "PDOCode"
        lstgrppdo.DataTextField = "Desr"
        lstgrppdo.DataBind()
        lstgrppdo.SelectedValue = trim(pTipe)
	End Sub

    Sub onLoad_BindkategoriPDO(ByVal pTipe As String)
        Dim objJobGroup As DataSet
		objJobGroup = LoadkategoriPDO()       
	    ddlCodeGrpJob.DataSource = objJobGroup.Tables(0)
        ddlCodeGrpJob.DataValueField = "PdoGrpID"
        ddlCodeGrpJob.DataTextField = "Description"
        ddlCodeGrpJob.DataBind()
        ddlCodeGrpJob.SelectedValue = trim(pTipe)

    End Sub
	
	Sub BindUOM(ByVal ptipe As String)
		Dim strOpCd_DivId As String = "ADMIN_CLSUOM_UOM_LIST_GET"
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer
        Dim objJobGroup As New Object

        Dim ParamNama As String
        Dim ParamValue As String

        ParamNama = "SEARCHSTR|SORTEXP"
        ParamValue = "|"
        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_DivId, ParamNama, ParamValue, objJobGroup)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_STP_PDO_HEADER_LIST&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objJobGroup.Tables(0).Rows.Count - 1
            objJobGroup.Tables(0).Rows(intCnt).Item("UOMCode") = Trim(objJobGroup.Tables(0).Rows(intCnt).Item("UOMCode"))
            objJobGroup.Tables(0).Rows(intCnt).Item("UOMDesc") = Trim(objJobGroup.Tables(0).Rows(intCnt).Item("UOMDesc"))
               if  objJobGroup.Tables(0).Rows(intCnt).Item("UOMCode")=Trim(pTipe) Then 
			      intSelectedIndex = intCnt + 1
			   end if
        Next

        dr = objJobGroup.Tables(0).NewRow()
        dr("UOMCode") = ""
        dr("UOMDesc") = "Select Satuan"
        objJobGroup.Tables(0).Rows.InsertAt(dr, 0)
		
		ddluom.DataSource = objJobGroup.Tables(0)
        ddluom.DataValueField = "UOMCode"
        ddluom.DataTextField = "UOMDesc"
        ddluom.DataBind()
        ddluom.SelectedIndex = intSelectedIndex

		
	End Sub

    Function GetAutoNum(ByVal pBulan As String, ByVal pTahun As String, ByVal pLocCode As String) As String
        Dim strOpCd_AutoNum As String = "GL_CLSTRX_PDO_DETAIL_AUTONUM"
        Dim intErrNo As Integer
        Dim objNumDs As New Object()
        Dim nNOUrut As Long = 0
        Dim Newno As Long = 0
        Dim NewNoStr As String


        strParamName = "BULAN|TAHUN|LOC"
        strParamValue = pBulan & "|" & pTahun & "|" & pLocCode

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_AutoNum, strParamName, strParamValue, objNumDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSTRX_PDO_DETAIL_AUTONUM &errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objNumDs.Tables(0).Rows.Count = 1 Then
            nNOUrut = objNumDs.Tables(0).Rows(0).Item("TransactionID")
        Else
            nNOUrut = 0
        End If

        Newno = nNOUrut + 1
        NewNoStr = Strings.Right("0000" & CStr(Newno), 4)
        Return NewNoStr
    End Function

    Protected Function LoadDataGrid() As DataSet
        Dim objDs As New Object()
        Dim strOpCd_PDOGet As String = "GL_CLSTRX_PDO_DETAIL_SEARCH"
        Dim intErrNo As Integer
        Dim objTransDivDs As New Object()

        strParamName = "TRANSID"
        strParamValue = lblTxID.Text.Trim

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_PDOGet, strParamName, strParamValue, objDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSTRX_PDO_DETAIL_SEARCH&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        Return objDs
    End Function

    Protected Function LoadDataGrid_BankUse() As DataSet
        Dim objDs As New Object()
        Dim strOpCd_PDOGet As String = "GL_CLSTRX_PDO_DETAIL_BANK_SEARCH"
        Dim intErrNo As Integer

        strParamName = "TRANSID"
        strParamValue = lblTxID.Text.Trim

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_PDOGet, strParamName, strParamValue, objDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSTRX_PDO_DETAIL_SEARCH&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        Return objDs
    End Function

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim btn As LinkButton
        Dim lbl As Label
        Dim Delbutton As LinkButton
        Dim strAccCode As String

        Dim strDBCR As String = ""
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim indDate As String = ""

        blnUpdate.Text = True
        dgTx.EditItemIndex = CInt(E.Item.ItemIndex)

        lbl = E.Item.FindControl("lblLnID")
        lblTxLnID.Text = lbl.Text.Trim

	    lbl = E.Item.FindControl("lblPDOID")
        BindkategoriPDO(lbl.Text.Trim())

        lbl = E.Item.FindControl("lblGroupIDGrd")
        lblPDOGroup.Text = lbl.Text.Trim

        lbl = E.Item.FindControl("lblTypeEmpGrd")
        lblTyEmp.Text = lbl.Text.Trim

        lbl = E.Item.FindControl("lblDesc")
        txtDescLn.Text = lbl.Text.Trim
		
		lbl = E.Item.FindControl("lblQty")
		txtQty.Text =  Replace(Replace(Replace(lbl.Text.Trim, ".", ""), ",", "."), "-", "")
		
		lbl = E.Item.FindControl("lbluom")
	    BindUOM(lbl.text)
		
        lbl = E.Item.FindControl("lblAmount")
        txtDPPAmount.Text = Replace(Replace(Replace(lbl.Text.Trim, ".", ""), ",", "."), "-", "")

        'lbl = E.Item.FindControl("lblAddNote")
        'TxtAddNote.Text = Replace(Replace(Replace(lbl.Text.Trim, ".", ""), ",", "."), "-", "")
        
		
		
		'Delbutton = E.Item.FindControl("Delete")
        'Delbutton.Visible = False

        'If lblTxLnID.Text <> "" Then
        '    Add.Visible = False
        '    Update.Visible = True
        'Else
        '    Add.Visible = True
        '    Update.Visible = False
        'End If

        'dgTx.DataSource = Nothing
        'dgTx.DataBind()
        dgTx.EditItemIndex = -1
        BindGrid(False, 0)
    End Sub

    Sub DEDR_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        lblTxLnID.Text = ""
        txtDescLn.Text = ""
        Initialize()

        dgTx.EditItemIndex = -1
        BindGrid(False, 0)

        Add.Visible = True
        Update.Visible = False

    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim StrOpCode_Del As String = "GL_CLSTRX_PDO_DETAIL_DEL"

        Dim lbl As Label

        Dim strDBCR As String = ""
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim indDate As String = ""

        Dim ParamNama As String = ""
        Dim ParamValue As String = ""

        If txtDate.Text = "" Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If
        If CheckDate(txtDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(lstYear.SelectedItem.Value) * 100) + CInt(lstMonth.SelectedItem.Value)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        
        If intInputPeriod < intCurPeriod  Then
                lblDate.Visible = True
                lblDate.Text = "Invalid Transaction Date"
                Exit Sub
        End If

        lbl = E.Item.FindControl("lblLnID")
        lblTxLnID.Text = lbl.Text.Trim

        ParamNama = "TRANSID|TRANSLNID"
        ParamValue = lblTxID.Text & "|" & _
                     lblTxLnID.Text

        Try
            intErrNo = ObjOk.mtdInsertDataCommon(StrOpCode_Del, ParamNama, ParamValue)
        Catch ex As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDHOURLINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_Stocktransfer_List.aspx")
        End Try

        BindGrid(False, 0)
        PageControl()

    End Sub

    Sub DEDR_DeleteBank(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim StrOpCode_Del As String = "GL_CLSTRX_PDO_DETAIL_BANK_DEL"

        Dim lbl As Label

        Dim strDBCR As String = ""
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim indDate As String = ""

        Dim ParamNama As String = ""
        Dim ParamValue As String = ""

        If txtDate.Text = "" Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If
        If CheckDate(txtDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(lstYear.SelectedItem.Value) * 100) + CInt(lstMonth.SelectedItem.Value)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)


        If intInputPeriod < intCurPeriod Then
            lblDate.Visible = True
            lblDate.Text = "Invalid Transaction Date"
            Exit Sub
        End If

        lbl = E.Item.FindControl("lblLnIDBank")
        lblTxLnID.Text = lbl.Text.Trim

        ParamNama = "TRANSID|LNID"
        ParamValue = lblTxID.Text & "|" & _
                     lblTxLnID.Text.Trim

        Try
            intErrNo = ObjOk.mtdInsertDataCommon(StrOpCode_Del, ParamNama, ParamValue)
        Catch ex As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDHOURLINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_Stocktransfer_List.aspx")
        End Try

        BindGrid_BankUse(False, 0)
        PageControl()

    End Sub

    Sub BtnAddAllItem_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strOpCdPDOGetPinjaman_ADD As String 
        Dim objStckDiv As New Object()

        Dim ParamNama As String
        Dim ParamValue As String

        Dim strNewIDFormat As String
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim indDate As String = ""
        Dim StrNoTransF As String = vbNullString

        If CheckDate(txtDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(lstYear.SelectedItem.Value) * 100) + CInt(lstMonth.SelectedItem.Value)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        
        If intInputPeriod < intCurPeriod  Then
                lblDate.Visible = True
                lblDate.Text = "Invalid Transaction Date"
                Exit Sub
        End If
        
        If lstStep.SelectedItem.Value = "" Then
            lblPDOStep.Visible = True
            Exit Sub
        End If
 


        If Len(lblTxID.Text) = 0 Then
            StrNoTransF = GetAutoNum(lstMonth.SelectedItem.Value, lstYear.SelectedItem.Value, strLocation)
            strNewIDFormat = "PDD" & "/" & strCompany & "/" & strLocation & "/" & lstYear.SelectedItem.Value & IIf(Len(Trim(lstMonth.SelectedItem.Value)) = 1, "0" & lstMonth.SelectedItem.Value, lstMonth.SelectedItem.Value) & "/" & StrNoTransF
            ParamNama = "TRANSID|LOCCODE|DOCDATE|DESC|TOF|FR|SUBJECT|STEP|ACCMONTH|ACCYEAR|TOTAL|STATUS|CRDATE|UPDATE|UPDATEID|PDDAMOUNTMNL|BALEND|BALENDACT|PDDTRF"
            ParamValue = strNewIDFormat & "|" & _
                strLocation & "|" & _
                strDate & "|" & _
                txtDesc.Text & "|" & _
                TxtTo.Text & "|" & _
                TxtFrom.Text & "|" & _
                TxtSubject.Text & "|" & _
                lstStep.SelectedItem.Value & "|" & _
                lstMonth.SelectedItem.Value & "|" & _
                lstYear.SelectedItem.Value & "|" & _
                LCDBL(lblTotalAmount.Text) & "|" & _
                "1" & "|" & _
                Date.Now() & "|" & _
                Date.Now() & "|" & _
                strUserId & "|" & LCDBL(txtPDDManual.Text) & "|" & LCDBL(txtBalEnd.Text) & "|" & LCDBL(txtBalEndAct.Text) & "|" & LCDBL(txtPDDTrf.Text)
            Try
                intErrNo = ObjOk.mtdInsertDataCommon(strOpCdPDOTxLine_ADD, ParamNama, ParamValue)
            Catch ex As Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDHOURLINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_Stocktransfer_List.aspx")
            End Try

            lblTxID.Text = strNewIDFormat
        End If

        If Len(lblTxID.Text) > 0 Then
		    '-- UPAH
		    strOpCdPDOGetPinjaman_ADD  = "GL_CLSTRX_PDO_DETAIL_UPAH_GEN"
            ParamNama = "LOC|BLN|THN|TRID|UID|TAHAP"
            ParamValue = strLocation & "|" & _
                     lstMonth.SelectedItem.Value & "|" & _
                     lstYear.SelectedItem.Value & "|" & _
                     lblTxID.Text & "|" & _
                     strUserId & "|" & _
					 lstStep.SelectedItem.Value.Trim()
            Try
                intErrNo = ObjOk.mtdInsertDataCommon(strOpCdPDOGetPinjaman_ADD, ParamNama, ParamValue)
            Catch ex As Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDHOURLINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_Stocktransfer_List.aspx")
            End Try
			
			'-- SPK
            'strOpCdPDOGetPinjaman_ADD  = "GL_CLSTRX_PDO_DETAIL_SPK_GEN"
            '         ParamNama = "LOC|BLN|THN|TRID|UID"
            '         ParamValue = strLocation & "|" & _
            '                  lstMonth.SelectedItem.Value & "|" & _
            '                  lstYear.SelectedItem.Value & "|" & _
            '                  lblTxID.Text & "|" & _
            '                  strUserId 
            '         Try
            '             intErrNo = ObjOk.mtdInsertDataCommon(strOpCdPDOGetPinjaman_ADD, ParamNama, ParamValue)
            '         Catch ex As Exception
            '             Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDHOURLINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_Stocktransfer_List.aspx")
            '         End Try
			
			'-- INV
			strOpCdPDOGetPinjaman_ADD  = "GL_CLSTRX_PDO_DETAIL_INV_GEN"
            ParamNama = "LOC|BLN|THN|TRID|UID"
            ParamValue = strLocation & "|" & _
                     lstMonth.SelectedItem.Value & "|" & _
                     lstYear.SelectedItem.Value & "|" & _
                     lblTxID.Text & "|" & _
                     strUserId 
            Try
                intErrNo = ObjOk.mtdInsertDataCommon(strOpCdPDOGetPinjaman_ADD, ParamNama, ParamValue)
            Catch ex As Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDHOURLINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_Stocktransfer_List.aspx")
            End Try
            '-- BANK
        End If

        lClearDetail()
        LoadPDOHeader()
        BindGrid(False, 0)
        PageControl()
    End Sub

    Sub btnAdd_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strOpCdPDOTxLineDet_ADD As String = "GL_CLSTRX_PDO_DETAIL_ADD"
        Dim objStckDiv As New Object()

        Dim ParamNama As String
        Dim ParamValue As String

        Dim strNewIDFormat As String
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim indDate As String = ""
        Dim StrNoTransF As String = vbNullString

        If CheckDate(txtDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(lstYear.SelectedItem.Value) * 100) + CInt(lstMonth.SelectedItem.Value)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        
        If intInputPeriod < intCurPeriod  Then
                lblDate.Visible = True
                lblDate.Text = "Invalid Transaction Date"
                Exit Sub
        End If
        
        If lstStep.SelectedItem.Value = "" Then
            lblPDOStep.Visible = True
            Exit Sub
        End If

 
        strAccYear = Year(strDate)
        strAccMonth = Month(strDate)

        If Len(lblTxID.Text) = 0 Then
            StrNoTransF = GetAutoNum(lstMonth.SelectedItem.Value, lstYear.SelectedItem.Value, strLocation)
            strNewIDFormat = "PDD" & "/" & strCompany & "/" & strLocation & "/" & lstYear.SelectedItem.Value & IIf(Len(Trim(lstMonth.SelectedItem.Value)) = 1, "0" & lstMonth.SelectedItem.Value, lstMonth.SelectedItem.Value) & "/" & StrNoTransF
            ParamNama = "TRANSID|LOCCODE|DOCDATE|DESC|TOF|FR|SUBJECT|STEP|ACCMONTH|ACCYEAR|TOTAL|STATUS|CRDATE|UPDATE|UPDATEID"
            ParamValue = strNewIDFormat & "|" & _
                         strLocation & "|" & _
                         strDate & "|" & _
                         txtDesc.Text & "|" & _
                         TxtTo.Text & "|" & _
                         TxtFrom.Text & "|" & _
                         TxtSubject.Text & "|" & _
                         lstStep.SelectedItem.Value & "|" & _
                         lstMonth.SelectedItem.Value & "|" & _
                         lstYear.SelectedItem.Value & "|" & _
                         LCDBL(lblTotalAmount.Text) & "|" & _
                         "1" & "|" & _
                         Date.Now() & "|" & _
                         Date.Now() & "|" & _
                         strUserId
            Try
                intErrNo = ObjOk.mtdInsertDataCommon(strOpCdPDOTxLine_ADD, ParamNama, ParamValue)
            Catch ex As Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDHOURLINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_Stocktransfer_List.aspx")
            End Try

            lblTxID.Text = strNewIDFormat
        End If

        ParamNama = "TRANSID|PDOLNID|DESC|QTY|UOM|TIPE|TOTAL|ADDNOTE|CRDATE|UDATE|UDATEID|TRANSLNID"
        ParamValue = lblTxID.Text & "|" & _
                     lblPDOGroup.Text & "|" & _
                     txtDescLn.Text & "|" & _
                     txtQty.Text & "|" & _
                     ddluom.SelectedItem.Value.Trim() & "|" & _
                     lstgrppdo.SelectedItem.Value.Trim() & "|" & _
                     LCDBL(txtDPPAmount.Text) & "|" & _
                     TxtAddNote.Text & "|" & _
                     Date.Now() & "|" & _
                     Date.Now() & "|" & _
                     strUserId & "|" & _
                     lblTxLnID.Text
        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOpCdPDOTxLineDet_ADD, ParamNama, ParamValue)
        Catch ex As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDHOURLINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_Stocktransfer_List.aspx")
        End Try

        lClearDetail()
        LoadPDOHeader()
        BindGrid(False, 0)
        PageControl()
    End Sub

    Sub btnAddBank_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strOpCdPDOTxLineDet_ADD As String = "GL_CLSTRX_PDO_DETAIL_BANK_ADD"
        Dim objStckDiv As New Object()

        Dim ParamNama As String
        Dim ParamValue As String

        Dim strNewIDFormat As String
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim indDate As String = ""
        Dim StrNoTransF As String = vbNullString

        If CheckDate(txtDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(lstYear.SelectedItem.Value) * 100) + CInt(lstMonth.SelectedItem.Value)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        If intInputPeriod < intCurPeriod Then
            lblDate.Visible = True
            lblDate.Text = "Invalid Transaction Date"
            Exit Sub
        End If

        If lstStep.SelectedItem.Value = "" Then
            lblPDOStep.Visible = True
            Exit Sub
        End If


        strAccYear = Year(strDate)
        strAccMonth = Month(strDate)

        If Len(lblTxID.Text) > 0 Then   
            ParamNama = "TRANSID|TOTAL|ADDNOTE|UDATEID"
            ParamValue = lblTxID.Text.Trim & "|" & _
                          LCDBL(txtaddBankUse.Text) & "|" & _
                         txtBankAddNote.Text & "|" & _
                         strUserId

            Try
                intErrNo = ObjOk.mtdInsertDataCommon(strOpCdPDOTxLineDet_ADD, ParamNama, ParamValue)
            Catch ex As Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDHOURLINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_Stocktransfer_List.aspx")
            End Try
             
        End If

     
        BindGrid_BankUse(False, 0)

        PageControl()
    End Sub
    Sub NewBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("../../GL/Trx/GL_trx_PDODet.aspx")
    End Sub

    Sub btnSave_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strOpCdPDOTxLine_UPD As String = "GL_CLSTRX_PDO_DETAIL_UPD"
        Dim nColDescriptioN As Byte = 2

        Dim objStckDiv As New Object()
        Dim TxID As String
        Dim nTxIDLN As String = ""
        Dim ni As Integer
        Dim nTipe As String = ""


        Dim ParamNama As String
        Dim ParamValue As String

        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim indDate As String = ""
        Dim StrNoTransF As String = vbNullString

        If CheckDate(txtDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(lstYear.SelectedItem.Value) * 100) + CInt(lstMonth.SelectedItem.Value)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        
        If intInputPeriod < intCurPeriod  Then
                lblDate.Visible = True
                lblDate.Text = "Invalid Transaction Date"
                Exit Sub
        End If
        
        If lstStep.SelectedItem.Value = "" Then
            lblPDOStep.Visible = True
            Exit Sub
        End If


        strAccYear = Year(strDate)
        strAccMonth = Month(strDate)

        If Len(lblTxID.Text) > 0 Then
            ParamNama = "TRANSID|LOCCODE|DOCDATE|DESC|TOF|FR|SUBJECT|STEP|ACCMONTH|ACCYEAR|TOTAL|STATUS|CRDATE|UPDATE|UPDATEID|PDDAMOUNTMNL|BALEND|BALENDACT|PDDTRF"
            ParamValue = lblTxID.Text & "|" & _
                         strLocation & "|" & _
                         strDate & "|" & _
                         txtDesc.Text & "|" & _
                         TxtTo.Text & "|" & _
                         TxtFrom.Text & "|" & _
                         TxtSubject.Text & "|" & _
                         lstStep.SelectedItem.Value & "|" & _
                         lstMonth.SelectedItem.Value & "|" & _
                         lstYear.SelectedItem.Value & "|" & _
                         LCDBL(lblTotalAmount.Text) & "|" & _
                         "1" & "|" & _
                         Date.Now() & "|" & _
                         Date.Now() & "|" & _
                         strUserId & "|" & LCDBL(txtPDDManual.Text) & "|" & LCDBL(txtBalEnd.Text) & "|" & LCDBL(txtBalEndAct.Text) & "|" & LCDBL(txtPDDTrf.Text)
            Try
                intErrNo = ObjOk.mtdInsertDataCommon(strOpCdPDOTxLine_ADD, ParamNama, ParamValue)
            Catch ex As Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDHOURLINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_Stocktransfer_List.aspx")
            End Try

            
        End If

        lClearDetail()
        LoadPDOHeader()
        BindGrid(False, 0)
        PageControl()

    End Sub

    'Sub btnPrint_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)

    '    Dim strTRXID As String

    '    strTRXID = Trim(lblTxID.Text)

    '    Response.Write("<Script Language=""JavaScript"">window.open(""../reports/GL_Rpt_PDODet.aspx?Type=Print&CompName=" & strCompany & _
    '                   "&Location=" & strLocation & _
    '                   "&TRXID=" & strTRXID & _
    '                   """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

    'End Sub

    Sub btnPrintPDO_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim strTRXID As String
        Dim Rpttype As String
        strTRXID = Trim(lblTxID.Text)
        Rpttype = "PDO"
        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/GL_Rpt_PDODet.aspx?Type=Print&CompName=" & strCompany & _
                       "&Location=" & strLocation & _
                       "&TRXID=" & strTRXID & _
                       "&RPTTYPE=" & Rpttype & _
                       """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

    End Sub

    Sub btnPrintLPJ_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim strTRXID As String
        Dim Rpttype As String

        strTRXID = Trim(lblTxID.Text)
        Rpttype = "LPJ"
        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/GL_Rpt_PDODet.aspx?Type=Print&CompName=" & strCompany & _
                       "&Location=" & strLocation & _
                       "&TRXID=" & strTRXID & _
                       "&RPTTYPE=" & Rpttype & _
                       """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

    End Sub

    Sub btnDelete_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim StrOpCodeHead_Del As String = "GL_CLSTRX_PDO_HEADER_DEL"

        Dim strDBCR As String = ""
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim indDate As String = ""

        Dim ParamNama As String = ""
        Dim ParamValue As String = ""

        If txtDate.Text = "" Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If
        If CheckDate(txtDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(lstYear.SelectedItem.Value) * 100) + CInt(lstMonth.SelectedItem.Value)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        
        If intInputPeriod < intCurPeriod  Then
                lblDate.Visible = True
                lblDate.Text = "Invalid Transaction Date"
                Exit Sub
        End If

        ParamNama = "TRANSID|STATUS"
        ParamValue = lblTxID.Text & "|" & 2

        Try
            intErrNo = ObjOk.mtdInsertDataCommon(StrOpCodeHead_Del, ParamNama, ParamValue)
        Catch ex As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDHOURLINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_Stocktransfer_List.aspx")
        End Try
		
        lClearDetail()
        LoadPDOHeader()
        BindGrid(False, 0)
        PageControl()

    End Sub

    Sub btnUnDelete_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim StrOpCodeHead_Del As String = "GL_CLSTRX_PDO_HEADER_DEL"

        Dim strDBCR As String = ""
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim indDate As String = ""

        Dim ParamNama As String = ""
        Dim ParamValue As String = ""

        If txtDate.Text = "" Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If
        If CheckDate(txtDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        
        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(lstYear.SelectedItem.Value) * 100) + CInt(lstMonth.SelectedItem.Value)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        
        If intInputPeriod < intCurPeriod  Then
                lblDate.Visible = True
                lblDate.Text = "Invalid Transaction Date"
                Exit Sub
        End If

        ParamNama = "TRANSID|STATUS"
        ParamValue = lblTxID.Text & "|" & 1

        Try
            intErrNo = ObjOk.mtdInsertDataCommon(StrOpCodeHead_Del, ParamNama, ParamValue)
        Catch ex As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDHOURLINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_Stocktransfer_List.aspx")
        End Try
        LoadPDOHeader()
        BindGrid(False, 0)
        PageControl()
    End Sub

    Sub btnBack_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("../../GL/Trx/GL_trx_PDO_List.aspx")
    End Sub

    Function CheckDate(ByVal pv_strDate As String, ByRef pr_strDate As String) As Boolean
        Dim strDateFormatCode As String = Session("SS_DATEFMT")
        Dim strDateFormat As String

        pr_strDate = ""
        CheckDate = True
        If Not pv_strDate = "" Then
            If objGlobal.mtdValidInputDate(strDateFormatCode, pv_strDate, strDateFormat, pr_strDate) = False Then
                lblFmt.Text = strDateFormat
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

    Sub lstgrppdo_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If lstgrppdo.SelectedItem.Value <> "" Then
            txtDescLn.Text = Left(Trim(lstgrppdo.SelectedItem.Text), InStr(Trim(lstgrppdo.SelectedItem.Text), "(") - 1)
        End If

        Dim strOpCd_DivId As String = "GL_STP_PDO_LIST"
        Dim intErrNo As Integer
        Dim objJobGroup As New Object

        Dim ParamNama As String
        Dim ParamValue As String

        ParamNama = "STRSEARCH"
        ParamValue = "WHERE PDOCODE='" & lstgrppdo.SelectedItem.Value & "'"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_DivId, ParamNama, ParamValue, objJobGroup)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_STP_PDO_HEADER_LIST&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        lblPDOGroup.Text = ""
        lblTyEmp.Text = ""

        If objJobGroup.Tables(0).Rows.Count <> 0 Then
            lblPDOGroup.Text = Trim(objJobGroup.Tables(0).Rows(0).Item("PDOGRPCODE"))
            lblTyEmp.Text = Trim(objJobGroup.Tables(0).Rows(0).Item("TyEmp"))
        End If
    End Sub
	 

    Sub txtPDDManual_TextChanged(sender As Object, e As System.EventArgs) Handles txtPDDManual.TextChanged
        lSum()
    End Sub
End Class
