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

Public Class PR_StdRpt_AlokasiPekerjaan : Inherits Page
    Protected WithEvents PrintPrev As ImageButton
    Protected WithEvents BtnDateFrom As Image
    Protected WithEvents BtnDateTo As Image
    Protected RptSelect As UserControl

    Dim objOk As New agri.GL.ClsTrx()
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblLocation As Label

    Protected WithEvents txtDateFrom As TextBox
    Protected WithEvents txtDateTo As TextBox

    Protected WithEvents ddlmonth As DropDownList
    Protected WithEvents txtyear As TextBox
    Protected WithEvents ddlkategori As DropDownList
    Protected WithEvents ddldivisi As DropDownList
    Protected WithEvents ddlmandor As DropDownList
    Protected WithEvents ddljabatan As DropDownList

    Protected WithEvents lblDateFromFmt As Label
    Protected WithEvents lblDateToFmt As Label
    Protected WithEvents lblErrDate As Label
    Protected WithEvents lblvaldate As Label
    Protected WithEvents lblErrAttdDate As Label
    Protected WithEvents lblErrAttdDateDesc As Label
    Protected WithEvents lblErrMaxDate As Label

    Dim objLangCapDs As New Object()
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
    Dim strLocType As String

    Dim strDateFmt As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_SELACCMONTH")
        strAccYear = Session("SS_SELACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        strPhyMonth = Session("SS_PHYMONTH")
        strPhyYear = Session("SS_PHYYEAR")
        strLocType = Session("SS_LOCTYPE")
        strDateFmt = Session("SS_DATEFMT")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            lblErrAttdDate.Visible = False
            lblErrAttdDateDesc.Visible = False
            lblErrMaxDate.Visible = False
            If Not Page.IsPostBack Then
                ddlmonth.SelectedIndex = Month(Now()) - 1
                txtyear.Text = Year(Now())
                txtDateFrom.Text = objGlobal.GetShortDate(strDateFmt, Now())
                txtDateTo.Text = objGlobal.GetShortDate(strDateFmt, Now())

                onload_GetLangCap()
                BindKategori()
                BindDivisiCode()
                BindJabatan()
            End If
        End If

    End Sub
    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim ucTrDecimal As HtmlTableRow

        ucTrDecimal = RptSelect.FindControl("TrDecimal")
        ucTrDecimal.Visible = True
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
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden
        Dim strParam As String
        Dim strMonth As String
        Dim strDate As String
        Dim strYear As String

        Dim strFromDate As String = ""
        Dim strToDate As String = ""
        Dim strKategori As String = ""
        Dim strDivisi As String = ""
        Dim strMandor As String = ""
        Dim strPekerjaan As String = ""

        Dim objFormatDate As String
        Dim strDuration As String = "0"
        Dim strFStartDate As String
        Dim strFEndDate As String

        If Len(Trim(txtDateFrom.Text)) < 10 Or Len(Trim(txtDateTo.Text)) < 10 Then
            lblvaldate.Visible = True
            Exit Sub
        Else
            lblvaldate.Visible = False
        End If

        If Trim(txtDateFrom.Text) <> "" And Trim(txtDateTo.Text) <> "" Then
            If objGlobal.mtdValidInputDate(strDateFmt, _
                                            txtDateFrom.Text, _
                                            objFormatDate, _
                                            strFromDate) = False Or _
            objGlobal.mtdValidInputDate(strDateFmt, _
                                            txtDateTo.Text, _
                                            objFormatDate, _
                                            strToDate) = False Then
                lblvaldate.Visible = True
                Exit Sub
            End If
        End If

        If Trim(txtDateFrom.Text) <> "" Then
            strDate = Left(txtDateFrom.Text, 2)
            strMonth = Right(Left(txtDateFrom.Text, 5), 2)
            strYear = Right(txtDateFrom.Text, 4)

            lblDateFromFmt.Text = strMonth & "/" & strDate & "/" & strYear
        Else
            lblDateFromFmt.Text = strPhyMonth & "/" & "01" & "/" & strPhyYear
        End If

        If Trim(txtDateTo.Text) <> "" Then
            strDate = Left(txtDateTo.Text, 2)
            strMonth = Right(Left(txtDateTo.Text, 5), 2)
            strYear = Right(txtDateTo.Text, 4)

            lblDateToFmt.Text = strMonth & "/" & strDate & "/" & strYear
        Else
            If (strPhyMonth = "1") Or (strPhyMonth = "3") Or (strPhyMonth = "5") Or (strPhyMonth = "7") Or (strPhyMonth = "8") Or (strPhyMonth = "10") Or (strPhyMonth = "12") Then
                lblDateToFmt.Text = strPhyMonth & "/" & "31" & "/" & strPhyYear
            End If
            If (strPhyMonth = "4") Or (strPhyMonth = "6") Or (strPhyMonth = "11") Then
                lblDateToFmt.Text = strPhyMonth & "/" & "30" & "/" & strPhyYear
            End If
            If (strPhyMonth = "2") And (CDbl(strPhyYear) Mod 4) > 0 Then
                lblDateToFmt.Text = strPhyMonth & "/" & "28" & "/" & strPhyYear
            End If
            If (strPhyMonth = "2") And (CDbl(strPhyYear) Mod 4) = 0 Then
                lblDateToFmt.Text = strPhyMonth & "/" & "29" & "/" & strPhyYear
            End If
        End If

        If Trim(lblDateFromFmt.Text) = "" Then
            lblErrDate.Visible = True
            Exit Sub
        Else
            lblErrDate.Visible = False
        End If

        strFStartDate = lblDateFromFmt.Text
        strFEndDate = lblDateToFmt.Text
        strDuration = CalcDuration(strFStartDate, strFEndDate)

        If Val(strDuration) > 31 Then
            lblErrMaxDate.Visible = True
            Exit Sub
        End If

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

        If ddlkategori.SelectedItem.Value = "" Then
            strKategori = ""
        Else
            strKategori = Trim(ddlkategori.SelectedItem.Value)
        End If

        If ddldivisi.SelectedItem.Value = "" Then
            strDivisi = ""
        Else
            strDivisi = Trim(ddldivisi.SelectedItem.Value)
        End If

        If ddljabatan.SelectedItem.Value = "" Then
            strPekerjaan = ""
        Else
            strPekerjaan = Trim(ddljabatan.SelectedItem.Value)
        End If

        If ddlmandor.Text = "" Then
            strMandor = ""
        Else
            If ddlmandor.SelectedItem.Value = "" Then
                strMandor = ""
            Else
                strMandor = Trim(ddlmandor.SelectedItem.Value)
            End If

        End If


        Response.Write("<Script Language=""JavaScript"">window.open(""PR_StdRpt_DaftarAbsensiPreview_Estate.aspx?CompCode=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & strDec & _
                       "&lblLocation=" & lblLocation.Text & "&SelAccMonth=" & strddlAccMth & "&SelAccYear=" & strddlAccYr & "&SelPhyMonth=" & strPhyMonth & "&SelPhyYear=" & strPhyYear & "&SelDateFrom=" & lblDateFromFmt.Text & "&SelDateTo=" & lblDateToFmt.Text & _
                       "&Kategori=" & strKategori & "&Divisi=" & strDivisi & "&Mandor=" & strMandor & "&Pekerjaan=" & strPekerjaan & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
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
                If strLocType = objLoc.EnumLocType.Mill Then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                Else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                End If
                Exit For
            End If
        Next
    End Function

    Function CalcDuration(ByVal vstrStartDt As String, ByVal vstrEndDate As String) As String
        Dim dblDay As Double

        dblDay = DateDiff(DateInterval.Day, CDate(vstrStartDt), CDate(vstrEndDate))

        CalcDuration = dblDay
    End Function

    Sub BindKategori()
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_BK_CATEGORY_GET_LIST"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpDivDs As New Object()
        Dim dr As DataRow


        strParamName = "SEARCH|SORT"
        strParamValue = "|"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpDivDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BK_CATEGORY_GET_LIST&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objEmpDivDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpDivDs.Tables(0).Rows.Count - 1
                objEmpDivDs.Tables(0).Rows(intCnt).Item("CatID") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("CatID"))
                objEmpDivDs.Tables(0).Rows(intCnt).Item("CatName") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("CatID")) & " (" & Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("CatName")) & ")"
            Next
        End If

        dr = objEmpDivDs.Tables(0).NewRow()
        dr("CatID") = ""
        dr("CatName") = "All"
        objEmpDivDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlkategori.DataSource = objEmpDivDs.Tables(0)
        ddlkategori.DataTextField = "CatName"
        ddlkategori.DataValueField = "CatID"
        ddlkategori.DataBind()
    End Sub

    Sub BindDivisiCode()
        Dim strOpCd As String = "PR_PR_STP_DIVISICODE_GET"
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim objDs As New Object()
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intselectIndex As Integer = 0

        Dim dr As DataRow

        ParamName = "SEARCH|SORT"
        ParamValue = "AND A.LocCode='" & strLocation & "' AND A.Status='1'|ORDER By BlkGrpCode"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd, ParamName, ParamValue, objDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_DIVISICODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objDs.Tables(0).Rows.Count - 1
                objDs.Tables(0).Rows(intCnt).Item("BlkGrpCode") = Trim(objDs.Tables(0).Rows(intCnt).Item("BlkGrpCode"))
                objDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objDs.Tables(0).Rows(intCnt).Item("Description"))
            Next
        End If

        dr = objDs.Tables(0).NewRow()
        dr("BlkGrpCode") = ""
        dr("Description") = "All"
        objDs.Tables(0).Rows.InsertAt(dr, 0)

        ddldivisi.DataSource = objDs.Tables(0)
        ddldivisi.DataTextField = "Description"
        ddldivisi.DataValueField = "BlkGrpCode"
        ddldivisi.DataBind()
    End Sub

    Sub BindMandoran(ByVal strdivcode As String)
        Dim strOpCd_EmpDiv As String = "HR_HR_TRX_EMPLOYEE_LIST_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim objEmpCodeDs As New Object()
        Dim dr As DataRow
		Dim SM As String 
      
	    IF cint(strAccMonth) < 10 Then
			SM = "0" & strAccMonth
		Else 
		    SM = strAccMonth
		End If

        strParamName = "LOC|AM|AY|SEARCH|SORT"
        strParamValue =  strLocation & "|" & SM & "|" & strAccYear & "|AND A.Status='1' AND A.LocCode = '" & strLocation & "' AND C.IDDiv like '%" & strdivcode & "%' AND (isMandor='1')|ORDER BY A.EmpName"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_TRX_EMPLOYEE_LIST_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objEmpCodeDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpCodeDs.Tables(0).Rows.Count - 1
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpCode"))
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("_Description") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("_Description"))
            Next
        End If

        dr = objEmpCodeDs.Tables(0).NewRow()
        dr("EmpCode") = ""
        dr("_Description") = "All"
        objEmpCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlmandor.DataSource = objEmpCodeDs.Tables(0)
        ddlmandor.DataTextField = "_Description"
        ddlmandor.DataValueField = "EmpCode"
        ddlmandor.DataBind()
    End Sub

    Sub BindJabatan()
        Dim strOpCd As String = "HR_HR_STP_GROUPJOB_LIST_GET"
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim objDs As New Object()
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intselectIndex As Integer = 0

        Dim dr As DataRow

        ParamName = "SEARCH|SORT"
        ParamValue = "|"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd, ParamName, ParamValue, objDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_STP_GROUPJOB_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objDs.Tables(0).Rows.Count - 1
                objDs.Tables(0).Rows(intCnt).Item("GrpJobCode") = Trim(objDs.Tables(0).Rows(intCnt).Item("GrpJobCode"))
                objDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objDs.Tables(0).Rows(intCnt).Item("Description"))
            Next
        End If

        dr = objDs.Tables(0).NewRow()
        dr("GrpJobCode") = ""
        dr("Description") = "All"
        objDs.Tables(0).Rows.InsertAt(dr, 0)

        ddljabatan.DataSource = objDs.Tables(0)
        ddljabatan.DataTextField = "Description"
        ddljabatan.DataValueField = "GrpJobCode"
        ddljabatan.DataBind()
    End Sub

    Sub ddldivisi_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If ddldivisi.SelectedItem.Value <> "" Then
            BindMandoran(ddldivisi.SelectedItem.Value.Trim())
        End If
    End Sub

End Class
