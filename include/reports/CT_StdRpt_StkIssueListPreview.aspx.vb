Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information
Imports System.Xml
Imports System.Web.Services
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web

Public Class CT_StdRpt_StkIssueList_Preview : Inherits Page
    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objCT As New agri.CT.clsReport()
    Dim objCTSetup As New agri.CT.clsSetup()
    Dim objCTTrx As New agri.CT.clsTrx()

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

    Dim intDecimal As Integer
    Dim strBlkType As String
    Dim tempLoc As String
    Dim strDDLAccMth As String
    Dim strDDLAccYr As String

    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        crvView.Visible = False

        If Right(Request.QueryString("Location"), 1) = "," Then
            strUserLoc = Left(Request.QueryString("Location"), Len(Request.QueryString("Location")) - 1)
        Else
            strUserLoc = Trim(Request.QueryString("Location"))
        End If

        tempLoc = Request.QueryString("Location")
        If Right(tempLoc, 1) = "," Then
            Session("SS_LOC") = Left(tempLoc, Len(tempLoc) - 1)
        Else
            Session("SS_LOC") = tempLoc.Replace("','", ", ")
        End If

        strBlkType = Request.QueryString("BlkType")
        intDecimal = Request.QueryString("Decimal")
        strDDLAccMth = Request.QueryString("DDLAccMth")
        strDDLAccYr = Request.QueryString("DDLAccYr")

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            BindReport()
        End If
    End Sub

    Sub BindReport()
        Dim objRptDs As New DataSet()
        Dim objMapPath As String
        Dim objItem As New DataSet()
        Dim intCnt As Integer
        Dim intCntRem As Integer
        Dim SearchStr As String
        Dim itemSelectStr As String
        Dim vehSelectStr As String
        Dim blkSelectStr As String
        Dim itemSearchStr As String
        Dim vehSearchStr As String
        Dim blkSearchStr As String
        Dim strOpCdStkIssue As String
        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim strOpCdStkIssue_BlkGrp_GET As String = "CT_STDRPT_STKISSUE_AND_STKISSUELN_BLKGRP_GET"
        Dim strOpCdStkIssue_Blk_GET As String = "CT_STDRPT_STKISSUE_AND_STKISSUELN_BLK_GET"
        Dim strOpCdStkIssue_SubBlk_GET As String = "CT_STDRPT_STKISSUE_AND_STKISSUELN_SUBBLK_GET"
        Dim strOpCdItem_GET As String = "CT_STDRPT_ITEM_GET"
        Dim strParam As String
        Dim strParamItm As String
        Dim strItemCode As String

        If strBlkType = "BlkGrp" Then
            If Not Request.QueryString("BlkGrp") = "" Then
                blkSelectStr = "AND STKISSLN.BlkCode IN (SELECT BlkCode FROM GL_BLKGRP WHERE BlkGrpCode LIKE '" & Request.QueryString("BlkGrp") & "%') "
            End If
            Session("SS_BLKGRPHEADER") = "BlkGrp"
            strOpCdStkIssue = strOpCdStkIssue_BlkGrp_GET

        ElseIf strBlkType = "BlkCode" Then
            If Not Request.QueryString("BlkCode") = "" Then
                blkSelectStr = blkSelectStr & "AND STKISSLN.BlkCode LIKE '" & Request.QueryString("BlkCode") & "' "
            End If
            Session("SS_BLKHEADER") = "BlkCode"
            strOpCdStkIssue = strOpCdStkIssue_Blk_GET

        ElseIf strBlkType = "SubBlkCode" Then
            If Not Request.QueryString("SubBlkCode") = "" Then
                blkSelectStr = blkSelectStr & "AND STKISSLN.BlkCode LIKE '" & Request.QueryString("SubBlkCode") & "' "
            End If
            Session("SS_SUBBLKHEADER") = "SubBlkCode"
            strOpCdStkIssue = strOpCdStkIssue_SubBlk_GET
        End If

        If Not (Request.QueryString("DateFrom") = "" And Request.QueryString("DateTo") = "") Then
            SearchStr = SearchStr & "AND (DateDiff(Day, '" & Request.QueryString("DateFrom") & "', STKISS.CreateDate) >= 0) And (DateDiff(Day, '" & Request.QueryString("DateTo") & "', STKISS.CreateDate) <= 0) "
        End If

        If Not (Request.QueryString("StkIssueIDFrom") = "" And Request.QueryString("StkIssueIDTo") = "") Then
            SearchStr = SearchStr & "AND STKISS.StockIssueID >= '" & Request.QueryString("StkIssueIDFrom") & _
                        "' AND STKISS.StockIssueID <= '" & Request.QueryString("StkIssueIDTo") & "' "
        End If

        If Not Request.QueryString("StkIssueType") = "" Then
            SearchStr = SearchStr & "AND STKISS.IssueType = '" & Request.QueryString("StkIssueType") & "' "
        End If

        If Not Request.QueryString("ProdType") = "" Then
            itemSelectStr = "AND ITM.ProdTypeCode LIKE '" & Request.QueryString("ProdType") & "' "
        End If

        If Not Request.QueryString("ProdBrand") = "" Then
            itemSelectStr = itemSelectStr & "AND ITM.ProdBrandCode LIKE '" & Request.QueryString("ProdBrand") & "' "
        End If

        If Not Request.QueryString("ProdModel") = "" Then
            itemSelectStr = itemSelectStr & "AND ITM.ProdModelCode LIKE '" & Request.QueryString("ProdModel") & "' "
        End If

        If Not Request.QueryString("ProdCat") = "" Then
            itemSelectStr = itemSelectStr & "AND ITM.ProdCatCode LIKE '" & Request.QueryString("ProdCat") & "' "
        End If

        If Not Request.QueryString("ProdMat") = "" Then
            itemSelectStr = itemSelectStr & "AND ITM.ProdMatCode LIKE '" & Request.QueryString("ProdMat") & "' "
        End If

        If Not Request.QueryString("StkAna") = "" Then
            itemSelectStr = itemSelectStr & "AND ITM.StockAnalysisCode LIKE '" & Request.QueryString("StkAna") & "' "
        End If

        If Not Request.QueryString("ItemCode") = "" Then
            itemSelectStr = itemSelectStr & "AND ITM.ItemCode LIKE '" & Request.QueryString("ItemCode") & "' "
        End If

        If Not Request.QueryString("AccCode") = "" Then
            SearchStr = SearchStr & "AND STKISSLN.AccCode LIKE '" & Request.QueryString("AccCode") & "' "
        End If

        If Not Request.QueryString("VehCode") = "" Then
            vehSelectStr = "AND VEH.VehCode LIKE '" & Request.QueryString("VehCode") & "' "
        End If

        If Not Request.QueryString("VehTypeCode") = "" Then
            vehSelectStr = vehSelectStr & "AND VEH.VehTypeCode LIKE '" & Request.QueryString("VehTypeCode") & "' "
        End If

        If Not Request.QueryString("VehExpCode") = "" Then
            SearchStr = SearchStr & "AND STKISSLN.VehExpCode LIKE '" & Request.QueryString("VehExpCode") & "' "
        End If

        If Not Request.QueryString("Status") = "" Then
            If Not Request.QueryString("Status") = objCTTrx.EnumStockIssueStatus.All Then
                SearchStr = SearchStr & "AND STKISS.Status = '" & Request.QueryString("Status") & "' "
            Else
                SearchStr = SearchStr & "AND STKISS.Status NOT LIKE ''"
            End If
        End If

        If Not SearchStr = "" Or Not itemSelectStr = "" Or Not vehSelectStr = "" Or Not blkSelectStr = "" Then
            If Not Request.QueryString("AccCode") = "" Or Not Request.QueryString("VehTypeCode") = "" Or Not Request.QueryString("VehCode") = "" Or Not Request.QueryString("VehExpCode") = "" Or Not _
                   Request.QueryString("BlkGrp") = "" Or Not Request.QueryString("BlkCode") = "" Or Not Request.QueryString("SubBlkCode") = "" Or Not Request.QueryString("ItemCode") = "" Then
                strParam = strUserLoc & "|" & strDDLAccMth & "|" & strDDLAccYr & "|" & vehSelectStr & "|" & vehSearchStr & "|" & blkSelectStr & "|" & blkSearchStr & "|" & itemSelectStr & "AND ITM.ItemType = '" & objCTSetup.EnumInventoryItemType.CanteenItem & "'|" & itemSearchStr & "|" & SearchStr
            Else
                strParam = strUserLoc & "|" & strDDLAccMth & "|" & strDDLAccYr & "|" & vehSelectStr & "|" & vehSearchStr & "|" & blkSelectStr & "|" & blkSearchStr & "|" & itemSelectStr & "AND ITM.ItemType = '" & objCTSetup.EnumInventoryItemType.CanteenItem & "'|" & itemSearchStr & "|" & SearchStr
            End If
        End If

        Try
            intErrNo = objCT.mtdGetReport_StockIssueList(strOpCdStkIssue, strParam, objRptDs, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_STOCKISSUE_LIST_REPORT&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try




        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\CT_StdRpt_StkIssueList.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3

        crvView.Visible = False                         
        crvView.ReportSource = rdCrystalViewer          
        crvView.DataBind()                              

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\CT_StdRpt_StkIssueList.pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/CT_StdRpt_StkIssueList.pdf"">")
    End Sub

    Sub PassParam()
        Dim strIssueTypeText As String
        Dim strStatusText As String

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
        Dim paramField34 As New ParameterField()
        Dim paramField35 As New ParameterField()
        Dim paramField36 As New ParameterField()
        Dim paramField37 As New ParameterField()
        Dim paramField38 As New ParameterField()
        Dim paramField39 As New ParameterField()
        Dim paramField40 As New ParameterField()
        Dim paramField41 As New ParameterField()
        Dim paramField42 As New ParameterField()
        Dim paramField43 As New ParameterField()
        Dim paramField44 As New ParameterField()
        Dim paramField45 As New ParameterField()

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
        Dim ParamDiscreteValue34 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue35 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue36 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue37 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue38 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue39 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue40 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue41 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue42 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue43 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue44 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue45 As New ParameterDiscreteValue()

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
        Dim crParameterValues34 As ParameterValues
        Dim crParameterValues35 As ParameterValues
        Dim crParameterValues36 As ParameterValues
        Dim crParameterValues37 As ParameterValues
        Dim crParameterValues38 As ParameterValues
        Dim crParameterValues39 As ParameterValues
        Dim crParameterValues40 As ParameterValues
        Dim crParameterValues41 As ParameterValues
        Dim crParameterValues42 As ParameterValues
        Dim crParameterValues43 As ParameterValues
        Dim crParameterValues44 As ParameterValues
        Dim crParameterValues45 As ParameterValues

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        If trim(Request.QueryString("StkIssueType")) = "" Then
            strIssueTypeText = "All"
        Else
            strIssueTypeText = objCTTrx.mtdGetStockIssueType(Trim(Request.QueryString("StkIssueType")))
        End If 
        strStatusText = objCTTrx.mtdGetStockIssueStatus(Trim(Request.QueryString("Status")))

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("ParamLocation")
        paramField2 = paramFields.Item("ParamDateFrom")
        paramField3 = paramFields.Item("ParamDateTo")
        paramField4 = paramFields.Item("ParamStkIssueIDFrom")
        paramField5 = paramFields.Item("ParamStkIssueIDTo")
        paramField6 = paramFields.Item("ParamStkIssueType")
        paramField7 = paramFields.Item("ParamAccCode")
        paramField8 = paramFields.Item("ParamVehCode")
        paramField9 = paramFields.Item("ParamVehExpCode")
        paramField10 = paramFields.Item("ParamBlkCode")
        paramField11 = paramFields.Item("ParamSubBlkCode")
        paramField12 = paramFields.Item("ParamStatus")
        paramField13 = paramFields.Item("ParamBlkOrSubBlk")
        paramField14 = paramFields.Item("ParamCompanyName")
        paramField15 = paramFields.Item("ParamUserName")
        paramField16 = paramFields.Item("ParamDecimal")
        paramField17 = paramFields.Item("ParamRptID")
        paramField18 = paramFields.Item("ParamRptName")
        paramField19 = paramFields.Item("ParamAccMonth")
        paramField20 = paramFields.Item("ParamAccYear")
        paramField21 = paramFields.Item("lblAccCode")
        paramField22 = paramFields.Item("lblVehCode")
        paramField23 = paramFields.Item("lblVehExpCode")
        paramField24 = paramFields.Item("lblBlkCode")
        paramField25 = paramFields.Item("lblSubBlkCode")
        paramField26 = paramFields.Item("ParamItemCode")
        paramField27 = paramFields.Item("lblLocation")
        paramField28 = paramFields.Item("ParamBlkType")
        paramField29 = paramFields.Item("ParamBlkGrp")
        paramField30 = paramFields.Item("lblBlkGrp")
        paramField31 = paramFields.Item("ParamProdType")
        paramField32 = paramFields.Item("ParamProdBrand")
        paramField33 = paramFields.Item("ParamProdModel")
        paramField34 = paramFields.Item("ParamProdCat")
        paramField35 = paramFields.Item("ParamProdMat")
        paramField36 = paramFields.Item("ParamStkAna")
        paramField37 = paramFields.Item("lblProdTypeCode")
        paramField38 = paramFields.Item("lblProdBrandCode")
        paramField39 = paramFields.Item("lblProdModelCode")
        paramField40 = paramFields.Item("lblProdCatCode")
        paramField41 = paramFields.Item("lblProdMatCode")
        paramField42 = paramFields.Item("lblStkAnaCode")
        paramField43 = paramFields.Item("lblVehTypeCode")
        paramField44 = paramFields.Item("ParamVehTypeCode")
        paramField45 = paramFields.Item("lblBillPartyCode")

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
        crParameterValues34 = paramField34.CurrentValues
        crParameterValues35 = paramField35.CurrentValues
        crParameterValues36 = paramField36.CurrentValues
        crParameterValues37 = paramField37.CurrentValues
        crParameterValues38 = paramField38.CurrentValues
        crParameterValues39 = paramField39.CurrentValues
        crParameterValues40 = paramField40.CurrentValues
        crParameterValues41 = paramField41.CurrentValues
        crParameterValues42 = paramField42.CurrentValues
        crParameterValues43 = paramField43.CurrentValues
        crParameterValues44 = paramField44.CurrentValues
        crParameterValues45 = paramField45.CurrentValues

        ParamDiscreteValue1.Value = Session("SS_LOC")
        ParamDiscreteValue2.Value = Request.QueryString("DateFrom")
        ParamDiscreteValue3.Value = Request.QueryString("DateTo")
        ParamDiscreteValue4.Value = Request.QueryString("StkIssueIDFrom")
        ParamDiscreteValue5.Value = Request.QueryString("StkIssueIDTo")
        ParamDiscreteValue6.Value = strIssueTypeText
        ParamDiscreteValue7.Value = Request.QueryString("AccCode")
        ParamDiscreteValue8.Value = Request.QueryString("VehCode")
        ParamDiscreteValue9.Value = Request.QueryString("VehExpCode")
        ParamDiscreteValue10.Value = Request.QueryString("BlkCode")
        ParamDiscreteValue11.Value = Request.QueryString("SubBlkCode")
        ParamDiscreteValue12.Value = strStatusText

        If strBlkType = "BlkGrp" Then
            ParamDiscreteValue13.Value = Session("SS_BLKGRPHEADER")
        ElseIf strBlkType = "BlkCode" Then
            ParamDiscreteValue13.Value = Session("SS_BLKHEADER")
        ElseIf strBlkType = "SubBlkCode" Then
            ParamDiscreteValue13.Value = Session("SS_SUBBLKHEADER")
        End If

        ParamDiscreteValue14.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue15.Value = Session("SS_USERNAME")
        ParamDiscreteValue16.Value = Request.QueryString("Decimal")
        ParamDiscreteValue17.Value = Request.QueryString("RptID")
        ParamDiscreteValue18.Value = Request.QueryString("RptName")
        ParamDiscreteValue19.Value = Request.QueryString("DDLAccMth")
        ParamDiscreteValue20.Value = Request.QueryString("DDLAccYr")
        ParamDiscreteValue21.Value = Request.QueryString("lblAccCode")
        ParamDiscreteValue22.Value = Request.QueryString("lblVehCode")
        ParamDiscreteValue23.Value = Request.QueryString("lblVehExpCode")
        ParamDiscreteValue24.Value = Request.QueryString("lblBlkCode")
        ParamDiscreteValue25.Value = Request.QueryString("lblSubBlkCode")
        ParamDiscreteValue26.Value = Request.QueryString("ItemCode")
        ParamDiscreteValue27.Value = Request.QueryString("lblLocation")
        ParamDiscreteValue28.Value = Request.QueryString("BlkType")
        ParamDiscreteValue29.Value = Request.QueryString("BlkGrp")
        ParamDiscreteValue30.Value = Request.QueryString("lblBlkGrp")
        ParamDiscreteValue31.Value = Request.QueryString("ProdType")
        ParamDiscreteValue32.Value = Request.QueryString("ProdBrand")
        ParamDiscreteValue33.Value = Request.QueryString("ProdModel")
        ParamDiscreteValue34.Value = Request.QueryString("ProdCat")
        ParamDiscreteValue35.Value = Request.QueryString("ProdMat")
        ParamDiscreteValue36.Value = Request.QueryString("StkAna")
        ParamDiscreteValue37.Value = Request.QueryString("lblProdTypeCode")
        ParamDiscreteValue38.Value = Request.QueryString("lblProdBrandCode")
        ParamDiscreteValue39.Value = Request.QueryString("lblProdModelCode")
        ParamDiscreteValue40.Value = Request.QueryString("lblProdCatCode")
        ParamDiscreteValue41.Value = Request.QueryString("lblProdMatCode")
        ParamDiscreteValue42.Value = Request.QueryString("lblStkAnaCode")
        ParamDiscreteValue43.Value = Request.QueryString("lblVehTypeCode")
        ParamDiscreteValue44.Value = Request.QueryString("VehTypeCode")
        ParamDiscreteValue45.Value = Request.QueryString("lblBillPartyCode")

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
        crParameterValues34.Add(ParamDiscreteValue34)
        crParameterValues35.Add(ParamDiscreteValue35)
        crParameterValues36.Add(ParamDiscreteValue36)
        crParameterValues37.Add(ParamDiscreteValue37)
        crParameterValues38.Add(ParamDiscreteValue38)
        crParameterValues39.Add(ParamDiscreteValue39)
        crParameterValues40.Add(ParamDiscreteValue40)
        crParameterValues41.Add(ParamDiscreteValue41)
        crParameterValues42.Add(ParamDiscreteValue42)
        crParameterValues43.Add(ParamDiscreteValue43)
        crParameterValues44.Add(ParamDiscreteValue44)
        crParameterValues45.Add(ParamDiscreteValue45)

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
        PFDefs(33).ApplyCurrentValues(crParameterValues34)
        PFDefs(34).ApplyCurrentValues(crParameterValues35)
        PFDefs(35).ApplyCurrentValues(crParameterValues36)
        PFDefs(36).ApplyCurrentValues(crParameterValues37)
        PFDefs(37).ApplyCurrentValues(crParameterValues38)
        PFDefs(38).ApplyCurrentValues(crParameterValues39)
        PFDefs(39).ApplyCurrentValues(crParameterValues40)
        PFDefs(40).ApplyCurrentValues(crParameterValues41)
        PFDefs(41).ApplyCurrentValues(crParameterValues42)
        PFDefs(42).ApplyCurrentValues(crParameterValues43)
        PFDefs(43).ApplyCurrentValues(crParameterValues44)
        PFDefs(44).ApplyCurrentValues(crParameterValues45)

        crvView.ParameterFieldInfo = paramFields
    End Sub
End Class
