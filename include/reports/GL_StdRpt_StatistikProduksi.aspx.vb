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

Public Class GL_StdRpt_StatistikProduksi : Inherits Page

    Protected RptSelect As UserControl

    Dim objGL As New agri.GL.clsReport()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents PrintPrev As ImageButton
    Protected WithEvents lblLevel As Label
    Protected WithEvents lblBlkGrp As Label
    Protected WithEvents lblBlk As Label
    Protected WithEvents lblSubBlk As Label  
    Protected WithEvents lblCode As Label
    Protected WithEvents lblHidCostLevel As Label    
    Protected WithEvents ddlLevel As DropDownList
    Protected WithEvents ddlBlkGrp As DropDownList
    Protected WithEvents ddlBlk As DropDownList
    Protected WithEvents ddlSubBlk As DropDownList
    Protected WithEvents TrBlkGrp As HtmlTableRow
    Protected WithEvents TrBlk As HtmlTableRow
    Protected WithEvents TrSubBlk As HtmlTableRow
    Protected WithEvents TrLoc As HtmlTableRow
    Protected WithEvents lblLocCode As Label 
    Protected WithEvents TrThnTnm As HtmlTableRow
    Protected WithEvents lblThnTnm As Label 
    Protected WithEvents ddlThnTnm As DropDownList

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLocTag As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfigsetting As Integer
    Dim intErrNo As Integer

    Dim objLangCapDs As New Object()
    Dim objDataSet As New Object()
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

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
            If Not Page.IsPostBack Then
                onload_GetLangCap()   
                BindBlkGrp("")
                BindBlk("")
                BindSubBlk("")
                BindThnTnm("")
                BindLevel()
            End If

            If Trim(ddlLevel.SelectedItem.Value) = "1" Then
                TrBlkGrp.Visible = True
                TrBlk.Visible = False
                TrSubBlk.Visible = False
                TrLoc.Visible = False
                TrThnTnm.Visible = False
                BindBlk("")
                BindSubBlk("")
            ElseIf Trim(ddlLevel.SelectedItem.Value) = "2" Then
                TrBlkGrp.Visible = False
                TrBlk.Visible = True
                TrSubBlk.Visible = False
                TrLoc.Visible = False
                TrThnTnm.Visible = False
                BindBlkGrp("")
                BindSubBlk("")
            ElseIf Trim(ddlLevel.SelectedItem.Value) = "3" Then
                TrBlkGrp.Visible = False
                TrBlk.Visible = False
                TrSubBlk.Visible = True
                TrLoc.Visible = False
                TrThnTnm.Visible = False
                BindBlkGrp("")
                BindBlk("")
            ElseIf Trim(ddlLevel.SelectedItem.Value) = "4" Then
                TrBlkGrp.Visible = False
                TrBlk.Visible = False
                TrSubBlk.Visible = False
                TrLoc.Visible = True
                TrThnTnm.Visible = False
                BindBlkGrp("")
                BindBlk("")
            ElseIf Trim(ddlLevel.SelectedItem.Value) = "5" Then
                TrBlkGrp.Visible = False
                TrBlk.Visible = False
                TrSubBlk.Visible = False
                TrLoc.Visible = False
                TrThnTnm.Visible = True
                BindBlkGrp("")
                BindBlk("")            
            End If
        End If
    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim ucTrMthYr As HtmlTableRow
        ucTrMthYr = RptSelect.FindControl("TrMthYr")
        ucTrMthYr.Visible = True
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblBlkGrp.Text = GetCaption(objLangCap.EnumLangCap.BlockGrp) & lblCode.Text
        lblBlk.Text = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text
        lblSubBlk.Text = GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.Text
        lblThnTnm.Text = GetCaption(objLangCap.EnumLangCap.Block)
        lblLocCode.Text = GetCaption(objLangCap.EnumLangCap.Location) & lblCode.Text
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


    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)

        Dim strddlAccMth As String
        Dim strddlAccYr As String
        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String
        Dim ddlDropDown As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden
        Dim strParam As String
        Dim objSysCfgDs As New Object()
        Dim strSrchBlkGrp As String
        Dim strSrchBlk As String
        Dim strSrchSubBlk As String
        Dim strLevel As String

        Dim strSrchAccMonth As String

        Dim enSrchBlkGrp As String
        Dim enSrchBlk As String
        Dim enSrchSubBlk As String
        Dim enLevel As String

        ddlDropDown = RptSelect.FindControl("lstAccMonth")
        strddlAccMth = Trim(ddlDropDown.SelectedItem.Value)
        ddlDropDown = RptSelect.FindControl("lstAccYear")
        strddlAccYr = Trim(ddlDropDown.SelectedItem.Value)
        ddlDropDown = RptSelect.FindControl("lstRptName")
        strRptID = Trim(ddlDropDown.SelectedItem.Value)
        strRptName = Trim(ddlDropDown.SelectedItem.Text)
        ddlDropDown = RptSelect.FindControl("lstDecimal")
        strDec = Trim(ddlDropDown.SelectedItem.Value)        
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

        strSrchBlkGrp = Trim(ddlBlkGrp.SelectedItem.Value)
        strSrchBlk = Trim(ddlBlk.SelectedItem.Value)
        strSrchSubBlk = Trim(ddlSubBlk.SelectedItem.Value)
        strLevel = Trim(ddlLevel.SelectedItem.Value)
        If Trim(ddlLevel.SelectedItem.Value) = "5" Then 
            strSrchBlk = Trim(ddlThnTnm.SelectedItem.Value)
        End If 
        enSrchBlkGrp = Server.UrlEncode(strSrchBlkGrp)
        enSrchBlk = Server.UrlEncode(strSrchBlk)
        enSrchSubBlk = Server.UrlEncode(strSrchSubBlk)
        enLevel = Server.UrlEncode(strLevel)
        
        Response.Write("<Script Language=""JavaScript"">window.open(""GL_StdRpt_StatistikProduksiPreview.aspx?CompName=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & strDec & _
                    "&DDLAccMth=" & strddlAccMth & "&DDLAccYr=" & strddlAccYr & "&lblLocation=" & strLocTag & _
                    "&SelBlkGrp=" & enSrchBlkGrp & _
                    "&SelBlk=" & enSrchBlk & _
                    "&SelSubBlk=" & enSrchSubBlk & _
                    "&Level=" & enLevel & _
                    """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

    End Sub

    Sub BindSubBlk(Optional ByVal pv_strCode as String = "")
        Dim strOpCode As String = "GL_CLSSETUP_SUBBLOCK_LIST_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim objActDs As New DataSet
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intSelectIndex As Integer = 0

        strParam =  " order by Sub.subblkcode asc " & "|" & _
                    " and Sub.status ='" & objGLSetup.EnumSubBlockStatus.Active & "' and Sub.loccode = '" & trim(strLocation) & "' and sub.subblktype = '" & objGLSetup.EnumPlantedAreaBlkType.Mature & "' "

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCode, _
                                                 strParam, _
                                                 objGLSetup.EnumGLMasterType.SubBlock, _
                                                 objActDs)
       
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
                    " and blk.status ='" & objGLSetup.EnumBlockStatus.Active & "' and blk.loccode = '" & trim(strLocation) & "'" ' and blk.blktype = '" & objGLSetup.EnumPlantedAreaBlkType.Mature & "' "


        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCode, _
                                                 strParam, _
                                                 objGLSetup.EnumGLMasterType.BlkGrp, _
                                                 objActDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_SUBBLOCK_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
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

        strParam =  " order by blk.blkcode asc " & "|" & _
                    " and blk.status ='" & objGLSetup.EnumBlockStatus.Active & "' and blk.loccode = '" & trim(strLocation) & "' and blk.blktype = '" & objGLSetup.EnumPlantedAreaBlkType.Mature & "' "

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCode, _
                                                 strParam, _
                                                 objGLSetup.EnumGLMasterType.Block, _
                                                 objActDs)
        
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_SUBBLOCK_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
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

    Sub BindThnTnm(Optional ByVal pv_strCode as String = "")
        Dim strOpCode As String = "GL_CLSSETUP_TAHUNTANAM_LIST_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim objActDs As New DataSet
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intSelectIndex As Integer = 0


        strParam =  " order by left(rtrim(blk.blkcode),4) asc " & "|" & _
                    " and blk.status ='" & objGLSetup.EnumBlockStatus.Active & "' and blk.loccode = '" & trim(strLocation) & "' and blk.blktype = '" & objGLSetup.EnumPlantedAreaBlkType.Mature & "' "

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCode, _
                                                 strParam, _
                                                 objGLSetup.EnumGLMasterType.Block, _
                                                 objActDs)
        
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_SUBBLOCK_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objActDs.Tables(0).Rows.Count - 1
            objActDs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(objActDs.Tables(0).Rows(intCnt).Item("BlkCode"))
            objActDs.Tables(0).Rows(intCnt).Item("Description") = objActDs.Tables(0).Rows(intCnt).Item("BlkCode") & " (" & "TAHUN TANAM " & Trim(objActDs.Tables(0).Rows(intCnt).Item("BlkCode")) & ")"
            If objActDs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(pv_strCode) Then
                intSelectIndex = intCnt + 1
            End If
        Next

        dr = objActDs.Tables(0).NewRow()
        dr("BlkCode") = "%"
        dr("Description") = "Select " & lblThnTnm.Text
        objActDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlThnTnm.DataSource = objActDs.Tables(0)
        ddlThnTnm.DataValueField = "BlkCode"
        ddlThnTnm.DataTextField = "Description"
        ddlThnTnm.DataBind()

        ddlThnTnm.SelectedIndex = intSelectIndex

    End Sub

    Sub BindLevel()
        ddlLevel.Items.Clear()
        ddlLevel.Items.Add(New ListItem(lblBlkGrp.Text, "1"))
        ddlLevel.Items.Add(New ListItem(lblBlk.Text, "2"))
        ddlLevel.Items.Add(New ListItem(lblSubBlk.Text, "3"))
        ddlLevel.Items.Add(New ListItem(lblLocCode.Text, "4"))
        ddlLevel.Items.Add(New ListItem(lblThnTnm.Text, "5"))
    End Sub
End Class
