Imports System
Imports System.IO
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

Public Class PU_StdRpt_POStatementNew : Inherits Page

    Protected RptSelect As UserControl

    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objCMRpt As New agri.CM.clsReport()
    Dim objBITrx As New agri.BI.clsTrx()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objPU As New agri.PU.clsTrx()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objBPDs As New Object()
    Dim objSysCfgDs As New Object()

    Protected WithEvents txtPIC As Textbox
    Protected WithEvents txtJabatan As Textbox
    Protected WithEvents ddlPONoFrom As Dropdownlist
    Protected WithEvents ddlPONoTo As Dropdownlist
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents PrintPrev As ImageButton

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

    Dim objLangCapDs As New Object()
    Dim strBankAccNo As String
    Dim strBankName As String
    Dim strBankBranch As String
    Dim strUndName As String
    Dim strUndPost As String
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_ARACCMONTH")
        strAccYear = Session("SS_ARACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")     
        Else
            If Not Page.IsPostBack
                onload_GetLangCap()
                BindPONoFrom()
                BindPONoTo()
            End If
        End If
    End Sub

    Sub BindPONoFrom()
        Dim strOpCd As String = "PU_CLSTRX_PO_GET"
        Dim strParam as String
        Dim intSelectedIndex as integer
        Dim objPODs as new Object()

        strParam = "|" & strLocation & "|||||||order by A.PoID|||" 
        
        Try
            intErrNo = objPU.mtdGetPO(strOpCd, _
                                      strParam, _
                                      objPODs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_PO&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/PU_trx_POList.aspx")
        End Try

        For intCnt = 0 to objPODs.Tables(0).Rows.Count - 1
            objPODs.Tables(0).Rows(intCnt).Item("POId") = Trim(objPODs.Tables(0).Rows(intCnt).Item("POId"))
            objPODs.Tables(0).Rows(intCnt).Item("LocCode") = Trim(objPODs.Tables(0).Rows(intCnt).Item("POId")) & " ( " & Trim(objPODs.Tables(0).Rows(intCnt).Item("LocCode")) & " ) "
        Next
        
        dr = objPODs.Tables(0).NewRow()
        dr("POId") = ""
        dr("LocCode") = "Please Select Purchase Order Number"
        objPODs.Tables(0).Rows.InsertAt(dr, 0)

        ddlPONoFrom.DataSource = objPODs.Tables(0)
        ddlPONoFrom.DataValueField = "POId"
        ddlPONoFrom.DataTextField = "LocCode"
        ddlPONoFrom.DataBind()               
    End Sub

    Sub BindPONoTo()
        Dim strOpCd As String = "PU_CLSTRX_PO_GET"
        Dim strParam as String
        Dim intSelectedIndex as integer
        Dim objPODs as new Object()

        strParam = "|" & strLocation & "|||||||order by A.PoID|||" 

        Try
            intErrNo = objPU.mtdGetPO(strOpCd, _
                                      strParam, _
                                      objPODs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_PO&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/PU_trx_POList.aspx")
        End Try

        For intCnt = 0 to objPODs.Tables(0).Rows.Count - 1
            objPODs.Tables(0).Rows(intCnt).Item("POId") = Trim(objPODs.Tables(0).Rows(intCnt).Item("POId"))
            objPODs.Tables(0).Rows(intCnt).Item("LocCode") = Trim(objPODs.Tables(0).Rows(intCnt).Item("POId")) & " ( " & Trim(objPODs.Tables(0).Rows(intCnt).Item("LocCode")) & " ) "
        Next
        
        dr = objPODs.Tables(0).NewRow()
        dr("POId") = ""
        dr("LocCode") = "Please Select Purchase Order Number"
        objPODs.Tables(0).Rows.InsertAt(dr, 0)

        ddlPONoTo.DataSource = objPODs.Tables(0)
        ddlPONoTo.DataValueField = "POId"
        ddlPONoTo.DataTextField = "LocCode"
        ddlPONoTo.DataBind()        
    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender

        Dim UCTrDocDateFromTo As HtmlTableRow
        Dim ucTrMthYr As HtmlTableRow

        UCTrDocDateFromTo = RptSelect.FindControl("TRDocDateFromTo")
        UCTrDocDateFromTo.Visible = True

        ucTrMthYr = RptSelect.FindControl("TrMthYr")
        ucTrMthYr.Visible = false


        If Page.IsPostBack Then
        end if
    End Sub


    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strDec As String
        Dim strRptId As String
        Dim strRptName As String
        Dim strParam As String
        Dim strPIC as String
        Dim strJabatan as String
        Dim strPONoFrom as String
        Dim strPONoTo as String
        Dim strPeriodeFrom as String
        Dim strPeriodeTo as String

        Dim ddlist As DropDownList
        Dim rdblist As RadioButtonList
        Dim txt As TextBox

        Dim strDateSetting As String
        Dim objDateFrom As String
        Dim objDateTo As String
        Dim objDateFormat As String
        Dim intCnt As Integer

        if txtPIC.Text <> "" then 
            strPIC = txtPIC.Text
        else
            strPIC = ""
        end if

        if txtJabatan.Text <> "" then 
            strJabatan = txtJabatan.Text        
        else
            strJabatan = ""
        end if 

        ddlist = RptSelect.FindControl("lstRptName")
        strRptId = Trim(ddlist.SelectedItem.Value)

        ddlist = RptSelect.FindControl("lstRptName")
        strRptName = Trim(ddlist.SelectedItem.text)

        ddlist = RptSelect.FindControl("lstDecimal")
        strDec = Trim(ddlist.SelectedItem.Value)
    
        txt = RptSelect.FindControl("txtDateFrom")
        strPeriodeFrom = Trim(txt.Text)

        txt = RptSelect.FindControl("txtDateTo")
        strPeriodeTo = Trim(txt.Text)

        if ddlPONoFrom.SelectedItem.Text <> "Please Select Purchase Order Number" then 
            strPONoFrom = ddlPONoFrom.SelectedItem.Value
        else 
            strPONoFrom = ""
        end if 

        if ddlPONoTo.SelectedItem.Text <> "Please Select Purchase Order Number" then 
            strPONoTo = ddlPONoTo.SelectedItem.Value
        else 
            strPONoTo = ""
        end if 


        
        Response.Write("<Script Language=""JavaScript"">window.open(""PU_StdRpt_POStatementNewPreview.aspx?Type=Print&CompName=" & strCompany & _
                       "&Location=" & strLocation & _
                       "&RptId=" & strRptId & _
                       "&RptName=" & strRptName & _
                       "&Decimal=" & strDec & _
                       "&PONoFrom=" & strPONoFrom & _
                       "&PONoTo=" & strPONoTo & _
                       "&PeriodeFrom=" & strPeriodeFrom & _
                       "&PeriodeTo=" & strPeriodeTo & _
                       "&PIC=" & strPIC & _
                       "&Jabatan=" & strJabatan & _
                       """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_STDRPT_TRIALBALANCE_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/IN_StdRpt_Selection.aspx")
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
