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

Public Class PR_StdRpt_bukumandorPreview_Estate : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objPRRpt As New agri.PR.clsReport()
    Dim objHRReport As New agri.HR.clsReport()
    Dim objOk As New agri.GL.clsReport

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
    Dim strPhyMonth As String
    Dim strPhyYear As String
    Dim strDateFrom As String
    Dim strDateTo As String

    Dim strEmpCode As String
    Dim strDivisi As String
    Dim strMandor As String
    Dim objFTPFolder As String

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

        strAccMonth = Request.QueryString("AccMonth")
        strAccYear = Request.QueryString("AccYear")
        strRptName = Request.QueryString("RptName")
        strLangCode = Session("SS_LANGCODE")
        strPhyMonth = Request.QueryString("AccMonth")
        strPhyYear = Request.QueryString("AccYear")
        strDateFrom = Request.QueryString("SelDateFrom")
        strDateTo = Request.QueryString("SelDateTo")
        strEmpCode = Request.QueryString("EmpCode")
        strDivisi = Request.QueryString("Divisi")
        strMandor = Request.QueryString("Mandor")


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
        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim strOpCd As String = ""
        Dim fname As String = ""
        Dim strParamName As String
        Dim strParamValue As String
        Dim ReportID As String = Request.QueryString("RptID")
        Dim SubCat As String = Request.QueryString("SubCat")

        Select Case ReportID.Trim
            Case "RPTPR1000060"
                Select Case SubCat.Trim
                    Case "PBS"
                        strOpCd = "PR_PR_STDRPT_BUKUMANDOR_TBS_REPORT"
                        fname = "PR_StdRpt_BMandor_Tbs_Estate"
                    Case "KBS"
                        strOpCd = "PR_PR_STDRPT_BUKUMANDOR_KBS_REPORT"
                        fname = "PR_StdRpt_BMandor_Kbs_Estate"
                    Case "DRK"
                        strOpCd = "PR_PR_STDRPT_BUKUMANDOR_DRK_REPORT"
                        fname = "PR_StdRpt_BMandor_Drk_Estate"
                End Select
            Case "RPTPR1000061"
                Select Case SubCat.Trim
                    Case "PBS"
                        strOpCd = "PR_PR_STDRPT_BUKUKCS_TBS_REPORT"
                        fname = "PR_StdRpt_BKcs_Tbs_Estate"
                    Case "KBS"
                        strOpCd = "PR_PR_STDRPT_BUKUKCS_KBS_REPORT"
                        fname = "PR_StdRpt_BKcs_Kbs_Estate"
                End Select
            Case "RPTPR1000062"
                strOpCd = "PR_PR_STDRPT_BUKU_MANDOR_RAWAT_REPORT"
                fname = "PR_StdRpt_BMandor_Rawat_Estate"
        End Select

        strParamName = "ACCMONTH|ACCYEAR|PHYMONTH|PHYYEAR|COMPCODE|LOCCODE|DATEFROM|DATETO|DECIMAL|F_EMPCODE|F_DIVCODE|F_MCODE"
        strParamValue = strAccMonth & "|" & _
                        strAccYear & "|" & _
                        strAccMonth & "|" & _
                        strAccYear & "|" & _
                        strCompany & "|" & _
                        strLocation & "|" & _
                        strDateFrom & "|" & _
                        strDateTo & "|" & _
                        intDecimal & "|" & _
                        strEmpCode & "|" & _
                        strDivisi & "|" & _
                        strMandor

        Try
            intErrNo = objOk.mtdGetReportDataCommon(strOpCd, strParamName, strParamValue, objRptDs, objMapPath, objFTPFolder )

        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PR_PR_STDRPT_BUKUMANDOR_REPORT&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\reports\crystal\" & fname & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))


        crvView.Visible = False
        crvView.ReportSource = rdCrystalViewer
        crvView.DataBind()

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & fname & ".pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        rdCrystalViewer.Close()
        rdCrystalViewer.Dispose()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & fname & ".pdf"">")
    End Sub

    Sub PassParam()
        Dim paramFields As New ParameterFields()
        Dim paramField1 As New ParameterField()

        Dim ParamDiscreteValue1 As New ParameterDiscreteValue()

        Dim crParameterValues1 As ParameterValues


        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("ParamUserName")


        crParameterValues1 = paramField1.CurrentValues


        ParamDiscreteValue1.Value = Session("SS_USERNAME")


        crParameterValues1.Add(ParamDiscreteValue1)


        PFDefs(0).ApplyCurrentValues(crParameterValues1)

        crvView.ParameterFieldInfo = paramFields
    End Sub
End Class
