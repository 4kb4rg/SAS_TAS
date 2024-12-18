Imports System
Imports System.IO
Imports System.Data
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

Public Class AR_StdRpt_DebtorAccountAgeing : Inherits Page

    Protected RptSelect As UserControl

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfgDs As New Object()
    Dim objAdminLoc As New agri.Admin.clsLoc()


    Protected WithEvents txtBuyer As TextBox
    Protected WithEvents txtAccCode As TextBox
    Protected WithEvents txtCutOffDate As TextBox
    Protected WithEvents txtFromAge1 As TextBox
    Protected WithEvents txtToAge1 As TextBox
    Protected WithEvents txtFromAge2 As TextBox
    Protected WithEvents txtToAge2 As TextBox
    Protected WithEvents txtFromAge3 As TextBox
    Protected WithEvents txtToAge3 As TextBox
    Protected WithEvents txtFromAge4 As TextBox
    Protected WithEvents txtToAge4 As TextBox
    Protected WithEvents txtFromAge5 As TextBox
    Protected WithEvents lblLocation As Label
    Protected WithEvents lblBillParty As Label
    Protected WithEvents lblAccCode As Label
    Protected WithEvents lblBlkCode as Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblToAge1 As Label
    Protected WithEvents lblFromAge2 As Label
    Protected WithEvents lblToAge2 As Label
    Protected WithEvents lblFromAge3 As Label
    Protected WithEvents lblToAge3 As Label
    Protected WithEvents lblFromAge4 As Label
    Protected WithEvents lblToAge4 As Label
    Protected WithEvents lblFromAge5 As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblDate As Label
    Protected WithEvents lblDateFormat As Label
    Protected WithEvents lblErrAge As Label
    Protected WithEvents rbSuppYes As RadioButton
    Protected WithEvents rbSuppNo As RadioButton
    Protected WithEvents PrintPrev As ImageButton
    Protected WithEvents lblErrToAge1 As Label
    Protected WithEvents lblErrFromAge2 As Label
    Protected WithEvents lblErrToAge2 As Label
    Protected WithEvents lblErrFromAge3 As Label
    Protected WithEvents lblErrToAge3 As Label
    Protected WithEvents lblErrFromAge4 As Label
    Protected WithEvents lblErrToAge4 As Label
    Protected WithEvents lblErrFromAge5 As Label

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

    Dim strUserLoc As String
    Dim strToAge1 As String
    Dim strFromAge2 As String
    Dim strToAge2 As String
    Dim strFromAge3 As String
    Dim strToAge3 As String
    Dim strFromAge4 As String
    Dim strToAge4 As String
    Dim strFromAge5 As String

    Dim objLangCapDs As New Object()
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
                DisplayXmlParameters(strToAge1, strFromAge2, strToAge2, strFromAge3, strToAge3, strFromAge4, strToAge4, strFromAge5)
                lblToAge1.text = strToAge1
                lblFromAge2.text = strFromAge2
                lblToAge2.text = strToAge2
                lblFromAge3.text = strFromAge3
                lblToAge3.text = strToAge3
                lblFromAge4.text = strFromAge4
                lblToAge4.text = strToAge4
                lblFromAge5.text = strFromAge5

                If Trim(strToAge1) <> "" Then
                    txtToAge1.Text = strToAge1
                End If
                If Trim(strFromAge2) <> "" Then
                    txtFromAge2.text = strFromAge2
                End If
                If Trim(strToAge2) <> "" Then
                    txtToAge2.text = strToAge2
                End If
                If Trim(strFromAge3) <> "" Then
                    txtFromAge3.text = strFromAge3
                End If
                If Trim(strToAge3) <> "" Then
                    txtToAge3.text = strToAge3
                End If 
                If Trim(strFromAge4) <> "" Then
                    txtFromAge4.text = strFromAge4
                End If
                If Trim(strToAge4) <> "" Then
                    txtToAge4.text = strToAge4
                End If
                If Trim(strFromAge5) <> "" Then
                    txtFromAge5.text = strFromAge5
                End If
                
                onload_GetLangCap()
                
                txtCutOffDate.text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Now)
            End If
        End If
    End Sub








    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim htmltr As HtmlTableRow

        htmltr = RptSelect.FindControl("SelLocation")
        htmltr.visible = True

        htmltr = RptSelect.FindControl("TrMthYr")
        htmltr.visible = True

        htmltr = RptSelect.FindControl("SelDecimal")
        htmltr.visible = True

        If Page.IsPostBack Then
        end if
    End Sub


    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strBillPartyCode As String
        Dim strCutOffDate As String
        Dim strDec As String
        Dim strSelAccMonth As String
        Dim strSelAccYear As String
        Dim strRptId As String
        Dim strRptName As String
        Dim strSupp As String
        Dim strParam As String

        Dim ddlist As DropDownList
        Dim rdblist As RadioButtonList
        Dim tempUserLoc As HtmlInputHidden
        Dim templblUL As Label
        Dim txt As TextBox
        Dim intCnt As Integer

        Dim objDateFormat As String
        Dim objCutOffDate As String
        Dim strDateSetting As String
        Dim intToAge1 As Integer
        Dim intFromAge2 As Integer
        Dim intToAge2 As Integer
        Dim intFromAge3 As Integer
        Dim intToAge3 As Integer
        Dim intFromAge4 As Integer
        Dim intToAge4 As Integer
        Dim intFromAge5 As Integer

        lblErrToAge1.visible = False
        lblErrFromAge2.visible = False
        lblErrToAge2.visible = False
        lblErrFromAge3.visible = False
        lblErrToAge3.visible = False
        lblErrFromAge4.visible = False
        lblErrToAge4.visible = False
        lblErrFromAge5.visible = False
        

        strBillPartyCode = txtBuyer.text
        strCutOffDate = Trim(txtCutOffDate.text)

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

        tempUserLoc = RptSelect.FindControl("hidUserLoc")

        strUserLoc = Trim(tempUserLoc.Value)
        If strUserLoc = "" Then
            templblUL = RptSelect.FindControl("lblUserLoc")
            templblUL.Visible = True
            Exit Sub
        Else
            If Left(strUserLoc, 3) = "','" Then
                strUserLoc = Right(strUserLoc, Len(strUserLoc) - 3)
            ElseIf Right(strUserLoc, 3) = "','" Then
                strUserLoc = Left(strUserLoc, Len(strUserLoc) - 3)
            End If
        End If
    
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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=AR_STDRPT_DEBTORACCAGEING_GET_CONFIG_DATE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        strDateSetting = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))

        If Not (strCutOffDate) = "" Then
            If objGlobal.mtdValidInputDate(strDateSetting, strCutOffDate, objDateFormat, objCutOffDate) = True Then
            Else
                lblDateFormat.Text = objDateFormat & "."
                lblDate.Visible = True
                lblDateFormat.Visible = True
                Exit Sub
            End If
        End If

        intToAge1 = txtToAge1.text
        intFromAge2 = txtFromAge2.text
        intToAge2 = txtToAge2.text
        intFromAge3 = txtFromAge3.text
        intToAge3 = txtToAge3.text
        intFromAge4 = txtFromAge4.text
        intToAge4 = txtToAge4.text
        intFromAge5 = txtFromAge5.text

        If intToAge1 - 1 < 0 Then
            lblErrToAge1.visible = true
            exit sub
        End If

        If intFromAge2 - intToAge1 <> 1 Then
            lblErrFromAge2.visible = true
            exit sub
        End If

        If intToAge2 - intFromAge2 < 0 Then
            lblErrToAge2.visible = true
            exit sub
        End If

        If intFromAge3 - intToAge2 <> 1 Then
            lblErrFromAge3.visible = true
            exit sub
        End If

        If intToAge3 - intFromAge3 < 0 Then
            lblErrToAge3.visible = true
            exit sub
        End If

        If intFromAge4 - intToAge3 <> 1 Then
            lblErrFromAge4.visible = true
            exit sub
        End If

        If intToAge4 - intFromAge4 < 0 Then
            lblErrToAge4.visible = true
            exit sub
        End If
        
        If intFromAge5 - intToAge4 <> 1 Then
            lblErrFromAge5.visible = true
            exit sub
        End If

        UpdateXmlParameters()

        Response.Write("<Script Language=""JavaScript"">window.open(""AR_StdRpt_DebtorAccountAgeingPreview.aspx?Type=Print&CompName=" & strCompany & _
                       "&Location=" & strUserLoc & _
                       "&SelAccMonth=" & strSelAccMonth & _
                       "&SelAccYear=" & strSelAccYear & _
                       "&RptId=" & strRptId & _
                       "&RptName=" & strRptName & _
                       "&Decimal=" & strDec & _
                       "&Supp=" & strSupp & _
                       "&BillPartyCode=" & Server.UrlEncode(strBillPartyCode) & _
                       "&AccCode=" & Server.UrlEncode(txtAccCode.text) & _
                       "&CutOffDate=" & objCutOffDate & _
                       "&ToAge1=" & txtToAge1.Text & _
                       "&FromAge2=" & txtFromAge2.text & _
                       "&ToAge2=" & txtToAge2.text & _
                       "&FromAge3=" & txtFromAge3.text & _
                       "&ToAge3=" & txtToAge3.text & _
                       "&FromAge4=" & txtFromAge4.text & _
                       "&ToAge4=" & txtToAge4.text & _
                       "&FromAge5=" & txtFromAge5.text & _
                       "&LocationTag=" & lblLocation.text & _
                       "&BillPartyTag=" & lblBillParty.text & _
                       "&AccCodeTag=" & lblAccCode.text & _
                       "&BlkCodeTag=" & lblBlkCode.text & _
                       """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Sub DisplayXmlParameters(ByRef pr_strToAge1 As String, _
                             ByRef pr_strFromAge2 As String, _
                             ByRef pr_strToAge2 As String, _
                             ByRef pr_strFromAge3 As String, _
                             ByRef pr_strToAge3 As String, _
                             ByRef pr_strFromAge4 As String, _
                             ByRef pr_strToAge4 As String, _
                             ByRef pr_strFromAge5 As String)

        Dim objStreamReader As StreamReader
        Dim objDtsXml As New DataSet()
        Dim intErrNo As Integer
        Dim strFtpPath As String
        Dim strFile As String

        pr_strToAge1 = ""
        pr_strFromAge2 = ""
        pr_strToAge2 = ""
        pr_strFromAge3 = ""
        pr_strToAge3 = ""
        pr_strFromAge4 = ""
        pr_strToAge4 = ""
        pr_strFromAge5 = ""

        Try
            intErrNo = objSysCfg.mtdGetFtpPath(strFtpPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=AR_STDRPT_DEBTORACCAGEING_GET_FTPPATH_DISPLAY&errmesg=" & Exp.ToString() & "&redirect=reports/ar_stdrpt_debtoraccountageing.aspx")
        End Try

        strFile = strFtpPath & "param\AR_REPORT_PARAM.xml"

        objDtsXml.EnforceConstraints = False
        objDtsXml.ReadXml(strFile)

        If objDtsXml.Tables(0).Rows.Count > 0 Then
            pr_strToAge1 = objDtsXml.Tables(0).Rows(intCnt).Item("ToAge1")
            pr_strFromAge2 = objDtsXml.Tables(0).Rows(intCnt).Item("FromAge2")
            pr_strToAge2 = objDtsXml.Tables(0).Rows(intCnt).Item("ToAge2")
            pr_strFromAge3 = objDtsXml.Tables(0).Rows(intCnt).Item("FromAge3")
            pr_strToAge3 = objDtsXml.Tables(0).Rows(intCnt).Item("ToAge3")
            pr_strFromAge4 = objDtsXml.Tables(0).Rows(intCnt).Item("FromAge4")
            pr_strToAge4 = objDtsXml.Tables(0).Rows(intCnt).Item("ToAge4")
            pr_strFromAge5 = objDtsXml.Tables(0).Rows(intCnt).Item("FromAge5")
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=AR_STDRPT_DEBTORACCAGEING_GET_FTPPATH_UPD&errmesg=" & Exp.ToString() & "&redirect=reports/ar_stdrpt_debtoraccountageing.aspx")
        End Try

        strFile = strFtpPath & "param\AR_REPORT_PARAM.xml"

        objStreamReader = File.OpenText(strFile)
        strXmlSrc = objStreamReader.ReadToEnd()
        objStreamReader.Close()

        strXmlSrc = Replace(strXmlSrc, "<ToAge1>" & lblToAge1.Text & "</ToAge1>", "<ToAge1>" & txtToAge1.Text & "</ToAge1>")
        strXmlSrc = Replace(strXmlSrc, "<FromAge2>" & lblFromAge2.Text & "</FromAge2>", "<FromAge2>" & txtFromAge2.text & "</FromAge2>")
        strXmlSrc = Replace(strXmlSrc, "<ToAge2>" & lblToAge2.text & "</ToAge2>", "<ToAge2>" & txtToAge2.text & "</ToAge2>")
        strXmlSrc = Replace(strXmlSrc, "<FromAge3>" & lblFromAge3.text & "</FromAge3>", "<FromAge3>" & txtFromAge3.text & "</FromAge3>")
        strXmlSrc = Replace(strXmlSrc, "<ToAge3>" & lblToAge3.text & "</ToAge3>", "<ToAge3>" & txtToAge3.text & "</ToAge3>")
        strXmlSrc = Replace(strXmlSrc, "<FromAge4>" & lblFromAge4.text & "</FromAge4>", "<FromAge4>" & txtFromAge4.text & "</FromAge4>")
        strXmlSrc = Replace(strXmlSrc, "<ToAge4>" & lblToAge4.text & "</ToAge4>", "<ToAge4>" & txtToAge4.text & "</ToAge4>")
        strXmlSrc = Replace(strXmlSrc, "<FromAge5>" & lblFromAge5.text & "</FromAge5>", "<FromAge5>" & txtFromAge5.text & "</FromAge5>")
        Dim objStreamWrite As StreamWriter = New StreamWriter(strFile)
        objStreamWrite.Write(strXmlSrc)
        objStreamWrite.Close()

        lblToAge1.Text = txtToAge1.Text
        lblFromAge2.text = txtFromAge2.text
        lblToAge2.text = txtToAge2.text
        lblFromAge3.text = txtFromAge3.text
        lblToAge3.text = txtToAge3.text
        lblFromAge4.text = txtFromAge4.text
        lblToAge4.text = txtToAge4.text
        lblFromAge5.text = txtFromAge5.text

    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblLocation.text = GetCaption(objLangCap.EnumLangCap.Location)
        lblBillParty.text = GetCaption(objLangCap.EnumLangCap.BillParty)
        lblAccCode.text = GetCaption(objLangCap.EnumLangCap.Account) & " " & lblCode.text
        lblBlkCode.text = GetCaption(objLangCap.EnumLangCap.Block) & " " & lblCode.text
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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=AR_STDRPT_DEBTORACCAGE_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/IN_StdRpt_Selection.aspx")
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
