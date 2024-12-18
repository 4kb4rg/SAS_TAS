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

Public Class IN_StdRpt_RkpMtsiStkThn : Inherits Page

    Protected RptSelect As UserControl

    Dim objIN As New agri.IN.clsReport()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Protected objINSetup As New agri.IN.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
   
    Protected WithEvents lblDate As Label
    Protected WithEvents lblDateFormat As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents rbIN As RadioButton
    Protected WithEvents rbOUT As RadioButton
    Protected WithEvents txtItemCode As TextBox



    Protected WithEvents lblLocation As Label

    Protected WithEvents PrintPrev As ImageButton


    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String

    Dim dr As DataRow
    Dim intCnt As Integer
    Dim intErrNo As Integer
    Dim strWS As String

    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String
    Dim RptType As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        strLocType = Session("SS_LOCTYPE")
        lblDate.Visible = False
        lblDateFormat.Visible = False

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else

            If Not Page.IsPostBack Then
                Dim ddlAccMnth As DropDownList
                ddlAccMnth = RptSelect.FindControl("lstAccMonth")
                ddlAccMnth.Visible = False
                onload_GetLangCap()
            End If
        End If
    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim ucTrMthYr As HtmlTableRow
        ucTrMthYr = RptSelect.FindControl("TrMthYr")
        ucTrMthYr.Visible = True

    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PWSYSTEM_CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/IN_StdRpt_Selection.aspx")
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

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strddlAccMth As String
        Dim strddlAccYr As String
        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String
        Dim strParam As String
        Dim strItemCode As String

        Dim tempAccMth As DropDownList
        Dim tempAccYr As DropDownList
        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden


        Dim objSysCfgDs As New Object()
        Dim strSp As String

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
        strItemCode = Trim(txtItemCode.Text)


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

        If rbIN.Checked Then
            strSp = "IN_STDRPT_RKP_MUTASI_STOCK_THN_IN"
            RptType = "MASUK"
        ElseIf rbOUT.Checked Then
            strSp = "IN_STDRPT_RKP_MUTASI_STOCK_THN_OUT"
            RptType = "KELUAR"
        End If




        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"
        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=AR_STDRPT_GET_CONFIG_DATE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        Response.Write("<Script Language=""JavaScript"">window.open(""IN_StdRpt_RkpMtsiStkThnPreview.aspx?Type=Print&CompName=" & strCompany & _
                               "&Location=" & strUserLoc & _
                               "&RptID=" & strRptID & _
                               "&RptName=" & strRptName & _
                               "&RptType=" & RptType & _
                               "&StrSp=" & strSp & _
                               "&DDLAccYr=" & strddlAccYr & _
                               "&ItemCode=" & strItemCode & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")


    End Sub

End Class
