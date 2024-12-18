Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic 
Imports Microsoft.VisualBasic.Strings
Imports System.Xml
Imports System.Web.Services
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web


Public Class CT_StdRpt_InventoryValList_Preview : Inherits Page
    Protected objAdmin As New agri.Admin.clsShare()

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objCT As New agri.CT.clsReport()
    Dim objCTSetup As New agri.CT.clsSetup()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strUserLoc As String

    Dim tempLoc As String
    Dim intDecimal As Integer

    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        crvView.Visible = False

        If Right(Request.QueryString("Location"), 1) = "," Then
            strUserLoc = Left(Request.QueryString("Location"), Len(Request.QueryString("Location")) - 1)
        Else
            strUserLoc = Trim(Request.QueryString("Location"))
        End If

        intDecimal = Request.QueryString("Decimal")

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        strLangCode = Session("SS_LANGCODE")

        Session("SS_lblProdTypeCode") = Request.QueryString("lblProdTypeCode")
        Session("SS_lblProdBrandCode") = Request.QueryString("lblProdBrandCode")
        Session("SS_lblProdModelCode") = Request.QueryString("lblProdModelCode")
        Session("SS_lblProdCatCode") = Request.QueryString("lblProdCatCode")
        Session("SS_lblProdMatCode") = Request.QueryString("lblProdMatCode")
        Session("SS_lblStkAnaCode") = Request.QueryString("lblStkAnaCode")
        Session("SS_lblLocation") = Request.QueryString("lblLocation")

        Session("SS_RptID") = Request.QueryString("RptID")
        Session("SS_RptName") = Request.QueryString("RptName")
        Session("SS_Decimal") = Request.QueryString("Decimal")
        Session("SS_Supp") = Request.QueryString("Supp")
        Session("SS_OrderBy") = Request.QueryString("GrpBy")
        Session("SS_ProdType") = Request.QueryString("ProdType")
        Session("SS_ProdBrand") = Request.QueryString("ProdBrand")
        Session("SS_ProdModel") = Request.QueryString("ProdModel")
        Session("SS_ProdCat") = Request.QueryString("ProdCat")
        Session("SS_ProdMat") = Request.QueryString("ProdMat")
        Session("SS_StkAna") = Request.QueryString("StkAna")
        Session("SS_ItemCode") = Request.QueryString("ItemCode")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            BindReport()
        End If
    End Sub

    Sub BindReport()

        Dim objRptDs As New DataSet()
        Dim objMapPath As String

        Dim intCnt As Integer

        Dim SearchStr As String
        Dim strParam As String
        Dim strParamOrderBy As String

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOpCd_InventoryVal_GET As String = "CT_STDRPT_INVENTORYVAL_GET"


        tempLoc = Request.QueryString("Location")
        If Right(tempLoc, 1) = "," Then
            Session("SS_LOC") = Left(tempLoc, Len(tempLoc) - 1)
        Else
            Session("SS_LOC") = tempLoc.Replace("','", ", ")
        End If

        If Request.QueryString("GrpBy") = "Type" Then
            strParamOrderBy = "ITM.ProdTypeCode"
        ElseIf Request.QueryString("GrpBy") = "Brand" Then
            strParamOrderBy = "ITM.ProdBrandCode"
        ElseIf Request.QueryString("GrpBy") = "Model" Then
            strParamOrderBy = "ITM.ProdModelCode"
        ElseIf Request.QueryString("GrpBy") = "Category" Then
            strParamOrderBy = "ITM.ProdCatCode"
        ElseIf Request.QueryString("GrpBy") = "Material" Then
            strParamOrderBy = "ITM.ProdMatCode"
        ElseIf Request.QueryString("GrpBy") = "StkAna" Then
            strParamOrderBy = "ITM.StockAnalysisCode"
        End If

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
            SearchStr = SearchStr & "ITM.ItemCode LIKE '" & Request.QueryString("ItemCode") & "'"
        Else
            SearchStr = SearchStr & "ITM.ItemCode LIKE '%'"
        End If


        If Not SearchStr = "" Then
            If Right(SearchStr, 4) = "AND " Then
                SearchStr = Left(SearchStr, Len(SearchStr) - 4)
            End If

            strParam = objCTSetup.EnumInventoryItemType.CanteenItem & "|" & strUserLoc & "|" & SearchStr
        End If

        Try
            intErrNo = objCT.mtdGetReport_InventoryValList(strOpCd_InventoryVal_GET, _
                                                           intDecimal, _
                                                           strParam, _
                                                           strParamOrderBy, _
                                                           objRptDs, _
                                                           objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_CT_INVENTORYVAL_LIST_REPORT&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objRptDs.Tables(0).Rows.Count - 1
            objRptDs.Tables(0).Rows(intCnt).Item("TotalCost") = RoundNumber(RoundNumber(RoundNumber(RoundNumber(objRptDs.Tables(0).Rows(intCnt).Item("QtyOnHand"), 5) + RoundNumber(objRptDs.Tables(0).Rows(intCnt).Item("QtyOnHold"), 5), 5) * RoundNumber(objRptDs.Tables(0).Rows(intCnt).Item("AverageCost"), 5), 5) + RoundNumber(objRptDs.Tables(0).Rows(intCnt).Item("DiffAvgCost"), 5), 5)
        Next


        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\CT_StdRpt_InventoryValList.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3

        crvView.Visible = False                         
        crvView.ReportSource = rdCrystalViewer          
        crvView.DataBind()                              

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\CT_StdRpt_InventoryValList.pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/CT_StdRpt_InventoryValList.pdf"">")
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

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("ParamCompanyName")
        paramField2 = paramFields.Item("ParamLocation")
        paramField3 = paramFields.Item("ParamUserName")
        paramField4 = paramFields.Item("ParamDecimal")
        paramField5 = paramFields.Item("ParamSuppress")
        paramField6 = paramFields.Item("ParamOrderBy")
        paramField7 = paramFields.Item("ParamProdType")
        paramField8 = paramFields.Item("ParamProdBrand")
        paramField9 = paramFields.Item("ParamProdModel")
        paramField10 = paramFields.Item("ParamProdCat")
        paramField11 = paramFields.Item("ParamProdMat")
        paramField12 = paramFields.Item("ParamStkAna")
        paramField13 = paramFields.Item("ParamItemCode")
        paramField14 = paramFields.Item("ParamAccMonth")
        paramField15 = paramFields.Item("ParamAccYear")
        paramField16 = paramFields.Item("ParamRptID")
        paramField17 = paramFields.Item("ParamRptName")
        paramField18 = paramFields.Item("lblProdTypeCode")
        paramField19 = paramFields.Item("lblProdBrandCode")
        paramField20 = paramFields.Item("lblProdModelCode")
        paramField21 = paramFields.Item("lblProdCatCode")
        paramField22 = paramFields.Item("lblProdMatCode")
        paramField23 = paramFields.Item("lblStkAnaCode")
        paramField24 = paramFields.Item("lblLocation")

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

        ParamDiscreteValue1.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue2.Value = Session("SS_LOC")
        ParamDiscreteValue3.Value = Session("SS_USERNAME")
        ParamDiscreteValue4.Value = Session("SS_DECIMAL")
        ParamDiscreteValue5.Value = Session("SS_SUPP")
        ParamDiscreteValue6.Value = Session("SS_ORDERBY")
        ParamDiscreteValue7.Value = Session("SS_PRODTYPE")
        ParamDiscreteValue8.Value = Session("SS_PRODBRAND")
        ParamDiscreteValue9.Value = Session("SS_PRODMODEL")
        ParamDiscreteValue10.Value = Session("SS_PRODCAT")
        ParamDiscreteValue11.Value = Session("SS_PRODMAT")
        ParamDiscreteValue12.Value = Session("SS_STKANA")
        ParamDiscreteValue13.Value = Session("SS_ITEMCODE")
        ParamDiscreteValue14.Value = Session("SS_INACCMONTH")
        ParamDiscreteValue15.Value = Session("SS_INACCYEAR")
        ParamDiscreteValue16.Value = Session("SS_RPTID")
        ParamDiscreteValue17.Value = Session("SS_RPTNAME")
        ParamDiscreteValue18.Value = Session("SS_LBLPRODTYPECODE")
        ParamDiscreteValue19.Value = Session("SS_LBLPRODBRANDCODE")
        ParamDiscreteValue20.Value = Session("SS_LBLPRODMODELCODE")
        ParamDiscreteValue21.Value = Session("SS_LBLPRODCATCODE")
        ParamDiscreteValue22.Value = Session("SS_LBLPRODMATCODE")
        ParamDiscreteValue23.Value = Session("SS_LBLSTKANACODE")
        ParamDiscreteValue24.Value = Session("SS_LBLLOCATION")

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

        crvView.ParameterFieldInfo = paramFields
    End Sub

    Public Function RoundNumber(ByVal d As Double, Optional ByVal decimals As Double = 0) As Double
        RoundNumber = Decimal.Divide(Int(Decimal.Add(Decimal.Multiply(d, 10.0 ^ decimals), 0.5)), 10.0 ^ decimals)
    End Function
End Class
