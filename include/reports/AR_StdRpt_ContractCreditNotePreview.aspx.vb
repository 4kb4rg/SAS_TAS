Imports System
Imports System.Data
Imports System.Collections 
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports System.Xml
Imports System.Web.Services
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web
imports System.Math

Imports agri.PWSystem.clsLangCap

Public Class AR_StdRpt_ContractCreditNotePreview : Inherits Page

    Dim objARRpt As New agri.BI.clsReport()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objBITrx As New agri.BI.clsTrx()

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents lblNoRecord As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim rdCrystalViewer As ReportDocument
    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strLocationName As String
    Dim strUserId As String
    Dim strPrintedBy As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfigsetting As Integer

    Dim strRptId As String
    Dim strRptName As String
    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim intSelDecimal As Integer
    Dim strSelSupress As String
    Dim strUserLoc As String
    Dim strFromCreditNoteId As String
    Dim strToCreditNoteId As String
    Dim strFromCreditNoteDate As String
    Dim strToCreditNoteDate As String
    Dim strBillPartyCode As String
    Dim strCreditNoteType As String
    Dim strSearchExp As String = ""
    Dim BillPartyTag As String

    Dim strUndName As String
    Dim strUndPost As String
    Dim strCompanyAddress As String

    Dim strExcPrintedDoc As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        crvView.Visible = False
        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strLocationName = Session("SS_LOCATIONNAME")
        strUserId = Session("SS_USERID")
        strPrintedBy = Session("SS_USERNAME")
        strAccMonth = Session("SS_ARACCMONTH")
        strAccYear = Session("SS_ARACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strCompanyAddress = Session("SS_COMPANYADDRESS")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            strUserLoc = Trim(Request.QueryString("Location"))
            strRptId = Trim(Request.QueryString("RptId"))
            strRptName = Trim(Request.QueryString("RptName"))
            strSelAccMonth = Request.QueryString("SelAccMonth")
            strSelAccYear = Request.QueryString("SelAccYear")
            intSelDecimal= CInt(Request.QueryString("Decimal"))
            strSelSupress = Request.QueryString("Supp")

            strFromCreditNoteId = Trim(Request.QueryString("FromCNId"))
            strToCreditNoteId = Trim(Request.QueryString("ToCNId"))
            strFromCreditNoteDate = Trim(Request.QueryString("FromCNDate"))
            strToCreditNoteDate = Trim(Request.QueryString("ToCNDate"))
            strBillPartyCode = Trim(Request.QueryString("BillPartyCode"))
            strCreditNoteType = Trim(Request.QueryString("CNType"))
            BillPartyTag = Trim(Request.QueryString("BillPartyTag"))
            strUndName = Trim(Request.QueryString("UndName"))
            strUndPost = Trim(Request.QueryString("UndPost"))
            strExcPrintedDoc = Trim(Request.QueryString("ExcPrintedDoc"))


            strSearchExp = "and dn.LocCode = '" & strUserLoc & "' " & _
                            "and dn.DocType = '" & strCreditNoteType & "' " & _
                            "and dn.Status = '" & objBITrx.EnumCreditNoteStatus.Confirmed & "' "

            If strExcPrintedDoc = "True" Then
                strSearchExp = strSearchExp + " and DateDiff(day, dn.PrintDate, '1900-01-01') = 0"
            End If

      
            If Trim(strFromCreditNoteId) <> "" Then
                strSearchExp = strSearchExp & "and dn.CreditNoteId >= '" & strFromCreditNoteId & "' "
            End If

            If Trim(strToCreditNoteId) <> "" Then
                strSearchExp = strSearchExp & "and dn.CreditNoteId <= '" & strToCreditNoteId & "' "
            End If

            If Trim(strFromCreditNoteDate) <> "" Then
                strSearchExp = strSearchExp & "and datediff(d, '" & strFromCreditNoteDate & "', dn.CreateDate) >= 0 "
            End If
            
            If Trim(strToCreditNoteDate) <> "" Then
                strSearchExp = strSearchExp & "and datediff(d, '" & strToCreditNoteDate & "', dn.CreateDate) <= 0 "
            End If

            If Trim(strBillPartyCode) <> "" Then
                strSearchExp = strSearchExp & "and dn.BillPartyCode like '" & strBillPartyCode & "' "
            End If

            BindReport()
        End If
    End Sub


    Sub BindReport()
        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim strOpCd As String
        Dim strOpCdRslGet As String = ""
        Dim objRptDs As New Dataset()
        Dim objMapPath As String
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strRptPrefix As String
        Dim intCnt As Integer
        dim totalRp as string


        Select Case CInt(strCreditNoteType)
            Case objBITrx.EnumCreditNoteDocType.Manual 
                strRptPrefix = "AR_StdRpt_ManualCreditNote"
                strOpCd = "AR_STDRPT_MANUAL_CREDITNOTE_GET_SP" & "|" & "AR_CREDIT"
            Case objBITrx.EnumCreditNoteDocType.Manual_Millware
                strRptPrefix = "AR_StdRpt_ManualMillCreditNote"
                strOpCd = "AR_STDRPT_MANUALMILL_CREDITNOTE_GET_SP" & "|" & "AR_CREDIT"
            Case objBITrx.EnumCreditNoteDocType.Auto_Millware
                strRptPrefix = "AR_StdRpt_AutoMillCreditNote"
                strOpCd = "AR_STDRPT_AUTOMILL_CREDITNOTE_GET_SP" & "|" & "AR_CREDIT"
            Case else
        End Select        

        strParam = strSelAccMonth & "|" & _
                   strSelAccYear & "|" & _
                   strSearchExp & "|" & _
                   objBITrx.EnumCreditNoteStatus.Active & "|" & _
                   objBITrx.EnumCreditNoteStatus.Confirmed


        Try
            intErrNo = objARRpt.mtdGetReport_CreditNote(strOpCd, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strParam, _
                                                        objRptDs, _
                                                        objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=AR_STDRPT_CREDITNOTE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

    
        For intCnt = 0 To objRptDs.Tables(0).Rows.Count - 1
            totalrp = Trim(CStr(objRptDs.Tables(0).Rows(intCnt).Item("Amount")))
            if totalrp>=0 then
            objRptDs.Tables(0).rows(intcnt).Item("Terbilang") = objGlobal.TerbilangDesimal(totalrp, "Rupiah")
            else
            objRptDs.Tables(0).rows(intcnt).Item("Terbilang") = "Negatif " & objGlobal.TerbilangDesimal(abs(cdbl(totalrp)), "Rupiah")
            end if            
        Next intCnt

   
        rdCrystalViewer = New ReportDocument()
        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strRptPrefix & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs)
        


        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strRptPrefix & ".pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strRptPrefix & ".pdf"">")
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

        Dim ParameterValues1 As New ParameterValues()
        Dim ParameterValues2 As New ParameterValues()
        Dim ParameterValues3 As New ParameterValues()
        Dim ParameterValues4 As New ParameterValues()
        Dim ParameterValues5 As New ParameterValues()
        Dim ParameterValues6 As New ParameterValues()
        Dim ParameterValues7 As New ParameterValues()
        Dim ParameterValues8 As New ParameterValues()
        Dim ParameterValues9 As New ParameterValues()

        Dim ParamDiscreteValue1 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue2 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue3 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue4 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue5 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue6 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue7 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue8 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue9 As New ParameterDiscreteValue()

        ParamDiscreteValue1.Value = strCompanyName
        ParamDiscreteValue2.Value = ""
        ParamDiscreteValue3.Value = ""
        ParamDiscreteValue4.Value = ""
        ParamDiscreteValue5.Value = strUndName
        ParamDiscreteValue6.Value = strUndPost
        ParamDiscreteValue7.Value = strPrintedBy
        ParamDiscreteValue8.Value = strLocationName
        if CInt(strCreditNoteType) = objBITrx.EnumCreditNoteDocType.Manual then
            ParamDiscreteValue9.Value = strCompanyAddress
        end if

        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields

        ParamFieldDef1 = ParamFieldDefs.Item("SellerName")
        ParamFieldDef2 = ParamFieldDefs.Item("BankAccNo")
        ParamFieldDef3 = ParamFieldDefs.Item("BankName")
        ParamFieldDef4 = ParamFieldDefs.Item("BankBranch")
        ParamFieldDef5 = ParamFieldDefs.Item("UndersignedName")
        ParamFieldDef6 = ParamFieldDefs.Item("UndersignedPost")
        ParamFieldDef7 = ParamFieldDefs.Item("SessionUserName")
        ParamFieldDef8 = ParamFieldDefs.Item("SessionLocName")
        if CInt(strCreditNoteType) =  objBITrx.EnumCreditNoteDocType.Manual   then
            ParamFieldDef9 = ParamFieldDefs.Item("SessionCompAdd")
        end if

        ParameterValues1 = ParamFieldDef1.CurrentValues
        ParameterValues2 = ParamFieldDef2.CurrentValues
        ParameterValues3 = ParamFieldDef3.CurrentValues
        ParameterValues4 = ParamFieldDef4.CurrentValues
        ParameterValues5 = ParamFieldDef5.CurrentValues
        ParameterValues6 = ParamFieldDef6.CurrentValues
        ParameterValues7 = ParamFieldDef7.CurrentValues
        ParameterValues8 = ParamFieldDef8.CurrentValues
        if CInt(strCreditNoteType) =  objBITrx.EnumCreditNoteDocType.Manual   then
            ParameterValues9 = ParamFieldDef9.CurrentValues
        end if

        ParameterValues1.Add(ParamDiscreteValue1)
        ParameterValues2.Add(ParamDiscreteValue2)
        ParameterValues3.Add(ParamDiscreteValue3)
        ParameterValues4.Add(ParamDiscreteValue4)
        ParameterValues5.Add(ParamDiscreteValue5)
        ParameterValues6.Add(ParamDiscreteValue6)
        ParameterValues7.Add(ParamDiscreteValue7)
        ParameterValues8.Add(ParamDiscreteValue8)
        if CInt(strCreditNoteType) =  objBITrx.EnumCreditNoteDocType.Manual   then
            ParameterValues9.Add(ParamDiscreteValue9)
        end if

        ParamFieldDef1.ApplyCurrentValues(ParameterValues1)
        ParamFieldDef2.ApplyCurrentValues(ParameterValues2)
        ParamFieldDef3.ApplyCurrentValues(ParameterValues3)
        ParamFieldDef4.ApplyCurrentValues(ParameterValues4)
        ParamFieldDef5.ApplyCurrentValues(ParameterValues5)
        ParamFieldDef6.ApplyCurrentValues(ParameterValues6)
        ParamFieldDef7.ApplyCurrentValues(ParameterValues7)
        ParamFieldDef8.ApplyCurrentValues(ParameterValues8)
        if CInt(strCreditNoteType) =  objBITrx.EnumCreditNoteDocType.Manual   then
            ParamFieldDef9.ApplyCurrentValues(ParameterValues9)
        end if
    End Sub

End Class
