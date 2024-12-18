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

Imports agri.HR
Imports agri.GlobalHdl

Public Class HR_data_Generate_EmpCode : Inherits Page

    Protected WithEvents lblLastEmpId As Label
    Protected WithEvents txtNumberId As Textbox
    Protected WithEvents GenerateBtn As ImageButton
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblResult As Label
    Protected WithEvents lblResultText As Label
    Protected WithEvents lblErrLoc As Label
    Protected WithEvents ddlLocCode As DropDownList

    Dim objHRData As New agri.HR.clsData()
    Dim objHRTrx As New agri.HR.clsTrx()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim objEmpCodeDs As New Object()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intHRAR As Long

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intHRAR = Session("SS_HRAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRGenerateEmpCode), intHRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrLoc.Visible=False
            lblResult.Text = ""
            If Not IsPostBack Then
                onLoad_Display(strLocation)
                BindLocation()
            End If
        End If
    End Sub

    Sub onLoad_Display(ByVal pv_strLocation As String)
        Dim strOpCd As String = "HR_CLSTRX_EMPCODE_LAST_GET"
        Dim intErrNo As Integer
        Dim intIndex As Integer
        Dim strParam As String
        Dim objDs As New Object()

        If pv_strLocation <> "" Then
            Try
                strParam = "|" & pv_strLocation
                intErrNo = objHRTrx.mtdGenerateEmployeeCode(strOpCd, _
                                                            strParam, _
                                                            objEmpCodeDs, _
                                                            True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_DATA_GENEMPCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try

            If Not IsDbNull(objEmpCodeDs.Tables(0).Rows(0).Item("LastEmpCode")) Then
                lblLastEmpId.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("LastEmpCode"))
            Else
                lblLastEmpId.Text = ""
            End If
        Else
            lblErrLoc.Visible=True
        End If
    End Sub

    Sub OnChange_Location(Sender As Object, E As EventArgs)
        If ddlLocCode.SelectedItem.Value = "" Then
            lblErrLoc.Visible=True
        Else
            onLoad_Display(ddlLocCode.SelectedItem.Value)
        End If
    End Sub

    Sub BindLocation()
        Dim strOpCdLocList_GET As String = "ADMIN_CLSLOC_LOCATION_LIST_GET"
        Dim dsForDropDown As DataSet
        Dim SelectedIndex As Integer = 0
        Dim intCnt As Integer
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strFieldCheck As String
        Dim drinsert As DataRow

        Try
            strParam = "||1||LocCode|ASC|"
            intErrNo = objAdminLoc.mtdGetLocCode(strOpCdLocList_GET, strParam, dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_DATA_GENEMPCODE_LOC&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            strFieldCheck = dsForDropDown.Tables(0).Rows(intCnt).Item("LocCode").Trim()
            dsForDropDown.Tables(0).Rows(intCnt).Item("LocCode") = dsForDropDown.Tables(0).Rows(intCnt).Item("LocCode").Trim()
            dsForDropDown.Tables(0).Rows(intCnt).Item("Description") = dsForDropDown.Tables(0).Rows(intCnt).Item("LocCode") & " ( " & _
                                                                   dsForDropDown.Tables(0).Rows(intCnt).Item("Description").Trim() & " )"
            If strFieldCheck.ToUpper = UCase(Session("SS_LOCATION")) Then
                SelectedIndex = intCnt + 1
            End If
        Next intCnt

        drinsert = dsForDropDown.Tables(0).NewRow()
        drinsert("LocCode") = ""
        drinsert("Description") = ""
        dsForDropDown.Tables(0).Rows.InsertAt(drinsert, 0)

        ddlLocCode.DataSource = dsForDropDown.Tables(0)
        ddlLocCode.DataValueField = "LocCode"
        ddlLocCode.DataTextField = "Description"
        ddlLocCode.SelectedIndex = SelectedIndex
        ddlLocCode.DataBind()
    End Sub


    Sub Button_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCd_Add As String = "HR_CLSTRX_EMPCODE_ADD"
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim strDummy As String
        Dim intErrNo As Integer
        Dim strSelectedLoc As String = ddlLocCode.SelectedItem.Value
        Dim strParam As String

        If strCmdArgs = "Generate" And strSelectedLoc <> "" Then
            Try
                strParam = txtNumberId.Text & "|" & strSelectedLoc
                intErrNo = objHRTrx.mtdGenerateEmployeeCode(strOpCd_Add, _
                                                            strParam, _
                                                            strDummy, _
                                                            False)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_DATA_GENEMPCODE_ADD&errmesg=" & Exp.ToString() & "&redirect=")
            End Try

            lblResult.Text = "<br>" & txtNumberId.Text & lblResultText.Text & "<br>"
            onLoad_Display(strSelectedLoc)
        ElseIf strCmdArgs = "Download" And strSelectedLoc <> "" Then
            Response.Redirect("HR_data_download_savefile.aspx?empcode=true&empcodeloc=" & strSelectedLoc)
        Else
            lblErrLoc.Visible=True
        End If
    End Sub


End Class
