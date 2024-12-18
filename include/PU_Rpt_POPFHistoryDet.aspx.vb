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


Public Class PU_Rpt_POPFHistoryDet : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents dgResult As DataGrid
    Protected WithEvents lblErrMesage As Label

    Dim objPU As New agri.PU.clsTrx()
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

    Dim strStatus As String
    Dim strPOId As String
    Dim strPrintDate As String
    Dim strSortLine As String
    Dim batchPrint As String
    Dim strReprintedID As String
    Dim arrReprintedID As Array
    Dim intCnt2 As Integer

    Dim strCompName As String
    Dim strLocName As String
    Dim strUserName As String

    Dim strRptId As String
    Dim strRptName As String
    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim intSelDecimal As Integer
    Dim strSelSupress As String
    Dim strUserLoc As String
    Dim strPONoFrom As String
    Dim strPONoTo As String
    Dim strPeriodeFrom As String
    Dim strPeriodeTo As String
    Dim strPIC1 As String
    Dim strJabatan1 As String
    Dim strPIC2 As String
    Dim strJabatan2 As String
    Dim strCatatan As String
    Dim strSearchExp As String = ""
    Dim strLokasi As String
    Dim strCompanyAddress As String
    Dim strSuppCode As String
    Dim strItemCode As String
    Dim strService As String
    Dim strSentPeriod As String

    Public Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PUACCMONTH")
        strAccYear = Session("SS_PUACCYEAR")
        strCompName = Session("SS_COMPANYNAME")
        strLocName = Session("SS_LOCATIONNAME")
        strUserName = Session("SS_USERNAME")

        crvView.Visible = False

        strUserLoc = Trim(Request.QueryString("Location"))
        strRptId = Trim(Request.QueryString("RptId"))
        strRptName = Trim(Request.QueryString("RptName"))
        intSelDecimal = CInt(Request.QueryString("Decimal"))

        strPONoFrom = Trim(Request.QueryString("PONoFrom"))
        strPONoTo = Trim(Request.QueryString("PONoTo"))
        strPeriodeFrom = Trim(Request.QueryString("PeriodeFrom"))
        strPeriodeTo = Trim(Request.QueryString("PeriodeTo"))
        strPIC1 = Trim(Request.QueryString("PIC1"))
        strJabatan1 = Trim(Request.QueryString("Jabatan1"))
        strPIC2 = Trim(Request.QueryString("PIC2"))
        strJabatan2 = Trim(Request.QueryString("Jabatan2"))
        strCatatan = Trim(Request.QueryString("Catatan"))
        strLokasi = Request.QueryString("Lokasi")
        strSuppCode = Request.QueryString("SuppCode")
        strItemCode = Request.QueryString("ItemCode")
        strService = Request.QueryString("Service")
        strSentPeriod = Request.QueryString("SentPeriod")

        Bind_ITEM(True)

    End Sub

    Sub Bind_ITEM(ByVal blnIsPDFFormat As Boolean)
        Dim crLogonInfo As CrystalDecisions.Shared.TableLogOnInfo
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim crExportOptions As ExportOptions
        Dim objRptDs As New DataSet()
        Dim objMapPath As New Object()
        Dim strOpCd_Get As String = ""
        Dim strOpCd_GetLine As String = ""
        Dim strOpCd_GetLocInfo As String = ""
        Dim strOpCodes As String
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strReportName As String
        Dim strPDFName As String
        Dim totalrp As Double
        Dim objRptDs1 As New DataSet

        strOpCd_Get = "PU_STDRPT_PURCHASEORDERPF_HISTORYPRICE_GET"

        If strPONoFrom <> "" Then
            strParam = Trim(strPONoFrom)
        End If

        strReportName = "PU_StdRpt_PurchaseOrderPF.rpt"


        Try
            intErrNo = objPU.mtdGetPOPFDocRpt(strOpCd_Get, _
                                            strParam, _
                                            strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strAccMonth, _
                                            strAccYear, _
                                            objRptDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=&errmesg=" & lblErrMesage.Text & "&redirect=")
        End Try

        If objRptDs.Tables(0).Rows.Count > 0 Then
            If strCatatan = "" Then
                strCatatan = objRptDs.Tables(0).Rows(0).Item("Catatan")
            End If
            For intCnt = 0 To objRptDs.Tables(0).Rows.Count - 1
                'totalrp = Trim(CStr(objRptDs.Tables(0).Rows(intCnt).Item("TotalAmountToReport")))
                'totalrp = Trim(CStr(FormatNumber(objRptDs.Tables(0).Rows(intCnt).Item("TotalAmountToReport"), 2)))
                totalrp = Trim(CStr(FormatNumber(objRptDs.Tables(0).Rows(intCnt).Item("TotalAmountCurrency"), 2)))
                If UCase(Trim(objRptDs.Tables(0).Rows(intCnt).Item("CurrencyCode"))) = "IDR" Then
                    objRptDs.Tables(0).Rows(intCnt).Item("Terbilang") = objGlobal.TerbilangDesimal(totalrp, "Rupiah")
                Else
                    objRptDs.Tables(0).Rows(intCnt).Item("Terbilang") = Replace(objGlobal.ConvertNo2WordsDecimal(FormatNumber(totalrp, 2)), "Zero", "")
                End If
            Next intCnt
        End If

        Try
            intErrNo = objAdmin.mtdGetBasePath(objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=&errmesg=" & lblErrMesage.Text & "&redirect=")
        End Try



        rdCrystalViewer.Load(objMapPath & "Web\EN\PU\Reports\Crystal\" & strReportName, OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        crvView.Visible = False
        crvView.ReportSource = rdCrystalViewer
        crvView.DataBind()

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\PU_Rpt_POPFDet.pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        rdCrystalViewer.Close()
        rdCrystalViewer.Dispose()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../../ftp/PU_Rpt_POPFDet.pdf"">")

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

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("ParamUserLoc")
        paramField2 = paramFields.Item("ParamRptID")
        paramField3 = paramFields.Item("ParamRptName")
        paramField4 = paramFields.Item("ParamPIC1")
        paramField5 = paramFields.Item("ParamPrintedBy")
        paramField6 = paramFields.Item("ParamSelDecimal")
        paramField7 = paramFields.Item("ParamJabatan1")
        paramField8 = paramFields.Item("ParamCatatan")
        paramField9 = paramFields.Item("ParamPIC2")
        paramField10 = paramFields.Item("ParamJabatan2")
        paramField11 = paramFields.Item("ParamLokasi")
        paramField12 = paramFields.Item("ParamSentPeriod")
        paramField13 = paramFields.Item("ParamUserName")
        paramField14 = paramFields.Item("ParamSyaratBayar")

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

        ParamDiscreteValue1.Value = Request.QueryString("Location")
        ParamDiscreteValue2.Value = Request.QueryString("RptID")
        ParamDiscreteValue3.Value = Request.QueryString("RptName")
        ParamDiscreteValue4.Value = Request.QueryString("PIC1")
        ParamDiscreteValue5.Value = Session("SS_USERNAME")
        ParamDiscreteValue6.Value = Request.QueryString("Decimal")
        ParamDiscreteValue7.Value = Request.QueryString("Jabatan1")
        ParamDiscreteValue8.Value = strCatatan 'Request.QueryString("Catatan")
        ParamDiscreteValue9.Value = Request.QueryString("PIC2")
        ParamDiscreteValue10.Value = Request.QueryString("Jabatan2")
        ParamDiscreteValue11.Value = strLokasi 'Request.QueryString("Lokasi")
        ParamDiscreteValue12.Value = strSentPeriod
        ParamDiscreteValue13.Value = strUserName
        ParamDiscreteValue14.Value = ""

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
    End Sub


End Class

