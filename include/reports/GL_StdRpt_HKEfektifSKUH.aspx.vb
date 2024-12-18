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

Public Class GL_StdRpt_HKEfektifSKUH : Inherits Page

    Protected RptSelect As UserControl

    Dim objGL As New agri.GL.clsReport()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    
    Protected WithEvents hidAccMonthPX As HtmlInputHidden
    Protected WithEvents hidAccYearPX As HtmlInputHidden
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label    
    Protected WithEvents ddlPhyMonthFrom As DropDownList
    Protected WithEvents txtPhyYearFrom As TextBox
    Protected WithEvents ddlPhyMonthTo As DropDownList
    Protected WithEvents txtPhyYearTo As TextBox
    Protected WithEvents txtRemarks As TextBox



    Protected WithEvents PrintPrev As ImageButton


    Dim TrMthYr As HtmlTableRow

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfigsetting As Integer
    Dim dr As DataRow
    Dim intCnt As Integer
    Dim intErrNo As Integer
    Dim tempActGrp As String
    Dim strResult As String
    Dim strUserLocation as String
    Dim strPhyYear as String 
    Dim strPhyMonth as String 

    Dim objLangCapDs As New Object()
    Dim objDataSet As New Object()
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String
    Dim strRemarks as string 

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strPhyYear = Session("SS_PHYYEAR")
        strPhyMonth = Session("SS_PHYMONTH")
        strLocType = Session("SS_LOCTYPE")

        Dim rdblist as RadioButtonList

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
                onload_GetLangCap()
                txtPhyYearFrom.Text = strPhyYear
                BindPhyMonthList1(12)
                txtPhyYearTo.Text = strPhyYear
                BindPhyMonthList2(12)
            End If
        End If
    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim htmltr As HtmlTableRow

        htmltr = RptSelect.FindControl("TrMthYr")
        htmltr.Visible = True
        htmltr = RptSelect.FindControl("TrCheckLoc")
        htmltr.Visible = True
        htmltr = RptSelect.FindControl("TrRadioLoc")
        htmltr.Visible = False
        htmltr = RptSelect.FindControl("TrDivision")
        htmltr.Visible = True

        If Page.IsPostBack Then
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
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
        Dim strRptId As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String
        Dim strSrchAccCode As String
        Dim strSrchBlkCode As String
        Dim strSrchVehCode As String
        Dim strSrchVehExpCode As String
        Dim strSupp As String
        Dim strAccType As String
        Dim strAccTypeText As String
        Dim strWithTrans As String
        Dim strEstExpense As String
        Dim strParam As String
        Dim strDateSetting As String
        Dim objSysCfgDs As New Object()
        Dim intCntActGrp As Integer
        Dim strDivision As String
        Dim ddlist As DropDownList
        Dim rdblist As RadioButtonList
        Dim intCnt As Integer
        Dim strPhyMonth1 as String
        Dim strPhyYear1 as String
        Dim strPhyMonth2 as String
        Dim strPhyYear2 as String
        Dim strOppCd_GET As String = "GL_CLSSETUP_BLOCKGROUP_LIST_GET"
        Dim SearchStr as String = ""
        Dim sortItem as String = ""
        Dim tempUserLoc As HtmlInputHidden
        Dim templblUL As Label


        ddlist = RptSelect.FindControl("lstAccMonth")
        strddlAccMth = Trim(ddlist.SelectedItem.Value)

        ddlist = RptSelect.FindControl("lstAccYear")
        strddlAccYr = Trim(ddlist.SelectedItem.Value)

        ddlist = RptSelect.FindControl("lstRptName")
        strRptId = Trim(ddlist.SelectedItem.Value)

        ddlist = RptSelect.FindControl("lstRptName")
        strRptName = Trim(ddlist.SelectedItem.Text)

        ddlist = RptSelect.FindControl("lstDecimal")
        strDec = Trim(ddlist.SelectedItem.Value)


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
            ElseIf Left(strUserLoc, 1) = "," Then
                strUserLoc = Right(strUserLoc, Len(strUserLoc) - 1)
            ElseIf Right(strUserLoc, 1) = "," Then
                strUserLoc = Left(strUserLoc, Len(strUserLoc) - 1)
            End If
        End If



        ddlist = RptSelect.FindControl("ddlDivision")
        strDivision = Trim(ddlist.SelectedItem.Text)

        strPhyMonth1 = Trim(ddlPhyMonthFrom.SelectedItem.Value)
        strPhyYear1 = Trim(txtPhyYearFrom.Text)

        strPhyMonth2 = Trim(ddlPhyMonthTo.SelectedItem.Value)
        strPhyYear2 = Trim(txtPhyYearTo.Text)

        strRemarks = Trim(txtremarks.text)



        if strDivision = "All" then 
            SearchStr = " and blk.Status like '1' and blk.loccode in ('" & trim(strUserLoc) & "')"

            sortItem = "ORDER BY blk.BlkGrpCode" 
            strParam = sortItem & "|" & SearchStr

            Try
                intErrNo = objGLSetup.mtdGetMasterList(strOppCd_GET, strParam, objGLSetup.EnumGLMasterType.BlkGrp, objDataSet)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_BLOCKGROUP_GET&errmesg=" & lblErrMessage.Text & "&redirect=GL/Setup/GL_Setup_BlockGrp.aspx")
            End Try     

            strDivision = "''"
            For intCnt = 0 to objDataSet.Tables(0).Rows.Count - 1
                strDivision = strDivision & Trim(objDataSet.Tables(0).Rows(intCnt).Item("BlkGrpCode")) & "'',''" 
            Next  
            strDivision = strDivision & "KNTR'',''KNTOR'',''WSHOP'',''" 
            strDivision = strDivision 
            strDivision = left(strDivision, len(strDivision) - 3)
        end if 

        Response.Write("<Script Language=""JavaScript"">window.open(""GL_StdRpt_HKEfektifSKUHPreview.aspx?Type=Print&CompName=" & strCompany & _
                       "&Location=" & strUserLoc & _
                       "&DDLAccMth=" & strddlAccMth & _
                       "&DDLAccYr=" & strddlAccYr & _
                       "&RptId=" & strRptId & _
                       "&RptName=" & strRptName & _
                       "&Decimal=" & strDec & _
                       "&Division=" & strDivision & _
                       "&PhyMonth1=" & strPhyMonth1 & _
                       "&PhyYear1=" & strPhyYear1 & _
                       "&PhyMonth2=" & strPhyMonth2 & _
                       "&PhyYear2=" & strPhyYear2 & _
                       "&LocCode=" & strUserLoc & _
                       "&Remarks=" & strRemarks & _
                       """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

    End Sub

    Sub BindPhyMonthList1(ByVal pv_intMaxMonth As Integer)
        Dim intCnt As Integer
        Dim intSelIndex As Integer = 0

        ddlPhyMonthFrom.Items.Clear
        For intCnt = 1 To pv_intMaxMonth
            ddlPhyMonthFrom.Items.Add(intCnt)
            If intCnt = Convert.ToInt16(strPhyMonth) Then
                intSelIndex = intCnt - 1
            End If
        Next
        ddlPhyMonthFrom.SelectedIndex = intSelIndex
    End Sub

    Sub BindPhyMonthList2(ByVal pv_intMaxMonth As Integer)
        Dim intCnt As Integer
        Dim intSelIndex As Integer = 0

        ddlPhyMonthTo.Items.Clear
        For intCnt = 1 To pv_intMaxMonth
            ddlPhyMonthTo.Items.Add(intCnt)
            If intCnt = Convert.ToInt16(strPhyMonth) Then
                intSelIndex = intCnt - 1
            End If
        Next
        ddlPhyMonthTo.SelectedIndex = intSelIndex
    End Sub



End Class
