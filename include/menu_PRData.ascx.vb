'***********************************************************************************************************
'No.    Author  Date            Remarks                                                             Patch
'***********************************************************************************************************
'1.     GCH     13 Sep 2004     CR_THG00016 : New data upload screen
'2.     DIAN    22 Aug 2006     Quick Fix Access Rights for UAT Phase 2
'3.     Dian    23 Aug 2006     New Tab for Attendance System Interface
'************************************************* END *****************************************************
Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information

Imports agri.GlobalHdl.clsAccessRights

Public Class menu_PR_Data : Inherits UserControl

    Protected WithEvents tblMenu1 As HtmlTable
    Protected WithEvents tblMenu2 As HtmlTable

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim strUserId As String
    Dim intPRAR As Long

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
        Dim strHrefDownload As String = ""
        Dim strHrefUpload As String = ""
        Dim strHrefTrx As String = ""
        Dim strHrefDownload_Statutory As String = ""
        Dim strHrefDwWages As String = ""
'----- #1 Start -----
        Dim strHrefUpWages As String = ""
'----- #1 End -----
        Dim strHrefDownload_BankAutoCredit As String = ""
'#3 - start
        Dim strHrefDownload_AttdInterface As String = ""
'#3 - end

        If strScriptName = "menu_prdata_page.aspx" Then
            strActiveLeft = "<img src=""../images/dl.gif"" border=0 align=texttop>"
            strActiveRight = "<img src=""../images/dr.gif"" border=0 align=texttop>"
            strInActiveLeft = "<img src=""../images/ll.gif"" border=0 align=texttop>"
            strInActiveRight = "<img src=""../images/lr.gif"" border=0 align=texttop>"
        End If
        
        intPRAR = Session("SS_PRAR")

        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDataTransfer), intPRAR) = True) Then
            strHrefDownload = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/PR/data/PR_data_download.aspx"" target=_self>Download Reference File</a>"
            strHrefUpload = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/PR/data/PR_data_upload.aspx"" target=_self>Upload Reference File</a>"
        Else
            strHrefDownload = "Download Reference File"
            strHrefUpload = "Upload Reference File"
        End IF
' #2 - start
        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDownload), intPRAR) = True) Then
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetAutoDataTransfer(objSysCfg.EnumAutoDataTransfer.Payroll), Session("SS_AUTOGLPOSTING")) = False Then
                strHrefTrx = "<a class=mt-t target=_blank href=""/" & Session("SS_LANGCODE") & "/PR/data/PR_data_trx_savefile.aspx"" target=_self>Download Payroll Transaction</a>"
            Else
                strHrefTrx = "Download Payroll Transaction"
            End If
            'strHrefDownload_Statutory = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/PR/data/PR_data_download_statutory.aspx"" target=_self>Download Statutory File</a>"
            'strHrefDwWages = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/PR/data/PR_data_wages_download.aspx"" target=_self>Download Wages Payment</a>"
'----- #1 Start -----
            'strHrefUpWages = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/PR/data/PR_data_wages_upload.aspx"" target=_self>Upload Wages Payment</a>"
'----- #1 End -----
            'strHrefDownload_BankAutoCredit = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/PR/data/PR_data_download_bank.aspx"" target=_self>Download Bank AutoCredit File</a>"
        Else
            strHrefTrx = "Download Payroll Transaction"
            'strHrefDownload_Statutory = "Download Statutory File"
            'strHrefDwWages = "Download Wages Payment"
'----- #1 Start -----
            'strHrefUpWages = "Upload Wages Payment"
'----- #1 End -----
            'strHrefDownload_BankAutoCredit = "Download Bank AutoCredit File"
        End IF
        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDownload), intPRAR) = True) Then
            strHrefDownload_Statutory = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/PR/data/PR_data_download_statutory.aspx"" target=_self>Download Statutory File</a>"
        Else
            strHrefDownload_Statutory = "Download Statutory File"
        End IF
        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDownloadWages), intPRAR) = True) Then
            strHrefDwWages = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/PR/data/PR_data_wages_download.aspx"" target=_self>Download Wages Payment</a>"
            strHrefUpWages = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/PR/data/PR_data_wages_upload.aspx"" target=_self>Upload Wages Payment</a>"
        Else
            strHrefDwWages = "Download Wages Payment"
            strHrefUpWages = "Upload Wages Payment"
        End IF
        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDownloadBankAuto), intPRAR) = True) Then
            strHrefDownload_BankAutoCredit = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/PR/data/PR_data_download_bank.aspx"" target=_self>Download Bank AutoCredit File</a>"
        Else
            strHrefDownload_BankAutoCredit = "Download Bank AutoCredit File"
        End IF
' #2 - end

' #3 - start
        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDwAttdInterface), intPRAR) = True) Then
            strHrefDownload_AttdInterface = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/PR/data/PR_data_attdinterface.aspx"" target=_self>Upload Attendance System Interface</a>"
        Else
            strHrefDownload_AttdInterface = "Upload Attendance System Interface"
        End IF
' #3 - end
        tblMenu1.Rows(0).Cells(0).bgcolor = "#e5e5e5"
        tblMenu1.Rows(0).Cells(0).innerHTML = strInActiveLeft & strHrefDownload & strInActiveRight
        tblMenu1.Rows(0).Cells(2).bgcolor = "#e5e5e5"
        tblMenu1.Rows(0).Cells(2).innerHTML = strInActiveLeft & strHrefUpload & strInActiveRight
        'tblMenu1.Rows(0).Cells(4).bgcolor = "#e5e5e5"
        'tblMenu1.Rows(0).Cells(4).innerHTML = strInActiveLeft & strHrefTrx & strInActiveRight
' #3 - start
        tblMenu1.Rows(0).Cells(4).bgcolor = "#e5e5e5"
        tblMenu1.Rows(0).Cells(4).innerHTML = strInActiveLeft & strHrefDownload_AttdInterface & strInActiveRight
' #3 - end

        tblMenu2.Rows(0).Cells(0).bgcolor = "#e5e5e5"
        tblMenu2.Rows(0).Cells(0).innerHTML = strInActiveLeft & strHrefDownload_Statutory & strInActiveRight
        tblMenu2.Rows(0).Cells(2).bgcolor = "#e5e5e5"
        tblMenu2.Rows(0).Cells(2).innerHTML = strInActiveLeft & strHrefDwWages & strInActiveRight
'----- #1 Start -----
'        tblMenu2.Rows(0).Cells(4).bgcolor = "#e5e5e5"
'        tblMenu2.Rows(0).Cells(4).innerHTML = strInActiveLeft & strHrefDownload_BankAutoCredit & strInActiveRight
        tblMenu2.Rows(0).Cells(4).bgcolor = "#e5e5e5"
        tblMenu2.Rows(0).Cells(4).innerHTML = strInActiveLeft & strHrefUpWages & strInActiveRight
        tblMenu2.Rows(0).Cells(6).bgcolor = "#e5e5e5"
        tblMenu2.Rows(0).Cells(6).innerHTML = strInActiveLeft & strHrefDownload_BankAutoCredit & strInActiveRight
'----- #1 End -----


        Select Case strScriptName
            Case "pr_data_download.aspx"
                    tblMenu1.Rows(0).Cells(0).bgcolor = "#d4d0c8"
                    tblMenu1.Rows(0).Cells(0).innerHTML = strActiveLeft & strHrefDownload & strActiveRight
            Case "pr_data_upload.aspx"
                    tblMenu1.Rows(0).Cells(2).bgcolor = "#d4d0c8"
                    tblMenu1.Rows(0).Cells(2).innerHTML = strActiveLeft & strHrefUpload & strActiveRight

' #3 - start
            Case "pr_data_attdinterface.aspx"
                    tblMenu1.Rows(0).Cells(4).bgcolor = "#d4d0c8"
                    tblMenu1.Rows(0).Cells(4).innerHTML = strActiveLeft & strHrefDownload_AttdInterface & strActiveRight
' #3 - end

            Case "pr_data_download_statutory.aspx"
                    tblMenu2.Rows(0).Cells(0).bgcolor = "#d4d0c8"
                    tblMenu2.Rows(0).Cells(0).innerHTML = strActiveLeft & strHrefDownload_statutory & strActiveRight
            Case "pr_data_wages_download.aspx"
                    tblMenu2.Rows(0).Cells(2).bgcolor = "#d4d0c8"
                    tblMenu2.Rows(0).Cells(2).innerHTML = strActiveLeft & strHrefDwWages & strActiveRight
'----- #1 Start -----
'            Case "pr_data_download_bank.aspx"
'                    tblMenu2.Rows(0).Cells(4).bgcolor = "#d4d0c8"
'                    tblMenu2.Rows(0).Cells(4).innerHTML = strActiveLeft & strHrefDownload_BankAutoCredit & strActiveRight
            Case "pr_data_wages_upload.aspx"
                    tblMenu2.Rows(0).Cells(4).bgcolor = "#d4d0c8"
                    tblMenu2.Rows(0).Cells(4).innerHTML = strActiveLeft & strHrefUpWages & strActiveRight
            Case "pr_data_download_bank.aspx"
                    tblMenu2.Rows(0).Cells(6).bgcolor = "#d4d0c8"
                    tblMenu2.Rows(0).Cells(6).innerHTML = strActiveLeft & strHrefDownload_BankAutoCredit & strActiveRight
'----- #1 End -----
        End Select
    End Sub

End Class
