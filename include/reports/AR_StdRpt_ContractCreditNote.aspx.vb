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

Public Class AR_StdRpt_ContractCreditNote : Inherits Page

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

    Protected WithEvents txtFromCreditNoteId As Textbox
    Protected WithEvents txtToCreditNoteId As Textbox
    Protected WithEvents txtFromCreditNoteDate As Textbox
    Protected WithEvents txtToCreditNoteDate As Textbox
    Protected WithEvents txtBuyer As Textbox
    Protected WithEvents ddlCNDocType As Dropdownlist
    Protected WithEvents lblBillParty As Label
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
                DisplayXmlParameters(strUndName, strUndPost)
                lblUndName.text = strUndName
                lblUndPost.text = strUndPost
                If Trim(strUndName) <> "" Then
                    txtUndName.text = strUndName
                End If
                If Trim(strUndPost) <> "" Then
                    txtUndPost.text = strUndPost
                End If 
                onload_GetLangCap()
                BindCreditNoteType()
            End If
        End If
    End Sub









    Sub BindCreditNoteType()
        ddlCNDocType.Items.Add(New ListItem(objBITrx.mtdGetCreditNoteDocType(objBITrx.EnumCreditNoteDocType.Manual), objBITrx.EnumCreditNoteDocType.Manual))
        ddlCNDocType.Items.Add(New ListItem(objBITrx.mtdGetCreditNoteDocType(objBITrx.EnumCreditNoteDocType.Manual_Millware), objBITrx.EnumCreditNoteDocType.Manual_Millware))
        ddlCNDocType.Items.Add(New ListItem(objBITrx.mtdGetCreditNoteDocType(objBITrx.EnumCreditNoteDocType.Auto_Millware), objBITrx.EnumCreditNoteDocType.Auto_Millware))
        ddlCNDocType.SelectedIndex = 0
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


    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strFromCreditNoteId As String
        Dim strToCreditNoteId As String
        Dim strFromCreditNoteDate As String
        Dim strToCreditNoteDate As String
        Dim strBillPartyCode As String
        Dim strCreditNoteType As String
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
        
        strFromCreditNoteId = txtFromCreditNoteId.text
        strToCreditNoteId = txtToCreditNoteId.text
        strFromCreditNoteDate = txtFromCreditNoteDate.text
        strToCreditNoteDate = txtToCreditNoteDate.text
        strBillPartyCode = txtBuyer.text
        strCreditNoteType = ddlCNDocType.SelectedItem.Value

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

        If Not Trim(strFromCreditNoteDate) = "" Then
            If objGlobal.mtdValidInputDate(strDateSetting, strFromCreditNoteDate, objDateFormat, objDateFrom) = True Then
            Else
                lblDateFormat.text = objDateFormat & "."
                lblDate.visible = True
                lblDateFormat.visible = True
                Exit Sub
            End If
        End If

        If Not Trim(strToCreditNoteDate) = "" Then
            If objGlobal.mtdValidInputDate(strDateSetting, strToCreditNoteDate, objDateFormat, objDateTo) = True Then
            Else
                lblDateFormat.text = objDateFormat & "."
                lblDate.visible = True
                lblDateFormat.visible = True
                Exit Sub
            End If
        End If


        UpdateXmlParameters()
        
        Response.Write("<Script Language=""JavaScript"">window.open(""AR_StdRpt_ContractCreditNotePreview.aspx?Type=Print&CompName=" & strCompany & _
                       "&Location=" & strLocation & _
                       "&SelAccMonth=" & strSelAccMonth & _
                       "&SelAccYear=" & strSelAccYear & _
                       "&RptId=" & strRptId & _
                       "&RptName=" & strRptName & _
                       "&Decimal=" & strDec & _
                       "&Supp=" & strSupp & _
                       "&FromCNId=" & strFromCreditNoteId & _
                       "&ToCNId=" & strToCreditNoteId & _
                       "&FromCNDate=" & objDateFrom & _
                       "&ToCNDate=" & objDateTo & _
                       "&BillPartyCode=" & Server.UrlEncode(strBillPartyCode) & _
                       "&CNType=" & strCreditNoteType & _
                       "&BillPartyTag=" & lblBillParty.text & _
                       "&UndName=" & txtUndName.text & _
                       "&UndPost=" & txtUndPost.text & _
                       "&ExcPrintedDoc=" & cbExcPrintedDoc.Checked & _
                       """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Sub DisplayXmlParameters(ByRef pr_strUndName As String, _
                             ByRef pr_strUndPost As String)

        Dim objStreamReader As StreamReader
        Dim objDtsXml As New DataSet()
        Dim intErrNo As Integer
        Dim strFtpPath As String
        Dim strFile As String

        pr_strUndName = ""
        pr_strUndPost = ""

        Try
            intErrNo = objSysCfg.mtdGetFtpPath(strFtpPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=AR_STDRPT_GET_FTPPATH_DISPLAY&errmesg=" & Exp.ToString() & "&redirect=reports/ar_stdrpt_contractcreditnote.aspx")
        End Try

        strFile = strFtpPath & "param\AR_REPORT_PARAM.xml"

        objDtsXml.EnforceConstraints = False
        objDtsXml.ReadXml(strFile)

        If objDtsXml.Tables(0).Rows.Count > 0 Then
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=AR_STD_RPT_GET_FTPPATH_UPD&errmesg=" & Exp.ToString() & "&redirect=reports/ar_stdrpt_contractcreditnote.aspx")
        End Try

        strFile = strFtpPath & "param\AR_REPORT_PARAM.xml"

        objStreamReader = File.OpenText(strFile)
        strXmlSrc = objStreamReader.ReadToEnd()
        objStreamReader.Close()

        strXmlSrc = Replace(strXmlSrc, "<UndersignedName>" & lblUndName.text & "</UndersignedName>", "<UndersignedName>" & txtUndName.text & "</UndersignedName>")
        strXmlSrc = Replace(strXmlSrc, "<UndersignedPost>" & lblUndPost.text & "</UndersignedPost>", "<UndersignedPost>" & txtUndPost.text & "</UndersignedPost>")

        Dim objStreamWrite As StreamWriter = New StreamWriter(strFile)
        objStreamWrite.Write(strXmlSrc)
        objStreamWrite.Close()

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
