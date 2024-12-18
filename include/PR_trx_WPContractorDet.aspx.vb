Imports System
Imports System.Data
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Information
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction


Public Class PR_trx_WPContractorDet : Inherits Page

    Protected WithEvents txtDesc As TextBox
    Protected WithEvents txtWPDate As TextBox
    Protected WithEvents ddlContractor As DropDownList
    Protected WithEvents ddlAccCode As DropDownList 
    Protected WithEvents ddlBlock As DropDownList
    Protected WithEvents txtWorkProductivity As TextBox
    Protected WithEvents txtHariKerja As TextBox
    Protected WithEvents tblSelection As HtmlTable
    Protected WithEvents dgLineDet As DataGrid

    Protected WithEvents lblWPTrxID As Label
    Protected WithEvents lblUOM As Label
    Protected WithEvents lblBlock As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    
    Protected WithEvents WPTrxID As HtmlInputHidden
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents CancelBtn As ImageButton
    Protected WithEvents NewBtn As ImageButton
    
    Protected WithEvents lblErrWPDate As Label
    Protected WithEvents lblErrWPDateFmt As Label
    Protected WithEvents lblErrWPDateFmtMsg As Label
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrContractor As Label
    Protected WithEvents lblErrBlock As Label
    Protected WithEvents lblErrAccCode As Label
    Protected WithEvents lblErrWorkProductivity As Label
    Protected WithEvents lblErrExceeding As Label
    Protected WithEvents lblErrSelect As Label
    Protected WithEvents lblErrDupl As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblToDateWP As Label
    Protected WithEvents lblTotalWP As Label
    Protected WithEvents lblTotalHK As Label
    Protected lblCloseExist As Label    
    Protected lblTotArea As Label  

    Protected objPRTrx As New agri.PR.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objHRTrx As New agri.HR.clsTrx()
    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdmAcc As New agri.Admin.clsAccPeriod()
    
    Dim objLangCapDs As New Dataset()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intPRAR As Long
    Dim intConfig As Integer
    Dim strDateFmt As String
    Dim strSelectedWPId As String = ""
    Dim intStatus As Integer
    Dim strAcceptFormat As String
    Dim strUOMAccCode As String
    Dim strUOMBlock As String
    Dim objLoc As New agri.Admin.clsLoc()
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intPRAR = Session("SS_PRAR")
        intConfig = Session("SS_CONFIGSETTING")
        strDateFmt = Session("SS_DATEFMT")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxWPContractor), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            lblErrContractor.Visible = False
            lblErrBlock.Visible = False
            lblErrAccCode.Visible = False
            lblErrDupl.Visible = False
            lblErrExceeding.Visible = False
            lblErrWPDate.Visible = False
            lblErrWPDateFmt.Visible = False
            lblErrWPDateFmtMsg.Visible = False
            
            strSelectedWPId = Trim(IIf(Request.QueryString("WPTrxID") <> "", Request.QueryString("WPTrxID"), Request.Form("WPTrxID")))
            intStatus = Convert.ToInt32(lblHiddenSts.Text)
            
            If Not IsPostBack Then
                If strSelectedWPId <> "" Then
                    WPTrxID.Value = strSelectedWPId
                    onLoad_Display()
                    onLoad_DisplayLine()
                    onLoad_BindButton()
                    BindBlock("")      
                Else
                    txtWPDate.Text = objGlobal.GetShortDate(strDateFmt, Now())
                    BindContractor("")
                    BindAccCode("")
                    BindBlock("")            
                    onLoad_BindButton()
                End If
            End If

        End If
    End Sub



    Sub onload_GetLangCap()
        GetEntireLangCap()
        Try
            lblBlock.Text = GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.Text

        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=pr_trx_wpdet_GET_LANGCAP_COSTLEVEL&errmesg=" & Exp.ToString() & "&redirect=PR/trx/pr_trx_wpdet.aspx")
        End Try
        lblErrBlock.Text = lblErrSelect.Text & lblBlock.Text
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
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_WPDET_GET_LANGCAP&errmesg=" & Exp.ToString() & "&redirect=PR/trx/PR_trx_WPDet.aspx")
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


    Sub onLoad_BindButton()
        txtWPDate.Enabled = False
        ddlContractor.Enabled = False
        ddlBlock.Enabled = False
        ddlAccCode.Enabled = False
        txtWorkProductivity.Enabled = False
        txtHariKerja.Enabled = False
        SaveBtn.Visible = False
        tblSelection.Visible = False
        lblUOM.Text = ""
        lblToDateWP.Text = 0
        txtWorkProductivity.Text = 0
        txtHariKerja.Text = 0

        Select Case intStatus
            Case objPRTrx.EnumWPContractorTrxStatus.Active
                txtWPDate.Enabled = True
                ddlContractor.Enabled = False
                ddlBlock.Enabled = True
                ddlAccCode.Enabled = False
                txtWorkProductivity.Enabled = True
                txtHariKerja.Enabled = True
                SaveBtn.Visible = True
                tblSelection.Visible = True
            Case objPRTrx.EnumWPContractorTrxStatus.Closed, objPRTrx.EnumWPContractorTrxStatus.Deleted
                
            Case Else
                txtWPDate.Enabled = True    
                ddlContractor.Enabled = True
                ddlBlock.Enabled = True
                ddlAccCode.Enabled = True
                txtWorkProductivity.Enabled = True
                txtHariKerja.Enabled = True
                SaveBtn.Visible = True
                tblSelection.Visible = True
         End Select
    End Sub

    Sub onLoad_Display()
        Dim objWPTrxDs As New Dataset
        Dim strOpCd As String = "PR_CLSTRX_WPCONTRACTORTRX_GET"
        Dim strParam As String = strSelectedWPId
        Dim intErrNo As Integer

        Try
            intErrNo = objPRTrx.mtdGetWPTrx(strOpCd, _
                                            strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strAccMonth, _
                                            strAccYear, _
                                            strParam, _
                                            objWPTrxDs, _
                                            True)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_WPCONTRACTORTRX_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        WPTrxID.Value = strSelectedWPId
        lblWPTrxID.Text = strSelectedWPId
        txtWPDate.Text =  Date_Validation(objWPTrxDs.Tables(0).Rows(0).Item("WPDate"), True)
        intStatus = Convert.ToInt32(objWPTrxDs.Tables(0).Rows(0).Item("Status"))
        lblHiddenSts.Text = objWPTrxDs.Tables(0).Rows(0).Item("Status").Trim()
        lblStatus.Text = objPRTrx.mtdGetWPTrxStatus(Convert.ToInt16(objWPTrxDs.Tables(0).Rows(0).Item("Status")))
        lblDateCreated.Text = objGlobal.GetLongDate(objWPTrxDs.Tables(0).Rows(0).Item("CreateDate"))
        lblLastUpdate.Text = objGlobal.GetLongDate(objWPTrxDs.Tables(0).Rows(0).Item("UpdateDate"))
        lblUpdatedBy.Text = objWPTrxDs.Tables(0).Rows(0).Item("UserName")
        BindContractor(objWPTrxDs.Tables(0).Rows(0).Item("ContractorCode").Trim())
        BindAccCode(objWPTrxDs.Tables(0).Rows(0).Item("AccCode").Trim())
        objWPTrxDs = Nothing
    End Sub

    Sub onLoad_DisplayLine()
        Dim objWPTrxLnDs As New Dataset()
        Dim strOpCd As String = "PR_CLSTRX_WPCONTRACTORTRX_LINE_GET"
        Dim strParam As String = strSelectedWPId
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim lbButton As LinkButton
        Dim dblWorkProductivity As Integer = 0
        Dim dblHariKerja As Integer = 0

        Try
            intErrNo = objPRTrx.mtdGetWPTrx(strOpCd, _
                                            strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strAccMonth, _
                                            strAccYear, _
                                            strParam, _
                                            objWPTrxLnDs, _
                                            True)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_WPCONTRACTORTRXLINE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objWPTrxLnDs.Tables(0).Rows.Count - 1
            dblWorkProductivity += objWPTrxLnDs.Tables(0).Rows(intCnt).Item("WorkProductivity")
            dblHariKerja += objWPTrxLnDs.Tables(0).Rows(intCnt).Item("HariKerja")
        Next

        dgLineDet.DataSource = objWPTrxLnDs.Tables(0)
        dgLineDet.DataBind()

        lblTotalWP.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(dblWorkProductivity, 0)
        lblTotalHK.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(dblHariKerja, 0)

        If intStatus = objPRTrx.EnumWPContractorTrxStatus.Active Then
            For intCnt = 0 To dgLineDet.Items.Count - 1
                lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = True
                    lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Next
        Else
            For intCnt = 0 To dgLineDet.Items.Count - 1
                lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                lbButton.Visible = False
            Next
        End If
    End Sub

    Sub BindContractor(ByVal pv_strContractorId As String)
        Dim objGangDs As New Dataset
        Dim strOpCd As String = "PR_CLSTRX_WPCONTRACTORTRX_GET_CONTRACTORLIST"
        Dim dr As DataRow           
        Dim strParam As String = "|" & "where Status = '" & objPRSetup.EnumContrListStatus.Active & "'" 
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCd, strParam, 0, objGangDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_WPCONTRACTORTRX_CONTRACTOR_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        pv_strContractorId = Trim(UCase(pv_strContractorId))

        For intCnt = 0 To objGangDs.Tables(0).Rows.Count - 1
            objGangDs.Tables(0).Rows(intCnt).Item("Name") = objGangDs.Tables(0).Rows(intCnt).Item("ContractorCode") & " (" & objGangDs.Tables(0).Rows(intCnt).Item("Name") & ")"
            If UCase(objGangDs.Tables(0).Rows(intCnt).Item("ContractorCode")) = pv_strContractorId Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objGangDs.Tables(0).NewRow()
        If intSelectedIndex = 0 Then
            If pv_strContractorId <> "" Then
                dr("ContractorCode") = Trim(pv_strContractorId)
                dr("Name") = Trim(pv_strContractorId)
            Else
                dr("ContractorCode") = ""
                dr("Name") = "Select one Contractor Code"
            End If
        Else
            dr("ContractorCode") = ""
            dr("Name") = "Select one Contractor Code"
        End If
        objGangDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlContractor.DataSource = objGangDs.Tables(0)
        ddlContractor.DataValueField = "ContractorCode"
        ddlContractor.DataTextField = "Name"
        ddlContractor.DataBind()
        ddlContractor.SelectedIndex = intSelectedIndex
        objGangDs = Nothing
    End Sub

    Sub BindAccCode(ByVal pv_strAccCode As String)
        Dim objSubActDs As New Dataset
        Dim strOpCd As String = "PR_CLSTRX_WPCONTRACTORTRX_GET_ACCOUNTLIST"
        Dim dr As DataRow           
        Dim strParam As String = "|" & "where Status = '" & objGLSetup.EnumAccStatus.Active & "'" 
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCd, strParam, 0, objSubActDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_WPCONTRACTORTRX_ACCOUNT_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        pv_strAccCode = Trim(UCase(pv_strAccCode))

        For intCnt = 0 To objSubActDs.Tables(0).Rows.Count - 1
            objSubActDs.Tables(0).Rows(intCnt).Item("Description") = objSubActDs.Tables(0).Rows(intCnt).Item("AccCode") & " (" & objSubActDs.Tables(0).Rows(intCnt).Item("Description") & ")"
            If UCase(objSubActDs.Tables(0).Rows(intCnt).Item("AccCode")) = pv_strAccCode Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objSubActDs.Tables(0).NewRow()
        If intSelectedIndex = 0 Then
            If pv_strAccCode <> "" Then
                dr("AccCode") = Trim(pv_strAccCode)
                dr("Description") = Trim(pv_strAccCode)
            Else
                dr("AccCode") = ""
                dr("Description") = "Select one Account"
            End If
        Else
            dr("AccCode") = ""
            dr("Description") = "Select one Account"
        End If
        objSubActDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlAccCode.DataSource = objSubActDs.Tables(0)
        ddlAccCode.DataValueField = "AccCode"
        ddlAccCode.DataTextField = "Description"
        ddlAccCode.DataBind()
        ddlAccCode.SelectedIndex = intSelectedIndex
        objSubActDs = Nothing
   End Sub

   Sub BindBlock(ByVal pv_strBlockCode As String)
        Dim objBlkDs As New Dataset()
        Dim strOpCd As String = "GL_CLSSETUP_SUBBLOCK_LIST_GET"
        Dim dr As DataRow
        Dim strParam As String = "|" & "And Sub.LocCode = '" & Trim(strLocation) & "' And Sub.Status = '" & objGLSetup.EnumSubBlockStatus.Active & "'" & "|" & strLocation
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intMasterType As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCd, strParam, 0, objBlkDs)
 
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_WPTRX_BLOCK_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        pv_strBlockCode = Trim(UCase(pv_strBlockCode))

        For intCnt = 0 To objBlkDs.Tables(0).Rows.Count - 1
            objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") = objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode").Trim()
            objBlkDs.Tables(0).Rows(intCnt).Item("Description") = objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") & " (" & objBlkDs.Tables(0).Rows(intCnt).Item("Description").Trim() & "), " & objBlkDs.Tables(0).Rows(intCnt).Item("TotalArea") & ", " & objBlkDs.Tables(0).Rows(intCnt).Item("AreaUOM").Trim() 
            If UCase(objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode")) = pv_strBlockCode Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objBlkDs.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("Description") = lblSelect.Text & lblBlock.Text
        objBlkDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlBlock.DataSource = objBlkDs.Tables(0)
        ddlBlock.DataValueField = "BlkCode"
        ddlBlock.DataTextField = "Description"
        ddlBlock.DataBind()
        ddlBlock.SelectedIndex = intSelectedIndex
        ddlBlock.AutoPostBack = True

        objBlkDs = Nothing
    End Sub
        

    


    
    Sub onSelect_Block(ByVal Sender As Object, ByVal E As EventArgs)
        BlockChange()
    End Sub

    Sub BlockChange()
        If Not (ddlAccCode.SelectedItem.Value = "" And ddlBlock.SelectedItem.Value = "")
            GetUOMBlock(ddlBlock.SelectedItem.Value)    
            GetTotWorkProductivity(ddlAccCode.SelectedItem.Value,ddlBlock.SelectedItem.Value)
        End If
    End Sub

    Sub GetUOMBlock(ByVal pv_strBlockCode As String)
        Dim objUOMDs As New Object()
        Dim strOpCd As String = "GL_CLSSETUP_SUBBLOCK_LIST_GET"
        Dim dr As DataRow          
        Dim strParam As String = "|" & "And Sub.LocCode = '" & strLocation & "' And Sub.SubBlkCode = '" & Trim(pv_strBlockCode) & "' And Sub.Status = '" & objGLSetup.EnumSubBlockStatus.Active & "'" & "|" & strLocation
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCd, strParam, 0, objUOMDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_WPCONTRACTORTRX_BLOCK_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objUOMDs.Tables(0).Rows.Count <=0 Then
            Exit Sub
        Else
            lblUOM.Text = objUOMDs.Tables(0).Rows(0).Item("AreaUOM").Trim()
            lblTotArea.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(objUOMDs.Tables(0).Rows(0).Item("TotalArea"),0)
        End If       
    End Sub

    Function GetTotWorkProductivity(ByVal pv_strAccCode As String, ByVal pv_strBlockCode As String) As Integer
        Dim objTotWP As New Object()
        Dim objWPTrxDs As New Dataset()
        Dim strOpCd As String = "PR_CLSTRX_WPCONTRACTORTRX_GET_TOTWP"
        Dim strOpCode_UpdLine As String = "PR_CLSTRX_WPTRX_LINE_UPD"
        Dim dr As DataRow           
        Dim strParam As String = "|" & "where AccCode = '" & Trim(pv_strAccCode) & "' And BlkCode = '" & Trim(pv_strBlockCode) & "' And wpln.Status = '" & objPRTrx.EnumWPContractorTrxLnStatus.Active & "' And wp.Status = '" & objPRTrx.EnumWPContractorTrxStatus.Active & "' " 
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strAccCode As String = Request.Form("ddlAccCode")
        Dim strBlock As String = Request.Form("ddlBlock")
        Dim intSelectedIndex As Integer = 0
        Dim strToDateWP As Integer = 0

        Try
            intErrNo = objPRTrx.mtdGetWPTotWorkProductivity(strOpCd, strParam, objTotWP)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_WPTRX_TotWP_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objTotWP.Tables(0).Rows.Count <=0 Then
            strToDateWP = 0         
            lblToDateWP.Text = strToDateWP
        Else
            strToDateWP = objTotWP.Tables(0).Rows(0).Item("TotWP")
            If strToDateWP < lblTotArea.Text
                lblToDateWP.Text = strToDateWP
            Else
                strParam = strSelectedWPId & "|" & _
                            strBlock & "|" & _
                            Trim(lblUOM.Text) & "|" & _
                            Trim(txtWorkProductivity.Text) & "|" & _
                            Trim(txtHariKerja.Text) & "|" & _
                            Convert.ToString(objPRTrx.EnumWPContractorTrxLnStatus.Active)

                Try
                    intErrNo = objPRTrx.mtdUpdWPContractorTrxLine(" ", _
                                                            strOpCode_UpdLine, _
                                                            strCompany, _
                                                            strLocation, _
                                                            strUserId, _
                                                            strParam, _
                                                            True, _
                                                            objWPTrxDs)
                Catch Exp As Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_WPTRX_LINE_UPD&errmesg=" & Exp.ToString() & "&redirect=pr/trx/pr_clstrx_WPdet.aspx?WPTrxID=" & strSelectedWPId)
                End Try
                lblToDateWP.Text = 0
            End If
        End If
    End Function
        
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
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_WP_GET_CONFIG&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_GRList.aspx")
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

    Sub btnAdd_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim objWPTrxDs As New Dataset()
        Dim objFound As Boolean
        Dim blnIsUpdated As Boolean
        Dim strOpCode_GetLine As String = "PR_CLSTRX_WPCONTRACTORTRX_LINEDUPL_GET"
        Dim strOpCode_AddLine As String = "PR_CLSTRX_WPCONTRACTORTRX_LINE_ADD"
        Dim strOpCode_UpdID As String = "PR_CLSTRX_WPCONTRACTORTRX_STATUS_UPD"
        Dim strAccCode As String = Request.Form("ddlAccCode")
        Dim strBlock As String = Request.Form("ddlBlock")
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intTotWP As Integer = 0
        
        strAccCode = IIf(strAccCode = "", ddlAccCode.SelectedItem.Value, strAccCode)

        If strAccCode = "" Then
            lblErrAccCode.Visible = True
            Exit Sub
        ElseIf strBlock = "" Then
            lblErrBlock.Visible = True
            Exit Sub
        End If
         
        strSelectedWPId = Trim(lblWPTrxID.Text)
        InsertRecord(blnIsUpdated)
        If strSelectedWPId = "" Then
            Exit Sub
        End If
              
        If txtWorkProductivity.Text > 0 Then
            strParam = "|" & "WPTrxID = '" & strSelectedWPId & "' And BlkCode = '" & Trim(strBlock) & "' "
            Try
                intErrNo = objPRTrx.mtdGetWPContractorTrxLn(strOpCode_GetLine, _
                                                strParam, _
                                                objWPTrxDs, _
                                                False)
            Catch Exp As Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSTRX_WPCONTRACTORTRXLN_GET&errmesg=" & Exp.ToString() & "&redirect=")
            End Try

            If objWPTrxDs.Tables(0).Rows.Count > 0 Then
                lblErrDupl.Visible = True
                Exit Sub
            Else 

                strParam = strSelectedWPId & "|" & _
                            strBlock & "|" & _
                            Trim(lblUOM.Text) & "|" & _
                            Trim(txtWorkProductivity.Text) & "|" & _
                            Trim(txtHariKerja.Text) & "|" & _
                            Convert.ToString(objPRTrx.EnumWPContractorTrxLnStatus.Active)
                
                Try
                    intErrNo = objPRTrx.mtdUpdWPContractorTrxLine(strOpCode_UpdID, _
                                                            strOpCode_AddLine, _
                                                            strCompany, _
                                                            strLocation, _
                                                            strUserId, _
                                                            strParam, _
                                                            False, _
                                                            objWPTrxDs)
                Catch Exp As Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_WPCONTRACTORTRX_LINE_ADD&errmesg=" & Exp.ToString() & "&redirect=pr/trx/pr_clstrx_WPdet.aspx?WPTrxID=" & strSelectedWPId)
                End Try
            End If
            
            If strSelectedWPId <> "" Then
                onLoad_Display()
                onLoad_DisplayLine()
                onLoad_BindButton()
            End If
        End If

        objWPTrxDs = Nothing
    End Sub

    Sub InsertRecord(ByRef pr_blnIsUpdated As Boolean)
        Dim objWPTrxDs As New Dataset()
        Dim objWPTrxID As String
        Dim strOpCd_Add As String = "PR_CLSTRX_WPCONTRACTORTRX_ADD"
        Dim strOpCd_Upd As String = "PR_CLSTRX_WPCONTRACTORTRX_UPD"
        Dim strOpCd_Get As String = "PR_CLSTRX_WPCONTRACTORTRX_GET"
        Dim strOpCd_Sts As String = "PR_CLSTRX_WPCONTRACTORTRX_STATUS_UPD"
        Dim objWPCodeDs As New Dataset()
        Dim strOpCd As String
        Dim strContractor As String = Request.Form("ddlContractor")
        Dim strAccCode As String = Request.Form("ddlAccCode")
        Dim strWPDate As String = txtWPDate.Text
        Dim intErrNo As Integer
        Dim strParam As String = strSelectedWPId
        Dim objFormatDate As String
        Dim objActualDate As String
      
        strOpCd = IIf(intStatus = 0, strOpCd_Add, strOpCd_Upd)
        strContractor = IIf(strContractor = "", ddlContractor.SelectedItem.Value, strContractor)
        strAccCode = IIf(strAccCode = "", ddlAccCode.SelectedItem.Value, strAccCode)

        If Trim(txtWPDate.Text) = "" Then
            lblErrWPDate.Visible = True
            Exit Sub
        ElseIf objGlobal.mtdValidInputDate(strDateFmt, _
                                           strWPDate, _
                                           objFormatDate, _
                                           objActualDate) = False Then
    	        lblErrWPDateFmt.Visible = True
                lblErrWPDateFmt.Text = lblErrWPDateFmtMsg.Text & objFormatDate
                Exit Sub
            Else
                strWPDate = Date_Validation(strWPDate, False)
        End If

        strSelectedWPId = Trim(lblWPTrxID.Text)   
        strOpCd = IIf(intStatus = 0, strOpCd_Add, strOpCd_Upd)
        strParam =  objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.WPContractorTrx) & "|" & _
                strSelectedWPId & "|" & _
                strContractor & "|" & _
                strAccCode & "|" & _
                strWPDate & "|" & _                  
                objPRTrx.EnumWPContractorTrxStatus.Active
        Try
            intErrNo = objPRTrx.mtdUpdWPContractorTrx(strOpCd, _
                                            strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strAccMonth, _
                                            strAccYear, _
                                            strParam, _
                                            False, _
                                            objWPTrxID)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSTRX_WPCONTRACTORTRX_SAVE&errmesg=" & Exp.ToString() & "&redirect=pr/trx/pr_clstrx_WPdet.aspx")
        End Try
        
        strSelectedWPId = objWPTrxID
        WPTrxID.Value = strSelectedWPId
        pr_blnIsUpdated = True
        objWPTrxDs = Nothing
    End Sub

    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Add As String = "PR_CLSTRX_WPCONTRACTORTRX_ADD"
        Dim strOpCd_Upd As String = "PR_CLSTRX_WPCONTRACTORTRX_UPD"
        Dim strOpCd_Get As String = "PR_CLSTRX_WPCONTRACTORTRX_GET"
        Dim strOpCd_Sts As String = "PR_CLSTRX_WPCONTRACTORTRX_STATUS_UPD"
        Dim blnIsUpdated As Boolean
        Dim strOpCd As String
        Dim strContractor As String = Request.Form("ddlContractor")
        Dim strAccCode As String = Request.Form("ddlAccCode")
        Dim objWPTrxId As String
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim intErrNo As Integer
        Dim strParam As String = "" 

        strContractor = IIf(strContractor = "", ddlContractor.SelectedItem.Value, strContractor)
        strAccCode = IIf(strAccCode = "", ddlAccCode.SelectedItem.Value, strAccCode)

        If strCmdArgs = "New" Then
            Response.Redirect("PR_trx_WPContractorDet.aspx?" & _
                            "WPTrxID=" & strSelectedWPId)
        End If

        If strContractor = "" Then
            lblErrContractor.Visible = True
            Exit Sub
        ElseIf strAccCode = "" Then
            lblErrAccCode.Visible = True
            Exit Sub
        End If

        If strCmdArgs = "Save" Then
            InsertRecord(blnIsUpdated)
         ElseIf strCmdArgs = "Back" Then
            strParam = strSelectedWPId & "|" & objPRTrx.EnumWPContractorTrxStatus.Closed
            Try
                intErrNo = objPRTrx.mtdUpdWPContractorTrx(strOpCd_Sts, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strAccMonth, _
                                                strAccYear, _
                                                strParam, _
                                                True, _
                                                objWPTrxId)
            Catch Exp As Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSTRX_WPCONTRACTORTRX_CLOSE&errmesg=" & Exp.ToString() & "&redirect=pr/trx/pr_clstrx_WPdet.aspx?WPTrxID=" & strSelectedWPId)
            End Try
        End If

        If strSelectedWPId <> "" Then
            onLoad_Display()
            onLoad_DisplayLine()
            onLoad_BindButton()
        End If
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim objResult As New Object()
        Dim strOpCode_DelLine As String = "PR_CLSTRX_WPCONTRACTORTRX_LINE_DEL"
        Dim strOpCode_UpdID As String = "PR_CLSTRX_WPCONTRACTORTRX_STATUS_UPD"
        Dim strParam As String
        Dim lblAccCode As Label
        Dim lblBlk As Label
        Dim strAccCode As String
        Dim strBlock As String
        Dim intErrNo As Integer

        dgLineDet.EditItemIndex = Convert.ToInt32(E.Item.ItemIndex)
        lblBlk = dgLineDet.Items.Item(Convert.ToInt32(E.Item.ItemIndex)).FindControl("BlkCode")
        strBlock = lblBlk.Text

        Try
            strParam = strSelectedWPId & "|" & _
                       strBlock & "||||" & objPRTrx.EnumWPContractorTrxStatus.Active 
            intErrNo = objPRTrx.mtdUpdWPContractorTrxLine(strOpCode_UpdID, _
                                                strOpCode_DelLine, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                False, _
                                                objResult)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_WPCONTRACTORTRX_LINE_DEL&errmesg=" & Exp.ToString() & "&redirect=pr/trx/pr_clstrx_WPdet.aspx?WPTrxID=" & strSelectedWPId)
        End Try

        onLoad_Display()
        onLoad_DisplayLine()
        onLoad_BindButton()
    End Sub


    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_trx_WPContractorList.aspx")
    End Sub


End Class
