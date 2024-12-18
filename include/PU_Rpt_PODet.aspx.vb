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


Public Class PU_Rpt_PODet : Inherits Page

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

    Dim strPIC1 As String
    Dim strJabatan1 As String
    Dim strPIC2 As String
    Dim strJabatan2 As String
    Dim strCatatan As String
    Dim strLokasi As String

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

        strStatus = Trim(Request.QueryString("strStatus"))
        strPOId = Trim(Request.QueryString("strPOId"))
        strPrintDate = Trim(Request.QueryString("strPrintDate"))
        strSortLine = Trim(Request.QueryString("strSortLine"))
        batchPrint = Trim(Request.QueryString("batchPrint"))
        strReprintedID = Trim(Request.QueryString("reprintId"))
        strPIC1 = Trim(Request.QueryString("PIC1"))
        strJabatan1 = Trim(Request.QueryString("Jabatan1"))
        strPIC2 = Trim(Request.QueryString("PIC2"))
        strJabatan2 = Trim(Request.QueryString("Jabatan2"))
        strCatatan = Trim(Request.QueryString("Catatan"))
        strLokasi = Trim(Request.QueryString("Lokasi"))
        Bind_ITEM(True)

    End Sub

    Sub Bind_ITEM(ByVal blnIsPDFFormat As Boolean)
        Dim crLogonInfo As CrystalDecisions.Shared.TableLogOnInfo
        Dim objRptDs As New Dataset()
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


        strOpCd_Get = "PU_CLSTRX_PO_GET_FOR_DOCRPT" & "|" & "PurchaseOrder"
        strOpCd_GetLine = "PU_CLSTRX_PO_LINE_GET_FOR_DOCRPT" & "|" & "PurchaseOrderLn"
        strOpCd_GetLocInfo = "PU_CLSTRX_PO_GET_LOCATIONINFO_FOR_DOCRPT" & "|" & "LocInfo" 
        strReportName = "PU_Rpt_PODet.rpt"

        strOpCodes = strOpCd_Get & Chr(9) & strOpCd_GetLine & Chr(9) & strOpCd_GetLocInfo 
        strParam = strPOId & "||"

        Try
            intErrNo = objPU.mtdGetPODocRpt(strOpCodes, _
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

        If objRptDs.Tables("PurchaseOrder").Rows.Count > 0 Then
            For intCnt=0 To objRptDs.Tables("PurchaseOrder").Rows.Count - 1
                objRptDs.Tables("PurchaseOrder").Rows(intCnt).Item("POType") = objPU.mtdGetPOType(CInt(objRptDs.Tables("PurchaseOrder").Rows(intCnt).Item("POType")))
            Next
        End If

        If objRptDs.Tables(0).Rows.Count > 0 Then
            For intCnt=0 To objRptDs.Tables(0).Rows.Count - 1
                If batchPrint = "yes" Then
                    objRptDs.Tables(0).Rows(intCnt).Item("PrintStatus") = objPU.mtdGetPOStatus(CInt(objRptDs.Tables(0).Rows(intCnt).Item("Status")))
                    
                Else
                    objRptDs.Tables(0).Rows(intCnt).Item("PrintStatus") = strStatus
                End If
            Next
        End If

        If objRptDs.Tables(0).Rows.Count > 0 Then
            If batchPrint = "yes" Then
                If InStr(strReprintedID, "|") <> 0 Then
                    arrReprintedID = Split(strReprintedID, "|")
                    For intCnt=0 To objRptDs.Tables(0).Rows.Count - 1
                        For intCnt2=0 To UBound(arrReprintedID)
                            If Trim(objRptDs.Tables(0).Rows(intCnt).Item("POID")) = Trim(arrReprintedID(intCnt2)) Then
                                objRptDs.Tables(0).Rows(intCnt).Item("PrintStatus") = objPU.mtdGetPOStatus(CInt(objRptDs.Tables(0).Rows(intCnt).Item("Status"))) & " (re-printed)"                                
                            End If
                        Next
                    Next
                End If
            End If
        End If

        If objRptDs.Tables(0).Rows.Count > 0 then 
            For intCnt = 0 To objRptDs.Tables(0).Rows.Count - 1
                totalrp = Trim(CStr(objRptDs.Tables(0).Rows(intCnt).Item("TotalAmountToReport")))                

                If Ucase(Trim(objRptDs.Tables(0).Rows(intCnt).Item("CurrencyCode"))) = "IDR" Then
                    objRptDs.Tables(0).Rows(intCnt).Item("Terbilang") = objGlobal.TerbilangDesimal(totalrp, "")
                Else
                    objRptDs.Tables(0).Rows(intCnt).Item("Terbilang") = objGlobal.ConvertNo2WordsDecimal(totalRp)
                End If
            Next intCnt
        End if 

        Try
            intErrNo = objAdmin.mtdGetBasePath(objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=&errmesg=" & lblErrMesage.Text & "&redirect=")
        End Try


        rdCrystalViewer.Load(objMapPath & "Web\EN\PU\Reports\Crystal\" & strReportName, OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs)

        If Not blnIsPDFFormat Then
            crvView.Visible = True     
            crvView.ReportSource = rdCrystalViewer
            crvView.DataBind()
            PassParam()
        Else
            crvView.Visible = False     
            crvView.ReportSource = rdCrystalViewer
            crvView.DataBind()
            PassParam()

            Dim DiskOpts As CrystalDecisions.Shared.DiskFileDestinationOptions = New CrystalDecisions.Shared.DiskFileDestinationOptions()
            rdCrystalViewer.ExportOptions.ExportDestinationType = CrystalDecisions.[Shared].ExportDestinationType.DiskFile
            rdCrystalViewer.ExportOptions.ExportFormatType = CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat
            DiskOpts.DiskFileName = objMapPath & "web\ftp\PU_Rpt_PODet.pdf"
            rdCrystalViewer.ExportOptions.DestinationOptions = DiskOpts
            rdCrystalViewer.Export()

            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../../ftp/PU_Rpt_PODet.pdf"">")
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

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        ParamFields = crvView.ParameterFieldInfo
     
        ParamField1 = ParamFields.Item("strPrintDate")  
        ParamField2 = ParamFields.Item("strPrintedBy")
        paramField3 = paramFields.Item("strLocNameUC") 
        paramField4 = paramFields.Item("strLocName")
        ParamField5 = ParamFields.Item("strStatus")
        paramField6 = paramFields.Item("ParamPIC1")
        paramField7 = paramFields.Item("ParamJabatan1")
        ParamField8 = ParamFields.Item("ParamCatatan")
        paramField9 = paramFields.Item("ParamPIC2")
        paramField10 = paramFields.Item("ParamJabatan2")
        paramField11 = paramFields.Item("ParamLokasi")

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
        crParameterValues11 = ParamField11.CurrentValues

        ParamDiscreteValue1.value = strPrintDate
        ParamDiscreteValue2.value = strUserName
        ParamDiscreteValue3.value = UCase(strLocName) 
        ParamDiscreteValue4.value = strLocName
        ParamDiscreteValue5.value = strStatus
        ParamDiscreteValue6.value = strPIC1
        ParamDiscreteValue7.value = strJabatan1
        ParamDiscreteValue8.value = strCatatan
        ParamDiscreteValue9.value = strPIC2
        ParamDiscreteValue10.value = strJabatan2
        ParamDiscreteValue11.value = strLokasi

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

        crvView.ParameterFieldInfo = paramFields
    End Sub



End Class

