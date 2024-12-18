
Imports System
Imports System.Data
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings


Public Class WM_WeighBridgeTicket_Det : Inherits Page

    Dim dsTicketItem As DataSet

    Protected objWMTrx As New agri.WM.clsTrx()
    Dim objWMSetup As New agri.WM.clsSetup()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objPUSetup As New agri.PU.clsSetup()
    Dim objCMSetup As New agri.CM.clsSetup()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objGLTrx As New agri.GL.ClsTrx()

    Protected WithEvents hidtranstype As HtmlInputHidden
    Protected WithEvents hidNetWgt As HtmlInputHidden
    Protected WithEvents hidCustNetWgt As HtmlInputHidden
    Protected WithEvents hidTicketNo As HtmlInputHidden
    Protected WithEvents hidMatchingID As HtmlInputHidden
    Protected WithEvents hidFirstWgt As HtmlInputHidden
    Protected WithEvents hidSecondWgt As HtmlInputHidden
    Protected WithEvents hidCalNetWgt As HtmlInputHidden
    Protected WithEvents hidCustFirstWgt As HtmlInputHidden
    Protected WithEvents hidCustSecondWgt As HtmlInputHidden
    Protected WithEvents hidCalCustNetWgt As HtmlInputHidden
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents lblDupMsg As Label
    Protected WithEvents lblErrFirstWgt As Label
    Protected WithEvents lblErrSecWgt As Label
    Protected WithEvents lblErrTimeIn As Label
    Protected WithEvents lblErrTimeOut As Label
    Protected WithEvents lblErrCustFirstWgt As Label
    Protected WithEvents lblErrCustFirstWgtZero As Label
    Protected WithEvents lblErrCustSecWgt As Label
    Protected WithEvents lblErrCustSecWgtZero As Label
    Protected WithEvents lblErrDateIn As Label
    Protected WithEvents lblErrDateInMsg As Label
    Protected WithEvents lblErrDateOut As Label
    Protected WithEvents lblErrDateOutBlank As Label
    Protected WithEvents lblErrDateOutMsg As Label
    Protected WithEvents lblErrDateRcv As Label
    Protected WithEvents lblErrDateRcvBlank As Label
    Protected WithEvents lblErrDateRcvMsg As Label
    Protected WithEvents lblErrPlantYr As Label
    Protected WithEvents lblErrNetWgt As Label
    Protected WithEvents lblErrCustNetWgt As Label
    Protected WithEvents lblEmpErr As Label
    Protected WithEvents lblPeriod As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblCreateDate As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdateBy As Label
    Protected WithEvents lblCustomerTag As Label
    Protected WithEvents lblCustDocRefNoTag As Label
    Protected WithEvents lblBillPartyTag As Label
    Protected WithEvents lblVehicleTag As Label
    Protected WithEvents lblBlockTag As Label
    Protected WithEvents lblDestinationErr As Label

    Protected WithEvents lblTicketNo As Label
    Protected WithEvents rblTransType As RadioButtonList
    Protected WithEvents lstProduct As DropDownList
    Protected WithEvents lstDestination As DropDownList
    Protected WithEvents lstCustomer As DropDownList
    Protected WithEvents txtCustDocRefNo As TextBox

    Protected WithEvents lstDeliveryOrder As DropDownList

    Protected WithEvents txtPL3No As TextBox
    Protected WithEvents lstTransporter As DropDownList

    Protected WithEvents txtVehicle As TextBox
    Protected WithEvents txtDriverName As TextBox
    Protected WithEvents txtDriverICNo As TextBox
    Protected WithEvents txtPlantingYear As TextBox
    Protected WithEvents txtBlock As TextBox
    Protected WithEvents txtDateIn As TextBox
    Protected WithEvents txtInHour As TextBox
    Protected WithEvents txtInMinute As TextBox
    Protected WithEvents lstInAMPM As DropDownList
    Protected WithEvents txtFirstWgt As TextBox
    Protected WithEvents txtDateOut As TextBox
    Protected WithEvents txtOutHour As TextBox
    Protected WithEvents txtOutMinute As TextBox
    Protected WithEvents lstOutAMPM As DropDownList
    Protected WithEvents txtSecondWgt As TextBox
    Protected WithEvents txtDateRcv As TextBox
    Protected WithEvents txtCustFirstWgt As TextBox
    Protected WithEvents txtCustSecondWgt As TextBox
    Protected WithEvents txtRemarks As TextBox
    Protected WithEvents lblCustomerErr As Label
    Protected WithEvents lstEmpCode As DropDownList
    Protected WithEvents validateEmp As RequiredFieldValidator
    Protected WithEvents lstPurchType As DropDownList

    Protected WithEvents btnSelInDate As Image
    Protected WithEvents btnSelOutDate As Image
    Protected WithEvents btnSelRcvDate As Image
    Protected WithEvents FindEmp As HtmlInputButton    

    Protected WithEvents btnCurrInDtTime As ImageButton
    Protected WithEvents btnCurrOutDtTime As ImageButton

    Protected WithEvents btnSave As ImageButton
    Protected WithEvents btnDelete As ImageButton
    Protected WithEvents btnUnDelete As ImageButton
    
    Protected WithEvents lstOrigin As DropDownList
    Protected WithEvents lblOriginErr As Label

    Dim strOppCd_WeighBridge_Ticket_GET As String = "WM_CLSTRX_WEIGHBRIDGE_TICKET_GET"
    Dim strOppCd_WeighBridge_Ticket_ADD As String = "WM_CLSTRX_WEIGHBRIDGE_TICKET_ADD"
    Dim strOppCd_WeighBridge_Ticket_UPD As String = "WM_CLSTRX_WEIGHBRIDGE_TICKET_UPD"
    Dim blnDupKey As Boolean = False

    Dim strParam As String
    Dim objDataSet As New DataSet()
    Dim objLangCapDs As New Object()
    Dim intErrNo As Integer

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intWMAR As Integer
    Protected WithEvents lblTracker As System.Web.UI.WebControls.Label
    Protected WithEvents revDelNoteNo As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents validateTrans As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents validateDateIn As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents ChcktxtInHour As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents txtInHourRange As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents ChcktxtInMinute As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents txtInMinuteRange As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents validateTimeInHr As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents validateTimeInMin As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents revFirstWgt As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents ChcktxtOutHour As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents txtOutHourRange As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents ChcktxtOutMinute As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents txtOutMinuteRange As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents revSecondWgt As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents revCustFirstWgt As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents revCustSecondWgt As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents btnBack As System.Web.UI.WebControls.ImageButton
    Protected WithEvents rsl As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents btnPrint As System.Web.UI.WebControls.ImageButton
    Dim strDateFormat As String
    Dim strLocType As String
    Dim strAcceptFormat As String
    Dim intLevel As Integer
    Dim intLocLevel As Integer
    Dim strParamName As String
    Dim strParamValue As String


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PMACCMONTH")
        strAccYear = Session("SS_PMACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intWMAR = Session("SS_WMAR")
        strDateFormat = Session("SS_DATEFMT")
        strLocType = Session("SS_LOCTYPE")
        intLevel = Session("SS_USRLEVEL")
        intLocLevel = Session("SS_LOCLEVEL")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWMAccessRights.WMWeighBridgeTicket), intWMAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            lblErrDateIn.Visible = False
            lblErrDateOut.Visible = False
            lblErrDateOutBlank.Visible = False
            lblErrDateRcv.Visible = False
            lblErrDateRcvBlank.Visible = False
            lblErrFirstWgt.Visible = False
            lblErrSecWgt.Visible = False
            lblErrTimeIn.Visible = False
            lblErrTimeOut.Visible = False
            lblErrCustFirstWgt.Visible = False
            lblErrCustFirstWgtZero.Visible = False
            lblErrCustSecWgt.Visible = False
            lblErrCustSecWgtZero.Visible = False
            lblErrPlantYr.Visible = False
            lblErrNetWgt.Visible = False
            lblErrCustNetWgt.Visible = False
            lblEmpErr.Visible = False
            lblCustomerErr.Visible = False
            lblDestinationErr.Visible = False
           

            If Not Page.IsPostBack Then
                If Not Request.QueryString("TicketNo") = "" Then
                    hidTicketNo.value = Request.QueryString("TicketNo")
                End If

                If Not hidTicketNo.Value = "" Then
                    BindTransType()
                    BindPurchType("")
                    BindTransporter("")
                    BindDeliveryOrder(strLocation,"")
                    DisplayData()
                    blnUpdate.Text = True
                    
                Else
                    BindTransType()
                    BindPurchType(objPUSetup.EnumSupplierType.FFBSupplier)
                    BindProduct("1")
                    BindDestination("")
                    BindCustomer("")
                    BindEmployee()
                    BindTransporter("")
                    BindDeliveryOrder(strLocation, "")
                    BindOrigin("")
                    EnableControl()
                    blnUpdate.Text = False
                    lstEmpCode.Enabled = False
                    FindEmp.Visible = False
                End If
            End If

            If intLocLevel = objAdminLoc.EnumLocLevel.Estate And strLocType = objAdminLoc.EnumLocType.Mill Then
                btnSave.Visible = True
                btnDelete.Visible = True
                btnUnDelete.Visible = True
            Else
                btnSave.Visible = False
                btnDelete.Visible = False
                btnUnDelete.Visible = False
            End If
        End If
    End Sub

    Sub onload_GetLangCap()

        GetEntireLangCap()

        lblBillPartyTag.Text = GetCaption(objLangCap.EnumLangCap.BillParty)
        lblVehicleTag.Text = GetCaption(objLangCap.EnumLangCap.Vehicle)
        lblBlockTag.Text = "Block" 

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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WM_CLSTRX_WEIGHBRIDGE_TICKET_DET_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=WM/trx/WM_trx_WeighBridgeTicketDet.aspx")
        End Try
    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

        For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If strLocType = objAdminLoc.EnumLocType.Mill then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                Else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                End If
                Exit For
            End If
        Next
    End Function


     Sub OnTrans_Change(ByVal Sender As System.Object, ByVal E As System.EventArgs)
        Dim strOpCode As String = "WM_CLSSETUP_TRANSPORTER_GET"
        Dim dsTrans As DataSet
        Dim strParam As String
        Dim strSortExpression As String = "TransporterCode"

        If Sender.SelectedItem.Value = "" Then
            lstEmpCode.Enabled = False
            FindEmp.Visible = False
            lstEmpCode.SelectedIndex = 0
        end if

        strParam = Sender.SelectedItem.Value & "|||||" & strSortExpression & "|"
        Try
            intErrNo = objWMSetup.mtdGetTransporter(strOpCode, strParam, dsTrans)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WM_CLSTRX_WEIGHBRIDGE_TICKET_CHANGESTATUS&errmesg=" & lblErrMessage.Text & "&redirect=WM/trx/WM_trx_WeighBridgeTicketDet.aspx")
        End Try
                
        lstEmpCode.Enabled = False
        FindEmp.Visible = False

        If dsTrans.Tables(0).Rows.Count > 0 Then
             
            If dsTrans.Tables(0).Rows(0).Item("TType") = objWMSetup.EnumTransporterType.Internal Then
                lstEmpCode.Enabled = True
                FindEmp.Visible = True
            Else
                lstEmpCode.SelectedIndex = 0
                lstEmpCode.Enabled = False
                FindEmp.Visible = False
            End If
           
        End if
        
        If Not dsTrans Is Nothing Then  dsTrans = Nothing
       
    End Sub

   Sub BindEmployee(Optional ByVal pv_strEmpCode As String = "")

        Dim strOpCdEmployee_Get As String = "HR_CLSSETUP_EMPLOYEE_LIST_GET"
        Dim dsForDropDown As DataSet
        Dim SelectedIndex As Integer = -1
        Dim intCnt As Integer
        Dim TblAlias As String
        Dim DataTextField As String
        Dim SearchStr As String
        Dim strParam As String
        Dim drinsert As DataRow
        Dim intSelectedIndex As Integer = 0
        Dim objHRTrx As New agri.HR.clsTrx()

        Try
            strParam = "|||" & objHRTrx.EnumEmpStatus.active & "|" & strLocation & "|Mst.EmpCode|ASC" 
            intErrNo = objHRTrx.mtdGetEmployeeList(strOpCdEmployee_Get, strParam, dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ITEMTODROPDOWN&errmesg=" & lblErrMessage.Text & "&redirect=WM/trx/WM_trx_WeighBridgeTicketDet.aspx")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item(0) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0))
            dsForDropDown.Tables(0).Rows(intCnt).Item(1) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0)) & " ( " & _
                                                          Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(1)) & " )"
            If dsForDropDown.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(pv_strEmpCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        drinsert = dsForDropDown.Tables(0).NewRow()
        drinsert(0) = ""
        drinsert(1) = "Select one Employee Code"
        dsForDropDown.Tables(0).Rows.InsertAt(drinsert, 0)

        lstEmpCode.DataSource = dsForDropDown.Tables(0)
        lstEmpCode.DataValueField = "EmpCode"
        lstEmpCode.DataTextField = "EmpName"
        lstEmpCode.DataBind()
        lstEmpCode.SelectedIndex = intSelectedIndex

        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If
        
    End Sub


    Protected Function LoadData() As DataSet
        Dim strSearch As String
        Dim strSrchTicketNo As String

        If Trim(txtDateRcv.Text) = "1/1/1900" Then
            txtDateRcv.Text = ""
        End If

        strSrchTicketNo = IIf(hidTicketNo.Value = "", "", "AND TIC.TicketNo LIKE '" & Trim(hidTicketNo.Value) & "%' ")
        strSearch = strSrchTicketNo

        strSearch = " WHERE " & Mid(Trim(strSearch), 5)
        strSearch = strSearch

        strParamName = "SEARCHSTR"
        strParamValue = strSearch

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOppCd_WeighBridge_Ticket_GET, _
                                                 strParamName, _
                                                 strParamValue, _
                                                 objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GENERATEINV_LOAD&errmesg=" & lblErrMessage.Text & "&redirect=ap/trx/ap_trx_invrcv_wm_list.aspx")
        End Try

        'strParam = "||||||||LocCode, TIC.TicketNo||" & hidTicketNo.Value

        'Try
        '    intErrNo = objWMTrx.mtdGetWeighBridgeTicket(strOppCd_WeighBridge_Ticket_GET, _
        '                                                strLocation, _
        '                                                strUserId, _
        '                                                strAccMonth, _
        '                                                strAccYear, _
        '                                                strParam, _
        '                                                objDataSet)
        'Catch Exp As System.Exception
        '    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=WM_CLSTRX_WEIGHBRIDGE_TICKET_DETAILS_GET&errmesg=" & lblErrMessage.Text & "&redirect=WM/trx/WM_trx_WeighBridgeTicketDet.aspx")
        'End Try

        Return objDataSet

    End Function

    Sub BindTransType()
        rblTransType.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketTransType(objWMTrx.EnumWeighBridgeTicketTransType.Purchase), objWMTrx.EnumWeighBridgeTicketTransType.Purchase))
        rblTransType.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketTransType(objWMTrx.EnumWeighBridgeTicketTransType.Sales), objWMTrx.EnumWeighBridgeTicketTransType.Sales))
        rblTransType.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketTransType(objWMTrx.EnumWeighBridgeTicketTransType.Usage), objWMTrx.EnumWeighBridgeTicketTransType.Usage))
        rblTransType.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketTransType(objWMTrx.EnumWeighBridgeTicketTransType.Others), objWMTrx.EnumWeighBridgeTicketTransType.Others))
        rblTransType.SelectedIndex = 0
    End Sub
    
    Sub BindPurchType(Byval pv_strPurchType As String)
        
        If pv_strPurchType <> "" Then
            If lstPurchType.Items.Count = 0 then
                lstPurchType.Items.Clear
                lstPurchType.Items.Add(New ListItem("Select Purchase Type", ""))
                lstPurchType.Items.Add(New ListItem(objPUSetup.mtdGetSupplierType(objPUSetup.EnumSupplierType.Internal), objPUSetup.EnumSupplierType.Internal))
                lstPurchType.Items.Add(New ListItem(objPUSetup.mtdGetSupplierType(objPUSetup.EnumSupplierType.External), objPUSetup.EnumSupplierType.External))
                lstPurchType.Items.Add(New ListItem(objPUSetup.mtdGetSupplierType(objPUSetup.EnumSupplierType.Associate), objPUSetup.EnumSupplierType.Associate))
                lstPurchType.Items.Add(New ListItem(objPUSetup.mtdGetSupplierType(objPUSetup.EnumSupplierType.Contractor), objPUSetup.EnumSupplierType.Contractor))
                lstPurchType.Items.Add(New ListItem(objPUSetup.mtdGetSupplierType(objPUSetup.EnumSupplierType.FFBSupplier), objPUSetup.EnumSupplierType.FFBSupplier))
            End If
            lstPurchType.SelectedIndex = pv_strPurchType 
        Else
             lstPurchType.Items.Clear
             lstPurchType.Items.Add(New ListItem("Select Purchase Type", ""))
             lstPurchType.Items.Add(New ListItem(objPUSetup.mtdGetSupplierType(objPUSetup.EnumSupplierType.Internal), objPUSetup.EnumSupplierType.Internal))
             lstPurchType.Items.Add(New ListItem(objPUSetup.mtdGetSupplierType(objPUSetup.EnumSupplierType.External), objPUSetup.EnumSupplierType.External))
             lstPurchType.Items.Add(New ListItem(objPUSetup.mtdGetSupplierType(objPUSetup.EnumSupplierType.Associate), objPUSetup.EnumSupplierType.Associate))
            lstPurchType.Items.Add(New ListItem(objPUSetup.mtdGetSupplierType(objPUSetup.EnumSupplierType.Contractor), objPUSetup.EnumSupplierType.Contractor))
            lstPurchType.Items.Add(New ListItem(objPUSetup.mtdGetSupplierType(objPUSetup.EnumSupplierType.FFBSupplier), objPUSetup.EnumSupplierType.FFBSupplier))
        End If

    End Sub

   

    Sub BindProduct(ByVal pv_strProduct As String)
        lstProduct.Items.Clear
        lstProduct.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketProduct(objWMTrx.EnumWeighBridgeTicketProduct.FFB), objWMTrx.EnumWeighBridgeTicketProduct.FFB))
        lstProduct.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketProduct(objWMTrx.EnumWeighBridgeTicketProduct.CPO), objWMTrx.EnumWeighBridgeTicketProduct.CPO))
        lstProduct.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketProduct(objWMTrx.EnumWeighBridgeTicketProduct.PK), objWMTrx.EnumWeighBridgeTicketProduct.PK))
        lstProduct.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketProduct(objWMTrx.EnumWeighBridgeTicketProduct.Others), objWMTrx.EnumWeighBridgeTicketProduct.Others))
        lstProduct.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketProduct(objWMTrx.EnumWeighBridgeTicketProduct.EFB), objWMTrx.EnumWeighBridgeTicketProduct.EFB))
        lstProduct.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketProduct(objWMTrx.EnumWeighBridgeTicketProduct.Shell), objWMTrx.EnumWeighBridgeTicketProduct.Shell))
        lstProduct.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketProduct(objWMTrx.EnumWeighBridgeTicketProduct.AbuJanjang), objWMTrx.EnumWeighBridgeTicketProduct.AbuJanjang))
        lstProduct.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketProduct(objWMTrx.EnumWeighBridgeTicketProduct.MinyakLimbah), objWMTrx.EnumWeighBridgeTicketProduct.MinyakLimbah))

        If pv_strProduct <> "" Then
            lstProduct.SelectedIndex = pv_strProduct - 1
            'If pv_strProduct = objWMTrx.EnumWeighBridgeTicketProduct.FFB Then
            '    lstProduct.SelectedIndex = 1
            'ElseIf pv_strProduct = objWMTrx.EnumWeighBridgeTicketProduct.CPO Then
            '    lstProduct.SelectedIndex = 0
            'ElseIf pv_strProduct = objWMTrx.EnumWeighBridgeTicketProduct.PK Then
            '    lstProduct.SelectedIndex = 2
            'ElseIf pv_strProduct = objWMTrx.EnumWeighBridgeTicketProduct.Others Then
            '    lstProduct.SelectedIndex = 5
            'ElseIf pv_strProduct = objWMTrx.EnumWeighBridgeTicketProduct.EFB Then
            '    lstProduct.SelectedIndex = 3
            'ElseIf pv_strProduct = objWMTrx.EnumWeighBridgeTicketProduct.Shell Then
            '    lstProduct.SelectedIndex = 4
            'ElseIf pv_strProduct = objWMTrx.EnumWeighBridgeTicketProduct.MinyakLimbah Then
            '    lstProduct.SelectedIndex = 6
            'ElseIf pv_strProduct = objWMTrx.EnumWeighBridgeTicketProduct.AbuJanjang Then
            '    lstProduct.SelectedIndex = 7

            'End If
        End If
    End Sub
    
    Sub BindDestination(ByVal pv_strDestination As String)

        If pv_strDestination <> "" Then
            If lstDestination.Items.Count = 0 then
                lstDestination.Items.Clear
                lstDestination.Items.Add(New ListItem("Select Destination", ""))
                lstDestination.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeDestination(objWMTrx.EnumWeighBridgeDestination.Bulking), objWMTrx.EnumWeighBridgeDestination.Bulking))
                lstDestination.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeDestination(objWMTrx.EnumWeighBridgeDestination.BillParty), objWMTrx.EnumWeighBridgeDestination.BillParty))
            End if

            lstDestination.SelectedIndex = pv_strDestination
            If pv_strDestination  = objWMTrx.EnumWeighBridgeDestination.BillParty Then
                lstCustomer.Enabled = True
                txtCustDocRefNo.Enabled = True
            Else
                lstCustomer.Enabled = False
                txtCustDocRefNo.Enabled = False
            End If

        Else
            lstDestination.Items.Clear
            lstDestination.Items.Add(New ListItem("Select Destination", ""))
            lstDestination.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeDestination(objWMTrx.EnumWeighBridgeDestination.Bulking), objWMTrx.EnumWeighBridgeDestination.Bulking))
            lstDestination.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeDestination(objWMTrx.EnumWeighBridgeDestination.BillParty), objWMTrx.EnumWeighBridgeDestination.BillParty))
        End If
    End Sub

    Sub Trans_Type(ByVal Sender As Object, ByVal E As EventArgs)
        BindCustomer("")
    End Sub

    Sub Purchase_Type(ByVal Sender As Object, ByVal E As EventArgs)
        BindSupplierByType(Sender.SelectedItem.Value, "")
    End Sub

    Sub Destination_Change(ByVal Sender As Object, ByVal E As EventArgs)
        BindDestination(Sender.SelectedItem.Value)
    End Sub

    Sub BindDeliveryOrder(ByVal pv_strLocCode As String, Byval pv_strDeliveryOrder As String)
        Dim strOpCd_DeliveryOrder_GET As String = "CM_CLSSETUP_DELIVERYORDER_DROPDOWNLIST_GET"
        Dim dsDeliveryOrder As New DataSet()
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer

        strParam =  pv_strLocCode '"||" & objCMSetup.EnumDOREGStatus.Active & "|||TransporterCode|"


        Try
            intErrNo = objCMSetup.mtdGetDeliveryOrderDropDownList(strOpCd_DeliveryOrder_GET, strParam, dsDeliveryOrder)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=CM_CLSSETUP_DELIVERYORDER_DROPDOWNLIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=WM/trx/WM_trx_WeighBridgeTicketDet.aspx")
        End Try

        For intCnt = 0 To dsDeliveryOrder.Tables(0).Rows.Count - 1
            dsDeliveryOrder.Tables(0).Rows(intCnt).Item(0) = Trim(dsDeliveryOrder.Tables(0).Rows(intCnt).Item(0))

            If dsDeliveryOrder.Tables(0).Rows(intCnt).Item("DoNo") = Trim(pv_strDeliveryOrder) Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        dr = dsDeliveryOrder.Tables(0).NewRow()
        dr("DoNo") = "Select Delivery Order"
        dsDeliveryOrder.Tables(0).Rows.InsertAt(dr, 0)

        
         If intSelectedIndex = 0 And Trim(pv_strDeliveryOrder) <> "" Then
                dr = dsDeliveryOrder.Tables(0).NewRow()
                dr("DoNo") = pv_strDeliveryOrder & " (Deleted)"
                intSelectedIndex = dsDeliveryOrder.Tables(0).Rows.Count
                dsDeliveryOrder.Tables(0).Rows.InsertAt(dr, intSelectedIndex)
         End If


        lstDeliveryOrder.DataSource = dsDeliveryOrder.Tables(0)
        lstDeliveryOrder.DataValueField = "DoNo"
        lstDeliveryOrder.DataTextField = "DoNo"
        lstDeliveryOrder.DataBind()

        lstDeliveryOrder.SelectedIndex = intSelectedIndex

        If Not dsDeliveryOrder Is Nothing Then
            dsDeliveryOrder = Nothing
        End If

    End Sub    

    Sub BindSupplierByType(ByVal pv_strSuppType As String, Byval pv_strCustomer As String)
        
        Dim strOppCd_Supplier_GET As String = "PU_CLSSETUP_SUPPLIER_GET"
        Dim dsCustomer As New DataSet()
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intCustIndex As Integer

        IF pv_strSuppType <> "" Then
            
            lstCustomer.Enabled = True

            strParam = "||" & objPUSetup.EnumSuppStatus.Active & "||SupplierCode||SELECT"
            strParam = strParam & "|" & IIf(Session("SS_COACENTRALIZED") = "1", "", " A.AccCode in (SELECT AccCode FROM GL_Account WHERE LocCode='" & Trim(strLocation) & "') ")
            strParam = strParam & "|" & pv_strSuppType

            'strParam = "||" & objPUSetup.EnumSuppStatus.Active & "||SupplierCode|||" & pv_strSuppType
            
            Try
                intErrNo = objPUSetup.mtdGetSupplier(strOppCd_Supplier_GET, strParam, dsCustomer)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=WM_CLSTRX_WEIGHBRIDGE_TICKET_SUPP_DROPDOWNLIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=WM/trx/WM_trx_WeighBridgeTicketDet.aspx")
            End Try

            If dsCustomer.Tables(0).Rows.Count > 0 Then
                For intCnt = 0 To dsCustomer.Tables(0).Rows.Count - 1
                    dsCustomer.Tables(0).Rows(intCnt).Item("SupplierCode") = Trim(dsCustomer.Tables(0).Rows(intCnt).Item("SupplierCode"))
                    dsCustomer.Tables(0).Rows(intCnt).Item("Name") = Trim(dsCustomer.Tables(0).Rows(intCnt).Item("SupplierCode")) & " (" & Trim(dsCustomer.Tables(0).Rows(intCnt).Item("Name")) & ")"

                    If dsCustomer.Tables(0).Rows(intCnt).Item("SupplierCode") = pv_strCustomer Then
                        intCustIndex = intCnt + 1
                    End If
                Next intCnt
            End If

            dr = dsCustomer.Tables(0).NewRow()
            dr("SupplierCode") = ""
            dr("Name") = "Select Supplier"
            dsCustomer.Tables(0).Rows.InsertAt(dr, 0)

            lstCustomer.DataSource = dsCustomer.Tables(0)
            lstCustomer.DataValueField = "SupplierCode"
            lstCustomer.DataTextField = "Name"
            lstCustomer.DataBind()
            lstCustomer.SelectedIndex = intCustIndex


            If Not dsCustomer Is Nothing Then dsCustomer = Nothing
        
        Else
             lstCustomer.Enabled = False
             lstCustomer.SelectedIndex = 0        

        End If
        
    End Sub

    Sub BindCustomer(ByVal pv_strCustomer As String)
        Dim strOppCd_Supplier_GET As String = "PU_CLSSETUP_SUPPLIER_GET"
        Dim strOppCd_BillParty_GET As String = "GL_CLSSETUP_BILLPARTY_GET"
        Dim dsCustomer As New DataSet()
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intCustIndex As Integer

        hidtranstype.Value = rblTransType.SelectedItem.Value

        If rblTransType.SelectedItem.Value = "1" Then
            lblCustomerErr.Text = "Please enter Supplier."
            lstPurchType.Enabled = True
            lstCustomer.Enabled = True
            txtCustDocRefNo.Enabled = True
            lstDestination.SelectedIndex = 0
            lstDestination.Enabled = False
            lblCustomerTag.Text = "Supplier"
            lblCustDocRefNoTag.Text = "Supplier"
            lstOrigin.Enabled = False
            lstDeliveryOrder.Enabled = False
            lstTransporter.Enabled = False

            If lstPurchType.SelectedItem.Value = "" then
                lstPurchType.SelectedIndex = objPUSetup.EnumSupplierType.FFBSupplier
            End If

            strParam = "||" & objPUSetup.EnumSuppStatus.Active & "||SupplierCode||SELECT"
            strParam = strParam & "|" & IIf(Session("SS_COACENTRALIZED") = "1", "", " A.AccCode in (SELECT AccCode FROM GL_Account WHERE LocCode='" & Trim(strLocation) & "') ")
            strParam = strParam & "|" & objPUSetup.EnumSupplierType.FFBSupplier 'lstPurchType.SelectedItem.Value
            'strParam = "||" & objPUSetup.EnumSuppStatus.Active & "||SupplierCode|||" & lstPurchType.SelectedItem.Value

            Try
                intErrNo = objPUSetup.mtdGetSupplier(strOppCd_Supplier_GET, strParam, dsCustomer)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=WM_CLSTRX_WEIGHBRIDGE_TICKET_SUPP_DROPDOWNLIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=WM/trx/WM_trx_WeighBridgeTicketDet.aspx")
            End Try

            If dsCustomer.Tables(0).Rows.Count > 0 Then
                For intCnt = 0 To dsCustomer.Tables(0).Rows.Count - 1
                    dsCustomer.Tables(0).Rows(intCnt).Item("SupplierCode") = Trim(dsCustomer.Tables(0).Rows(intCnt).Item("SupplierCode"))
                    dsCustomer.Tables(0).Rows(intCnt).Item("Name") = Trim(dsCustomer.Tables(0).Rows(intCnt).Item("SupplierCode")) & " (" & Trim(dsCustomer.Tables(0).Rows(intCnt).Item("Name")) & ")"

                    If TRIM(dsCustomer.Tables(0).Rows(intCnt).Item("SupplierCode")) = TRIM(pv_strCustomer) Then
                        intCustIndex = intCnt + 1
                    End If
                Next intCnt
            End If

            dr = dsCustomer.Tables(0).NewRow()
            dr("SupplierCode") = ""
            dr("Name") = "Select Supplier"
            dsCustomer.Tables(0).Rows.InsertAt(dr, 0)
            If intCustIndex = 0 And Trim(pv_strCustomer) <> "" Then
                dr = dsCustomer.Tables(0).NewRow()
                dr("SupplierCode") = pv_strCustomer
                dr("Name") = pv_strCustomer & " (Deleted)"
                intCustIndex = dsCustomer.Tables(0).Rows.Count
                dsCustomer.Tables(0).Rows.InsertAt(dr, intCustIndex)
            End If

            lstCustomer.DataSource = dsCustomer.Tables(0)
            lstCustomer.DataValueField = "SupplierCode"
            lstCustomer.DataTextField = "Name"
            lstCustomer.DataBind()
            lstCustomer.SelectedIndex = intCustIndex

        ElseIf rblTransType.SelectedItem.Value = "2" Then
            lstPurchType.SelectedIndex = 0
            lstPurchType.Enabled = False
            lstCustomer.Enabled = True
            txtCustDocRefNo.Enabled = True
            lstDestination.SelectedIndex = 0
            lstDestination.Enabled = False
            lstOrigin.Enabled = True

            lblCustomerErr.Text = "Please enter " & lblBillPartyTag.Text & "."
            lblCustomerTag.Text = lblBillPartyTag.Text
            lblCustDocRefNoTag.Text = lblBillPartyTag.Text

            strParam = "||" & objGLSetup.EnumBillPartyStatus.Active & "||BillPartyCode||"
            Try
                intErrNo = objGLSetup.mtdGetBillParty(strOppCd_BillParty_GET, strParam, dsCustomer)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=WM_CLSTRX_WEIGHBRIDGE_TICKET_BILLPARTY_DROPDOWNLIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=WM/trx/WM_trx_WeighBridgeTicketDet.aspx")
            End Try

            If dsCustomer.Tables(0).Rows.Count > 0 Then
                For intCnt = 0 To dsCustomer.Tables(0).Rows.Count - 1
                    dsCustomer.Tables(0).Rows(intCnt).Item("BillPartyCode") = Trim(dsCustomer.Tables(0).Rows(intCnt).Item("BillPartyCode"))
                    dsCustomer.Tables(0).Rows(intCnt).Item("Name") = Trim(dsCustomer.Tables(0).Rows(intCnt).Item("BillPartyCode")) & " (" & Trim(dsCustomer.Tables(0).Rows(intCnt).Item("Name")) & ")"

                    If TRIM(dsCustomer.Tables(0).Rows(intCnt).Item("BillPartyCode")) = TRIM(pv_strCustomer) Then
                        intCustIndex = intCnt + 1
                    End If
                Next intCnt
            End If

            dr = dsCustomer.Tables(0).NewRow()
            dr("BillPartyCode") = ""
            dr("Name") = "Select " & lblBillPartyTag.Text
            dsCustomer.Tables(0).Rows.InsertAt(dr, 0)
            If intCustIndex = 0 And Trim(pv_strCustomer) <> "" Then
                dr = dsCustomer.Tables(0).NewRow()
                dr("BillPartyCode") = pv_strCustomer
                dr("Name") = pv_strCustomer & " (Deleted)"
                intCustIndex = dsCustomer.Tables(0).Rows.Count
                dsCustomer.Tables(0).Rows.InsertAt(dr, intCustIndex)
            End If

            lstCustomer.DataSource = dsCustomer.Tables(0)
            lstCustomer.DataValueField = "BillPartyCode"
            lstCustomer.DataTextField = "Name"
            lstCustomer.DataBind()
            lstCustomer.SelectedIndex = intCustIndex
        
        ElseIf rblTransType.SelectedItem.Value = "3" Then
            lstCustomer.SelectedIndex = 0
            lstCustomer.Enabled = False
            txtCustDocRefNo.Text = ""
            txtCustDocRefNo.Enabled = False

            lstPurchType.SelectedIndex = 0
            lstPurchType.Enabled = False
            lstDestination.SelectedIndex = 0
            lstDestination.Enabled = False

            lblCustomerErr.Text = ""
            lblCustomerTag.Text = ""
            lblCustDocRefNoTag.Text = ""

             BindProduct(objWMTrx.EnumWeighBridgeTicketProduct.Others)
        ElseIf rblTransType.SelectedItem.Value = "4" Then
            lstPurchType.SelectedIndex = 0
            lstPurchType.Enabled = False
            lstDestination.Enabled = True
            lblCustomerErr.Text = "Please enter " & lblBillPartyTag.Text & "."
            
            lblCustomerTag.Text = lblBillPartyTag.Text
            lblCustDocRefNoTag.Text = lblBillPartyTag.Text

            strParam = "||" & objGLSetup.EnumBillPartyStatus.Active & "||BillPartyCode||"
            Try
                intErrNo = objGLSetup.mtdGetBillParty(strOppCd_BillParty_GET, strParam, dsCustomer)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=WM_CLSTRX_WEIGHBRIDGE_TICKET_BILLPARTY_DROPDOWNLIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=WM/trx/WM_trx_WeighBridgeTicketDet.aspx")
            End Try

            If dsCustomer.Tables(0).Rows.Count > 0 Then
                For intCnt = 0 To dsCustomer.Tables(0).Rows.Count - 1
                    dsCustomer.Tables(0).Rows(intCnt).Item("BillPartyCode") = Trim(dsCustomer.Tables(0).Rows(intCnt).Item("BillPartyCode"))
                    dsCustomer.Tables(0).Rows(intCnt).Item("Name") = Trim(dsCustomer.Tables(0).Rows(intCnt).Item("BillPartyCode")) & " (" & Trim(dsCustomer.Tables(0).Rows(intCnt).Item("Name")) & ")"

                    If dsCustomer.Tables(0).Rows(intCnt).Item("BillPartyCode") = pv_strCustomer Then
                        intCustIndex = intCnt + 1
                    End If
                Next intCnt
            End If

            dr = dsCustomer.Tables(0).NewRow()
            dr("BillPartyCode") = ""
            dr("Name") = "Select " & lblBillPartyTag.Text
            dsCustomer.Tables(0).Rows.InsertAt(dr, 0)
            If intCustIndex = 0 And Trim(pv_strCustomer) <> "" Then
                dr = dsCustomer.Tables(0).NewRow()
                dr("BillPartyCode") = pv_strCustomer
                dr("Name") = pv_strCustomer & " (Deleted)"
                intCustIndex = dsCustomer.Tables(0).Rows.Count
                dsCustomer.Tables(0).Rows.InsertAt(dr, intCustIndex)
            End If
            lstCustomer.DataSource = dsCustomer.Tables(0)
            lstCustomer.DataValueField = "BillPartyCode"
            lstCustomer.DataTextField = "Name"
            lstCustomer.DataBind()
            lstCustomer.SelectedIndex = intCustIndex
            
        End If

        If Not dsCustomer Is Nothing Then
            dsCustomer = Nothing
        End If

    End Sub

    Sub BindTransporter(ByVal pv_strTransporter As String)
        Dim strOpCd_Transporter_GET As String = "WM_CLSSETUP_TRANSPORTER_GET"
        Dim dsTransporter As New DataSet()
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intTransIndex As Integer

        strParam = "||" & objWMSetup.EnumTransporterStatus.Active & "|||TransporterCode|"
        Try
            intErrNo = objWMSetup.mtdGetTransporter(strOpCd_Transporter_GET, strParam, dsTransporter)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=WM_CLSTRX_WEIGHBRIDGE_TICKET_TRANSPORTER_DROPDOWNLIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=WM/trx/WM_trx_WeighBridgeTicketDet.aspx")
        End Try

        If dsTransporter.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To dsTransporter.Tables(0).Rows.Count - 1
                dsTransporter.Tables(0).Rows(intCnt).Item("TransporterCode") = Trim(dsTransporter.Tables(0).Rows(intCnt).Item("TransporterCode"))
                dsTransporter.Tables(0).Rows(intCnt).Item("Name") = Trim(dsTransporter.Tables(0).Rows(intCnt).Item("TransporterCode")) & " (" & Trim(dsTransporter.Tables(0).Rows(intCnt).Item("Name")) & ")"

                If dsTransporter.Tables(0).Rows(intCnt).Item("TransporterCode") = pv_strTransporter Then
                    intTransIndex = intCnt + 1
                End If
            Next intCnt
        End If

        dr = dsTransporter.Tables(0).NewRow()
        dr("TransporterCode") = ""
        dr("Name") = "Select Transporter"
        dsTransporter.Tables(0).Rows.InsertAt(dr, 0)
        If intTransIndex = 0 And Trim(pv_strTransporter) <> "" Then
            dr = dsTransporter.Tables(0).NewRow()
            dr("TransporterCode") = pv_strTransporter
            dr("Name") = pv_strTransporter & " (Deleted)"
            intTransIndex = dsTransporter.Tables(0).Rows.Count
            dsTransporter.Tables(0).Rows.InsertAt(dr, intTransIndex)
        End If

        lstTransporter.DataSource = dsTransporter.Tables(0)
        lstTransporter.DataValueField = "TransporterCode"
        lstTransporter.DataTextField = "Name"
        lstTransporter.DataBind()

        lstTransporter.SelectedIndex = intTransIndex

        If Not dsTransporter Is Nothing Then
            dsTransporter = Nothing
        End If

    End Sub

    Sub DisableControl()

        rblTransType.Enabled = False
        lstProduct.Enabled = False
        lstCustomer.Enabled = False
        txtCustDocRefNo.Enabled = False
        txtPL3No.Enabled = False
        lstTransporter.Enabled = False
        txtVehicle.Enabled = False
        txtDriverName.Enabled = False
        txtDriverICNo.Enabled = False
        txtPlantingYear.Enabled = False
        txtBlock.Enabled = False
        txtDateIn.Enabled = False
        txtInHour.Enabled = False
        txtInMinute.Enabled = False
        lstInAMPM.Enabled = False
        txtFirstWgt.Enabled = False
        txtDateOut.Enabled = False
        txtOutHour.Enabled = False
        txtOutMinute.Enabled = False
        lstOutAMPM.Enabled = False
        txtSecondWgt.Enabled = False
        txtDateRcv.Enabled = False
        txtCustFirstWgt.Enabled = False
        txtCustSecondWgt.Enabled = False
        txtRemarks.Enabled = False

        lblPeriod.Enabled = False
        lblStatus.Enabled = False
        lblCreateDate.Enabled = False
        lblLastUpdate.Enabled = False
        lblUpdateBy.Enabled = False

        btnSave.Visible = True
        btnDelete.Visible = True
        'use G2 Weighing System
        btnUnDelete.Visible = True

        btnSelInDate.Visible = False
        btnSelOutDate.Visible = False
        btnSelRcvDate.Visible = False
        btnCurrInDtTime.Enabled = False
        btnCurrOutDtTime.Enabled = False
        
        lstEmpCode.Enabled = False
        FindEmp.Visible = False
        lstDestination.Enabled = False
    End Sub

    Sub EnableControl()
        
        txtCustDocRefNo.Enabled = True
        txtPL3No.Enabled = True
        lstTransporter.Enabled = True
        txtVehicle.Enabled = True
        txtDriverName.Enabled = True
        txtDriverICNo.Enabled = True
        txtPlantingYear.Enabled = True
        txtBlock.Enabled = True
        txtDateIn.Enabled = True
        txtInHour.Enabled = True
        txtInMinute.Enabled = True
        lstInAMPM.Enabled = True
        txtFirstWgt.Enabled = True
        txtDateOut.Enabled = True
        txtOutHour.Enabled = True
        txtOutMinute.Enabled = True
        lstOutAMPM.Enabled = True
        txtSecondWgt.Enabled = True
        txtDateRcv.Enabled = True
        txtRemarks.Enabled = True

        lblPeriod.Enabled = True
        lblStatus.Enabled = True
        lblCreateDate.Enabled = True
        lblLastUpdate.Enabled = True
        lblUpdateBy.Enabled = True

        'use G2 Weighing System
        btnSave.Visible = True

       
        If Not hidTicketNo.Value = "" Then
            If Not hidMatchingID.Value = "" Then
                rblTransType.Enabled = False
                lstProduct.Enabled = False
                
                lstDestination.Enabled = False

                lstCustomer.Enabled = False
                txtCustFirstWgt.Enabled = False
                txtCustSecondWgt.Enabled = False
                btnDelete.Visible = False
                btnUnDelete.Visible = False
            Else
                rblTransType.Enabled = True
                lstProduct.Enabled = True

                lstDestination.Enabled = true                

                lstCustomer.Enabled = True
                txtCustFirstWgt.Enabled = True
                txtCustSecondWgt.Enabled = True
                'use G2 Weighing System
                btnDelete.Visible = True
                btnUnDelete.Visible = True
            End If
            btnPrint.Visible = True
        Else


            btnDelete.Visible = False
            btnUnDelete.Visible = False

            btnPrint.Visible = False
        End If

        btnSelInDate.Visible = True
        btnSelOutDate.Visible = True
        btnSelRcvDate.Visible = True
        btnCurrInDtTime.Enabled = True
        btnCurrOutDtTime.Enabled = True

    End Sub

    Sub DisplayData()
        Dim arrDateIn As Array
        Dim arrDateOut As Array

        dsTicketItem = LoadData()

        If dsTicketItem.Tables(0).Rows.Count > 0 Then

           
            lblTicketNo.Text =  Trim(dsTicketItem.Tables(0).Rows(0).Item("TicketNo"))

            lblPeriod.Text = Trim(dsTicketItem.Tables(0).Rows(0).Item("AccMonth")) & "/" & Trim(dsTicketItem.Tables(0).Rows(0).Item("AccYear"))
            lblStatus.Text = Trim(objWMTrx.mtdGetWeighBridgeTicketStatus(dsTicketItem.Tables(0).Rows(0).Item("Status")))
            lblCreateDate.Text = Trim(objGlobal.Getlongdate(dsTicketItem.Tables(0).Rows(0).Item("CreateDate")))
            lblLastUpdate.Text = Trim(objGlobal.Getlongdate(dsTicketItem.Tables(0).Rows(0).Item("UpdateDate")))
            lblUpdateBy.Text = Trim(dsTicketItem.Tables(0).Rows(0).Item("UserName"))
                        
            txtCustDocRefNo.Text = Trim(dsTicketItem.Tables(0).Rows(0).Item("CustomerDocNo"))
            txtPL3No.Text = Trim(dsTicketItem.Tables(0).Rows(0).Item("PL3No"))
            txtVehicle.Text = Trim(dsTicketItem.Tables(0).Rows(0).Item("VehicleCode"))
            txtDriverName.Text = Trim(dsTicketItem.Tables(0).Rows(0).Item("DriverName"))
            txtDriverICNo.Text = Trim(dsTicketItem.Tables(0).Rows(0).Item("DriverIC"))
            txtPlantingYear.Text = Trim(dsTicketItem.Tables(0).Rows(0).Item("PlantingYear"))
            txtBlock.Text = Trim(dsTicketItem.Tables(0).Rows(0).Item("BlkCode"))

            Select Case Trim(lblStatus.Text)
                Case objWMTrx.mtdGetWeighBridgeTicketStatus(objWMTrx.EnumWeighBridgeTicketStatus.Active)
                    EnableControl()
                Case objWMTrx.mtdGetWeighBridgeTicketStatus(objWMTrx.EnumWeighBridgeTicketStatus.Deleted)
                    DisableControl()
            End Select


            txtDateIn.Text = objGlobal.GetShortDate(strDateFormat, Trim(dsTicketItem.Tables(0).Rows(0).Item("InDate")))
            txtInHour.Text = Hour(Format(dsTicketItem.Tables(0).Rows(0).Item("InDate"), "h:m:s"))
            txtInMinute.Text = objGlobal.GetMinute(Minute(Trim(dsTicketItem.Tables(0).Rows(0).Item("InDate"))))

            arrDateIn = Split(Trim(dsTicketItem.Tables(0).Rows(0).Item("InDate")), " ")
            If UBound(arrDateIn) >= 2 Then
                lstInAMPM.SelectedItem.Text = arrDateIn(2)
            End If
        
            txtDateOut.Text = objGlobal.GetShortDate(strDateFormat, Trim(dsTicketItem.Tables(0).Rows(0).Item("OutDate")))

            arrDateOut = Split(Trim(dsTicketItem.Tables(0).Rows(0).Item("OutDate")), " ")

            If arrDateOut(0) <> "1/1/1900" Then
                txtOutHour.Text = Hour(Format(dsTicketItem.Tables(0).Rows(0).Item("OutDate"), "h:m:s"))
                txtOutMinute.Text = objGlobal.GetMinute(Minute(Trim(dsTicketItem.Tables(0).Rows(0).Item("OutDate"))))
                If arrDateOut.GetUpperBound(0) = 0 Then
                    lstOutAMPM.SelectedIndex = 0
                Else
                    lstOutAMPM.SelectedItem.Text = arrDateOut(2)
                End If
            Else
                lstOutAMPM.SelectedIndex = 0
                txtDateOut.Text = ""
                txtOutHour.Text = ""
                txtOutMinute.Text = ""
            End If

            If IsPostBack Then


            Else
                
                txtFirstWgt.Text = dsTicketItem.Tables(0).Rows(0).Item("FirstWeight")
                txtSecondWgt.Text = dsTicketItem.Tables(0).Rows(0).Item("SecondWeight")
                hidNetWgt.Value = dsTicketItem.Tables(0).Rows(0).Item("NetWeight")

    
                If objGlobal.GetShortDate(strDateFormat, Trim(dsTicketItem.Tables(0).Rows(0).Item("DateReceived"))) = "" Or _
                    dsTicketItem.Tables(0).Rows(0).Item("DateReceived") = "1/1/1900" Then
                    txtCustFirstWgt.Text = ""
                    txtCustSecondWgt.Text = ""
                    hidCustNetWgt.Value = ""
                Else
                    
                    txtCustFirstWgt.Text = dsTicketItem.Tables(0).Rows(0).Item("BuyerFirstWeight")
                    txtCustSecondWgt.Text = dsTicketItem.Tables(0).Rows(0).Item("BuyerSecondWeight")
                    hidCustNetWgt.Value = dsTicketItem.Tables(0).Rows(0).Item("BuyerNetWeight")


                End If
            End If

            If dsTicketItem.Tables(0).Rows(0).Item("DateReceived") = "1/1/1900" then
                txtDateRcv.Text = ""
            else
                txtDateRcv.Text = objGlobal.GetShortDate(strDateFormat, Trim(dsTicketItem.Tables(0).Rows(0).Item("DateReceived")))
            end if            
            txtRemarks.Text = Trim(dsTicketItem.Tables(0).Rows(0).Item("Remarks"))

            If Trim(dsTicketItem.Tables(0).Rows(0).Item("TransType")) = objWMTrx.EnumWeighBridgeTicketTransType.Purchase Then
                rblTransType.SelectedIndex = 0
            ElseIf Trim(dsTicketItem.Tables(0).Rows(0).Item("TransType")) = objWMTrx.EnumWeighBridgeTicketTransType.Sales Then
                rblTransType.SelectedIndex = 1
            ElseIf Trim(dsTicketItem.Tables(0).Rows(0).Item("TransType")) = objWMTrx.EnumWeighBridgeTicketTransType.Usage
                rblTransType.SelectedIndex = 2
            ElseIf Trim(dsTicketItem.Tables(0).Rows(0).Item("TransType")) = objWMTrx.EnumWeighBridgeTicketTransType.Others
                rblTransType.SelectedIndex = 3
            End If
            
            BindPurchType(Trim(dsTicketItem.Tables(0).Rows(0).Item("PurchaseType")))
            BindProduct(Trim(dsTicketItem.Tables(0).Rows(0).Item("ProductCode")))
            BindCustomer(Trim(dsTicketItem.Tables(0).Rows(0).Item("CustomerCode")))
            BindTransporter(Trim(dsTicketItem.Tables(0).Rows(0).Item("TransporterCode")))
            BindDestination(Trim(dsTicketItem.Tables(0).Rows(0).Item("Destination")))
            BindEmployee(Trim(dsTicketItem.Tables(0).Rows(0).Item("EmpCode")))
            'BindDeliveryOrder("",Trim(dsTicketItem.Tables(0).Rows(0).Item("DeliveryNoteNo")))
            BindOrigin(Trim(dsTicketItem.Tables(0).Rows(0).Item("Origin")))

            If Trim(dsTicketItem.Tables(0).Rows(0).Item("EmpCode")) = "" Then
                lstEmpCode.Enabled = False
                FindEmp.Visible = False
            Else
                lstEmpCode.Enabled = True
                FindEmp.Visible = True
            End If

            hidMatchingID.Value = Trim(dsTicketItem.Tables(0).Rows(0).Item("MatchingID"))
            
        End If
    End Sub


    Sub btnSave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim decFirstWgt As Decimal
        Dim decSecondWgt As Decimal
        Dim decNetWeight As Decimal
        Dim decCustFirstWgt As Decimal
        Dim decCustSecondWgt As Decimal
        Dim decCustNetWeight As Decimal
        Dim objFormatDate As String
        Dim objActualDate As String
        Dim strDateIn As String
        Dim strDateOut As String
        Dim strDateRcv As String
        Dim strInTime As String
        Dim strOutTime As String
        Dim strRslKey As String
        Dim strTrxDate As String = Date_Validation(txtDateIn.Text, False)

        decFirstWgt = IIf(Trim(txtFirstWgt.Text) = "", 0, Trim(txtFirstWgt.Text))
        decSecondWgt = IIf(Trim(txtSecondWgt.Text) = "", 0, Trim(txtSecondWgt.Text))
        decCustFirstWgt = IIf(Trim(txtCustFirstWgt.Text) = "", 0, Trim(txtCustFirstWgt.Text))
        decCustSecondWgt = IIf(Trim(txtCustSecondWgt.Text) = "", 0, Trim(txtCustSecondWgt.Text))

        
        If rblTransType.SelectedItem.Value = objWMTrx.EnumWeighBridgeTicketTransType.Sales Then
            decNetWeight = decSecondWgt - decFirstWgt
            decCustNetWeight = decCustFirstWgt - decCustSecondWgt
        Else
            decNetWeight = decFirstWgt - decSecondWgt
            decCustNetWeight = decCustSecondWgt - decCustFirstWgt
        End If
        

        hidFirstWgt.Value = decFirstWgt
        hidSecondWgt.Value = decSecondWgt
        hidCalNetWgt.Value = decNetWeight
        hidNetWgt.Value = decNetWeight

        hidCustFirstWgt.Value = decCustFirstWgt
        hidCustSecondWgt.Value = decCustSecondWgt
        hidCalCustNetWgt.Value = decCustNetWeight
        hidCustNetWgt.Value = decCustNetWeight

        If hidFirstWgt.Value > 0 And hidSecondWgt.Value > 0 Then
            If hidCalNetWgt.Value < 0 Then
                lblErrNetWgt.Visible = True
                If lblStatus.Text.Trim <> "" Then
                    DisplayData()
                End If
                Exit Sub
            End If
        End If


        If Not txtDateIn.Text = "" Then
            If objGlobal.mtdValidInputDate(strDateFormat, Trim(txtDateIn.Text), objFormatDate, objActualDate) = False Then
                lblErrDateIn.Visible = True
                lblErrDateIn.Text = lblErrDateInMsg.Text & objFormatDate
                Exit Sub
            Else
                strDateIn = objActualDate
            End If
        End If

        If Not txtDateOut.Text = "" Then
            If objGlobal.mtdValidInputDate(strDateFormat, Trim(txtDateOut.Text), objFormatDate, objActualDate) = False Then
                lblErrDateOut.Visible = True
                lblErrDateOut.Text = lblErrDateOutMsg.Text & objFormatDate
                Exit Sub
            Else
                strDateOut = objActualDate
            End If
        End If

        If Not txtDateRcv.Text = "" Then
            If objGlobal.mtdValidInputDate(strDateFormat, Trim(txtDateRcv.Text), objFormatDate, objActualDate) = False Then
                lblErrDateRcv.Visible = True
                lblErrDateRcv.Text = lblErrDateRcvMsg.Text & objFormatDate
                Exit Sub
            Else
                strDateRcv = objActualDate
            End If
        Else
            strDateRcv = ""
        End If

        If Trim(txtInHour.Text) = "" And Trim(txtInMinute.Text) = "" Then
            strInTime = Now.ToLongTimeString
        Else
            strInTime = Trim(txtInHour.Text) & ":" & Trim(txtInMinute.Text) & ":00 " & lstInAMPM.SelectedItem.Text
        End If

        If Trim(txtOutHour.Text) = "" And Trim(txtOutMinute.Text) = "" Then
            strOutTime = "" 
        Else
            strOutTime = Trim(txtOutHour.Text) & ":" & Trim(txtOutMinute.Text) & ":00 " & lstOutAMPM.SelectedItem.Text
        End If

        If txtDateIn.Text.Trim <> "" And (txtInHour.Text.Trim = "" Or txtInMinute.Text.Trim = "") Then
            lblErrTimeIn.Visible = True
            Exit Sub
        End If

        If txtDateIn.Text.Trim <> "" And txtInHour.Text.Trim <> "" And txtInMinute.Text.Trim <> "" And txtFirstWgt.Text.Trim = "" Then
            lblErrFirstWgt.Visible = True
            Exit Sub
        End If


        If txtDateOut.Text.Trim <> "" And (txtOutHour.Text.Trim = "" Or txtOutMinute.Text.Trim = "") Then
            lblErrTimeOut.Visible = True
            Exit Sub
        ElseIf txtDateOut.Text.Trim = "" And (txtOutHour.Text.Trim <> "" Or txtOutMinute.Text.Trim <> "") Then
            lblErrDateOutBlank.Visible = True
            Exit Sub
        End If

        If txtDateOut.Text.Trim <> "" And txtOutHour.Text.Trim <> "" And txtOutMinute.Text.Trim <> "" And txtSecondWgt.Text.Trim = "" Then
            lblErrSecWgt.Visible = True
            Exit Sub
        End If

        If txtDateRcv.Text.Trim <> "" Then
            If txtCustFirstWgt.Text.Trim = "" Then
                lblErrCustFirstWgt.Visible = True
                Exit Sub
            ElseIf txtCustFirstWgt.Text.Trim = 0 Then
                lblErrCustFirstWgtZero.Visible = True
                Exit Sub
            End If

            If txtCustSecondWgt.Text.Trim = "" Then
                lblErrCustSecWgt.Visible = True
                Exit Sub
            ElseIf txtCustSecondWgt.Text.Trim = 0 Then
                lblErrCustSecWgtZero.Visible = True
                Exit Sub
            End If
        End If


        If (txtDateRcv.Text.Trim = "" And txtCustFirstWgt.Text.Trim <> "") Or _
            (txtDateRcv.Text.Trim = "" And txtCustSecondWgt.Text.Trim <> "") Then
            lblErrDateRcvBlank.Visible = True
            Exit Sub
        End If

        REM If rblTransType.SelectedItem.Value = objWMTrx.EnumWeighBridgeTicketTransType.Purchase And lstPurchType.SelectedItem.Value = objPUSetup.EnumSupplierType.Internal Then
            REM If txtPlantingYear.Text.Trim = "" Then
                REM lblErrPlantYr.Visible = True
                REM Exit Sub
            REM End If
        REM End If
        REM If lstEmpCode.Enabled = True then
            REM If lstEmpCode.SelectedIndex = 0 Then
                REM lblEmpErr.Visible = True
                REM Exit Sub
            REM End If
        REM End If
		
        Select Case rblTransType.SelectedItem.Value
            Case objWMTrx.EnumWeighBridgeTicketTransType.Purchase, objWMTrx.EnumWeighBridgeTicketTransType.Sales
                If Trim(lstCustomer.SelectedItem.Value) = "" Then
                    lblCustomerErr.Visible = True
                    Exit Sub
                End If
            Case objWMTrx.EnumWeighBridgeTicketTransType.Others
                If lstDestination.SelectedItem.Value = "" Then
                    lblDestinationErr.Visible = True
                    Exit Sub
                End If
                If lstDestination.SelectedItem.Value = objWMTrx.EnumWeighBridgeDestination.BillParty And _
                  Trim(lstCustomer.SelectedItem.Value) = "" Then
                    lblCustomerErr.Visible = True
                    Exit Sub
                End If
        End Select

        If lstOrigin.SelectedItem.Value = "" And rblTransType.SelectedItem.Value = objWMTrx.EnumWeighBridgeTicketTransType.Sales Then
            lblOriginErr.Visible = True
            Exit Sub
        End If

        strAccMonth = Month(strDateIn)
        strAccYear = Year(strDateIn)

        strParam =
        Trim(lblTicketNo.Text) & "|" &
        Trim(rblTransType.SelectedItem.Value) & "|" &
        Trim(lstProduct.SelectedItem.Value) & "|" &
        Trim(lstCustomer.SelectedItem.Value) & "|" &
        IIf(Trim(txtCustDocRefNo.Text) = "", "", Trim(txtCustDocRefNo.Text)) & "|" &
        Trim(lstDeliveryOrder.SelectedItem.Value) & "|" &
        IIf(Trim(txtPL3No.Text) = "", "", Trim(txtPL3No.Text)) & "|" &
        IIf(Trim(lstTransporter.SelectedItem.Value) = "", "", Trim(lstTransporter.SelectedItem.Value)) & "|" &
        IIf(Trim(txtVehicle.Text) = "", "", Trim(txtVehicle.Text)) & "|" &
        IIf(Trim(txtDriverName.Text) = "", "", Trim(txtDriverName.Text).Replace("'", "''")) & "|" &
        IIf(Trim(txtDriverICNo.Text) = "", "", Trim(txtDriverICNo.Text)) & "|" &
        IIf(Trim(txtPlantingYear.Text) = "", "", Trim(txtPlantingYear.Text)) & "|" &
        IIf(Trim(txtBlock.Text) = "", "", Trim(txtBlock.Text)) & "|" &
        Trim(strDateIn) & " " & strInTime & "|" &
        IIf(Trim(txtFirstWgt.Text) = "", 0, Trim(txtFirstWgt.Text)) & "|" &
        Trim(strDateOut) & " " & strOutTime & "|" &
        IIf(Trim(txtSecondWgt.Text) = "", 0, Trim(txtSecondWgt.Text)) & "|" &
        IIf(Trim(decNetWeight) = "", 0, Trim(decNetWeight)) & "|" &
        IIf(Trim(strDateRcv) = "", "", Trim(strDateRcv)) & "|" &
        IIf(Trim(txtCustFirstWgt.Text) = "", 0, Trim(txtCustFirstWgt.Text)) & "|" &
        IIf(Trim(txtCustSecondWgt.Text) = "", 0, Trim(txtCustSecondWgt.Text)) & "|" &
        IIf(Trim(decCustNetWeight) = "", 0, Trim(decCustNetWeight)) & "|" &
        IIf(Trim(txtRemarks.Text) = "", "", Trim(txtRemarks.Text)) & "|" &
        objWMTrx.EnumWeighBridgeTicketStatus.Active & "|" & Trim(lstEmpCode.SelectedItem.Value) & "|" &
        Trim(lstDestination.SelectedItem.Value) & "|" & Trim(lstPurchType.SelectedItem.Value) & "|" &
        Trim(lstOrigin.SelectedItem.Value) & "|" &
        strTrxDate

        Try
        
            intErrNo = objWMTrx.mtdUpdWeighBridgeTicket(strOppCd_WeighBridge_Ticket_ADD, _
                                                        strOppCd_WeighBridge_Ticket_UPD, _
                                                        strOppCd_WeighBridge_Ticket_GET, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strParam, _
                                                        blnDupKey, _
                                                        blnUpdate.Text, strRslKey)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=WM_CLSTRX_WEIGHBRIDGE_TICKET_DET_UPDATE&errmesg=" & lblErrMessage.Text & "&redirect=WM/trx/WM_trx_WeighBridgeTicketDet.aspx")
        End Try

        If blnDupKey = True Then 
            lblDupMsg.Visible = True
        Else
            lblDupMsg.Visible = False
            blnUpdate.Text = True
            If Trim(strRslKey) = "" then
                hidTicketNo.value = Trim(lblTicketNo.Text)
            else
                lblTicketNo.Text = strRslKey
                hidTicketNo.value = Trim(lblTicketNo.Text)
            end if

            DisplayData()
        End If
    End Sub

    Sub btnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)

        strParam = lblTicketNo.Text.Trim & "|" & objWMTrx.EnumWeighBridgeTicketStatus.Deleted
        Try
            intErrNo = objWMTrx.mtdDelWeighBridgeTicket(strOppCd_WeighBridge_Ticket_UPD, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strParam)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=WM_CLSTRX_WEIGHBRIDGE_TICKET_DET_DELETE&errmesg=" & lblErrMessage.Text & "&redirect=WM/trx/WM_trx_WeighBridgeTicketDet.aspx")
        End Try

        DisplayData()
    End Sub

    Sub btnUnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)

        strParam = lblTicketNo.Text.Trim & "|" & objWMTrx.EnumWeighBridgeTicketStatus.Active
        Try
            intErrNo = objWMTrx.mtdDelWeighBridgeTicket(strOppCd_WeighBridge_Ticket_UPD, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strParam)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=WM_CLSTRX_WEIGHBRIDGE_TICKET_DET_UNDELETE&errmesg=" & lblErrMessage.Text & "&redirect=WM/trx/WM_trx_WeighBridgeTicketDet.aspx")
        End Try

        DisplayData()
    End Sub

    Sub btnCurrInDtTime_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)

        Dim strDate As String = Now.Today
        Dim strHour As String = Now.Hour
        Dim strMinute As String = Now.Minute
        Dim currDt As Date
        Dim arrDt As Array

        currDt = TimeOfDay
        arrDt = Split(currDt, " ")

        txtInHour.Text = objGlobal.GetHour(strHour)
        txtInMinute.Text = objGlobal.GetMinute(strMinute)
        txtDateIn.Text = objGlobal.GetShortDate(strDateFormat, strDate)

        If arrDt(1) = "AM" Then
            lstInAMPM.SelectedIndex = 0
        Else
            lstInAMPM.SelectedIndex = 1
        End If


    End Sub

    Sub btnCurrOutDtTime_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strDate As String = Now.Today
        Dim strHour As String = Now.Hour
        Dim strMinute As String = Now.Minute
        Dim currDt As Date
        Dim arrDt As Array

        currDt = TimeOfDay
        arrDt = Split(currDt, " ")

        txtOutHour.Text = objGlobal.GetHour(strHour)
        txtOutMinute.Text = objGlobal.GetMinute(strMinute)
        txtDateOut.Text = objGlobal.GetShortDate(strDateFormat, strDate)

        If arrDt(1) = "AM" Then
            lstOutAMPM.SelectedIndex = 0
        Else
            lstOutAMPM.SelectedIndex = 1
        End If

    End Sub

    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("WM_trx_WeighBridgeTicketList.aspx")
    End Sub


    Protected Sub btnPrint_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strTicketNo As String = lblTicketNo.Text
        Dim strCustDocRefNoTag As String = lblCustDocRefNoTag.Text
        Dim strCustomerTag As String = lblCustomerTag.Text
        Dim strVehicleTag As String = lblVehicleTag.Text
        Dim BlockTag As String = lblBlockTag.Text

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/WM_Rpt_WeighBridgeTicketDet.aspx" & _
                       "?TicketNo=" & Server.UrlEncode(strTicketNo) & _
                       "&CustDocRefNoTag=" & Server.UrlEncode(strCustDocRefNoTag) & _
                       "&CustomerTag=" & Server.UrlEncode(strCustomerTag) & _
                       "&VehicleTag=" & Server.UrlEncode(strVehicleTag) & _
                       "&BlockTag=" & Server.UrlEncode(BlockTag) & _
                       """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Sub BindOrigin(ByVal pv_strOrigin As String)

        If pv_strOrigin <> "" Then
            If lstOrigin.Items.Count = 0 Then
                lstOrigin.Items.Clear()
                lstOrigin.Items.Add(New ListItem("Select Origin", ""))
                lstOrigin.Items.Add(New ListItem("Mill", "1"))
                lstOrigin.Items.Add(New ListItem("Bulking", "2"))
            End If

            lstOrigin.SelectedIndex = pv_strOrigin
            'If pv_strOrigin = objWMTrx.EnumWeighBridgeDestination.BillParty Then
            '    lstCustomer.Enabled = True
            '    txtCustDocRefNo.Enabled = True
            'Else
            '    lstCustomer.Enabled = False
            '    txtCustDocRefNo.Enabled = False
            'End If

        Else
            lstOrigin.Items.Clear()
            lstOrigin.Items.Add(New ListItem("Select Origin", ""))
            lstOrigin.Items.Add(New ListItem("Mill", "1"))
            lstOrigin.Items.Add(New ListItem("Bulking", "2"))
        End If
    End Sub

    Function Date_Validation(ByVal pv_strInputDate As String, ByVal pv_blnIsShortDate As Boolean) As String
        Dim objSysCfgDs As New Object
        Dim objActualDate As New Object
        Dim strDateFormat As String
        Dim strParam As String
        Dim intErrNo As Integer

        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"

        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GR_GET_CONFIG&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_DAList.aspx")
        End Try

        strDateFormat = objSysCfg.mtdGetDateFormat(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt").Trim())

        If pv_blnIsShortDate Then
            Date_Validation = objGlobal.GetShortDate(strDateFormat, pv_strInputDate)
        Else
            If objGlobal.mtdValidInputDate(strDateFormat, _
                                           pv_strInputDate, _
                                           strAcceptFormat, _
                                           objActualDate) = True Then
                Date_Validation = objActualDate
            Else
                Date_Validation = ""
            End If
        End If
    End Function
End Class
