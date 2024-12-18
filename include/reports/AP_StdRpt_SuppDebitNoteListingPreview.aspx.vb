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

Public Class AP_StdRpt_SuppDebitNoteListing_Preview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
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

    Dim strStatus As String
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

    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer

    Dim dsTrx As New DataSet()
    Dim dsStmtTrx As New DataSet()
    Dim dsComp As New DataSet()


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
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

        strDDLAccMth = Request.QueryString("DDLAccMth")
        strDDLAccYr = Request.QueryString("DDLAccYr")

        strFileName = "AP_StdRpt_SuppDebitNoteListing"

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

        Session("SS_DebitNoteIDFrom") = Request.QueryString("txtDebitNoteIDFrom")
        Session("SS_DebitNoteIDTo") = Request.QueryString("txtDebitNoteIDTo")

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
        Session("SS_SuppDebitNoteRefNo") = Request.QueryString("txtSuppDebitNoteRefNo")
        Session("SS_SuppDebitNoteRefDateFrom") = Request.QueryString("txtSuppDebitNoteRefDateFrom")
        Session("SS_SuppDebitNoteRefDateTo") = Request.QueryString("txtSuppDebitNoteRefDateTo")
        Session("SS_SuppCode") = Request.QueryString("txtSuppCode")
        Session("SS_COACode") = Request.QueryString("txtCOACode")
        Session("SS_SuppDebitNoteIDFrom") = Request.QueryString("txtSuppDebitNoteIDFrom")
        Session("SS_SuppDebitNoteIDTo") = Request.QueryString("txtSuppDebitNoteIDTo")


        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else

            BindReport()
        End If
    End Sub



    Sub BindReport()

        Dim objRptDs As New DataSet()

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOpCd_SuppDebitNoteListing_GET As String = "AP_STDRPT_DEBITNOTELISTING_GET"
        Dim SearchStr As String
        Dim SQLStr As String
        Dim objFTPFolder As String
        Dim intCnt As Integer

        If Not (Request.QueryString("txtSuppDebitNoteIDFrom") = "" And Request.QueryString("txtSuppDebitNoteIDFromIDTo") = "") Then SearchStr = SearchStr & " AND APD.DebitNoteID >= '" & Request.QueryString("txtSuppDebitNoteIDFrom") & "' AND  APD.DebitNoteID <= '" & Request.QueryString("txtSuppDebitNoteIDTo") & "'"
        If Not Request.QueryString("txtSuppDebitNoteRefNo") = "" Then SearchStr = SearchStr & " AND APD.SupplierDocRefNo LIKE '" & Request.QueryString("txtSuppDebitNoteRefNo") & "' "
        If Not Request.QueryString("txtCOACode") = "" Then SearchStr = SearchStr & "AND APDLN.AccCode LIKE '" & Request.QueryString("txtCOACode") & "' "
        If Not Request.QueryString("txtBlock") = "" Then SearchStr = SearchStr & "AND APDLN.BlkCode LIKE '" & Request.QueryString("txtBlock") & "' "
        If Not Request.QueryString("txtVehicleType") = "" Then SearchStr = SearchStr & "AND GLV.VehTypeCode LIKE '" & Request.QueryString("txtVehicleType") & "' "
        If Not Request.QueryString("txtVehicle") = "" Then SearchStr = SearchStr & "AND APDLN.VehCode LIKE '" & Request.QueryString("txtVehicle") & "' "
        If Not Request.QueryString("txtVehicleExp") = "" Then SearchStr = SearchStr & "AND APDLN.VehExpenseCode LIKE '" & Request.QueryString("txtVehicleExp") & "' "
        If Not Request.QueryString("txtStatus") = "0" Then SearchStr = SearchStr & "AND APD.Status LIKE '" & Request.QueryString("txtStatus") & "' "
        If Not Request.QueryString("txtSuppCode") = "" Then SearchStr = SearchStr & "AND (APD.SupplierCode LIKE '%" & Request.QueryString("txtSuppCode") & "%' OR PUSS.Name LIKE '%" & Request.QueryString("txtSuppCode") & "%') "
        If Not Request.QueryString("InvoiceRcvRefNo") = "" Then SearchStr = SearchStr & "AND APD.InvoiceRcvRefNo LIKE '" & Request.QueryString("InvoiceRcvRefNo") & "' "
        If Not (Request.QueryString("txtSuppDebitNoteRefDateFrom") = "" And Request.QueryString("txtSuppDebitNoteRefDateTo") = "") Then
            SearchStr = SearchStr & " AND (DateDiff(Day, '" & Session("SS_SuppDebitNoteRefDateFrom") & "', APD.SupplierDocRefDate) >= 0) And (DateDiff(Day, '" & Session("SS_SuppDebitNoteRefDateTo") & "', APD.SupplierDocRefDate) <= 0) "
        End If


        strParam = strUserLoc & "|" & strDDLAccMth & "|" & strDDLAccYr & "|" & strStatus & "|" & SearchStr
        Try
            intErrNo = objAP.mtdGetReport_SuppDebitNoteListing(strOpCd_SuppDebitNoteListing_GET, strParam, objRptDs, objMapPath, objFTPFolder)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=APD_StdRpt_SuppDebitNoteListing&errmesg=" & lblErrMessage.Text & "&redirect=")
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
        paramField21 = paramFields.Item("lblLocation")
        paramField22 = paramFields.Item("ParamBlkType")
        paramField23 = paramFields.Item("ParamBlkGrp")
        paramField24 = paramFields.Item("lblBlkGrp")
        paramField25 = paramFields.Item("lblVehicle")
        paramField26 = paramFields.Item("ParamVehTypeCode")
        paramField27 = paramFields.Item("ParamSuppDebitNoteRefNo")
        paramField28 = paramFields.Item("ParamSuppDebitNoteRefDateFrom")
        paramField29 = paramFields.Item("ParamSuppCode")
        paramField30 = paramFields.Item("ParamCOACode")
        paramField31 = paramFields.Item("ParamSuppDebitNoteRefDateTo")
        paramField32 = paramFields.Item("ParamSuppDebitNoteIDFrom")
        paramField33 = paramFields.Item("ParamSuppDebitNoteIDTo")
        paramField34 = paramFields.Item("lblInvoiceRcvRefNo")
        paramField35 = paramFields.Item("ParamInvoiceRcvRefNo")

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
        ParamDiscreteValue21.Value = Session("SS_LBLLOCATION")
        ParamDiscreteValue22.Value = Session("SS_BLKTYPE")
        ParamDiscreteValue23.Value = Session("SS_BLKGRP")
        ParamDiscreteValue24.Value = Session("SS_LBLBLKGRP")
        ParamDiscreteValue25.Value = Session("SS_LBLVEHTYPECODE")
        ParamDiscreteValue26.Value = Session("SS_VEHTYPECODE")
        ParamDiscreteValue27.Value = Session("SS_SuppDebitNoteRefNo")
        ParamDiscreteValue28.Value = Session("SS_SuppDebitNoteREFDATEFROM")
        ParamDiscreteValue29.Value = Session("SS_SUPPCODE")
        ParamDiscreteValue30.Value = Session("SS_COACODE")
        ParamDiscreteValue31.Value = Session("SS_SuppDebitNoteREFDATETO")
        ParamDiscreteValue32.Value = Session("SS_SuppDebitNoteIDFrom")
        ParamDiscreteValue33.Value = Session("SS_SuppDebitNoteIDTo")
        ParamDiscreteValue34.Value = Request.QueryString("lblInvoiceRcvRefNo")
        ParamDiscreteValue35.Value = Request.QueryString("InvoiceRcvRefNo")

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

        crvView.ParameterFieldInfo = paramFields

    End Sub

End Class
