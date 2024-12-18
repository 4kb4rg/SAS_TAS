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


Public Class CT_StdRpt_ItemSummaryList : Inherits Page

    Protected RptSelect = Page.LoadControl("../../en/Include/reports/CT_StdRpt_Selection_Ctrl.ascx")

    Protected objCT As New agri.CT.clsReport()
    Protected objCTSetup As New agri.CT.clsSetup()
    Protected objCTTrx As New agri.CT.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objSysCfg As New agri.PWSystem.clsConfig()

    Protected WithEvents lblDate As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker as Label

    Protected WithEvents lstItemCode As DropDownList
    Protected WithEvents lstItemType As DropDownList
    Protected WithEvents lstItemStatus As DropDownList

    Protected WithEvents PrintPrev as Button

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim dsForItemCodeDropDown As New DataSet()
    Dim dsForItemTypeDropDown As New DataSet()
    Dim dsForItemStatusDropDown As New DataSet()
    Dim objDsAccPeriod as New DataSet()

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
            If Not Page.IsPostBack Then
                BindItemStatusList()
                RptSelect.hideCalendar()
            end if            
       end if
    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        GetLocation()
        BindItemCodeList()
    End Sub

    Sub GetLocation()
        Dim tempUserLoc as HtmlInputText
        DIm strUserLoc as String

        tempUserLoc = RptSelect.FindControl("hidUserLoc")
        strUserLoc = Trim(tempUserLoc.Value)

        If Left(strUserLoc, 2) = ",'" Then
            strUserLoc = Right(strUserLoc, Len(strUserLoc) - 2)
        Else If Right(strUserLoc, 1) = "," Then
            strUserLoc = Left(strUserLoc, Len(strUserLoc) - 1)
        End If

    End Sub

    Sub BindItemCodeList()
        Dim strParam As String
        Dim strOppCd_Item_GET As String = "CT_STDRPT_ITEM_GET"
        Dim tempUserLoc As HtmlInputText
        Dim strUserLoc As String

        tempUserLoc = RptSelect.FindControl("hidUserLoc")
        strUserLoc = Trim(tempUserLoc.Value)

        If Left(strUserLoc, 2) = ",'" Then
            strUserLoc = Right(strUserLoc, Len(strUserLoc) - 2)
        Else If Right(strUserLoc, 1) = "," Then
            strUserLoc = Left(strUserLoc, Len(strUserLoc) - 1)
        End If

        strParam = "WHERE ITM.UpdateID = USR.UserID AND ITM.LocCode IN ('" & strUserLoc & "')"
        Try
            intErrNo = objCT.mtdGetItem(strOppCd_Item_GET, _
                                        strCompany, _
                                        strLocation, _
                                        strUserId, _
                                        strAccMonth, _
                                        strAccYear, _
                                        strParam, _
                                        dsForItemCodeDropDown)
        Catch Exp As Exception
            Response.Redirect("../../mesg/ErrorMessage.aspx?errcode=GET_ITEM_LIST&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/CT_StdRpt_Selection.aspx")
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


    Sub BindItemStatusList()

        lstItemStatus.Items.Add(New ListItem(objCTSetup.mtdGetStockItemStatus(objCTSetup.EnumStockItemStatus.All), objCTSetup.EnumStockItemStatus.All))
        lstItemStatus.Items.Add(New ListItem(objCTSetup.mtdGetStockItemStatus(objCTSetup.EnumStockItemStatus.Active), objCTSetup.EnumStockItemStatus.Active))
        lstItemStatus.Items.Add(New ListItem(objCTSetup.mtdGetStockItemStatus(objCTSetup.EnumStockItemStatus.Deleted), objCTSetup.EnumStockItemStatus.Deleted))

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
        Dim tempUserLoc As HtmlInputText
       
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
        tempUserLoc = RptSelect.FindControl("hidUserLoc")
        strUserLoc = trim(tempUserLoc.value)

        If Left(strUserLoc, 3) = "','" Then
            strUserLoc = Right(strUserLoc, Len(strUserLoc) - 3)
        else if Right(strUserLoc, 3) = "','" Then
            strUserLoc = Left(strUserLoc, Len(strUserLoc) - 3)
        end if

        if lstItemCode.SelectedIndex = 0 then
            strItemCode = ""
        else
            strItemCode = Trim(lstItemCode.SelectedItem.Value)
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
            Response.Redirect("../../mesg/ErrorMessage.aspx?errcode=CT_STDRPT_ITEMLIST_GET_CONFIG_DATE&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/CT_StdRpt_Selection.aspx")
        End Try

        strDateSetting = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))

        if not (strDateFrom = "" and strDateTo = "") then
            If objGlobal.mtdValidInputDate(strDateSetting, strDateFrom, objDateFormat, objDateFrom) = True and objGlobal.mtdValidInputDate(strDateSetting, strDateTo, objDateFormat, objDateTo) = True Then
                Response.Write("<Script Language=""JavaScript"">window.open(""CT_StdRpt_ItemSummaryListPreview.aspx?Type=Print&CompName=" & strCompany & "&Location=" & strUserLoc & "&AccMonth=" & _
                               strAccMonth & "&AccYear=" & strAccYear & "&RptName=" & strRptName & "&DateFrom=" & objDateFrom & "&DateTo=" & objDateTo & "&ItemStatus=" & strItemStatus & "&ItemCode=" & _
                               strItemCode & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
            Else
                lblDate.text = lblDate.text & objDateFormat & "."
                lblDate.visible = true
            End If
        else
            Response.Write("<Script Language=""JavaScript"">window.open(""CT_StdRpt_ItemSummaryListPreview.aspx?Type=Print&CompName=" & strCompany & "&Location=" & strUserLoc & "&AccMonth=" & _
                           strAccMonth & "&AccYear=" & strAccYear & "&RptName=" & strRptName & "&DateFrom=" & objDateFrom & "&DateTo=" & objDateTo & "&ItemStatus=" & strItemStatus & "&ItemCode=" & _
                           strItemCode & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
        end if
    End Sub
End Class
