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

Public Class PU_StdRpt_HistoricalServicePrice_Preview : Inherits Page

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

        'Dim strOpCdPO_GET As String = "PU_STDRPT_HISTORICAL_SERVICE_PRICE_GET"
        Dim strOpCdPO_GET As String = "PU_STDRPT_HISTORICAL_SERVICE_GET"
        Dim strOpCdItem_GET As String = "PU_STDRPT_ITEM_GET"

        Dim strParam As String
        Dim strParamItm As String
        Dim strItemCode As String
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
            SearchStr = SearchStr & "PO.POID IN (SELECT SUBPO.POID FROM PU_PO SUBPO WHERE SUBPO.POID >= ''" & Request.QueryString("DocNoFrom") & _
                        "'' AND SUBPO.POID <= ''" & Request.QueryString("DocNoTo") & "'') AND "
        End If

        If Not Request.QueryString("Supplier") = "" Then
            SearchStr = SearchStr & "(PO.SupplierCode LIKE ''%" & Request.QueryString("Supplier") & "%'' OR SPL.Name LIKE ''%" & Request.QueryString("Supplier") & "%'') AND "
        Else
            SearchStr = SearchStr & "PO.SupplierCode LIKE ''%'' AND "
        End If

        If Not Request.QueryString("ItemCode") = "" Then
            SearchStr = SearchStr & "POLN.Catatan LIKE ''%" & Request.QueryString("ItemCode") & "%'' AND "
        End If

        If Not Request.QueryString("AddNote") = "" Then
            SearchStr = SearchStr & "POLN.AdditionalNote LIKE ''%" & Request.QueryString("AddNote") & "%'' AND "
        End If

        'If Not Request.QueryString("ItemCode") = "" Then
        '    itemSelectStr = itemSelectStr & "ITM.ItemCode LIKE '" & Request.QueryString("ItemCode") & "' AND "
        'Else
        '    itemSelectStr = itemSelectStr & "ITM.ItemCode LIKE '%' AND "
        'End If

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
                'SearchStr = SearchStr & "PO.Status LIKE '%' AND "
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


        If Not SearchStr = "" Then
            If Right(SearchStr, 4) = "AND " Then
                SearchStr = Left(SearchStr, Len(SearchStr) - 4)
            End If

            'If Right(itemSelectStr, 4) = "AND " Then
            '    itemSelectStr = Left(itemSelectStr, Len(itemSelectStr) - 4)
            'End If

            If Not Request.QueryString("ItemCode") = "" Then
                strParam = strUserLoc1 & "|" & strDDLAccMth & "|" & strDDLAccYr & "|" & "" & "|" & "" & "|" & SearchStr & "|" & NormStr
            Else
                strParam = strUserLoc1 & "|" & strDDLAccMth & "|" & strDDLAccYr & "|" & "" & "|" & "" & "|" & SearchStr & "|" & WildStr
            End If

        End If

        Try
            intErrNo = objPU.mtdGetReport_POList(strOpCdPO_GET, strParam, objRptDs, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_STDRPT_PO_LIST&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        'For intCnt = 0 To objRptDs.Tables(0).Rows.Count - 1
        '    If Not IsDBNull(objRptDs.Tables(0).Rows(intCnt).Item("ItemCode")) Then
        '        strItemCode = Trim(objRptDs.Tables(0).Rows(intCnt).Item("ItemCode"))
        '        strParamItm = strItemCode & "|"
        '        Try
        '            intErrNo = objPU.mtdGetItem(strOpCdItem_GET, _
        '                                        strCompany, _
        '                                        strLocation, _
        '                                        strUserId, _
        '                                        strAccMonth, _
        '                                        strAccYear, _
        '                                        strParamItm, _
        '                                        objItem)
        '        Catch Exp As System.Exception
        '            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_ITEM_FOR_POLIST_REPORT&errmesg=" & lblErrMessage.Text & "&redirect=")
        '        End Try

        '        If objItem.Tables(0).Rows.Count > 0 Then
        '            'objRptDs.Tables(0).Rows(intCnt).Item("ItemDesc") = Trim(objItem.Tables(0).Rows(0).Item("Description"))
        '            'objRptDs.Tables(0).Rows(intCnt).Item("PurchaseUOM") = Trim(objItem.Tables(0).Rows(0).Item("PurchaseUOM"))
        '        End If
        '    End If
        'Next intCnt

        'If Not Request.QueryString("ItemCode") = "" Then
        '    If objRptDs.Tables(0).Rows.Count > 0 Then
        '        Do
        '            If IsDBNull(objRptDs.Tables(0).Rows(intCntRem).Item("POLnID")) Then
        '                objRptDs.Tables(0).Rows.RemoveAt(intCntRem)

        '                If intCntRem <> 0 Then
        '                    intCntRem = intCntRem - 1
        '                Else
        '                    intCntRem = 0
        '                End If
        '            Else
        '                intCntRem = intCntRem + 1
        '            End If
        '        Loop While intCntRem <= objRptDs.Tables(0).Rows.Count - 1
        '    End If
        'Else
        '    If objRptDs.Tables(0).Rows.Count > 0 Then
        '        Do
        '            If IsDBNull(objRptDs.Tables(0).Rows(intCntRem).Item("POLnID")) Then
        '                objRptDs.Tables(0).Rows.RemoveAt(intCntRem)

        '                If intCntRem <> 0 Then
        '                    intCntRem = intCntRem - 1
        '                Else
        '                    intCntRem = 0
        '                End If
        '            Else
        '                intCntRem = intCntRem + 1
        '            End If
        '        Loop While intCntRem <= objRptDs.Tables(0).Rows.Count - 1
        '    End If
        'End If


        Dim strHistoryBy As String
        Dim strRptPrefix As String

        strHistoryBy = Request.QueryString("HistoryBy")
        If strHistoryBy = "1" Then
            strRptPrefix = "PU_StdRpt_HistoricalServicePriceByService"
        ElseIf strHistoryBy = "2" Then
            strRptPrefix = "PU_StdRpt_HistoricalServicePriceByItem"
        Else
            strRptPrefix = "PU_StdRpt_HistoricalServicePriceBySupplier"
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
            ' .ExportFormatType = ExportFormatType.PortableDocFormat

            If strExportToExcel = "0" Then
                .ExportFormatType = ExportFormatType.PortableDocFormat
            Else
                .ExportFormatType = ExportFormatType.Excel
            End If

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
        paramField14 = paramFields.Item("ParamItemCode")
        paramField15 = paramFields.Item("ParamPOType")
        paramField16 = paramFields.Item("ParamStatus")
        paramField17 = paramFields.Item("lblLocation")

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
        ParamDiscreteValue14.Value = Request.QueryString("ItemCode")
        ParamDiscreteValue15.Value = Request.QueryString("POType")
        ParamDiscreteValue16.Value = Request.QueryString("Status")
        ParamDiscreteValue17.Value = Request.QueryString("lblLocation")

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
    End Sub
End Class
