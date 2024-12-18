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

Public Class AP_StdRpt_InvRcvNoteFakturPajakListingPreview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents EventData As DataGrid

    Dim objAP As New agri.AP.clsReport()
    Dim objAPTrx As New agri.AP.clsTrx()
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
    Dim strExportToExcel As String

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
    Dim intConfigsetting As String

    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_APACCMONTH")
        strAccYear = Session("SS_APACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        strPhyMonth = Session("SS_PHYMONTH")
        strPhyYear = Session("SS_PHYYEAR")
        intConfigsetting = Session("SS_CONFIGSETTING")


        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
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
            strInvRcvTag = Trim(Request.QueryString("lblInvRcv"))
            strDDLAccMth = Request.QueryString("DDLAccMth")
            strDDLAccYr = Request.QueryString("DDLAccYr")
            strFileName = "AP_StdRpt_InvRcvNoteFakturPajakListing"
            strExportToExcel = Trim(Request.QueryString("ExportToExcel"))

            Session("SS_LBLLOCATION") = Request.QueryString("lblLocation")
            Session("SS_Decimal") = Request.QueryString("Decimal")
            Session("SS_RptID") = Request.QueryString("RptID")
            Session("SS_RptName") = Request.QueryString("RptName")
            Session("SS_AccMonth") = Request.QueryString("DDLAccMth")
            Session("SS_AccYear") = Request.QueryString("DDLAccYr")
            Session("SS_Status") = Request.QueryString("txtStatus")
            Session("SS_InvRcvNo") = Request.QueryString("txtInvRcvNo")
            Session("SS_POID") = Request.QueryString("txtPOID")
            Session("SS_DocNoFrom") = Request.QueryString("DocNoFrom")
            Session("SS_DocNoTo") = Request.QueryString("DocNoTo")
            Session("SS_InvRcvRefDateFrom") = Request.QueryString("txtInvRcvRefDateFrom")
            Session("SS_InvRcvRefDateTo") = Request.QueryString("txtInvRcvRefDateTo")
            Session("SS_CreditTermType") = Request.QueryString("txtCreditTermType")
            Session("SS_CreditTerm") = Request.QueryString("txtCreditTerm")
            Session("SS_SuppCode") = Request.QueryString("txtSuppCode")
            Session("SS_InvDueDateFrom") = Request.QueryString("txtInvDueDateFrom")
            Session("SS_InvDueDateTo") = Request.QueryString("txtInvDueDateTo")
            Select Case Trim(Request.QueryString("ddlInvoiceType"))
                Case objAPTrx.EnumInvoiceType.SupplierPO
                    Session("SS_InvoiceType") = objAPTrx.mtdGetInvoiceType(objAPTrx.EnumInvoiceType.SupplierPO)
                Case objAPTrx.EnumInvoiceType.ContractorWorkOrder
                    Session("SS_InvoiceType") = objAPTrx.mtdGetInvoiceType(objAPTrx.EnumInvoiceType.ContractorWorkOrder)
                Case objAPTrx.EnumInvoiceType.Others
                    Session("SS_InvoiceType") = objAPTrx.mtdGetInvoiceType(objAPTrx.EnumInvoiceType.Others)
                Case objAPTrx.EnumInvoiceType.TransportFee
                    Session("SS_InvoiceType") = objAPTrx.mtdGetInvoiceType(objAPTrx.EnumInvoiceType.TransportFee)
                Case objAPTrx.EnumInvoiceType.FFBSupplier
                    Session("SS_InvoiceType") = objAPTrx.mtdGetInvoiceType(objAPTrx.EnumInvoiceType.FFBSupplier)
                Case Else
                    Session("SS_InvoiceType") = "All"
            End Select
            BindReport()
        End If
    End Sub



    Sub BindReport()
        Dim objRptDs As New DataSet()
        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim strOpCd_InvRcvListing_GET As String = "AP_STDRPT_INVRCVNOTEFAKTURPAJAKLISTING_GET"
        Dim SearchStr As String
        Dim SQLStr As String
        Dim intCnt As Integer
        Dim objFTPFolder As String
        SearchStr = ""



        If Not Request.QueryString("txtInvRcvNo") = "" Then SearchStr = SearchStr & " AND AP.InvoiceRcvRefNo LIKE '" & Request.QueryString("txtInvRcvNo") & "' "
        If Not Request.QueryString("txtPOID") = "" Then SearchStr = SearchStr & "AND AP.POID LIKE '" & Request.QueryString("txtPOID") & "' "
        If Not Request.QueryString("txtCreditTerm") = "" Then SearchStr = SearchStr & "AND AP.CreditTerm LIKE '" & Request.QueryString("txtCreditTerm") & "' "
        If Not Request.QueryString("txtCreditTermTypeValue") = "13" Then SearchStr = SearchStr & "AND AP.TermType LIKE '" & Request.QueryString("txtCreditTermTypeValue") & "' "

        If Not Request.QueryString("txtStatus") = "0" Then SearchStr = SearchStr & "AND AP.Status LIKE '" & Request.QueryString("txtStatus") & "' "
        If Not Request.QueryString("txtStatus") = "0" Then
            SearchStr = SearchStr & "AND AP.Status LIKE '" & Request.QueryString("txtStatus") & "' "
        Else
            SearchStr = SearchStr & "AND AP.Status Not In ('3','4') "
        End If
        If Not Request.QueryString("ddlInvoiceType") = "0" Then SearchStr = SearchStr & "AND AP.InvoiceType LIKE '" & Request.QueryString("ddlInvoiceType") & "' "
        If Not Request.QueryString("txtSuppCode") = "" Then SearchStr = SearchStr & "AND (AP.SupplierCode LIKE '%" & Request.QueryString("txtSuppCode") & "%' Or PU_SUPPLIER.Name LIKE '%" & Request.QueryString("txtSuppCode") & "%')"
        If Not (Request.QueryString("txtInvRcvRefDateFrom") = "" And Request.QueryString("txtInvRcvRefDateTo") = "") Then
            SearchStr = SearchStr & " AND (DateDiff(Day, '" & Session("SS_InvRcvRefDateFrom") & "', AP.CreateDate) >= 0) And (DateDiff(Day, '" & Session("SS_InvRcvRefDateTo") & "', AP.CreateDate) <= 0) "
        End If
        If Not (Request.QueryString("txtInvDueDateFrom") = "" And Request.QueryString("txtInvDueDateTo") = "") Then
            SearchStr = SearchStr & " AND (DateDiff(Day, '" & Session("SS_InvDueDateFrom") & "', APLN.FakturPajakDate) >= 0) And (DateDiff(Day, '" & Session("SS_InvDueDateTo") & "', APLN.FakturPajakDate) <= 0) "
        End If
        If (Len(Request.QueryString("DocNoFrom")) > 0) And (Len(Request.QueryString("DocNoTo")) > 0) Then
            SearchStr = SearchStr & "AND AP.InvoiceRcvID >= '" & Request.QueryString("DocNoFrom") & "' AND AP.InvoiceRcvID <= '" & Request.QueryString("DocNoTo") & "' "
        ElseIf (Len(Request.QueryString("DocNoFrom")) > 0) Then
            SearchStr = SearchStr & "AND AP.InvoiceRcvID LIKE '" & Request.QueryString("DocNoFrom") & "' "
        End If

        strParam = strUserLoc & "|" & strDDLAccMth & "|" & strDDLAccYr & "|" & strStatus & "|" & SearchStr
        Try
            intErrNo = objAP.mtdGetReport_InvRcvListing(strOpCd_InvRcvListing_GET, strParam, objRptDs, objMapPath, objFTPFolder)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=AP_STDRPT_InvRcvListing&errmesg=" & Exp.ToString() & "&redirect=")
        End Try


        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strFileName & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        crvView.Visible = False
        crvView.ReportSource = rdCrystalViewer
        crvView.DataBind()

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        'crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strFileName & ".pdf"
        If strExportToExcel = "0" Then
            crDiskFileDestinationOptions.DiskFileName = objFTPFolder & strFileName & ".pdf"
        Else
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
            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../" & strUrl & strFileName & ".pdf"">")
        Else
            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../" & strUrl & strFileName & ".xls"">")
        End If
        'Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strFileName & ".pdf"">")

        objRptDs = Nothing
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
        Dim paramField22 As New ParameterField()
        Dim paramField23 As New ParameterField()
        Dim paramField24 As New ParameterField()

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
        Dim ParamDiscreteValue22 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue23 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue24 As New ParameterDiscreteValue()

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
        Dim crParameterValues22 As ParameterValues
        Dim crParameterValues23 As ParameterValues
        Dim crParameterValues24 As ParameterValues

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
        paramField10 = paramFields.Item("lblLocation")
        paramField11 = paramFields.Item("ParamInvRcvNo")
        paramField12 = paramFields.Item("ParamPOID")
        paramField13 = paramFields.Item("ParamInvRcvRefDateFrom")
        paramField14 = paramFields.Item("ParamCreditTermType")
        paramField15 = paramFields.Item("ParamCreditTerm")
        paramField16 = paramFields.Item("ParamSuppCode")
        paramField17 = paramFields.Item("ParamInvRcvRefDateTo")
        paramField18 = paramFields.Item("ParamInvoiceType")
        paramField19 = paramFields.Item("ParamDocNoFrom")
        paramField20 = paramFields.Item("ParamDocNoTo")
        paramField21 = paramFields.Item("lblInvRcv_UCase")
        paramField22 = paramFields.Item("lblInvRcv_NCase")
        paramField23 = paramFields.Item("ParamInvDueDateFrom")
        paramField24 = paramFields.Item("ParamInvDueDateTo")

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
        crParameterValues22 = paramField22.CurrentValues
        crParameterValues23 = paramField23.CurrentValues
        crParameterValues24 = paramField24.CurrentValues

        ParamDiscreteValue1.Value = Session("SS_LOC")
        ParamDiscreteValue2.Value = Session("SS_Status")
        ParamDiscreteValue3.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue4.Value = Session("SS_USERNAME")
        ParamDiscreteValue5.Value = Session("SS_DECIMAL")
        ParamDiscreteValue6.Value = Session("SS_RPTID")
        ParamDiscreteValue7.Value = Session("SS_RPTNAME")
        ParamDiscreteValue8.Value = Session("SS_ACCMONTH")
        ParamDiscreteValue9.Value = Session("SS_ACCYEAR")
        ParamDiscreteValue10.Value = Session("SS_LBLLOCATION")
        ParamDiscreteValue11.Value = Session("SS_INVRCVNO")
        ParamDiscreteValue12.Value = Session("SS_POID")
        ParamDiscreteValue13.Value = Session("SS_INVRCVREFDATEFROM")
        ParamDiscreteValue14.Value = Session("SS_CREDITTERMTYPE")
        ParamDiscreteValue15.Value = Session("SS_CREDITTERM")
        ParamDiscreteValue16.Value = Session("SS_SUPPCODE")
        ParamDiscreteValue17.Value = Session("SS_INVRCVREFDATETO")
        ParamDiscreteValue18.Value = Session("SS_InvoiceType")
        ParamDiscreteValue19.Value = Session("SS_DocNoFrom")
        ParamDiscreteValue20.Value = Session("SS_DocNoTo")
        ParamDiscreteValue21.Value = UCase(strInvRcvTag)
        ParamDiscreteValue22.Value = strInvRcvTag
        ParamDiscreteValue23.Value = Session("SS_INVDUEDATETO")
        ParamDiscreteValue24.Value = Session("SS_INVDUEDATETO")

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
        crParameterValues22.Add(ParamDiscreteValue22)
        crParameterValues23.Add(ParamDiscreteValue23)
        crParameterValues24.Add(ParamDiscreteValue24)

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
        PFDefs(21).ApplyCurrentValues(crParameterValues22)
        PFDefs(22).ApplyCurrentValues(crParameterValues23)
        PFDefs(23).ApplyCurrentValues(crParameterValues24)

        crvView.ParameterFieldInfo = paramFields
    End Sub
End Class
