Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web 
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information
Imports Microsoft.VisualBasic
Imports System.Xml
Imports System.Web.Services

Public Class PR_StdRpt_TAXBorangCP39 : Inherits Page

    Protected RptSelect As UserControl

    Protected objHRSetup As New agri.HR.clsSetup()
    Protected objHR As New agri.HR.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objSysCfg As New agri.PWSystem.clsConfig()

    Protected WithEvents lblDate As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label

    Protected WithEvents txtChequeNo As TextBox
    Protected WithEvents ddlTaxBranch As DropDownList

    Protected WithEvents PrintPrev As ImageButton

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfigsetting As Integer

    Dim dr As DataRow
    Dim intCnt As Integer
    Dim intErrNo As Integer
    Dim strSelectedTaxBranch As String
    Dim strEmployerTaxNo As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        
        lblDate.Visible = False
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
                BindTaxBranch("")
            End If   
        End If
    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim ucTrMthYr As HtmlTableRow

        ucTrMthYr = RptSelect.FindControl("TrMthYr")
        ucTrMthYr.Visible = True
    End Sub

    Sub BindTAXBranch(ByVal pv_strSelectedTaxBranch As Object)
        Dim objTAXBranchDs As New Object()
        Dim strOpCd_TAX As String = "HR_CLSSETUP_TAXBRANCH_SEARCH"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer
        
        strParam = "||" & objHRSetup.EnumTAXBranchStatus.Active & "||TB.TAXBranchCode|" 

        Try
            intErrNo = objHRSetup.mtdGetTaxBranch(strOpCd_TAX, strParam, objTAXBranchDs, False)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_GET_TAXBRANCH&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
    
        strEmployerTaxNo = ""

        If objTAXBranchDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objTAXBranchDs.Tables(0).Rows.Count - 1
                objTAXBranchDs.Tables(0).Rows(intCnt).Item("TAXBranchCode") = Trim(objTAXBranchDs.Tables(0).Rows(intCnt).Item("TAXBranchCode"))
                objTAXBranchDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objTAXBranchDs.Tables(0).Rows(intCnt).Item("TAXBranchCode")) & " (" & _
                                                                            Trim(objTAXBranchDs.Tables(0).Rows(intCnt).Item("Description")) & ")"

                If objTAXBranchDs.Tables(0).Rows(intCnt).Item("TAXBranchCode") = pv_strSelectedTaxBranch Then
                    intSelectedIndex = intCnt 
                    strEmployerTaxNo = Trim(objTAXBranchDs.Tables(0).Rows(intCnt).Item("EmployerTaxNo"))
                End If
            Next
        End If
        dr = objTAXBranchDs.Tables(0).NewRow()
        dr("TAXBranchCode") = ""
        dr("Description") = "Please select a Tax Branch"
        objTAXBranchDs.Tables(0).Rows.InsertAt(dr, 0)
        ddlTAXBranch.DataSource = objTAXBranchDs.Tables(0)
        ddlTAXBranch.DataTextField = "Description"
        ddlTAXBranch.DataValueField = "TAXBranchCode"
        ddlTAXBranch.DataBind()
        ddlTAXBranch.SelectedIndex = intSelectedIndex
    End Sub


    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strDdlAccMth As String
        Dim strDdlAccYr As String
        Dim strRptName As String
        Dim strDec As String
        Dim strUserLoc As String
        Dim strChequeNo As String
        Dim strTaxBranch As String

        Dim tempAccMth As DropDownList
        Dim tempAccYr As DropDownList
        Dim tempRptName As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden

        tempAccMth = RptSelect.FindControl("lstAccMonth")
        strddlAccMth = Trim(tempAccMth.SelectedItem.Value)
        tempAccYr = RptSelect.FindControl("lstAccYear")
        strddlAccYr = Trim(tempAccYr.SelectedItem.Value)
        tempRptName = RptSelect.FindControl("lstRptName")
        strRptName = Trim(tempRptName.SelectedItem.Value)
        tempDec = RptSelect.FindControl("lstDecimal")
        strDec = Trim(tempDec.SelectedItem.Value)
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

        If txtChequeNo.Text = "" Then
            strChequeNo = ""
        Else
            strChequeNo = Trim(txtChequeNo.Text)
        End If

        strTaxBranch = Trim(ddlTaxBranch.SelectedItem.Value)

        Response.Write("<Script Language=""JavaScript"">window.open(""PR_StdRpt_TAXBorangCP39Preview.aspx?Type=Print&CompName=" & strCompany & _
                       "&Location=" & strUserLoc & "&ddlAccMth=" & strDdlAccMth & "&ddlAccYr=" & strDdlAccYr & _
                       "&Decimal=" & strDec & "&ChequeNo=" & strChequeNo & _
                       "&TaxBranch=" & strTaxBranch & "&EmployerTaxNo=" & strEmployerTaxNo & _
                       """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

    End Sub

End Class
