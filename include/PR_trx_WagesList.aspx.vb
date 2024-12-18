Imports System
Imports System.Data
Imports System.Collections 
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic


Public Class PR_trx_WagesList : Inherits Page

    Protected WithEvents dgLine As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents txtEmpCode As TextBox
    Protected WithEvents txtName As TextBox
    Protected WithEvents txtChequeNo As TextBox
    Protected WithEvents txtWagesPeriod As TextBox
    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents btnPaidAll As ImageButton
    Protected WithEvents lblSelection As Label
    Protected WithEvents lblErrProcess As Label
    Dim objHRTrx As New agri.HR.clsTrx()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objPRSetup As New agri.PR.clsSetup()
    Protected objPRTrx As New agri.PR.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intPRAR As Long

    Dim objWagesDs As New DataSet()

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intPRAR = Session("SS_PRAR")
        
        lblErrProcess.Visible = False

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxWagesPayment), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.Text = "" Then
                SortExpression.Text = "E.EmpCode"
            End If
            If Not Page.IsPostBack Then
                BindSearchList()
                BindGrid()
                BindPageList()
            End If
        End If
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgLine.CurrentPageIndex = 0
        dgLine.EditItemIndex = -1
        BindGrid()
        BindPageList()
    End Sub

    Sub BindGrid()


        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadData()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgLine.PageSize)

        dgLine.DataSource = dsData
        If dgLine.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgLine.CurrentPageIndex = 0
            Else
                dgLine.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgLine.DataBind()
        BindPageList()
        PageNo = dgLine.CurrentPageIndex + 1
        lblTracker.Text = "Page " & PageNo & " of " & dgLine.PageCount
    End Sub

    Sub BindSearchList()
        ddlStatus.Items.Add(New ListItem("All", ""))
        ddlStatus.Items.Add(New ListItem(objPRTrx.mtdGetWagesStatus(objPRTrx.EnumWagesStatus.Active), objPRTrx.EnumWagesStatus.Active))
        ddlStatus.Items.Add(New ListItem(objPRTrx.mtdGetWagesStatus(objPRTrx.EnumWagesStatus.Duplicated), objPRTrx.EnumWagesStatus.Duplicated))
        ddlStatus.Items.Add(New ListItem(objPRTrx.mtdGetWagesStatus(objPRTrx.EnumWagesStatus.Paid), objPRTrx.EnumWagesStatus.Paid))
        ddlStatus.Items.Add(New ListItem(objPRTrx.mtdGetWagesStatus(objPRTrx.EnumWagesStatus.Printed), objPRTrx.EnumWagesStatus.Printed))
        ddlStatus.Items.Add(New ListItem(objPRTrx.mtdGetWagesStatus(objPRTrx.EnumWagesStatus.Void), objPRTrx.EnumWagesStatus.Void))
        ddlStatus.SelectedIndex = 1
    End Sub

    Sub BindPageList()
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgLine.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgLine.CurrentPageIndex
    End Sub

    Sub Link_Click(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        If Not E.CommandName.ToString = "Sort" And Not E.CommandName.ToString = "Delete" Then
            Dim lblValue As Label
            Dim intIndex As Integer = E.Item.ItemIndex
            Dim strWagesID As String
            Dim strAccMonth As String
            Dim strAccYear As String
            Dim strCompCode As String
            Dim strLocCode As String
            Dim strEmpCode As String

            lblValue = dgLine.Items.Item(intIndex).FindControl("lblWagesID")
            strWagesID = lblValue.Text.Trim()

            lblValue = dgLine.Items.Item(intIndex).FindControl("lblAccMonth")
            strAccMonth = lblValue.Text.Trim()

            lblValue = dgLine.Items.Item(intIndex).FindControl("lblAccYear")
            strAccYear = lblValue.Text.Trim()

            lblValue = dgLine.Items.Item(intIndex).FindControl("lblCompCode")
            strCompCode = lblValue.Text.Trim()

            lblValue = dgLine.Items.Item(intIndex).FindControl("lblLocCode")
            strLocCode = lblValue.Text.Trim()

            lblValue = dgLine.Items.Item(intIndex).FindControl("lblEmpCode")
            strEmpCode = lblValue.Text.Trim()

            Response.Redirect("PR_Trx_WagesDet.aspx?WagesID=" & strWagesID & "&AccMonth=" & strAccMonth & "&AccYear=" & strAccYear & "&CompCode=" & strCompCode & "&LocCode=" & strLocCode & "&empcode=" & strEmpCode)
        End If
    End Sub

    Protected Function LoadData() As DataSet
        Dim strOpCd_GET As String = "PR_CLSTRX_WAGES_SEARCH"
        Dim strSrchEmpCode As String
        Dim strSrchName As String
        Dim strSrchChequeNo As String
        Dim strSrchPeriod As String
        Dim strSrchStatus As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strSearch As String
        Dim arrPeriod As Array
        Dim strEmpCode As String
        Dim strEmpName As String
        Dim strChequeNo As String
        Dim strStatus As String
        Dim strPeriod As String


        strSrchEmpCode = IIf(txtEmpCode.Text = "", "", txtEmpCode.Text)
        strSrchName = IIf(txtName.Text = "", "", txtName.Text)
        strSrchChequeNo = IIf(txtChequeNo.Text = "", "", txtChequeNo.Text)
        strSrchPeriod = IIf(txtWagesPeriod.Text = "", "", txtWagesPeriod.Text)
        strSrchStatus = IIf(ddlStatus.SelectedItem.Value = "", "", ddlStatus.SelectedItem.Value)
    
         strEmpCode = IIf(Trim(strSrchEmpCode) = "", "", "E.EmpCode LIKE '" & Trim(strSrchEmpCode) & "%' AND ")
         strEmpName = IIf(Trim(strSrchName) = "", "", "E.EmpName LIKE '" & Trim(strSrchName) & "%' AND ")
         strChequeNo = IIf(Trim(strChequeNo) = "", "", "E.ChequeNo LIKE '" & Trim(strChequeNo) & "%' AND ")
         strStatus = IIf(Trim(strSrchStatus) = "", "", "E.Status = '" & Trim(strSrchStatus) & "' AND ")

         IF strSrchPeriod <> "" Then
            If InStr(strSrchPeriod, "/") Then
                arrPeriod = Split(strSrchPeriod, "/")
                strPeriod = "E.AccMonth = '" & arrPeriod(0) & "' AND E.AccYear = '" & arrPeriod(1) & "' AND "
            End If
         End If

        strSearch = strEmpCode & strEmpName & strChequeNo & strPeriod & strStatus
        If (Trim(strSearch) <> "") Then
              strSearch = " AND " & Mid(strSearch, 1, Len(strSearch) - 4) & " "
        End If
        lblSelection.Text = strSearch 

        strParam = strSrchEmpCode & "|" & _
                   strSrchName & "|" & _
                   strSrchChequeNo & "|" & _
                   strSrchPeriod & "|" & _
                   strSrchStatus & "|" & _
                   SortExpression.Text & "|" & _
                   SortCol.Text & "|"

        Try
            intErrNo = objPRTrx.mtdGetWagesPayment(strOpCd_GET, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strAccMonth, _
                                                    strAccYear, _
                                                    strParam, _
                                                    objWagesDs, _
                                                    False)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_WAGES_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try
        If  objWagesDs.Tables(0).Rows.Count > 0 And  ddlStatus.SelectedItem.Value = cstr(Trim(objPRTrx.EnumWagesStatus.Active)) Then
            btnPaidAll.Visible = True
            btnPaidAll.Attributes("onclick") = "javascript:return ConfirmAction('Paid All');"
        Else
            btnPaidAll.Visible = False
        End If
        Return objWagesDs
    End Function

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgLine.CurrentPageIndex = 0
            Case "prev"
                dgLine.CurrentPageIndex = _
                    Math.Max(0, dgLine.CurrentPageIndex - 1)
            Case "next"
                dgLine.CurrentPageIndex = _
                    Math.Min(dgLine.PageCount - 1, dgLine.CurrentPageIndex + 1)
            Case "last"
                dgLine.CurrentPageIndex = dgLine.PageCount - 1
        End Select
        lstDropList.SelectedIndex = dgLine.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgLine.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgLine.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        dgLine.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub btnPaidAll_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
       Dim intErrNo As Integer
       Dim strOpCode As String = "PR_CLSTRX_WAGES_PAYALL_SP"
       Dim strOpCdPaySetup As String = "PR_CLSSETUP_PAYROLLPROCESS_GET"
       Dim strOpCdDoubleEntry As String = "GL_CLSSETUP_ENTRYSETUP_GET"
       Dim strParam As String
       Dim strParamName As String
       Dim strParamValue As String
       Dim strCashStatus As String
       Dim strAccCash As String
       Dim strAccBank As String
       Dim strAccPayroll As String
       Dim strDocType As String
       Dim strModulCode As String
       Dim strPaidStatus As String
       Dim objPaySetupDs As DataSet
       Dim objGLDs As DataSet


        Try
            strParam = "|"
            intErrNo = objPRSetup.mtdGetMasterList(strOpCdPaySetup, _
                                                   strParam, _
                                                   0, _
                                                   objPaySetupDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_WAGES_PAYROLLPROCESS_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objPaySetupDs.Tables(0).Rows.Count > 0 Then
            If Trim(objPaySetupDs.Tables(0).Rows(0).Item("BankCode")) = "" then
                 lblErrProcess.Text = "please define Bank Code on payroll setup"
                 lblErrProcess.Visible = False
                 Exit Sub
            End If 
            If Trim(objPaySetupDs.Tables(0).Rows(0).Item("CashAcc")) = "" then
                 lblErrProcess.Text = "please define Cash Account on payroll setup"
                 lblErrProcess.Visible = False
                 Exit Sub
            End If 
            strAccCash = Trim(objPaySetupDs.Tables(0).Rows(0).Item("CashAcc"))
            strAccBank = Trim(objPaySetupDs.Tables(0).Rows(0).Item("BankCode"))
        Else
             lblErrProcess.Text = "please define payroll setup"
             lblErrProcess.Visible = False
             Exit Sub
        End If
        
        Try
            strParam = "|"
            intErrNo = objGLSetup.mtdEntrySetup(strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strAccMonth, _
                                                strAccYear, _
                                                strOpCdDoubleEntry, _
                                                strParam, _
                                                True, _
                                                objGLDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_WAGES_DBLENTRY_GET&errmesg=" & Exp.ToString & "&redirect=")
        End Try

        If objGLDs.Tables(0).Rows.Count > 0 Then
             
            Dim objGLDv As DataView = New DataView(objGLDs.Tables(0), _
                                      "EntryType = '33'", _
                                      "CRAccCode", _
                                      DataViewRowState.CurrentRows)



            If objGLDv.Count > 0
               strAccPayroll = Trim(objGLDv(0)("CRAccCode"))
            Else 
                lblErrProcess.Text = "please define double entry for Payroll Clearence Account"
                lblErrProcess.Visible = False
                Exit Sub
            End IF
        Else
             lblErrProcess.Text = "please define double entry for Payroll Clearence Account"
             lblErrProcess.Visible = False
             Exit Sub
        End If

       strParamName = "STRSEARCH|CASHSTAT|ACCCASH|ACCBANK|ACCPAY|DOCTYPE|MODULCODE|PAIDSTAT"
       
       strCashStatus = objHRTrx.EnumPayMode.Cash
       strDocType = objGlobal.EnumDocType.PRWagesPay
       strModulCode = objGlobal.EnumModule.Payroll
       strPaidStatus = objPRTrx.EnumWagesStatus.Paid

       strParamValue = lblSelection.Text & "|" & strCashStatus & "|" & strAccCash & "|" & strAccBank & _
                       "|" & strAccPayroll & "|" & strDocType & "|" & strModulCode & "|" & strPaidStatus
        
        
        Try
            intErrNo = objPRTrx.mtdPaidAllWages(strOpCode, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strAccMonth, _
                                                    strAccYear, _
                                                    strParamName, _
                                                    strParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_WAGES_PAIDALL&errmesg=" & Exp.ToString() & "&redirect=")
        End Try
        
        BindGrid()
        BindPageList()
        
    End Sub



End Class
