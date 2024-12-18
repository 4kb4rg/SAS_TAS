Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic
Imports System.XML
Imports System.Web.Services
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web

Imports agri.FA.clsSetup
Imports agri.Admin.clsShare
Imports agri.GlobalHdl.clsGlobalHdl

Public Class FA_Rpt_AssetRegln : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents lblErrMesage As Label

    Dim objFASetup As New agri.FA.clsSetup()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim rdCrystalViewer As New ReportDocument()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String

    Dim strStatus As String
    Dim strAssetCode As String
    Dim strAssetHeaderCode As String
    Dim strUpdateBy As String
    Dim strSortExp As String
    Dim strSortCol As String

    Dim DocTitleTag As String
    Dim AssetHeaderTag As String
    Dim AssetClassTag As String
    Dim AssetGrpTag As String
    Dim AssetCodeTag As String
    Dim SerialNoTag As String
    Dim DescriptionTag As String
    Dim ExtDescriptionTag As String
    Dim AcquisitionModeTag As String
    Dim PurchaseDateTag As String
    Dim DeprMethodTag As String
    Dim YearDeprRateTag As String
    Dim StartDeprPeriodTag As String
    Dim EndDeprPeriodTag As String
    Dim LastDeprPeriodTag As String
    Dim DeprIndTag As String
    Dim GMValueTag As String
    Dim AssetValueTag As String
    Dim AccumDeprValueTag As String
    Dim DispWOAssetValueTag As String
    Dim DispWOAccumDeprValueTag As String
    Dim NetValueTag As String
    Dim FinalValueTag As String
    Dim DeprGLDeprAccCodeTag As String
    Dim DeprGLAccumDeprAccCodeTag As String
    Dim DispGLAssetAccCodeTag As String
    Dim DispGLAccumDeprAccCodeTag As String
    Dim DispGLGainLossAccCodeTag As String
    Dim WOGLAssetAccCodeTag As String
    Dim WOGLAccumDeprAccCodeTag As String
    Dim WOGLWOAccCodeTag As String

    Public Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")

        crvView.Visible = False

        strStatus = Trim(Request.QueryString("strStatus"))
        strAssetCode = Trim(Request.QueryString("strAssetCode"))
        strAssetHeaderCode = Trim(Request.QueryString("strAssetHeaderCode"))
        strUpdateBy = Trim(Request.QueryString("strUpdateBy"))
        strSortExp = Trim(Request.QueryString("strSortExp"))
        strSortCol = Trim(Request.QueryString("strSortCol"))
        DocTitleTag = Trim(Request.QueryString("DocTitleTag"))
        AssetHeaderTag = Trim(Request.QueryString("AssetHeaderTag"))
        AssetClassTag = Trim(Request.QueryString("AssetClassTag"))
        AssetGrpTag = Trim(Request.QueryString("AssetGrpTag"))
        AssetCodeTag = Trim(Request.QueryString("AssetCodeTag"))
        SerialNoTag = Trim(Request.QueryString("SerialNoTag"))
        DescriptionTag = Trim(Request.QueryString("DescriptionTag"))
        ExtDescriptionTag = Trim(Request.QueryString("ExtDescriptionTag"))
        AcquisitionModeTag = Trim(Request.QueryString("AcquisitionModeTag"))
        PurchaseDateTag = Trim(Request.QueryString("PurchaseDateTag"))
        DeprMethodTag = Trim(Request.QueryString("DeprMethodTag"))
        YearDeprRateTag = Trim(Request.QueryString("YearDeprRateTag"))
        StartDeprPeriodTag = Trim(Request.QueryString("StartDeprPeriodTag"))
        EndDeprPeriodTag = Trim(Request.QueryString("EndDeprPeriodTag"))
        LastDeprPeriodTag = Trim(Request.QueryString("LastDeprPeriodTag"))
        DeprIndTag = Trim(Request.QueryString("DeprIndTag"))
        GMValueTag = Trim(Request.QueryString("GMValueTag"))
        AssetValueTag = Trim(Request.QueryString("AssetValueTag"))
        AccumDeprValueTag = Trim(Request.QueryString("AccumDeprValueTag"))
        DispWOAssetValueTag = Trim(Request.QueryString("DispWOAssetValueTag"))
        DispWOAccumDeprValueTag = Trim(Request.QueryString("DispWOAccumDeprValueTag"))
        NetValueTag = Trim(Request.QueryString("NetValueTag"))
        FinalValueTag = Trim(Request.QueryString("FinalValueTag"))
        DeprGLDeprAccCodeTag = Trim(Request.QueryString("DeprGLDeprAccCodeTag"))
        DeprGLAccumDeprAccCodeTag = Trim(Request.QueryString("DeprGLAccumDeprAccCodeTag"))
        DispGLAssetAccCodeTag = Trim(Request.QueryString("DispGLAssetAccCodeTag"))
        DispGLAccumDeprAccCodeTag = Trim(Request.QueryString("DispGLAccumDeprAccCodeTag"))
        DispGLGainLossAccCodeTag = Trim(Request.QueryString("DispGLGainLossAccCodeTag"))
        WOGLAssetAccCodeTag = Trim(Request.QueryString("WOGLAssetAccCodeTag"))
        WOGLAccumDeprAccCodeTag = Trim(Request.QueryString("WOGLAccumDeprAccCodeTag"))
        WOGLWOAccCodeTag = Trim(Request.QueryString("WOGLWOAccCodeTag"))

        Bind_ITEM(True)

    End Sub

    Sub Bind_ITEM(ByVal blnIsPDFFormat As Boolean)
        Dim crLogonInfo As CrystalDecisions.Shared.TableLogOnInfo
        Dim objRptDs As New DataSet()
        Dim objMapPath As New Object()
        Dim strOpCd As String = "FA_CLSSETUP_ASSETREGLN_LIST_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim strSearch As String
        Dim strSortItem As String
        Dim intCnt As Integer
        Dim dr As DataRow

        strParam = strAssetCode & "|" & _
                    strAssetHeaderCode & "|" & _
                    strStatus & "|" & _
                    strUpdateBy & "|" & _
                    strSortExp & " " & strSortItem & "|"

        Try
            intErrNo = objFASetup.mtdGetAssetRegln(strOpCd, _
                                                   strLocation, _
                                                   strParam, _
                                                   objRptDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=&errmesg=" & lblErrMesage.Text & "&redirect=")
        End Try

        Try
            intErrNo = objAdmin.mtdGetBasePath(objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=&errmesg=" & lblErrMesage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objRptDs.Tables(0).Rows.Count - 1
            objRptDs.Tables(0).Rows(intCnt).Item("AssetClassCode") = objRptDs.Tables(0).Rows(intCnt).Item("AssetClassCode").Trim()
            objRptDs.Tables(0).Rows(intCnt).Item("ClassDesc") = objRptDs.Tables(0).Rows(intCnt).Item("ClassDesc").Trim()
            objRptDs.Tables(0).Rows(intCnt).Item("AssetGrpCode") = objRptDs.Tables(0).Rows(intCnt).Item("AssetGrpCode").Trim()
            objRptDs.Tables(0).Rows(intCnt).Item("GrpDesc") = objRptDs.Tables(0).Rows(intCnt).Item("GrpDesc").Trim()
            objRptDs.Tables(0).Rows(intCnt).Item("AssetHeaderCode") = objRptDs.Tables(0).Rows(intCnt).Item("AssetHeaderCode").Trim()
            objRptDs.Tables(0).Rows(intCnt).Item("AssetHeaderDesc") = objRptDs.Tables(0).Rows(intCnt).Item("AssetHeaderDesc").Trim()
            objRptDs.Tables(0).Rows(intCnt).Item("AssetCode") = objRptDs.Tables(0).Rows(intCnt).Item("AssetCode").Trim()
            objRptDs.Tables(0).Rows(intCnt).Item("Serialno") = objRptDs.Tables(0).Rows(intCnt).Item("Serialno").Trim()
            objRptDs.Tables(0).Rows(intCnt).Item("AssetDesc") = objRptDs.Tables(0).Rows(intCnt).Item("AssetDesc").Trim()
            objRptDs.Tables(0).Rows(intCnt).Item("StartDeprMonth") = objRptDs.Tables(0).Rows(intCnt).Item("StartDeprMonth").Trim()
            objRptDs.Tables(0).Rows(intCnt).Item("StartDeprYear") = objRptDs.Tables(0).Rows(intCnt).Item("StartDeprYear").Trim()
            objRptDs.Tables(0).Rows(intCnt).Item("EndDeprMonth") = objRptDs.Tables(0).Rows(intCnt).Item("EndDeprMonth").Trim()
            objRptDs.Tables(0).Rows(intCnt).Item("EndDeprYear") = objRptDs.Tables(0).Rows(intCnt).Item("EndDeprYear").Trim()
            objRptDs.Tables(0).Rows(intCnt).Item("LastDeprMonth") = objRptDs.Tables(0).Rows(intCnt).Item("LastDeprMonth").Trim()
            objRptDs.Tables(0).Rows(intCnt).Item("LastDeprYear") = objRptDs.Tables(0).Rows(intCnt).Item("LastDeprYear").Trim()
            objRptDs.Tables(0).Rows(intCnt).Item("DeprInd") = objRptDs.Tables(0).Rows(intCnt).Item("DeprInd").Trim()
            objRptDs.Tables(0).Rows(intCnt).Item("DeprGLDeprAccCode") = objRptDs.Tables(0).Rows(intCnt).Item("DeprGLDeprAccCode").Trim()
            objRptDs.Tables(0).Rows(intCnt).Item("DeprGLAccumDeprAccCode") = objRptDs.Tables(0).Rows(intCnt).Item("DeprGLAccumDeprAccCode").Trim()
            objRptDs.Tables(0).Rows(intCnt).Item("DispGLAssetAccCode") = objRptDs.Tables(0).Rows(intCnt).Item("DispGLAssetAccCode").Trim()
            objRptDs.Tables(0).Rows(intCnt).Item("DispGLAccumDeprAccCode") = objRptDs.Tables(0).Rows(intCnt).Item("DispGLAccumDeprAccCode").Trim()
            objRptDs.Tables(0).Rows(intCnt).Item("DispGLGainLossAccCode") = objRptDs.Tables(0).Rows(intCnt).Item("DispGLGainLossAccCode").Trim()
            objRptDs.Tables(0).Rows(intCnt).Item("WOGLAssetAccCode") = objRptDs.Tables(0).Rows(intCnt).Item("WOGLAssetAccCode").Trim()
            objRptDs.Tables(0).Rows(intCnt).Item("WOGLAccumDeprAccCode") = objRptDs.Tables(0).Rows(intCnt).Item("WOGLAccumDeprAccCode").Trim()
            objRptDs.Tables(0).Rows(intCnt).Item("WOGLWOAccCode") = objRptDs.Tables(0).Rows(intCnt).Item("WOGLWOAccCode").Trim()
            objRptDs.Tables(0).Rows(intCnt).Item("Username") = objRptDs.Tables(0).Rows(intCnt).Item("Username").Trim()
        Next

        For intCnt = 0 To objRptDs.Tables(0).Rows.Count - 1
            objRptDs.Tables(0).Rows(intCnt).Item("Status") = objFASetup.mtdGetAssetReglnStatus(objRptDs.Tables(0).Rows(intCnt).Item("Status"))
            objRptDs.Tables(0).Rows(intCnt).Item("AcquisitionMode") = objFASetup.mtdGetAcquisitionMode(objRptDs.Tables(0).Rows(intCnt).Item("AcquisitionMode"))
            objRptDs.Tables(0).Rows(intCnt).Item("DeprMethod") = objFASetup.mtdGetDeprMethod(objRptDs.Tables(0).Rows(intCnt).Item("DeprMethod"))
            objRptDs.Tables(0).Rows(intCnt).Item("DeprInd") = objFASetup.mtdGetDeprInd(objRptDs.Tables(0).Rows(intCnt).Item("DeprInd"))
            objRptDs.Tables(0).Rows(intCnt).Item("UpdateDate") = objGlobal.GetLongDate(objRptDs.Tables(0).Rows(intCnt).Item("UpdateDate"))
        Next

        rdCrystalViewer.Load(objMapPath & "Web\EN\FA\Reports\Crystal\FA_Rpt_AssetRegln.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA4

        If Not blnIsPDFFormat Then
            crvView.Visible = True     
            crvView.ReportSource = rdCrystalViewer
            crvView.DataBind()
            PassParam()
        Else
            crvView.Visible = False
            crvView.ReportSource = rdCrystalViewer
            crvView.DataBind()
            PassParam()
            Dim DiskOpts As CrystalDecisions.Shared.DiskFileDestinationOptions = New CrystalDecisions.Shared.DiskFileDestinationOptions()
            rdCrystalViewer.ExportOptions.ExportDestinationType = CrystalDecisions.[Shared].ExportDestinationType.DiskFile
            rdCrystalViewer.ExportOptions.ExportFormatType = CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat
            DiskOpts.DiskFileName = objMapPath & "web\ftp\FA_Rpt_AssetRegLn.pdf"
            rdCrystalViewer.ExportOptions.DestinationOptions = DiskOpts
            rdCrystalViewer.Export()

            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../../ftp/FA_Rpt_AssetRegln.pdf"">")
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
        Dim paramField13 As New ParameterField()
        Dim paramField14 As New ParameterField()
        Dim paramField15 As New ParameterField()
        Dim paramField16 As New ParameterField()
        Dim paramField17 As New ParameterField()
        Dim paramField18 As New ParameterField()
        Dim paramField19 As New ParameterField()
        Dim paramField20 As New ParameterField()
        Dim paramField21 As New ParameterField()
        Dim paramField22 As New ParameterField()
        Dim paramField23 As New ParameterField()
        Dim paramField24 As New ParameterField()
        Dim paramField25 As New ParameterField()
        Dim paramField26 As New ParameterField()
        Dim paramField27 As New ParameterField()
        Dim paramField28 As New ParameterField()
        Dim paramField29 As New ParameterField()
        Dim paramField30 As New ParameterField()
        Dim paramField31 As New ParameterField()
        Dim paramField32 As New ParameterField()
        Dim paramField33 As New ParameterField()

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
        Dim ParamDiscreteValue32 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue33 As New ParameterDiscreteValue()

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
        Dim crParameterValues13 As ParameterValues
        Dim crParameterValues14 As ParameterValues
        Dim crParameterValues15 As ParameterValues
        Dim crParameterValues16 As ParameterValues
        Dim crParameterValues17 As ParameterValues
        Dim crParameterValues18 As ParameterValues
        Dim crParameterValues19 As ParameterValues
        Dim crParameterValues20 As ParameterValues
        Dim crParameterValues21 As ParameterValues
        Dim crParameterValues22 As ParameterValues
        Dim crParameterValues23 As ParameterValues
        Dim crParameterValues24 As ParameterValues
        Dim crParameterValues25 As ParameterValues
        Dim crParameterValues26 As ParameterValues
        Dim crParameterValues27 As ParameterValues
        Dim crParameterValues28 As ParameterValues
        Dim crParameterValues29 As ParameterValues
        Dim crParameterValues30 As ParameterValues
        Dim crParameterValues31 As ParameterValues
        Dim crParameterValues32 As ParameterValues
        Dim crParameterValues33 As ParameterValues

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("strCompName")
        paramField2 = paramFields.Item("strLocName")
        paramField3 = paramFields.Item("DocTitleTag")
        paramField4 = paramFields.Item("AssetHeaderTag")
        paramField5 = paramFields.Item("AssetClassTag")
        paramField6 = paramFields.Item("AssetGrpTag")
        paramField7 = paramFields.Item("AssetCodeTag")
        paramField8 = paramFields.Item("SerialNoTag")
        paramField9 = paramFields.Item("DescriptionTag")
        paramField10 = paramFields.Item("AcquisitionModeTag")
        paramField11 = paramFields.Item("PurchaseDateTag")
        paramField12 = paramFields.Item("DeprMethodTag")
        paramField13 = paramFields.Item("YearDeprRateTag")
        paramField14 = paramFields.Item("StartDeprPeriodTag")
        paramField15 = paramFields.Item("EndDeprPeriodTag")
        paramField16 = paramFields.Item("LastDeprPeriodTag")
        paramField17 = paramFields.Item("DeprIndTag")
        paramField18 = paramFields.Item("GMValueTag")
        paramField19 = paramFields.Item("AssetValueTag")
        paramField20 = paramFields.Item("AccumDeprValueTag")
        paramField21 = paramFields.Item("DispWOAssetValueTag")
        paramField22 = paramFields.Item("DispWOAccumDeprValueTag")
        paramField23 = paramFields.Item("NetValueTag")
        paramField24 = paramFields.Item("FinalValueTag")
        paramField25 = paramFields.Item("DeprGLDeprAccCodeTag")
        paramField26 = paramFields.Item("DeprGLAccumDeprAccCodeTag")
        paramField27 = paramFields.Item("DispGLAssetAccCodeTag")
        paramField28 = paramFields.Item("DispGLAccumDeprAccCodeTag")
        paramField29 = paramFields.Item("DispGLGainLossAccCodeTag")
        paramField30 = paramFields.Item("WOGLAssetAccCodeTag")
        paramField31 = paramFields.Item("WOGLAccumDeprAccCodeTag")
        paramField32 = paramFields.Item("WOGLWOAccCodeTag")
        paramField33 = paramFields.Item("ExtDescriptionTag")

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
        crParameterValues13 = paramField13.CurrentValues
        crParameterValues14 = paramField14.CurrentValues
        crParameterValues15 = paramField15.CurrentValues
        crParameterValues16 = paramField16.CurrentValues
        crParameterValues17 = paramField17.CurrentValues
        crParameterValues18 = paramField18.CurrentValues
        crParameterValues19 = paramField19.CurrentValues
        crParameterValues20 = paramField20.CurrentValues
        crParameterValues21 = paramField21.CurrentValues
        crParameterValues22 = paramField22.CurrentValues
        crParameterValues23 = paramField23.CurrentValues
        crParameterValues24 = paramField24.CurrentValues
        crParameterValues25 = paramField25.CurrentValues
        crParameterValues26 = paramField26.CurrentValues
        crParameterValues27 = paramField27.CurrentValues
        crParameterValues28 = paramField28.CurrentValues
        crParameterValues29 = paramField29.CurrentValues
        crParameterValues30 = paramField30.CurrentValues
        crParameterValues31 = paramField31.CurrentValues
        crParameterValues32 = paramField32.CurrentValues
        crParameterValues33 = paramField33.CurrentValues

        ParamDiscreteValue1.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue2.Value = Session("SS_LOCATIONNAME")
        ParamDiscreteValue3.Value = DocTitleTag
        ParamDiscreteValue4.Value = AssetHeaderTag
        ParamDiscreteValue5.Value = AssetClassTag
        ParamDiscreteValue6.Value = AssetGrpTag
        ParamDiscreteValue7.Value = AssetCodeTag
        ParamDiscreteValue8.Value = SerialNoTag
        ParamDiscreteValue9.Value = DescriptionTag
        ParamDiscreteValue10.Value = AcquisitionModeTag
        ParamDiscreteValue11.Value = PurchaseDateTag
        ParamDiscreteValue12.Value = DeprMethodTag
        ParamDiscreteValue13.Value = YearDeprRateTag
        ParamDiscreteValue14.Value = StartDeprPeriodTag
        ParamDiscreteValue15.Value = EndDeprPeriodTag
        ParamDiscreteValue16.Value = LastDeprPeriodTag
        ParamDiscreteValue17.Value = DeprIndTag
        ParamDiscreteValue18.Value = GMValueTag
        ParamDiscreteValue19.Value = AssetValueTag
        ParamDiscreteValue20.Value = AccumDeprValueTag
        ParamDiscreteValue21.Value = DispWOAssetValueTag
        ParamDiscreteValue22.Value = DispWOAccumDeprValueTag
        ParamDiscreteValue23.Value = NetValueTag
        ParamDiscreteValue24.Value = FinalValueTag
        ParamDiscreteValue25.Value = DeprGLDeprAccCodeTag
        ParamDiscreteValue26.Value = DeprGLAccumDeprAccCodeTag
        ParamDiscreteValue27.Value = DispGLAssetAccCodeTag
        ParamDiscreteValue28.Value = DispGLAccumDeprAccCodeTag
        ParamDiscreteValue29.Value = DispGLGainLossAccCodeTag
        ParamDiscreteValue30.Value = WOGLAssetAccCodeTag
        ParamDiscreteValue31.Value = WOGLAccumDeprAccCodeTag
        ParamDiscreteValue32.Value = WOGLWOAccCodeTag
        ParamDiscreteValue33.Value = ExtDescriptionTag

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
        crParameterValues13.Add(ParamDiscreteValue13)
        crParameterValues14.Add(ParamDiscreteValue14)
        crParameterValues15.Add(ParamDiscreteValue15)
        crParameterValues16.Add(ParamDiscreteValue16)
        crParameterValues17.Add(ParamDiscreteValue17)
        crParameterValues18.Add(ParamDiscreteValue18)
        crParameterValues19.Add(ParamDiscreteValue19)
        crParameterValues20.Add(ParamDiscreteValue20)
        crParameterValues21.Add(ParamDiscreteValue21)
        crParameterValues22.Add(ParamDiscreteValue22)
        crParameterValues23.Add(ParamDiscreteValue23)
        crParameterValues24.Add(ParamDiscreteValue24)
        crParameterValues25.Add(ParamDiscreteValue25)
        crParameterValues26.Add(ParamDiscreteValue26)
        crParameterValues27.Add(ParamDiscreteValue27)
        crParameterValues28.Add(ParamDiscreteValue28)
        crParameterValues29.Add(ParamDiscreteValue29)
        crParameterValues30.Add(ParamDiscreteValue30)
        crParameterValues31.Add(ParamDiscreteValue31)
        crParameterValues32.Add(ParamDiscreteValue32)
        crParameterValues33.Add(ParamDiscreteValue33)

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
        PFDefs(12).ApplyCurrentValues(crParameterValues13)
        PFDefs(13).ApplyCurrentValues(crParameterValues14)
        PFDefs(14).ApplyCurrentValues(crParameterValues15)
        PFDefs(15).ApplyCurrentValues(crParameterValues16)
        PFDefs(16).ApplyCurrentValues(crParameterValues17)
        PFDefs(17).ApplyCurrentValues(crParameterValues18)
        PFDefs(18).ApplyCurrentValues(crParameterValues19)
        PFDefs(19).ApplyCurrentValues(crParameterValues20)
        PFDefs(20).ApplyCurrentValues(crParameterValues21)
        PFDefs(21).ApplyCurrentValues(crParameterValues22)
        PFDefs(22).ApplyCurrentValues(crParameterValues23)
        PFDefs(23).ApplyCurrentValues(crParameterValues24)
        PFDefs(24).ApplyCurrentValues(crParameterValues25)
        PFDefs(25).ApplyCurrentValues(crParameterValues26)
        PFDefs(26).ApplyCurrentValues(crParameterValues27)
        PFDefs(27).ApplyCurrentValues(crParameterValues28)
        PFDefs(28).ApplyCurrentValues(crParameterValues29)
        PFDefs(29).ApplyCurrentValues(crParameterValues30)
        PFDefs(30).ApplyCurrentValues(crParameterValues31)
        PFDefs(31).ApplyCurrentValues(crParameterValues32)
        PFDefs(32).ApplyCurrentValues(crParameterValues33)

        crvView.ParameterFieldInfo = paramFields
    End Sub

End Class
