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


Public Class PR_StdRpt_EmpMoveList_Preview : Inherits Page
    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData as Datagrid

    Dim objPR As New agri.PR.clsReport()
    Dim objPRTrx As New agri.PR.clsTrx()
    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objHRTrx As New agri.HR.clsTrx()

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

    Dim rdCrystalViewer As ReportDocument 
    Dim crExportOptions As ExportOptions
    Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

    Dim intErrNo As Integer
    Dim dr As DataRow

    Sub Page_Load(Sender As Object, E As EventArgs)
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

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLangCode = Session("SS_LANGCODE")

        Session("SS_LBLLOCATION") = Request.QueryString("lblLocation")
        Session("SS_RptID") = Request.QueryString("RptID")
        Session("SS_RptName") = Request.QueryString("RptName")
        Session("SS_Decimal") = Request.QueryString("Decimal")
        Session("SS_PhyMonth") = Request.QueryString("PhyMth")
        Session("SS_PhyYear") = Request.QueryString("PhyYr")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            BindReport()
        End If
    End Sub

    Sub BindReport()

        Dim objRptEmpMove As New DataSet()
        Dim objRptEmpMoveChild As New DataSet()
        Dim objMapPath As String

        Dim objRptEmpMoveCountEmp As New DataSet()
        Dim objRptEmpMoveCountNewRecruitCPType As New DataSet()
        Dim objRptEmpMoveCeaseCPType As New DataSet()

        Dim objSummaryDs As New DataSet()

        Dim strOpCdEmpMovement_GET As String = "PR_STDRPT_EMPMOVEMENT_LIST"
        Dim strOpCdEmpMovement_Child_GET As String = "PR_STDRPT_EMPMOVEMENT_CHILD_GET"

        Dim strOpCdEmpMovement_CountEmp_GET As String = "PR_STDRPT_EMPMOVEMENT_COUNTEMP_GET"
        Dim strOpCdEmpMovement_CPType_CountNewRecruit_GET As String = "PR_STDRPT_EMPMOVEMENT_CPTYPE_COUNT_NEWRECRUIT_LIST_GET"
        Dim strOpCdEmpMovement_CPType_CountCease_GET As String = "PR_STDRPT_EMPMOVEMENT_CPTYPE_COUNT_CEASE_LIST_GET"

        Dim strParam As String
        Dim strParamEmpMoveChild As String
        Dim strParamApp As String
        Dim strParamCease As String
        Dim strParamNoOfEmp As String


        Dim intCntOpenBal As Integer
        Dim intCntEmpMove as integer
        Dim intCntNewRecruit As Integer
        Dim intCntCease As Integer
        Dim intCntCountNewRecruit As Integer
        Dim intCntCountCease As Integer



        Dim strEmpCode As String
        Dim strDateDiff As String


        strDateDiff = Request.QueryString("PhyMth") & " " & Request.QueryString("PhyYr") '"30 " & 
        strParam = objHRTrx.EnumEmpStatus.Active & "|" & strUserLoc & "|" & "AND DateDiff(Month, '" & strDateDiff & "',EMPY.LastCPDateFrom ) = 0" & "|" & _
                   "AND DateDiff(Month, '" & strDateDiff & "',EMPY.AppJoinDate) = 0"
        Try
            intErrNo = objPR.mtdGetReport_EmpMovementList(strOpCdEmpMovement_GET, _
                                                          strCompany, _
                                                          strLocation, _
                                                          strUserId, _
                                                          strAccMonth, _
                                                          strAccYear, _
                                                          strParam, _
                                                          objRptEmpMove, _
                                                          objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_PR_EMPMOVE_REPORT&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCntEmpMove = 0 To objRptEmpMove.Tables(0).Rows.Count - 1
            strEmpCode = Trim(objRptEmpMove.Tables(0).Rows(intCntEmpMove).Item("EmpCode"))

            strParamEmpMoveChild = objHRTrx.EnumRelationship.Child & "|" & strEmpCode & "|"
            Try
                intErrNo = objPR.mtdGetReport_EmpMovement_Child_List(strOpCdEmpMovement_Child_GET, _
                                                                    strCompany, _
                                                                    strLocation, _
                                                                    strUserId, _
                                                                    strAccMonth, _
                                                                    strAccYear, _
                                                                    strParamEmpMoveChild, _
                                                                    objRptEmpMoveChild, _
                                                                    objMapPath)
            Catch Exp As System.Exception
                Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_PR_EMPMOVE_CHILD_REPORT&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try

            If objRptEmpMoveChild.Tables(0).Rows.Count > 0 Then
                objRptEmpMove.Tables(0).Rows(intCntEmpMove).Item("NoOfChild") = objRptEmpMoveChild.Tables(0).Rows(0).Item("NoOfChild")
            End If


            If Not Trim(objRptEmpMove.Tables(0).Rows(intCntEmpMove).Item("CPType")) = objHRSetup.EnumCPType.Appointment Then
                intCntOpenBal += 1
            End If
        Next intCntEmpMove

        Dim tblSummary As DataTable = New DataTable("tblSummary")
        Dim Col1 As DataColumn = New DataColumn()
        Dim Col2 As DataColumn = New DataColumn()
        Dim Col3 As DataColumn = New DataColumn()

        Col1.DataType = System.Type.GetType("System.String")
        Col1.AllowDBNull = True
        Col1.Caption = "Caption"
        Col1.ColumnName = "Caption"
        Col1.DefaultValue = ""
        tblSummary.Columns.Add(Col1)

        Col2.DataType = System.Type.GetType("System.String")
        Col2.AllowDBNull = True
        Col2.Caption = "Value"
        Col2.ColumnName = "Value"
        Col2.DefaultValue = ""
        tblSummary.Columns.Add(Col2)

        Col3.DataType = System.Type.GetType("System.String")
        Col3.AllowDBNull = True
        Col3.Caption = "Type"
        Col3.ColumnName = "Type"
        Col3.DefaultValue = ""
        tblSummary.Columns.Add(Col3)


        dr = tblSummary.NewRow()
        dr("Caption") = "Last Employee Number"
        If objRptEmpMove.Tables(0).Rows.Count > 0 Then
            dr("Value") = Trim(objRptEmpMove.Tables(0).Rows(objRptEmpMove.Tables(0).Rows.Count - 1).Item("EmpCode")) & " " & Trim(objRptEmpMove.Tables(0).Rows(objRptEmpMove.Tables(0).Rows.Count - 1).Item("EmpName"))
        Else
            dr("Value") = ""
        End If
        dr("Type") = "LEN"
        tblSummary.Rows.Add(dr)


        dr = tblSummary.NewRow()
        dr("Caption") = "Opening Balance"
        dr("Value") = intCntOpenBal 
        dr("Type") = "NOE"
        tblSummary.Rows.Add(dr)

        strParamApp = strUserLoc & "|" & objHRSetup.EnumCPType.Appointment & "|" & objHRSetup.EnumCPStatus.Active & "|" & objHRTrx.EnumEmpStatus.Active & "|" & _
                      "AND DateDiff(Month,'" & strDateDiff & "', CPTRX.DateFrom) = 0 AND DateDiff(Month,'" & strDateDiff & "',  EMPY.LastCPDateFrom) = 0"
        Try
            intErrNo = objPR.mtdGetReport_EmpMovement_CPType_CountNewRecruit_List(strOpCdEmpMovement_CPType_CountNewRecruit_GET, _
                                                                                strCompany, _
                                                                                strLocation, _
                                                                                strUserId, _
                                                                                strAccMonth, _
                                                                                strAccYear, _
                                                                                strParamApp, _
                                                                                objRptEmpMoveCountNewRecruitCPType, _
                                                                                objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_PR_EMPMOVE_CPTYPE_NEWRECRUIT_REPORT&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objRptEmpMoveCountNewRecruitCPType.Tables(0).Rows.Count > 0 Then
            dr = tblSummary.NewRow()
            dr("Caption") = "New Recruitment During the Period"
            dr("Value") = ""
            dr("Type") = "NEWREC"
            tblSummary.Rows.Add(dr)

            For intCntNewRecruit = 0 To objRptEmpMoveCountNewRecruitCPType.Tables(0).Rows.Count - 1
                dr = tblSummary.NewRow()
                dr("Caption") = Chr(9) & Trim(objRptEmpMoveCountNewRecruitCPType.Tables(0).Rows(intCntNewRecruit).Item("NewRecruitCPCode")) & " " & Trim(objRptEmpMoveCountNewRecruitCPType.Tables(0).Rows(intCntNewRecruit).Item("NewRecruitCPName"))
                dr("Value") = Chr(9) & Trim(objRptEmpMoveCountNewRecruitCPType.Tables(0).Rows(intCntNewRecruit).Item("CountNewRecruit"))
                dr("Type") = "NEWLIST"
                tblSummary.Rows.Add(dr)

                intCntCountNewRecruit = Trim(objRptEmpMoveCountNewRecruitCPType.Tables(0).Rows(intCntNewRecruit).Item("CountNewRecruit")) + intCntCountNewRecruit
            Next intCntNewRecruit
        End If

        strParamCease = strUserLoc & "|" & objHRSetup.EnumCPType.Cease & "|" & objHRSetup.EnumCPStatus.Active & "|" & objHRTrx.EnumEmpStatus.Active & "|" & _
                      "AND DateDiff(Month,'" & strDateDiff & "', CPTRX.DateFrom) = 0 AND DateDiff(Month,'" & strDateDiff & "',  EMPY.LastCPDateFrom) = 0"
        Try
            intErrNo = objPR.mtdGetReport_EmpMovement_CPType_CountCease_List(strOpCdEmpMovement_CPType_CountCease_GET, _
                                                                            strCompany, _
                                                                            strLocation, _
                                                                            strUserId, _
                                                                            strAccMonth, _
                                                                            strAccYear, _
                                                                            strParamCease, _
                                                                            objRptEmpMoveCeaseCPType, _
                                                                            objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_PR_EMPMOVE_CPTYPE_CEASE_REPORT&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objRptEmpMoveCeaseCPType.Tables(0).Rows.Count > 0 Then
            dr = tblSummary.NewRow()
            dr("Caption") = "Cessation"
            dr("Value") = ""
            dr("Type") = "CEASE"
            tblSummary.Rows.Add(dr)

            For intCntCease = 0 To objRptEmpMoveCeaseCPType.Tables(0).Rows.Count - 1
                dr = tblSummary.NewRow()
                dr("Caption") = Chr(9) & Trim(objRptEmpMoveCeaseCPType.Tables(0).Rows(intCntCease).Item("CeaseCPCode")) & " " & Trim(objRptEmpMoveCeaseCPType.Tables(0).Rows(intCntCease).Item("CeaseCPName"))
                dr("Value") = Chr(9) & Trim(objRptEmpMoveCeaseCPType.Tables(0).Rows(intCntCease).Item("CountCease"))
                dr("Type") = "CEASELIST"
                tblSummary.Rows.Add(dr)
                intCntCountCease = Trim(objRptEmpMoveCeaseCPType.Tables(0).Rows(intCntCease).Item("CountCease")) + intCntCountCease
            Next intCntCease
        End If

        dr = tblSummary.NewRow()
        dr("Caption") = "Closing Balance"
        If Not dr("Value") = "0" Then
            dr("Value") = "0"
            dr("Value") = intCntOpenBal + intCntCountNewRecruit - intCntCountCease 
        End If

        dr("Type") = "CB"
        tblSummary.Rows.Add(dr)

        objSummaryDs.Tables.Add(tblSummary)


        rdCrystalViewer = New ReportDocument()

        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\PR_StdRpt_EmpMoveList.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptEmpMove.Tables(0))

        Dim subDoc As ReportDocument = rdCrystalViewer.OpenSubreport("PR_StdRpt_EmpMoveList_Summary")
        subDoc.SetDataSource(objSummaryDs.Tables(0))

        crvView.Visible = False                          
        crvView.ReportSource = rdCrystalViewer          
        crvView.DataBind()                              

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\PR_StdRpt_EmpMoveList.pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/PR_StdRpt_EmpMoveList.pdf"">")
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

        ParamDiscreteValue1.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue2.Value = Session("SS_LOC")
        ParamDiscreteValue3.Value = Session("SS_USERNAME")
        ParamDiscreteValue4.Value = Session("SS_PRACCMONTH")
        ParamDiscreteValue5.Value = Session("SS_PRACCYEAR")
        ParamDiscreteValue6.Value = Session("SS_RPTID")
        ParamDiscreteValue7.Value = Session("SS_RPTNAME")
        ParamDiscreteValue8.Value = Session("SS_LBLLOCATION")
        ParamDiscreteValue9.Value = Session("SS_Decimal")
        ParamDiscreteValue10.Value = Session("SS_PhyMonth")
        ParamDiscreteValue11.Value = Session("SS_PhyYear")

        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        ParamFieldDef1 = ParamFieldDefs.Item("ParamCompanyName")
        ParamFieldDef2 = ParamFieldDefs.Item("ParamLocation")
        ParamFieldDef3 = ParamFieldDefs.Item("ParamUserName")
        ParamFieldDef4 = ParamFieldDefs.Item("ParamAccMonth")
        ParamFieldDef5 = ParamFieldDefs.Item("ParamAccYear")
        ParamFieldDef6 = ParamFieldDefs.Item("ParamRptID")
        ParamFieldDef7 = ParamFieldDefs.Item("ParamRptName")
        ParamFieldDef8 = ParamFieldDefs.Item("lblLocation")
        ParamFieldDef9 = ParamFieldDefs.Item("ParamDecimal")
        ParamFieldDef10 = ParamFieldDefs.Item("ParamPhyMonth")
        ParamFieldDef11 = ParamFieldDefs.Item("ParamPhyYear")

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

    End Sub

End Class
