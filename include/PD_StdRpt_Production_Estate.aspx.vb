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


Public Class PD_StdRpt_Production_Estate : Inherits Page

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

    Protected WithEvents ddlTipe As DropDownList
   
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

        Dim strSrchType As String
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


        strSrchType = Trim(ddlTipe.SelectedItem.Value)
      	strExportToExcel = IIf(cbExcel.Checked = True, "1", "0")
		
        strDateFrom = CheckDate(Trim(txtDateFrom.Text), strErrDateFrom)
        strDateTo = CheckDate(Trim(txtDateTo.Text), strErrDateTo)

        If strErrDateFrom = "" And strErrDateTo = "" Then
            lblDateFrom.visible = False
            lblDateTo.visible = False

            Response.Write("<Script Language=""JavaScript"">window.open(""PD_StdRpt_Production_Estate_Preview.aspx?Type=Print&CompName=" & strCompany & _
                        "&Location=" & strUserLoc & _
                        "&DDLAccMth=" & strddlAccMth & _
                        "&DDLAccYr=" & strddlAccYr & _
                        "&RptID=" & strRptID & _
                        "&RptName=" & strRptName & _              
                        "&Decimal=" & strDec & _
                        "&Tipe=" & strSrchType & _
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
