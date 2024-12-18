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


Public Class PR_setup_AttdDet_Estate : Inherits Page

    Protected WithEvents ddlAttCd As DropDownList
    Protected WithEvents ddlEmpTyCd As DropDownList
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents AttId As HtmlInputHidden
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
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblDepartment As Label
    Protected WithEvents lblCompany As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents lblDeptHead As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblList As Label
    Protected WithEvents lblCodeDiv As Label
    Protected WithEvents lblDivHead As Label
    Protected WithEvents lblAttId As Label
    Protected WithEvents lblAtt As Label
    Protected WithEvents lblAttCd As Label
    Protected WithEvents lblEmp As Label
    Protected WithEvents txtAttId As TextBox
    Protected WithEvents Option1 As RadioButtonList
    Protected WithEvents Option2 As RadioButtonList
    Protected WithEvents Option3 As RadioButtonList
    Protected WithEvents Option4 As RadioButtonList

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

    Dim strSelectedAttId As String = ""
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
            
            strSelectedAttId = Trim(IIf(Request.QueryString("AttID") <> "", Request.QueryString("AttID"), Request.Form("AttID")))
            intStatus = CInt(lblHiddenSts.Text)

            If Not IsPostBack Then
                If strSelectedAttId <> "" Then
                    AttId.Value = strSelectedAttId
                    onLoad_Display()
                    txtAttId.Enabled = False
                Else
                    txtAttId.Enabled = False
                    onLoad_BindAccCd("")
                    onLoad_BindEmpTycd("")
                    'onLoad_BindButton()
                End If
            End If
        End If

    End Sub


    Sub onLoad_Display()
        Dim strOpCd_Get As String = "PR_PR_STP_ATTID_GET"
        Dim intErrNo As Integer
        Dim strSearch As String
        Dim sortitem As String


        strSearch = "AND A.AttID like '" & Trim(AttId.Value) & "%'"
        sortitem = ""
        ParamNama = "SEARCH|SORT"
        ParamValue = strSearch & "|" & sortitem

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_Get, ParamNama, ParamValue, objDeptDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PREMIBERAS_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        Dim Op1 As String
        Dim Op2 As String
        Dim Op3 As String
        Dim Op4 As String

        If objDeptDs.Tables(0).Rows.Count > 0 Then
            intStatus = CInt(Trim(objDeptDs.Tables(0).Rows(0).Item("Status")))
            lblHiddenSts.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("Status"))
            txtAttId.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("AttID"))

            Op1 = Trim(objDeptDs.Tables(0).Rows(0).Item("PotUpah"))
            If Trim(Op1) = "1" Then
                Option1.Items.Item(0).Selected = True
            Else
                Option1.Items.Item(1).Selected = True
            End If

            Op2 = Trim(objDeptDs.Tables(0).Rows(0).Item("PotBeras"))
            If Trim(Op2) = "1" Then
                Option2.Items.Item(0).Selected = True
            Else
                Option2.Items.Item(1).Selected = True
            End If

            Op3 = Trim(objDeptDs.Tables(0).Rows(0).Item("PotCuti"))
            If Trim(Op3) = "1" Then
                Option3.Items.Item(0).Selected = True
            Else
                Option3.Items.Item(1).Selected = True
            End If

            Op4 = Trim(objDeptDs.Tables(0).Rows(0).Item("PotWeekend"))
            If Trim(Op4) = "1" Then
                Option4.Items.Item(0).Selected = True
            Else
                Option4.Items.Item(1).Selected = True
            End If

            lblStatus.Text = objHRSetup.mtdGetDeptStatus(Trim(objDeptDs.Tables(0).Rows(0).Item("Status")))
            lblDateCreated.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("UserName"))
            onLoad_BindAccCd(Trim(objDeptDs.Tables(0).Rows(0).Item("CodeAtt")))
            onLoad_BindEmpTycd(Trim(objDeptDs.Tables(0).Rows(0).Item("CodeEmpTy")))
        Else
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PREMIBERAS_GET&errmesg=" & lblNoRecord.Text & "&redirect=pr/setup/PR_setup_PrmBrsDet_Estate.aspx")
        End If

    End Sub

    Sub onLoad_BindAccCd(ByVal pv_strAccCd As String)
        Dim strOpCd_Acc As String = "PR_PR_STP_ATTCODE_LIST_GET"
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strSearch As String
        Dim strSearch_all As String = ""
        Dim sortitem As String


        strSearch = "where Status='1'"
        sortitem = "order by AttCode"
        ParamNama = "SEARCH|SORT"
        ParamValue = strSearch & "|" & sortitem


        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_Acc, ParamNama, ParamValue, objDeptCodeDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_TANGGUNGAN_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try


        For intCnt = 0 To objDeptCodeDs.Tables(0).Rows.Count - 1
            objDeptCodeDs.Tables(0).Rows(intCnt).Item("AttCode") = Trim(objDeptCodeDs.Tables(0).Rows(intCnt).Item("AttCode"))
            objDeptCodeDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objDeptCodeDs.Tables(0).Rows(intCnt).Item("AttCode")) & " (" & Trim(objDeptCodeDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objDeptCodeDs.Tables(0).Rows(intCnt).Item("AttCode") = pv_strAccCd Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objDeptCodeDs.Tables(0).NewRow()
        dr("AttCode") = ""
        dr("Description") = "Select Attendence Code"
        objDeptCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlAttCd.DataSource = objDeptCodeDs.Tables(0)
        ddlAttCd.DataValueField = "AttCode"
        ddlAttCd.DataTextField = "Description"
        ddlAttCd.DataBind()
        ddlAttCd.SelectedIndex = intSelectedIndex
    End Sub

    Sub onLoad_BindEmpTycd(ByVal pv_strEmpTyCd As String)
        Dim strOpCd_EmpTy As String = "HR_HR_STP_EMPTYPE_LIST_GET"
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strSearch As String
        Dim strSearch_all As String = ""
        Dim sortitem As String

        strSearch = "WHERE Status='1'"
        sortitem = "order by EmpTyCode"
        ParamNama = "SEARCH|SORT"
        ParamValue = strSearch & "|" & sortitem

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_EmpTy, ParamNama, ParamValue, objDeptCodeDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_STP_EMPTYPE_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objDeptCodeDs.Tables(0).Rows.Count - 1
            objDeptCodeDs.Tables(0).Rows(intCnt).Item("EmpTyCode") = Trim(objDeptCodeDs.Tables(0).Rows(intCnt).Item("EmpTyCode"))
            objDeptCodeDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objDeptCodeDs.Tables(0).Rows(intCnt).Item("EmpTyCode")) & " (" & Trim(objDeptCodeDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objDeptCodeDs.Tables(0).Rows(intCnt).Item("EmpTyCode") = pv_strEmpTyCd Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objDeptCodeDs.Tables(0).NewRow()
        dr("EmpTyCode") = ""
        dr("Description") = "Select Employment Type Code"
        objDeptCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlEmpTyCd.DataSource = objDeptCodeDs.Tables(0)
        ddlEmpTyCd.DataValueField = "EmpTyCode"
        ddlEmpTyCd.DataTextField = "Description"
        ddlEmpTyCd.DataBind()
        ddlEmpTyCd.SelectedIndex = intSelectedIndex
    End Sub

    Sub onLoad_BindButton()
        lblAtt.Visible = True
        'txtAttId.Visible = True
        'txtAttId.Enabled = False
        'Option1.Enabled = False
        'Option2.Enabled = False
        'Option3.Enabled = False
        ''Option4.Enabled = False
        'SaveBtn.Visible = False
        'UnDelBtn.Visible = False
        'DelBtn.Visible = False

        'Select Case Trim(intStatus)
        '    Case "1"
        '        Option1.Enabled = True
        '        Option2.Enabled = True
        '        Option3.Enabled = True
        '        'Option4.Enabled = True
        '        SaveBtn.Visible = True
        '        DelBtn.Visible = True
        '        DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
        '    Case "2"
        '        UnDelBtn.Visible = True
        '    Case Else
        '        lblAtt.Visible = False
        '        txtAttId.Visible = False
        '        Option1.Enabled = True
        '        Option2.Enabled = True
        '        Option3.Enabled = True
        '        '  Option4.Enabled = True
        '        SaveBtn.Visible = True
        'End Select

    End Sub


    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "PR_PR_STP_ATTID_UPD"
        Dim strOpcd_GetId As String = "ALL_ALL_GETID"
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim intErrNo As Integer
        Dim strId As String
        Dim ValAttCd As String
        Dim strPrefix As String
        Dim strstatus As String = ""
        Dim objID As New Object()


        If intStatus = 0 Then

            strPrefix = "ATT"
            ParamNama = "PREFIX|LOCID|LEN"
            ParamValue = strPrefix & "||8"

            Try
                intErrNo = ObjOK.mtdGetDataCommon(strOpcd_GetId, ParamNama, ParamValue, objID)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_DEPT_DEPTCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try

            strId = Trim(objID.Tables(0).Rows(0).Item(0))
            ValAttCd = strId

        Else

            ValAttCd = strSelectedAttId

        End If

       
        If strCmdArgs = "Save" Or strCmdArgs = "UnDel" Then

            strstatus = "1"

        ElseIf strCmdArgs = "Del" Then

            strstatus = "2"

        End If

        AttId.Value = ValAttCd
        strSelectedAttId = AttId.Value
        ParamNama = "AI|CA|CE|PU|PCu|PCi|Pw|ST|CD|UD|UI"
        ParamValue = Trim(AttId.Value) & "|" & _
                     ddlAttCd.SelectedValue & "|" & _
                     ddlEmpTyCd.SelectedValue & "|" & _
                     IIf(Trim(Option1.SelectedValue = "Ya"), "1", "0") & "|" & _
                     IIf(Trim(Option2.SelectedValue = "Ya"), "1", "0") & "|" & _
                     IIf(Trim(Option3.SelectedValue = "Ya"), "1", "0") & "|" & _
                     IIf(Trim(Option4.SelectedValue = "Ya"), "1", "0") & "|" & _
                     strstatus & "|" & _
                     DateTime.Now & "|" & _
                     DateTime.Now & "|" & _
                     strUserId



        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOpCd_Upd, ParamNama, ParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PREMIBERAS_UPD&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        onLoad_Display()

    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_Setup_AttdList_Estate.aspx")
    End Sub

End Class
