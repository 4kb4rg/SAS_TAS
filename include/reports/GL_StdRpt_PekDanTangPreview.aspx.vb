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

Public Class GL_StdRpt_PekDanTangPreview : Inherits Page
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGLRpt As New agri.GL.clsReport()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
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
    Dim strSrchPhyMonth as String
    Dim strSrchPhyYear as String
    Dim strSrchDivision as String
    Dim strSrchLocCode as String
	
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String
	

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        crvView.Visible = False
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

        strSrchLocCode = Trim(Request.QueryString("LocCode"))
        strSrchDivision = Trim(Request.QueryString("Division"))
        strSrchPhyMonth = Trim(Request.QueryString("PhyMonth"))
        strSrchPhyYear = Trim(Request.QueryString("PhyYear"))


         


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
        Dim objFTPFolder As String



       if len(strSrchPhyMonth) = 1 then
            strSrchPhyMonth = "0" & strSrchPhyMonth
       end if        

       strSearchExp = ""
        
       strRptPrefix = "GL_StdRpt_PekDanTang"
 
       strParam = strSelAccMonth & "|" & _
                    strSelAccYear & "|" & _
                    strRptId & "|" & _
                    strRptName & "|" & _
                    strCompany & "|" & _
                    strSrchLocCode & "|" & _ 
                    intSelDecimal & "|" & _
                    strSrchDivision & "|" & _
                    strSrchPhyMonth & "|" & _
                    strSrchPhyYear & "|" 


       strOpCd = "GL_STDRPT_PEKDANTANG" & "|" & objGLRpt.mtdGetGLReportTable(objGLRpt.EnumGLReportTable.PekDanTang) & Chr(9) & _
                  "GET_REPORT_INFO_BY_REPORTID|SH_REPORT"




        Try
            intErrNo = objGLRpt.mtdGetReport_PekDanTang(strOpCd, _
                                                            strCompany, _
                                                            strLocation, _
                                                            strUserId, _
                                                            strAccMonth, _
                                                            strAccYear, _
                                                            strParam, _
                                                            objRptDs, _
                                                            objMapPath, objFTPFolder)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_STDRPT_PEKDANTANG&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        rdCrystalViewer = New ReportDocument()
        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strRptPrefix & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))
        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3

        crvView.Visible = False                          
        crvView.ReportSource = rdCrystalViewer           
        crvView.DataBind()                               

        PassParam()
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


        Dim ParameterValues1 As New ParameterValues()
        Dim ParameterValues2 As New ParameterValues()
        Dim ParameterValues3 As New ParameterValues()
        Dim ParameterValues4 As New ParameterValues()        
        Dim ParameterValues5 As New ParameterValues()
        Dim ParameterValues6 As New ParameterValues()

        Dim ParamDiscreteValue1 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue2 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue3 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue4 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue5 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue6 As New ParameterDiscreteValue()


        strUserLoc = Replace(strUserLoc, "','", ", ")

        ParamDiscreteValue1.Value = strUserLoc
        ParamDiscreteValue2.Value = strSelAccMonth
        ParamDiscreteValue3.Value = strSelAccYear
        ParamDiscreteValue4.Value = strPrintedBy
        ParamDiscreteValue5.Value = UCase(lblLocation)
        ParamDiscreteValue6.Value = strRptId


        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        ParamFieldDef1 = ParamFieldDefs.Item("SelLocCode")
        ParamFieldDef2 = ParamFieldDefs.Item("SelAccMonth")
        ParamFieldDef3 = ParamFieldDefs.Item("SelAccYear")
        ParamFieldDef4 = ParamFieldDefs.Item("SessionPrintedBy")
        ParamFieldDef5 = ParamFieldDefs.Item("lblLocation")
        ParamFieldDef6 = ParamFieldDefs.Item("RptId")


        ParameterValues1 = ParamFieldDef1.CurrentValues
        ParameterValues2 = ParamFieldDef2.CurrentValues
        ParameterValues3 = ParamFieldDef3.CurrentValues
        ParameterValues4 = ParamFieldDef3.CurrentValues
        ParameterValues5 = ParamFieldDef3.CurrentValues
        ParameterValues6 = ParamFieldDef3.CurrentValues


        ParameterValues1.Add(ParamDiscreteValue1)
        ParameterValues2.Add(ParamDiscreteValue2)
        ParameterValues3.Add(ParamDiscreteValue3)
        ParameterValues4.Add(ParamDiscreteValue4)
        ParameterValues5.Add(ParamDiscreteValue5)
        ParameterValues6.Add(ParamDiscreteValue6)


        ParamFieldDef1.ApplyCurrentValues(ParameterValues1)
        ParamFieldDef2.ApplyCurrentValues(ParameterValues2)
        ParamFieldDef3.ApplyCurrentValues(ParameterValues3)
        ParamFieldDef4.ApplyCurrentValues(ParameterValues4)
        ParamFieldDef5.ApplyCurrentValues(ParameterValues5)
        ParamFieldDef6.ApplyCurrentValues(ParameterValues6)


        ParameterValues1 = Nothing
        ParameterValues2 = Nothing
        ParameterValues3 = Nothing
        ParameterValues4 = Nothing
        ParameterValues5 = Nothing
        ParameterValues6 = Nothing


        ParamDiscreteValue1 = Nothing
        ParamDiscreteValue2 = Nothing
        ParamDiscreteValue3 = Nothing
        ParamDiscreteValue4 = Nothing
        ParamDiscreteValue5 = Nothing
        ParamDiscreteValue6 = Nothing



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
