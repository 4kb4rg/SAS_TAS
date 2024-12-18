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

Public Class PR_StdRpt_StmtTaxDeduct : Inherits Page

    Protected RptSelect As UserControl

    Protected objHRSetup As New agri.HR.clsSetup()
    Protected objHR As New agri.HR.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdmAcc As New agri.Admin.clsAccPeriod()

    Protected WithEvents lblDate As Label
    Protected WithEvents lblTracker As Label

    Protected WithEvents lstEAYear As DropDownList
    Protected WithEvents lstTaxBranch As DropDownList
    Protected WithEvents txtPeriodEnd As TextBox
    Protected WithEvents btnSelPeriodEnd As Image
    Protected WithEvents txtRefNo As TextBox
    Protected WithEvents txtNatureBuss As TextBox
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

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        Dim intMaxPeriod As Integer
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
                intMaxPeriod = BindAccYearList(strLocation, strAccYear)  
                BindTaxBranch()
            End If   
        End If
    End Sub

    Function BindAccYearList(ByVal pv_strLocation As String, _
                             ByVal pv_strAccYear As String) As Integer
        Dim strOpCd_Max_Get As String = "ADMIN_CLSACCPERIOD_CONFIG_ALLLOC_MAXPERIOD_GET"
        Dim strOpCd_Dist_Get As String = "ADMIN_CLSACCPERIOD_CONFIG_ACCYEAR_DISTINCT_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intAccYear As Integer
        Dim intMaxPeriod As Integer = 0
        Dim intCnt As Integer
        Dim intSelIndex As Integer
        Dim objAccCfg As New Dataset()

        If pv_strLocation = "" Then
            pv_strLocation = strLocation
        Else
            If Left(pv_strLocation, 3) = "','" Then
                pv_strLocation = Right(pv_strLocation, Len(pv_strLocation) - 3)
            ElseIf Right(pv_strLocation, 3) = "','" Then
                pv_strLocation = Left(pv_strLocation, Len(pv_strLocation) - 3)
            ElseIf Left(pv_strLocation, 1) = "," Then
                pv_strLocation = Right(pv_strLocation, Len(pv_strLocation) - 1)
            ElseIf Right(pv_strLocation, 1) = "," Then
                pv_strLocation = Left(pv_strLocation, Len(pv_strLocation) - 1)
            End If
        End If

        Try
            strParam = "||"
            intErrNo = objAdmAcc.mtdGetAccPeriodCfg(strOpCd_Dist_Get, _
                                                    strCompany, _
                                                    pv_strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    objAccCfg)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_STMT_DEDUCT_ACCCFG_DIST_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        intSelIndex = 0 
        lstEAYear.Items.Clear

        If objAccCfg.Tables(0).Rows.Count > 0 Then      
            For intCnt = 0 To objAccCfg.Tables(0).Rows.Count - 1    
                lstEAYear.Items.Add(objAccCfg.Tables(0).Rows(intCnt).Item("AccYear"))

                If objAccCfg.Tables(0).Rows(intCnt).Item("AccYear") = pv_strAccYear Then
                    intSelIndex = intCnt    
                End If
            Next

            lstEAYear.SelectedIndex = intSelIndex
        Else
            lstEAYear.Items.Add(strAccYear)    
            lstEAYear.SelectedIndex = intSelIndex
        End If

        objAccCfg = Nothing
        Return intMaxPeriod
    End Function

    Sub BindTaxBranch()
        Dim dsForTaxBranchDropDown As New DataSet()
        Dim strOpCd As String = "HR_CLSSETUP_TAXBRANCH_SEARCH"
        Dim strParam As String 

        strParam = "||" & objHRSetup.EnumTAXBranchStatus.Active & "||TB.TAXBranchCode|" 

        Try
            intErrNo = objHRSetup.mtdGetTaxBranch(strOpCd, strParam, dsForTaxBranchDropDown, False)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_PR_TAXBRANCH&errmesg=" & Exp.ToString() & "&redirect=../../en/reports/PR_StdRpt_Selection.aspx")
        End Try

        dr = dsForTaxBranchDropDown.Tables(0).NewRow()
        dr("TAXBranchCode") = ""
        dr("Description") = "All"
        dsForTaxBranchDropDown.Tables(0).Rows.InsertAt(dr, 0)

        lstTaxBranch.DataSource = dsForTaxBranchDropDown.Tables(0)
        lstTaxBranch.DataValueField = "TAXBranchCode"
        lstTaxBranch.DataTextField = "Description"
        lstTaxBranch.DataBind()

        dsForTaxBranchDropDown = Nothing
    End Sub

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strUserLoc As String
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden
        Dim strDec As String
        Dim strEAYear As String
        Dim strTaxBranch As String
        Dim strPeriodEnd As String
        Dim strRefNo As String
        Dim strNatureBuss As String
        Dim strSignName As String
        Dim strSignNRIC As String
        Dim strSignDesignation As String

        Dim strParam as String
        Dim strDateSetting As String
        Dim objSysCfgDs as New Object()
        Dim objDateFormat as New Object()
        Dim objPeriodEnd as String

        Dim ddlist As DropDownList

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

        ddlist = RptSelect.FindControl("lstDecimal")
        strDec = Trim(ddlist.SelectedItem.value)

        strEAYear = Trim(lstEAYear.SelectedItem.Value)

        If lstTaxBranch.SelectedIndex = 0 Then
            strTaxBranch = ""
        Else
            strTaxBranch = Trim(lstTaxBranch.SelectedItem.Value)
        End If

        If txtRefNo.Text = "" Then
            strRefNo = ""
        Else
            strRefNo = Trim(txtRefNo.Text)
        End If

        If txtNatureBuss.Text = "" Then
            strNatureBuss = ""
        Else
            strNatureBuss = Trim(txtNatureBuss.Text)
        End If

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

        strPeriodEnd = txtPeriodEnd.Text

        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"
        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PR_STDRPT_EAFORM_GET_CONFIG_DATE&errmesg=" & Exp.ToString() & "&redirect=../../en/reports/PR_StdRpt_Selection.aspx")
        End Try

        strDateSetting = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))

        If Not (strPeriodEnd = "") Then
            If objGlobal.mtdValidInputDate(strDateSetting, strPeriodEnd, objDateFormat, objPeriodEnd) = False Then
                lblDate.Text = lblDate.Text & objDateFormat & "."
                lblDate.Visible = True
            End If
        End If

        Response.Write("<Script Language=""JavaScript"">window.open(""PR_StdRpt_StmtTaxDeductPreview.aspx?Type=Print&CompName=" & strCompany & _
                        "&Location=" & strUserLoc & "&Decimal=" & strDec & _
                        "&EAYear=" & strEAYear & "&TaxBranch=" & strTaxBranch & _
                        "&PeriodEnd=" & objPeriodEnd & "&RefNo=" & strRefNo & "&NatureBuss=" & strNatureBuss & _
                        "&SignName=" & strSignName & "&SignNRIC=" & strSignNRIC & "&SignDesignation=" & strSignDesignation & _
                        """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

    End Sub

End Class
