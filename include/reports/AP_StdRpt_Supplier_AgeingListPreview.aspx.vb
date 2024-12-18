
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

Public Class AP_StdRpt_Supplier_AgeingListPreview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid
    Protected WithEvents lblCode As Label
    Protected WithEvents lblRefDesc As Label
    Protected WithEvents lbltest As Label

    Dim objAdmin As New agri.Admin.clsShare()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGLRpt As New agri.GL.clsReport()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()

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

    Dim strSelLocation As String

    Dim strSrchSupplier As String
    Dim strSrchPeriode1 As String
    Dim strSrchPeriode2 As String
    Dim strExportToExcel As String

    Dim IntFromAge1 As Integer
    Dim IntToAge1 As Integer
    Dim IntFromAge2 As Integer
    Dim IntToAge2 As Integer
    Dim IntFromAge3 As Integer
    Dim IntToAge3 As Integer
    Dim IntFromAge4 As Integer
    Dim IntToAge4 As Integer
    Dim IntAge5 As Integer

    Dim strCategory1 As String
    Dim strCategory2 As String
    Dim strCategory3 As String
    Dim strCategory4 As String
    Dim strCategory5 As String

    Dim rdCrystalViewer As New ReportDocument()

    Dim intErrNo As Integer
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        crvView.Visible = False
        strUserId = Session("SS_USERID")
        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strLocationName = Session("SS_LOCATIONNAME")
        strPrintedBy = Session("SS_USERNAME")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            strRptId = Trim(Request.QueryString("RptId"))
            strRptName = Trim(Request.QueryString("RptName"))
            intSelDecimal = CInt(Request.QueryString("Decimal"))

            strSelLocation = Trim(Request.QueryString("SrchLocation"))
            strSrchSupplier = Trim(Request.QueryString("SrchSupplier"))
            strSrchPeriode1 = Trim(Request.QueryString("SrchPeriod1"))
            strSrchPeriode2 = Trim(Request.QueryString("SrchPeriod2"))
            strExportToExcel = Trim(Request.QueryString("ExportToExcel"))

            IntFromAge1 = Trim(Request.QueryString("FromAge1"))
            IntToAge1 = Trim(Request.QueryString("ToAge1"))
            IntFromAge2 = Trim(Request.QueryString("FromAge2"))
            IntToAge2 = Trim(Request.QueryString("ToAge2"))
            IntFromAge3 = Trim(Request.QueryString("FromAge3"))
            IntToAge3 = Trim(Request.QueryString("ToAge3"))
            IntFromAge4 = Trim(Request.QueryString("FromAge4"))
            IntToAge4 = Trim(Request.QueryString("ToAge4"))
            IntAge5 = Trim(Request.QueryString("FromAge5"))


            'strCategory1 = "1 - " & strToAge1
            'strCategory2 = strFromAge2 & " - " & strToAge2
            'strCategory3 = strFromAge3 & " - " & strToAge3
            'strCategory4 = strFromAge4 & " - " & strToAge4
            'strCategory5 = strFromAge5

            If Right(Request.QueryString("Location"), 1) = "," Then
                strSelLocation = Left(Request.QueryString("Location"), Len(Request.QueryString("Location")) - 1)
            Else
                strSelLocation = Trim(Request.QueryString("SelLocation"))
            End If

            If strUserId = "" Then
                Response.Redirect("/SessionExpire.aspx")
            Else
                onload_GetLangCap()
                BindReport()
            End If
        End If
    End Sub

    Sub BindReport()
        Dim objRptDs As New DataSet()
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim objMapPath As String
        Dim strPeriod1 As String
        Dim strPeriod2 As String

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOpCode As String = "AP_STDRPT_SUPPLIER_AGEING_DETAIL_SP"
        Dim strFileName As String = "AP_StdRpt_Supplier_AgeingList"

        Dim strParam As String
        Dim searchStr As String
        Dim objFTPFolder As String


        strParamName = "SUPCODE|LOCCODE|PERIOD1|PERIOD2|AGEFROM1|AGETO1|AGEFROM2|AGETO2|AGEFROM3|AGETO3|AGEFROM4|AGETO4|AGE5"
        'strParamValue = strSrchSupplier & "|''" & strSelLocation & _
        '                "''|" & strSrchPeriode1 & _
        '                "|" & strSrchPeriode1 & _
        '                "|" & IntFromAge1 & _
        '                "|" & IntToAge2 & _
        '                "|" & IntFromAge2 & _
        '                "|" & IntToAge2 & _
        '                "|" & IntFromAge3 & _
        '                "|" & IntToAge3 & _
        '                "|" & IntFromAge4 & _
        '                "|" & IntToAge4 & _
        '                "|" & IntAge5 & ""

        strParamValue = strSrchSupplier & _
                "|" & strSelLocation & _
                "|" & strSrchPeriode1 & _
                "|" & strSrchPeriode1 & _
                "|" & IntFromAge1 & _
                "|" & IntToAge1 & _
                "|" & IntFromAge2 & _
                "|" & IntToAge2 & _
                "|" & IntFromAge3 & _
                "|" & IntToAge3 & _
                "|" & IntFromAge4 & _
                "|" & IntToAge4 & _
                "|" & IntAge5 & ""

        Try
            intErrNo = objGLRpt.mtdGetReportDataCommon(strOpCode, strParamName, strParamValue, objRptDs, objMapPath, objFTPFolder)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_REPLACEMENTSCHEDULE_LIST_REPORT&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try


        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\AP_StdRpt_Supplier_AgeingList.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))
        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA4

        crvView.Visible = False
        crvView.ReportSource = rdCrystalViewer
        crvView.DataBind()

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()

        If strExportToExcel = "0" Then
            'crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strFileName & ".pdf"
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
            'Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strFileName & ".pdf"">")
            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../" & strUrl & strFileName & ".pdf"">")
        Else
            'Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strFileName & ".xls"">")
            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../" & strUrl & strFileName & ".xls"">")
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

        strSelLocation = Replace(strSelLocation, "','", ", ")

        ParamDiscreteValue1.Value = strSelLocation
        ParamDiscreteValue2.Value = strRptId
        ParamDiscreteValue3.Value = strRptName
        ParamDiscreteValue4.Value = intSelDecimal
        ParamDiscreteValue5.Value = strCompanyName
        ParamDiscreteValue6.Value = strPrintedBy
        ParamDiscreteValue7.Value = strSrchSupplier
        ParamDiscreteValue8.Value = strSrchPeriode1
        ParamDiscreteValue9.Value = strSrchPeriode2
        ParamDiscreteValue10.Value = strUserId

        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        ParamFieldDef1 = ParamFieldDefs.Item("SelLocCode")
        ParamFieldDef2 = ParamFieldDefs.Item("RptId")
        ParamFieldDef3 = ParamFieldDefs.Item("RptName")
        ParamFieldDef4 = ParamFieldDefs.Item("SelDecimal")
        ParamFieldDef5 = ParamFieldDefs.Item("SessionCompName")
        ParamFieldDef6 = ParamFieldDefs.Item("SessionPrintedBy")
        ParamFieldDef7 = ParamFieldDefs.Item("strSrchSupplier")
        ParamFieldDef8 = ParamFieldDefs.Item("strSrchPeriode1")
        ParamFieldDef9 = ParamFieldDefs.Item("strSrchPeriode2")
        ParamFieldDef10 = ParamFieldDefs.Item("UserID")

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

    Sub onload_GetLangCap()
        'GetEntireLangCap()
    End Sub

    Sub GetEntireLangCap()
        Dim strOpCode_BussTerm As String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
        Dim strParam As String
        Dim intErrNo As Integer

        strParam = strLangCode
        Try
            intErrNo = objLangCap.mtdGetBussTerm(strOpCode_BussTerm, _
                                                 strCompany, _
                                                 strLocation, _
                                                 strUserId, _
                                                 strAccMonth, _
                                                 strAccYear, _
                                                 objLangCapDs, _
                                                 strParam)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_STDRPT_DETAILED_ACCOUNT_LEDGER_GET_LANGCAP&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

        For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If strLocType = objAdminLoc.EnumLocType.Mill Then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                Else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                End If
                Exit For
            End If
        Next
    End Function


End Class
