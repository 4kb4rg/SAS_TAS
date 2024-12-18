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
Imports agri.IN
Imports agri.PWSystem.clsConfig
Imports agri.Admin.clsShare

Public Class IN_StdRpt_ItemSummaryList : Inherits Page

    Protected RptSelect = Page.LoadControl("../../en/Include/reports/IN_StdRpt_Selection_Ctrl.ascx")

    Protected objIN As New agri.IN.clsReport()
    Protected objINSetup As New agri.IN.clsSetup()
    Protected objINTrx As New agri.IN.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objSysCfg As New agri.PWSystem.clsConfig()
    Protected objAdmin As New agri.Admin.clsShare()

    Protected WithEvents lblDate As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker as Label

    Protected WithEvents lstItemCode As DropDownList
    Protected WithEvents lstItemType As DropDownList
    Protected WithEvents lstItemStatus As DropDownList

    Protected WithEvents PrintPrev as Button
    Protected WithEvents SreeenPrev As Button

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim dsForItemCodeDropDown As New DataSet()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
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
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        
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
                BindItemCodeList()
                BindItemTypeList()
                BindItemStatusList()
                RptSelect.hideCalendar()
            end if            
       end if
    End Sub


    Sub BindItemCodeList()
        Dim strParam As String
        Dim strOppCd_Item_GET As String = "IN_STDRPT_ITEM_GET"

            strParam = " AND ITM.UpdateID = Usr.UserID"

        Try
            intErrNo = objIN.mtdGetItem(strOppCd_Item_GET, _
                                        strCompany, _
                                        strLocation, _
                                        strUserId, _
                                        strAccMonth, _
                                        strAccYear, _
                                        strParam, _
                                        dsForItemCodeDropDown)
        Catch Exp As Exception
            Response.Redirect("../mesg/ErrorMessage.aspx?errcode=GET_ITEM_LIST&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/IN_StdRpt_Selection.aspx")
        End Try

        For intCnt = 0 To dsForItemCodeDropDown.Tables(0).Rows.Count - 1
            dsForItemCodeDropDown.Tables(0).Rows(intCnt).Item("ItemCode") = Trim(dsForItemCodeDropDown.Tables(0).Rows(intCnt).Item("ItemCode"))
            dsForItemCodeDropDown.Tables(0).Rows(intCnt).Item("Description") = Trim(dsForItemCodeDropDown.Tables(0).Rows(intCnt).Item("ItemCode"))
        Next intCnt

        dr = dsForItemCodeDropDown.Tables(0).NewRow()
        dr("ItemCode") = ""
        dr("Description") = "All"
        dsForItemCodeDropDown.Tables(0).Rows.InsertAt(dr, 0)

        lstItemCode.DataSource = dsForItemCodeDropDown.Tables(0)
        lstItemCode.DataValueField = "ItemCode"
        lstItemCode.DataTextField = "Description"
        lstItemCode.DataBind()
    End Sub

    Sub BindItemTypeList()
        Dim strText = "All"

        lstItemType.Items.Add(New ListItem(strText))
        lstItemType.Items.Add(New ListItem(objINSetup.mtdGetInventoryItemType(objINSetup.EnumInventoryItemType.DirectCharge), objINSetup.EnumInventoryItemType.DirectCharge))
        lstItemType.Items.Add(New ListItem(objINSetup.mtdGetInventoryItemType(objINSetup.EnumInventoryItemType.Stock), objINSetup.EnumInventoryItemType.Stock))
    
    End Sub

    Sub BindItemStatusList()

        lstItemStatus.Items.Add(New ListItem(objINSetup.mtdGetStockItemStatus(objINSetup.EnumStockItemStatus.All), objINSetup.EnumStockItemStatus.All))
        lstItemStatus.Items.Add(New ListItem(objINSetup.mtdGetStockItemStatus(objINSetup.EnumStockItemStatus.Active), objINSetup.EnumStockItemStatus.Active))
        lstItemStatus.Items.Add(New ListItem(objINSetup.mtdGetStockItemStatus(objINSetup.EnumStockItemStatus.Deleted), objINSetup.EnumStockItemStatus.Deleted))

        lstItemStatus.SelectedIndex = 1
    End Sub


    Sub btnPrintPrev_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strItemStatus As String
        Dim strItemType As String
        Dim strItemCode As String

        Dim strDateFrom As String
        Dim strDateTo As String
        Dim strRptName As String
        Dim strUserLoc As String

        Dim tempDateFrom As TextBox
        Dim tempDateTo As TextBox
        Dim tempRptName As DropDownList
        Dim tempUserLoc as HtmlInputHidden
        
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
        tempUserLoc = RptSelect.FindControl("UserLoc")
        strUserLoc = trim(tempUserLoc.value)

        If Left(strUserLoc, 3) = "','" Then
            strUserLoc = Right(strUserLoc, Len(strUserLoc) - 3)
        else if Right(strUserLoc, 3) = "','" Then
            strUserLoc = Left(strUserLoc, Len(strUserLoc) - 3)
        End If

        if lstItemCode.SelectedIndex = 0 then
            strItemCode = ""
        else
            strItemCode = Trim(lstItemCode.SelectedItem.Value)
        end if

        if lstItemType.SelectedIndex = 0 then
            strItemType = ""
        else
            strItemType = Trim(lstItemType.SelectedItem.Value)
        end if

        strItemStatus = Trim(lstItemStatus.SelectedItem.Value)

        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"
        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As Exception
            Response.Redirect("../mesg/ErrorMessage.aspx?errcode=IN_STDRPT_ITEMLIST_GET_CONFIG_DATE&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/IN_StdRpt_Selection.aspx")
        End Try

        strDateSetting = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))

        if not (strDateFrom = "" and strDateTo = "") then
            If objGlobal.mtdValidInputDate(strDateSetting, strDateFrom, objDateFormat, objDateFrom) = True and objGlobal.mtdValidInputDate(strDateSetting, strDateTo, objDateFormat, objDateTo) = True Then
                Response.Write("<Script Language=""JavaScript"">window.open(""IN_StdRpt_ItemSummaryListPreview.aspx?Type=Print&CompName=" & strCompany & "&Location=" & strUserLoc & "&AccMonth=" & strAccMonth & "&AccYear=" & strAccYear & "&RptName=" & strRptName & "&DateFrom=" & objDateFrom & "&DateTo=" & objDateTo & "&ItemStatus=" & strItemStatus & "&ItemType=" & strItemType & "&ItemCode=" & strItemCode & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
            Else
                lblDate.text = lblDate.text & objDateFormat & "."
                lblDate.visible = true
            End If
        else
            Response.Write("<Script Language=""JavaScript"">window.open(""IN_StdRpt_ItemSummaryListPreview.aspx?Type=Print&CompName=" & strCompany & "&Location=" & strUserLoc & "&AccMonth=" & strAccMonth & "&AccYear=" & strAccYear & "&RptName=" & strRptName & "&DateFrom=" & objDateFrom & "&DateTo=" & objDateTo & "&ItemStatus=" & strItemStatus & "&ItemType=" & strItemType & "&ItemCode=" & strItemCode & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
        end if
    End Sub

End Class
