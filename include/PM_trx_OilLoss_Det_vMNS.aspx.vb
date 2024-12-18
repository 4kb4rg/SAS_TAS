
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

Public Class PM_oilLoss_Det : Inherits Page

    Dim objPMTrx As New agri.PM.clsTrx
    Dim objPMSetup As New agri.PM.clsSetup()
 
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblDupMsg As Label
    Protected WithEvents lblFmt As Label
    Protected WithEvents lblDate As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents lblPeriod As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblCreateDate As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdateBy As Label
    Protected WithEvents lblId As Label

    Protected WithEvents txtdate As TextBox
    Protected WithEvents ddlTestSampleCode As DropDownList

    Protected WithEvents Save As ImageButton    
    Protected WithEvents Delete As ImageButton
    Protected WithEvents btnSelDateFrom As Image
    
    Protected WithEvents btnNew As ImageButton 

    Dim strTransId As String
  
    Dim strEdit As String

    Dim strParam As String
    Dim strParamName As String

    Dim objDataSet As New DataSet()
    Dim objTestSampleCode As New DataSet()
    Dim objMachine As New DataSet()
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

    Protected WithEvents lblTracker As System.Web.UI.WebControls.Label
    Protected WithEvents rfvDate As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents rfvTestSampleCode As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents Back As System.Web.UI.WebControls.ImageButton
    Protected WithEvents lbl1 As System.Web.UI.WebControls.Label
    Protected WithEvents txt1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvTxt1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents revTxt1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents lblType1 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl2 As System.Web.UI.WebControls.Label
    Protected WithEvents txt2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvTxt2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents revTxt2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents lblType2 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl3 As System.Web.UI.WebControls.Label
    Protected WithEvents txt3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvTxt3 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents revTxt3 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents lblType3 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl4 As System.Web.UI.WebControls.Label
    Protected WithEvents txt4 As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvTxt4 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents revTxt4 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents lblType4 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl5 As System.Web.UI.WebControls.Label
    Protected WithEvents txt5 As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvTxt5 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents revTxt5 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents lblType5 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl6 As System.Web.UI.WebControls.Label
    Protected WithEvents txt6 As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvTxt6 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents revTxt6 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents lblType6 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl7 As System.Web.UI.WebControls.Label
    Protected WithEvents txt7 As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvTxt7 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents revTxt7 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents lblType7 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl8 As System.Web.UI.WebControls.Label
    Protected WithEvents txt8 As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvTxt8 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents revTxt8 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents lblType8 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl9 As System.Web.UI.WebControls.Label
    Protected WithEvents txt9 As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvTxt9 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents revTxt9 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents lblType9 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl10 As System.Web.UI.WebControls.Label
    Protected WithEvents txt10 As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvTxt10 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents revTxt10 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents lblType10 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl11 As System.Web.UI.WebControls.Label
    Protected WithEvents txt11 As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvTxt11 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents revTxt11 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents lblType11 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl12 As System.Web.UI.WebControls.Label
    Protected WithEvents txt12 As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvTxt12 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents revTxt12 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents lblType12 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl13 As System.Web.UI.WebControls.Label
    Protected WithEvents txt13 As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvTxt13 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents revTxt13 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents lblType13 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl14 As System.Web.UI.WebControls.Label
    Protected WithEvents txt14 As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvTxt14 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents revTxt14 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents lblType14 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl15 As System.Web.UI.WebControls.Label
    Protected WithEvents txt15 As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvTxt15 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents revTxt15 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents lblType15 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl16 As System.Web.UI.WebControls.Label
    Protected WithEvents txt16 As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvTxt16 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents revTxt16 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents lblType16 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl17 As System.Web.UI.WebControls.Label
    Protected WithEvents txt17 As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvTxt17 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents revTxt17 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents lblType17 As System.Web.UI.WebControls.Label

    Protected WithEvents lbl18 As System.Web.UI.WebControls.Label
    Protected WithEvents txt18 As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvTxt18 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents revTxt18 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents lblType18 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl19 As System.Web.UI.WebControls.Label
    Protected WithEvents txt19 As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvTxt19 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents revTxt19 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents lblType19 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl20 As System.Web.UI.WebControls.Label
    Protected WithEvents txt20 As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvTxt20 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents revTxt20 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents lblType20 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl21 As System.Web.UI.WebControls.Label
    Protected WithEvents txt21 As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvTxt21 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents revTxt21 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents lblType21 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl22 As System.Web.UI.WebControls.Label
    Protected WithEvents txt22 As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvTxt22 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents revTxt22 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents lblType22 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl23 As System.Web.UI.WebControls.Label
    Protected WithEvents txt23 As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvTxt23 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents revTxt23 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents lblType23 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl24 As System.Web.UI.WebControls.Label
    Protected WithEvents txt24 As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvTxt24 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents revTxt24 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents lblType24 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl25 As System.Web.UI.WebControls.Label
    Protected WithEvents txt25 As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvTxt25 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents revTxt25 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents lblType25 As System.Web.UI.WebControls.Label

    Protected WithEvents tr1 As HtmlTableRow
    Protected WithEvents tr2 As HtmlTableRow
    Protected WithEvents tr3 As HtmlTableRow
    Protected WithEvents tr4 As HtmlTableRow
    Protected WithEvents tr5 As HtmlTableRow
    Protected WithEvents tr6 As HtmlTableRow
    Protected WithEvents tr7 As HtmlTableRow
    Protected WithEvents tr8 As HtmlTableRow
    Protected WithEvents tr9 As HtmlTableRow
    Protected WithEvents tr10 As HtmlTableRow
    Protected WithEvents tr11 As HtmlTableRow
    Protected WithEvents tr12 As HtmlTableRow
    Protected WithEvents tr13 As HtmlTableRow
    Protected WithEvents tr14 As HtmlTableRow
    Protected WithEvents tr15 As HtmlTableRow
    Protected WithEvents tr16 As HtmlTableRow
    Protected WithEvents tr17 As HtmlTableRow
    Protected WithEvents tr18 As HtmlTableRow
    Protected WithEvents tr19 As HtmlTableRow
    Protected WithEvents tr20 As HtmlTableRow
    Protected WithEvents tr21 As HtmlTableRow
    Protected WithEvents tr22 As HtmlTableRow
    Protected WithEvents tr23 As HtmlTableRow
    Protected WithEvents tr24 As HtmlTableRow
    Protected WithEvents tr25 As HtmlTableRow


    Protected WithEvents rfvStation As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents ddlStation As System.Web.UI.WebControls.DropDownList
    Protected WithEvents rfvMachine As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents ddlMachine As System.Web.UI.WebControls.DropDownList
    

 
    Dim strOppCd_OilLoss_GET As String = "PM_CLSTRX_OILLOSS_GET"
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String

    Private Sub InitializeComponent()

    End Sub

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

        lblDupMsg.Visible = False

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMOilLoss), intPMAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()

            If Not Page.IsPostBack Then
                strTransId = Request.QueryString("TransId")
                strEdit = Request.QueryString("Edit")
                lblId.Text = ""
                BindTestSampleCode() 
                BindStation("")
                BindMachine("", "")
               
                If Request.QueryString("TransId") <> "" And Request.QueryString("Edit") <> ""  Then
                     strTransId = Request.QueryString("TransId")
                     lblId.Text = strTransId
                     strEdit = Request.QueryString("Edit")
                End If

                If strEdit = "True" Then
                    DisplayData()
                    
                Else
                    blnUpdate.Text = True
                    txtdate.Enabled = True
                    ddlTestSampleCode.Enabled = True
                    btnSelDateFrom.Visible = True
                    ddlMachine.Enabled = True
                    ddlStation.Enabled = True

                    SetRowVisible(False)
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_OIL_LOSS_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/pm_trx_oilLoss_list.aspx")
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


    Protected Function LoadData() As DataSet
        
        StrParam = Trim(lblId.Text)

        Try
            intErrNo = objPMTrx.mtdGetOilLoss(strOppCd_OilLoss_GET, strLocation, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_oilLoss_GET&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/PM_trx_oilLoss_List.aspx")
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
        Dim I As Integer
        Dim dsOilLoss As DataSet = LoadData()

        If dsOilLoss.Tables(0).Rows.Count > 0 Then
            
            txtdate.Text = objGlobal.GetShortDate(strDateFormat, Trim(dsOilLoss.Tables(0).Rows(0).Item("TransDate")))

            Try
                ddlTestSampleCode.SelectedValue = dsOilLoss.Tables(0).Rows(0).Item("TestSampleCode")
            Catch Exp As System.Exception
                ddlTestSampleCode.SelectedValue = ""
            End Try

            Try
                ddlStation.SelectedValue = dsOilLoss.Tables(0).Rows(0).Item("StationCode")
            Catch Exp As System.Exception
                ddlStation.SelectedValue = ""
            End Try


            lblPeriod.Text = Trim(dsOilLoss.Tables(0).Rows(0).Item("AccMonth")) & "/" & Trim(dsOilLoss.Tables(0).Rows(0).Item("AccYear"))
            lblStatus.Text = objPMTrx.mtdGetOilLossStatus(Trim(dsOilLoss.Tables(0).Rows(0).Item("Status")))
            lblCreateDate.Text = objGlobal.GetLongDate(Trim(dsOilLoss.Tables(0).Rows(0).Item("CreateDate")))
            lblLastUpdate.Text = objGlobal.GetLongDate(Trim(dsOilLoss.Tables(0).Rows(0).Item("UpdateDate")))
            lblUpdateBy.Text = Trim(dsOilLoss.Tables(0).Rows(0).Item("Username"))
                        
            PopulateControls(dsOilLoss)

            DisableControl(Trim(dsOilLoss.Tables(0).Rows(0).Item("Status")))

            Delete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Delete.Visible = True

            blnUpdate.Text = False
            txtdate.Enabled = False
            ddlTestSampleCode.Enabled = False
            btnSelDateFrom.Visible = False
            ddlMachine.Enabled = False
            ddlStation.Enabled = False

        End If
    End Sub
  
    Sub UpdateData(ByVal strAction As String)

        Dim strOppCd_OilLoss_ADD As String = "PM_CLSTRX_OILLOSS_ADD"
        Dim strOppCd_OilLoss_UPD As String = "PM_CLSTRX_OIL_LOSS_UPD"
        Dim strOppCd_Dtl_ADD As String = "PM_CLSTRX_OILLOSS_DETAIL_ADD"
        Dim strOppCd_Dtl_UPD As String = "PM_CLSTRX_OIL_LOSS_DETAIL_UPD"
        Dim strOpCodeDelDtl As String = "PM_CLSTRX_OILLOSS_DETAIL_DEL"

        Dim blnDupKey As Boolean = False
        Dim blnUpd As Boolean = False
        Dim strStatus As String
        Dim strDate As String = CheckDate()
        Dim strGetId As String
        Dim strTrxID As String = "OilLossID" 

        If strDate = "" then
            Exit Sub
        End If
        
        Dim strParam As String
        Dim objDataSet As New DataSet
        Dim dtParamDtl As DataTable = getDetailData()

        strGetId = lblId.Text

        If Trim(strGetId) = "" THEN
            blnUpd = False
        Else
            blnUpd = True
        End If

        strParam =  lblId.Text & "|" & _
                    strDate & "|" & _
                    ddlTestSampleCode.SelectedValue & "|" & _
                    ddlStation.SelectedValue & "|" & _
                    ddlMachine.SelectedValue & "|" &  objPMTrx.EnumOilLossStatus.Active
                    
        Try

            intErrNo = objPMTrx.mtdUpdOilLoss(strOppCd_OilLoss_ADD, _
                                              strOppCd_OilLoss_UPD, _
                                              strOppCd_Dtl_ADD, _
                                              strOppCd_Dtl_UPD, _
                                              strOpCodeDelDtl, _
                                              strLocation, _
                                              strUserId, _
                                              strAccMonth, _
                                              strAccYear, _
                                              strParam, _
                                              dtParamDtl, _
                                              blnUpd, _
                                              blnDupKey, _
                                              strGetId, _
                                              strTrxID)


        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_OIL_LOSS_UPDATE&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/pm_trx_oilLoss_list.aspx")
        End Try
        
        If intErrNo = 5 then
            lblDupMsg.Visible = True
            Exit Sub
        End If

        If blnUpd = False Then lblId.Text = strGetId

        DisplayData()

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
                strValidDate = ""   
            End If
        End If

    End Function

    Sub btnSave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        UpdateData("Save")
    End Sub

    Sub btnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOppCd_DEL As String = "PM_CLSTRX_OILLOSS_DEL"
      
        Dim strParam As String

        strParam = lblId.Text

        Try
            intErrNo = objPMTrx.mtdPhysicalDelete(strOppCd_DEL, _
                                                  strParam, objPMTrx.EnumTransType.OilLoss)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_OIL_LOSS_DEL&errmesg=" & lblErrMessage.Text & "&redirect=PM/Trx/PM_trx_oilLoss_List.aspx")
        End Try

        Response.Redirect("PM_trx_oilLoss_List.aspx")
    End Sub

    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PM_trx_oilLoss_List.aspx")
    End Sub

    Sub btnNew_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
       lblId.Text = ""
       strEdit = "False"
       
       txtdate.Text = ""
       ddlTestSampleCode.SelectedIndex = 0
       ddlStation.SelectedIndex = 0
       ddlMachine.SelectedIndex = 0
       lblPeriod.Text = ""
       lblStatus.Text = ""
       lblCreateDate.Text = ""
       lblLastUpdate.Text = ""
       lblUpdateBy.Text = ""

       blnUpdate.Text = True
       txtdate.Enabled = True
       ddlTestSampleCode.Enabled = True
       btnSelDateFrom.Visible = True
       ddlMachine.Enabled = True
       ddlStation.Enabled = True
       SetRowVisible(False)   
       Delete.Visible = False

    End Sub
    Sub BindTestSampleCode()

        Dim strOpCode = "PM_CLSSETUP_TESTSAMPLE_GET"
        Dim strParam = "ORDER BY M.TestSampleCode| AND M.LocCode='" & Session("SS_LOCATION") & "' AND M.Status ='1'"
        Dim intCnt As Integer

        objPMSetup.mtdGetTestSample(strOpCode, strParam, objTestSampleCode)
        ddlTestSampleCode.Items.Add(New ListItem("Please select Test Sample Code", ""))

        For intCnt = 0 To objTestSampleCode.Tables(0).Rows.Count - 1
            objTestSampleCode.Tables(0).Rows(intCnt).Item("TestSampleCode") = Trim(objTestSampleCode.Tables(0).Rows(intCnt).Item("TestSampleCode"))
            ddlTestSampleCode.Items.Add(New ListItem(objTestSampleCode.Tables(0).Rows(intCnt).Item("TestSampleCode") & " (" & objTestSampleCode.Tables(0).Rows(intCnt).Item("Description") & ")", _
                                        objTestSampleCode.Tables(0).Rows(intCnt).Item("TestSampleCode")))
        Next

    End Sub
    
    Sub BindStation(Byval vstrValue As String)
        Dim strOpCd As String =  "PM_CLSTRX_STATION_GET"
        Dim dsList As DataSet
        Dim intCnt As Integer
        Dim strSearch As String = ""
                
        strSearch = "WHERE Status = '1' AND LocCode = '" &  strLocation & "' AND UsedFor = '" &  objPMSetup.EnumMachineCriteriaFor.OilLoss & "'"
        

        strParam = "|" & strSearch
        Try
            intErrNo = objPMTrx.mtdGetTransactionList(strOpCd, strParam, dsList)
        Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_OIL_LOSS_BINDSTATION&errmesg=" & lblErrMessage.Text & "&redirect=PM/Trx/PM_trx_oilLoss_List.aspx")
        End Try

        ddlStation.Items.Add(New ListItem("Please Select Station", ""))
        
        If dsList.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To dsList.Tables(0).Rows.Count - 1
                ddlStation.Items.Add(New ListItem(dsList.Tables(0).Rows(intCnt).Item("StationDesc"), dsList.Tables(0).Rows(intCnt).Item("Station")))
            Next
        End If

        Try
            ddlStation.SelectedValue = vstrValue
        Catch Exp As System.Exception
            ddlStation.SelectedValue = ""
        End try

    End Sub

    Sub BindMachine(Byval vstrValue As String, Optional Byval vstrStation As String = "")
        Dim strOpCd As String =  "PM_CLSTRX_MACHINE_GET"
        Dim dsList As DataSet
        Dim intCnt As Integer
        Dim strSearch As String = ""

        ddlMachine.items.clear
        ddlMachine.Items.Add(New ListItem("Please Select Machine", ""))

        If vstrStation <> "" Then

            strSearch = "WHERE Status = '1' AND LocCode = '" &  strLocation & "' AND UsedFor = '" &  objPMSetup.EnumMachineCriteriaFor.OilLoss & "'"

            If vstrStation <> "" Then
                strSearch = strSearch & " AND Station = '" & vstrStation & "'"
             End If

            strParam = "|" & strSearch
            Try
                intErrNo = objPMTrx.mtdGetTransactionList(strOpCd, strParam, dsList)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_OIL_LOSS_BINDMACHINE&errmesg=" & lblErrMessage.Text & "&redirect=PM/Trx/PM_trx_oilLoss_List.aspx")
            End Try
        
            If dsList.Tables(0).Rows.Count > 0 Then
                For intCnt = 0 To dsList.Tables(0).Rows.Count - 1
                    ddlMachine.Items.Add(New ListItem(dsList.Tables(0).Rows(intCnt).Item("StationDesc"), dsList.Tables(0).Rows(intCnt).Item("Machine")))
                Next
            End If

            Try
                ddlMachine.SelectedValue = vstrValue
            Catch Exp As System.Exception
                ddlMachine.SelectedValue = ""
            End try

        Else
             ddlMachine.SelectedValue = ""
        End If
    End Sub

    Private Sub PopulateControls(ByVal ds As DataSet)
        Dim intCnt As Integer
        Dim intCount As Integer
        Dim ctrl As Control, rfv As RequiredFieldValidator
        Dim txt As TextBox, lbl As Label
        Dim rev As RegularExpressionValidator, rv As RangeValidator
        Dim hrow As HtmlTableRow
        
        BindMachine(Trim(ds.Tables(0).Rows(0).Item("MachineCode")),  Trim(ds.Tables(0).Rows(0).Item("StationCode")))
        
        SetRowVisible(False)
        For intCnt = 0 To ds.Tables(1).Rows.Count - 1

            intCount = intCnt + 1
            
            Select Case   ds.Tables(1).Rows(intCnt).Item("FieldType")

            Case objPMSetup.EnumFieldType.KeyIn
                    hrow = Page.FindControl("tr" & intCount.ToString())
                    If Not hrow Is Nothing Then
                        hrow.Visible = true
                    End If            

                    lbl = Page.FindControl("lbl" & intCount.ToString())
                    If Not lbl Is Nothing Then
                       lbl.Visible = true
                       lbl.Text = ds.Tables(1).Rows(intCnt).Item("FieldName")
                    End If
                    txt = Page.FindControl("txt" & intCount.ToString())
                    If Not txt Is Nothing Then
                        txt.Visible = True
                        txt.Text = ds.Tables(1).Rows(intCnt).Item("FieldValue")
                    End If
                    lbl = Page.FindControl("lblType" & intCount.ToString())
                    If Not lbl Is Nothing Then
                       lbl.Text = objPMSetup.EnumFieldType.KeyIn
                    End If                    
                    rfv = Page.FindControl("rfvTxt" & intCount.ToString())
                    If Not rfv Is Nothing Then
                        rfv.Enabled = True
                    End If
                    rev = Page.FindControl("revTxt" & intCount.ToString())
                    If Not rev Is Nothing Then
                        rev.Enabled = True
                    End If
                    rv = Page.FindControl("rvTxt" & intCount.ToString())
                    If Not rv Is Nothing Then
                        rv.Enabled = True
                    End If
                

            Case objPMSetup.EnumFieldType.Header
                    hrow = Page.FindControl("tr" & intCount.ToString())
                    If Not hrow Is Nothing Then
                        hrow.Visible = true
                    End If            

                    lbl = Page.FindControl("lbl" & intCount.ToString())
                    If Not lbl Is Nothing Then
                       lbl.Visible = true
                       lbl.Text = ds.Tables(1).Rows(intCnt).Item("FieldName")
                       lbl.Font.Bold = True 
                    End If
                    txt = Page.FindControl("txt" & intCount.ToString())
                    If Not txt Is Nothing Then
                        txt.Visible = False
                    End If
                    lbl = Page.FindControl("lblType" & intCount.ToString())
                    If Not lbl Is Nothing Then
                       lbl.Text = objPMSetup.EnumFieldType.Header
                    End If                    
                    rfv = Page.FindControl("rfvTxt" & intCount.ToString())
                    If Not rfv Is Nothing Then
                        rfv.Enabled = False
                    End If
                    rev = Page.FindControl("revTxt" & intCount.ToString())
                    If Not rev Is Nothing Then
                        rev.Enabled = False
                    End If
                    rv = Page.FindControl("rvTxt" & intCount.ToString())
                    If Not rv Is Nothing Then
                        rv.Enabled = False
                    End If

            End Select
            
        Next intCnt


    End Sub
    
    Private Sub SetRowVisible(ByVal blnVisible As Boolean)
        Dim hrow As HtmlTableRow
        Dim intTotalCltr As Int16 = 25

        For intCount As Int16 = 1 To intTotalCltr
            hrow = Page.FindControl("tr" & intCount.ToString())
            If Not hrow Is Nothing Then
                hrow.Visible = blnVisible
            End If            
        Next
    End Sub

    
    Private Function getDetailData () As DataTable
    
        Dim hrow As HtmlTableRow
        Dim intTotalCltr As Int16 = 25
        Dim ctrl As Control
        Dim lbl As Label, txt As TextBox

        Dim dtTable As DataTable = New DataTable("DetailData")
        Dim dtColumn As DataColumn
        Dim dtRow As DataRow
    
        dtColumn = New DataColumn
        dtColumn.DataType = System.Type.GetType("System.String")
        dtColumn.ColumnName = "FieldName"
        dtTable.Columns.Add(dtColumn)

        dtColumn = New DataColumn
        dtColumn.DataType = System.Type.GetType("System.String")
        dtColumn.ColumnName = "FieldType"
        dtTable.Columns.Add(dtColumn)

        dtColumn = New DataColumn
        dtColumn.DataType = System.Type.GetType("System.Double")
        dtColumn.ColumnName = "FieldValue"
        dtTable.Columns.Add(dtColumn)

        For intCount As Int16 = 1 To intTotalCltr
            hrow = Page.FindControl("tr" & intCount.ToString())
            If Not hrow Is Nothing Then
                If hrow.Visible = True then
                    dtRow = dtTable.NewRow()
                    lbl = Page.FindControl("lbl" & intCount.ToString())
                    If Not lbl Is Nothing Then
                        dtRow("FieldName") = lbl.Text
                    End If
                    txt = Page.FindControl("txt" & intCount.ToString())
                    If Not txt Is Nothing Then
                         dtRow("FieldValue") = IIf(txt.Text = "", 0, txt.Text)
                    End If
                    lbl = Page.FindControl("lblType" & intCount.ToString())
                    If Not lbl Is Nothing Then
                       dtRow("FieldType") =  lbl.Text
                    End If               
                    dtTable.Rows.Add(dtRow)
                Else
                    Exit For
                End If
            End If            
        Next

        Return dtTable
    End Function

    Protected Sub ddlStation_SelectedIndexChanged(ByVal Sender As Object, ByVal E As System.EventArgs)
           BindMachine("", Sender.SelectedValue)
           SetRowVisible(False)

    End Sub

    Protected Sub ddlMachine_SelectedIndexChanged(ByVal Sender As Object, ByVal E As System.EventArgs)
           SetRowVisible(False)
           PopulateTemplControls(Sender.SelectedValue)
        
    End Sub

    Private Sub PopulateTemplControls(ByVal vstrMachine As String) 
 
           Dim strOpCd As String = "PM_CLSTRX_MACHINE_TEMPLATE_GET"
           Dim strSearch As String
           Dim dsList As DataSet
           Dim intCnt As Integer
           Dim intCount As Integer
           Dim lbl As Label, txt As TextBox
           Dim ctrl As Control, rfv As RequiredFieldValidator
           Dim rev As RegularExpressionValidator, rv As RangeValidator
           Dim hrow As HtmlTableRow
            
            strSearch = "WHERE Status = '1' AND LocCode = '" &  strLocation & "' AND UsedFor = '" &  objPMSetup.EnumMachineCriteriaFor.OilLoss & "'"
            strSearch = strSearch & " AND Machine = '" & vstrMachine & "'"
           
            strParam = "|" & strSearch
            Try
                intErrNo = objPMTrx.mtdGetTransactionList(strOpCd, strParam, dsList)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_OIL_LOSS_MACHINETEMPLATE_GET&errmesg=" & lblErrMessage.Text & "&redirect=PM/Trx/PM_trx_oilLoss_List.aspx")
            End Try
        
            
            For intCnt = 0 To dsList.Tables(0).Rows.Count - 1

            intCount = intCnt + 1
            
            Select Case   dsList.Tables(0).Rows(intCnt).Item("FieldType")

            Case objPMSetup.EnumFieldType.KeyIn
                    hrow = Page.FindControl("tr" & intCount.ToString())
                    If Not hrow Is Nothing Then
                        hrow.Visible = true
                    End If            

                    lbl = Page.FindControl("lbl" & intCount.ToString())
                    If Not lbl Is Nothing Then
                       lbl.Visible = true
                       lbl.Text = dsList.Tables(0).Rows(intCnt).Item("FieldName")
                       lbl.Font.Bold = False
                    End If
                    txt = Page.FindControl("txt" & intCount.ToString())
                    If Not txt Is Nothing Then
                        txt.Visible = True
                        txt.Text = "0"
                    End If
                    lbl = Page.FindControl("lblType" & intCount.ToString())
                    If Not lbl Is Nothing Then
                       lbl.Text = objPMSetup.EnumFieldType.KeyIn
                    End If                    
                    rfv = Page.FindControl("rfvTxt" & intCount.ToString())
                    If Not rfv Is Nothing Then
                        rfv.Enabled = True
                    End If
                    rev = Page.FindControl("revTxt" & intCount.ToString())
                    If Not rev Is Nothing Then
                        rev.Enabled = True
                    End If
                    rv = Page.FindControl("rvTxt" & intCount.ToString())
                    If Not rv Is Nothing Then
                        rv.Enabled = True
                    End If
                

            Case objPMSetup.EnumFieldType.Header
                    hrow = Page.FindControl("tr" & intCount.ToString())
                    If Not hrow Is Nothing Then
                        hrow.Visible = true
                    End If            

                    lbl = Page.FindControl("lbl" & intCount.ToString())
                    If Not lbl Is Nothing Then
                       lbl.Visible = true
                       lbl.Text = dsList.Tables(0).Rows(intCnt).Item("FieldName")
                       lbl.Font.Bold = True 
                    End If
                    txt = Page.FindControl("txt" & intCount.ToString())
                    If Not txt Is Nothing Then
                        txt.Visible = False
                    End If
                    lbl = Page.FindControl("lblType" & intCount.ToString())
                    If Not lbl Is Nothing Then
                       lbl.Text = objPMSetup.EnumFieldType.Header
                    End If                    
                    rfv = Page.FindControl("rfvTxt" & intCount.ToString())
                    If Not rfv Is Nothing Then
                        rfv.Enabled = False
                    End If
                    rev = Page.FindControl("revTxt" & intCount.ToString())
                    If Not rev Is Nothing Then
                        rev.Enabled = False
                    End If
                    rv = Page.FindControl("rvTxt" & intCount.ToString())
                    If Not rv Is Nothing Then
                        rv.Enabled = False
                    End If

            End Select
            
        Next intCnt

    End Sub

End Class
