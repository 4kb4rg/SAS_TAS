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

Public Class WS_StdRpt_CompletedJobList : Inherits Page

    Protected RptSelect As UserControl

    Dim objWS As New agri.WS.clsReport()
    Dim objWSTrx As New agri.WS.clsTrx()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Protected WithEvents lblDate As Label
    Protected WithEvents lblDateFormat As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label

    Protected WithEvents lblCompany As Label
    Protected WithEvents lblVehicle As Label
    Protected WithEvents lblTypeOfVeh As Label
    Protected WithEvents lblVehRegNo As Label
    Protected WithEvents lblBillPartyCode As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents lblServTypeCode As Label

    Protected WithEvents txtJobIDFrom As TextBox
    Protected WithEvents txtJobIDTo As TextBox
    Protected WithEvents lstTypeOfVeh As DropDownList
    Protected WithEvents txtVehRegNo As TextBox
    Protected WithEvents txtBillPartyCode As TextBox
    Protected WithEvents lstPrintStatus As DropDownList

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
                BindPrintStatus()
                BindVehType()
            End If

        End If
    End Sub


    Sub onload_GetLangCap()
        GetEntireLangCap()

        lblServTypeCode.Text = GetCaption(objLangCap.EnumLangCap.ServType) & " Code :"
        lblVehicle.Text = GetCaption(objLangCap.EnumLangCap.Vehicle)
        lblCompany.Text = GetCaption(objLangCap.EnumLangCap.Company)
        lblBillPartyCode.Text = GetCaption(objLangCap.EnumLangCap.BillParty) & " Code :"
        lblTypeOfVeh.Text = "Type of " & GetCaption(objLangCap.EnumLangCap.Vehicle) & " :"
        lblVehRegNo.Text = GetCaption(objLangCap.EnumLangCap.Vehicle) & " Code/Reg No :"
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

    Sub BindPrintStatus()
        lstPrintStatus.Items.Add(New ListItem("All", "A"))
        lstPrintStatus.Items.Add(New ListItem("Printed", "P"))
        lstPrintStatus.Items.Add(New ListItem("Not Printed", "N"))

        lstPrintStatus.SelectedIndex = 2

    End Sub

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strJobIDFrom As String
        Dim strJobIDTo As String
        Dim strTypeOfVeh As String
        Dim strVehRegNo As String
        Dim strBillPartyCode As String
        Dim strPrintStatus As String

        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String

        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden

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

        strTypeOfVeh = Trim(lstTypeOfVeh.SelectedItem.Value)

        If txtVehRegNo.Text = "" Then
            strVehRegNo = ""
        Else
            strVehRegNo = Trim(txtVehRegNo.Text)
        End If

        If txtBillPartyCode.Text = "" Then
            strBillPartyCode = ""
        Else
            strBillPartyCode = Trim(txtBillPartyCode.Text)
        End If

        strPrintStatus = Trim(lstPrintStatus.SelectedItem.Value)

        strVehRegNo = Server.UrlEncode(strVehRegNo)
        strBillPartyCode = Server.UrlEncode(strBillPartyCode)

        Response.Write("<Script Language=""JavaScript"">window.open(""WS_StdRpt_CompletedJobListPreview.aspx?CompName=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & _
                       "&RptName=" & strRptName & "&Decimal=" & strDec & "&lblVehicle=" & lblVehicle.Text & "&lblCompany=" & lblCompany.Text & "&lblBillPartyCode=" & lblBillPartyCode.Text & _
                       "&lblTypeOfVeh=" & lblTypeOfVeh.Text & "&lblVehRegNo=" & lblVehRegNo.Text & "&lblServTypeCode=" & lblServTypeCode.Text & "&lblLocation=" & lblLocation.Text & _
                       "&JobIDFrom=" & strJobIDFrom & "&JobIDTo=" & strJobIDTo & "&TypeOfVeh=" & strTypeOfVeh & "&VehRegNo=" & strVehRegNo & "&BillPartyCode=" & strBillPartyCode & _
                       "&PrintStatus=" & strPrintStatus & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub
End Class
