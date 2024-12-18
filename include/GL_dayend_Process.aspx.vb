Imports System
Imports System.Data
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic

Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.GlobalHdl.clsAccessRights
Imports agri.GL.clsMthEnd


Public Class GL_dayend_Process: Inherits Page


    Protected WithEvents ddlAccMonth As DropDownList
    Protected WithEvents ddlAccYear As DropDownList
    Protected WithEvents lblErrProcess As System.Web.UI.WebControls.Label
    Protected WithEvents btnProceed As System.Web.UI.WebControls.ImageButton

    Protected WithEvents dgViewJournal As DataGrid
    Protected WithEvents dgViewActive As DataGrid
    Protected WithEvents lblBalance As Label
    Protected WithEvents lblTotalBalance As Label

    Protected WithEvents chkValidate As CheckBox

    Dim objAR As New agri.GlobalHdl.clsAccessRights
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl
    Dim objAdmAcc As New agri.Admin.clsAccPeriod()
    Dim objGLTrx As New agri.GL.ClsTrx

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String = ""
    Dim strAccYear As String = ""
    Dim intConfig As Integer
    Dim intGLAR As Integer
    Dim objLangCapDs As New DataSet

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intConfig = Session("SS_CONFIGSETTING")
        intGLAR = Session("SS_GLAR")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")


        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLMonthEnd), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else

            lblErrProcess.Visible = False

            If Not Page.IsPostBack Then
                btnProceed.Attributes("onclick") = "javascript:return ConfirmAction('Process Trial Month End');"
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
            ddlAccYear.Items.Clear()


            For intCnt = (Convert.ToInt16(strAccYear) - 1) To iif(ucase(trim(strCompany)) = "STA", Convert.ToInt16(strAccYear) + 2, Convert.ToInt16(strAccYear) + 1)
                ddlAccYear.Items.Add(New ListItem(intCnt, intCnt))
            Next
          
            'ddlAccYear.SelectedIndex = ddlAccYear.Items.Count - 2
            ddlAccYear.Text = strAccYear
            _strAccYear = ddlAccYear.SelectedItem.Value
        Else
            _strAccYear = pv_strAccYear
        End If

        ddlAccMonth.Items.Clear()
        If _strAccYear = strAccYear Then
            intAccMonth = 12
            For intCnt = 1 To intAccMonth
                ddlAccMonth.Items.Add(New ListItem(intCnt, intCnt))
            Next
            'ddlAccMonth.SelectedIndex = Val(strAccMonth)
            ddlAccMonth.Text = Val(strAccMonth)
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
                lblErrProcess.Visible = True
                btnProceed.Visible = False
            End Try
            objAccCfg = Nothing

            For intCnt = 1 To 12
                ddlAccMonth.Items.Add(New ListItem(intCnt, intCnt))
            Next
            'ddlAccMonth.SelectedIndex = Val(strAccMonth)
            ddlAccMonth.Text = Val(strAccMonth)
        End If
    End Sub

    Sub OnIndexChage_ReloadAccPeriod(ByVal sender As Object, ByVal e As System.EventArgs)
        'OnLoad_Display(ddlAccYear.SelectedItem.Value)
    End Sub

    Sub btnProceed_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)

        Dim intErrNo As Integer
        Dim dsResult As New Object
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim strOpCode As String = "GL_MONTHEND_TRIALCLOSING"
        Dim strOpCodeStatus As String = "GL_MONTHEND_CHECK_STATUS"

        Dim intEndPeriod As Integer
        Dim intSelPeriod As Integer
        Dim strEndAccMonth As String = ""
        Dim strEndAccYear As String = ""

        strEndAccMonth = IIf(Session("SS_GLACCMONTH") <> "", Session("SS_GLACCMONTH"), Month(Now()))
        strEndAccYear = IIf(Session("SS_GLACCYEAR") <> "", Session("SS_GLACCYEAR"), Year(Now()))

        strAccMonth = ddlAccMonth.SelectedValue
        strAccYear = ddlAccYear.SelectedValue

        intEndPeriod = (CInt(strEndAccYear) * 100) + CInt(strEndAccMonth)
        intSelPeriod = (CInt(strAccYear) * 100) + CInt(strAccMonth)

        If intSelPeriod < intEndPeriod Then
            lblErrProcess.Visible = True
            lblErrProcess.Text = "Invalid period"
            Exit Sub
        End If


        strParamName = "LOCCODE|ACCMONTH|ACCYEAR|USERID"
        strParamValue = strLocation & "|" & ddlAccMonth.SelectedItem.Value & _
                        "|" & ddlAccYear.SelectedItem.Value & "|" & _
                        Session("SS_USERID")

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCode, _
                                                    strParamName, _
                                                    strParamValue, _
                                                    dsResult)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_DAYEND_PROCESS&errmesg=" & Exp.Message.ToString & "&redirect=")
        End Try

        lblErrProcess.Visible = True
        lblErrProcess.Text = trim(dsResult.Tables(0).Rows(0).Item("Msg"))


        If chkValidate.Checked = True Then
            LoadUnActive()
        End If

        
        If chkValidate.Checked = False Then
            
        End If
    End Sub

    
    Private Sub LoadUnActive()
        Dim intErrNo As Integer
        Dim dsResult As New Object
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim strOpCode As String = "GL_MONTHEND_CHECK_UNACTIVE"
        Dim arrPeriod As Array

        strParamName = "LOCCODE|ACCMONTH|ACCYEAR"
        strParamValue = strLocation & "|" & ddlAccMonth.SelectedItem.Value & _
                        "|" & ddlAccYear.SelectedItem.Value

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCode, _
                                                    strParamName, _
                                                    strParamValue, _
                                                    dsResult)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_DAYEND_PROCESS&errmesg=" & Exp.Message.ToString & "&redirect=")
        End Try

        If dsResult.Tables(0).Rows.Count > 0 Then
            Dim intCnt As Integer
            dgViewActive.Visible = True
            dgViewActive.DataSource = Nothing
            dgViewActive.DataSource = dsResult.Tables(0)
            dgViewActive.DataBind()
        Else
            dgViewActive.Visible = False
        End If

    End Sub

    Private Sub LoadUnBalance()
        Dim intErrNo As Integer
        Dim dsResult As New Object
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim strOpCode As String = "GL_MONTHEND_CHECK_UNBALANCE"
        Dim arrPeriod As Array

        strParamName = "LOCCODE|ACCMONTH|ACCYEAR"
        strParamValue = strLocation & "|" & ddlAccMonth.SelectedItem.Value & _
                        "|" & ddlAccYear.SelectedItem.Value

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCode, _
                                                    strParamName, _
                                                    strParamValue, _
                                                    dsResult)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_DAYEND_PROCESS&errmesg=" & Exp.Message.ToString & "&redirect=")
        End Try

        If dsResult.Tables(0).Rows.Count > 0 Then
            Dim TotalBalance As Double
            Dim intCnt As Integer
            For intCnt = 0 To dsResult.Tables(0).Rows.Count - 1
                TotalBalance += dsResult.Tables(0).Rows(intCnt).Item("Balance")
            Next
            lblBalance.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(TotalBalance, 2), 2)

            dgViewJournal.Visible = True
            dgViewJournal.DataSource = Nothing
            dgViewJournal.DataSource = dsResult.Tables(0)
            dgViewJournal.DataBind()

            lblBalance.Visible = True
            lblTotalBalance.Visible = True
            lblTotalBalance.Text = "Total UnBalance Amount : "
        Else
            lblBalance.Visible = False
            lblTotalBalance.Visible = False
            dgViewJournal.Visible = False
        End If

    End Sub
End Class
