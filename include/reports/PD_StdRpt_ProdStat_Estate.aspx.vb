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


Public Class PD_StdRpt_ProdStat_Estate : Inherits Page

    Protected RptSelect As UserControl

    Dim objPD As New agri.PD.clsReport()
    Dim objPDTrx As New agri.PD.clsTrx()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
	Dim ObjOk As New agri.GL.ClsTrx()

    Protected WithEvents lblDateTo As Label
    Protected WithEvents lblDateFrom As Label
    Protected WithEvents lblTracker As Label

    Protected WithEvents lblLocation As Label
    Protected WithEvents txtDateFrom As TextBox
    Protected WithEvents txtDateTo As TextBox

    Protected WithEvents ddlMill As DropDownList
    Protected WithEvents ddlBlkGrp As DropDownList
    Protected WithEvents PrintPrev As ImageButton
	Protected WithEvents cbExcel As CheckBox

    Dim objLangCapDs As New Dataset()

    Dim TrMthYr As HtmlTableRow
    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfigSetting As Integer
    Dim strUserLoc As String
    Dim intCnt As Integer
    Dim intErrNo As Integer
    Dim strDateFormat As String
    Dim strBlockTag As String
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim objGLSetup As New agri.GL.clsSetup()
	Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_PDACCMONTH")
        strAccYear = Session("SS_PDACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigSetting = Session("SS_CONFIGSETTING")
        strDateFormat = Session("SS_DATEFMT")
        strLocation = Session("SS_LOCATION")
       
	    strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
			    txtDateFrom.Text = objGlobal.GetShortDate(strDateFormat, Now())
				txtDateTo.Text = objGlobal.GetShortDate(strDateFormat, Now())
                BindDivision()
				BindMill()
            End If
        End If
    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim ucTrMthYr As HtmlTableRow

        ucTrMthYr = RptSelect.FindControl("TrMthYr")
        ucTrMthYr.Visible = True

        If Page.IsPostBack Then
        End If
    End Sub

    Function CheckDate(ByVal strDate As String, ByRef strErrMsg As String) As String
        Dim strDateSetting As String
        Dim objSysCfgDs As DataSet
        Dim objDateFormat As String
        Dim strValidDate As String
        
        CheckDate = ""
        strErrMsg = ""
        If Not strDate = "" Then
            If objGlobal.mtdValidInputDate(strDateFormat, strDate, objDateFormat, strValidDate) = True Then
                CheckDate = strValidDate
            Else
                strErrMsg = "Date Entered should be in the format " & objDateFormat & "."
            End If
        End If
    End Function
	
	Sub BindDivision()
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_DIVISICODE_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objEmpDivDs As New Object
        Dim intCnt As Integer
        Dim dr As DataRow


        strParamName = "SEARCH|SORT"
        strParamValue = "AND A.LocCode='" & strLocation & "' AND A.Status='1'|ORDER By BlkGrpCode"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpDivDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_DIVISICODE_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objEmpDivDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpDivDs.Tables(0).Rows.Count - 1
                objEmpDivDs.Tables(0).Rows(intCnt).Item("BlkGrpCode") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("BlkGrpCode"))
                objEmpDivDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("BlkGrpCode")) & " (" & Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            Next
        End If

        dr = objEmpDivDs.Tables(0).NewRow()
        dr("BlkGrpCode") = " "
        dr("Description") = "Pilih Divisi"
        objEmpDivDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlBlkGrp.DataSource = objEmpDivDs.Tables(0)
        ddlBlkGrp.DataTextField = "Description"
        ddlBlkGrp.DataValueField = "BlkGrpCode"
        ddlBlkGrp.DataBind()
        ddlBlkGrp.SelectedIndex = 0

    End Sub
	
	Sub BindMILL()
        Dim strOpCd_EmpDiv As String = "PM_CLSSETUP_MILL_LIST_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
		Dim objEmpDivDs As New Object
        Dim dr As DataRow
		Dim intSelect As Integer

        strParamName = "SEARCHSTR"
        strParamValue = "AND PM.Status='1' ORDER By MillCode"

        Try
            intErrNo = Objok.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpDivDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_DIVISICODE_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objEmpDivDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpDivDs.Tables(0).Rows.Count - 1
                objEmpDivDs.Tables(0).Rows(intCnt).Item("MillCode") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("MillCode"))
                objEmpDivDs.Tables(0).Rows(intCnt).Item("MillDesc") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("MillCode")) & " (" & Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("MillDesc")) & ")"
            Next
        End If

        dr = objEmpDivDs.Tables(0).NewRow()
        dr("MillCode") = ""
        dr("MillDesc") = "Pilih Pabrik"
        objEmpDivDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlMill.DataSource = objEmpDivDs.Tables(0)
        ddlMill.DataTextField = "MillDesc"
        ddlMill.DataValueField = "MillCode"
        ddlMill.DataBind()
        ddlMill.SelectedIndex = 0

    End Sub
	
    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strYieldIDFrom As String
        Dim strYieldIDTo As String
        Dim strBlkType As String
        Dim strBlkGrp As String
        Dim strBlkCode As String
        Dim strSubBlkCode As String
        Dim strddlAccMth As String
        Dim strddlAccYr As String
        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String
        Dim strSupp As String
        Dim tempAccMth As DropDownList
        Dim tempAccYr As DropDownList
        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden

        Dim strDateFrom As String
        Dim strDateTo As String
        Dim tempDateFrom As TextBox
        Dim tempDateTo As TextBox
        Dim strDateSetting As String
        Dim strParam As String
        Dim strLblLocation As String

        Dim strSrchDivisi As String
        Dim strSrchPabrik As String
        Dim strExportToExcel As String
        
        Dim strErrDateFrom As String
        Dim strErrDateTo As String

        strDateFrom = Trim(txtDateFrom.Text)
        strDateTo = Trim(txtDateTo.Text)

        If strDateFrom = "" Then
            lblDateFrom.Text = "<br> Please insert Date From"
            lblDateFrom.Visible = True
            Exit Sub
        End If
        If strDateTo = "" Then
            lblDateTo.Text = "<br> Please insert Date To"
            lblDateTo.Visible = True
            Exit Sub
        End If

        strLblLocation = Trim(lblLocation.Text)
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
            End If
        End If


        strSrchDivisi = Trim(ddlBlkGrp.SelectedItem.Value)
        strSrchPabrik = Trim(ddlMill.SelectedItem.Value)

		strExportToExcel = IIf(cbExcel.Checked = True, "1", "0")
		
        strDateFrom = CheckDate(Trim(txtDateFrom.Text), strErrDateFrom)
        strDateTo = CheckDate(Trim(txtDateTo.Text), strErrDateTo)

        If strErrDateFrom = "" And strErrDateTo = "" Then
            lblDateFrom.visible = False
            lblDateTo.visible = False

            Response.Write("<Script Language=""JavaScript"">window.open(""PD_StdRpt_ProdStat_Estate_Preview.aspx?Type=Print&CompName=" & strCompany & _
                        "&Location=" & strUserLoc & _
                        "&DDLAccMth=" & strddlAccMth & _
                        "&DDLAccYr=" & strddlAccYr & _
                        "&RptID=" & strRptID & _
                        "&RptName=" & strRptName & _              
                        "&Decimal=" & strDec & _
                        "&Divisi=" & strSrchDivisi & _
						"&Pabrik=" & strSrchPabrik & _
                        "&DateFrom=" & strDateFrom & _
                        "&DateTo=" & strDateTo & _
						"&ExportToExcel=" & strExportToExcel & _
                        """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

        Else
            If strErrDateFrom = "" Then
                lblDateFrom.Visible = False
            Else
                lblDateFrom.Text = "<br>" & strErrDateFrom
                lblDateFrom.Visible = True
            End If
            
            If strErrDateTo = "" Then
                lblDateTo.Visible = False
            Else
                lblDateTo.Text = "<br>" & strErrDateTo
                lblDateTo.Visible = True
            End If
        End If

    End Sub
End Class
