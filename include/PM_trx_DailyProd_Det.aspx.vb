

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


Public Class PM_DailyProd_Det : Inherits Page

    Dim dsTicketItem As DataSet

    Dim objPMTrx As New agri.PM.clsTrx()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objOk As New agri.GL.ClsTrx()
     

    Dim strProdDate As String

'----- #3 Start -----    
    Dim dbUnit As Double = 1000
'----- #3 End   -----

    '---#4 s
   
    '---#4 e

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
    Dim strAcceptFormat As String
    Dim strOppCd_Prod_GET As String = "PM_CLSTRX_DAILYPROD_GET"
    Dim strOpCd_Bunches_Get As String = "PM_CLSTRX_BACKLOGBUNCHES_GET"
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
        'add by alim
        strLocType = Session("SS_LOCTYPE")
        'End of Add by alim
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMDailyProduction), intPMAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()

            If Not Page.IsPostBack Then
                If Not Request.QueryString("Date") = "" Then
                    strProdDate = Request.QueryString("Date")
                    ViewState.Item("DATE") = Request.QueryString("Date")
                Else
                   
                End If

                '----- Determine if action is Edit or Add new item ----------
                If Not strProdDate = "" Then
                    DisplayData()
                    '----- #1 -----
                    Delete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    '----- #1 -----
                    blnUpdate.Text = True

                    'DisableControl()
                Else

                    DisplayData()
                    blnUpdate.Text = False
                    'Fill Default Value 
                    lblErrStartTime.Visible = False
                    lblErrEndTime.Visible = False
                    lblErrBrokTime.Visible = False
                    lblEndtDate.Visible = False
                End If
            End If
        End If

    End Sub

    '=== For Language Caption==================================================
    Sub onload_GetLangCap()
        'GetEntireLangCap()
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_DAILYPROD_DET_GET_LANGCAP&errmesg=" & Exp.ToString() & "&redirect=PM/trx/PM_trx_DailyProd_Det.aspx")
        End Try

    End Sub

    'add by alim
    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

        For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If strLocType = objAdminLoc.EnumLocType.Mill Then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                Else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                End If
                'Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                Exit For
            End If
        Next
    End Function
    'End of Add by alim

    '=====End for Language Caption ===============================================


    '-------Calls the com to create a dataset  --------------------
    Protected Function LoadData() As DataSet

        strParam = "||TransDate||" & strProdDate & "|"

        Try
            '------------ USING COMPONENTS -----------------
            intErrNo = objPMTrx.mtdGetDailyProd(strOppCd_Prod_GET, _
                                                strLocation, _
                                                strUserId, _
                                                strAccMonth, _
                                                strAccYear, _
                                                strParam, _
                                                objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_DAILYPROD_GET&errmesg=" & Exp.ToString() & "&redirect=PM/trx/PM_trx_DailyProd_List.aspx")
        End Try

        Return objDataSet
    End Function


    '----- Disable Control if Item is Deleted ----------
    Sub DisableControl(ByVal pv_strstatus)
        Dim strView As Boolean

        If pv_strstatus = objPMTrx.EnumDailyProdStatus.Deleted Then
            strView = False
            'Delete.ImageUrl = "../../images/butt_Undelete.gif"

        ElseIf pv_strstatus = objPMTrx.EnumDailyProdStatus.Active Then
            strView = True
            'Delete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
        End If

        'txtdate.Enabled = strView
 
        txtStartHour.Enabled = strView
        txtStartMnt.Enabled = strView

        txtStartDate.Enabled = strView
        txtEndHour.Enabled = strView
        txtEndMnt.Enabled = strView
        txtEndDate.Enabled = strView
   

        txtBrokHour.Enabled = strView
        txtBrokMnt.Enabled = strView

         
        Save.Visible = strView
    End Sub

    '----- Display all Item To the Page ----------
    Sub DisplayData()
        Dim objProd As New DataSet()
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim strDate As String = Date_Validation(txtdate.Text, False)
        Dim dsProdItem As DataSet = LoadData()
        Dim dblConvert As Double

        'If dsProdItem.Tables(0).Rows.Count > 0 Then
        '    'txtdate.Text = objGlobal.GetShortDate(strDateFormat, dsProdItem.Tables(0).Rows(0).Item("TransDate"))
        '    txtdate.Text = Date_Validation(dsProdItem.Tables(0).Rows(0).Item("TransDate"), True)
        '    txtStartDate.Text = Date_Validation(dsProdItem.Tables(0).Rows(0).Item("ProcessStart"), True)
        '    txtStartHour.Text = Format(Hour(dsProdItem.Tables(0).Rows(0).Item("ProcessStart")), "00")
        '    txtStartMnt.Text = Format(Minute(dsProdItem.Tables(0).Rows(0).Item("ProcessStart")), "00")
        '    txtEndDate.Text = Date_Validation(dsProdItem.Tables(0).Rows(0).Item("ProcessEnd"), True)
        '    If dsProdItem.Tables(0).Rows(0).Item("ProcessEnd") = "1/1/1900" Then
        '        txtEndHour.Text = ""
        '        txtEndMnt.Text = ""
        '    Else
        '        txtEndHour.Text = Format(Hour(dsProdItem.Tables(0).Rows(0).Item("ProcessEnd")), "00")
        '        txtEndMnt.Text = Format(Minute(dsProdItem.Tables(0).Rows(0).Item("ProcessEnd")), "00")
        '    End If

        'End If

        Dim strOppCode_Get As String = "WM_CLSTRX_PRODUCTION_DAILY_DETAIL_GET"
        Dim intErrNo As Integer

        strParamName = "TGL|LOC"
        strParamValue = "2022-07-01" & "|" & strLocation

        Try
            intErrNo = objOk.mtdGetDataCommon(strOppCode_Get, strParamName, strParamValue, objProd)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=wm_ticket&errmesg=" & Exp.Message & "&redirect=")
        End Try

        dgStorageCPO.DataSource = New DataView(objProd.Tables(0))
        dgStorageCPO.Dispose()
        dgStorageCPO.DataBind()

    End Sub

     

    '----- Update Data from all controls to Database----------
    Sub UpdateData(ByVal strAction As String)
        Dim strOppCd_DailyProduction_ADD As String = "PM_CLSTRX_DAILYPROD_ADD"
        Dim strOppCd_DailyProduction_UPD As String = "PM_CLSTRX_DAILYPROD_UPD"
        Dim blnDupKey As Integer
        Dim strStatus As String
        'Dim strDate As String = CheckDate()
        Dim strDate As String = Date_Validation(txtdate.Text, False)
        Dim strStartDate As String = Date_Validation(txtStartDate.Text, False)
        Dim strEndDate As String = Date_Validation(txtEndDate.Text, False)
        Dim strEndHour As String = txtEndHour.Text
        Dim strEndMnt As String = txtEndMnt.Text
        Dim strStartHour As String = txtStartHour.Text
        Dim strStartMnt As String = txtStartMnt.Text
        Dim strDuration As String = "0"
        Dim strFStartDate As String
        Dim strFEndDate As String
         
            

    End Sub

    Function blnValidEndStartDate(Byval pv_strEndDate As String, Byval pv_strStartDate as string) As Boolean
            blnValidEndStartDate = False
        If CDate(pv_strStartDate) < CDate(pv_strEndDate) Then
            blnValidEndStartDate = True
        End If
    End Function



    Protected Function CheckDate() As String

        Dim strDateSetting As String
        Dim objSysCfgDs As DataSet
        Dim objDateFormat As String
        Dim strValidDate As String
        'Check User Input Date
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

    'Private Sub btnCalc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCalc.Click
    '    Dim strStartDate As String = Date_Validation(txtStartDate.Text, False)
    '    Dim strEndDate As String = Date_Validation(txtEndDate.Text, False)
    '    Dim strEndHour As String = txtEndHour.Text
    '    Dim strEndMnt As String = txtEndMnt.Text
    '    Dim strStartHour As String = txtStartHour.Text
    '    Dim strStartMnt As String = txtStartMnt.Text
    '    Dim strDuration As String =  "0"
    '    Dim strFStartDate As String
    '    Dim strFEndDate As String

    '    if Trim(strStartHour) = "" then strStartHour = "00"
    '    if Trim(strStartMnt) = "" then strStartMnt = "00"
    '    if Trim(strEndHour) = "" then strEndHour = "00"
    '    if Trim(strEndMnt) = "" then strEndMnt = "00"

    '    '#5s
    '    If Not(strStartHour = "00" and strStartMnt = "00" and strEndHour = "00" and strEndMnt = "00") Then        
    '        if Trim(strEndDate) = "" then 
    '            strEndDate = "1900/1/1"
    '            strEndHour = "00"
    '            strEndMnt = "00"
    '            strDuration = "0"
    '        end if
    '        if Trim(strStartDate) = "" then 
    '            strStartDate = "1900/1/1"
    '            strStartHour = "00"
    '            strStartMnt = "00"
    '            strDuration = "0"
    '        end if

    '        strFStartDate = strStartDate & " " & strStartHour & ":" & strStartMnt & ":00" 
    '        strFEndDate = strEndDate & " " & strEndHour & ":" & strEndMnt & ":00"

    '        if Trim(strEndDate) <>  "1900/1/1" then 
    '                'validate date range
    '                If blnValidEndStartDate(Trim(strFEndDate), Trim(strFStartDate)) = False
    '                    lblEndtDate.Text = "Ending Date must greater then Starting Date."
    '                    lblEndtDate.Visible = True
    '                    Exit Sub
    '                End if 

    '                strDuration = CalcDuration(strFStartDate, strFEndDate)

    '                If Val(strDuration) < 0 then
    '                    lblErrBrokTime.Visible = True
    '                    Exit Sub
    '                else
    '                    txtDuration.Text = strDuration
    '                End If                    
    '        end if
    '    Else
    '        txtDuration.Text = 0
    '        txtCPOProduced.Text = 0
    '        txtPKProduced.Text = 0
    '    End If        
    '    '#5e
    'End Sub          

    Function CalcDuration(Byval vstrStartDt As String, ByVal vstrEndDate As String) As String
        Dim intHour As Integer
        Dim dblHour As Double
        Dim dblMinute As Double
        Dim dblBrok As Double

        dblMinute = DateDiff(DateInterval.Minute, CDate(vstrStartDt), CDate(vstrEndDate))
        intHour = dblMinute \ 60
        dblHour = (dblMinute - (intHour * 60)) / 60
        CalcDuration = dblHour + intHour
      
        'minus broken hour
        dblBrok = Val(txtBrokHour.Text) + (Val(txtBrokMnt.Text) / 60)

        CalcDuration = CalcDuration - dblBrok
        

    End Function    

    Function Date_Validation(ByVal pv_strInputDate As String, ByVal pv_blnIsShortDate As Boolean) As String
        Dim objSysCfgDs As New Object
        Dim objActualDate As New Object
        Dim strDateFormat As String
        Dim strParam As String
        Dim intErrNo As Integer

        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"

        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CB_DEPOSIT_GET_CONFIG&errmesg=" & Exp.Message & "&redirect=CB/trx/cb_trx_DepositList.aspx")
        End Try

        strDateFormat = objSysCfg.mtdGetDateFormat(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt").Trim())

        If pv_blnIsShortDate Then
            Date_Validation = objGlobal.GetShortDate(strDateFormat, pv_strInputDate)
        Else
            If objGlobal.mtdValidInputDate(strDateFormat, _
                                           pv_strInputDate, _
                                           strAcceptFormat, _
                                           objActualDate) = True Then
                Date_Validation = objActualDate
            Else
                Date_Validation = ""
            End If
        End If
    End Function


    '----- Save Button Click Event----------
    Sub btnSave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        '---#4 s
        Dim strEndHour As String = txtEndHour.Text
        Dim strEndMnt As String = txtEndMnt.Text
        Dim strStartHour As String = txtStartHour.Text
        Dim strStartMnt As String = txtStartMnt.Text
        
        If strStartHour = "" Or strStartMnt = "" 
                lblErrStartTime.Visible = True
                Exit sub
        Else
                lblErrStartTime.Visible = False
        End If
        If strEndHour = "" Or strEndMnt = "" 
                lblErrEndTime.Visible = True
                Exit sub    
        Else
                lblErrEndTime.Visible = False
        End IF
        '---#4 s
        Response.Write("masuk")
        UpdateData("Save")            
    End Sub
     
    '----- Delete Button Click Event----------
    Sub btnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOppCd_DEL As String = "PM_CLSTRX_DAILYPROD_DEL"
        Dim strDate As String = CheckDate()
        Dim strParam As String
        
        strParam = strLocation & "|" & strDate
        Try
            intErrNo = objPMTrx.mtdPhysicalDelete(strOppCd_DEL, _
                                                  strParam, _
                                                  objPMTrx.EnumTransType.DailyProduction)
        
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_DAILYPROD_DEL&errmesg=" & Exp.ToString() & "&redirect=PM/Trx/PM_trx_DailyProd_List.aspx")
        End Try
        
        Response.Redirect("PM_trx_DailyProd_List.aspx")
    End Sub

    '----- Save Back Click Event----------
    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PM_trx_DailyProd_List.aspx")
    End Sub

End Class
