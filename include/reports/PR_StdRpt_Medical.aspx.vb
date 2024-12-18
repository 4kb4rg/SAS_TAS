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

Public Class PR_StdRpt_Medical : Inherits Page
    Protected WithEvents PrintPrev As ImageButton
    Protected RptSelect As UserControl
    
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objHRtrx As New agri.HR.clsTrx()

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents ddlEmployee As DropDownList

    Dim objLangCapDs As New Object()
    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String

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

        strLocType = Session("SS_LOCTYPE")
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
                onload_GetLangCap()
                BindEmployee("")
            End If
        End If

    End Sub
    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim ucTrMthYr As HtmlTableRow
        Dim ucTrDecimal As HtmlTableRow

        ucTrMthYr = RptSelect.FindControl("TrMthYr")
        ucTrMthYr.Visible = True

        ucTrDecimal = RptSelect.FindControl("TrDecimal")
        ucTrDecimal.Visible = True

    End Sub
    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
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
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden
        Dim strParam As String
        Dim strTemp As String
        Dim strAccMth As String
        Dim strEmpCode As String

        tempAccMth = RptSelect.FindControl("lstAccMonth")
        strddlAccMth = Trim(tempAccMth.SelectedItem.Value)
        strAccMth = Trim(tempAccMth.SelectedItem.Value)
        tempAccYr = RptSelect.FindControl("lstAccYear")
        strddlAccYr = Trim(tempAccYr.SelectedItem.Value)
        tempRpt = RptSelect.FindControl("lstRptName")
        strRptID = Trim(tempRpt.SelectedItem.Value)
        strRptName = Trim(tempRpt.SelectedItem.Text)
        tempUserLoc = RptSelect.FindControl("hidUserLoc")
        strUserLoc = Trim(tempUserLoc.Value)
        tempDec = RptSelect.FindControl("lstDecimal")
        strDec = Trim(tempDec.SelectedItem.Value)

        strEmpCode = Trim(ddlEmployee.SelectedItem.Value)

        If Trim(strddlAccMth) <> "1" Then
            For intCnt = 1 To CInt(strddlAccMth)
                strTemp = CStr(intCnt) & "','" & strTemp
            Next
            strddlAccMth = strTemp
        End If

        If strUserLoc = "" Then
            templblUL = RptSelect.FindControl("lblUserLoc")
            templblUL.Visible = True
            Exit Sub
        Else
            If Left(strUserLoc, 3) = "','" Then
                strUserLoc = Right(strUserLoc, Len(strUserLoc) - 3)
            ElseIf Right(strUserLoc, 3) = "','" Then
                strUserLoc = Left(strUserLoc, Len(strUserLoc) - 3)
            End If
        End If

        If Right(strddlAccMth, 2) = ",'" Then
                    strddlAccMth = Left(strddlAccMth, Len(strddlAccMth) - 3)
        Else
                    strddlAccMth = Trim(strddlAccMth)
        End If

        Response.Write("<Script Language=""JavaScript"">window.open(""PR_StdRpt_MedicalPreview.aspx?CompCode=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & strDec & _
                       "&strEmpCode=" & strEmpCode & "&AccMonthTo=" & strddlAccMth & "&lblLocation=" & lblLocation.Text & "&SelAccMonth=" & strAccMth & "&SelAccYear=" & strddlAccYr & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

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

    Sub BindEmployee(ByVal pv_strEmpId As String)
        Dim objEmpDs As New Dataset
        Dim strOpCd As String = "HR_CLSSETUP_EMPID_LIST_GET"
        Dim dr As DataRow
        Dim strParam As String = "|" & Convert.ToString(objHRTrx.EnumEmpStatus.Active) & "|" & strLocation
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            intErrNo = objHRTrx.mtdGetEmpCode(strOpCd, strParam, objEmpDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ADTRX_EMP_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        pv_strEmpId = Trim(UCase(pv_strEmpId))

        For intCnt = 0 To objEmpDs.Tables(0).Rows.Count - 1
            objEmpDs.Tables(0).Rows(intCnt).Item("EmpName") = objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode") & " (" & objEmpDs.Tables(0).Rows(intCnt).Item("EmpName") & ")"
            If UCase(objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode")) = pv_strEmpId Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objEmpDs.Tables(0).NewRow()
        If intSelectedIndex = 0 Then
            If pv_strEmpId <> "" Then
                dr("EmpCode") = Trim(pv_strEmpId)
                dr("EmpName") = Trim(pv_strEmpId)
            Else
                dr("EmpCode") = ""
                dr("EmpName") = "Select one Employee ID"
            End If
        Else
            dr("EmpCode") = ""
            dr("EmpName") = "Select one Employee ID"
        End If
        objEmpDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlEmployee.DataSource = objEmpDs.Tables(0)
        ddlEmployee.DataValueField = "EmpCode"
        ddlEmployee.DataTextField = "EmpName"
        ddlEmployee.DataBind()
        ddlEmployee.SelectedIndex = intSelectedIndex
        objEmpDs = Nothing
    End Sub
End Class
