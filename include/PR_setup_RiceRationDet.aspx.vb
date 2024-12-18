Imports System
Imports System.Data
Imports System.IO 
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Information
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction
Imports System.Math
Imports Microsoft.VisualBasic



Public Class PR_setup_RiceRationDet : Inherits Page

    Protected WithEvents lblTitle As Label
    Protected WithEvents lblRiceCode As Label
    Protected WithEvents lblRiceDesc As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents txtRiceCode As TextBox
    Protected WithEvents txtDescription As TextBox
    Protected WithEvents ddlPayType As Dropdownlist
    Protected WithEvents txtIndRice As TextBox
    Protected WithEvents txtSpouseRice As TextBox
    Protected WithEvents txtChildRice As TextBox
    Protected WithEvents txtTotalChild As TextBox
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents tbcode As HtmlInputHidden
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrDup As Label
    Protected WithEvents lblErrMessage As Label

    Protected WithEvents txtPricePerKg As TextBox
    Protected WithEvents cbBonus As CheckBox
    Dim objHRSetup As New agri.HR.clsSetup()
    Protected WithEvents txtBonusPricePerKg As TextBox
    Protected WithEvents txtTHRPricePerKg As TextBox
    Protected WithEvents txtJAMPricePerKg As TextBox    
    Dim objPRSetup As New agri.PR.clsSetup
    
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights
    Dim objLangCap As New agri.PWSystem.clsLangCap
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intPRAR As Long

    Dim objRiceDs As New DataSet
    Dim objLangCapDs As New Object
    Dim objSalSchemeDs As New Object()

    Dim strSelectedRiceCode As String = ""
    Dim intStatus As Integer
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_ACCMONTH")
        strAccYear = Session("SS_ACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intPRAR = Session("SS_PRAR")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRRice), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrDup.Visible = False
            strSelectedRiceCode = Trim(IIf(Request.QueryString("tbcode") <> "", Request.QueryString("tbcode"), Request.Form("tbcode")))
            intStatus = CInt(lblHiddenSts.Text)

            onload_GetLangCap()
            If Not IsPostBack Then
                If strSelectedRiceCode <> "" Then
                    tbcode.Value = strSelectedRiceCode
                    onLoad_Display()
                Else
                    BindPayType("")
                End If
            End If
            onLoad_BindButton()
        End If
    End Sub

    Sub BindPayType(ByVal pv_strPayType As String)

        Dim strOpCd_Get As String = "HR_CLSSETUP_SALSCHEME_LIST_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim dr As DataRow

        strParam = "Order By SAL.SalSchemeCode | AND SAL.Status = '" & objHRSetup.EnumSalSchemeStatus.Active & "'"

        Try
            intErrNo = objHRSetup.mtdGetMasterList(strOpCd_Get, strParam, objHRSetup.EnumHRMasterType.SalScheme, objSalSchemeDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=HR_CLSSETUP_SALSCHEME_LIST_GET&errmesg=" & Exp.ToString() & "&redirect=pr/setup/PR_setup_RiceRationDet.aspx")
        End Try

        If objSalSchemeDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objSalSchemeDs.Tables(0).Rows.Count - 1
                If Trim(objSalSchemeDs.Tables(0).Rows(intCnt).Item("SalSchemeCode")) = Trim(pv_strPayType) Then
                    intSelectedIndex = intCnt + 1
                    Exit For
                End If
            Next
        End If

        dr = objSalSchemeDs.Tables(0).NewRow()
        dr("SalSchemeCode") = ""
        dr("_Description") = "Please Select Employee Category"
        objSalSchemeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlPayType.DataSource = objSalSchemeDs.Tables(0)
        ddlPayType.DataTextField = "_Description"
        ddlPayType.DataValueField = "SalSchemeCode"
        ddlPayType.DataBind()
        ddlPayType.SelectedIndex = intSelectedIndex
    End Sub

    Sub onLoad_Display()
        Dim strOpCd As String = "PR_CLSSETUP_RICERATION_LIST_GET_BY_RICECODE"
        Dim strParam As String = strSelectedRiceCode
        Dim intErrNo As Integer

        Try
            intErrNo = objPRSetup.mtdGetRiceRation(strOpCd, _
                                                  strParam, _
                                                  objRiceDs, _
                                                  True)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_CLSSETUP_RICERATION_LIST_GET_BY_RICECODE&errmesg=" & Exp.ToString() & "&redirect=pr/setup/PR_setup_RiceRationDet.aspx")
        End Try

        txtRiceCode.Text = objRiceDs.Tables(0).Rows(0).Item("RiceRationCode")
        txtDescription.Text = objRiceDs.Tables(0).Rows(0).Item("Description")


        txtIndRice.Text = objRiceDs.Tables(0).Rows(0).Item("IndividualRice")
        txtSpouseRice.Text = objRiceDs.Tables(0).Rows(0).Item("SpouseRice")
        txtChildRice.Text = objRiceDs.Tables(0).Rows(0).Item("ChildRice")
        
        
        txtTotalChild.Text = objRiceDs.Tables(0).Rows(0).Item("TotalChild")
        BindPayType(objRiceDs.Tables(0).Rows(0).Item("Category"))

        txtPricePerKg.Text = objGlobal.DisplayForEditCurrencyFormat(objRiceDs.Tables(0).Rows(0).Item("PricePerKG"))
        cbBonus.Checked = IIf(CInt(IIF(Isnumeric(objRiceDs.Tables(0).Rows(0).Item("Bonus")),objRiceDs.Tables(0).Rows(0).Item("Bonus"),2)) = objPRSetup.EnumBonus.Yes, True, False)


        txtBonusPricePerKg.Text = objGlobal.DisplayForEditCurrencyFormat(objRiceDs.Tables(0).Rows(0).Item("BonusPricePerKG"))
        txtTHRPricePerKg.Text = objGlobal.DisplayForEditCurrencyFormat(objRiceDs.Tables(0).Rows(0).Item("THRPricePerKG"))
        txtJAMPricePerKg.Text = objGlobal.DisplayForEditCurrencyFormat(objRiceDs.Tables(0).Rows(0).Item("JAMPricePerKG"))  
       
      
        intStatus = CInt(objRiceDs.Tables(0).Rows(0).Item("Status"))
        lblHiddenSts.Text = objRiceDs.Tables(0).Rows(0).Item("Status")
        lblStatus.Text = objPRSetup.mtdGetRiceStatus(objRiceDs.Tables(0).Rows(0).Item("Status"))
        lblDateCreated.Text = objGlobal.GetLongDate(objRiceDs.Tables(0).Rows(0).Item("CreateDate"))
        lblLastUpdate.Text = objGlobal.GetLongDate(objRiceDs.Tables(0).Rows(0).Item("UpdateDate"))
        lblUpdatedBy.Text = objRiceDs.Tables(0).Rows(0).Item("UserName")
        onLoad_BindButton()
    End Sub


    Sub onLoad_BindButton()
        txtRiceCode.Enabled = False
        txtDescription.Enabled = False
        ddlPayType.Enabled = False
        txtIndRice.Enabled = False
        txtSpouseRice.Enabled = False        
        txtChildRice.Enabled = False
        txtTotalChild.Enabled = False
        SaveBtn.Visible = False
        DelBtn.Visible = False
        UnDelBtn.Visible = False
        txtPricePerKg.Enabled = False
        cbBonus.Enabled = False
        txtBonusPricePerKg.Enabled = False
        txtTHRPricePerKg.Enabled = False
        txtJAMPricePerKg.Enabled = False
        Select Case intStatus
            Case objPRSetup.EnumRiceStatus.Active
                txtDescription.Enabled = True
                txtIndRice.Enabled = True
                ddlPayType.Enabled = True
                txtSpouseRice.Enabled = True
                txtChildRice.Enabled = True
                txtTotalChild.Enabled = True
                txtPricePerKg.Enabled = True
                cbBonus.Enabled = True
                txtBonusPricePerKg.Enabled = True
                txtTHRPricePerKg.Enabled = True
                txtJAMPricePerKg.Enabled = True
                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"

            Case objPRSetup.EnumRiceStatus.Deleted
                UnDelBtn.Visible = True
            Case Else
                txtRiceCode.Enabled = True
                txtDescription.Enabled = True
                ddlPayType.Enabled = True
                txtIndRice.Enabled = True
                txtSpouseRice.Enabled = True
                txtChildRice.Enabled = True
                txtTotalChild.Enabled = True
                txtPricePerKg.Enabled = True
                cbBonus.Enabled = True
                txtBonusPricePerKg.Enabled = True
                txtTHRPricePerKg.Enabled = True
                txtJAMPricePerKg.Enabled = True
                SaveBtn.Visible = True
        End Select
    End Sub

    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Add As String = "PR_CLSSETUP_RICERATION_DETAIL_ADD"
        Dim strOpCd_Upd As String = "PR_CLSSETUP_RICERATION_DETAIL_UPD"
        Dim strOpCd_Get As String = "PR_CLSSETUP_RICERATION_LIST_GET_BY_RICECODE"
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim intErrNo As Integer
        Dim blnIsUpdate As Boolean
        Dim strParam As String = ""

        If strCmdArgs = "Save" Then
            strParam = txtRiceCode.Text.Trim
            Try
                intErrNo = objPRSetup.mtdGetRiceRation(strOpCd_Get, _
                                                       strParam, _
                                                       objRiceDs, _
                                                       True)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_RICERATION_GET_BY_RICECODE&errmesg=" & Exp.ToString() & "&redirect=pr/setup/PR_setup_RiceRationDet.aspx")
            End Try

            If objRiceDs.Tables(0).Rows.Count > 0 And intStatus = 0 Then
                lblErrDup.Visible = True
                Exit Sub
            Else
                strSelectedRiceCode = txtRiceCode.Text.Trim
                blnIsUpdate = IIf(intStatus = 0, False, True)
                tbcode.Value = strSelectedRiceCode

                strParam = txtRiceCode.Text.Trim & "|" & _
                           txtDescription.Text.Trim & "|" & _
                           ddlPayType.SelectedItem.Value & "|" & _
                           txtIndRice.Text.Trim & "|" & _
                           txtSpouseRice.Text.Trim & "|" & _
                           txtChildRice.Text.Trim & "|" & _
                           txtTotalChild.Text.Trim & "|" & _
                           objPRSetup.EnumRiceStatus.Active & "|" & _
                           txtPricePerKg.Text.Trim & "|" & _
                           IIf(cbBonus.Checked = True, objPRSetup.EnumBonus.Yes, objPRSetup.EnumBonus.No) & "|" & _
                           txtBonusPricePerKg.Text.Trim & "|" & _
                           txtTHRPricePerKg.Text.Trim & "|" & _
                           txtJAMPricePerKg.Text.Trim & "|"    
                
                Try
                    intErrNo = objPRSetup.mtdUpdRiceRation(strOpCd_Add, _
                                                           strOpCd_Upd, _
                                                           strCompany, _
                                                           strLocation, _
                                                           strUserId, _
                                                           strParam, _
                                                           blnIsUpdate)
                Catch Exp As System.Exception
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_RICERATION_SAVE&errmesg=" & Exp.ToString() & "&redirect=pr/setup/PR_setup_RiceRationDet.aspx")
                End Try
            End If
        ElseIf strCmdArgs = "Del" Then
            strParam = txtRiceCode.Text.Trim & "|||||||" & objPRSetup.EnumRiceStatus.Deleted & "||||||"
            Try
                intErrNo = objPRSetup.mtdUpdRiceRation(strOpCd_Add, _
                                                       strOpCd_Upd, _
                                                       strCompany, _
                                                       strLocation, _
                                                       strUserId, _
                                                       strParam, _
                                                       True)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_RICERATION_DEL&errmesg=" & Exp.ToString() & "&redirect=pr/setup/PR_setup_RiceRationDet.aspx?tbcode=" & strSelectedRiceCode)
            End Try
        ElseIf strCmdArgs = "UnDel" Then
            strParam = txtRiceCode.Text.Trim & "|||||||" & objPRSetup.EnumRiceStatus.Active & "||||||"
            Try 
                intErrNo = objPRSetup.mtdUpdRiceRation(strOpCd_Add, _
                                                       strOpCd_Upd, _
                                                       strCompany, _
                                                       strLocation, _
                                                       strUserId, _
                                                       strParam, _
                                                       True)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_RICERATION_UNDEL&errmesg=" & Exp.ToString() & "&redirect=pr/setup/PR_setup_RiceRationDet.aspx?tbcode=" & strSelectedRiceCode)
            End Try
        End If

        If strSelectedRiceCode <> "" Then
            onLoad_Display()
        End If
    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_setup_RiceRationList.aspx")
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblTitle.Text = UCase(GetCaption(objLangCap.EnumLangCap.RiceRation))
        lblRiceCode.text = GetCaption(objLangCap.EnumLangCap.RiceRation) & lblCode.text
        lblRiceDesc.text = GetCaption(objLangCap.EnumLangCap.RiceRationDesc)
        lblErrDup.Text = "<br>This code has been used, please try another " & lblRiceCode.Text & " ."
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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_RICERATIONDET_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=pr/Setup/pr_setup_riceration.aspx")
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




End Class
