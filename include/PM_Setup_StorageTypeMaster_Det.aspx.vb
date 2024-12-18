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

Public Class PM_Setup_StorageTypeMaster_Det : Inherits Page
    Protected objPMSetup As New agri.PM.clsSetup()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

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
    
    Protected WithEvents lblStorageTypeCode As Label
    Protected WithEvents lblLineNo As Label
    Protected WithEvents lblFormulaType As Label
    Protected WithEvents lblFMLCreateDate As Label
    
    Protected WithEvents txtStorageTypeCode As TextBox
    Protected WithEvents txtDescription As TextBox
    Protected WithEvents lstProductCode As DropDownList

    Protected WithEvents lblCPOStorageLocation as Label
    Protected WithEvents rbCPOStorageLocationMill As RadioButton
    Protected WithEvents rbCPOStorageLocationBulking As RadioButton

    
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

    Dim strOppCd_GET As String = "PM_CLSSETUP_STORAGETYPE_GET"
    Dim strOppCd_ADD As String = "PM_CLSSETUP_STORAGETYPE_ADD"
    Dim strOppCd_UPD As String = "PM_CLSSETUP_STORAGETYPE_UPD"
    Dim strOppCdFML_GET As String = "PM_CLSSETUP_STORAGETYPEFML_GET"
    Dim strOppCdFML_ADD As String = "PM_CLSSETUP_STORAGETYPEFML_ADD"
    Dim strOppCdFML_UPD As String = "PM_CLSSETUP_STORAGETYPEFML_UPD"
    Dim strOppCdFML_DEL As String = "PM_CLSSETUP_STORAGETYPEFML_DEL"
	
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
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMStorageTypeMaster), intPMAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            If Not Page.IsPostBack Then
                BindDropDownList()
                lblStorageTypeCode.Text = Trim(Request.QueryString("StorageTypeCode"))
                lblLineNo.Text = ""
                lblFormulaType.Text = ""
                If lblStorageTypeCode.Text = "" Then   
                    Delete.visible = False
                    Undelete.visible = False
                    FMLAdd.Enabled = False
                    FMLSave.Enabled = False
                    txtLineNo.Enabled = False
                    lstFormulaType.Enabled = False
                    NewFormula()
                Else    
                    LoadMasterData()
                    NewFormula()
                End If
            Else
                If lblLineNo.Text <> "" Then
                    If lblFormulaType.Text = "" Then
                    End If
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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSSETUP_STORAGETYPE_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_StorageTypeMaster.aspx")
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
        lstProductCode.Items.Add(New ListItem("Please select product code", ""))
        lstProductCode.Items.Add(New ListItem(objPMSetup.mtdGetProductCode(objPMSetup.EnumProductCode.CPO), objPMSetup.EnumProductCode.CPO))
        lstProductCode.Items.Add(New ListItem(objPMSetup.mtdGetProductCode(objPMSetup.EnumProductCode.PK), objPMSetup.EnumProductCode.PK))

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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSSETUP_STORAGETYPE_BINDTABLECODE&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_StorageTypeMaster.aspx")
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

        SearchStr = " AND STF.StorageTypeCode = '" & lblStorageTypeCode.Text & "'"

        strParam = "ORDER BY STF.LnNo ASC|" & SearchStr

        Try
            intErrNo = objPMSetup.mtdGetStorageTypeFormula(strOppCdFML_GET, strParam, dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSSETUP_STORAGETYPE_BINDLINENO&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_StorageTypeMaster.aspx")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item(0) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(1))
            dsForDropDown.Tables(0).Rows(intCnt).Item(2) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(1))
            If Not LineNo = "" Then
                If Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(1)) = Trim(LineNo) Then
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
        strParam = "|AND ST.StorageTypeCode='" & lblStorageTypeCode.Text & "'"
        Try
            intErrNo = objPMSetup.mtdGetStorageType(strOppCd_GET, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSSETUP_STORAGETYPE_LOADMASTERDATA&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_StorageTypeMaster.aspx")
        End Try

        If objDataSet.Tables(0).Rows.Count > 0 Then
            txtStorageTypeCode.Text = Trim(objDataSet.Tables(0).Rows(0).Item("StorageTypeCode"))
            txtDescription.Text = Trim(objDataSet.Tables(0).Rows(0).Item("Description"))
             For I = 0 To lstProductCode.Items.Count - 1
                If lstProductCode.Items.Item(I).value = Trim(objDataSet.Tables(0).Rows(0).Item("ProductCode")) Then
                    lstProductCode.selectedIndex = I
                End If
            Next            

                IF objDataSet.Tables(0).Rows(0).Item("StorageLocation") = objPMSetup.EnumCPOStorageLocation.Mill Then
                    rbCPOStorageLocationMill.checked = "True"
                    rbCPOStorageLocationBulking.checked = "False"
                Else
                    rbCPOStorageLocationMill.checked = "False"
                    rbCPOStorageLocationBulking.checked = "True"    
                End If

                lblCPOStorageLocation.visible = "True"
                rbCPOStorageLocationMill.visible = "True"
                rbCPOStorageLocationBulking.visible = "True"

            txtStorageTypeCode.Enabled = False
            lblStatus.Text = objPMSetup.mtdGetStorageTypeStatus(Trim(objDataSet.Tables(0).Rows(0).Item("Status")))
            lblCreateDate.Text = objGlobal.GetLongDate(Trim(objDataSet.Tables(0).Rows(0).Item("CreateDate")))
            txtCreateDate.Text = Trim(objDataSet.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(Trim(objDataSet.Tables(0).Rows(0).Item("UpdateDate")))
            lblUpdateBy.Text = Trim(objDataSet.Tables(0).Rows(0).Item("Username"))
            
            If Trim(objDataSet.Tables(0).Rows(0).Item("Status")) = objPMsetup.EnumStorageTypeStatus.Active Then
                txtDescription.Enabled = True
                lstProductCode.Enabled = True
                Save.visible = True
                Delete.visible = True
                Undelete.visible = False
                FMLAdd.Enabled = True
                FMLSave.Enabled = True
                txtLineNo.Enabled = True
                lstFormulaType.Enabled = True
            Else
                NewFormula()
                txtDescription.Enabled = False
                lstProductCode.Enabled = False
                Save.visible = False
                Delete.visible = False
                Undelete.visible = True
                FMLAdd.Enabled = False
                FMLSave.Enabled = False
                txtLineNo.Enabled = False
                lstFormulaType.Enabled = False
            End If
            BindGrid()
        Else
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSSETUP_STORAGETYPE_MASTERDATA_NOTFOUND&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_StorageTypeMaster.aspx")
        End If
    End Sub

    Public Sub LoadItemData()
        Dim I As Integer
        strParam = "|AND STF.StorageTypeCode='" & lblStorageTypeCode.Text & "' AND STF.LnNo=" & lblLineNo.text
        Try
            intErrNo = objPMSetup.mtdGetStorageTypeFormula(strOppCdFML_GET, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSSETUP_STORAGETYPE_LOADITEMDATA&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_StorageTypeMaster.aspx")
        End Try
        If objDataSet.Tables(0).Rows.Count > 0 Then
            lblFormulaTypeMsg.visible = False
            lblFMLDupMsg.visible = False
            txtLineNo.Text = objDataSet.Tables(0).Rows(0).Item("LnNo")
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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSSETUP_STORAGETYPE_ITEMDATA_NOTFOUND&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_StorageTypeMaster.aspx")
        End If
    End Sub
    
    Protected Function LoadGridData() As DataSet
        strParam = "ORDER BY LnNo ASC|AND STF.StorageTypeCode='" & lblStorageTypeCode.Text & "'"
        Try
            intErrNo = objPMSetup.mtdGetStorageTypeFormula(strOppCdFML_GET, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSSETUP_STORAGETYPE_LOADGRIDDATA&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_StorageTypeMaster.aspx")
        End Try

        Return objDataSet
    End Function
    
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
        lblFormulaTypeMsg.visible = False
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
        BindGrid() 
        lblLineNo.Text = ""
        NewFormula()
    End Sub

    Sub DEDR_Delete(Sender As Object, E As DataGridCommandEventArgs)
        Dim lbl As Label
        
        lbl = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("FMLLineNo")
        
        strParam =  lblStorageTypeCode.Text & "|" & _
                    CDbl(lbl.Text)
        
        Try
        intErrNo = objPMSetup.mtdDelStorageTypeFormula(strOppCdFML_DEL, strParam)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSSETUP_STORAGETYPE_DELETEFORMULA&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_StorageTypeMaster.aspx")
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
            lblFormulaTypeMsg.visible = True
            Exit Sub
        End If

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
        
        strParam = txtStorageTypeCode.Text.Trim & "|" & _
                   txtLineNo.Text.Trim & "|" & _
                   lstFormulaType.SelectedItem.Value & "|" & _
                   strTableCode & "|" & _
                   strOperandType1 & "|" & _
                   strOperandValue1 & "|" & _
                   strOperandType2 & "|" & _
                   strOperandValue2 & "|" & _
                   objPMSetup.EnumStorageTypeStatus.Active & "|"
                   
        
        Try
            intErrNo = objPMSetup.mtdUpdStorageTypeFormula(strOppCdFML_ADD, _
                                                strOppCdFML_UPD, _
                                                strOppCdFML_GET, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                blnDupKey, _
                                                False)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSSETUP_STORAGETYPE_ADDFORMULA&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_StorageTypeMaster.aspx")
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
            lblFormulaTypeMsg.visible = True
            Exit Sub
        End If
        
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
        
        strParamUPD = txtStorageTypeCode.Text.Trim & "|" & _
                   txtLineNo.Text.Trim & "|" & _
                   lstFormulaType.SelectedItem.Value & "|" & _
                   strTableCode & "|" & _
                   strOperandType1 & "|" & _
                   strOperandValue1 & "|" & _
                   strOperandType2 & "|" & _
                   strOperandValue2 & "|" & _
                   objPMSetup.EnumStorageTypeStatus.Active & "|" & _
                   lblFMLCreateDate.Text
        
        If txtLineNo.Text <> lblLineNo.Text Then
            strLineNo = lblLineNo.Text
            btnFMLAdd_Click(Sender, E)
            If lblFMLDupMsg.Visible = False Then
                strParamDEL =  Trim(lblStorageTypeCode.Text) & "|" & _
                            strLineNo
                
                Try
                intErrNo = objPMSetup.mtdDelStorageTypeFormula(strOppCdFML_DEL, strParamDEL)
                Catch Exp As System.Exception
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSSETUP_STORAGETYPE_SAVEFORMULA_RENUM&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_StorageTypeMaster.aspx")
                End Try
            Else
                Exit Sub
            End If
        End If
        
        Try
            intErrNo = objPMSetup.mtdUpdStorageTypeFormula(strOppCdFML_ADD, _
                                                strOppCdFML_UPD, _
                                                strOppCdFML_GET, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParamUPD, _
                                                blnDupKey, _
                                                True)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSSETUP_STORAGETYPE_UPDATEFORMULA&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_StorageTypeMaster.aspx")
        End Try
        
        lblFMLDupMsg.visible = False
        EventData.EditItemIndex = -1
        BindGrid() 
        NewFormula()
    End Sub
    
    Sub btnSave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim blnDupKey As Boolean = False
        Dim intStoragelocation As Integer = -1
        

        If lblStorageTypeCode.Text <> "" Then
            blnUpdate.text = True
            If FMLAdd.visible = True Then
                btnFMLAdd_Click(Sender, E)
            Else
                btnFMLSave_Click(Sender, E)
            End If
        Else
            blnUpdate.text = False
        End If

            IF rbCPOStorageLocationMill.checked = "True"
                intStoragelocation = objPMSetup.EnumCPOStorageLocation.Mill
            ELSE
                intStoragelocation = objPMSetup.EnumCPOStorageLocation.Bulking
            END IF
        
        strParam = txtStorageTypeCode.Text.Trim & "|" & _
                   txtDescription.Text.Trim & "|" & _
                   lstProductCode.SelectedItem.Value & "|" & _
                   intStoragelocation & "|" & _
                   objPMSetup.EnumStorageTypeStatus.Active & "|" & _
                   txtCreateDate.Text
        
        Try
            intErrNo = objPMSetup.mtdUpdStorageType(strOppCd_ADD, _
                                                strOppCd_UPD, _
                                                strOppCd_GET, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                blnDupKey, _
                                                blnUpdate.text)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSSETUP_STORAGETYPE_SAVE&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_StorageTypeMaster.aspx")
        End Try
        
        If blnDupKey Then
            lblDupMsg.Visible = True
        Else
            lblDupMsg.visible = False
            txtStorageTypeCode.readonly = True
            lblStorageTypeCode.Text = txtStorageTypeCode.Text.Trim
            LoadMasterData()
            NewFormula()
        End If
    End Sub

    Sub btnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim blnDupKey As Boolean = False
        strParam = txtStorageTypeCode.Text.Trim & "|" & _
                   txtDescription.Text.Trim & "|" & _
                   lstProductCode.SelectedItem.Value & "|" & _
                   objPMSetup.EnumStorageTypeStatus.Deleted & "|" & _
                   txtCreateDate.Text
        
        Try
            intErrNo = objPMSetup.mtdUpdStorageType(strOppCd_ADD, _
                                                strOppCd_UPD, _
                                                strOppCd_GET, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                blnDupKey, _
                                                True)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSSETUP_STORAGETYPE_DELETE&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_StorageTypeMaster.aspx")
        End Try
        LoadMasterData()
    End Sub
    
    Sub btnUndelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim blnDupKey As Boolean = False
        strParam = txtStorageTypeCode.Text.Trim & "|" & _
                   txtDescription.Text.Trim & "|" & _
                   lstProductCode.SelectedItem.Value & "|" & _
                   objPMSetup.EnumStorageTypeStatus.Active & "|" & _
                   txtCreateDate.Text
        
        Try
            intErrNo = objPMSetup.mtdUpdStorageType(strOppCd_ADD, _
                                                strOppCd_UPD, _
                                                strOppCd_GET, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                blnDupKey, _
                                                True)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSSETUP_STORAGETYPE_UNDELETE&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_StorageTypeMaster.aspx")
        End Try
        LoadMasterData()
    End Sub
    
    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PM_Setup_StorageTypeMaster.aspx")
    End Sub
    



    Sub lstFormulaType_OnSelectedIndexChanged(ByVal Sender As Object, ByVal E As EventArgs)
        If lstFormulaType.SelectedIndex = 0 Then
            lblFormulaTypeMsg.visible = True
        Else
            lblFormulaTypeMsg.visible = False
        End If
        BindOperandTypeList()
        SetFormulaVisibility()
    End Sub

    
    Sub lstProductCode_OnSelectedIndexChanged(ByVal Sender As Object, ByVal E As EventArgs)

        IF lstProductCode.SelectedItem.Value = "" Then
            lstProductCode.SelectedItem.Value = -1
        End IF

        IF lstProductCode.SelectedItem.Value = objPMSetup.EnumProductCode.CPO Then
            lblCPOStorageLocation.visible = "True"
            rbCPOStorageLocationMill.visible = "True"
            rbCPOStorageLocationBulking.visible = "True"
        ELSE
            lblCPOStorageLocation.visible = "True"
            rbCPOStorageLocationMill.visible = "True"
            rbCPOStorageLocationBulking.visible = "True"   

        End IF

    End Sub

    Sub lstDropDownList_OnSelectedIndexChanged(ByVal Sender As Object, ByVal E As EventArgs)
        SetFormulaVisibility()
    End Sub

End Class
