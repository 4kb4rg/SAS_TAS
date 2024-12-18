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

Public Class CB_StdRpt_PaymentListing_Preview : Inherits Page

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
    Dim strInvRcvTag As String  
    Dim strExportToExcel As String

    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer

    Dim dsTrx As New DataSet()
    Dim dsStmtTrx As New DataSet()
    Dim dsComp As New DataSet()

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)


        crvView.Visible = False

        strStatus = Request.QueryString("ddlStatus")
        strInvRcvTag = Request.QueryString("lblInvRcv")
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

        Session("SS_lblAccCode") = Request.QueryString("lblCOACode")
        Session("SS_LBLLOCATION") = Request.QueryString("lblLocation")
        Session("SS_Decimal") = Request.QueryString("Decimal")
        Session("SS_RptID") = Request.QueryString("RptID")
        Session("SS_RptName") = Request.QueryString("RptName")
        Session("SS_AccMonth") = Request.QueryString("DDLAccMth")
        Session("SS_AccYear") = Request.QueryString("DDLAccYr")
        Session("SS_AccCode") = Request.QueryString("lblCOACode")
        Session("SS_Status") = Request.QueryString("txtStatus")
        Session("SS_PaymentIDFrom") = Request.QueryString("txtPaymentIDFrom")
        Session("SS_PaymentIDTo") = Request.QueryString("txtPaymentIDTo")
        Session("SS_SuppCode") = Request.QueryString("txtSuppCode")
        Session("SS_COACode") = Request.QueryString("txtCOACode")
        Session("SS_PaymentType") = Request.QueryString("txtPaymentType")
        Session("SS_BankCode") = Request.QueryString("txtBankCode")
        Session("SS_ChequeNo") = Request.QueryString("txtChequeNo")
        Session("SS_DocumentID") = Request.QueryString("txtDocumentID")
        Session("SS_DateFrom") = Request.QueryString("DateFrom")
        Session("SS_DateTo") = Request.QueryString("DateTo")
        Session("SS_VerifiedBy") = Request.QueryString("VerifiedBy")
        Session("SS_SerahTerima") = Request.QueryString("SerahTerima")
        strExportToExcel = Trim(Request.QueryString("ExportToExcel"))

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

        Dim strOpCd_PaymentListing_GET As String = "CB_STDRPT_PAYMENTLISTING_GET"
        Dim SearchStr As String
        Dim SQLStr As String
        Dim PayType as string

        Dim intCnt As Integer
        Dim objFTPFolder As String

        If Not (Request.QueryString("txtPaymentIDFrom") = "" And Request.QueryString("txtPaymentIDFromIDTo") = "") Then SearchStr = SearchStr & " AND APP.PaymentID >= '" & Request.QueryString("txtPaymentIDFrom") & "' AND  APP.PaymentID <= '" & Request.QueryString("txtPaymentIDTo") & "'"
        If Not Request.QueryString("txtSuppCode") = "" Then SearchStr = SearchStr & " AND (APP.SupplierCode LIKE '%" & Request.QueryString("txtSuppCode") & "%' OR PUSS.Name LIKE '%" & Request.QueryString("txtSuppCode") & "%') "
        If Not Request.QueryString("txtPaymentType") = "0" Then SearchStr = SearchStr & "AND APP.PaymentType LIKE '" & Request.QueryString("txtPaymentType") - 1 & "' "
        If Not Request.QueryString("txtBankCode") = "" Then SearchStr = SearchStr & "AND APP.BankCode LIKE '" & Request.QueryString("txtBankCode") & "' "
        If Not Request.QueryString("txtChequeNo") = "" Then SearchStr = SearchStr & "AND APP.ChequeNo LIKE '" & Request.QueryString("txtChequeNo") & "' "
        If Not Request.QueryString("txtCOACode") = "" Then SearchStr = SearchStr & "AND APPLN.AccCode LIKE '" & Request.QueryString("txtCOACode") & "' "
        If Not Request.QueryString("txtDocumentID") = "" Then SearchStr = SearchStr & "AND APPLN.DocID LIKE '" & Request.QueryString("txtDocumentID") & "' "
        'If Not Request.QueryString("txtStatus") = "0" Then SearchStr = SearchStr & "AND APP.Status LIKE '" & Request.QueryString("txtStatus") & "' "
        If Request.QueryString("txtStatus") = "0" Then
            SearchStr = SearchStr & "AND APP.Status IN ('1', '2', '4', '5', '9') "
        Else
            SearchStr = SearchStr & "AND APP.Status LIKE '" & Request.QueryString("txtStatus") & "' "
        End If

        'Add Date Selection
        If Not (Request.QueryString("DateFrom") = "" And Request.QueryString("DateTo") = "") Then
            SearchStr = SearchStr & "AND APP.CreateDate BETWEEN '" & Request.QueryString("DateFrom") & "' AND '" & Request.QueryString("DateTo") & "'  "
        End If

        If Request.QueryString("txtStatus") = objCBTrx.EnumPaymentStatus.Verified Then
            If Request.QueryString("VerifiedBy") = "ACC" Then
                SearchStr = SearchStr & "AND Left(APP.PaymentID,3) = 'XXX'  "
            Else
                SearchStr = SearchStr & "AND Left(APP.PaymentID,3) <> 'XXX'  "
            End If
        End If

        If Request.QueryString("PrintFor") = "LIST" Then
            strFileName = "CB_StdRpt_PaymentListing"
        Else
            strFileName = "CB_StdRpt_PaymentListingSerahTerima"
            If Request.QueryString("PrintFor") = "PJK" Then
                'SearchStr = SearchStr & "AND APPLN.DocID IN ('33.3.01.003','33.3.01.007')  "
                SearchStr = SearchStr & "AND APPLN.DocID IN (SELECT AccCode FROM TX_TaxObjectRate WHERE Status='1' AND ExpiredStatus='0')  "
            ElseIf Request.QueryString("PrintFor") = "FIN" Then
                'SearchStr = SearchStr & "AND APPLN.DocID IN ('33.3.01.003','33.3.01.007')  "
                SearchStr = SearchStr & "AND APP.PaymentID NOT IN (SELECT PaymentID FROM CB_PaymentLn WHERE DocID IN (SELECT AccCode FROM TX_TaxObjectRate WHERE Status='1' AND ExpiredStatus='0')) "
            End If
        End If

        If Request.QueryString("OtherPayment") = "YES" Then
        Else
            SearchStr = SearchStr & "AND APPLN.DocType <> '4'  "
        End If

        strParam = strUserLoc & "|" & strDDLAccMth & "|" & strDDLAccYr & "|" & strStatus & "|" & SearchStr
        Try
            intErrNo = objCB.mtdGetReport_PaymentListing(strOpCd_PaymentListing_GET, strParam, objRptDs, objMapPath, objFTPFolder)
        Catch Exp As Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=CB_StdRpt_PaymentListing&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strFileName & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        If Request.QueryString("PrintFor") = "LIST" Then
            rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        Else
            rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Portrait
        End If
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3

        crvView.Visible = False
        crvView.ReportSource = rdCrystalViewer
        crvView.DataBind()

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        'crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strFileName & ".pdf"
        If strExportToExcel = "0" Then
            'crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strRptPrefix & ".pdf"
            crDiskFileDestinationOptions.DiskFileName = objFTPFolder & strFileName & ".pdf"
        Else
            'crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strRptPrefix & ".xls"
            crDiskFileDestinationOptions.DiskFileName = objFTPFolder & strFileName & ".xls"
        End If

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            If strExportToExcel = "0" Then
                .ExportFormatType = ExportFormatType.PortableDocFormat
            Else
                .ExportFormatType = ExportFormatType.Excel
            End If
        End With

        rdCrystalViewer.Export()
        rdCrystalViewer.Close()
        rdCrystalViewer.Dispose()

        Dim strUrl As String
        strUrl = objFTPFolder.Substring(InStr(objFTPFolder, "ftp") - 1, Len(objFTPFolder) - InStr(objFTPFolder, "ftp") + 1)
        strUrl = Replace(strUrl, "\", "/")

        If strExportToExcel = "0" Then
            'Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strRptPrefix & ".pdf"">")
            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../" & strUrl & strFileName & ".pdf"">")
        Else
            'Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strRptPrefix & ".xls"">")
            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../" & strUrl & strFileName & ".xls"">")
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
        Dim paramField12 As New ParameterField()
        Dim paramField13 As New ParameterField()
        Dim paramField14 As New ParameterField()
        Dim paramField15 As New ParameterField()
        Dim paramField16 As New ParameterField()
        Dim paramField17 As New ParameterField()
        Dim paramField18 As New ParameterField()
        Dim paramField19 As New ParameterField()
        Dim paramField20 As New ParameterField()
        Dim paramField21 As New ParameterField()

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
        Dim ParamDiscreteValue19 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue20 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue21 As New ParameterDiscreteValue()

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
        Dim crParameterValues19 As ParameterValues
        Dim crParameterValues20 As ParameterValues
        Dim crParameterValues21 As ParameterValues

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("ParamLocation")
        paramField2 = paramFields.Item("ParamAccCode")
        paramField3 = paramFields.Item("ParamStatus")
        paramField4 = paramFields.Item("ParamCompanyName")
        paramField5 = paramFields.Item("ParamUserName")
        paramField6 = paramFields.Item("ParamDecimal")
        paramField7 = paramFields.Item("ParamRptID")
        paramField8 = paramFields.Item("ParamRptName")
        paramField9 = paramFields.Item("ParamAccMonth")
        paramField10 = paramFields.Item("ParamAccYear")
        paramField11 = paramFields.Item("lblCOACode")
        paramField12 = paramFields.Item("lblLocation")
        paramField13 = paramFields.Item("ParamSuppCode")
        paramField14 = paramFields.Item("ParamCOACode")
        paramField15 = paramFields.Item("ParamPaymentIDFrom")
        paramField16 = paramFields.Item("ParamPaymentIDTo")
        paramField17 = paramFields.Item("ParamPaymentType")
        paramField18 = paramFields.Item("ParamBankCode")
        paramField19 = paramFields.Item("ParamChequeNo")
        paramField20 = paramFields.Item("ParamDocumentID")
        paramField21 = paramFields.Item("lblInvRcv")

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
        crParameterValues19 = paramField19.CurrentValues
        crParameterValues20 = paramField20.CurrentValues
        crParameterValues21 = paramField21.CurrentValues

        ParamDiscreteValue1.Value = Session("SS_LOC")
        ParamDiscreteValue2.Value = Session("SS_AccCode")
        ParamDiscreteValue3.Value = Session("SS_Status")
        ParamDiscreteValue4.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue5.Value = Session("SS_USERNAME")
        ParamDiscreteValue6.Value = Session("SS_DECIMAL")
        ParamDiscreteValue7.Value = Session("SS_RPTID")
        ParamDiscreteValue8.Value = Session("SS_RPTNAME")
        ParamDiscreteValue9.Value = Session("SS_ACCMONTH")
        ParamDiscreteValue10.Value = Session("SS_ACCYEAR")
        ParamDiscreteValue11.Value = Session("SS_LBLACCCODE")
        ParamDiscreteValue12.Value = Session("SS_LBLLOCATION")
        ParamDiscreteValue13.Value = Session("SS_SuppCode")
        ParamDiscreteValue14.Value = Session("SS_COACode")
        ParamDiscreteValue15.Value = Session("SS_PaymentIDFrom")
        ParamDiscreteValue16.Value = Session("SS_PaymentIDTo")
        ParamDiscreteValue17.Value = Session("SS_PaymentType")
        ParamDiscreteValue18.Value = Session("SS_BankCode")
        ParamDiscreteValue19.Value = Session("SS_ChequeNo")
        ParamDiscreteValue20.Value = Session("SS_DocumentID")
        ParamDiscreteValue21.Value = strInvRcvTag

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
        crParameterValues19.Add(ParamDiscreteValue19)
        crParameterValues20.Add(ParamDiscreteValue20)
        crParameterValues21.Add(ParamDiscreteValue21)

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
        PFDefs(18).ApplyCurrentValues(crParameterValues19)
        PFDefs(19).ApplyCurrentValues(crParameterValues20)
        PFDefs(20).ApplyCurrentValues(crParameterValues21)

        crvView.ParameterFieldInfo = paramFields

    End Sub

End Class
