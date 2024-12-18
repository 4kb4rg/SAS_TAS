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

Public Class BD_StdRpt_UnMatureCropPreview : Inherits Page

    Protected WithEvents crvUnMatCrop As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objBD As New agri.BD.clsReport()
    Dim objBDTrx As New agri.BD.clsTrx()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim tblTotalAreaFFB As DataTable = New DataTable("tblTotalAreaFFB")
    Dim dsTotalAreaFFB As New DataSet()
    Dim objLangCapDs As New Object()
    Dim dr As DataRow

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strUserLoc As String
    Dim intConfigsetting As Integer
    Dim strLocationTag As String

    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer
    Dim strLocType As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        crvUnMatCrop.Visible = False

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            onload_GetLangCap()
            BindReport()
        End If
    End Sub

    Sub CreateTableCols()

        Dim Col1 As DataColumn = New DataColumn()
        Dim Col2 As DataColumn = New DataColumn()
        Dim Col3 As DataColumn = New DataColumn()

        Col1.DataType = System.Type.GetType("System.String")
        Col1.AllowDBNull = True
        Col1.Caption = "BlkCode"
        Col1.ColumnName = "BlkCode"
        Col1.DefaultValue = 0
        tblTotalAreaFFB.Columns.Add(Col1)

        Col2.DataType = System.Type.GetType("System.Decimal")
        Col2.AllowDBNull = True
        Col2.Caption = "TotalAreaSize"
        Col2.ColumnName = "TotalAreaSize"
        Col2.DefaultValue = 0
        tblTotalAreaFFB.Columns.Add(Col2)

        Col3.DataType = System.Type.GetType("System.Decimal")
        Col3.AllowDBNull = True
        Col3.Caption = "TotalFFB"
        Col3.ColumnName = "TotalFFB"
        Col3.DefaultValue = 0
        tblTotalAreaFFB.Columns.Add(Col3)

    End Sub

    Sub BindReport()

        Dim dsTotalArea As New DataSet()
        Dim dsTotalYield As New DataSet()
        Dim objRptDs As New DataSet()
        Dim dsBlkCode As New DataSet()
        Dim objMapPath As String

        Dim strParam As String
        Dim strParamArea As String
        Dim strParamFFB As String
        Dim strParamImMatureCrop As String

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOpCd_ImMatureCrop_Format_GET As String = "BD_CLSTRX_UNMATURECROP_FORMAT_GET"
        Dim strOpCd_ImMatureCrop_BlkCode_GET As String = "BD_STDRPT_UNMATURECROP_BLKCODE_GET"
        Dim strOppCd_ImMatureCrop_TotalFFB_GET As String = "BD_CLSTRX_MATURECROP_PRODFFB_GET"
        Dim strOppCd_ImMatureCrop_TotalArea_GET As String

        Dim intCnt As Integer
        Dim strBlkCode As String
        Dim SearchStr As String

        Dim decTotalArea As Decimal
        Dim decTotalFFB As Decimal

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
            If Not Request.QueryString("BlkCode") = "" Then
                SearchStr = " AND MC.BlkCode LIKE '" & Request.QueryString("BlkCode") & "' "
            Else
                SearchStr = " AND MC.BlkCode LIKE '%' "
            End If
        Else
            If Not Request.QueryString("SubBlkCode") = "" Then
                SearchStr = " AND MC.BlkCode LIKE '" & Request.QueryString("SubBlkCode") & "' "
            Else
                SearchStr = " AND MC.BlkCode LIKE '%' "
            End If
        End If

        CreateTableCols()

        strParamImMatureCrop = Request.QueryString("DDLPeriodID") & "|" & strLocation & "|" & SearchStr & "||||" & "MC.BlkCode, MCS.DispSeq ASC"
        Try
            intErrNo = objBD.mtdGetReport_UnMatureCrop(strOpCd_ImMatureCrop_Format_GET, strParamImMatureCrop, objRptDs, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=BD_IMMATURECROP_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        strParam = strLocation & "|" & Request.QueryString("DDLPeriodID") & "|"
        Try
            intErrNo = objBD.mtdGetMatureCropBlkCode(strOpCd_ImMatureCrop_BlkCode_GET, strParam, dsBlkCode)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=BD_STDRPT_IMMATURECROP_BLKCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To dsBlkCode.Tables(0).Rows.Count - 1
            strBlkCode = Trim(dsBlkCode.Tables(0).Rows(intCnt).Item("BlkCode"))

            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
                strOppCd_ImMatureCrop_TotalArea_GET = "BD_CLSTRX_MATURECROP_BLKTOTALAREA_GET"
                strParamArea = objGLSetup.EnumBlockType.InMatureField & "|" & objGLSetup.EnumBlockStatus.Active & "|" & strLocation & "|" & strBlkCode & "|"
            Else
                strOppCd_ImMatureCrop_TotalArea_GET = "BD_CLSTRX_MATURECROP_SUBBLKTOTALAREA_GET"
                strParamArea = objGLSetup.EnumBlockType.InMatureField & "|" & objGLSetup.EnumSubBlockStatus.Active & "|" & strLocation & "|" & strBlkCode & "|"
            End If

            Try
                intErrNo = objBDTrx.mtdGetMatureCropTotalArea(strOppCd_ImMatureCrop_TotalArea_GET, strParamArea, dsTotalArea)
            Catch Exp As System.Exception
                Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=BD_STDRPT_IMMATURECROP_TOTALAREA_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try

            strParamFFB = strBlkCode & "|" & Request.QueryString("DDLPeriodID") & "|" & strLocation & "|"
            Try
                intErrNo = objBDTrx.mtdGetMatureCropTotalFFB(strOppCd_ImMatureCrop_TotalFFB_GET, strParamFFB, dsTotalYield)
            Catch Exp As System.Exception
                Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=BD_STDRPT_IMMATURECROP_TOTALYIELD_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try

            If dsTotalArea.Tables(0).Rows.Count > 0 Then
                decTotalArea = dsTotalArea.Tables(0).Rows(0).Item("AreaSize")
            Else
                decTotalArea = 0
            End If

            If dsTotalYield.Tables(0).Rows.Count > 0 Then
                decTotalFFB = dsTotalYield.Tables(0).Rows(0).Item("Yield")
            Else
                decTotalFFB = 0
            End If

            dr = tblTotalAreaFFB.NewRow()
            dr("BlkCode") = strBlkCode
            dr("TotalAreaSize") = decTotalArea
            dr("TotalFFB") = decTotalFFB
            tblTotalAreaFFB.Rows.Add(dr)
        Next

        dsTotalAreaFFB.Tables.Add(tblTotalAreaFFB)

        objRptDs.Tables(0).TableName = "BD_FORMAT"
        objRptDs.Tables.Add(dsTotalAreaFFB.Tables(0).Copy())
        objRptDs.Tables(1).TableName = "BD_TOTALAREAFFB"


        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\BD_StdRpt_UnMatureCrop.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs)

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3

        crvUnMatCrop.Visible = False                         
        crvUnMatCrop.ReportSource = rdCrystalViewer          
        crvUnMatCrop.DataBind()                              

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\BD_StdRpt_UnMatureCrop.pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/BD_StdRpt_UnMatureCrop.pdf"">")
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

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvUnMatCrop.ParameterFieldInfo

        paramField1 = paramFields.Item("ParamCompanyName")
        paramField2 = paramFields.Item("ParamLocation")
        paramField3 = paramFields.Item("ParamUserName")
        paramField4 = paramFields.Item("ParamDecimal")
        paramField5 = paramFields.Item("ParamRptID")
        paramField6 = paramFields.Item("ParamRptName")
        paramField7 = paramFields.Item("ParamPeriod")
        paramField8 = paramFields.Item("ParamlblCrop")
        paramField9 = paramFields.Item("lblBlkCodeTag")
        paramField10 = paramFields.Item("lblSubBlkCodeTag")
        paramField11 = paramFields.Item("ParamBlkCode")
        paramField12 = paramFields.Item("ParamSubBlkCode")
        paramField13 = paramFields.Item("ParamBlkOrSubBlk")
        paramField14 = paramFields.Item("lblAccCodeTag")
        paramField15 = paramFields.Item("ParamLocationTag")

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

        ParamDiscreteValue1.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue2.Value = Session("SS_LOCATION")
        ParamDiscreteValue3.Value = Session("SS_USERNAME")
        ParamDiscreteValue4.Value = Request.QueryString("Decimal")
        ParamDiscreteValue5.Value = Request.QueryString("RptID")
        ParamDiscreteValue6.Value = Request.QueryString("RptName")
        ParamDiscreteValue7.Value = Request.QueryString("DDLPeriodName")
        ParamDiscreteValue8.Value = Request.QueryString("lblCrop")
        ParamDiscreteValue9.Value = Request.QueryString("lblBlkCodeTag")
        ParamDiscreteValue10.Value = Request.QueryString("lblSubBlkCodeTag")
        ParamDiscreteValue11.Value = Request.QueryString("BlkCode")
        ParamDiscreteValue12.Value = Request.QueryString("SubBlkCode")

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
            ParamDiscreteValue13.Value = "block"
        Else
            ParamDiscreteValue13.Value = "subblock"
        End If
        ParamDiscreteValue14.Value = Request.QueryString("lblAccCodeTag")
        ParamDiscreteValue15.Value = UCase(strLocationTag)

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

        crvUnMatCrop.ParameterFieldInfo = paramFields
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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=BD_STDRPT_UNMATURECROP_LANGCAP_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
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
