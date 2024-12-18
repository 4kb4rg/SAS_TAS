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


Public Class PR_setup_PrmiKrjnn_Estate : Inherits Page


    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents txtpstart As TextBox
    Protected WithEvents txtpend As TextBox
    Protected WithEvents txtMinHK As TextBox
    Protected WithEvents txtMinKg As TextBox
    Protected WithEvents txtPrmRt As TextBox
    Protected WithEvents txtOvRt As TextBox
    Protected WithEvents txtprmdrs As TextBox


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

    Dim PStart As String = ""
    Dim PEnd As String = ""

    Dim strSelectedAstekCd As String = ""
    Dim intStatus As Integer
    Dim strLocType As String
    Dim ParamNama As String
    Dim ParamValue As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        intHRAR = Session("SS_HRAR")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        'ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRCompany), intHRAR) = False Then
         '   Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrMessage.Visible = False
            PStart = Trim(IIf(Request.QueryString("periodestart") <> "", Request.QueryString("periodestart"), Request.Form("periodestart")))
            PEnd = Trim(IIf(Request.QueryString("periodeend") <> "", Request.QueryString("periodeend"), Request.Form("periodeend")))

            If Not IsPostBack Then
                If PStart <> "" And PEnd <> "" Then
                    onLoad_Display(PStart, PEnd)
                Else
                    intStatus = 0
                    txtpstart.Text = Format(DateTime.Now, "MMyyyy")
                    txtpend.Text = Format(DateTime.Now, "MMyyyy")
                End If
            End If
        End If
    End Sub


    Sub onLoad_Display(ByVal ps As String, ByVal pe As String)
        Dim strOpCd_Get As String = "PR_PR_STP_PREMI_KERAJINAN_GET"
        Dim intErrNo As Integer
        Dim strSearch As String
        Dim sortitem As String


        strSearch = "AND periodestart='" & ps & "' AND periodeend='" & pe & "'"
        sortitem = ""
        ParamNama = "SEARCH|SORT"
        ParamValue = strSearch & "|" & sortitem


        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_Get, ParamNama, ParamValue, objDeptDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PREMI_KERAJINAN_GET&errmesg=" & Exp.Message.Trim() & "&redirect=")
        End Try


        If objDeptDs.Tables(0).Rows.Count > 0 Then
            intStatus = CInt(Trim(objDeptDs.Tables(0).Rows(0).Item("Status")))
            txtpstart.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("PeriodeStart"))
            txtpend.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("PeriodeEnd"))
            txtMinHK.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("MinHK"))
            txtMinKg.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("MinKG"))
            txtPrmRt.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("PrmRate"))
            txtOvRt.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("OvrRate"))
            txtprmdrs.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("KaretRate"))
           
            lblStatus.Text = objHRSetup.mtdGetDeptStatus(Trim(objDeptDs.Tables(0).Rows(0).Item("Status")))
            lblDateCreated.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("UserName"))
            onLoad_BindButton()
        End If

    End Sub

    Sub onLoad_BindButton()
    End Sub


    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "PR_PR_STP_PREMI_KERAJINAN_UPD"
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim intErrNo As Integer
        Dim strstatus As String = ""
        Dim objID As New Object()

        If txtMinHK.Text = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Please Input Min HK"
            Exit Sub
        End If

        If txtPrmRt.Text = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Pleae Input Premi Rate"
            Exit Sub
        End If

        If txtOvRt.Text = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Pleae Input Overtime Rate"
            Exit Sub
        End If


        If strCmdArgs = "Save" Or strCmdArgs = "UnDel" Then
            strstatus = "1"
        ElseIf strCmdArgs = "Del" Then
            strstatus = "2"
        End If


        ParamNama = "PS|PE|MHK|MKG|PRate|Orate|KRate|ST|CD|UD|UI"
        ParamValue = txtpstart.Text & "|" & _
                     txtpend.Text & "|" & _
                     txtMinHK.Text & "|" & _
                     txtMinKg.Text & "|" & _
                     txtPrmRt.Text & "|" & _
                     txtOvRt.Text & "|" & _
                     txtprmdrs.Text & "|" & _
                     strstatus & "|" & _
                     DateTime.Now & "|" & DateTime.Now & "|" & strUserId

       

        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOpCd_Upd, ParamNama, ParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PREMI_KERAJINAN_UPD&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try


        Response.Redirect("PR_setup_PrmiKrjnnList_Estate.aspx")

    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_setup_PrmiKrjnnList_Estate.aspx")
    End Sub

End Class
