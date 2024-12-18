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


Public Class GL_trx_PDO_List : Inherits Page

    Protected WithEvents dgPDO As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblUnDel As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents SQLStatement As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents StatusList As DropDownList
    Protected WithEvents SortExpression As Label
    Protected WithEvents sortcol As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents curStatus As Label
    Protected WithEvents srchDesc As TextBox
    Protected WithEvents lstAccYear As DropDownList

    Protected WithEvents ibNew As ImageButton
    Protected WithEvents ibPrint As ImageButton

    Protected objGLtx As New agri.GL.ClsTrx()

    Protected objINtx As New agri.IN.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objGLtrx As New agri.GL.ClsTrx()
    Dim ObjOk As New agri.GL.ClsTrx()

    Dim objDataSet As New DataSet()
    Dim intErrNo As Integer
    Dim strParam As String = ""

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strARAccMonth As String
    Dim strARAccYear As String
    Dim intGLAR As Integer
    Dim strSelAccMonth As String
    Dim strSelAccYear As String
	Dim intLevel As Integer
    Dim strLocLevel As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        strARAccMonth = Session("SS_ARACCMONTH")
        strARAccYear = Session("SS_ARACCYEAR")
        intGLAR = Session("SS_GLAR") 
        strSelAccMonth = Session("SS_SELACCMONTH")
        strSelAccYear = Session("SS_SELACCYEAR")
		intLevel = Session("SS_USRLEVEL")
        strLocLevel = Session("SS_LOCLEVEL")

         If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLJournal), intGLAR) = False and intLevel < 3 Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            'to avoid double click, on aspx add this : UseSubmitBehavior="false"
            ibNew.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(ibNew).ToString())
            'ibPrint.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(ibPrint).ToString())

            If SortExpression.Text = "" Then
                SortExpression.Text = "StockTransferID"
            End If
            If Not Page.IsPostBack Then
                If Session("SS_FILTERPERIOD") = "0" Then
                    BindAccYear(strAccYear)
                Else
                    BindAccYear(strSelAccYear)
                End If

                BindSearchList()
                BindGrid()
                BindPageList()
            End If
        End If
    End Sub

    Sub onload_GetLangCap()
        Dim strOpCode_BussTerm As String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
        Dim objLangCapDs As New DataSet()
        Dim strParam As String
        Dim intErrNo As Integer

        strParam = strLangCode & "|" & objGlobal.mtdGetModuleCode(objGlobal.EnumModule.Inventory)
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=../../menu/menu_INTrx_Page.aspx")
        End Try
    End Sub

    Sub BindGrid()

        Dim intCnt As Integer
        Dim lbl As Label
        Dim btn As LinkButton

        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadData()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgPDO.PageSize)

        dgPDO.DataSource = dsData
        If dgPDO.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgPDO.CurrentPageIndex = 0
            Else
                dgPDO.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgPDO.DataBind()
        BindPageList()
        PageNo = dgPDO.CurrentPageIndex + 1
        lblTracker.Text = "Page " & PageNo & " of " & dgPDO.PageCount


        '      Case CStr(objGLtx.EnumJournalStatus.Posted), _
        '         CStr(objGLtx.EnumJournalStatus.Closed)
        'Print.Visible = True

        '    Case CStr(objGLtx.EnumJournalStatus.Deleted)

        'For intCnt = 0 To dgPDO.Items.Count - 1
        '    lbl = dgPDO.Items.Item(intCnt).FindControl("lblStatus")
        '    btn = dgPDO.Items.Item(intCnt).FindControl("Delete")
        'If CInt(lbl.Text) = objGLtx.EnumJournalStatus.Active Then
        '    btn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
        'ElseIf CInt(lbl.Text) = objGLtx.EnumJournalStatus.Deleted Then
        '    If lstAccMonth.SelectedItem.Value >= Session("SS_INACCMONTH") Then
        '        btn.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
        '        btn.Text = "Undelete"
        '        btn.Visible = True
        '    Else
        '        btn.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
        '        btn.Text = "Undelete"
        '        btn.Visible = False
        '    End If
        'Else
        '    btn.Visible = False
        'End If
        'Next intCnt

    End Sub

    Sub BindSearchList()
    End Sub

    Sub BindPageList()
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgPDO.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgPDO.CurrentPageIndex

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

    Protected Function LoadData() As DataSet
        Dim strOppCd_GET As String = "GL_CLSTRX_PDO_HEADER_LIST"
        Dim strParamName As String
        Dim strParamValue As String

        Dim objDs As New Object()
        Dim intErrNo As Integer
        Dim objTransDivDs As New Object()

        strAccYear = lstAccYear.SelectedItem.Value
        strParamName = "LOC|PERIODE"
        strParamValue = strLocation & "|" & strAccYear

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOppCd_GET, strParamName, strParamValue, objDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_STOCKTRANSFERDIV_DISPLAYLINE_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        Return objDs
    End Function

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)

    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgPDO.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgPDO.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        sortcol.Text = IIf(sortcol.Text = "ASC", "DESC", "ASC")
        dgPDO.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub btnNewItm_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("../../GL/Trx/GL_trx_PDODet.aspx")
    End Sub

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgPDO.CurrentPageIndex = 0
            Case "prev"
                dgPDO.CurrentPageIndex = _
                    Math.Max(0, dgPDO.CurrentPageIndex - 1)
            Case "next"
                dgPDO.CurrentPageIndex = _
                    Math.Min(dgPDO.PageCount - 1, dgPDO.CurrentPageIndex + 1)
            Case "last"
                dgPDO.CurrentPageIndex = dgPDO.PageCount - 1
        End Select
        lstDropList.SelectedIndex = dgPDO.CurrentPageIndex
        BindGrid()
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgPDO.CurrentPageIndex = 0
        dgPDO.EditItemIndex = -1
        BindGrid()
        BindPageList()
    End Sub

End Class
