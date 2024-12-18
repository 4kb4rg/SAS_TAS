
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
Imports Microsoft.VisualBasic
Imports System.Xml
Imports System.Web.Services

Public Class PR_StdRpt_PayslipTW : Inherits Page

    Protected RptSelect As UserControl

    Protected objHRSetup As New agri.HR.clsSetup()
    Protected objHR As New agri.HR.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objSysCfg As New agri.PWSystem.clsConfig()
    Protected objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Protected WithEvents lblDate As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents lblBlock As Label
    Protected WithEvents lblSubBlk As Label

    Protected WithEvents lstDeptCode As DropDownList
    Protected WithEvents lstEmpCodeFrom As DropDownList
    Protected WithEvents lstEmpCodeTo As DropDownList
    Protected WithEvents txtGangCode As TextBox

    Protected WithEvents PrintPrev As ImageButton

    Dim objLangCapDs As New Object()
    
    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfigsetting As Integer

    Dim dr As DataRow
    Dim intCnt As Integer
    Dim intErrNo As Integer
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        Dim tempDateFrom As Textbox
        Dim tempDateTo As Textbox

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")
        
        lblDate.Visible = False
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
                onload_GetLangCap()
                BindDeptCode()
                BindEmpCodeList()
            End If   
        End If
    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim ucTrMthYr As HtmlTableRow

        ucTrMthYr = RptSelect.FindControl("TrMthYr")
        ucTrMthYr.Visible = True

    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        lblLocation.Text = GetCaption(objLangCap.EnumLangCap.Location)
        lblBlock.Text = GetCaption(objLangCap.EnumLangCap.Block)
        lblSubBlk.Text = GetCaption(objLangCap.EnumLangCap.SubBlock)
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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PWSYSTEM_CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/PR_StdRpt_Selection.aspx")
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

    Sub BindDeptCode()
        Dim dsForDeptCodeDropDown As New DataSet()
        Dim strOpCd As String = "HR_CLSSETUP_DEPT_SEARCH"
        Dim strParam As String 

        strParam = "|" & objHRSetup.EnumDeptStatus.Active & "||A.DeptCode||" 

        Try
            intErrNo = objHRSetup.mtdGetDept(strOpCd, strParam, dsForDeptCodeDropDown, False)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_PR_DEPTCODE&errmesg=" & lblErrMessage.Text & "&redirect=../../en/reports/PR_StdRpt_Selection.aspx")
        End Try

        For intCnt = 0 To dsForDeptCodeDropDown.Tables(0).Rows.Count - 1
            dsForDeptCodeDropDown.Tables(0).Rows(intCnt).Item("DeptCode") = Trim(dsForDeptCodeDropDown.Tables(0).Rows(intCnt).Item("DeptCode"))
            dsForDeptCodeDropDown.Tables(0).Rows(intCnt).Item("Description") = Trim(dsForDeptCodeDropDown.Tables(0).Rows(intCnt).Item("Description"))
        Next intCnt

        dr = dsForDeptCodeDropDown.Tables(0).NewRow()
        dr("DeptCode") = ""
        dr("Description") = "All"
        dsForDeptCodeDropDown.Tables(0).Rows.InsertAt(dr, 0)

        lstDeptCode.DataSource = dsForDeptCodeDropDown.Tables(0)
        lstDeptCode.DataValueField = "DeptCode"
        lstDeptCode.DataTextField = "Description"
        lstDeptCode.DataBind()
    End Sub

    Sub BindEmpCodeList()
        Dim objRptDs As New Object()
        Dim strOpCd_Get As String = "HR_CLSTRX_EMPLOYEE_GET"
        Dim strParam As String = ""

        strParam = "|" & objHR.EnumEmpStatus.Active

        Try
            intErrNo = objHR.mtdGetOthEmployee(strOpCd_Get, strParam, strLocation, objRptDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_STDRPT_EMPLOYEE_GET&errmesg=" & lblErrMessage.Text & "&redirect=../../en/reports/PR_StdRpt_Selection.aspx")
        End Try

        For intCnt = 0 To objRptDs.Tables(0).Rows.Count - 1
            objRptDs.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(objRptDs.Tables(0).Rows(intCnt).Item("EmpCode"))
            objRptDs.Tables(0).Rows(intCnt).Item("EmpName") = Trim(objRptDs.Tables(0).Rows(intCnt).Item("EmpCode")) & " (" & _
                                                                Trim(objRptDs.Tables(0).Rows(intCnt).Item("EmpName")) & ")"
        Next

        dr = objRptDs.Tables(0).NewRow()
        dr("EmpCode") = ""
        dr("EmpName") = "All"
        objRptDs.Tables(0).Rows.InsertAt(dr, 0)

        lstEmpCodeFrom.DataSource = objRptDs.Tables(0)
        lstEmpCodeFrom.DataTextField = "EmpName"
        lstEmpCodeFrom.DataValueField = "EmpCode"
        lstEmpCodeFrom.DataBind()

        lstEmpCodeTo.DataSource = objRptDs.Tables(0)
        lstEmpCodeTo.DataTextField = "EmpName"
        lstEmpCodeTo.DataValueField = "EmpCode"
        lstEmpCodeTo.DataBind()
    End Sub

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strRptID As String
        Dim strRptTitle As String
        Dim strSelLocCode As String
        Dim strSelAccMonth As String
        Dim strSelAccYear As String
        Dim strDecimal As String
        Dim strDeptCode As String
        Dim strEmpCodeFrom As String
        Dim strEmpCodeTo As String
        Dim strGangCode As String

        Dim lbl As Label
        Dim ddl As Dropdownlist
        Dim hih As HtmlInputHidden

        ddl = RptSelect.FindControl("lstRptname")
        strRptID = ddl.SelectedItem.Value
        
        ddl = RptSelect.FindControl("lstRptname")
        strRptTitle = ddl.SelectedItem.Text

        hih = RptSelect.FindControl("hidUserLoc")
        strSelLocCode = Trim(hih.Value)

        If strSelLocCode = "" Then
            lbl = RptSelect.FindControl("lblUserLoc")
            lbl.Visible = True
            Exit Sub
        Else
            If Instr(strSelLocCode, "','") > 0 Then
                strSelLocCode = right(strSelLocCode, Len(strSelLocCode) - 3)
            End If
        End If 
        
        ddl = RptSelect.FindControl("lstAccMonth")
        strSelAccMonth = Trim(ddl.SelectedItem.Value)

        ddl = RptSelect.FindControl("lstAccYear")
        strSelAccYear = Trim(ddl.SelectedItem.Value)

        ddl = RptSelect.FindControl("lstDecimal")
        strDecimal = Trim(ddl.SelectedItem.Value)

        strDeptCode = lstDeptCode.SelectedItem.Value
        strEmpCodeFrom = lstEmpCodeFrom.SelectedItem.Value
        strEmpCodeTo = lstEmpCodeTo.SelectedItem.Value
        strGangCode = txtGangCode.Text


        Response.Write("<Script Language=""JavaScript"">window.open(""PR_StdRpt_PayslipTWPreview.aspx?Type=Print&CompName=" & strCompany & _
                       "&RptID=" & strRptID & _
                       "&RptTitle=" & strRptTitle & _
                       "&SelLocCode=" & strSelLocCode & _
                       "&Decimal=" & strDecimal & _
                       "&LocationTag=" & lblLocation.Text & _
                       "&BlockTag=" & lblBlock.Text & _
                       "&SubBlkTag=" & lblSubBlk.Text & _
                       "&SelAccMonth=" & strSelAccMonth & _
                       "&SelAccYear=" & strSelAccYear & _
                       "&DeptCode=" & strDeptCode & _
                       "&EmpCodeFrom=" & strEmpCodeFrom & _
                       "&EmpCodeTo=" & strEmpCodeTo & _
                       "&GangCode=" & strGangCode & _
                       """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

    End Sub

End Class
