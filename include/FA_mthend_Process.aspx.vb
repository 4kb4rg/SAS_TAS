Imports System
Imports System.Data
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information

Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.GlobalHdl.clsAccessRights
Imports agri.FA.clsMthEnd
Imports agri.Admin.clsShare


Public Class FA_mthend_Process : Inherits Page

    Protected WithEvents lblAccPeriod As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblLastProcessDate As Label
    Protected WithEvents btnProceed As ImageButton
    Protected WithEvents lblErrNotClose As Label
    Protected WithEvents lblErrProcess As Label
    Protected WithEvents lblErrDocument As Label
    Protected WithEvents btnRollBack As ImageButton

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAdminShare As New agri.Admin.clsShare()
    Dim objMthEndDs As New agri.FA.clsMthEnd()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objFATrx As New agri.FA.clsTrx()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intFAAR As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_FAACCMONTH")
        strAccYear = Session("SS_FAACCYEAR")
        intFAAR = Session("SS_FAAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAMonthEnd), intFAAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrNotClose.Visible = False
            lblErrProcess.Visible = False
            lblErrDocument.Visible = False

            If Not Page.IsPostBack Then
                onLoad_Display()
            End If
        End If
    End Sub


    Sub btnRollBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)

        Dim intErrNo As Integer
        Dim strParamName As String
        Dim strParamValue As String
        Dim strOpCode As String
        Dim objDataSet As DataSet

        strOpCode = "FA_CLSMTHEND_ROLLBACK"
        strParamName = "LOCCODE|ACCMONTH|ACCYEAR|USERID"
        strParamValue = strLocation & "|" & strAccMonth & "|" & strAccYear & "|" & strUserId

        Try
            intErrNo = objFATrx.mtdGetDataCommon(strOpCode, _
                                               strParamName, _
                                               strParamValue, _
                                               objDataSet)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=FA_MTHEND_ROLLBACK&errmesg=&redirect=")
        End Try


        If objDataSet.Tables(0).Rows(0).Item("Msg").Trim() = "OK" Then
            lblErrDocument.Text = "Rollback Succeed.."
            lblErrDocument.forecolor = System.Drawing.Color.Blue
            lblErrDocument.Visible = True


            Session("SS_FAACCMONTH") = objDataSet.Tables(0).Rows(0).Item("AccMonth").Trim()
            Session("SS_FAACCYEAR") = objDataSet.Tables(0).Rows(0).Item("AccYear").Trim()

            strAccMonth = objDataSet.Tables(0).Rows(0).Item("AccMonth").Trim()
            strAccYear = objDataSet.Tables(0).Rows(0).Item("AccYear").Trim()

            'response.write(strAccMonth & " - " & strAccYear)

            onLoad_Display()

        ElseIf objDataSet.Tables(0).Rows(0).Item("Msg") = "NO" Then
            lblErrDocument.Text = "No Data To RollBack!"
            lblErrDocument.forecolor = System.Drawing.Color.Red
            lblErrDocument.Visible = True

        Else
            lblErrDocument.Text = objDataSet.Tables(0).Rows(0).Item("Msg")
            lblErrDocument.Visible = True
        End If


    End Sub


    Sub onLoad_Display()

        Dim intErrNo As Integer
        Dim strParamName As String
        Dim strParamValue As String
        Dim strOpCode As String
        Dim objResult As DataSet

        strOpCode = "FA_CLSMTHEND_GET"
        strParamName = "LOCCODE|MODULECODE"
        strParamValue = strLocation & "|21"

        Try
            intErrNo = objFATrx.mtdGetDataCommon(strOpCode, _
                                               strParamName, _
                                               strParamValue, _
                                               objResult)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=FA_MTHEND_GENDEPR&errmesg=&redirect=")
        End Try

        If objResult.Tables(0).Rows.Count > 0 Then
            lblLastProcessDate.Text = objGlobal.GetLongDate(objResult.Tables(0).Rows(0).Item("LastProcessDate"))
            lblStatus.Text = objAdminShare.mtdGetMtdEndClose(CInt(objResult.Tables(0).Rows(0).Item("CloseInd")))
            lblAccPeriod.Text = objResult.Tables(0).Rows(0).Item("CurrAccMonth").Trim() & "/" & objResult.Tables(0).Rows(0).Item("CurrAccYear").Trim()

            Session("SS_FAACCMONTH") = objResult.Tables(0).Rows(0).Item("CurrAccMonth").Trim()
            Session("SS_FAACCYEAR") = objResult.Tables(0).Rows(0).Item("CurrAccYear").Trim()

            'If (CInt(objResult.Tables(0).Rows(0).Item("CurrAccMonth").Trim()) = CInt(strAccMonth)) And _
            '   (CInt(objResult.Tables(0).Rows(0).Item("CurrAccYear").Trim()) = CInt(strAccYear)) And _
            '   (CInt(objResult.Tables(0).Rows(0).Item("CloseInd")) = objAdminShare.EnumMthEndClose.No) Then
            '    btnProceed.Visible = True
            '    btnRollBack.Visible = True
            'Else
            '    btnProceed.Visible = False
            '    btnRollBack.Visible = False
            'End If

            btnProceed.Visible = True
            btnRollBack.Visible = True

        Else
            btnProceed.Visible = False
            btnRollBack.Visible = False
            lblStatus.Text = ""
            lblLastProcessDate.Text = ""
            lblAccPeriod.Text = ""
            lblErrDocument.Text = "No Akun Periode Defined, Please Contact System Administartor"
            lblErrDocument.Visible = True
        End If
    End Sub

    Sub btnProceed_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)

        Dim intErrNo As Integer
        Dim strParamName As String
        Dim strParamValue As String
        Dim strOpCode As String
        Dim objDataSet As DataSet
        Dim strPeriode As String

        strOpCode = "FA_CLSMTHEND_ASSET_MONTH_ADD"
        strParamName = "PERIODE|LOCCODE|ACCMONTH|ACCYEAR|USERID"


        If Len(strAccMonth) = 1 Then
            strPeriode = strAccYear & "0" & strAccMonth
        Else
            strPeriode = strAccYear & strAccMonth
        End If

        'response.write(strAccMonth & " - " & strAccYear)


        strParamValue = strPeriode & "|" & strLocation & "|" & strAccMonth & "|" & strAccYear & "|" & strUserId

        Try
            intErrNo = objFATrx.mtdGetDataCommon(strOpCode, _
                                               strParamName, _
                                               strParamValue, _
                                               objDataSet)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=FA_MTHEND_PROCESS&errmesg=&redirect=")
        End Try

			

        If objDataSet.Tables(0).Rows(0).Item("Msg").Trim() = "OK" Then
            lblErrDocument.Text = "Fixed Asset Monthend Succeed.."
            lblErrDocument.forecolor = System.Drawing.Color.Blue
            lblErrDocument.Visible = True

            Session("SS_FAACCMONTH") = objDataSet.Tables(0).Rows(0).Item("AccMonth").Trim()
            Session("SS_FAACCYEAR") = objDataSet.Tables(0).Rows(0).Item("AccYear").Trim()

            strAccMonth = objDataSet.Tables(0).Rows(0).Item("AccMonth").Trim()
            strAccYear = objDataSet.Tables(0).Rows(0).Item("AccYear").Trim()


            onLoad_Display()

            'response.write(strAccMonth & " - " & strAccYear)

        ElseIf objDataSet.Tables(0).Rows(0).Item("Msg") = "ACT" Then

            lblErrDocument.Text = "Can't Process Monthend, There Active Document On Current Periode.!"
            lblErrDocument.forecolor = System.Drawing.Color.Red
            lblErrDocument.Visible = True

        Else

            lblErrDocument.Text = objDataSet.Tables(0).Rows(0).Item("Msg")
            lblErrDocument.forecolor = System.Drawing.Color.Red
            lblErrDocument.Visible = True

        End If


    End Sub

End Class
