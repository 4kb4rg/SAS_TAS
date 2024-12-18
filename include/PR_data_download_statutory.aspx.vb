Imports System
Imports System.Data
Imports System.IO 
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information
Imports Microsoft.VisualBasic

Imports agri.GlobalHdl.clsAccessRights
Imports agri.HR


Public Class PR_data_download_statutory : Inherits Page
    Protected WithEvents tblDownload As HtmlTable
    Protected WithEvents tblSave As HtmlTable
    Protected WithEvents rdStatutory As RadioButtonList
    Protected WithEvents TrPhyMthYr As HtmlTableRow
    Protected WithEvents lstAccMonth As DropDownList
    Protected WithEvents lstAccYear As DropDownList
    Protected WithEvents lblTaxBranch As Label
    Protected WithEvents ddlTaxBranch As DropDownList
    Protected WithEvents lblErrGenerate As Label
    Protected WithEvents lblDownloadfile As Label
    Protected WithEvents lblErrMesage As Label
    Protected WithEvents btnGenerate As ImageButton
    Protected WithEvents lnkSaveTheFile As Hyperlink

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objTAXBranchDs As New Object()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intPRAR As Long

    Dim strSelectedDocument As String

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        intPRAR = Session("SS_PRAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDownload), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            strSelectedDocument = rdStatutory.SelectedItem.Value 

            If Not Page.IsPostBack Then
                BindAccMonthList()
                BindAccYearList()
                If strSelectedDocument = 2 Then
                    lblTaxBranch.Visible = True
                    ddlTaxBranch.Visible = True
                Else
                    lblTaxBranch.Visible = False
                    ddlTaxBranch.Visible = False
                End If
                BindTaxBranch()
                tblDownload.Visible = True
                tblSave.Visible = False
            Else
                tblDownload.Visible = True
                tblSave.Visible = False
            End If
        End If
    End Sub

    Sub BindAccMonthList()
        Dim intCntddlMth As Integer

        If strAccMonth = 1 Then
            lstAccMonth.SelectedIndex = 11
        Else
            lstAccMonth.SelectedIndex = strAccMonth - 2
        End If

    End Sub

    Sub BindAccYearList()
        Dim CurrDate As Date
        Dim CurrYear As Integer
        Dim intCntAddYr As Integer = 1
        Dim intCntMinYr As Integer = 5
        Dim NewAddCurrYear As Integer
        Dim NewMinCurrYear As Integer
        Dim intCntddlYr As Integer = 0

        CurrDate = Today
        CurrYear = Year(CurrDate)

        While intCntMinYr <> 0
            intCntMinYr = intCntMinYr - 1
            NewMinCurrYear = CurrYear - intCntMinYr
            lstAccYear.Items.Add(NewMinCurrYear)
        End While

        For intCntAddYr = 1 To 5
            NewAddCurrYear = CurrYear + intCntAddYr
            lstAccYear.Items.Add(NewAddCurrYear)
        Next

        For intCntddlYr = 0 To lstAccYear.Items.Count - 1
            If strAccMonth = 1 Then
                If lstAccYear.Items(intCntddlYr).Text = strAccYear - 1 Then
                    lstAccYear.SelectedIndex = intCntddlYr
                End If
            Else
                If lstAccYear.Items(intCntddlYr).Text = strAccYear Then
                    lstAccYear.SelectedIndex = intCntddlYr
                End If
            End If
        Next
    End Sub

    Sub RadioButtonChanged(ByVal Sender As Object, ByVal E As EventArgs)
        strSelectedDocument = rdStatutory.SelectedItem.Value 
        If strSelectedDocument = 2 Then
            lblTaxBranch.Visible = True
            ddlTaxBranch.Visible = True
            BindTaxBranch()
        Else
            lblTaxBranch.Visible = False
            ddlTaxBranch.Visible = False
        End If
    End Sub

    Sub BindTAXBranch()
        Dim strOpCd_TAX As String = "HR_CLSSETUP_TAXBRANCH_SEARCH"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
        
        strParam = "||" & objHRSetup.EnumTAXBranchStatus.Active & "||TB.TAXBranchCode|" 

        Try
            intErrNo = objHRSetup.mtdGetTaxBranch(strOpCd_TAX, strParam, objTAXBranchDs, False)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_GET_TAXBRANCH&errmesg=" & lblErrMesage.Text & "&redirect=")
        End Try

        If objTAXBranchDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objTAXBranchDs.Tables(0).Rows.Count - 1
                objTAXBranchDs.Tables(0).Rows(intCnt).Item("TAXBranchCode") = Trim(objTAXBranchDs.Tables(0).Rows(intCnt).Item("TAXBranchCode"))
                objTAXBranchDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objTAXBranchDs.Tables(0).Rows(intCnt).Item("TAXBranchCode")) & " (" & _
                                                                            Trim(objTAXBranchDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            Next
        End If

        dr = objTAXBranchDs.Tables(0).NewRow()
        dr("TAXBranchCode") = ""
        dr("Description") = "Please Select TAX Branch"
        objTAXBranchDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlTAXBranch.DataSource = objTAXBranchDs.Tables(0)
        ddlTAXBranch.DataTextField = "Description"
        ddlTAXBranch.DataValueField = "TAXBranchCode"
        ddlTAXBranch.DataBind()
    End Sub

    Sub btnGenerate_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strParam As String = ""
        Dim strAccMthPeriod As String
        Dim strAccYrPeriod As String
        Dim strTaxBranch As String

        strParam = strSelectedDocument
        strAccMthPeriod = lstAccMonth.SelectedItem.Value
        strAccYrPeriod = lstAccYear.SelectedItem.Value
        strTaxBranch = ddlTaxBranch.SelectedItem.Value

        If strParam = "" Then
            lblErrGenerate.Visible = True
        Else
            Response.Redirect("PR_data_download_statutory_savefile.aspx?" & _
                              "&Statutory=" & strParam & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & _
                              "&TaxBranch=" & strTaxBranch)
        End If
    End Sub

End Class
