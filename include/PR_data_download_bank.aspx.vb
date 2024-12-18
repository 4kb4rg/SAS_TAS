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
Imports Microsoft.VisualBasic.FileSystem

Imports agri.GlobalHdl.clsAccessRights
Imports agri.HR.clsSetup
Imports agri.PR.clsReport
Imports agri.PWSystem.clsConfig
Imports agri.GlobalHdl.clsGlobalHdl


Public Class PR_data_download_bank : Inherits Page

    Protected WithEvents tblDownload As HtmlTable
    Protected WithEvents tblSave As HtmlTable
    Protected WithEvents TrPhyMthYr As HtmlTableRow
    Protected WithEvents lstAccMonth As DropDownList
    Protected WithEvents lstAccYear As DropDownList
    Protected WithEvents ddlBankCode As DropDownList
    Protected WithEvents txtProgramPath As TextBox
    Protected WithEvents txtCreditDate As TextBox
    Protected WithEvents btnSelDate As Image
    Protected WithEvents lblErrCreditDate As Label
    Protected WithEvents lblDownloadfile As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents btnGenerate As ImageButton
    Protected WithEvents lnkSaveTheFile As Hyperlink

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objPRRpt As New agri.PR.clsReport()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intPRAR As Long

    Dim strSelectedBankCode As String

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
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDownloadBankAuto), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If Not Page.IsPostBack Then
                BindAccMonthList()
                BindAccYearList()
                BindBankCode("")

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

    Sub BindBankCode(ByVal pv_strSelectedBankCode As Object)
        Dim dsForBankCodeDropDown As New DataSet()
        Dim strOpCd As String = "PR_CLSDATA_BANK_GET_AUTOCRFMT"
        Dim strParam As String 
        Dim intCnt As Integer
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer

        strParam = objHRSetup.EnumBankStatus.Active & "|" & _
                   objHRSetup.EnumBankFormatStatus.Active & "|" & _
                   objHRSetup.EnumBankFormatType.Autocredit & "|"
        Try
            intErrNo = objPRRpt.mtdGetBank(strOpCd, strParam, dsForBankCodeDropDown)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_PR_BANKCODE&errmesg=" & Exp.ToString() & "&redirect=../../en/reports/PR_StdRpt_Selection.aspx")
        End Try
        
        If dsForBankCodeDropDown.Tables(0).Rows.Count > 0 Then
            txtProgramPath.Text = dsForBankCodeDropDown.Tables(0).Rows(0).Item("ProgramPath").Trim()
            For intCnt = 0 To dsForBankCodeDropDown.Tables(0).Rows.Count - 1
                dsForBankCodeDropDown.Tables(0).Rows(intCnt).Item("BankCode") = dsForBankCodeDropDown.Tables(0).Rows(intCnt).Item("BankCode").Trim()
                dsForBankCodeDropDown.Tables(0).Rows(intCnt).Item("Description") = dsForBankCodeDropDown.Tables(0).Rows(intCnt).Item("BankCode").Trim() & " (" & dsForBankCodeDropDown.Tables(0).Rows(intCnt).Item("Description").Trim() & ")"

                If dsForBankCodeDropDown.Tables(0).Rows(intCnt).Item("BankCode") = pv_strSelectedBankCode Then
                    txtProgramPath.Text = dsForBankCodeDropDown.Tables(0).Rows(intCnt).Item("ProgramPath").Trim()
                    intSelectedIndex = intCnt
                End If
            Next intCnt
            
            ddlBankCode.DataSource = dsForBankCodeDropDown.Tables(0)
            ddlBankCode.DataValueField = "BankCode"
            ddlBankCode.DataTextField = "Description"
            ddlBankCode.DataBind()
            ddlBankCode.SelectedIndex = intSelectedIndex
        End If
    End Sub

    Sub IndexBankChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        strSelectedBankCode = ddlBankCode.SelectedItem.Value
        BindBankCode(strSelectedBankCode)
    End Sub

    Sub btnGenerate_Click(Sender As Object, E As ImageClickEventArgs)
        Dim arrParam As Array
        Dim strAccMthPeriod As String
        Dim strAccYrPeriod As String
        Dim strBankCode As String
        Dim strCreditDate As String
        Dim strAccPeriod As String

        Dim strParam As String
        Dim strDateSetting As String
        Dim objSysCfgDs as New Object()
        Dim objDateFormat as New Object()
        Dim objExpiryDate as String
        Dim intErrNo As Integer

        strAccMthPeriod = lstAccMonth.SelectedItem.Value
        strAccYrPeriod = lstAccYear.SelectedItem.Value
        strBankCode = ddlBankCode.SelectedItem.Value

        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"
        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PR_DOWNLOADBANK_GET_CONFIG_DATE&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        strDateSetting = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))

        If Not (txtCreditDate.Text = "") Then
            If objGlobal.mtdValidInputDate(strDateSetting, txtCreditDate.Text, objDateFormat, objExpiryDate) = True Then

                arrParam = Split(txtCreditDate.Text, "/")

                objPRRpt.GetCharPeriod(CInt(arrParam(1)), CInt(arrParam(2)), strAccPeriod)
                strCreditDate = Trim(arrParam(0)) & " " & strAccPeriod
                
                Response.Redirect("" & txtProgramPath.Text & "?" & _
                                  "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & _
                                  "&BankCode=" & strBankCode & "&CreditDate=" & strCreditDate)
            Else
                lblErrCreditDate.Text = lblErrCreditDate.Text & objDateFormat & "."
                lblErrCreditDate.Visible = True
            End If
        End If
    End Sub


End Class
