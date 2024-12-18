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
Imports System.Globalization
Imports agri.PM

Public Class PM_Setup_StorageAreaMaster_Det : Inherits Page
    Protected objPMSetup As New agri.PM.clsSetup()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Protected WithEvents tblFormula As HtmlTable
    Protected WithEvents EventData as DataGrid
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblDupMsg As Label
    Protected WithEvents lblFMLDupMsg As Label
    Protected WithEvents lblFormulaTypeMsg As Label
    Protected WithEvents lblDate As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblCreateDate As Label
    Protected WithEvents txtCreateDate As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdateBy As Label
    
    Protected WithEvents lblTempStorageTypeCode As Label
    Protected WithEvents lblStorageTypeCode As Label
    Protected WithEvents lblStorageAreaCode As Label
    Protected WithEvents lblLineNo As Label
    Protected WithEvents lblFormulaType As Label
    Protected WithEvents lblFMLCreateDate As Label
    
    Protected WithEvents txtStorageAreaCode As TextBox
    Protected WithEvents lstStorageType As DropDownList
    Protected WithEvents txtStorageNo As TextBox
    
    Protected WithEvents txtLineNo As TextBox
    Protected WithEvents lstFormulaType As DropDownList
    Protected WithEvents lstTableCode As DropDownList
    Protected WithEvents lstFirstOperandType As DropDownList
    Protected WithEvents lstFirstOperandValue As DropDownList
    Protected WithEvents txtFirstOperandValue As TextBox
    Protected WithEvents lstSecondOperandType As DropDownList
    Protected WithEvents lstSecondOperandValue As DropDownList
    Protected WithEvents lstMatchType As DropDownList
    Protected WithEvents txtSecondOperandValue As TextBox
    Protected WithEvents FMLAdd As ImageButton
    Protected WithEvents FMLSave As ImageButton
    Protected WithEvents Save As ImageButton
    Protected WithEvents Delete As ImageButton
    Protected WithEvents Undelete As ImageButton
    Protected WithEvents Back As ImageButton
    
    Protected WithEvents rfvFirstOperandValuelst As RequiredFieldValidator
    Protected WithEvents rfvFirstOperandValue As RequiredFieldValidator
    Protected WithEvents revFirstOperandValue As RegularExpressionValidator
    Protected WithEvents rfvSecondOperandValuelst As RequiredFieldValidator
    Protected WithEvents rfvMatchType As RequiredFieldValidator
    Protected WithEvents rfvSecondOperandValue As RequiredFieldValidator
    Protected WithEvents revSecondOperandValue As RegularExpressionValidator
    
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

    Dim strOppCdFMLTemp_GET As String = "PM_CLSSETUP_STORAGETYPEFML_GET"
    Dim strOppCdFMLType_GET As String = "PM_CLSSETUP_STORAGETYPEFML_GET"
    Dim strOppCd_GET As String = "PM_CLSSETUP_STORAGEAREA_GET"
    Dim strOppCd_ADD As String = "PM_CLSSETUP_STORAGEAREA_ADD"
    Dim strOppCd_UPD As String = "PM_CLSSETUP_STORAGEAREA_UPD"
    Dim strOppCdFML_GET As String = "PM_CLSSETUP_STORAGEAREAFML_GET"
    Dim strOppCdFML_ADD As String = "PM_CLSSETUP_STORAGEAREAFML_ADD"
    Dim strOppCdFML_UPD As String = "PM_CLSSETUP_STORAGEAREAFML_UPD"
    Dim strOppCdFML_DEL As String = "PM_CLSSETUP_STORAGEAREAFML_DEL"
    Dim strOppCdFML_DELALL As String = "PM_CLSSETUP_STORAGEAREAFML_DELALL"
	
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
	    
        strLocType = Session("SS_LOCTYPE")
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMStorageAreaMaster), intPMAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()

            If Not Page.IsPostBack Then
                BindDropDownList()
                BindStorageTypeList(lstStorageType, "")
                lblStorageAreaCode.Text = Trim(Request.QueryString("StorageAreaCode"))
                lblStorageTypeCode.Text = ""
                lblTempStorageTypeCode.Text = ""
                lblLineNo.Text = ""
                lblFormulaType.Text = ""
                If lblStorageAreaCode.Text = "" Then   
                    NewFormula()
                    Delete.visible = False
                    Undelete.visible = False
                Else    
                    LoadMasterData()
                    BindGrid()
                    NewFormula()
                End If
            End If
        End If
    End Sub

    Sub onload_GetLangCap()
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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSSETUP_STORAGEAREA_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_StorageTypeMaster.aspx")
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


    Sub SetFormulaVisibility()
        If lstFormulaType.SelectedIndex > 0 Then
            tblFormula.Rows(4).visible = True
            Select Case lstFormulaType.SelectedItem.Value 
                Case objPMSetup.EnumFormulaType.UllageVolumeTable, objPMSetup.EnumFormulaType.UllageAverageCapacityTable
                    tblFormula.Rows(3).visible = True
                    tblFormula.Rows(6).visible = True
                Case objPMSetup.EnumFormulaType.CPOPropertiesTable
                    tblFormula.Rows(3).visible = True
                    tblFormula.Rows(6).visible = False
                Case Else
                    tblFormula.Rows(3).visible = False
                    tblFormula.Rows(6).visible = True
            End Select
        Else
            tblFormula.Rows(3).visible = False
            tblFormula.Rows(4).visible = False
            tblFormula.Rows(6).visible = False
        End If
        
        If tblFormula.Rows(4).visible = True Then
            If lstFirstOperandType.SelectedIndex > 0 Then
                Select Case lstFirstOperandType.SelectedItem.Value 
                    Case objPMSetup.EnumOperandType.GivenUllage
                        tblFormula.Rows(5).visible = False
                    Case objPMSetup.EnumOperandType.MatchedUllage
                        tblFormula.Rows(5).visible = True
                        lstFirstOperandValue.visible = True
                        txtFirstOperandValue.visible = False
                    Case objPMSetup.EnumOperandType.GivenTemperature
                        tblFormula.Rows(5).visible = False
                    Case objPMSetup.EnumOperandType.LineResult
                        tblFormula.Rows(5).visible = True
                        lstFirstOperandValue.visible = True
                        txtFirstOperandValue.visible = False
                    Case objPMSetup.EnumOperandType.Value
                        tblFormula.Rows(5).visible = True
                        lstFirstOperandValue.visible = False
                        txtFirstOperandValue.visible = True
                    Case Else
                End Select
                If tblFormula.Rows(5).visible = True Then
                    rfvFirstOperandValuelst.Enabled = lstFirstOperandValue.visible
                    rfvFirstOperandValue.Enabled = txtFirstOperandValue.visible
                    revFirstOperandValue.Enabled = txtFirstOperandValue.visible
                End If
            Else
                tblFormula.Rows(5).visible = False
            End If
        Else
            tblFormula.Rows(5).visible = False
        End If
        
        If tblFormula.Rows(6).visible = True Then
            If lstSecondOperandType.SelectedIndex > 0 Then
                Select Case lstSecondOperandType.SelectedItem.Value 
                    Case objPMSetup.EnumOperandType.GivenUllage
                        tblFormula.Rows(7).visible = False
                    Case objPMSetup.EnumOperandType.MatchedUllage
                        tblFormula.Rows(7).visible = True
                        lstSecondOperandValue.visible = True
                        lstMatchType.visible = False
                        txtSecondOperandValue.visible = False
                    Case objPMSetup.EnumOperandType.GivenTemperature
                        tblFormula.Rows(7).visible = False
                    Case objPMSetup.EnumOperandType.LineResult
                        tblFormula.Rows(7).visible = True
                        lstSecondOperandValue.visible = True
                        lstMatchType.visible = False
                        txtSecondOperandValue.visible = False
                    Case objPMSetup.EnumOperandType.Value
                        tblFormula.Rows(7).visible = True
                        lstSecondOperandValue.visible = False
                        lstMatchType.visible = False
                        txtSecondOperandValue.visible = True
                    Case objPMSetup.EnumOperandType.MatchType
                        tblFormula.Rows(7).visible = True
                        lstSecondOperandValue.visible = False
                        lstMatchType.visible = True
                        txtSecondOperandValue.visible = False
                    Case Else
                End Select
                If tblFormula.Rows(7).visible = True Then
                    rfvSecondOperandValuelst.Enabled = lstSecondOperandValue.visible
                    rfvMatchType.Enabled = lstMatchType.visible
                    rfvSecondOperandValue.Enabled = txtSecondOperandValue.visible
                    revSecondOperandValue.Enabled = txtSecondOperandValue.visible
                End If
            Else
                tblFormula.Rows(7).visible = False
            End If
        Else
            tblFormula.Rows(7).visible = False
        End If
    End Sub
    
    Sub BindDropDownList()
        lstFormulaType.Items.Add(New ListItem("Please select formula type", ""))
        lstFormulaType.Items.Add(New ListItem(objPMSetup.mtdGetFormulaType(objPMSetup.EnumFormulaType.UllageVolumeTable), objPMSetup.EnumFormulaType.UllageVolumeTable))
        lstFormulaType.Items.Add(New ListItem(objPMSetup.mtdGetFormulaType(objPMSetup.EnumFormulaType.UllageAverageCapacityTable), objPMSetup.EnumFormulaType.UllageAverageCapacityTable))
        lstFormulaType.Items.Add(New ListItem(objPMSetup.mtdGetFormulaType(objPMSetup.EnumFormulaType.CPOPropertiesTable), objPMSetup.EnumFormulaType.CPOPropertiesTable))
        lstFormulaType.Items.Add(New ListItem(objPMSetup.mtdGetFormulaType(objPMSetup.EnumFormulaType.Addition), objPMSetup.EnumFormulaType.Addition))
        lstFormulaType.Items.Add(New ListItem(objPMSetup.mtdGetFormulaType(objPMSetup.EnumFormulaType.Subtraction), objPMSetup.EnumFormulaType.Subtraction))
        lstFormulaType.Items.Add(New ListItem(objPMSetup.mtdGetFormulaType(objPMSetup.EnumFormulaType.Multiplication), objPMSetup.EnumFormulaType.Multiplication))
        lstFormulaType.Items.Add(New ListItem(objPMSetup.mtdGetFormulaType(objPMSetup.EnumFormulaType.Division), objPMSetup.EnumFormulaType.Division))
        
        lstMatchType.Items.Add(New ListItem("Please select match type", ""))
        lstMatchType.Items.Add(New ListItem(objPMSetup.mtdGetMatchType(objPMSetup.EnumMatchType.TheNextSmaller), objPMSetup.EnumMatchType.TheNextSmaller))
        lstMatchType.Items.Add(New ListItem(objPMSetup.mtdGetMatchType(objPMSetup.EnumMatchType.TheNextLarger), objPMSetup.EnumMatchType.TheNextLarger))
        
        BindOperandTypeList()
    End Sub
    
    Sub BindOperandTypeList()
        Dim I As Integer
        For I = 1 To lstFirstOperandType.Items.Count
            lstFirstOperandType.Items.RemoveAt(0)
        Next
        For I = 1 To lstSecondOperandType.Items.Count
            lstSecondOperandType.Items.RemoveAt(0)
        Next
        lstFirstOperandType.Items.Add(New ListItem("Please select first operand type",""))

        If lstFormulaType.SelectedIndex > 0 Then
            If lstFormulaType.SelectedItem.Value <> objPMSetup.EnumFormulaType.CPOPropertiesTable Then
                lstFirstOperandType.Items.Add(New ListItem(objPMSetup.mtdGetOperandType(objPMSetup.EnumOperandType.GivenUllage), objPMSetup.EnumOperandType.GivenUllage))
                lstFirstOperandType.Items.Add(New ListItem(objPMSetup.mtdGetOperandType(objPMSetup.EnumOperandType.MatchedUllage), objPMSetup.EnumOperandType.MatchedUllage))
            End If
            
            Select Case lstFormulaType.SelectedItem.Value
                Case objPMSetup.EnumFormulaType.CPOPropertiesTable, _
                     objPMSetup.EnumFormulaType.Addition, _
                     objPMSetup.EnumFormulaType.Subtraction, _
                     objPMSetup.EnumFormulaType.Multiplication, _
                     objPMSetup.EnumFormulaType.Division 

                    lstFirstOperandType.Items.Add(New ListItem(objPMSetup.mtdGetOperandType(objPMSetup.EnumOperandType.GivenTemperature), objPMSetup.EnumOperandType.GivenTemperature))
            End Select
            lstFirstOperandType.Items.Add(New ListItem(objPMSetup.mtdGetOperandType(objPMSetup.EnumOperandType.LineResult), objPMSetup.EnumOperandType.LineResult))
            lstFirstOperandType.Items.Add(New ListItem(objPMSetup.mtdGetOperandType(objPMSetup.EnumOperandType.Value), objPMSetup.EnumOperandType.Value))
            
            lstSecondOperandType.Items.Add(New ListItem("Please select second operand type",""))
            Select Case lstFormulaType.SelectedItem.Value
                Case objPMSetup.EnumFormulaType.UllageVolumeTable
                    lstSecondOperandType.Items.Add(New ListItem(objPMSetup.mtdGetOperandType(objPMSetup.EnumOperandType.MatchType), objPMSetup.EnumOperandType.MatchType))
                    
                Case objPMSetup.EnumFormulaType.UllageAverageCapacityTable
                    lstSecondOperandType.Items.Add(New ListItem(objPMSetup.mtdGetOperandType(objPMSetup.EnumOperandType.GivenUllage), objPMSetup.EnumOperandType.GivenUllage))
                    lstSecondOperandType.Items.Add(New ListItem(objPMSetup.mtdGetOperandType(objPMSetup.EnumOperandType.MatchedUllage), objPMSetup.EnumOperandType.MatchedUllage))
                    lstSecondOperandType.Items.Add(New ListItem(objPMSetup.mtdGetOperandType(objPMSetup.EnumOperandType.LineResult), objPMSetup.EnumOperandType.LineResult))
                    lstSecondOperandType.Items.Add(New ListItem(objPMSetup.mtdGetOperandType(objPMSetup.EnumOperandType.Value), objPMSetup.EnumOperandType.Value))

                Case objPMSetup.EnumFormulaType.Addition, _
                     objPMSetup.EnumFormulaType.Subtraction, _
                     objPMSetup.EnumFormulaType.Multiplication, _
                     objPMSetup.EnumFormulaType.Division 
                    lstSecondOperandType.Items.Add(New ListItem(objPMSetup.mtdGetOperandType(objPMSetup.EnumOperandType.GivenUllage), objPMSetup.EnumOperandType.GivenUllage))
                    lstSecondOperandType.Items.Add(New ListItem(objPMSetup.mtdGetOperandType(objPMSetup.EnumOperandType.MatchedUllage), objPMSetup.EnumOperandType.MatchedUllage))
                    lstSecondOperandType.Items.Add(New ListItem(objPMSetup.mtdGetOperandType(objPMSetup.EnumOperandType.GivenTemperature), objPMSetup.EnumOperandType.GivenTemperature))
                    lstSecondOperandType.Items.Add(New ListItem(objPMSetup.mtdGetOperandType(objPMSetup.EnumOperandType.LineResult), objPMSetup.EnumOperandType.LineResult))
                    lstSecondOperandType.Items.Add(New ListItem(objPMSetup.mtdGetOperandType(objPMSetup.EnumOperandType.Value), objPMSetup.EnumOperandType.Value))
            End Select
        End If
    End Sub 
    
    Sub BindStorageTypeList(ByRef lstStorageType As DropDownList, ByVal StorageType As String)
        Dim strOpCdTableCode_Get As String = "PM_CLSSETUP_STORAGETYPE_GET"
        Dim dsForDropDown As DataSet
        Dim SelectedIndex As Integer = -1
        Dim intCnt As Integer
        Dim TblAlias As String
        Dim DataTextField As String
        Dim SearchStr As String
        Dim strParam As String
        Dim drinsert As DataRow
        SearchStr = " AND ST.Status = '" & objPMSetup.EnumStorageTypeStatus.Active & "'"

        strParam = "ORDER BY ST.StorageTypeCode ASC|" & SearchStr

        Try
            intErrNo = objPMSetup.mtdGetStorageType(strOpCdTableCode_Get, strParam, dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSSETUP_STORAGEAREA_BINDSTORAGETYPELIST&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_StorageTypeMaster.aspx")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item(0) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0))
            dsForDropDown.Tables(0).Rows(intCnt).Item(1) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0)) & " (" & Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(1)) & ")"
            If Not StorageType = "" Then
                If UCase(Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0))) = UCase(Trim(StorageType)) Then
                    SelectedIndex = intCnt + 1
                End If
            End If
        Next intCnt

        drinsert = dsForDropDown.Tables(0).NewRow()
        drinsert(0) = ""
        drinsert(1) = "Please select a storage type"
        dsForDropDown.Tables(0).Rows.InsertAt(drinsert, 0)

        lstStorageType.DataSource = dsForDropDown.Tables(0)
        lstStorageType.DataValueField = "StorageTypeCode"
        lstStorageType.DataTextField = "Description"
        lstStorageType.DataBind()

        If Not StorageType = "" Then
            If SelectedIndex = -1 Then
                lstStorageType.Items.Add(New ListItem(Trim(StorageType), Trim(StorageType)))
                SelectedIndex = lstStorageType.Items.Count - 1
            End If
            lstStorageType.SelectedIndex = SelectedIndex
        End If

        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If
    End Sub    

    Sub BindTableCodeList(ByRef lstTableCode As DropDownList, ByVal TableCode As String)
        Dim strOpCdTableCode_Get As String = "PM_CLSSETUP_UVTABLE_GET"
        Dim dsForDropDown As DataSet
        Dim SelectedIndex As Integer = -1
        Dim intCnt As Integer
        Dim TblAlias As String
        Dim DataTextField As String
        Dim SearchStr As String
        Dim strParam As String
        Dim drinsert As DataRow

        SearchStr = " AND UVT.Status = '" & objPMSetup.EnumUllageVolumeTableStatus.Active & "'"

        strParam = "ORDER BY UVT.UVTableCode asc|" & SearchStr

        Try
            intErrNo = objPMSetup.mtdGetUllageVolumeTable(strOpCdTableCode_Get, strParam, dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSSETUP_STORAGEAREA_BINDTABLECODELIST&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_StorageTypeMaster.aspx")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item(0) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0))
            dsForDropDown.Tables(0).Rows(intCnt).Item(1) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0))
            If Not TableCode = "" Then
                If UCase(Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0))) = UCase(Trim(TableCode)) Then
                    SelectedIndex = intCnt + 1
                End If
            End If
        Next intCnt

        drinsert = dsForDropDown.Tables(0).NewRow()
        drinsert(0) = ""
        drinsert(1) = "Please select a code "
        dsForDropDown.Tables(0).Rows.InsertAt(drinsert, 0)

        lstTableCode.DataSource = dsForDropDown.Tables(0)
        lstTableCode.DataValueField = "UVTableCode"
        lstTableCode.DataTextField = "Description"
        lstTableCode.DataBind()

        If Not TableCode = "" Then
            If SelectedIndex = -1 Then
                lstTableCode.Items.Add(New ListItem(Trim(TableCode), Trim(TableCode)))
                SelectedIndex = lstTableCode.Items.Count - 1
            End If
            lstTableCode.SelectedIndex = SelectedIndex
        End If

        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If
    End Sub
    
    Sub BindLineNoList(ByRef lstLineNo As DropDownList, ByVal LineNo As String)
        Dim dsForDropDown As DataSet
        Dim SelectedIndex As Integer = -1
        Dim intCnt As Integer
        Dim TblAlias As String
        Dim DataTextField As String
        Dim SearchStr As String
        Dim strParam As String
        Dim drinsert As DataRow

        If Trim(lblTempStorageTypeCode.Text) = "" Then
            SearchStr = " AND SAF.LocCode='" & strLocation & "' AND SAF.StorageAreaCode = '" & lblStorageAreaCode.Text & "'"

            strParam = "ORDER BY SAF.LnNo ASC|" & SearchStr

            Try
                intErrNo = objPMSetup.mtdGetStorageAreaFormula(strOppCdFML_GET, strParam, dsForDropDown)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSSETUP_STORAGEAREA_BINDAREALINENO&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_StorageTypeMaster.aspx")
            End Try

            For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
                dsForDropDown.Tables(0).Rows(intCnt).Item(1) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(2))
                dsForDropDown.Tables(0).Rows(intCnt).Item(3) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(2))
                If Not LineNo = "" Then
                    If Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(2)) = Trim(LineNo) Then
                        SelectedIndex = intCnt + 1
                    End If
                End If
            Next intCnt

            drinsert = dsForDropDown.Tables(0).NewRow()
            drinsert(3) = ""
            drinsert(1) = "Please select a line"
            dsForDropDown.Tables(0).Rows.InsertAt(drinsert, 0)

            lstLineNo.DataSource = dsForDropDown.Tables(0)
            lstLineNo.DataValueField = "FormulaType"
            lstLineNo.DataTextField = "StorageAreaCode"
            lstLineNo.DataBind()
        Else    
            SearchStr = " AND STF.StorageTypeCode = '" & Trim(lblTempStorageTypeCode.Text) & "'"

            strParam = "ORDER BY STF.LnNo ASC|" & SearchStr

            Try
                intErrNo = objPMSetup.mtdGetStorageTypeFormula(strOppCdFMLType_GET, strParam, dsForDropDown)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSSETUP_STORAGEAREA_BINDTYPELINENO&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_StorageTypeMaster.aspx")
            End Try

            For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
                dsForDropDown.Tables(0).Rows(intCnt).Item(0) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(1))
                dsForDropDown.Tables(0).Rows(intCnt).Item(2) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(1))
                If Not LineNo = "" Then
                    If Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(2)) = Trim(LineNo) Then
                        SelectedIndex = intCnt + 1
                    End If
                End If
            Next intCnt

            drinsert = dsForDropDown.Tables(0).NewRow()
            drinsert(2) = ""
            drinsert(0) = "Please select a line"
            dsForDropDown.Tables(0).Rows.InsertAt(drinsert, 0)

            lstLineNo.DataSource = dsForDropDown.Tables(0)
            lstLineNo.DataValueField = "FormulaType"
            lstLineNo.DataTextField = "StorageTypeCode"
            lstLineNo.DataBind()
        End If
        If Not LineNo = "" Then
            If SelectedIndex = -1 Then
                lstLineNo.Items.Add(New ListItem(Trim(LineNo), Trim(LineNo)))
                SelectedIndex = lstLineNo.Items.Count - 1
            End If
            lstLineNo.SelectedIndex = SelectedIndex
        End If

        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If
    End Sub
    
    Sub LoadMasterData()
        Dim I As Integer
        strParam = "|AND SA.LocCode='" & strLocation & "' AND SA.StorageAreaCode='" & lblStorageAreaCode.Text & "'"
        Try
            intErrNo = objPMSetup.mtdGetStorageArea(strOppCd_GET, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSSETUP_STORAGEAREA_LOADMASTERDATA&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_StorageTypeMaster.aspx")
        End Try
        If objDataSet.Tables(0).Rows.Count > 0 Then
            txtStorageAreaCode.Text = Trim(objDataSet.Tables(0).Rows(0).Item("StorageAreaCode"))
            For I = 0 To lstStorageType.Items.Count - 1
                If lstStorageType.Items.Item(I).value = Trim(objDataSet.Tables(0).Rows(0).Item("StorageTypeCode")) Then
                    lstStorageType.selectedIndex = I
                End If
            Next
            txtStorageNo.Text = Trim(objDataSet.Tables(0).Rows(0).Item("StorageNo"))
            
            txtStorageAreaCode.Enabled = False
            lblStorageTypeCode.Text = Trim(objDataSet.Tables(0).Rows(0).Item("StorageTypeCode"))
            lblStatus.Text = objPMSetup.mtdGetStorageTypeStatus(Trim(objDataSet.Tables(0).Rows(0).Item("Status")))
            lblCreateDate.Text = objGlobal.GetLongDate(Trim(objDataSet.Tables(0).Rows(0).Item("CreateDate")))
            txtCreateDate.Text = Trim(objDataSet.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(Trim(objDataSet.Tables(0).Rows(0).Item("UpdateDate")))
            lblUpdateBy.Text = Trim(objDataSet.Tables(0).Rows(0).Item("Username"))
            
            If Trim(objDataSet.Tables(0).Rows(0).Item("Status")) = objPMsetup.EnumStorageAreaStatus.Active Then
                lstStorageType.Enabled = True
                txtStorageNo.Enabled = True
                Save.visible = True
                Delete.visible = True
                Undelete.visible = False
                FMLAdd.Enabled = True
                FMLSave.Enabled = True
                txtLineNo.Enabled = True
                lstFormulaType.Enabled = True
            Else
                NewFormula()
                lstStorageType.Enabled = False
                txtStorageNo.Enabled = False
                Save.visible = False
                Delete.visible = False
                Undelete.visible = True
                FMLAdd.Enabled = False
                FMLSave.Enabled = False
                txtLineNo.Enabled = False
                lstFormulaType.Enabled = False
            End If
        Else
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSSETUP_STORAGEAREA_MASTERDATA_NOTFOUND&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_StorageTypeMaster.aspx")
        End If
    End Sub

    Public Sub LoadItemData()
        Dim I As Integer
        
        If Trim(lblTempStorageTypeCode.Text) = "" Then
            strParam = "| AND SAF.LocCode='" & strLocation & "' AND SAF.StorageAreaCode='" & lblStorageAreaCode.Text & "' AND SAF.LnNo=" & lblLineNo.text
            Try
                intErrNo = objPMSetup.mtdGetStorageAreaFormula(strOppCdFML_GET, strParam, objDataSet)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSSETUP_STORAGEAREA_LOADITEMDATA&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_StorageTypeMaster.aspx")
            End Try
        Else
            strParam = "| AND STF.StorageTypeCode='" & lblTempStorageTypeCode.Text & "' AND STF.LnNo=" & lblLineNo.Text
            Try
                intErrNo = objPMSetup.mtdGetStorageTypeFormula(strOppCdFMLTemp_GET, strParam, objDataSet)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSSETUP_STORAGEAREA_LOADTEMPGRIDDATA&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_StorageTypeMaster.aspx")
            End Try
        End If
        
        If objDataSet.Tables(0).Rows.Count > 0 Then
            lblFormulaTypeMsg.visible = False
            lblFMLDupMsg.visible = False
            txtLineNo.Text = Trim(objDataSet.Tables(0).Rows(0).Item("LnNo"))
            For I = 1 To lstFormulaType.Items.Count - 1
                If Trim(lstFormulaType.Items.Item(I).value) = Trim(objDataSet.Tables(0).Rows(0).Item("FormulaType")) Then
                    lstFormulaType.selectedIndex = I
                End If
            Next
            BindOperandTypeList()
            For I = 1 To lstFirstOperandType.Items.Count - 1
                If Trim(lstFirstOperandType.Items.Item(I).value) = Trim(objDataSet.Tables(0).Rows(0).Item("OperandType1")) Then
                    lstFirstOperandType.selectedIndex = I
                End If
            Next
            Select Case Trim(objDataSet.Tables(0).Rows(0).Item("FormulaType"))
                Case objPMSetup.EnumFormulaType.UllageVolumeTable, _
                     objPMSetup.EnumFormulaType.UllageAverageCapacityTable, _
                     objPMSetup.EnumFormulaType.CPOPropertiesTable
                    BindTableCodeList(lstTableCode, Trim(objDataSet.Tables(0).Rows(0).Item("TableCode")))
                    For I = 1 To lstSecondOperandType.Items.Count - 1
                        If Trim(lstSecondOperandType.Items.Item(I).value) = Trim(objDataSet.Tables(0).Rows(0).Item("OperandType2")) Then
                            lstSecondOperandType.selectedIndex = I
                        End If
                    Next
                Case Else
                    For I = 1 To lstSecondOperandType.Items.Count - 1
                        If Trim(lstSecondOperandType.Items.Item(I).value) = Trim(objDataSet.Tables(0).Rows(0).Item("OperandType2")) Then
                            lstSecondOperandType.selectedIndex = I
                        End If
                    Next
            End Select
            Select Case Trim(objDataSet.Tables(0).Rows(0).Item("OperandType1"))
                Case objPMSetup.EnumOperandType.GivenUllage
                    txtFirstOperandValue.Text = "0"
                Case objPMSetup.EnumOperandType.MatchedUllage
                    BindLineNoList(lstFirstOperandValue, CDbl(objDataSet.Tables(0).Rows(0).Item("OperandValue1")))
                    txtFirstOperandValue.Text = "0"
                Case objPMSetup.EnumOperandType.GivenTemperature
                    txtFirstOperandValue.Text = "0"
                Case objPMSetup.EnumOperandType.LineResult
                    BindLineNoList(lstFirstOperandValue, CDbl(objDataSet.Tables(0).Rows(0).Item("OperandValue1")))
                    txtFirstOperandValue.Text = "0"
                Case objPMSetup.EnumOperandType.Value
                    txtFirstOperandValue.Text = CDbl(objDataSet.Tables(0).Rows(0).Item("OperandValue1"))
            End Select
            Select Case Trim(objDataSet.Tables(0).Rows(0).Item("OperandType2"))
                Case objPMSetup.EnumOperandType.GivenUllage
                    txtSecondOperandValue.Text = "0"
                Case objPMSetup.EnumOperandType.MatchedUllage
                    BindLineNoList(lstSecondOperandValue, CDbl(objDataSet.Tables(0).Rows(0).Item("OperandValue2")))
                    txtSecondOperandValue.Text = "0"
                Case objPMSetup.EnumOperandType.GivenTemperature
                    txtSecondOperandValue.Text = "0"
                Case objPMSetup.EnumOperandType.LineResult
                    BindLineNoList(lstSecondOperandValue, CDbl(objDataSet.Tables(0).Rows(0).Item("OperandValue2")))
                    txtSecondOperandValue.Text = "0"
                Case objPMSetup.EnumOperandType.Value
                    txtSecondOperandValue.Text = CDbl(objDataSet.Tables(0).Rows(0).Item("OperandValue2"))
                Case objPMSetup.EnumOperandType.MatchType
                    For I = 1 To lstMatchType.Items.Count - 1
                        If CInt(lstMatchType.Items.Item(I).value) = CDbl(objDataSet.Tables(0).Rows(0).Item("OperandValue2")) Then
                            lstMatchType.selectedIndex = I
                        End If
                    Next
                    txtSecondOperandValue.Text = "0"
            End Select
            SetFormulaVisibility()
            lblLineNo.Text = Trim(objDataSet.Tables(0).Rows(0).Item("LnNo"))
            lblFormulaType.Text = Trim(objDataSet.Tables(0).Rows(0).Item("FormulaType"))
            lblFMLCreateDate.Text = objDataSet.Tables(0).Rows(0).Item("CreateDate")
            FMLAdd.visible = False
            FMLSave.visible = True
        Else
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSSETUP_STORAGEAREA_ITEMDATA_NOTFOUND&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_StorageTypeMaster.aspx")
        End If
    End Sub
    
    Protected Function LoadGridData() As DataSet
        strParam = "ORDER BY LnNo ASC|AND SAF.LocCode='" & strLocation & "' AND SAF.StorageAreaCode='" & lblStorageAreaCode.Text & "'"
        Try
            intErrNo = objPMSetup.mtdGetStorageAreaFormula(strOppCdFML_GET, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSSETUP_STORAGEAREA_LOADGRIDDATA&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_StorageTypeMaster.aspx")
        End Try

        Return objDataSet
    End Function
    
    Protected Function LoadTempGridData() As DataSet
        strParam = "ORDER BY LnNo ASC|AND STF.StorageTypeCode='" & lblTempStorageTypeCode.Text & "'"
        Try
            intErrNo = objPMSetup.mtdGetStorageTypeFormula(strOppCdFMLTemp_GET, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSSETUP_STORAGEAREA_LOADTEMPGRIDDATA&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_StorageTypeMaster.aspx")
        End Try

        Return objDataSet
    End Function
    
    Sub BindTempGrid() 
        EventData.DataSource = LoadTempGridData
        EventData.DataBind()
    End Sub 
    
    Sub BindGrid() 
        EventData.DataSource = LoadGridData
        EventData.DataBind()
    End Sub 
    
    Sub DataGrid_ItemDataCreated(ByVal Sender As Object, ByVal e As DataGridItemEventArgs) Handles EventData.ItemDataBound
        Dim lbl As Label
        Dim lbl1 As Label
        Dim lbl2 As Label
        Dim Updbutton As LinkButton
        Dim strFormulaType As String
        Dim strTableCode As String
        Dim strOperandType1 As String
        Dim strOperandValue1 As String
        Dim strOperandType2 As String
        Dim strOperandValue2 As String
        
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            lbl = e.Item.FindControl("FMLFormulaType")
            lbl2 = e.Item.FindControl("FMLTableCode")
            strFormulaType = objPMSetup.mtdGetFormulaType(Trim(lbl.Text))
            Select Case Trim(lbl.Text)
                Case objPMSetup.EnumFormulaType.UllageVolumeTable, _
                     objPMSetup.EnumFormulaType.UllageAverageCapacityTable, _
                     objPMSetup.EnumFormulaType.CPOPropertiesTable
                    strTableCode = Trim(lbl2.Text)
                Case Else
                    strTableCode = ""
            End Select
            
            lbl = e.Item.FindControl("FMLOperandType1")
            lbl2 = e.Item.FindControl("FMLOperandValue1")
            strOperandType1 = objPMSetup.mtdGetOperandType(lbl.Text)
            Select Case Trim(lbl.Text)
                Case objPMSetup.EnumOperandType.MatchedUllage, objPMSetup.EnumOperandType.LineResult, objPMSetup.EnumOperandType.Value
                    strOperandValue1 = CDbl(lbl2.Text)
                Case Else
                    strOperandValue1 = ""
            End Select
            
            lbl = e.Item.FindControl("FMLOperandType2")
            lbl1 = e.Item.FindControl("FMLFormulaType")
            lbl2 = e.Item.FindControl("FMLOperandValue2")
            Select Case Trim(lbl1.Text)
                Case objPMSetup.EnumFormulaType.CPOPropertiesTable
                    strOperandType2 = ""
                    strOperandValue2 = ""
                Case Else
                    strOperandType2 = objPMSetup.mtdGetOperandType(lbl.Text)
                    Select Case Trim(lbl.Text)
                        Case objPMSetup.EnumOperandType.MatchedUllage, objPMSetup.EnumOperandType.LineResult, objPMSetup.EnumOperandType.Value
                            strOperandValue2 = CDbl(lbl2.Text)
                        Case objPMSetup.EnumOperandType.MatchType
                            strOperandValue2 = objPMSetup.mtdGetMatchType(Trim(lbl2.Text))
                        Case Else
                            strOperandValue2 = ""
                    End Select
            End Select
            lbl = e.Item.FindControl("FMLFormulaType")
            lbl.Text = strFormulaType
            lbl = e.Item.FindControl("FMLTableCode")
            lbl.Text = strTableCode
            lbl = e.Item.FindControl("FMLOperandType1")
            lbl.Text = strOperandType1
            lbl = e.Item.FindControl("FMLOperandValue1")
            if strOperandType1 = objPMSetup.EnumOperandType.Value.tostring() then
                lbl.Text =objGlobal.GetIDDecimalSeparator_FreeDigit(cdec(strOperandValue1), 10)
            else
                lbl.Text = strOperandValue1
            end if
            lbl = e.Item.FindControl("FMLOperandType2")
            lbl.Text = strOperandType2
            lbl = e.Item.FindControl("FMLOperandValue2")
            if strOperandType2 = objPMSetup.EnumOperandType.Value.tostring() then
                lbl.Text =objGlobal.GetIDDecimalSeparator_FreeDigit(cdec(strOperandValue2), 10)
            else
                lbl.Text = strOperandValue2
            end if
            If Undelete.visible = False Then
                Updbutton = e.Item.FindControl("DELETE")
                Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Else
                Updbutton = e.Item.FindControl("EDIT")
                Updbutton.visible = False
                Updbutton = e.Item.FindControl("DELETE")
                Updbutton.visible = False
            End If
        End If
    End Sub

    Sub SaveMasterData()
        Dim blnDupKey As Boolean = False

        If lblStorageAreaCode.Text <> "" Then
            blnUpdate.text = True
        Else
            blnUpdate.text = False
        End If
        
        strParam = txtStorageAreaCode.Text.Trim & "|" & _
                   lstStorageType.SelectedItem.Value & "|" & _
                   txtStorageNo.Text.Trim & "|" & _
                   objPMSetup.EnumStorageTypeStatus.Active & "|" & _
                   txtCreateDate.Text
        
        Try
            intErrNo = objPMSetup.mtdUpdStorageArea(strOppCd_ADD, _
                                                strOppCd_UPD, _
                                                strOppCd_GET, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                blnDupKey, _
                                                blnUpdate.text)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSSETUP_STORAGEAREA_SAVEMASTERDATA&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_StorageTypeMaster.aspx")
        End Try
        
        If blnDupKey Then
            lblDupMsg.Visible = True
        Else
            lblStorageAreaCode.Text = txtStorageAreaCode.Text.Trim
            lblDupMsg.visible = False
            txtStorageAreaCode.readonly = True
            LoadMasterData()
        End If
    End Sub

    Sub CloneFormula()
        Dim blnDupKey As Boolean = False
        Dim I As Integer
        Dim objSource As DataSet
        
        If Trim(lblTempStorageTypeCode.Text) = "" Then  
            Exit Sub
        End If
        
        strParam =  lblStorageAreaCode.Text & "|"
        Try
        intErrNo = objPMSetup.mtdDelStorageAreaFormulaAll(strOppCdFML_DELALL, strLocation, strParam)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSSETUP_STORAGEAREA_DELETEEXISTING&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_StorageAreaMaster.aspx")
        End Try
        
        strParam = "| AND STF.StorageTypeCode='" & lblTempStorageTypeCode.Text & "'"
        Try
            intErrNo = objPMSetup.mtdGetStorageType(strOppCdFMLType_GET, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSSETUP_STORAGEAREA_GETTYPEDATA&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_StorageAreaMaster.aspx")
        End Try
        For I = 0 To objDataSet.Tables(0).Rows.Count - 1
            strParam = Trim(lblStorageAreaCode.Text) & "|" & _
                       (objDataSet.Tables(0).Rows(I).Item("LnNo")) & "|" & _
                       Trim(objDataSet.Tables(0).Rows(I).Item("FormulaType")) & "|" & _
                       Trim(objDataSet.Tables(0).Rows(I).Item("TableCode")) & "|" & _
                       Trim(objDataSet.Tables(0).Rows(I).Item("OperandType1"))  & "|" & _
                       (objDataSet.Tables(0).Rows(I).Item("OperandValue1")) & "|" & _
                       Trim(objDataSet.Tables(0).Rows(I).Item("OperandType2")) & "|" & _
                       (objDataSet.Tables(0).Rows(I).Item("OperandValue2")) & "|" & _
                       objPMSetup.EnumStorageAreaFormulaStatus.Active & "|"
            
            Try
                intErrNo = objPMSetup.mtdUpdStorageAreaFormula(strOppCdFML_ADD, _
                                                    strOppCdFML_UPD, _
                                                    strOppCdFML_GET, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    blnDupKey, _
                                                    False)

            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSSETUP_STORAGEAREA_ADDTYPEDATA&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_StorageAreaMaster.aspx")
            End Try 
            If blnDupKey Then
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSSETUP_STORAGEAREA_TYPEDUPLICATE&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_StorageAreaMaster.aspx")
            End If
        Next
        lblStorageTypeCode.Text = lblTempStorageTypeCode.Text
        lblTempStorageTypeCode.Text = ""
    End Sub

    Sub NewFormula()
        Dim dsDataSet As DataSet
        dsDataSet = LoadGridData
        If dsDataSet.Tables(0).Rows.Count > 0 Then
            txtLineNo.Text = CInt(dsDataSet.Tables(0).Rows(dsDataSet.Tables(0).Rows.Count - 1).Item("LnNo")) + 7
        Else
            txtLineNo.Text = "1"
        End If
        lstFormulaType.SelectedIndex = 0
        lstMatchType.SelectedIndex = 0
        BindTableCodeList(lstTableCode, "")
        BindLineNoList(lstFirstOperandValue, "")
        BindLineNoList(lstSecondOperandValue, "")
        txtFirstOperandValue.Text = "0"
        txtSecondOperandValue.Text = "0"
        
        SetFormulaVisibility()
        FMLAdd.visible = True
        FMLSave.visible = False
        lblLineNo.Text = ""
        lblFormulaType.Text = lstFormulaType.SelectedItem.Value
        lblFormulaTypeMsg.Visible = False
        lblFMLDupMsg.visible = False
    End Sub

    Sub DEDR_Edit(Sender As Object, E As DataGridCommandEventArgs)
        Dim I As Integer
        Dim Flag As Boolean
        Dim Updbutton As LinkButton
        Dim lbl As Label
        Dim EditText As TextBox
        
        For I = 0 To EventData.Items.Count - 1
            If I = CInt(e.Item.ItemIndex) Then
                Flag = False
            Else
                Flag = True
            End If
            Updbutton = EventData.Items.Item(I).FindControl("Edit")
            Updbutton.visible = Flag
            Updbutton = EventData.Items.Item(I).FindControl("Delete")
            Updbutton.visible = Flag
            Updbutton = EventData.Items.Item(I).FindControl("Cancel")
            Updbutton.visible = Not Flag
        Next
        
        lbl = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("FMLLineNo")
        lblLineNo.Text = CDbl(lbl.Text)
        LoadItemData()
    End Sub

    Sub DEDR_Cancel(Sender As Object, E As DataGridCommandEventArgs)
        If Trim(lblTempStorageTypeCode.Text) = "" Then
            BindGrid() 
        Else
            BindTempGrid()
        End If
        lblLineNo.Text = ""
        NewFormula()
    End Sub

    Sub DEDR_Delete(Sender As Object, E As DataGridCommandEventArgs)
        Dim lbl As Label
        Dim strTParam As String
        
        lbl = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("FMLLineNo")
        
        strTParam =  lblStorageAreaCode.Text & "|" & _
                     CDbl(lbl.Text)
        
        SaveMasterData()
        If lblDupMsg.visible = True Then
            Exit Sub
        End If
        CloneFormula()

        Try
        intErrNo = objPMSetup.mtdDelStorageAreaFormula(strOppCdFML_DEL, strLocation, strtParam)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSSETUP_STORAGEAREA_DELETE&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_StorageTypeMaster.aspx")
        End Try
        
        NewFormula()
        BindGrid() 
    End Sub

    Sub btnFMLAdd_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim blnDupKey As Boolean = False
        Dim strTableCode As String
        Dim strOperandType1 As String
        Dim strOperandValue1 As String
        Dim strOperandType2 As String
        Dim strOperandValue2 As String
        
        If lstFormulaType.SelectedIndex = 0 Then    
            lblFormulaTypeMsg.Visible = True
            Exit Sub
        End If
        
        SaveMasterData()
        If lblDupMsg.visible = True Then
            Exit Sub
        End If
        CloneFormula()

        Select Case lstFormulaType.SelectedItem.Value 
            Case objPMSetup.EnumFormulaType.UllageVolumeTable, _
                 objPMSetup.EnumFormulaType.UllageAverageCapacityTable, _
                 objPMSetup.EnumFormulaType.CPOPropertiesTable
                strTableCode = lstTableCode.SelectedItem.Value
            Case Else
                strTableCode = ""
        End Select
        
        strOperandType1 = lstFirstOperandType.SelectedItem.Value
        Select Case lstFirstOperandType.SelectedItem.Value
            Case objPMSetup.EnumOperandType.MatchedUllage, objPMSetup.EnumOperandType.LineResult
                strOperandValue1 = Trim(lstFirstOperandValue.SelectedItem.Value)
            Case objPMSetup.EnumOperandType.Value
                strOperandValue1 = Trim(txtFirstOperandValue.Text)
            Case Else
                strOperandValue1 = "0"
        End Select
        
        Select Case lstFormulaType.SelectedItem.Value 
            Case objPMSetup.EnumFormulaType.CPOPropertiesTable
                strOperandType2 = "0"
                strOperandValue2 = "0"
            Case Else
                strOperandType2 = lstSecondOperandType.SelectedItem.Value
                Select Case lstSecondOperandType.SelectedItem.Value
                    Case objPMSetup.EnumOperandType.MatchedUllage, objPMSetup.EnumOperandType.LineResult
                        strOperandValue2 = Trim(lstSecondOperandValue.SelectedItem.Value)
                    Case objPMSetup.EnumOperandType.Value
                        strOperandValue2 = Trim(txtSecondOperandValue.Text)
                    Case objPMSetup.EnumOperandType.MatchType
                        strOperandValue2 = Trim(lstMatchType.SelectedItem.Value)
                    Case Else
                        strOperandValue2 = "0"
                End Select
        End Select
        
        strParam = txtStorageAreaCode.Text.Trim & "|" & _
                   txtLineNo.Text.Trim & "|" & _
                   lstFormulaType.SelectedItem.Value & "|" & _
                   strTableCode & "|" & _
                   strOperandType1 & "|" & _
                   strOperandValue1 & "|" & _
                   strOperandType2 & "|" & _
                   strOperandValue2 & "|" & _
                   objPMSetup.EnumStorageAreaStatus.Active & "|"
                   
        
        Try
            intErrNo = objPMSetup.mtdUpdStorageAreaFormula(strOppCdFML_ADD, _
                                                strOppCdFML_UPD, _
                                                strOppCdFML_GET, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                blnDupKey, _
                                                False)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSSETUP_STORAGEAREA_ADDFORMULA&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_StorageTypeMaster.aspx")
        End Try
        
        If blnDupKey Then
            lblFMLDupMsg.Visible = True
        Else
            lblFMLDupMsg.visible = False
            EventData.EditItemIndex = -1
            BindGrid() 
            NewFormula()
        End If
    End Sub
    
    Sub btnFMLSave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim blnDupKey As Boolean = False
        Dim strParamDEL As String
        Dim strParamUPD As String
        Dim strLineNo As String
        Dim strTableCode As String
        Dim strOperandType1 As String
        Dim strOperandValue1 As String
        Dim strOperandType2 As String
        Dim strOperandValue2 As String
        
        If lstFormulaType.SelectedIndex = 0 Then
            lblFormulaTypeMsg.Visible = True
            Exit Sub
        End If
        
        SaveMasterData()
        If lblDupMsg.visible = True Then
            Exit Sub
        End If
        CloneFormula()
        
        Select Case lstFormulaType.SelectedItem.Value 
            Case objPMSetup.EnumFormulaType.UllageVolumeTable, _
                 objPMSetup.EnumFormulaType.UllageAverageCapacityTable, _
                 objPMSetup.EnumFormulaType.CPOPropertiesTable
                strTableCode = lstTableCode.SelectedItem.Value
            Case Else
                strTableCode = ""
        End Select
        
        strOperandType1 = lstFirstOperandType.SelectedItem.Value
        Select Case lstFirstOperandType.SelectedItem.Value
            Case objPMSetup.EnumOperandType.MatchedUllage, objPMSetup.EnumOperandType.LineResult
                strOperandValue1 = Trim(lstFirstOperandValue.SelectedItem.Value)
            Case objPMSetup.EnumOperandType.Value
                strOperandValue1 = Trim(txtFirstOperandValue.Text)
            Case Else
                strOperandValue1 = "0"
        End Select
        
        Select Case lstFormulaType.SelectedItem.Value 
            Case objPMSetup.EnumFormulaType.CPOPropertiesTable
                strOperandType2 = "0"
                strOperandValue2 = "0"
            Case Else
                strOperandType2 = lstSecondOperandType.SelectedItem.Value
                Select Case lstSecondOperandType.SelectedItem.Value
                    Case objPMSetup.EnumOperandType.MatchedUllage, objPMSetup.EnumOperandType.LineResult
                        strOperandValue2 = Trim(lstSecondOperandValue.SelectedItem.Value)
                    Case objPMSetup.EnumOperandType.Value
                        strOperandValue2 = Trim(txtSecondOperandValue.Text)
                    Case objPMSetup.EnumOperandType.MatchType
                        strOperandValue2 = Trim(lstMatchType.SelectedItem.Value)
                    Case Else
                        strOperandValue2 = "0"
                End Select
        End Select
        
        strParamUPD = txtStorageAreaCode.Text.Trim & "|" & _
                   txtLineNo.Text.Trim & "|" & _
                   lstFormulaType.SelectedItem.Value & "|" & _
                   strTableCode & "|" & _
                   strOperandType1 & "|" & _
                   strOperandValue1 & "|" & _
                   strOperandType2 & "|" & _
                   strOperandValue2 & "|" & _
                   objPMSetup.EnumStorageAreaStatus.Active & "|" & _
                   lblFMLCreateDate.Text
                   
        If txtLineNo.Text <> lblLineNo.Text Then
            strLineNo = lblLineNo.Text
            btnFMLAdd_Click(Sender, E)
            If lblFMLDupMsg.Visible = False Then
                strParamDEL =  Trim(lblStorageAreaCode.Text) & "|" & _
                            strLineNo
                
                Try
                intErrNo = objPMSetup.mtdDelStorageAreaFormula(strOppCdFML_DEL, strLocation, strParamDEL)
                Catch Exp As System.Exception
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSSETUP_STORAGEAREA_RENAME_LINENO&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_StorageTypeMaster.aspx")
                End Try
            Else
                Exit Sub
            End If
        End If
        
        Try
            intErrNo = objPMSetup.mtdUpdStorageAreaFormula(strOppCdFML_ADD, _
                                                strOppCdFML_UPD, _
                                                strOppCdFML_GET, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParamUPD, _
                                                blnDupKey, _
                                                True)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSSETUP_STORAGEAREA_UPDATEFORMULA&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_StorageTypeMaster.aspx")
        End Try
        
        lblFMLDupMsg.visible = False
        EventData.EditItemIndex = -1
        BindGrid() 
        NewFormula()
    End Sub
    
    Sub btnSave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        SaveMasterData()
        If lblDupMsg.visible = True Then
            Exit Sub
        End If
        CloneFormula()
        BindGrid()
    End Sub

    Sub btnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim blnDupKey As Boolean = False
        strParam = txtStorageAreaCode.Text.Trim & "|" & _
                   lstStorageType.SelectedItem.Value & "|" & _
                   txtStorageNo.Text.Trim & "|" & _
                   objPMSetup.EnumStorageAreaStatus.Deleted & "|" & _
                   txtCreateDate.Text
        
        Try
            intErrNo = objPMSetup.mtdUpdStorageArea(strOppCd_ADD, _
                                                strOppCd_UPD, _
                                                strOppCd_GET, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                blnDupKey, _
                                                True)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSSETUP_STORAGEAREA_DELETE&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_StorageTypeMaster.aspx")
        End Try

        lblTempStorageTypeCode.Text = ""
        LoadMasterData()
        BindGrid()
    End Sub
    
    Sub btnUndelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim blnDupKey As Boolean = False
        strParam = txtStorageAreaCode.Text.Trim & "|" & _
                   lstStorageType.SelectedItem.Value & "|" & _
                   txtStorageNo.Text.Trim & "|" & _
                   objPMSetup.EnumStorageAreaStatus.Active & "|" & _
                   txtCreateDate.Text
        
        Try
            intErrNo = objPMSetup.mtdUpdStorageArea(strOppCd_ADD, _
                                                strOppCd_UPD, _
                                                strOppCd_GET, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                blnDupKey, _
                                                True)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSSETUP_STORAGEAREA_UNDELETE&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_StorageTypeMaster.aspx")
        End Try
        
        lblTempStorageTypeCode.Text = ""
        LoadMasterData()
        BindGrid()
    End Sub
    
    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PM_Setup_StorageAreaMaster.aspx")
    End Sub
    
    Sub lstStorageType_OnSelectedIndexChanged(ByVal Sender As Object, ByVal E As EventArgs)
        lblTempStorageTypeCode.Text = Trim(lstStorageType.SelectedItem.Value)
        BindLineNoList(lstFirstOperandValue, "")
        BindLineNoList(lstSecondOperandValue, "")
        BindTempGrid()
    End Sub
    
    Sub lstFormulaType_OnSelectedIndexChanged(ByVal Sender As Object, ByVal E As EventArgs)
        If lstFormulaType.SelectedIndex = 0 Then
            lblFormulaTypeMsg.Visible = True
        Else
            lblFormulaTypeMsg.Visible = False
        End If
        BindOperandTypeList()
        SetFormulaVisibility()
    End Sub
    
    Sub lstDropDownList_OnSelectedIndexChanged(ByVal Sender As Object, ByVal E As EventArgs)
        SetFormulaVisibility()
    End Sub


End Class
