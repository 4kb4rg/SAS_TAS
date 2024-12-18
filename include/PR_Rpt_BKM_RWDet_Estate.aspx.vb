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


Public Class PR_Rpt_BKM_RWDet_Estate : Inherits Page

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
    Dim strGoodsRcvId As String
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
        strGoodsRcvId = Trim(Request.QueryString("strGoodsRcvId"))
        strPrintDate = Trim(Request.QueryString("strPrintDate"))
        strSortLine = Trim(Request.QueryString("strSortLine"))
        batchPrint = Trim(Request.QueryString("batchPrint"))
        strReprintedID = Trim(Request.QueryString("reprintId"))

        Bind_ITEM(True)

    End Sub

    Sub Bind_ITEM(ByVal blnIsPDFFormat As Boolean)
        Dim crLogonInfo As CrystalDecisions.Shared.TableLogOnInfo
        Dim objRptDs As New DataSet()
        Dim objMapPath As New Object()
        Dim strOpCd_Get As String = ""
        Dim strOpCd_GetLine As String = ""
        Dim strOpCodes As String
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strReportName As String
        Dim strPDFName As String

        'strOpCd_Get = "PU_CLSTRX_GOODSRECEIVE_DETAIL_GET_DOCRPT" & "|" & "GoodsRcv"
        'strOpCd_GetLine = "PU_CLSTRX_GOODSRECEIVE_DETAIL_LINE_GET_DOCRPT" & "|" & "GoodsRcvLn"
        strReportName = "PR_Rpt_BKM_RW_Estate.rpt"

        'strOpCodes = strOpCd_Get & Chr(9) & strOpCd_GetLine
        'strParam = strGoodsRcvId & "||"

        'Try
        '    intErrNo = objPU.mtdGetGRDocRpt(strOpCodes, _
        '                                     strParam, _
        '                                     strCompany, _
        '                                     strLocation, _
        '                                     strUserId, _
        '                                     strAccMonth, _
        '                                     strAccYear, _
        '                                     objRptDs)
        'Catch Exp As System.Exception
        '    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=&errmesg=" & lblErrMesage.Text & "&redirect=")
        'End Try

        'If objRptDs.Tables("GoodsRcv").Rows.Count > 0 Then
        '    For intCnt = 0 To objRptDs.Tables("GoodsRcv").Rows.Count - 1
        '        objRptDs.Tables("GoodsRcv").Rows(intCnt).Item("DispAdvType") = objPU.mtdGetDAType(CInt(objRptDs.Tables("DispAdv").Rows(intCnt).Item("DispAdvType")))
        '    Next
        'End If


        'If objRptDs.Tables(0).Rows.Count > 0 Then
        '    For intCnt = 0 To objRptDs.Tables(0).Rows.Count - 1
        '        If batchPrint = "yes" Then
        '            objRptDs.Tables(0).Rows(intCnt).Item("PrintStatus") = objPU.mtdGetGRStatus(CInt(objRptDs.Tables(0).Rows(intCnt).Item("Status")))
        '        Else
        '            objRptDs.Tables(0).Rows(intCnt).Item("PrintStatus") = strStatus
        '        End If
        '    Next
        'End If

        'If objRptDs.Tables(0).Rows.Count > 0 Then
        '    If batchPrint = "yes" Then
        '        If InStr(strReprintedID, "|") <> 0 Then
        '            arrReprintedID = Split(strReprintedID, "|")
        '            For intCnt = 0 To objRptDs.Tables(0).Rows.Count - 1
        '                For intCnt2 = 0 To UBound(arrReprintedID)
        '                    If Trim(objRptDs.Tables(0).Rows(intCnt).Item("GoodsRcvID")) = Trim(arrReprintedID(intCnt2)) Then
        '                        objRptDs.Tables(0).Rows(intCnt).Item("PrintStatus") = objPU.mtdGetDAStatus(CInt(objRptDs.Tables(0).Rows(intCnt).Item("Status"))) & " (re-printed)"
        '                    End If
        '                Next
        '            Next
        '        End If
        '    End If
        'End If

        Try
            intErrNo = objAdmin.mtdGetBasePath(objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=&errmesg=" & lblErrMesage.Text & "&redirect=")
        End Try


        rdCrystalViewer.Load(objMapPath & "Web\EN\PR\Reports\Crystal\" & strReportName, OpenReportMethod.OpenReportByTempCopy)
        'rdCrystalViewer.SetDataSource(objRptDs)

        If Not blnIsPDFFormat Then
            crvView.Visible = True
            crvView.ReportSource = rdCrystalViewer
            crvView.DataBind()
            'PassParam()
        Else
            crvView.Visible = False
            crvView.ReportSource = rdCrystalViewer
            crvView.DataBind()
            'PassParam()
            Dim DiskOpts As CrystalDecisions.Shared.DiskFileDestinationOptions = New CrystalDecisions.Shared.DiskFileDestinationOptions()
            rdCrystalViewer.ExportOptions.ExportDestinationType = CrystalDecisions.[Shared].ExportDestinationType.DiskFile
            rdCrystalViewer.ExportOptions.ExportFormatType = CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat
            DiskOpts.DiskFileName = objMapPath & "web\ftp\PR_Rpt_BKM_RW_Estate.pdf"
            rdCrystalViewer.ExportOptions.DestinationOptions = DiskOpts
            rdCrystalViewer.Export()

            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../../ftp/PR_Rpt_BKM_RW_Estate.pdf"">")
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
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("strPrintedBy")
        paramField2 = paramFields.Item("strCompName")
        paramField3 = paramFields.Item("strLocName")
        paramField4 = paramFields.Item("strStatus")

        crParameterValues1 = paramField1.CurrentValues
        crParameterValues2 = paramField2.CurrentValues
        crParameterValues3 = paramField3.CurrentValues
        crParameterValues4 = paramField4.CurrentValues

        ParamDiscreteValue1.Value = strUserName
        ParamDiscreteValue2.Value = strCompName
        ParamDiscreteValue3.Value = strLocName
        ParamDiscreteValue4.Value = strStatus

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

