
Imports System
Imports System.Data
Imports System.Math
Imports System.Collections 
Imports System.Data.SqlClient
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports System.Web.UI.HtmlControls
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic
'Imports Infragistics.WebUI.WebCombo

Public Class IN_PurReqDetails : Inherits Page

    Protected WithEvents dgPRLn As DataGrid

    Protected WithEvents txtItemCode As TextBox
    Protected WithEvents TxtItemName As TextBox
    Protected WithEvents dsForDropDown As DataSet
    Protected WithEvents hidPQID As HtmlInputHidden
    Protected WithEvents PRLnTable As HtmlTable
    Protected WithEvents tblLine As HtmlTable

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents SortExpression As Label

    Protected WithEvents QtyReq As TextBox
    Protected WithEvents UnitCost As TextBox
    Protected WithEvents txtRemarks As TextBox

    Protected WithEvents Status As Label
    Protected WithEvents CreateDate As Label
    Protected WithEvents UpdateDate As Label
    Protected WithEvents UpdateBy As Label
    Protected WithEvents lblPurReqID As Label
    Protected WithEvents lblAccPeriod As Label
    Protected WithEvents lblTotAmtFig As Label
    Protected WithEvents lblTotAmtFigDisplay As Label
    Protected WithEvents lblQtyApp As Label
    Protected WithEvents lblQtyRcv As Label
    Protected WithEvents lblQtyOutstanding As Label
    Protected WithEvents lblUnitCost As Label
    Protected WithEvents lblAmount As Label
    Protected WithEvents lblQtyAppDisplay As Label
    Protected WithEvents lblQtyRcvDisplay As Label
    Protected WithEvents lblQtyOutstandingDisplay As Label
    Protected WithEvents lblUnitCostDisplay As Label
    Protected WithEvents lblAmountDisplay As Label
    Protected WithEvents hidStatus As Label
    Protected WithEvents PRType As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblPrintDate As Label

    Protected WithEvents Undelete As ImageButton
    Protected WithEvents PRDelete As ImageButton
    Protected WithEvents Save As ImageButton
    Protected WithEvents Confirm As ImageButton
    Protected WithEvents Cancel As ImageButton
    Protected WithEvents btnAdd As ImageButton
    Protected WithEvents Print As ImageButton
    Protected WithEvents Back As ImageButton
    Protected WithEvents FindIN As HtmlInputButton
    Protected WithEvents FindWS As HtmlInputButton
    Protected WithEvents FindDC As HtmlInputButton
    Protected WithEvents FindFA As HtmlInputButton
    Protected WithEvents FindNU As HtmlInputButton

    Protected WithEvents ddlDeptCode As DropDownList
    Protected WithEvents ddlPRLevel As DropDownList
    Protected WithEvents lblErrDeptCode As Label
    Protected WithEvents lblErrPRLevel As Label
    Protected WithEvents txtAddNote As HtmlTextArea
    Protected WithEvents lstStatusLn As DropDownList
    Protected WithEvents ddlStatusLn As DropDownList
    Protected WithEvents lblStatusDescln As Label

    Protected WithEvents txtDate As TextBox
    Protected WithEvents lblFmt As Label
    Protected WithEvents lblDate As Label

    Protected WithEvents chkCentralized As CheckBox
    Protected WithEvents lblErrGR As Label
    Protected WithEvents btnAddendum As ImageButton

    Dim strDateFMT As String

    Protected objIN As New agri.IN.clsTrx()
    Protected objINSetup As New agri.IN.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl
    Protected objAdmin As New agri.Admin.clsShare()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLocCodeDs As New Object()

    Protected WithEvents lblErrMesage As Label
    Protected objHR As New agri.HR.clsSetup()
    Dim objSysComp As New agri.Admin.clsComp()
    Dim objGLtrx As New agri.GL.ClsTrx()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()

    Dim strOppCd_GET_PRList As String = "IN_CLSTRX_PURREQ_LIST_GET"
    Dim strOppCd_GET_PRLnList As String = "IN_CLSTRX_PURREQLN_LIST_GET"
    Dim strOppCd_UpdPQ As String = "IN_CLSTRX_PURREQ_LIST_UPD"
    Dim strOppCd_DetUpdPQ As String = "IN_CLSTRX_PURREQ_DETAIL_UPD"

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intINAR As Integer
    Dim strAccMonth As String
    Dim strAccYear As String

    Dim strPhyYear As String
    Dim strPhyMonth As String
    Dim strLastPhyYear As String
    Dim intLevel As Integer

    Const ITEM_PART_SEPERATOR As String = " @ "

    Dim objDataSet As New Object()
    Dim intCnt As Integer
    Dim intErrNo As Integer
    Dim strPRID As String
    Dim strPRQType As String
    Dim dsStkDCItem As DataSet
    Dim pv_strItemCode As String
    Dim intPRLnCount As Integer
    Dim strLocLevel As String
    Dim strLocType As String
    Dim strSelAccMonth As String
    Dim strSelAccYear As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        Dim prqtype As String

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intINAR = Session("SS_INAR")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        strPhyYear = Session("SS_PHYYEAR")
        strPhyMonth = Session("SS_PHYMONTH")
        strPhyMonth = strAccMonth
        strLastPhyYear = Session("SS_LASTPHYYEAR")
        strLocLevel = Session("SS_LOCLEVEL")
        strLocType = Session("SS_LOCTYPE")
        intLevel = Session("SS_USRLEVEL")
        strDateFMT = Session("SS_DATEFMT")
        strSelAccMonth = Session("SS_SELACCMONTH")
        strSelAccYear = Session("SS_SELACCYEAR")


        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INPurchaseRequest), intINAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.Text = "" Then
                SortExpression.Text = "ItemCode"
            End If

            lblErrGR.Visible = False
            btnAddendum.Visible = False

            'to avoid double click, on aspx add this : UseSubmitBehavior="false"
            btnAdd.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnAdd).ToString())
            Save.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Save).ToString())
            Confirm.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Confirm).ToString())
            Cancel.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Cancel).ToString())
            Print.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Print).ToString())
            PRDelete.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(PRDelete).ToString())
            Undelete.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Undelete).ToString())
            Back.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Back).ToString())
            btnAddendum.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnAddendum).ToString())

            strPRID = IIf(Request.QueryString("prid") = "", Request.Form("hidPRID"), Request.QueryString("prid"))

            If Not IsPostBack Then
                If lblPurReqID.Text = "" Then
                    If strPRID <> "" Then
                        hidPQID.Value = strPRID
                    End If

                    If strPRID <> "" Then
                        onProcess_Load(strPRID)
                    End If
                End If

                If strPRID <> "" Then

                    onProcess_Load(strPRID)
                    'BindStkDCList(strPRID)

                Else
                    txtDate.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Now)
                    PRType.Text = "1" 'Request.QueryString("prqtype")
                    Print.Visible = False
                    Undelete.Visible = False
                    PRDelete.Visible = False
                    Confirm.Visible = False
                    PRLnTable.Visible = False
                    chkCentralized.Enabled = False
                    BindDeptCode("")
                    BindPRLevel("")
                End If
                txtItemCode.Attributes.Add("readonly", "readonly")
                TxtItemName.Attributes.Add("readonly", "readonly")
                'BindStkDCList(strPRID)
                'BindStatusLn("")
            End If
            CheckType()
        End If
    End Sub

    Sub onProcess_Load(ByVal pv_strPRID As String)
        onLoad_DisplayPR(pv_strPRID)
        onLoad_DisplayPRLn(pv_strPRID)
    End Sub

    Sub CheckType()
        If PRType.Text <> "" Then

            Select Case Status.Text
                Case objIN.mtdGetPurReqStatus(objIN.EnumPurReqStatus.Active)
                    Select Case PRType.Text
                        Case objIN.EnumPurReqDocType.StockPR
                            FindIN.Visible = True
                            FindDC.Visible = False
                            FindWS.Visible = False
                            FindNU.Visible = False
                        Case objIN.EnumPurReqDocType.DirectChargePR
                            FindIN.Visible = False
                            FindDC.Visible = True
                            FindWS.Visible = False
                            FindNU.Visible = False
                        Case objIN.EnumPurReqDocType.WorkshopPR
                            FindIN.Visible = False
                            FindDC.Visible = False
                            FindWS.Visible = True
                            FindNU.Visible = False
                        Case objIN.EnumPurReqDocType.NurseryPR
                            FindIN.Visible = False
                            FindDC.Visible = False
                            FindWS.Visible = False
                            FindNU.Visible = True
                    End Select
                Case objIN.mtdGetPurReqStatus(objIN.EnumPurReqStatus.Confirmed), _
                     objIN.mtdGetPurReqStatus(objIN.EnumPurReqStatus.Deleted), _
                     objIN.mtdGetPurReqStatus(objIN.EnumPurReqStatus.Cancelled), _
                     objIN.mtdGetPurReqStatus(objIN.EnumPurReqStatus.Fulfilled)
                    FindIN.Visible = False
                    FindDC.Visible = False
                    FindWS.Visible = False
                    FindNU.Visible = False
                Case Else
                    Select Case PRType.Text
                        Case objIN.EnumPurReqDocType.StockPR
                            FindIN.Visible = True
                            FindDC.Visible = False
                            FindWS.Visible = False
                            FindNU.Visible = False

                        Case objIN.EnumPurReqDocType.DirectChargePR
                            FindIN.Visible = False
                            FindDC.Visible = True
                            FindWS.Visible = False
                            FindNU.Visible = False

                        Case objIN.EnumPurReqDocType.WorkshopPR
                            FindIN.Visible = False
                            FindDC.Visible = False
                            FindWS.Visible = True
                            FindNU.Visible = False

                        Case objIN.EnumPurReqDocType.NurseryPR
                            FindIN.Visible = False
                            FindDC.Visible = False
                            FindWS.Visible = False
                            FindNU.Visible = True
                    End Select
            End Select
        End If
    End Sub

    Sub onLoad_DisplayPR(ByVal pv_strPRID As String)
        Dim objPRDs As New Data.DataSet()
        Dim strParam As String
        Dim TempStatus As String
        Dim prqtype As String
        Dim TotAmtFigTemp As Decimal
        Dim strResultLocCode As String = strLocation

        PRLnTable.Visible = True

        strParam = "And PR.PRID = '" & pv_strPRID & "' AND PR.PRID *= PRLN.PRID" & "|" & " "
        'strParam = "And PR.PRID = '" & pv_strPRID & "' AND PR.AccMonth = '" & strAccMonth & "' AND PR.AccYear = '" & strAccYear & "'" & "|" & " "


        Try
            intErrNo = objIN.mtdGetPurchaseRequest(strOppCd_GET_PRList, _
                                                   strParam, _
                                                   objIN.EnumPurReqDocType.StockPR, _
                                                   strAccMonth, _
                                                   strAccYear, _
                                                   Trim(strLocation), _
                                                   objPRDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PURREQ_LIST_GET_DISPLAYPR&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        End Try

        If objPRDs.Tables(0).Rows.Count > 0 Then
            lblPurReqID.Text = pv_strPRID
            lblAccPeriod.Text = objPRDs.Tables(0).Rows(0).Item("AccMonth") & "/" & objPRDs.Tables(0).Rows(0).Item("AccYear")
            Status.Text = objIN.mtdGetPurReqStatus(Trim(objPRDs.Tables(0).Rows(0).Item("Status")))
            lblStatus.Text = objPRDs.Tables(0).Rows(0).Item("Status")
            lblPrintDate.Text = objGlobal.GetLongDate(Trim(objPRDs.Tables(0).Rows(0).Item("PrintDate")))
            CreateDate.Text = objGlobal.GetLongDate(Trim(objPRDs.Tables(0).Rows(0).Item("CreateDate")))
            txtDate.Text = objGlobal.GetShortDate(strDateFMT, Trim(objPRDs.Tables(0).Rows(0).Item("CreateDate")))
            UpdateDate.Text = objGlobal.GetLongDate(Trim(objPRDs.Tables(0).Rows(0).Item("UpdateDate")))
            UpdateBy.Text = Trim(objPRDs.Tables(0).Rows(0).Item("UserName"))
            TotAmtFigTemp = Trim(objPRDs.Tables(0).Rows(0).Item("TotalAmount"))
            PRType.Text = Trim(objPRDs.Tables(0).Rows(0).Item("PRType"))
            'lblTotAmtFigDisplay.Text = objGlobal.GetIDDecimalSeparator(Round(TotAmtFigTemp, 2))
            lblTotAmtFigDisplay.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(objPRDs.Tables(0).Rows(0).Item("TotalAmount"), 2), 2)
            lblTotAmtFig.Text = FormatNumber(TotAmtFigTemp, 2)
            txtRemarks.Text = Trim(objPRDs.Tables(0).Rows(0).Item("Remark"))
            BindDeptCode(Trim(objPRDs.Tables(0).Rows(0).Item("DeptCode")))
            BindPRLevel(Trim(objPRDs.Tables(0).Rows(0).Item("LocLevel")))

            If Trim(objPRDs.Tables(0).Rows(0).Item("Centralized")) = "1" Then
                chkCentralized.Checked = True
                chkCentralized.Text = "  Yes"
            Else
                chkCentralized.Checked = False
                chkCentralized.Text = "  No"
            End If
        End If

        If Status.Text = objIN.mtdGetPurReqStatus(objIN.EnumPurReqStatus.Confirmed) Then
            tblLine.Visible = False
            Save.Visible = False
            Confirm.Visible = False
            Undelete.Visible = False
            PRDelete.Visible = False
            Cancel.Visible = False
            Print.Visible = True
            ddlDeptCode.Enabled = False
            ddlPRLevel.Enabled = False
            txtRemarks.Enabled = False
            txtDate.Enabled = False
            btnAddendum.Visible = True
            chkCentralized.Enabled = False

        ElseIf Status.Text = objIN.mtdGetPurReqStatus(objIN.EnumPurReqStatus.Active) Then
            tblLine.Visible = True
            Save.Visible = True
            If intLevel = 0 Then
                Confirm.Visible = False
            Else
                Confirm.Visible = True
            End If

            PRDelete.Visible = True
            PRDelete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Undelete.Visible = False
            Cancel.Visible = False
            Print.Visible = True
            ddlDeptCode.Enabled = True
            ddlPRLevel.Enabled = True
            txtRemarks.Enabled = True
            btnAddendum.Visible = False
            chkCentralized.Enabled = False

        ElseIf Status.Text = objIN.mtdGetPurReqStatus(objIN.EnumPurReqStatus.Deleted) Then
            tblLine.Visible = False
            Save.Visible = False
            Confirm.Visible = False
            PRDelete.Visible = False
            Undelete.Visible = True
            Cancel.Visible = False
            Print.Visible = False
            ddlDeptCode.Enabled = False
            ddlPRLevel.Enabled = False
            txtRemarks.Enabled = False
            txtDate.Enabled = False
            btnAddendum.Visible = False
            chkCentralized.Enabled = False

        ElseIf Status.Text = objIN.mtdGetPurReqStatus(objIN.EnumPurReqStatus.Cancelled) Or _
                 Status.Text = objIN.mtdGetPurReqStatus(objIN.EnumPurReqStatus.Fulfilled) Then
            tblLine.Visible = False
            Save.Visible = False
            Confirm.Visible = False
            PRDelete.Visible = False
            Undelete.Visible = False
            Cancel.Visible = False
            Print.Visible = False
            ddlDeptCode.Enabled = False
            ddlPRLevel.Enabled = False
            txtRemarks.Enabled = False
            txtDate.Enabled = False
            btnAddendum.Visible = False
            chkCentralized.Enabled = False
        Else
            Print.Visible = False
        End If
    End Sub

    Sub onLoad_DisplayPRLn(ByVal pv_strPRID As String)
        Dim UpdButton As LinkButton
        Dim DelButton As LinkButton
        Dim EdtButton As LinkButton
        Dim CanButton As LinkButton
        Dim lblStatus As Label
        Dim lblQtyAppText As Label
        Dim lblQtyRcvText As Label
        Dim lblQtyOutText As Label
        Dim strhidStatus As String
        Dim strQtyApp As String
        Dim strQtyRcv As String
        Dim strQtyOut As String
        Dim lblPOQtyText As Label
        Dim lblPODateText As Label
        Dim lblhidApprovedBy As Label
        Dim strApprovedBy As String

        dgPRLn.DataSource = LoadPRData(pv_strPRID)
        dgPRLn.DataBind()

        If objDataSet.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To intPRLnCount - 1
                lblStatus = dgPRLn.Items.Item(intCnt).FindControl("hidStatus")
                strhidStatus = lblStatus.Text
                lblQtyAppText = dgPRLn.Items.Item(intCnt).FindControl("lblQtyApp")
                strQtyApp = lblQtyAppText.Text
                lblQtyRcvText = dgPRLn.Items.Item(intCnt).FindControl("lblQtyRcv")
                strQtyRcv = lblQtyRcvText.Text
                lblQtyOutText = dgPRLn.Items.Item(intCnt).FindControl("lblQtyOutstanding")
                strQtyOut = lblQtyOutText.Text

                lblPOQtyText = dgPRLn.Items.Item(intCnt).FindControl("lblQtyOrderLast")
                lblPODateText = dgPRLn.Items.Item(intCnt).FindControl("lblPODateDisplay")
                If lblPOQtyText.Text = 0 Then
                    lblPODateText.Visible = False
                Else
                    lblPODateText.Visible = True
                End If

                lblhidApprovedBy = dgPRLn.Items.Item(intCnt).FindControl("lblhidApprovedBy")
                strApprovedBy = lblhidApprovedBy.Text

                EdtButton = dgPRLn.Items.Item(intCnt).FindControl("Edit")
                DelButton = dgPRLn.Items.Item(intCnt).FindControl("Delete")
                UpdButton = dgPRLn.Items.Item(intCnt).FindControl("Update")
                CanButton = dgPRLn.Items.Item(intCnt).FindControl("Cancel")

                If Status.Text = objIN.mtdGetPurReqStatus(objIN.EnumPurReqStatus.Active) Then
                    Select Case intLevel
                        Case 0
                            EdtButton.Visible = False
                            If strhidStatus = objIN.mtdGetPurReqLnStatus(objIN.EnumPurReqLnStatus.Active) Then
                                DelButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                                DelButton.Visible = True
                            Else
                                DelButton.Visible = False
                            End If

                        Case 1
                            EdtButton.Visible = True
                            If Right(Trim(lblPurReqID.Text), 4) = "-ADD" Then
                                If strhidStatus = objIN.mtdGetPurReqLnStatus(objIN.EnumPurReqLnStatus.Cancel) Then
                                    DelButton.Visible = False
                                Else
                                    DelButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                                    DelButton.Visible = True
                                End If
                            Else
                                DelButton.Visible = False
                            End If

                        Case Else
                            EdtButton.Visible = False
                            DelButton.Visible = False
                    End Select

                    UpdButton.Visible = False
                    CanButton.Visible = False

                ElseIf Status.Text = objIN.mtdGetPurReqStatus(objIN.EnumPurReqStatus.Confirmed) Then
                    Select Case CInt(intLevel) - CInt(strApprovedBy)
                        Case 1
                            If strApprovedBy < intLevel Then
                                EdtButton.Visible = True
                                DelButton.Visible = False
                            Else
                                EdtButton.Visible = False
                                DelButton.Visible = False
                            End If

                            '--diremark krn cancel via addendum
                            'Case 0
                            'If strhidStatus = objIN.mtdGetPurReqLnStatus(objIN.EnumPurReqLnStatus.Approved) Then
                            '    EdtButton.Visible = False
                            '    DelButton.Visible = False
                            'Else
                            '    EdtButton.Visible = True
                            '    DelButton.Visible = False
                            'End If

                        Case Else
                            EdtButton.Visible = False
                            DelButton.Visible = False
                    End Select

                    UpdButton.Visible = False
                    CanButton.Visible = False

                    'ElseIf Status.Text = objIN.mtdGetPurReqStatus(objIN.EnumPurReqStatus.Active) And strQtyRcv <> 0 Then
                    '    UpdButton = dgPRLn.Items.Item(intCnt).FindControl("Cancel")
                    '    UpdButton.Visible = False
                    '    UpdButton = dgPRLn.Items.Item(intCnt).FindControl("Delete")
                    '    UpdButton.Visible = False
                    'ElseIf Status.Text = objIN.mtdGetPurReqStatus(objIN.EnumPurReqStatus.Confirmed) And _
                    '    strhidStatus = objIN.mtdGetPurReqStatus(objIN.EnumPurReqStatus.Active) And _
                    '    strQtyReq <> strQtyRcv And strQtyOut <> 0 Then
                    '    UpdButton = dgPRLn.Items.Item(intCnt).FindControl("Cancel")
                    '    UpdButton.Visible = False
                    '    UpdButton = dgPRLn.Items.Item(intCnt).FindControl("Delete")
                    '    UpdButton.Visible = False
                    'ElseIf Status.Text = objIN.mtdGetPurReqStatus(objIN.EnumPurReqStatus.Confirmed) And _
                    '    strhidStatus = objIN.mtdGetPurReqStatus(objIN.EnumPurReqStatus.Active) And _
                    '    strQtyReq = strQtyRcv And strQtyOut = 0 Then
                    '    UpdButton = dgPRLn.Items.Item(intCnt).FindControl("Cancel")
                    '    UpdButton.Visible = False
                    '    UpdButton = dgPRLn.Items.Item(intCnt).FindControl("Delete")
                    '    UpdButton.Visible = False
                    'ElseIf Status.Text = objIN.mtdGetPurReqStatus(objIN.EnumPurReqStatus.Confirmed) And _
                    '    strhidStatus = objIN.mtdGetPurReqStatus(objIN.EnumPurReqStatus.Active) And _
                    '    strQtyReq <> strQtyRcv And strQtyOut = 0 Then
                    '    UpdButton = dgPRLn.Items.Item(intCnt).FindControl("Cancel")
                    '    UpdButton.Visible = False
                    '    UpdButton = dgPRLn.Items.Item(intCnt).FindControl("Delete")
                    '    UpdButton.Visible = False
                    'ElseIf Status.Text = objIN.mtdGetPurReqStatus(objIN.EnumPurReqStatus.Confirmed) And _
                    '    strhidStatus = objIN.mtdGetPurReqLnStatus(objIN.EnumPurReqLnStatus.Cancel) Then
                    '    UpdButton = dgPRLn.Items.Item(intCnt).FindControl("Cancel")
                    '    UpdButton.Visible = False
                    '    UpdButton = dgPRLn.Items.Item(intCnt).FindControl("Delete")
                    '    UpdButton.Visible = False
                    'ElseIf Status.Text = objIN.mtdGetPurReqStatus(objIN.EnumPurReqStatus.Cancelled) Or _
                    '    Status.Text = objIN.mtdGetPurReqStatus(objIN.EnumPurReqStatus.Fulfilled) Or _
                    '    Status.Text = objIN.mtdGetPurReqStatus(objIN.EnumPurReqStatus.Deleted) Then
                    '    UpdButton = dgPRLn.Items.Item(intCnt).FindControl("Cancel")
                    '    UpdButton.Visible = False
                    '    UpdButton = dgPRLn.Items.Item(intCnt).FindControl("Delete")
                    '    UpdButton.Visible = False
                    'Else
                    '    UpdButton = dgPRLn.Items.Item(intCnt).FindControl("Delete")
                    '    UpdButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');" '
                End If
            Next intCnt


        Else
            PRLnTable.Visible = False
            Confirm.Visible = False
        End If
    End Sub

    Protected Function LoadPRData(ByVal pv_strPRID As String) As DataSet

        Dim strParam As String = pv_strPRID & "|" & "PRln.PRID"
        Dim edittext As TextBox

        Try
            intErrNo = objIN.mtdGetPRLnList(strOppCd_GET_PRLnList, _
                                            strCompany, _
                                            Trim(strLocation), _
                                            strUserId, _
                                            strAccMonth, _
                                            strAccYear, _
                                            strParam, _
                                            objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PURREQLN_LIST_GET_LOADPRDATA&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        End Try

        For intCnt = 0 To objDataSet.Tables(0).Rows.Count - 1
            objDataSet.Tables(0).Rows(intCnt).Item(0) = Trim(objDataSet.Tables(0).Rows(intCnt).Item(0))
            objDataSet.Tables(0).Rows(intCnt).Item(1) = Trim(objDataSet.Tables(0).Rows(intCnt).Item(1))
            objDataSet.Tables(0).Rows(intCnt).Item(2) = Trim(objDataSet.Tables(0).Rows(intCnt).Item(2))
            objDataSet.Tables(0).Rows(intCnt).Item(3) = Trim(objDataSet.Tables(0).Rows(intCnt).Item(3))
            objDataSet.Tables(0).Rows(intCnt).Item(4) = Trim(objDataSet.Tables(0).Rows(intCnt).Item(4))
            objDataSet.Tables(0).Rows(intCnt).Item(5) = Trim(objDataSet.Tables(0).Rows(intCnt).Item(5))
            objDataSet.Tables(0).Rows(intCnt).Item(6) = Trim(objDataSet.Tables(0).Rows(intCnt).Item(6))
            objDataSet.Tables(0).Rows(intCnt).Item(7) = Trim(objDataSet.Tables(0).Rows(intCnt).Item(7))
        Next intCnt

        intPRLnCount = objDataSet.Tables(0).Rows.Count

        Return objDataSet
    End Function


    'Sub BindStkDCList(ByVal pv_strPRID As String)
    '    Dim strOppCd_Item_GET As String = "IN_CLSTRX_PURREQ_ITEMLIST_WITHPRID_GET"
    '    Dim strOppCd_Item_GET_NOPRID As String = "IN_CLSTRX_PURREQ_ITEMLIST_WITHOUTPRID_GET"
    '    Dim strOppCd_ItemPart_GET As String = "IN_CLSTRX_ITEMPART_LIST_WITHPRID_GET"
    '    Dim strOppCd_ItemPart_NOPRID_GET As String = "IN_CLSTRX_ITEMPART_LIST_WITHOUTPRID_GET"
    '    Dim strItemType As String
    '    Dim strParam As String
    '    Dim dsItemPart As New DataSet()
    '    Dim intCntWS As Integer

    '    If PRType.Text <> "" Then
    '        Select Case PRType.Text
    '            Case objIN.EnumPurReqDocType.StockPR
    '                strItemType = "'" & objINSetup.EnumInventoryItemType.Stock & "','" & objINSetup.EnumInventoryItemType.WorkshopItem & "'"
    '            Case objIN.EnumPurReqDocType.DirectChargePR
    '                strItemType = objINSetup.EnumInventoryItemType.DirectCharge
    '            Case objIN.EnumPurReqDocType.CanteenPR
    '                strItemType = objINSetup.EnumInventoryItemType.CanteenItem
    '            Case objIN.EnumPurReqDocType.WorkshopPR
    '                strItemType = objINSetup.EnumInventoryItemType.WorkshopItem
    '            Case objIN.EnumPurReqDocType.FixedAssetPR
    '                strItemType = objINSetup.EnumInventoryItemType.FixedAssetItem
    '            Case objIN.EnumPurReqDocType.NurseryPR
    '                strItemType = objINSetup.EnumInventoryItemType.NurseryItem
    '        End Select
    '    End If

    '    If PRType.Text <> objIN.EnumPurReqDocType.WorkshopPR Then

    '        strParam = pv_strPRID & "|" & strItemType & "|" & objINSetup.EnumStockItemStatus.Active & "|"
    '        Try
    '            intErrNo = objIN.mtdGetItemList(strOppCd_Item_GET, _
    '                                            strOppCd_Item_GET_NOPRID, _
    '                                            strCompany, _
    '                                            Trim(strLocation), _
    '                                            strUserId, _
    '                                            strAccMonth, _
    '                                            strAccYear, _
    '                                            strParam, _
    '                                            dsForDropDown)
    '        Catch Exp As System.Exception
    '            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_ITEM_LIST_BINDSTKDCLIST&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
    '        End Try


    '        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
    '            dsForDropDown.Tables(0).Rows(intCnt).Item("ItemCode") = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("ItemCode"))
    '            dsForDropDown.Tables(0).Rows(intCnt).Item("Description") = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("ItemCode")) & " ( " & _
    '                                                                        Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("Description")) & " ), " & _
    '                                                                        "Rp. " & objGlobal.GetIDDecimalSeparator(dsForDropDown.Tables(0).Rows(intCnt).Item("AverageCost")) & ", " & _
    '                                                                        objGlobal.GetIDDecimalSeparator_FreeDigit(dsForDropDown.Tables(0).Rows(intCnt).Item("QtyOnHand"), 5) & ", " & _
    '                                                                        Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("UOMCode"))

    '        Next intCnt

    '        Dim drinsert As DataRow
    '        drinsert = dsForDropDown.Tables(0).NewRow()
    '        drinsert(0) = " "
    '        drinsert(1) = "Select Item Code"
    '        dsForDropDown.Tables(0).Rows.InsertAt(drinsert, 0)

    '        lstStkDC.DataSource = dsForDropDown.Tables(0)
    '        lstStkDC.DataValueField = "ItemCode"
    '        lstStkDC.DataTextField = "Description"
    '        lstStkDC.DataBind()

    '    Else

    '        strParam = pv_strPRID & "|" & strItemType & "|" & objINSetup.EnumStockItemStatus.Active & "|" & objINSetup.EnumItemPartStatus.Active
    '        Try
    '            intErrNo = objIN.mtdGetItemList(strOppCd_ItemPart_GET, _
    '                                            strOppCd_ItemPart_NOPRID_GET, _
    '                                            strCompany, _
    '                                            Trim(strLocation), _
    '                                            strUserId, _
    '                                            strAccMonth, _
    '                                            strAccYear, _
    '                                            strParam, _
    '                                            dsItemPart)
    '        Catch Exp As System.Exception
    '            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_PR_DET_ITEMPART_GET&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
    '        End Try

    '        For intCnt = 0 To dsItemPart.Tables(0).Rows.Count - 1
    '            dsItemPart.Tables(0).Rows(intCnt).Item("ItemCode") = Trim(dsItemPart.Tables(0).Rows(intCnt).Item("ItemCode")) & _
    '                                                                 ITEM_PART_SEPERATOR & dsItemPart.Tables(0).Rows(intCnt).Item("PartNo").Trim()
    '        Next intCnt

    '        Dim drinsert As DataRow
    '        drinsert = dsItemPart.Tables(0).NewRow()
    '        drinsert("ItemCode") = " "
    '        drinsert("Description") = "Select Item Code"
    '        dsItemPart.Tables(0).Rows.InsertAt(drinsert, 0)

    '        lstStkDC.DataSource = dsItemPart.Tables(0)
    '        lstStkDC.DataValueField = "ItemCode"
    '        lstStkDC.DataTextField = "Description"
    '        lstStkDC.DataBind()
    '    End If
    'End Sub

    Sub btnAdd_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOppCd_NewPQ As String = "IN_CLSTRX_PURREQ_ADD"
        Dim strOppCd_NewPQLn As String = "IN_CLSTRX_PURREQLN_ADD"
        Dim strOppCd_Item As String = "IN_CLSSETUP_STOCKITEM_DETAILS_GET"
        Dim strOppCd_RDP As String = "ADMIN_CLSLOC_LOCATION_LIST_GET"
        Dim strOppCd_GetID As String = "IN_CLSTRX_PURREQ_GETID"
        Dim strOppCd As String = "IN_CLSTRX_PURREQ_MOVEID"
        Dim strOppCd_Back As String = "IN_CLSTRX_PURREQ_BACKID"
        Dim strItemCode As String = Request.Form("txtItemCode").Trim
        Dim strQtyReq As String = QtyReq.Text.Trim
        Dim strUnitCost As String = IIf(UnitCost.Text.Trim = "", "0", UnitCost.Text.Trim)
        Dim strStatus As String = objIN.EnumPurReqStatus.Active
        Dim strDeptCode As String = ddlDeptCode.SelectedItem.Value
        Dim strNewIDFormat As String
        Dim strNewYear As String = ""
        Dim strTranPrefix As String = "PR"
        Dim strHistYear As String = ""
        Dim strRDP As String
        Dim objCompDs As New Object
        Dim blnIsDetail As Boolean = True
        Dim strRemarks As String
        Dim strPrintDate As String
        Dim objPRID As Object
        Dim objPRLnId As Object
        Dim objPRDs As Object
        Dim strParam As String
        Dim strPRID As String
        Dim arrItem As Array
        Dim strSortExpression As String = "LocCode"
        Dim strAddNote As String = Trim(txtAddNote.Value)
        Dim strPRLevelCode As String
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim indDate As String = ""

        If CheckDate(txtDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

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

        If ddlPRLevel.SelectedItem.Value = "" Then
            lblErrPRLevel.Visible = True
            Exit Sub
        Else
            lblErrPRLevel.Visible = False
        End If

        If strDeptCode = "" Then
            lblErrDeptCode.Visible = True
            Exit Sub
        Else
            lblErrDeptCode.Visible = False
        End If

        If InStr(strItemCode, ITEM_PART_SEPERATOR) <> 0 Then
            strItemCode = Trim(Mid(strItemCode, 1, InStr(strItemCode, ITEM_PART_SEPERATOR) - 1))
        ElseIf InStr(strItemCode, ITEM_PART_SEPERATOR) = 0 Then
            arrItem = Split(strItemCode, " @")
            strItemCode = arrItem(0)
        End If

        strParam = strLocation & "||||" & strSortExpression & "||"

        Try
            intErrNo = objAdminLoc.mtdGetLocCode(strOppCd_RDP, strParam, objLocCodeDs)
        Catch Exp As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADMIN_GET_LOCATION&errmesg=" & lblErrMessage.Text & "&redirect=admin/location/admin_location_locdet.aspx")
        End Try

        If objLocCodeDs.Tables(0).Rows.Count > 0 Then
            strRDP = objLocCodeDs.Tables(0).Rows(0).Item("RDP")
        End If

        If Len(strPhyMonth) = 1 Then
            strPhyMonth = "0" & strPhyMonth
        End If

        strParam = "where phyyear = '" & Right(Trim(strPhyYear), 2) & "' and tran_prefix = 'PR'" & "|"
        Try
            intErrNo = objIN.mtdGetPurchaseRequest(strOppCd_GetID, _
                                                   strParam, _
                                                   objIN.EnumPurReqDocType.StockPR, _
                                                   strAccMonth, _
                                                   strAccYear, _
                                                   Trim(strLocation), _
                                                   objPRDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PURREQ_LIST_GET_DISPLAYPR&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        End Try


        If objPRDs.Tables(0).Rows.Count > 0 Then
            strNewYear = ""
        Else
            strHistYear = Right(strLastPhyYear, 2)
            strNewYear = "1"
        End If

        Select Case ddlPRLevel.SelectedItem.Value
            Case objAdminLoc.EnumLocLevel.HQ
                strPRLevelCode = "M"
            Case objAdminLoc.EnumLocLevel.Perwakilan
                strPRLevelCode = "R"
            Case objAdminLoc.EnumLocLevel.Estate
                strPRLevelCode = "L"
            Case objAdminLoc.EnumLocLevel.Mill
                strPRLevelCode = "L"
        End Select

        'If Month(strDate) < strAccMonth And Year(strDate) <= strAccYear Then
        '    lblDate.Visible = True
        '    lblDate.Text = "Invalid transaction date."
        '    Exit Sub
        'End If

        strAccYear = Year(strDate)
        strAccMonth = Month(strDate)
        'Select Case strCompany
        '    Case "SAM", "MIL"
        '        strNewIDFormat = Left(Trim(strDeptCode), 3) & "/" & Trim(strLocation) & "/" & Right(strAccYear, 2) & "/" & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/" & Trim(strRDP) & "-"
        '    Case Else
        '        strNewIDFormat = "BOR" & "/" & strCompany & "/" & strLocation & "/" & strPRLevelCode & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"
        'End Select
        strNewIDFormat = "PR" & "/" & strCompany & "/" & strLocation & "/" & strPRLevelCode & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"

        If chkCentralized.Checked = True Then
            strStatus = objIN.EnumPurReqLnStatus.Active
        Else
            strStatus = objIN.EnumPurReqLnStatus.Approved
        End If


        If lblPurReqID.Text = "" Then
            strRemarks = Trim(txtRemarks.Text)

            strParam = "|" & strItemCode & "|" & strQtyReq & "|" & strUnitCost & "|" & strRemarks & "|" & strStatus & "|" & strPrintDate & "|" & "PRLn." & SortExpression.Text & "|" & PRType.Text & "|" & _
                        strNewIDFormat & "|" & strNewYear & "|" & strTranPrefix & "|" & strHistYear & "|" & Right(strPhyYear, 2) & "|" & ddlDeptCode.SelectedItem.Value & "|" & ddlPRLevel.SelectedItem.Value & "|" & _
                        strAddNote & "|" & strDate & "|" & IIf(chkCentralized.Checked = True, "1", "0") & "|" & Trim(strLocation)

            Try
                intErrNo = objIN.mtdNewPurchaseRequestLn(strOppCd_NewPQ, _
                                                         strOppCd_NewPQLn, _
                                                         strOppCd_Item, _
                                                         strOppCd_GET_PRLnList, _
                                                         strOppCd_UpdPQ, _
                                                         strOppCd, _
                                                         strOppCd_Back, _
                                                         strParam, _
                                                         strCompany, _
                                                         Trim(strLocation), _
                                                         strUserId, _
                                                         strAccMonth, _
                                                         strAccYear, _
                                                         objPRID, _
                                                         objPRLnId, _
                                                         objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.PurchaseRequest), _
                                                         objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.PurchaseRequestLn))
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=NEW_PURREQLN_WITHOUT_PRID&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
            End Try

            hidPQID.Value = objPRID
            onProcess_Load(hidPQID.Value)
        Else

            strPRID = Trim(lblPurReqID.Text)
            strRemarks = Trim(txtRemarks.Text)
            strParam = strPRID & "|" & strItemCode & "|" & strQtyReq & "|" & strUnitCost & "|" & strRemarks & "|" & strStatus & "|" & strPrintDate & "|" & "PRLn." & SortExpression.Text & "|" & PRType.Text & "|" & _
                       strNewIDFormat & "|" & strNewYear & "|" & strTranPrefix & "|" & strHistYear & "|" & Right(strPhyYear, 2) & "|" & ddlDeptCode.SelectedItem.Value & "|" & ddlPRLevel.SelectedItem.Value & "|" & _
                       strAddNote & "|" & strDate & "|" & IIf(chkCentralized.Checked = True, "1", "0")
            Try
                intErrNo = objIN.mtdNewPurchaseRequestLn(strOppCd_NewPQ, _
                                                         strOppCd_NewPQLn, _
                                                         strOppCd_Item, _
                                                         strOppCd_GET_PRLnList, _
                                                         strOppCd_UpdPQ, _
                                                         strOppCd, _
                                                         strOppCd_Back, _
                                                         strParam, _
                                                         strCompany, _
                                                         Trim(strLocation), _
                                                         strUserId, _
                                                         strAccMonth, _
                                                         strAccYear, _
                                                         objPRID, _
                                                         objPRLnId, _
                                                         objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.PurchaseRequest), _
                                                         objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.PurchaseRequestLn))
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=NEW_PURREQLN_WITH_PRID&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
            End Try

            onProcess_Load(strPRID)
        End If
        'BindStkDCList(objPRID)

        txtItemCode.Text = ""
        TxtItemName.Text = ""
        strNewYear = ""
        QtyReq.Text = ""
        UnitCost.Text = ""
        txtAddNote.Value = ""
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOppCd_PurReqLn_DEL As String = "IN_CLSTRX_PURREQLN_DEL"
        Dim strParam As String
        Dim objPRID As Object
        Dim strPRID As String = lblPurReqID.Text.Trim
        Dim strItemCode As String
        Dim DelText As Label
        Dim strRemarks As String = Request.Form("txtRemarks").Trim
        Dim strPRStatus As String = objIN.EnumPurReqStatus.Active
        Dim strResultLocCode As String = strLocation
        Dim LnID As Label
        Dim strPRLnID As String
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim indDate As String = ""

        If CheckDate(txtDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

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

        dgPRLn.EditItemIndex = CInt(E.Item.ItemIndex)
        DelText = dgPRLn.Items.Item(CInt(E.Item.ItemIndex)).FindControl("ItemCode")
        strItemCode = DelText.Text
        LnID = dgPRLn.Items.Item(CInt(E.Item.ItemIndex)).FindControl("LnID")
        strPRLnID = LnID.Text

        strParam = strPRID & "|" & strItemCode & "|" & strPRStatus & "|" & strRemarks & "|" & "PRLn.ItemCode" & "|" & PRType.Text & "|" & ddlPRLevel.SelectedItem.Value
        strParam = strParam & "|" & strLocation & "|" & strPRLnID

        Try
            intErrNo = objIN.mtdDelPurchaseRequestLn(strOppCd_PurReqLn_DEL, _
                                                     strOppCd_GET_PRLnList, _
                                                     strOppCd_UpdPQ, _
                                                     strParam, _
                                                     strCompany, _
                                                     Trim(strLocation), _
                                                     strUserId, _
                                                     objPRID, _
                                                     objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.PurchaseRequest))
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=DEL_PURREQLN&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        End Try
        onProcess_Load(strPRID)
        'BindStkDCList(strPRID)
    End Sub

    Sub DEDR_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOppCd_PurReqLn_UPD As String = "IN_CLSTRX_PURREQLN_LIST_UPD"
        Dim objPRID As Object
        Dim CancelText As Label
        Dim Updbutton As LinkButton

        'Dim strRemarks As String = Request.Form("txtRemarks").Trim
        Dim strPRID As String = lblPurReqID.Text.Trim
        Dim strParam As String
        Dim strItemCode As String
        Dim strQtyReq As String
        Dim strQtyRcv As String
        Dim strQtyOutstanding As String
        Dim strCost As String
        Dim strStatus As String

        dgPRLn.EditItemIndex = CInt(E.Item.ItemIndex)
        CancelText = dgPRLn.Items.Item(CInt(E.Item.ItemIndex)).FindControl("ItemCode")
        strItemCode = CancelText.Text
        CancelText = dgPRLn.Items.Item(CInt(E.Item.ItemIndex)).FindControl("hidStatus")
        strStatus = CancelText.Text
        CancelText = dgPRLn.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblQtyApp")
        strQtyReq = CancelText.Text
        CancelText = dgPRLn.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblQtyRcv")
        strQtyRcv = CancelText.Text
        CancelText = dgPRLn.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblQtyOutstanding")
        strQtyOutstanding = CancelText.Text
        CancelText = dgPRLn.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblUnitCost")
        strCost = CancelText.Text

        'strParam = strPRID & "|" & strItemCode & "|" & strQtyReq & "|" & strQtyRcv & "|" & strQtyOutstanding & "|" & strCost & "|" & strStatus & "|" & strRemarks & "|" & "PRLn.ItemCode" & "|" & PRType.Text
        'Try
        '    intErrNo = objIN.mtdCancelPurchaseRequestLn(strOppCd_PurReqLn_UPD, _
        '                                                strOppCd_GET_PRLnList, _
        '                                                strOppCd_UpdPQ, _
        '                                                strParam, _
        '                                                strCompany, _
        '                                                TRIM(strLocation), _
        '                                                strUserId, _
        '                                                objPRID, _
        '                                                objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.PurchaseRequest))

        'Catch Exp As System.Exception
        '    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=CANCEL_PURREQLN&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        'End Try
        onProcess_Load(strPRID)
    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOppCd_PurReqLn_DEL As String = "IN_CLSTRX_PURREQLN_DEL"
        Dim strPRID As String = lblPurReqID.Text.Trim
        Dim strStatusLn As String
        Dim EditStatusText As Label
        Dim EditStatus As DropDownList
        Dim EditStatusDescln As Label
        Dim strItemCode As String
        Dim ItemText As Label
        Dim QtyDispText As Label
        Dim EditQtyText As Label
        Dim EditQty As TextBox
        Dim UpdButton As LinkButton
        Dim EditAddNoteText As Label
        Dim EditAddNote As TextBox
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim indDate As String = ""

        If CheckDate(txtDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

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

        dgPRLn.EditItemIndex = CInt(E.Item.ItemIndex)
        ItemText = dgPRLn.Items.Item(CInt(E.Item.ItemIndex)).FindControl("ItemCode")
        strItemCode = ItemText.Text

        QtyDispText = dgPRLn.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblQtyAppDisplay")
        QtyDispText.Visible = False
        EditQtyText = dgPRLn.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblQtyApp")
        EditQty = dgPRLn.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lstQtyApp")
        EditQty.Text = EditQtyText.Text
        EditQty.Visible = True

        EditStatusDescln = dgPRLn.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblStatusDescln")
        EditStatusDescln.Visible = False
        EditStatusText = dgPRLn.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblStatusln")
        strStatusLn = EditStatusText.Text
        EditStatus = dgPRLn.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lstStatusLn")
        EditStatus.Visible = True

        EditAddNoteText = dgPRLn.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblAddNote")
        EditAddNoteText.Visible = False
        EditAddNote = dgPRLn.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lstAddNote")
        EditAddNote.Text = EditAddNoteText.Text
        EditAddNote.Visible = True

        EditStatus.Items.Add(New ListItem(objIN.mtdGetPurReqLnStatus(objIN.EnumPurReqLnStatus.Active), objIN.EnumPurReqLnStatus.Active))
        EditStatus.Items.Add(New ListItem(objIN.mtdGetPurReqLnStatus(objIN.EnumPurReqLnStatus.Cancel), objIN.EnumPurReqLnStatus.Cancel))
        EditStatus.Items.Add(New ListItem(objIN.mtdGetPurReqLnStatus(objIN.EnumPurReqLnStatus.Approved), objIN.EnumPurReqLnStatus.Approved))
        EditStatus.Items.Add(New ListItem(objIN.mtdGetPurReqLnStatus(objIN.EnumPurReqLnStatus.Hold), objIN.EnumPurReqLnStatus.Hold))

        EditStatus.SelectedIndex = EditStatus.Items.IndexOf(EditStatus.Items.FindByValue(Trim(strStatusLn)))

        UpdButton = dgPRLn.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Edit")
        UpdButton.Visible = False
        UpdButton = dgPRLn.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Update")
        UpdButton.Visible = True
        UpdButton = dgPRLn.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Cancel")
        UpdButton.Visible = True
        UpdButton = dgPRLn.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Delete")
        UpdButton.Visible = False

    End Sub

    Sub DEDR_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOpCd As String = "IN_CLSTRX_PURREQLN_UPD"
        Dim strPRID As String = lblPurReqID.Text.Trim
        Dim objItemDs As New Object()
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim strItemCode As String
        Dim strQtyApp As String
        Dim strStatusLn As String
        Dim strAddNote As String
        Dim EditItem As Label
        Dim EditText As TextBox
        Dim EditStatus As DropDownList
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim indDate As String = ""
        Dim sTrPrLnID As String = ""


        If CheckDate(txtDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

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

        EditItem = E.Item.FindControl("ItemCode")
        strItemCode = EditItem.Text
        EditText = E.Item.FindControl("lstQtyApp")
        strQtyApp = EditText.Text
        EditText = E.Item.FindControl("lstAddNote")
        strAddNote = EditText.Text
        EditStatus = E.Item.FindControl("lstStatusLn")
        strStatusLn = EditStatus.SelectedItem.Value

        sTrPrLnID = dgPRLn.Items.Item(E.Item.ItemIndex).Cells(0).Text

        strParamName = "PRID|ITEMCODE|QTYAPP|QTYOUTSTANDING|STATUSLN|ADDITIONALNOTE|APPROVEDBY|PRLNID"

        strParamValue = strPRID & "|" & strItemCode & "|" & strQtyApp & "|" & strQtyApp & "|" & _
                        strStatusLn & "|" & strAddNote & "|" & intLevel & "|" & sTrPrLnID

        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpCd, _
                                                    strParamName, _
                                                    strParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ADD_DETAIL&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        End Try

        onProcess_Load(strPRID)
        'BindStkDCList(strPRID)
    End Sub

    Sub btnSave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOppCd_NewPQ As String = "IN_CLSTRX_PURREQ_ADD"
        Dim strOppCd As String = "IN_CLSTRX_PURREQ_MOVEID"
        Dim strOppCd_Back As String = "IN_CLSTRX_PURREQ_BACKID"
        Dim strRemarks As String = Request.Form("txtRemarks").Trim
        Dim strPrintDate As String
        Dim strPRStatus As String = objIN.EnumPurReqStatus.Active
        Dim objPRID As Object
        Dim strPRID As String = lblPurReqID.Text.Trim
        Dim strParam As String
        Dim strOppCd_RDP As String = "ADMIN_CLSLOC_LOCATION_LIST_GET"
        Dim strOppCd_GetID As String = "IN_CLSTRX_PURREQ_GETID"
        Dim strDeptCode As String = ddlDeptCode.SelectedItem.Value
        Dim strNewIDFormat As String
        Dim strNewYear As String = ""
        Dim strTranPrefix As String = "PR"
        Dim strHistYear As String = ""
        Dim strRDP As String
        Dim objCompDs As New Object
        Dim blnIsDetail As Boolean = True
        Dim objPRDs As Object
        Dim strSortExpression As String = "LocCode"
        Dim strPRLevel As String
        Dim strPRLevelCode As String
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim indDate As String = ""

        If CheckDate(txtDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

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

        If lblPurReqID.Text = "" Then
            Exit Sub
        End If


        Dim arrParam As Array
        arrParam = Split(lblAccPeriod.Text, "/")
        If Month(strDate) <> arrParam(0) Then
            lblDate.Visible = True
            lblDate.Text = "Invalid transaction date."
            Exit Sub
        ElseIf Year(strDate) <> arrParam(1) Then
            lblDate.Visible = True
            lblDate.Text = "Invalid transaction date."
            Exit Sub
        End If

        If ddlPRLevel.SelectedItem.Value = "" Then
            lblErrPRLevel.Visible = True
            Exit Sub
        Else
            lblErrPRLevel.Visible = False
        End If

        If strDeptCode = "" Then
            lblErrDeptCode.Visible = True
            Exit Sub
        Else
            lblErrDeptCode.Visible = False
        End If

        strParam = strLocation & "||||" & strSortExpression & "||"

        Try
            intErrNo = objAdminLoc.mtdGetLocCode(strOppCd_RDP, strParam, objLocCodeDs)
        Catch Exp As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADMIN_GET_LOCATION&errmesg=" & lblErrMessage.Text & "&redirect=admin/location/admin_location_locdet.aspx")
        End Try

        If objLocCodeDs.Tables(0).Rows.Count > 0 Then
            strRDP = objLocCodeDs.Tables(0).Rows(0).Item("RDP")
        End If

        If Len(strPhyMonth) = 1 Then
            strPhyMonth = "0" & strPhyMonth
        End If

        strParam = "where phyyear = '" & Right(Trim(strPhyYear), 2) & "' and tran_prefix = 'PR'" & "|"
        Try
            intErrNo = objIN.mtdGetPurchaseRequest(strOppCd_GetID, _
                                                   strParam, _
                                                   objIN.EnumPurReqDocType.StockPR, _
                                                   strAccMonth, _
                                                   strAccYear, _
                                                   Trim(strLocation), _
                                                   objPRDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PURREQ_LIST_GET_DISPLAYPR&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        End Try


        If objPRDs.Tables(0).Rows.Count > 0 Then
            strNewYear = ""
        Else
            strHistYear = Right(strLastPhyYear, 2)
            strNewYear = "1"
        End If

        Select Case ddlPRLevel.SelectedItem.Value
            Case objAdminLoc.EnumLocLevel.HQ
                strPRLevelCode = "M"
            Case objAdminLoc.EnumLocLevel.Perwakilan
                strPRLevelCode = "R"
            Case objAdminLoc.EnumLocLevel.Estate
                strPRLevelCode = "L"
            Case objAdminLoc.EnumLocLevel.Mill
                strPRLevelCode = "L"
        End Select

        strAccYear = Year(strDate)
        strAccMonth = Month(strDate)
        'Select Case strCompany
        '    Case "SAM", "MIL"
        '        strNewIDFormat = Left(Trim(strDeptCode), 3) & "/" & Trim(strLocation) & "/" & Right(strAccYear, 2) & "/" & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/" & Trim(strRDP) & "-"
        '    Case Else
        '        strNewIDFormat = "BOR" & "/" & strCompany & "/" & strLocation & "/" & strPRLevelCode & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"
        'End Select
        strNewIDFormat = "BOR" & "/" & strCompany & "/" & strLocation & "/" & strPRLevelCode & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"

        If lblPurReqID.Text = "" Then
            strParam = "|" & strRemarks & "|" & lblTotAmtFig.Text & "|" & strPRStatus & "|" & strPrintDate & "|" & PRType.Text & "|" & _
                        strNewIDFormat & "|" & strNewYear & "|" & strTranPrefix & "|" & strHistYear & "|" & Right(strPhyYear, 2) & "|" & _
                        ddlDeptCode.SelectedItem.Value & "|" & ddlPRLevel.SelectedItem.Value & "|" & strDate & "|" & IIf(chkCentralized.Checked = True, "1", "0")

            Try
                intErrNo = objIN.mtdNewPurchaseRequest(strOppCd_NewPQ, _
                                                       strOppCd_DetUpdPQ, _
                                                       strOppCd, _
                                                       strOppCd_Back, _
                                                       strParam, _
                                                       strCompany, _
                                                       Trim(strLocation), _
                                                       strUserId, _
                                                       strAccMonth, _
                                                       strAccYear, _
                                                       objPRID, _
                                                       objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.PurchaseRequest))
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=NEW_PURREQ_WITHOUT_PRID_SAVE&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
            End Try

            hidPQID.Value = objPRID
            onProcess_Load(hidPQID.Value)
        Else
            strParam = strPRID & "|" & strRemarks & "|" & lblTotAmtFig.Text & "|" & strPRStatus & "|" & strPrintDate & "|" & PRType.Text & "|" & _
                       strNewIDFormat & "|" & strNewYear & "|" & strTranPrefix & "|" & strHistYear & "|" & Right(strPhyYear, 2) & "|" & _
                       ddlDeptCode.SelectedItem.Value & "|" & ddlPRLevel.SelectedItem.Value & "|" & strDate & "|" & IIf(chkCentralized.Checked = True, "1", "0")

            Try
                intErrNo = objIN.mtdNewPurchaseRequest(strOppCd_NewPQ, _
                                                      strOppCd_DetUpdPQ, _
                                                       strOppCd, _
                                                       strOppCd_Back, _
                                                       strParam, _
                                                       strCompany, _
                                                       Trim(strLocation), _
                                                       strUserId, _
                                                       strAccMonth, _
                                                       strAccYear, _
                                                       objPRID, _
                                                       objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.PurchaseRequest))
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=NEW_PURREQLN_WITH_PRID_SAVE&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
            End Try

            onProcess_Load(strPRID)
        End If
    End Sub


    Sub btnConfirm_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strPRID As String = Trim(lblPurReqID.Text)
        Dim strStatus As Integer = objIN.EnumPurReqStatus.Confirmed
        Dim objPRDs As DataSet
        Dim objPRID As Object
        Dim strTotalAmt As String = lblTotAmtFig.Text.Trim
        Dim lblRemarks As Label
        Dim strRemarks As String = Request.Form("txtRemarks").Trim 
        Dim strParam As String
        Dim strParamTemp As String
        Dim strResultLocCode as string  = strLocation
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim indDate As String = ""

        If CheckDate(txtDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

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

        Dim arrParam As Array
        arrParam = Split(lblAccPeriod.Text, "/")
        If Month(strDate) <> arrParam(0) Then
            lblDate.Visible = True
            lblDate.Text = "Invalid transaction date."
            Exit Sub
        ElseIf Year(strDate) <> arrParam(1) Then
            lblDate.Visible = True
            lblDate.Text = "Invalid transaction date."
            Exit Sub
        End If

        strParamTemp = strPRID & "|" & strRemarks & "|" & strStatus & "|" & strTotalAmt & "|" & PRType.Text & "|" & ddlPRLevel.SelectedItem.Value

        If ddlPRLevel.SelectedItem.Value = objAdminLoc.EnumLocLevel.Estate Or ddlPRLevel.SelectedItem.Value = objAdminLoc.EnumLocLevel.Mill Then
            strParamTemp = strParamTemp & "|" & strLocation
        ElseIf ddlPRLevel.SelectedItem.Value = objAdminLoc.EnumLocLevel.Perwakilan Then
            GetLocCode(objAdminLoc.EnumLocLevel.Perwakilan, strResultLocCode)
            'to capture not online PR
            If chkCentralized.Checked = False Then
                strParamTemp = strParamTemp & "|" & strLocation
            Else
                strParamTemp = strParamTemp & "|" & strResultLocCode
            End If

        ElseIf ddlPRLevel.SelectedItem.Value = objAdminLoc.EnumLocLevel.HQ Then
            GetLocCode(objAdminLoc.EnumLocLevel.HQ, strResultLocCode)
            'to capture not online PR
            If chkCentralized.Checked = False Then
                strParamTemp = strParamTemp & "|" & strLocation
            Else
                strParamTemp = strParamTemp & "|" & strResultLocCode
            End If
        Else
            strParamTemp = strParamTemp & "|" & strLocation
        End If

        If ddlPRLevel.SelectedItem.Value = objAdminLoc.EnumLocLevel.Estate Or ddlPRLevel.SelectedItem.Value = objAdminLoc.EnumLocLevel.Mill Then
            strLocation = Trim(strLocation)
        ElseIf ddlPRLevel.SelectedItem.Value = objAdminLoc.EnumLocLevel.Perwakilan Then
            GetLocCode(objAdminLoc.EnumLocLevel.Perwakilan, strResultLocCode)
            'to capture not online PR
            If chkCentralized.Checked = False Then
                strLocation = Trim(strLocation)
            Else
                strLocation = Trim(strResultLocCode)
            End If
        ElseIf ddlPRLevel.SelectedItem.Value = objAdminLoc.EnumLocLevel.HQ Then
            GetLocCode(objAdminLoc.EnumLocLevel.HQ, strResultLocCode)
            'to capture not online PR
            If chkCentralized.Checked = False Then
                strLocation = Trim(strLocation)
            Else
                strLocation = Trim(strResultLocCode)
            End If
        Else
            strLocation = Trim(strLocation)
        End If

       

        Try
            intErrNo = objIN.mtdUpdPurchaseRequest(strOppCd_UpdPQ, _
                                                   strParamTemp, _
                                                   strCompany, _
                                                   Trim(strLocation), _
                                                   strUserId, _
                                                   objPRID, _
                                                   objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.PurchaseRequest))
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PURREQ_LIST_UPD_CONFIRM&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        End Try
        strLocation = strResultLocCode
        onProcess_Load(strPRID)
    End Sub

    Sub btnPRDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim objPRDs As DataSet
        Dim objPRLnDs As DataSet
        Dim objPRID As Object
        Dim strPRID As String = Trim(lblPurReqID.Text)
        Dim strPRStatus As Integer = objIN.EnumPurReqStatus.Deleted
        Dim strRemarks As String = Trim(txtRemarks.Text)
        Dim strTotalAmt As String = Trim(lblTotAmtFig.Text)
        Dim lblRemarks As Label
        Dim strParamTemp As String
        Dim strParam As String
        Dim strResultLocCode as string  = strLocation
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim indDate As String = ""

        If CheckDate(txtDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

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

        strParam = strPRID & "|" & strRemarks & "|" & strPRStatus & "|" & strTotalAmt & "|" & PRType.Text & "|" & ddlPRLevel.SelectedItem.Value

        strParam = strParam & "|" & strLocation

        if strLocLevel = objAdminLoc.EnumLocLevel.Estate then 
            strLocation = strLocation
        Elseif strLocLevel = objAdminLoc.EnumLocLevel.Perwakilan then 
            GetLocCode(objAdminLoc.EnumLocLevel.Perwakilan, strResultLocCode)
            strLocation = strResultLocCode
        Elseif strLocLevel = objAdminLoc.EnumLocLevel.HQ then 
            GetLocCode(objAdminLoc.EnumLocLevel.HQ, strResultLocCode)
            strLocation = strResultLocCode
        Else
            strLocation = strLocation
        End If

        Try
            intErrNo = objIN.mtdUpdPurchaseRequest(strOppCd_UpdPQ, _
                                                   strParam, _
                                                   strCompany, _
                                                   Trim(strLocation), _
                                                   strUserId, _
                                                   objPRID, _
                                                   objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.PurchaseRequest))
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PURREQ_LIST_UPD_DELETE_1REC&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        End Try
        
        onProcess_Load(strPRID)
    End Sub

    Sub btnPRUnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim objPRDs As DataSet
        Dim strPRID As String = Trim(lblPurReqID.Text)
        Dim strPRStatus As Integer = objIN.EnumPurReqStatus.Active
        Dim objPRID As Object
        Dim strTotalAmt As String = Trim(lblTotAmtFig.Text)
        Dim strRemarks As String = Trim(txtRemarks.Text)
        Dim strParam As String

        Dim strResultLocCode as string  = strLocation
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim indDate As String = ""

        If CheckDate(txtDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

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


        strParam = strPRID & "|" & strRemarks & "|" & strPRStatus & "|" & strTotalAmt & "|" & PRType.Text & "|" & ddlPRLevel.SelectedItem.Value

        strParam = strParam & "|" & strLocation

        if ddlPRLevel.SelectedItem.Value = objAdminLoc.EnumLocLevel.Estate then 
            strLocation = strLocation
        Elseif ddlPRLevel.SelectedItem.Value = objAdminLoc.EnumLocLevel.Perwakilan then 
            GetLocCode(objAdminLoc.EnumLocLevel.Perwakilan, strResultLocCode)
            strLocation = strResultLocCode
        Elseif ddlPRLevel.SelectedItem.Value = objAdminLoc.EnumLocLevel.HQ then 
            GetLocCode(objAdminLoc.EnumLocLevel.HQ, strResultLocCode)
            strLocation = strResultLocCode
        Else
            strLocation = strLocation
        end if

        Try
            intErrNo = objIN.mtdUpdPurchaseRequest(strOppCd_UpdPQ, _
                                                   strParam, _
                                                   strCompany, _
                                                   TRIM(strLocation), _
                                                   strUserId, _
                                                   objPRID, _
                                                   objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.PurchaseRequest))
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PURREQ_LIST_UPD_UNDELETE&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        End Try
        onProcess_Load(strPRID)
    End Sub

    Sub btnCancel_click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim objPRDs As DataSet
        Dim strPRID As String = Trim(lblPurReqID.Text)
        Dim strPRStatus As Integer = objIN.EnumPurReqStatus.Cancelled
        Dim objPRID As Object
        Dim strTotalAmt As String = Trim(lblTotAmtFig.Text)
        Dim strRemarks As String = Trim(txtRemarks.Text)
        Dim strParam As String
        Dim strResultLocCode As String = strLocation
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim indDate As String = ""

        If CheckDate(txtDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

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

        strParam = strPRID & "|" & strRemarks & "|" & strPRStatus & "|" & strTotalAmt & "|" & PRType.Text & "|" & ddlPRLevel.SelectedItem.Value
        strParam = strParam & "|" & strLocation

        Try
            intErrNo = objIN.mtdUpdPurchaseRequest(strOppCd_UpdPQ, _
                                                   strParam, _
                                                   strCompany, _
                                                   TRIM(strLocation), _
                                                   strUserId, _
                                                   objPRID, _
                                                   objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.PurchaseRequest))
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PURREQ_LIST_UPD_CANCEL&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        End Try

        onProcess_Load(strPRID)
    End Sub

    Sub btnPreview_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strOpCodePrint As String = "ADMIN_SHARE_UPD_PRINTDATE"
        Dim strUpdString As String = ""
        Dim strStatus As String
        Dim intStatus As Integer
        Dim strSortLine As String
        Dim strPrintDate As String
        Dim strTable As String

        strUpdString = "where PRID = '" & strPRID & "'"
        strStatus = Trim(Status.Text)
        intStatus = CInt(lblStatus.Text.Trim)
        strPrintDate = lblPrintDate.Text.Trim
        strSortLine = "PRln.PRID"
        strTable = "IN_PR"

        If intStatus = objIN.EnumPurReqStatus.Confirmed Then
            If strPrintDate = "" Then
                Try
                    intErrNo = objAdmin.mtdUpdPrintDate(strOpCodePrint, _
                                                        strUpdString, _
                                                        strTable, _
                                                        strCompany, _
                                                        TRIM(strLocation), _
                                                        strUserId)
                Catch Exp As System.Exception
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=IN_PURREQ_DETAILS_UPD_PRINTDATE&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
                End Try

            Else
                strStatus = strStatus & " (re-printed)"
            End If
        End If
        onLoad_DisplayPR(lblPurReqID.Text)
        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/IN_Rpt_PurReqDet.aspx?strPRID=" & lblPurReqID.Text & _
                       "&strPrintDate=" & strPrintDate & "&strStatus=" & strStatus & "&strSortLine=" & strSortLine & """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub


    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("IN_PurReq.aspx")
    End Sub

    Sub BindDeptCode(ByVal pv_strDeptCode As String)
        Dim strOpCd As String = "HR_CLSSETUP_DEPTCODE_LIST_GET"
        Dim strParam As String 
        Dim sortitem As String 
        Dim SearchStr As String 
        Dim drinsert As DataRow
        Dim intCnt As Integer
        Dim intSelectedIndex as Integer

        sortitem = "ORDER BY DEPT.deptcode" 
        SearchStr = ""
        strParam =  sortitem & "|" & SearchStr

        Try
            intErrNo = objHR.mtdGetMasterList(strOpCd, strParam, objHR.EnumHRMasterType.DeptCode, objDataSet)

            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=IN_PURREQ_DETAILS&errmesg=" & lblErrMessage.Text & "&redirect=")

        End Try

       For intCnt = 0 To objDataSet.Tables(0).Rows.Count - 1
            objDataSet.Tables(0).Rows(intCnt).Item("DeptCode")    = Trim(objDataSet.Tables(0).Rows(intCnt).Item("DeptCode"))
            objDataSet.Tables(0).Rows(intCnt).Item("Description") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("DeptCode")) & " ( " & _
                                                                            Trim(objDataSet.Tables(0).Rows(intCnt).Item("Description")) & " ) "                                                      
            If objDataSet.Tables(0).Rows(intCnt).Item("DeptCode") = pv_strDeptCode Then
                intSelectedIndex = intCnt + 1
            End If
       Next intCnt
            
       drinsert = objDataSet.Tables(0).NewRow()
       drinsert(0) = ""
       drinsert(1) = "Select Department Code"
       objDataSet.Tables(0).Rows.InsertAt(drinsert, 0)

       ddlDeptCode.DataSource = objDataSet.Tables(0)
       ddlDeptCode.DataValueField = "DeptCode"
       ddlDeptCode.DataTextField = "Description"
       ddlDeptCode.DataBind()
       ddlDeptCode.SelectedIndex = intSelectedIndex


    End Sub

    Sub BindPRLevel(ByVal pv_strPRLevel As String)

        if strLocLevel = "1" then 
            ddlPRLevel.Items.Clear
            ddlPRLevel.Items.Add(New ListItem("Select Location Level", ""))
            If strLocType = objAdminLoc.EnumLocType.Mill Then
                ddlPRLevel.Items.Add(New ListItem(objAdminLoc.mtdGetLocLevel(objAdminLoc.EnumLocLevel.Mill), objAdminLoc.EnumLocLevel.Mill))
            Else
                ddlPRLevel.Items.Add(New ListItem(objAdminLoc.mtdGetLocLevel(objAdminLoc.EnumLocLevel.Estate), objAdminLoc.EnumLocLevel.Estate))
            End If

            'ddlPRLevel.Items.Add(New ListItem(objAdminLoc.mtdGetLocLevel(objAdminLoc.EnumLocLevel.Perwakilan), objAdminLoc.EnumLocLevel.Perwakilan))
            'ddlPRLevel.Items.Add(New ListItem(objAdminLoc.mtdGetLocLevel(objAdminLoc.EnumLocLevel.HQ), objAdminLoc.EnumLocLevel.HQ))
            'ddlPRLevel.Items.Add(New ListItem(objAdminLoc.mtdGetLocLevel(objAdminLoc.EnumLocLevel.Mill), objAdminLoc.EnumLocLevel.Mill))
        ElseIf strLocLevel = "2" Then
            ddlPRLevel.Items.Clear()
            ddlPRLevel.Items.Add(New ListItem("Select Location Level", ""))
            ddlPRLevel.Items.Add(New ListItem(objAdminLoc.mtdGetLocLevel(objAdminLoc.EnumLocLevel.Perwakilan), objAdminLoc.EnumLocLevel.Perwakilan))
            'ddlPRLevel.Items.Add(New ListItem(objAdminLoc.mtdGetLocLevel(objAdminLoc.EnumLocLevel.HQ), objAdminLoc.EnumLocLevel.HQ))
        ElseIf strLocLevel = "3" Then
            ddlPRLevel.Items.Clear()
            ddlPRLevel.Items.Add(New ListItem("Select Location Level", ""))
            ddlPRLevel.Items.Add(New ListItem(objAdminLoc.mtdGetLocLevel(objAdminLoc.EnumLocLevel.HQ), objAdminLoc.EnumLocLevel.HQ))
        Else
            ddlPRLevel.Items.Clear()
            ddlPRLevel.Items.Add(New ListItem("Select Location Level", ""))
            ddlPRLevel.Items.Add(New ListItem(objAdminLoc.mtdGetLocLevel(objAdminLoc.EnumLocLevel.Estate), objAdminLoc.EnumLocLevel.Estate))
            ddlPRLevel.Items.Add(New ListItem(objAdminLoc.mtdGetLocLevel(objAdminLoc.EnumLocLevel.Perwakilan), objAdminLoc.EnumLocLevel.Perwakilan))
            ddlPRLevel.Items.Add(New ListItem(objAdminLoc.mtdGetLocLevel(objAdminLoc.EnumLocLevel.HQ), objAdminLoc.EnumLocLevel.HQ))
            ddlPRLevel.Items.Add(New ListItem(objAdminLoc.mtdGetLocLevel(objAdminLoc.EnumLocLevel.Mill), objAdminLoc.EnumLocLevel.Mill))
        End If

        If Not Trim(pv_strPRLevel) = "" Then
            ddlPRLevel.Items.Clear()
            ddlPRLevel.Items.Add(New ListItem("Select Location Level", ""))
            ddlPRLevel.Items.Add(New ListItem(objAdminLoc.mtdGetLocLevel(objAdminLoc.EnumLocLevel.Estate), objAdminLoc.EnumLocLevel.Estate))
            ddlPRLevel.Items.Add(New ListItem(objAdminLoc.mtdGetLocLevel(objAdminLoc.EnumLocLevel.Perwakilan), objAdminLoc.EnumLocLevel.Perwakilan))
            ddlPRLevel.Items.Add(New ListItem(objAdminLoc.mtdGetLocLevel(objAdminLoc.EnumLocLevel.HQ), objAdminLoc.EnumLocLevel.HQ))
            ddlPRLevel.Items.Add(New ListItem(objAdminLoc.mtdGetLocLevel(objAdminLoc.EnumLocLevel.Mill), objAdminLoc.EnumLocLevel.Mill))
            With ddlPRLevel
                .SelectedIndex = .Items.IndexOf(.Items.FindByValue(Trim(pv_strPRLevel)))
            End With
        End If

    End Sub

    Sub GetLocCode(ByVal pv_strLocLevel as string, ByRef pv_strResult as string)
        Dim strOpCd_GetLocLevel As String = "ADMIN_CLSLOC_LOCATION_LIST_GET" 
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim objLocCodeDs as New Object()

        strParam = " and a.loclevel = '" & pv_strLocLevel & "'"

        Try
            intErrNo = objAdminLoc.mtdGetDataLocCode(strOpCd_GetLocLevel, strParam, objLocCodeDs)
        Catch Exp As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADMIN_GET_LOCATION&errmesg=" & lblErrMessage.Text & "&redirect=admin/location/admin_location_locdet.aspx")
        End Try

        if objLocCodeDs.Tables(0).Rows.Count > 0 then
            pv_strResult = objLocCodeDs.Tables(0).Rows(0).Item("LocCode")
        end if 
    End Sub

    Sub TextChanged()

        Dim strOpCd As String = "IN_CLSTRX_PURREQ_ITEMLIST_GET"
        Dim objItemDs As New Object()
        Dim strParam As String = ""
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer = 0
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim strItemType As String


        Select Case PRType.Text
            Case objIN.EnumPurReqDocType.StockPR
                strItemType = "'" & objINSetup.EnumInventoryItemType.Stock & "','" & objINSetup.EnumInventoryItemType.WorkshopItem & "'"
            Case objIN.EnumPurReqDocType.DirectChargePR
                strItemType = objINSetup.EnumInventoryItemType.DirectCharge
            Case objIN.EnumPurReqDocType.CanteenPR
                strItemType = objINSetup.EnumInventoryItemType.CanteenItem
            Case objIN.EnumPurReqDocType.WorkshopPR
                strItemType = objINSetup.EnumInventoryItemType.WorkshopItem
            Case objIN.EnumPurReqDocType.FixedAssetPR
                strItemType = objINSetup.EnumInventoryItemType.FixedAssetItem
            Case objIN.EnumPurReqDocType.NurseryPR
                strItemType = objINSetup.EnumInventoryItemType.NurseryItem
        End Select

        strParamName = "LOCCODE|ITEMTYPE|ITEMSTATUS|ITEMCODE|CENTRALIZED|PRID"
        strParamValue = strLocation & _
                        "|" & strItemType & _
                        "|" & objINSetup.EnumStockItemStatus.Active & _
                        "|" & txtItemCode.Text & _
                        "|" & "0" & _
                        "|" & ""

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objItemDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_ITEM&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        End Try

        If objItemDs.Tables(0).Rows.Count > 0 Then
            TxtItemName.Text = objItemDs.Tables(0).Rows(0).Item("Description")
            UnitCost.Text = objItemDs.Tables(0).Rows(0).Item("AverageCost")
        End If
    End Sub

    'Sub BindStatusLn(ByVal pv_status As String)
    '    ddlStatusLn.Items.Clear()
    '    ddlStatusLn.Items.Add(New ListItem(objIN.mtdGetPurReqLnStatus(objIN.EnumPurReqLnStatus.Active), objIN.EnumPurReqLnStatus.Active))
    '    ddlStatusLn.Items.Add(New ListItem(objIN.mtdGetPurReqLnStatus(objIN.EnumPurReqLnStatus.Cancel), objIN.EnumPurReqLnStatus.Cancel))
    '    ddlStatusLn.Items.Add(New ListItem(objIN.mtdGetPurReqLnStatus(objIN.EnumPurReqLnStatus.Confirmed), objIN.EnumPurReqLnStatus.Confirmed))
    '    ddlStatusLn.Items.Add(New ListItem(objIN.mtdGetPurReqLnStatus(objIN.EnumPurReqLnStatus.Hold), objIN.EnumPurReqLnStatus.Hold))

    '    If Not Trim(pv_status) = "" Then
    '        With ddlStatusLn
    '            .SelectedIndex = .Items.IndexOf(.Items.FindByValue(Trim(pv_status)))
    '        End With
    '    End If
    'End Sub

    'Sub BindItemCode(ByVal pv_strPRId As String, ByVal pv_strItemCode As String)
    '    Dim strOpCd As String = "IN_CLSTRX_PURREQ_ITEMLIST_GET"
    '    Dim objItemDs As New Object()
    '    Dim strParam As String = ""
    '    Dim intCnt As Integer = 0
    '    Dim intErrNo As Integer
    '    Dim dr As DataRow
    '    Dim intSelectedIndex As Integer = 0
    '    Dim strParamName As String = ""
    '    Dim strParamValue As String = ""
    '    Dim strItemType As String

    '    Select Case PRType.Text
    '        Case objIN.EnumPurReqDocType.StockPR
    '            strItemType = "'" & objINSetup.EnumInventoryItemType.Stock & "','" & objINSetup.EnumInventoryItemType.WorkshopItem & "'"
    '        Case objIN.EnumPurReqDocType.DirectChargePR
    '            strItemType = objINSetup.EnumInventoryItemType.DirectCharge
    '        Case objIN.EnumPurReqDocType.CanteenPR
    '            strItemType = objINSetup.EnumInventoryItemType.CanteenItem
    '        Case objIN.EnumPurReqDocType.WorkshopPR
    '            strItemType = objINSetup.EnumInventoryItemType.WorkshopItem
    '        Case objIN.EnumPurReqDocType.FixedAssetPR
    '            strItemType = objINSetup.EnumInventoryItemType.FixedAssetItem
    '        Case objIN.EnumPurReqDocType.NurseryPR
    '            strItemType = objINSetup.EnumInventoryItemType.NurseryItem
    '    End Select

    '    strParamName = "LOCCODE|PRID|ITEMTYPE|ITEMSTATUS|ITEMCODE"
    '    strParamValue = strLocation & _
    '                    "|" & pv_strPRId & _
    '                    "|" & strItemType & _
    '                    "|" & objINSetup.EnumStockItemStatus.Active & _
    '                    "|" & pv_strItemCode

    '    Try
    '        intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
    '                                            strParamName, _
    '                                            strParamValue, _
    '                                            objItemDs)
    '    Catch Exp As System.Exception
    '        Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_ITEM&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
    '    End Try

    '    For intCnt = 0 To objItemDs.Tables(0).Rows.Count - 1
    '        objItemDs.Tables(0).Rows(intCnt).Item("ItemCode") = Trim(objItemDs.Tables(0).Rows(intCnt).Item("ItemCode"))
    '        objItemDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objItemDs.Tables(0).Rows(intCnt).Item("ItemCode")) & " ( " & _
    '                                                                    Trim(objItemDs.Tables(0).Rows(intCnt).Item("Description")) & " ), " & _
    '                                                                    "Rp. " & objGlobal.GetIDDecimalSeparator(objItemDs.Tables(0).Rows(intCnt).Item("AverageCost")) & ", " & _
    '                                                                    objGlobal.GetIDDecimalSeparator_FreeDigit(objItemDs.Tables(0).Rows(intCnt).Item("QtyOnHand"), 5) & ", " & _
    '                                                                    Trim(objItemDs.Tables(0).Rows(intCnt).Item("UOMCode"))

    '    Next intCnt

    '    Dim drinsert As DataRow
    '    drinsert = objItemDs.Tables(0).NewRow()
    '    drinsert(0) = " "
    '    drinsert(1) = "Select Item Code"
    '    objItemDs.Tables(0).Rows.InsertAt(drinsert, 0)

    '    lstStkDC.DataSource = objItemDs.Tables(0)
    '    lstStkDC.DataValueField = "ItemCode"
    '    lstStkDC.DataTextField = "Description"
    '    lstStkDC.DataBind()
    '    lstStkDC.SelectedIndex = 1
    'End Sub

    Function CheckDate(ByVal pv_strDate As String, ByRef pr_strDate As String) As Boolean
        Dim strDateFormatCode As String = Session("SS_DATEFMT")
        Dim strDateFormat As String

        pr_strDate = ""
        CheckDate = True
        If Not pv_strDate = "" Then
            If objGlobal.mtdValidInputDate(strDateFormatCode, pv_strDate, strDateFormat, pr_strDate) = False Then
                lblFmt.Text = strDateFormat
                pr_strDate = ""
                CheckDate = False
            End If
        End If
    End Function

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

    Sub Centralized_Type(ByVal Sender As Object, ByVal E As EventArgs)
        If chkCentralized.Checked = True Then
            chkCentralized.Text = "  Yes"
        Else
            chkCentralized.Text = "  No"
        End If
    End Sub

    Sub btnAddendum_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_GetPR As String = "IN_CLSTRX_PURREQ_LIST_GET"
        Dim strOpCd As String = "IN_CLSTRX_PURREQ_ADD_ADDENDUM"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim intErrNo As Integer
        Dim dsMaster As Object
        Dim objItemDs As New Object()
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim indDate As String = ""

        If CheckDate(txtDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

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

        strParamName = "SEARCHSTR|SORTEXP"
        strParamValue = "AND PR.PRID = '" & Trim(lblPurReqID.Text) & "-ADD' " & "|"

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd_GetPR, _
                                                strParamName, _
                                                strParamValue, _
                                                dsMaster)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_PO&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_POdet")
        End Try

        If dsMaster.Tables(0).Rows.Count = 0 Then
            strParamName = "PRID|USERID"
            strParamValue = Trim(lblPurReqID.Text) & _
                            "|" & Trim(strUserId)

            Try
                intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                                    strParamName, _
                                                    strParamValue, _
                                                    objItemDs)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_PR&errmesg=" & Exp.ToString() & "&redirect=in/trx/IN_PurReq_Details")
            End Try

            If objItemDs.Tables(0).Rows.Count = 0 Then
                lblErrGR.Visible = True
                lblErrGR.Text = "All Item have been purchased. Cannot create addendum."
                Exit Sub
            Else
                Response.Redirect("IN_PurReq_Details.aspx?PRID=" & Trim(lblPurReqID.Text) & "-ADD")
            End If
        Else
            lblErrGR.Visible = True
            lblErrGR.Text = "This PR already have addendum."
            Exit Sub
        End If
    End Sub
End Class
