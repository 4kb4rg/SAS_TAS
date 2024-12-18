

Imports System
Imports System.Data
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information

Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.GlobalHdl.clsAccessRights
Imports agri.GL.clsMthEnd
Imports agri.Admin.clsShare


Public Class GL_mthend_Journal : Inherits Page

    Protected WithEvents btnProceed As ImageButton
    Protected WithEvents ddlAccMonth As DropDownList
    Protected WithEvents ddlAccYear As DropDownList
    Protected WithEvents lblErrLic As Label
    Protected WithEvents lblSuccess As Label
    Protected WithEvents lblErrPosted As Label
    Protected WithEvents lblErrOther As Label
    Protected WithEvents lblMessage as Label

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objGLMthEnd As New agri.GL.clsMthEnd()
    Dim objAdmAcc As New agri.Admin.clsAccPeriod()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intConfig As Integer
    Dim intGLAR As Integer

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intConfig = Session("SS_CONFIGSETTING")
        intGLAR = Session("SS_GLAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLJrnMonthEnd), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrLic.Visible = False
            lblSuccess.Visible = False
            lblErrPosted.Visible = False
            lblErrOther.Visible = False
            lblMessage.Visible = false

            If Not Page.IsPostBack Then
                onLoad_Display("")
            End If
        End If
    End Sub

    Sub OnLoad_Display(ByVal pv_strAccYear As String)
        Dim objAccCfg As New Dataset()
        Dim strOpCd_AccCfg_Get As String = "ADMIN_CLSACCPERIOD_CONFIG_ACCYEAR_GET"
        Dim intMaxPeriod As Integer
        Dim intAccMonth As Integer
        Dim _strAccYear As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strParam As String

        If pv_strAccYear = "" Then
            ddlAccYear.Items.Clear
            If strAccMonth > 1 Then
                For intCnt = (Convert.ToInt16(strAccYear) - 1) to Convert.ToInt16(strAccYear)
                    ddlAccYear.Items.Add(New ListItem(intCnt, intCnt))
                Next
            Else
                For intCnt = (Convert.ToInt16(strAccYear) - 1) to (Convert.ToInt16(strAccYear) - 1)
                    ddlAccYear.Items.Add(New ListItem(intCnt, intCnt))
                Next
            End If
            ddlAccYear.SelectedIndex = 0
            _strAccYear = ddlAccYear.SelectedItem.Value
        Else
            _strAccYear = pv_strAccYear
        End If

        ddlAccMonth.Items.Clear()
        If _strAccYear = strAccYear Then      
            intAccMonth = Convert.ToInt16(strAccMonth) - 1
            For intCnt = 1 To intAccMonth
                ddlAccMonth.Items.Add(New ListItem(intCnt, intCnt))
            Next
            ddlAccMonth.SelectedIndex = intAccMonth - 1
        Else    
            Try
                strParam = "||" & _strAccYear
                intErrNo = objAdmAcc.mtdGetAccPeriodCfg(strOpCd_AccCfg_Get, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strParam, _
                                                        objAccCfg)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_TRX_JOURNALADJ_DETAIL_ACCCFG_GET&errmesg=" & Exp.ToString() & "&redirect=GL/trx/GL_trx_JournalAdj_List.aspx")
            End Try

            Try
                btnProceed.Visible = True
                intAccMonth = Convert.ToInt16(objAccCfg.Tables(0).Rows(0).Item("MaxPeriod"))
            Catch Exp As System.Exception
                lblMessage.Visible = true 
                btnProceed.Visible = False
            End Try
            objAccCfg = Nothing

            For intCnt = 1 To intAccMonth + 1
                ddlAccMonth.Items.Add(New ListItem(intCnt, intCnt))
            Next
            ddlAccMonth.SelectedIndex = intAccMonth
        End If
    End Sub

    Sub OnIndexChage_ReloadAccPeriod(ByVal sender As Object, ByVal e As System.EventArgs)
        OnLoad_Display(ddlAccYear.SelectedItem.Value)
    End Sub

    Sub btnProceed_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCd As String = ""
        Dim intErrNo As Integer
        Dim strParam As String = ""
        Dim objResult As New Object()

        Try
            strParam = Convert.ToString(objGlobal.EnumModule.GeneralLedger) & "|" & _
                       ddlAccMonth.SelectedItem.Value & "|" & _
                       ddlAccYear.SelectedItem.Value
            intErrNo = objGLMthEnd.mtdMonthEndJournalAdj(strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strOpCd, _
                                                        strParam, _
                                                        objResult)
        Catch Exp As System.Exception 
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_MTHEND_JOURNAL_PROCESS&errmesg=" & Exp.ToString & "&redirect=GL/mthend/GL_mthend_Journal.aspx")
        End Try

        Select Case objResult
            Case 0 : lblErrLic.Visible = True
            Case 1 : lblSuccess.Visible = True
            Case 11 : lblErrPosted.Visible = True
            Case Else : lblErrOther.Visible = True
        End Select
    End Sub

End Class
