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

Public Class GL_StdRpt_LaporanBiayaEstate : Inherits Page

    Protected RptSelect As UserControl

    Dim objGL As New agri.GL.clsReport()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGLTrx As New agri.GL.ClsTrx()

    Protected WithEvents hidAccMonthPX As HtmlInputHidden
    Protected WithEvents hidAccYearPX As HtmlInputHidden
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblTipe As Label
    Protected WithEvents ttrType As HtmlTableRow
    Protected WithEvents ttrBy As HtmlTableRow

    Protected WithEvents PrintPrev As ImageButton
    Protected WithEvents ddlBy As DropDownList
    Protected WithEvents ddlRptType As DropDownList
    Protected WithEvents ddlType As DropDownList
    Protected WithEvents cbExcel As CheckBox

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

    Dim objLangCapDs As New Object()

    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then

            End If
        End If
    End Sub

    Sub SelectBy(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If ddlRptType.SelectedValue = 5 Then
            ttrBy.Visible = False
            ttrType.Visible = False
            ddlBy.SelectedValue = 0
        Else
            ttrBy.Visible = True
        End If
    End Sub


    Sub BindDivisi()
        Dim strOpCd As String = "GL_CLSRPT_DDL_DIVISI_SEARCH"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer = 0
        Dim objAccDs As DataSet


        strParamName = "LocCode"
        strParamValue = strLocation

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objAccDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=FA_TRX_ASSETADD_ACCCODE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objAccDs.Tables(0).Rows.Count - 1
            objAccDs.Tables(0).Rows(intCnt).Item("ClassCode") = objAccDs.Tables(0).Rows(intCnt).Item("ClassCode").Trim()
        Next

       
        ddlType.DataSource = objAccDs.Tables(0)
        ddlType.DataValueField = "ClassCode"
        ddlType.DataBind()
        ddlType.SelectedIndex = intSelectedIndex
    End Sub


    Sub BindThnTnm()
        Dim strOpCd As String = "GL_CLSRPT_DDL_THNTANAM_SEARCH"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer = 0
        Dim objAccDs As DataSet


        strParamName = "LocCode"
        strParamValue = strLocation

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objAccDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=FA_TRX_ASSETADD_ACCCODE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objAccDs.Tables(0).Rows.Count - 1
            objAccDs.Tables(0).Rows(intCnt).Item("GroupCoa") = objAccDs.Tables(0).Rows(intCnt).Item("GroupCoa").Trim()
        Next

     

        ddlType.DataSource = objAccDs.Tables(0)
        ddlType.DataValueField = "GroupCoa"
        ddlType.DataBind()
        ddlType.SelectedIndex = intSelectedIndex
    End Sub

	Sub BindBlok()
        Dim strOpCd As String = "GL_CLSRPT_DDL_BLOK_SEARCH"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer = 0
        Dim objAccDs As DataSet


        strParamName = "LocCode"
        strParamValue = strLocation

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objAccDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=FA_TRX_ASSETADD_ACCCODE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objAccDs.Tables(0).Rows.Count - 1
            objAccDs.Tables(0).Rows(intCnt).Item("GroupCoa") = objAccDs.Tables(0).Rows(intCnt).Item("GroupCoa").Trim()
        Next
    

        ddlType.DataSource = objAccDs.Tables(0)
        ddlType.DataValueField = "GroupCoa"
        ddlType.DataBind()
        ddlType.SelectedIndex = intSelectedIndex
    End Sub


    Sub SelectType(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If ddlBy.SelectedValue = "0" Then
            ttrType.Visible = False
        ElseIf ddlBy.SelectedValue = "1" Then
            ttrType.Visible = True
            lblTipe.Text = "Divisi"
            BindDivisi()
        ElseIf ddlBy.SelectedValue = "2" Then
            ttrType.Visible = True
            lblTipe.Text = "Tahun Tanam"
            BindThnTnm()
		Else 
            ttrType.Visible = True
            lblTipe.Text = "Blok"
            BindBlok()	
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

        If Page.IsPostBack Then
        End If
    End Sub

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)

        Dim strddlAccMth As String
        Dim strddlAccYr As String
        Dim strRptId As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String
        Dim strRptType As String
        Dim strParam As String
        Dim strBy As String
        Dim strType As String

        Dim ddlist As DropDownList
        Dim intCnt As Integer

        Dim tempUserLoc As HtmlInputHidden
        Dim templblUL As Label
        Dim strTemp As String

        Dim strExportToExcel As String

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
            End If
        End If

        If Right(strUserLoc, 1) = "," Then
            strUserLoc = Left(strUserLoc, Len(strUserLoc) - 1)
        Else
            strUserLoc = Trim(strUserLoc)
        End If

        strRptType = ddlRptType.SelectedItem.Value.Trim()

        strExportToExcel = IIf(cbExcel.Checked = True, "1", "0")

        strBy = IIf(ttrBy.Visible = False, "", ddlBy.SelectedValue)
        strType = IIf(ttrType.Visible = False, "", ddlType.SelectedValue)


        Response.Write("<Script Language=""JavaScript"">window.open(""GL_StdRpt_LaporanBiayaKebunPreview.aspx?Type=Print&CompName=" & strCompany & _
                       "&Location=" & strUserLoc & _
                       "&DDLAccMth=" & strddlAccMth & _
                       "&DDLAccYr=" & strddlAccYr & _
                       "&RptId=" & strRptId & _
                       "&RptName=" & strRptName & _
                       "&Decimal=" & strDec & _
                       "&StrRptType=" & strRptType & _
                       "&ExportToExcel=" & strExportToExcel & _
                       "&By=" & strBy & _
                       "&TypeX=" & strType & _
                       """,""" & strRptId & """ ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub
End Class
