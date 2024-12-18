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
Imports Microsoft.VisualBasic.Information
Imports System.Xml
Imports System.Web.Services
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web

Public Class IN_StdRpt_FuelIssueList_Preview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objIN As New agri.IN.clsReport()
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

    Dim tempLoc As String
    Dim intDecimal As Integer
    Dim strBlkType As String
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

        strBlkType = Request.QueryString("BlkType")
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


        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            BindReport()
        End If
    End Sub

    Sub BindReport()

        Dim objRptDs As New DataSet()
        Dim objVehicle As New DataSet()
        Dim objMapPath As String

        Dim intCnt As Integer
        Dim intCntRem As Integer

        Dim SearchStr As String
        Dim itemSelectStr As String
        Dim vehSelectStr As String
        Dim blkSelectStr As String
        Dim itemSearchStr As String
        Dim vehSearchStr As String
        Dim blkSearchStr As String

        Dim strOpCdFuelIssue As String

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOpCdFuelIssue_BlkGrp_And_Blk_GET As String = "IN_STDRPT_FUELISSUE_AND_FUELISSUELN_BLKGRP_AND_BLK_GET"
        Dim strOpCdFuelIssue_SubBlk_GET As String = "IN_STDRPT_FUELISSUE_AND_FUELISSUELN_SUBBLK_GET"
        Dim strOpCdFuelIssueListGet As String = "IN_STDRPT_FUELISSUE_LISTING_GET"
        Dim strOpCdVehicle_GET As String = "IN_STDRPT_VEHICLE_GET"

        Dim strParamVeh As String
        Dim strVehCode As String

        Dim strParam As String

        Dim WildStr As String = "AND ISS.FuelIssueID *= ISSLN.FuelIssueID "
        Dim NormStr As String = "AND ISS.FuelIssueID = ISSLN.FuelIssueID "

    SearchStr = ""
        
        If Not (Request.QueryString("DateFrom") = "" And Request.QueryString("DateTo") = "") Then
            SearchStr = SearchStr & "  AND (DateDiff(Day, '" & Request.QueryString("DateFrom") & "', ISS.CreateDate) >= 0) AND (DateDiff(Day, '" & Request.QueryString("DateTo") & "', ISS.CreateDate) <= 0)" & vbCrLf
        End If

        If Not (Request.QueryString("FuelIssueIDFrom") = "" And Request.QueryString("FuelIssueIDTo") = "") Then
            SearchStr = SearchStr & "  AND ISS.FuelIssueID >= '" & Request.QueryString("FuelIssueIDFrom") & "' AND ISS.FuelIssueID <= '" & Request.QueryString("FuelIssueIDTo") & "'" & vbCrLf
        End If

        If Not Request.QueryString("FuelIssueType") = objINTrx.mtdGetFuelIssueType(objINTrx.EnumFuelIssueType.All) Then
            SearchStr = SearchStr & "  AND ISS.IssueType = '" & Request.QueryString("FuelIssueType") & "'" & vbCrLf
        End If
        
        If Not Request.QueryString("Status") = "" Then
            If Not Request.QueryString("Status") = objINTrx.EnumStockIssueStatus.All Then
                SearchStr = SearchStr & "  AND ISS.Status = '" & Request.QueryString("Status") & "'" & vbCrLf
            End If
        End If

        If Not Request.QueryString("AccCode") = "" Then
            SearchStr = SearchStr & "  AND ISSLN.AccCode LIKE '" & Request.QueryString("AccCode") & "'" & vbCrLf
        End If
        
        If strBlkType = "BlkGrp" Then
            If Not Request.QueryString("BlkGrp") = "" Then
                SearchStr = SearchStr & "  AND ISSLN.BlkCode IN (SELECT BlkCode FROM GL_BLOCK WHERE BlkGrpCode LIKE '" & Request.QueryString("BlkGrp") & "')" & vbCrLf
            End If
            Session("SS_BLKGRPHEADER") = "BlkGrp"
        ElseIf strBlkType = "BlkCode" Then
            If Not Request.QueryString("BlkCode") = "" Then
                SearchStr = SearchStr & "  AND ISSLN.BlkCode LIKE '" & Request.QueryString("BlkCode") & "'" & vbCrLf
            End If
            Session("SS_BLKHEADER") = "BlkCode"
        ElseIf strBlkType = "SubBlkCode" Then
            If Not Request.QueryString("SubBlkCode") = "" Then
                SearchStr = SearchStr & "  AND ISSLN.BlkCode LIKE '" & Request.QueryString("SubBlkCode") & "'" & vbCrLf
            End If
            Session("SS_SUBBLKHEADER") = "SubBlkCode"
        End If
        
        If Not (Request.QueryString("VehCode") = "" And Request.QueryString("VehTypeCode") = "") Then
            SearchStr = SearchStr & "  AND ISSLN.VehCode IN (SELECT VEH.VehCode FROM GL_VEHICLE VEH WHERE "
            If Not Request.QueryString("VehCode") = "" Then
                SearchStr = SearchStr & "VEH.VehCode LIKE '" & Request.QueryString("VehCode") & "'"
            End If
            If Not Request.QueryString("VehTypeCode") = "" Then
                If Not Request.QueryString("VehCode") = "" Then
                    SearchStr = SearchStr & " AND "
                End If
                SearchStr = SearchStr & "VEH.VehTypeCode LIKE '" & Request.QueryString("VehTypeCode") & "'"
            End If
            SearchStr = SearchStr & ")" & vbCrLf
        End If

        If Not Request.QueryString("VehExpCode") = "" Then
            SearchStr = SearchStr & "  AND ISSLN.VehExpCode LIKE '" & Request.QueryString("VehExpCode") & "'" & vbCrLf
        End If
        
        If Not Request.QueryString("ProdType") = "" Then
            itemSelectStr = "ITM.ProdTypeCode LIKE '" & Request.QueryString("ProdType") & "' AND "
            SearchStr = SearchStr & "  AND ITM.ProdTypeCode LIKE '" & Request.QueryString("ProdType") & "' " & vbCrLf
        End If

        If Not Request.QueryString("ProdBrand") = "" Then
            SearchStr = SearchStr & "  AND ITM.ProdBrandCode LIKE '" & Request.QueryString("ProdBrand") & "' " & vbCrLf
        End If

        If Not Request.QueryString("ProdModel") = "" Then
            SearchStr = SearchStr & "  AND ITM.ProdModelCode LIKE '" & Request.QueryString("ProdModel") & "' " & vbCrLf
        End If

        If Not Request.QueryString("ProdCat") = "" Then
            SearchStr = SearchStr & "  AND ITM.ProdCatCode LIKE '" & Request.QueryString("ProdCat") & "' " & vbCrLf
        End If

        If Not Request.QueryString("ProdMat") = "" Then
            SearchStr = SearchStr & "  AND ITM.ProdMatCode LIKE '" & Request.QueryString("ProdMat") & "' " & vbCrLf
        End If

        If Not Request.QueryString("StkAna") = "" Then
            SearchStr = SearchStr & "  AND ITM.StockAnalysisCode LIKE '" & Request.QueryString("StkAna") & "' " & vbCrLf
        End If

        If Not Request.QueryString("ItemCode") = "" Then
            SearchStr = SearchStr & "  AND ITM.ItemCode LIKE '" & Request.QueryString("ItemCode") & "' " & vbCrLf
        End If
        
        strParam = strUserLoc & "|" & strDDLAccMth & "|" & strDDLAccYr & "|||||||" & SearchStr
        strOpCdFuelIssue = strOpCdFuelIssueListGet

        Try
            intErrNo = objIN.mtdGetReport_FuelIssueList(strOpCdFuelIssue, strParam, objRptDs, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_FUELISSUE_LIST_REPORT&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If Not Request.QueryString("AccCode") = "" Or Not Request.QueryString("VehTypeCode") = "" Or Not Request.QueryString("VehCode") = "" Or Not Request.QueryString("VehExpCode") = "" Or Not Request.QueryString("BlkGrp") = "" Or Not _
               Request.QueryString("BlkCode") = "" Or Not Request.QueryString("SubBlkCode") = "" Or Not Request.QueryString("ItemCode") = "" Or Not Request.QueryString("ProdType") = "" Or Not Request.QueryString("ProdBrand") = "" Or Not _
               Request.QueryString("ProdModel") = "" Or Not Request.QueryString("ProdCat") = "" Or Not Request.QueryString("ProdMat") = "" Or Not Request.QueryString("StkAna") = "" Then
            If objRptDs.Tables(0).Rows.Count > 0 Then
                Do
                    If IsDBNull(objRptDs.Tables(0).Rows(intCntRem).Item("FuelIssueLnId")) Then
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


        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\IN_StdRpt_FuelIssueList.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3

        crvView.Visible = False                          
        crvView.ReportSource = rdCrystalViewer           
        crvView.DataBind()                              

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\IN_StdRpt_FuelIssueList.pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/IN_StdRpt_FuelIssueList.pdf"">")
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
        Dim paramField35 As New ParameterField()
        Dim paramField36 As New ParameterField()
        Dim paramField37 As New ParameterField()
        Dim paramField38 As New ParameterField()
        Dim paramField39 As New ParameterField()
        Dim paramField40 As New ParameterField()
        Dim paramField41 As New ParameterField()
        Dim paramField42 As New ParameterField()
        Dim paramField43 As New ParameterField()
        Dim paramField44 As New ParameterField()
        Dim paramField45 As New ParameterField()
        Dim paramField46 As New ParameterField()
        Dim paramField47 As New ParameterField()

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
        Dim ParamDiscreteValue35 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue36 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue37 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue38 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue39 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue40 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue41 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue42 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue43 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue44 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue45 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue46 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue47 As New ParameterDiscreteValue()

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
        Dim crParameterValues35 As ParameterValues
        Dim crParameterValues36 As ParameterValues
        Dim crParameterValues37 As ParameterValues
        Dim crParameterValues38 As ParameterValues
        Dim crParameterValues39 As ParameterValues
        Dim crParameterValues40 As ParameterValues
        Dim crParameterValues41 As ParameterValues
        Dim crParameterValues42 As ParameterValues
        Dim crParameterValues43 As ParameterValues
        Dim crParameterValues44 As ParameterValues
        Dim crParameterValues45 As ParameterValues
        Dim crParameterValues46 As ParameterValues
        Dim crParameterValues47 As ParameterValues

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("ParamLocation")
        paramField2 = paramFields.Item("ParamDateFrom")
        paramField3 = paramFields.Item("ParamDateTo")
        paramField4 = paramFields.Item("ParamFuelIssueIDFrom")
        paramField5 = paramFields.Item("ParamFuelIssueIDTo")
        paramField6 = paramFields.Item("ParamFuelIssueType")
        paramField7 = paramFields.Item("ParamAccCode")
        paramField8 = paramFields.Item("ParamVehCode")
        paramField9 = paramFields.Item("ParamVehExpCode")
        paramField10 = paramFields.Item("ParamBlkCode")
        paramField11 = paramFields.Item("ParamSubBlkCode")
        paramField12 = paramFields.Item("ParamStatus")
        paramField13 = paramFields.Item("ParamBlkOrSubBlk")
        paramField14 = paramFields.Item("ParamCompanyName")
        paramField15 = paramFields.Item("ParamUserName")
        paramField16 = paramFields.Item("ParamDecimal")
        paramField17 = paramFields.Item("ParamRptID")
        paramField18 = paramFields.Item("ParamRptName")
        paramField19 = paramFields.Item("ParamAccMonth")
        paramField20 = paramFields.Item("ParamAccYear")
        paramField21 = paramFields.Item("lblAccCode")
        paramField22 = paramFields.Item("lblVehCode")
        paramField23 = paramFields.Item("lblVehExpCode")
        paramField24 = paramFields.Item("lblBlkCode")
        paramField25 = paramFields.Item("lblSubBlkCode")
        paramField26 = paramFields.Item("ParamItemCode")
        paramField27 = paramFields.Item("lblLocation")
        paramField28 = paramFields.Item("ParamProdType")
        paramField29 = paramFields.Item("ParamProdBrand")
        paramField30 = paramFields.Item("ParamProdModel")
        paramField31 = paramFields.Item("ParamProdCat")
        paramField32 = paramFields.Item("ParamProdMat")
        paramField33 = paramFields.Item("ParamStkAna")
        paramField34 = paramFields.Item("lblProdTypeCode")
        paramField35 = paramFields.Item("lblProdBrandCode")
        paramField36 = paramFields.Item("lblProdModelCode")
        paramField37 = paramFields.Item("lblProdCatCode")
        paramField38 = paramFields.Item("lblProdMatCode")
        paramField39 = paramFields.Item("lblStkAnaCode")
        paramField40 = paramFields.Item("ParamBlkType")
        paramField41 = paramFields.Item("ParamBlkGrp")
        paramField42 = paramFields.Item("lblBlkGrp")
        paramField43 = paramFields.Item("lblVehTypeCode")
        paramField44 = paramFields.Item("ParamVehTypeCode")
        paramField45 = paramFields.Item("lblBillParty")
        paramField46 = paramFields.Item("lblVehDesc")
        paramField47 = paramFields.Item("ParamInterEstateEnabled")

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
        crParameterValues35 = paramField35.CurrentValues
        crParameterValues36 = paramField36.CurrentValues
        crParameterValues37 = paramField37.CurrentValues
        crParameterValues38 = paramField38.CurrentValues
        crParameterValues39 = paramField39.CurrentValues
        crParameterValues40 = paramField40.CurrentValues
        crParameterValues41 = paramField41.CurrentValues
        crParameterValues42 = paramField42.CurrentValues
        crParameterValues43 = paramField43.CurrentValues
        crParameterValues44 = paramField44.CurrentValues
        crParameterValues45 = paramField45.CurrentValues
        crParameterValues46 = paramField46.CurrentValues
        crParameterValues47 = paramField47.CurrentValues

        ParamDiscreteValue1.Value = Session("SS_LOC")
        ParamDiscreteValue2.Value = Request.QueryString("DateFrom")
        ParamDiscreteValue3.Value = Request.QueryString("DateTo")
        ParamDiscreteValue4.Value = Request.QueryString("FuelIssueIDFrom")
        ParamDiscreteValue5.Value = Request.QueryString("FuelIssueIDTo")
        ParamDiscreteValue6.Value = Request.QueryString("FuelIssueType")
        ParamDiscreteValue7.Value = Request.QueryString("AccCode")
        ParamDiscreteValue8.Value = Request.QueryString("VehCode")
        ParamDiscreteValue9.Value = Request.QueryString("VehExpCode")
        ParamDiscreteValue10.Value = Request.QueryString("BlkCode")
        ParamDiscreteValue11.Value = Request.QueryString("SubBlkCode")
        ParamDiscreteValue12.Value = Request.QueryString("Status")

        If strBlkType = "BlkGrp" Then
            ParamDiscreteValue13.Value = Session("SS_BLKGRPHEADER")
        ElseIf strBlkType = "BlkCode" Then
            ParamDiscreteValue13.Value = Session("SS_BLKHEADER")
        ElseIf strBlkType = "SubBlkCode" Then
            ParamDiscreteValue13.Value = Session("SS_SUBBLKHEADER")
        End If

        ParamDiscreteValue14.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue15.Value = Session("SS_USERNAME")
        ParamDiscreteValue16.Value = Request.QueryString("Decimal")
        ParamDiscreteValue17.Value = Request.QueryString("RptID")
        ParamDiscreteValue18.Value = Request.QueryString("RptName")
        ParamDiscreteValue19.Value = Request.QueryString("DDLAccMth")
        ParamDiscreteValue20.Value = Request.QueryString("DDLAccYr")
        ParamDiscreteValue21.Value = Request.QueryString("lblAccCode")
        ParamDiscreteValue22.Value = Request.QueryString("lblVehCode")
        ParamDiscreteValue23.Value = Request.QueryString("lblVehExpCode")
        ParamDiscreteValue24.Value = Request.QueryString("lblBlkCode")
        ParamDiscreteValue25.Value = Request.QueryString("lblSubBlkCode")
        ParamDiscreteValue26.Value = Request.QueryString("ItemCode")
        ParamDiscreteValue27.Value = Request.QueryString("lblLocation")
        ParamDiscreteValue28.Value = Request.QueryString("ProdType")
        ParamDiscreteValue29.Value = Request.QueryString("ProdBrand")
        ParamDiscreteValue30.Value = Request.QueryString("ProdModel")
        ParamDiscreteValue31.Value = Request.QueryString("ProdCat")
        ParamDiscreteValue32.Value = Request.QueryString("ProdMat")
        ParamDiscreteValue33.Value = Request.QueryString("StkAna")
        ParamDiscreteValue34.Value = Request.QueryString("lblProdTypeCode")
        ParamDiscreteValue35.Value = Request.QueryString("lblProdBrandCode")
        ParamDiscreteValue36.Value = Request.QueryString("lblProdModelCode")
        ParamDiscreteValue37.Value = Request.QueryString("lblProdCatCode")
        ParamDiscreteValue38.Value = Request.QueryString("lblProdMatCode")
        ParamDiscreteValue39.Value = Request.QueryString("lblStkAnaCode")
        ParamDiscreteValue40.Value = Request.QueryString("BlkType")
        ParamDiscreteValue41.Value = Request.QueryString("BlkGrp")
        ParamDiscreteValue42.Value = Request.QueryString("lblBlkGrp")
        ParamDiscreteValue43.Value = Request.QueryString("lblVehTypeCode")
        ParamDiscreteValue44.Value = Request.QueryString("VehTypeCode")
        ParamDiscreteValue45.Value = Request.QueryString("lblBillParty")
        ParamDiscreteValue46.Value = Request.QueryString("lblVehDesc")
        ParamDiscreteValue47.Value = IIf(Session("SS_INTER_ESTATE_CHARGING"), "true", "false")

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
        crParameterValues35.Add(ParamDiscreteValue35)
        crParameterValues36.Add(ParamDiscreteValue36)
        crParameterValues37.Add(ParamDiscreteValue37)
        crParameterValues38.Add(ParamDiscreteValue38)
        crParameterValues39.Add(ParamDiscreteValue39)
        crParameterValues40.Add(ParamDiscreteValue40)
        crParameterValues41.Add(ParamDiscreteValue41)
        crParameterValues42.Add(ParamDiscreteValue42)
        crParameterValues43.Add(ParamDiscreteValue43)
        crParameterValues44.Add(ParamDiscreteValue44)
        crParameterValues45.Add(ParamDiscreteValue45)
        crParameterValues46.Add(ParamDiscreteValue46)
        crParameterValues47.Add(ParamDiscreteValue47)

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
        PFDefs(34).ApplyCurrentValues(crParameterValues35)
        PFDefs(35).ApplyCurrentValues(crParameterValues36)
        PFDefs(36).ApplyCurrentValues(crParameterValues37)
        PFDefs(37).ApplyCurrentValues(crParameterValues38)
        PFDefs(38).ApplyCurrentValues(crParameterValues39)
        PFDefs(39).ApplyCurrentValues(crParameterValues40)
        PFDefs(40).ApplyCurrentValues(crParameterValues41)
        PFDefs(41).ApplyCurrentValues(crParameterValues42)
        PFDefs(42).ApplyCurrentValues(crParameterValues43)
        PFDefs(43).ApplyCurrentValues(crParameterValues44)
        PFDefs(44).ApplyCurrentValues(crParameterValues45)
        PFDefs(45).ApplyCurrentValues(crParameterValues46)
        PFDefs(46).ApplyCurrentValues(crParameterValues47)
        crvView.ParameterFieldInfo = paramFields
    End Sub
End Class
