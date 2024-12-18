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

Public Class CT_StdRpt_MthlyStkRptList_Preview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents EventData As DataGrid

    Dim objCTRpt As New agri.CT.clsReport()
    Dim objCTTrx As New agri.CT.clsTrx()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()

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
    Dim tempLoc As String
    Dim strDDLAccMthBF As String
    Dim strDDLAccYrBF As String
    Dim strDDLAccMth As String
    Dim strDDLAccYr As String

    Dim tblStkRpt As DataTable = New DataTable("tblStkRpt")

    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer

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

            tempLoc = Request.QueryString("Location")
            If Right(tempLoc, 1) = "," Then
                Session("SS_LOC") = Left(tempLoc, Len(tempLoc) - 1)
            Else
                Session("SS_LOC") = tempLoc.Replace("','", ", ")
            End If

            strDDLAccMth = Request.QueryString("DDLAccMth")
            strDDLAccYr = Request.QueryString("DDLAccYr")
            intDecimal = Request.QueryString("Decimal")

            strDDLAccMthBF = strDDLAccMth
            strDDLAccYrBF = strDDLAccYr
            BindReport()
        End If
    End Sub

    Sub BindReport()

        Dim objRptDsStkRpt As New DataSet()
        Dim dsBalBF As New DataSet()
        Dim objMapPath As String
        Dim arrLocCode As Array

        Dim intCnt As Integer
        Dim intCntLocCode As Integer

        Dim SearchStr As String
        Dim strParam As String
        Dim dr As DataRow

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOpCdMthlyStkRpt_StkRpt_GET_SP As String = "CT_STDRPT_MONTHLY_STKRPT_GET_SP"


        If Not Request.QueryString("ProdType") = "" Then
            SearchStr = "AND ITM.ProdTypeCode LIKE '" & Request.QueryString("ProdType") & "' "
        End If

        If Not Request.QueryString("ProdBrand") = "" Then
            SearchStr = SearchStr & "AND ITM.ProdBrandCode LIKE '" & Request.QueryString("ProdBrand") & "' "
        End If

        If Not Request.QueryString("ProdModel") = "" Then
            SearchStr = SearchStr & "AND ITM.ProdModelCode LIKE '" & Request.QueryString("ProdModel") & "' "
        End If

        If Not Request.QueryString("ProdCat") = "" Then
            SearchStr = SearchStr & "AND ITM.ProdCatCode LIKE '" & Request.QueryString("ProdCat") & "' "
        End If

        If Not Request.QueryString("ProdMat") = "" Then
            SearchStr = SearchStr & "AND ITM.ProdMatCode LIKE '" & Request.QueryString("ProdMat") & "' "
        End If

        If Not Request.QueryString("StkAna") = "" Then
            SearchStr = SearchStr & "AND ITM.StockAnalysisCode LIKE '" & Request.QueryString("StkAna") & "' "
        End If

        If Not Request.QueryString("ItemCode") = "" Then
            SearchStr = SearchStr & "AND ITM.ItemCode LIKE '" & Request.QueryString("ItemCode") & "' "
        End If

        CreateTables()

        arrLocCode = Split(strUserLoc, "','")
        For intCntLocCode = 0 To arrLocCode.GetUpperBound(0)

            strParam = arrLocCode(intCntLocCode) & "|" & strDDLAccMthBF & "|" & strDDLAccYrBF & "|" & strDDLAccMth & "|" & strDDLAccYr & "|''" & _
                    Convert.ToString(objCTTrx.EnumCanteenReceiveStatus.Confirmed) & "'',''" & Convert.ToString(objCTTrx.EnumCanteenReceiveStatus.Closed) & "''|''" & _
                    Convert.ToString(objCTTrx.EnumCanRetAdvStatus.Confirmed) & "'',''" & Convert.ToString(objCTTrx.EnumCanRetAdvStatus.Closed) & "''|''" & _
                    Convert.ToString(objCTTrx.EnumStockIssueStatus.Confirmed) & "'',''" & Convert.ToString(objCTTrx.EnumStockIssueStatus.DBNote) & "'',''" & Convert.ToString(objCTTrx.EnumStockIssueStatus.Closed) & "''|''" & _
                    Convert.ToString(objCTTrx.EnumStockTransferStatus.Confirmed) & "'',''" & Convert.ToString(objCTTrx.EnumStockTransferStatus.DBNote) & "'',''" & Convert.ToString(objCTTrx.EnumStockTransferStatus.Closed) & "''|''" & _
                    Convert.ToString(objCTTrx.EnumStockReturnStatus.Confirmed) & "'',''" & Convert.ToString(objCTTrx.EnumStockReturnStatus.Closed) & "''|''" & _
                    Convert.ToString(objCTTrx.EnumStockAdjustStatus.Confirmed) & "'',''" & Convert.ToString(objCTTrx.EnumStockAdjustStatus.Closed) & "''|" & _
                    Convert.ToString(objGlobal.EnumDocType.CanteenReturnAdvice) & "|" & Replace(SearchStr, "'", "''")
            Try
                intErrNo = objCTRpt.mtdGetReport_MthlyStockReportList(strOpCdMthlyStkRpt_StkRpt_GET_SP, strParam, dsBalBF, objMapPath)
            Catch Exp As System.Exception
                Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_CT_MTHLYSTKRPT_BALBF_LIST&errmesg=" & Exp.ToString() & "&redirect=")
            End Try

            If dsBalBF.Tables(0).Rows.Count > 0 Then
                For intCnt = 0 To dsBalBF.Tables(0).Rows.Count - 1
                    dr = tblStkRpt.NewRow()
                    dr("LocCode") = dsBalBF.Tables(0).Rows(intCnt).Item("LocCode").Trim()
                    dr("ItemCode") = dsBalBF.Tables(0).Rows(intCnt).Item("ItemCode").Trim()
                    dr("ItemDesc") = dsBalBF.Tables(0).Rows(intCnt).Item("ItemDesc").Trim()
                    dr("UOM") = dsBalBF.Tables(0).Rows(intCnt).Item("UOM").Trim()
                    dr("BalBFQty") = dsBalBF.Tables(0).Rows(intCnt).Item("BalBFQty")
                    dr("BalBFCost") = dsBalBF.Tables(0).Rows(intCnt).Item("BalBFCost")
                    dr("RcvQty") = dsBalBF.Tables(0).Rows(intCnt).Item("RcvQty")
                    dr("RcvAmt") = dsBalBF.Tables(0).Rows(intCnt).Item("RcvAmt")
                    dr("SalesQty") = dsBalBF.Tables(0).Rows(intCnt).Item("SalesQty")
                    dr("SalesAmt") = dsBalBF.Tables(0).Rows(intCnt).Item("SalesAmt")
                    dr("AdjQty") = dsBalBF.Tables(0).Rows(intCnt).Item("AdjQty")
                    dr("AdjAmt") = dsBalBF.Tables(0).Rows(intCnt).Item("AdjAmt")
                    dr("BalCFQty") = dsBalBF.Tables(0).Rows(intCnt).Item("BalCFQty")
                    dr("BalCFCost") = dsBalBF.Tables(0).Rows(intCnt).Item("BalCFCost")
                    dr("BalCFAmt") = dsBalBF.Tables(0).Rows(intCnt).Item("BalCFAmt")
                    tblStkRpt.Rows.Add(dr)
                Next
            End If
        Next
        objRptDsStkRpt.Tables.Add(tblStkRpt)


        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\CT_StdRpt_MthlyStkRptList.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDsStkRpt.Tables(0))

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3

        crvView.Visible = False                         
        crvView.ReportSource = rdCrystalViewer          
        crvView.DataBind()                              

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\CT_StdRpt_MthlyStkRptList.pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()

        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/CT_StdRpt_MthlyStkRptList.pdf"">")

        objRptDsStkRpt = Nothing
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

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("ParamLocation")
        paramField2 = paramFields.Item("ParamCompanyName")
        paramField3 = paramFields.Item("ParamUserName")
        paramField4 = paramFields.Item("ParamDecimal")
        paramField5 = paramFields.Item("ParamRptID")
        paramField6 = paramFields.Item("ParamRptName")
        paramField7 = paramFields.Item("ParamAccMonth")
        paramField8 = paramFields.Item("ParamAccYear")
        paramField9 = paramFields.Item("ParamItemCode")
        paramField10 = paramFields.Item("lblLocation")
        paramField11 = paramFields.Item("ParamProdType")
        paramField12 = paramFields.Item("ParamProdBrand")
        paramField13 = paramFields.Item("ParamProdModel")
        paramField14 = paramFields.Item("ParamProdCat")
        paramField15 = paramFields.Item("ParamProdMat")
        paramField16 = paramFields.Item("ParamStkAna")
        paramField17 = paramFields.Item("lblProdTypeCode")
        paramField18 = paramFields.Item("lblProdBrandCode")
        paramField19 = paramFields.Item("lblProdModelCode")
        paramField20 = paramFields.Item("lblProdCatCode")
        paramField21 = paramFields.Item("lblProdMatCode")
        paramField22 = paramFields.Item("lblStkAnaCode")
        paramField23 = paramFields.Item("ParamSuppZero")

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

        ParamDiscreteValue1.Value = Session("SS_LOC")
        ParamDiscreteValue2.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue3.Value = Session("SS_USERNAME")
        ParamDiscreteValue4.Value = Request.QueryString("Decimal")
        ParamDiscreteValue5.Value = Request.QueryString("RptID")
        ParamDiscreteValue6.Value = Request.QueryString("RptName")
        ParamDiscreteValue7.Value = Request.QueryString("DDLAccMth")
        ParamDiscreteValue8.Value = Request.QueryString("DDLAccYr")
        ParamDiscreteValue9.Value = Request.QueryString("ItemCode")
        ParamDiscreteValue10.Value = Request.QueryString("lblLocation")
        ParamDiscreteValue11.Value = Request.QueryString("ProdType")
        ParamDiscreteValue12.Value = Request.QueryString("ProdBrand")
        ParamDiscreteValue13.Value = Request.QueryString("ProdModel")
        ParamDiscreteValue14.Value = Request.QueryString("ProdCat")
        ParamDiscreteValue15.Value = Request.QueryString("ProdMat")
        ParamDiscreteValue16.Value = Request.QueryString("StkAna")
        ParamDiscreteValue17.Value = Request.QueryString("lblProdTypeCode")
        ParamDiscreteValue18.Value = Request.QueryString("lblProdBrandCode")
        ParamDiscreteValue19.Value = Request.QueryString("lblProdModelCode")
        ParamDiscreteValue20.Value = Request.QueryString("lblProdCatCode")
        ParamDiscreteValue21.Value = Request.QueryString("lblProdMatCode")
        ParamDiscreteValue22.Value = Request.QueryString("lblStkAnaCode")
        ParamDiscreteValue23.Value = Request.QueryString("Supp")

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

        crvView.ParameterFieldInfo = paramFields
    End Sub


    Sub CreateTables()
        Dim Col1 As DataColumn = New DataColumn()
        Dim Col2 As DataColumn = New DataColumn()
        Dim Col3 As DataColumn = New DataColumn()
        Dim Col4 As DataColumn = New DataColumn()
        Dim Col5 As DataColumn = New DataColumn()
        Dim Col6 As DataColumn = New DataColumn()
        Dim Col7 As DataColumn = New DataColumn()
        Dim Col8 As DataColumn = New DataColumn()
        Dim Col9 As DataColumn = New DataColumn()
        Dim Col10 As DataColumn = New DataColumn()
        Dim Col11 As DataColumn = New DataColumn()
        Dim Col12 As DataColumn = New DataColumn()
        Dim Col13 As DataColumn = New DataColumn()
        Dim Col14 As DataColumn = New DataColumn()
        Dim Col15 As DataColumn = New DataColumn()

        Col1.DataType = System.Type.GetType("System.String")
        Col1.AllowDBNull = True
        Col1.Caption = "LocCode"
        Col1.ColumnName = "LocCode"
        Col1.DefaultValue = ""
        tblStkRpt.Columns.Add(Col1)

        Col2.DataType = System.Type.GetType("System.String")
        Col2.AllowDBNull = True
        Col2.Caption = "ItemCode"
        Col2.ColumnName = "ItemCode"
        Col2.DefaultValue = ""
        tblStkRpt.Columns.Add(Col2)

        Col3.DataType = System.Type.GetType("System.String")
        Col3.AllowDBNull = True
        Col3.Caption = "ItemDesc"
        Col3.ColumnName = "ItemDesc"
        Col3.DefaultValue = ""
        tblStkRpt.Columns.Add(Col3)

        Col4.DataType = System.Type.GetType("System.String")
        Col4.AllowDBNull = True
        Col4.Caption = "UOM"
        Col4.ColumnName = "UOM"
        Col4.DefaultValue = ""
        tblStkRpt.Columns.Add(Col4)

        Col5.DataType = System.Type.GetType("System.Decimal")
        Col5.AllowDBNull = True
        Col5.Caption = "BalBFQty"
        Col5.ColumnName = "BalBFQty"
        Col5.DefaultValue = 0
        tblStkRpt.Columns.Add(Col5)

        Col6.DataType = System.Type.GetType("System.Decimal")
        Col6.AllowDBNull = True
        Col6.Caption = "BalBFCost"
        Col6.ColumnName = "BalBFCost"
        Col6.DefaultValue = 0
        tblStkRpt.Columns.Add(Col6)    

        Col7.DataType = System.Type.GetType("System.Decimal")
        Col7.AllowDBNull = True
        Col7.Caption = "RcvQty"
        Col7.ColumnName = "RcvQty"
        Col7.DefaultValue = 0
        tblStkRpt.Columns.Add(Col7)

        Col8.DataType = System.Type.GetType("System.Decimal")
        Col8.AllowDBNull = True
        Col8.Caption = "RcvAmt"
        Col8.ColumnName = "RcvAmt"
        Col8.DefaultValue = 0
        tblStkRpt.Columns.Add(Col8)

        Col9.DataType = System.Type.GetType("System.Decimal")
        Col9.AllowDBNull = True
        Col9.Caption = "SalesQty"
        Col9.ColumnName = "SalesQty"
        Col9.DefaultValue = 0
        tblStkRpt.Columns.Add(Col9)

        Col10.DataType = System.Type.GetType("System.Decimal")
        Col10.AllowDBNull = True
        Col10.Caption = "SalesAmt"
        Col10.ColumnName = "SalesAmt"
        Col10.DefaultValue = 0
        tblStkRpt.Columns.Add(Col10)

        Col11.DataType = System.Type.GetType("System.Decimal")
        Col11.AllowDBNull = True
        Col11.Caption = "AdjQty"
        Col11.ColumnName = "AdjQty"
        Col11.DefaultValue = 0
        tblStkRpt.Columns.Add(Col11)

        Col12.DataType = System.Type.GetType("System.Decimal")
        Col12.AllowDBNull = True
        Col12.Caption = "AdjAmt"
        Col12.ColumnName = "AdjAmt"
        Col12.DefaultValue = 0
        tblStkRpt.Columns.Add(Col12)

        Col13.DataType = System.Type.GetType("System.Decimal")
        Col13.AllowDBNull = True
        Col13.Caption = "BalCFQty"
        Col13.ColumnName = "BalCFQty"
        Col13.DefaultValue = 0
        tblStkRpt.Columns.Add(Col13)

        Col14.DataType = System.Type.GetType("System.Decimal")
        Col14.AllowDBNull = True
        Col14.Caption = "BalCFCost"
        Col14.ColumnName = "BalCFCost"
        Col14.DefaultValue = 0
        tblStkRpt.Columns.Add(Col14)

        Col15.DataType = System.Type.GetType("System.Decimal")
        Col15.AllowDBNull = True
        Col15.Caption = "BalCFAmt"
        Col15.ColumnName = "BalCFAmt"
        Col15.DefaultValue = 0
        tblStkRpt.Columns.Add(Col15)
    End Sub
End Class
