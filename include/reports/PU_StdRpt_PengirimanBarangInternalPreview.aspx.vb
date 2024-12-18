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

Public Class PU_StdRpt_PengirimanBarangInternalPreview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objPU As New agri.PU.clsReport()
    Dim objPUTrx As New agri.PU.clsTrx()

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
    Dim strBlkType As String

    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer

    Dim strDANoFrom As String
    Dim strDANoTo As String
    Dim strPeriodeFrom As String
    Dim strPeriodeTo As String
    Dim strSender As String
    Dim strExpedition As String
    Dim strReceiver As String
    Dim strRemarks As String
    Dim strExportToExcel As String

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
        strBlkType = Request.QueryString("BlkType")

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_PUACCMONTH")
        strAccYear = Session("SS_PUACCYEAR")
        strLangCode = Session("SS_LANGCODE")

        strDANoFrom = Request.QueryString("DANoFrom")
        strDANoTo  = Request.QueryString("DANoTo")
        strPeriodeFrom = Request.QueryString("PeriodeFrom")
        strPeriodeTo = Request.QueryString("PeriodeTo")
        strSender  = Request.QueryString("Sender")
        strExpedition  = Request.QueryString("Expedition")
        strReceiver  = Request.QueryString("Receiver")
        strRemarks = Request.QueryString("Remarks")
        strExportToExcel = Trim(Request.QueryString("ExportToExcel"))


        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            BindReport()
        End If
    End Sub

    Sub BindReport()

        Dim objRptDs As New DataSet()
        Dim objMapPath As String
        Dim objItem As New DataSet()
        Dim objPOID As New DataSet()

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOpCd as String = "PU_STDRPT_DISPATCH_ADVICE"


        Dim strDANo as String = ""
        Dim strPeriode as String = ""
        Dim Cnt1 as integer
        Dim Cnt2 as integer
        Dim intCnt as integer
        Dim strDANoCnt as String
        Dim strExistComp as String
        Dim strParam as String


        if strDANoFrom <> ""  or strDANoTo <> "" then 
            Cnt1 = CInt(right(strDANoFrom, 4))
            Cnt2 = CInt(right(strDANoTo, 4))
            strDANo = right(strDANoFrom, 4)
        end if 

        If strDANo = "" then 
            strDANo = ""
            response.write("test")
        Else
            For intCnt = Cnt1 to (Cnt2 - 1)
                strDANoCnt = CStr(intCnt + 1)
                If len(strDANoCnt) = 1 then 
                    strDANoCnt = "000" & strDANoCnt
                elseif len(strDANoCnt) = 2 then
                    strDANoCnt = "00" & strDANoCnt
                elseif len(strDANoCnt) = 3 then
                    strDANoCnt = "0" & strDANoCnt
                elseif len(strDANoCnt) = 4 then
                    strDANoCnt = strDANoCnt
                End If
                strDANo = strDANo & "', '" &  strDANoCnt
            Next
            strDANo = " and right(rtrim(da.DispAdvId),4) in ( '" & Trim(strDANo) & "' )"
        End If

        If strPeriode = "" then  
            strPeriode = "" 
        else 
            strPeriode = " and da.CreateDate between '" & Trim(strPeriodeFrom) & "' and '" & Trim(strPeriodeTo) & "'"
        end if 

        if strUserLoc = "" then  
            strUserLoc = ""
        else
            strUserLoc = " and da.LocCode in ( '" & Trim(strUserLoc) & "' ) " 
        end if 


        strParam = strDANo & strPeriode & strExistComp & " order by da.DispAdvId "

        Try
            intErrNo = objPU.mtdGetReport_InternalSend(strOpCd, strParam, objRptDs, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_STDRPT_PENGIRIMAN_BARANG_INTERNAL&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
   

        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\PU_StdRpt_PengirimanBarangInternal.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        crvView.Visible = False                         
        crvView.ReportSource = rdCrystalViewer          
        crvView.DataBind()                              

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()

        If strExportToExcel = "0" Then
            crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\PU_StdRpt_PengirimanBarangInternal.pdf"
        Else
            crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\PU_StdRpt_PengirimanBarangInternal.xls"
        End If

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        rdCrystalViewer.Close()
        rdCrystalViewer.Dispose()

        If strExportToExcel = "0" Then
            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/PU_StdRpt_PengirimanBarangInternal.pdf"">")
        Else
            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/PU_StdRpt_PengirimanBarangInternal.xls"">")
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
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("ParamLocation")
        paramField2 = paramFields.Item("ParamRptID")
        paramField3 = paramFields.Item("ParamRptName")
        paramField4 = paramFields.Item("ParamCompanyName")
        paramField5 = paramFields.Item("ParamUserName")
        paramField6 = paramFields.Item("ParamDecimal")
        paramField7 = paramFields.Item("ParamSender")
        paramField8 = paramFields.Item("ParamExpedition")
        paramField9 = paramFields.Item("ParamReceiver")
        paramField10 = paramFields.Item("ParamRemarks")


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

        ParamDiscreteValue1.Value = Session("SS_LOC")
        ParamDiscreteValue2.Value = Request.QueryString("RptID")
        ParamDiscreteValue3.Value = Request.QueryString("RptName")
        ParamDiscreteValue4.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue5.Value = Session("SS_USERNAME")
        ParamDiscreteValue6.Value = Request.QueryString("Decimal")
        ParamDiscreteValue7.Value = Request.QueryString("Sender")
        ParamDiscreteValue8.Value = Request.QueryString("Expedition")
        ParamDiscreteValue9.Value = Request.QueryString("Receiver")
        ParamDiscreteValue10.Value = Request.QueryString("Remarks")

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

    End Sub
End Class
