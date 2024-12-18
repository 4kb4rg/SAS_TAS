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

Public Class WS_StdRpt_VehBreakdownList : Inherits Page

    Protected RptSelect As UserControl

    Dim objWS As New agri.WS.clsReport()
    Dim objWSTrx As New agri.WS.clsTrx()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Protected WithEvents lblDate As Label
    Protected WithEvents lblDateFormat As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label

    Protected WithEvents lblTypeOfVeh As Label
    Protected WithEvents lblVehRegNo As Label
    Protected WithEvents lblVehExpCode As Label
    Protected WithEvents lblBillPartyCode As Label
    Protected WithEvents lblWorkCode As Label
    Protected WithEvents lblServTypeCode As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents lblVeh As Label
    Protected WithEvents lblCompany As Label

    Protected WithEvents txtJobIDFrom As TextBox
    Protected WithEvents txtJobIDTo As TextBox
    Protected WithEvents txtJobCreateDateFrom As TextBox
    Protected WithEvents txtJobCreateDateTo As TextBox
    Protected WithEvents txtBillPartyCode As TextBox
    Protected WithEvents txtDateInFrom As TextBox
    Protected WithEvents txtDateInTo As TextBox
    Protected WithEvents lstTypeOfVeh As DropDownList
    Protected WithEvents txtVehRegNo As TextBox
    Protected WithEvents txtVehExpCode As TextBox
    Protected WithEvents txtWorkCode As TextBox
    Protected WithEvents txtServTypeCode As TextBox
    Protected WithEvents txtMechIDFrom As TextBox
    Protected WithEvents txtMechIDTo As TextBox
    Protected WithEvents lstStatus As DropDownList

    Protected WithEvents btnSelDateFrom As Image
    Protected WithEvents btnSelDateTo As Image
    Protected WithEvents btnSelDateInFrom As Image
    Protected WithEvents btnSelDateInTo As Image

    Protected WithEvents PrintPrev As ImageButton

    Dim objLangCapDs As New Object()
    Dim objLoc As New agri.Admin.clsLoc()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strLocType as String


    Dim dr As DataRow
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


        lblDate.Visible = False
        lblDateFormat.Visible = False

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else

            If Not Page.IsPostBack Then
                onload_GetLangCap()
                BindVehType()
                BindStatusList()
            End If

        End If
    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim UCTrMthYr As HtmlTableRow

        UCTrMthYr = RptSelect.FindControl("TrMthYr")
        UCTrMthYr.Visible = True
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        lblBillPartyCode.Text = GetCaption(objLangCap.EnumLangCap.BillParty) & " Code :"
        lblTypeOfVeh.Text = "Type of " & GetCaption(objLangCap.EnumLangCap.Vehicle) & " :"
        lblVehRegNo.Text = GetCaption(objLangCap.EnumLangCap.Vehicle) & " Code/Reg No :"
        lblVehExpCode.Text = GetCaption(objLangCap.EnumLangCap.VehExpense) & " Code :"
        lblWorkCode.Text = GetCaption(objLangCap.EnumLangCap.Work) & " Code :"
        lblServTypeCode.Text = GetCaption(objLangCap.EnumLangCap.ServType) & " Code :"
        lblLocation.Text = GetCaption(objLangCap.EnumLangCap.Location)
        lblVeh.Text = GetCaption(objLangCap.EnumLangCap.Vehicle)
        lblCompany.Text = GetCaption(objLangCap.EnumLangCap.Company)
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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PWSYSTEM_CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/WS_StdRpt_Selection.aspx")
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

    Sub BindVehType()

        lstTypeOfVeh.Items.Add(New ListItem("All", "A"))
        lstTypeOfVeh.Items.Add(New ListItem(lblCompany.Text, "C"))
        lstTypeOfVeh.Items.Add(New ListItem("Personal", "P"))

        lstTypeOfVeh.SelectedIndex = 1
    End Sub

    Sub BindStatusList()
        lstStatus.Items.Add(New ListItem(objWSTrx.mtdGetJobStatus(objWSTrx.EnumJobStatus.All), objWSTrx.EnumJobStatus.All))
        lstStatus.Items.Add(New ListItem(objWSTrx.mtdGetJobStatus(objWSTrx.EnumJobStatus.Active), objWSTrx.EnumJobStatus.Active))
        lstStatus.Items.Add(New ListItem(objWSTrx.mtdGetJobStatus(objWSTrx.EnumJobStatus.Closed), objWSTrx.EnumJobStatus.Closed))
        lstStatus.Items.Add(New ListItem(objWSTrx.mtdGetJobStatus(objWSTrx.EnumJobStatus.Deleted), objWSTrx.EnumJobStatus.Deleted))

        lstStatus.SelectedIndex = 2
    End Sub

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strJobIDFrom As String
        Dim strJobIDTo As String
        Dim strJobCreateDateFrom As String
        Dim strJobCreateDateTo As String
        Dim strDateInFrom As String
        Dim strDateInTo As String
        Dim strBillPartyCode As String
        Dim strTypeOfVeh As String
        Dim strVehRegNo As String
        Dim strVehExpCode As String
        Dim strWorkCode As String
        Dim strServTypeCode As String
        Dim strMechIDFrom As String
        Dim strMechIDTo As String
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
        Dim strDateSetting As String
        Dim objSysCfgDs As New Object()
        Dim objDateFormat As New Object()
        Dim objDateFrom As String
        Dim objDateTo As String
        Dim objDateInFrom As String
        Dim objDateInTo As String

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

        If txtJobIDFrom.Text = "" Then
            strJobIDFrom = ""
        Else
            strJobIDFrom = Trim(txtJobIDFrom.Text)
        End If

        If txtJobIDTo.Text = "" Then
            strJobIDTo = ""
        Else
            strJobIDTo = Trim(txtJobIDTo.Text)
        End If

        strJobCreateDateFrom = txtJobCreateDateFrom.Text
        strJobCreateDateTo = txtJobCreateDateTo.Text

        strDateInFrom = txtDateInFrom.Text
        strDateInTo = txtDateInTo.Text

        If txtBillPartyCode.Text = "" Then
            strBillPartyCode = ""
        Else
            strBillPartyCode = Trim(txtBillPartyCode.Text)
        End If

        strTypeOfVeh = Trim(lstTypeOfVeh.SelectedItem.Value)

        If txtVehRegNo.Text = "" Then
            strVehRegNo = ""
        Else
            strVehRegNo = Trim(txtVehRegNo.Text)
        End If

        If txtVehExpCode.Text = "" Then
            strVehExpCode = ""
        Else
            strVehExpCode = Trim(txtVehExpCode.Text)
        End If

        If txtWorkCode.Text = "" Then
            strWorkCode = ""
        Else
            strWorkCode = Trim(txtWorkCode.Text)
        End If

        If txtServTypeCode.Text = "" Then
            strServTypeCode = ""
        Else
            strServTypeCode = Trim(txtServTypeCode.Text)
        End If

        If txtMechIDFrom.Text = "" Then
            strMechIDFrom = ""
        Else
            strMechIDFrom = Trim(txtMechIDFrom.Text)
        End If

        If txtMechIDTo.Text = "" Then
            strMechIDTo = ""
        Else
            strMechIDTo = Trim(txtMechIDTo.Text)
        End If

        strStatus = Trim(lstStatus.SelectedItem.Value)

        strBillPartyCode = Server.UrlEncode(strBillPartyCode)
        strVehRegNo = Server.UrlEncode(strVehRegNo)
        strVehExpCode = Server.UrlEncode(strVehExpCode)
        strWorkCode = Server.UrlEncode(strWorkCode)
        strServTypeCode = Server.UrlEncode(strServTypeCode)

        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"
        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=WS_STDRPT_COMPLETEJOB_GET_CONFIG_DATE&errmesg=" & lblErrMessage.Text & "&redirect=../../en/reports/WS_StdRpt_Selection.aspx")
        End Try

        strDateSetting = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))

        If Not (strJobCreateDateFrom = "" And strJobCreateDateTo = "") Or Not (strDateInFrom = "" And strDateInTo = "") Then
            If objGlobal.mtdValidInputDate(strDateSetting, strJobCreateDateFrom, objDateFormat, objDateFrom) = True And objGlobal.mtdValidInputDate(strDateSetting, strJobCreateDateTo, objDateFormat, objDateTo) = True Or objGlobal.mtdValidInputDate(strDateSetting, strDateInFrom, objDateFormat, strDateInTo) = True And objGlobal.mtdValidInputDate(strDateSetting, strDateInTo, objDateFormat, objDateInTo) = True Then
                Response.Write("<Script Language=""JavaScript"">window.open(""WS_StdRpt_VehBreakdownListPreview.aspx?CompName=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & strDec & _
                               "&DDLAccMth=" & strddlAccMth & "&DDLAccYr=" & strddlAccYr & "&lblVehicle=" & lblVeh.Text & "&lblCompany=" & lblCompany.Text & "&lblBillPartyCode=" & lblBillPartyCode.Text & "&lblTypeOfVeh=" & lblTypeOfVeh.Text & _
                               "&lblVehRegNo=" & lblVehRegNo.Text & "&lblVehExpCode=" & lblVehExpCode.Text & "&lblWorkCode=" & lblWorkCode.Text & "&lblServTypeCode=" & lblServTypeCode.Text & "&lblLocation=" & lblLocation.Text & "&lblVeh=" & lblVeh.Text & _
                               "&JobIDFrom=" & strJobIDFrom & "&JobIDTo=" & strJobIDTo & "&JobCreateDateFrom=" & objDateFrom & "&JobCreateDateTo=" & objDateTo & "&DateInFrom=" & strDateInFrom & "&DateInTo=" & strDateInTo & "&BillPartyCode=" & strBillPartyCode & _
                               "&TypeOfVeh=" & strTypeOfVeh & "&VehRegNo=" & strVehRegNo & "&VehExpCode=" & strVehExpCode & "&WorkCode=" & strWorkCode & "&ServTypeCode=" & strServTypeCode & "&MechIDFrom=" & strMechIDFrom & "&MechIDTo=" & strMechIDTo & _
                               "&Status=" & strStatus & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
            Else
                lblDateFormat.Text = objDateFormat & "."
                lblDate.Visible = True
                lblDateFormat.Visible = True
            End If
        Else
            Response.Write("<Script Language=""JavaScript"">window.open(""WS_StdRpt_VehBreakdownListPreview.aspx?CompName=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & strDec & _
                           "&DDLAccMth=" & strddlAccMth & "&DDLAccYr=" & strddlAccYr & "&lblVehicle=" & lblVeh.Text & "&lblCompany=" & lblCompany.Text & "&lblBillPartyCode=" & lblBillPartyCode.Text & "&lblTypeOfVeh=" & lblTypeOfVeh.Text & _
                           "&lblVehRegNo=" & lblVehRegNo.Text & "&lblVehExpCode=" & lblVehExpCode.Text & "&lblWorkCode=" & lblWorkCode.Text & "&lblServTypeCode=" & lblServTypeCode.Text & "&lblLocation=" & lblLocation.Text & "&lblVeh=" & lblVeh.Text & _
                           "&JobIDFrom=" & strJobIDFrom & "&JobIDTo=" & strJobIDTo & "&JobCreateDateFrom=" & objDateFrom & "&JobCreateDateTo=" & objDateTo & "&DateInFrom=" & strDateInFrom & "&DateInTo=" & strDateInTo & "&BillPartyCode=" & strBillPartyCode & _
                           "&TypeOfVeh=" & strTypeOfVeh & "&VehRegNo=" & strVehRegNo & "&VehExpCode=" & strVehExpCode & "&WorkCode=" & strWorkCode & "&ServTypeCode=" & strServTypeCode & "&MechIDFrom=" & strMechIDFrom & "&MechIDTo=" & strMechIDTo & _
                           "&Status=" & strStatus & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
        End If
    End Sub
End Class
