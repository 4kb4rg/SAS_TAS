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

Public Class PR_StdRpt_JamsostekPreview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objPRRpt As New agri.PR.clsReport()
    Dim objHRReport As New agri.HR.clsReport()
    Dim objHR As New agri.HR.clsSetup()

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
    Dim intDecimal As Integer

    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer

    Sub Page_Init(ByVal Sender As Object, ByVal E As EventArgs)
        If Trim(Request.QueryString("TransferInd")) = "1" Then
            Response.Write("Transferred to MS Excel file (" & Trim(Request.QueryString("RptName")) & ".xls)")
        End If
    End Sub
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

        intDecimal = Request.QueryString("Decimal")

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")

        strAccMonth = Request.QueryString("SelAccMonth")
        strAccYear = Request.QueryString("SelAccYear")
        strLangCode = Session("SS_LANGCODE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            BindReport()
        End If
    End Sub

    Sub BindReport()
        Dim objRptDs As New DataSet()
        Dim objVehicle As New DataSet()
        Dim objMapPath As String
        Dim intCnt As Integer
        Dim intCntRem As Integer
        Dim SearchStr As String
        Dim itemSelectStr As String
        Dim vehSelectStr As String
        Dim blkSelectStr As String
        Dim itemSearchStr As String
        Dim vehSearchStr As String
        Dim blkSearchStr As String
        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim strOpCdJamsostekListGet As String = "PR_STDRPT_JAMSOSTEK_LIST"
        Dim strParamVeh As String
        Dim strVehCode As String
        Dim strParam As String
        Dim strAccMonthFrom As String = Request.QueryString("SelAccMonthFrom")
        Dim strAccYearFrom As String = Request.QueryString("SelAccYearFrom")
        Dim strAccMonthTo As String = Request.QueryString("SelAccMonthTo")
        Dim strAccYearTo As String = Request.QueryString("SelAccYearTo")
        Dim ReportID As String = Request.QueryString("RptID")
        Dim strSKU As String = Trim(Request.QueryString("SKU"))
        Dim strStaff As String = Trim(Request.QueryString("Staff"))

        SearchStr = ""       
        If strSKU = "1" Then
             SearchStr = SearchStr & " and ss.categorytypeind in ('" & objHR.EnumCategoryType.SKUB & "','" & objHR.EnumCategoryType.SKUH & "')"
        End If
        If strStaff = "1" Then
             SearchStr = SearchStr & " and ss.categorytypeind in ('" & objHR.EnumCategoryType.Staff  & "','" & objHR.EnumCategoryType.NonStaff  & "')"
        End If
        If strUserLoc <> "" Then
             SearchStr = SearchStr & " AND e.LOCCODE in ('" & strUserLoc & "')"
        End If
        strParam = SearchStr & "|" & ReportID  & "|" & strAccMonthFrom  & "|" & strAccYearFrom  & "|" & strAccMonthTo  & "|" & strAccYearTo                                     

        Try
            intErrNo = objPRRpt.mtdGetReport_JamsostekList(strOpCdJamsostekListGet, _
                                                           strCompany, _
                                                           strLocation, _
                                                           strUserId, _
                                                           strAccMonth, _
                                                           strAccYear, _
                                                           strParam, objRptDs, objMapPath)           
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PR_STDRPT_Jamsostek_LIST_REPORT&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\PR_StdRpt_JamsostekListing.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))


        crvView.Visible = False                          
        crvView.ReportSource = rdCrystalViewer           
        crvView.DataBind()                              

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()

        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\PR_StdRpt_JamsostekListing.pdf"
       

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile

            .ExportFormatType = ExportFormatType.PortableDocFormat

        End With      
        rdCrystalViewer.Export()

        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/PR_StdRpt_JamsostekListing.pdf"">")
       
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
        
        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("ReportName")
        paramField2 = paramFields.Item("RptId")
        paramField3 = paramFields.Item("lblLocation")
        paramField4 = paramFields.Item("SelLocCode")       
        paramField5 = paramFields.Item("CompName")
        paramField6 = paramFields.Item("ParamUserName")
        paramField7 = paramFields.Item("Period")
        paramField8 = paramFields.Item("SelDecimal")
        paramField9 = paramFields.Item("PeriodTo")
        
        crParameterValues1 = paramField1.CurrentValues
        crParameterValues2 = paramField2.CurrentValues
        crParameterValues3 = paramField3.CurrentValues
        crParameterValues4 = paramField4.CurrentValues
        crParameterValues5 = paramField5.CurrentValues
        crParameterValues6 = paramField6.CurrentValues
        crParameterValues7 = paramField7.CurrentValues
        crParameterValues8 = paramField8.CurrentValues
        crParameterValues9 = paramField9.CurrentValues
        
        ParamDiscreteValue1.Value = Request.QueryString("RptName")
        ParamDiscreteValue2.Value = Request.QueryString("RptID")
        ParamDiscreteValue3.Value = Request.QueryString("lblLocation")
        ParamDiscreteValue4.Value = Session("SS_LOC")        
        ParamDiscreteValue5.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue6.Value = Session("SS_USERNAME")
        ParamDiscreteValue7.Value = Request.QueryString("SelAccMonthFrom") & "/" & Request.QueryString("SelAccYearFrom")
        ParamDiscreteValue8.Value = intDecimal
        ParamDiscreteValue9.Value = Request.QueryString("SelAccMonthTo") & "/" & Request.QueryString("SelAccYearTo")

        crParameterValues1.Add(ParamDiscreteValue1)
        crParameterValues2.Add(ParamDiscreteValue2)
        crParameterValues3.Add(ParamDiscreteValue3)
        crParameterValues4.Add(ParamDiscreteValue4)
        crParameterValues5.Add(ParamDiscreteValue5)
        crParameterValues6.Add(ParamDiscreteValue6)
        crParameterValues7.Add(ParamDiscreteValue7)
        crParameterValues8.Add(ParamDiscreteValue8)
        crParameterValues9.Add(ParamDiscreteValue9)
        
        PFDefs(0).ApplyCurrentValues(crParameterValues1)
        PFDefs(1).ApplyCurrentValues(crParameterValues2)
        PFDefs(2).ApplyCurrentValues(crParameterValues3)
        PFDefs(3).ApplyCurrentValues(crParameterValues4)
        PFDefs(4).ApplyCurrentValues(crParameterValues5)
        PFDefs(5).ApplyCurrentValues(crParameterValues6)
        PFDefs(6).ApplyCurrentValues(crParameterValues7)
        PFDefs(7).ApplyCurrentValues(crParameterValues8)
        PFDefs(8).ApplyCurrentValues(crParameterValues9)
        
        crvView.ParameterFieldInfo = paramFields
    End Sub
End Class
