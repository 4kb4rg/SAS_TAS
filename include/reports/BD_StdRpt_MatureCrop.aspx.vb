Imports System
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

Public Class BD_StdRpt_MatureCrop : Inherits Page

    Protected RptSelect As UserControl

    Dim objBD As New agri.BD.clsReport()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblAccCodeTag As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblCrop As Label
    Protected WithEvents lblBlkCodeTag As Label
    Protected WithEvents lblSubBlkCodeTag As Label
    Protected WithEvents txtBlkCode As TextBox
    Protected WithEvents txtSubBlkCode As TextBox

    Protected WithEvents PrintPrev As ImageButton
    Protected WithEvents TrBlk As HtmlTableRow
    Protected WithEvents TrSubBlk As HtmlTableRow

    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strUserLoc As String
    Dim intConfigsetting As Integer

    Dim intCnt As Integer
    Dim intErrNo As Integer
    Dim dr As DataRow
    Dim strLocType As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
                onload_GetLangCap()
            End If

            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
                TrBlk.Visible = True
                TrSubBlk.Visible = False
            Else
                TrBlk.Visible = False
                TrSubBlk.Visible = True
            End If
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        lblCrop.Text = UCase(GetCaption(objLangCap.EnumLangCap.Crop))
        lblBlkCodeTag.Text = GetCaption(objLangCap.EnumLangCap.Block) & " Code :"
        lblSubBlkCodeTag.Text = GetCaption(objLangCap.EnumLangCap.SubBlock) & " Code :"
        lblTitle.Text = "BUDGETING - MATURE CROP ACTIVITY"
        lblAccCodeTag.Text = GetCaption(objLangCap.EnumLangCap.Account) & " Code"
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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=BD_STDRPT_MATURECROP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/BD_StdRpt_Selection.aspx")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

        For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If Trim(strLocType) = objAdminLoc.EnumLocType.Mill Then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                Else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                End If
                Exit For
            End If
        Next
    End Function


    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strBlkCode As String
        Dim strSubBlkCode As String

        Dim strddlPeriodID As String
        Dim strddlPeriodName As String
        Dim strRptID As String
        Dim strRptName As String
        Dim strDec As String

        Dim tempPeriod As DropDownList
        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim lblPeriod As Label

        tempPeriod = RptSelect.FindControl("lstPeriodName")
        strddlPeriodID = Trim(tempPeriod.SelectedItem.Value)
        strddlPeriodName = Trim(tempPeriod.SelectedItem.Text)
        tempRpt = RptSelect.FindControl("lstRptName")
        strRptID = Trim(tempRpt.SelectedItem.Value)
        strRptName = Trim(tempRpt.SelectedItem.Text)
        tempDec = RptSelect.FindControl("lstDecimal")
        strDec = Trim(tempDec.SelectedItem.Value)


        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
            strBlkCode = txtBlkCode.Text.Trim
            strBlkCode = Server.UrlEncode(strBlkCode)
        Else
            strSubBlkCode = txtSubBlkCode.Text.Trim
            strSubBlkCode = Server.UrlEncode(strSubBlkCode)
        End If

        If strddlPeriodID = 0 Then
            lblPeriod = RptSelect.FindControl("lblPeriod")
            lblPeriod.Visible = True
            Exit Sub
        Else
            lblPeriod = RptSelect.FindControl("lblPeriod")
            lblPeriod.Visible = False
        End If

        Response.Write("<Script Language=""JavaScript"">window.open(""BD_StdRpt_MatureCropPreview.aspx?DDLPeriodID=" & strddlPeriodID & "&DDLPeriodName=" & strddlPeriodName & _
                       "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & strDec & "&lblCrop=" & lblCrop.Text & "&BlkCode=" & strBlkCode & "&SubBlkCode=" & strSubBlkCode & _
                       "&lblBlkCodeTag=" & lblBlkCodeTag.Text & "&lblSubBlkCodeTag=" & lblSubBlkCodeTag.Text & "&lblAccCodeTag=" & lblAccCodeTag.Text & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

    End Sub

End Class
