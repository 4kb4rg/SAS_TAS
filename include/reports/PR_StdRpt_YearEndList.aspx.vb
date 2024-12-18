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

Public Class PR_StdRpt_YearEndList : Inherits Page

    Protected RptSelect As UserControl

    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objHR As New agri.HR.clsTrx()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdmAcc As New agri.Admin.clsAccPeriod()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Protected WithEvents lblTracker As Label
    Protected WithEvents lblLocation As Label

    Protected WithEvents lstEAYear As DropDownList
    Protected WithEvents lstTaxBranch As DropDownList

    Protected WithEvents PrintPrev As ImageButton

    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfigsetting As Integer
    Dim strUserLoc As String

    Dim dr As DataRow
    Dim intCnt As Integer
    Dim intErrNo As Integer
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        Dim intMaxPeriod As Integer
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")
        
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
                onload_GetLangCap()
                intMaxPeriod = BindAccYearList(strLocation, strAccYear)  
                BindTaxBranch()
            End If   
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PWSYSTEM_CLSLANGCAP_BUSSTERM_GET&errmesg=" & Exp.ToString() & "&redirect=../en/reports/PR_StdRpt_Selection.aspx")
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_YEAREND_LIST_ACCCFG_DIST_GET&errmesg=" & Exp.ToString() & "&redirect=")
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
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String
        Dim strEAYear As String
        Dim strTaxBranch As String

        Dim tempRptName As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden

        tempRptName = RptSelect.FindControl("lstRptName")
        strRptName = Trim(tempRptName.SelectedItem.Value)
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

        strEAYear = Trim(lstEAYear.SelectedItem.Value)

        If lstTaxBranch.SelectedIndex = 0 Then
            strTaxBranch = ""
        Else
            strTaxBranch = Trim(lstTaxBranch.SelectedItem.Value)
        End If

        Response.Write("<Script Language=""JavaScript"">window.open(""PR_StdRpt_YearEndListPreview.aspx?Type=Print&CompName=" & strCompany & _
                       "&Location=" & strUserLoc & "&RptName=" & strRptName & "&Decimal=" & strDec & "&lblLocation=" & lblLocation.Text & _
                       "&EAYear=" & strEAYear & "&TaxBranch=" & strTaxBranch & _
                       """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

End Class
