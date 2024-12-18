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

Public Class FA_Rpt_AssetPermit : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents lblErrMesage As Label

    Dim objFASetup As New agri.FA.clsSetup()
    Dim objFATrx As New agri.FA.clsTrx()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim rdCrystalViewer As New ReportDocument()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String

    Dim strStatus As String
    Dim strAssetCode As String
    Dim strDescription As String
    Dim strUpdateBy As String
    Dim strSortExp As String
    Dim strSortCol As String

    Dim DocTitleTag As String
    Dim AssetCodeTag As String
    Dim AssetDescTag As String
    Dim AssetHeaderTag As String
    Dim AssetAddPermTag As String
    Dim AssetGenDeprPermTag As String
    Dim AssetManDeprPermTag As String
    Dim AssetDispPermTag As String
    Dim AssetWOPermTag As String

    Public Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")

        crvView.Visible = False

        strStatus = Trim(Request.QueryString("strStatus"))
        strAssetCode = Trim(Request.QueryString("strAssetCode"))
        strDescription = Trim(Request.QueryString("strDescription"))
        strUpdateBy = Trim(Request.QueryString("strUpdateBy"))
        strSortExp = Trim(Request.QueryString("strSortExp"))
        strSortCol = Trim(Request.QueryString("strSortCol"))
        DocTitleTag = Trim(Request.QueryString("DocTitleTag"))
        AssetCodeTag = Trim(Request.QueryString("AssetCodeTag"))
        AssetDescTag = Trim(Request.QueryString("AssetDescTag"))
        AssetHeaderTag = Trim(Request.QueryString("AssetHeaderTag"))
        AssetAddPermTag = Trim(Request.QueryString("AssetAddPermTag"))
        AssetGenDeprPermTag = Trim(Request.QueryString("AssetGenDeprPermTag"))
        AssetManDeprPermTag = Trim(Request.QueryString("AssetManDeprPermTag"))
        AssetDispPermTag = Trim(Request.QueryString("AssetDispPermTag"))
        AssetWOPermTag = Trim(Request.QueryString("AssetWOPermTag"))

        Bind_ITEM(True)

    End Sub

    Sub Bind_ITEM(ByVal blnIsPDFFormat As Boolean)
        Dim crLogonInfo As CrystalDecisions.Shared.TableLogOnInfo
        Dim objRptDs As New DataSet()
        Dim objMapPath As New Object()
        Dim strOpCd As String = "FA_CLSSETUP_ASSETPERMIT_LIST_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim strSearch As String
        Dim strSortItem As String
        Dim intCnt As Integer
        Dim dr As DataRow

        strParam = strAssetCode & "|" & _
                    strStatus & "|" & _
                    strUpdateBy & "|" & _
                    strSortExp & " " & strSortItem

        Try
            intErrNo = objFASetup.mtdGetAssetPermit(strOpCd, _
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
            objRptDs.Tables(0).Rows(intCnt).Item("AssetCode") = objRptDs.Tables(0).Rows(intCnt).Item("AssetCode").Trim()
            objRptDs.Tables(0).Rows(intCnt).Item("Description") = objRptDs.Tables(0).Rows(intCnt).Item("Description").Trim()
            objRptDs.Tables(0).Rows(intCnt).Item("HeaderDesc") = objRptDs.Tables(0).Rows(intCnt).Item("HeaderDesc").Trim()
            objRptDs.Tables(0).Rows(intCnt).Item("Username") = objRptDs.Tables(0).Rows(intCnt).Item("Username").Trim()
        Next

        For intCnt = 0 To objRptDs.Tables(0).Rows.Count - 1
            objRptDs.Tables(0).Rows(intCnt).Item("Status") = objFASetup.mtdGetAssetPermitStatus(objRptDs.Tables(0).Rows(intCnt).Item("Status"))
            objRptDs.Tables(0).Rows(intCnt).Item("AssetAddPerm") = IIf(objRptDs.Tables(0).Rows(intCnt).Item("AssetAddPerm") = objFASetup.EnumAssetAddPerm.Yes, objFASetup.EnumAssetAddPerm.Yes, objFASetup.EnumAssetAddPerm.No)
            objRptDs.Tables(0).Rows(intCnt).Item("AssetGenDeprPerm") = IIf(objRptDs.Tables(0).Rows(intCnt).Item("AssetGenDeprPerm") = objFASetup.EnumAssetGenDeprPerm.Yes, objFASetup.EnumAssetGenDeprPerm.Yes, objFASetup.EnumAssetGenDeprPerm.No)
            objRptDs.Tables(0).Rows(intCnt).Item("AssetManDeprPerm") = IIf(objRptDs.Tables(0).Rows(intCnt).Item("AssetManDeprPerm") = objFASetup.EnumAssetManDeprPerm.Yes, objFASetup.EnumAssetManDeprPerm.Yes, objFASetup.EnumAssetManDeprPerm.No)
            objRptDs.Tables(0).Rows(intCnt).Item("AssetDispPerm") = IIf(objRptDs.Tables(0).Rows(intCnt).Item("AssetDispPerm") = objFASetup.EnumAssetDispPerm.Yes, objFASetup.EnumAssetDispPerm.Yes, objFASetup.EnumAssetDispPerm.No)
            objRptDs.Tables(0).Rows(intCnt).Item("AssetWOPerm") = IIf(objRptDs.Tables(0).Rows(intCnt).Item("AssetWOPerm") = objFASetup.EnumAssetWOPerm.Yes, objFASetup.EnumAssetWOPerm.Yes, objFASetup.EnumAssetWOPerm.No)
            objRptDs.Tables(0).Rows(intCnt).Item("UpdateDate") = objGlobal.GetLongDate(objRptDs.Tables(0).Rows(intCnt).Item("UpdateDate"))
        Next

        rdCrystalViewer.Load(objMapPath & "Web\EN\FA\Reports\Crystal\FA_Rpt_AssetPermit.rpt", OpenReportMethod.OpenReportByTempCopy)
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
            DiskOpts.DiskFileName = objMapPath & "web\ftp\FA_Rpt_AssetPermit.pdf"
            rdCrystalViewer.ExportOptions.DestinationOptions = DiskOpts
            rdCrystalViewer.Export()

            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../../ftp/FA_Rpt_AssetPermit.pdf"">")
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

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("strCompName")
        paramField2 = paramFields.Item("strLocName")
        paramField3 = paramFields.Item("DocTitleTag")
        paramField4 = paramFields.Item("AssetCodeTag")
        paramField5 = paramFields.Item("AssetDescTag")
        paramField6 = paramFields.Item("AssetHeaderTag")
        paramField7 = paramFields.Item("AssetAddPermTag")
        paramField8 = paramFields.Item("AssetGenDeprPermTag")
        paramField9 = paramFields.Item("AssetManDeprPermTag")
        paramField10 = paramFields.Item("AssetDispPermTag")
        paramField11 = paramFields.Item("AssetWOPermTag")

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

        ParamDiscreteValue1.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue2.Value = Session("SS_LOCATIONNAME")
        ParamDiscreteValue3.Value = DocTitleTag
        ParamDiscreteValue4.Value = AssetCodeTag
        ParamDiscreteValue5.Value = AssetDescTag
        ParamDiscreteValue6.Value = AssetHeaderTag
        ParamDiscreteValue7.Value = AssetAddPermTag
        ParamDiscreteValue8.Value = AssetGenDeprPermTag
        ParamDiscreteValue9.Value = AssetManDeprPermTag
        ParamDiscreteValue10.Value = AssetDispPermTag
        ParamDiscreteValue11.Value = AssetWOPermTag

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

        crvView.ParameterFieldInfo = paramFields
    End Sub

End Class
