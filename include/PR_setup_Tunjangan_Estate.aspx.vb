Imports System
Imports System.Data
Imports System.Collections 
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic

Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.HR
Imports agri.GL
Imports agri.PWSystem.clsLangCap

Public Class PR_Setup_Tunjangan_Estate : Inherits Page

    Protected WithEvents EventData As DataGrid
    Protected WithEvents ErrorMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents SQLStatement As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents StatusList As DropDownList
    Protected WithEvents srchStatusList As DropDownList
    Protected WithEvents SortExpression As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents curStatus As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblDupMsg As Label
    Protected WithEvents sortcol As Label

    Protected WithEvents srchDeptCode As TextBox
    Protected WithEvents srchName As TextBox
    Protected WithEvents isNew As HtmlInputHidden
    

    Protected objHR As New agri.HR.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim strOppCd_GET As String = "PR_PR_STP_TUNJANGAN_GET"
    Dim strOppCd_UPD As String = "PR_PR_STP_TUNJANGAN_UPD"

    Dim objDataSet As New Object()
    Dim objLangCapDs As New Object()
    Dim intErrNo As Integer
    Dim strParam As String = ""

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim ObjOK As New agri.GL.ClsTrx
    Dim strCompany As String
    Dim strLocation As String

    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intHRAR As Long
    Dim strLocType As String
    Dim ParamNama As String
    Dim ParamValue As String


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        intHRAR = Session("SS_HRAR")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        'ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRDepartment), intHRAR) = False Then
        '    Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.Text = "" Then
                SortExpression.Text = "CatId"
                sortcol.Text = "ASC"
            End If
            If Not Page.IsPostBack Then
                BindGrid()
            End If

        End If
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        EventData.CurrentPageIndex = 0
        EventData.EditItemIndex = -1
        BindGrid()
        
    End Sub

    Sub BindGrid()

        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadData()
        EventData.DataSource = dsData
        EventData.DataBind()
    End Sub


    Protected Function LoadData() As DataSet

        Dim SearchStr As String
        Dim sortitem As String


        SearchStr = "WHERE idTnj like '%" & srchDeptCode.Text & "%' AND Description like '%" & srchName.Text & "%' "

        sortitem = ""
        ParamNama = "SEARCH|SORT"
        ParamValue = SearchStr & "|" & sortitem

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOppCd_GET, ParamNama, ParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=HR_DEPTCODE_GET_LIST&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        Return objDataSet
    End Function

    
    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim Validator As RequiredFieldValidator

        EventData.EditItemIndex = CInt(E.Item.ItemIndex)

        BindGrid()
        If CInt(E.Item.ItemIndex) >= EventData.Items.Count Then
            EventData.EditItemIndex = -1
            Exit Sub
        End If

        EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("BlkGrpCode")
        EditText.ReadOnly = True

        Validator = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("ValidateCode")
        Validator.ErrorMessage = "Please insert kode tunjangan"
        Validator = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("ValidateDesc")
        Validator.ErrorMessage = "Please insert deskripsi"

        isNew.Value = "False"
    End Sub

    Sub DEDR_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim DivCode As String
        Dim Description As String
		Dim isPPH As String

        Dim blnDupKey As Boolean = False

        EditText = E.Item.FindControl("BlkGrpCode")
        DivCode = EditText.Text
        EditText = E.Item.FindControl("Description")
        Description = EditText.Text
		EditText = E.Item.FindControl("isPPH21")
        isPPH = EditText.Text


        ParamNama = "CI|CN|PPH"
        ParamValue = DivCode & "|" & _
                     Description & "|" & _
                     isPPH


        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOppCd_UPD, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_UPDATE_DEPTCODE&errmesg=" & lblErrMessage.Text & "&redirect=HR/Setup/HR_setup_DeptCode.aspx")
        End Try

        EventData.EditItemIndex = -1
        BindGrid()
        isNew.Value = "False"
    End Sub

    Sub DEDR_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        isNew.Value = "False"
        EventData.EditItemIndex = -1
        BindGrid()
    End Sub


    Sub DEDR_Add(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim dataSet As DataSet = LoadData()
        Dim newRow As DataRow
        Dim Validator As RequiredFieldValidator
        Dim PageCount As Integer

        newRow = dataSet.Tables(0).NewRow()
        newRow.Item("idTnj") = ""
        newRow.Item("Description") = ""
		newRow.Item("isPPH21") = "0"
        dataSet.Tables(0).Rows.Add(newRow)

        EventData.DataSource = dataSet
        EventData.DataBind()
        EventData.EditItemIndex = EventData.Items.Count - 1
        EventData.DataBind()

        Validator = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateCode")
        Validator.ErrorMessage = "Please insert kode tunjangan"
        Validator = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateDesc")
        Validator.ErrorMessage = "Please insert deskripsi"
        isNew.Value = "True"
    End Sub

End Class
