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

Public Class CT_StdRpt_MthlyIssueForPayrollList_Preview : Inherits Page
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

    Dim intDecimal As Integer
    Dim tempLoc As String
    Dim strQueryDisplay As String

    Dim strDisplay As String
    Dim selectStr As String
    Dim fromStr As String
    Dim whereStr As String
    Dim groupOrderByStr As String

    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer

    Dim tblCat As DataTable = New DataTable("tblCat")
    Dim dr As DataRow

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

        intDecimal = Request.QueryString("Decimal")
        strQueryDisplay = Request.QueryString("Display")

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
            Display()
            BindReport()
        End If
    End Sub

    Sub Display()
        If strQueryDisplay = "Type" Then
            strDisplay = UCase(Left(Request.QueryString("lblProdTypeCode"), Len(Request.QueryString("lblProdTypeCode")) - 6))
            selectStr = "PTYPE.ProdTypeCode as ProdCode, PType.Description as ProdDesc, "
            fromStr = "IN_PRODTYPE PTYPE"
            whereStr = "AND PTYPE.ProdTypeCode = ITM.ProdTypeCode"
            groupOrderByStr = "PTYPE.ProdTypeCode, PTYPE.Description, "
        ElseIf strQueryDisplay = "Brand" Then
            strDisplay = UCase(Left(Request.QueryString("lblProdBrandCode"), Len(Request.QueryString("lblProdBrandCode")) - 6))
            selectStr = "PBRAND.ProdBrandCode as ProdCode, PBRAND.Description as ProdDesc, "
            fromStr = "IN_PRODBRAND PBRAND"
            whereStr = "AND PBRAND.ProdBrandCode = ITM.ProdBrandCode"
            groupOrderByStr = "PBRAND.ProdBrandCode, PBRAND.Description, "
        ElseIf strQueryDisplay = "Model" Then
            strDisplay = UCase(Left(Request.QueryString("lblProdModelCode"), Len(Request.QueryString("lblProdModelCode")) - 6))
            selectStr = "PMODEL.ProdModelCode as ProdCode, PMODEL.Description as ProdDesc, "
            fromStr = "IN_PRODMODEL PMODEL"
            whereStr = "AND PMODEL.ProdModelCode = ITM.ProdModelCode"
            groupOrderByStr = "PMODEL.ProdModelCode, PMODEL.Description, "
        ElseIf strQueryDisplay = "Category" Then
            strDisplay = UCase(Left(Request.QueryString("lblProdCatCode"), Len(Request.QueryString("lblProdCatCode")) - 6))
            selectStr = "PCAT.ProdCatCode as ProdCode, PCAT.Description as ProdDesc, "
            fromStr = "IN_PRODCAT PCAT"
            whereStr = "AND PCAT.ProdCatCode = ITM.ProdCatCode"
            groupOrderByStr = "PCAT.ProdCatCode, PCAT.Description, "
        ElseIf strQueryDisplay = "Material" Then
            strDisplay = UCase(Left(Request.QueryString("lblProdMatCode"), Len(Request.QueryString("lblProdMatCode")) - 6))
            selectStr = "PMAT.ProdMatCode as ProdCode, PMAT.Description as ProdDesc, "
            fromStr = "IN_PRODMAT PMAT"
            whereStr = "AND PMAT.ProdMatCode = ITM.ProdMatCode"
            groupOrderByStr = "PMAT.ProdMatCode, PMAT.Description, "
        ElseIf strQueryDisplay = "StkAna" Then
            strDisplay = UCase(Left(Request.QueryString("lblStkAnaCode"), Len(Request.QueryString("lblStkAnaCode")) - 6))
            selectStr = "STKANA.StockAnalysisCode as ProdCode, STKANA.Description as ProdDesc, "
            fromStr = "IN_STOCKANALYSIS STKANA"
            whereStr = "AND STKANA.StockAnalysisCode = ITM.StockAnalysisCode"
            groupOrderByStr = "STKANA.StockAnalysisCode, STKANA.Description, "
        End If
    End Sub

    Sub createCols()
        Dim Col1 As DataColumn = New DataColumn()
        Dim Col2 As DataColumn = New DataColumn()
        Dim Col3 As DataColumn = New DataColumn()
        Dim Col4 As DataColumn = New DataColumn()
        Dim Col5 As DataColumn = New DataColumn()
        Dim Col6 As DataColumn = New DataColumn()

        Col1.DataType = System.Type.GetType("System.String")
        Col1.AllowDBNull = True
        Col1.Caption = "LocCode"
        Col1.ColumnName = "LocCode"
        Col1.DefaultValue = ""
        tblCat.Columns.Add(Col1)

        Col2.DataType = System.Type.GetType("System.String")
        Col2.AllowDBNull = True
        Col2.Caption = "Type"
        Col2.ColumnName = "Type"
        Col2.DefaultValue = ""
        tblCat.Columns.Add(Col2)

        Col3.DataType = System.Type.GetType("System.String")
        Col3.AllowDBNull = True
        Col3.Caption = "EmpCode"
        Col3.ColumnName = "EmpCode"
        Col3.DefaultValue = ""
        tblCat.Columns.Add(Col3)

        Col4.DataType = System.Type.GetType("System.String")
        Col4.AllowDBNull = True
        Col4.Caption = "ProdCat"
        Col4.ColumnName = "ProdCat"
        Col4.DefaultValue = ""
        tblCat.Columns.Add(Col4)

        Col5.DataType = System.Type.GetType("System.Decimal")
        Col5.AllowDBNull = True
        Col5.Caption = "Amount"
        Col5.ColumnName = "Amount"
        Col5.DefaultValue = 0
        tblCat.Columns.Add(Col5)

        Col6.DataType = System.Type.GetType("System.Decimal")
        Col6.AllowDBNull = True
        Col6.Caption = "Total"
        Col6.ColumnName = "Total"
        Col6.DefaultValue = 0
        tblCat.Columns.Add(Col6)
    End Sub

    Sub BindReport()

        Dim objRptDs As New DataSet()
        Dim objRptDsCat As New DataSet()
        Dim objMapPath As String

        Dim intCnt As Integer
        Dim intCntCat As Integer
        Dim intInsert As Integer
        Dim intCntInsCat As Integer
        Dim intCntCatList As Integer

        Dim decTotal As Decimal
        Dim decLocTotal As Decimal
        Dim decGrandTotal As Decimal

        Dim strEmpCode As String
        Dim strLocCode As String
        Dim SearchStr As String
        Dim strParam As String
        Dim strParamCat As String

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOpCdStkIssue_GET As String = "CT_STDRPT_MONTHLY_ISSUETRANSFER_FOR_PAYROLL_GET"
        Dim strOpCdStkIssueCat_GET As String = "CT_STDRPT_MONTHLY_ISSUETRANSFER_FOR_PAYROLL_CATEGORY_GET"

        If Not Request.QueryString("ProdType") = "" Then
            SearchStr = "AND ITM.ProdTypeCode LIKE '" & Request.QueryString("ProdType") & "' AND "
        Else
            SearchStr = "AND ITM.ProdTypeCode LIKE '%' AND "
        End If

        If Not Request.QueryString("ProdBrand") = "" Then
            SearchStr = SearchStr & "ITM.ProdBrandCode LIKE '" & Request.QueryString("ProdBrand") & "' AND "
        Else
            SearchStr = SearchStr & "ITM.ProdBrandCode LIKE '%' AND "
        End If

        If Not Request.QueryString("ProdModel") = "" Then
            SearchStr = SearchStr & "ITM.ProdModelCode LIKE '" & Request.QueryString("ProdModel") & "' AND "
        Else
            SearchStr = SearchStr & "ITM.ProdModelCode LIKE '%' AND "
        End If

        If Not Request.QueryString("ProdCat") = "" Then
            SearchStr = SearchStr & "ITM.ProdCatCode LIKE '" & Request.QueryString("ProdCat") & "' AND "
        Else
            SearchStr = SearchStr & "ITM.ProdCatCode LIKE '%' AND "
        End If

        If Not Request.QueryString("ProdMat") = "" Then
            SearchStr = SearchStr & "ITM.ProdMatCode LIKE '" & Request.QueryString("ProdMat") & "' AND "
        Else
            SearchStr = SearchStr & "ITM.ProdMatCode LIKE '%' AND "
        End If

        If Not Request.QueryString("StkAna") = "" Then
            SearchStr = SearchStr & "ITM.StockAnalysisCode LIKE '" & Request.QueryString("StkAna") & "' AND "
        Else
            SearchStr = SearchStr & "ITM.StockAnalysisCode LIKE '%' AND "
        End If

        If Not Request.QueryString("ItemCode") = "" Then
            SearchStr = SearchStr & "STKISSLN.ItemCode LIKE '" & Request.QueryString("ItemCode") & "' AND "
        Else
            SearchStr = SearchStr & "STKISSLN.ItemCode LIKE '%' AND "
        End If

        If Not SearchStr = "" Then
            If Right(SearchStr, 4) = "AND " Then
                SearchStr = Left(SearchStr, Len(SearchStr) - 4)
            End If
        End If

        strParam = strUserLoc & "|" & Request.QueryString("DDLAccMth") & "|" & Request.QueryString("DDLAccYr") & "|" & _
                   objCTTrx.EnumStockIssueStatus.Confirmed & "','" & objCTTrx.EnumStockIssueStatus.Closed & "','" & objCTTrx.EnumStockIssueStatus.DBNote & "|" & _
                   objCTTrx.EnumStockIssueType.StaffPayroll & "|" & SearchStr
        Try
            intErrNo = objCT.mtdGetReport_MthlyIssueForPayrollList(strOpCdStkIssue_GET, strParam, objRptDs, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_MTHLY_ISSUE_FOR_PAYROLL_LIST&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        While intCnt < objRptDs.Tables(0).Rows.Count
            strEmpCode = Trim(objRptDs.Tables(0).Rows(intCnt).Item("EmpCode"))
            strLocCode = Trim(objRptDs.Tables(0).Rows(intCnt).Item("LocCode"))

            dr = objRptDs.Tables(0).NewRow
            dr("LocCode") = strLocCode
            dr("Type") = "LBLCAT"
            dr("EmpCode") = strEmpCode
            dr("EmpName") = Chr(9) & strDisplay
            dr("Amount") = 0
            dr("Total") = 0
            intCnt = intCnt + 1
            objRptDs.Tables(0).Rows.InsertAt(dr, intCnt)
            intCnt += 1

            strParamCat = selectStr & "|" & fromStr & "|" & whereStr & "|" & strEmpCode & "|" & strUserLoc & "|" & _
                          Request.QueryString("DDLAccMth") & "|" & Request.QueryString("DDLAccYr") & "|" & _
                          objCTTrx.EnumStockIssueStatus.Confirmed & "','" & objCTTrx.EnumStockIssueStatus.Closed & "','" & objCTTrx.EnumStockIssueStatus.DBNote & "|" & _
                          objCTTrx.EnumStockIssueType.StaffPayroll & "|" & _
                          objCTSetup.EnumInventoryItemType.CanteenItem & "|" & SearchStr & "|" & groupOrderByStr & "|" & groupOrderByStr
            Try
                intErrNo = objCT.mtdGetMthlyIssueForPayrollCatList(strOpCdStkIssueCat_GET, strParamCat, objRptDsCat, objMapPath)
            Catch Exp As System.Exception
                Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_MTHLY_ISSUE_FOR_PAYROLL_CAT_LIST&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try

            For intCntCatList = 0 To objRptDsCat.Tables(0).Rows.Count - 1
                dr = objRptDs.Tables(0).NewRow
                dr("LocCode") = strLocCode
                dr("Type") = "PCAT"
                dr("EmpCode") = strEmpCode
                dr("EmpName") = Chr(9) & Trim(objRptDsCat.Tables(0).Rows(intCntCatList).Item("ProdCode")) & " (" & Trim(objRptDsCat.Tables(0).Rows(intCntCatList).Item("ProdDesc")) & ")"
                dr("Amount") = FormatNumber(Trim(objRptDsCat.Tables(0).Rows(intCntCatList).Item("Amount")), intDecimal)
                dr("Total") = 0
                decTotal = decTotal + Trim(objRptDsCat.Tables(0).Rows(intCntCatList).Item("Amount"))
                objRptDs.Tables(0).Rows.InsertAt(dr, intCnt)
                intCnt += 1
            Next
            objRptDs.Tables(0).Rows(intCnt - 1).BeginEdit()
            objRptDs.Tables(0).Rows(intCnt - 1).Item("Type") = "EMPTOTAL"
            objRptDs.Tables(0).Rows(intCnt - 1).Item("Total") = FormatNumber(decTotal, intDecimal)
            objRptDs.Tables(0).Rows(intCnt - 1).EndEdit()

            decLocTotal = decLocTotal + decTotal
            decTotal = 0

        End While

        dr = objRptDs.Tables(0).NewRow
        dr("LocCode") = strLocCode
        dr("Type") = "BL"
        dr("EmpCode") = ""
        dr("EmpName") = ""
        dr("Amount") = 0
        dr("Total") = 0
        objRptDs.Tables(0).Rows.InsertAt(dr, objRptDs.Tables(0).Rows.Count)

        intCnt = 0
        For intCnt = 0 To objRptDs.Tables(0).Rows.Count - 1
            If Not intCnt = objRptDs.Tables(0).Rows.Count - 1 Then
                If Not Trim(objRptDs.Tables(0).Rows(intCnt + 1).Item("LocCode")) = Trim(objRptDs.Tables(0).Rows(intCnt).Item("LocCode")) Then

                    dr = objRptDs.Tables(0).NewRow
                    dr("LocCode") = strLocCode
                    dr("Type") = "LT"
                    dr("EmpCode") = UCase(Request.QueryString("lblLocation")) & " TOTAL :"
                    dr("EmpName") = ""
                    dr("Amount") = 0
                    dr("Total") = FormatNumber(decLocTotal, intDecimal)
                    objRptDs.Tables(0).Rows.InsertAt(dr, objRptDs.Tables(0).Rows.Count)
                    decGrandTotal = decGrandTotal + decLocTotal
                End If
            Else
                dr = objRptDs.Tables(0).NewRow
                dr("LocCode") = strLocCode
                dr("Type") = "LT"
                dr("EmpCode") = UCase(Request.QueryString("lblLocation")) & " TOTAL :"
                dr("EmpName") = ""
                dr("Amount") = 0
                dr("Total") = FormatNumber(decLocTotal, intDecimal)
                objRptDs.Tables(0).Rows.InsertAt(dr, objRptDs.Tables(0).Rows.Count)
                decGrandTotal = decGrandTotal + decLocTotal
            End If
        Next

        dr = objRptDs.Tables(0).NewRow
        dr("LocCode") = strLocCode
        dr("Type") = "BL"
        dr("EmpCode") = ""
        dr("EmpName") = ""
        dr("Amount") = 0
        dr("Total") = 0
        objRptDs.Tables(0).Rows.InsertAt(dr, objRptDs.Tables(0).Rows.Count)

        dr = objRptDs.Tables(0).NewRow
        dr("LocCode") = strLocCode
        dr("Type") = "GT"
        dr("EmpCode") = "GRAND TOTAL :"
        dr("EmpName") = ""
        dr("Amount") = 0
        dr("Total") = FormatNumber(decGrandTotal, intDecimal)
        objRptDs.Tables(0).Rows.InsertAt(dr, objRptDs.Tables(0).Rows.Count)


        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\CT_StdRpt_MthlyIssueForPayrollList.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3

        crvView.Visible = False                         
        crvView.ReportSource = rdCrystalViewer          
        crvView.DataBind()                              

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\CT_StdRpt_MthlyIssueForPayrollList.pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/CT_StdRpt_MthlyIssueForPayrollList.pdf"">")
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
        paramField23 = paramFields.Item("ParamDisplay")

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
        ParamDiscreteValue23.Value = strDisplay

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
End Class
