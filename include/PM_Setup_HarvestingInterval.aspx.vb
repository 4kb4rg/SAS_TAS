Imports System
Imports System.Data
Imports System.Collections 
Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic

Public Class PM_Setup_HarvestingInterval : Inherits Page

    Protected WithEvents EventData As DataGrid            
    Protected WithEvents SortExpression As Label
    Protected WithEvents lblErrMessage As Label    
    Protected WithEvents sortcol As Label 

    Protected objPM As New agri.PM.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Dim strOppCd_GET As String = "PM_CLSSETUP_HAINTERVAL_GET"    
    Dim strOppCd_UPD As String = "PM_CLSSETUP_HAINTERVAL_UPD"
    Dim objDataSet As New Object()
    Dim objLangCapDs As New Object()
    Dim intErrNo As Integer
    Dim strParam As String = ""
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intPMAR As Integer
    Dim strAccMonth As String
    Dim strAccYear As String
	
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String
	
    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intPMAR = Session("SS_PMAR")
        strAccMonth = Session("SS_PMACCMONTH")
        strAccYear = Session("SS_PMACCYEAR")
	    
        strLocType = Session("SS_LOCTYPE")
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMHarvestingInterval), intPMAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            If SortExpression.Text = "" Then
                SortExpression.Text = "PM_HAINTERVAL.IntervalID"
                sortcol.Text = "ASC"
            End If
            If Not Page.IsPostBack Then                
                BindGrid()
            End If
        End If
    End Sub

    Sub onload_GetLangCap()

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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PM_SETUP_HAINTERVAL_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_HarvestingInterval.aspx")
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


    Sub BindGrid()
        Dim dsData As DataSet

        dsData = LoadData()      
        EventData.DataSource = dsData
        EventData.DataBind()
    End Sub

    Protected Function LoadData() As DataSet

        Dim UpdateBy As String
        Dim strParam As String
        Dim intCnt As Integer

        strParam = "ORDER BY " & SortExpression.Text & " " & sortcol.Text & "|"

        Try
            intErrNo = objPM.mtdGetHaInterval(strOppCd_GET, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_HAINTERVAL&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_HarvestingInterval.aspx")
        End Try

        Return objDataSet

    End Function

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)

        EventData.EditItemIndex = CInt(E.Item.ItemIndex)
        BindGrid()

    End Sub

    Sub DEDR_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)        
        Dim EditText As TextBox
        Dim DayFrom As String
        Dim DayTo As String
        Dim IntervalID As String
        
        EditText = E.Item.FindControl("txtDayFrom")
        DayFrom = EditText.Text
        EditText = E.Item.FindControl("txtDayTo")
        DayTo = EditText.Text
        EditText = E.Item.FindControl("txtIntervalID")
        IntervalID = EditText.Text

        strParam =  IntervalID & "|" & DayFrom & "|" & DayTo

        Try
            intErrNo = objPM.mtdUpdHaInterval(strOppCd_UPD, _
                                                    strUserId, _
                                                    strParam)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UPDATE_HAINTERVAL&errmesg=" & lblErrMessage.Text & "&redirect=PM_Setup_HarvestingInterval.aspx")
        End Try

        EventData.EditItemIndex = -1
        BindGrid()

    End Sub

    Sub DEDR_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        EventData.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        sortcol.Text = IIf(sortcol.Text = "ASC", "DESC", "ASC")
        BindGrid()
    End Sub

    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("../../menu/menu_PDSetup_page.aspx")
    End Sub


End Class
