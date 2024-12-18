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

Imports agri.GlobalHdl.clsGlobalHdl

Public Class AR_StdRpt_FakturPajakStandarPreview : Inherits Page

    Dim objARRpt As New agri.BI.clsReport()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objBITrx As New agri.BI.clsTrx()

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents lblNoRecord As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid
    Protected WithEvents lblErrMesage As Label
    Dim objCompany As New agri.Admin.clsComp()
    Dim objCountryDesc As New agri.Admin.clsCountry()
    Dim objSysLoc As New agri.PWSystem.clsConfig()

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
    Dim strSelSupress As String
    Dim strUserLoc As String
    Dim strFaktur As String
    Dim strTglBayar As String
    Dim strProduct As String
    Dim strUOM As String
    Dim strContract As String
    Dim strInvoice As String
    Dim strUndName As String
    Dim strUndPost As String
    Dim strOptionNo As String
    Dim strOptionDesc As String
    Dim strPPN As String
    Dim strSearchExp As String = ""
    Dim strCompanyAddress As String

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
        strCompanyAddress = Session("SS_COMPANYADDRESS")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            strUserLoc = Trim(Request.QueryString("Location"))
            strRptId = Trim(Request.QueryString("RptId"))
            strRptName = Trim(Request.QueryString("RptName"))
            strSelAccMonth = Request.QueryString("SelAccMonth")
            strSelAccYear = Request.QueryString("SelAccYear")
            intSelDecimal = CInt(Request.QueryString("Decimal"))
            strSelSupress = Request.QueryString("Supp")

            strFaktur = Trim(Request.QueryString("Faktur"))
            strTglBayar = Trim(Request.QueryString("TglBayar"))
            strProduct = Trim(Request.QueryString("Product"))
            strUOM = Trim(Request.QueryString("UOM"))
            strContract = Trim(Request.QueryString("Contract"))
            strInvoice = Trim(Request.QueryString("Invoice"))
            strUndName = Trim(Request.QueryString("UndName"))
            strUndPost = Trim(Request.QueryString("UndPost"))
            strOptionNo = Request.QueryString("OptionNo")
            strOptionDesc = Request.QueryString("OptionDesc")
            strPPN = Request.QueryString("PPN")


            strSearchExp = "and inv.LocCode = '" & strUserLoc & "' " & _
                            "and inv.DocType = '" & objBITrx.EnumInvoiceDocType.Manual & "' " & _
                                "and inv.Status IN ('" & objBITrx.EnumInvoiceStatus.Confirmed & "','" & objBITrx.EnumInvoiceStatus.Confirmed & "') "



            If Trim(strInvoice) <> "" Then
                strSearchExp = strSearchExp & "and inv.InvoiceId >= '" & strInvoice & "' "
            End If

            If Trim(strInvoice) <> "" Then
                strSearchExp = strSearchExp & "and inv.InvoiceId <= '" & strInvoice & "' "
            End If

            BindReport()
        End If
    End Sub


    Sub BindReport()
        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim strOpCd As String
        Dim strOpCdRslGet As String = ""
        Dim objRptDs As New DataSet()
        Dim objMapPath As String
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strRptPrefix As String
        Dim intCnt As Integer
        Dim totalRp As Double

        strRptPrefix = "AR_StdRpt_FakturPajakStandar"
        strOpCd = "AR_STDRPT_MANUAL_INVOICE_GET_BILL" & "|" & "AR_INVOICE"

        strParam = strSelAccMonth & "|" & _
                   strSelAccYear & "|" & _
                   strSearchExp & "|" & _
                   objBITrx.EnumInvoiceStatus.Active & "|" & _
                   objBITrx.EnumInvoiceStatus.Confirmed


        Try
            intErrNo = objARRpt.mtdGetReport_ContractInvoice(strOpCd, _
                                                             strCompany, _
                                                             strLocation, _
                                                             strUserId, _
                                                             strAccMonth, _
                                                             strAccYear, _
                                                             strParam, _
                                                             objRptDs, _
                                                             objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=AR_STDRPT_CONTRACTINVOICE_GET&errmesg=" & Exp.ToString() & "&redirect=")
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
        rdCrystalViewer.Close()
        rdCrystalViewer.Dispose()

        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strRptPrefix & ".pdf"">")
    End Sub

    Sub PassParam()
        Dim strOpCode_GetSysLoc As String = "PWSYSTEM_CLSCONFIG_SYSLOC_DETAILS_GET"
        Dim strOpCode_GetCountryDetails As String = "ADMIN_CLSCOUNTRY_COUNTRY_DETAILS_GET"
        Dim intErrNo As Integer
        Dim strSelectedCo As String = ""
        Dim objSysLocDs As New DataSet()
        Dim objCountryDs As Object
        Dim strParam As String

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

        Try
            strParam = strCompany & "|" & strLocation & "|" & strUserId
            intErrNo = objSysLoc.mtdGetSysLocInfo(strOpCode_GetSysLoc, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysLocDs, _
                                                  strParam)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=SETLOC_GET_SYSLOC&errmesg=" & lblErrMesage.Text & "&redirect=system/user/setlocation.aspx")
        End Try

        'strSelectedCo = Session("SS_LOCATION")
        'Try
        '    intErrNo = objCompany.mtdGetComp(strOpCode_GetComp, _
        '                                    strSelectedCo, _
        '                                    objCompDs, _
        '                                    True)
        'Catch Exp As System.Exception
        '    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADMIN_CLSCOMP_COMPANY_DETAILS_GET&errmesg=" & lblErrMesage.Text & "&redirect=")
        'End Try

        'strParam = trim(objCompDs.Tables(0).Rows(0).Item("CountryCode")) & "|"
        'Try
        '    intErrNo = objCountryDesc.mtdGetCountryDetails(strOpCode_GetCountryDetails, _
        '                                    objCountryDs, _
        '                                    strParam)
        'Catch Exp As System.Exception
        '    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADMIN_CLSCOUNTRY_COUNTRY_DETAILS_GET&errmesg=" & lblErrMesage.Text & "&redirect=")
        'End Try

        ParamDiscreteValue1.Value = strCompanyName
        ParamDiscreteValue2.Value = strFaktur
        ParamDiscreteValue3.Value = strProduct
        ParamDiscreteValue4.Value = strUOM
        ParamDiscreteValue5.Value = strUndName
        ParamDiscreteValue6.Value = strUndPost
        ParamDiscreteValue7.Value = strPrintedBy
        ParamDiscreteValue8.Value = strLocationName
        ParamDiscreteValue9.Value = strCompanyAddress
        If objSysLocDs.Tables(0).Rows.Count > 0 Then
            ParamDiscreteValue10.Value = Trim(objSysLocDs.Tables(0).Rows(0).Item("LocAddress"))
            ParamDiscreteValue11.Value = Trim(objSysLocDs.Tables(0).Rows(0).Item("City")) & " - " & Trim(objSysLocDs.Tables(0).Rows(0).Item("PostCode"))
            ParamDiscreteValue12.Value = "Telp. " & Trim(objSysLocDs.Tables(0).Rows(0).Item("TelNo")) & " Fax. " & Trim(objSysLocDs.Tables(0).Rows(0).Item("FaxNo"))
            ParamDiscreteValue18.Value = Trim(objSysLocDs.Tables(0).Rows(0).Item("NPWP"))
        End If
        ParamDiscreteValue13.Value = strTglBayar
        ParamDiscreteValue14.Value = strContract
        ParamDiscreteValue15.Value = strInvoice
        ParamDiscreteValue16.Value = strOptionNo
        ParamDiscreteValue17.Value = strPPN
        ParamDiscreteValue19.Value = strOptionDesc

        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        ParamFieldDef1 = ParamFieldDefs.Item("SellerName")
        ParamFieldDef2 = ParamFieldDefs.Item("Faktur")
        ParamFieldDef3 = ParamFieldDefs.Item("Product")
        ParamFieldDef4 = ParamFieldDefs.Item("UOM")
        ParamFieldDef5 = ParamFieldDefs.Item("UndersignedName")
        ParamFieldDef6 = ParamFieldDefs.Item("UndersignedPost")
        ParamFieldDef7 = ParamFieldDefs.Item("SessionUserName")
        ParamFieldDef8 = ParamFieldDefs.Item("SessionLocName")
        ParamFieldDef9 = ParamFieldDefs.Item("SessionCompAdd")
        ParamFieldDef10 = ParamFieldDefs.Item("CompAddress")
        ParamFieldDef11 = ParamFieldDefs.Item("CompCityPostcode")
        ParamFieldDef12 = ParamFieldDefs.Item("CompNumber")
        ParamFieldDef13 = ParamFieldDefs.Item("TglBayar")
        ParamFieldDef14 = ParamFieldDefs.Item("Contract")
        ParamFieldDef15 = ParamFieldDefs.Item("Invoice")
        ParamFieldDef16 = ParamFieldDefs.Item("OptionNo")
        ParamFieldDef17 = ParamFieldDefs.Item("PPN")
        ParamFieldDef18 = ParamFieldDefs.Item("NPWP")
        ParamFieldDef19 = ParamFieldDefs.Item("OptionDesc")

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
    End Sub

End Class