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

Public Class GL_StdRpt_TrialBalance : Inherits Page

    Protected RptSelect As UserControl

    Dim objGL As New agri.GL.clsReport()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()


    Protected WithEvents hidAccMonthPX As HtmlInputHidden
    Protected WithEvents hidAccYearPX As HtmlInputHidden
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label

    Protected WithEvents rbSuppYes As RadioButton
    Protected WithEvents rbSuppNo As RadioButton
    Protected WithEvents cblAccType As CheckBoxList
    Protected WithEvents PrintPrev As ImageButton
    Protected WithEvents lblChartofAccCode As Label
    Protected WithEvents lblChartofAccCode2 As Label

    Protected WithEvents lblChartofAccType As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblType As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents lblAccDesc As Label
    'Protected WithEvents txtSrchAccCodeTo As TextBox
    'Protected WithEvents txtSrchAccCode As TextBox
    Protected WithEvents lstAccCode As DropDownList
    Protected WithEvents lstAccCode2 As DropDownList
    Protected WithEvents Find As HtmlInputButton
    Protected WithEvents Find2 As HtmlInputButton

    Protected WithEvents cbExcel As CheckBox


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
            If Not Page.IsPostBack
                onload_GetLangCap()
                cblAccType.Items(0).Selected = True
                cblAccType.Items(1).Selected = True
                BindAccCodeDropList("")

            End If
        End If
    End Sub

    Sub BindAccCodeDropList(Optional ByVal pv_strAccCode As String = "")

        Dim strOpCd As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        Dim dr As DataRow
        Dim strParam As String = "Order By ACC.AccCode|And ACC.Status = '" & objGLSetup.EnumAccountCodeStatus.Active & "'"
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim dsForDropDown As DataSet

        strParam = strParam & IIf(Session("SS_COACENTRALIZED") = "1", "", " AND LocCode = '" & Trim(strLocation) & "' ")

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.AccountCode, _
                                                   dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BindAccCodeDropList&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try


        dr = dsForDropDown.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("_Description") = "Select COA"
        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)

        lstAccCode.DataSource = dsForDropDown.Tables(0)
        lstAccCode.DataValueField = "AccCode"
        lstAccCode.DataTextField = "_Description"
        lstAccCode.DataBind()
        lstAccCode.SelectedIndex = intSelectedIndex

        lstAccCode2.DataSource = dsForDropDown.Tables(0)
        lstAccCode2.DataValueField = "AccCode"
        lstAccCode2.DataTextField = "_Description"
        lstAccCode2.DataBind()
        lstAccCode2.SelectedIndex = intSelectedIndex

        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If
    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim htmltr As HtmlTableRow

        htmltr = RptSelect.FindControl("TrMthYr")
        htmltr.visible = True
        htmltr = RptSelect.FindControl("TrCheckLoc")
        htmltr.visible = True
        htmltr = RptSelect.FindControl("TrRadioLoc")
        htmltr.visible = False

        If Page.IsPostBack Then
        end if
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblChartofAccCode.Text = GetCaption(objLangCap.EnumLangCap.Account) & lblCode.text

        lblChartofAccCode2.Text = "To " & GetCaption(objLangCap.EnumLangCap.Account) & lblCode.text

        lblChartofAccType.Text = GetCaption(objLangCap.EnumLangCap.Account) & lblType.text
        lblLocation.Text = GetCaption(objLangCap.EnumLangCap.Location)
        lblAccDesc.Text = GetCaption(objLangCap.EnumLangCap.AccDesc)
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

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)

        Dim strddlAccMth As String
        Dim strddlAccYr As String
        Dim strRptId As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String
        Dim strSrchAccCode As String
        Dim strSrchAccCodeTo As String
        Dim strSupp As String
        Dim strAccType As String
        Dim strAccTypeText As String
        Dim strParam As String

        Dim ddlist As DropDownList
        Dim rdblist As RadioButtonList
        Dim intCnt As Integer

        Dim tempUserLoc As HtmlInputHidden
        Dim templblUL As Label
        Dim strTemp As String

        Dim LocTag As String
        Dim AccCodeTag As String
        Dim AccDescTag As String
        Dim AccTypeTag As String
        Dim strExportToExcel As String

        ddlist = RptSelect.FindControl("lstAccMonth")
        strddlAccMth = Trim(ddlist.SelectedItem.Value)

        ddlist = RptSelect.FindControl("lstAccYear")
        strddlAccYr = Trim(ddlist.SelectedItem.Value)

        ddlist = RptSelect.FindControl("lstRptName")
        strRptId = Trim(ddlist.SelectedItem.Value)

        ddlist = RptSelect.FindControl("lstRptName")
        strRptName = Trim(ddlist.SelectedItem.Text)

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

        If Right(strUserLoc, 1) = "," Then
            strUserLoc = Left(strUserLoc, Len(strUserLoc) - 1)
        Else
            strUserLoc = Trim(strUserLoc)
        End If


        If rbSuppYes.Checked Then
            strSupp = rbSuppYes.Text
        ElseIf rbSuppNo.Checked Then
            strSupp = rbSuppNo.Text
        End If


        strSrchAccCode = lstAccCode.SelectedItem.Value.Trim()
        strSrchAccCodeTo = lstAccCode2.SelectedItem.Value.Trim()

        'strSrchAccCode = Trim(txtSrchAccCode.Text)
        'strSrchAccCodeTo = Trim(txtSrchAccCodeTo.Text)

        For intCnt = 0 To cblAccType.Items.Count - 1
            If cblAccType.Items(intCnt).Selected Then
                If cblAccType.Items.Count = 1 Then
                    strAccType = cblAccType.Items(intCnt).Value
                    strAccTypeText = cblAccType.Items(intCnt).Text
                Else
                    strAccType = strAccType & "','" & cblAccType.Items(intCnt).Value
                    strAccTypeText = strAccTypeText & ", " & cblAccType.Items(intCnt).Text
                End If
            End If
        Next
        If intCnt <> 1 Then
            strAccType = Right(strAccType, Len(strAccType) - 3)
            strAccTypeText = Right(strAccTypeText, Len(strAccTypeText) - 2)
        End If

        LocTag = lblLocation.Text
        AccCodeTag = lblChartofAccCode.Text
        AccDescTag = lblAccDesc.Text
        AccTypeTag = lblChartofAccType.Text

        strExportToExcel = IIf(cbExcel.Checked = True, "1", "0")


        Response.Write("<Script Language=""JavaScript"">window.open(""GL_StdRpt_TrialBalancePreview.aspx?Type=Print&CompName=" & strCompany & _
                       "&Location=" & strUserLoc & _
                       "&DDLAccMth=" & strddlAccMth & _
                       "&DDLAccYr=" & strddlAccYr & _
                       "&RptId=" & strRptId & _
                       "&RptName=" & strRptName & _
                       "&Decimal=" & strDec & _
                       "&Supp=" & strSupp & "&sum=no" & _
                       "&SrchAccCode=" & strSrchAccCode & _
                       "&SrchAccCodeTo=" & strSrchAccCodeTo & _
                       "&AccType=" & strAccType & _
                       "&AccTypeText=" & strAccTypeText & _
                       "&LocTag=" & LocTag & _
                       "&AccCodeTag=" & AccCodeTag & _
                       "&AccDescTag=" & AccDescTag & _
                       "&AccTypeTag=" & AccTypeTag & _
                       "&ExportToExcel=" & strExportToExcel & _
                       """,""" & strRptId & """ ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub
End Class
