Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports System.Xml
Imports System.Web.Services

Public Class GL_StdRpt_VehicleUsageList : Inherits Page

#Region " -- Form Properties -- "
    Protected RptSelect As UserControl
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblVehUsgId As Label
    Protected WithEvents lblVehCode As Label
    Protected WithEvents lblVehTypeCode As Label
    Protected WithEvents lblAccCode As Label
    Protected WithEvents lblBlock As Label
    Protected WithEvents lblBlkGrpCode As Label
    Protected WithEvents lblBlkCode As Label
    Protected WithEvents lblSubBlkCode As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblID As Label
    Protected WithEvents lblHidCostLevel As Label
    Protected WithEvents txtSrchVehUsgId As TextBox
    Protected WithEvents txtSrchVehCode As TextBox
    Protected WithEvents txtSrchVehTypeCode As TextBox
    Protected WithEvents txtSrchAccCode As TextBox
    Protected WithEvents ddlBlkType As DropDownList
    Protected WithEvents txtSrchBlkGrpCode As TextBox
    Protected WithEvents txtSrchBlkCode As TextBox
    Protected WithEvents txtSrchSubBlkCode As TextBox
    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents ddlHitung As DropDownList
    Protected WithEvents TrBlkGrp As HtmlTableRow
    Protected WithEvents TrBlk As HtmlTableRow
    Protected WithEvents TrSubBlk As HtmlTableRow
    Protected WithEvents rbSuppYes As RadioButton
    Protected WithEvents rbSuppNo As RadioButton
    Protected WithEvents rbUsage As RadioButton
    Protected WithEvents rbAct As RadioButton
    Protected WithEvents PrintPrev As ImageButton

    Protected WithEvents lblVehUsg As Label
    Protected WithEvents lblVehicle As Label
    Protected WithEvents lblVehType As Label
    Protected WithEvents lblAccount As Label
    Protected WithEvents lblSubBlk As Label
    Protected WithEvents lblBlkGrp As Label
    Protected WithEvents lblRunUnit As Label
    Protected WithEvents lblLocation As Label
#End Region
    
    Dim objGL As New agri.GL.clsReport
    Dim objSysCfg As New agri.PWSystem.clsConfig
    Dim objGLSetup As New agri.GL.clsSetup
    Dim objGLTrx As New agri.GL.clsTrx
    Dim objLangCap As New agri.PWSystem.clsLangCap
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl
    Dim objLangCapDs As New Object
    Dim TrMthYr As HtmlTableRow

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfigsetting As Integer
    Dim dr As DataRow
    Dim intErrNo As Integer
    Dim strCostLevel As String
    Dim GrpType As String = ""

	
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String
	

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        Dim tempDateFrom As TextBox
        Dim tempDateTo As TextBox

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            onload_GetLangCap()
            If Not Page.IsPostBack Then
                BindBlkType()
                BindStatus()
            End If

            If ddlBlkType.SelectedItem.Value = "BlkCode" Then
                TrBlkGrp.Visible = False
                TrBlk.Visible = True
                TrSubBlk.Visible = False
            ElseIf ddlBlkType.SelectedItem.Value = "BlkGrp" Then
                TrBlkGrp.Visible = True
                TrBlk.Visible = False
                TrSubBlk.Visible = False
            ElseIf ddlBlkType.SelectedItem.Value = "SubBlkCode" Then
                TrBlkGrp.Visible = False
                TrBlk.Visible = False
                TrSubBlk.Visible = True
            End If
        End If
    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim htmltr As HtmlTableRow

        htmltr = RptSelect.FindControl("TrMthYr")
        htmltr.Visible = True
        If Page.IsPostBack Then
        End If
    End Sub



    Sub BindBlkType()
        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
            lblHidCostLevel.Text = "block"
            ddlBlkType.Items.Add(New ListItem(lblBlkCode.Text, "BlkCode"))
            ddlBlkType.Items.Add(New ListItem(lblBlkGrpCode.Text, "BlkGrp"))
        Else
            lblHidCostLevel.Text = "subblock"
            ddlBlkType.Items.Add(New ListItem(lblSubBlkCode.Text, "SubBlkCode"))
            ddlBlkType.Items.Add(New ListItem(lblBlkCode.Text, "BlkCode"))
        End If
    End Sub


    Sub BindStatus()
        ddlStatus.Items.Add(New ListItem(objGLTrx.mtdGetVehicleUsageStatus(objGLTrx.EnumVehicleUsageStatus.All), objGLTrx.EnumVehicleUsageStatus.All))
        ddlStatus.Items.Add(New ListItem(objGLTrx.mtdGetVehicleUsageStatus(objGLTrx.EnumVehicleUsageStatus.Active), objGLTrx.EnumVehicleUsageStatus.Active))
        ddlStatus.Items.Add(New ListItem(objGLTrx.mtdGetVehicleUsageStatus(objGLTrx.EnumVehicleUsageStatus.Closed), objGLTrx.EnumVehicleUsageStatus.Closed))
        ddlStatus.Items.Add(New ListItem(objGLTrx.mtdGetVehicleUsageStatus(objGLTrx.EnumVehicleUsageStatus.Cancelled), objGLTrx.EnumVehicleUsageStatus.Cancelled))
    End Sub


    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strddlAccMth As String
        Dim strddlAccYr As String
        Dim strRptId As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String

        Dim strSrchVehUsgId As String
        Dim strSrchVehCode As String
        Dim strSrchVehTypeCode As String
        Dim strSrchAccCode As String
        Dim strSrchBlkGrpCode As String
        Dim strSrchBlkCode As String
        Dim strSrchSubBlkCode As String
        Dim strSrchStatus As String
        Dim strSrchText As String

        Dim strBlkType As String
        Dim strSupp As String
        Dim objSysCfgDs As New Object

        Dim enStrVehUsgId As String
        Dim enStrVehCode As String
        Dim enStrVehTypeCode As String
        Dim enStrAccCode As String
        Dim enStrBlkGrpCode As String
        Dim enStrBlkCode As String
        Dim enStrSubBlkCode As String
        Dim enStrStatus As String


        Dim ddlist As DropDownList
        Dim tempUserLoc As HtmlInputHidden
        Dim templblUL As Label

        ddlist = RptSelect.FindControl("lstAccMonth")
        strddlAccMth = Trim(ddlist.SelectedItem.Value)

        ddlist = RptSelect.FindControl("lstAccYear")
        strddlAccYr = Trim(ddlist.SelectedItem.Value)

        ddlist = RptSelect.FindControl("lstRptName")
        strRptId = Trim(ddlist.SelectedItem.Value)

        ddlist = RptSelect.FindControl("lstRptName")
        strRptName = Trim(ddlist.SelectedItem.Text)

        ddlist = RptSelect.FindControl("lstDecimal")
        strDec = Trim(ddlist.SelectedItem.Value)

        strBlkType = ddlBlkType.SelectedItem.Value

        tempUserLoc = RptSelect.FindControl("hidUserLoc")

        strUserLoc = Trim(tempUserLoc.Value)
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

        If rbSuppYes.Checked Then
            strSupp = rbSuppYes.Text
        ElseIf rbSuppNo.Checked Then
            strSupp = rbSuppNo.Text
        End If

        strCostLevel = Trim(lblHidCostLevel.Text)
        strSrchVehUsgId = Trim(txtSrchVehUsgId.Text)
        strSrchVehCode = Trim(txtSrchVehCode.Text)
        strSrchVehTypeCode = Trim(txtSrchVehTypeCode.Text)
        strSrchAccCode = Trim(txtSrchAccCode.Text)
        strSrchBlkGrpCode = Trim(txtSrchBlkGrpCode.Text)
        strSrchBlkCode = Trim(txtSrchBlkCode.Text)
        strSrchSubBlkCode = Trim(txtSrchSubBlkCode.Text)

        strSrchText = Trim(ddlStatus.SelectedItem.Text)
        strSrchStatus = Trim(ddlStatus.SelectedItem.Value)



        If rbUsage.Checked Then
            GrpType = "U"
        ElseIf rbAct.Checked Then
            GrpType = "A"
        End If

        enStrVehUsgId = Server.UrlEncode(strSrchVehUsgId)
        enStrVehCode = Server.UrlEncode(strSrchVehCode)
        enStrVehTypeCode = Server.UrlEncode(strSrchVehTypeCode)
        enStrAccCode = Server.UrlEncode(strSrchAccCode)
        enStrBlkGrpCode = Server.UrlEncode(strSrchBlkGrpCode)
        enStrBlkCode = Server.UrlEncode(strSrchBlkCode)
        enStrSubBlkCode = Server.UrlEncode(strSrchSubBlkCode)
        enStrStatus = Server.UrlEncode(strSrchStatus)

        Response.Write("<Script Language=""JavaScript"">window.open(""GL_StdRpt_VehicleUsageListPreview.aspx?Type=Print&CompName=" & strCompany & _
                       "&SelLocation=" & strUserLoc & _
                       "&DDLAccMth=" & strddlAccMth & _
                       "&DDLAccYr=" & strddlAccYr & _
                       "&RptId=" & strRptId & _
                       "&RptName=" & strRptName & _
                       "&CostLevel=" & strCostLevel & _
                       "&Decimal=" & strDec & _
                       "&Supp=" & strSupp & _
                       "&SrchVehUsgId=" & enStrVehUsgId & _
                       "&SrchVehCode=" & enStrVehCode & _
                       "&SrchVehTypeCode=" & enStrVehTypeCode & _
                       "&SrchAccCode=" & enStrAccCode & _
                       "&SrchBlkType=" & strBlkType & _
                       "&SrchBlkGrpCode=" & enStrBlkGrpCode & _
                       "&SrchBlkCode=" & enStrBlkCode & _
                       "&SrchSubBlkCode=" & enStrSubBlkCode & _
                       "&SrchStatus=" & enStrStatus & _
                       "&SrchText=" & strSrchText & _
                       "&lblID=" & lblID.Text & _
                       "&lblCode=" & lblCode.Text & _
                       "&lblVehUsg=" & lblVehUsg.Text & _
                       "&lblVehicle=" & lblVehicle.Text & _
                       "&lblVehType=" & lblVehType.Text & _
                       "&lblAccount=" & lblAccount.Text & _
                       "&lblSubBlk=" & lblSubBlk.Text & _
                       "&lblBlock=" & lblBlock.Text & _
                       "&lblBlkGrp=" & lblBlkGrp.Text & _
                       "&lblRunUnit=" & lblRunUnit.Text & _
                       "&lblLocation=" & lblLocation.Text & _
                       "&GrpType=" & Trim(GrpType) & _
                       "&hitung=" & ddlHitung.SelectedIndex & _
                       """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        lblVehUsgId.Text = GetCaption(objLangCap.EnumLangCap.VehUsage) & lblID.Text
        lblVehCode.Text = GetCaption(objLangCap.EnumLangCap.Vehicle) & lblCode.Text
        lblVehTypeCode.Text = GetCaption(objLangCap.EnumLangCap.VehType) & lblCode.Text
        lblAccCode.Text = GetCaption(objLangCap.EnumLangCap.Account) & lblCode.Text
        lblBlock.Text = GetCaption(objLangCap.EnumLangCap.Block)
        lblBlkGrpCode.Text = GetCaption(objLangCap.EnumLangCap.BlockGrp) & lblCode.Text
        lblBlkCode.Text = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text
        lblSubBlkCode.Text = GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.Text

        lblVehUsg.Text = GetCaption(objLangCap.EnumLangCap.VehUsage)
        lblVehicle.Text = GetCaption(objLangCap.EnumLangCap.Vehicle)
        lblVehType.Text = GetCaption(objLangCap.EnumLangCap.VehType)
        lblAccount.Text = GetCaption(objLangCap.EnumLangCap.Account)
        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
            lblBlock.Text = GetCaption(objLangCap.EnumLangCap.Block)
        Else
            lblSubBlk.Text = GetCaption(objLangCap.EnumLangCap.SubBlock)
        End If
        lblBlkGrp.Text = GetCaption(objLangCap.EnumLangCap.BlockGrp)
        lblRunUnit.Text = GetCaption(objLangCap.EnumLangCap.VehUsageUnit)
        lblLocation.Text = GetCaption(objLangCap.EnumLangCap.Location)
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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_REPORTS_MTHENDTRX_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/GL_StdRpt_Selection.aspx")
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

End Class
