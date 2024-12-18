
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

Public Class PM_KernelQualityList : Inherits Page
     

    Protected objPMTrx As New agri.PM.clsTrx()
    Protected objPMSetup As New agri.PM.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Protected objGLTrx As New agri.GL.ClsTrx()

    Dim strCompany As String
    Dim strLocation As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intPMAR As Integer
    Dim objKernelQuality As New DataSet()
    Dim objProcessingLine As New DataSet()
    Dim objMachine As New DataSet()
    Dim strDateFormat As String


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strAccMonth = Session("SS_PMACCMONTH")
        strAccYear = Session("SS_PMACCYEAR")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intPMAR = Session("SS_PMAR")
        strDateFormat = Session("SS_DATEFMT")

        NewBtn.Visible = False

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMKernelQuality), intPMAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.Text = "" Then
                SortExpression.Text = "TransDate"
            End If

            If Not Page.IsPostBack Then
                lstAccMonth.Text = Session("SS_SELACCMONTH")
                BindAccYear(Session("SS_SELACCYEAR"))

                BindSearchList()

                BindGrid()

                BindPageList()
            End If
        End If
    End Sub

    Sub BindSearchList()

        srchStatusList.Items.Add(New ListItem(objPMTrx.mtdGetKernelQualityStatus(objPMTrx.EnumKernelQualityStatus.Active), objPMTrx.EnumKernelQualityStatus.Active))
        srchStatusList.Items.Add(New ListItem(objPMTrx.mtdGetKernelQualityStatus(objPMTrx.EnumKernelQualityStatus.Deleted), objPMTrx.EnumKernelQualityStatus.Deleted))
        srchStatusList.Items.Add(New ListItem(objPMTrx.mtdGetKernelQualityStatus(objPMTrx.EnumKernelQualityStatus.All), objPMTrx.EnumKernelQualityStatus.All))

    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgKernelQualityList.CurrentPageIndex = 0
        dgKernelQualityList.EditItemIndex = -1
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
        
        dsData = LoadData
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgKernelQualityList.PageSize)

        dgKernelQualityList.DataSource = dsData
        If dgKernelQualityList.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgKernelQualityList.CurrentPageIndex = 0
            Else
                dgKernelQualityList.CurrentPageIndex = PageCount - 1
            End If
        End If
        
        dgKernelQualityList.DataBind()
        BindPageList()
        PageNo = dgKernelQualityList.CurrentPageIndex + 1
        lblTracker.Text="Page " & pageno & " of " & dgKernelQualityList.PageCount

        intStatus = CInt(srchStatusList.SelectedItem.Value)

        For intCnt = 0 To dgKernelQualityList.Items.Count - 1
            Select Case intStatus
                Case objPMTrx.EnumDailyProdStatus.Active
                    lbButton = dgKernelQualityList.Items.Item(intCnt).FindControl("Delete")
                    lbButton.Visible = True
                    lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Case objPMTrx.EnumDailyProdStatus.Deleted
                    lbButton = dgKernelQualityList.Items.Item(intCnt).FindControl("Delete")
                    lbButton.Visible = False
            End Select
        Next

    End Sub

    Sub BindPageList()
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgKernelQualityList.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While

        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgKernelQualityList.CurrentPageIndex

    End Sub

    Protected Function LoadData() As DataSet
        Dim strOppCode_Get As String = "PM_CLSTRX_KERNEL_QUALITY_LIST_GET"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer

        strParamName = "SEARCHSTR"
        strParamValue = "Where Month(t.Indate)='" & lstAccMonth.SelectedItem.Value & "' And year(t.InDate)='" & lstAccYear.SelectedItem.Value & "' AND t.LocCode='" & strLocation & "'"

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOppCode_Get, strParamName, strParamValue, objKernelQuality)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PM_GET_DATA&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/PM_trx_OilQuality_List_KLR.aspx")
        End Try


        For intCnt = 0 To objKernelQuality.Tables(0).Rows.Count - 1
            objKernelQuality.Tables(0).Rows(intCnt).Item("UpdateDate") = Trim(objKernelQuality.Tables(0).Rows(intCnt).Item("UpdateDate"))
            objKernelQuality.Tables(0).Rows(intCnt).Item("Status") = Trim(objKernelQuality.Tables(0).Rows(intCnt).Item("Status"))
            objKernelQuality.Tables(0).Rows(intCnt).Item("UpdateID") = Trim(objKernelQuality.Tables(0).Rows(intCnt).Item("UpdateID"))
        Next

        Return objKernelQuality
    End Function

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgKernelQualityList.CurrentPageIndex = 0
            Case "prev"
                dgKernelQualityList.CurrentPageIndex = _
                Math.Max(0, dgKernelQualityList.CurrentPageIndex - 1)
            Case "next"
                dgKernelQualityList.CurrentPageIndex = _
                Math.Min(dgKernelQualityList.PageCount - 1, dgKernelQualityList.CurrentPageIndex + 1)
            Case "last"
                dgKernelQualityList.CurrentPageIndex = dgKernelQualityList.PageCount - 1
        End Select

        lstDropList.SelectedIndex = dgKernelQualityList.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgKernelQualityList.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgKernelQualityList.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        dgKernelQualityList.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub


    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOpCd_KernelQuality_Upd As String = "PM_CLSTRX_KERNEL_QUALITY_UPD"
        Dim strOpCd_KernelQuality_Add As String = ""
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim strSelectedLocCode As String
        Dim strSelectedTransDate As String
        Dim strProcessingLnNo As String
        Dim lblLocCode As Label
        Dim lblTransDate As Label
        Dim lblProcessingLnNo As Label


        dgKernelQualityList.EditItemIndex = CInt(E.Item.ItemIndex)
        lblLocCode = dgKernelQualityList.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblLocCode")
        strSelectedLocCode = lblLocCode.Text
        lblProcessingLnNo = dgKernelQualityList.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblProcessingLnNo")
        strProcessingLnNo = lblProcessingLnNo.Text
        lblTransDate = dgKernelQualityList.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblTransDate")
        strSelectedTransDate = lblTransDate.Text

        strParam = strSelectedTransDate & "|||||" & _
                   "||||2"

        Try
            intErrNo = objPMTrx.mtdUpdKernelQuality(strOpCd_KernelQuality_Add, _
                                                strOpCd_KernelQuality_Upd, _
                                                strLocation, _
                                                strUserId, _
                                                strAccMonth, _
                                                strAccYear, _
                                                strParam, _
                                                True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_OIL_LOSS_DEL&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/pm_trx_KernelQuality_list.aspx")
        End Try

        dgKernelQualityList.EditItemIndex = -1
        BindGrid()
    End Sub


    Sub NewBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PM_trx_KernelQuality_Det.aspx")
    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strSelectedLocCode As String
        Dim strSelectedTransDate As String
        Dim strProcessingLnNo As String
        Dim strSelectedMachineCode As String
        Dim QString As String

        Dim lblLocCode As Label
        Dim lblTransDate As Label
        Dim lblProcessingLnNo As Label
        Dim lblMachineCode As Label

        dgKernelQualityList.EditItemIndex = CInt(E.Item.ItemIndex)
        lblLocCode = dgKernelQualityList.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblLocCode")
        strSelectedLocCode = lblLocCode.Text
        lblTransDate = dgKernelQualityList.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblTransDate")
        strSelectedTransDate = lblTransDate.Text
        lblProcessingLnNo = dgKernelQualityList.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblProcessingLnNo")
        strProcessingLnNo = lblProcessingLnNo.Text


        QString = "?LocCode=" & Server.UrlEncode(Trim(strSelectedLocCode)) & _
                  "&TransDate=" & Server.UrlEncode(Trim(objGlobal.GetLongDate(strSelectedTransDate))) & _
                  "&ProcessingLnNo=" & Server.UrlEncode(Trim(strProcessingLnNo)) & _
                  "&Edit=True"
        Response.Redirect("PM_trx_KernelQuality_Det.aspx" & QString)
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
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
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



    Protected Function CheckDate() As String

        Dim strDateSetting As String
        Dim objSysCfgDs As DataSet
        Dim objDateFormat As String
        Dim strValidDate As String
        lblFmt.Visible = False
        lblDate.Visible = False
        lblFmt.Visible = False
        'If Not txtDate.Text = "" Then
        '    If objGlobal.mtdValidInputDate(strDateFormat, txtDate.Text, objDateFormat, strValidDate) = True Then
        '        Return strValidDate
        '    Else
        '        lblFmt.Visible = True
        '        lblFmt.Text = objDateFormat & "."
        '        lblDate.Visible = True
        '        lblFmt.Visible = True

        '    End If
        'End If
    End Function

End Class
