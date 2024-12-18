
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


Public Class GL_Setup_Budget : Inherits Page

    Protected WithEvents EventData As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents StatusList As DropDownList
	Protected WithEvents LevelList As DropDownList
	Protected WithEvents lstAccYear As DropDownList
	
    Protected WithEvents SortExpression As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblDupMsg As Label
    Protected WithEvents sortcol As Label
    Protected WithEvents srchVehActCd As TextBox
    Protected WithEvents srchDescription As TextBox
	Protected WithEvents srchSubCat As DropDownList 
    Protected WithEvents srchUpdBy As TextBox
    Protected WithEvents srchStatus As DropDownList
	
	Protected WithEvents srchDiv As DropDownList
	Protected WithEvents srchTTnm As DropDownList
	
	
    Protected WithEvents lblVehActCode As Label
    Protected WithEvents lblDescription As Label
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblPleaseEnter As Label
    Protected WithEvents lblList As Label
    Protected WithEvents ddlAccount As DropDownList
    Protected WithEvents hidStatusEdited As HtmlInputHidden

    Protected objGLSetup As New agri.GL.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim ObjGLTrx As New agri.GL.ClsTrx

    Dim strOppCd_GET As String = "GL_GL_STP_BUDGET_GET"
    Dim strOppCd_ADD As String = "GL_GL_STP_BUDGET_ADD"

    Dim objDataSet As New Object()
    Dim objLangCapDs As New Object()
    Dim intErrNo As Integer
    Dim strParam As String = ""
    Dim strValidateCode As String
    Dim strValidateDesc As String
    Dim strAccCodeTag As String
    Dim intConfigsetting As Integer

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intGLAR As Integer
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String
    Dim ParamName As String
    Dim ParamValue As String
	
	Dim hastbl_bhn As New System.Collections.Hashtable()
	
	Dim OldCat As String
	Dim OldAcc As String
	Dim OldItm As String
	

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_SELACCMONTH")
        strAccYear = Session("SS_SELACCYEAR")
        intGLAR = Session("SS_ADAR")

        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADBudgeting), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")

        Else
       			
            If Not Page.IsPostBack Then
		'	response.write("1A")
		        BindAccYear(strAccYear)
                BindSearchList()
				BindSubCategory() 
				BindDivisi()				
				BindTTanam(srchDiv.SelectedItem.Value.Trim())
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

    Sub BindStatusList(ByVal index As Integer)
        StatusList = EventData.Items.Item(index).FindControl("StatusList")
        StatusList.Items.Add(New ListItem(objGLSetup.mtdGetVehicleStatus(objGLSetup.EnumVehicleStatus.Active), objGLSetup.EnumVehicleStatus.Active))
        StatusList.Items.Add(New ListItem(objGLSetup.mtdGetVehicleStatus(objGLSetup.EnumVehicleStatus.Deleted), objGLSetup.EnumVehicleStatus.Deleted))
    End Sub
	
	Sub BindLevelList(ByVal index As Integer)
        LevelList = EventData.Items.Item(index).FindControl("ddllevel")
        LevelList.Items.Add(New ListItem("0", "0"))
        LevelList.Items.Add(New ListItem("1", "1"))
		LevelList.Items.Add(New ListItem("2", "2"))
		LevelList.Items.Add(New ListItem("3", "3"))
    End Sub

    Sub BindSearchList()
        srchStatus.Items.Add(New ListItem(objGLSetup.mtdGetVehicleStatus(objGLSetup.EnumVehicleStatus.All), objGLSetup.EnumVehicleStatus.All))
        srchStatus.Items.Add(New ListItem(objGLSetup.mtdGetVehicleStatus(objGLSetup.EnumVehicleStatus.Active), objGLSetup.EnumVehicleStatus.Active))
        srchStatus.Items.Add(New ListItem(objGLSetup.mtdGetVehicleStatus(objGLSetup.EnumVehicleStatus.Deleted), objGLSetup.EnumVehicleStatus.Deleted))
        srchStatus.SelectedIndex = 1
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
	
    Protected Function LoadData() As DataSet

        Dim ParamNama As String
		Dim ParamValue As String
        Dim SearchStr As String
        Dim sortItem As String


        SearchStr = " WHERE A.Status like '" & IIf(srchStatus.SelectedItem.Value <> objGLSetup.EnumVehicleStatus.All, srchStatus.SelectedItem.Value, "%") & "' " & _
		            "AND '" & lstAccYear.SelectedItem.Value.trim() & "' between right(rtrim(periodestart),4) and right(rtrim(periodeend),4) "  
        If Not srchVehActCd.Text = "" Then
            SearchStr = SearchStr & " AND A.AccCode like '" & srchVehActCd.Text & "%'"
        End If
        If Not srchDescription.Text = "" Then
            SearchStr = SearchStr & " AND b.description like '%" & srchDescription.Text & "%'"
        End If
        If Not srchUpdBy.Text = "" Then
            SearchStr = SearchStr & " AND Usr.UserName like '" & srchUpdBy.Text & "%'"
        End If 
		
        SearchStr = SearchStr & " And subcategory= '" & srchSubCat.SelectedItem.Value & "' And A.LocCode= '" & strLocation & "' AND CodeBlkGrp='" & srchDiv.SelectedItem.Value.Trim() & "' AND CodeBlk like '" & srchTTnm.SelectedItem.Value.Trim() & "%'"

        ParamNama = "LOC|SEARCH|SORT"
        ParamValue = strLocation & "|" & SearchStr & "|Order by Subcategory,A.Acccode,A.itemcode,coalevel  " 


        Try
            intErrNo = ObjGLTrx.mtdGetDataCommon(strOppCd_GET, ParamNama, ParamValue, objDataSet)
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

    Sub DEDR_Edit(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim txtEditText As TextBox
        Dim ddlList As DropDownList
        Dim lbUpdbutton As LinkButton
        Dim validateCode As RequiredFieldValidator
        Dim validateDesc As RequiredFieldValidator
        Dim lblTemp As Label
        Dim ddlTemp As DropDownList
        hidStatusEdited.Value = "1"

        blnUpdate.Text = True
        EventData.EditItemIndex = CInt(e.Item.ItemIndex)

		
        BindGrid()
        If CInt(e.Item.ItemIndex) >= EventData.Items.Count Then
            EventData.EditItemIndex = -1
            Exit Sub
        End If
		BindItemDropList()
        BindStatusList(EventData.EditItemIndex)
		BindLevelList(EventData.EditItemIndex)
		BindAccCodeDropList(EventData.EditItemIndex)
		BindItemDropList(EventData.EditItemIndex)

                 StatusList.SelectedIndex = 0
                 lbUpdbutton = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Delete")
                 lbUpdbutton.Text = "Delete"
                 lbUpdbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"

                 lblTemp = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblAccCode")
                 ddlTemp = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("ddlAccount")
                 If Not (lblTemp Is Nothing) Then
                     ddlTemp.SelectedValue = Trim(lblTemp.Text)
					 OldAcc = Trim(lblTemp.Text)
					 
                 End If
				 
				 lblTemp = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblItem")
                 ddlTemp = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("ddlItem")
                 If Not (lblTemp Is Nothing) Then
                     ddlTemp.SelectedValue = Trim(lblTemp.Text) 
					 OldItm = Trim(lblTemp.Text)
                 End If
				 
				 
				 lblTemp = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lbllevel")
                 ddlTemp = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("ddllevel")
                 If Not (lblTemp Is Nothing) Then
                     ddlTemp.SelectedValue = Trim(lblTemp.Text)
                 End If

          
    End Sub

    Sub DEDR_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim txtEditText As TextBox
        Dim ddlList As DropDownList
        Dim strVehActCode As String
        Dim strDescription As String
        Dim strStatus As String
        Dim blnDupKey As Boolean = False
        Dim lblMsg As Label 
		Dim lblgrd as Label 
        Dim strCreateDate As String
        Dim list As DropDownList
		
        Dim strAcc As String
        Dim strType As String
		Dim strItm As String
		Dim strlvl As String
		Dim strdiv as String
		Dim strtt as String

		Dim PS as String 
		Dim PE as String 
		
        lblgrd = E.Item.FindControl("lblType")
        strType = lblgrd.Text.Trim()
		
		lblgrd = E.Item.FindControl("lbldiv")
        strdiv = lblgrd.Text.Trim()
		
		lblgrd = E.Item.FindControl("lblttnm")
        strtt = lblgrd.Text.Trim()

		ddlList = E.Item.FindControl("ddlAccount")
        strAcc = ddlList.SelectedItem.Value

		ddlList = E.Item.FindControl("ddlItem")
        strItm = ddlList.SelectedItem.Value
		
		ddlList = E.Item.FindControl("ddllevel")
        strlvl = ddlList.SelectedItem.Value
		
		txtEditText = E.Item.FindControl("PStart")
		PS = txtEditText.Text.Trim 
		
		txtEditText = E.Item.FindControl("PEnd")
		PE = txtEditText.Text.Trim 
		
		
		ParamName = "SC|AC|IC|LV|ST|LOC|CD|UD|UI|PS|PE|DV|TT"
        ParamValue = strType & "|" & _
                     strAcc & "|" & _
                     strItm & "|" & _
                     strlvl & "|1|" & _
					 strlocation & "|" & _
                     Now() & "|" & _
                     Now() & "|" & _
                     strUserId & "|" & _
					 PS & "|" & _
					 PE & "|" & _
					 strdiv & "|" & _ 
					 strtt 
					 
					 
	         Try
                intErrNo = ObjGLTrx.mtdInsertDataCommon(strOppCd_ADD, ParamName, ParamValue)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GL_VEHICLEACTIVITY&errmesg=" & lblErrMessage.Text & "&redirect=HR/Setup/HR_setup_Religion.aspx")
            End Try

            EventData.EditItemIndex = -1
            BindGrid()
    End Sub

    Sub DEDR_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        EventData.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)

        Dim txtEditText As TextBox
		Dim txtLabel As label
        Dim strType As String
        Dim strAccCode As String
        Dim strItem As String
		Dim strdiv as String
		Dim strtt as String
		
		Dim strOppCd As String = "GL_GL_STP_BUDGET_DEL"
        
        EventData.EditItemIndex = CInt(e.Item.ItemIndex)
        BindGrid()
		
      	txtLabel = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblType")
		strType = txtLabel.Text
		
		txtLabel = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lbldiv")
		strdiv = txtLabel.Text
		
		txtLabel = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblttnm")
		strtt = txtLabel.Text
		
		txtLabel = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblAccCode")
		strAccCode = txtLabel.Text 
		
		txtLabel = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblItem")
		strItem = txtLabel.Text
		
        ParamName = "SC|AC|IC|DV|TT"
        ParamValue = strType & "|" & _
                     strAccCode & "|" & _
                     strItem & "|" & _
					 strdiv & "|" & _ 
					 strtt 
					 
					
                     
        Try
            intErrNo = ObjGLTrx.mtdInsertDataCommon(strOppCd, ParamName, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GL_GL_STP_BUDGET_DEL&errmesg=" & lblErrMessage.Text & "&redirect=H")
        End Try

        EventData.EditItemIndex = -1
        BindGrid()

    End Sub

    Sub DEDR_Add(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim dataSet As DataSet = LoadData()
        Dim newRow As DataRow
        Dim lbUpdbutton As LinkButton
        Dim validateCode As RequiredFieldValidator
        Dim validateDesc As RequiredFieldValidator
        Dim PageCount As Integer
		Dim ddlTemp As DropDownList
        hidStatusEdited.Value = "0"

        blnUpdate.Text = False
        newRow = dataSet.Tables(0).NewRow()
		
		newRow.Item("PeriodeStart") = ""
		newRow.Item("PeriodeEnd") = "12" & lstAccYear.SelectedItem.Value.Trim
        newRow.Item("subcategory") = srchSubCat.SelectedItem.Value.Trim
        newRow.Item("CodeBlkGrp") = srchDiv.SelectedItem.Value.Trim
        newRow.Item("CodeBlk") = srchTTnm.SelectedItem.Value.Trim
        newRow.Item("acccode") = ""
        newRow.Item("itemcode") = ""
        newRow.Item("AccCode") = ""
		newRow.Item("coalevel") = ""
        newRow.Item("Status") = 1
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
		
        BindItemDropList()
        BindStatusList(EventData.EditItemIndex)
		BindLevelList(EventData.EditItemIndex)
		BindAccCodeDropList(EventData.EditItemIndex)
		BindItemDropList(EventData.EditItemIndex)
        
      	
       ' validateCode = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateAcc")
       ' validateDesc = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateItem")

        'validateCode.ErrorMessage = strValidateCode
        'validateDesc.ErrorMessage = strValidateDesc

    End Sub

    Sub BindAccCodeDropList(ByVal index As Integer)
        ddlAccount = EventData.Items.Item(index).FindControl("ddlAccount")

        Dim strOpCdAccCode_Get As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET" '"GL_CLSTRX_ACCOUNTCODE_LIST_GET"
        Dim dsForDropDown As DataSet
        Dim SelectedIndex As Integer = -1
        Dim intCnt As Integer
        Dim strFieldCheck As String
        'Dim strParam As New StringBuilder("")
        Dim strParam As String = "Order By ACC.AccCode|And ACC.Status = '" & objGLSetup.EnumAccountCodeStatus.Active & "'"
        Dim strWorkShopFilter As String = ""

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.InternalLabourCharge), intConfigsetting) = True Then
            strWorkShopFilter = " AND WSAccDistUse = '" & objGLSetup.EnumWSAccDistUse.No & "' "
        End If

        strParam = strParam & " AND (AccType = '" & objGLSetup.EnumAccountType.BalanceSheet & "' OR (AccType = '" & objGLSetup.EnumAccountType.ProfitAndLost & "' AND AccPurpose = '" & objGLSetup.EnumAccountPurpose.NonVehicle & "'))" & strWorkShopFilter
        strParam = strParam & IIf(Session("SS_COACENTRALIZED") = "1", "", " AND ACC.LocCode = '" & Trim(strLocation) & "' ")

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCdAccCode_Get, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.AccountCode, _
                                                   dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BindAccCodeDropList&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            strFieldCheck = dsForDropDown.Tables(0).Rows(intCnt).Item(0)
            dsForDropDown.Tables(0).Rows(intCnt).Item(1) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0)) & " ( " & _
                                                          Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(1)) & " )"
            dsForDropDown.Tables(0).Rows(intCnt).Item(0) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0))
        Next intCnt

      
        ddlAccount.DataSource = dsForDropDown.Tables(0)
        ddlAccount.DataValueField = "AccCode"
        ddlAccount.DataTextField = "Description"
        ddlAccount.DataBind()

    End Sub
	
	Sub BindItemDropList()
        Dim strOpCd_DivId As String = "PR_PR_TRX_BKM_BAHAN_LIST_GET"
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objJobGroup As New Object
        Dim ParamNama As String
		Dim ParamValue As String
        Dim filter As String = ""
       
        if hastbl_bhn.Count = 0 then
				
		Select Case Left(Trim(srchSubCat.SelectedValue),2) 
            Case "UM"
                filter = "AND (ProdTypeCode in ('005','101','102','103','201','202','302')  or ProdTypeCode like '4%' or ProdTypeCode like '5%' or ProdTypeCode like '9%') "
			Case "PN"
                filter = "AND (ProdTypeCode in ('001','002','003','004','005','101','661','201','202','310','316','802','302')) "           
		    Case Else
                filter = "AND (ProdTypeCode in ('001','002','003','004','005','101','661','201','202','310','316','802','302')) "
            
        End Select
	
        ParamNama = "SEARCH|SORT"
        ParamValue = "WHERE LocCode = '" & strlocation & "' " & filter & "|" & "Order by itemCode"

        Try
            intErrNo = ObjGLTrx.mtdGetDataCommon(strOpCd_DivId, ParamNama, ParamValue, objJobGroup)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_BAHAN_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        hastbl_bhn.Clear()
	    hastbl_bhn.Add("" , "None")
	
        If objJobGroup.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objJobGroup.Tables(0).Rows.Count - 1
                hastbl_bhn.Add(Trim(objJobGroup.Tables(0).Rows(intCnt).Item("ItemCode")) , Trim(objJobGroup.Tables(0).Rows(intCnt).Item("ItemCode")) & "  " & Trim(objJobGroup.Tables(0).Rows(intCnt).Item("Descr")))
            Next
        End If

		end if
     
    End Sub
	
	Sub BindItemDropList(ByVal index As Integer)	
		Dim DDLabs As DropDownList
		DDLabs = EventData.Items.Item(index).FindControl("ddlItem")
        Dim sorted_job = New SortedList(hastbl_bhn)
        DDLabs.DataSource = sorted_job
        DDLabs.DataTextField = "value"
        DDLabs.DataValueField = "key"
        DDLabs.DataBind()
    End Sub
End Class
