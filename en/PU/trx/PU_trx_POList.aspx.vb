

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


Public Class PU_POList : Inherits Page

    Protected WithEvents dgPOList As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents txtPOId As TextBox
    Protected WithEvents ddlPOType As DropDownList
    Protected WithEvents txtSuppCode As TextBox
    'Protected WithEvents txtName As TextBox
    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblErrQtyReceive As Label
    Protected WithEvents NewINPOBtn As ImageButton
    Protected WithEvents NewFAPOBtn As ImageButton
    Protected WithEvents NewDCPOBtn As ImageButton
    Protected WithEvents NewNUPOBtn As ImageButton
    Protected WithEvents txtRPHId As TextBox
    Protected WithEvents txtPRID As TextBox
    Protected WithEvents lstAccMonth As DropDownList
    Protected WithEvents lstAccYear As DropDownList

    Protected objPU As New agri.PU.clsTrx()
    Protected objPUSetup As New agri.PU.clsSetup()
    Protected objINSetup As New agri.IN.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGLtrx As New agri.GL.ClsTrx()
    Dim ObjOk As New agri.GL.ClsTrx()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intPUAR As Integer
    Dim intFAAR As Integer

    Dim objPODs As New Object()
    Dim objPOLnDs As New Object()
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
        intLevel = Session("SS_USRLEVEL")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUPurchaseOrder), intPUAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            'to avoid double click, on aspx add this : UseSubmitBehavior="false"
            NewINPOBtn.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(NewINPOBtn).ToString())
            NewDCPOBtn.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(NewDCPOBtn).ToString())
            NewFAPOBtn.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(NewFAPOBtn).ToString())
            NewNUPOBtn.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(NewNUPOBtn).ToString())

            lblErrQtyReceive.Visible = False
            If SortExpression.Text = "" Then
                SortExpression.Text = "A.POId"
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

                BindPOType()
                BindGrid()
                BindPageList()
            End If
        End If
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgPOList.CurrentPageIndex = 0
        dgPOList.EditItemIndex = -1
        BindGrid()
        BindPageList()
    End Sub

    Sub BindPOType()
        ddlPOType.Items.Clear()
        ddlPOType.Items.Add(New ListItem(objPU.mtdGetPOType(objPU.EnumPOType.All), objPU.EnumPOType.All))
        ddlPOType.Items.Add(New ListItem(objPU.mtdGetPOType(objPU.EnumPOType.DirectCharge), objPU.EnumPOType.DirectCharge))
        ddlPOType.Items.Add(New ListItem(objPU.mtdGetPOType(objPU.EnumPOType.FixedAsset), objPU.EnumPOType.FixedAsset))
        ddlPOType.Items.Add(New ListItem("Stock / Workshop", objPU.EnumPOType.Stock)) 

        ddlPOType.Items.Add(New ListItem(objPU.mtdGetPOType(objPU.EnumPOType.Nursery), objPU.EnumPOType.Nursery)) 
            
    End Sub

    Sub BindGrid()
        Dim strOpCd As String = "PU_CLSTRX_PO_GET"

        Dim strSrchPOId As String
        Dim strSrchPOType As String
        Dim strSrchSuppCode As String
        Dim strSrchName As String
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
        Dim strSrchRPHId As String
        Dim strSrchPRId As String
        Dim sSQLKriteria As String

        If lstAccMonth.SelectedItem.Value = "0" Then
            strAccMonth = "1','2','3','4','5','6','7','8','9','10','11','12"
        Else
            strAccMonth = lstAccMonth.SelectedItem.Value
        End If

        strAccYear = lstAccYear.SelectedItem.Value
        'strSrchRPHId = IIf(txtRPHId.Text = "", "", txtRPHId.Text)
        'strSrchPOId = IIf(txtPOId.Text = "", "", txtPOId.Text)
        'strSrchPRId = IIf(txtPRID.Text = "", "", txtPRID.Text)

        'strSrchPOType = IIf(ddlPOType.SelectedItem.Value = 0, "", ddlPOType.SelectedItem.Value)
        'strSrchSuppCode = IIf(txtSuppCode.Text = "", "", txtSuppCode.Text)
        ''strSrchName = IIf(txtName.Text = "", "", txtName.Text)
        'strSrchStatus = IIf(ddlStatus.SelectedItem.Value = "0", "", ddlStatus.SelectedItem.Value)
        'strSrchLastUpdate = IIf(txtLastUpdate.Text = "", "", txtLastUpdate.Text)


        sSQLKriteria = "AND a.AccYear='" & strAccYear & _
                    "' AND a.AccMonth in ('" & strAccMonth & "') AND a.LocCode='" & strLocation & "'"


        'If intLevel <= 1 Then
        '    sSQLKriteria = sSQLKriteria & "AND a.UserPO='" & strUserId & "'"
        'End If

        If Len(txtLastUpdate.Text) > 0 Then
            sSQLKriteria = sSQLKriteria & "AND ((a.UserPO LIKE '%" & txtLastUpdate.Text & "%') OR (c.UserName LIKE '%" & txtLastUpdate.Text & "%'))"
        End If

        If ddlPOType.SelectedIndex > 0 Then
            sSQLKriteria = sSQLKriteria & "AND A.POType='" & ddlPOType.SelectedItem.Value & "'"
        End If

        If Len(txtPOId.Text) > 0 Then
            sSQLKriteria = sSQLKriteria & "AND a.POID LIKE '%" & txtPOId.Text & "%'"
        End If

        If Len(txtPRID.Text) > 0 Then
            sSQLKriteria = sSQLKriteria & "AND E.PRID='%" & txtPOId.Text & "%'"
        End If

        If Len(txtSuppCode.Text) > 0 Then
            sSQLKriteria = sSQLKriteria & "AND (A.SupplierCode like '%" & txtSuppCode.Text & "%' OR B.Name like '%" & txtSuppCode.Text & "%')"
        End If




        strParam = "STRSEARCH"
        strSearch = sSQLKriteria

        'strParam = strSrchPOId & "||" & _
        '           strSrchPOType & "|" & _
        '           strSrchSuppCode & "|" & _
        '           strSrchPRId & "|" & _
        '           strSrchStatus & "|" & _
        '           strSrchLastUpdate & "|" & _
        '           SortExpression.Text & "|" & _
        '           SortCol.Text & "|" & _
        '           strSrchRPHId & "|" & _
        '           strAccMonth & "|" & _
        'strAccYear

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd, strParam, strSearch, objPODs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_STOCKTRANSFERDIV_DISPLAYLINE_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        'Try
        '    intErrNo = objPU.mtdGetPO(strOpCd, _
        '                              strParam, _
        '                              objPODs)
        'Catch Exp As System.Exception
        '    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_PO&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/PU_trx_POList.aspx")
        'End Try

        For intCnt = 0 To objPODs.Tables(0).Rows.Count - 1
            objPODs.Tables(0).Rows(intCnt).Item("POId") = objPODs.Tables(0).Rows(intCnt).Item("POId").Trim()
            If objPODs.Tables(0).Rows(intCnt).Item("POType").Trim() = objPU.EnumPOType.Stock Then
                objPODs.Tables(0).Rows(intCnt).Item("POType") = "Stock / Workshop"
            Else
                objPODs.Tables(0).Rows(intCnt).Item("POType") = objPU.mtdGetPOType(objPODs.Tables(0).Rows(intCnt).Item("POType").Trim())
            End If
            objPODs.Tables(0).Rows(intCnt).Item("SupplierName") = objPODs.Tables(0).Rows(intCnt).Item("SupplierName").Trim()
            objPODs.Tables(0).Rows(intCnt).Item("Status") = objPODs.Tables(0).Rows(intCnt).Item("Status").Trim()
            objPODs.Tables(0).Rows(intCnt).Item("UserName") = objPODs.Tables(0).Rows(intCnt).Item("UserName").Trim()
        Next

        PageCount = objGlobal.mtdGetPageCount(objPODs.Tables(0).Rows.Count, dgPOList.PageSize)
        dgPOList.DataSource = objPODs
        If dgPOList.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgPOList.CurrentPageIndex = 0
            Else
                dgPOList.CurrentPageIndex = PageCount - 1
            End If
        End If
        dgPOList.DataBind()
        BindPageList()

        For intCnt = 0 To dgPOList.Items.Count - 1
            lbl = dgPOList.Items.Item(intCnt).FindControl("lblStatus")
            Select Case CInt(Trim(lbl.Text))
                Case objPU.EnumPOStatus.Active
                    lbButton = dgPOList.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = True
                    lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    lbButton = dgPOList.Items.Item(intCnt).FindControl("lbUndelete")
                    lbButton.Visible = False
                Case objPU.EnumPOStatus.Confirmed, objPU.EnumPOStatus.Cancelled, objPU.EnumPOStatus.Invoiced, objPU.EnumPOStatus.Closed
                    lbButton = dgPOList.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = False
                    lbButton = dgPOList.Items.Item(intCnt).FindControl("lbUndelete")
                    lbButton.Visible = False
                Case objPU.EnumPOStatus.Deleted
                    lbButton = dgPOList.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = False
                    If lstAccMonth.SelectedItem.Value >= Session("SS_PUACCMONTH") Then
                        lbButton = dgPOList.Items.Item(intCnt).FindControl("lbUndelete")
                        lbButton.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
                        lbButton.Visible = True
                    Else
                        lbButton = dgPOList.Items.Item(intCnt).FindControl("lbUndelete")
                        lbButton.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
                        lbButton.Visible = False
                    End If
            End Select
        Next

        PageNo = dgPOList.CurrentPageIndex + 1
        lblTracker.Text = "Page " & PageNo & " of " & dgPOList.PageCount
    End Sub

    Sub BindPageList()
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgPOList.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgPOList.CurrentPageIndex
    End Sub

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgPOList.CurrentPageIndex = 0
            Case "prev"
                dgPOList.CurrentPageIndex = _
                    Math.Max(0, dgPOList.CurrentPageIndex - 1)
            Case "next"
                dgPOList.CurrentPageIndex = _
                    Math.Min(dgPOList.PageCount - 1, dgPOList.CurrentPageIndex + 1)
            Case "last"
                dgPOList.CurrentPageIndex = dgPOList.PageCount - 1
        End Select
        lstDropList.SelectedIndex = dgPOList.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgPOList.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgPOList.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        dgPOList.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Function CheckStatus(ByVal strSelectedPOId) As Double
        Dim strOpCd As String = "PU_CLSTRX_PO_LINE_GET"
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim strParam As String = ""
        Dim strPOId As String
        Dim dblQtyReceive As Double = 0

        strPOId = strSelectedPOId

        strParam = strPOId & "||||||Del"

        Try
            intErrNo = objPU.mtdGetPOLn(strOpCd, _
                                        strCompany, _
                                        strLocation, _
                                        strUserId, _
                                        strAccMonth, _
                                        strAccYear, _
                                        strParam, _
                                        objPOLnDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_POLN&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/PU_trx_POList.aspx")
        End Try

        For intCnt = 0 To objPOLnDs.Tables(0).Rows.Count - 1
            dblQtyReceive += objPOLnDs.Tables(0).Rows(intCnt).Item("QtyReceive")
        Next

        Return dblQtyReceive
    End Function

    Sub DEDR_Delete(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim strOpCd_AddPO As String = ""
        Dim strOpCd_UpdPO As String = "PU_CLSTRX_PO_UPD"
        Dim objPOId As New Object()
        Dim strParam As String = ""
        Dim POCell As TableCell = e.Item.Cells(0)
        Dim StsCell As TableCell = e.Item.Cells(1)
        Dim strSelectedPOId As String
        Dim strSelectedStatus As Integer
        Dim intErrNo As Integer
        Dim dblQtyReceive As Double = 0
        Dim Lbl As Label

        Dim strOppCd As String = "PU_CLSTRX_PO_MOVEID"
        Dim strOppCd_Back As String = "PU_CLSTRX_PO_BACKID"

        strSelectedPOId = POCell.Text

        Lbl = E.Item.FindControl("lblPOType")

        Select Case Trim(lbl.Text) 
            Case "Stock / Workshop" 
             lbl.Text = "1"
            Case "Direct Charge"
             lbl.Text = "2"        
            Case "Canteen"
             lbl.Text = "3"
            Case "Fixed Asset"
             lbl.Text = "6"
            Case "Nursery"
             lbl.Text = "7"

        End Select

        dblQtyReceive = CheckStatus(strSelectedPOId)
        If dblQtyReceive = 0 Then
            strSelectedStatus = CInt(StsCell.Text)

            strParam = strSelectedPOId & "|" & Lbl.Text & "|||" & objPU.EnumPOStatus.Deleted & "||||||||||||||||||"

            Try
               intErrNo = objPU.mtdUpdPO(strOpCd_AddPO, _
                                          strOpCd_UpdPO, _
                                          strOppCd, _
                                          strOppCd_Back, _                                        
                                          strCompany, _
                                          strLocation, _
                                          strUserId, _
                                          strParam, _
                                          objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.PurchaseOrder), _
                                          objPOId) 


            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_DEL_PO&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/PU_trx_POList.aspx")
            End Try

            dgPOList.EditItemIndex = -1
            BindGrid()
        Else
            lblErrQtyReceive.Visible = True
        End If
    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim strOpCd_AddPO As String = ""
        Dim strOpCd_UpdPO As String = "PU_CLSTRX_PO_UPD"
        Dim objPOId As New Object()
        Dim strParam As String = ""
        Dim POCell As TableCell = e.Item.Cells(0)
        Dim StsCell As TableCell = e.Item.Cells(1)
        Dim strSelectedPOId As String
        Dim strSelectedStatus As Integer
        Dim strStatus As Integer
        Dim intErrNo As Integer
        Dim dblQtyReceive As Double = 0

        Dim Lbl As Label

        Dim strOppCd As String = "PU_CLSTRX_PO_MOVEID"
        Dim strOppCd_Back As String = "PU_CLSTRX_PO_BACKID"

        strSelectedPOId = POCell.Text

        Lbl = E.Item.FindControl("lblPOType")

        Select Case Trim(lbl.Text) 
            Case "Stock / Workshop" 
             lbl.Text = "1"
            Case "Direct Charge"
             lbl.Text = "2"        
            Case "Canteen"
             lbl.Text = "3"
            Case "Fixed Asset"
             lbl.Text = "6"
            Case "Nursery"
             lbl.Text = "7"

        End Select

        dblQtyReceive = CheckStatus(strSelectedPOId)
        If dblQtyReceive = 0 Then
            strSelectedStatus = CInt(StsCell.Text)

            strParam = strSelectedPOId & "|" & Lbl.Text & "|||" & objPU.EnumPOStatus.Active & "|Del|||||||||||||||||"

            Try
                intErrNo = objPU.mtdUpdPO(strOpCd_AddPO, _
                                          strOpCd_UpdPO, _
                                          strOppCd, _
                                          strOppCd_Back, _  
                                          strCompany, _
                                          strLocation, _
                                          strUserId, _
                                          strParam, _
                                          objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.PurchaseOrder), _
                                          objPOId)

            

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_DEL_PO&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/PU_trx_POList.aspx")
            End Try

            dgPOList.EditItemIndex = -1
            BindGrid()
        Else
            lblErrQtyReceive.Visible = True
        End If
    End Sub

    Sub NewINPOBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Session("SS_POTYPE") = objPU.EnumPOType.Stock
        Response.Redirect("PU_trx_PODet.aspx?POType=" & objPU.EnumPOType.Stock)
    End Sub

    Sub NewDCPOBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Session("SS_POTYPE") = objPU.EnumPOType.DirectCharge
        Response.Redirect("PU_trx_PODet.aspx?POType=" & objPU.EnumPOType.DirectCharge)
    End Sub

    Sub NewCTPOBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Session("SS_POTYPE") = objPU.EnumPOType.Canteen
        Response.Redirect("PU_trx_PODet.aspx?POType=" & objPU.EnumPOType.Canteen)
    End Sub

    Sub NewFAPOBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Session("SS_POTYPE") = objPU.EnumPOType.FixedAsset
        Response.Redirect("PU_trx_PODet.aspx?POType=" & objPU.EnumPOType.FixedAsset)
    End Sub

    Sub NewNUPOBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Session("SS_POTYPE") = objPU.EnumPOType.Nursery
        Response.Redirect("PU_trx_PODet.aspx?POType=" & objPU.EnumPOType.Nursery)
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

End Class
