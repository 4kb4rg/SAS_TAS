
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

Public Class GL_Setup_TBM_TM : Inherits Page

    Protected WithEvents EventData As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents StatusList As DropDownList
    Protected WithEvents SortExpression As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblDupMsg As Label
    Protected WithEvents sortcol As Label
    Protected WithEvents srchBlkGrpCode As TextBox
    Protected WithEvents srchDescription As TextBox
    Protected WithEvents srchUpdBy As TextBox
    Protected WithEvents srchStatus As DropDownList
    Protected WithEvents lblBlkGrpCode As Label
    Protected WithEvents lblDescription As Label
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblErrEnter As Label
	
    Protected WithEvents txtID As TextBox 
    Protected WithEvents ddlAccMonth As DropDownList
    Protected WithEvents txtAccYear As DropDownList
    Protected WithEvents tmpAccMonth As DropDownList
    Protected WithEvents tmpAccYear As DropDownList
	Protected WithEvents ddlTBM As DropDownList
	Protected WithEvents ddlTM As DropDownList
	Protected WithEvents isNew As HtmlInputHidden
	

    Protected ObjOK As New agri.GL.ClsTrx
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Dim objDataSet As New Object()
    Dim objLangCapDs As New Object()
    Dim intErrNo As Integer
    Dim strParam As String = ""
	Dim strParamValue  As String = ""
    Dim strValidateCode As String
    Dim strValidateDesc As String

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intGLAR As Integer
    Dim strBlkGrpCodeTag As String
    Dim strDescTag As String
    Dim strTitleTag As String
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType as String
	Dim intLevel As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_SELACCMONTH")
        strAccYear = Session("SS_SELACCYEAR")
        intGLAR = Session("SS_GLAR")
		intLevel = Session("SS_USRLEVEL")
        strLocType = Session("SS_LOCTYPE")
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLBlock), intGLAR) = False) and (intLevel < 2) Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            
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

        dsData = LoadData
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
        lblTracker.Text = "Page " & pageno & " of " & EventData.PageCount
    End Sub

    Sub BindPageList()

        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = EventData.PageCount + 1
            arrDList.Add("Page " & count)
            Count = Count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = EventData.CurrentPageIndex

    End Sub

	Function getCode() As String
        Dim strOpCd_GetID As String = "ALL_ALL_GETID"
        Dim objNewID As New Object
		Dim Prefix As String

		Prefix = "M" & trim(strlocation)
        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_GetID, "PREFIX|LOCID|LEN", Prefix & "||8", objNewID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ALL_ALL_GETID&errmesg=" & Exp.Message )
        End Try

        Return Trim(objNewID.Tables(0).Rows(0).Item("CURID"))
    End Function

    Function getcodetmp() As String
        Dim strOpCd_GetID As String = "ALL_ALL_GETIDTMP"
        Dim objNewID As New Object
		Dim Prefix As String
		
		Prefix = "M" & trim(strlocation)
		
        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_GetID, "PREFIX|LOCID|LEN", Prefix & "||8", objNewID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ALL_ALL_GETID&errmesg=" & Exp.Message )
        End Try

        Return Trim(objNewID.Tables(0).Rows(0).Item("CURID"))
    End Function

    Protected Function LoadData() As DataSet
        strParam = "STRSEARCH"
        strParamValue = "And LocCode='" & strlocation & "'"
        Try
            intErrNo = ObjOK.mtdGetDataCommon("GL_CLSSETUP_SUBBLOCK_TBM_TM_LIST_GET", strParam, strParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_SUBBLOCK_TBM_TM_LIST_GET&errmesg=" & lblErrMessage.Text )
        End Try
        Return objDataSet
    End Function

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).commandargument
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
        SortExpression.text = e.SortExpression.ToString()
        sortcol.text = IIF(sortcol.text = "ASC", "DESC", "ASC")
        EventData.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim txtEditText As TextBox
        Dim ddlList As Dropdownlist
		Dim lblList As Label
        Dim lbUpdbutton As linkbutton
        Dim validateCode As RequiredFieldValidator
        Dim validateDesc As RequiredFieldValidator
		

        blnUpdate.text = True
        EventData.EditItemIndex = CInt(e.Item.ItemIndex)

        BindGrid()
        If CInt(e.Item.ItemIndex) >= EventData.Items.Count Then
            EventData.EditItemIndex = -1
            Exit Sub
        End If
		
        onLoad_BindTBM(EventData.EditItemIndex)
        onLoad_BindTM(EventData.EditItemIndex)
		
		lblList = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblTBM")
		ddlList = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("ddlTBM")
		ddlList.SelectedValue = lblList.Text.Trim()
				
		lblList = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblTM")
		ddlList = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("ddlTM")
		ddlList.SelectedValue = lblList.Text.Trim()
				
        txtEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Status")
		Select Case CInt(txtEditText.text) 
            Case True
                Statuslist.selectedindex = 0
                txtEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("txtID")
                txtEditText.readonly = True
                lbUpdbutton = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Delete")
				
                lbUpdbutton.Text = "Delete"
                lbUpdbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case False
                Statuslist.selectedindex = 1
                txtEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("txtID")
                txtEditText.Enabled = False
                lbUpdbutton = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Delete")
                lbUpdbutton.Text = "Undelete"
        End Select
     isNew.Value = "False"
    End Sub

    Sub DEDR_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
       Dim ID As String
	   Dim list As DropDownList
	   Dim EditText As TextBox
	   
	   Dim strAccMonth As String
	   Dim strAccYear As String
	   Dim strTBM As String
	   Dim strTM As String
	   
	   
	    EditText = E.Item.FindControl("txtID")

        If isNew.Value = "True" Then
            ID = getCode()
        Else
            ID = EditText.Text
        End If
		
		list = E.Item.FindControl("ddlTBM")
		strTBM = list.SelectedItem.Value
		
		list = E.Item.FindControl("ddlTM")
		strTM = list.SelectedItem.Value
		
		list = E.Item.FindControl("ddlAccMonth")
		strAccMonth = list.SelectedItem.Value
		
		EditText = E.Item.FindControl("txtAccYear")
		strAccYear = EditText.Text

		strParam = "ID|AM|AY|TBM|TM|LOC|ST|CD|UD|UI"
        strParamValue = ID & "|" & _
                    strAccMonth & "|" & _
                    strAccYear & "|" & _
                    strTBM & "|" & _
                    strTM & "|" & _
					strlocation & "|1|" & _
                    DateTime.Now() & "|" & _
                    DateTime.Now() & "|" & _
                    strUserId

        Try
            intErrNo = ObjOK.mtdInsertDataCommon("GL_CLSSETUP_SUBBLOCK_TBM_TM_LIST_UPD", strParam, strParamValue)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=HR_HR_STP_JABATAN_GET&errmesg=" & lblErrMessage.Text )
        End Try
		
		EventData.EditItemIndex = -1
        BindGrid()
        isNew.Value = "False"
    End Sub

    Sub DEDR_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        EventData.EditItemIndex = -1
        BindGrid()
		isNew.Value = "False"
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
       
        EventData.EditItemIndex = -1
        BindGrid()
		isNew.Value = "False"
    End Sub

    Sub DEDR_Add(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim dataSet As DataSet = LoadData
        Dim newRow As DataRow
        Dim lbUpdbutton As LinkButton
        Dim validateCode As RequiredFieldValidator
        Dim validateDesc As RequiredFieldValidator
        Dim PageCount As Integer

        blnUpdate.text = False
        newRow = dataSet.Tables(0).NewRow()
        newRow.Item("ID") = getcodetmp()
        newRow.Item("AccMonth") = strAccMonth
        newRow.Item("AccYear") = strAccYear
		newRow.Item("TBMSubblkCode") = ""
		newRow.Item("TMSubblkCode") = ""
		newRow.Item("Status") = "1"
        newRow.Item("CreateDate") = DateTime.now()
        NewRow.Item("UpdateDate") = DateTime.now()
        NewRow.Item("UserName") = ""
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
        onLoad_BindTBM(EventData.EditItemIndex)
        onLoad_BindTM(EventData.EditItemIndex)

        lbUpdbutton = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("Delete")
        lbUpdbutton.visible = False
		isNew.Value = "True"
       
    End Sub

	Function BindTBM() As DataSet
        Dim strOpCd_DivId As String = "GL_CLSSETUP_SUBBLOCK_LIST_GET"
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objJobGroup As New Object

        strParam = "STRSEARCH"
        strParamValue = "And LocCode='" & strlocation & "' And SubBlkType='2' And GrpType='1' And RptBlkType='2' "
		
        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_DivId, strParam, strParamValue, objJobGroup)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_SUBBLOCK_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objJobGroup.Tables(0).Rows.Count - 1
            objJobGroup.Tables(0).Rows(intCnt).Item("SubBlkCode") = Trim(objJobGroup.Tables(0).Rows(intCnt).Item("SubBlkCode"))
            objJobGroup.Tables(0).Rows(intCnt).Item("Description") = Trim(objJobGroup.Tables(0).Rows(intCnt).Item("Description"))
        Next

        dr = objJobGroup.Tables(0).NewRow()
        dr("SubBlkCode") = ""
        dr("Description") = "Select Blok TBM"
        objJobGroup.Tables(0).Rows.InsertAt(dr, 0)

        Return objJobGroup
    End Function
	
	Function BindTM() As DataSet
        Dim strOpCd_DivId As String = "GL_CLSSETUP_SUBBLOCK_LIST_GET"
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objJobGroup As New Object

        strParam = "STRSEARCH"
        strParamValue = "And LocCode='" & strlocation & "' And SubBlkType='1' And GrpType='1' And RptBlkType='1' "
		
        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_DivId, strParam, strParamValue, objJobGroup)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_SUBBLOCK_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objJobGroup.Tables(0).Rows.Count - 1
            objJobGroup.Tables(0).Rows(intCnt).Item("SubBlkCode") = Trim(objJobGroup.Tables(0).Rows(intCnt).Item("SubBlkCode"))
            objJobGroup.Tables(0).Rows(intCnt).Item("Description") = Trim(objJobGroup.Tables(0).Rows(intCnt).Item("Description"))
        Next

        dr = objJobGroup.Tables(0).NewRow()
        dr("SubBlkCode") = ""
        dr("Description") = "Select Blok TBM"
        objJobGroup.Tables(0).Rows.InsertAt(dr, 0)

        Return objJobGroup
    End Function

	Sub onLoad_BindTBM(ByVal index As Integer)
        ddlTBM = EventData.Items.Item(index).FindControl("ddlTBM")
        ddlTBM.DataSource = BindTBM()
        ddlTBM.DataTextField = "Description"
        ddlTBM.DataValueField = "SubBlkCode"
        ddlTBM.DataBind()
    End Sub
	
	Sub onLoad_BindTM(ByVal index As Integer)
        ddlTM = EventData.Items.Item(index).FindControl("ddlTM")
        ddlTM.DataSource = BindTM()
        ddlTM.DataTextField = "Description"
        ddlTM.DataValueField = "SubBlkCode"
        ddlTM.DataBind()
    End Sub
	
End Class
