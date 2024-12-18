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

Public Class AR_StdRpt_DebitNoteList : Inherits Page

    Protected RptSelect As UserControl
    Dim objARRpt As New agri.BI.clsReport()
    Dim objARTrx As New agri.BI.clsTrx()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminShare As New agri.Admin.clsShare()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblBillParty As Label
    Protected WithEvents lblCOA As Label
    Protected WithEvents lblBlkType As Label
    Protected WithEvents lblBlkGrp As Label
    Protected WithEvents lblBlkCode As Label
    Protected WithEvents lblSubBlkCode As Label
    Protected WithEvents lblVehicle As Label
    Protected WithEvents lblVehicleExpCode As Label
    Protected WithEvents lblLocation As Label

    Protected WithEvents txtInvNoFrom As TextBox    
    Protected WithEvents txtInvNoTo As TextBox
    Protected WithEvents txtBillParty As TextBox
    Protected WithEvents txtCustomerRef As TextBox
    Protected WithEvents txtDeliveryRef As TextBox
    Protected WithEvents txtCOA As TextBox
    Protected WithEvents txtBlkGrp As TextBox
    Protected WithEvents txtBlkCode As TextBox
    Protected WithEvents txtSubBlkCode As TextBox
    Protected WithEvents txtVehicle As TextBox
    Protected WithEvents txtVehicleExpCode As TextBox
    Protected WithEvents txtItemDesc As TextBox

    Protected WithEvents ddlDebitNoteType As DropDownList
    Protected WithEvents ddlStatus As DropDownList 
    Protected WithEvents lstBlkType As DropDownList

    Protected WithEvents TrBlkGrp As HtmlTableRow
    Protected WithEvents TrBlk As HtmlTableRow
    Protected WithEvents TrSubBlk As HtmlTableRow

    Protected WithEvents PrintPrev As ImageButton

    Dim objLangCapDs As New Object()
    Dim objTermTypeDs As New Object()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfigsetting As String
    Dim strBlock As String


    Dim dr As DataRow
    Dim intCnt As Integer
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
        intConfigsetting = Session("SS_CONFIGSETTING")
  
        strLocType = Session("SS_LOCTYPE")
        

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
                onload_GetLangCap()
                BindStatus()     
                BindDebitNoteType()
                BlkTypeList()
            End If

            If lstBlkType.SelectedItem.Value = "BlkCode" Then
                TrBlkGrp.Visible = False
                TrBlk.Visible = True
                TrSubBlk.Visible = False
            ElseIf lstBlkType.SelectedItem.Value = "BlkGrp" Then
                TrBlkGrp.Visible = True
                TrBlk.Visible = False
                TrSubBlk.Visible = False
            ElseIf lstBlkType.SelectedItem.Value = "SubBlkCode" Then
                TrBlkGrp.Visible = False
                TrBlk.Visible = False
                TrSubBlk.Visible = True
            End If

        End If
    End Sub

    
    Sub BindDebitNoteType()

        ddlDebitNoteType.Items.Add(New ListItem("All",0))
        ddlDebitNoteType.Items.Add(New ListItem(objARTrx.mtdGetDebitNoteDocType(objARTrx.EnumDebitNoteDocType.Auto_CTTransfer), objARTrx.EnumDebitNoteDocType.Auto_CTTransfer))
        ddlDebitNoteType.Items.Add(New ListItem(objARTrx.mtdGetDebitNoteDocType(objARTrx.EnumDebitNoteDocType.Auto_CTIssue_External), objARTrx.EnumDebitNoteDocType.Auto_CTIssue_External))
        ddlDebitNoteType.Items.Add(New ListItem(objARTrx.mtdGetDebitNoteDocType(objARTrx.EnumDebitNoteDocType.Auto_INFuelIssue_External), objARTrx.EnumDebitNoteDocType.Auto_INFuelIssue_External))
        ddlDebitNoteType.Items.Add(New ListItem(objARTrx.mtdGetDebitNoteDocType(objARTrx.EnumDebitNoteDocType.Auto_INIssue_External), objARTrx.EnumDebitNoteDocType.Auto_INIssue_External))
        ddlDebitNoteType.Items.Add(New ListItem(objARTrx.mtdGetDebitNoteDocType(objARTrx.EnumDebitNoteDocType.Auto_WSJob_External), objARTrx.EnumDebitNoteDocType.Auto_WSJob_External))
        ddlDebitNoteType.Items.Add(New ListItem(objARTrx.mtdGetDebitNoteDocType(objARTrx.EnumDebitNoteDocType.Auto_Millware), objARTrx.EnumDebitNoteDocType.Auto_Millware))
        ddlDebitNoteType.Items.Add(New ListItem(objARTrx.mtdGetDebitNoteDocType(objARTrx.EnumDebitNoteDocType.Auto_CTIssue_Staff), objARTrx.EnumDebitNoteDocType.Auto_CTIssue_Staff))
        ddlDebitNoteType.Items.Add(New ListItem(objARTrx.mtdGetDebitNoteDocType(objARTrx.EnumDebitNoteDocType.Auto_INFuelIssue_Staff), objARTrx.EnumDebitNoteDocType.Auto_INFuelIssue_Staff))
        ddlDebitNoteType.Items.Add(New ListItem(objARTrx.mtdGetDebitNoteDocType(objARTrx.EnumDebitNoteDocType.Auto_INIssue_Staff), objARTrx.EnumDebitNoteDocType.Auto_INIssue_Staff))
        ddlDebitNoteType.Items.Add(New ListItem(objARTrx.mtdGetDebitNoteDocType(objARTrx.EnumDebitNoteDocType.Auto_WSJob_Staff), objARTrx.EnumDebitNoteDocType.Auto_WSJob_Staff))
        ddlDebitNoteType.Items.Add(New ListItem(objARTrx.mtdGetDebitNoteDocType(objARTrx.EnumDebitNoteDocType.Auto_INTransfer), objARTrx.EnumDebitNoteDocType.Auto_INTransfer))
        ddlDebitNoteType.Items.Add(New ListItem(objARTrx.mtdGetDebitNoteDocType(objARTrx.EnumDebitNoteDocType.Auto_INIssue_Internal), objARTrx.EnumDebitNoteDocType.Auto_INIssue_Internal)) 
        ddlDebitNoteType.Items.Add(New ListItem(objARTrx.mtdGetDebitNoteDocType(objARTrx.EnumDebitNoteDocType.Auto_INFuelIssue_Internal), objARTrx.EnumDebitNoteDocType.Auto_INFuelIssue_Internal)) 
        ddlDebitNoteType.Items.Add(New ListItem(objARTrx.mtdGetDebitNoteDocType(objARTrx.EnumDebitNoteDocType.Manual), objARTrx.EnumDebitNoteDocType.Manual))
        ddlDebitNoteType.Items.Add(New ListItem(objARTrx.mtdGetDebitNoteDocType(objARTrx.EnumDebitNoteDocType.Manual_Millware), objARTrx.EnumDebitNoteDocType.Manual_Millware))

    End Sub


    Sub BindStatus()

        ddlStatus.Items.Add(New ListItem("All", 0))
        ddlStatus.Items.Add(New ListItem(objARTrx.mtdGetCreditNoteStatus(objARTrx.EnumDebitNoteStatus.Active), objARTrx.EnumDebitNoteStatus.Active))
        ddlStatus.Items.Add(New ListItem(objARTrx.mtdGetCreditNoteStatus(objARTrx.EnumDebitNoteStatus.Confirmed), objARTrx.EnumDebitNoteStatus.Confirmed))
        ddlStatus.Items.Add(New ListItem(objARTrx.mtdGetCreditNoteStatus(objARTrx.EnumDebitNoteStatus.Deleted), objARTrx.EnumDebitNoteStatus.Deleted))
        ddlStatus.Items.Add(New ListItem(objARTrx.mtdGetCreditNoteStatus(objARTrx.EnumDebitNoteStatus.Cancelled), objARTrx.EnumDebitNoteStatus.Cancelled))        
        ddlStatus.Items.Add(New ListItem(objARTrx.mtdGetCreditNoteStatus(objARTrx.EnumDebitNoteStatus.Closed), objARTrx.EnumDebitNoteStatus.Closed))        
        ddlStatus.Items.Add(New ListItem(objARTrx.mtdGetCreditNoteStatus(objARTrx.EnumDebitNoteStatus.WrittenOff), objARTrx.EnumDebitNoteStatus.WrittenOff))        

    End Sub      


    Sub BlkTypeList()

        Dim strBlkGrp As String
        Dim strBlk As String
        Dim strSubBlk As String

        strBlkGrp = Left(lblBlkGrp.Text, Len(lblBlkGrp.Text) - 2)
        strBlk = Left(lblBlkCode.Text, Len(lblBlkCode.Text) - 2)
        strSubBlk = Left(lblSubBlkCode.Text, Len(lblSubBlkCode.Text) - 2)

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
            lstBlkType.Items.Add(New ListItem(strBlk, "BlkCode"))
            lstBlkType.Items.Add(New ListItem(strBlkGrp, "BlkGrp"))
        Else
            lstBlkType.Items.Add(New ListItem(strSubBlk, "SubBlkCode"))
            lstBlkType.Items.Add(New ListItem(strBlk, "BlkCode"))
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
        lblVehicleExpCode.Text  = GetCaption(objLangCap.EnumLangCap.VehExpense)
        lblVehicle.Text = GetCaption(objLangCap.EnumLangCap.Vehicle)
        lblCOA.Text = GetCaption(objLangCap.EnumLangCap.Account)
        lblBlkType.Text = GetCaption(objLangCap.EnumLangCap.Block)
        lblBlkGrp.Text = GetCaption(objLangCap.EnumLangCap.Block) & " Group :"
        lblBlkCode.Text = GetCaption(objLangCap.EnumLangCap.Block) & " Code :"
        lblSubBlkCode.Text = GetCaption(objLangCap.EnumLangCap.SubBlock) & " Code :"
        lblBillParty.Text =  GetCaption(objLangCap.EnumLangCap.BillParty)

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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=AR_STDRPT_DebitNote_LIST_CLSLANGCAR_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/AR_StdRpt_Selection.aspx")
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
        Dim strSupplier As String
        Dim strStmtType As String

        Dim strddlAccMth As String
        Dim strddlAccYr As String
        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String
        Dim strDateFrom As String
        Dim strDateTo As String
        Dim strDateSetting As String

        Dim strInvNoFrom As String
        Dim strInvNoTo As String
        Dim strlblBillParty As String
        Dim strBillParty As String
        Dim strDebitNoteType As String
        Dim strCustomerRef As String
        Dim strDeliveryRef As String
        Dim strlblCOA As String
        Dim strCOA As String
        Dim strlblBlkType As String
        Dim strBlkType As String
        Dim strlblBlkGrp As String
        Dim strBlkGrp As String
        Dim strlblBlkCode As String
        Dim strBlkCode As String
        Dim strlblSubBlkCode As String
        Dim strSubBlkCode As String
        Dim strlblVehicle As String
        Dim strVehicle As String
        Dim strlblVehicleExpCode As String
        Dim strVehicleExpCode As String
        Dim strItemDesc As String
        Dim strStatus As String
        Dim strlblLocation As String

        Dim tempAccMth As DropDownList
        Dim tempAccYr As DropDownList
        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden

        Dim strParam As String
        Dim objSysCfgDs As New Object()
        Dim objDateFormat As New Object()
        Dim objDateFrom As String
        Dim objDateTo As String



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

        strInvNoFrom = Trim(txtInvNoFrom.Text)
        strInvNoTo = Trim(txtInvNoTo.Text)
        strlblBillParty = Trim(lblBillParty.Text)
        strBillParty = Trim(txtBillParty.Text)
        strDebitNoteType = Trim(ddlDebitNoteType.SelectedItem.value)
        strCustomerRef = Trim(txtCustomerRef.Text)
        strDeliveryRef = Trim(txtDeliveryRef.Text)
        strlblCOA = Trim(lblCOA.Text)
        strCOA = Trim(txtCOA.Text)
        strlblBlkType = Trim(lblBlkType.Text)
        strBlkType = Trim(lstBlkType.SelectedItem.value)
        strlblBlkGrp = Trim(lblBlkGrp.Text)
        strBlkGrp = Trim(txtBlkGrp.Text)
        strlblBlkCode = Trim(lblBlkCode.Text)
        strBlkCode = Trim(txtBlkCode.Text)
        strlblSubBlkCode = Trim(lblSubBlkCode.Text)
        strSubBlkCode = Trim(txtSubBlkCode.Text)
        strlblVehicle = Trim(lblVehicle.Text)
        strVehicle = Trim(txtVehicle.Text)
        strlblVehicleExpCode = Trim(lblVehicleExpCode.Text)
        strVehicleExpCode = Trim(txtVehicleExpCode.Text)
        strItemDesc = Trim(txtItemDesc.Text)
        strStatus = Trim(ddlStatus.SelectedItem.value)
        strlblLocation = Trim(lblLocation.Text)

        strInvNoFrom = Server.UrlEncode(strInvNoFrom)
        strInvNoTo = Server.UrlEncode(strInvNoTo)
        strlblBillParty = Server.UrlEncode(strlblBillParty)
        strBillParty = Server.UrlEncode(strBillParty)
        strDebitNoteType = Server.UrlEncode(strDebitNoteType)
        strCustomerRef = Server.UrlEncode(strCustomerRef)
        strDeliveryRef = Server.UrlEncode(strDeliveryRef)
        strlblCOA = Server.UrlEncode(strlblCOA)
        strCOA = Server.UrlEncode(strCOA)
        strlblBlkType = Server.UrlEncode(strlblBlkType)
        strBlkType = Server.UrlEncode(strBlkType)
        strlblBlkGrp = Server.UrlEncode(strlblBlkGrp)
        strBlkGrp = Server.UrlEncode(strBlkGrp)
        strlblBlkCode = Server.UrlEncode(strlblBlkCode)
        strBlkCode = Server.UrlEncode(strBlkCode)
        strlblSubBlkCode = Server.UrlEncode(strlblSubBlkCode)
        strSubBlkCode = Server.UrlEncode(strSubBlkCode)
        strlblVehicle = Server.UrlEncode(strlblVehicle)
        strVehicle = Server.UrlEncode(strVehicle)
        strlblVehicleExpCode = Server.UrlEncode(strlblVehicleExpCode)
        strVehicleExpCode = Server.UrlEncode(strVehicleExpCode)
        strItemDesc = Server.UrlEncode(strItemDesc)
        strStatus = Server.UrlEncode(strStatus)
        strlblLocation = Server.UrlEncode(strlblLocation)

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

        If txtBlkGrp.visible = true then strBlock = Trim(txtBlkGrp.Text)
        If txtBlkCode.visible = true then strBlock = Trim(txtBlkCode.Text)
        If txtSubBlkCode.visible = true then strBlock = Trim(txtSubBlkCode.Text)

        



                Response.Write("<Script Language=""JavaScript"">window.open(""AR_StdRpt_DebitNoteListPreview.aspx?CompName=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & strDec & _
                            "&DDLAccMth=" & strddlAccMth & "&DDLAccYr=" & strddlAccYr & "&lblLocation=" & lblLocation.Text & _
                            "&InvNoFrom=" & strInvNoFrom & "&InvNoTo=" & strInvNoTo & "&BillParty=" & strBillParty & _
                            "&lblBillParty=" & strlblBillParty & _
                            "&DebitNoteType=" & strDebitNoteType & "&CustomerRef=" & strCustomerRef & "&DeliveryRef=" & strDeliveryRef & _
                            "&lblCOA=" & strlblCOA & "&COA=" & strCOA & "&lblBlkType=" & strlblBlkType & "&BlkType=" & strBlkType & _
                            "&lblBlkGrp=" & strlblBlkGrp & "&BlkGrp=" & strBlkGrp & "&lblBlkCode=" & strlblBlkCode & "&BlkCode=" & strBlkCode  & _
                            "&lblSubBlkCode=" & strlblSubBlkCode & "&SubBlkCode=" & strSubBlkCode & "&lblVehicle=" & strlblVehicle  & _
                            "&Vehicle=" & strVehicle & "&lblVehicleExpCode=" & strlblVehicleExpCode & "&VehicleExpCode=" & strVehicleExpCode & _
                            "&ItemDesc=" & strItemDesc & "&Status=" & strStatus & "&lblLocation=" & strlblLocation & _
                            """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

    End Sub

End Class

