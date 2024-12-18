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

Public Class GL_StdRpt_ActGrpSummary : Inherits Page
    Dim objGL As New agri.GL.clsReport()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objSysCfg As New agri.PWSystem.clsConfig()

    Protected RptSelect As UserControl

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label

    Protected WithEvents lblTitle As Label
    Protected WithEvents lblActGrp As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lstActGrp As DropDownList

    Protected WithEvents rbSuppYes As RadioButton
    Protected WithEvents rbSuppNo As RadioButton
    Protected WithEvents PrintPrev As ImageButton

    Dim TrMthYr As HtmlTableRow
    Dim objLangCapDs As New Object()
    Dim objSysConfigDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfigsetting As Integer

    Dim dr As DataRow
    Dim intCnt As Integer
    Dim intErrNo As Integer
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
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
                onload_GetLangCap()
                BindActGrpList()
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

        lblActGrp.Text = GetCaption(objLangCap.EnumLangCap.ActGrp) & lblCode.text
        lblTitle.Text = UCase(GetCaption(objLangCap.EnumLangCap.ActGrp))
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
        Catch Exp As Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_REPORTS_ACTGRPSUMMARY_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/GL_StdRpt_Selection.aspx")
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

    Sub BindActGrpList()
        Dim dr As Datarow
        Dim strParam As String
        Dim objActGrp As New DataSet()
        Dim intCntActGrp As Integer
        Dim strOppCd_ActGrp_GET As String = "GL_STDRPT_MAINHARVEST_ACTGRP_GET"

        strParam = objGLSetup.EnumActGrpStatus.Active & "|"
        Try
            intErrNo = objGL.mtdGetMaintainHarvestActGrpList(strOppCd_ActGrp_GET, strParam, objActGrp)
        Catch Exp As Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_GL_ACTGRPSUMMARY_ACTGRP&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/GL_StdRpt_Selection.aspx")
        End Try

        For intCntActGrp = 0 To objActGrp.Tables(0).Rows.Count - 1
            objActGrp.Tables(0).Rows(intCntActGrp).Item("ActGrpCode") = Trim(objActGrp.Tables(0).Rows(intCntActGrp).Item("ActGrpCode"))
            objActGrp.Tables(0).Rows(intCntActGrp).Item("Description") = Trim(objActGrp.Tables(0).Rows(intCntActGrp).Item("ActGrpCode")) & " (" & _
                                                                         Trim(objActGrp.Tables(0).Rows(intCntActGrp).Item("Description")) & ")"
        Next

        dr = objActGrp.Tables(0).NewRow()
        dr("ActGrpCode") = "%"
        dr("Description") = "All "
        objActGrp.Tables(0).Rows.InsertAt(dr, 0)

        lstActGrp.DataSource = objActGrp.Tables(0)
        lstActGrp.DataValueField = "ActGrpCode"
        lstActGrp.DataTextField = "Description"
        lstActGrp.DataBind()

    End Sub


    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strActGrpCode As String
        Dim strActGrpName As String

        Dim strddlAccMth As String
        Dim strddlAccYr As String
        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String
        Dim strSupp As String
        Dim strCostLevel As String

        Dim tempAccMth As DropDownList
        Dim tempAccYr As DropDownList
        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden
        Dim intPosition As Integer
        Dim fmtActGrpName As String

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

        strActGrpCode = Trim(lstActGrp.SelectedItem.Value)
        strActGrpName = Trim(lstActGrp.SelectedItem.Text)
        intPosition = InStr(strActGrpName, "(")
        fmtActGrpName = Mid(strActGrpName, intPosition+1, Len(strActGrpName)-intPosition)
        fmtActGrpName = Replace(fmtActGrpName, ")", "")
        
        If rbSuppYes.Checked Then
            strSupp = rbSuppYes.Text
        ElseIf rbSuppNo.Checked Then
            strSupp = rbSuppNo.Text
        End If

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigSetting) = True Then
            strCostLevel = "block"
        Else
            strCostLevel = "subblock"
        End If

        Response.Write("<Script Language=""JavaScript"">window.open(""GL_StdRpt_ActGrpSummaryPreview.aspx?Type=Print&CompName=" & strCompany & _
                       "&SelLocation=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & _
                       "&Decimal=" & strDec & _
                       "&DDLAccMth=" & strddlAccMth & _
                       "&DDLAccYr=" & strddlAccYr & _
                       "&Supp=" & strSupp & _
                       "&SelActGrpCode=" & strActGrpCode & _
                       "&SelActGrpName=" & fmtActGrpName & _
                       "&CostLevel=" & strCostLevel & _
                       """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

    End Sub
End Class
