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

Imports agri.Admin.clsCountry

Public Class AR_StdRpt_BillPartyList : Inherits Page

    Protected RptSelect As UserControl

    Dim objAR As New agri.BI.clsReport()
    Dim objARTrx As New agri.BI.clsTrx()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objSysCountry As New agri.Admin.clsCountry()
    Dim objCountryDs As New Object()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objBPDs As New Object()
    Dim objAdminLoc As New agri.Admin.clsLoc()




    Protected WithEvents lblDate As Label
    Protected WithEvents lblDateFormat As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblCOA As Label

    Protected WithEvents lblLocation As Label
    Protected WithEvents lblBillParty As Label
    Protected WithEvents lstStatus As DropDownList
    Protected WithEvents ddlCountry As DropDownList
    Protected WithEvents ddlCreditTermType As DropDownList

    Protected WithEvents PrintPrev As ImageButton
    Protected WithEvents txtBillPartyCode as TextBox
    Protected WithEvents txtName as TextBox
    Protected WithEvents txtContactPerson as TextBox
    Protected WithEvents txtTown as TextBox
    Protected WithEvents txtState as TextBox
    Protected WithEvents txtPostCode as TextBox
    Protected WithEvents txtTelNo as TextBox
    Protected WithEvents txtFaxNo as TextBox
    Protected WithEvents txtEmail as TextBox
    Protected WithEvents txtCreditTerm as TextBox
    Protected WithEvents txtCreditLimit as TextBox
    Protected WithEvents txtCOA as TextBox


    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String

    Dim intErrNo As Integer
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_ARACCMONTH")
        strAccYear = Session("SS_ARACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        strLocType = Session("SS_LOCTYPE")

        lblDate.Visible = False
        lblDateFormat.Visible = False

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else

            If Not Page.IsPostBack Then
                onload_GetLangCap()
                BindStatusList()
                BindCountryList()
            End If
        End If
    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim SDecimal As  HtmlTableRow 
        Dim SLocation As HtmlTableRow

        
        SDecimal  = RptSelect.FindControl("SelDecimal")
        SLocation  = RptSelect.FindControl("SelLocation")
        
        SDecimal.visible = false
        SLocation.visible = false        

    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblLocation.Text = GetCaption(objLangCap.EnumLangCap.Location)
        lblCOA.Text = GetCaption(objLangCap.EnumLangCap.Account) & " Code"
        lblBillParty.Text = GetCaption(objLangCap.EnumLangCap.BillParty) & " Code"
        
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
                    If strLocType = objAdminLoc.EnumLocType.Mill then
                        Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                    else
                        Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                    end if
                    Exit For
                End If
            Next
        End Function


    Sub BindCountryList()

        Dim strOpCd_Country As String = "ADMIN_CLSCOUNTRY_COUNTRY_LIST_GET"
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intCountryIndex As Integer = 0

            Try
                intErrNo = objSysCountry.mtdGetCountryList(strOpCd_Country, objCountryDs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=AR_Bill_Party_List_Country_Get&errmesg=" & lblErrMessage.Text)
            End Try

            If objCountryDs.Tables(0).Rows.Count > 0 Then
                For intCnt = 0 To objCountryDs.Tables(0).Rows.Count - 1
                    objCountryDs.Tables(0).Rows(intCnt).Item(0) = Trim(objCountryDs.Tables(0).Rows(intCnt).Item(0))
                    objCountryDs.Tables(0).Rows(intCnt).Item(1) = objCountryDs.Tables(0).Rows(intCnt).Item(0) & " (" & Trim(objCountryDs.Tables(0).Rows(intCnt).Item(1)) & ")"
                Next intCnt
            End If

            Dim dr As DataRow
            dr = objCountryDs.Tables(0).NewRow()
            dr("CountryCode") = ""
            dr("CountryDesc") = "All"
            objCountryDs.Tables(0).Rows.InsertAt(dr, 0)

            ddlCountry.DataSource = objCountryDs.Tables(0)
            ddlCountry.DataTextField = "CountryDesc"
            ddlCountry.DataValueField = "CountryCode"
            ddlCountry.DataBind()
            ddlCountry.SelectedIndex = intCountryIndex


    End Sub


    Sub BindStatusList()
        Dim strText = "All"

        lstStatus.Items.Add(New ListItem(strText))                
        lstStatus.Items.Add(New ListItem(objGLSetup.mtdGetBillPartyStatus(objGLSetup.EnumBillPartyStatus.Active), objGLSetup.EnumBillPartyStatus.Active))
        lstStatus.Items.Add(New ListItem(objGLSetup.mtdGetBillPartyStatus(objGLSetup.EnumBillPartyStatus.Deleted), objGLSetup.EnumBillPartyStatus.Deleted))

    End Sub


    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strPRNoFrom As String
        Dim strPRNoTo As String
        Dim strPRType As String
        Dim strStatus As String

        Dim strRptID As String
        Dim strRptName As String
        Dim strName As String
        Dim strUserLoc As String
        Dim strDec As String
        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden       

        Dim strParam As String
        Dim strDateSetting


        Dim strBillPartyCode As String
        Dim strContactPerson As String
        Dim strTown As String
        Dim strState As String
        Dim strPostCode As String
        Dim strCountry As String
        Dim strTelNo As String
        Dim strFaxNo As String
        Dim strEmail As String
        Dim strCreditTerm As String
        Dim strCreditTermType As String
        Dim strCreditLimit As String
        Dim strCOA As String
        Dim strLblCOA As String
        Dim strLocation As String 
        Dim strLblBillParty As String

        Dim objSysCfgDs As New Object()
        Dim objDateFormat As New Object()
        Dim objDateFrom As String
        Dim objDateTo As String
                
        tempRpt = RptSelect.FindControl("lstRptName")
        strRptID = Trim(tempRpt.SelectedItem.Value)
        strRptName = Trim(tempRpt.SelectedItem.Text)
        
        strBillPartyCode = Trim(txtBillPartyCode.Text)
        strName = Trim(txtName.Text)
        strContactPerson = Trim(txtContactPerson.Text)
        strTown = Trim(txtTown.Text)
        strState = Trim(txtState.Text)
        strPostCode = Trim(txtPostCode.Text)
        strCountry = Trim(ddlCountry.SelectedItem.value)
        strTelNo = Trim(txtTelNo.Text)
        strFaxNo = Trim(txtFaxNo.Text)
        strEmail = Trim(txtEmail.Text)
        strCreditTerm = Trim(txtCreditTerm.Text)
        strCreditTermType = Trim(ddlCreditTermType.SelectedItem.value)
        strCreditLimit = Trim(txtCreditLimit.Text)
        strCOA = Trim(txtCOA.Text)
        strLblCOA = Trim(lblCOA.Text)
        strStatus = Trim(lstStatus.SelectedItem.Text)   
        strLocation = Trim(LblLocation.Text)
        strLblBillParty = Trim(lblBillParty.Text)

        strBillPartyCode = Server.UrlEncode(strBillPartyCode)
        strName = Server.UrlEncode(strName)
        strContactPerson = Server.UrlEncode(strContactPerson)
        strTown = Server.UrlEncode(strTown)
        strState = Server.UrlEncode(strState)
        strPostCode = Server.UrlEncode(strPostCode)
        strCountry = Server.UrlEncode(strCountry)
        strTelNo = Server.UrlEncode(strTelNo)
        strFaxNo = Server.UrlEncode(strFaxNo)
        strEmail = Server.UrlEncode(strEmail)
        strCreditTerm = Server.UrlEncode(strCreditTerm)
        strCreditTermType = Server.UrlEncode(strCreditTermType)
        strCreditLimit = Server.UrlEncode(strCreditLimit)
        strCOA = Server.UrlEncode(strCOA)
        strLblCOA = Server.UrlEncode(strLblCOA)
        strStatus = Server.UrlEncode(strStatus)   
        strLocation = Server.UrlEncode(strLocation)



        








           Response.Write("<Script Language=""JavaScript"">window.open(""AR_StdRpt_BillPartyListPreview.aspx?Type=Print&location=" & strUserloc & "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & "" & _
                           "&BillPartyCode=" & strBillPartyCode & "&Name=" & strName & "&ContactPerson=" & strContactPerson  & "&Town=" & strTown & _
                           "&State=" & strState & "&PostCode=" & strPostCode & "&Country=" & strCountry & "&TelNo=" & strTelNo & _
                           "&FaxNo=" & strFaxNo & "&Email=" & strEmail & "&CreditTerm=" & strCreditTerm & "&CreditTermType=" & strCreditTermType & _
                           "&CreditLimit=" & strCreditLimit & "&lblCOA=" & strlblCOA & "&COA=" & strCOA & "&LblLocation=" & strLocation  & _
                           "&lblBillParty=" & strLblBillParty & _
                           "&Status=" & strStatus & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

End Class
 
