
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

Public Class PM_Trx_PKStorage_List : Inherits Page

    Protected objPMTrx As New agri.PM.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objOk As New agri.GL.ClsTrx()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intPMAR As Integer
    Dim objDataSet As New DataSet()
    Dim strDateFormat As String
    
    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PMACCMONTH")
        strAccYear  = Session("SS_PMACCYEAR")
        intPMAR = Session("SS_PMAR")
        strDateFormat = Session("SS_DATEFMT")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMPKStorage), intPMAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.Text = "" Then
                SortExpression.Text = "TransDate"
            End If

            If Not Page.IsPostBack Then
                lstAccMonth.Text = Session("SS_SELACCMONTH")
                BindAccYear(Session("SS_PMACCYEAR"))

                BindGrid()
                BindGrid_Bulking()
                BindGrid_Gudang()
                BindPageList()
            End If
        End If
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        EventData.CurrentPageIndex = 0
        EventData.EditItemIndex = -1
        BindGrid()
        BindGrid_Bulking()
        BindGrid_Gudang()
        BindPageList()
    End Sub

    Sub BindAccYear(ByVal pv_strAccYear As String)
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim intSelectedIndex As Integer
        Dim objAccYearDs As New Object
        Dim strOpCd As String = "ADMIN_CLSACCPERIOD_CONFIG_GET"

        strParamName = "LOCCODE|SEARCHSTR|SORTEXP"
        strParamValue = strLocation & "||Order By HD.AccYear"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objAccYearDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & Exp.Message & "&redirect=")
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

    Sub BindGrid()

        Dim PageNo As Integer 
        Dim PageCount As Integer
        Dim dsData As DataSet
        
        dsData = LoadData
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, EventData.PageSize)
        
        EventData.DataSource = dsData
        If EventData.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                EventData.CurrentPageIndex = 0
            Else
                EventData.CurrentPageIndex = PageCount - 1
            End If
        End If
        
        EventData.DataBind()
        BindPageList()
        PageNo = EventData.CurrentPageIndex + 1
        lblTracker.Text="Page " & pageno & " of " & EventData.PageCount
    End Sub

    Sub BindGrid_Bulking()
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadData_Bulking()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, EvenBulk.PageSize)

        EvenBulk.DataSource = dsData
        If EvenBulk.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                EvenBulk.CurrentPageIndex = 0
            Else
                EvenBulk.CurrentPageIndex = PageCount - 1
            End If
        End If

        EvenBulk.DataBind()
    End Sub


    Sub BindGrid_Gudang()
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadData_Gudang()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgGudang.PageSize)

        dgGudang.DataSource = dsData
        If dgGudang.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgGudang.CurrentPageIndex = 0
            Else
                dgGudang.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgGudang.DataBind()
    End Sub


    Sub BindPageList()
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = EventData.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While

        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = EventData.CurrentPageIndex

    End Sub

    Protected Function LoadData() As DataSet
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim objProd As New DataSet()

        Dim strOppCode_Get As String = "PM_CLSTRX_DAILYPROD_PK_STORAGE_DRYING_GET"
        Dim intErrNo As Integer

        strParamName = "ACCMONTH|ACCYEAR|STRSEARCH"

        strParamValue = lstAccMonth.SelectedItem.Value & "|" & lstAccYear.SelectedItem.Value & "|" & strLocation

        Try
            intErrNo = objOk.mtdGetDataCommon(strOppCode_Get, strParamName, strParamValue, objProd)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=wm_ticket&errmesg=" & Exp.Message & "&redirect=")
        End Try


        EventData.DataSource = objProd
        EventData.DataBind()

        Return objProd

    End Function

    Protected Function LoadData_Bulking() As DataSet
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim objBulk As New DataSet()

        Dim strOppCode_Get As String = "PM_CLSTRX_DAILYPROD_PK_STORAGE_BULKING_GET"
        Dim intErrNo As Integer

        strParamName = "ACCMONTH|ACCYEAR|STRSEARCH"
         strParamValue = lstAccMonth.SelectedItem.Value & "|" & lstAccYear.SelectedItem.Value & "|" & strLocation

        Try
            intErrNo = objOk.mtdGetDataCommon(strOppCode_Get, strParamName, strParamValue, objBulk)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=wm_ticket&errmesg=" & Exp.Message & "&redirect=")
        End Try


        EvenBulk.DataSource = objBulk
        EvenBulk.DataBind()

        Return objBulk

    End Function


    Protected Function LoadData_Gudang() As DataSet
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim objGdg As New DataSet()

        Dim strOppCode_Get As String = "PM_CLSTRX_DAILYPROD_PK_STORAGE_GUDANG_GET"
        Dim intErrNo As Integer

        strParamName = "ACCMONTH|ACCYEAR|STRSEARCH"
        strParamValue = lstAccMonth.SelectedItem.Value & "|" & lstAccYear.SelectedItem.Value & "|" & strLocation

        Try
            intErrNo = objOk.mtdGetDataCommon(strOppCode_Get, strParamName, strParamValue, objGdg)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=wm_ticket&errmesg=" & Exp.Message & "&redirect=")
        End Try


        dgGudang.DataSource = objGdg
        dgGudang.DataBind()

        Return objGdg

    End Function

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                EventData.CurrentPageIndex = 0
            Case "prev"
                EventData.CurrentPageIndex = _
                Math.Max(0, EventData.CurrentPageIndex - 1)
            Case "next"
                EventData.CurrentPageIndex = _
                Math.Min(EventData.PageCount - 1, EventData.CurrentPageIndex + 1)
            Case "last"
                EventData.CurrentPageIndex = EventData.PageCount - 1
        End Select

        lstDropList.SelectedIndex = EventData.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            EventData.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        EventData.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        EventData.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strTransDate As String
        Dim strStorageAreaCode As string
        Dim txt As TextBox

        txt = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("txtDate")
        strTransDate = Trim(txt.Text)
        txt = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("txtStorageAreaCode")
        strStorageAreaCode = Trim(txt.Text)
        If strTransDate <> "" And strStorageAreaCode <> "" Then
            Response.Redirect("PM_trx_PKStorage_Det.aspx?TransDate=" & strTransDate & "&StorageAreaCode=" & strStorageAreaCode)
        End If
    End Sub

    Sub NewBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PM_trx_PKStorage_Det.aspx")
    End Sub
    
    Protected Function CheckDate() As String
        Dim strDateSetting As String
        Dim objSysCfgDs As DataSet
        Dim objDateFormat As String
        Dim strValidDate As String
        lblDate.Visible = False
        lblFmt.Visible = False
        If Not srchTransDate.Text = "" Then
            If objGlobal.mtdValidInputDate(strDateFormat, srchTransDate.Text, objDateFormat, strValidDate) = True Then
                Return strValidDate
            Else
                lblFmt.Text = objDateFormat & "."
                lblDate.Visible = True
                lblFmt.Visible = True
                Return ""
            End If
        End If
    End Function


End Class
