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


Public Class PR_setup_PrmiPanenList_Estate : Inherits Page

    Protected WithEvents dghist As DataGrid
    Protected WithEvents lbDelete As LinkButton
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
  
    Protected WithEvents srcpmonth As DropDownList
    Protected WithEvents srcpyear As DropDownList

    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label

    Protected objHRSetup As New agri.HR.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim ObjOK As New agri.GL.ClsTrx
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intPRAR As Long
    Dim ParamNama As String
    Dim ParamValue As String
    Dim objBankDs As New Object()
	 Dim intLevel As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intPRAR = Session("SS_PRAR")
		intLevel = Session("SS_USRLEVEL")


        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        'ElseIf (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRPaySetup), intPRAR) = False)  Then
        '    Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
        
            If Not Page.IsPostBack Then
			    srcpmonth.SelectedValue = Cint(Session("SS_SELACCMONTH")) 
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
        Dim strOpCd_GET As String = "GL_CLSSETUP_SUBBLOCK_BY_SUBBLOCK_GET_HIST_LIST"
        Dim strSearch As String
        Dim sortitem As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
		Dim ParamNama As String = ""
        Dim ParamValue As String  = ""

       

        ParamNama = "LOC|MN|YR|SRC"
        ParamValue = strLocation & "|" & srcpmonth.selecteditem.value.trim()  & "|" & srcpyear.selecteditem.value.trim() & "|" & txtLastUpdate.Text.Trim() 

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
	
	Public Sub UserMsgBox(ByVal F As Object, ByVal sMsg As String)
        Dim sb As New StringBuilder()
        Dim oFormObject As System.Web.UI.Control = Nothing
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


    Sub dghist_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim Updbutton As LinkButton
        Dim lblTemp As Label
        Dim ddlTemp As DropDownList
		
		if (intLevel < 2)
		    UserMsgBox(Me, "Access Denied")
			Exit Sub
		End if

        dghist.EditItemIndex = CInt(E.Item.ItemIndex)
		BindGrid()
        If CInt(E.Item.ItemIndex) >= dghist.Items.Count Then
            dghist.EditItemIndex = -1
            Exit Sub
        End If

    End Sub

    Sub dghist_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        dghist.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub dghist_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOppCd As String = "GL_CLSSETUP_SUBBLOCK_BY_SUBBLOCK_UPD_HIST"
        Dim ParamNama As String
        Dim ParamValue As String
        Dim intErrNo As Integer

        Dim EditText As TextBox
        Dim list As DropDownList

        Dim strBLK As String
        Dim strMN As String
        Dim strYR As String
        Dim strTA As String
        Dim strST As String
        Dim strTS As String
		Dim strTB As String
		
		Dim strBJR As String
		Dim strBSS As String
		Dim strOB As String
		Dim strOB2 As String
		
		Dim strP1K As String
		Dim strP1R As String
		Dim strP2K As String
		Dim strP2R As String
		Dim strPRP As String
		
		Dim strANT1 As String
		Dim strANT2 As String
		Dim strANT3 As String
		Dim strANT4 As String
		Dim strANT5 As String
		
		Dim strsty As String
		
		Dim strTSb As String
		Dim strBSSHA As String
		
		EditText = E.Item.FindControl("id1")
		strBLK = EditText.Text.Trim()
		
		EditText = E.Item.FindControl("id2")
		strMN= EditText.Text.Trim()
		
		EditText = E.Item.FindControl("id3")
		strYR = EditText.Text.Trim()
		
		EditText = E.Item.FindControl("id4")
		strBJR = EditText.Text.Trim()
		
		EditText = E.Item.FindControl("id5")
		strBSS = EditText.Text.Trim()
		
		EditText = E.Item.FindControl("id6")
		strOB = EditText.Text.Trim()
		
		EditText = E.Item.FindControl("id9")
		strTA = EditText.Text.Trim()
		
		EditText = E.Item.FindControl("id10")
		strTS = EditText.Text.Trim()
		
		EditText = E.Item.FindControl("id11")
		strST = EditText.Text.Trim()
		
		EditText = E.Item.FindControl("id12")
		strTB = EditText.Text.Trim()
		
		EditText = E.Item.FindControl("id13")
		strOB2 = EditText.Text.Trim()
		
		EditText = E.Item.FindControl("id14")
		strP1K = EditText.Text.Trim()
		
		EditText = E.Item.FindControl("id15")
		strP1R = EditText.Text.Trim()
		
		EditText = E.Item.FindControl("id16")
		strP2K = EditText.Text.Trim()
		
		EditText = E.Item.FindControl("id17")
		strP2R = EditText.Text.Trim()
		
		EditText = E.Item.FindControl("id18")
		strPRP = EditText.Text.Trim()
		
		EditText = E.Item.FindControl("idT1")
		strANT1 = EditText.Text.Trim()
		EditText = E.Item.FindControl("idT2")
		strANT2 = EditText.Text.Trim()
		EditText = E.Item.FindControl("idT3")
		strANT3 = EditText.Text.Trim()
		EditText = E.Item.FindControl("idT4")
		strANT4 = EditText.Text.Trim()
		EditText = E.Item.FindControl("idT5")
		strANT5 = EditText.Text.Trim()
		EditText = E.Item.FindControl("ddlapl")
		strsty = EditText.Text.Trim()
		
		EditText = E.Item.FindControl("id20")
		strTSb = EditText.Text.Trim()
		
		EditText = E.Item.FindControl("id5b")
		strBSSHA = EditText.Text.Trim()
		
        ParamNama = "TA|ST|TS|TB|BLK|MN|YR|LOC|UI|BJR|BSS|OB|OB2|P1K|P1R|P2K|P2R|PRP|ANT1|ANT2|ANT3|ANT4|ANT5|STY|PS|BSSHA"
        ParamValue = strTA & "|" & strST & "|" & strTS & "|" & _
                     strTB & "|" & strBLK & "|" & strMN & "|" & _
					 strYR & "|" & strlocation & "|" & strUserId & "|" & _
					 strBJR & "|" & strBSS & "|" & strOB & "|" & strOB2 & "|" & _
					 strP1K & "|" & strP1R & "|" & strP2K & "|" & strP2R & "|" & strPRP & "|" & _
					 strANT1 & "|" & strANT2 & "|" & strANT3 & "|" & strANT4 & "|" & strANT5 & "|" & strsty & "|" & strTSb & "|" & strBSSHA

        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOppCd, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_SUBBLOCK_BY_SUBBLOCK_UPD_HIST&errmesg=" & Exp.Message & "&redirect=")
        End Try
	   dghist.EditItemIndex = -1
       BindGrid()
    End Sub
	    

End Class
