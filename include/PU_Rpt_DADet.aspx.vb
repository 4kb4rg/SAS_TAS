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


Public Class PU_Rpt_DADet : Inherits Page

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
    Dim strDispAdvId As String
    Dim strPrintDate As String
    Dim strSortLine As String
    Dim batchPrint As String
    Dim strReprintedID As String
    Dim arrReprintedID As Array
    Dim intCnt2 As Integer

    Dim strCompName As String
    Dim strLocName As String
    Dim strUserName As String

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
        strDispAdvId = Trim(Request.QueryString("strDispAdvId"))
        strPrintDate = Trim(Request.QueryString("strPrintDate"))
        strSortLine = Trim(Request.QueryString("strSortLine"))
        batchPrint = Trim(Request.QueryString("batchPrint"))
        strReprintedID = Trim(Request.QueryString("reprintId"))

        Bind_ITEM(True)

    End Sub

    Sub Bind_ITEM(ByVal blnIsPDFFormat As Boolean)
        Dim crLogonInfo As CrystalDecisions.Shared.TableLogOnInfo
        Dim objRptDs As New Dataset()
        Dim objMapPath As New Object()
        Dim strOpCd_Get As String = ""
        Dim strOpCd_GetLine As String = ""
        Dim strOpCodes As String
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strReportName As String
        Dim strPDFName As String


        strOpCd_Get = "PU_CLSTRX_DISPADV_DETAIL_GET_DOCRPT" & "|" & "DispAdv"
        strOpCd_GetLine = "PU_CLSTRX_DISPADV_DETAIL_LINE_GET_DOCRPT" & "|" & "DispAdvLn"
        'strOpCd_Get = "PU_CLSTRX_DISPATCHADVICEINTERNAL_DETAIL_GET_DOCRPT" & "|" & "DispAdv"
        'strOpCd_GetLine = "PU_CLSTRX_DISPATCHADVICEINTERNAL_DETAIL_LINE_GET_DOCRPT" & "|" & "DispAdvLn"
        strReportName = "PU_Rpt_DispatchAdvice.rpt"

        strOpCodes = strOpCd_Get & chr(9) & strOpCd_GetLine
        strParam = strDispAdvId & "||"

        Try
            intErrNo = objPU.mtdGetDADocRpt(strOpCodes, _
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

        If objRptDs.Tables("DispAdv").Rows.Count > 0 Then
            For intCnt=0 To objRptDs.Tables("DispAdv").Rows.Count - 1
                objRptDs.Tables("DispAdv").Rows(intCnt).Item("DispAdvType") = objPU.mtdGetDAType(CInt(objRptDs.Tables("DispAdv").Rows(intCnt).Item("DispAdvType")))
            Next
        End If


        If objRptDs.Tables(0).Rows.Count > 0 Then
            For intCnt=0 To objRptDs.Tables(0).Rows.Count - 1
                If batchPrint = "yes" Then
                    objRptDs.Tables(0).Rows(intCnt).Item("PrintStatus") = objPU.mtdGetDAStatus(CInt(objRptDs.Tables(0).Rows(intCnt).Item("Status")))
                    
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
                            If Trim(objRptDs.Tables(0).Rows(intCnt).Item("DispAdvID")) = Trim(arrReprintedID(intCnt2)) Then
                                objRptDs.Tables(0).Rows(intCnt).Item("PrintStatus") = objPU.mtdGetDAStatus(CInt(objRptDs.Tables(0).Rows(intCnt).Item("Status"))) & " (re-printed)"                                
                            End If
                        Next
                    Next
                End If
            End If
        End If

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
            DiskOpts.DiskFileName = objMapPath & "web\ftp\PU_Rpt_DispatchAdvice.pdf"
            rdCrystalViewer.ExportOptions.DestinationOptions = DiskOpts
            rdCrystalViewer.Export()
            rdCrystalViewer.Close()
            rdCrystalViewer.Dispose()
            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../../ftp/PU_Rpt_DispatchAdvice.pdf"">")
        End If

    End Sub

    Sub PassParam()
        Dim paramFields As New ParameterFields()
        Dim paramField1 As New ParameterField()
        Dim paramField2 As New ParameterField()
        Dim paramField3 As New ParameterField()
        Dim paramField4 As New ParameterField()

        Dim ParamDiscreteValue1 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue2 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue3 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue4 As New ParameterDiscreteValue()

        Dim crParameterValues1 As ParameterValues
        Dim crParameterValues2 As ParameterValues
        Dim crParameterValues3 As ParameterValues
        Dim crParameterValues4 As ParameterValues

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        ParamFields = crvView.ParameterFieldInfo

        ParamField1 = ParamFields.Item("strPrintedBy")
        ParamField2 = ParamFields.Item("strCompName")
        ParamField3 = ParamFields.Item("strLocName")
        ParamField4 = ParamFields.Item("strStatus")

        crParameterValues1 = ParamField1.CurrentValues
        crParameterValues2 = ParamField2.CurrentValues
        crParameterValues3 = ParamField3.CurrentValues
        crParameterValues4 = ParamField4.CurrentValues

        ParamDiscreteValue1.value = strUserName
        ParamDiscreteValue2.value = strCompName
        ParamDiscreteValue3.value = strLocName
        ParamDiscreteValue4.value = strStatus

        crParameterValues1.Add(ParamDiscreteValue1)
        crParameterValues2.Add(ParamDiscreteValue2)
        crParameterValues3.Add(ParamDiscreteValue3)
        crParameterValues4.Add(ParamDiscreteValue4)

        PFDefs(0).ApplyCurrentValues(crParameterValues1)
        PFDefs(1).ApplyCurrentValues(crParameterValues2)
        PFDefs(2).ApplyCurrentValues(crParameterValues3)
        PFDefs(3).ApplyCurrentValues(crParameterValues4)

        crvView.ParameterFieldInfo = paramFields
    End Sub

End Class

