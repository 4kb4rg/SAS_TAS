Imports System
Imports System.Data
Imports System.IO 
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction
Imports Microsoft.VisualBasic
Imports agri.Admin
Imports agri.HR
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.PWSystem.clsLangCap
Imports agri.GL


Public Class PR_setup_ASTEKDet_Estate : Inherits Page


    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents AstekCd As HtmlInputHidden
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblNoRecord As Label
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrDupDept As Label
    Protected WithEvents lblNoBrsRt As Label
    Protected WithEvents lblNoCompCode As Label
    Protected WithEvents lblNoLocCode As Label
   
    Protected WithEvents lblDepartment As Label
    Protected WithEvents lblCompany As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents lblDeptHead As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblList As Label
    Protected WithEvents lblCodeDiv As Label
    Protected WithEvents lblDivHead As Label
    Protected WithEvents lblAstekCd As Label
    Protected WithEvents lblDesc As Label
    Protected WithEvents txtDesc As TextBox
    Protected WithEvents txtAstekCd As TextBox
    Protected WithEvents txtPStart As TextBox
    Protected WithEvents txtPEnd As TextBox
    Protected WithEvents txtJKM As TextBox
    Protected WithEvents txtJKK As TextBox
    Protected WithEvents txtJHT As TextBox
    Protected WithEvents txtJPK As TextBox
    Protected WithEvents txtJPKKwn As TextBox
	Protected WithEvents txtJP As TextBox
    Protected WithEvents txtPotHJT As TextBox 
	Protected WithEvents txtPotJPK As TextBox 
	Protected WithEvents txtPotJP  As TextBox
	
	Protected WithEvents chkjpk As CheckBox 
    Protected WithEvents validateCode As RequiredFieldValidator
    Protected WithEvents revCode As RegularExpressionValidator


    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objAdminComp As New agri.Admin.clsComp()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim ObjOK As New agri.GL.ClsTrx

    Dim objJobCode As New Object()
    Dim objDeptDs As New Object()
    Dim objDeptCodeDs As New Object()
    Dim objCompDs As New Object()
    Dim objDivHeadDs As New Object()
    Dim objLocDs As New Object()
    Dim objEmpDs As New Object()
    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intHRAR As Long

    Dim strSelectedAstekCd As String = ""
    Dim intStatus As Integer
    Dim strLocType As String
    Dim ParamNama As String
    Dim ParamValue As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        intHRAR = Session("SS_HRAR")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        'ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRCompany), intHRAR) = False Then
        '    Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblAstekCd.Visible = False
            lblDesc.Visible = False
          
            strSelectedAstekCd = Trim(IIf(Request.QueryString("AstekCode") <> "", Request.QueryString("AstekCode"), Request.Form("AstekCode")))
            intStatus = CInt(lblHiddenSts.Text)

            If Not IsPostBack Then
                If strSelectedAstekCd <> "" Then
                    AstekCd.Value = strSelectedAstekCd
                    onLoad_Display()
                    txtAstekCd.Enabled = False
                Else
                    txtAstekCd.Enabled = True
                    onLoad_BindButton()
                End If
            End If
        End If

    End Sub

    Sub onLoad_Display()
        Dim strOpCd_Get As String = "PR_PR_STP_ASTEK_GET"
        Dim intErrNo As Integer
        Dim strSearch As String
        Dim sortitem As String


        strSearch = "AND A.LocCode='" & strLocation &"' AND A.AstekCode like '" & Trim(AstekCd.Value) & "%'"
        sortitem = ""
        ParamNama = "SEARCH|SORT"
        ParamValue = strSearch & "|" & sortitem

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_Get, ParamNama, ParamValue, objDeptDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_ASTEK_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try


        If objDeptDs.Tables(0).Rows.Count > 0 Then
            intStatus = CInt(Trim(objDeptDs.Tables(0).Rows(0).Item("Status")))
            lblHiddenSts.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("Status"))
            txtAstekCd.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("AstekCode"))
            txtDesc.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("Description"))
            txtPStart.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("PeriodeStart"))
            txtPEnd.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("PeriodeEnd"))
            txtJKM.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("JKMPsn"))
            txtJKK.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("JKKPsn"))
            txtJHT.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("JHTPsn"))
            txtJPK.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("JKPPsn"))
            txtJPKKwn.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("JKPPsnKwn"))
            txtPotHJT.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("PotHJTPsn"))
			txtPotJPK.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("PotJPKPsn"))
            lblStatus.Text = objHRSetup.mtdGetDeptStatus(Trim(objDeptDs.Tables(0).Rows(0).Item("Status")))
            lblDateCreated.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("UserName")) 
			chkjpk.Checked = IIf(Trim(objDeptDs.Tables(0).Rows(0).Item("JPKisPsn")) = "1", True, False) 
			txtJP.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("JPPsn"))
			txtPotJP.Text= Trim(objDeptDs.Tables(0).Rows(0).Item("PotJPPsn"))
            onLoad_BindButton()
        Else
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_ASTEK_GET&errmesg=" & lblNoRecord.Text )
        End If

    End Sub

    Sub onLoad_BindButton()

        txtAstekCd.Enabled = False
        txtDesc.Enabled = False
        txtPStart.Enabled = False
        txtPEnd.Enabled = False
        txtJKM.Enabled = False
        txtJKK.Enabled = False
        txtJHT.Enabled = False
        txtJPK.Enabled = False
        txtJPKKwn.Enabled = False
        txtPotHJT.Enabled = False
		txtPotJPK.Enabled = False
		txtJP.Enabled = False
		txtPotJP.Enabled = False
        SaveBtn.Visible = False
        UnDelBtn.Visible = False
        DelBtn.Visible = False

        Select Case Trim(intStatus)
            Case "1"
                txtDesc.Enabled = True
                txtPStart.Enabled = True
                txtPEnd.Enabled = True
                txtJKM.Enabled = True
                txtJKK.Enabled = True
                txtJHT.Enabled = True
                txtJPK.Enabled = True
                txtJPKKwn.Enabled = True
                txtPotHJT.Enabled = True
				txtPotJPK.Enabled = True
				txtJP.Enabled = True
				txtPotJP.Enabled = True
                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case "2"
                UnDelBtn.Visible = True
            Case Else
                txtAstekCd.Enabled = True
                txtDesc.Enabled = True
                txtPStart.Enabled = True
                txtPEnd.Enabled = True
                txtJKM.Enabled = True
                txtJKK.Enabled = True
                txtJHT.Enabled = True
                txtJPK.Enabled = True
                txtJPKKwn.Enabled = True
                txtPotHJT.Enabled = True
				txtPotJPK.Enabled = True
				txtJP.Enabled = True
				txtPotJP.Enabled = True
                SaveBtn.Visible = True
        End Select

    End Sub


    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "PR_PR_STP_ASTEK_UPD"
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim intErrNo As Integer
        Dim Adate As String
        Dim strstatus As String = "" 
		Dim strchkastek As Integer = CInt(chkjpk.Checked) * -1 
        Dim objID As New Object()

        If txtAstekCd.Text = "" Then
            lblErrMessage.Text = "Please Insert Astek Code"
            lblErrMessage.Visible = True
            Exit Sub
        End If

        If txtDesc.Text = "" Then
            lblErrMessage.Text = "Please Insert Deskripsi"
            lblErrMessage.Visible = True
            Exit Sub
        End If

        If txtPStart.Text = "" Then
            lblErrMessage.Text = "Please Insert Periode start"
            lblErrMessage.Visible = True
            Exit Sub
        End If

        If strCmdArgs = "Save" Or strCmdArgs = "UnDel" Then

            strstatus = "1"

        ElseIf strCmdArgs = "Del" Then

            strstatus = "2"

        End If

        strSelectedAstekCd = Trim(txtAstekCd.Text)

        ParamNama = "AC|Desc|PS|PE|JKMP|JKKP|JHTP|JKPP|JKPPK|PHJTP|ST|CD|UD|UI|PSN|PJPK|JP|PJP|LOC"
        ParamValue = Trim(txtAstekCd.Text) & "|" & txtDesc.Text & "|" & txtPStart.Text & "|" & txtPEnd.Text & "|" & txtJKM.Text & "|" & txtJKK.Text & "|" & _
                     txtJHT.Text & "|" & txtJPK.Text & "|" & txtJPKKwn.Text & "|" & txtPotHJT.Text & "|" & _
                     strstatus & "|" & DateTime.Now & "|" & DateTime.Now & "|" & strUserId & "|" & strchkastek & "|" & iif(txtPotJPK.text.trim()="","0",txtPotJPK.Text.trim()) & "|" & _
					 txtJP.Text  & "|" &  txtPotJP.Text & "|" & _
					 strLocation 
					 
        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOpCd_Upd, ParamNama, ParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PREMIBERAS_UPD&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If strSelectedAstekCd <> "" Then
            onLoad_Display()
        End If

    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_setup_AstekList_Estate.aspx")
    End Sub

End Class
