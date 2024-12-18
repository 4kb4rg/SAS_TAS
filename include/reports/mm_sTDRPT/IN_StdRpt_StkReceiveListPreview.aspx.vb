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

Public Class IN_StdRpt_StkReceiveList_Preview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objIN As New agri.IN.clsReport()
    Dim objINSetup As New agri.IN.clsSetup()
    Dim objINTrx As New agri.IN.clsTrx()

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
        Session("SS_StkRcvIDFrom") = Request.QueryString("StkRcvIDFrom")
        Session("SS_StkRcvIDTo") = Request.QueryString("StkRcvIDTo")
        Session("SS_StkRRefNo") = Request.QueryString("StkRefNo")
        Session("SS_PRIDFrom") = Request.QueryString("PRIDFrom")
        Session("SS_PRIDTo") = Request.QueryString("PRIDTo")
        Session("SS_StkRcvType") = Request.QueryString("StkRcvType")
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

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOpCdStkRcv As String = "IN_STDRPT_STKRECEIVE_AND_STKRECEIVELN_GET"
        Dim strOpCdItem_GET As String = "IN_STDRPT_ITEM_GET"

        Dim strParam As String
        Dim strParamItm As String
        Dim strItemCode As String

        Dim WildStr As String = "AND STKRCV.StockReceiveID *= STKRCVLn.StockReceiveID AND "
        Dim NormStr As String = "AND STKRCV.StockReceiveID = STKRCVLn.StockReceiveID AND "

        If Not (Request.QueryString("DateFrom") = "" And Request.QueryString("DateTo") = "") Then
            SearchStr = "(DateDiff(Day, '" & Session("SS_DATEFROM") & "', STKRCV.StockRefDate) >= 0) And (DateDiff(Day, '" & Session("SS_DATETO") & "', STKRCV.StockRefDate) <= 0) AND "
        End If

        If Not (Request.QueryString("StkRcvIDFrom") = "" And Request.QueryString("StkRcvIDTo") = "") Then
            SearchStr = SearchStr & "STKRCV.StockReceiveID IN (SELECT SUBSTK.StockReceiveID FROM IN_STOCKRECEIVE SUBSTK WHERE SUBSTK.StockReceiveID >= '" & Request.QueryString("StkRcvIDFrom") & _
                        "' AND SUBSTK.StockReceiveID <= '" & Request.QueryString("StkRcvIDTo") & "') AND "
        End If

        If Not Request.QueryString("StkRefNo") = "" Then
            SearchStr = SearchStr & "STKRCV.StockRefNo LIKE '" & Request.QueryString("StkRefNo") & "' AND "
        Else
            SearchStr = SearchStr & "STKRCV.StockRefNo LIKE '%' AND "
        End If

        If Not (Request.QueryString("PRIDFrom") = "" And Request.QueryString("PRIDTo") = "") Then
            SearchStr = SearchStr & "STKRCVLn.DocID IN (SELECT SUBSTK.DocID FROM IN_STOCKRECEIVELN SUBSTK WHERE SUBSTK.DocID >= '" & Request.QueryString("PRIDFrom") & _
                        "' AND SUBSTK.DocID <= '" & Request.QueryString("PRIDTo") & "') AND "
        End If

        If Not Request.QueryString("StkRcvType") = "All" Then
            SearchStr = SearchStr & "STKRCV.StockDocType = '" & Request.QueryString("StkRcvType") & "' AND "
        Else
            SearchStr = SearchStr & "STKRCV.StockDocType LIKE '%' AND "
        End If

        If Not Request.QueryString("ProdType") = "" Then
            itemSelectStr = "ITM.ProdTypeCode LIKE '" & Request.QueryString("ProdType") & "' AND "
        Else
            itemSelectStr = "ITM.ProdTypeCode LIKE '%' AND "
        End If

        If Not Request.QueryString("ProdBrand") = "" Then
            itemSelectStr = itemSelectStr & "ITM.ProdBrandCode LIKE '" & Request.QueryString("ProdBrand") & "' AND "
        Else
            itemSelectStr = itemSelectStr & "ITM.ProdBrandCode LIKE '%' AND "
        End If

        If Not Request.QueryString("ProdModel") = "" Then
            itemSelectStr = itemSelectStr & "ITM.ProdModelCode LIKE '" & Request.QueryString("ProdModel") & "' AND "
        Else
            itemSelectStr = itemSelectStr & "ITM.ProdModelCode LIKE '%' AND "
        End If

        If Not Request.QueryString("ProdCat") = "" Then
            itemSelectStr = itemSelectStr & "ITM.ProdCatCode LIKE '" & Request.QueryString("ProdCat") & "' AND "
        Else
            itemSelectStr = itemSelectStr & "ITM.ProdCatCode LIKE '%' AND "
        End If

        If Not Request.QueryString("ProdMat") = "" Then
            itemSelectStr = itemSelectStr & "ITM.ProdMatCode LIKE '" & Request.QueryString("ProdMat") & "' AND "
        Else
            itemSelectStr = itemSelectStr & "ITM.ProdMatCode LIKE '%' AND "
        End If

        If Not Request.QueryString("StkAna") = "" Then
            itemSelectStr = itemSelectStr & "ITM.StockAnalysisCode LIKE '" & Request.QueryString("StkAna") & "' AND "
        Else
            itemSelectStr = itemSelectStr & "ITM.StockAnalysisCode LIKE '%' AND "
        End If

        If Not Request.QueryString("ItemCode") = "" Then
            itemSelectStr = itemSelectStr & "ITM.ItemCode LIKE '" & Request.QueryString("ItemCode") & "' AND "
        Else
            itemSelectStr = itemSelectStr & "ITM.ItemCode LIKE '%' AND "
        End If

        If Request.QueryString("ProdType") = "" And Request.QueryString("ProdBrand") = "" And Request.QueryString("ProdModel") = "" And Request.QueryString("ProdCat") = "" And Request.QueryString("ProdMat") = "" And Request.QueryString("StkAna") = "" And Request.QueryString("ItemCode") = "" Then
            itemSearchStr = "OR ( STKRCVLN.ItemCode = '' )"
        End If

        If Not Request.QueryString("Status") = "" Then
            If Not Request.QueryString("Status") = objINTrx.EnumStockReceiveStatus.All Then
                SearchStr = SearchStr & "STKRCV.Status = '" & Request.QueryString("Status") & "' AND "
            Else
                SearchStr = SearchStr & "STKRCV.Status LIKE '%' AND "
            End If
        End If

        If Not SearchStr = "" Or Not itemSelectStr = "" Then
            If Right(SearchStr, 4) = "AND " Then
                SearchStr = Left(SearchStr, Len(SearchStr) - 4)
            End If

            If Right(itemSelectStr, 4) = "AND " Then
                itemSelectStr = Left(itemSelectStr, Len(itemSelectStr) - 4)
            End If

            If Not Request.QueryString("ItemCode") = "" Or Not Request.QueryString("ProdType") = "" Or Not Request.QueryString("ProdBrand") = "" Or Not _
                   Request.QueryString("ProdModel") = "" Or Not Request.QueryString("ProdCat") = "" Or Not Request.QueryString("ProdMat") = "" Or Not Request.QueryString("StkAna") = "" Then
                strParam = strUserLoc & "|" & strDDLAccMth & "|" & strDDLAccYr & "|" & itemSelectStr & "|" & itemSearchStr & "|" & NormStr & SearchStr
            Else
                strParam = strUserLoc & "|" & strDDLAccMth & "|" & strDDLAccYr & "|" & itemSelectStr & "|" & itemSearchStr & "|" & WildStr & SearchStr
            End If

        End If

        Try
            intErrNo = objIN.mtdGetReport_StockReceiveList(strOpCdStkRcv, strParam, objRptDs, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_STKRECEIVE_LIST_REPORT&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objRptDs.Tables(0).Rows.Count - 1
            If Not IsDBNull(objRptDs.Tables(0).Rows(intCnt).Item("ItemCode")) Then
                strItemCode = Trim(objRptDs.Tables(0).Rows(intCnt).Item("ItemCode"))
                strParamItm = "WHERE ITM.ItemCode = '" & strItemCode & "'"
                Try
                    intErrNo = objIN.mtdGetItem(strOpCdItem_GET, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strAccMonth, _
                                                strAccYear, _
                                                strParamItm, _
                                                objItem)
                Catch Exp As System.Exception
                    Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_STKRECEIVE_ITEM_REPORT&errmesg=" & lblErrMessage.Text & "&redirect=")
                End Try

                If objItem.Tables(0).Rows.Count > 0 Then
                    objRptDs.Tables(0).Rows(intCnt).Item("ItemDesc") = Trim(objItem.Tables(0).Rows(0).Item("Description"))
                    objRptDs.Tables(0).Rows(intCnt).Item("UOMCode") = Trim(objItem.Tables(0).Rows(0).Item("UOMCode"))
                End If

            End If
        Next intCnt

        If Not Request.QueryString("ItemCode") = "" Or Not Request.QueryString("ProdType") = "" Or Not Request.QueryString("ProdBrand") = "" Or Not _
               Request.QueryString("ProdModel") = "" Or Not Request.QueryString("ProdCat") = "" Or Not Request.QueryString("ProdMat") = "" Or Not Request.QueryString("StkAna") = "" Then
            If objRptDs.Tables(0).Rows.Count > 0 Then
                Do
                    If IsDBNull(objRptDs.Tables(0).Rows(intCntRem).Item("StockReceiveLNID")) Then
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
                    If IsDBNull(objRptDs.Tables(0).Rows(intCntRem).Item("StockReceiveLNID")) Then
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


        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\IN_StdRpt_StkReceiveList.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA4

        crvView.Visible = False
        crvView.ReportSource = rdCrystalViewer
        crvView.DataBind()

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\IN_StdRpt_StkReceiveList.pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        rdCrystalViewer.Close()
        rdCrystalViewer.Dispose()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/IN_StdRpt_StkReceiveList.pdf"">")
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

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("ParamLocation")
        paramField2 = paramFields.Item("ParamDateFrom")
        paramField3 = paramFields.Item("ParamDateTo")
        paramField4 = paramFields.Item("ParamStkRcvIDFrom")
        paramField5 = paramFields.Item("ParamStkRcvIDTo")
        paramField6 = paramFields.Item("ParamStkRcvType")
        paramField7 = paramFields.Item("ParamProdType")
        paramField8 = paramFields.Item("ParamProdBrand")
        paramField9 = paramFields.Item("ParamProdModel")
        paramField10 = paramFields.Item("ParamProdCat")
        paramField11 = paramFields.Item("ParamProdMat")
        paramField12 = paramFields.Item("ParamStatus")
        paramField13 = paramFields.Item("ParamStkAna")
        paramField14 = paramFields.Item("ParamCompanyName")
        paramField15 = paramFields.Item("ParamUserName")
        paramField16 = paramFields.Item("ParamDecimal")
        paramField17 = paramFields.Item("ParamRptID")
        paramField18 = paramFields.Item("ParamRptName")
        paramField19 = paramFields.Item("ParamAccMonth")
        paramField20 = paramFields.Item("ParamAccYear")
        paramField21 = paramFields.Item("lblProdTypeCode")
        paramField22 = paramFields.Item("lblProdBrandCode")
        paramField23 = paramFields.Item("lblProdModelCode")
        paramField24 = paramFields.Item("lblProdCatCode")
        paramField25 = paramFields.Item("lblProdMatCode")
        paramField26 = paramFields.Item("ParamItemCode")
        paramField27 = paramFields.Item("ParamStkRefNo")
        paramField28 = paramFields.Item("ParamPRIDFrom")
        paramField29 = paramFields.Item("ParamPRIDTo")
        paramField30 = paramFields.Item("lblLocation")
        paramField31 = paramFields.Item("lblStkAnaCode")

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

        ParamDiscreteValue1.Value = Session("SS_LOC")
        ParamDiscreteValue2.Value = Session("SS_DateFrom")
        ParamDiscreteValue3.Value = Session("SS_DateTo")
        ParamDiscreteValue4.Value = Request.QueryString("StkRcvIDFrom") 'Session("SS_StkIssueIDFrom")
        ParamDiscreteValue5.Value = Request.QueryString("StkRcvIDTo") 'Session("SS_StkIssueIDTo")
        ParamDiscreteValue6.Value = Session("SS_StkRcvType")
        ParamDiscreteValue7.Value = Session("SS_PRODTYPE")
        ParamDiscreteValue8.Value = Session("SS_PRODBRAND")
        ParamDiscreteValue9.Value = Session("SS_PRODMODEL")
        ParamDiscreteValue10.Value = Session("SS_PRODCAT")
        ParamDiscreteValue11.Value = Session("SS_PRODMAT")
        ParamDiscreteValue12.Value = Session("SS_Status")
        ParamDiscreteValue13.Value = Session("SS_STKANA")
        ParamDiscreteValue14.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue15.Value = Session("SS_USERNAME")
        ParamDiscreteValue16.Value = Session("SS_DECIMAL")
        ParamDiscreteValue17.Value = Session("SS_RPTID")
        ParamDiscreteValue18.Value = Session("SS_RPTNAME")
        ParamDiscreteValue19.Value = Session("SS_ACCMONTH")
        ParamDiscreteValue20.Value = Session("SS_ACCYEAR")
        ParamDiscreteValue21.Value = Session("SS_LBLPRODTYPECODE")
        ParamDiscreteValue22.Value = Session("SS_LBLPRODBRANDCODE")
        ParamDiscreteValue23.Value = Session("SS_LBLPRODMODELCODE")
        ParamDiscreteValue24.Value = Session("SS_LBLPRODCATCODE")
        ParamDiscreteValue25.Value = Session("SS_LBLPRODMATCODE")
        ParamDiscreteValue26.Value = Session("SS_ITEMCODE")
        ParamDiscreteValue27.Value = Session("SS_StkRRefNo")
        ParamDiscreteValue28.Value = Session("SS_PRIDFROM")
        ParamDiscreteValue29.Value = Session("SS_PRIDTO")
        ParamDiscreteValue30.Value = Session("SS_LBLLOCATION")
        ParamDiscreteValue31.Value = Session("SS_LBLSTKANACODE")

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

        crvView.ParameterFieldInfo = paramFields
    End Sub
End Class
