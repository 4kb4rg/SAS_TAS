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

Public Class AR_StdRpt_ReceiptList_Preview : Inherits Page
                         
    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objAR As New agri.BI.clsReport()
    Dim objARTrx As New agri.BI.clsTrx()
    Dim objPUSetup As New agri.PU.clsSetup()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdminComp As New agri.Admin.clsComp()
    Dim objAdminCty As New agri.Admin.clsCountry()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strUserLoc As String
    Dim strPhyMonth As String
    Dim strPhyYear As String

    Dim strStatus as String
    Dim tempLoc As String
    Dim strDDLAccMth As String
    Dim strDDLAccYr As String
    Dim strParam As String
    Dim strDate As String
    Dim strSuppCode As String
    Dim objMapPath As String
    Dim arrUserLoc As Array
    Dim intCntUserLoc As Integer
    Dim strLocCode As String
    Dim strOrderBy As String
    Dim strFileName As String
    Dim strSelPhyMonth As String
    Dim strSelPhyYear As String
    Dim objDsAgeingYTDAccPeriod As String

    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer

    Dim dsTrx As New DataSet()
    Dim dsStmtTrx As New DataSet()
    Dim dsComp As New DataSet()


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        crvView.Visible = False
        strStatus = Request.QueryString("ddlStatus")

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

        strFileName = "AR_StdRpt_ReceiptListing"

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_ARACCMONTH")
        strAccYear = Session("SS_ARACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        strPhyMonth = Session("SS_PHYMONTH")
        strPhyYear = Session("SS_PHYYEAR")

        Session("SS_Decimal") = Request.QueryString("Decimal")
        Session("SS_RptID") = Request.QueryString("RptID")
        Session("SS_RptName") = Request.QueryString("RptName")
        Session("SS_AccMonth") = Request.QueryString("DDLAccMth")
        Session("SS_AccYear") = Request.QueryString("DDLAccYr")

        Session("SS_ReceiptIDFrom") = Request.QueryString("ReceiptIDFrom")
        Session("SS_ReceiptIDTo") = Request.QueryString("ReceiptIDTo")
        Session("SS_lblBillParty") = Request.QueryString("lblBillParty")
        Session("SS_BillParty") = Request.QueryString("BillParty")
        Session("SS_BankCode") = Request.QueryString("BankCode")
        Session("SS_ChequeNo") = Request.QueryString("ChequeNo")
        Session("SS_Status") = objARTrx.mtdGetReceiptStatus(Trim(Request.QueryString("Status")))
        Session("SS_lblLocation") = Request.QueryString("lblLocation")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else

            BindReport()
        End If
    End Sub



    Sub BindReport()

        Dim objRptDs As New DataSet()

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOpCd_ReceiptList_GET As String = "AR_STDRPT_RECEIPT_LIST_GET"
        Dim SearchStr As String
        Dim SQLStr As String

        Dim intCnt As Integer

        SearchStr = ""
        If Not (Request.QueryString("ReceiptIDFrom") = "" AND Request.QueryString("ReceiptIDTo") = "") Then 
            SearchStr = SearchStr & " AND R.ReceiptID >='" & Replace(Request.QueryString("ReceiptIDFrom"), "'", "''") & "' AND R.ReceiptID <='" & Replace(Request.QueryString("ReceiptIDTo"), "'", "''") & "' "
        End If
        If Not Request.QueryString("BillParty") = "" Then 
            SearchStr = SearchStr & " AND R.BillPartyCode LIKE '" & Replace(Request.QueryString("BillParty"), "'", "''") & "' "
        End If
        If Not Request.QueryString("BankCode") = "" Then 
            SearchStr = SearchStr & " AND R.BankCode LIKE '" & Replace(Request.QueryString("BankCode"), "'", "''") & "' "
        End If
        If Not Request.QueryString("ChequeNo") = "" Then 
            SearchStr = SearchStr & " AND R.ChequeNo LIKE '" & Replace(Request.QueryString("ChequeNo"), "'", "''") & "' "
        End If
        If Not Request.QueryString("Status") = "0" Then 
            SearchStr = SearchStr & " AND R.Status LIKE '" & Request.QueryString("Status") & "' "
        End If

        strParam =  strUserLoc & "|" & strDDLAccMth & "|" & strDDLAccYr & "|" & SearchStr
        Try
            intErrNo = objAR.mtdGetReport_ReceiptList(strOpCd_ReceiptList_GET, strParam, objRptDs, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=AR_STDRPT_ReceiptList&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        
        For intCnt = 0 To objRptDs.Tables(0).Rows.Count - 1
            If LCase(TypeName(objRptDs.Tables(0).Rows(intCnt).Item("DocType"))) = "string" Then
                objRptDs.Tables(0).Rows(intCnt).Item("DocType") = objGlobal.mtdGetDocName(Trim(objRptDs.Tables(0).Rows(intCnt).Item("DocType")))
            End If
            objRptDs.Tables(0).Rows(intCnt).Item("Status") = objARTrx.mtdGetReceiptStatus(Trim(objRptDs.Tables(0).Rows(intCnt).Item("Status")))
        Next
        

        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strFileName & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        crvView.Visible = False                         
        crvView.ReportSource = rdCrystalViewer          
        crvView.DataBind()                              

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strFileName & ".pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strFileName & ".pdf"">")
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
        
        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("ParamLocation")
        paramField2 = paramFields.Item("ParamCompanyName")
        paramField3 = paramFields.Item("ParamUserName")
        paramField4 = paramFields.Item("ParamDecimal")
        paramField5 = paramFields.Item("ParamRptID")
        paramField6 = paramFields.Item("ParamRptName")
        paramField7 = paramFields.Item("ParamAccMonth")
        paramField8 = paramFields.Item("ParamAccYear")
        paramField9 = paramFields.Item("ParamlblBillParty")
        paramField10 = paramFields.Item("ParamReceiptIDFrom")
        paramField11 = paramFields.Item("ParamReceiptIDTo")
        paramField12 = paramFields.Item("ParamBillParty")
        paramField13 = paramFields.Item("ParamBankCode")
        paramField14 = paramFields.Item("ParamChequeNo")
        paramField15 = paramFields.Item("ParamStatus")
        paramField16 = paramFields.Item("ParamlblLocation")
        paramField17 = paramFields.Item("ParamlblCOACode")
        paramField18 = paramFields.Item("ParamlblBlkCode")
        
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
        
        ParamDiscreteValue1.Value = Session("SS_LOC")
        ParamDiscreteValue2.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue3.Value = Session("SS_USERNAME")
        ParamDiscreteValue4.Value = Session("SS_DECIMAL")
        ParamDiscreteValue5.Value = Session("SS_RPTID")
        ParamDiscreteValue6.Value = Session("SS_RPTNAME")
        ParamDiscreteValue7.Value = Session("SS_ACCMONTH")
        ParamDiscreteValue8.Value = Session("SS_ACCYEAR")
        ParamDiscreteValue9.Value = Session("SS_lblBillParty")
        ParamDiscreteValue10.Value = Session("SS_ReceiptIDFrom")
        ParamDiscreteValue11.Value = Session("SS_ReceiptIDTo")
        ParamDiscreteValue12.Value = Session("SS_BillParty")
        ParamDiscreteValue13.Value = Session("SS_BankCode")
        ParamDiscreteValue14.Value = Session("SS_ChequeNo")
        ParamDiscreteValue15.Value = Session("SS_Status")
        ParamDiscreteValue16.Value = Session("SS_lblLocation")
        ParamDiscreteValue17.Value = Trim(Request.QueryString("lblCOA"))
        ParamDiscreteValue18.Value = Trim(Request.QueryString("lblBlkCode"))
        
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
        
        crvView.ParameterFieldInfo = paramFields

    End Sub    

End Class
