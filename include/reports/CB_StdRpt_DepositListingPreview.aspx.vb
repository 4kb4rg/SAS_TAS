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

Public Class CB_StdRpt_DepositListing_Preview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid
   

    Dim objCB As New agri.CB.clsReport()
    Dim objCBTrx As New agri.CB.clsTrx()
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

    Dim strStatus As String
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
        strFileName = "CB_StdRpt_DepositListing"

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_CBACCMONTH")
        strAccYear = Session("SS_CBACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        strPhyMonth = Session("SS_PHYMONTH")
        strPhyYear = Session("SS_PHYYEAR")

        Session("SS_LBLLOCATION") = Request.QueryString("lblLocation")
        Session("SS_Decimal") = Request.QueryString("Decimal")
        Session("SS_RptID") = Request.QueryString("RptID")
        Session("SS_RptName") = Request.QueryString("RptName")
        Session("SS_AccMonth") = Request.QueryString("DDLAccMth")
        Session("SS_AccYear") = Request.QueryString("DDLAccYr")
     
        Session("SS_AccCode") = Request.QueryString("txtCOACode")
        Session("SS_Currency") = Request.QueryString("txtCurrency")
        Session("SS_Status") = Request.QueryString("txtStatus")
        Session("SS_Type") = Request.QueryString("txtType")
        Session("SS_BankCode") = Request.QueryString("txtBankCode")
        Session("SS_Description") = Request.QueryString("txtDescription")
        Session("SS_DepositCodeFrom") = Request.QueryString("txtDepositCodeFrom")
        Session("SS_DepositCodeTo") = Request.QueryString("txtDepositCodeTo")  

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

        Dim strOpCode As String = "CB_STDRPT_DEPOSITLISTING_GET"
        Dim SearchStr As String
        Dim SQLStr As String
        Dim objFTPFolder As String
        Dim intCnt As Integer
       
        If Not (Request.QueryString("txtDepositCodeFrom") = "" And Request.QueryString("txtDepositCodeTo") = "") Then SearchStr = SearchStr & " AND DEP.DepositCode >= '" & Request.QueryString("txtDepositCodeFrom") & "' AND  DEP.DepositCode <= '" & Request.QueryString("txtDepositCodeTo") & "'"
        If Not Request.QueryString("txtDescription") = "" Then SearchStr = SearchStr & " AND DEP.Description LIKE '%" & Request.QueryString("txtDescription") & "%' "
        If Not Request.QueryString("txtBankCode") = "" Then SearchStr = SearchStr & " AND DEP.BankCode = '" & Request.QueryString("txtBankCode") & "' "
        If Not Request.QueryString("txtType") = "0" Then SearchStr = SearchStr & " AND DEP.Type = '" & Request.QueryString("txtType") & "' "
        If Not Request.QueryString("txtStatus") = "0" Then SearchStr = SearchStr & " AND DEP.Status = '" & Request.QueryString("txtStatus") & "' "
        If Not Request.QueryString("txtCurrency") = "" Then SearchStr = SearchStr & " AND DEP.Currency  = '" & Request.QueryString("txtCurrency") & "' "
        If Not Request.QueryString("txtCOACode") = "" Then SearchStr = SearchStr & " AND DEP.AccCode  = '" & Request.QueryString("txtCOACode") & "' "

        strParam = strUserLoc & "|" & strDDLAccMth & "|" & strDDLAccYr & "|" & strStatus & "|" & SearchStr
        Try
            intErrNo = objCB.mtdGetReport_DepositListing(strOpCode, strParam, objRptDs, objMapPath, objFTPFolder)
        Catch Exp As Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=CB_StdRpt_DepositListing&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strFileName & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3

        crvView.Visible = False
        crvView.ReportSource = rdCrystalViewer          
        crvView.DataBind()                              

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        'crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strFileName & ".pdf"
        crDiskFileDestinationOptions.DiskFileName = objFTPFolder & strFileName & ".pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        rdCrystalViewer.Close()
        rdCrystalViewer.Dispose()

        Dim strUrl As String
        strUrl = objFTPFolder.Substring(InStr(objFTPFolder, "ftp") - 1, Len(objFTPFolder) - InStr(objFTPFolder, "ftp") + 1)
        strUrl = Replace(strUrl, "\", "/")
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../" & strUrl & strFileName & ".pdf"">")
        'Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strFileName & ".pdf"">")

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
        
        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("ParamLocation")
        paramField2 = paramFields.Item("ParamStatus")
        paramField3 = paramFields.Item("ParamCompanyName")
        paramField4 = paramFields.Item("ParamUserName")
        paramField5 = paramFields.Item("ParamDecimal")
        paramField6 = paramFields.Item("ParamRptID")
        paramField7 = paramFields.Item("ParamRptName")
        paramField8 = paramFields.Item("ParamAccMonth")
        paramField9 = paramFields.Item("ParamAccYear")
        paramField10 = paramFields.Item("ParamDepositCodeFrom")
        paramField11 = paramFields.Item("ParamDepositCodeTo")
        paramField12 = paramFields.Item("ParamDescription")
        paramField13 = paramFields.Item("ParamBankCode")
        paramField14 = paramFields.Item("ParamType")
        paramField15 = paramFields.Item("ParamCurrency")
        paramField16 = paramFields.Item("ParamCOACode")
        
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
        
        ParamDiscreteValue1.Value = Session("SS_LOC")
        ParamDiscreteValue2.Value = Session("SS_Status")
        ParamDiscreteValue3.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue4.Value = Session("SS_USERNAME")
        ParamDiscreteValue5.Value = Session("SS_DECIMAL")
        ParamDiscreteValue6.Value = Session("SS_RPTID")
        ParamDiscreteValue7.Value = Session("SS_RPTNAME")
        ParamDiscreteValue8.Value = Session("SS_ACCMONTH")
        ParamDiscreteValue9.Value = Session("SS_ACCYEAR")
        ParamDiscreteValue10.Value = Session("SS_DepositCodeFrom")
        ParamDiscreteValue11.Value = Session("SS_DepositCodeTo")
        ParamDiscreteValue12.Value = Session("SS_Description")
        ParamDiscreteValue13.Value = Session("SS_BankCode")
        ParamDiscreteValue14.Value = Session("SS_Type")
        ParamDiscreteValue15.Value = Session("SS_Currency")
        ParamDiscreteValue16.Value = Session("SS_AccCode")
        
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
        
        crvView.ParameterFieldInfo = paramFields

    End Sub

End Class
