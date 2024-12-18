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

Public Class FA_StdRpt_AssetDeprListPreview : Inherits Page

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
        Dim intCnt As Integer

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim strFileName As String


        If strDeprType = "0" Then
            strFileName = "FA_StdRpt_AssetDeprList"
        Else
            strFileName = "FA_StdRpt_AssetDeprListFiskal"
        End If


        Dim strOpCd_GET As String = "FA_STDRPT_ASSETDEPRLIST_GET"


        If Not (Request.QueryString("TxIDFrom") = "" And Request.QueryString("TxIDTo") = "") Then
            SearchStr = SearchStr & " AND FA.AssetDeprID >= '" & Request.QueryString("TxIDFrom") & "' AND FA.AssetDeprID <= '" & Request.QueryString("TxIDTo") & "'"
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

        If Not Request.QueryString("AssetHeaderCode") = "" Then
            SearchStr = SearchStr & " AND RGLN.AssetHeaderCode LIKE '" & Request.QueryString("AssetHeaderCode") & "'"
        End If

        If Not Request.QueryString("DeprGLDeprAccCode") = "" Then
            SearchStr = SearchStr & " AND RGLN.DeprGLDeprAccCode LIKE '" & Request.QueryString("DeprGLDeprAccCode") & "'"
        End If

        If Not Request.QueryString("DeprGLAccumDeprAccCode") = "" Then
            SearchStr = SearchStr & " AND RGLN.DeprGLAccumDeprAccCode LIKE '" & Request.QueryString("DeprGLAccumDeprAccCode") & "'"
        End If

        If Not Request.QueryString("RefNo") = "" Then
            SearchStr = SearchStr & " AND FA.RefNo LIKE '" & Request.QueryString("RefNo") & "'"
        End If

        If Not Request.QueryString("Status") = "" Then
            If Not Request.QueryString("Status") = objFATrx.EnumAssetDeprStatus.All Then
                SearchStr = SearchStr & " AND FA.Status = '" & Request.QueryString("Status") & "'"
            End If
        End If

        Try
            intErrNo = objFA.mtdGetAccPeriod(intDDLAccMthFrom, intDDLAccYrFrom, intDDLAccMthTo, intDDLAccYrTo, strAccPeriod)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=FA_STDRPT_ASSETDEPR_GET_CHECKACCPERIOD&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        SearchStr = SearchStr & strAccPeriod

        strParam = strUserLoc & "|" & _
                    SearchStr

        Try
            intErrNo = objFA.mtdGetReport_AssetDeprList(strOpCd_GET, strParam, objRptDs, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_FA_ASSETDEPRLIST_REPORT&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objRptDs.Tables(0).Rows.Count - 1
            objRptDs.Tables(0).Rows(intCnt).Item("Status") = objFATrx.mtdGetAssetDeprStatus(objRptDs.Tables(0).Rows(intCnt).Item("Status"))
            objRptDs.Tables(0).Rows(intCnt).Item("GenDeprInd") = objFATrx.mtdGetGenDeprInd(objRptDs.Tables(0).Rows(intCnt).Item("GenDeprInd"))
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
        Dim ParamFieldDef15 As ParameterFieldDefinition
        Dim ParamFieldDef16 As ParameterFieldDefinition
        Dim ParamFieldDef17 As ParameterFieldDefinition
        Dim ParamFieldDef18 As ParameterFieldDefinition
        Dim ParamFieldDef19 As ParameterFieldDefinition
        Dim ParamFieldDef20 As ParameterFieldDefinition
        Dim ParamFieldDef21 As ParameterFieldDefinition
        Dim ParamFieldDef22 As ParameterFieldDefinition
        Dim ParamFieldDef23 As ParameterFieldDefinition
        Dim ParamFieldDef24 As ParameterFieldDefinition
        Dim ParamFieldDef25 As ParameterFieldDefinition
        Dim ParamFieldDef26 As ParameterFieldDefinition

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
        Dim ParameterValues15 As New ParameterValues()
        Dim ParameterValues16 As New ParameterValues()
        Dim ParameterValues17 As New ParameterValues()
        Dim ParameterValues18 As New ParameterValues()
        Dim ParameterValues19 As New ParameterValues()
        Dim ParameterValues20 As New ParameterValues()
        Dim ParameterValues21 As New ParameterValues()
        Dim ParameterValues22 As New ParameterValues()
        Dim ParameterValues23 As New ParameterValues()
        Dim ParameterValues24 As New ParameterValues()
        Dim ParameterValues25 As New ParameterValues()
        Dim ParameterValues26 As New ParameterValues()

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

        ParamDiscreteValue1.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue2.Value = Session("SS_LOC")
        ParamDiscreteValue3.Value = Session("SS_USERNAME")
        ParamDiscreteValue4.Value = Request.QueryString("RptID")
        ParamDiscreteValue5.Value = Request.QueryString("RptName")
        ParamDiscreteValue6.Value = Request.QueryString("lblLocation")
        ParamDiscreteValue7.Value = objFATrx.mtdGetAssetAddStatus(Request.QueryString("Status"))
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
        ParamDiscreteValue20.Value = Request.QueryString("lblAssetHeaderCode")
        ParamDiscreteValue21.Value = Request.QueryString("lblDeprGLDeprAccCode")
        ParamDiscreteValue22.Value = Request.QueryString("lblDeprGLAccumDeprAccCode")
        ParamDiscreteValue23.Value = Request.QueryString("AssetHeaderCode")
        ParamDiscreteValue24.Value = Request.QueryString("DeprGLDeprAccCode")
        ParamDiscreteValue25.Value = Request.QueryString("DeprGLAccumDeprAccCode")
        ParamDiscreteValue26.Value = Request.QueryString("Decimal")

        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        ParamFieldDef1 = ParamFieldDefs.Item("ParamCompanyName")
        ParamFieldDef2 = ParamFieldDefs.Item("ParamLocation")
        ParamFieldDef3 = ParamFieldDefs.Item("ParamUserName")
        ParamFieldDef4 = ParamFieldDefs.Item("ParamRptID")
        ParamFieldDef5 = ParamFieldDefs.Item("ParamRptName")
        ParamFieldDef6 = ParamFieldDefs.Item("lblLocation")
        ParamFieldDef7 = ParamFieldDefs.Item("ParamStatus")
        ParamFieldDef8 = ParamFieldDefs.Item("ParamAccMonth")
        ParamFieldDef9 = ParamFieldDefs.Item("ParamAccYear")
        ParamFieldDef10 = ParamFieldDefs.Item("ParamAccMonthTo")
        ParamFieldDef11 = ParamFieldDefs.Item("ParamAccYearTo")
        ParamFieldDef12 = ParamFieldDefs.Item("ParamTxIDFrom")
        ParamFieldDef13 = ParamFieldDefs.Item("ParamTxIDTo")
        ParamFieldDef14 = ParamFieldDefs.Item("ParamAssetCode")
        ParamFieldDef15 = ParamFieldDefs.Item("ParamRefNo")
        ParamFieldDef16 = ParamFieldDefs.Item("ParamTxDateFrom")
        ParamFieldDef17 = ParamFieldDefs.Item("ParamTxDateTo")
        ParamFieldDef18 = ParamFieldDefs.Item("lblTrxID")
        ParamFieldDef19 = ParamFieldDefs.Item("lblAssetCode")
        ParamFieldDef20 = ParamFieldDefs.Item("lblAssetHeaderCode")
        ParamFieldDef21 = ParamFieldDefs.Item("lblDeprGLDeprAccCode")
        ParamFieldDef22 = ParamFieldDefs.Item("lblDeprGLAccumDeprAccCode")
        ParamFieldDef23 = ParamFieldDefs.Item("ParamAssetHeaderCode")
        ParamFieldDef24 = ParamFieldDefs.Item("ParamDeprGLDeprAccCode")
        ParamFieldDef25 = ParamFieldDefs.Item("ParamDeprGLAccumDeprAccCode")
        ParamFieldDef26 = ParamFieldDefs.Item("ParamDecimal")

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
        ParameterValues15 = ParamFieldDef15.CurrentValues
        ParameterValues16 = ParamFieldDef16.CurrentValues
        ParameterValues17 = ParamFieldDef17.CurrentValues
        ParameterValues18 = ParamFieldDef18.CurrentValues
        ParameterValues19 = ParamFieldDef19.CurrentValues
        ParameterValues20 = ParamFieldDef20.CurrentValues
        ParameterValues21 = ParamFieldDef21.CurrentValues
        ParameterValues22 = ParamFieldDef22.CurrentValues
        ParameterValues23 = ParamFieldDef23.CurrentValues
        ParameterValues24 = ParamFieldDef24.CurrentValues
        ParameterValues25 = ParamFieldDef25.CurrentValues
        ParameterValues26 = ParamFieldDef26.CurrentValues

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
        ParameterValues15.Add(ParamDiscreteValue15)
        ParameterValues16.Add(ParamDiscreteValue16)
        ParameterValues17.Add(ParamDiscreteValue17)
        ParameterValues18.Add(ParamDiscreteValue18)
        ParameterValues19.Add(ParamDiscreteValue19)
        ParameterValues20.Add(ParamDiscreteValue20)
        ParameterValues21.Add(ParamDiscreteValue21)
        ParameterValues22.Add(ParamDiscreteValue22)
        ParameterValues23.Add(ParamDiscreteValue23)
        ParameterValues24.Add(ParamDiscreteValue24)
        ParameterValues25.Add(ParamDiscreteValue25)
        ParameterValues26.Add(ParamDiscreteValue26)

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
        ParamFieldDef15.ApplyCurrentValues(ParameterValues15)
        ParamFieldDef16.ApplyCurrentValues(ParameterValues16)
        ParamFieldDef17.ApplyCurrentValues(ParameterValues17)
        ParamFieldDef18.ApplyCurrentValues(ParameterValues18)
        ParamFieldDef19.ApplyCurrentValues(ParameterValues19)
        ParamFieldDef20.ApplyCurrentValues(ParameterValues20)
        ParamFieldDef21.ApplyCurrentValues(ParameterValues21)
        ParamFieldDef22.ApplyCurrentValues(ParameterValues22)
        ParamFieldDef23.ApplyCurrentValues(ParameterValues23)
        ParamFieldDef24.ApplyCurrentValues(ParameterValues24)
        ParamFieldDef25.ApplyCurrentValues(ParameterValues25)
        ParamFieldDef26.ApplyCurrentValues(ParameterValues26)

    End Sub
End Class
