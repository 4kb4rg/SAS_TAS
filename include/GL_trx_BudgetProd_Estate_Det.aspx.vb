
Imports System
Imports System.Data
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports System.Globalization
Imports System.Math 


Public Class GL_trx_BudgetProd_Estate_Det : Inherits Page

    Dim objGLTrx As New agri.GL.clsTrx()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblCreateDate As Label
    Protected WithEvents lblLastUpdate As Label

    Protected WithEvents txtYearBudget As TextBox
    Protected WithEvents lblUpdateBy As Label

    Protected WithEvents lblCode As Label

    Protected WithEvents lblValueError As Label

    Protected WithEvents txtB01T As TextBox
    Protected WithEvents txtB02T As TextBox
    Protected WithEvents txtB03T As TextBox
    Protected WithEvents txtB04T As TextBox
    Protected WithEvents txtB05T As TextBox
    Protected WithEvents txtB06T As TextBox
    Protected WithEvents txtB07T As TextBox
    Protected WithEvents txtB08T As TextBox
    Protected WithEvents txtB09T As TextBox
    Protected WithEvents txtB10T As TextBox
    Protected WithEvents txtB11T As TextBox
    Protected WithEvents txtB12T As TextBox
    
    Protected WithEvents txtR01T As TextBox
    Protected WithEvents txtR02T As TextBox
    Protected WithEvents txtR03T As TextBox
    Protected WithEvents txtR04T As TextBox
    Protected WithEvents txtR05T As TextBox
    Protected WithEvents txtR06T As TextBox
    Protected WithEvents txtR07T As TextBox
    Protected WithEvents txtR08T As TextBox
    Protected WithEvents txtR09T As TextBox
    Protected WithEvents txtR10T As TextBox
    Protected WithEvents txtR11T As TextBox
    Protected WithEvents txtR12T As TextBox
    Protected WithEvents ddlGroupCOA As DropDownList
	Protected WithEvents ddlKomoditi As DropDownList
	Protected WithEvents ddlSubBlok As DropDownList
	
    Protected WithEvents btnSave As ImageButton
    Protected WithEvents btnDelete As ImageButton
    Protected WithEvents btnBack As ImageButton
	
	Protected WithEvents dgBudget As DataGrid

    Dim objAccDs As New Dataset()
    Dim objBlkDs As New Dataset()
    Dim objVehDs As New Dataset()

    Dim strParam As String
    Dim objDataSet As New DataSet()
    Dim objLangCapDs As New Object()
    Dim intErrNo As Integer

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intGLAR As Integer

    Dim intConfigSetting As Integer

    Dim strBdgYear As String
    Dim strBdgAcc As String
    Dim strLocType As String
    Dim BlockTag As String
	
	Dim PAccCode As String
	Dim PGroupCOA As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        intGLAR = Session("SS_GLAR")
        intConfigSetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLMonthEnd), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            PAccCode = Trim(IIf(Request.QueryString("AccMonth") <> "", Request.QueryString("AccMonth"), Request.Form("AccMonth")))
            PGroupCOA = Trim(IIf(Request.QueryString("GroupCoa") <> "", Request.QueryString("GroupCoa"), Request.Form("GroupCoa")))


            lblValueError.Visible = False

            If Not IsPostBack Then
			
                If Not PAccCode = "" Then
                    txtYearBudget.Text = PAccCode 
					BindGroupCoa(PGroupCOA)
					BindSubBlok(PGroupCOA)
                    dbBudget_OnLoad()
                Else
				    BindGroupCoa("")
                End If
			End If
        End If
    End Sub

   
    Sub BindGroupCoa(ByVal pv_strGroupCoa As String)
        Dim strOpCd As String = "GL_CLSSETUP_BLOCK_LIST_GET"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer = 0

        strParamName = "STRSEARCH"
        strParamValue = "and blk.LocCode = '" & strlocation & "' and blk.Status = '1'  order by blk.BlkCode asc" 

		
        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objAccDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=FA_TRX_ASSETADD_ACCCODE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objAccDs.Tables(0).Rows.Count - 1 
		    objAccDs.Tables(0).Rows(intCnt).Item("Description") = trim(objAccDs.Tables(0).Rows(intCnt).Item("BlkCode")) & " (" & objAccDs.Tables(0).Rows(intCnt).Item("Description").Trim() & ")"
            If trim(objAccDs.Tables(0).Rows(intCnt).Item("BlkCode")) = Trim(pv_strGroupCoa) Then
                intSelectedIndex = intCnt + 1
                Exit For
            End If
        Next
		
		
        dr = objAccDs.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("Description") = "Pilih Tahun Tanam"
        objAccDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlGroupCOA.DataSource = objAccDs.Tables(0)
        ddlGroupCOA.DataValueField = "BlkCode"
        ddlGroupCOA.DataTextField = "Description"
        ddlGroupCOA.DataBind()
        ddlGroupCOA.SelectedIndex = intSelectedIndex
    End Sub

	Sub BindSubBlok(ByVal pv_strGroupCoa As String)
        Dim strOpCd As String = "GL_CLSSETUP_SUBBLOCK_LIST_GET"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer = 0

        strParamName = "STRSEARCH"
        strParamValue = "and sub.BlkCode = '" & pv_strGroupCoa  & "' and sub.LocCode = '" & strlocation & "' and sub.Status = '1'  order by sub.SubBlkCode asc" 

		
        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objAccDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=FA_TRX_ASSETADD_ACCCODE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objAccDs.Tables(0).Rows.Count - 1 
		    objAccDs.Tables(0).Rows(intCnt).Item("Description") = trim(objAccDs.Tables(0).Rows(intCnt).Item("BlkCode")) & " (" & objAccDs.Tables(0).Rows(intCnt).Item("Description").Trim() & ")"
        Next
		
		
        dr = objAccDs.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("Description") = "Pilih Blok"
        objAccDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlSubBlok.DataSource = objAccDs.Tables(0)
        ddlSubBlok.DataValueField = "BlkCode"
        ddlSubBlok.DataTextField = "Description"
        ddlSubBlok.DataBind()
        
    End Sub

	Protected Sub ddlGroupCOA_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
		dbBudget_OnLoad()
        BindSubBlok(ddlGroupCOA.SelectedItem.Value.Trim())
    End Sub

	Sub dgBudget_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) 
        If e.Item.ItemType = ListItemType.Header Then
            Dim dgItem As DataGridItem
            Dim dgCell As TableCell
            dgItem = New DataGridItem(0, 0, ListItemType.Header)

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = ""
            dgCell.HorizontalAlign = HorizontalAlign.Left
			
			dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "Blok"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "Januari"
            dgCell.HorizontalAlign = HorizontalAlign.Center

			dgCell = New TableCell()
            dgCell.ColumnSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "Februari"
            dgCell.HorizontalAlign = HorizontalAlign.Center

			dgCell = New TableCell()
            dgCell.ColumnSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "Maret"
            dgCell.HorizontalAlign = HorizontalAlign.Center

			dgCell = New TableCell()
            dgCell.ColumnSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "April"
            dgCell.HorizontalAlign = HorizontalAlign.Center

			dgCell = New TableCell()
            dgCell.ColumnSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "Mei"
            dgCell.HorizontalAlign = HorizontalAlign.Center

			dgCell = New TableCell()
            dgCell.ColumnSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "Juni"
            dgCell.HorizontalAlign = HorizontalAlign.Center

			dgCell = New TableCell()
            dgCell.ColumnSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "Juli"
            dgCell.HorizontalAlign = HorizontalAlign.Center

			dgCell = New TableCell()
            dgCell.ColumnSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "Agustus"
            dgCell.HorizontalAlign = HorizontalAlign.Center

			dgCell = New TableCell()
            dgCell.ColumnSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "September"
            dgCell.HorizontalAlign = HorizontalAlign.Center

			dgCell = New TableCell()
            dgCell.ColumnSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "Oktober"
            dgCell.HorizontalAlign = HorizontalAlign.Center

			dgCell = New TableCell()
            dgCell.ColumnSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "November"
            dgCell.HorizontalAlign = HorizontalAlign.Center

			dgCell = New TableCell()
            dgCell.ColumnSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "Desember"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgBudget.Controls(0).Controls.AddAt(0, dgItem)
        End If
    End Sub

    Sub dgBudget_BindGrid(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) 
        If e.Item.ItemType = ListItemType.Header Then
            Dim dgItem As DataGridItem

            dgItem = New DataGridItem(0, 0, ListItemType.Header)

            e.Item.Cells(0).Visible = False
            e.Item.Cells(1).Visible = False
        End If
		
		 If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
		 Dim Updbutton As LinkButton
		 Dim lbl As Label
		 
		 lbl = CType(e.Item.FindControl("dgBudget_blok"), Label)
		 
		 Updbutton = CType(e.Item.FindControl("Delete"), LinkButton)
         Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete blok " & lbl.Text.Trim() & " ');"
			
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue'")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")

            End If
			
		 End If
    End Sub
	
	 Protected Function dgBudget_Reload() As DataSet
        'load data
        Dim strOpCd_EmpDiv As String = "GL_CLSTRX_BUDGETPROD_ESTATE_GETLIST"
        Dim strParamName As String
        Dim strParamValue As String
        Dim objEmpCodeDs As New Object()
        Dim sc As String 
        Dim intErrNo As Integer

        strParamName = "TT|YR|LOC"
        strParamValue = ddlGroupCOA.SelectedItem.Value.Trim() & "|" & _
						txtYearBudget.Text.Trim() & "|" & _
                        strLocation 
                        
        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSTRX_BUDGETPROD_ESTATE_GETLIST&errmesg=" & Exp.Message & "&redirect=")
        End Try


        Return objEmpCodeDs

    End Function
	
	Sub dbBudget_OnLoad()
        dgBudget.EditItemIndex = -1
        dgBudget.DataSource = dgBudget_Reload()
        dgBudget.DataBind()
    End Sub
	
	Sub btnAdd_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
		Dim strOpCode As String = "GL_CLSTRX_BUDGETPROD_ESTATE_ADD"
        Dim intErrNo As Integer
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim objRslSet As New DataSet()

		if ddlGroupCOA.SelectedValue.trim() = ""  then 
			lblValueError.Visible = True
			lblValueError.text = "Silakan pilih tahun tanam " 
			exit sub
		end if
		
		if ddlSubBlok.SelectedValue.trim() = ""  then 
			lblValueError.Visible = True
			lblValueError.text = "Silakan pilih blok " 
			exit sub
		end if
		
        if ddlKomoditi.SelectedValue.trim() = ""  then 
			lblValueError.Visible = True
			lblValueError.text = "Silakan pilih komoditi " 
			exit sub
		end if
		

        strParamName = "AccYear|GROUPCOA|CODESUBBLOK|LocCode|UpdateId|Komoditi"


        strParamValue = txtYearBudget.Text & "|" &  ddlGroupCOA.SelectedValue.trim() & "|" & ddlSubBlok.SelectedValue.trim() & "|" & strLocation & "|" & _
						strUserId & "|" & ddlKomoditi.SelectedValue.trim() 

        Try


            intErrNo = objGLTrx.mtdGetDataCommon(strOpCode, _
                                                   strParamName, _
                                                   strParamValue, objRslSet)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_BUDGET_SAVE&errmesg=" & lblErrMessage.Text & "&redirect=gl/trx/GL_trx_Budget_list")

        Finally

        dbBudget_OnLoad()
        End try

    End Sub
	
	Sub btnSave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)

        Dim strOpCode As String = "GL_CLSTRX_BUDGETPROD_ESTATE_SAVE"
        Dim intErrNo As Integer
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        
		Dim i As Integer 
		Dim hid_trx As HiddenField 
		Dim B01T As TextBox 
		Dim R01T As TextBox 
		Dim B02T As TextBox 
		Dim R02T As TextBox 
		Dim B03T As TextBox 
		Dim R03T As TextBox 
		Dim B04T As TextBox 
		Dim R04T As TextBox 
		Dim B05T As TextBox 
		Dim R05T As TextBox 
		Dim B06T As TextBox 
		Dim R06T As TextBox 
		Dim B07T As TextBox 
		Dim R07T As TextBox 
		Dim B08T As TextBox 
		Dim R08T As TextBox 
		Dim B09T As TextBox 
		Dim R09T As TextBox 
		Dim B10T As TextBox 
		Dim R10T As TextBox 
		Dim B11T As TextBox 
		Dim R11T As TextBox 
		Dim B12T As TextBox 
		Dim R12T As TextBox 
		
		For i = 0 To dgBudget.Items.Count - 1
		hid_trx = dgBudget.Items.Item(i).FindControl("dgBudget_hid_trxid") 
		 B01T  = dgBudget.Items.Item(i).FindControl("dgBudget_B01") 
		 R01T  = dgBudget.Items.Item(i).FindControl("dgBudget_R01")
		 B02T  = dgBudget.Items.Item(i).FindControl("dgBudget_B02")
		 R02T  = dgBudget.Items.Item(i).FindControl("dgBudget_R02")
		 B03T  = dgBudget.Items.Item(i).FindControl("dgBudget_B03")
		 R03T  = dgBudget.Items.Item(i).FindControl("dgBudget_R03")
		 B04T  = dgBudget.Items.Item(i).FindControl("dgBudget_B04")
		 R04T  = dgBudget.Items.Item(i).FindControl("dgBudget_R04")
		 B05T  = dgBudget.Items.Item(i).FindControl("dgBudget_B05")
		 R05T  = dgBudget.Items.Item(i).FindControl("dgBudget_R05")
		 B06T  = dgBudget.Items.Item(i).FindControl("dgBudget_B06")
		 R06T  = dgBudget.Items.Item(i).FindControl("dgBudget_R06")
		 B07T  = dgBudget.Items.Item(i).FindControl("dgBudget_B07")
		 R07T  = dgBudget.Items.Item(i).FindControl("dgBudget_R07")
		 B08T  = dgBudget.Items.Item(i).FindControl("dgBudget_B08")
		 R08T  = dgBudget.Items.Item(i).FindControl("dgBudget_R08")
		 B09T  = dgBudget.Items.Item(i).FindControl("dgBudget_B09")
		 R09T  = dgBudget.Items.Item(i).FindControl("dgBudget_R09")
		 B10T  = dgBudget.Items.Item(i).FindControl("dgBudget_B10")
		 R10T  = dgBudget.Items.Item(i).FindControl("dgBudget_R10")
		 B11T  = dgBudget.Items.Item(i).FindControl("dgBudget_B11")
		 R11T  = dgBudget.Items.Item(i).FindControl("dgBudget_R11")
		 B12T  = dgBudget.Items.Item(i).FindControl("dgBudget_B12")
		 R12T  = dgBudget.Items.Item(i).FindControl("dgBudget_R12")
		 
    	strParamName = "TRXID|B01T|R01T|B02T|R02T|B03T|R03T|B04T|R04T|B05T|R05T|B06T|R06T|B07T|R07T|B08T|R08T|B09T" & _
                       "|R09T|B10T|R10T|B11T|R11T|B12T|R12T|UpdateId"


        strParamValue = hid_trx.Value.trim() & "|" & _ 
						IIf(B01T.Text.Trim() = "","0",B01T.Text.Trim() ) & "|" & IIf(R01T.Text.Trim() = "","0", R01T.Text.Trim()) & "|" & _
						IIf(B02T.Text.Trim() = "","0",B02T.Text.Trim() ) & "|" & IIf(R02T.Text.Trim() = "","0", R02T.Text.Trim()) & "|" & _
						IIf(B03T.Text.Trim() = "","0",B03T.Text.Trim() ) & "|" & IIf(R03T.Text.Trim() = "","0", R03T.Text.Trim()) & "|" & _
                        IIf(B04T.Text.Trim() = "","0",B04T.Text.Trim() ) & "|" & IIf(R04T.Text.Trim() = "","0", R04T.Text.Trim()) & "|" & _
						IIf(B05T.Text.Trim() = "","0",B05T.Text.Trim() ) & "|" & IIf(R05T.Text.Trim() = "","0", R05T.Text.Trim()) & "|" & _
						IIf(B06T.Text.Trim() = "","0",B06T.Text.Trim() ) & "|" & IIf(R06T.Text.Trim() = "","0", R06T.Text.Trim()) & "|" & _
						IIf(B07T.Text.Trim() = "","0",B07T.Text.Trim() ) & "|" & IIf(R07T.Text.Trim() = "","0", R07T.Text.Trim()) & "|" & _
						IIf(B08T.Text.Trim() = "","0",B08T.Text.Trim() ) & "|" & IIf(R08T.Text.Trim() = "","0", R08T.Text.Trim()) & "|" & _
                        IIf(B09T.Text.Trim() = "","0",B09T.Text.Trim() ) & "|" & IIf(R09T.Text.Trim() = "","0", R09T.Text.Trim()) & "|" & _
						IIf(B10T.Text.Trim() = "","0",B10T.Text.Trim() ) & "|" & IIf(R10T.Text.Trim() = "","0", R10T.Text.Trim()) & "|" & _
                        IIf(B11T.Text.Trim() = "","0",B11T.Text.Trim() ) & "|" & IIf(R11T.Text.Trim() = "","0", R11T.Text.Trim()) & "|" & _
						IIf(B12T.Text.Trim() = "","0",B12T.Text.Trim() ) & "|" & IIf(R12T.Text.Trim() = "","0", R12T.Text.Trim() ) & "|" & _
						strUserId 

		
			Try
				intErrNo = objGLTrx.mtdInsertDataCommon(strOpCode, strParamName, strParamValue)
			Catch Exp As System.Exception
				Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSTRX_BUDGETPROD_ESTATE_SAVE&errmesg=" & Exp.Message & "&redirect=")
			End Try
		
		Next
		dbBudget_OnLoad()
    End Sub

	Sub dgBudget_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
		Dim strOpCode As String = "GL_CLSTRX_BUDGETPROD_ESTATE_DEL"
        Dim intErrNo As Integer
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
		Dim hid_trxid As HiddenField = CType(E.Item.FindControl("dgBudget_hid_trxid"), HiddenField)
		
		strParamName = "TRXID"
		strParamValue = hid_trxid.value.trim() 
			Try
				intErrNo = objGLTrx.mtdInsertDataCommon(strOpCode, strParamName, strParamValue)
			Catch Exp As System.Exception
				Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSTRX_BUDGETPROD_ESTATE_DEL&errmesg=" & Exp.Message & "&redirect=")
			End Try
	
		dbBudget_OnLoad()
    End Sub
	
    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
		Response.Redirect("GL_trx_BudgetProd_Estate_list.aspx")
    End Sub
	
End Class
