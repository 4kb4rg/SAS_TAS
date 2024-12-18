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

Public Class PR_StdRpt_bukumandor_Estate : Inherits Page
    Protected WithEvents PrintPrev As ImageButton
    Protected WithEvents BtnDateFrom As Image
    Protected WithEvents BtnDateTo As Image
    Protected RptSelect As UserControl

    Dim objOk As New agri.GL.ClsTrx()
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblLocation As Label

    Protected WithEvents ddldivisi As DropDownList
    Protected WithEvents ddlmandor As DropDownList
    Protected WithEvents ddlempcode As DropDownList
    Protected WithEvents ddlsubcat As DropDownList

    Protected WithEvents txtFromTrxDate As TextBox
    Protected WithEvents txtToTrxDate As TextBox

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
    Dim strAcceptFormat As String
    Dim intSelectedIndex As String

    Function Date_Validation(ByVal pv_strInputDate As String, ByVal pv_blnIsShortDate As Boolean) As String
        Dim objSysCfgDs As New Object()
        Dim objActualDate As New Object()
        Dim strDateFormat As String
        Dim strParam As String
        Dim intErrNo As Integer

        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"

        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GR_GET_CONFIG&errmesg=" & Exp.Message & "&redirect=pu/trx/pu_GRList.aspx")
        End Try

        strDateFormat = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))

        If pv_blnIsShortDate Then
            Date_Validation = objGlobal.GetShortDate(strDateFormat, pv_strInputDate)
        Else
            If objGlobal.mtdValidInputDate(strDateFormat, _
                                           pv_strInputDate, _
                                           strAcceptFormat, _
                                           objActualDate) = True Then
                Date_Validation = objActualDate
            Else
                Date_Validation = ""
            End If
        End If
    End Function

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

        lblErrMessage.Visible = False

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            intSelectedIndex = Trim(IIf(Request.QueryString("StrIndex") <> "", Request.QueryString("StrIndex"), Request.Form("StrIndex")))

            If Not Page.IsPostBack Then
                txtFromTrxDate.Text = objGlobal.GetShortDate(strDateFmt, Now())
                txtToTrxDate.Text = objGlobal.GetShortDate(strDateFmt, Now())
                BindDivisiCode()
                ddldivisi_OnSelectedIndexChanged(Sender, E)
                BindSubCat()
            End If
        End If

    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim ucTrDecimal As HtmlTableRow

        ucTrDecimal = RptSelect.FindControl("TrDecimal")
        ucTrDecimal.Visible = True
    End Sub

    Sub BindSubCat()
        ddlsubcat.Items.Clear()
        Select Case intSelectedIndex.Trim
            Case "rptpr1000060"
                ddlsubcat.Items.Add(New ListItem("TBS", "PBS"))
                ddlsubcat.Items.Add(New ListItem("Brondolan", "KBS"))
                ddlsubcat.Items.Add(New ListItem("Karet", "DRK"))
            Case "rptpr1000061"
                ddlsubcat.Items.Add(New ListItem("TBS", "PBS"))
                ddlsubcat.Items.Add(New ListItem("Brondolan", "KBS"))
            Case "rptpr1000062"
                ddlsubcat.Items.Add(New ListItem("None", ""))
        End Select
    End Sub

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strddlAccMth As String
        Dim strddlAccYr As String
        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String
        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden

        Dim strFrom As String = ""
        Dim strTo As String = ""
        Dim strEmpCode As String = ""
        Dim strDivisi As String = ""
        Dim strMandor As String = ""
        Dim strSubCat As String = ""

        Dim strDuration As String = "0"

        strFrom = Date_Validation(txtFromTrxDate.Text.Trim, False)
        strTo = Date_Validation(txtToTrxDate.Text.Trim, False)

        If strFrom = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Silakan input tanggal !"
            Exit Sub
        End If

        If strTo = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Silakan input tanggal !"
            Exit Sub
        End If

        If Format(CDate(strFrom), "yyyyMMdd") > Format(CDate(strTo), "yyyyMMdd") Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Silakan koreksi tanggal !"
            Exit Sub
        End If

        strddlAccMth = Month(strFrom)
        strddlAccYr = Year(strTo)
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

        If ddlempcode.SelectedItem.Value.Trim = "" Then
            strEmpCode = ""
        Else
            strEmpCode = Trim(ddlempcode.SelectedItem.Value.Trim)
        End If

        If ddldivisi.SelectedItem.Value = "" Then
            strDivisi = ""
        Else
            strDivisi = Trim(ddldivisi.SelectedItem.Value.Trim)
        End If

        If ddlmandor.Text = "" Then
            strMandor = ""
        Else
            If ddlmandor.SelectedItem.Value = "" Then
                strMandor = ""
            Else
                strMandor = Trim(ddlmandor.SelectedItem.Value.Trim)
            End If

        End If

        strSubCat = ddlsubcat.SelectedItem.Value.Trim

        Response.Write("<Script Language=""JavaScript"">window.open(""PR_StdRpt_bukumandorPreview_Estate.aspx?CompCode=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & strDec & _
                       "&lblLocation=" & lblLocation.Text & "&AccMonth=" & strddlAccMth & "&AccYear=" & strddlAccYr & _
                       "&SelDateFrom=" & strFrom & "&SelDateTo=" & strTo & _
                       "&EmpCode=" & strEmpCode & "&Divisi=" & strDivisi & "&Mandor=" & strMandor & "&SubCat=" & strSubCat & """ ,""" & strRptID & """ ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
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

    Sub BindMandor(ByVal strDivCode As String)
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_BKM_MANDOR_LIST"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim objEmpCodeDs As New Object()
        Dim dr As DataRow


        strParamName = "LOC|SEARCH|SORT"
        strParamValue = strLocation & "|AND a.Status='1' AND c.IDDiv like '%" & strDivCode & "%' |ORDER BY b.EmpName"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_MANDOR_LIST&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objEmpCodeDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpCodeDs.Tables(0).Rows.Count - 1
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("codeemp") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("codeemp"))
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("empname") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("empname"))
            Next
        End If

        dr = objEmpCodeDs.Tables(0).NewRow()
        dr("codeemp") = ""
        dr("empname") = "All"
        objEmpCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlmandor.DataSource = objEmpCodeDs.Tables(0)
        ddlmandor.DataTextField = "empname"
        ddlmandor.DataValueField = "codeemp"
        ddlmandor.DataBind()
    End Sub

    Sub BindEmployee(ByVal strdivcode As String, ByVal MandorCode As String)
        Dim strOpCd_EmpDiv As String = "HR_HR_TRX_EMPLOYEE_LIST_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim objEmpCodeDs As New Object()
        Dim dr As DataRow
        Dim tmpParam As String
		Dim SM As String 
      
	    IF cint(strAccMonth) < 10 Then
			SM = "0" & strAccMonth
		Else 
		    SM = strAccMonth
		End If

        
        strParamName = "LOC|AM|AY|SEARCH|SORT"
        If MandorCode <> "" Then
            tmpParam = "AND A.EmpCode IN (SELECT y.CodeEmp FROM hr_trx_empmandor x,hr_trx_empmandorln y WHERE x.mandorcode=y.codemandor and x.codeemp='" & MandorCode & "') "
        Else
            tmpParam = ""
        End If
        strParamValue =  strLocation & "|" & SM & "|" & strAccYear & "|" & tmpParam & " AND A.Status='1' AND A.LocCode = '" & strLocation & "' AND C.IDDiv like '%" & strdivcode & "%' |ORDER BY A.EmpName"

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

        ddlempcode.DataSource = objEmpCodeDs.Tables(0)
        ddlempcode.DataTextField = "_Description"
        ddlempcode.DataValueField = "EmpCode"
        ddlempcode.DataBind()
    End Sub

    Sub ddldivisi_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        BindMandor(ddldivisi.SelectedItem.Value.Trim())
        BindEmployee(ddldivisi.SelectedItem.Value.Trim(), "")
    End Sub

End Class
