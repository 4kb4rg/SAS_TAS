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

Public Class GL_StdRpt_NotaDebetPreview : Inherits Page
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objGLRpt As New agri.GL.clsReport()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblRefDesc As Label
    Protected WithEvents EventData As DataGrid

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
    Dim strNo As String
    Dim strDate As String
    Dim strTo As String
    Dim strAlamat As String
    Dim strPrintFrom As String
    Dim strCharging As String
    Dim strSrchBlkCode As String
    Dim strSrchVehCode As String
    Dim strSrchVehExpCode As String
    Dim strAccType As String
    Dim strAccTypeText As String
    Dim strWithTrans As String
    Dim strEstExpense As String
    Dim strSearchExp As String = ""
    Dim lblBlkCode As String = ""
    Dim lblVehCode As String = ""
    Dim lblVehExpCode As String = ""
    Dim lblLocation As String = ""
    Dim lblAccCode As String = ""
    Dim lblAccType As String = ""
    Dim dblPCF As Double = 0
    Dim dblNCF As Double = 0
    Dim strTerbilang As String = ""
    Dim rdCrystalViewer As New ReportDocument()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String
    Dim strExportToExcel As String
    Dim strAssignBy As String
    Dim strCheckBy As String
    Dim strJbtn1 As String
    Dim strJbtn2 As String



    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        crvView.Visible = False


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


        strRptId = Trim(Request.QueryString("RptId"))
        strRptName = Trim(Request.QueryString("RptName"))
        strSelAccMonth = Request.QueryString("DDLAccMth")
        strSelAccYear = Request.QueryString("DDLAccYr")
        intSelDecimal = CInt(Request.QueryString("Decimal"))
        strSrchAccCode = Trim(Request.QueryString("SrchAccCode"))
        strNo = Trim(Request.QueryString("Nomor"))
        strDate = Trim(Request.QueryString("Tanggal"))
        strTo = Trim(Request.QueryString("Kepada"))
        strAlamat = Trim(Request.QueryString("Alamat"))
        strPrintFrom = Trim(Request.QueryString("PrintFrom"))
        strCharging = Trim(Request.QueryString("Charging"))
        strExportToExcel = Trim(Request.QueryString("ExportToExcel"))
        strAssignBy = Trim(Request.QueryString("AssignBy"))
        strCheckBy = Trim(Request.QueryString("CheckBy"))
        strJbtn1 = Trim(Request.QueryString("Jbtn1"))
        strJbtn2 = Trim(Request.QueryString("Jbtn2"))

		If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
           BindReport()
        End If
    End Sub

    Sub BindReport()

        Dim objRptDs As New DataSet()
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim objMapPath As String
        Dim intErrNo As Integer

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOpCode As String
        Dim strFileName As String
        Dim totalrp As Double = 0
        Dim strTotalrp As String
        Dim intCnt As Integer
        Dim objFTPFolder As String

        strParamName = "LOCCODE|ACCMONTH|ACCYEAR|ACCCODE|TABLE|STRSEARCH"

        If Trim(strPrintFrom) = "Temporary" Then
            If Trim(strCharging) = "Company" Then
                strParamValue = strLocation & "|" & strSelAccMonth & "|" & strSelAccYear & "|" & strSrchAccCode & "|" & "GL_MTHENDTRX_TRIAL" & "|" & " AND PRICEAMOUNT > 0 "
            Else
                strParamValue = strLocation & "|" & strSelAccMonth & "|" & strSelAccYear & "|" & strSrchAccCode & "|" & "GL_MTHENDTRX_TRIAL" & "|" & "  "
            End If
        Else
            If Trim(strCharging) = "Company" Then
                strParamValue = strLocation & "|" & strSelAccMonth & "|" & strSelAccYear & "|" & strSrchAccCode & "|" & "SH_MTHENDTRX" & "|" & " AND PRICEAMOUNT > 0 "
            Else
                strParamValue = strLocation & "|" & strSelAccMonth & "|" & strSelAccYear & "|" & strSrchAccCode & "|" & "SH_MTHENDTRX" & "|" & "  "
            End If
        End If
		
        strOpCode = "GL_STDRPT_NOTADEBET_GET"
        strFileName = "GL_StdRpt_NotaDebet"

        Dim strParam As String
        Dim searchStr As String

        Try
            intErrNo = objGLRpt.mtdGetReportDataCommon(strOpCode, strParamName, strParamValue, objRptDs, objMapPath, objFTPFolder)

        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_STDRPT_TRIALBAL_TEMPORARY_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objRptDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objRptDs.Tables(0).Rows.Count - 1
                totalrp += CDbl(objRptDs.Tables(0).Rows(intCnt).Item("PriceAmount"))
            Next intCnt

            If totalrp < 0 Then totalrp = totalrp * -1

            strTotalrp = Trim(CStr(FormatNumber(totalrp, 2)))
            strTerbilang = objGlobal.TerbilangDesimal(Replace(strTotalrp, ",", ""), "Rupiah")
        End If

        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strFileName & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Portrait
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA4

        crvView.Visible = False
        crvView.ReportSource = rdCrystalViewer
        crvView.DataBind()

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()

        If strExportToExcel = "0" Then
            'crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strRptPrefix & ".pdf"
            crDiskFileDestinationOptions.DiskFileName = objFTPFolder & strFileName & ".pdf"
        Else
            'crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strFileName & ".xls"
            crDiskFileDestinationOptions.DiskFileName = objFTPFolder & strFileName & ".xls"
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
            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../" & strUrl & strFileName & ".pdf"">")
            'Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strFileName & ".pdf"">")
        Else
            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../" & strUrl & strFileName & ".xls"">")
            'Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strFileName & ".xls"">")
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

        ParamDiscreteValue1.Value = strCompanyName
        ParamDiscreteValue2.Value = strLocation
        ParamDiscreteValue3.Value = strPrintedBy
        ParamDiscreteValue4.Value = intSelDecimal
        ParamDiscreteValue5.Value = strRptId
        ParamDiscreteValue6.Value = strRptName
        ParamDiscreteValue7.Value = strLocationName
        ParamDiscreteValue8.Value = strNo
        ParamDiscreteValue9.Value = strDate
        ParamDiscreteValue10.Value = strTo
        ParamDiscreteValue11.Value = strAlamat
        ParamDiscreteValue12.Value = strTerbilang
        ParamDiscreteValue13.Value = strAssignBy
        ParamDiscreteValue14.Value = strCheckBy
        ParamDiscreteValue15.Value = strJbtn1
        ParamDiscreteValue16.Value = strJbtn2

        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        ParamFieldDef1 = ParamFieldDefs.Item("ParamCompanyName")
        ParamFieldDef2 = ParamFieldDefs.Item("ParamLocation")
        ParamFieldDef3 = ParamFieldDefs.Item("ParamUserName")
        ParamFieldDef4 = ParamFieldDefs.Item("ParamDecimal")
        ParamFieldDef5 = ParamFieldDefs.Item("ParamRptID")
        ParamFieldDef6 = ParamFieldDefs.Item("ParamRptName")
        ParamFieldDef7 = ParamFieldDefs.Item("ParamLocationName")
        ParamFieldDef8 = ParamFieldDefs.Item("ParamNomor")
        ParamFieldDef9 = ParamFieldDefs.Item("ParamTanggal")
        ParamFieldDef10 = ParamFieldDefs.Item("ParamKepada")
        ParamFieldDef11 = ParamFieldDefs.Item("ParamAlamat")
        ParamFieldDef12 = ParamFieldDefs.Item("ParamTerbilang")
        ParamFieldDef13 = ParamFieldDefs.Item("ParamAssignBy")
        ParamFieldDef14 = ParamFieldDefs.Item("ParamCheckBy")
        ParamFieldDef15 = ParamFieldDefs.Item("ParamJbtn1")
        ParamFieldDef16 = ParamFieldDefs.Item("ParamJbtn2")

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
    End Sub

    Sub onload_GetLangCap()

        lblBlkCode = " Block Code"
        lblVehCode = " Veh Code"
        lblVehExpCode = " Veh Exp Code"
        lblLocation = "Location"
        lblAccCode = "Account Code"
        lblAccType = " Account Type"
    End Sub

End Class
