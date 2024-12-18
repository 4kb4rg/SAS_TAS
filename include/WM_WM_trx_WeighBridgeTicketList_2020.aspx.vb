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


Public Class WM_WeighBridgeTicketList : Inherits Page


    Protected WithEvents dgDisbunDet As DataGrid
    Protected WithEvents dgTicketList As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblErrDateIn As Label
    Protected WithEvents lblErrDateInMsg As Label

    Protected WithEvents srchTicketNo As TextBox
    Protected WithEvents srchDeliveryNo As TextBox
    Protected WithEvents srchDateIn As TextBox
    Protected WithEvents srchCustomer As TextBox
    Protected WithEvents srchProductList As DropDownList
    Protected WithEvents srchStatusList As DropDownList
    Protected WithEvents srchUpdateBy As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lstDropList As DropDownList

    Protected WithEvents ddlMonth As DropDownList
    Protected WithEvents ddlyear As DropDownList

    Protected WithEvents ddlTBSPemilik As DropDownList
    Protected WithEvents ddlTBSAgen As DropDownList
    Protected WithEvents ddlPPN As DropDownList
    Protected WithEvents ddlOB As DropDownList
    Protected WithEvents ddlOL As DropDownList
    Protected WithEvents ddlOBBiaya As DropDownList
    Protected WithEvents ddlOLBiaya As DropDownList

    Protected WithEvents dgPPHList As DataGrid
    Protected WithEvents cbExcelPPH As CheckBox
    Protected WithEvents PPHPrintPrev As ImageButton
    Protected WithEvents cbExcelTicket As CheckBox
    Protected WithEvents btnRefresh As ImageButton
    Protected WithEvents btnGenerate As ImageButton
    Protected WithEvents TicketPrintPrev As ImageButton
    Protected WithEvents lblErrRefresh As Label
    Protected WithEvents lblErrGenerate As Label

    Protected WithEvents btnPrev As ImageButton
    Protected WithEvents btnNext As ImageButton
    Protected WithEvents SearchBtn As Button


    Protected WithEvents dgFFFBOB As DataGrid
    Protected WithEvents dgFFBPriceList As DataGrid
    Protected WithEvents dgFFBPriceListDet As DataGrid

    Protected objWMTrx As New agri.WM.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objOk As New agri.GL.ClsTrx()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intWMAR As Integer
    Dim strDateFormat As String
    Dim strParamName As String
    Dim strParamValue As String

    Dim objPPHDs As New DataSet()
    Dim objTicketDs As New DataSet()
    Dim objDisbunDs As New DataSet()

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PMACCMONTH")
        strAccYear = Session("SS_PMACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intWMAR = Session("SS_WMAR")
        strDateFormat = Session("SS_DATEFMT")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWMAccessRights.WMWeighBridgeTicket), intWMAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrDateInMsg.Visible = false
            lblErrDateIn.Visible = False
            lblErrMessage.Visible = False
            lblErrRefresh.Visible = False
            lblErrGenerate.Visible = False

            If SortExpression.Text = "" Then
                SortExpression.Text = "TicketNo"
            End If

            btnRefresh.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnRefresh).ToString())
            btnRefresh.Attributes("onclick") = "javascript:return confirm('Get WB ticket ?');"
            btnGenerate.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnGenerate).ToString())
            btnGenerate.Attributes("onclick") = "javascript:return confirm('Generate journal ?');"

            If Not Page.IsPostBack Then
                SearchBtn.Attributes("onclick") = "javascript:return ConfirmAction('Generated');"
                ddlMonth.SelectedIndex = CInt(Session("SS_SELACCMONTH"))
                BindAccYear(Session("SS_SELACCYEAR"))

                BindSearchStatusList()
                BindSearchProductList()
                BindAccount("", "", "", "", "", "", "")
                LoadCOASetting()

                lstDropList.Visible = False
                btnPrev.Visible = False
                btnNext.Visible = False

            End If
        End If
    End Sub

    Sub BindSearchStatusList()

        srchStatusList.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketStatus(objWMTrx.EnumWeighBridgeTicketStatus.All), objWMTrx.EnumWeighBridgeTicketStatus.All))
        srchStatusList.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketStatus(objWMTrx.EnumWeighBridgeTicketStatus.Active), objWMTrx.EnumWeighBridgeTicketStatus.Active))
        srchStatusList.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketStatus(objWMTrx.EnumWeighBridgeTicketStatus.Deleted), objWMTrx.EnumWeighBridgeTicketStatus.Deleted))

        srchStatusList.SelectedIndex = 1
    End Sub

    Sub BindSearchProductList()

        srchProductList.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketProduct(objWMTrx.EnumWeighBridgeTicketProduct.All), objWMTrx.EnumWeighBridgeTicketProduct.All))
        srchProductList.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketProduct(objWMTrx.EnumWeighBridgeTicketProduct.CPO), objWMTrx.EnumWeighBridgeTicketProduct.CPO))
        srchProductList.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketProduct(objWMTrx.EnumWeighBridgeTicketProduct.FFB), objWMTrx.EnumWeighBridgeTicketProduct.FFB))
        srchProductList.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketProduct(objWMTrx.EnumWeighBridgeTicketProduct.PK), objWMTrx.EnumWeighBridgeTicketProduct.PK))
        srchProductList.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketProduct(objWMTrx.EnumWeighBridgeTicketProduct.Others), objWMTrx.EnumWeighBridgeTicketProduct.Others))

    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgTicketList.CurrentPageIndex = 0
        dgTicketList.EditItemIndex = -1
        BindGrid()
        BindGrid_Disbun()
        dgPPHList.CurrentPageIndex = 0
        dgPPHList.EditItemIndex = -1
        BindGridPPH()

        dgFFBPriceList.CurrentPageIndex = 0
        dgFFBPriceList.EditItemIndex = -1
        BindGridFFBPrice()
        BindGridFFBPricedDetail()
        BindGridFFBPriceBongkar()
        LoadDataEmpty()

        UserMsgBox(Me, "Generated Sucsess !!!")
        Exit Sub
    End Sub

    Private Function lCDbl(ByVal pcVal As String) As Double
        If IsNumeric(pcVal) Then lCDbl = CDbl(pcVal) Else lCDbl = 0
    End Function

    Sub BindGrid()
        Dim lbButton As LinkButton
        Dim intCnt As Integer
        Dim Status As Label
        Dim strStatus As String

        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet
        Dim lbl As Label

        dsData = LoadData()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgTicketList.PageSize)

        dgTicketList.DataSource = dsData
        If dgTicketList.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgTicketList.CurrentPageIndex = 0
            Else
                dgTicketList.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgTicketList.DataBind()
        'BindPageList()

        'PageNo = dgTicketList.CurrentPageIndex + 1
        'lblTracker.Text = "Page " & PageNo & " of " & dgTicketList.PageCount

        'CType(dgTicketList.Items(intCnt).FindControl("lblNoKontrakWB"), Label).BackColor =
        Dim lblNoKontrak As Label
        Dim lblNoKontrakWB As Label
        Dim lblPotDiakui As Label
        Dim lblPotWB As Label

        For intCnt = 0 To dgTicketList.Items.Count - 1
            lbl = dgTicketList.Items.Item(intCnt).FindControl("lblKodeSpl")

            If Trim(lbl.Text) <> "ZZZZZZ" Then
                lblNoKontrak = dgTicketList.Items.Item(intCnt).FindControl("lblNoKontrakWB")
                lblNoKontrakWB = dgTicketList.Items.Item(intCnt).FindControl("lblNoKontrak")

                lblPotDiakui = dgTicketList.Items.Item(intCnt).FindControl("lblPotDiakui")
                lblPotWB = dgTicketList.Items.Item(intCnt).FindControl("lblPotWB")

                If lblNoKontrak.Text.Trim <> lblNoKontrakWB.Text.Trim Then
                    dgTicketList.Items.Item(intCnt).ForeColor = Drawing.Color.Red
                End If

                If lCDbl(lblPotDiakui.Text) <> lCDbl(lblPotWB.Text) Then
                    dgTicketList.Items.Item(intCnt).BackColor = Drawing.Color.Yellow
                    dgTicketList.Items.Item(intCnt).ForeColor = Drawing.Color.Blue
                End If

            End If

            If Trim(lbl.Text) = "ZZZZZZ" Then
                lbl = dgTicketList.Items.Item(intCnt).FindControl("lblNoUrut")
                lbl.Visible = False
                lbl = dgTicketList.Items.Item(intCnt).FindControl("lblProd")
                lbl.Visible = False
                lbl = dgTicketList.Items.Item(intCnt).FindControl("lblTglMsuk")
                lbl.Visible = False
                lbl = dgTicketList.Items.Item(intCnt).FindControl("lblJamMasuk")
                lbl.Visible = False
                lbl = dgTicketList.Items.Item(intCnt).FindControl("lblJamKeluar")
                lbl.Visible = False
                lbl = dgTicketList.Items.Item(intCnt).FindControl("lblNamaSpl")
                lbl.Text = Replace(lbl.Text, "ZZZ", "")
                lbl.Font.Bold = True
                lbl = dgTicketList.Items.Item(intCnt).FindControl("lblPotBM")
                lbl.Visible = False
                lbl = dgTicketList.Items.Item(intCnt).FindControl("lblPotBB")
                lbl.Visible = False
                lbl = dgTicketList.Items.Item(intCnt).FindControl("lblPotTP")
                lbl.Visible = False
                lbl = dgTicketList.Items.Item(intCnt).FindControl("lblPotJK")
                lbl.Visible = False
                lbl = dgTicketList.Items.Item(intCnt).FindControl("lblPotSampah")
                lbl.Visible = False
                lbl = dgTicketList.Items.Item(intCnt).FindControl("lblPotAir")
                lbl.Visible = False
                lbl = dgTicketList.Items.Item(intCnt).FindControl("lblPotCong")
                lbl.Visible = False
                lbl = dgTicketList.Items.Item(intCnt).FindControl("lblPotTotal")
                'lbl.Visible = False
                'lbl = dgTicketList.Items.Item(intCnt).FindControl("lblHrgBB")
                'lbl.Visible = False
                'lbl = dgTicketList.Items.Item(intCnt).FindControl("lblHrgBK")
                lbl.Visible = False
                lbl = dgTicketList.Items.Item(intCnt).FindControl("lblRate")
                lbl.Visible = False
                lbl = dgTicketList.Items.Item(intCnt).FindControl("lblKGOB")
                lbl.Visible = False
                lbl = dgTicketList.Items.Item(intCnt).FindControl("lblKGOL")
                lbl.Visible = False
            End If
        Next
    End Sub

    Sub BindGrid_Disbun()
        Dim lbButton As LinkButton
        Dim intCnt As Integer
        Dim Status As Label
        Dim strStatus As String

        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsDisbun As DataSet
        Dim lbl As Label

        dsDisbun = LoadData_Disbun()
        'PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgDisbunDet.PageSize)

        dgDisbunDet.DataSource = dsDisbun
        If dgDisbunDet.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgDisbunDet.CurrentPageIndex = 0
            Else
                dgDisbunDet.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgDisbunDet.DataBind()
        'BindPageList()

        'PageNo = dgTicketList.CurrentPageIndex + 1
        'lblTracker.Text = "Page " & PageNo & " of " & dgTicketList.PageCount

        'CType(dgTicketList.Items(intCnt).FindControl("lblNoKontrakWB"), Label).BackColor =
        'Dim lblNoKontrak As Label
        'Dim lblNoKontrakWB As Label
        'Dim lblPotDiakui As Label
        'Dim lblPotWB As Label

        'For intCnt = 0 To dgTicketList.Items.Count - 1
        '    lbl = dgTicketList.Items.Item(intCnt).FindControl("lblKodeSpl")

        '    If Trim(lbl.Text) <> "ZZZZZZ" Then
        '        lblNoKontrak = dgTicketList.Items.Item(intCnt).FindControl("lblNoKontrakWB")
        '        lblNoKontrakWB = dgTicketList.Items.Item(intCnt).FindControl("lblNoKontrak")

        '        lblPotDiakui = dgTicketList.Items.Item(intCnt).FindControl("lblPotDiakui")
        '        lblPotWB = dgTicketList.Items.Item(intCnt).FindControl("lblPotWB")

        '        If lblNoKontrak.Text.Trim <> lblNoKontrakWB.Text.Trim Then
        '            dgTicketList.Items.Item(intCnt).ForeColor = Drawing.Color.Red
        '        End If

        '        If lCDbl(lblPotDiakui.Text) <> lCDbl(lblPotWB.Text) Then
        '            dgTicketList.Items.Item(intCnt).BackColor = Drawing.Color.Yellow
        '            dgTicketList.Items.Item(intCnt).ForeColor = Drawing.Color.Blue
        '        End If

        '    End If

        '    If Trim(lbl.Text) = "ZZZZZZ" Then
        '        lbl = dgTicketList.Items.Item(intCnt).FindControl("lblNoUrut")
        '        lbl.Visible = False
        '        lbl = dgTicketList.Items.Item(intCnt).FindControl("lblProd")
        '        lbl.Visible = False
        '        lbl = dgTicketList.Items.Item(intCnt).FindControl("lblTglMsuk")
        '        lbl.Visible = False
        '        lbl = dgTicketList.Items.Item(intCnt).FindControl("lblJamMasuk")
        '        lbl.Visible = False
        '        lbl = dgTicketList.Items.Item(intCnt).FindControl("lblJamKeluar")
        '        lbl.Visible = False
        '        lbl = dgTicketList.Items.Item(intCnt).FindControl("lblNamaSpl")
        '        lbl.Text = Replace(lbl.Text, "ZZZ", "")
        '        lbl.Font.Bold = True
        '        lbl = dgTicketList.Items.Item(intCnt).FindControl("lblPotBM")
        '        lbl.Visible = False
        '        lbl = dgTicketList.Items.Item(intCnt).FindControl("lblPotBB")
        '        lbl.Visible = False
        '        lbl = dgTicketList.Items.Item(intCnt).FindControl("lblPotTP")
        '        lbl.Visible = False
        '        lbl = dgTicketList.Items.Item(intCnt).FindControl("lblPotJK")
        '        lbl.Visible = False
        '        lbl = dgTicketList.Items.Item(intCnt).FindControl("lblPotSampah")
        '        lbl.Visible = False
        '        lbl = dgTicketList.Items.Item(intCnt).FindControl("lblPotAir")
        '        lbl.Visible = False
        '        lbl = dgTicketList.Items.Item(intCnt).FindControl("lblPotCong")
        '        lbl.Visible = False
        '        lbl = dgTicketList.Items.Item(intCnt).FindControl("lblPotTotal")
        '        'lbl.Visible = False
        '        'lbl = dgTicketList.Items.Item(intCnt).FindControl("lblHrgBB")
        '        'lbl.Visible = False
        '        'lbl = dgTicketList.Items.Item(intCnt).FindControl("lblHrgBK")
        '        lbl.Visible = False
        '        lbl = dgTicketList.Items.Item(intCnt).FindControl("lblRate")
        '        lbl.Visible = False
        '        lbl = dgTicketList.Items.Item(intCnt).FindControl("lblKGOB")
        '        lbl.Visible = False
        '        lbl = dgTicketList.Items.Item(intCnt).FindControl("lblKGOL")
        '        lbl.Visible = False
        '    End If
        'Next
    End Sub


    Sub BindGridPPH()
        Dim lbButton As LinkButton
        Dim intCnt As Integer
        Dim Status As Label
        Dim strStatus As String

        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet
        Dim lbl As Label

        dsData = LoadDataPPH()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgPPHList.PageSize)

        dgPPHList.DataSource = dsData
        If dgPPHList.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgPPHList.CurrentPageIndex = 0
            Else
                dgPPHList.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgPPHList.DataBind()

        For intCnt = 0 To dgPPHList.Items.Count - 1
            lbl = dgPPHList.Items.Item(intCnt).FindControl("lblKodeSpl")
            If Trim(lbl.Text) = "ZZZZZZ" Then
                lbl = dgPPHList.Items.Item(intCnt).FindControl("lblNoUrut")
                lbl.Visible = False
                lbl = dgPPHList.Items.Item(intCnt).FindControl("lblTglMasuk")
                lbl.Visible = False
                lbl = dgPPHList.Items.Item(intCnt).FindControl("lblNamaSpl")
                lbl.Text = Replace(lbl.Text, "ZZZ", "")
                lbl.Font.Bold = True
                lbl = dgPPHList.Items.Item(intCnt).FindControl("lblNPWP")
                lbl.Visible = False
                lbl = dgPPHList.Items.Item(intCnt).FindControl("lblDPPAmountPPH")
                lbl.Font.Bold = True
                lbl = dgPPHList.Items.Item(intCnt).FindControl("lblPPHAmountPPH")
                lbl.Font.Bold = True
                lbl = dgPPHList.Items.Item(intCnt).FindControl("lblRatePPH")
                lbl.Visible = False
                lbl = dgPPHList.Items.Item(intCnt).FindControl("lblOBKGPPH")
                lbl.Visible = False
                lbl = dgPPHList.Items.Item(intCnt).FindControl("lblOBTripPPH")
                lbl.Visible = False
                lbl = dgPPHList.Items.Item(intCnt).FindControl("lblOBTotalPPH")
                lbl.Font.Bold = True
                lbl = dgPPHList.Items.Item(intCnt).FindControl("lblOLKGPPH")
                lbl.Visible = False
                lbl = dgPPHList.Items.Item(intCnt).FindControl("lblOLTripPPH")
                lbl.Visible = False
                lbl = dgPPHList.Items.Item(intCnt).FindControl("lblOLTotalPPH")
                lbl.Font.Bold = True
            End If
        Next
    End Sub

    Sub BindGridFFBPrice()
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadDataFFBPrice()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgTicketList.PageSize)

        dgFFBPriceList.DataSource = dsData
        If dgFFBPriceList.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgFFBPriceList.CurrentPageIndex = 0
            Else
                dgFFBPriceList.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgFFBPriceList.DataBind()
        'BindPageList()

        'PageNo = dgTicketList.CurrentPageIndex + 1
        'lblTracker.Text = "Page " & PageNo & " of " & dgTicketList.PageCount
    End Sub

    Sub BindGridFFBPricedDetail()
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadDataFFBPriceDetail()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgTicketList.PageSize)

        dgFFBPriceListDet.DataSource = dsData
        If dgFFBPriceList.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgFFBPriceListDet.CurrentPageIndex = 0
            Else
                dgFFBPriceListDet.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgFFBPriceListDet.DataBind()
        'BindPageList()

        'PageNo = dgTicketList.CurrentPageIndex + 1
        'lblTracker.Text = "Page " & PageNo & " of " & dgTicketList.PageCount
    End Sub

    Sub BindGridFFBPriceBongkar()
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadDataFFBPrice_Bongkar()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgFFFBOB.PageSize)

        dgFFFBOB.DataSource = dsData
        If dgFFFBOB.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgFFFBOB.CurrentPageIndex = 0
            Else
                dgFFFBOB.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgFFFBOB.DataBind()

    End Sub

    Protected Function LoadData() As DataSet
        Dim strOppCode_Get As String = "WM_WM_CLSTRX_TICKET_GET"
        Dim strSrchTicketNo As String
        Dim strSrchDeliveryNo As String
        Dim strSrchDateIn As String
        Dim strSrchCustomer As String
        Dim strSrchProduct As String
        Dim strSrchStatus As String
        Dim strSearch As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objFormatDate As String
        Dim objActualDate As String

        If Not srchDateIn.Text = "" Then
            If objGlobal.mtdValidInputDate(strDateFormat, Trim(srchDateIn.Text), objFormatDate, objActualDate) = False Then
                lblErrDateIn.Visible = True
                lblErrDateIn.Text = lblErrDateInMsg.Text & objFormatDate
                Exit Function
            Else
                strSrchDateIn = objActualDate
            End If
        End If

        strSrchTicketNo = IIf(srchTicketNo.Text = "", "", " AND KodeSlipTimbangan like '%" & srchTicketNo.Text & "%' ")
        strSrchDeliveryNo = IIf(srchDeliveryNo.Text = "", "", " AND ContractNo like '%" & srchDeliveryNo.Text & "%' ")
        strSrchDateIn = IIf(srchDateIn.Text = "", "", " AND TglMasuk='" & Date_Validation(srchDateIn.Text, False) & "' ")
        strSrchCustomer = IIf(srchCustomer.Text = "", "", " AND (KodeSupplier LIKE '%" & Trim(srchCustomer.Text) & "%' OR NamaSupplier LIKE '%" & Trim(srchCustomer.Text) & "%') ")
        strSrchProduct = IIf(srchProductList.SelectedItem.Value = objWMTrx.EnumWeighBridgeTicketProduct.All, "", " AND A.ProductCode='" & srchProductList.SelectedItem.Value & "' ")
        strSrchStatus = IIf(srchStatusList.SelectedItem.Value = objWMTrx.EnumWeighBridgeTicketStatus.All, "", " AND A.Status='" & srchStatusList.SelectedItem.Value & "' ")
        strSearch = strSrchTicketNo & strSrchDeliveryNo & strSrchDateIn & strSrchCustomer & strSrchProduct & strSrchStatus

        strParamName = "ACCMONTH|ACCYEAR|STRSERCH"
        strParamValue = Trim(ddlMonth.SelectedItem.Value) & "|" & Trim(ddlyear.SelectedItem.Value) & "|" & _
                        Trim(strSearch)

        Try
            intErrNo = objOk.mtdGetDataCommon(strOppCode_Get, strParamName, strParamValue, objTicketDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=wm_ticket&errmesg=" & Exp.Message & "&redirect=")
        End Try

        dgTicketList.DataSource = objTicketDs
        dgTicketList.DataBind()

        If objTicketDs.Tables(0).Rows.Count > 0 Then
            cbExcelTicket.Visible = True
            TicketPrintPrev.Visible = True

            lstDropList.Visible = False
            btnPrev.Visible = False
            btnNext.Visible = False
        Else
            cbExcelTicket.Visible = False
            TicketPrintPrev.Visible = False

            lstDropList.Visible = False
            btnPrev.Visible = False
            btnNext.Visible = False
        End If

        Return objTicketDs

    End Function

    Protected Function LoadData_Disbun() As DataSet
        Dim strOppCode_Get As String = "WM_WM_CLSTRX_TICKET_DISBUN_GET"
        Dim strSrchTicketNo As String
        Dim strSrchDeliveryNo As String
        Dim strSrchDateIn As String
        Dim strSrchCustomer As String
        Dim strSrchProduct As String
        Dim strSrchStatus As String
        Dim strSearch As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objFormatDate As String
        Dim objActualDate As String

        If Not srchDateIn.Text = "" Then
            If objGlobal.mtdValidInputDate(strDateFormat, Trim(srchDateIn.Text), objFormatDate, objActualDate) = False Then
                lblErrDateIn.Visible = True
                lblErrDateIn.Text = lblErrDateInMsg.Text & objFormatDate
                Exit Function
            Else
                strSrchDateIn = objActualDate
            End If
        End If

        strSrchTicketNo = IIf(srchTicketNo.Text = "", "", " AND H.KodeSlipTimbangan like '%" & srchTicketNo.Text & "%' ")
        strSrchDeliveryNo = IIf(srchDeliveryNo.Text = "", "", " AND t.ContractNo like '%" & srchDeliveryNo.Text & "%' ")
        strSrchDateIn = IIf(srchDateIn.Text = "", "", " AND h.TglMasuk='" & Date_Validation(srchDateIn.Text, False) & "' ")
        strSrchCustomer = IIf(srchCustomer.Text = "", "", " AND (h.KodeSupplier LIKE '%" & Trim(srchCustomer.Text) & "%' OR pu.Name LIKE '%" & Trim(srchCustomer.Text) & "%') ")
        strSrchProduct = IIf(srchProductList.SelectedItem.Value = objWMTrx.EnumWeighBridgeTicketProduct.All, "", " AND A.ProductCode='" & srchProductList.SelectedItem.Value & "' ")
        strSrchStatus = IIf(srchStatusList.SelectedItem.Value = objWMTrx.EnumWeighBridgeTicketStatus.All, "", " AND h.Status='" & srchStatusList.SelectedItem.Value & "' ")
        strSearch = strSrchTicketNo & strSrchDeliveryNo & strSrchDateIn & strSrchCustomer & strSrchProduct & strSrchStatus

        strParamName = "ACCMONTH|ACCYEAR|STRSERCH"
        strParamValue = Trim(ddlMonth.SelectedItem.Value) & "|" & Trim(ddlyear.SelectedItem.Value) & "|" & _
                        Trim(strSearch)

        Try
            intErrNo = objOk.mtdGetDataCommon(strOppCode_Get, strParamName, strParamValue, objDisbunDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=wm_ticket&errmesg=" & Exp.Message & "&redirect=")
        End Try

        dgDisbunDet.DataSource = objDisbunDs
        dgDisbunDet.DataBind()

        'If objTicketDs.Tables(0).Rows.Count > 0 Then
        '    cbExcelTicket.Visible = True
        '    TicketPrintPrev.Visible = True

        '    lstDropList.Visible = False
        '    btnPrev.Visible = False
        '    btnNext.Visible = False
        'Else
        '    cbExcelTicket.Visible = False
        '    TicketPrintPrev.Visible = False

        '    lstDropList.Visible = False
        '    btnPrev.Visible = False
        '    btnNext.Visible = False
        'End If

        Return objDisbunDs

    End Function
    Protected Function LoadDataPPH() As DataSet
        Dim strOppCode_Get As String = "WM_WM_CLSTRX_TICKET_GET"
        Dim strSrchTicketNo As String
        Dim strSrchDeliveryNo As String
        Dim strSrchDateIn As String
        Dim strSrchCustomer As String
        Dim strSrchProduct As String
        Dim strSrchStatus As String
        Dim strSearch As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objFormatDate As String
        Dim objActualDate As String
        Dim lbl As Label

        If Not srchDateIn.Text = "" Then
            If objGlobal.mtdValidInputDate(strDateFormat, Trim(srchDateIn.Text), objFormatDate, objActualDate) = False Then
                lblErrDateIn.Visible = True
                lblErrDateIn.Text = lblErrDateInMsg.Text & objFormatDate
                Exit Function
            Else
                strSrchDateIn = objActualDate
            End If
        End If

        strSrchTicketNo = IIf(srchTicketNo.Text = "", "", " AND KodeSlipTimbangan like '%" & srchTicketNo.Text & "%' ")
        strSrchDeliveryNo = IIf(srchDeliveryNo.Text = "", "", " AND NoDO='" & srchDeliveryNo.Text & "' ")
        strSrchDateIn = IIf(srchDateIn.Text = "", "", " AND TglMasuk='" & Date_Validation(srchDateIn.Text, False) & "' ")
        strSrchCustomer = IIf(srchCustomer.Text = "", "", " AND (KodeSupplier LIKE '%" & Trim(srchCustomer.Text) & "%' OR NamaSupplier LIKE '%" & Trim(srchCustomer.Text) & "%') ")
        strSrchProduct = IIf(srchProductList.SelectedItem.Value = objWMTrx.EnumWeighBridgeTicketProduct.All, "", " AND A.ProductCode='" & srchProductList.SelectedItem.Value & "' ")
        strSrchStatus = IIf(srchStatusList.SelectedItem.Value = objWMTrx.EnumWeighBridgeTicketStatus.All, "", " AND A.Status='" & srchStatusList.SelectedItem.Value & "' ")
        strSearch = strSrchTicketNo & strSrchDeliveryNo & strSrchDateIn & strSrchCustomer & strSrchProduct & strSrchStatus

        strOppCode_Get = "WM_WM_CLSTRX_TICKET_GET_SUMMARY"
        strParamName = "ACCMONTH|ACCYEAR|STRSERCH"
        strParamValue = Trim(ddlMonth.SelectedItem.Value) & "|" & Trim(ddlyear.SelectedItem.Value) & "|" & _
                        " WHERE A.AccYear='" & Trim(ddlyear.SelectedItem.Value) & "' AND A.AccMonth='" & Trim(ddlMonth.SelectedItem.Value) & "' " & _
                        Trim(strSearch)

        Try
            intErrNo = objOk.mtdGetDataCommon(strOppCode_Get, strParamName, strParamValue, objPPHDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=wm_ticket&errmesg=" & Exp.Message & "&redirect=")
        End Try

        dgPPHList.DataSource = objPPHDs
        dgPPHList.DataBind()

        If objPPHDs.Tables(0).Rows.Count > 0 Then
            btnGenerate.Visible = True
            cbExcelPPH.Visible = True
            PPHPrintPrev.Visible = True
        Else
            btnGenerate.Visible = False
            cbExcelPPH.Visible = False
            PPHPrintPrev.Visible = False
        End If


        Return objPPHDs
    End Function

    Protected Function LoadDataFFBPrice() As DataSet
        Dim strOppCode_Get As String = "WM_WM_CLSTRX_TICKET_GET"
        Dim intErrNo As Integer

        strOppCode_Get = "WM_WM_CLSTRX_FFB_PRICE_GET"
        strParamName = "ACCMONTH|ACCYEAR"
        strParamValue = Trim(ddlMonth.SelectedItem.Value) & "|" & Trim(ddlyear.SelectedItem.Value)

        Try
            intErrNo = objOk.mtdGetDataCommon(strOppCode_Get, strParamName, strParamValue, objPPHDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=wm_ticket&errmesg=" & Exp.Message & "&redirect=")
        End Try

        dgFFBPriceList.DataSource = objPPHDs
        dgFFBPriceList.DataBind()

        Return objPPHDs
    End Function

    Protected Function LoadDataFFBPrice_Bongkar() As DataSet
        Dim strOppCode_Get As String = "WM_WM_CLSTRX_FFB_ONGKOS_BONGKAR_GET"
        Dim intErrNo As Integer

        strOppCode_Get = "WM_WM_CLSTRX_FFB_ONGKOS_BONGKAR_GET"
        strParamName = "ACCMONTH|ACCYEAR"
        strParamValue = Trim(ddlMonth.SelectedItem.Value) & "|" & Trim(ddlyear.SelectedItem.Value)

        Try
            intErrNo = objOk.mtdGetDataCommon(strOppCode_Get, strParamName, strParamValue, objPPHDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=wm_ticket&errmesg=" & Exp.Message & "&redirect=")
        End Try

        dgFFFBOB.DataSource = objPPHDs
        dgFFFBOB.DataBind()

        Return objPPHDs
    End Function

    Protected Function LoadDataFFBPriceDetail() As DataSet
        Dim strOppCode_Get As String = "WM_WM_CLSTRX_FFB_PRICE_DETAIL_GET"
        Dim intErrNo As Integer

        strOppCode_Get = "WM_WM_CLSTRX_FFB_PRICE_DETAIL_GET"
        strParamName = "ACCMONTH|ACCYEAR"
        strParamValue = Trim(ddlMonth.SelectedItem.Value) & "|" & Trim(ddlyear.SelectedItem.Value)

        Try
            intErrNo = objOk.mtdGetDataCommon(strOppCode_Get, strParamName, strParamValue, objPPHDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=wm_ticket&errmesg=" & Exp.Message & "&redirect=")
        End Try

        dgFFBPriceListDet.DataSource = objPPHDs
        dgFFBPriceListDet.DataBind()

        Return objPPHDs
    End Function

    Protected Function LoadDataEmpty() As DataSet
        Dim strOppCode_Get As String = "WM_WM_CLSTRX_TICKET_GET_EMPTY"
        Dim intErrNo As Integer

        strParamName = "ACCMONTH|ACCYEAR"
        strParamValue = Trim(ddlMonth.SelectedItem.Value) & "|" & Trim(ddlyear.SelectedItem.Value)

        Try
            intErrNo = objOk.mtdGetDataCommon(strOppCode_Get, strParamName, strParamValue, objPPHDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=wm_ticket&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objPPHDs.Tables(0).Rows.Count > 0 Then
            lblErrRefresh.Visible = True
            lblErrRefresh.Text = objPPHDs.Tables(0).Rows(0).Item("Msg")
        End If

        Return objPPHDs
    End Function


    Sub BindPageList()

        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgTicketList.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgTicketList.CurrentPageIndex

    End Sub

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgTicketList.CurrentPageIndex = 0
            Case "prev"
                dgTicketList.CurrentPageIndex = _
                Math.Max(0, dgTicketList.CurrentPageIndex - 1)
            Case "next"
                dgTicketList.CurrentPageIndex = _
                Math.Min(dgTicketList.PageCount - 1, dgTicketList.CurrentPageIndex + 1)
            Case "last"
                dgTicketList.CurrentPageIndex = dgTicketList.PageCount - 1
        End Select

        lstDropList.SelectedIndex = dgTicketList.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgTicketList.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgTicketList.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        dgTicketList.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOppCd_WeighBridge_Ticket_GET As String = "WM_CLSTRX_WEIGHBRIDGE_TICKET_GET"
        Dim strOppCd_WeighBridge_Ticket_ADD As String = "WM_CLSTRX_WEIGHBRIDGE_TICKET_ADD"
        Dim strOppCd_WeighBridge_Ticket_UPD As String = "WM_CLSTRX_WEIGHBRIDGE_TICKET_UPD"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim strTicketNo As String
        Dim lblTicketNo As Label

        dgTicketList.EditItemIndex = CInt(E.Item.ItemIndex)
        lblTicketNo = dgTicketList.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblTicketNo")
        strTicketNo = lblTicketNo.Text

        strParam = strTicketNo & "|||||||||||||||||||||||" & objWMTrx.EnumWeighBridgeTicketStatus.Deleted
        Try
            strParam = strTicketNo & "|" & objWMTrx.EnumWeighBridgeTicketStatus.Deleted
            intErrNo = objWMTrx.mtdDelWeighBridgeTicket(strOppCd_WeighBridge_Ticket_UPD, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strParam)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=WM_CLSTRX_WEIGHBRIDGE_TICKET_LIST_DELETE&errmesg=" & lblErrMessage.Text & "&redirect=WM/trx/WM_trx_WeighBridgeTicketDet.aspx")
        End Try

        dgTicketList.EditItemIndex = -1
        BindGrid()
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

        ddlyear.DataSource = objAccYearDs.Tables(0)
        ddlyear.DataValueField = "AccYear"
        ddlyear.DataTextField = "UserName"
        ddlyear.DataBind()
        ddlyear.SelectedIndex = intSelectedIndex - 1
    End Sub

    Sub BindAccount(ByVal pv_strCOATBSPemilik As String, ByVal pv_strCOATBSAgen As String, ByVal pv_strCOAPPN As String, ByVal pv_strCOAOB As String, ByVal pv_strCOAOL As String, ByVal pv_strCOAOBBiaya As String, ByVal pv_strCOAOLBiaya As String)
        Dim strOpCd As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        Dim strParam As String = "Order By ACC.AccCode|And ACC.Status = '" & objGLSetup.EnumAccountCodeStatus.Active & "'"
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim intSelectedIndexCOATBSPemilik As Integer = 0
        Dim intSelectedIndexCOATBSAgen As Integer = 0
        Dim intSelectedIndexCOAPPN As Integer = 0
        Dim intSelectedIndexCOAOB As Integer = 0
        Dim intSelectedIndexCOAOL As Integer = 0
        Dim intSelectedIndexCOAOBBiaya As Integer = 0
        Dim intSelectedIndexCOAOLBiaya As Integer = 0
        Dim objAccDs As New Object

        strParam = strParam & IIf(Session("SS_COACENTRALIZED") = "1", "", " AND LocCode = '" & Trim(strLocation) & "' ")

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.AccountCode, _
                                                   objAccDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GR_ACCCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objAccDs.Tables(0).Rows.Count - 1
            If objAccDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(pv_strCOATBSPemilik) Then
                intSelectedIndexCOATBSPemilik = intCnt + 1
                Exit For
            End If
        Next

        For intCnt = 0 To objAccDs.Tables(0).Rows.Count - 1
            If objAccDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(pv_strCOATBSAgen) Then
                intSelectedIndexCOATBSAgen = intCnt + 1
                Exit For
            End If
        Next

        For intCnt = 0 To objAccDs.Tables(0).Rows.Count - 1
            If objAccDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(pv_strCOAPPN) Then
                intSelectedIndexCOAPPN = intCnt + 1
                Exit For
            End If
        Next

        For intCnt = 0 To objAccDs.Tables(0).Rows.Count - 1
            If objAccDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(pv_strCOAOB) Then
                intSelectedIndexCOAOB = intCnt + 1
                Exit For
            End If
        Next

        For intCnt = 0 To objAccDs.Tables(0).Rows.Count - 1
            If objAccDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(pv_strCOAOL) Then
                intSelectedIndexCOAOL = intCnt + 1
                Exit For
            End If
        Next

        For intCnt = 0 To objAccDs.Tables(0).Rows.Count - 1
            If objAccDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(pv_strCOAOBBiaya) Then
                intSelectedIndexCOAOBBiaya = intCnt + 1
                Exit For
            End If
        Next

        For intCnt = 0 To objAccDs.Tables(0).Rows.Count - 1
            If objAccDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(pv_strCOAOLBiaya) Then
                intSelectedIndexCOAOLBiaya = intCnt + 1
                Exit For
            End If
        Next

        dr = objAccDs.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("_Description") = "Please select account code"
        objAccDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlTBSPemilik.DataSource = objAccDs.Tables(0)
        ddlTBSPemilik.DataValueField = "AccCode"
        ddlTBSPemilik.DataTextField = "_Description"
        ddlTBSPemilik.DataBind()
        ddlTBSPemilik.SelectedIndex = intSelectedIndexCOATBSPemilik

        ddlTBSAgen.DataSource = objAccDs.Tables(0)
        ddlTBSAgen.DataValueField = "AccCode"
        ddlTBSAgen.DataTextField = "_Description"
        ddlTBSAgen.DataBind()
        ddlTBSAgen.SelectedIndex = intSelectedIndexCOATBSAgen

        ddlPPN.DataSource = objAccDs.Tables(0)
        ddlPPN.DataValueField = "AccCode"
        ddlPPN.DataTextField = "_Description"
        ddlPPN.DataBind()
        ddlPPN.SelectedIndex = intSelectedIndexCOAPPN

        ddlOB.DataSource = objAccDs.Tables(0)
        ddlOB.DataValueField = "AccCode"
        ddlOB.DataTextField = "_Description"
        ddlOB.DataBind()
        ddlOB.SelectedIndex = intSelectedIndexCOAOB

        ddlOL.DataSource = objAccDs.Tables(0)
        ddlOL.DataValueField = "AccCode"
        ddlOL.DataTextField = "_Description"
        ddlOL.DataBind()
        ddlOL.SelectedIndex = intSelectedIndexCOAOL

        ddlOBBiaya.DataSource = objAccDs.Tables(0)
        ddlOBBiaya.DataValueField = "AccCode"
        ddlOBBiaya.DataTextField = "_Description"
        ddlOBBiaya.DataBind()
        ddlOBBiaya.SelectedIndex = intSelectedIndexCOAOBBiaya

        ddlOLBiaya.DataSource = objAccDs.Tables(0)
        ddlOLBiaya.DataValueField = "AccCode"
        ddlOLBiaya.DataTextField = "_Description"
        ddlOLBiaya.DataBind()
        ddlOLBiaya.SelectedIndex = intSelectedIndexCOAOLBiaya

    End Sub

    Sub btnSaveSetting_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_DKtr As String = "WM_WM_CLSTRX_TICKET_COASETTING_UPDATE"
        Dim objDataSet As New Object()
        Dim intErrNo As Integer
        Dim strMn As String
        Dim strYr As String

        strMn = ddlMonth.SelectedItem.Value.Trim
        strYr = ddlyear.SelectedItem.Value.Trim

        strParamName = "COATBSPEMILIK|COATBSAGEN|COAPPN|COAONGKOSBONGKAR|COAONGKOSLAPANGAN|COABIAYAONGKOSBONGKAR|COABIAYAONGKOSLAPANGAN"
        strParamValue = Trim(ddlTBSPemilik.SelectedItem.Value) & "|" & Trim(ddlTBSAgen.SelectedItem.Value) & "|" & Trim(ddlPPN.SelectedItem.Value) & "|" & _
                        Trim(ddlOB.SelectedItem.Value) & "|" & Trim(ddlOL.SelectedItem.Value) & "|" & _
                        Trim(ddlOBBiaya.SelectedItem.Value) & "|" & Trim(ddlOLBiaya.SelectedItem.Value)

        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCd_DKtr, strParamName, strParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MthEnd_PPH21_Estate&errmesg=" & Exp.Message & "&redirect=")
        End Try

        LoadCOASetting()
    End Sub

    Protected Function LoadCOASetting() As DataSet
        Dim strOpCd_DKtr As String = "WM_WM_CLSTRX_TICKET_COASETTING_GET"
        Dim objDataSet As New Object()
        Dim intErrNo As Integer
        Dim strMn As String
        Dim strYr As String

        strMn = ddlMonth.SelectedItem.Value.Trim
        strYr = ddlyear.SelectedItem.Value.Trim

        strParamName = "STRSEARCH"
        strParamValue = ""

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_DKtr, strParamName, strParamValue, objTicketDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MthEnd_PPH21_Estate&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objTicketDs.Tables(0).Rows.Count > 0 Then
            BindAccount(objTicketDs.Tables(0).Rows(0).Item("COATBSPemilik"), objTicketDs.Tables(0).Rows(0).Item("COATBSAgen"), objTicketDs.Tables(0).Rows(0).Item("COAPPN"), objTicketDs.Tables(0).Rows(0).Item("COAOngkosBongkar"), objTicketDs.Tables(0).Rows(0).Item("COAOngkosLapangan"), objTicketDs.Tables(0).Rows(0).Item("COABiayaOngkosBongkar"), objTicketDs.Tables(0).Rows(0).Item("COABiayaOngkosLapangan"))
        Else
            BindAccount("", "", "", "", "", "", "")
        End If
    End Function

    Sub btnRefresh_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_DKtr As String = "WM_WM_CLSTRX_TICKET_GETDATA"
        Dim objDataSet As New Object()
        Dim intErrNo As Integer
        Dim strMn As String
        Dim strYr As String

        strMn = ddlMonth.SelectedItem.Value.Trim
        strYr = ddlyear.SelectedItem.Value.Trim

        If (CDbl(strYr) * 100) + CDbl(strMn) > 201703 Then
            If Trim(strCompany) = "KSJ" Then
                Exit Sub
            Else
                strParamName = "LOCCODE|ACCMONTH|ACCYEAR|UPDATEID"
                strParamValue = strLocation & "|" & strMn & "|" & strYr & "|" & strUserId

                Try
                    intErrNo = objOk.mtdGetDataCommon(strOpCd_DKtr, strParamName, strParamValue, objTicketDs)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MthEnd_PPH21_Estate&errmesg=" & Exp.Message & "&redirect=")
                End Try

                If objTicketDs.Tables(0).Rows.Count > 0 Then
                    lblErrRefresh.Visible = True
                    lblErrRefresh.Text = objTicketDs.Tables(0).Rows(0).Item("Msg")

                    lstDropList.Visible = True
                    btnPrev.Visible = True
                    btnNext.Visible = True
                Else
                    lstDropList.Visible = False
                    btnPrev.Visible = False
                    btnNext.Visible = False
                End If

                dgTicketList.CurrentPageIndex = 0
                dgTicketList.EditItemIndex = -1
                BindGrid()

                dgPPHList.CurrentPageIndex = 0
                dgPPHList.EditItemIndex = -1
                BindGridPPH()
            End If
        Else
            strParamName = "LOCCODE|ACCMONTH|ACCYEAR|UPDATEID"
            strParamValue = strLocation & "|" & strMn & "|" & strYr & "|" & strUserId

            Try
                intErrNo = objOk.mtdGetDataCommon(strOpCd_DKtr, strParamName, strParamValue, objTicketDs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MthEnd_PPH21_Estate&errmesg=" & Exp.Message & "&redirect=")
            End Try

            If objTicketDs.Tables(0).Rows.Count > 0 Then
                lblErrRefresh.Visible = True
                lblErrRefresh.Text = objTicketDs.Tables(0).Rows(0).Item("Msg")

                lstDropList.Visible = True
                btnPrev.Visible = True
                btnNext.Visible = True
            Else
                lstDropList.Visible = False
                btnPrev.Visible = False
                btnNext.Visible = False
            End If

            dgTicketList.CurrentPageIndex = 0
            dgTicketList.EditItemIndex = -1
            BindGrid()

            dgPPHList.CurrentPageIndex = 0
            dgPPHList.EditItemIndex = -1
            BindGridPPH()
        End If

    End Sub

    Sub btnGenerate_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_DKtr As String = "WM_WM_CLSTRX_TICKET_GENERATE_JOURNAL"
        Dim objDataSet As New Object()
        Dim intErrNo As Integer
        Dim strMn As String
        Dim strYr As String

        strMn = ddlMonth.SelectedItem.Value.Trim
        strYr = ddlyear.SelectedItem.Value.Trim

        strParamName = "LOCCODE|ACCMONTH|ACCYEAR|UPDATEID"
        strParamValue = strLocation & "|" & strMn & "|" & strYr & "|" & strUserId

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_DKtr, strParamName, strParamValue, objPPHDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MthEnd_PPH21_Estate&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objPPHDs.Tables(0).Rows.Count > 0 Then
            lblErrGenerate.Visible = True
            lblErrGenerate.Text = objPPHDs.Tables(0).Rows(0).Item("Msg")
        End If

        'LoadData()
        LoadDataPPH()

  

    End Sub

    Sub btnPPHPrintPrev_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim cAccMonth As String = IIf(Len(ddlMonth.SelectedItem.Value) = 1, "0" & Trim(ddlyear.SelectedItem.Value), Trim(ddlMonth.SelectedItem.Value))
        strAccYear = ddlyear.SelectedItem.Value

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=TBSPPH-" & Trim(strLocation) & "-" & Trim(cAccMonth) & Trim(strAccYear) & ".xls")
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.xls"

        Dim stringWrite = New System.IO.StringWriter()
        Dim htmlWrite = New HtmlTextWriter(stringWrite)

        dgPPHList.RenderControl(htmlWrite)
        Response.Write(stringWrite.ToString())
        Response.End()
    End Sub

    Sub btnTicketPrintPrev_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim cAccMonth As String = IIf(Len(ddlMonth.SelectedItem.Value) = 1, "0" & Trim(ddlyear.SelectedItem.Value), Trim(ddlMonth.SelectedItem.Value))
        strAccYear = ddlyear.SelectedItem.Value

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=TBSTICKET-" & Trim(strLocation) & "-" & Trim(cAccMonth) & Trim(strAccYear) & ".xls")
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.xls"

        Dim stringWrite = New System.IO.StringWriter()
        Dim htmlWrite = New HtmlTextWriter(stringWrite)

        dgTicketList.RenderControl(htmlWrite)
        Response.Write(stringWrite.ToString())
        Response.End()
    End Sub

    Sub dgTicketList_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='DarkSeaGreen'")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")

            End If
        End If
    End Sub

    Private Sub dgTicketList_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgTicketList.ItemCreated
        If e.Item.ItemType = ListItemType.Header Then
            Dim dgItem As DataGridItem
            Dim dgCell As TableCell
            dgItem = New DataGridItem(0, 0, ListItemType.Header)

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "NO"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "SUPPLIER/CUSTOMER"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "PRODUK"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 15
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "DATA TIMBANGAN"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 8
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "% POTONGAN"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "JANJANG"
            dgCell.HorizontalAlign = HorizontalAlign.Center


            dgCell = New TableCell()
            dgCell.ColumnSpan = 3
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "BAYAR"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            'dgCell = New TableCell()
            'dgCell.ColumnSpan = 3
            'dgItem.Cells.Add(dgCell)
            'dgCell.Text = "BUAH BESAR"
            'dgCell.HorizontalAlign = HorizontalAlign.Center

            'dgCell = New TableCell()
            'dgCell.ColumnSpan = 3
            'dgItem.Cells.Add(dgCell)
            'dgCell.Text = "BUAH KECIL"
            'dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "TAMBAHAN <BR>HARGA"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 3
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "PPH"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 3
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "ONGKOS BONGKAR"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 3
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "ONGKOS LAPANGAN"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "KODE BAYAR TBS"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "REALISASI BAYAR TBS"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 3
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "TONASE <BR>PKS"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgItem.Font.Bold = True
            dgTicketList.Controls(0).Controls.AddAt(0, dgItem)
        End If
    End Sub

    Private Sub dgTicketList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgTicketList.ItemDataBound
        If e.Item.ItemType = ListItemType.Header Then
            Dim dgItem As DataGridItem

            dgItem = New DataGridItem(0, 0, ListItemType.Header)

            e.Item.Cells(0).Visible = False
            e.Item.Cells(1).Visible = False
            e.Item.Cells(2).Visible = False

            e.Item.Cells(26).Visible = False
            'e.Item.Cells(38).Visible = False

            e.Item.Cells(40).Visible = False
            e.Item.Cells(41).Visible = False
            e.Item.Cells(42).Visible = False

        End If
    End Sub

    Sub dgPPHList_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='DarkSeaGreen'")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")
            End If
        End If
    End Sub

    Private Sub dgPPHList_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPPHList.ItemCreated
        If e.Item.ItemType = ListItemType.Header Then
            Dim dgItem As DataGridItem
            Dim dgCell As TableCell
            dgItem = New DataGridItem(0, 0, ListItemType.Header)

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "NO"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "TANGGAL"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 3
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "SUPPLIER/CUSTOMER"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "TONASE <BR>PKS"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "TONASE DIBAYAR"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "TAMBAHAN <BR>HARGA"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "TOTAL DIBAYAR"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 3
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "PPH"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 3
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "ONGKOS BONGKAR"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 3
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "ONGKOS LAPANGAN"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "NO. JURNAL"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgItem.Font.Bold = True
            dgPPHList.Controls(0).Controls.AddAt(0, dgItem)
        End If
    End Sub

    Private Sub dgPPHList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPPHList.ItemDataBound
        If e.Item.ItemType = ListItemType.Header Then
            Dim dgItem As DataGridItem

            dgItem = New DataGridItem(0, 0, ListItemType.Header)

            e.Item.Cells(0).Visible = False
            e.Item.Cells(1).Visible = False
            e.Item.Cells(5).Visible = False
            'e.Item.Cells(6).Visible = False
            'e.Item.Cells(7).Visible = False
            e.Item.Cells(8).Visible = False
            e.Item.Cells(9).Visible = False
            e.Item.Cells(19).Visible = False
        End If
    End Sub

    Sub dgFFBPriceList_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='DarkSeaGreen'")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")

            End If
        End If
    End Sub

    Sub dgFFFBOB_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='DarkSeaGreen'")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")

            End If
        End If
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

    Sub UserMsgBox(ByVal F As Object, ByVal sMsg As String)
        Dim sb As New StringBuilder()
        Dim oFormObject As System.Web.UI.Control = Nothing
        Try
            sMsg = sMsg.Replace("'", "\'")
            sMsg = sMsg.Replace(Chr(34), "\" & Chr(34))
            sMsg = sMsg.Replace(vbCrLf, "\n")
            sMsg = "<script language='javascript'>alert('" & sMsg & "');</script>"
            sb = New StringBuilder()
            sb.Append(sMsg)
            For Each oFormObject In F.Controls
                If TypeOf oFormObject Is HtmlForm Then
                    Exit For
                End If
            Next
            oFormObject.Controls.AddAt(oFormObject.Controls.Count, New LiteralControl(sb.ToString()))
        Catch ex As Exception

        End Try
    End Sub

End Class
