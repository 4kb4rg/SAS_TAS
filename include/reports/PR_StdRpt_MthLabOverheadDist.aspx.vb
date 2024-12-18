Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic
Imports System.Xml
Imports System.Web.Services

Public Class PR_StdRpt_MthLabOverheadDist : Inherits Page

    Protected RptSelect As UserControl

    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objSysCfg As New agri.PWSystem.clsConfig()

    Protected WithEvents lblTracker As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblAccCode As Label
    Protected WithEvents lblBlkCode As Label
    Protected WithEvents lblVehCode As Label
    Protected WithEvents lblVehExpCode As Label

    Protected WithEvents lstSortBy As DropDownList
    Protected WithEvents lstOrderBy As DropDownList

    Dim objLangCapDs As New Object()
    Dim objSysCfgDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strUserLoc As String
    Dim strCostLevel As String

    Dim dr As DataRow
    Dim intCnt As Integer
    Dim intErrNo As Integer
    Dim tempAD As String
    Dim objLoc As New agri.Admin.clsLoc()
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        strCostLevel = Session("SS_COSTLEVEL")

        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            onload_GetLangCap()
            If Not Page.IsPostBack Then
                BindSortByList()
            End If
        End If
    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim ucTrMthYr As HtmlTableRow
        ucTrMthYr = RptSelect.FindControl("TrMthYr")
        ucTrMthYr.Visible = True
    End Sub


    Sub BindSortByList()
        Dim strBlkCode As String
        lstSortBy.Items.Add(New ListItem(lblAccCode.Text, "AccCode"))
        lstSortBy.Items.Add(New ListItem(lblBlkCode.Text, "BlkCode"))
    End Sub

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim ddl As DropDownList
        Dim lbl As Label
        Dim InputHidden As HtmlInputHidden
        Dim strRptID As String
        Dim strRptTitle As String
        Dim strSelLocation As String
        Dim strSelAccMonth As String
        Dim strSelAccYear As String
        Dim strDecimal As String
        Dim strSortBy As String
        Dim strOrderBy As String

        ddl = RptSelect.FindControl("lstRptName")
        strRptID = ddl.SelectedItem.Value
        strRptTitle = ddl.SelectedItem.Text

        InputHidden = RptSelect.FindControl("hidUserLoc")
        strSelLocation = Trim(InputHidden.Value)
        If strSelLocation = "" Then
            lbl = RptSelect.FindControl("lblUserLoc")
            lbl.Visible = True
            Exit Sub
        Else
            If Left(strSelLocation, 3) = "','" Then
                strSelLocation = Right(strSelLocation, Len(strSelLocation) - 3)
            End If
        End If

        ddl = RptSelect.FindControl("lstAccMonth")
        strSelAccMonth = ddl.SelectedItem.Value

        ddl = RptSelect.FindControl("lstAccYear")
        strSelAccYear = ddl.SelectedItem.Value

        ddl = RptSelect.FindControl("lstDecimal")
        strDecimal = ddl.SelectedItem.Value

        strSortBy = Trim(lstSortBy.SelectedItem.Value)
        strOrderBy = Trim(lstOrderBy.SelectedItem.Value)

        Response.Write("<Script Language=""JavaScript"">window.open(""PR_StdRpt_MthLabOverheadDistPreview.aspx?Type=Print&CompName=" & strCompany & _
                       "&SelLocation=" & strSelLocation & _
                       "&SelAccMonth=" & strSelAccMonth & _
                       "&SelAccYear=" & strSelAccYear & _
                       "&RptID=" & strRptID & _
                       "&RptTitle=" & strRptTitle & _
                       "&Decimal=" & strDecimal & _
                       "&LocationTag=" & lblLocation.Text & _
                       "&AccCodeTag=" & lblAccCode.Text & _
                       "&BlkCodeTag=" & lblBlkCode.Text & _
                       "&VehCodeTag=" & lblVehCode.Text & _
                       "&VehExpCodeTag=" & lblVehExpCode.Text & _
                       "&SortBy=" & strSortBy & _
                       "&OrderBy=" & strOrderBy & _
                       """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub


    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblLocation.Text = GetCaption(objLangCap.EnumLangCap.Location)
        lblAccCode.Text = GetCaption(objLangCap.EnumLangCap.Account) & lblCode.Text
        If LCase(strCostLevel) = "block" Then
            lblBlkCode.Text = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text
        Else
            lblBlkCode.Text = GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.Text
        End If
        lblVehCode.Text = GetCaption(objLangCap.EnumLangCap.Vehicle) & lblCode.Text
        lblVehExpCode.Text = GetCaption(objLangCap.EnumLangCap.VehExpense) & lblCode.Text
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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PR_STDRPT_LOADERPAY_LANGCAP_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/PR_StdRpt_Selection.aspx")
        End Try
    End Sub




    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

       For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If strLocType = objLoc.EnumLocType.Mill then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                end if
                Exit For
            End If
        Next
    End Function

End Class
