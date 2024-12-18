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

Public Class PU_StdRpt_PurchasesItemList_Preview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objPU As New agri.PU.clsReport()
    Dim objPUTrx As New agri.PU.clsTrx()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strUserLoc As String
    Dim strLocLevel As String

    Dim tempLoc As String
    Dim strDDLAccMth As String
    Dim strDDLAccYr As String
    Dim strExportToExcel As String

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

        strDDLAccMth = Request.QueryString("DDLAccMth")
        strDDLAccYr = Request.QueryString("DDLAccYr")

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_PUACCMONTH")
        strAccYear = Session("SS_PUACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        strLocLevel = Session("SS_LOCLEVEL")
        strExportToExcel = Trim(Request.QueryString("ExportToExcel"))

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

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        'Dim strOpCdPO_GET As String = "PU_STDRPT_PURCHASES_ITEM_GET"
        Dim strOpCdPO_GET As String = "PU_STDRPT_HISTORICAL_ITEM_GET"
        Dim strOpCdItem_GET As String = "PU_STDRPT_ITEM_GET"

        Dim strParam As String
        Dim strParamItm As String
        Dim strItemCode As String
        Dim strItemDesc As String
        Dim SearchStr As String
        Dim itemSelectStr As String
        Dim itemSearchStr As String
        Dim strUserLoc1 As String
        Dim MyPos As Integer
        Dim intCnt As Integer
        Dim intCntRem As Integer

        'Dim WildStr As String = " FROM PU_PO PO left outer join  PU_POLN POLN on PO.POID = POLN.POID left join in_pr pr on poln.prid = pr.prid "
        'Dim NormStr As String = " FROM PU_PO PO inner join  PU_POLN POLN on PO.POID = POLN.POID left join in_pr pr on poln.prid = pr.prid "

        If Not (Request.QueryString("DocDateFrom") = "" And Request.QueryString("DocDateTo") = "") Then
            SearchStr = SearchStr & "(DateDiff(Day, ''" & Request.QueryString("DocDateFrom") & "'', PO.CreateDate) >= 0) And (DateDiff(Day, ''" & Request.QueryString("DocDateTo") & "'', PO.CreateDate) <= 0) AND "
        End If

        If Not (Request.QueryString("DocNoFrom") = "" And Request.QueryString("DocNoTo") = "") Then
            SearchStr = SearchStr & "PO.POID IN (SELECT SUBPO.POID FROM PU_PO SUBPO WHERE SUBPO.POID >= '" & Request.QueryString("DocNoFrom") & _
                        "' AND SUBPO.POID <= ''" & Request.QueryString("DocNoTo") & "'') AND "
        End If

        If Not Request.QueryString("Supplier") = "" Then
            SearchStr = SearchStr & "(PO.SupplierCode LIKE ''%" & Request.QueryString("Supplier") & "%'' OR SPL.Name LIKE ''%" & Request.QueryString("Supplier") & "%'') AND "
        Else
            SearchStr = SearchStr & "PO.SupplierCode LIKE ''%'' AND "
        End If

        If Not Request.QueryString("AddNote") = "" Then
            SearchStr = SearchStr & "(POLN.AdditionalNote LIKE '%" & Request.QueryString("AddNote") & "%' OR POLN.Catatan LIKE ''%" & Request.QueryString("AddNote") & "%'') AND "
        End If

        If Not Request.QueryString("ProdType") = "" Then
            itemSelectStr = "ITM.ProdTypeCode LIKE ''" & Request.QueryString("ProdType") & "'' AND "
        Else
            itemSelectStr = "ITM.ProdTypeCode LIKE ''%'' AND "
        End If

        If Not Request.QueryString("ProdBrand") = "" Then
            itemSelectStr = itemSelectStr & "ITM.ProdBrandCode LIKE ''" & Request.QueryString("ProdBrand") & "'' AND "
        Else
            itemSelectStr = itemSelectStr & "ITM.ProdBrandCode LIKE ''%'' AND "
        End If

        If Not Request.QueryString("ProdModel") = "" Then
            itemSelectStr = itemSelectStr & "ITM.ProdModelCode LIKE ''" & Request.QueryString("ProdModel") & "'' AND "
        Else
            itemSelectStr = itemSelectStr & "ITM.ProdModelCode LIKE ''%'' AND "
        End If

        If Not Request.QueryString("ProdCat") = "" Then
            itemSelectStr = itemSelectStr & "ITM.ProdCatCode LIKE ''" & Request.QueryString("ProdCat") & "'' AND "
        Else
            itemSelectStr = itemSelectStr & "ITM.ProdCatCode LIKE ''%'' AND "
        End If

        If Not Request.QueryString("ProdMat") = "" Then
            itemSelectStr = itemSelectStr & "ITM.ProdMatCode LIKE ''" & Request.QueryString("ProdMat") & "'' AND "
        Else
            itemSelectStr = itemSelectStr & "ITM.ProdMatCode LIKE ''%'' AND "
        End If

        If Not Request.QueryString("StkAna") = "" Then
            itemSelectStr = itemSelectStr & "ITM.StockAnalysisCode LIKE ''" & Request.QueryString("StkAna") & "'' AND "
        Else
            itemSelectStr = itemSelectStr & "ITM.StockAnalysisCode LIKE ''%'' AND "
        End If

        If Not Request.QueryString("ItemCode") = "" Then
            itemSelectStr = itemSelectStr & "ITM.ItemCode LIKE ''" & Request.QueryString("ItemCode") & "'' AND "
        Else
            itemSelectStr = itemSelectStr & "ITM.ItemCode LIKE ''%'' AND "
        End If

        If Not Request.QueryString("ItemDesc") = "" Then
            itemSelectStr = itemSelectStr & "ITM.Description LIKE ''%" & Request.QueryString("ItemDesc") & "%'' AND "
        Else
            itemSelectStr = itemSelectStr & "ITM.Description LIKE ''%'' AND "
        End If

        If Request.QueryString("ProdType") = "" And Request.QueryString("ProdBrand") = "" And Request.QueryString("ProdModel") = "" And Request.QueryString("ProdCat") = "" And Request.QueryString("ProdMat") = "" And Request.QueryString("StkAna") = "" And Request.QueryString("ItemCode") = "" And Request.QueryString("ItemDesc") = "" Then
            itemSearchStr = "OR ( POLN.ItemCode = '''' )"
        End If

        If Not Request.QueryString("POType") = "" Then
            If Not Request.QueryString("POType") = objPUTrx.EnumPOType.All Then
                SearchStr = SearchStr & "PO.POType = ''" & Request.QueryString("POType") & "'' AND "
            Else
                SearchStr = SearchStr & "PO.POType LIKE ''%'' AND "
            End If
        End If

        If Not Request.QueryString("Status") = "" Then
            If Not Request.QueryString("Status") = objPUTrx.EnumPOStatus.All Then
                SearchStr = SearchStr & "PO.Status = ''" & Request.QueryString("Status") & "'' AND "
            Else
                SearchStr = SearchStr & "PO.Status NOT IN (''3'',''4'') AND "
                'SearchStr = SearchStr & "PO.Status LIKE ''%'' AND "
            End If
        End If


        '---REMARK FOR USE GETTING ALL COMPANY DATA
        'MyPos = InStr(strUserLoc, strLocation)
        'If MyPos > 0 Then
        '    Select Case strLocLevel
        '        Case "1" 'Estate
        '            strLocLevel = " WHERE LocLevel in ('1') "
        '        Case "2" 'Perwakilan
        '            strLocLevel = " WHERE LocLevel in ('1','2','4') "
        '        Case "3" 'HO
        '            strLocLevel = " WHERE LocLevel in ('1','2','3','4') "
        '        Case "4" 'Mill
        '            strLocLevel = " WHERE LocLevel in ('4') "
        '    End Select
        '    'If strLocLevel = "1" Then 'Mill/Estate
        '    '    strUserLoc1 = " WHERE PO.LocCode IN ('" & strUserLoc & "') "
        '    'End If
        '    'If strLocLevel = "2" Then 'RO
        '    '    strUserLoc1 = " where PR.LocLevel in ('1', '2') "
        '    'End If
        '    'If strLocLevel = "3" Then 'HO
        '    '    'strUserLoc1 = " where PR.LocLevel in ('1', '2', '3') " remark temporary coz dont have PR for any location
        '    '    strUserLoc1 = " where PO.LocCode IN ('" & strUserLoc & "') "
        '    'End If
        '    strUserLoc1 = " WHERE PO.LocCode IN ('" & strUserLoc & "') "
        'Else
        '    strUserLoc1 = " WHERE PO.LocCode IN ('" & strUserLoc & "') "
        'End If
        strUserLoc1 = " WHERE "

        Dim WildStr As String = " FROM PU_PO PO left outer join  PU_POLN POLN on PO.POID = POLN.POID left join (select prid, loclevel from in_pr " & strLocLevel & ") pr on poln.prid = pr.prid "
        Dim NormStr As String = " FROM PU_PO PO inner join  PU_POLN POLN on PO.POID = POLN.POID left join (select prid, loclevel from in_pr " & strLocLevel & ") pr on poln.prid = pr.prid "

        If Not SearchStr = "" Or Not itemSelectStr = "" Then
            If Right(SearchStr, 4) = "AND " Then
                SearchStr = Left(SearchStr, Len(SearchStr) - 4)
            End If

            If Right(itemSelectStr, 4) = "AND " Then
                itemSelectStr = Left(itemSelectStr, Len(itemSelectStr) - 4)
            End If

            If Not Request.QueryString("ItemCode") = "" Then
                strParam = strUserLoc1 & "|" & strDDLAccMth & "|" & strDDLAccYr & "|" & itemSelectStr & "|" & itemSearchStr & "|" & SearchStr & "|" & NormStr
            Else
                strParam = strUserLoc1 & "|" & strDDLAccMth & "|" & strDDLAccYr & "|" & itemSelectStr & "|" & itemSearchStr & "|" & SearchStr & "|" & WildStr
            End If

        End If

        Try
            intErrNo = objPU.mtdGetReport_POList(strOpCdPO_GET, strParam, objRptDs, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_STDRPT_PO_LIST&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objRptDs.Tables(0).Rows.Count - 1
            If Not IsDBNull(objRptDs.Tables(0).Rows(intCnt).Item("ItemCode")) Then
                strItemCode = Trim(objRptDs.Tables(0).Rows(intCnt).Item("ItemCode"))
                strParamItm = strItemCode & "|"
                Try
                    intErrNo = objPU.mtdGetItem(strOpCdItem_GET, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strAccMonth, _
                                                strAccYear, _
                                                strParamItm, _
                                                objItem)
                Catch Exp As System.Exception
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_ITEM_FOR_POLIST_REPORT&errmesg=" & lblErrMessage.Text & "&redirect=")
                End Try

                If objItem.Tables(0).Rows.Count > 0 Then
                    objRptDs.Tables(0).Rows(intCnt).Item("ItemDesc") = Trim(objItem.Tables(0).Rows(0).Item("Description"))
                    'objRptDs.Tables(0).Rows(intCnt).Item("PurchaseUOM") = Trim(objItem.Tables(0).Rows(0).Item("PurchaseUOM"))
                End If
            End If
        Next intCnt

        If Not Request.QueryString("ItemCode") = "" Or Not Request.QueryString("ItemDesc") = "" Or Not Request.QueryString("ProdType") = "" Or Not Request.QueryString("ProdBrand") = "" Or Not _
            Request.QueryString("ProdModel") = "" Or Not Request.QueryString("ProdCat") = "" Or Not Request.QueryString("ProdMat") = "" Or Not Request.QueryString("StkAna") = "" Then
            If objRptDs.Tables(0).Rows.Count > 0 Then
                Do
                    If IsDBNull(objRptDs.Tables(0).Rows(intCntRem).Item("POLnID")) Then
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
        Else
            If objRptDs.Tables(0).Rows.Count > 0 Then
                Do
                    If IsDBNull(objRptDs.Tables(0).Rows(intCntRem).Item("POLnID")) Then
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
        End If


        Dim strHistoryBy As String
        Dim strRptPrefix As String

        strHistoryBy = Request.QueryString("HistoryBy")
        If strHistoryBy = "1" Then
            strRptPrefix = "PU_StdRpt_HistoricalItemPriceByItem"
        Else
            strRptPrefix = "PU_StdRpt_HistoricalItemPriceBySupplier"
        End If

        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strRptPrefix & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        crvView.Visible = False
        crvView.ReportSource = rdCrystalViewer
        crvView.DataBind()

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        If strExportToExcel = "0" Then
            crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strRptPrefix & ".pdf"
        Else
            crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strRptPrefix & ".xls"
        End If

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile

            If strExportToExcel = "0" Then
                .ExportFormatType = ExportFormatType.PortableDocFormat
            Else
                .ExportFormatType = ExportFormatType.Excel
            End If

            '.ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        rdCrystalViewer.Close()
        rdCrystalViewer.Dispose()

        If strExportToExcel = "0" Then
            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strRptPrefix & ".pdf"">")
        Else
            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strRptPrefix & ".xls"">")
        End If
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

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("ParamLocation")
        paramField2 = paramFields.Item("ParamRptID")
        paramField3 = paramFields.Item("ParamRptName")
        paramField4 = paramFields.Item("ParamCompanyName")
        paramField5 = paramFields.Item("ParamUserName")
        paramField6 = paramFields.Item("ParamDecimal")
        paramField7 = paramFields.Item("ParamAccMonth")
        paramField8 = paramFields.Item("ParamAccYear")
        paramField9 = paramFields.Item("ParamSupplier")
        paramField10 = paramFields.Item("ParamDocNoFrom")
        paramField11 = paramFields.Item("ParamDocNoTo")
        paramField12 = paramFields.Item("ParamDocDateFrom")
        paramField13 = paramFields.Item("ParamDocDateTo")
        paramField14 = paramFields.Item("ParamProdType")
        paramField15 = paramFields.Item("ParamProdBrand")
        paramField16 = paramFields.Item("ParamProdModel")
        paramField17 = paramFields.Item("ParamProdCat")
        paramField18 = paramFields.Item("ParamProdMat")
        paramField19 = paramFields.Item("ParamStkAna")
        paramField20 = paramFields.Item("ParamItemCode")
        paramField21 = paramFields.Item("ParamPOType")
        paramField22 = paramFields.Item("ParamStatus")
        paramField23 = paramFields.Item("lblLocation")
        paramField24 = paramFields.Item("lblProdTypeCode")
        paramField25 = paramFields.Item("lblProdBrandCode")
        paramField26 = paramFields.Item("lblProdModelCode")
        paramField27 = paramFields.Item("lblProdCatCode")
        paramField28 = paramFields.Item("lblProdMatCode")
        paramField29 = paramFields.Item("lblStkAnaCode")
        paramField30 = paramFields.Item("ParamItemDesc")

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

        ParamDiscreteValue1.Value = Session("SS_LOC")
        ParamDiscreteValue2.Value = Request.QueryString("RptID")
        ParamDiscreteValue3.Value = Request.QueryString("RptName")
        ParamDiscreteValue4.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue5.Value = Session("SS_USERNAME")
        ParamDiscreteValue6.Value = Request.QueryString("Decimal")
        ParamDiscreteValue7.Value = Request.QueryString("DDLAccMth")
        ParamDiscreteValue8.Value = Request.QueryString("DDLAccYr")
        ParamDiscreteValue9.Value = Request.QueryString("Supplier")
        ParamDiscreteValue10.Value = Request.QueryString("DocNoFrom")
        ParamDiscreteValue11.Value = Request.QueryString("DocNoTo")
        ParamDiscreteValue12.Value = Request.QueryString("DocDateFrom")
        ParamDiscreteValue13.Value = Request.QueryString("DocDateTo")
        ParamDiscreteValue14.Value = Request.QueryString("ProdType")
        ParamDiscreteValue15.Value = Request.QueryString("ProdBrand")
        ParamDiscreteValue16.Value = Request.QueryString("ProdModel")
        ParamDiscreteValue17.Value = Request.QueryString("ProdCat")
        ParamDiscreteValue18.Value = Request.QueryString("ProdMat")
        ParamDiscreteValue19.Value = Request.QueryString("StkAna")
        ParamDiscreteValue20.Value = Request.QueryString("ItemCode")
        ParamDiscreteValue21.Value = Request.QueryString("POType")
        ParamDiscreteValue22.Value = Request.QueryString("Status")
        ParamDiscreteValue23.Value = Request.QueryString("lblLocation")
        ParamDiscreteValue24.Value = Request.QueryString("lblProdTypeCode")
        ParamDiscreteValue25.Value = Request.QueryString("lblProdBrandCode")
        ParamDiscreteValue26.Value = Request.QueryString("lblProdModelCode")
        ParamDiscreteValue27.Value = Request.QueryString("lblProdCatCode")
        ParamDiscreteValue28.Value = Request.QueryString("lblProdMatCode")
        ParamDiscreteValue29.Value = Request.QueryString("lblStkAnaCode")
        ParamDiscreteValue30.Value = Request.QueryString("ItemDesc")

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
    End Sub
End Class
