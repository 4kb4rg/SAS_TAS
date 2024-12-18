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


Public Class CT_StdRpt_MthCTAccMoveList_Preview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents EventData As DataGrid

    Dim objCT As New agri.CT.clsReport()
    Dim objCTTrx As New agri.CT.clsTrx()
    Dim objCTSetup As New agri.CT.clsSetup()
    Dim objPUTrx As New agri.PU.clsTrx()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    
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

    Dim rdCrystalViewer As New ReportDocument()
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

            BindReport()
        End If
    End Sub

    Sub BindReport()
        Dim objRptDs As New DataSet()
        Dim objMapPath As String
        Dim tempLoc As String

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOpCd As String = "CT_STDRPT_MONTHLY_CANTEEN_ACCOUNT_MOVEMENT_LIST_SP"
        Dim strParam As String
        
	    Dim strLocCode As String
	    Dim strAccMonth As String
	    Dim strAccYear As String
    	Dim strAccCode As String
        Dim strItemType As String
        
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
	    Dim strStkIssStatus As String

	    Dim strPUGoodsRcvStatus As String
	    Dim strPUGoodsRetStatus As String
	    Dim strPUDispAdvStatus As String

	    Dim strPUGoodsRetType As String
	    Dim strPUDispAdvType As String
    	
        tempLoc = Request.QueryString("Location")
        If Right(tempLoc, 1) = "," Then
            Session("SS_LOC") = Left(tempLoc, Len(tempLoc) - 1)
        Else
            Session("SS_LOC") = tempLoc.Replace("','", ", ")
        End If
        
        strLocCode = strUserLoc
	    strAccMonth = strDDLAccMth
	    strAccYear = strDDLAccYr
        If Request.QueryString("AccCode") = "" Then
            strAccCode = "%"
        Else
            strAccCode = Request.QueryString("AccCode")
        End If
        strItemType = Convert.ToString(objCTSetup.EnumInventoryItemType.CanteenItem)
        
        strStkRcvStatus = Convert.ToString(objCTTrx.EnumCanteenReceiveStatus.Confirmed) & "','" & Convert.ToString(objCTTrx.EnumCanteenReceiveStatus.Closed)
	    strStkRtnAdvStatus = Convert.ToString(objCTTrx.EnumCanRetAdvStatus.Confirmed) & "','" & Convert.ToString(objCTTrx.EnumCanRetAdvStatus.Closed)
	    strStkTranStatus = Convert.ToString(objCTTrx.EnumStockTransferStatus.Confirmed) & "','" & Convert.ToString(objCTTrx.EnumStockTransferStatus.DBNote) & "','" & Convert.ToString(objCTTrx.EnumStockTransferStatus.Closed)
	    strStkRtnStatus = Convert.ToString(objCTTrx.EnumStockReturnStatus.Confirmed) & "','" & Convert.ToString(objCTTrx.EnumStockReturnStatus.Closed)
	    strStkAdjStatus = Convert.ToString(objCTTrx.EnumStockAdjustStatus.Confirmed) & "','" & Convert.ToString(objCTTrx.EnumStockAdjustStatus.Closed)
	    strStkIssStatus = Convert.ToString(objCTTrx.EnumStockIssueStatus.Confirmed) & "','" & Convert.ToString(objCTTrx.EnumStockIssueStatus.DBNote) & "','" & Convert.ToString(objCTTrx.EnumStockIssueStatus.Closed)
	    strPUGoodsRcvStatus = Convert.ToString(objPUTrx.EnumGRStatus.Confirmed) & "','" & Convert.ToString(objPUTrx.EnumGRStatus.Closed)
	    strPUGoodsRetStatus = Convert.ToString(objPUTrx.EnumGRNStatus.Confirmed) & "','" & Convert.ToString(objPUTrx.EnumGRNStatus.Closed)
	    strPUDispAdvStatus = Convert.ToString(objPUTrx.EnumDAStatus.Confirmed) & "','" & Convert.ToString(objPUTrx.EnumDAStatus.Closed)
        
        strPUGoodsRetType = Convert.ToString(objPUTrx.EnumGRNType.Canteen)
	    strPUDispAdvType = Convert.ToString(objPUTrx.EnumDAType.Canteen)
        
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
        strParam = strLocCode & "|" & strAccMonth & "|" & strAccYear & "|" & strAccCode & "|" & strItemType & "|" & _
                   strProdTable & "|" & strProdCode & "|" & strItemCode & "|" & strProdDesc & "|" & strSearch & "|" & _
                   strStkRcvStatus & "|" & strStkRtnAdvStatus & "|" & strStkTranStatus & "|" & _
                   strStkAdjStatus & "|" & strStkIssStatus & "|" & strStkRtnStatus & "|" & _
                   strPUGoodsRcvStatus & "|" & strPUGoodsRetStatus & "|" & strPUDispAdvStatus & "|" & _
                   strPUGoodsRetType & "|" & strPUDispAdvType
        Try
            intErrNo = objCT.mtdGetReport_MthCTAccMoveList(strOpCd, Replace(strParam, "'", "''"), objRptDs, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=CT_STDRPT_MONTHLY_CANTEEN_ACCOUNT_MOVEMENT_LIST&errmesg=" & Exp.ToString() & "&redirect=")
        End Try


        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\CT_StdRpt_MthCTAccMoveList.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3

        crvView.Visible = False                         
        crvView.ReportSource = rdCrystalViewer          
        crvView.DataBind()                              

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\CT_StdRpt_MthCTAccMoveList.pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/CT_StdRpt_MthCTAccMoveList.pdf"">")
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
        paramField9 = paramFields.Item("ParamSuppress")
        paramField10 = paramFields.Item("lblLocation")
        paramField11 = paramFields.Item("ParamProdType")
        paramField12 = paramFields.Item("ParamProdBrand")
        paramField13 = paramFields.Item("ParamProdModel")
        paramField14 = paramFields.Item("ParamProdCat")
        paramField15 = paramFields.Item("ParamProdMat")
        paramField16 = paramFields.Item("ParamStkAna")
        paramField17 = paramFields.Item("ParamAccCode")
        paramField18 = paramFields.Item("lblProdTypeCode")
        paramField19 = paramFields.Item("lblProdBrandCode")
        paramField20 = paramFields.Item("lblProdModelCode")
        paramField21 = paramFields.Item("lblProdCatCode")
        paramField22 = paramFields.Item("lblProdMatCode")
        paramField23 = paramFields.Item("lblStkAnaCode")
        paramField24 = paramFields.Item("lblAccCode")
        paramField25 = paramFields.Item("ParamAnaGrp")

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

        ParamDiscreteValue1.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue2.Value = Session("SS_LOC")
        ParamDiscreteValue3.Value = Session("SS_USERNAME")
        ParamDiscreteValue4.Value = Request.QueryString("DDLAccMth")
        ParamDiscreteValue5.Value = Request.QueryString("DDLAccYr")
        ParamDiscreteValue6.Value = Request.QueryString("Decimal")
        ParamDiscreteValue7.Value = Request.QueryString("RptID")
        ParamDiscreteValue8.Value = Request.QueryString("RptName")
        ParamDiscreteValue9.Value = Request.QueryString("Supp")
        ParamDiscreteValue10.Value = Request.QueryString("lblLocation")
        ParamDiscreteValue11.Value = Request.QueryString("ProdType")
        ParamDiscreteValue12.Value = Request.QueryString("ProdBrand")
        ParamDiscreteValue13.Value = Request.QueryString("ProdModel")
        ParamDiscreteValue14.Value = Request.QueryString("ProdCat")
        ParamDiscreteValue15.Value = Request.QueryString("ProdMat")
        ParamDiscreteValue16.Value = Request.QueryString("StkAna")
        ParamDiscreteValue17.Value = Request.QueryString("AccCode")
        ParamDiscreteValue18.Value = Request.QueryString("lblProdTypeCode")
        ParamDiscreteValue19.Value = Request.QueryString("lblProdBrandCode")
        ParamDiscreteValue20.Value = Request.QueryString("lblProdModelCode")
        ParamDiscreteValue21.Value = Request.QueryString("lblProdCatCode")
        ParamDiscreteValue22.Value = Request.QueryString("lblProdMatCode")
        ParamDiscreteValue23.Value = Request.QueryString("lblStkAnaCode")
        ParamDiscreteValue24.Value = Request.QueryString("lblAccCode")
        ParamDiscreteValue25.Value = Request.QueryString("AnaGrp")

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

        crvView.ParameterFieldInfo = paramFields
    End Sub
End Class
