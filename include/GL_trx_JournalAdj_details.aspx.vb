Imports System
Imports System.Collections
Imports System.Data
Imports System.Data.SqlClient
Imports System.Math
Imports System.Text
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic


Public Class GL_Trx_JournalAdj_Det : Inherits Page

    Protected WithEvents dgResult As DataGrid
    Protected WithEvents lblJrnAdjId As Label
    Protected WithEvents lblBlkTag As Label
    Protected WithEvents lblAccCodeTag As Label
    Protected WithEvents lblReprint As Label
    Protected WithEvents ddlAccMonth As DropDownList
    Protected WithEvents ddlAccYear As DropDownList
    Protected WithEvents ddlReceiveFrom As DropDownList
    Protected WithEvents lstAccCode As DropDownList
    Protected WithEvents lstBlock As DropDownList
    Protected WithEvents lstJrnType As DropDownList
    Protected WithEvents Status As Label
    Protected WithEvents CreateDate As Label
    Protected WithEvents UpdateDate As Label
    Protected WithEvents UpdateBy As Label
    Protected WithEvents lblPrintDate As Label
    Protected WithEvents lblTotAmtFig As Label
    Protected WithEvents txtDate As TextBox
    Protected WithEvents txtDesc As TextBox
    Protected WithEvents txtRefNo As TextBox
    Protected WithEvents txtAmt As TextBox
    Protected WithEvents txtDescLn As TextBox
    Protected WithEvents txtQty As TextBox
    Protected WithEvents txtQtyCR As TextBox
    Protected WithEvents txtPrice As TextBox
    Protected WithEvents txtPriceCR As TextBox
    Protected WithEvents txtDRTotalAmount As TextBox
    Protected WithEvents txtCRTotalAmount As TextBox
    Protected WithEvents TAmt As Label
    Protected WithEvents tblAdd As HtmlTable
    Protected WithEvents lblError As Label
    Protected WithEvents lblFmt As Label
    Protected WithEvents lblDate As Label
    Protected WithEvents lblAccCodeErr As Label
    Protected WithEvents lblBlockErr As Label
    Protected WithEvents lblDescErr As Label
    Protected WithEvents lblStsHid As Label
    Protected WithEvents lblCtrlAmtFig As Label
    Protected WithEvents lblNoQty As Label
    Protected WithEvents lblTwoQty As Label
    Protected WithEvents lblNoPrice As Label
    Protected WithEvents lblTwoPrice As Label
    Protected WithEvents lblTwoAmount As Label
    Protected WithEvents lblDRorCR As Label
    Protected WithEvents btnNew As ImageButton
    Protected WithEvents btnAdd As ImageButton
    Protected WithEvents btnUpdate As ImageButton
    Protected WithEvents btnSave As ImageButton
    Protected WithEvents btnPost As ImageButton
    Protected WithEvents btnPrint As ImageButton
    Protected WithEvents btnDelete As ImageButton
    Protected WithEvents btnBack As ImageButton
    Protected WithEvents btnSelDate As Image
    Protected WithEvents Find As HtmlInputButton
    Protected WithEvents blnShortCut As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblPleaseSelect As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents lblTxLnID As Label
    Protected WithEvents SearchBtn As Button

    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objHRTrx As New agri.HR.clsTrx()
    Dim objGLTrx As New agri.GL.clsTrx()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objAdmAcc As New agri.Admin.clsAccPeriod()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()

    Dim strOpCd_Jrn_Add As String = "GL_CLSTRX_JOURNALADJ_DETAIL_ADD"
    Dim strOpCd_Jrn_Upd As String = "GL_CLSTRX_JOURNALADJ_DETAIL_UPD"
    Dim strOpCd_JrnLn_Get As String = "GL_CLSTRX_JOURNALADJ_LINE_GET"
    Dim strOpCd_JrnLn_Upd As String = "GL_CLSTRX_JOURNALADJ_LINE_UPD"


    Dim objDataSet As New DataSet()
    Dim dsGrid As New DataSet()
    Dim objLangCapDs As New Dataset()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intGLAR As Integer
    Dim strDateFMT As String
    Dim intConfigSetting As Integer

    Dim strAccountTag As String
    Dim strVehTag As String
    Dim strBlockTag As String
    Dim strVehExpTag As String
    Dim rptAccTag As String
    Dim rptBlkTag As String
    Dim rptLocation As String
    Dim intErrNo As Integer
    Dim strParam As String = ""
	
    Dim strSelAccMonth As String
    Dim strSelAccYear As String
	Dim strLocType as String
    Dim intLevel As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        strDateFMT = Session("SS_DATEFMT")
        intGLAR = Session("SS_GLAR")
        intConfigSetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")
        intLevel = Session("SS_USRLEVEL")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLJournalAdj), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()

            'to avoid double click, on aspx add this : UseSubmitBehavior="false"
            btnAdd.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnAdd).ToString())
            btnNew.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnNew).ToString())
            btnSave.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnSave).ToString())
            btnPost.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnPost).ToString())
            btnDelete.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnDelete).ToString())
            btnPrint.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnPrint).ToString())
            btnBack.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnBack).ToString())

            If Not Page.IsPostBack Then
                lblJrnAdjId.Text = Request.QueryString("Id")
                If Not Request.QueryString("Id") = "" Then
                    LoadJournalTrx()
                    If objDataSet.Tables(0).Rows.Count > 0 Then
                        DisplayFromDB()
                        BindGrid()
                    End If
                Else
                    txtDate.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Now)
                    OnLoad_GetAccPeriod("", "", True)
                    OnLoad_GetAccPeriod("", strAccYear, True)
                    ddlAccYear.SelectedValue = strAccYear
                    'ddlAccMonth.SelectedValue = strAccMonth - 1
                End If
                BindAccCodeDropList()
                BindBlockDropList("")
                BindTxTypeList()
                PageControl()
            End If
            lblNoQty.Visible = False
            lblTwoQty.Visible = False
            lblNoPrice.Visible = False
            lblTwoPrice.Visible = False
            lblTwoAmount.Visible = False
            lblDRorCR.Visible = False
            lblError.Visible = False
            lblDate.Visible = False
            lblFmt.Visible = False
            lblAccCodeErr.Visible = False
            lblBlockErr.Visible = False
            lblDescErr.Visible = False
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        Try
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigSetting) = True Then
                strBlockTag = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text
                rptBlkTag = GetCaption(objLangCap.EnumLangCap.Block)
            Else
                strBlockTag = GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.Text
                rptBlkTag = GetCaption(objLangCap.EnumLangCap.SubBlock)
            End If

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_TRX_JOURNALADJ_DETAILS_GET_COSTLEVEL_LANGCAP&errmesg=" & Exp.ToString() & "&redirect=gl/trx/GL_trx_JournalAdj_list.aspx")
        End Try

        strAccountTag = GetCaption(objLangCap.EnumLangCap.Account) & lblCode.Text
        strVehTag = GetCaption(objLangCap.EnumLangCap.Vehicle) & lblCode.Text
        strVehExpTag = GetCaption(objLangCap.EnumLangCap.VehExpense) & lblCode.Text
        rptAccTag = GetCaption(objLangCap.EnumLangCap.Account)
        rptLocation = GetCaption(objLangCap.EnumLangCap.Location)

        lblAccCodeTag.Text = strAccountTag & " :* "
        lblBlkTag.Text = strBlockTag & " : "

        lblAccCodeErr.Text = "<BR>" & lblPleaseSelect.Text & strAccountTag
        lblBlockErr.Text = lblPleaseSelect.Text & strBlockTag

        dgResult.Columns(2).HeaderText = strAccountTag
        dgResult.Columns(3).HeaderText = strBlockTag

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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_TRX_JOURNALADJ_DETAIL_GET_LANGCAP&errmesg=" & Exp.ToString() & "&redirect=gl/trx/GL_trx_JournalAdj_list.aspx")
        End Try

    End Sub



    Function GetCaption(ByVal pv_TermCode) As String
            Dim count As Integer

            For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
                If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                    If strLocType = objAdminLoc.EnumLocType.Mill then
                        Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                    else
                        Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                    end if
                    Exit For
                End If
            Next
        End Function



    Sub OnLoad_GetAccPeriod(ByVal pv_strAccMonth As String, ByVal pv_strAccYear As String, ByVal pv_blnIsNewTrx As Boolean)
        Dim objAccCfg As New Dataset()
        Dim strOpCd_AccCfg_Get As String = "ADMIN_CLSACCPERIOD_CONFIG_ACCYEAR_GET"
        Dim intMaxPeriod As Integer
        Dim intAccMonth As Integer
        Dim _strAccYear As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strParam As String

        If pv_strAccYear = "" Then
            ddlAccYear.Items.Clear
            'If strAccMonth > 1 Then
            If strAccMonth > 0 Then
                For intCnt = (Convert.ToInt16(strAccYear) - 1) To Convert.ToInt16(strAccYear)
                    ddlAccYear.Items.Add(New ListItem(intCnt, intCnt))
                Next
            Else
                For intCnt = (Convert.ToInt16(strAccYear) - 1) To (Convert.ToInt16(strAccYear) - 1)
                    ddlAccYear.Items.Add(New ListItem(intCnt, intCnt))
                Next
            End If
            ddlAccYear.SelectedIndex = 0
            _strAccYear = ddlAccYear.SelectedItem.Value
        ElseIf pv_strAccMonth <> "" And pv_strAccYear <> "" Then
            ddlAccYear.Items.Add(New ListItem(pv_strAccYear, pv_strAccYear))
            ddlAccYear.SelectedIndex = 0
        Else
            _strAccYear = pv_strAccYear
        End If



        ddlAccMonth.Items.Clear()
        If pv_strAccMonth = "" And pv_strAccYear = strAccYear Then      
            intAccMonth = Convert.ToInt16(strAccMonth) - 1
            For intCnt = 1 To intAccMonth
                ddlAccMonth.Items.Add(New ListItem(intCnt, intCnt))
            Next
            ddlAccMonth.SelectedIndex = intAccMonth - 1
        ElseIf pv_strAccMonth <> "" And pv_strAccYear <> "" Then        
            ddlAccMonth.Items.Add(New ListItem(pv_strAccMonth, pv_strAccMonth))
            ddlAccMonth.SelectedIndex = 0
        Else                                                            
            Try
                strParam = "||" & _strAccYear
                intErrNo = objAdmAcc.mtdGetAccPeriodCfg(strOpCd_AccCfg_Get, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strParam, _
                                                        objAccCfg)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_TRX_JOURNALADJ_DETAIL_ACCCFG_GET&errmesg=" & Exp.ToString() & "&redirect=GL/trx/GL_trx_JournalAdj_List.aspx")
            End Try

            Try
                intAccMonth = Convert.ToInt16(objAccCfg.Tables(0).Rows(0).Item("MaxPeriod"))
            Catch Exp As System.Exception
               Response.Redirect("GL_trx_JournalAdj_List.aspx?&errmsg=System required period configuration to process your request. Please set period configuration for the year of " & _strAccYear)
            End Try
            objAccCfg = Nothing

            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.UseAdjPeriod), intConfigSetting) = True Then
                For intCnt = 1 To intAccMonth + 1   
                    ddlAccMonth.Items.Add(New ListItem(intCnt, intCnt))
                Next
                ddlAccMonth.SelectedIndex = intAccMonth
            Else
                For intCnt = 1 To intAccMonth       
                    ddlAccMonth.Items.Add(New ListItem(intCnt, intCnt))
                Next
                ddlAccMonth.SelectedIndex = intAccMonth - 1
            End If
        End If
    End Sub

    Sub OnIndexChage_ReloadAccPeriod(ByVal sender As Object, ByVal e As System.EventArgs)
        OnLoad_GetAccPeriod("", ddlAccYear.SelectedItem.Value, False)
    End Sub

    Sub PageControl()
        btnSelDate.Enabled = False
        btnSave.Visible = False
        btnPost.Visible = False
        btnDelete.Visible = False
        btnPrint.Visible = False
        btnNew.Visible = False
        txtDate.Enabled = False
        tblAdd.Visible = False
        btnAdd.Enabled = False
        ddlAccMonth.Enabled = False
        ddlAccYear.Enabled = False
        txtPrice.Enabled = False
        txtQty.Enabled = False
        txtPriceCR.Enabled = False
        txtQtyCR.Enabled = False
        txtDRTotalAmount.Enabled = False
        txtCRTotalAmount.Enabled = False
        txtDesc.Enabled = False
        txtDescLn.Enabled = False
        txtRefNo.Enabled = False
        txtAmt.Enabled = False
        lstAccCode.Enabled = False
        lstBlock.Enabled = False
        ddlReceiveFrom.Enabled = False
        lstJrnType.Enabled = False

        If lblStsHid.Text = Convert.ToString(objGLTrx.EnumJournalAdjStatus.Active) Then
            tblAdd.Visible = True
            txtDescLn.Enabled = True
            lstAccCode.Enabled = True
            lstBlock.Enabled = True
            txtPrice.Enabled = True
            txtQty.Enabled = True
            txtPriceCR.Enabled = True
            txtQtyCR.Enabled = True
            txtDRTotalAmount.Enabled = True
            txtCRTotalAmount.Enabled = True
            btnAdd.Enabled = True
            btnDelete.Visible = True
            btnNew.Visible = True
            'If Convert.ToDouble(Replace(Replace(lblCtrlAmtFig.Text.Trim,".",""),",",".")) = 0 And Convert.ToDouble(Replace(Replace(lblTotAmtFig.Text.Trim,".",""),",",".")) = Convert.ToDouble(txtAmt.Text) Then
            'btnPost.Visible = True
            ' If
        ElseIf lblStsHid.Text = Convert.ToString(objGLTrx.EnumJournalAdjStatus.Posted) Then
        btnPrint.Visible = True
        btnNew.Visible = True
        ElseIf lblJrnAdjId.Text = "" Then
        btnSelDate.Enabled = True
        txtDate.Enabled = True
        tblAdd.Visible = True
        btnAdd.Enabled = True
        ddlAccMonth.Enabled = True
        ddlAccYear.Enabled = True
        txtPrice.Enabled = True
        txtQty.Enabled = True
        txtPriceCR.Enabled = True
        txtQtyCR.Enabled = True
        txtDRTotalAmount.Enabled = True
        txtCRTotalAmount.Enabled = True
        txtDesc.Enabled = True
        txtDescLn.Enabled = True
        txtRefNo.Enabled = True
        txtAmt.Enabled = True
        lstAccCode.Enabled = True
        lstBlock.Enabled = True
        ddlReceiveFrom.Enabled = True
        lstJrnType.Enabled = True
        End If
    End Sub

    Sub DataGrid_ItemCreated(ByVal Sender As Object, ByVal e As DataGridItemEventArgs)
        If dgResult.Enabled = True Then
            Select Case e.Item.ItemType
                Case ListItemType.Item, ListItemType.AlternatingItem
                    Dim lbl As Label
                    Dim lblAmt As Label
                    lbl = e.Item.FindControl("lblIdx")
                    lbl.Text = e.Item.ItemIndex.ToString + 1

                    Dim DeleteButton As LinkButton
                    Dim EditButton As LinkButton
                    Select Case Status.Text.Trim
                        Case objGLtrx.mtdGetJournalAdjStatus(objGLtrx.EnumJournalAdjStatus.Active)
                            DeleteButton = e.Item.FindControl("Delete")
                            DeleteButton.Visible = True
                            DeleteButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                            EditButton = e.Item.FindControl("Edit")
                            EditButton.Visible = True
                        Case objGLtrx.mtdGetJournalAdjStatus(objGLtrx.EnumJournalAdjStatus.Deleted), _
                              objGLtrx.mtdGetJournalAdjStatus(objGLtrx.EnumJournalAdjStatus.Posted), _
                              objGLtrx.mtdGetJournalAdjStatus(objGLtrx.EnumJournalAdjStatus.Closed)
                            DeleteButton = e.Item.FindControl("Delete")
                            DeleteButton.Visible = False
                            EditButton = e.Item.FindControl("Edit")
                            EditButton.Visible = False
                    End Select

                    lblAmt = e.Item.FindControl("lblAmount")
                    lbl = e.Item.FindControl("lblAccTx")


                    If Sign(CDbl(Replace(Replace(lblAmt.Text.Trim,".",""),",","."))) = 1 Then 
                        lbl.Text = "DR"
                    ElseIf Sign(CDbl(Replace(Replace(lblAmt.Text.Trim,".",""),",","."))) = -1 Then
                        lbl.Text = "CR"
                    End If

                Case ListItemType.EditItem
                    Dim lbl As Label
                    Dim lblAmt As Label
                    lbl = e.Item.FindControl("lblIdx")
                    lbl.Text = e.Item.ItemIndex.ToString + 1

                    lblAmt = e.Item.FindControl("lblAmount")
                    lbl = e.Item.FindControl("lblAccTx")


                    If Sign(CDbl(Replace(Replace(lblAmt.Text.Trim,".",""),",","."))) = 1 Then
                        lbl.Text = "DR"
                    ElseIf Sign(CDbl(Replace(Replace(lblAmt.Text.Trim,".",""),",","."))) = -1 Then
                        lbl.Text = "CR"
                    End If
            End Select

        Else
            Select Case e.Item.ItemType
                Case ListItemType.Item, ListItemType.AlternatingItem
                    Dim lbl As Label
                    lbl = e.Item.FindControl("lblIdx")
                    lbl.Text = e.Item.ItemIndex.ToString + 1

                    Dim DeleteButton As LinkButton
                    DeleteButton = e.Item.FindControl("Delete")
                    DeleteButton.Enabled = False
            End Select
        End If
    End Sub

    Sub DisplayFromDB()
        ddlReceiveFrom.SelectedIndex = Convert.ToInt16(objDataSet.Tables(0).Rows(0).Item("ReceiveFrom")) - 1
        Status.Text = objGLTrx.mtdGetJournalAdjStatus(objDataSet.Tables(0).Rows(0).Item("Status").Trim())
        lblStsHid.Text = objDataSet.Tables(0).Rows(0).Item("Status").Trim()
        CreateDate.Text = objGlobal.GetLongDate(objDataSet.Tables(0).Rows(0).Item("CreateDate"))
        UpdateDate.Text = objGlobal.GetLongDate(objDataSet.Tables(0).Rows(0).Item("UpdateDate"))
        UpdateBy.Text = objDataSet.Tables(0).Rows(0).Item("UserName").Trim()
        
        lblCtrlAmtFig.Text = objGlobal.GetIDDecimalSeparator(FormatNumber(objDataSet.Tables(0).Rows(0).Item("GrandTotal"),0,True,False,False))
        lblPrintDate.Text = objGlobal.GetLongDate(objDataSet.Tables(0).Rows(0).Item("PrintDate"))
        txtDesc.Text = objDataSet.Tables(0).Rows(0).Item("Description").Trim()
        txtRefNo.Text = objDataSet.Tables(0).Rows(0).Item("RefNo").Trim()
        txtDate.Text = objGlobal.GetShortDate(strDateFMT, objDataSet.Tables(0).Rows(0).Item("DocDate"))
        txtAmt.Text = objDataSet.Tables(0).Rows(0).Item("DocAmt")
        OnLoad_GetAccPeriod(objDataSet.Tables(0).Rows(0).Item("AccMonth").Trim(), objDataSet.Tables(0).Rows(0).Item("AccYear").Trim(), False)
    End Sub

    Sub Initialize()
        lstAccCode.SelectedIndex = 0
        lstBlock.SelectedIndex = 0
    End Sub

    Sub BindGrid()
        Dim intCnt As Integer
        Dim Total As Double

        dgResult.DataSource = LoadDataGrid()
        dgResult.DataBind()

        Total = 0
        For intCnt = 0 To dsGrid.Tables(0).Rows.Count - 1
            If Sign(dsGrid.Tables(0).Rows(intCnt).Item("Total")) = 1 Then
                Total += dsGrid.Tables(0).Rows(intCnt).Item("Total")
            End If
        Next intCnt
        
        lblTotAmtFig.Text = objGlobal.GetIDDecimalSeparator(FormatNumber(Total,0,True,False,False))
        
    End Sub

    Protected Function LoadDataGrid() As DataSet
        Dim strParam As String

        Try
            strParam = Trim(lblJrnAdjId.Text)
            intErrNo = objGLTrx.mtdGetJournalAdj(strCompany, _
                                                 strLocation, _
                                                 strAccMonth, _
                                                 strAccYear, _
                                                 strUserId, _
                                                 strOpCd_JrnLn_Get, _
                                                 strParam, _
                                                 dsGrid)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_TRX_JOURNALADJ_GLTRX_LOAD&errmesg=" & Exp.ToString() & "&redirect=GL/Trx/GL_Trx_JournalAdj_List.aspx")
        End Try

        Return dsGrid
        dsGrid = Nothing
    End Function

    Sub LoadJournalTrx()
        Dim strOpCd_Jrn_Get As String = "GL_CLSTRX_JOURNALADJ_DETAIL_GET"
        Dim StockCode As String

        strParam = Trim(lblJrnAdjId.Text)
        Try
            intErrNo = objGLTrx.mtdGetJournalAdj(strCompany, _
                                                strLocation, _
                                                strAccMonth, _
                                                strAccYear, _
                                                strUserId, _
                                                strOpCd_Jrn_Get, _
                                                strParam, _
                                                objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_TRX_JOURNALADJ_STKTRX_LOAD&errmesg=" & Exp.ToString() & "&redirect=GL/Trx/GL_Trx_JournalAdj_List.aspx")
        End Try
    End Sub

    Protected Function CheckDate() As String
        Dim objDateFormat As String
        Dim strValidDate As String

        If Not txtDate.Text = "" Then
            If objGlobal.mtdValidInputDate(strDateFMT, txtDate.Text, objDateFormat, strValidDate) = True Then
                Return strValidDate
            Else
                lblFmt.Text = objDateFormat & "."
                lblDate.Visible = True
                lblFmt.Visible = True
            End If
        End If
    End Function

    Sub OnChange_Reload(ByVal sender As Object, ByVal e As System.EventArgs)
        RebindAccount()
    End Sub

    Sub GetAccountDetails(ByVal pv_strAccCode As String, _
                          ByRef pr_strAccType As Integer, _
                          ByRef pr_strAccPurpose As Integer, _
                          ByRef pr_strNurseryInd As Integer, _
                          ByRef pr_blnFound As Boolean)

        Dim _objAccDs As New DataSet()
        Dim strOpCd As String = "GL_CLSSETUP_CHARTOFACCOUNT_GET_BY_ACCCODE"
        Dim strParam As String = pv_strAccCode
        Dim intErrNo As Integer

        Try
            intErrNo = objGLSetup.mtdGetAccount(strOpCd, _
                                                strParam, _
                                                _objAccDs, _
                                                True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_TRX_JOURNALADJ_ACC_GET&errmesg=" & Exp.ToString() & "&redirect=GL/trx/GL_trx_JournalAdj_List.aspx")
        End Try

        If _objAccDs.Tables(0).Rows.Count = 1 Then
            pr_strAccType = Convert.ToInt16(_objAccDs.Tables(0).Rows(0).Item("AccType"))
            pr_strAccPurpose = Convert.ToInt16(_objAccDs.Tables(0).Rows(0).Item("AccPurpose"))
            pr_strNurseryInd = Convert.ToInt16(_objAccDs.Tables(0).Rows(0).Item("NurseryInd"))
            pr_blnFound = True
        Else
            pr_blnFound = False
            pr_strAccType = 0
            pr_strAccPurpose = 0
            pr_strNurseryInd = 0
        End If
    End Sub

    Sub RebindAccount()
        Dim intAccType As Integer
        Dim intAccPurpose As Integer
        Dim blnFound As Boolean
        Dim strAcc As String = Request.Form("lstAccCode")
        Dim strBlk As String = Request.Form("lstBlock")


            BindBlockDropList(strAcc, strBlk)
    End Sub

    Function CheckRequiredField() As Boolean
        Dim intAccType As Integer
        Dim intAccPurpose As Integer
        Dim intNurseryInd As Integer        
        Dim blnFound As Boolean
        Dim strAcc As String = Request.Form("lstAccCode")
        Dim strBlk As String = Request.Form("lstBlock")

        If txtDescLn.Text = "" Then
            lblDescErr.Visible = True
            Return True
        End If

        If strAcc = "" Then
            lblAccCodeErr.Visible = True
            Return True
        End If

        GetAccountDetails(strAcc, intAccType, intAccPurpose, intNurseryInd, blnFound)

        If blnFound = True Then
            If intAccType = objGLSetup.EnumAccountType.BalanceSheet And intNurseryInd = objGLSetup.EnumNurseryAccount.No Then
                Return False
            ElseIf intAccType = objGLSetup.EnumAccountType.BalanceSheet And intNurseryInd = objGLSetup.EnumNurseryAccount.Yes Then
                If strBlk = "" Then
                    lblBlockErr.Visible = True
                    Return True
                Else
                    Return False
                End If
            ElseIf intAccType = objGLSetup.EnumAccountType.ProfitAndLost And _
                intAccPurpose = objGLSetup.EnumAccountPurpose.NonVehicle And _
                strBlk = "" Then
                lblBlockErr.Visible = True
                Return True
            Else
                Return False
            End If
        End If
    End Function

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim btn As LinkButton
        Dim lbl As Label
        Dim Delbutton As LinkButton
        Dim strDesc As String
        Dim strAccCode As String
        Dim strBlkCode As String
        Dim intAccType As Integer
        Dim intAccPurpose As Integer
        Dim intNurseryInd As Integer
        Dim blnFound As Boolean
        Dim strDate As String = CheckDate()

        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        If Session("SS_FILTERPERIOD") = "0" Then
            If intCurPeriod < intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
        Else
            If intSelPeriod <> intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
            If intSelPeriod < intCurPeriod And intLevel < 2 Then
                lblDate.Visible = True
                lblDate.Text = "This period already locked."
                Exit Sub
            End If
        End If

        blnUpdate.Text = True
        dgResult.EditItemIndex = CInt(E.Item.ItemIndex)

        lbl = E.Item.FindControl("lblID")
        lblTxLnID.Text = lbl.Text.Trim

        lbl = E.Item.FindControl("lblDesc")
        txtDescLn.Text = lbl.Text.Trim
        lbl = E.Item.FindControl("lblAccTx")

        If lbl.Text.Trim = "DR" Then
            lbl = E.Item.FindControl("lblQtyTrx")
            txtQty.Text = lbl.Text.Trim
            lbl = E.Item.FindControl("lblUnitCost")
            txtPrice.Text = lbl.Text.Trim
            lbl = E.Item.FindControl("lblAmt")
            txtDRTotalAmount.Text = lbl.Text.Trim
        ElseIf lbl.Text = "CR" Then
            lbl = E.Item.FindControl("lblQtyTrx")
            txtQtyCR.Text = lbl.Text.Trim
            lbl = E.Item.FindControl("lblUnitCost")
            
            txtPriceCR.Text = Replace(Replace(Replace(lbl.Text.Trim, ".", ""), ",", "."), "-", "") 'FormatNumber(Abs(CDbl(lbl.Text.Trim)),2,True,False,False)
            lbl = E.Item.FindControl("lblAmt")
            txtCRTotalAmount.Text = Replace(Replace(Replace(lbl.Text.Trim, ".", ""), ",", "."), "-", "") 'FormatNumber(Abs(CDbl(lbl.Text.Trim)),2,True,False,False)
        End If

        lbl = E.Item.FindControl("lblAccCode")
        strAccCode = lbl.Text.Trim
        lbl = E.Item.FindControl("lblBlkCode")
        strBlkCode = lbl.Text.Trim

        BindAccCodeDropList(strAccCode)
        GetAccountDetails(strAccCode, intAccType, intAccPurpose, intNurseryInd, blnFound)

        If intAccType = objGLSetup.EnumAccountType.ProfitAndLost Then
            BindBlockDropList(strAccCode, strBlkCode)
        End If

        Delbutton = E.Item.FindControl("Delete")
        Delbutton.Visible = False

        BindGrid()

        If lblTxLnID.Text <> "" Then
            btnAdd.Visible = False
            btnUpdate.Visible = True
        Else
            btnAdd.Visible = True
            btnUpdate.Visible = False
        End If
    End Sub

    Sub DEDR_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)

        lblTxLnID.Text = ""

        txtDescLn.Text = ""
        txtQty.Text = ""
        txtQtyCR.Text = ""
        txtPrice.Text = ""
        txtPriceCR.Text = ""
        txtDRTotalAmount.Text = ""
        txtCRTotalAmount.Text = ""

        Initialize()

        dgResult.EditItemIndex = -1
        BindGrid()
        PageControl()
        btnAdd.Visible = True
        btnUpdate.Visible = False
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strDate As String = CheckDate()

        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        If Session("SS_FILTERPERIOD") = "0" Then
            If intCurPeriod < intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
        Else
            If intSelPeriod <> intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
            If intSelPeriod < intCurPeriod And intLevel < 2 Then
                lblDate.Visible = True
                lblDate.Text = "This period already locked."
                Exit Sub
            End If
        End If

        If lblStsHid.Text = Convert.ToString(objGLTrx.EnumJournalAdjStatus.Active) Then
            Dim strOpCd_JrnLn_Del As String = "GL_CLSTRX_JOURNALADJ_LINE_DEL"
            Dim lbl As Label
            Dim id As String
            Dim ItemCode As String
            Dim strParam As String
            Dim StrTxParam As String
            Dim TxID As String
            Dim ErrorChk As Integer = objGLTrx.EnumGeneralLedgerTxErrorType.NoError

            lbl = E.Item.FindControl("lblID")
            id = lbl.Text

            strParam = id & "|" & Trim(lblJrnAdjId.Text)
            Try
                intErrNo = objGLTrx.mtdDelTransactLn(strOpCd_JrnLn_Del, strParam)
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_TRX_JOURNALADJ_TRXLINE_DEL&errmesg=" & Exp.ToString() & "&redirect=GL/Trx/GL_Trx_JournalAdj_List.aspx")
                End If

            End Try

            StrTxParam = lblJrnAdjId.Text & "||||||||||" & strLocation & "||"

            Try
                intErrNo = objGLTrx.mtdUpdJournalAdjDetail(strOpCd_Jrn_Add, _
                                                        strOpCd_Jrn_Upd, _
                                                        strOpCd_JrnLn_Get, _
                                                        strUserId, _
                                                        StrTxParam.ToString, _
                                                        objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.GLTrxJournalAdj), _
                                                        ErrorChk, _
                                                        TxID)

                If ErrorChk = objGLTrx.EnumGeneralLedgerTxErrorType.Overflow Then
                    lblError.Visible = True
                End If

                lblJrnAdjId.Text = TxID
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_TRX_JOURNALADJ_JRN_DETAIL_UPD&errmesg=" & Exp.ToString() & "&redirect=GL/Trx/GL_Trx_Journal_List.aspx")
                End If
            End Try
            LoadJournalTrx()
            DisplayFromDB()
            BindGrid()
            PageControl()
        End If
    End Sub

    Sub BindTxTypeList()
        Dim IntCnt As Integer

        lstJrnType.Items.Add(New ListItem(objGLTrx.mtdGetJournalAdjType(objGLTrx.EnumJournalAdjType.Adjustment), objGLTrx.EnumJournalAdjType.Adjustment))
        lstJrnType.Items.Add(New ListItem(objGLTrx.mtdGetJournalAdjType(objGLTrx.EnumJournalAdjType.CreditNote), objGLTrx.EnumJournalAdjType.CreditNote))
        lstJrnType.Items.Add(New ListItem(objGLTrx.mtdGetJournalAdjType(objGLTrx.EnumJournalAdjType.DebitNote), objGLTrx.EnumJournalAdjType.DebitNote))
        lstJrnType.Items.Add(New ListItem(objGLTrx.mtdGetJournalAdjType(objGLTrx.EnumJournalAdjType.Invoice), objGLTrx.EnumJournalAdjType.Invoice))
        lstJrnType.Items.Add(New ListItem(objGLTrx.mtdGetJournalAdjType(objGLTrx.EnumJournalAdjType.Umum), objGLTrx.EnumJournalAdjType.Umum))
        If lblJrnAdjId.Text <> "" Then
            For IntCnt = 0 To lstJrnType.Items.Count - 1
                If objDataSet.Tables(0).Rows(0).Item("TransactType").Trim() = lstJrnType.Items(IntCnt).Value Then
                    lstJrnType.SelectedIndex = IntCnt
                End If
            Next

        End If

    End Sub

    Sub BindBlockDropList(ByVal pv_strAccCode As String, Optional ByVal pv_strBlkCode As String = "")
        Dim dsForDropDown As DataSet
        Dim intSelectedIndex As Integer = 0
        Dim intCnt As Integer
        Dim strOpCdBlockList_Get As String
        Dim strParam As String
        Dim dr As DataRow

        Try
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigSetting) = True Then
                strOpCdBlockList_Get = "GL_CLSSETUP_ACCOUNT_BLOCK_GET"
                strParam = pv_strAccCode & "|" & Session("SS_LOCATION") & "|" & objGLSetup.EnumBlockStatus.Active
            Else
                strOpCdBlockList_Get = "GL_CLSSETUP_ACCOUNT_SUBBLOCK_GET"
                strParam = pv_strAccCode & "|" & Session("SS_LOCATION") & "|" & objGLSetup.EnumSubBlockStatus.Active
            End If
            intErrNo = objGLSetup.mtdGetAccountBlock(strOpCdBlockList_Get, _
                                                   strParam, _
                                                   dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSTRX_JOURNALADJ_ACCBLK_GET&errmesg=" & Exp.ToString() & "&redirect=GL/trx/GL_trx_JournalAdj_List.aspx")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode") = dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode").Trim()
            dsForDropDown.Tables(0).Rows(intCnt).Item("Description") = dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode") & " (" & dsForDropDown.Tables(0).Rows(intCnt).Item("Description").Trim() & ")"
            If dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(pv_strBlkCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = dsForDropDown.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("Description") = lblSelect.Text & strBlockTag
        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)
        lstBlock.DataSource = dsForDropDown.Tables(0)
        lstBlock.DataValueField = "BlkCode"
        lstBlock.DataTextField = "Description"
        lstBlock.DataBind()
        lstBlock.SelectedIndex = intSelectedIndex

        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If
    End Sub

    Sub BindAccCodeDropList(Optional ByVal pv_strAccCode As String = "")

        Dim strOpCd As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        Dim dr As DataRow
        'Dim strParam As String = "Order By ACC.AccCode| And ACC.Status = '" & _
        '                         Convert.ToString(objGLSetup.EnumAccountCodeStatus.Active) & "' " & _
        '                         " And (AccType = '" & objGLSetup.EnumAccountType.BalanceSheet & "' Or (AccType = '" & _
        '                         objGLSetup.EnumAccountType.ProfitAndLost & "' And AccPurpose = '" & _
        '                         objGLSetup.EnumAccountPurpose.NonVehicle & "')) "

        Dim strParam As String = "Order By ACC.AccCode| And ACC.Status = '" & _
                                Convert.ToString(objGLSetup.EnumAccountCodeStatus.Active) & "' " 

        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim dsForDropDown As DataSet

        strParam = strParam & IIf(Session("SS_COACENTRALIZED") = "1", "", " AND LocCode = '" & Trim(strLocation) & "' ")

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.AccountCode, _
                                                   dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_TRX_JOURNALADJ_ACC_BIND&errmesg=" & Exp.ToString() & "&redirect=GL/trx/GL_trx_JournalAdj_List.aspx")
        End Try

        pv_strAccCode = Trim(pv_strAccCode)
        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            If dsForDropDown.Tables(0).Rows(intCnt).Item("AccCode") = pv_strAccCode Then
                intSelectedIndex = intCnt + 1
                Exit For
            End If
        Next

        dr = dsForDropDown.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("_Description") = lblSelect.Text & strAccountTag
        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)

        lstAccCode.DataSource = dsForDropDown.Tables(0)
        lstAccCode.DataValueField = "AccCode"
        lstAccCode.DataTextField = "_Description"
        lstAccCode.DataBind()
        lstAccCode.SelectedIndex = intSelectedIndex

        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If
    End Sub


    Sub btnAdd_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        If CheckRequiredField() Then
            Exit Sub
        End If

        Dim strOpCd_Add_JrnLine As String = "GL_CLSTRX_JOURNALADJ_LINE_ADD"
        Dim strStatus As String
        Dim TxID As String
        Dim IssType As String
        Dim strAmount As String
        Dim StrTxParam As New StringBuilder()
        Dim strTxLnParam As New StringBuilder()
        Dim ErrorChk As Integer = objGLTrx.EnumGeneralLedgerTxErrorType.NoError
        Dim dblQty As Double
        Dim dblPrice As Double
        Dim dblTotal As Double = 0
        Dim strNewIDFormat As String
        Dim strDate As String = CheckDate()

        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        If Session("SS_FILTERPERIOD") = "0" Then
            If intCurPeriod < intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
        Else
            If intSelPeriod <> intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
            If intSelPeriod < intCurPeriod And intLevel < 2 Then
                lblDate.Visible = True
                lblDate.Text = "This period already locked."
                Exit Sub
            End If
        End If

        If txtQty.Text = "" And txtQtyCR.Text = "" Then
            lblNoQty.Visible = True
            Exit Sub
        ElseIf txtQty.Text <> "" And txtQtyCR.Text <> "" Then
            lblTwoQty.Visible = True
            Exit Sub
        ElseIf txtPrice.Text = "" And txtPriceCR.Text = "" Then
            lblNoPrice.Visible = True
            Exit Sub
        ElseIf txtPrice.Text <> "" And txtPriceCR.Text <> "" Then
            lblTwoPrice.Visible = True
            Exit Sub
        ElseIf txtDRTotalAmount.Text <> "" And txtCRTotalAmount.Text <> "" Then
            lblTwoAmount.Visible = True
            Exit Sub
        ElseIf (txtQty.Text = "" And txtPrice.Text <> "") Or _
               (txtQty.Text <> "" And txtPrice.Text = "") Or _
               (txtQtyCR.Text = "" And txtPriceCR.Text <> "") Or _
               (txtQtyCR.Text <> "" And txtPriceCR.Text = "") Then
            lblDRorCR.Visible = True
            Exit Sub
        End If

        If txtQty.Text <> "" And txtPrice.Text <> "" Then
            dblQty = txtQty.Text
            dblPrice = txtPrice.Text
            If txtDRTotalAmount.Text <> "" Then
                dblTotal = Convert.ToDouble(txtDRTotalAmount.Text)
            End If
        Else
            dblQty = txtQtyCR.Text
            dblPrice = objGlobal.mtdGetReverseValue(txtPriceCR.Text)
            If txtCRTotalAmount.Text <> "" Then
                dblTotal = Convert.ToDouble(txtCRTotalAmount.Text)
                dblTotal = dblTotal - (dblTotal * 2)
            End If
        End If

        strAccYear = Year(strDate)
        strAccMonth = Month(strDate)
        strNewIDFormat = "MMA" & "/" & strCompany & "/" & strLocation & "/" & Trim(lstJrnType.SelectedItem.Value) & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"

        If lblJrnAdjId.Text = "" Then
            Try
                StrTxParam.Append(lblJrnAdjId.Text)
                StrTxParam.Append("|")
                StrTxParam.Append(txtDesc.Text)
                StrTxParam.Append("|")
                StrTxParam.Append(ddlReceiveFrom.SelectedItem.Value)
                StrTxParam.Append("|")
                StrTxParam.Append(lstJrnType.SelectedItem.Value)
                StrTxParam.Append("|")
                StrTxParam.Append(txtRefNo.Text)
                StrTxParam.Append("|")
                StrTxParam.Append(CheckDate())
                StrTxParam.Append("|")
                StrTxParam.Append(txtAmt.Text)
                StrTxParam.Append("|")
                StrTxParam.Append(0)
                StrTxParam.Append("|")
                StrTxParam.Append(ddlAccMonth.SelectedItem.Value)
                StrTxParam.Append("|")
                StrTxParam.Append(ddlAccYear.SelectedItem.Value)
                StrTxParam.Append("|")
                StrTxParam.Append(strLocation)
                StrTxParam.Append("|||")
                StrTxParam.Append(strNewIDFormat)

                intErrNo = objGLTrx.mtdUpdJournalAdjDetail(strOpCd_Jrn_Add, _
                                                        strOpCd_Jrn_Upd, _
                                                        strOpCd_JrnLn_Get, _
                                                        strUserId, _
                                                        StrTxParam.ToString, _
                                                        objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.GLTrxJournalAdj), _
                                                        ErrorChk, _
                                                        TxID)
                lblJrnAdjId.Text = TxID
                If ErrorChk = objGLTrx.EnumGeneralLedgerTxErrorType.OverFlow Then
                    lblError.Visible = True
                End If

            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_TRX_JOURNALADJ_ADD&errmesg=" & Exp.ToString() & "&redirect=GL/Trx/GL_Trx_JournalAdj_List.aspx")
                End If
            End Try

        End If

        If lblTxLnID.Text = "" Then
            Try
                strTxLnParam.Append(lblJrnAdjId.Text)
                strTxLnParam.Append("|")
                strTxLnParam.Append(txtDescLn.Text)
                strTxLnParam.Append("|")
                strTxLnParam.Append(dblQty)
                strTxLnParam.Append("|")
                strTxLnParam.Append(dblPrice)
                strTxLnParam.Append("|")
                strTxLnParam.Append(dblTotal)
                strTxLnParam.Append("|")
                strTxLnParam.Append(Request.Form("lstAccCode"))
                strTxLnParam.Append("|")
                strTxLnParam.Append(Request.Form("lstBlock"))
                intErrNo = objGLTrx.mtdAddJournalAdjLn(strCompany, _
                                                    strLocation, _
                                                    strAccMonth, _
                                                    strAccYear, _
                                                    strUserId, _
                                                    strOpCd_Add_JrnLine, _
                                                    objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.GLTrxJournalAdjLn), _
                                                    strTxLnParam.ToString, _
                                                    ErrorChk)
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_TRX_JOURNALADJ_LINE_ADD&errmesg=" & Exp.ToString() & "&redirect=GL/Trx/GL_Trx_JournalAdj_List.aspx")
                End If
            End Try
        Else
            Try
                strTxLnParam.Append(strLocation)
                strTxLnParam.Append("|")
                strTxLnParam.Append(lblTxLnID.Text)
                strTxLnParam.Append("|")
                strTxLnParam.Append(txtDescLn.Text)
                strTxLnParam.Append("|")
                strTxLnParam.Append(dblQty)
                strTxLnParam.Append("|")
                strTxLnParam.Append(dblPrice)
                strTxLnParam.Append("|")
                strTxLnParam.Append(dblTotal)
                strTxLnParam.Append("|")
                strTxLnParam.Append(Request.Form("lstAccCode"))
                strTxLnParam.Append("|")
                strTxLnParam.Append(Request.Form("lstBlock"))
                intErrNo = objGLTrx.mtdUpdJournalAdjLineDetail(strCompany, _
                                                               strLocation, _
                                                               strAccMonth, _
                                                               strAccYear, _
                                                               strUserId, _
                                                               strOpCd_JrnLn_Upd, _
                                                               strTxLnParam.ToString, _
                                                               ErrorChk)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_TRX_JOURNALADJ_JRNLINE_UPD&errmesg=" & Exp.ToString() & "&redirect=GL/Trx/GL_Trx_JournalAdj_List.aspx")
            End Try
            lblTxLnID.Text = ""
            dgResult.EditItemIndex = -1
        End If

        If ErrorChk = objGLTrx.EnumGeneralLedgerTxErrorType.OverFlow Then
                lblError.Visible = True
        End If

        StrTxParam.Remove(0, StrTxParam.Length)
        StrTxParam.Append(lblJrnAdjId.Text)
        StrTxParam.Append("||||||||||")
        StrTxParam.Append(strLocation)
        StrTxParam.Append("|||")

        Try
            intErrNo = objGLTrx.mtdUpdJournalAdjDetail(strOpCd_Jrn_Add, _
                                                    strOpCd_Jrn_Upd, _
                                                    strOpCd_JrnLn_Get, _
                                                    strUserId, _
                                                    StrTxParam.ToString, _
                                                    objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.GLTrxJournalAdj), _
                                                    ErrorChk, _
                                                    TxID)
            If ErrorChk = objGLTrx.EnumGeneralLedgerTxErrorType.OverFlow Then
                lblError.Visible = True
            End If

            lblJrnAdjId.Text = TxID
        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_TRX_JOURNALADJ_LISTDATE_GET&errmesg=" & Exp.ToString() & "&redirect=GL/Trx/GL_Trx_JournalAdj_List.aspx")
            End If
        End Try
        Initialize()
        LoadJournalTrx()
        DisplayFromDB()
        BindGrid()
        PageControl()
        txtPrice.Text = ""
        txtQty.Text = ""
        txtPriceCR.Text = ""
        txtQtyCR.Text = ""
        txtDRTotalAmount.Text = ""
        txtCRTotalAmount.Text = ""
        'txtDescLn.Text = ""
        txtDRTotalAmount.Text = ""
        txtCRTotalAmount.Text = ""
        blnShortCut.Text = False

        If Not StrTxParam Is Nothing Then
            StrTxParam = Nothing
        End If

        If Not strTxLnParam Is Nothing Then
            strTxLnParam = Nothing
        End If

        If lblTxLnID.Text <> "" Then
            btnAdd.Visible = False
            btnUpdate.Visible = True
        Else
            btnAdd.Visible = True
            btnUpdate.Visible = False
        End If

    End Sub

    Sub btnSave_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim TxID As String
        Dim IssType As String
        Dim ErrorChk As Integer = objGLTrx.EnumGeneralLedgerTxErrorType.NoError
        Dim StrTxParam As New StringBuilder()
        Dim strNewIDFormat As String
        Dim strDate As String = CheckDate()

        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        If Session("SS_FILTERPERIOD") = "0" Then
            If intCurPeriod < intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
        Else
            If intSelPeriod <> intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
            If intSelPeriod < intCurPeriod And intLevel < 2 Then
                lblDate.Visible = True
                lblDate.Text = "This period already locked."
                Exit Sub
            End If
        End If

        If lblJrnAdjId.Text = "" Then
            Exit Sub
        End If

        If CheckRequiredField() Then
            Exit Sub
        End If

        strNewIDFormat = "MMA" & "/" & strCompany & "/" & strLocation & "/" & Trim(lstJrnType.SelectedItem.Value) & "/" & Year(strDate) & IIf(Len(Month(strDate) = 1), "0" & Month(strDate), Month(strDate)) & "/"
        strAccYear = Year(strDate)
        strAccMonth = Month(strDate)

        If lblJrnAdjId.Text = "" Then
            StrTxParam.Append(lblJrnAdjId.Text)
            StrTxParam.Append("|")
            StrTxParam.Append(txtDesc.Text)
            StrTxParam.Append("|")
            StrTxParam.Append(ddlReceiveFrom.SelectedItem.Value)
            StrTxParam.Append("|")
            StrTxParam.Append(lstJrnType.SelectedItem.Value)
            StrTxParam.Append("|")
            StrTxParam.Append(txtRefNo.Text)
            StrTxParam.Append("|")
            StrTxParam.Append(CheckDate())
            StrTxParam.Append("|")
            StrTxParam.Append(txtAmt.Text)
            StrTxParam.Append("|")
            StrTxParam.Append(0)
            StrTxParam.Append("|")
            StrTxParam.Append(ddlAccMonth.SelectedItem.Value)
            StrTxParam.Append("|")
            StrTxParam.Append(ddlAccMonth.SelectedItem.Value)
            StrTxParam.Append("|")
            StrTxParam.Append(strLocation)
            StrTxParam.Append("|||")
            StrTxParam.Append(strNewIDFormat)
        Else
            StrTxParam.Append(lblJrnAdjId.Text)
            StrTxParam.Append("|")
            StrTxParam.Append(txtDesc.Text)
            StrTxParam.Append("|")
            StrTxParam.Append(ddlReceiveFrom.SelectedItem.Value)
            StrTxParam.Append("|")
            StrTxParam.Append(lstJrnType.SelectedItem.Value)
            StrTxParam.Append("|")
            StrTxParam.Append(txtRefNo.Text)
            StrTxParam.Append("|")
            StrTxParam.Append(CheckDate())
            StrTxParam.Append("|")
            StrTxParam.Append(txtAmt.Text)
            StrTxParam.Append("||")
            StrTxParam.Append(ddlAccMonth.SelectedItem.Value)
            StrTxParam.Append("|")
            StrTxParam.Append(ddlAccYear.SelectedItem.Value)
            StrTxParam.Append("|")
            StrTxParam.Append(strLocation)
            StrTxParam.Append("|||")
        End If

        Try
            intErrNo = objGLTrx.mtdUpdJournalAdjDetail(strOpCd_Jrn_Add, _
                                                    strOpCd_Jrn_Upd, _
                                                    strOpCd_JrnLn_Get, _
                                                    strUserId, _
                                                    StrTxParam.ToString, _
                                                    objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.GLTrxJournalAdj), _
                                                    ErrorChk, _
                                                    TxID)
            lblJrnAdjId.Text = TxID
            If ErrorChk = objGLTrx.EnumGeneralLedgerTxErrorType.OverFlow Then
                lblError.Visible = True
            End If

        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_TRX_JOURNALADJ_JRNDET_UPD&errmesg=" & Exp.ToString() & "&redirect=GL/Trx/GL_Trx_JournalAdj_List.aspx")
            End If
        End Try

        If Not StrTxParam Is Nothing Then
            StrTxParam = Nothing
        End If

        LoadJournalTrx()
        DisplayFromDB()
        PageControl()
    End Sub


    Sub btnPrint_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim StrTxParam As String
        Dim TxID As String
        Dim strRptName As String = ""
        Dim strDec As String = "2"
        Dim strJrnAdjIDFr As String = lblJrnAdjId.Text
        Dim strJrnAdjIDTo As String = lblJrnAdjId.Text
        Dim strStatus As String = "%"
        Dim ErrorChk As Integer = objGLTrx.EnumGeneralLedgerTxErrorType.NoError

        Response.Write("<Script Language=""JavaScript"">window.open(""../../reports/GL_StdRpt_JournalAdjVoucherPreview.aspx?Type=Print&CompName=" & strCompanyName & "&Location=" & strLocation & "&RptID=rptgl1000020" & _
                    "&AccMonth=" & ddlAccMonth.SelectedItem.Value & "&AccYear=" & ddlAccYear.SelectedItem.Value & "&AccTag=" & rptAccTag & "&BlkTag=" & rptBlkTag & _
                    "&RptName=" & strRptName & "&Decimal=" & strDec & "&lblLocation=" & rptLocation & "&JournalAdjIDFrom=" & strJrnAdjIDFr & "&JournalAdjTo=" & strJrnAdjIDTo & _
                    "&Status=" & strStatus & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

        If lblPrintDate.Text = "" Then
            Try
                StrTxParam = lblJrnAdjId.Text & "||||||||||" & strLocation & "|" & Convert.ToString(Now()) & "||"
                intErrNo = objGLTrx.mtdUpdJournalAdjDetail(strOpCd_Jrn_Add, _
                                                        strOpCd_Jrn_Upd, _
                                                        strOpCd_JrnLn_Get, _
                                                        strUserId, _
                                                        StrTxParam.ToString, _
                                                        objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.GLTrxJournalAdj), _
                                                        ErrorChk, _
                                                        TxID)
                lblJrnAdjId.Text = TxID
                If ErrorChk = objGLTrx.EnumGeneralLedgerTxErrorType.OverFlow Then
                    lblError.Visible = True
                End If

            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_TRX_JOURNALADJ_JRNDET2_UPD&errmesg=" & Exp.ToString() & "&redirect=GL/Trx/GL_Trx_JournalAdj_List.aspx")
                End If
            End Try
        Else
            lblReprint.Visible = False
        End If

        LoadJournalTrx()
        DisplayFromDB()
        PageControl()
    End Sub

    Sub btnDelete_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim StrTxParam As String
        Dim TxID As String
        Dim ErrorChk As Integer = objGLTrx.EnumGeneralLedgerTxErrorType.NoError
        Dim strDate As String = CheckDate()

        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        If Session("SS_FILTERPERIOD") = "0" Then
            If intCurPeriod < intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
        Else
            If intSelPeriod <> intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
            If intSelPeriod < intCurPeriod And intLevel < 2 Then
                lblDate.Visible = True
                lblDate.Text = "This period already locked."
                Exit Sub
            End If
        End If

        StrTxParam = lblJrnAdjId.Text & "||||||||||" & strLocation & "||" & objGLTrx.EnumJournalAdjStatus.Deleted & "|"

        If intErrNo = 0 And ErrorChk = objGLTrx.EnumGeneralLedgerTxErrorType.NoError Then
            Try
                intErrNo = objGLTrx.mtdUpdJournalAdjDetail(strOpCd_Jrn_Add, _
                                                        strOpCd_Jrn_Upd, _
                                                        strOpCd_JrnLn_Get, _
                                                        strUserId, _
                                                        StrTxParam.ToString, _
                                                        objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.GLTrxJournalAdj), _
                                                        ErrorChk, _
                                                        TxID)
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_TRX_JOURNALADJ_DELETE&errmesg=" & Exp.ToString() & "&redirect=GL/Trx/GL_Trx_JournalAdj_List.aspx")
                End If
            End Try
        End If

        LoadJournalTrx()
        DisplayFromDB()
        BindGrid()
        PageControl()
    End Sub

    Sub btnPost_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim StrTxParam As String
        Dim TxID As String
        Dim ErrorChk As Integer = objGLTrx.EnumGeneralLedgerTxErrorType.NoError
        Dim strDate As String = CheckDate()

        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        If Session("SS_FILTERPERIOD") = "0" Then
            If intCurPeriod < intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
        Else
            If intSelPeriod <> intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
            If intSelPeriod < intCurPeriod And intLevel < 2 Then
                lblDate.Visible = True
                lblDate.Text = "This period already locked."
                Exit Sub
            End If
        End If

        StrTxParam = lblJrnAdjId.Text & "||||||||||" & strLocation & "||" & objGLTrx.EnumJournalAdjStatus.Posted & "|"

        If intErrNo = 0 And ErrorChk = objGLTrx.EnumGeneralLedgerTxErrorType.NoError Then
            Try
                intErrNo = objGLTrx.mtdUpdJournalAdjDetail(strOpCd_Jrn_Add, _
                                                        strOpCd_Jrn_Upd, _
                                                        strOpCd_JrnLn_Get, _
                                                        strUserId, _
                                                        StrTxParam.ToString, _
                                                        objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.GLTrxJournalAdj), _
                                                        ErrorChk, _
                                                        TxID)
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_TRX_JOURNALADJ_POST&errmesg=" & Exp.ToString() & "&redirect=GL/Trx/GL_Trx_JournalAdj_List.aspx")
                End If
            End Try
        End If

        LoadJournalTrx()
        DisplayFromDB()
        BindGrid()
        PageControl()
    End Sub

    Sub btnBack_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("GL_Trx_JournalAdj_List.aspx")
    End Sub

    Sub btnNew_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("GL_Trx_JournalAdj_Details.aspx")
    End Sub

End Class
