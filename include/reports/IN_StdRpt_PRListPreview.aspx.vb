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

Public Class IN_StdRpt_PRList_Preview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objIN As New agri.IN.clsReport()
    Dim objINTrx As New agri.IN.clsTrx()
    Dim objAdmin As New agri.Admin.clsShare()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strUserLoc As String
    Dim strLocLevel as string 

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

        tempLoc = Request.QueryString("Location")
        If Right(tempLoc, 1) = "," Then
            Session("SS_LOC") = Left(tempLoc, Len(tempLoc) - 1)
        Else
            Session("SS_LOC") = tempLoc.Replace("','", ", ")
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
        strLocLevel = session("SS_LOCLEVEL")

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
        Dim itmSearchStr As String

        Dim strParam As String
        Dim strPRID As String
        Dim decQtyOut As Decimal

        Dim strUserLoc1 as string

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOpCdPR_GET As String = "IN_STDRPT_PR_AND_PRLN_GET"

        Dim WildStr As String = "AND PR.PRID *= PRLn.PRID "
        Dim NormStr As String = "AND PR.PRID = PRLn.PRID "

        If Not (Request.QueryString("DateFrom") = "" And Request.QueryString("DateTo") = "") Then
            SearchStr = "AND (DateDiff(Day, '" & Request.QueryString("DateFrom") & "', PR.CreateDate) >= 0) And (DateDiff(Day, '" & Request.QueryString("DateTo") & "', PR.CreateDate) <= 0) "
        End If

        If Not (Request.QueryString("PRNoFrom") = "" And Request.QueryString("PRNoTo") = "") Then
            SearchStr = SearchStr & "AND PR.PRID IN (SELECT SUBPR.PRID FROM IN_PR SUBPR WHERE SUBPR.PRID >= '" & Request.QueryString("PRNoFrom") & _
                        "' AND SUBPR.PRID <= '" & Request.QueryString("PRNoTo") & "') "
        End If

        If Request.QueryString("PRType") = "All" Then
            SearchStr = SearchStr & "AND PR.PRType IN ('" & objINTrx.EnumPurReqDocType.StockPR & "','" & objINTrx.EnumPurReqDocType.DirectChargePR & "','" & objINTrx.EnumPurReqDocType.WorkshopPR & "') "
        ElseIf Request.QueryString("PRType") = objINTrx.EnumPurReqDocType.StockPR Then
            SearchStr = SearchStr & "AND PR.PRType = '" & objINTrx.EnumPurReqDocType.StockPR & "' "
        ElseIf Request.QueryString("PRType") = objINTrx.EnumPurReqDocType.DirectChargePR Then
            SearchStr = SearchStr & "AND PR.PRType = '" & objINTrx.EnumPurReqDocType.DirectChargePR & "' "
        ElseIf Request.QueryString("PRType") = objINTrx.EnumPurReqDocType.WorkshopPR Then
            SearchStr = SearchStr & "AND PR.PRType = '" & objINTrx.EnumPurReqDocType.WorkshopPR & "' "
        End If

        If Request.QueryString("PRStatus") = "All" Then
        ElseIf Request.QueryString("PRStatus") = "Closed" Then
            SearchStr = SearchStr & "AND PR.Status = '" & objINTrx.EnumPurReqStatus.Fulfilled & "' "
        ElseIf Request.QueryString("PRStatus") = "Outstanding" Then
            SearchStr = SearchStr & "AND PR.Status = '" & objINTrx.EnumPurReqStatus.Confirmed & "' "
        ElseIf Request.QueryString("PRStatus") = "Active" Then
            SearchStr = SearchStr & "AND PR.Status = '" & objINTrx.EnumPurReqStatus.Active & "' "
        ElseIf Request.QueryString("PRStatus") = "Cancelled" Then
            SearchStr = SearchStr & "AND PR.Status = '" & objINTrx.EnumPurReqStatus.Cancelled & "' "
        ElseIf Request.QueryString("PRStatus") = "Deleted" Then
            SearchStr = SearchStr & "AND PR.Status = '" & objINTrx.EnumPurReqStatus.Deleted & "' "
        End If

        If Not Request.QueryString("ProdType") = "" Then
            itmSearchStr = "ITM.ProdTypeCode LIKE '" & Request.QueryString("ProdType") & "' AND "
        End If

        If Not Request.QueryString("ProdBrand") = "" Then
            itmSearchStr = itmSearchStr & "ITM.ProdBrandCode LIKE '" & Request.QueryString("ProdBrand") & "' AND "
        End If

        If Not Request.QueryString("ProdModel") = "" Then
            itmSearchStr = itmSearchStr & "ITM.ProdModelCode LIKE '" & Request.QueryString("ProdModel") & "' AND "
        End If

        If Not Request.QueryString("ProdCat") = "" Then
            itmSearchStr = itmSearchStr & "ITM.ProdCatCode LIKE '" & Request.QueryString("ProdCat") & "' AND "
        End If

        If Not Request.QueryString("ProdMat") = "" Then
            itmSearchStr = itmSearchStr & "ITM.ProdMatCode LIKE '" & Request.QueryString("ProdMat") & "' AND "
        End If

        If Not Request.QueryString("StkAna") = "" Then
            itmSearchStr = itmSearchStr & "ITM.StockAnalysisCode LIKE '" & Request.QueryString("StkAna") & "' AND "
        End If

        If Not Request.QueryString("ItemCode") = "" Then
            SearchStr = SearchStr & "AND PRLN.ItemCode LIKE '" & Request.QueryString("ItemCode") & "' "
        End If

        If Request.QueryString("PRLnStatus") = "All" Then
            SearchStr = SearchStr & "AND PRLN.Status LIKE '%' "
        ElseIf Request.QueryString("PRLnStatus") = "Closed" Then
            SearchStr = SearchStr & "AND PRLN.QtyOutstanding = 0 "
        ElseIf Request.QueryString("PRLnStatus") = "Outstanding" Then
            SearchStr = SearchStr & "AND PRLN.QtyOutstanding <> 0 "
        End If




        If strLocLevel = "1" then
            strUserLoc1 = " WHERE PR.LocCode IN ('" & strUserLoc & "') "   
        ElseIf strLocLevel = "2" then    
            strUserLoc1 = " where not exists (select loccode from sh_location where loclevel = '3' and loccode = pr.loccode) "     
        Else  
            strUserLoc1 = " where not exists (select loccode from sh_location where loclevel = '2' and loccode = pr.loccode) "     
        End If

        If Not itmSearchStr = "" Then
            If Right(itmSearchStr, 4) = "AND " Then
                itmSearchStr = Left(itmSearchStr, Len(itmSearchStr) - 4)
            End If
            itmSearchStr = "WHERE " & itmSearchStr
        End If


        strParam = strUserLoc1 & "|" & strAccMonth & "|" & strAccYear & "|" & itmSearchStr & "|" & NormStr & SearchStr
        Try
            intErrNo = objIN.mtdGetReport_PurchaseRequestList(strOpCdPR_GET, strParam, objRptDs, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_IN_PR_LIST_REPORT&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objRptDs.Tables(0).Rows.Count - 1
            strPRID = Trim(objRptDs.Tables(0).Rows(intCnt).Item("PRID"))
            decQtyOut = Trim(objRptDs.Tables(0).Rows(intCnt).Item("QtyOutstanding"))

            If Not intCnt = objRptDs.Tables(0).Rows.Count - 1 Then
                If strPRID = Trim(objRptDs.Tables(0).Rows(intCnt + 1).Item("PRID")) Then
                    If decQtyOut = 0 And objRptDs.Tables(0).Rows(intCnt + 1).Item("QtyOutstanding") = 0 Then
                        objRptDs.Tables(0).Rows(intCnt).BeginEdit()
                        objRptDs.Tables(0).Rows(intCnt).Item("PRStatus") = "Closed"
                        objRptDs.Tables(0).Rows(intCnt).EndEdit()
                    Else
                        objRptDs.Tables(0).Rows(intCnt).BeginEdit()
                        objRptDs.Tables(0).Rows(intCnt).Item("PRStatus") = "Outstanding"
                        objRptDs.Tables(0).Rows(intCnt).EndEdit()
                    End If
                Else
                    If objRptDs.Tables(0).Rows(intCnt).Item("QtyOutstanding") = 0 Then
                        objRptDs.Tables(0).Rows(intCnt).BeginEdit()
                        objRptDs.Tables(0).Rows(intCnt).Item("PRStatus") = "Closed"
                        objRptDs.Tables(0).Rows(intCnt).EndEdit()
                    Else
                        objRptDs.Tables(0).Rows(intCnt).BeginEdit()
                        objRptDs.Tables(0).Rows(intCnt).Item("PRStatus") = "Outstanding"
                        objRptDs.Tables(0).Rows(intCnt).EndEdit()
                    End If
                End If
            Else
                If objRptDs.Tables(0).Rows(intCnt).Item("QtyOutstanding") = 0 Then
                    objRptDs.Tables(0).Rows(intCnt).BeginEdit()
                    objRptDs.Tables(0).Rows(intCnt).Item("PRStatus") = "Closed"
                    objRptDs.Tables(0).Rows(intCnt).EndEdit()
                Else
                    objRptDs.Tables(0).Rows(intCnt).BeginEdit()
                    objRptDs.Tables(0).Rows(intCnt).Item("PRStatus") = "Outstanding"
                    objRptDs.Tables(0).Rows(intCnt).EndEdit()
                End If
            End If
        Next


        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\IN_StdRpt_PRList.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3

        crvView.Visible = False                         
        crvView.ReportSource = rdCrystalViewer          
        crvView.DataBind()                              

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\IN_StdRpt_PRList.pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/IN_StdRpt_PRList.pdf"">")
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

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("ParamCompanyName")
        paramField2 = paramFields.Item("ParamLocation")
        paramField3 = paramFields.Item("ParamUserName")
        paramField4 = paramFields.Item("ParamDecimal")
        paramField5 = paramFields.Item("ParamDateFrom")
        paramField6 = paramFields.Item("ParamDateTo")
        paramField7 = paramFields.Item("ParamPRNoFrom")
        paramField8 = paramFields.Item("ParamPRNoTo")
        paramField9 = paramFields.Item("ParamPRStatus")
        paramField10 = paramFields.Item("ParamPRLnStatus")
        paramField11 = paramFields.Item("ParamItemCode")
        paramField12 = paramFields.Item("ParamAccMonth")
        paramField13 = paramFields.Item("ParamAccYear")
        paramField14 = paramFields.Item("lblLocation")
        paramField15 = paramFields.Item("ParamRptID")
        paramField16 = paramFields.Item("ParamRptName")
        paramField17 = paramFields.Item("ParamPRType")
        paramField18 = paramFields.Item("ParamProdType")
        paramField19 = paramFields.Item("ParamProdBrand")
        paramField20 = paramFields.Item("ParamProdModel")
        paramField21 = paramFields.Item("ParamProdCat")
        paramField22 = paramFields.Item("ParamProdMat")
        paramField23 = paramFields.Item("ParamStkAna")
        paramField24 = paramFields.Item("lblProdTypeCode")
        paramField25 = paramFields.Item("lblProdBrandCode")
        paramField26 = paramFields.Item("lblProdModelCode")
        paramField27 = paramFields.Item("lblProdCatCode")
        paramField28 = paramFields.Item("lblProdMatCode")
        paramField29 = paramFields.Item("lblStkAnaCode")

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
        crParameterValues29 = paramField28.CurrentValues

        ParamDiscreteValue1.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue2.Value = Session("SS_LOC")
        ParamDiscreteValue3.Value = Session("SS_USERNAME")
        ParamDiscreteValue4.Value = Request.QueryString("Decimal")
        ParamDiscreteValue5.Value = Request.QueryString("DateFrom")
        ParamDiscreteValue6.Value = Request.QueryString("DateTo")
        ParamDiscreteValue7.Value = Request.QueryString("PRNoFrom")
        ParamDiscreteValue8.Value = Request.QueryString("PRNoTo")
        ParamDiscreteValue9.Value = Request.QueryString("PRStatus")
        ParamDiscreteValue10.Value = Request.QueryString("PRLnStatus")
        ParamDiscreteValue11.Value = Request.QueryString("ItemCode")
        ParamDiscreteValue12.Value = Session("SS_INACCMONTH")
        ParamDiscreteValue13.Value = Session("SS_INACCYEAR")
        ParamDiscreteValue14.Value = Request.QueryString("lblLocation")
        ParamDiscreteValue15.Value = Request.QueryString("RptID")
        ParamDiscreteValue16.Value = Request.QueryString("RptName")
        ParamDiscreteValue17.Value = Request.QueryString("PRType")
        ParamDiscreteValue18.Value = Request.QueryString("ProdType")
        ParamDiscreteValue19.Value = Request.QueryString("ProdBrand")
        ParamDiscreteValue20.Value = Request.QueryString("ProdModel")
        ParamDiscreteValue21.Value = Request.QueryString("ProdCat")
        ParamDiscreteValue22.Value = Request.QueryString("ProdMat")
        ParamDiscreteValue23.Value = Request.QueryString("StkAna")
        ParamDiscreteValue24.Value = Request.QueryString("lblProdTypeCode")
        ParamDiscreteValue25.Value = Request.QueryString("lblProdBrandCode")
        ParamDiscreteValue26.Value = Request.QueryString("lblProdModelCode")
        ParamDiscreteValue27.Value = Request.QueryString("lblProdCatCode")
        ParamDiscreteValue28.Value = Request.QueryString("lblProdMatCode")
        ParamDiscreteValue29.Value = Request.QueryString("lblStkAnaCode")

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
        crParameterValues29.Add(ParamDiscreteValue28)

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

        crvView.ParameterFieldInfo = paramFields
    End Sub
End Class
