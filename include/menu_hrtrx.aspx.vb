
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




Public Class menu_hrtrx : Inherits Page


    Dim strUserId As String
    Dim strLangCode As String
    Dim strCompany As String
    Dim strLocation As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intLocType As Integer
    Dim intModuleActivate As Integer
    Dim strLocTag As String

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
   

    Protected WithEvents lnkHR01 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkHR02 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkHR03 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkHR04 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkHR05 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkHR06 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkHR07 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkHR08 As System.Web.UI.WebControls.HyperLink
    
    Protected WithEvents tblHRHead As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tblHR As System.Web.UI.HtmlControls.HtmlTable

    Protected WithEvents tblSpc1 As System.Web.UI.HtmlControls.HtmlTable
	Protected WithEvents tblSpc3 As System.Web.UI.HtmlControls.HtmlTable
	
    Protected WithEvents tblPRHead As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tblPR As System.Web.UI.HtmlControls.HtmlTable

	Protected WithEvents TblPR_EstateHead As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tblPR_Estate As System.Web.UI.HtmlControls.HtmlTable
	
	Protected WithEvents TblHR_EstateHead As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tblHR_Estate As System.Web.UI.HtmlControls.HtmlTable
	
	Protected WithEvents TblPR_MILLHead As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tblPR_MILL As System.Web.UI.HtmlControls.HtmlTable
	
	Protected WithEvents TblHR_MILLHead As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tblHR_MILL As System.Web.UI.HtmlControls.HtmlTable
	
    Protected WithEvents lnkPR01 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkPR02 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkPR03 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkPR04 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkPR05 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkPR06 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkPR07 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkPR08 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkPR09 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkPR10 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkPR11 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkPR12 As System.Web.UI.WebControls.HyperLink


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strUserId = Session("SS_USERID")

        intModuleActivate = Session("SS_MODULEACTIVATE")
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strLangCode = Session("SS_LANGCODE")
        intLocType = Session("SS_LOCTYPE")
        strAccMonth = Session("SS_FAACCMONTH")
        strAccYear = Session("SS_FAACCYEAR")

        If strUserId = "" Then
            Response.Write("<Script language=""Javascript"">parent.right.location.href = '/Login.aspx'</Script>")
        Else

	
        intHRAR = Session("SS_HRAR")
        intPRAR = Session("SS_PRAR")
      
        'GetEntireLangCap()
        onLoad_Display()
             
        End If

    End Sub

   Sub onLoad_Display()

        If Session("SS_LOCATION") = "" Or Session("SS_MAXPERIOD") = 0 Then
            Call DisableAllHead()
        Else
		   Call DisableAllHead()
		   tblSpc3.Visible = True
		   IF intLocType=1 then   'only estate location type
		        
				Call TrxHRRight_Estate()
				Call TrxPRRight_Estate()
				
			ELSE
			    Call TrxHRRight_MILL()
				Call TrxPRRight_MILL()
				tblSpc3.Visible = True
				'Call TrxHRRight()
				'Call TrxPRRight()
				'tblSpc1.Visible = True
         	END IF
        End If

    End Sub


    Private Sub DisableAllHead()

        tblHRHead.Visible = False
        tblSpc1.Visible = False
        tblPRHead.Visible = False
		TblHR_EstateHead.Visible = False
		tblSpc3.Visible = False
		TblPR_EstateHead.Visible =False
		TblHR_MILLHead.Visible = False
		TblPR_MILLHead.Visible =False
		
       
    End Sub


    Private Sub TrxPRRight()

        Dim blnPR As Boolean
        blnPR = False

        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Payroll), intModuleActivate) Then

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxRatePayment), intPRAR) = True) Then
                tblPR.Rows(0).Visible = True
                blnPR = True
            Else
                tblPR.Rows(0).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxContractPayment), intPRAR) = True) Then
                tblPR.Rows(1).Visible = True
                blnPR = True
            Else
                tblPR.Rows(1).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxTrip), intPRAR) = True) Then
                tblPR.Rows(2).Visible = True
                blnPR = True
            Else
                tblPR.Rows(2).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxWagesPayment), intPRAR) = True) Then
                tblPR.Rows(3).Visible = True
                blnPR = True
            Else
                tblPR.Rows(3).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxAllowanceDeduction), intPRAR) = True) Then
                tblPR.Rows(4).Visible = True
                blnPR = True
            Else
                tblPR.Rows(4).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxWorkPerformance), intPRAR) = True) Then
                tblPR.Rows(5).Visible = True
                blnPR = True
            Else
                tblPR.Rows(5).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDailyAttd), intPRAR) = True) Then
                tblPR.Rows(6).Visible = True
                blnPR = True
            Else
                tblPR.Rows(6).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRHarvAttd), intPRAR) = True) Then
                tblPR.Rows(7).Visible = True
                blnPR = True
            Else
                tblPR.Rows(7).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRWeekly), intPRAR) = True) Then
                tblPR.Rows(8).Visible = True
                blnPR = True
            Else
                tblPR.Rows(8).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxAttendance), intPRAR) = True) Then
                tblPR.Rows(9).Visible = True
                blnPR = True
            Else
                tblPR.Rows(9).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxContractorCheckroll), intPRAR) = True) Then
                tblPR.Rows(10).Visible = True
                blnPR = True
            Else
                tblPR.Rows(10).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxWPContractor), intPRAR) = True) Then
                tblPR.Rows(11).Visible = True
                blnPR = True
            Else
                tblPR.Rows(11).Visible = False
            End If
        Else
            tblPRHead.Visible = False
            tblPR.Visible = False
        End If

            tblPRHead.Visible = blnPR
            tblPR.Visible = blnPR

    End Sub


    Private Sub TrxHRRight()
        Dim blnHR As Boolean
        blnHR = False

        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.HumanResource), intModuleActivate) Then

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeDetails), intHRAR) = True) Then
                tblHR.Rows(0).Visible = True
                blnHR = True
            Else
                tblHR.Rows(0).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeePayroll), intHRAR) = True) Then
                tblHR.Rows(1).Visible = True
                blnHR = True
            Else
                tblHR.Rows(1).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeEmployement), intHRAR) = True) Then
                tblHR.Rows(2).Visible = True
                blnHR = True
            Else
                tblHR.Rows(2).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeStatutory), intHRAR) = True) Then
                tblHR.Rows(3).Visible = True
                blnHR = True
            Else
                tblHR.Rows(3).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeFamily), intHRAR) = True) Then
                tblHR.Rows(4).Visible = True
                blnHR = True
            Else
                tblHR.Rows(4).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeQualification), intHRAR) = True) Then
                tblHR.Rows(5).Visible = True
                blnHR = True
            Else
                tblHR.Rows(5).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeSkill), intHRAR) = True) Then
                tblHR.Rows(6).Visible = True
                blnHR = True
            Else
                tblHR.Rows(6).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRCareerProgress), intHRAR) = True) Then
                tblHR.Rows(7).Visible = True
                blnHR = True
            Else
                tblHR.Rows(7).Visible = False
            End If          
        Else
            tblHRHead.Visible = False
            tblHR.Visible = False
            tblSpc1.Visible = False
        End If

		        tblHRHead.Visible = blnHR
                tblHR.Visible = blnHR
                tblSpc1.Visible = blnHR
				
    End Sub


	Private Sub TrxHRRight_Estate()
	TblHR_EstateHead.Visible = True
	End Sub
	
	Private Sub TrxPRRight_Estate()
	TblPR_EstateHead.Visible = True
	End Sub
	
	
	Private Sub TrxHRRight_MILL()
	TblHR_MILLHead.Visible = True
	End Sub
	
	Private Sub TrxPRRight_MILL()
	TblPR_MILLHead.Visible = True
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
