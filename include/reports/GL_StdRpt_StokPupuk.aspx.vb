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

Public Class GL_StdRpt_StokPupuk : Inherits Page

    Protected RptSelect As UserControl

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblBlk As Label
    Protected WithEvents lblActivity As Label
    Protected WithEvents lblSubActivity As Label
    Protected WithEvents lblProdModel As Label    
    Protected WithEvents lblCode As Label
    Protected WithEvents lblHidCostLevel As Label    
    Protected WithEvents ddlBlk As DropDownList
    Protected WithEvents ddlActivity As DropDownList
    Protected WithEvents ddlActivityTo As DropDownList
    Protected WithEvents ddlSubActivity As DropDownList
    Protected WithEvents ddlProdModel As DropDownList
    Protected WithEvents ddlSubActivityTo As DropDownList

    Protected WithEvents PrintPrev As ImageButton

    Protected WithEvents Table2 As HTMLTable
    Protected WithEvents Table3 As HTMLTable

    Dim TrMthYr As HtmlTableRow

    Dim objGL As New agri.GL.clsReport()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objINSetup As New agri.IN.clsSetup()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfigsetting As Integer
    Dim dr As DataRow
    Dim intCnt As Integer
    Dim intErrNo As Integer
	
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String
	

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        Dim tempDateFrom As TextBox
        Dim tempDateTo As TextBox
        Dim intCnt As Integer
    
        Table2.Visible = False
        Table3.Visible = True

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")     
        Else
            onload_GetLangCap()
            If Not Page.IsPostBack Then
                BindActivity("00")
                BindSubActivity("011")
                BindSubActivityTo("013")
                BindSubBlk("")
                BindProdModel("")
            End If
        End If
    End Sub
    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim ucTrMthYr As HtmlTableRow
        Dim ucTrDecimal As HtmlTableRow

        ucTrMthYr = RptSelect.FindControl("TrMthYr")
        ucTrMthYr.Visible = True

    End Sub

    Sub BindActivity(Optional ByVal pv_strCode as String = "")
        Dim strOpCode As String = "GL_CLSSETUP_ACTIVITY_LIST_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim objActDs As New DataSet
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intSelectIndex As Integer = 0

        strParam =  "||" & objGLSetup.EnumActStatus.Active & "||Act.ActCode|"
        Try
            intErrNo = objGLSetup.mtdGetActivity(strOpCode, _
                                                 strParam, _
                                                 objActDs, _
                                                 False)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_ACTIVITY_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objActDs.Tables(0).Rows.Count - 1
            objActDs.Tables(0).Rows(intCnt).Item("ActCode") = Trim(objActDs.Tables(0).Rows(intCnt).Item("ActCode"))
            objActDs.Tables(0).Rows(intCnt).Item("Description") = objActDs.Tables(0).Rows(intCnt).Item("ActCode") & " (" & Trim(objActDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objActDs.Tables(0).Rows(intCnt).Item("ActCode") = Trim(pv_strCode) Then
                intSelectIndex = intCnt + 1
            End If
        Next

        dr = objActDs.Tables(0).NewRow()
        dr("ActCode") = ""
        dr("Description") = "Select " & lblActivity.Text
        objActDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlActivity.DataSource = objActDs.Tables(0)
        ddlActivity.DataValueField = "ActCode"
        ddlActivity.DataTextField = "Description"
        ddlActivity.DataBind()

        ddlActivity.SelectedIndex = intSelectIndex

        ddlActivityTo.DataSource = objActDs.Tables(0)
        ddlActivityTo.DataValueField = "ActCode"
        ddlActivityTo.DataTextField = "Description"
        ddlActivityTo.DataBind()

        ddlActivityTo.SelectedIndex = intSelectIndex
    End Sub
    Sub BindSubActivity(Optional ByVal pv_strCode as String = "")
        Dim strOpCode As String = "GL_CLSSETUP_SUBACTIVITY_LIST_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim objActDs As New DataSet
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intSelectIndex As Integer = 0

        strParam =  "||" & objGLSetup.EnumSubActStatus.Active & "||SubAct.SubActCode|||" & Trim(ddlActivity.SelectedItem.Value)
        Try
            intErrNo = objGLSetup.mtdGetSubActivity(strOpCode, _
                                                 strParam, _
                                                 objActDs, _
                                                 False)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_SUBACTIVITY_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objActDs.Tables(0).Rows.Count - 1
            objActDs.Tables(0).Rows(intCnt).Item("SubActCode") = Trim(objActDs.Tables(0).Rows(intCnt).Item("SubActCode"))
            objActDs.Tables(0).Rows(intCnt).Item("Description") = objActDs.Tables(0).Rows(intCnt).Item("SubActCode") & " (" & Trim(objActDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objActDs.Tables(0).Rows(intCnt).Item("SubActCode") = Trim(pv_strCode) Then
                intSelectIndex = intCnt + 1
            End If
        Next

        dr = objActDs.Tables(0).NewRow()
        dr("SubActCode") = ""
        dr("Description") = "Select " & lblSubActivity.Text
        objActDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlSubActivity.DataSource = objActDs.Tables(0)
        ddlSubActivity.DataValueField = "SubActCode"
        ddlSubActivity.DataTextField = "Description"
        ddlSubActivity.DataBind()

        ddlSubActivity.SelectedIndex = intSelectIndex

    End Sub
    Sub BindSubActivityTo(Optional ByVal pv_strCode as String = "")
        Dim strOpCode As String = "GL_CLSSETUP_SUBACTIVITY_LIST_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim objActDs As New DataSet
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intSelectIndexTo As Integer = 0
       

        strParam =  "||" & objGLSetup.EnumSubActStatus.Active & "||SubAct.SubActCode|||" & Trim(ddlActivityTo.SelectedItem.Value)
        Try
            intErrNo = objGLSetup.mtdGetSubActivity(strOpCode, _
                                                 strParam, _
                                                 objActDs, _
                                                 False)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_SUBACTIVITY_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objActDs.Tables(0).Rows.Count - 1
            objActDs.Tables(0).Rows(intCnt).Item("SubActCode") = Trim(objActDs.Tables(0).Rows(intCnt).Item("SubActCode"))
            objActDs.Tables(0).Rows(intCnt).Item("Description") = objActDs.Tables(0).Rows(intCnt).Item("SubActCode") & " (" & Trim(objActDs.Tables(0).Rows(intCnt).Item("Description")) & ")"

            If objActDs.Tables(0).Rows(intCnt).Item("SubActCode") = Trim(pv_strCode) Then
                intSelectIndexTo = intCnt + 1
            End If
        Next

        dr = objActDs.Tables(0).NewRow()
        dr("SubActCode") = ""
        dr("Description") = "Select " & lblSubActivity.Text
        objActDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlSubActivityTo.DataSource = objActDs.Tables(0)
        ddlSubActivityTo.DataValueField = "SubActCode"
        ddlSubActivityTo.DataTextField = "Description"
        ddlSubActivityTo.DataBind()

        ddlSubActivityTo.SelectedIndex = intSelectIndexTo
    End Sub
    Sub BindSubBlk(Optional ByVal pv_strCode as String = "")
        Dim strOpCode As String = "GL_CLSSETUP_SUBBLOCK_LIST_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim objActDs As New DataSet
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intSelectIndex As Integer = 0

        strParam =  "||" & objGLSetup.EnumSubBlockStatus.Active & "||Sub.SubBlkCode|||"
        Try
            intErrNo = objGLSetup.mtdGetSubBlock(strOpCode, _
                                                 strLocation, _
                                                 strParam, _
                                                 objActDs, _
                                                 False)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_SUBBLOCK_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objActDs.Tables(0).Rows.Count - 1
            objActDs.Tables(0).Rows(intCnt).Item("SubBlkCode") = Trim(objActDs.Tables(0).Rows(intCnt).Item("SubBlkCode"))
            objActDs.Tables(0).Rows(intCnt).Item("Description") = objActDs.Tables(0).Rows(intCnt).Item("SubBlkCode") & " (" & Trim(objActDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objActDs.Tables(0).Rows(intCnt).Item("SubBlkCode") = Trim(pv_strCode) Then
                intSelectIndex = intCnt + 1
            End If
        Next

        dr = objActDs.Tables(0).NewRow()
        dr("SubBlkCode") = ""
        dr("Description") = "Select " & lblBlk.Text
        objActDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlBlk.DataSource = objActDs.Tables(0)
        ddlBlk.DataValueField = "SubBlkCode"
        ddlBlk.DataTextField = "Description"
        ddlBlk.DataBind()

        ddlBlk.SelectedIndex = intSelectIndex

    End Sub
    Sub BindProdModel(Optional ByVal pv_strCode as String = "")
        Dim strOpCode As String = "IN_CLSSETUP_PRODMODEL_LIST_GET"
        Dim strParam As String
        Dim strSrch As String
        Dim intErrNo As Integer
        Dim objActDs As New DataSet
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intSelectIndex As Integer = 0
        
        strSrch = "AND PModel.Status ='" & objINSetup.EnumProductModelStatus.Active & "'"

        strParam =  "|" & strSrch
        Try
            intErrNo = objINSetup.mtdGetMasterList(strOpCode, _
                                                 strParam, _
                                                 objINSetup.EnumInventoryMasterType.ProductModel, _
                                                 objActDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_SUBACTIVITY_ACTCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objActDs.Tables(0).Rows.Count - 1
            objActDs.Tables(0).Rows(intCnt).Item("ProdModelCode") = Trim(objActDs.Tables(0).Rows(intCnt).Item("ProdModelCode"))
            objActDs.Tables(0).Rows(intCnt).Item("Description") = objActDs.Tables(0).Rows(intCnt).Item("ProdModelCode") & " (" & Trim(objActDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objActDs.Tables(0).Rows(intCnt).Item("ProdModelCode") = Trim(pv_strCode) Then
                intSelectIndex = intCnt + 1
            End If
        Next

        dr = objActDs.Tables(0).NewRow()
        dr("ProdModelCode") = ""
        dr("Description") = "Select " & lblProdModel.Text
        objActDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlProdModel.DataSource = objActDs.Tables(0)
        ddlProdModel.DataValueField = "ProdModelCode"
        ddlProdModel.DataTextField = "Description"
        ddlProdModel.DataBind()

        ddlProdModel.SelectedIndex = intSelectIndex

    End Sub
    Sub onSelect_ActCode (ByVal sender As Object, ByVal e As EventArgs)
        BindSubActivity("")
    End Sub
    Sub onSelect_ActCodeTo (ByVal sender As Object, ByVal e As EventArgs)
        BindSubActivityTo("")
    End Sub
    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)

        Dim strddlAccMth As String
        Dim strddlAccYr As String
        Dim strRptId As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String

        Dim strParam As String
        Dim strDateSetting As String
        Dim objSysCfgDs As New Object()

        Dim strSrchProdModel As String
        Dim strSrchBlk As String
        Dim strSrchActivity As String
        Dim strSrchSubAct As String
        Dim strSrchActivityTo As String
        Dim strSrchSubActTo As String
        Dim strSrchAccMonth As String

        Dim enSrchProdModel As String
        Dim enSrchBlk As String
        Dim enSrchActivity As String
        Dim enSrchSubAct As String
        Dim enSrchAccMonth As String
        Dim enSrchActivityTo As String
        Dim enSrchSubActTo As String
        Dim intCnt As Integer
        Dim ddlist As DropDownList
        Dim rdblist As RadioButtonList
        Dim tempUserLoc As HtmlInputHidden
        Dim templblUL As Label
        Dim strTemp As String

        ddlist = RptSelect.FindControl("lstAccMonth")
        strddlAccMth = Trim(ddlist.SelectedItem.Value)

        strSrchAccMonth = strddlAccMth

        If Trim(strddlAccMth) <> "1" Then
            For intCnt = 1 To CInt(strddlAccMth)
                strTemp = CStr(intCnt) & "','" & strTemp
            Next
            strddlAccMth = strTemp
        End If

        ddlist = RptSelect.FindControl("lstAccYear")
        strddlAccYr = Trim(ddlist.SelectedItem.Value)

        ddlist = RptSelect.FindControl("lstRptName")
        strRptId = Trim(ddlist.SelectedItem.Value)

        ddlist = RptSelect.FindControl("lstRptName")
        strRptName = Trim(ddlist.SelectedItem.Text)

        ddlist = RptSelect.FindControl("lstDecimal")
        strDec = Trim(ddlist.SelectedItem.Value)

        tempUserLoc = RptSelect.FindControl("hidUserLoc")

        strUserLoc = Trim(tempUserLoc.Value)
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

       If Right(strUserLoc, 1) = "," Then
                strUserLoc = Left(strUserLoc, Len(strUserLoc) - 1)
       Else
                strUserLoc = Trim(strUserLoc)
       End If

       If Right(strddlAccMth, 2) = ",'" Then
                strddlAccMth = Left(strddlAccMth, Len(strddlAccMth) - 3)
       Else
                strddlAccMth = Trim(strddlAccMth)
       End If

        strSrchProdModel = Trim(ddlProdModel.SelectedItem.Value)
        strSrchBlk = Trim(ddlBlk.SelectedItem.Value)
        strSrchActivity = Trim(ddlActivity.SelectedItem.Value)
        strSrchSubAct = Trim(ddlSubActivity.SelectedItem.Value)
        strSrchActivityTo = Trim(ddlActivityTo.SelectedItem.Value)
        strSrchSubActTo = Trim(ddlSubActivityTo.SelectedItem.Value)


        enSrchProdModel = Server.UrlEncode(strSrchProdModel)
        enSrchBlk = Server.UrlEncode(strSrchBlk)
        enSrchActivity = Server.UrlEncode(strSrchActivity)
        enSrchSubAct = Server.UrlEncode(strSrchSubAct)
        enSrchActivityTo = Server.UrlEncode(strSrchActivityTo)
        enSrchSubActTo = Server.UrlEncode(strSrchSubActTo)


        Response.Write("<Script Language=""JavaScript"">window.open(""GL_StdRpt_StokPupukPreview.aspx?Type=Print&CompName=" & strCompany & _
                       "&SelLocation=" & strUserLoc & _
                       "&DDLAccMth=" & strddlAccMth & _
                       "&SelAccMth=" & strSrchAccMonth & _
                       "&DDLAccYr=" & strddlAccYr & _
                       "&RptId=" & strRptId & _
                       "&RptName=" & strRptName & _
                       "&Decimal=" & strDec & _
                       "&SelBlk=" & enSrchBlk & _
                       "&SelProdModel=" & enSrchProdModel & _
                       "&SelAct=" & enSrchActivity & _
                       "&SelActTo=" & enSrchActivityTo & _
                       "&SelSubAct=" & enSrchSubAct & _
                       "&SelSubActTo=" & enSrchSubActTo & _
                       """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, " & _
                       "location=no"");</Script>")


    End Sub

    Sub onload_GetLangCap()

        GetEntireLangCap()
        lblBlk.Text = GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.Text
        lblActivity.Text = GetCaption(objLangCap.EnumLangCap.Activity) & lblCode.Text
        lblSubActivity.Text = GetCaption(objLangCap.EnumLangCap.SubAct) & lblCode.Text
        lblProdModel.Text = GetCaption(objLangCap.EnumLangCap.ProdModel) & lblCode.Text

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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_REPORTS_VEHICLEEXPENSEDETAIL_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/GL_StdRpt_Selection.aspx")
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


End Class
