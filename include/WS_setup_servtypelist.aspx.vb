Imports System
Imports System.Data
Imports System.Collections 
Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic


Public Class WS_ServTypeList : Inherits Page

    Protected WithEvents dgServType As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents txtServType As TextBox
    Protected WithEvents txtDescription As TextBox
    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblServType As Label
    Protected WithEvents lblServTypeDesc As Label
    Protected WithEvents blnUpdate as Label
    Protected WithEvents lblErrDupST as Label
    Protected WithEvents lblDuplicateCode as Label

    Protected objWS As New agri.WS.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objLoc As New agri.Admin.clsLoc()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intWSAR As Integer
    Dim strLocType as String


    Dim objServTypeDs As New Object()
    Dim objLangCapDs As New Object()
    Dim objServTypeDUPDs As New Object() 

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        intWSAR = Session("SS_WSAR")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSWorkshopService), intWSAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            If SortExpression.Text = "" Then
                SortExpression.Text = "ServTypeCode"
            End If
            If Not Page.IsPostBack Then
                BindGrid()
                BindPageList()
            End If
        End If
    End Sub


    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblServType.Text = GetCaption(objLangCap.EnumLangCap.ServType)
        lblServTypeDesc.Text = GetCaption(objLangCap.EnumLangCap.ServTypeDesc)
        lblTitle.Text = UCase(lblServType.Text)

        dgServType.Columns(1).HeaderText = lblServType.Text
        dgServType.Columns(2).HeaderText = lblServTypeDesc.Text        
        
        
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_SETUP_SERVICETYPE_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=ws/setup/ws_servtypelist.aspx")
        End Try

    End Sub



    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

       For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If strLocType = objLoc.EnumLocType.Mill then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                end if
                Exit For
            End If
        Next
    End Function



    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgServType.CurrentPageIndex = 0
        dgServType.EditItemIndex = -1
        BindGrid()
        BindPageList()
    End Sub

    Sub BindGrid()
        Dim intCnt As Integer
        Dim lbButton As LinkButton
        Dim lbl As Label

        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadData()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgServType.PageSize)

        dgServType.DataSource = dsData
        If dgServType.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgServType.CurrentPageIndex = 0
            Else
                dgServType.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgServType.DataBind()
        BindPageList()
        PageNo = dgServType.CurrentPageIndex + 1
        lblTracker.Text = "Page " & PageNo & " of " & dgServType.PageCount


    End Sub

    Sub BindPageList()
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgServType.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgServType.CurrentPageIndex
    End Sub

    Protected Function LoadData() As DataSet
        Dim strOpCd_GET As String = "WS_CLSSETUP_SERVTYPE_LIST_GET"
        Dim strSrchServType As String
        Dim strSrchDescription As String
        Dim strSrchStatus As String
        Dim strSrchLastUpdate As String
        Dim strSearch As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer

        strSrchServType = IIf(txtServType.Text = "", "", txtServType.Text)
        strSrchDescription = IIf(txtDescription.Text = "", "", txtDescription.Text)
        strSrchStatus = IIf(ddlStatus.SelectedItem.Value = "0", "", ddlStatus.SelectedItem.Value)
        strSrchLastUpdate = IIf(txtLastUpdate.Text = "", "", txtLastUpdate.Text)
        strParam = strSrchServType & "|" & _
                   strSrchDescription & "|" & _
                   strSrchStatus & "|" & _
                   strSrchLastUpdate & "|" & _
                   SortExpression.Text & "|" & _
                   SortCol.Text & "|"

        Try
            intErrNo = objWS.mtdGetServType(strOpCd_GET, _
                                            strParam, _
                                            objServTypeDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=SERVTYPELIST_GET_SERVTYPE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objServTypeDs.Tables(0).Rows.Count - 1
            objServTypeDs.Tables(0).Rows(intCnt).Item(0) = Trim(objServTypeDs.Tables(0).Rows(intCnt).Item(0))
            objServTypeDs.Tables(0).Rows(intCnt).Item(1) = Trim(objServTypeDs.Tables(0).Rows(intCnt).Item(1))
            objServTypeDs.Tables(0).Rows(intCnt).Item(5) = Trim(objServTypeDs.Tables(0).Rows(intCnt).Item(5))
            objServTypeDs.Tables(0).Rows(intCnt).Item(3) = Trim(objServTypeDs.Tables(0).Rows(intCnt).Item(3))
        Next

        Return objServTypeDs
    End Function

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgServType.CurrentPageIndex = 0
            Case "prev"
                dgServType.CurrentPageIndex = _
                    Math.Max(0, dgServType.CurrentPageIndex - 1)
            Case "next"
                dgServType.CurrentPageIndex = _
                    Math.Min(dgServType.PageCount - 1, dgServType.CurrentPageIndex + 1)
            Case "last"
                dgServType.CurrentPageIndex = dgServType.PageCount - 1
        End Select
        lstDropList.SelectedIndex = dgServType.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgServType.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgServType.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        dgServType.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    

    Sub DEDR_Cancel(Sender As Object, E As DataGridCommandEventArgs)
        dgServType.EditItemIndex = -1
        BindGrid() 
    End Sub

    Sub DEDR_Edit(Sender As Object, E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim List As Dropdownlist
        Dim Updbutton As linkbutton
        Dim ValidateServTypeCode As RequiredFieldValidator
        Dim validateDesc As RequiredFieldValidator
        Dim lblMsg as label
        
        blnUpdate.text = True
        dgServType.EditItemIndex = CInt(e.Item.ItemIndex)

        BindGrid() 
        If CInt(e.Item.ItemIndex) >= dgServType.Items.Count then
            dgServType.EditItemIndex = -1
            Exit Sub
        End If        

        EditText = dgServType.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Status")        
        Select Case cint(Edittext.text) = objWS.EnumServiceTypeStatus.Active
            Case True
                EditText = dgServType.Items.Item(CInt(e.Item.ItemIndex)).FindControl("ServTypeCode")
                EditText.readonly = true
                Updbutton = dgServType.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Delete")
                Updbutton.Text = "Delete"
                Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case False
                EditText = dgServType.Items.Item(CInt(e.Item.ItemIndex)).FindControl("ServTypeCode")
                EditText.Enabled = False
                EditText = dgServType.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Description")
                EditText.Enabled = False
                EditText = dgServType.Items.Item(CInt(e.Item.ItemIndex)).FindControl("UpdateDate")
                EditText.Enabled = False
                Updbutton = dgServType.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Update")
                Updbutton.Visible = False
                Updbutton = dgServType.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Delete")
                Updbutton.Text = "Undelete"
        End Select  
        ValidateServTypeCode = dgServType.Items.Item(CInt(e.Item.ItemIndex)).FindControl("validateServTypeCode")
        ValidateDesc = dgServType.Items.Item(CInt(e.Item.ItemIndex)).FindControl("validateDesc")

        ValidateServTypeCode.ErrorMessage = "Please enter " & lblServType.text & "."
        ValidateDesc.ErrorMessage = "Please enter " & lblServTypeDesc.text & "."        
    End Sub


    Sub DEDR_Update(Sender As Object, E As DataGridCommandEventArgs)  
        Dim strOpCd_Add As String = "WS_CLSSETUP_SERVTYPE_LIST_ADD"
        Dim strOpCd_Upd As String = "WS_CLSSETUP_SERVTYPE_LIST_UPD"
        Dim strOpCd_Get As String = "WS_CLSSETUP_SERVTYPE_LIST_GET"      
        Dim EditText As TextBox
        Dim list As Dropdownlist
        Dim ServTypeCode As String
        Dim Description As String
        Dim strAccCode As String = ""
        Dim stStatus As String
        Dim blnDupKey As Boolean = False
        Dim lblMsg as label
        Dim CreateDate As String
        Dim strParam As String = ""
        Dim intErrNo As Integer                
         
        EditText = E.Item.FindControl("ServTypeCode")
        ServTypeCode = EditText.Text
        EditText = E.Item.FindControl("Description")
        Description = EditText.Text        
        EditText = dgServType.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Status")
        stStatus = EditText.Text 
        EditText = E.Item.FindControl("CreateDate")
        CreateDate = EditText.Text
       
        If blnUpdate.Text = "True" then

            strParam =  ServTypeCode & "|" & _
                        Description & "|" & _
                        "|" & _
                        stStatus 
        Else

            strParam = ServTypeCode & "||||ServTypeCode" & "||Add"
            
            Try
                intErrNo = objWS.mtdGetServType(strOpCd_Get, strParam, objServTypeDUPDs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=SERVTYPEDET_GET_SERVTYPE&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try
            
            If objServTypeDUPDs.Tables(0).Rows.Count <> 0 Then
                lblMsg = E.Item.FindControl("lblErrDupST")                
                lblMsg.Text = lblDuplicateCode.Text                
                lblMsg.Visible = True
                Exit Sub
            Else                    
                lblMsg = E.Item.FindControl("lblErrDupST")
                lblMsg.Text = ""
                lblMsg.Visible = True                  


                strParam = ServTypeCode & "|" & _
                                   Description & "|" & _
                                   "|" & _
                                   objWS.EnumStatus.Active & "|"
            End If
        End If

        Try
        intErrNo = objWS.mtdUpdServType(strOpCd_Add, _
                                        strOpCd_Upd, _
                                        strCompany, _
                                        strLocation, _
                                        strUserId, _
                                        strParam, _
                                        blnUpdate.Text)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=SERVTYPEDET_UPD_SERVTYPE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        
        dgServType.EditItemIndex = -1
        BindGrid() 

    End Sub


    Sub DEDR_Delete(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim strOpCd_ADD As String = ""
        Dim strOpCd_UPD As String = "WS_CLSSETUP_SERVTYPE_LIST_UPD"
        Dim strParam As String = ""
        Dim ServTypeCell As TableCell = e.Item.Cells(0)
        Dim strSelectedServType As String
        Dim intErrNo As Integer
        Dim EditText As TextBox
        Dim stStatus As String = ""

        EditText = E.Item.FindControl("ServTypeCode")
        strSelectedServType = EditText.Text

        EditText = dgServType.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Status")
        stStatus = IIF(EditText.Text = objWS.EnumServiceTypeStatus.Active, _
                        objWS.EnumServiceTypeStatus.Deleted, _
                        objWS.EnumServiceTypeStatus.Active )

        
        strParam = strSelectedServType & "|||" & stStatus

        Try
            intErrNo = objWS.mtdUpdServType(strOpCd_ADD, _
                                            strOpCd_UPD, _
                                            strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strParam, _
                                            True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=SERVTYPELIST_DEL_SERVTYPE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        dgServType.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub DEDR_Add(Sender As Object, E As ImageClickEventArgs)
        Dim dataSet As DataSet = LoadData
        Dim newRow As DataRow
        Dim Updbutton as LinkButton             
        Dim validateDesc As RequiredFieldValidator
        Dim ValidateServTypeCode As RequiredFieldValidator 
        Dim PageCount As Integer
        
        
        blnUpdate.text = False
        newRow = dataSet.Tables(0).NewRow()
        newRow.Item("ServTypeCode") = ""        
        newRow.Item("Description") = ""
        newRow.Item("Status") = 1
        newRow.Item("CreateDate") = DateTime.now()
        NewRow.Item("UpdateDate") = DateTime.now()
        NewRow.Item("UserName") = ""
        dataSet.Tables(0).Rows.Add(newRow)
        
        dgServType.DataSource = dataSet
        PageCount = objGlobal.mtdGetPageCount(dataSet.Tables(0).Rows.Count, dgServType.PageSize)
        If dgServType.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgServType.CurrentPageIndex = 0
            Else
                dgServType.CurrentPageIndex = PageCount - 1
            End If
        End If
        dgServType.DataBind()
        BindPageList()

        dgServType.CurrentPageIndex = dgServType.PageCount - 1
        lblTracker.Text="Page " & (dgServType.CurrentPageIndex + 1) & " of " & dgServType.PageCount
        lstDropList.SelectedIndex = dgServType.CurrentPageIndex
        dgServType.DataBind()
        dgServType.EditItemIndex = dgServType.Items.Count -1
        dgServType.DataBind()

        Updbutton = dgServType.Items.Item(CInt(dgServType.EditItemIndex)).FindControl("Delete")
        Updbutton.visible = False
        
        ValidateServTypeCode = dgServType.Items.Item(CInt(dgServType.EditItemIndex)).FindControl("validateServTypeCode")
        validateDesc = dgServType.Items.Item(CInt(dgServType.EditItemIndex)).FindControl("validateDesc")

        ValidateServTypeCode.ErrorMessage = "Please enter " & lblServType.text & "."
        validateDesc.ErrorMessage = "Please enter " & lblServTypeDesc.text & "."              
    End Sub

    Sub btnPreview_Click (sender As Object, e As ImageClickEventArgs)
        Dim strStatus As String
        Dim strServType As String
        Dim strDescription As String
        Dim strUpdateBy As String
        Dim strSortExp As String
        Dim strSortCol As String

        strStatus = IIF(Not ddlStatus.selectedItem.Value = "0", ddlStatus.selectedItem.Value, "")
        strServType = txtServType.text
        strDescription = txtDescription.text
        strUpdateBy =  txtLastUpdate.text
        strSortExp = SortExpression.text
        strSortCol = SortCol.text

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/WS_Rpt_ServTypeList.aspx?strServTypeTag=" & lblServType.Text & _
                    "&strDescTag=" & lblServTypeDesc.Text & "&strTitleTag=" & lblServType.Text & _
                    "&strStatus=" & strStatus & "&strServType=" & strServType & "&strDescription=" & strDescription & _
                    "&strUpdateBy=" & strUpdateBy & "&strSortExp=" & strSortExp & _
                    "&strSortCol=" & strSortCol & """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub




End Class
