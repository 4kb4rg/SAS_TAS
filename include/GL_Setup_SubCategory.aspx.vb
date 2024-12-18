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
Imports agri.PWSystem.clsLangCap
Imports agri.GL

Public Class GL_Setup_SubCategory : Inherits Page

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
    Protected WithEvents sortcol As Label
    Protected WithEvents srchJobCode As TextBox
    Protected WithEvents srchDesc As TextBox
    Protected WithEvents srchUpdateBy As TextBox
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblReligion As Label
    Protected WithEvents lblDesc As Label
    Protected WithEvents lblValidate As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents isNew As HtmlInputHidden

    Protected WithEvents ddlCodeGrpJob As DropDownList
    Protected WithEvents lblCodeGrpJob As Label

    Protected objHR As New agri.HR.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim strOppCd_GET As String = "GL_GL_STP_SUBCATEGORY_GET"
    Dim strOppCd_UPD As String = "GL_GL_STP_SUBCATEGORY_UPD"
    Dim objDataSet As New Object()
    Dim objLangCapDs As New Object()
    Dim intErrNo As Integer
    Dim strParam As String = ""
    Dim strValidateCode As String
    Dim strValidateDesc As String

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

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")

        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        intHRAR = Session("SS_ADAR")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
		ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADBudgeting), intHRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")

        Else
            If SortExpression.Text = "" Then
                SortExpression.Text = "IdCat,SubCatID"
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


        SearchStr = " WHERE SubCatID like '%" & srchJobCode.Text & "%' AND SubCatName like '%" & srchDesc.Text & "%' "
        sortitem = "ORDER BY " & SortExpression.Text & " " & sortcol.Text

        ParamNama = "SEARCH|SORT"
        ParamValue = SearchStr & "|" & sortitem


        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOppCd_GET, ParamNama, ParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=HR_HR_STP_GROUPJOB_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
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
        Dim lblTemp As Label
        Dim ddlTemp As DropDownList
        Dim Validator As RequiredFieldValidator

        EventData.EditItemIndex = CInt(E.Item.ItemIndex)

        BindGrid()
        If CInt(E.Item.ItemIndex) >= EventData.Items.Count Then
            EventData.EditItemIndex = -1
            Exit Sub
        End If

        onLoad_Bindkategori(EventData.EditItemIndex)

        EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("JbtCode")
        EditText.ReadOnly = True
        lblTemp = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblCodeGrpJob")
        ddlTemp = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("ddlCodeGrpJob")
        If Not (lblTemp Is Nothing) Then
            ddlTemp.SelectedValue = Trim(lblTemp.Text)
        End If
        Validator = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("validateCode")
        Validator.ErrorMessage = strValidateCode
        Validator = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("validateDesc")
        Validator.ErrorMessage = strValidateDesc
        isNew.Value = "False"
    End Sub

    Sub DEDR_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim list As DropDownList
        Dim JobCode As String
        Dim Description As String

        Dim blnDupKey As Boolean = False
        Dim lblMsg As Label
        Dim StrGrpCode As String

        list = E.Item.FindControl("ddlCodeGrpJob")
        StrGrpCode = list.SelectedItem.Value
        lblMsg = E.Item.FindControl("lblErrCodeGrpJobe")
        If Trim(StrGrpCode) = "" Then
            lblMsg.Visible = True
            Exit Sub
        End If

        EditText = E.Item.FindControl("JbtCode")
        JobCode = EditText.Text.ToUpper.Trim
        EditText = E.Item.FindControl("Description")
        Description = EditText.Text
        
        ParamNama = "SCI|CI|SCN"
        ParamValue = JobCode & "|" & _
                     StrGrpCode & "|" & _
                     Description
                    
        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOppCd_UPD, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=HR_HR_STP_JABATAN_GET&errmesg=" & lblErrMessage.Text & "&redirect=HR/Setup/HR_setup_Religion.aspx")
        End Try

        EventData.EditItemIndex = -1
        BindGrid()
        isNew.Value = "False"
    End Sub

    Sub DEDR_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        isNew.Value = "False"
        EventData.EditItemIndex = -1
        BindGrid()
    End Sub

    
    Sub DEDR_Add(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim dataSet As DataSet = LoadData()
        Dim newRow As DataRow
        Dim Validator As RequiredFieldValidator
        Dim PageCount As Integer

        blnUpdate.Text = False
        newRow = dataSet.Tables(0).NewRow()
        newRow.Item("SubCatID") = ""
        newRow.Item("IdCat") = ""
        newRow.Item("SubCatName") = ""
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

        onLoad_Bindkategori(EventData.EditItemIndex)
      
        Validator = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateCode")
        Validator.ErrorMessage = strValidateCode
        Validator = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateDesc")
        Validator.ErrorMessage = strValidateDesc
        isNew.Value = "True"
    End Sub

    Sub onLoad_Bindkategori(ByVal index As Integer)
        ddlCodeGrpJob = EventData.Items.Item(index).FindControl("ddlCodeGrpJob")

        Dim strOpCd_DivId As String = "GL_GL_STP_CATEGORY_GET_LIST"
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objJobGroup As New Object

        ParamNama = "SEARCH|SORT"
        ParamValue = "|Order by CatId"

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_DivId, ParamNama, ParamValue, objJobGroup)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_GL_STP_CATEGORY_GET_LIST&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objJobGroup.Tables(0).Rows.Count - 1
            objJobGroup.Tables(0).Rows(intCnt).Item("CatId") = Trim(objJobGroup.Tables(0).Rows(intCnt).Item("CatId"))
            objJobGroup.Tables(0).Rows(intCnt).Item("CatName") = Trim(objJobGroup.Tables(0).Rows(intCnt).Item("CatName")) & " (" & Trim(objJobGroup.Tables(0).Rows(intCnt).Item("CatId")) & ")"
        Next

        dr = objJobGroup.Tables(0).NewRow()
        dr("CatId") = ""
        dr("CatName") = "Select Kategori"
        objJobGroup.Tables(0).Rows.InsertAt(dr, 0)

        ddlCodeGrpJob.DataSource = objJobGroup.Tables(0)
        ddlCodeGrpJob.DataValueField = "CatId"
        ddlCodeGrpJob.DataTextField = "CatName"
        ddlCodeGrpJob.DataBind()
    End Sub

End Class
