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
Imports System.Web.Services
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web

Imports agri.PWSystem.clsLangCap

Public Class AR_StdRpt_StmtOfAccountPreview : Inherits Page

    Dim objBIRpt As New agri.BI.clsReport()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objBITrx As New agri.BI.clsTrx()
    Dim objGLSetup As New agri.GL.clsSetup()

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents lblNoRecord As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim rdCrystalViewer As ReportDocument
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
    Dim strReportTitle As String
    Dim strSelPhyMonth As String
    Dim strSelPhyYear As String
    Dim strPhyMonth As String
    Dim strPhyYear As String

    Dim strRptId As String
    Dim strRptName As String
    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim intSelDecimal As Integer
    Dim strSelSupress As String
    Dim strUserLoc As String
    Dim strBillPartyCode As String
    Dim strCutOffDate As String
    Dim strToAge1 As String
    Dim strFromAge2 As String
    Dim strToAge2 As String
    Dim strFromAge3 As String
    Dim strToAge3 As String
    Dim strFromAge4 As String
    Dim strToAge4 As String
    Dim strFromAge5 As String

    Dim strCategory1 As String
    Dim strCategory2 As String
    Dim strCategory3 As String
    Dim strCategory4 As String
    Dim strCategory5 As String

    Dim strSearchExp As String = ""
    Dim strSearchExp_Age As String = ""
    Dim BillPartyTag As String
    Dim AccCodeTag As String    
    Dim BlkCodeTag As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        crvView.Visible = False
        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strLocationName = Session("SS_LOCATIONNAME")
        strUserId = Session("SS_USERID")
        strPrintedBy = Session("SS_USERNAME")
        strAccMonth = Session("SS_ARACCMONTH")
        strAccYear = Session("SS_ARACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strPhyMonth = Session("SS_PHYMONTH")
        strPhyYear = Session("SS_PHYYEAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            strUserLoc = Trim(Request.QueryString("Location"))
            strRptId = Trim(Request.QueryString("RptId"))
            strRptName = Trim(Request.QueryString("RptName"))
            strSelAccMonth = Request.QueryString("SelAccMonth")
            strSelAccYear = Request.QueryString("SelAccYear")
            intSelDecimal= CInt(Request.QueryString("Decimal"))
            strSelSupress = Request.QueryString("Supp")

            strBillPartyCode = Trim(Request.QueryString("BillPartyCode"))
            strCutOffDate = Trim(Request.QueryString("CutOffDate"))
            strToAge1 = Trim(Request.QueryString("ToAge1"))
            strFromAge2 = Trim(Request.QueryString("FromAge2"))
            strToAge2 = Trim(Request.QueryString("ToAge2"))
            strFromAge3 = Trim(Request.QueryString("FromAge3"))
            strToAge3 = Trim(Request.QueryString("ToAge3"))
            strFromAge4 = Trim(Request.QueryString("FromAge4"))
            strToAge4 = Trim(Request.QueryString("ToAge4"))
            strFromAge5 = Trim(Request.QueryString("FromAge5"))
            BillPartyTag = Trim(Request.QueryString("BillPartyTag"))
            AccCodeTag = Trim(Request.QueryString("AccCodeTag"))
            BlkCodeTag = Trim(Request.QueryString("BlkCodeTag"))

      
            If Trim(strBillPartyCode) <> "" Then
                strSearchExp = "and bill.BillPartyCode like '" & strBillPartyCode & "' " & _
                               "and bill.Status = '" & objGLSetup.EnumBillPartyStatus.Active & "' " 
                strSearchExp_Age = "and bill.BillPartyCode like '" & strBillPartyCode & "' " & _
                                    "and bill.Status = '" & objGLSetup.EnumBillPartyStatus.Active & "' "
            Else 
                strSearchExp = ""
                strSearchExp_Age = ""
            End If

            strCategory1 = "1-" & strToAge1
            strCategory2 = strFromAge2 & "-" & strToAge2
            strCategory3 = strFromAge3 & "-" & strToAge3
            strCategory4 = strFromAge4 & "-" & strToAge4
            strCategory5 = strFromAge5

            BindReport()
        End If
    End Sub


    Sub BindReport()
        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim strOpCd As String
        Dim objRptDs As New Dataset()
        Dim objMapPath As String
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strRptPrefix As String
        Dim intCnt As Integer

        Try
            intErrNo = objSysCfg.mtdGetPhyPeriod(strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strAccMonth, _
                                                strAccYear, _
                                                strSelAccMonth, _
                                                strSelAccYear, _
                                                strPhyMonth, _
                                                strPhyYear, _
                                                strSelPhyMonth, _
                                                strSelPhyYear)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=AR_STDRPT_PHYPERIOD_GET&errmesg=" & Server.UrlEncode(Exp.ToString()) & "&redirect=")
        End Try
        strReportTitle = "STATEMENT OF ACCOUNT FOR THE MONTH OF " & UCase(objGlobal.GetLongMonth(strSelPhyMonth)) & " " & strSelPhyYear

        strRptPrefix = "AR_StdRpt_StmtOfAccount"
        strOpCd = "AR_STDRPT_STMT_OF_ACCOUNT_GET_SP" & "|" & "AR_STMTACC" & Chr(9) & _
                  "AR_STDRPT_STMT_OF_ACCOUNT_AGEING_GET_SP" & "|" & "AR_STMTACC_AGEING"

        strParam = strSelAccMonth & "|" & _
                   strSelAccYear & "|" & _
                   strSearchExp & "|" & _
                   strCutOffDate & "|" & _
                   strToAge1 & "|" & _
                   strFromAge2 & "|" & _
                   strToAge2 & "|" & _
                   strFromAge3 & "|" & _
                   strToAge3 & "|" & _
                   strFromAge4 & "|" & _
                   strToAge4 & "|" & _
                   strFromAge5 & "|" & _
                   strSearchExp_Age

        Try
            intErrNo = objBIRpt.mtdGetReport_StmtOfAccount(strOpCd, _
                                                           strCompany, _
                                                           strLocation, _
                                                           strUserId, _
                                                           strAccMonth, _
                                                           strAccYear, _
                                                           strParam, _
                                                           objRptDs, _
                                                           objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=AR_STDRPT_STMTOFACCOUNT_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try


   
        rdCrystalViewer = New ReportDocument()
        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strRptPrefix & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs)
        


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

        Dim ParameterValues1 As New ParameterValues()
        Dim ParameterValues2 As New ParameterValues()
        Dim ParameterValues3 As New ParameterValues()
        Dim ParameterValues4 As New ParameterValues()
        Dim ParameterValues5 As New ParameterValues()
        Dim ParameterValues6 As New ParameterValues()
        Dim ParameterValues7 As New ParameterValues()

        Dim ParamDiscreteValue1 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue2 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue3 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue4 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue5 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue6 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue7 As New ParameterDiscreteValue()

        ParamDiscreteValue1.Value = strPrintedBy
        ParamDiscreteValue2.Value = strCategory1
        ParamDiscreteValue3.Value = strCategory2
        ParamDiscreteValue4.Value = strCategory3
        ParamDiscreteValue5.Value = strCategory4
        ParamDiscreteValue6.Value = strCategory5
        ParamDiscreteValue7.Value = strReportTitle

        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        ParamFieldDef1 = ParamFieldDefs.Item("SessionUserName")
        ParamFieldDef2 = ParamFieldDefs.Item("Category1")
        ParamFieldDef3 = ParamFieldDefs.Item("Category2")
        ParamFieldDef4 = ParamFieldDefs.Item("Category3")
        ParamFieldDef5 = ParamFieldDefs.Item("Category4")
        ParamFieldDef6 = ParamFieldDefs.Item("Category5")
        ParamFieldDef7 = ParamFieldDefs.Item("ReportTitle")

        ParameterValues1 = ParamFieldDef1.CurrentValues
        ParameterValues2 = ParamFieldDef2.CurrentValues
        ParameterValues3 = ParamFieldDef3.CurrentValues
        ParameterValues4 = ParamFieldDef4.CurrentValues
        ParameterValues5 = ParamFieldDef5.CurrentValues
        ParameterValues6 = ParamFieldDef6.CurrentValues
        ParameterValues7 = ParamFieldDef7.CurrentValues

        ParameterValues1.Add(ParamDiscreteValue1)
        ParameterValues2.Add(ParamDiscreteValue2)
        ParameterValues3.Add(ParamDiscreteValue3)
        ParameterValues4.Add(ParamDiscreteValue4)
        ParameterValues5.Add(ParamDiscreteValue5)
        ParameterValues6.Add(ParamDiscreteValue6)
        ParameterValues7.Add(ParamDiscreteValue7)

        ParamFieldDef1.ApplyCurrentValues(ParameterValues1)
        ParamFieldDef2.ApplyCurrentValues(ParameterValues2)
        ParamFieldDef3.ApplyCurrentValues(ParameterValues3)
        ParamFieldDef4.ApplyCurrentValues(ParameterValues4)
        ParamFieldDef5.ApplyCurrentValues(ParameterValues5)
        ParamFieldDef6.ApplyCurrentValues(ParameterValues6)
        ParamFieldDef7.ApplyCurrentValues(ParameterValues7)

    End Sub

End Class
