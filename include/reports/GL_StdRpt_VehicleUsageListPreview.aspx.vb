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

Public Class GL_StdRpt_VehicleUsageListPreview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objAdmin As New agri.Admin.clsShare()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGLRpt As New agri.GL.clsReport()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objGLTrx As New agri.GL.clsTrx()
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
    
    Dim strSrchVehUsgId As String
    Dim strSrchVehCode As String
    Dim strSrchVehTypeCode As String
    Dim strSrchBlkType As String
    Dim strSrchAccCode As String
    Dim strSrchBlkGrpCode As String
    Dim strSrchBlkCode As String
    Dim strSrchSubBlkCode As String
    Dim strSrchStatus As String
    Dim strSrchText As String
    Dim strCostLevel As String

    Dim IDTag As String
    Dim CodeTag As String
    Dim VehUsgTag As String
    Dim VehTag As String
    Dim VehTypeTag As String
    Dim AccTag As String
    Dim SubBlkTag As String
    Dim BlkTag As String
    Dim BlkGrpTag As String
    Dim RunUnitTag As String
    Dim LocTag As String
    Dim strIndex As String

    
    Dim rdCrystalViewer As ReportDocument

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
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            strRptId = Trim(Request.QueryString("RptId"))
            strRptName = Trim(Request.QueryString("RptName"))
            strSelAccMonth = Trim(Request.QueryString("DDLAccMth"))
            strSelAccYear = Trim(Request.QueryString("DDLAccYr"))
            intSelDecimal= CInt(Trim(Request.QueryString("Decimal")))
            strSelSupress = Trim(Request.QueryString("Supp"))
            
            strSrchVehUsgId = Trim(Request.QueryString("SrchVehUsgId"))
            strSrchVehCode = Trim(Request.QueryString("SrchVehCode"))
            strSrchVehTypeCode = Trim(Request.QueryString("SrchVehTypeCode"))
            strSrchAccCode = Trim(Request.QueryString("SrchAccCode"))
            strSrchBlkType = Trim(Request.QueryString("SrchBlkType"))
            strSrchBlkGrpCode = Trim(Request.QueryString("SrchBlkGrpCode"))
            strSrchBlkCode = Trim(Request.QueryString("SrchBlkCode"))
            strSrchSubBlkCode = Trim(Request.QueryString("SrchSubBlkCode"))
            strSrchStatus = Trim(Request.QueryString("SrchStatus"))
            strSrchText = Trim(Request.QueryString("SrchText")) 
            strCostLevel = Trim(Request.QueryString("CostLevel"))

            IDTag = Trim(Request.QueryString("lblID"))
            CodeTag = Trim(Request.QueryString("lblCode"))
            VehUsgTag = Trim(Request.QueryString("lblVehUsg"))
            VehTag = Trim(Request.QueryString("lblVehicle"))
            VehTypeTag = Trim(Request.QueryString("lblVehType"))
            AccTag = Trim(Request.QueryString("lblAccount"))
            SubBlkTag = Trim(Request.QueryString("lblSubBlk"))
            BlkTag = Trim(Request.QueryString("lblBlock"))
            BlkGrpTag = Trim(Request.QueryString("lblBlkGrp"))
            RunUnitTag = Trim(Request.QueryString("lblRunUnit"))
            LocTag = Trim(Request.QueryString("lblLocation"))
            strIndex = Trim(Request.QueryString("hitung"))


            If Right(Request.QueryString("Location"), 1) = "," Then
                strSelLocation = Left(Request.QueryString("Location"), Len(Request.QueryString("Location")) - 1)
            Else
                strSelLocation = Trim(Request.QueryString("SelLocation"))
            End If

            BindReport()
        End If

    End Sub

    Sub BindReport()
        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim strOpCode As String
        Dim objRptDs As New Dataset()
        Dim objMTDDs As New Dataset()
        Dim objMapPath As String
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strRptPrefix As String
        Dim objFTPFolder As String


        If Trim(Request.QueryString("GrpType")) = "U" Then
            strRptPrefix = "GL_StdRpt_VehicleUsageList"
        Else
            strRptPrefix = "GL_StdRpt_VehicleUsageListAktfti"
        End If
        strOpCode = "GL_STDRPT_VEHUSAGELIST_GET_SP" & "|" & "GL_VEHUSAGE"

        Try
            strParam = strSelAccMonth & "|" & _
                        strSelAccYear & strIndex & "|" & _
                        strSelLocation & "|" & _
                        strCostLevel & "|" & _
                        strSrchBlkType & "|" & _
                        strSrchVehUsgId & "|" & _
                        strSrchVehCode & "|" & _
                        strSrchVehTypeCode & "|" & _
                        strSrchAccCode & "|" & _
                        strSrchSubBlkCode & "|" & _
                        strSrchBlkCode & "|" & _
                        strSrchBlkGrpCode & "|" & _
                        strSrchStatus

            intErrNo = objGLRpt.mtdGetReport_VehUsgList(strOpCode, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strParam, _
                                                        objRptDs, _
                                                        objMapPath, objFTPFolder)

        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_STDRPT_VEHUSAGELIST_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try


        rdCrystalViewer = New ReportDocument()
        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strRptPrefix & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs)
        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        If Trim(Request.QueryString("GrpType")) = "U" Then
            rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3
        Else
            rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA4
        End If



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
        Dim ParamFieldDef25 As ParameterFieldDefinition
        Dim ParamFieldDef26 As ParameterFieldDefinition
        Dim ParamFieldDef27 As ParameterFieldDefinition
        Dim ParamFieldDef28 As ParameterFieldDefinition
        Dim ParamFieldDef29 As ParameterFieldDefinition
        Dim ParamFieldDef30 As ParameterFieldDefinition
        Dim ParamFieldDef31 As ParameterFieldDefinition

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
        Dim ParameterValues25 As New ParameterValues()
        Dim ParameterValues26 As New ParameterValues()
        Dim ParameterValues27 As New ParameterValues()
        Dim ParameterValues28 As New ParameterValues()
        Dim ParameterValues29 As New ParameterValues()
        Dim ParameterValues30 As New ParameterValues()
        Dim ParameterValues31 As New ParameterValues()

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
        Dim ParamDiscreteValue25 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue26 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue27 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue28 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue29 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue30 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue31 As New ParameterDiscreteValue()

        strSelLocation = Replace(strSelLocation, "','", ", ")

        ParamDiscreteValue1.Value = strRptId
        ParamDiscreteValue2.Value = strRptName
        ParamDiscreteValue3.Value = strSelLocation
        ParamDiscreteValue4.Value = strSelAccMonth
        ParamDiscreteValue5.Value = strSelAccYear
        ParamDiscreteValue6.Value = intSelDecimal
        ParamDiscreteValue7.Value = strSelSupress
        ParamDiscreteValue8.Value = strCompanyName
        ParamDiscreteValue9.Value = strLocationName
        ParamDiscreteValue10.Value = strPrintedBy
        ParamDiscreteValue11.Value = UCase(LocTag)
        ParamDiscreteValue12.Value = UCase(VehUsgTag) & " " & UCase(IDTag)
        ParamDiscreteValue13.Value = UCase(VehTag) & " " & UCase(CodeTag)
        ParamDiscreteValue14.Value = UCase(VehTypeTag) & " " & UCase(CodeTag)
        ParamDiscreteValue15.Value = UCase(AccTag) & " " & UCase(CodeTag)
        If LCase(strSrchBlkType) = "blkcode" Then
            ParamDiscreteValue16.Value = BlkTag & " " & CodeTag
        ElseIf LCase(strSrchBlkType) = "subblkcode" Then
            ParamDiscreteValue16.Value = SubBlkTag & " " & CodeTag
        Else
            ParamDiscreteValue16.Value = UCase(BlkGrpTag) & " " & UCase(CodeTag)
        End If
        ParamDiscreteValue17.Value = strSrchVehUsgId
        ParamDiscreteValue18.Value = strSrchVehCode
        ParamDiscreteValue19.Value = strSrchVehTypeCode
        ParamDiscreteValue20.Value = strSrchAccCode
        If LCase(strSrchBlkType) = "blkcode" Then
            ParamDiscreteValue21.Value = strSrchBlkCode
        ElseIf LCase(strSrchBlkType) = "subblkcode" Then
            ParamDiscreteValue21.Value = strSrchSubBlkCode
        Else
            ParamDiscreteValue21.Value = strSrchBlkGrpCode
        End If
        ParamDiscreteValue22.Value = strSrchText 
        ParamDiscreteValue23.Value = LocTag
        ParamDiscreteValue24.Value = VehUsgTag & " " & IDTag
        ParamDiscreteValue25.Value = VehTag 
        ParamDiscreteValue26.Value = AccTag & " " & CodeTag
        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
            ParamDiscreteValue27.Value = BlkTag & " " & CodeTag
        Else
            ParamDiscreteValue27.Value = SubBlkTag & " " & CodeTag
        End If
        ParamDiscreteValue28.Value = RunUnitTag
        ParamDiscreteValue29.Value = UCase(BlkTag & " Type")
        ParamDiscreteValue30.Value = IIf(Session("SS_INTER_ESTATE_CHARGING"), "true", "false")
        ParamDiscreteValue31.Value = strIndex

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
        ParamFieldDef10 = ParamFieldDefs.Item("SessionPrintedBy")
        ParamFieldDef11 = ParamFieldDefs.Item("LocTag")
        ParamFieldDef12 = ParamFieldDefs.Item("VehUsgIdTag")
        ParamFieldDef13 = ParamFieldDefs.Item("VehCodeTag")
        ParamFieldDef14 = ParamFieldDefs.Item("VehTypeCode")
        ParamFieldDef15 = ParamFieldDefs.Item("AccCodeTag")
        ParamFieldDef16 = ParamFieldDefs.Item("BlkCodeTag")
        ParamFieldDef17 = ParamFieldDefs.Item("SrchVehUsgId")
        ParamFieldDef18 = ParamFieldDefs.Item("SrchVehCode")
        ParamFieldDef19 = ParamFieldDefs.Item("SrchVehTypeCode")
        ParamFieldDef20 = ParamFieldDefs.Item("SrchAccCode")
        ParamFieldDef21 = ParamFieldDefs.Item("SrchBlkCode")
        ParamFieldDef22 = ParamFieldDefs.Item("SrchStatus")
        ParamFieldDef23 = ParamFieldDefs.Item("HeadLoc")
        ParamFieldDef24 = ParamFieldDefs.Item("HeadVehUsgId")
        ParamFieldDef25 = ParamFieldDefs.Item("HeadVehicle")
        ParamFieldDef26 = ParamFieldDefs.Item("HeadAccCode")
        ParamFieldDef27 = ParamFieldDefs.Item("HeadBlkCode")
        ParamFieldDef28 = ParamFieldDefs.Item("HeadRunUnit")
        ParamFieldDef29 = ParamFieldDefs.Item("BlkTypeTag")
        ParamFieldDef30 = ParamFieldDefs.Item("ParamInterEstateEnabled")
        ParamFieldDef31 = ParamFieldDefs.Item("ParamstrIndex")

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
        ParameterValues25 = ParamFieldDef25.CurrentValues
        ParameterValues26 = ParamFieldDef26.CurrentValues
        ParameterValues27 = ParamFieldDef27.CurrentValues
        ParameterValues28 = ParamFieldDef28.CurrentValues
        ParameterValues29 = ParamFieldDef29.CurrentValues
        ParameterValues30 = ParamFieldDef30.CurrentValues
        ParameterValues31 = ParamFieldDef31.CurrentValues

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
        ParameterValues25.Add(ParamDiscreteValue25)
        ParameterValues26.Add(ParamDiscreteValue26)
        ParameterValues27.Add(ParamDiscreteValue27)
        ParameterValues28.Add(ParamDiscreteValue28)
        ParameterValues29.Add(ParamDiscreteValue29)
        ParameterValues30.Add(ParamDiscreteValue30)
        ParameterValues31.Add(ParamDiscreteValue31)

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
        ParamFieldDef25.ApplyCurrentValues(ParameterValues25)
        ParamFieldDef26.ApplyCurrentValues(ParameterValues26)
        ParamFieldDef27.ApplyCurrentValues(ParameterValues27)
        ParamFieldDef28.ApplyCurrentValues(ParameterValues28)
        ParamFieldDef29.ApplyCurrentValues(ParameterValues29)
        ParamFieldDef30.ApplyCurrentValues(ParameterValues30)
        ParamFieldDef31.ApplyCurrentValues(ParameterValues31)
    End Sub

End Class
