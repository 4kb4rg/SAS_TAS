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

Public Class IN_StdRpt_MthInvUtilByBlock_Preview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTitle As Label
    Protected WithEvents EventData As DataGrid

    Dim objIN As New agri.IN.clsReport()
    Dim objINTrx As New agri.IN.clsTrx()
    Dim objGLRpt As New agri.GL.clsReport()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strUserLoc As String

    Dim strBlkType As String
    Dim strIssueType As String
    Dim intDecimal As Integer

    Dim strAccMth As String
    Dim strAccYr As String
    Dim strDDLAccMth As String
    Dim strDDLAccYr As String

    Dim rdCrystalViewer As ReportDocument
    Dim intErrNo As Integer
    Dim strItemType As String = ""

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        crvView.Visible = False

        If Right(Request.QueryString("Location"), 1) = "," Then
            strUserLoc = Left(Request.QueryString("Location"), Len(Request.QueryString("Location")) - 1)
        Else
            strUserLoc = Trim(Request.QueryString("Location"))
        End If

        strDDLAccMth = Request.QueryString("DDLAccMth")
        strDDLAccYr = Request.QueryString("DDLAccYr")
        strBlkType = Request.QueryString("BlkType")
        strIssueType = Request.QueryString("IssueType")
        intDecimal = Request.QueryString("Decimal")

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
        Dim objMapPath As String

        Dim StkSearchStr As String
        Dim FuelSearchStr As String
        Dim StkRtnSearchStr As String
        Dim strIndicator As String = ""
        Dim tempLoc As String
        Dim strParam As String

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim strOpCdMthInvUtil_SP As String = "IN_STDRPT_MTH_INV_UTIL_SP"

        tempLoc = Request.QueryString("Location")
        If Right(tempLoc, 1) = "," Then
            Session("SS_LOC") = Left(tempLoc, Len(tempLoc) - 1)
        Else
            Session("SS_LOC") = tempLoc.Replace("','", ", ")
        End If

        If strBlkType = "BlkGrp" Then
            If strIssueType = "Stk" Then
                If Not Request.QueryString("BlkGrp") = "" Then
                    StkSearchStr = "AND BG.BlkGrpCode LIKE '" & Request.QueryString("BlkGrp") & "' "
                    StkRtnSearchStr = "AND BG.BlkGrpCode LIKE '" & Request.QueryString("BlkGrp") & "' "
                End If
                strIndicator = "BLKGRP_STK"
            ElseIf strIssueType = "Fuel" Then
                If Not Request.QueryString("BlkGrp") = "" Then
                    FuelSearchStr = "AND BG.BlkGrpCode LIKE '" & Request.QueryString("BlkGrp") & "' "
                End If
                strIndicator = "BLKGRP_FUL"
            ElseIf strIssueType = "All" Then
                If Not Request.QueryString("BlkGrp") = "" Then
                    StkSearchStr = "AND BG.BlkGrpCode LIKE '" & Request.QueryString("BlkGrp") & "' "
                    FuelSearchStr = "AND BG.BlkGrpCode LIKE '" & Request.QueryString("BlkGrp") & "' "
                    StkRtnSearchStr = "AND BG.BlkGrpCode LIKE '" & Request.QueryString("BlkGrp") & "' "
                End If
                strIndicator = "BLKGRP_ALL"
            End If
            Session("SS_BLKGRPHEADER") = "BlkGrp"
            lblTitle.Text = lblTitle.Text & "Block Listing"

        ElseIf strBlkType = "BlkCode" Then
            If strIssueType = "Stk" Then
                If Not Request.QueryString("BlkCode") = "" Then
                    StkSearchStr = "AND STKISSLN.BlkCode LIKE '" & Request.QueryString("BlkCode") & "' "
                    StkRtnSearchStr = "AND SRL.BlkCode LIKE '" & Request.QueryString("BlkCode") & "' "
                End If
                strIndicator = "BLK_STK"
            ElseIf strIssueType = "Fuel" Then
                If Not Request.QueryString("BlkCode") = "" Then
                    FuelSearchStr = "AND FISSLN.BlkCode LIKE '" & Request.QueryString("BlkCode") & "' "
                End If
                strIndicator = "BLK_FUL"
            ElseIf strIssueType = "All" Then
                If Not Request.QueryString("BlkCode") = "" Then
                    StkSearchStr = "AND STKISSLN.BlkCode LIKE '" & Request.QueryString("BlkCode") & "' "
                    FuelSearchStr = "AND FISSLN.BlkCode LIKE '" & Request.QueryString("BlkCode") & "' "
                    StkRtnSearchStr = "AND SRL.BlkCode LIKE '" & Request.QueryString("BlkCode") & "' "
                End If
                strIndicator = "BLK_ALL"
            End If
            Session("SS_BLKHEADER") = "Block"
            lblTitle.Text = lblTitle.Text & "Block Listing"

        ElseIf strBlkType = "SubBlkCode" Then
            If strIssueType = "Stk" Then
                If Not Request.QueryString("SubBlkCode") = "" Then
                    StkSearchStr = "AND STKISSLN.BlkCode LIKE '" & Request.QueryString("SubBlkCode") & "' "
                    StkRtnSearchStr = "AND SRL.BlkCode LIKE '" & Request.QueryString("SubBlkCode") & "' "
                End If
                strIndicator = "SUBBLK_STK"
            ElseIf strIssueType = "Fuel" Then
                If Not Request.QueryString("SubBlkCode") = "" Then
                    FuelSearchStr = "AND FISSLN.BlkCode LIKE '" & Request.QueryString("SubBlkCode") & "' "
                End If
                strIndicator = "SUBBLK_FUL"
            ElseIf strIssueType = "All" Then
                If Not Request.QueryString("SubBlkCode") = "" Then
                    StkSearchStr = "AND STKISSLN.BlkCode LIKE '" & Request.QueryString("SubBlkCode") & "' "
                    FuelSearchStr = "AND FISSLN.BlkCode LIKE '" & Request.QueryString("SubBlkCode") & "' "
                    StkRtnSearchStr = "AND SRL.BlkCode LIKE '" & Request.QueryString("SubBlkCode") & "' "
                End If
                strIndicator = "SUBBLK_ALL"
            End If
            Session("SS_SUBBLKHEADER") = "SubBlock"
            lblTitle.Text = lblTitle.Text & "Sub Block Listing"
        End If

        If strIssueType = "Stk" Then
            If Not Request.QueryString("ItemCode") = "" Then
                StkSearchStr = StkSearchStr & "AND STKISSLN.ItemCode LIKE '" & Request.QueryString("ItemCode") & "' "
                StkRtnSearchStr = StkRtnSearchStr & "AND SRL.ItemCode LIKE '" & Request.QueryString("ItemCode") & "' "
            End If

            If Not Request.QueryString("AccCode") = "" Then
                StkSearchStr = StkSearchStr & "AND STKISSLN.AccCode LIKE '" & Request.QueryString("AccCode") & "' "
                StkRtnSearchStr = StkRtnSearchStr & "AND SRL.AccCode LIKE '" & Request.QueryString("AccCode") & "' "
            End If

        ElseIf strIssueType = "Fuel" Then
            If Not Request.QueryString("ItemCode") = "" Then
                FuelSearchStr = FuelSearchStr & "AND FISSLN.ItemCode LIKE '" & Request.QueryString("ItemCode") & "' "
            End If

            If Not Request.QueryString("AccCode") = "" Then
                FuelSearchStr = FuelSearchStr & "AND FISSLN.AccCode LIKE '" & Request.QueryString("AccCode") & "' "
            End If

        ElseIf strIssueType = "All" Then
            If Not Request.QueryString("ItemCode") = "" Then
                StkSearchStr = StkSearchStr & "AND STKISSLN.ItemCode LIKE '" & Request.QueryString("ItemCode") & "' "
                FuelSearchStr = FuelSearchStr & "AND FISSLN.ItemCode LIKE '" & Request.QueryString("ItemCode") & "' "
                StkRtnSearchStr = StkRtnSearchStr & "AND SRL.ItemCode LIKE '" & Request.QueryString("ItemCode") & "' "
            End If

            If Not Request.QueryString("AccCode") = "" Then
                StkSearchStr = StkSearchStr & "AND STKISSLN.AccCode LIKE '" & Request.QueryString("AccCode") & "' "
                FuelSearchStr = FuelSearchStr & "AND FISSLN.AccCode LIKE '" & Request.QueryString("AccCode") & "' "
                StkRtnSearchStr = StkRtnSearchStr & "AND SRL.AccCode LIKE '" & Request.QueryString("AccCode") & "' "
            End If
        End If
        If Not Request.QueryString("ProdType") = "" Then
            StkSearchStr = StkSearchStr & "AND ITM.ProdTypeCode LIKE '" & Request.QueryString("ProdType") & "' "
            FuelSearchStr = FuelSearchStr & "AND ITM.ProdTypeCode LIKE '" & Request.QueryString("ProdType") & "' "
            StkRtnSearchStr = StkRtnSearchStr & "AND ITM.ProdTypeCode LIKE '" & Request.QueryString("ProdType") & "' "
        End If

        If Not Request.QueryString("ProdBrand") = "" Then
            StkSearchStr = StkSearchStr & "AND ITM.ProdBrandCode LIKE '" & Request.QueryString("ProdBrand") & "' "
            FuelSearchStr = FuelSearchStr & "AND ITM.ProdBrandCode LIKE '" & Request.QueryString("ProdBrand") & "' "
            StkRtnSearchStr = StkRtnSearchStr & "AND ITM.ProdBrandCode LIKE '" & Request.QueryString("ProdBrand") & "' "
        End If

        If Not Request.QueryString("ProdModel") = "" Then
            StkSearchStr = StkSearchStr & "AND ITM.ProdModelCode LIKE '" & Request.QueryString("ProdModel") & "' "
            FuelSearchStr = FuelSearchStr & "AND ITM.ProdModelCode LIKE '" & Request.QueryString("ProdModel") & "' "
            StkRtnSearchStr = StkRtnSearchStr & "AND ITM.ProdModelCode LIKE '" & Request.QueryString("ProdModel") & "' "
        End If

        If Not Request.QueryString("ProdCat") = "" Then
            StkSearchStr = StkSearchStr & "AND ITM.ProdCatCode LIKE '" & Request.QueryString("ProdCat") & "' "
            FuelSearchStr = FuelSearchStr & "AND ITM.ProdCatCode LIKE '" & Request.QueryString("ProdCat") & "' "
            StkRtnSearchStr = StkRtnSearchStr & "AND ITM.ProdCatCode LIKE '" & Request.QueryString("ProdCat") & "' "
        End If

        If Not Request.QueryString("ProdMat") = "" Then
            StkSearchStr = StkSearchStr & "AND ITM.ProdMatCode LIKE '" & Request.QueryString("ProdMat") & "' "
            FuelSearchStr = FuelSearchStr & "AND ITM.ProdMatCode LIKE '" & Request.QueryString("ProdMat") & "' "
            StkRtnSearchStr = StkRtnSearchStr & "AND ITM.ProdMatCode LIKE '" & Request.QueryString("ProdMat") & "' "
        End If

        If Not Request.QueryString("StkAna") = "" Then
            StkSearchStr = StkSearchStr & "AND ITM.StockAnalysisCode LIKE '" & Request.QueryString("StkAna") & "' "
            FuelSearchStr = FuelSearchStr & "AND ITM.StockAnalysisCode LIKE '" & Request.QueryString("StkAna") & "' "
            StkRtnSearchStr = StkRtnSearchStr & "AND ITM.StockAnalysisCode LIKE '" & Request.QueryString("StkAna") & "' "
        End If




        If (Request.QueryString("WS") = "No")
            strItemType = "AND ITM.ItemType NOT LIKE '4' "
        End If

        strParam = strUserLoc & "|" & strDDLAccMth & "|" & strDDLAccYr & "|" & strIndicator & "|" & _
                   objINTrx.EnumStockIssueStatus.Confirmed & "','" & objINTrx.EnumStockIssueStatus.Closed & "','" & objINTrx.EnumStockIssueStatus.DBNote & "|" & _
                   objINTrx.EnumFuelIssueStatus.Confirmed & "','" & objINTrx.EnumFuelIssueStatus.Closed & "','" & objINTrx.EnumFuelIssueStatus.DBNote & "|" & _
                   objINTrx.EnumStockReturnStatus.Confirmed & "','" & objINTrx.EnumStockReturnStatus.Closed & "|" & _
                   StkSearchStr & "|" & FuelSearchStr & "|" & StkRtnSearchStr & "|" & strItemType  

        Try
            intErrNo = objIN.mtdGetReport_MthInvUtilList(strOpCdMthInvUtil_SP, strLocation, strParam, objRptDs, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_MTHINVUTILBYBLK_LIST_REPORT&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try


        If objRptDs.Tables.Count = 0 Then
            Response.write("<script language=javascript>window.opener.document.body.innerHTML = window.opener.document.body.innerHTML + ""<table border=0><tr><td><span id=lblTemp style='color:Red;'>Please configure double entry setup!</span></td></tr></table>""; window.close();</script>")
            Exit Sub
        End If  











        rdCrystalViewer = New ReportDocument()
        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\IN_StdRpt_MthInvUtilByBlockList.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs) 

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3

        PassParam()

        Dim myTable As CrystalDecisions.CrystalReports.Engine.Table
        Dim myLogin As CrystalDecisions.Shared.TableLogOnInfo
        For Each myTable In rdCrystalViewer.Database.Tables

            If myTable.Name = "IN_StdRpt_DS_MthInvUtilByBlockList" Then
                myLogin = myTable.LogOnInfo

                Try
                    intErrNo = objGLRpt.mtdGet_DBConnObj(myLogin)
                Catch Exp As System.Exception
                    Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=IN_STDRPT_MTHINVUTILBYBLOCKLISTPREVIEW&errmesg=" & Exp.ToString() & "&redirect=")
                End Try

                myTable.ApplyLogOnInfo(myLogin)
                myTable.Location = myLogin.ConnectionInfo.DatabaseName & ".dbo.IN_INVUTIL_RPT"
            End If

        Next

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\IN_StdRpt_MthInvUtilByBlockList.pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/IN_StdRpt_MthInvUtilByBlockList.pdf"">")

        objRptDs.Dispose()
        If Not objRptDs Is Nothing Then
            objRptDs = Nothing
        End If


    End Sub














    Sub PassParam()
        Dim ParamFieldDefs As ParameterFieldDefinitions
        Dim ParamFieldDef1 As ParameterFieldDefinition
        Dim ParamFieldDef2 As ParameterFieldDefinition
        Dim ParamFieldDef3 As ParameterFieldDefinition
        Dim ParamFieldDef4 As ParameterFieldDefinition
        Dim ParamFieldDef5 As ParameterFieldDefinition
        Dim ParamFieldDef6 As ParameterFieldDefinition
        Dim ParamFieldDef7 As ParameterFieldDefinition
        Dim ParamFieldDef8 As ParameterFieldDefinition
        Dim ParamFieldDef9 As ParameterFieldDefinition
        Dim ParamFieldDef10 As ParameterFieldDefinition
        Dim ParamFieldDef11 As ParameterFieldDefinition
        Dim ParamFieldDef12 As ParameterFieldDefinition
        Dim ParamFieldDef13 As ParameterFieldDefinition
        Dim ParamFieldDef14 As ParameterFieldDefinition
        Dim ParamFieldDef15 As ParameterFieldDefinition
        Dim ParamFieldDef16 As ParameterFieldDefinition
        Dim ParamFieldDef17 As ParameterFieldDefinition
        Dim ParamFieldDef18 As ParameterFieldDefinition
        Dim ParamFieldDef19 As ParameterFieldDefinition
        Dim ParamFieldDef20 As ParameterFieldDefinition
        Dim ParamFieldDef21 As ParameterFieldDefinition
        Dim ParamFieldDef22 As ParameterFieldDefinition
        Dim ParamFieldDef23 As ParameterFieldDefinition
        Dim ParamFieldDef24 As ParameterFieldDefinition
        Dim ParamFieldDef25 As ParameterFieldDefinition
        Dim ParamFieldDef26 As ParameterFieldDefinition
        Dim ParamFieldDef27 As ParameterFieldDefinition
        Dim ParamFieldDef28 As ParameterFieldDefinition
        Dim ParamFieldDef29 As ParameterFieldDefinition
        Dim ParamFieldDef30 As ParameterFieldDefinition
        Dim ParamFieldDef31 As ParameterFieldDefinition
        Dim ParamFieldDef32 As ParameterFieldDefinition
        Dim ParamFieldDef33 As ParameterFieldDefinition
        Dim ParamFieldDef34 As ParameterFieldDefinition

        Dim ParameterValues1 As New ParameterValues()
        Dim ParameterValues2 As New ParameterValues()
        Dim ParameterValues3 As New ParameterValues()
        Dim ParameterValues4 As New ParameterValues()
        Dim ParameterValues5 As New ParameterValues()
        Dim ParameterValues6 As New ParameterValues()
        Dim ParameterValues7 As New ParameterValues()
        Dim ParameterValues8 As New ParameterValues()
        Dim ParameterValues9 As New ParameterValues()
        Dim ParameterValues10 As New ParameterValues()
        Dim ParameterValues11 As New ParameterValues()
        Dim ParameterValues12 As New ParameterValues()
        Dim ParameterValues13 As New ParameterValues()
        Dim ParameterValues14 As New ParameterValues()
        Dim ParameterValues15 As New ParameterValues()
        Dim ParameterValues16 As New ParameterValues()
        Dim ParameterValues17 As New ParameterValues()
        Dim ParameterValues18 As New ParameterValues()
        Dim ParameterValues19 As New ParameterValues()
        Dim ParameterValues20 As New ParameterValues()
        Dim ParameterValues21 As New ParameterValues()
        Dim ParameterValues22 As New ParameterValues()
        Dim ParameterValues23 As New ParameterValues()
        Dim ParameterValues24 As New ParameterValues()
        Dim ParameterValues25 As New ParameterValues()
        Dim ParameterValues26 As New ParameterValues()
        Dim ParameterValues27 As New ParameterValues()
        Dim ParameterValues28 As New ParameterValues()
        Dim ParameterValues29 As New ParameterValues()
        Dim ParameterValues30 As New ParameterValues()
        Dim ParameterValues31 As New ParameterValues()
        Dim ParameterValues32 As New ParameterValues()
        Dim ParameterValues33 As New ParameterValues()
        Dim ParameterValues34 As New ParameterValues()

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

        ParamDiscreteValue1.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue2.Value = Session("SS_LOC")
        ParamDiscreteValue3.Value = Session("SS_USERNAME")
        ParamDiscreteValue4.Value = Request.QueryString("DDLAccMth")
        ParamDiscreteValue5.Value = Request.QueryString("DDLAccYr")
        ParamDiscreteValue6.Value = Request.QueryString("Decimal")
        ParamDiscreteValue7.Value = Request.QueryString("RptID")
        ParamDiscreteValue8.Value = Request.QueryString("RptName")
        ParamDiscreteValue9.Value = Request.QueryString("BlkType")
        ParamDiscreteValue10.Value = Request.QueryString("BlkGrp")
        ParamDiscreteValue11.Value = Request.QueryString("BlkCode")
        ParamDiscreteValue12.Value = Request.QueryString("SubBlkCode")

        If strBlkType = "BlkGrp" Then
            ParamDiscreteValue13.Value = Session("SS_BLKGRPHEADER")
        ElseIf strBlkType = "BlkCode" Then
            ParamDiscreteValue13.Value = Session("SS_BLKHEADER")
        ElseIf strBlkType = "SubBlkCode" Then
            ParamDiscreteValue13.Value = Session("SS_SUBBLKHEADER")
        End If

        ParamDiscreteValue14.Value = Request.QueryString("IssueType")
        ParamDiscreteValue15.Value = Request.QueryString("ItemCode")
        ParamDiscreteValue16.Value = Request.QueryString("lblBlkGrp")
        ParamDiscreteValue17.Value = Request.QueryString("lblBlkCode")
        ParamDiscreteValue18.Value = Request.QueryString("lblSubBlkCode")
        ParamDiscreteValue19.Value = Request.QueryString("lblLocation")
        ParamDiscreteValue20.Value = Request.QueryString("ProdType")
        ParamDiscreteValue21.Value = Request.QueryString("ProdBrand")
        ParamDiscreteValue22.Value = Request.QueryString("ProdModel")
        ParamDiscreteValue23.Value = Request.QueryString("ProdCat")
        ParamDiscreteValue24.Value = Request.QueryString("ProdMat")
        ParamDiscreteValue25.Value = Request.QueryString("StkAna")
        ParamDiscreteValue26.Value = Request.QueryString("lblProdTypeCode")
        ParamDiscreteValue27.Value = Request.QueryString("lblProdBrandCode")
        ParamDiscreteValue28.Value = Request.QueryString("lblProdModelCode")
        ParamDiscreteValue29.Value = Request.QueryString("lblProdCatCode")
        ParamDiscreteValue30.Value = Request.QueryString("lblProdMatCode")
        ParamDiscreteValue31.Value = Request.QueryString("lblStkAnaCode")
        ParamDiscreteValue32.Value = Request.QueryString("AccCode")
        ParamDiscreteValue33.Value = Request.QueryString("lblAccCode")
        ParamDiscreteValue34.Value = Request.QueryString("WS")

        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        ParamFieldDef1 = ParamFieldDefs.Item("ParamCompanyName")
        ParamFieldDef2 = ParamFieldDefs.Item("ParamLocation")
        ParamFieldDef3 = ParamFieldDefs.Item("ParamUserName")
        ParamFieldDef4 = ParamFieldDefs.Item("ParamAccMonth")
        ParamFieldDef5 = ParamFieldDefs.Item("ParamAccYear")
        ParamFieldDef6 = ParamFieldDefs.Item("ParamDecimal")
        ParamFieldDef7 = ParamFieldDefs.Item("ParamRptID")
        ParamFieldDef8 = ParamFieldDefs.Item("ParamRptName")
        ParamFieldDef9 = ParamFieldDefs.Item("ParamBlkType")
        ParamFieldDef10 = ParamFieldDefs.Item("ParamBlkGrp")
        ParamFieldDef11 = ParamFieldDefs.Item("ParamBlkCode")
        ParamFieldDef12 = ParamFieldDefs.Item("ParamSubBlkCode")
        ParamFieldDef13 = ParamFieldDefs.Item("ParamBlkOrSubBlk")
        ParamFieldDef14 = ParamFieldDefs.Item("ParamIssueType")
        ParamFieldDef15 = ParamFieldDefs.Item("ParamItemCode")
        ParamFieldDef16 = ParamFieldDefs.Item("lblBlkGrp")
        ParamFieldDef17 = ParamFieldDefs.Item("lblBlkCode")
        ParamFieldDef18 = ParamFieldDefs.Item("lblSubBlkCode")
        ParamFieldDef19 = ParamFieldDefs.Item("lblLocation")
        ParamFieldDef20 = ParamFieldDefs.Item("ParamProdType")
        ParamFieldDef21 = ParamFieldDefs.Item("ParamProdBrand")
        ParamFieldDef22 = ParamFieldDefs.Item("ParamProdModel")
        ParamFieldDef23 = ParamFieldDefs.Item("ParamProdCat")
        ParamFieldDef24 = ParamFieldDefs.Item("ParamProdMat")
        ParamFieldDef25 = ParamFieldDefs.Item("ParamStkAna")
        ParamFieldDef26 = ParamFieldDefs.Item("lblProdTypeCode")
        ParamFieldDef27 = ParamFieldDefs.Item("lblProdBrandCode")
        ParamFieldDef28 = ParamFieldDefs.Item("lblProdModelCode")
        ParamFieldDef29 = ParamFieldDefs.Item("lblProdCatCode")
        ParamFieldDef30 = ParamFieldDefs.Item("lblProdMatCode")
        ParamFieldDef31 = ParamFieldDefs.Item("lblStkAnaCode")
        ParamFieldDef32 = ParamFieldDefs.Item("ParamAccCode")
        ParamFieldDef33 = ParamFieldDefs.Item("lblAccCode")
        ParamFieldDef34 = ParamFieldDefs.Item("ParamIncWS")


        ParameterValues1 = ParamFieldDef1.CurrentValues
        ParameterValues2 = ParamFieldDef2.CurrentValues
        ParameterValues3 = ParamFieldDef3.CurrentValues
        ParameterValues4 = ParamFieldDef4.CurrentValues
        ParameterValues5 = ParamFieldDef5.CurrentValues
        ParameterValues6 = ParamFieldDef6.CurrentValues
        ParameterValues7 = ParamFieldDef7.CurrentValues
        ParameterValues8 = ParamFieldDef8.CurrentValues
        ParameterValues9 = ParamFieldDef9.CurrentValues
        ParameterValues10 = ParamFieldDef10.CurrentValues
        ParameterValues11 = ParamFieldDef11.CurrentValues
        ParameterValues12 = ParamFieldDef12.CurrentValues
        ParameterValues13 = ParamFieldDef13.CurrentValues
        ParameterValues14 = ParamFieldDef14.CurrentValues
        ParameterValues15 = ParamFieldDef15.CurrentValues
        ParameterValues16 = ParamFieldDef16.CurrentValues
        ParameterValues17 = ParamFieldDef17.CurrentValues
        ParameterValues18 = ParamFieldDef18.CurrentValues
        ParameterValues19 = ParamFieldDef19.CurrentValues
        ParameterValues20 = ParamFieldDef20.CurrentValues
        ParameterValues21 = ParamFieldDef21.CurrentValues
        ParameterValues22 = ParamFieldDef22.CurrentValues
        ParameterValues23 = ParamFieldDef23.CurrentValues
        ParameterValues24 = ParamFieldDef24.CurrentValues
        ParameterValues25 = ParamFieldDef25.CurrentValues
        ParameterValues26 = ParamFieldDef26.CurrentValues
        ParameterValues27 = ParamFieldDef27.CurrentValues
        ParameterValues28 = ParamFieldDef28.CurrentValues
        ParameterValues29 = ParamFieldDef28.CurrentValues
        ParameterValues30 = ParamFieldDef28.CurrentValues
        ParameterValues31 = ParamFieldDef28.CurrentValues
        ParameterValues32 = ParamFieldDef28.CurrentValues
        ParameterValues33 = ParamFieldDef28.CurrentValues
        ParameterValues34 = ParamFieldDef28.CurrentValues

        ParameterValues1.Add(ParamDiscreteValue1)
        ParameterValues2.Add(ParamDiscreteValue2)
        ParameterValues3.Add(ParamDiscreteValue3)
        ParameterValues4.Add(ParamDiscreteValue4)
        ParameterValues5.Add(ParamDiscreteValue5)
        ParameterValues6.Add(ParamDiscreteValue6)
        ParameterValues7.Add(ParamDiscreteValue7)
        ParameterValues8.Add(ParamDiscreteValue8)
        ParameterValues9.Add(ParamDiscreteValue9)
        ParameterValues10.Add(ParamDiscreteValue10)
        ParameterValues11.Add(ParamDiscreteValue11)
        ParameterValues12.Add(ParamDiscreteValue12)
        ParameterValues13.Add(ParamDiscreteValue13)
        ParameterValues14.Add(ParamDiscreteValue14)
        ParameterValues15.Add(ParamDiscreteValue15)
        ParameterValues16.Add(ParamDiscreteValue16)
        ParameterValues17.Add(ParamDiscreteValue17)
        ParameterValues18.Add(ParamDiscreteValue18)
        ParameterValues19.Add(ParamDiscreteValue19)
        ParameterValues20.Add(ParamDiscreteValue20)
        ParameterValues21.Add(ParamDiscreteValue21)
        ParameterValues22.Add(ParamDiscreteValue22)
        ParameterValues23.Add(ParamDiscreteValue23)
        ParameterValues24.Add(ParamDiscreteValue24)
        ParameterValues25.Add(ParamDiscreteValue25)
        ParameterValues26.Add(ParamDiscreteValue26)
        ParameterValues27.Add(ParamDiscreteValue27)
        ParameterValues28.Add(ParamDiscreteValue28)
        ParameterValues29.Add(ParamDiscreteValue29)
        ParameterValues30.Add(ParamDiscreteValue30)
        ParameterValues31.Add(ParamDiscreteValue31)
        ParameterValues32.Add(ParamDiscreteValue32)
        ParameterValues33.Add(ParamDiscreteValue33)
        ParameterValues34.Add(ParamDiscreteValue34)

        ParamFieldDef1.ApplyCurrentValues(ParameterValues1)
        ParamFieldDef2.ApplyCurrentValues(ParameterValues2)
        ParamFieldDef3.ApplyCurrentValues(ParameterValues3)
        ParamFieldDef4.ApplyCurrentValues(ParameterValues4)
        ParamFieldDef5.ApplyCurrentValues(ParameterValues5)
        ParamFieldDef6.ApplyCurrentValues(ParameterValues6)
        ParamFieldDef7.ApplyCurrentValues(ParameterValues7)
        ParamFieldDef8.ApplyCurrentValues(ParameterValues8)
        ParamFieldDef9.ApplyCurrentValues(ParameterValues9)
        ParamFieldDef10.ApplyCurrentValues(ParameterValues10)
        ParamFieldDef11.ApplyCurrentValues(ParameterValues11)
        ParamFieldDef12.ApplyCurrentValues(ParameterValues12)
        ParamFieldDef13.ApplyCurrentValues(ParameterValues13)
        ParamFieldDef14.ApplyCurrentValues(ParameterValues14)
        ParamFieldDef15.ApplyCurrentValues(ParameterValues15)
        ParamFieldDef16.ApplyCurrentValues(ParameterValues16)
        ParamFieldDef17.ApplyCurrentValues(ParameterValues17)
        ParamFieldDef18.ApplyCurrentValues(ParameterValues18)
        ParamFieldDef19.ApplyCurrentValues(ParameterValues19)
        ParamFieldDef20.ApplyCurrentValues(ParameterValues20)
        ParamFieldDef21.ApplyCurrentValues(ParameterValues21)
        ParamFieldDef22.ApplyCurrentValues(ParameterValues22)
        ParamFieldDef23.ApplyCurrentValues(ParameterValues23)
        ParamFieldDef24.ApplyCurrentValues(ParameterValues24)
        ParamFieldDef25.ApplyCurrentValues(ParameterValues25)
        ParamFieldDef26.ApplyCurrentValues(ParameterValues26)
        ParamFieldDef27.ApplyCurrentValues(ParameterValues27)
        ParamFieldDef28.ApplyCurrentValues(ParameterValues28)
        ParamFieldDef29.ApplyCurrentValues(ParameterValues29)
        ParamFieldDef30.ApplyCurrentValues(ParameterValues30)
        ParamFieldDef31.ApplyCurrentValues(ParameterValues31)
        ParamFieldDef32.ApplyCurrentValues(ParameterValues32)
        ParamFieldDef33.ApplyCurrentValues(ParameterValues33)
        ParamFieldDef34.ApplyCurrentValues(ParameterValues34)
       
    End Sub


End Class
