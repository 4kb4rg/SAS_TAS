
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

Public Class PR_StdRpt_EPFBorangA : Inherits Page

    Protected RptSelect As UserControl

    Protected objHRSetup As New agri.HR.clsSetup()
    Protected objHR As New agri.HR.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objSysCfg As New agri.PWSystem.clsConfig()

    Protected WithEvents lblDate As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label

    Protected WithEvents txtChequeNo As TextBox
    Protected WithEvents lstEmpCode As DropDownList
    Protected WithEvents txtSignName As TextBox
    Protected WithEvents txtSignNRIC As TextBox
    Protected WithEvents txtSignDesignation As TextBox

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
    Dim strSelectedEmpCode As String

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
            txtSignName.Enabled = True
            txtSignNRIC.Enabled = True
            txtSignDesignation.Enabled = True

            If Not Page.IsPostBack Then
                BindEmpCodeList("")
            End If   
        End If
    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim ucTrMthYr As HtmlTableRow

        ucTrMthYr = RptSelect.FindControl("TrMthYr")
        ucTrMthYr.Visible = True
    End Sub

    Sub BindEmpCodeList(ByVal pv_strSelectedEmpCode As Object)
        Dim objRptDs As New Object()
        Dim strOpCd_Get As String = "PR_STDRPT_EMPLOYEE_GET"
        Dim strParam As String = ""
        Dim intSelectedIndex As Integer

        strParam = "|" & objHR.EnumEmpStatus.Active

        Try
            intErrNo = objHR.mtdGetOthEmployee(strOpCd_Get, strParam, strLocation, objRptDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_STDRPT_EMPLOYEE_GET&errmesg=" & lblErrMessage.Text & "&redirect=reports/PR_StdRpt_Selection.aspx")
        End Try

        txtSignName.Text = ""
        txtSignNRIC.Text = ""
        txtSignDesignation.Text = ""

        For intCnt = 0 To objRptDs.Tables(0).Rows.Count - 1
            objRptDs.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(objRptDs.Tables(0).Rows(intCnt).Item("EmpCode"))
           
            If objRptDs.Tables(0).Rows(intCnt).Item("EmpCode") = pv_strSelectedEmpCode Then
                intSelectedIndex = intCnt + 1
                txtSignName.Text = Trim(objRptDs.Tables(0).Rows(intCnt).Item("EmpName"))
                txtSignNRIC.Text = IIf(Trim(objRptDs.Tables(0).Rows(intCnt).Item("NewICNo")) <> "", Trim(objRptDs.Tables(0).Rows(intCnt).Item("NewICNo")), Trim(objRptDs.Tables(0).Rows(intCnt).Item("OldICNo")))
                txtSignDesignation.Text = Trim(objRptDs.Tables(0).Rows(intCnt).Item("PosDesc"))
            End If
            objRptDs.Tables(0).Rows(intCnt).Item("EmpName") = Trim(objRptDs.Tables(0).Rows(intCnt).Item("EmpCode")) & " (" & _
                                                              Trim(objRptDs.Tables(0).Rows(intCnt).Item("EmpName")) & ")"
        Next

        dr = objRptDs.Tables(0).NewRow()
        dr("EmpCode") = ""
        dr("EmpName") = "All employee code"

        objRptDs.Tables(0).Rows.InsertAt(dr, 0)

        lstEmpCode.DataSource = objRptDs.Tables(0)
        lstEmpCode.DataTextField = "EmpName"
        lstEmpCode.DataValueField = "EmpCode"
        lstEmpCode.DataBind()
        lstEmpCode.SelectedIndex = intSelectedIndex
    End Sub

    Sub EmpIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        strSelectedEmpCode = lstEmpCode.SelectedItem.Value
        BindEmpCodeList(strSelectedEmpCode)
        If lstEmpCode.SelectedItem.Value <> "" Then
            txtSignName.Enabled = False
            txtSignNRIC.Enabled = False
            txtSignDesignation.Enabled = False
        Else
            txtSignName.Enabled = True
            txtSignNRIC.Enabled = True
            txtSignDesignation.Enabled = True
        End If
    End Sub

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strDdlAccMth As String
        Dim strDdlAccYr As String
        Dim strRptName As String
        Dim strDec As String
        Dim strUserLoc As String
        Dim strChequeNo As String
        Dim strExpiryDate As String
        Dim strEmpCode As String
        Dim strSignName As String
        Dim strSignNRIC As String
        Dim strSignDesignation As String

        Dim tempAccMth As DropDownList
        Dim tempAccYr As DropDownList
        Dim tempRptName As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden

        Dim strParam As String
        Dim strDateSetting As String
        Dim objSysCfgDs as New Object()
        Dim objDateFormat as New Object()
        Dim objExpiryDate as String

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

        strEmpCode = Trim(lstEmpCode.SelectedItem.Value)

        If txtSignName.Text = "" Then
            strSignName = ""
        Else
            strSignName = Trim(txtSignName.Text)
        End If

        If txtSignNRIC.Text = "" Then
            strSignNRIC = ""
        Else
            strSignNRIC = Trim(txtSignNRIC.Text)
        End If

        If txtSignDesignation.Text = "" Then
            strSignDesignation = ""
        Else
            strSignDesignation = Trim(txtSignDesignation.Text)
        End If

        Response.Write("<Script Language=""JavaScript"">window.open(""PR_StdRpt_EPFBorangAPreview.aspx?Type=Print&CompName=" & strCompany & _
                       "&Location=" & strUserLoc & "&ddlAccMth=" & strDdlAccMth & "&ddlAccYr=" & strDdlAccYr & _
                       "&Decimal=" & strDec & _
                       "&ChequeNo=" & strChequeNo & "&EmpCode=" & strEmpCode & _
                       "&SignName=" & strSignName & "&SignNRIC=" & strSignNRIC & "&SignDesignation=" & strSignDesignation & _
                       """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

    End Sub

End Class
