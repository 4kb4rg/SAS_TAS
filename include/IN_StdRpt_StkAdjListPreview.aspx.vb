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

Public Class IN_StdRpt_StkAdjList_Preview : Inherits Page
    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objINRpt As New agri.IN.clsReport()
    Dim objINSetup As New agri.IN.clsSetup()
    Dim objINTrx As New agri.IN.clsTrx()
    Dim objSysCfg As New agri.PWSystem.clsConfig()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strUserLoc As String
    Dim intConfigsetting As Integer

    Dim intDecimal As Integer
    Dim strBlkType As String
    Dim tempLoc As String
    Dim strDDLAccMth As String
    Dim strDDLAccYr As String
    Dim blnCostAtBlock As Boolean
    Dim strBlockCode As String
    Dim lblBlockType As String
    Dim lblBlockCode As String
    Dim lblBlockSubBlockCode As String
    Dim strStatus As String
    Dim strAdjType As String
    Dim strTransType As String

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
        intConfigsetting = Session("SS_CONFIGSETTING")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            BindReport()
        End If
    End Sub

    Sub BindReport()
        Dim dsRpt As New DataSet()
        Dim objMapPath As String
        Dim intCnt As Integer
        Dim strWhere As String = ""
        Dim strOrderBy As String = " ORDER BY HD.LocCode, HD.StockAdjID, LN.StockAdjLNID"
        Dim strOpCd As String = "IN_STDRPT_STOCKADJUST_LIST_GET"    

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        
        strWhere = " WHERE HD.LocCode IN('" & strUserLoc & "') AND HD.AccMonth = '" & strDDLAccMth & "' AND HD.AccYear = '" & strDDLAccYr & "'" & vbCrLf
        
        If Not (Request.QueryString("DateFrom") = "" And Request.QueryString("DateTo") = "") Then
            strWhere = strWhere & " AND (DateDiff(Day, '" & Request.QueryString("DateFrom") & "', HD.CreateDate) >= 0) And (DateDiff(Day, '" & Request.QueryString("DateTo") & "', HD.CreateDate) <= 0)" & vbCrLf
        End If

        If Not (Request.QueryString("StkAdjIDFrom") = "" And Request.QueryString("StkAdjIDTo") = "") Then
            strWhere = strWhere & " AND (HD.StockAdjID >= '" & Request.QueryString("StkAdjIDFrom") & "' AND HD.StockAdjID <= '" & Request.QueryString("StkAdjIDTo") & "')" & vbCrLf
        End If
        
        If Not Request.QueryString("AdjType") = "" Then
            If Not Request.QueryString("AdjType") = objINTrx.EnumAdjustmentType.All Then
                strWhere = strWhere & " AND HD.AdjType = '" & Request.QueryString("AdjType") & "'" & vbCrLf
            End If
        End If
        strAdjType = objINTrx.mtdGetAdjustmentType(Request.QueryString("AdjType"))
        
        If Not Request.QueryString("TransType") = "" Then
            If Not Request.QueryString("TransType") = objINTrx.EnumTransactionType.All Then
                strWhere = strWhere & " AND HD.TransType = '" & Request.QueryString("TransType") & "'" & vbCrLf
            End If
        End If
        strTransType = objINTrx.mtdGetTransactionType(Request.QueryString("TransType"))

        If Not Request.QueryString("Status") = "" Then
            If Not Request.QueryString("Status") = objINTrx.EnumStockAdjustStatus.All Then
                strWhere = strWhere & " AND HD.Status = '" & Request.QueryString("Status") & "'" & vbCrLf
            End If
        End If
        
        strStatus = objINTrx.mtdGetStockAdjustStatus(Request.QueryString("Status"))
        
        If Not Request.QueryString("RefNo") = "" Then
            strWhere = strWhere & " AND LN.AdjRefNo LIKE '" & Request.QueryString("RefNo") & "'" & vbCrLf
        End If
        
        If Not Request.QueryString("AccCode") = "" Then
            strWhere = strWhere & " AND LN.AccCode LIKE '" & Request.QueryString("AccCode") & "'" & vbCrLf
        End If

        If Not Request.QueryString("VehCode") = "" Then
            strWhere = strWhere & " AND LN.VehCode LIKE '" & Request.QueryString("VehCode") & "'" & vbCrLf
        End If

        If Not Request.QueryString("VehTypeCode") = "" Then
            strWhere = strWhere & " AND LN.VehCode IN (SELECT DISTINCT VehCode FROM GL_VEHICLE WHERE VehTypeCode LIKE '" & Request.QueryString("VehTypeCode") & "')" & vbCrLf
        End If

        If Not Request.QueryString("VehExpCode") = "" Then
            strWhere = strWhere & " AND LN.VehExpCode LIKE '" & Request.QueryString("VehExpCode") & "'" & vbCrLf
        End If
        
        If Not Request.QueryString("ItemCode") = "" Then
            strWhere = strWhere & " AND ITM.ItemCode LIKE '" & Request.QueryString("ItemCode") & "'" & vbCrLf
        End If

        If Not Request.QueryString("ProdType") = "" Then
            strWhere = strWhere & " AND ITM.ProdTypeCode LIKE '" & Request.QueryString("ProdType") & "'" & vbCrLf
        End If

        If Not Request.QueryString("ProdBrand") = "" Then
            strWhere = strWhere & " AND ITM.ProdBrandCode LIKE '" & Request.QueryString("ProdBrand") & "'" & vbCrLf
        End If

        If Not Request.QueryString("ProdModel") = "" Then
            strWhere = strWhere & " AND ITM.ProdModelCode LIKE '" & Request.QueryString("ProdModel") & "'" & vbCrLf
        End If

        If Not Request.QueryString("ProdCat") = "" Then
            strWhere = strWhere & " AND ITM.ProdCatCode LIKE '" & Request.QueryString("ProdCat") & "'" & vbCrLf
        End If

        If Not Request.QueryString("ProdMat") = "" Then
            strWhere = strWhere & " AND ITM.ProdMatCode LIKE '" & Request.QueryString("ProdMat") & "'" & vbCrLf
        End If

        If Not Request.QueryString("StkAna") = "" Then
            strWhere = strWhere & " AND ITM.StockAnalysisCode LIKE '" & Request.QueryString("StkAna") & "'" & vbCrLf
        End If
        
        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
            blnCostAtBlock = True
            lblBlockSubBlockCode = Request.QueryString("lblBlkCode")
        Else
            blnCostAtBlock = False
            lblBlockSubBlockCode = Request.QueryString("lblSubBlkCode")
        End If
        
        If strBlkType = "BlkGrp" Then
            lblBlockCode = Request.QueryString("lblBlkGrp")
            strBlockCode = Request.QueryString("BlkGrp")
            If Not Request.QueryString("BlkGrp") = "" Then
                If blnCostAtBlock = True Then
                    strWhere = strWhere & " AND LN.BlkCode IN (SELECT DISTINCT BlkCode FROM GL_BLOCK WHERE BlkGrpCode LIKE '" & Request.QueryString("BlkGrp") & "')" & vbCrLf
                Else
                    strWhere = strWhere & " AND LN.BlkCode IN (SELECT DISTINCT SubBlkCode FROM GL_SUBBLK WHERE BlkCode IN (SELECT DISTINCT BlkCode FROM GL_BLOCK WHERE BlkGrpCode LIKE '" & Request.QueryString("BlkGrp") & "')" & vbCrLf
                End If
            End If
        ElseIf strBlkType = "BlkCode" Then
            lblBlockCode = Request.QueryString("lblBlkCode")
            strBlockCode = Request.QueryString("BlkCode")
            If Not Request.QueryString("BlkCode") = "" Then
                If blnCostAtBlock = True Then
                    strWhere = strWhere & " AND LN.BlkCode LIKE '" & Request.QueryString("BlkCode") & "'" & vbCrLf
                Else
                    strWhere = strWhere & " AND LN.BlkCode IN (SELECT DISTINCT SubBlkCode FROM GL_SUBBLK WHERE BlkCode LIKE '" & Request.QueryString("BlkCode") & "')" & vbCrLf
                End If
            End If
        ElseIf strBlkType = "SubBlkCode" Then
            lblBlockCode = Request.QueryString("lblSubBlkCode")
            strBlockCode = Request.QueryString("SubBlkCode")
            If Not Request.QueryString("SubBlkCode") = "" Then
                If blnCostAtBlock = True Then
                    strWhere = strWhere & " AND LN.BlkCode IN (SELECT DISTINCT BlkCode FROM GL_SUBBLK WHERE SubBlkCode LIKE '" & Request.QueryString("SubBlkCode") & "')" & vbCrLf
                Else
                    strWhere = strWhere & " AND LN.BlkCode LIKE '" & Request.QueryString("SubBlkCode") & "'" & vbCrLf
                End If
            End If
        End If
        
        Try
            intErrNo = objINRpt.mtdGetReport_StockAdjustmentList(strOpCd, strWhere, strOrderBy, dsRpt, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_STOCKADJ_LIST_REPORT&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        


        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\IN_StdRpt_StkAdjList.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(dsRpt.Tables(0))

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3

        crvView.Visible = False                         
        crvView.ReportSource = rdCrystalViewer          
        crvView.DataBind()                              

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\IN_StdRpt_StkAdjList.pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/IN_StdRpt_StkAdjList.pdf"">")
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
        Dim paramField48 As New ParameterField()
        Dim paramField49 As New ParameterField()
        Dim paramField50 As New ParameterField()
        Dim paramField51 As New ParameterField()
        Dim paramField52 As New ParameterField()
        Dim paramField53 As New ParameterField()
        Dim paramField54 As New ParameterField()
        Dim paramField55 As New ParameterField()
        Dim paramField56 As New ParameterField()
        Dim paramField57 As New ParameterField()
        Dim paramField58 As New ParameterField()
        Dim paramField59 As New ParameterField()
        Dim paramField60 As New ParameterField()
        Dim paramField61 As New ParameterField()
        Dim paramField62 As New ParameterField()
        Dim paramField63 As New ParameterField()
        Dim paramField64 As New ParameterField()
        Dim paramField65 As New ParameterField()
        Dim paramField66 As New ParameterField()
        Dim paramField67 As New ParameterField()
        

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
        Dim ParamDiscreteValue48 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue49 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue50 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue51 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue52 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue53 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue54 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue55 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue56 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue57 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue58 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue59 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue60 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue61 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue62 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue63 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue64 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue65 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue66 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue67 As New ParameterDiscreteValue()


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
        Dim crParameterValues48 As ParameterValues
        Dim crParameterValues49 As ParameterValues
        Dim crParameterValues50 As ParameterValues
        Dim crParameterValues51 As ParameterValues
        Dim crParameterValues52 As ParameterValues
        Dim crParameterValues53 As ParameterValues
        Dim crParameterValues54 As ParameterValues
        Dim crParameterValues55 As ParameterValues
        Dim crParameterValues56 As ParameterValues
        Dim crParameterValues57 As ParameterValues
        Dim crParameterValues58 As ParameterValues
        Dim crParameterValues59 As ParameterValues
        Dim crParameterValues60 As ParameterValues
        Dim crParameterValues61 As ParameterValues
        Dim crParameterValues62 As ParameterValues
        Dim crParameterValues63 As ParameterValues
        Dim crParameterValues64 As ParameterValues
        Dim crParameterValues65 As ParameterValues
        Dim crParameterValues66 As ParameterValues
        Dim crParameterValues67 As ParameterValues


        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("ParamLocation")
        paramField2 = paramFields.Item("ParamCompanyName")
        paramField3 = paramFields.Item("ParamUserName")
        paramField4 = paramFields.Item("ParamRptID")
        paramField5 = paramFields.Item("ParamRptName")
        paramField6 = paramFields.Item("ParamDecimal")
        paramField7 = paramFields.Item("ParamAccMonth")
        paramField8 = paramFields.Item("ParamAccYear")
        paramField9 = paramFields.Item("ParamDateFrom")
        paramField10 = paramFields.Item("ParamDateTo")
        paramField11 = paramFields.Item("ParamStockAdjIDFrom")
        paramField12 = paramFields.Item("ParamStockAdjIDTo")
        paramField13 = paramFields.Item("ParamAdjustmentType")
        paramField14 = paramFields.Item("ParamTransactionType")
        paramField15 = paramFields.Item("ParamAdjRefNo")
        paramField16 = paramFields.Item("ParamProdTypeCode")
        paramField17 = paramFields.Item("ParamProdBrandCode")
        paramField18 = paramFields.Item("ParamProdModelCode")
        paramField19 = paramFields.Item("ParamProdCatCode")
        paramField20 = paramFields.Item("ParamProdMatCode")
        paramField21 = paramFields.Item("ParamAnalysisCode")
        paramField22 = paramFields.Item("ParamItemCode")
        paramField23 = paramFields.Item("ParamAccCode")
        paramField24 = paramFields.Item("ParamVehicleType")
        paramField25 = paramFields.Item("ParamVehicleCode")
        paramField26 = paramFields.Item("ParamVehExpCode")
        paramField27 = paramFields.Item("ParamBlockType")
        paramField28 = paramFields.Item("ParamBlockCode")
        paramField29 = paramFields.Item("ParamStatus")
        paramField30 = paramFields.Item("lblLocation")
        paramField31 = paramFields.Item("lblProdTypeCode")
        paramField32 = paramFields.Item("lblProdBrandCode")
        paramField33 = paramFields.Item("lblProdModelCode")
        paramField34 = paramFields.Item("lblProdCatCode")
        paramField35 = paramFields.Item("lblProdMatCode")
        paramField36 = paramFields.Item("lblAnalysisCode")
        paramField37 = paramFields.Item("lblAccCode")
        paramField38 = paramFields.Item("lblBlockCode")
        paramField39 = paramFields.Item("lblVehicleType")
        paramField40 = paramFields.Item("lblVehicleCode")
        paramField41 = paramFields.Item("lblVehExpCode")
        paramField42 = paramFields.Item("lblBlockSubBlockCode")
        paramField43 = paramFields.Item("lblBlockType")
        paramField44 = paramFields.Item("TransType_New_Enum")
        paramField45 = paramFields.Item("TransType_New_Val")
        paramField46 = paramFields.Item("TransType_Difference_Enum")
        paramField47 = paramFields.Item("TransType_Difference_Val")
        paramField48 = paramFields.Item("AdjType_Quantity_Enum")
        paramField49 = paramFields.Item("AdjType_Quantity_Val")
        paramField50 = paramFields.Item("AdjType_AvrgCost_Enum")
        paramField51 = paramFields.Item("AdjType_AvrgCost_Val")
        paramField52 = paramFields.Item("AdjType_TotalCost_Enum")
        paramField53 = paramFields.Item("AdjType_TotalCost_Val")
        paramField54 = paramFields.Item("AdjType_QtyAtAvrgCost_Enum")
        paramField55 = paramFields.Item("AdjType_QtyAtAvrgCost_Val")
        paramField56 = paramFields.Item("AdjType_QtyAtTotalCost_Enum")
        paramField57 = paramFields.Item("AdjType_QtyAtTotalCost_Val")
        paramField58 = paramFields.Item("Status_Active_Enum")
        paramField59 = paramFields.Item("Status_Active_Val")
        paramField60 = paramFields.Item("Status_Confirmed_Enum")
        paramField61 = paramFields.Item("Status_Confirmed_Val")
        paramField62 = paramFields.Item("Status_Deleted_Enum")
        paramField63 = paramFields.Item("Status_Deleted_Val")
        paramField64 = paramFields.Item("Status_Cancelled_Enum")
        paramField65 = paramFields.Item("Status_Cancelled_Val")
        paramField66 = paramFields.Item("Status_Closed_Enum")
        paramField67 = paramFields.Item("Status_Closed_Val")
        
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
        crParameterValues48 = paramField48.CurrentValues
        crParameterValues49 = paramField49.CurrentValues
        crParameterValues50 = paramField50.CurrentValues
        crParameterValues51 = paramField51.CurrentValues
        crParameterValues52 = paramField52.CurrentValues
        crParameterValues53 = paramField53.CurrentValues
        crParameterValues54 = paramField54.CurrentValues
        crParameterValues55 = paramField55.CurrentValues
        crParameterValues56 = paramField56.CurrentValues
        crParameterValues57 = paramField57.CurrentValues
        crParameterValues58 = paramField58.CurrentValues
        crParameterValues59 = paramField59.CurrentValues
        crParameterValues60 = paramField60.CurrentValues
        crParameterValues61 = paramField61.CurrentValues
        crParameterValues62 = paramField62.CurrentValues
        crParameterValues63 = paramField63.CurrentValues
        crParameterValues64 = paramField64.CurrentValues
        crParameterValues65 = paramField65.CurrentValues
        crParameterValues66 = paramField66.CurrentValues
        crParameterValues67 = paramField67.CurrentValues


        ParamDiscreteValue1.Value = Session("SS_LOC")
        ParamDiscreteValue2.Value = Session("SS_COMPANYNAME") 
        ParamDiscreteValue3.Value = Session("SS_USERNAME")
        ParamDiscreteValue4.Value = Request.QueryString("RptID") 
        ParamDiscreteValue5.Value = Request.QueryString("RptName") 
        ParamDiscreteValue6.Value = Request.QueryString("Decimal") 
        ParamDiscreteValue7.Value = Request.QueryString("DDLAccMth") 
        ParamDiscreteValue8.Value = Request.QueryString("DDLAccYr") 
        ParamDiscreteValue9.Value = Request.QueryString("DateFrom") 
        ParamDiscreteValue10.Value = Request.QueryString("DateTo")
        ParamDiscreteValue11.Value = Request.QueryString("StkAdjIDFrom") 
        ParamDiscreteValue12.Value = Request.QueryString("StkAdjIDTo")
        ParamDiscreteValue13.Value = strAdjType
        ParamDiscreteValue14.Value = strTransType
        ParamDiscreteValue15.Value = Request.QueryString("RefNo")
        ParamDiscreteValue16.Value = Request.QueryString("ProdType")
        ParamDiscreteValue17.Value = Request.QueryString("ProdBrand")
        ParamDiscreteValue18.Value = Request.QueryString("ProdModel")
        ParamDiscreteValue19.Value = Request.QueryString("ProdCat")
        ParamDiscreteValue20.Value = Request.QueryString("ProdMat")
        ParamDiscreteValue21.Value = Request.QueryString("StkAna") 
        ParamDiscreteValue22.Value = Request.QueryString("ItemCode") 
        ParamDiscreteValue23.Value = Request.QueryString("AccCode") 
        ParamDiscreteValue24.Value = Request.QueryString("VehTypeCode") 
        ParamDiscreteValue25.Value = Request.QueryString("VehCode")
        ParamDiscreteValue26.Value = Request.QueryString("VehExpCode") 
        ParamDiscreteValue27.Value = lblBlockCode
        ParamDiscreteValue28.Value = strBlockCode
        ParamDiscreteValue29.Value = strStatus
        ParamDiscreteValue30.Value = Request.QueryString("lblLocation")
        ParamDiscreteValue31.Value = Request.QueryString("lblProdTypeCode")
        ParamDiscreteValue32.Value = Request.QueryString("lblProdBrandCode")
        ParamDiscreteValue33.Value = Request.QueryString("lblProdModelCode")
        ParamDiscreteValue34.Value = Request.QueryString("lblProdCatCode")
        ParamDiscreteValue35.Value = Request.QueryString("lblProdMatCode") 
        ParamDiscreteValue36.Value = Request.QueryString("lblStkAnaCode")
        ParamDiscreteValue37.Value = Request.QueryString("lblAccCode")
        ParamDiscreteValue38.Value = lblBlockCode
        ParamDiscreteValue39.Value = Request.QueryString("lblVehTypeCode")
        ParamDiscreteValue40.Value = Request.QueryString("lblVehCode")
        ParamDiscreteValue41.Value = Request.QueryString("lblVehExpCode")
        ParamDiscreteValue42.Value = lblBlockSubBlockCode 
        ParamDiscreteValue43.Value = Request.QueryString("lblBlkType")
        ParamDiscreteValue44.Value = objINTrx.EnumTransactionType.NewValue
        ParamDiscreteValue45.Value = objINTrx.mtdGetTransactionType(objINTrx.EnumTransactionType.NewValue)
        ParamDiscreteValue46.Value = objINTrx.EnumTransactionType.Difference
        ParamDiscreteValue47.Value = objINTrx.mtdGetTransactionType(objINTrx.EnumTransactionType.Difference)

        ParamDiscreteValue48.Value = objINTrx.EnumAdjustmentType.Quantity
        ParamDiscreteValue49.Value = objINTrx.mtdGetAdjustmentType(objINTrx.EnumAdjustmentType.Quantity)
        ParamDiscreteValue50.Value = objINTrx.EnumAdjustmentType.AverageCost
        ParamDiscreteValue51.Value = objINTrx.mtdGetAdjustmentType(objINTrx.EnumAdjustmentType.AverageCost)
        ParamDiscreteValue52.Value = objINTrx.EnumAdjustmentType.TotalCost
        ParamDiscreteValue53.Value = objINTrx.mtdGetAdjustmentType(objINTrx.EnumAdjustmentType.TotalCost)
        ParamDiscreteValue54.Value = objINTrx.EnumAdjustmentType.QuantityAtAverageCost
        ParamDiscreteValue55.Value = objINTrx.mtdGetAdjustmentType(objINTrx.EnumAdjustmentType.QuantityAtAverageCost)
        ParamDiscreteValue56.Value = objINTrx.EnumAdjustmentType.QuantityAtTotalCost
        ParamDiscreteValue57.Value = objINTrx.mtdGetAdjustmentType(objINTrx.EnumAdjustmentType.QuantityAtTotalCost)

        ParamDiscreteValue58.Value = objINTrx.EnumStockAdjustStatus.Active
        ParamDiscreteValue59.Value = objINTrx.mtdGetStockAdjustStatus(objINTrx.EnumStockAdjustStatus.Active)
        ParamDiscreteValue60.Value = objINTrx.EnumStockAdjustStatus.Confirmed
        ParamDiscreteValue61.Value = objINTrx.mtdGetStockAdjustStatus(objINTrx.EnumStockAdjustStatus.Confirmed)
        ParamDiscreteValue62.Value = objINTrx.EnumStockAdjustStatus.Deleted
        ParamDiscreteValue63.Value = objINTrx.mtdGetStockAdjustStatus(objINTrx.EnumStockAdjustStatus.Deleted)
        ParamDiscreteValue64.Value = objINTrx.EnumStockAdjustStatus.Cancelled
        ParamDiscreteValue65.Value = objINTrx.mtdGetStockAdjustStatus(objINTrx.EnumStockAdjustStatus.Cancelled)
        ParamDiscreteValue66.Value = objINTrx.EnumStockAdjustStatus.Closed
        ParamDiscreteValue67.Value = objINTrx.mtdGetStockAdjustStatus(objINTrx.EnumStockAdjustStatus.Closed)


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
        crParameterValues48.Add(ParamDiscreteValue48)
        crParameterValues49.Add(ParamDiscreteValue49)
        crParameterValues50.Add(ParamDiscreteValue50)
        crParameterValues51.Add(ParamDiscreteValue51)
        crParameterValues52.Add(ParamDiscreteValue52)
        crParameterValues53.Add(ParamDiscreteValue53)
        crParameterValues54.Add(ParamDiscreteValue54)
        crParameterValues55.Add(ParamDiscreteValue55)
        crParameterValues56.Add(ParamDiscreteValue56)
        crParameterValues57.Add(ParamDiscreteValue57)
        crParameterValues58.Add(ParamDiscreteValue58)
        crParameterValues59.Add(ParamDiscreteValue59)
        crParameterValues60.Add(ParamDiscreteValue60)
        crParameterValues61.Add(ParamDiscreteValue61)
        crParameterValues62.Add(ParamDiscreteValue62)
        crParameterValues63.Add(ParamDiscreteValue63)
        crParameterValues64.Add(ParamDiscreteValue64)
        crParameterValues65.Add(ParamDiscreteValue65)
        crParameterValues66.Add(ParamDiscreteValue66)
        crParameterValues67.Add(ParamDiscreteValue67)
        

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
        PFDefs(47).ApplyCurrentValues(crParameterValues48)
        PFDefs(48).ApplyCurrentValues(crParameterValues49)
        PFDefs(49).ApplyCurrentValues(crParameterValues50)
        PFDefs(50).ApplyCurrentValues(crParameterValues51)
        PFDefs(51).ApplyCurrentValues(crParameterValues52)
        PFDefs(52).ApplyCurrentValues(crParameterValues53)
        PFDefs(53).ApplyCurrentValues(crParameterValues54)
        PFDefs(54).ApplyCurrentValues(crParameterValues55)
        PFDefs(55).ApplyCurrentValues(crParameterValues56)
        PFDefs(56).ApplyCurrentValues(crParameterValues57)
        PFDefs(57).ApplyCurrentValues(crParameterValues58)
        PFDefs(58).ApplyCurrentValues(crParameterValues59)
        PFDefs(59).ApplyCurrentValues(crParameterValues60)
        PFDefs(60).ApplyCurrentValues(crParameterValues61)
        PFDefs(61).ApplyCurrentValues(crParameterValues62)
        PFDefs(62).ApplyCurrentValues(crParameterValues63)
        PFDefs(63).ApplyCurrentValues(crParameterValues64)
        PFDefs(64).ApplyCurrentValues(crParameterValues65)
        PFDefs(65).ApplyCurrentValues(crParameterValues66)
        PFDefs(66).ApplyCurrentValues(crParameterValues67)

        crvView.ParameterFieldInfo = paramFields
    End Sub
End Class

