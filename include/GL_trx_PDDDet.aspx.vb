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

Public Class GL_trx_PDDDet : Inherits Page


    Protected WithEvents issueType As Label
    Protected WithEvents lblStsHid As Label
    Protected WithEvents blnShortCut As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblPleaseSelect As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents lblTxLnID As Label
    Protected WithEvents dgDetail As DataGrid
    Protected WithEvents lblTxID As Label
    Protected WithEvents lblErrMessage As Label
	
	Protected WithEvents lblPDO  As Label 
    Protected WithEvents btnAddAllItem As ImageButton
	
	Protected WithEvents dgsaldo As DataGrid
	
    Protected WithEvents txtDesc As TextBox
    Protected WithEvents TxtFrom As TextBox
    Protected WithEvents TxtSubject As TextBox
    Protected WithEvents TxtAddNote As TextBox

    Protected WithEvents lstMonth As DropDownList
    Protected WithEvents lstYear As DropDownList
	Protected WithEvents lstPDO As DropDownList 
    Protected WithEvents ddlCodeGrpJob As DropDownList

    Protected WithEvents txtDate As TextBox
    Protected WithEvents lblDate As Label
    Protected WithEvents lblFmt As Label
    Protected WithEvents lblPDOStep As Label

    Protected WithEvents txtDPPAmount As TextBox

    Protected WithEvents Add As ImageButton
    Protected WithEvents Update As ImageButton
    Protected WithEvents lblTotalAmount As Label
    'Protected WithEvents btnPrint As ImageButton

    Protected WithEvents lblBPErr As Label
    Protected WithEvents lblLocCodeErr As Label
    Protected WithEvents NewBtn As ImageButton
    Protected WithEvents Save As ImageButton
    Protected WithEvents Delete As ImageButton
    Protected WithEvents Undelete As ImageButton
    Protected WithEvents Print As ImageButton
    Protected WithEvents Back As ImageButton


    Protected WithEvents lblAccPeriod As Label
    Protected WithEvents Status As Label
    Protected WithEvents CreateDate As Label
    Protected WithEvents UpdateDate As Label
    Protected WithEvents UpdateBy As Label
    Protected WithEvents lblPrintDate As Label
    Protected WithEvents lblDescErr As Label
	Protected WithEvents isNew As HtmlInputHidden

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
    Dim strLocation As String
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
	
	Dim cnt1 As double
	Dim cnt2 As double
	Dim cnt3 As double

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        strDateFMT = Session("SS_DATEFMT")
        intGLAR = Session("SS_GLAR")
        intConfigsetting = Session("SS_CONFIGSETTING")
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
            NewBtn.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(NewBtn).ToString())
            Save.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Save).ToString())
            Print.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Print).ToString())
            Back.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Back).ToString())
           
            If Not Page.IsPostBack Then
                txtDate.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Now)
                lblTxID.Text = Request.QueryString("Id")
                BindAccYear(strSelAccYear)
                BindAccMonth(strSelAccMonth)

				If lblTxID.Text.Trim() <> "" Then
                    isNew.Value = "False"
                    LoadPDDHeader()
					
                Else
                    isNew.Value = "True"
					
					lblPDO.visible = False
					lstPDO.visible = True
					btnAddAllItem.visible = True
					
                    BindPDO()
                End If
			else
			    lblErrMessage.visible=false
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

	Sub BindPDO()
		Dim strOpCd_TransDiv As String = "GL_CLSTRX_PDO_GSJ_GET_LIST"
        Dim intErrNo As Integer
        Dim objTransDs As New Object()
        Dim e As New System.EventArgs()
		Dim dr As DataRow

        strParamName = "SEARCH"
        strParamValue = "AND a.LocCode='" & strlocation & "' AND a.AccMonth='" & lstMonth.SelectedItem.Value & "' AND a.AccYear='" & lstYear.SelectedItem.Value & "'"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_TransDiv, strParamName, strParamValue, objTransDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSTRX_PDO_HEADER_SEARCH&errmesg=" & Exp.Message )
        End Try

		dr = objTransDs.Tables(0).NewRow()
        dr("PDOID") = ""
        dr("PDOID") = "Pilih No.PDO"
        objTransDs.Tables(0).Rows.InsertAt(dr, 0)
		
				
        lstPDO.DataSource = objTransDs.Tables(0)
        lstPDO.DataValueField = "PDOID"
        lstPDO.DataTextField = "PDOID"
        lstPDO.DataBind()
        
	End Sub
	
	Sub BtnGetPDOList_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        BindPDO()
		if isNew.Value = "True" Then
			Save.Visible = True
		End if
    End Sub
	
    Sub LoadPDDHeader()
        Dim strOpCd_TransDiv As String = "GL_CLSTRX_PDD_HEADER_SEARCH"
        Dim intErrNo As Integer
        Dim objTransDs As New Object()
        Dim e As New System.EventArgs()

        strParamName = "TRANSID"
        strParamValue = lblTxID.Text

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_TransDiv, strParamName, strParamValue, objTransDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSTRX_PDO_HEADER_SEARCH&errmesg=" & Exp.Message )
        End Try

        If objTransDs.Tables(0).Rows.Count <> 0 Then
            lblTxID.Text = objTransDs.Tables(0).Rows(0).Item("PDDID")
            CreateDate.Text = Format(objTransDs.Tables(0).Rows(0).Item("CreateDate"), "dd-MM-yyyy HH:mm:ss")
            UpdateDate.Text = Format(objTransDs.Tables(0).Rows(0).Item("UpdateDate"), "dd-MM-yyyy HH:mm:ss")
            UpdateBy.Text = objTransDs.Tables(0).Rows(0).Item("UpdateID")
            txtDate.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Trim(objTransDs.Tables(0).Rows(0).Item("PDDDate")))
            txtDesc.Text = RTrim(objTransDs.Tables(0).Rows(0).Item("Note1"))
            lblStsHid.Text = RTrim(objTransDs.Tables(0).Rows(0).Item("Status"))
            Status.Text = objGLtx.mtdGetJournalStatus(Trim(objTransDs.Tables(0).Rows(0).Item("Status")))
            lblPDO.visible = True
			lblPDO.Text = RTrim(objTransDs.Tables(0).Rows(0).Item("PDOID"))
			lstPDO.visible = False
			btnAddAllItem.visible = False
		  
            BindAccYear(Trim(objTransDs.Tables(0).Rows(0).Item("AccYear")))
            BindAccMonth(Trim(objTransDs.Tables(0).Rows(0).Item("AccMonth")))
			BindPDO()
			BindGrid(False, 0)
        End If
    End Sub

    Sub BindGrid(ByVal pEditRow As Boolean, ByVal pIndex As Integer)
		Dim dsDataDtl As DataSet
        Dim intRow As Integer

		dsDataDtl = LoadDataDtlGrid()
        dgDetail.DataSource = dsDataDtl.Tables(0)
        dgDetail.DataBind()
        dgDetail.Dispose()
        
		
        dgsaldo.DataSource = dsDataDtl.Tables(1)
        dgsaldo.DataBind()
        dgsaldo.Dispose()
    End Sub

   
	Protected Function LoadDataDtlGrid() As DataSet
        Dim objDs As New Object()
        Dim strOpCd_PDOGet As String = "GL_CLSTRX_PDD_GSJ_GET"
        Dim intErrNo As Integer
        Dim objTransDivDs As New Object()
		DIm intCnt As Integer

        strParamName = "LOC|BLN|THN|PDO"
        strParamValue = strlocation & "|" & lstmonth.SelectedItem.Value & "|" & lstYear.SelectedItem.Value & "|" & _ 
		                iif(isNew.Value = "True",lstPDO.SelectedItem.Value.Trim(),lblPDO.Text.Trim())

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_PDOGet, strParamName, strParamValue, objDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSTRX_PDO_DETAIL_SEARCH&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

		cnt1=0
		cnt2=0
		cnt3=0
		If objDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objDs.Tables(0).Rows.Count - 1
				 cnt1 = cnt1 + objDs.Tables(0).Rows(intCnt).Item("AmountPDO")
				 cnt2 = cnt2 + objDs.Tables(0).Rows(intCnt).Item("AmountPDD")
				 cnt3 = cnt3 + objDs.Tables(0).Rows(intCnt).Item("Selisih")
		     Next
        End If
		
        Return objDs
    End Function

    
    Sub BtnAddAllItem_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        BindGrid(False, 0)
    End Sub

    
    Sub NewBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("../../GL/Trx/GL_trx_PDDDet.aspx")
    End Sub

    Sub btnSave_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
       if Date_Validation(txtDate.text,false) <> "" then
		if month(Date_Validation(txtDate.text,false)) <> lstMonth.selecteditem.value+1 then
		    lblErrMessage.text = " Tgl PDD adalah tanggal awal bulan periode PDO + 1 " 
			lblErrMessage.visible = True
			exit sub
		end if
	   else
	        lblErrMessage.text = " Silakan isi Tgl PDD "
			lblErrMessage.visible = True
			exit sub
	   end if
       
	    strParamName = "PDDID|PDOID|PDDDT|LOC|AM|AY|UI|KET"
        strParamValue = lblTxID.Text.Trim() & "|" & _ 
		                lstPDO.SelectedItem.Value.Trim() & "|" & _ 
		                Date_Validation(txtDate.text,false) & "|" &  _ 
						strlocation & "|" & _ 
						lstmonth.SelectedItem.Value & "|" & _ 
						lstYear.SelectedItem.Value & "|" & _ 
						struserid & "|" & _ 
						replace(txtDesc.Text.Trim(),"'"," ")
						
		

        Try
            intErrNo = ObjOk.mtdInsertDataCommon("GL_CLSTRX_PDD_GSJ_UPD", strParamName, strParamValue)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSTRX_PDO_DETAIL_SEARCH&errmesg=" & Exp.Message )
        End Try
		BindGrid(False, 0)
		
    End Sub

    Sub btnPrint_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)

        Dim strTRXID As String

        strTRXID = Trim(lblPDO.Text.Trim())

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/GL_Rpt_PDDDet.aspx?Type=Print&CompName=" & strCompany & _
                       "&AccMonth=" & lstmonth.SelectedItem.Value & _
					   "&AccYear=" & lstYear.SelectedItem.Value  & _
					   "&Location=" & strLocation & _
                       "&TRXID=" & strTRXID & _
                       """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

    End Sub

    Sub btnDelete_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
       

    End Sub

    
    Sub btnBack_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("../../GL/Trx/GL_trx_PDDList.aspx")
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
   
   Sub KeepRunningSum(ByVal sender As Object, ByVal E As DataGridItemEventArgs)
		If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
			
            E.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='DarkSeaGreen'")

            If E.Item.ItemType = ListItemType.AlternatingItem Then
                E.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                E.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")
            End If
	
		End If
    
		If E.Item.ItemType = ListItemType.Footer Then
            E.Item.Cells(2).Text = objGlobal.GetIDDecimalSeparator_FreeDigit(cnt1, 0)
            E.Item.Cells(3).Text = objGlobal.GetIDDecimalSeparator_FreeDigit(cnt2, 0)
			E.Item.Cells(4).Text = objGlobal.GetIDDecimalSeparator_FreeDigit(cnt3, 0)
            E.Item.Font.Bold = True
        End If
    End Sub


End Class
