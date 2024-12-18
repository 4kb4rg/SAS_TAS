
Imports System
Imports System.Data
Imports System.IO 
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Information
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction

Imports agri.Admin
Imports agri.HR
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.PWSystem.clsLangCap

Public Class PM_Trx_ProdSummary : Inherits Page

    Protected WithEvents lblCode As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblPleaseSelect As Label
    Protected WithEvents txtYear As TextBox
    Protected WithEvents ddlMonth As DropDownList
    Protected WithEvents ddlAsalTBS1 As DropDownList
    Protected WithEvents ddlAsalTBS2 As DropDownList
    Protected WithEvents ddlAsalTBS3 As DropDownList
    Protected WithEvents ddlAsalTBS4 As DropDownList
    Protected WithEvents ddlTerimaTBS1 As DropDownList
    Protected WithEvents ddlTerimaTBS2 As DropDownList
    Protected WithEvents ddlTerimaTBS3 As DropDownList
    Protected WithEvents ddlTerimaTBS4 As DropDownList

    Protected WithEvents Textbox1 As TextBox
    Protected WithEvents Textbox2 As TextBox
    Protected WithEvents Textbox3 As TextBox
    Protected WithEvents Textbox4 As TextBox
    Protected WithEvents Textbox5 As TextBox
    Protected WithEvents Textbox6 As TextBox
    Protected WithEvents Textbox7 As TextBox
    Protected WithEvents Textbox8 As TextBox
    Protected WithEvents Textbox9 As TextBox
    Protected WithEvents Textbox10 As TextBox
    Protected WithEvents Textbox11 As TextBox
    Protected WithEvents Textbox12 As TextBox
    Protected WithEvents Textbox13 As TextBox
    Protected WithEvents Textbox14 As TextBox
    Protected WithEvents Textbox15 As TextBox
    Protected WithEvents Textbox16 As TextBox
    Protected WithEvents Textbox17 As TextBox
    Protected WithEvents Textbox18 As TextBox
    Protected WithEvents Textbox19 As TextBox
    Protected WithEvents Textbox20 As TextBox
    Protected WithEvents Textbox21 As TextBox
    Protected WithEvents Textbox22 As TextBox
    Protected WithEvents Textbox23 As TextBox
    Protected WithEvents Textbox24 As TextBox
    Protected WithEvents Textbox25 As TextBox
    Protected WithEvents Textbox26 As TextBox
    Protected WithEvents Textbox27 As TextBox
    Protected WithEvents Textbox28 As TextBox
    Protected WithEvents Textbox29 As TextBox
    Protected WithEvents Textbox30 As TextBox
    Protected WithEvents Textbox31 As TextBox
    Protected WithEvents Textbox32 As TextBox
    Protected WithEvents Textbox33 As TextBox
    Protected WithEvents Textbox34 As TextBox
    Protected WithEvents Textbox35 As TextBox
    Protected WithEvents Textbox36 As TextBox
    Protected WithEvents Textbox37 As TextBox
    Protected WithEvents Textbox38 As TextBox
    Protected WithEvents Textbox39 As TextBox
    Protected WithEvents Textbox40 As TextBox
    Protected WithEvents Textbox41 As TextBox
    Protected WithEvents Textbox42 As TextBox
    Protected WithEvents Textbox43 As TextBox
    Protected WithEvents Textbox44 As TextBox
    Protected WithEvents Textbox45 As TextBox
    Protected WithEvents Textbox46 As TextBox
    Protected WithEvents Textbox47 As TextBox
    Protected WithEvents Textbox48 As TextBox
    Protected WithEvents Textbox49 As TextBox
    Protected WithEvents Textbox50 As TextBox
    Protected WithEvents Textbox51 As TextBox
    Protected WithEvents Textbox52 As TextBox
    Protected WithEvents Textbox53 As TextBox
    Protected WithEvents Textbox54 As TextBox
    Protected WithEvents Textbox55 As TextBox
    Protected WithEvents Textbox56 As TextBox
    Protected WithEvents Textbox57 As TextBox
    Protected WithEvents Textbox58 As TextBox
    Protected WithEvents Textbox59 As TextBox
    Protected WithEvents Textbox60 As TextBox
    Protected WithEvents Textbox61 As TextBox

    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents trxid As HtmlInputHidden
    Protected WithEvents trxlnid As HtmlInputHidden
    Protected WithEvents NewBtn As ImageButton
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrDup As Label
    Protected WithEvents lblErrPeriode As Label
    'Protected WithEvents lblErrTBSQty As Label
    'Protected WithEvents lblErrTBSRp As Label
    Protected WithEvents lblErrAwalCPO As Label
    Protected WithEvents lblErrNo31 As Label
    Protected WithEvents lblErrNo27 As Label
    Protected WithEvents lblErrProdPK As Label
    Protected WithEvents lblErrTitipCPO As Label
    Protected WithEvents lblErrTitipPK As Label
    Protected WithEvents lblErrNo15 As Label
    Protected WithEvents lblErrNo23 As Label
    Protected WithEvents lblErrNo16 As Label
    Protected WithEvents lblErrNo22 As Label
    Protected WithEvents lblErrAkhirCPO As Label
    Protected WithEvents lblErrAkhirPK As Label
    Protected WithEvents lblErrAsalTBS1 As Label
    Protected WithEvents lblErrAsalTBS2 As Label
    Protected WithEvents lblErrTerimaTBS1 As Label
    Protected WithEvents lblErrTerimaTBS2 As Label

    Protected WithEvents dgLineDet As DataGrid

    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objGLTrx As New agri.GL.ClsTrx()
    Dim objAdmin As New agri.Admin.clsUom()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objSys As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Protected objCBTrx As New agri.CB.clsTrx()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Protected objPU As New agri.PU.clsTrx()

    Dim objTaxDs As New Object()
    Dim objActDs As New Object()
    Dim objUOMDs As New Object()
    Dim objConfigDs As New Object()
    Dim objLangCapDs As New Object()

    Dim intConfigsetting As Integer
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intPMAR As Integer
    Dim strTrxID As String = ""
    Dim intStatus As Integer
    Dim intMaxLen As Integer = 0
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strAccountTag As String
    Dim strLocType As String
    Dim strAcceptDateFormat As String
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
        intPMAR = Session("SS_PMAR")
        strLocType = Session("SS_LOCTYPE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        intLevel = Session("SS_USRLEVEL")
        strSelAccMonth = Session("SS_SELACCMONTH")
        strSelAccYear = Session("SS_SELACCYEAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMDailyProduction), intPMAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            strTrxID = strSelAccYear 'Trim(IIf(Request.QueryString(strSelAccYear) <> "", Request.QueryString(strSelAccYear), Request.Form(strSelAccYear)))

            'to avoid double click, on aspx add this : UseSubmitBehavior="false"
            SaveBtn.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(SaveBtn).ToString())
            DelBtn.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(DelBtn).ToString())

            intStatus = CInt(lblHiddenSts.Text)
            lblErrPeriode.Visible = False
            'lblErrTBSQty.Visible = False
            'lblErrTBSRp.Visible = False
            'lblErrAwalCPO.Visible = False
            'lblErrNo31.Visible = False
            'lblErrNo27.Visible = False
            'lblErrProdPK.Visible = False
            'lblErrTitipCPO.Visible = False
            'lblErrTitipPK.Visible = False
            'lblErrNo15.Visible = False
            'lblErrNo23.Visible = False
            'lblErrNo16.Visible = False
            'lblErrNo22.Visible = False
            'lblErrAkhirCPO.Visible = False
            'lblErrAkhirPK.Visible = False
            'lblErrAsalTBS1.Visible = False
            'lblErrAsalTBS2.Visible = False
            'lblErrTerimaTBS1.Visible = False
            'lblErrTerimaTBS2.Visible = False

            If Not IsPostBack Then
                txtYear.Text = strSelAccYear
                ddlMonth.SelectedIndex = strSelAccMonth
                BindLocCode("", "", "", "")
                BindLocCodeTerima("", "", "", "")
                If strTrxID <> "" Then
                    onLoad_Display(strTrxID)
                End If
            End If
        End If
    End Sub

    Sub BindLocCode(ByVal pv_Loc1 As String, ByVal pv_Loc2 As String, ByVal pv_Loc3 As String, ByVal pv_Loc4 As String)
        Dim strOpCd As String = "ADMIN_CLSLOC_LOCATION_LIST_GET"
        Dim objToLocCodeDs As New Object()
        Dim strToLocCode As String
        Dim strParam As String = ""
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer
        Dim strParamName As String
        Dim strParamValue As String

        strToLocCode = IIf(Trim(pv_Loc1) = "", "", " AND A.LocCode = '" & Trim(pv_Loc1) & "' ")

        strParamName = "STRSEARCH"
        strParamValue = " AND A.LocLevel = '1' AND A.LocType = '1' " & strToLocCode

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objToLocCodeDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LOC&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_dalist.aspx")
        End Try

        For intCnt = 0 To objToLocCodeDs.Tables(0).Rows.Count - 1
            With objToLocCodeDs.Tables(0).Rows(intCnt)
                .Item("LocCode") = Trim(.Item("LocCode"))
                .Item("Description") = Trim(.Item("LocCode")) & " (" & Trim(.Item("Description")) & ")"
                If Trim(.Item("LocCode")) = Trim(pv_Loc1) Then intSelectedIndex = intCnt + 1
            End With
        Next intCnt

        dr = objToLocCodeDs.Tables(0).NewRow()
        dr("LocCode") = ""
        dr("Description") = lblPleaseSelect.Text & " location"
        objToLocCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlAsalTBS1.DataSource = objToLocCodeDs.Tables(0)
        ddlAsalTBS1.DataValueField = "LocCode"
        ddlAsalTBS1.DataTextField = "Description"
        ddlAsalTBS1.DataBind()
        ddlAsalTBS1.SelectedIndex = intSelectedIndex

        strToLocCode = ""
        intSelectedIndex = 0
        strToLocCode = IIf(Trim(pv_Loc2) = "", "", " AND A.LocCode = '" & Trim(pv_Loc2) & "' ")

        strParamName = "STRSEARCH"
        strParamValue = " AND A.LocLevel = '1' AND A.LocType = '1' " & strToLocCode

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objToLocCodeDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LOC&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_dalist.aspx")
        End Try

        For intCnt = 0 To objToLocCodeDs.Tables(0).Rows.Count - 1
            With objToLocCodeDs.Tables(0).Rows(intCnt)
                .Item("LocCode") = Trim(.Item("LocCode"))
                .Item("Description") = Trim(.Item("LocCode")) & " (" & Trim(.Item("Description")) & ")"
                If Trim(.Item("LocCode")) = Trim(pv_Loc2) Then intSelectedIndex = intCnt + 1
            End With
        Next intCnt

        dr = objToLocCodeDs.Tables(0).NewRow()
        dr("LocCode") = ""
        dr("Description") = lblPleaseSelect.Text & " location"
        objToLocCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlAsalTBS2.DataSource = objToLocCodeDs.Tables(0)
        ddlAsalTBS2.DataValueField = "LocCode"
        ddlAsalTBS2.DataTextField = "Description"
        ddlAsalTBS2.DataBind()
        ddlAsalTBS2.SelectedIndex = intSelectedIndex

        '--asal 3
        strToLocCode = ""
        intSelectedIndex = 0
        strToLocCode = IIf(Trim(pv_Loc3) = "", "", " AND A.LocCode = '" & Trim(pv_Loc3) & "' ")

        strParamName = "STRSEARCH"
        strParamValue = " AND A.LocLevel = '1' AND A.LocType = '1' " & strToLocCode

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objToLocCodeDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LOC&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_dalist.aspx")
        End Try

        For intCnt = 0 To objToLocCodeDs.Tables(0).Rows.Count - 1
            With objToLocCodeDs.Tables(0).Rows(intCnt)
                .Item("LocCode") = Trim(.Item("LocCode"))
                .Item("Description") = Trim(.Item("LocCode")) & " (" & Trim(.Item("Description")) & ")"
                If Trim(.Item("LocCode")) = Trim(pv_Loc3) Then intSelectedIndex = intCnt + 1
            End With
        Next intCnt

        dr = objToLocCodeDs.Tables(0).NewRow()
        dr("LocCode") = ""
        dr("Description") = lblPleaseSelect.Text & " location"
        objToLocCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlAsalTBS3.DataSource = objToLocCodeDs.Tables(0)
        ddlAsalTBS3.DataValueField = "LocCode"
        ddlAsalTBS3.DataTextField = "Description"
        ddlAsalTBS3.DataBind()
        ddlAsalTBS3.SelectedIndex = intSelectedIndex

        '--asal 4
        strToLocCode = ""
        intSelectedIndex = 0
        strToLocCode = IIf(Trim(pv_Loc4) = "", "", " AND A.LocCode = '" & Trim(pv_Loc4) & "' ")

        strParamName = "STRSEARCH"
        strParamValue = " AND A.LocLevel = '1' AND A.LocType = '1' " & strToLocCode

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objToLocCodeDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LOC&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_dalist.aspx")
        End Try

        For intCnt = 0 To objToLocCodeDs.Tables(0).Rows.Count - 1
            With objToLocCodeDs.Tables(0).Rows(intCnt)
                .Item("LocCode") = Trim(.Item("LocCode"))
                .Item("Description") = Trim(.Item("LocCode")) & " (" & Trim(.Item("Description")) & ")"
                If Trim(.Item("LocCode")) = Trim(pv_Loc4) Then intSelectedIndex = intCnt + 1
            End With
        Next intCnt

        dr = objToLocCodeDs.Tables(0).NewRow()
        dr("LocCode") = ""
        dr("Description") = lblPleaseSelect.Text & " location"
        objToLocCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlAsalTBS4.DataSource = objToLocCodeDs.Tables(0)
        ddlAsalTBS4.DataValueField = "LocCode"
        ddlAsalTBS4.DataTextField = "Description"
        ddlAsalTBS4.DataBind()
        ddlAsalTBS4.SelectedIndex = intSelectedIndex
    End Sub

    Sub BindLocCodeTerima(ByVal pv_Loc1 As String, ByVal pv_Loc2 As String, ByVal pv_Loc3 As String, ByVal pv_Loc4 As String)
        Dim strOpCd As String = "ADMIN_CLSLOC_LOCATION_LIST_GET"
        Dim objToLocCodeDs As New Object()
        Dim strToLocCode As String
        Dim strParam As String = ""
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer
        Dim strParamName As String
        Dim strParamValue As String

        strToLocCode = IIf(Trim(pv_Loc1) = "", "", " AND A.LocCode = '" & Trim(pv_Loc1) & "' ")

        strParamName = "STRSEARCH"
        strParamValue = " AND A.LocLevel = '1' AND A.LocType = '1' " & strToLocCode

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objToLocCodeDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LOC&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_dalist.aspx")
        End Try

        For intCnt = 0 To objToLocCodeDs.Tables(0).Rows.Count - 1
            With objToLocCodeDs.Tables(0).Rows(intCnt)
                .Item("LocCode") = Trim(.Item("LocCode"))
                .Item("Description") = Trim(.Item("LocCode")) & " (" & Trim(.Item("Description")) & ")"
                If Trim(.Item("LocCode")) = Trim(pv_Loc1) Then intSelectedIndex = intCnt + 1
            End With
        Next intCnt

        dr = objToLocCodeDs.Tables(0).NewRow()
        dr("LocCode") = ""
        dr("Description") = lblPleaseSelect.Text & " location"
        objToLocCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlTerimaTBS1.DataSource = objToLocCodeDs.Tables(0)
        ddlTerimaTBS1.DataValueField = "LocCode"
        ddlTerimaTBS1.DataTextField = "Description"
        ddlTerimaTBS1.DataBind()
        ddlTerimaTBS1.SelectedIndex = intSelectedIndex

        strToLocCode = ""
        intSelectedIndex = 0
        strToLocCode = IIf(Trim(pv_Loc2) = "", "", " AND A.LocCode = '" & Trim(pv_Loc2) & "' ")

        strParamName = "STRSEARCH"
        strParamValue = " AND A.LocLevel = '1' AND A.LocType = '1' " & strToLocCode

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objToLocCodeDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LOC&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_dalist.aspx")
        End Try

        For intCnt = 0 To objToLocCodeDs.Tables(0).Rows.Count - 1
            With objToLocCodeDs.Tables(0).Rows(intCnt)
                .Item("LocCode") = Trim(.Item("LocCode"))
                .Item("Description") = Trim(.Item("LocCode")) & " (" & Trim(.Item("Description")) & ")"
                If Trim(.Item("LocCode")) = Trim(pv_Loc2) Then intSelectedIndex = intCnt + 1
            End With
        Next intCnt

        dr = objToLocCodeDs.Tables(0).NewRow()
        dr("LocCode") = ""
        dr("Description") = lblPleaseSelect.Text & " location"
        objToLocCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlTerimaTBS2.DataSource = objToLocCodeDs.Tables(0)
        ddlTerimaTBS2.DataValueField = "LocCode"
        ddlTerimaTBS2.DataTextField = "Description"
        ddlTerimaTBS2.DataBind()
        ddlTerimaTBS2.SelectedIndex = intSelectedIndex

        strToLocCode = ""
        intSelectedIndex = 0
        strToLocCode = IIf(Trim(pv_Loc3) = "", "", " AND A.LocCode = '" & Trim(pv_Loc3) & "' ")

        strParamName = "STRSEARCH"
        strParamValue = " AND A.LocLevel = '1' AND A.LocType = '1' " & strToLocCode

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objToLocCodeDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LOC&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_dalist.aspx")
        End Try

        For intCnt = 0 To objToLocCodeDs.Tables(0).Rows.Count - 1
            With objToLocCodeDs.Tables(0).Rows(intCnt)
                .Item("LocCode") = Trim(.Item("LocCode"))
                .Item("Description") = Trim(.Item("LocCode")) & " (" & Trim(.Item("Description")) & ")"
                If Trim(.Item("LocCode")) = Trim(pv_Loc3) Then intSelectedIndex = intCnt + 1
            End With
        Next intCnt

        dr = objToLocCodeDs.Tables(0).NewRow()
        dr("LocCode") = ""
        dr("Description") = lblPleaseSelect.Text & " location"
        objToLocCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlTerimaTBS3.DataSource = objToLocCodeDs.Tables(0)
        ddlTerimaTBS3.DataValueField = "LocCode"
        ddlTerimaTBS3.DataTextField = "Description"
        ddlTerimaTBS3.DataBind()
        ddlTerimaTBS3.SelectedIndex = intSelectedIndex


        strToLocCode = ""
        intSelectedIndex = 0
        strToLocCode = IIf(Trim(pv_Loc4) = "", "", " AND A.LocCode = '" & Trim(pv_Loc4) & "' ")

        strParamName = "STRSEARCH"
        strParamValue = " AND A.LocLevel = '1' AND A.LocType = '1' " & strToLocCode

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objToLocCodeDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LOC&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_dalist.aspx")
        End Try

        For intCnt = 0 To objToLocCodeDs.Tables(0).Rows.Count - 1
            With objToLocCodeDs.Tables(0).Rows(intCnt)
                .Item("LocCode") = Trim(.Item("LocCode"))
                .Item("Description") = Trim(.Item("LocCode")) & " (" & Trim(.Item("Description")) & ")"
                If Trim(.Item("LocCode")) = Trim(pv_Loc4) Then intSelectedIndex = intCnt + 1
            End With
        Next intCnt

        dr = objToLocCodeDs.Tables(0).NewRow()
        dr("LocCode") = ""
        dr("Description") = lblPleaseSelect.Text & " location"
        objToLocCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlTerimaTBS4.DataSource = objToLocCodeDs.Tables(0)
        ddlTerimaTBS4.DataValueField = "LocCode"
        ddlTerimaTBS4.DataTextField = "Description"
        ddlTerimaTBS4.DataBind()
        ddlTerimaTBS4.SelectedIndex = intSelectedIndex
    End Sub

    Sub onLoad_Display(ByVal pv_strTrxID As String)
        Dim strOpCd As String = "PM_CLSTRX_PRODSUMMARY_LIST_GET"
        Dim intErrNo As Integer
        Dim strParamName As String
        Dim strParamValue As String
        Dim intCnt As Integer = 0
        Dim lbl As Label
        Dim strRowID As Integer

		strAccYear = txtYear.Text
        strParamName = "LOCCODE|ACCYEAR|ACCMONTH|USERID"
        strParamValue = strLocation & "|" & strAccYear & "|" & Trim(ddlMonth.SelectedItem.Value) & "|" & Trim(strUserId)

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objTaxDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objTaxDs.Tables(0).Rows.Count > 0 Then
            dgLineDet.DataSource = objTaxDs.Tables(0)
            dgLineDet.DataBind()

            intStatus = CInt(Trim(objTaxDs.Tables(0).Rows(0).Item("Status")))
            lblHiddenSts.Text = Trim(objTaxDs.Tables(0).Rows(0).Item("Status"))
            lblStatus.Text = objGLTrx.mtdGetLeaseFinanceStatus(Trim(objTaxDs.Tables(0).Rows(0).Item("Status")))
            lblDateCreated.Text = objGlobal.GetLongDate(objTaxDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objTaxDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = Trim(objTaxDs.Tables(0).Rows(0).Item("UserName"))
            txtYear.Enabled = False

            For intCnt = 0 To objTaxDs.Tables(0).Rows.Count - 1
                'lbl = dgLineDet.Items.Item(intCnt).FindControl("lblRowID")
                'If lbl.Text = "99" Then
                '    lbl.Visible = False
                '    lbl = dgLineDet.Items.Item(intCnt).FindControl("lblDescription")
                '    lbl.Font.Bold = True
                '    lbl = dgLineDet.Items.Item(intCnt).FindControl("lblBln1")
                '    lbl.Visible = False
                '    lbl = dgLineDet.Items.Item(intCnt).FindControl("lblBln2")
                '    lbl.Visible = False
                '    lbl = dgLineDet.Items.Item(intCnt).FindControl("lblBln3")
                '    lbl.Visible = False
                '    lbl = dgLineDet.Items.Item(intCnt).FindControl("lblBln4")
                '    lbl.Visible = False
                '    lbl = dgLineDet.Items.Item(intCnt).FindControl("lblBln5")
                '    lbl.Visible = False
                '    lbl = dgLineDet.Items.Item(intCnt).FindControl("lblBln6")
                '    lbl.Visible = False
                '    lbl = dgLineDet.Items.Item(intCnt).FindControl("lblBln7")
                '    lbl.Visible = False
                '    lbl = dgLineDet.Items.Item(intCnt).FindControl("lblBln8")
                '    lbl.Visible = False
                '    lbl = dgLineDet.Items.Item(intCnt).FindControl("lblBln9")
                '    lbl.Visible = False
                '    lbl = dgLineDet.Items.Item(intCnt).FindControl("lblBln10")
                '    lbl.Visible = False
                '    lbl = dgLineDet.Items.Item(intCnt).FindControl("lblBln11")
                '    lbl.Visible = False
                '    lbl = dgLineDet.Items.Item(intCnt).FindControl("lblBln12")
                '    lbl.Visible = False
                '    lbl = dgLineDet.Items.Item(intCnt).FindControl("lblTotal")
                '    lbl.Font.Bold = True
                '    dgLineDet.Items.Item(intCnt).BackColor = Drawing.Color.lightblue
                'End If
                'If lbl.Text = "999" Then
                '    lbl.Visible = False
                '    lbl = dgLineDet.Items.Item(intCnt).FindControl("lblDescription")
                '    lbl.Font.Bold = True
                '    lbl = dgLineDet.Items.Item(intCnt).FindControl("lblBln1")
                '    lbl.Visible = False
                '    lbl = dgLineDet.Items.Item(intCnt).FindControl("lblBln2")
                '    lbl.Visible = False
                '    lbl = dgLineDet.Items.Item(intCnt).FindControl("lblBln3")
                '    lbl.Visible = False
                '    lbl = dgLineDet.Items.Item(intCnt).FindControl("lblBln4")
                '    lbl.Visible = False
                '    lbl = dgLineDet.Items.Item(intCnt).FindControl("lblBln5")
                '    lbl.Visible = False
                '    lbl = dgLineDet.Items.Item(intCnt).FindControl("lblBln6")
                '    lbl.Visible = False
                '    lbl = dgLineDet.Items.Item(intCnt).FindControl("lblBln7")
                '    lbl.Visible = False
                '    lbl = dgLineDet.Items.Item(intCnt).FindControl("lblBln8")
                '    lbl.Visible = False
                '    lbl = dgLineDet.Items.Item(intCnt).FindControl("lblBln9")
                '    lbl.Visible = False
                '    lbl = dgLineDet.Items.Item(intCnt).FindControl("lblBln10")
                '    lbl.Visible = False
                '    lbl = dgLineDet.Items.Item(intCnt).FindControl("lblBln11")
                '    lbl.Visible = False
                '    lbl = dgLineDet.Items.Item(intCnt).FindControl("lblBln12")
                '    lbl.Visible = False
                '    lbl = dgLineDet.Items.Item(intCnt).FindControl("lblTotal")
                '    lbl.Font.Bold = True
                '    dgLineDet.Items.Item(intCnt).BackColor = Drawing.Color.lightblue
                'End If

                'lbl = dgLineDet.Items.Item(intCnt).FindControl("lblRowID")
                'strRowID = lbl.Text
                'If lbl.Text = "2" Or lbl.Text = "6" Or lbl.Text = "11" Or lbl.Text = "16" Then
                lbl = dgLineDet.Items.Item(intCnt).FindControl("lblDescription")

                If Left(Trim(lbl.Text), 1) = "1" Then

                    'Select Case strRowID
                    '    Case 2
                    '        lbl.Text = "TBS"
                    '    Case 6
                    '        lbl.Text = "CPO"
                    '    Case 11
                    '        lbl.Text = "PK"
                    '    Case Else
                    '        lbl.Text = ""
                    'End Select
                    lbl.Text = Replace(Trim(lbl.Text), "1", "")
                    lbl.Font.Bold = True

                    lbl = dgLineDet.Items.Item(intCnt).FindControl("lblRowID")
                    lbl.Visible = False
                    lbl = dgLineDet.Items.Item(intCnt).FindControl("lblBln1")
                    lbl.Visible = False
                    lbl = dgLineDet.Items.Item(intCnt).FindControl("lblBln2")
                    lbl.Visible = False
                    lbl = dgLineDet.Items.Item(intCnt).FindControl("lblBln3")
                    lbl.Visible = False
                    lbl = dgLineDet.Items.Item(intCnt).FindControl("lblBln4")
                    lbl.Visible = False
                    lbl = dgLineDet.Items.Item(intCnt).FindControl("lblBln5")
                    lbl.Visible = False
                    lbl = dgLineDet.Items.Item(intCnt).FindControl("lblBln6")
                    lbl.Visible = False
                    lbl = dgLineDet.Items.Item(intCnt).FindControl("lblBln7")
                    lbl.Visible = False
                    lbl = dgLineDet.Items.Item(intCnt).FindControl("lblBln8")
                    lbl.Visible = False
                    lbl = dgLineDet.Items.Item(intCnt).FindControl("lblBln9")
                    lbl.Visible = False
                    lbl = dgLineDet.Items.Item(intCnt).FindControl("lblBln10")
                    lbl.Visible = False
                    lbl = dgLineDet.Items.Item(intCnt).FindControl("lblBln11")
                    lbl.Visible = False
                    lbl = dgLineDet.Items.Item(intCnt).FindControl("lblBln12")
                    lbl.Visible = False
                    lbl = dgLineDet.Items.Item(intCnt).FindControl("lblTotal")
                    lbl.Visible = False
                End If
                'End If
            Next

            GetDataDetail(ddlMonth.SelectedItem.Value)
        End If
    End Sub

    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "PM_CLSTRX_PRODSUMMARY_UPDATE"
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim intErrNo As Integer
        Dim blnIsUpdate As Boolean
        Dim strParamName As String
        Dim strParamValue As String
        Dim intCnt As Integer

        strAccYear = Trim(txtYear.Text)
        strAccMonth = ddlMonth.SelectedItem.Value
        intCnt = 1

        'If Textbox1.Text <> "" Or Textbox1.Text <> "0" Or CDbl(Textbox1.Text) <> 0 Then
        '    If ddlAsalTBS1.SelectedItem.Value = 0 Then
        '        lblErrAsalTBS1.Visible = True
        '        Exit Sub
        '    End If
        'End If
        'If Textbox6.Text <> "" Or Textbox6.Text <> "0" Or CDbl(Textbox6.Text) <> 0 Then
        '    If ddlAsalTBS2.SelectedItem.Value = 0 Then
        '        lblErrAsalTBS2.Visible = True
        '        Exit Sub
        '    End If
        'End If
        'If Textbox12.Text <> "" Or Textbox12.Text <> "0" Or CDbl(Textbox12.Text) <> 0 Then
        '    If ddlTerimaTBS1.SelectedItem.Value = 0 Then
        '        lblErrTerimaTBS1.Visible = True
        '        Exit Sub
        '    End If
        'End If
        'If Textbox13.Text <> "" Or Textbox13.Text <> "0" Or CDbl(Textbox13.Text) <> 0 Then
        '    If ddlTerimaTBS2.SelectedItem.Value = 0 Then
        '        lblErrTerimaTBS2.Visible = True
        '        Exit Sub
        '    End If
        'End If

        Dim lblDescr As Label
        Dim lblRefNo As Label
        If strCmdArgs = "Save" Then
            For intCnt = 1 To 63 'baca rowid
                lblDescr = Form.FindControl("LabelDescr" & Trim(intCnt))
                lblRefNo = Form.FindControl("lblRefNo" & Trim(intCnt))
                Select Case intCnt
                    '----pengiriman tbs
                    Case 1, 2
                        lblRefNo.Text = Trim(ddlAsalTBS1.SelectedItem.Value)
                    Case 3, 4
                        lblRefNo.Text = Trim(ddlAsalTBS2.SelectedItem.Value)
                    Case 5, 6
                        lblRefNo.Text = Trim(ddlAsalTBS3.SelectedItem.Value)
                    Case 7, 8
                        lblRefNo.Text = Trim(ddlAsalTBS4.SelectedItem.Value)

                    Case 9, 10, 11
                        If intCnt = 9 Then
                            lblDescr.Text = "Pihak Ketiga " & IIf(Trim(Textbox9.Text) = "", "-", Trim(Textbox9.Text))
                        End If
                    Case 12, 13, 14
                        If intCnt = 12 Then
                            lblDescr.Text = "Pihak Ketiga " & IIf(Trim(Textbox12.Text) = "", "-", Trim(Textbox12.Text))
                        End If

                        '----penerimaan tbs
                    Case 16
                        lblDescr.Text = "Terima TBS 1 (Kg) " & Trim(ddlTerimaTBS1.SelectedItem.Value)
                    Case 17
                        lblDescr.Text = "Terima TBS 2 (Kg) " & Trim(ddlTerimaTBS2.SelectedItem.Value)
                    Case 18
                        lblDescr.Text = "Terima TBS 3 (Kg) " & Trim(ddlTerimaTBS3.SelectedItem.Value)
                    Case 19
                        lblDescr.Text = "Terima TBS 4 (Kg) " & Trim(ddlTerimaTBS4.SelectedItem.Value)

                    Case 20
                        lblDescr.Text = "Terima TBS Luar 1 (Kg) " & IIf(Trim(Textbox21.Text) = "", "", Trim(Textbox21.Text))
                    Case 21
                        Continue For
                    Case 22
                        lblDescr.Text = "Terima TBS Luar 2 (Kg) " & IIf(Trim(Textbox23.Text) = "", "", Trim(Textbox23.Text))
                    Case 23
                        Continue For

                    Case 40, 41, 42, 43, 44
                        lblRefNo.Text = "CPO" & " 3RD PARTI I " & IIf(Trim(Textbox9.Text) = "", "", Trim(Textbox9.Text))
                    Case 45, 46, 47, 48, 49
                        lblRefNo.Text = "PK" & " 3RD PARTI I " & IIf(Trim(Textbox9.Text) = "", "", Trim(Textbox9.Text)) 'Replace(lblRefNo.Text, Trim(Textbox3.Text), "") & " 3RD PARTI I " & Trim(Textbox3.Text)
                    Case 50, 51, 52, 53, 54
                        lblRefNo.Text = "CPO" & " BULKING" '" 3RD PARTI II " & IIf(Trim(Textbox8.Text) = "", "", Trim(Textbox8.Text))
                    Case 55, 56, 57, 58, 59
                        lblRefNo.Text = "PK" & " BULKING" '" 3RD PARTI II " & IIf(Trim(Textbox8.Text) = "", "", Trim(Textbox8.Text)) 'Replace(lblRefNo.Text, Trim(Textbox8.Text), "") & " 3RD PARTI II " & Trim(Textbox8.Text)
                End Select

                strParamName = "LOCCODE|ACCYEAR|ACCMONTH|REFNO|ROWID|DESCRIPTION|PERIODE|PERIODEVALUE|STATUS|UPDATEID"
                Select Case intCnt
                    Case 9, 12, 21, 23
                        strParamValue = strLocation & "|" & _
                                                        strAccYear & "|" & _
                                                        strAccMonth & "|" & _
                                                        Trim(lblRefNo.Text) & "|" & _
                                                        intCnt & "|" & _
                                                        UCase(Trim(lblDescr.Text)) & "|" & _
                                                        "Bln" & Trim(ddlMonth.SelectedItem.Value) & "|" & _
                                                        0 & "|" & _
                                                        "1" & "|" & _
                                                        Trim(strUserId)
                    Case Else
                        strParamValue = strLocation & "|" & _
                                                        strAccYear & "|" & _
                                                        strAccMonth & "|" & _
                                                        Trim(lblRefNo.Text) & "|" & _
                                                        intCnt & "|" & _
                                                        UCase(Trim(lblDescr.Text)) & "|" & _
                                                        "Bln" & Trim(ddlMonth.SelectedItem.Value) & "|" & _
                                                        Request.Form("Textbox" & Trim(intCnt) & "") & "|" & _
                                                        "1" & "|" & _
                                                        Trim(strUserId)
                End Select

                Try
                    intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd_Upd, strParamName, strParamValue)

                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
                End Try
            Next


        ElseIf strCmdArgs = "Del" Then
            For intCnt = 1 To 63 'baca rowid
                Select Case intCnt
                    Case 3
                    Case 7
                    Case 12
                    Case Else
                        lblDescr = Form.FindControl("LabelDescr" & Trim(intCnt))
                        lblRefNo = Form.FindControl("lblRefNo" & Trim(intCnt))

                        If intCnt = 1 Or intCnt = 2 Then
                            lblRefNo.Text = Trim(ddlAsalTBS1.SelectedItem.Value)
                        End If
                        If intCnt = 3 Or intCnt = 4 Then
                            lblRefNo.Text = Trim(ddlAsalTBS2.SelectedItem.Value)
                        End If

                        strParamName = "LOCCODE|ACCYEAR|ACCMONTH|REFNO|ROWID|DESCRIPTION|PERIODE|PERIODEVALUE|STATUS|UPDATEID"
                        strParamValue = strLocation & "|" & _
                                        strAccYear & "|" & _
                                        strAccMonth & "|" & _
                                        Trim(lblRefNo.Text) & "|" & _
                                        intCnt & "|" & _
                                        Trim(lblDescr.Text) & "|" & _
                                        "Bln" & Trim(ddlMonth.SelectedItem.Value) & "|" & _
                                        0 & "|" & _
                                        "1" & "|" & _
                                        Trim(strUserId)

                        Try
                            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd_Upd, strParamName, strParamValue)

                        Catch Exp As System.Exception
                            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
                        End Try
                End Select
            Next

        End If


        onLoad_Display(txtYear.Text)
    End Sub

    Sub GetDataDetail(ByVal pv_AccMonth As String)
        Dim strOpCd As String = "PM_CLSTRX_PRODSUMMARY_LIST_GET_DETAIL"
        Dim intErrNo As Integer
        Dim strParamName As String
        Dim strParamValue As String
        Dim intCnt As Integer = 0

        strAccYear = txtYear.Text
        strParamName = "LOCCODE|ACCYEAR|ACCMONTH"
        strParamValue = strLocation & "|" & strAccYear & "|" & Trim(pv_AccMonth)

        If pv_AccMonth <> 0 Then

            Try
                intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                    strParamName, _
                                                    strParamValue, _
                                                    objTaxDs)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try

            If objTaxDs.Tables(0).Rows.Count > 0 Then
                Textbox1.Text = objTaxDs.Tables(0).Rows(0).Item("Amount1")
                Textbox2.Text = objTaxDs.Tables(0).Rows(0).Item("Amount2")
                Textbox3.Text = objTaxDs.Tables(0).Rows(0).Item("Amount3")
                Textbox4.Text = objTaxDs.Tables(0).Rows(0).Item("Amount4")
                Textbox5.Text = objTaxDs.Tables(0).Rows(0).Item("Amount5")
                Textbox6.Text = objTaxDs.Tables(0).Rows(0).Item("Amount6")
                Textbox7.Text = objTaxDs.Tables(0).Rows(0).Item("Amount7")
                Textbox8.Text = objTaxDs.Tables(0).Rows(0).Item("Amount8")
                Textbox9.Text = objTaxDs.Tables(0).Rows(0).Item("PMKS1")
                Textbox10.Text = objTaxDs.Tables(0).Rows(0).Item("Amount10")
                Textbox11.Text = objTaxDs.Tables(0).Rows(0).Item("Amount11")
                Textbox12.Text = objTaxDs.Tables(0).Rows(0).Item("PMKS2")
                Textbox13.Text = objTaxDs.Tables(0).Rows(0).Item("Amount13")
                Textbox14.Text = objTaxDs.Tables(0).Rows(0).Item("Amount14")
                Textbox15.Text = objTaxDs.Tables(0).Rows(0).Item("Amount15")
                Textbox16.Text = objTaxDs.Tables(0).Rows(0).Item("Amount16")
                Textbox17.Text = objTaxDs.Tables(0).Rows(0).Item("Amount17")
                Textbox18.Text = objTaxDs.Tables(0).Rows(0).Item("Amount18")
                Textbox19.Text = objTaxDs.Tables(0).Rows(0).Item("Amount19")
                Textbox20.Text = objTaxDs.Tables(0).Rows(0).Item("Amount20")
                'Textbox21.Text = objTaxDs.Tables(0).Rows(0).Item("Amount21")
                Textbox22.Text = objTaxDs.Tables(0).Rows(0).Item("Amount22")
                'Textbox23.Text = objTaxDs.Tables(0).Rows(0).Item("Amount23")
                Textbox24.Text = objTaxDs.Tables(0).Rows(0).Item("Amount24")
                Textbox25.Text = objTaxDs.Tables(0).Rows(0).Item("Amount25")
                Textbox26.Text = objTaxDs.Tables(0).Rows(0).Item("Amount26")
                Textbox27.Text = objTaxDs.Tables(0).Rows(0).Item("Amount27")
                Textbox28.Text = objTaxDs.Tables(0).Rows(0).Item("Amount28")
                Textbox29.Text = objTaxDs.Tables(0).Rows(0).Item("Amount29")
                Textbox30.Text = objTaxDs.Tables(0).Rows(0).Item("Amount30")
                Textbox31.Text = objTaxDs.Tables(0).Rows(0).Item("Amount31")
                Textbox32.Text = objTaxDs.Tables(0).Rows(0).Item("Amount32")
                Textbox33.Text = objTaxDs.Tables(0).Rows(0).Item("Amount33")
                Textbox34.Text = objTaxDs.Tables(0).Rows(0).Item("Amount34")
                Textbox35.Text = objTaxDs.Tables(0).Rows(0).Item("Amount35")
                Textbox36.Text = objTaxDs.Tables(0).Rows(0).Item("Amount36")
                Textbox37.Text = objTaxDs.Tables(0).Rows(0).Item("Amount37")
                Textbox38.Text = objTaxDs.Tables(0).Rows(0).Item("Amount38")
                Textbox39.Text = objTaxDs.Tables(0).Rows(0).Item("Amount39")
                Textbox40.Text = objTaxDs.Tables(0).Rows(0).Item("Amount40")
                Textbox41.Text = objTaxDs.Tables(0).Rows(0).Item("Amount41")
                Textbox42.Text = objTaxDs.Tables(0).Rows(0).Item("Amount42")
                Textbox43.Text = objTaxDs.Tables(0).Rows(0).Item("Amount43")
                Textbox44.Text = objTaxDs.Tables(0).Rows(0).Item("Amount44")
                Textbox45.Text = objTaxDs.Tables(0).Rows(0).Item("Amount45")
                Textbox46.Text = objTaxDs.Tables(0).Rows(0).Item("Amount46")
                Textbox47.Text = objTaxDs.Tables(0).Rows(0).Item("Amount47")
                Textbox48.Text = objTaxDs.Tables(0).Rows(0).Item("Amount48")
                Textbox49.Text = objTaxDs.Tables(0).Rows(0).Item("Amount49")
                Textbox50.Text = objTaxDs.Tables(0).Rows(0).Item("Amount50")
                Textbox51.Text = objTaxDs.Tables(0).Rows(0).Item("Amount51")
                Textbox52.Text = objTaxDs.Tables(0).Rows(0).Item("Amount52")
                Textbox53.Text = objTaxDs.Tables(0).Rows(0).Item("Amount53")
                Textbox54.Text = objTaxDs.Tables(0).Rows(0).Item("Amount54")
                Textbox55.Text = objTaxDs.Tables(0).Rows(0).Item("Amount55")
                Textbox56.Text = objTaxDs.Tables(0).Rows(0).Item("Amount56")
                Textbox57.Text = objTaxDs.Tables(0).Rows(0).Item("Amount57")
                Textbox58.Text = objTaxDs.Tables(0).Rows(0).Item("Amount58")
                Textbox59.Text = objTaxDs.Tables(0).Rows(0).Item("Amount59")
                Textbox60.Text = objTaxDs.Tables(0).Rows(0).Item("Amount60")
                Textbox61.Text = objTaxDs.Tables(0).Rows(0).Item("Amount61")

                BindLocCode(objTaxDs.Tables(0).Rows(0).Item("RefNo1"), objTaxDs.Tables(0).Rows(0).Item("RefNo2"), objTaxDs.Tables(0).Rows(0).Item("RefNo3"), objTaxDs.Tables(0).Rows(0).Item("RefNo4"))
                BindLocCodeTerima(objTaxDs.Tables(0).Rows(0).Item("TBSTerima1"), objTaxDs.Tables(0).Rows(0).Item("TBSTerima2"), objTaxDs.Tables(0).Rows(0).Item("TBSTerima3"), objTaxDs.Tables(0).Rows(0).Item("TBSTerima4"))
                
                If Cdbl(Textbox15.Text) = 0 Then
                    'mulai periode prodsummary
                    Textbox15.ReadOnly = False
                    Textbox26.ReadOnly = False
                    Textbox32.ReadOnly = False
                    Textbox38.ReadOnly = False
                    Textbox43.ReadOnly = False
                    Textbox48.ReadOnly = False
                    Textbox53.ReadOnly = False
                    Textbox58.ReadOnly = False
                    Textbox60.ReadOnly = False
                Else    
                    Textbox15.ReadOnly = True
                    Textbox26.ReadOnly = True
                    Textbox32.ReadOnly = True
                    Textbox38.ReadOnly = True
                    Textbox43.ReadOnly = True
                    Textbox48.ReadOnly = True
                    'Textbox53.ReadOnly = True
                End If
            Else
                Textbox15.ReadOnly = False
                Textbox26.ReadOnly = False
                Textbox32.ReadOnly = False
                Textbox38.ReadOnly = False
                Textbox43.ReadOnly = False
                Textbox48.ReadOnly = False
                Textbox53.ReadOnly = False
                Textbox58.ReadOnly = False
                Textbox60.ReadOnly = False
            End If
        End If
    End Sub

    Sub GetData(ByVal sender As System.Object, ByVal e As System.EventArgs)
        onLoad_Display(strTrxID)
        GetDataDetail(ddlMonth.SelectedItem.Value)
    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        strAccountTag = GetCaption(objLangCap.EnumLangCap.Account) & lblCode.Text
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_TRX_JOURNAL_DETAIL_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=CB/CB_trx_CashBankDet.aspx")
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

    Private Sub dgLineDet_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgLineDet.ItemCreated
        If e.Item.ItemType = ListItemType.Header Then
            Dim dgItem As DataGridItem
            Dim dgCell As TableCell
            dgItem = New DataGridItem(0, 0, ListItemType.Header)

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "NO."
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "DESCRIPTION"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "TOTAL"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 12
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "PERIODE"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgLineDet.Controls(0).Controls.AddAt(0, dgItem)
        End If
    End Sub

    Private Sub dgLineDet_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgLineDet.ItemDataBound
        If e.Item.ItemType = ListItemType.Header Then
            Dim dgItem As DataGridItem

            dgItem = New DataGridItem(0, 0, ListItemType.Header)

            e.Item.Cells(0).Visible = False
            e.Item.Cells(1).Visible = False
            e.Item.Cells(2).Visible = False
        End If
    End Sub

    Sub Preview_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=ProductionSummary-" & Trim(txtYear.text) & ".xls")
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.xls"

        Dim stringWrite = New System.IO.StringWriter()
        Dim htmlWrite = New HtmlTextWriter(stringWrite)

        dgLineDet.RenderControl(htmlWrite)
        Response.Write(stringWrite.ToString())
        Response.End()
    End Sub

    Sub dgLine_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue'")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")
            End If
        End If
    End Sub

End Class
