Imports System
Imports System.Data
Imports System.Collections 
Imports System.IO
Imports System.web.UI
Imports System.web.UI.webControls
Imports System.web.UI.Page
Imports System.web.UI.Control
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction

Imports agri.HR
Imports agri.GlobalHdl.clsGlobalHdl

Public Class PR_KTNList : Inherits Page

    Protected WithEvents dgEmpList As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents ddlStatus As DropDownList

    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents NewEmpBtn As ImageButton
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblCurrentIndex As Label
    Protected WithEvents lblPageCount As Label

    Protected WithEvents txtBKM As TextBox
    Protected WithEvents txtEmpName As TextBox
    Protected WithEvents ddlEmpDiv As DropDownList
    Protected WithEvents ddlEmpMonth As DropDownList
    Protected WithEvents ddlyear As DropDownList
    Protected WithEvents ddlEmpBKMTy As DropDownList
    Protected WithEvents ddlEmpBKMSub As DropDownList
    Protected WithEvents ddlBkmtype As DropDownList
	Protected WithEvents ddlEmpBKMAkt As DropDownList

    Dim ObjOk As New agri.GL.ClsTrx
    Protected objHR As New agri.HR.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intHRAR As Long
    Dim blnCanDelete As Boolean = False
    Dim objEmpDs As New Object()

    Dim objEmpDivDs As New Object()
    Dim cnt As Double

     Public Sub UserMsgBox(ByVal F As Object, ByVal sMsg As String)
        Dim sb As New StringBuilder()
        Dim oFormObject As System.web.UI.Control = Nothing
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
	
    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intHRAR = Session("SS_PRAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxWorkPerformance), intHRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.Text = "" Then
                SortExpression.Text = "BKMDate,divisi"
            End If

            If Not Page.IsPostBack Then
                BindAccYear(Session("SS_SELACCYEAR"))
                ddlEmpMonth.SelectedIndex = Cint(Session("SS_SELACCMONTH")) - 1
                BindCategory()
                BindSubCategory("", "")
				BindAktiviti("","","")
                BindEmpDiv()
				
                If Session("KTN") <> "" Then
                    Dim prevset As String
                    Dim ary As Array
                    prevset = Session("KTN")
                    ary = Split(prevset, "|")
                    ddlEmpMonth.SelectedValue = ary(0)
                    BindAccYear(ary(1))
                    txtEmpName.Text = ary(2)
                    ddlEmpDiv.SelectedValue = ary(3)
                    ddlEmpBKMTy.SelectedValue = ary(4)
                    BindSubCategory(ary(4), ary(5))
                    lblCurrentIndex.Text = ary(6)
					BindAktiviti(ary(4), ary(5), ary(7))
					ddlBkmtype.SelectedValue = ary(8)
                End If
                BindGrid()
            End If
        End If
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblCurrentIndex.Text = 0
        dgEmpList.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub BindCategory()
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_BK_CATEGORY_GET_LIST"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpDivDs As New Object()
        Dim dr As DataRow


        strParamName = "SEARCH|SORT"
        strParamValue = "WHERE (CatID Not IN ('AD','TR')) |"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpDivDs)
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
        dr("CatName") = "All"
        objEmpDivDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlEmpBKMTy.DataSource = objEmpDivDs.Tables(0)
        ddlEmpBKMTy.DataTextField = "CatName"
        ddlEmpBKMTy.DataValueField = "CatID"
        ddlEmpBKMTy.DataBind()
    End Sub

    Sub BindSubCategory(ByVal cat As String, ByVal scat As String)
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_BK_SUBCATEGORY_GET_LIST"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpDivDs As New Object()
        Dim dr As DataRow
        Dim intSelectedIndex As Integer


        strParamName = "SEARCH|SORT"
        strParamValue = "AND idCat like '%" & cat & "%'|"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpDivDs)
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
        dr("SubCatName") = "All"
        objEmpDivDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlEmpBKMSub.DataSource = objEmpDivDs.Tables(0)
        ddlEmpBKMSub.DataTextField = "SubCatName"
        ddlEmpBKMSub.DataValueField = "SubCatID"
        ddlEmpBKMSub.DataBind()
        ddlEmpBKMSub.SelectedIndex = intSelectedIndex
    End Sub

	Sub BindAktiviti(ByVal cat As String, ByVal scat As String, Byval s as String)
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_BK_JOB_GET_LIST"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpDivDs As New Object()
        Dim dr As DataRow
		Dim intSelectedIndex As Integer = 0
      
  	    strParamName = "SEARCH|SORT"
        strParamValue = "WHERE Catid like '" & cat & "%' and subcatid like '" & scat & "%' and loccode = '" & strlocation & "' and status='1'|Order by Description"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpDivDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BK_JOB_GET_LIST&errmesg=" & Exp.Message & "&redirect=")
        End Try

       
		
		If objEmpDivDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpDivDs.Tables(0).Rows.Count - 1
                If Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("JobCode")) = Trim(s) Then
                    intSelectedIndex = intCnt + 1
                End If
            Next
        End If
		
		 dr = objEmpDivDs.Tables(0).NewRow()
        dr("JobCode") = ""
        dr("Description") = "All"
        objEmpDivDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlEmpBKMAkt.DataSource = objEmpDivDs.Tables(0)
        ddlEmpBKMAkt.DataTextField = "Description"
        ddlEmpBKMAkt.DataValueField = "JobCode"
        ddlEmpBKMAkt.DataBind()
        ddlEmpBKMAkt.SelectedIndex = intSelectedIndex
    End Sub
	
    Sub BindAccYear(ByVal pv_strAccYear As String)
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim intSelectedIndex As Integer
        Dim objAccYearDs As New Object
        Dim strOpCd As String = "ADMIN_CLSACCPERIOD_CONFIG_GET"

        strParamName = "LOCCODE|SEARCHSTR|SORTEXP"
        strParamValue = strLocation & "||Order By HD.AccYear"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objAccYearDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & Exp.Message & "&redirect=")
        End Try

        For intCnt = 0 To objAccYearDs.Tables(0).Rows.Count - 1
            objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear") = Trim(objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear"))
            objAccYearDs.Tables(0).Rows(intCnt).Item("UserName") = Trim(objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear"))
            If objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear") = pv_strAccYear Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        ddlyear.DataSource = objAccYearDs.Tables(0)
        ddlyear.DataValueField = "AccYear"
        ddlyear.DataTextField = "UserName"
        ddlyear.DataBind()
        ddlyear.SelectedIndex = intSelectedIndex - 1
    End Sub

    Sub BindEmpDiv()
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_DIVISICODE_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow


        strParamName = "SEARCH|SORT"
        strParamValue = "AND A.LocCode='" & strLocation & "' AND A.Status='1'|ORDER By BlkGrpCode"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpDivDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_STP_DIVISI_LIST_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objEmpDivDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpDivDs.Tables(0).Rows.Count - 1
                objEmpDivDs.Tables(0).Rows(intCnt).Item("BlkGrpCode") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("BlkGrpCode"))
                objEmpDivDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("Description"))
            Next
        End If

        dr = objEmpDivDs.Tables(0).NewRow()
        dr("BlkGrpCode") = ""
        dr("Description") = "All"
        objEmpDivDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlEmpDiv.DataSource = objEmpDivDs.Tables(0)
        ddlEmpDiv.DataTextField = "Description"
        ddlEmpDiv.DataValueField = "BlkGrpCode"
        ddlEmpDiv.DataBind()
        ddlEmpDiv.SelectedIndex = 0

    End Sub

    Sub BindGrid()
        Dim intCnt As Integer
        Dim lbButton As LinkButton
        Dim lbstatus As Label

        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadData()
        PageCount = objGlobal.mtdGetPageCount(Session("SS_PAGINGCOUNT"), dgEmpList.PageSize)
        If PageCount < 1 Then
            PageCount = 1
        End If
        If lblCurrentIndex.Text >= PageCount Then
            If PageCount = 0 Then
                lblCurrentIndex.Text = 0
            Else
                lblCurrentIndex.Text = PageCount - 1
                dsData = LoadData()
            End If
        End If

        dgEmpList.DataSource = dsData
        dgEmpList.DataBind()
        lblPageCount.Text = PageCount
        BindPageList(PageCount)
        PageNo = lblCurrentIndex.Text + 1
        For intCnt = 0 To dgEmpList.Items.Count - 1
            lbstatus = dgEmpList.Items.Item(intCnt).FindControl("lblstatus")
            lbButton = dgEmpList.Items.Item(intCnt).FindControl("lbDelete")
            If lbstatus.Text.Trim() = "1" Then
			    lbButton.Text = "Delete"
                lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Else
				lbButton.Text = "UnDelete"
                lbButton.Attributes("onclick") = "javascript:return ConfirmAction('Undelete');"
            End If
            lbButton.Visible = True
        Next

    End Sub

    Protected Function LoadData() As DataSet
        Dim strOpCd_Get As String = "PR_PR_TRX_KTN_MAIN_GET_LIST"
        Dim strSrchEmpName As String
        Dim strSrchEmpDiv As String
        Dim strSrchBKMTy As String
        Dim strSrchBKMSub As String
        Dim strSrchMonth As String
        Dim strSrchYear As String
        Dim strSrchStatus As String
        Dim strSrchBKM As String
		Dim strSrchBKMType As String
		Dim strSrchBKMAkt As String

        Dim strSearch As String
		Dim strSearch1 As String
		Dim strSearch2 As String

        Dim strParamName As String
        Dim strParamValue As String

        Dim intErrNo As Integer
        Dim intCnt As Integer


        Dim strSortExpression As String


        Session("SS_PAGING") = lblCurrentIndex.Text

        cnt = 0
        strSrchBKM = IIf(txtBKM.Text.Trim() = "", "", txtBKM.Text.Trim())
        strSrchEmpName = IIf(txtEmpName.Text.Trim() = "", "", txtEmpName.Text.Trim())
        strSrchEmpDiv = IIf(ddlEmpDiv.SelectedItem.Value.Trim() = "", "", ddlEmpDiv.SelectedItem.Value.Trim())
        strSrchBKMTy = IIf(ddlEmpBKMTy.SelectedItem.Value.Trim() = "", "", ddlEmpBKMTy.SelectedItem.Value.Trim())
        strSrchBKMSub = IIf(ddlEmpBKMSub.SelectedItem.Value.Trim() = "", "", ddlEmpBKMSub.SelectedItem.Value.Trim())
        strSrchMonth = ddlEmpMonth.SelectedItem.Value.Trim()
        strSrchYear = ddlyear.SelectedItem.Value.Trim()
        strSrchStatus = IIf(ddlStatus.SelectedItem.Value.Trim() = "0", "%", ddlStatus.SelectedItem.Value.Trim())
        strSrchBKMType = ddlBkmtype.SelectedItem.Value.Trim()
		strSrchBKMAkt = ddlEmpBKMAkt.SelectedItem.Value.Trim()

        If SortExpression.Text = "UserName" Then
            strSortExpression = "C.UserName"
        Else
            strSortExpression = SortExpression.Text & " " & SortCol.Text
        End If

        strSearch1 = "AND x.BKMCode Like '%" & strSrchBKM & "%' " & _
 					 "AND x.BKMType = '" & strSrchBKMType & "' " & _
                     "AND x.IDDiv Like '" & strSrchEmpDiv & "%' " & _
                     "AND x.IDCat Like '" & strSrchBKMTy & "%' " & _
                     "AND x.IDSubCat Like '" & strSrchBKMSub & "%' " & _
                     "AND x.AccMonth = '" & strSrchMonth & "' " & _
                     "AND x.AccYear = '" & strSrchYear & "' " & _
                     "AND x.LocCode = '" & strLocation & "' " & _
                     "AND x.Status like '" & strSrchStatus & "%' " 
					 
	    strSearch2 = "AND x.BKMCode Like '%" & strSrchBKM & "%' " & _
 					 "AND x.BKMType = '" & strSrchBKMType & "' " & _
                     "AND x.IDDiv Like '" & strSrchEmpDiv & "%' " & _
                     "AND x.IDCat Like 'TR%' " & _
                     "AND x.IDSubCat Like '" & strSrchBKMSub & "%' " & _
                     "AND x.AccMonth = '" & strSrchMonth & "' " & _
                     "AND x.AccYear = '" & strSrchYear & "' " & _
                     "AND x.LocCode = '" & strLocation & "' " & _
                     "AND x.Status like '" & strSrchStatus & "%' " 
					 
					
	    strSearch = "AND B.EmpName Like '%" & strSrchEmpName & "%' " 

        strParamName = "LOC|SEARCH1|SEARCH2|SEARCH|SORT"
        strParamValue = strLocation & "|" & strSearch1 & "|"  & strSearch2 & "|" & strSearch & "|ORDER BY " & strSortExpression

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_Get, strParamName, strParamValue, objEmpDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_KTN_MAIN_GET_LIST&errmesg=" & Exp.Message )
        End Try

        Return objEmpDs
    End Function

    Sub BKMLink_Click(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        If Not E.CommandName.ToString = "Sort" And Not E.CommandName.ToString = "Delete" Then
            Dim intIndex As Integer = E.Item.ItemIndex
            Dim hid As HiddenField = dgEmpList.Items.Item(intIndex).FindControl("hidbkm")

            Session("KTN") = ddlEmpMonth.SelectedItem.Value.Trim() & "|" & _
                          ddlyear.SelectedItem.Value.Trim() & "|" & _
                          txtEmpName.Text.Trim() & "|" & _
                          ddlEmpDiv.SelectedItem.Value.Trim() & "|" & _
                          ddlEmpBKMTy.SelectedItem.Value.Trim() & "|" & _
                          ddlEmpBKMSub.SelectedItem.Value.Trim() & "|" & _
                          lblCurrentIndex.Text & "|" & _
						  ddlEmpBKMAkt.SelectedItem.Value.Trim() & "|" & _
						  ddlBkmtype.SelectedItem.Value.Trim() 


            Response.Redirect("PR_trx_KTNDet_New_Estate.aspx?BKMCode=" & hid.Value.Trim)

        End If
    End Sub

    Sub BindPageList(ByVal cnt As String)
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = lblPageCount.Text + 1
            arrDList.Add("Page " & count & " of " & cnt)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = lblCurrentIndex.Text
    End Sub

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                lblCurrentIndex.Text = 0
            Case "prev"
                lblCurrentIndex.Text = _
                Math.Max(0, lblCurrentIndex.Text - 1)
            Case "next"
                lblCurrentIndex.Text = _
                Math.Min(lblPageCount.Text - 1, lblCurrentIndex.Text + 1)
            Case "last"
                lblCurrentIndex.Text = lblPageCount.Text - 1
        End Select
        lstDropList.SelectedIndex = lblCurrentIndex.Text
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            lblCurrentIndex.Text = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        lblCurrentIndex.Text = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        lblCurrentIndex.Text = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOpCd_Status As String = "PR_PR_TRX_KTN_MAIN_DEL"
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strValue As String
        Dim lblEmpCode As Label
        Dim lbstatus As Label
        Dim strEmpCode As String
        Dim strstatus As String

		IF StatusPayroll(Cint(ddlEmpMonth.SelectedItem.Value.Trim()),ddlyear.SelectedItem.Value.Trim(),strLocation) = "3" Then
		  Exit Sub
		End IF
		
        dgEmpList.EditItemIndex = CInt(E.Item.ItemIndex)
        lblEmpCode = dgEmpList.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblBKMid")
        lbstatus = dgEmpList.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblstatus")
        strEmpCode = lblEmpCode.Text.Trim()
        strstatus = IIf(lbstatus.Text.Trim() = "1", "2", "1")

        strParam = "BKM|LOC|ST"
        strValue = strEmpCode & "|" & strLocation & "|" & strstatus

        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOpCd_Status, strParam, strValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_KTN_MAIN_DEL&errmesg=" & Exp.Message & "&redirect=")
        End Try
        dgEmpList.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub NewEmpBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_trx_KTNDet_New_Estate.aspx")

    End Sub

    Sub ddlEmpBKMTy_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        BindSubCategory(ddlEmpBKMTy.SelectedItem.Value.Trim, "")
		BindAktiviti(ddlEmpBKMTy.SelectedItem.Value.Trim,ddlEmpBKMsub.SelectedItem.Value.Trim,"")
    End Sub
	
	Sub ddlEmpBKMsub_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
		BindAktiviti(ddlEmpBKMTy.SelectedItem.Value.Trim,ddlEmpBKMsub.SelectedItem.Value.Trim,"")
    End Sub
	
	Function StatusPayroll(Byval mn as String,Byval yr as String,Byval loc as String)as Integer
        Dim strOpCdSP As String = "PR_PR_TRX_MTHEND_GET_STATUS"
        Dim objDataSet As New Object()
        Dim intErrNo As Integer
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
		Dim i as Integer
      
       
        ParamName = "MN|YR|LOC"
        ParamValue = mn & "|" & _
                     yr & "|" & _
                     loc
        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCdSP, ParamName, ParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_MTHEND_GET_STATUS&errmesg=" & Exp.Message & "&redirect=")
        End Try
	
		If objDataSet.Tables(0).Rows.Count > 0 Then
        		i = objDataSet.Tables(0).Rows(0).Item("Status")
				IF i = 3 Then
					UserMsgBox(Me, "Proses ditutup, Periode "& mn & "/" & yr & " Sudah Confirm")
				End If
		Else
		        i = 0 
		end if
		
       Return i

    End Function
	
    Sub UpdateStatusPayroll(Byval mn as String,Byval yr as String,Byval loc as String)
        Dim strOpCdSP As String = "PR_PR_TRX_MTHEND_UPD"
        Dim objDataSet As New Object()
        Dim intErrNo As Integer
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
      
       
        ParamName = "MN|YR|LOC|S1|S2|S3|S4"
        ParamValue = mn & "|" & _
                     yr & "|" & _
                     loc & "|1|||" 
        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCdSP, ParamName, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_MTHEND_UPD&errmesg=" & Exp.Message & "&redirect=")
        End Try

    End Sub
	
End Class
