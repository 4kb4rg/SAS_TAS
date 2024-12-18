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
Imports agri.HR.clsSetup
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.GL


Public Class WM_Setup_FFBPriceList : Inherits Page

    Protected WithEvents dgLine As DataGrid
    Protected WithEvents lbDelete As LinkButton
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
 
    Protected WithEvents txtJobCode As TextBox
    Protected WithEvents ddlStatus As DropDownList

    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents srcpmonth As DropDownList
    Protected WithEvents srcpyear As DropDownList
    Protected WithEvents srchlocation As DropDownList
    Protected WithEvents btnGen As ImageButton

    Protected objHRSetup As New agri.HR.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objGLTrx As New agri.GL.clsTrx()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim ObjOK As New agri.GL.ClsTrx
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intPRAR As Long
    Dim ParamNama As String
    Dim ParamValue As String
    Dim objPriceDs As New Object()
    Dim intLevel As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intPRAR = Session("SS_PRAR")
        intLevel = Session("SS_USRLEVEL")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ' ElseIf (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRPaySetup), intPRAR) = False) And (intLevel < 2) Then
        '     Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            btnGen.Attributes("onclick") = "javascript:return ConfirmAction('generate');"
            lblErrMessage.Visible = False

            If SortExpression.Text = "" Then
                SortExpression.Text = "A.SalaryCode"
            End If
            
            If Not Page.IsPostBack Then
                BindSubLocation("")
                srcpmonth.SelectedIndex = CInt(Session("SS_SELACCMONTH")) - 1
                BindAccYear(Session("SS_SELACCYEAR"))
                BindGrid()
                BindPageList()
            End If
        End If
        btnGen.Visible = False

    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgLine.CurrentPageIndex = 0
        dgLine.EditItemIndex = -1
        BindGrid()
        BindPageList()
    End Sub

    Sub BindSubLocation(ByVal pKode As string)
        Dim strParamName As String
        Dim strParamValue As String
        Dim strOpCd_Get As String = "IN_CLSTRX_GLOBAL_UPD"

        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim sSQLKriteria As String = ""
        Dim objdsST As New DataSet
        Dim intSelectedIndex As Integer
 
        sSQLKriteria="SELECT SubLocCode,SubDescription From SH_LOCATIONSUB  Where LocCode='" & strlocation & "' "
        strParamName = "STRSEARCH"
        strParamValue = sSQLKriteria

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd_Get, _
                                         strParamName, _
                                         strParamValue, _
                                         objdsST)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=STOCK_ADJUSTMENT_UPDATE_FAILED&errmesg=&redirect=IN/Trx/IN_trx_StockAdj_list.aspx")
        End Try
 
         For intCnt = 0 To objdsST.Tables(0).Rows.Count - 1
            objdsST.Tables(0).Rows(intCnt).Item("SubLocCode") = Trim(objdsST.Tables(0).Rows(intCnt).Item("SubLocCode"))
            objdsST.Tables(0).Rows(intCnt).Item("SubDescription") = objdsST.Tables(0).Rows(intCnt).Item("SubDescription") & " (" & Trim(objdsST.Tables(0).Rows(intCnt).Item("SubLocCode")) & ")"
            If objdsST.Tables(0).Rows(intCnt).Item("SubLocCode") = Trim(pKode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objdsST.Tables(0).NewRow()
        dr("SubLocCode") = "" 
        dr("SubDescription") = "Please Select Location" 
        objdsST.Tables(0).Rows.InsertAt(dr, 0)

        srchlocation.DataSource = objdsST.Tables(0)
        srchlocation.DataValueField = "SubLocCode"
        srchlocation.DataTextField = "SubDescription"
        srchlocation.DataBind()
        srchlocation.SelectedIndex = intSelectedIndex
 
    End Sub

    Sub BindAccYear(ByVal pv_strAccYear As String)
        Dim strParamName As String
        Dim strParamValue As String
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim intselection As Integer = 0
        Dim objAccYearDs As New Object
        Dim strOpCd As String = "ADMIN_CLSACCPERIOD_CONFIG_GET"

        strParamName = "LOCCODE|SEARCHSTR|SORTEXP"
        strParamValue = strLocation & "||Order By HD.AccYear"

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objAccYearDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & Exp.Message & "&redirect=")
        End Try

        For intCnt = 0 To objAccYearDs.Tables(0).Rows.Count - 1
            objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear") = Trim(objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear"))
            objAccYearDs.Tables(0).Rows(intCnt).Item("UserName") = Trim(objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear"))
            If pv_strAccYear = Trim(objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear")) Then
                'intselection = intCnt + 1
                intselection = intCnt
            End If
        Next intCnt

        'dr = objAccYearDs.Tables(0).NewRow()
        'dr("AccYear") = ""
        'dr("UserName") = "All"
        'objAccYearDs.Tables(0).Rows.InsertAt(dr, 0)

        srcpyear.DataSource = objAccYearDs.Tables(0)
        srcpyear.DataValueField = "AccYear"
        srcpyear.DataTextField = "UserName"
        srcpyear.DataBind()
        srcpyear.SelectedIndex = intselection
    End Sub
     

    Sub BindGrid()
        Dim intCnt As Integer
        Dim lbButton As LinkButton
        Dim lbl As Label

        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadData()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgLine.PageSize)

        dgLine.DataSource = dsData
        If dgLine.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgLine.CurrentPageIndex = 0
            Else
                dgLine.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgLine.DataBind()
        BindPageList()
        PageNo = dgLine.CurrentPageIndex + 1
        lblTracker.Text = "Page " & PageNo & " of " & dgLine.PageCount

        

    End Sub

    Sub BindPageList()
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgLine.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgLine.CurrentPageIndex
    End Sub

    Protected Function LoadData() As DataSet
        Dim strOpCd_GET As String = "WM_CLSSETUP_FFB_PRICE_LIST"
        Dim strSearch As String
        Dim sortitem As String
        Dim intErrNo As Integer
        Dim intCnt As Integer

        ParamNama = "STRSEARCH"
        ParamValue = "Where Year(h.InDate)='" & srcpyear.SelectedItem.Value & "' AND Month(h.InDate)='" & srcpmonth.SelectedItem.Value & "' And h.LocCode='" & strLocation & "' And ((h.SubLocCode='" & srchlocation.SelectedItem.Value & "') OR ('" & srchlocation.SelectedItem.Value & "'='')) "

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_GET, ParamNama, ParamValue, objPriceDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_BANK_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        Return objPriceDs
    End Function

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgLine.CurrentPageIndex = 0
            Case "prev"
                dgLine.CurrentPageIndex = _
                    Math.Max(0, dgLine.CurrentPageIndex - 1)
            Case "next"
                dgLine.CurrentPageIndex = _
                    Math.Min(dgLine.PageCount - 1, dgLine.CurrentPageIndex + 1)
            Case "last"
                dgLine.CurrentPageIndex = dgLine.PageCount - 1
        End Select
        lstDropList.SelectedIndex = dgLine.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgLine.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgLine.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        dgLine.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)

        Dim int As Integer = e.Item.ItemIndex
        Dim SalCell As TableCell = e.Item.Cells(0)
        Dim strSalCd As String
        Dim strDate As Date

        Dim EditText As Label


        strSalCd = SalCell.Text
        EditText = dgLine.Items.Item(int).FindControl("Status")
        strDate = IIf(EditText.Text = "Active", "2", "1")
         
        Response.Redirect("WM_Setup_FFBPriceDet.aspx?POType=" & strDate)
    End Sub

    Sub NewSalBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("WM_Setup_FFBPriceDet.aspx")
    End Sub

    Sub DEDR_Gen(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
       
    End Sub


End Class
