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

Public Class PD_StdRpt_OilPalmYieldStatList_Preview : Inherits Page
    Dim objPD As New agri.PD.clsReport()
    Dim objPDTrx As New agri.PD.clsTrx()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objSysCfg As New agri.PWSystem.clsConfig()

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents EventData As DataGrid

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfigsetting As Integer
    Dim strUserLoc As String
    Dim strBlkType As String
    Dim strAccMth As String
    Dim strAccYr As String
    Dim strDDLAccMth As String
    Dim strDDLAccYr As String
    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer
    Dim intDecimal As Integer


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_PDACCMONTH")
        strAccYear = Session("SS_PDACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            crvView.Visible = False

            If Right(Request.QueryString("Location"), 1) = "," Then
                strUserLoc = Left(Request.QueryString("Location"), Len(Request.QueryString("Location")) - 1)
            Else
                strUserLoc = Trim(Request.QueryString("Location"))
            End If

            strDDLAccMth = Request.QueryString("DDLAccMth")
            strDDLAccYr = Request.QueryString("DDLAccYr")
            strBlkType = Request.QueryString("BlkType")
            intDecimal = Request.QueryString("Decimal")
            Session("SS_lblBlkType") = Request.QueryString("lblBlkType")
            Session("SS_lblBlkGrp") = Request.QueryString("lblBlkGrp")
            Session("SS_lblBlkCode") = Request.QueryString("lblBlkCode")
            Session("SS_lblSubBlkCode") = Request.QueryString("lblSubBlkCode")
            Session("SS_lblLocation") = Request.QueryString("lblLocation")
            Session("SS_RptID") = Request.QueryString("RptID")
            Session("SS_RptName") = Request.QueryString("RptName")
            Session("SS_AccMth") = Request.QueryString("DDLAccMth")
            Session("SS_AccYr") = Request.QueryString("DDLAccYr")
            Session("SS_DocNoFrom") = Request.QueryString("DocNoFrom")
            Session("SS_DocNoTo") = Request.QueryString("DocNoTo")
            Session("SS_BlkType") = Request.QueryString("BlkType")
            Session("SS_BlkGrp") = Request.QueryString("BlkGrp")
            Session("SS_BlkCode") = Request.QueryString("BlkCode")
            Session("SS_SubBlkCode") = Request.QueryString("SubBlkCode")
            Session("SS_Decimal") = Request.QueryString("Decimal")
            Session("SS_Supp") = Request.QueryString("Supp")

            BindReport()
        End If
    End Sub


    Sub BindReport()
        Dim objMapPath As String
        Dim objRptDs As New DataSet()
        Dim objRptDsMTDAll As New DataSet()
        Dim objRptDsMTDYieldPerHarvMDay As New DataSet()
        Dim objRptDsYTDAll As New DataSet()
        Dim objRptDsYTDYieldPerHarvMDay As New DataSet()
        Dim intCnt As Integer
        Dim objDsYTDAccPeriod As String
        Dim intCntBlk As Integer
        Dim intCntMTDAll As Integer
        Dim intCntMTDYPHM As Integer
        Dim intCntYTDAll As Integer
        Dim intCntYTDYPHM As Integer
        Dim SearchStr As String
        Dim tempLoc As String
        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim strOpCd_OilPalmYieldStat_BlkGrp_GET As String = "PD_STDRPT_OILPALMYIELDSTAT_BLKGRP_GET"
        Dim strOpCd_OilPalmYieldStat_BlkGrp_All_GET As String = "PD_STDRPT_OILPALMYIELDSTAT_BLKGRP_ALL_EXC_YIEPERHARMDAY_GET"
        Dim strOpCd_OilPalmYieldStat_BlkGrp_YieldPerHarvestManDay_GET As String = "PD_STDRPT_OILPALMYIELDSTAT_BLKGRP_YIELDPERHARVESTMANDAY_GET"
        Dim strOpCd_OilPalmYieldStat_Blk_GET As String = "PD_STDRPT_OILPALMYIELDSTAT_BLOCK_GET"
        Dim strOpCd_OilPalmYieldStat_Blk_All_GET As String = "PD_STDRPT_OILPALMYIELDSTAT_BLK_ALL_EXC_YIEPERHARMDAY_GET"
        Dim strOpCd_OilPalmYieldStat_Blk_YieldPerHarvestManDay_GET As String = "PD_STDRPT_OILPALMYIELDSTAT_BLK_YIELDPERHARVESTMANDAY_GET"
        Dim strOpCd_OilPalmYieldStat_SubBlk_GET As String = "PD_STDRPT_OILPALMYIELDSTAT_SUBBLOCK_GET"
        Dim strOpCd_OilPalmYieldStat_SubBlk_All_GET As String = "PD_STDRPT_OILPALMYIELDSTAT_SBLK_ALL_EXC_YIEPERHARMDAY_GET"
        Dim strOpCd_OilPalmYieldStat_SubBlk_YieldPerHarvestManDay_GET As String = "PD_STDRPT_OILPALMYIELDSTAT_SBLK_YIEPERHARMANDAY_GET"
        Dim strParam As String
        Dim strParamMTDAll As String
        Dim strParamYTDAll As String
        Dim strParamMTDYieldPerHarvMDay As String
        Dim strParamYTDYieldPerHarvMDay As String
        Dim strOpCd As String
        Dim strOpCdMTDYTDFig As String
        Dim strOpCdMTDYTDHarvestManDay As String
        Dim strLocCode As String
        Dim strBlkCode As String
        Dim strEmpCode As String
        Dim NormStr As String = "AND EY.LocCode IN ('" & strUserLoc & "') AND EY.AccMonth = '" & strDDLAccMth & "' AND EY.AccYear = '" & strDDLAccYr & "' "

        tempLoc = Request.QueryString("Location")
        If Right(tempLoc, 1) = "," Then
            Session("SS_LOC") = Left(tempLoc, Len(tempLoc) - 1)
        Else
            Session("SS_LOC") = tempLoc.Replace("','", ", ")
        End If

        If (Len(Trim(Request.QueryString("DocNoFrom"))) > 0) And (Len(Trim(Request.QueryString("DocNoTo"))) > 0) Then
            NormStr += "AND EY.EstateYieldID Between '" & Request.QueryString("DocNoFrom") & "' AND '" & Request.QueryString("DocNoTo") & "' "
        ElseIf (Len(Trim(Request.QueryString("DocNoFrom"))) > 0) Then
            NormStr += "AND EY.EstateYieldID LIKE '" & Request.QueryString("DocNoFrom") & "' "
        End If

        NormStr += "AND EY.Status IN ('" & objPDTrx.EnumPOMYieldStatus.Active & "','" & objPDTrx.EnumPOMYieldStatus.Closed & "') "

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockYieldLevel), intConfigSetting) = True Then
            If strBlkType = "BlkCode" Then
                If Not Request.QueryString("BlkCode") = "" Then
                    SearchStr = "AND EY.BlkCode LIKE '" & Request.QueryString("BlkCode") & "' "
                End If
                Session("SS_BLKHEADER") = "Block"
                strOpCd = strOpCd_OilPalmYieldStat_Blk_GET
                strOpCdMTDYTDFig = strOpCd_OilPalmYieldStat_Blk_All_GET
                strOpCdMTDYTDHarvestManDay = strOpCd_OilPalmYieldStat_Blk_YieldPerHarvestManDay_GET            
            Else 'strBlkType = "BlkGrp"
                If Not Request.QueryString("BlkGrp") = "" Then
                    SearchStr = "AND BG.BlkGrpCode LIKE '" & Request.QueryString("BlkGrp") & "' "
                End If
                Session("SS_BLKGRPHEADER") = "BlkGrp"
                strOpCd = strOpCd_OilPalmYieldStat_BlkGrp_GET
                strOpCdMTDYTDFig = strOpCd_OilPalmYieldStat_BlkGrp_All_GET
                strOpCdMTDYTDHarvestManDay = strOpCd_OilPalmYieldStat_BlkGrp_YieldPerHarvestManDay_GET
            End If
        Else
            If strBlkType = "SubBlkCode" Then
                If Not Request.QueryString("SubBlkCode") = "" Then
                    SearchStr = "AND EY.BlkCode LIKE '" & Request.QueryString("SubBlkCode") & "' "
                End If
                Session("SS_SUBBLKHEADER") = "SubBlock"
                strOpCd = strOpCd_OilPalmYieldStat_SubBlk_GET
                strOpCdMTDYTDFig = strOpCd_OilPalmYieldStat_SubBlk_All_GET
                strOpCdMTDYTDHarvestManDay = strOpCd_OilPalmYieldStat_SubBlk_YieldPerHarvestManDay_GET                
            Else 'strBlkType = "BlkCode"
                If Not Request.QueryString("BlkCode") = "" Then
                    SearchStr = "AND BLK.BlkCode LIKE '" & Request.QueryString("BlkCode") & "' "
                End If
                Session("SS_BLKHEADER") = "Block"
                strOpCd = strOpCd_OilPalmYieldStat_SubBlk_GET
                strOpCdMTDYTDFig = strOpCd_OilPalmYieldStat_SubBlk_All_GET
                strOpCdMTDYTDHarvestManDay = strOpCd_OilPalmYieldStat_SubBlk_YieldPerHarvestManDay_GET                
            End If
        End If


        strParam = NormStr & SearchStr

        Try
            intErrNo = objSysCfg.mtdGetAccPeriodCol(strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strDDLAccMth, _
                                                    strDDLAccYr, _
                                                    objSysCfg.EnumAccountPeriodType.YTD, _
                                                    "EY.AccMonth", _
                                                    "EY.AccYear", _
                                                    objDsYTDAccPeriod)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_OILPALMYIELD_ACCPERIOD_LIST&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        Try
            intErrNo = objPD.mtdGetReport_OilPalmYieldStatList(strOpCd, strParam, objRptDs, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_OILPALMYIELDSTAT_LIST_REPORT&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCntBlk = 0 To objRptDs.Tables(0).Rows.Count - 1
            strLocCode = objRptDs.Tables(0).Rows(intCntBlk).Item("LocCode")
            strBlkCode = objRptDs.Tables(0).Rows(intCntBlk).Item("BlkCode")
            strEmpCode = objRptDs.Tables(0).Rows(intCntBlk).Item("EmpCode")

            strParamMTDAll = strBlkCode & "|" & _
                             strEmpCode & "|" & _
                             objPDTrx.EnumPOMYieldStatus.Active & "','" & objPDTrx.EnumPOMYieldStatus.Closed & "|" & _
                             "(EY.AccMonth = '" & strDDLAccMth & "' AND EY.AccYear = '" & strDDLAccYr & "')" & "|" & _
                             "AND EY.LocCode = '" & strLocCode & "' "
            

            Try
                intErrNo = objPD.mtdGetOilPalmYieldStatMthToDate(strOpCdMTDYTDFig, strParamMTDAll, objRptDsMTDAll)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_OILPALMYIELDSTAT_MTH_ALL_EXCEPT_YIEPERHARMDAY_LIST&errmesg=" & Exp.ToString() & "&redirect=")
            End Try

            For intCntMTDAll = 0 To objRptDsMTDAll.Tables(0).Rows.Count - 1
                objRptDs.Tables(0).Rows(intCntBlk).Item("MthRoundNo") = FormatNumber(objRptDsMTDAll.Tables(0).Rows(intCntMTDAll).Item("RoundNo"), intDecimal)
                objRptDs.Tables(0).Rows(intCntBlk).Item("MthHarvestManDay") = FormatNumber(CDbl(objRptDsMTDAll.Tables(0).Rows(intCntMTDAll).Item("HarvestManDay")), intDecimal)
                objRptDs.Tables(0).Rows(intCntBlk).Item("ActualMthTotalFFB") = FormatNumber(objRptDsMTDAll.Tables(0).Rows(intCntMTDAll).Item("ActualTotalFFB"), intDecimal)
                objRptDs.Tables(0).Rows(intCntBlk).Item("MthBunchNo") = FormatNumber(objRptDsMTDAll.Tables(0).Rows(intCntMTDAll).Item("BunchNo"), intDecimal)
            Next

            strParamYTDAll = strBlkCode & "|" & _
                             strEmpCode & "|" & _
                             objPDTrx.EnumPOMYieldStatus.Active & "','" & objPDTrx.EnumPOMYieldStatus.Closed & "|" & _
                             objDsYTDAccPeriod & "|" & _
                             "AND EY.LocCode = '" & strLocCode & "' "
            Try
                intErrNo = objPD.mtdGetOilPalmYieldStatMthToDate(strOpCdMTDYTDFig, strParamYTDAll, objRptDsYTDAll)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_OILPALMYIELDSTAT_YTD_ALL_EXCEPT_YIEPERHARMDAY_LIST&errmesg=" & Exp.ToString() & "&redirect=")
            End Try

            For intCntYTDAll = 0 To objRptDsYTDAll.Tables(0).Rows.Count - 1
                objRptDs.Tables(0).Rows(intCntBlk).Item("ToDateRoundNo") = FormatNumber(objRptDsYTDAll.Tables(0).Rows(intCntYTDAll).Item("RoundNo"), intDecimal)
                objRptDs.Tables(0).Rows(intCntBlk).Item("ToDateHarvestManDay") = FormatNumber(CDbl(objRptDsYTDAll.Tables(0).Rows(intCntYTDAll).Item("HarvestManDay")), intDecimal)
                objRptDs.Tables(0).Rows(intCntBlk).Item("ActualToDateTotalFFB") = FormatNumber(objRptDsYTDAll.Tables(0).Rows(intCntYTDAll).Item("ActualTotalFFB"), intDecimal)
                objRptDs.Tables(0).Rows(intCntBlk).Item("ToDateBunchNo") = FormatNumber(objRptDsYTDAll.Tables(0).Rows(intCntYTDAll).Item("BunchNo"), intDecimal)
            Next




        Next intCntBlk


        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\PD_StdRpt_OilPalmYieldStatList.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3

        crvView.Visible = False                          
        crvView.ReportSource = rdCrystalViewer           
        crvView.DataBind()                              

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\PD_StdRpt_OilPalmYieldStatList.pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
	rdCrystalViewer.Close()
        rdCrystalViewer.Dispose()

        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/PD_StdRpt_OilPalmYieldStatList.pdf"">")

        objRptDs = Nothing
        objRptDsMTDAll = Nothing
        objRptDsMTDYieldPerHarvMDay = Nothing
        objRptDsYTDAll = Nothing
        objRptDsYTDYieldPerHarvMDay = Nothing
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

        ParamDiscreteValue1.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue2.Value = Session("SS_LOC")
        ParamDiscreteValue3.Value = Session("SS_USERNAME")
        ParamDiscreteValue4.Value = Session("SS_AccMth")
        ParamDiscreteValue5.Value = Session("SS_AccYr")
        ParamDiscreteValue6.Value = Session("SS_Decimal")
        ParamDiscreteValue7.Value = Session("SS_BlkGrp")
        ParamDiscreteValue8.Value = Session("SS_BlkCode")
        ParamDiscreteValue9.Value = Session("SS_SubBlkCode")

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockYieldLevel), intConfigSetting) = True Then            
            ParamDiscreteValue21.Value = "Block"
            If strBlkType = "BlkGrp" Then
                ParamDiscreteValue10.Value = Session("SS_BLKGRPHEADER")
            Else
                ParamDiscreteValue10.Value = Session("SS_BLKHEADER")
            End If
        Else
            ParamDiscreteValue21.Value = "SubBlock"
            If strBlkType = "SubBlkCode" Then
                ParamDiscreteValue10.Value = Session("SS_SUBBLKHEADER")
            Else 
               ParamDiscreteValue10.Value = Session("SS_BLKHEADER")
            End If
        End If 

        ParamDiscreteValue11.Value = Session("SS_SUPP")
        ParamDiscreteValue12.Value = Session("SS_RPTID")
        ParamDiscreteValue13.Value = Session("SS_RPTNAME")
        ParamDiscreteValue14.Value = Session("SS_LBLLOCATION")
        ParamDiscreteValue15.Value = Session("SS_BLKTYPE")
        ParamDiscreteValue16.Value = Session("SS_LBLBLKGRP")
        ParamDiscreteValue17.Value = Session("SS_LBLBLKCODE")
        ParamDiscreteValue18.Value = Session("SS_LBLSUBBLKCODE")
        ParamDiscreteValue19.Value = Session("SS_DocNoFrom")
        ParamDiscreteValue20.Value = Session("SS_DocNoTo")

        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        ParamFieldDef1 = ParamFieldDefs.Item("ParamCompanyName")
        ParamFieldDef2 = ParamFieldDefs.Item("ParamLocation")
        ParamFieldDef3 = ParamFieldDefs.Item("ParamUserName")
        ParamFieldDef4 = ParamFieldDefs.Item("ParamAccMonth")
        ParamFieldDef5 = ParamFieldDefs.Item("ParamAccYear")
        ParamFieldDef6 = ParamFieldDefs.Item("ParamDecimal")
        ParamFieldDef7 = ParamFieldDefs.Item("ParamBlkGrp")
        ParamFieldDef8 = ParamFieldDefs.Item("ParamBlkCode")
        ParamFieldDef9 = ParamFieldDefs.Item("ParamSubBlkCode")
        ParamFieldDef10 = ParamFieldDefs.Item("ParamBlkOrSubBlk")
        ParamFieldDef11 = ParamFieldDefs.Item("ParamSuppress")
        ParamFieldDef12 = ParamFieldDefs.Item("ParamRptID")
        ParamFieldDef13 = ParamFieldDefs.Item("ParamRptName")
        ParamFieldDef14 = ParamFieldDefs.Item("lblLocation")
        ParamFieldDef15 = ParamFieldDefs.Item("ParamBlkType")
        ParamFieldDef16 = ParamFieldDefs.Item("lblBlkGrp")
        ParamFieldDef17 = ParamFieldDefs.Item("lblBlkCode")
        ParamFieldDef18 = ParamFieldDefs.Item("lblSubBlkCode")
        ParamFieldDef19 = ParamFieldDefs.Item("ParamDocNoFrom")
        ParamFieldDef20 = ParamFieldDefs.Item("ParamDocNoTo")
        ParamFieldDef21 = ParamFieldDefs.Item("ParamBlkOrSubBlkShown") 


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
        ParameterValues19 = ParamFieldDef18.CurrentValues
        ParameterValues20 = ParamFieldDef18.CurrentValues
        ParameterValues21 = ParamFieldDef21.CurrentValues 

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

    End Sub
End Class
