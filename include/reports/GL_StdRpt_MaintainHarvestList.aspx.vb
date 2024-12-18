Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports System.Xml
Imports System.Web.Services

Public Class GL_StdRpt_MaintainHarvestList : Inherits Page

    Protected RptSelect As UserControl

    Dim objGL As New agri.GL.clsReport()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Protected WithEvents hidActGrpCode As HtmlInputHidden

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblActGrp As Label
    Protected WithEvents lblTracker As Label

    Protected WithEvents lblBlkType As Label
    Protected WithEvents lblBlkGrp As Label
    Protected WithEvents lblBlkCode As Label
    Protected WithEvents lblSubBlkCode As Label
    Protected WithEvents lblActGrpCode As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents lblHidCostIsBlock As Label
    Protected WithEvents lblHidActGrpCode As Label
    Protected WithEvents lblHidSubBlkCode As Label
    Protected WithEvents lblHidBlkCode As Label
    Protected WithEvents lblHidBlkGrpCode As Label
    Protected WithEvents lblHidSubBlk As Label
    Protected WithEvents lblHidBlk As Label
    Protected WithEvents lblCode As Label

    Protected WithEvents lstBlkType As DropDownList
    Protected WithEvents txtBlkGrp As TextBox
    Protected WithEvents txtBlkCode As TextBox
    Protected WithEvents txtSubBlkCode As TextBox

    Protected WithEvents cblActGrp As CheckBoxList
    Protected WithEvents cbActGrpAll As CheckBox
    Protected WithEvents rbSumYes As RadioButton
    Protected WithEvents rbSumNo As RadioButton
    Protected WithEvents rbSuppYes As RadioButton
    Protected WithEvents rbSuppNo As RadioButton

    Protected WithEvents TrBlkGrp As HtmlTableRow
    Protected WithEvents TrBlk As HtmlTableRow
    Protected WithEvents TrSubBlk As HtmlTableRow
    Protected WithEvents trsumrprt As HtmlTableRow

    Protected WithEvents PrintPrev As ImageButton

    Dim objLangCapDs As New Object()

    Dim TrMthYr As HtmlTableRow

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfigsetting As Integer
    Dim strUserLoc As String

    Dim dr As DataRow
    Dim intCnt As Integer
    Dim intErrNo As Integer
    Dim tempActGrp As String
    Dim strCostIsBlock As String
    Dim strYieldIsBlock As String
	
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String
	

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")

        lblActGrp.Visible = False

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else

            If Not Page.IsPostBack Then
                onload_GetLangCap()
                GetActGrp()
                BlkTypeList()
            End If

            If lstBlkType.SelectedItem.Value = "BlkCode" Then
                TrBlkGrp.Visible = False
                TrBlk.Visible = True
                TrSubBlk.Visible = False
            ElseIf lstBlkType.SelectedItem.Value = "BlkGrp" Then
                TrBlkGrp.Visible = True
                TrBlk.Visible = False
                TrSubBlk.Visible = False
            ElseIf lstBlkType.SelectedItem.Value = "SubBlkCode" Then
                TrBlkGrp.Visible = False
                TrBlk.Visible = False
                TrSubBlk.Visible = True
            End If

        End If
    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim ucTrMthYr As HtmlTableRow

        ucTrMthYr = RptSelect.FindControl("TrMthYr")
        ucTrMthYr.Visible = True

        If Page.IsPostBack Then
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        lblBlkType.Text = GetCaption(objLangCap.EnumLangCap.Block) & " Type :"
        lblBlkGrp.Text = GetCaption(objLangCap.EnumLangCap.BlockGrp) & " Group :"
        lblBlkCode.Text = GetCaption(objLangCap.EnumLangCap.Block) & " Code :"
        lblSubBlkCode.Text = GetCaption(objLangCap.EnumLangCap.SubBlock) & " Code :"
        lblActGrpCode.Text = GetCaption(objLangCap.EnumLangCap.ActGrp) & " Code :"
        lblActGrp.Text = "You must select at least one " & GetCaption(objLangCap.EnumLangCap.ActGrp) & " Code."
        lblLocation.Text = GetCaption(objLangCap.EnumLangCap.Location)

        lblHidActGrpCode.Text = UCase(GetCaption(objLangCap.EnumLangCap.ActGrp)) & " " & UCase(lblCode.Text)
        lblHidSubBlkCode.Text = UCase(GetCaption(objLangCap.EnumLangCap.SubBlock)) & " " & UCase(lblCode.Text)
        lblHidBlkCode.Text = UCase(GetCaption(objLangCap.EnumLangCap.Block)) & " " & UCase(lblCode.Text)
        lblHidBlkGrpCode.Text = UCase(GetCaption(objLangCap.EnumLangCap.BlockGrp)) & " " & UCase(lblCode.Text)
        lblHidSubBlk.Text = GetCaption(objLangCap.EnumLangCap.SubBlock)
        lblHidBlk.Text = GetCaption(objLangCap.EnumLangCap.Block)
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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PWSYSTEM_CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/GL_StdRpt_Selection.aspx")
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

    Sub GetActGrp()
        Dim strParam As String
        Dim objActGrp As New DataSet()
        Dim arrParam As Array
        Dim intCntActGrp As Integer
        Dim intCntUB As Integer
        Dim strArrUserLoc As String
        Dim strOppCd_MainHarv_ActGrp_GET As String = "GL_STDRPT_MAINHARVEST_ACTGRP_GET"

        strParam = objGLSetup.EnumActGrpStatus.Active & "|"
        Try
            intErrNo = objGL.mtdGetMaintainHarvestActGrpList(strOppCd_MainHarv_ActGrp_GET, strParam, objActGrp)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_GL_MAINHARV_ACTGRP&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/GL_StdRpt_Selection.aspx")
        End Try

        For intCnt = 0 To objActGrp.Tables(0).Rows.Count - 1
            objActGrp.Tables(0).Rows(intCnt).Item("ActGrpCode") = Trim(objActGrp.Tables(0).Rows(intCnt).Item("ActGrpcode"))
            objActGrp.Tables(0).Rows(intCnt).Item("Description") = Trim(objActGrp.Tables(0).Rows(intCnt).Item("ActGrpCode")) & "(" & Trim(objActGrp.Tables(0).Rows(intCnt).Item("Description")) & ")"
        Next


        cblActGrp.DataSource = objActGrp.Tables(0)
        cblActGrp.DataValueField = "ActGrpCode"
        cblActGrp.DataTextField = "Description"
        cblActGrp.DataBind()

    End Sub

    Sub Check_Clicked(ByVal Sender As Object, ByVal E As EventArgs)
        Dim intCntActGrpAll As Integer = 0
        Dim intCntActGrp As Integer = 0

        If cbActGrpAll.Checked Then
            For intCntActGrpAll = 0 To cblActGrp.Items.Count - 1
                cblActGrp.Items(intCntActGrpAll).Selected = True
            Next
        Else
            For intCntActGrpAll = 0 To cblActGrp.Items.Count - 1
                cblActGrp.Items(intCntActGrpAll).Selected = False
            Next

        End If
        ActGrpCheck()
    End Sub

    Sub ActGrpCheckList(ByVal Sender As Object, ByVal E As EventArgs)
        ActGrpCheck()
    End Sub

    Sub ActGrpCheck()
        Dim intCntActGrp As Integer = 0

        For intCntActGrp = 0 To cblActGrp.Items.Count - 1
            If cblActGrp.Items(intCntActGrp).Selected Then
                If cblActGrp.Items.Count = 1 Then
                    tempActGrp = cblActGrp.Items(intCntActGrp).Value
                Else
                    tempActGrp = tempActGrp & "','" & cblActGrp.Items(intCntActGrp).Value
                End If
            End If
        Next

        If Left(tempActGrp, 3) = "','" Then
            tempActGrp = Right(tempActGrp, Len(tempActGrp) - 3)
        End If
        hidActGrpCode.Value = tempActGrp

    End Sub

    Sub BlkTypeList()

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
            lblHidCostIsBlock.Text = "true"
            lstBlkType.Items.Add(New ListItem(Left(lblBlkCode.Text, Len(lblBlkCode.Text) - 2), "BlkCode"))
            lstBlkType.Items.Add(New ListItem(Left(lblBlkGrp.Text, Len(lblBlkGrp.Text) - 2), "BlkGrp"))
        Else
            lblHidCostIsBlock.Text = "false"
            lstBlkType.Items.Add(New ListItem(Left(lblSubBlkCode.Text, Len(lblSubBlkCode.Text) - 2), "SubBlkCode"))
            lstBlkType.Items.Add(New ListItem(Left(lblBlkCode.Text, Len(lblBlkCode.Text) - 2), "BlkCode"))
        End If

    End Sub


    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strBlkType As String
        Dim strBlkGrp As String
        Dim strBlkCode As String
        Dim strSubBlkCode As String
        Dim strSum As String
        Dim strSupp As String

        Dim strddlAccMth As String
        Dim strddlAccYr As String
        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String
        Dim strFileName As String

        Dim tempAccMth As DropDownList
        Dim tempAccYr As DropDownList
        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden

        Dim intCntActGrp As Integer

        Dim enStrBlkCode As String
        Dim enStrBlkGrpCode As String
        Dim enStrSubBlkCode As String

        Dim blnSel As Boolean

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockYieldLevel), intConfigsetting) = True Then
            strYieldIsBlock = "true"
        Else
            strYieldIsBlock = "false"
        End If

        tempAccMth = RptSelect.FindControl("lstAccMonth")
        strddlAccMth = Trim(tempAccMth.SelectedItem.Value)
        tempAccYr = RptSelect.FindControl("lstAccYear")
        strddlAccYr = Trim(tempAccYr.SelectedItem.Value)

        tempRpt = RptSelect.FindControl("lstRptName")
        strRptID = Trim(tempRpt.SelectedItem.Value)
        strRptName = Trim(tempRpt.SelectedItem.Text)
        tempUserLoc = RptSelect.FindControl("hidUserLoc")
        strUserLoc = Trim(tempUserLoc.Value)
        tempDec = RptSelect.FindControl("lstDecimal")
        strDec = Trim(tempDec.SelectedItem.Value)

        If strUserLoc = "" Then
            templblUL = RptSelect.FindControl("lblUserLoc")
            templblUL.Visible = True
            Exit Sub
        Else
            If Left(strUserLoc, 3) = "','" Then
                strUserLoc = Right(strUserLoc, Len(strUserLoc) - 3)
            ElseIf Right(strUserLoc, 3) = "','" Then
                strUserLoc = Left(strUserLoc, Len(strUserLoc) - 3)
            End If
        End If

        strBlkType = Trim(lstBlkType.SelectedItem.Value)
        strCostIsBlock = Trim(lblHidCostIsBlock.Text)

        If txtBlkGrp.Text = "" Then
            strBlkGrp = ""
        Else
            strBlkGrp = Trim(txtBlkGrp.Text)
        End If

        If txtBlkCode.Text = "" Then
            strBlkCode = ""
        Else
            strBlkCode = Trim(txtBlkCode.Text)
        End If

        If txtSubBlkCode.Text = "" Then
            strSubBlkCode = ""
        Else
            strSubBlkCode = Trim(txtSubBlkCode.Text)
        End If

        If rbSumYes.Checked Then
            strSum = rbSumYes.Text
        ElseIf rbSumNo.Checked Then
            strSum = rbSumNo.Text
        End If

        If rbSuppYes.Checked Then
            strSupp = rbSuppYes.Text
        ElseIf rbSuppNo.Checked Then
            strSupp = rbSuppNo.Text
        End If

        For intCntActGrp = 0 To cblActGrp.Items.Count - 1
            If cblActGrp.Items(intCntActGrp).Selected Then
                blnSel = True
                Exit For
            End If
        Next

        If Not blnSel = True Then
            blnSel = False
            lblActGrp.Visible = True
            Exit Sub
        End If

        enStrBlkGrpCode = Server.UrlEncode(strBlkGrp)
        enStrBlkCode = Server.UrlEncode(strBlkCode)
        enStrSubBlkCode = Server.UrlEncode(strSubBlkCode)

        strFileName = "GL_StdRpt_MaintainHarvestListPreview.aspx"


        Response.Write("<Script Language=""JavaScript"">window.open("" " & strFileName & "?Type=Print&Location=" & strUserLoc & _
                      "&DDLAccMth=" & strddlAccMth & _
                      "&DDLAccYr=" & strddlAccYr & _
                      "&RptID=" & strRptID & _
                      "&RptName=" & strRptName & _
                      "&Decimal=" & strDec & _
                      "&lblBlkType=" & lblBlkType.Text & _
                      "&lblBlkGrp=" & lblHidBlkGrpCode.Text & _
                      "&lblBlkCode=" & lblHidBlkCode.Text & _
                      "&lblSubBlkCode=" & lblHidSubBlkCode.Text & _
                      "&lblActGrpCode=" & lblHidActGrpCode.Text & _
                      "&lblLocation=" & lblLocation.Text & _
                      "&SubBlkTag=" & lblHidSubBlk.Text & _
                      "&BlkTag=" & lblHidBlk.Text & _
                      "&BlkType=" & strBlkType & _
                      "&BlkGrp=" & enStrBlkGrpCode & _
                      "&BlkCode=" & enStrBlkCode & _
                      "&SubBlkCode=" & enStrSubBlkCode & _
                      "&ActGrpCode=" & Server.UrlEncode(hidActGrpCode.Value) & _
                      "&CostIsBlock=" & strCostIsBlock & _
                      "&YieldIsBlock=" & strYieldIsBlock & _
                      "&Sum=" & strSum & _
                      "&Supp=" & strSupp & """,null ,""status=yes, resizable=yes, scrollbars=yes, menubar=no, toolbar=no, location=no"");</Script>")

    End Sub

End Class
