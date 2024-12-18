
Imports System
Imports System.Math
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings


Public Class WS_TRX_MECHANIC_HOUR_DETAIL : Inherits Page

    Protected WithEvents dgMechHour As DataGrid

    Protected WithEvents lblCode As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblPleaseSelect As Label
    Protected WithEvents lblMechHourID As Label
    Protected WithEvents lblWorkingDateErr As Label
    Protected WithEvents lblPeriod As Label
    Protected WithEvents lblEmpCodeErr As Label
    Protected WithEvents lblStatusText As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDeleteDependency As Label
    Protected WithEvents lblCreateDate As Label
    Protected WithEvents lblUpdateDate As Label
    Protected WithEvents lblUserName As Label
    Protected WithEvents lblJobIDErr As Label
    Protected WithEvents lblWorkCodeTag As Label
    Protected WithEvents lblWorkCodeErr As Label
    Protected WithEvents lblTimeSpentErr As Label
    Protected WithEvents lblActionResult As Label

    Protected WithEvents ddlEmpCode As DropDownList
    Protected WithEvents ddlJobID As DropDownList
    Protected WithEvents ddlWorkCode As DropDownList

    Protected WithEvents txtWorkingDate As TextBox
    Protected WithEvents txtHourSpent As TextBox
    Protected WithEvents txtMinuteSpent As TextBox
    Protected WithEvents txtRemark As TextBox

    Protected WithEvents ibAdd As ImageButton
    Protected WithEvents ibSave As ImageButton
    Protected WithEvents ibDelete As ImageButton
    Protected WithEvents ibNew As ImageButton
    Protected WithEvents ibBack As ImageButton

    Protected WithEvents imgWorkingDate As Image

    Protected WithEvents revHourSpent As RegularExpressionValidator
    Protected WithEvents revMinuteSpent As RegularExpressionValidator

    Protected WithEvents tblMain As HtmlTable
    Protected WithEvents tblAdd As HtmlTable

    Protected WithEvents trMechHour As HtmlTableRow

    Protected objWSTrx As New agri.WS.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objLoc As New agri.Admin.clsLoc()


    Dim objWSSetup As New agri.WS.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objHRTrx As New agri.HR.clsTrx()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()

    Dim strOpCode_Mechanic_Hour_Add As String = "WS_CLSTRX_MECHANIC_HOUR_ADD"
    Dim strOpCode_Mechanic_Hour_Get As String = "WS_CLSTRX_MECHANIC_HOUR_GET"
    Dim strOpCode_Mechanic_Hour_Upd As String = "WS_CLSTRX_MECHANIC_HOUR_UPD"
    Dim strOpCode_Mechanic_Hour_Line_Add As String = "WS_CLSTRX_MECHANIC_HOUR_LINE_ADD"
    Dim strOpCode_Mechanic_Hour_Line_Del As String = "WS_CLSTRX_MECHANIC_HOUR_LINE_DEL"
    Dim strOpCode_Mechanic_Hour_Line_Get As String = "WS_CLSTRX_MECHANIC_HOUR_LINE_GET"
    Dim strOpCode_Job_Get As String = "WS_CLSTRX_JOB_GET"
    Dim strOpCode_Job_Upd As String = "WS_CLSTRX_JOB_UPD"

    Dim dsLangCap As New DataSet()

    Const APPEND_EMP_CODE As Boolean = True
    Const APPEND_JOB_ID As Boolean = False
    Const APPEND_WORK_CODE As Boolean = False

    Dim intErrNo As Integer
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intWSAR As Integer
    Dim intConfigsetting As Integer
    Dim intCnt As Integer
    Dim strLocType As String


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        intWSAR = Session("SS_WSAR")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")


        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSMechanicHour), intWSAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblWorkingDateErr.Visible = False
            lblEmpCodeErr.Visible = False
            lblJobIDErr.Visible = False
            lblWorkCodeErr.Visible = False
            lblTimeSpentErr.Visible = False
            lblActionResult.Visible = False

            If Not Page.IsPostBack Then
                GetLangCap()
                lblMechHourID.Text = Trim(Request.QueryString("mechhourid"))
                If lblMechHourID.Text = "" Then
                    ResetPage(False, False, True)
                    txtWorkingDate.Text = Trim(Request.QueryString("workingdate"))
                Else
                    ResetPage(True, True, True)
                End If
            End If
        End If
    End Sub

    Sub GetLangCap()
        dsLangCap = GetLanguageCaptionDS()

        lblWorkCodeTag.Text = GetCaption(objLangCap.EnumLangCap.Work) & lblCode.Text

        lblWorkCodeErr.Text = lblPleaseSelect.Text & lblWorkCodeTag.Text

        dgMechHour.Columns(1).HeaderText = lblWorkCodeTag.Text
    End Sub


    Function GetCaption(ByVal pv_TermCode As String) As String
        Dim I As Integer

        For I = 0 To dsLangCap.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(dsLangCap.Tables(0).Rows(I).Item("TermCode")) Then
                If strLocType = objLoc.EnumLocType.Mill Then
                    Return Trim(dsLangCap.Tables(0).Rows(I).Item("BusinessTermMW"))
                Else
                    Return Trim(dsLangCap.Tables(0).Rows(I).Item("BusinessTerm"))
                End If
                Exit For
            End If
        Next
    End Function


    Sub SetObjectAccessibilityByStatus()
        ddlEmpCode.Enabled = False
        ddlJobID.Enabled = False
        ddlWorkCode.Enabled = False

        txtWorkingDate.Enabled = False
        txtHourSpent.Enabled = False
        txtMinuteSpent.Enabled = False
        txtRemark.Enabled = False

        ibAdd.Visible = False
        ibSave.Visible = False
        ibDelete.Visible = False
        ibNew.Visible = False

        imgWorkingDate.Visible = False

        revHourSpent.Enabled = False
        revMinuteSpent.Enabled = False

        tblAdd.Visible = False

        Select Case Trim(lblStatus.Text)
            Case Trim(CStr(objWSTrx.EnumMechanicHourStatus.Deleted))
                ibNew.Visible = True
                ibDelete.Visible = True
                ibDelete.ImageUrl = "../../images/butt_undelete.gif"
                ibDelete.AlternateText = "Undelete"
                ibDelete.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
            Case Trim(CStr(objWSTrx.EnumMechanicHourStatus.Closed))
                ibNew.Visible = True
            Case Else
                If Trim(lblStatus.Text) = Trim(CStr(objWSTrx.EnumJobStatus.Active)) Or Trim(lblMechHourID.Text) = "" Then
                    ddlJobID.Enabled = True
                    ddlWorkCode.Enabled = True

                    txtHourSpent.Enabled = True
                    txtMinuteSpent.Enabled = True
                    txtRemark.Enabled = True

                    ibAdd.Visible = True
                    ibSave.Visible = True

                    revHourSpent.Enabled = True
                    revMinuteSpent.Enabled = True

                    tblAdd.Visible = True
                End If
                If dgMechHour.Items.Count = 0 Then
                    txtWorkingDate.Enabled = True
                    imgWorkingDate.Visible = True
                    ddlEmpCode.Enabled = True
                End If

                If Trim(lblStatus.Text) = Trim(CStr(objWSTrx.EnumJobStatus.Active)) Then
                    ibNew.Visible = True

                    If Not (CInt(Trim(lblDeleteDependency.Text)) <> 0) Then
                        ibDelete.Visible = True
                        ibDelete.ImageUrl = "../../images/butt_delete.gif"
                        ibDelete.AlternateText = "Delete"
                        ibDelete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    End If
                End If
        End Select
    End Sub

    Sub ResetPage(ByVal blnHeader As Boolean, ByVal blnLines As Boolean, ByVal blnAdd As Boolean)
        If blnHeader = True Then
            DisplayMechanicHourHeader()
        Else
            txtWorkingDate.Text = ""
            lblPeriod.Text = ""
            BindEmpCodeDropDownList(Session("SS_LOCATION"), "")
            lblStatus.Text = ""
            lblStatusText.Text = ""
            lblCreateDate.Text = ""
            lblUpdateDate.Text = ""
            lblUserName.Text = ""
        End If

        If blnLines = True Then
            DisplayMechanicHourLines()
        End If

        If blnAdd = True Then
            BindJobIDDropDownList(Session("SS_LOCATION"), "")
            BindWorkCodeDropDownList(GetDropDownListValue(ddlJobID), "")
            txtHourSpent.Text = ""
            txtMinuteSpent.Text = ""
            txtRemark.Text = ""
        End If

        SetObjectAccessibilityByStatus()
    End Sub

    Sub BindEmpCodeDropDownList(ByVal pv_strLocCode As String, ByVal pv_strEmpCode As String)
        Dim strOpCd As String = "HR_CLSSETUP_EMPLOYEE_LIST_GET"
        Dim intSelectedIndex As Integer = 0
        Dim dsList As DataSet
        Dim drNew As DataRow
        Dim intCnt As Integer
        Dim strParam As String

        strParam = "ORDER BY Mst.EmpCode|AND Mst.LocCode = '" & FixSQL(pv_strLocCode) & "' AND Mst.Status = '" & objHRTrx.EnumEmpStatus.Active & "' AND Det.MechInd = '" & objHRTrx.mtdGetMechStatus(objHRTrx.EnumMechStatus.Yes) & "'"
        Try
            intErrNo = objHRSetup.mtdGetMasterList(strOpCd, strParam, 1, dsList)
        Catch ex As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_MECHANIC_HOUR_DETAIL_BIND_EMPLOYEE&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_MechanicHour_List.aspx")
        End Try

        For intCnt = 0 To dsList.Tables(0).Rows.Count - 1
            dsList.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(dsList.Tables(0).Rows(intCnt).Item("EmpCode"))
            dsList.Tables(0).Rows(intCnt).Item("EmpName") = Trim(dsList.Tables(0).Rows(intCnt).Item("EmpCode")) & " (" & Trim(dsList.Tables(0).Rows(intCnt).Item("EmpName")) & ")"
            If LCase(Trim(dsList.Tables(0).Rows(intCnt).Item("EmpCode"))) = LCase(Trim(pv_strEmpCode)) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        drNew = dsList.Tables(0).NewRow()
        drNew("EmpCode") = ""
        drNew("EmpName") = lblSelect.Text & "Employee Code"
        dsList.Tables(0).Rows.InsertAt(drNew, 0)

        If Trim(pv_strEmpCode) <> "" And intSelectedIndex = 0 And APPEND_EMP_CODE = True Then
            drNew = dsList.Tables(0).NewRow()
            drNew("EmpCode") = Trim(pv_strEmpCode)
            drNew("EmpName") = Trim(pv_strEmpCode) & " (Deleted)"
            dsList.Tables(0).Rows.InsertAt(drNew, dsList.Tables(0).Rows.Count)
            intSelectedIndex = dsList.Tables(0).Rows.Count - 1
        End If

        ddlEmpCode.DataSource = dsList.Tables(0)
        ddlEmpCode.DataValueField = "EmpCode"
        ddlEmpCode.DataTextField = "EmpName"
        ddlEmpCode.DataBind()
        ddlEmpCode.SelectedIndex = intSelectedIndex

        If Not dsList Is Nothing Then
            dsList = Nothing
        End If
    End Sub

    Sub BindJobIDDropDownList(ByVal pv_strLocCode As String, ByVal pv_strJobID As String)
        Dim strOpCd As String = "WS_CLSTRX_JOB_GET"
        Dim colParam As New Collection
        Dim intSelectedIndex As Integer = 0
        Dim dsList As DataSet
        Dim drNew As DataRow
        Dim intCnt As Integer
        Dim strSearch As String
        Dim strErrMsg As String

        strSearch = "WHERE J.LocCode = '" & FixSQL(pv_strLocCode) & "' " & vbCrLf & _
                    "  AND J.Status = '" & objWSTrx.EnumJobStatus.Active & "' " & vbCrLf & _
                    "ORDER BY J.JobID"
        colParam.Add(strSearch, "PM_SEARCH")
        colParam.Add(strOpCd, "OC_JOB_GET")
        Try
            intErrNo = objWSTrx.mtdJob_Get(colParam, dsList, strErrMsg)
            If intErrNo <> objWSTrx.EnumException.NoError Then
                Throw New System.Exception(strErrMsg)
            End If
        Catch ex As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_MECHANIC_HOUR_DETAIL_BIND_JOBID&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_MechanicHour_List.aspx")
        End Try

        For intCnt = 0 To dsList.Tables(0).Rows.Count - 1
            dsList.Tables(0).Rows(intCnt).Item("JobID") = Trim(dsList.Tables(0).Rows(intCnt).Item("JobID"))
            dsList.Tables(0).Rows(intCnt).Item("Description") = Trim(dsList.Tables(0).Rows(intCnt).Item("JobID")) & " (" & Trim(dsList.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If LCase(Trim(dsList.Tables(0).Rows(intCnt).Item("JobID"))) = LCase(Trim(pv_strJobID)) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        drNew = dsList.Tables(0).NewRow()
        drNew("JobID") = ""
        drNew("Description") = lblSelect.Text & "Job ID"
        dsList.Tables(0).Rows.InsertAt(drNew, 0)

        If Trim(pv_strJobID) <> "" And intSelectedIndex = 0 And APPEND_JOB_ID = True Then
            drNew = dsList.Tables(0).NewRow()
            drNew("JobID") = Trim(pv_strJobID)
            drNew("Description") = Trim(pv_strJobID) & " (Deleted)"
            dsList.Tables(0).Rows.InsertAt(drNew, dsList.Tables(0).Rows.Count)
            intSelectedIndex = dsList.Tables(0).Rows.Count - 1
        End If

        ddlJobID.DataSource = dsList.Tables(0)
        ddlJobID.DataValueField = "JobID"
        ddlJobID.DataTextField = "Description"
        ddlJobID.DataBind()
        ddlJobID.SelectedIndex = intSelectedIndex

        If Not dsList Is Nothing Then
            dsList = Nothing
        End If
    End Sub

    Sub BindWorkCodeDropDownList(ByVal pv_strJobID As String, ByVal pv_strWorkCode As String)
        Dim strOpCd As String = "WS_CLSTRX_JOBWORKCODE_GET"
        Dim colParam As New Collection
        Dim intSelectedIndex As Integer = 0
        Dim dsList As DataSet
        Dim drNew As DataRow
        Dim intCnt As Integer
        Dim strSearch As String
        Dim strErrMsg As String

        strSearch = "WHERE J.JobID = '" & FixSQL(Trim(pv_strJobID)) & "'" & vbCrLf & "ORDER BY WC.WorkCode"
        colParam.Add(strSearch, "PM_SEARCH")
        colParam.Add(strOpCd, "OC_JOB_WORK_CODE_GET")
        Try
            intErrNo = objWSTrx.mtdJobWorkCode_Get(colParam, dsList, strErrMsg)
            If intErrNo <> objWSTrx.EnumException.NoError Then
                Throw New System.Exception(strErrMsg)
            End If
        Catch ex As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_MECHANIC_HOUR_DETAIL_BIND_WORKCODE&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_MechanicHour_List.aspx")
        End Try

        For intCnt = 0 To dsList.Tables(0).Rows.Count - 1
            dsList.Tables(0).Rows(intCnt).Item("WorkCode") = Trim(dsList.Tables(0).Rows(intCnt).Item("WorkCode"))
            dsList.Tables(0).Rows(intCnt).Item("Description") = Trim(dsList.Tables(0).Rows(intCnt).Item("WorkCode")) & " (" & Trim(dsList.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If LCase(Trim(dsList.Tables(0).Rows(intCnt).Item("WorkCode"))) = LCase(Trim(pv_strWorkCode)) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        drNew = dsList.Tables(0).NewRow()
        drNew("WorkCode") = ""
        drNew("Description") = lblSelect.Text & lblWorkCodeTag.Text
        dsList.Tables(0).Rows.InsertAt(drNew, 0)

        If Trim(pv_strWorkCode) <> "" And intSelectedIndex = 0 And APPEND_WORK_CODE = True Then
            drNew = dsList.Tables(0).NewRow()
            drNew("WorkCode") = Trim(pv_strWorkCode)
            drNew("Description") = Trim(pv_strWorkCode) & " (Deleted)"
            dsList.Tables(0).Rows.InsertAt(drNew, dsList.Tables(0).Rows.Count)
            intSelectedIndex = dsList.Tables(0).Rows.Count - 1
        End If

        ddlWorkCode.DataSource = dsList.Tables(0)
        ddlWorkCode.DataValueField = "WorkCode"
        ddlWorkCode.DataTextField = "Description"
        ddlWorkCode.DataBind()
        ddlWorkCode.SelectedIndex = intSelectedIndex

        If Not dsList Is Nothing Then
            dsList = Nothing
        End If
    End Sub

    Sub ddlJobID_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        BindWorkCodeDropDownList(GetDropDownListValue(ddlJobID), "")
    End Sub

    Protected Function GetMechanicHourHeaderDs() As DataSet
        Dim dsTemp As New DataSet
        Dim colParam As New Collection
        Dim strSearch As String
        Dim strErrMsg As String

        Try
            strSearch = " WHERE MH.MechHourID = '" & FixSQL(Trim(lblMechHourID.Text)) & "'"
            colParam.Add(strSearch, "PM_SEARCH")
            colParam.Add(strOpCode_Mechanic_Hour_Get, "OC_MECHANIC_HOUR_GET")

            intErrNo = objWSTrx.mtdMechanicHour_Get(colParam, dsTemp, strErrMsg)
            If intErrNo <> objWSTrx.EnumException.NoError Then
                Throw New System.Exception(strErrMsg)
            End If

            Return dsTemp
        Catch ex As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_MECHANIC_HOUR_DETAIL_GET_MECHANIC_HOUR_HEADER&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_MechanicHour_List.aspx")
        Finally
            If Not dsTemp Is Nothing Then
                dsTemp = Nothing
            End If
        End Try
    End Function

    Protected Function GetMechanicHourLinesDs() As DataSet
        Dim dsTemp As New DataSet
        Dim colParam As New Collection
        Dim strSearch As String
        Dim strErrMsg As String

        Try
            strSearch = " WHERE MHL.MechHourID = '" & FixSQL(Trim(lblMechHourID.Text)) & "'"
            colParam.Add(strSearch, "PM_SEARCH")
            colParam.Add(strOpCode_Mechanic_Hour_Line_Get, "OC_MECHANIC_HOUR_LINE_GET")

            intErrNo = objWSTrx.mtdMechanicHourLine_Get(colParam, dsTemp, strErrMsg)
            If intErrNo <> objWSTrx.EnumException.NoError Then
                Throw New System.Exception(strErrMsg)
            End If

            Return dsTemp
        Catch ex As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_MECHANIC_HOUR_DETAIL_GET_MECHANIC_HOUR_LINE&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_MechanicHour_List.aspx")
        Finally
            If Not dsTemp Is Nothing Then
                dsTemp = Nothing
            End If
        End Try
    End Function

    Function GetLanguageCaptionDS() As DataSet
        Dim strOpCode_BussTerm As String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
        Dim dsLC As DataSet
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
                                                 dsLC, _
                                                 strParam)
        Catch ex As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_MECHANIC_HOUR_DETAIL_GET_LANGCAP&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_MechanicHour_List.aspx")
        End Try

        Return dsLC
        If Not dsLC Is Nothing Then
            dsLC = Nothing
        End If
    End Function

    Sub DisplayMechanicHourHeader()
        Dim intCnt As Integer
        Dim dsHeader As DataSet
        dsHeader = GetMechanicHourHeaderDs()
        lblMechHourID.Text = Trim(dsHeader.Tables(0).Rows(0).Item("MechHourID"))
        txtWorkingDate.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Trim(dsHeader.Tables(0).Rows(0).Item("WorkingDate")))
        BindEmpCodeDropDownList(Trim(dsHeader.Tables(0).Rows(0).Item("LocCode")), Trim(dsHeader.Tables(0).Rows(0).Item("EmpCode")))

        lblPeriod.Text = Trim(dsHeader.Tables(0).Rows(0).Item("AccMonth")) & "/" & Trim(dsHeader.Tables(0).Rows(0).Item("AccYear"))
        lblStatus.Text = Trim(dsHeader.Tables(0).Rows(0).Item("Status"))
        lblStatusText.Text = objWSTrx.mtdGetMechanicHourStatus(lblStatus.Text)
        lblDeleteDependency.Text = dsHeader.Tables(0).Rows(0).Item("DeleteDependency")
        lblCreateDate.Text = objGlobal.GetLongDate(dsHeader.Tables(0).Rows(0).Item("CreateDate"))
        lblUpdateDate.Text = objGlobal.GetLongDate(dsHeader.Tables(0).Rows(0).Item("UpdateDate"))
        lblUserName.Text = Trim(dsHeader.Tables(0).Rows(0).Item("UserName"))

        SetObjectAccessibilityByStatus()
    End Sub

    Sub DisplayMechanicHourLines()
        dgMechHour.DataSource = GetMechanicHourLinesDs()
        dgMechHour.DataBind()
    End Sub

    Sub dgMechHour_OnItemDataBound(ByVal Sender As Object, ByVal e As DataGridItemEventArgs)
        Dim lbTemp As LinkButton
        Dim lblTemp As Label

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            lbTemp = e.Item.FindControl("lbDelete")
            lblTemp = e.Item.FindControl("lblDNID")
            If Trim(lblStatus.Text) = Trim(CStr(objWSTrx.EnumJobStatus.Active)) And Trim(lblTemp.Text) = "" Then
                lbTemp.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                lbTemp.Visible = True
            Else
                lbTemp.Visible = False
            End If
        End If
    End Sub

    Sub dgMechHour_OnDeleteCommand(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim colParam As New Collection
        Dim lblTemp As Label
        Dim strErrMsg As String

        colParam.Add(Trim(lblMechHourID.Text), "PM_MECHHOURID")
        lblTemp = E.Item.FindControl("lblMechHourLnID")
        colParam.Add(Trim(lblTemp.Text), "PM_MECHHOURLNID")
        colParam.Add(Trim(strUserId), "PM_UPDATEID")
        colParam.Add(strOpCode_Mechanic_Hour_Get, "OC_MECHANIC_HOUR_GET")
        colParam.Add(strOpCode_Mechanic_Hour_Upd, "OC_MECHANIC_HOUR_UPD")
        colParam.Add(strOpCode_Mechanic_Hour_Line_Get, "OC_MECHANIC_HOUR_LINE_GET")
        colParam.Add(strOpCode_Mechanic_Hour_Line_Del, "OC_MECHANIC_HOUR_LINE_DEL")

        Try
            intErrNo = objWSTrx.mtdMechanicHourLine_Delete(colParam, strErrMsg)

            If intErrNo <> objWSTrx.EnumException.NoError Then
                lblActionResult.Text = "Cannot perform the operation. " & strErrMsg
                lblActionResult.Visible = True
            Else
                ResetPage(True, True, True)
            End If
        Catch ex As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_MECHANIC_HOUR_DETAIL_ON_DELETE_COMMAND&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_MechanicHour_List.aspx")
        End Try
    End Sub

    Function mtdSaveMechanicHourHeader(ByVal pv_blnResetPage As Boolean) As Boolean
        Dim colParam As New Collection
        Dim strErrMsg As String
        Dim strMechHourID As String = Trim(lblMechHourID.Text)
        Dim strWorkingDate As String = Trim(txtWorkingDate.Text)
        Dim strEmpCode As String = Trim(GetDropDownListValue(ddlEmpCode))
        Dim strStatus As String = Trim(lblStatus.Text)

        If strWorkingDate = "" Then
            lblWorkingDateErr.Text = "Working Date cannot be blank"
            lblWorkingDateErr.Visible = True
        Else
            strWorkingDate = GetValidDate(strWorkingDate, strErrMsg)
            If strWorkingDate = "" Then
                lblWorkingDateErr.Text = strErrMsg
                lblWorkingDateErr.Visible = True
            End If
        End If
        If strEmpCode = "" Then
            lblEmpCodeErr.Visible = True
        End If

        mtdSaveMechanicHourHeader = False
        If lblWorkingDateErr.Visible = False And lblEmpCodeErr.Visible = False Then
            If strMechHourID = "" Then
                colParam.Add(Trim(strLocation), "PM_LOCCODE")
                colParam.Add(strWorkingDate, "PM_WORKINGDATE")
                colParam.Add(strEmpCode, "PM_EMPCODE")
                colParam.Add(Trim(strAccMonth), "PM_ACCMONTH")
                colParam.Add(Trim(strAccYear), "PM_ACCYEAR")
                colParam.Add(Trim(strUserId), "PM_UPDATEID")
                colParam.Add(strOpCode_Mechanic_Hour_Add, "OC_MECHANIC_HOUR_ADD")
                Try
                    intErrNo = objWSTrx.mtdMechanicHour_Add(colParam, strMechHourID, strErrMsg)
                    If intErrNo = objWSTrx.EnumException.NoError Then
                        lblMechHourID.Text = strMechHourID
                        If pv_blnResetPage = True Then
                            ResetPage(True, True, True)
                        End If
                        mtdSaveMechanicHourHeader = True
                    Else
                        lblActionResult.Text = strErrMsg
                        lblActionResult.Visible = True
                    End If
                Catch ex As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_MECHANIC_HOUR_DETAIL_ADD&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_MechanicHour_List.aspx")
                End Try
            Else
                colParam.Add(strMechHourID, "PM_MECHHOURID")
                colParam.Add(strWorkingDate, "PM_WORKINGDATE")
                colParam.Add(strEmpCode, "PM_EMPCODE")
                colParam.Add(Trim(strUserId), "PM_UPDATEID")

                colParam.Add("false", "PM_UPDATE_HEADER")
                colParam.Add("false", "PM_UPDATE_ACCOUNT_PERIOD")
                colParam.Add("true", "PM_UPDATE_DETAIL")
                colParam.Add("false", "PM_UPDATE_STATUS")

                colParam.Add(strOpCode_Mechanic_Hour_Upd, "OC_MECHANIC_HOUR_UPD")
                Try
                    intErrNo = objWSTrx.mtdMechanicHour_Update(colParam, strErrMsg)
                    If intErrNo = objWSTrx.EnumException.NoError Then
                        If pv_blnResetPage = True Then
                            ResetPage(True, True, True)
                        End If
                        mtdSaveMechanicHourHeader = True
                    Else
                        lblActionResult.Text = strErrMsg
                        lblActionResult.Visible = True
                    End If
                Catch ex As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_MECHANIC_HOUR_DETAIL_UPDATE&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_MechanicHour_List.aspx")
                End Try
            End If
        End If
    End Function

    Sub ibAdd_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim colParam As New Collection
        Dim strErrMsg As String
        Dim strMechHourLnID As String
        Dim strJobID As String = Trim(GetDropDownListValue(ddlJobID))
        Dim strWorkCode As String = Trim(GetDropDownListValue(ddlWorkCode))
        Dim strHourSpent As String = Trim(txtHourSpent.Text)
        Dim strMinuteSpent As String = Trim(txtMinuteSpent.Text)
        Dim strRemark As String = Trim(txtRemark.Text)

        If strJobID = "" Then
            lblJobIDErr.Visible = True
        End If
        If strWorkCode = "" Then
            lblWorkCodeErr.Visible = True
        End If
        If IsNumeric(strHourSpent) = False Or IsNumeric(strMinuteSpent) = False Then
            lblTimeSpentErr.Text = "Time spent must be number"
            lblTimeSpentErr.Visible = True
        ElseIf CInt(strHourSpent) <= 0 And CInt(strMinuteSpent) <= 0 Then
            lblTimeSpentErr.Text = "Time spent cannot be zero"
            lblTimeSpentErr.Visible = True
        ElseIf CInt(strHourSpent) > 23 Then
            lblTimeSpentErr.Text = "Time spent (hour) cannot be larger than 23"
            lblTimeSpentErr.Visible = True
        ElseIf CInt(strMinuteSpent) > 59 Then
            lblTimeSpentErr.Text = "Time spent (minute) cannot be larger than 59"
            lblTimeSpentErr.Visible = True
        End If

        If lblJobIDErr.Visible = True Or lblWorkCodeErr.Visible = True Or lblTimeSpentErr.Visible = True Then
            Exit Sub
        End If

        If mtdSaveMechanicHourHeader(False) = False Then
            Exit Sub
        End If

        colParam.Add(Trim(lblMechHourID.Text), "PM_MECHHOURID")
        colParam.Add(strJobID, "PM_JOBID")
        colParam.Add(strWorkCode, "PM_WORKCODE")
        colParam.Add(strHourSpent, "PM_HOURSPENT")
        colParam.Add(strMinuteSpent, "PM_MINUTESPENT")
        colParam.Add(strRemark, "PM_REMARK")
        colParam.Add(Trim(strUserId), "PM_UPDATEID")
        colParam.Add(lblWorkCodeTag.Text, "LC_WORKCODE")

        colParam.Add(strOpCode_Mechanic_Hour_Get, "OC_MECHANIC_HOUR_GET")
        colParam.Add(strOpCode_Mechanic_Hour_Upd, "OC_MECHANIC_HOUR_UPD")
        colParam.Add(strOpCode_Mechanic_Hour_Line_Get, "OC_MECHANIC_HOUR_LINE_GET")
        colParam.Add(strOpCode_Mechanic_Hour_Line_Add, "OC_MECHANIC_HOUR_LINE_ADD")
        colParam.Add(strOpCode_Job_Get, "OC_JOB_GET")
        colParam.Add(strOpCode_Job_Upd, "OC_JOB_UPD")

        Try
            intErrNo = objWSTrx.mtdMechanicHourLine_Add(colParam, strMechHourLnID, strErrMsg)

            If intErrNo <> objWSTrx.EnumException.NoError Then
                lblActionResult.Text = "Cannot perform the operation. " & strErrMsg
                lblActionResult.Visible = True
            Else
                ResetPage(True, True, True)
            End If
        Catch ex As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_MECHANIC_HOUR_DETAIL_ADD_LINE&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_MechanicHour_List.aspx")
        End Try
    End Sub

    Sub ibSave_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        If mtdSaveMechanicHourHeader(True) = False Then
            Exit Sub
        End If

    End Sub

    Sub ibDelete_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim colParam As New Collection
        Dim strErrMsg As String

        colParam.Add(Trim(lblMechHourID.Text), "PM_MECHHOURID")
        colParam.Add(strUserId, "PM_UPDATEID")
        colParam.Add(strOpCode_Mechanic_Hour_Get, "OC_MECHANIC_HOUR_GET")
        colParam.Add(strOpCode_Mechanic_Hour_Upd, "OC_MECHANIC_HOUR_UPD")
        Try
            If Trim(lblStatus.Text) = Trim(CStr(objWSTrx.EnumMechanicHourStatus.Active)) Then
                intErrNo = objWSTrx.mtdMechanicHour_Delete(colParam, strErrMsg)
            Else
                intErrNo = objWSTrx.mtdMechanicHour_Undelete(colParam, strErrMsg)
            End If

            If intErrNo <> objWSTrx.EnumException.NoError Then
                lblActionResult.Text = "Cannot perform the operation. " & strErrMsg
                lblActionResult.Visible = True
            Else
                ResetPage(True, True, True)
            End If
        Catch ex As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_MECHANIC_HOUR_DETAIL_DELETE_UNDELETE&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_MechanicHour_List.aspx")
        End Try
    End Sub

    Sub ibNew_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("WS_Trx_MechanicHour_Detail.aspx?workingdate=" & Trim(txtWorkingDate.Text))
    End Sub

    Sub ibBack_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("WS_Trx_MechanicHour_List.aspx")
    End Sub

    Protected Function FixSQL(ByVal pv_strParam As String) As String
        Return Replace(Trim(pv_strParam), "'", "''")
    End Function

    Protected Function RoundNumber(ByVal d As Double, Optional ByVal decimals As Double = 0) As Decimal
        RoundNumber = Decimal.Divide(Int(Decimal.Add(Decimal.Multiply(d, 10.0 ^ decimals), 0.5)), 10.0 ^ decimals)
    End Function

    Protected Function GetDropDownListValue(ByRef pr_ddlObject As DropDownList) As String
        If Trim(Request.Form(pr_ddlObject.ID)) <> "" Then
            GetDropDownListValue = Trim(Request.Form(pr_ddlObject.ID))
        Else
            GetDropDownListValue = pr_ddlObject.SelectedItem.Value
        End If
    End Function

    Protected Function GetValidDate(ByVal pv_strInputDate As String, ByRef pr_strErrMsg As String) As String
        Dim strDateFormat As String
        Dim strSQLDate As String

        If objGlobal.mtdValidInputDate(Session("SS_DATEFMT"), _
                                       pv_strInputDate, _
                                       strDateFormat, _
                                       strSQLDate) = True Then
            GetValidDate = strSQLDate
            pr_strErrMsg = ""
        Else
            GetValidDate = ""
            pr_strErrMsg = "Date format should be in " & strDateFormat
        End If
    End Function

End Class
