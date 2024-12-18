

Imports System
Imports System.Data
Imports System.Collections
Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic


Public Class PU_trx_RPHList : Inherits Page

    Protected WithEvents dgRPHList As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents txtRPHID As TextBox
    Protected WithEvents ddlRPHType As DropDownList
    Protected WithEvents txtPRID As TextBox
    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblErrQtyReceive As Label
    Protected WithEvents NewFAPOBtn As ImageButton
    Protected WithEvents lstAccMonth As DropDownList
    Protected WithEvents lstAccYear As DropDownList

    Protected objPU As New agri.PU.clsTrx()
    Protected objPUSetup As New agri.PU.clsSetup()
    Protected objINSetup As New agri.IN.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objAdm As New agri.Admin.clsLoc()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGLtrx As New agri.GL.ClsTrx()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intPUAR As Integer
    Dim intFAAR As Integer
    Dim intLevel As Integer

    Dim objRPHDs As New Object()
    Dim objRPHLnDs As New Object()

#Region "TOOLS & COMPONENT"

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        'strAccMonth = Session("SS_PUACCMONTH")
        'strAccYear = Session("SS_PUACCYEAR")
        strAccMonth = Session("SS_SELACCMONTH")
        strAccYear = Session("SS_SELACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intPUAR = Session("SS_PUAR")
        intFAAR = Session("SS_FAAR")
        intLevel = Session("SS_USRLEVEL")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PURPH), intPUAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else

            lblErrQtyReceive.Visible = False
            If SortExpression.Text = "" Then
                SortExpression.Text = "A.RPHID"
            End If
            If Not Page.IsPostBack Then
                lstAccMonth.SelectedValue = strAccMonth
                BindAccYear(strAccYear)
                BindRPHType()
                BindGrid()
                BindPageList()

            End If
        End If
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgRPHList.CurrentPageIndex = 0
        dgRPHList.EditItemIndex = -1
        BindGrid()
        BindPageList()
    End Sub

    Sub BindRPHType()
        ddlRPHType.Items.Clear()
        ddlRPHType.Items.Add(New ListItem(objPU.mtdGetRPHType(objPU.EnumRPHType.All), objPU.EnumRPHType.All))
        ddlRPHType.Items.Add(New ListItem(objPU.mtdGetRPHType(objPU.EnumRPHType.DirectCharge), objPU.EnumRPHType.DirectCharge))
        ddlRPHType.Items.Add(New ListItem(objPU.mtdGetRPHType(objPU.EnumRPHType.FixedAsset), objPU.EnumRPHType.FixedAsset))
        ddlRPHType.Items.Add(New ListItem("Stock / Workshop", objPU.EnumRPHType.Stock)) 
        ddlRPHType.Items.Add(New ListItem(objPU.mtdGetRPHType(objPU.EnumRPHType.Nursery), objPU.EnumRPHType.Nursery))             
    End Sub

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgRPHList.CurrentPageIndex = 0
            Case "prev"
                dgRPHList.CurrentPageIndex = _
                    Math.Max(0, dgRPHList.CurrentPageIndex - 1)
            Case "next"
                dgRPHList.CurrentPageIndex = _
                    Math.Min(dgRPHList.PageCount - 1, dgRPHList.CurrentPageIndex + 1)
            Case "last"
                dgRPHList.CurrentPageIndex = dgRPHList.PageCount - 1
        End Select
        lstDropList.SelectedIndex = dgRPHList.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgRPHList.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgRPHList.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        dgRPHList.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Function CheckStatus(ByVal strSelectedRPHId) As Double
        Dim strOpCd As String = "PU_CLSTRX_RPH_GET"
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim strParam As String = ""
        Dim strRPHId As String
        Dim dblQtyReceive As Double = 0

        strRPHId = strSelectedRPHId

        strParam = strSelectedRPHId & "|" & strLocation & "||||||A.RPHID||||" & "|" & strAccYear

        Try
            intErrNo = objPU.mtdGetRPH(strOpCd, _
                                      strParam, _
                                      objRPHDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_RPHLN&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/PU_trx_RPHList.aspx")
        End Try


    End Function

    Sub DEDR_Delete(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim strOpCd_AddRPH As String = ""
        Dim strOpCd_UpdRPH As String = "PU_CLSTRX_RPH_UPD"
        Dim objRPHId As New Object()
        Dim strParam As String = ""
        Dim RPHCell As TableCell = e.Item.Cells(0)
        Dim StsCell As TableCell = e.Item.Cells(1)
        Dim strSelectedRPHId As String
        Dim strSelectedStatus As Integer
        Dim intErrNo As Integer
        Dim dblQtyReceive As Double = 0
        Dim Lbl As Label
        Dim strRPHType1 As String


        Dim strOppCd As String = "PU_CLSTRX_PO_MOVEID"
        Dim strOppCd_Back As String = "PU_CLSTRX_PO_BACKID"

        strSelectedRPHId = RPHCell.Text

        Lbl = e.Item.FindControl("lblRPHType")

        Select Case Trim(Lbl.Text)
            Case "Stock / Workshop"
                Lbl.Text = "1"
            Case "Direct Charge"
                Lbl.Text = "2"
            Case "Fixed Asset"
                Lbl.Text = "6"
            Case "Nursery"
                Lbl.Text = "7"
            Case "Stock"
                Lbl.Text = "1"
        End Select


        strSelectedStatus = CInt(StsCell.Text)

        strParam = strSelectedRPHId & "|0||" & Lbl.Text & "|" & strAccMonth & "|" & strAccYear & "|" & objPU.EnumRPHStatus.Deleted & "||||||||||||"

        Try
            intErrNo = objPU.mtdUpdRPH(strOpCd_AddRPH, _
                                       strOpCd_UpdRPH, _
                                       strOppCd, _
                                       strOppCd_Back, _
                                       strCompany, _
                                       strLocation, _
                                       strUserId, _
                                       strParam, _
                                       "RPH", _
                                       objRPHId)


        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_DEL_PO&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/PU_trx_RPHList.aspx")
        End Try

        dgRPHList.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim strOpCd_AddRPH As String = ""
        Dim strOpCd_UpdRPH As String = "PU_CLSTRX_RPH_UPD"
        Dim objRPHId As New Object()
        Dim strParam As String = ""
        Dim RPHCell As TableCell = e.Item.Cells(0)
        Dim StsCell As TableCell = e.Item.Cells(1)
        Dim strSelectedRPHId As String
        Dim strSelectedStatus As Integer
        Dim strStatus As Integer
        Dim intErrNo As Integer
        Dim dblQtyReceive As Double = 0
        Dim strRPHType1 As String

        Dim Lbl As Label

        Dim strOppCd As String = "PU_CLSTRX_PO_MOVEID"
        Dim strOppCd_Back As String = "PU_CLSTRX_PO_BACKID"

        strSelectedRPHId = RPHCell.Text

        Lbl = e.Item.FindControl("lblRPHType")

        Select Case Trim(Lbl.Text)
            Case "Stock / Workshop"
                Lbl.Text = "1"
            Case "Direct Charge"
                Lbl.Text = "2"
            Case "Fixed Asset"
                Lbl.Text = "6"
            Case "Nursery"
                Lbl.Text = "7"
            Case "Stock"
                Lbl.Text = "1"
        End Select

        dblQtyReceive = CheckStatus(strSelectedRPHId)
        If dblQtyReceive = 0 Then
            strSelectedStatus = CInt(StsCell.Text)

            strParam = strSelectedRPHId & "|0||" & Lbl.Text & "|" & strAccMonth & "|" & strAccYear & "|" & objPU.EnumRPHStatus.Active & "||||||||||||"



            Try
                intErrNo = objPU.mtdUpdRPH(strOpCd_AddRPH, _
                                          strOpCd_UpdRPH, _
                                          strOppCd, _
                                          strOppCd_Back, _
                                          strCompany, _
                                          strLocation, _
                                          strUserId, _
                                          strParam, _
                                          "RPH", _
                                          objRPHId)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_DEL_RPH&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/PU_trx_RPHList.aspx")
            End Try

            dgRPHList.EditItemIndex = -1
            BindGrid()
        Else
            lblErrQtyReceive.Visible = True
        End If
    End Sub

    Sub NewINPOBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PU_trx_RPHDet.aspx?RPHType=" & objPU.EnumRPHType.Stock)
    End Sub

    Sub NewDCPOBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PU_trx_RPHDet.aspx?RPHType=" & objPU.EnumRPHType.DirectCharge)
    End Sub

    Sub NewFAPOBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PU_trx_RPHDet.aspx?RPHType=" & objPU.EnumRPHType.FixedAsset)
    End Sub

    Sub NewNUPOBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PU_trx_RPHDet.aspx?RPHType=" & objPU.EnumRPHType.Nursery)
    End Sub
#End Region

#Region "lOCAL & PROCEDURE"
    Function lCDbl(ByVal pcVal As String) As Double
        If IsNumeric(pcVal) Then lCDbl = CDbl(pcVal) Else lCDbl = 0
    End Function

    Sub BindGrid()
        Dim strOpCd As String = "PU_CLSTRX_RPH_GET"
        Dim strSrchRPHId As String
        Dim strSrchRPHType As String
        Dim strSrchPRID As String
        Dim strSrchStatus As String
        Dim strSrchLastUpdate As String
        Dim strSearch As String
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim lbButton As LinkButton
        Dim lbl As Label
        Dim sSQLUser As String
        Dim sSQLKriteria As String

        If lstAccMonth.SelectedItem.Value = "0" Then
            strAccMonth = "1','2','3','4','5','6','7','8','9','10','11','12"
        Else
            strAccMonth = lstAccMonth.SelectedItem.Value
        End If

        'If intLevel <= 1 Then
        '    sSQLUser = "AND a.UserPO='" & strUserId & "'"
        'Else
            sSQLUser = ""
        'End If

        strAccYear = lstAccYear.SelectedItem.Value
        strSrchRPHId = IIf(txtRPHID.Text = "", "", txtRPHID.Text)
        strSrchRPHType = IIf(ddlRPHType.SelectedItem.Value = 0, "", ddlRPHType.SelectedItem.Value)
        strSrchPRID = IIf(txtPRID.Text = "", "", txtPRID.Text)
        strSrchStatus = IIf(ddlStatus.SelectedItem.Value = "0", "1','2','4", ddlStatus.SelectedItem.Value)
        strSrchLastUpdate = IIf(txtLastUpdate.Text = "", "", txtLastUpdate.Text)

        sSQLKriteria = "Where a.LocCode='" & strLocation & _
                        "' AND a.AccYear='" & strAccYear & _
                        "' AND a.AccMonth in ('" & strAccMonth & "')" & _
                        " And a.Status in ('" & strSrchStatus & "')" & sSQLUser

        If txtRPHID.Text.Trim <> "" Then
            sSQLKriteria = sSQLKriteria & "AND a.RPHID LIKE '%" & strSrchRPHId & "%'"
        ElseIf txtPRID.Text.Trim <> "" Then
            sSQLKriteria = sSQLKriteria & "AND b.PRID LIKE '%" & strSrchPRID & "%'"
        ElseIf ddlRPHType.SelectedIndex > 0 Then
            sSQLKriteria = sSQLKriteria & "AND a.RPHType ='" & strSrchRPHType & "'"
        ElseIf txtLastUpdate.Text.Trim <> "" Then
            sSQLKriteria = sSQLKriteria & "AND a.Updateid ='" & strSrchLastUpdate & "'"
        End If

        strParamName = "STRSEARCH"
        strParamValue = sSQLKriteria

        'strParamValue = strSrchRPHId & "|" & _
        '               strLocation & "|" & _
        '               strSrchRPHType & "|" & _
        '               strSrchPRID & "|" & _
        '               "" & "|" & _
        '               strSrchStatus & "|" & _
        '               strSrchLastUpdate & "|" & _
        '               SortExpression.Text & "|" & _
        '               SortCol.Text & "|" & _
        '               strAccYear & "|" & _
        '               strAccMonth

        'Try
        '    intErrNo = objPU.mtdGetRPH(strOpCd, _
        '                              strParam, _
        '                              objRPHDs)
        'Catch Exp As System.Exception
        '    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_RPH&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/PU_trx_POList.aspx")
        'End Try

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, strParamName, strParamValue, objRPHDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_STOCKTRANSFERDIV_DISPLAYLINE_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        For intCnt = 0 To objRPHDs.Tables(0).Rows.Count - 1
            objRPHDs.Tables(0).Rows(intCnt).Item("RPHID") = objRPHDs.Tables(0).Rows(intCnt).Item("RPHId").Trim()
            objRPHDs.Tables(0).Rows(intCnt).Item("RPHType") = objPU.mtdGetRPHType(objRPHDs.Tables(0).Rows(intCnt).Item("RPHType"))
            objRPHDs.Tables(0).Rows(intCnt).Item("UserName") = objRPHDs.Tables(0).Rows(intCnt).Item("UserName").Trim()
        Next

        PageCount = objGlobal.mtdGetPageCount(objRPHDs.Tables(0).Rows.Count, dgRPHList.PageSize)
        dgRPHList.DataSource = objRPHDs
        If dgRPHList.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgRPHList.CurrentPageIndex = 0
            Else
                dgRPHList.CurrentPageIndex = PageCount - 1
            End If
        End If
        dgRPHList.DataBind()
        BindPageList()

        'For intCnt = 0 To dgRPHList.Items.Count - 1
        '    lbl = dgRPHList.Items.Item(intCnt).FindControl("lblStatus")
        '    Select Case CInt(Trim(lbl.Text))
        '        Case objPU.EnumRPHStatus.Active
        '            lbButton = dgRPHList.Items.Item(intCnt).FindControl("lbDelete")
        '            lbButton.Visible = True
        '            lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
        '            lbButton = dgRPHList.Items.Item(intCnt).FindControl("lbUndelete")
        '            lbButton.Visible = False
        '        Case objPU.EnumRPHStatus.Confirmed, objPU.EnumRPHStatus.Cancelled, objPU.EnumRPHStatus.Closed
        '            lbButton = dgRPHList.Items.Item(intCnt).FindControl("lbDelete")
        '            lbButton.Visible = False
        '            lbButton = dgRPHList.Items.Item(intCnt).FindControl("lbUndelete")
        '            lbButton.Visible = False
        '        Case objPU.EnumRPHStatus.Deleted
        '            lbButton = dgRPHList.Items.Item(intCnt).FindControl("lbDelete")
        '            lbButton.Visible = False
        '            If lstAccMonth.SelectedItem.Value >= Session("SS_PUACCMONTH") Then
        '                lbButton = dgRPHList.Items.Item(intCnt).FindControl("lbUndelete")
        '                lbButton.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
        '                lbButton.Visible = True
        '            Else
        '                lbButton = dgRPHList.Items.Item(intCnt).FindControl("lbUndelete")
        '                lbButton.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
        '                lbButton.Visible = False
        '            End If
        '    End Select
        'Next

        PageNo = dgRPHList.CurrentPageIndex + 1
        lblTracker.Text = "Page " & PageNo & " of " & dgRPHList.PageCount
        PageConTrol()
    End Sub

    Sub BindPageList()
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgRPHList.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgRPHList.CurrentPageIndex
    End Sub

    Sub PageConTrol()
        Dim intCnt As Integer
        For intCnt = 0 To dgRPHList.Items.Count - 1
            If lCDbl(CType(dgRPHList.Items(intCnt).FindControl("lblRecItem"), Label).Text) = 0 Then
                dgRPHList.Items(intCnt).ForeColor = Drawing.Color.Red
            End If
        Next
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

    Sub dgLine_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue'")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")
            End If
        End If
    End Sub

#End Region

End Class
