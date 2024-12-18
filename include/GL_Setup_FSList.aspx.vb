
Imports System
Imports System.Data
Imports System.Collections 
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction

Public Class GL_Setup_FSList : Inherits Page

    Protected WithEvents lblErrMesage As Label
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label

    Protected WithEvents dgFSSetup As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList 

    Protected objGLSetup As New agri.GL.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intGLAR As Integer

    Dim objFSDS As New Object()

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intGLAR = Session("SS_GLAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLFSSETUP), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.Text = "" Then
                 SortExpression.Text = "ReportCode"
            End If

            If Not Page.IsPostBack Then
                BindGrid()
                BindPageList()
            End If
        End If
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgFSSetup.CurrentPageIndex = 0
        dgFSSetup.EditItemIndex = -1
        BindGrid()
        BindPageList()
    End Sub

    Sub BindGrid()
     
        Dim strOpCode As String = "GL_CLSSETUP_FSTEMPLATE_LIST_GET" 
        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim lbButton As LinkButton
        
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim lbl As Label

        strParam = " | " & SortExpression.text & " " & SortCol.Text
          
        Try
            intErrNo = objGLSetup.mtdGetFSTemplateList(strOpCode, _
                                              strCompany, _
                                              strLocation, _
                                              strUserId, _
                                              strParam, _
                                              objFSDS)
        Catch Exp As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=FSTEMPLATE_LIST_GET&errmesg=" & Exp.Message & "&redirect=GL/Setup/gl_setup_FSList.aspx")
        End Try

        PageCount = objGlobal.mtdGetPageCount(objFSDS.Tables(0).Rows.Count, dgFSSetup.PageSize)
        dgFSSetup.DataSource = objFSDS
        If dgFSSetup.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgFSSetup.CurrentPageIndex = 0
            Else
                dgFSSetup.CurrentPageIndex = PageCount - 1
            End If
        End If
        dgFSSetup.DataBind()
        BindPageList()

        For intCnt = 0 To dgFSSetup.Items.Count - 1
            lbButton = dgFSSetup.Items.Item(intCnt).FindControl("lbDelete")
            lbButton.Visible = True
            lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
        Next

        PageNo = dgFSSetup.CurrentPageIndex + 1
        lblTracker.Text = "Page " & PageNo & " of " & dgFSSetup.PageCount
        
    End Sub

    Sub BindPageList()
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgFSSetup.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgFSSetup.CurrentPageIndex
    End Sub

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgFSSetup.CurrentPageIndex = 0
            Case "prev"
                dgFSSetup.CurrentPageIndex = _
                Math.Max(0, dgFSSetup.CurrentPageIndex - 1)
            Case "next"
                dgFSSetup.CurrentPageIndex = _
                Math.Min(dgFSSetup.PageCount - 1, dgFSSetup.CurrentPageIndex + 1)
            Case "last"
                dgFSSetup.CurrentPageIndex = dgFSSetup.PageCount - 1
        End Select
        lstDropList.SelectedIndex = dgFSSetup.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgFSSetup.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgFSSetup.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        dgFSSetup.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim lblDelText As Label      
        Dim strOpCode As String = "GL_CLSSETUP_FSTEMPLATE_DELETED"
        Dim strParam As String = ""
        Dim intCnt As Integer
        Dim intErrNo As Integer

        dgFSSetup.EditItemIndex = CInt(E.Item.ItemIndex)
        lblDelText = dgFSSetup.Items.Item(CInt(E.Item.ItemIndex)).FindControl("idStrCode")
        
        strParam = lblDelText.Text
            
        Try
            intErrNo = objGLSetup.mtdDelFSTemplate(strOpCode, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam)
 
        Catch Exp As Exception
             Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_FSTEMPLATE_DELETED&errmesg=" & Exp.Message & "&redirect=GL/Setup/gl_setup_FSList.aspx")
        End Try

        BindGrid()
        BindPageList()
    End Sub


    Sub NewBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("GL_Setup_FS.aspx")
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
