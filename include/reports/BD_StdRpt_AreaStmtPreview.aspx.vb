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

Public Class BD_StdRpt_AreaStmtPreview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objBD As New agri.BD.clsReport()
    Dim objBDTrx As New agri.BD.clsTrx()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strUserLoc As String
    Dim intDecimal As Integer
    Dim strLocationTag As String
    Dim strAddAdjustmentTag As String
    Dim strLessAdjustmentTag As String

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

        Dim objRptDs As New DataSet()
        Dim objRptDsNew As New DataSet()
        Dim objRptDsUnplanted As New DataSet()
        Dim objRptDsAdjustment As New DataSet()
        Dim objRptDsTitleArea As New DataSet()
        Dim objMapPath As String

        Dim intCnt As Integer

        Dim SearchStr As String
        Dim strParam As String

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOpCd_AreaStatement_GET As String = "BD_STDRPT_AREASTATEMENT_GET"
        Dim strOpCd_AreaStatement_TitleArea_GET As String = "BD_STDRPT_AREASTATEMENT_TITLEAREA_GET"

        strParam = Request.QueryString("DDLPeriodID") & "|" & objBDTrx.EnumAreaType.MatureArea & "|" & objBDTrx.EnumAreaStatementStatus.Active & "|" & strLocation & "|"
        Try
            intErrNo = objBD.mtdGetReport_AreaStatement(strOpCd_AreaStatement_GET, strParam, objRptDs, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=BD_AREA_STMT_MATURE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        strParam = Request.QueryString("DDLPeriodID") & "|" & objBDTrx.EnumAreaType.NewArea & "|" & objBDTrx.EnumAreaStatementStatus.Active & "|" & strLocation & "|"
        Try
            intErrNo = objBD.mtdGetReport_AreaStatement(strOpCd_AreaStatement_GET, strParam, objRptDsNew, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=BD_AREA_STMT_NEW&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        strParam = Request.QueryString("DDLPeriodID") & "|" & objBDTrx.EnumAreaType.UnPlantedArea & "|" & objBDTrx.EnumAreaStatementStatus.Active & "|" & strLocation & "|"
        Try
            intErrNo = objBD.mtdGetReport_AreaStatement(strOpCd_AreaStatement_GET, strParam, objRptDsUnplanted, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=BD_AREA_STMT_UNPLANTED&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        strParam = Request.QueryString("DDLPeriodID") & "|" & objBDTrx.EnumAreaType.Adjustment & "|" & objBDTrx.EnumAreaStatementStatus.Active & "|" & strLocation & "|"
        Try
            intErrNo = objBD.mtdGetReport_AreaStatement(strOpCd_AreaStatement_GET, strParam, objRptDsAdjustment, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=BD_AREA_STMT_UNPLANTED&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        strParam = Request.QueryString("DDLPeriodID") & "||||"
        Try
            intErrNo = objBD.mtdGetReport_AreaStatement(strOpCd_AreaStatement_TitleArea_GET, strParam, objRptDsTitleArea, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=BD_AREA_STMT_TITLEAREA&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        objRptDs.Tables(0).TableName = "BD_MATURE_AREA"
        objRptDs.Tables.Add(objRptDsNew.Tables(0).Copy())
        objRptDs.Tables(1).TableName = "BD_NEW_AREA"
        objRptDs.Tables.Add(objRptDsUnplanted.Tables(0).Copy())
        objRptDs.Tables(2).TableName = "BD_UNPLANTED_AREA"
        objRptDs.Tables.Add(objRptDsAdjustment.Tables(0).Copy())
        objRptDs.Tables(3).TableName = "BD_ADJUSTMENT_AREA"
        objRptDs.Tables.Add(objRptDsTitleArea.Tables(0).Copy())
        objRptDs.Tables(4).TableName = "BD_TITLE_AREA"


        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\BD_StdRpt_AreaStmt.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs)

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3

        crvView.Visible = False                         
        crvView.ReportSource = rdCrystalViewer          
        crvView.DataBind()                              

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\BD_StdRpt_AreaStmt.pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/BD_StdRpt_AreaStmt.pdf"">")
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
        paramField9 = paramFields.Item("ParamAddAdjustmentTag")
        paramField10 = paramFields.Item("ParamLessAdjustmentTag")

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

        ParamDiscreteValue1.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue2.Value = Session("SS_LOCATION")
        ParamDiscreteValue3.Value = Session("SS_USERNAME")
        ParamDiscreteValue4.Value = Request.QueryString("Decimal")
        ParamDiscreteValue5.Value = Request.QueryString("RptID")
        ParamDiscreteValue6.Value = Request.QueryString("RptName")
        ParamDiscreteValue7.Value = Request.QueryString("DDLPeriodName")
        ParamDiscreteValue8.Value = strLocationTag
        ParamDiscreteValue9.Value = strAddAdjustmentTag
        ParamDiscreteValue10.Value = strLessAdjustmentTag

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

        crvView.ParameterFieldInfo = paramFields
    End Sub

    Sub onload_GetLangCap()

        GetEntireLangCap()
        strLocationTag = GetCaption(objLangCap.EnumLangCap.Location)
        strAddAdjustmentTag = GetCaption(objLangCap.EnumLangCap.AddAdjustment)
        strLessAdjustmentTag = GetCaption(objLangCap.EnumLangCap.LessAdjustment)
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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=BD_STDRPT_AREASTMT_CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
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
