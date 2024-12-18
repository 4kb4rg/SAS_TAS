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

Public Class AP_StdRpt_SuppAccAgeList_Preview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objAP As New agri.AP.clsReport()
    Dim objAPTrx As New agri.AP.clsTrx()
    Dim objPUSetup As New agri.PU.clsSetup()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()

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
    Dim strDDLAccMth As String
    Dim strDDLAccYr As String
    Dim strParam As String
    Dim strDate As String
    Dim strSuppCode As String
    Dim objMapPath As String
    Dim arrUserLoc As Array
    Dim intCntUserLoc As Integer
    Dim strLocCode As String

    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer

    Dim strAccCode As String
    Dim strCutOffDate As String
    Dim strToAge1 As String
    Dim strFromAge2 As String
    Dim strToAge2 As String
    Dim strFromAge3 As String
    Dim strToAge3 As String
    Dim strFromAge4 As String
    Dim strToAge4 As String
    Dim strFromAge5 As String

    Dim strCategory1 As String
    Dim strCategory2 As String
    Dim strCategory3 As String
    Dim strCategory4 As String
    Dim strCategory5 As String






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

        strDDLAccMth = Request.QueryString("DDLAccMth")
        strDDLAccYr = Request.QueryString("DDLAccYr")

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_APACCMONTH")
        strAccYear = Session("SS_APACCYEAR")
        strLangCode = Session("SS_LANGCODE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            strAccCode = Trim(Request.QueryString("AccCode"))
            strCutOffDate = Trim(Request.QueryString("CutOffDate"))
            strToAge1 = Trim(Request.QueryString("ToAge1"))
            strFromAge2 = Trim(Request.QueryString("FromAge2"))
            strToAge2 = Trim(Request.QueryString("ToAge2"))
            strFromAge3 = Trim(Request.QueryString("FromAge3"))
            strToAge3 = Trim(Request.QueryString("ToAge3"))
            strFromAge4 = Trim(Request.QueryString("FromAge4"))
            strToAge4 = Trim(Request.QueryString("ToAge4"))
            strFromAge5 = Trim(Request.QueryString("FromAge5"))

            strCategory1 = "1 - " & strToAge1
            strCategory2 = strFromAge2 & " - " & strToAge2
            strCategory3 = strFromAge3 & " - " & strToAge3
            strCategory4 = strFromAge4 & " - " & strToAge4
            strCategory5 = strFromAge5

            BindReport()
        End If
    End Sub

    Sub BindReport()

        Dim objRptDs As New DataSet()

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOpCd As String '= "PU_STDRPT_SUPPLIER_GET"
        Dim strSearchExp As String
        Dim strRptPrefix As String

        Dim strInvStatus As String
        Dim strDNStatus As String
        Dim strCNStatus As String
        Dim strPYStatus As String
        Dim strCJStatus As String
        Dim objFTPFolder As String

        strRptPrefix = "AP_StdRpt_SuppAccAgeList"
        strOpCd = "AP_STDRPT_SUPPLIER_ACCOUNT_AGEING_GET_SP" & "|" & "AP_SUPPACCAGE"

        If InStr(strUserLoc, "','") > 0 Then
            strUserLoc = Replace(strUserLoc, "','", "'',''")
        End If

        If Not Request.QueryString("Supplier") = "" Then
            strSearchExp = "AND (SUPP.SupplierCode LIKE '%" & Request.QueryString("Supplier") & "%' OR SUPP.Name LIKE '%" & Request.QueryString("Supplier") & "%') " & _
                        "AND SUPP.Status = '" & objPUSetup.EnumSuppStatus.Active & "' "
        End If

        If strAccCode <> "" Then
            strSearchExp = strSearchExp & "AND SUPP.AccCode LIKE '" & strAccCode & "' "
        End If

        strInvStatus = objAPTrx.EnumInvoiceRcvStatus.Confirmed & "','" & objAPTrx.EnumInvoiceRcvStatus.Closed & "','" & objAPTrx.EnumInvoiceRcvStatus.Writeoff & "','" & objAPTrx.EnumInvoiceRcvStatus.Paid
        strDNStatus = objAPTrx.EnumDebitNoteStatus.Confirmed & "','" & objAPTrx.EnumDebitNoteStatus.Closed & "','" & objAPTrx.EnumDebitNoteStatus.Writeoff & "','" & objAPTrx.EnumDebitNoteStatus.Paid
        strCNStatus = objAPTrx.EnumCreditNoteStatus.Confirmed & "','" & objAPTrx.EnumCreditNoteStatus.Closed & "','" & objAPTrx.EnumCreditNoteStatus.Writeoff & "','" & objAPTrx.EnumCreditNoteStatus.Paid
        strPYStatus = objAPTrx.EnumPaymentStatus.Confirmed & "','" & objAPTrx.EnumPaymentStatus.Closed & "','" & objAPTrx.EnumPaymentStatus.Void 'BFR_THE00048 Item 57 
        strCJStatus = objAPTrx.EnumCreditorJournalStatus.Confirmed & "','" & objAPTrx.EnumCreditorJournalStatus.Closed & "','" & objAPTrx.EnumCreditorJournalStatus.WriteOff

        strParam = strUserLoc & "|" & _
                    strDDLAccMth & "|" & _
                    strDDLAccYr & "|" & _
                    strSearchExp & "|" & _
                    Request.QueryString("Suppress") & "|" & _
                    strCutOffDate & "|" & _
                    strToAge1 & "|" & _
                    strFromAge2 & "|" & _
                    strToAge2 & "|" & _
                    strFromAge3 & "|" & _
                    strToAge3 & "|" & _
                    strFromAge4 & "|" & _
                    strToAge4 & "|" & _
                    strFromAge5 & "|" & _
                    strInvStatus & "|" & _
                    strDNStatus & "|" & _
                    strCNStatus & "|" & _
                    strPYStatus & "|" & _
                    strCJStatus

        Try
            intErrNo = objAP.mtdGetReport_SupplierAccAgeing(strOpCd, _
                                                             strCompany, _
                                                             strLocation, _
                                                             strUserId, _
                                                             strAccMonth, _
                                                             strAccYear, _
                                                             strParam, _
                                                             objRptDs, _
                                                             objMapPath, objFTPFolder)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=AP_STDRPT_SUPPACCAGE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try



        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strRptPrefix & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs)

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3


        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        'crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strRptPrefix & ".pdf"
        crDiskFileDestinationOptions.DiskFileName = objFTPFolder & strRptPrefix & ".pdf"

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
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../" & strUrl & strRptPrefix & ".pdf"">")
        'Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strRptPrefix & ".pdf"">")
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
        Dim ParamFieldDef12 As ParameterFieldDefinition
        Dim ParamFieldDef13 As ParameterFieldDefinition
        Dim ParamFieldDef14 As ParameterFieldDefinition
        Dim ParamFieldDef15 As ParameterFieldDefinition
        Dim ParamFieldDef16 As ParameterFieldDefinition
        Dim ParamFieldDef17 As ParameterFieldDefinition
        Dim ParamFieldDef18 As ParameterFieldDefinition
        Dim ParamFieldDef19 As ParameterFieldDefinition

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
        Dim ParameterValues12 As New ParameterValues()
        Dim ParameterValues13 As New ParameterValues()
        Dim ParameterValues14 As New ParameterValues()
        Dim ParameterValues15 As New ParameterValues()
        Dim ParameterValues16 As New ParameterValues()
        Dim ParameterValues17 As New ParameterValues()
        Dim ParameterValues18 As New ParameterValues()
        Dim ParameterValues19 As New ParameterValues()

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

        ParamDiscreteValue1.Value = Session("SS_LOC")
        ParamDiscreteValue2.Value = Request.QueryString("RptID")
        ParamDiscreteValue3.Value = Request.QueryString("RptName")
        ParamDiscreteValue4.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue5.Value = Session("SS_USERNAME")
        ParamDiscreteValue6.Value = Request.QueryString("Decimal")
        ParamDiscreteValue7.Value = Request.QueryString("DDLAccMth")
        ParamDiscreteValue8.Value = Request.QueryString("DDLAccYr")
        ParamDiscreteValue9.Value = Request.QueryString("Supplier")
        ParamDiscreteValue10.Value = Request.QueryString("lblLocation")
        ParamDiscreteValue11.Value = Request.QueryString("AccCodeTag")
        ParamDiscreteValue12.Value = strAccCode
        ParamDiscreteValue13.Value = strCutOffDate
        ParamDiscreteValue14.Value = strCategory1
        ParamDiscreteValue15.Value = strCategory2
        ParamDiscreteValue16.Value = strCategory3
        ParamDiscreteValue17.Value = strCategory4
        ParamDiscreteValue18.Value = strCategory5
        ParamDiscreteValue19.Value = Request.QueryString("Suppress")

        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        ParamFieldDef1 = ParamFieldDefs.Item("ParamLocation")
        ParamFieldDef2 = ParamFieldDefs.Item("ParamRptID")
        ParamFieldDef3 = ParamFieldDefs.Item("ParamRptName")
        ParamFieldDef4 = ParamFieldDefs.Item("ParamCompanyName")
        ParamFieldDef5 = ParamFieldDefs.Item("ParamUserName")
        ParamFieldDef6 = ParamFieldDefs.Item("ParamDecimal")
        ParamFieldDef7 = ParamFieldDefs.Item("ParamAccMonth")
        ParamFieldDef8 = ParamFieldDefs.Item("ParamAccYear")
        ParamFieldDef9 = ParamFieldDefs.Item("ParamSupplier")
        ParamFieldDef10 = ParamFieldDefs.Item("lblLocation")
        ParamFieldDef11 = ParamFieldDefs.Item("lblAccCode")
        ParamFieldDef12 = ParamFieldDefs.Item("ParamAccCode")
        ParamFieldDef13 = ParamFieldDefs.Item("ParamCutOffDate")
        ParamFieldDef14 = ParamFieldDefs.Item("ParamCategory1")
        ParamFieldDef15 = ParamFieldDefs.Item("ParamCategory2")
        ParamFieldDef16 = ParamFieldDefs.Item("ParamCategory3")
        ParamFieldDef17 = ParamFieldDefs.Item("ParamCategory4")
        ParamFieldDef18 = ParamFieldDefs.Item("ParamCategory5")
        ParamFieldDef19 = ParamFieldDefs.Item("ParamSuppress")

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
        ParameterValues12 = ParamFieldDef12.CurrentValues
        ParameterValues13 = ParamFieldDef13.CurrentValues
        ParameterValues14 = ParamFieldDef14.CurrentValues
        ParameterValues15 = ParamFieldDef15.CurrentValues
        ParameterValues16 = ParamFieldDef16.CurrentValues
        ParameterValues17 = ParamFieldDef17.CurrentValues
        ParameterValues18 = ParamFieldDef18.CurrentValues
        ParameterValues19 = ParamFieldDef19.CurrentValues

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
        ParameterValues12.Add(ParamDiscreteValue12)
        ParameterValues13.Add(ParamDiscreteValue13)
        ParameterValues14.Add(ParamDiscreteValue14)
        ParameterValues15.Add(ParamDiscreteValue15)
        ParameterValues16.Add(ParamDiscreteValue16)
        ParameterValues17.Add(ParamDiscreteValue17)
        ParameterValues18.Add(ParamDiscreteValue18)
        ParameterValues19.Add(ParamDiscreteValue19)

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
        ParamFieldDef12.ApplyCurrentValues(ParameterValues12)
        ParamFieldDef13.ApplyCurrentValues(ParameterValues13)
        ParamFieldDef14.ApplyCurrentValues(ParameterValues14)
        ParamFieldDef15.ApplyCurrentValues(ParameterValues15)
        ParamFieldDef16.ApplyCurrentValues(ParameterValues16)
        ParamFieldDef17.ApplyCurrentValues(ParameterValues17)
        ParamFieldDef18.ApplyCurrentValues(ParameterValues18)
        ParamFieldDef19.ApplyCurrentValues(ParameterValues19)

    End Sub























































End Class
