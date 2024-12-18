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
Imports System.IO

Public Class WM_StdRpt_WeighBridge_Ticket_ListingPreview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objWM As New agri.WM.clsReport()
    Dim objWMTrx As New agri.WM.clsTrx()

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

    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        crvView.Visible = False

        If Left(Request.QueryString("Location"), 3) = "','" Then
            strUserLoc = Right(Request.QueryString("Location"), Len(Request.QueryString("Location")) - 3)
        Else
            strUserLoc = Trim(Request.QueryString("Location"))
        End If

        tempLoc = Request.QueryString("Location")
          If Right(tempLoc, 1) = "," Then
            Session("SS_LOC") = Left(tempLoc, Len(tempLoc) - 1)
        Else
            Session("SS_LOC") = tempLoc.Replace("','", ", ")
        End If


        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_PMACCMONTH")
        strAccYear = Session("SS_PMACCYEAR")
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

        Dim SearchStr As String
        Dim strParam As String

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOpCode_GET As String = "WM_STDRPT_WEIGHBRIDGE_TICKET_LISTING"

        If Not (Request.QueryString("DateInFrom") = "" And Request.QueryString("DateInTo") = "") Then
            SearchStr = " AND (DateDiff(Day, '" & Request.QueryString("DateInFrom") & "', WMT.INDATE) >= 0) And (DateDiff(Day, '" & Request.QueryString("DateInTo") & "', WMT.INDATE) <= 0)"
        End If

        If Not (Request.QueryString("InHour") = "" Or Request.QueryString("InHourTo") = "" Or Request.QueryString("InMinute") = "" Or Request.QueryString("InMinuteTo") = "") Then
            SearchStr = SearchStr & " AND (DateDiff(mi, '" & Request.QueryString("InHour") & ":" & Request.QueryString("InMinute") & Request.QueryString("InAMPM") & "', RIGHT(WMT.INDATE,7)) >= 0) " & _
                        "AND (DateDiff(mi, '" & Request.QueryString("InHourTo") & ":" & Request.QueryString("InMinuteTo") & Request.QueryString("InAMPMTo") & "', RIGHT(WMT.INDATE,7)) <= 0)"
        End If

        If Not (Request.QueryString("DateOutFrom") = "" And Request.QueryString("DateOutTo") = "") Then
            SearchStr = SearchStr & " AND (DateDiff(Day, '" & Request.QueryString("DateOutFrom") & "', WMT.INDATE) >= 0) And (DateDiff(Day, '" & Request.QueryString("DateOutTo") & "', WMT.INDATE) <= 0)"
        End If

        If Not (Request.QueryString("OutHour") = "" Or Request.QueryString("OutHourTo") = "" Or Request.QueryString("OutMinute") = "" Or Request.QueryString("OutMinuteTo") = "") Then
            SearchStr = SearchStr & " AND (DateDiff(mi, '" & Request.QueryString("OutHour") & ":" & Request.QueryString("OutMinute") & Request.QueryString("OutAMPM") & "', RIGHT(WMT.OUTDATE,7)) >= 0) " & _
                        "AND (DateDiff(mi, '" & Request.QueryString("OutHourTo") & ":" & Request.QueryString("OutMinuteTo") & Request.QueryString("OutAMPMTo") & "', RIGHT(WMT.OUTDATE,7)) <= 0)"
        End If

        If Not (Request.QueryString("DateRcv") = "" And Request.QueryString("DateRcvTo") = "") Then
            SearchStr = SearchStr & " AND (DateDiff(Day, '" & Request.QueryString("DateRcv") & "', WMT.DATERECEIVED) >= 0) And (DateDiff(Day, '" & Request.QueryString("DateRcvTo") & "', WMT.DATERECEIVED) <= 0)"
        End If

        If Not Request.QueryString("TicketNo") = "" Then
            SearchStr = SearchStr & " AND WMT.TICKETNO LIKE '" & Request.QueryString("TicketNo") & "'"
        End If

        If Not Request.QueryString("TransactionType") = "0" Then
            SearchStr = SearchStr & " AND WMT.TRANSTYPE = '" & Request.QueryString("TransactionType") & "'"
        End If

        If Not Request.QueryString("Product") = "0" Then
            SearchStr = SearchStr & " AND WMT.PRODUCTCODE = '" & Request.QueryString("Product") & "'"
        End If

        If Not Request.QueryString("SuppBillParty") = "" Then
            SearchStr = SearchStr & " AND WMT.CUSTOMERCODE LIKE '" & Request.QueryString("SuppBillParty") & "'"
        End If

        If Not Request.QueryString("DocRefNo") = "" Then
            SearchStr = SearchStr & " AND WMT.CUSTOMERDOCNO LIKE '" & Request.QueryString("DocRefNo") & "'"
        End If

        If Not Request.QueryString("DeliveryNoteNo") = "" Then
            SearchStr = SearchStr & " AND WMT.DELIVERYNOTENO LIKE '" & Request.QueryString("DeliveryNoteNo") & "'"
        End If

        If Not Request.QueryString("PL3No") = "" Then
            SearchStr = SearchStr & " AND WMT.PL3NO LIKE '" & Request.QueryString("PL3No") & "'"
        End If

        If Not Request.QueryString("Transporter") = "" Then
            SearchStr = SearchStr & " AND WMT.TRANSPORTERCODE LIKE '" & Request.QueryString("Transporter") & "'"
        End If

        If Not Request.QueryString("Vehicle") = "" Then
            SearchStr = SearchStr & " AND WMT.VEHICLECODE LIKE '" & Request.QueryString("Vehicle") & "'"
        End If

        If Not Request.QueryString("DriverName") = "" Then
            SearchStr = SearchStr & " AND WMT.DRIVERNAME LIKE '" & Request.QueryString("DriverName") & "'"
        End If

        If Not Request.QueryString("DriverICNo") = "" Then
            SearchStr = SearchStr & " AND WMT.DRIVERIC LIKE '" & Request.QueryString("DriverICNo") & "'"
        End If

        If Not Request.QueryString("PlantingYear") = "" Then
            SearchStr = SearchStr & " AND WMT.PLANTINGYEAR LIKE '" & Request.QueryString("PlantingYear") & "'"
        End If

        If Not Request.QueryString("Block") = "" Then
            SearchStr = SearchStr & " AND WMT.BLKCODE LIKE '" & Request.QueryString("Block") & "'"
        End If

        If Request.QueryString("Status") = "All" Then
        ElseIf Request.QueryString("Status") = "Active" Then
            SearchStr = SearchStr & " AND WMT.Status = '" & objWMTrx.EnumWeighBridgeTicketStatus.Active & "'"
        ElseIf Request.QueryString("Status") = "Deleted" Then
            SearchStr = SearchStr & " AND WMT.Status = '" & objWMTrx.EnumWeighBridgeTicketStatus.Deleted & "'"
        End If

        strParam = strUserLoc & "|" & Request.QueryString("DDLAccMth") & "|" & Request.QueryString("DDLAccYr") & "||" & SearchStr
        Try
            intErrNo = objWM.mtdGetReport_WeighBridgeTransactionList(strOpCode_GET, strParam, objRptDs, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_WM_WEIGHBRIDGE_TRX_LIST_REPORT&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try



        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\WM_StdRpt_WeighBridgeTransactionList.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3

        crvView.Visible = False                         
        crvView.ReportSource = rdCrystalViewer          
        crvView.DataBind()                              

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\WM_StdRpt_WeighBridgeTransactionList.pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/WM_StdRpt_WeighBridgeTransactionList.pdf"">")
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


        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("ParamCompanyName")
        paramField2 = paramFields.Item("ParamLocation")
        paramField3 = paramFields.Item("ParamUserName")
        paramField4 = paramFields.Item("ParamDecimal")
        paramField5 = paramFields.Item("ParamTicketNo")
        paramField6 = paramFields.Item("ParamRptName")
        paramField7 = paramFields.Item("ParamStatus")
        paramField8 = paramFields.Item("ParamSuppBillParty")
        paramField9 = paramFields.Item("ParamDocRefNo")
        paramField10 = paramFields.Item("ParamDeliveryNoteNo")
        paramField11 = paramFields.Item("ParamPL3No")
        paramField12 = paramFields.Item("ParamRptID")
        paramField13 = paramFields.Item("ParamAccMth")
        paramField14 = paramFields.Item("ParamAccYear")
        paramField15 = paramFields.Item("ParamTransporter")
        paramField16 = paramFields.Item("ParamVehicle")
        paramField17 = paramFields.Item("ParamDriverName")
        paramField18 = paramFields.Item("ParamDriverICNo")
        paramField19 = paramFields.Item("ParamPlantingYear")
        paramField20 = paramFields.Item("ParamBlock")
        paramField21 = paramFields.Item("ParamDateIn")
        paramField22 = paramFields.Item("ParamInHour")
        paramField23 = paramFields.Item("ParamInMinute")
        paramField24 = paramFields.Item("ParamInAMPM")
        paramField25 = paramFields.Item("ParamDateOut")
        paramField26 = paramFields.Item("ParamOutHour")
        paramField27 = paramFields.Item("ParamOutAMPM")
        paramField28 = paramFields.Item("ParamOutMinute")
        paramField29 = paramFields.Item("ParamDateRcv")
        paramField30 = paramFields.Item("ParamDateInTo")
        paramField31 = paramFields.Item("ParamInHourTo")
        paramField32 = paramFields.Item("ParamInMinuteTo")
        paramField33 = paramFields.Item("ParamInAMPMTo")
        paramField34 = paramFields.Item("ParamDateOutTo")
        paramField35 = paramFields.Item("ParamOutHourTo")
        paramField36 = paramFields.Item("ParamOutMinuteTo")
        paramField37 = paramFields.Item("ParamOutAMPMTo")
        paramField38 = paramFields.Item("ParamDateRcvTo")
        paramField39 = paramFields.Item("ParamTransactionType")
        paramField40 = paramFields.Item("ParamProduct")
        paramField41 = paramFields.Item("LblLocation")
        paramField42 = paramFields.Item("LblVehicle")
        paramField43 = paramFields.Item("LblBillParty")
        paramField44 = paramFields.Item("lblBlockTag")

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

        ParamDiscreteValue1.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue2.Value = Session("SS_LOC")
        ParamDiscreteValue3.Value = Session("SS_USERNAME")
        ParamDiscreteValue4.Value = Request.QueryString("Decimal")
        ParamDiscreteValue5.Value = Request.QueryString("TicketNo")
        ParamDiscreteValue6.Value = Ucase(Request.QueryString("RptName"))
        ParamDiscreteValue7.Value = Request.QueryString("Status")
        ParamDiscreteValue8.Value = Request.QueryString("SuppBillParty")
        ParamDiscreteValue9.Value = Request.QueryString("DocRefNo")
        ParamDiscreteValue10.Value = Request.QueryString("DeliveryNoteNo")
        ParamDiscreteValue11.Value = Request.QueryString("PL3No")
        ParamDiscreteValue12.Value = Request.QueryString("RptID")
        ParamDiscreteValue13.Value = Request.QueryString("DDLAccMth")
        ParamDiscreteValue14.Value = Request.QueryString("DDLAccYr")
        ParamDiscreteValue15.Value = Request.QueryString("Transporter")
        ParamDiscreteValue16.Value = Request.QueryString("Vehicle")
        ParamDiscreteValue17.Value = Request.QueryString("DriverName")
        ParamDiscreteValue18.Value = Request.QueryString("DriverICNo")
        ParamDiscreteValue19.Value = Request.QueryString("PlantingYear")
        ParamDiscreteValue20.Value = Request.QueryString("Block")
        ParamDiscreteValue21.Value = Request.QueryString("DateInFrom")
        If Not Request.QueryString("InHour") = "" And Not Request.QueryString("InMinute") = "" Then
            ParamDiscreteValue22.Value = Request.QueryString("InHour") & ":"
            ParamDiscreteValue23.Value = Request.QueryString("InMinute")
            ParamDiscreteValue24.Value = Request.QueryString("InAMPM")
        Else
            ParamDiscreteValue22.Value = ""
            ParamDiscreteValue23.Value = ""
            ParamDiscreteValue24.Value = ""
        End If
        ParamDiscreteValue25.Value = Request.QueryString("DateOutFrom")
        If Not Request.QueryString("OutHour") = "" And Not Request.QueryString("OutMinute") = "" Then
            ParamDiscreteValue26.Value = Request.QueryString("OutHour") & ":"
            ParamDiscreteValue27.Value = Request.QueryString("OutAMPM")
            ParamDiscreteValue28.Value = Request.QueryString("OutMinute")
        Else
            ParamDiscreteValue26.Value = ""
            ParamDiscreteValue27.Value = ""
            ParamDiscreteValue28.Value = ""
        End If
        ParamDiscreteValue29.Value = Request.QueryString("DateRcv")
        ParamDiscreteValue30.Value = Request.QueryString("DateInTo")
        If Not Request.QueryString("InHourTo") = "" And Not Request.QueryString("InMinuteTo") = "" Then
            ParamDiscreteValue31.Value = Request.QueryString("InHourTo") & ":"
            ParamDiscreteValue32.Value = Request.QueryString("InMinuteTo")
            ParamDiscreteValue33.Value = Request.QueryString("InAMPMTo")
        Else
            ParamDiscreteValue31.Value = ""
            ParamDiscreteValue32.Value = ""
            ParamDiscreteValue33.Value = ""
        End If
        ParamDiscreteValue34.Value = Request.QueryString("DateOutTo")
        If Not Request.QueryString("OutHourTo") = "" And Not Request.QueryString("OutMinuteTo") = "" Then
            ParamDiscreteValue35.Value = Request.QueryString("OutHourTo") & ":"
            ParamDiscreteValue36.Value = Request.QueryString("OutMinuteTo")
            ParamDiscreteValue37.Value = Request.QueryString("OutAMPMTo")
        Else
            ParamDiscreteValue35.Value = ""
            ParamDiscreteValue36.Value = ""
            ParamDiscreteValue37.Value = ""
        End If
        ParamDiscreteValue38.Value = Request.QueryString("DateRcvTo")
        ParamDiscreteValue39.Value = Request.QueryString("TransactionType")
        ParamDiscreteValue40.Value = Request.QueryString("Product")
        ParamDiscreteValue41.Value = Request.QueryString("lblLocation")
        ParamDiscreteValue42.Value = Request.QueryString("lblVehicle")
        ParamDiscreteValue43.Value = Request.QueryString("lblBillParty")
        ParamDiscreteValue44.Value = Request.QueryString("lblBlockTag")

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


        crvView.ParameterFieldInfo = paramFields
    End Sub
End Class
