
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


Public Class PM_OilLoss_Det : Inherits Page

    Dim objPMTrx As New agri.PM.clsTrx()
    Dim objPMSetup As New agri.PM.clsSetup()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()

    Dim objOk As New agri.GL.ClsTrx()
    'Protected WithEvents lblErrMessage As Label
    'Protected WithEvents lblDupMsg As Label
    'Protected WithEvents lblFmt As Label
    'Protected WithEvents lblDate As Label
    'Protected WithEvents blnUpdate As Label
    'Protected WithEvents lblOER As Label
    'Protected WithEvents lblKER As Label
    'Protected WithEvents lblPeriod As Label
    'Protected WithEvents lblStatus As Label
    'Protected WithEvents lblCreateDate As Label
    'Protected WithEvents lblLastUpdate As Label
    'Protected WithEvents lblUpdateBy As Label

    'Protected WithEvents txtdate As TextBox

    'Protected WithEvents Save As ImageButton
    'Protected WithEvents Delete As ImageButton
    'Protected WithEvents btnSelDateFrom As Image
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

    Dim strOppCd_OilLoss_GET As String = "PM_CLSTRX_OIL_LOSSES_GET"

    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String

    Private Sub InitializeComponent()

    End Sub

    Public Sub Page_Load(ByVal Sender As System.Object, ByVal E As System.EventArgs) Handles MyBase.Load
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


        Save.Visible = False
        Delete.Visible = False


        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMOilLoss), intPMAR) = False Then
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_OIL_LOSS_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/pm_trx_OilLoss_list.aspx")
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
        Dim strOppCode_Get As String = "PM_CLSTRX_OIL_LOSSES_LIST_GET"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""


        strParamName = "AM|AY|LOC|SEARCHSTR"
        strParamValue = Month(strTransDate) & "|" & Year(strTransDate) & "|" & strLocation & "|" & "AND  DateProd='" & strTransDate & "'"
         
       Try
            intErrNo = objOk.mtdGetDataCommon(strOppCode_Get, strParamName, strParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=wm_ticket&errmesg=" & Exp.Message & "&redirect=")
        End Try


        Return objDataSet
    End Function


    Sub DisableControl(ByVal pv_strstatus)
        Dim strView As Boolean

        If pv_strstatus = objPMTrx.EnumOilLossStatus.Deleted Then
            strView = False

        ElseIf pv_strstatus = objPMTrx.EnumOilLossStatus.Active Then
            strView = True
        End If

        txtdate.Enabled = strView
        Save.Visible = strView

    End Sub

    Sub DisplayData()

        Dim dsOilLoss As DataSet = LoadData()

        If dsOilLoss.Tables(0).Rows.Count > 0 Then
            txtdate.Text = objGlobal.GetShortDate(strDateFormat, Trim(dsOilLoss.Tables(0).Rows(0).Item("DateProd")))

            With dsOilLoss.Tables(0).Rows(0)
                txtOilDraftAkhir.Text = .Item("OILDraftAkhr")
                txtOilAfterEBP.Text = .Item("OILJksAftEBP")
                txtOilFibre.Text = .Item("OILFibPres")
                txtOilNutPres.Text = .Item("OILNutPres")
                txtOilSolid.Text = .Item("OILSolidDctr")
                txtOilLosses.Text = .Item("OILLosses")

                txtPKFibre.Text = .Item("KerFibCyc")
                txtPKLTDS1.Text = .Item("KerLDTS1")
                txtPKLTDS2.Text = .Item("KerLDTS2")
                txtPKShell.Text = .Item("KerClyBath")
                txtPKLosses.Text = .Item("KerLosses")
            
            End With
             

            lblPeriod.Text = Trim(dsOilLoss.Tables(0).Rows(0).Item("AccMonth")) & "/" & Trim(dsOilLoss.Tables(0).Rows(0).Item("AccYear"))
            'lblStatus.Text = objPMTrx.mtdGetOilLossStatus(Trim(dsOilLoss.Tables(0).Rows(0).Item("Status")))
            lblCreateDate.Text = objGlobal.GetLongDate(Trim(dsOilLoss.Tables(0).Rows(0).Item("CreateDate")))
            lblLastUpdate.Text = objGlobal.GetLongDate(Trim(dsOilLoss.Tables(0).Rows(0).Item("UpdateDate")))
            lblUpdateBy.Text = Trim(dsOilLoss.Tables(0).Rows(0).Item("UpdateId"))

            'DisableControl(Trim(dsOilLoss.Tables(0).Rows(0).Item("Status")))
            Delete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            'Delete.Visible = True
        End If
    End Sub


    Sub UpdateData(ByVal strAction As String)
        Dim strOppCd_OilLoss_ADD As String = "PM_CLSTRX_OIL_LOSSES_ADD"
        Dim strOppCd_OilLoss_UPD As String = "PM_CLSTRX_OIL_LOSSES_UPD"
        Dim blnDupKey As Boolean = False
        Dim blnUpd As Boolean = False
        Dim strStatus As String
        Dim strDate As String = CheckDate()
        Dim strParam1 As String
        Dim objDataSet1 As New DataSet
        Dim strMinStdWetOilRebus1 As String
        Dim strMaxStdWetOilRebus1 As String
        Dim strOilWetRebus1 As String
        Dim strVMRebus1 As String
        Dim strNOSRebus1 As String
        Dim strOilDMRebus1 As String
        Dim strMinStdOilDMRebus1 As String
        Dim strMaxStdOilDMRebus1 As String
        Dim strMinStdWetOilRebus2 As String
        Dim strMaxStdWetOilRebus2 As String
        Dim strOilWetRebus2 As String
        Dim strVMRebus2 As String
        Dim strNOSRebus2 As String
        Dim strOilDMRebus2 As String
        Dim strMinStdOilDMRebus2 As String
        Dim strMaxStdOilDMRebus2 As String
        Dim strMinStdWetOilJjgKsg As String
        Dim strMaxStdWetOilJjgKsg As String
        Dim strOilWetJjgKsg As String
        Dim strVMJjgKsg As String
        Dim strNOSJjgKsg As String
        Dim strOilDMJjgKsg As String
        Dim strMinStdOilDMJjgKsg As String
        Dim strMaxStdOilDMJjgKsg As String
        Dim strMinStdWetOilUSB As String
        Dim strMaxStdWetOilUSB As String
        Dim strOilWetUSB As String
        Dim strVMUSB As String
        Dim strNOSUSB As String
        Dim strOilDMUSB As String
        Dim strMinStdOilDMUSB As String
        Dim strMaxStdOilDMUSB As String
        Dim strMinStdWetOilFinal As String
        Dim strMaxStdWetOilFinal As String
        Dim strOilWetFinal As String
        Dim strVMFinal As String
        Dim strNOSFinal As String
        Dim strOilDMFinal As String
        Dim strMinStdOilDMFinal As String
        Dim strMaxStdOilDMFinal As String
        Dim strMinStdWetOilAmpas1 As String
        Dim strMaxStdWetOilAmpas1 As String
        Dim strOilWetAmpas1 As String
        Dim strVMAmpas1 As String
        Dim strNOSAmpas1 As String
        Dim strOilDMAmpas1 As String
        Dim strMinStdOilDMAmpas1 As String
        Dim strMaxStdOilDMAmpas1 As String
        Dim strMinStdWetOilAmpas2 As String
        Dim strMaxStdWetOilAmpas2 As String
        Dim strOilWetAmpas2 As String
        Dim strVMAmpas2 As String
        Dim strNOSAmpas2 As String
        Dim strOilDMAmpas2 As String
        Dim strMinStdOilDMAmpas2 As String
        Dim strMaxStdOilDMAmpas2 As String
        Dim strMinStdWetOilAmpas3 As String
        Dim strMaxStdWetOilAmpas3 As String
        Dim strOilWetAmpas3 As String
        Dim strVMAmpas3 As String
        Dim strNOSAmpas3 As String
        Dim strOilDMAmpas3 As String
        Dim strMinStdOilDMAmpas3 As String
        Dim strMaxStdOilDMAmpas3 As String
        Dim strMinStdWetOilAmpas4 As String
        Dim strMaxStdWetOilAmpas4 As String
        Dim strOilWetAmpas4 As String
        Dim strVMAmpas4 As String
        Dim strNOSAmpas4 As String
        Dim strOilDMAmpas4 As String
        Dim strMinStdOilDMAmpas4 As String
        Dim strMaxStdOilDMAmpas4 As String
        Dim strMinStdWetOilSludge1 As String
        Dim strMaxStdWetOilSludge1 As String
        Dim strOilWetSludge1 As String
        Dim strVMSludge1 As String
        Dim strNOSSludge1 As String
        Dim strOilDMSludge1 As String
        Dim strMinStdOilDMSludge1 As String
        Dim strMaxStdOilDMSludge1 As String
        Dim strMinStdWetOilSludge2 As String
        Dim strMaxStdWetOilSludge2 As String
        Dim strOilWetSludge2 As String
        Dim strVMSludge2 As String
        Dim strNOSSludge2 As String
        Dim strOilDMSludge2 As String
        Dim strMinStdOilDMSludge2 As String
        Dim strMaxStdOilDMSludge2 As String
        Dim strMinStdWetOilSludge3 As String
        Dim strMaxStdWetOilSludge3 As String
        Dim strOilWetSludge3 As String
        Dim strVMSludge3 As String
        Dim strNOSSludge3 As String
        Dim strOilDMSludge3 As String
        Dim strMinStdOilDMSludge3 As String
        Dim strMaxStdOilDMSludge3 As String
        Dim strMinStdWetOilSludge4 As String
        Dim strMaxStdWetOilSludge4 As String
        Dim strOilWetSludge4 As String
        Dim strVMSludge4 As String
        Dim strNOSSludge4 As String
        Dim strOilDMSludge4 As String
        Dim strMinStdOilDMSludge4 As String
        Dim strMaxStdOilDMSludge4 As String
        Dim strMinStdWetOilCOT As String
        Dim strMaxStdWetOilCOT As String
        Dim strOilWetCOT As String
        Dim strVMCOT As String
        Dim strNOSCOT As String
        Dim strOilDMCOT As String
        Dim strMinStdOilDMCOT As String
        Dim strMaxStdOilDMCOT As String
        Dim strMinStdWetOilUFC As String
        Dim strMaxStdWetOilUFC As String
        Dim strOilWetUFC As String
        Dim strVMUFC As String
        Dim strNOSUFC As String
        Dim strOilDMUFC As String
        Dim strMinStdOilDMUFC As String
        Dim strMaxStdOilDMUFC As String

        'strMinStdWetOilRebus1 = txtMinStdWetOilRebus1.Text
        'strMaxStdWetOilRebus1 = txtMaxStdWetOilRebus1.Text
        'strOilWetRebus1 = txtOilWetRebus1.Text
        'strVMRebus1 = txtVMRebus1.Text
        'strNOSRebus1 = txtNOSRebus1.Text
        'strOilDMRebus1 = txtOilDMRebus1.Text
        'strMinStdOilDMRebus1 = txtMinStdOilDMRebus1.Text
        'strMaxStdOilDMRebus1 = txtMaxStdOilDMRebus1.Text
        'strMinStdWetOilRebus2 = txtMinStdWetOilRebus2.Text
        'strMaxStdWetOilRebus2 = txtMaxStdWetOilRebus2.Text
        'strOilWetRebus2 = txtOilWetRebus2.Text
        'strVMRebus2 = txtVMRebus2.Text
        'strNOSRebus2 = txtNOSRebus2.Text
        'strOilDMRebus2 = txtOilDMRebus2.Text
        'strMinStdOilDMRebus2 = txtMinStdOilDMRebus2.Text
        'strMaxStdOilDMRebus2 = txtMaxStdOilDMRebus2.Text
        'strMinStdWetOilJjgKsg = txtMinStdWetOilJjgKsg.Text
        'strMaxStdWetOilJjgKsg = txtMaxStdWetOilJjgKsg.Text
        'strOilWetJjgKsg = txtOilWetJjgKsg.Text
        'strVMJjgKsg = txtVMJjgKsg.Text
        'strNOSJjgKsg = txtNOSJjgKsg.Text
        'strOilDMJjgKsg = txtOilDMJjgKsg.Text
        'strMinStdOilDMJjgKsg = txtMinStdOilDMJjgKsg.Text
        'strMaxStdOilDMJjgKsg = txtMaxStdOilDMJjgKsg.Text
        'strMinStdWetOilUSB = txtMinStdWetOilUSB.Text
        'strMaxStdWetOilUSB = txtMaxStdWetOilUSB.Text
        'strOilWetUSB = txtOilWetUSB.Text
        'strVMUSB = txtVMUSB.Text
        'strNOSUSB = txtNOSUSB.Text
        'strOilDMUSB = txtOilDMUSB.Text
        'strMinStdOilDMUSB = txtMinStdOilDMUSB.Text
        'strMaxStdOilDMUSB = txtMaxStdOilDMUSB.Text
        'strMinStdWetOilFinal = txtMinStdWetOilFinal.Text
        'strMaxStdWetOilFinal = txtMaxStdWetOilFinal.Text
        'strOilWetFinal = txtOilWetFinal.Text
        'strVMFinal = txtVMFinal.Text
        'strNOSFinal = txtNOSFinal.Text
        'strOilDMFinal = txtOilDMFinal.Text
        'strMinStdOilDMFinal = txtMinStdOilDMFinal.Text
        'strMaxStdOilDMFinal = txtMaxStdOilDMFinal.Text
        'strMinStdWetOilAmpas1 = txtMinStdWetOilAmpas1.Text
        'strMaxStdWetOilAmpas1 = txtMaxStdWetOilAmpas1.Text
        'strOilWetAmpas1 = txtOilWetAmpas1.Text
        'strVMAmpas1 = txtVMAmpas1.Text
        'strNOSAmpas1 = txtNOSAmpas1.Text
        'strOilDMAmpas1 = txtOilDMAmpas1.Text
        'strMinStdOilDMAmpas1 = txtMinStdOilDMAmpas1.Text
        'strMaxStdOilDMAmpas1 = txtMaxStdOilDMAmpas1.Text
        'strMinStdWetOilAmpas2 = txtMinStdWetOilAmpas2.Text
        'strMaxStdWetOilAmpas2 = txtMaxStdWetOilAmpas2.Text
        'strOilWetAmpas2 = txtOilWetAmpas2.Text
        'strVMAmpas2 = txtVMAmpas2.Text
        'strNOSAmpas2 = txtNOSAmpas2.Text
        'strOilDMAmpas2 = txtOilDMAmpas2.Text
        'strMinStdOilDMAmpas2 = txtMinStdOilDMAmpas2.Text
        'strMaxStdOilDMAmpas2 = txtMaxStdOilDMAmpas2.Text
        'strMinStdWetOilAmpas3 = txtMinStdWetOilAmpas3.Text
        'strMaxStdWetOilAmpas3 = txtMaxStdWetOilAmpas3.Text
        'strOilWetAmpas3 = txtOilWetAmpas3.Text
        'strVMAmpas3 = txtVMAmpas3.Text
        'strNOSAmpas3 = txtNOSAmpas3.Text
        'strOilDMAmpas3 = txtOilDMAmpas3.Text
        'strMinStdOilDMAmpas3 = txtMinStdOilDMAmpas3.Text
        'strMaxStdOilDMAmpas3 = txtMaxStdOilDMAmpas3.Text
        'strMinStdWetOilAmpas4 = txtMinStdWetOilAmpas4.Text
        'strMaxStdWetOilAmpas4 = txtMaxStdWetOilAmpas4.Text
        'strOilWetAmpas4 = txtOilWetAmpas4.Text
        'strVMAmpas4 = txtVMAmpas4.Text
        'strNOSAmpas4 = txtNOSAmpas4.Text
        'strOilDMAmpas4 = txtOilDMAmpas4.Text
        'strMinStdOilDMAmpas4 = txtMinStdOilDMAmpas4.Text
        'strMaxStdOilDMAmpas4 = txtMaxStdOilDMAmpas4.Text
        'strMinStdWetOilSludge1 = txtMinStdWetOilSludge1.Text
        'strMaxStdWetOilSludge1 = txtMaxStdWetOilSludge1.Text
        'strOilWetSludge1 = txtOilWetSludge1.Text
        'strVMSludge1 = txtVMSludge1.Text
        'strNOSSludge1 = txtNOSSludge1.Text
        'strOilDMSludge1 = txtOilDMSludge1.Text
        'strMinStdOilDMSludge1 = txtMinStdOilDMSludge1.Text
        'strMaxStdOilDMSludge1 = txtMaxStdOilDMSludge1.Text
        'strMinStdWetOilSludge2 = txtMinStdWetOilSludge2.Text
        'strMaxStdWetOilSludge2 = txtMaxStdWetOilSludge2.Text
        'strOilWetSludge2 = txtOilWetSludge2.Text
        'strVMSludge2 = txtVMSludge2.Text
        'strNOSSludge2 = txtNOSSludge2.Text
        'strOilDMSludge2 = txtOilDMSludge2.Text
        'strMinStdOilDMSludge2 = txtMinStdOilDMSludge2.Text
        'strMaxStdOilDMSludge2 = txtMaxStdOilDMSludge2.Text
        'strMinStdWetOilSludge3 = txtMinStdWetOilSludge3.Text
        'strMaxStdWetOilSludge3 = txtMaxStdWetOilSludge3.Text
        'strOilWetSludge3 = txtOilWetSludge3.Text
        'strVMSludge3 = txtVMSludge3.Text
        'strNOSSludge3 = txtNOSSludge3.Text
        'strOilDMSludge3 = txtOilDMSludge3.Text
        'strMinStdOilDMSludge3 = txtMinStdOilDMSludge3.Text
        'strMaxStdOilDMSludge3 = txtMaxStdOilDMSludge3.Text
        'strMinStdWetOilSludge4 = txtMinStdWetOilSludge4.Text
        'strMaxStdWetOilSludge4 = txtMaxStdWetOilSludge4.Text
        'strOilWetSludge4 = txtOilWetSludge4.Text
        'strVMSludge4 = txtVMSludge4.Text
        'strNOSSludge4 = txtNOSSludge4.Text
        'strOilDMSludge4 = txtOilDMSludge4.Text
        'strMinStdOilDMSludge4 = txtMinStdOilDMSludge4.Text
        'strMaxStdOilDMSludge4 = txtMaxStdOilDMSludge4.Text
        'strMinStdWetOilCOT = txtMinStdWetOilCOT.Text
        'strMaxStdWetOilCOT = txtMaxStdWetOilCOT.Text
        'strOilWetCOT = txtOilWetCOT.Text
        'strVMCOT = txtVMCOT.Text
        'strNOSCOT = txtNOSCOT.Text
        'strOilDMCOT = txtOilDMCOT.Text
        'strMinStdOilDMCOT = txtMinStdOilDMCOT.Text
        'strMaxStdOilDMCOT = txtMaxStdOilDMCOT.Text
        'strMinStdWetOilUFC = txtMinStdWetOilUFC.Text
        'strMaxStdWetOilUFC = txtMaxStdWetOilUFC.Text
        'strOilWetUFC = txtOilWetUFC.Text
        'strVMUFC = txtVMUFC.Text
        'strNOSUFC = txtNOSUFC.Text
        'strOilDMUFC = txtOilDMUFC.Text
        'strMinStdOilDMUFC = txtMinStdOilDMUFC.Text
        'strMaxStdOilDMUFC = txtMaxStdOilDMUFC.Text

        If strEdit = "True" Then
            blnUpd = True
        Else
            blnUpd = False

            strParam1 = "||TransDate||" & _
                      strDate & "|||"

            Try
                intErrNo = objPMTrx.mtdGetOilLosses(strOppCd_OilLoss_GET, strLocation, strParam1, objDataSet1)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_OilLoss_GET&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/PM_trx_OilLoss_List.aspx")
            End Try

            If objDataSet1.Tables(0).Rows.Count > 0 Then
                blnDupKey = True
                lblDupMsg.Visible = True
                Exit Sub
            Else
                blnDupKey = False
            End If
        End If

        strStatus = IIf(lblStatus.Text = objPMTrx.mtdGetOilLossStatus(objPMTrx.EnumOilLossStatus.Deleted), _
                        objPMTrx.EnumOilLossStatus.Deleted, _
                        objPMTrx.EnumOilLossStatus.Active)

        strParam = strDate & "|" & _
                      strMinStdWetOilRebus1 & "|" & _
                        strMaxStdWetOilRebus1 & "|" & _
                        strOilWetRebus1 & "|" & _
                        strVMRebus1 & "|" & _
                        strNOSRebus1 & "|" & _
                        strOilDMRebus1 & "|" & _
                        strMinStdOilDMRebus1 & "|" & _
                        strMaxStdOilDMRebus1 & "|" & _
                        strMinStdWetOilRebus2 & "|" & _
                        strMaxStdWetOilRebus2 & "|" & _
                        strOilWetRebus2 & "|" & _
                        strVMRebus2 & "|" & _
                        strNOSRebus2 & "|" & _
                        strOilDMRebus2 & "|" & _
                        strMinStdOilDMRebus2 & "|" & _
                        strMaxStdOilDMRebus2 & "|" & _
                        strMinStdWetOilJjgKsg & "|" & _
                        strMaxStdWetOilJjgKsg & "|" & _
                        strOilWetJjgKsg & "|" & _
                        strVMJjgKsg & "|" & _
                        strNOSJjgKsg & "|" & _
                        strOilDMJjgKsg & "|" & _
                        strMinStdOilDMJjgKsg & "|" & _
                        strMaxStdOilDMJjgKsg & "|" & _
                        strMinStdWetOilUSB & "|" & _
                        strMaxStdWetOilUSB & "|" & _
                        strOilWetUSB & "|" & _
                        strVMUSB & "|" & _
                        strNOSUSB & "|" & _
                        strOilDMUSB & "|" & _
                        strMinStdOilDMUSB & "|" & _
                        strMaxStdOilDMUSB & "|" & _
                        strMinStdWetOilFinal & "|" & _
                        strMaxStdWetOilFinal & "|" & _
                        strOilWetFinal & "|" & _
                        strVMFinal & "|" & _
                        strNOSFinal & "|" & _
                        strOilDMFinal & "|" & _
                        strMinStdOilDMFinal & "|" & _
                        strMaxStdOilDMFinal & "|" & _
                        strMinStdWetOilAmpas1 & "|" & _
                        strMaxStdWetOilAmpas1 & "|" & _
                        strOilWetAmpas1 & "|" & _
                        strVMAmpas1 & "|" & _
                        strNOSAmpas1 & "|" & _
                        strOilDMAmpas1 & "|" & _
                        strMinStdOilDMAmpas1 & "|" & _
                        strMaxStdOilDMAmpas1 & "|" & _
                        strMinStdWetOilAmpas2 & "|" & _
                        strMaxStdWetOilAmpas2 & "|" & _
                        strOilWetAmpas2 & "|" & _
                        strVMAmpas2 & "|" & _
                        strNOSAmpas2 & "|" & _
                        strOilDMAmpas2 & "|" & _
                        strMinStdOilDMAmpas2 & "|" & _
                        strMaxStdOilDMAmpas2 & "|" & _
                        strMinStdWetOilAmpas3 & "|" & _
                        strMaxStdWetOilAmpas3 & "|" & _
                        strOilWetAmpas3 & "|" & _
                        strVMAmpas3 & "|" & _
                        strNOSAmpas3 & "|" & _
                        strOilDMAmpas3 & "|" & _
                        strMinStdOilDMAmpas3 & "|" & _
                        strMaxStdOilDMAmpas3 & "|" & _
                        strMinStdWetOilAmpas4 & "|" & _
                        strMaxStdWetOilAmpas4 & "|" & _
                        strOilWetAmpas4 & "|" & _
                        strVMAmpas4 & "|" & _
                        strNOSAmpas4 & "|" & _
                        strOilDMAmpas4 & "|" & _
                        strMinStdOilDMAmpas4 & "|" & _
                        strMaxStdOilDMAmpas4 & "|" & _
                        strMinStdWetOilSludge1 & "|" & _
                        strMaxStdWetOilSludge1 & "|" & _
                        strOilWetSludge1 & "|" & _
                        strVMSludge1 & "|" & _
                        strNOSSludge1 & "|" & _
                        strOilDMSludge1 & "|" & _
                        strMinStdOilDMSludge1 & "|" & _
                        strMaxStdOilDMSludge1 & "|" & _
                        strMinStdWetOilSludge2 & "|" & _
                        strMaxStdWetOilSludge2 & "|" & _
                        strOilWetSludge2 & "|" & _
                        strVMSludge2 & "|" & _
                        strNOSSludge2 & "|" & _
                        strOilDMSludge2 & "|" & _
                        strMinStdOilDMSludge2 & "|" & _
                        strMaxStdOilDMSludge2 & "|" & _
                        strMinStdWetOilSludge3 & "|" & _
                        strMaxStdWetOilSludge3 & "|" & _
                        strOilWetSludge3 & "|" & _
                        strVMSludge3 & "|" & _
                        strNOSSludge3 & "|" & _
                        strOilDMSludge3 & "|" & _
                        strMinStdOilDMSludge3 & "|" & _
                        strMaxStdOilDMSludge3 & "|" & _
                        strMinStdWetOilSludge4 & "|" & _
                        strMaxStdWetOilSludge4 & "|" & _
                        strOilWetSludge4 & "|" & _
                        strVMSludge4 & "|" & _
                        strNOSSludge4 & "|" & _
                        strOilDMSludge4 & "|" & _
                        strMinStdOilDMSludge4 & "|" & _
                        strMaxStdOilDMSludge4 & "|" & _
                        strMinStdWetOilCOT & "|" & _
                        strMaxStdWetOilCOT & "|" & _
                        strOilWetCOT & "|" & _
                        strVMCOT & "|" & _
                        strNOSCOT & "|" & _
                        strOilDMCOT & "|" & _
                        strMinStdOilDMCOT & "|" & _
                        strMaxStdOilDMCOT & "|" & _
                        strMinStdWetOilUFC & "|" & _
                        strMaxStdWetOilUFC & "|" & _
                        strOilWetUFC & "|" & _
                        strVMUFC & "|" & _
                        strNOSUFC & "|" & _
                        strOilDMUFC & "|" & _
                        strMinStdOilDMUFC & "|" & _
                        strMaxStdOilDMUFC & "|" & _
                        strStatus

        Try
            intErrNo = objPMTrx.mtdUpdOilLosses(strOppCd_OilLoss_ADD, _
                                              strOppCd_OilLoss_UPD, _
                                              strLocation, _
                                              strUserId, _
                                              strAccMonth, _
                                              strAccYear, _
                                              strParam, _
                                              blnUpd)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_OIL_LOSS_UPDATE&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/pm_trx_OilLoss_list.aspx")
        End Try

        Select Case blnUpd
            Case True
                Response.Redirect("PM_trx_OilLoss_Det.aspx?LocCode=" & strLocation & "&TransDate=" & strTransDate & "&Edit=True")
            Case False
                Response.Redirect("PM_trx_OilLoss_List.aspx")
        End Select

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
        Dim strOppCd_DEL As String = "PM_CLSTRX_OIL_LOSSES_DEL"
        Dim strDate As String = CheckDate()
        Dim strParam As String

        strParam = strLocation & "|" & strDate
        Try
            intErrNo = objPMTrx.mtdPhysicalDelete(strOppCd_DEL, _
                                                  strParam, _
                                                  objPMTrx.EnumTransType.OilLosses)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_OilLoss_DEL&errmesg=" & lblErrMessage.Text & "&redirect=PM/Trx/PM_trx_OilLoss_List.aspx")
        End Try

        Response.Redirect("PM_trx_OilLoss_List.aspx")
    End Sub

    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PM_trx_OilLoss_List.aspx")
    End Sub

End Class
