Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports System.Xml
Imports System.Web.Services
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web

Public Class WS_StdRpt_StockTakeList_Preview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objINRpt As New agri.IN.clsReport()
    Dim objINSetup As New agri.IN.clsSetup()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objPWSysConfig As New agri.PWSystem.clsConfig()

	Dim objWSReport As New agri.WS.clsReport() 

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strLocationName As String
    Dim strUserId As String
    Dim strPrintedBy As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfigsetting As Integer

    Dim intSelDecimal As String
    Dim strlblProdTypeCode As String
    Dim strlblProdBrandCode As String
    Dim strlblProdModelCode As String
    Dim strlblProdCatCode As String
    Dim strlblProdMatCode As String
    Dim strlblStkAnaCode As String
    Dim strProdType As String
    Dim strProdBrand As String
    Dim strProdModel As String
    Dim strProdCat As String
    Dim strProdMat As String
    Dim strStkAna As String
    Dim strItemCode As String
    Dim strItemType As String
    Dim strItemStatus As String
    Dim strSuppress As String
    Dim strUserLoc As String
    Dim strItemTypeDesc As String
    Dim strItemStatusDesc As String

    Dim rdCrystalViewer As ReportDocument

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        crvView.Visible = False

        If Right(Request.QueryString("Location"), 1) = "," Then
            strUserLoc = Left(Request.QueryString("Location"), Len(Request.QueryString("Location")) - 1)
        Else
            strUserLoc = Trim(Request.QueryString("Location"))
        End If

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strLocationName = Session("SS_LOCATIONNAME")
        strUserId = Session("SS_USERID")
        strPrintedBy = Session("SS_USERNAME")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        intSelDecimal = CInt(Request.QueryString("Decimal"))
        strlblProdTypeCode = Request.QueryString("lblProdTypeCode")
        strlblProdBrandCode = Request.QueryString("lblProdBrandCode")
        strlblProdModelCode = Request.QueryString("lblProdModelCode")
        strlblProdCatCode = Request.QueryString("lblProdCatCode")
        strlblProdMatCode = Request.QueryString("lblProdMatCode")
        strlblStkAnaCode = Request.QueryString("lblStkAnaCode")
        strProdType = Request.QueryString("ProdType")
        strProdBrand = Request.QueryString("ProdBrand")
        strProdModel = Request.QueryString("ProdModel")
        strProdCat = Request.QueryString("ProdCat")
        strProdMat = Request.QueryString("ProdMat")
        strStkAna = Request.QueryString("StkAna")
        strItemCode = Request.QueryString("ItemCode")
        strItemStatus = Request.QueryString("ItemStatus")
        strSuppress = Request.QueryString("Suppress")
        strProdType = Request.QueryString("ProdType")
        strProdBrand = Request.QueryString("ProdBrand")
        strProdModel = Request.QueryString("ProdModel")
        strProdCat = Request.QueryString("ProdCat")
        strProdMat = Request.QueryString("ProdMat")
        strStkAna = Request.QueryString("StkAna")
        strItemCode = Request.QueryString("ItemCode")
        strItemType = Request.QueryString("ItemType")
        strItemStatus = Request.QueryString("ItemStatus")

        Session("SS_LBLLOCATION") = Request.QueryString("lblLocation")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            BindReport()
        End If
    End Sub

    Sub BindReport()
        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim strOpCd As String
        Dim objRptDs As New Dataset()
        Dim objMapPath As String
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strRptPrefix As String
        Dim strReportID As String

        strReportID = "RPTWS1000012"

        If Right(strUserLoc, 1) = "," Then
            Session("SS_LOC") = Left(strUserLoc, Len(strUserLoc) - 1)
        Else
            Session("SS_LOC") = strUserLoc.Replace("'", "")
        End If
        
        If strItemType = "" Then
            strItemTypeDesc = "All"
        Else
            strItemTypeDesc = objINSetup.mtdGetInventoryItemType(CInt(strItemType))
        End If

        If strItemStatus = "" Then
            strItemStatusDesc = objINSetup.mtdGetStockItemStatus(objINSetup.EnumStockItemStatus.All)
        Else
            strItemStatusDesc = objINSetup.mtdGetStockItemStatus(strItemStatus)
        End If
        
        Try
            strRptPrefix = "WS_StdRpt_StockTakeList"

			
            strOpCd = "WS_CLSSETUP_INVITEM_LIST_GET_FOR_REPORT" & "|" & "WS_STOCKTAKE" & Chr(9) & _
                      "GET_REPORT_INFO_BY_REPORTID" & "|" & "SH_REPORT"

            strParam = strUserLoc & "|" & _
                       strItemType & "|" & _
                       strItemStatus & "|" & _
                       strProdType & "|" & _
                       strProdBrand & "|" & _
                       strProdModel & "|" & _
                       strProdCat & "|" & _
                       strProdMat & "|" & _
                       strStkAna & "|" & _
                       strItemCode & "|" & _
                       strReportID & "|ORDER BY itm.LocCode, itm.ProdTypeCode, itm.ItemCode|" & _
                       strSuppress

			
			intErrNo = objWSReport.mtdGetReport_StockTakeList(strOpCd, _
                                                           strCompany, _
                                                           strLocation, _
                                                           strUserId, _
                                                           strAccMonth, _
                                                           strAccYear, _
                                                           strParam, _
                                                           objRptDs, _
                                                           objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=IN_STDRPT_STOCKTAKELIST_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try


        rdCrystalViewer = New ReportDocument()
        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strRptPrefix & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs)


        crvView.Visible = False                          
        crvView.ReportSource = rdCrystalViewer           
        crvView.DataBind()                               

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strRptPrefix & ".pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strRptPrefix & ".pdf"">")
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

        ParamDiscreteValue1.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue2.Value = Session("SS_LOC")
        ParamDiscreteValue3.Value = Session("SS_USERNAME")
        ParamDiscreteValue4.Value = strAccMonth
        ParamDiscreteValue5.Value = strAccYear
        ParamDiscreteValue6.Value = intSelDecimal
        ParamDiscreteValue7.Value = Session("SS_LBLLOCATION")
        ParamDiscreteValue8.Value = strlblProdTypeCode
        ParamDiscreteValue9.Value = strlblProdBrandCode
        ParamDiscreteValue10.Value = strlblProdModelCode
        ParamDiscreteValue11.Value = strlblProdCatCode
        ParamDiscreteValue12.Value = strlblProdMatCode
        ParamDiscreteValue13.Value = strlblStkAnaCode
        ParamDiscreteValue14.Value = strProdType
        ParamDiscreteValue15.Value = strProdBrand
        ParamDiscreteValue16.Value = strProdModel
        ParamDiscreteValue17.Value = strProdCat
        ParamDiscreteValue18.Value = strProdMat
        ParamDiscreteValue19.Value = strStkAna
        ParamDiscreteValue20.Value = strItemCode
        ParamDiscreteValue21.Value = strItemStatusDesc
        ParamDiscreteValue22.Value = strSuppress
        ParamDiscreteValue23.Value = strItemTypeDesc
        
        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        ParamFieldDef1 = ParamFieldDefs.Item("ParamCompanyName")
        ParamFieldDef2 = ParamFieldDefs.Item("ParamLocation")
        ParamFieldDef3 = ParamFieldDefs.Item("ParamUserName")
        ParamFieldDef4 = ParamFieldDefs.Item("ParamAccMonth")
        ParamFieldDef5 = ParamFieldDefs.Item("ParamAccYear")
        ParamFieldDef6 = ParamFieldDefs.Item("ParamSelDecimal")
        ParamFieldDef7 = ParamFieldDefs.Item("lblLocation")
        ParamFieldDef8 = ParamFieldDefs.Item("lblProdTypeCode")
        ParamFieldDef9 = ParamFieldDefs.Item("lblProdBrandCode")
        ParamFieldDef10 = ParamFieldDefs.Item("lblProdModelCode")
        ParamFieldDef11 = ParamFieldDefs.Item("lblProdCatCode")
        ParamFieldDef12 = ParamFieldDefs.Item("lblProdMatCode")
        ParamFieldDef13 = ParamFieldDefs.Item("lblStkAnaCode")
        ParamFieldDef14 = ParamFieldDefs.Item("ParamProdType")
        ParamFieldDef15 = ParamFieldDefs.Item("ParamProdBrand")
        ParamFieldDef16 = ParamFieldDefs.Item("ParamProdModel")
        ParamFieldDef17 = ParamFieldDefs.Item("ParamProdCat")
        ParamFieldDef18 = ParamFieldDefs.Item("ParamProdMat")
        ParamFieldDef19 = ParamFieldDefs.Item("ParamStkAna")
        ParamFieldDef20 = ParamFieldDefs.Item("ParamItemCode")
        ParamFieldDef21 = ParamFieldDefs.Item("ParamItemStatus")
        ParamFieldDef22 = ParamFieldDefs.Item("ParamSuppress")
        ParamFieldDef23 = ParamFieldDefs.Item("ParamItemType")

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
    End Sub

End Class
