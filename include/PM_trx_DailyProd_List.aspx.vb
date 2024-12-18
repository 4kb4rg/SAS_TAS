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

Public Class PM_DailyProdList : Inherits Page

    ''DAILY
    Protected WithEvents dgDailyProdList As DataGrid
    Protected WithEvents dgTBSGroup As DataGrid
    Protected WithEvents dgFFBTTanam As DataGrid
    Protected WithEvents dgFFBSupp As DataGrid
    Protected WithEvents dgDisp As DataGrid
    Protected WithEvents dgWeightOth As DataGrid
    ''DAILY
    Protected WithEvents dgMonthlyProdList As DataGrid
    Protected WithEvents dgFFBMonthly As DataGrid
    Protected WithEvents dgSumDisp As DataGrid



    Protected WithEvents lblTracker As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblFmt As Label
    Protected WithEvents lblDate As Label

    Protected WithEvents chkIsAll As checkbox

    Protected WithEvents srchTransDate As TextBox
    Protected WithEvents srchUpdateBy As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lstDropList As DropDownList

    Protected WithEvents lstAccMonth As DropDownList
    Protected WithEvents lstAccYear As DropDownList

    Protected objPMTrx As New agri.PM.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objOk As New agri.GL.ClsTrx()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objSysCfg As New agri.PWSystem.clsConfig()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intPMAR As Integer
    Dim objProdDs As New DataSet()
    Dim strDateFormat As String
    Dim fNom As String = "#,###."
    Dim fAngka As String = "#,###.#0"

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PMACCMONTH")
        strAccYear = Session("SS_PMACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intPMAR = Session("SS_PMAR")
        strDateFormat = Session("SS_DATEFMT")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMDailyProduction), intPMAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.Text = "" Then
                SortExpression.Text = "TransDate"
            End If

            If Not Page.IsPostBack Then
                BindAccYear(Session("SS_SELACCYEAR"))
                lstAccMonth.Text = Session("SS_SELACCMONTH")
 
                ''DAILY
                BindGrid()
                ''MONTHLY
                BindGrid_Monthly()

            End If
        End If
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgDailyProdList.CurrentPageIndex = 0
        dgDailyProdList.EditItemIndex = -1
        ''DAILY
        BindGrid()
        BindPageList()
        ''MONTHLY
        BindGrid_Monthly()

    End Sub

    Sub EmpLink_Click(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)

        If Not E.CommandName.ToString = "Sort" And Not E.CommandName.ToString = "Delete" Then
            Dim lbl As Label
            Dim intIndex As Integer = E.Item.ItemIndex
            Dim strSuppCode As String = ""
            Dim strItemCode As String = ""
            Dim strService As String = ""

            lbl = dgDailyProdList.Items.Item(intIndex).FindControl("lblEditTransDate")

            Dim strTrxDate As String = lbl.Text

            'strTrxDate = CheckDate(strTrxDate)


            Response.Write("<Script Language=""JavaScript"">pop_Att=window.open(""../../reports/PM_StdRpt_DailyProdReportPreview.aspx?redirect=attm&TransDate=" & strTrxDate & _
            "&strddlAccMth=" & strAccMonth & _
            "&strddlAccYr=" & strAccYear & _
            "&Location=" & strLocation & """, null ,""'pop_Att',width=1000,height=500,top=100,left=100,status=yes, resizable=no, scrollbars=yes, toolbar=no, location=no"");pop_Att.focus();</Script>")
             
        End If
    End Sub

    Sub Check_CheckedChanged()
        If chkIsAll.Checked = True Then
            dgDailyProdList.Pagesize = 31
        Else
            dgDailyProdList.Pagesize = 10
        End If

        BindGrid()
    End Sub


    

    Sub dgLine_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        'If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
        '    e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue'")
        '    If e.Item.ItemType = ListItemType.AlternatingItem Then
        '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
        '    Else
        '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")
        '    End If
        'End If
    End Sub

    Sub dgFFBSupp_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)


        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue'")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")
            End If
        End If
    End Sub

    Sub dgTBSGroup_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue'")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")
            End If
        End If
    End Sub

    Private Sub dgDailyProdList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDailyProdList.ItemDataBound
        If e.Item.ItemType = ListItemType.Header Then
            Dim dgItem As DataGridItem

            dgItem = New DataGridItem(0, 0, ListItemType.Header)

            e.Item.Cells(0).Visible = False
            e.Item.Cells(1).Visible = False
            e.Item.Cells(2).Visible = False
        End If

        Dim nGrossTBS As Double = 0
        Dim nCPODespatch As Double = 0
        Dim nPKDespatch As Double = 0

        nGrossTBS = 0
        nCPODespatch = 0

        For intCnt = 0 To dgDailyProdList.Items.Count - 1
            nGrossTBS = nGrossTBS + lCDbl(CType(dgDailyProdList.Items(intCnt).FindControl("lblFFBBGross"), Label).Text)
            nCPODespatch = nCPODespatch + lCDbl(CType(dgDailyProdList.Items(intCnt).FindControl("lblDespatchCPO"), Label).Text)
            nPKDespatch = nPKDespatch + lCDbl(CType(dgDailyProdList.Items(intCnt).FindControl("lblDespatchPK"), Label).Text)
        Next


        If e.Item.ItemType = ListItemType.Footer Then
            e.Item.Cells(3).Text = objGlobal.GetIDDecimalSeparator_FreeDigit(nGrossTBS, 0)
            e.Item.Cells(9).Text = objGlobal.GetIDDecimalSeparator_FreeDigit(nCPODespatch, 0)
            e.Item.Cells(14).Text = objGlobal.GetIDDecimalSeparator_FreeDigit(nPKDespatch, 0)
        End If

    End Sub

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgDailyProdList.CurrentPageIndex = 0
            Case "prev"
                dgDailyProdList.CurrentPageIndex = _
                Math.Max(0, dgDailyProdList.CurrentPageIndex - 1)
            Case "next"
                dgDailyProdList.CurrentPageIndex = _
                Math.Min(dgDailyProdList.PageCount - 1, dgDailyProdList.CurrentPageIndex + 1)
            Case "last"
                dgDailyProdList.CurrentPageIndex = dgDailyProdList.PageCount - 1
        End Select

        lstDropList.SelectedIndex = dgDailyProdList.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgDailyProdList.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
            lsumgrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgDailyProdList.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        'SortExpression.Text = e.SortExpression.ToString()
        'SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        'dgDailyProdList.CurrentPageIndex = lstDropList.SelectedIndex
        'BindGrid()
    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strddlAccMth As String
        Dim strddlAccYr As String
        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String = "0"
        Dim lbl As Label
        Dim strTrxDate As String

        strddlAccMth = lstAccMonth.SelectedItem.Value
        strddlAccYr = lstAccYear.SelectedItem.Value
        strUserLoc = strLocation
         
        lbl = E.Item.FindControl("lblTransDate")
        strTrxDate = Date_Validation(lbl.Text, False)
        'strTrxDate = CheckDate(lbl.Text)W

        If strTrxDate Is Nothing OrElse strTrxDate.Trim().Length() = 0 Then
            Exit Sub
        End If
        'file:///
        'rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strFileName & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        Response.Write("<Script Language=""JavaScript"">window.open(""Web\En\reports\PM_StdRpt_DailyProdReportPreview.aspx?Type=Print&CompName=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & _
                       "&strddlAccMth=" & strddlAccMth & "&strddlAccYr=" & strddlAccYr & "&Decimal=" & strDec & "&lblLocation=" & strLocation & "&TransDate=" & strTrxDate & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        dgDailyProdList.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub NewBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PM_trx_DailyProd_Det.aspx")
    End Sub


    Function Date_Validation(ByVal pv_strInputDate As String, ByVal pv_blnIsShortDate As Boolean) As String
        Dim objSysCfgDs As New Object
        Dim objActualDate As New Object
        Dim strDateFormat As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim strAcceptFormat As String

        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"

        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CB_DEPOSIT_GET_CONFIG&errmesg=" & Exp.Message & "&redirect=CB/trx/cb_trx_DepositList.aspx")
        End Try

        strDateFormat = objSysCfg.mtdGetDateFormat(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt").Trim())

        If pv_blnIsShortDate Then
            Date_Validation = objGlobal.GetShortDate(strDateFormat, pv_strInputDate)
        Else
            If objGlobal.mtdValidInputDate(strDateFormat, _
                                           pv_strInputDate, _
                                           strAcceptFormat, _
                                           objActualDate) = True Then
                Date_Validation = objActualDate
            Else
                Date_Validation = ""
            End If
        End If
    End Function

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

        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim objProd As New DataSet()

        Dim strOppCode_Get As String = "PM_CLSTRX_MILL_PROD_DAILY_GET"
        Dim intErrNo As Integer

        strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
        strParamValue = lstAccMonth.SelectedItem.Value & "|" & lstAccYear.SelectedItem.Value & "|" & strLocation

        Try
            intErrNo = objOk.mtdGetDataCommon(strOppCode_Get, strParamName, strParamValue, objProd)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=wm_ticket&errmesg=" & Exp.Message & "&redirect=")
        End Try

        ''0 pengolahan
        dgDailyProdList.DataSource = New DataView(objProd.Tables(0))
        dgDailyProdList.Dispose()
        dgDailyProdList.DataBind()


        ''1 Penerimaan TBS
        dgFFBSupp.DataSource = New DataView(objProd.Tables(1))
        dgFFBSupp.Dispose()
        dgFFBSupp.DataBind()


        ''2 Dispatch
        dgDisp.DataSource = New DataView(objProd.Tables(2))
        dgDisp.Dispose()
        dgDisp.DataBind()


        ''3 Timbangan Lainnya
        dgWeightOth.DataSource = New DataView(objProd.Tables(3))
        dgWeightOth.Dispose()
        dgWeightOth.DataBind()


        ''4 Timbangan Lainnya
        dgFFBTTanam.DataSource = New DataView(objProd.Tables(4))
        dgFFBTTanam.Dispose()
        dgFFBTTanam.DataBind()

        PageCount = objGlobal.mtdGetPageCount(objProd.Tables(0).Rows.Count, dgDailyProdList.PageSize)

        'dgDailyProdList.DataSource = dsData
        If dgDailyProdList.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgDailyProdList.CurrentPageIndex = 0
            Else
                dgDailyProdList.CurrentPageIndex = PageCount - 1
            End If
        End If


        BindPageList()
        PageNo = dgDailyProdList.CurrentPageIndex + 1
        lblTracker.Text = "Page " & pageno & " of " & dgDailyProdList.PageCount

        lSumGrid()
    End Sub


    Sub BindGrid_Monthly()
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim objProd As New DataSet()

        Dim strOppCode_Get As String = "PM_CLSTRX_MILL_PROD_MONTHLY_GET"
        Dim intErrNo As Integer

        strParamName = "ACCYEAR|ACCMONTH|LOCCODE"
        strParamValue = lstAccYear.SelectedItem.Value & "|" & lstAccMonth.SelectedItem.Value & "|" & strLocation

        Try
            intErrNo = objOk.mtdGetDataCommon(strOppCode_Get, strParamName, strParamValue, objProd)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=wm_ticket&errmesg=" & Exp.Message & "&redirect=")
        End Try

        dgMonthlyProdList.DataSource = New DataView(objProd.Tables(0))
        dgMonthlyProdList.Dispose()
        dgMonthlyProdList.DataBind()

        dgFFBMonthly.DataSource = New DataView(objProd.Tables(1))
        dgFFBMonthly.Dispose()
        dgFFBMonthly.DataBind()

        dgSumDisp.DataSource = New DataView(objProd.Tables(2))
        dgSumDisp.Dispose()
        dgSumDisp.DataBind()


        lSumMonthly()

    End Sub


    Sub BindPageList()
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgDailyProdList.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While

        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgDailyProdList.CurrentPageIndex

    End Sub

    Function lCDbl(ByVal pcVal As String) As Double
        If IsNumeric(pcVal) Then lCDbl = CDbl(pcVal) Else lCDbl = 0
    End Function

    Sub lSumGrid()


        Dim nNettoTBS As Double = 0
        Dim nPotTBS As Double = 0
        Dim nGrossTBS As Double = 0
        Dim nNettoDesp As Double = 0
        Dim nTBSOlah As Double = 0
        Dim nTBSAvailale As Double = 0
        Dim nCPOProses As Double = 0
        Dim nCPOKirim As Double = 0
        Dim nPKProses As Double = 0
        Dim nPKKirim As Double = 0

        ''Control Sheet

        nGrossTBS = 0
        nCPOProses = 0
        For intCnt = 0 To dgDailyProdList.Items.Count - 1

            nGrossTBS = nGrossTBS + lCDbl(CType(dgDailyProdList.Items(intCnt).FindControl("lblSheetFFBBGross"), Label).Text)
            nTBSAvailale = nTBSAvailale + lCDbl(CType(dgDailyProdList.Items(intCnt).FindControl("lblSheetFFBBAvl"), Label).Text)
            nTBSOlah = nTBSOlah + lCDbl(CType(dgDailyProdList.Items(intCnt).FindControl("lblSheetFFBProc"), Label).Text)
            nCPOKirim = nCPOKirim + lCDbl(CType(dgDailyProdList.Items(intCnt).FindControl("lblDespatchCPO"), Label).Text)
            nCPOProses = nCPOProses + lCDbl(CType(dgDailyProdList.Items(intCnt).FindControl("lblSheetCPOProc"), Label).Text)

            nPKProses = nPKProses + lCDbl(CType(dgDailyProdList.Items(intCnt).FindControl("lblSheetPKProc"), Label).Text)
            nPKKirim = nPKKirim + lCDbl(CType(dgDailyProdList.Items(intCnt).FindControl("lblDespatchPK"), Label).Text)

        Next

        CType(getFooter(dgDailyProdList).FindControl("lblTotSheetFFBBGross"), Label).Text = Format(nGrossTBS, FNom)
        CType(getFooter(dgDailyProdList).FindControl("lblTotSheetFFBBAvl"), Label).Text = Format(nTBSAvailale, FNom)
        CType(getFooter(dgDailyProdList).FindControl("lblTotSheetFFBProc"), Label).Text = Format(nTBSOlah, FNom)
        CType(getFooter(dgDailyProdList).FindControl("lblTotSheetCPODesp"), Label).Text = Format(nCPOKirim, FNom)
        CType(getFooter(dgDailyProdList).FindControl("lblTotSheetCPOProc"), Label).Text = Format(nCPOProses, FNom)

        CType(getFooter(dgDailyProdList).FindControl("lblTotSheetPKProc"), Label).Text = Format(nPKProses, FNom)
        CType(getFooter(dgDailyProdList).FindControl("lblTotDespatchPK"), Label).Text = Format(nPKKirim, FNom)

        '''--Penerimaan TBS
        nNettoTBS = 0
        nGrossTBS = 0
        nPotTBS = 0

        For intCnt = 0 To dgFFBSupp.Items.Count - 1
            nNettoTBS = nNettoTBS + lCDbl(CType(dgFFBSupp.Items(intCnt).FindControl("lblNweight"), Label).Text)
            nPotTBS = nPotTBS + lCDbl(CType(dgFFBSupp.Items(intCnt).FindControl("lblPotweight"), Label).Text)
            nGrossTBS = nGrossTBS + lCDbl(CType(dgFFBSupp.Items(intCnt).FindControl("lblGweight"), Label).Text)
        Next


        CType(getFooter(dgFFBSupp).FindControl("lbTotFFBGross"), Label).Text = Format(nGrossTBS, FNom)
        CType(getFooter(dgFFBSupp).FindControl("lblTotFFBPot"), Label).Text = Format(nPotTBS, Fnom)
        CType(getFooter(dgFFBSupp).FindControl("lbTotFFBNetto"), Label).Text = Format(nNettoTBS, FNom)


        ''---despathc
        For intCnt = 0 To dgDisp.Items.Count - 1
            nNettoDesp = nNettoDesp + lCDbl(CType(dgDisp.Items(intCnt).FindControl("lblNweight"), Label).Text)
        Next

        CType(getFooter(dgDisp).FindControl("lbTotDisp"), Label).Text = Format(nNettoDesp, FNom)


        '' ----By Tahun Tanam
        nNettoTBS = 0
        nGrossTBS = 0
        nPotTBS = 0

        For intCnt = 0 To dgFFBTTanam.Items.Count - 1
            nNettoTBS = nNettoTBS + lCDbl(CType(dgFFBTTanam.Items(intCnt).FindControl("lblNweight"), Label).Text)
            nPotTBS = nPotTBS + lCDbl(CType(dgFFBTTanam.Items(intCnt).FindControl("lblPotweight"), Label).Text)
            nGrossTBS = nGrossTBS + lCDbl(CType(dgFFBTTanam.Items(intCnt).FindControl("lblGweight"), Label).Text)
        Next

        CType(getFooter(dgFFBTTanam).FindControl("lbTotFFBTTGros"), Label).Text = Format(nGrossTBS, FNom)
        CType(getFooter(dgFFBTTanam).FindControl("lbTotFFBTTPot"), Label).Text = Format(nPotTBS, Fnom)
        CType(getFooter(dgFFBTTanam).FindControl("lbTotFFBTTNettto"), Label).Text = Format(nNettoTBS, FNom)


    End Sub

    Sub lSumMonthly()

        Dim nNettoTBS As Double = 0
        Dim nPotTBS As Double = 0
        Dim nGrossTBS As Double = 0
        Dim nNettoDesp As Double = 0
        Dim nTBSOlah As Double = 0
        Dim nTBSAvailale As Double = 0
        Dim nCPOProses As Double = 0
        Dim nCPOKirim As Double = 0
        Dim nPKProses As Double = 0
        Dim nPKKirim As Double = 0

        ''Control Sheet

        nGrossTBS = 0
        nCPOProses = 0
        For intCnt = 0 To dgMonthlyProdList.Items.Count - 1

            nGrossTBS = nGrossTBS + lCDbl(CType(dgMonthlyProdList.Items(intCnt).FindControl("lblSheetSumFFB"), Label).Text)
            nTBSAvailale = nTBSAvailale + lCDbl(CType(dgMonthlyProdList.Items(intCnt).FindControl("lblSheetSumFFBAVl"), Label).Text)
            nTBSOlah = nTBSOlah + lCDbl(CType(dgMonthlyProdList.Items(intCnt).FindControl("lblSheetSumFFBProc"), Label).Text)

            nCPOProses = nCPOProses + lCDbl(CType(dgMonthlyProdList.Items(intCnt).FindControl("lblSheetSumCPOProc"), Label).Text)
            nCPOKirim = nCPOKirim + lCDbl(CType(dgMonthlyProdList.Items(intCnt).FindControl("lblSheetSumCPODesp"), Label).Text)

            nPKProses = nPKProses + lCDbl(CType(dgMonthlyProdList.Items(intCnt).FindControl("lblSheetSumPKProc"), Label).Text)
            nPKKirim = nPKKirim + lCDbl(CType(dgMonthlyProdList.Items(intCnt).FindControl("lblSheetSumPKDesp"), Label).Text)

        Next

        CType(getFooter(dgMonthlyProdList).FindControl("lblSheetTotSumFFB"), Label).Text = Format(nGrossTBS, FNom)
        CType(getFooter(dgMonthlyProdList).FindControl("lblSheetTotSumFFBAvl"), Label).Text = Format(nTBSAvailale, FNom)
        CType(getFooter(dgMonthlyProdList).FindControl("lblSheetTotSumFFBProc"), Label).Text = Format(nTBSOlah, FNom)

        CType(getFooter(dgMonthlyProdList).FindControl("lblSheetTotSumCPOProc"), Label).Text = Format(nCPOProses, FNom)
        CType(getFooter(dgMonthlyProdList).FindControl("lblSheetTotSumCPODesp"), Label).Text = Format(nCPOKirim, FNom)

        CType(getFooter(dgMonthlyProdList).FindControl("lblSheetTotSumPKProc"), Label).Text = Format(nPKProses, FNom)
        CType(getFooter(dgMonthlyProdList).FindControl("lblSheetTotSumPKDesp"), Label).Text = Format(nPKKirim, FNom)

        CType(getFooter(dgMonthlyProdList).FindControl("lblSheetMonthlyPK"), Label).Text = Format(Divide(nPKProses * 100, nTBSOlah), Fangka)
        CType(getFooter(dgMonthlyProdList).FindControl("lblSheetMonthlyCPO"), Label).Text = Format(Divide(nCPOProses * 100, nTBSOlah), Fangka)
         
        '''--Penerimaan TBS
        nNettoTBS = 0
        nGrossTBS = 0
        nPotTBS = 0

        For intCnt = 0 To dgFFBMonthly.Items.Count - 1
            nNettoTBS = nNettoTBS + lCDbl(CType(dgFFBMonthly.Items(intCnt).FindControl("lblNweight"), Label).Text)
            nPotTBS = nPotTBS + lCDbl(CType(dgFFBMonthly.Items(intCnt).FindControl("lblPotweight"), Label).Text)
            nGrossTBS = nGrossTBS + lCDbl(CType(dgFFBMonthly.Items(intCnt).FindControl("lblGweight"), Label).Text)
        Next


        CType(getFooter(dgFFBMonthly).FindControl("lblFFBSumGross"), Label).Text = Format(nGrossTBS, FNom)
        CType(getFooter(dgFFBMonthly).FindControl("lblFFBSumPot"), Label).Text = Format(nPotTBS, Fnom)
        CType(getFooter(dgFFBMonthly).FindControl("lblFFBSumNet"), Label).Text = Format(nNettoTBS, FNom)


        ''---despathc
        For intCnt = 0 To dgSumDisp.Items.Count - 1
            nNettoDesp = nNettoDesp + lCDbl(CType(dgSumDisp.Items(intCnt).FindControl("lblMonthlyNweight"), Label).Text)
        Next

        CType(getFooter(dgSumDisp).FindControl("lblSumMonthlyNweight"), Label).Text = Format(nNettoDesp, FNom)


    End Sub

    Function getFooter(ByVal grid As DataGrid) As DataGridItem
        For Each ctrl As WebControl In grid.Controls(0).Controls
            'loop DataGridTable
            If TypeOf ctrl Is System.Web.UI.WebControls.DataGridItem Then
                Dim item As DataGridItem = DirectCast(ctrl, DataGridItem)
                If item.ItemType = ListItemType.Footer Then Return item
            End If
        Next
    End Function


    Function Divide(ByVal Val1 As Double, ByVal Val2 As Double) As Double
        Dim nVal As Double

        If IsNothing(Val2) Then
            nVal = 0
        Else
            If Val2 = 0 Then
                nVal = 0
            Else
                nVal = Val1 / Val2
            End If
        End If

        Divide = nVal
    End Function

End Class
