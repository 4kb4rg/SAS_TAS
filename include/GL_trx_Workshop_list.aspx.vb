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


Public Class GL_Workshop_List : Inherits Page

    Protected WithEvents dgTx As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents StatusList As DropDownList
    Protected WithEvents srchStatusList As DropDownList
    Protected WithEvents SortExpression As Label
    Protected WithEvents sortcol As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents curStatus As Label
    Protected WithEvents srchVehUsageID As TextBox
    Protected WithEvents srchDesc As TextBox
    Protected WithEvents srchUpdateBy As TextBox
    Protected WithEvents srchVehCode As TextBox
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblVehUsageId As Label
    Protected WithEvents lblVehCode As Label
    Protected WithEvents SearchBtn As Button

    Protected WithEvents lstAccMonth As DropDownList
    Protected WithEvents lstAccYear As DropDownList

    Dim objGLtrx As New agri.GL.ClsTrx()
    Protected objGLtx As New agri.GL.ClsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Dim strOppCd_GET As String = "GL_CLSTRX_WORKSHOP_LIST_GET"
    Dim intErrNo As Integer
    Dim strParam As String = ""

    Dim objLangCapDs As New Object()
    Dim objDataSet As New DataSet()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intGLAR As Integer

    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String
    Dim strSelAccMonth As String
    Dim strSelAccYear As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        intGLAR = Session("SS_GLAR")
        strLocType = Session("SS_LOCTYPE")
        strSelAccMonth = Session("SS_SELACCMONTH")
        strSelAccYear = Session("SS_SELACCYEAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicleUsage), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")

        Else
            onload_GetLangCap()
            If SortExpression.Text = "" Then
                SortExpression.Text = "VehUsageID"
            End If
            If Not Page.IsPostBack Then
                If Session("SS_FILTERPERIOD") = "0" Then
                    lstAccMonth.SelectedValue = strAccMonth
                    BindAccYear(strAccYear)
                Else
                    lstAccMonth.SelectedValue = strSelAccMonth
                    BindAccYear(strSelAccYear)
                End If

                BindSearchList()
                BindGrid()
                BindPageList()
            End If
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        lblTitle.Text = "WORKSHOP"
        lblVehUsageId.Text = "Workshop ID"
        lblVehCode.Text = "Workshop Code"

        dgTx.Columns(0).HeaderText = lblVehUsageId.Text
        dgTx.Columns(1).HeaderText = lblVehCode.Text
        dgTx.Columns(2).HeaderText = "Deskripsi"

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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/menu_GLTrx_page.aspx")
        End Try

    End Sub



    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

        For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If strLocType = objAdminLoc.EnumLocType.Mill Then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                Else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                End If
                Exit For
            End If
        Next
    End Function

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgTx.CurrentPageIndex = 0
        dgTx.EditItemIndex = -1
        BindGrid()
        BindPageList()
    End Sub

    Sub BindGrid()
        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadData()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgTx.PageSize)

        dgTx.DataSource = dsData
        If dgTx.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgTx.CurrentPageIndex = 0
            Else
                dgTx.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgTx.DataBind()
        BindPageList()
        PageNo = dgTx.CurrentPageIndex + 1
        lblTracker.Text = "Page " & PageNo & " of " & dgTx.PageCount
    End Sub
    Sub BindSearchList()

        srchStatusList.Items.Add(New ListItem(objGLtx.mtdGetVehicleUsageStatus(objGLtx.EnumVehicleUsageStatus.All), objGLtx.EnumVehicleUsageStatus.All))
        srchStatusList.Items.Add(New ListItem(objGLtx.mtdGetVehicleUsageStatus(objGLtx.EnumVehicleUsageStatus.Active), objGLtx.EnumVehicleUsageStatus.Active))
        srchStatusList.Items.Add(New ListItem(objGLtx.mtdGetVehicleUsageStatus(objGLtx.EnumVehicleUsageStatus.Cancelled), objGLtx.EnumVehicleUsageStatus.Cancelled))
        srchStatusList.SelectedIndex = 1
    End Sub

    Sub BindPageList()

        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgTx.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgTx.CurrentPageIndex

    End Sub

    Protected Function LoadData() As DataSet
        Dim StockCode As String
        Dim Desc As String
        Dim UpdateBy As String
        Dim srchStatus As String
        Dim strParam As String
        Dim SearchStr As String
        Dim sortitem As String

        If lstAccMonth.SelectedItem.Value = "0" Then
            strAccMonth = "1','2','3','4','5','6','7','8','9','10','11','12"
        Else
            strAccMonth = lstAccMonth.SelectedItem.Value
        End If

        strAccYear = lstAccYear.SelectedItem.Value
        strParam = srchVehUsageID.Text & "|" & _
                    srchVehCode.Text & "|" & _
                    srchUpdateBy.Text & "|" & _
                    srchStatusList.SelectedItem.Value & "|" & _
                    strLocation & "|" & _
                    strAccMonth & "|" & _
                    strAccYear & "|" & _
                    SortExpression.Text & "|" & _
                    sortcol.Text

        Try
            intErrNo = objGLtx.mtdGetVehicleUsageList(strOppCd_GET, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_VEHICLEUSAGELIST&errmesg=" & lblErrMessage.Text & "&redirect=../EN/menu/menu_GLTrx_page.aspx")
        End Try
        Return objDataSet
    End Function

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgTx.CurrentPageIndex = 0
            Case "prev"
                dgTx.CurrentPageIndex = _
                    Math.Max(0, dgTx.CurrentPageIndex - 1)
            Case "next"
                dgTx.CurrentPageIndex = _
                    Math.Min(dgTx.PageCount - 1, dgTx.CurrentPageIndex + 1)
            Case "last"
                dgTx.CurrentPageIndex = dgTx.PageCount - 1
        End Select
        lstDropList.SelectedIndex = dgTx.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgTx.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgTx.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        sortcol.Text = IIf(sortcol.Text = "ASC", "DESC", "ASC")
        dgTx.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub btnNewItm_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("../../GL/Trx/GL_Trx_Workshop_Details.aspx")
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
