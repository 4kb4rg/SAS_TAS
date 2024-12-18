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

Public Class WS_StdRpt_MthWSUtilByBlock_Preview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTitle As Label
    Protected WithEvents EventData As DataGrid

    Dim objIN As New agri.IN.clsReport()
    Dim objINTrx As New agri.IN.clsTrx()

	Dim objWSReport As New agri.WS.clsReport() 

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strUserLoc As String

    Dim strBlkType As String
    Dim strIssueType As String
    Dim intDecimal As Integer

    Dim strAccMth As String
    Dim strAccYr As String
    Dim strDDLAccMth As String
    Dim strDDLAccYr As String

    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer


    Dim strItemType As String = ""


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        crvView.Visible = False

        If Right(Request.QueryString("Location"), 1) = "," Then
            strUserLoc = Left(Request.QueryString("Location"), Len(Request.QueryString("Location")) - 1)
        Else
            strUserLoc = Trim(Request.QueryString("Location"))
        End If

        strDDLAccMth = Request.QueryString("DDLAccMth")
        strDDLAccYr = Request.QueryString("DDLAccYr")
        strBlkType = Request.QueryString("BlkType")
        strIssueType = Request.QueryString("IssueType")
        intDecimal = Request.QueryString("Decimal")

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        strLangCode = Session("SS_LANGCODE")


        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            BindReport()
        End If
    End Sub

    Sub BindReport()

        Dim objRptDs As New DataSet()
        Dim objMapPath As String

        Dim StkSearchStr As String
        Dim FuelSearchStr As String

        Dim StkRtnSearchStr As String

        Dim strIndicator As String = ""
        Dim tempLoc As String
        Dim strParam As String

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOpCdMthInvUtil_SP As String = "WS_STDRPT_MTH_WS_UTIL_SP"

        tempLoc = Request.QueryString("Location")
        If Right(tempLoc, 1) = "," Then
            Session("SS_LOC") = Left(tempLoc, Len(tempLoc) - 1)
        Else
            Session("SS_LOC") = tempLoc.Replace("','", ", ")
        End If

        If strBlkType = "BlkGrp" Then
            If strIssueType = "Stk" Then
                If Not Request.QueryString("BlkGrp") = "" Then
                    StkSearchStr = "AND BG.BlkGrpCode LIKE '" & Request.QueryString("BlkGrp") & "' "

                    StkRtnSearchStr = "AND BG.BlkGrpCode LIKE '" & Request.QueryString("BlkGrp") & "' "

                End If

                strIndicator = "BLKGRP_STK"
            ElseIf strIssueType = "Fuel" Then
                If Not Request.QueryString("BlkGrp") = "" Then
                    FuelSearchStr = "AND BG.BlkGrpCode LIKE '" & Request.QueryString("BlkGrp") & "' "
                End If

                strIndicator = "BLKGRP_FUL"
            ElseIf strIssueType = "All" Then

                If Not Request.QueryString("BlkGrp") = "" Then
                    StkSearchStr = "AND BG.BlkGrpCode LIKE '" & Request.QueryString("BlkGrp") & "' "
                    FuelSearchStr = "AND BG.BlkGrpCode LIKE '" & Request.QueryString("BlkGrp") & "' "

                    StkRtnSearchStr = "AND BG.BlkGrpCode LIKE '" & Request.QueryString("BlkGrp") & "' "

                End If

                strIndicator = "BLKGRP_ALL"
            End If
            Session("SS_BLKGRPHEADER") = "BlkGrp"
            lblTitle.Text = lblTitle.Text & "Block Listing"

        ElseIf strBlkType = "BlkCode" Then
            If strIssueType = "Stk" Then
                If Not Request.QueryString("BlkCode") = "" Then
                    StkSearchStr = "AND STKISSLN.BlkCode LIKE '" & Request.QueryString("BlkCode") & "' "

                    StkRtnSearchStr = "AND SRL.BlkCode LIKE '" & Request.QueryString("BlkCode") & "' "

                End If

                strIndicator = "BLK_STK"
            ElseIf strIssueType = "Fuel" Then
                If Not Request.QueryString("BlkCode") = "" Then
                    FuelSearchStr = "AND FISSLN.BlkCode LIKE '" & Request.QueryString("BlkCode") & "' "
                End If

                strIndicator = "BLK_FUL"
            ElseIf strIssueType = "All" Then

                If Not Request.QueryString("BlkCode") = "" Then
                    StkSearchStr = "AND STKISSLN.BlkCode LIKE '" & Request.QueryString("BlkCode") & "' "
                    FuelSearchStr = "AND FISSLN.BlkCode LIKE '" & Request.QueryString("BlkCode") & "' "

                    StkRtnSearchStr = "AND SRL.BlkCode LIKE '" & Request.QueryString("BlkCode") & "' "

                End If

                strIndicator = "BLK_ALL"
            End If
            Session("SS_BLKHEADER") = "Block"
            lblTitle.Text = lblTitle.Text & "Block Listing"

        ElseIf strBlkType = "SubBlkCode" Then
            If strIssueType = "Stk" Then
                If Not Request.QueryString("SubBlkCode") = "" Then
                    StkSearchStr = "AND STKISSLN.BlkCode LIKE '" & Request.QueryString("SubBlkCode") & "' "

                    StkRtnSearchStr = "AND SRL.BlkCode LIKE '" & Request.QueryString("SubBlkCode") & "' "

                End If


                strIndicator = "SUBBLK_STK"
            ElseIf strIssueType = "Fuel" Then
                If Not Request.QueryString("SubBlkCode") = "" Then
                    FuelSearchStr = "AND FISSLN.BlkCode LIKE '" & Request.QueryString("SubBlkCode") & "' "
                End If

                strIndicator = "SUBBLK_FUL"
            ElseIf strIssueType = "All" Then

                If Not Request.QueryString("SubBlkCode") = "" Then
                    StkSearchStr = "AND STKISSLN.BlkCode LIKE '" & Request.QueryString("SubBlkCode") & "' "
                    FuelSearchStr = "AND FISSLN.BlkCode LIKE '" & Request.QueryString("SubBlkCode") & "' "

                    StkRtnSearchStr = "AND SRL.BlkCode LIKE '" & Request.QueryString("SubBlkCode") & "' "

                End If

                strIndicator = "SUBBLK_ALL"
            End If
            Session("SS_SUBBLKHEADER") = "SubBlock"
            lblTitle.Text = lblTitle.Text & "Sub Block Listing"
        End If

        If strIssueType = "Stk" Then
            If Not Request.QueryString("ItemCode") = "" Then
                StkSearchStr = StkSearchStr & "AND STKISSLN.ItemCode LIKE '" & Request.QueryString("ItemCode") & "' "

                StkRtnSearchStr = StkRtnSearchStr & "AND SRL.ItemCode LIKE '" & Request.QueryString("ItemCode") & "' "

            End If

            If Not Request.QueryString("AccCode") = "" Then
                StkSearchStr = StkSearchStr & "AND STKISSLN.AccCode LIKE '" & Request.QueryString("AccCode") & "' "

                StkRtnSearchStr = StkRtnSearchStr & "AND SRL.AccCode LIKE '" & Request.QueryString("AccCode") & "' "

            End If

        ElseIf strIssueType = "Fuel" Then
            If Not Request.QueryString("ItemCode") = "" Then
                FuelSearchStr = FuelSearchStr & "AND FISSLN.ItemCode LIKE '" & Request.QueryString("ItemCode") & "' "
            End If

            If Not Request.QueryString("AccCode") = "" Then
                FuelSearchStr = FuelSearchStr & "AND FISSLN.AccCode LIKE '" & Request.QueryString("AccCode") & "' "
            End If

        ElseIf strIssueType = "All" Then


            If Not Request.QueryString("ItemCode") = "" Then
                StkSearchStr = StkSearchStr & "AND STKISSLN.ItemCode LIKE '" & Request.QueryString("ItemCode") & "' "
                FuelSearchStr = FuelSearchStr & "AND FISSLN.ItemCode LIKE '" & Request.QueryString("ItemCode") & "' "

                StkRtnSearchStr = StkRtnSearchStr & "AND SRL.ItemCode LIKE '" & Request.QueryString("ItemCode") & "' "

            End If

            If Not Request.QueryString("AccCode") = "" Then
                StkSearchStr = StkSearchStr & "AND STKISSLN.AccCode LIKE '" & Request.QueryString("AccCode") & "' "
                FuelSearchStr = FuelSearchStr & "AND FISSLN.AccCode LIKE '" & Request.QueryString("AccCode") & "' "

                StkRtnSearchStr = StkRtnSearchStr & "AND SRL.AccCode LIKE '" & Request.QueryString("AccCode") & "' "

            End If

        End If

        If Not Request.QueryString("ProdType") = "" Then
            StkSearchStr = StkSearchStr & "AND ITM.ProdTypeCode LIKE '" & Request.QueryString("ProdType") & "' "
            FuelSearchStr = FuelSearchStr & "AND ITM.ProdTypeCode LIKE '" & Request.QueryString("ProdType") & "' "

            StkRtnSearchStr = StkRtnSearchStr & "AND ITM.ProdTypeCode LIKE '" & Request.QueryString("ProdType") & "' "

        End If

        If Not Request.QueryString("ProdBrand") = "" Then
            StkSearchStr = StkSearchStr & "AND ITM.ProdBrandCode LIKE '" & Request.QueryString("ProdBrand") & "' "
            FuelSearchStr = FuelSearchStr & "AND ITM.ProdBrandCode LIKE '" & Request.QueryString("ProdBrand") & "' "

            StkRtnSearchStr = StkRtnSearchStr & "AND ITM.ProdBrandCode LIKE '" & Request.QueryString("ProdBrand") & "' "

        End If

        If Not Request.QueryString("ProdModel") = "" Then
            StkSearchStr = StkSearchStr & "AND ITM.ProdModelCode LIKE '" & Request.QueryString("ProdModel") & "' "
            FuelSearchStr = FuelSearchStr & "AND ITM.ProdModelCode LIKE '" & Request.QueryString("ProdModel") & "' "

            StkRtnSearchStr = StkRtnSearchStr & "AND ITM.ProdModelCode LIKE '" & Request.QueryString("ProdModel") & "' "

        End If

        If Not Request.QueryString("ProdCat") = "" Then
            StkSearchStr = StkSearchStr & "AND ITM.ProdCatCode LIKE '" & Request.QueryString("ProdCat") & "' "
            FuelSearchStr = FuelSearchStr & "AND ITM.ProdCatCode LIKE '" & Request.QueryString("ProdCat") & "' "

            StkRtnSearchStr = StkRtnSearchStr & "AND ITM.ProdCatCode LIKE '" & Request.QueryString("ProdCat") & "' "

        End If

        If Not Request.QueryString("ProdMat") = "" Then
            StkSearchStr = StkSearchStr & "AND ITM.ProdMatCode LIKE '" & Request.QueryString("ProdMat") & "' "
            FuelSearchStr = FuelSearchStr & "AND ITM.ProdMatCode LIKE '" & Request.QueryString("ProdMat") & "' "

            StkRtnSearchStr = StkRtnSearchStr & "AND ITM.ProdMatCode LIKE '" & Request.QueryString("ProdMat") & "' "

        End If

        If Not Request.QueryString("StkAna") = "" Then
            StkSearchStr = StkSearchStr & "AND ITM.StockAnalysisCode LIKE '" & Request.QueryString("StkAna") & "' "
            FuelSearchStr = FuelSearchStr & "AND ITM.StockAnalysisCode LIKE '" & Request.QueryString("StkAna") & "' "

            StkRtnSearchStr = StkRtnSearchStr & "AND ITM.StockAnalysisCode LIKE '" & Request.QueryString("StkAna") & "' "

        End If




        strItemType = "AND ITM.ItemType LIKE '4' "

        strParam = strUserLoc & "|" & strDDLAccMth & "|" & strDDLAccYr & "|" & strIndicator & "|" & _
                   objINTrx.EnumStockIssueStatus.Confirmed & "','" & objINTrx.EnumStockIssueStatus.Closed & "','" & objINTrx.EnumStockIssueStatus.DBNote & "|" & _
                   objINTrx.EnumFuelIssueStatus.Confirmed & "','" & objINTrx.EnumFuelIssueStatus.Closed & "','" & objINTrx.EnumFuelIssueStatus.DBNote & "|" & _
                   objINTrx.EnumStockReturnStatus.Confirmed & "','" & objINTrx.EnumStockReturnStatus.Closed & "|" & _
                   StkSearchStr & "|" & FuelSearchStr & "|" & StkRtnSearchStr & "|" & strItemType  


        Try
			
			intErrNo = objWSReport.mtdGetReport_MthInvUtilList(strOpCdMthInvUtil_SP, strLocation, strParam, objRptDs, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_MTHINVUTILBYBLK_LIST_REPORT&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try


        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\WS_StdRpt_MthWSUtilByBlockList.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3

        crvView.Visible = False                         
        crvView.ReportSource = rdCrystalViewer          
        crvView.DataBind()                              

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\WS_StdRpt_MthWSUtilByBlockList.pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/WS_StdRpt_MthWSUtilByBlockList.pdf"">")
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

        paramField1 = paramFields.Item("ParamCompanyName")
        paramField2 = paramFields.Item("ParamLocation")
        paramField3 = paramFields.Item("ParamUserName")
        paramField4 = paramFields.Item("ParamAccMonth")
        paramField5 = paramFields.Item("ParamAccYear")
        paramField6 = paramFields.Item("ParamDecimal")
        paramField7 = paramFields.Item("ParamRptID")
        paramField8 = paramFields.Item("ParamRptName")
        paramField9 = paramFields.Item("ParamBlkType")
        paramField10 = paramFields.Item("ParamBlkGrp")
        paramField11 = paramFields.Item("ParamBlkCode")
        paramField12 = paramFields.Item("ParamSubBlkCode")
        paramField13 = paramFields.Item("ParamBlkOrSubBlk")
        paramField14 = paramFields.Item("ParamIssueType")
        paramField15 = paramFields.Item("ParamItemCode")
        paramField16 = paramFields.Item("lblBlkGrp")
        paramField17 = paramFields.Item("lblBlkCode")
        paramField18 = paramFields.Item("lblSubBlkCode")
        paramField19 = paramFields.Item("lblLocation")
        paramField20 = paramFields.Item("ParamProdType")
        paramField21 = paramFields.Item("ParamProdBrand")
        paramField22 = paramFields.Item("ParamProdModel")
        paramField23 = paramFields.Item("ParamProdCat")
        paramField24 = paramFields.Item("ParamProdMat")
        paramField25 = paramFields.Item("ParamStkAna")
        paramField26 = paramFields.Item("lblProdTypeCode")
        paramField27 = paramFields.Item("lblProdBrandCode")
        paramField28 = paramFields.Item("lblProdModelCode")
        paramField29 = paramFields.Item("lblProdCatCode")
        paramField30 = paramFields.Item("lblProdMatCode")
        paramField31 = paramFields.Item("lblStkAnaCode")
        paramField32 = paramFields.Item("ParamAccCode")
        paramField33 = paramFields.Item("lblAccCode")


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
        ParamDiscreteValue2.Value = Session("SS_LOC")
        ParamDiscreteValue3.Value = Session("SS_USERNAME")
        ParamDiscreteValue4.Value = Request.QueryString("DDLAccMth")
        ParamDiscreteValue5.Value = Request.QueryString("DDLAccYr")
        ParamDiscreteValue6.Value = Request.QueryString("Decimal")
        ParamDiscreteValue7.Value = Request.QueryString("RptID")
        ParamDiscreteValue8.Value = Request.QueryString("RptName")
        ParamDiscreteValue9.Value = Request.QueryString("BlkType")
        ParamDiscreteValue10.Value = Request.QueryString("BlkGrp")
        ParamDiscreteValue11.Value = Request.QueryString("BlkCode")
        ParamDiscreteValue12.Value = Request.QueryString("SubBlkCode")

        If strBlkType = "BlkGrp" Then
            ParamDiscreteValue13.Value = Session("SS_BLKGRPHEADER")
        ElseIf strBlkType = "BlkCode" Then
            ParamDiscreteValue13.Value = Session("SS_BLKHEADER")
        ElseIf strBlkType = "SubBlkCode" Then
            ParamDiscreteValue13.Value = Session("SS_SUBBLKHEADER")
        End If

        ParamDiscreteValue14.Value = Request.QueryString("IssueType")
        ParamDiscreteValue15.Value = Request.QueryString("ItemCode")
        ParamDiscreteValue16.Value = Request.QueryString("lblBlkGrp")
        ParamDiscreteValue17.Value = Request.QueryString("lblBlkCode")
        ParamDiscreteValue18.Value = Request.QueryString("lblSubBlkCode")
        ParamDiscreteValue19.Value = Request.QueryString("lblLocation")
        ParamDiscreteValue20.Value = Request.QueryString("ProdType")
        ParamDiscreteValue21.Value = Request.QueryString("ProdBrand")
        ParamDiscreteValue22.Value = Request.QueryString("ProdModel")
        ParamDiscreteValue23.Value = Request.QueryString("ProdCat")
        ParamDiscreteValue24.Value = Request.QueryString("ProdMat")
        ParamDiscreteValue25.Value = Request.QueryString("StkAna")
        ParamDiscreteValue26.Value = Request.QueryString("lblProdTypeCode")
        ParamDiscreteValue27.Value = Request.QueryString("lblProdBrandCode")
        ParamDiscreteValue28.Value = Request.QueryString("lblProdModelCode")
        ParamDiscreteValue29.Value = Request.QueryString("lblProdCatCode")
        ParamDiscreteValue30.Value = Request.QueryString("lblProdMatCode")
        ParamDiscreteValue31.Value = Request.QueryString("lblStkAnaCode")
        ParamDiscreteValue32.Value = Request.QueryString("AccCode")
        ParamDiscreteValue33.Value = Request.QueryString("lblAccCode")


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
