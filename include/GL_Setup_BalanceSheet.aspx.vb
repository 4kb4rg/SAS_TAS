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
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.PWSystem.clsLangCap
Imports agri.PWSystem.clsConfig
Imports agri.GL.clsSetup


Public Class GL_Setup_BalanceSheet : Inherits Page

    Protected WithEvents TmplList As DataGrid
    Protected WithEvents ErrorMessage As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblOper As Label
    Protected WithEvents lblDupDispSeq As Label
    Protected WithEvents lblAccount As Label
    Protected WithEvents lblErrCalc As Label
    Protected objGLSetup As New agri.GL.clsSetup()

    Dim objSysConfig As New agri.PWSystem.clsConfig()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap() 

    Dim strOpCd_Config As String = "PWSYSTEM_CLSCONFIG_CONFIG_GET" 
    Dim strOpCd_Get As String = "GL_SETUP_STMTTEMPL_GET"
    Dim strOpCd_Add As String = "GL_SETUP_STMTTEMPL_ADD"
    Dim strOpCd_Upd As String = "Gl_SETUP_STMTTEMPL_UPD"
    Dim strOpCdDelTempl As String = "GL_SETUP_STMTTEMPL_DEL"
    Dim strOpCdDelAcc As String = "GL_SETUP_TEMPLACC_DEL"
    Dim strOpCdDelFig As String = "GL_SETUP_STMTFIGURE_DEL"
    Dim strOpCd_GetSp As String = "GL_SETUP_POPULATE_STMT_GETSP"
    Dim strOpCd_GetFig As String = "GL_SETUP_POPULATE_STMT_GETFIGURE"
    Dim strOpCd_Calc As String = "GL_SETUP_STMTFIGURE_CALCULATE"
    Dim strOpCd_UpdFigure As String = "GL_SETUP_STMTFIGURE_UPDATE"

    Dim objLangCapDs As New Object
    Dim objDataSet As New DataSet()
    Dim intErrNo As Integer
    Dim strParam As String = ""
    Dim strStmtType As String


    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intGLAR As Integer
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        intGLAR = Session("SS_GLAR")
        strLocType = Session("SS_LOCTYPE")
        lblErrCalc.visible = false
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLBalSheetSetup), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            strStmtType = CInt(objGLSetup.EnumStmtType.BalanceSheet)
            If Not Page.IsPostBack Then
                BindGrid() 
            End If
        End IF

    End Sub

    Sub BindGrid()
        TmplList.DataSource = LoadData()
        TmplList.DataBind()
    End Sub

    Sub DataGrid_ItemCreated(ByVal Sender As Object, ByVal e As DataGridItemEventArgs)
        Dim lbl As Label
        Dim btn As Button
       
        Select Case e.Item.ItemType
           Case ListItemType.Item, ListItemType.AlternatingItem
                lbl = e.Item.FindControl("lblNo")
                lbl.text = e.Item.ItemIndex.ToString + 1

                lbl = e.Item.FindControl("lblRowType")
                If lbl.text.trim = objGLSetup.EnumRowType.Entry then
                    btn = e.item.FindControl("btnAccount")
                    btn.visible = True
                End If
        End Select
    End Sub

    Sub BindRowTypeList(ByRef DropList As DropDownList, Optional ByVal itemtype As String = "")
        DropList.Items.Add(New ListItem(objGLSetup.mtdGetRowType(objGLSetup.EnumRowType.Entry), objGLSetup.EnumRowType.Entry))
        DropList.Items.Add(New ListItem(objGLSetup.mtdGetRowType(objGLSetup.EnumRowType.Header), objGLSetup.EnumRowType.Header))
        DropList.Items.Add(New ListItem(objGLSetup.mtdGetRowType(objGLSetup.EnumRowType.Formula), objGLSetup.EnumRowType.Formula))
        Select Case itemtype.Trim
            Case objGLSetup.EnumRowType.Entry
                DropList.SelectedIndex = 0
            Case objGLSetup.EnumRowType.Header
                DropList.SelectedIndex = 1
            Case objGLSetup.EnumRowType.Formula
                DropList.SelectedIndex = 2
        End Select
    End Sub


    Protected Function LoadData() As DataSet
        strParam = strStmtType & "|" & strAccMonth & "|" & strAccYear

        Try
            intErrNo = objGLSetup.mtdLoadStmtTempl(strOpCd_Get, _
                                                   strOpCd_Config, _
                                                   strOpCd_Add, _
                                                   strCompany, _
                                                   strLocation, _
                                                   strUserId, _
                                                   strParam, _
                                                   objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_BALSHEET_GET&errmesg=" & lblErrMessage.Text & "&redirect=GL/Setup/GL_Setup_BalanceSheet.aspx")
        End Try
        Return objDataSet
    End Function

    Protected Function GetStmtAccYear As String
        Dim dsConfig As Dataset
        Dim strStmtAccYear As String
        Dim intStartMonth As Integer
        Dim intAccMonth As Integer
        Dim intAccYear As Integer
        
        Try
            intErrNo = objSysConfig.mtdGetConfigInfo(strOpCd_Config, _
                                                     strCompany, _
                                                     strLocation, _
                                                     strUserId, _
                                                     DsConfig, "")
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_BALSHEET_GETACCYEAR&errmesg=" & lblErrMessage.Text & "&redirect=GL/Setup/GL_Setup_BalanceSheet.aspx")
        End Try
        intStartMonth = CInt(DsConfig.Tables(0).Rows(0).Item("StartAccMonth").Trim())
        intAccMonth = CInt(strAccMonth)
        intAccYear = CInt(strAccYear)

        If intAccMonth < intStartMonth Then
            strStmtAccYear = CStr(intAccYear - 1)
        Else
            strStmtAccYear = CStr(intAccYear)
        End If
        Return strStmtAccYear
    End Function


    Sub ddlCheckType(ByVal Sender As Object, ByVal E As EventArgs)
        Dim Droplist As DropDownList
        Dim txt As TextBox
        Dim btn As Button
        Dim lbl As Label
        Dim rfv As RequiredFieldValidator

        Droplist = TmplList.Items.Item(CInt(TmplList.EditItemIndex)).FindControl("ddlRowType")
        If Droplist.SelectedItem.Value.Trim = objGLSetup.EnumRowType.Entry Then
            btn = TmplList.Items.Item(CInt(TmplList.EditItemIndex)).FindControl("btnEditAccount")
            btn.visible = False
            txt = TmplList.Items.Item(CInt(TmplList.EditItemIndex)).FindControl("txtRefNo")
            txt.visible = True
            txt = TmplList.Items.Item(CInt(TmplList.EditItemIndex)).FindControl("txtFormula")
            txt.visible = False
            rfv = TmplList.Items.Item(CInt(TmplList.EditItemIndex)).FindControl("rfvRefNo")
            rfv.Visible = True
        ElseIf Droplist.SelectedItem.Value.Trim = objGLSetup.EnumRowType.Header Then
            btn = TmplList.Items.Item(CInt(TmplList.EditItemIndex)).FindControl("btnEditAccount")
            btn.visible = False
            txt = TmplList.Items.Item(CInt(TmplList.EditItemIndex)).FindControl("txtRefNo")
            txt.visible = False
            txt = TmplList.Items.Item(CInt(TmplList.EditItemIndex)).FindControl("txtFormula")
            txt.visible = False 
            rfv = TmplList.Items.Item(CInt(TmplList.EditItemIndex)).FindControl("rfvRefNo")
            rfv.Visible = False
            rfv = TmplList.Items.Item(CInt(TmplList.EditItemIndex)).FindControl("rfvFormula")
            rfv.Visible = False        
        Else 
            btn = TmplList.Items.Item(CInt(TmplList.EditItemIndex)).FindControl("btnEditAccount")
            btn.visible = False
            txt = TmplList.Items.Item(CInt(TmplList.EditItemIndex)).FindControl("txtRefNo")
            txt.visible = True
            txt = TmplList.Items.Item(CInt(TmplList.EditItemIndex)).FindControl("txtFormula")
            txt.visible = True
            rfv = TmplList.Items.Item(CInt(TmplList.EditItemIndex)).FindControl("rfvRefNo")
            rfv.Visible = True
            rfv = TmplList.Items.Item(CInt(TmplList.EditItemIndex)).FindControl("rfvFormula")
            rfv.Visible = True
       End If
    End Sub


    Sub onClick_Account(ByVal Sender As Object, ByVal E As EventArgs)
        Dim lbl As Label
        Dim txt As Textbox
        Dim strRowId As String
        Dim strDesc As String
        Dim strID As String

        strID = CType(sender,Button).ID
        If strID = "btnAccount" Then
            strRowId = CType(sender, Button).CommandArgument.trim
            strDesc = CType(sender,Button).CommandName.trim
        Else
            lbl = tmplList.Items.Item(CInt(TmplList.EditItemIndex)).FindControl("lblRowId")
            strRowId = lbl.text.trim
            txt = tmplList.Items.Item(CInt(TmplList.EditItemIndex)).FindControl("txtDescription")
            strDesc = txt.text.trim
        End If

        If strRowId <> "" Then
            Response.Write("<Script Language=""JavaScript"">pop_Account=window.open(""../../GL/Setup/GL_Setup_TemplAccount.aspx?rowid=" & strRowId & _
                            "&strdesc=" & strDesc & _
                            "&stmttype=" & strStmtType & _
                            """, null ,""'pop_Account',width=800,height=600,top=50,left=150,status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");pop_Account.focus();</Script>")
        End If
    End Sub


    Sub DEDR_Add(ByVal Sender As Object, ByVal E As ImageClickEventArgs)

        Dim dataSet As dataSet = LoadData()
        Dim newRow As DataRow
        Dim Updbutton As LinkButton
        Dim Droplist As DropDownList
        Dim lbl As Label
        Dim txt As TextBox
        Dim btn As Button
        Dim intDispSeq As Integer
        Dim strStmtAccYear As String
        Dim rfv As RequiredFieldValidator

        newRow = dataSet.Tables(0).NewRow()
        newRow.Item("RowId") = ""
        newRow.Item("DispSeq") = 0
        newRow.Item("Description") = ""
        newRow.Item("RowType") = 1
        newRow.Item("RefNo") = ""
        newRow.Item("Formula") = ""
        newRow.Item("StmtType") = strStmtType
        newRow.Item("AccYear") = ""
        newRow.Item("CreateDate") = DateTime.Now()
        newRow.Item("UpdateDate") = DateTime.Now()
        newRow.Item("UpdateId") = ""
        dataSet.Tables(0).Rows.Add(newRow)

        TmplList.DataSource = dataSet
        TmplList.DataBind()

        TmplList.EditItemIndex = TmplList.Items.Count - 1
        TmplList.DataBind()

        lblOper.Text = "add"
        
        If CInt(TmplList.EditItemIndex) > 0 Then
            lbl = TmplList.Items.Item(CInt(TmplList.EditItemIndex) - 1).FindControl("lblDispSeq")
            intDispSeq = CInt(lbl.text) + 5
            lbl = TmplList.Items.Item(CInt(TmplList.EditItemIndex) - 1).FindControl("lblAccYear")
            strStmtAccYear = lbl.text.trim
        Else
            intDispSeq = 1
            strStmtAccYear = GetStmtAccYear()
        End If

        txt = TmplList.Items.Item(CInt(TmplList.EditItemIndex)).FindControl("txtDispSeq")
        txt.Text = intDispSeq
        txt.Readonly  = True

        lbl = TmplList.Items.Item(CInt(TmplList.EditItemIndex)).FindControl("lblAccYear")
        lbl.text = strStmtAccYear

        btn = TmplList.Items.Item(CInt(TmplList.EditItemIndex)).FindControl("btnEditAccount")
        btn.Visible = False

        Droplist = TmplList.Items.Item(CInt(TmplList.EditItemIndex)).FindControl("ddlRowType")
        BindRowTypeList(Droplist)

        txt = TmplList.Items.Item(CInt(TmplList.EditItemIndex)).FindControl("txtRefNo")
        txt.Visible = True

        Updbutton = TmplList.Items.Item(CInt(TmplList.EditItemIndex)).FindControl("Delete")
        Updbutton.Visible = False

        rfv = TmplList.Items.Item(CInt(TmplList.EditItemIndex)).FindControl("rfvRefNo")
        rfv.Visible = True

    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim Droplist As DropDownList
        Dim Label As Label
        Dim txt As TextBox
        Dim Updbutton As LinkButton
        Dim btn As Button
        Dim rfv As RequiredFieldValidator
        Dim strRowType As String

        lblOper.text = "upd"
        
        Label = E.Item.FindControl("lblRowType")
        strRowType = Label.text

        TmplList.EditItemIndex = CInt(E.Item.ItemIndex)
        BindGrid()

        Droplist = TmplList.Items.Item(CInt(TmplList.EditItemIndex)).FindControl("ddlRowType")
        BindRowTypeList(Droplist, strRowType)

        If Droplist.SelectedItem.value = objGLSetup.EnumRowType.Entry Then
            Droplist.visible = True
            btn = TmplList.Items.Item(CInt(TmplList.EditItemIndex)).FindControl("btnEditAccount")
            btn.Visible = True
            txt = TmplList.Items.Item(CInt(TmplList.EditItemIndex)).FindControl("txtRefNo")
            txt.Visible = True
            txt = TmplList.Items.Item(CInt(TmplList.EditItemIndex)).FindControl("txtFormula")
            txt.Visible = False
            rfv = TmplList.Items.Item(CInt(TmplList.EditItemIndex)).FindControl("rfvRefNo")
            rfv.Visible = True
        ElseIf Droplist.SelectedItem.value = objGLSetup.EnumRowType.Header Then
            btn = TmplList.Items.Item(CInt(TmplList.EditItemIndex)).FindControl("btnEditAccount")
            btn.Visible = False
            txt = TmplList.Items.Item(CInt(TmplList.EditItemIndex)).FindControl("txtRefNo")
            txt.Visible = False
            txt = TmplList.Items.Item(CInt(TmplList.EditItemIndex)).FindControl("txtFormula")
            txt.Visible = False
            rfv = TmplList.Items.Item(CInt(TmplList.EditItemIndex)).FindControl("rfvRefNo")
            rfv.Visible = False
            rfv = TmplList.Items.Item(CInt(TmplList.EditItemIndex)).FindControl("rfvFormula")
            rfv.Visible = False
        Else
            btn = TmplList.Items.Item(CInt(TmplList.EditItemIndex)).FindControl("btnEditAccount")
            btn.Visible = False
            txt = TmplList.Items.Item(CInt(TmplList.EditItemIndex)).FindControl("txtRefNo")
            txt.Visible = True
            txt = TmplList.Items.Item(CInt(TmplList.EditItemIndex)).FindControl("txtFormula")
            txt.Visible = True
            rfv = TmplList.Items.Item(CInt(TmplList.EditItemIndex)).FindControl("rfvRefNo")
            rfv.Visible = True
            rfv = TmplList.Items.Item(CInt(TmplList.EditItemIndex)).FindControl("rfvFormula")
            rfv.Visible = True
        End If
        Updbutton = TmplList.Items.Item(CInt(TmplList.EditItemIndex)).FindControl("Delete")
        Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
    End Sub


    Sub DEDR_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOpCode As String
        Dim blnUpdate As Boolean
        Dim blnCheckSeq As Boolean
        Dim blnDupSeq As Boolean = False
        Dim blnCheckRef As Boolean = True
        Dim blnDupRef As Boolean = False

        Dim list As DropDownList
        Dim lbl As Label
        Dim txt As TextBox

        Dim strRowId As String
        Dim strStmtType As String
        Dim strStmtAccYear As String
        Dim strDispSeq As String
        Dim strDesc As String
        Dim strRowType As String
        Dim strRefNo As String
        Dim strFormula As String
        
        lbl = E.Item.FindControl("lblRowId")
        strRowId = lbl.Text.Trim

        lbl = E.Item.FindControl("lblStmtType")
        strStmtType = lbl.Text.Trim

        lbl = E.Item.FindControl("lblAccYear")
        strStmtAccYear = lbl.Text.Trim

        txt = E.Item.FindControl("txtDispSeq")
        strDispSeq = txt.Text.Trim

        txt = E.Item.FindControl("txtDescription")
        strDesc = txt.Text.Trim

        list = E.Item.FindControl("ddlRowType")
        strRowType = list.SelectedItem.Value

        If Trim(strRowType) = objGLSetup.EnumRowType.Entry Then
            txt = E.Item.FindControl("txtRefNo")
            strRefNo = txt.text.trim
            strFormula = ""
        ElseIf Trim(strRowType) = objGLSetup.EnumRowType.Header Then
            strRefNo = ""
            strFormula = ""
            blnCheckRef = False
       Else
            txt = E.Item.FindControl("txtRefNo")
            strRefNo = txt.text.trim
            txt = E.Item.FindControl("txtFormula")
            strFormula = txt.text.trim
            
        End If


        strParam = strRowId & chr(9) & _
                   strDispSeq & chr(9) & _
                   strDesc & chr(9) & _
                   strRowType & chr(9) & _
                   strRefNo & chr(9) & _
                   strFormula & chr(9) & _
                   strStmtType & chr(9) & _
                   strStmtAccYear

        If lblOper.text.trim = "add" Then
            strOpCode = strOpCd_Add
            blnUpdate = False
            blnCheckSeq = False
        Else
            strOpCode = strOpCd_Upd
            blnUpdate = True
            blnCheckSeq = True
        End If

        Try
            intErrNo = objGLSetup.mtdUpdStmtTempl(strOpCode, _
                                                  strOpCd_Get, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  strParam, _
                                                  blnCheckSeq, _
                                                  blnCheckRef, _
                                                  blnDupSeq, _
                                                  blnDupRef, _
                                                  blnUpdate)

        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_BALSHEET_UPD&errmesg=" & lblErrMessage.Text & "&redirect=GL/Setup/GL_Setup_BalanceSheet.aspx")
        End Try

        If blnDupSeq = True Then
            lbl = E.Item.FindControl("lblDupSeq")
            lbl.visible = True
            Exit Sub
        End If

        If blnCheckRef = True And blnDupRef = True Then
            lbl = E.Item.FindControl("lblDupRef")
            lbl.visible = True
            Exit Sub
        End If

        TmplList.EditItemIndex = -1
        BindGrid()
    End Sub


    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)

        Dim lbl As Label
        Dim txt As TextBox
        Dim list As DropDownList
        
        Dim strRowId As String
        
        lbl = E.Item.FindControl("lblRowId")
        strRowId = lbl.Text.Trim

        strParam = "RowId = '" & strRowId & "' "
        Try
            intErrNo = objGLSetup.mtdDelStmtTempl(strOpCdDelTempl, _
                                                  strOpCdDelAcc, _
                                                  strOpCdDelFig, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  strParam)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_BALSHEET_DEL&errmesg=" & lblErrMessage.Text & "&redirect=GL/Setup/GL_Setup_BalanceSheet.aspx")
        End Try

        TmplList.EditItemIndex = -1
        BindGrid()
    End Sub


    Sub DEDR_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        TmplList.EditItemIndex = -1
        BindGrid()
    End Sub


    Sub onclick_GenStmt(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strParam As String
        Dim intErrNo As Integer
        Dim pr_intError As Integer
        Dim strStmtAccYear As String
        Dim strRowType As String
        Dim strEntryRow As String

        strStmtAccYear = GetStmtAccYear()
        strRowType = CInt(objGLSetup.EnumRowType.Entry) & "','" & CInt(objGLSetup.EnumRowType.Formula)
        strEntryRow = CInt(objGLSetup.EnumRowType.Entry)
        
        strParam = strAccMonth & chr(9) & _
                   strAccYear & chr(9) & _
                   strStmtAccYear & chr(9) & _
                   strStmtType & chr(9) & _
                   strRowType & chr(9) & _
                   strEntryRow & chr(9)
        
        Try
            intErrNo = objGLSetup.mtdPopulateStmt(strOpCd_GetSp, _
                                                  strOpCd_GetFig, _
                                                  strOpCd_Calc, _
                                                  strOpCd_UpdFigure, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  strParam, _
                                                  pr_intError)

        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_BALSHEET_POPULATE&errmesg=" & lblErrMessage.Text & "&redirect=GL/Setup/GL_Setup_BalanceSheet.aspx")
        End Try

        If pr_intError = 0 Then
            lblErrCalc.text = "Balance Sheet is created or updated successfully."
            lblErrCalc.visible = true
        Else
            lblErrCalc.text = "Balance Sheet is not created or updated successfully." 
            lblErrCalc.visible = true
        End If

        TmplList.EditItemIndex = -1
        BindGrid()

    End Sub


    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblAccount.text = GetCaption(objLangCap.EnumLangCap.Account)
        TmplList.Columns(6).HeaderText = lblAccount.text
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_BALSHEET_GETLANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_Setup_BalanceSheet.aspx")
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


End Class
