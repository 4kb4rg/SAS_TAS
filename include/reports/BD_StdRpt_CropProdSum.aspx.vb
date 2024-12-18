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

Public Class BD_StdRpt_CropProdSum : Inherits Page

    Protected RptSelect As UserControl

    Dim objBD As New agri.BD.clsReport()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstBlkType As DropDownList
    Protected WithEvents lblBlkType As Label
    Protected WithEvents lblBlkGrp As Label
    Protected WithEvents lblBlkCode As Label
    Protected WithEvents lblSubBlkCode As Label

    Protected WithEvents PrintPrev As ImageButton
    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strUserLoc As String
    Dim intConfigsetting As String

    Dim intCnt As Integer
    Dim intErrNo As Integer
    Dim strLocType As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else

            If Not Page.IsPostBack Then
                onload_GetLangCap()
                BlkTypeList()
            End If

        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        lblBlkType.Text = GetCaption(objLangCap.EnumLangCap.Block) & " Type :"
        lblBlkGrp.Text = GetCaption(objLangCap.EnumLangCap.Block) & " Group"
        lblBlkCode.Text = GetCaption(objLangCap.EnumLangCap.Block) & " Code"
        lblSubBlkCode.Text = GetCaption(objLangCap.EnumLangCap.SubBlock) & " Code"
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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=BD_STDRPT_CROPPRODSUM_CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/BD_StdRpt_Selection.aspx")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

        For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If Trim(strLocType) = objAdminLoc.EnumLocType.Mill Then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                Else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                End If
                Exit For
            End If
        Next
    End Function

    Sub BlkTypeList()

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockYieldLevel), intConfigsetting) = True Then
            lstBlkType.Items.Add(New ListItem(lblBlkCode.Text, "BlkCode"))
        Else
            lstBlkType.Items.Add(New ListItem(lblSubBlkCode.Text, "SubBlkCode"))
            lstBlkType.Items.Add(New ListItem(lblBlkCode.Text, "BlkCode"))
        End If

    End Sub

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strBlkType As String
        Dim strBlkTypeText As String

        Dim strddlPeriodID As String
        Dim strddlPeriodName As String
        Dim strRptID As String
        Dim strRptName As String
        Dim strDec As String

        Dim tempPeriod As DropDownList
        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim lblPeriod As Label

        tempPeriod = RptSelect.FindControl("lstPeriodName")
        strddlPeriodID = Trim(tempPeriod.SelectedItem.Value)
        strddlPeriodName = Trim(tempPeriod.SelectedItem.Text)
        tempRpt = RptSelect.FindControl("lstRptName")
        strRptID = Trim(tempRpt.SelectedItem.Value)
        strRptName = Trim(tempRpt.SelectedItem.Text)
        tempDec = RptSelect.FindControl("lstDecimal")
        strDec = Trim(tempDec.SelectedItem.Value)

        strBlkType = Trim(lstBlkType.SelectedItem.Value)
        strBlkTypeText = Trim(lstBlkType.SelectedItem.Text)


        If strddlPeriodID = 0 Then
            lblPeriod = RptSelect.FindControl("lblPeriod")
            lblPeriod.Visible = True
            Exit Sub
        Else
            lblPeriod = RptSelect.FindControl("lblPeriod")
            lblPeriod.Visible = False
        End If

        Response.Write("<Script Language=""JavaScript"">window.open(""BD_StdRpt_CropProdSumPreview.aspx?DDLPeriodID=" & strddlPeriodID & "&DDLPeriodName=" & strddlPeriodName & _
                       "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & strDec & "&lblBlkType=" & lblBlkType.Text & "&BlkType=" & strBlkType & "&BlkTypeText=" & strBlkTypeText & _
                       """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

    End Sub

End Class
