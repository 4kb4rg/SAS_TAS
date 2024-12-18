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
Imports agri.PWSystem.clsLangCap

Imports agri.GlobalHdl.clsGlobalHdl

Public Class PU_StdRpt_POStatementNewPreview : Inherits Page

    Dim objPURpt As New agri.PU.clsReport()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objBITrx As New agri.BI.clsTrx()

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents lblNoRecord As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid
    Protected WithEvents lblErrMesage As Label

    Dim objCompany As New agri.Admin.clsComp()
    Dim objCountryDesc As New agri.Admin.clsCountry()

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
    Dim strPONoFrom As String
    Dim strPONoTo As String
    Dim strPeriodeFrom As String
    Dim strPeriodeTo As String
    Dim strPIC As String
    Dim strJabatan As String
    Dim strSearchExp As String = ""

    Dim strCompanyAddress As String

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
            intSelDecimal= CInt(Request.QueryString("Decimal"))

            strPONoFrom = Trim(Request.QueryString("PONoFrom"))
            strPONoTo = Trim(Request.QueryString("PONoTo"))
            strPeriodeFrom = Trim(Request.QueryString("PeriodeFrom"))
            strPeriodeTo = Trim(Request.QueryString("PeriodeTo"))
            strPIC = Trim(Request.QueryString("PIC"))
            strJabatan = Trim(Request.QueryString("Jabatan"))


   
            BindReport()
        End If
    End Sub


    Sub BindReport()
        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim strOpCd As String = "PU_STDRPT_PO_STATEMENT_PF"
        Dim objRptDs As New Dataset()
        Dim objMapPath As String
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strRptPrefix As String
        Dim intCnt As Integer
        Dim strPOID as String = ""
        Dim Cnt1 as Integer
        Dim Cnt2 as Integer
        Dim Cnt as Integer
        Dim strPOIDCnt as String
        Dim strPeriode as String = ""
        Dim totalrp as Integer
 

        If strPONoFrom = "" And strPONoTo = "" Then
            strPOID = ""
        Else
            strPOID = " and po.POID in ('" & strPONoFrom & "','" & strPONoTo & "') "
        End If
   
        
        If strPeriodeFrom <> "" and strPeriodeTo = "" then  
            strPeriode = " and po.CreateDate = '" & Trim(strPeriodeFrom) & "' "
        ElseIf strPeriodeFrom = "" and strPeriodeTo <> "" then  
            strPeriode = " and po.CreateDate = '" & Trim(strPeriodeTo) & "' "
        ElseIf strPeriodeFrom <> "" and strPeriodeTo <> "" then  
            strPeriode = " and po.CreateDate between '" & Trim(strPeriodeFrom) & "' and '" & Trim(strPeriodeTo) & "'"
        Else
            strPeriode = ""
        end if 

        if strUserLoc = "" then  
            strUserLoc = ""
        else
            strUserLoc = " and po.LocCode in ( '" & Trim(strUserLoc) & "' ) " 
        end if 

        strParam = strPOID & strPeriode & strUserLoc 

        Try
            intErrNo = objPURpt.mtdGetReport_PurchaseOrderPF(strOpCd, _
                                                             strParam, _
                                                             objRptDs, _
                                                             objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PU_STDRPT_PURCHASE_ORDER_PF&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objRptDs.Tables(0).Rows.Count > 0 then 
        For intCnt = 0 To objRptDs.Tables(0).Rows.Count - 1
            totalrp = Trim(CStr(objRptDs.Tables(0).Rows(intCnt).Item("TotalAmount")))
            objRptDs.Tables(0).rows(intcnt).Item("Terbilang") = objGlobal.terbilangdesimal(totalrp, "Rupiah")
        Next intCnt
        end if 

        strRptPrefix = "PU_StdRpt_POStatementNew"

   
        rdCrystalViewer = New ReportDocument()
        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strRptPrefix & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))
        

        crvView.Visible = False                          
        crvView.ReportSource = rdCrystalViewer           
        crvView.DataBind()                               

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
        rdCrystalViewer.Close()
        rdCrystalViewer.Dispose()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strRptPrefix & ".pdf"">")
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

        Dim ParamDiscreteValue1 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue2 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue3 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue4 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue5 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue6 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue7 As New ParameterDiscreteValue()


        Dim crParameterValues1 As ParameterValues
        Dim crParameterValues2 As ParameterValues
        Dim crParameterValues3 As ParameterValues
        Dim crParameterValues4 As ParameterValues
        Dim crParameterValues5 As ParameterValues
        Dim crParameterValues6 As ParameterValues
        Dim crParameterValues7 As ParameterValues

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("ParamUserLoc")
        paramField2 = paramFields.Item("ParamRptID")
        paramField3 = paramFields.Item("ParamRptName")
        paramField4 = paramFields.Item("ParamPIC")
        paramField5 = paramFields.Item("ParamPrintedBy")
        paramField6 = paramFields.Item("ParamSelDecimal")
        paramField7 = paramFields.Item("ParamJabatan")


        crParameterValues1 = paramField1.CurrentValues
        crParameterValues2 = paramField2.CurrentValues
        crParameterValues3 = paramField3.CurrentValues
        crParameterValues4 = paramField4.CurrentValues
        crParameterValues5 = paramField5.CurrentValues
        crParameterValues6 = paramField6.CurrentValues
        crParameterValues7 = paramField7.CurrentValues

        ParamDiscreteValue1.Value = Request.QueryString("Location")
        ParamDiscreteValue2.Value = Request.QueryString("RptID")
        ParamDiscreteValue3.Value = Request.QueryString("RptName")
        ParamDiscreteValue4.Value = Request.QueryString("PIC")
        ParamDiscreteValue5.Value = Session("SS_USERNAME")
        ParamDiscreteValue6.Value = Request.QueryString("Decimal")
        ParamDiscreteValue7.Value = Request.QueryString("Jabatan")

        crParameterValues1.Add(ParamDiscreteValue1)
        crParameterValues2.Add(ParamDiscreteValue2)
        crParameterValues3.Add(ParamDiscreteValue3)
        crParameterValues4.Add(ParamDiscreteValue4)
        crParameterValues5.Add(ParamDiscreteValue5)
        crParameterValues6.Add(ParamDiscreteValue6)
        crParameterValues7.Add(ParamDiscreteValue7)

        PFDefs(0).ApplyCurrentValues(crParameterValues1)
        PFDefs(1).ApplyCurrentValues(crParameterValues2)
        PFDefs(2).ApplyCurrentValues(crParameterValues3)
        PFDefs(3).ApplyCurrentValues(crParameterValues4)
        PFDefs(4).ApplyCurrentValues(crParameterValues5)
        PFDefs(5).ApplyCurrentValues(crParameterValues6)
        PFDefs(6).ApplyCurrentValues(crParameterValues7)

    End Sub

End Class
