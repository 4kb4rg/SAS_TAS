
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
Imports Microsoft.VisualBasic.Information

'Imports Infragistics.WebUI.WebCombo

Public Class PU_trx_RPHDet_Generate : Inherits Page
    Private fNom As String = "#,###."

    Protected WithEvents dgPRLn As DataGrid

    Protected WithEvents hidPQID As HtmlInputHidden
    Protected WithEvents PRLnTable As HtmlTable
    'Protected WithEvents BtnViewPR As HtmlInputButton

    Protected WithEvents LblStatus As Label

    Protected WithEvents lblRemark As Label

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents SortExpression As Label


    Protected WithEvents txtPRID_Plmph As TextBox
    Protected WithEvents lblPurReqID As Label

    Protected WithEvents lblErrGR As Label
    Protected WithEvents Save As Image
    
    Protected WithEvents Back As Image

    Protected WithEvents ddlPPN1 As DropDownList
    Protected WithEvents ddlPPN2 As DropDownList
    Protected WithEvents ddlPPN3 As DropDownList

    Protected WithEvents txtSupCode1 As TextBox
    Protected WithEvents txtSupCode2 As TextBox
    Protected WithEvents txtSupCode3 As TextBox

    Protected WithEvents txtSupName1 As TextBox
    Protected WithEvents txtSupName2 As TextBox
    Protected WithEvents txtSupName3 As TextBox

    Protected WithEvents lblErrItemQty1 As Label
    Protected WithEvents lblErrItemQty2 As Label
    Protected WithEvents lblErrItemQty3 As Label

    Protected WithEvents txtPPNInit1 As TextBox
    Protected WithEvents txtPPNInit2 As TextBox
    Protected WithEvents txtPPNInit3 As TextBox

    Protected WithEvents hidPPN1 As HtmlInputHidden
    Protected WithEvents hidPPN2 As HtmlInputHidden
    Protected WithEvents hidPPN3 As HtmlInputHidden


    'Protected WithEvents hidPRType As HtmlInputHidden

    Dim strDateFMT As String

    Protected objIN As New agri.IN.clsTrx()
    Protected objINSetup As New agri.IN.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl
    Protected objAdmin As New agri.Admin.clsShare()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLocCodeDs As New Object()

    Protected objPU As New agri.PU.clsTrx()
    Protected WithEvents lblErrMesage As Label
    Protected objHR As New agri.HR.clsSetup()
    Dim objSysComp As New agri.Admin.clsComp()
    Dim objGLtrx As New agri.GL.ClsTrx()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()

    Protected objGLSetup As New agri.GL.clsSetup()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Dim PreBlockTag As String
    Dim BlockTag As String
    Dim objAccDs As New Dataset()
    Dim objBlkDs As New Dataset()
    Dim objVehDs As New Dataset()
    Dim objVehExpDs As New Dataset()
    Dim objPODs As New Object()
    Dim objLangCapDs As New Dataset()
    Dim intConfigSetting As Integer
    Dim strParamName As String
    Dim strParamValue As String


    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intPUAR As Integer
    Dim strAccMonth As String
    Dim strAccYear As String

    Dim strPhyYear As String
    Dim strPhyMonth As String
    Dim strLastPhyYear As String
    Dim intLevel As Integer

    Const ITEM_PART_SEPERATOR As String = " @ "

    Dim objItemDs As New Object()
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

#Region "TOOLS & COMPONENT"

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        Dim prqtype As String

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intPUAR = Session("SS_PUAR")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_SELACCYEAR") 'Session("SS_INACCYEAR")
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
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUPurchaseOrder), intPUAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.Text = "" Then
                SortExpression.Text = "ItemCode"
            End If

            txtSupCode1.Attributes.Add("readonly", "readonly")
            txtSupCode2.Attributes.Add("readonly", "readonly")
            txtSupCode3.Attributes.Add("readonly", "readonly")

            txtSupName1.Attributes.Add("readonly", "readonly")
            txtSupName2.Attributes.Add("readonly", "readonly")
            txtSupName3.Attributes.Add("readonly", "readonly")

            'ddlPPN1.Attributes.Add("readonly", "readonly")
            'ddlPPN2.Attributes.Add("readonly", "readonly")
            'ddlPPN3.Attributes.Add("readonly", "readonly")

            'to avoid double click, on aspx add this : UseSubmitBehavior="false"
            'btnAdd.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnAdd).ToString())
            Save.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Save).ToString())
            Back.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Back).ToString())

            If Not IsPostBack Then
                BindPPNList()

                txtSupCode1.Text = Session("SupCode1")
                txtSupName1.Text = Session("SupName1")

                txtSupCode2.Text = Session("SupCode2")
                txtSupName2.Text = Session("SupName2")

                txtSupCode3.Text = Session("SupCode3")
                txtSupName3.Text = Session("SupName3")

                If Session("PPN1") = "1" Then
                    ddlPPN1.SelectedIndex = "1"
                    txtPPNInit1.Text = "1"
                    hidPPN1.Value = "1"
                Else
                    ddlPPN1.SelectedIndex = "0"
                End If

                If Session("PPN2") = "1" Then
                    ddlPPN2.SelectedIndex = "1"
                    txtPPNInit2.Text = "1"
                    hidPPN2.Value = "1"
                Else
                    ddlPPN2.SelectedIndex = "0"
                End If

                If Session("PPN3") = "1" Then
                    ddlPPN3.SelectedIndex = "1"
                    txtPPNInit3.Text = "1"
                    hidPPN3.Value = "1"
                Else
                    ddlPPN3.SelectedIndex = "0"
                End If

                lblPurReqID.Text = Session("DTID")
                txtPRID_Plmph.Attributes.Add("readonly", "readonly")
                txtPRID_Plmph.Text = Session("PRID")
                onProcess_Load()
            End If
        End If
    End Sub

    Sub btnAdd_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        '        dgPRLn.Items.Item(E.Item.ItemIndex).Cells.RemoveAt(1)

        'dg.Rows[gr.RowIndex].Cells[0].Text == dg.Rows[gr.RowIndex + 1].Cells[0].Text

        hidPPN1.Value = txtPPNInit1.Text
        hidPPN2.Value = txtPPNInit2.Text
        hidPPN3.Value = txtPPNInit3.Text

        CType(dgPRLn.Items(E.Item.ItemIndex).FindControl("lblPrLNID"), Label).Text = ""
        CType(dgPRLn.Items(E.Item.ItemIndex).FindControl("lblItemCode"), Label).Text = ""
        CType(dgPRLn.Items(E.Item.ItemIndex).FindControl("lblItemDesc"), Label).Text = ""
        CType(dgPRLn.Items(E.Item.ItemIndex).FindControl("lblUOMCode"), Label).Text = ""
        CType(dgPRLn.Items(E.Item.ItemIndex).FindControl("lblQtyReqDisplay"), Label).Text = ""
        CType(dgPRLn.Items(E.Item.ItemIndex).FindControl("txtQtyOrder1"), TextBox).Text = ""
        CType(dgPRLn.Items(E.Item.ItemIndex).FindControl("txtCost1"), TextBox).Text = ""
        CType(dgPRLn.Items(E.Item.ItemIndex).FindControl("txtQtyOrder2"), TextBox).Text = ""
        CType(dgPRLn.Items(E.Item.ItemIndex).FindControl("txtSupNote1"), TextBox).Text = ""
        CType(dgPRLn.Items(E.Item.ItemIndex).FindControl("txtCost2"), TextBox).Text = ""
        CType(dgPRLn.Items(E.Item.ItemIndex).FindControl("txtSupNote2"), TextBox).Text = ""
        CType(dgPRLn.Items(E.Item.ItemIndex).FindControl("txtQtyOrder3"), TextBox).Text = ""
        CType(dgPRLn.Items(E.Item.ItemIndex).FindControl("txtCost3"), TextBox).Text = ""
        CType(dgPRLn.Items(E.Item.ItemIndex).FindControl("txtSupNote3"), TextBox).Text = ""


        'dgPRLn.Items(E.Item.ItemIndex).Cells(4).BackColor = Drawing.Color.Gray
        'dgPRLn.Items(E.Item.ItemIndex).Cells(5).BackColor = Drawing.Color.Gray
        'dgPRLn.Items(E.Item.ItemIndex).Cells(6).BackColor = Drawing.Color.Gray

        CType(dgPRLn.Items(E.Item.ItemIndex).FindControl("txtQtyOrder1"), TextBox).Visible = False
        CType(dgPRLn.Items(E.Item.ItemIndex).FindControl("txtQtyOrder2"), TextBox).Visible = False
        CType(dgPRLn.Items(E.Item.ItemIndex).FindControl("txtQtyOrder3"), TextBox).Visible = False
        CType(dgPRLn.Items(E.Item.ItemIndex).FindControl("txtCost1"), TextBox).Visible = False
        CType(dgPRLn.Items(E.Item.ItemIndex).FindControl("txtCost2"), TextBox).Visible = False
        CType(dgPRLn.Items(E.Item.ItemIndex).FindControl("txtCost3"), TextBox).Visible = False

        CType(dgPRLn.Items(E.Item.ItemIndex).FindControl("txtSupNote1"), TextBox).Visible = False
        CType(dgPRLn.Items(E.Item.ItemIndex).FindControl("txtSupNote2"), TextBox).Visible = False
        CType(dgPRLn.Items(E.Item.ItemIndex).FindControl("txtSupNote3"), TextBox).Visible = False
        dgPRLn.Items(E.Item.ItemIndex).Visible = False
        PageConTrol()
    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        hidPPN1.Value = txtPPNInit1.Text
        hidPPN2.Value = txtPPNInit2.Text
        hidPPN3.Value = txtPPNInit3.Text
    End Sub

    Private Sub dgPRLn_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPRLn.ItemCreated
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
            dgCell.Text = "ITEM /PR NOTE"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "STOCK UOM"
            dgCell.HorizontalAlign = HorizontalAlign.Center


            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "QTY REQUESTED"
            dgCell.HorizontalAlign = HorizontalAlign.Center



            dgCell = New TableCell()
            dgCell.ColumnSpan = 3
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "SUPPLIER I"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 3
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "SUPPLER II"
            dgCell.HorizontalAlign = HorizontalAlign.Center


            dgCell = New TableCell()
            dgCell.ColumnSpan = 3
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "SUPPLIER III"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = ""
            dgCell.HorizontalAlign = HorizontalAlign.Center




            'dgCell = New TableCell()
            'dgCell.RowSpan = 2
            'dgItem.Cells.Add(dgCell)
            'dgCell.Text = "Qty Order"
            'dgCell.HorizontalAlign = HorizontalAlign.Center


            'dgCell = New TableCell()
            'dgCell.RowSpan = 2
            'dgItem.Cells.Add(dgCell)
            'dgCell.Text = "Cost"
            'dgCell.HorizontalAlign = HorizontalAlign.Center

            'dgCell = New TableCell()
            'dgCell.ColumnSpan = 9
            'dgItem.Cells.Add(dgCell)
            'dgCell.Text = "SUPPLIER 1"
            'dgCell.HorizontalAlign = HorizontalAlign.Center


            dgItem.Font.Bold = True
            dgPRLn.Controls(0).Controls.AddAt(0, dgItem)
        End If
    End Sub

    Private Sub dgPRLn_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPRLn.ItemDataBound
        If e.Item.ItemType = ListItemType.Header Then
            Dim dgItem As DataGridItem

            dgItem = New DataGridItem(0, 0, ListItemType.Header)

            e.Item.Cells(0).Visible = False
            e.Item.Cells(1).Visible = False
            e.Item.Cells(2).Visible = False
            e.Item.Cells(3).Visible = False
            e.Item.Cells(13).Visible = False
            'e.Item.Cells(4).Visible = False
            'e.Item.Cells(5).Visible = False
            'e.Item.Cells(6).Visible = False
        End If
    End Sub

    Sub dgPRLn_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue'")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")
            End If
        End If
    End Sub
    Sub btnNew_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        txtSupCode1.Text = ""
        txtSupCode2.Text = ""
        txtSupCode3.Text = ""
        txtSupName1.Text = ""
        txtSupName2.Text = ""
        txtSupName3.Text = ""
    End Sub
    Sub btnSave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        'UserMsgBox(Me, ddlPPN1.SelectedItem.Value)
        Dim strOpCd_AddRPHLn As String = "PU_CLSTRX_RPH_LINE_ADD"
        Dim strPRId As String = Trim(Request.Form("txtPRID_Plmph"))
        Dim strRemark As String = Trim(Request.Form("txtRemark"))
        Dim bBoolean As Boolean = False
        Dim strParam As String = ""
        Dim strParamName As String = ""

        Dim intErrNo As Integer
        Dim strFSupp1 As String = "2"
        Dim strFSupp2 As String = "2"
        Dim strFSupp3 As String = "2"
        Dim strPBBKB1 As Double = 0
        Dim strPBBKB2 As Double = 0
        Dim strPBBKB3 As Double = 0

        Dim strPBBKBRate1 As Double = 0
        Dim strPBBKBRate2 As Double = 0
        Dim strPBBKBRate3 As Double = 0

        Dim strTransporter As String = ""
        Dim strAmtTransportFee As Double = 0
        Dim strTransporter2 As String = ""
        Dim strAmtTransportFee2 As Double = 0
        Dim strTransporter3 As String = ""
        Dim strAmtTransportFee3 As Double = 0
        Dim strExRate As Double = 0

        Dim strDiscount1 As Double = 0
        Dim strDiscount2 As Double = 0
        Dim strDiscount3 As Double = 0
        Dim strPPN221 As Double = 0
        Dim strPPN222 As Double = 0
        Dim strPPN223 As Double = 0
        Dim strAddDiscount As String = 0

        Dim nPPNAmount1 As Double
        Dim nPPNAmount2 As Double
        Dim nPPNAmount3 As Double

        Dim nPPNAmounTrans1 As Double
        Dim nPPNAmountTrans2 As Double
        Dim nPPNAmountTrans3 As Double

        Dim StrNoUrut As String = ""

        Dim strTtlCost1 As Double = 0
        Dim strTtlCost2 As Double = 0
        Dim strTtlCost3 As Double = 0

        Dim IsPPN1 As Integer = ddlPPN1.SelectedItem.Value
        Dim IsPPN2 As Integer = ddlPPN2.SelectedItem.Value
        Dim IsPPN3 As Integer = ddlPPN3.SelectedItem.Value

        nPPNAmount1 = 0
        nPPNAmount2 = 0
        nPPNAmount3 = 0

        nPPNAmounTrans1 = 0
        nPPNAmountTrans2 = 0
        nPPNAmountTrans3 = 0

        If Len(txtSupCode1.Text) = 0 And Len(txtSupCode2.Text) = 0 And Len(txtSupCode3.Text) = 0 Then
            UserMsgBox(Me, "Please Select Supplier !!!")
            Exit Sub
        End If

        bBoolean = False
        strExRate = Session("EXRATE")
        ''detail
        Dim StrRPHLnID As String

        For intCnt = 0 To dgPRLn.Items.Count - 1
            If Len(CType(dgPRLn.Items(intCnt).FindControl("lblPrLNID"), Label).Text) > 0 Then

                Dim strPRLocCode As String = CType(dgPRLn.Items(intCnt).FindControl("lblPrLocCode"), Label).Text
                Dim strPRRefId As String = CType(dgPRLn.Items(intCnt).FindControl("lblPrLNID"), Label).Text
                Dim strPRRefLocCode As String = Session("PRREFLOCCODE")
                Dim strItemCode As String = CType(dgPRLn.Items(intCnt).FindControl("lblItemCode"), Label).Text

                Dim strQtyPR As Double = CType(dgPRLn.Items(intCnt).FindControl("lblQtyReqDisplay"), Label).Text

                Dim strQtyOrder1 As Double = lCDbl(CType(dgPRLn.Items(intCnt).FindControl("txtQtyOrder1"), TextBox).Text)
                Dim strCost1 As Double = lCDbl(CType(dgPRLn.Items(intCnt).FindControl("txtCost1"), TextBox).Text)

                Dim strQtyOrder2 As Double = IIf(Len(txtSupCode2.Text) > 0, lCDbl(CType(dgPRLn.Items(intCnt).FindControl("txtQtyOrder2"), TextBox).Text), 0)
                Dim strCost2 As Double = IIf(Len(txtSupCode2.Text) > 0, lCDbl(CType(dgPRLn.Items(intCnt).FindControl("txtCost2"), TextBox).Text), 0)

                Dim strQtyOrder3 As Double = IIf(Len(txtSupCode3.Text) > 0, lCDbl(CType(dgPRLn.Items(intCnt).FindControl("txtQtyOrder3"), TextBox).Text), 0)
                Dim strCost3 As Double = IIf(Len(txtSupCode3.Text) > 0, lCDbl(CType(dgPRLn.Items(intCnt).FindControl("txtCost3"), TextBox).Text), 0)

                Dim SupNote1 As String = IIf(Len(txtSupCode1.Text) > 0, CType(dgPRLn.Items(intCnt).FindControl("txtSupNote1"), TextBox).Text, "")
                Dim SupNote2 As String = IIf(Len(txtSupCode2.Text) > 0, CType(dgPRLn.Items(intCnt).FindControl("txtSupNote2"), TextBox).Text, "")
                Dim SupNote3 As String = IIf(Len(txtSupCode3.Text) > 0, CType(dgPRLn.Items(intCnt).FindControl("txtSupNote3"), TextBox).Text, "")
                Dim strAddNote As String = CType(dgPRLn.Items(intCnt).FindControl("txtAddNote"), TextBox).Text

                strTtlCost1 = IIf(Len(txtSupCode1.Text) > 0, (strQtyOrder1 * strCost1), 0)
                strTtlCost2 = IIf(Len(txtSupCode2.Text) > 0, (strQtyOrder2 * strCost2), 0)
                strTtlCost3 = IIf(Len(txtSupCode3.Text) > 0, (strQtyOrder3 * strCost3), 0)

                hidPPN1.Value = txtPPNInit1.Text
                hidPPN2.Value = txtPPNInit2.Text
                hidPPN3.Value = txtPPNInit3.Text

                
                If strQtyOrder1 > strQtyPR Then
                    CType(dgPRLn.Items(intCnt).FindControl("lblErrItemQty1"), Label).Visible = True
                    Exit Sub
                End If

                If strQtyOrder2 > strQtyPR Then
                    CType(dgPRLn.Items(intCnt).FindControl("lblErrItemQty2"), Label).Visible = True
                    Exit Sub
                End If

                If strQtyOrder3 > strQtyPR Then
                    CType(dgPRLn.Items(intCnt).FindControl("lblErrItemQty3"), Label).Visible = True
                    Exit Sub
                End If

                If txtPPNInit1.Text = "1" Then 'ddlPPN1.SelectedItem.Value = "1" Then
                    nPPNAmount1 = Round(Divide(Session("SS_PPNRATE") * strTtlCost1, 100), 0)
                Else
                    nPPNAmount1 = 0
                End If

                If txtPPNInit2.Text = "1" Then 'ddlPPN2.SelectedItem.Value = "1" Then
                    nPPNAmount2 = Round(Divide(Session("SS_PPNRATE") * strTtlCost2, 100), 0)
                Else
                    nPPNAmount2 = 0
                End If

                If txtPPNInit3.Text = "1" Then 'ddlPPN3.SelectedItem.Value = "1" Then
                    nPPNAmount3 = Round(Divide(Session("SS_PPNRATE") * strTtlCost3, 100), 0)
                Else
                    nPPNAmount3 = 0
                End If

                'Response.Write(txtSupCode1.Text & "|" & Session("SS_PPNRATE") & "|" & strTtlCost1 & "|" & nPPNAmount1 & "|" & ddlPPN1.SelectedItem.Value & "|" & txtPPNInit1.text & "-")


                If strCost1 > 0 Or strCost2 > 0 Or strCost3 > 0 Then
                    StrRPHLnID = "RPHLN" & Right(RTrim(strAccYear), 2)
                    strParamName = "RPHLNID|RPHID|PRID|PRLNID|PRLOCCODE|PRREFID|PRREFLOCCODE|ITEMCODE|" & _
                                    "SUPPCODE1|QTYORDER1|COST1|NETAMOUNT1|PPNAMOUNT1|FSUPP1|" & _
                                    "SUPPCODE2|QTYORDER2|COST2|NETAMOUNT2|PPNAMOUNT2|FSUPP2|" & _
                                    "SUPPCODE3|QTYORDER3|COST3|NETAMOUNT3|PPNAMOUNT3|FSUPP3|" & _
                                    "PPN|PPN2|PPN3|STATUS|PBBKB1|PBBKB2|PBBKB3|PPNPSN1|PPNPSN2|PPNPSN3|" & _
                                    "PBBKBRATE1|PBBKBRATE2|PBBKBRATE3|ACCYEAR|ADDITIONALNOTE|" & _
                                    "AMOUNTCURRENCY1|AMOUNTCURRENCY2|AMOUNTCURRENCY3|" & _
                                    "TRANSPORTER|AMTTRANSPORTFEE|PPNTR1|NOMPPNTR1|" & _
                                    "TRANSPORTER2|AMTTRANSPORTFEE2|PPNTR2|NOMPPNTR2|" & _
                                    "TRANSPORTER3|AMTTRANSPORTFEE3|PPNTR3|NOMPPNTR3|" & _
                                    "DISCOUNT1|DISCOUNT2|DISCOUNT3|PPN221|PPN222|PPN223|LOCCODE|EXRATE|SUPNOTE1|SUPNOTE2|SUPNOTE3|REPITEM"
                    strParam = StrRPHLnID & "|" & _
                                   lblPurReqID.Text & "|" & _
                                   strPRId & "|" & _
                                   strPRRefId & "|" & _
                                   strPRLocCode & "|" & _
                                   strPRRefId & "|" & _
                                   strPRRefLocCode & "|" & _
                                   strItemCode & "|" & _
                                   txtSupCode1.Text & "|" & _
                                   strQtyOrder1 & "|" & _
                                   strCost1 & "|" & _
                                   strTtlCost1 & "|" & _
                                   nPPNAmount1 & "|" & _
                                   strFSupp1 & "|" & _
                                   txtSupCode2.Text & "|" & _
                                   strQtyOrder2 & "|" & _
                                   strCost2 & "|" & _
                                   strTtlCost2 & "|" & _
                                   nPPNAmount2 & "|" & _
                                   strFSupp2 & "|" & _
                                   txtSupCode3.Text & "|" & _
                                   strQtyOrder3 & "|" & _
                                   strCost3 & "|" & _
                                   strTtlCost3 & "|" & _
                                   nPPNAmount3 & "|" & _
                                   strFSupp3 & "|" & _
                                   IIf(txtPPNInit1.Text = "1", objPU.EnumPPN.Yes, objPU.EnumPPN.No) & "|" & _
                                   IIf(txtPPNInit2.Text = "1", objPU.EnumPPN.Yes, objPU.EnumPPN.No) & "|" & _
                                   IIf(txtPPNInit3.Text = "1", objPU.EnumPPN.Yes, objPU.EnumPPN.No) & "|" & _
                                   "1" & "|" & _
                                   strPBBKB1 & "|" & _
                                   strPBBKB2 & "|" & _
                                   strPBBKB3 & "|" & _
                                   IIf(txtPPNInit1.Text = "1", Session("SS_PPNRATE"), 0) & "|" & _
                                   IIf(txtPPNInit2.Text = "1", Session("SS_PPNRATE"), 0) & "|" & _
                                   IIf(txtPPNInit3.Text = "1", Session("SS_PPNRATE"), 0) & "|" & _
                                   strPBBKBRate1 & "|" & _
                                   strPBBKBRate2 & "|" & _
                                   strPBBKBRate3 & "|" & _
                                   strAccYear & "|" & _
                                   strAddNote & "|" & _
                                   strCost1 & "|" & _
                                   strCost2 & "|" & _
                                   strCost3 & "|" & _
                                   strTransporter & "|" & strAmtTransportFee & "|" & _
                                   0 & "|" & _
                                   nPPNAmounTrans1 & "|" & _
                                   strTransporter2 & "|" & strAmtTransportFee2 & "|" & _
                                   0 & "|" & _
                                   nPPNAmountTrans2 & "|" & _
                                   strTransporter3 & "|" & strAmtTransportFee3 & "|" & _
                                   0 & "|" & _
                                   nPPNAmountTrans3 & "|" & _
                                   strDiscount1 & "|" & _
                                   strDiscount2 & "|" & _
                                   strDiscount3 & "|" & _
                                   strPPN221 & "|" & _
                                   strPPN222 & "|" & _
                                   strPPN223 & "|" & _
                                   strLocation & "|" & _
                                   lCDbl(strExRate) & "|" & _
                                   SupNote1 & "|" & _
                                   SupNote2 & "|" & _
                                   SupNote3 & "|" & _
                                   ""

                    Try
                        intErrNo = objGLtrx.mtdInsertDataCommon(strOpCd_AddRPHLn, _
                                                                strParamName, _
                                                                strParam)
                    Catch Exp As System.Exception
                        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ADD_DETAIL&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
                    End Try

                    bBoolean = True
                End If
            End If
        Next

        If bBoolean = True Then
            UserMsgBox(Me, "Generate Sucsess!!!")
            Response.Redirect("PU_trx_RPHDet.aspx?RPHID=" & lblPurReqID.Text)
        End If
    End Sub

    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PU_trx_RPHDet.aspx?RPHID=" & lblPurReqID.Text)
    End Sub

#End Region

#Region "LOCAL & PROCEDURE"
    Function lCDbl(ByVal pcVal As String) As Double
        If IsNumeric(pcVal) Then lCDbl = CDbl(pcVal) Else lCDbl = 0
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

    Sub BindPPNList()

        ddlPPN1.Items.Add(New ListItem("No", "0"))
        ddlPPN1.Items.Add(New ListItem("Yes", "1"))

        ddlPPN2.Items.Add(New ListItem("No", "0"))
        ddlPPN2.Items.Add(New ListItem("Yes", "1"))

        ddlPPN3.Items.Add(New ListItem("No", "0"))
        ddlPPN3.Items.Add(New ListItem("Yes", "1"))
    End Sub

    Sub onProcess_Load()
        onLoad_DisplayPRLn()
    End Sub

    Sub onLoad_DisplayPRLn()
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadData()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgPRLn.PageSize)

        dgPRLn.DataSource = dsData
        If dgPRLn.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgPRLn.CurrentPageIndex = 0
            Else
                dgPRLn.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgPRLn.DataBind()
        PageConTrol()

    End Sub

    Sub PageConTrol()
        For intCnt = 0 To dgPRLn.Items.Count - 1
            CType(dgPRLn.Items(intCnt).FindControl("lblNo"), Label).Text = intCnt + 1

            CType(dgPRLn.Items(intCnt).FindControl("txtQtyOrder1"), TextBox).BackColor = Drawing.Color.Yellow
            CType(dgPRLn.Items(intCnt).FindControl("txtQtyOrder2"), TextBox).BackColor = Drawing.Color.Gold
            CType(dgPRLn.Items(intCnt).FindControl("txtQtyOrder3"), TextBox).BackColor = Drawing.Color.LightGreen

            CType(dgPRLn.Items(intCnt).FindControl("txtSupNote1"), TextBox).BackColor = Drawing.Color.Yellow
            CType(dgPRLn.Items(intCnt).FindControl("txtSupNote2"), TextBox).BackColor = Drawing.Color.Gold
            CType(dgPRLn.Items(intCnt).FindControl("txtSupNote3"), TextBox).BackColor = Drawing.Color.LightGreen


            CType(dgPRLn.Items(intCnt).FindControl("txtCost1"), TextBox).BackColor = Drawing.Color.Yellow
            CType(dgPRLn.Items(intCnt).FindControl("txtCost2"), TextBox).BackColor = Drawing.Color.Gold
            CType(dgPRLn.Items(intCnt).FindControl("txtCost3"), TextBox).BackColor = Drawing.Color.LightGreen

            'dgPRLn.Items(intCnt).Cells(4).BackColor = Drawing.Color.Yellow
            'dgPRLn.Items(intCnt).Cells(5).BackColor = Drawing.Color.Yellow
            'dgPRLn.Items(intCnt).Cells(6).BackColor = Drawing.Color.Yellow

            'dgPRLn.Items(intCnt).Cells(7).BackColor = Drawing.Color.Gold
            'dgPRLn.Items(intCnt).Cells(8).BackColor = Drawing.Color.Gold
            'dgPRLn.Items(intCnt).Cells(9).BackColor = Drawing.Color.Gold

            'dgPRLn.Items(intCnt).Cells(10).BackColor = Drawing.Color.LightGreen
            'dgPRLn.Items(intCnt).Cells(11).BackColor = Drawing.Color.LightGreen
            'dgPRLn.Items(intCnt).Cells(12).BackColor = Drawing.Color.LightGreen
 

            Dim nQtyReq As Double = Replace(Replace(CType(dgPRLn.Items(intCnt).FindControl("lblQtyReqDisplay"), Label).Text.Trim, ".", ""), ",", ".")
            CType(dgPRLn.Items(intCnt).FindControl("lblQtyReqDisplay"), Label).Text = nQtyReq
            CType(dgPRLn.Items(intCnt).FindControl("txtQtyOrder1"), TextBox).Text = Replace(Replace(CType(dgPRLn.Items(intCnt).FindControl("lblQtyReqDisplay"), Label).Text.Trim, ".", ""), ",", ".")
            CType(dgPRLn.Items(intCnt).FindControl("txtQtyOrder2"), TextBox).Text = Replace(Replace(CType(dgPRLn.Items(intCnt).FindControl("lblQtyReqDisplay"), Label).Text.Trim, ".", ""), ",", ".")
 
            CType(dgPRLn.Items(intCnt).FindControl("txtQtyOrder3"), TextBox).Text = Replace(Replace(CType(dgPRLn.Items(intCnt).FindControl("lblQtyReqDisplay"), Label).Text.Trim, ".", ""), ",", ".")
            CType(dgPRLn.Items(intCnt).FindControl("lblPPN1"), Label).Text = Session("PPN1")
            CType(dgPRLn.Items(intCnt).FindControl("lblPPN2"), Label).Text = Session("PPN2")
            CType(dgPRLn.Items(intCnt).FindControl("lblPPN3"), Label).Text = Session("PPN3")

        Next

        ddlPPN1.SelectedIndex = IIf(hidPPN1.Value = "1", 1, 0)
        ddlPPN2.SelectedIndex = IIf(hidPPN2.Value = "1", 1, 0)
        ddlPPN3.SelectedIndex = IIf(hidPPN3.Value = "1", 1, 0)

    End Sub

    Protected Function LoadData() As DataSet
        Dim strOpCode As String = "PU_CLSTRX_PR_RPH_LINE_GET"
        Dim intErrNo As Integer
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim sSQLKriteria As String = "AND a.PRID='" & txtPRID_Plmph.Text & "'"

        strParamName = "LOCCODE|USERPO|STRSEARCH"
        strParamValue = "" & Trim(strLocation) & "|" & strUserId & "|" & sSQLKriteria

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                objItemDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_ITEM_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        Return objItemDs
    End Function

#End Region

    Sub GetLocCode(ByVal pv_strLocLevel As String, ByRef pv_strResult As String)
        Dim strOpCd_GetLocLevel As String = "ADMIN_CLSLOC_LOCATION_LIST_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim objLocCodeDs As New Object()

        strParam = " and a.loclevel = '" & pv_strLocLevel & "'"

        Try
            intErrNo = objAdminLoc.mtdGetDataLocCode(strOpCd_GetLocLevel, strParam, objLocCodeDs)
        Catch Exp As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADMIN_GET_LOCATION&errmesg=" & lblErrMessage.Text & "&redirect=admin/location/admin_location_locdet.aspx")
        End Try

        If objLocCodeDs.Tables(0).Rows.Count > 0 Then
            pv_strResult = objLocCodeDs.Tables(0).Rows(0).Item("LocCode")
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GRDET_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/PU_trx_GRList.aspx")
        End Try
    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

        For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = objLangCapDs.Tables(0).Rows(count).Item("TermCode").trim() Then
                If strLocType = objAdminLoc.EnumLocType.Mill Then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                Else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                End If
                Exit For
            End If
        Next
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
