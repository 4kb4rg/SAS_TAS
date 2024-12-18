Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.web
Imports System.web.UI
Imports System.web.UI.webControls
Imports System.web.UI.HtmlControls
Imports System.web.UI.Page
Imports System.Text
Imports System.IO
Imports Microsoft.VisualBasic

Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information
Imports System.Xml
Imports System.web.Services
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.web

Public Class PR_StdRpt_GajiBankPreview_Estate : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objPRRpt As New agri.PR.clsReport()
    Dim objHRReport As New agri.HR.clsReport()
    Dim objOk As New agri.GL.clsReport
    Dim ObjGet As New agri.GL.ClsTrx
	Dim objSysCfg As New agri.PWSystem.clsConfig()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
	Dim strLocName As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strUserLoc As String
    Dim tempLoc As String
    Dim intDecimal As Integer
    Dim strRptName As String
    Dim strPhyMonth As String
    Dim strPhyYear As String
    Dim strDateFrom As String
    Dim strDateTo As String
    Dim strEmpType As String

    Dim strEmpCode As String
    Dim strDivisi As String
    Dim strMandor As String
    Dim strPekerjaan As String
    Dim objFTPFolder As String

    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer
    Dim strExportToExcel As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

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

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
		strLocName = Session("SS_LOCATIONNAME")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")

        strAccMonth = Request.QueryString("AccMonth")
        strAccYear = Request.QueryString("AccYear")
        strRptName = Request.QueryString("RptName")
        strLangCode = Session("SS_LANGCODE")
        strPhyMonth = Request.QueryString("AccMonth")
        strPhyYear = Request.QueryString("AccYear")
        strDateFrom = Request.QueryString("SelDateFrom")
        strDateTo = Request.QueryString("SelDateTo")
        strEmpType = Request.QueryString("Type")

        'strExportToExcel = Trim(Request.QueryString("ExportToExcel"))

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            'BindReport()
			ExportToCSV()
        End If
    End Sub
	
	Public Sub UserMsgBox(ByVal F As Object, ByVal sMsg As String)
        Dim sb As New StringBuilder()
        Dim oFormObject As System.Web.UI.Control = Nothing
        Try
            sMsg = sMsg.Replace("'", "\'")
            sMsg = sMsg.Replace(Chr(34), "\" & Chr(34))
            sMsg = sMsg.Replace(vbCrLf, "\n")
            sMsg = "<script language='javascript'>alert('" & sMsg & "');</script>"
            sb = New StringBuilder()
            sb.Append(sMsg)
            For Each oFormObject In F.Controls
                If TypeOf oFormObject Is HtmlForm Then
                    Exit For
                End If
            Next
            oFormObject.Controls.AddAt(oFormObject.Controls.Count, New LiteralControl(sb.ToString()))
        Catch ex As Exception

        End Try
    End Sub
	
   Sub ExportToCSV()
        Dim objRptDs As New DataSet()
        Dim objDataSet As New Object()
        Dim strOpCd As String = "PR_PR_STDRPT_GAJIBANK_REPORT"
        Dim strOpCdSP As String = "PR_PR_TRX_MTHEND_GET_STATUS"

        Dim strParamName As String
        Dim strParamValue As String
		Dim objMapPath As String
		
		Dim strFtpPath As String
		Dim strXmlFileName As String = ""
        Dim strXmlPath As String = ""
        Dim x As Integer


        strParamName = "MN|YR|LOC"
        strParamValue = strAccMonth & "|" & _
                     strAccYear & "|" & _
                     strLocation

        Try
            intErrNo = ObjGet.mtdGetDataCommon(strOpCdSP, strParamName, strParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_MTHEND_GET_STATUS&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objDataSet.Tables(0).Rows.Count > 0 Then
            x = objDataSet.Tables(0).Rows(0).Item("StatusGaji")
        End If


        If x = 3 Then
            Select Case strEmpType
                Case "U"
                    strXmlFileName = "Payroll" & Trim(strLocation) & Trim(strAccYear) & Trim(strAccMonth) & ".csv"
                Case "R"
                    strXmlFileName = "Rapel" & Trim(strLocation) & Trim(strAccYear) & Trim(strAccMonth) & ".csv"
                Case "T"
                    strXmlFileName = "THR" & Trim(strLocation) & Trim(strAccYear) & Trim(strAccMonth) & ".csv"
                Case "B"
                    strXmlFileName = "BONUS" & Trim(strLocation) & Trim(strAccYear) & Trim(strAccMonth) & ".csv"
            End Select
        Else
            Select Case strEmpType
                Case "U"
                    strXmlFileName = "Payroll" & Trim(strLocation) & Trim(strAccYear) & Trim(strAccMonth) & ".csv"
                Case "R"
                    strXmlFileName = "Rapel" & Trim(strLocation) & Trim(strAccYear) & Trim(strAccMonth) & ".csv"
                Case "T"
                    strXmlFileName = "THR" & Trim(strLocation) & Trim(strAccYear) & Trim(strAccMonth) & ".csv"
                Case "B"
                    strXmlFileName = "BONUS" & Trim(strLocation) & Trim(strAccYear) & Trim(strAccMonth) & ".csv"
            End Select
        End If

        Try
            intErrNo = objSysCfg.mtdGetFtpPath(strFtpPath)
            strXmlPath = strFtpPath & strXmlFileName
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WM_GET_FTPPATH&errmesg=&redirect=")
        End Try

        strParamName = "ACCMONTH|ACCYEAR|PHYMONTH|PHYYEAR|COMPCODE|LOCCODE|DATEFROM|DATETO|DECIMAL|TY"
        strParamValue = strAccMonth & "|" & _
                        strAccYear & "|" & _
                        strAccMonth & "|" & _
                        strAccYear & "|" & _
                        strCompany & "|" & _
                        strLocation & "|" & _
                        strDateFrom & "|" & _
                        strDateTo & "|" & _
                        intDecimal & "|" & _
      strEmpType
        Try
            intErrNo = objOk.mtdGetReportDataCommon(strOpCd, strParamName, strParamValue, objRptDs, objMapPath, objFTPFolder)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PR_PR_STDRPT_GAJIKECIL_REPORT&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objRptDs.Tables(0).Rows.Count > 0 Then


            Dim sb As New StringBuilder()
            ''Kolom Header
            'For k As Integer = 0 To objRptDs.Tables(0).Columns.Count - 1
            '	sb.Append(objRptDs.Tables(0).Columns(k).ColumnName + ","c)
            'Next
            'sb.Append(vbCr & vbLf)

            For i As Integer = 0 To objRptDs.Tables(0).Rows.Count - 1
                For k As Integer = 0 To objRptDs.Tables(0).Columns.Count - 1
                    'add separator
                    If k = objRptDs.Tables(0).Columns.Count - 1 Then
                        sb.Append(trim(objRptDs.Tables(0).Rows(i)(k).ToString().Replace(",", ";")))
                    Else
                        sb.Append(trim(objRptDs.Tables(0).Rows(i)(k).ToString().Replace(",", ";")) + ","c)
                    End If
                Next
                'append new line
                sb.Append(vbCr & vbLf)
            Next
            sb.Append(vbCr & vbLf)
            sb.Append(vbCr & vbLf)
            sb.Append("" + ","c + "" + ","c + trim(strlocname) + " " + Format(DateTime.Now, "dd MMMM yyyy"))
            sb.Append(vbCr & vbLf)
            sb.Append("Diperiksa Oleh" + ","c + "" + ","c + "Dibuat Oleh")
            sb.Append(vbCr & vbLf)
            sb.Append(vbCr & vbLf)
            sb.Append(vbCr & vbLf)
            sb.Append("KTU" + ","c + "" + ","c + "Kr.Personalia")


            Dim file As System.IO.FileInfo = New System.IO.FileInfo(strXmlPath)
            lblErrMessage.text = strXmlPath
            lblErrMessage.visible = True
            Response.Clear()
            Response.Buffer = True
            Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
            Response.Charset = ""
            Response.ContentType = "application/text"
            Response.Output.Write(sb.ToString())
            Response.Flush()
            Response.End()
        Else
            UserMsgBox(Me, "No Record Created")
            Exit Sub

        End If
    End Sub

    Sub BindReport()
        Dim objRptDs As New DataSet()
        Dim objVehicle As New DataSet()
        Dim objMapPath As String
        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim strOpCd As String = "PR_PR_STDRPT_GAJIBANK_REPORT"
        Dim strParamName As String
        Dim strParamValue As String
        Dim ReportID As String = Request.QueryString("RptID")


        strParamName = "ACCMONTH|ACCYEAR|PHYMONTH|PHYYEAR|COMPCODE|LOCCODE|DATEFROM|DATETO|DECIMAL"
        strParamValue = strAccMonth & "|" & _
                        strAccYear & "|" & _
                        strAccMonth & "|" & _
                        strAccYear & "|" & _
                        strCompany & "|" & _
                        strLocation & "|" & _
                        strDateFrom & "|" & _
                        strDateTo & "|" & _
                        intDecimal 
                        

        Try
            intErrNo = objOk.mtdGetReportDataCommon(strOpCd, strParamName, strParamValue, objRptDs, objMapPath, objFTPFolder)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PR_PR_STDRPT_GAJIKECIL_REPORT&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

		
        'rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\reports\crystal\PR_StdRpt_GajiBank_Estate.rpt", OpenReportMethod.OpenReportByTempCopy)
        'rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        'crvView.Visible = False
        'crvView.ReportSource = rdCrystalViewer
        'crvView.DataBind()

        ''PassParam()

        'crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        'crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\PR_StdRpt_GajiBank_Estate.csv"
        
        'crExportOptions = rdCrystalViewer.ExportOptions
        'With crExportOptions
        '    .DestinationOptions = crDiskFileDestinationOptions
        '    .ExportDestinationType = ExportDestinationType.DiskFile
        '    .ExportFormatType = ExportFormatType.Excel
        'End With
		

        'rdCrystalViewer.Export()
        'rdCrystalViewer.Close()
        'rdCrystalViewer.Dispose()
		


        'Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/PR_StdRpt_GajiBank_Estate.csv"">")
        'Response.End
    End Sub

    Sub PassParam()
        Dim paramFields As New ParameterFields()
        Dim paramField1 As New ParameterField()

        Dim ParamDiscreteValue1 As New ParameterDiscreteValue()

        Dim crParameterValues1 As ParameterValues


        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("ParamUserName")


        crParameterValues1 = paramField1.CurrentValues


        ParamDiscreteValue1.Value = Session("SS_USERNAME")


        crParameterValues1.Add(ParamDiscreteValue1)


        PFDefs(0).ApplyCurrentValues(crParameterValues1)

        crvView.ParameterFieldInfo = paramFields
    End Sub
End Class
