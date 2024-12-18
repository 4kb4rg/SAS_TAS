

Imports System
Imports System.Data
Imports System.Collections
Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic


Public Class PU_HistoricalPrices : Inherits Page

    Protected WithEvents dgPOList As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents txtItemCode As TextBox
    Protected WithEvents txtSuppCode As TextBox
    Protected WithEvents txtService As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents ddlProdCat As DropDownList
  
    Protected objPU As New agri.PU.clsTrx()
    Protected objPUSetup As New agri.PU.clsSetup()
    Protected objINSetup As New agri.IN.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGLtrx As New agri.GL.ClsTrx()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intPUAR As Integer
    Dim intFAAR As Integer

    Dim objPODs As New Object()
    Dim objPOLnDs As New Object()
    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim strSuppCode As String
    Dim strItemCode As String
    Dim strService As String
    Dim strProdCat As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PUACCMONTH")
        strAccYear = Session("SS_PUACCYEAR")
        strSelAccMonth = Session("SS_SELACCMONTH")
        strSelAccYear = Session("SS_SELACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intPUAR = Session("SS_PUAR")
        intFAAR = Session("SS_FAAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUPurchaseOrder), intPUAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.Text = "" Then
                SortExpression.Text = "A.POId"
            End If

            If Not Page.IsPostBack Then
                BindProdCat()
                BindGrid()
                BindPageList()
            End If
        End If
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        strSuppCode = IIf(txtSuppCode.Text = "", "", Trim(txtSuppCode.Text))
        strItemCode = IIf(txtItemCode.Text = "", "", Trim(txtItemCode.Text))
        strService = IIf(txtService.Text = "", "", Trim(txtService.Text))
        strProdCat = IIf(ddlProdCat.SelectedItem.Value = "", "", Trim(ddlProdCat.SelectedItem.Value))
        If strSuppCode = "" And strItemCode = "" And strService = "" And strProdCat = "" Then
            Exit Sub
        End If

        dgPOList.CurrentPageIndex = 0
        dgPOList.EditItemIndex = -1
        BindGrid()
        BindPageList()
    End Sub

    Sub BindGrid()
        Dim strOpCd As String = "PU_STDRPT_HISTORICAL_PRICES_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim lbButton As LinkButton
        Dim lbl As Label
        Dim strParamName As String
        Dim strParamValue As String


        strSuppCode = IIf(txtSuppCode.Text = "", "", Trim(txtSuppCode.Text))
        strItemCode = IIf(txtItemCode.Text = "", "", Trim(txtItemCode.Text))
        strService = IIf(txtService.Text = "", "", Trim(txtService.Text))
        strProdCat = IIf(ddlProdCat.SelectedItem.Value = "", "", Trim(ddlProdCat.SelectedItem.Value))
        If strSuppCode = "" And strItemCode = "" And strService = "" And strProdCat = "" Then
            Exit Sub
        End If

        strParamName = "STRITEM|STRSERVICE|STRSUPPLIER|STRPRODCAT|SORTBY"
        strParamValue = strItemCode & "|" & strService & "|" & strSuppCode & "|" & strProdCat & "|" & SortExpression.Text & SortCol.Text

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objPODs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        PageCount = objGlobal.mtdGetPageCount(objPODs.Tables(0).Rows.Count, dgPOList.PageSize)
        dgPOList.DataSource = objPODs
        If dgPOList.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgPOList.CurrentPageIndex = 0
            Else
                dgPOList.CurrentPageIndex = PageCount - 1
            End If
        End If
        dgPOList.DataBind()
        BindPageList()

        PageNo = dgPOList.CurrentPageIndex + 1
        lblTracker.Text = "Page " & PageNo & " of " & dgPOList.PageCount
    End Sub

    Sub BindPageList()
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgPOList.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgPOList.CurrentPageIndex
    End Sub

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument

        strSuppCode = IIf(txtSuppCode.Text = "", "", Trim(txtSuppCode.Text))
        strItemCode = IIf(txtItemCode.Text = "", "", Trim(txtItemCode.Text))
        strService = IIf(txtService.Text = "", "", Trim(txtService.Text))
        If strSuppCode = "" And strItemCode = "" And strService = "" Then
            Exit Sub
        End If

        Select Case direction
            Case "first"
                dgPOList.CurrentPageIndex = 0
            Case "prev"
                dgPOList.CurrentPageIndex = _
                    Math.Max(0, dgPOList.CurrentPageIndex - 1)
            Case "next"
                dgPOList.CurrentPageIndex = _
                    Math.Min(dgPOList.PageCount - 1, dgPOList.CurrentPageIndex + 1)
            Case "last"
                dgPOList.CurrentPageIndex = dgPOList.PageCount - 1
        End Select
        lstDropList.SelectedIndex = dgPOList.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgPOList.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgPOList.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = " ASC", " DESC", " ASC")
        dgPOList.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub PreviewPO(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim lbl As Label
        Dim strPOID As String
        Dim strSuppCode As String
        Dim strItemCode As String
        Dim strService As String

        lbl = E.Item.FindControl("lblPOID")
        strPOID = lbl.Text.Trim
        lbl = E.Item.FindControl("lblSupplierCode")
        strSuppCode = lbl.Text.Trim
        lbl = E.Item.FindControl("lblItemCode")
        strItemCode = lbl.Text.Trim
        lbl = E.Item.FindControl("lblService")
        strService = lbl.Text.Trim

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/PU_Rpt_POPFHistoryDet.aspx?Type=Print&CompName=" & strCompany & _
                               "&Location=" & strLocation & _
                               "&RptId=" & "" & _
                               "&RptName=" & "" & _
                               "&Decimal=" & 0 & _
                               "&PONoFrom=" & strPOID & _
                               "&PONoTo=" & "" & _
                               "&PeriodeFrom=" & "" & _
                               "&PeriodeTo=" & "" & _
                               "&PIC1=" & _
                               "&Jabatan1=" & _
                               "&PIC2=" & _
                               "&Jabatan2=" & _
                               "&Catatan=" & _
                               "&Lokasi=" & _
                               "&SuppCode=" & strSuppCode & _
                               "&ItemCode=" & strItemCode & _
                               "&Service=" & strService & _
                               "&SentPeriod=" & _
                               """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

 
    End Sub

    Sub BindProdCat()
        Dim strOpCd As String = "IN_CLSSETUP_PRODCAT_LIST_GET"
        Dim objCatDs As New Object()
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer = 0
        Dim dr As DataRow
        Dim strParamName As String
        Dim strParamValue As String

        strParamName = "SEARCHSTR|SORTEXP"
        strParamValue = "AND PCat.Status = '1'|ORDER BY PCat.ProdCatCode"

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objCatDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=INVOICERCV_GET_PODETAIL&errmesg=" & lblErrMessage.Text & "&redirect=ap/trx/ap_trx_invrcvlist.aspx")
        End Try

        For intCnt = 0 To objCatDs.Tables(0).Rows.Count - 1
            objCatDs.Tables(0).Rows(intCnt).Item("ProdCatCode") = Trim(objCatDs.Tables(0).Rows(intCnt).Item("ProdCatCode"))
            objCatDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objCatDs.Tables(0).Rows(intCnt).Item("ProdCatCode")) & " (" & _
                                                              Trim(objCatDs.Tables(0).Rows(intCnt).Item("Description")) & ") "
        Next intCnt

        dr = objCatDs.Tables(0).NewRow()
        dr("ProdCatCode") = ""
        dr("Description") = "Please Select Product Category"
        objCatDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlProdCat.DataSource = objCatDs.Tables(0)
        ddlProdCat.DataValueField = "ProdCatCode"
        ddlProdCat.DataTextField = "Description"
        ddlProdCat.DataBind()
        ddlProdCat.SelectedIndex = intSelectedIndex
    End Sub
End Class
