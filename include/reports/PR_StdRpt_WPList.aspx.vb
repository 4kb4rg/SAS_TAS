Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports System.Xml
Imports System.Web.Services

Public Class PR_StdRpt_WPList : Inherits Page

    Protected RptSelect As UserControl

    Dim objPR As New agri.PR.clsReport()
    Dim objHRTrx as New agri.HR.clsTrx()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Protected WithEvents ddlSubActCodeFrom As DropDownList
    Protected WithEvents ddlSubActCodeTo As DropDownList
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents ddlAccClsCode as DropDownList
    Protected WithEvents PrintPrev As ImageButton
    Protected WithEvents txtDateFrom As TextBox
    Protected WithEvents txtDateTo As TextBox
    Protected WithEvents txtPIC1 As TextBox
    Protected WithEvents txtPIC2 As TextBox
    Protected WithEvents lblErrAccClsCode As Label
    Protected WithEvents lblErrSubActCode As Label
    Protected WithEvents lblErrDupl As Label
    Protected WithEvents dgLineDet As DataGrid
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblDate As Label
    Protected WithEvents lblDateFormat As Label
    Protected WithEvents btnSelDateFrom As Image
    Protected WithEvents btnSelDateTo As Image

    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfigsetting As Integer
    Dim strUserLoc As String
    Dim strLocType as String  
    Dim strDateFmt As String

    Dim dr As DataRow
    Dim intCnt As Integer
    Dim intErrNo As Integer
    Dim tempAD As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        Dim objWPTrxDs As New Dataset()
        Dim strOpCd As String = "PR_STDRPT_WPLIST_DEL_BYUSER"
        Dim strParam As String 
        Dim intErrNo As Integer

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")
        strDateFmt = Session("SS_DATEFMT")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            lblErrAccClsCode.Visible = False
            lblErrSubActCode.Visible = False
            lblErrDupl.Visible = False
            lblErrMessage.Visible = False
            lblDate.Visible = False
            lblDateFormat.Visible = False
            If Not Page.IsPostBack Then
                onload_GetLangCap()
                BindAccClsCode("")

                strParam = Trim(strUserId)
                Try
                    intErrNo = objPR.mtdGetWPListStdRpt(strOpCd, _
                                                    strParam, _
                                                    objWPTrxDs, _
                                                    True)
                Catch Exp As Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_STDRPT_WPLIST_DEL_BYUSER&errmesg=" & Exp.ToString() & "&redirect=")
                End Try
            End If
        End If
    End Sub




    Sub onload_GetLangCap()
        GetEntireLangCap()

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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PWSYSTEM_CLSLANGCAP_BUSSTERM_GET&errmesg=" & Exp.ToString() & "&redirect=../en/reports/PR_StdRpt_Selection.aspx")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

        For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If strLocType = objAdminLoc.EnumLocType.Mill then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                Else    
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                End If
                Exit For
            End If
        Next
    End Function


    Sub BindAccClsCode(ByVal pv_strAccClsCode As String)
        Dim objAccClsDs As New Dataset
        Dim strOpCd As String = "GL_CLSSETUP_ACCCLS_LIST_SEARCH"
        Dim dr As DataRow           
        Dim strParam As String = "|" & "and Acc.Status = '" & objGLSetup.EnumAccClsStatus.Active & "'" 
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, strParam, 0, objAccClsDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_WPCONTRACTORTRX_ACCCLS_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        pv_strAccClsCode = Trim(UCase(pv_strAccClsCode))

        For intCnt = 0 To objAccClsDs.Tables(0).Rows.Count - 1
            objAccClsDs.Tables(0).Rows(intCnt).Item("Description") = objAccClsDs.Tables(0).Rows(intCnt).Item("AccClsCode") & " (" & objAccClsDs.Tables(0).Rows(intCnt).Item("Description") & ")"
            If UCase(objAccClsDs.Tables(0).Rows(intCnt).Item("AccClsCode")) = pv_strAccClsCode Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objAccClsDs.Tables(0).NewRow()
        If intSelectedIndex = 0 Then
            If pv_strAccClsCode <> "" Then
                dr("AccClsCode") = Trim(pv_strAccClsCode)
                dr("Description") = Trim(pv_strAccClsCode)
            Else
                dr("AccClsCode") = ""
                dr("Description") = "Select one Account Class"
            End If
        Else
            dr("AccClsCode") = ""
            dr("Description") = "Select one Account Class"
        End If
        objAccClsDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlAccClsCode.DataSource = objAccClsDs.Tables(0)
        ddlAccClsCode.DataValueField = "AccClsCode"
        ddlAccClsCode.DataTextField = "Description"
        ddlAccClsCode.DataBind()
        ddlAccClsCode.SelectedIndex = intSelectedIndex
        objAccClsDs = Nothing
    End Sub

    Sub onSelect_AccClsCode(ByVal Sender As Object, ByVal E As EventArgs)
        AccClsCodeChange()
    End Sub

    Sub AccClsCodeChange()
        GetSubActCode(ddlAccClsCode.SelectedItem.Value)    
    End Sub

    Sub GetSubActCode(ByVal pv_strSubActCode As String)
        Dim objSubActDs As New Dataset
        Dim strOpCd As String = "PR_CLSTRX_WPCONTRACTORTRX_GET_SUBACTLIST"
        Dim dr As DataRow           
        Dim strParam As String = "|" & "Where AccClsCode = '" & Trim(pv_strSubActCode) & "' "
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCd, strParam, 0, objSubActDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_WPCONTRACTORTRX_SUBACT_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        pv_strSubActCode = Trim(UCase(pv_strSubActCode))

        For intCnt = 0 To objSubActDs.Tables(0).Rows.Count - 1
            objSubActDs.Tables(0).Rows(intCnt).Item("Description") = objSubActDs.Tables(0).Rows(intCnt).Item("SubActCode") & " (" & objSubActDs.Tables(0).Rows(intCnt).Item("Description") & ")"
            If UCase(objSubActDs.Tables(0).Rows(intCnt).Item("SubActCode")) = pv_strSubActCode Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objSubActDs.Tables(0).NewRow()
        dr("SubActCode") = ""
        dr("_Description") = "Select one Sub Activity"
        objSubActDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlSubActCodeFrom.DataSource = objSubActDs.Tables(0)
        ddlSubActCodeFrom.DataValueField = "SubActCode"
        ddlSubActCodeFrom.DataTextField = "_Description"
        ddlSubActCodeFrom.DataBind()
        ddlSubActCodeFrom.SelectedIndex = intSelectedIndex

        ddlSubActCodeTo.DataSource = objSubActDs.Tables(0)
        ddlSubActCodeTo.DataValueField = "SubActCode"
        ddlSubActCodeTo.DataTextField = "_Description"
        ddlSubActCodeTo.DataBind()
        ddlSubActCodeTo.SelectedIndex = intSelectedIndex
        objSubActDs = Nothing    
    End Sub

    Sub BindSubActCode(ByVal pv_strFunctioCode As String)
        Dim objSubActDs As New Dataset
        Dim strOpCd As String = "PR_CLSTRX_WPTRX_GET_SUBACTLIST"
        Dim dr As DataRow           
        Dim strParam As String = "|" & "where Status = '" & objGLSetup.EnumSubActStatus.Active & "'" 
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCd, strParam, 0, objSubActDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_WPCONTRACTORTRX_SUBACT_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        pv_strFunctioCode = Trim(UCase(pv_strFunctioCode))

        For intCnt = 0 To objSubActDs.Tables(0).Rows.Count - 1
            objSubActDs.Tables(0).Rows(intCnt).Item("Description") = objSubActDs.Tables(0).Rows(intCnt).Item("FunctionCode") & " (" & objSubActDs.Tables(0).Rows(intCnt).Item("Description") & ")"
            If UCase(objSubActDs.Tables(0).Rows(intCnt).Item("FunctionCode")) = pv_strFunctioCode Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objSubActDs.Tables(0).NewRow()
        If intSelectedIndex = 0 Then
            If pv_strFunctioCode <> "" Then
                dr("FunctionCode") = Trim(pv_strFunctioCode)
                dr("_Description") = Trim(pv_strFunctioCode)
            Else
                dr("FunctionCode") = ""
                dr("_Description") = "Select one Sub Activity"
            End If
        Else
            dr("SubActCode") = ""
            dr("_Description") = "Select one Sub Activity"
        End If
        objSubActDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlSubActCodeFrom.DataSource = objSubActDs.Tables(0)
        ddlSubActCodeFrom.DataValueField = "FunctionCode"
        ddlSubActCodeFrom.DataTextField = "_Description"
        ddlSubActCodeFrom.DataBind()
        ddlSubActCodeFrom.SelectedIndex = intSelectedIndex

        ddlSubActCodeTo.DataSource = objSubActDs.Tables(0)
        ddlSubActCodeTo.DataValueField = "FunctionCode"
        ddlSubActCodeTo.DataTextField = "_Description"
        ddlSubActCodeTo.DataBind()
        ddlSubActCodeTo.SelectedIndex = intSelectedIndex
        objSubActDs = Nothing
    End Sub

    Sub onLoad_DisplayLine()
        Dim objWPTrxDs As New Dataset()
        Dim strOpCd As String = "PR_STDRPT_WPLIST_GET"
        Dim strParam As String 
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim lbButton As LinkButton

        strParam = "|" & "PR.UpdateID = '" & Trim(strUserId) & "' "

        Try
            intErrNo = objPR.mtdGetWPListStdRpt(strOpCd, _
                                            strParam, _
                                            objWPTrxDs, _
                                            False)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_STDRPT_WPLIST_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        dgLineDet.DataSource = objWPTrxDs.Tables(0)
        dgLineDet.DataBind()

        For intCnt = 0 To dgLineDet.Items.Count - 1
            lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
            lbButton.Visible = True
            lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
        Next
    End Sub

    Sub btnAdd_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim objWPTrxDs As New Dataset()
        Dim objFound As Boolean
        Dim blnIsUpdated As Boolean
        Dim strOpCode_GetLine As String = "PR_STDRPT_WPLIST_LINEDUPL_GET"
        Dim strOpCode_AddLine As String = "PR_STDRPT_WPLIST_ADD"
        Dim strAccClsCode As String = Request.Form("ddlAccClsCode")
        Dim strSubActCodeFrom As String = Request.Form("ddlSubActCodeFrom")
        Dim strSubActCodeTo As String = Request.Form("ddlSubActCodeTo")
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intTotWP As Integer = 0
        
        strAccClsCode = IIf(strAccClsCode = "", ddlAccClsCode.SelectedItem.Value, strAccClsCode)

        If strAccClsCode = "" Then
            lblErrAccClsCode.Visible = True
            Exit Sub
        ElseIf strSubActCodeFrom = "" or strSubActCodeTo = "" Then
            lblErrSubActCode.Visible = True
            Exit Sub
        End If

        strParam = "|" & "AccClsCode = '" & strAccClsCode & "' And UpdateID = '" & Trim(strUserId) & "' "
        Try
            intErrNo = objPR.mtdGetWPListStdRpt(strOpCode_GetLine, _
                                            strParam, _
                                            objWPTrxDs, _
                                            False)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_STDRPT_WPLIST_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objWPTrxDs.Tables(0).Rows.Count > 0 Then
            lblErrDupl.Visible = True
            Exit Sub
        Else 
            strParam = strAccClsCode & "|" & _
                        strSubActCodeFrom & "|" & _
                        strSubActCodeTo & "|" & _
                        Trim(strUserID) 
            
            Try
                intErrNo = objPR.mtdUpdWPListStdRpt(strOpCode_AddLine, _
                                                        strParam, _
                                                        False, _
                                                        objWPTrxDs)
            Catch Exp As Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_STDRPT_WPLIST_ADD&errmesg=" & Exp.ToString() & "&redirect=/report/pr_stdrpt_WPList.aspx?AccClsCode=" & strAccClsCode)
            End Try
            
        onLoad_DisplayLine()
        End If
        objWPTrxDs = Nothing
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim objWPTrxDs As New Object()
        Dim strOpCode_DelLine As String = "PR_STDRPT_WPLIST_DEL"
        Dim strParam As String
        Dim lblAccClsCode As Label
        Dim strAccClsCode As String
        Dim intErrNo As Integer

        dgLineDet.EditItemIndex = Convert.ToInt32(E.Item.ItemIndex)
        lblAccClsCode = dgLineDet.Items.Item(Convert.ToInt32(E.Item.ItemIndex)).FindControl("AccClsCode")
        strAccClsCode = lblAccClsCode.Text

        Try
            strParam = strAccClsCode & "|" & _
                       strUserId
            intErrNo = objPR.mtdUpdWPListStdRpt(strOpCode_DelLine, _
                                                        strParam, _
                                                        True, _
                                                        objWPTrxDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_STDRPT_WPLIST_DEL&errmesg=" & Exp.ToString() & "&redirect=/report/pr_stdrpt_WPList.aspx?AccClsCode=" & strAccClsCode)
        End Try

        onLoad_DisplayLine()
    End Sub

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strPIC1 As String
        Dim strPIC2 As String

        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String
        Dim strDateFrom As String
        Dim strDateTo As String

        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden

        Dim strParam As String
        Dim strDateSetting As String
        Dim objSysCfgDs As New Object()
        Dim objDateFormat As New Object()
        Dim objDateFrom As String
        Dim objDateTo As String

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
        
        If Not (txtDateFrom.Text = "" And txtDateTo.Text="") Then
            strDateFrom = txtDateFrom.Text
            strDateTo = txtDateTo.Text
        Else
            strDateFrom = objGlobal.GetShortDate(strDateFmt, Now())
            strDateTo = objGlobal.GetShortDate(strDateFmt, Now())
        End If

        strPIC1 = txtPIC1.Text
        strPIC2 = txtPIC2.Text

        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"
        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PR_STDRPT_WPLIST_GET_CONFIG_DATE&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/IN_StdRpt_Selection.aspx")
        End Try

        strDateSetting = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))

        If Not (strDateFrom = "" And strDateTo = "") Then
            If objGlobal.mtdValidInputDate(strDateSetting, strDateFrom, objDateFormat, objDateFrom) = True And objGlobal.mtdValidInputDate(strDateSetting, strDateTo, objDateFormat, objDateTo) = True Then
                Response.Write("<Script Language=""JavaScript"">window.open(""PR_StdRpt_WPListPreview.aspx?Type=Print&CompName=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & _
                               "&Decimal=" & strDec & "&DateFrom=" & objDateFrom & "&DateTo=" & objDateTo & _
                               "&PIC1=" & strPIC1 & "&PIC2=" & strPIC2 & _
                               """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
            Else
                lblDateFormat.Text = objDateFormat & "."
                lblDate.Visible = True
                lblDateFormat.Visible = True
            End If
        Else
            Response.Write("<Script Language=""JavaScript"">window.open(""PR_StdRpt_WPListPreview.aspx?Type=Print&CompName=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & _
                               "&Decimal=" & strDec & "&DateFrom=" & objDateFrom & "&DateTo=" & objDateTo & _
                               "&PIC1=" & strPIC1 & "&PIC2=" & strPIC2 & _
                               """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
        End If
    End Sub

End Class
