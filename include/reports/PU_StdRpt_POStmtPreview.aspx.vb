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

Public Class PU_StdRpt_POStmt_Preview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid
    Protected WithEvents EventData2 As DataGrid

    Dim objPU As New agri.PU.clsReport()
    Dim objPUTrx As New agri.PU.clsTrx()
    Dim objAPTrx As New agri.AP.clsTrx()

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
    Dim strDDLAccMth As String
    Dim strDDLAccYr As String

    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer

    Dim tblPOLine As DataTable = New DataTable("tblPOLine")
    Dim tblGoodsRcv As DataTable = New DataTable("tblGoodsRcv")
    Dim tblInvRcv As DataTable = New DataTable("tblInvRcv")


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

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            BindReportSP()
        End If
    End Sub

    Sub BindReportSP()
        Dim objRptDs As New DataSet()
        Dim objMapPath As String
        Dim strOpCode As String = "PU_STDRPT_PURCHASE_ORDER_STATEMENT_GETSP"
        Dim strPOSearch As String = ""
        Dim strItemSearch As String = ""
        Dim strPOStatus As String = ""

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        If Not (Request.QueryString("DocDateFrom") = "" And Request.QueryString("DocDateTo") = "") Then
            strPOSearch = strPOSearch & " And (DateDiff(Day, '" & Request.QueryString("DocDateFrom") & "', po.CreateDate) >= 0) And (DateDiff(Day, '" & Request.QueryString("DocDateTo") & "', po.CreateDate) <= 0) "
        End If

        If Not (Request.QueryString("DocNoFrom") = "" And Request.QueryString("DocNoTo") = "") Then
            strPOSearch = strPOSearch & " And po.POID >= '" & Request.QueryString("DocNoFrom") & "' And po.POID <= '" & Request.QueryString("DocNoTo") & "' "
        End If
        
        If Not Request.QueryString("POType") = "" Then
            If Not Request.QueryString("POType") = objPUTrx.EnumPOType.All Then
                strPOSearch = strPOSearch & " And po.POType = '" & Request.QueryString("POType") & "' "
            End If
        End If

        If Not Request.QueryString("Supplier") = "" Then
            strPOSearch = strPOSearch & " And po.SupplierCode LIKE '" & Request.QueryString("Supplier") & "%' "
        End If

        If Not Request.QueryString("ProdType") = "" Then
            strItemSearch = strItemSearch & " And itm.ProdTypeCode LIKE '" & Request.QueryString("ProdType") & "%' "
        End If

        If Not Request.QueryString("ProdBrand") = "" Then
            strItemSearch = strItemSearch & " And itm.ProdBrandCode LIKE '" & Request.QueryString("ProdBrand") & "%' "
        End If

        If Not Request.QueryString("ProdModel") = "" Then
            strItemSearch = strItemSearch & " And itm.ProdModelCode LIKE '" & Request.QueryString("ProdModel") & "%' "
        End If

        If Not Request.QueryString("ProdCat") = "" Then
            strItemSearch = strItemSearch & " And itm.ProdCatCode LIKE '" & Request.QueryString("ProdCat") & "%' "
        End If

        If Not Request.QueryString("ProdMat") = "" Then
            strItemSearch = strItemSearch & " And itm.ProdMatCode LIKE '" & Request.QueryString("ProdMat") & "%' "
        End If

        If Not Request.QueryString("StkAna") = "" Then
            strItemSearch = strItemSearch & " And itm.StockAnalysisCode LIKE '" & Request.QueryString("StkAna") & "%' "
        End If

        If Not Request.QueryString("ItemCode") = "" Then
            strItemSearch = strItemSearch & " And itm.ItemCode LIKE '" & Request.QueryString("ItemCode") & "%' "
        End If

        If Not Request.QueryString("Status") = objPUTrx.mtdGetPOStatus(objPUTrx.EnumPOStatus.All) Then
            strPOStatus = Request.QueryString("Status")
        End If
        
        Try
            intErrNo = objPU.mtdGetReport_POStatementSP("'" & strUserLoc & "'", _
                                                        strDDLAccMth, _
                                                        strDDLAccYr, _
                                                        strPOSearch, _
                                                        strItemSearch, _
                                                        strPOStatus, _
                                                        strOpCode, _
                                                        objRptDs, _
                                                        objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_STDRPT_POSTMT_GOODSRCV_DET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        
        objRptDs.Tables(0).TableName = "PU_PO_STMT_DET"
        objRptDs.Tables(1).TableName = "PU_PO_LINE_STMT_DET"
        objRptDs.Tables(2).TableName = "PU_PO_STMT_GOODSRCV_DET"
        objRptDs.Tables(3).TableName = "PU_PO_STMT_INVOICERCV_DET"


        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\PU_StdRpt_POStmt.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs)

        crvView.Visible = False                         
        crvView.ReportSource = rdCrystalViewer          
        crvView.DataBind()                              

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\PU_StdRpt_POStmt.pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        rdCrystalViewer.Close()
        rdCrystalViewer.Dispose()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/PU_StdRpt_POStmt.pdf"">")
    End Sub

    Sub BindReport()

        Dim objRptDs As New DataSet()
        Dim dsPOLine As New DataSet()
        Dim dsPOLineDet As New DataSet()
        Dim dsGoodsRcvDet As New DataSet()
        Dim dsGRDet As New DataSet()
        Dim dsInvoiceRcvDet As New DataSet()
        Dim dsIRDet As New DataSet()
        Dim objItem As New DataSet()
        Dim objMapPath As String

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOpCd_POStmt_Det_GET As String = "PU_STDRPT_POSTMT_DET_GET"
        Dim strOpCd_POLnStmt_Det_GET As String = "PU_STDRPT_POLNSTMT_DET_GET"
        Dim strOpCd_POStmt_GoodsRcv_Det_GET As String = "PU_STDRPT_POSTMT_GOODSRCV_DET_GET"
        Dim strOpCd_POStmt_InvoiceRcv_Det_GET As String = "PU_STDRPT_POSTMT_INVOICERCV_DET_GET"
        Dim strOpCdItem_GET As String = "PU_STDRPT_ITEM_GET"

        Dim strParam As String
        Dim strParamLine As String
        Dim strParamGoodsRcv As String
        Dim strParamInvRcv As String
        Dim strParamItm As String
        Dim SearchStr As String
        Dim itemSelectStr As String
        Dim itemSearchStr As String
        Dim StatusSearchStr As String
        Dim strPOID As String
        Dim strItemCode As String
        Dim strLineStatus As String
        Dim strQtyRcv As String
        Dim strQtyCancel As String
        Dim strQtyOrder As String
        Dim strQtyInvoice As String

        Dim intCnt As Integer
        Dim intCntLine As Integer
        Dim intCntStsCancel As Integer
        Dim intCntStsNew As Integer
        Dim intCntStsFM As Integer
        Dim intCntStsPM As Integer
        Dim intCntGR As Integer
        Dim intCntIR As Integer
        Dim intCntRem As Integer

        Dim dr As DataRow

        If Not (Request.QueryString("DocDateFrom") = "" And Request.QueryString("DocDateTo") = "") Then
            SearchStr = "AND (DateDiff(Day, '" & Session("SS_DOCDATEFROM") & "', PO.CreateDate) >= 0) And (DateDiff(Day, '" & Session("SS_DOCDATETO") & "', PO.CreateDate) <= 0) "
        End If

        If Not (Request.QueryString("DocNoFrom") = "" And Request.QueryString("DocNoTo") = "") Then
            SearchStr = SearchStr & "AND PO.POID IN (SELECT SUBPO.POID FROM PU_PO SUBPO WHERE SUBPO.POID >= '" & Request.QueryString("DocNoFrom") & _
                        "' AND SUBPO.POID <= '" & Request.QueryString("DocNoTo") & "') "
        End If

        If Not Request.QueryString("Supplier") = "" Then
            SearchStr = SearchStr & "AND PO.SupplierCode LIKE '" & Request.QueryString("Supplier") & "' "
        Else
            SearchStr = SearchStr & "AND PO.SupplierCode LIKE '%'"
        End If

        If Not Request.QueryString("ProdType") = "" Then
            itemSelectStr = "ITM.ProdTypeCode LIKE '" & Request.QueryString("ProdType") & "' "
        Else
            itemSelectStr = "ITM.ProdTypeCode LIKE '%' "
        End If

        If Not Request.QueryString("ProdBrand") = "" Then
            itemSelectStr = itemSelectStr & "AND ITM.ProdBrandCode LIKE '" & Request.QueryString("ProdBrand") & "' "
        Else
            itemSelectStr = itemSelectStr & "AND ITM.ProdBrandCode LIKE '%' "
        End If

        If Not Request.QueryString("ProdModel") = "" Then
            itemSelectStr = itemSelectStr & "AND ITM.ProdModelCode LIKE '" & Request.QueryString("ProdModel") & "' "
        Else
            itemSelectStr = itemSelectStr & "AND ITM.ProdModelCode LIKE '%' "
        End If

        If Not Request.QueryString("ProdCat") = "" Then
            itemSelectStr = itemSelectStr & "AND ITM.ProdCatCode LIKE '" & Request.QueryString("ProdCat") & "' "
        Else
            itemSelectStr = itemSelectStr & "AND ITM.ProdCatCode LIKE '%' "
        End If

        If Not Request.QueryString("ProdMat") = "" Then
            itemSelectStr = itemSelectStr & "AND ITM.ProdMatCode LIKE '" & Request.QueryString("ProdMat") & "' "
        Else
            itemSelectStr = itemSelectStr & "AND ITM.ProdMatCode LIKE '%' "
        End If

        If Not Request.QueryString("StkAna") = "" Then
            itemSelectStr = itemSelectStr & "AND ITM.StockAnalysisCode LIKE '" & Request.QueryString("StkAna") & "' "
        Else
            itemSelectStr = itemSelectStr & "AND ITM.StockAnalysisCode LIKE '%' "
        End If

        If Not Request.QueryString("ItemCode") = "" Then
            itemSelectStr = itemSelectStr & "AND ITM.ItemCode LIKE '" & Request.QueryString("ItemCode") & "' "
        Else
            itemSelectStr = itemSelectStr & "AND ITM.ItemCode LIKE '%' "
        End If

        If Request.QueryString("ProdType") = "" And Request.QueryString("ProdBrand") = "" And Request.QueryString("ProdModel") = "" And Request.QueryString("ProdCat") = "" And Request.QueryString("ProdMat") = "" And Request.QueryString("StkAna") = "" And Request.QueryString("ItemCode") = "" Then
            itemSearchStr = "OR ( POLN.ItemCode = '' )"
        End If

        If Not Request.QueryString("POType") = "" Then
            If Not Request.QueryString("POType") = objPUTrx.EnumPOType.All Then
                SearchStr = SearchStr & "AND PO.POType = '" & Request.QueryString("POType") & "' "
            Else
                SearchStr = SearchStr & "AND PO.POType LIKE '%' "
            End If
        End If

        strParam = strUserLoc & "|" & strDDLAccMth & "|" & strDDLAccYr & "|||" & SearchStr
        Try
            intErrNo = objPU.mtdGetReport_POList(strOpCd_POStmt_Det_GET, strParam, objRptDs, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_STDRPT_PO_STMT&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objRptDs.Tables(0).Rows.Count - 1
            If Not IsDBNull(objRptDs.Tables(0).Rows(intCnt).Item("POID")) Then
                strPOID = Trim(objRptDs.Tables(0).Rows(intCnt).Item("POID"))
            End If

            strParamLine = "|||" & itemSelectStr & "|" & itemSearchStr & "|POLN.POID = '" & strPOID & "'"
            Try
                intErrNo = objPU.mtdGetReport_POList(strOpCd_POLnStmt_Det_GET, strParamLine, dsPOLine, objMapPath)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_STDRPT_POLINE_STMT&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try

            For intCntLine = 0 To dsPOLine.Tables(0).Rows.Count - 1
                strItemCode = Trim(dsPOLine.Tables(0).Rows(intCntLine).Item("ItemCode"))
                If Not IsDBNull(dsPOLine.Tables(0).Rows(intCntLine).Item("Status")) Then
                    strLineStatus = Trim(dsPOLine.Tables(0).Rows(intCntLine).Item("Status"))
                End If
                If Not IsDBNull(dsPOLine.Tables(0).Rows(intCntLine).Item("QtyReceive")) Then
                    strQtyRcv = Trim(dsPOLine.Tables(0).Rows(intCntLine).Item("QtyReceive"))
                Else
                    strQtyRcv = 0
                End If
                If Not IsDBNull(dsPOLine.Tables(0).Rows(intCntLine).Item("QtyCancel")) Then
                    strQtyCancel = Trim(dsPOLine.Tables(0).Rows(intCntLine).Item("QtyCancel"))
                Else
                    strQtyCancel = 0
                End If
                If Not IsDBNull(dsPOLine.Tables(0).Rows(intCntLine).Item("QtyOrder")) Then
                    strQtyOrder = Trim(dsPOLine.Tables(0).Rows(intCntLine).Item("QtyOrder"))
                Else
                    strQtyOrder = 0
                End If
                If Not IsDBNull(dsPOLine.Tables(0).Rows(intCntLine).Item("QtyInvoice")) Then
                    strQtyInvoice = Trim(dsPOLine.Tables(0).Rows(intCntLine).Item("QtyInvoice"))
                Else
                    strQtyInvoice = 0
                End If

                If strQtyRcv = 0 And strQtyCancel <> 0 And strLineStatus = objPUTrx.EnumPOLnStatus.Cancelled Then
                    intCntStsCancel += 1
                    dsPOLine.Tables(0).Rows(intCntLine).Item("POLnStatus") = objPUTrx.mtdGetPOLnStatus(objPUTrx.EnumPOLnStatus.Cancelled)
                End If
                If strQtyOrder = strQtyRcv And strLineStatus = objPUTrx.EnumPOLnStatus.Active Then
                    intCntStsFM += 1
                    dsPOLine.Tables(0).Rows(intCntLine).Item("POLnStatus") = objPUTrx.mtdGetPOLnStatus(objPUTrx.EnumPOLnStatus.FullyMatched)
                End If
                If strQtyRcv = 0 And strQtyCancel = 0 And strLineStatus = objPUTrx.EnumPOLnStatus.Active Then
                    intCntStsNew += 1
                    dsPOLine.Tables(0).Rows(intCntLine).Item("POLnStatus") = objPUTrx.mtdGetPOLnStatus(objPUTrx.EnumPOLnStatus.AllNew)
                End If
                If strQtyOrder <> strQtyRcv And strQtyRcv <> 0 And strQtyCancel = 0 And strLineStatus = objPUTrx.EnumPOLnStatus.Active Then
                    intCntStsPM += 1
                    dsPOLine.Tables(0).Rows(intCntLine).Item("POLnStatus") = objPUTrx.mtdGetPOLnStatus(objPUTrx.EnumPOLnStatus.PartialMatched)
                ElseIf strQtyOrder <> strQtyRcv And strQtyRcv <> 0 And strQtyCancel <> 0 And strLineStatus = objPUTrx.EnumPOLnStatus.Cancelled Then
                    intCntStsPM += 1
                    dsPOLine.Tables(0).Rows(intCntLine).Item("POLnStatus") = objPUTrx.mtdGetPOLnStatus(objPUTrx.EnumPOLnStatus.PartialCancel)
                End If

                strParamGoodsRcv = "|||||AND GR.POID = '" & strPOID & "'AND GRLN.ItemCode = '" & strItemCode & "' AND GR.Status NOT IN ('" & objPUTrx.EnumGRStatus.Deleted & "', '" & objPUTrx.EnumGRStatus.Cancelled & "')"
                Try
                    intErrNo = objPU.mtdGetReport_POList(strOpCd_POStmt_GoodsRcv_Det_GET, strParamGoodsRcv, dsGoodsRcvDet, objMapPath)
                Catch Exp As System.Exception
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_STDRPT_POSTMT_GOODSRCV_DET&errmesg=" & lblErrMessage.Text & "&redirect=")
                End Try

                strParamInvRcv = "|||||AND IR.POID = '" & strPOID & "'AND IRLN.ItemCode = '" & strItemCode & "' AND IR.Status NOT IN ('" & objAPTrx.EnumInvoiceRcvStatus.Deleted & "', '" & objAPTrx.EnumInvoiceRcvStatus.Cancelled & "')"
                Try
                    intErrNo = objPU.mtdGetReport_POList(strOpCd_POStmt_InvoiceRcv_Det_GET, strParamInvRcv, dsInvoiceRcvDet, objMapPath)
                Catch Exp As System.Exception
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_STDRPT_POSTMT_INVRCV_DET&errmesg=" & lblErrMessage.Text & "&redirect=")
                End Try

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
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_ITEM_FOR_POLN_STMT_REPORT&errmesg=" & lblErrMessage.Text & "&redirect=")
                End Try

                If objItem.Tables(0).Rows.Count > 0 Then
                    dsPOLine.Tables(0).Rows(intCntLine).Item("ItemDesc") = Trim(objItem.Tables(0).Rows(0).Item("Description"))
                    dsPOLine.Tables(0).Rows(intCntLine).Item("PurchaseUOM") = Trim(objItem.Tables(0).Rows(0).Item("PurchaseUOM"))
                End If

                dr = tblPOLine.NewRow()
                dr("POID") = Trim(dsPOLine.Tables(0).Rows(intCntLine).Item("POID"))
                dr("PRID") = Trim(dsPOLine.Tables(0).Rows(intCntLine).Item("PRID"))
                dr("ItemCode") = Trim(dsPOLine.Tables(0).Rows(intCntLine).Item("ItemCode"))
                dr("ItemDesc") = Trim(dsPOLine.Tables(0).Rows(intCntLine).Item("ItemDesc"))
                dr("PurchaseUOM") = Trim(dsPOLine.Tables(0).Rows(intCntLine).Item("PurchaseUOM"))
                dr("QtyOrder") = dsPOLine.Tables(0).Rows(intCntLine).Item("QtyOrder")
                dr("QtyReceive") = dsPOLine.Tables(0).Rows(intCntLine).Item("QtyReceive")
                dr("QtyInvoice") = dsPOLine.Tables(0).Rows(intCntLine).Item("QtyInvoice")
                dr("QtyCancel") = dsPOLine.Tables(0).Rows(intCntLine).Item("QtyCancel")
                dr("Cost") = dsPOLine.Tables(0).Rows(intCntLine).Item("Cost")
                dr("Amount") = dsPOLine.Tables(0).Rows(intCntLine).Item("Amount")
                dr("POLnStatus") = Trim(dsPOLine.Tables(0).Rows(intCntLine).Item("POLnStatus"))
                tblPOLine.Rows.Add(dr)

                For intCntGR = 0 To dsGoodsRcvDet.Tables(0).Rows.Count - 1
                    dr = tblGoodsRcv.NewRow()
                    dr("POID") = Trim(dsGoodsRcvDet.Tables(0).Rows(intCntGR).Item("POID"))
                    dr("ItemCode") = Trim(dsGoodsRcvDet.Tables(0).Rows(intCntGR).Item("ItemCode"))
                    dr("GoodsRcvID") = Trim(dsGoodsRcvDet.Tables(0).Rows(intCntGR).Item("GoodsRcvID"))
                    dr("CreateDate") = Trim(dsGoodsRcvDet.Tables(0).Rows(intCntGR).Item("CreateDate"))
                    dr("ReceiveQty") = dsGoodsRcvDet.Tables(0).Rows(intCntGR).Item("ReceiveQty")
                    If Not IsDBNull(dsGoodsRcvDet.Tables(0).Rows(intCntGR).Item("AccCode")) Then
                        dr("AccCode") = Trim(dsGoodsRcvDet.Tables(0).Rows(intCntGR).Item("AccCode"))
                    Else
                        dr("AccCode") = ""
                    End If
                    If Not IsDBNull(dsGoodsRcvDet.Tables(0).Rows(intCntGR).Item("BlkCode")) Then
                        dr("BlkCode") = Trim(dsGoodsRcvDet.Tables(0).Rows(intCntGR).Item("BlkCode"))
                    Else
                        dr("BlkCode") = ""
                    End If
                    If Not IsDBNull(dsGoodsRcvDet.Tables(0).Rows(intCntGR).Item("VehCode")) Then
                        dr("VehCode") = Trim(dsGoodsRcvDet.Tables(0).Rows(intCntGR).Item("VehCode"))
                    Else
                        dr("VehCode") = ""
                    End If
                    If Not IsDBNull(dsGoodsRcvDet.Tables(0).Rows(intCntGR).Item("VehExpenseCode")) Then
                        dr("VehExpenseCode") = Trim(dsGoodsRcvDet.Tables(0).Rows(intCntGR).Item("VehExpenseCode"))
                    Else
                        dr("VehExpenseCode") = ""
                    End If

                    tblGoodsRcv.Rows.Add(dr)
                Next

                For intCntIR = 0 To dsInvoiceRcvDet.Tables(0).Rows.Count - 1
                    dr = tblInvRcv.NewRow()
                    dr("POID") = Trim(dsInvoiceRcvDet.Tables(0).Rows(intCntIR).Item("POID"))
                    dr("ItemCode") = Trim(dsInvoiceRcvDet.Tables(0).Rows(intCntIR).Item("ItemCode"))
                    dr("InvoiceRcvID") = Trim(dsInvoiceRcvDet.Tables(0).Rows(intCntIR).Item("InvoiceRcvID"))
                    dr("CreateDate") = Trim(dsInvoiceRcvDet.Tables(0).Rows(intCntIR).Item("CreateDate"))
                    dr("QtyInvoice") = dsInvoiceRcvDet.Tables(0).Rows(intCntIR).Item("QtyInvoice")
                    If Not IsDBNull(dsInvoiceRcvDet.Tables(0).Rows(intCntIR).Item("AccCode")) Then
                        dr("AccCode") = Trim(dsInvoiceRcvDet.Tables(0).Rows(intCntIR).Item("AccCode"))
                    Else
                        dr("AccCode") = ""
                    End If
                    If Not IsDBNull(dsInvoiceRcvDet.Tables(0).Rows(intCntIR).Item("BlkCode")) Then
                        dr("BlkCode") = Trim(dsInvoiceRcvDet.Tables(0).Rows(intCntIR).Item("BlkCode"))
                    Else
                        dr("BlkCode") = ""
                    End If
                    If Not IsDBNull(dsInvoiceRcvDet.Tables(0).Rows(intCntIR).Item("VehCode")) Then
                        dr("VehCode") = Trim(dsInvoiceRcvDet.Tables(0).Rows(intCntIR).Item("VehCode"))
                    Else
                        dr("VehCode") = ""
                    End If
                    If Not IsDBNull(dsInvoiceRcvDet.Tables(0).Rows(intCntIR).Item("VehExpenseCode")) Then
                        dr("VehExpenseCode") = Trim(dsInvoiceRcvDet.Tables(0).Rows(intCntIR).Item("VehExpenseCode"))
                    Else
                        dr("VehExpenseCode") = ""
                    End If
                    tblInvRcv.Rows.Add(dr)
                Next
            Next

            If intCntStsCancel = dsPOLine.Tables(0).Rows.Count Then
                objRptDs.Tables(0).Rows(intCnt).Item("POStatus") = objPUTrx.mtdGetPOStatus(objPUTrx.EnumPOStatus.Cancelled)
            End If

            If intCntStsFM = dsPOLine.Tables(0).Rows.Count Then
                objRptDs.Tables(0).Rows(intCnt).Item("POStatus") = objPUTrx.mtdGetPOStatus(objPUTrx.EnumPOStatus.FullyMatched)
            End If

            If intCntStsNew = dsPOLine.Tables(0).Rows.Count Then
                objRptDs.Tables(0).Rows(intCnt).Item("POStatus") = objPUTrx.mtdGetPOStatus(objPUTrx.EnumPOStatus.AllNew)
            ElseIf intCntStsNew <> 0 Then
                objRptDs.Tables(0).Rows(intCnt).Item("POStatus") = objPUTrx.mtdGetPOStatus(objPUTrx.EnumPOStatus.PartialMatched)
            End If

            If intCntStsPM = dsPOLine.Tables(0).Rows.Count Then
                objRptDs.Tables(0).Rows(intCnt).Item("POStatus") = objPUTrx.mtdGetPOStatus(objPUTrx.EnumPOStatus.PartialMatched)
            ElseIf intCntStsPM <> 0 Then
                objRptDs.Tables(0).Rows(intCnt).Item("POStatus") = objPUTrx.mtdGetPOStatus(objPUTrx.EnumPOStatus.PartialMatched)
            End If

            intCntStsCancel = 0
            intCntStsFM = 0
            intCntStsNew = 0
            intCntStsPM = 0

        Next
        dsPOLineDet.Tables.Add(tblPOLine)
        dsGRDet.Tables.Add(tblGoodsRcv)
        dsIRDet.Tables.Add(tblInvRcv)

        If Not Request.QueryString("Status") = objPUTrx.mtdGetPOStatus(objPUTrx.EnumPOStatus.All) Then
            If objRptDs.Tables(0).Rows.Count > 0 Then
                Do
                    If Not objRptDs.Tables(0).Rows(intCntRem).Item("POStatus") = Request.QueryString("Status") Then
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

        objRptDs.Tables(0).TableName = "PU_PO_STMT_DET"
        If objRptDs.Tables(0).Rows.Count > 0 Then
            objRptDs.Tables.Add(dsPOLineDet.Tables(0).Copy())
            objRptDs.Tables(1).TableName = "PU_PO_LINE_STMT_DET"
            objRptDs.Tables.Add(dsGRDet.Tables(0).Copy())
            objRptDs.Tables(2).TableName = "PU_PO_STMT_GOODSRCV_DET"
            objRptDs.Tables.Add(dsIRDet.Tables(0).Copy())
            objRptDs.Tables(3).TableName = "PU_PO_STMT_INVOICERCV_DET"
        End If


        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\PU_StdRpt_POStmt.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs)

        crvView.Visible = False                         
        crvView.ReportSource = rdCrystalViewer          
        crvView.DataBind()                              

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\PU_StdRpt_POStmt.pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/PU_StdRpt_POStmt.pdf"">")
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
        Dim paramField34 As New ParameterField()

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
        paramField30 = paramFields.Item("lblAccCode")
        paramField31 = paramFields.Item("lblBlkCode")
        paramField32 = paramFields.Item("lblSubBlkCode")
        paramField33 = paramFields.Item("lblVehCode")
        paramField34 = paramFields.Item("lblVehExpCode")

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
        ParamDiscreteValue30.Value = Request.QueryString("lblAccCodeTag")
        ParamDiscreteValue31.Value = Request.QueryString("lblBlkCodeTag")
        ParamDiscreteValue32.Value = Request.QueryString("lblSubBlkCodeTag")
        ParamDiscreteValue33.Value = Request.QueryString("lblVehCodeTag")
        ParamDiscreteValue34.Value = Request.QueryString("lblVehExpCodeTag")

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
    End Sub

    Sub CreatePOLineTables()
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

        Col1.DataType = System.Type.GetType("System.String")
        Col1.AllowDBNull = True
        Col1.Caption = "POID"
        Col1.ColumnName = "POID"
        Col1.DefaultValue = ""
        tblPOLine.Columns.Add(Col1)

        Col2.DataType = System.Type.GetType("System.String")
        Col2.AllowDBNull = True
        Col2.Caption = "PRID"
        Col2.ColumnName = "PRID"
        Col2.DefaultValue = ""
        tblPOLine.Columns.Add(Col2)

        Col3.DataType = System.Type.GetType("System.String")
        Col3.AllowDBNull = True
        Col3.Caption = "ItemCode"
        Col3.ColumnName = "ItemCode"
        Col3.DefaultValue = ""
        tblPOLine.Columns.Add(Col3)

        Col4.DataType = System.Type.GetType("System.String")
        Col4.AllowDBNull = True
        Col4.Caption = "ItemDesc"
        Col4.ColumnName = "ItemDesc"
        Col4.DefaultValue = ""
        tblPOLine.Columns.Add(Col4)

        Col5.DataType = System.Type.GetType("System.String")
        Col5.AllowDBNull = True
        Col5.Caption = "PurchaseUOM"
        Col5.ColumnName = "PurchaseUOM"
        Col5.DefaultValue = ""
        tblPOLine.Columns.Add(Col5)

        Col6.DataType = System.Type.GetType("System.Decimal")
        Col6.AllowDBNull = True
        Col6.Caption = "QtyOrder"
        Col6.ColumnName = "QtyOrder"
        Col6.DefaultValue = 0
        tblPOLine.Columns.Add(Col6)

        Col7.DataType = System.Type.GetType("System.Decimal")
        Col7.AllowDBNull = True
        Col7.Caption = "QtyReceive"
        Col7.ColumnName = "QtyReceive"
        Col7.DefaultValue = 0
        tblPOLine.Columns.Add(Col7)

        Col8.DataType = System.Type.GetType("System.Decimal")
        Col8.AllowDBNull = True
        Col8.Caption = "QtyInvoice"
        Col8.ColumnName = "QtyInvoice"
        Col8.DefaultValue = 0
        tblPOLine.Columns.Add(Col8)

        Col9.DataType = System.Type.GetType("System.Decimal")
        Col9.AllowDBNull = True
        Col9.Caption = "QtyCancel"
        Col9.ColumnName = "QtyCancel"
        Col9.DefaultValue = 0
        tblPOLine.Columns.Add(Col9)

        Col10.DataType = System.Type.GetType("System.Decimal")
        Col10.AllowDBNull = True
        Col10.Caption = "Cost"
        Col10.ColumnName = "Cost"
        Col10.DefaultValue = 0
        tblPOLine.Columns.Add(Col10)

        Col11.DataType = System.Type.GetType("System.Decimal")
        Col11.AllowDBNull = True
        Col11.Caption = "Amount"
        Col11.ColumnName = "Amount"
        Col11.DefaultValue = 0
        tblPOLine.Columns.Add(Col11)

        Col12.DataType = System.Type.GetType("System.String")
        Col12.AllowDBNull = True
        Col12.Caption = "POLnStatus"
        Col12.ColumnName = "POLnStatus"
        Col12.DefaultValue = 0
        tblPOLine.Columns.Add(Col12)

    End Sub

    Sub CreateGoodsRcvTables()
        Dim Col1 As DataColumn = New DataColumn()
        Dim Col2 As DataColumn = New DataColumn()
        Dim Col3 As DataColumn = New DataColumn()
        Dim Col4 As DataColumn = New DataColumn()
        Dim Col5 As DataColumn = New DataColumn()
        Dim Col6 As DataColumn = New DataColumn()
        Dim Col7 As DataColumn = New DataColumn()
        Dim Col8 As DataColumn = New DataColumn()
        Dim Col9 As DataColumn = New DataColumn()

        Col1.DataType = System.Type.GetType("System.String")
        Col1.AllowDBNull = True
        Col1.Caption = "POID"
        Col1.ColumnName = "POID"
        Col1.DefaultValue = ""
        tblGoodsRcv.Columns.Add(Col1)

        Col2.DataType = System.Type.GetType("System.String")
        Col2.AllowDBNull = True
        Col2.Caption = "ItemCode"
        Col2.ColumnName = "ItemCode"
        Col2.DefaultValue = ""
        tblGoodsRcv.Columns.Add(Col2)

        Col3.DataType = System.Type.GetType("System.String")
        Col3.AllowDBNull = True
        Col3.Caption = "GoodsRcvID"
        Col3.ColumnName = "GoodsRcvID"
        Col3.DefaultValue = ""
        tblGoodsRcv.Columns.Add(Col3)

        Col4.DataType = System.Type.GetType("System.String")
        Col4.AllowDBNull = True
        Col4.Caption = "CreateDate"
        Col4.ColumnName = "CreateDate"
        Col4.DefaultValue = ""
        tblGoodsRcv.Columns.Add(Col4)

        Col5.DataType = System.Type.GetType("System.Decimal")
        Col5.AllowDBNull = True
        Col5.Caption = "ReceiveQty"
        Col5.ColumnName = "ReceiveQty"
        Col5.DefaultValue = 0
        tblGoodsRcv.Columns.Add(Col5)

        Col6.DataType = System.Type.GetType("System.String")
        Col6.AllowDBNull = True
        Col6.Caption = "AccCode"
        Col6.ColumnName = "AccCode"
        Col6.DefaultValue = ""
        tblGoodsRcv.Columns.Add(Col6)

        Col7.DataType = System.Type.GetType("System.String")
        Col7.AllowDBNull = True
        Col7.Caption = "BlkCode"
        Col7.ColumnName = "BlkCode"
        Col7.DefaultValue = ""
        tblGoodsRcv.Columns.Add(Col7)

        Col8.DataType = System.Type.GetType("System.String")
        Col8.AllowDBNull = True
        Col8.Caption = "VehCode"
        Col8.ColumnName = "VehCode"
        Col8.DefaultValue = ""
        tblGoodsRcv.Columns.Add(Col8)

        Col9.DataType = System.Type.GetType("System.String")
        Col9.AllowDBNull = True
        Col9.Caption = "VehExpenseCode"
        Col9.ColumnName = "VehExpenseCode"
        Col9.DefaultValue = ""
        tblGoodsRcv.Columns.Add(Col9)
    End Sub

    Sub CreateInvRcvTables()
        Dim Col1 As DataColumn = New DataColumn()
        Dim Col2 As DataColumn = New DataColumn()
        Dim Col3 As DataColumn = New DataColumn()
        Dim Col4 As DataColumn = New DataColumn()
        Dim Col5 As DataColumn = New DataColumn()
        Dim Col6 As DataColumn = New DataColumn()
        Dim Col7 As DataColumn = New DataColumn()
        Dim Col8 As DataColumn = New DataColumn()
        Dim Col9 As DataColumn = New DataColumn()

        Col1.DataType = System.Type.GetType("System.String")
        Col1.AllowDBNull = True
        Col1.Caption = "POID"
        Col1.ColumnName = "POID"
        Col1.DefaultValue = ""
        tblInvRcv.Columns.Add(Col1)

        Col2.DataType = System.Type.GetType("System.String")
        Col2.AllowDBNull = True
        Col2.Caption = "ItemCode"
        Col2.ColumnName = "ItemCode"
        Col2.DefaultValue = ""
        tblInvRcv.Columns.Add(Col2)

        Col3.DataType = System.Type.GetType("System.String")
        Col3.AllowDBNull = True
        Col3.Caption = "InvoiceRcvID"
        Col3.ColumnName = "InvoiceRcvID"
        Col3.DefaultValue = ""
        tblInvRcv.Columns.Add(Col3)

        Col4.DataType = System.Type.GetType("System.String")
        Col4.AllowDBNull = True
        Col4.Caption = "CreateDate"
        Col4.ColumnName = "CreateDate"
        Col4.DefaultValue = ""
        tblInvRcv.Columns.Add(Col4)

        Col5.DataType = System.Type.GetType("System.Decimal")
        Col5.AllowDBNull = True
        Col5.Caption = "QtyInvoice"
        Col5.ColumnName = "QtyInvoice"
        Col5.DefaultValue = 0
        tblInvRcv.Columns.Add(Col5)

        Col6.DataType = System.Type.GetType("System.String")
        Col6.AllowDBNull = True
        Col6.Caption = "AccCode"
        Col6.ColumnName = "AccCode"
        Col6.DefaultValue = ""
        tblInvRcv.Columns.Add(Col6)

        Col7.DataType = System.Type.GetType("System.String")
        Col7.AllowDBNull = True
        Col7.Caption = "BlkCode"
        Col7.ColumnName = "BlkCode"
        Col7.DefaultValue = ""
        tblInvRcv.Columns.Add(Col7)

        Col8.DataType = System.Type.GetType("System.String")
        Col8.AllowDBNull = True
        Col8.Caption = "VehCode"
        Col8.ColumnName = "VehCode"
        Col8.DefaultValue = ""
        tblInvRcv.Columns.Add(Col8)

        Col9.DataType = System.Type.GetType("System.String")
        Col9.AllowDBNull = True
        Col9.Caption = "VehExpenseCode"
        Col9.ColumnName = "VehExpenseCode"
        Col9.DefaultValue = ""
        tblInvRcv.Columns.Add(Col9)
    End Sub
End Class
