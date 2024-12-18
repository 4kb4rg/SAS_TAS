Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic
Imports System.XML
Imports System.Web.Services
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web

Imports agri.PU.clsTrx
Imports agri.Admin.clsShare
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.PWSystem.clsLangCap


Public Class TX_Rpt_TaxReports : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents dgResult As DataGrid
    Protected WithEvents lblErrMessage As Label

    Dim objGLRpt As New agri.GL.clsReport()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objLangCapDs As New Object()
    Dim rdCrystalViewer As New ReportDocument()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String

    Dim strTrxID As String
    Dim strDocID As String
    Dim strCreateDate As String
    Dim strAccCode As String
    Dim strSupplierCode As String
    Dim strPrintOpt As String
    Dim strQtySPP1 As String
    Dim strQtySPP2 As String
    Dim strQtySPP3 As String
    Dim strQtyBuktiPtg As String
    Dim strSPTRev As String
    Dim strCompNPWPNo As String
    Dim strCompNPWPLoc As String
    Dim strTaxInit As String
    Dim strPelapor As String
    Dim strPelaporNPWP As String
    Dim strPelapor2 As String
    Dim strKPPInit As String
    Dim strExportToExcel As String

    Dim strTerbilang1 As String = ""
    Dim strTerbilang2 As String = ""
    Dim strQtyBPOT As String
    Dim strAkunPjk As String
    Dim strJnsSetoran As String
    Dim strUraianBayar As String
    Dim strLembar As String
    Dim strLembarInt As String

    Dim strCompName As String
    Dim strLocName As String
    Dim strUserName As String

    Public Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strCompName = Session("SS_COMPANYNAME")
        strLocName = Session("SS_LOCATIONNAME")
        strUserName = Session("SS_USERNAME")

        crvView.Visible = False

        strTrxID = Trim(Request.QueryString("strTrxID"))
        strDocID = Trim(Request.QueryString("strDocID"))
        strCreateDate = Trim(Request.QueryString("strCreateDate"))
        strAccMonth = Trim(Request.QueryString("strAccMonth"))
        strAccYear = Trim(Request.QueryString("strAccYear"))
        strAccCode = Trim(Request.QueryString("strAccCode"))
        strSupplierCode = Trim(Request.QueryString("strSupplierCode"))
        strPrintOpt = Trim(Request.QueryString("strPrintOpt"))
        strQtySPP1 = Trim(Request.QueryString("strQtySPP1"))
        strQtySPP2 = Trim(Request.QueryString("strQtySPP2"))
        strQtySPP3 = Trim(Request.QueryString("strQtySPP3"))
        strQtyBuktiPtg = Trim(Request.QueryString("strQtyBuktiPtg"))
        strSPTRev = Trim(Request.QueryString("strSPTRev"))
        strCompNPWPNo = Trim(Request.QueryString("strCompNPWPNo"))
        strCompNPWPLoc = Trim(Request.QueryString("strCompNPWPLoc"))
        strTaxInit = Trim(Request.QueryString("strTaxInit"))
        strPelapor = Trim(Request.QueryString("strPelapor"))
        strPelaporNPWP = Trim(Request.QueryString("strPelaporNPWP"))
        strPelapor2 = Trim(Request.QueryString("strPelapor2"))
        strKPPInit = Trim(Request.QueryString("strKPPInit"))
        strExportToExcel = Trim(Request.QueryString("ExportToExcel"))
        strAkunPjk = Trim(Request.QueryString("strAkunPjk"))
        strJnsSetoran = Trim(Request.QueryString("strJnsSetoran"))
        strUraianBayar = Trim(Request.QueryString("strUraianBayar"))
        strLembar = Trim(Request.QueryString("strLembar"))
        strLembarInt = Trim(Request.QueryString("strLembarInt"))

        'response.write(strKPPInit & "," & strTaxInit)
        Bind_ITEM(True)
    End Sub

    Sub Bind_ITEM(ByVal blnIsPDFFormat As Boolean)
        Dim crLogonInfo As CrystalDecisions.Shared.TableLogOnInfo
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim crExportOptions As ExportOptions
        Dim objRptDs As New DataSet()
        Dim objMapPath As String
        Dim strOpCd As String = "TX_STDRPT_GETDATA"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strReportName As String
        Dim strPDFName As String
        Dim objFTPFolder As String
        Dim TtlAmt1 As String
        Dim TtlAmt2 As String
        Dim strBeforeDec1 As String
        Dim strBeforeDec2 As String
        Dim strSearch As String

        Select Case strPrintOpt
            Case "1", "5"
                strOpCd = "TX_STDRPT_GET_SPTMASA"
            Case "2"
                strOpCd = "TX_STDRPT_GET_DAFTARPTG"
            Case "3"
                strOpCd = "TX_STDRPT_GET_BUKTIPTG"
            Case "4"
                If strTrxID = "XXX" Then
                    strOpCd = "TX_STDRPT_GET_TAXVERIFIEDNEEDLISTING"
                Else
                    strOpCd = "TX_STDRPT_GET_TAXVERIFIEDLISTING"
                End If
        End Select

        strSearch = IIf(strKPPInit = "", "", " KPPInit='" & strKPPInit & " ' AND ")
        strSearch = strSearch + IIf(strTrxID = "", "", IIf(strTrxID = "XXX", "", " TrxID = '" & strTrxID & "' AND "))
        If (Trim(strSearch) <> "") Then
            strSearch = " AND " & Mid(strSearch, 1, Len(strSearch) - 4) & " "
        End If


        strParamName = "COMPCODE|LOCCODE|ACCMONTH|ACCYEAR|TAXINIT|STRSEARCH"
        If strTaxInit = "23" Or strTaxInit = "26" Then
            If strPrintOpt = "3" Then
                strParamValue = strCompany & "|" & strLocation & "|" & strAccMonth & "|" & strAccYear & "|" & strTaxInit & "|" & strSearch
                'If strTrxID <> "" Then
                '    strParamValue = strCompany & "|" & strLocation & "|" & strAccMonth & "|" & strAccYear & "|" & strTaxInit & "|" & strSearch
                'Else
                '    strParamValue = strCompany & "|" & strLocation & "|" & strAccMonth & "|" & strAccYear & "|" & strTaxInit & "|"
                'End If
            Else
                strParamValue = strCompany & "|" & strLocation & "|" & strAccMonth & "|" & strAccYear & "|" & "'23','26'" & "|" & strSearch
                'If strTrxID <> "" Then
                '    strParamValue = strCompany & "|" & strLocation & "|" & strAccMonth & "|" & strAccYear & "|" & "'23','26'" & "|" & " AND TrxID = '" & strTrxID & "'"
                'Else
                '    strParamValue = strCompany & "|" & strLocation & "|" & strAccMonth & "|" & strAccYear & "|" & "'23','26'" & "|"
                'End If
            End If
           
        ElseIf strTaxInit = "" Then
            strParamValue = strCompany & "|" & strLocation & "|" & strAccMonth & "|" & strAccYear & "|" & "'21','22','23','26','4'" & "|" & strSearch
            'If strTrxID <> "" Then
            '    If strTrxID = "XXX" Then
            '        strParamValue = strCompany & "|" & strLocation & "|" & strAccMonth & "|" & strAccYear & "|" & "'21','22','23','26','4'" & "|"
            '    Else
            '        strParamValue = strCompany & "|" & strLocation & "|" & strAccMonth & "|" & strAccYear & "|" & "'21','22','23','26','4'" & "|" & " AND TrxID = '" & strTrxID & "'"
            '    End If
            'Else
            '    strParamValue = strCompany & "|" & strLocation & "|" & strAccMonth & "|" & strAccYear & "|" & "'21','22','23','26','4'" & "|"
            'End If
        Else
            strParamValue = strCompany & "|" & strLocation & "|" & strAccMonth & "|" & strAccYear & "|" & strTaxInit & "|" & strSearch
            'If strTrxID <> "" Then
            '    strParamValue = strCompany & "|" & strLocation & "|" & strAccMonth & "|" & strAccYear & "|" & strTaxInit & "|" & " AND TrxID = '" & strTrxID & "'"
            'Else
            '    strParamValue = strCompany & "|" & strLocation & "|" & strAccMonth & "|" & strAccYear & "|" & strTaxInit & "|"
            'End If
        End If

        Try
            intErrNo = objGLRpt.mtdGetReportDataCommon(strOpCd, strParamName, strParamValue, objRptDs, objMapPath, objFTPFolder)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        Try
            intErrNo = objAdmin.mtdGetBasePath(objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objRptDs.Tables(0).Rows.Count > 0 Then
            Select Case strPrintOpt
                Case "1"
                    strReportName = Trim(objRptDs.Tables(0).Rows(intCnt).Item("SPTMasa"))
                Case "2"
                    strReportName = Trim(objRptDs.Tables(0).Rows(intCnt).Item("DaftarPtg"))
                Case "3"
                    strReportName = Trim(objRptDs.Tables(0).Rows(intCnt).Item("BuktiPtg"))
                Case "4"
                    If strTrxID = "XXX" Then
                        strReportName = "RptTaxVerifiedNeedListing"
                    Else
                        strReportName = "RptTaxVerifiedListing"
                    End If
                Case "5"
                    strReportName = "SSPAll"
            End Select

            If strPrintOpt = "1" Or strPrintOpt = "3" Or strPrintOpt = "5" Then
                For intCnt = 0 To objRptDs.Tables(0).Rows.Count - 1
                    If objRptDs.Tables(0).Rows(intCnt).Item("Tbl1TtlTaxAmt") = 0 Then
                        strTerbilang1 = ""
                        objRptDs.Tables(0).Rows(intCnt).Item("Tbl1Terbilang") = strTerbilang1
                    Else
                        TtlAmt1 = Trim(CStr(FormatNumber(objRptDs.Tables(0).Rows(intCnt).Item("Tbl1TtlTaxAmt"), 2)))
                        strBeforeDec1 = objGlobal.TerbilangDesimal(Replace(TtlAmt1, ",", ""), "Rupiah")
                        strTerbilang1 = strBeforeDec1
                        objRptDs.Tables(0).Rows(intCnt).Item("Tbl1Terbilang") = Replace(strTerbilang1, "SeRatus", "Seratus")
                    End If

                    If objRptDs.Tables(0).Rows(intCnt).Item("Tbl2TtlTaxAmt") = 0 Then
                        strTerbilang2 = ""
                        objRptDs.Tables(0).Rows(intCnt).Item("Tbl2Terbilang") = strTerbilang2
                    Else
                        TtlAmt2 = Trim(CStr(FormatNumber(objRptDs.Tables(0).Rows(intCnt).Item("Tbl2TtlTaxAmt"), 2)))
                        strBeforeDec2 = objGlobal.TerbilangDesimal(Replace(TtlAmt2, ",", ""), "Rupiah")
                        strTerbilang2 = strBeforeDec2
                        objRptDs.Tables(0).Rows(intCnt).Item("Tbl2Terbilang") = Replace(strTerbilang2, "SeRatus", "Seratus")
                    End If
                Next

                If strPrintOpt = "1" And strTaxInit <> "22" Then
                    strQtyBPOT = objRptDs.Tables(1).Rows(0).Item("QtyBPOT")
                Else
                    strQtyBPOT = ""
                End If
            Else
                strQtyBPOT = ""
            End If
        Else
            Response.Write("Cannot found any data !")
            Exit Sub
        End If

        rdCrystalViewer.Load(objMapPath & "Web\EN\TX\Reports\Crystal\" & strReportName & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        crvView.Visible = False
        crvView.ReportSource = rdCrystalViewer
        crvView.DataBind()
        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()

        If strExportToExcel = "0" Then
            crDiskFileDestinationOptions.DiskFileName = objFTPFolder & strReportName & ".pdf"
        Else
            crDiskFileDestinationOptions.DiskFileName = objFTPFolder & strReportName & ".xls"
        End If

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            If strExportToExcel = "0" Then
                .ExportFormatType = ExportFormatType.PortableDocFormat
            Else
                .ExportFormatType = ExportFormatType.Excel
            End If
        End With


        rdCrystalViewer.Export()
        rdCrystalViewer.Close()
        rdCrystalViewer.Dispose()

        Dim strUrl As String
        strUrl = objFTPFolder.Substring(InStr(objFTPFolder, "ftp") - 1, Len(objFTPFolder) - InStr(objFTPFolder, "ftp") + 1)
        strUrl = Replace(strUrl, "\", "/")

        If strExportToExcel = "0" Then
            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../../" & strUrl & strReportName & ".pdf"">")
        Else
            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../../" & strUrl & strReportName & ".xls"">")
        End If


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

        paramField1 = paramFields.Item("strPrintedBy")
        paramField2 = paramFields.Item("strCompName")
        paramField3 = paramFields.Item("strCreateDate")
        paramField4 = paramFields.Item("strDocID")
        paramField5 = paramFields.Item("strTerbilang1")
        paramField6 = paramFields.Item("strTerbilang2")
        paramField7 = paramFields.Item("strQtySPP1")
        paramField8 = paramFields.Item("strQtySPP2")
        paramField9 = paramFields.Item("strQtySPP3")
        paramField10 = paramFields.Item("strQtyBuktiPtg")
        paramField11 = paramFields.Item("strSPTRev")
        paramField12 = paramFields.Item("strCompNPWPLoc")
        paramField13 = paramFields.Item("strPelapor")
        paramField14 = paramFields.Item("strPelaporNPWP")
        paramField15 = paramFields.Item("strPelapor2")
        paramField16 = paramFields.Item("strAkunPjk")
        paramField17 = paramFields.Item("strJnsSetoran")
        paramField18 = paramFields.Item("strUraianBayar")
        paramField19 = paramFields.Item("strLembar")
        paramField20 = paramFields.Item("strLembarInt")

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

        ParamDiscreteValue1.Value = strUserName
        ParamDiscreteValue2.Value = strCompName
        ParamDiscreteValue3.Value = strCreateDate
        ParamDiscreteValue4.Value = strDocID
        ParamDiscreteValue5.Value = strTerbilang1
        ParamDiscreteValue6.Value = strTerbilang2
        ParamDiscreteValue7.Value = strQtySPP1
        ParamDiscreteValue8.Value = strQtySPP2
        ParamDiscreteValue9.Value = strQtySPP3
        ParamDiscreteValue10.Value = strQtyBPOT 'strQtyBuktiPtg
        ParamDiscreteValue11.Value = strSPTRev
        ParamDiscreteValue12.Value = strCompNPWPLoc
        ParamDiscreteValue13.Value = strPelapor
        ParamDiscreteValue14.Value = strPelaporNPWP
        ParamDiscreteValue15.Value = strPelapor2
        ParamDiscreteValue16.Value = strAkunPjk
        ParamDiscreteValue17.Value = strJnsSetoran
        ParamDiscreteValue18.Value = strUraianBayar
        ParamDiscreteValue19.Value = strLembar
        ParamDiscreteValue20.Value = strLembarInt

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

