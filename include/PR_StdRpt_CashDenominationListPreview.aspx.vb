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

Public Class PR_StdRpt_CashDenominationListPreview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objHRTrx As New agri.HR.clsTrx()
    Dim objPRRpt As New agri.PR.clsReport()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strLangCode As String
    Dim strUserLoc As String
    Dim tempLoc As String

    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strDecimal As String
    Dim strEmpIDFrom As String
    Dim strEmpIDTo As String
    Dim strGangCode As String
    Dim strStatus As String
    
    Dim strDN1 As String
    Dim strDN2 As String
    Dim strDN3 As String
    Dim strDN4 As String
    Dim strDN5 As String
    Dim strDN6 As String
    Dim strDN7 As String
    Dim strDN8 As String
    Dim strDN9 As String
    Dim strDN10 As String
    Dim strDN11 As String
    Dim strDN12 As String

    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        crvView.Visible = False
        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strLangCode = Session("SS_LANGCODE")
        
        strAccMonth = Trim(Request.QueryString("strAccMonth"))
        strAccYear = Trim(Request.QueryString("strAccYear"))
        strDecimal = Trim(Request.QueryString("strDecimal"))
        strEmpIDFrom = Trim(Request.QueryString("strEmpIDFrom"))
        strEmpIDTo = Trim(Request.QueryString("strEmpIDTo"))
        strGangCode = Trim(Request.QueryString("strGangCode"))
        strStatus = Trim(Request.QueryString("strStatus"))
        strDN1 = Trim(Request.QueryString("strDN1"))
        strDN2 = Trim(Request.QueryString("strDN2"))
        strDN3 = Trim(Request.QueryString("strDN3"))
        strDN4 = Trim(Request.QueryString("strDN4"))
        strDN5 = Trim(Request.QueryString("strDN5"))
        strDN6 = Trim(Request.QueryString("strDN6"))
        strDN7 = Trim(Request.QueryString("strDN7"))
        strDN8 = Trim(Request.QueryString("strDN8"))
        strDN9 = Trim(Request.QueryString("strDN9"))
        strDN10 = Trim(Request.QueryString("strDN10"))
        strDN11 = Trim(Request.QueryString("strDN11"))
        strDN12 = Trim(Request.QueryString("strDN12"))
        
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
        
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            BindReport()
        End If
    End Sub

    Function GetDec(strVal)
        If Trim(strVal) = "" Then
            Return 0.0
        Else
            Return CDbl(strVal)
        End If
    End Function

    Sub BindReport()

        Dim objRptDs As New DataSet()
        Dim objMapPath As String

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOpCd As String = "PR_STDRPT_CASH_DENOMINATION_LIST_GET_SP"
        Dim strFileName As String = "PR_StdRpt_CashDenominationList"

        Dim strParam As String
        Dim strSearch As String
        Dim I As Long

        strSearch = ""
        If strEmpIDFrom <> "" Then
            strSearch = strSearch & "AND EMP.EmpCode >= '" & strEmpIDFrom & "' "
        End If
        If strEmpIDTo <> "" Then
	    
	    strSearch = strSearch & "AND EMP.EmpCode <= '" & strEmpIDTo & "' "
        End If
        If strGangCode <> "" Then
            strSearch = strSearch & "AND (EMP.EmpCode in (" & _
                                    "SELECT ln.GangMember FROM HR_GANG G, HR_GANGLN ln WHERE G.GangCode = ln.GangCode AND G.Status = '" & objHRSetup.EnumGangStatus.Active & "' AND G.GangCode LIKE '" & strGangCode & "') " & Chr(13) & _
                                    "OR EMP.EmpCode in (" & _
                                    "SELECT G.GangLeader FROM HR_GANG G WHERE G.GangLeader <> '' AND G.Status = '" & objHRSetup.EnumGangStatus.Active & "' AND G.GangCode LIKE '" & strGangCode & "')) " & Chr(13)
        End If

        Select Case strStatus
            Case objHRTrx.EnumEmpStatus.Active
                strSearch = strSearch & " AND TerminateDate = ''"
            Case objHRTrx.EnumEmpStatus.Terminated
                strSearch = strSearch & " AND TerminateDate <> '' AND DateDiff(Day, TerminateDate, GetDate()) > 0"
        End Select

        strStatus = objHRTrx.mtdGetEmpStatus(strStatus)
        
        strSearch = Replace(strSearch, "'", "''")
        
        strParam = Replace(strUserLoc, "'", "''") & "|" & strAccMonth & "|" & strAccYear & "|" & objHRTrx.EnumEmpStatus.Active & "|" & strSearch & "|" & _
                   GetDec(strDN1) & "|" & GetDec(strDN2) & "|" & GetDec(strDN3) & "|" & GetDec(strDN4) & "|" & _
                   GetDec(strDN5) & "|" & GetDec(strDN6) & "|" & GetDec(strDN7) & "|" & GetDec(strDN8) & "|" & _
                   GetDec(strDN9) & "|" & GetDec(strDN10) & "|" & GetDec(strDN11) & "|" & GetDec(strDN12)
        Try
            intErrNo = objPRRpt.mtdGetReport_CashDenominationList(strOpCd, _
                                                strParam, _
                                                objRptDs, _
                                                objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PR_STDRPT_CASH_DENOMINATION_LIST_GET_SP&errmesg=" & lblErrMessage.Text & "&redirect=")
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

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("ParamCompanyName")
        paramField2 = paramFields.Item("ParamLocation")
        paramField3 = paramFields.Item("ParamUserName")
        paramField4 = paramFields.Item("ParamRptID")
        paramField5 = paramFields.Item("ParamRptName")
        paramField6 = paramFields.Item("ParamAccMonth")
        paramField7 = paramFields.Item("ParamAccYear")
        paramField8 = paramFields.Item("ParamEmpIDFrom")
        paramField9 = paramFields.Item("ParamEmpIDTo")
        paramField10 = paramFields.Item("ParamStatus")
        paramField11 = paramFields.Item("ParamDN1")
        paramField12 = paramFields.Item("ParamDN2")
        paramField13 = paramFields.Item("ParamDN3")
        paramField14 = paramFields.Item("ParamDN4")
        paramField15 = paramFields.Item("ParamDN5")
        paramField16 = paramFields.Item("ParamDN6")
        paramField17 = paramFields.Item("ParamDN7")
        paramField18 = paramFields.Item("ParamDN8")
        paramField19 = paramFields.Item("ParamDN9")
        paramField20 = paramFields.Item("ParamDN10")
        paramField21 = paramFields.Item("ParamDN11")
        paramField22 = paramFields.Item("ParamDN12")
        paramField23 = paramFields.Item("ParamDecimal")
        paramField24 = paramFields.Item("ParamlblLocation")
        paramField25 = paramFields.Item("SrchGangCode")

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

        ParamDiscreteValue1.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue2.Value = Session("SS_LOC")
        ParamDiscreteValue3.Value = Session("SS_USERNAME")
        ParamDiscreteValue4.Value = Request.QueryString("RptID")
        ParamDiscreteValue5.Value = Request.QueryString("RptName")
        ParamDiscreteValue6.Value = strAccMonth
        ParamDiscreteValue7.Value = strAccYear
        ParamDiscreteValue8.Value = strEmpIDFrom
        ParamDiscreteValue9.Value = strEmpIDTo
        ParamDiscreteValue10.Value = strStatus
        ParamDiscreteValue11.Value = strDN1
        ParamDiscreteValue12.Value = strDN2
        ParamDiscreteValue13.Value = strDN3
        ParamDiscreteValue14.Value = strDN4
        ParamDiscreteValue15.Value = strDN5
        ParamDiscreteValue16.Value = strDN6
        ParamDiscreteValue17.Value = strDN7
        ParamDiscreteValue18.Value = strDN8
        ParamDiscreteValue19.Value = strDN9
        ParamDiscreteValue20.Value = strDN10
        ParamDiscreteValue21.Value = strDN11
        ParamDiscreteValue22.Value = strDN12
        ParamDiscreteValue23.Value = strDecimal
        ParamDiscreteValue24.Value = Request.QueryString("lblLocation")
        ParamDiscreteValue25.Value = strGangCode

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
        
        crvView.ParameterFieldInfo = paramFields
    End Sub
End Class
