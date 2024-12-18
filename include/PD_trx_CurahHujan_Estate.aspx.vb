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

Public Class PD_trx_CurahHujan_Estate : Inherits Page

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
    
	Protected WithEvents ddlMonth As DropDownList
    Protected WithEvents ddlyear As DropDownList
	Protected WithEvents ddlDiv As DropDownList
	
    Protected WithEvents isNew As HtmlInputHidden
	Protected strDateFormat As String

    Protected objHR As New agri.HR.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
	
	Dim ddlCodeGrpJob As DropDownList

    Dim strOppCd_GET As String = "PD_PD_TRX_CURAHHUJAN_GET"
    Dim strOppCd_UPD As String = "PD_PD_TRX_CURAHHUJAN_UPD"

    Dim objDataSet As New Object()
    Dim objLangCapDs As New Object()
    Dim intErrNo As Integer
    Dim strParam As String = ""
    Dim strValidateCode As String
    Dim strValidateDesc As String
    Dim Prefix As String = "PD"

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim objSysCfg As New agri.PWSystem.clsConfig()
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
	Dim strAcceptFormat As String

    Dim hastbl_cb As New System.Collections.Hashtable()


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")

        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        intHRAR = Session("SS_PDAR")
        strLocType = Session("SS_LOCTYPE")
		strDateFormat = Session("SS_DATEFMT")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
	    'ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDEstateProduction), intHRAR) = False Then
        '    Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
		
		
            If SortExpression.Text = "" Then
                SortExpression.Text = "BlkGrpCode,CH_Date"
                sortcol.Text = "ASC"
            End If
            If Not Page.IsPostBack Then
				BindAccYear(Session("SS_SELACCYEAR"))
                ddlMonth.SelectedIndex = Cint(Session("SS_SELACCMONTH")) - 1
                src_BindDivisi()
		        BindGrid()
                BindPageList()
            End If

        End If
    End Sub
	
	Function Date_Validation(ByVal pv_strInputDate As String, ByVal pv_blnIsShortDate As Boolean) As String
        Dim objSysCfgDs As New Object()
        Dim objActualDate As New Object()
        Dim strDateFormat As String
        Dim strParam As String
        Dim intErrNo As Integer

        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"

        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GR_GET_CONFIG&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_GRList.aspx")
        End Try

        strDateFormat = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))

        If pv_blnIsShortDate Then
            Date_Validation = objGlobal.GetShortDate(strDateFormat, pv_strInputDate)
        Else
            If objGlobal.mtdValidInputDate(strDateFormat, _
                                           pv_strInputDate, _
                                           strAcceptFormat, _
                                           objActualDate) = True Then
                Date_Validation = objActualDate
            Else
                Date_Validation = ""
            End If
        End If
    End Function

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

    Sub BindStatusList(ByVal index As Integer)

        StatusList = EventData.Items.Item(index).FindControl("StatusList")
        StatusList.Items.Add(New ListItem(objHR.mtdGetDeptCodeStatus(objHR.EnumDeptCodeStatus.Active), objHR.EnumDeptCodeStatus.Active))
        StatusList.Items.Add(New ListItem(objHR.mtdGetDeptCodeStatus(objHR.EnumDeptCodeStatus.Deleted), objHR.EnumDeptCodeStatus.Deleted))

    End Sub

    Sub BindSearchList()

        srchStatusList.Items.Add(New ListItem(objHR.mtdGetDeptCodeStatus(objHR.EnumDeptCodeStatus.All), objHR.EnumDeptCodeStatus.All))
        srchStatusList.Items.Add(New ListItem(objHR.mtdGetDeptCodeStatus(objHR.EnumDeptCodeStatus.Active), objHR.EnumDeptCodeStatus.Active))
        srchStatusList.Items.Add(New ListItem(objHR.mtdGetDeptCodeStatus(objHR.EnumDeptCodeStatus.Deleted), objHR.EnumDeptCodeStatus.Deleted))
        srchStatusList.SelectedIndex = 1

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

	
	Function BindDivisi()  As DataSet 
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_DIVISICODE_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpDivDs As New Object()
        Dim dr As DataRow

        strParamName = "SEARCH|SORT"
        strParamValue = "AND A.LocCode='" & strLocation & "' AND A.Status='1'|ORDER By BlkGrpCode"


        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpDivDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_STP_DIVISI_LIST_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objEmpDivDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpDivDs.Tables(0).Rows.Count - 1
                objEmpDivDs.Tables(0).Rows(intCnt).Item("BlkGrpCode") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("BlkGrpCode"))
                objEmpDivDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("BlkGrpCode")) & " (" & Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            Next
        End If

        dr = objEmpDivDs.Tables(0).NewRow()
        dr("BlkGrpCode") = ""
        dr("Description") = "Pilih Divisi"
        objEmpDivDs.Tables(0).Rows.InsertAt(dr, 0)

        Return objEmpDivDs

		End Function

	Sub onLoad_BindDivisi(ByVal index As Integer)
        ddlCodeGrpJob = EventData.Items.Item(index).FindControl("ddlBlkGrpCode")
        ddlCodeGrpJob.DataSource = BindDivisi()
        ddlCodeGrpJob.DataTextField = "Description"
        ddlCodeGrpJob.DataValueField = "BlkGrpCode"
        ddlCodeGrpJob.DataBind()
    End Sub

    Sub src_BindDivisi()
        ddlDiv.DataSource = BindDivisi()
        ddlDiv.DataTextField = "Description"
        ddlDiv.DataValueField = "BlkGrpCode"
        ddlDiv.DataBind()
    End Sub
		
  
    Protected Function LoadData() As DataSet

        Dim SearchStr As String
        Dim sortitem As String


        SearchStr = " AND A.LocCode='" & strlocation & "' AND A.BlkGrpCode like '%" & ddlDiv.SelectedItem.Value & "%' AND month(A.CH_Date)=" & ddlMonth.SelectedIndex + 1 & " AND year(A.CH_Date)=" & ddlyear.selectedItem.Value  
        
        
        sortitem = "ORDER BY " & SortExpression.Text & " " & sortcol.Text
        ParamNama = "SEARCH|SORT"
        ParamValue = SearchStr & "|" & sortitem

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOppCd_GET, ParamNama, ParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PREMIDRIVER_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
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
        Dim lbl As Label
        Dim Validator As RequiredFieldValidator
		Dim lblTemp As Label
        Dim ddlTemp As DropDownList

        blnUpdate.Text = True
        EventData.EditItemIndex = CInt(E.Item.ItemIndex)

        BindGrid()
        If CInt(E.Item.ItemIndex) >= EventData.Items.Count Then
            EventData.EditItemIndex = -1
            Exit Sub
        End If
		onLoad_BindDivisi(EventData.EditItemIndex)
        BindStatusList(EventData.EditItemIndex)

        EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Status")
                StatusList.SelectedIndex = 0
                EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("CH_Date")
                EditText.ReadOnly = True
                
                List = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("ddlBlkGrpCode")
       			lblTemp = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblBlkGrpCode")
                
                If Not (lblTemp Is Nothing) Then
                    List.Selectedvalue = Trim(lblTemp.Text)
                End If

                Updbutton = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Delete")
                Updbutton.Text = "Delete"
                Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            
        Validator = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("validatehol")
        Validator.ErrorMessage = strValidateCode 
		
		Validator = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("validatediv")
        Validator.ErrorMessage = strValidateDesc 
		
    End Sub

    Sub DEDR_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim list As DropDownList
		
        Dim DT As String
        Dim DV As String
        Dim PG As String
        Dim SI As String
        Dim SR As String
        Dim ML As String
        
        Dim Status As String
        Dim blnDupKey As Boolean = False
        Dim lblMsg As Label
        Dim CreateDate As String

        EditText = E.Item.FindControl("CH_Date")
        DT = Date_Validation(EditText.Text, False)

		list = E.Item.FindControl("ddlBlkGrpCode")
        DV = list.SelectedItem.Value.Trim
		
        EditText = E.Item.FindControl("CH_pagi")
        PG = EditText.Text

        EditText = E.Item.FindControl("CH_siang")
        SI = EditText.Text

        EditText = E.Item.FindControl("CH_sore")
        SR = EditText.Text

        EditText = E.Item.FindControl("CH_malam")
        ML = EditText.Text
		
		

		ParamNama = "DT|DV|LOC|PG|SI|SR|ML|CD|UD|UI"
        ParamValue = DT & "|" & _
                     DV & "|" & _
                     strLocation & "|" & _
                     PG & "|" & _
                     SI & "|" & _
                     SR & "|" & _
                     ML & "|" & _
                     DateTime.Now() & "|" & _
                     DateTime.Now() & "|" & _
                     strUserId 


        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOppCd_UPD, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PREMIDRIVER_UPD&errmesg=" & lblErrMessage.Text & "&redirect=HR/Setup/HR_setup_DeptCode.aspx")
        End Try


		
			EventData.EditItemIndex = -1
			BindGrid()
		
    End Sub

    Sub DEDR_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        EventData.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)

        Dim EditText As TextBox
        Dim lbl As Label
		Dim DivCode As String
		
        Dim Description As String
        Dim list As DropDownList
        Dim pstart As String
        Dim pend As String
        Dim thari As String
        Dim Basis As String
        Dim Unit As String
        Dim Rate As String
        Dim Status As String
        Dim blnDupKey As Boolean = False

        EditText = E.Item.FindControl("CH_Date")
        pstart = Date_Validation(EditText.Text, False)

        lbl = E.Item.FindControl("lblBlkGrpCode")
        pend = lbl.Text.Trim

        ParamNama = "DT|DV|LOC"
        ParamValue = pstart & "|" & _
                     pend & "|" & _
                     strlocation 

        Try
            intErrNo = ObjOK.mtdInsertDataCommon("PD_PD_TRX_CURAHHUJAN_DEL", ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PREMIDRIVER_UPD&errmesg=" & lblErrMessage.Text)
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
        Dim ddl As DropDownList
      
        blnUpdate.Text = False
      
        newRow = dataSet.Tables(0).NewRow()

        newRow.Item("CH_Date") = Format(DateTime.Now(), "dd MMM yyyy")
        newRow.Item("BlkGrpCode") = ""
        newRow.Item("CH_pagi") = 0
        newRow.Item("CH_siang") = 0
        newRow.Item("CH_sore") = 0
        newRow.Item("CH_malam") = 0
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
		onLoad_BindDivisi(EventData.EditItemIndex)
        BindStatusList(EventData.EditItemIndex)
		
        Updbutton = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("Delete")
        Updbutton.Visible = False
		
        Validator = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validatehol")
        Validator.ErrorMessage = strValidateCode
		Validator = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validatediv")
        Validator.ErrorMessage = strValidateDesc

        
    End Sub

End Class
