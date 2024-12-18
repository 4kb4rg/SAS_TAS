Imports System
Imports System.Data
Imports System.IO 
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Information
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction

Imports agri.Admin
Imports agri.GlobalHdl.clsGlobalHdl


Public Class IN_Rpt_StockIssueDetPrompt : Inherits Page

    Protected WithEvents rdDisplayCost As RadioButtonList
    Protected WithEvents lblErrMessage As Label

    Dim objAdmin As New agri.Admin.clsShare()

    Dim objActDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        
    End Sub

    Sub btnPreview_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strOpCodePrint As String = "ADMIN_SHARE_UPD_PRINTDATE"
        Dim strUpdString As String = ""
        Dim strStatus As String
        Dim intStatus As Integer
        Dim strSortLine As String
        Dim strPrintDate As String
        Dim strTable As String
        Dim strStockTxId As String
        Dim strIssueType As String

        strStockTxId = Trim(lblStckTxID.Text)

        strUpdString = "where StockIssueID = '" & strStockTxId & "'"
        strStatus = Trim(Status.Text)
        intStatus = CInt(Trim(lblStatusHid.Text))
        strPrintDate = Trim(lblPrintDate.Text)
        strIssueType = Trim(IssueType.Text)
        strTable = "IN_STOCKISSUE"
        strSortLine = ""


        If intStatus = objINtx.EnumStockIssueStatus.Confirmed Then
            If strPrintDate = "" Then
                Try
                    intErrNo = objAdmin.mtdUpdPrintDate(strOpCodePrint, _
                                                        strUpdString, _
                                                        strTable, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId)
                Catch Exp As System.Exception
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADMIN_SHARE_UPD_PRINTDATE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockIssue_List.aspx")
                End Try
                LoadStockTxDetails()
                DisplayFromDB()
                PageControl()
            Else
                strStatus = strStatus & " (re-printed)"
            End If
        End If

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/IN_Rpt_StockIssueDet.aspx?strStockIssueId=" & strStockTxId & _
                       "&strPrintDate=" & strPrintDate & "&strStatus=" & strStatus & "&strIssueType=" & strIssueType & "&strSortLine=" & strSortLine & """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub



End Class
