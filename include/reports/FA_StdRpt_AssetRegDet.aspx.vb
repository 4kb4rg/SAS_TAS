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

Public Class FA_StdRpt_AssetRegDet : Inherits Page

    Protected RptSelect As UserControl

    Dim objFAReport As New agri.FA.clsReport()
    Dim objFASetup As New agri.FA.clsSetup()
    Dim objFATrx As New agri.FA.clsTrx()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Protected WithEvents lblDate As Label
    Protected WithEvents lblDateFormat As Label
    Protected WithEvents lblErrAccMonth As Label
    Protected WithEvents lblErrAccYear As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label

    Protected WithEvents lblLocation As Label
    Protected WithEvents lblCode As Label

    Protected WithEvents txtAssetCategory As TextBox
    Protected WithEvents txtAssetGroup As TextBox
    Protected WithEvents cbFiskal As CheckBox
    Protected WithEvents cbExcel As CheckBox

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
    Dim intConfigsetting As String

    Dim dr As DataRow
    Dim intCnt As Integer
    Dim intErrNo As Integer
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_FAACCMONTH")
        strAccYear = Session("SS_FAACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")

        lblDate.Visible = False
        lblDateFormat.Visible = False

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
                onload_GetLangCap()
            Else
                onload_GetLangCap()
            End If
        End If
    End Sub


    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim htmltr As HtmlTableRow
        Dim htmltc As HtmlTableCell

        htmltr = RptSelect.FindControl("TrMthYr")
        htmltr.Visible = True
    End Sub

    Sub onload_GetLangCap()
        'GetEntireLangCap()
        lblLocation.Text = "Location"
        'Select Case ddlAssetCategory.SelectedItem.Value
        '    Case objFASetup.EnumAssetCategory.AssetClass
        '        lblAssetCategory.Text = GetCaption(objLangCap.EnumLangCap.AssetClass)
        '    Case objFASetup.EnumAssetCategory.AssetGrp
        '        lblAssetCategory.Text = GetCaption(objLangCap.EnumLangCap.AssetGrp)
        'End Select
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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PWSYSTEM_CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/FA_StdRpt_Selection.aspx")
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
       
        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String

        Dim strddlAccMth As String
        Dim strddlAccYr As String
        
        Dim intddlAccMth As Integer
        Dim intddlAccYr As Integer

        Dim tempAccMth As DropDownList
        Dim tempAccYr As DropDownList
        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden
        

        Dim strParam As String
        Dim strDateSetting As String
        Dim objSysCfgDs As New Object()
        Dim strFiskal As String

        tempAccMth = RptSelect.FindControl("lstAccMonth")
        strddlAccMth = Trim(tempAccMth.SelectedItem.Value)
        tempAccYr = RptSelect.FindControl("lstAccYear")
        strddlAccYr = Trim(tempAccYr.SelectedItem.Value)

        intddlAccMth = CInt(strddlAccMth)
        intddlAccYr = CInt(strddlAccYr)

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

       
        strFiskal = IIF(cbFiskal.Checked = True, "1", "0")

        Dim strExportToExcel As String
        strExportToExcel = IIF(cbExcel.Checked = True, "1", "0")


        Response.Write("<Script Language=""JavaScript"">window.open(""FA_StdRpt_AssetRegDetPreview.aspx?Type=Print&CompName=" & strCompany & _
                        "&Location=" & strUserLoc & "&RptID=" & strRptID & _
                        "&RptName=" & strRptName & "&Decimal=" & strDec & _
                        "&DDLAccMth=" & strddlAccMth & "&DDLAccYr=" & strddlAccYr & _
                        "&AssetCategory=" & txtAssetCategory.Text & "&AssetGroup=" & txtAssetGroup.Text & _
                        "&lblLocation=" & lblLocation.Text & _
                        "&IsFiskal=" & strFiskal & _
                        "&ExportToExcel=" & strExportToExcel & _
                        """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")


    End Sub
End Class
