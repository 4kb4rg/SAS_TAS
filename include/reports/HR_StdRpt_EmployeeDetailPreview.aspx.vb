Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information
Imports System.Xml
Imports System.Web.Services
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web

Public Class HR_StdRpt_EmployeeDetailPreview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objIN As New agri.IN.clsReport()
    Dim objINTrx As New agri.IN.clsTrx()
    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objHRTrx As New agri.HR.clsTrx()
    Dim objHRReport As New agri.HR.clsReport()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strUserLoc As String
    Dim tempLoc As String

    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        If Right(Request.QueryString("Location"), 1) = "," Then
            strUserLoc = Left(Request.QueryString("Location"), Len(Request.QueryString("Location")) - 1)
        Else
            strUserLoc = Trim(Request.QueryString("Location"))
        End If

        tempLoc = Request.QueryString("Location")
        If Right(tempLoc, 1) = "," Then
            Session("SS_LOC") = Left(tempLoc, Len(tempLoc) - 1)
        Else
            Session("SS_LOC") = tempLoc.Replace("','", ", ")
        End If

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        strLangCode = Session("SS_LANGCODE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            BindReport()
        End If
    End Sub

    Sub BindReport()

        Dim objRptDs As New DataSet()
        Dim objMapPath As String
        Dim SearchStr As String
        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim strOpCdEmpDetailGet As String = "HR_STDRPT_EMPLOYEEDETAIL_GET"
        Dim strParam As String

        Dim strEmployeeCodeFrom As String = Request.QueryString("EmployeeCodeFrom")
        Dim strEmployeeCodeTo As String = Request.QueryString("EmployeeCodeTo")
        Dim strEmployeeName As String = Request.QueryString("EmployeeName")
        Dim strGangCode As String = Request.QueryString("GangCode")
        Dim strGender As String = Request.QueryString("Gender")
        Dim strMaritalStatus As String = Request.QueryString("MaritalStatus")
        Dim strNationality As String = Request.QueryString("Nationality")
        Dim strRace As String = Request.QueryString("Race")
        Dim strReligion As String = Request.QueryString("Religion")
        Dim strDOBFrom As String = Request.QueryString("DOBFrom")
        Dim strDOBTo As String = Request.QueryString("DOBTo")
        Dim strStatus As String = Request.QueryString("Status")
        Dim strRptFileName = "HR_StdRpt_EmployeeDetail"

        SearchStr = ""
        
        If Not (strEmployeeCodeFrom = "" And strEmployeeCodeTo = "") Then
            SearchStr = SearchStr & " AND EMP.EmpCode >= '" & strEmployeeCodeFrom & "' AND EMP.EmpCode <= '" & strEmployeeCodeTo & "'" & vbCrLf
        End If

        If Not strEmployeeName = "" Then
            SearchStr = SearchStr & " AND EMP.EmpName LIKE '" & strEmployeeName & "%'" & vbCrLf
        End If

        If Not strGangCode = "" Then
            SearchStr = SearchStr & " AND GANG.GangCode = '" & strGangCode & "'" & vbCrLf
        End If

        If Not strGender = "" Then
            SearchStr = SearchStr & " AND EMP.Gender = '" & strGender & "'" & vbCrLf
        End If

        If Not strMaritalStatus = "" Then
            SearchStr = SearchStr & " AND EMP.MaritalStatus = '" & strMaritalStatus & "'" & vbCrLf
        End If

        If Not strNationality = "" Then
            SearchStr = SearchStr & " AND EMP.Nation = '" & strNationality & "'" & vbCrLf
        End If

        If Not strRace = "" Then
            SearchStr = SearchStr & " AND EMP.Race = '" & strRace & "'" & vbCrLf
        End If

        If Not strReligion = "" Then
            SearchStr = SearchStr & " AND EMP.Religion = '" & strReligion & "'" & vbCrLf
        End If

        If Not (strDOBFrom = "" And strDOBTo = "") Then
            SearchStr = SearchStr & " AND EMP.DOB >= '" & strDOBFrom & "' AND EMP.DOB <= '" & strDOBTo & "'" & vbCrLf
        End If
        
        If Not strStatus = "" Then
            If Not strStatus = objHRTrx.EnumEmpStatus.All Then
                SearchStr = SearchStr & " AND EMP.Status = '" & strStatus & "'" & vbCrLf
            End If
        End If

        strParam = strUserLoc & "|" & SearchStr

        Try
            intErrNo = objHRReport.mtdGetReport_HRGeneral(strOpCdEmpDetailGet, strParam, objRptDs, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_EMPLOYEE_DETAIL_REPORT&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strRptFileName & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        crvView.Visible = False                          
        crvView.ReportSource = rdCrystalViewer           
        crvView.DataBind()                              

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strRptFileName & ".pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
	 rdCrystalViewer.Close()
        rdCrystalViewer.Dispose()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strRptFileName & ".pdf"">")
    End Sub

    Sub PassParam()
        Dim paramFields As New ParameterFields()
        Dim paramField1 As New ParameterField()
        Dim paramField2 As New ParameterField()
        Dim paramField3 As New ParameterField()
        Dim paramField4 As New ParameterField()
        Dim paramField5 As New ParameterField()
        Dim paramField6 As New ParameterField()
        Dim paramField7 As New ParameterField()
        Dim paramField8 As New ParameterField()
        Dim paramField9 As New ParameterField()
        Dim paramField10 As New ParameterField()
        Dim paramField11 As New ParameterField()
        Dim paramField12 As New ParameterField()
        Dim paramField13 As New ParameterField()
        Dim paramField14 As New ParameterField()
        Dim paramField15 As New ParameterField()
        Dim paramField16 As New ParameterField()
        Dim paramField17 As New ParameterField()
        Dim paramField18 As New ParameterField()

        Dim ParamDiscreteValue1 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue2 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue3 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue4 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue5 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue6 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue7 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue8 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue9 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue10 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue11 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue12 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue13 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue14 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue15 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue16 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue17 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue18 As New ParameterDiscreteValue()
        
        Dim crParameterValues1 As ParameterValues
        Dim crParameterValues2 As ParameterValues
        Dim crParameterValues3 As ParameterValues
        Dim crParameterValues4 As ParameterValues
        Dim crParameterValues5 As ParameterValues
        Dim crParameterValues6 As ParameterValues
        Dim crParameterValues7 As ParameterValues
        Dim crParameterValues8 As ParameterValues
        Dim crParameterValues9 As ParameterValues
        Dim crParameterValues10 As ParameterValues
        Dim crParameterValues11 As ParameterValues
        Dim crParameterValues12 As ParameterValues
        Dim crParameterValues13 As ParameterValues
        Dim crParameterValues14 As ParameterValues
        Dim crParameterValues15 As ParameterValues
        Dim crParameterValues16 As ParameterValues
        Dim crParameterValues17 As ParameterValues
        Dim crParameterValues18 As ParameterValues
        
        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("ReportName")
        paramField2 = paramFields.Item("RptId")
        paramField3 = paramFields.Item("lblLocation")
        paramField4 = paramFields.Item("SelLocCode")
        paramField5 = paramFields.Item("EmployeeCodeFrom")
        paramField6 = paramFields.Item("EmployeeCodeTo")
        paramField7 = paramFields.Item("EmployeeName")
        paramField8 = paramFields.Item("Status")
        paramField9 = paramFields.Item("CompName")
        paramField10 = paramFields.Item("ParamUserName")
        paramField11 = paramFields.Item("Gang")
        paramField12 = paramFields.Item("Gender")
        paramField13 = paramFields.Item("MaritalStatus")
        paramField14 = paramFields.Item("Nationality")
        paramField15 = paramFields.Item("Race")
        paramField16 = paramFields.Item("Religion")
        paramField17 = paramFields.Item("DOBFrom")
        paramField18 = paramFields.Item("DOBTo")
        
        crParameterValues1 = paramField1.CurrentValues
        crParameterValues2 = paramField2.CurrentValues
        crParameterValues3 = paramField3.CurrentValues
        crParameterValues4 = paramField4.CurrentValues
        crParameterValues5 = paramField5.CurrentValues
        crParameterValues6 = paramField6.CurrentValues
        crParameterValues7 = paramField7.CurrentValues
        crParameterValues8 = paramField8.CurrentValues
        crParameterValues9 = paramField9.CurrentValues
        crParameterValues10 = paramField10.CurrentValues
        crParameterValues11 = paramField11.CurrentValues
        crParameterValues12 = paramField12.CurrentValues
        crParameterValues13 = paramField13.CurrentValues
        crParameterValues14 = paramField14.CurrentValues
        crParameterValues15 = paramField15.CurrentValues
        crParameterValues16 = paramField16.CurrentValues
        crParameterValues17 = paramField17.CurrentValues
        crParameterValues18 = paramField18.CurrentValues
        
        ParamDiscreteValue1.Value = Request.QueryString("RptName")
        ParamDiscreteValue2.Value = Request.QueryString("RptID")
        ParamDiscreteValue3.Value = Request.QueryString("lblLocation")
        ParamDiscreteValue4.Value = Session("SS_LOC")
        ParamDiscreteValue5.Value = Request.QueryString("EmployeeCodeFrom")
        ParamDiscreteValue6.Value = Request.QueryString("EmployeeCodeTo")
        ParamDiscreteValue7.Value = Request.QueryString("EmployeeName")
        ParamDiscreteValue8.Value = Request.QueryString("Status") 'objHRSetup.mtdGetBonusStatus(Request.QueryString("Status"))
        ParamDiscreteValue9.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue10.Value = Session("SS_USERNAME")
        ParamDiscreteValue11.Value = Request.QueryString("GangCode")
        If Request.QueryString("Gender") = "" Then
            ParamDiscreteValue12.Value = "All"
        Else
            ParamDiscreteValue12.Value = objHRTrx.mtdGetSex(Request.QueryString("Gender"))
        End If
        
        If Request.QueryString("MaritalStatus") = "" Then
            ParamDiscreteValue13.Value = "All"
        Else
            ParamDiscreteValue13.Value = objHRTrx.mtdGetMarital(Request.QueryString("MaritalStatus"))
        End If
        ParamDiscreteValue14.Value = Request.QueryString("Nationality")
        ParamDiscreteValue15.Value = Request.QueryString("Race")
        ParamDiscreteValue16.Value = Request.QueryString("Religion")
        ParamDiscreteValue17.Value = Request.QueryString("DOBFrom")
        ParamDiscreteValue18.Value = Request.QueryString("DOBTo")

        crParameterValues1.Add(ParamDiscreteValue1)
        crParameterValues2.Add(ParamDiscreteValue2)
        crParameterValues3.Add(ParamDiscreteValue3)
        crParameterValues4.Add(ParamDiscreteValue4)
        crParameterValues5.Add(ParamDiscreteValue5)
        crParameterValues6.Add(ParamDiscreteValue6)
        crParameterValues7.Add(ParamDiscreteValue7)
        crParameterValues8.Add(ParamDiscreteValue8)
        crParameterValues9.Add(ParamDiscreteValue9)
        crParameterValues10.Add(ParamDiscreteValue10)
        crParameterValues11.Add(ParamDiscreteValue11)
        crParameterValues12.Add(ParamDiscreteValue12)
        crParameterValues13.Add(ParamDiscreteValue13)
        crParameterValues14.Add(ParamDiscreteValue14)
        crParameterValues15.Add(ParamDiscreteValue15)
        crParameterValues16.Add(ParamDiscreteValue16)
        crParameterValues17.Add(ParamDiscreteValue17)
        crParameterValues18.Add(ParamDiscreteValue18)
        
        PFDefs(0).ApplyCurrentValues(crParameterValues1)
        PFDefs(1).ApplyCurrentValues(crParameterValues2)
        PFDefs(2).ApplyCurrentValues(crParameterValues3)
        PFDefs(3).ApplyCurrentValues(crParameterValues4)
        PFDefs(4).ApplyCurrentValues(crParameterValues5)
        PFDefs(5).ApplyCurrentValues(crParameterValues6)
        PFDefs(6).ApplyCurrentValues(crParameterValues7)
        PFDefs(7).ApplyCurrentValues(crParameterValues8)
        PFDefs(8).ApplyCurrentValues(crParameterValues9)
        PFDefs(9).ApplyCurrentValues(crParameterValues10)
        PFDefs(10).ApplyCurrentValues(crParameterValues11)
        PFDefs(11).ApplyCurrentValues(crParameterValues12)
        PFDefs(12).ApplyCurrentValues(crParameterValues13)
        PFDefs(13).ApplyCurrentValues(crParameterValues14)
        PFDefs(14).ApplyCurrentValues(crParameterValues15)
        PFDefs(15).ApplyCurrentValues(crParameterValues16)
        PFDefs(16).ApplyCurrentValues(crParameterValues17)
        PFDefs(17).ApplyCurrentValues(crParameterValues18)
        
        crvView.ParameterFieldInfo = paramFields
    End Sub
End Class
