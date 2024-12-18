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


Public Class GL_trx_BudgetVeh_list : Inherits Page

    Protected WithEvents dgList As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblErrMessage As Label

    Protected WithEvents srchAccCode As TextBox
    Protected WithEvents srchAccYear As TextBox
    Protected WithEvents srchBlockCode As TextBox
    Protected WithEvents srchVehCode As TextBox

    Protected WithEvents srchUpdateBy As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents srchStatusList As DropDownList
    Protected WithEvents lblBlkCode As Label
    Protected WithEvents lblVehCode As Label

    Protected objGLTrx As New agri.GL.clsTrx()
    Protected objGLSetup As New agri.GL.clsSetup()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intGLAR As Integer
    Dim strLocType As String

    Dim objBudgetDs As New DataSet()
    Dim objLangCapDs As New Object()

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intGLAR = Session("SS_GLAR")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLMonthEnd), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.Text = "" Then
                SortExpression.Text = "AccYear"
            End If

            onload_GetLangCap()

            If Not Page.IsPostBack Then
                srchAccYear.Text = year(datetime.now)
                BindGrid()
                BindPageList()
            End If
        End If
    End Sub


    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgList.CurrentPageIndex = 0
        dgList.EditItemIndex = -1
        BindGrid()
        BindPageList()
    End Sub

    Sub BindGrid()

        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadData()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgList.PageSize)

        dgList.DataSource = dsData
        If dgList.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgList.CurrentPageIndex = 0
            Else
                dgList.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgList.DataBind()
        BindPageList()
        PageNo = dgList.CurrentPageIndex + 1
        lblTracker.Text = "Page " & PageNo & " of " & dgList.PageCount



    End Sub

    Sub BindPageList()
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgList.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While

        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgList.CurrentPageIndex

    End Sub

    Protected Function LoadData() As DataSet

        Dim strOpCd As String = "GL_CLSTRX_BUDGET_VEH_SEARCH"

        Dim dsResult As New Object

        Dim strSrchAccYear As String
        Dim strSrchVehCode As String
        Dim strSearch As String
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer

        strSrchAccYear = IIf(Trim(srchAccYear.Text) = "", "", " AND  AccYear = '" & srchAccYear.Text & "'")


        strSrchVehCode = IIf(Trim(srchVehCode.Text) = "", "", " AND  Description LIKE '%" & srchVehCode.Text & "%'")

        strSearch = strSrchAccYear & strSrchVehCode & " AND  A.LOCCODE = '" & strLocation & "'"

        strSearch = " AND " & Mid(Trim(strSearch), 6)

        strParamName = "STRSEARCH"
        strParamValue = strSearch

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                dsResult)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BUDGET_LIST_SEARCH&errmesg=" & lblErrMessage.Text & "&redirect=gl/trx/gl_trx_budget_list.aspx")
        End Try


        For intCnt = 0 To dsResult.Tables(0).Rows.Count - 1
            dsResult.Tables(0).Rows(intCnt).Item("AccYear") = Trim(dsResult.Tables(0).Rows(intCnt).Item("AccYear"))
            dsResult.Tables(0).Rows(intCnt).Item("Description") = Trim(dsResult.Tables(0).Rows(intCnt).Item("Description"))
            dsResult.Tables(0).Rows(intCnt).Item("CreateDate") = Trim(dsResult.Tables(0).Rows(intCnt).Item("CreateDate"))
            dsResult.Tables(0).Rows(intCnt).Item("UpdateID") = Trim(dsResult.Tables(0).Rows(intCnt).Item("UpdateID"))
        Next

        Return dsResult

    End Function

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgList.CurrentPageIndex = 0
            Case "prev"
                dgList.CurrentPageIndex = _
                Math.Max(0, dgList.CurrentPageIndex - 1)
            Case "next"
                dgList.CurrentPageIndex = _
                Math.Min(dgList.PageCount - 1, dgList.CurrentPageIndex + 1)
            Case "last"
                dgList.CurrentPageIndex = dgList.PageCount - 1
        End Select

        lstDropList.SelectedIndex = dgList.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgList.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgList.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        dgList.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub NewBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("GL_trx_BudgetVeh_Details.aspx")
    End Sub

    Sub onload_GetLangCap()

        GetEntireLangCap()

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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/gl_setup_subblock.aspx")
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


End Class
