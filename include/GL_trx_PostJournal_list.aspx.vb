
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


Public Class GL_PostJournal_List : Inherits Page

    Protected WithEvents dgTx As DataGrid
    Protected WithEvents lblUnDel As Label
    Protected WithEvents ErrorMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents SQLStatement As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents StatusList As DropDownList
    Protected WithEvents srchTxTypeList As DropDownList
    Protected WithEvents SortExpression As Label
    Protected WithEvents sortcol As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents curStatus As Label
    Protected WithEvents srchStockTxID As TextBox
    Protected WithEvents srchDesc As TextBox
    Protected WithEvents srchRefNo As TextBox
    Protected WithEvents srchTxType As TextBox
    Protected WithEvents lblErrMsg As Label
    Protected WithEvents dgErr As DataGrid
    Protected WithEvents lstAccMonth As DropDownList
    Protected WithEvents lstAccYear As DropDownList

    Protected WithEvents postJr As ImageButton
    Protected WithEvents ibPrint As ImageButton

    Protected objGLtx As New agri.GL.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim strOppCd_GET As String = "GL_CLSTRX_POSTJOURNAL_LIST_GET"
    Dim objDataSet As New DataSet()
    Dim objLangCapDs As New DataSet()
    Dim intErrNo As Integer
    Dim strParam As String = ""

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objSysCfg As New agri.PWSystem.clsConfig()

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
    Dim intLevel As Integer

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
        intLevel = Session("SS_USRLEVEL")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLPosting), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")

        Else
            GetEntireLangCap()
            If SortExpression.Text = "" Then
                SortExpression.Text = "JournalID"
            End If
            'to avoid double click, on aspx add this : UseSubmitBehavior="false"
            postJr.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(postJr).ToString())
            ibPrint.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(ibPrint).ToString())

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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_TRX_JOURNAL_DETAIL_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=gl/trx/journal_details.aspx")
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
        
        dsData = LoadData
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
        lblTracker.Text="Page " & pageno & " of " & dgTx.PageCount
    End Sub
    Sub BindSearchList()
        srchTxTypeList.Items.Clear
        srchTxTypeList.Items.Add(New ListItem(objGLtx.mtdGetJournalTransactType(objGLtx.EnumJournalTransactType.All), objGLtx.EnumJournalTransactType.All))
        srchTxTypeList.Items.Add(New ListItem(objGLtx.mtdGetJournalTransactType(objGLtx.EnumJournalTransactType.Adjustment), objGLtx.EnumJournalTransactType.Adjustment))
        srchTxTypeList.Items.Add(New ListItem(objGLtx.mtdGetJournalTransactType(objGLtx.EnumJournalTransactType.CreditNote), objGLtx.EnumJournalTransactType.CreditNote))
        srchTxTypeList.Items.Add(New ListItem(objGLtx.mtdGetJournalTransactType(objGLtx.EnumJournalTransactType.DebitNote), objGLtx.EnumJournalTransactType.DebitNote))
        srchTxTypeList.Items.Add(New ListItem(objGLtx.mtdGetJournalTransactType(objGLtx.EnumJournalTransactType.Invoice), objGLtx.EnumJournalTransactType.Invoice))
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

        strParam = srchStockTxID.Text & "|" & _
                    srchDesc.Text & "|" & _
                    srchRefNo.Text & "|" & _
                    srchTxTypeList.SelectedItem.Value & "|" & _
                    strLocation & "|" & _
                    SortExpression.Text & "|" & _
                    sortcol.Text & "|" & _
                    objGLtx.EnumJournalStatus.Active & "|" & _
                    strUserId & "|" & _
                    strAccMonth & "|" & _
                    strAccYear

        Try
            intErrNo = objGLtx.mtdGetPostJournalList(strOppCd_GET, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_JOURNALLIST&errmesg=" & lblErrMessage.Text & "&redirect=../EN/menu/menu_GLTrx_page.aspx")
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

    Sub dgErr_ItemCreated(ByVal Sender As Object, ByVal e As DataGridItemEventArgs)
        Dim lbl As Label
        Select Case e.Item.ItemType
            Case ListItemType.Item, ListItemType.AlternatingItem
                lbl = e.Item.FindControl("lblReason")
                Select Case lbl.Text
                    Case "0" : lbl.Text = "No journal line."
                    Case "1" : lbl.Text = "Involved account from more than two " & GetCaption(objLangCap.EnumLangCap.Location) & "s."
                    Case "2" : lbl.Text = "Account from active " & GetCaption(objLangCap.EnumLangCap.Location) & " was not found in the journal."
                    Case "3" : lbl.Text = "Inter-" & GetCaption(objLangCap.EnumLangCap.Location) & " appear in both debit and credit side."
                    Case "4" : lbl.Text = "No error found."
                End Select
        End Select
    End Sub

    Sub btnPost_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strOpCdStckTxDet_UPD As String = "GL_CLSTRX_JOURNAL_DETAIL_UPD"
        Dim ErrorChk As Integer = objGLtx.EnumGeneralLedgerTxErrorType.NoError
        Dim dsError As New DataSet()
        Dim colParam As New Collection
        Dim postAccMonth As String
        Dim postAccYear As String

        If lstAccMonth.SelectedItem.Value = "0" Then
            postAccMonth = "1','2','3','4','5','6','7','8','9','10','11','12"
            lblErrMsg.Text = "Please select only 1 account period to proceed this posting."
            lblErrMsg.Visible = True
            Exit Sub
        Else
            postAccMonth = lstAccMonth.SelectedItem.Value
        End If

        postAccYear = lstAccYear.SelectedItem.Value

        Dim intInputPeriod As Integer = (CInt(postAccYear) * 100) + CInt(postAccMonth)
        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        If Session("SS_FILTERPERIOD") = "0" Then
            If intCurPeriod < intSelPeriod Then
                lblErrMsg.Visible = True
                lblErrMsg.Text = "Invalid posting period."
                Exit Sub
            End If
        Else
            If intSelPeriod <> intInputPeriod Then
                lblErrMsg.Visible = True
                lblErrMsg.Text = "Invalid posting period."
                Exit Sub
            End If
            If intSelPeriod < intCurPeriod And intLevel < 2 Then
                lblErrMsg.Visible = True
                lblErrMsg.Text = "This period already locked."
                Exit Sub
            End If
        End If


        colParam.Add(GetCaption(objLangCap.EnumLangCap.Account), "MS_COA")
        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), Session("SS_CONFIGSETTING")) = True Then
            colParam.Add(GetCaption(objLangCap.EnumLangCap.Block), "MS_BLOCK")
        Else
            colParam.Add(GetCaption(objLangCap.EnumLangCap.SubBlock), "MS_BLOCK")
        End If
        colParam.Add(GetCaption(objLangCap.EnumLangCap.Vehicle), "MS_VEHICLE")
        colParam.Add(GetCaption(objLangCap.EnumLangCap.VehExpense), "MS_VEHEXP")
        If intErrNo = 0 And ErrorChk = objGLtx.EnumGeneralLedgerTxErrorType.NoError Then
            Try
                intErrNo = objGLtx.mtdPostJournal("GL_CLSTRX_JOURNAL_INTER_ESTATE_CHECK", _
                                                  "GL_CLSTRX_JOURNAL_DETAIL_ADD", _
                                                  "GL_CLSTRX_JOURNAL_DETAIL_UPD", _
                                                  "GL_CLSTRX_JOURNAL_LINE_ADD", _
                                                  "GL_CLSTRX_JOURNAL_LINE_GET", _
                                                  "ADMIN_CLSLOC_INTER_ESTATE_DETAIL_GET", _
                                                  objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.Journal), _
                                                  objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.JournalLn), _
                                                  strCompany, _
                                                  strLocation, _
                                                  postAccMonth, _
                                                  postAccYear, _
                                                  strOppCd_GET, _
                                                  strOpCdStckTxDet_UPD, _
                                                  strUserId, _
                                                  colParam, _
                                                  ErrorChk, _
                                                  dsError)
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=CANCELUSAGE&errmesg=" & lblErrMessage.Text & "&redirect=GL/Trx/GL_Trx_PostJournal_List.aspx")
                End If
            End Try

            'update balik userid
            Dim strParamName As String
            Dim strParamValue As String
            Dim strOppCd_UPD As String = "GL_CLSTRX_POSTJOURNAL_LIST_UPD"
            Dim objAccYearDs As New Object

            strParamName = "LOCCODE|ACCYEAR|ACCMONTH"
            strParamValue = strLocation & "|" & postAccYear & "|" & postAccMonth

            Try
                intErrNo = objGLtx.mtdGetDataCommon(strOppCd_UPD, _
                                                    strParamName, _
                                                    strParamValue, _
                                                    objAccYearDs)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try

        End If
        If ErrorChk = 0 And dsError.Tables.Count = 0 Then
            dgErr.Visible = False
            lblErrMsg.Visible = False
        Else
            lblErrMsg.Visible = True
            Select Case ErrorChk
                Case 0 : lblErrMsg.Text = "Cannot post journal listed below due to respective error."
                Case 1 : lblErrMsg.Text = "Cannot post the journal due to system error"
                Case 2 : lblErrMsg.Text = "Cannot post the journal, please define " & GetCaption(objLangCap.EnumLangCap.Account) & " for " & GetCaption(objLangCap.EnumLangCap.Location) & " " & strLocation & "."
                Case 3 : lblErrMsg.Text = "Cannot post the journal, please define the accounting period for all " & GetCaption(objLangCap.EnumLangCap.Location) & "s."
            End Select

            dgErr.Visible = False
            If dsError.Tables.Count <> 0 Then
                If dsError.Tables(0).Rows.Count = 0 Then
                    lblErrMsg.Visible = False
                Else
                    dgErr.Visible = True
                    dgErr.DataSource = dsError
                    dgErr.DataBind()
                End If
            End If
        End If
        BindSearchList()
        BindGrid()
        BindPageList()
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
            intErrNo = objGLtx.mtdGetDataCommon(strOpCd, _
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
