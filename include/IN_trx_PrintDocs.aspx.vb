Imports System
Imports System.Data
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings

Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Public Class IN_trx_PrintDocs : Inherits Page

    Protected WithEvents txtFromId As TextBox
    Protected WithEvents txtToId As TextBox
    Protected WithEvents ddlIssueType As Dropdownlist
    Protected WithEvents cblDisplayCost As CheckBoxList
    Protected WithEvents ibConfirm As ImageButton
    Protected WithEvents lblDocName As Label
    Protected WithEvents lblPR As Label
    Protected WithEvents lblSRA As Label
    Protected WithEvents lblST As Label
    Protected WithEvents lblSI As Label
    Protected WithEvents lblFI As Label
    Protected WithEvents lblErrCode As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents txtDateFrom As TextBox
    Protected WithEvents txtDateTo As TextBox
    Protected WithEvents lblDateFrom As Label
    Protected WithEvents lblDateTo As Label

    Dim objINTrx As New agri.IN.clsTrx()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCapDs As New Dataset()
    Dim objRptDs As New DataSet()

    Const PR = 1    
    Const SRA = 2   
    Const ST = 3    
    Const SI = 4    
    Const FI = 5    

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim intConfig As integer
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intDocType As Integer


    Dim strAccTag As String
    Dim strBlkTag As String
    Dim strVehTag As String
    Dim strVehExpCodeTag As String
    Dim strLangCode As String
    Dim strDateFormat As String
    Dim strAcceptFormat As String

	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String
	
    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        intConfig = Session("SS_CONFIGSETTING")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        strDateFormat = Session("SS_DATEFMT")
        strLocType = Session("SS_LOCTYPE")

        If Request.QueryString("doctype") = "" Then
            intDocType = 0
        Else
            intDocType = Request.QueryString("doctype")
        End If

        lblErrCode.Visible = False
        lblDateFrom.Visible = False
        lblDateTo.Visible = False

        If Page.IsPostBack Then

            Select Case CInt(intDocType)
                Case PR
                Case SRA
                Case ST
                Case SI
                    onsubmit_printSI()
                Case FI
                    onsubmit_printFI()
            End Select
        Else
            Select Case CInt(intDocType)
                Case PR
                    lblDocName.Text = lblPR.Text
                Case SRA
                    lblDocName.Text = lblSRA.Text
                Case ST
                    lblDocName.Text = lblST.Text
                Case SI
                    lblDocName.Text = lblSI.Text
                    BindIssueType()
                Case FI
                    lblDocName.Text = lblFI.Text
                    BindIssueType()
                Case Else
                    lblDocName.Text = "Unknown document"
            End Select
        End If
    End Sub

    Sub BindIssueType()
        ddlIssueType.Items.Add(New ListItem(objINTrx.mtdGetStockIssueType(objINTrx.EnumStockIssueType.OwnUse), objINTrx.EnumStockIssueType.OwnUse))
        ddlIssueType.Items.Add(New ListItem(objINTrx.mtdGetStockIssueType(objINTrx.EnumStockIssueType.StaffPayroll), objINTrx.EnumStockIssueType.StaffPayroll))
        ddlIssueType.Items.Add(New ListItem(objINTrx.mtdGetStockIssueType(objINTrx.EnumStockIssueType.StaffDN), objINTrx.EnumStockIssueType.StaffDN))
        ddlIssueType.Items.Add(New ListItem(objINTrx.mtdGetStockIssueType(objINTrx.EnumStockIssueType.External), objINTrx.EnumStockIssueType.External))
        IF CInt(intDocType) = SI
            ddlIssueType.Items.Add(New ListItem(objINTrx.mtdGetStockIssueType(objINTrx.EnumStockIssueType.Nursery), objINTrx.EnumStockIssueType.Nursery))
        END IF        
    End Sub



    Sub onsubmit_printSI()
        Dim strOpCd_Get As String = "IN_CLSTRX_STOCKISSUE_LIST_GET" & "|" & "StockIssue"
        Dim strOpCodePrint As String = "ADMIN_SHARE_UPD_PRINTDATE"
        Dim strTable As String = "IN_STOCKISSUE"
        Dim strUpdString As String = ""
        Dim strParam As String
        Dim SearchStr As String = ""
        Dim SortStr As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intStatus As Integer
        Dim strPrintDate As String
        Dim strStockIssueID As String
        Dim strIDRange As String = ""
        Dim strReprintedID As String = ""
        Dim strSortLine As String

        Dim strFromID As String
        Dim strToID As String
        Dim strIssueType As String
        Dim strStatus As String
        Dim strDisplayCost As String = ""
        Dim strDateFrom As String = Date_Validation(txtDateFrom.Text, False)
        Dim strDateTo As String = Date_Validation(txtDateTo.Text, False)

        strFromID = txtFromId.Text.Trim
        strToID = txtToId.Text.Trim
        strIssueType = Trim(ddlIssueType.SelectedItem.Value)
        strStatus = CStr(objINTrx.EnumStockIssueStatus.Confirmed) & "','" & Cstr(objINTrx.EnumStockIssueStatus.DbNote)
        If cblDisplayCost.Items(0).Selected Then
            strDisplayCost = "1"
        End If

        If strDateFrom = "" And strDateTo = "" Then
            SearchStr = "and iss.StockIssueID >= '" & strFromID & "' " & _
                        "and iss.StockIssueID <= '" & strToID & "' " & _
                        "and iss.IssueType in ('" & strIssueType & "') " & _
                        "and iss.Status in ('" & strStatus & "') " & _
                        "and iss.LocCode = '" & strLocation & "' "
        Else
            SearchStr = "and iss.StockIssueID >= '" & strFromID & "' " & _
                        "and iss.StockIssueID <= '" & strToID & "' " & _
                        "and iss.IssueType in ('" & strIssueType & "') " & _
                        "and iss.Status in ('" & strStatus & "') " & _
                        "and iss.LocCode = '" & strLocation & "' " & _
                        "and iss.StockIssueDate Between '" & strDateFrom & "' and '" & strDateTo & "' "
        End If

        SortStr = "order by StockIssueID"

        strParam = "||" & SearchStr & "|" & SortStr

        Try
            intErrNo = objINTrx.mtdGetStockIssueReport(strOpCd_Get, _
                                                       strParam, _
                                                       strCompany, _
                                                       strLocation, _
                                                       strUserId, _
                                                       strAccMonth, _
                                                       strAccYear, _
                                                       objRptDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_TRX_PRINTDOCS_GETRANGE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objRptDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objRptDs.Tables(0).Rows.Count - 1

                intStatus = CInt(Trim(objRptDs.Tables(0).Rows(intCnt).Item("Status")))
                strPrintDate = objGlobal.GetLongDate(objRptDs.Tables(0).Rows(intCnt).Item("PrintDate"))
                strStockIssueID = Trim(objRptDs.Tables(0).Rows(intCnt).Item("StockIssueID"))
                strIDRange = strIDRange & strStockIssueID & "','"

                strUpdString = "where StockIssueID = '" & strStockIssueID & "'"
                If intStatus = objINTrx.EnumStockIssueStatus.Confirmed Or intStatus = objINTrx.EnumStockIssueStatus.DbNote Then
                    If strPrintDate = "" Then
                        Try
                            intErrNo = objAdmin.mtdUpdPrintDate(strOpCodePrint, _
                                                                strUpdString, _
                                                                strTable, _
                                                                strCompany, _
                                                                strLocation, _
                                                                strUserId)
                        Catch Exp As System.Exception
                            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_TRX_PRINTDOCS_UPDPRINTDATE&errmesg=" & lblErrMessage.Text & "&redirect=")
                        End Try
                    Else
                        strReprintedID = strReprintedID & strStockIssueID & "|"
                    End If
                End If
            Next
            strIDRange = Left(strIDRange, Len(strIDRange) - 3)
        Else
            lblErrCode.Visible = True
            Exit Sub
        End If


        onload_GetLangCap()

        If strLocation <> "OLN" Then
            Response.Write("<Script Language=""JavaScript"">window.open(""../reports/IN_Rpt_StockIssueDet.aspx?strStockIssueId=" & strIDRange & _
                           "&strIssueType=" & strIssueType & _
                           "&strDisplayCost=" & strDisplayCost & _
                           "&AccountTag=" & strAccTag & _
                           "&BlockTag=" & strBlkTag & _
                           "&VehicleTag=" & strVehTag & _
                           "&VehExpenseTag=" & strVehExpCodeTag & _
                           "&batchPrint=yes" & _
                           "&reprintId=" & strReprintedID & _
                           """, null,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");window.close();</Script>")
        Else
            Response.Write("<Script Language=""JavaScript"">window.open(""../reports/IN_Rpt_StockIssueDet_Olein.aspx?strStockIssueId=" & strIDRange & _
                                      "&strIssueType=" & strIssueType & _
                                      "&strDisplayCost=" & strDisplayCost & _
                                      "&AccountTag=" & strAccTag & _
                                      "&BlockTag=" & strBlkTag & _
                                      "&VehicleTag=" & strVehTag & _
                                      "&VehExpenseTag=" & strVehExpCodeTag & _
                                      "&batchPrint=yes" & _
                                      "&reprintId=" & strReprintedID & _
                                      "&DateFrom=" & strDateFrom & _
                                      "&DateTo=" & strDateTo & _
                                      """, null,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");window.close();</Script>")
        End If
    End Sub

    Sub onsubmit_printFI()
        Dim strOpCd_Get As String = "IN_CLSTRX_FUELISSUE_LIST_GET" & "|" & "FuelIssue"
        Dim strOpCodePrint As String = "ADMIN_SHARE_UPD_PRINTDATE"
        Dim strTable As String = "IN_FUELISSUE"
        Dim strUpdString As String = ""
        Dim strParam As String
        Dim SearchStr As String = ""
        Dim SortStr As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intStatus As Integer
        Dim strPrintDate As String
        Dim strFuelIssueID As String
        Dim strIDRange As String = ""
        Dim strReprintedID As String = ""
        Dim strSortLine As String

        Dim strFromID As String
        Dim strToID As String
        Dim strIssueType As String
        Dim strStatus As String
        Dim strDisplayCost As String = ""
        Dim strDateFrom As String = Date_Validation(txtDateFrom.Text, False)
        Dim strDateTo As String = Date_Validation(txtDateFrom.Text, False)


        strFromID = txtFromId.Text.Trim
        strToID = txtToId.Text.Trim
        strIssueType = Trim(ddlIssueType.SelectedItem.Value)
        strStatus = CStr(objINTrx.EnumFuelIssueStatus.Confirmed) & "','" & CStr(objINTrx.EnumFuelIssueStatus.DbNote)
        If cblDisplayCost.Items(0).Selected Then
            strDisplayCost = "1"
        End If

        If strDateFrom = "" And strDateTo = "" Then
            SearchStr = "and iss.FuelIssueID >= '" & strFromID & "' " & _
                    "and iss.FuelIssueID <= '" & strToID & "' " & _
                    "and iss.IssueType in ('" & strIssueType & "') " & _
                    "and iss.Status in ('" & strStatus & "') " & _
                    "and iss.LocCode = '" & strLocation & "' "
        Else
            SearchStr = "and iss.FuelIssueID >= '" & strFromID & "' " & _
                    "and iss.FuelIssueID <= '" & strToID & "' " & _
                    "and iss.IssueType in ('" & strIssueType & "') " & _
                    "and iss.Status in ('" & strStatus & "') " & _
                    "and iss.LocCode = '" & strLocation & "' " & _
                    "and iss.CreateDate Between '" & strDateFrom & "' and '" & strDateTo & "' "

        End If

        SortStr = "order by FuelIssueID"

        strParam = "||" & SearchStr & "|" & SortStr

        Try
            intErrNo = objINTrx.mtdGetFuelIssueReport(strOpCd_Get, _
                                                       strParam, _
                                                       strCompany, _
                                                       strLocation, _
                                                       strUserId, _
                                                       strAccMonth, _
                                                       strAccYear, _
                                                       objRptDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_TRX_PRINTDOCS_GETRANGE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objRptDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objRptDs.Tables(0).Rows.Count - 1

                intStatus = CInt(Trim(objRptDs.Tables(0).Rows(intCnt).Item("Status")))
                strPrintDate = objGlobal.GetLongDate(objRptDs.Tables(0).Rows(intCnt).Item("PrintDate"))
                strFuelIssueID = Trim(objRptDs.Tables(0).Rows(intCnt).Item("FuelIssueID"))
                strIDRange = strIDRange & strFuelIssueID & "','"

                strUpdString = "where FuelIssueID = '" & strFuelIssueID & "'"
                If intStatus = objINTrx.EnumStockIssueStatus.Confirmed Or intStatus = objINTrx.EnumStockIssueStatus.DbNote Then
                    If strPrintDate = "" Then
                        Try
                            intErrNo = objAdmin.mtdUpdPrintDate(strOpCodePrint, _
                                                                strUpdString, _
                                                                strTable, _
                                                                strCompany, _
                                                                strLocation, _
                                                                strUserId)
                        Catch Exp As System.Exception
                            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_TRX_PRINTDOCS_UPDPRINTDATE&errmesg=" & lblErrMessage.Text & "&redirect=")
                        End Try
                    Else
                        strReprintedID = strReprintedID & strFuelIssueID & "|"
                    End If
                End If
            Next
            strIDRange = Left(strIDRange, Len(strIDRange) - 3)
        Else
            lblErrCode.Visible = True
            Exit Sub
        End If

        onload_GetLangCap()

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/IN_Rpt_FuelIssueDet.aspx?strFuelIssueId=" & strIDRange & _
                       "&strIssueType=" & strIssueType & _
                       "&strDisplayCost=" & strDisplayCost & _
                       "&AccountTag=" & strAccTag & _
                       "&BlockTag=" & strBlkTag & _
                       "&VehicleTag=" & strVehTag & _
                       "&VehExpenseTag=" & strVehExpCodeTag & _
                       "&batchPrint=yes" & _
                       "&reprintId=" & strReprintedID & _
                       """, null,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");window.close();</Script>")

    End Sub

    Sub close_window()
        Response.Write("<Script Language=""JavaScript"">window.close();</Script>")
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        Try
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfig) = True Then
                strBlkTag = GetCaption(objLangCap.EnumLangCap.Block)
            Else
               strBlkTag = GetCaption(objLangCap.EnumLangCap.SubBlock)
            End If
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_TRX_PRINTDOCS_GETLANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        strAccTag = GetCaption(objLangCap.EnumLangCap.Account)
        strVehTag = GetCaption(objLangCap.EnumLangCap.Vehicle)
        strVehExpCodeTag = GetCaption(objLangCap.EnumLangCap.VehExpense)
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_TRX_PRINTDOCS_GETENTIRELANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=")
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

End Class
