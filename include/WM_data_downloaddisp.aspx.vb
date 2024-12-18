Imports System
Imports System.Data
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction
Imports Microsoft.VisualBasic.FileSystem
Imports Microsoft.VisualBasic


Public Class WM_data_downloaddisp : Inherits Page

    Protected WithEvents btnGenerate As ImageButton
    Protected WithEvents txtBatchNo As TextBox
    Protected WithEvents txtFromDelDate As TextBox
    Protected WithEvents txtToDelDate As TextBox
    Protected WithEvents lblErrBatchNo As Label
    Protected WithEvents lblErrDelivery As Label
    Protected WithEvents lblErrDateFormat As Label

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strDateFmt As String
    Dim intWMAR As Integer

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strDateFmt = Session("SS_DATEFMT")
        intWMAR = Session("SS_WMAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWMAccessRights.WMDataTransfer), intWMAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrBatchNo.Visible=False
            lblErrDelivery.Visible=False
            lblErrDateFormat.Visible=False
        End If
    End Sub

    Sub btnGenerate_Click(Sender As Object, E As ImageClickEventArgs)
        Dim objFormatDate As String
        Dim objActualDate As String
        Dim strQuery As String

        If txtBatchNo.Text <> "" And txtFromDelDate.Text <> "" Then
            lblErrBatchNo.Visible = True
            Exit Sub
        End If

        If (txtFromDelDate.Text = "" And txtToDelDate.Text <> "") Or _
           (txtFromDelDate.Text <> "" And txtToDelDate.Text = "") Then
            lblErrDelivery.Visible = True
            Exit Sub
        End If

        If txtFromDelDate.Text <> "" Then
            If objGlobal.mtdValidInputDate(strDateFmt, _
                                        txtFromDelDate.Text, _
                                        objFormatDate, _
                                        objActualDate) = False Then
                lblErrDateFormat.Visible = True
                lblErrDateFormat.Text = lblErrDateFormat.Text & objFormatDate
                Exit Sub
            Else
                strQuery = "from=" & objActualDate & "&"
            End If
        End If

        If txtToDelDate.Text <> "" Then
            If objGlobal.mtdValidInputDate(strDateFmt, _
                                        txtToDelDate.Text, _
                                        objFormatDate, _
                                        objActualDate) = False Then
                lblErrDateFormat.Visible = True
                lblErrDateFormat.Text = lblErrDateFormat.Text & objFormatDate
                Exit Sub
            Else
                strQuery += "to=" & objActualDate
            End If
        End If

        If txtBatchNo.Text <> "" Then
            strQuery = "batchno=" & txtBatchNo.Text
        End If

        Response.Redirect("WM_data_downloaddisp_savefile.aspx?" & strQuery)
    End Sub

End Class
