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

Public Class AP_StdRpt_SuppAccAgeList : Inherits Page

    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdminLoc As New agri.Admin.clsLoc()


    Dim objSysCfgDs As New Object()
    Dim objLangCapDs As New Object()

    Protected RptSelect As UserControl

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label

    Protected WithEvents lblLocation As Label
    Protected WithEvents lblAccCode As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents txtSupplier As TextBox
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

    Protected WithEvents lblToAge1 As Label
    Protected WithEvents lblFromAge2 As Label
    Protected WithEvents lblToAge2 As Label
    Protected WithEvents lblFromAge3 As Label
    Protected WithEvents lblToAge3 As Label
    Protected WithEvents lblFromAge4 As Label
    Protected WithEvents lblToAge4 As Label
    Protected WithEvents lblFromAge5 As Label

    Protected WithEvents lblErrToAge1 As Label
    Protected WithEvents lblErrFromAge2 As Label
    Protected WithEvents lblErrToAge2 As Label
    Protected WithEvents lblErrFromAge3 As Label
    Protected WithEvents lblErrToAge3 As Label
    Protected WithEvents lblErrFromAge4 As Label
    Protected WithEvents lblErrToAge4 As Label
    Protected WithEvents lblErrFromAge5 As Label

    Protected WithEvents lblDate As Label
    Protected WithEvents lblDateFormat As Label
    Protected WithEvents lblErrAge As Label
    Protected WithEvents rbSuppYes As RadioButton
    Protected WithEvents rbSuppNo As RadioButton

    Protected WithEvents PrintPrev As ImageButton

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String

    Dim dr As DataRow
    Dim intCnt As Integer
    Dim intErrNo As Integer

    Dim strToAge1 As String
    Dim strFromAge2 As String
    Dim strToAge2 As String
    Dim strFromAge3 As String
    Dim strToAge3 As String
    Dim strFromAge4 As String
    Dim strToAge4 As String
    Dim strFromAge5 As String
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_APACCMONTH")
        strAccYear = Session("SS_APACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
                DisplayXmlParameters(strToAge1, strFromAge2, strToAge2, strFromAge3, strToAge3, strFromAge4, strToAge4, strFromAge5)
                lblToAge1.Text = strToAge1
                lblFromAge2.Text = strFromAge2
                lblToAge2.Text = strToAge2
                lblFromAge3.Text = strFromAge3
                lblToAge3.Text = strToAge3
                lblFromAge4.Text = strFromAge4
                lblToAge4.Text = strToAge4
                lblFromAge5.Text = strFromAge5

                If Trim(strToAge1) <> "" Then
                    txtToAge1.Text = strToAge1
                End If
                If Trim(strFromAge2) <> "" Then
                    txtFromAge2.Text = strFromAge2
                End If
                If Trim(strToAge2) <> "" Then
                    txtToAge2.Text = strToAge2
                End If
                If Trim(strFromAge3) <> "" Then
                    txtFromAge3.Text = strFromAge3
                End If
                If Trim(strToAge3) <> "" Then
                    txtToAge3.Text = strToAge3
                End If
                If Trim(strFromAge4) <> "" Then
                    txtFromAge4.Text = strFromAge4
                End If
                If Trim(strToAge4) <> "" Then
                    txtToAge4.Text = strToAge4
                End If
                If Trim(strFromAge5) <> "" Then
                    txtFromAge5.Text = strFromAge5
                End If

                onload_GetLangCap()

                txtCutOffDate.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Now)

            End If

        End If
    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim ucTrMthYr As HtmlTableRow

        ucTrMthYr = RptSelect.FindControl("TrMthYr")
        ucTrMthYr.Visible = True

    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        lblLocation.Text = GetCaption(objLangCap.EnumLangCap.Location)
        lblAccCode.Text = GetCaption(objLangCap.EnumLangCap.Account) & " " & lblCode.Text

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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=AP_STDRPT_SUPPACCAGE_LIST_CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/AP_StdRpt_Selection.aspx")
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=AP_STDRPT_SUPPDEBTORACCAGEING_GET_FTPPATH_DISPLAY&errmesg=" & Exp.ToString() & "&redirect=reports/ap_stdrpt_SuppAccAgeList.aspx")
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=AP_STDRPT_SUPPDEBTORACCAGEING_GET_FTPPATH_UPD&errmesg=" & Exp.ToString() & "&redirect=reports/ap_stdrpt_SuppAccAgeList.aspx")
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

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strCutOffDate As String
        Dim strSupplier As String
        Dim intToAge1 As Integer
        Dim intFromAge2 As Integer
        Dim intToAge2 As Integer
        Dim intFromAge3 As Integer
        Dim intToAge3 As Integer
        Dim intFromAge4 As Integer
        Dim intToAge4 As Integer
        Dim intFromAge5 As Integer

        Dim objDateFormat As String
        Dim objCutOffDate As String
        Dim strDateSetting As String

        Dim strddlAccMth As String
        Dim strddlAccYr As String
        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String
        Dim strSupp As String

        Dim tempAccMth As DropDownList
        Dim tempAccYr As DropDownList
        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden

        Dim strParam As String

        lblErrToAge1.Visible = False
        lblErrFromAge2.Visible = False
        lblErrToAge2.Visible = False
        lblErrFromAge3.Visible = False
        lblErrToAge3.Visible = False
        lblErrFromAge4.Visible = False
        lblErrToAge4.Visible = False
        lblErrFromAge5.Visible = False

        tempAccMth = RptSelect.FindControl("lstAccMonth")
        strddlAccMth = Trim(tempAccMth.SelectedItem.Value)
        tempAccYr = RptSelect.FindControl("lstAccYear")
        strddlAccYr = Trim(tempAccYr.SelectedItem.Value)
        tempRpt = RptSelect.FindControl("lstRptName")
        strRptID = Trim(tempRpt.SelectedItem.Value)
        strRptName = Trim(tempRpt.SelectedItem.Text)
        tempUserLoc = RptSelect.FindControl("hidUserLoc")
        strUserLoc = Trim(tempUserLoc.Value)
        tempDec = RptSelect.FindControl("lstDecimal")
        strDec = Trim(tempDec.SelectedItem.Value)

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

        strCutOffDate = txtCutOffDate.Text.Trim
        If txtSupplier.Text = "" Then
            strSupplier = ""
        Else
            strSupplier = Trim(txtSupplier.Text)
        End If

        strSupplier = Server.UrlEncode(strSupplier)

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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=AP_STDRPT_SUPPACCAGEING_GET_CONFIG_DATE&errmesg=" & lblErrMessage.Text & "&redirect=reports/ap_stdrpt_SuppAccAgeList.aspx")
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

        intToAge1 = txtToAge1.Text
        intFromAge2 = txtFromAge2.Text
        intToAge2 = txtToAge2.Text
        intFromAge3 = txtFromAge3.Text
        intToAge3 = txtToAge3.Text
        intFromAge4 = txtFromAge4.Text
        intToAge4 = txtToAge4.Text
        intFromAge5 = txtFromAge5.Text

        If intToAge1 - 1 < 0 Then
            lblErrToAge1.Visible = True
            Exit Sub
        End If

        If intFromAge2 - intToAge1 <> 1 Then
            lblErrFromAge2.Visible = True
            Exit Sub
        End If

        If intToAge2 - intFromAge2 < 0 Then
            lblErrToAge2.Visible = True
            Exit Sub
        End If

        If intFromAge3 - intToAge2 <> 1 Then
            lblErrFromAge3.Visible = True
            Exit Sub
        End If

        If intToAge3 - intFromAge3 < 0 Then
            lblErrToAge3.Visible = True
            Exit Sub
        End If

        If intFromAge4 - intToAge3 <> 1 Then
            lblErrFromAge4.Visible = True
            Exit Sub
        End If

        If intToAge4 - intFromAge4 < 0 Then
            lblErrToAge4.Visible = True
            Exit Sub
        End If

        If intFromAge5 - intToAge4 <> 1 Then
            lblErrFromAge5.Visible = True
            Exit Sub
        End If

        UpdateXmlParameters()

        Response.Write("<Script Language=""JavaScript"">window.open(""AP_StdRpt_SuppAccAgeListPreview.aspx?CompName=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & strDec & _
                       "&DDLAccMth=" & strddlAccMth & "&DDLAccYr=" & strddlAccYr & "&Suppress=" & strSupp & _
                       "&lblLocation=" & lblLocation.Text & _
                       "&AccCodeTag=" & lblAccCode.Text & _
                       "&Supplier=" & strSupplier & _
                       "&CutOffDate=" & objCutOffDate & _
                       "&ToAge1=" & txtToAge1.Text & _
                       "&FromAge2=" & txtFromAge2.Text & _
                       "&ToAge2=" & txtToAge2.Text & _
                       "&FromAge3=" & txtFromAge3.Text & _
                       "&ToAge3=" & txtToAge3.Text & _
                       "&FromAge4=" & txtFromAge4.Text & _
                       "&ToAge4=" & txtToAge4.Text & _
                       "&FromAge5=" & txtFromAge5.Text & _
                       """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

    End Sub

End Class
