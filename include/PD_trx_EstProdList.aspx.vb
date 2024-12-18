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
Imports agri.PD.clsTrx
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.PWSystem.clsLangCap


Public Class PD_trx_EstProdList : Inherits Page

    Protected WithEvents dgLine As DataGrid
    Protected WithEvents lbDelete As LinkButton    
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents txtBlkCode As TextBox
    Protected WithEvents txtRefNo As TextBox
    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblBlock As Label
    Protected WithEvents lblCode As Label

    Protected objPDTrx As New agri.PD.clsTRx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objSysCfg As New agri.PWSystem.clsConfig()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intPDAR As Integer
    Dim intConfigsetting As Integer

    Dim objEstProdDs As New Object()
    Dim objLangCapDs As New Object()    
	
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String
	

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PDACCMONTH")
        strAccYear = Session("SS_PDACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intPDAR = Session("SS_PDAR")
        intConfigsetting = Session("SS_CONFIGSETTING")
	    
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDEstateProduction), intPDAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            If SortExpression.Text = "" Then
                SortExpression.Text = "EY.BlkCode"
            End If
            If Not Page.IsPostBack Then
                BindGrid() 
                BindPageList()
            End If
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        Try
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
                lblBlock.text = GetCaption(objLangCap.EnumLangCap.Block)
            Else
               lblBlock.text = GetCaption(objLangCap.EnumLangCap.SubBlock)
            End If
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PD_TRX_ESTPRODLIST_GET_LANGCAP_COSTLEVEL&errmesg=" & lblErrMessage.Text & "&redirect=PD/trx/PD_trx_EstProdList.aspx")
        End Try

        dgLine.Columns(0).HeaderText = lblBlock.text & lblCode.text
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PD_TRX_ESTPRODLIST_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=PD/trx/PD_trx_EstProdList.aspx")
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


    Sub srchBtn_Click(sender As Object, e As System.EventArgs) 
        dgLine.CurrentPageIndex = 0
        dgLine.EditItemIndex = -1
        BindGrid() 
        BindPageList()
    End Sub 

    Sub BindGrid() 
        Dim intCnt As Integer
        Dim lbButton As LinkButton
        Dim lbl As Label

        Dim PageNo As Integer 
        Dim PageCount As Integer
        Dim dsData As DataSet
        
        dsData = LoadData
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgLine.PageSize)
        
        dgLine.DataSource = dsData
        If dgLine.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgLine.CurrentPageIndex = 0
            Else
                dgLine.CurrentPageIndex = PageCount - 1
            End If
        End If
        
        dgLine.DataBind()
        BindPageList()
        PageNo = dgLine.CurrentPageIndex + 1
        lblTracker.Text="Page " & pageno & " of " & dgLine.PageCount

        For intCnt = 0 To dgLine.Items.Count - 1
            lbl = dgLine.Items.Item(intCnt).FindControl("lblStatus") 
           	Select Case CInt(Trim(lbl.Text)) 
                Case objPDTrx.EnumEstateYieldStatus.Active
                        lbButton = dgLine.Items.Item(intCnt).FindControl("lbDelete")
                        lbButton.Visible = True
                        lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Case objPDTrx.EnumEstateYieldStatus.Deleted
                        lbButton = dgLine.Items.Item(intCnt).FindControl("lbDelete")
                        lbButton.Visible = False
            End Select
        Next

    End Sub 

    Sub BindPageList() 
        Dim count As Integer = 1   
        Dim arrDList As New ArrayList()

        While not count = dgLine.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While 
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgLine.CurrentPageIndex
    End Sub 

    Protected Function LoadData() As DataSet
        Dim strOpCd_GET As String = "PD_CLSTRX_ESTATEYIELD_SEARCH"
        Dim strSrchBlkCode As String
        Dim strSrchRefNo As String
        Dim strSrchStatus As String
        Dim strSrchLastUpdate As String
        Dim strSearch As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer

        strSrchBlkCode = IIF(txtBlkCode.Text = "", "", txtBlkCode.Text)
        strSrchRefNo = IIF(txtRefNo.Text = "", "", txtRefNo.Text)
        strSrchStatus = IIF(ddlStatus.SelectedItem.Value = "0", "", ddlStatus.SelectedItem.Value)
        strSrchLastUpdate = IIF(txtLastUpdate.Text = "", "", txtLastUpdate.Text)
        strParam = strSrchBlkCode & "|" & _
                   strSrchRefNo & "|" & _
                   strSrchStatus & "|" & _
                   strSrchLastUpdate & "|" & _
                   SortExpression.Text & "|" & _
                   SortCol.Text & "|"

        Try
            intErrNo = objPDTrx.mtdGetEstateYield(strOpCd_GET, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  strAccMonth, _
                                                  strAccYear, _
                                                  strParam, _
                                                  objEstProdDs, _
                                                  False)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PD_TRX_ESTPROD_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objEstProdDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEstProdDs.Tables(0).Rows.Count - 1
                objEstProdDs.Tables(0).Rows(intCnt).Item("EstateYieldID") = Trim(objEstProdDs.Tables(0).Rows(intCnt).Item("EstateYieldID"))
                objEstProdDs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(objEstProdDs.Tables(0).Rows(intCnt).Item("BlkCode"))
                objEstProdDs.Tables(0).Rows(intCnt).Item("RefNo") = Trim(objEstProdDs.Tables(0).Rows(intCnt).Item("RefNo"))
                objEstProdDs.Tables(0).Rows(intCnt).Item("Status") = Trim(objEstProdDs.Tables(0).Rows(intCnt).Item("Status"))
                objEstProdDs.Tables(0).Rows(intCnt).Item("UpdateID") = Trim(objEstProdDs.Tables(0).Rows(intCnt).Item("UpdateID"))
                objEstProdDs.Tables(0).Rows(intCnt).Item("UserName") = Trim(objEstProdDs.Tables(0).Rows(intCnt).Item("UserName"))
            Next
        End If

        Return objEstProdDs
    End Function

    Sub btnPrevNext_Click (sender As Object, e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).commandargument
        Select Case direction
            Case "first"
                dgLine.CurrentPageIndex = 0
            Case "prev"
                dgLine.CurrentPageIndex = _
                    Math.Max(0, dgLine.CurrentPageIndex - 1)
            Case "next"
                dgLine.CurrentPageIndex = _
                    Math.Min(dgLine.PageCount - 1, dgLine.CurrentPageIndex + 1)
            Case "last"
                dgLine.CurrentPageIndex = dgLine.PageCount - 1
        End Select
        lstDropList.SelectedIndex = dgLine.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged (sender As Object, e As EventArgs)
        If Page.IsPostBack Then
            dgLine.CurrentPageIndex = lstDropList.SelectedIndex 
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(sender As Object, e As DataGridPageChangedEventArgs)
        dgLine.CurrentPageIndex = e.NewPageIndex
        BindGrid() 
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIF(SortCol.Text = "ASC", "DESC", "ASC")
        dgLine.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid() 
    End Sub

    Sub DEDR_Delete(Sender As Object, e As DataGridCommandEventArgs)
        Dim strOpCd As String = "PD_CLSTRX_ESTATEYIELD_STATUS_UPD"
        Dim objYieldID As String
        Dim strParam As String = ""
        Dim strSelectedEstProd As String 
        Dim intErrNo As Integer
        Dim lblEYId As Label

        dgLine.EditItemIndex = CInt(E.Item.ItemIndex)
        lblEYId = dgLine.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblId")
        strSelectedEstProd = lblEYId.Text
        strParam = strSelectedEstProd & "|" & objPDTrx.EnumEstateYieldStatus.Deleted
        
        Try
            intErrNo = objPDTrx.mtdUpdEstateYield("", _
                                                  strOpCd, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  strAccMonth, _
                                                  strAccYear, _
                                                  strParam, _
                                                  True, _
                                                  objYieldID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PD_TRX_ESTPROD_DEL&errmesg=" & lblErrMessage.Text & "&redirect=pd/trx/PD_trx_EstProdList.aspx")
        End Try

        dgLine.EditItemIndex = -1
        BindGrid() 
    End Sub

    Sub NewEYBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("PD_trx_EstProdDet.aspx")
    End Sub


End Class
