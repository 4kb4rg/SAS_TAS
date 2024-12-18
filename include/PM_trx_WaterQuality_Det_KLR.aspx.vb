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

Public Class PM_trx_WaterQuality_Det_KLR : Inherits Page

    Dim objGLTrx As New agri.GL.clsTrx()
    Dim objPMTrx As New agri.PM.clsTrx()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblDupMsg As Label
    Protected WithEvents lblFmt As Label
    Protected WithEvents lblDate As Label
    Protected WithEvents blnUpdate As Label

    Protected WithEvents lblStatus As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdateBy As Label


    Protected WithEvents txtPH1 As TextBox
    Protected WithEvents txtPH2 As TextBox
    Protected WithEvents txtPH3 As TextBox
    Protected WithEvents txtPH_K As TextBox
    Protected WithEvents txtHardness1 As TextBox
    Protected WithEvents txtHardness2 As TextBox
    Protected WithEvents txtHardness3 As TextBox
    Protected WithEvents txtHardness_K As TextBox
    Protected WithEvents txtAlkalinity1 As TextBox
    Protected WithEvents txtAlkalinity2 As TextBox
    Protected WithEvents txtAlkalinity3 As TextBox
    Protected WithEvents txtAlkalinity_K As TextBox
    Protected WithEvents txtTDS1 As TextBox
    Protected WithEvents txtTDS2 As TextBox
    Protected WithEvents txtTDS3 As TextBox
    Protected WithEvents txtTDS_K As TextBox
    Protected WithEvents txtSilica1 As TextBox
    Protected WithEvents txtSilica2 As TextBox
    Protected WithEvents txtSilica3 As TextBox
    Protected WithEvents txtSilica_K As TextBox
    Protected WithEvents txtPH21 As TextBox
    Protected WithEvents txtPH22 As TextBox
    Protected WithEvents txtPH23 As TextBox
    Protected WithEvents txtPH2_K As TextBox
    Protected WithEvents txtHardness21 As TextBox
    Protected WithEvents txtHardness22 As TextBox
    Protected WithEvents txtHardness23 As TextBox
    Protected WithEvents txtHardness2_K As TextBox
    Protected WithEvents txtAlkalinity21 As TextBox
    Protected WithEvents txtAlkalinity22 As TextBox
    Protected WithEvents txtAlkalinity23 As TextBox
    Protected WithEvents txtAlkalinity2_K As TextBox
    Protected WithEvents txtTDS21 As TextBox
    Protected WithEvents txtTDS22 As TextBox
    Protected WithEvents txtTDS23 As TextBox
    Protected WithEvents txtTDS2_K As TextBox
    Protected WithEvents txtSilica21 As TextBox
    Protected WithEvents txtSilica22 As TextBox
    Protected WithEvents txtSilica23 As TextBox
    Protected WithEvents txtSilica2_K As TextBox
    Protected WithEvents txtSulfit21 As TextBox
    Protected WithEvents txtSulfit22 As TextBox
    Protected WithEvents txtSulfit23 As TextBox
    Protected WithEvents txtSulfit2_K As TextBox
    Protected WithEvents txtSquest21 As TextBox
    Protected WithEvents txtSquest22 As TextBox
    Protected WithEvents txtSquest23 As TextBox
    Protected WithEvents txtSquest2_K As TextBox
    Protected WithEvents txtAlkalinity31 As TextBox
    Protected WithEvents txtAlkalinity32 As TextBox
    Protected WithEvents txtAlkalinity33 As TextBox
    Protected WithEvents txtAlkalinity3_K As TextBox

    Protected WithEvents txtReb1 As TextBox
    Protected WithEvents txtReb2 As TextBox
    Protected WithEvents txtReb3 As TextBox
    Protected WithEvents txtReb_K As TextBox

    Protected WithEvents txtWak1 As TextBox
    Protected WithEvents txtWak2 As TextBox
    Protected WithEvents txtWak3 As TextBox
    Protected WithEvents txtWak_K As TextBox

    Protected WithEvents txtDig1 As TextBox
    Protected WithEvents txtDig2 As TextBox
    Protected WithEvents txtDig3 As TextBox
    Protected WithEvents txtDig_K As TextBox

    Protected WithEvents txtSlg1 As TextBox
    Protected WithEvents txtSlg2 As TextBox
    Protected WithEvents txtSlg3 As TextBox
    Protected WithEvents txtSlg_K As TextBox

    Protected WithEvents txtOtk1 As TextBox
    Protected WithEvents txtOtk2 As TextBox
    Protected WithEvents txtOtk3 As TextBox
    Protected WithEvents txtOtk_K As TextBox

    Protected WithEvents txtOpu1 As TextBox
    Protected WithEvents txtOpu2 As TextBox
    Protected WithEvents txtOpu3 As TextBox
    Protected WithEvents txtOpu_K As TextBox

    Protected WithEvents txtVdr1 As TextBox
    Protected WithEvents txtVdr2 As TextBox
    Protected WithEvents txtVdr3 As TextBox
    Protected WithEvents txtVdr_K As TextBox

    Protected WithEvents txtKdr1 As TextBox
    Protected WithEvents txtKdr2 As TextBox
    Protected WithEvents txtKdr3 As TextBox
    Protected WithEvents txtKdr_K As TextBox

    Protected WithEvents txtTvd1 As TextBox
    Protected WithEvents txtTvd2 As TextBox
    Protected WithEvents txtTvd3 As TextBox
    Protected WithEvents txtTvd_K As TextBox

    Protected WithEvents txtSmr1 As TextBox
    Protected WithEvents txtSmr2 As TextBox
    Protected WithEvents txtSmr3 As TextBox
    Protected WithEvents txtSmr_K As TextBox


    Protected WithEvents txtdate As TextBox
   
    Protected WithEvents Save As ImageButton
    Protected WithEvents Delete As ImageButton
    Protected WithEvents btnSelDateFrom As Image
    Dim strTransDate As String
    Dim strEdit As String

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
    Dim intPMAR As Integer
    Dim strDateFormat As String

    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PMACCMONTH")
        strAccYear = Session("SS_PMACCYEAR")
        intPMAR = Session("SS_PMAR")
        strDateFormat = Session("SS_DATEFMT")

        strLocType = Session("SS_LOCTYPE")
        strTransDate = Request.QueryString("TransDate")
        strEdit = Request.QueryString("Edit")


        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMWaterQuality), intPMAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()

            If Not Page.IsPostBack Then
                If Not (Request.QueryString("LocCode") = "" And Request.QueryString("TransDate") = "") Then
                    strTransDate = Request.QueryString("TransDate")
                    strEdit = Request.QueryString("Edit")
                End If

                If strEdit = "True" Then
                    DisplayData()
                    blnUpdate.Text = False
                    txtdate.Enabled = False
                    btnSelDateFrom.Visible = False
                Else
                    blnUpdate.Text = True
                    txtdate.Enabled = True
                    btnSelDateFrom.Visible = True
                End If
            End If
        End If
    End Sub

    Sub onload_GetLangCap()
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/PM_trx_WaterQuality_List_KLR.aspx")
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

    Protected Function LoadData() As DataSet

        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim strOpCode As String = "PM_CLSTRX_WATERQUALITY_KLR_GET"

        strParamName = "TRANSDATE|LOCCODE"
        strParamValue = strTransDate & "|" & strLocation

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCode, strParamName, strParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PM_GET_DATA&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/PM_trx_WaterQuality_List_KLR.aspx")
        End Try


        Return objDataSet
    End Function


    Sub DisableControl(ByVal pv_strstatus)
        Dim strView As Boolean

        If pv_strstatus = objPMTrx.EnumWaterQualityStatus.Deleted Then
            strView = False

        ElseIf pv_strstatus = objPMTrx.EnumWaterQualityStatus.Active Then
            strView = True
        End If

        txtdate.Enabled = strView
        Save.Visible = strView

    End Sub

    Sub DisplayData()

        Dim dsDisplay As DataSet = LoadData()

        If dsDisplay.Tables(0).Rows.Count > 0 Then
            txtdate.Text = objGlobal.GetShortDate(strDateFormat, Trim(dsDisplay.Tables(0).Rows(0).Item("TransDate")))


            txtPH1.Text = dsDisplay.Tables(0).Rows(0).Item("PH1")
            txtPH2.Text = dsDisplay.Tables(0).Rows(0).Item("PH2")
            txtPH3.Text = dsDisplay.Tables(0).Rows(0).Item("PH3")
            txtPH_K.Text = Trim(dsDisplay.Tables(0).Rows(0).Item("PH_K"))

            txtHardness1.Text = dsDisplay.Tables(0).Rows(0).Item("Hardness1")
            txtHardness2.Text = dsDisplay.Tables(0).Rows(0).Item("Hardness2")
            txtHardness3.Text = dsDisplay.Tables(0).Rows(0).Item("Hardness3")
            txtHardness_K.Text = Trim(dsDisplay.Tables(0).Rows(0).Item("Hardness_K"))

            txtAlkalinity1.Text = dsDisplay.Tables(0).Rows(0).Item("Alkalinity1")
            txtAlkalinity2.Text = dsDisplay.Tables(0).Rows(0).Item("Alkalinity2")
            txtAlkalinity3.Text = dsDisplay.Tables(0).Rows(0).Item("Alkalinity3")
            txtAlkalinity_K.Text = Trim(dsDisplay.Tables(0).Rows(0).Item("Alkalinity_K"))

            txtTDS1.Text = dsDisplay.Tables(0).Rows(0).Item("TDS1")
            txtTDS2.Text = dsDisplay.Tables(0).Rows(0).Item("TDS2")
            txtTDS3.Text = dsDisplay.Tables(0).Rows(0).Item("TDS3")
            txtTDS_K.Text = Trim(dsDisplay.Tables(0).Rows(0).Item("TDS_K"))

            txtSilica1.Text = dsDisplay.Tables(0).Rows(0).Item("Silica1")
            txtSilica2.Text = dsDisplay.Tables(0).Rows(0).Item("Silica2")
            txtSilica3.Text = dsDisplay.Tables(0).Rows(0).Item("Silica3")
            txtSilica_K.Text = Trim(dsDisplay.Tables(0).Rows(0).Item("Silica_K"))

            txtPH21.Text = dsDisplay.Tables(0).Rows(0).Item("PH21")
            txtPH22.Text = dsDisplay.Tables(0).Rows(0).Item("PH22")
            txtPH23.Text = dsDisplay.Tables(0).Rows(0).Item("PH23")
            txtPH2_K.Text = Trim(dsDisplay.Tables(0).Rows(0).Item("PH2_K"))

            txtHardness21.Text = dsDisplay.Tables(0).Rows(0).Item("Hardness21")
            txtHardness22.Text = dsDisplay.Tables(0).Rows(0).Item("Hardness22")
            txtHardness23.Text = dsDisplay.Tables(0).Rows(0).Item("Hardness23")
            txtHardness2_K.Text = Trim(dsDisplay.Tables(0).Rows(0).Item("Hardness2_K"))

            txtAlkalinity21.Text = dsDisplay.Tables(0).Rows(0).Item("Alkalinity21")
            txtAlkalinity22.Text = dsDisplay.Tables(0).Rows(0).Item("Alkalinity22")
            txtAlkalinity23.Text = dsDisplay.Tables(0).Rows(0).Item("Alkalinity23")
            txtAlkalinity2_K.Text = Trim(dsDisplay.Tables(0).Rows(0).Item("Alkalinity2_K"))

            txtTDS21.Text = dsDisplay.Tables(0).Rows(0).Item("TDS21")
            txtTDS22.Text = dsDisplay.Tables(0).Rows(0).Item("TDS22")
            txtTDS23.Text = dsDisplay.Tables(0).Rows(0).Item("TDS23")
            txtTDS2_K.Text = Trim(dsDisplay.Tables(0).Rows(0).Item("TDS2_K"))

            txtSilica21.Text = dsDisplay.Tables(0).Rows(0).Item("Silica21")
            txtSilica22.Text = dsDisplay.Tables(0).Rows(0).Item("Silica22")
            txtSilica23.Text = dsDisplay.Tables(0).Rows(0).Item("Silica23")
            txtSilica2_K.Text = Trim(dsDisplay.Tables(0).Rows(0).Item("Silica2_K"))

            txtSulfit21.Text = dsDisplay.Tables(0).Rows(0).Item("Sulfit21")
            txtSulfit22.Text = dsDisplay.Tables(0).Rows(0).Item("Sulfit22")
            txtSulfit23.Text = dsDisplay.Tables(0).Rows(0).Item("Sulfit23")
            txtSulfit2_K.Text = Trim(dsDisplay.Tables(0).Rows(0).Item("Sulfit2_K"))

            txtSquest21.Text = dsDisplay.Tables(0).Rows(0).Item("Squest21")
            txtSquest22.Text = dsDisplay.Tables(0).Rows(0).Item("Squest22")
            txtSquest23.Text = dsDisplay.Tables(0).Rows(0).Item("Squest23")
            txtSquest2_K.Text = Trim(dsDisplay.Tables(0).Rows(0).Item("Squest2_K"))


            txtAlkalinity31.Text = dsDisplay.Tables(0).Rows(0).Item("Alkalinity31")
            txtAlkalinity32.Text = dsDisplay.Tables(0).Rows(0).Item("Alkalinity32")
            txtAlkalinity33.Text = dsDisplay.Tables(0).Rows(0).Item("Alkalinity33")
            txtAlkalinity3_K.Text = Trim(dsDisplay.Tables(0).Rows(0).Item("Alkalinity3_K"))

            txtReb1.Text = dsDisplay.Tables(0).Rows(0).Item("Reb1")
            txtReb2.Text = dsDisplay.Tables(0).Rows(0).Item("Reb2")
            txtReb3.Text = dsDisplay.Tables(0).Rows(0).Item("Reb3")
            txtReb_K.Text = dsDisplay.Tables(0).Rows(0).Item("Reb_K")

            txtWak1.Text = dsDisplay.Tables(0).Rows(0).Item("Wak1")
            txtWak2.Text = dsDisplay.Tables(0).Rows(0).Item("Wak2")
            txtWak3.Text = dsDisplay.Tables(0).Rows(0).Item("Wak3")
            txtWak_K.Text = dsDisplay.Tables(0).Rows(0).Item("Wak_K")

            txtDig1.Text = dsDisplay.Tables(0).Rows(0).Item("Dig1")
            txtDig2.Text = dsDisplay.Tables(0).Rows(0).Item("Dig2")
            txtDig3.Text = dsDisplay.Tables(0).Rows(0).Item("Dig3")
            txtDig_K.Text = dsDisplay.Tables(0).Rows(0).Item("Dig_K")

            txtSlg1.Text = dsDisplay.Tables(0).Rows(0).Item("Slg1")
            txtSlg2.Text = dsDisplay.Tables(0).Rows(0).Item("Slg2")
            txtSlg3.Text = dsDisplay.Tables(0).Rows(0).Item("Slg3")
            txtSlg_K.Text = dsDisplay.Tables(0).Rows(0).Item("Slg_K")

            txtOtk1.Text = dsDisplay.Tables(0).Rows(0).Item("Otk1")
            txtOtk2.Text = dsDisplay.Tables(0).Rows(0).Item("Otk2")
            txtOtk3.Text = dsDisplay.Tables(0).Rows(0).Item("Otk3")
            txtOtk_K.Text = dsDisplay.Tables(0).Rows(0).Item("Otk_K")

            txtOpu1.Text = dsDisplay.Tables(0).Rows(0).Item("Opu1")
            txtOpu2.Text = dsDisplay.Tables(0).Rows(0).Item("Opu2")
            txtOpu3.Text = dsDisplay.Tables(0).Rows(0).Item("Opu3")
            txtOpu_K.Text = dsDisplay.Tables(0).Rows(0).Item("Opu_K")

            txtVdr1.Text = dsDisplay.Tables(0).Rows(0).Item("Vdr1")
            txtVdr2.Text = dsDisplay.Tables(0).Rows(0).Item("Vdr2")
            txtVdr3.Text = dsDisplay.Tables(0).Rows(0).Item("Vdr3")
            txtVdr_K.Text = dsDisplay.Tables(0).Rows(0).Item("Vdr_K")

            txtKdr1.Text = dsDisplay.Tables(0).Rows(0).Item("Kdr1")
            txtKdr2.Text = dsDisplay.Tables(0).Rows(0).Item("Kdr2")
            txtKdr3.Text = dsDisplay.Tables(0).Rows(0).Item("Kdr3")
            txtKdr_K.Text = dsDisplay.Tables(0).Rows(0).Item("Kdr_K")

            txtTvd1.Text = dsDisplay.Tables(0).Rows(0).Item("Tvd1")
            txtTvd2.Text = dsDisplay.Tables(0).Rows(0).Item("Tvd2")
            txtTvd3.Text = dsDisplay.Tables(0).Rows(0).Item("Tvd3")
            txtTvd_K.Text = dsDisplay.Tables(0).Rows(0).Item("Tvd_K")

            txtSmr1.Text = dsDisplay.Tables(0).Rows(0).Item("Smr1")
            txtSmr2.Text = dsDisplay.Tables(0).Rows(0).Item("Smr2")
            txtSmr3.Text = dsDisplay.Tables(0).Rows(0).Item("Smr3")
            txtSmr_K.Text = dsDisplay.Tables(0).Rows(0).Item("Smr_K")


            lblStatus.Text = objPMTrx.mtdGetWaterQualityStatus(Trim(dsDisplay.Tables(0).Rows(0).Item("Status")))
            lblLastUpdate.Text = objGlobal.GetLongDate(Trim(dsDisplay.Tables(0).Rows(0).Item("UpdateDate")))
            lblUpdateBy.Text = Trim(dsDisplay.Tables(0).Rows(0).Item("Username"))

            DisableControl(Trim(dsDisplay.Tables(0).Rows(0).Item("Status")))
            Delete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Delete.visible = True
        End If
    End Sub


    Sub UpdateData(ByVal strAction As String)

        Dim strOpCode As String
        Dim strOpCodeTrans As String
        Dim blnDuDKey As Boolean = False
        Dim blnUpd As Boolean = False
        Dim strStatus As String
        Dim strDate As String = CheckDate()
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim objDataSet1 As New DataSet()

        
        If strEdit = "True" Then
            blnUpd = True
            strOpCodeTrans = "PM_CLSTRX_WATERQUALITY_KLR_UPDATE"
        Else
            blnUpd = False
            strOpCodeTrans = "PM_CLSTRX_WATERQUALITY_KLR_INSERT"

            strOpCode = "PM_CLSTRX_WATERQUALITY_KLR_GET"

            strParamName = "TRANSDATE|LOCCODE"
            strParamValue = strDate & "|" & strLocation

            Try
                intErrNo = objGLTrx.mtdGetDataCommon(strOpCode, strParamName, strParamValue, objDataSet1)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PM_GET_DATA&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/PM_trx_WaterQuality_List_KLR.aspx")
            End Try

            If objDataSet1.Tables(0).Rows.Count > 0 Then
                blnDuDKey = True
                lblDupMsg.Visible = True
                Exit Sub
            Else
                blnDuDKey = False
            End If
        End If


        strStatus = IIf(lblStatus.Text = objPMTrx.mtdGetWaterQualityStatus(objPMTrx.EnumWaterQualityStatus.Deleted), _
                        objPMTrx.EnumWaterQualityStatus.Deleted, _
                        objPMTrx.EnumWaterQualityStatus.Active)
     
        strParamName = ""
        strParamName = "TRANSDATE|LOCCODE|UPDATEID|STATUS|ACCMONTH|ACCYEAR|"
        strParamName = strParamName & "PH1|PH2|PH3|PH_K|HARDNESS1|HARDNESS2|HARDNESS3|HARDNESS_K|"
        strParamName = strParamName & "ALKALINITY1|ALKALINITY2|ALKALINITY3|ALKALINITY_K|TDS1|TDS2|TDS3|TDS_K|"
        strParamName = strParamName & "SILICA1|SILICA2|SILICA3|SILICA_K|PH21|PH22|PH23|PH2_K|"
        strParamName = strParamName & "HARDNESS21|HARDNESS22|HARDNESS23|HARDNESS2_K|"
        strParamName = strParamName & "ALKALINITY21|ALKALINITY22|ALKALINITY23|ALKALINITY2_K|TDS21|TDS22|TDS23|TDS2_K|"
        strParamName = strParamName & "SILICA21|SILICA22|SILICA23|SILICA2_K|SULFIT21|SULFIT22|SULFIT23|SULFIT2_K|"
        strParamName = strParamName & "SQUEST21|SQUEST22|SQUEST23|SQUEST2_K|ALKALINITY31|ALKALINITY32|ALKALINITY33|ALKALINITY3_K|"
        strParamName = strParamName & "Reb1|Reb2|Reb3|Reb_K|Wak1|Wak2|Wak3|Wak_K|"
        strParamName = strParamName & "Dig1|Dig2|Dig3|Dig_K|Slg1|Slg2|Slg3|Slg_K|"
        strParamName = strParamName & "Otk1|Otk2|Otk3|Otk_K|Opu1|Opu2|Opu3|Opu_K|"
        strParamName = strParamName & "Vdr1|Vdr2|Vdr3|Vdr_K|Kdr1|Kdr2|Kdr3|Kdr_K|"
        strParamName = strParamName & "Tvd1|Tvd2|Tvd3|Tvd_K|Smr1|Smr2|Smr3|Smr_K"

        strParamValue = ""
        strParamValue = strDate & "|" & strLocation & "|" & strUserId & "|"
        strParamValue = strParamValue & strStatus & "|" & strAccMonth & "|" & strAccYear & "|"
        strParamValue = strParamValue & Val(txtPH1.Text) & "|" & Val(txtPH2.Text) & "|" & Val(txtPH3.Text) & "|" & TRIM(txtPH_K.Text) & "|"
        strParamValue = strParamValue & Val(txtHardness1.Text) & "|" & Val(txtHardness2.Text) & "|" & Val(txtHardness3.Text) & "|" & TRIM(txtHardness_K.Text) & "|"
        strParamValue = strParamValue & Val(txtAlkalinity1.Text) & "|" & Val(txtAlkalinity2.Text) & "|" & Val(txtAlkalinity3.Text) & "|" & TRIM(txtAlkalinity_K.Text) & "|"
        strParamValue = strParamValue & Val(txtTDS1.Text) & "|" & Val(txtTDS2.Text) & "|" & Val(txtTDS3.Text) & "|" & TRIM(txtTDS_K.Text) & "|"
        strParamValue = strParamValue & Val(txtSilica1.Text) & "|" & Val(txtSilica2.Text) & "|" & Val(txtSilica3.Text) & "|" & TRIM(txtSilica_K.Text) & "|"
        strParamValue = strParamValue & Val(txtPH21.Text) & "|" & Val(txtPH22.Text) & "|" & Val(txtPH23.Text) & "|" & TRIM(txtPH2_K.Text) & "|"
        strParamValue = strParamValue & Val(txtHardness21.Text) & "|" & Val(txtHardness22.Text) & "|" & Val(txtHardness23.Text) & "|" & TRIM(txtHardness2_K.Text) & "|"
        strParamValue = strParamValue & Val(txtAlkalinity21.Text) & "|" & Val(txtAlkalinity22.Text) & "|" & Val(txtAlkalinity23.Text) & "|" & TRIM(txtAlkalinity2_K.Text) & "|"
        strParamValue = strParamValue & Val(txtTDS21.Text) & "|" & Val(txtTDS22.Text) & "|" & Val(txtTDS23.Text) & "|" & TRIM(txtTDS2_K.Text) & "|"
        strParamValue = strParamValue & Val(txtSilica21.Text) & "|" & Val(txtSilica22.Text) & "|" & Val(txtSilica23.Text) & "|" & TRIM(txtSilica2_K.Text) & "|"
        strParamValue = strParamValue & Val(txtSulfit21.Text) & "|" & Val(txtSulfit22.Text) & "|" & Val(txtSulfit23.Text) & "|" & TRIM(txtSulfit2_K.Text) & "|"
        strParamValue = strParamValue & Val(txtSquest21.Text) & "|" & Val(txtSquest22.Text) & "|" & Val(txtSquest23.Text) & "|" & TRIM(txtSquest2_K.Text) & "|"
        strParamValue = strParamValue & Val(txtAlkalinity31.Text) & "|" & Val(txtAlkalinity32.Text) & "|" & Val(txtAlkalinity33.Text) & "|" & TRIM(txtAlkalinity3_K.Text) & "|"
        strParamValue = strParamValue & Val(txtReb1.Text) & "|" & Val(txtReb2.Text) & "|" & Val(txtReb3.Text) & "|" & TRIM(txtReb_K.Text) & "|"
        strParamValue = strParamValue & Val(txtWak1.Text) & "|" & Val(txtWak2.Text) & "|" & Val(txtWak3.Text) & "|" & TRIM(txtWak_K.Text) & "|"
        strParamValue = strParamValue & Val(txtDig1.Text) & "|" & Val(txtDig2.Text) & "|" & Val(txtDig3.Text) & "|" & TRIM(txtDig_K.Text) & "|"
        strParamValue = strParamValue & Val(txtSlg1.Text) & "|" & Val(txtSlg2.Text) & "|" & Val(txtSlg3.Text) & "|" & TRIM(txtSlg_K.Text) & "|"
        strParamValue = strParamValue & Val(txtOtk1.Text) & "|" & Val(txtOtk2.Text) & "|" & Val(txtOtk3.Text) & "|" & TRIM(txtOtk_K.Text) & "|"
        strParamValue = strParamValue & Val(txtOpu1.Text) & "|" & Val(txtOpu2.Text) & "|" & Val(txtOpu3.Text) & "|" & TRIM(txtOpu_K.Text) & "|"
        strParamValue = strParamValue & Val(txtVdr1.Text) & "|" & Val(txtVdr2.Text) & "|" & Val(txtVdr3.Text) & "|" & TRIM(txtVdr_K.Text) & "|"
        strParamValue = strParamValue & Val(txtKdr1.Text) & "|" & Val(txtKdr2.Text) & "|" & Val(txtKdr3.Text) & "|" & TRIM(txtKdr_K.Text) & "|"
        strParamValue = strParamValue & Val(txtTvd1.Text) & "|" & Val(txtTvd2.Text) & "|" & Val(txtTvd3.Text) & "|" & TRIM(txtTvd_K.Text) & "|"
        strParamValue = strParamValue & Val(txtSmr1.Text) & "|" & Val(txtSmr2.Text) & "|" & Val(txtSmr3.Text) & "|" & TRIM(txtSmr_K.Text)


        Try
            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCodeTrans, _
                                                   strParamName, _
                                                   strParamValue)


        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UPDATE&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/PM_trx_WaterQuality_List_KLR.aspx")
        End Try

       
        Response.Redirect("PM_trx_WaterQuality_Det_KLR.aspx?LocCode=" & strLocation & "&TransDate=" & strDate & "&Edit=True")
          

    End Sub

    Protected Function CheckDate() As String
        Dim strDateSetting As String
        Dim objSysCfgDs As DataSet
        Dim objDateFormat As String
        Dim strValidDate As String
        If Not txtdate.Text = "" Then
            If objGlobal.mtdValidInputDate(strDateFormat, txtdate.Text, objDateFormat, strValidDate) = True Then
                Return strValidDate
            Else
                lblFmt.Text = objDateFormat & "."
                lblDate.Visible = True
                lblFmt.Visible = True
            End If
        End If

    End Function

    Sub btnSave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        UpdateData("Save")
    End Sub

    Sub btnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCode As String = "PM_CLSTRX_WATERQUALITY_KLR_DEL"
        Dim strDate As String = CheckDate()
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        strParamName = "TRANSDATE|LOCCODE"
        strParamValue = strDate & "|" & strLocation

        Try
            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCode, _
                                                   strParamName, _
                                                   strParamValue)


        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=DELETE&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/PM_trx_WaterQuality_List_KLR.aspx")
        End Try

        Response.Redirect("PM_trx_WaterQuality_List_KLR.aspx")
    End Sub

    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PM_trx_WaterQuality_List_KLR.aspx")
    End Sub

End Class
