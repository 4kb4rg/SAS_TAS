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

Public Class AR_StdRpt_ContractInvoiceAttch : Inherits Page

    Protected RptSelect As UserControl

    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objCMRpt As New agri.CM.clsReport()
    Dim objBITrx As New agri.BI.clsTrx()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objBPDs As New Object()
    Dim objSysCfgDs As New Object()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Protected WithEvents txtFromMatchId As Textbox
    Protected WithEvents txtToMatchId As Textbox
    Protected WithEvents txtBuyer As TextBox
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
    Dim strBankAccNo As String
    Dim strBankName As String
    Dim strBankBranch As String
    Dim strUndName As String
    Dim strUndPost As String
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









    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim htmltr As HtmlTableRow

        htmltr = RptSelect.FindControl("SelLocation")
        htmltr.visible = False

        htmltr = RptSelect.FindControl("TrMthYr")
        htmltr.visible = True

        htmltr = RptSelect.FindControl("SelDecimal")
        htmltr.visible = False

        If Page.IsPostBack Then
        end if
    End Sub


    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strFromMatchiId As String
        Dim strToMatchId As String
        Dim strFromInvoiceDate As String
        Dim strToInvoiceDate As String
        Dim strBillPartyCode As String
        Dim strInvoiceType As String
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

        Dim strDateSetting As String
        Dim objDateFrom As String
        Dim objDateTo As String
        Dim objDateFormat As String
        Dim intCnt As Integer
        
        strFromMatchiId = txtFromMatchId.text
        strToMatchId = txtToMatchId.text
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
        
        Response.Write("<Script Language=""JavaScript"">window.open(""AR_StdRpt_ContractInvoiceAttchPreview.aspx?Type=Print&CompName=" & strCompany & _
                       "&Location=" & strLocation & _
                       "&SelAccMonth=" & strSelAccMonth & _
                       "&SelAccYear=" & strSelAccYear & _
                       "&RptId=" & strRptId & _
                       "&RptName=" & strRptName & _
                       "&Decimal=" & strDec & _
                       "&Supp=" & strSupp & _
                       "&FromMatchId=" & strFromMatchiId & _
                       "&ToMatchId=" & strToMatchId & _
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
        Catch Exp As System.Exception
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
	                    'Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
	                    Exit For
	                End If
	            Next
	        End Function
	    

End Class
