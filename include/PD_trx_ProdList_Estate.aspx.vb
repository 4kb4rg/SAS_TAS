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

Imports agri.HR
Imports agri.GlobalHdl.clsGlobalHdl

Public Class PD_trx_ProdList_Estate : Inherits Page

    Protected WithEvents dgEmpList As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents txtEmpCode As TextBox
    Protected WithEvents txtEmpName As TextBox
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents NewEmpBtn As ImageButton
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblRedirect As Label
   

    Protected WithEvents ddlEmpDiv As DropDownList
	Protected WithEvents ddldivisi As DropDownList
    Protected WithEvents ddlEmpType As DropDownList
    Protected WithEvents ddlEmpMonth As DropDownList
    Protected WithEvents ddlyear As DropDownList

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
    Dim objEmpTypeDs As New Object()
    Dim objEmpDivDs As New Object()
    Dim cnt As integer
	Dim cntjjg As integer


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
	
    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intHRAR = Session("SS_PRAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        'ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxWorkPerformance), intHRAR) = False Then
        '    Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblRedirect.Text = Request.QueryString("redirect")
            If SortExpression.Text = "" Then
                SortExpression.Text = "A.SPBCode"
            End If

            If Not Page.IsPostBack Then
                ddlEmpMonth.SelectedIndex = Cint(Session("SS_SELACCMONTH")) - 1
                BindAccYear(Session("SS_SELACCYEAR"))
                BindMILL()
				BindDivision()
                BindGrid()
            End If
        End If
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
         dgEmpList.EditItemIndex = -1
        BindGrid()
    End Sub

	Sub BindDivision()
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_DIVISICODE_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
		Dim intSelect As Integer


        strParamName = "SEARCH|SORT"
        strParamValue = "AND A.LocCode = '" & strLocation & "' AND A.Status='1'|ORDER By BlkGrpCode"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpDivDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_DIVISICODE_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objEmpDivDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpDivDs.Tables(0).Rows.Count - 1
                objEmpDivDs.Tables(0).Rows(intCnt).Item("BlkGrpCode") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("BlkGrpCode"))
                objEmpDivDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("BlkGrpCode")) & " (" & Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            Next
        End If

        dr = objEmpDivDs.Tables(0).NewRow()
        dr("BlkGrpCode") = " "
        dr("Description") = "All"
        objEmpDivDs.Tables(0).Rows.InsertAt(dr, 0)

        ddldivisi.DataSource = objEmpDivDs.Tables(0)
        ddldivisi.DataTextField = "Description"
        ddldivisi.DataValueField = "BlkGrpCode"
        ddldivisi.DataBind()
        ddldivisi.SelectedIndex = 0

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
            intErrNo = objOk.mtdGetDataCommon(strOpCd, _
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

    Sub BindMILL()
        Dim strOpCd_EmpDiv As String = "PM_CLSSETUP_MILL_LIST_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow

        strParamName = "SEARCHSTR"
        strParamValue = "AND PM.Status='1' ORDER By MillCode"

        Try
            intErrNo = Objok.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpDivDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_DIVISICODE_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objEmpDivDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpDivDs.Tables(0).Rows.Count - 1
                objEmpDivDs.Tables(0).Rows(intCnt).Item("MillCode") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("MillCode"))
                objEmpDivDs.Tables(0).Rows(intCnt).Item("MillDesc") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("MillDesc"))
            Next
        End If

        dr = objEmpDivDs.Tables(0).NewRow()
        dr("MillCode") = ""
        dr("MillDesc") = "All"
        objEmpDivDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlEmpDiv.DataSource = objEmpDivDs.Tables(0)
        ddlEmpDiv.DataTextField = "MillDesc"
        ddlEmpDiv.DataValueField = "MillCode"
        ddlEmpDiv.DataBind()
        ddlEmpDiv.SelectedIndex = 0

    End Sub

    Sub BindGrid()
        Dim intCnt As Integer
        Dim lbButton As LinkButton

        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadData()
		
        'PageCount = objGlobal.mtdGetPageCount(Session("SS_PAGINGCOUNT"), dgEmpList.PageSize)
        'If PageCount < 1 Then
        '    PageCount = 1
        'End If
        'If lblCurrentIndex.Text >= PageCount Then
        '    If PageCount = 0 Then
        '        lblCurrentIndex.Text = 0
        '    Else
        '        lblCurrentIndex.Text = PageCount - 1
        '        dsData = LoadData()
        '    End If
        'End If

        dgEmpList.DataSource = dsData
        dgEmpList.DataBind()
        'lblPageCount.Text = PageCount
        'BindPageList(PageCount)
        'PageNo = lblCurrentIndex.Text + 1

    End Sub

    Protected Function LoadData() As DataSet
        Dim strOpCd_Get As String = "PD_PD_TRX_PROD_ESTATE_GET_LIST"
        Dim strSrchEmpCode As String
        Dim strSrchEmpName As String
        Dim strSrchEmpDiv As String
        Dim strSrchEmpTy As String
        Dim strSrchEmpMonth As String
        Dim strSrchEmpYear As String
		Dim strDiv As String
        Dim strSearch As String

        Dim strParamName As String
        Dim strParamValue As String

        Dim intErrNo As Integer
        Dim intCnt As Integer


        Dim strSortExpression As String


       

        cnt = 0
		cntjjg = 0
        strSrchEmpCode = IIf(txtEmpCode.Text = "", "%", "%" & txtEmpCode.Text & "%")
        strSrchEmpDiv = IIf(ddlEmpDiv.SelectedItem.Value.Trim = "", "%", ddlEmpDiv.SelectedItem.Value.Trim & "%")
        strSrchEmpMonth = ddlEmpMonth.SelectedItem.Value.Trim
        strSrchEmpYear = ddlyear.SelectedItem.Value.Trim
		strDiv = IIf(ddldivisi.SelectedItem.Value.Trim = "", "%", ddldivisi.SelectedItem.Value.Trim & "%")

        strParamName = "AM|AY|LOC|MC|SP|DV"
        strParamValue = strSrchEmpMonth & "|"  & _
		                strSrchEmpYear  & "|"  & _
						strLocation & "|"  & _
						strSrchEmpDiv & "|"  & _
						strSrchEmpCode & "|"  & _
						strDiv 

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_Get, strParamName, strParamValue, objEmpDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PD_PD_TRX_PROD_ESTATE_GET_LIST&errmesg=" & Exp.Message)
        End Try
		
		If objEmpDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpDs.Tables(0).Rows.Count - 1
                objEmpDs.Tables(0).Rows(intCnt).Item("SPBCode") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("SPBCode"))
                objEmpDs.Tables(0).Rows(intCnt).Item("RefNo") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("RefNo"))
				objEmpDs.Tables(0).Rows(intCnt).Item("NoPolisi") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("NoPolisi"))
				objEmpDs.Tables(0).Rows(intCnt).Item("Supir") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("Supir"))
				objEmpDs.Tables(0).Rows(intCnt).Item("MillDesc") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("MillDesc"))
				if Trim(objEmpDs.Tables(0).Rows(intCnt).Item("MillType")) = "99" then 
				 cnt = cnt + objEmpDs.Tables(0).Rows(intCnt).Item("WB_NettoFinal")
				 cntjjg = cntjjg + objEmpDs.Tables(0).Rows(intCnt).Item("TotalJJG")
				 objEmpDs.Tables(0).Rows(intCnt).Item("SPBDate") =objEmpDs.Tables(0).Rows(intCnt).Item("updatedate")
				end if
		     Next
        End If

        Return objEmpDs
    
	End Function

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        BindGrid()
    End Sub

    Sub NewEmpBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PD_trx_ProdDet_Estate.aspx?redirect=OtID")
    End Sub

	Sub KeepRunningSum(ByVal sender As Object, ByVal E As DataGridItemEventArgs)
		If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
			Dim lbl As Label
			Dim lbltot As Label
			
		    lbl = CType(e.Item.FindControl("lblMType"), Label)
			lbltot  = CType(e.Item.FindControl("lblNetto"), Label)
	     
            If lbl.Text.Trim = "99" Then
                E.Item.Font.Bold = True
                E.Item.ForeColor = Drawing.Color.DarkGreen
            End If

            E.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue'")

            If E.Item.ItemType = ListItemType.AlternatingItem Then
                E.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                E.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")
            End If
	
		End If
    
		If E.Item.ItemType = ListItemType.Footer Then
            E.Item.Cells(10).Text = objGlobal.GetIDDecimalSeparator_FreeDigit(cnt, 0)
            E.Item.Cells(11).Text = objGlobal.GetIDDecimalSeparator_FreeDigit(cntjjg, 0)
            E.Item.Font.Bold = True
        End If
    End Sub

	Sub GenBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim ParamNama As String
        Dim ParamValue As String
        Dim strOpCd_Up As String = "PD_PD_TRX_PROD_ESTATELN_GEN_ALL"
		Dim intErrNo As Integer		
		Dim strSrchEmpMonth As String
        Dim strSrchEmpYear As String
			
		strSrchEmpMonth = ddlEmpMonth.SelectedItem.Value.Trim
        strSrchEmpYear = ddlyear.SelectedItem.Value.Trim
		
        ParamNama = "LOC|AM|AY"
        ParamValue = strlocation & "|" & strSrchEmpMonth  & "|" & strSrchEmpYear 
		             
        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOpCd_Up, ParamNama, ParamValue) 
			UserMsgBox(Me, "Generate KG/Blok Sukses") 
        Catch ex As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PD_PD_TRX_PROD_ESTATELN_GEN&errmesg=" & ex.ToString() )
        End Try
	
    End Sub

End Class
