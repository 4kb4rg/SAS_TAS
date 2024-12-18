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

Public Class FA_StdRpt_AssetWOListPreview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objFA As New agri.FA.clsReport()
    Dim objFASetup As New agri.FA.clsSetup()
    Dim objFATrx As New agri.FA.clsTrx()
    Dim objAdmin As New agri.Admin.clsShare()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intCnt As Integer
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

    Dim strExportToExcel As String
    Dim strDeprType As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        crvView.Visible = False

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

        strExportToExcel = Trim(Request.QueryString("ExportToExcel"))
        strDeprType = Trim(Request.QueryString("DeprType"))

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_FAACCMONTH")
        strAccYear = Session("SS_FAACCYEAR")
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
        Dim strParam As String

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOpCd_GET As String = "FA_STDRPT_ASSETWOLIST_GET"

        Dim strFileName As String

        Select Case strDeprType
            Case "0"
                strFileName = "FA_StdRpt_AssetWOList"
            Case "1"
                strFileName = "FA_StdRpt_AssetWOListFiskal"
            Case "2"
                strFileName = "FA_StdRpt_AssetWOListFiskalVSKomersial"
        End Select



        If Not (Request.QueryString("TxIDFrom") = "" And Request.QueryString("TxIDTo") = "") Then
            SearchStr = SearchStr & " AND FA.AssetWOID >= '" & Request.QueryString("TxIDFrom") & "' AND FA.AssetWOID <= '" & Request.QueryString("TxIDTo") & "'"
        End If

        If Not (Request.QueryString("TxDateFrom") = "" And Request.QueryString("TxDateTo") = "") Then
            SearchStr = SearchStr & " AND (DateDiff(Day, '" & Request.QueryString("TxDateFrom") & "', FA.RefDate) >= 0) And (DateDiff(Day, '" & Request.QueryString("TxDateTo") & "', FA.RefDate) <= 0) "
        End If

        If Not Request.QueryString("AssetCode") = "" Then
            SearchStr = SearchStr & " AND FA.AssetCode LIKE '" & Request.QueryString("AssetCode") & "'"
        End If

        If Not Request.QueryString("AssetHeaderCode") = "" Then
            SearchStr = SearchStr & " AND RGLN.AssetHeaderCode LIKE '" & Request.QueryString("AssetHeaderCode") & "'"
        End If

        'If Not Request.QueryString("WOGLAssetAccCode") = "" Then
        '    SearchStr = SearchStr & " AND RGLN.AssetAccCode LIKE '" & Request.QueryString("WOGLAssetAccCode") & "'"
        'End If

        'If Not Request.QueryString("WOGLAccumDeprAccCode") = "" Then
        '    SearchStr = SearchStr & " AND RGLN.DeprGLAccumDeprAccCode LIKE '" & Request.QueryString("WOGLAccumDeprAccCode") & "'"
        'End If

        'If Not Request.QueryString("WOGLWOAccCode") = "" Then
        '    SearchStr = SearchStr & " AND FA.WOGLWOAccCode LIKE '" & Request.QueryString("WOGLWOAccCode") & "'"
        'End If

        If Not Request.QueryString("RefNo") = "" Then
            SearchStr = SearchStr & " AND FA.RefNo LIKE '" & Request.QueryString("RefNo") & "'"
        End If

        If Not Request.QueryString("Status") = "" Then
            If Not Request.QueryString("Status") = objFATrx.EnumAssetWOStatus.All Then
                SearchStr = SearchStr & " AND FA.Status = '" & Request.QueryString("Status") & "'"
            End If
        End If

        Try
            intErrNo = objFA.mtdGetAccPeriod(intDDLAccMthFrom, intDDLAccYrFrom, intDDLAccMthTo, intDDLAccYrTo, strAccPeriod)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=FA_STDRPT_ASSETWO_GET_CHECKACCPERIOD&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        SearchStr = SearchStr & strAccPeriod
        'SearchStr = SearchStr & " ORDER BY FA.AssetWOID ASC "

        strParam = strUserLoc & "|" & _
                    SearchStr

        Try
            intErrNo = objFA.mtdGetReport_AssetWOList(strOpCd_GET, strParam, objRptDs, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_FA_ASSETWOLIST_REPORT&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objRptDs.Tables(0).Rows.Count - 1
            objRptDs.Tables(0).Rows(intCnt).Item("Status") = objFATrx.mtdGetAssetWOStatus(objRptDs.Tables(0).Rows(intCnt).Item("Status"))
        Next


        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strFileName & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3

        crvView.Visible = False
        crvView.ReportSource = rdCrystalViewer
        crvView.DataBind()

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()

        If strExportToExcel = "0" Then
            crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strFileName & ".pdf"
        Else
            crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strFileName & ".xls"
        End If


        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile

            If strExportToExcel = "0" Then
                .ExportFormatType = ExportFormatType.PortableDocFormat
            Else
                .ExportFormatType = ExportFormatType.Excel
            End If
        End With

        rdCrystalViewer.Export()

        If strExportToExcel = "0" Then
            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strFileName & ".pdf"">")
        Else
            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strFileName & ".xls"">")
        End If

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
        Dim paramField19 As New ParameterField()
        Dim paramField20 As New ParameterField()
        Dim paramField21 As New ParameterField()
        Dim paramField22 As New ParameterField()
        Dim paramField23 As New ParameterField()
        Dim paramField24 As New ParameterField()
        Dim paramField25 As New ParameterField()
        Dim paramField26 As New ParameterField()
        Dim paramField27 As New ParameterField()
        Dim paramField28 As New ParameterField()

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
        Dim ParamDiscreteValue19 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue20 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue21 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue22 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue23 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue24 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue25 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue26 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue27 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue28 As New ParameterDiscreteValue()

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
        Dim crParameterValues19 As ParameterValues
        Dim crParameterValues20 As ParameterValues
        Dim crParameterValues21 As ParameterValues
        Dim crParameterValues22 As ParameterValues
        Dim crParameterValues23 As ParameterValues
        Dim crParameterValues24 As ParameterValues
        Dim crParameterValues25 As ParameterValues
        Dim crParameterValues26 As ParameterValues
        Dim crParameterValues27 As ParameterValues
        Dim crParameterValues28 As ParameterValues

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("ParamCompanyName")
        paramField2 = paramFields.Item("ParamLocation")
        paramField3 = paramFields.Item("ParamUserName")
        paramField4 = paramFields.Item("ParamRptID")
        paramField5 = paramFields.Item("ParamRptName")
        paramField6 = paramFields.Item("lblLocation")
        paramField7 = paramFields.Item("ParamStatus")
        paramField8 = paramFields.Item("ParamAccMonth")
        paramField9 = paramFields.Item("ParamAccYear")
        paramField10 = paramFields.Item("ParamAccMonthTo")
        paramField11 = paramFields.Item("ParamAccYearTo")
        paramField12 = paramFields.Item("ParamTxIDFrom")
        paramField13 = paramFields.Item("ParamTxIDTo")
        paramField14 = paramFields.Item("ParamAssetCode")
        paramField15 = paramFields.Item("ParamRefNo")
        paramField16 = paramFields.Item("ParamTxDateFrom")
        paramField17 = paramFields.Item("ParamTxDateTo")
        paramField18 = paramFields.Item("lblTrxID")
        paramField19 = paramFields.Item("lblAssetCode")
        paramField20 = paramFields.Item("lblAssetHeaderCode")
        paramField21 = paramFields.Item("lblWOGLAssetAccCode")
        paramField22 = paramFields.Item("lblWOGLAccumDeprAccCode")
        paramField23 = paramFields.Item("lblWOGLWOAccCode")
        paramField24 = paramFields.Item("ParamAssetHeaderCode")
        paramField25 = paramFields.Item("ParamWOGLAssetAccCode")
        paramField26 = paramFields.Item("ParamWOGLAccumDeprAccCode")
        paramField27 = paramFields.Item("ParamWOGLWOAccCode")
        paramField28 = paramFields.Item("ParamDecimal")

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
        crParameterValues19 = paramField19.CurrentValues
        crParameterValues20 = paramField20.CurrentValues
        crParameterValues21 = paramField21.CurrentValues
        crParameterValues22 = paramField22.CurrentValues
        crParameterValues23 = paramField23.CurrentValues
        crParameterValues24 = paramField24.CurrentValues
        crParameterValues25 = paramField25.CurrentValues
        crParameterValues26 = paramField26.CurrentValues
        crParameterValues27 = paramField27.CurrentValues
        crParameterValues28 = paramField28.CurrentValues

        ParamDiscreteValue1.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue2.Value = Session("SS_LOC")
        ParamDiscreteValue3.Value = Session("SS_USERNAME")
        ParamDiscreteValue4.Value = Request.QueryString("RptID")
        ParamDiscreteValue5.Value = Request.QueryString("RptName")
        ParamDiscreteValue6.Value = Request.QueryString("lblLocation")
        ParamDiscreteValue7.Value = objFATrx.mtdGetAssetWOStatus(Request.QueryString("Status"))
        ParamDiscreteValue8.Value = Request.QueryString("DDLAccMth")
        ParamDiscreteValue9.Value = Request.QueryString("DDLAccYr")
        ParamDiscreteValue10.Value = Request.QueryString("DDLAccMthTo")
        ParamDiscreteValue11.Value = Request.QueryString("DDLAccYrTo")
        ParamDiscreteValue12.Value = Request.QueryString("TxIDFrom")
        ParamDiscreteValue13.Value = Request.QueryString("TxIDTo")
        ParamDiscreteValue14.Value = Request.QueryString("AssetCode")
        ParamDiscreteValue15.Value = Request.QueryString("RefNo")
        ParamDiscreteValue16.Value = Request.QueryString("TxDateFrom")
        ParamDiscreteValue17.Value = Request.QueryString("TxDateTo")
        ParamDiscreteValue18.Value = Request.QueryString("lblTxID")
        ParamDiscreteValue19.Value = Request.QueryString("lblAssetCode")
        ParamDiscreteValue20.Value = "" 'Request.QueryString("lblAssetHeaderCode")
        ParamDiscreteValue21.Value = "" 'Request.QueryString("lblWOGLAssetAccCode")
        ParamDiscreteValue22.Value = "" 'Request.QueryString("lblWOGLAccumDeprAccCode")
        ParamDiscreteValue23.Value = "" 'Request.QueryString("lblWOGLWOAccCode")
        ParamDiscreteValue24.Value = Request.QueryString("AssetHeaderCode")
        ParamDiscreteValue25.Value = Request.QueryString("WOGLAssetAccCode")
        ParamDiscreteValue26.Value = Request.QueryString("WOGLAccumDeprAccCode")
        ParamDiscreteValue27.Value = Request.QueryString("WOGLWOAccCode")
        ParamDiscreteValue28.Value = Request.QueryString("Decimal")

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
        crParameterValues19.Add(ParamDiscreteValue19)
        crParameterValues20.Add(ParamDiscreteValue20)
        crParameterValues21.Add(ParamDiscreteValue21)
        crParameterValues22.Add(ParamDiscreteValue22)
        crParameterValues23.Add(ParamDiscreteValue23)
        crParameterValues24.Add(ParamDiscreteValue24)
        crParameterValues25.Add(ParamDiscreteValue25)
        crParameterValues26.Add(ParamDiscreteValue26)
        crParameterValues27.Add(ParamDiscreteValue27)
        crParameterValues28.Add(ParamDiscreteValue28)

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
        PFDefs(18).ApplyCurrentValues(crParameterValues19)
        PFDefs(19).ApplyCurrentValues(crParameterValues20)
        PFDefs(20).ApplyCurrentValues(crParameterValues21)
        PFDefs(21).ApplyCurrentValues(crParameterValues22)
        PFDefs(22).ApplyCurrentValues(crParameterValues23)
        PFDefs(23).ApplyCurrentValues(crParameterValues24)
        PFDefs(24).ApplyCurrentValues(crParameterValues25)
        PFDefs(25).ApplyCurrentValues(crParameterValues26)
        PFDefs(26).ApplyCurrentValues(crParameterValues27)
        PFDefs(27).ApplyCurrentValues(crParameterValues28)

        crvView.ParameterFieldInfo = paramFields
    End Sub
End Class
