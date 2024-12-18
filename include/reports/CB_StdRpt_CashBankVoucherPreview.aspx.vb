Imports System
Imports System.Data
Imports System.Collections 
Imports System.Data.SqlClient
imports System.Math
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
Imports agri.GlobalHdl.clsGlobalHdl

Public Class CB_StdRpt_CashBankVoucherPreview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents lblNoRecord As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objGLRpt As New agri.GL.clsReport()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objCBTrx As New agri.CB.clsTrx()

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

    Dim strUserLoc As String
    Dim strFromReceiptId As String
    Dim strToReceiptId As String



    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        crvView.Visible = False

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strLocationName = Session("SS_LOCATIONNAME")
        strUserId = Session("SS_USERID")
        strPrintedBy = Session("SS_USERNAME")
        strAccMonth = Session("SS_CBACCMONTH")
        strAccYear = Session("SS_CBACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            strUserLoc = Trim(Request.QueryString("Location"))
            strFromReceiptId = Trim(Request.QueryString("FromReceiptId"))
            strToReceiptId = Trim(Request.QueryString("ToReceiptId"))
            BindReport()
        End If
    End Sub


    Sub BindReport()
        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim strOpCode As String
        Dim strOpCdRslGet As String = ""
        Dim objRptDs As New DataSet()
        Dim objMapPath As String
        Dim intErrNo As Integer
        Dim strParamName As String
        Dim strParamValue As String
        Dim strRptPrefix As String
        Dim intCnt As Integer
        Dim totalRp As Double
        Dim objFTPFolder As String

        strRptPrefix = "CB_StdRpt_CashBankVoucher"
        strOpCode = "CB_CLSTRX_CASHBANK_GET_FOR_DOCRPT"

        'strParamName = "CASHBANKIDFROM|CASHBANKIDTO|LOCCODE"
        'strParamValue = strFromReceiptId & "|" & strToReceiptId & "|" & strLocation

        strParamName = "CASHBANKIDFROM|CASHBANKIDTO|LOCCODE"
        strParamValue = strFromReceiptId & "|" & strToReceiptId & "|" & strLocation

        Try
            intErrNo = objGLRpt.mtdGetReportDataCommon(strOpCode, strParamName, strParamValue, objRptDs, objMapPath, objFTPFolder)

            'intErrNo = objGLRpt.mtdGetReport_BiayaProduksi(strOpCode, strParamName, strParamValue, objRptDs, objMapPath)
        Catch Exp As Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=CB_STDRPT_RECEIPTVOUCER_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        objRptDs.Tables(0).TableName = "CashBank"
        objRptDs.Tables(1).TableName = "CashBankLn"

        For intCnt = 0 To objRptDs.Tables(0).Rows.Count - 1
            totalrp = Trim(CStr(objRptDs.Tables(0).Rows(intCnt).Item("TotalAmount")))
            If totalrp >= 0 Then
                objRptDs.Tables(0).rows(intcnt).Item("UpdateID") = objGlobal.TerbilangDesimal(totalrp, "Rupiah")
            End If
        Next intCnt

       
        rdCrystalViewer = New ReportDocument()
        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strRptPrefix & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs)

        crvView.Visible = False
        crvView.ReportSource = rdCrystalViewer
        crvView.DataBind()

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()


        crDiskFileDestinationOptions.DiskFileName = objFTPFolder & strRptPrefix & ".pdf" 'objMapPath & "web\ftp\" & strRptPrefix & ".pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()

        Dim strUrl As String
        strUrl = objFTPFolder.Substring(InStr(objFTPFolder, "ftp") - 1, Len(objFTPFolder) - InStr(objFTPFolder, "ftp") + 1)
        strUrl = Replace(strUrl, "\", "/")

        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../" & strUrl & strRptPrefix & ".pdf"">")


        'Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strRptPrefix & ".pdf"">")

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

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        ParamFields = crvView.ParameterFieldInfo

        ParamField1 = ParamFields.Item("strPrintDate")
        ParamField2 = ParamFields.Item("strPrintedBy")
        ParamField3 = ParamFields.Item("strCompName")
        ParamField4 = ParamFields.Item("strLocName")
        ParamField5 = ParamFields.Item("strStatus")
        ParamField6 = ParamFields.Item("lblAccount")
        ParamField7 = ParamFields.Item("TotalAmountSentence")
        ParamField8 = ParamFields.Item("strVehTag")
        ParamField9 = ParamFields.Item("strVehExpTag")
        ParamField10 = ParamFields.Item("strBlockTag")

        crParameterValues1 = ParamField1.CurrentValues
        crParameterValues2 = ParamField2.CurrentValues
        crParameterValues3 = ParamField3.CurrentValues
        crParameterValues4 = ParamField4.CurrentValues
        crParameterValues5 = ParamField5.CurrentValues
        crParameterValues6 = ParamField6.CurrentValues
        crParameterValues7 = ParamField7.CurrentValues
        crParameterValues8 = ParamField8.CurrentValues
        crParameterValues9 = ParamField9.CurrentValues
        crParameterValues10 = ParamField10.CurrentValues

        ParamDiscreteValue1.value = "1"
        ParamDiscreteValue2.value = strPrintedBy
        ParamDiscreteValue3.value = strCompanyName
        ParamDiscreteValue4.value = strLocationName
        ParamDiscreteValue5.value = "1"
        ParamDiscreteValue6.value = "COA"
        ParamDiscreteValue7.value = "1"
        ParamDiscreteValue8.value = "1"
        ParamDiscreteValue9.value = "Kendaraan"
        ParamDiscreteValue10.value = "Mesin"

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

        crvView.ParameterFieldInfo = paramFields

    End Sub

End Class
