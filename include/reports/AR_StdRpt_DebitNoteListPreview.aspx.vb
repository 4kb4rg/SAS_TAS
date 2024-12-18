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

Public Class AR_StdRpt_DebitNoteList_Preview : Inherits Page
                         
    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objAR As New agri.BI.clsReport()
    Dim objARTrx As New agri.BI.clsTrx()
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

        strFileName = "AR_StdRpt_DebitNoteListing"

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_ARACCMONTH")
        strAccYear = Session("SS_ARACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        strPhyMonth = Session("SS_PHYMONTH")
        strPhyYear = Session("SS_PHYYEAR")

        Session("SS_Decimal") = Request.QueryString("Decimal")
        Session("SS_RptID") = Request.QueryString("RptID")
        Session("SS_RptName") = Request.QueryString("RptName")
        Session("SS_AccMonth") = Request.QueryString("DDLAccMth")
        Session("SS_AccYear") = Request.QueryString("DDLAccYr")

        Session("SS_InvNoFrom") = Request.QueryString("InvNoFrom")
        Session("SS_InvNoTo") = Request.QueryString("InvNoTo")
        Session("SS_lblBillParty") = Request.QueryString("lblBillParty")
        Session("SS_BillParty") = Request.QueryString("BillParty")
        Session("SS_DebitNoteType") = Request.QueryString("DebitNoteType")
        Session("SS_CustomerRef") = Request.QueryString("CustomerRef")
        Session("SS_DeliveryRef") = Request.QueryString("DeliveryRef")
        Session("SS_lblCOA") = Request.QueryString("lblCOA")
        Session("SS_COA") = Request.QueryString("COA")
        Session("SS_lblBlkType") = Request.QueryString("lblBlkType")
        Session("SS_BlkType") = Request.QueryString("BlkType")
        Session("SS_lblBlkGrp") = Request.QueryString("lblBlkGrp")
        Session("SS_BlkGrp") = Request.QueryString("BlkGrp")
        Session("SS_lblBlkCode") = Request.QueryString("lblBlkCode")
        Session("SS_BlkCode") = Request.QueryString("BlkCode")
        Session("SS_lblSubBlkCode") = Request.QueryString("lblSubBlkCode")
        Session("SS_SubBlkCode") = Request.QueryString("SubBlkCode")
        Session("SS_lblVehicle") = Request.QueryString("lblVehicle")
        Session("SS_Vehicle") = Request.QueryString("Vehicle")
        Session("SS_lblVehicleExpCode") = Request.QueryString("lblVehicleExpCode")
        Session("SS_VehicleExpCode") = Request.QueryString("VehicleExpCode")
        Session("SS_ItemDesc") = Request.QueryString("ItemDesc")
        Session("SS_Status") = Request.QueryString("Status")
        Session("SS_lblLocation") = Request.QueryString("lblLocation")


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

        Dim strOpCd_DebitNoteList_GET As String = "AR_STDRPT_DebitNote_LIST"
        Dim SearchStr As String
        Dim SQLStr As String

        Dim intCnt As Integer

        If Not (Request.QueryString("InvNoFrom") = "" AND Request.QueryString("InvNoTo") = "") Then SearchStr = " AND dbn.DebitNoteID >='" & Request.QueryString("InvNoFrom") & "' AND dbn.DebitNoteID <='" & Request.QueryString("InvNoTo") & "'"
        If Not Request.QueryString("BillParty") = "" Then SearchStr = SearchStr & " AND dbn.BillPartyCode LIKE '" & Request.QueryString("BillParty") & "' "

        If Not Request.QueryString("DebitNoteType") = "0" Then SearchStr = SearchStr & " AND dbn.DocType LIKE '" & Request.QueryString("DebitNoteType") & "' "
        If Not Request.QueryString("CustomerRef") = "" Then SearchStr = SearchStr & " AND dbn.CustRef LIKE '" & Request.QueryString("CustomerRef") & "' "
        If Not Request.QueryString("DeliveryRef") = "" Then SearchStr = SearchStr & " AND dbn.DeliveryRef LIKE '" & Request.QueryString("DeliveryRef") & "' "
        If Not Request.QueryString("COA") = "" Then SearchStr = SearchStr & " AND dbnln.AccCode LIKE '" & Request.QueryString("COA") & "' "
        If Not Request.QueryString("BlkCode") = "" Then SearchStr = SearchStr & " AND dbnln.BlkCode LIKE '" & Request.QueryString("BlkCode") & "' "
        If Not Request.QueryString("Vehicle") = "" Then SearchStr = SearchStr & " AND dbnln.VehCode LIKE '" & Request.QueryString("Vehicle") & "' "
        If Not Request.QueryString("VehicleExpCode") = "" Then SearchStr = SearchStr & " AND dbnln.VehExpenseCode LIKE '" & Request.QueryString("VehicleExpCode") & "' "
        If Not Request.QueryString("ItemDesc") = "" Then SearchStr = SearchStr & " AND dbnln.Description LIKE '" & Request.QueryString("ItemDesc") & "' "
        If Not Request.QueryString("Status") = "0" Then SearchStr = SearchStr & " AND dbn.Status LIKE '" & Request.QueryString("Status") & "' "

        strParam =  strUserLoc & "|" & strDDLAccMth & "|" & strDDLAccYr & "|"  & strStatus & "|" & SearchStr
        Try
            intErrNo = objAR.mtdGetReport_DebitNoteList(strOpCd_DebitNoteList_GET, strParam, objRptDs, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=AR_STDRPT_DebitNoteList&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try


        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strFileName & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3

        crvView.Visible = False                         
        crvView.ReportSource = rdCrystalViewer          
        crvView.DataBind()                              

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strFileName & ".pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
 	rdCrystalViewer.Close()
        rdCrystalViewer.Dispose()

        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strFileName & ".pdf"">")
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
        paramField26 = paramFields.Item("ParamInvNoFrom")
        paramField27 = paramFields.Item("ParamInvNoTo")
        paramField28 = paramFields.Item("ParamlblBillParty")
        paramField29 = paramFields.Item("ParamBillParty")
        paramField30 = paramFields.Item("ParamDebitNoteType")
        paramField31 = paramFields.Item("ParamCustomerRef")
        paramField32 = paramFields.Item("ParamDeliveryRef")
        paramField33 = paramFields.Item("ParamItemDesc")
        paramField34 = paramFields.Item("ParamStatus1")
        paramField35 = paramFields.Item("ParamlblLocation")
        paramField36 = paramFields.Item("ParamVehTypeCode")


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


        ParamDiscreteValue1.Value = Session("SS_LOC")
        ParamDiscreteValue2.Value = Session("SS_COA")
        ParamDiscreteValue3.Value = Session("SS_Vehicle")
        ParamDiscreteValue4.Value = Session("SS_VehicleExpCode")
        ParamDiscreteValue5.Value = Session("SS_BlkCode")
        ParamDiscreteValue6.Value = Session("SS_SubBlkCode")
        ParamDiscreteValue7.Value = Session("SS_Status")

        Dim strBlkType As String
        strBlkType = Request.QueryString("BlkType")
        
        If strBlkType = "BlkGrp" Then
            ParamDiscreteValue8.Value = Session("SS_lblBlkGrp")
        ElseIf strBlkType = "BlkCode" Then
            ParamDiscreteValue8.Value = Session("SS_lblBlkCode")
        ElseIf strBlkType = "SubBlkCode" Then
            ParamDiscreteValue8.Value = Session("SS_lblSubBlkCode")
        Else ParamDiscreteValue8.Value = strBlkType
        End If

        ParamDiscreteValue9.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue10.Value = Session("SS_USERNAME")
        ParamDiscreteValue11.Value = Session("SS_DECIMAL")
        ParamDiscreteValue12.Value = Session("SS_RPTID")
        ParamDiscreteValue13.Value = Session("SS_RPTNAME")
        ParamDiscreteValue14.Value = Session("SS_ACCMONTH")
        ParamDiscreteValue15.Value = Session("SS_ACCYEAR")
        ParamDiscreteValue16.Value = Session("SS_lblCOA")
        ParamDiscreteValue17.Value = Session("SS_lblVehicle")
        ParamDiscreteValue18.Value = Session("SS_lblVehicleExpCode")
        ParamDiscreteValue19.Value = Session("SS_LBLBLKCODE")
        ParamDiscreteValue20.Value = Session("SS_LBLSUBBLKCODE")
        ParamDiscreteValue21.Value = "" 'Session("SS_ITEMCODE")
        ParamDiscreteValue22.Value = Session("SS_LBLLOCATION")
        ParamDiscreteValue23.Value = Session("SS_BLKTYPE")
        ParamDiscreteValue24.Value = Session("SS_BLKGRP")
        ParamDiscreteValue25.Value = Session("SS_LBLBLKGRP")
        ParamDiscreteValue26.Value = Session("SS_InvNoFrom")
        ParamDiscreteValue27.Value = Session("SS_InvNoTo")
        ParamDiscreteValue28.Value = Session("SS_lblBillParty")
        ParamDiscreteValue29.Value = Session("SS_BillParty")
        ParamDiscreteValue30.Value = Session("SS_DebitNoteType")
        ParamDiscreteValue31.Value = Session("SS_CustomerRef")
        ParamDiscreteValue32.Value = Session("SS_DeliveryRef")
        ParamDiscreteValue33.Value = Session("SS_ItemDesc")
        ParamDiscreteValue34.Value = "StatusText"
        ParamDiscreteValue35.Value = "LblLocation"
        ParamDiscreteValue36.Value = Session("SS_Vehicle")


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


        crvView.ParameterFieldInfo = paramFields

    End Sub    

End Class
