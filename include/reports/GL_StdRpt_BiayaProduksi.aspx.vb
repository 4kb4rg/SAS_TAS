
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

Public Class GL_StdRpt_BiayaProduksi : Inherits Page

    Protected RptSelect As UserControl

    Dim objGL As New agri.GL.clsReport()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblCOACode As Label    
    Protected WithEvents lblDate As Label
    Protected WithEvents lblDateFormat As Label  
    Protected WithEvents txtRemarks As TextBox
    Protected WithEvents PrintPrev As ImageButton

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfigsetting As Integer
    Dim intErrNo As Integer
    Dim strLocTag As String

    Dim objLangCapDs As New Object()
    Dim objDataSet As New Object()
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
        strLocTag = GetCaption(objLangCap.EnumLangCap.Location)
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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_STDRPT_DETACCLIST_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/IN_StdRpt_Selection.aspx")
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

        Dim strddlAccMth As String
        Dim strddlAccYr As String
        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String
        Dim strRemarks As String
        Dim ddlDropDown As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden
        Dim strParam As String
        Dim objSysCfgDs As New Object()

        ddlDropDown = RptSelect.FindControl("lstAccMonth")
        strddlAccMth = Trim(ddlDropDown.SelectedItem.Value)
        ddlDropDown = RptSelect.FindControl("lstAccYear")
        strddlAccYr = Trim(ddlDropDown.SelectedItem.Value)
        ddlDropDown = RptSelect.FindControl("lstRptName")
        strRptID = Trim(ddlDropDown.SelectedItem.Value)
        strRptName = Trim(ddlDropDown.SelectedItem.Text)
        ddlDropDown = RptSelect.FindControl("lstDecimal")
        strDec = Trim(ddlDropDown.SelectedItem.Value)        
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

        Response.Write("<Script Language=""JavaScript"">window.open(""GL_StdRpt_BiayaProduksiPreview.aspx?CompName=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & strDec & _
                    "&DDLAccMth=" & strddlAccMth & "&DDLAccYr=" & strddlAccYr & "&lblLocation=" & strLocTag & _
                    "&Remarks=" & Server.UrlEncode(Trim(txtRemarks.Text))  & _
                    """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

    End Sub

    
End Class
