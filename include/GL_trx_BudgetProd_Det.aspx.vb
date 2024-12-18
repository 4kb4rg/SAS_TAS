
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


Public Class GL_trx_BudgetProd_Det : Inherits Page

    Dim objGLTrx As New agri.GL.clsTrx()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblCreateDate As Label
    Protected WithEvents lblLastUpdate As Label

    Protected WithEvents txtYearBudget As TextBox
    Protected WithEvents lblUpdateBy As Label

    Protected WithEvents lblCode As Label

    Protected WithEvents lblValueError As Label

    Protected WithEvents txtB01P As TextBox
    Protected WithEvents txtB02P As TextBox
    Protected WithEvents txtB03P As TextBox
    Protected WithEvents txtB04P As TextBox
    Protected WithEvents txtB05P As TextBox
    Protected WithEvents txtB06P As TextBox
    Protected WithEvents txtB07P As TextBox
    Protected WithEvents txtB08P As TextBox
    Protected WithEvents txtB09P As TextBox
    Protected WithEvents txtB10P As TextBox
    Protected WithEvents txtB11P As TextBox
    Protected WithEvents txtB12P As TextBox
    Protected WithEvents txtB01K As TextBox
    Protected WithEvents txtB02K As TextBox
    Protected WithEvents txtB03K As TextBox
    Protected WithEvents txtB04K As TextBox
    Protected WithEvents txtB05K As TextBox
    Protected WithEvents txtB06K As TextBox
    Protected WithEvents txtB07K As TextBox
    Protected WithEvents txtB08K As TextBox
    Protected WithEvents txtB09K As TextBox
    Protected WithEvents txtB10K As TextBox
    Protected WithEvents txtB11K As TextBox
    Protected WithEvents txtB12K As TextBox
    Protected WithEvents txtB01T As TextBox
    Protected WithEvents txtB02T As TextBox
    Protected WithEvents txtB03T As TextBox
    Protected WithEvents txtB04T As TextBox
    Protected WithEvents txtB05T As TextBox
    Protected WithEvents txtB06T As TextBox
    Protected WithEvents txtB07T As TextBox
    Protected WithEvents txtB08T As TextBox
    Protected WithEvents txtB09T As TextBox
    Protected WithEvents txtB10T As TextBox
    Protected WithEvents txtB11T As TextBox
    Protected WithEvents txtB12T As TextBox
    Protected WithEvents txtR01P As TextBox
    Protected WithEvents txtR02P As TextBox
    Protected WithEvents txtR03P As TextBox
    Protected WithEvents txtR04P As TextBox
    Protected WithEvents txtR05P As TextBox
    Protected WithEvents txtR06P As TextBox
    Protected WithEvents txtR07P As TextBox
    Protected WithEvents txtR08P As TextBox
    Protected WithEvents txtR09P As TextBox
    Protected WithEvents txtR10P As TextBox
    Protected WithEvents txtR11P As TextBox
    Protected WithEvents txtR12P As TextBox
    Protected WithEvents txtR01K As TextBox
    Protected WithEvents txtR02K As TextBox
    Protected WithEvents txtR03K As TextBox
    Protected WithEvents txtR04K As TextBox
    Protected WithEvents txtR05K As TextBox
    Protected WithEvents txtR06K As TextBox
    Protected WithEvents txtR07K As TextBox
    Protected WithEvents txtR08K As TextBox
    Protected WithEvents txtR09K As TextBox
    Protected WithEvents txtR10K As TextBox
    Protected WithEvents txtR11K As TextBox
    Protected WithEvents txtR12K As TextBox
    Protected WithEvents txtR01T As TextBox
    Protected WithEvents txtR02T As TextBox
    Protected WithEvents txtR03T As TextBox
    Protected WithEvents txtR04T As TextBox
    Protected WithEvents txtR05T As TextBox
    Protected WithEvents txtR06T As TextBox
    Protected WithEvents txtR07T As TextBox
    Protected WithEvents txtR08T As TextBox
    Protected WithEvents txtR09T As TextBox
    Protected WithEvents txtR10T As TextBox
    Protected WithEvents txtR11T As TextBox
    Protected WithEvents txtR12T As TextBox

    Protected WithEvents btnSave As ImageButton
    Protected WithEvents btnDelete As ImageButton
    Protected WithEvents btnBack As ImageButton

    Protected WithEvents hidAccYear As HtmlInputHidden

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

            lblValueError.Visible = False

            If Not IsPostBack Then
                If Not Request.QueryString("AccYear") = "" Then
                    hidAccYear.Value = Request.QueryString("AccYear")
                End If

                If Not hidAccYear.Value = "" Then
                    DisplayData(hidAccYear.Value)
                Else
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
        '    BlockTag = GetCaption(objLangCap.EnumLangCap.SubBlock)
        '    'End If
        'Catch Exp As System.Exception
        '    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GRDET_GET_LANGCAP_COSTLEVEL&errmesg=" & Exp.ToString() & "&redirect=pu/trx/PU_trx_GRList.aspx")
        'End Try


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

        Dim strOpCode As String = "GL_CLSTRX_BUDGETPROD_GET"
        Dim intErrNo As Integer
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        strParamName = "AccYear|LocCode"
        strParamValue = vstrAccYear & "|" & strLocation

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

        If dsTx.Tables(0).Rows.Count > 0 Then

            txtYearBudget.Text = Trim(dsTx.Tables(0).Rows(0).Item("AccYear"))

            txtB01P.Text = Trim(dsTx.Tables(0).Rows(0).Item("B01P"))
            txtB02P.Text = Trim(dsTx.Tables(0).Rows(0).Item("B02P"))
            txtB03P.Text = Trim(dsTx.Tables(0).Rows(0).Item("B03P"))
            txtB04P.Text = Trim(dsTx.Tables(0).Rows(0).Item("B04P"))
            txtB05P.Text = Trim(dsTx.Tables(0).Rows(0).Item("B05P"))
            txtB06P.Text = Trim(dsTx.Tables(0).Rows(0).Item("B06P"))
            txtB07P.Text = Trim(dsTx.Tables(0).Rows(0).Item("B07P"))
            txtB08P.Text = Trim(dsTx.Tables(0).Rows(0).Item("B08P"))
            txtB09P.Text = Trim(dsTx.Tables(0).Rows(0).Item("B09P"))
            txtB10P.Text = Trim(dsTx.Tables(0).Rows(0).Item("B10P"))
            txtB11P.Text = Trim(dsTx.Tables(0).Rows(0).Item("B11P"))
            txtB12P.Text = Trim(dsTx.Tables(0).Rows(0).Item("B12P"))

            txtB01K.Text = Trim(dsTx.Tables(0).Rows(0).Item("B01K"))
            txtB02K.Text = Trim(dsTx.Tables(0).Rows(0).Item("B02K"))
            txtB03K.Text = Trim(dsTx.Tables(0).Rows(0).Item("B03K"))
            txtB04K.Text = Trim(dsTx.Tables(0).Rows(0).Item("B04K"))
            txtB05K.Text = Trim(dsTx.Tables(0).Rows(0).Item("B05K"))
            txtB06K.Text = Trim(dsTx.Tables(0).Rows(0).Item("B06K"))
            txtB07K.Text = Trim(dsTx.Tables(0).Rows(0).Item("B07K"))
            txtB08K.Text = Trim(dsTx.Tables(0).Rows(0).Item("B08K"))
            txtB09K.Text = Trim(dsTx.Tables(0).Rows(0).Item("B09K"))
            txtB10K.Text = Trim(dsTx.Tables(0).Rows(0).Item("B10K"))
            txtB11K.Text = Trim(dsTx.Tables(0).Rows(0).Item("B11K"))
            txtB12K.Text = Trim(dsTx.Tables(0).Rows(0).Item("B12K"))

            txtB01T.Text = Trim(dsTx.Tables(0).Rows(0).Item("B01T"))
            txtB02T.Text = Trim(dsTx.Tables(0).Rows(0).Item("B02T"))
            txtB03T.Text = Trim(dsTx.Tables(0).Rows(0).Item("B03T"))
            txtB04T.Text = Trim(dsTx.Tables(0).Rows(0).Item("B04T"))
            txtB05T.Text = Trim(dsTx.Tables(0).Rows(0).Item("B05T"))
            txtB06T.Text = Trim(dsTx.Tables(0).Rows(0).Item("B06T"))
            txtB07T.Text = Trim(dsTx.Tables(0).Rows(0).Item("B07T"))
            txtB08T.Text = Trim(dsTx.Tables(0).Rows(0).Item("B08T"))
            txtB09T.Text = Trim(dsTx.Tables(0).Rows(0).Item("B09T"))
            txtB10T.Text = Trim(dsTx.Tables(0).Rows(0).Item("B10T"))
            txtB11T.Text = Trim(dsTx.Tables(0).Rows(0).Item("B11T"))
            txtB12T.Text = Trim(dsTx.Tables(0).Rows(0).Item("B12T"))

            txtR01P.Text = Trim(dsTx.Tables(0).Rows(0).Item("R01P"))
            txtR02P.Text = Trim(dsTx.Tables(0).Rows(0).Item("R02P"))
            txtR03P.Text = Trim(dsTx.Tables(0).Rows(0).Item("R03P"))
            txtR04P.Text = Trim(dsTx.Tables(0).Rows(0).Item("R04P"))
            txtR05P.Text = Trim(dsTx.Tables(0).Rows(0).Item("R05P"))
            txtR06P.Text = Trim(dsTx.Tables(0).Rows(0).Item("R06P"))
            txtR07P.Text = Trim(dsTx.Tables(0).Rows(0).Item("R07P"))
            txtR08P.Text = Trim(dsTx.Tables(0).Rows(0).Item("R08P"))
            txtR09P.Text = Trim(dsTx.Tables(0).Rows(0).Item("R09P"))
            txtR10P.Text = Trim(dsTx.Tables(0).Rows(0).Item("R10P"))
            txtR11P.Text = Trim(dsTx.Tables(0).Rows(0).Item("R11P"))
            txtR12P.Text = Trim(dsTx.Tables(0).Rows(0).Item("R12P"))

            txtR01K.Text = Trim(dsTx.Tables(0).Rows(0).Item("R01K"))
            txtR02K.Text = Trim(dsTx.Tables(0).Rows(0).Item("R02K"))
            txtR03K.Text = Trim(dsTx.Tables(0).Rows(0).Item("R03K"))
            txtR04K.Text = Trim(dsTx.Tables(0).Rows(0).Item("R04K"))
            txtR05K.Text = Trim(dsTx.Tables(0).Rows(0).Item("R05K"))
            txtR06K.Text = Trim(dsTx.Tables(0).Rows(0).Item("R06K"))
            txtR07K.Text = Trim(dsTx.Tables(0).Rows(0).Item("R07K"))
            txtR08K.Text = Trim(dsTx.Tables(0).Rows(0).Item("R08K"))
            txtR09K.Text = Trim(dsTx.Tables(0).Rows(0).Item("R09K"))
            txtR10K.Text = Trim(dsTx.Tables(0).Rows(0).Item("R10K"))
            txtR11K.Text = Trim(dsTx.Tables(0).Rows(0).Item("R11K"))
            txtR12K.Text = Trim(dsTx.Tables(0).Rows(0).Item("R12K"))

            txtR01T.Text = Trim(dsTx.Tables(0).Rows(0).Item("R01T"))
            txtR02T.Text = Trim(dsTx.Tables(0).Rows(0).Item("R02T"))
            txtR03T.Text = Trim(dsTx.Tables(0).Rows(0).Item("R03T"))
            txtR04T.Text = Trim(dsTx.Tables(0).Rows(0).Item("R04T"))
            txtR05T.Text = Trim(dsTx.Tables(0).Rows(0).Item("R05T"))
            txtR06T.Text = Trim(dsTx.Tables(0).Rows(0).Item("R06T"))
            txtR07T.Text = Trim(dsTx.Tables(0).Rows(0).Item("R07T"))
            txtR08T.Text = Trim(dsTx.Tables(0).Rows(0).Item("R08T"))
            txtR09T.Text = Trim(dsTx.Tables(0).Rows(0).Item("R09T"))
            txtR10T.Text = Trim(dsTx.Tables(0).Rows(0).Item("R10T"))
            txtR11T.Text = Trim(dsTx.Tables(0).Rows(0).Item("R11T"))
            txtR12T.Text = Trim(dsTx.Tables(0).Rows(0).Item("R12T"))

            lblCreateDate.Text = Trim(objGlobal.GetLongDate(dsTx.Tables(0).Rows(0).Item("CreateDate")))
            lblUpdateBy.Text = Trim(dsTx.Tables(0).Rows(0).Item("UpdateId"))

            txtYearBudget.Enabled = False

        End If
    End Sub



    Sub btnSave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)

        Dim strOpCode As String = "GL_CLSTRX_BUDGETPROD_SAVE"
        Dim intErrNo As Integer
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim objRslSet As New DataSet()

        

        strParamName = "LocCode|AccYear|B01P|B01K|B01T|R01P|R01K|R01T|B02P|B02K|B02T|R02P|R02K|R02T" & _
                       "|B03P|B03K|B03T|R03P|R03K|R03T|B04P|B04K|B04T|R04P|R04K|R04T" & _
                       "|B05P|B05K|B05T|R05P|R05K|R05T|B06P|B06K|B06T|R06P|R06K|R06T" & _
                       "|B07P|B07K|B07T|R07P|R07K|R07T|B08P|B08K|B08T|R08P|R08K|R08T" & _
                       "|B09P|B09K|B09T|R09P|R09K|R09T|B10P|B10K|B10T|R10P|R10K|R10T" & _
                       "|B11P|B11K|B11T|R11P|R11K|R11T|B12P|B12K|B12T|R12P|R12K|R12T" & _
                       "|UpdateId"


        strParamValue = strLocation & "|" & txtYearBudget.Text & "|" & txtB01P.Text & "|" & txtB01K.Text & "|" & _
                        txtB01T.Text & "|" & txtR01P.Text & "|" & txtR01K.Text & "|" & txtR01T.Text & "|" & _
                        txtB02P.Text & "|" & txtB02K.Text & "|" & txtB02T.Text & "|" & txtR02P.Text & "|" & _
                        txtR02K.Text & "|" & txtR02T.Text & "|" & txtB03P.Text & "|" & txtB03K.Text & "|" & _
                        txtB03T.Text & "|" & txtR03P.Text & "|" & txtR03K.Text & "|" & txtR03T.Text & "|" & _
                        txtB04P.Text & "|" & txtB04K.Text & "|" & txtB04T.Text & "|" & txtR04P.Text & "|" & _
                        txtR04K.Text & "|" & txtR04T.Text & "|" & txtB05P.Text & "|" & txtB05K.Text & "|" & _
                        txtB05T.Text & "|" & txtR05P.Text & "|" & txtR05K.Text & "|" & txtR05T.Text & "|" & _
                        txtB06P.Text & "|" & txtB06K.Text & "|" & txtB06T.Text & "|" & txtR06P.Text & "|" & _
                        txtR06K.Text & "|" & txtR06T.Text & "|" & txtB07P.Text & "|" & txtB07K.Text & "|" & _
                        txtB07T.Text & "|" & txtR07P.Text & "|" & txtR07K.Text & "|" & txtR07T.Text & "|" & _
                        txtB08P.Text & "|" & txtB08K.Text & "|" & txtB08T.Text & "|" & txtR08P.Text & "|" & _
                        txtR08K.Text & "|" & txtR08T.Text & "|" & txtB09P.Text & "|" & txtB09K.Text & "|" & _
                        txtB09T.Text & "|" & txtR09P.Text & "|" & txtR09K.Text & "|" & txtR09T.Text & "|" & _
                        txtB10P.Text & "|" & txtB10K.Text & "|" & txtB10T.Text & "|" & txtR10P.Text & "|" & _
                        txtR10K.Text & "|" & txtR10T.Text & "|" & txtB11P.Text & "|" & txtB11K.Text & "|" & _
                        txtB11T.Text & "|" & txtR11P.Text & "|" & txtR11K.Text & "|" & txtR11T.Text & "|" & _
                        txtB12P.Text & "|" & txtB12K.Text & "|" & txtB12T.Text & "|" & txtR12P.Text & "|" & _
                        txtR12K.Text & "|" & txtR12T.Text & "|" & strUserId

        Try


            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCode, _
                                                   strParamName, _
                                                   strParamValue)

            'hidAccYear.Value = objRslSet.Tables(0).Rows(0).Item("AccYear")


        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_BUDGET_SAVE&errmesg=" & lblErrMessage.Text & "&redirect=gl/trx/GL_trx_Budget_list")

        Finally

            DisplayData(txtYearBudget.Text)

        End Try


    End Sub


    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)

        Response.Redirect("GL_trx_BudgetProd_list.aspx")

    End Sub

End Class
