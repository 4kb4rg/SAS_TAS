
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

Public Class PM_StdRpt_OilLossTransactionList : Inherits Page

    Protected RptSelect As UserControl

    Dim objPM As New agri.PM.clsReport()
    Dim objPMSetup As New agri.PM.clsSetup()
    Dim objPMTrx As New agri.PM.clsTrx()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Protected WithEvents hidUserLocPX As HtmlInputHidden
    Protected WithEvents hidAccMonthPX As HtmlInputHidden
    Protected WithEvents hidAccYearPX As HtmlInputHidden

    Protected WithEvents lblDate As Label
    Protected WithEvents lblDateFormat As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label

    Protected WithEvents lblLocation As Label
    Protected WithEvents lstStatus As DropDownList
    Protected WithEvents ddlTestSampleCode As DropDownList 
    Protected WithEvents ddlMachine As DropDownList
    Protected WithEvents ddlMachineTo As DropDownList

    Protected WithEvents PrintPrev As ImageButton
    Protected WithEvents txtDateFrom As TextBox
    Protected WithEvents txtDateTo As TextBox


    Dim objLangCapDs As New Object()
    Dim objTestSample As New Object() 
    Dim objMachine As New Object()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strLblLocation As String

    Dim intErrNo As Integer
	
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String
	
    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
		
	        strLocType = Session("SS_LOCTYPE")

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strLangCode = Session("SS_LANGCODE")

        lblDate.Visible = False
        lblDateFormat.Visible = False


        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else

            If Not Page.IsPostBack Then
                onload_GetLangCap()
                BindTestSampleList() 
                BindMachineList()
            End If
        End If
    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim ucTrMthYr As HtmlTableRow

        ucTrMthYr = RptSelect.FindControl("TrMthYr")
        ucTrMthYr.Visible = True



    End Sub


    Sub BindTestSampleList()

        Dim strOpCode = "PM_CLSSETUP_TESTSAMPLE_GET"
        Dim strParam = "|"
        Dim intCnt As Integer

        objPMSetup.mtdGetTestSample(strOpCode, strParam, objTestSample)

        ddlTestSampleCode.Items.Add(New ListItem("All"))

        For intCnt = 0 To objTestSample.Tables(0).Rows.Count - 1
            objTestSample.Tables(0).Rows(intCnt).Item("TestSampleCode") = Trim(objTestSample.Tables(0).Rows(intCnt).Item("TestSampleCode"))
            ddlTestSampleCode.Items.Add(New ListItem(objTestSample.Tables(0).Rows(intCnt).Item("TestSampleCode")))
        Next

    End Sub

    Sub BindMachineList()

        Dim strOpCode = "PM_CLSSETUP_MACHINECRITERIA_MACHINE_GET"
        Dim strParam = "|WHERE Status = '1' AND ProcessCtrl = '1' AND SubBlkCode In (SELECT Machine FROM PM_MACHINE_CRITERIA WHERE UsedFor = '" & objPMSetup.EnumMachineCriteriaFor.OilLoss & "' GROUP BY Machine) "
        Dim intCnt As Integer
        Dim strMachineCodeDesc As String

        objPMSetup.mtdGetProcessingLine(strOpCode, strParam, objMachine)


        For intCnt = 0 To objMachine.Tables(0).Rows.Count - 1
            ddlMachine.Items.Add(New ListItem(objMachine.Tables(0).Rows(intCnt).Item("_Descr")))
        Next
        For intCnt = 0 To objMachine.Tables(0).Rows.Count - 1
            ddlMachineTo.Items.Add(New ListItem(objMachine.Tables(0).Rows(intCnt).Item("_Descr")))
        Next
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblLocation.Text = GetCaption(objLangCap.EnumLangCap.Location)
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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PWSYSTEM_CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/IN_StdRpt_Selection.aspx")
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


    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strPRNoFrom As String
        Dim strPRNoTo As String
        Dim strPRType As String
        Dim strStatus As String

        Dim strDateFrom As String
        Dim strDateTo As String
        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String
        Dim strAccMonth As String
        Dim strAccYear As String
        Dim strTestSampleCode As String
        Dim strMachine As String
        

        Dim tempAccMonth As DropDownList
        Dim tempAccYear As DropDownList
        Dim tempDateFrom As TextBox
        Dim tempDateTo As TextBox
        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden

        Dim strParam As String
        Dim strDateSetting As String

        Dim objSysCfgDs As New Object()
        Dim objDateFormat As New Object()
        Dim objDateFrom As String
        Dim objDateTo As String

        Dim strMachineTo As String
        Dim arrParam As Array    

        strDateFrom = Trim(txtDateFrom.Text)
        strDateTo = Trim(txtDateTo.Text)
        tempRpt = RptSelect.FindControl("lstRptName")
        strRptID = Trim(tempRpt.SelectedItem.Value)
        strRptName = Trim(tempRpt.SelectedItem.Text)
        strLblLocation = Trim(lblLocation.Text)

        tempAccMonth = RptSelect.FindControl("lstAccMonth")
        strAccMonth = Trim(tempAccMonth.SelectedItem.Value)
        tempAccYear = RptSelect.FindControl("lstAccYear")
        strAccYear = Trim(tempAccYear.SelectedItem.Value)
        tempRpt = RptSelect.FindControl("lstRptName")
        strRptID = Trim(tempRpt.SelectedItem.Value)
        strRptName = Trim(tempRpt.SelectedItem.Text)
        tempDec = RptSelect.FindControl("lstDecimal")
        strDec = Trim(tempDec.SelectedItem.Text)
        tempUserLoc = RptSelect.FindControl("hidUserLoc")
        strUserLoc = Trim(tempUserLoc.Value)
        strTestSampleCode = Trim(ddlTestSampleCode.SelectedItem.Text)

        arrParam = Split(Trim(ddlMachine.SelectedItem.Text), "-")
        strMachine = arrParam(0)
        arrParam = Split(Trim(ddlMachineTo.SelectedItem.Text), "-")
        strMachineTo = arrParam(0)
        If strUserLoc = "" Then
            templblUL = RptSelect.FindControl("lblUserLoc")
            templblUL.Visible = True
            Exit Sub
        Else
            If Left(strUserLoc, 3) = "','" Then
                strUserLoc = Right(strUserLoc, Len(strUserLoc) - 3)
            ElseIf Right(strUserLoc, 3) = "','" Then
                strUserLoc = Left(strUserLoc, Len(strUserLoc) - 3)
            End If
        End If


        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"
        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PM_STDRPT_Produced_Kernel_Loss_GET_CONFIG_DATE&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/PM_StdRpt_Selection.aspx")
        End Try

        strDateSetting = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))

        If Not (strDateFrom = "" And strDateTo = "") Then
            If objGlobal.mtdValidInputDate(strDateSetting, strDateFrom, objDateFormat, objDateFrom) = True And objGlobal.mtdValidInputDate(strDateSetting, strDateTo, objDateFormat, objDateTo) = True Then
                Response.Write("<Script Language=""JavaScript"">window.open(""PM_StdRpt_OilLossTransactionListPreview.aspx?Type=Print&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & strDec & _
                                "&DateFrom=" & objDateFrom & "&DateTo=" & objDateTo & _
                                "&TestSampleCode=" & strTestSampleCode & "&Machine=" & strMachine & "&MachineTo=" & strMachineTo & _
                                "&AccMonth=" & strAccMonth & "&AccYear=" & strAccYear & _
                                "&lblLocation=" & strLblLocation & _
                                "&Status=" & strStatus & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
            Else
                lblDateFormat.Text = objDateFormat & "."
                lblDate.Visible = True
                lblDateFormat.Visible = True
            End If
        Else
            Response.Write("<Script Language=""JavaScript"">window.open(""PM_StdRpt_OilLossTransactionListPreview.aspx?Type=Print&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & strDec & _
                 "&DateFrom=" & objDateFrom & "&DateTo=" & objDateTo & _
                 "&TestSampleCode=" & strTestSampleCode & "&Machine=" & strMachine & "&MachineTo=" & strMachineTo & _
                 "&AccMonth=" & strAccMonth & "&AccYear=" & strAccYear & _
                 "&lblLocation=" & strLblLocation & _
                 "&Status=" & strStatus & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
        End If
    End Sub

End Class
 
