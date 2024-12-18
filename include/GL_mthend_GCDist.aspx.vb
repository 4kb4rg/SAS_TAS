
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


Public Class GL_mthend_GCDist : Inherits Page

    Protected WithEvents dgResult As DataGrid
    Protected WithEvents lblDistribute As LinkButton
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents srchAccPeriod As TextBox
    Protected WithEvents srchMature As TextBox
    Protected WithEvents srchImmature As TextBox
    Protected WithEvents srchStatus As DropDownList
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents btnNew As ImageButton
    Protected WithEvents SortCol As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblGCSuccess As Label
    Protected WithEvents lblErrGCFail As Label
    Protected WithEvents lblErrGCNoAllocation As Label
    Protected WithEvents lblErrGCNoLocation As Label

    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objGLMthEnd As New agri.GL.clsMthEnd()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objSysCfg As New agri.PWSystem.clsConfig()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intConfigSetting As String
    Dim intGLAR As Integer

    Dim objLangCapDs As New Object()

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        intConfigSetting = Session("SS_CONFIGSETTING")
        intGLAR = Session("SS_GLAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLMthEndGCDist), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblGCSuccess.Visible = False
            lblErrGCFail.Visible = False
            lblErrGCNoAllocation.Visible = False
            lblErrGCNoLocation.Visible = False
            If SortExpression.Text = "" Then
                SortExpression.Text = "HD.AccYear, HD.AccMonth"
            End If
            If Not Page.IsPostBack Then
                BindGrid()
                BindPageList()
            End If
        End If
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgResult.CurrentPageIndex = 0
        dgResult.EditItemIndex = -1
        BindGrid()
        BindPageList()
    End Sub

    Sub BindGrid()
        Dim intCnt As Integer
        Dim lbButton As LinkButton
        Dim lbl As Label
        Dim lblMonth As Label
        Dim lblYear As Label
        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadData()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgResult.PageSize)

        dgResult.DataSource = dsData
        If dgResult.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgResult.CurrentPageIndex = 0
            Else
                dgResult.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgResult.DataBind()
        BindPageList()
        PageNo = dgResult.CurrentPageIndex + 1
        lblTracker.Text = "Page " & PageNo & " of " & dgResult.PageCount

        For intCnt = 0 To dgResult.Items.Count - 1
            lbl = dgResult.Items.Item(intCnt).FindControl("lblStatus")
            lblMonth = dgResult.Items.Item(intCnt).FindControl("lblAccMonth")
            lblYear = dgResult.Items.Item(intCnt).FindControl("lblAccYear")
            If Convert.ToInt16(lbl.Text) = objGLMthEnd.EnumGCDistributeStatus.Active Then
                If (objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.GCDistributeByPreceedMth), intConfigSetting) = True) And _
                    (Convert.ToInt16(lblMonth.Text) < Convert.ToInt16(strAccMonth)) And _
                    (Convert.ToInt16(lblYear.Text) <= Convert.ToInt16(strAccYear)) Then
                    lbButton = dgResult.Items.Item(intCnt).FindControl("lblDistribute")
                    lbButton.Visible = True
                Else
                    lbButton = dgResult.Items.Item(intCnt).FindControl("lblDistribute")
                    lbButton.Visible = False
                End If
            Else
                lbButton = dgResult.Items.Item(intCnt).FindControl("lblDistribute")
                lbButton.Visible = False
            End If
        Next

        If (objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.NoGCDistribute), intConfigSetting) = True) Then
            btnNew.Visible = False
        End If

        If (objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.GCDistributionByMthEnd), intConfigSetting) = True) And _
           (objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.ProportionGCDistribute), intConfigSetting) = False) Then
            btnNew.Visible = False
        End If

    End Sub


    Sub BindPageList()
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgResult.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgResult.CurrentPageIndex
    End Sub

    Protected Function LoadData() As DataSet
        Dim objGLMthEndDs As New Dataset()
        Dim strOpCd As String = "GL_CLSMTHEND_GCDISTRIBUTE_GET"
        Dim strsrchAccPeriod As String
        Dim strSrchMature As String
        Dim strSrchImmature As String
        Dim strSrchStatus As String
        Dim strSrchLastUpdate As String
        Dim strSearch As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer

        strsrchAccPeriod = IIf(srchAccPeriod.Text = "", "", srchAccPeriod.Text)
        strSrchMature = IIf(srchMature.Text = "", "", srchMature.Text)
        strSrchImmature = IIf(srchImmature.Text = "", "", srchImmature.Text)
        strSrchStatus = IIf(srchStatus.SelectedItem.Value = "", "", srchStatus.SelectedItem.Value)
        strSrchLastUpdate = IIf(txtLastUpdate.Text = "", "", txtLastUpdate.Text)
        strParam = strsrchAccPeriod & "|||" & _
                   strSrchMature & "|" & _
                   strSrchImmature & "|" & _
                   strSrchStatus & "|" & _
                   strSrchLastUpdate & "|" & _
                   SortExpression.Text & "|" & _
                   SortCol.Text
        Try
            intErrNo = objGLMthEnd.mtdGetGCDistribute(strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strAccMonth, _
                                                    strAccYear, _
                                                    strOpCd, _
                                                    strParam, _
                                                    objGLMthEndDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_MTHEND_GCDISTRIBUTE_LIST_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        Return objGLMthEndDs
        objGLMthEndDs = Nothing
    End Function

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgResult.CurrentPageIndex = 0
            Case "prev"
                dgResult.CurrentPageIndex = _
                    Math.Max(0, dgResult.CurrentPageIndex - 1)
            Case "next"
                dgResult.CurrentPageIndex = _
                    Math.Min(dgResult.PageCount - 1, dgResult.CurrentPageIndex + 1)
            Case "last"
                dgResult.CurrentPageIndex = dgResult.PageCount - 1
        End Select
        lstDropList.SelectedIndex = dgResult.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgResult.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgResult.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "asc", "desc", "asc")
        dgResult.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub DEDR_Update(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim intErrNo As Integer
        Dim strParam As String
        Dim lblLabel As Label
        Dim strGCAccMonth As String
        Dim strGCAccYear As String
        Dim intStatus As Integer

        dgResult.EditItemIndex = Convert.ToInt16(E.Item.ItemIndex)
        lblLabel = dgResult.Items.Item(Convert.ToInt16(E.Item.ItemIndex)).FindControl("lblAccMonth")
        strGCAccMonth = Trim(lblLabel.Text)
        lblLabel = dgResult.Items.Item(Convert.ToInt16(E.Item.ItemIndex)).FindControl("lblAccYear")
        strGCAccYear = Trim(lblLabel.Text)

        Try
            intErrNo = objGLMthEnd.mtdDistributeGCProcess(strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strGCAccMonth, _
                                                        strGCAccYear, _
                                                        intConfigSetting, _
                                                        intStatus)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_MTHEND_GCDIST_PROCESS&errmesg=" & Exp.ToString() & "&redirect=GL_mthend_GCDist.aspx")
        End Try

        Select Case intStatus
            Case 0      
                lblGCSuccess.Visible = True
            Case 21     
                lblErrGCNoAllocation.Visible = True
            Case 22     
                lblErrGCNoLocation.Visible = True
            Case Else   
                lblErrGCFail.Visible = True
        End Select

        BindGrid()
    End Sub

    Sub btnNew_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("GL_mthend_GCDist_Loc.aspx")
    End Sub

    Sub dgResult_OnItemDataBound(ByVal Sender As Object, ByVal e As DataGridItemEventArgs)
        Dim lbl As Label

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

            If (objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.GCDistributionByMthEnd), intConfigSetting) = True) And _
                (objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.ProportionGCDistribute), intConfigSetting) = False) Then

                lbl = e.Item.FindControl("lblAccPeriod")
                e.Item.Cells(0).Text = lbl.Text
            End If
        End If
        
    End Sub

End Class
