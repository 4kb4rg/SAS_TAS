Imports System
Imports System.Data
Imports System.Collections 
Imports System.Data.SqlClient
Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic



Public Class PR_setup_IncentiveList : Inherits Page

    Protected WithEvents dgLine As DataGrid
    Protected WithEvents SortExpression As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblDupMsg As Label
    Protected WithEvents lblPleaseEnter As Label
    Protected WithEvents lblList As Label
    Protected WithEvents sortcol As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents srchIncentiveCode As TextBox
    Protected WithEvents srchDesc As TextBox
    Protected WithEvents srchStatusList As DropDownList
    Protected WithEvents srchUpdateBy As TextBox
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblIncentiveCode As Label
    Protected WithEvents lblDescription As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents ddlStatus As DropDownList

    Protected WithEvents ddlPremiType As DropDownList
    Protected WithEvents lblErrPremiType As Label
    Protected WithEvents txtPercentage As TextBox
    Protected WithEvents txtRate As TextBox            

    Protected objPR As New agri.PR.clsSetup
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl
    Dim objAR As New agri.GlobalHdl.clsAccessRights
    Dim objLangCap As New agri.PWSystem.clsLangCap
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim strOppCd_GET As String = "PR_CLSSETUP_INCENTIVE_LIST_GET"
    Dim strOppCd_ADD As String = "PR_CLSSETUP_INCENTIVE_LIST_ADD"
    Dim strOppCd_UPD As String = "PR_CLSSETUP_INCENTIVE_LIST_UPD"

    Dim objDataSet As New Object
    Dim objLangCapDs As New Object
    Dim intErrNo As Integer
    Dim strParam As String = ""
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intPRAR As Long
    Dim strAccMonth As String
    Dim strAccYear As String

    Dim strValidateCode As String
    Dim strValidateDesc As String
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intPRAR = Session("SS_PRAR")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRPremi), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")

        Else
            onload_GetLangCap()
            If SortExpression.Text = "" Then
                SortExpression.Text = "IncentiveCode"
                sortcol.Text = "ASC"
            End If

            If Not Page.IsPostBack Then                
                BindSearchList()
                BindGrid()
                BindPageList()                
            End If
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblTitle.Text = UCase(GetCaption(objLangCap.EnumLangCap.Incentive))
        lblIncentiveCode.Text = GetCaption(objLangCap.EnumLangCap.Incentive) & lblCode.Text
        lblDescription.Text = GetCaption(objLangCap.EnumLangCap.IncentiveDesc)

        strValidateCode = lblPleaseEnter.Text & lblIncentiveCode.Text & "."
        strValidateDesc = lblPleaseEnter.Text & lblDescription.Text & "."

        dgLine.Columns(0).HeaderText = lblIncentiveCode.Text
        dgLine.Columns(1).HeaderText = lblDescription.Text

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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_INCENTIVELIST_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=pr/Setup/pr_setup_IncentiveList.aspx")
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

    Sub BindPageList()

        Dim count As Integer = 1
        Dim arrDList As New ArrayList

        While Not count = dgLine.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgLine.CurrentPageIndex

    End Sub

    Sub BindddlStatus(ByVal index As Integer)

        ddlStatus = dgLine.Items.Item(index).FindControl("ddlStatus")
        ddlStatus.Items.Add(New ListItem(objPR.mtdGetIncentiveStatus(objPR.EnumIncentiveStatus.Active), objPR.EnumIncentiveStatus.Active))
        ddlStatus.Items.Add(New ListItem(objPR.mtdGetIncentiveStatus(objPR.EnumIncentiveStatus.Deleted), objPR.EnumIncentiveStatus.Deleted))

    End Sub

    Sub BindSearchList()
        srchStatusList.Items.Add(New ListItem(objPR.mtdGetIncentiveStatus(objPR.EnumIncentiveStatus.All), objPR.EnumIncentiveStatus.All))
        srchStatusList.Items.Add(New ListItem(objPR.mtdGetIncentiveStatus(objPR.EnumIncentiveStatus.Active), objPR.EnumIncentiveStatus.Active))
        srchStatusList.Items.Add(New ListItem(objPR.mtdGetIncentiveStatus(objPR.EnumIncentiveStatus.Deleted), objPR.EnumIncentiveStatus.Deleted))

        srchStatusList.SelectedIndex = 1
    End Sub

    Protected Function LoadData() As DataSet

        Dim strParam As String
        Dim SearchStr As String
        Dim sortitem As String


        SearchStr = " AND Inc.Status like '" & IIf(Not srchStatusList.SelectedItem.Value = objPR.EnumIncentiveStatus.All, srchStatusList.SelectedItem.Value, "% ") & "' "
        SearchStr = SearchStr & " AND Inc.LocCode = '" & strLocation & "' "

        If Not srchIncentiveCode.Text = "" Then
            SearchStr = SearchStr & " AND Inc.IncentiveCode like '" & srchIncentiveCode.Text & "%' "
        End If

        If Not srchDesc.Text = "" Then
            SearchStr = SearchStr & " AND Inc.Description like '" & srchDesc.Text & "%' "
        End If

        If Not srchUpdateBy.Text = "" Then
            SearchStr = SearchStr & " AND usr.Username like '" & srchUpdateBy.Text & "%' "
        End If

        sortitem = "ORDER BY " & SortExpression.Text & " " & sortcol.Text
        strParam = sortitem & "|" & SearchStr

        Try
            intErrNo = objPR.mtdGetIncentiveList(strOppCd_GET, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_INCENTIVELIST&errmesg=" & lblErrMessage.Text & "&redirect=pr/Setup/pr_setup_IncentiveList.aspx")
        End Try

        Return objDataSet
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
        sortcol.Text = IIf(sortcol.Text = "ASC", "DESC", "ASC")
        dgLine.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim List As DropDownList
        Dim EditLbl As Label
        Dim strPremiType As String         
        Dim Updbutton As LinkButton
        Dim validateCode As RequiredFieldValidator
        Dim validateDesc As RequiredFieldValidator

        blnUpdate.Text = True
        dgLine.EditItemIndex = CInt(E.Item.ItemIndex)

        BindGrid()
        If CInt(E.Item.ItemIndex) >= dgLine.Items.Count Then
            dgLine.EditItemIndex = -1
            Exit Sub
        End If
        BindddlStatus(dgLine.EditItemIndex)
        BindPremiType(dgLine.EditItemIndex)
        
        Editlbl = dgLine.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblPremiType")    
        strPremiType = Editlbl.Text
        
        If strPremiType <> "" Then
            List = dgLine.Items.Item(CInt(E.Item.ItemIndex)).FindControl("ddlPremiType")
            List.SelectedIndex = Cint(strPremiType)
        End If
                

        EditText = dgLine.Items.Item(CInt(E.Item.ItemIndex)).FindControl("txtStatus")
        Select Case CInt(EditText.Text) = objPR.EnumIncentiveStatus.Active
            Case True
                ddlStatus.SelectedIndex = 0
                EditText = dgLine.Items.Item(CInt(E.Item.ItemIndex)).FindControl("txtIncentiveCode")
                EditText.ReadOnly = True
                Updbutton = dgLine.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Delete")
                Updbutton.Text = "Delete"
                Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case False
                ddlStatus.SelectedIndex = 1
                EditText = dgLine.Items.Item(CInt(E.Item.ItemIndex)).FindControl("txtIncentiveCode")
                EditText.Enabled = False
                EditText = dgLine.Items.Item(CInt(E.Item.ItemIndex)).FindControl("txtDescription")
                EditText.Enabled = False
                EditText = dgLine.Items.Item(CInt(E.Item.ItemIndex)).FindControl("txtRate")
                EditText.Enabled = False
                EditText = dgLine.Items.Item(CInt(E.Item.ItemIndex)).FindControl("txtUpdateDate")
                EditText.Enabled = False
                EditText = dgLine.Items.Item(CInt(E.Item.ItemIndex)).FindControl("txtUserName")
                EditText.Enabled = False
                List = dgLine.Items.Item(CInt(E.Item.ItemIndex)).FindControl("ddlStatus")
                List.Enabled = False
                Updbutton = dgLine.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Update")
                Updbutton.Visible = False
                Updbutton = dgLine.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Delete")
                Updbutton.Text = "Undelete"
        End Select
        validateCode = dgLine.Items.Item(CInt(E.Item.ItemIndex)).FindControl("ValidateCode")
        validateDesc = dgLine.Items.Item(CInt(E.Item.ItemIndex)).FindControl("ValidateDesc")

        validateCode.ErrorMessage = strValidateCode
        validateDesc.ErrorMessage = strValidateDesc

    End Sub

    Sub DEDR_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim list As DropDownList
        Dim strIncentiveCode As String
        Dim strDescription As String
        Dim strRate As String
        Dim strStatus As String
        Dim blnDupKey As Boolean = False
        Dim lblMsg As Label
        Dim strCreateDate As String
        Dim strPremiType As String
        Dim strPercentage As String

        EditText = E.Item.FindControl("txtIncentiveCode")
        strIncentiveCode = EditText.Text
        EditText = E.Item.FindControl("txtDescription")
        strDescription = EditText.Text
        EditText = E.Item.FindControl("txtRate")
        strRate = EditText.Text
        strRate = iif(strRate = "","0",strRate)
        list = E.Item.FindControl("ddlStatus")
        strStatus = list.SelectedItem.Value
        EditText = E.Item.FindControl("txtCreateDate")
        strCreateDate = EditText.Text
        
        list = E.Item.FindControl("ddlPremiType")  
        strPremiType = list.SelectedItem.Value
        EditText = E.Item.FindControl("txtPercentage")
        strPercentage = EditText.Text
        strParam = strIncentiveCode & "|" & strDescription & "|" & strRate & "|" & strStatus & "|" & strCreateDate & _
                   "|" & strPremiType & "|" & strPercentage & "|" & strLocation   
        

        Try
            intErrNo = objPR.mtdUpdIncentiveList(strOppCd_ADD, _
                                                strOppCd_UPD, _
                                                strOppCd_GET, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                blnDupKey, _
                                                blnUpdate.Text)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_INCENTIVELIST_UPD&errmesg=" & lblErrMessage.Text & "&redirect=pr/Setup/pr_setup_IncentiveList.aspx")
        End Try

        If blnDupKey Then
            lblMsg = E.Item.FindControl("lblDupMsg")
            lblMsg.Visible = True
        Else
            dgLine.EditItemIndex = -1
            BindGrid()
        End If

    End Sub

    Sub DEDR_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        dgLine.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim list As DropDownList
        Dim EditText As TextBox
        Dim strIncentiveCode As String
        Dim strDescription As String
        Dim strRate As String
        Dim strStatus As String
        Dim blnDupKey As Boolean = False
        Dim strCreateDate As String
        Dim strPremiType As String
        Dim strPercentage As String
        
        EditText = E.Item.FindControl("txtIncentiveCode")
        strIncentiveCode = EditText.Text
        EditText = E.Item.FindControl("txtDescription")
        strDescription = EditText.Text
        EditText = E.Item.FindControl("txtRate")
        strRate = EditText.Text
        EditText = E.Item.FindControl("txtStatus")
        strStatus = IIf(EditText.Text = objPR.EnumIncentiveStatus.Active, objPR.EnumIncentiveStatus.Deleted, objPR.EnumIncentiveStatus.Active)
        EditText = E.Item.FindControl("txtCreateDate")
        strCreateDate = EditText.Text

        list = E.Item.FindControl("ddlPremiType")  
        strPremiType = list.SelectedItem.Value
        EditText = E.Item.FindControl("txtPercentage")
        strPercentage = EditText.Text

        strParam = strIncentiveCode & "|" & strDescription & "|" & strRate & "|" & strStatus & "|" & strCreateDate & _
                   "|" & strPremiType & "|" & strPercentage & "|" & strLocation    
 
        Try
            intErrNo = objPR.mtdUpdIncentiveList(strOppCd_ADD, _
                                                strOppCd_UPD, _
                                                strOppCd_GET, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                blnDupKey, _
                                                blnUpdate.Text)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_INCENTIVELIST_DELE&errmesg=" & lblErrMessage.Text & "&redirect=pr/Setup/pr_setup_IncentiveList.aspx")
        End Try

        dgLine.EditItemIndex = -1

        BindGrid()
        BindPageList()
    End Sub

    Sub DEDR_Add(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim dataSet As DataSet = LoadData()
        Dim newRow As DataRow
        Dim Updbutton As LinkButton
        Dim validateCode As RequiredFieldValidator
        Dim validateDesc As RequiredFieldValidator
        Dim PageCount As Integer

        blnUpdate.Text = False
        newRow = dataSet.Tables(0).NewRow()
        newRow.Item("IncentiveCode") = ""
        newRow.Item("Description") = ""
        newRow.Item("Rate") = 0
        newRow.Item("Percentage") = 0
        newRow.Item("Status") = 1
        newRow.Item("PremiType") = 0
        newRow.Item("CreateDate") = DateTime.Now()
        newRow.Item("UpdateDate") = DateTime.Now()
        newRow.Item("UserName") = ""
        dataSet.Tables(0).Rows.Add(newRow)

        dgLine.DataSource = dataSet
        PageCount = objGlobal.mtdGetPageCount(dataSet.Tables(0).Rows.Count, dgLine.PageSize)
        If dgLine.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgLine.CurrentPageIndex = 0
            Else
                dgLine.CurrentPageIndex = PageCount - 1
            End If
        End If
        dgLine.DataBind()
        BindPageList()

        dgLine.CurrentPageIndex = dgLine.PageCount - 1
        lblTracker.Text = "Page " & (dgLine.CurrentPageIndex + 1) & " of " & dgLine.PageCount
        lstDropList.SelectedIndex = dgLine.CurrentPageIndex
        dgLine.DataBind()
        dgLine.EditItemIndex = dgLine.Items.Count - 1
        dgLine.DataBind()
        BindddlStatus(dgLine.EditItemIndex)
        BindPremiType(dgLine.EditItemIndex)
        Updbutton = dgLine.Items.Item(CInt(dgLine.EditItemIndex)).FindControl("Delete")
        Updbutton.Visible = False

        validateCode = dgLine.Items.Item(CInt(dgLine.EditItemIndex)).FindControl("validateCode")
        validateDesc = dgLine.Items.Item(CInt(dgLine.EditItemIndex)).FindControl("validateDesc")

        validateCode.ErrorMessage = strValidateCode
        validateDesc.ErrorMessage = strValidateDesc

    End Sub

    Sub BindPremiType(Byval index As Integer)
        ddlPremiType = dgLine.Items.Item(index).FindControl("ddlPremiType")
        ddlPremiType.Items.Add(New ListItem("Select Premi Type", ""))
        ddlPremiType.Items.Add(New ListItem(objPR.mtdGetPremiType(objPR.EnumPremiType.PremiKeraniPanen), objPR.EnumPremiType.PremiKeraniPanen))
        ddlPremiType.Items.Add(New ListItem(objPR.mtdGetPremiType(objPR.EnumPremiType.PremiMandorPanen), objPR.EnumPremiType.PremiMandorPanen))
        ddlPremiType.Items.Add(New ListItem(objPR.mtdGetPremiType(objPR.EnumPremiType.PremiMandor1), objPR.EnumPremiType.PremiMandor1))
        ddlPremiType.Items.Add(New ListItem(objPR.mtdGetPremiType(objPR.EnumPremiType.PremiMandorTunasPanen), objPR.EnumPremiType.PremiMandorTunasPanen))
        ddlPremiType.Items.Add(New ListItem(objPR.mtdGetPremiType(objPR.EnumPremiType.PremiMandorTunas1), objPR.EnumPremiType.PremiMandorTunas1))
        ddlPremiType.Items.Add(New ListItem(objPR.mtdGetPremiType(objPR.EnumPremiType.PremiKeraniTransport), objPR.EnumPremiType.PremiKeraniTransport))
        ddlPremiType.Items.Add(New ListItem(objPR.mtdGetPremiType(objPR.EnumPremiType.PremiMandorTransport), objPR.EnumPremiType.PremiMandorTransport))
        ddlPremiType.Items.Add(New ListItem(objPR.mtdGetPremiType(objPR.EnumPremiType.PremiTransportKondektur), objPR.EnumPremiType.PremiTransportKondektur))
        ddlPremiType.Items.Add(New ListItem(objPR.mtdGetPremiType(objPR.EnumPremiType.TenagaAplikasiJanjangKosong), objPR.EnumPremiType.TenagaAplikasiJanjangKosong))
        ddlPremiType.Items.Add(New ListItem(objPR.mtdGetPremiType(objPR.EnumPremiType.Others), objPR.EnumPremiType.Others))
    End Sub

    Sub PremiType_Changed (sender As Object, e As EventArgs)
        Dim txt As TextBox
        Dim EditList As DropDownList
        Dim strEmpCode As String
        Dim strDenda As String
    
        EditList = dgLine.Items.Item(CInt(dgLine.EditItemIndex)).FindControl("ddlPremiType")
        strDenda = EditList.SelectedItem.Value

        Select strDenda
            Case objPR.EnumPremiType.PremiKeraniPanen, objPR.EnumPremiType.PremiMandorPanen, _
                objPR.EnumPremiType.PremiMandor1, objPR.EnumPremiType.PremiMandorTunasPanen, _
                objPR.EnumPremiType.PremiMandorTunasPanen, objPR.EnumPremiType.PremiMandorTunas1, _
                objPR.EnumPremiType.PremiKeraniTransport, objPR.EnumPremiType.PremiMandorTransport                   
                    txt = dgLine.Items.Item(CInt(dgLine.EditItemIndex)).FindControl("txtPercentage")
                    txt.Text = ""
                    txt.Enabled = True                            
                    txt = dgLine.Items.Item(CInt(dgLine.EditItemIndex)).FindControl("txtRate")
                    txt.Text = ""
                    txt.Enabled = False                    
            Case Else
                    txt = dgLine.Items.Item(CInt(dgLine.EditItemIndex)).FindControl("txtPercentage")
                    txt.Text = ""
                    txt.Enabled = True            
                    txt = dgLine.Items.Item(CInt(dgLine.EditItemIndex)).FindControl("txtRate")
                    txt.Text = ""                    
                    txt.Enabled = True
                    
        End Select         

    End Sub


End Class
