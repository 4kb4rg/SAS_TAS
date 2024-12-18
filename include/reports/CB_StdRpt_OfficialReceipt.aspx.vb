Imports System
Imports System.IO
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

Public Class CB_StdRpt_OfficialReceipt : Inherits Page

    Protected RptSelect As UserControl

    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objCMRpt As New agri.CM.clsReport()
    Dim objCBTrx As New agri.CB.clsTrx()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objBPDs As New Object()
    Dim objSysCfgDs As New Object()

    Protected WithEvents txtFromReceiptId As Textbox
    Protected WithEvents txtToReceiptId As Textbox
    Protected WithEvents txtBuyer As Textbox
    Protected WithEvents lblBillParty As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents rbSuppYes As RadioButton
    Protected WithEvents rbSuppNo As RadioButton
    Protected WithEvents PrintPrev As ImageButton

    Dim TrMthYr As HtmlTableRow
   
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

    Dim objLangCapDs As New Object()

    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_ARACCMONTH")
        strAccYear = Session("SS_ARACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")     
        Else
            If Not Page.IsPostBack
                onload_GetLangCap()
            End If
        End If
    End Sub















    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strFromReceiptId As String
        Dim strToReceiptId As String
        Dim strBillPartyCode As String
        Dim strDec As String
        Dim strSelAccMonth As String
        Dim strSelAccYear As String
        Dim strRptId As String
        Dim strRptName As String
        Dim strSupp As String
        Dim strParam As String

        Dim ddlist As DropDownList
        Dim rdblist As RadioButtonList
        Dim txt As TextBox
        Dim intCnt As Integer
        
        strFromReceiptId = txtFromReceiptId.text
        strToReceiptId = txtToReceiptId.text

        strBillPartyCode = txtBuyer.text

        ddlist = RptSelect.FindControl("lstAccMonth")
        strSelAccMonth = Trim(ddlist.SelectedItem.Value)

        ddlist = RptSelect.FindControl("lstAccYear")
        strSelAccYear = Trim(ddlist.SelectedItem.Value)

        ddlist = RptSelect.FindControl("lstRptName")
        strRptId = Trim(ddlist.SelectedItem.Value)

        ddlist = RptSelect.FindControl("lstRptName")
        strRptName = Trim(ddlist.SelectedItem.text)

        ddlist = RptSelect.FindControl("lstDecimal")
        strDec = Trim(ddlist.SelectedItem.Value)
    
        If rbSuppYes.Checked Then
            strSupp = rbSuppYes.Text
        ElseIf rbSuppNo.Checked Then
            strSupp = rbSuppNo.Text
        End If
        
        Response.Write("<Script Language=""JavaScript"">window.open(""CB_StdRpt_OfficialReceiptPreview.aspx?Type=Print&CompName=" & strCompany & _
                       "&Location=" & strLocation & _
                       "&SelAccMonth=" & strSelAccMonth & _
                       "&SelAccYear=" & strSelAccYear & _
                       "&RptId=" & strRptId & _
                       "&RptName=" & strRptName & _
                       "&Decimal=" & strDec & _
                       "&Supp=" & strSupp & _
                       "&FromReceiptId=" & strFromReceiptId & _
                       "&ToReceiptId=" & strToReceiptId & _
                       "&BillPartyCode=" & Server.UrlEncode(strBillPartyCode) & _
                       "&BillPartyTag=" & lblBillParty.text & _
                       """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub


    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblBillParty.text = GetCaption(objLangCap.EnumLangCap.BillParty)
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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_STDRPT_TRIALBALANCE_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/IN_StdRpt_Selection.aspx")
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
