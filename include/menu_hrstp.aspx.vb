
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls


Imports agri.GlobalHdl
Imports agri.PWSystem.clsLangCap
Imports agri.PWSystem.clsConfig


Public Class menu_hrstp : Inherits Page


    Dim strUserId As String
    Dim strLangCode As String
    Dim strCompany As String
    Dim strLocation As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intLocType As Integer
    Dim intModuleActivate As Integer
    Dim strLocTag As String
	Dim intLevel As Integer

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objLangCapVw As New DataView

  
    Dim intHRAR As Long
    Dim intPRAR As Long
   

    Const C_ADMIN = "ADMIN"

   
    Dim blnHRS As Boolean = False
    Dim blnPRS As Boolean = False


    Protected WithEvents lnkStpHR01 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpHR02 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpHR03 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpHR04 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpHR05 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpHR06 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpHR07 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpHR08 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpHR09 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpHR10 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpHR11 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpHR12 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpHR13 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpHR14 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpHR15 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpHR16 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpHR17 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpHR18 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpHR19 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpHR20 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpHR21 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpHR22 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpHR23 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpHR24 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpHR25 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpHR26 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpHR27 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpPR01 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpPR02 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpPR03 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpPR04 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpPR05 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpPR06 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpPR07 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpPR08 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpPR09 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpPR10 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpPR11 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpPR12 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpPR13 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpPR14 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpPR15 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpPR16 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpPR17 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpPR18 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpPR19 As System.Web.UI.WebControls.HyperLink


    Protected WithEvents tlbStpHRHead As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tlbStpHR As System.Web.UI.HtmlControls.HtmlTable

    Protected WithEvents tlbSpc1 As System.Web.UI.HtmlControls.HtmlTable
	
    Protected WithEvents tlbStpPRHead As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tlbStpPR As System.Web.UI.HtmlControls.HtmlTable

	Protected WithEvents tlbStpHRHead_Estate As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tlbStpHR_Estate As System.Web.UI.HtmlControls.HtmlTable

	Protected WithEvents tlbStpHRHead_MILL As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tlbStpHR_MILL As System.Web.UI.HtmlControls.HtmlTable

	Protected WithEvents tlbSpc3 As System.Web.UI.HtmlControls.HtmlTable

	Protected WithEvents tlbStpPRHead_Estate As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tlbStpPR_Estate As System.Web.UI.HtmlControls.HtmlTable
	
	Protected WithEvents tlbStpPRHead_MILL As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tlbStpPR_MILL As System.Web.UI.HtmlControls.HtmlTable

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strUserId = Session("SS_USERID")

        intModuleActivate = Session("SS_MODULEACTIVATE")
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strLangCode = Session("SS_LANGCODE")
        intLocType = Session("SS_LOCTYPE")
        strAccMonth = Session("SS_FAACCMONTH")
        strAccYear = Session("SS_FAACCYEAR")
		intLevel = Session("SS_USRLEVEL")

		
        If strUserId = "" Then
            Response.Write("<Script language=""Javascript"">parent.right.location.href = '/Login.aspx'</Script>")
        Else

	
        intHRAR = Session("SS_HRAR")
        intPRAR = Session("SS_PRAR")
      
		GetEntireLangCap()
        onLoad_Display()


        End If

    End Sub

   Sub onLoad_Display()

        If Session("SS_LOCATION") = "" Or Session("SS_MAXPERIOD") = 0 Then
            Call DisableAllHead()
        Else
             Call DisableAllHead()
            IF intLocType=1 then   'only estate location type
				Call StpHRRight_Estate()
				Call StpPRRight_Estate()
				tlbSpc3.Visible = True
			Else
				Call StpHRRight_MILL()
				Call StpPRRight_MILL()
				tlbSpc3.Visible = True
				'Call StpHRRight()
				'Call StpPRRight()
			END IF
         
           
        End If

    End Sub


	Private Sub StpHRRight_Estate
		if intLevel=0 Then
		tlbStpHRHead_Estate.Visible = False
		else
		tlbStpHRHead_Estate.Visible = True
		end if
	End Sub
	
	Private Sub StpPRRight_Estate
		if intLevel=0 Then
		tlbStpPRHead_Estate.Visible = False
		else
		tlbStpPRHead_Estate.Visible = True
		end if
	End Sub
	
	Private Sub StpHRRight_MILL
		if intLevel=0 Then
		tlbStpHRHead_MILL.Visible = False
		else
		tlbStpHRHead_MILL.Visible = True
		end if
	End Sub
	
	Private Sub StpPRRight_MILL
		if intLevel=0 Then
		tlbStpPRHead_MILL.Visible = False
		else
		tlbStpPRHead_MILL.Visible = True
		end if
	End Sub
	
   Private Sub StpHRRight()

        Dim strDeptCodeTag As String
        Dim strDeptTag As String
        Dim strFuncTag As String
        Dim strLevelTag As String
        Dim strReligionTag As String
        Dim strCareerProgTag As String
        Dim strJamsostekTag As String
        Dim strPOHTag As String

        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.HumanResource), intModuleActivate) Then

            strDeptCodeTag = GetCaption(objLangCap.EnumLangCap.Department) & " Code"
            strDeptTag = GetCaption(objLangCap.EnumLangCap.Department)
            strFuncTag = GetCaption(objLangCap.EnumLangCap.Func)
            strLevelTag = GetCaption(objLangCap.EnumLangCap.Level)
            strReligionTag = GetCaption(objLangCap.EnumLangCap.Religion)
            strCareerProgTag = GetCaption(objLangCap.EnumLangCap.CareerProgress)
            strJamsostekTag = GetCaption(objLangCap.EnumLangCap.Jamsostek)
            strPOHTag = GetCaption(objLangCap.EnumLangCap.POHCode)

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRPointHired), intHRAR) = True) Then
                tlbStpHR.Rows(0).Visible = True
                lnkStpHR01.Text = strPOHTag
                blnHRS = True
            Else
                tlbStpHR.Rows(0).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRDepartment), intHRAR) = True) Then
                tlbStpHR.Rows(1).Visible = True
                lnkStpHR02.Text = strDeptCodeTag
                blnHRS = True
            Else
                tlbStpHR.Rows(1).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRCompany), intHRAR) = True) Then
                tlbStpHR.Rows(2).Visible = True
                lnkStpHR03.Text = strDeptTag
                blnHRS = True
            Else
                tlbStpHR.Rows(2).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRNationality), intHRAR) = True) Then
                tlbStpHR.Rows(3).Visible = True
                blnHRS = True
            Else
                tlbStpHR.Rows(3).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRFunction), intHRAR) = True) Then
                tlbStpHR.Rows(4).Visible = True
                lnkStpHR05.Text = strFuncTag
                blnHRS = True
            Else
                tlbStpHR.Rows(4).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRPosition), intHRAR) = True) Then
                tlbStpHR.Rows(5).Visible = True
                blnHRS = True
            Else
                tlbStpHR.Rows(5).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRLevel), intHRAR) = True) Then
                tlbStpHR.Rows(6).Visible = True
                lnkStpHR07.Text = strLevelTag
                blnHRS = True
            Else
                tlbStpHR.Rows(6).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRReligion), intHRAR) = True) Then
                tlbStpHR.Rows(7).Visible = True
                lnkStpHR08.Text = strReligionTag
                blnHRS = True
            Else
                tlbStpHR.Rows(7).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRICType), intHRAR) = True) Then
                tlbStpHR.Rows(8).Visible = True
                blnHRS = True
            Else
                tlbStpHR.Rows(8).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRRace), intHRAR) = True) Then
                tlbStpHR.Rows(9).Visible = True
                blnHRS = True
            Else
                tlbStpHR.Rows(9).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRSkill), intHRAR) = True) Then
                tlbStpHR.Rows(10).Visible = True
                blnHRS = True
            Else
                tlbStpHR.Rows(10).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRShift), intHRAR) = True) Then
                tlbStpHR.Rows(11).Visible = True
                blnHRS = True
            Else
                tlbStpHR.Rows(11).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRQualification), intHRAR) = True) Then
                tlbStpHR.Rows(12).Visible = True
                blnHRS = True
            Else
                tlbStpHR.Rows(12).Visible = False
            End If

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRSubject), intHRAR) = True) Then
                tlbStpHR.Rows(13).Visible = True
                blnHRS = True
            Else
                tlbStpHR.Rows(13).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREvaluation), intHRAR) = True) Then
                tlbStpHR.Rows(14).Visible = True
                blnHRS = True
            Else
                tlbStpHR.Rows(14).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRCPCode), intHRAR) = True) Then
                tlbStpHR.Rows(15).Visible = True
                lnkStpHR16.Text = strCareerProgTag
                blnHRS = True
            Else
                tlbStpHR.Rows(15).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRSalScheme), intHRAR) = True) Then
                tlbStpHR.Rows(16).Visible = True
                blnHRS = True
            Else
                tlbStpHR.Rows(16).Visible = False
            End If

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRSalGrade), intHRAR) = True) Then
                tlbStpHR.Rows(17).Visible = True
                blnHRS = True
            Else
                tlbStpHR.Rows(17).Visible = False
            End If

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRBankFormat), intHRAR) = True) Then
                tlbStpHR.Rows(18).Visible = True
                blnHRS = True
            Else
                tlbStpHR.Rows(18).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRBank), intHRAR) = True) Then
                tlbStpHR.Rows(19).Visible = True
                blnHRS = True
            Else
                tlbStpHR.Rows(19).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRTaxBranch), intHRAR) = True) Then
                tlbStpHR.Rows(20).Visible = True
                blnHRS = True
            Else
                tlbStpHR.Rows(20).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRTax), intHRAR) = True) Then
                tlbStpHR.Rows(21).Visible = True
                blnHRS = True
            Else
                tlbStpHR.Rows(21).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRJamsostek), intHRAR) = True) Then
                tlbStpHR.Rows(22).Visible = True
                lnkStpHR23.Text = strJamsostekTag
                blnHRS = True
            Else
                tlbStpHR.Rows(22).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRPublicHoliday), intHRAR) = True) Then
                tlbStpHR.Rows(23).Visible = True
                blnHRS = True
            Else
                tlbStpHR.Rows(23).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRHoliday), intHRAR) = True) Then
                tlbStpHR.Rows(24).Visible = True
                blnHRS = True
            Else
                tlbStpHR.Rows(24).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRGang), intHRAR) = True) Then
                tlbStpHR.Rows(25).Visible = True
                blnHRS = True
            Else
                tlbStpHR.Rows(25).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRContractorSupervision), intHRAR) = True) Then
                tlbStpHR.Rows(26).Visible = True
                blnHRS = True
            Else
                tlbStpHR.Rows(26).Visible = False
            End If
         Else
            tlbStpHRHead.Visible = False
            tlbStpHR.Visible = False
            tlbSpc1.Visible = False
        End If
		
	            tlbStpHRHead.Visible = blnHRS
                tlbStpHR.Visible = blnHRS
                tlbSpc1.Visible = blnHRS
    End Sub

    Private Sub StpPRRight()

        Dim strLoadTag As String
        Dim strRouteTag As String
        Dim strRiceTag As String
        Dim strIncentiveTag As String
        Dim strDendaTag As String
        Dim strAirBusTag As String
        Dim strMaternityTag As String
        Dim strRelocationTag As String
        Dim strMedicalTag As String
        Dim strDanaPensiunTag As String
        Dim strEmployeeEvaluationTag As String
        Dim strStandardEvaluationTag As String
        Dim strSalaryIncreaseTag As String
        Dim strTranIncTag As String

        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Payroll), intModuleActivate) Then

            strLoadTag = GetCaption(objLangCap.EnumLangCap.Load)
            strRouteTag = GetCaption(objLangCap.EnumLangCap.Route)
            strRiceTag = GetCaption(objLangCap.EnumLangCap.RiceRation)
            strIncentiveTag = Trim(GetCaption(objLangCap.EnumLangCap.Incentive))
            strAirBusTag = GetCaption(objLangCap.EnumLangCap.AirBusTicket)
            strMaternityTag = GetCaption(objLangCap.EnumLangCap.Maternity)
            strRelocationTag = GetCaption(objLangCap.EnumLangCap.Relocation)
            strMedicalTag = GetCaption(objLangCap.EnumLangCap.Medical)
            strDanaPensiunTag = GetCaption(objLangCap.EnumLangCap.DanaPensiun)
            strEmployeeEvaluationTag = Trim(GetCaption(objLangCap.EnumLangCap.EmployeeEvaluation))
            strStandardEvaluationTag = GetCaption(objLangCap.EnumLangCap.StandardEvaluation)
            strSalaryIncreaseTag = GetCaption(objLangCap.EnumLangCap.SalaryIncrease)
            strTranIncTag = GetCaption(objLangCap.EnumLangCap.TransportIncentive)
            strDendaTag = GetCaption(objLangCap.EnumLangCap.Denda)

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRADGroup), intPRAR) = True) Then
                tlbStpPR.Rows(0).Visible = True
                blnPRS = True
            Else
                tlbStpPR.Rows(0).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRAllowanceDeduction), intPRAR) = True) Then
                tlbStpPR.Rows(1).Visible = True
                blnPRS = True
            Else
                tlbStpPR.Rows(1).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRLoad), intPRAR) = True) Then
                tlbStpPR.Rows(2).Visible = True
                lnkStpPR03.Text = strLoadTag
                blnPRS = True
            Else
                tlbStpPR.Rows(2).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRRoute), intPRAR) = True) Then
                tlbStpPR.Rows(3).Visible = True
                lnkStpPR04.Text = strRouteTag
                blnPRS = True
            Else
                tlbStpPR.Rows(3).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRSalary), intPRAR) = True) Then
                tlbStpPR.Rows(4).Visible = True
                blnPRS = True
            Else
                tlbStpPR.Rows(4).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRAirBus), intPRAR) = True) Then
                tlbStpPR.Rows(5).Visible = True
                lnkStpPR06.Text = strAirBusTag
                blnPRS = True
            Else
                tlbStpPR.Rows(5).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDenda), intPRAR) = True) Then
                tlbStpPR.Rows(6).Visible = True
                lnkStpPR07.Text = strDendaTag
                blnPRS = True
            Else
                tlbStpPR.Rows(6).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRHarvInc), intPRAR) = True) Then
                tlbStpPR.Rows(7).Visible = True
                blnPRS = True
            Else
                tlbStpPR.Rows(7).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRContract), intPRAR) = True) Then
                tlbStpPR.Rows(8).Visible = True
                blnPRS = True
            Else
                tlbStpPR.Rows(8).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRRice), intPRAR) = True) Then
                tlbStpPR.Rows(9).Visible = True
                lnkStpPR10.Text = strRiceTag
                blnPRS = True
            Else
                tlbStpPR.Rows(9).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRPremi), intPRAR) = True) Then
                tlbStpPR.Rows(10).Visible = True
                lnkStpPR11.Text = strIncentiveTag
                blnPRS = True
            Else
                tlbStpPR.Rows(10).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMedical), intPRAR) = True) Then
                tlbStpPR.Rows(11).Visible = True
                lnkStpPR12.Text = strMedicalTag
                blnPRS = True
            Else
                tlbStpPR.Rows(11).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMaternity), intPRAR) = True) Then
                tlbStpPR.Rows(12).Visible = True
                lnkStpPR13.Text = strMaternityTag
                blnPRS = True
            Else
                tlbStpPR.Rows(12).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRPaySetup), intPRAR) = True) Then
                tlbStpPR.Rows(13).Visible = True
                blnPRS = True
            Else
                tlbStpPR.Rows(13).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRPensiun), intPRAR) = True) Then
                tlbStpPR.Rows(14).Visible = True
                lnkStpPR15.Text = strDanaPensiunTag
                blnPRS = True
            Else
                tlbStpPR.Rows(14).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRPensiun), intPRAR) = True) Then
                tlbStpPR.Rows(14).Visible = True
                lnkStpPR15.Text = strDanaPensiunTag
                blnPRS = True
            Else
                tlbStpPR.Rows(14).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRRelocation), intPRAR) = True) Then
                tlbStpPR.Rows(15).Visible = True
                lnkStpPR16.Text = strRelocationTag
                blnPRS = True
            Else
                tlbStpPR.Rows(15).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PREmployeeEvaluation), intPRAR) = True) Then
                tlbStpPR.Rows(16).Visible = True
                lnkStpPR17.Text = strEmployeeEvaluationTag
                blnPRS = True
            Else
                tlbStpPR.Rows(16).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRStandardEvaluation), intPRAR) = True) Then
                tlbStpPR.Rows(17).Visible = True
                lnkStpPR18.Text = strStandardEvaluationTag
                blnPRS = True
            Else
                tlbStpPR.Rows(17).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRSalaryIncrease), intPRAR) = True) Then
                tlbStpPR.Rows(18).Visible = True
                lnkStpPR19.Text = strSalaryIncreaseTag
                blnPRS = True
            Else
                tlbStpPR.Rows(18).Visible = False
            End If

        Else
            tlbStpPRHead.Visible = False
            tlbStpPR.Visible = False
        End If
		
	            tlbStpPRHead.Visible = blnPRS
                tlbStpPR.Visible = blnPRS

    End Sub



    Private Sub DisableAllHead()

        tlbStpHRHead.Visible = False
        tlbSpc1.Visible = False
        tlbStpPRHead.Visible = False
		tlbStpHRHead_Estate.Visible = False
        tlbSpc3.Visible = False
        tlbStpPRHead_Estate.Visible = False
		tlbStpHRHead_MILL.Visible = False
		tlbStpPRHead_MILL.Visible = False
		
       
    End Sub



    Sub GetEntireLangCap()
        Dim objLangCapDs As New DataSet()
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

            objLangCapVw = New DataView(objLangCapDs.Tables(0))

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=MENU_FASETUP_GET_LANGCAP&errmesg=&redirect=")
        End Try

    End Sub


    Function GetCaption(ByVal pv_TermCode As Integer) As String

        Dim intIndex As Integer

        objLangCapVw.Sort = "TermCode"

        intIndex = objLangCapVw.Find(pv_TermCode)

        If intIndex = -1 Then
            Return ""
        Else
            If intLocType = 4 Then
                Return Trim(objLangCapVw(intIndex).Item("BusinessTermMW"))
            Else
                Return Trim(objLangCapVw(intIndex).Item("BusinessTerm"))
            End If
        End If

    End Function



End Class