Imports System
Imports System.Data
Imports System.Collections 
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic

Imports agri.PR
Imports agri.HR
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.PWSystem.clsLangCap


Public Class PR_trx_AttdList : Inherits Page

    Protected WithEvents dgLine As DataGrid
    Protected WithEvents lbDelete As LinkButton    
    Protected WithEvents lblTracker As Label
    Protected WithEvents ddlDept As DropDownList
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents lblCurrentIndex As Label   
    Protected WithEvents lblPageCount As Label      
    Protected WithEvents txtEmpID As TextBox
    Protected WithEvents txtName As TextBox
    Protected WithEvents ddlMonth As DropDownList
    Protected WithEvents ddlYear As DropDownList
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblDepartment As Label
    Protected WithEvents lblTotalDept As Label
    Protected WithEvents ddlGang As DropDownList

    Protected objPRTrx As New agri.PR.clsTrx()
    Protected objHRTrx As New agri.HR.clsTrx()
    Protected objHRSetup As New agri.HR.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intPRAR As Long

    Dim objAttdDs As New Object()
    Dim objLangCapDs As New Object()
    Dim objGangDs As New Object()
    Dim objLoc As New agri.Admin.clsLoc()
    Dim strLocType as String

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intPRAR = Session("SS_PRAR")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxAttendance), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            If Not Page.IsPostBack Then
                ddlMonth.SelectedIndex = Month(Now()) - 1
                BindGang()
                BindDept()
                BindYear()
                BindGrid() 
                BindPageList()
            End If
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblDepartment.text = GetCaption(objLangCap.EnumLangCap.Department)
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=PR/trx/PR_trx_AttdList.aspx")
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


    Sub srchBtn_Click(sender As Object, e As System.EventArgs) 
        lblCurrentIndex.Text = 0
        dgLine.EditItemIndex = -1
        BindGrid() 
        BindPageList()
    End Sub 

    Sub BindGrid() 
        Dim PageNo As Integer 
        Dim intCnt As Integer
        Dim lbButton As LinkButton


        Dim PageCount As Integer
        Dim dsTemp As DataSet
        dsTemp = LoadAttData
        PageCount = objGlobal.mtdGetPageCount(Session("SS_PAGINGCOUNT"), dgLine.PageSize)
        If PageCount < 1 Then
            PageCount = 1
        End If
        If lblCurrentIndex.Text >= PageCount Then
            If PageCount = 0 Then
                lblCurrentIndex.Text = 0
            Else
                lblCurrentIndex.Text = PageCount - 1
                dsTemp = LoadAttData
            End If
        End If
        
        dgLine.DataSource = dsTemp
        dgLine.DataBind()
        lblPageCount.Text = PageCount
        BindPageList()
        PageNo = lblCurrentIndex.Text + 1
        lblTracker.Text="Page " & pageno & " of " & PageCount
    End Sub 

    Sub BindDept()
        ddlDept.DataSource = LoadDeptData
        ddlDept.DataValueField = "DeptCode"
        ddlDept.DataTextField = "Description"
        ddlDept.DataBind()
    End Sub

    Sub BindPageList() 
        Dim count As Integer = 1   
        Dim arrDList As New ArrayList()

        While not count = lblPageCount.Text + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While 
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = lblCurrentIndex.Text
    End Sub 

    Protected Function LoadAttData() As DataSet
        Dim strOpCd_Emp As String = "PR_CLSTRX_ATTENDANCELIST_EMP_GET"
        Dim strOpCd_Trx As String = "PR_CLSTRX_ATTENDANCELIST_TRX_GET"
        Dim strOpCds As String = strOpCd_Emp & "|" & strOpCd_Trx
        Dim strSrchGangCode As String
        Dim strSrchEmpId As String
        Dim strSrchName As String
        Dim strSrchDept As String
        Dim strSrchMonth As String
        Dim strSrchYear As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer

        Session("SS_PAGING") = lblCurrentIndex.Text 

        strSrchGangCode = IIF(ddlGang.SelectedItem.Value = "", "", ddlGang.SelectedItem.Value)
        strSrchEmpId = IIF(txtEmpID.Text = "", "", Replace(txtEmpID.Text, "'", ""))
        strSrchName = IIF(txtName.Text = "", "", Replace(txtName.Text, "'", ""))

        If lblTotalDept.Text = 0 Then
            strSrchDept = ""
        Else
            strSrchDept = ddlDept.SelectedItem.Value
        End If

        strSrchMonth = IIF(ddlMonth.SelectedItem.Value = "", "", ddlMonth.SelectedItem.Value)
        strSrchYear = ddlYear.SelectedItem.Value
        strParam = strSrchEmpId & "|" & _
                   strSrchName & "|" & _
                   strSrchDept & "|" & _
                   strSrchMonth & "|" & _
                   strSrchYear & "|" & _
                   objGlobal.mtdGetTotalDays(CInt(strSrchMonth), CInt(strSrchYear)) & "|" & _
                   objHRTrx.EnumEmpStatus.Active & "|" & _
                   strSrchGangCode 
        Try
            intErrNo = objPRTrx.mtdGetAttendanceList(strOpCds, _
                                                     strCompany, _
                                                     strLocation, _
                                                     strUserId, _
                                                     strAccMonth, _
                                                     strAccYear, _
                                                     strParam, _
                                                     objAttdDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ATTENDANCELIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        Return objAttdDs
    End Function


    Protected Function LoadDeptData() As DataSet
        Dim strOpCd_GET As String = "HR_CLSSETUP_DEPT_SEARCH1"
        Dim objDeptDs As New Object()
        Dim strSearch As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        
        strParam = "|||A.DeptCode||"

        Try
            intErrNo = objHRSetup.mtdGetDept(strOpCd_GET, strParam, objDeptDs, False)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ATTENDANCELIST_DEPT_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objDeptDs.Tables(0).Rows.Count > 0 Then
            lblTotalDept.Text = objDeptDs.Tables(0).Rows.Count
            For intCnt = 0 To objDeptDs.Tables(0).Rows.Count - 1
                objDeptDs.Tables(0).Rows(intCnt).Item("DeptCode") = Trim(objDeptDs.Tables(0).Rows(intCnt).Item("DeptCode"))
                objDeptDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objDeptDs.Tables(0).Rows(intCnt).Item("Description"))
            Next
        End If

        Return objDeptDs
    End Function

    Sub BindYear() 
        Dim intPrev As Integer = CInt(strAccYear) - 1
        Dim intNext As Integer = CInt(strAccYear) + 1
        Dim intCalYear As Integer = Year(Now())     
        Dim intCnt As Integer = 0
        Dim intSelectedIndex As Integer = 1

        ddlYear.Items.Add(New ListItem(intPrev, intPrev))
        ddlYear.Items.Add(New ListItem(strAccYear, strAccYear))
        ddlYear.Items.Add(New ListItem(intNext, intNext))        
        If intNext < intCalYear Then
            ddlYear.Items.Add(New ListItem(intCalYear, intCalYear))
        End If 
        For intCnt=0 To ddlYear.Items.Count - 1
            If ddlYear.Items(intCnt).Value = intCalYear Then
                intSelectedIndex = intCnt
                Exit For
            End If
        Next    
        ddlYear.SelectedIndex = intSelectedIndex
    End Sub 

    Sub btnPrevNext_Click (sender As Object, e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).commandargument
        Select Case direction
            Case "first"
                lblCurrentIndex.Text = 0
            Case "prev"
                lblCurrentIndex.Text = _
                    Math.Max(0, lblCurrentIndex.Text - 1)
            Case "next"
                lblCurrentIndex.Text = _
                    Math.Min(lblPageCount.Text - 1, lblCurrentIndex.Text + 1)
            Case "last"
                lblCurrentIndex.Text = lblPageCount.Text - 1
        End Select
        lstDropList.SelectedIndex = lblCurrentIndex.Text
        BindGrid()
    End Sub

    Sub PagingIndexChanged (sender As Object, e As EventArgs)
        If Page.IsPostBack Then
            lblCurrentIndex.Text = lstDropList.SelectedIndex 
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(sender As Object, e As DataGridPageChangedEventArgs)
        lblCurrentIndex.Text = e.NewPageIndex
        BindGrid() 
    End Sub


    Sub OnCommand_Redirect(Sender As Object, E As DataGridCommandEventArgs)
        Dim RecCell As TableCell = e.Item.Cells(1)
        Dim strEmp as string
        Dim strCmdArgs As String = e.CommandArgument
        Dim strDay As String = Mid(strCmdArgs, 2, Len(strCmdArgs) - 1)
        Dim strMonth As String = ddlMonth.SelectedItem.Value
        Dim strYear As String = ddlYear.SelectedItem.Value
        Dim strDate As String
        Dim lblDelText As Label     

        Select Case strMonth
            Case "1" : strMonth = "-Jan-"
            Case "2" : strMonth = "-Feb-"
            Case "3" : strMonth = "-Mar-"
            Case "4" : strMonth = "-Apr-"
            Case "5" : strMonth = "-May-"
            Case "6" : strMonth = "-Jun-"
            Case "7" : strMonth = "-Jul-"
            Case "8" : strMonth = "-Aug-"
            Case "9" : strMonth = "-Sep-"
            Case "10" : strMonth = "-Oct-"
            Case "11" : strMonth = "-Nov-"
            Case "12" : strMonth = "-Dec-"
        End Select
        
        dgLine.EditItemIndex = CInt(E.Item.ItemIndex)
        lblDelText = dgLine.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblGangCode")            

        strDate = strDay & strMonth & strYear
        Response.Redirect("PR_trx_DailyAttd.aspx?empcode=" & RecCell.Text & "&date=" & strDate & "&gangcode=" & lblDelText.Text)
        
    End Sub

    Sub AttdBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("PR_trx_DailyAttd.aspx")
    End Sub


    Sub BindGang()
        Dim strOpCd As String = "HR_CLSSETUP_GANG_SEARCH"
        Dim dr As DataRow
        Dim strParam As String        
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        strParam = strLocation & "||||" & objHRSetup.EnumGangStatus.Active & "||GangCode|"
        Try
            intErrNo = objHRSetup.mtdGetGang(strOpCd, strParam, objGangDs, False)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ATTENDANCELIST_GET&errmesg=" & Exp.ToString & "&redirect=")
        End Try

        dr = objGangDs.Tables(0).NewRow()
        dr("GangCode") = ""
        dr("_Description") = "Select Gang Code"
        objGangDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlGang.DataSource = objGangDs.Tables(0)
        ddlGang.DataValueField = "GangCode"
        ddlGang.DataTextField = "_Description"
        ddlGang.DataBind()
        ddlGang.SelectedIndex = intSelectedIndex
        ddlGang.AutoPostBack = True
    End Sub



End Class
