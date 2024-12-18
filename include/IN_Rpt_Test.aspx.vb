Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic
Imports System.XML
Imports System.Web.Services
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web

Imports agri.IN.clsTrx
Imports agri.Admin.clsShare
Imports agri.Admin.clsComp
Imports agri.Admin.clsLoc
Imports agri.GlobalHdl.clsGlobalHdl

Public Class IN_Rpt_Test : inherits page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents lblErrMesage As Label

    Dim objIn As New agri.IN.clsTrx()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objComp As New agri.Admin.clsComp()
    Dim objLoc As New agri.Admin.clsLoc()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim rdCrystalViewer As New ReportDocument()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String

    Dim strStatus As String
    Dim strStockTxID As String
    Dim strRefNo As String
    Dim strUpdateBy As String
    Dim strSortExp As String
    Dim strSortCol As String

    Public Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        
        crvView.Visible = False  

        strStatus = Trim(Request.QueryString("strStatus"))
        strStockTxID = Trim(Request.QueryString("strStockTxID"))
        strRefNo = Trim(Request.QueryString("strRefNo"))
        strUpdateBy = Trim(Request.QueryString("strUpdateBy"))
        strSortExp = Trim(Request.QueryString("strSortExp"))
        strSortCol = Trim(Request.QueryString("strSortCol"))

        Bind_ITEM(True)

    End Sub

    Sub Bind_ITEM(ByVal blnIsPDFFormat As Boolean)
        Dim crLogonInfo As CrystalDecisions.Shared.TableLogOnInfo
        Dim objRptDs As New Dataset()
        Dim objMapPath As New Object()
        Dim objCompDs As New Dataset()
        Dim objLocDs As New Dataset()
        Dim strOpCd_Stock As String = "IN_CLSTRX_STOCKRECEIVE_LIST_GET_FOR_REPORT" & "|" & "Stock"
        Dim strOpCd_Comp As String = "ADMIN_CLSCOMP_COMPANY_DETAILS_GET" & "|" & "Company"
        Dim strOpCd_Loc As String = "ADMIN_CLSLOC_LOCATION_DETAILS_GET" & "|" & "Location"
        Dim strOpCodes As String
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim strSearch As String
        Dim strSortItem As String
        Dim intCnt As Integer
        Dim strCompName As String
        Dim strLocName As String
        Dim dr As DataRow
        
        strParam = strStockTxID & "|" & strRefNo & "|" & strUpdateBy & "|" & _
                   strStatus & "|" & strSortExp & "|" & strSortCol & "|" & _
                   strAccMonth & "|" & strAccYear & "|" & strLocation & "|" & _
                   strCompany & "|" & strLocation

        strOpCodes = strOpCd_Stock & chr(9) & strOpCd_Comp & chr(9) & strOpCd_Loc

        Try
            intErrNo = objIN.mtdGetStockReceiveListRpt(strOpCodes, strParam, objRptDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=&errmesg=" & lblErrMesage.Text & "&redirect=")
        End Try

        Try
            intErrNo = objAdmin.mtdGetBasePath(objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=&errmesg=" & lblErrMesage.Text & "&redirect=")
        End Try



        


        rdCrystalViewer.Load(objMapPath & "Web\EN\IN\Reports\Crystal\In_Rpt_Test.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs)

        If Not blnIsPDFFormat Then
            crvView.Visible = True     
            crvView.ReportSource = rdCrystalViewer
            crvView.DataBind()
        Else
            Dim DiskOpts As CrystalDecisions.Shared.DiskFileDestinationOptions = New CrystalDecisions.Shared.DiskFileDestinationOptions()
            rdCrystalViewer.ExportOptions.ExportDestinationType = CrystalDecisions.[Shared].ExportDestinationType.DiskFile
            rdCrystalViewer.ExportOptions.ExportFormatType = CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat
            DiskOpts.DiskFileName = objMapPath & "web\ftp\In_Rpt_Test.pdf"
            rdCrystalViewer.ExportOptions.DestinationOptions = DiskOpts
            rdCrystalViewer.Export()

            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../../ftp/In_Rpt_Test.pdf"">")
        End If

    End Sub



End Class

