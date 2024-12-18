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


Public Class PD_StdRpt_ProdStat : Inherits Page

    Protected RptSelect As UserControl

    Dim objPD As New agri.PD.clsReport()
    Dim objPDTrx As New agri.PD.clsTrx()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Protected WithEvents lblDateTo As Label
    Protected WithEvents lblDateFrom As Label
    Protected WithEvents lblTracker As Label

    Protected WithEvents lblLocation As Label
    Protected WithEvents txtDateFrom As TextBox
    Protected WithEvents txtDateTo As TextBox

    Protected WithEvents lblBlkGrp As Label
    Protected WithEvents lblBlk As Label
    Protected WithEvents lblSubBlk As Label

    Protected WithEvents ddlLevel As DropDownList
    Protected WithEvents ddlBlkGrp As DropDownList
    Protected WithEvents ddlBlk As DropDownList
    Protected WithEvents ddlSubBlk As DropDownList

    Protected WithEvents TrBlkGrp As HtmlTableRow
    Protected WithEvents TrBlk As HtmlTableRow
    Protected WithEvents TrSubBlk As HtmlTableRow

    Protected WithEvents PrintPrev As ImageButton

    Dim objLangCapDs As New Dataset()

    Dim TrMthYr As HtmlTableRow
    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfigSetting As Integer
    Dim strUserLoc As String
    Dim intCnt As Integer
    Dim intErrNo As Integer
    Dim strDateFormat As String
    Dim strBlockTag As String
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim objGLSetup As New agri.GL.clsSetup()
	Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_PDACCMONTH")
        strAccYear = Session("SS_PDACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigSetting = Session("SS_CONFIGSETTING")
        strDateFormat = Session("SS_DATEFMT")
        strLocation = Session("SS_LOCATION")
   
	    strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
                onload_GetLangCap()
                BindBlkGrp("")
                BindBlk("")
                BindSubBlk("")
                BindLevel()
            End If

            If Trim(ddlLevel.SelectedItem.Value) = "1" Then
                TrBlkGrp.Visible = True
                TrBlk.Visible = False
                TrSubBlk.Visible = False
                BindBlk("")
                BindSubBlk("")
            ElseIf Trim(ddlLevel.SelectedItem.Value) = "2" Then
                TrBlkGrp.Visible = False
                TrBlk.Visible = True
                TrSubBlk.Visible = False
                BindBlkGrp("")
                BindSubBlk("")
            ElseIf Trim(ddlLevel.SelectedItem.Value) = "3" Then
                TrBlkGrp.Visible = False
                TrBlk.Visible = False
                TrSubBlk.Visible = True
                BindBlkGrp("")
                BindBlk("")
            End If           
        End If
    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim ucTrMthYr As HtmlTableRow

        ucTrMthYr = RptSelect.FindControl("TrMthYr")
        ucTrMthYr.Visible = True

        If Page.IsPostBack Then
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        lblBlkGrp.Text = GetCaption(objLangCap.EnumLangCap.BlockGrp) & " Code "
        lblBlk.Text = GetCaption(objLangCap.EnumLangCap.Block) & " Code "
        lblSubBlk.Text = GetCaption(objLangCap.EnumLangCap.SubBlock) & " Code "
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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PWSYSTEM_CLSLANGCAP_BUSSTERM_GET&errmesg=" & Exp.ToString() & "&redirect=../en/reports/IN_StdRpt_Selection.aspx")
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

    Function CheckDate(ByVal strDate As String, ByRef strErrMsg As String) As String
        Dim strDateSetting As String
        Dim objSysCfgDs As DataSet
        Dim objDateFormat As String
        Dim strValidDate As String
        
        CheckDate = ""
        strErrMsg = ""
        If Not strDate = "" Then
            If objGlobal.mtdValidInputDate(strDateFormat, strDate, objDateFormat, strValidDate) = True Then
                CheckDate = strValidDate
            Else
                strErrMsg = "Date Entered should be in the format " & objDateFormat & "."
            End If
        End If
    End Function
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_SUBBLOCK_LIST_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        For intCnt = 0 To objActDs.Tables(0).Rows.Count - 1
            objActDs.Tables(0).Rows(intCnt).Item("SubBlkCode") = Trim(objActDs.Tables(0).Rows(intCnt).Item("SubBlkCode"))
            objActDs.Tables(0).Rows(intCnt).Item("Description") = objActDs.Tables(0).Rows(intCnt).Item("SubBlkCode") & " (" & Trim(objActDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objActDs.Tables(0).Rows(intCnt).Item("SubBlkCode") = Trim(pv_strCode) Then
                intSelectIndex = intCnt + 1
            End If
        Next

        dr = objActDs.Tables(0).NewRow()
        dr("SubBlkCode") = "%"
        dr("Description") = "Select " & lblSubBlk.Text
        objActDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlSubBlk.DataSource = objActDs.Tables(0)
        ddlSubBlk.DataValueField = "SubBlkCode"
        ddlSubBlk.DataTextField = "Description"
        ddlSubBlk.DataBind()

        ddlSubBlk.SelectedIndex = intSelectIndex

    End Sub
    Sub BindBlkGrp(Optional ByVal pv_strCode as String = "")
        Dim strOpCode As String = "GL_CLSSETUP_BLOCKGROUP_LIST_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim objActDs As New DataSet
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intSelectIndex As Integer = 0

        strParam =  " order by blk.blkgrpcode asc " & "|" & _
                    " and blk.status ='" & objGLSetup.EnumBlockStatus.Active & "'"

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCode, _
                                                 strParam, _
                                                 objGLSetup.EnumGLMasterType.BlkGrp, _
                                                 objActDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_SUBBLOCK_LIST_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        For intCnt = 0 To objActDs.Tables(0).Rows.Count - 1
            objActDs.Tables(0).Rows(intCnt).Item("BlkGrpCode") = Trim(objActDs.Tables(0).Rows(intCnt).Item("BlkGrpCode"))
            objActDs.Tables(0).Rows(intCnt).Item("Description") = objActDs.Tables(0).Rows(intCnt).Item("BlkGrpCode") & " (" & Trim(objActDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objActDs.Tables(0).Rows(intCnt).Item("BlkGrpCode") = Trim(pv_strCode) Then
                intSelectIndex = intCnt + 1
            End If
        Next

        dr = objActDs.Tables(0).NewRow()
        dr("BlkGrpCode") = "%"
        dr("Description") = "Select " & lblBlkGrp.Text
        objActDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlBlkGrp.DataSource = objActDs.Tables(0)
        ddlBlkGrp.DataValueField = "BlkGrpCode"
        ddlBlkGrp.DataTextField = "Description"
        ddlBlkGrp.DataBind()

        ddlBlkGrp.SelectedIndex = intSelectIndex

    End Sub
    Sub BindBlk(Optional ByVal pv_strCode as String = "")
        Dim strOpCode As String = "GL_CLSSETUP_BLOCK_LIST_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim objActDs As New DataSet
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intSelectIndex As Integer = 0

        strParam =  "||" & objGLSetup.EnumBlockStatus.Active & "||blk.BlkCode|||"
        Try
            intErrNo = objGLSetup.mtdGetBlock(strOpCode, _
                                                 strLocation, _
                                                 strParam, _
                                                 objActDs, _
                                                 False)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_SUBBLOCK_LIST_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        For intCnt = 0 To objActDs.Tables(0).Rows.Count - 1
            objActDs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(objActDs.Tables(0).Rows(intCnt).Item("BlkCode"))
            objActDs.Tables(0).Rows(intCnt).Item("Description") = objActDs.Tables(0).Rows(intCnt).Item("BlkCode") & " (" & Trim(objActDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objActDs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(pv_strCode) Then
                intSelectIndex = intCnt + 1
            End If
        Next

        dr = objActDs.Tables(0).NewRow()
        dr("BlkCode") = "%"
        dr("Description") = "Select " & lblBlk.Text
        objActDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlBlk.DataSource = objActDs.Tables(0)
        ddlBlk.DataValueField = "BlkCode"
        ddlBlk.DataTextField = "Description"
        ddlBlk.DataBind()

        ddlBlk.SelectedIndex = intSelectIndex

    End Sub
    Sub BindLevel()
        ddlLevel.Items.Clear()
        ddlLevel.Items.Add(New ListItem(lblBlkGrp.Text, "1"))
        ddlLevel.Items.Add(New ListItem(lblBlk.Text, "2"))
        ddlLevel.Items.Add(New ListItem(lblSubBlk.Text, "3"))
    End Sub
    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strYieldIDFrom As String
        Dim strYieldIDTo As String
        Dim strBlkType As String
        Dim strBlkGrp As String
        Dim strBlkCode As String
        Dim strSubBlkCode As String
        Dim strddlAccMth As String
        Dim strddlAccYr As String
        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String
        Dim strSupp As String
        Dim tempAccMth As DropDownList
        Dim tempAccYr As DropDownList
        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden

        Dim strDateFrom As String
        Dim strDateTo As String
        Dim tempDateFrom As TextBox
        Dim tempDateTo As TextBox
        Dim strDateSetting As String
        Dim strParam As String
        Dim strLblLocation As String

        Dim strSrchBlkGrp As String
        Dim strSrchBlk As String
        Dim strSrchSubBlk As String
        Dim strLevel As String

        Dim enSrchBlkGrp As String
        Dim enSrchBlk As String
        Dim enSrchSubBlk As String

        Dim strErrDateFrom As String
        Dim strErrDateTo As String

        strDateFrom = Trim(txtDateFrom.Text)
        strDateTo = Trim(txtDateTo.Text)

        If strDateFrom = "" Then
            lblDateFrom.Text = "<br> Please insert Date From"
            lblDateFrom.Visible = True
            Exit Sub
        End If
        If strDateTo = "" Then
            lblDateTo.Text = "<br> Please insert Date To"
            lblDateTo.Visible = True
            Exit Sub
        End If

        strLblLocation = Trim(lblLocation.Text)
        tempAccMth = RptSelect.FindControl("lstAccMonth")
        strddlAccMth = Trim(tempAccMth.SelectedItem.Value)
        tempAccYr = RptSelect.FindControl("lstAccYear")
        strddlAccYr = Trim(tempAccYr.SelectedItem.Value)

        tempRpt = RptSelect.FindControl("lstRptName")
        strRptID = Trim(tempRpt.SelectedItem.Value)
        strRptName = Trim(tempRpt.SelectedItem.Text)
        tempUserLoc = RptSelect.FindControl("hidUserLoc")
        strUserLoc = Trim(tempUserLoc.Value)
        tempDec = RptSelect.FindControl("lstDecimal")
        strDec = Trim(tempDec.SelectedItem.Value)

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


        strSrchBlkGrp = Trim(ddlBlkGrp.SelectedItem.Value)
        strSrchBlk = Trim(ddlBlk.SelectedItem.Value)
        strSrchSubBlk = Trim(ddlSubBlk.SelectedItem.Value)
        strLevel = Trim(ddlLevel.SelectedItem.Value) 

        enSrchBlkGrp = Server.UrlEncode(strSrchBlkGrp)
        enSrchBlk = Server.UrlEncode(strSrchBlk)
        enSrchSubBlk = Server.UrlEncode(strSrchSubBlk)

        strDateFrom = CheckDate(Trim(txtDateFrom.Text), strErrDateFrom)
        strDateTo = CheckDate(Trim(txtDateTo.Text), strErrDateTo)

        If strErrDateFrom = "" And strErrDateTo = "" Then
            lblDateFrom.visible = False
            lblDateTo.visible = False

            Response.Write("<Script Language=""JavaScript"">window.open(""PD_StdRpt_ProdStatPreview.aspx?Type=Print&CompName=" & strCompany & _
                        "&Location=" & strUserLoc & _
                        "&DDLAccMth=" & strddlAccMth & _
                        "&DDLAccYr=" & strddlAccYr & _
                        "&RptID=" & strRptID & _
                        "&RptName=" & strRptName & _              
                        "&lblBlkGrp=" & lblBlkGrp.Text & _
                        "&lblBlk=" & lblBlk.Text & _
                        "&lblSubBlk=" & lblSubBlk.Text & _
                        "&lblLocation=" & lblLocation.Text & _
                        "&Decimal=" & strDec & _
                        "&Level=" & strLevel & _
                        "&BlkGrp=" & enSrchBlkGrp & _
                        "&Blk=" & enSrchBlk & _
                        "&SubBlk=" & enSrchSubBlk & _
                        "&DateFrom=" & strDateFrom & _
                        "&DateTo=" & strDateTo & _
                        """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

        Else
            If strErrDateFrom = "" Then
                lblDateFrom.Visible = False
            Else
                lblDateFrom.Text = "<br>" & strErrDateFrom
                lblDateFrom.Visible = True
            End If
            
            If strErrDateTo = "" Then
                lblDateTo.Visible = False
            Else
                lblDateTo.Text = "<br>" & strErrDateTo
                lblDateTo.Visible = True
            End If
        End If

    End Sub
End Class
