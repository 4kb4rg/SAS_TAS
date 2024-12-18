Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information
Imports System.Xml
Imports System.Web.Services
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web

Public Class PR_StdRpt_AdvPaymentPreview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objPRRpt As New agri.PR.clsReport()
    Dim objPRTrx As New agri.PR.clsTrx()

    Dim strCompCode As String
    Dim strCompName As String
    Dim strLocCode As String
    Dim strLocName As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfig As Integer
    Dim strCostLevel As String

    Dim strRptID As String
    Dim strRptTitle As String
    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim intSelDecimal As String
    Dim strDeptCode As String
    Dim strEmpCodeFrom As String
    Dim strEmpCodeTo As String
    Dim strGangCode As String
    Dim strSelLocCode As String
    Dim strLocTag As String
    Dim strBlockTag As String
    Dim strSubBlkTag As String
    
    Dim rdCrystalViewer As New ReportDocument()

    Sub Page_Load(Sender As Object, E As EventArgs)
        crvView.Visible = False

        strCompCode = Session("SS_COMPANY")
        strCompName = Session("SS_COMPANYNAME")  
        strLocCode = Session("SS_LOCATION")
        strLocName = Session("SS_LOCATIONNAME")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfig = Session("SS_CONFIGSETTING")
        strCostLevel = Session("SS_COSTLEVEL")
 
        strRptID = Trim(Request.QueryString("RptID"))
        strRptTitle = Trim(Request.QueryString("RptTitle"))
        strSelLocCode = Trim(Request.QueryString("SelLocCode"))
        strSelAccMonth = Request.QueryString("SelAccMonth")
        strSelAccYear = Request.QueryString("SelAccYEar")
        intSelDecimal = CInt(Request.QueryString("Decimal"))
        strDeptCode = Request.QueryString("DeptCode")
        strEmpCodeFrom = Request.QueryString("EmpCodeFrom")
        strEmpCodeTo = Request.QueryString("EmpCodeTo")
        strGangCode = Request.QueryString("GangCode")
        strLocTag = Request.QueryString("LocationTag")
        strBlockTag = Request.QueryString("BlockTag")
        strSubBlkTag = Request.QueryString("SubBlkTag")


        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            BindReport()
        End if
    End Sub


    Sub BindReport()
        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim strOpCd As String
        Dim objRptDs As New DataSet()
        Dim objMapPath As String
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strRptPrefix As String

        strRptPrefix = "PR_StdRpt_AdvPaymentListing"

        strOpCd = "PR_STDRPT_ADVPAYMENT_GET" 
                                     
        strParam = strSelLocCode & "|" & _
                   strDeptCode & "|" & _
                   strEmpCodeFrom & "|" & _
                   strEmpCodeTo & "|" & _
                   strGangCode & "|" & _
                   strSelAccMonth & "|" & _
                   strSelAccYear 
        

        Try
            intErrNo = objPRRpt.mtdGetReport_AdvPayment(strOpCd, _
                                                       strCompCode, _
                                                       strLocCode, _
                                                       strUserId, _
                                                       strAccMonth, _
                                                       strAccYear, _
                                                       strParam, _
                                                       objRptDs, _
                                                       objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PR_GET_ADVPAYMENT&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try


        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strRptPrefix & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))
        
        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperLetter

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
    End Sub


    Sub PassParam()
        Dim ParamFieldDefs As ParameterFieldDefinitions
        Dim ParamFieldDef1 As ParameterFieldDefinition
        Dim ParamFieldDef2 As ParameterFieldDefinition
        Dim ParamFieldDef3 As ParameterFieldDefinition
        Dim ParamFieldDef4 As ParameterFieldDefinition
        Dim ParamFieldDef5 As ParameterFieldDefinition
        Dim ParamFieldDef6 As ParameterFieldDefinition
        Dim ParamFieldDef7 As ParameterFieldDefinition
        Dim ParamFieldDef8 As ParameterFieldDefinition
        Dim ParamFieldDef9 As ParameterFieldDefinition
        Dim ParamFieldDef10 As ParameterFieldDefinition
        Dim ParamFieldDef11 As ParameterFieldDefinition
        Dim ParamFieldDef12 As ParameterFieldDefinition
        Dim ParamFieldDef13 As ParameterFieldDefinition
        Dim ParamFieldDef14 As ParameterFieldDefinition

        Dim ParameterValues1 As New ParameterValues()
        Dim ParameterValues2 As New ParameterValues()
        Dim ParameterValues3 As New ParameterValues()
        Dim ParameterValues4 As New ParameterValues()
        Dim ParameterValues5 As New ParameterValues()
        Dim ParameterValues6 As New ParameterValues()
        Dim ParameterValues7 As New ParameterValues()
        Dim ParameterValues8 As New ParameterValues()
        Dim ParameterValues9 As New ParameterValues()
        Dim ParameterValues10 As New ParameterValues()
        Dim ParameterValues11 As New ParameterValues()
        Dim ParameterValues12 As New ParameterValues()
        Dim ParameterValues13 As New ParameterValues()
        Dim ParameterValues14 As New ParameterValues()

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

        If InStr(strSelLocCode, "','") > 0 Then
            strSelLocCode = Replace(strSelLocCode, "','", ", ")
        End If

        ParamDiscreteValue1.Value = strRptID
        ParamDiscreteValue2.Value = strRptTitle
        ParamDiscreteValue3.Value = strCompName
        ParamDiscreteValue4.Value = strLocName
        ParamDiscreteValue5.Value = strUserName
        ParamDiscreteValue6.Value = strSelLocCode
        ParamDiscreteValue7.Value = strSelAccMonth
        ParamDiscreteValue8.Value = strSelAccYear
        ParamDiscreteValue9.Value = intSelDecimal
        ParamDiscreteValue10.Value = strDeptCode
        ParamDiscreteValue11.Value = strEmpCodeFrom
        ParamDiscreteValue12.Value = strEmpCodeTo
        ParamDiscreteValue13.Value = strGangCode
        ParamDiscreteValue14.Value = strLocTag

        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        ParamFieldDef1 = ParamFieldDefs.Item("paramRptID")
        ParamFieldDef2 = ParamFieldDefs.Item("paramRptTitle")
        ParamFieldDef3 = ParamFieldDefs.Item("paramCompName")
        ParamFieldDef4 = ParamFieldDefs.Item("paramLocName")
        ParamFieldDef5 = ParamFieldDefs.Item("paramUserName")
        ParamFieldDef6 = ParamFieldDefs.Item("selLocation")
        ParamFieldDef7 = ParamFieldDefs.Item("selAccMonth")
        ParamFieldDef8 = ParamFieldDefs.Item("selAccYear")
        ParamFieldDef9 = ParamFieldDefs.Item("selDecimal")
        ParamFieldDef10 = ParamFieldDefs.Item("selDeptCode")
        ParamFieldDef11 = ParamFieldDefs.Item("selEmpCodeFrom")
        ParamFieldDef12 = ParamFieldDefs.Item("selEmpCodeTo")
        ParamFieldDef13 = ParamFieldDefs.Item("srchGangCode")
        ParamFieldDef14 = ParamFieldDefs.Item("LocationTag")

        ParameterValues1 = ParamFieldDef1.CurrentValues
        ParameterValues2 = ParamFieldDef2.CurrentValues
        ParameterValues3 = ParamFieldDef3.CurrentValues
        ParameterValues4 = ParamFieldDef4.CurrentValues
        ParameterValues5 = ParamFieldDef5.CurrentValues
        ParameterValues6 = ParamFieldDef6.CurrentValues
        ParameterValues7 = ParamFieldDef7.CurrentValues
        ParameterValues8 = ParamFieldDef8.CurrentValues
        ParameterValues9 = ParamFieldDef9.CurrentValues
        ParameterValues10 = ParamFieldDef10.CurrentValues
        ParameterValues11 = ParamFieldDef11.CurrentValues
        ParameterValues12 = ParamFieldDef12.CurrentValues
        ParameterValues13 = ParamFieldDef13.CurrentValues
        ParameterValues14 = ParamFieldDef14.CurrentValues

        ParameterValues1.Add(ParamDiscreteValue1)
        ParameterValues2.Add(ParamDiscreteValue2)
        ParameterValues3.Add(ParamDiscreteValue3)
        ParameterValues4.Add(ParamDiscreteValue4)
        ParameterValues5.Add(ParamDiscreteValue5)
        ParameterValues6.Add(ParamDiscreteValue6)
        ParameterValues7.Add(ParamDiscreteValue7)
        ParameterValues8.Add(ParamDiscreteValue8)
        ParameterValues9.Add(ParamDiscreteValue9)
        ParameterValues10.Add(ParamDiscreteValue10)
        ParameterValues11.Add(ParamDiscreteValue11)
        ParameterValues12.Add(ParamDiscreteValue12)
        ParameterValues13.Add(ParamDiscreteValue13)
        ParameterValues14.Add(ParamDiscreteValue14)

        ParamFieldDef1.ApplyCurrentValues(ParameterValues1)
        ParamFieldDef2.ApplyCurrentValues(ParameterValues2)
        ParamFieldDef3.ApplyCurrentValues(ParameterValues3)
        ParamFieldDef4.ApplyCurrentValues(ParameterValues4)
        ParamFieldDef5.ApplyCurrentValues(ParameterValues5)
        ParamFieldDef6.ApplyCurrentValues(ParameterValues6)
        ParamFieldDef7.ApplyCurrentValues(ParameterValues7)
        ParamFieldDef8.ApplyCurrentValues(ParameterValues8)
        ParamFieldDef9.ApplyCurrentValues(ParameterValues9)
        ParamFieldDef10.ApplyCurrentValues(ParameterValues10)
        ParamFieldDef11.ApplyCurrentValues(ParameterValues11)
        ParamFieldDef12.ApplyCurrentValues(ParameterValues12)
        ParamFieldDef13.ApplyCurrentValues(ParameterValues13)
        ParamFieldDef14.ApplyCurrentValues(ParameterValues14)
    End Sub

End Class
