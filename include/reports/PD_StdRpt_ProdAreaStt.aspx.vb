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


Public Class PD_StdRpt_ProdAreaStt : Inherits Page

    Protected RptSelect As UserControl

    Dim objPD As New agri.PD.clsReport()
    Dim objPDTrx As New agri.PD.clsTrx()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Protected WithEvents lblTracker As Label
    Protected WithEvents lblBlkType As Label
    Protected WithEvents lblBlkGrp As Label
    Protected WithEvents lblBlkCode As Label
    Protected WithEvents lblSubBlkCode As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents txtDocNoFrom As TextBox
    Protected WithEvents txtDocNoTo As TextBox
    Protected WithEvents lstBlkType As DropDownList
    Protected WithEvents txtBlkGrp As TextBox
    Protected WithEvents txtBlkCode As TextBox
    Protected WithEvents txtSubBlkCode As TextBox
    Protected WithEvents TrBlkGrp As HtmlTableRow
    Protected WithEvents TrBlk As HtmlTableRow
    Protected WithEvents TrSubBlk As HtmlTableRow
    Protected WithEvents rbSuppYes As RadioButton
    Protected WithEvents rbSuppNo As RadioButton
    Protected WithEvents PrintPrev As ImageButton

    Dim objLangCapDs As New Dataset()

    Dim TrMthYr As HtmlTableRow
    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfigSetting As Integer
    Dim strUserLoc As String
    Dim intCnt As Integer
    Dim intErrNo As Integer
	
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String
	
    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_PDACCMONTH")
        strAccYear = Session("SS_PDACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigSetting = Session("SS_CONFIGSETTING")
		
	        strLocType = Session("SS_LOCTYPE")
	    
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
                onload_GetLangCap()
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
        lblBlkGrp.Text = GetCaption(objLangCap.EnumLangCap.BlockGrp) & " Code :"
        lblBlkCode.Text = GetCaption(objLangCap.EnumLangCap.Block) & " Code :"
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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PWSYSTEM_CLSLANGCAP_BUSSTERM_GET&errmesg=" & Exp.ToString() & "&redirect=../en/reports/IN_StdRpt_Selection.aspx")
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
    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strDocNoFrom As String
        Dim strDocNoTo As String
        Dim strBlkType As String
        Dim strBlkGrp As String
        Dim strBlkCode As String
        Dim strSubBlkCode As String
        Dim strddlAccMth As String
        Dim strddlAccYr As String
        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String
        Dim strSupp As String
        Dim tempAccMth As DropDownList
        Dim tempAccYr As DropDownList
        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden
        Dim enStrBlkGrpCode As String
        Dim enStrBlkCode As String
        Dim enStrSubBlkCode As String

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

        enStrBlkGrpCode = txtBlkGrp.text
        enStrBlkCode = txtBlkCode.text
    
        Response.Write("<Script Language=""JavaScript"">window.open(""PD_StdRpt_ProdAreaSttPreview.aspx?Type=Print&CompName=" & strCompany & _
                        "&Location=" & strUserLoc & _
                        "&RptID=" & strRptID & _
                        "&RptName=" & strRptName & _
                        "&DDLAccMth=" & strddlAccMth & _
                        "&DDLAccYr=" & strddlAccYr & _
                        "&lblLocation=" & lblLocation.Text & _
                        "&Decimal=" & strDec & _
                        "&lblBlkGrp=" & lblBlkGrp.text & _
                        "&lblBlkCode=" & lblBlkCode.text & _
                        "&BlkGrp=" & enStrBlkGrpCode & _
                        "&BlkCode=" & enStrBlkCode & _
                        """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

    End Sub
End Class
