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

Public Class AR_StdRpt_DebtorAccountAgeingPreview : Inherits Page

    Dim objBIRpt As New agri.BI.clsReport()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
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

    Dim strRptId As String
    Dim strRptName As String
    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim intSelDecimal As Integer
    Dim strSelSuppress As String
    Dim strUserLoc As String
    Dim strBillPartyCode As String
    Dim strAccCode As String
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
    Dim LocationTag As String
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

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            strUserLoc = Trim(Request.QueryString("Location"))
            strRptId = Trim(Request.QueryString("RptId"))
            strRptName = Trim(Request.QueryString("RptName"))
            strSelAccMonth = Request.QueryString("SelAccMonth")
            strSelAccYear = Request.QueryString("SelAccYear")
            intSelDecimal= CInt(Request.QueryString("Decimal"))
            strSelSuppress = Request.QueryString("Supp")

            strBillPartyCode = Trim(Request.QueryString("BillPartyCode"))
            strAccCode = Trim(Request.QueryString("AccCode"))
            strCutOffDate = Trim(Request.QueryString("CutOffDate"))
            strToAge1 = Trim(Request.QueryString("ToAge1"))
            strFromAge2 = Trim(Request.QueryString("FromAge2"))
            strToAge2 = Trim(Request.QueryString("ToAge2"))
            strFromAge3 = Trim(Request.QueryString("FromAge3"))
            strToAge3 = Trim(Request.QueryString("ToAge3"))
            strFromAge4 = Trim(Request.QueryString("FromAge4"))
            strToAge4 = Trim(Request.QueryString("ToAge4"))
            strFromAge5 = Trim(Request.QueryString("FromAge5"))
            LocationTag = Trim(Request.QueryString("LocationTag"))
            BillPartyTag = Trim(Request.QueryString("BillPartyTag"))
            AccCodeTag = Trim(Request.QueryString("AccCodeTag"))
            BlkCodeTag = Trim(Request.QueryString("BlkCodeTag"))

            If Trim(strBillPartyCode) <> "" Then
                strSearchExp = "and bill.BillPartyCode like '" & strBillPartyCode & "' " & _
                               "and bill.Status = '" & objGLSetup.EnumBillPartyStatus.Active & "' "
            End If

            If Trim(strAccCode) <> "" Then
                strSearchExp = strSearchExp & "and bill.AccCode like '" & strAccCode & "' "
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

        strRptPrefix = "AR_StdRpt_DebtAccAgeing"
        strOpCd = "AR_STDRPT_DEBTOR_ACCOUNT_AGEING_GET_SP" & "|" & "AR_DEBTACCAGE"

        If instr(strUserLoc, "','") > 0 Then
            strUserLoc = Replace(strUserLoc, "','", "'',''")
        End If

        strParam = strUserLoc & "|" & _
                   strSelAccMonth & "|" & _
                   strSelAccYear & "|" & _
                   strSearchExp & "|" & _
                   Request.QueryString("Supp") & "|" & _ 
                   strCutOffDate & "|" & _
                   strToAge1 & "|" & _
                   strFromAge2 & "|" & _
                   strToAge2 & "|" & _
                   strFromAge3 & "|" & _
                   strToAge3 & "|" & _
                   strFromAge4 & "|" & _
                   strToAge4 & "|" & _
                   strFromAge5


        Try
            intErrNo = objBIRpt.mtdGetReport_DebtorAccAgeing(strOpCd, _
                                                             strCompany, _
                                                             strLocation, _
                                                             strUserId, _
                                                             strAccMonth, _
                                                             strAccYear, _
                                                             strParam, _
                                                             objRptDs, _
                                                             objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=AR_STDRPT_DEBTACCAGE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try


   
        rdCrystalViewer = New ReportDocument()
        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strRptPrefix & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs)
        
        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3


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
        Dim ParamFieldDef15 As ParameterFieldDefinition
        Dim ParamFieldDef16 As ParameterFieldDefinition
        Dim ParamFieldDef17 As ParameterFieldDefinition
        Dim ParamFieldDef18 As ParameterFieldDefinition
        Dim ParamFieldDef19 As ParameterFieldDefinition
        Dim ParamFieldDef20 As ParameterFieldDefinition
        Dim ParamFieldDef21 As ParameterFieldDefinition

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

        strUserLoc = Replace(strUserLoc, "'',''", ", ")

        ParamDiscreteValue1.Value = strRptId
        ParamDiscreteValue2.Value = strRptName
        ParamDiscreteValue3.Value = strUserLoc
        ParamDiscreteValue4.Value = strSelAccMonth
        ParamDiscreteValue5.Value = strSelAccYear
        ParamDiscreteValue6.Value = intSelDecimal
        ParamDiscreteValue7.Value = strSelSuppress
        ParamDiscreteValue8.Value = strCompanyName
        ParamDiscreteValue9.Value = strLocationName
        ParamDiscreteValue10.Value = strPrintedBy
        ParamDiscreteValue11.Value = LocationTag
        ParamDiscreteValue12.Value = BillPartyTag
        ParamDiscreteValue13.Value = strBillPartyCode
        ParamDiscreteValue14.Value = AccCodeTag
        ParamDiscreteValue15.Value = strAccCode
        ParamDiscreteValue16.Value = strCategory1
        ParamDiscreteValue17.Value = strCategory2
        ParamDiscreteValue18.Value = strCategory3
        ParamDiscreteValue19.Value = strCategory4
        ParamDiscreteValue20.Value = strCategory5
        ParamDiscreteValue21.Value = strCutOffDate

        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        ParamFieldDef1 = ParamFieldDefs.Item("RptId")
        ParamFieldDef2 = ParamFieldDefs.Item("RptName")
        ParamFieldDef3 = ParamFieldDefs.Item("SelLocCode")
        ParamFieldDef4 = ParamFieldDefs.Item("SelAccMonth")
        ParamFieldDef5 = ParamFieldDefs.Item("SelAccYear")
        ParamFieldDef6 = ParamFieldDefs.Item("SelDecimal")
        ParamFieldDef7 = ParamFieldDefs.Item("SelSuppress")
        ParamFieldDef8 = ParamFieldDefs.Item("SessionCompName")
        ParamFieldDef9 = ParamFieldDefs.Item("SessionLocName")
        ParamFieldDef10 = ParamFieldDefs.Item("SessionUserName")
        ParamFieldDef11 = ParamFieldDefs.Item("LocationTag")
        ParamFieldDef12 = ParamFieldDefs.Item("BillPartyTag")
        ParamFieldDef13 = ParamFieldDefs.Item("SrchBillParty")
        ParamFieldDef14 = ParamFieldDefs.Item("AccCodeTag")
        ParamFieldDef15 = ParamFieldDefs.Item("SrchAccCode")
        ParamFieldDef16 = ParamFieldDefs.Item("Category1")
        ParamFieldDef17 = ParamFieldDefs.Item("Category2")
        ParamFieldDef18 = ParamFieldDefs.Item("Category3")
        ParamFieldDef19 = ParamFieldDefs.Item("Category4")
        ParamFieldDef20 = ParamFieldDefs.Item("Category5")
        ParamFieldDef21 = ParamFieldDefs.Item("CutOffDate")

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

    End Sub

End Class
