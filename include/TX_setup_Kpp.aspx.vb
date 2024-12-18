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

Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.HR
Imports agri.GL
Imports agri.PWSystem.clsLangCap

Public Class Tx_setup_Kpp : Inherits Page

    Protected WithEvents EventData As DataGrid
    Protected WithEvents ErrorMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents SQLStatement As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents StatusList As DropDownList
    Protected WithEvents srchStatusList As DropDownList
    Protected WithEvents SortExpression As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents curStatus As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblDupMsg As Label
    Protected WithEvents lblAtt As Label
    Protected WithEvents sortcol As Label
    Protected WithEvents srchDeptCode As TextBox
    Protected WithEvents srchName As TextBox
    Protected WithEvents srchUpdateBy As TextBox
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblDepartment As Label
    Protected WithEvents lblDesc As Label
    Protected WithEvents lblValidate As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents isNew As HtmlInputHidden
 
    Protected objHR As New agri.HR.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim strOppCd_GET As String = "TX_STP_KPP_GET"
    Dim strOppCd_UPD As String = "TX_STP_KPP_UPD"
    Dim strOppCd_DEL As String = "TX_STP_KPP_DEL"

    Dim objDataSet As New Object()
    Dim objLangCapDs As New Object()
    Dim intErrNo As Integer
    Dim strParam As String = ""
    Dim strValidateCode As String
    Dim strValidateDesc As String
    Dim validateBs As String
    Dim validateActDate As String

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim ObjOK As New agri.GL.ClsTrx
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intHRAR As Long
    Dim strLocType As String
    Dim ParamNama As String
    Dim ParamValue As String
	Dim Prefix as String

		
    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        intHRAR = Session("SS_HRAR")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeDetails), intHRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()

            lblErrMessage.Visible = False

            If SortExpression.Text = "" Then
                SortExpression.Text = "KPPCode"
                sortcol.Text = "ASC"
            End If
            If Not Page.IsPostBack Then
                BindGrid()
                BindPageList()
            End If
        End If
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        EventData.CurrentPageIndex = 0
        EventData.EditItemIndex = -1
        BindGrid()
        BindPageList()
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
        BindPageList()
        PageNo = EventData.CurrentPageIndex + 1
        lblTracker.Text = "Page " & PageNo & " of " & EventData.PageCount
    End Sub

    Sub BindPageList()

        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = EventData.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = EventData.CurrentPageIndex

    End Sub

        Protected Function LoadData() As DataSet

        Dim SearchStr As String
        Dim sortitem As String

        SearchStr = ""
        If Not srchDeptCode.Text = "" Then
            SearchStr = SearchStr & " AND A.KppCode like '%" & srchDeptCode.Text & "%' "
        End If
       
	    If Not srchName.Text = "" Then
            SearchStr = SearchStr & " AND A.KPPDescr like '%" & srchDeptCode.Text & "%' "
        End If
       
	      
        If Not srchUpdateBy.Text = "" Then
            SearchStr = SearchStr & " AND B.UserName like '%" & _
                        srchUpdateBy.Text & "%' "
        End If


        sortitem = "ORDER BY " & SortExpression.Text & " " & sortcol.Text
        ParamNama = "SEARCH|SORT"
        ParamValue = SearchStr & "|" & sortitem

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOppCd_GET, ParamNama, ParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=HR_DEPTCODE_GET_LIST&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        Return objDataSet
    End Function

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                EventData.CurrentPageIndex = 0
            Case "prev"
                EventData.CurrentPageIndex = _
                    Math.Max(0, EventData.CurrentPageIndex - 1)
            Case "next"
                EventData.CurrentPageIndex = _
                    Math.Min(EventData.PageCount - 1, EventData.CurrentPageIndex + 1)
            Case "last"
                EventData.CurrentPageIndex = EventData.PageCount - 1
        End Select
        lstDropList.SelectedIndex = EventData.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            EventData.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        EventData.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        sortcol.Text = IIf(sortcol.Text = "ASC", "DESC", "ASC")
        EventData.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim List As DropDownList
        Dim Updbutton As LinkButton
        Dim Validator As RequiredFieldValidator

        blnUpdate.Text = True
        EventData.EditItemIndex = CInt(E.Item.ItemIndex)

        BindGrid()
        If CInt(E.Item.ItemIndex) >= EventData.Items.Count Then
            EventData.EditItemIndex = -1
            Exit Sub
        End If

             Updbutton = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Delete")
             Updbutton.Text = "Delete"
             Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
        
		Validator = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("ValidateCode")
        Validator.ErrorMessage = strValidateCode
        Validator = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateBs")
        Validator.ErrorMessage = validateBs
        Validator = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateDes")
        Validator.ErrorMessage = validateActDate
        isNew.Value = "False"
    End Sub

    Sub DEDR_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        
        Dim KppCode As String
        Dim KPPInit As String
        Dim KPPDescr As String
        Dim KPPAddr As String
		Dim KppPhone As String
        Dim blnDupKey As Boolean = False
        Dim lblMsg As Label
        Dim CreateDate As String

		EditText = E.Item.FindControl("KppCode")
        KppCode = EditText.Text
        EditText = E.Item.FindControl("KPPInit")
        KPPInit = EditText.Text
        EditText = E.Item.FindControl("KPPDescr")
        KPPDescr = EditText.Text
		EditText = E.Item.FindControl("KPPAddr")
        KPPAddr = EditText.Text
		EditText = E.Item.FindControl("KppPhone")
        KppPhone = EditText.Text
		
        
        EditText = E.Item.FindControl("CreateDate")
        CreateDate = EditText.Text
        ParamNama = "KC|KI|KD|KA|KP|CD|UD|UI"
        ParamValue = KppCode & "|" & _
                     KPPInit & "|" & _
                     KPPDescr & "|" & _
                     KPPAddr & "|" & _
					 KppPhone & "|" & _
                     CreateDate & "|" & _
                     DateTime.Now() & "|" & _
                     strUserId


        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOppCd_UPD, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_UPDATE_DEPTCODE&errmesg=" & lblErrMessage.Text & "&redirect=HR/Setup/HR_setup_DeptCode.aspx")
        End Try

        If blnDupKey Then
            lblMsg = E.Item.FindControl("lblDupMsg")
            lblMsg.Visible = True
        Else
            EventData.EditItemIndex = -1
            BindGrid()
        End If
    isNew.Value = "False"
    End Sub

    Sub DEDR_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        EventData.EditItemIndex = -1
        BindGrid()
		isNew.Value = "False"
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)

        Dim EditText As TextBox
        Dim KPPCode As String
       
        EditText = E.Item.FindControl("KPPCode")
        KPPCode = EditText.Text
       
        ParamNama = "KC"
        ParamValue = KPPCode 

        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOppCd_DEL, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_DELETE_DEPTCODE&errmesg=" & lblErrMessage.Text & "&redirect=HR/Setup/HR_setup_DeptCode.aspx")
        End Try

        EventData.EditItemIndex = -1
        BindGrid()

    End Sub

    Sub DEDR_Add(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim dataSet As DataSet = LoadData()
        Dim newRow As DataRow
        Dim Updbutton As LinkButton
        Dim Validator As RequiredFieldValidator
        Dim PageCount As Integer

        

        blnUpdate.Text = False
        newRow = dataSet.Tables(0).NewRow()
        newRow.Item("KppCode") = ""
        newRow.Item("KPPInit") = ""
        newRow.Item("KPPDescr") = ""
		newRow.Item("KPPAddr") = ""
		newRow.Item("KppPhone") = ""
        newRow.Item("CreateDate") = DateTime.Now()
        newRow.Item("UpdateDate") = DateTime.Now()
        newRow.Item("UserName") = ""
        dataSet.Tables(0).Rows.Add(newRow)

        EventData.DataSource = dataSet
        PageCount = objGlobal.mtdGetPageCount(dataSet.Tables(0).Rows.Count, EventData.PageSize)
        If EventData.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                EventData.CurrentPageIndex = 0
            Else
                EventData.CurrentPageIndex = PageCount - 1
            End If
        End If
        EventData.DataBind()
        BindPageList()

        EventData.CurrentPageIndex = EventData.PageCount - 1
        lblTracker.Text = "Page " & (EventData.CurrentPageIndex + 1) & " of " & EventData.PageCount
        lstDropList.SelectedIndex = EventData.CurrentPageIndex
        EventData.DataBind()
        EventData.EditItemIndex = EventData.Items.Count - 1
        EventData.DataBind()

        Updbutton = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("Delete")
        Updbutton.Visible = False

        Validator = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("ValidateCode")
        Validator.ErrorMessage = strValidateCode
        Validator = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateBs")
        Validator.ErrorMessage = validateBs
        Validator = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateDes")
        Validator.ErrorMessage = validateActDate
       isNew.Value = "True"
    End Sub

    Sub onload_GetLangCap()
        strValidateCode = "Please Kpp code"
		validateBs="Please insert Kpp code Alias"
		validateActDate="Please unit kerja "
    End Sub

   End Class
