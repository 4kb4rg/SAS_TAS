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


Public Class IN_setup_UserApprovalDet : Inherits Page

    Protected WithEvents EventData As DataGrid


    Protected WithEvents DDLUser As DropDownList
    Protected WithEvents DDLItemGroup As DropDownList

    Protected WithEvents StatusList As DropDownList
    Protected WithEvents SortExpression As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblDupMsg As Label
    Protected WithEvents sortcol As Label



    'Protected WithEvents srchAccType As TextBox
    'Protected WithEvents srchVehTypeCode As TextBox
    'Protected WithEvents srchDescription As TextBox
    'Protected WithEvents srctoLocCode As TextBox
    'Protected WithEvents lblVehTypeCode As Label
    'Protected WithEvents lblDescription As Label
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblCode As Label

    Protected WithEvents btnDelete As ImageButton
    Protected WithEvents btnUnDelete As ImageButton
    Protected WithEvents btnSave As ImageButton
    Protected WithEvents btnAdd As ImageButton

    Protected objGLSetup As New agri.GL.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Dim strOppCd_ADD As String = "PU_CLSSETUP_SUPPLIER_USER_ADD"
    Dim strOppCd_DEL As String = "PU_CLSSETUP_SUPPLIER_USER_DEL"


    Dim ObjOk As New agri.GL.ClsTrx()
    Dim objDataSet As New Object()
    Dim objLangCapDs As New Object()
    Dim intErrNo As Integer
    Dim strParam As String = ""
    Dim strValidateCode As String
    Dim strValidateDesc As String
    Dim strValidateToLoc As String
    Dim strValidateAccGrp As String

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intGLAR As Integer
    Dim strVehTypeCodeTag As String
    Dim strDescTag As String
    Dim strTitleTag As String
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String
    Dim strSelectedID As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        intGLAR = Session("SS_GLAR")

        strLocType = Session("SS_LOCTYPE")
        strSelectedID = Trim(IIf(Request.QueryString("SuppCode") <> "", Request.QueryString("SuppCode"), Request.Form("SuppCode")))

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLEntrySetup), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            If SortExpression.Text = "" Then
                SortExpression.Text = "TypeCode"
                sortcol.Text = "ASC"
            End If
            If Not Page.IsPostBack Then
                BindUserlist(strSelectedID)

                BindGrid()
            End If
        End If
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        EventData.CurrentPageIndex = 0
        EventData.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub BindGrid()
        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadData()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, EventData.PageSize)

        EventData.DataSource = dsData
        If EventData.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                EventData.CurrentPageIndex = 0
            Else
                EventData.CurrentPageIndex = PageCount - 1
            End If
        End If

        EventData.DataBind()
        PageNo = EventData.CurrentPageIndex + 1

        'If dsData.Tables(0).Rows.Count > 0 Then
        '    DDLUser.Enabled = False
        '    If Trim(dsData.Tables(0).Rows(0).Item("Status")) = "1" Then
        '        btnDelete.Visible = True
        '        btnUnDelete.Visible = False
        '        EventData.Enabled = True
        '        btnAdd.Enabled = True
        '    Else
        '        btnDelete.Visible = False
        '        btnUnDelete.Visible = True
        '        EventData.Enabled = False
        '        btnAdd.Enabled = False
        '    End If
        'Else
        '    DDLUser.Enabled = True
        'End If
        BindItemGroup()
    End Sub

    Sub BindUserlist(ByVal pv_User As String)
        Dim dr As DataRow
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim intSelectedIndex As Integer
        Dim objUser As New Object
        Dim strOpCd As String = "PU_CLSSETUP_SUPPLIER_USER_ADMIN_GET"

        strParamName = "SEARCHSTR"

        If pv_User <> "" Then
            strParamValue = "Where UserID='" & pv_User & "' or'" & pv_User & "'='' ORDER By UserName ASC "
        Else
            strParamValue = "Where UserID NOT IN (SELECT UserID FROM IN_APPROVAL)  ORDER By UserName ASC "
        End If

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objUser)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & Exp.Message & "&redirect=")
        End Try



        For intCnt = 0 To objUser.Tables(0).Rows.Count - 1
            objUser.Tables(0).Rows(intCnt).Item("UserID") = Trim(objUser.Tables(0).Rows(intCnt).Item("UserID"))
            objUser.Tables(0).Rows(intCnt).Item("UserName") = Trim(objUser.Tables(0).Rows(intCnt).Item("UserName")) & "," & Trim(objUser.Tables(0).Rows(intCnt).Item("UserID")) & " (" & Trim(objUser.Tables(0).Rows(intCnt).Item("UsrLevel")) & ")"
            If objUser.Tables(0).Rows(intCnt).Item("UserID") = pv_User Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

         
        dr = objUser.Tables(0).NewRow()
        dr("UserID") = ""
        dr("UserName") = "Please Select User Name"
        objUser.Tables(0).Rows.InsertAt(dr, 0)
         
         
        DDLUser.DataSource = objUser.Tables(0)
        DDLUser.DataValueField = "UserID"
        DDLUser.DataTextField = "UserName"
        DDLUser.DataBind()
        DDLUser.SelectedIndex = intSelectedIndex - 1
    End Sub

    Sub UserIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then

            BindGrid()
        End If
    End Sub

    Sub BindItemGroup()
        Dim dr As DataRow
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim intSelectedIndex As Integer
        Dim objItemGR As New Object
        Dim strOpCd As String = "HR_CLSSETUP_DEPTCODE_LIST_GET"

        strParamName = "SEARCHSTR|SORTEXP"
        strParamValue = " AND DEPT.DeptCode NOT IN (SELECT DeptCode From IN_APPROVALLN Where USERID='" & Trim(DDLUser.SelectedItem.Value) & "') " & "|" & ""

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objItemGR)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & Exp.Message & "&redirect=")
        End Try

        For intCnt = 0 To objItemGR.Tables(0).Rows.Count - 1
            objItemGR.Tables(0).Rows(intCnt).Item("DeptCode") = Trim(objItemGR.Tables(0).Rows(intCnt).Item("DeptCode"))
            objItemGR.Tables(0).Rows(intCnt).Item("CodeDescr") = Trim(objItemGR.Tables(0).Rows(intCnt).Item("CodeDescr"))
           
        Next intCnt

        dr = objItemGR.Tables(0).NewRow()
        dr("DeptCode") = ""
        dr("CodeDescr") = "Please Select Department"
        objItemGR.Tables(0).Rows.InsertAt(dr, 0)

        DDLItemGroup.DataSource = objItemGR.Tables(0)
        DDLItemGroup.DataValueField = "DeptCode"
        DDLItemGroup.DataTextField = "CodeDescr"
        DDLItemGroup.DataBind()
        DDLItemGroup.SelectedIndex = intSelectedIndex - 1
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblTitle.Text = "SETUP APPROVAL LEVEL"
        'lblVehTypeCode.Text = "Acc. Group Code"
        'lblDescription.Text = "Description"
        'strValidateCode = "Please enter " & lblVehTypeCode.Text & "."
        'strValidateDesc = "Please enter " & lblDescription.Text & "."

        'EventData.Columns(0).HeaderText = lblVehTypeCode.Text
        'EventData.Columns(1).HeaderText = lblDescription.Text

        'strVehTypeCodeTag = lblVehTypeCode.Text
        'strDescTag = lblDescription.Text
        strTitleTag = lblTitle.Text
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=GL/Setup/GL_Setup_VehicleType.aspx")
        End Try
    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

        For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If strLocType = objAdminLoc.EnumLocType.Mill Then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                Else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                End If
                Exit For
            End If
        Next
    End Function

    Protected Function LoadData() As DataSet
        Dim strOppCd_GET As String = "IN_APPROVAL_LEVEL_UPD"
        Dim strParam As String
        Dim SearchStr As String
        Dim objTransDs As New Object()
        Dim ssQLKriteria As String = ""


        strParam = "SEARCHSTR"
        SearchStr = "SELECT ln.USERID,ln.DeptCode,D.Description,ln.UpDateDate,ln.UpdateID " & _
                    "FROM IN_APPROVALLN ln inner join HR_DEPTCODE D ON D.DeptCode=ln.DeptCode " & _
                    "Where  ln.UserID='" & Trim(DDLUser.SelectedItem.Value) & "'  "


        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOppCd_GET, strParam, SearchStr, objTransDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_STOCKTRANSFERDIV_DISPLAYLINE_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        Return objTransDs
    End Function

    Sub lClear()
        DDLItemGroup.Enabled = True
        DDLUser.Enabled = True

        BindUserlist("")
        BindGrid()
    End Sub

    Sub UserMsgBox(ByVal F As Object, ByVal sMsg As String)
        Dim sb As New StringBuilder()
        Dim oFormObject As System.Web.UI.Control = Nothing
        Try
            sMsg = sMsg.Replace("'", "\'")
            sMsg = sMsg.Replace(Chr(34), "\" & Chr(34))
            sMsg = sMsg.Replace(vbCrLf, "\n")
            sMsg = "<script language='javascript'>alert('" & sMsg & "');</script>"
            sb = New StringBuilder()
            sb.Append(sMsg)
            For Each oFormObject In F.Controls
                If TypeOf oFormObject Is HtmlForm Then
                    Exit For
                End If
            Next
            oFormObject.Controls.AddAt(oFormObject.Controls.Count, New LiteralControl(sb.ToString()))
        Catch ex As Exception

        End Try
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        sortcol.Text = IIf(sortcol.Text = "ASC", "DESC", "ASC")
        BindGrid()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOppCd_GET As String = "IN_APPROVAL_LEVEL_UPD"
        Dim strParam As String
        Dim SearchStr As String
        Dim ParamValue As String
        Dim objTransDs As New Object()
        Dim ssQLKriteria As String = ""
        Dim strTypeCode As String = ""

        Dim txtEditText As Label 
 
        Dim blnDupKey As Boolean = False

        txtEditText = E.Item.FindControl("lblProdTypeCode")
        strTypeCode = txtEditText.Text.Trim
          
        strParam = "SEARCHSTR"
        ParamValue = "DELETE IN_APPROVALLN Where  UserID='" & Trim(DDLUser.SelectedItem.Value) & "' AND LocCode='" & strLocation & "' AND DeptCode='" & strTypeCode & "'"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOppCd_GET, strParam, ParamValue, objTransDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_STOCKTRANSFERDIV_DISPLAYLINE_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try
        EventData.EditItemIndex = -1
        BindGrid()


    End Sub

    Sub AddBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "IN_APPROVAL_HEADER_LEVEL_INSERT"
        Dim strOpCd_Det As String = "IN_APPROVAL_DETAIL_LEVEL_INSERT"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objEmp As New Object()

        'If DDLUser.SelectedIndex = 0 Then
        '    UserMsgBox(Me, "Please User Name")
        '    Exit Sub
        'End If

        If DDLItemGroup.SelectedIndex = 0 Then
            UserMsgBox(Me, "Please Input Item Group")
            Exit Sub
        End If


        strParamName = "LOC|UI|UP"
        strParamValue = strLocation & "|" & Trim(DDLUser.SelectedItem.Value) & "|" & strUserId

        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOpCd_Upd, strParamName, strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        strParamName = "LOC|UI|DPCODE|UP"
        strParamValue = strLocation & "|" & Trim(DDLUser.SelectedItem.Value) & "|" & Trim(DDLItemGroup.SelectedItem.Value) & "|" & strUserId

        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOpCd_Det, strParamName, strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
         
        BindGrid()

    End Sub

    Sub AddBtnALL_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "IN_APPROVAL_DETAIL_LEVEL_INSERT_ALL"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objEmp As New Object()

        'If DDLUser.SelectedIndex = 0 Then
        '    UserMsgBox(Me, "Please User Name")
        '    Exit Sub
        'End If

        strParamName = "LOC|UI|UPID"
        strParamValue = strLocation & "|" & Trim(DDLUser.SelectedItem.Value) & "|" & strUserId

        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOpCd_Upd, strParamName, strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        BindGrid()

    End Sub
    Sub btnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "IN_APPROVAL_DETAIL_LEVEL_DEL"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objEmp As New Object()

        'If DDLUser.Text 0 Then
        '    UserMsgBox(Me, "Please User Name")
        '    Exit Sub
        'End If

        strParamName = "UI|LOC"
        strParamValue = Trim(DDLUser.SelectedItem.Value) & "|" & strLocation

        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOpCd_Upd, strParamName, strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        UserMsgBox(Me, "Delete Sucsess")
        BindGrid()

    End Sub
     

End Class
