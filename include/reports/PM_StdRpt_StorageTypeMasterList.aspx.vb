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

Public Class PM_StdRpt_StorageTypeMasterList : Inherits Page

    Protected RptSelect As UserControl

    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objPMSetup As New agri.PM.clsSetup()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblLocation As Label
    
    Protected WithEvents txtStorageTypeCode As TextBox
    Protected WithEvents ddlProduct As DropDownList
    Protected WithEvents ddlStatus As DropDownList

    Protected WithEvents PrintPrev As ImageButton

    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strLangCode As String
    Dim strParam As String
    Dim strDateFormat As String

    Dim intCnt As Integer
    Dim intErrNo As Integer
    Dim dr As DataRow
	
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String
	
    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strLangCode = Session("SS_LANGCODE")
        strDateFormat = Session("SS_DATEFMT")
 		
	        strLocType = Session("SS_LOCTYPE")
	    
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
                onload_GetLangCap()
                BindDropDownList()
                BindStatusList()
            End If
        End If
    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim ucTrMthYr As HtmlTableRow

        ucTrMthYr = RptSelect.FindControl("SelLocation")
        ucTrMthYr.Visible = False
        ucTrMthYr = RptSelect.FindControl("TrMthYr")
        ucTrMthYr.Visible = False
        ucTrMthYr = RptSelect.FindControl("SelDecimal")
        ucTrMthYr.Visible = False
        
    End Sub

    Protected Sub BindDropDownList()
        ddlProduct.Items.Add(New ListItem("All", ""))
        ddlProduct.Items.Add(New ListItem(objPMSetup.mtdGetProductCode(objPMSetup.EnumProductCode.CPO), objPMSetup.EnumProductCode.CPO))
        ddlProduct.Items.Add(New ListItem(objPMSetup.mtdGetProductCode(objPMSetup.EnumProductCode.PK), objPMSetup.EnumProductCode.PK))
    End Sub
    
    Sub BindStatusList()
        ddlStatus.Items.Add(New ListItem(objPMSetup.mtdGetStorageTypeStatus(objPMSetup.EnumStorageTypeStatus.All), objPMSetup.EnumStorageTypeStatus.All))
        ddlStatus.Items.Add(New ListItem(objPMSetup.mtdGetStorageTypeStatus(objPMSetup.EnumStorageTypeStatus.Active), objPMSetup.EnumStorageTypeStatus.Active))
        ddlStatus.Items.Add(New ListItem(objPMSetup.mtdGetStorageTypeStatus(objPMSetup.EnumStorageTypeStatus.Deleted), objPMSetup.EnumStorageTypeStatus.Deleted))
    End Sub
    
    Sub onload_GetLangCap()
        GetEntireLangCap()

        lblLocation.Text = GetCaption(objLangCap.EnumLangCap.Location)
    End Sub

    Sub GetEntireLangCap()
        Dim strOpCode_BussTerm As String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim strAccMonth As String
        Dim strAccYear As String

        strAccMonth = Session("SS_PMACCMONTH")
        strAccYear = Session("SS_PMACCYEAR")
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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PM_STDRPT_MONTHLY_PROD_SUM_CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/PM_StdRpt_Selection.aspx")
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
        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String

        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        
        Dim strStorageTypeCode As String
        Dim strProduct As String
        Dim strStatus As String

        tempRpt = RptSelect.FindControl("lstRptName")
        strRptID = Trim(tempRpt.SelectedItem.Value)
        strRptName = Trim(tempRpt.SelectedItem.Text)
        tempDec = RptSelect.FindControl("lstDecimal")
        strDec = Trim(tempDec.SelectedItem.Value)

        strStorageTypeCode = Server.URLEncode(Trim(txtStorageTypeCode.Text))
        strProduct = Trim(ddlProduct.SelectedItem.Value)
        strStatus = Server.URLEncode(Trim(ddlStatus.SelectedItem.Value))
        
        Response.Write("<Script Language=""JavaScript"">window.open(""PM_StdRpt_StorageTypeMasterListPreview.aspx?Type=Print&CompName=" & strCompany & "&Location=" & strLocation & "&RptID=" & strRptID & "&RptName=" & strRptName & _
                       "&Decimal=" & strDec & "&strStorageTypeCode=" & strStorageTypeCode & "&strProduct=" & strProduct & "&strStatus=" & strStatus & _
                       "&lblLocation=" & lblLocation.Text & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub
End Class
