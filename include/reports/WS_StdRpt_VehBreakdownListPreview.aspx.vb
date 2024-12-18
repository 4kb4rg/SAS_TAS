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

Public Class WS_StdRpt_VehBreakdownList_Preview : Inherits Page
    Dim objWS As New agri.WS.clsReport()
    Dim objWSTrx As New agri.WS.clsTrx()

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String

    Dim intDecimal As Integer
    Dim strUserLoc As String
    Dim tempLoc As String
    Dim strTypeOfVeh As String

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
        Session("SS_lblBillPartyCode") = Request.QueryString("lblBillPartyCode")
        Session("SS_lblTypeOfVeh") = Request.QueryString("lblTypeOfVeh")
        Session("SS_lblVehRegNo") = Request.QueryString("lblVehRegNo")
        Session("SS_lblVehExpCode") = Request.QueryString("lblVehExpCode")
        Session("SS_lblWorkCode") = Request.QueryString("lblWorkCode")
        Session("SS_lblServTypeCode") = Request.QueryString("lblServTypeCode")
        Session("SS_lblLocation") = Request.QueryString("lblLocation")
        Session("SS_lblVeh") = Request.QueryString("lblVeh")

        Session("SS_Decimal") = Request.QueryString("Decimal")
        Session("SS_RptID") = Request.QueryString("RptID")
        Session("SS_RptName") = Request.QueryString("RptName")
        Session("SS_AccMonth") = Request.QueryString("DDLAccMth")
        Session("SS_AccYear") = Request.QueryString("DDLAccYr")

        strAccMonth = Request.QueryString("DDLAccMth")
        strAccYear = Request.QueryString("DDLAccYr")
        Session("SS_JobIDFrom") = Request.QueryString("JobIDFrom")
        Session("SS_JobIDTo") = Request.QueryString("JobIDTo")
        Session("SS_JobCreateDateFrom") = Request.QueryString("JobCreateDateFrom")
        Session("SS_JobCreateDateTo") = Request.QueryString("JobCreateDateTo")
        Session("SS_DateInFrom") = Request.QueryString("DateInFrom")
        Session("SS_DateInTo") = Request.QueryString("DateInTo")
        Session("SS_BillParty") = Request.QueryString("BillParty")
        Session("SS_VehRegNo") = Request.QueryString("VehRegNo")
        Session("SS_VehExpCode") = Request.QueryString("VehExpCode")
        Session("SS_WorkCode") = Request.QueryString("WorkCode")
        Session("SS_ServType") = Request.QueryString("ServType")
        Session("SS_MechIDFrom") = Request.QueryString("MechIDFrom")
        Session("SS_MechIDTo") = Request.QueryString("MechIDTo")
        Session("SS_Status") = Request.QueryString("Status")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            BindReport()
        End If
    End Sub

    Sub BindReport()

        Dim objRptDsVehBrDown As New DataSet()
        Dim objRptDsMechHr As New DataSet()
        Dim objMapPath As String

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOpCdVehBreakdown_GET As String = "WS_STDRPT_VEHICLE_BREAKDOWN_GET"
        Dim strOpCdVehBreakdown_MechHour_GET As String = "WS_STDRPT_VEHICLE_BREAKDOWN_MECHHOUR_GET"

        Dim strParam As String

        Dim SearchStr As String
        Dim MHSearchStr As String

        Dim strCoPsVehCode As String
        Dim strJobID As String
        Dim strLocCode As String

        Dim intCnt As Integer
        Dim intCntMH As Integer

        Dim tblMH As DataTable = New DataTable("tblMH")
        Dim Col1 As DataColumn = New DataColumn()
        Dim Col2 As DataColumn = New DataColumn()
        Dim Col3 As DataColumn = New DataColumn()
        Dim Col4 As DataColumn = New DataColumn()
        Dim Col5 As DataColumn = New DataColumn()

        Dim drMH As DataRow

        Col1.DataType = System.Type.GetType("System.String")
        Col1.AllowDBNull = True
        Col1.Caption = "ServTypeCode"
        Col1.ColumnName = "ServTypeCode"
        Col1.DefaultValue = ""
        tblMH.Columns.Add(Col1)

        Col2.DataType = System.Type.GetType("System.String")
        Col2.AllowDBNull = True
        Col2.Caption = "WorkCode"
        Col2.ColumnName = "WorkCode"
        Col2.DefaultValue = ""
        tblMH.Columns.Add(Col2)

        Col3.DataType = System.Type.GetType("System.String")
        Col3.AllowDBNull = True
        Col3.Caption = "EmpCode"
        Col3.ColumnName = "EmpCode"
        Col3.DefaultValue = ""
        tblMH.Columns.Add(Col3)

        Col4.DataType = System.Type.GetType("System.String")
        Col4.AllowDBNull = True
        Col4.Caption = "RepairHour"
        Col4.ColumnName = "RepairHour"
        Col4.DefaultValue = ""
        tblMH.Columns.Add(Col4)

        Col5.DataType = System.Type.GetType("System.String")
        Col5.AllowDBNull = True
        Col5.Caption = "RepairMinute"
        Col5.ColumnName = "RepairMinute"
        Col5.DefaultValue = ""
        tblMH.Columns.Add(Col5)

        If Not Request.QueryString("BillParty") = "" Then
            SearchStr = "AND JOB.BillPartyCode LIKE '" & Request.QueryString("BillParty") & "' AND "
        Else
            SearchStr = "AND JOB.BillPartyCode LIKE '%' AND "
        End If

        If Not (Request.QueryString("JobIDFrom") = "" And Request.QueryString("JobIDTo") = "") Then
            SearchStr = SearchStr & "JOB.JobID IN (SELECT SUBJOB.JobID FROM WS_JOB SUBJOB WHERE SUBJOB.JobID >= '" & Request.QueryString("JobIDFrom") & _
                         "' AND SUBJOB.JobID <= '" & Request.QueryString("JobIDTo") & "') AND "
        End If

        If Not (Request.QueryString("JobStartDateFrom") = "" And Request.QueryString("JobStartDateTo") = "") Then
            SearchStr = SearchStr & "(DateDiff(Day, '" & Session("SS_JOBSTARTDATEFROM") & "', JOB.JobStartDate) >= 0) And (DateDiff(Day, '" & Session("SS_JOBSTARTDATETO") & "', JOB.JobStartDate) <= 0) AND "
        End If

        If Not (Request.QueryString("DateInFrom") = "" And Request.QueryString("DateInTo") = "") Then
            SearchStr = SearchStr & "(DateDiff(Day, '" & Session("SS_DATEINFROM") & "', JOB.JobStartdate) >= 0) And (DateDiff(Day, '" & Session("SS_DATEINTO") & "', JOB.JobStartdate) <= 0) AND "
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

        If Not Request.QueryString("WorkCode") = "" Then
            MHSearchStr = "AND MHRLN.WorkCode LIKE '" & Request.QueryString("WorkCode") & "' AND "
        Else
            MHSearchStr = "AND MHRLN.WorkCode LIKE '%' AND "
        End If

        If Not (Request.QueryString("MechIDFrom") = "" And Request.QueryString("MechIDTo") = "") Then
            MHSearchStr = MHSearchStr & "MH.EmpCode IN (SELECT SUBMH.EmpCode FROM WS_MECHHOUR SUBMH WHERE SUBMH.EmpCode >= '" & Request.QueryString("MechIDFrom") & _
                         "' AND SUBMH.EmpCode <= '" & Request.QueryString("MechIDTo") & "') AND "
        End If

        If Not Request.QueryString("ServType") = "" Then
            MHSearchStr = MHSearchStr & "MHRLN.ServTypeCode LIKE '" & Request.QueryString("ServType") & "' AND "
        Else
            MHSearchStr = MHSearchStr & "MHRLN.ServTypeCode LIKE '%' AND "
        End If

        If Not Request.QueryString("Status") = "" Then
            If Not Request.QueryString("Status") = objWSTrx.EnumJobStatus.All Then
                SearchStr = SearchStr & "JOB.Status = '" & Request.QueryString("Status") & "' AND "
            Else
                SearchStr = SearchStr & "JOB.Status LIKE '%' AND "
            End If
        End If

        If Not SearchStr = "" Or Not MHSearchStr = "" Then
            If Right(SearchStr, 4) = "AND " Then
                SearchStr = Left(SearchStr, Len(SearchStr) - 4)
            End If

            If Right(MHSearchStr, 4) = "AND " Then
                MHSearchStr = Left(MHSearchStr, Len(MHSearchStr) - 4)
            End If
        End If

        Try
            intErrNo = objWS.mtdGetReport_VehBreakdownList(strOpCdVehBreakdown_GET, _
                                                            strCompany, _
                                                            strUserLoc, _
                                                            strUserId, _
                                                            strAccMonth, _
                                                            strAccYear, _
                                                            SearchStr, _
                                                            objRptDsVehBrDown, _
                                                            objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_VEHICLE_BREAKDOWN_REPORT&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        While intCnt < objRptDsVehBrDown.Tables(0).Rows.Count

            If Not IsDBNull(objRptDsVehBrDown.Tables(0).Rows(intCnt).Item("JobID")) Then
                strCoPsVehCode = Trim(objRptDsVehBrDown.Tables(0).Rows(intCnt).Item("CoPsVehCode"))
                strJobID = Trim(objRptDsVehBrDown.Tables(0).Rows(intCnt).Item("JobID"))
                strLocCode = Trim(objRptDsVehBrDown.Tables(0).Rows(intCnt).Item("LocCode"))

                strParam = strJobID & "|" & MHSearchStr
                Try
                    intErrNo = objWS.mtdGetVehBreakdownMechHourList(strOpCdVehBreakdown_MechHour_GET, strParam, objRptDsMechHr)
                Catch Exp As System.Exception
                    Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_VEHICLE_BREAKDOWN_MECHHOUR&errmesg=" & lblErrMessage.Text & "&redirect=")
                End Try

                For intCntMH = 0 To objRptDsMechHr.Tables(0).Rows.Count - 1

                    If intCntMH = 0 Then
                        objRptDsVehBrDown.Tables(0).Rows(intCnt).Item("ServTypeCode") = Trim(objRptDsMechHr.Tables(0).Rows(intCntMH).Item("ServTypeCode"))
                        objRptDsVehBrDown.Tables(0).Rows(intCnt).Item("WorkCode") = Trim(objRptDsMechHr.Tables(0).Rows(intCntMH).Item("WorkCode"))
                        objRptDsVehBrDown.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(objRptDsMechHr.Tables(0).Rows(intCntMH).Item("EmpCode"))
                        objRptDsVehBrDown.Tables(0).Rows(intCnt).Item("RepairHour") = Trim(objRptDsMechHr.Tables(0).Rows(intCntMH).Item("RepairHour"))
                        objRptDsVehBrDown.Tables(0).Rows(intCnt).Item("RepairMinute") = Trim(objRptDsMechHr.Tables(0).Rows(intCntMH).Item("RepairMinute"))
                    Else
                        drMH = objRptDsVehBrDown.Tables(0).NewRow
                        drMH("ServTypeCode") = Trim(objRptDsMechHr.Tables(0).Rows(intCntMH).Item("ServTypeCode"))
                        drMH("WorkCode") = Trim(objRptDsMechHr.Tables(0).Rows(intCntMH).Item("WorkCode"))
                        drMH("EmpCode") = Trim(objRptDsMechHr.Tables(0).Rows(intCntMH).Item("EmpCode"))
                        drMH("RepairHour") = Trim(objRptDsMechHr.Tables(0).Rows(intCntMH).Item("RepairHour"))
                        drMH("RepairMinute") = Trim(objRptDsMechHr.Tables(0).Rows(intCntMH).Item("RepairMinute"))

                        intCnt = intCnt + 1
                        objRptDsVehBrDown.Tables(0).Rows.InsertAt(drMH, intCnt)

                        objRptDsVehBrDown.Tables(0).Rows(intCnt).BeginEdit()
                        objRptDsVehBrDown.Tables(0).Rows(intCnt).Item("JobID") = strJobID
                        objRptDsVehBrDown.Tables(0).Rows(intCnt).Item("LocCode") = strLocCode
                        objRptDsVehBrDown.Tables(0).Rows(intCnt).Item("CoPsVehCode") = strCoPsVehCode
                        objRptDsVehBrDown.Tables(0).Rows(intCnt).EndEdit()
                    End If
                Next
                intCnt = intCnt + 1
            Else
                intCnt = intCnt + 1
            End If
        End While


        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\WS_StdRpt_VehBreakdownList.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDsVehBrDown.Tables(0))

        crvView.Visible = False                         
        crvView.ReportSource = rdCrystalViewer          
        crvView.DataBind()                              

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\WS_StdRpt_VehBreakdownList.pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
	rdCrystalViewer.Close()
        rdCrystalViewer.Dispose()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/WS_StdRpt_VehBreakdownList.pdf"">")
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
        paramField11 = paramFields.Item("ParamJobCreateDateFrom")
        paramField12 = paramFields.Item("ParamJobCreateDateTo")
        paramField13 = paramFields.Item("ParamBillParty")
        paramField14 = paramFields.Item("ParamTypeOfVeh")
        paramField15 = paramFields.Item("ParamVehRegNo")
        paramField16 = paramFields.Item("ParamVehExpCode")
        paramField17 = paramFields.Item("ParamWorkCode")
        paramField18 = paramFields.Item("ParamServType")
        paramField19 = paramFields.Item("ParamMechIDFrom")
        paramField20 = paramFields.Item("ParamMechIDTo")
        paramField21 = paramFields.Item("ParamStatus")
        paramField22 = paramFields.Item("lblBillPartyCode")
        paramField23 = paramFields.Item("lblTypeOfVeh")
        paramField24 = paramFields.Item("lblVehRegNo")
        paramField25 = paramFields.Item("lblVehExpCode")
        paramField26 = paramFields.Item("lblWorkCode")
        paramField27 = paramFields.Item("lblServTypeCode")
        paramField28 = paramFields.Item("lblLocation")
        paramField29 = paramFields.Item("ParamDateInFrom")
        paramField30 = paramFields.Item("ParamDateInTo")
        paramField31 = paramFields.Item("lblVeh")
        paramField32 = paramFields.Item("lblCompany")

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

        ParamDiscreteValue1.Value = Session("SS_LOC")
        ParamDiscreteValue2.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue3.Value = Session("SS_USERNAME")
        ParamDiscreteValue4.Value = Session("SS_DECIMAL")
        ParamDiscreteValue5.Value = Session("SS_RPTID")
        ParamDiscreteValue6.Value = Session("SS_RPTNAME")
        ParamDiscreteValue7.Value = Session("SS_ACCMONTH")
        ParamDiscreteValue8.Value = Session("SS_ACCYEAR")
        ParamDiscreteValue9.Value = Session("SS_JobIDFrom")
        ParamDiscreteValue10.Value = Session("SS_JobIDTo")
        ParamDiscreteValue11.Value = Session("SS_JobCreateDateFrom")
        ParamDiscreteValue12.Value = Session("SS_JobCreateDateTo")
        ParamDiscreteValue13.Value = Session("SS_BillParty")
        ParamDiscreteValue14.Value = Session("SS_TypeOfVeh")
        ParamDiscreteValue15.Value = Session("SS_VehRegNo")
        ParamDiscreteValue16.Value = Session("SS_VehExpCode")
        ParamDiscreteValue17.Value = Session("SS_WorkCode")
        ParamDiscreteValue18.Value = Session("SS_ServType")
        ParamDiscreteValue19.Value = Session("SS_EmpIDFrom")
        ParamDiscreteValue20.Value = Session("SS_EmpIDTo")
        ParamDiscreteValue21.Value = Session("SS_Status")
        ParamDiscreteValue22.Value = Session("SS_lblBillPartyCode")
        ParamDiscreteValue23.Value = Session("SS_lblTypeOfVeh")
        ParamDiscreteValue24.Value = Session("SS_lblVehRegNo")
        ParamDiscreteValue25.Value = Session("SS_lblVehExpCode")
        ParamDiscreteValue26.Value = Session("SS_lblWorkCode")
        ParamDiscreteValue27.Value = Session("SS_lblServTypeCode")
        ParamDiscreteValue28.Value = Session("SS_lblLocation")
        ParamDiscreteValue29.Value = Session("SS_DateInFrom")
        ParamDiscreteValue30.Value = Session("SS_DateInTo")
        ParamDiscreteValue31.Value = Session("SS_lblVeh")
        ParamDiscreteValue32.Value = Session("SS_LBLCOMPANY")

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

        crvView.ParameterFieldInfo = paramFields
    End Sub
End Class
