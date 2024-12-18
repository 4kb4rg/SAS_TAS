Imports System
Imports System.Data
Imports System.Collections 
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction


Public Class GL_trx_Budget_Item_list : Inherits Page

    Protected WithEvents dgList As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblErrMessage As Label

    Protected WithEvents srchAccCode As TextBox
    Protected WithEvents lstAccYear As DropDownList
    Protected WithEvents srchSubCat As DropDownList 

	Protected WithEvents srchDiv As DropDownList
	Protected WithEvents srchTTnm As DropDownList
	

    Protected WithEvents srchUpdateBy As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents srchStatusList As DropDownList
    Protected WithEvents lblBlkCode As Label
    Protected WithEvents lblVehCode As Label

    Protected objGLTrx As New agri.GL.ClsTrx()
    Protected objGLSetup As New agri.GL.clsSetup()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intGLAR As Integer
    Dim strLocType As String

    Dim objBudgetDs As New DataSet()
    Dim objLangCapDs As New Object()

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_SELACCMONTH")
        strAccYear = Session("SS_SELACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intGLAR = Session("SS_ADAR")
        strLocType = Session("SS_LOCTYPE")
		

     If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
		ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADBudgeting), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
       Else
            If SortExpression.Text = "" Then
                SortExpression.Text = "AccYear"
            End If

            onload_GetLangCap()

            If Not Page.IsPostBack Then
                BindAccYear(strAccYear)
				BindSubCategory() 	
				BindDivisi()				
				BindTTanam(srchDiv.SelectedItem.Value.Trim())
				BindSearchStatusList()
                BindGrid()
                BindPageList()
				
            End If
        End If
    End Sub

    Sub BindSearchStatusList()

        srchStatusList.Items.Add(New ListItem(objGLSetup.mtdGetActSatus(objGLSetup.EnumActStatus.All), objGLSetup.EnumActStatus.All))
        srchStatusList.Items.Add(New ListItem(objGLSetup.mtdGetActSatus(objGLSetup.EnumActStatus.Active), objGLSetup.EnumActStatus.Active))
        srchStatusList.Items.Add(New ListItem(objGLSetup.mtdGetActSatus(objGLSetup.EnumActStatus.Deleted), objGLSetup.EnumActStatus.Deleted))
        srchStatusList.SelectedIndex = 1

    End Sub

	Sub BindAccYear(ByVal pv_strAccYear As String)
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim intSelectedIndex As Integer
        Dim objAccYearDs As New Object
        Dim dr As DataRow
        Dim strOpCd As String = "ADMIN_CLSACCPERIOD_CONFIG_GET"

        strParamName = "LOCCODE|SEARCHSTR|SORTEXP"
        strParamValue = strLocation & "||Order By HD.AccYear"

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objAccYearDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objAccYearDs.Tables(0).Rows.Count - 1
            objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear") = Trim(objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear"))
            objAccYearDs.Tables(0).Rows(intCnt).Item("UserName") = Trim(objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear"))
            If objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear") = pv_strAccYear Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        lstAccYear.DataSource = objAccYearDs.Tables(0)
        lstAccYear.DataValueField = "AccYear"
        lstAccYear.DataTextField = "UserName"
        lstAccYear.DataBind()
        lstAccYear.SelectedIndex = intSelectedIndex - 1
    End Sub

	Sub BindDivisi()
	    Dim strOpCd_DivId As String = "GL_CLSSETUP_BLOCKGROUP_LIST_GET"
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objJobGroup As New Object
		Dim ParamNama As String
		Dim ParamValue As String
		Dim dr As DataRow
	
        ParamNama = "SEARCHSTR|SORTEXP"
        ParamValue = "and blk.LocCode = '" & strLocation & "'  and blk.Status like '1'|Order by BlkGrpCode"

        Try
            intErrNo = ObjGLTrx.mtdGetDataCommon(strOpCd_DivId, ParamNama, ParamValue, objJobGroup)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_BLOCKGROUP_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

		dr = objJobGroup.Tables(0).NewRow()
        dr("BlkGrpCode") = ""
        dr("BlkGrpCode") = ""
        objJobGroup.Tables(0).Rows.InsertAt(dr, 0)
             
        srchDiv.DataSource = objJobGroup.Tables(0)
        srchDiv.DataValueField = "BlkGrpCode"
        srchDiv.DataTextField = "BlkGrpCode"
        srchDiv.DataBind()
	End Sub
	 
	Sub srchDiv_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        BindTTanam(srchDiv.SelectedItem.Value.Trim)
		srchBtn_Click(Sender,e)
    End Sub
	
	Sub BindTTanam(Byval str as String)
	    Dim strOpCd_DivId As String = "GL_CLSSETUP_BLOCK_LIST_GET"
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objJobGroup As New Object
		Dim ParamNama As String
		Dim ParamValue As String
		Dim dr As DataRow
	
        ParamNama = "STRSEARCH"
        ParamValue = "and blk.LocCode = '" & strLocation & "'  and blk.Status like '1' and blk.BlkGrpCode = '" & str & "'"

        Try
            intErrNo = ObjGLTrx.mtdGetDataCommon(strOpCd_DivId, ParamNama, ParamValue, objJobGroup)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_BLOCKGROUP_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

             
	    dr = objJobGroup.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("BlkCode") = ""
        objJobGroup.Tables(0).Rows.InsertAt(dr, 0)
		
        srchTTnm.DataSource = objJobGroup.Tables(0)
        srchTTnm.DataValueField = "BlkCode"
        srchTTnm.DataTextField = "BlkCode"
        srchTTnm.DataBind()
	End Sub

	Sub srchTTnm_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        srchBtn_Click(Sender,e)
    End Sub
	
	Sub BindSubCategory()
	    Dim strOpCd_DivId As String = "GL_GL_STP_SUBCATEGORY_GET"
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objJobGroup As New Object
		Dim ParamNama As String
		Dim ParamValue As String
		Dim intselect As Integer 

        ParamNama = "SEARCH|SORT"
        ParamValue = "|Order by SubCatID"

        Try
            intErrNo = ObjGLTrx.mtdGetDataCommon(strOpCd_DivId, ParamNama, ParamValue, objJobGroup)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_GL_STP_SUBCATEGORY_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objJobGroup.Tables(0).Rows.Count - 1
            objJobGroup.Tables(0).Rows(intCnt).Item("SubCatID") = Trim(objJobGroup.Tables(0).Rows(intCnt).Item("SubCatID"))
            objJobGroup.Tables(0).Rows(intCnt).Item("SubCatName") = Trim(objJobGroup.Tables(0).Rows(intCnt).Item("SubCatName")) & " (" & Trim(objJobGroup.Tables(0).Rows(intCnt).Item("SubCatID")) & ")" 
			if Trim(objJobGroup.Tables(0).Rows(intCnt).Item("SubCatID"))="PNS" then
				intselect=intCnt
			end if 
			
        Next
        
        srchSubCat.DataSource = objJobGroup.Tables(0)
        srchSubCat.DataValueField = "SubCatID"
        srchSubCat.DataTextField = "SubCatName"
        srchSubCat.DataBind()
		srchSubCat.SelectedIndex = intselect
	End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgList.CurrentPageIndex = 0
        dgList.EditItemIndex = -1
        BindGrid()
        BindPageList()
    End Sub

    Sub BindGrid()
        Dim lbButton As LinkButton
        Dim intCnt As Integer
        Dim Status As Label
        Dim strStatus As String

        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadData()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgList.PageSize)

        dgList.DataSource = dsData
        If dgList.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgList.CurrentPageIndex = 0
            Else
                dgList.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgList.DataBind()
        BindPageList()
        PageNo = dgList.CurrentPageIndex + 1
        lblTracker.Text = "Page " & PageNo & " of " & dgList.PageCount


        For intCnt = 0 To dgList.Items.Count - 1
            Status = dgList.Items.Item(intCnt).FindControl("lblStatus")
            strStatus = Status.Text

            Select Case strStatus

                Case objGLSetup.mtdGetActSatus(objGLSetup.EnumActStatus.Active)
                    lbButton = dgList.Items.Item(intCnt).FindControl("Delete")
                    lbButton.Visible = True
                    lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Case objGLSetup.mtdGetActSatus(objGLSetup.EnumActStatus.Deleted)
                    lbButton = dgList.Items.Item(intCnt).FindControl("Delete")
                    lbButton.Visible = False
            End Select
        Next

    End Sub

    Sub BindPageList()
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgList.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While

        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgList.CurrentPageIndex

    End Sub

    Protected Function LoadData() As DataSet

        Dim strOpCd As String = "GL_CLSTRX_BUDGET_ITEM_SEARCH"

        Dim dsResult As New Object

        Dim strSrchAccYear As String
        Dim strSrchAccCode As String
        
        Dim strSrchStatus As String
        Dim strSrchLastUpdate As String
        Dim strSearch As String
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer

        strSrchAccYear = IIf(Trim(lstAccYear.SelectedItem.Value) = "", "", " AND  AccYear = '" & lstAccYear.SelectedItem.Value & "'")
        strSrchAccCode = IIf(Trim(srchAccCode.Text) = "", "", " AND  a.AccCode LIKE '%" & srchAccCode.Text & "%'")

        
        strSrchStatus = IIf(srchStatusList.SelectedItem.Value = objGLSetup.EnumActStatus.All, "", " AND  a.Status = '" & srchStatusList.SelectedItem.Value & "'")
        strSrchLastUpdate = IIf(Trim(srchUpdateBy.Text) = "", "", " AND a.UpdateID = '" & srchUpdateBy.Text & "'")


        strSearch = strSrchAccYear & strSrchAccCode & strSrchStatus & strSrchLastUpdate & _
                    " And IDSubCat= '" & srchSubCat.SelectedItem.Value & "' AND  LOCCODE = '" & strLocation & "' AND CodeBlkGrp='" & srchDiv.SelectedItem.Value.Trim() & "' AND CodeBlk like '" & srchTTnm.SelectedItem.Value.Trim() & "%'"

        strSearch = " WHERE " & Mid(Trim(strSearch), 6)

        strParamName = "LOC|STRSEARCH"
        strParamValue = strlocation & "|" & strSearch

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                dsResult)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BUDGET_LIST_SEARCH&errmesg=" & lblErrMessage.Text & "&redirect=gl/trx/gl_trx_budget_list.aspx")
        End Try


        For intCnt = 0 To dsResult.Tables(0).Rows.Count - 1
            dsResult.Tables(0).Rows(intCnt).Item("AccYear") = Trim(dsResult.Tables(0).Rows(intCnt).Item("AccYear"))
            dsResult.Tables(0).Rows(intCnt).Item("AccCode") = Trim(dsResult.Tables(0).Rows(intCnt).Item("AccCode"))
            dsResult.Tables(0).Rows(intCnt).Item("ItemCode") = Trim(dsResult.Tables(0).Rows(intCnt).Item("ItemCode"))
            dsResult.Tables(0).Rows(intCnt).Item("TotalAmount") = Trim(dsResult.Tables(0).Rows(intCnt).Item("TotalAmount"))
            dsResult.Tables(0).Rows(intCnt).Item("TotalFisik") = Trim(dsResult.Tables(0).Rows(intCnt).Item("TotalFisik"))
            dsResult.Tables(0).Rows(intCnt).Item("UpdateDate") = Trim(dsResult.Tables(0).Rows(intCnt).Item("UpdateDate"))
            dsResult.Tables(0).Rows(intCnt).Item("Status") = Trim(dsResult.Tables(0).Rows(intCnt).Item("Status"))
            dsResult.Tables(0).Rows(intCnt).Item("UserName") = Trim(dsResult.Tables(0).Rows(intCnt).Item("UserName"))
        Next

        Return dsResult

    End Function

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgList.CurrentPageIndex = 0
            Case "prev"
                dgList.CurrentPageIndex = _
                Math.Max(0, dgList.CurrentPageIndex - 1)
            Case "next"
                dgList.CurrentPageIndex = _
                Math.Min(dgList.PageCount - 1, dgList.CurrentPageIndex + 1)
            Case "last"
                dgList.CurrentPageIndex = dgList.PageCount - 1
        End Select

        lstDropList.SelectedIndex = dgList.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgList.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgList.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        dgList.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)


        Dim strOpCd As String = "GL_CLSTRX_BUDGET_ITEM_DEL"

        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        Dim intErrNo As Integer
        Dim strTrxID As String
        Dim lblTrxID As Label


        dgList.EditItemIndex = CInt(E.Item.ItemIndex)
        lblTrxID = dgList.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblTrxID")
        strTrxID = lblTrxID.Text

        strParamName = "TRXID"

        strParamValue = strTrxID 

        Try
            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd, _
                                                    strParamName, _
                                                    strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_BUDGET_DELETED&errmesg=" & lblErrMessage.Text & "&redirect=gl/trx/gl_trx_budget_list")
        End Try

        dgList.EditItemIndex = -1
        BindGrid()

    End Sub

    Sub NewBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
         Dim strOpCd As String = "GL_CLSTRX_BUDGET_ITEM_GEN"

        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        Dim intErrNo As Integer

        strParamName = "AY|LOC|CD|UD|UI|AM"
        strParamValue = lstAccYear.SelectedItem.Value & "|" & strlocation & "|" & Now() & "|" & Now() & "|" & strUserId  & "|" & strAccMonth

        Try
            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd, _
                                                    strParamName, _
                                                    strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_BUDGET_DELETED&errmesg=" & lblErrMessage.Text & "&redirect=gl/trx/gl_trx_budget_list")
        End Try

         BindGrid()
    End Sub

    Sub onload_GetLangCap()

        GetEntireLangCap()

        'lblBlkCode.Text = GetCaption(objLangCap.EnumLangCap.SubBlock)
        'lblVehCode.Text = GetCaption(objLangCap.EnumLangCap.Vehicle)

        'dgList.Columns(2).HeaderText = lblBlkCode.Text
        'dgList.Columns(3).HeaderText = lblVehCode.Text

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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/gl_setup_subblock.aspx")
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


End Class
