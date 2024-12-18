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

Public Class AR_StdRpt_LaporanPenjualanPreview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objCM As New agri.CM.clsReport()
    Dim objBI As New agri.BI.clsReport()
    Dim objCMSetup As New agri.CM.clsSetup()
    Dim objCMTrx As New agri.CM.clsTrx()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strUserLoc As String

    Dim strRptId As String
    Dim strRptName As String
    Dim strDecimal As String
    Dim strContractNo As String
    Dim strProduct As String
    Dim strBillParty As String
    Dim strDelMonth As String
    Dim strDelYear As String
    Dim strStatus As String
    Dim strBillPartyTag As String
	Dim strExportToExcel As String

    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        crvView.Visible = False

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_ARACCMONTH")
        strAccYear = Session("SS_ARACCYEAR")
        strLangCode = Session("SS_LANGCODE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Right(Request.QueryString("Location"), 1) = "," Then
                strUserLoc = Left(Request.QueryString("Location"), Len(Request.QueryString("Location")) - 1)
            Else
                strUserLoc = Trim(Request.QueryString("Location"))
            End If

            strRptId = Trim(Request.QueryString("RptID"))
            strRptName = Trim(Request.QueryString("RptName"))
            strDecimal = Trim(Request.QueryString("Decimal"))

            strContractNo = Trim(Request.QueryString("ContractNo"))
            strProduct = Trim(Request.QueryString("Product"))
            strBillParty = Trim(Request.QueryString("BillParty"))
            strDelMonth = Trim(Request.QueryString("DelMonth"))
            strDelYear = Trim(Request.QueryString("DelYear"))
            strStatus = Trim(Request.QueryString("Status"))
            strBillPartyTag = Trim(Request.QueryString("BillPartyTag"))
            strExportToExcel = Trim(Request.QueryString("ExportToExcel"))

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
        Dim strOpCdGet As String

        SearchStr = " "

        strOpCdGet = "AR_STDRPT_LAPORAN_PENJUALAN"
        If Not strBillParty = "" Then
            SearchStr = SearchStr & "AND C.BuyerCode LIKE '" & strBillParty & "' "
        End If
       
        If strDelMonth <> "" Then
            SearchStr = SearchStr & "AND Month(C.CreateDate) = '" & strDelMonth & "' "
        End If

        If strDelYear <> "" Then
            SearchStr = SearchStr & "AND Year(C.CreateDate) = '" & strDelYear & "' "
        End If

        If strProduct <> "" Then
            SearchStr = SearchStr & "AND C.ProductCode like '" & strProduct & "' "
        End If

        If strContractNo <> "" Then
            SearchStr = SearchStr & "AND C.ContractNo LIKE '" & strContractNo & "' "
        End If

        strParam = strUserLoc & "||||" & SearchStr

        Try
            intErrNo = objBI.mtdGetReport_LaporanPenjualan(strOpCdGet, strParam, objRptDs, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=AR_STDRPT_LAPORANPENJUALAN&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try



        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\AR_StdRpt_LaporanPenjualan.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3

        crvView.Visible = False
        crvView.ReportSource = rdCrystalViewer
        crvView.DataBind()

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
		If strExportToExcel = "0" Then
            'crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strFileName & ".pdf"
            crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\AR_StdRpt_LaporanPenjualan.pdf"
        Else
            'crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strFileName & ".xls"
            crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\AR_StdRpt_LaporanPenjualan.xls"
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

        If strExportToExcel = "0" Then
            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/AR_StdRpt_LaporanPenjualan.pdf"">")
        Else
            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/AR_StdRpt_LaporanPenjualan.xls"">")
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
       
        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("ParamCompanyName")
        paramField2 = paramFields.Item("ParamLocation")
        paramField3 = paramFields.Item("ParamUserName")
        paramField4 = paramFields.Item("ParamDecimal")
        paramField5 = paramFields.Item("ParamRptName")
        paramField6 = paramFields.Item("ParamMonth")
        paramField7 = paramFields.Item("ParamYear")
        paramField8 = paramFields.Item("ParamProduct")
        paramField9 = paramFields.Item("ParamRptID")
        paramField10 = paramFields.Item("ParamContractNo")
        paramField11 = paramFields.Item("ParamSellerOrBillParty")
        paramField12 = paramFields.Item("ParamLblSellerOrBillParty")


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
    

        If InStr(strUserLoc, "','") > 0 Then
            strUserLoc = Replace(strUserLoc, "','", ", ")
        End If

      
        If strDelMonth = "" Then
            strDelMonth = "All"
        Else
            strDelMonth = objGlobal.GetShortMonth(strDelMonth)
        End If

        If strDelYear = "" And strDelMonth <> "All" Then
            strDelYear = "All"
        End If


        ParamDiscreteValue1.Value = strCompanyName
        ParamDiscreteValue2.Value = strUserLoc
        ParamDiscreteValue3.Value = strUserName
        ParamDiscreteValue4.Value = strDecimal
        ParamDiscreteValue5.Value = strRptName
        ParamDiscreteValue6.Value = strStatus
        ParamDiscreteValue7.Value = strDelMonth
        ParamDiscreteValue8.Value = strDelYear
        ParamDiscreteValue9.Value = strProduct
        ParamDiscreteValue10.Value = strRptId
        ParamDiscreteValue11.Value = strContractNo
        ParamDiscreteValue12.Value = strBillParty
      
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
    
        crvView.ParameterFieldInfo = paramFields
    End Sub
End Class
