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
Imports Microsoft.VisualBasic

Imports agri.GlobalHdl.clsGlobalHdl


Public Class PD_trx_YearOfPlantYieldDet : Inherits Page

    Protected WithEvents txtYearOfPlant As TextBox
    Protected WithEvents txtGroupRef As TextBox
    Protected WithEvents txtRefDate As TextBox
    Protected WithEvents txtRate As TextBox
    Protected WithEvents txtTotalWeight As TextBox

    Protected WithEvents lblPeriod As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label

    Protected WithEvents tbcode As HtmlInputHidden
    Protected WithEvents btnSelDate As Image
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrDup As Label
    Protected WithEvents lblErrRefDate As Label
    Protected WithEvents lblErrRefDateDesc As Label
    Protected WithEvents lblErrMatchYear As Label
    Protected WithEvents lblBlock As Label
    
    Dim objPDTrx As New agri.PD.clsTrx()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGLSetup As New agri.GL.clsSetup()

    Dim objYearPlantDs As New Object()
    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intPDAR As Integer
    Dim strCostLevel As String

    Dim strCompositKey As String = ""
    Dim intStatus As Integer
    Dim strDateFmt As String
	
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String
	

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PDACCMONTH")
        strAccYear = Session("SS_PDACCYEAR")
        intPDAR = Session("SS_PDAR")
        strDateFmt = Session("SS_DATEFMT")
        strCostLevel = Session("SS_COSTLEVEL")
    	
        strLocType = Session("SS_LOCTYPE")

        lblErrDup.Visible = False
        lblErrRefDate.Visible = False
        lblErrRefDateDesc.Visible = False
        lblErrMatchYear.Visible = False

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDYearOfPlantYield), intPDAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            strCompositKey = Trim(IIf(Request.QueryString("tbcode") <> "", Request.QueryString("tbcode"), Request.Form("tbcode")))
            intStatus = CInt(lblHiddenSts.Text)
            onload_GetLangCap()
            If Not IsPostBack Then
                If strCompositKey <> "" Then
                    tbcode.Value = strCompositKey
                    onLoad_Display()
                    onLoad_BindButton()
                Else
                    lblPeriod.Text = strAccMonth & "/" & strAccYear
                    onLoad_BindButton()
                End If
            End If
        End If
    End Sub


    Sub onLoad_Display()
        Dim strOpCdGet As String = "PD_CLSTRX_YEAR_OF_PLANT_YIELD_GET"
        Dim arrParam As Array
        Dim strParam As String        
        Dim intErrNo As Integer
        Dim strSearch As String = ""

        Dim strYearOfPlant As String
        Dim strYieldLocCode As String
        Dim strYieldAccMonth As String
        Dim strYieldAccYear As String
        Dim strGroupRef As String
        Dim strRefDate As String

        arrParam = Split(strCompositKey, "|")
        strYearOfPlant = Trim(arrParam(0))
        strYieldLocCode = Trim(arrParam(1))
        strYieldAccMonth = Trim(arrParam(2))
        strYieldAccYear = Trim(arrParam(3))
        strGroupRef = Trim(arrParam(4))
        strRefDate = Trim(arrParam(5))
        
        strSearch = strSearch & "and yld.Year = '" & strYearOfPlant & "' " & _
                                "and yld.LocCode = '" & strYieldLocCode & "' " & _
                                "and yld.AccMonth = '" & strYieldAccMonth & "' " & _
                                "and yld.AccYear = '" & strYieldAccYear & "' " & _
                                "and yld.GroupRef = '" & strGroupRef & "' " & _
                                "and yld.RefDate = '" & strRefDate & "' "

        strParam = strSearch & "|" & ""

        Try
            intErrNo = objPDTrx.mtdGetYearOfPlantYield(strOpCdGet, strParam, objYearPlantDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PD_TRX_YEAROFPLANTYIELDDET_GET&errmesg=" & lblErrMessage.Text & "&redirect=pd/trx/pd_trx_yearofplantyieldlist.aspx")
        End Try

        txtYearOfPlant.Text = Trim(objYearPlantDs.Tables(0).Rows(0).Item("Year"))
        txtGroupRef.Text = Trim(objYearPlantDs.Tables(0).Rows(0).Item("GroupRef"))
        txtRefDate.Text = objGlobal.GetShortDate(strDateFmt, objYearPlantDs.Tables(0).Rows(0).Item("RefDate"))
        txtRate.Text = objYearPlantDs.Tables(0).Rows(0).Item("Rate")
        txtTotalWeight.Text = objYearPlantDs.Tables(0).Rows(0).Item("TotalWeight")

        lblPeriod.Text = Trim(objYearPlantDs.Tables(0).Rows(0).Item("AccMonth")) & "/" & Trim(objYearPlantDs.Tables(0).Rows(0).Item("AccYear"))
        intStatus = CInt(Trim(objYearPlantDs.Tables(0).Rows(0).Item("Status")))
        lblHiddenSts.Text = Trim(objYearPlantDs.Tables(0).Rows(0).Item("Status"))
        lblStatus.Text = objPDTrx.mtdGetMPOBPriceStatus(Trim(objYearPlantDs.Tables(0).Rows(0).Item("Status")))

        lblDateCreated.Text = objGlobal.GetLongDate(objYearPlantDs.Tables(0).Rows(0).Item("CreateDate"))
        lblLastUpdate.Text = objGlobal.GetLongDate(objYearPlantDs.Tables(0).Rows(0).Item("UpdateDate"))
        lblUpdatedBy.Text = Trim(objYearPlantDs.Tables(0).Rows(0).Item("UserName"))
        
    End Sub


    Sub onLoad_BindButton()
        txtYearOfPlant.Enabled = False
        txtGroupRef.Enabled = False
        txtRefDate.Enabled = False
        txtRate.Enabled = False
        txtTotalWeight.Enabled = False
        SaveBtn.Visible = False
        DelBtn.Visible = False
        UnDelBtn.Visible = False
        btnSelDate.Visible = False
        Select Case intStatus
            Case objPDTrx.EnumYearOfPlantYieldStatus.Active
                txtRate.Enabled = True
                txtTotalWeight.Enabled = True
                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case objPDTrx.EnumYearOfPlantYieldStatus.Deleted
                UnDelBtn.Visible = True
            Case Else
                txtYearOfPlant.Enabled = True
                txtGroupRef.Enabled = True
                txtRefDate.Enabled = True
                btnSelDate.Visible = True
                txtRate.Enabled = True
                txtTotalWeight.Enabled = True
                SaveBtn.Visible = True
        End Select        
    End Sub


    Sub Button_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "PD_CLSTRX_YEAR_OF_PLANT_YIELD_UPD"
        Dim strOpCd_Get As String = "PD_CLSTRX_YEAR_OF_PLANT_YIELD_GET"
        Dim strOpCd_Add As String = "PD_CLSTRX_YEAR_OF_PLANT_YIELD_ADD"
        Dim strOpCd_ChkBlk As String = ""

        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument        
        Dim intErrNo As Integer
        Dim blnIsUpdate As Boolean
        Dim blnDupKey As Boolean
        Dim strParam As String = ""
        Dim objFormatDate As String
        Dim strSearchBlk As String
        Dim intErrType As Integer = 0

        Dim strYearOfPlant As String
        Dim strGroupRef As String
        Dim strRefDate As String 
        Dim dblRate As Double
        Dim dblTotalWeight As Double

        strYearOfPlant = txtYearOfPlant.Text
        strGroupRef = txtGroupRef.Text
        dblRate = txtRate.Text
        dblTotalWeight = txtTotalWeight.Text

        If LCase(strCostLevel) = "block" Then
            strOpCd_ChkBlk = "GL_CLSSETUP_BLOCK_LIST_GET"
            strSearchBlk = "and blk.Status = '" & objGLSetup.EnumBlockStatus.Active & "' " & _
                           "and blk.BlkType = '" & objGLSetup.EnumBlockType.MatureField & "' " & _
                           "and Year(blk.PlantingDate) = '" & strYearOfPlant & "' " 
        Else
            strOpCd_ChkBlk = "GL_CLSSETUP_SUBBLOCK_LIST_GET"
            strSearchBlk = "and sub.Status = '" & objGLSetup.EnumSubBlockStatus.Active & "' " & _
                           "and sub.SubBlkType = '" & objGLSetup.EnumSubBlockType.MatureField & "' " & _
                           "and Year(sub.PlantingDate) = '" & strYearOfPlant & "' "
        End If

        If Trim(txtRefDate.Text) = "" Then
            strRefDate = ""
        Else
            If objGlobal.mtdValidInputDate(strDateFmt, _
                                           txtRefDate.Text, _
                                           objFormatDate, _
                                           strRefDate) = False Then
                lblErrRefDate.Text = lblErrRefDateDesc.Text & objFormatDate & "."
                lblErrRefDate.Visible = True
                Exit Sub
            End If
        End If

        If strCmdArgs = "Save" Then 
            blnIsUpdate = IIf(intStatus = 0, False, True)
            tbcode.Value = strCompositKey  

            strParam = strYearOfPlant & Chr(9) & _
                       strGroupRef & Chr(9) & _
                       strRefDate & Chr(9) & _
                       dblRate & Chr(9) & _
                       dblTotalWeight & Chr(9) & _
                       objPDTrx.EnumYearOfPlantYieldStatus.Active

            Try
                intErrNo = objPDTrx.mtdUpdYearOfPlantYield(strOpCd_Get, _
                                                           strOpCd_Add, _
                                                           strOpCd_Upd, _
                                                           strOpCd_ChkBlk, _
                                                           strCompany, _
                                                           strLocation, _
                                                           strUserId, _
                                                           strAccMonth, _
                                                           strAccYear, _
                                                           strParam, _
                                                           strSearchBlk, _
                                                           blnDupKey, _
                                                           intErrType, _
                                                           blnIsUpdate)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PD_TRX_YEAROFPLANTYIELDDET_SAVE&errmesg=" & lblErrMessage.Text & "&redirect=pd/trx/pd_trx_yearofplantyieldlist.aspx?tbcode=" & strCompositKey)
            End Try

            If blnDupKey = True Then
                lblErrDup.Visible = True
                Exit Sub
            End If

            If intErrType = 1 Then
                lblErrMatchYear.Text = "<br>" & lblBlock.Text & " with the above Year of Planting does not exist."
                lblErrMatchYear.Visible = True
                Exit Sub
            End If

        ElseIf strCmdArgs = "Del" Then
            strParam = strYearOfPlant & Chr(9) & _
                       strGroupRef & Chr(9) & _
                       strRefDate & Chr(9) & Chr(9) & Chr(9) & _
                       objPDTrx.EnumYearOfPlantYieldStatus.Deleted

            Try
                intErrNo = objPDTrx.mtdUpdYearOfPlantYield(strOpCd_Get, _
                                                           strOpCd_Add, _
                                                           strOpCd_Upd, _
                                                           strOpCd_ChkBlk, _
                                                           strCompany, _
                                                           strLocation, _
                                                           strUserId, _
                                                           strAccMonth, _
                                                           strAccYear, _
                                                           strParam, _
                                                           strSearchBlk, _
                                                           False, _
                                                           intErrType, _
                                                           True)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PD_TRX_YEAROFPLANTYIELDDET_DEL&errmesg=" & lblErrMessage.Text & "&redirect=pd/trx/pd_trx_yearofplantyieldlist.aspx?tbcode=" & strCompositKey)
            End Try
        ElseIf strCmdArgs = "UnDel" Then
            strParam = strYearOfPlant & Chr(9) & _
                       strGroupRef & Chr(9) & _
                       strRefDate & Chr(9) & Chr(9) & Chr(9) & _
                       objPDTrx.EnumYearOfPlantYieldStatus.Active

            Try
                intErrNo = objPDTrx.mtdUpdYearOfPlantYield(strOpCd_Get, _
                                                           strOpCd_Add, _
                                                           strOpCd_Upd, _
                                                           strOpCd_ChkBlk, _
                                                           strCompany, _
                                                           strLocation, _
                                                           strUserId, _
                                                           strAccMonth, _
                                                           strAccYear, _
                                                           strParam, _
                                                           strSearchBlk, _
                                                           False, _
                                                           intErrType, _
                                                           True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PD_TRX_YEAROFPLANTYIELDDET_UNDEL&errmesg=" & lblErrMessage.Text & "&redirect=pd/trx/pd_trx_yearofplantyieldlist.aspx?tbcode=" & strCompositKey)
            End Try
        End If

        If strCompositKey = "" Then
            strCompositKey = strYearOfPlant & "|" & strLocation & "|" & strAccMonth & "|" & strAccYear & "|" & strGroupRef & "|" & strRefDate
        End If

        If strCompositKey <> "" Then
            onLoad_Display()
            onLoad_BindButton()
        End If
    End Sub

    Sub BackBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("PD_trx_YearOfPlantYieldList.aspx")
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        If LCase(strCostLevel) = "block" Then
            lblBlock.Text = GetCaption(objLangCap.EnumLangCap.Block)
        Else
            lblBlock.Text = GetCaption(objLangCap.EnumLangCap.SubBlock)
        End If
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PD_TRX_YEAROFPLANTYIELD_GET_LANGCAP&errmesg=" & Exp.ToString() & "&redirect=")
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



End Class
