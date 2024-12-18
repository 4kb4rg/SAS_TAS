
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

Public Class CT_StdRpt_MthlyIssueForPayrollBillList : Inherits Page

    Protected RptSelect As UserControl

    Dim objCT As New agri.CT.clsReport()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label

    Protected WithEvents lstDisplay As DropDownList
    Protected WithEvents lblBillPartyCode As Label
    Protected WithEvents lblLocation As Label

    Protected WithEvents txtEmpCode As TextBox
    Protected WithEvents txtBillPartyCode As TextBox
    Protected WithEvents lstOrderBy As DropDownList

    Protected WithEvents trEmp As HtmlTableRow
    Protected WithEvents trBillParty As HtmlTableRow

    Protected WithEvents PrintPrev As ImageButton

    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String

    Dim dr As DataRow
    Dim intCnt As Integer
    Dim intErrNo As Integer
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType as String


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else

            If Not Page.IsPostBack Then
                onload_GetLangCap()
                BindDisplayList()
            End If

            If lstDisplay.SelectedItem.Value = "emp" Then
                trEmp.Visible = True
                trBillParty.Visible = False
            ElseIf lstDisplay.SelectedItem.Value = "bill" Then
                trEmp.Visible = False
                trBillParty.Visible = True
            End If
        End If
    End Sub


    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim UCAccMthYr As HtmlTableRow

        UCAccMthYr = RptSelect.FindControl("TrMthYr")
        UCAccMthYr.Visible = True

    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblBillPartyCode.Text = GetCaption(objLangCap.EnumLangCap.BillParty) & " Code :"
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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=VT_STDRPT_PAYBILL_CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/CT_StdRpt_Selection.aspx")
        End Try

    End Sub

	
	 Function GetCaption(ByVal pv_TermCode) As String
	        Dim count As Integer
	        
	        For count=0 To objLangCapDs.Tables(0).Rows.Count - 1
	            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode"))
	                If strLocType = objAdminLoc.EnumLocType.Mill then
	                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
	                else
	                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
	                end if
	                'Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
	                Exit For
	            End If
	        Next
	 End Function
	

    Sub BindDisplayList()
        lstDisplay.Items.Add(New ListItem("Employee Code", "emp"))
        lstDisplay.Items.Add(New ListItem(Left(lblBillPartyCode.Text, Len(lblBillPartyCode.Text) - 7), "bill"))

    End Sub

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strDisplay As String
        Dim strOrderBy As String
        Dim strOrderByText As String
        Dim strEmpCode As String
        Dim strBillPartyCode As String

        Dim strddlAccMth As String
        Dim strddlAccYr As String
        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String

        Dim tempAccMth As DropDownList
        Dim tempAccYr As DropDownList
        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden

        Dim strParam As String
        Dim strDateSetting As String

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

        strDisplay = Trim(lstDisplay.SelectedItem.Value)
        strOrderBy = Trim(lstOrderBy.SelectedItem.Value)
        strOrderByText = Trim(lstOrderBy.SelectedItem.Text)

        If txtEmpCode.Text = "" Then
            strEmpCode = ""
        Else
            strEmpCode = Trim(txtEmpCode.Text)
        End If

        If txtBillPartyCode.Text = "" Then
            strBillPartyCode = ""
        Else
            strBillPartyCode = Trim(txtBillPartyCode.Text)
        End If

        strEmpCode = Server.UrlEncode(strEmpCode)
        strBillPartyCode = Server.UrlEncode(strBillPartyCode)

        Response.Write("<Script Language=""JavaScript"">window.open(""CT_StdRpt_MthlyIssueForPayrollBillListPreview.aspx?Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & _
                       "&Decimal=" & strDec & "&DDLAccMth=" & strddlAccMth & "&DDLAccYr=" & strddlAccYr & "&lblBillPartyCode=" & lblBillPartyCode.Text & _
                       "&lblLocation=" & lblLocation.Text & "&Display=" & strDisplay & "&OrderBy=" & strOrderBy & "&OrderByText=" & strOrderByText & "&EmpCode=" & strEmpCode & _
                       "&BillPartyCode=" & strBillPartyCode & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

End Class

