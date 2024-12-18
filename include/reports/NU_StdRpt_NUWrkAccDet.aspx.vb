Imports System
Imports System.Data
Imports System.IO
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports System.Xml
Imports System.Web.Services

Public Class NU_StdRpt_NUWrkAccDet : Inherits Page

    Protected RptSelect As UserControl

    Dim objNUReport As New agri.NU.clsReport()
    Dim objNUSetup As New agri.NU.clsSetup()
    Dim objNUTrx As New agri.NU.clsTrx()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Protected WithEvents lblDate As Label
    Protected WithEvents lblDateFormat As Label
    Protected WithEvents lblErrAccMonth As Label
    Protected WithEvents lblErrAccYear As Label
    Protected WithEvents lblErrAccCode As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label

    Protected WithEvents lblBlkCode As Label
    Protected WithEvents lblAccCode As Label
    Protected WithEvents lblXmlAccCode As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents lblCode As Label

    Protected WithEvents txtBlkCode As TextBox
    Protected WithEvents txtAccCode As TextBox

    Protected WithEvents PrintPrev As ImageButton

    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfigsetting As String
    Dim strAccCode As String

    Dim dr As DataRow
    Dim intCnt As Integer
    Dim intErrNo As Integer
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_NUACCMONTH")
        strAccYear = Session("SS_NUACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")

        lblDate.Visible = False
        lblDateFormat.Visible = False
        lblErrAccMonth.Visible = False
        lblErrAccYear.Visible = False
        lblErrAccCode.Visible = False

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
                onload_GetLangCap()
                DisplayAccCode(strAccCode)
                lblXmlAccCode.Text = strAccCode
                If strAccCode <> "" Then
                    txtAccCode.Text = strAccCode
                End If
            End If
        End If
    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim htmltr As HtmlTableRow
        Dim htmltc As HtmlTableCell

        htmltr = RptSelect.FindControl("TrMthYr")
        htmltr.Visible = True
    End Sub

    Sub onload_GetLangCap()

        GetEntireLangCap()
        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
            lblBlkCode.Text = GetCaption(objLangCap.EnumLangCap.NurseryBlock) & " Code"
        Else
            lblBlkCode.Text = GetCaption(objLangCap.EnumLangCap.NurserySubBlock) & " Code"
        End If
        lblAccCode.Text = "Distribution Account"
        lblLocation.Text = GetCaption(objLangCap.EnumLangCap.Location)
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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PWSYSTEM_CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/NU_StdRpt_Selection.aspx")
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

    Sub DisplayAccCode(ByRef pr_strAccCode As String)
        Dim objStreamReader As StreamReader
        Dim objDtsXml As New DataSet()
        Dim intErrNo As Integer
        Dim strFtpPath As String
        Dim strFile As String


        pr_strAccCode = ""

        Try
            intErrNo = objSysCfg.mtdGetFtpPath(strFtpPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_GET_FTPPATH&errmesg=" & Exp.ToString() & "&redirect=reports/nu_stdrpt_nuwrkaccdet.aspx")
        End Try

        strFile = strFtpPath & "param\NU_REPORT_PARAM.xml"

        objDtsXml.EnforceConstraints = False
        objDtsXml.ReadXml(strFile)

        If objDtsXml.Tables(0).Rows.Count > 0 Then
            pr_strAccCode = objDtsXml.Tables(0).Rows(intCnt).Item("NurseryDistAccCode")
        End If
        objDtsXml = Nothing
    End Sub

    Sub UpdateAccCode()
        Dim objStreamReader As StreamReader
        Dim strXmlSrc As String
        Dim intErrNo As Integer
        Dim strFtpPath As String
        Dim strFile As String

        Try
            intErrNo = objSysCfg.mtdGetFtpPath(strFtpPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_GET_FTPPATH&errmesg=" & Exp.ToString() & "&redirect=reports/nu_stdrpt_nuwrkaccdet.aspx")
        End Try

        strFile = strFtpPath & "param\NU_REPORT_PARAM.xml"

        objStreamReader = File.OpenText(strFile)
        strXmlSrc = objStreamReader.ReadToEnd()
        objStreamReader.Close()

        strXmlSrc = Replace(strXmlSrc, "<NurseryDistAccCode>" & lblXmlAccCode.Text & "</NurseryDistAccCode>", "<NurseryDistAccCode>" & txtAccCode.Text & "</NurseryDistAccCode>")

        Dim objStreamWrite As StreamWriter = New StreamWriter(strFile)
        objStreamWrite.Write(strXmlSrc)
        objStreamWrite.Close()

        lblXmlAccCode.Text = txtAccCode.Text
    End Sub

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strBlkCode As String

        Dim strddlAccMth As String
        Dim strddlAccYr As String
        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String

        Dim intddlAccMth As Integer
        Dim intddlAccYr As Integer

        Dim tempAccMth As DropDownList
        Dim tempAccYr As DropDownList
        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden

        Dim strParam As String
        Dim strDateSetting As String
        Dim objSysCfgDs As New Object()
        Dim objDateFormat As New Object()
        Dim objDateFrom As String
        Dim objDateTo As String

        tempAccMth = RptSelect.FindControl("lstAccMonth")
        strddlAccMth = Trim(tempAccMth.SelectedItem.Value)
        tempAccYr = RptSelect.FindControl("lstAccYear")
        strddlAccYr = Trim(tempAccYr.SelectedItem.Value)

        intddlAccMth = CInt(strddlAccMth)
        intddlAccYr = CInt(strddlAccYr)

        tempRpt = RptSelect.FindControl("lstRptName")
        strRptID = Trim(tempRpt.SelectedItem.Value)
        strRptName = Trim(tempRpt.SelectedItem.Text)
        tempUserLoc = RptSelect.FindControl("hidUserLoc")
        strUserLoc = Trim(tempUserLoc.Value)
        tempDec = RptSelect.FindControl("lstDecimal")
        strDec = Trim(tempDec.SelectedItem.Value)

        If txtAccCode.Text = "" Then
            lblErrAccCode.Visible = True
            Exit Sub
        End If

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

        If txtBlkCode.Text = "" Then
            strBlkCode = ""
        Else
            strBlkCode = Trim(txtBlkCode.Text)
        End If

        strBlkCode = Server.UrlEncode(strBlkCode)

        UpdateAccCode()

        Response.Write("<Script Language=""JavaScript"">window.open(""NU_StdRpt_NUWrkAccDetPreview.aspx?Type=Print&CompName=" & strCompany & _
                        "&Location=" & strUserLoc & "&RptID=" & strRptID & _
                        "&RptName=" & strRptName & "&Decimal=" & strDec & _
                        "&DDLAccMth=" & strddlAccMth & "&DDLAccYr=" & strddlAccYr & "&AccCode=" & txtAccCode.Text & _
                        "&lblLocation=" & lblLocation.Text & "&lblBlkCode=" & lblBlkCode.Text & _
                        "&BlkCode=" & strBlkCode & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

End Class
