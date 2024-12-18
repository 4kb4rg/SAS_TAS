Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports System.Xml
Imports System.Web.Services

Public Class AR_StdRpt_ContractDebitNote : Inherits Page

    Protected RptSelect As UserControl

    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objCMRpt As New agri.CM.clsReport()
    Dim objBITrx As New agri.BI.clsTrx()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objBPDs As New Object()
    Dim objSysCfgDs As New Object()
    Dim objAdminLoc As New agri.Admin.clsLoc()


    Protected WithEvents txtFromDebitNoteId As Textbox
    Protected WithEvents txtToDebitNoteId As Textbox
    Protected WithEvents txtFromDebitNoteDate As Textbox
    Protected WithEvents txtToDebitNoteDate As Textbox
    Protected WithEvents txtBuyer As Textbox
    Protected WithEvents ddlDNDocType As Dropdownlist
    Protected WithEvents lblBillParty As Label
    Protected WithEvents txtBankAccNo AS Textbox
    Protected WithEvents lblBankAccNo As Label
    Protected WithEvents txtBankName As Textbox
    Protected WithEvents lblBankName As Label
    Protected WithEvents txtBankBranch As Textbox
    Protected WithEvents lblBankBranch As Label
    Protected WithEvents txtUndName As Textbox
    Protected WithEvents lblUndName As Label
    Protected WithEvents txtUndPost As Textbox
    Protected WithEvents lblUndPost As Label
    Protected WithEvents lblDate As Label
    Protected WithEvents lblDateFormat As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents rbSuppYes As RadioButton
    Protected WithEvents rbSuppNo As RadioButton
    Protected WithEvents PrintPrev As ImageButton
    Protected WithEvents TrBillParty As HtmlTableRow
    Protected WithEvents TrBankAccNo As HtmlTableRow
    Protected WithEvents TrBankName As HtmlTableRow
    Protected WithEvents TrBankBranch As HtmlTableRow
    Protected WithEvents TrUndName As HtmlTableRow
    Protected WithEvents TrUndPost As HtmlTableRow
    Protected WithEvents cbExcPrintedDoc As CheckBox

    Dim TrMthYr As HtmlTableRow

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfigsetting As Integer
    Dim dr As DataRow
    Dim intCnt As Integer
    Dim intErrNo As Integer

    Dim objLangCapDs As New Object()
    Dim strBankAccNo As String
    Dim strBankName As String
    Dim strBankBranch As String
    Dim strUndName As String
    Dim strUndPost As String

    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_ARACCMONTH")
        strAccYear = Session("SS_ARACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")     
        Else
            If Not Page.IsPostBack
                DisplayXmlParameters(strBankAccNo, strBankName, strBankBranch, strUndName, strUndPost)
                lblBankAccNo.text = strBankAccNo
                lblBankName.text = strBankName
                lblBankBranch.text = strBankBranch
                lblUndName.text = strUndName
                lblUndPost.text = strUndPost
                If Trim(strBankAccNo) <> "" Then
                    txtBankAccNo.Text = strBankAccNo
                End If
                If Trim(strBankName) <> "" Then
                    txtBankName.text = strBankName
                End If
                If Trim(strBankBranch) <> "" Then
                    txtBankBranch.text = strBankBranch
                End If
                If Trim(strUndName) <> "" Then
                    txtUndName.text = strUndName
                End If
                If Trim(strUndPost) <> "" Then
                    txtUndPost.text = strUndPost
                End If 
                onload_GetLangCap()
                BindDebitNoteType()
            End If
        End If
    End Sub









    Sub BindDebitNoteType()
        ddlDNDocType.Items.Add(New ListItem(objBITrx.mtdGetDebitNoteDocType(objBITrx.EnumDebitNoteDocType.Manual), objBITrx.EnumDebitNoteDocType.Manual))
        ddlDNDocType.Items.Add(New ListItem(objBITrx.mtdGetDebitNoteDocType(objBITrx.EnumDebitNoteDocType.Manual_Millware), objBITrx.EnumDebitNoteDocType.Manual_Millware))
        ddlDNDocType.Items.Add(New ListItem(objBITrx.mtdGetDebitNoteDocType(objBITrx.EnumDebitNoteDocType.Auto_Millware), objBITrx.EnumDebitNoteDocType.Auto_Millware))
        ddlDNDocType.Items.Add(New ListItem(objBITrx.mtdGetDebitNoteDocType(objBITrx.EnumDebitNoteDocType.Auto_INIssue_Staff), objBITrx.EnumDebitNoteDocType.Auto_INIssue_Staff))
        ddlDNDocType.Items.Add(New ListItem(objBITrx.mtdGetDebitNoteDocType(objBITrx.EnumDebitNoteDocType.Auto_INIssue_External), objBITrx.EnumDebitNoteDocType.Auto_INIssue_External))
        ddlDNDocType.Items.Add(New ListItem(objBITrx.mtdGetDebitNoteDocType(objBITrx.EnumDebitNoteDocType.Auto_INTransfer), objBITrx.EnumDebitNoteDocType.Auto_INTransfer))
        ddlDNDocType.Items.Add(New ListItem(objBITrx.mtdGetDebitNoteDocType(objBITrx.EnumDebitNoteDocType.Auto_CTIssue_Staff), objBITrx.EnumDebitNoteDocType.Auto_CTIssue_Staff))
        ddlDNDocType.Items.Add(New ListItem(objBITrx.mtdGetDebitNoteDocType(objBITrx.EnumDebitNoteDocType.Auto_CTIssue_External), objBITrx.EnumDebitNoteDocType.Auto_CTIssue_External))
        ddlDNDocType.Items.Add(New ListItem(objBITrx.mtdGetDebitNoteDocType(objBITrx.EnumDebitNoteDocType.Auto_CTTransfer), objBITrx.EnumDebitNoteDocType.Auto_CTTransfer))
        ddlDNDocType.Items.Add(New ListItem(objBITrx.mtdGetDebitNoteDocType(objBITrx.EnumDebitNoteDocType.Auto_INFuelIssue_Staff), objBITrx.EnumDebitNoteDocType.Auto_INFuelIssue_Staff))
        ddlDNDocType.Items.Add(New ListItem(objBITrx.mtdGetDebitNoteDocType(objBITrx.EnumDebitNoteDocType.Auto_INFuelIssue_External), objBITrx.EnumDebitNoteDocType.Auto_INFuelIssue_External))
        ddlDNDocType.Items.Add(New ListItem(objBITrx.mtdGetDebitNoteDocType(objBITrx.EnumDebitNoteDocType.Auto_WSJob_Staff), objBITrx.EnumDebitNoteDocType.Auto_WSJob_Staff))
        ddlDNDocType.Items.Add(New ListItem(objBITrx.mtdGetDebitNoteDocType(objBITrx.EnumDebitNoteDocType.Auto_WSJob_External), objBITrx.EnumDebitNoteDocType.Auto_WSJob_External))
        ddlDNDocType.SelectedIndex = 0
    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim htmltr As HtmlTableRow

        htmltr = RptSelect.FindControl("SelLocation")
        htmltr.visible = False

        htmltr = RptSelect.FindControl("TrMthYr")
        htmltr.visible = True

        htmltr = RptSelect.FindControl("SelDecimal")
        htmltr.visible = False

        If Page.IsPostBack Then
        end if
    End Sub

    Sub onChange_DocType(ByVal sender As Object, ByVal e As EventArgs)
        Dim strDocType As String

        TrBillParty.visible = False
        TrBankAccNo.visible = False
        TrBankName.visible = False
        TrBankBranch.visible = False
        TrUndName.visible = False
        TrUndPost.visible = False

        strDocType = ddlDNDocType.SelectedItem.Value
        Select Case CInt(strDocType)
            Case objBITrx.EnumDebitNoteDocType.Manual, objBITrx.EnumDebitNoteDocType.Manual_Millware, objBITrx.EnumDebitNoteDocType.Auto_Millware
                TrBillParty.visible = True
                TrBankAccNo.visible = True
                TrBankName.visible = True
                TrBankBranch.visible = True
                TrUndName.visible = True
                TrUndPost.visible = True
            Case objBITrx.EnumDebitNoteDocType.Auto_INIssue_Staff, objBITrx.EnumDebitNoteDocType.Auto_CTIssue_Staff, objBITrx.EnumDebitNoteDocType.Auto_INFuelIssue_Staff, objBITrx.EnumDebitNoteDocType.Auto_WSJob_Staff
            Case objBITrx.EnumDebitNoteDocType.Auto_INIssue_External, objBITrx.EnumDebitNoteDocType.Auto_CTIssue_External, objBITrx.EnumDebitNoteDocType.Auto_INFuelIssue_External, objBITrx.EnumDebitNoteDocType.Auto_WSJob_External
                TrBillParty.visible = True
            Case objBITrx.EnumDebitNoteDocType.Auto_INTransfer, objBITrx.EnumDebitNoteDocType.Auto_CTTransfer
        End Select
    End Sub


    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strFromDebitNoteId As String
        Dim strToDebitNoteId As String
        Dim strFromDebitNoteDate As String
        Dim strToDebitNoteDate As String
        Dim strBillPartyCode As String
        Dim strDebitNoteType As String
        Dim strDec As String
        Dim strSelAccMonth As String
        Dim strSelAccYear As String
        Dim strRptId As String
        Dim strRptName As String
        Dim strSupp As String
        Dim strParam As String

        Dim ddlist As DropDownList
        Dim rdblist As RadioButtonList
        Dim txt As TextBox

        Dim strDateSetting As String
        Dim objDateFrom As String
        Dim objDateTo As String
        Dim objDateFormat As String
        Dim intCnt As Integer
        
        strFromDebitNoteId = txtFromDebitNoteId.text
        strToDebitNoteId = txtToDebitNoteId.text
        strFromDebitNoteDate = txtFromDebitNoteDate.text
        strToDebitNoteDate = txtToDebitNoteDate.text
        strBillPartyCode = txtBuyer.text
        strDebitNoteType = ddlDNDocType.SelectedItem.Value

        ddlist = RptSelect.FindControl("lstAccMonth")
        strSelAccMonth = Trim(ddlist.SelectedItem.Value)

        ddlist = RptSelect.FindControl("lstAccYear")
        strSelAccYear = Trim(ddlist.SelectedItem.Value)

        ddlist = RptSelect.FindControl("lstRptName")
        strRptId = Trim(ddlist.SelectedItem.Value)

        ddlist = RptSelect.FindControl("lstRptName")
        strRptName = Trim(ddlist.SelectedItem.text)

        ddlist = RptSelect.FindControl("lstDecimal")
        strDec = Trim(ddlist.SelectedItem.Value)
    
        If rbSuppYes.Checked Then
            strSupp = rbSuppYes.Text
        ElseIf rbSuppNo.Checked Then
            strSupp = rbSuppNo.Text
        End If

        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"
        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=AR_STDRPT_GET_CONFIG_DATE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        strDateSetting = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))

        If Not Trim(strFromDebitNoteDate) = "" Then
            If objGlobal.mtdValidInputDate(strDateSetting, strFromDebitNoteDate, objDateFormat, objDateFrom) = True Then
            Else
                lblDateFormat.text = objDateFormat & "."
                lblDate.visible = True
                lblDateFormat.visible = True
                Exit Sub
            End If
        End If

        If Not Trim(strToDebitNoteDate) = "" Then
            If objGlobal.mtdValidInputDate(strDateSetting, strToDebitNoteDate, objDateFormat, objDateTo) = True Then
            Else
                lblDateFormat.text = objDateFormat & "."
                lblDate.visible = True
                lblDateFormat.visible = True
            End If
        End If


        UpdateXmlParameters()
        
        Response.Write("<Script Language=""JavaScript"">window.open(""AR_StdRpt_ContractDebitNotePreview.aspx?Type=Print&CompName=" & strCompany & _
                       "&Location=" & strLocation & _
                       "&SelAccMonth=" & strSelAccMonth & _
                       "&SelAccYear=" & strSelAccYear & _
                       "&RptId=" & strRptId & _
                       "&RptName=" & strRptName & _
                       "&Decimal=" & strDec & _
                       "&Supp=" & strSupp & _
                       "&FromDNId=" & strFromDebitNoteId & _
                       "&ToDNId=" & strToDebitNoteId & _
                       "&FromDNDate=" & objDateFrom & _
                       "&ToDNDate=" & objDateTo & _
                       "&BillPartyCode=" & Server.UrlEncode(strBillPartyCode) & _
                       "&DNType=" & strDebitNoteType & _
                       "&BillPartyTag=" & lblBillParty.text & _
                       "&BankAccNo=" & txtBankAccNo.text & _
                       "&BankName=" & txtBankName.text & _
                       "&BankBranch=" & txtBankBranch.text & _
                       "&UndName=" & txtUndName.text & _
                       "&UndPost=" & txtUndPost.text & _
                       "&ExcPrintedDoc=" & cbExcPrintedDoc.Checked & _
                       """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Sub DisplayXmlParameters(ByRef pr_strBankAccNo As String, _
                             ByRef pr_strBankName As String, _
                             ByRef pr_strBankBranch As String, _
                             ByRef pr_strUndName As String, _
                             ByRef pr_strUndPost As String)

        Dim objStreamReader As StreamReader
        Dim objDtsXml As New DataSet()
        Dim intErrNo As Integer
        Dim strFtpPath As String
        Dim strFile As String

        pr_strBankAccNo = ""
        pr_strBankName = ""
        pr_strBankBranch = ""
        pr_strUndName = ""
        pr_strUndPost = ""

        Try
            intErrNo = objSysCfg.mtdGetFtpPath(strFtpPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=AR_STDRPT_GET_FTPPATH_DISPLAY&errmesg=" & Exp.ToString() & "&redirect=reports/ar_stdrpt_contractinvoice.aspx")
        End Try

        strFile = strFtpPath & "param\AR_REPORT_PARAM.xml"

        objDtsXml.EnforceConstraints = False
        objDtsXml.ReadXml(strFile)

        If objDtsXml.Tables(0).Rows.Count > 0 Then
            pr_strBankAccNo = objDtsXml.Tables(0).Rows(intCnt).Item("BankAccNo")
            pr_strBankName = objDtsXml.Tables(0).Rows(intCnt).Item("BankName")
            pr_strBankBranch = objDtsXml.Tables(0).Rows(intCnt).Item("BankBranch")
            pr_strUndName = objDtsXml.Tables(0).Rows(intCnt).Item("UndersignedName")
            pr_strUndPost = objDtsXml.Tables(0).Rows(intCnt).Item("UndersignedPost")
        End If
        objDtsXml = Nothing
    End Sub

    Sub UpdateXmlParameters()
        Dim objStreamReader As StreamReader
        Dim strXmlSrc As String
        Dim intErrNo As Integer
        Dim strFtpPath As String
        Dim strFile As String

        Try
            intErrNo = objSysCfg.mtdGetFtpPath(strFtpPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=AR_STD_RPT_GET_FTPPATH_UPD&errmesg=" & Exp.ToString() & "&redirect=reports/ar_stdrpt_contractinvoice.aspx")
        End Try

        strFile = strFtpPath & "param\AR_REPORT_PARAM.xml"

        objStreamReader = File.OpenText(strFile)
        strXmlSrc = objStreamReader.ReadToEnd()
        objStreamReader.Close()

        strXmlSrc = Replace(strXmlSrc, "<BankAccNo>" & lblBankAccNo.Text & "</BankAccNo>", "<BankAccNo>" & txtBankAccno.Text & "</BankAccNo>")
        strXmlSrc = Replace(strXmlSrc, "<BankName>" & lblBankName.Text & "</BankName>", "<BankName>" & txtBankName.text & "</BankName>")
        strXmlSrc = Replace(strXmlSrc, "<BankBranch>" & lblBankBranch.text & "</BankBranch>", "<BankBranch>" & txtBankBranch.text & "</BankBranch>")
        strXmlSrc = Replace(strXmlSrc, "<UndersignedName>" & lblUndName.text & "</UndersignedName>", "<UndersignedName>" & txtUndName.text & "</UndersignedName>")
        strXmlSrc = Replace(strXmlSrc, "<UndersignedPost>" & lblUndPost.text & "</UndersignedPost>", "<UndersignedPost>" & txtUndPost.text & "</UndersignedPost>")

        Dim objStreamWrite As StreamWriter = New StreamWriter(strFile)
        objStreamWrite.Write(strXmlSrc)
        objStreamWrite.Close()

        lblBankAccNo.Text = txtBankAccNo.Text
        lblBankName.text = txtBankName.text
        lblBankBranch.text = txtBankBranch.text
        lblUndName.text = txtUndName.text
        lblUndPost.text = txtUndPost.text

    End Sub


    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblBillParty.text = GetCaption(objLangCap.EnumLangCap.BillParty)
    End Sub

    Sub GetEntireLangCap()
        Dim strOpCode_BussTerm As String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
        Dim strParam As String
        Dim intErrNo As Integer

        strParam = strLangCode
        Try
            intErrNo = objLangCap.mtdGetBussTerm(strOpCode_BussTerm, _
                                                 strCompany, _
                                                 strLocation, _
                                                 strUserId, _
                                                 strAccMonth, _
                                                 strAccYear, _
                                                 objLangCapDs, _
                                                 strParam)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_STDRPT_TRIALBALANCE_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/IN_StdRpt_Selection.aspx")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
            Dim count As Integer

            For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
                If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                    If strLocType = objAdminLoc.EnumLocType.Mill then
                        Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                    else
                        Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                    end if
                    Exit For
                End If
            Next
        End Function

End Class
