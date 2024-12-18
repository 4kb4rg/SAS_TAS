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

Public Class PM_trx_OilQuality_Det_KLR : Inherits Page

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


    Protected WithEvents txtFFA1 As TextBox
    Protected WithEvents txtFFA2 As TextBox
    Protected WithEvents txtFFA3 As TextBox
    Protected WithEvents txtFFA_K As TextBox

    Protected WithEvents txtVM1 As TextBox
    Protected WithEvents txtVM2 As TextBox
    Protected WithEvents txtVM3 As TextBox
    Protected WithEvents txtVM_K As TextBox

    Protected WithEvents txtDirt1 As TextBox
    Protected WithEvents txtDirt2 As TextBox
    Protected WithEvents txtDirt3 As TextBox
    Protected WithEvents txtDirt_K As TextBox

    Protected WithEvents txtTank1FAA As TextBox
    Protected WithEvents txtTank1Moist As TextBox
    Protected WithEvents txtTank1Dirt As TextBox

    Protected WithEvents txtTank2FAA As TextBox
    Protected WithEvents txtTank2Moist As TextBox
    Protected WithEvents txtTank2Dirt As TextBox

    Protected WithEvents txtTank3FAA As TextBox
    Protected WithEvents txtTank3Moist As TextBox
    Protected WithEvents txtTank3Dirt As TextBox

    Protected WithEvents txtTank4FAA As TextBox
    Protected WithEvents txtTank4Moist As TextBox
    Protected WithEvents txtTank4Dirt As TextBox

    Protected WithEvents txtTank5FAA As TextBox
    Protected WithEvents txtTank5Moist As TextBox
    Protected WithEvents txtTank5Dirt As TextBox

    Protected WithEvents txtTank6FAA As TextBox
    Protected WithEvents txtTank6Moist As TextBox
    Protected WithEvents txtTank6Dirt As TextBox

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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/PM_trx_OilQuality_List_KLR.aspx")
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
        Dim strOpCode As String = "PM_CLSTRX_OILQUALITY_KLR_GET"

        strParamName = "TRANSDATE|LOCCODE"
        strParamValue = strTransDate & "|" & strLocation

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCode, strParamName, strParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PM_GET_DATA&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/PM_trx_OilQuality_List_KLR.aspx")
        End Try


        Return objDataSet
    End Function


    Sub DisableControl(ByVal pv_strstatus)
        Dim strView As Boolean

        If pv_strstatus = objPMTrx.EnumOilQualityStatus.Deleted Then
            strView = False

        ElseIf pv_strstatus = objPMTrx.EnumOilQualityStatus.Active Then
            strView = True
        End If

        txtdate.Enabled = strView
        Save.Visible = strView

    End Sub

    Sub DisplayData()

        Dim dsDisplay As DataSet = LoadData()

        If dsDisplay.Tables(0).Rows.Count > 0 Then
            txtdate.Text = objGlobal.GetShortDate(strDateFormat, Trim(dsDisplay.Tables(0).Rows(0).Item("TransDate")))


            txtFFA1.Text = dsDisplay.Tables(0).Rows(0).Item("FFA1")
            txtFFA2.Text = dsDisplay.Tables(0).Rows(0).Item("FFA2")
            txtFFA3.Text = dsDisplay.Tables(0).Rows(0).Item("FFA3")
            txtFFA_K.Text = Trim(dsDisplay.Tables(0).Rows(0).Item("FFA_K"))

            txtVM1.Text = dsDisplay.Tables(0).Rows(0).Item("VM1")
            txtVM2.Text = dsDisplay.Tables(0).Rows(0).Item("VM2")
            txtVM3.Text = dsDisplay.Tables(0).Rows(0).Item("VM3")
            txtVM_K.Text = Trim(dsDisplay.Tables(0).Rows(0).Item("VM_K"))

            txtDirt1.Text = dsDisplay.Tables(0).Rows(0).Item("Dirt1")
            txtDirt2.Text = dsDisplay.Tables(0).Rows(0).Item("Dirt2")
            txtDirt3.Text = dsDisplay.Tables(0).Rows(0).Item("Dirt3")
            txtDirt_K.Text = Trim(dsDisplay.Tables(0).Rows(0).Item("Dirt_K"))

            txtTank1FAA.Text = dsDisplay.Tables(0).Rows(0).Item("Tank1FAA")
            txtTank1Moist.Text = dsDisplay.Tables(0).Rows(0).Item("Tank1Moist")
            txtTank1Dirt.Text = dsDisplay.Tables(0).Rows(0).Item("Tank1Dirt")

            txtTank2FAA.Text = dsDisplay.Tables(0).Rows(0).Item("Tank2FAA")
            txtTank2Moist.Text = dsDisplay.Tables(0).Rows(0).Item("Tank2Moist")
            txtTank2Dirt.Text = dsDisplay.Tables(0).Rows(0).Item("Tank2Dirt")

            txtTank3FAA.Text = dsDisplay.Tables(0).Rows(0).Item("Tank3FAA")
            txtTank3Moist.Text = dsDisplay.Tables(0).Rows(0).Item("Tank3Moist")
            txtTank3Dirt.Text = dsDisplay.Tables(0).Rows(0).Item("Tank3Dirt")

            txtTank4FAA.Text = dsDisplay.Tables(0).Rows(0).Item("Tank4FAA")
            txtTank4Moist.Text = dsDisplay.Tables(0).Rows(0).Item("Tank4Moist")
            txtTank4Dirt.Text = dsDisplay.Tables(0).Rows(0).Item("Tank4Dirt")

            txtTank5FAA.Text = dsDisplay.Tables(0).Rows(0).Item("Tank5FAA")
            txtTank5Moist.Text = dsDisplay.Tables(0).Rows(0).Item("Tank5Moist")
            txtTank5Dirt.Text = dsDisplay.Tables(0).Rows(0).Item("Tank5Dirt")

            txtTank6FAA.Text = dsDisplay.Tables(0).Rows(0).Item("Tank6FAA")
            txtTank6Moist.Text = dsDisplay.Tables(0).Rows(0).Item("Tank6Moist")
            txtTank6Dirt.Text = dsDisplay.Tables(0).Rows(0).Item("Tank6Dirt")

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
            strOpCodeTrans = "PM_CLSTRX_OILQUALITY_KLR_UPDATE"
        Else
            blnUpd = False
            strOpCodeTrans = "PM_CLSTRX_OILQUALITY_KLR_INSERT"

            strOpCode = "PM_CLSTRX_OILQUALITY_KLR_GET"

            strParamName = "TRANSDATE|LOCCODE"
            strParamValue = strDate & "|" & strLocation

            Try
                intErrNo = objGLTrx.mtdGetDataCommon(strOpCode, strParamName, strParamValue, objDataSet1)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PM_GET_DATA&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/PM_trx_OilQuality_List_KLR.aspx")
            End Try

            If objDataSet1.Tables(0).Rows.Count > 0 Then
                blnDuDKey = True
                lblDupMsg.Visible = True
                Exit Sub
            Else
                blnDuDKey = False
            End If
        End If


        strStatus = IIf(lblStatus.Text = objPMTrx.mtdGetOilQualityStatus(objPMTrx.EnumOilQualityStatus.Deleted), _
                        objPMTrx.EnumOilQualityStatus.Deleted, _
                        objPMTrx.EnumOilQualityStatus.Active)
     
        strParamName = ""
        strParamName = "TRANSDATE|LOCCODE|UPDATEID|STATUS|ACCMONTH|ACCYEAR|"
        strParamName = strParamName & "FFA1|FFA2|FFA3|FFA_K|"
        strParamName = strParamName & "VM1|VM2|VM3|VM_K|"
        strParamName = strParamName & "Dirt1|Dirt2|Dirt3|Dirt_K|"
        strParamName = strParamName & "Tank1FAA|Tank1Moist|Tank1Dirt|"
        strParamName = strParamName & "Tank2FAA|Tank2Moist|Tank2Dirt|"
        strParamName = strParamName & "Tank3FAA|Tank3Moist|Tank3Dirt|"
        strParamName = strParamName & "Tank4FAA|Tank4Moist|Tank4Dirt|"
        strParamName = strParamName & "Tank5FAA|Tank5Moist|Tank5Dirt|"
        strParamName = strParamName & "Tank6FAA|Tank6Moist|Tank6Dirt"

        strParamValue = ""
        strParamValue = strDate & "|" & strLocation & "|" & strUserId & "|"
        strParamValue = strParamValue & strStatus & "|" & strAccMonth & "|" & strAccYear & "|"
        strParamValue = strParamValue & Val(txtFFA1.Text) & "|" & Val(txtFFA2.Text) & "|" & Val(txtFFA3.Text) & "|" & TRIM(txtFFA_K.Text) & "|"
        strParamValue = strParamValue & Val(txtVM1.Text) & "|" & Val(txtVM2.Text) & "|" & Val(txtVM3.Text) & "|" & TRIM(txtVM_K.Text) & "|"
        strParamValue = strParamValue & Val(txtDirt1.Text) & "|" & Val(txtDirt2.Text) & "|" & Val(txtDirt3.Text) & "|" & TRIM(txtDirt_K.Text) & "|"
        strParamValue = strParamValue & Val(txtTank1FAA.Text) & "|" & Val(txtTank1Moist.Text) & "|" & Val(txtTank1Dirt.Text) & "|"
        strParamValue = strParamValue & Val(txtTank2FAA.Text) & "|" & Val(txtTank2Moist.Text) & "|" & Val(txtTank2Dirt.Text) & "|"
        strParamValue = strParamValue & Val(txtTank3FAA.Text) & "|" & Val(txtTank3Moist.Text) & "|" & Val(txtTank3Dirt.Text) & "|"
        strParamValue = strParamValue & Val(txtTank4FAA.Text) & "|" & Val(txtTank4Moist.Text) & "|" & Val(txtTank4Dirt.Text) & "|"
        strParamValue = strParamValue & Val(txtTank5FAA.Text) & "|" & Val(txtTank5Moist.Text) & "|" & Val(txtTank5Dirt.Text) & "|"
        strParamValue = strParamValue & Val(txtTank6FAA.Text) & "|" & Val(txtTank6Moist.Text) & "|" & Val(txtTank6Dirt.Text) & "|"

        Try
            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCodeTrans, _
                                                   strParamName, _
                                                   strParamValue)


        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UPDATE&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/PM_trx_OilQuality_List_KLR.aspx")
        End Try

       
        Response.Redirect("PM_trx_OilQuality_Det_KLR.aspx?LocCode=" & strLocation & "&TransDate=" & strDate & "&Edit=True")
          

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
        Dim strOpCode As String = "PM_CLSTRX_OILQUALITY_KLR_DEL"
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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=DELETE&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/PM_trx_OilQuality_List_KLR.aspx")
        End Try

        Response.Redirect("PM_trx_OilQuality_List_KLR.aspx")
    End Sub

    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PM_trx_OilQuality_List_KLR.aspx")
    End Sub

End Class
