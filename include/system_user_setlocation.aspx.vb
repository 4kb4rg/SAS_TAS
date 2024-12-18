
Imports System
Imports System.Data

Imports agri.Admin.clsLoc
Imports agri.GlobalHdl.clsAccessRights
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.PWSystem.clsLangCap

Public Class system_user_setlocation : Inherits Page

    Protected WithEvents lblMultiLoc As System.Web.UI.WebControls.Label
    Protected WithEvents lblSingleLoc As System.Web.UI.WebControls.Label
    Protected WithEvents rblLocList As System.Web.UI.WebControls.RadioButtonList
    Protected WithEvents lblDefaultLoc As System.Web.UI.WebControls.Label
    Protected WithEvents lblErrLoc As System.Web.UI.WebControls.Label
    Protected WithEvents lblErrMsgPeriodLoc As System.Web.UI.WebControls.Label
    Protected WithEvents lblErrMsgPeriodYr As System.Web.UI.WebControls.Label
    Protected WithEvents lblErrMsgPeriodSet As System.Web.UI.WebControls.Label
    Protected WithEvents lblErrMaxPeriod As System.Web.UI.WebControls.Label
    Protected WithEvents lblErrMesage As System.Web.UI.WebControls.Label
    Protected WithEvents lblErrSelect As System.Web.UI.WebControls.Label
    Protected WithEvents lblSelectLoc As System.Web.UI.WebControls.Label
    Protected WithEvents lblAccess As System.Web.UI.WebControls.Label
    Protected WithEvents lblYour As System.Web.UI.WebControls.Label
    Protected WithEvents lblSetting As System.Web.UI.WebControls.Label
    Protected WithEvents lblLocation As System.Web.UI.WebControls.Label

    Protected WithEvents lblKodeUnit As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeUnitI As System.Web.UI.WebControls.Label
    Protected WithEvents lblManager As System.Web.UI.WebControls.Label
    Protected WithEvents lblManagerI As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaUnit As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaUnitI As System.Web.UI.WebControls.Label
    Protected WithEvents lblKTU As System.Web.UI.WebControls.Label
    Protected WithEvents lblKTUI As System.Web.UI.WebControls.Label
    Protected WithEvents lblAlamat As System.Web.UI.WebControls.Label
    Protected WithEvents lblAlamatI As System.Web.UI.WebControls.Label
    Protected WithEvents lblKotaI As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodePosI As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoTelp As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoTelpI As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoFax As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoFaxI As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoNPWP As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoNPWPI As System.Web.UI.WebControls.Label
    Protected WithEvents lblUser As System.Web.UI.WebControls.Label

    Protected WithEvents ddlAccMonth As DropDownList
    Protected WithEvents ddlAccYear As DropDownList
    Protected WithEvents lblErrPeriod As Label
    Protected WithEvents lblAccPeriod As Label

    Dim objUserLoc As New agri.PWSystem.clsUser()
    Dim objSysLoc As New agri.PWSystem.clsConfig()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objAdmAcc As New agri.Admin.clsAccPeriod()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGLtrx As New agri.GL.ClsTrx()

    Dim objLocDs As New Object()
    Dim objLangCapDs As New Object()

    Dim strCompanyName As String
    Dim strCompany As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intModuleActivate As Integer
    Dim strAccMonth As String = ""
    Dim strAccYear As String = ""
    Dim strEndAccMonth As String = ""
    Dim strEndAccYear As String = ""
    Dim strLocation As String
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String
	

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompanyName = Session("SS_COMPANYNAME")
        strCompany = Session("SS_COMPANY")
        strLangCode = Session("SS_LANGCODE")
        strUserId = Session("SS_USERID")
        intModuleActivate = Session("SS_MODULEACTIVATE")
		
        strLocType = Session("SS_LOCTYPE")
	    
        lblErrLoc.Visible = False
        lblDefaultLoc.Visible = False
        lblSingleLoc.Visible = False
        lblMultiLoc.Visible = False
        lblErrMaxPeriod.Visible = False
        lblErrMaxPeriod.Text = ""
        lblErrPeriod.Visible = False

        If strUserId = "" Then
            Response.Redirect("/login.aspx")
        Else
            onload_GetLangCap()

            lblUser.Text = Session("SS_USERNAME") & " (" & Session("SS_COMPANYNAME") & ")"

            If Not Page.IsPostBack Then
                onload_display()
                LicCheck()
                strAccMonth = Month(Now())
                strAccYear = Year(Now())
                ddlAccMonth.SelectedValue = strAccMonth
                BindAccYear(strAccYear)
            Else
                onclick_refresh("")
                LicCheck()
            End If

            If Session("SS_FILTERPERIOD") = "1" Then
                ddlAccMonth.Visible = True
                ddlAccYear.Visible = True
                lblAccPeriod.Visible = True
            Else
                ddlAccMonth.Visible = False
                ddlAccYear.Visible = False
                lblAccPeriod.Visible = False
            End If
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblLocation.Text = GetCaption(EnumLangCap.Location)
        lblMultiLoc.text = lblSelectLoc.text & lblLocation.text & lblAccess.text
        lblSingleLoc.text = lblYour.text & lblLocation.text & lblSetting.text
        lblErrLoc.text = lblErrSelect.text & lblLocation.text & "."
    End Sub

    Sub GetEntireLangCap()
        Dim strOpCode_BussTerm As String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
        Dim strParam As String
        Dim intErrNo As Integer

        strParam = strLangCode
        Try
            intErrNo = objLangCap.mtdGetBussTerm(strOpCode_BussTerm, _
                                                 strCompany, _
                                                 "", _
                                                 strUserId, _
                                                 "", _
                                                 "", _
                                                 objLangCapDs, _
                                                 strParam)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LANGCAP&errmesg=" & lblErrMesage.Text & "&redirect=system/user/setlocation.aspx")
        End Try
    End Sub

    
    Function GetCaption(ByVal pv_TermCode) As String
            Dim count As Integer

            For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
                If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If strLocType = EnumLocType.Mill Then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                Else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                End If
                    Exit For
                End If
            Next
        End Function

    Sub LicCheck()
       
    End Sub


    Sub onload_display()
        Dim strOpCode_GetUserLoc As String = "PWSYSTEM_CLSUSER_USERLOC_ACTIVE_GET"
        Dim intCnt As Integer
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strSelectedLoc As String = Session("SS_LOCATION")
        Dim intSelIndex As Integer = -1

        'lblINPeriod.Text = Session("SS_INACCMONTH") & "/" & Session("SS_INACCYEAR")
        'lblNUPeriod.Text = Session("SS_NUACCMONTH") & "/" & Session("SS_NUACCYEAR")
        'lblPUPeriod.Text = Session("SS_PUACCMONTH") & "/" & Session("SS_PUACCYEAR")
        'lblAPPeriod.Text = Session("SS_APACCMONTH") & "/" & Session("SS_APACCYEAR")
        'lblPRPeriod.Text = Session("SS_PRACCMONTH") & "/" & Session("SS_PRACCYEAR")
        'lblPDPeriod.Text = Session("SS_PDACCMONTH") & "/" & Session("SS_PDACCYEAR")
        'lblARPeriod.Text = Session("SS_ARACCMONTH") & "/" & Session("SS_ARACCYEAR")
        'lblGLPeriod.Text = Session("SS_GLACCMONTH") & "/" & Session("SS_GLACCYEAR")
        'lblFAPeriod.Text = Session("SS_FAACCMONTH") & "/" & Session("SS_FAACCYEAR")
        'lblCBPeriod.Text = Session("SS_CBACCMONTH") & "/" & Session("SS_CBACCYEAR")

        strParam = strUserId & "|"
        Try
            intErrNo = objUserLoc.mtdGetUserLocDet(strOpCode_GetUserLoc, _
                                                   objLocDs, _
                                                   strParam)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=SETLOC_GET_USERLOC&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objLocDs.Tables(0).Rows.Count - 1
            If objLocDs.Tables(0).Rows(intCnt).Item("LocCode") = strSelectedLoc Then
                intSelIndex = intCnt
            End If
        Next

        If objLocDs.Tables(0).Rows.Count = 1 Then
            lblMultiLoc.Visible = False
            lblSingleLoc.Visible = True
            lblDefaultLoc.Visible = True
            lblDefaultLoc.Text = objLocDs.Tables(0).Rows(0).Item(1)
            onclick_refresh(objLocDs.Tables(0).Rows(0).Item("LocCode"))
        Else
            lblMultiLoc.Visible = True
            lblSingleLoc.Visible = False
            lblDefaultLoc.Visible = False
            rblLocList.DataSource = objLocDs.Tables(0)
            rblLocList.DataTextField = "Description"
            rblLocList.DataValueField = "LocCode"
            rblLocList.DataBind()
            rblLocList.AutoPostBack = True
            
            If intSelIndex > -1 Then
                rblLocList.SelectedIndex = intSelIndex
                onclick_refresh(rblLocList.SelectedItem.Value)
            Else
                rblLocList.SelectedIndex = 0
                onclick_refresh(rblLocList.SelectedItem.Value)
            End If
        End If

    End Sub

    Protected Sub btnLaunch_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim intEndPeriod As Integer
        Dim intSelPeriod As Integer
		Dim objGLtrx As New agri.GL.ClsTrx()
		
        strEndAccMonth = IIf(Session("SS_GLACCMONTH") <> "", Session("SS_GLACCMONTH"), Month(Now()))
        strEndAccYear = IIf(Session("SS_GLACCYEAR") <> "", Session("SS_GLACCYEAR"), Year(Now()))
      
        strAccMonth = ddlAccMonth.SelectedValue
        strAccYear = ddlAccYear.SelectedValue

        intEndPeriod = (CInt(strEndAccYear) * 100) + CInt(strEndAccMonth)
        intSelPeriod = (CInt(strAccYear) * 100) + CInt(strAccMonth)

        If intSelPeriod < intEndPeriod Then
            lblErrPeriod.Visible = True
            Exit Sub
        End If

        If Session("SS_FILTERPERIOD") = "1" Then
            Session("SS_SELACCMONTH") = Trim(strAccMonth)
            Session("SS_SELACCYEAR") = Trim(strAccYear)
        Else
            Session("SS_SELACCMONTH") = Trim(strEndAccMonth)
            Session("SS_SELACCYEAR") = Trim(strEndAccYear)
        End If
		
		Dim dsPPN As New Object()
		Dim strOpCode As String = "PWSYSTEM_CLSCONFIG_PPNRATE_GET"
		Dim strParamName As String = "ACCYEAR|ACCMONTH"
		Dim strParamValue As String = Trim(strAccYear) & "|" & Trim(strAccMonth)
		Dim intErrNo As Integer
		
		Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                dsPPN)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ID_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try
		
		Session("SS_PPNRATE") = Trim(dsPPN.Tables(0).Rows(0).Item("PPNRate"))
        Response.Redirect("/" & strLangCode & "/menu.aspx")
    End Sub

    Sub onclick_refresh(ByVal pv_strSelectedLoc As String)
        Dim strOpCode_GetUserLocAR As String = "PWSYSTEM_CLSUSER_USERLOC_DETAILS_GET"
        Dim strOpCode_GetSysLoc As String = "PWSYSTEM_CLSCONFIG_SYSLOC_DETAILS_GET"
        Dim strOpCd_AccCfg_Get As String = "ADMIN_CLSACCPERIOD_CONFIG_ACCYEAR_GET"
        Dim strOpCD_Location_Get As String = "ADMIN_CLSLOC_LOCATION_LIST_GET"

        Dim objUserARDs As New Dataset()
        Dim objSysLocDs As New Dataset()
        Dim objAccCfg As New Dataset()
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strLocCode As String

        Try
            If pv_strSelectedLoc = "" Then
               strLocCode = rblLocList.SelectedItem.Value
            Else
               strLocCode = pv_strSelectedLoc
            End If
        Catch Exp As Exception
            lblErrLoc.Visible = True
            Exit Sub
        End Try

        Try
            strParam = strUserId & "|" & strLocCode
            intErrNo = objUserLoc.mtdGetUserLocDet(strOpCode_GetUserLocAR, _
                                                   objUserARDs, _
                                                   strParam)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=SETLOC_GET_USERAR&errmesg=" & lblErrMesage.Text & "&redirect=system/user/setlocation.aspx")
        End Try

        Try
            strParam = strCompany & "|" & strLocCode & "|" & strUserId
            intErrNo = objSysLoc.mtdGetSysLocInfo(strOpCode_GetSysLoc, _
                                                  strCompany, _
                                                  strLocCode, _
                                                  strUserId, _
                                                  objSysLocDs, _
                                                  strParam)
        Catch Exp As Exception 
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=SETLOC_GET_SYSLOC&errmesg=" & lblErrMesage.Text & "&redirect=system/user/setlocation.aspx")
        End Try

        strLocType = objSysLocDs.Tables(0).Rows(0).Item("LocType")
        onload_GetLangCap

        If IsDBNull(objSysLocDs.Tables(0).Rows(0).Item("INAccMonth")) Then
            objSysLocDs.Tables(0).Rows(0).Item("INAccMonth") = ""
        End If
        If IsDBNull(objSysLocDs.Tables(0).Rows(0).Item("INAccYear")) Then
            objSysLocDs.Tables(0).Rows(0).Item("INAccYear") = ""
        End If
        If IsDBNull(objSysLocDs.Tables(0).Rows(0).Item("NUAccMonth")) Then
            objSysLocDs.Tables(0).Rows(0).Item("NUAccMonth") = ""
        End If
        If IsDBNull(objSysLocDs.Tables(0).Rows(0).Item("NUAccYear")) Then
            objSysLocDs.Tables(0).Rows(0).Item("NUAccYear") = ""
        End If
        If IsDBNull(objSysLocDs.Tables(0).Rows(0).Item("PUAccMonth")) Then
            objSysLocDs.Tables(0).Rows(0).Item("PUAccMonth") = ""
        End If
        If IsDBNull(objSysLocDs.Tables(0).Rows(0).Item("PUAccYear")) Then
            objSysLocDs.Tables(0).Rows(0).Item("PUAccYear") = ""
        End If
        If IsDBNull(objSysLocDs.Tables(0).Rows(0).Item("APAccMonth")) Then
            objSysLocDs.Tables(0).Rows(0).Item("APAccMonth") = ""
        End If
        If IsDBNull(objSysLocDs.Tables(0).Rows(0).Item("APAccYear")) Then
            objSysLocDs.Tables(0).Rows(0).Item("APAccYear") = ""
        End If
        If IsDBNull(objSysLocDs.Tables(0).Rows(0).Item("PRAccMonth")) Then
            objSysLocDs.Tables(0).Rows(0).Item("PRAccMonth") = ""
        End If
        If IsDBNull(objSysLocDs.Tables(0).Rows(0).Item("PRAccYear")) Then
            objSysLocDs.Tables(0).Rows(0).Item("PRAccYear") = ""
        End If
        If IsDBNull(objSysLocDs.Tables(0).Rows(0).Item("PDAccMonth")) Then
            objSysLocDs.Tables(0).Rows(0).Item("PDAccMonth") = ""
        End If
        If IsDBNull(objSysLocDs.Tables(0).Rows(0).Item("PDAccYear")) Then
            objSysLocDs.Tables(0).Rows(0).Item("PDAccYear") = ""
        End If
        If IsDBNull(objSysLocDs.Tables(0).Rows(0).Item("PMAccMonth")) Then
            objSysLocDs.Tables(0).Rows(0).Item("PMAccMonth") = ""
        End If
        If IsDBNull(objSysLocDs.Tables(0).Rows(0).Item("PMAccYear")) Then
            objSysLocDs.Tables(0).Rows(0).Item("PMAccYear") = ""
        End If
        If IsDBNull(objSysLocDs.Tables(0).Rows(0).Item("ARAccMonth")) Then
            objSysLocDs.Tables(0).Rows(0).Item("ARAccMonth") = ""
        End If
        If IsDBNull(objSysLocDs.Tables(0).Rows(0).Item("ARAccYear")) Then
            objSysLocDs.Tables(0).Rows(0).Item("ARAccYear") = ""
        End If
        If IsDBNull(objSysLocDs.Tables(0).Rows(0).Item("GLAccMonth")) Then
            objSysLocDs.Tables(0).Rows(0).Item("GLAccMonth") = ""
        End If
        If IsDBNull(objSysLocDs.Tables(0).Rows(0).Item("GLAccYear")) Then
            objSysLocDs.Tables(0).Rows(0).Item("GLAccYear") = ""
        End If
        If IsDBNull(objSysLocDs.Tables(0).Rows(0).Item("FAAccMonth")) Then
            objSysLocDs.Tables(0).Rows(0).Item("FAAccMonth") = ""
        End If
        If IsDBNull(objSysLocDs.Tables(0).Rows(0).Item("FAAccYear")) Then
            objSysLocDs.Tables(0).Rows(0).Item("FAAccYear") = ""
        End If
        If IsDBNull(objSysLocDs.Tables(0).Rows(0).Item("CBAccMonth")) Then
            objSysLocDs.Tables(0).Rows(0).Item("CBAccMonth") = ""
        End If
        If IsDBNull(objSysLocDs.Tables(0).Rows(0).Item("CBAccYear")) Then
            objSysLocDs.Tables(0).Rows(0).Item("CBAccYear") = ""
        End If
        If IsDBNull(objSysLocDs.Tables(0).Rows(0).Item("PhyMonth")) Then
            objSysLocDs.Tables(0).Rows(0).Item("PhyMonth") = ""
        End If
        If IsDBNull(objSysLocDs.Tables(0).Rows(0).Item("PhyYear")) Then
            objSysLocDs.Tables(0).Rows(0).Item("PhyYear") = ""
        End If
        If IsDBNull(objSysLocDs.Tables(0).Rows(0).Item("LocType")) Then
            objSysLocDs.Tables(0).Rows(0).Item("LocType") = ""
        End If
        If IsDBNull(objSysLocDs.Tables(0).Rows(0).Item("LocLevel")) Then
            objSysLocDs.Tables(0).Rows(0).Item("LocLevel") = ""
        End If
        

        Session("SS_LOCATION") = strLocCode
        Session("SS_LOCATIONNAME") = objSysLocDs.Tables(0).Rows(0).Item("LocDesc").Trim()
        Session("SS_LOCTYPE") = objSysLocDs.Tables(0).Rows(0).Item("LocType").Trim()
        Session("SS_LOCLEVEL") = objSysLocDs.Tables(0).Rows(0).Item("LocLevel").Trim()
        Session("SS_MANAGER") = Trim(objSysLocDs.Tables(0).Rows(0).Item("Manager"))
		Session("SS_KTU") = Trim(objSysLocDs.Tables(0).Rows(0).Item("Kasie"))
        
        lblKodeUnitI.Text = Trim(objSysLocDs.Tables(0).Rows(0).Item("CodeUnit"))
        lblManagerI.Text = Trim(objSysLocDs.Tables(0).Rows(0).Item("Manager"))
        lblNamaUnitI.Text = Trim(objSysLocDs.Tables(0).Rows(0).Item("LocDesc"))
        lblKTUI.Text = Trim(objSysLocDs.Tables(0).Rows(0).Item("Kasie"))
        lblAlamatI.Text = Trim(objSysLocDs.Tables(0).Rows(0).Item("LocAddress"))
        lblKotaI.Text = Trim(objSysLocDs.Tables(0).Rows(0).Item("City"))
        lblKodePosI.Text = Trim(objSysLocDs.Tables(0).Rows(0).Item("PostCode"))
        lblNoTelpI.Text = Trim(objSysLocDs.Tables(0).Rows(0).Item("TelNo"))
        lblNoFaxI.Text = Trim(objSysLocDs.Tables(0).Rows(0).Item("FaxNo"))
        lblNoNPWPI.Text = Trim(objSysLocDs.Tables(0).Rows(0).Item("NPWP"))


        Session("SS_AUTOGLPOSTING") = objSysLocDs.Tables(0).Rows(0).Item("DataTransferInd")
        Session("SS_INAR") = objUserARDs.Tables(0).Rows(0).Item("INAR")
        Session("SS_CTAR") = objUserARDs.Tables(0).Rows(0).Item("CTAR")
        Session("SS_WSAR") = objUserARDs.Tables(0).Rows(0).Item("WSAR")
        Session("SS_NUAR") = objUserARDs.Tables(0).Rows(0).Item("NUAR")
        Session("SS_PUAR") = objUserARDs.Tables(0).Rows(0).Item("PUAR")
        Session("SS_APAR") = objUserARDs.Tables(0).Rows(0).Item("APAR")
        Session("SS_HRAR") = objUserARDs.Tables(0).Rows(0).Item("HRAR")
        Session("SS_PRAR") = objUserARDs.Tables(0).Rows(0).Item("PRAR")
        Session("SS_PDAR") = objUserARDs.Tables(0).Rows(0).Item("PDAR")
        Session("SS_BIAR") = objUserARDs.Tables(0).Rows(0).Item("BIAR")
        Session("SS_WMAR") = objUserARDs.Tables(0).Rows(0).Item("WMAR")
        Session("SS_PMAR") = objUserARDs.Tables(0).Rows(0).Item("PMAR")
        Session("SS_CMAR") = objUserARDs.Tables(0).Rows(0).Item("CMAR")
        Session("SS_GLAR") = objUserARDs.Tables(0).Rows(0).Item("GLAR")
        Session("SS_FAAR") = objUserARDs.Tables(0).Rows(0).Item("FAAR")
        Session("SS_CBAR") = objUserARDs.Tables(0).Rows(0).Item("CBAR") 
        
        Session("SS_INACCMONTH") = objSysLocDs.Tables(0).Rows(0).Item("INAccMonth").Trim()
        Session("SS_INACCYEAR") = objSysLocDs.Tables(0).Rows(0).Item("INAccYear").Trim()
        Session("SS_NUACCMONTH") = objSysLocDs.Tables(0).Rows(0).Item("NUAccMonth").Trim()
        Session("SS_NUACCYEAR") = objSysLocDs.Tables(0).Rows(0).Item("NUAccYear").Trim()
        Session("SS_PUACCMONTH") = objSysLocDs.Tables(0).Rows(0).Item("PUAccMonth").Trim()
        Session("SS_PUACCYEAR") = objSysLocDs.Tables(0).Rows(0).Item("PUAccYear").Trim()
        Session("SS_APACCMONTH") = objSysLocDs.Tables(0).Rows(0).Item("APAccMonth").Trim()
        Session("SS_APACCYEAR") = objSysLocDs.Tables(0).Rows(0).Item("APAccYear").Trim()
        Session("SS_PRACCMONTH") = objSysLocDs.Tables(0).Rows(0).Item("PRAccMonth").Trim()
        Session("SS_PRACCYEAR") = objSysLocDs.Tables(0).Rows(0).Item("PRAccYear").Trim()
        Session("SS_PDACCMONTH") = objSysLocDs.Tables(0).Rows(0).Item("PDAccMonth").Trim()
        Session("SS_PDACCYEAR") = objSysLocDs.Tables(0).Rows(0).Item("PDAccYear").Trim()
        Session("SS_PMACCMONTH") = objSysLocDs.Tables(0).Rows(0).Item("PMAccMonth").Trim()
        Session("SS_PMACCYEAR") = objSysLocDs.Tables(0).Rows(0).Item("PMAccYear").Trim()
        Session("SS_ARACCMONTH") = objSysLocDs.Tables(0).Rows(0).Item("ARAccMonth").Trim()
        Session("SS_ARACCYEAR") = objSysLocDs.Tables(0).Rows(0).Item("ARAccYear").Trim()
        Session("SS_GLACCMONTH") = objSysLocDs.Tables(0).Rows(0).Item("GLAccMonth").Trim()
        Session("SS_GLACCYEAR") = objSysLocDs.Tables(0).Rows(0).Item("GLAccYear").Trim()
        Session("SS_FAACCMONTH") = objSysLocDs.Tables(0).Rows(0).Item("FAAccMonth").Trim()
        Session("SS_FAACCYEAR") = objSysLocDs.Tables(0).Rows(0).Item("FAAccYear").Trim()
        Session("SS_CBACCMONTH") = objSysLocDs.Tables(0).Rows(0).Item("CBAccMonth").Trim()
        Session("SS_CBACCYEAR") = objSysLocDs.Tables(0).Rows(0).Item("CBAccYear").Trim()

        Session("SS_PHYMONTH") = objSysLocDs.Tables(0).Rows(0).Item("PhyMonth").Trim()
        Session("SS_PHYYEAR") = objSysLocDs.Tables(0).Rows(0).Item("PhyYear").Trim()
        Session("SS_MODULEACTIVATE") = objSysLocDs.Tables(0).Rows(0).Item("ModuleActivate")
        intModuleActivate = Session("SS_MODULEACTIVATE")
        Session("SS_COMPANYADDRESS") =  Trim(objSysLocDs.Tables(0).Rows(0).Item("LocAddress"))
        'Session("SS_PPNRATE") = Trim(objSysLocDs.Tables(0).Rows(0).Item("PPNRate"))
        Session("SS_LOCTYPE") = Trim(objSysLocDs.Tables(0).Rows(0).Item("LocType"))
        Session("SS_LOCLEVEL") = Trim(objSysLocDs.Tables(0).Rows(0).Item("LocLevel"))
        Session("SS_ADAR") = mtdCombineAccessRights(Session("SS_SH_USER_ADAR"), objUserARDs.Tables(0).Rows(0).Item("ADAR"))




        'lblINPeriod.Text = Session("SS_INACCMONTH") & "/" & Session("SS_INACCYEAR")
        'lblNUPeriod.Text = Session("SS_NUACCMONTH") & "/" & Session("SS_NUACCYEAR")
        'lblPUPeriod.Text = Session("SS_PUACCMONTH") & "/" & Session("SS_PUACCYEAR")
        'lblAPPeriod.Text = Session("SS_APACCMONTH") & "/" & Session("SS_APACCYEAR")
        'lblPRPeriod.Text = Session("SS_PRACCMONTH") & "/" & Session("SS_PRACCYEAR")
        'lblPDPeriod.Text = Session("SS_PDACCMONTH") & "/" & Session("SS_PDACCYEAR")
        'lblARPeriod.Text = Session("SS_ARACCMONTH") & "/" & Session("SS_ARACCYEAR")
        'lblGLPeriod.Text = Session("SS_GLACCMONTH") & "/" & Session("SS_GLACCYEAR")
        'lblFAPeriod.Text = Session("SS_FAACCMONTH") & "/" & Session("SS_FAACCYEAR")
        'lblCBPeriod.Text = Session("SS_CBACCMONTH") & "/" & Session("SS_CBACCYEAR") 

        
      
        If  lblSingleLoc.Visible = True then
            lblMultiLoc.Visible = False
        Else
            lblMultiLoc.Visible = True
        End If

        Try
            strParam = "||" & Session("SS_GLACCYEAR")
            intErrNo = objAdmAcc.mtdGetAccPeriodCfg(strOpCd_AccCfg_Get, _
                                                    strCompany, _
                                                    strLocCode, _
                                                    strUserId, _
                                                    strParam, _
                                                    objAccCfg)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=SYSTEM_SETLOCATION_ACCCFG_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        Try
            Session("SS_MAXPERIOD") = Convert.ToInt16(objAccCfg.Tables(0).Rows(0).Item("MaxPeriod"))
        Catch Exp As Exception
            Session("SS_MAXPERIOD") = 0
            lblErrMaxPeriod.Text = lblErrMsgPeriodLoc.Text & Session("SS_LOCATIONNAME") & lblErrMsgPeriodYr.Text & Session("SS_GLACCYEAR") & lblErrMsgPeriodSet.Text
            lblErrMaxPeriod.Visible = True
        End Try

        objAccCfg = Nothing
        objSysLocDs = Nothing
        objUserARDs = Nothing

        'remarks
        'Response.Write("<Script language=""Javascript"">parent.banner.location.href = '../../../banner.aspx'</Script>")
        'Response.Write("<Script language=""Javascript"">parent.left.location.href = '/" & strLangCode & "/appmenu.aspx'</Script>")
        'Response.Write("<Script language=""Javascript"">parent.left.location.href = '/" & strLangCode & "/menu.aspx'</Script>")
        'Response.Redirect("/" & strLangCode & "/menu.aspx")

        'validasi selected period
		        'add by aam 26 Jul 2010 create session HR Code
        Select Case Session("SS_COMPANY")
            Case "STA"
                Session("SS_HR_COMPANY") = "S"
            Case "SPP"
                Session("SS_HR_COMPANY") = "P"
            Case "KAS"
                Session("SS_HR_COMPANY") = "K"
            Case "JSAR"
                Session("SS_HR_COMPANY") = "J"
            Case "MAL"
                Session("SS_HR_COMPANY") = "M"
            Case "PML"
                Session("SS_HR_COMPANY") = "L"
            Case "KSUP"
                Session("SS_HR_COMPANY") = "U"
        End Select


        strLocation = strLocCode
        strEndAccMonth = Session("SS_GLACCMONTH")
        strEndAccYear = Session("SS_GLACCYEAR")

        Dim strParamName As String
        Dim strParamValue As String
        Dim objResult As New Object
        Dim intCnt As Integer
        Dim strOpCd As String = "ADMIN_CLSSHARE_MTHEND_GET_ALL"

        strParamName = "LOCCODE"
        strParamValue = strLocation

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objResult)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_GET_POItem&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList.aspx")
        End Try

        If objResult.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objResult.Tables(0).Rows.Count - 1
                Select Case Trim(objResult.Tables(0).Rows(intCnt).Item("ModuleCode").Trim())
                    Case objGlobal.EnumModule.Purchasing
                        Session("SS_CLSPUACCMONTH") = Trim(objResult.Tables(0).Rows(intCnt).Item("CurrAccMonth").Trim())
                        Session("SS_CLSPUACCYEAR") = Trim(objResult.Tables(0).Rows(intCnt).Item("CurrAccYear").Trim())

                    Case objGlobal.EnumModule.Inventory
                        Session("SS_CLSINACCMONTH") = Trim(objResult.Tables(0).Rows(intCnt).Item("CurrAccMonth").Trim())
                        Session("SS_CLSINACCYEAR") = Trim(objResult.Tables(0).Rows(intCnt).Item("CurrAccYear").Trim())

                    Case objGlobal.EnumModule.AccountPayable
                        Session("SS_CLSAPACCMONTH") = Trim(objResult.Tables(0).Rows(intCnt).Item("CurrAccMonth").Trim())
                        Session("SS_CLSAPACCYEAR") = Trim(objResult.Tables(0).Rows(intCnt).Item("CurrAccYear").Trim())

                    Case objGlobal.EnumModule.CashAndBank
                        Session("SS_CLSCBACCMONTH") = Trim(objResult.Tables(0).Rows(intCnt).Item("CurrAccMonth").Trim())
                        Session("SS_CLSCBACCYEAR") = Trim(objResult.Tables(0).Rows(intCnt).Item("CurrAccYear").Trim())

                    Case objGlobal.EnumModule.GeneralLedger
                        Session("SS_CLSGLACCMONTH") = Trim(objResult.Tables(0).Rows(intCnt).Item("CurrAccMonth").Trim())
                        Session("SS_CLSGLACCYEAR") = Trim(objResult.Tables(0).Rows(intCnt).Item("CurrAccYear").Trim())

                    Case objGlobal.EnumModule.HumanResource
                        Session("SS_CLSHRACCMONTH") = Trim(objResult.Tables(0).Rows(intCnt).Item("CurrAccMonth").Trim())
                        Session("SS_CLSHRACCYEAR") = Trim(objResult.Tables(0).Rows(intCnt).Item("CurrAccYear").Trim())

                    Case objGlobal.EnumModule.Payroll
                        Session("SS_CLSPRACCMONTH") = Trim(objResult.Tables(0).Rows(intCnt).Item("CurrAccMonth").Trim())
                        Session("SS_CLSPRACCYEAR") = Trim(objResult.Tables(0).Rows(intCnt).Item("CurrAccYear").Trim())

                    Case objGlobal.EnumModule.Production
                        Session("SS_CLSPDACCMONTH") = Trim(objResult.Tables(0).Rows(intCnt).Item("CurrAccMonth").Trim())
                        Session("SS_CLSPDACCYEAR") = Trim(objResult.Tables(0).Rows(intCnt).Item("CurrAccYear").Trim())

                    Case objGlobal.EnumModule.MillProduction
                        Session("SS_CLSPMACCMONTH") = Trim(objResult.Tables(0).Rows(intCnt).Item("CurrAccMonth").Trim())
                        Session("SS_CLSPMACCYEAR") = Trim(objResult.Tables(0).Rows(intCnt).Item("CurrAccYear").Trim())

                    Case objGlobal.EnumModule.Nursery
                        Session("SS_CLSNUACCMONTH") = Trim(objResult.Tables(0).Rows(intCnt).Item("CurrAccMonth").Trim())
                        Session("SS_CLSNUACCYEAR") = Trim(objResult.Tables(0).Rows(intCnt).Item("CurrAccYear").Trim())

                    Case objGlobal.EnumModule.Workshop
                        Session("SS_CLSWSACCMONTH") = Trim(objResult.Tables(0).Rows(intCnt).Item("CurrAccMonth").Trim())
                        Session("SS_CLSWSACCYEAR") = Trim(objResult.Tables(0).Rows(intCnt).Item("CurrAccYear").Trim())

                    Case objGlobal.EnumModule.MillContract
                        Session("SS_CLSCMACCMONTH") = Trim(objResult.Tables(0).Rows(intCnt).Item("CurrAccMonth").Trim())
                        Session("SS_CLSCMACCYEAR") = Trim(objResult.Tables(0).Rows(intCnt).Item("CurrAccYear").Trim())

                    Case objGlobal.EnumModule.MillWeighing
                        Session("SS_CLSWMACCMONTH") = Trim(objResult.Tables(0).Rows(intCnt).Item("CurrAccMonth").Trim())
                        Session("SS_CLSWMACCYEAR") = Trim(objResult.Tables(0).Rows(intCnt).Item("CurrAccYear").Trim())

                    Case objGlobal.EnumModule.Billing
                        Session("SS_CLSBIACCMONTH") = Trim(objResult.Tables(0).Rows(intCnt).Item("CurrAccMonth").Trim())
                        Session("SS_CLSBIACCYEAR") = Trim(objResult.Tables(0).Rows(intCnt).Item("CurrAccYear").Trim())

                    Case objGlobal.EnumModule.FixAsset
                        Session("SS_CLSFAACCMONTH") = Trim(objResult.Tables(0).Rows(intCnt).Item("CurrAccMonth").Trim())
                        Session("SS_CLSFAACCYEAR") = Trim(objResult.Tables(0).Rows(intCnt).Item("CurrAccYear").Trim())
                End Select
            Next
        End If
    End Sub

    Function mtdCombineAccessRights(ByVal lngAR1 As Long, ByVal lngAR2 As Long) As Long
        Dim lngAR As Long = 0
        Dim I As Long
        Dim lngCheck As Long
        Dim lngMaxValue As Long
        If lngAR1 > lngAR2 Then
            lngMaxValue = lngAR1
        Else
            lngMaxValue = lngAR2
        End If
        
        I = -1
        Do
            I = I + 1
            lngCheck = (2 ^ I)
            If objAR.mtdHasAccessRights(lngCheck, lngAR1) = True Or objAR.mtdHasAccessRights(lngCheck, lngAR2) = True Then
                lngAR = lngAR + lngCheck
            End If
        Loop Until lngCheck > lngMaxValue
        
        mtdCombineAccessRights = lngAR
    End Function

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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & lblErrMesage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objAccYearDs.Tables(0).Rows.Count - 1
            objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear") = Trim(objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear"))
            objAccYearDs.Tables(0).Rows(intCnt).Item("UserName") = Trim(objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear"))
            If objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear") = pv_strAccYear Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        ddlAccYear.DataSource = objAccYearDs.Tables(0)
        ddlAccYear.DataValueField = "AccYear"
        ddlAccYear.DataTextField = "UserName"
        ddlAccYear.DataBind()
        ddlAccYear.SelectedIndex = intSelectedIndex - 1
    End Sub


End Class
