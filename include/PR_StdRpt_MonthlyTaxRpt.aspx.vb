Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information
Imports System.Xml
Imports System.Web.Services

Public Class PR_StdRpt_MonthlyTaxRpt : Inherits Page
    Protected WithEvents PrintPrev As ImageButton
    Protected RptSelect As UserControl
    Protected objHR As New agri.HR.clsSetup()
    
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGLSetup As New agri.GL.clsSetup()

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents ddlPhyMonthFrom As DropDownList
    Protected WithEvents txtPhyYearFrom As TextBox
    Protected WithEvents ddlEmpCategory As DropDownList
    Protected WithEvents ddlPhyMonthTo As DropDownList
    Protected WithEvents txtPhyYearTo As TextBox

    Dim objLangCapDs As New Object()
    Dim objDataSet As New Object()
    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strPhyMonth As String
    Dim strPhyYear As String

    Dim dr As DataRow
    Dim intCnt As Integer
    Dim intErrNo As Integer
    Dim objLoc As New agri.Admin.clsLoc()
    Dim strLocType as String
    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        strPhyMonth = Session("SS_PHYMONTH")
        strPhyYear = Session("SS_PHYYEAR")
        strLocType = Session("SS_LOCTYPE")
        
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
                onload_GetLangCap()
                txtPhyYearFrom.Text = strPhyYear
                txtPhyYearTo.Text = strPhyYear
                BindPhyMonthList1(12)
                BindPhyMonthList2(12)
                BindEmpCategory(0)
            End If
        End If

    End Sub
    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim ucTrMthYr As HtmlTableRow
        Dim ucTrDecimal As HtmlTableRow
        Dim ucTrDivision As HtmlTableRow

        ucTrMthYr = RptSelect.FindControl("TrMthYr")
        ucTrMthYr.Visible = True

        ucTrDecimal = RptSelect.FindControl("TrDecimal")
        ucTrDecimal.Visible = True

        ucTrDivision = RptSelect.FindControl("TrDivision")
        ucTrDivision.Visible = True
    End Sub

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strDendaCodeFrom As String
        Dim strDendaCodeTo As String
        Dim strDendaType As String
        Dim strStatus As String
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
        Dim ddlist As DropDownList
        Dim rdblist As RadioButtonList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden
        Dim strParam As String
        Dim strPhyMonth1 As String
        Dim strPhyYear1 As String
        Dim strPhyMonth2 As String
        Dim strPhyYear2 As String
        Dim strDivCode As String
        Dim strEmpCat As String
        Dim strOppCd_GET As String = "GL_CLSSETUP_BLOCKGROUP_LIST_GET"
        Dim SearchStr As String
        Dim sortItem As String

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
            ElseIf Left(strUserLoc, 1) = "," Then
                strUserLoc = Right(strUserLoc, Len(strUserLoc) - 1)
            ElseIf Right(strUserLoc, 1) = "," Then
                strUserLoc = Left(strUserLoc, Len(strUserLoc) - 1)
            End If 

       
        End If


        ddlist = RptSelect.FindControl("ddlDivision")
        strDivCode = Trim(ddlist.SelectedItem.Value)

        strPhyMonth1 = Trim(ddlPhyMonthFrom.SelectedItem.Value)
        strPhyYear1 = Trim(txtPhyYearFrom.Text)

        strPhyMonth2 = Trim(ddlPhyMonthTo.SelectedItem.Value)
        strPhyYear2 = Trim(txtPhyYearTo.Text)

        strEmpCat = Trim(ddlEmpCategory.SelectedItem.Value)
        if  strEmpCat = "All" then 
            strEmpCat = "''1'', ''2'', ''3'', ''4'', ''5'', ''6'', ''7''"
        else        
            strEmpCat = "''" & strEmpCat & "''"
        end if 



        if strDivCode = "All" then 
            SearchStr = " and blk.Status like '1' "

            sortItem = "ORDER BY blk.BlkGrpCode" 
            strParam = sortItem & "|" & SearchStr

            Try
                intErrNo = objGLSetup.mtdGetMasterList(strOppCd_GET, strParam, objGLSetup.EnumGLMasterType.BlkGrp, objDataSet)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_BLOCKGROUP_GET&errmesg=" & lblErrMessage.Text & "&redirect=GL/Setup/GL_Setup_BlockGrp.aspx")
            End Try     

            strDivCode = "''"
            For intCnt = 0 to objDataSet.Tables(0).Rows.Count - 1
                strDivCode = strDivCode & Trim(objDataSet.Tables(0).Rows(intCnt).Item("BlkGrpCode")) & "'',''" 
            Next  
            strDivCode = left(strDivCode, len(strDivCode) - 3)
        else
            strDivCode = "''" & strDivCode & "''"
        end if 


        Response.Write("<Script Language=""JavaScript"">window.open(""PR_StdRpt_MonthlyTaxRptPreview.aspx?CompCode=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & strDec & _
                       "&lblLocation=" & lblLocation.Text & "&SelAccMonth=" & strddlAccMth & "&SelAccYear=" & strddlAccYr & "&PhyMonth1=" & strPhyMonth1 & "&PhyYear1=" & strPhyYear1 & "&PhyMonth2=" & strPhyMonth2 & "&PhyYear2=" & strPhyYear2 & _
                       "&DivCode=" & strDivCode & "&EmpCat=" & strEmpCat & "&LocCode=" & strUserLoc & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Sub onload_GetLangCap()

        GetEntireLangCap()
        lblLocation.Text = GetCaption(objLangCap.EnumLangCap.Location)

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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_STDRPT_DENDALIST_GET_CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/PR_StdRpt_Selection.aspx")
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

    Sub BindEmpCategory(ByVal pv_EmpCat As Integer) 
        ddlEmpCategory.Items.Add(New ListItem("Select Category Type", ""))
        ddlEmpCategory.Items.Add(New ListItem(objHR.mtdGetCategoryType(objHR.EnumCategoryType.Honorer), objHR.EnumCategoryType.Honorer))
        ddlEmpCategory.Items.Add(New ListItem(objHR.mtdGetCategoryType(objHR.EnumCategoryType.NonStaff), objHR.EnumCategoryType.NonStaff))
        ddlEmpCategory.Items.Add(New ListItem(objHR.mtdGetCategoryType(objHR.EnumCategoryType.Staff), objHR.EnumCategoryType.Staff))
        ddlEmpCategory.Items.Add(New ListItem(objHR.mtdGetCategoryType(objHR.EnumCategoryType.Executive), objHR.EnumCategoryType.Executive))
        ddlEmpCategory.Items.Add(New ListItem(objHR.mtdGetCategoryType(objHR.EnumCategoryType.SKUB), objHR.EnumCategoryType.SKUB))
        ddlEmpCategory.Items.Add(New ListItem(objHR.mtdGetCategoryType(objHR.EnumCategoryType.SKUH), objHR.EnumCategoryType.SKUH))
        ddlEmpCategory.Items.Add(New ListItem(objHR.mtdGetCategoryType(objHR.EnumCategoryType.OTH), objHR.EnumCategoryType.OTH))
        ddlEmpCategory.Items.Add(New ListItem("All", "All"))
        ddlEmpCategory.SelectedIndex = 8

    End Sub
        

End Class
