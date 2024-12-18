
Imports System
Imports System.Data
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports System.Text.RegularExpressions
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports System.Globalization

Public Class PM_Trx_CPOStorage_Det : Inherits Page
    Dim dsItem As DataSet

    Dim objPMTrx As New agri.PM.clsTrx()
    Dim objPMSetup As New agri.PM.clsSetup()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents lblCalcErr As Label
    Protected WithEvents lblDupMsg As Label
    Protected WithEvents lblFmt As Label
    Protected WithEvents lblDate As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblPeriod As Label
    Protected WithEvents txtAccMonth As Label
    Protected WithEvents txtAccYear As Label
    Protected WithEvents lblCreateDate As Label
    Protected WithEvents txtCreateDate As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdateBy As Label
    
    Protected WithEvents lblTransDate As Label
    Protected WithEvents lblStorageAreaCode As Label
    Protected WithEvents txtDate As TextBox
    Protected WithEvents lstStorageArea As DropDownList
    Protected WithEvents txtTemperature As TextBox
    Protected WithEvents txtCPOUllage As TextBox
    Protected WithEvents txtCPOWeight As TextBox
    
    Protected WithEvents rfvCPOWeight As Label
    Protected WithEvents revCPOWeight As Label

    Protected WithEvents btnSelTransDate As Image
    Protected WithEvents Delete As ImageButton
    
    Protected WithEvents btnCalculateCPOWeight As Button

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
    Dim intPMAR As Integer
    Dim strDateFormat As String

    Dim strOppCd_GET As String = "PM_CLSTRX_CPOSTORAGE_GET"
    Dim strOppCd_ADD As String = "PM_CLSTRX_CPOSTORAGE_ADD"
    Dim strOppCd_UPD As String = "PM_CLSTRX_CPOSTORAGE_UPD"
    Dim strOppCd_DEL As String = "PM_CLSTRX_CPOSTORAGE_DEL"
    Dim dbUnit As Double = 1000
	
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String
	
    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PMACCMONTH")
        strAccYear = Session("SS_PMACCYEAR")
        intPMAR = Session("SS_PMAR")
        strDateFormat = Session("SS_DATEFMT")
	    
        strLocType = Session("SS_LOCTYPE")
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMDailyProduction), intPMAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            lblDupMsg.Visible = False
            lblDate.Visible = False
            lblFmt.Visible = False
            If Not Page.IsPostBack Then
                lblCalcErr.Visible = False
                lblTransDate.Text = Trim(Request.QueryString("TransDate"))
                lblStorageAreaCode.Text = Trim(Request.QueryString("StorageAreaCode"))
                If lblTransDate.Text = "" Or lblStorageAreaCode.Text = "" Then
                    BindStorageAreaList(lstStorageArea, "")
                    txtAccMonth.Text = Trim(strAccMonth)
                    txtAccYear.Text = Trim(strAccYear)
                Else
                    DisplayData()
                End If
            End If
        End If
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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_CPOSTORAGE_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=PM/Trx/PM_Trx_CPOStorage_List.aspx")
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



    Protected Function LoadData() As DataSet
        
        strParam = "|AND CPO.LocCode='" & strLocation & _
                    "' AND TransDate='" & Trim(lblTransDate.Text) & _
                    "' AND StorageAreaCode='" & lblStorageAreaCode.Text & "'"
        Try
            intErrNo = objPMTrx.mtdGetCPOStorage(strOppCd_GET, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_CPOSTORAGE_GET&errmesg=" & lblErrMessage.Text & "&redirect=PM/Trx/PM_Trx_CPOStorage_List.aspx")
        End Try

        Return objDataSet
    End Function


    Sub DisplayData()
        Dim dsCPO As DataSet = LoadData()

        If dsCPO.Tables(0).Rows.Count > 0 Then
            lblErrMessage.visible = False
            lblDupMsg.visible = False
            lblFmt.visible = False
            rfvCPOWeight.visible = False
            revCPOWeight.visible = False
    
            txtDate.Text = objGlobal.GetShortDate(strDateFormat, dsCPO.Tables(0).Rows(0).Item("TransDate"))
            BindStorageAreaList(lstStorageArea, Trim(dsCPO.Tables(0).Rows(0).Item("StorageAreaCode")))
            txtTemperature.Text = dsCPO.Tables(0).Rows(0).Item("Temperature")
            txtCPOUllage.Text = dsCPO.Tables(0).Rows(0).Item("CPOUllage")
            txtCPOWeight.Text = FormatNumber(dsCPO.Tables(0).Rows(0).Item("CPOWeight") * dbUnit , 2, TriState.True, TriState.False, TriState.False)
            
            txtAccMonth.Text = Trim(dsCPO.Tables(0).Rows(0).Item("AccMonth"))
            txtAccYear.Text = Trim(dsCPO.Tables(0).Rows(0).Item("AccYear"))
            lblPeriod.Text = txtAccMonth.Text & "/" & txtAccYear.Text
            lblStatus.Text = objPMTrx.mtdGetCPOStorageStatus(Trim(dsCPO.Tables(0).Rows(0).Item("Status")))
            lblCreateDate.Text = objGlobal.GetLongDate(Trim(dsCPO.Tables(0).Rows(0).Item("CreateDate")))
            txtCreateDate.Text = Trim(dsCPO.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(Trim(dsCPO.Tables(0).Rows(0).Item("UpdateDate")))
            lblUpdateBy.Text = Trim(dsCPO.Tables(0).Rows(0).Item("Username"))
            
            txtDate.Enabled = False
            btnSelTransDate.visible = False
            lstStorageArea.Enabled = False
            lblTransDate.Text = Trim(dsCPO.Tables(0).Rows(0).Item("TransDate"))
            lblStorageAreaCode.Text = Trim(dsCPO.Tables(0).Rows(0).Item("StorageAreaCode"))
            Delete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Delete.visible = True
        End If
    End Sub
    
    Sub BindStorageAreaList(ByRef lstStorageArea As DropDownList, ByVal StorageArea As String)
        Dim strOpCdAreaCode_Get As String = "PM_CLSSETUP_STORAGEAREA_GET"
        Dim dsForDropDown As DataSet
        Dim SelectedIndex As Integer = -1
        Dim intCnt As Integer
        Dim TblAlias As String
        Dim DataTextField As String
        Dim SearchStr As String
        Dim strParam As String
        Dim drinsert As DataRow

        SearchStr = " AND SA.Status = '" & objPMSetup.EnumStorageAreaStatus.Active & _
                    "' AND SA.LocCode = '" & strLocation & _
                    "' AND ST.ProductCode ='" & objPMSetup.EnumProductCode.CPO & "'"

        strParam = "ORDER BY SA.StorageAreaCode asc|" & SearchStr

        Try
            intErrNo = objPMSetup.mtdGetStorageArea(strOpCdAreaCode_Get, strParam, dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_STORAGEAREA_BINDSTORAGEAREACODE&errmesg=" & lblErrMessage.Text & "&redirect=PM/Trx/PM_Trx_CPOStorage_List.aspx")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item("StorageAreaCode") = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("StorageAreaCode"))
            dsForDropDown.Tables(0).Rows(intCnt).Item("Description") = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("StorageAreaCode")) & " (" & Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("Description")) & " No. " & Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("StorageNo")) & ")"
            
            If Not StorageArea = "" Then
                If UCase(Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0))) = StorageArea Then
                    SelectedIndex = intCnt + 1
                End If
            End If
        Next intCnt

        drinsert = dsForDropDown.Tables(0).NewRow()
        drinsert(0) = ""
        drinsert(7) = "Please select storage area "
        dsForDropDown.Tables(0).Rows.InsertAt(drinsert, 0)

        lstStorageArea.DataSource = dsForDropDown.Tables(0)
        lstStorageArea.DataValueField = "StorageAreaCode"
        lstStorageArea.DataTextField = "Description"
        lstStorageArea.DataBind()

        If Not StorageArea = "" Then
            If SelectedIndex = -1 Then
                lstStorageArea.Items.Add(New ListItem(Trim(StorageArea), Trim(StorageArea)))
                SelectedIndex = lstStorageArea.Items.Count - 1
            End If
            lstStorageArea.SelectedIndex = SelectedIndex
        End If

        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If
    End Sub
    
    Sub btnSave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strDate As String = CheckDate()
        Dim blnDupKey As Boolean = False
        Dim oRegEx As RegEx = New RegEx("^\-|\d{1,15}(\.\d{1,5}|\.|)$")

        If Trim(txtCPOWeight.Text) = "" Then
            rfvCPOWeight.visible = True 
            revCPOWeight.visible = False
            Exit Sub
        Else
            rfvCPOWeight.visible = False
        End If

        If oRegEx.IsMatch(txtCPOWeight.Text) = True Then
            revCPOWeight.visible = False
        Else
            revCPOWeight.visible = True
            Exit Sub
        End If
        
        If strDate = "" Then
            Exit Sub
        End If

        If txtDate.Enabled = False Then
            blnUpdate.text = True
        Else
            blnUpdate.text = False
        End If
        strParam = strDate & "|" & _
                   lstStorageArea.SelectedItem.Value & "|" & _
                   txtTemperature.Text.Trim & "|" & _
                   txtCPOUllage.Text.Trim & "|" & _
                   txtCPOWeight.Text / dbUnit & "|" & _
                   txtAccMonth.Text & "|" & _ 
                   txtAccYear.Text & "|" & _
                   objPMTrx.EnumCPOStorageStatus.Active & "|" & _
                   txtCreateDate.Text
        Try
            intErrNo = objPMTrx.mtdUpdCPOStorage(strOppCd_ADD, _
                                                strOppCd_UPD, _
                                                strOppCd_GET, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                blnDupKey, _
                                                blnUpdate.text)


        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_CPOSTORAGE_UPD&errmesg=" & lblErrMessage.Text & "&redirect=PM/Trx/PM_Trx_CPOStorage_List.aspx")
        End Try
        
        If txtDate.Enabled = True And blnDupKey Then
            lblDupMsg.Visible = True
        Else
            Response.Redirect("PM_Trx_CPOStorage_List.aspx")
        End If
        
    End Sub
    
    Sub btnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strParam As String
        Dim strDate As String = CheckDate()

        strParam = strLocation & "|" & strDate & "|" & lblStorageAreaCode.Text
        Try
            intErrNo = objPMTrx.mtdPhysicalDelete(strOppCd_DEL, _
                                                  strParam, _
                                                  objPMTrx.EnumTransType.CPOStorage)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_CPOSTORAGE_DEL&errmesg=" & lblErrMessage.Text & "&redirect=PM/Trx/PM_Trx_CPOStorage_List.aspx")
        End Try
        
        Response.Redirect("PM_Trx_CPOStorage_List.aspx")
    End Sub    

    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PM_Trx_CPOStorage_List.aspx")
    End Sub
    
    Protected Function CheckDate() As String
        Dim strDateSetting As String
        Dim objSysCfgDs As DataSet
        Dim objDateFormat As String
        Dim strValidDate As String
        If Not txtDate.Text = "" Then
            If objGlobal.mtdValidInputDate(strDateFormat, txtdate.Text, objDateFormat, strValidDate) = True Then
                lblDate.Visible = False
                lblFmt.Visible = False
                Return strValidDate
            Else
                lblFmt.Text = objDateFormat & "."
                lblDate.Visible = True
                lblFmt.Visible = True
                Return ""
            End If
        End If
    End Function

    Sub CalculateCPOWeight(ByVal Sender As Object, ByVal E As EventArgs) Handles btnCalculateCPOWeight.Click
        Dim strOpCode_FML As String = "PM_CLSSETUP_STORAGEAREAFML_GET"
        Dim strOpCode_UVC As String = "PM_CLSSETUP_UVCONVERSION_GET"
        Dim strOpCode_UACC As String = "PM_CLSSETUP_UACAPACITYCONVERSION_GET"
        Dim strOpCode_CPO As String = "PM_CLSSETUP_CPOPROPERTIES_GET"
        Dim strStorageArea As String
        Dim strTemperature As String
        Dim strCPOUllage As String
        Dim dblCPOWeight As Double
        Dim strErrMsg As String
        Dim blnCreateLogRecord As Boolean = False

        strStorageArea = Trim(lstStorageArea.SelectedItem.Value)
        strTemperature = Trim(txtTemperature.Text)
        strCPOUllage = Trim(txtCPOUllage.Text)
        
        Try
            intErrNo = objPMTrx.mtdCalculateTotalWeight(strOpCode_FML, _
                                                        strOpCode_UVC, _
                                                        strOpCode_UACC, _
                                                        strOpCode_CPO, _
                                                        Val(strTemperature), _
                                                        Val(strCPOUllage), _
                                                        strLocation, _
                                                        strStorageArea, _
                                                        blnCreateLogRecord, _
                                                        dblCPOWeight, _
                                                        strErrMsg)


        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSSETUP_ACCEPTABLEOILQUALITY&errmesg=" & lblErrMessage.Text & "&redirect=PM/Trx/PM_Trx_CPOStorage_List.aspx")
        End Try

        If intErrNo <> 0 then
            If strErrMsg = "" Then
                lblCalcErr.Text = "Calculation Error"
            Else
                lblCalcErr.Text = strErrMsg
            End If
            lblCalcErr.Visible = True
        Else
            lblCalcErr.Visible = False
            rfvCPOWeight.visible = False 
            txtCPOWeight.Text = FormatNumber(dblCPOWeight, 2, TriState.True, TriState.False, TriState.False)
        End If
    End Sub


End Class
