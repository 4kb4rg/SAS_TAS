
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


Public Class PU_DAList : Inherits Page

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblErrOnHand As Label
    Protected WithEvents lblErrOnHold As Label
    Protected WithEvents lblTo As Label
    Protected WithEvents lblCode As Label
    'Protected WithEvents lblLocation As Label
    Protected WithEvents dgDAList As DataGrid
    Protected WithEvents dgAdvAPost As DataGrid

    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents txtDispAdvId As TextBox
    Protected WithEvents ddlDAType As DropDownList
    Protected WithEvents txtSuppCode As TextBox
    Protected WithEvents txtGoodsRcvID As TextBox
    Protected WithEvents txtToLocCode As TextBox
    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents NewINDABtn As ImageButton
    Protected WithEvents NewFADABtn As ImageButton
    Protected WithEvents NewDCDABtn As ImageButton
    Protected WithEvents NewNUDABtn As ImageButton
    Protected WithEvents lstAccMonth As DropDownList
    Protected WithEvents lstAccYear As DropDownList

    Protected objPU As New agri.PU.clsTrx()
    Protected objPUSetup As New agri.PU.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objGLtrx As New agri.GL.ClsTrx()
    Dim objOk As New agri.GL.ClsTrx()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intPUAR As Integer
    Dim intFAAR As Integer

    Dim objDADs As New Object()
    Dim objDALnDs As New Object()
    Dim objLangCapDs As New Object()
    Dim strLocType as String
    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim intLevel As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PUACCMONTH")
        strAccYear = Session("SS_PUACCYEAR")
        strSelAccMonth = Session("SS_SELACCMONTH")
        strSelAccYear = Session("SS_SELACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intPUAR = Session("SS_PUAR")
        intFAAR = Session("SS_FAAR")
        strLocType = Session("SS_LOCTYPE")
        intLevel = Session("SS_USRLEVEL")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUDispatchAdvice), intPUAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            'to avoid double click, on aspx add this : UseSubmitBehavior="false"
            NewINDABtn.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(NewINDABtn).ToString())
            NewDCDABtn.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(NewDCDABtn).ToString())
            NewFADABtn.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(NewFADABtn).ToString())
            NewNUDABtn.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(NewNUDABtn).ToString())

            onload_GetLangCap()
            If SortExpression.Text = "" Then
                SortExpression.Text = "A.DispAdvId"
            End If

            If Not Page.IsPostBack Then
                If Session("SS_FILTERPERIOD") = "0" Then
                    lstAccMonth.SelectedValue = strAccMonth
                    BindAccYear(strAccYear)
                Else
                    lstAccMonth.SelectedValue = strSelAccMonth
                    BindAccYear(strSelAccYear)
                End If

                If intLevel = 0 Then
                    ddlStatus.SelectedIndex = 1
                Else
                    ddlStatus.SelectedIndex = 0
                End If
                BindDAType()
                BindGrid()
                BindPageList()
                ''
                BindGrid_AutoPost()
            End If
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        'lblLocation.Text = GetCaption(objLangCap.EnumLangCap.Location)
        'dgDAList.Columns(6).HeaderText = lblTo.Text & lblLocation.Text & lblCode.Text
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_DALIST_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=pu/setup/PU_trx_DAList.aspx")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

        For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If strLocType = objAdminLoc.EnumLocType.Mill then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                end if
                Exit For
            End If
        Next
    End Function

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgDAList.CurrentPageIndex = 0
        dgDAList.EditItemIndex = -1
        BindGrid()
        BindGrid_AutoPost()
        BindPageList()
    End Sub

    Sub BindDAType()
        ddlDAType.Items.Clear()
        ddlDAType.Items.Add(New ListItem(objPU.mtdGetDAType(objPU.EnumDAType.All), objPU.EnumDAType.All))
        ddlDAType.Items.Add(New ListItem("Stock / Workshop", objPU.EnumDAType.Stock)) 
        ddlDAType.Items.Add(New ListItem(objPU.mtdGetDAType(objPU.EnumDAType.DirectCharge), objPU.EnumDAType.DirectCharge))
        ddlDAType.Items.Add(New ListItem(objPU.mtdGetDAType(objPU.EnumDAType.FixedAsset), objPU.EnumDAType.FixedAsset))
        ddlDAType.Items.Add(New ListItem(objPU.mtdGetDAType(objPU.EnumDAType.Nursery), objPU.EnumDAType.Nursery))
        
    End Sub

    Sub BindGrid()
        Dim strOpCd As String = "PU_CLSTRX_DA_GET"
        Dim strSrchDispAdvId As String
        Dim strSrchDAType As String
        Dim strSrchSuppCode As String
        Dim strSrchGRID As String
        Dim strSrchToLocCode As String
        Dim strSrchStatus As String
        Dim strSrchLastUpdate As String
        Dim strSearch As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim lbButton As LinkButton
        Dim lbl As Label

        If lstAccMonth.SelectedItem.Value = "0" Then
            strAccMonth = "1','2','3','4','5','6','7','8','9','10','11','12"
        Else
            strAccMonth = lstAccMonth.SelectedItem.Value
        End If

        strAccYear = lstAccYear.SelectedItem.Value
        strSrchDispAdvId = IIf(txtDispAdvId.Text = "", "", txtDispAdvId.Text)
        strSrchDAType = IIf(ddlDAType.SelectedItem.Value = 0, "", ddlDAType.SelectedItem.Value)
        strSrchSuppCode = IIf(txtSuppCode.Text = "", "", txtSuppCode.Text)
        strSrchGRID = IIf(txtGoodsRcvID.Text = "", "", txtGoodsRcvID.Text)
        strSrchToLocCode = IIf(txtToLocCode.Text = "", "", txtToLocCode.Text)
        strSrchStatus = IIf(ddlStatus.SelectedItem.Value = "0", "", ddlStatus.SelectedItem.Value)
        strSrchLastUpdate = IIf(txtLastUpdate.Text = "", "", txtLastUpdate.Text)

        strParam = strSrchDispAdvId & "|" & _
                      strLocation & "|" & _
                      strSrchDAType & "|" & _
                      strSrchGRID & "|" & _
                      strSrchToLocCode & "|" & _
                      strSrchStatus & "|" & _
                      strSrchLastUpdate & "|" & _
                      SortExpression.Text & "|" & _
                      SortCol.Text & "|" & _
                      strSrchSuppCode & "|" & _
                      strAccMonth & "|" & _
                      strAccYear

        Try
            intErrNo = objPU.mtdGetDA(strOpCd, _
                                      strParam, _
                                      objDADs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_GET_DA&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/PU_trx_DAList.aspx")
        End Try

        For intCnt = 0 To objDADs.Tables(0).Rows.Count - 1
            objDADs.Tables(0).Rows(intCnt).Item("DispAdvId") = Trim(objDADs.Tables(0).Rows(intCnt).Item("DispAdvId"))
            If objDADs.Tables(0).Rows(intCnt).Item("DispAdvType").Trim() = objPU.EnumDAType.Stock Then
                objDADs.Tables(0).Rows(intCnt).Item("DispAdvType") = "Stock / Workshop"
            Else
                objDADs.Tables(0).Rows(intCnt).Item("DispAdvType") = objPU.mtdGetDAType(Trim(objDADs.Tables(0).Rows(intCnt).Item("DispAdvType")))
            End If

            'objDADs.Tables(0).Rows(intCnt).Item("SupplierName") = Trim(objDADs.Tables(0).Rows(intCnt).Item("SupplierName"))
            objDADs.Tables(0).Rows(intCnt).Item("ToLocCode") = Trim(objDADs.Tables(0).Rows(intCnt).Item("ToLocCode"))
            objDADs.Tables(0).Rows(intCnt).Item("UpdateDate") = Trim(objDADs.Tables(0).Rows(intCnt).Item("UpdateDate"))
            objDADs.Tables(0).Rows(intCnt).Item("Status") = Trim(objDADs.Tables(0).Rows(intCnt).Item("Status"))
            objDADs.Tables(0).Rows(intCnt).Item("UserName") = Trim(objDADs.Tables(0).Rows(intCnt).Item("UserName"))
        Next

        PageCount = objGlobal.mtdGetPageCount(objDADs.Tables(0).Rows.Count, dgDAList.PageSize)
        dgDAList.DataSource = objDADs

        If dgDAList.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgDAList.CurrentPageIndex = 0
            Else
                dgDAList.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgDAList.DataBind()
        BindPageList()

        For intCnt = 0 To dgDAList.Items.Count - 1
            lbl = dgDAList.Items.Item(intCnt).FindControl("lblStatus")
            Select Case CInt(Trim(lbl.Text))
                Case objPU.EnumDAStatus.Active
                    lbButton = dgDAList.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = True
                    lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    lbButton = dgDAList.Items.Item(intCnt).FindControl("lbUndelete")
                    lbButton.Visible = False
                Case objPU.EnumDAStatus.Deleted
                    lbButton = dgDAList.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = False
                    If lstAccMonth.SelectedItem.Value >= Session("SS_PUACCMONTH") Then
                        lbButton = dgDAList.Items.Item(intCnt).FindControl("lbUndelete")
                        lbButton.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
                        lbButton.Visible = True
                    Else
                        lbButton = dgDAList.Items.Item(intCnt).FindControl("lbUndelete")
                        lbButton.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
                        lbButton.Visible = False
                    End If
                Case objPU.EnumDAStatus.Confirmed, objPU.EnumDAStatus.Cancelled
                    lbButton = dgDAList.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = False
                    lbButton = dgDAList.Items.Item(intCnt).FindControl("lbUndelete")
                    lbButton.Visible = False
            End Select
        Next

        PageNo = dgDAList.CurrentPageIndex + 1
        lblTracker.Text = "Page " & PageNo & " of " & dgDAList.PageCount
    End Sub

    Sub BindGrid_AutoPost()
        Dim PageCount As Integer
        Dim dsData As DataSet
        Dim intCnt As Integer = 0

        dsData = LoadData_AutoPost()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgAdvAPost.PageSize)

        dgAdvAPost.DataSource = dsData
        If dgAdvAPost.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgAdvAPost.CurrentPageIndex = 0
            Else
                dgAdvAPost.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgAdvAPost.DataBind()


    End Sub

    Protected Function LoadData_AutoPost() As DataSet
        Dim strParamName As String
        Dim strParamValue As String
        Dim objOst As New DataSet()

        Dim strOppCode_Get As String = "PU_CLSTRX_DA_AUTOPOST_GET"

        Dim intErrNo As Integer
        Dim sSQLKriteria As String
        Dim intCnt As Integer
        Dim lbl As Label
        Dim lbButton As Label

        sSQLKriteria = "AND A.LocCode='" & strLocation & "' AND A.AccYear='" & lstAccYear.SelectedItem.Value & "' AND ((A.AccMonth='" & lstAccMonth.SelectedItem.Value & "' OR '" & lstAccMonth.SelectedItem.Value & "'=''))"

        If Len(txtDispAdvId.Text.Trim) > 0 Then
            sSQLKriteria = sSQLKriteria & "AND A.DispAdvID LIKE '%" & txtDispAdvId.Text.Trim & "%'"
        End If

        If Len(txtGoodsRcvID.Text.Trim) > 0 Then
            sSQLKriteria = sSQLKriteria & "AND E.GoodsRcvID LIKE '%" & txtGoodsRcvID.Text.Trim & "%'"
        End If

        If Len(txtGoodsRcvID.Text.Trim) > 0 Then
            sSQLKriteria = sSQLKriteria & "AND E.GoodsRcvID LIKE '%" & txtGoodsRcvID.Text.Trim & "%'"
        End If

        strParamName = "STRSEARCH"
        strParamValue = sSQLKriteria

        Try
            intErrNo = objOk.mtdGetDataCommon(strOppCode_Get, strParamName, strParamValue, objOst)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=wm_ticket&errmesg=" & Exp.Message & "&redirect=")
        End Try

        For intCnt = 0 To objOst.Tables(0).Rows.Count - 1
            objOst.Tables(0).Rows(intCnt).Item("DispAdvId") = Trim(objOst.Tables(0).Rows(intCnt).Item("DispAdvId"))
            If objOst.Tables(0).Rows(intCnt).Item("DispAdvType").Trim() = objPU.EnumDAType.Stock Then
                objOst.Tables(0).Rows(intCnt).Item("DispAdvType") = "Stock / Workshop"
            Else
                objOst.Tables(0).Rows(intCnt).Item("DispAdvType") = objPU.mtdGetDAType(Trim(objOst.Tables(0).Rows(intCnt).Item("DispAdvType")))
            End If

            'objDADs.Tables(0).Rows(intCnt).Item("SupplierName") = Trim(objDADs.Tables(0).Rows(intCnt).Item("SupplierName"))
            objOst.Tables(0).Rows(intCnt).Item("ToLocCode") = Trim(objOst.Tables(0).Rows(intCnt).Item("ToLocCode"))
            objOst.Tables(0).Rows(intCnt).Item("UpdateDate") = Trim(objOst.Tables(0).Rows(intCnt).Item("UpdateDate"))
            objOst.Tables(0).Rows(intCnt).Item("Status") = Trim(objOst.Tables(0).Rows(intCnt).Item("Status"))
            objOst.Tables(0).Rows(intCnt).Item("UserName") = Trim(objOst.Tables(0).Rows(intCnt).Item("UserName"))

        Next

        dgAdvAPost.DataSource = objOst

        dgAdvAPost.DataBind()
 

        Return objOst
    End Function

    Sub BindPageList()
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgDAList.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgDAList.CurrentPageIndex
    End Sub

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgDAList.CurrentPageIndex = 0
            Case "prev"
                dgDAList.CurrentPageIndex = _
                    Math.Max(0, dgDAList.CurrentPageIndex - 1)
            Case "next"
                dgDAList.CurrentPageIndex = _
                    Math.Min(dgDAList.PageCount - 1, dgDAList.CurrentPageIndex + 1)
            Case "last"
                dgDAList.CurrentPageIndex = dgDAList.PageCount - 1
        End Select
        lstDropList.SelectedIndex = dgDAList.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgDAList.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgDAList.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        dgDAList.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim strOpCd_GetDALn As String = "PU_CLSTRX_DA_LINE_GET"
        Dim strOpCd_GetPRLn As String = "PU_CLSTRX_GR_PR_LINE_GET"
        Dim strOpCd_UpdPRLn As String = "PU_CLSTRX_PR_LINE_UPD"
        Dim strOpCd_GetItem As String = "IN_CLSSETUP_STOCKITEM_LIST_GET"
        Dim strOpCd_UpdItem As String = "PU_CLSTRX_INVITEM_DETAILS_UPD"
        Dim strOpCd_UpdDA As String = "PU_CLSTRX_DA_UPD"
        Dim strOpCd_UpdGRLn As String = "PU_CLSTRX_GR_LINE_UPD"
        Dim objDAId As Object
        Dim DispAdvCell As TableCell = e.Item.Cells(0)
        Dim strSelectedDispAdvId As String = DispAdvCell.Text
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intErrorCheck As Integer

        strParam = strSelectedDispAdvId & "||||" & objPU.EnumDAStatus.Deleted

        Try
            intErrNo = objPU.mtdUpdDALn(strOpCd_GetDALn, _
                                        strOpCd_GetPRLn, _
                                        strOpCd_UpdPRLn, _
                                        strOpCd_GetItem, _
                                        strOpCd_UpdItem, _
                                        strOpCd_UpdDA, _
                                        strOpCd_UpdGRLn, _
                                        strCompany, _
                                        strLocation, _
                                        strUserId, _
                                        strParam, _
                                        intErrorCheck)
            Select Case intErrorCheck
                Case -1
                    lblErrOnHand.Visible = True
                Case -2
                    lblErrOnHold.Visible = True
            End Select
        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_DEL_DA&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/PU_trx_DAList.aspx")
            End If
        End Try

        dgDAList.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim strOpCd_GetDALn As String = "PU_CLSTRX_DA_LINE_GET"
        Dim strOpCd_GetPRLn As String = "PU_CLSTRX_GR_PR_LINE_GET"
        Dim strOpCd_UpdPRLn As String = "PU_CLSTRX_PR_LINE_UPD"
        Dim strOpCd_GetItem As String = "IN_CLSSETUP_STOCKITEM_LIST_GET"
        Dim strOpCd_UpdItem As String = "PU_CLSTRX_INVITEM_DETAILS_UPD"
        Dim strOpCd_UpdDA As String = "PU_CLSTRX_DA_UPD"
        Dim strOpCd_UpdGRLn As String = "PU_CLSTRX_GR_LINE_UPD"
        Dim objDAId As Object
        Dim DispAdvCell As TableCell = e.Item.Cells(0)
        Dim strSelectedDispAdvId As String = DispAdvCell.Text
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intErrorCheck As Integer

        strParam = strSelectedDispAdvId & "||||" & objPU.EnumDAStatus.Active

        Try
            intErrNo = objPU.mtdUpdDALn(strOpCd_GetDALn, _
                                        strOpCd_GetPRLn, _
                                        strOpCd_UpdPRLn, _
                                        strOpCd_GetItem, _
                                        strOpCd_UpdItem, _
                                        strOpCd_UpdDA, _
                                        strOpCd_UpdGRLn, _
                                        strCompany, _
                                        strLocation, _
                                        strUserId, _
                                        strParam, _
                                        intErrorCheck)

            Select Case intErrorCheck
                Case -1
                    lblErrOnHand.Visible = True
                Case -2
                    lblErrOnHold.Visible = True
            End Select
        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_UNDELETE_DA&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/PU_trx_DAList.aspx")
            End If
        End Try

        dgDAList.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub NewINDABtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PU_trx_DADet.aspx?DAType=" & objPU.EnumDAType.Stock)
    End Sub

    Sub NewDCDABtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PU_trx_DADet.aspx?DAType=" & objPU.EnumDAType.DirectCharge)
    End Sub


    Sub NewFADABtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PU_trx_DADet.aspx?DAType=" & objPU.EnumDAType.FixedAsset)
    End Sub

    Sub NewNUDABtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PU_trx_DADet.aspx?DAType=" & objPU.EnumDAType.Nursery)
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
End Class
