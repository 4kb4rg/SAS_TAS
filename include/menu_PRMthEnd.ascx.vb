'**********************************************************************************************
'1.     BHL     03 Oct 2005     Add New Tab "Bonus Process"
'2.     SMN     23 June 2006    Add New Tab "Transfer Interfaces"  FS 2.31.5 Minamas  
'3.     DIAN    19 Aug 2006     Quick Fix Access Rights for UAT phase 2
'**********************************************************************************************
Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information

Imports agri.GlobalHdl.clsAccessRights

Public Class menu_PR_MthEnd : Inherits UserControl

    Protected WithEvents tblMenu As HtmlTable

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objLangCapDs As New Object()
    Dim strLangCode As New Object()
    Dim strUserId As String
    Dim intPRAR As Long
    Dim strRiceRationTag As String
	'add by alim
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String
	'End of Add by alim

    Sub Page_Load(Sender As Object, E As EventArgs)
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
	    'add by alim
        strLocType = Session("SS_LOCTYPE")
        'End of Add by alim
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
        Dim strHrefRiceRation As String = ""
        '--- #1 ---
        Dim strHrefBonus as string  = ""
        Dim strHrefTHR As String = ""
        Dim strHrefProcess As String = ""
        Dim strHrefRollBack As String = ""
        Dim strHrefMthEnd As String = ""
        Dim strHrefDailyProcess As string = ""
        Dim strHrefDailyRollback As string = ""
        Dim strHrefRapel As string = ""
        Dim strHrefTransfer As string = ""

        If strScriptName = "menu_prmthend_page.aspx" Then
            strActiveLeft = "<img src=""../images/dl.gif"" border=0 align=texttop>"
            strActiveRight = "<img src=""../images/dr.gif"" border=0 align=texttop>"
            strInActiveLeft = "<img src=""../images/ll.gif"" border=0 align=texttop>"
            strInActiveRight = "<img src=""../images/lr.gif"" border=0 align=texttop>"
        End If

        onload_GetLangCap()
        intPRAR = Session("SS_PRAR")
' #3 - start
        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMonthEnd), intPRAR) = True) Then
            'strHrefRiceRation = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/PR/mthend/PR_mthend_riceration.aspx"" target=_self>" & strRiceRationTag & "</a>"
            'strHrefRapel = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/PR/mthend/PR_mthend_RapelProcess.aspx"" target=_self>Rapel</a>"
            '--- #1 ---
            'strHrefBonus = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/PR/mthend/PR_mthend_BonusProcess.aspx"" target=_self>Bonus Process</a>"
            'strHrefTHR = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/PR/mthend/PR_mthend_THRProcess.aspx"" target=_self> THR </a>"
            'strHrefDailyProcess = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/PR/mthend/PR_mthend_dailyprocess.aspx"" target=_self>Daily Process</a>"
            'strHrefDailyRollback = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/PR/mthend/PR_mthend_dailyrollback.aspx"" target=_self>Daily Rollback</a>"
            'strHrefProcess = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/PR/mthend/PR_mthend_payrollprocess.aspx"" target=_self>Payroll Process</a>"
            'strHrefRollBack = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/PR/mthend/PR_mthend_payrollrollback.aspx"" target=_self>Payroll RollBack</a>"
            strHrefMthEnd = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/PR/mthend/PR_mthend_Process.aspx"" target=_self>Month End Process</a>"
            '2# [S]
            'strHrefTransfer = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/PR/mthend/PR_mthend_Transfer.aspx"" target=_self>Transfer Interface</a>"
            '2# [E]
        Else
            'strHrefRiceRation = strRiceRationTag
            'strHrefRapel = "Rapel"
            '--- #1 ---
            'strHrefBonus = "Bonus Process"
            'strHrefTHR = " THR "
            'strHrefDailyProcess = "Daily Process"
            'strHrefDailyRollback = "Daily Rollback"
            'strHrefProcess = "Payroll Process"
            'strHrefRollBack = "Payroll RollBack"
            strHrefMthEnd = "Month End Process"
            '2# [S]
            'strHrefTransfer = "Transfer Interface"
            '2# [E]
        End IF
        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthRice), intPRAR) = True) Then
            strHrefRiceRation = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/PR/mthend/PR_mthend_riceration.aspx"" target=_self>" & strRiceRationTag & "</a>"
        Else
            strHrefRiceRation = strRiceRationTag
        End IF
        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthRapel), intPRAR) = True) Then
            strHrefRapel = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/PR/mthend/PR_mthend_RapelProcess.aspx"" target=_self>Rapel</a>"
        Else
            strHrefRapel = "Rapel"
        End IF
        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthBonus), intPRAR) = True) Then
            strHrefBonus = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/PR/mthend/PR_mthend_BonusProcess.aspx"" target=_self>Bonus Process</a>"
        Else
            strHrefBonus = "Bonus Process"
        End IF
        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthTHR), intPRAR) = True) Then
            strHrefTHR = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/PR/mthend/PR_mthend_THRProcess.aspx"" target=_self> THR </a>"
        Else
            strHrefTHR = " THR "
        End IF
        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthDaily), intPRAR) = True) Then
            strHrefDailyProcess = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/PR/mthend/PR_mthend_dailyprocess.aspx"" target=_self>Daily Process</a>"
            strHrefDailyRollback = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/PR/mthend/PR_mthend_dailyrollback.aspx"" target=_self>Daily Rollback</a>"
        Else
            strHrefDailyProcess = "Daily Process"
            strHrefDailyRollback = "Daily Rollback"
        End IF
        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthPayroll), intPRAR) = True) Then
            strHrefProcess = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/PR/mthend/PR_mthend_payrollprocess.aspx"" target=_self>Payroll Process</a>"
            strHrefRollBack = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/PR/mthend/PR_mthend_payrollrollback.aspx"" target=_self>Payroll RollBack</a>"
        Else
            strHrefProcess = "Payroll Process"
            strHrefRollBack = "Payroll RollBack"
        End IF
        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthTransfer), intPRAR) = True) Then
            '2# [S]
            strHrefTransfer = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/PR/mthend/PR_mthend_Transfer.aspx"" target=_self>Transfer Interface</a>"
            '2# [E]
        Else
            '2# [S]
            strHrefTransfer = "Transfer Interface"
            '2# [E]
        End IF
' #3 - start
        tblMenu.Rows(0).Cells(0).bgcolor = "#e5e5e5"
        tblMenu.Rows(0).Cells(0).innerHTML = strInActiveLeft & strHrefRiceRation & strInActiveRight
        tblMenu.Rows(0).Cells(2).bgcolor = "#e5e5e5"
        tblMenu.Rows(0).Cells(2).innerHTML = strInActiveLeft & strHrefRapel & strInActiveRight
        tblMenu.Rows(0).Cells(4).bgcolor = "#e5e5e5"
        tblMenu.Rows(0).Cells(4).innerHTML = strInActiveLeft & strHrefBonus & strInActiveRight
        tblMenu.Rows(0).Cells(6).bgcolor = "#e5e5e5"
        tblMenu.Rows(0).Cells(6).innerHTML = strInActiveLeft & strHrefTHR & strInActiveRight
        tblMenu.Rows(0).Cells(8).bgcolor = "#e5e5e5"
        tblMenu.Rows(0).Cells(8).innerHTML = strInActiveLeft & strHrefDailyProcess & strInActiveRight
        tblMenu.Rows(0).Cells(10).bgcolor = "#e5e5e5"
        tblMenu.Rows(0).Cells(10).innerHTML = strInActiveLeft & strHrefDailyRollback & strInActiveRight
        tblMenu.Rows(0).Cells(12).bgcolor = "#e5e5e5"
        tblMenu.Rows(0).Cells(12).innerHTML = strInActiveLeft & strHrefProcess & strInActiveRight
        tblMenu.Rows(0).Cells(14).bgcolor = "#e5e5e5"
        tblMenu.Rows(0).Cells(14).innerHTML = strInActiveLeft & strHrefRollBack & strInActiveRight
        tblMenu.Rows(0).Cells(16).bgcolor = "#e5e5e5"
        tblMenu.Rows(0).Cells(16).innerHTML = strInActiveLeft & strHrefMthEnd & strInActiveRight
        '#2 [S]
        tblMenu.Rows(0).Cells(18).bgcolor = "#e5e5e5"
        tblMenu.Rows(0).Cells(18).innerHTML = strInActiveLeft & strHrefTransfer & strInActiveRight
        '#2 [E]        
        Select Case strScriptName
            Case "pr_mthend_riceration.aspx"
                tblMenu.Rows(0).Cells(0).bgcolor = "#d4d0c8"
                tblMenu.Rows(0).Cells(0).innerHTML = strActiveLeft & strHrefRiceRation & strActiveRight
            Case "pr_mthend_rapelprocess.aspx"
                tblMenu.Rows(0).Cells(2).bgcolor = "#d4d0c8"
                tblMenu.Rows(0).Cells(2).innerHTML = strActiveLeft & strHrefRapel & strActiveRight
            Case "pr_mthend_bonusprocess.aspx"
                tblMenu.Rows(0).Cells(4).bgcolor = "#d4d0c8"
                tblMenu.Rows(0).Cells(4).innerHTML = strActiveLeft & strHrefBonus & strActiveRight
            Case "pr_mthend_thrprocess.aspx"
                tblMenu.Rows(0).Cells(6).bgcolor = "#d4d0c8"
                tblMenu.Rows(0).Cells(6).innerHTML = strActiveLeft & strHrefTHR & strActiveRight
            Case "pr_mthend_dailyprocess.aspx"
                tblMenu.Rows(0).Cells(8).bgcolor = "#d4d0c8"
                tblMenu.Rows(0).Cells(8).innerHTML = strActiveLeft & strHrefDailyProcess & strActiveRight
            Case "pr_mthend_dailyrollback.aspx"
                tblMenu.Rows(0).Cells(10).bgcolor = "#d4d0c8"
                tblMenu.Rows(0).Cells(10).innerHTML = strActiveLeft & strHrefDailyRollback & strActiveRight
            Case "pr_mthend_payrollprocess.aspx"
                tblMenu.Rows(0).Cells(12).bgcolor = "#d4d0c8"
                tblMenu.Rows(0).Cells(12).innerHTML = strActiveLeft & strHrefProcess & strActiveRight
            Case "pr_mthend_payrollrollback.aspx"
                tblMenu.Rows(0).Cells(14).bgcolor = "#d4d0c8"
                tblMenu.Rows(0).Cells(14).innerHTML = strActiveLeft & strHrefRollBack & strActiveRight
            Case "pr_mthend_process.aspx"
                tblMenu.Rows(0).Cells(16).bgcolor = "#d4d0c8"
                tblMenu.Rows(0).Cells(16).innerHTML = strActiveLeft & strHrefMthEnd & strActiveRight
            Case "pr_mthend_transfer.aspx"
                tblMenu.Rows(0).Cells(18).bgcolor = "#d4d0c8"
                tblMenu.Rows(0).Cells(18).innerHTML = strActiveLeft & strHrefTransfer & strActiveRight
        
        End Select

    End Sub

    '=== For Language Caption==================================================
    Sub onload_GetLangCap()
        GetEntireLangCap()
        strRiceRationTag = GetCaption(objLangCap.EnumLangCap.RiceRation)
    End Sub

    Sub GetEntireLangCap()
        Dim strOpCode_BussTerm As String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
        Dim strParam As String
        Dim intErrNo As Integer

        strParam = strLangCode
        Try
            intErrNo = objLangCap.mtdGetBussTerm(strOpCode_BussTerm, _
                                                 "", _
                                                 "", _
                                                 strUserId, _
                                                 "", _
                                                 "", _
                                                 objLangCapDs, _
                                                 strParam)
        Catch Exp As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=MENU_PRMTHEND_LANGCAP&errmesg=&redirect=")
        End Try
    End Sub

    
    'add by alim
    Function GetCaption(ByVal pv_TermCode) As String
            Dim count As Integer

            For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
                If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                    If strLocType = objAdminLoc.EnumLocType.Mill then
                        Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                    else
                        Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                    end if
                    'Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                    Exit For
                End If
            Next
        End Function
    'End of Add by alim

    '=====End for Language Caption ===============================================
End Class
