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

Public Class GL_StdRpt_BlockLedgerActPreview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblRefDesc As Label
    Protected WithEvents EventData As DataGrid
    Protected WithEvents lblCode As Label

    Dim objAdmin As New agri.Admin.clsShare()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGLRpt As New agri.GL.clsReport()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objPDTrx As New agri.PD.clsTrx()

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
    
    Dim strSrchActType As String
    Dim strSrchBlkType As String
    Dim strSrchActGrpCode As String
    Dim strSrchActCode As String
    Dim strSrchBlkGrpCode As String
    Dim strSrchBlkCode As String
    Dim strSrchSubBlkCode As String
    Dim strSrchAccCode As String
    Dim strCostLevel As String

    Dim strSearchExp As String = ""
    Dim strActCodeTag As String = ""
    Dim strBlkCodeTag As String = ""
    Dim strAccCodeTag As String = ""
    Dim strLocationTag As String = ""
    Dim strActTag As String = ""
    Dim strBlkTag As String = ""
    Dim strAccTag As String = ""
    Dim strLoc1Tag As String = ""
    Dim strTotalAreaTag As String = ""
    Dim strVehTag As String = ""
    Dim strVehExpenseTag As String = ""
    
    Dim rdCrystalViewer As ReportDocument
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String
	

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
        strLocType = Session("SS_LOCTYPE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
    
            strRptId = Trim(Request.QueryString("RptId"))
            strRptName = Trim(Request.QueryString("RptName"))
            strSelAccMonth = Request.QueryString("DDLAccMth")
            strSelAccYear = Request.QueryString("DDLAccYr")
            intSelDecimal= CInt(Request.QueryString("Decimal"))
            strSelSupress = Request.QueryString("Supp")

            strSrchActType = Trim(Request.QueryString("SrchActType"))
            strSrchBlkType = Trim(Request.QueryString("SrchBlkType"))
            strSrchActGrpCode = Trim(Request.QueryString("SrchActGrpCode"))
            strSrchActCode = Trim(Request.QueryString("SrchActCode"))
            strSrchBlkGrpCode = Trim(Request.QueryString("SrchBlkGrpCode"))
            strSrchBlkCode = Trim(Request.QueryString("SrchBlkCode"))
            strSrchSubBlkCode = Trim(Request.QueryString("SrchSubBlkCode"))
            strSrchAccCode = Trim(Request.QueryString("SrchAccCode"))
            strCostLevel = Trim(Request.QueryString("CostLevel"))


            If Right(Request.QueryString("Location"), 1) = "," Then
                strSelLocation = Left(Request.QueryString("Location"), Len(Request.QueryString("Location")) - 1)
            Else
                strSelLocation = Trim(Request.QueryString("SelLocation"))
            End If

            onload_GetLangCap()
            BindReport()
        End If

    End Sub

    Sub BindReport()
        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim strOpCd As String
        Dim strOpCdTrx As String
        Dim objRptDs As New Dataset()
        Dim objRptDsBgtMTD As New DataSet()
        Dim objRptDsBgtYTD As New DataSet()
        Dim objMapPath As String
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strBGTParam As String
        Dim strRptPrefix As String
        Dim intCnt As Integer
        Dim objFTPFolder As String

        Try
            strRptPrefix = "GL_StdRpt_BlockLedgerByAct"


            
            strOpCd = "GL_STDRPT_BLKLEDGERACT_GET_SP|GL_BLOCKLEDGERACT"

            strParam = strSelAccMonth & "|" & _
                       strSelAccYear & "|" & _
                       strSelLocation & "|" & _
                       strSrchActType & "|" & _
                       strSrchBlkType & "|" & _
                       strSrchActGrpCode & "|" & _
                       strSrchBlkGrpCode & "|" & _
                       strSrchActCode & "|" & _
                       strSrchBlkCode & "|" & _
                       strSrchSubBlkCode & "|" & _
                       strSrchAccCode & "|" & _
                       strCostLevel & "|" & _
                       CInt(objPDTrx.EnumEstateYieldStatus.Closed)


            intErrNo = objGLRpt.mtdGetReport_BlkLedgerAct(strOpCd, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strParam, _
                                                        objRptDs, _
                                                        objMapPath, objFTPFolder)

        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_STDRPT_BLOCK_LEDGER_BYACT_GET_DETAIL&errmesg=" & Exp.ToString() & "&redirect=")
        End Try












        rdCrystalViewer = New ReportDocument()
        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strRptPrefix & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3


        PassParam()

        Dim tblTemp As CrystalDecisions.CrystalReports.Engine.Table
        Dim loiTemp As CrystalDecisions.Shared.TableLogOnInfo
        For Each tblTemp In rdCrystalViewer.Database.Tables
            If tblTemp.Name = "GL_BLOCKLEDGERACT" Then
                loiTemp = tblTemp.LogOnInfo
                Try
                    intErrNo = objGLRpt.mtdGet_DBConnObj(loiTemp)
                Catch Exp As System.Exception
                    Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_STDRPT_BLOCK_LEDGER_BYACT_GET_DETAIL&errmesg=" & Exp.ToString() & "&redirect=")
                End Try
                tblTemp.ApplyLogOnInfo(loiTemp)
                tblTemp.Location = loiTemp.ConnectionInfo.DatabaseName & ".dbo.GL_BLOCKLEDGERACT_RPT"
            End If
        Next

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        'crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strRptPrefix & ".pdf"
        crDiskFileDestinationOptions.DiskFileName = objFTPFolder & strRptPrefix & ".pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        rdCrystalViewer.Close()
        rdCrystalViewer.Dispose()

        Dim strUrl As String
        strUrl = objFTPFolder.Substring(InStr(objFTPFolder, "ftp") - 1, Len(objFTPFolder) - InStr(objFTPFolder, "ftp") + 1)
        strUrl = Replace(strUrl, "\", "/")
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../" & strUrl & strRptPrefix & ".pdf"">")
        'Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strRptPrefix & ".pdf"">")
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
        Dim ParamFieldDef23 As ParameterFieldDefinition
        Dim ParamFieldDef24 As ParameterFieldDefinition

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
        Dim ParameterValues23 As New ParameterValues()
        Dim ParameterValues24 As New ParameterValues()

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
        Dim ParamDiscreteValue23 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue24 As New ParameterDiscreteValue()

        strSelLocation = Replace(strSelLocation, "','", ", ")
        strSrchActGrpCode = Replace(strSrchActGrpCode, "','", ", ")

        ParamDiscreteValue1.Value = strSelLocation
        ParamDiscreteValue2.Value = strSelAccMonth
        ParamDiscreteValue3.Value = strSelAccYear
        ParamDiscreteValue4.Value = intSelDecimal
        ParamDiscreteValue5.Value = strSelSupress
        ParamDiscreteValue6.Value = strCompanyName
        ParamDiscreteValue7.Value = strLocationName
        ParamDiscreteValue8.Value = strPrintedBy
        ParamDiscreteValue9.Value = UCase(strLocationTag)
        ParamDiscreteValue10.Value = UCase(strActCodeTag)
        ParamDiscreteValue11.Value = UCase(strBlkCodeTag)
        ParamDiscreteValue12.Value = UCase(strAccCodeTag)
        If LCase(strSrchActType) = "actcode" Then
            ParamDiscreteValue13.Value = strSrchActCode
        Else
            ParamDiscreteValue13.Value = strSrchActGrpCode
        End If

        If LCase(strSrchBlkType) = "blkcode" Then
            ParamDiscreteValue14.Value = strSrchBlkCode
        ElseIf LCase(strSrchBlkType) = "subblkcode" Then
            ParamDiscreteValue14.Value = strSrchSubBlkCode
        Else
            ParamDiscreteValue14.Value = strSrchBlkGrpCode
        End If

        ParamDiscreteValue15.Value = strSrchAccCode
        ParamDiscreteValue16.Value = strActTag
        ParamDiscreteValue17.Value = strBlkTag
        ParamDiscreteValue18.Value = strAccTag
        ParamDiscreteValue19.Value = strLoc1Tag
        ParamDiscreteValue20.Value = strTotalAreaTag
        ParamDiscreteValue21.Value = strVehTag
        ParamDiscreteValue22.Value = strVehExpenseTag
        ParamDiscreteValue23.Value = strRptId
        ParamDiscreteValue24.Value = strRptName

        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        ParamFieldDef1 = ParamFieldDefs.Item("SelLocCode")
        ParamFieldDef2 = ParamFieldDefs.Item("SelAccMonth")
        ParamFieldDef3 = ParamFieldDefs.Item("SelAccYear")
        ParamFieldDef4 = ParamFieldDefs.Item("SelDecimal")
        ParamFieldDef5 = ParamFieldDefs.Item("SelSuppress")
        ParamFieldDef6 = ParamFieldDefs.Item("SessionCompName")
        ParamFieldDef7 = ParamFieldDefs.Item("SessionLocName")
        ParamFieldDef8 = ParamFieldDefs.Item("SessionPrintedBy")
        ParamFieldDef9 = ParamFieldDefs.Item("LocationTag")
        ParamFieldDef10 = ParamFieldDefs.Item("ActCodeTag")
        ParamFieldDef11 = ParamFieldDefs.Item("BlkCodeTag")
        ParamFieldDef12 = ParamFieldDefs.Item("AccCodeTag")
        ParamFieldDef13 = ParamFieldDefs.Item("SrchActCode")
        ParamFieldDef14 = ParamFieldDefs.Item("SrchBlkCode")
        ParamFieldDef15 = ParamFieldDefs.Item("SrchAccCode")
        ParamFieldDef16 = ParamFieldDefs.Item("ActTag")
        ParamFieldDef17 = ParamFieldDefs.Item("BlkTag")
        ParamFieldDef18 = ParamFieldDefs.Item("AccTag")
        ParamFieldDef19 = ParamFieldDefs.Item("Loc1Tag")
        ParamFieldDef20 = ParamFieldDefs.Item("TotalAreaTag")
        ParamFieldDef21 = ParamFieldDefs.Item("VehTag")
        ParamFieldDef22 = ParamFieldDefs.Item("VehExpTag")
        ParamFieldDef23 = ParamFieldDefs.Item("RptId")
        ParamFieldDef24 = ParamFieldDefs.Item("RptName")

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
        ParameterValues22 = ParamFieldDef22.CurrentValues
        ParameterValues23 = ParamFieldDef23.CurrentValues
        ParameterValues24 = ParamFieldDef24.CurrentValues

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
        ParameterValues23.Add(ParamDiscreteValue23)
        ParameterValues24.Add(ParamDiscreteValue24)

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
        ParamFieldDef23.ApplyCurrentValues(ParameterValues23)
        ParamFieldDef24.ApplyCurrentValues(ParameterValues24)
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        If LCase(strSrchActType) = "actcode" Then
            strActCodeTag = GetCaption(objLangCap.EnumLangCap.Activity) & lblCode.text
        Else
            strActCodeTag = GetCaption(objLangCap.EnumLangCap.ActGrp) & lblCode.text
        End If

        If LCase(strSrchBlkType) = "blkcode" Then
            strBlkCodeTag = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.text
        ElseIf LCase(strSrchBlkType) = "subblkcode" Then
            strBlkCodeTag = GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.text
        Else
            strBlkCodeTag = GetCaption(objLangCap.EnumLangCap.BlockGrp) & lblCode.text
        End If

        If LCase(strCostLevel) = "subblock" Then
            strBlkTag = GetCaption(objLangCap.EnumLangCap.SubBlock)
        Else
            strBlkTag = GetCaption(objLangCap.EnumLangCap.Block)
        End If

        strAccCodeTag = GetCaption(objLangCap.EnumLangCap.Account) & lblCode.text
        strLocationTag = GetCaption(objLangCap.EnumLangCap.Location)

        strActTag = GetCaption(objLangCap.EnumLangCap.Activity)
        strAccTag = GetCaption(objLangCap.EnumLangCap.Account)
        strLoc1Tag = GetCaption(objLangCap.EnumLangCap.Location)
        strTotalAreaTag = GetCaption(objLangCap.EnumLangCap.TotalArea)
        strVehTag = GetCaption(objLangCap.EnumLangCap.Vehicle)
        strVehExpenseTag = GetCaption(objLangCap.EnumLangCap.VehExpense)

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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_STDRPT_BLOCK_LEDGER_BYACT_GET_LANGCAP&errmesg=" & Exp.ToString() & "&redirect=")
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
