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

Public Class GL_StdRpt_TrialBalSummaryList : Inherits Page

    Protected RptSelect As UserControl

    Dim objGL As New agri.GL.clsReport()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Protected WithEvents hidAccMonthPX As HtmlInputHidden
    Protected WithEvents hidAccYearPX As HtmlInputHidden
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents txtSrchAccCode As TextBox
    Protected WithEvents rbSuppYes As RadioButton
    Protected WithEvents rbSuppNo As RadioButton
    Protected WithEvents cbBalSheet As CheckBox
    Protected WithEvents cbProfLoss As CheckBox
    Protected WithEvents cbEstExpense As CheckBox
    Protected WithEvents PrintPrev As ImageButton
    Protected WithEvents lblChartofAccCode As Label
    Protected WithEvents lblChartofAccType As Label
    Protected WithEvents lblCode As Label

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
    Dim tempActGrp As String

    Dim objLangCapDs As New Object()
	
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String
	

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
                onload_GetLangCap()
                cbBalSheet.Checked = True
                cbProfLoss.Checked = True
                cbEstExpense.Checked = True
            End If
        End If
    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim htmltr As HtmlTableRow

        htmltr = RptSelect.FindControl("TrMthYr")
        htmltr.visible = True
        htmltr = RptSelect.FindControl("TrCheckLoc")
        htmltr.visible = False
        htmltr = RptSelect.FindControl("TrRadioLoc")
        htmltr.visible = True

        If Page.IsPostBack Then
        end if
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblChartofAccCode.Text = GetCaption(objLangCap.EnumLangCap.Account) & lblCode.text
        lblChartofAccType.Text = GetCaption(objLangCap.EnumLangCap.Account)
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
        Catch Exp As Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PWSYSTEM_CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/IN_StdRpt_Selection.aspx")
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


    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)

        Dim strddlAccMth As String
        Dim strddlAccYr As String
        Dim strRptId As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String
        Dim strSrchAccCode As String
        Dim strSupp As String
        Dim strAccType As String
        Dim strAccTypeText As String
        Dim strEstExpense As String
        Dim strDateSetting As String
        Dim objSysCfgDs As New Object()
        Dim intCntActGrp As Integer

        Dim ddlist As DropDownList
        Dim rdblist As RadioButtonList
        Dim intCnt As Integer

        ddlist = RptSelect.FindControl("lstAccMonth")
        strddlAccMth = Trim(ddlist.SelectedItem.Value)

        ddlist = RptSelect.FindControl("lstAccYear")
        strddlAccYr = Trim(ddlist.SelectedItem.Value)

        ddlist = RptSelect.FindControl("lstRptName")
        strRptId = Trim(ddlist.SelectedItem.value)

        ddlist = RptSelect.FindControl("lstRptName")
        strRptName = Trim(ddlist.SelectedItem.text)

        ddlist = RptSelect.FindControl("lstDecimal")
        strDec = Trim(ddlist.SelectedItem.Value)

        rdblist = RptSelect.FindControl("rblLocation")
        strUserLoc = rdblist.SelectedItem.Value

        If rbSuppYes.Checked Then
            strSupp = rbSuppYes.Text
        ElseIf rbSuppNo.Checked Then
            strSupp = rbSuppNo.Text
        End If

        strSrchAccCode = Trim(txtSrchAccCode.Text)

        If cbBalSheet.Checked = True Then
            strAccType = strAccType & "','" & objGLSetup.EnumAccountType.BalanceSheet
            strAccTypeText = strAccTypeText & ", " & objGLSetup.mtdGetAccountType(CInt(objGLSetup.EnumAccountType.BalanceSheet))
        End If
        If cbProfLoss.Checked = True Then
            strAccType = strAccType & "','" & objGLSetup.EnumAccountType.ProfitAndLost
            strAccTypeText = strAccTypeText & ", " & objGLSetup.mtdGetAccountType(CInt(objGLSetup.EnumAccountType.ProfitAndLost))
        End If
        
        If cbEstExpense.Checked = True
            strEstExpense = "Yes"
        Else
            strEstExpense = "No"
        End If
        If inStr(strAccType, "','") = 0 Then
        Else
            strAccType = Right(strAccType, Len(strAccType) - 3)
            strAccTypeText = Right(strAccTypeText, Len(strAccTypeText) - 2)
        End If

        Response.Write("<Script Language=""JavaScript"">window.open(""GL_StdRpt_TrialBalSummaryListPreview.aspx?Type=Print&CompName=" & strCompany & _
                       "&Location=" & strUserLoc & _
                       "&DDLAccMth=" & strddlAccMth & _
                       "&DDLAccYr=" & strddlAccYr & _
                       "&RptId=" & strRptId & _
                       "&RptName=" & strRptName & _
                       "&Decimal=" & strDec & _
                       "&Supp=" & strSupp & _
                       "&sum=yes" & _
                       "&SrchAccCode=" & strSrchAccCode & _
                       "&AccType=" & strAccType & _
                       "&AccTypeText=" & strAccTypeText & _
                       "&EstExpense=" & strEstExpense & _
                       """,""" & strRptID & """ ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub
End Class
