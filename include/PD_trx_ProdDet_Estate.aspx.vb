Imports System
Imports System.Data
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Text.RegularExpressions
Imports Microsoft.VisualBasic.Information
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction

Public Class PD_trx_ProdDet_Estate : Inherits Page

#Region "declaration"

    Protected WithEvents tblSelection As HtmlTable
    Protected WithEvents dgOvtDet As DataGrid

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblPeriod As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label

    Protected WithEvents lblSPB As Label
	Protected WithEvents txtNoRef  As TextBox
    Protected WithEvents txtWPDate As TextBox
	
	Protected WithEvents txtWPProd As TextBox
	
	
    Protected WithEvents ddldivisi As DropDownList
    Protected WithEvents ddlMill As DropDownList
    Protected WithEvents txtnopolisi As TextBox
    Protected WithEvents txtsupir As TextBox
   
    Protected WithEvents TxtBruto2 As TextBox
    Protected WithEvents TxtTarra2 As TextBox
    Protected WithEvents TxtNetto2 As TextBox
    Protected WithEvents TxtPotKg As TextBox
    Protected WithEvents TxtNetto As TextBox
    Protected WithEvents TxtJJG As TextBox
	Protected WithEvents TxtBJR As TextBox
	Protected WithEvents TxtPotBM As TextBox
	Protected WithEvents TxtPotBB As TextBox
	Protected WithEvents TxtPotSP As TextBox
	Protected WithEvents TxtPotAR As TextBox
	Protected WithEvents TxtPotPL As TextBox
	Protected WithEvents TxtPot As TextBox
	Protected WithEvents TxtBruto1 As TextBox
	Protected WithEvents TxtTarra1 As TextBox
	Protected WithEvents TxtNetto1 As TextBox
	
	Protected WithEvents TxtBKecil  As TextBox
	Protected WithEvents TxtBBesar   As TextBox
   
   	Protected WithEvents TxtMasuk  As TextBox
	Protected WithEvents TxtKeluar   As TextBox
	
	Protected WithEvents TxtJJgKBN   As TextBox
	
    Protected WithEvents ddlblock_det As DropDownList 
    Protected WithEvents txtjjg_det As TextBox 
	Protected WithEvents txtbrd_det As TextBox 
	
    Protected WithEvents NewBtn As ImageButton
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DeleteBtn As ImageButton
	Protected WithEvents isNew As HtmlInputHidden
	Protected strDateFormat As String
  
    Dim ObjOk As New agri.GL.ClsTrx()
    Dim strLocType As String

    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objEmpDivDs As New Object()


    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strDateFmt As String
    Dim intPRAR As Long
    Dim intConfig As Integer
    Dim strAcceptFormat As String
    Dim intLevel As Integer

	Dim strOTCode As String
    Dim strSelectedPayId As String = ""
    Dim intStatus As Integer
	Dim cnt As Integer
	Dim cnt2 As Double
	Dim cnt3 As Double

#End Region

#Region "Function & Procedure"

    
    Function toNumber(ByVal s As String) As String
        If (s = "" Or s = "NULL") Then
            Return objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber("0", 2), 2)
        Else
            Return objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(s, 2), 2)
        End If

    End Function

    Function Date_Validation(ByVal pv_strInputDate As String, ByVal pv_blnIsShortDate As Boolean) As String
        Dim objSysCfgDs As New Object()
        Dim objActualDate As New Object()
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GR_GET_CONFIG&errmesg=" & Exp.Message & "&redirect=pu/trx/pu_GRList.aspx")
        End Try

        strDateFormat = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))

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

    Function getCode() As String
        Dim strOpCd_GetID As String = "HR_PR_TRX_IDNUMBER"
        Dim objNewID As New Object
        Dim intErrNo As Integer
        Dim tcode As String
        Dim ParamName As String
        Dim ParamValue As String

        tcode = "SPB/" & strLocation & "/" & Mid(Trim(txtWPDate.Text), 4, 2) & Right(Trim(txtWPDate.Text), 2) & "/"
        ParamName = "LOC|ID|HDR|DIGIT"
        ParamValue = strLocation & "|104|" & tcode & "|5"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_GetID, ParamName, ParamValue, objNewID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_PR_TRX_IDNUMBER&errmesg=" & Exp.Message )
        End Try

        Return Trim(objNewID.Tables(0).Rows(0).Item("N"))

    End Function

    Sub clearAll()
	 txtNoRef.Text = ""
 	 txtnopolisi.Text = "" 
	 txtsupir.Text = "" 
	
	 TxtBruto2.Text = ""
     TxtTarra2.Text = ""
     TxtNetto2.Text = ""
     TxtPotKg.Text = ""
     TxtNetto.Text = ""
     TxtJJG.Text = ""
	 TxtBJR.Text = ""
	 TxtPotBM.Text = ""
	 TxtPotBB.Text = ""
	 TxtPotSP.Text = ""
	 TxtPotAR.Text = ""
	 TxtPotPL.Text = ""
	 TxtPot.Text = ""
	 TxtBruto1.Text = ""
	 TxtTarra1.Text = ""
	 TxtNetto1.Text = ""
	 
	 TxtBKecil.Text = ""
	 TxtBBesar.Text = ""
        
     TxtMasuk.Text = ""
     TxtKeluar.Text =""
	 
	 TxtJJgKBN.Text = ""
     
	 lblPeriod.Text = ""
     lblStatus.Text = ""
     lblDateCreated.Text = ""
     lblLastUpdate.Text = ""
     lblupdatedby.Text = ""
    End Sub

#End Region

#Region "Page Load"

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")

        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intPRAR = Session("SS_PRAR")
        intConfig = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")
        strDateFmt = Session("SS_DATEFMT")
        intLevel = Session("SS_USRLEVEL")
		strDateFormat = Session("SS_DATEFMT")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        'ElseIf objAR.mtdHasAcceWssRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxWorkPerformance), intPRAR) = False Then
        '    Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrMessage.Visible = False
            DeleteBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"

            strOTCode = Trim(IIf(Request.QueryString("SPB") <> "", Request.QueryString("SPB"), Request.Form("SPB")))

            If Not Page.IsPostBack Then
                txtWPDate.Text = objGlobal.GetShortDate(strDateFmt, Now())
				txtWPProd.Text = objGlobal.getShortDate(strDateFmt, Now())
          
                If strOTCode <> "" Then
                    isNew.Value = "False"
                    lblSPB.Text = strOTCode
	                onLoad_Display()
					BindBlok() 
                Else
                    isNew.Value = "True"
                    lblSPB.Text = getCode()
                    BindDivision("")
                    BindMILL("")
					BindBlok()
                End If
            End If

        End If
 
    End Sub

#End Region

#Region "Binding"

	Sub BindMILL(ByVal str As String)
        Dim strOpCd_EmpDiv As String = "PM_CLSSETUP_MILL_LIST_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
		Dim intSelect As Integer

        strParamName = "SEARCHSTR"
        strParamValue = "AND PM.Status='1' ORDER By MillCode"

        Try
            intErrNo = Objok.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpDivDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_DIVISICODE_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objEmpDivDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpDivDs.Tables(0).Rows.Count - 1
                objEmpDivDs.Tables(0).Rows(intCnt).Item("MillCode") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("MillCode"))
                objEmpDivDs.Tables(0).Rows(intCnt).Item("MillDesc") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("MillCode")) & " (" & Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("MillDesc")) & ")"
				 IF Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("MillCode")) = trim(str) Then
 				  intSelect = intCnt + 1
				End if
            Next
        End If

        dr = objEmpDivDs.Tables(0).NewRow()
        dr("MillCode") = ""
        dr("MillDesc") = "All"
        objEmpDivDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlMill.DataSource = objEmpDivDs.Tables(0)
        ddlMill.DataTextField = "MillDesc"
        ddlMill.DataValueField = "MillCode"
        ddlMill.DataBind()
        ddlMill.SelectedIndex = intSelect

    End Sub

    Sub BindDivision(ByVal strDivCode As String)
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_DIVISICODE_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
		Dim intSelect As Integer


        strParamName = "SEARCH|SORT"
        strParamValue = "AND A.LocCode = '" & strLocation & "' AND A.Status='1'|ORDER By BlkGrpCode"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpDivDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_DIVISICODE_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objEmpDivDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpDivDs.Tables(0).Rows.Count - 1
                objEmpDivDs.Tables(0).Rows(intCnt).Item("BlkGrpCode") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("BlkGrpCode"))
                objEmpDivDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("BlkGrpCode")) & " (" & Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
                IF Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("BlkGrpCode")) = trim(strDivCode) Then
 				  intSelect = intCnt + 1
				End if
            Next
        End If

        dr = objEmpDivDs.Tables(0).NewRow()
        dr("BlkGrpCode") = " "
        dr("Description") = "Please Select Employee Division"
        objEmpDivDs.Tables(0).Rows.InsertAt(dr, 0)

        ddldivisi.DataSource = objEmpDivDs.Tables(0)
        ddldivisi.DataTextField = "Description"
        ddldivisi.DataValueField = "BlkGrpCode"
        ddldivisi.DataBind()
        ddldivisi.SelectedIndex = intSelect

    End Sub
	
	Protected Function LoadData(ByVal s As String) As DataSet

         Dim strOpCd_Get As String = "PD_PD_TRX_PROD_ESTATELN_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim strSearch As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objOTLnDs As New Object()

        strSearch = "AND CodeSPB='" & s & "'"

        strParamName = "SEARCH"
        strParamValue = strSearch & " ORDER BY CodeBlok"

		cnt = 0
		cnt2 = 0
		cnt3 = 0
		
        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_Get, strParamName, strParamValue, objOTLnDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PD_PD_TRX_PROD_ESTATELN_GET&errmesg=" & Exp.Message)
        End Try

		For intCnt = 0 To objOTLnDs.Tables(0).Rows.Count - 1
            cnt = cnt + objOTLnDs.Tables(0).Rows(intCnt).Item("Jjg")
			cnt2 = cnt2 + objOTLnDs.Tables(0).Rows(intCnt).Item("Brondolan")
			cnt3 = cnt3 + iif( IsDBNull(objOTLnDs.Tables(0).Rows(intCnt).Item("Kg")),0,objOTLnDs.Tables(0).Rows(intCnt).Item("Kg"))
        Next
        Return objOTLnDs
    End Function

    Sub BindDetail(ByVal strOTID As String)
        Dim dsData As DataSet

        dsData = LoadData(strOTID)
        dgOvtDet.DataSource = dsData
        dgOvtDet.DataBind()
		
       	TxtJJG.Text = cnt
    End Sub

    Sub BindBlok()
        Dim strOpCd_EmpDiv As String = "GL_CLSSETUP_SUBBLOCK_LIST_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpBlkDs As New Object()
        Dim dr As DataRow
        Dim strDate As String = Date_Validation(txtWPDate.Text, False)

        ' Dim strMonthPD As String=Month(strDate)
        ' Dim strYearPD AS String=Year(strDate)

        strParamName = "STRSEARCH"
        strParamValue = " AND sub.LocCode = '" & strlocation & "' and sub.Status = '1'  and sub.BlkCode not like 'OH%' order by sub.SubBlkCode"
        ' strParamValue = " AND sub.LocCode = '" & strlocation & "' and sub.AccYear = '" & strYearPD & "' and sub.AccMonth = '" & strMonthPD & "' and sub.Active = '1'  and sub.BlkCode not like 'OH%' order by sub.SubBlkCode"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpBlkDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BLOK_BKM_GET&errmesg=" & Exp.Message)
        End Try

        If objEmpBlkDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpBlkDs.Tables(0).Rows.Count - 1
                objEmpBlkDs.Tables(0).Rows(intCnt).Item("SubBlkCode") = Trim(objEmpBlkDs.Tables(0).Rows(intCnt).Item("SubBlkCode"))
                objEmpBlkDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objEmpBlkDs.Tables(0).Rows(intCnt).Item("Description"))
            Next
        End If

        dr = objEmpBlkDs.Tables(0).NewRow()
        dr("SubBlkCode") = ""
        dr("Description") = "Pilih Blok"
        objEmpBlkDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlblock_det.DataSource = objEmpBlkDs.Tables(0)
        ' ddlblock_det.DataTextField = "Description"
        ddlblock_det.DataValueField = "SubBlkCode"
        ddlblock_det.DataBind()

    End Sub

#End Region

#Region "Event"

    Sub onLoad_Display()
        Dim strOpCd_EmpDiv As String = "PD_PD_TRX_PROD_ESTATE_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objEmpCodeDs As New Object()
        Dim e As New System.EventArgs()

        strParamName = "SEARCH"
        strParamValue = "AND SPBCode Like '" & strOTCode & "%' AND LocCode='" & strLocation & "'"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PD_PD_TRX_PROD_ESTATE_GET&errmesg=" & Exp.Message )
        End Try

        If objEmpCodeDs.Tables(0).Rows.Count = 1 Then
		    txtNoRef.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("RefNo"))
			txtWPDate.Text = Date_Validation(objEmpCodeDs.Tables(0).Rows(0).Item("SPBDate"), True)
			txtWPProd.Text = Date_Validation(objEmpCodeDs.Tables(0).Rows(0).Item("DateProd"), True)
			BindDivision(Trim(objEmpCodeDs.Tables(0).Rows(0).Item("Divisi")))
			BindMILL(Trim(objEmpCodeDs.Tables(0).Rows(0).Item("MillCode")))
    		txtnopolisi.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("NoPolisi"))
			txtsupir.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("Supir"))
   
			TxtBruto2.Text = objEmpCodeDs.Tables(0).Rows(0).Item("WB_Bruto2")
			TxtTarra2.Text = objEmpCodeDs.Tables(0).Rows(0).Item("WB_Tara2")
			TxtNetto2.Text = objEmpCodeDs.Tables(0).Rows(0).Item("WB_Netto2")
			TxtPotKg.Text = objEmpCodeDs.Tables(0).Rows(0).Item("PotonganKg")
			TxtNetto.Text = objEmpCodeDs.Tables(0).Rows(0).Item("WB_NettoFinal")
			TxtJJG.Text = objEmpCodeDs.Tables(0).Rows(0).Item("TotalJJG")
			TxtBJR.Text = objEmpCodeDs.Tables(0).Rows(0).Item("BJR")
			
			TxtBKecil.Text = objEmpCodeDs.Tables(0).Rows(0).Item("BuahKecil")
			TxtBBesar.Text = objEmpCodeDs.Tables(0).Rows(0).Item("BuahBesar")
			
			TxtMasuk.Text = objEmpCodeDs.Tables(0).Rows(0).Item("Masuk")
			TxtKeluar.Text = objEmpCodeDs.Tables(0).Rows(0).Item("Keluar")
			
			TxtPotBM.Text = objEmpCodeDs.Tables(0).Rows(0).Item("POT_BM")
			TxtPotBB.Text = objEmpCodeDs.Tables(0).Rows(0).Item("POT_BB")
			TxtPotSP.Text = objEmpCodeDs.Tables(0).Rows(0).Item("POT_SP")
			TxtPotAR.Text = objEmpCodeDs.Tables(0).Rows(0).Item("POT_AR")
			TxtPotPL.Text = objEmpCodeDs.Tables(0).Rows(0).Item("POT_Lain")
			TxtPot.Text = objEmpCodeDs.Tables(0).Rows(0).Item("PotonganPsn")
			
			TxtBruto1.Text = objEmpCodeDs.Tables(0).Rows(0).Item("WB_Bruto1")
			TxtTarra1.Text = objEmpCodeDs.Tables(0).Rows(0).Item("WB_Tara1")
			TxtNetto1.Text = objEmpCodeDs.Tables(0).Rows(0).Item("WB_Netto1")
			TxtJJgKBN.Text = objEmpCodeDs.Tables(0).Rows(0).Item("TotalJJgkbn")
			
			lblPeriod.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("AccMonth")) & "/" & Trim(objEmpCodeDs.Tables(0).Rows(0).Item("AccYear"))
            lblStatus.Text = IIf(Trim(objEmpCodeDs.Tables(0).Rows(0).Item("Status")) = "1", "Active", "Delete")
            lblDateCreated.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblupdatedby.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("UName"))
			
			BindDetail(lblSPB.Text.trim())
        End If
    End Sub

	Sub SaveBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
    	Dim strIDM As String
		Dim ParamNama As String
        Dim ParamValue As String
        Dim strOpCd_Up As String = "PD_PD_TRX_PROD_ESTATE_UPD"
        Dim intErrNo As Integer
		
		Dim strtxtNoRef  As String = txtNoRef.text.trim.toupper 
		Dim strtxtWPDate As String = Date_Validation(Trim(txtWPDate.Text), False) 
		Dim strtxtWPProd As String = Date_Validation(Trim(txtWPDate.Text), False)
		Dim AM As String = Mid(Trim(txtWPDate.Text), 4, 2)
		Dim AY As String = Right(Trim(txtWPDate.Text), 4)
		Dim strddldivisi As String
		Dim strddlMill As String
		Dim strTxtJJgKBN As String
		Dim strtxtnopolisi As String = txtnopolisi.text.trim.toupper 
		Dim strtxtsupir As String = txtsupir.text.trim.toupper
	
	    Dim strMasuk As String = TxtMasuk.text.trim
		Dim strKeluar  As String = TxtKeluar.text.trim 
	
		Dim strTxtBruto2 As String
		Dim strTxtTarra2 As String
		Dim strTxtNetto2 As String
		Dim strTxtPotKg As String
		Dim strTxtNetto As String
		
		Dim strTxtJJG As String
		Dim strTxtBJR As String
		Dim strTxtPotBM As String
		Dim strTxtPotBB As String
		Dim strTxtPotSP As String
		Dim strTxtPotAR As String
		Dim strTxtPotPL As String
		Dim strTxtPot As String
		Dim strTxtBruto1 As String
		Dim strTxtTarra1 As String
		Dim strTxtNetto1 As String
	
		Dim strTxtBKecil  As String
		Dim strTxtBBesar   As String
		
	 
		strTxtBruto1 = Request.Form("TxtBruto1")
		strTxtTarra1 = Request.Form("TxtTarra1")
		strTxtNetto1 = Request.Form("TxtNetto1")
		strTxtJJgKBN = TxtJJgKBN.Text 
		
		strTxtBruto2 = Request.Form("TxtBruto2")
		strTxtTarra2 = Request.Form("TxtTarra2")
		strTxtNetto2 = Request.Form("TxtNetto2")
		strTxtPotKg = Request.Form("TxtPotKg")
		strTxtNetto = Request.Form("TxtNetto")
		strTxtJJG = Request.Form("TxtJJG")
		strTxtBJR = Request.Form("TxtBJR")
		strTxtPotBM = Request.Form("TxtPotBM")
		strTxtPotBB = Request.Form("TxtPotBB")
		strTxtPotSP = Request.Form("TxtPotSP")
		strTxtPotAR = Request.Form("TxtPotAR")
		strTxtPotPL = Request.Form("TxtPotPL")
		strTxtPot = Request.Form("TxtPot")
		
		strTxtBKecil  = Request.Form("TxtBKecil") 
		strTxtBBesar   = Request.Form("TxtBBesar")
		
		
		
		If ddldivisi.SelectedItem.Value.Trim() <> "" Then
            strddldivisi = ddldivisi.SelectedItem.Value.Trim()
        Else
            strddldivisi = ""
        End If
		
		If ddlMill.SelectedItem.Value.Trim() <> "" Then
            strddlMill = ddlMill.SelectedItem.Value.Trim()
        Else
            strddlMill = ""
        End If
		
		
		
		if strtxtnopolisi = "" Then
		   lblErrMessage.Visible = True
           lblErrMessage.Text = "Silakan id No.Polisi !"
           Exit Sub
		End if
		
		if strtxtsupir = "" Then
			lblErrMessage.Visible = True
           lblErrMessage.Text = "Silakan isi supir !"
           Exit Sub
		End if
		
		'if strddldivisi="" Then
        '   lblErrMessage.Visible = True
        '   lblErrMessage.Text = "Silakan pilih divisi !"
        '   Exit Sub
        'End If
		
		if strddlMill="" Then
           lblErrMessage.Visible = True
           lblErrMessage.Text = "Silakan pilih pabrik !"
           Exit Sub
        End If
		
		if strTxtNetto = "" or strTxtNetto="0" Then 
		   lblErrMessage.Visible = True
           lblErrMessage.Text = "Silakan isi Brutto & Tarra Timbangan !"
           Exit Sub
        End If
		
	
		if strTxtBJR = "" or strTxtBJR="0" Then 
		   lblErrMessage.Visible = True
           lblErrMessage.Text = "Silakan isi Tot. BJR!"
           Exit Sub
        End If
		
		
		
		If isNew.Value = "True" Then
            strIDM = getCode()
        Else
            strIDM = lblSPB.Text.trim()
        End If
        lblSPB.Text = strIDM
				  
        ParamNama = "SPB|REF|SD|AM|AY|NP|SP|WB1|WT1|WN1|WB2|WT2|WN2|PKG|WNF|BK|BB|JJG|BJR|PBM|PBB|PSP|PAR|PLN|PPSN|MC|KET|LOC|CD|UD|UI|DV|JM|JK|JJGKBN|PD"
        ParamValue= strIDM & "|" & _ 
		            strtxtNoRef & "|" & _ 
					strtxtWPDate & "|" & _ 
					AM & "|" & _ 
					AY & "|" & _ 
					strtxtnopolisi & "|" & _ 
					strtxtsupir & "|" & _ 
					iif(strTxtBruto1="","0",strTxtBruto1) & "|" & _  
					iif(strTxtTarra1="","0",strTxtTarra1) & "|" & _ 
					iif(strTxtNetto1="","0",strTxtNetto1)& "|" & _ 
					strTxtBruto2 & "|" & _ 
					strTxtTarra2 & "|" & _ 
					strTxtNetto2 & "|" & _ 
					strTxtPotKg & "|" & _ 
					strTxtNetto & "|" & _ 
					iif(strTxtBKecil="","0",strTxtBKecil) & "|" & _  
					iif(strTxtBBesar="","0",strTxtBBesar) & "|" & _ 
					iif(strTxtJJG="","0",strTxtJJG) & "|" & _ 
					strTxtBJR & "|" & _ 
					iif(strTxtPotBM="","0",strTxtPotBM) & "|" & _ 
					iif(strTxtPotBB="","0",strTxtPotBB) & "|" & _ 
					iif(strTxtPotSP="","0",strTxtPotSP) & "|" & _ 
					iif(strTxtPotAR="","0",strTxtPotAR) & "|" & _ 
					iif(strTxtPotPL="","0",strTxtPotPL) & "|" & _ 
					iif(strTxtPot="","0",strTxtPot) & "|" & _ 		
					strddlMill & "||" & _ 
					strLocation  & "|" & _ 
					DateTime.Now() & "|" & _
					DateTime.Now() & "|" & _
					strUserId & "|" & _
					strddldivisi & "|" & _
					strMasuk & "|" & _
					strKeluar & "|" & _
					iif(strTxtJJgKBN="","0",strTxtJJgKBN) & "|" & _ 
					strtxtWPProd
		 
        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOpCd_Up, ParamNama, ParamValue)
        Catch ex As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_PREMIPANEN_UPD&errmesg=" & ex.ToString() & "&redirect=PR/Setup/PR_SETUP_CUTOFF_ESTATE.aspx")
        End Try

        isNew.Value = "False"
		strOTCode = strIDM
        clearAll()
		onLoad_Display()
        BindDetail(strIDM)
	      
    End Sub

    Sub DeleteBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim ParamNama As String
        Dim ParamValue As String
        Dim strOpCd_Up As String = "PD_PD_TRX_PROD_ESTATE_DEL"
        Dim intErrNo As Integer

        ParamNama = "SPB|LOC"
        ParamValue = lblSPB.Text.Trim() & "|" & strlocation 
        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOpCd_Up, ParamNama, ParamValue)
        Catch ex As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_OVRTIMELN_DEL&errmesg=" & ex.ToString() )
        End Try

		BackBtn_Click(sender,E)
		
    End Sub

	Sub AddBlock_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
		Dim ParamNama As String
        Dim ParamValue As String
        Dim strOpCd_Up As String = "PD_PD_TRX_PROD_ESTATELN_UPD"
		Dim strOpCd_Up2 As String = "PD_PD_TRX_PROD_ESTATE_UPD_JJG"
		
        Dim intErrNo As Integer

	 if isNew.Value then
		   lblErrMessage.Visible = True
           lblErrMessage.Text = "Silakan buat SPB dahulu"
           Exit Sub
	 end if
	 
	 If Date_Validation(txtWPProd.Text, False) = "" Then
            lblErrMessage.Text = "Silakan isi tagl.Panen dengan format (dd/MM/yyyy)"
            lblErrMessage.Visible = True
            Exit Sub
        End If
		
	  if ddlblock_det.SelectedItem.Value.Trim() = "" then
		   lblErrMessage.Visible = True
           lblErrMessage.Text = "Silakan pilih blok"
           Exit Sub
	 end if
	 
	 if txtjjg_det.Text.Trim() = "" then
		   lblErrMessage.Visible = True
           lblErrMessage.Text = "Silakan input JJG"
           Exit Sub
	 end if
		
        ParamNama = "SPB|CB|JJG|BRD|CD|UD|UI|BJR|KG|DP"
        ParamValue = lblSPB.Text.trim() & "|" & _ 
		             ddlblock_det.SelectedItem.Value.Trim() & "|" & _ 
					 txtjjg_det.Text & "|" & _ 
					 iif(txtbrd_det.text.trim() = "","0",txtbrd_det.text.trim() ) & "|" & _ 
					 DateTime.Now() & "|" & _
					 DateTime.Now() & "|" & _
					 strUserId & "|0|0|" & _
                     Date_Validation(txtWPProd.Text, False)                					 
        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOpCd_Up, ParamNama, ParamValue)
        Catch ex As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_OVRTIMELN_DEL&errmesg=" & ex.ToString() )
        End Try
		
	 	 BindDetail(lblSPB.Text.trim())
		 
		ParamNama = "SPB|TJJG"
        ParamValue = lblSPB.Text.trim() & "|" & _
      				 cnt 
        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOpCd_Up2, ParamNama, ParamValue)
        Catch ex As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PD_PD_TRX_PROD_ESTATE_UPD_JJG&errmesg=" & ex.ToString() )
        End Try
		 
		 ddlblock_det.SelectedIndex = 0
		 txtjjg_det.Text = ""
		 
	End Sub
	
	Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOpCd_Status As String = "PD_PD_TRX_PROD_ESTATELN_DEL"
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strValue As String
        Dim lbl As Label
		Dim strSPB As String
        Dim strBlock As String

        dgOvtDet.EditItemIndex = CInt(E.Item.ItemIndex)
        lbl = dgOvtDet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblSPB")
		strSPB = lbl.Text.Trim()
        lbl = dgOvtDet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblblock_ed")
		strBlock = lbl.Text.Trim()
        
        strParam = "SPB|CB"
        strValue = strSPB & "|" & strBlock 

        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOpCd_Status, strParam, strValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_MAIN_DEL&errmesg=" & Exp.Message)
        End Try
		
        dgOvtDet.EditItemIndex = -1
        BindDetail(lblSPB.Text.trim())
    End Sub
	
	 Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim List As DropDownList
        Dim Updbutton As LinkButton
        Dim lbl As Label
		Dim lblTemp As Label
        Dim ddlTemp As DropDownList
		
        dgOvtDet.EditItemIndex = CInt(E.Item.ItemIndex)
        BindDetail(lblSPB.Text.trim())
        If CInt(E.Item.ItemIndex) >= dgOvtDet.Items.Count Then
            dgOvtDet.EditItemIndex = -1
            Exit Sub
        End If

    End Sub

	
	Sub DEDR_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOpCd_Status As String = "PD_PD_TRX_PROD_ESTATELN_UPD"
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strValue As String
        Dim lbl As Label
		Dim edittext As TextBox
		Dim strdp as String 
		Dim strSPB As String
        Dim strBlock As String
		Dim strQty As String
		Dim strBrd As String
		Dim strBjr As String
		Dim strKg As String
		Dim lblMsg As Label
		

       
        lbl =  E.Item.FindControl("lblSPB")
        lbl =  E.Item.FindControl("lblSPB")
		strSPB = lbl.Text.Trim()
        lbl =  E.Item.FindControl("lblblock_ed")
		strBlock = lbl.Text.Trim()
        
		edittext =  E.Item.FindControl("lbldate_ed")
		strdp = Date_Validation(edittext.Text, False)   
		If strdp = "" Then
            lblMsg = E.Item.FindControl("lblDupMsg")
            lblMsg.Text = "Silakan input tgl.prod (dd/MM/yyyy)"
            lblMsg.Visible = True
            Exit Sub
        End If
		
		
		edittext =  E.Item.FindControl("lblQty_ed")
		strQty = iif(edittext.Text.Trim()="","0",edittext.text.trim())
		
		edittext =  E.Item.FindControl("lblBrd_ed")
		strBrd = iif(edittext.Text.Trim()="","0",edittext.text.trim())
		
		edittext =  E.Item.FindControl("lblKg_ed")
		strKg = iif(edittext.Text.Trim()="","0",edittext.text.trim())
		
		edittext =  E.Item.FindControl("lblBjr_ed")
		strBjr =   iif(edittext.Text.Trim()="","0",edittext.text.trim())
		if (strQty <> "0" and strKg <> "0")  then
			strbjr = cdbl(strkg)/cdbl(strQty) 
		end if	
		
		
		
        strParam = "SPB|CB|JJG|BRD|CD|UD|UI|BJR|KG|DP"
        strValue = strSPB & "|" & _ 
		           strBlock & "|" & _ 
				   strQty & "|" & _ 
				   strBrd & "|" & _ 
				   DateTime.Now() & "|" & _
				   DateTime.Now() & "|" & _
				   strUserId & "|" & _ 
				   strBjr & "|" & _ 
				   strKg & "|" & _ 
				   strdp

        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOpCd_Status, strParam, strValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_MAIN_DEL&errmesg=" & Exp.Message)
        End Try
		
		dgOvtDet.EditItemIndex = -1
        BindDetail(lblSPB.Text.trim())
    End Sub

    Sub DEDR_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        dgOvtDet.EditItemIndex = -1
        BindDetail(lblSPB.Text.trim())
    End Sub
	
	Sub KeepRunningSum(ByVal sender As Object, ByVal E As DataGridItemEventArgs)
        If E.Item.ItemType = ListItemType.Footer Then
            E.Item.Cells(2).Text = objGlobal.GetIDDecimalSeparator_FreeDigit(cnt, 0)
			E.Item.Cells(3).Text = objGlobal.GetIDDecimalSeparator_FreeDigit(cnt2, 0)
			E.Item.Cells(5).Text = objGlobal.GetIDDecimalSeparator_FreeDigit(cnt3, 0)
        End If
    End Sub
	
    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PD_trx_ProdList_Estate.aspx")
    End Sub

	Sub NewBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        isNew.Value = "True"
		lblSPB.Text = getCode()
        clearAll()
		dgOvtDet.EditItemIndex = -1
        BindDetail(lblSPB.Text.trim())
	 End Sub
	
	Sub GenBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim ParamNama As String
        Dim ParamValue As String
        Dim strOpCd_Up As String = "PD_PD_TRX_PROD_ESTATELN_GEN"
		Dim intErrNo As Integer		
				
		if isNew.Value = "False" then 
			ParamNama = "LOC|SPB"
			ParamValue = strlocation & "|" & lblSPB.Text.trim() 
		             
			Try
				intErrNo = ObjOk.mtdInsertDataCommon(strOpCd_Up, ParamNama, ParamValue) 
				BindDetail(lblSPB.Text.trim()) 
			Catch ex As Exception
				Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PD_PD_TRX_PROD_ESTATELN_GEN&errmesg=" & ex.ToString() )
			End Try
		end if
	
    End Sub
	
#End Region






End Class
