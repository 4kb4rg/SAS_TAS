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

Public Class WM_StdRpt_LetterToSupp_FFBRcv_SumPreview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objWM As New agri.WM.clsReport()
    Dim objWMTrx As New agri.WM.clsTrx()
    Dim objPUSetup As New agri.PU.clsSetup()
    Dim objPWSystem As New agri.PWSystem.clsConfig()
    Dim objAdmin As New agri.Admin.clsAccPeriod()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()  
    Dim objAdminCty As New agri.Admin.clsCountry

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strLangCode As String
    Dim strAPAccMonth As String
    Dim strAPAccYear As String
    Dim strGLAccMonth As String
    Dim strGLAccYear As String
    Dim strSSPhyMonth As String
    Dim strSSPhyYear As String
    Dim intModuleActivate As Integer
    Dim strUserLoc As String
    Dim strRptTitle As String
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
        strLangCode = Session("SS_LANGCODE")
        strAPAccMonth = Session("SS_APACCMONTH")
        strAPAccYear = Session("SS_APACCYEAR")
        strGLAccMonth = Session("SS_GLACCMONTH")
        strGLAccYear = Session("SS_GLACCYEAR")
        strSSPhyMonth = Session("SS_PHYMONTH")
        strSSPhyYear = Session("SS_PHYYEAR")
        intModuleActivate = Session("SS_MODULEACTIVATE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            BindReport()
        End If
    End Sub

    Sub BindReport()
        Dim strPhyMonth As String
        Dim strPhyYear As String

        Dim dsFFBRcv As New DataSet()
        Dim dsCountry As New Object()
        Dim objMapPath As String

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOpCd_LetterToSupp_FFBRcvSum_GET As String = "WM_STDRPT_FFBRCVSUM_BYPLANTYR_GET" 
        Dim strOpCd_Country_GET As String = "ADMIN_CLSCOUNTRY_COUNTRY_DETAILS_GET"
        Dim strFileName As String = "WM_StdRpt_LetterToSuppFFBRcvSum"

        Dim strParam As String
        Dim searchStr As String
        Dim intCnt As Integer

        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.AccountPayable), intModuleActivate) Then
            

            Try
                intErrNo = objPWSystem.mtdGetPhyPeriod(strCompany, _
                                                       strLocation, _
                                                       strUserId, _
                                                       strAPAccMonth, _
                                                       strAPAccYear, _
                                                       Trim(Request.QueryString("strddlAccMth")), _
                                                       Trim(Request.QueryString("strddlAccYr")), _
                                                       strSSPhyMonth, _
                                                       strSSPhyYear, _
                                                       strPhyMonth, _
                                                       strPhyYear)
            Catch Exp As System.Exception
                Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=WM_STDRPT_LETTERTOSUPPFFBRCV_GETPHYPERIOD&errmesg=&redirect=")
            End Try
        Else
            Try
                intErrNo = objPWSystem.mtdGetPhyPeriod(strCompany, _
                                                       strLocation, _
                                                       strUserId, _
                                                       strGLAccMonth, _
                                                       strGLAccYear, _
                                                       Trim(Request.QueryString("strddlAccMth")), _
                                                       Trim(Request.QueryString("strddlAccYr")), _
                                                       strSSPhyMonth, _
                                                       strSSPhyYear, _
                                                       strPhyMonth, _
                                                       strPhyYear)
            Catch Exp As System.Exception
                Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=WM_STDRPT_LETTERTOSUPPFFBRCV_GETPHYPERIOD&errmesg=&redirect=")
            End Try
        End If

        strRptTitle = "FFB Supplied For The Month Of " & Trim(objGlobal.GetLongMonth(strPhyMonth)) & " " & strPhyYear

        If Not Trim(Request.QueryString("SuppCode")) = "" Then
            searchStr = "AND TIC.CustomerCode LIKE '" & Trim(Request.QueryString("SuppCode")) & "' "
        End If

        If Not Request.QueryString("SuppType") = objPUSetup.EnumSupplierType.All Then
            searchStr = searchStr & "AND SUP.SuppType = '" & Request.QueryString("SuppType") & "' "
        End If

        strParam = strUserLoc & "|" & Request.QueryString("strddlAccMth") & "|" & _
                   Request.QueryString("strddlAccYr") & "|" & _
                   objWMTrx.EnumWeighBridgeTicketTransType.Purchase & "|" & _
                   objWMTrx.EnumWeighBridgeTicketStatus.Active & "|" & _
                   searchStr
        Try
            intErrNo = objWM.mtdGetReport_FFBRcvSumByPlantYrReport(strOpCd_LetterToSupp_FFBRcvSum_GET, _
                                                                   strParam, _
                                                                   dsFFBRcv, _
                                                                   objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=WM_STDRPT_LETTOSUPP_FFBRCVSUM_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If dsFFBRcv.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If

        For intCnt = 0 To dsFFBRcv.Tables(0).Rows.Count - 1
            strParam = dsFFBRcv.Tables(0).Rows(intCnt).item("CountryCode").trim() & "|"

            Try
                intErrNo = objAdminCty.mtdGetCountryDetails(strOpCd_Country_GET, dsCountry, strParam)

            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=AP_STDRPT_STMTOFACC_COUNTRYDET&errmesg=" & Exp.ToString() & "&redirect=")
            End Try

            If (dsFFBRcv.Tables(0).Rows(intCnt).item("CountryCode").trim() = "") Then
                dsFFBRcv.Tables(0).Rows(intCnt).Item("CountryDesc") = ""
            Else
                dsFFBRcv.Tables(0).Rows(intCnt).Item("CountryDesc") = dsCountry.tables(0).rows(0).item("CountryDesc").trim()
            End If
        Next
            

        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strFileName & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(dsFFBRcv.Tables(0))

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Portrait
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA4

        crvView.Visible = False                         
        crvView.ReportSource = rdCrystalViewer          
        crvView.DataBind()                              

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
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
        Dim paramField7 As New ParameterField()

        Dim ParamDiscreteValue1 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue2 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue3 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue4 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue5 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue6 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue7 As New ParameterDiscreteValue()

        Dim crParameterValues1 As ParameterValues
        Dim crParameterValues2 As ParameterValues
        Dim crParameterValues3 As ParameterValues
        Dim crParameterValues4 As ParameterValues
        Dim crParameterValues5 As ParameterValues
        Dim crParameterValues6 As ParameterValues
        Dim crParameterValues7 As ParameterValues

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("ParamDecimal")
        paramField2 = paramFields.Item("ParamAccMonth")
        paramField3 = paramFields.Item("ParamAccYear")
        paramField4 = paramFields.Item("ParamRefNo")
        paramField5 = paramFields.Item("ParamRptTitle")
        paramField6 = paramFields.Item("ParamCompanyName")
        paramField7 = paramFields.Item("ParamLocation")

        crParameterValues1 = paramField1.CurrentValues
        crParameterValues2 = paramField2.CurrentValues
        crParameterValues3 = paramField3.CurrentValues
        crParameterValues4 = paramField4.CurrentValues
        crParameterValues5 = paramField5.CurrentValues
        crParameterValues6 = paramField6.CurrentValues  
        crParameterValues7 = paramField7.CurrentValues

        ParamDiscreteValue1.Value = Request.QueryString("Decimal")
        ParamDiscreteValue2.Value = Request.QueryString("strddlAccMth")
        ParamDiscreteValue3.Value = Request.QueryString("strddlAccYr")
        ParamDiscreteValue4.Value = Request.QueryString("RefNo")
        ParamDiscreteValue5.Value = strRptTitle
        ParamDiscreteValue6.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue7.Value = Session("SS_LOCATION")

        crParameterValues1.Add(ParamDiscreteValue1)
        crParameterValues2.Add(ParamDiscreteValue2)
        crParameterValues3.Add(ParamDiscreteValue3)
        crParameterValues4.Add(ParamDiscreteValue4)
        crParameterValues5.Add(ParamDiscreteValue5)
        crParameterValues6.Add(ParamDiscreteValue6)
        crParameterValues7.Add(ParamDiscreteValue7)

        PFDefs(0).ApplyCurrentValues(crParameterValues1)
        PFDefs(1).ApplyCurrentValues(crParameterValues2)
        PFDefs(2).ApplyCurrentValues(crParameterValues3)
        PFDefs(3).ApplyCurrentValues(crParameterValues4)
        PFDefs(4).ApplyCurrentValues(crParameterValues5)
        PFDefs(5).ApplyCurrentValues(crParameterValues6)
        PFDefs(6).ApplyCurrentValues(crParameterValues7)

        crvView.ParameterFieldInfo = paramFields
    End Sub
End Class
