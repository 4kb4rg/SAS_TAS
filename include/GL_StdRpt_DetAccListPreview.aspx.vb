
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


Public Class GL_StdRpt_DetAccListPreview : Inherits Page
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGLRpt As New agri.GL.clsReport()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Protected WithEvents crvList As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblRefDesc As Label
    Protected WithEvents EventData As DataGrid

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
    Dim strUserLoc As String
    Dim strSrchAccCode As String
    Dim strSrchBlkCode As String
    Dim strSrchVehCode As String
    Dim strSrchVehExpCode As String
    Dim strAccType As String
    Dim strAccTypeText As String
    Dim strWithTrans As String
    Dim strEstExpense As String
    Dim strSearchExp As String = ""
    Dim lblBlkCode As String = ""
    Dim lblVehCode As String = ""
    Dim lblVehExpCode As String = ""
    Dim lblLocation As String = ""
    Dim lblAccCode As String = ""
    Dim lblAccType As String = ""
    Dim dblPCF As Double = 0
    Dim dblNCF As Double = 0
    Dim rdCrystalViewer As ReportDocument
	
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String
	
    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        crvList.Visible = False
        strUserLoc = Trim(Request.QueryString("Location"))

        strRptId = Trim(Request.QueryString("RptId"))
        strRptName = Trim(Request.QueryString("RptName"))
        strSelAccMonth = Request.QueryString("DDLAccMth")
        strSelAccYear = Request.QueryString("DDLAccYr")
        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strLocationName = Session("SS_LOCATIONNAME")
        strUserId = Session("SS_USERID")
        strPrintedBy = Session("SS_USERNAME")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")

        intSelDecimal= CInt(Request.QueryString("Decimal"))
        strSelSupress = Request.QueryString("Supp")

        strSrchAccCode = Trim(Request.QueryString("SrchAccCode"))
        strSrchBlkCode = Trim(Request.QueryString("SrchBlkCode"))
        strSrchVehCode = Trim(Request.QueryString("SrchVehCode"))
        strSrchVehExpCode = Trim(Request.QueryString("SrchVehExpCode"))
        strAccType = Trim(Request.QueryString("AccType"))
        strAccTypeText = Trim(Request.QueryString("AccTypeText"))
        strWithTrans = Trim(Request.QueryString("WithTrans"))
        strEstExpense = Trim(Request.QueryString("EstExpense"))

        If strSrchAccCode = "" Then
            strSearchExp = ""
        Else
            strSearchExp = "AND ME.AccCode LIKE '" & strSrchAccCode & "'"
        End If

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            onload_GetLangCap()
            BindReport()
        End If

        objLangCapDs.Dispose()
        If Not EventData Is Nothing Then
            EventData.Dispose()
            EventData = Nothing
        End If
        If Not crvList Is Nothing Then
            crvList.Dispose()
            crvList = Nothing
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
        Dim strDBName As String


        If strSrchBlkCode = "" Then
        Else
            strSearchExp = strSearchExp & "AND ME.BlkCode LIKE '" & strSrchBlkCode & "'"
        End If

        If strSrchVehCode = "" Then
        Else
            strSearchExp = strSearchExp & "AND ME.VehCode LIKE '" & strSrchVehCode & "'"
        End If

        If strSrchVehExpCode = "" Then
        Else
            strSearchExp = strSearchExp & "AND ME.VehExpenseCode LIKE '" & strSrchVehExpCode & "'"
        End If

        If LCase(strEstExpense) = "yes" Then
            strRptPrefix = "GL_StdRpt_DetTrialBalTransEst"
            strOpCd = "GL_STDRPT_TRIALBAL_DETAIL_GET_WITH_TRANS_SP" & "|" & objGLRpt.mtdGetGLReportTable(objGLRpt.EnumGLReportTable.TrialBalSummary) & Chr(9) & _
                        "GL_STDRPT_TRIALBAL_GET_ESTEXP_SP|GL_ESTEXP"
        Else
            strRptPrefix = "GL_StdRpt_DetTrialBalanceTrans"
            strOpCd = "GL_STDRPT_TRIALBAL_DETAIL_GET_WITH_TRANS_SP" & "|" & objGLRpt.mtdGetGLReportTable(objGLRpt.EnumGLReportTable.TrialBalSummary)
        End If

        strParam = strSelAccMonth & "|" & _
                    strSelAccYear & "|" & _
                    strUserLoc & "|" & _
                    strAccType & "|" & _
                    strSearchExp & "|" & _
                    strRptId & "|" & _
                    strEstExpense.ToLower

        Try
            intErrNo = objGLRpt.mtdGetTrialBalRpt_WithTrans(strOpCd, _
                                                            strCompany, _
                                                            strLocation, _
                                                            strUserId, _
                                                            strAccMonth, _
                                                            strAccYear, _
                                                            strParam, _
                                                            objRptDs, _
                                                            objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_STDRPT_TRIALBAL_DETAIL_GET_WITH_TRANS&errmesg=" & Exp.ToString() & "&redirect=")
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

            If myTable.Name = "GL_AccListing_RPT" Then
                myLogin = myTable.LogOnInfo

                Try
                    intErrNo = objGLRpt.mtdGet_DBConnObj(myLogin)
                Catch Exp As System.Exception
                    Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_STDRPT_TRIALBAL_DETAIL_GET_WITH_TRANS&errmesg=" & Exp.ToString() & "&redirect=")
                End Try

                myTable.ApplyLogOnInfo(myLogin)
                myTable.Location = myLogin.ConnectionInfo.DatabaseName & ".dbo.GL_AccListing_RPT"
            End If

        Next

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

        objRptDs.Dispose()
        If Not objRptDs Is Nothing Then
            objRptDs = Nothing
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
        Dim ParamFieldDef21 As ParameterFieldDefinition
        Dim ParamFieldDef22 As ParameterFieldDefinition

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
        Dim ParameterValues22 As New ParameterValues()

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
        Dim ParamDiscreteValue22 As New ParameterDiscreteValue()

        strUserLoc = Replace(strUserLoc, "','", ", ")

        ParamDiscreteValue1.Value = strUserLoc
        ParamDiscreteValue2.Value = strSelAccMonth
        ParamDiscreteValue3.Value = strSelAccYear
        ParamDiscreteValue4.Value = intSelDecimal
        ParamDiscreteValue5.Value = strSelSupress
        ParamDiscreteValue6.Value = strCompanyName
        ParamDiscreteValue7.Value = strLocationName
        ParamDiscreteValue8.Value = strPrintedBy
        ParamDiscreteValue9.Value = UCase(lblAccCode)
        ParamDiscreteValue10.Value = strAccTypeText
        ParamDiscreteValue11.Value = strSrchAccCode
        ParamDiscreteValue12.Value = UCase(lblAccType)
        ParamDiscreteValue13.Value = UCase(lblLocation)
        ParamDiscreteValue14.Value = lblBlkCode
        ParamDiscreteValue15.Value = lblVehCode
        ParamDiscreteValue16.Value = lblVehExpCode
        ParamDiscreteValue17.Value = strSrchBlkCode
        ParamDiscreteValue18.Value = strSrchVehCode
        ParamDiscreteValue19.Value = strSrchVehExpCode
        ParamDiscreteValue20.Value = strRptId
        ParamDiscreteValue21.Value = strRptName
        ParamDiscreteValue22.Value = strEstExpense

        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        ParamFieldDef1 = ParamFieldDefs.Item("SelLocCode")
        ParamFieldDef2 = ParamFieldDefs.Item("SelAccMonth")
        ParamFieldDef3 = ParamFieldDefs.Item("SelAccYear")
        ParamFieldDef4 = ParamFieldDefs.Item("SelDecimal")
        ParamFieldDef5 = ParamFieldDefs.Item("SelSuppress")
        ParamFieldDef6 = ParamFieldDefs.Item("SessionCompName")
        ParamFieldDef7 = ParamFieldDefs.Item("SessionLocName")
        ParamFieldDef8 = ParamFieldDefs.Item("SessionPrintedBy")
        ParamFieldDef9 = ParamFieldDefs.Item("lblAccCode")
        ParamFieldDef10 = ParamFieldDefs.Item("SelAccType")
        ParamFieldDef11 = ParamFieldDefs.Item("SrchAccCode")
        ParamFieldDef12 = ParamFieldDefs.Item("lblAccType")
        ParamFieldDef13 = ParamFieldDefs.Item("lblLocation")
        ParamFieldDef14 = ParamFieldDefs.Item("lblBlkCode")
        ParamFieldDef15 = ParamFieldDefs.Item("lblVehCode")
        ParamFieldDef16 = ParamFieldDefs.Item("lblVehExpCode")
        ParamFieldDef17 = ParamFieldDefs.Item("SrchBlkCode")
        ParamFieldDef18 = ParamFieldDefs.Item("SrchVehCode")
        ParamFieldDef19 = ParamFieldDefs.Item("SrchVehExpCode")
        ParamFieldDef20 = ParamFieldDefs.Item("RptId")
        ParamFieldDef21 = ParamFieldDefs.Item("RptName")
        ParamFieldDef22 = ParamFieldDefs.Item("EstExpense")

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
        ParameterValues22 = ParamFieldDef21.CurrentValues

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
        ParameterValues22.Add(ParamDiscreteValue22)

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
        ParamFieldDef22.ApplyCurrentValues(ParameterValues22)

        ParameterValues1 = Nothing
        ParameterValues2 = Nothing
        ParameterValues3 = Nothing
        ParameterValues4 = Nothing
        ParameterValues5 = Nothing
        ParameterValues6 = Nothing
        ParameterValues7 = Nothing
        ParameterValues8 = Nothing
        ParameterValues9 = Nothing
        ParameterValues10 = Nothing
        ParameterValues11 = Nothing
        ParameterValues12 = Nothing
        ParameterValues13 = Nothing
        ParameterValues14 = Nothing
        ParameterValues15 = Nothing
        ParameterValues16 = Nothing
        ParameterValues17 = Nothing
        ParameterValues18 = Nothing
        ParameterValues19 = Nothing
        ParameterValues20 = Nothing
        ParameterValues21 = Nothing
        ParameterValues22 = Nothing

        ParamDiscreteValue1 = Nothing
        ParamDiscreteValue2 = Nothing
        ParamDiscreteValue3 = Nothing
        ParamDiscreteValue4 = Nothing
        ParamDiscreteValue5 = Nothing
        ParamDiscreteValue6 = Nothing
        ParamDiscreteValue7 = Nothing
        ParamDiscreteValue8 = Nothing
        ParamDiscreteValue9 = Nothing
        ParamDiscreteValue10 = Nothing
        ParamDiscreteValue11 = Nothing
        ParamDiscreteValue12 = Nothing
        ParamDiscreteValue13 = Nothing
        ParamDiscreteValue14 = Nothing
        ParamDiscreteValue15 = Nothing
        ParamDiscreteValue16 = Nothing
        ParamDiscreteValue17 = Nothing
        ParamDiscreteValue18 = Nothing
        ParamDiscreteValue19 = Nothing
        ParamDiscreteValue20 = Nothing
        ParamDiscreteValue21 = Nothing
        ParamDiscreteValue22 = Nothing


    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblBlkCode = GetCaption(objLangCap.EnumLangCap.Block) & " Code"
        lblVehCode = GetCaption(objLangCap.EnumLangCap.Vehicle) & " Code"
        lblVehExpCode = GetCaption(objLangCap.EnumLangCap.VehExpense) & " Code"
        lblLocation = GetCaption(objLangCap.EnumLangCap.Location)
        lblAccCode = GetCaption(objLangCap.EnumLangCap.Account) & " Code"
        lblAccType = GetCaption(objLangCap.EnumLangCap.Account) & " Type"
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_STDRPT_DETACCLIST_GET_LANGCAP&errmesg=" & Exp.ToString() & "&redirect=")
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


End Class
