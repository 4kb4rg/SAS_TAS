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

Public Class WS_StdRpt_CompletedJobList_Preview : Inherits Page
    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objWS As New agri.WS.clsReport()
    Dim objWSTrx As New agri.WS.clsTrx()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String

    Dim tempLoc As String
    Dim strTypeOfVeh As String
    Dim strUserLoc As String
    Dim intDecimal As Integer

    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer
    Dim dr As DataRow

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
        strTypeOfVeh = Request.QueryString("TypeOfVeh")

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        strLangCode = Session("SS_LANGCODE")

        Session("SS_lblCompany") = Request.QueryString("lblCompany")
        Session("SS_lblVehicle") = Request.QueryString("lblVehicle")
        Session("SS_lblTypeOfVeh") = Request.QueryString("lblTypeOfVeh")
        Session("SS_lblVehRegNo") = Request.QueryString("lblVehRegNo")
        Session("SS_lblBillPartyCode") = Request.QueryString("lblBillPartyCode")
        Session("SS_lblServTypeCode") = Request.QueryString("lblServTypeCode")
        Session("SS_lblLocation") = Request.QueryString("lblLocation")

        Session("SS_Decimal") = Request.QueryString("Decimal")
        Session("SS_RptID") = Request.QueryString("RptID")
        Session("SS_RptName") = Request.QueryString("RptName")
        Session("SS_JobIDFrom") = Request.QueryString("JobIDFrom")
        Session("SS_JobIDTo") = Request.QueryString("JobIDTo")
        Session("SS_VehRegNo") = Request.QueryString("VehRegNo")
        Session("SS_BillParty") = Request.QueryString("BillParty")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            BindReport()
        End If
    End Sub

    Sub BindReport()

        Dim objRpDsCompJob As New DataSet()
        Dim objRptDsStkItem As New DataSet()
        Dim objRptDsDCItem As New DataSet()
        Dim objRptDsLabour As New DataSet()
        Dim objMapPath As String
        Dim objRptDsPrint As New DataSet()

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOpCdCompletedJob_GET As String = "WS_STDRPT_COMPLETEDJOB_GET"
        Dim strOpCdCompletedJob_Item_GET As String = "WS_STDRPT_COMPLETEDJOB_ITEM_GET"
        Dim strOpCdCompletedJob_Labour_GET As String = "WS_STDRPT_COMPLETEDJOB_LABOUR_GET"
        Dim strOpCdCompletedJob_Printdate_UPD As String = "WS_STDRPT_COMPLETEDJOB_PRINTDATE_UPD"

        Dim strParam As String
        Dim strParamStkItem As String
        Dim strParamDCItem As String
        Dim strParamLabour As String
        Dim strParamPrintDate As String

        Dim intCntJob As Integer
        Dim intCntStkItem As Integer
        Dim intCntDCItem As Integer
        Dim intCntLabour As Integer

        Dim SearchStr As String
        Dim strJobID As String
        Dim strLocCode As String
        Dim strBillParty As String

        Dim decDCItemAmt As Decimal
        Dim decDCItemBilledAmt As Decimal
        Dim decLabourAmt As Decimal
        Dim decLabourBilledAmt As Decimal

        Dim intMin As Integer
        Dim intHour As Integer
        Dim decTotalLabourPerHr As Decimal

        Dim drItem As DataRow
        Dim drLabour As DataRow

        If Not Request.QueryString("BillParty") = "" Then
            SearchStr = "AND JOB.BillPartyCode LIKE '" & Request.QueryString("BillParty") & "' AND "
        Else
            SearchStr = "AND JOB.BillPartyCode LIKE '%' AND "
        End If

        If Not (Request.QueryString("JobIDFrom") = "" And Request.QueryString("JobIDTo") = "") Then
            SearchStr = SearchStr & "JOB.JobID IN (SELECT SUBJOB.JobID FROM WS_JOB SUBJOB WHERE SUBJOB.JobID >= '" & Request.QueryString("JobIDFrom") & _
                         "' AND SUBJOB.JobID <= '" & Request.QueryString("JobIDTo") & "') AND "
        End If

        If strTypeOfVeh = "A" Then
            Session("SS_TypeOfVeh") = "All"
        ElseIf strTypeOfVeh = "C" Then
            SearchStr = SearchStr & "JOB.CoVehCode <> '' AND JOB.CoLocCode <> '' AND "
            Session("SS_TypeOfVeh") = Session("SS_lblCompany")
        ElseIf strTypeOfVeh = "P" Then
            SearchStr = SearchStr & "JOB.PsVehRegNo <> '' AND JOB.PsLocCode <> '' AND "
            Session("SS_TypeOfVeh") = "Personal"
        End If

        If Request.QueryString("PrintStatus") = "A" Then
            Session("SS_PrintStatus") = "All"
            SearchStr = SearchStr & "JOB.PrintDate LIKE '%' AND "
        ElseIf Request.QueryString("PrintStatus") = "P" Then
            Session("SS_PrintStatus") = "Printed"
            SearchStr = SearchStr & "JOB.PrintDate <> '01-Jan-1900' AND "
        ElseIf Request.QueryString("PrintStatus") = "N" Then
            Session("SS_PrintStatus") = "Not Printed"
            SearchStr = SearchStr & "JOB.PrintDate = '01-Jan-1900' AND "
        End If


        If Not SearchStr = "" Then
            If Right(SearchStr, 4) = "AND " Then
                SearchStr = Left(SearchStr, Len(SearchStr) - 4)
            End If
        End If

        strParam = strUserLoc & "|" & objWSTrx.EnumJobStatus.Closed & "','" & objWSTrx.EnumJobStatus.DebitNote & "','" & objWSTrx.EnumJobStatus.Posted & "|" & SearchStr
        Try
            intErrNo = objWS.mtdGetReport_CompletedJobList(strOpCdCompletedJob_GET, _
                                                           strCompany, _
                                                           strUserId, _
                                                           strAccMonth, _
                                                           strAccYear, _
                                                           strParam, _
                                                           objRpDsCompJob, _
                                                           objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_COMPLETEDJOB_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        While intCntJob < objRpDsCompJob.Tables(0).Rows.Count
            If Not IsDBNull(objRpDsCompJob.Tables(0).Rows(intCntJob).Item("JobID")) Then
                strJobID = Trim(objRpDsCompJob.Tables(0).Rows(intCntJob).Item("JobID"))
                strLocCode = Trim(objRpDsCompJob.Tables(0).Rows(intCntJob).Item("LocCode"))
                strBillParty = Trim(objRpDsCompJob.Tables(0).Rows(intCntJob).Item("BillPartyCode"))

                drItem = objRpDsCompJob.Tables(0).NewRow
                drItem("Code") = "-STOCK ITEM-"
                drItem("Type") = "SI"
                intCntJob = intCntJob + 1
                objRpDsCompJob.Tables(0).Rows.InsertAt(drItem, intCntJob)

                objRpDsCompJob.Tables(0).Rows(intCntJob).BeginEdit()
                objRpDsCompJob.Tables(0).Rows(intCntJob).Item("JobID") = strJobID
                objRpDsCompJob.Tables(0).Rows(intCntJob).Item("LocCode") = strLocCode
                objRpDsCompJob.Tables(0).Rows(intCntJob).EndEdit()

                strParamStkItem = strJobID & "|" & objWSTrx.EnumInventoryItemType.WorkshopItem & "|"
                Try
                    intErrNo = objWS.mtdGetCompletedJob(strOpCdCompletedJob_Item_GET, strParamStkItem, objRptDsStkItem)
                Catch Exp As System.Exception
                    Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_COMPLETEDJOB_STKITEM&errmesg=" & lblErrMessage.Text & "&redirect=")
                End Try

                For intCntStkItem = 0 To objRptDsStkItem.Tables(0).Rows.Count - 1
                    drItem = objRpDsCompJob.Tables(0).NewRow
                    drItem("RecNo") = intCntStkItem + 1
                    drItem("Code") = Trim(objRptDsStkItem.Tables(0).Rows(intCntStkItem).Item("Code"))
                    drItem("Description") = Trim(objRptDsStkItem.Tables(0).Rows(intCntStkItem).Item("Description"))
                    drItem("Qty") = objRptDsStkItem.Tables(0).Rows(intCntStkItem).Item("Qty")
                    drItem("UnitPrice") = objRptDsStkItem.Tables(0).Rows(intCntStkItem).Item("UnitPrice")
                    drItem("MechID") = objRptDsStkItem.Tables(0).Rows(intCntStkItem).Item("MechID")
                    drItem("BilledAmt") = objRptDsStkItem.Tables(0).Rows(intCntStkItem).Item("Qty") * objRptDsStkItem.Tables(0).Rows(intCntStkItem).Item("UnitPrice")
                    drItem("Type") = "SID"

                    intCntJob = intCntJob + 1
                    objRpDsCompJob.Tables(0).Rows.InsertAt(drItem, intCntJob)

                    objRpDsCompJob.Tables(0).Rows(intCntJob).BeginEdit()
                    objRpDsCompJob.Tables(0).Rows(intCntJob).Item("JobID") = strJobID
                    objRpDsCompJob.Tables(0).Rows(intCntJob).Item("LocCode") = strLocCode
                    objRpDsCompJob.Tables(0).Rows(intCntJob).Item("BillPartyCode") = strBillParty

                    If objRpDsCompJob.Tables(0).Rows(intCntJob).Item("BillPartyCode") = "" Then
                        objRpDsCompJob.Tables(0).Rows(intCntJob).Item("UnitPrice") = 0
                    End If
                    objRpDsCompJob.Tables(0).Rows(intCntJob).EndEdit()
                Next

                drItem = objRpDsCompJob.Tables(0).NewRow
                drItem("Code") = ""
                drItem("JobID") = strJobID
                drItem("LocCode") = strLocCode
                intCntJob = intCntJob + 1
                objRpDsCompJob.Tables(0).Rows.InsertAt(drItem, intCntJob)

                drItem = objRpDsCompJob.Tables(0).NewRow
                drItem("Code") = "-DIRECT CHARGE ITEM-"
                drItem("Type") = "DC"
                intCntJob = intCntJob + 1
                objRpDsCompJob.Tables(0).Rows.InsertAt(drItem, intCntJob)

                objRpDsCompJob.Tables(0).Rows(intCntJob).BeginEdit()
                objRpDsCompJob.Tables(0).Rows(intCntJob).Item("JobID") = strJobID
                objRpDsCompJob.Tables(0).Rows(intCntJob).Item("LocCode") = strLocCode
                objRpDsCompJob.Tables(0).Rows(intCntJob).EndEdit()

                strParamDCItem = strJobID & "|" & objWSTrx.EnumInventoryItemType.DirectCharge & "|"
                Try
                    intErrNo = objWS.mtdGetCompletedJob(strOpCdCompletedJob_Item_GET, strParamDCItem, objRptDsDCItem)
                Catch Exp As System.Exception
                    Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_COMPLETEDJOB_DCITEM&errmesg=" & lblErrMessage.Text & "&redirect=")
                End Try

                For intCntDCItem = 0 To objRptDsDCItem.Tables(0).Rows.Count - 1
                    drItem = objRpDsCompJob.Tables(0).NewRow
                    drItem("RecNo") = intCntDCItem + 1
                    drItem("Code") = Trim(objRptDsDCItem.Tables(0).Rows(intCntDCItem).Item("Code"))
                    drItem("Description") = Trim(objRptDsDCItem.Tables(0).Rows(intCntDCItem).Item("Description"))
                    drItem("Qty") = objRptDsDCItem.Tables(0).Rows(intCntDCItem).Item("Qty")
                    drItem("UnitPrice") = objRptDsDCItem.Tables(0).Rows(intCntDCItem).Item("UnitPrice")
                    drItem("BilledAmt") = objRptDsDCItem.Tables(0).Rows(intCntDCItem).Item("Qty") * objRptDsDCItem.Tables(0).Rows(intCntDCItem).Item("UnitPrice")
                    drItem("MechID") = objRptDsDCItem.Tables(0).Rows(intCntDCItem).Item("MechID")
                    drItem("Type") = "DCD"

                    decDCItemAmt = objRptDsDCItem.Tables(0).Rows(intCntDCItem).Item("Qty") * objRptDsDCItem.Tables(0).Rows(intCntDCItem).Item("UnitPrice")
                    decDCItemBilledAmt = decDCItemAmt + decDCItemBilledAmt

                    intCntJob = intCntJob + 1
                    objRpDsCompJob.Tables(0).Rows.InsertAt(drItem, intCntJob)

                    objRpDsCompJob.Tables(0).Rows(intCntJob).BeginEdit()
                    objRpDsCompJob.Tables(0).Rows(intCntJob).Item("JobID") = strJobID
                    objRpDsCompJob.Tables(0).Rows(intCntJob).Item("LocCode") = strLocCode
                    objRpDsCompJob.Tables(0).Rows(intCntJob).Item("BillPartyCode") = strBillParty
                    If objRpDsCompJob.Tables(0).Rows(intCntJob).Item("BillPartyCode") = "" Then
                        objRpDsCompJob.Tables(0).Rows(intCntJob).Item("UnitPrice") = 0
                        objRpDsCompJob.Tables(0).Rows(intCntJob - 1).Item("BilledAmt") = 0
                    End If
                    objRpDsCompJob.Tables(0).Rows(intCntJob).EndEdit()

                Next

                If Not IsDBNull(objRpDsCompJob.Tables(0).Rows(intCntJob).Item("BillPartyCode")) Then
                    drItem = objRpDsCompJob.Tables(0).NewRow
                    drItem("JobID") = strJobID
                    drItem("LocCode") = strLocCode
                    drItem("Code") = "TOTAL DIRECT CHARGE"
                    drItem("Type") = "TDC"
                    drItem("BilledAmt") = decDCItemBilledAmt
                    intCntJob = intCntJob + 1
                    objRpDsCompJob.Tables(0).Rows.InsertAt(drItem, intCntJob)
                    decDCItemBilledAmt = 0
                End If

                drItem = objRpDsCompJob.Tables(0).NewRow
                drItem("Code") = ""
                drItem("JobID") = strJobID
                drItem("LocCode") = strLocCode
                intCntJob = intCntJob + 1
                objRpDsCompJob.Tables(0).Rows.InsertAt(drItem, intCntJob)

                drItem = objRpDsCompJob.Tables(0).NewRow
                drItem("Code") = "-LABOUR CHARGE-"
                drItem("Type") = "LC"
                intCntJob = intCntJob + 1
                objRpDsCompJob.Tables(0).Rows.InsertAt(drItem, intCntJob)

                objRpDsCompJob.Tables(0).Rows(intCntJob).BeginEdit()
                objRpDsCompJob.Tables(0).Rows(intCntJob).Item("JobID") = strJobID
                objRpDsCompJob.Tables(0).Rows(intCntJob).Item("LocCode") = strLocCode
                objRpDsCompJob.Tables(0).Rows(intCntJob).EndEdit()


                drItem = objRpDsCompJob.Tables(0).NewRow
                drItem("Code") = Session("SS_lblServTypeCode")
                drItem("Description") = ""
                drItem("Type") = "ST"
                intCntJob = intCntJob + 1
                objRpDsCompJob.Tables(0).Rows.InsertAt(drItem, intCntJob)

                objRpDsCompJob.Tables(0).Rows(intCntJob).BeginEdit()
                objRpDsCompJob.Tables(0).Rows(intCntJob).Item("JobID") = strJobID
                objRpDsCompJob.Tables(0).Rows(intCntJob).Item("LocCode") = strLocCode
                objRpDsCompJob.Tables(0).Rows(intCntJob).EndEdit()

                strParam = strJobID & "||"
                Try
                    intErrNo = objWS.mtdGetJobItemList(strOpCdCompletedJob_Labour_GET, strParam, objRptDsLabour)
                Catch Exp As System.Exception
                    Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_COMPLETEDJOB_LABOUR&errmesg=" & lblErrMessage.Text & "&redirect=")
                End Try

                For intCntLabour = 0 To objRptDsLabour.Tables(0).Rows.Count - 1
                    If objRpDsCompJob.Tables(0).Rows(intCntJob).Item("Type") = "ST" Then
                        objRpDsCompJob.Tables(0).Rows(intCntJob).BeginEdit()
                        objRpDsCompJob.Tables(0).Rows(intCntJob).Item("Description") = Trim(objRptDsLabour.Tables(0).Rows(intCntLabour).Item("ServTypeCode")) & " (" & Trim(objRptDsLabour.Tables(0).Rows(intCntLabour).Item("ServTypeDesc")) & ")"
                        objRpDsCompJob.Tables(0).Rows(intCntJob).EndEdit()
                    End If

                    drLabour = objRpDsCompJob.Tables(0).NewRow
                    drLabour("RecNo") = intCntLabour + 1
                    drLabour("Code") = Trim(objRptDsLabour.Tables(0).Rows(intCntLabour).Item("WorkCode"))
                    drLabour("Description") = Trim(objRptDsLabour.Tables(0).Rows(intCntLabour).Item("WorkCodeDesc"))
                    drLabour("MechID") = Trim(objRptDsLabour.Tables(0).Rows(intCntLabour).Item("LabourMechID"))
                    drLabour("WorkDate") = Trim(objRptDsLabour.Tables(0).Rows(intCntLabour).Item("WorkDate"))
                    drLabour("HourSpent") = Trim(objRptDsLabour.Tables(0).Rows(intCntLabour).Item("HourSpent"))
                    drLabour("MinuteSpent") = Trim(objRptDsLabour.Tables(0).Rows(intCntLabour).Item("MinuteSpent"))
                    drLabour("ChrgRate") = Trim(objRptDsLabour.Tables(0).Rows(intCntLabour).Item("ChrgRate"))
                    drLabour("BilledAmt") = objRptDsLabour.Tables(0).Rows(intCntLabour).Item("HourSpent") * objRptDsLabour.Tables(0).Rows(intCntLabour).Item("ChrgRate") + (objRptDsLabour.Tables(0).Rows(intCntLabour).Item("MinuteSpent") / 60 * objRptDsLabour.Tables(0).Rows(intCntLabour).Item("ChrgRate"))
                    drLabour("Type") = "LCD"

                    decLabourAmt = objRptDsLabour.Tables(0).Rows(intCntLabour).Item("HourSpent") * objRptDsLabour.Tables(0).Rows(intCntLabour).Item("ChrgRate") + (objRptDsLabour.Tables(0).Rows(intCntLabour).Item("MinuteSpent") / 60 * objRptDsLabour.Tables(0).Rows(intCntLabour).Item("ChrgRate"))
                    decLabourBilledAmt = decLabourAmt + decLabourBilledAmt

                    intMin += objRptDsLabour.Tables(0).Rows(intCntLabour).Item("MinuteSpent")
                    intHour += objRptDsLabour.Tables(0).Rows(intCntLabour).Item("HourSpent")

                    intCntJob = intCntJob + 1
                    objRpDsCompJob.Tables(0).Rows.InsertAt(drLabour, intCntJob)

                    objRpDsCompJob.Tables(0).Rows(intCntJob).BeginEdit()
                    objRpDsCompJob.Tables(0).Rows(intCntJob).Item("JobID") = strJobID
                    objRpDsCompJob.Tables(0).Rows(intCntJob).Item("LocCode") = strLocCode
                    objRpDsCompJob.Tables(0).Rows(intCntJob).Item("BillPartyCode") = strBillParty
                    If objRpDsCompJob.Tables(0).Rows(intCntJob).Item("BillPartyCode") = "" Then
                        objRpDsCompJob.Tables(0).Rows(intCntJob).Item("ChrgRate") = 0
                        objRpDsCompJob.Tables(0).Rows(intCntJob).Item("BilledAmt") = 0
                    End If
                    objRpDsCompJob.Tables(0).Rows(intCntJob).EndEdit()
                Next

                decTotalLabourPerHr = FormatNumber((decLabourBilledAmt / (intHour * 60 + intMin)) * 60, intDecimal)

                If Not IsDBNull(objRpDsCompJob.Tables(0).Rows(intCntJob).Item("BillPartyCode")) Then
                    drItem = objRpDsCompJob.Tables(0).NewRow
                    drItem("JobID") = strJobID
                    drItem("LocCode") = strLocCode
                    drItem("Code") = "TOTAL LABOUR CHARGE ( @ RM " & decTotalLabourPerHr & " PER HOUR )"
                    drItem("Type") = "TLC"
                    drItem("BilledAmt") = decLabourBilledAmt
                    intCntJob = intCntJob + 1
                    objRpDsCompJob.Tables(0).Rows.InsertAt(drItem, intCntJob)
                End If
                decLabourBilledAmt = 0

            End If
            intCntJob = intCntJob + 1

            strParamPrintDate = strJobID & "|"
            Try
                intErrNo = objWS.mtdUpdCompletedJobList(strOpCdCompletedJob_Printdate_UPD, strParamPrintDate, objRptDsPrint)
            Catch Exp As System.Exception
                Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_COMPLETEDJOB_PRINTDATE_UPD&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try

        End While


        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\WS_StdRpt_CompletedJobList.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRpDsCompJob.Tables(0))

        crvView.ReportSource = rdCrystalViewer          
        crvView.DataBind()                              

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\WS_StdRpt_CompletedJobList.pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/WS_StdRpt_CompletedJobList.pdf"">")
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

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("ParamLocation")
        paramField2 = paramFields.Item("ParamCompanyName")
        paramField3 = paramFields.Item("ParamUserName")
        paramField4 = paramFields.Item("ParamDecimal")
        paramField5 = paramFields.Item("ParamRptID")
        paramField6 = paramFields.Item("ParamRptName")
        paramField7 = paramFields.Item("ParamAccMonth")
        paramField8 = paramFields.Item("ParamAccYear")
        paramField9 = paramFields.Item("ParamJobIDFrom")
        paramField10 = paramFields.Item("ParamJobIDTo")
        paramField11 = paramFields.Item("ParamBillParty")
        paramField12 = paramFields.Item("ParamTypeOfVeh")
        paramField13 = paramFields.Item("ParamVehRegNo")
        paramField14 = paramFields.Item("ParamPrintStatus")
        paramField15 = paramFields.Item("lblBillPartyCode")
        paramField16 = paramFields.Item("lblTypeOfVeh")
        paramField17 = paramFields.Item("lblVehRegNo")
        paramField18 = paramFields.Item("lblLocation")
        paramField19 = paramFields.Item("lblVehicle")
        paramField20 = paramFields.Item("lblCompany")

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

        ParamDiscreteValue1.Value = Session("SS_LOC")
        ParamDiscreteValue2.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue3.Value = Session("SS_USERNAME")
        ParamDiscreteValue4.Value = Session("SS_DECIMAL")
        ParamDiscreteValue5.Value = Session("SS_RPTID")
        ParamDiscreteValue6.Value = Session("SS_RPTNAME")
        ParamDiscreteValue7.Value = Session("SS_INACCMONTH")
        ParamDiscreteValue8.Value = Session("SS_INACCYEAR")
        ParamDiscreteValue9.Value = Session("SS_JobIDFrom")
        ParamDiscreteValue10.Value = Session("SS_JobIDTo")
        ParamDiscreteValue11.Value = Session("SS_BillParty")
        ParamDiscreteValue12.Value = Session("SS_TypeOfVeh")
        ParamDiscreteValue13.Value = Session("SS_VehRegNo")
        ParamDiscreteValue14.Value = Session("SS_PrintStatus")
        ParamDiscreteValue15.Value = Session("SS_lblBillPartyCode")
        ParamDiscreteValue16.Value = Session("SS_lblTypeOfVeh")
        ParamDiscreteValue17.Value = Session("SS_lblVehRegNo")
        ParamDiscreteValue18.Value = Session("SS_lblLocation")
        ParamDiscreteValue19.Value = Session("SS_LBLVEHICLE")
        ParamDiscreteValue20.Value = Session("SS_LBLCOMPANY")

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

        crvView.ParameterFieldInfo = paramFields
    End Sub
End Class
