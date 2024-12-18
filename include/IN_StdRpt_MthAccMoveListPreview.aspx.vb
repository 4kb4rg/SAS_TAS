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

Public Class IN_StdRpt_MthAccMoveList_Preview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents EventData As DataGrid

    Dim objIN As New agri.IN.clsReport()
    Dim objINTrx As New agri.IN.clsTrx()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objINSetup As New agri.IN.clsSetup()
    Dim objPUTrx As New agri.PU.clsTrx()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objGLRpt As New agri.GL.clsReport()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strUserLoc As String
    Dim intConfigsetting As String

    Dim strDDLAccMth As String
    Dim strDDLAccYr As String
    Dim strDDLAccMthFrom As String
    Dim strDDLAccYrFrom As String

    Dim rdCrystalViewer As ReportDocument
    Dim intErrNo As Integer
    Dim intDecimal As Integer

    Dim strAnaGrp As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
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
            crvView.Visible = False

            If Right(Request.QueryString("Location"), 1) = "," Then
                strUserLoc = Left(Request.QueryString("Location"), Len(Request.QueryString("Location")) - 1)
            Else
                strUserLoc = Trim(Request.QueryString("Location"))
            End If

            strDDLAccMth = Request.QueryString("DDLAccMth")
            strDDLAccYr = Request.QueryString("DDLAccYr")
            intDecimal = Request.QueryString("Decimal")
            strAnaGrp = Request.QueryString("AnaGrp")

            If Not strDDLAccMth = 1 Then
                strDDLAccMthFrom = strDDLAccMth - 1
                strDDLAccYrFrom = strDDLAccYr
            Else
                strDDLAccMthFrom = 12
                strDDLAccYrFrom = strDDLAccYr - 1
            End If
            BindReportUsingStoredProcedure()
        End If
    End Sub

    Sub BindReportUsingStoredProcedure()
        Dim objRptDs As New DataSet()
        Dim objMapPath As String
        Dim tempLoc As String
        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim strOpCd As String = "IN_STDRPT_MTHACCMOVE_GET_SP"
        Dim strParam As String
	    Dim strLocCode As String
	    Dim strPrevAccMonth As String
	    Dim strPrevAccYear As String
	    Dim strAccMonth As String
	    Dim strAccYear As String
    	Dim strAccCode As String
        Dim strItemType As String
        Dim strStkIssueType As String
        Dim strFuelIssueType As String
        Dim strProdTable As String
	    Dim strProdCode As String
	    Dim strItemCode As String
	    Dim strProdDesc As String
	    Dim strSearch As String
	    Dim strStkRcvStatus As String
	    Dim strStkRtnAdvStatus As String
	    Dim strStkTranStatus As String
	    Dim strStkRtnStatus As String
	    Dim strStkAdjStatus As String
	    Dim strStkIssBlkStatus As String
	    Dim strFuelIssBlkStatus As String
	    Dim strStkIssVehStatus As String
	    Dim strFuelIssVehStatus As String
	    Dim strStkIssLedgerStatus As String
	    Dim strFuelIssLedgerStatus As String
	    Dim strStkIssLedgerEmpStatus As String
	    Dim strFuelIssLedgerEmpStatus As String
	    Dim strStkIssLedgerBSStatus As String
	    Dim strFuelIssLedgerBSStatus As String
	    Dim strStkIssLedgerBillStatus As String
	    Dim strFuelIssLedgerBillStatus As String
	    Dim strStkRtnVehStatus As String
	    Dim strStkRtnBlkStatus As String
	    Dim strStkRtnLedgerStatus As String
	    Dim strStkRtnLedgerEmpStatus As String
	    Dim strStkRtnLedgerBSStatus As String
	    Dim strPUGoodsRcvStatus As String
	    Dim strPUGoodsRetStatus As String
        Dim strPUDispAdvStatus As String
        Dim strCostLevel As String
	    Dim strPUGoodsRetType As String
	    Dim strPUDispAdvType As String
	    Dim strStkIssBlkBlkType As String
	    Dim strFuelIssBlkBlkType As String
	    Dim strStkIssLdgrBlkType As String
	    Dim strFuelIssLdgrBlkType As String
	    Dim strStkRtnBlkBlkType As String
	    Dim strStkRtnLdgrBlkType As String
        Dim strStkIssLedgerAccType As String
	    Dim strFuelIssLedgerAccType As String
	    Dim strStkRtnLedgerAccType As String

        tempLoc = Request.QueryString("Location")
        If Right(tempLoc, 1) = "," Then
            Session("SS_LOC") = Left(tempLoc, Len(tempLoc) - 1)
        Else
            Session("SS_LOC") = tempLoc.Replace("','", ", ")
        End If

        strLocCode = strUserLoc
        strPrevAccMonth = strDDLAccMthFrom
	    strPrevAccYear = strDDLAccYrFrom
	    strAccMonth = strDDLAccMth
	    strAccYear = strDDLAccYr
        If Request.QueryString("AccCode") = "" Then
            strAccCode = "%"
        Else
            strAccCode = Request.QueryString("AccCode")
        End If

        If (Request.QueryString("WS") = "Yes")
            strItemType = Convert.ToString(objINSetup.EnumInventoryItemType.Stock) & "','" & Convert.ToString(objINSetup.EnumInventoryItemType.WorkshopItem)
        Else
            strItemType = Convert.ToString(objINSetup.EnumInventoryItemType.Stock) 
        End If

        strStkIssueType = Convert.ToString(objINTrx.EnumStockIssueType.External) 
        strFuelIssueType = Convert.ToString(objINTrx.EnumFuelIssueType.External)
        strStkRcvStatus = Convert.ToString(objINTrx.EnumStockReceiveStatus.Confirmed) & "','" & Convert.ToString(objINTrx.EnumStockReceiveStatus.Closed)
	    strStkRtnAdvStatus = Convert.ToString(objINTrx.EnumStockRetAdvStatus.Confirmed) & "','" & Convert.ToString(objINTrx.EnumStockRetAdvStatus.Closed)
	    strStkTranStatus = Convert.ToString(objINTrx.EnumStockTransferStatus.Confirmed) & "','" & Convert.ToString(objINTrx.EnumStockTransferStatus.DBNote) & "','" & Convert.ToString(objINTrx.EnumStockTransferStatus.Closed)
	    strStkRtnStatus = Convert.ToString(objINTrx.EnumStockReturnStatus.Confirmed) & "','" & Convert.ToString(objINTrx.EnumStockReturnStatus.Closed)
	    strStkAdjStatus = Convert.ToString(objINTrx.EnumStockAdjustStatus.Confirmed) & "','" & Convert.ToString(objINTrx.EnumStockAdjustStatus.Closed)
	    strStkIssBlkStatus = Convert.ToString(objINTrx.EnumStockIssueStatus.Confirmed) & "','" & Convert.ToString(objINTrx.EnumStockIssueStatus.DBNote) & "','" & Convert.ToString(objINTrx.EnumStockIssueStatus.Closed)
	    strFuelIssBlkStatus = Convert.ToString(objINTrx.EnumFuelIssueStatus.Confirmed) & "','" & Convert.ToString(objINTrx.EnumFuelIssueStatus.DBNote) & "','" & Convert.ToString(objINTrx.EnumFuelIssueStatus.Closed)
	    strStkIssVehStatus = Convert.ToString(objINTrx.EnumStockIssueStatus.Confirmed) & "','" & Convert.ToString(objINTrx.EnumStockIssueStatus.DBNote) & "','" & Convert.ToString(objINTrx.EnumStockIssueStatus.Closed)
	    strFuelIssVehStatus = Convert.ToString(objINTrx.EnumFuelIssueStatus.Confirmed) & "','" & Convert.ToString(objINTrx.EnumFuelIssueStatus.DBNote) & "','" & Convert.ToString(objINTrx.EnumFuelIssueStatus.Closed)
	    strStkIssLedgerStatus = Convert.ToString(objINTrx.EnumStockIssueStatus.Confirmed) & "','" & Convert.ToString(objINTrx.EnumStockIssueStatus.DBNote) & "','" & Convert.ToString(objINTrx.EnumStockIssueStatus.Closed)
	    strFuelIssLedgerStatus = Convert.ToString(objINTrx.EnumFuelIssueStatus.Confirmed) & "','" & Convert.ToString(objINTrx.EnumFuelIssueStatus.DBNote) & "','" & Convert.ToString(objINTrx.EnumFuelIssueStatus.Closed)
	    strStkIssLedgerEmpStatus = Convert.ToString(objINTrx.EnumStockIssueStatus.Confirmed) & "','" & Convert.ToString(objINTrx.EnumStockIssueStatus.DBNote) & "','" & Convert.ToString(objINTrx.EnumStockIssueStatus.Closed)
	    strFuelIssLedgerEmpStatus = Convert.ToString(objINTrx.EnumFuelIssueStatus.Confirmed) & "','" & Convert.ToString(objINTrx.EnumFuelIssueStatus.DBNote) & "','" & Convert.ToString(objINTrx.EnumFuelIssueStatus.Closed)
	    strStkIssLedgerBSStatus = Convert.ToString(objINTrx.EnumStockIssueStatus.Confirmed) & "','" & Convert.ToString(objINTrx.EnumStockIssueStatus.DBNote) & "','" & Convert.ToString(objINTrx.EnumStockIssueStatus.Closed)
	    strFuelIssLedgerBSStatus = Convert.ToString(objINTrx.EnumFuelIssueStatus.Confirmed) & "','" & Convert.ToString(objINTrx.EnumFuelIssueStatus.DBNote) & "','" & Convert.ToString(objINTrx.EnumFuelIssueStatus.Closed)
	    strStkIssLedgerBillStatus = Convert.ToString(objINTrx.EnumStockIssueStatus.Confirmed) & "','" & Convert.ToString(objINTrx.EnumStockIssueStatus.DBNote) & "','" & Convert.ToString(objINTrx.EnumStockIssueStatus.Closed)
	    strFuelIssLedgerBillStatus = Convert.ToString(objINTrx.EnumFuelIssueStatus.Confirmed) & "','" & Convert.ToString(objINTrx.EnumFuelIssueStatus.DBNote) & "','" & Convert.ToString(objINTrx.EnumFuelIssueStatus.Closed)
	    strStkRtnVehStatus = Convert.ToString(objINTrx.EnumStockReturnStatus.Confirmed) & "','" & Convert.ToString(objINTrx.EnumStockReturnStatus.Closed)
	    strStkRtnBlkStatus = Convert.ToString(objINTrx.EnumStockReturnStatus.Confirmed) & "','" & Convert.ToString(objINTrx.EnumStockReturnStatus.Closed)
	    strStkRtnLedgerStatus = Convert.ToString(objINTrx.EnumStockReturnStatus.Confirmed) & "','" & Convert.ToString(objINTrx.EnumStockReturnStatus.Closed)
	    strStkRtnLedgerEmpStatus = Convert.ToString(objINTrx.EnumStockReturnStatus.Confirmed) & "','" & Convert.ToString(objINTrx.EnumStockReturnStatus.Closed)
	    strStkRtnLedgerBSStatus = Convert.ToString(objINTrx.EnumStockReturnStatus.Confirmed) & "','" & Convert.ToString(objINTrx.EnumStockReturnStatus.Closed)
	    strPUGoodsRcvStatus = Convert.ToString(objPUTrx.EnumGRStatus.Confirmed) & "','" & Convert.ToString(objPUTrx.EnumGRStatus.Closed)
	    strPUGoodsRetStatus = Convert.ToString(objPUTrx.EnumGRNStatus.Confirmed) & "','" & Convert.ToString(objPUTrx.EnumGRNStatus.Closed)
	    strPUDispAdvStatus = Convert.ToString(objPUTrx.EnumDAStatus.Confirmed) & "','" & Convert.ToString(objPUTrx.EnumDAStatus.Closed)
        strPUGoodsRetType = Convert.ToString(objPUTrx.EnumGRNType.Stock)
	    strPUDispAdvType = Convert.ToString(objPUTrx.EnumDAType.Stock)
        
        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
            strStkIssBlkBlkType = Convert.ToString(objGLSetup.EnumBlockType.MatureField) & "','" & Convert.ToString(objGLSetup.EnumBlockType.InMatureField) & "','" & Convert.ToString(objGLSetup.EnumBlockType.Nursery)
            strFuelIssBlkBlkType = Convert.ToString(objGLSetup.EnumBlockType.MatureField) & "','" & Convert.ToString(objGLSetup.EnumBlockType.InMatureField) & "','" & Convert.ToString(objGLSetup.EnumBlockType.Nursery)
            strStkIssLdgrBlkType = Convert.ToString(objGLSetup.EnumBlockType.Office)
            strFuelIssLdgrBlkType = Convert.ToString(objGLSetup.EnumBlockType.Office)
            strStkRtnBlkBlkType = Convert.ToString(objGLSetup.EnumBlockType.MatureField) & "','" & Convert.ToString(objGLSetup.EnumBlockType.InMatureField) & "','" & Convert.ToString(objGLSetup.EnumBlockType.Nursery)
            strStkRtnLdgrBlkType = Convert.ToString(objGLSetup.EnumBlockType.Office)
            strCostLevel = "true"
        Else
            strStkIssBlkBlkType = Convert.ToString(objGLSetup.EnumSubBlockType.MatureField) & "','" & Convert.ToString(objGLSetup.EnumSubBlockType.InMatureField) & "','" & Convert.ToString(objGLSetup.EnumSubBlockType.Nursery)
            strFuelIssBlkBlkType = Convert.ToString(objGLSetup.EnumSubBlockType.MatureField) & "','" & Convert.ToString(objGLSetup.EnumSubBlockType.InMatureField) & "','" & Convert.ToString(objGLSetup.EnumSubBlockType.Nursery)
            strStkIssLdgrBlkType = Convert.ToString(objGLSetup.EnumSubBlockType.Office)
            strFuelIssLdgrBlkType = Convert.ToString(objGLSetup.EnumSubBlockType.Office)
            strStkRtnBlkBlkType = Convert.ToString(objGLSetup.EnumSubBlockType.MatureField) & "','" & Convert.ToString(objGLSetup.EnumSubBlockType.InMatureField) & "','" & Convert.ToString(objGLSetup.EnumSubBlockType.Nursery)
            strStkRtnLdgrBlkType = Convert.ToString(objGLSetup.EnumSubBlockType.Office)
            strCostLevel = "false"
        End If

        strStkIssLedgerAccType = Convert.ToString(objGLSetup.EnumAccountType.BalanceSheet)
        strFuelIssLedgerAccType = Convert.ToString(objGLSetup.EnumAccountType.BalanceSheet)
        strStkRtnLedgerAccType = Convert.ToString(objGLSetup.EnumAccountType.BalanceSheet)

        If strAnaGrp = "PType" Then
            strProdTable = "IN_PRODTYPE PTYPE"
            strProdCode = "PTYPE.ProdTypeCode"
            strItemCode = "ITM.ProdTypeCode"
            strProdDesc = "PTYPE.Description"
            If Not Request.QueryString("ProdType") = "" Then
                strSearch = " AND PTYPE.ProdTypeCode LIKE '" & Request.QueryString("ProdType") & "' "
            End If
        ElseIf strAnaGrp = "PBrand" Then
            strProdTable = "IN_PRODBRAND PBRA"
            strProdCode = "PBRA.ProdBrandCode"
            strItemCode = "ITM.ProdBrandCode"
            strProdDesc = "PBRA.Description"
            If Not Request.QueryString("ProdBrand") = "" Then
                strSearch = " AND PBRA.ProdBrandCode LIKE '" & Request.QueryString("ProdBrand") & "' "
            End If
        ElseIf strAnaGrp = "PModel" Then
            strProdTable = "IN_PRODMODEL PMOD"
            strProdCode = "PMOD.ProdModelCode"
            strItemCode = "ITM.ProdModelCode"
            strProdDesc = "PMOD.Description"
            If Not Request.QueryString("ProdModel") = "" Then
                strSearch = " AND PMOD.ProdModelCode LIKE '" & Request.QueryString("ProdModel") & "' "
            End If
        ElseIf strAnaGrp = "PCat" Then
            strProdTable = "IN_PRODCAT PCAT"
            strProdCode = "PCAT.ProdCatCode"
            strItemCode = "ITM.ProdCatCode"
            strProdDesc = "PCAT.Description"
            If Not Request.QueryString("ProdCat") = "" Then
                strSearch = " AND PCAT.ProdCatCode LIKE '" & Request.QueryString("ProdCat") & "' "
            End If
        ElseIf strAnaGrp = "PMat" Then
            strProdTable = "IN_PRODMAT PMAT"
            strProdCode = "PMAT.ProdMatCode"
            strItemCode = "ITM.ProdMatCode"
            strProdDesc = "PMAT.Description"
            If Not Request.QueryString("ProdMat") = "" Then
                strSearch = " AND PMAT.ProdMatCode LIKE '" & Request.QueryString("ProdMat") & "' "
            End If
        ElseIf strAnaGrp = "StkAna" Then
            strProdTable = "IN_STOCKANALYSIS SA"
            strProdCode = "SA.StockAnalysisCode"
            strItemCode = "ITM.StockAnalysisCode"
            strProdDesc = "SA.Description"
            If Not Request.QueryString("StkAna") = "" Then
                strSearch = " AND SA.StockAnalysisCode LIKE '" & Request.QueryString("StkAna") & "' "
            End If
        End If

        strParam = strLocCode & "|" & strPrevAccMonth & "|" & strPrevAccYear & "|" & strAccMonth & "|" & strAccYear & "|" & _
                   strAccCode & "|" & strItemType & "|" & strStkIssueType & "|" & strFuelIssueType & "|" & Request.QueryString("Supp") & "|" & _
                   strProdTable & "|" & strProdCode & "|" & strItemCode & "|" & strProdDesc & "|" & strSearch & "|" & _
                   strStkRcvStatus & "|" & strStkRtnAdvStatus & "|" & strStkTranStatus & "|" & strStkRtnStatus & "|" & strStkAdjStatus & "|" & strStkIssBlkStatus & "|" & strFuelIssBlkStatus & "|" & _
                   strStkIssVehStatus & "|" & strFuelIssVehStatus & "|" & strStkIssLedgerStatus & "|" & strFuelIssLedgerStatus & "|" & strStkIssLedgerEmpStatus & "|" & strFuelIssLedgerEmpStatus & "|" & _
                   strStkIssLedgerBSStatus & "|" & strFuelIssLedgerBSStatus & "|" & strStkIssLedgerBillStatus & "|" & strFuelIssLedgerBillStatus & "|" & strStkRtnVehStatus & "|" & strStkRtnBlkStatus & "|" & _
                   strStkRtnLedgerStatus & "|" & strStkRtnLedgerEmpStatus & "|" & strStkRtnLedgerBSStatus & "|" & strPUGoodsRcvStatus & "|" & strPUGoodsRetStatus & "|" & strPUDispAdvStatus & "|" & _
                   strPUGoodsRetType & "|" & strPUDispAdvType & "|" & _
                   strStkIssBlkBlkType & "|" & strFuelIssBlkBlkType & "|" & strStkIssLdgrBlkType & "|" & strFuelIssLdgrBlkType & "|" & strStkRtnBlkBlkType & "|" & strStkRtnLdgrBlkType & "|" & _
                   strStkIssLedgerAccType & "|" & strFuelIssLedgerAccType & "|" & strStkRtnLedgerAccType & "|" & strCostLevel

        Try
            intErrNo = objIN.mtdGetReport_MthStkAccMoveList(strOpCd, Replace(strParam, "'", "''"), objRptDs, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_IN_MTHACCMOVE_LIST_REPORT&errmesg=" & Exp.ToString() & "&redirect=")
        End Try











        rdCrystalViewer = New ReportDocument()
        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\IN_StdRpt_MthAccMoveList.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs) 

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3

        PassParam()

        Dim myTable As CrystalDecisions.CrystalReports.Engine.Table
        Dim myLogin As CrystalDecisions.Shared.TableLogOnInfo
        For Each myTable In rdCrystalViewer.Database.Tables

            If myTable.Name = "IN_StdRpt_DS_MthAccMoveList" Then
                myLogin = myTable.LogOnInfo

                Try
                    intErrNo = objGLRpt.mtdGet_DBConnObj(myLogin)
                Catch Exp As System.Exception
                    Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=IN_STDRPT_MONTHLY_STOCK_ACCOUNT_MOVEMENT_LIST&errmesg=" & Exp.ToString() & "&redirect=")
                End Try

                myTable.ApplyLogOnInfo(myLogin)
                myTable.Location = myLogin.ConnectionInfo.DatabaseName & ".dbo.IN_ACCMOVELIST_RPT"
            End If

        Next

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\IN_StdRpt_MthAccMoveList.pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/IN_StdRpt_MthAccMoveList.pdf"">")

        objRptDs.Dispose()
        If Not objRptDs Is Nothing Then
            objRptDs = Nothing
        End If

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

        ParamDiscreteValue1.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue2.Value = Session("SS_LOC")
        ParamDiscreteValue3.Value = Session("SS_USERNAME")
        ParamDiscreteValue4.Value = Request.QueryString("DDLAccMth")
        ParamDiscreteValue5.Value = Request.QueryString("DDLAccYr")
        ParamDiscreteValue6.Value = Request.QueryString("Decimal")
        ParamDiscreteValue7.Value = Request.QueryString("RptID")
        ParamDiscreteValue8.Value = Request.QueryString("RptName")
        ParamDiscreteValue9.Value = Request.QueryString("Supp")
        ParamDiscreteValue10.Value = Request.QueryString("lblBlk")
        ParamDiscreteValue11.Value = Request.QueryString("lblVeh")
        ParamDiscreteValue12.Value = Request.QueryString("lblLocation")
        ParamDiscreteValue13.Value = Request.QueryString("ProdType")
        ParamDiscreteValue14.Value = Request.QueryString("ProdBrand")
        ParamDiscreteValue15.Value = Request.QueryString("ProdModel")
        ParamDiscreteValue16.Value = Request.QueryString("ProdCat")
        ParamDiscreteValue17.Value = Request.QueryString("ProdMat")
        ParamDiscreteValue18.Value = Request.QueryString("StkAna")
        ParamDiscreteValue19.Value = Request.QueryString("AccCode")
        ParamDiscreteValue20.Value = Request.QueryString("lblProdTypeCode")
        ParamDiscreteValue21.Value = Request.QueryString("lblProdBrandCode")
        ParamDiscreteValue22.Value = Request.QueryString("lblProdModelCode")
        ParamDiscreteValue23.Value = Request.QueryString("lblProdCatCode")
        ParamDiscreteValue24.Value = Request.QueryString("lblProdMatCode")
        ParamDiscreteValue25.Value = Request.QueryString("lblStkAnaCode")
        ParamDiscreteValue26.Value = Request.QueryString("lblAccCode")
        ParamDiscreteValue27.Value = Request.QueryString("AnaGrp")
        ParamDiscreteValue28.Value = Request.QueryString("WS")

        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        ParamFieldDef1 = ParamFieldDefs.Item("ParamCompanyName")
        ParamFieldDef2 = ParamFieldDefs.Item("ParamLocation")
        ParamFieldDef3 = ParamFieldDefs.Item("ParamUserName")
        ParamFieldDef4 = ParamFieldDefs.Item("ParamAccMonth")
        ParamFieldDef5 = ParamFieldDefs.Item("ParamAccYear")
        ParamFieldDef6 = ParamFieldDefs.Item("ParamDecimal")
        ParamFieldDef7 = ParamFieldDefs.Item("ParamRptID")
        ParamFieldDef8 = ParamFieldDefs.Item("ParamRptName")
        ParamFieldDef9 = ParamFieldDefs.Item("ParamSuppress")
        ParamFieldDef10 = ParamFieldDefs.Item("lblBlk")
        ParamFieldDef11 = ParamFieldDefs.Item("lblVeh")
        ParamFieldDef12 = ParamFieldDefs.Item("lblLocation")
        ParamFieldDef13 = ParamFieldDefs.Item("ParamProdType")
        ParamFieldDef14 = ParamFieldDefs.Item("ParamProdBrand")
        ParamFieldDef15 = ParamFieldDefs.Item("ParamProdModel")
        ParamFieldDef16 = ParamFieldDefs.Item("ParamProdCat")
        ParamFieldDef17 = ParamFieldDefs.Item("ParamProdMat")
        ParamFieldDef18 = ParamFieldDefs.Item("ParamStkAna")
        ParamFieldDef19 = ParamFieldDefs.Item("ParamAccCode")
        ParamFieldDef20 = ParamFieldDefs.Item("lblProdTypeCode")
        ParamFieldDef21 = ParamFieldDefs.Item("lblProdBrandCode")
        ParamFieldDef22 = ParamFieldDefs.Item("lblProdModelCode")
        ParamFieldDef23 = ParamFieldDefs.Item("lblProdCatCode")
        ParamFieldDef24 = ParamFieldDefs.Item("lblProdMatCode")
        ParamFieldDef25 = ParamFieldDefs.Item("lblStkAnaCode")
        ParamFieldDef26 = ParamFieldDefs.Item("lblAccCode")
        ParamFieldDef27 = ParamFieldDefs.Item("ParamAnaGrp")
        ParamFieldDef28 = ParamFieldDefs.Item("ParamIncWS")

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
       
    End Sub
End Class
