Imports System 
Imports System.Data
Imports System.Collections 
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports System.Xml
Imports System.Web.Services
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web

Public Class PR_StdRpt_EPFBorangA_Preview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objAdminComp As New agri.Admin.clsComp()
    Dim objHR As New agri.HR.clsTrx()
    Dim objPRRpt As New agri.PR.clsReport()
    Dim objAdmAcc As New agri.Admin.clsAccPeriod()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth as String
    Dim strAccYear as String
    Dim strLangCode As String
    Dim intConfigsetting As Integer

    Dim strAccPeriod As String
    Dim strUserLoc As String
    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim intSelDecimal As Integer
    Dim strChequeNo As String
    Dim strEmpCode As String
    Dim strSignName As String
    Dim strSignNRIC As String
    Dim strSignDesignation As String
    Dim strAddress As String
    Dim strTelNo As String
    Dim strEPFRef As String

    Dim intCnt As Integer

    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer
    Dim intCount As Integer

    Sub Page_Load(Sender As Object, E As EventArgs)
        crvView.Visible = False  
 
        strUserLoc = Trim(Request.QueryString("Location"))
        strSelAccMonth = Request.QueryString("ddlAccMth")
        strSelAccYear = Request.QueryString("ddlAccYr")
        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")

        intSelDecimal = Convert.ToInt16(Request.QueryString("Decimal"))
        strChequeNo = Trim(Request.QueryString("ChequeNo"))
        strEmpCode = Trim(Request.QueryString("EmpCode"))
        strSignName = Trim(Request.QueryString("SignName"))
        strSignNRIC = Trim(Request.QueryString("SignNRIC"))
        strSignDesignation = Trim(Request.QueryString("SignDesignation"))

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            GetCompanyAddress()
            BindReport()
        End if
    End Sub

    Sub GetCompanyAddress()
        Dim objCompDs As New Object()
        Dim strOpCd_Get As String = "ADMIN_CLSCOMP_COMPANY_LIST_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer

        strParam = strCompany & "||" & objAdminComp.EnumCompanyStatus.Active & "||CompCode|" 

        Try
            intErrNo = objAdminComp.mtdGetComp(strOpCd_Get, strParam, objCompDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_GET_COMPANY&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objCompDs.Tables(0).Rows.Count > 0 Then
            strAddress = objCompDs.Tables(0).Rows(0).Item("Address").Trim()
            strTelNo = objCompDs.Tables(0).Rows(0).Item("TelNo").Trim()
            strEPFRef = objCompDs.Tables(0).Rows(0).Item("EPFRef").Trim()
        End If
    End Sub

    Sub BindReport()
        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim strOpCd As String
        Dim objRptDs As New DataSet()
        Dim objAccCfg As New Dataset()
        Dim objMapPath As String
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strRptPrefix As String
        Dim strReportID As String
        Dim tempLoc As String
        Dim intNextAccMonth As Integer
        Dim intNextAccYear As Integer
        Dim strOpCd_AccCfg_Get As String = "ADMIN_CLSACCPERIOD_CONFIG_ACCYEAR_GET"
        Dim intMaxPeriod As Integer

        Try
        strParam = "||" & Convert.ToString(strSelAccYear)
        intErrNo = objAdmAcc.mtdGetAccPeriodCfg(strOpCd_AccCfg_Get, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                objAccCfg)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_STDRPT_EPFBORANGA_ACCCFG_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        Try
            intMaxPeriod = Convert.ToInt16(objAccCfg.Tables(0).Rows(0).Item("MaxPeriod"))
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_STDRPT_EPFBORANGA_ACCCFG_MAXPERIOD&errmesg=System required period configuration to process your request. Please set period configuration for the year of " & strSelAccYear & "&redirect=")
        End Try
        objAccCfg = Nothing



        If (Convert.ToInt16(strSelAccMonth) > intMaxPeriod) Then
            intNextAccMonth = 1
            intNextAccYear = Convert.ToInt16(strSelAccYear) + 1            
        Else
            intNextAccMonth = Convert.ToInt16(strSelAccMonth) + 1
            intNextAccYear = Convert.ToInt16(strSelAccYear)
        End If

        objPRRpt.GetCharPeriod(Convert.ToString(intNextAccMonth), Convert.ToString(intNextAccYear), strAccPeriod)

        strReportID = "RPTPR1000020"

        If Right(strUserLoc, 1) = "," Then
            Session("SS_LOC") = Left(strUserLoc, Len(strUserLoc) - 1)
        Else
            Session("SS_LOC") = strUserLoc
        End If

        Try
            strRptPrefix = "PR_StdRpt_EPFBorangA"

            strOpCd = "PR_STDRPT_EPFBORANGA" & "|" & objPRRpt.mtdGetPRReportTable(objPRRpt.EnumPRReportTable.EPFBorangA) & Chr(9) & _
                      "GET_REPORT_INFO_BY_REPORTID|SH_REPORT"

            strParam = strSelAccMonth & "|" & _
                       strSelAccYear & "|" & _
                       strUserLoc & "|" & _
                       strReportID & "|"

            intErrNo = objPRRpt.mtdGetReport_Statutory(strOpCd, _
                                                       strCompany, _
                                                       strLocation, _
                                                       strUserId, _
                                                       strAccMonth, _
                                                       strAccYear, _
                                                       strParam, _
                                                       objRptDs, _
                                                       objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PR_STDRPT_EPFBORANGA_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try


        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strRptPrefix & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA4

        crvView.Visible = False                          
        crvView.ReportSource = rdCrystalViewer           
        crvView.DataBind()                               

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strRptPrefix & ".pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strRptPrefix & ".pdf"">")

        objRptDs = Nothing
        objMapPath = Nothing
    End Sub

    Sub PassParam()
        Dim crParameterFieldDefinitions As ParameterFieldDefinitions
        Dim crParameterFieldDefinition1 As ParameterFieldDefinition
        Dim crParameterFieldDefinition2 As ParameterFieldDefinition
        Dim crParameterFieldDefinition3 As ParameterFieldDefinition
        Dim crParameterFieldDefinition4 As ParameterFieldDefinition
        Dim crParameterFieldDefinition5 As ParameterFieldDefinition
        Dim crParameterFieldDefinition6 As ParameterFieldDefinition
        Dim crParameterFieldDefinition7 As ParameterFieldDefinition
        Dim crParameterFieldDefinition8 As ParameterFieldDefinition
        Dim crParameterFieldDefinition9 As ParameterFieldDefinition
        Dim crParameterFieldDefinition10 As ParameterFieldDefinition
        Dim crParameterFieldDefinition11 As ParameterFieldDefinition

        Dim crParameterValues1 As New ParameterValues()
        Dim crParameterValues2 As New ParameterValues()
        Dim crParameterValues3 As New ParameterValues()
        Dim crParameterValues4 As New ParameterValues()
        Dim crParameterValues5 As New ParameterValues()
        Dim crParameterValues6 As New ParameterValues()
        Dim crParameterValues7 As New ParameterValues()
        Dim crParameterValues8 As New ParameterValues()
        Dim crParameterValues9 As New ParameterValues()
        Dim crParameterValues10 As New ParameterValues()
        Dim crParameterValues11 As New ParameterValues()

        Dim crParameterDiscreteValue1 As New ParameterDiscreteValue()
        Dim crParameterDiscreteValue2 As New ParameterDiscreteValue()
        Dim crParameterDiscreteValue3 As New ParameterDiscreteValue()
        Dim crParameterDiscreteValue4 As New ParameterDiscreteValue()
        Dim crParameterDiscreteValue5 As New ParameterDiscreteValue()
        Dim crParameterDiscreteValue6 As New ParameterDiscreteValue()
        Dim crParameterDiscreteValue7 As New ParameterDiscreteValue()
        Dim crParameterDiscreteValue8 As New ParameterDiscreteValue()
        Dim crParameterDiscreteValue9 As New ParameterDiscreteValue()
        Dim crParameterDiscreteValue10 As New ParameterDiscreteValue()
        Dim crParameterDiscreteValue11 As New ParameterDiscreteValue()
       
        crParameterDiscreteValue1.Value = UCASE(strAccPeriod)
        crParameterDiscreteValue2.Value = strChequeNo
        crParameterDiscreteValue3.Value = strCompanyName 
        crParameterDiscreteValue4.Value = strSignName 
        crParameterDiscreteValue5.Value = strSignNRIC 
        crParameterDiscreteValue6.Value = strSignDesignation 
        crParameterDiscreteValue7.Value = intSelDecimal 
        crParameterDiscreteValue8.Value = strAddress  
        crParameterDiscreteValue9.Value = strEPFRef
        crParameterDiscreteValue10.Value = strTelNo 
        crParameterDiscreteValue11.Value = RIGHT("00" & TRIM(strSelAccMonth),2) 

        crParameterFieldDefinitions = rdCrystalViewer.DataDefinition.ParameterFields
        crParameterFieldDefinition1 = crParameterFieldDefinitions.Item("ParamAccPeriod")
        crParameterFieldDefinition2 = crParameterFieldDefinitions.Item("ParamChequeNo")
        crParameterFieldDefinition3 = crParameterFieldDefinitions.Item("ParamCompanyName")
        crParameterFieldDefinition4 = crParameterFieldDefinitions.Item("ParamSignName")
        crParameterFieldDefinition5 = crParameterFieldDefinitions.Item("ParamSignNRIC")
        crParameterFieldDefinition6 = crParameterFieldDefinitions.Item("ParamSignDesignation")
        crParameterFieldDefinition7 = crParameterFieldDefinitions.Item("ParamSelDecimal")
        crParameterFieldDefinition8 = crParameterFieldDefinitions.Item("ParamAddress")
        crParameterFieldDefinition9 = crParameterFieldDefinitions.Item("ParamEPFNo")
        crParameterFieldDefinition10 = crParameterFieldDefinitions.Item("ParamTelNo")
        crParameterFieldDefinition11 = crParameterFieldDefinitions.Item("ParamAccMonth")

        crParameterValues1 = crParameterFieldDefinition1.CurrentValues
        crParameterValues2 = crParameterFieldDefinition2.CurrentValues
        crParameterValues3 = crParameterFieldDefinition3.CurrentValues
        crParameterValues4 = crParameterFieldDefinition4.CurrentValues
        crParameterValues5 = crParameterFieldDefinition5.CurrentValues
        crParameterValues6 = crParameterFieldDefinition6.CurrentValues
        crParameterValues7 = crParameterFieldDefinition7.CurrentValues
        crParameterValues8 = crParameterFieldDefinition8.CurrentValues
        crParameterValues9 = crParameterFieldDefinition9.CurrentValues
        crParameterValues10 = crParameterFieldDefinition10.CurrentValues
        crParameterValues11 = crParameterFieldDefinition11.CurrentValues

        crParameterValues1.Add(crParameterDiscreteValue1)
        crParameterValues2.Add(crParameterDiscreteValue2)
        crParameterValues3.Add(crParameterDiscreteValue3)
        crParameterValues4.Add(crParameterDiscreteValue4)
        crParameterValues5.Add(crParameterDiscreteValue5)
        crParameterValues6.Add(crParameterDiscreteValue6)
        crParameterValues7.Add(crParameterDiscreteValue7)
        crParameterValues8.Add(crParameterDiscreteValue8)
        crParameterValues9.Add(crParameterDiscreteValue9)
        crParameterValues10.Add(crParameterDiscreteValue10)
        crParameterValues11.Add(crParameterDiscreteValue11)

        crParameterFieldDefinition1.ApplyCurrentValues(crParameterValues1)
        crParameterFieldDefinition2.ApplyCurrentValues(crParameterValues2)
        crParameterFieldDefinition3.ApplyCurrentValues(crParameterValues3)
        crParameterFieldDefinition4.ApplyCurrentValues(crParameterValues4)
        crParameterFieldDefinition5.ApplyCurrentValues(crParameterValues5)
        crParameterFieldDefinition6.ApplyCurrentValues(crParameterValues6)
        crParameterFieldDefinition7.ApplyCurrentValues(crParameterValues7)
        crParameterFieldDefinition8.ApplyCurrentValues(crParameterValues8)
        crParameterFieldDefinition9.ApplyCurrentValues(crParameterValues9)
        crParameterFieldDefinition10.ApplyCurrentValues(crParameterValues10)
        crParameterFieldDefinition11.ApplyCurrentValues(crParameterValues11)
    End Sub

End Class
