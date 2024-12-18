
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


Public Class IN_PurchaseRequest : Inherits Page

    Protected WithEvents dgPRListing As DataGrid
    Protected WithEvents dgPROst As DataGrid

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents lstDept As DropDownList

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
    Protected WithEvents ddlType As DropDownList

    Protected WithEvents srchItem As TextBox

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
    Dim objOk As New agri.GL.ClsTrx()

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
                    lstAccMonth.SelectedValue = strSelAccMonth
                    BindAccYear(strSelAccYear)
                End If

                srchApprovedBy.SelectedIndex = IIf(intLevel = 0, 1, intLevel)
                BindSearchList()
                BindPRTypeList()
                BindPRLevelList()
                BindDept()
                BindGrid()
                BindGrid_OutStanding()
                BindPageList()
                'CheckStatus()
            End If
        End If
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgPRListing.CurrentPageIndex = 0
        dgPRListing.EditItemIndex = -1
        BindGrid()
        BindGrid_OutStanding()
        CheckStatus()
        BindPageList()
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
    End Sub

    Sub BindGrid_OutStanding()
        Dim PageCount As Integer
        Dim dsData As DataSet
        Dim intCnt As Integer = 0
        Dim lbl As Label

        dsData = LoadData_OutStanding()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgPROst.PageSize)

        dgPROst.DataSource = dsData
        If dgPROst.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgPROst.CurrentPageIndex = 0
            Else
                dgPROst.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgPROst.DataBind()

        'For intCnt = 0 To dgInvOst.Items.Count - 1
        '    lbl = dgInvOst.Items.Item(intCnt).FindControl("lblNo")
        '    lbl.Text = intCnt + 1
        'Next
    End Sub

    Protected Function LoadData_OutStanding() As DataSet
        Dim strParamName As String
        Dim strParamValue As String
        Dim objOst As New DataSet()


        Dim strOppCode_Get As String = "IN_CLSTRX_PURREQ_OUTSTANDING_LIST_GET"
        Dim intErrNo As Integer

        strParamName = "LOCCODE|STRSEARCH"
        strParamValue = strLocation & "|" & ""

        Try
            intErrNo = objOk.mtdGetDataCommon(strOppCode_Get, strParamName, strParamValue, objOst)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=wm_ticket&errmesg=" & Exp.Message & "&redirect=")
        End Try

        dgPROst.DataSource = objOst
        dgPROst.DataBind()

        Return objOst
    End Function

    Sub BindDept()
        Dim dr As DataRow
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim intSelectedIndex As Integer
        Dim objItemGR As New Object
        Dim strOpCd As String = "HR_CLSSETUP_DEPTCODE_LIST_GET"

        strParamName = "SEARCHSTR|SORTEXP"
        strParamValue = "" & "|" & ""

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objItemGR)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & Exp.Message & "&redirect=")
        End Try

        For intCnt = 0 To objItemGR.Tables(0).Rows.Count - 1
            objItemGR.Tables(0).Rows(intCnt).Item("DeptCode") = Trim(objItemGR.Tables(0).Rows(intCnt).Item("DeptCode"))
            objItemGR.Tables(0).Rows(intCnt).Item("CodeDescr") = Trim(objItemGR.Tables(0).Rows(intCnt).Item("CodeDescr"))

        Next intCnt

        dr = objItemGR.Tables(0).NewRow()
        dr("DeptCode") = ""
        dr("CodeDescr") = "Please Select Department"
        objItemGR.Tables(0).Rows.InsertAt(dr, 0)

        lstDept.DataSource = objItemGR.Tables(0)
        lstDept.DataValueField = "DeptCode"
        lstDept.DataTextField = "CodeDescr"
        lstDept.DataBind()
        lstDept.SelectedIndex = intSelectedIndex - 1
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

        srchStatuslnList.Items.Add(New ListItem("All", "1','2','3','4"))
        srchStatuslnList.Items.Add(New ListItem(objIN.mtdGetPurReqLnStatus(objIN.EnumPurReqLnStatus.Active), objIN.EnumPurReqLnStatus.Active))
        srchStatuslnList.Items.Add(New ListItem(objIN.mtdGetPurReqLnStatus(objIN.EnumPurReqLnStatus.Cancel), objIN.EnumPurReqLnStatus.Cancel))
        srchStatuslnList.Items.Add(New ListItem(objIN.mtdGetPurReqLnStatus(objIN.EnumPurReqLnStatus.Approved), objIN.EnumPurReqLnStatus.Approved))
        srchStatuslnList.Items.Add(New ListItem(objIN.mtdGetPurReqLnStatus(objIN.EnumPurReqLnStatus.Hold), objIN.EnumPurReqLnStatus.Hold))
        srchStatusLnList.SelectedIndex = 0

    End Sub

    Sub BindPRTypeList()
        srchPRTypeList.Items.Add(New ListItem(objIN.mtdGetPRType(objIN.EnumPurReqDocType.All), objIN.EnumPurReqDocType.All))
        srchPRTypeList.Items.Add(New ListItem(objIN.mtdGetPRType(objIN.EnumPurReqDocType.DirectChargePR), objIN.EnumPurReqDocType.DirectChargePR))
        srchPRTypeList.Items.Add(New ListItem(objIN.mtdGetPRType(objIN.EnumPurReqDocType.StockPR), objIN.EnumPurReqDocType.StockPR))
        srchPRTypeList.Items.Add(New ListItem(objIN.mtdGetPRType(objIN.EnumPurReqDocType.WorkShopPR), objIN.EnumPurReqDocType.WorkShopPR))
        srchPRTypeList.Items.Add(New ListItem(objIN.mtdGetPRType(objIN.EnumPurReqDocType.FixedAssetPR), objIN.EnumPurReqDocType.FixedAssetPR))
        srchPRTypeList.Items.Add(New ListItem(objIN.mtdGetPRType(objIN.EnumPurReqDocType.NurseryPR), objIN.EnumPurReqDocType.NurseryPR))

    End Sub

    Sub BindPRLevelList()
        SrchPRLevelList.Items.Clear
        SrchPRLevelList.Items.Add(New ListItem(objAdminLoc.mtdGetLocLevel(objAdminLoc.EnumLocLevel.All), objAdminLoc.EnumLocLevel.All))
        SrchPRLevelList.Items.Add(New ListItem(objAdminLoc.mtdGetLocLevel(objAdminLoc.EnumLocLevel.Estate), objAdminLoc.EnumLocLevel.Estate))
        SrchPRLevelList.Items.Add(New ListItem(objAdminLoc.mtdGetLocLevel(objAdminLoc.EnumLocLevel.Perwakilan), objAdminLoc.EnumLocLevel.Perwakilan))
        srchPRLevelList.Items.Add(New ListItem(objAdminLoc.mtdGetLocLevel(objAdminLoc.EnumLocLevel.HQ), objAdminLoc.EnumLocLevel.HQ))
        srchPRLevelList.Items.Add(New ListItem(objAdminLoc.mtdGetLocLevel(objAdminLoc.EnumLocLevel.Mill), objAdminLoc.EnumLocLevel.Mill))
    End Sub

    Protected Function LoadData() As DataSet

        Dim UpdateBy As String
        Dim srchStatus As String
        Dim strParam As String
        Dim SearchStr As String
        Dim sortitem As String
        Dim strSearchLocLevel As String = ""
        Dim strLastAccMonth As String
        Dim strParamName As String
        Dim strParamValue As String
        Dim objData As New Object

        strAccYear = Year(Now())
        strAccMonth = Month(Now())
        strLastAccMonth = Val(strAccMonth) - 1

        If strLocLevel = "1" Then
            strSearchLocLevel = " AND (LTRIM(RTRIM(PR.PRID)) LIKE '%" & Trim(strLocation) & "%') "
        ElseIf strLocLevel = "2" Then
            'strSearchLocLevel = " AND (LTRIM(RTRIM(PR.PRID)) LIKE '%" & Trim(strLocation) & "%' OR (PR.LocLevel IN ('1') AND PR.Status = '2') OR (PR.LocCode LIKE '%" & Trim(strLocation) & "%')) "
            strSearchLocLevel = " AND (LTRIM(RTRIM(PR.PRID)) LIKE '%" & Trim(strLocation) & "%' OR (PR.LocCode LIKE '%" & Trim(strLocation) & "%')) "
        ElseIf strLocLevel = "3" Then
            'strSearchLocLevel = " AND (LTRIM(RTRIM(PR.PRID)) LIKE '%" & Trim(strLocation) & "%' OR (PR.LocLevel IN ('1','2') AND PR.Status = '2') OR (PR.LocCode LIKE '%" & Trim(strLocation) & "%')) "
            strSearchLocLevel = " AND (LTRIM(RTRIM(PR.PRID)) LIKE '%" & Trim(strLocation) & "%' OR (PR.LocCode LIKE '%" & Trim(strLocation) & "%')) "
        End If

        If lstAccMonth.SelectedItem.Value = "0" Then
            strAccMonth = "1','2','3','4','5','6','7','8','9','10','11','12"
        Else
            strAccMonth = lstAccMonth.SelectedItem.Value
        End If


        strParamName = "LOCCODE"
        strParamValue = strLocation

        Try
            intErrNo = objGLtrx.mtdGetDataCommon("IN_APPROVAL_LEVEL_GET", _
                                                strParamName, _
                                                strParamValue, _
                                                objData)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PURREQ_LIST_GET_SEARCH&errmesg=" & lblErrMessage.Text & "&redirect=ap/trx/ap_trx_invrcvlist.aspx")
        End Try

        strAccYear = lstAccYear.SelectedItem.Value
        If objData.Tables(0).Rows.Count > 0 Then

            SearchStr = IIf(Not srchStatusLnList.SelectedItem.Text = "All", " AND PR.PRID = PRLN.PRID ", "AND PR.PRID *= PRLN.PRID ") & _
                              " AND PR.LocLevel like '" & IIf(srchPRLevelList.SelectedItem.Value = 0, "%", srchPRLevelList.SelectedItem.Value) & _
                              "' AND PR.PRType like '" & IIf(srchPRTypeList.SelectedItem.Value = objIN.EnumPurReqDocType.All, "%", srchPRTypeList.SelectedItem.Value) & _
                              "' AND PR.Status like '" & IIf(Not srchStatusList.SelectedItem.Value = objIN.EnumPurReqStatus.All, srchStatusList.SelectedItem.Value, "%") & _
                              "' AND PR.AccMonth IN ('" & strAccMonth & "')" & _
                              " AND PR.AccYear = '" & strAccYear & "' AND PR.PRType <> '" & objIN.EnumPurReqDocType.CanteenPR & "' " & _
                              " AND PRLN.Status IN (" & IIf(Not srchStatusLnList.SelectedItem.Text = "All", "'" & srchStatusLnList.SelectedItem.Value & "'", "'1','2','3','4'") & ") " & _
                              strSearchLocLevel
        Else

            If intLevel > 0 Then
                '''tanpa department

                SearchStr = IIf(Not srchStatusLnList.SelectedItem.Text = "All", " AND PR.PRID = PRLN.PRID ", "AND PR.PRID *= PRLN.PRID ") & _
                      " AND PR.LocLevel like '" & IIf(srchPRLevelList.SelectedItem.Value = 0, "%", srchPRLevelList.SelectedItem.Value) & _
                      "' AND ((PR.DeptCode='" & lstDept.SelectedItem.Value & "') OR ('" & lstDept.SelectedItem.Value & "'='')) " & _
                      " AND PR.PRType like '" & IIf(srchPRTypeList.SelectedItem.Value = objIN.EnumPurReqDocType.All, "%", srchPRTypeList.SelectedItem.Value) & _
                      "' AND PR.Status like '" & IIf(Not srchStatusList.SelectedItem.Value = objIN.EnumPurReqStatus.All, srchStatusList.SelectedItem.Value, "%") & _
                      "' AND PR.AccMonth IN ('" & strAccMonth & "')" & _
                      " AND PR.AccYear = '" & strAccYear & "' AND PR.PRType <> '" & objIN.EnumPurReqDocType.CanteenPR & "' " & _
                      " AND PRLN.Status IN (" & IIf(Not srchStatusLnList.SelectedItem.Text = "All", "'" & srchStatusLnList.SelectedItem.Value & "'", "'1','2','3','4'") & ") " & _
                      strSearchLocLevel
            Else
                ''tanpa
                SearchStr = IIf(Not srchStatusLnList.SelectedItem.Text = "All", " AND PR.PRID = PRLN.PRID ", "AND PR.PRID *= PRLN.PRID ") & _
                          " AND PR.DeptCode IN (SELECT DeptCode FROM IN_APPROVALLN Where USERID='" & strUserId & "') AND PR.LocLevel like '" & IIf(srchPRLevelList.SelectedItem.Value = 0, "%", srchPRLevelList.SelectedItem.Value) & _
                          "' AND PR.PRType like '" & IIf(srchPRTypeList.SelectedItem.Value = objIN.EnumPurReqDocType.All, "%", srchPRTypeList.SelectedItem.Value) & _
                          "' AND PR.Status like '" & IIf(Not srchStatusList.SelectedItem.Value = objIN.EnumPurReqStatus.All, srchStatusList.SelectedItem.Value, "%") & _
                          "' AND PR.AccMonth IN ('" & strAccMonth & "')" & _
                          " AND PR.AccYear = '" & strAccYear & "' AND PR.PRType <> '" & objIN.EnumPurReqDocType.CanteenPR & "' " & _
                          " AND PRLN.Status IN (" & IIf(Not srchStatusLnList.SelectedItem.Text = "All", "'" & srchStatusLnList.SelectedItem.Value & "'", "'1','2','3','4'") & ") " & _
      strSearchLocLevel
            End If

            End If

            '" AND PR.PRID IN (SELECT PRID FROM IN_PRLN WHERE ApprovedBy in (" & IIf(Not srchApprovedBy.SelectedItem.Text = "All", "'" & srchApprovedBy.SelectedItem.Value & "'", "'0','1','2','3','4'") & ") AND " & IIF(srchApprovedBy.SelectedItem.Text = "-", " ApprovedBy = AppLevel", "ApprovedBy < AppLevel") & ") " & _
            If Not srchPRID.Text = "" Then
                SearchStr = SearchStr & " AND PR.PRID like '" & srchPRID.Text & "%' "
            End If

            If Not srchUpdateBy.Text = "" Then
                SearchStr = SearchStr & " AND Usr.Username like '" & _
                            srchUpdateBy.Text & "%' "
            End If

            If Not srchItem.Text = "" Then
                SearchStr = SearchStr & "AND (Itm.ItemCode LIKE '%" & Trim(srchItem.Text) & "%' OR Itm.Description LIKE '%" & Trim(srchItem.Text) & "%' OR PRLn.OtherName LIKE '%" & Trim(srchItem.Text) & "%')"
            End If

            sortitem = "ORDER BY " & SortExpression.Text & " " & sortcol.Text
            strParam = SearchStr & "|" & sortitem

            Try
                intErrNo = objIN.mtdGetPurchaseRequest(strOppCd_GET_PRList, _
                                                       strParam, _
                                                       objIN.EnumPurReqDocType.StockPR, _
                                                       strAccMonth, _
                                                       strAccYear, _
                                                       Trim(strLocation), _
                                                       objDataSet)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PURREQ_LIST_GET_SEARCH&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
            End Try

            intPRCount = objDataSet.Tables(0).Rows.Count

            Return objDataSet
    End Function

    Sub btnPreview_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strStatus As String
        Dim strPurReqId As String
        Dim strPurReqType As String
        Dim strUpdateBy As String
        Dim strSortExp As String
        Dim strSortCol As String
        Dim strPurReqLevel As String

        strStatus = IIf(Not srchStatusList.SelectedItem.Value = objIN.EnumPurReqStatus.All, srchStatusList.SelectedItem.Value, "")
        strPurReqId = srchPRID.Text
        strPurReqType = IIf(srchPRTypeList.SelectedItem.Value = objIN.EnumPurReqDocType.All, "", srchPRTypeList.SelectedItem.Value)
        strPurReqLevel = IIf(srchPRLevelList.SelectedItem.Value = 0, "", srchPRLevelList.SelectedItem.Value)
        strUpdateBy = srchUpdateBy.Text
        strSortExp = SortExpression.Text
        strSortCol = sortcol.Text


        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/IN_Rpt_PurReq.aspx?strStatus=" & strStatus & _
                    "&strPurReqId=" & strPurReqId & "&strPurReqLevel=" & strPurReqLevel & "&strPurReqType=" & strPurReqType & _
                    "&strUpdateBy=" & strUpdateBy & "&strSortExp=" & strSortExp & _
                    "&strSortCol=" & strSortCol & """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
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
        BindGrid()
        CheckStatus()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgPRListing.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
            CheckStatus()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgPRListing.CurrentPageIndex = e.NewPageIndex
        BindGrid()
        CheckStatus()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        sortcol.Text = IIf(sortcol.Text = "ASC", "DESC", "ASC")
        dgPRListing.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
        CheckStatus()
    End Sub

    Sub CheckStatus()
        Dim strOppCd_GET_PRLnList As String = "IN_CLSTRX_PURREQLN_LIST_GET"

        Dim strParamTemp2 As String
        Dim strParamTemp3 As String
        Dim strPRStatus As String
        Dim objPRDs As DataSet
        Dim objPRLnDs As DataSet
        Dim DelButton As LinkButton
        Dim lblPRID As Label
        Dim strPRID As String
        Dim strPRIDTemp As String
        Dim strQtyRcv As String

        For intCnt = 0 To dgPRListing.Items.Count - 1
            lblPRID = dgPRListing.Items.Item(intCnt).FindControl("PRID")
            strPRID = lblPRID.Text
            strParamTemp2 = "And PR.PRID = '" & Trim(strPRID) & "'|" & " "
            Try
                intErrNo = objIN.mtdGetPurchaseRequest(strOppCd_GET_PRList, _
                                                       strParamTemp2, _
                                                       objIN.EnumPurReqDocType.StockPR, _
                                                       strAccMonth, _
                                                       strAccYear, _
                                                       TRIM(strLocation), _
                                                       objPRDs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PURREQ_LIST_GET_CHECK_STATUS&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
            End Try

            If objPRDs.Tables(0).Rows.Count > 0 Then
                strPRIDTemp = Trim(objPRDs.Tables(0).Rows(0).Item("PRID"))
                strPRStatus = Trim(objPRDs.Tables(0).Rows(0).Item("Status"))
            End If

            strParamTemp3 = strPRIDTemp & "|" & "PRLn.PRID"
            Try
                intErrNo = objIN.mtdGetPRLnList(strOppCd_GET_PRLnList, _
                                                strCompany, _
                                                TRIM(strLocation), _
                                                strUserId, _
                                                strAccMonth, _
                                                strAccYear, _
                                                strParamTemp3, _
                                                objPRLnDs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PURREQLN_LIST_GET_CHECK_STATUS&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
            End Try

            If objPRLnDs.Tables(0).Rows.Count > 0 Then
                strQtyRcv = objPRLnDs.Tables(0).Rows(0).Item("QtyRcv")
                If strPRStatus = objIN.EnumPurReqStatus.Active And strQtyRcv <> 0 Then
                    DelButton = dgPRListing.Items.Item(intCnt).FindControl("Delete")
                    DelButton.Visible = False
                ElseIf strPRStatus = objIN.EnumPurReqStatus.Active And strQtyRcv = "" Then
                    DelButton = dgPRListing.Items.Item(intCnt).FindControl("Delete")
                    DelButton.Visible = False
                ElseIf strPRStatus = objIN.EnumPurReqStatus.Confirmed Then
                    DelButton = dgPRListing.Items.Item(intCnt).FindControl("Delete")
                    DelButton.Visible = False
                ElseIf strPRStatus = objIN.EnumPurReqStatus.Deleted Then
                    DelButton = dgPRListing.Items.Item(intCnt).FindControl("Delete")
                    DelButton.Visible = False
                    If lstAccMonth.SelectedItem.Value >= Session("SS_INACCMONTH") Then
                        DelButton = dgPRListing.Items.Item(intCnt).FindControl("Undelete")
                        DelButton.Visible = True
                    Else
                        DelButton = dgPRListing.Items.Item(intCnt).FindControl("Undelete")
                        DelButton.Visible = False
                    End If
                ElseIf strPRStatus = objIN.EnumPurReqStatus.Cancelled Then
                    DelButton = dgPRListing.Items.Item(intCnt).FindControl("Delete")
                    DelButton.Visible = False
                Else
                    DelButton = dgPRListing.Items.Item(intCnt).FindControl("Delete")
                    DelButton.Visible = True
                    DelButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                End If
            Else
                If strPRStatus = objIN.EnumPurReqStatus.Active Then
                    DelButton = dgPRListing.Items.Item(intCnt).FindControl("Delete")
                    DelButton.Visible = True
                    DelButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                ElseIf strPRStatus = objIN.EnumPurReqStatus.Confirmed Then
                    DelButton = dgPRListing.Items.Item(intCnt).FindControl("Delete")
                    DelButton.Visible = False
                ElseIf strPRStatus = objIN.EnumPurReqStatus.Deleted Then
                    DelButton = dgPRListing.Items.Item(intCnt).FindControl("Delete")
                    DelButton.Visible = False
                    If lstAccMonth.SelectedItem.Value >= Session("SS_INACCMONTH") Then
                        DelButton = dgPRListing.Items.Item(intCnt).FindControl("Undelete")
                        DelButton.Visible = True
                    Else
                        DelButton = dgPRListing.Items.Item(intCnt).FindControl("Undelete")
                        DelButton.Visible = False
                    End If
                ElseIf strPRStatus = objIN.EnumPurReqStatus.Cancelled Then
                    DelButton = dgPRListing.Items.Item(intCnt).FindControl("Delete")
                    DelButton.Visible = False
                Else
                    DelButton = dgPRListing.Items.Item(intCnt).FindControl("Delete")
                    DelButton.Visible = True
                    DelButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                End If
            End If
        Next intCnt
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOppCd_UpdPQ As String = "IN_CLSTRX_PURREQ_LIST_UPD"
        Dim strOppCd_GET_PRLnList As String = "IN_CLSTRX_PURREQLN_LIST_GET"
        Dim objPRDs As DataSet
        Dim objPRLnDs As DataSet
        Dim objPRID As Object
        Dim strPRID As String
        Dim strPRStatus As Integer = objIN.EnumPurReqStatus.Deleted
        Dim strTotalAmt As String
        Dim strRemarks As String
        Dim strPRTypeCode As String
        Dim DelText As Label
        Dim strParamTemp As String
        Dim strParamTemp2 As String
        Dim strParam As String


        dgPRListing.EditItemIndex = CInt(E.Item.ItemIndex)
        DelText = dgPRListing.Items.Item(CInt(E.Item.ItemIndex)).FindControl("PRID")
        strPRID = DelText.Text
        DelText = dgPRListing.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Remark")
        strRemarks = DelText.Text
        DelText = dgPRListing.Items.Item(CInt(E.Item.ItemIndex)).FindControl("TotalAmount")
        strTotalAmt = DelText.Text
        DelText = dgPRListing.Items.Item(CInt(E.Item.ItemIndex)).FindControl("PRTypeCode")
        strPRTypeCode = DelText.Text

        strParamTemp = strPRID & "|" & "PRLn.PRID"
        Try
            intErrNo = objIN.mtdGetPRLnList(strOppCd_GET_PRLnList, _
                                            strCompany, _
                                            TRIM(strLocation), _
                                            strUserId, _
                                            strAccMonth, _
                                            strAccYear, _
                                            strParamTemp, _
                                            objPRLnDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PURREQLN_LIST_GET_DELETE&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        End Try

        If objPRLnDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objPRLnDs.Tables(0).Rows.Count - 1
                strParam = strPRID & "||" & strPRStatus & "|" & strTotalAmt & "|" & strPRTypeCode & "|" & strLocLevel & "|" & strLocation
                Try
                    intErrNo = objIN.mtdUpdPurchaseRequest(strOppCd_UpdPQ, _
                                                           strParam, _
                                                           strCompany, _
                                                           TRIM(strLocation), _
                                                           strUserId, _
                                                           objPRID, _
                                                           objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.PurchaseRequest))
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PURREQ_LIST_UPD_DELETE_MORERECS&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
                End Try
            Next intCnt
        Else

            strParam = strPRID & "|" & strRemarks & "|" & strPRStatus & "|" & strTotalAmt & "|" & strPRTypeCode & "|" & strLocLevel & "|" & strLocation
            Try
                intErrNo = objIN.mtdUpdPurchaseRequest(strOppCd_UpdPQ, _
                                                       strParam, _
                                                       strCompany, _
                                                       TRIM(strLocation), _
                                                       strUserId, _
                                                       objPRID, _
                                                       objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.PurchaseRequest))
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PURREQ_LIST_UPD_DELETE_1REC&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
            End Try
        End If
        BindGrid()
        CheckStatus()
        BindPageList()
    End Sub


    Sub DEDR_Undelete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOppCd_UpdPQ As String = "IN_CLSTRX_PURREQ_LIST_UPD"
        Dim strOppCd_GET_PRLnList As String = "IN_CLSTRX_PURREQLN_LIST_GET"
        Dim objPRLnDs As DataSet
        Dim objPRID As Object
        Dim strPRID As String
        Dim strPRStatus As Integer = objIN.EnumPurReqStatus.Active
        Dim strTotalAmt As String
        Dim strRemarks As String
        Dim strPRTypeCode As String
        Dim DelText As Label
        Dim strParamTemp As String
        Dim strParam As String


        dgPRListing.EditItemIndex = CInt(E.Item.ItemIndex)
        DelText = dgPRListing.Items.Item(CInt(E.Item.ItemIndex)).FindControl("PRID")
        strPRID = DelText.Text
        DelText = dgPRListing.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Remark")
        strRemarks = DelText.Text
        DelText = dgPRListing.Items.Item(CInt(E.Item.ItemIndex)).FindControl("TotalAmount")
        strTotalAmt = DelText.Text
        DelText = dgPRListing.Items.Item(CInt(E.Item.ItemIndex)).FindControl("PRTypeCode")
        strPRTypeCode = DelText.Text

        strParamTemp = strPRID & "|" & "PRLn.PRID"
        Try
            intErrNo = objIN.mtdGetPRLnList(strOppCd_GET_PRLnList, _
                                            strCompany, _
                                            TRIM(strLocation), _
                                            strUserId, _
                                            strAccMonth, _
                                            strAccYear, _
                                            strParamTemp, _
                                            objPRLnDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PURREQLN_LIST_GET_UNDELETE&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        End Try

        If objPRLnDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objPRLnDs.Tables(0).Rows.Count - 1
                strParam = Trim(strPRID) & "|" & Trim(strRemarks) & "|" & Trim(strPRStatus) & "|" & Trim(strTotalAmt) & "|" & Trim(strPRTypeCode) & "|" & Trim(strLocLevel)& "|" & Trim(strLocation)
                Try
                    intErrNo = objIN.mtdUpdPurchaseRequest(strOppCd_UpdPQ, _
                                                           strParam, _
                                                           strCompany, _
                                                           TRIM(strLocation), _
                                                           strUserId, _
                                                           objPRID, _
                                                           objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.PurchaseRequest))
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PURREQ_LIST_UPD_MORERECS&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
                End Try
            Next intCnt
        Else
            strParam = Trim(strPRID) & "|" & Trim(strRemarks) & "|" & Trim(strPRStatus) & "|" & Trim(strTotalAmt) & "|" & Trim(strPRTypeCode)
            strParam = Trim(strPRID) & "|" & Trim(strRemarks) & "|" & Trim(strPRStatus) & "|" & Trim(strTotalAmt) & "|" & Trim(strPRTypeCode) & "|" & Trim(strLocLevel)& "|" & Trim(strLocation)

            Try
                intErrNo = objIN.mtdUpdPurchaseRequest(strOppCd_UpdPQ, _
                                                       strParam, _
                                                       strCompany, _
                                                       TRIM(strLocation), _
                                                       strUserId, _
                                                       objPRID, _
                                                       objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.PurchaseRequest))
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PURREQ_LIST_UPD_1REC&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
            End Try
        End If
        BindGrid()
        CheckStatus()
        BindPageList()
    End Sub

    Sub btnNewStPR_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim Issuetype As String = CType(sender, ImageButton).CommandName
        Response.Redirect("IN_PurReq_Details.aspx?prqtype=" & Issuetype)
    End Sub
	
	Sub btnNewDCPR_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim Issuetype As String = CType(sender, ImageButton).CommandName
        Response.Redirect("IN_PurReq_Details.aspx?prqtype=" & Issuetype)
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

