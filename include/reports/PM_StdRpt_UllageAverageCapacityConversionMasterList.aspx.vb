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

Public Class PM_StdRpt_UllageAverageCapacityConversionMasterList : Inherits Page

    Protected RptSelect As UserControl

    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objPMSetup As New agri.PM.clsSetup()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblLocation As Label
    
    Protected WithEvents txtUVTableCode As TextBox
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

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strLangCode = Session("SS_LANGCODE")
        strDateFormat = Session("SS_DATEFMT")
        
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
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

    End Sub

    Sub BindStatusList()
        ddlStatus.Items.Add(New ListItem(objPMSetup.mtdGetUllageAverageCapacityConversionStatus(objPMSetup.EnumUllageAverageCapacityConversionStatus.All), objPMSetup.EnumUllageAverageCapacityConversionStatus.All))
        ddlStatus.Items.Add(New ListItem(objPMSetup.mtdGetUllageAverageCapacityConversionStatus(objPMSetup.EnumUllageAverageCapacityConversionStatus.Active), objPMSetup.EnumUllageAverageCapacityConversionStatus.Active))
        ddlStatus.Items.Add(New ListItem(objPMSetup.mtdGetUllageAverageCapacityConversionStatus(objPMSetup.EnumUllageAverageCapacityConversionStatus.Deleted), objPMSetup.EnumUllageAverageCapacityConversionStatus.Deleted))
    End Sub
    
    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String

        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        
        Dim strUVTableCode As String
        Dim strStatus As String

        tempRpt = RptSelect.FindControl("lstRptName")
        strRptID = Trim(tempRpt.SelectedItem.Value)
        strRptName = Trim(tempRpt.SelectedItem.Text)
        tempDec = RptSelect.FindControl("lstDecimal")
        strDec = Trim(tempDec.SelectedItem.Value)

        strUVTableCode = Server.URLEncode(Trim(txtUVTableCode.Text))
        strStatus = Server.URLEncode(Trim(ddlStatus.SelectedItem.Value))
        
        Response.Write("<Script Language=""JavaScript"">window.open(""PM_StdRpt_UllageAverageCapacityConversionMasterListPreview.aspx?Type=Print&CompName=" & strCompany & "&Location=" & strLocation & "&RptID=" & strRptID & "&RptName=" & strRptName & _
                       "&Decimal=" & strDec & "&strUVTableCode=" & strUVTableCode & "&strStatus=" & strStatus &  """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub
End Class
