Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information
Imports System.Xml
Imports System.Web.Services

Public Class IN_StdRpt_PrevMaintenance : Inherits Page

    Protected RptSelect As UserControl

    Dim objIN As New agri.IN.clsReport()
    Dim objINSetup As New agri.IN.clsSetup()
    Dim objINTrx As New agri.IN.clsTrx()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objPRSetup As New agri.PR.clsSetup()

    Protected WithEvents lblDate As Label
    Protected WithEvents lblDateFormat As Label
    Protected WithEvents lblIss As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblSelect As Label

    Protected WithEvents lblBlkTag As Label
    Protected WithEvents lblSubBlkTag As Label
    Protected WithEvents lblLocation As Label

    Protected WithEvents txtItemCode As TextBox
    Protected WithEvents txtInstallDateFrom As TextBox
    Protected WithEvents txtInstallDateTo As TextBox
    Protected WithEvents txtReplDateFrom As TextBox
    Protected WithEvents txtReplDateTo As TextBox
    Protected WithEvents ddlBlock As Dropdownlist
    Protected WithEvents ddlSubBlock As Dropdownlist

    Protected WithEvents btnSelInsDateFrom As Image
    Protected WithEvents btnSelInsDateTo As Image
    Protected WithEvents btnSelReplDateFrom As Image
    Protected WithEvents btnSelReplDateTo As Image

    Protected WithEvents PrintPrev As ImageButton

    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strSelectedBlkCode As String
    
    Dim intErrNo As Integer
	
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String
	
    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        strLocType = Session("SS_LOCTYPE")
        lblDate.Visible = False
        lblDateFormat.Visible = False

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
                onload_GetLangCap()
                BindBlock("")
                BindSubBlock("")
            End If
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        lblBlkTag.Text = GetCaption(objLangCap.EnumLangCap.Block) & " Code "
        lblSubBlkTag.Text = GetCaption(objLangCap.EnumLangCap.SubBlock) & " Code "
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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PWSYSTEM_CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/IN_StdRpt_Selection.aspx")
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

    Sub BindBlock(ByVal pv_strBlockCode As String)
        Dim objBlkDs As New Dataset()
        Dim strOpCd As String = "GL_CLSSETUP_BLOCK_LIST_GET"
        Dim dr As DataRow
        Dim strParam As String = "|" & "And blk.Status = '" & objGLSetup.EnumBlockStatus.Active & "' And blk.LocCode = '" & strLocation & "'" & "|" & strLocation
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intMasterType As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCd, strParam, 0, objBlkDs)
 
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_TRX_ITEMTOMACHINE_BLOCK_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        pv_strBlockCode = Trim(UCase(pv_strBlockCode))

        For intCnt = 0 To objBlkDs.Tables(0).Rows.Count - 1
            objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") = objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode").Trim()
            objBlkDs.Tables(0).Rows(intCnt).Item("Description") = objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") & " (" & objBlkDs.Tables(0).Rows(intCnt).Item("Description").Trim() & ") "
            If UCase(objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode")) = pv_strBlockCode Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objBlkDs.Tables(0).NewRow()
        If intSelectedIndex = 0 Then
            If pv_strBlockCode <> "" Then
                dr("BlkCode") = Trim(pv_strBlockCode)
                dr("Description") = Trim(pv_strBlockCode)
            Else
                dr("BlkCode") = ""
                dr("Description") = lblSelect.Text & lblBlkTag.Text
            End If
        Else
            dr("BlkCode") = ""
            dr("Description") = lblSelect.Text & lblBlkTag.Text
        End If
        objBlkDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlBlock.DataSource = objBlkDs.Tables(0)
        ddlBlock.DataValueField = "BlkCode"
        ddlBlock.DataTextField = "Description"
        ddlBlock.DataBind()
        ddlBlock.SelectedIndex = intSelectedIndex
        objBlkDs = Nothing
    End Sub

    Sub onSelect_Block(ByVal Sender As Object, ByVal E As EventArgs)
        If Not (ddlBlock.SelectedItem.Value = "")
           strSelectedBlkCode = Trim(ddlBlock.SelectedItem.Value)
           BindSubBlock("")
        End If
    End Sub

    Sub BindSubBlock(ByVal pv_strSubBlkCode As String)
        Dim objSubBlkDs As New Dataset()
        Dim strOpCd As String = "GL_CLSSETUP_SUBBLOCK_LIST_GET"
        Dim dr As DataRow
        Dim strParam As String 
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intMasterType As Integer
        Dim intSelectedIndex As Integer = 0
        
        strParam = "|" & "And Sub.Status = '" & objGLSetup.EnumSubBlockStatus.Active & "' And BlkCode = '" & Trim(strSelectedBlkCode) & "' And sub.LocCode = '" & strLocation & "'" & "|" & strLocation
        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCd, strParam, 0, objSubBlkDs)
 
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_TRX_ITEMTOMACHINE_BLOCK_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        pv_strSubBlkCode = Trim(UCase(pv_strSubBlkCode))

        For intCnt = 0 To objSubBlkDs.Tables(0).Rows.Count - 1
            objSubBlkDs.Tables(0).Rows(intCnt).Item("SubBlkCode") = objSubBlkDs.Tables(0).Rows(intCnt).Item("SubBlkCode").Trim()
            objSubBlkDs.Tables(0).Rows(intCnt).Item("Description") = objSubBlkDs.Tables(0).Rows(intCnt).Item("SubBlkCode") & " (" & objSubBlkDs.Tables(0).Rows(intCnt).Item("Description").Trim() & ") "
            If UCase(objSubBlkDs.Tables(0).Rows(intCnt).Item("SubBlkCode")) = pv_strSubBlkCode Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objSubBlkDs.Tables(0).NewRow()
        If intSelectedIndex = 0 Then
            If pv_strSubBlkCode <> "" Then
                dr("SubBlkCode") = Trim(pv_strSubBlkCode)
                dr("Description") = Trim(pv_strSubBlkCode)
            Else
                dr("SubBlkCode") = ""
                dr("Description") = lblSelect.Text & lblSubBlkTag.Text
            End If
        Else
            dr("SubBlkCode") = ""
            dr("Description") = lblSelect.Text & lblSubBlkTag.Text
        End If
        objSubBlkDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlSubBlock.DataSource = objSubBlkDs.Tables(0)
        ddlSubBlock.DataValueField = "SubBlkCode"
        ddlSubBlock.DataTextField = "Description"
        ddlSubBlock.DataBind()
        ddlSubBlock.SelectedIndex = intSelectedIndex
        objSubBlkDs = Nothing
    End Sub

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strSubBlkCode As String = Trim(ddlSubBlock.SelectedItem.Value)
        Dim strBlkCode As String = Trim(ddlBlock.SelectedItem.Value)
        Dim strItemCode As String = Trim(txtItemCode.Text)
        Dim strInstallDateFrom As String
        Dim strInstallDateTo As String
        Dim strReplDateFrom As String
        Dim strReplDateTo As String

        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String

        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden

        Dim strParam As String
        Dim strDateSetting As String
        Dim objSysCfgDs As New Object()
        Dim objDateFormat As New Object()
        Dim objInstallDateFrom As String
        Dim objInstallDateTo As String
        Dim objReplDateFrom As String
        Dim objReplDateTo As String

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
        
        strInstallDateFrom = txtInstallDateFrom.Text
        strInstallDateTo = txtInstallDateTo.Text
        strReplDateFrom = txtReplDateFrom.Text
        strReplDateTo = txtReplDateTo.Text
        
        strBlkCode = Server.UrlEncode(strBlkCode)
        strSubBlkCode = Server.UrlEncode(strSubBlkCode)
        strItemCode = Server.UrlEncode(strItemCode)

        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"
        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As System.Exception
            Response.Redirect("../mesg/ErrorMessage.aspx?errcode=IN_STDRPT_ITEMTOMACHINE_GET_CONFIG_DATE&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/IN_StdRpt_Selection.aspx")
        End Try

        strDateSetting = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))

        If Not (strInstallDateFrom = "" And strInstallDateTo = "") Then
            If objGlobal.mtdValidInputDate(strDateSetting, strInstallDateFrom, objDateFormat, objInstallDateFrom) = True And objGlobal.mtdValidInputDate(strDateSetting, strInstallDateTo, objDateFormat, objInstallDateTo) = True Then
                Response.Write("<Script Language=""JavaScript"">window.open(""IN_StdRpt_PrevMaintenancePreview.aspx?Type=Print&CompName=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & _
                               "&Decimal=" & strDec & "&lblBlkTag=" & lblBlkTag.Text & "&lblSubBlkTag=" & lblSubBlkTag.Text & _
                               "&BlkCode=" & strBlkCOde & "&SubBlkCode=" & strSubBlkCode & _
                               "&ItemCode=" & strItemCode & "&InstallDateFrom=" & objInstallDateFrom & "&InstallDateTo=" & objInstallDateTo & _
                               "&ReplDateFrom=" & objReplDateFrom & "&ReplDateTo=" & objReplDateTo & _
                               """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
            Else
                lblDateFormat.Text = objDateFormat & "."
                lblDate.Visible = True
                lblDateFormat.Visible = True
            End If
        Else
            Response.Write("<Script Language=""JavaScript"">window.open(""IN_StdRpt_PrevMaintenancePreview.aspx?Type=Print&CompName=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & _
                               "&Decimal=" & strDec & "&lblBlkTag=" & lblBlkTag.Text & "&lblSubBlkTag=" & lblSubBlkTag.Text & _
                               "&BlkCode=" & strBlkCOde & "&SubBlkCode=" & strSubBlkCode & _
                               "&ItemCode=" & strItemCode & "&InstallDateFrom=" & objInstallDateFrom & "&InstallDateTo=" & objInstallDateTo & _
                               "&ReplDateFrom=" & objReplDateFrom & "&ReplDateTo=" & objReplDateTo & _
                               """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
        End If

        If Not (strReplDateFrom = "" And strReplDateTo = "") Then
            If objGlobal.mtdValidInputDate(strDateSetting, strReplDateFrom, objDateFormat, objReplDateFrom) = True And objGlobal.mtdValidInputDate(strDateSetting, strReplDateTo, objDateFormat, objReplDateTo) = True Then
                Response.Write("<Script Language=""JavaScript"">window.open(""IN_StdRpt_PrevMaintenancePreview.aspx?Type=Print&CompName=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & _
                               "&Decimal=" & strDec & "&lblBlkTag=" & lblBlkTag.Text & "&lblSubBlkTag=" & lblSubBlkTag.Text & _
                               "&BlkCode=" & strBlkCOde & "&SubBlkCode=" & strSubBlkCode & _
                               "&ItemCode=" & strItemCode & "&InstallDateFrom=" & objInstallDateFrom & "&InstallDateTo=" & objInstallDateTo & _
                               "&ReplDateFrom=" & objReplDateFrom & "&ReplDateTo=" & objReplDateTo & _
                               """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
            Else
                lblDateFormat.Text = objDateFormat & "."
                lblDate.Visible = True
                lblDateFormat.Visible = True
            End If
        Else
            Response.Write("<Script Language=""JavaScript"">window.open(""IN_StdRpt_PrevMaintenancePreview.aspx?Type=Print&CompName=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & _
                               "&Decimal=" & strDec & "&lblBlkTag=" & lblBlkTag.Text & "&lblSubBlkTag=" & lblSubBlkTag.Text & _
                               "&BlkCode=" & strBlkCOde & "&SubBlkCode=" & strSubBlkCode & _
                               "&ItemCode=" & strItemCode & "&InstallDateFrom=" & objInstallDateFrom & "&InstallDateTo=" & objInstallDateTo & _
                               "&ReplDateFrom=" & objReplDateFrom & "&ReplDateTo=" & objReplDateTo & _
                               """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
        End If
    End Sub

End Class
