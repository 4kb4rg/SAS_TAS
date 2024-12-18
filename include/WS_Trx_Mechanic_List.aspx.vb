
Imports System
Imports System.Data
Imports System.Collections 
Imports System.Data.SqlClient
Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic


Public Class WS_Mechanic : Inherits Page

    Protected WithEvents dgMechanic As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents sortcol As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents SortExpression As Label
    Protected WithEvents lblCompanyTag As Label
    Protected WithEvents lbllocationTag As Label
    Protected WithEvents lblCompName As Label
    Protected WithEvents lblLocName As Label
    Protected WithEvents lbldatedisp As Label

    Protected objWSTx As New agri.WS.clsTrx()
    Dim objHRTx As New agri.HR.clsTrx()
    Dim objHRSt As New agri.HR.clsSetup()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLoc As New agri.Admin.clsLoc()


    Dim strMechanicList_GET As String = "WS_CLSTRX_MECHANIC_LIST_GET"

    Dim objDataSet As New Dataset()
    Dim objLangCapDs As New DataSet()
    Dim intErrNo As Integer
    Dim strParam As String = ""

    Dim dt As Date

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intWSAR As Integer
    Dim strLocType as String


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        intWSAR = Session("SS_WSAR")
        strLocType = Session("SS_LOCTYPE")


        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSMechanicHour), intWSAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else

            If SortExpression.Text = "" Then
                SortExpression.Text = "Mst.Empcode"
            End If

            onload_GetLangCap()
            If Not Page.IsPostBack Then
                BindGrid()
            End If

            dt = Request.QueryString("dt")

            lblCompName.Text = strCompany
            lblLocName.Text = strLocation
            lbldatedisp.Text = objGlobal.GetLongDate(dt)
        End If
    End Sub


    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblCompanyTag.Text = GetCaption(objLangCap.EnumLangCap.Company)
        lbllocationTag.Text = GetCaption(objLangCap.EnumLangCap.Location)

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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_TRX_MECH_LIST_GET_LANGCAP&errmesg=" & Exp.ToString() & "&redirect=ws/trx/WS_TRX_Mechanic_List.aspx")
        End Try

    End Sub


    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

       For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If strLocType = objLoc.EnumLocType.Mill then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                end if
                Exit For
            End If
        Next
    End Function


    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        sortcol.Text = IIf(sortcol.Text = "ASC", "DESC", "ASC")
        BindGrid()
    End Sub

    Sub BindGrid()
        Dim PageNo As Integer

        dgMechanic.DataSource = LoadData()
        dgMechanic.DataBind()

    End Sub

    Protected Function LoadData() As DataSet

        'Dim StockCode As String
        'Dim Desc As String
        'Dim UpdateBy As String
        'Dim srchStatus As String
        'Dim strParam As String
        'Dim SearchStr As String
        'Dim sortitem As String

        'strParam = objHRSt.EnumMechanicIndicator.Yes & "|" & objHRTx.EnumEmpStatus.Active & "|" & _
        '           SortExpression.Text & "|" & sortcol.Text
        'Try
        '    intErrNo = objWSTx.mtdGetMechanicList(strCompany, _
        '                                        strLocation, _
        '                                        strUserId, _
        '                                        strAccMonth, _
        '                                        strAccYear, _
        '                                        strMechanicList_GET, _
        '                                        strParam, _
        '                                        objDataSet)
        'Catch Exp As System.Exception
        '    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_MECHANICLIST&errmesg=" & Exp.ToString() & "&redirect=WS/Trx/WS_TRX_Mechanic_List.aspx")
        'End Try
        'Return objDataSet
    End Function

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim lnk As LinkButton
        Dim EmpCode As String
        lnk = E.Item.FindControl("EmpCode")
        EmpCode = lnk.Text
        Response.Redirect("WS_Trx_MechanicHour_Detail.aspx?id=" & Trim(EmpCode) & "&dt=" & Trim(dt))
    End Sub

    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("WS_Trx_Mechanic_Date.aspx")
    End Sub


End Class
