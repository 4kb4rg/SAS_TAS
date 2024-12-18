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


Public Class IN_StdRpt_ItemRetAdvList : Inherits Page

    Protected RptSelect As UserControl

    Protected objIN As New agri.IN.clsReport()
    Protected objINTrx As New agri.IN.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objSysCfg As New agri.PWSystem.clsConfig()

    Protected WithEvents hidUserLocPX As HtmlInputHidden
    Protected WithEvents hidAccMonthPX As HtmlInputHidden
    Protected WithEvents hidAccYearPX As HtmlInputHidden

    Protected WithEvents lblDate As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker as Label

    Protected WithEvents lstDocNoFrom As DropDownList
    Protected WithEvents lstDocNoTo As DropDownList    
    Protected WithEvents lstStatus As DropDownList
    Protected WithEvents lstItemCode As DropDownList

    Protected WithEvents PrintPrev as ImageButton

    Dim dsForDocNoFromDropDown as New DataSet()
    Dim dsForDocNoToDropDown as New DataSet()
    Dim dsForItemCodeDropDown as New DataSet()

    Dim strCompany As String
    Dim strCompanyName as String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String

    Dim dr As DataRow
    Dim intCnt As Integer
    Dim intErrNo As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        Dim tempDateFrom as Textbox
        Dim tempDateTo as Textbox
        Dim intCnt as Integer

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        
        lblDate.visible = false
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        else



        If Not Page.IsPostBack Then
            hidUserLocPX.Value = Request.QueryString("UserLoc")

            BindDocNoFromList("")
            BindItemCodeList()
            BindStatus()
        End If
       end if
    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim UCTrFromTo As HtmlTableRow

        If Page.IsPostBack Then
            BindDocNoFromList(Request.Form("lstDocNoFrom"))
            'DocNoToList()
            BindItemCodeList()
        End If

        UCTrFromTo = RptSelect.FindControl("trfromto")
        UCTrFromTo.Visible = True

    End Sub

    Sub BindDocNoFromList(ByVal pv_strDocNoFrom As String)
        '    Dim strParam As String
        '    Dim strOppCd_ItemRetAdv_DocNo_GET As String = "IN_STDRPT_ITEMRETADV_DOCNO_GET"
        '    Dim strUserLoc As String
        '    Dim intIndex As Integer = 0

        '    If Left(hidUserLocPX.Value, 3) = "','" Then
        '        strUserLoc = Right(hidUserLocPX.Value, Len(hidUserLocPX.Value) - 3)
        '    ElseIf Right(hidUserLocPX.Value, 1) = "," Then
        '        strUserLoc = Left(hidUserLocPX.Value, Len(hidUserLocPX.Value) - 1)
        '    End If

        '    strParam = strUserLoc & "|"
        '    Try
        '        intErrNo = objIN.mtdGetItemRetAdv(strOppCd_ItemRetAdv_DocNo_GET, _
        '                                          strParam, _
        '                                          objGlobal.EnumDocType.StockReturnAdvice, _
        '                                          dsForDocNoFromDropDown)
        '    Catch Exp As Exception
        '        Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_IN_ITEMRETADV_DOCNO_FROM_LIST&errmesg=" & lblErrMessage.Text & "&redirect=../../en/reports/IN_StdRpt_Selection.aspx")
        '    End Try

        '    For intCnt = 0 To dsForDocNoFromDropDown.Tables(0).Rows.Count - 1
        '        dsForDocNoFromDropDown.Tables(0).Rows(intCnt).Item("ItemRetAdvID") = Trim(dsForDocNoFromDropDown.Tables(0).Rows(intCnt).Item("ItemRetAdvID"))
        '        dsForDocNoFromDropDown.Tables(0).Rows(intCnt).Item("Description") = Trim(dsForDocNoFromDropDown.Tables(0).Rows(intCnt).Item("ItemRetAdvID"))
        '        If Trim(pv_strDocNoFrom) = Trim(dsForDocNoFromDropDown.Tables(0).Rows(intCnt).Item("ItemRetAdvID")) Then
        '            intIndex = intCnt + 1
        '        End If
        '    Next intCnt

        '    dr = dsForDocNoFromDropDown.Tables(0).NewRow()
        '    dr("ItemRetAdvID") = ""
        '    dr("Description") = "All"
        '    dsForDocNoFromDropDown.Tables(0).Rows.InsertAt(dr, 0)

        '    lstDocNoFrom.DataSource = dsForDocNoFromDropDown.Tables(0)
        '    lstDocNoFrom.DataValueField = "ItemRetAdvID"
        '    lstDocNoFrom.DataTextField = "Description"
        '    lstDocNoFrom.DataBind()
        '    lstDocNoFrom.SelectedIndex = intIndex

        'End Sub

        'Sub BindDocNoToList(ByVal Sender As Object, ByVal E As EventArgs)
        '    DocNoToList()
        'End Sub

        'Sub DocNoToList()
        '    Dim strParam As String
        '    Dim dsEmpty As New DataTable()
        '    Dim strOppCd_ItemRetAdv_DocNo_GET As String = "IN_STDRPT_ITEMRETADV_DOCNO_GET"
        '    Dim strUserLoc As String

        '    If Left(hidUserLocPX.Value, 3) = "','" Then
        '        strUserLoc = Right(hidUserLocPX.Value, Len(hidUserLocPX.Value) - 3)
        '    ElseIf Right(hidUserLocPX.Value, 1) = "," Then
        '        strUserLoc = Left(hidUserLocPX.Value, Len(hidUserLocPX.Value) - 1)
        '    End If

        '    If Not Trim(lstDocNoFrom.SelectedItem.Value) = "" Then
        '        strParam = strUserLoc & "|" & "AND IRA.ItemRetAdvID >= '" & Trim(lstDocNoFrom.SelectedItem.Value) & "'"

        '        Try
        '            intErrNo = objIN.mtdGetItemRetAdv(strOppCd_ItemRetAdv_DocNo_GET, _
        '                                            strParam, _
        '                                            objGlobal.EnumDocType.StockReturnAdvice, _
        '                                            dsForDocNoToDropDown)
        '        Catch Exp As Exception
        '            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_IN_ITEMRETADV_DOCNO_TO_LIST&errmesg=" & lblErrMessage.Text & "&redirect=../../en/reports/IN_StdRpt_Selection.aspx")
        '        End Try

        '        If Not lstDocNoFrom.SelectedIndex = 0 Then
        '            For intCnt = 0 To dsForDocNoToDropDown.Tables(0).Rows.Count - 1
        '                dsForDocNoToDropDown.Tables(0).Rows(intCnt).Item("ItemRetAdvID") = Trim(dsForDocNoToDropDown.Tables(0).Rows(intCnt).Item("ItemRetAdvID"))
        '            Next intCnt

        '            lstDocNoTo.DataSource = dsForDocNoToDropDown.Tables(0)
        '            lstDocNoTo.DataValueField = "ItemRetAdvID"
        '            lstDocNoTo.DataBind()
        '        Else
        '            lstDocNoTo.DataSource = dsEmpty
        '            lstDocNoTo.DataBind()
        '        End If
        '    End If

        '    If lstDocNoFrom.SelectedIndex = 0 Then
        '        lstDocNoTo.DataSource = dsEmpty
        '        lstDocNoTo.DataBind()
        '    End If

    End Sub

    Sub BindItemCodeList()
        'Dim strParam As String
        'Dim strOppCd_ItemRetAdv_ItemCode_GET = "IN_STDRPT_ITEMRETADV_ITEMCODE_GET"
        'Dim strUserLoc As String

        'If Left(hidUserLocPX.Value, 3) = "','" Then
        '    strUserLoc = Right(hidUserLocPX.Value, Len(hidUserLocPX.Value) - 3)
        'ElseIf Right(hidUserLocPX.Value, 1) = "," Then
        '    strUserLoc = Left(hidUserLocPX.Value, Len(hidUserLocPX.Value) - 1)
        'End If

        'strParam = strUserLoc & "| "
        'Try
        '    intErrNo = objIN.mtdGetItemRetAdv(strOppCd_ItemRetAdv_ItemCode_GET, _
        '                                      strParam, _
        '                                      objGlobal.EnumDocType.StockReturnAdvice, _
        '                                      dsForItemCodeDropDown)
        'Catch Exp As Exception
        '    Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_IN_ITEMRETADV_ITEMCODE_LIST&errmesg=" & lblErrMessage.Text & "&redirect=../../en/reports/IN_StdRpt_Selection.aspx")
        'End Try

        'For intCnt = 0 To dsForItemCodeDropDown.Tables(0).Rows.Count - 1
        '    dsForItemCodeDropDown.Tables(0).Rows(intCnt).Item("ItemCode") = Trim(dsForItemCodeDropDown.Tables(0).Rows(intCnt).Item("ItemCode"))
        '    dsForItemCodeDropDown.Tables(0).Rows(intCnt).Item("Description") = Trim(dsForItemCodeDropDown.Tables(0).Rows(intCnt).Item("ItemCode"))
        'Next intCnt

        'dr = dsForItemCodeDropDown.Tables(0).NewRow()
        'dr("ItemCode") = ""
        'dr("Description") = "All"
        'dsForItemCodeDropDown.Tables(0).Rows.InsertAt(dr, 0)

        'lstItemCode.DataSource = dsForItemCodeDropDown.Tables(0)
        'lstItemCode.DataValueField = "ItemCode"
        'lstItemCode.DataTextField = "Description"
        'lstItemCode.DataBind()
    End Sub


    Sub BindStatus()

        lstStatus.Items.Add(New ListItem(objINTrx.mtdGetStockRetAdvStatus(objINTrx.EnumStockRetAdvStatus.All), objINTrx.EnumStockRetAdvStatus.All))
        lstStatus.Items.Add(New ListItem(objINTrx.mtdGetStockRetAdvStatus(objINTrx.EnumStockRetAdvStatus.Active), objINTrx.EnumStockRetAdvStatus.Active))
        lstStatus.Items.Add(New ListItem(objINTrx.mtdGetStockRetAdvStatus(objINTrx.EnumStockRetAdvStatus.Cancelled), objINTrx.EnumStockRetAdvStatus.Cancelled))
        lstStatus.Items.Add(New ListItem(objINTrx.mtdGetStockRetAdvStatus(objINTrx.EnumStockRetAdvStatus.Confirmed), objINTrx.EnumStockRetAdvStatus.Confirmed))
        lstStatus.Items.Add(New ListItem(objINTrx.mtdGetStockRetAdvStatus(objINTrx.EnumStockRetAdvStatus.Deleted), objINTrx.EnumStockRetAdvStatus.Deleted))

        lstStatus.SelectedIndex = 3

    End Sub


    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strDocNoFrom As String
        Dim strDocNoTo As String
        Dim strItemCode As String
        Dim strStatus As String

        Dim strDateFrom As String
        Dim strDateTo As String
        Dim strRptName As String
        Dim strUserLoc As String

        Dim tempDateFrom As TextBox
        Dim tempDateTo As TextBox
        Dim tempRptName As DropDownList

        Dim strParam As String
        Dim strDateSetting As String

        Dim objSysCfgDs As New Object()
        Dim objDateFormat As New Object()
        Dim objDateFrom As String
        Dim objDateTo As String

        tempDateFrom = RptSelect.FindControl("txtDateFrom")
        strDateFrom = Trim(tempDateFrom.Text)
        tempDateTo = RptSelect.FindControl("txtDateTo")
        strDateTo = Trim(tempDateTo.Text)
        tempRptName = RptSelect.FindControl("lstRptName")
        strRptName = Trim(tempRptName.SelectedItem.Value)
        strUserLoc = Trim(hidUserLocPX.Value)

        If Left(strUserLoc, 3) = "','" Then
            strUserLoc = Right(strUserLoc, Len(strUserLoc) - 3)
        ElseIf Right(strUserLoc, 3) = "','" Then
            strUserLoc = Left(strUserLoc, Len(strUserLoc) - 3)
        End If

        If lstDocNoFrom.SelectedIndex = 0 Then
            strDocNoFrom = ""
        Else
            strDocNoFrom = Trim(lstDocNoFrom.SelectedItem.Value)
        End If

        If lstDocNoTo.SelectedIndex = -1 Then
            strDocNoTo = ""
        Else
            strDocNoTo = Trim(lstDocNoTo.SelectedItem.Value)
        End If

        If lstItemCode.SelectedIndex = 0 Then
            strItemCode = ""
        Else
            strItemCode = Trim(lstItemCode.SelectedItem.Value)
        End If

        strStatus = Trim(lstStatus.SelectedItem.Value)

        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"
        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=IN_STDRPT_ITEMRETADV_GET_CONFIG_DATE&errmesg=" & lblErrMessage.Text & "&redirect=../../en/reports/IN_StdRpt_Selection.aspx")
        End Try

        strDateSetting = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))

        If Not (strDateFrom = "" And strDateTo = "") Then
            If objGlobal.mtdValidInputDate(strDateSetting, strDateFrom, objDateFormat, objDateFrom) = True And objGlobal.mtdValidInputDate(strDateSetting, strDateTo, objDateFormat, objDateTo) = True Then
                Response.Write("<Script Language=""JavaScript"">window.open(""IN_StdRpt_ItemRetAdvListPreview.aspx?Type=Print&CompName=" & strCompany & "&Location=" & strUserLoc & "&AccMonth=" & hidAccMonthPX.Value & "&AccYear=" & _
                               hidAccYearPX.Value & "&RptName=" & strRptName & "&DateFrom=" & objDateFrom & "&DateTo=" & objDateTo & "&DocNoFrom=" & strDocNoFrom & "&DocNoTo=" & strDocNoTo & "&ItemCode=" & _
                               strItemCode & "&Status=" & strStatus & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
            Else
                lblDate.Text = lblDate.Text & objDateFormat & "."
                lblDate.Visible = True
            End If
        Else
            Response.Write("<Script Language=""JavaScript"">window.open(""IN_StdRpt_ItemRetAdvListPreview.aspx?Type=Print&CompName=" & strCompany & "&Location=" & strUserLoc & "&AccMonth=" & hidAccMonthPX.Value & "&AccYear=" & _
                            hidAccYearPX.Value & "&RptName=" & strRptName & "&DateFrom=" & objDateFrom & "&DateTo=" & objDateTo & "&DocNoFrom=" & strDocNoFrom & "&DocNoTo=" & strDocNoTo & "&ItemCode=" & _
                            strItemCode & "&Status=" & strStatus & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
        End If
    End Sub

End Class
