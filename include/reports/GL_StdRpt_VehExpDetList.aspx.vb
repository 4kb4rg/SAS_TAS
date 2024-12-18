Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports System.Xml
Imports System.Web.Services

Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.GL
Imports agri.PWSystem.clsConfig
Imports agri.Admin.clsShare

Public Class GL_StdRpt_VehExpDetList : Inherits Page

    Protected RptSelect As UserControl

    Protected objGL As New agri.GL.clsReport()
    Protected objGLSetup As New agri.GL.clsSetup()
    Protected objGLTrx As New agri.GL.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objSysCfg As New agri.PWSystem.clsConfig()
    Protected objAdmin As New agri.Admin.clsShare()

    Protected WithEvents hidUserLocPX As HtmlInputText
    Protected WithEvents hidAccMonthPX As HtmlInputText
    Protected WithEvents hidAccYearPX As HtmlInputText
    Protected WithEvents hidJournalID As HtmlInputText

    Protected WithEvents lblDate As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker as Label

    Protected WithEvents lstJournalIDFrom As DropDownList
    Protected WithEvents lstJournalIDTo As DropDownList
    Protected WithEvents lstRefNo As DropDownList
    Protected WithEvents lstTransType as DropDownList
    Protected WithEvents lstUpdatedBy As DropDownList
    Protected WithEvents lstStatus As DropDownList
    Protected WithEvents lstAccCode As DropDownList
    Protected WithEvents lstVehCode As DropDownList
    Protected WithEvents lstVehExpCode As DropDownList
    Protected WithEvents lstBlkCode As DropDownList
    Protected WithEvents lstSubBlkCode As DropDownList

    Protected WithEvents dsForJournalIDFromDropDown as DataSet
    Protected WithEvents dsForJournalIDToDropDown as DataSet
    Protected WithEvents dsForRefNoDropDown As DataSet
    Protected WithEvents dsForTransTypeDropDown As DataSet
    Protected WithEvents dsForUpdatedByDropDown As DataSet
    Protected WithEvents dsForAccCodeDropDown As DataSet
    Protected WithEvents dsForVehCodeDropDown As DataSet
    Protected WithEvents dsForVehExpCodeDropDown As DataSet
    Protected WithEvents dsForBlkCodeDropDown As DataSet
    Protected WithEvents dsForSubBlkCodeDropDown As DataSet

    Protected WithEvents lblBlkCode As Label
    Protected WithEvents lblSubBlkCode As Label

    Protected WithEvents PrintPrev as ImageButton

    Dim objAR As New agri.GlobalHdl.clsAccessRights()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfigsetting As Integer
    Dim strUserLoc as String

    Dim dr As DataRow
    Dim intCnt As Integer
    Dim intErrNo As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        Dim tempDateFrom as Textbox
        Dim tempDateTo as Textbox
        Dim intCnt as Integer

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        
        lblDate.visible = false
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        else
            if not Request.QueryString("DateFrom") = "" then
                tempDateFrom = RptSelect.FindControl("txtDateFrom")
                tempDateFrom.text = Request.QueryString("DateFrom")
            end if
        
            if not Request.QueryString("DateTo") = "" then
                tempDateTo = RptSelect.FindControl("txtDateTo")
                tempDateTo.text = Request.QueryString("DateTo")
            end if

            If Not Page.IsPostBack Then
                hidUserLocPX.value = Request.QueryString("UserLoc")
                BindStatus()
                BindVehCodeList()
                BindVehExpCodeList()

                If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
                    lblBlkCode.Visible = True
                    lstBlkCode.Visible = true    
                else
                    lblSubBlkCode.Visible = true
                    lstSubBlkCode.Visible = true    
                end if

            end if            
       end if
    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        If Not Page.IsPostBack Then
        else 
            trace.warn("postback")
            BindVehCodeList()
            BindVehExpCodeList()
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
                lblBlkCode.Visible = True
                lstBlkCode.Visible = true    
            else
                lblSubBlkCode.Visible = true
                lstSubBlkCode.Visible = true    
            end if
        End If
    End Sub


    Sub BindVehCodeList()
        'Dim strParam As String
        'Dim strOppCd_Journal_VehCode_GET = "GL_STDRPT_JOURNALLN_VEHCODE_GET"
        'Dim strUserLoc as String

        'If Left(hidUserLocPX.value, 3) = "','" Then
        '    strUserLoc = Right(hidUserLocPX.value, Len(hidUserLocPX.value) - 3)
        'Else If Right(hidUserLocPX.value, 1) = "," Then
        '    strUserLoc = Left(hidUserLocPX.value, Len(hidUserLocPX.value) - 1)
        'End If

        ''Protected objGLSetup As New agri.GL.clsSetup()
        ''Protected objGLTrx As New agri.GL.ClsTrx()
        'strParam = strUserLoc & "|"
        'Try
        '    intErrNo = objGLSetup.strOppCd_Journal_VehCode_GET, _
        '                                   strParam, _
        '                                   dsForVehCodeDropDown)
        'Catch Exp As System.Exception
        '    Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_JOURNAL_VEHCODE_LIST&errmesg=" & lblErrMessage.Text & "&redirect=../../en/reports/GL_StdRpt_JournalList.aspx")
        'End Try

        'For intCnt = 0 To dsForVehCodeDropDown.Tables(0).Rows.Count - 1
        '    dsForVehCodeDropDown.Tables(0).Rows(intCnt).Item("VehCode") = Trim(dsForVehCodeDropDown.Tables(0).Rows(intCnt).Item("VehCode"))
        '    dsForVehCodeDropDown.Tables(0).Rows(intCnt).Item("Description") = Trim(dsForVehCodeDropDown.Tables(0).Rows(intCnt).Item("VehCode"))
        'Next intCnt

        'dr = dsForVehCodeDropDown.Tables(0).NewRow()
        'dr("VehCode") = ""
        'dr("Description") = "All"
        'dsForVehCodeDropDown.Tables(0).Rows.InsertAt(dr, 0)

        'lstVehCode.DataSource = dsForVehCodeDropDown.Tables(0)
        'lstVehCode.DataValueField = "VehCode"
        'lstVehCode.DataTextField = "Description"
        'lstVehCode.DataBind()
    End Sub

    Sub BindVehExpCodeList()
        'Dim strParam As String
        'Dim strOppCd_Journal_VehExpCode_GET = "GL_STDRPT_JOURNALLN_VEHEXPCODE_GET"
        'Dim strUserLoc as String

        'If Left(hidUserLocPX.value, 3) = "','" Then
        '    strUserLoc = Right(hidUserLocPX.value, Len(hidUserLocPX.value) - 3)
        'Else If Right(hidUserLocPX.value, 1) = "," Then
        '    strUserLoc = Left(hidUserLocPX.value, Len(hidUserLocPX.value) - 1)
        'End If

        'strParam = strUserLoc & "|"
        'Try
        '    intErrNo = objGL.mtdGetJournal(strOppCd_Journal_VehExpCode_GET, _
        '                                   strParam, _
        '                                   dsForVehExpCodeDropDown)
        'Catch Exp As System.Exception
        '    Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_JOURNAL_VEHEXPCODE_LIST&errmesg=" & lblErrMessage.Text & "&redirect=../../en/reports/GL_StdRpt_JournalList.aspx")
        'End Try

        'For intCnt = 0 To dsForVehExpCodeDropDown.Tables(0).Rows.Count - 1
        '    dsForVehExpCodeDropDown.Tables(0).Rows(intCnt).Item("VehExpCode") = Trim(dsForVehExpCodeDropDown.Tables(0).Rows(intCnt).Item("VehExpCode"))
        '    dsForVehExpCodeDropDown.Tables(0).Rows(intCnt).Item("Description") = Trim(dsForVehExpCodeDropDown.Tables(0).Rows(intCnt).Item("VehExpCode"))
        'Next intCnt

        'dr = dsForVehExpCodeDropDown.Tables(0).NewRow()
        'dr("VehExpCode") = ""
        'dr("Description") = "All"
        'dsForVehExpCodeDropDown.Tables(0).Rows.InsertAt(dr, 0)

        'lstVehExpCode.DataSource = dsForVehExpCodeDropDown.Tables(0)
        'lstVehExpCode.DataValueField = "VehExpCode"
        'lstVehExpCode.DataTextField = "Description"
        'lstVehExpCode.DataBind()
    End Sub

    Sub BindStatus()

        lstStatus.Items.Add(New ListItem(objGLTrx.mtdGetJournalStatus(objGLTrx.EnumJournalStatus.All), objGLTrx.EnumJournalStatus.All))      
        lstStatus.Items.Add(New ListItem(objGLTrx.mtdGetJournalStatus(objGLTrx.EnumJournalStatus.Active), objGLTrx.EnumJournalStatus.Active))
        lstStatus.Items.Add(New ListItem(objGLTrx.mtdGetJournalStatus(objGLTrx.EnumJournalStatus.Cancelled), objGLTrx.EnumJournalStatus.Cancelled))
        lstStatus.Items.Add(New ListItem(objGLTrx.mtdGetJournalStatus(objGLTrx.EnumJournalStatus.Posted), objGLTrx.EnumJournalStatus.Posted))

        lstStatus.SelectedIndex = 1    

    End Sub


    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strStatus As String
        Dim strVehCode As String
        Dim strVehExpCode As String

        Dim strDateFrom As String
        Dim strDateTo As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strJournalIDFrom As String
        Dim strJournalIDTo As String
        Dim strRefNo As String
        Dim strTransType As String
        Dim strAccCode As String
        Dim strBlkCode As String
        Dim strSubBlkCode As String
        Dim strUpdatedBy As String

        Dim tempDateFrom As TextBox
        Dim tempDateTo As TextBox
        Dim tempRptName As DropDownList

        Dim strParam as String
        Dim strDateSetting As String
        Dim objSysCfgDs as New Object()
        Dim objDateFormat as New Object()
        Dim objDateFrom as String
        Dim objDateTo as String

        tempDateFrom = RptSelect.FindControl("txtDateFrom")
        strDateFrom = Trim(tempDateFrom.Text)
        tempDateTo = RptSelect.FindControl("txtDateTo")
        strDateTo = trim(tempDateTo.Text)
        tempRptName = RptSelect.FindControl("lstRptName")
        strRptName = trim(tempRptName.SelectedItem.Value)
        strUserLoc = trim(hidUserLocPX.value)

        If Left(strUserLoc, 3) = "','" Then
            strUserLoc = Right(strUserLoc, Len(strUserLoc) - 3)
        else if Right(strUserLoc, 3) = "','" Then
            strUserLoc = Left(strUserLoc, Len(strUserLoc) - 3)
        End If

        if lstJournalIDFrom.SelectedIndex = 0 then
            strJournalIDFrom = ""
        else
            strJournalIDFrom = Trim(lstJournalIDFrom.SelectedItem.Value)
        end if

        If lstJournalIDTo.SelectedIndex = -1 Then
            strJournalIDTo = ""
        Else
            strJournalIDTo = Trim(lstJournalIDTo.SelectedItem.Value)
        End If

        if lstRefNo.SelectedIndex = 0 then
            strRefNo = ""
        else
            strRefNo = Trim(lstRefNo.SelectedItem.Value)
        end if

        strTransType = Trim(lstTransType.SelectedItem.Value)
        strStatus = Trim(lstStatus.SelectedItem.Value)

        If lstAccCode.SelectedIndex = 0 Then
            strAccCode = ""
        Else
            strAccCode = Trim(lstAccCode.SelectedItem.Value)
        End If

        If lstVehCode.SelectedIndex = 0 Then
            strVehCode = ""
        Else
            strVehCode = Trim(lstVehCode.SelectedItem.Value)
        End If

        If lstVehExpCode.SelectedIndex = -1 Then
            strVehCode = ""
        Else
            strVehExpCode = Trim(lstVehExpCode.SelectedItem.Value)
        End If

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
            If lstBlkCode.SelectedIndex = 0 Then
                strVehCode = ""
            Else
                strBlkCode = Trim(lstBlkCode.SelectedItem.Value)
            End If
        else
            If lstSubBlkCode.SelectedIndex = 0 Then
                strSubBlkCode = ""
            Else
                strSubBlkCode = Trim(lstSubBlkCode.SelectedItem.Value)
            End If
        end if


        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"
        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_STDRPT_JOURNAL_GET_CONFIG_DATE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        strDateSetting = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))

        if not (strDateFrom = "" and strDateTo = "") then
            If objGlobal.mtdValidInputDate(strDateSetting, strDateFrom, objDateFormat, objDateFrom) = True and objGlobal.mtdValidInputDate(strDateSetting, strDateTo, objDateFormat, objDateTo) = True Then
                Response.Write("<Script Language=""JavaScript"">window.open(""GL_StdRpt_JournalListPreview.aspx?Type=Print&CompName=" & strCompany & "&Location=" & strUserLoc & "&AccMonth=" & hidAccMonthPX.Value & "&AccYear=" & _
                               hidAccYearPX.Value & "&RptName=" & strRptName & "&DateFrom=" & objDateFrom & "&DateTo=" & objDateTo & "&JournalIDFrom=" & strJournalIDFrom & "&JournalIDTo=" & strJournalIDTo & "&RefNo=" & strRefNo & "&TransType=" & _
                               strTransType & "&UpdatedBy=" & strUpdatedBy & "&Status=" & strStatus & "&AccCode=" & strAccCode & "&VehCode=" & strVehCode & "&VehExpCode=" & strVehExpCode & "&BlkCode=" & strBlkCode & "&SubBlkCode=" & _ 
                               strSubBlkCode & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
            Else
                lblDate.text = lblDate.text & objDateFormat & "."       
                lblDate.visible = true
            End If
        else
            Response.Write("<Script Language=""JavaScript"">window.open(""GL_StdRpt_JournalListPreview.aspx?Type=Print&CompName=" & strCompany & "&Location=" & strUserLoc & "&AccMonth=" & hidAccMonthPX.Value & "&AccYear=" & _
                            hidAccYearPX.Value & "&RptName=" & strRptName & "&DateFrom=" & objDateFrom & "&DateTo=" & objDateTo & "&JournalIDFrom=" & strJournalIDFrom & "&JournalIDTo=" & strJournalIDTo & "&RefNo=" & strRefNo & "&TransType=" & _
                            strTransType & "&UpdatedBy=" & strUpdatedBy & "&Status=" & strStatus & "&AccCode=" & strAccCode & "&VehCode=" & strVehCode & "&VehExpCode=" & strVehExpCode & "&BlkCode=" & strBlkCode & "&SubBlkCode=" & _ 
                            strSubBlkCode & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
        end if
    End Sub

End Class
