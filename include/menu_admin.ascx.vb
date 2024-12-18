'*******************************************************************************************************************
'1. DIAN    13 July 2006    Minamas Phase 2 Part 2  FS 2.37 New Screen : Nearest Location
'2. DIAN    19 Aug 2006     Quick Fix Access Rights for UAT phase 2
'*******************************************************************************************************************
Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information

Imports agri.GlobalHdl.clsAccessRights
Imports agri.PWSystem.clsLangCap

Public Class menu_admin : Inherits UserControl

    Protected WithEvents tblMenu As HtmlTable

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objLangCapDs As New Object()
    Dim strUserId As String
    Dim strAccMonth As String = ""
    Dim strAccYear As String = ""
    Dim strLocation As String
    Dim strLocTag As String
    Dim strCompTag As String
    Dim intADAR As Integer
    Dim strLangCode As String
    '#1 - [S]
    Dim strNearestLocTag As String
    '#1 - [E]

    Sub Page_Load(Sender As Object, E As EventArgs)
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        End If
        onload_GetLangCap()

        Dim strActiveLeft = "<img src=""../../images/dl.gif"" border=0 align=texttop>"
        Dim strActiveRight = "<img src=""../../images/dr.gif"" border=0 align=texttop>"
        Dim strInActiveLeft = "<img src=""../../images/ll.gif"" border=0 align=texttop>"
        Dim strInActiveRight = "<img src=""../../images/lr.gif"" border=0 align=texttop>"

        Dim strScriptPath As String = lcase(Request.ServerVariables("SCRIPT_NAME"))
        Dim arrScriptName As Array = Split(strScriptPath, "/")
        Dim strScriptName As String = arrScriptName(UBound(arrScriptName, 1))
        Dim strHrefCo As String = ""
        Dim strHrefLoc As String = ""
        '#1 - [S]
        Dim strHrefNearestLoc As String = ""
        '#1 - [E]
        Dim strHrefAccPeriod As String = ""
        Dim strPeriodConfig As String = ""
        Dim strUOM As String = ""
        Dim strUOMCon As String = ""

        If strScriptName = "menu_admin_page.aspx" Then
            strActiveLeft = "<img src=""../images/dl.gif"" border=0 align=texttop>"
            strActiveRight = "<img src=""../images/dr.gif"" border=0 align=texttop>"
            strInActiveLeft = "<img src=""../images/ll.gif"" border=0 align=texttop>"
            strInActiveRight = "<img src=""../images/lr.gif"" border=0 align=texttop>"
        End If

        strLocation = Session("SS_LOCATION")
        intADAR = Session("SS_ADAR")

        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADCompany), intADAR) = True) Then
            strHrefCo = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/admin/company/admin_comp_list.aspx"" target=_self>" & strCompTag & "</a>"
        Else
            strHrefCo = strCompTag
        End If

        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADLocation), intADAR) = True) Then
            strHrefLoc = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/admin/location/Admin_location_LocList.aspx"" target=_self>" & strLocTag & "</a>"
        Else
            strHrefLoc = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/admin/location/Admin_location_LocList.aspx"" target=_self>" & strLocTag & "</a>"
        End If
'#1 - [S]
        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADNearestLocation), intADAR) = True) Then
            strHrefNearestLoc = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/admin/location/Admin_location_NearestLocList.aspx"" target=_self>" & strNearestLocTag & "</a>"
        Else
            strHrefNearestLoc = strNearestLocTag
        End If
'#1 - [E]

        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADUOM), intADAR) = True) Then
            strUOM = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/admin/uom/Admin_uom_UOMList.aspx"" target=_self>Unit of Measurement</a>"
            strUOMCon = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/admin/uom/Admin_uom_UOMConvertionList.aspx"" target=_self>UOM Convertion</a>"
        Else
            strUOM = "Unit of Measurement"
            strUOMCon = "UOM Convertion"
        End If
' # 2 - start
        strHrefAccPeriod = "Account Period"
        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADAccPeriod), intADAR) = True) Then
            'strPeriodConfig = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/admin/accperiod/admin_accperiod_cfglist.aspx"" target=_self>Period Configuration</a>"
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADUser), intADAR) = True) Or _
                (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADSystemConfig), intADAR) = True) Or _
                (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADLanguageCaption), intADAR) = True) Then
                '--Do Nothing
            Else
                If strLocation <> "" Then
                    strHrefAccPeriod = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/admin/accperiod/admin_accperiod.aspx"" target=_self>Account Period</a>"
                End If
            End If
        'Else
            'strPeriodConfig = "Period Configuration"
        End If

        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADPeriodCfg), intADAR) = True) Then
            strPeriodConfig = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/admin/accperiod/admin_accperiod_cfglist.aspx"" target=_self>Period Configuration</a>"
        Else
            strPeriodConfig = "Period Configuration"
        End If
' # 2 - start
        '#1 - [S]
        'tblMenu.Rows(0).Cells(0).bgcolor = "#e5e5e5"
        'tblMenu.Rows(0).Cells(0).innerHTML = strInActiveLeft & strHrefCo & strInActiveRight
        'tblMenu.Rows(0).Cells(2).bgcolor = "#e5e5e5"
        'tblMenu.Rows(0).Cells(2).innerHTML = strInActiveLeft & strHrefLoc & strInActiveRight
        'tblMenu.Rows(0).Cells(4).bgcolor = "#e5e5e5"
        'tblMenu.Rows(0).Cells(4).innerHTML = strInActiveLeft & strUOM & strInActiveRight
        'tblMenu.Rows(0).Cells(6).bgcolor = "#e5e5e5"
        'tblMenu.Rows(0).Cells(6).innerHTML = strInActiveLeft & strUOMCon & strInActiveRight
        'tblMenu.Rows(0).Cells(8).bgcolor = "#e5e5e5"
        'tblMenu.Rows(0).Cells(8).innerHTML = strInActiveLeft & strPeriodConfig & strInActiveRight
        'tblMenu.Rows(0).Cells(10).bgcolor = "#e5e5e5"
        'tblMenu.Rows(0).Cells(10).innerHTML = strInActiveLeft & strHrefAccPeriod & strInActiveRight


        tblMenu.Rows(0).Cells(0).bgcolor = "#e5e5e5"
        tblMenu.Rows(0).Cells(0).innerHTML = strInActiveLeft & strHrefCo & strInActiveRight
        tblMenu.Rows(0).Cells(2).bgcolor = "#e5e5e5"
        tblMenu.Rows(0).Cells(2).innerHTML = strInActiveLeft & strHrefLoc & strInActiveRight
        tblMenu.Rows(0).Cells(4).bgcolor = "#e5e5e5"
        tblMenu.Rows(0).Cells(4).innerHTML = strInActiveLeft & strHrefNearestLoc & strInActiveRight
        tblMenu.Rows(0).Cells(6).bgcolor = "#e5e5e5"
        tblMenu.Rows(0).Cells(6).innerHTML = strInActiveLeft & strUOM & strInActiveRight
        tblMenu.Rows(0).Cells(8).bgcolor = "#e5e5e5"
        tblMenu.Rows(0).Cells(8).innerHTML = strInActiveLeft & strUOMCon & strInActiveRight
        tblMenu.Rows(0).Cells(10).bgcolor = "#e5e5e5"
        tblMenu.Rows(0).Cells(10).innerHTML = strInActiveLeft & strPeriodConfig & strInActiveRight
        tblMenu.Rows(0).Cells(12).bgcolor = "#e5e5e5"
        tblMenu.Rows(0).Cells(12).innerHTML = strInActiveLeft & strHrefAccPeriod & strInActiveRight

        'Select Case strScriptName
        '    Case "admin_comp_list.aspx", "admin_comp_det.aspx"
        '            tblMenu.Rows(0).Cells(0).bgcolor = "#d4d0c8"
        '            tblMenu.Rows(0).Cells(0).innerHTML = strActiveLeft & strHrefCo & strActiveRight
        '    Case "admin_location_loclist.aspx", "admin_location_locdet.aspx"
        '            tblMenu.Rows(0).Cells(2).bgcolor = "#d4d0c8"
        '            tblMenu.Rows(0).Cells(2).innerHTML = strActiveLeft & strHrefLoc & strActiveRight
        '    Case "admin_uom_uomlist.aspx"
        '            tblMenu.Rows(0).Cells(4).bgcolor = "#d4d0c8"
        '            tblMenu.Rows(0).Cells(4).innerHTML = strActiveLeft & strUOM & strActiveRight
        '    Case "admin_uom_uomconvertionlist.aspx", "admin_uom_uomconvertiondet.aspx"
        '            tblMenu.Rows(0).Cells(6).bgcolor = "#d4d0c8"
        '            tblMenu.Rows(0).Cells(6).innerHTML = strActiveLeft & strUOMCon & strActiveRight
        '    Case "admin_accperiod_cfglist.aspx"
        '            tblMenu.Rows(0).Cells(8).bgcolor = "#d4d0c8"
        '            tblMenu.Rows(0).Cells(8).innerHTML = strActiveLeft & strPeriodConfig & strActiveRight
        '    Case "admin_accperiod.aspx"
        '            tblMenu.Rows(0).Cells(10).bgcolor = "#d4d0c8"
        '            tblMenu.Rows(0).Cells(10).innerHTML = strActiveLeft & strHrefAccPeriod & strActiveRight
        'End Select

        'Merge Code 10/08/2006
        Select Case strScriptName
            Case "admin_comp_list.aspx", "admin_comp_det.aspx"
                    tblMenu.Rows(0).Cells(0).bgcolor = "#d4d0c8"
                    tblMenu.Rows(0).Cells(0).innerHTML = strActiveLeft & strHrefCo & strActiveRight
            Case "admin_location_loclist.aspx", "admin_location_locdet.aspx"
                    tblMenu.Rows(0).Cells(2).bgcolor = "#d4d0c8"
                    tblMenu.Rows(0).Cells(2).innerHTML = strActiveLeft & strHrefLoc & strActiveRight
            Case "admin_location_nearestloclist.aspx"
                    tblMenu.Rows(0).Cells(4).bgcolor = "#d4d0c8"
                    tblMenu.Rows(0).Cells(4).innerHTML = strActiveLeft & strHrefNearestLoc & strActiveRight
            Case "admin_uom_uomlist.aspx"
                    tblMenu.Rows(0).Cells(6).bgcolor = "#d4d0c8"
                    tblMenu.Rows(0).Cells(6).innerHTML = strActiveLeft & strUOM & strActiveRight
            Case "admin_uom_uomconvertionlist.aspx", "admin_uom_uomconvertiondet.aspx"
                    tblMenu.Rows(0).Cells(8).bgcolor = "#d4d0c8"
                    tblMenu.Rows(0).Cells(8).innerHTML = strActiveLeft & strUOMCon & strActiveRight
            Case "admin_accperiod_cfglist.aspx"
                    tblMenu.Rows(0).Cells(10).bgcolor = "#d4d0c8"
                    tblMenu.Rows(0).Cells(10).innerHTML = strActiveLeft & strPeriodConfig & strActiveRight
            Case "admin_accperiod.aspx"
                    tblMenu.Rows(0).Cells(12).bgcolor = "#d4d0c8"
                    tblMenu.Rows(0).Cells(12).innerHTML = strActiveLeft & strHrefAccPeriod & strActiveRight
        End Select

    End Sub

    '=== For Language Caption==================================================
    Sub onload_GetLangCap()
        GetEntireLangCap()
        strCompTag = GetCaption(objLangCap.EnumLangCap.Company)
        strLocTag = GetCaption(objLangCap.EnumLangCap.Location)
        strNearestLocTag = GetCaption(objLangCap.EnumLangCap.NearestLocation)
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
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=MENU_ADMIN&errmesg=&redirect=")
        End Try
    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer
        
        For count=0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode"))
                Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                Exit For
            End If
        Next
    End Function

    '=====End for Language Caption ===============================================
End Class
