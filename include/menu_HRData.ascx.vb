Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information

Imports agri.GlobalHdl.clsAccessRights
Imports agri.PWSystem.clsConfig

Public Class menu_HR_Data : Inherits UserControl

    Protected WithEvents tblMenu1 As HtmlTable

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim strUserId As String
    Dim intConfigSetting As Integer
    Dim intHRAR As Long

    Sub Page_Load(Sender As Object, E As EventArgs)
        strUserId = Session("SS_USERID")
        intHRAR = Session("SS_HRAR")
        intConfigSetting = Session("SS_CONFIGSETTING")

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
        Dim strEmpCode As String = ""
        Dim strHrefDownload As String = ""
        Dim strHrefUpload As String = ""

        If strScriptName = "menu_hrdata_page.aspx" Then
            strActiveLeft = "<img src=""../images/dl.gif"" border=0 align=texttop>"
            strActiveRight = "<img src=""../images/dr.gif"" border=0 align=texttop>"
            strInActiveLeft = "<img src=""../images/ll.gif"" border=0 align=texttop>"
            strInActiveRight = "<img src=""../images/lr.gif"" border=0 align=texttop>"
        End If
        
        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRGenerateEmpCode), intHRAR) = True) And _
           (objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.AutoEmpCode), intConfigSetting) = True) Then
            strEmpCode = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/HR/data/HR_data_Generate_EmpCode.aspx"" target=_self>Generate Employee Code</a>"
        Else
            strEmpCode = "Generate Employee Code"
        End IF

        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRDataTransfer), intHRAR) = True) Then
            strHrefDownload = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/HR/data/HR_data_download.aspx"" target=_self>Download Reference File</a>"
            strHrefUpload = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/HR/data/HR_data_upload.aspx"" target=_self>Upload Reference File</a>"
        Else
            strHrefDownload = "Download Reference File"
            strHrefUpload = "Upload Reference File"
        End IF

        tblMenu1.Rows(0).Cells(0).bgcolor = "#e5e5e5"
        tblMenu1.Rows(0).Cells(0).innerHTML = strInActiveLeft & strEmpCode & strInActiveRight
        tblMenu1.Rows(0).Cells(2).bgcolor = "#e5e5e5"
        tblMenu1.Rows(0).Cells(2).innerHTML = strInActiveLeft & strHrefDownload & strInActiveRight
        tblMenu1.Rows(0).Cells(4).bgcolor = "#e5e5e5"
        tblMenu1.Rows(0).Cells(4).innerHTML = strInActiveLeft & strHrefUpload & strInActiveRight

        Select Case strScriptName
            Case "hr_data_generate_empcode.aspx"
                    tblMenu1.Rows(0).Cells(0).bgcolor = "#d4d0c8"
                    tblMenu1.Rows(0).Cells(0).innerHTML = strActiveLeft & strEmpCode & strActiveRight
            Case "hr_data_download.aspx"
                    tblMenu1.Rows(0).Cells(2).bgcolor = "#d4d0c8"
                    tblMenu1.Rows(0).Cells(2).innerHTML = strActiveLeft & strHrefDownload & strActiveRight
            Case "hr_data_upload.aspx"
                    tblMenu1.Rows(0).Cells(4).bgcolor = "#d4d0c8"
                    tblMenu1.Rows(0).Cells(4).innerHTML = strActiveLeft & strHrefUpload & strActiveRight
        End Select

    End Sub
End Class
