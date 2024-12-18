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

Public Class PR_setup_AktivitiList_Estate : Inherits Page

    Protected WithEvents dgaktiviti As DataGrid
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
    Protected WithEvents srchcatid As DropDownList
    Protected WithEvents srchsubcatid As DropDownList
	Protected WithEvents srchAcc  As TextBox

    Protected WithEvents lblTitle As Label
    Protected WithEvents lblReligion As Label
    Protected WithEvents lblDesc As Label
    Protected WithEvents lblValidate As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents isNew As HtmlInputHidden

    Protected WithEvents ddlcatid As DropDownList
    Protected WithEvents lblcatid As Label

    Protected objHR As New agri.HR.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objDataSet As New Object()
    Dim objLangCapDs As New Object()
    Dim intErrNo As Integer
    Dim strParam As String = ""
    Dim Prefix As String = "JOB"
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
     Dim intLevel As Integer
	Dim intPRAR As Long
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
        intPRAR = Session("SS_PRAR")
		intLevel = Session("SS_USRLEVEL")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
          ElseIf (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRPaySetup), intPRAR) = False) and (intLevel < 2) Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.Text = "" Then
                SortExpression.Text = "JobCode"
                sortcol.Text = "ASC"
            End If
            If Not Page.IsPostBack Then
                BindBkCategory()
				BindBKSubKategory("", "")
                BindSearchList()
				If Session("Aktiviti") <> "" Then
                    Dim prevset As String
                    Dim ary As Array
                    prevset = Session("Aktiviti")
                    ary = Split(prevset, "|")
					
					srchJobCode.Text = ary(0)
				    srchcatid.SelectedValue = ary(1)
   					srchsubcatid.SelectedValue = ary(2)
                    srchDesc.Text = ary(3)
                End If
                BindGrid()
                BindPageList()

            End If
        End If
    End Sub

    Sub BindBkCategory()
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_BK_CATEGORY_GET_LIST"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpDivDs As New Object()
        Dim dr As DataRow


        strParamName = "SEARCH|SORT"
        strParamValue = "|Order By CatName"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpDivDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BK_CATEGORY_GET_LIST&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objEmpDivDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpDivDs.Tables(0).Rows.Count - 1
                objEmpDivDs.Tables(0).Rows(intCnt).Item("CatID") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("CatID"))
                objEmpDivDs.Tables(0).Rows(intCnt).Item("CatName") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("CatID")) & " (" & Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("CatName")) & ")"
            Next
        End If

        dr = objEmpDivDs.Tables(0).NewRow()
        dr("CatID") = ""
        dr("CatName") = "Pilih Kategori"
        objEmpDivDs.Tables(0).Rows.InsertAt(dr, 0)

        srchcatid.DataSource = objEmpDivDs.Tables(0)
        srchcatid.DataTextField = "CatName"
        srchcatid.DataValueField = "CatID"
        srchcatid.DataBind()
    End Sub

    Sub BindBKSubKategory(ByVal id As String, ByVal scat As String)
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_BK_SUBCATEGORY_GET_LIST"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpDivDs As New Object()
        Dim dr As DataRow
		Dim intSelectedIndex As Integer


        strParamName = "SEARCH|SORT"
        strParamValue = "AND idCat like '" & id & "%' |Order by SubCatName"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpDivDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BK_SUBCATEGORY_GET_LIST&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objEmpDivDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpDivDs.Tables(0).Rows.Count - 1
                objEmpDivDs.Tables(0).Rows(intCnt).Item("SubCatID") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("SubCatID"))
                objEmpDivDs.Tables(0).Rows(intCnt).Item("SubCatName") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("SubCatID")) & " (" & Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("SubCatName")) & ")"
				If objEmpDivDs.Tables(0).Rows(intCnt).Item("SubCatID") = Trim(scat) Then
                    intSelectedIndex = intCnt + 1
                End If
            Next
        End If

        dr = objEmpDivDs.Tables(0).NewRow()
        dr("SubCatID") = ""
        dr("SubCatName") = "Pilih Sub Kategori"
        objEmpDivDs.Tables(0).Rows.InsertAt(dr, 0)

        srchsubcatid.DataSource = objEmpDivDs.Tables(0)
        srchsubcatid.DataTextField = "SubCatName"
        srchsubcatid.DataValueField = "SubCatID"
        srchsubcatid.DataBind()
		srchsubcatid.SelectedIndex = intSelectedIndex

    End Sub


    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgaktiviti.CurrentPageIndex = 0
        dgaktiviti.EditItemIndex = -1
        BindGrid()
        BindPageList()
    End Sub

    Sub BindGrid()

        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadData()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgaktiviti.PageSize)

        dgaktiviti.DataSource = dsData
        If dgaktiviti.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgaktiviti.CurrentPageIndex = 0
            Else
                dgaktiviti.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgaktiviti.DataBind()
        BindPageList()
        PageNo = dgaktiviti.CurrentPageIndex + 1
        lblTracker.Text = "Page " & PageNo & " of " & dgaktiviti.PageCount
    End Sub

    Sub BindPageList()

        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgaktiviti.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgaktiviti.CurrentPageIndex

    End Sub

    Sub BindStatusList(ByVal index As Integer)

        StatusList = dgaktiviti.Items.Item(index).FindControl("StatusList")
        StatusList.Items.Add(New ListItem(objHR.mtdGetReligionStatus(objHR.EnumReligionStatus.Active), objHR.EnumReligionStatus.Active))
        StatusList.Items.Add(New ListItem(objHR.mtdGetReligionStatus(objHR.EnumReligionStatus.Deleted), objHR.EnumReligionStatus.Deleted))

    End Sub

    Sub BindSearchList()

        srchStatusList.Items.Add(New ListItem(objHR.mtdGetReligionStatus(objHR.EnumReligionStatus.All), objHR.EnumReligionStatus.All))
        srchStatusList.Items.Add(New ListItem(objHR.mtdGetReligionStatus(objHR.EnumReligionStatus.Active), objHR.EnumReligionStatus.Active))
        srchStatusList.Items.Add(New ListItem(objHR.mtdGetReligionStatus(objHR.EnumReligionStatus.Deleted), objHR.EnumReligionStatus.Deleted))
        srchStatusList.SelectedIndex = 1

    End Sub

    Protected Function LoadData() As DataSet
        Dim strOpCd_GET As String = "PR_PR_STP_BK_JOB_GET_NEW"
        Dim SearchStr As String
		Dim SearchStr2 As String = ""
        Dim sortitem As String


        SearchStr = " AND A.LocCode='" & strLocation & "' AND A.Status like '" & IIf(Not srchStatusList.SelectedItem.Value = "0", _
                       srchStatusList.SelectedItem.Value, "%") & "' "

        If Not srchJobCode.Text = "" Then
            SearchStr = SearchStr & " AND JobCode like '%" & srchJobCode.Text & "%' "
        End If

        If Not srchcatid.SelectedItem.Value.Trim = "" Then
            SearchStr = SearchStr & " AND A.CatId like '%" & srchcatid.SelectedItem.Value.Trim & "%' "
        End If

        If Not srchsubcatid.Text.Trim = "" Then
            SearchStr = SearchStr & " AND A.SubCatID like '%" & srchsubcatid.SelectedItem.Value.Trim & "%' "
        End If

        If Not srchDesc.Text = "" Then
            SearchStr = SearchStr & " AND A.Description like '%" & _
                        srchDesc.Text & "%' "
        End If
		
        If Not srchUpdateBy.Text = "" Then
            SearchStr = SearchStr & " AND B.UserName like '" & _
                        srchUpdateBy.Text & "%' "
        End If
		
		If Not trim(srchAcc.Text) = "" Then
			SearchStr2 = SearchStr2 & " AND (('" & Session("SS_SELACCYEAR") & "'+'01' >= right(rtrim(periodestart),4)+left(rtrim(periodestart),2)) and " & _
                                         " ('" & Session("SS_SELACCYEAR") & "'+'01' <= right(rtrim(periodeend),4)+left(rtrim(periodeend),2))) "

		End If
        sortitem = "ORDER BY " & SortExpression.Text & " " & sortcol.Text

        ParamNama = "SEARCH|SORT|ACC|SEARCH2"
        ParamValue = SearchStr & "|" & sortitem & "|" & trim(srchAcc.Text) & "|" & SearchStr2 


        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_GET, ParamNama, ParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BK_JOB_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        Return objDataSet

    End Function

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgaktiviti.CurrentPageIndex = 0
            Case "prev"
                dgaktiviti.CurrentPageIndex = _
                    Math.Max(0, dgaktiviti.CurrentPageIndex - 1)
            Case "next"
                dgaktiviti.CurrentPageIndex = _
                    Math.Min(dgaktiviti.PageCount - 1, dgaktiviti.CurrentPageIndex + 1)
            Case "last"
                dgaktiviti.CurrentPageIndex = dgaktiviti.PageCount - 1
        End Select
        lstDropList.SelectedIndex = dgaktiviti.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgaktiviti.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgaktiviti.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        sortcol.Text = IIf(sortcol.Text = "ASC", "DESC", "ASC")
        dgaktiviti.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub DEDR_Add(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_setup_AktivitiDet_Estate.aspx")
    End Sub

    Sub srchcatid_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        BindBKSubKategory(srchcatid.SelectedItem.Value.Trim,"")
    End Sub

	Sub JobLink_Click(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        If Not E.CommandName.ToString = "Sort" And Not E.CommandName.ToString = "Delete" Then
            Dim intIndex As Integer = E.Item.ItemIndex
            Dim hid As HiddenField = dgaktiviti.Items.Item(intIndex).FindControl("hidjob")

            Session("Aktiviti") = srchJobCode.Text.Trim() & "|" & _
								  srchcatid.SelectedItem.Value.Trim() & "|" & _
   								  srchsubcatid.SelectedItem.Value.Trim & "|" & _
                                  srchDesc.Text.Trim() 

            Response.Redirect("PR_setup_AktivitiDet_Estate.aspx?JobCode=" & hid.Value.Trim)

        End If
    End Sub

End Class
