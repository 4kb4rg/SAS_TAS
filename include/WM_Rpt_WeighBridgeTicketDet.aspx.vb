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

Imports agri.PM.clsSetup
Imports agri.Admin.clsShare
Imports agri.Admin.clsComp
Imports agri.Admin.clsLoc
Imports agri.GlobalHdl.clsGlobalHdl


Public Class WM_Rpt_WeighBridgeTicketDet : inherits page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents lblErrMesage As Label

    Dim objWMTrx As New agri.WM.clsTrx
    Dim objAdmin As New agri.Admin.clsShare
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl
    Dim rdCrystalViewer As New ReportDocument()

    Dim strCompany As String
    Dim strLocation As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strUserId As String
    Dim strLangCode As String

    Dim strCustDocRefNoTag As String
    Dim strCustomerTag As String
    Dim strVehicleTag As String
    Dim strBlockTag As String
    Dim strTicketNo As String

    Public Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strAccMonth = Session("SS_PMACCMONTH")
        strAccYear = Session("SS_PMACCYEAR")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")

        crvView.Visible = False

        strTicketNo = Trim(Request.QueryString("TicketNo"))
        strCustDocRefNoTag = Trim(Request.QueryString("CustDocRefNoTag"))
        strCustomerTag = Trim(Request.QueryString("CustomerTag"))
        strVehicleTag = Trim(Request.QueryString("VehicleTag"))
        strBlockTag = Trim(Request.QueryString("BlockTag"))

        Bind_ITEM(True)
    End Sub

    Sub Bind_ITEM(ByVal blnIsPDFFormat As Boolean)
        Dim objRptDs As New DataSet
        Dim objMapPath As New Object
        Dim strOpCd As String = "WM_CLSTRX_WEIGHBRIDGE_TICKET_GET_RPT"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dc As DataColumn

        strParam = strTicketNo

        Try
            intErrNo = objWMTrx.mtdGetWeighBridgeTicketRpt(strOpCd, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strParam, _
                                                        objRptDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=&errmesg=" & lblErrMesage.Text & "&redirect=")
        End Try

        Try
            intErrNo = objAdmin.mtdGetBasePath(objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=&errmesg=" & lblErrMesage.Text & "&redirect=")
        End Try

        dc = New DataColumn("TransTypeDesc")
        dc.AllowDBNull = True
        dc.DataType = Type.GetType("System.String")
        objRptDs.Tables(0).Columns.Add(dc)

        dc = New DataColumn("ProductDesc")
        dc.AllowDBNull = True
        dc.DataType = Type.GetType("System.String")
        objRptDs.Tables(0).Columns.Add(dc)

        With objRptDs.Tables(0)
            For intCnt = 0 To .Rows.Count - 1
                If Not IsDBNull(.Rows(intCnt).Item("TransType")) AndAlso .Rows(intCnt).Item("TransType") <> "" Then
                    .Rows(intCnt).Item("TransTypeDesc") = objWMTrx.mtdGetWeighBridgeTicketTransType(.Rows(intCnt).Item("TransType"))
                Else
                    .Rows(intCnt).Item("TransTypeDesc") = ""
                End If
                If Not IsDBNull(.Rows(intCnt).Item("ProductCode")) AndAlso .Rows(intCnt).Item("ProductCode") <> "" Then
                    .Rows(intCnt).Item("ProductDesc") = objWMTrx.mtdGetWeighBridgeTicketProduct(.Rows(intCnt).Item("ProductCode"))
                Else
                    .Rows(intCnt).Item("ProductDesc") = ""
                End If
            Next
        End With

        rdCrystalViewer.Load(objMapPath & "Web\EN\WM\Reports\Crystal\WM_Rpt_WeighBridgeTicketDet.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        If Not blnIsPDFFormat Then
            crvView.Visible = True     
            crvView.ReportSource = rdCrystalViewer
            crvView.DataBind()
            PassParam()
        Else
            Dim DiskOpts As CrystalDecisions.Shared.DiskFileDestinationOptions = New CrystalDecisions.Shared.DiskFileDestinationOptions
            DiskOpts.DiskFileName = objMapPath & "web\ftp\WM_Rpt_WeighBridgeTicketDet.pdf"

            crvView.Visible = False
            crvView.ReportSource = rdCrystalViewer
            crvView.DataBind()

            PassParam()

            rdCrystalViewer.ExportOptions.ExportDestinationType = CrystalDecisions.[Shared].ExportDestinationType.DiskFile
            rdCrystalViewer.ExportOptions.ExportFormatType = CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat
            rdCrystalViewer.ExportOptions.DestinationOptions = DiskOpts
            rdCrystalViewer.Export()

            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../../ftp/WM_Rpt_WeighBridgeTicketDet.pdf"">")
        End If
    End Sub


    Sub PassParam()
        Dim paramFields As New ParameterFields
        Dim paramField1 As New ParameterField
        Dim paramField2 As New ParameterField
        Dim paramField3 As New ParameterField
        Dim paramField4 As New ParameterField

        Dim ParamDiscreteValue1 As New ParameterDiscreteValue
        Dim ParamDiscreteValue2 As New ParameterDiscreteValue
        Dim ParamDiscreteValue3 As New ParameterDiscreteValue
        Dim ParamDiscreteValue4 As New ParameterDiscreteValue

        Dim crParameterValues1 As ParameterValues
        Dim crParameterValues2 As ParameterValues
        Dim crParameterValues3 As ParameterValues
        Dim crParameterValues4 As ParameterValues

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("CustDocRefNoTag")
        paramField2 = paramFields.Item("CustomerTag")
        paramField3 = paramFields.Item("VehicleTag")
        paramField4 = paramFields.Item("BlockTag")

        crParameterValues1 = paramField1.CurrentValues
        crParameterValues2 = paramField2.CurrentValues
        crParameterValues3 = paramField3.CurrentValues
        crParameterValues4 = paramField4.CurrentValues

        ParamDiscreteValue1.Value = strCustDocRefNoTag
        ParamDiscreteValue2.Value = strCustomerTag
        ParamDiscreteValue3.Value = strVehicleTag
        ParamDiscreteValue4.Value = strBlockTag

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

