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

Public Class PU_StdRpt_BAPP_Pembayaran : Inherits Page

    Protected RptSelect As UserControl
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objSysCfg As New agri.PWSystem.clsConfig()
    Protected objLangCap As New agri.PWSystem.clsLangCap()

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents PrintPrev As ImageButton
    Protected WithEvents btnDate As Image

    Protected WithEvents ddlRpt As DropDownList
    Protected WithEvents ddlRptType As DropDownList
    Protected WithEvents txtSplCode As TextBox
    Protected WithEvents txtDoc As TextBox
    Protected WithEvents txtPotMaterial As TextBox
    Protected WithEvents txtPotPinjaman As TextBox
	Protected WithEvents txtPotDP As TextBox
    Protected WithEvents cbExcel As CheckBox

    Dim objGLRpt As New agri.GL.clsReport()


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
    Dim strDateFmt As String
    Dim strAcceptFormat As String

    Dim VPA As String
    Dim GeneralManager As String
    Dim Manager As String
    Dim KTU As String

    Dim dr As DataRow
    Dim intCnt As Integer
    Dim intErrNo As Integer

    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        Dim strMonth As String
        Dim strYear As String
        Dim strDate As String

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strDateFmt = Session("SS_DATEFMT")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else

            If Not Page.IsPostBack Then
              
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
    End Sub

    Sub GetLocationData()

        Dim strOpCode As String = "PWSYSTEM_SHLOCATION_GET"

        Dim intErrNo As Integer

        Dim objRptDs As New DataSet()
        Dim objRptDs1 As New DataSet()
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim objMapPath As String
        Dim objFTPFolder As String


        strParamName = "LOCCODE"
        strParamValue = strLocation

        Try
            intErrNo = objGLRpt.mtdGetReportDataCommon(strOpCode, strParamName, strParamValue, objRptDs, objMapPath, objFTPFolder)

            'objRptDs.Tables(0).Row(1)

        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PU_STDRPT_BAPP_PEMBAYARAN&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        VPA = Trim(objRptDs.Tables(0).Rows(0).Item("vpa"))
        GeneralManager = Trim(objRptDs.Tables(0).Rows(0).Item("gm"))
        Manager = Trim(objRptDs.Tables(0).Rows(0).Item("manager"))
        KTU = Trim(objRptDs.Tables(0).Rows(0).Item("ktu"))

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
                If strLocType = objAdminLoc.EnumLocType.Mill Then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                Else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                End If
                Exit For
            End If
        Next
    End Function


    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strRptID As String
        Dim strRptTitle As String
        Dim strDecimal As String


        Dim intType As Integer
        Dim strUserLoc As String
        Dim tempUserLoc As HtmlInputHidden
        Dim templblUL As Label
        Dim ddl As DropDownList

        Dim strRpt As String
        Dim strRptType As String
        Dim strSplCode As String
        Dim strDoc As String
        Dim strPotMaterial As String
        Dim strPotPinjaman As String
		Dim strPotDP As String
        Dim strExportToExcel As String

        Dim tempAccMt As DropDownList
        Dim strddlAccMt As String
        Dim tempAccYr As DropDownList
        Dim strddlAccYr As String

        ddl = RptSelect.FindControl("lstRptName")
        strRptID = ddl.SelectedItem.Value

        ddl = RptSelect.FindControl("lstRptname")
        strRptTitle = ddl.SelectedItem.Text

        ddl = RptSelect.FindControl("lstDecimal")
        strDecimal = Trim(ddl.SelectedItem.Value)

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
            End If
        End If


        strRpt = ddlRpt.SelectedItem.Value
        strRptType = ddlRptType.SelectedItem.Value
        strSplCode = txtSplCode.Text
        strDoc = txtDoc.Text
        strPotMaterial = txtPotMaterial.Text
        strPotPinjaman = txtPotPinjaman.Text
		strPotDP = txtPotDP.Text
		
        strExportToExcel = IIf(cbExcel.Checked = True, "1", "0")

        tempAccMt = RptSelect.FindControl("lstAccMonth")
        strddlAccMt = Trim(tempAccMt.SelectedItem.Value)
        tempAccYr = RptSelect.FindControl("lstAccYear")
        strddlAccYr = Trim(tempAccYr.SelectedItem.Value)

        strUserLoc = ""
        'perintah untuk mendapatkan data vpa, gm, manager, dan ktu utk laporan biaya bayar bapp
        GetLocationData()

        Response.Write("<Script Language=""JavaScript"">window.open(""PU_StdRpt_BAPP_PembayaranPreview.aspx?Type=Print&CompName=" & strCompany & _
                       "&RptID=" & strRptID & _
                       "&RptTitle=" & strRptTitle & _
                       "&Decimal=" & strDecimal & _
                       "&Location=" & strUserLoc & _
                       "&strRpt=" & strRpt & _
                       "&strRptType=" & strRptType & _
                       "&strSplCode=" & strSplCode & _
                       "&strDoc=" & strDoc & _
                       "&strPotMaterial=" & strPotMaterial & _
                       "&strPotPinjaman=" & strPotPinjaman & _
					   "&strPotDP=" & strPotDP & _
                       "&strExportToExcel=" & strExportToExcel & _
                       "&DDLAccMt=" & strddlAccMt & _
                       "&DDLAccYr=" & strddlAccYr & _
                       "&vpa=" & VPA & _
                       "&gm=" & GeneralManager & _
                       "&manager=" & Manager & _
                       "&ktu=" & KTU & _
                       """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

    End Sub

End Class
