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
Imports agri.HR.clsSetup
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.GL


Public Class PR_setup_DivAsisten_Estate : Inherits Page

    Protected WithEvents dghist As DataGrid
    Protected WithEvents lbDelete As LinkButton
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
  
    Protected WithEvents srcpmonth As DropDownList
    Protected WithEvents srcpyear As DropDownList
	Protected WithEvents ddlbeforemonth As DropDownList
	Protected WithEvents ddlbeforeyear As DropDownList

    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label
	
	Protected WithEvents id5 As DropDownList
	

    Protected objHRSetup As New agri.HR.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim ObjOK As New agri.GL.ClsTrx
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intHRAR As Long
    Dim ParamNama As String
    Dim ParamValue As String
    Dim objBankDs As New Object()

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intHRAR = Session("SS_HRAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
        
            If Not Page.IsPostBack Then
			    srcpmonth.SelectedIndex = Cint(Session("SS_SELACCMONTH")) - 1
				ddlbeforemonth.SelectedIndex = Cint(Session("SS_SELACCMONTH")) - 1
                BindAccYear(Session("SS_SELACCYEAR"))
                BindGrid()
                BindPageList()
            End If
        End If
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dghist.CurrentPageIndex = 0
        dghist.EditItemIndex = -1
        BindGrid()
        BindPageList()
    End Sub

    Sub BindAccYear(ByVal pv_strAccYear As String)
        Dim strParamName As String
        Dim strParamValue As String
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim intselection As Integer = 0
        Dim objAccYearDs As New Object
        Dim strOpCd As String = "ADMIN_CLSACCPERIOD_CONFIG_GET"

        strParamName = "LOCCODE|SEARCHSTR|SORTEXP"
        strParamValue = strLocation & "||Order By HD.AccYear"

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objAccYearDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & Exp.Message & "&redirect=")
        End Try

        For intCnt = 0 To objAccYearDs.Tables(0).Rows.Count - 1
            objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear") = Trim(objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear"))
            objAccYearDs.Tables(0).Rows(intCnt).Item("UserName") = Trim(objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear"))
            If pv_strAccYear = Trim(objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear")) Then
                intselection = intCnt + 1
            End If
        Next intCnt

        dr = objAccYearDs.Tables(0).NewRow()
        dr("AccYear") = ""
        dr("UserName") = "All"
        objAccYearDs.Tables(0).Rows.InsertAt(dr, 0)

        srcpyear.DataSource = objAccYearDs.Tables(0)
        srcpyear.DataValueField = "AccYear"
        srcpyear.DataTextField = "UserName"
        srcpyear.DataBind()
        srcpyear.SelectedIndex = intselection
		
		ddlbeforeyear.DataSource = objAccYearDs.Tables(0)
        ddlbeforeyear.DataValueField = "AccYear"
        ddlbeforeyear.DataTextField = "UserName"
        ddlbeforeyear.DataBind()
        ddlbeforeyear.SelectedIndex = intselection
    End Sub

    Sub BindGrid()
        Dim intCnt As Integer
        Dim lbButton As LinkButton
        Dim lbl As Label

        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadData()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dghist.PageSize)

        dghist.DataSource = dsData
        If dghist.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dghist.CurrentPageIndex = 0
            Else
                dghist.CurrentPageIndex = PageCount - 1
            End If
        End If

        dghist.DataBind()
        BindPageList()
        PageNo = dghist.CurrentPageIndex + 1
        lblTracker.Text = "Page " & PageNo & " of " & dghist.PageCount

    End Sub

    Sub BindPageList()
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dghist.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dghist.CurrentPageIndex
    End Sub

    Protected Function LoadData() As DataSet
        Dim strOpCd_GET As String = "HR_PR_TRX_MANAGER_ASISTEN"
        Dim strSearch As String
        Dim sortitem As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
		Dim ParamNama As String = ""
        Dim ParamValue As String  = ""

       

        ParamNama = "LOC|DIV|AM|AY"
        ParamValue = strLocation & "||" & srcpmonth.selecteditem.value.trim()  & "|" & srcpyear.selecteditem.value.trim() 

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_GET, ParamNama, ParamValue, objBankDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_BANK_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        Return objBankDs
    End Function

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dghist.CurrentPageIndex = 0
            Case "prev"
                dghist.CurrentPageIndex = _
                    Math.Max(0, dghist.CurrentPageIndex - 1)
            Case "next"
                dghist.CurrentPageIndex = _
                    Math.Min(dghist.PageCount - 1, dghist.CurrentPageIndex + 1)
            Case "last"
                dghist.CurrentPageIndex = dghist.PageCount - 1
        End Select
        lstDropList.SelectedIndex = dghist.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dghist.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dghist.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        dghist.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub dghist_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim Updbutton As LinkButton
        Dim lblTemp As Label
        Dim ddlTemp As DropDownList

        dghist.EditItemIndex = CInt(E.Item.ItemIndex)
		BindGrid()
        If CInt(E.Item.ItemIndex) >= dghist.Items.Count Then
            dghist.EditItemIndex = -1
            Exit Sub
        End If
		
		onLoad_BindMandor(dghist.EditItemIndex)
		lblTemp = dghist.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lb5")
		ddlTemp = dghist.Items.Item(CInt(E.Item.ItemIndex)).FindControl("id5")
		
	
		ddlTemp.selectedvalue = lblTemp.Text
		

    End Sub

    Sub dghist_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        dghist.EditItemIndex = -1
        BindGrid()
    End Sub

	 Sub onLoad_BindMandor(ByVal index As Integer)
        id5 = dghist.Items.Item(index).FindControl("id5")

        Dim strOpCd_DivId As String = "HR_HR_TRX_EMPLOYEE_LIST_GET"
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objJobGroup As New Object
		Dim dv as TextBox = dghist.Items.Item(index).FindControl("id1")

        ParamNama = "LOC|AM|AY|SEARCH|SORT"
        ParamValue = strlocation & "|" & srcpmonth.selecteditem.value.trim()  & "|" & srcpyear.selecteditem.value.trim() & "|AND A.Status='1' AND A.LocCode = '" & strlocation & "' AND C.IDDiv like '" & dv.text.Trim() & "%' AND JOBCODE='MDR'|ORDER BY EmpName"

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_DivId, ParamNama, ParamValue, objJobGroup)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_TRX_EMPLOYEE_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        dr = objJobGroup.Tables(0).NewRow()
        dr("EmpCode") = ""
        dr("_Description") = ""
        objJobGroup.Tables(0).Rows.InsertAt(dr, 0)

        id5.DataSource = objJobGroup.Tables(0)
        id5.DataValueField = "EmpCode"
        id5.DataTextField = "_Description"
        id5.DataBind()
    End Sub

    Sub dghist_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOppCd As String = "HR_PR_TRX_MANAGER_ASISTEN_UPD"
        Dim ParamNama As String
        Dim ParamValue As String
        Dim intErrNo As Integer

        Dim EditText As TextBox
        Dim list As DropDownList

        Dim strBLK As String
        Dim strAM As String
        Dim strAY As String
        Dim strAS As String
        Dim strMN As String
        
		EditText = E.Item.FindControl("id1")
		strBLK = EditText.Text.Trim()
		
		EditText = E.Item.FindControl("id2")
		strAM= EditText.Text.Trim()
		
		EditText = E.Item.FindControl("id3")
		strAY = EditText.Text.Trim()
		
		EditText = E.Item.FindControl("id4")
		strAS = EditText.Text.Trim()
		
		list = E.Item.FindControl("id5")
		strMN = list.SelectedItem.Value.Trim()
		
		
        ParamNama = "LOC|DIV|AM|AY|AS|MN"
        ParamValue = strlocation & "|" & _
		             strBLK & "|" & _
					 strAM & "|" & _
					 strAY & "|" & _
					 strAS & "|" & _
					 strMN 

        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOppCd, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_SUBBLOCK_BY_SUBBLOCK_UPD_HIST&errmesg=" & Exp.Message & "&redirect=")
        End Try
	   dghist.EditItemIndex = -1
       BindGrid()
    End Sub
	
	Sub Copybtn_Click(ByVal Sender As Object, ByVal E As EventArgs)
        Dim strOpCd As String = "HR_PR_TRX_MANAGER_ASISTEN_CPY"
        Dim intErrNo As Integer
        Dim ParamNama As String
        Dim ParamValue As String

        ParamNama = "BAM|BAY|LOC|AM|AY"
        ParamValue =  ddlbeforemonth.SelectedItem.Value.Trim() & "|" & _
                      ddlbeforeyear.SelectedItem.Value.Trim() & "|" & _
					  strlocation & "|" & _
		  			  srcpmonth.SelectedItem.Value.Trim() & "|" & _
                      srcpyear.SelectedItem.Value.Trim() 
		
		Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOpCd, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=" & Exp.Message & "&redirect=")
        End Try

        BindGrid()
    End Sub
	    

End Class
