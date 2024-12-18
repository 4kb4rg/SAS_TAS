Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information

Imports agri.GlobalHdl.clsAccessRights

Public Class menu_WM_Data : Inherits UserControl

    Protected WithEvents tblMenu1 As HtmlTable
    Protected WithEvents tblMenu2 As HtmlTable

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim strUserId As String
    Dim intWMAR As Integer

    Sub Page_Load(Sender As Object, E As EventArgs)
        strUserId = Session("SS_USERID")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        End If

        Dim strActiveLeft = "<img src=""../../images/dl.gif"" border=0 align=texttop>"
        Dim strActiveRight = "<img src=""../../images/dr.gif"" border=0 align=texttop>"
        Dim strInActiveLeft = "<img src=""../../images/ll.gif"" border=0 align=texttop>"
        Dim strInActiveRight = "<img src=""../../images/lr.gif"" border=0 align=texttop>"

        Dim strScriptPath As String = lcase(Request.ServerVariables("SCRIPT_NAME"))
        Dim arrScriptName As Array = Split(strScriptPath, "/")
        Dim strScriptName As String = arrScriptName(UBound(arrScriptName, 1))
        Dim strHrefDownRef As String = ""
        Dim strHrefUpRef As String = ""
        Dim strHrefUpWeigh As String = ""
        Dim strHrefDownload As String = ""
        Dim strHrefUpload As String = ""

        If strScriptName = "menu_wmdata_page.aspx" Then
            strActiveLeft = "<img src=""../images/dl.gif"" border=0 align=texttop>"
            strActiveRight = "<img src=""../images/dr.gif"" border=0 align=texttop>"
            strInActiveLeft = "<img src=""../images/ll.gif"" border=0 align=texttop>"
            strInActiveRight = "<img src=""../images/lr.gif"" border=0 align=texttop>"
        End If

        intWMAR = Session("SS_WMAR")

        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWMAccessRights.WMDataTransfer), intWMAR) = True) Then
            strHrefDownRef = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/WM/data/WM_data_download.aspx"" target=_self>Download Reference Data</a>"
            strHrefUpRef = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/WM/data/WM_data_upload.aspx"" target=_self>Upload Reference Data</a>"
            strHrefUpWeigh = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/WM/data/WM_data_uploadweigh.aspx"" target=_self>Upload WeighBridge Data</a>"
            strHrefDownload = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/WM/data/WM_data_downloaddisp.aspx"" target=_self>Download Dispatch Data</a>"
            strHrefUpload = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/WM/data/WM_data_uploaddisp.aspx"" target=_self>Upload Dispatch Data</a>"
        Else
            strHrefDownRef = "Download Reference File"
            strHrefUpRef = "Upload Reference File"
            strHrefUpWeigh = "Upload WeighBridge Data"
            strHrefDownload = "Download Dispatch Data"
            strHrefUpload = "Upload Dispatch Data"
        End IF

        tblMenu1.Rows(0).Cells(0).bgcolor = "#e5e5e5"
        tblMenu1.Rows(0).Cells(0).innerHTML = strInActiveLeft & strHrefDownRef & strInActiveRight
        tblMenu1.Rows(0).Cells(2).bgcolor = "#e5e5e5"
        tblMenu1.Rows(0).Cells(2).innerHTML = strInActiveLeft & strHrefUpRef & strInActiveRight

        tblMenu2.Rows(0).Cells(0).bgcolor = "#e5e5e5"
        tblMenu2.Rows(0).Cells(0).innerHTML = strInActiveLeft & strHrefUpWeigh & strInActiveRight
        tblMenu2.Rows(0).Cells(2).bgcolor = "#e5e5e5"
        tblMenu2.Rows(0).Cells(2).innerHTML = strInActiveLeft & strHrefDownload & strInActiveRight
        tblMenu2.Rows(0).Cells(4).bgcolor = "#e5e5e5"
        tblMenu2.Rows(0).Cells(4).innerHTML = strInActiveLeft & strHrefUpload & strInActiveRight

        Select Case strScriptName
            Case "wm_data_download.aspx"
                    tblMenu1.Rows(0).Cells(0).bgcolor = "#d4d0c8"
                    tblMenu1.Rows(0).Cells(0).innerHTML = strActiveLeft & strHrefDownRef & strActiveRight
            Case "wm_data_upload.aspx"
                    tblMenu1.Rows(0).Cells(2).bgcolor = "#d4d0c8"
                    tblMenu1.Rows(0).Cells(2).innerHTML = strActiveLeft & strHrefUpRef & strActiveRight
            Case "wm_data_uploadweigh.aspx"
                    tblMenu2.Rows(0).Cells(0).bgcolor = "#d4d0c8"
                    tblMenu2.Rows(0).Cells(0).innerHTML = strActiveLeft & strHrefUpWeigh & strActiveRight
            Case "wm_data_downloaddisp.aspx"
                    tblMenu2.Rows(0).Cells(2).bgcolor = "#d4d0c8"
                    tblMenu2.Rows(0).Cells(2).innerHTML = strActiveLeft & strHrefDownload & strActiveRight
            Case "wm_data_uploaddisp.aspx"
                    tblMenu2.Rows(0).Cells(4).bgcolor = "#d4d0c8"
                    tblMenu2.Rows(0).Cells(4).innerHTML = strActiveLeft & strHrefUpload & strActiveRight
        End Select

    End Sub
End Class
