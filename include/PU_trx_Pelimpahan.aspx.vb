
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


Public Class PU_PelimpahanList : Inherits Page

    Protected WithEvents dgPelimpahanList As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents StatusList as DropDownList
    Protected WithEvents SrchPelimpahanID As TextBox
    Protected WithEvents txtPelimpahanID As TextBox 
    Protected WithEvents SrchPRID As TextBox
    Protected WithEvents txtPRID As TextBox
    Protected WithEvents ddlPRID As DropDownList
    Protected WithEvents ddlPelimpahanSrchType As DropDownList
    Protected WithEvents ddlPelimpahanType As DropDownList
    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents btnNew As ImageButton
    Protected WithEvents blnUpdate as Label


    Protected objPU As New agri.PU.clsTrx()
    Protected objPUSetup As New agri.PU.clsSetup()
    Protected objINSetup As New agri.IN.clsSetup()
    Protected objIN As New agri.IN.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objAdm As New agri.Admin.clsLoc()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Protected objAdmin As New agri.Admin.clsLoc()
        

    Dim strOppCd_GET As String = "PU_CLSTRX_PELIMPAHAN_GET"
    Dim strOppCd_ADD As String = "PU_CLSTRX_PELIMPAHAN_ADD"
    Dim strOppCd_UPD As String = "PU_CLSTRX_PELIMPAHAN_UPD"
    
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strLocLevel As String
    Dim intPUAR As Integer
    Dim intFAAR As Integer
    Dim intErrNo As Integer

    Dim objDataSet As New Object()
    Dim objPelimpahanId As New Object()

    Protected WithEvents lblPRID as Label


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PUACCMONTH")
        strAccYear = Session("SS_PUACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        strLocLevel = Session("SS_LOCLEVEL")
        intPUAR = Session("SS_PUAR")
      
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUPelimpahan), intPUAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.Text = "" Then
                SortExpression.Text = "A.PelimpahanId"
                sortcol.text = "ASC"
            End If
            If Not Page.IsPostBack Then
                BindPelimpahanSrchType("",0)
                BindGrid()
            End If
        End If
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgPelimpahanList.CurrentPageIndex = 0
        BindGrid()
        BindPageList()
    End Sub


    Sub BindGrid() 
        Dim PageNo As Integer 
        Dim PageCount As Integer
        Dim dsData As DataSet
        Dim lbButton As LinkButton
        Dim lbl as Label
        Dim intCnt as Integer = 0
        
        dsData = LoadData
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgPelimpahanList.PageSize)
            
        dgPelimpahanList.DataSource = dsData
        If dgPelimpahanList.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgPelimpahanList.CurrentPageIndex = 0
            Else
                dgPelimpahanList.CurrentPageIndex = PageCount - 1
            End If
        End If
        
        dgPelimpahanList.DataBind()
        BindPageList()

         For intCnt = 0 To dgPelimpahanList.Items.Count - 1
            lbl = dgPelimpahanList.Items.Item(intCnt).FindControl("lblStatus")
            Select Case CInt(Trim(lbl.Text))
                Case objPU.EnumPelimpahanStatus.Active
                    lbButton = dgPelimpahanList.Items.Item(intCnt).FindControl("Confirm")
                    lbButton.Visible = True
                    lbButton = dgPelimpahanList.Items.Item(intCnt).FindControl("Delete")
                    lbButton.Visible = True
                Case objPU.EnumPelimpahanStatus.Cancelled, objPU.EnumPelimpahanStatus.Confirmed, objPU.EnumPelimpahanStatus.Closed
                    lbButton = dgPelimpahanList.Items.Item(intCnt).FindControl("Confirm")
                    lbButton.Visible = False
                    lbButton = dgPelimpahanList.Items.Item(intCnt).FindControl("Delete")
                    lbButton.Visible = False
            End Select
        Next
        PageNo = dgPelimpahanList.CurrentPageIndex + 1
        lblTracker.Text="Page " & pageno & " of " & dgPelimpahanList.PageCount

        
    End Sub 


    Sub BindPageList()
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        If dgPelimpahanList.PageCount > 0 Then
            While Not count = dgPelimpahanList.PageCount + 1
                arrDList.Add("Page " & count)
                count = count + 1
            End While
            lstDropList.DataSource = arrDList
            lstDropList.DataBind()
            lstDropList.SelectedIndex = dgPelimpahanList.CurrentPageIndex
        End If
    End Sub

    Protected Function BindPR(ByVal pv_strPRId As String,  ByRef pr_intIndex As Integer) as DataSet
        Dim strOpCd As String = "PU_CLSTRX_RPHPR_ID_GET"
        Dim objPRDs As New Object()
        Dim strParam As String = ""
        Dim dr As DataRow
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strPOType As String = ""
        Dim strSelectedPRId As String = ""
        dim strLocLevelPelimpahan as string = ""
        
        strLocLevelPelimpahan = strLocLevel
        strParam = "|" & strPOType & "|" & objIN.EnumPurReqStatus.Confirmed & "|" & strLocation & "|" & strLocLevelPelimpahan

        Try
            intErrNo = objPU.mtdGetPRPelimpahan(strOpCd, strParam, objPRDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_GET_PR&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_polist.aspx")
        End Try

        For intCnt = 0 To objPRDs.Tables(0).Rows.Count - 1
            objPRDs.Tables(0).Rows(intCnt).Item("PRId") = objPRDs.Tables(0).Rows(intCnt).Item("PRId").Trim()
        Next intCnt

        dr = objPRDs.Tables(0).NewRow()
        dr("PRId") = "Please Select PR ID"
        objPRDs.Tables(0).Rows.InsertAt(dr, 0)

        Return objPRDs
    End Function

   Protected Function BindPelimpahanSrchType(ByVal pv_strPelimpahan As String, ByRef pr_intIndex As Integer) As DataSet 
        ddlPelimpahanSrchType.Items.Clear()
        ddlPelimpahanSrchType.Items.Add(New ListItem(objAdm.mtdGetLocLevel(objAdm.EnumLocLevel.All), objAdm.EnumLocLevel.All))
        ddlPelimpahanSrchType.Items.Add(New ListItem(objAdm.mtdGetLocLevel(objAdm.EnumLocLevel.Estate), objAdm.EnumLocLevel.Estate))
        ddlPelimpahanSrchType.Items.Add(New ListItem(objAdm.mtdGetLocLevel(objAdm.EnumLocLevel.Perwakilan), objAdm.EnumLocLevel.Perwakilan))
        ddlPelimpahanSrchType.Items.Add(New ListItem(objAdm.mtdGetLocLevel(objAdm.EnumLocLevel.HQ), objAdm.EnumLocLevel.HQ))
        ddlPelimpahanSrchType.SelectedIndex = 0
    End Function

   Protected Function BindPelimpahanType(ByVal pv_strPelimpahan As String, ByRef pr_intIndex As Integer) As DataSet 
        If strLocLevel = 2 Then 
            ddlPelimpahanType.Items.Clear()
            ddlPelimpahanType.Items.Add(New ListItem(objAdm.mtdGetLocLevel(objAdm.EnumLocLevel.Estate), objAdm.EnumLocLevel.Estate))
            ddlPelimpahanType.Items.Add(New ListItem(objAdm.mtdGetLocLevel(objAdm.EnumLocLevel.HQ), objAdm.EnumLocLevel.HQ))
            ddlPelimpahanType.SelectedIndex = 0
        Else
            ddlPelimpahanType.Items.Clear()
            ddlPelimpahanType.Items.Add(New ListItem(objAdm.mtdGetLocLevel(objAdm.EnumLocLevel.Estate), objAdm.EnumLocLevel.Estate))
            ddlPelimpahanType.Items.Add(New ListItem(objAdm.mtdGetLocLevel(objAdm.EnumLocLevel.Perwakilan), objAdm.EnumLocLevel.Perwakilan))
            ddlPelimpahanType.SelectedIndex = 0
        End If
    End Function


    Protected Function LoadData() As DataSet
        Dim strSrchPelimpahanId As String
        Dim strSrchPRId As String
        Dim strSrchPelimpahanType As String
        Dim strSrchStatus As String
        Dim strSrchLastUpdate As String
        Dim strSearch As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim updButton As LinkButton
        Dim lbl As Label
           

        strSrchPelimpahanId = IIf(SrchPelimpahanID.Text = "", "", SrchPelimpahanID.Text)
        strSrchPRId = IIf(SrchPRID.Text = "", "", SrchPRID.Text)
        strSrchPelimpahanType = IIf(ddlPelimpahanSrchType.SelectedItem.Value = 0, "", ddlPelimpahanSrchType.SelectedItem.Value)
        strSrchStatus = IIf(ddlStatus.SelectedItem.Value = "0", "", ddlStatus.SelectedItem.Value)
        strSrchLastUpdate = IIf(txtLastUpdate.Text = "", "", txtLastUpdate.Text)

        strParam = strSrchPelimpahanId & "|" & _
                   strLocation & "|" & _
                   strSrchPRId & "|" & _ 
                   strSrchPelimpahanType & "|" & _
                   strSrchStatus & "|" & _
                   strSrchLastUpdate & "|" & _
                   SortExpression.Text & "|" & _
                   SortCol.Text
    

        Try
            intErrNo = objPU.mtdGetPelimpahan(strOppCd_GET, _
                                      strParam, _
                                      objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_PELIMPAHAN&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/PU_trx_Pelimpahan.aspx")
        End Try

        If objDataSet.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objDataSet.Tables(0).Rows.Count - 1
                objDataSet.Tables(0).Rows(intCnt).Item("PelimpahanID") = objDataSet.Tables(0).Rows(intCnt).Item("PelimpahanID").Trim()
                objDataSet.Tables(0).Rows(intCnt).Item("PRID") = objDataSet.Tables(0).Rows(intCnt).Item("PRID").Trim()
                objDataSet.Tables(0).Rows(intCnt).Item("PelimpahanType") = objDataSet.Tables(0).Rows(intCnt).Item("PelimpahanType").Trim()
            Next intCnt
        End If
        Return objDataSet
    End Function

Sub btnPreview_Click (sender As Object, e As ImageClickEventArgs)
    Dim strStatus As String
    Dim strPelimpahanID As String
    Dim strPRID As String
    Dim strPelimpahanType As String
    Dim strUpdateBy As String
    Dim strSortExp As String
    Dim strSortCol As String
    Dim DocTitleTag As String

    strStatus = IIF(Not ddlStatus.selectedItem.Value = objPU.EnumPelimpahanStatus.All, ddlStatus.selectedItem.Value, "")
    strPelimpahanID = SrchPelimpahanID.text
    strPRID = SrchPRId.text
    strPelimpahanType = ddlPelimpahanSrchType.selectedItem.Value
    strUpdateBy =  txtLastUpdate.text
    strSortExp = sortexpression.text
    strSortCol = sortcol.text
    DocTitleTag = "Pelimpahan List"

    Response.Write("<Script Language=""JavaScript"">window.open(""../reports/PU_Rpt_PelimpahanList.aspx?strStatus=" & strStatus & _
                   "&strPelimpahanID=" & strPelimpahanID & _
                   "&strPRID=" & strPRID & _ 
                   "&strPelimpahanType=" & strPelimpahanType & _
                   "&strUpdateBy=" & strUpdateBy & _
                   "&strSortExp=" & strSortExp & _
                   "&strSortCol=" & strSortCol & _
                   "&DocTitleTag=" & DocTitleTag & _
                   """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
End Sub

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgPelimpahanList.CurrentPageIndex = 0
            Case "prev"
                dgPelimpahanList.CurrentPageIndex = _
                    Math.Max(0, dgPelimpahanList.CurrentPageIndex - 1)
            Case "next"
                dgPelimpahanList.CurrentPageIndex = _
                    Math.Min(dgPelimpahanList.PageCount - 1, dgPelimpahanList.CurrentPageIndex + 1)
            Case "last"
                dgPelimpahanList.CurrentPageIndex = dgPelimpahanList.PageCount - 1
        End Select
        lstDropList.SelectedIndex = dgPelimpahanList.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgPelimpahanList.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgPelimpahanList.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        dgPelimpahanList.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    
    Sub DEDR_Add(Sender As Object, E As ImageClickEventArgs)
        Dim dataSet As DataSet = LoadData
        Dim newRow As DataRow
        Dim strPelimpahanType As String = ""
        Dim strPRID As String = ""
        Dim intSelectedPelimpahanType As Integer
        Dim intSelectedPRID As Integer
        Dim PageCount As Integer
        Dim UpdButton As LinkButton
        Dim strOpCd As String = "ADMIN_CLSLOC_LOCATION_LIST_GET"
        Dim objLocDs As New Object()
        Dim strPRRefLocCode As String
        Dim strParam As String = ""
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer
        Dim strSelectedPRRefLocCode as string = ""

        blnUpdate.text = False
        newRow = dataSet.Tables(0).NewRow()
        newRow.Item("PelimpahanID") = ""
        newRow.Item("PRID") = ""
        newRow.Item("PelimpahanType") = 0
        newRow.Item("Status") = 1
        newRow.Item("CreateDate") = DateTime.now()
        NewRow.Item("UpdateDate") = DateTime.now()
        NewRow.Item("UpdateID") = ""
        dataSet.Tables(0).Rows.Add(newRow)
        
        dgPelimpahanList.DataSource = dataSet
        PageCount = objGlobal.mtdGetPageCount(dataSet.Tables(0).Rows.Count, dgPelimpahanList.PageSize)
        If dgPelimpahanList.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgPelimpahanList.CurrentPageIndex = 0
            Else
                dgPelimpahanList.CurrentPageIndex = PageCount - 1
            End If
        End If
        dgPelimpahanList.DataBind()
        BindPageList()

        dgPelimpahanList.CurrentPageIndex = dgPelimpahanList.PageCount - 1
        lblTracker.Text="Page " & (dgPelimpahanList.CurrentPageIndex + 1) & " of " & dgPelimpahanList.PageCount
        lstDropList.SelectedIndex = dgPelimpahanList.CurrentPageIndex
        dgPelimpahanList.DataBind()
        dgPelimpahanList.EditItemIndex = dgPelimpahanList.Items.Count -1
        dgPelimpahanList.DataBind()

    
        ddlPRID = dgPelimpahanList.Items.Item(CInt(dgPelimpahanList.EditItemIndex)).FindControl("ddlPRID")
        ddlPRID.DataSource = BindPR(strPRID, intSelectedPRID)
        ddlPRID.DataValueField = "PRID"
        ddlPRID.DataTextField = "PRID"
        ddlPRID.DataBind()
        ddlPRID.SelectedIndex = intSelectedPRID


        
        if strLocLevel = "2" then 
            strParam = "('1','3')||LocCode|"
        else
            strParam = "('1','2')||LocCode|"
        end if 
        Try
            intErrNo = objPU.mtdGetLocPelimpahan(strOpCd, strParam, objLocDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LOC&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_polist.aspx")
        End Try

        For intCnt = 0 To objLocDs.Tables(0).Rows.Count - 1
            objLocDs.Tables(0).Rows(intCnt).Item("LocCode") = objLocDs.Tables(0).Rows(intCnt).Item("LocCode").Trim()
            objLocDs.Tables(0).Rows(intCnt).Item("Description") = objLocDs.Tables(0).Rows(intCnt).Item("LocCode") & " (" & objLocDs.Tables(0).Rows(intCnt).Item("Description").Trim() & ")"

            If objLocDs.Tables(0).Rows(intCnt).Item("LocCode") = strSelectedPRRefLocCode Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        dr = objLocDs.Tables(0).NewRow()
        dr("LocCode") = ""
        dr("Description") = "Please Select Pelimpahan Type"
        objLocDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlPelimpahanType = dgPelimpahanList.Items.Item(CInt(dgPelimpahanList.EditItemIndex)).FindControl("ddlPelimpahanType")

        ddlPelimpahanType.DataSource = objLocDs.Tables(0)
        ddlPelimpahanType.DataValueField = "LocCode"
        ddlPelimpahanType.DataTextField = "Description"
        ddlPelimpahanType.DataBind()
        ddlPelimpahanType.SelectedIndex = intSelectedIndex


        Updbutton = dgPelimpahanList.Items.Item(CInt(dgPelimpahanList.EditItemIndex)).FindControl("Confirm")
        Updbutton.visible = False
        Updbutton = dgPelimpahanList.Items.Item(CInt(dgPelimpahanList.EditItemIndex)).FindControl("Cancel")
        Updbutton.visible = True
      
    End Sub

    Sub DEDR_Save(Sender As Object, E As DataGridCommandEventArgs)
        Dim LabelText As Label
        Dim list As Dropdownlist
        Dim PelimpahanID As String
        Dim PRID As String
        Dim PelimpahanType As String
        Dim lblMsg as label
        Dim CreateDate As String
        Dim strParam As String
        Dim Updbutton as LinkButton

      
        
        LabelText = E.Item.FindControl("lblPelimpahanID")
        PelimpahanID = LabelText.Text
        list = E.Item.FindControl("ddlPRID")
        PRID = list.Selecteditem.Value
        list = E.Item.FindControl("ddlPelimpahanType")
        PelimpahanType = list.Selecteditem.Value

        lblMsg = E.Item.FindControl("lblErrPRID")
        If Trim(PRID) = "Please Select PR ID" Then
           lblMsg.Visible = True
           Exit Sub 
        End If
 
        strParam =  PelimpahanID & "|" & _
                    PRID & "|" & _
                    PelimpahanType & "|" & _
                    objPU.EnumPelimpahanStatus.Active
                
        Try
        intErrNo = objPU.mtdUpdPelimpahan(strOppCd_ADD, _
                                          strOppCd_UPD, _
                                          strOppCd_GET, _
                                          strCompany, _
                                          strLocation, _
                                          strUserId, _
                                          strParam, _
                                          objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.Pelimpahan), _ 
                                          objPelimpahanID, _  
                                          FALSE)
      
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_TRX_UPDATE_PELIMPAHAN&errmesg=" & lblErrMessage.Text & "&redirect=PU/Trx/PU_Trx_Pelimpahan.aspx")
        End Try
        
        dgPelimpahanList.EditItemIndex = -1
        BindGrid() 

    End Sub

    Sub DEDR_Confirm(Sender As Object, E As DataGridCommandEventArgs)
        Dim LabelText As Label
        Dim list As Dropdownlist
        Dim PelimpahanID As String
        Dim PRID As String
        Dim PelimpahanType As String
        Dim lblMsg as label
        Dim CreateDate As String
        Dim strParam As String
        Dim strOpCd_Upd as string = "PU_CLSTRX_PELIMPAHAN_UPD_PR"
        Dim objPRPem as New Object()
         
        LabelText = E.Item.FindControl("lblPelimpahanID")
        PelimpahanID = LabelText.Text
        LabelText = E.Item.FindControl("lblPRID")
        PRID = LabelText.Text
        LabelText = E.Item.FindControl("lblPelimpahanType")
        PelimpahanType = LabelText.Text
        strParam =  PelimpahanID & "|" & _
                    PRID & "|" & _
                    PelimpahanType & "|" & _
                    objPU.EnumPelimpahanStatus.Confirmed
                     
        Try
        intErrNo = objPU.mtdUpdPelimpahan(strOppCd_ADD, _
                                          strOppCd_UPD, _
                                          strOppCd_GET, _
                                          strCompany, _
                                          strLocation, _
                                          strUserId, _
                                          strParam, _
                                          objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.Pelimpahan), _ 
                                          objPelimpahanID, _  
                                          FALSE)
      
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_TRX_UPDATE_PELIMPAHAN&errmesg=" & lblErrMessage.Text & "&redirect=PU/Trx/PU_Trx_Pelimpahan.aspx")
        End Try

        strParam = " Set LocCode = '" & Trim(PelimpahanType) & "' where prid = '" & trim(PRID) & "'"

        Try
            intErrNo = objPU.mtdUpdPRbasedonPelimpahan(strOpCd_Upd, _
                                                           strParam, _
                                                           objPRPem)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=NEW_PURREQ_WITHOUT_PRID_SAVE&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        End Try     
   
        dgPelimpahanList.EditItemIndex = -1
        BindGrid() 
    End Sub


    Sub DEDR_Cancel(Sender As Object, E As DataGridCommandEventArgs)
        dgPelimpahanList.EditItemIndex = -1
        BindGrid() 
    End Sub


    Sub DEDR_Delete(Sender As Object, E As DataGridCommandEventArgs)
        Dim LabelText As Label
        Dim EditText As TextBox
        Dim list As Dropdownlist
        
        Dim PelimpahanID As String
        Dim PRID As String
        Dim PelimpahanType As String

        Dim lblMsg as label
        Dim CreateDate As String
        Dim strParam As String

        LabelText = E.Item.FindControl("lblPelimpahanID")
        PelimpahanID = LabelText.Text
        LabelText = E.Item.FindControl("lblPRID")
        PRID = LabelText.Text
        LabelText = E.Item.FindControl("lblPelimpahanType")

        Select Case Trim(LabelText.Text)
            Case "Estate"
                LabelText.Text = "1"
            Case "Perwakilan"
                LabelText.Text = "2"    
            Case "Kantor Pusat"
                LabelText.Text = "3"
        End Select

        PelimpahanType = LabelText.Text
                    
        strParam =  PelimpahanID & "|" & _
                    PRID & "|" & _
                    PelimpahanType & "|" & _
                    objPU.EnumPelimpahanStatus.Cancelled
                     

        
        Try
        intErrNo = objPU.mtdUpdPelimpahan(strOppCd_ADD, _
                                          strOppCd_UPD, _
                                          strOppCd_GET, _
                                          strCompany, _
                                          strLocation, _
                                          strUserId, _
                                          strParam, _
                                          objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.Pelimpahan), _ 
                                          objPelimpahanID, _  
                                          TRUE)
      
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_TRX_UPDATE_PELIMPAHAN&errmesg=" & lblErrMessage.Text & "&redirect=PU/Trx/PU_Trx_Pelimpahan.aspx")
        End Try
        
        dgPelimpahanList.EditItemIndex = -1
        BindGrid() 
    End Sub

  

End Class
