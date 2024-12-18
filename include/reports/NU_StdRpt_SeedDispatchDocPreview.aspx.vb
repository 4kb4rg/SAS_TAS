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

Public Class NU_StdRpt_SeedDispatchDocPreview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objNU As New agri.NU.clsReport()
    Dim objNUSetup As New agri.NU.clsSetup()
    Dim objNUTrx As New agri.NU.clsTrx()
    Dim objAdmin As New agri.Admin.clsComp()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strUserLoc As String

    Dim intDDLAccMthFrom As Integer
    Dim intDDLAccYrFrom As Integer
    Dim intDDLAccMthTo As Integer
    Dim intDDLAccYrTo As Integer

    Dim tempLoc As String
    Dim strAccPeriod As String
    Dim strDecimal As String

    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        crvView.Visible = False
        strLocType = Session("SS_LOCTYPE")

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

        intDDLAccMthFrom = Request.QueryString("DDLAccMth")
        intDDLAccYrFrom = Request.QueryString("DDLAccYr")
        intDDLAccMthTo = Request.QueryString("DDLAccMthTo")
        intDDLAccYrTo = Request.QueryString("DDLAccYrTo")

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_NUACCMONTH")
        strAccYear = Session("SS_NUACCYEAR")
        strLangCode = Session("SS_LANGCODE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            BindReport()
        End If
    End Sub


    Sub BindReport()

        Dim objRptDs As New DataSet()
        Dim dsComp As New DataSet()
        Dim objMapPath As String

        Dim SearchStr As String
        Dim strParam As String

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOpCd_GET As String = "NU_STDRPT_SEEDDISPATCHLIST_GET"
        Dim strOpCd_CompanyDet_GET As String = "ADMIN_CLSCOMP_COMPANY_DETAILS_GET"


        If Not (Request.QueryString("TxIDFrom") = "" And Request.QueryString("TxIDTo") = "") Then
            SearchStr = SearchStr & " AND NU.DispatchID >= '" & Request.QueryString("TxIDFrom") & "' AND NU.DispatchID <= '" & Request.QueryString("TxIDTo") & "'"
        End If

        If Not Request.QueryString("DocRefNoFrom") = "" Then
            SearchStr = SearchStr & " AND NU.DocRefNo LIKE '" & Request.QueryString("DocRefNoFrom") & "'"
        End If

        If Not (Request.QueryString("TxDateFrom") = "" And Request.QueryString("TxDateTo") = "") Then
            SearchStr = SearchStr & " AND (DateDiff(Day, '" & Request.QueryString("TxDateFrom") & "', NU.DispatchDate) >= 0) And (DateDiff(Day, '" & Request.QueryString("TxDateTo") & "', NU.DispatchDate) <= 0) "
        End If

        If Not Request.QueryString("BlkCode") = "" Then
            SearchStr = SearchStr & " AND NU.BlkCode LIKE '" & Request.QueryString("BlkCode") & "'"
        End If

        If Not Request.QueryString("BatchNo") = "" Then
            SearchStr = SearchStr & " AND NU.BatchNo LIKE '" & Request.QueryString("BatchNo") & "'"
        End If

        If Not Request.QueryString("BillPartyCode") = "" Then
            SearchStr = SearchStr & " AND NU.BillPartyCode LIKE '" & Request.QueryString("BillPartyCode") & "'"
        End If

        SearchStr = SearchStr & " AND NU.Status <> '" & Request.QueryString("Status") & "'"

        Try
            intErrNo = objNU.mtdGetAccPeriod(intDDLAccMthFrom, intDDLAccYrFrom, intDDLAccMthTo, intDDLAccYrTo, strAccPeriod)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=NU_STDRPT_SEEDDISPATCH_GET_CHECKACCPERIOD&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        SearchStr = SearchStr & strAccPeriod
        SearchStr = SearchStr & " ORDER BY NU.DispatchID ASC "

        strParam = strLocation & "|" & _
                    SearchStr

        Try
            intErrNo = objNU.mtdGetReport_SeedDispatchDoc(strOpCd_GET, strParam, objRptDs, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_NU_SEEDDISPATCHDOC_REPORT&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        strParam = strCompany
        Try
            intErrNo = objAdmin.mtdGetComp(strOpCd_CompanyDet_GET, strParam, dsComp, True)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=WM_STDRPT_COMP_DET_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        objRptDs.Tables(0).TableName = "NU_StdRpt_SeedDispatchList"
        objRptDs.Tables.Add(dsComp.Tables(0).Copy())
        objRptDs.Tables(1).TableName = "COMP_INFO"

        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\NU_StdRpt_SeedDispatchDoc.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs)

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Portrait
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA4

        crvView.Visible = False                         
        crvView.ReportSource = rdCrystalViewer          
        crvView.DataBind()                              

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\NU_StdRpt_SeedDispatchDoc.pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/NU_StdRpt_SeedDispatchDoc.pdf"">")
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

        paramField1 = paramFields.Item("ParamRptName")
        paramField2 = paramFields.Item("ParamLocation")
        paramField3 = paramFields.Item("lblBillPartyCode")
        paramField4 = paramFields.Item("lblBlkCode")
        paramField5 = paramFields.Item("lblBatchNo")

        crParameterValues1 = paramField1.CurrentValues
        crParameterValues2 = paramField2.CurrentValues
        crParameterValues3 = paramField3.CurrentValues
        crParameterValues4 = paramField4.CurrentValues
        crParameterValues5 = paramField5.CurrentValues

        ParamDiscreteValue1.Value = Request.QueryString("RptName")
        ParamDiscreteValue2.Value = Session("SS_LOC")
        ParamDiscreteValue3.Value = Request.QueryString("lblBillPartyCode")
        ParamDiscreteValue4.Value = Request.QueryString("lblBlkCode")
        ParamDiscreteValue5.Value = Request.QueryString("lblBatchNo")

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
