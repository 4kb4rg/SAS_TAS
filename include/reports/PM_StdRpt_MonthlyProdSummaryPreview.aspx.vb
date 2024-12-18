Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports System.Xml
Imports System.Web.Services
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web

Public Class PM_StdRpt_MonthlyProdSum_Preview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objPM As New agri.PM.clsReport()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strUserLoc As String

    Dim intDecimal As Integer
    Dim tempLoc As String

    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        crvView.Visible = False

        If Left(Request.QueryString("Location"), 3) = "','" Then
            strUserLoc = Right(Request.QueryString("Location"), Len(Request.QueryString("Location")) - 3)
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
        strAccMonth = Session("SS_PMACCMONTH")
        strAccYear = Session("SS_PMACCYEAR")
        strLangCode = Session("SS_LANGCODE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            BindReport()
        End If
    End Sub

    Sub BindReport()

        Dim objRptDs As New DataSet()
        Dim dsInternal As New DataSet()
        Dim dsAssociate As New DataSet()
        Dim dsExternal As New DataSet()
        Dim dsContractor As New DataSet()
        Dim dsMthlyProd As New DataSet()
        Dim dsCPODisp As New DataSet()
        Dim dsPKDisp As New DataSet()
        Dim objMapPath As String

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOpCd_Monthly_Prod_Sum_FFBReceived_GET As String = "WM_STDRPT_MONTHLY_PROD_SUMMARY_FFBRCV_GET"
        Dim strOpCd_Monthly_Prod_Sum_GET As String = "WM_STDRPT_MONTHLY_PROD_SUMMARY_GET"
        Dim strOpCd_Monthly_Prod_Sum_CPOPKDispatch_GET As String = "WM_STDRPT_MONTHLY_PROD_SUMMARY_CPOPKDISP_GET"
        Dim strFileName As String = "PM_StdRpt_MonthlyProdSummary"

        Dim strParam As String

        strParam = strUserLoc & "|" & Request.QueryString("strddlAccMth") & "|" & Request.QueryString("strddlAccYr") & "|"

        Try
            intErrNo = objPM.mtdGetReport_MonthlyProdSumReport(strOpCd_Monthly_Prod_Sum_FFBReceived_GET, _
                                                               strOpCd_Monthly_Prod_Sum_GET, _
                                                               strOpCd_Monthly_Prod_Sum_CPOPKDispatch_GET, _
                                                               strParam, _
                                                               dsInternal, _
                                                               dsAssociate, _
                                                               dsExternal, _
                                                               dsContractor, _
                                                               dsMthlyProd, _
                                                               dsCPODisp, _
                                                               dsPKDisp, _
                                                               objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PM_STDRPT_MONTHLY_PROD_SUM&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        objRptDs.Tables.Add(dsInternal.Tables(0).Copy())
        objRptDs.Tables(0).TableName = "PM_MONTHLY_PROD_SUM_INT"
        objRptDs.Tables.Add(dsAssociate.Tables(0).Copy())
        objRptDs.Tables(1).TableName = "PM_MONTHLY_PROD_SUM_ASS"
        objRptDs.Tables.Add(dsExternal.Tables(0).Copy())
        objRptDs.Tables(2).TableName = "PM_MONTHLY_PROD_SUM_EXT"
        objRptDs.Tables.Add(dsContractor.Tables(0).Copy())
        objRptDs.Tables(3).TableName = "PM_MONTHLY_PROD_SUM_CON"
        objRptDs.Tables.Add(dsMthlyProd.Tables(0).Copy())
        objRptDs.Tables(4).TableName = "PM_MONTHLY_PROD_SUM"
        objRptDs.Tables.Add(dsCPODisp.Tables(0).Copy())
        objRptDs.Tables(5).TableName = "PM_MONTHLY_PROD_SUM_CPODISP"
        objRptDs.Tables.Add(dsPKDisp.Tables(0).Copy())
        objRptDs.Tables(6).TableName = "PM_MONTHLY_PROD_SUM_PKDISP"


        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strFileName & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs)

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Portrait
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA4

        crvView.Visible = False                         
        crvView.ReportSource = rdCrystalViewer          
        crvView.DataBind()                              

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strFileName & ".pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strFileName & ".pdf"">")
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

        Dim ParamDiscreteValue1 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue2 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue3 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue4 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue5 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue6 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue7 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue8 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue9 As New ParameterDiscreteValue()

        Dim crParameterValues1 As ParameterValues
        Dim crParameterValues2 As ParameterValues
        Dim crParameterValues3 As ParameterValues
        Dim crParameterValues4 As ParameterValues
        Dim crParameterValues5 As ParameterValues
        Dim crParameterValues6 As ParameterValues
        Dim crParameterValues7 As ParameterValues
        Dim crParameterValues8 As ParameterValues
        Dim crParameterValues9 As ParameterValues

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("ParamCompanyName")
        paramField2 = paramFields.Item("ParamLocation")
        paramField3 = paramFields.Item("ParamUserName")
        paramField4 = paramFields.Item("ParamDecimal")
        paramField5 = paramFields.Item("ParamRptName")
        paramField6 = paramFields.Item("ParamRptID")
        paramField7 = paramFields.Item("ParamAccMonth")
        paramField8 = paramFields.Item("ParamAccYear")
        paramField9 = paramFields.Item("LblLocation")

        crParameterValues1 = paramField1.CurrentValues
        crParameterValues2 = paramField2.CurrentValues
        crParameterValues3 = paramField3.CurrentValues
        crParameterValues4 = paramField4.CurrentValues
        crParameterValues5 = paramField5.CurrentValues
        crParameterValues6 = paramField6.CurrentValues
        crParameterValues7 = paramField7.CurrentValues
        crParameterValues8 = paramField8.CurrentValues
        crParameterValues9 = paramField9.CurrentValues

        ParamDiscreteValue1.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue2.Value = Session("SS_LOC")
        ParamDiscreteValue3.Value = Session("SS_USERNAME")
        ParamDiscreteValue4.Value = Request.QueryString("Decimal")
        ParamDiscreteValue5.Value = Request.QueryString("RptName")
        ParamDiscreteValue6.Value = Request.QueryString("RptID")
        ParamDiscreteValue7.Value = Request.QueryString("strddlAccMth")
        ParamDiscreteValue8.Value = Request.QueryString("strddlAccYr")
        ParamDiscreteValue9.Value = Request.QueryString("lblLocation")

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
