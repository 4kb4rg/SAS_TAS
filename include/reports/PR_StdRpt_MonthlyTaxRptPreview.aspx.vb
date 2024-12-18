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

Public Class PR_StdRpt_MonthlyTaxRptPreview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objPRRpt As New agri.PR.clsReport()
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
    Dim intDecimal As Integer
    Dim strRptName As String
    Dim strCompCode As String
    Dim strLocCode As String
    Dim strDivCode As String
    Dim strPhyMonth1 As String
    Dim strPhyYear1 As String
    Dim strPhyMonth2 As String
    Dim strPhyYear2 As String
    Dim strEmpCatCode As String


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

        intDecimal = Request.QueryString("Decimal")

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")

        strAccMonth = Request.QueryString("SelAccMonth")
        strAccYear = Request.QueryString("SelAccYear")
        strRptName = Request.QueryString("RptName")
        strLangCode = Session("SS_LANGCODE")

        strCompCode = Request.QueryString("CompCode")
        strLocCode = Request.QueryString("LocCode")
        strDivCode = Request.QueryString("DivCode")        
        strPhyMonth1 = Request.QueryString("PhyMonth1")
        strPhyYear1 = Request.QueryString("PhyYear1")
        strPhyMonth2 = Request.QueryString("PhyMonth2")
        strPhyYear2 = Request.QueryString("PhyYear2")
        strEmpCatCode = Request.QueryString("EmpCat")

        if len(strPhyMonth1) = 1 then 
            strPhyMonth1 = "0" & strPhyMonth1
        end if
        if len(strPhyMonth2) = 1 then 
            strPhyMonth2 = "0" & strPhyMonth2
        end if



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
        Dim strOpCd As String = ""
        Dim strParamVeh As String
        Dim strVehCode As String
        Dim strParam As String
        Dim ReportID As String = Request.QueryString("RptID")


       strOpCd = "PR_STDRPT_MONTHLYTAXREPORT" & "|" & objPRRpt.mtdGetPRReportTable(objPRRpt.EnumPRReportTable.MonthlyTaxRpt) & Chr(9) & _
                  "GET_REPORT_INFO_BY_REPORTID|SH_REPORT"


        strParam =  strRptName & "|" & _
                    strCompCode & "|" & _
                    strLocCode & "|" & _
                    intDecimal & "|" & _
                    strDivCode & "|" & _
                    strPhyMonth1 & "|" & _
                    strPhyYear1 & "|" & _
                    strPhyMonth2 & "|" & _
                    strPhyYear2 & "|" & _
                    strEmpCatCode & "|" & _
                    ReportID     


        Try
            intErrNo = objPRRpt.mtdGetReport_MonthlyTaxRpt(strOpCd, _
                                                           strCompany, _
                                                           strLocation, _
                                                           strUserId, _
                                                           strAccMonth, _
                                                           strAccYear, _
                                                           strParam, objRptDs, objMapPath)           
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PR_STDRPT_MONTHLYTAXRPT&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\PR_StdRpt_MonthlyTaxRpt.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))


        crvView.Visible = False                          
        crvView.ReportSource = rdCrystalViewer           
        crvView.DataBind()                              

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\PR_StdRpt_MonthlyTaxRpt.pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/PR_StdRpt_MonthlyTaxRpt.pdf"">")
    End Sub

    Sub PassParam()
        Dim paramFields As New ParameterFields()
        Dim paramField1 As New ParameterField()
        Dim paramField2 As New ParameterField()
        Dim paramField3 As New ParameterField()
        Dim paramField4 As New ParameterField()
        Dim paramField5 As New ParameterField()

        Dim ParamDiscreteValue1 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue2 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue3 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue4 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue5 As New ParameterDiscreteValue()
        
        Dim crParameterValues1 As ParameterValues
        Dim crParameterValues2 As ParameterValues
        Dim crParameterValues3 As ParameterValues
        Dim crParameterValues4 As ParameterValues
        Dim crParameterValues5 As ParameterValues
        
        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("RptId")
        paramField2 = paramFields.Item("SelLocCode")       
        paramField3 = paramFields.Item("ParamUserName")
        paramField4 = paramFields.Item("Period")
        paramField5 = paramFields.Item("SelDecimal")
        
        crParameterValues1 = paramField1.CurrentValues
        crParameterValues2 = paramField2.CurrentValues
        crParameterValues3 = paramField3.CurrentValues
        crParameterValues4 = paramField4.CurrentValues
        crParameterValues5 = paramField5.CurrentValues
        
        ParamDiscreteValue1.Value = Request.QueryString("RptID")
        ParamDiscreteValue2.Value = Request.QueryString("Location")
        ParamDiscreteValue3.Value = Session("SS_USERNAME")
        ParamDiscreteValue4.Value = strAccMonth & "/" & strAccYear
        ParamDiscreteValue5.Value = intDecimal

        crParameterValues1.Add(ParamDiscreteValue1)
        crParameterValues2.Add(ParamDiscreteValue2)
        crParameterValues3.Add(ParamDiscreteValue3)
        crParameterValues4.Add(ParamDiscreteValue4)
        crParameterValues5.Add(ParamDiscreteValue5)
        
        PFDefs(0).ApplyCurrentValues(crParameterValues1)
        PFDefs(1).ApplyCurrentValues(crParameterValues2)
        PFDefs(2).ApplyCurrentValues(crParameterValues3)
        PFDefs(3).ApplyCurrentValues(crParameterValues4)
        PFDefs(4).ApplyCurrentValues(crParameterValues5)
        
        crvView.ParameterFieldInfo = paramFields
    End Sub
End Class
