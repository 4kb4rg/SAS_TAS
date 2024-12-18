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

Public Class PU_StdRpt_BAPP_PembayaranPreview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objGLRpt As New agri.GL.clsReport()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdminComp As New agri.Admin.clsComp()
    Dim objAdminCty As New agri.Admin.clsCountry()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strLocName As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfig As Integer
    Dim objMapPath As String

    Dim strRptID As String
    Dim strRptTitle As String
    Dim intSelDecimal As String
    Dim strRpt As String
    Dim strRptType As String
    Dim strSplCode As String
    Dim strDoc As String
    Dim strPotMaterial As String
    Dim strPotPinjaman As String
    Dim strExportToExcel As String
    Dim strDDLAccMt As String
    Dim strDDLAccYr As String
	Dim strPotDP As String

    Dim VPA As String
    Dim GeneralManager As String
    Dim Manager As String
    Dim KTU As String

    Dim strDate As String
    Dim strAccCode As String
    Dim strFileName As String
    Dim strUserLoc As String
    Dim strDispLoc As String
	

    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        crvView.Visible = False

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strLangCode = Session("SS_LANGCODE")
        intConfig = Session("SS_CONFIGSETTING")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLocation = Session("SS_LOCATION")

        strRptID = Trim(Request.QueryString("RptID"))
        strRptTitle = Trim(Request.QueryString("RptTitle"))
        intSelDecimal = CInt(Request.QueryString("Decimal"))
        strRpt = Trim(Request.QueryString("strRpt"))
        strRptType = Trim(Request.QueryString("strRptType"))
        strSplCode = Trim(Request.QueryString("strSplCode"))
        strDoc = Trim(Request.QueryString("strDoc"))
        strPotMaterial = Trim(Request.QueryString("strPotMaterial"))
        strPotPinjaman = Trim(Request.QueryString("strPotPinjaman"))
		strPotDP = Trim(Request.QueryString("strPotDP"))
        strExportToExcel = Trim(Request.QueryString("strExportToExcel"))
        strDDLAccMt = Request.QueryString("DDLAccMt")
        strDDLAccYr = Request.QueryString("DDLAccYr")


        VPA = Request.QueryString("vpa")
        GeneralManager = Request.QueryString("gm")
        Manager = Request.QueryString("manager")
        KTU = Request.QueryString("ktu")


        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            BindReport()
        End If
    End Sub

	Sub GetSHLocationData()
		
	End Sub

    Sub BindReport()

        Dim objRptDs As New DataSet()
        Dim objRptDs1 As New DataSet()
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim objMapPath As String

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOpCode As String = "PU_STDRPT_BAPP_PERMOHONANPEMBAYARAN"
        Dim strFileName As String = "PU_StdRpt_BAPP_Report"

        Dim objFTPFolder As String

        If strRptType = "1" Then
            Select Case strRpt
                Case "1"
				    strOpCode = "PU_STDRPT_BAPP_HASIL"
                    strFileName = "PU_StdRpt_BAPP_Hasil"
                Case "2"
				    strOpCode = "PU_STDRPT_BAPP_REKAP"
                    strFileName = "PU_STDRPT_BAPP_REKAP"
                Case "3"
                    strOpCode = "PU_STDRPT_BAPP_HASIL"
                    strFileName = "PU_StdRpt_BAPP_Hasil_Panen"
                    'strFileName = "PU_StdRpt_BAPPRekap_Report"
            End Select
            'Else
            '    Select Case strRpt
            '        Case "1"
            '            strFileName = "PU_StdRpt_BAPP_Bayar_Report"
            '        Case "2"
            '            strFileName = "PU_StdRpt_BAPP_BayarListing_Report"
            '        Case "3"
            '            strFileName = "PU_StdRpt_BAPP_BayarRekap_Report"
            '    End Select
        End If
        


        strParamName = "LOCCODE|ACCYEAR|ACCMONTH|DOCID|SUPPLIERCODE|POTDP|POTMATERIAL|POTPINJAMAN"
        strParamValue = Trim(strLocation) & _
                            "|" & strDDLAccYr & _
                            "|" & strDDLAccMt & _
                            "|" & strDoc & _
                            "|" & strSplCode & _
							"|" & strPotDP & _
                            "|" & strPotMaterial & _
                            "|" & strPotPinjaman

        Try
            intErrNo = objGLRpt.mtdGetReportDataCommon(strOpCode, strParamName, strParamValue, objRptDs, objMapPath, objFTPFolder)

			If strRptType = "1" Then
				Select Case strRpt
                    Case "1", "3"
                        objRptDs.Tables(0).TableName = "BAPP_HASIL"
                    Case "2"
                        objRptDs.Tables(0).TableName = "BAPP_REKAP"
                End Select
			Else
				objRptDs.Tables(0).TableName = "BAPP_DETAIL"
				objRptDs.Tables(1).TableName = "BAPP_REKAP"
				objRptDs.Tables(2).TableName = "BAPP_BAYAR"
			end if

        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PU_STDRPT_BAPP_PEMBAYARAN&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

      

        rdCrystalViewer.Load(objMapPath & "web\en\Reports\Crystal\" & strFileName & ".rpt", OpenReportMethod.OpenReportByTempCopy) 
		If strRptType = "1" Then
				Select Case strRpt
                Case "1", "3"
                    rdCrystalViewer.SetDataSource(objRptDs.Tables(0))
                Case "2"
                    rdCrystalViewer.SetDataSource(objRptDs.Tables(0))
            End Select
		Else
			objRptDs1.Tables.Add(objRptDs.Tables(0).Copy())
			objRptDs1.Tables(0).TableName = "BAPP_DETAIL"
			objRptDs1.Tables.Add(objRptDs.Tables(1).Copy())
			objRptDs1.Tables(1).TableName = "BAPP_REKAP"
			objRptDs1.Tables.Add(objRptDs.Tables(2).Copy())
			objRptDs1.Tables(2).TableName = "BAPP_BAYAR"
			rdCrystalViewer.SetDataSource(objRptDs1)
    	end if

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
            ' .ExportFormatType = ExportFormatType.PortableDocFormat

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


        ParamDiscreteValue1.Value = strCompanyName
        ParamDiscreteValue2.Value = strLocation
        ParamDiscreteValue3.Value = strUserName
        ParamDiscreteValue4.Value = intSelDecimal
        ParamDiscreteValue5.Value = strRptID
        ParamDiscreteValue6.Value = UCase(strRptTitle)
        ParamDiscreteValue7.Value = VPA
        ParamDiscreteValue8.Value = GeneralManager
        ParamDiscreteValue9.Value = Manager
        ParamDiscreteValue10.Value = KTU

        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        ParamFieldDef1 = ParamFieldDefs.Item("ParamCompanyName")
        ParamFieldDef2 = ParamFieldDefs.Item("ParamLocation")
        ParamFieldDef3 = ParamFieldDefs.Item("ParamUserName")
        ParamFieldDef4 = ParamFieldDefs.Item("ParamDecimal")
        ParamFieldDef5 = ParamFieldDefs.Item("ParamRptID")
        ParamFieldDef6 = ParamFieldDefs.Item("ParamRptName")
        ParamFieldDef7 = ParamFieldDefs.Item("ParamVpaName")
        ParamFieldDef8 = ParamFieldDefs.Item("ParamGMName")
        ParamFieldDef9 = ParamFieldDefs.Item("ParamManagerName")
        ParamFieldDef10 = ParamFieldDefs.Item("ParamKTUName")

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


    End Sub

End Class
