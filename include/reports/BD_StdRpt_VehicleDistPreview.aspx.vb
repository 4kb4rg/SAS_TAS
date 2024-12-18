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


Public Class BD_StdRpt_VehicleDistPreview : Inherits Page


    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objBD As New agri.BD.clsReport()
    Dim objBDTrx As New agri.BD.clsTrx()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
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
    Dim intConfigsetting As Integer
    Dim strLocationTag As String

    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer
    Dim strParam As String
    Dim strYrPlanted As String
    Dim dr As DataRow

    Dim arrParam As Array

    Dim objLangCapDs As New Object()
    Dim dsYrPlanted As New DataSet()
    Dim objRptDs As New DataSet()
    Dim dsUsgCost As New DataSet()
    Dim dsTotalArea As New DataSet()
    Dim dsTotalFFB As New DataSet()
    Dim dsAreaFFB As New DataSet()
    Dim dsGrandTotalArea As New DataSet()
    Dim dsGrandTotalFFB As New DataSet()

    Dim intRecCount As Integer
    Dim intCntUsgCost As Integer
    Dim intCnt As Integer
    Dim intCntYr As Integer
    Dim intDec As Integer

    Dim strOppCd_VehTypeDist_TotalArea As String
    Dim strLocType As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        crvView.Visible = False

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
            intDec = Request.QueryString("Decimal")
            onload_GetLangCap()
            BindReport()
        End If
    End Sub


    Sub CountYrPlanted()
        Dim strOppCd_YrPlanted_GET As String
        Dim intCntYr As Integer
        Dim strYr As String

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
            strOppCd_YrPlanted_GET = "BD_CLSTRX_VEHTYPEDIST_BLOCK_YEAR_GET"
            strParam = objGLSetup.EnumBlockType.MatureField & "','" & objGLSetup.EnumBlockType.InMatureField & "|" & _
                       objGLSetup.EnumBlockStatus.Active & "|" & strLocation
        Else
            strOppCd_YrPlanted_GET = "BD_CLSTRX_VEHTYPEDIST_SUBBLOCK_YEAR_GET"
            strParam = objGLSetup.EnumSubBlockType.MatureField & "','" & objGLSetup.EnumSubBlockType.InMatureField & "|" & _
                       objGLSetup.EnumSubBlockStatus.Active & "|" & strLocation
        End If

        Try
            intErrNo = objBDTrx.mtdGetVehTypeDistYear(strOppCd_YrPlanted_GET, strParam, dsYrPlanted)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=BD_RPT_VEHTYPEDIST_YEAR_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCntYr = 0 To dsYrPlanted.Tables(0).Rows.Count - 1
            strYr = Trim(dsYrPlanted.Tables(0).Rows(intCntYr).Item("BlkCode"))

            strYrPlanted = strYrPlanted & "%" & strYr

        Next
        strYrPlanted = strYrPlanted.TrimStart("%")
        strYrPlanted = strYrPlanted & "%"

    End Sub

    Protected Sub LoadTotal()
        Dim strOppCd_VehTypeDistParam_GTCost_SUM As String = "BD_CLSTRX_VEHTYPEDISTPARAM_GTCOST_SUM"
        Dim strOppCd_VehTypeDistUsgCost_SUM As String = "BD_CLSTRX_VEHTYPEDIST_USGCOST_SUM"
        Dim dsTotalCost As New DataSet()
        Dim dsGTCost As New DataSet()
        Dim intCntYr As Integer = 0
        Dim strParamCost As String
        Dim strParamGTCost As String
        Dim arrParam As Array

        dr = objRptDs.Tables(0).NewRow()
        dr("VehTypeDistSetID") = 0
        dr("Activity") = "TOTAL"
        dr("VehType") = ""
        dr("Parameter") = 0

        arrParam = Split(strYrPlanted, "%")
        For intCntYr = 0 To arrParam.GetUpperBound(0) - 1

            strParamCost = "||||WHERE BD.BlkCode = '" & arrParam(intCntYr) & "'|"
            Try
                intErrNo = objBDTrx.mtdGetVehTypeDistParam(strOppCd_VehTypeDistUsgCost_SUM, strParamCost, dsTotalCost)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=BD_RPT_VEHTYPEDIST_TOTALCOST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try

            dr(arrParam(intCntYr)) = 0
            dr(arrParam(intCntYr) & "a") = dsTotalCost.Tables(0).Rows(0).Item("Cost")

        Next
        strParamGTCost = "|||||"
        Try
            intErrNo = objBDTrx.mtdGetVehTypeDistParam(strOppCd_VehTypeDistParam_GTCost_SUM, strParamGTCost, dsGTCost)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=BD_TRX_VEHTYPEDISTPARAM_GTCOST_GET&errmesg=" & lblErrMessage.Text & "&redirect=BD/trx/BD_trx_VehTypeDist_Details.aspx")
        End Try

        dr("GrandTotalUsage") = 0
        dr("GrandTotalCost") = dsGTCost.Tables(0).Rows(0).Item("GrandTotalCost")
        dr("Status") = ""
        dr("PeriodID") = 0
        dr("LocCode") = 0
        dr("CreateDate") = "01-Jan-1900"
        dr("UpdateDate") = "01-Jan-1900"
        dr("UserName") = ""
        objRptDs.Tables(0).Rows.InsertAt(dr, objRptDs.Tables(0).Rows.Count + 1)
    End Sub

    Sub BindReport()
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim crExportOptions As ExportOptions

        Dim strOppCd_VehTypeDist_ActVehParam_GET As String = "BD_STDRPT_VEHTYPEDIST_ACTVEHPARAM_GET"
        Dim strOppCd_VehTypeDist_UsageCost_GET As String = "BD_STDRPT_VEHTYPEDIST_USAGECOST_GET"
        Dim strOppCd_YrPlanted_GET As String

        Dim strParamVehDist As String
        Dim objMapPath As String
        Dim intCntArea As Integer = -1
        Dim intCntVeh As Integer = -1

        CountYrPlanted()
        BlockTotalAreaFFB()
        GrandTotalAreaFFB()

        strParamVehDist = intDec & "|AND VP.LocCode = '" & strLocation & "' AND VP.PeriodID = " & Request.QueryString("DDLPeriodID")
        Try
            intErrNo = objBD.mtdGetReport_VehicleDist(strOppCd_VehTypeDist_ActVehParam_GET, strParamVehDist, objRptDs, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=BD_VEHDIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        strParamVehDist = "|AND VTD.LocCode = '" & strLocation & "' AND VTD.PeriodID = " & Request.QueryString("DDLPeriodID")
        Try
            intErrNo = objBD.mtdGetReport_VehicleDist(strOppCd_VehTypeDist_UsageCost_GET, strParamVehDist, dsUsgCost, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=BD_VEHDIST_USGCOST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCntVeh = 0 To objRptDs.Tables(0).Rows.Count - 1
            objRptDs.Tables(0).Rows(intCntVeh).Item("Parameter") = FormatNumber(objRptDs.Tables(0).Rows(intCntVeh).Item("Parameter"), intDec)
            objRptDs.Tables(0).Rows(intCntVeh).BeginEdit()
            objRptDs.Tables(0).Rows(intCntVeh).Item("UsgCost") = "Usage : " & Chr(10) & "Cost : "
            objRptDs.Tables(0).Rows(intCntVeh).EndEdit()
        Next




        While intCntUsgCost <= dsUsgCost.Tables(0).Rows.Count - 1
            While intCntArea <= dsAreaFFB.Tables(0).Rows.Count - 1
                intCntArea += 1
                dsUsgCost.Tables(0).Rows(intCntUsgCost).BeginEdit()
                dsUsgCost.Tables(0).Rows(intCntUsgCost).Item("BlkCode") = dsUsgCost.Tables(0).Rows(intCntUsgCost).Item("BlkCode") & Chr(10) & " Total Area : " & _
                                                                          ObjGlobal.GetIDDecimalSeparator_FreeDigit(dsAreaFFB.Tables(0).Rows(intCntArea).Item("TotalArea"), intdec) & Chr(10) & " Total FFB : " & _
                                                                          ObjGlobal.GetIDDecimalSeparator(dsAreaFFB.Tables(0).Rows(intCntArea).Item("TotalFFB"))
                dsUsgCost.Tables(0).Rows(intCntUsgCost).EndEdit()
                If intCntArea + 1 > dsAreaFFB.Tables(0).Rows.Count - 1 Then
                    intCntArea = -1
                End If
                Exit While
                
            End While
                


            intCntUsgCost += 1
        End While

        objRptDs.Tables(0).TableName = "BD_ACTVEHPARAM"
        objRptDs.Tables.Add(dsUsgCost.Tables(0).Copy())
        objRptDs.Tables(1).TableName = "BD_USGCOST"
        objRptDs.Tables.Add(dsAreaFFB.Tables(0).Copy())
        objRptDs.Tables(2).TableName = "BD_AREAFFB"


        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\BD_StdRpt_VehicleDist.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs)

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3

        crvView.Visible = False                         
        crvView.ReportSource = rdCrystalViewer          
        crvView.DataBind()                              

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\BD_StdRpt_VehicleDist.pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile

            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/BD_StdRpt_VehicleDist.pdf"">")
    End Sub













    Sub BlockTotalAreaFFB()
        Dim strOppCd_VehTypeDist_TotalFFB As String = "BD_CLSTRX_VEHTYPEDIST_TOTALFFB"
        Dim strParamFFB As String

        Dim table As DataTable = New DataTable("table")
        Dim Col1 As DataColumn = New DataColumn()
        Dim Col2 As DataColumn = New DataColumn()
        Dim Col3 As DataColumn = New DataColumn()

        Col1.DataType = System.Type.GetType("System.String")
        Col1.AllowDBNull = True
        Col1.Caption = "BlkCode"
        Col1.ColumnName = "BlkCode"
        Col1.DefaultValue = ""
        table.Columns.Add(Col1)

        Col2.DataType = System.Type.GetType("System.Decimal")
        Col2.AllowDBNull = True
        Col2.Caption = "TotalArea"
        Col2.ColumnName = "TotalArea"
        Col2.DefaultValue = 0
        table.Columns.Add(Col2)

        Col3.DataType = System.Type.GetType("System.Decimal")
        Col3.AllowDBNull = True
        Col3.Caption = "TotalFFB"
        Col3.ColumnName = "TotalFFB"
        Col3.DefaultValue = 0
        table.Columns.Add(Col3)

        arrParam = Split(strYrPlanted, "%")
        For intCntYr = 0 To arrParam.GetUpperBound(0) - 1

            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
                strOppCd_VehTypeDist_TotalArea = "BD_CLSTRX_VEHTYPEDIST_BLKTOTALAREA"
                strParam = objGLSetup.EnumBlockType.MatureField & "','" & objGLSetup.EnumBlockType.InMatureField & "|" & _
                            objGLSetup.EnumBlockStatus.Active & "|" & strLocation & "|" & arrParam(intCntYr) & "|"
            Else
                strOppCd_VehTypeDist_TotalArea = "BD_CLSTRX_VEHTYPEDIST_SUBBLKTOTALAREA"
                strParam = objGLSetup.EnumSubBlockType.MatureField & "','" & objGLSetup.EnumSubBlockType.InMatureField & "|" & _
                            objGLSetup.EnumSubBlockStatus.Active & "|" & strLocation & "|" & arrParam(intCntYr) & "|"
            End If

            Try
                intErrNo = objBDTrx.mtdGetBlockTotalArea(strOppCd_VehTypeDist_TotalArea, strParam, dsTotalArea)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=BD_RPT_GET_BLKTOTALAREA&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try

            strParamFFB = arrParam(intCntYr) & "|AND PeriodID = " & Request.QueryString("DDLPeriodID") & " AND LocCode = '" & strLocation & "' "
            Try
                intErrNo = objBDTrx.mtdGetVehTypeDist_TotalFFB(strOppCd_VehTypeDist_TotalFFB, strParamFFB, dsTotalFFB)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=BD_RPT_GET_BLKTOTALFFB&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try

            dr = table.NewRow()
            dr("BlkCode") = arrParam(intCntYr)
            dr("TotalArea") = FormatNumber(dsTotalArea.Tables(0).Rows(0).Item("TotalArea"), intDec)
            dr("TotalFFB") = FormatNumber(dsTotalFFB.Tables(0).Rows(0).Item("TotalFFB"), intDec)
            table.Rows.Add(dr)

        Next

        dsAreaFFB.Tables.Add(table)

    End Sub

    Sub GrandTotalAreaFFB()
        Dim strOppCd_VehTypeDist_TotalFFB As String = "BD_CLSTRX_VEHTYPEDIST_TOTALFFB"
        Dim strOppCd_VehTypeDist_TotalArea As String
        Dim strParamFFB As String
        Dim strYears As String

        strYears = Replace(strYrPlanted, "%", "','")
        strYears = Left(strYears, Len(strYears) - 3)

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
            strOppCd_VehTypeDist_TotalArea = "BD_CLSTRX_VEHTYPEDIST_BLKTOTALAREA"
            strParam = objGLSetup.EnumBlockType.MatureField & "','" & objGLSetup.EnumBlockType.InMatureField & "|" & _
                       objGLSetup.EnumBlockStatus.Active & "|" & strLocation & "|" & strYears & "|"
        Else
            strOppCd_VehTypeDist_TotalArea = "BD_CLSTRX_VEHTYPEDIST_SUBBLKTOTALAREA"
            strParam = objGLSetup.EnumSubBlockType.MatureField & "','" & objGLSetup.EnumSubBlockType.InMatureField & "|" & _
                       objGLSetup.EnumSubBlockStatus.Active & "|" & strLocation & "|" & strYears & "|"
        End If

        Try
            intErrNo = objBDTrx.mtdGetBlockTotalArea(strOppCd_VehTypeDist_TotalArea, strParam, dsGrandTotalArea)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=BD_RPT_GET_BLKGTOTALAREA&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        strParamFFB = strYears & "|AND PeriodID = " & Request.QueryString("DDLPeriodID") & " AND LocCode = '" & strLocation & "' "
        Try
            intErrNo = objBDTrx.mtdGetVehTypeDist_TotalFFB(strOppCd_VehTypeDist_TotalFFB, strParamFFB, dsGrandTotalFFB)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=BD_RPT_GET_BLKGTOTALFFB&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=BD_STDRPT_VEHICLEDIST_LANGCAP_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
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
