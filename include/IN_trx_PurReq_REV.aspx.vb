 
Imports System
Imports System.Data
Imports System.Collections 
Imports System.Data.SqlClient
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic


Public Class IN_PurReq_REV : Inherits Page

    Protected WithEvents dgPRListing As DataGrid
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents StatusList As DropDownList
    Protected WithEvents srchStatusList As DropDownList
    Protected WithEvents srchPRTypeList As DropDownList
    Protected WithEvents srchPRLevelList As DropDownList
    Protected WithEvents SortExpression As Label
    Protected WithEvents sortcol As Label
    Protected WithEvents srchPRID As TextBox
    Protected WithEvents srchUpdateBy As TextBox
    Protected WithEvents TotalAmount As Label
    Protected WithEvents Stock As ImageButton
    Protected WithEvents DC As ImageButton
    Protected WithEvents WS As ImageButton
    Protected WithEvents FA As ImageButton
    Protected WithEvents NU As ImageButton
    Protected WithEvents ibPrint As ImageButton
    Protected WithEvents srchStatusLnList As DropDownList
    Protected WithEvents srchApprovedBy As DropDownList
    Protected WithEvents lstAccMonth As DropDownList
    Protected WithEvents lstAccYear As DropDownList
    Protected WithEvents lblSearch As Label

    Protected WithEvents srchItem As TextBox
	Protected WithEvents srchReqSatus As DropDownList

    Protected objIN As New agri.IN.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objAdminLoc As New agri.Admin.clsLoc()
    Dim objGLtrx As New agri.GL.ClsTrx()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim strOppCd_GET_PRList As String = "IN_CLSTRX_PURREQ_LIST_GET"
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intINAR As Integer

    Dim objDataSet As New DataSet()
    Dim intCnt As Integer
    Dim intErrNo As Integer
    Dim intPRCount As Integer
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLocLevel As String
    Dim intLevel As Integer
    Dim strSelAccMonth As String
    Dim strSelAccYear As String


    Dim BtnApproved As Button
    Dim BtnCancel As Button
    Dim APPButton As LinkButton
    Dim UpdButton As LinkButton
    Dim CancelButton As LinkButton
    Dim SaveButton As LinkButton

#Region "COMPONENT"

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intINAR = Session("SS_INAR")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        strLocLevel = Session("SS_LOCLEVEL")
        intLevel = Session("SS_USRLEVEL")
        strSelAccMonth = Session("SS_SELACCMONTH")
        strSelAccYear = Session("SS_SELACCYEAR")

        Stock.CommandName = objIN.EnumPurReqDocType.StockPR
        DC.CommandName = objIN.EnumPurReqDocType.DirectChargePR
        WS.CommandName = objIN.EnumPurReqDocType.WorkshopPR
        FA.CommandName = objIN.EnumPurReqDocType.FixedAssetPR

        NU.CommandName = objIN.EnumPurReqDocType.NurseryPR

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INPurchaseRequest), intINAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else

            If SortExpression.Text = "" Then
                SortExpression.Text = "PR.PRID"
                sortcol.Text = "ASC"
            End If

            'to avoid double click, on aspx add this : UseSubmitBehavior="false"
            Stock.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Stock).ToString())
            DC.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(DC).ToString())
            WS.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(WS).ToString())
            FA.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(FA).ToString())
            NU.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(NU).ToString())
            'ibPrint.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(ibPrint).ToString())

            If Not Page.IsPostBack Then
                If Session("SS_FILTERPERIOD") = "0" Then
                    lstAccMonth.SelectedValue = strAccMonth
                    BindAccYear(strAccYear)
                Else
                    lstAccMonth.SelectedValue = 0
                    BindAccYear(strSelAccYear)
                End If

                lblSearch.Text = ""
                srchApprovedBy.SelectedIndex = IIf(intLevel = 0, 1, intLevel)
                BindSearchList()
                BindPRTypeList()
                BindPRLevelList()
                BindGrid()
                BindPageList()

                'CheckStatus()
            End If
        End If
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgPRListing.CurrentPageIndex = 0
        dgPRListing.EditItemIndex = -1
        BindSearch()
        CheckStatus()
        BindPageList()
        lblSearch.Text = "CARI"
    End Sub

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgPRListing.CurrentPageIndex = 0
            Case "prev"
                dgPRListing.CurrentPageIndex = _
                    Math.Max(0, dgPRListing.CurrentPageIndex - 1)
            Case "next"
                dgPRListing.CurrentPageIndex = _
                    Math.Min(dgPRListing.PageCount - 1, dgPRListing.CurrentPageIndex + 1)
            Case "last"
                dgPRListing.CurrentPageIndex = dgPRListing.PageCount - 1
        End Select
        lstDropList.SelectedIndex = dgPRListing.CurrentPageIndex

        If lblSearch.Text = "" Then
            BindGrid()
        Else
            BindSearch()
        End If

        CheckStatus()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgPRListing.CurrentPageIndex = lstDropList.SelectedIndex
            If lblSearch.Text = "" Then
                BindGrid()
            Else
                BindSearch()
            End If
            CheckStatus()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        Try
            dgPRListing.CurrentPageIndex = e.NewPageIndex

            If lblSearch.Text = "" Then
                BindGrid()
            Else
                BindSearch()
            End If

            CheckStatus()
        Catch ex As Exception
            Exit Sub
        End Try
        
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        sortcol.Text = IIf(sortcol.Text = "ASC", "DESC", "ASC")
        dgPRListing.CurrentPageIndex = lstDropList.SelectedIndex
        If lblSearch.Text = "" Then
            BindGrid()
        Else
            BindSearch()
        End If
        CheckStatus()
    End Sub

    Sub CheckStatus()
        'Dim strOppCd_GET_PRLnList As String = "IN_CLSTRX_PURREQLN_LIST_GET"

        'Dim strParamTemp2 As String
        'Dim strParamTemp3 As String
        'Dim strPRStatus As String
        'Dim objPRDs As DataSet
        'Dim objPRLnDs As DataSet
        'Dim DelButton As LinkButton
        'Dim lblPRID As Label
        'Dim strPRID As String
        'Dim strPRIDTemp As String
        'Dim strQtyRcv As String

        'For intCnt = 0 To dgPRListing.Items.Count - 1
        '    lblPRID = dgPRListing.Items.Item(intCnt).FindControl("PRID")
        '    strPRID = lblPRID.Text
        '    strParamTemp2 = "And PR.PRID = '" & Trim(strPRID) & "'|" & " "
        '    Try
        '        intErrNo = objIN.mtdGetPurchaseRequest(strOppCd_GET_PRList, _
        '                                               strParamTemp2, _
        '                                               objIN.EnumPurReqDocType.StockPR, _
        '                                               strAccMonth, _
        '                                               strAccYear, _
        '                                               Trim(strLocation), _
        '                                               objPRDs)
        '    Catch Exp As System.Exception
        '        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PURREQ_LIST_GET_CHECK_STATUS&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        '    End Try

        '    If objPRDs.Tables(0).Rows.Count > 0 Then
        '        strPRIDTemp = Trim(objPRDs.Tables(0).Rows(0).Item("PRID"))
        '        strPRStatus = Trim(objPRDs.Tables(0).Rows(0).Item("Status"))
        '    End If

        '    strParamTemp3 = strPRIDTemp & "|" & "PRLn.PRID"
        '    Try
        '        intErrNo = objIN.mtdGetPRLnList(strOppCd_GET_PRLnList, _
        '                                        strCompany, _
        '                                        Trim(strLocation), _
        '                                        strUserId, _
        '                                        strAccMonth, _
        '                                        strAccYear, _
        '                                        strParamTemp3, _
        '                                        objPRLnDs)
        '    Catch Exp As System.Exception
        '        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PURREQLN_LIST_GET_CHECK_STATUS&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        '    End Try

        '    If objPRLnDs.Tables(0).Rows.Count > 0 Then
        '        strQtyRcv = objPRLnDs.Tables(0).Rows(0).Item("QtyRcv")
        '        If strPRStatus = objIN.EnumPurReqStatus.Active And strQtyRcv <> 0 Then
        '            DelButton = dgPRListing.Items.Item(intCnt).FindControl("Delete")
        '            DelButton.Visible = False
        '        ElseIf strPRStatus = objIN.EnumPurReqStatus.Active And strQtyRcv = "" Then
        '            DelButton = dgPRListing.Items.Item(intCnt).FindControl("Delete")
        '            DelButton.Visible = False
        '        ElseIf strPRStatus = objIN.EnumPurReqStatus.Confirmed Then
        '            DelButton = dgPRListing.Items.Item(intCnt).FindControl("Delete")
        '            DelButton.Visible = False
        '        ElseIf strPRStatus = objIN.EnumPurReqStatus.Deleted Then
        '            DelButton = dgPRListing.Items.Item(intCnt).FindControl("Delete")
        '            DelButton.Visible = False
        '            If lstAccMonth.SelectedItem.Value >= Session("SS_INACCMONTH") Then
        '                DelButton = dgPRListing.Items.Item(intCnt).FindControl("Undelete")
        '                DelButton.Visible = True
        '            Else
        '                DelButton = dgPRListing.Items.Item(intCnt).FindControl("Undelete")
        '                DelButton.Visible = False
        '            End If
        '        ElseIf strPRStatus = objIN.EnumPurReqStatus.Cancelled Then
        '            DelButton = dgPRListing.Items.Item(intCnt).FindControl("Delete")
        '            DelButton.Visible = False
        '        Else
        '            DelButton = dgPRListing.Items.Item(intCnt).FindControl("Delete")
        '            DelButton.Visible = True
        '            DelButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
        '        End If
        '    Else
        '        If strPRStatus = objIN.EnumPurReqStatus.Active Then
        '            DelButton = dgPRListing.Items.Item(intCnt).FindControl("Delete")
        '            DelButton.Visible = True
        '            DelButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
        '        ElseIf strPRStatus = objIN.EnumPurReqStatus.Confirmed Then
        '            DelButton = dgPRListing.Items.Item(intCnt).FindControl("Delete")
        '            DelButton.Visible = False
        '        ElseIf strPRStatus = objIN.EnumPurReqStatus.Deleted Then
        '            DelButton = dgPRListing.Items.Item(intCnt).FindControl("Delete")
        '            DelButton.Visible = False
        '            If lstAccMonth.SelectedItem.Value >= Session("SS_INACCMONTH") Then
        '                DelButton = dgPRListing.Items.Item(intCnt).FindControl("Undelete")
        '                DelButton.Visible = True
        '            Else
        '                DelButton = dgPRListing.Items.Item(intCnt).FindControl("Undelete")
        '                DelButton.Visible = False
        '            End If
        '        ElseIf strPRStatus = objIN.EnumPurReqStatus.Cancelled Then
        '            DelButton = dgPRListing.Items.Item(intCnt).FindControl("Delete")
        '            DelButton.Visible = False
        '        Else
        '            DelButton = dgPRListing.Items.Item(intCnt).FindControl("Delete")
        '            DelButton.Visible = True
        '            DelButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
        '        End If
        '    End If
        'Next intCnt
    End Sub

    
    Sub btnNewStPR_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim Issuetype As String = CType(sender, ImageButton).CommandName
        Response.Redirect("IN_PurReq_Details.aspx?prqtype=" & Issuetype)
    End Sub

    

    Sub Btnrefresh_onClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        BindGrid()
        lblSearch.Text = ""
    End Sub
#End Region

#Region "PROCEDURE"

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

    Sub UserMsgBox(ByVal F As Object, ByVal sMsg As String)
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

    Sub PageConTrol()
		Dim lbl As Label
		
        For intCnt = 0 To dgPRListing.Items.Count - 1
            
            If CType(dgPRListing.Items(intCnt).FindControl("lblStatusln"), Label).Text = 2 Then
                'dgPRListing.Items(intCnt).BackColor = Drawing.Color.red
				dgPRListing.Items.Item(intCnt).Cells(11).BackColor=Drawing.Color.Red
                dgPRListing.Items(intCnt).Font.Italic = True
			Else
				If Cdbl(CType(dgPRListing.Items(intCnt).FindControl("lblQtyOrder"), Label).Text) = Cdbl(CType(dgPRListing.Items(intCnt).FindControl("lblQtyApp"), Label).Text) Then
					dgPRListing.Items.Item(intCnt).Cells(11).BackColor=Drawing.Color.Green
					
					If Cdbl(CType(dgPRListing.Items(intCnt).FindControl("lblQtyOrder"), Label).Text) = Cdbl(CType(dgPRListing.Items(intCnt).FindControl("lblQtyRcv"), Label).Text) Then
						dgPRListing.Items.Item(intCnt).Cells(11).BackColor=Drawing.Color.Blue
					End If
				Else
					dgPRListing.Items.Item(intCnt).Cells(11).BackColor=Drawing.Color.Yellow
				End If
            End If
        Next
    End Sub

	
    Sub BindPageList()

        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgPRListing.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgPRListing.CurrentPageIndex

    End Sub

    Sub BindSearchList()
        srchStatusList.Items.Add(New ListItem(objIN.mtdGetPurReqStatus(objIN.EnumPurReqStatus.All), objIN.EnumPurReqStatus.All))
        srchStatusList.Items.Add(New ListItem(objIN.mtdGetPurReqStatus(objIN.EnumPurReqStatus.Active), objIN.EnumPurReqStatus.Active))
        srchStatusList.Items.Add(New ListItem(objIN.mtdGetPurReqStatus(objIN.EnumPurReqStatus.Cancelled), objIN.EnumPurReqStatus.Cancelled))
        srchStatusList.Items.Add(New ListItem(objIN.mtdGetPurReqStatus(objIN.EnumPurReqStatus.Confirmed), objIN.EnumPurReqStatus.Confirmed))
        srchStatusList.Items.Add(New ListItem(objIN.mtdGetPurReqStatus(objIN.EnumPurReqStatus.Deleted), objIN.EnumPurReqStatus.Deleted))
        srchStatusList.Items.Add(New ListItem("Closed", objIN.EnumPurReqStatus.Fulfilled))

        If intLevel = 0 Then
            srchStatusList.SelectedIndex = 1
        Else
            srchStatusList.SelectedIndex = 6
        End If

        srchStatusLnList.Items.Add(New ListItem("All", "1','2','3','4"))
        srchStatusLnList.Items.Add(New ListItem(objIN.mtdGetPurReqLnStatus(objIN.EnumPurReqLnStatus.Active), objIN.EnumPurReqLnStatus.Active))
        srchStatusLnList.Items.Add(New ListItem(objIN.mtdGetPurReqLnStatus(objIN.EnumPurReqLnStatus.Cancel), objIN.EnumPurReqLnStatus.Cancel))
        srchStatusLnList.Items.Add(New ListItem(objIN.mtdGetPurReqLnStatus(objIN.EnumPurReqLnStatus.Approved), objIN.EnumPurReqLnStatus.Approved))
        srchStatusLnList.Items.Add(New ListItem(objIN.mtdGetPurReqLnStatus(objIN.EnumPurReqLnStatus.Hold), objIN.EnumPurReqLnStatus.Hold))
        srchStatusLnList.SelectedIndex = 0

    End Sub

    Sub BindPRTypeList()
        srchPRTypeList.Items.Add(New ListItem(objIN.mtdGetPRType(objIN.EnumPurReqDocType.All), objIN.EnumPurReqDocType.All))
        srchPRTypeList.Items.Add(New ListItem(objIN.mtdGetPRType(objIN.EnumPurReqDocType.DirectChargePR), objIN.EnumPurReqDocType.DirectChargePR))
        srchPRTypeList.Items.Add(New ListItem(objIN.mtdGetPRType(objIN.EnumPurReqDocType.StockPR), objIN.EnumPurReqDocType.StockPR))
        srchPRTypeList.Items.Add(New ListItem(objIN.mtdGetPRType(objIN.EnumPurReqDocType.WorkshopPR), objIN.EnumPurReqDocType.WorkshopPR))
        srchPRTypeList.Items.Add(New ListItem(objIN.mtdGetPRType(objIN.EnumPurReqDocType.FixedAssetPR), objIN.EnumPurReqDocType.FixedAssetPR))
        srchPRTypeList.Items.Add(New ListItem(objIN.mtdGetPRType(objIN.EnumPurReqDocType.NurseryPR), objIN.EnumPurReqDocType.NurseryPR))

    End Sub

    Sub BindPRLevelList()
        srchPRLevelList.Items.Clear()
        srchPRLevelList.Items.Add(New ListItem(objAdminLoc.mtdGetLocLevel(objAdminLoc.EnumLocLevel.All), objAdminLoc.EnumLocLevel.All))
        srchPRLevelList.Items.Add(New ListItem(objAdminLoc.mtdGetLocLevel(objAdminLoc.EnumLocLevel.Estate), objAdminLoc.EnumLocLevel.Estate))
        srchPRLevelList.Items.Add(New ListItem(objAdminLoc.mtdGetLocLevel(objAdminLoc.EnumLocLevel.Perwakilan), objAdminLoc.EnumLocLevel.Perwakilan))
        srchPRLevelList.Items.Add(New ListItem(objAdminLoc.mtdGetLocLevel(objAdminLoc.EnumLocLevel.HQ), objAdminLoc.EnumLocLevel.HQ))
        srchPRLevelList.Items.Add(New ListItem(objAdminLoc.mtdGetLocLevel(objAdminLoc.EnumLocLevel.Mill), objAdminLoc.EnumLocLevel.Mill))
    End Sub

    Sub BindGrid()

        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadData()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgPRListing.PageSize)

        dgPRListing.DataSource = dsData
        If dgPRListing.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgPRListing.CurrentPageIndex = 0
            Else
                dgPRListing.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgPRListing.DataBind()
        BindPageList()
        PageNo = dgPRListing.CurrentPageIndex + 1
        lblTracker.Text = "Page " & PageNo & " of " & dgPRListing.PageCount
        PageConTrol()
    End Sub

    Sub BindSearch()
        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadSrch()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgPRListing.PageSize)

        dgPRListing.DataSource = dsData
        If dgPRListing.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgPRListing.CurrentPageIndex = 0
            Else
                dgPRListing.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgPRListing.DataBind()
        BindPageList()
        PageNo = dgPRListing.CurrentPageIndex + 1
        lblTracker.Text = "Page " & PageNo & " of " & dgPRListing.PageCount
        PageConTrol()
    End Sub

    Protected Function LoadData() As DataSet
        Dim objItemDs As New Object()
        Dim strOpCode As String = "IN_CLSTRX_PURREQLN_LIST_REVIEW"

        Dim strSearchLocLevel As String = ""
        Dim strLastAccMonth As String
        Dim strParamName As String
        Dim strParamValue As String
        Dim objData As New Object
        Dim StatusLn As String = ""

        'strAccYear = Year(Now())
		strAccYear = lstAccYear.SelectedItem.Value.Trim()
        strAccMonth = Month(Now())
        strLastAccMonth = Val(strAccMonth) - 1

 
        If lstAccMonth.SelectedItem.Value = "0" Then
            strAccMonth = "1','2','3','4','5','6','7','8','9','10','11','12"
        Else
            strAccMonth = lstAccMonth.SelectedItem.Value
        End If

        strParamName = "LOCCODE|ACCYEAR"
        strParamValue = strLocation & "|" & strAccYear

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                objItemDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_ITEM_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        intPRCount = objItemDs.Tables(0).Rows.Count

        Return objItemDs
    End Function

    Protected Function LoadSrch() As DataSet
        Dim objItemDs As New Object()
        Dim strOpCode As String = "IN_CLSTRX_PURREQLN_SEARCH_REVIEW"

        Dim strSearchLocLevel As String = ""
        Dim strLastAccMonth As String
        Dim strParamName As String
        Dim strParamValue As String
        Dim objData As New Object
		Dim strStatusLn As String
		Dim strReqStatus As String

        'strAccYear = Year(Now())
		strAccYear = lstAccYear.SelectedItem.Value.Trim()
        strAccMonth = Month(Now())
        strLastAccMonth = Val(strAccMonth) - 1

        If lstAccMonth.SelectedItem.Value = "0" Then
            strAccMonth = "1','2','3','4','5','6','7','8','9','10','11','12"
        Else
            strAccMonth = lstAccMonth.SelectedItem.Value
        End If
		
		Select Case srchReqSatus.SelectedItem.Value 
			Case 0 
				strReqStatus = ""
			Case 1
				strReqStatus = " AND COALESCE(PRLN.QTYAPP-COALESCE(PO.POQTY,0),0) <> 0 "
			Case 2
				strReqStatus = " AND COALESCE(PRLN.QTYAPP-COALESCE(PO.POQTY,0),0) = 0 "
			Case 3
				strReqStatus = " AND (COALESCE(PO.POQTY,0) <> 0 AND COALESCE(COALESCE(PO.POQTY,0)-COALESCE(PO.GRQTY,0),0) = 0) "
		End Select
		
		

        strParamName = "LOCCODE|STATUS|PRLEVEL|ACCMONTH|ACCYEAR|PRID|STATUSLN|ITEMSEARCH"
        strParamValue = strLocation & "|" & 1 & "|" & intLevel & "|" & strAccMonth & "|" & strAccYear & "|" & srchPRID.Text & "|" & srchStatusLnList.SelectedItem.Value & "|" & _
                        "AND (Itm.ItemCode LIKE '%" & Trim(srchItem.Text) & "%' OR Itm.Description LIKE '%" & Trim(srchItem.Text) & "%' OR PRLn.OtherName LIKE '%" & Trim(srchItem.Text) & "%')" & _
						strReqStatus

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                objItemDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_ITEM_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        intPRCount = objItemDs.Tables(0).Rows.Count

        Return objItemDs
    End Function

#End Region
End Class

