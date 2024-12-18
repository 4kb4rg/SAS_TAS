Imports System
Imports System.Data
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction

Imports agri.Admin.clsComp
Imports agri.Admin.clsLoc
Imports agri.Admin.clsCountry
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.PWSystem.clsLangCap

Public Class GL_mthend_GCDist_Loc : Inherits Page

    Protected WithEvents dgResult As DataGrid
    Protected WithEvents txtAccMonth As TextBox
    Protected WithEvents txtAccYear As TextBox
    Protected WithEvents txtMature As TextBox
    Protected WithEvents txtImmature As TextBox
    Protected WithEvents tblSelection As HtmlTable
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdateBy As Label
    Protected WithEvents lblErrAllocation As Label
    Protected WithEvents lblErrAccCfg As Label
    Protected WithEvents lblHiddenStatus As Label
    Protected WithEvents lblErrAccPeriod As Label
    Protected WithEvents lblErrLoc As Label
    Protected WithEvents lblLocCode As Label
    Protected WithEvents btnAdd As ImageButton
    Protected WithEvents btnSave As ImageButton
    Protected WithEvents btnDistribute As ImageButton
    Protected WithEvents btnBack As ImageButton
    Protected WithEvents ddlLocation As DropDownList
    Protected WithEvents lblErrCountry As Label
    Protected WithEvents lblAccPeriod As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblErrSelect As Label
    Protected WithEvents lblLoc1 As Label
    Protected WithEvents lblGCSuccess As Label
    Protected WithEvents lblErrGCFail As Label
    Protected WithEvents lblErrGCNoAllocation As Label
    Protected WithEvents lblErrGCNoLocation As Label

    Dim objGLMthEnd As New agri.GL.clsMthEnd()
    Dim objAdmLoc As New agri.Admin.clsLoc()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    
    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intConfigSetting As String
    Dim strLangCode As String
    Dim intGLAR As Integer
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType as String

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        intConfigSetting = Session("SS_CONFIGSETTING")
        strLangCode = Session("SS_LANGCODE")
        intGLAR = Session("SS_GLAR")
        strLocType = Session("SS_LOCTYPE")
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLMthEndGCDist), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            lblGCSuccess.Visible = False
            lblErrGCFail.Visible = False
            lblErrGCNoAllocation.Visible = False
            lblErrGCNoLocation.Visible = False
            lblErrAccCfg.Visible = False
            lblErrLoc.Visible = False
            lblErrAllocation.Visible = False
            lblErrAccPeriod.Visible = False
            lblAccPeriod.Text = Request.QueryString("accperiod")

            If Not IsPostBack Then
                onLoad_Display(lblAccPeriod.Text)
                onLoad_DisplayLn(lblAccPeriod.Text)
                onLoad_Location(lblAccPeriod.Text)
                onLoad_BindButton()
            End If
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblLoc1.Text = GetCaption(objLangCap.EnumLangCap.Location)
        lblLocCode.Text = lblLoc1.Text
        lblErrLoc.text = lblErrSelect.text & lblLoc1.Text
        dgResult.Columns(0).headertext = lblLoc1.Text & lblCode.text
        dgResult.Columns(1).headertext = GetCaption(objLangCap.EnumLangCap.LocDesc)
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
                                                 "", _
                                                 "", _
                                                 objLangCapDs, _
                                                 strParam)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_MTHEND_GCDIST_LOC_LANGCAP&errmesg=" & Exp.ToString() & "&redirect=GL_mthend_GCDist.aspx")
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


    Sub onLoad_BindButton()
        txtAccMonth.Enabled = False
        txtAccYear.Enabled = False
        txtMature.Enabled = False
        txtImmature.Enabled = False
        tblSelection.Visible = False
        btnSave.Visible = False
        btnDistribute.Visible = False
        Select Case lblHiddenStatus.Text
            Case objGLMthEnd.EnumGCDistributeStatus.Active
                txtMature.Enabled = True
                txtImmature.Enabled = True
                tblSelection.Visible = True
                btnSave.Visible = True
                If (objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.GCDistributeByPreceedMth), intConfigSetting) = True) And _
                   (Convert.ToInt16(txtAccMonth.Text) < Convert.ToInt16(strAccMonth)) And _
                   (Convert.ToInt16(txtAccYear.Text) <= Convert.ToInt16(strAccYear)) Then
                    btnDistribute.Visible = True
                End If
            Case objGLMthEnd.EnumGCDistributeStatus.Distributed
            Case Else
                txtAccMonth.Enabled = True
                txtAccYear.Enabled = True
                txtMature.Enabled = True
                txtImmature.Enabled = True
                tblSelection.Visible = True
                btnSave.Visible = True
        End Select
    End Sub

    Sub onLoad_Display(ByVal pv_strAccPeriod As String)
        Dim objGLMthEndDs As New Dataset()
        Dim strOpCd As String = "GL_CLSMTHEND_GCDISTRIBUTE_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer

        If pv_strAccPeriod <> "" Then
            Try
                strParam = pv_strAccPeriod & "||||||||"
                intErrNo = objGLMthEnd.mtdGetGCDistribute(strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strOpCd, _
                                                        strParam, _
                                                        objGLMthEndDs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_MTHEND_GCDIST_DET_GET&errmesg=" & Exp.ToString() & "&redirect=GL_mthend_GCDist.aspx")
            End Try

            lblAccPeriod.Text = objGLMthEndDs.Tables(0).Rows(0).Item("AccMonth").Trim() & "/" & objGLMthEndDs.Tables(0).Rows(0).Item("AccYear").Trim()
            txtAccMonth.Text = objGLMthEndDs.Tables(0).Rows(0).Item("AccMonth")
            txtAccYear.Text = objGLMthEndDs.Tables(0).Rows(0).Item("AccYear")
            txtMature.Text = objGLMthEndDs.Tables(0).Rows(0).Item("MaturePercent")
            txtImmature.Text = objGLMthEndDs.Tables(0).Rows(0).Item("ImmaturePercent")
            lblStatus.Text = objGLMthEnd.mtdGetGCDistributeStatus(objGLMthEndDs.Tables(0).Rows(0).Item("Status"))
            lblHiddenStatus.Text = Convert.ToInt16(objGLMthEndDs.Tables(0).Rows(0).Item("Status"))
            lblDateCreated.Text = objGlobal.GetLongDate(objGLMthEndDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objGLMthEndDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdateBy.Text = objGLMthEndDs.Tables(0).Rows(0).Item("UserName")

            objGLMthEndDs = Nothing
        Else
            lblAccPeriod.Text = ""
            lblHiddenStatus.Text = 0
        End If
    End Sub


    Sub onLoad_DisplayLn(ByVal pv_strAccPeriod As String)
        Dim objGLMthEndDs As New Dataset()
        Dim strOpCd As String = "GL_CLSMTHEND_GCDISTRIBUTE_LINE_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim lbButton As LinkButton

        If pv_strAccPeriod <> "" Then
            Try
                strParam = pv_strAccPeriod & "|||||||HD.ActLocCode|ASC"
                intErrNo = objGLMthEnd.mtdGetGCDistribute(strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strOpCd, _
                                                        strParam, _
                                                        objGLMthEndDs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_MTHEND_GCDIST_LINE_GET&errmesg=" & Exp.ToString() & "&redirect=GL_mthend_GCDist.aspx")
            End Try

            dgResult.DataSource = objGLMthEndDs.Tables(0)
            dgResult.DataBind()

            For intCnt = 0 To dgResult.Items.Count - 1
                Select Case lblHiddenStatus.Text
                    Case objGLMthEnd.EnumGCDistributeStatus.Active
                            lbButton = dgResult.Items.Item(intCnt).FindControl("lbDelete")
                            lbButton.Visible = True
                            lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    Case objGLMthEnd.EnumGCDistributeStatus.Distributed
                            lbButton = dgResult.Items.Item(intCnt).FindControl("lbDelete")
                            lbButton.Visible = False
                End Select
            Next

            objGLMthEndDs = Nothing
        End If
    End Sub

    Sub onLoad_Location(ByVal pv_strAccPeriod As String)
        Dim objLocDs As New Dataset()
        Dim strOpCd As String = "GL_CLSMTHEND_GCDISTRIBUTE_LOC_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim dr As DataRow

        If pv_strAccPeriod = "" Then
            pv_strAccPeriod = "/"
        End If

        Try
            strParam = pv_strAccPeriod & "||||||||"
            intErrNo = objGLMthEnd.mtdGetGCDistribute(strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strAccMonth, _
                                                    strAccYear, _
                                                    strOpCd, _
                                                    strParam, _
                                                    objLocDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_MTHEND_GCDIST_LOCATION_GET&errmesg=" & Exp.ToString() & "&redirect=GL_mthend_GCDist.aspx")
        End Try

        dr = objLocDs.Tables(0).NewRow()
        dr("LocCode") = ""
        dr("Description") = lblSelect.text & lblLocCode.Text
        objLocDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlLocation.DataSource = objLocDs.Tables(0)
        ddlLocation.DataTextField = "Description"
        ddlLocation.DataValueField = "LocCode"
        ddlLocation.DataBind()

        objLocDs = Nothing
    End Sub


    Function mtdSaveHeader() As Boolean
        Dim strOpCd As String = ""
        Dim strOpCode_Upd As String = "GL_CLSMTHEND_GCDISTRIBUTE_UPD"
        Dim strOpCode_Add As String = "GL_CLSMTHEND_GCDISTRIBUTE_ADD"
        Dim strOpCode_Get As String = "GL_CLSMTHEND_GCDISTRIBUTE_GET"
        Dim strOpCode_Period As String = "GL_CLSMTHEND_GCDISTRIBUTE_SHPERIOD_GET"
        Dim objGLMthEndDs As New Dataset()
        Dim blnNew As Boolean
        Dim intErrNo As Integer
        Dim strParam As String = ""
        

        If (Convert.ToDouble(txtMature.Text) + Convert.ToDouble(txtImmature.Text)) <> 100 Then
            lblErrAllocation.Visible = True
            Return False
        End If

        If lblHiddenStatus.Text = objGLMthEnd.EnumGCDistributeStatus.Active Then
            blnNew = False
            strOpCd = strOpCode_Upd
        Else
            blnNew = True
            strOpCd = strOpCode_Add
        End If

        If blnNew = True Then
            Try
                strParam = "|" & Trim(txtAccMonth.Text) & "|" & txtAccYear.Text & "||||||"
                intErrNo = objGLMthEnd.mtdGetGCDistribute(strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strOpCode_Period, _
                                                        strParam, _
                                                        objGLMthEndDs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_MTHEND_GCDIST_SHPERIOD_CHECK&errmesg=" & Exp.ToString() & "&redirect=GL_mthend_GCDist_Loc.aspx?accperiod=" & lblAccPeriod.Text)
            End Try

            If objGLMthEndDs.Tables(0).Rows(0).Item("Records") = 1 Then
                Try
                    strParam = Trim(txtAccMonth.Text) & "/" & txtAccYear.Text & "||||||||"
                    intErrNo = objGLMthEnd.mtdGetGCDistribute(strCompany, _
                                                            strLocation, _
                                                            strUserId, _
                                                            strAccMonth, _
                                                            strAccYear, _
                                                            strOpCode_Get, _
                                                            strParam, _
                                                            objGLMthEndDs)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_MTHEND_GCDIST_CHECK&errmesg=" & Exp.ToString() & "&redirect=GL_mthend_GCDist_Loc.aspx?accperiod=" & lblAccPeriod.Text)
                End Try

                If objGLMthEndDs.Tables(0).Rows.Count > 0 Then
                    lblErrAccPeriod.Visible = True
                    Return False
                End If
            Else
                lblErrAccCfg.Visible = True
                Return False
            End If
        End If

        Try
            strParam = lblAccPeriod.Text & "|" & _
                        txtAccMonth.Text & "|" & _
                        txtAccYear.Text & "||" & _
                        txtMature.Text & "|" & _
                        txtImmature.Text & "|0|" & _
                        objGLMthEnd.EnumGCDistributeStatus.Active
            intErrNo = objGLMthEnd.mtdUpdGCDistribute(strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strAccMonth, _
                                                    strAccYear, _
                                                    strOpCd, _
                                                    strParam)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_MTHEND_GCDIST_UPD&errmesg=" & Exp.ToString() & "&redirect=GL_mthend_GCDist_Loc.aspx?accperiod=")
        End Try

        objGLMthEndDs = Nothing

        lblAccPeriod.Text = Trim(txtAccMonth.Text) & "/" & txtAccYear.Text
        lblHiddenStatus.Text = objGLMthEnd.EnumGCDistributeStatus.Active

        Return True
    End Function

    
    Sub btnSave_Click(Sender As Object, E As ImageClickEventArgs)
        If mtdSaveHeader = True Then
            onLoad_Display(lblAccPeriod.Text)
            onLoad_BindButton()
        End If
    End Sub

    Sub btnAdd_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCd_Add As String = "GL_CLSMTHEND_GCDISTRIBUTE_ADD"
        Dim strOpCd As String = "GL_CLSMTHEND_GCDISTRIBUTE_LINE_ADD"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim blnSaveData As Boolean = False

        If ddlLocation.SelectedItem.Value = "" Then
            lblErrLoc.Visible = True
            Exit Sub
        End If

        If lblAccPeriod.Text = "" Then
            blnSaveData = mtdSaveHeader()
        Else
            blnSaveData = True
        End If

        If blnSaveData = True Then
            Try
                strParam = lblAccPeriod.Text & "|" & _
                            txtAccMonth.Text & "|" & _
                            txtAccYear.Text & "|" & _
                            ddlLocation.SelectedItem.Value & "|||0|"
                intErrNo = objGLMthEnd.mtdUpdGCDistribute(strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strOpCd, _
                                                        strParam)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_MTHEND_GCDIST_LINE_ADD&errmesg=" & Exp.ToString() & "&redirect=GL_mthend_GCDist_Loc.aspx?accperiod=" & lblAccPeriod.Text)
            End Try

            lblAccPeriod.Text = Trim(txtAccMonth.Text) & "/" & txtAccYear.Text
            lblHiddenStatus.Text = objGLMthEnd.EnumGCDistributeStatus.Active
            onLoad_Display(lblAccPeriod.Text)
            onLoad_DisplayLn(lblAccPeriod.Text)
            onLoad_Location(lblAccPeriod.Text)
            onLoad_BindButton()
        End If
    End Sub


    Sub DEDR_Delete(Sender As Object, E As DataGridCommandEventArgs)
        Dim strOpCd As String = "GL_CLSMTHEND_GCDISTRIBUTE_LINE_DEL"
        Dim LocCodeCell As TableCell = E.Item.Cells(0)
        Dim intErrNo As Integer
        Dim strParam As String

        Try
            strParam = lblAccPeriod.Text & "|" & _
                        txtAccMonth.Text & "|" & _
                        txtAccYear.Text & "|" & _
                        LocCodeCell.Text & "||||"
            intErrNo = objGLMthEnd.mtdUpdGCDistribute(strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strAccMonth, _
                                                    strAccYear, _
                                                    strOpCd, _
                                                    strParam)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_MTHEND_GCDIST_LINE_DEL&errmesg=" & Exp.ToString() & "&redirect=GL_mthend_GCDist_Loc.aspx?accperiod=")
        End Try

        onLoad_DisplayLn(lblAccPeriod.Text)
        onLoad_Location(lblAccPeriod.Text)
    End Sub

    Sub btnDistribute_Click(Sender As Object, E As ImageClickEventArgs)
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strGCAccMonth As String = Trim(txtAccMonth.Text)
        Dim strGCAccYear As String = Trim(txtAccYear.Text)
        Dim intStatus As Integer

        Try
            intErrNo = objGLMthEnd.mtdDistributeGCProcess(strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strGCAccMonth, _
                                                        strGCAccYear, _
                                                        intConfigSetting, _
                                                        intStatus)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_MTHEND_GCDIST_PROCESS&errmesg=" & Exp.ToString() & "&redirect=GL_mthend_GCDist_Loc.aspx?accperiod=")
        End Try

        Select Case intStatus
            Case 0      
                lblGCSuccess.Visible = True
            Case 21     
                lblErrGCNoAllocation.Visible = True
            Case 22     
                lblErrGCNoLocation.Visible = True
            Case Else   
                lblErrGCFail.Visible = True
        End Select

        onLoad_Display(lblAccPeriod.Text)
        onLoad_DisplayLn(lblAccPeriod.Text)
        onLoad_Location(lblAccPeriod.Text)
        onLoad_BindButton()

        Select Case intStatus

        End Select
    End Sub

    Sub btnBack_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("GL_mthend_GCDist.aspx")
    End Sub

End Class
