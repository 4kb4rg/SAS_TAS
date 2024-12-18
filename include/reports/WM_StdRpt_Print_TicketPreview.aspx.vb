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

Public Class WM_StdRpt_PrintTicketPreview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objWM As New agri.WM.clsReport()
    Dim objWMTrx As New agri.WM.clsTrx()
    Dim objAdmin As New agri.Admin.clsShare 

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
        Dim dsComp As New DataSet   
        Dim dsLoc As New DataSet 
        Dim objMapPath As String

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOpCd_Ticket_GET As String = "WM_STDRPT_PRINT_TICKET_GET"
        Dim strOpCd_LocationDet_GET As String = "ADMIN_CLSSHARE_COMPLOC_DETAILS_GET" 
        Dim strFileName As String = "WM_StdRpt_PrintTicket"

        Dim strParam As String
        Dim SearchStr As String

        If Not (Request.QueryString("DateOutFrom") = "" And Request.QueryString("DateOutTo") = "") Then
            SearchStr = "AND (DateDiff(Day, '" & Request.QueryString("DateOutFrom") & "', TIC.OutDate) >= 0) And (DateDiff(Day, '" & Request.QueryString("DateOutTo") & "', TIC.OutDate) <= 0) "
        End If

        If Not (Request.QueryString("TicketNoFrom") = "" And Request.QueryString("TicketNoTo") = "") Then
            SearchStr = SearchStr & "AND TIC.TicketNo IN (SELECT SUBTIC.TicketNo FROM WM_TICKET SUBTIC WHERE SUBTIC.TicketNo >= '" & Request.QueryString("TicketNoFrom") & _
                        "' AND SUBTIC.TicketNo <= '" & Request.QueryString("TicketNoTo") & "') "
        End If

        If Not Request.QueryString("Product") = objWMTrx.EnumWeighBridgeTicketProduct.All Then
            SearchStr = SearchStr & "AND TIC.ProductCode = '" & Request.QueryString("Product") & "' "
        End If

        If Not Request.QueryString("Customer") = "" Then
            SearchStr = SearchStr & "AND TIC.CustomerCode LIKE '" & Request.QueryString("Customer") & "' "
        End If

        If Not Request.QueryString("Vehicle") = "" Then
            SearchStr = SearchStr & "AND TIC.VehicleCode LIKE '" & Request.QueryString("Vehicle") & "' "
        End If

        strParam = strUserLoc & "|" & Request.QueryString("strddlAccMth") & "|" & Request.QueryString("strddlAccYr") & "|" & _
                   objWMTrx.EnumWeighBridgeTicketStatus.Active & "|" & _
                   objWMTrx.EnumWeighBridgeTicketTransType.Purchase & "|" & _
                   objWMTrx.EnumWeighBridgeTicketTransType.Sales & "|" & SearchStr
        Try
            intErrNo = objWM.mtdGetReport_PrintTicket(strOpCd_Ticket_GET, strParam, objRptDs, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=WM_STDRPT_PRINT_TICKET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try


        strParam = strCompany & "|" & strUserLoc & "|"
        Try
            intErrNo = objAdmin.mtdGetLocDetails(strOpCd_LocationDet_GET, _
                                                 strCompany, _
                                                 strLocation, _
                                                 strUserId, _
                                                 dsLoc, _
                                                 strParam)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=WM_STDRPT_LOC_DET_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        objRptDs.Tables(0).TableName = "WM_TICKET_INFO"
        objRptDs.Tables.Add(dsLoc.Tables(0).Copy()) 
        objRptDs.Tables(1).TableName = "LOC_INFO" 


        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strFileName & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs)

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Portrait
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA4

        crvView.Visible = False                         
        crvView.ReportSource = rdCrystalViewer          
        crvView.DataBind()                              

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions
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

        Dim ParamDiscreteValue1 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue2 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue3 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue4 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue5 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue6 As New ParameterDiscreteValue()

        Dim crParameterValues1 As ParameterValues
        Dim crParameterValues2 As ParameterValues
        Dim crParameterValues3 As ParameterValues
        Dim crParameterValues4 As ParameterValues
        Dim crParameterValues5 As ParameterValues
        Dim crParameterValues6 As ParameterValues

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("ParamDecimal")
        paramField2 = paramFields.Item("ParamRptName")
        paramField3 = paramFields.Item("lblVehicleTag")
        paramField4 = paramFields.Item("lblBlockTag")
        paramField5 = paramFields.Item("ParamUserName")
        paramField6 = paramFields.Item("ParamLocation")

        crParameterValues1 = paramField1.CurrentValues
        crParameterValues2 = paramField2.CurrentValues
        crParameterValues3 = paramField3.CurrentValues
        crParameterValues4 = paramField4.CurrentValues
        crParameterValues5 = paramField5.CurrentValues
        crParameterValues6 = paramField6.CurrentValues

        ParamDiscreteValue1.Value = Request.QueryString("Decimal")
        ParamDiscreteValue2.Value = Request.QueryString("RptName")
        ParamDiscreteValue3.Value = Request.QueryString("lblVehicleTag")
        ParamDiscreteValue4.Value = Request.QueryString("lblBlockTag")
        ParamDiscreteValue5.Value = Session("SS_USERNAME")
        ParamDiscreteValue6.Value = Session("SS_LOC")

        crParameterValues1.Add(ParamDiscreteValue1)
        crParameterValues2.Add(ParamDiscreteValue2)
        crParameterValues3.Add(ParamDiscreteValue3)
        crParameterValues4.Add(ParamDiscreteValue4)
        crParameterValues5.Add(ParamDiscreteValue5)
        crParameterValues6.Add(ParamDiscreteValue6)

        PFDefs(0).ApplyCurrentValues(crParameterValues1)
        PFDefs(1).ApplyCurrentValues(crParameterValues2)
        PFDefs(2).ApplyCurrentValues(crParameterValues3)
        PFDefs(3).ApplyCurrentValues(crParameterValues4)
        PFDefs(4).ApplyCurrentValues(crParameterValues5)
        PFDefs(5).ApplyCurrentValues(crParameterValues6)

        crvView.ParameterFieldInfo = paramFields
    End Sub
End Class
