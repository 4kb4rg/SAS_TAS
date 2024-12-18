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

Public Class AP_StdRpt_InvRcvListing_Preview : Inherits Page
                         
    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents EventData As DataGrid

    Dim objAP As New agri.AP.clsReport()
    Dim objAPTrx As New agri.AP.clsTrx()
    Dim objPUSetup As New agri.PU.clsSetup()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdminComp As New agri.Admin.clsComp()
    Dim objAdminCty As New agri.Admin.clsCountry()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strUserLoc As String
    Dim strPhyMonth As String
    Dim strPhyYear As String

    Dim strStatus as String
    Dim tempLoc As String
    Dim strDDLAccMth As String
    Dim strDDLAccYr As String
    Dim strParam As String
    Dim strDate As String
    Dim strSuppCode As String
    Dim objMapPath As String
    Dim arrUserLoc As Array
    Dim intCntUserLoc As Integer
    Dim strLocCode As String
    Dim strOrderBy As String
    Dim strFileName As String
    Dim strSelPhyMonth As String
    Dim strSelPhyYear As String
    Dim objDsAgeingYTDAccPeriod As String

    Dim strInvRcvTag As String
    Dim intConfigsetting As String 

    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_APACCMONTH")
        strAccYear = Session("SS_APACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        strPhyMonth = Session("SS_PHYMONTH")
        strPhyYear = Session("SS_PHYYEAR")
        intConfigsetting = Session("SS_CONFIGSETTING") 


        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            crvView.Visible = False
            strStatus = Request.QueryString("ddlStatus")

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
            strInvRcvTag = Trim(Request.QueryString("lblInvRcv"))
            strDDLAccMth = Request.QueryString("DDLAccMth")
            strDDLAccYr = Request.QueryString("DDLAccYr")
            strFileName = "AP_StdRpt_InvRcvListing"

            Session("SS_lblProdTypeCode") = Request.QueryString("lblProdTypeCode")
            Session("SS_lblProdBrandCode") = Request.QueryString("lblProdBrandCode")
            Session("SS_lblProdModelCode") = Request.QueryString("lblProdModelCode")
            Session("SS_lblProdCatCode") = Request.QueryString("lblProdCatCode")     
            Session("SS_lblProdMatCode") = Request.QueryString("lblProdMatCode") 
            Session("SS_lblStkAnaCode") = Request.QueryString("lblStkAnaCode")    
            Session("SS_lblAccCode") = Request.QueryString("lblCOACode")
            Session("SS_lblVehCode") = Request.QueryString("lblVehicle")
            Session("SS_lblVehTypeCode") = Request.QueryString("lblVehicleType")
            Session("SS_lblVehExpCode") = Request.QueryString("lblVehicleExp")
            Session("SS_lblBlkType") = Request.QueryString("lblBlkType")
            Session("SS_lblBlkGrp") = Request.QueryString("lblBlkGrp")
            Session("SS_lblBlkCode") = Request.QueryString("lblBlkCode")
            Session("SS_lblSubBlkCode") = Request.QueryString("lblSubBlkCode")
            Session("SS_LBLLOCATION") = Request.QueryString("lblLocation")
            Session("SS_Decimal") = Request.QueryString("Decimal")
            Session("SS_RptID") = Request.QueryString("RptID")
            Session("SS_RptName") = Request.QueryString("RptName")
            Session("SS_AccMonth") = Request.QueryString("DDLAccMth")
            Session("SS_AccYear") = Request.QueryString("DDLAccYr")
            Session("SS_ProdType") = Request.QueryString("txtProdType")
            Session("SS_ProdBrand") = Request.QueryString("txtProdBrand")
            Session("SS_ProdModel") = Request.QueryString("txtProdModel")
            Session("SS_ProdCat") = Request.QueryString("txtProdCat")
            Session("SS_ProdMat") = Request.QueryString("txtProdMaterial")
            Session("SS_StkAna") = Request.QueryString("txtStkAna")
            Session("SS_ItemCode") = Request.QueryString("txtItemCode")
            Session("SS_AccCode") = Request.QueryString("txtCOACode")
            Session("SS_VehCode") = Request.QueryString("txtVehicle")
            Session("SS_VehTypeCode") = Request.QueryString("txtVehicleType")
            Session("SS_VehExpCode") = Request.QueryString("txtVehicleExp")
            If Request.QueryString("lstBlkType") = "BlkGrp" Then
                Session("SS_BlkType") = Request.QueryString("lblBlkGrp")
            ElseIf Request.QueryString("lstBlkType") = "BlkCode" Then
                Session("SS_BlkType") = Request.QueryString("lblBlkCode")
            ElseIf Request.QueryString("lstBlkType") = "SubBlkCode" Then
                Session("SS_BlkType") = Request.QueryString("lblSubBlkCode")
            End If
            Session("SS_BlkGrp") = Request.QueryString("txtBlkGrp")
            Session("SS_BlkCode") = Request.QueryString("txtBlkCode")
            Session("SS_SubBlkCode") = Request.QueryString("txtSubBlkCode")

            Session("SS_Status") = Request.QueryString("txtStatus")
            Session("SS_InvRcvNo") = Request.QueryString("txtInvRcvNo")
            Session("SS_POID") = Request.QueryString("txtPOID")
            Session("SS_DocNoFrom") = Request.QueryString("DocNoFrom")
            Session("SS_DocNoTo") = Request.QueryString("DocNoTo")
            Session("SS_InvRcvRefDateFrom") = Request.QueryString("txtInvRcvRefDateFrom")
            Session("SS_InvRcvRefDateTo") = Request.QueryString("txtInvRcvRefDateTo")
            Session("SS_CreditTermType") = Request.QueryString("txtCreditTermType")
            Session("SS_CreditTerm") = Request.QueryString("txtCreditTerm")
            Session("SS_SuppCode") = Request.QueryString("txtSuppCode")
            Session("SS_COACode") = Request.QueryString("txtCOACode")
            Select Case Trim(Request.QueryString("ddlInvoiceType"))
                Case objAPTrx.EnumInvoiceType.SupplierPO 
                    Session("SS_InvoiceType") = objAPTrx.mtdGetInvoiceType(objAPTrx.EnumInvoiceType.SupplierPO)
                Case objAPTrx.EnumInvoiceType.ContractorWorkOrder
                    Session("SS_InvoiceType") = objAPTrx.mtdGetInvoiceType(objAPTrx.EnumInvoiceType.ContractorWorkOrder)
                Case Else
                    Session("SS_InvoiceType") = "All"
            End Select
            BindReport()
        End If
    End Sub



    Sub BindReport()
        Dim objRptDs As New DataSet()
        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim strOpCd_InvRcvListing_GET As String = "AP_STDRPT_INVRCVLISTING_GET"
        Dim SearchStr As String
        Dim SQLStr As String
        Dim intCnt As Integer
        Dim objFTPFolder As String
        SearchStr = ""    

       

        If Not Request.QueryString("txtInvRcvNo") = "" Then SearchStr = SearchStr & " AND AP.InvoiceRcvRefNo LIKE '" & Request.QueryString("txtInvRcvNo") & "' "
        If Not Request.QueryString("txtPOID") = "" Then SearchStr = SearchStr & "AND AP.POID LIKE '" & Request.QueryString("txtPOID") & "' "
        If Not Request.QueryString("txtCreditTerm") = "" Then SearchStr = SearchStr & "AND AP.CreditTerm LIKE '" & Request.QueryString("txtCreditTerm") & "' "
        If Not Request.QueryString("txtCreditTermTypeValue") = "13" Then SearchStr = SearchStr & "AND AP.TermType LIKE '" & Request.QueryString("txtCreditTermTypeValue") & "' "
        If Not Request.QueryString("txtProdType") = "" Then SearchStr = SearchStr & "AND IN_ITEM.ProdTypeCode LIKE '" & Request.QueryString("txtProdType") & "' "
        If Not Request.QueryString("txtProdBrand") = "" Then SearchStr = SearchStr & "AND IN_ITEM.ProdBrandCode LIKE '" & Request.QueryString("txtProdBrand") & "' "
        If Not Request.QueryString("txtProdModel") = "" Then SearchStr = SearchStr & "AND IN_ITEM.ProdModelCode LIKE '" & Request.QueryString("txtProdModel") & "' "
        If Not Request.QueryString("txtProdCat") = "" Then SearchStr = SearchStr & "AND IN_ITEM.ProdCatCode LIKE '" & Request.QueryString("txtProdCat") & "' "
        If Not Request.QueryString("txtProdMaterial") = "" Then SearchStr = SearchStr & "AND IN_ITEM.ProdMatCode LIKE '" & Request.QueryString("txtProdMaterial") & "' "
        If Not Request.QueryString("txtStkAna") = "" Then SearchStr = SearchStr & "AND IN_ITEM.StockAnalysisCode LIKE '" & Request.QueryString("txtStkAna") & "' "
        If Not Request.QueryString("txtItemCode") = "" Then SearchStr = SearchStr & "AND APLN.ItemCode LIKE '" & Request.QueryString("txtItemCode") & "' "      
        If Not Request.QueryString("txtCOACode") = "" Then SearchStr = SearchStr & "AND APLN.AccCode LIKE '" & Request.QueryString("txtCOACode") & "' "
        
        If Not Request.QueryString("txtBlock") = "" Then SearchStr = SearchStr & "AND APLN.BlkCode LIKE '" & Request.QueryString("txtBlock") & "' "       
        If Not Request.QueryString("txtVehicleType") = "" Then SearchStr = SearchStr & "AND GL_VEHICLE.VehTypeCode LIKE '" & Request.QueryString("txtVehicleType") & "' "       
        If Not Request.QueryString("txtVehicle") = "" Then SearchStr = SearchStr & "AND APLN.VehCode LIKE '" & Request.QueryString("txtVehicle") & "' "
        If Not Request.QueryString("txtVehicleExp") = "" Then SearchStr = SearchStr & "AND APLN.VehExpenseCode LIKE '" & Request.QueryString("txtVehicleExp") & "' "
        If Not Request.QueryString("txtStatus") = "0" Then SearchStr = SearchStr & "AND AP.Status LIKE '" & Request.QueryString("txtStatus") & "' "
        If Not Request.QueryString("ddlInvoiceType") = "0" Then SearchStr = SearchStr & "AND AP.InvoiceType LIKE '" & Request.QueryString("ddlInvoiceType") & "' "
        If Not Request.QueryString("txtSuppCode") = "" Then SearchStr = SearchStr & "AND (AP.SupplierCode LIKE '%" & Request.QueryString("txtSuppCode") & "%' Or PU_SUPPLIER.Name LIKE '%" & Request.QueryString("txtSuppCode") & "%')"
        If Not (Request.QueryString("txtInvRcvRefDateFrom") = "" And Request.QueryString("txtInvRcvRefDateTo") = "") Then
            SearchStr = SearchStr & " AND (DateDiff(Day, '" & Session("SS_InvRcvRefDateFrom") & "', AP.InvoiceRcvRefDate) >= 0) And (DateDiff(Day, '" & Session("SS_InvRcvRefDateTo") & "', AP.InvoiceRcvRefDate) <= 0) "
        End If

        If (Len(Request.QueryString("DocNoFrom")) > 0) And (Len(Request.QueryString("DocNoTo")) > 0) Then
            SearchStr = SearchStr & "AND AP.InvoiceRcvID >= '" & Request.QueryString("DocNoFrom") & "' AND AP.InvoiceRcvID <= '" & Request.QueryString("DocNoTo") & "' "
        ElseIf (Len(Request.QueryString("DocNoFrom")) > 0) Then
            SearchStr = SearchStr & "AND AP.InvoiceRcvID LIKE '" & Request.QueryString("DocNoFrom") & "' "
        End If

        strParam =  strUserLoc & "|" & strDDLAccMth & "|" & strDDLAccYr & "|"  & strStatus & "|" & SearchStr
        Try
            intErrNo = objAP.mtdGetReport_InvRcvListing(strOpCd_InvRcvListing_GET, strParam, objRptDs, objMapPath, objFTPFolder)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=AP_STDRPT_InvRcvListing&errmesg=" & Exp.ToString() & "&redirect=")
        End Try


        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strFileName & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        crvView.Visible = False                         
        crvView.ReportSource = rdCrystalViewer          
        crvView.DataBind()                              

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        'crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strFileName & ".pdf"
        crDiskFileDestinationOptions.DiskFileName = objFTPFolder & strFileName & ".pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With


        rdCrystalViewer.Export()
        rdCrystalViewer.Close()
        rdCrystalViewer.Dispose()

        Dim strUrl As String
        strUrl = objFTPFolder.Substring(InStr(objFTPFolder, "ftp") - 1, Len(objFTPFolder) - InStr(objFTPFolder, "ftp") + 1)
        strUrl = Replace(strUrl, "\", "/")
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../" & strUrl & strFileName & ".pdf"">")

        'Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strFileName & ".pdf"">")

        objRptDs = Nothing
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

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("ParamLocation")
        paramField2 = paramFields.Item("ParamAccCode")
        paramField3 = paramFields.Item("ParamVehCode")
        paramField4 = paramFields.Item("ParamVehExpCode")
        paramField5 = paramFields.Item("ParamBlkCode")
        paramField6 = paramFields.Item("ParamSubBlkCode")
        paramField7 = paramFields.Item("ParamStatus")
        paramField8 = paramFields.Item("ParamBlkOrSubBlk")
        paramField9 = paramFields.Item("ParamCompanyName")
        paramField10 = paramFields.Item("ParamUserName")
        paramField11 = paramFields.Item("ParamDecimal")
        paramField12 = paramFields.Item("ParamRptID")
        paramField13 = paramFields.Item("ParamRptName")
        paramField14 = paramFields.Item("ParamAccMonth")
        paramField15 = paramFields.Item("ParamAccYear")
        paramField16 = paramFields.Item("lblCOACode")
        paramField17 = paramFields.Item("lblVehicleExp")
        paramField18 = paramFields.Item("lblVehExpCode")
        paramField19 = paramFields.Item("lblBlkCode")
        paramField20 = paramFields.Item("lblSubBlkCode")
        paramField21 = paramFields.Item("ParamItemCode")
        paramField22 = paramFields.Item("lblLocation")
        paramField23 = paramFields.Item("ParamBlkType")
        paramField24 = paramFields.Item("ParamBlkGrp")
        paramField25 = paramFields.Item("lblBlkGrp")
        paramField26 = paramFields.Item("ParamProdType")
        paramField27 = paramFields.Item("ParamProdBrand")
        paramField28 = paramFields.Item("ParamProdModel")
        paramField29 = paramFields.Item("ParamProdCat")
        paramField30 = paramFields.Item("ParamProdMat")
        paramField31 = paramFields.Item("ParamStkAna")
        paramField32 = paramFields.Item("lblProdTypeCode")
        paramField33 = paramFields.Item("lblProdBrandCode")
        paramField34 = paramFields.Item("lblProdModelCode")
        paramField35 = paramFields.Item("lblProdCatCode")
        paramField36 = paramFields.Item("lblProdMatCode")
        paramField37 = paramFields.Item("lblStkAnaCode")
        paramField38 = paramFields.Item("lblVehicle")
        paramField39 = paramFields.Item("ParamVehTypeCode")
        paramField40 = paramFields.Item("ParamInvRcvNo")
        paramField41 = paramFields.Item("ParamPOID")
        paramField42 = paramFields.Item("ParamInvRcvRefDateFrom")
        paramField43 = paramFields.Item("ParamCreditTermType")
        paramField44 = paramFields.Item("ParamCreditTerm")
        paramField45 = paramFields.Item("ParamSuppCode")
        paramField46 = paramFields.Item("ParamCOACode")
        paramField47 = paramFields.Item("ParamInvRcvRefDateTo")
        paramField48 = paramFields.Item("ParamInvoiceType")
        paramField49 = paramFields.Item("ParamDocNoFrom")
        paramField50 = paramFields.Item("ParamDocNoTo")
        paramField51 = paramFields.Item("lblInvRcv_UCase")
        paramField52 = paramFields.Item("lblInvRcv_NCase")

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

        ParamDiscreteValue1.Value = Session("SS_LOC")
        ParamDiscreteValue2.Value = Session("SS_AccCode")
        ParamDiscreteValue3.Value = Session("SS_VehCode")
        ParamDiscreteValue4.Value = Session("SS_VehExpCode")
        ParamDiscreteValue5.Value = Session("SS_BlkCode")
        ParamDiscreteValue6.Value = Session("SS_SubBlkCode")
        ParamDiscreteValue7.Value = Session("SS_Status")

        Dim strBlkType As String
        strBlkType = Request.QueryString("lstBlkType")
        
        If strBlkType = "BlkGrp" Then
            ParamDiscreteValue8.Value = "BlkGrp"
        ElseIf strBlkType = "BlkCode" Then
            ParamDiscreteValue8.Value = "BlkCode"
        ElseIf strBlkType = "SubBlkCode" Then
            ParamDiscreteValue8.Value = "SubBlkCode"
        End If

        ParamDiscreteValue9.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue10.Value = Session("SS_USERNAME")
        ParamDiscreteValue11.Value = Session("SS_DECIMAL")
        ParamDiscreteValue12.Value = Session("SS_RPTID")
        ParamDiscreteValue13.Value = Session("SS_RPTNAME")
        ParamDiscreteValue14.Value = Session("SS_ACCMONTH")
        ParamDiscreteValue15.Value = Session("SS_ACCYEAR")
        ParamDiscreteValue16.Value = Session("SS_LBLACCCODE")
        ParamDiscreteValue17.Value = Session("SS_LBLVEHCODE")
        ParamDiscreteValue18.Value = Session("SS_LBLVEHEXPCODE")
        ParamDiscreteValue19.Value = Session("SS_LBLBLKCODE")
        ParamDiscreteValue20.Value = Session("SS_LBLSUBBLKCODE")
        ParamDiscreteValue21.Value = Session("SS_ITEMCODE")
        ParamDiscreteValue22.Value = Session("SS_LBLLOCATION")
        ParamDiscreteValue23.Value = Session("SS_BLKTYPE")
        ParamDiscreteValue24.Value = Session("SS_BLKGRP")
        ParamDiscreteValue25.Value = Session("SS_LBLBLKGRP")
        ParamDiscreteValue26.Value = Session("SS_PRODTYPE")
        ParamDiscreteValue27.Value = Session("SS_PRODBRAND")
        ParamDiscreteValue28.Value = Session("SS_PRODMODEL")
        ParamDiscreteValue29.Value = Session("SS_PRODCAT")
        ParamDiscreteValue30.Value = Session("SS_PRODMAT")
        ParamDiscreteValue31.Value = Session("SS_STKANA")
        ParamDiscreteValue32.Value = Session("SS_LBLPRODTYPECODE")
        ParamDiscreteValue33.Value = Session("SS_LBLPRODBRANDCODE")
        ParamDiscreteValue34.Value = Session("SS_LBLPRODMODELCODE")
        ParamDiscreteValue35.Value = Session("SS_LBLPRODCATCODE")
        ParamDiscreteValue36.Value = Session("SS_LBLPRODMATCODE")
        ParamDiscreteValue37.Value = Session("SS_LBLSTKANACODE")
        ParamDiscreteValue38.Value = Session("SS_LBLVEHTYPECODE")
        ParamDiscreteValue39.Value = Session("SS_VEHTYPECODE")
        ParamDiscreteValue40.Value = Session("SS_INVRCVNO")
        ParamDiscreteValue41.Value = Session("SS_POID")
        ParamDiscreteValue42.Value = Session("SS_INVRCVREFDATEFROM")
        ParamDiscreteValue43.Value = Session("SS_CREDITTERMTYPE")
        ParamDiscreteValue44.Value = Session("SS_CREDITTERM")
        ParamDiscreteValue45.Value = Session("SS_SUPPCODE")
        ParamDiscreteValue46.Value = Session("SS_COACODE")
        ParamDiscreteValue47.Value = Session("SS_INVRCVREFDATETO")
        ParamDiscreteValue48.Value = Session("SS_InvoiceType")
        ParamDiscreteValue49.Value = Session("SS_DocNoFrom")
        ParamDiscreteValue50.Value = Session("SS_DocNoTo")
        ParamDiscreteValue51.Value = UCase(strInvRcvTag)
        ParamDiscreteValue52.Value = strInvRcvTag

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

        crvView.ParameterFieldInfo = paramFields
    End Sub    
End Class
