Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports System.Xml
Imports System.Web.Services
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web

Public Class BD_StdRpt_PlantationOHSumPreview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objBD As New agri.BD.clsReport()
    Dim objBDSetup As New agri.BD.clsSetup()
    Dim objBDTrx As New agri.BD.clsTrx()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strUserLoc As String
    Dim strLocationTag As String

    Dim objLangCapDs As New DataSet()
    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer
    Dim strLocType As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        crvView.Visible = False

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strLangCode = Session("SS_LANGCODE")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            onload_GetLangCap()
            BindReport()
        End If
    End Sub

    Sub BindReport()

        Dim dsTitleArea As New DataSet()
        Dim dsProd As New DataSet()
        Dim dsFigure As New DataSet()
        Dim objRptDs As New DataSet()
        Dim objMapPath As String

        Dim strParam As String
        Dim strFormRef As String
        Dim intCnt As Integer

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOpCd_OverHead_Sum_GET As String = "BD_STDRPT_OVERHEAD_SUM_GET"
        Dim strOppCd_OverHead_Sum_Figure_GET As String = "BD_STDRPT_OVERHEAD_SUM_FIGURE_GET"
        Dim strOppCd_OverHead_Sum_CostPerArea_GET As String = "BD_CLSTRX_OVERHEAD_COSTPERAREA_SUM"
        Dim strOppCd_OverHead_Sum_CostPerWeight_GET As String = "BD_CLSTRX_OVERHEAD_COSTPERWEIGHT_SUM"


        strParam = Request.QueryString("DDLPeriodID") & "|" & strLocation & "|AND AreaType IN ('" & objBDTrx.EnumAreaType.MatureArea & "','" & objBDTrx.EnumAreaType.NewArea & "')"
        Try
            intErrNo = objBDTrx.mtdGetOverHeadSum(strOppCd_OverHead_Sum_CostPerArea_GET, strParam, dsTitleArea)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=BD_STDRPT_OVERHEADSUM_AREASTMT&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        strParam = Request.QueryString("DDLPeriodID") & "|" & strLocation & "|"
        Try
            intErrNo = objBDTrx.mtdGetOverHeadSum(strOppCd_OverHead_Sum_CostPerWeight_GET, strParam, dsProd)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=BD_STDRPT_OVERHEADSUM_PROD&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        strParam = Request.QueryString("DDLPeriodID") & "|" & strLocation & "|ORDER BY OHS.DispSeq ASC|" & objBDSetup.EnumBudgetFormatItem.Header & "|"
        Try
            intErrNo = objBD.mtdGetReport_Overhead(strOpCd_OverHead_Sum_GET, strParam, objRptDs, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=BD_STDRPT_OVERHEADSUM_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objRptDs.Tables(0).Rows.Count - 1
            strFormRef = Trim(objRptDs.Tables(0).Rows(intCnt).Item("ItemCalcFormula"))

            strParam = Request.QueryString("DDLPeriodID") & "|" & strLocation & "||" & objBDSetup.EnumBudgetFormatItem.Formula & "','" & objBDSetup.EnumBudgetFormatItem.Total & "|" & strFormRef
            Try
                intErrNo = objBD.mtdGetReport_Overhead(strOppCd_OverHead_Sum_Figure_GET, strParam, dsFigure, objMapPath)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=BD_STDRPT_OVERHEAD_SUM_FIGURE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try

            objRptDs.Tables(0).Rows(intCnt).Item("AreaSize") = Trim(dsTitleArea.Tables(0).Rows(0).Item("AreaSize"))
            objRptDs.Tables(0).Rows(intCnt).Item("Yield") = Trim(dsProd.Tables(0).Rows(0).Item("Yield"))

            If dsFigure.Tables(0).Rows.Count > 0 Then
                If Trim(dsFigure.Tables(0).Rows(0).Item("ItemDisplayType")) = objBDSetup.EnumBudgetFormatItem.Total Then
                    objRptDs.Tables(0).Rows(intCnt).Item("Cost") = dsFigure.Tables(0).Rows(0).Item("OtherCost") + _
                                                                   dsFigure.Tables(0).Rows(0).Item("LabourCost") + _
                                                                   dsFigure.Tables(0).Rows(0).Item("MaterialCost")
                Else
                    If Trim(dsFigure.Tables(0).Rows(0).Item("ItemDisplayCol")) = objBDSetup.EnumBudgetItemColumn.Other Then
                        objRptDs.Tables(0).Rows(intCnt).Item("Cost") = Trim(dsFigure.Tables(0).Rows(0).Item("OtherCost"))
                    ElseIf Trim(dsFigure.Tables(0).Rows(0).Item("ItemDisplayCol")) = objBDSetup.EnumBudgetItemColumn.Labour Then
                        objRptDs.Tables(0).Rows(intCnt).Item("Cost") = Trim(dsFigure.Tables(0).Rows(0).Item("LabourCost"))
                    ElseIf Trim(dsFigure.Tables(0).Rows(0).Item("ItemDisplayCol")) = objBDSetup.EnumBudgetItemColumn.Material Then
                        objRptDs.Tables(0).Rows(intCnt).Item("Cost") = Trim(dsFigure.Tables(0).Rows(0).Item("MaterialCost"))
                    End If
                End If
                objRptDs.Tables(0).Rows(intCnt).Item("CostPerArea") = Trim(dsFigure.Tables(0).Rows(0).Item("CostPerArea"))
                objRptDs.Tables(0).Rows(intCnt).Item("CostPerWeight") = Trim(dsFigure.Tables(0).Rows(0).Item("CostPerWeight"))
            End If

        Next


        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\BD_StdRpt_PlantationOHSum.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3

        crvView.Visible = False                         
        crvView.ReportSource = rdCrystalViewer          
        crvView.DataBind()                              

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\BD_StdRpt_PlantationOHSum.pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/BD_StdRpt_PlantationOHSum.pdf"">")
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

        Dim ParamDiscreteValue1 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue2 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue3 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue4 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue5 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue6 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue7 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue8 As New ParameterDiscreteValue()

        Dim crParameterValues1 As ParameterValues
        Dim crParameterValues2 As ParameterValues
        Dim crParameterValues3 As ParameterValues
        Dim crParameterValues4 As ParameterValues
        Dim crParameterValues5 As ParameterValues
        Dim crParameterValues6 As ParameterValues
        Dim crParameterValues7 As ParameterValues
        Dim crParameterValues8 As ParameterValues

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("ParamCompanyName")
        paramField2 = paramFields.Item("ParamLocation")
        paramField3 = paramFields.Item("ParamUserName")
        paramField4 = paramFields.Item("ParamDecimal")
        paramField5 = paramFields.Item("ParamRptID")
        paramField6 = paramFields.Item("ParamRptName")
        paramField7 = paramFields.Item("ParamPeriod")
        paramField8 = paramFields.Item("ParamLocationTag")

        crParameterValues1 = paramField1.CurrentValues
        crParameterValues2 = paramField2.CurrentValues
        crParameterValues3 = paramField3.CurrentValues
        crParameterValues4 = paramField4.CurrentValues
        crParameterValues5 = paramField5.CurrentValues
        crParameterValues6 = paramField6.CurrentValues
        crParameterValues7 = paramField7.CurrentValues
        crParameterValues8 = paramField8.CurrentValues

        ParamDiscreteValue1.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue2.Value = Session("SS_LOCATION")
        ParamDiscreteValue3.Value = Session("SS_USERNAME")
        ParamDiscreteValue4.Value = Request.QueryString("Decimal")
        ParamDiscreteValue5.Value = Request.QueryString("RptID")
        ParamDiscreteValue6.Value = Request.QueryString("RptName")
        ParamDiscreteValue7.Value = Request.QueryString("DDLPeriodName")
        ParamDiscreteValue8.Value = UCase(strLocationTag)

        crParameterValues1.Add(ParamDiscreteValue1)
        crParameterValues2.Add(ParamDiscreteValue2)
        crParameterValues3.Add(ParamDiscreteValue3)
        crParameterValues4.Add(ParamDiscreteValue4)
        crParameterValues5.Add(ParamDiscreteValue5)
        crParameterValues6.Add(ParamDiscreteValue6)
        crParameterValues7.Add(ParamDiscreteValue7)
        crParameterValues8.Add(ParamDiscreteValue8)

        PFDefs(0).ApplyCurrentValues(crParameterValues1)
        PFDefs(1).ApplyCurrentValues(crParameterValues2)
        PFDefs(2).ApplyCurrentValues(crParameterValues3)
        PFDefs(3).ApplyCurrentValues(crParameterValues4)
        PFDefs(4).ApplyCurrentValues(crParameterValues5)
        PFDefs(5).ApplyCurrentValues(crParameterValues6)
        PFDefs(6).ApplyCurrentValues(crParameterValues7)
        PFDefs(7).ApplyCurrentValues(crParameterValues8)

        crvView.ParameterFieldInfo = paramFields
    End Sub

    Sub onload_GetLangCap()

        GetEntireLangCap()
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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=BD_STDRPT_OVERHEADSUM_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

        For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If Trim(strLocType) = objAdminLoc.EnumLocType.Mill Then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                Else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                End If
                Exit For
            End If
        Next
    End Function
End Class
