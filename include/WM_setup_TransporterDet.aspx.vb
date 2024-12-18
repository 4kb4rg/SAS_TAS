
Imports System
Imports System.Data
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction

Public Class WM_TransporterDet : Inherits Page

    Dim objWM As New agri.WM.clsSetup()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGLTrx As New agri.GL.ClsTrx()

    Protected WithEvents txtTransCode As TextBox
    Protected WithEvents txtName As TextBox
    Protected WithEvents txtAddress As HtmlTextArea
    Protected WithEvents txtTelNo As TextBox

    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdateBy As Label

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblErrDup As Label
    Protected WithEvents lblErrAddress As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblSelectList As Label
    Protected WithEvents lblErrSelect As Label
    Protected WithEvents rbExt As RadioButton
    Protected WithEvents rbInt As RadioButton

    Protected WithEvents hidTransCode As HtmlInputHidden
    Protected WithEvents btnSave As ImageButton
    Protected WithEvents btnDelete As ImageButton
    Protected WithEvents btnUnDelete As ImageButton
    Protected WithEvents btnBack As ImageButton
	
	Protected WithEvents txtSupCode As TextBox

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intWMAR As Integer

    Dim objTransDs As New DataSet()

    Dim strSelectedTrans As String = ""
    Dim strSortExpression As String = "TransporterCode"

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        Dim arrParam As Array
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PMACCMONTH")
        strAccYear = Session("SS_PMACCYEAR")
        intWMAR = Session("SS_WMAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWMAccessRights.WMTransporterSetup), intWMAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If Request.QueryString("TransCode") <> "" Then
                arrParam = Split(Trim(IIf(Request.QueryString("TransCode") <> "", Request.QueryString("TransCode"), Request.Form("TransCode"))), "|")
                strSelectedTrans = arrParam(0)
            Else
                strSelectedTrans = Trim(IIf(Request.QueryString("TransCode") <> "", Request.QueryString("TransCode"), Request.Form("TransCode")))
            End If
            If Not IsPostBack Then
                If strSelectedTrans <> "" Then
                    onLoad_Display()
                Else
                    EnableControl()
                End If
            End If
        End If
    End Sub
	
	

    Sub onLoad_Display()
        Dim strOpCd_Transporter_Det As String = "WM_CLSSETUP_TRANSPORTER_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow

        txtTransCode.Text = strSelectedTrans
        hidTransCode.Value = strSelectedTrans

        strParam = hidTransCode.Value & "|||||" & strSortExpression & "|"
        Try
            intErrNo = objWM.mtdGetTransporter(strOpCd_Transporter_Det, strParam, objTransDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WM_TRANSPORTER_DET_GET&errmesg=" & lblErrMessage.Text & "&redirect=WM/setup/WM_setup_TransporterList.aspx")
        End Try

        If objTransDs.Tables(0).Rows.Count > 0 Then
            objTransDs.Tables(0).Rows(0).Item("Name") = Trim(objTransDs.Tables(0).Rows(0).Item("Name"))
            objTransDs.Tables(0).Rows(0).Item("Address") = Trim(objTransDs.Tables(0).Rows(0).Item("Address"))
            objTransDs.Tables(0).Rows(0).Item("TelNo") = Trim(objTransDs.Tables(0).Rows(0).Item("TelNo"))
            objTransDs.Tables(0).Rows(0).Item("Status") = Trim(objTransDs.Tables(0).Rows(0).Item("Status"))
            objTransDs.Tables(0).Rows(0).Item("CreateDate") = Trim(objTransDs.Tables(0).Rows(0).Item("CreateDate"))
            objTransDs.Tables(0).Rows(0).Item("UpdateDate") = Trim(objTransDs.Tables(0).Rows(0).Item("UpdateDate"))
            objTransDs.Tables(0).Rows(0).Item("UserName") = Trim(objTransDs.Tables(0).Rows(0).Item("UserName"))
        End If

        txtTransCode.Text = hidTransCode.Value
        txtName.Text = objTransDs.Tables(0).Rows(0).Item("Name")
        txtAddress.Value = objTransDs.Tables(0).Rows(0).Item("Address")
        txtTelNo.Text = objTransDs.Tables(0).Rows(0).Item("TelNo")
        lblStatus.Text = objWM.mtdGetTransporterStatus(Trim(objTransDs.Tables(0).Rows(0).Item("Status")))
        lblDateCreated.Text = objGlobal.GetLongDate(objTransDs.Tables(0).Rows(0).Item("CreateDate"))
        lblLastUpdate.Text = objGlobal.GetLongDate(objTransDs.Tables(0).Rows(0).Item("UpdateDate"))
        lblUpdateBy.Text = objTransDs.Tables(0).Rows(0).Item("UserName")
		txtSupCode.Text = objTransDs.Tables(0).Rows(0).Item("SupplierCode")
        
        Select Case objTransDs.Tables(0).Rows(0).Item("TType")
        case objWM.EnumTransporterType.External
            rbExt.Checked = True
        case else
            rbInt.Checked = True
        end select
      
        Select Case Trim(lblStatus.Text)
            Case objWM.mtdGetTransporterStatus(objWM.EnumTransporterStatus.Active)
                EnableControl()
            Case objWM.mtdGetTransporterStatus(objWM.EnumTransporterStatus.Deleted)
                DisableControl()
        End Select

    End Sub

    Sub DisableControl()
        txtTransCode.Enabled = False
        txtName.Enabled = False
        txtAddress.Disabled = True
        txtTelNo.Enabled = False
        btnSave.Visible = False
        btnDelete.Visible = False
        btnUnDelete.Visible = True
        rbExt.Enabled = False
        rbInt.Enabled = False
    End Sub

    Sub EnableControl()
        If txtTransCode.Text = "" Then
            txtTransCode.ReadOnly = False
            btnDelete.Visible = False
        Else
            txtTransCode.Enabled = True
            txtTransCode.ReadOnly = True
            btnDelete.Visible = True
            btnDelete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
        End If

        txtName.Enabled = True
        txtAddress.Disabled = False
        txtTelNo.Enabled = True
        btnSave.Visible = True
        btnUnDelete.Visible = False
        rbExt.Enabled = True
        rbInt.Enabled = True

    End Sub

    Sub btnSave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Transporter_Add As String = "WM_CLSSETUP_TRANSPORTER_ADD" '"WM_CLSSETUP_TRANSPORTER_ADD_AUTO" '"WM_CLSSETUP_TRANSPORTER_ADD"
        Dim strOpCd_Transporter_Upd As String = "WM_CLSSETUP_TRANSPORTER_UPD"
        Dim strOpCd_Transporter_Get As String = "WM_CLSSETUP_TRANSPORTER_GET"
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strSrchTrans As String
        Dim strName As String = txtName.Text.Trim
        Dim strAddress As String = IIf(txtAddress.Value = "", "", txtAddress.Value.Trim)
        Dim strTelNo As String = IIf(txtTelNo.Text = "", "", txtTelNo.Text.Trim)
        Dim strTType As String

        Dim strParamName As String
        Dim strParamValue As String
        Dim objSPLDs As New Object

        If rbExt.Checked = True Then
            strTType = objWM.EnumTransporterType.External
        Else
            strTType = objWM.EnumTransporterType.Internal
        End If 
     
        If Len(strAddress) > 512 Then
            lblErrAddress.Visible = True
            Exit Sub
        End If

        strParam = txtTransCode.Text & "|" & strName & "|" & strAddress & "|" & strTelNo & "|" & objWM.EnumTransporterStatus.Active & "|" & strTType

        If hidTransCode.Value <> "" Then
            Try
                intErrNo = objWM.mtdUpdTransporter(strOpCd_Transporter_Add, _
                                                    strOpCd_Transporter_Upd, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WM_SETUP_TRANSPORTER_UPD&errmesg=" & lblErrMessage.Text & "&redirect=WM/setup/WM_setup_TransporterList.aspx")
            End Try
			
			Try
				strParamName = "STRUPDATE"
				strParamValue = "SET SupplierCode = '" & txtSupCode.Text.Trim()  &  "' WHERE TRANSPORTERCODE='" & txtTransCode.Text & "'"
				
				intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd_Transporter_Upd, strParamName,  strParamValue)
			
			Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WM_SETUP_TRANSPORTER_UPD&errmesg=" & lblErrMessage.Text & "&redirect=WM/setup/WM_setup_TransporterList.aspx")
            End Try

        Else
            strParamName = "TRANSPORTERCODE|NAME|ADDRESS|TELNO|STATUS|CREATEDATE|UPDATEDATE|UPDATEID|TTYPE|SUPPLIERCODE"
            strParamValue = txtTransCode.Text & "|" & strName & "|" & strAddress & "|" & strTelNo & "|" & objWM.EnumTransporterStatus.Active & "|" & _
                            Now() & "|" & Now() & "|" & Trim(strUserId) & "|" & strTType & "|" & txtSupCode.Text.Trim() 

            Try
                intErrNo = objGLTrx.mtdGetDataCommon(strOpCd_Transporter_Add, _
                                                    strParamName, _
                                                    strParamValue, _
                                                    objSPLDs)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try

            If objSPLDs.Tables(0).Rows.Count > 0 Then
                txtTransCode.Text = Trim(objSPLDs.Tables(0).Rows(0).Item("TransporterCode"))
            End If

            'strSrchTrans = txtTransCode.Text.Trim & "|||||" & strSortExpression & "|"

            'Try
            '    intErrNo = objWM.mtdGetTransporter(strOpCd_Transporter_Get, strSrchTrans, objTransDs)
            'Catch Exp As System.Exception
            '    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WM_SETUP_TRANSPORTER_GET&errmesg=" & lblErrMessage.Text & "&redirect=WM/setup/WM_setup_TransporterList.aspx")
            'End Try

            'If objTransDs.Tables(0).Rows.Count = 0 Then
            '    Try
            '        intErrNo = objWM.mtdUpdTransporter(strOpCd_Transporter_Add, _
            '                                            strOpCd_Transporter_Upd, _
            '                                            strCompany, _
            '                                            strLocation, _
            '                                            strUserId, _
            '                                            strParam, _
            '                                            False)
            '    Catch Exp As System.Exception
            '        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WM_SETUP_TRANSPORTER_ADD&errmesg=" & lblErrMessage.Text & "&redirect=WM/setup/WM_setup_TransporterList.aspx")
            '    End Try

            'Else
            '    lblErrDup.Visible = True
            'End If
        End If

        strSelectedTrans = txtTransCode.Text.Trim

        onLoad_Display()
    End Sub

    Sub btnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Transporter_Upd As String = "WM_CLSSETUP_TRANSPORTER_UPD"
        Dim strOpCd_Transporter_Add As String = ""
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim strTType As String

        If rbExt.Checked = True Then
            strTType = objWM.EnumTransporterType.External
        Else
            strTType = objWM.EnumTransporterType.Internal
        End If 

        strParam = strSelectedTrans & "||||" & objWM.EnumTransporterStatus.Deleted & "|" & strTType

        Try
            intErrNo = objWM.mtdUpdTransporter(strOpCd_Transporter_Add, _
                                                strOpCd_Transporter_Upd, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WM_SETUP_TRANSPORTER_DEL&errmesg=" & lblErrMessage.Text & "&redirect=WM/setup/WM_setup_TransporterList.aspx")
        End Try

        onLoad_Display()
    End Sub


    Sub btnUnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Transporter_Upd As String = "WM_CLSSETUP_TRANSPORTER_UPD"
        Dim strOpCd_Transporter_Add As String = ""
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim strTType As String


        If rbExt.Checked = True Then
            strTType = objWM.EnumTransporterType.External
        Else
            strTType = objWM.EnumTransporterType.Internal
        End If 

        strParam = strSelectedTrans & "||||" & objWM.EnumTransporterStatus.Active & "|" & strTType

        Try
            intErrNo = objWM.mtdUpdTransporter(strOpCd_Transporter_Add, _
                                                strOpCd_Transporter_Upd, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WM_SETUP_TRANSPORTER_UNDEL&errmesg=" & lblErrMessage.Text & "&redirect=WM/setup/WM_setup_TransporterList.aspx")
        End Try

        onLoad_Display()
    End Sub


    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("WM_setup_TransporterList.aspx")
    End Sub



End Class
