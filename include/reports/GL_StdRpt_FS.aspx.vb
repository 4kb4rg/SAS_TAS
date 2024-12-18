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

Public Class GL_StdRpt_FS : Inherits Page

    Protected RptSelect As UserControl
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objSysCfg As New agri.PWSystem.clsConfig()
    Protected objLangCap As New agri.PWSystem.clsLangCap()
    Protected objPU As New agri.PU.clsTrx()
    Protected objAdminSetup As New agri.Admin.clsLoc()

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblErrRpt As Label
    Protected WithEvents ddlReport As DropDownList
    Protected WithEvents PrintPrev As ImageButton
    Protected WithEvents rbSuppYes As RadioButton
    Protected WithEvents rbSuppNo As RadioButton
    Protected WithEvents rbConsole As RadioButton
    Protected WithEvents lstDisplayType As DropDownList
    Protected WithEvents cbExcel As CheckBox
    Protected WithEvents ddlOption As DropDownList
    Protected WithEvents chkAudited As CheckBox
    Protected WithEvents chkDetail As CheckBox
    Protected WithEvents ddlLocCode As DropDownList

    Dim objLangCapDs As New Object()
    
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfigsetting As Integer
    Dim dr As DataRow
    Dim tempActGrp As String
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String	
    Dim objReportDs As New Object()

    Dim intCnt As Integer
    Dim intErrNo As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
 
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")

        lblErrRpt.visible = false

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack
                BindReportCode("")
                BindLocCode()
            End If
        End If

    End Sub

    Sub BindReportCode(ByVal pv_strReportCode As String)
        Dim strOpCode As String = "GL_CLSSETUP_FSTEMPLATE_LIST_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedReportIndex As Integer = 0
        Dim objGLSetup As New agri.GL.clsSetup()

        strParam = "|"
          
        Try
            intErrNo = objGLSetup.mtdGetFSTemplateList(strOpCode, _
                                              strCompany, _
                                              strLocation, _
                                              strUserId, _
                                              strParam, _
                                              objReportDs)
        Catch Exp As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GL_FSTEMPLATE_LIST_GET&errmesg=" & Exp.Message & "&redirect=../en/reports/GL_StdRpt_Selection.aspx")
        End Try

        For intCnt = 0 To objReportDs.Tables(0).Rows.Count - 1
            objReportDs.Tables(0).Rows(intCnt).Item("ReportCode") = objReportDs.Tables(0).Rows(intCnt).Item("ReportCode").Trim()
            objReportDs.Tables(0).Rows(intCnt).Item("Name") = objReportDs.Tables(0).Rows(intCnt).Item("ReportCode") & " (" & objReportDs.Tables(0).Rows(intCnt).Item("Name").Trim() & ")"
          
        Next intCnt

        Dim dr As DataRow
        dr = objReportDs.Tables(0).NewRow()
        dr("ReportCode") = ""
        dr("Name") = "Select Report Name" 
        objReportDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlReport.DataSource = objReportDs.Tables(0)
        ddlReport.DataValueField = "ReportCode"
        ddlReport.DataTextField = "Name"
        ddlReport.DataBind()
        ddlReport.SelectedIndex = intSelectedReportIndex
        

    End Sub


    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
       
        Dim htmltr As HtmlTableRow
 
        htmltr = RptSelect.FindControl("TrMthYr")
        htmltr.visible = True
        htmltr = RptSelect.FindControl("TrCheckLoc")
        htmltr.Visible = False
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
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

    
    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strddlAccMth As String
        Dim strddlAccYr As String
        Dim strRptId As String
        Dim strRptName As String
        Dim strUserLoc As String = ""
        Dim strDec As String
        Dim strSupp As String
        Dim strRptPkID As String
        Dim strRptTitle As String
        Dim intRptType As Integer
        Dim strExportToExcel As String
        Dim strOption As String
        Dim strAudited As String
        Dim strDetail As String

        Dim ddl As Dropdownlist
        Dim tempUserLoc As HtmlInputHidden
        Dim templblUL As Label

        ddl = RptSelect.FindControl("lstAccMonth")
        strddlAccMth = Trim(ddl.SelectedItem.Value)

        ddl = RptSelect.FindControl("lstAccYear")
        strddlAccYr = Trim(ddl.SelectedItem.Value)

        ddl = RptSelect.FindControl("lstRptName")
        strRptId = Trim(ddl.SelectedItem.Value)
        strRptName = Trim(ddl.SelectedItem.text)

        ddl = RptSelect.FindControl("lstDecimal")
        strDec = Trim(ddl.SelectedItem.Value)

        strLocation = ddlLocCode.SelectedItem.Value
        strRptPkID = ddlReport.SelectedItem.Value
        strRptTitle = ddlReport.SelectedItem.Text
        strOption = ddlOption.SelectedItem.Value
        strAudited = IIf(chkAudited.Checked = True, "1", "0")
        strDetail = IIf(chkDetail.Checked = True, "1", "0")
        strExportToExcel = IIf(cbExcel.Checked = True, "1", "0")

        Response.Write("<Script Language=""JavaScript"">window.open(""GL_StdRpt_FSPreview.aspx?Type=Print&CompName=" & strCompany & _
                       "&RptID=" & strRptId & _
                       "&RptTitle=" & strRptTitle & _
                       "&Decimal=" & strDec & _
                       "&AccMonth=" & strddlAccMth & _
                       "&AccYear=" & strddlAccYr & _
                       "&RptPkID=" & strRptPkID & _
                       "&SelLocation=" & strLocation & _
                       "&RepType=" & intRptType & _
                       "&RepOption=" & strOption & _
                       "&Audited=" & strAudited & _
                       "&Detail=" & strDetail & _
                       "&ExportToExcel=" & strExportToExcel & _
                       """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

    End Sub

    Sub BindLocCode()
        Dim strOpCd As String = "ADMIN_CLSLOC_LOCATION_LIST_GET"
        Dim objToLocCodeDs As New Object()
        Dim strToLocCode As String
        Dim strParam As String = ""
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer

        strToLocCode = ""
        strParam = "|" & objAdminSetup.EnumLocStatus.Active & "|LocCode|"

        Try
            intErrNo = objPU.mtdGetLoc(strOpCd, strParam, objToLocCodeDs, "")

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LOC&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_dalist.aspx")
        End Try

        For intCnt = 0 To objToLocCodeDs.Tables(0).Rows.Count - 1
            With objToLocCodeDs.Tables(0).Rows(intCnt)
                .Item("LocCode") = Trim(.Item("LocCode"))
                .Item("Description") = Trim(.Item("LocCode")) & " (" & Trim(.Item("Description")) & ")"
            End With
        Next intCnt

        dr = objToLocCodeDs.Tables(0).NewRow()
        dr("LocCode") = ""
        dr("Description") = "ALL"
        objToLocCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlLocCode.DataSource = objToLocCodeDs.Tables(0)
        ddlLocCode.DataValueField = "LocCode"
        ddlLocCode.DataTextField = "Description"
        ddlLocCode.DataBind()
        ddlLocCode.SelectedIndex = intSelectedIndex
    End Sub
End Class
