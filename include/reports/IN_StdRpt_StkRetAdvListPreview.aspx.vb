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

Public Class IN_StdRpt_StkRetAdvList_Preview : Inherits Page
    Dim objIN As New agri.IN.clsReport()
    Dim objINSetup As New agri.IN.clsSetup()
    Dim objINTrx As New agri.IN.clsTrx()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

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

        Session("SS_lblProdTypeCode") = Request.QueryString("lblProdTypeCode")
        Session("SS_lblProdBrandCode") = Request.QueryString("lblProdBrandCode")
        Session("SS_lblProdModelCode") = Request.QueryString("lblProdModelCode")
        Session("SS_lblProdCatCode") = Request.QueryString("lblProdCatCode")
        Session("SS_lblProdMatCode") = Request.QueryString("lblProdMatCode")
        Session("SS_lblStkAnaCode") = Request.QueryString("lblStkAnaCode")
        Session("SS_LBLLOCATION") = Request.QueryString("lblLocation")

        Session("SS_Decimal") = Request.QueryString("Decimal")
        Session("SS_RptID") = Request.QueryString("RptID")
        Session("SS_RptName") = Request.QueryString("RptName")
        Session("SS_DateFrom") = Request.QueryString("DateFrom")
        Session("SS_DateTo") = Request.QueryString("DateTo")
        Session("SS_AccMonth") = Request.QueryString("DDLAccMth")
        Session("SS_AccYear") = Request.QueryString("DDLAccYr")
        Session("SS_DocNoFrom") = Request.QueryString("DocNoFrom")
        Session("SS_DocNoTo") = Request.QueryString("DocNoTo")
        Session("SS_ProdType") = Request.QueryString("ProdType")
        Session("SS_ProdBrand") = Request.QueryString("ProdBrand")
        Session("SS_ProdModel") = Request.QueryString("ProdModel")
        Session("SS_ProdCat") = Request.QueryString("ProdCat")
        Session("SS_ProdMat") = Request.QueryString("ProdMat")
        Session("SS_StkAna") = Request.QueryString("StkAna")
        Session("SS_ItemCode") = Request.QueryString("ItemCode")
        Session("SS_Status") = Request.QueryString("Status")

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
        Dim itemSearchStr As String
        Dim tempLoc As String

        Dim strParam As String
        Dim strParamItm As String
        Dim strItemCode As String

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOpCdItemRetAdv_GET As String = "IN_STDRPT_ITEMRETADV_LN_GET"


        tempLoc = Request.QueryString("Location")
        If Right(tempLoc, 1) = "," Then
            Session("SS_LOC") = Left(tempLoc, Len(tempLoc) - 1)
        Else
            Session("SS_LOC") = tempLoc.Replace("','", ", ")
        End If

        If Not (Request.QueryString("DateFrom") = "" And Request.QueryString("DateTo") = "") Then
            SearchStr = "(DateDiff(Day, '" & Session("SS_DATEFROM") & "', IRA.CreateDate) >= 0) And (DateDiff(Day, '" & Session("SS_DATETO") & "', IRA.CreateDate) <= 0) AND "
        End If

        If Not (Request.QueryString("DocNoFrom") = "" And Request.QueryString("DocNoTo") = "") Then
            SearchStr = SearchStr & "IRA.ItemRetAdvID IN (SELECT SUBIRA.ItemRetAdvID FROM IN_ITEMRETADV SUBIRA WHERE SUBIRA.ItemRetAdvID >= '" & Request.QueryString("DocNoFrom") & _
                        "' AND SUBIRA.ItemRetAdvID <= '" & Request.QueryString("DocNoTo") & "') AND "
        End If

        If Not Request.QueryString("ProdType") = "" Then
           itemSelectStr = "AND ITM.ProdTypeCode LIKE '" & Request.QueryString("ProdType") & "' AND "
        Else
            itemSelectStr = "AND "
        End If

        If Not Request.QueryString("ProdBrand") = "" Then
            itemSelectStr = itemSelectStr & "ITM.ProdBrandCode LIKE '" & Request.QueryString("ProdBrand") & "' AND "
        End If

        If Not Request.QueryString("ProdModel") = "" Then
            itemSelectStr = itemSelectStr & "ITM.ProdModelCode LIKE '" & Request.QueryString("ProdModel") & "' AND "
        End If

        If Not Request.QueryString("ProdCat") = "" Then
            itemSelectStr = itemSelectStr & "ITM.ProdCatCode LIKE '" & Request.QueryString("ProdCat") & "' AND "
        End If

        If Not Request.QueryString("ProdMat") = "" Then
            itemSelectStr = itemSelectStr & "ITM.ProdMatCode LIKE '" & Request.QueryString("ProdMat") & "' AND "
        End If

        If Not Request.QueryString("StkAna") = "" Then
            itemSelectStr = itemSelectStr & "ITM.StockAnalysisCode LIKE '" & Request.QueryString("StkAna") & "' AND "
        End If

        If Not Request.QueryString("ItemCode") = "" Then
            itemSelectStr = itemSelectStr & "ITM.ItemCode LIKE '" & Request.QueryString("ItemCode") & "' AND "
        End If


        If Not Request.QueryString("Status") = "" Then
            If Not Request.QueryString("Status") = objINTrx.EnumStockRetAdvStatus.All Then
                SearchStr = SearchStr & "IRA.Status = '" & Request.QueryString("Status") & "' AND "
            Else
                SearchStr = SearchStr & "IRA.Status NOT LIKE ' ' AND "
            End If
        End If

        If Not SearchStr = "" Or Not itemSelectStr = "" Then
            If Right(SearchStr, 4) = "AND " Then
                SearchStr = Left(SearchStr, Len(SearchStr) - 4)
            End If

            If Right(itemSelectStr, 4) = "AND " Then
                itemSelectStr = Left(itemSelectStr, Len(itemSelectStr) - 4)
            End If

            strParam = strUserLoc & "|" & strDDLAccMth & "|" & strDDLAccYr & "|" & objGlobal.EnumDocType.StockReturnAdvice & "|" & itemSelectStr & "|" & SearchStr

        End If

        Try
            intErrNo = objIN.mtdGetReport_StkRetAdvList(strOpCdItemRetAdv_GET, strParam, objRptDs, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_STKRETADV_LIST_REPORT&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try




            If objRptDs.Tables(0).Rows.Count > 0 Then
                Do
                    If IsDBNull(objRptDs.Tables(0).Rows(intCntRem).Item("ItemRetAdvLnID")) Then
                        objRptDs.Tables(0).Rows.RemoveAt(intCntRem)

                        If intCntRem <> 0 Then
                            intCntRem = intCntRem - 1
                        Else
                            intCntRem = 0
                        End If
                    Else
                        intCntRem = intCntRem + 1
                    End If
                Loop While intCntRem <= objRptDs.Tables(0).Rows.Count - 1
            End If



        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\IN_StdRpt_StkRetAdvList.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3

        crvView.Visible = False                          
        crvView.ReportSource = rdCrystalViewer          
        crvView.DataBind()                              

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\IN_StdRpt_StkRetAdvList.pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/IN_StdRpt_StkRetAdvList.pdf"">")
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
        paramField7 = paramFields.Item("ParamDateFrom")
        paramField8 = paramFields.Item("ParamDateTo")
        paramField9 = paramFields.Item("ParamDocNoFrom")
        paramField10 = paramFields.Item("ParamDocNoTo")
        paramField11 = paramFields.Item("ParamItemCode")
        paramField12 = paramFields.Item("ParamStatus")
        paramField13 = paramFields.Item("lblLocation")
        paramField14 = paramFields.Item("ParamRptID")
        paramField15 = paramFields.Item("ParamRptName")
        paramField16 = paramFields.Item("ParamProdType")
        paramField17 = paramFields.Item("ParamProdBrand")
        paramField18 = paramFields.Item("ParamProdModel")
        paramField19 = paramFields.Item("ParamProdCat")
        paramField20 = paramFields.Item("ParamProdMat")
        paramField21 = paramFields.Item("ParamStkAna")
        paramField22 = paramFields.Item("lblProdTypeCode")
        paramField23 = paramFields.Item("lblProdBrandCode")
        paramField24 = paramFields.Item("lblProdModelCode")
        paramField25 = paramFields.Item("lblProdCatCode")
        paramField26 = paramFields.Item("lblProdMatCode")
        paramField27 = paramFields.Item("lblStkAnaCode")

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

        ParamDiscreteValue1.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue2.Value = Session("SS_LOC")
        ParamDiscreteValue3.Value = Session("SS_USERNAME")
        ParamDiscreteValue4.Value = Session("SS_ACCMONTH")
        ParamDiscreteValue5.Value = Session("SS_ACCYEAR")
        ParamDiscreteValue6.Value = Session("SS_DECIMAL")
        ParamDiscreteValue7.Value = Session("SS_DATEFROM")
        ParamDiscreteValue8.Value = Session("SS_DATETO")
        ParamDiscreteValue9.Value = Session("SS_DOCNOFROM")
        ParamDiscreteValue10.Value = Session("SS_DOCNOTO")
        ParamDiscreteValue11.Value = Session("SS_ITEMCODE")
        ParamDiscreteValue12.Value = Session("SS_STATUS")
        ParamDiscreteValue13.Value = Session("SS_LBLLOCATION")
        ParamDiscreteValue14.Value = Session("SS_RPTID")
        ParamDiscreteValue15.Value = Session("SS_RPTNAME")
        ParamDiscreteValue16.Value = Session("SS_PRODTYPE")
        ParamDiscreteValue17.Value = Session("SS_PRODBRAND")
        ParamDiscreteValue18.Value = Session("SS_PRODMODEL")
        ParamDiscreteValue19.Value = Session("SS_PRODCAT")
        ParamDiscreteValue20.Value = Session("SS_PRODMAT")
        ParamDiscreteValue21.Value = Session("SS_STKANA")
        ParamDiscreteValue22.Value = Session("SS_LBLPRODTYPECODE")
        ParamDiscreteValue23.Value = Session("SS_LBLPRODBRANDCODE")
        ParamDiscreteValue24.Value = Session("SS_LBLPRODMODELCODE")
        ParamDiscreteValue25.Value = Session("SS_LBLPRODCATCODE")
        ParamDiscreteValue26.Value = Session("SS_LBLPRODMATCODE")
        ParamDiscreteValue27.Value = Session("SS_LBLSTKANACODE")

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

        crvView.ParameterFieldInfo = paramFields
    End Sub
End Class
