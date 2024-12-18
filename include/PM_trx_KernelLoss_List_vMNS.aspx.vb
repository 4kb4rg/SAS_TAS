
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

Public Class PM_KernelLoss_List : Inherits Page

    Protected WithEvents dgKernelLossList As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblErrMessage As Label

    Protected WithEvents srchStatusList As DropDownList
    Protected WithEvents txtTestSample As TextBox
    Protected WithEvents txtdate As System.Web.UI.WebControls.TextBox

    Protected WithEvents srchUpdateBy As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents lblFmt As Label
    Protected WithEvents lblDate As Label
    Protected WithEvents lblDupMsg As Label

    Protected objPMTrx As New agri.PM.clsTrx()
    Protected objPMSetup As New agri.PM.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()

    Dim strCompany As String
    Dim strLocation As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intPMAR As Integer
    Dim objKernelLoss As New DataSet()
    Dim objTestSample as New DataSet()
    Dim objMachine As New DataSet()
    Protected WithEvents btnSelDateFrom As System.Web.UI.WebControls.Image
    Protected WithEvents SearchBtn As System.Web.UI.WebControls.Button
    Protected WithEvents btnPrev As System.Web.UI.WebControls.ImageButton
    Protected WithEvents btnNext As System.Web.UI.WebControls.ImageButton
    Protected WithEvents NewBtn As System.Web.UI.WebControls.ImageButton
    Protected WithEvents ibPrint As System.Web.UI.WebControls.ImageButton
    Protected WithEvents srchFunctionLoc As System.Web.UI.WebControls.DropDownList
    Dim strDateFormat As String

    Private Sub InitializeComponent()

    End Sub

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strAccMonth = Session("SS_PMACCMONTH")
        strAccYear = Session("SS_PMACCYEAR")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intPMAR = Session("SS_PMAR")
        strDateFormat = Session("SS_DATEFMT")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMKernelLoss), intPMAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.Text = "" Then
                SortExpression.Text = "TransDate"
            End If

            If Not Page.IsPostBack Then
              
                BindStation()
              
                BindGrid()
                BindPageList()
            End If
        End If
    End Sub

  
   Sub BindStation()
        Dim strOpCd As String =  "PM_CLSTRX_STATION_GET"
        Dim dsList As DataSet
        Dim intCnt As Integer
        Dim strSearch As String = ""
        Dim strParam  As String = ""
        Dim intErrNo  As Integer
                
        strSearch = "WHERE Status = '1' AND LocCode = '" &  strLocation & "' AND UsedFor = '" &  objPMSetup.EnumMachineCriteriaFor.KernelLoss & "'"

        strParam = "|" & strSearch
        Try
            intErrNo = objPMTrx.mtdGetTransactionList(strOpCd, strParam, dsList)
        Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_KERNELLOSS_BINDSTATION&errmesg=" & lblErrMessage.Text & "&redirect=PM/Trx/PM_trx_KernelLoss_List.aspx")
        End Try

        srchFunctionLoc.Items.Add(New ListItem("All", ""))
        
        If dsList.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To dsList.Tables(0).Rows.Count - 1
                srchFunctionLoc.Items.Add(New ListItem(dsList.Tables(0).Rows(intCnt).Item("Station"), dsList.Tables(0).Rows(intCnt).Item("Station")))
            Next
        End If

    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgKernelLossList.CurrentPageIndex = 0
        dgKernelLossList.EditItemIndex = -1
        BindGrid()
        BindPageList()
    End Sub

    Sub BindGrid()
        Dim lbButton As LinkButton
        Dim intCnt As Integer
        Dim intStatus As Integer

        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadData()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgKernelLossList.PageSize)

        If SortExpression.Text.Trim() = "OL.StationCode" Then
            Dim dView As New DataView(dsData.Tables(0))
            dView.Sort = "StationDesc " & SortCol.Text
            dgKernelLossList.DataSource = dView
        Else
            dgKernelLossList.DataSource = dsData
        End If

        If dgKernelLossList.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgKernelLossList.CurrentPageIndex = 0
            Else
                dgKernelLossList.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgKernelLossList.DataBind()
        BindPageList()
        PageNo = dgKernelLossList.CurrentPageIndex + 1
        lblTracker.Text = "Page " & PageNo & " of " & dgKernelLossList.PageCount

       

    End Sub

    Sub BindPageList()
        Dim count As Integer = 1
        Dim arrDList As New ArrayList

        While Not count = dgKernelLossList.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While

        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgKernelLossList.CurrentPageIndex

    End Sub

    Protected Function LoadData() As DataSet
        Dim strOppCode_Get As String
        Dim strDate As String
        Dim strSrchStatus As String = ""
        Dim strSrchLastUpdate As String
        Dim strSrchTestSample As String
        Dim strSrchMachine As String
        Dim strSrchFunctionLoc As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer

        strDate = IIf(txtdate.Text = "", "", CheckDate())
        strSrchLastUpdate = IIf(srchUpdateBy.Text = "", "", srchUpdateBy.Text)
        strSrchTestSample = IIf(txtTestSample.Text = "", "", txtTestSample.Text)
        strSrchFunctionLoc = IIf(srchFunctionLoc.SelectedItem.Value = "All", "", srchFunctionLoc.SelectedItem.Value)
        strOppCode_Get = "PM_CLSTRX_KERNEL_LOSS_LIST_GET"
        strParam = " AND OL.LocCode = '" & strLocation & "' AND OL.AccMonth = '" & strAccMonth & "' AND OL.AccYear = '" & strAccYear & "'" & _
                   " AND TS.LocCode = '" & strLocation & "' AND M.LocCode = '" & strLocation & "'" 
        If strDate <> "" Then
            strParam = strParam & " AND OL.TransDate = '" & strDate & "'"
        End If
        If strSrchStatus <> "" Then
            strParam = strParam & " AND OL.Status = '" & strSrchStatus & "'"
        End If
        If strSrchLastUpdate <> "" Then
            strParam = strParam & " AND USR.UserName LIKE '" & strSrchLastUpdate & "'"
        End If
        If strSrchTestSample <> "" Then
            strParam = strParam & " AND OL.TestSampleCode = '" & strSrchTestSample & "'"
        End If
        If strSrchMachine <> "" Then
            strParam = strParam & " AND OL.MachineCode = '" & strSrchMachine & "'"
        End If
        If strSrchFunctionLoc <> "" Then
            strParam = strParam & " AND OL.StationCode = '" & strSrchFunctionLoc & "'"
        End If
        strParam = " ORDER BY " & SortExpression.Text.Trim & " " & SortCol.Text & "|" & strParam
        Try
            intErrNo = objPMTrx.mtdGetTransactionList(strOppCode_Get, strParam, objKernelLoss)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WM_CLSTRX_KERNELLOST_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=WM/trx/pm_trx_KernelLoss_list.aspx")
        End Try

        Return objKernelLoss
    End Function

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgKernelLossList.CurrentPageIndex = 0
            Case "prev"
                dgKernelLossList.CurrentPageIndex = _
                Math.Max(0, dgKernelLossList.CurrentPageIndex - 1)
            Case "next"
                dgKernelLossList.CurrentPageIndex = _
                Math.Min(dgKernelLossList.PageCount - 1, dgKernelLossList.CurrentPageIndex + 1)
            Case "last"
                dgKernelLossList.CurrentPageIndex = dgKernelLossList.PageCount - 1
        End Select

        lstDropList.SelectedIndex = dgKernelLossList.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgKernelLossList.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgKernelLossList.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        dgKernelLossList.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub


    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
       
    End Sub


    Sub NewBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PM_trx_KernelLoss_Det.aspx")
    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
      
        Dim QString As String
        Dim lblTransId As Label
        Dim strTransId As String
   
        dgKernelLossList.EditItemIndex = CInt(E.Item.ItemIndex)
        lblTransId = dgKernelLossList.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblTransId")
        strTransId = lblTransId.Text
       
        QString = "?TransId=" & Server.UrlEncode(Trim(strTransId)) & _
                  "&Edit=True"
      
        Response.Redirect("PM_trx_KernelLoss_Det.aspx" & QString)
    End Sub


    Protected Function CheckDate() As String

        Dim strDateSetting As String
        Dim objSysCfgDs As DataSet
        Dim objDateFormat As String
        Dim strValidDate As String
        lblFmt.Visible = False
        lblDate.Visible = False
        lblFmt.Visible = False
        If Not txtdate.Text = "" Then
            If objGlobal.mtdValidInputDate(strDateFormat, txtdate.Text, objDateFormat, strValidDate) = True Then
                Return strValidDate
            Else
                lblFmt.Visible = True
                lblFmt.Text = objDateFormat & "."
                lblDate.Visible = True
                lblFmt.Visible = True

            End If
        End If
    End Function


End Class
