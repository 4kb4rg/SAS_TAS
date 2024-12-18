
Imports System
Imports System.Data
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports System.Globalization
Imports System.Math 


Public Class GL_trx_BudgetVeh_Details : Inherits Page

    Dim objGLTrx As New agri.GL.clsTrx()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Protected WithEvents lblPleaseSelect As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblCreateDate As Label
    Protected WithEvents lblVehicle As Label
    Protected WithEvents ddlVehCode As DropDownList
    Protected WithEvents lblErrVehicle As Label
    Protected WithEvents lblLastUpdate As Label

    Protected WithEvents txtYearBudget As TextBox
    Protected WithEvents lblUpdateBy As Label

    Protected WithEvents lblCode As Label

    Protected WithEvents lblValueError As Label

    Protected WithEvents txtb01 As TextBox
    Protected WithEvents txtb02 As TextBox
    Protected WithEvents txtb03 As TextBox
    Protected WithEvents txtb04 As TextBox
    Protected WithEvents txtb05 As TextBox
    Protected WithEvents txtb06 As TextBox
    Protected WithEvents txtb07 As TextBox
    Protected WithEvents txtb08 As TextBox
    Protected WithEvents txtb09 As TextBox
    Protected WithEvents txtb10 As TextBox
    Protected WithEvents txtb11 As TextBox
    Protected WithEvents txtb12 As TextBox

    Protected WithEvents btnSave As ImageButton
    Protected WithEvents btnDelete As ImageButton
    Protected WithEvents btnBack As ImageButton

    Protected WithEvents hidAccYear As HtmlInputHidden
    Protected WithEvents hidVehCode As HtmlInputHidden

    Dim objAccDs As New Dataset()
    Dim objBlkDs As New Dataset()
    Dim objVehDs As New Dataset()

    Dim strParam As String
    Dim objDataSet As New DataSet()
    Dim objLangCapDs As New Object()
    Dim intErrNo As Integer

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intGLAR As Integer

    Dim intConfigSetting As Integer

    Dim strBdgYear As String
    Dim strBdgAcc As String
    Dim strLocType As String
    Dim BlockTag As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        intGLAR = Session("SS_GLAR")
        intConfigSetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLMonthEnd), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()

            lblErrVehicle.Visible = False
            lblValueError.Visible = False

            If Not IsPostBack Then
                If Not Request.QueryString("Idx") = "" Then
                    hidAccYear.Value = Mid(Request.QueryString("Idx"), 1, 4)
                    hidVehCode.Value = Mid(Request.QueryString("Idx"), 5)
                End If

                If Not hidAccYear.Value = "" Then
                    DisplayData(hidAccYear.Value)
                    txtYearBudget.Enabled = False
                    ddlVehCode.Enabled = False
                Else
                    BindVehicle("")
                    btnSave.Visible = True

                End If
            End If
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        'Try
        '    'If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigSetting) = True Then
        '    'lblBlock.Text = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text
        '    'BlockTag = GetCaption(objLangCap.EnumLangCap.Block)
        '    'Else
        '    'lblBlock.Text = GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.Text
        '    'BlockTag = GetCaption(objLangCap.EnumLangCap.SubBlock)
        '    'End If
        'Catch Exp As System.Exception
        '    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GRDET_GET_LANGCAP_COSTLEVEL&errmesg=" & Exp.ToString() & "&redirect=pu/trx/PU_trx_GRList.aspx")
        'End Try


        lblVehicle.Text = GetCaption(objLangCap.EnumLangCap.Vehicle) & lblCode.Text
        lblErrVehicle.Text = "Please Select " & lblVehicle.Text

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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=FA_CLSTRX_AssetAddDETAILS_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=FA/trx/FA_trx_AssetAddDetails.aspx")
        End Try
    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

        For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If strLocType = objAdminLoc.EnumLocType.Mill Then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                Else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                End If
                Exit For
            End If
        Next
    End Function

    Protected Function LoadData(ByVal vstrAccYear As String) As DataSet

        Dim strOpCode As String = "GL_CLSTRX_BUDGET_VEH_GET"
        Dim intErrNo As Integer
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        strParamName = "AccYear|LocCode|VehCode"
        strParamValue = vstrAccYear & "|" & strLocation & "|" & hidVehCode.Value

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BUDGET_GET_DATA&errmesg=" & lblErrMessage.Text & "&redirect=gl/trx/gl_trx_budget_list.aspx")
        End Try

        Return objDataSet

    End Function

    Sub DisplayData(ByVal vstrAccYear As String)

        Dim dsTx As DataSet = LoadData(vstrAccYear)
        Dim strVehCode As String


        If dsTx.Tables(0).Rows.Count > 0 Then



            txtYearBudget.Text = Trim(dsTx.Tables(0).Rows(0).Item("AccYear"))

            txtb01.Text = dsTx.Tables(0).Rows(0).Item("b01")
            txtb02.Text = dsTx.Tables(0).Rows(0).Item("b02")
            txtb03.Text = dsTx.Tables(0).Rows(0).Item("b03")
            txtb04.Text = dsTx.Tables(0).Rows(0).Item("b04")
            txtb05.Text = dsTx.Tables(0).Rows(0).Item("b05")
            txtb06.Text = dsTx.Tables(0).Rows(0).Item("b06")
            txtb07.Text = dsTx.Tables(0).Rows(0).Item("b07")
            txtb08.Text = dsTx.Tables(0).Rows(0).Item("b08")
            txtb09.Text = dsTx.Tables(0).Rows(0).Item("b09")
            txtb10.Text = dsTx.Tables(0).Rows(0).Item("b10")
            txtb11.Text = dsTx.Tables(0).Rows(0).Item("b11")
            txtb12.Text = dsTx.Tables(0).Rows(0).Item("b12")
            lblCreateDate.Text = Trim(objGlobal.GetLongDate(dsTx.Tables(0).Rows(0).Item("CreateDate")))
            lblUpdateBy.Text = Trim(dsTx.Tables(0).Rows(0).Item("UpdateId"))
            hidVehCode.Value = Trim(dsTx.Tables(0).Rows(0).Item("VehCode"))


            strVehCode = Trim(dsTx.Tables(0).Rows(0).Item("VehCode"))
            BindVehicle(strVehCode)



        End If
    End Sub


    Sub BindVehicle(ByVal pv_strVehCode As String)

        Dim strOpCd As String = "GL_CLSTRX_VEHICLE_LIST_GET"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer = 0
        Dim strSearch As String
        Dim strSort As String


        strSearch = " AND LocCode='" & strLocation & "' And GL.Status='1'"
        strSort = " ORDER BY VehCode ASC "

        strParamName = "SEARCHSTR|SORTEXP"
        strParamValue = strSearch & "|" & strSort

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objVehDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GR_VEH_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try


        For intCnt = 0 To objVehDs.Tables(0).Rows.Count - 1
            objVehDs.Tables(0).Rows(intCnt).Item("VehCode") = Trim(objVehDs.Tables(0).Rows(intCnt).Item("VehCode"))
            objVehDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objVehDs.Tables(0).Rows(intCnt).Item("VehCode")) & " (" & Trim(objVehDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objVehDs.Tables(0).Rows(intCnt).Item("VehCode") = Trim(pv_strVehCode) Then
                intSelectedIndex = intCnt + 1
                Exit For
            End If
        Next

        dr = objVehDs.Tables(0).NewRow()
        dr("VehCode") = ""
        dr("Description") = lblPleaseSelect.Text & lblVehicle.Text
        objVehDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlVehCode.DataSource = objVehDs.Tables(0)
        ddlVehCode.DataValueField = "VehCode"
        ddlVehCode.DataTextField = "Description"
        ddlVehCode.DataBind()
        ddlVehCode.SelectedIndex = intSelectedIndex
    End Sub


    Sub btnSave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)

        Dim strOpCode As String = "GL_CLSTRX_BUDGETVEH_SAVE"
        Dim intErrNo As Integer
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim objRslSet As New DataSet()


        If ddlVehCode.Items.Count > 1 And ddlVehCode.SelectedValue = "" Then
            lblErrVehicle.Visible = True
            Exit Sub
        End If

        



        strParamName = "LocCode|AccYear|VehCode|b01|b02|b03|b04|b05|b06|b07|b08|b09|b10|b11|b12|UpdateId"


        strParamValue = strLocation & "|" & txtYearBudget.Text & "|" & ddlVehCode.SelectedValue & "|" & _
                        txtb01.Text & "|" & txtb02.Text & "|" & txtb03.Text & "|" & txtb04.Text & "|" & _
                        txtb05.Text & "|" & txtb06.Text & "|" & txtb07.Text & "|" & txtb08.Text & "|" & _
                        txtb09.Text & "|" & txtb10.Text & "|" & txtb11.Text & "|" & txtb12.Text & "|" & strUserId

        Try


            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCode, _
                                                   strParamName, _
                                                   strParamValue)



        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_BUDGET_SAVE&errmesg=" & lblErrMessage.Text & "&redirect=gl/trx/GL_trx_BudgetVeh_list")

        Finally

            txtYearBudget.Enabled = False
            ddlVehCode.Enabled = False
            DisplayData(txtYearBudget.Text)

        End Try


    End Sub


    
    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)

        Response.Redirect("GL_trx_BudgetVeh_list.aspx")

    End Sub

End Class
