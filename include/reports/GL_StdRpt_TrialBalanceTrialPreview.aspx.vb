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
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web

Imports agri.PWSystem.clsLangCap

Public Class GL_StdRpt_TrialBalanceTrialPreview : Inherits Page
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGLRpt As New agri.GL.clsReport()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim rdCrystalViewer As ReportDocument
    Dim objLangCapDs As New Object()
    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strLocationName As String
    Dim strUserId As String
    Dim strPrintedBy As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfigsetting As Integer

    Dim strRptId As String
    Dim strRptName As String
    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim intSelDecimal As Integer
    Dim strSelSupress As String
    Dim strUserLoc As String
    Dim strSrchAccCode As String
    Dim strSrchAccCodeTo As String
    Dim strAccType As String
    Dim strAccTypeText As String
    Dim strSearchExp As String = ""
    Dim LocTag As String
    Dim AccCodeTag As String
    Dim AccDescTag As String
    Dim AccTypeTag As String
    Dim strExportToExcel As String
    Dim strChargeLevel As String
    Dim strSrchBlkCode As String
    Dim strSrchBlkCodeTo As String
    Dim strSrchBlkCodeText As String
    Dim strSrchAccMonthFrom As String
    Dim strSrchAccMonthTo As String
    Dim strSrchAccYear As String


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        crvView.Visible = False
        Dim tempLoc As String
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


        strRptId = Trim(Request.QueryString("RptId"))
        strRptName = Trim(Request.QueryString("RptName"))
        strSelAccMonth = Request.QueryString("DDLAccMth")
        strSelAccYear = Request.QueryString("DDLAccYr")
        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strLocationName = Session("SS_LOCATIONNAME")
        strUserId = Session("SS_USERID")
        strPrintedBy = Session("SS_USERNAME")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")

        intSelDecimal = CInt(Request.QueryString("Decimal"))
        strSelSupress = Request.QueryString("Supp")

        strSrchAccCode = Trim(Request.QueryString("SrchAccCode"))
        strSrchAccCodeTo = Trim(Request.QueryString("SrchAccCodeTo"))
        strAccType = Trim(Request.QueryString("AccType"))
        strAccTypeText = Trim(Request.QueryString("AccTypeText"))
        LocTag = Trim(Request.QueryString("LocTag"))
        AccCodeTag = Trim(Request.QueryString("AccCodeTag"))
        AccDescTag = Trim(Request.QueryString("AccDescTag"))
        AccTypeTag = Trim(Request.QueryString("AccTypeTag"))
        strExportToExcel = Trim(Request.QueryString("ExportToExcel"))

        strChargeLevel = Trim(Request.QueryString("ChargeLevel"))
        strSrchBlkCode = Trim(Request.QueryString("SrchBlkCode"))
        strSrchBlkCodeTo = Trim(Request.QueryString("SrchBlkCodeTo"))
        strSrchBlkCodeText = Trim(Request.QueryString("SrchBlkCodeText"))
        strSrchAccMonthFrom = Trim(Request.QueryString("SrchAccMonthFrom"))
        strSrchAccMonthTo = Trim(Request.QueryString("SrchAccMonthTo"))
        strSrchAccYear = Trim(Request.QueryString("SrchAccYear"))

        strExportToExcel = Trim(Request.QueryString("ExportToExcel"))

        If strSrchAccCode = "" Then
            strSearchExp = ""
        Else
            strSearchExp = " AND acc.AccCode = '" & Replace(strSrchAccCode, "'", "''") & "'"
        End If

        If strSrchAccCode <> "" And strSrchAccCodeTo <> "" Then
            strSearchExp = " AND acc.AccCode BETWEEN '" & Replace(strSrchAccCode, "'", "''") & "' AND '" & Replace(strSrchAccCodeTo, "'", "''") & "'"
        End If

        If strSrchBlkCode <> "" And strSrchBlkCodeTo = "" Then
            If strChargeLevel = "0" Then
                strSearchExp = strSearchExp + " AND acc.BlkCode IN (SELECT SubBlkCode FROM GL_SUBBLK WHERE BlkCode = '" & Replace(strSrchBlkCode, "'", "''") & "' UNION SELECT BlkCode FROM GL_SUBBLK WHERE BlkCode = '" & Replace(strSrchBlkCode, "'", "''") & "')"
            Else
                strSearchExp = strSearchExp + " AND acc.BlkCode = '" & Replace(strSrchBlkCode, "'", "''") & "'"
            End If
        ElseIf strSrchBlkCode <> "" And strSrchBlkCodeTo <> "" Then
            If strChargeLevel = "0" Then
                strSearchExp = strSearchExp + " AND acc.BlkCode IN (SELECT SubBlkCode FROM GL_SUBBLK WHERE BlkCode BETWEEN '" & Replace(strSrchBlkCode, "'", "''") & "' AND '" & Replace(strSrchBlkCodeTo, "'", "''") & "' UNION SELECT BlkCode FROM GL_SUBBLK WHERE BlkCode BETWEEN '" & Replace(strSrchBlkCode, "'", "''") & "' AND '" & Replace(strSrchBlkCodeTo, "'", "''") & "')"
            Else
                strSearchExp = strSearchExp + " AND acc.BlkCode BETWEEN '" & Replace(strSrchBlkCode, "'", "''") & "' AND '" & Replace(strSrchBlkCodeTo, "'", "''") & "'"
            End If
        End If


        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            BindReport()
        End If
    End Sub

    Sub BindReport()
        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim strOpCd As String
        Dim strOpCdRslGet As String = ""
        Dim objRptDs As New Dataset()
        Dim objMapPath As String
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strRptPrefix As String
        Dim intCnt As Integer
        Dim objFTPFolder As String

        strRptPrefix = "GL_StdRpt_TrialBalanceTrial"

        If strSrchBlkCode = "" Then
            strOpCd = "GL_STDRPT_TRIAL_BALANCETRIAL_GET_SP" & "|" & "GL_TRIALBAL"
            strRptPrefix = "GL_StdRpt_TrialBalanceTrial"
        Else
            If strChargeLevel = "0" Then
                strOpCd = "GL_STDRPT_TRIAL_BALANCETRIAL_GET_THNTANAM_SP" & "|" & "GL_TRIALBAL"
            Else
                strOpCd = "GL_STDRPT_TRIAL_BALANCETRIAL_GET_BLOCK_SP" & "|" & "GL_TRIALBAL"
            End If

            strRptPrefix = "GL_StdRpt_TrialBalanceTrialBlock"
        End If

        'strOpCd = "GL_STDRPT_TRIAL_BALANCETRIAL_GET_SP" & "|" & "GL_TRIALBAL"

        strParam = strSrchAccMonthFrom & "|" & _
                   strSrchAccMonthTo & "|" & _
                   strSrchAccYear & "|" & _
                   strUserLoc & "|" & _
                   strAccType & "|" & _
                   strSearchExp

        Try
            intErrNo = objGLRpt.mtdGetReport_TrialBalance(strOpCd, _
                                                          strCompany, _
                                                          strLocation, _
                                                          strUserId, _
                                                          strAccMonth, _
                                                          strAccYear, _
                                                          strParam, _
                                                          objRptDs, _
                                                          objMapPath, objFTPFolder)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_STDRPT_TRIALBALANCE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try


        rdCrystalViewer = New ReportDocument()
        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strRptPrefix & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs)
        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3


        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        If strExportToExcel = "0" Then
            'crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strRptPrefix & ".pdf"
            crDiskFileDestinationOptions.DiskFileName = objFTPFolder & strRptPrefix & ".pdf"
        Else
            'crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strRptPrefix & ".xls"
            crDiskFileDestinationOptions.DiskFileName = objFTPFolder & strRptPrefix & ".xls"
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
        rdCrystalViewer.Close()
        rdCrystalViewer.Dispose()

        Dim strUrl As String
        strUrl = objFTPFolder.Substring(InStr(objFTPFolder, "ftp") - 1, Len(objFTPFolder) - InStr(objFTPFolder, "ftp") + 1)
        strUrl = Replace(strUrl, "\", "/")

        If strExportToExcel = "0" Then
            'Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strRptPrefix & ".pdf"">")
            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../" & strUrl & strRptPrefix & ".pdf"">")
        Else
            'Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strRptPrefix & ".xls"">")
            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../" & strUrl & strRptPrefix & ".xls"">")
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

        strUserLoc = Replace(strUserLoc, "','", ", ")
        strAccTypeText = Replace(strAccTypeText, "','", ", ")

        ParamDiscreteValue1.Value = strUserLoc
        ParamDiscreteValue2.Value = strSrchAccMonthFrom
        ParamDiscreteValue3.Value = strSrchAccYear
        ParamDiscreteValue4.Value = intSelDecimal
        ParamDiscreteValue5.Value = strSelSupress
        ParamDiscreteValue6.Value = strCompanyName
        ParamDiscreteValue7.Value = strLocationName
        ParamDiscreteValue8.Value = strPrintedBy
        ParamDiscreteValue9.Value = strRptId
        ParamDiscreteValue10.Value = strRptName
        ParamDiscreteValue11.Value = LocTag
        ParamDiscreteValue12.Value = AccCodeTag
        ParamDiscreteValue13.Value = AccTypeTag
        ParamDiscreteValue14.Value = strSrchAccCode
        ParamDiscreteValue15.Value = strAccTypeText
        ParamDiscreteValue16.Value = AccDescTag
        ParamDiscreteValue17.Value = strSrchAccMonthTo

        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        ParamFieldDef1 = ParamFieldDefs.Item("SelLocCode")
        ParamFieldDef2 = ParamFieldDefs.Item("SelAccMonth")
        ParamFieldDef3 = ParamFieldDefs.Item("SelAccYear")
        ParamFieldDef4 = ParamFieldDefs.Item("SelDecimal")
        ParamFieldDef5 = ParamFieldDefs.Item("SelSuppress")
        ParamFieldDef6 = ParamFieldDefs.Item("SessionCompName")
        ParamFieldDef7 = ParamFieldDefs.Item("SessionLocName")
        ParamFieldDef8 = ParamFieldDefs.Item("SessionPrintedBy")
        ParamFieldDef9 = ParamFieldDefs.Item("RptId")
        ParamFieldDef10 = ParamFieldDefs.Item("RptName")
        ParamFieldDef11 = ParamFieldDefs.Item("LocTag")
        ParamFieldDef12 = ParamFieldDefs.Item("AccCodeTag")
        ParamFieldDef13 = ParamFieldDefs.Item("AccTypeTag")
        ParamFieldDef14 = ParamFieldDefs.Item("SrchAccCode")
        ParamFieldDef15 = ParamFieldDefs.Item("SrchAccType")
        ParamFieldDef16 = ParamFieldDefs.Item("AccDescTag")
        ParamFieldDef17 = ParamFieldDefs.Item("SelAccMonthTo")

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

    End Sub

End Class
