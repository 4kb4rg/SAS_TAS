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

Public Class GL_StdRpt_MthVehDist : Inherits Page

    Protected RptSelect As UserControl

    Dim objGL As New agri.GL.clsReport()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label

    Protected WithEvents lblVehTypeCode As Label
    Protected WithEvents lblVehCode As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents lblAccCode As Label
    Protected WithEvents lblBlkCode As Label
    Protected WithEvents lblSubBlkCode As Label
    Protected WithEvents lblVehExpCode As Label
    Protected WithEvents lblVehType As Label
    Protected WithEvents lblVehicle As Label
    Protected WithEvents txtVehTypeCode As TextBox
    Protected WithEvents txtVehCode As TextBox

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
	
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String
	

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        strLangCode = Session("SS_LANGCODE")
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

        lblVehTypeCode.Text = GetCaption(objLangCap.EnumLangCap.VehType) & " Code :"
        lblVehCode.Text = GetCaption(objLangCap.EnumLangCap.Vehicle) & " Code :"
        lblLocation.Text = GetCaption(objLangCap.EnumLangCap.Location)
        lblAccCode.Text = GetCaption(objLangCap.EnumLangCap.Account)
        lblBlkCode.Text = GetCaption(objLangCap.EnumLangCap.Block)
        lblSubBlkCode.Text = GetCaption(objLangCap.EnumLangCap.SubBlock)
        lblVehExpCode.Text = GetCaption(objLangCap.EnumLangCap.VehExpense)
        lblVehType.Text = GetCaption(objLangCap.EnumLangCap.VehType)
        lblVehicle.Text = GetCaption(objLangCap.EnumLangCap.Vehicle)
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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_MTHVEHDIST__CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/IN_StdRpt_Selection.aspx")
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
        Dim strVehTypeCode As String
        Dim strVehCode As String
        Dim strAccCode As String
        Dim strBlkCode As String
        Dim strSubBlkCode As String
        Dim strVehExpCode As String

        Dim strddlAccMth As String
        Dim strddlAccYr As String
        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String

        Dim tempAccMth As DropDownList
        Dim tempAccYr As DropDownList
        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden

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

        If txtVehTypeCode.Text = "" Then
            strVehTypeCode = ""
        Else
            strVehTypeCode = Trim(txtVehTypeCode.Text)
        End If

        If txtVehCode.Text = "" Then
            strVehCode = ""
        Else
            strVehCode = Trim(txtVehCode.Text)
        End If

        strVehTypeCode = Server.UrlEncode(strVehTypeCode)
        strVehCode = Server.UrlEncode(strVehCode)

        Response.Write("<Script Language=""JavaScript"">window.open(""GL_StdRpt_MthVehDistPreview.aspx?Type=Print&CompName=" & strCompany & _
                        "&Location=" & strUserLoc & _
                        "&RptID=" & strRptID & _
                        "&RptName=" & strRptName & _
                        "&Decimal=" & strDec & _
                        "&DDLAccMth=" & strddlAccMth & _
                        "&DDLAccYr=" & strddlAccYr & _
                        "&lblVehTypeCode=" & lblVehTypeCode.Text & _
                        "&lblVehType=" & lblVehType.Text & _
                        "&lblVehCode=" & lblVehCode.Text & _
                        "&lblVehicle=" & lblVehicle.Text & _
                        "&lblAccCode=" & lblAccCode.Text & _
                        "&lblBlkCode=" & lblBlkCode.Text & _
                        "&lblSubBlkCode=" & lblSubBlkCode.Text & _
                        "&lblVehExpCode=" & lblVehExpCode.Text & _
                        "&lblLocation=" & lblLocation.Text & _
                        "&VehTypeCode=" & strVehTypeCode & _
                        "&VehCode=" & strVehCode & _
                        """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

End Class

