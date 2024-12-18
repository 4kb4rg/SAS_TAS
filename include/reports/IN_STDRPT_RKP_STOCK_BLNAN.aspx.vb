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

Public Class IN_STDRPT_RKP_STOCK_BLNAN : Inherits Page

    Protected RptSelect As UserControl
    Protected objAdmin As New agri.Admin.clsShare()

    Dim objIN As New agri.IN.clsReport()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Protected objINSetup As New agri.IN.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objGLtrx As New agri.GL.clsTrx()
    

    Protected WithEvents lblDate As Label
    Protected WithEvents lblDateFormat As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents txtItemCode As TextBox
    Protected WithEvents txtItemCodeTo As TextBox

    Protected WithEvents lblLocation As Label
    Protected WithEvents lblRptError As Label

    Protected WithEvents PrintPrev As ImageButton

    Protected WithEvents cbExcel As CheckBox
    Protected WithEvents ddlType As DropDownList
    Protected WithEvents ddlCategory As DropDownList
    Protected WithEvents lstStorage As DropDownList
    Protected WithEvents lstRpt As DropDownList

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
    Dim strWS As String

    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        strLocType = Session("SS_LOCTYPE")
        lblDate.Visible = False
        lblDateFormat.Visible = False

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else

            If Not Page.IsPostBack Then
                onload_GetLangCap()
                ddlType.Items.Clear()                
                ddlType.Items.Add(New ListItem("Detail", "1"))                
                ddlType.Items.Add(New ListItem("Group Stock", "2"))
                
                ddlCategory.Items.Clear()
                ddlCategory.Items.Add(New ListItem("All", "1"))
                ddlCategory.Items.Add(New ListItem("Saldo Stock Nol ", "2"))
                ddlCategory.Items.Add(New ListItem("Saldo Stock Lebih Besar Dari Nol", "3"))
                BindReportNameList()
                BindStorage("")
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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PWSYSTEM_CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/IN_StdRpt_Selection.aspx")
        End Try

    End Sub
    Sub BindReportNameList()
        Dim strParam As String
        Dim objMapPath As String
        Dim dsForDropDown As New DataSet()
        Dim strOppCd_StdRptName_GET As String = "IN_STDRPT_NAME_GET"

        strParam = " WHERE ReportType = '" & Convert.ToString(objGlobal.EnumStdRptType.Inventory) & "' AND RptLevel='3'  AND Reportid IN (Select ReportID From SH_REPORT_USER Where UserId='" & strUserId & "') AND Status = '" & Convert.ToString(objGlobal.EnumStdRptStatus.Active) & "' ORDER BY RptName"
        Try
            intErrNo = objAdmin.mtdGetStdRptName(strOppCd_StdRptName_GET, strParam, dsForDropDown, objMapPath)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_IN_SELECTIONCTRL_REPORT_NAME_LIST&errmesg=" & Exp.ToString() & "&redirect=../en/reports/IN_StdRpt_Selection.aspx")
        End Try

        dr = dsForDropDown.Tables(0).NewRow()
        dr("ReportID") = ""
        dr("RptName") = "Select Report Name"
        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)

        lstRpt.DataSource = dsForDropDown.Tables(0)
        lstRpt.DataValueField = "ReportID"
        lstRpt.DataTextField = "RptName"
        lstRpt.DataBind()
        dsForDropDown = Nothing
    End Sub

    Sub BindStorage(ByVal pv_strcode As String)
        Dim strParamName As String
        Dim strParamValue As String
        Dim strOpCd_Get As String = "IN_CLSTRX_GLOBAL_UPD"

        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim sSQLKriteria As String = ""
        Dim objdsST As New DataSet
        Dim intSelectedIndex As Integer


        sSQLKriteria = "Select StorageCode,Description From IN_STORAGE Where LocCode='" & strLocation & "'"


        strParamName = "STRSEARCH"
        strParamValue = sSQLKriteria

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd_Get, _
                                         strParamName, _
                                         strParamValue, _
                                         objdsST)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=STOCK_ADJUSTMENT_UPDATE_FAILED&errmesg=&redirect=IN/Trx/IN_trx_StockAdj_list.aspx")
        End Try

        For intCnt = 0 To objdsST.Tables(0).Rows.Count - 1
            objdsST.Tables(0).Rows(intCnt).Item("StorageCode") = Trim(objdsST.Tables(0).Rows(intCnt).Item("StorageCode"))
            objdsST.Tables(0).Rows(intCnt).Item("Description") = objdsST.Tables(0).Rows(intCnt).Item("StorageCode") & " (" & Trim(objdsST.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objdsST.Tables(0).Rows(intCnt).Item("StorageCode") = Trim(pv_strcode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objdsST.Tables(0).NewRow()
        dr("StorageCode") = ""
        dr("Description") = "Please Select Storage"
        objdsST.Tables(0).Rows.InsertAt(dr, 0)

        lstStorage.DataSource = objdsST.Tables(0)
        lstStorage.DataValueField = "StorageCode"
        lstStorage.DataTextField = "Description"
        lstStorage.DataBind()
        lstStorage.SelectedIndex = intSelectedIndex

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
        Dim strddlAccMth As String
        Dim strddlAccYr As String
        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String
        Dim strParam As String

        Dim tempAccMth As DropDownList
        Dim tempAccYr As DropDownList
        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden


        'Dim objDateFrom As String
        'Dim objDateTo As String

        Dim objSysCfgDs As New Object()
        Dim strBin As String
        Dim strItemCode As String
        Dim strItemCodeTo As String
        Dim strExportToExcel As String
        Dim strStorage As String
        Dim strRpt As String


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
        strItemCode = Trim(txtItemCode.Text)
        strItemCodeTo = Trim(txtItemCodeTo.Text)
        strStorage=Trim(lstStorage.SelectedItem.Value)
        
        IF lstRpt.selectedItem.Value="" Then'
                lblRptError.Visible=True
            Exit sub
        Else
                lblRptError.Visible=False
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

        strExportToExcel = IIf(cbExcel.Checked = True, "1", "0")

        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"
        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=AR_STDRPT_GET_CONFIG_DATE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try


        Response.Write("<Script Language=""JavaScript"">window.open(""IN_STDRPT_RKP_STOCK_BLNANPreview.aspx?Type=Print&CompName=" & strCompany & _
                       "&Location=" & strUserLoc & _
                       "&RptID=" & strRptID & _
                       "&RptName=" & strRptName & _
                       "&Decimal=" & strDec & _
                       "&DDLAccMth=" & strddlAccMth & _
                       "&DDLAccYr=" & strddlAccYr & _
                       "&ExportToExcel=" & strExportToExcel & _
                       "&ItemCode=" & strItemCode & _
                       "&ItemCodeTo=" & strItemCodeTo & _
                       "&Storage=" & strStorage & _
                       "&ReportType=" & ddlType.SelectedValue & _
                       "&SaldoStock=" & ddlCategory.SelectedValue & _
                       "&ReportLevel=" & lstRpt.selectedItem.Value & _
                       """,""" & strRptID & """ ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

    End Sub

End Class
