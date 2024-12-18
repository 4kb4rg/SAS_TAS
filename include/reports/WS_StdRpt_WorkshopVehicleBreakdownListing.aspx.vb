Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.Services
Imports System.Xml
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information

Public Class WS_StdRpt_WorkshopVehicleBreakdownListing : Inherits Page
    Protected RptSelect As UserControl
    
    Protected WithEvents lblLocation As Label
    Protected WithEvents lblWork As Label
    Protected WithEvents lblVehicle As Label
    Protected WithEvents lblBillParty As Label
    Protected WithEvents lblVehicleRegistrationNo As Label
    
    Protected WithEvents txtBillPartyCode As TextBox
    Protected WithEvents txtEmpCode As TextBox
    Protected WithEvents txtVehCode As TextBox
    Protected WithEvents txtVehRegNo As TextBox
    Protected WithEvents ddlJobType As DropDownList
    
    Protected WithEvents trBillPartyCode As HtmlTableRow
    Protected WithEvents trEmpCode As HtmlTableRow
    Protected WithEvents trVehCode As HtmlTableRow
    Protected WithEvents trVehRegNo As HtmlTableRow
    
    Protected WithEvents ibPrintPreview As ImageButton
    
    Dim objWS As New agri.WS.clsReport()
    Dim objWSTrx As New agri.WS.clsTrx()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objLoc As New agri.Admin.clsLoc()

    
    Dim dsLangCap As New DataSet()
    
    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strLocType as String

    
    Dim intCnt As Integer
    Dim intErrNo As Integer

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
        
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
                Call GetLangCap
                Call BindJobTypeDropDownList
            End If
        End If
    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim trMthYr As HtmlTableRow
        
        trMthYr = RptSelect.FindControl("TrMthYr")
        trMthYr.Visible = True
    End Sub
    
    Sub GetLangCap()
        dsLangCap = GetLanguageCaptionDS()
        
        lblLocation. Text = GetCaption(objLangCap.EnumLangCap.Location)
        lblWork.Text = GetCaption(objLangCap.EnumLangCap.Work)
        lblVehicle.Text = GetCaption(objLangCap.EnumLangCap.Vehicle)
        lblBillParty.Text = GetCaption(objLangCap.EnumLangCap.BillParty)
        lblVehicleRegistrationNo.Text = GetCaption(objLangCap.EnumLangCap.Vehicle) & " Registration No"
    End Sub



    Function GetCaption(ByVal pv_TermCode as String) As String
        Dim I As Integer

       For I = 0 To dsLangCap.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(dsLangCap.Tables(0).Rows(I).Item("TermCode")) Then
                If strLocType = objLoc.EnumLocType.Mill then
                    Return Trim(dsLangCap.Tables(0).Rows(I).Item("BusinessTermMW"))
                else
                    Return Trim(dsLangCap.Tables(0).Rows(I).Item("BusinessTerm"))
                end if
                Exit For
            End If
        Next
    End Function
    
    Function GetLanguageCaptionDS() As DataSet
        Dim strOpCode_BussTerm As String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
        Dim dsLC As DataSet
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
                                                 dsLC, _
                                                 strParam)
        Catch ex As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_STDRPT_WORKSHOP_VEHICLE_BREAKDOWN_LISTING_GET_LANGCAP&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_Job_List.aspx")
        End Try

        Return dsLC
        If Not dsLC Is Nothing Then
            dsLC = Nothing
        End If
    End Function
    
    Sub BindJobTypeDropDownList() 
        ddlJobType.iTems.Clear
        ddlJobType.Items.Add(New ListItem(objWSTrx.mtdGetJobType(objWSTrx.EnumJobType.All), objWSTrx.EnumJobType.All))
        ddlJobType.Items.Add(New ListItem(objWSTrx.mtdGetJobType(objWSTrx.EnumJobType.InternalUse), objWSTrx.EnumJobType.InternalUse))
        ddlJobType.Items.Add(New ListItem(objWSTrx.mtdGetJobType(objWSTrx.EnumJobType.StaffPayroll), objWSTrx.EnumJobType.StaffPayroll))
        ddlJobType.Items.Add(New ListItem(objWSTrx.mtdGetJobType(objWSTrx.EnumJobType.StaffDebitNote), objWSTrx.EnumJobType.StaffDebitNote))
        ddlJobType.Items.Add(New ListItem(objWSTrx.mtdGetJobType(objWSTrx.EnumJobType.ExternalParty), objWSTrx.EnumJobType.ExternalParty))
        ddlJobType.SelectedIndex = 0
    End Sub 

     Sub ddlJobType_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim intJobType As Integer = Trim(GetDropDownListValue(ddlJobType))
        Select Case intJobType
            Case objWSTrx.EnumJobType.All
                trBillPartyCode.Visible = True
                trEmpCode.Visible = True
                trVehCode.Visible = True
                trVehRegNo.Visible = True
            Case objWSTrx.EnumJobType.StaffPayroll, objWSTrx.EnumJobType.StaffDebitNote
                trBillPartyCode.Visible = False
                trEmpCode.Visible = True
                trVehCode.Visible = False
                trVehRegNo.Visible = True
            Case objWSTrx.EnumJobType.ExternalParty
                trBillPartyCode.Visible = True
                trEmpCode.Visible = False
                trVehCode.Visible = False
                trVehRegNo.Visible = True
            Case Else   
                trBillPartyCode.Visible = False
                trEmpCode.Visible = False
                trVehCode.Visible = True
                trVehRegNo.Visible = False
        End Select
    End Sub
    
    Sub ibPrintPreview_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim lblTemp As Label
        Dim hihTemp As HtmlInputHidden
        Dim ddlTemp As DropDownList
        
        Dim strJobType As String
        Dim strBillPartyCode As String
        Dim strEmpCode As String
        Dim strVehCode As String
        Dim strVehRegNo As String
        
        Dim strRptID As String
        Dim strRptName As String
        Dim strRptLocation As String
        Dim strRptAccMonth As String
        Dim strRptAccYear As String
        Dim strDecimal As String
        Dim strErrMsg As String
        Dim blnErrMsg As Boolean = False
        
        ddlTemp = RptSelect.FindControl("lstRptName")
        strRptID = Server.UrlEncode(Trim(ddlTemp.SelectedItem.Value))
        strRptName = Server.UrlEncode(Trim(ddlTemp.SelectedItem.Text))
        hihTemp = RptSelect.FindControl("hidUserLoc")
        strRptLocation = Trim(hihTemp.Value)
        ddlTemp = RptSelect.FindControl("lstAccMonth")
        strRptAccMonth = Server.UrlEncode(Trim(ddlTemp.SelectedItem.Value))
        ddlTemp = RptSelect.FindControl("lstAccYear")
        strRptAccYear = Server.UrlEncode(Trim(ddlTemp.SelectedItem.Value))
        ddlTemp = RptSelect.FindControl("lstDecimal")
        strDecimal = Server.UrlEncode(Trim(ddlTemp.SelectedItem.Value))

        If strRptLocation = "" Then
            lblTemp = RptSelect.FindControl("lblUserLoc")
            lblTemp.Visible = True
            blnErrMsg = True
        Else
            If Left(strRptLocation, 3) = "','" Then
                strRptLocation = Right(strRptLocation, Len(strRptLocation) - 3)
            ElseIf Right(strRptLocation, 3) = "','" Then
                strRptLocation = Left(strRptLocation, Len(strRptLocation) - 3)
            End If
        End If
        strRptLocation = Server.UrlEncode(strRptLocation)
        
        strJobType = Server.UrlEncode(Trim(GetDropDownListValue(ddlJobType)))
        strBillPartyCode = Server.UrlEncode(Trim(txtBillPartyCode.Text))
        strEmpCode = Server.UrlEncode(Trim(txtEmpCode.Text))
        strVehCode = Server.UrlEncode(Trim(txtVehCode.Text))
        strVehRegNo = Server.UrlEncode(Trim(txtVehRegNo.Text))
        
        If blnErrMsg = True Then
            Exit Sub
        End If
        
        Response.Write("<Script Language=""JavaScript"">window.open(""WS_StdRpt_WorkshopVehicleBreakdownListingPreview.aspx?CompName=" & strCompany & "&RptID=" & strRptID & "&RptName=" & strRptName & "&RptLocation=" & strRptLocation & "&RptAccMonth=" & strRptAccMonth & "&RptAccYear=" & strRptAccYear & "&Decimal=" & strDecimal & _
                        "&lblLocation=" & lblLocation.Text & "&lblWork=" & lblWork.Text & "&lblVehicle=" & lblVehicle.Text & "&lblVehicleRegistrationNo=" & lblVehicleRegistrationNo.Text & "&lblBillParty=" & lblBillParty.Text & _
                        "&JobType=" & strJobType & _
                        "&BillPartyCode=" & strBillPartyCode & "&EmpCode=" & strEmpCode & _
                        "&VehCode=" & strVehCode & "&VehRegNo=" & strVehRegNo & _
                        """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub
    
    Protected Function GetDropDownListValue(ByRef pr_ddlObject As DropDownList) As String
        If Trim(Request.Form(pr_ddlObject.ID)) <> "" Then
            GetDropDownListValue = Trim(Request.Form(pr_ddlObject.ID))
        Else
            GetDropDownListValue = pr_ddlObject.SelectedItem.Value
        End If
    End Function
End Class
