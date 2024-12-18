
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

Public Class GL_StdRpt_DetailedTrialBalancePreview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid
    Protected WithEvents lblCode As Label
    Protected WithEvents lblRefDesc As Label

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
    Dim strSelSupress As String
    Dim strSelLocation As String
    
    Dim strSrchAccCodeFrom As String
    Dim strSrchAccCodeTo As String
    Dim strSrchBlkCode As String
    Dim strSrchTrxIDFrom As String
    Dim strSrchTrxIDTo As String
    Dim strSrchDocDateFrom As String
    Dim strSrchDocDateTo As String
    Dim strSrchAccMonthFrom As String
    Dim strSrchAccYearFrom As String
    Dim strSrchAccMonthTo As String
    Dim strSrchAccYearTo As String
    Dim strDispByTrans As String
    Dim strExportToExcel As String


    Dim strAccCodeTag As String = ""
    Dim strLocationTag As String = ""

    Dim strAccTag As String = ""
    Dim strLoc1Tag As String = ""
    
    Dim rdCrystalViewer As ReportDocument
	
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
            strSelSupress = Request.QueryString("Supp")

            strSrchAccCodeFrom = Trim(Request.QueryString("SrchAccCodeFrom"))
            strSrchAccCodeTo = Trim(Request.QueryString("SrchAccCodeTo"))
            strSrchBlkCode = Trim(Request.QueryString("srchBlkCode"))
            strSrchTrxIDFrom = Trim(Request.QueryString("SrchTrxIDFrom"))
            strSrchTrxIDTo = Trim(Request.QueryString("SrchTrxIDTo"))
            strSrchDocDateFrom = Trim(Request.QueryString("SrchDocDateFrom"))
            strSrchDocDateTo = Trim(Request.QueryString("SrchDocDateTo"))
            strSrchAccMonthFrom = Trim(Request.QueryString("SrchAccMonthFrom"))
            strSrchAccYearFrom = Trim(Request.QueryString("SrchAccYearFrom"))
            strSrchAccMonthTo = Trim(Request.QueryString("SrchAccMonthTo"))
            strSrchAccYearTo = Trim(Request.QueryString("SrchAccYearTo"))

            strDispByTrans = Trim(Request.QueryString("DispByTrans"))
            strExportToExcel = Trim(Request.QueryString("ExportToExcel"))

            If Right(Request.QueryString("Location"), 1) = "," Then
                strSelLocation = Left(Request.QueryString("Location"), Len(Request.QueryString("Location")) - 1)
            Else
                strSelLocation = Trim(Request.QueryString("SelLocation"))
            End If

            If strUserId = "" Then
                Response.Redirect("/SessionExpire.aspx")
            Else
                onload_GetLangCap()

                If strDispByTrans = "0" Then
                    BindReport()
                Else
                    BindReportByTrans()
                End If

                objLangCapDs.Dispose()
                If Not EventData Is Nothing Then
                    EventData.Dispose()
                    EventData = Nothing
                End If
                If Not crvView Is Nothing Then
                    crvView.Dispose()
                    crvView = Nothing
                End If
                If Not rdCrystalViewer Is Nothing Then
                    rdCrystalViewer.Dispose()
                    rdCrystalViewer = Nothing
                End If
                If Not objAdmin Is Nothing Then
                    objAdmin = Nothing
                End If
                If Not objLangCap Is Nothing Then
                    objLangCap = Nothing
                End If
                If Not objGLRpt Is Nothing Then
                    objGLRpt = Nothing
                End If
                If Not objGLSetup Is Nothing Then
                    objGLSetup = Nothing
                End If
                If Not objGlobal Is Nothing Then
                    objGlobal = Nothing
                End If
                If Not objLangCapDs Is Nothing Then
                    objLangCapDs = Nothing
                End If

                GC.Collect()
            End If

        End If

    End Sub

    Sub BindReport()
        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim strOpCd As String = "GL_STDRPT_DETAILED_TRIAL_BALANCE_SP"

        Dim objRptDs As New Dataset()
        Dim objMapPath As String
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strRptPrefix As String
        Dim objFTPFolder As String
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

         Try
            strRptPrefix = "GL_StdRpt_DetailedTrialBalance"
            'strParamName = "LOCCODE|ACCCODEFROM|ACCCODETO|TRXIDFROM|TRXIDTO|DOCDATEFROM|DOCDATETO|ACCMONTHFROM|ACCYEARFROM|ACCMONTHTO|ACCYEARTO|USERID"

            strParam = strSelLocation & "|" & _
                       strSrchAccCodeFrom & "|" & _
                       strSrchAccCodeTo & "|" & _
                       strSrchTrxIDFrom & "|" & _
                       strSrchTrxIDTo & "|" & _
                       strSrchDocDateFrom & "|" & _
                       strSrchDocDateTo & "|" & _
                       strSrchAccMonthFrom & "|" & _
                       strSrchAccYearFrom & "|" & _
                       strSrchAccMonthTo & "|" & _
                       strSrchAccYearTo & "|"

            intErrNo = objGLRpt.mtdGetReport_DetailedTrialBalance(strOpCd, _
                                                                  strCompany, _
                                                                  strLocation, _
                                                                  strUserId, _
                                                                  strAccMonth, _
                                                                  strAccYear, _
                                                                  strParam, _
                                                                  objRptDs, _
                                                                  objMapPath, _
                                                                  objFTPFolder)

            'intErrNo = objGLRpt.mtdGetReportDataCommon(strOpCd, strParamName, strParamValue, objRptDs, objMapPath, objFTPFolder)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GL_STDRPT_DETAILED_ACCOUNT_LEDGER_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try


        rdCrystalViewer = New ReportDocument()
        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strRptPrefix & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs)
        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3


        PassParam()


        Dim myTable As CrystalDecisions.CrystalReports.Engine.Table
        Dim myLogin As CrystalDecisions.Shared.TableLogOnInfo

        For Each myTable In rdCrystalViewer.Database.Tables

            If myTable.Name = "GL_DET_TRIAL_BALANCE" Then
                myLogin = myTable.LogOnInfo

                Try
                    intErrNo = objGLRpt.mtdGet_DBConnObj(myLogin)
                Catch Exp As System.Exception
                    Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_STDRPT_DETAILED_TRIALBALANCE_GET&errmesg=" & Exp.ToString() & "&redirect=")
                End Try

                myTable.ApplyLogOnInfo(myLogin)
                myTable.Location = myLogin.ConnectionInfo.DatabaseName & ".dbo.GL_DetailTrialBalance_RPT"
                
            End If

        Next
        crDiskFileDestinationOptions = New DiskFileDestinationOptions()

        If strExportToExcel = "0" Then
            'crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strRptPrefix & ".pdf"
            crDiskFileDestinationOptions.DiskFileName = objFTPFolder & strRptPrefix & ".pdf"
        Else
            'crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strRptPrefix & ".xls"
            crDiskFileDestinationOptions.DiskFileName = objFTPFolder & strRptPrefix & ".xls"
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
            'Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strRptPrefix & ".pdf"">")
            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../" & strUrl & strRptPrefix & ".pdf"">")
        Else
            'Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strRptPrefix & ".xls"">")
            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../" & strUrl & strRptPrefix & ".xls"">")
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
        Dim ParamFieldDef17 As ParameterFieldDefinition
        Dim ParamFieldDef18 As ParameterFieldDefinition
        Dim ParamFieldDef19 As ParameterFieldDefinition
        Dim ParamFieldDef20 As ParameterFieldDefinition

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

        strSelLocation = Replace(strSelLocation, "','", ", ")

        ParamDiscreteValue1.Value = strSelLocation
        ParamDiscreteValue2.Value = strRptId
        ParamDiscreteValue3.Value = strRptName
        ParamDiscreteValue4.Value = intSelDecimal
        ParamDiscreteValue5.Value = strSelSupress
        ParamDiscreteValue6.Value = strCompanyName
        ParamDiscreteValue7.Value = strPrintedBy
        ParamDiscreteValue8.Value = strLocationTag
        ParamDiscreteValue9.Value = strAccCodeTag
        ParamDiscreteValue10.Value = strSrchAccCodeFrom
        ParamDiscreteValue11.Value = strSrchAccCodeTo
        ParamDiscreteValue12.Value = strSrchTrxIDFrom
        ParamDiscreteValue13.Value = strSrchTrxIDTo
        ParamDiscreteValue14.Value = strSrchDocDateFrom
        ParamDiscreteValue15.Value = strSrchDocDateTo
        ParamDiscreteValue16.Value = strSrchAccMonthFrom
        ParamDiscreteValue17.Value = strSrchAccYearFrom
        ParamDiscreteValue18.Value = strSrchAccMonthTo
        ParamDiscreteValue19.Value = strSrchAccYearTo
        ParamDiscreteValue20.Value = strUserID

        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        ParamFieldDef1 = ParamFieldDefs.Item("SelLocCode")
        ParamFieldDef2 = ParamFieldDefs.Item("RptId")
        ParamFieldDef3 = ParamFieldDefs.Item("RptName")
        ParamFieldDef4 = ParamFieldDefs.Item("SelDecimal")
        ParamFieldDef5 = ParamFieldDefs.Item("SelSuppress")
        ParamFieldDef6 = ParamFieldDefs.Item("SessionCompName")
        ParamFieldDef7 = ParamFieldDefs.Item("SessionPrintedBy")
        ParamFieldDef8 = ParamFieldDefs.Item("LocationTag")
        ParamFieldDef9 = ParamFieldDefs.Item("AccCodeTag")
        ParamFieldDef10 = ParamFieldDefs.Item("SrchAccCodeFrom")
        ParamFieldDef11 = ParamFieldDefs.Item("SrchAccCodeTo")
        ParamFieldDef12 = ParamFieldDefs.Item("SrchTrxIDFrom")
        ParamFieldDef13 = ParamFieldDefs.Item("SrchTrxIDTo")
        ParamFieldDef14 = ParamFieldDefs.Item("SrchDocDateFrom")
        ParamFieldDef15 = ParamFieldDefs.Item("SrchDocDateTo")
        ParamFieldDef16 = ParamFieldDefs.Item("SrchAccMonthFrom")
        ParamFieldDef17 = ParamFieldDefs.Item("SrchAccYearFrom")
        ParamFieldDef18 = ParamFieldDefs.Item("SrchAccMonthTo")
        ParamFieldDef19 = ParamFieldDefs.Item("SrchAccYearTo")
        ParamFieldDef20 = ParamFieldDefs.Item("UserID")

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

    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        strAccCodeTag = GetCaption(objLangCap.EnumLangCap.Account) & lblCode.text
        strLocationTag = GetCaption(objLangCap.EnumLangCap.Location)
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
                    If strLocType = objAdminLoc.EnumLocType.Mill then
                        Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                    else
                        Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                    end if
                    Exit For
                End If
            Next
    End Function


    Sub BindReportByTrans()

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim strOpCd As String = "GL_STDRPT_DETAILED_TRIAL_BALANCE_BY_TRANS_SP"

        Dim objRptDs As New Dataset()
        Dim objMapPath As String
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strRptPrefix As String
        Dim objFTPFolder As String
        Dim strParamName As String

        Try
            strRptPrefix = "GL_StdRpt_DetailedTrialBalanceByTrans"

            strParamName = "LOCCODE|ACCCODEFROM|ACCCODETO|BLKCODE|TRXIDFROM|TRXIDTO|DOCDATEFROM|DOCDATETO|ACCMONTHFROM|ACCYEARFROM|ACCMONTHTO|ACCYEARTO|"

            strParam = strSelLocation & "|" & _
                       strSrchAccCodeFrom & "|" & _
                       strSrchAccCodeTo & "|" & _
                       strSrchBlkCode & "|" & _
                       strSrchTrxIDFrom & "|" & _
                       strSrchTrxIDTo & "|" & _
                       strSrchDocDateFrom & "|" & _
                       strSrchDocDateTo & "|" & _
                       strSrchAccMonthFrom & "|" & _
                       strSrchAccYearFrom & "|" & _
                       strSrchAccMonthTo & "|" & _
                       strSrchAccYearTo & "|"

            Try
                intErrNo = objGLRpt.mtdGetReportDataCommon(strOpCd, strParamName, strParam, objRptDs, objMapPath, objFTPFolder)
            Catch Exp As Exception
                Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=LAPORAN_BIAYA_PRODUKSI&errmesg=" & Exp.ToString() & "&redirect=")
            End Try


            '    intErrNo = objGLRpt.mtdGetReport_DetailedTrialBalance(strOpCd, _
            '                                                          strCompany, _
            '                                                          strLocation, _
            '                                                          strUserId, _
            '                                                          strAccMonth, _
            '                                                          strAccYear, _
            '                                                          strParam, _
            '                                                          objRptDs, _
            '                                                          objMapPath, _
            '                                                          objFTPFolder)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GL_STDRPT_DETAILED_ACCOUNT_LEDGER_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try





        rdCrystalViewer = New ReportDocument()
        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strRptPrefix & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))
        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()

        If strExportToExcel = "0" Then
            'crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strRptPrefix & ".pdf"
            crDiskFileDestinationOptions.DiskFileName = objFTPFolder & strRptPrefix & ".pdf"
        Else
            'crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strRptPrefix & ".xls"
            crDiskFileDestinationOptions.DiskFileName = objFTPFolder & strRptPrefix & ".xls"
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
            'Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strRptPrefix & ".pdf"">")
            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../" & strUrl & strRptPrefix & ".pdf"">")
        Else
            'Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strRptPrefix & ".xls"">")
            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../" & strUrl & strRptPrefix & ".xls"">")
        End If

        'crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        'If strExportToExcel = "0" Then
        '    crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strRptPrefix & ".pdf"
        'Else
        '    crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strRptPrefix & ".xls"
        'End If


        'crExportOptions = rdCrystalViewer.ExportOptions
        'With crExportOptions
        '    .DestinationOptions = crDiskFileDestinationOptions
        '    .ExportDestinationType = ExportDestinationType.DiskFile
        '    If strExportToExcel = "0" Then
        '        .ExportFormatType = ExportFormatType.PortableDocFormat
        '    Else
        '        .ExportFormatType = ExportFormatType.Excel
        '    End If

        'End With

        'rdCrystalViewer.Export()
        'If strExportToExcel = "0" Then
        '    Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strRptPrefix & ".pdf"">")
        'Else
        '    Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strRptPrefix & ".xls"">")
        'End If
        
    End Sub




End Class
