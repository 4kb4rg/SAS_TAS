
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
Imports Microsoft.VisualBasic
Imports System.Xml
Imports System.Web.Services

Public Class CB_StdRpt_PosisiDeposito : Inherits Page

    Protected RptSelect As UserControl
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objSysCfg As New agri.PWSystem.clsConfig()
    Protected objLangCap As New agri.PWSystem.clsLangCap()

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents txtDateFrom As TextBox
    Protected WithEvents lblErrDateFrom As Label
    Protected WithEvents txtDateTo As TextBox
    Protected WithEvents lblErrDateTo As Label

    Protected WithEvents rbPro1 As RadioButton
    Protected WithEvents rbPro2 As RadioButton


    Protected WithEvents PrintPrev As ImageButton
    Protected WithEvents btnDateFrom As Image
    Protected WithEvents btnDateTo As Image
 
    Dim objLangCapDs As New Object()
    
    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfigsetting As Integer
    Dim strDateFmt As String
    Dim strAcceptFormat As String
    Dim strCOACode As String

    Dim strLocType as String
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim dr As DataRow
    Dim intCnt As Integer
    Dim intErrNo As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        Dim arrDate As Array
        Dim strMonth As String
        Dim strYear As String
        Dim strDate As String

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strDateFmt = Session("SS_DATEFMT")  
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            lblErrDateTo.Visible = false
            lblErrDateFrom.Visible = false
            onload_GetLangCap()
            If Not Page.IsPostBack Then
    
               
                txtDateTo.Text = objGlobal.GetShortDate(strDateFmt, Now())
                
                arrDate = Split(txtDateTo.Text, "/")

                IF strDateFmt = "DMY" THEN
                    If val(arrDate(1)) <> 1 Then
                        strMonth = Format(Val(arrDate(1) - 1), "00")
                        strYear = arrDate(2)
                    Else
                        strMonth = "12"
                        strYear = Val(arrDate(2)) - 1
                    End If
                   
                    strDate = arrDate(0)
                    txtDateFrom.Text = strDate & "/" & strMonth & "/" & strYear
                ELSE 
                    If val(arrDate(1)) <> 1 Then
                        strMonth = Format(Val(arrDate(0) - 1), "00")
                        strYear = arrDate(2)
                    Else
                        strMonth = "12"
                        strYear = Val(arrDate(2)) - 1
                    End IF
                    
                    strDate = arrDate(1)
                    txtDateFrom.Text = strMonth & "/" & strDate & "/" & strYear
                END IF
                
 
            End If   
        End If
    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
       
        Dim SLocation As HtmlTableRow

        SLocation  = RptSelect.FindControl("trLoc")
        
        SLocation.visible = false         

    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        strCOACode = GetCaption(objLangCap.EnumLangCap.Account)
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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PWSYSTEM_CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/PR_StdRpt_Selection.aspx")
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
        Dim strRptTitle As String
        Dim strDecimal As String
        Dim ddl As Dropdownlist
        Dim strDateFrom As String = Date_Validation(txtDateFrom.Text, False)
        Dim strDateTo As String = Date_Validation(txtDateTo.Text, False)
        Dim strType  As String
        
        ddl = RptSelect.FindControl("lstRptname")
        strRptID = ddl.SelectedItem.Value
       
        ddl = RptSelect.FindControl("lstRptname")
        strRptTitle = ddl.SelectedItem.Text

        ddl = RptSelect.FindControl("lstDecimal")
        strDecimal = Trim(ddl.SelectedItem.Value)

        If Trim(txtDateFrom.Text) <> "" Then
            If Trim(strDateFrom) = "" Then
                    lblErrDateFrom.Visible = True
                    lblErrDateFrom.Text = "Invalid date format." & strAcceptFormat
                    Exit Sub
            End If
        End If
        
        If Trim(txtDateTo.Text) <> "" Then
            If Trim(strDateTo) = "" Then
                  lblErrDateTo.Visible = True
                  lblErrDateTo.Text = "Invalid date format." & strAcceptFormat
                  Exit Sub
            End If
            If blnValidEndStartDate(Trim(strDateTo), Trim(strDateFrom)) = False
                 lblErrDateTo.Visible = True
                 lblErrDateTo.Text = "Ending Date must greater then Starting Date."
                 Exit Sub
            End if 
        End If
    
        if rbPro1.Checked= True then
            strType = "0"
        else
            strType = "1"
        end if
        
        Response.Write("<Script Language=""JavaScript"">window.open(""CB_StdRpt_PosisiDepositoPreview.aspx?Type=Print&CompName=" & strCompany & _
                       "&RptID=" & strRptID & _
                       "&RptTitle=" & strRptTitle & _
                       "&Decimal=" & strDecimal & _
                       "&DateTo=" & strDateTo & _
                       "&DateFrom=" & strDateFrom & _
                       "&RType=" & strType & _
                       "&COACode=" & strCOACode & _
                       """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
       

    End Sub

    Function blnValidEndStartDate(Byval pv_strEndDate As String, Byval pv_strStartDate as string) As Boolean
            blnValidEndStartDate = False
            If cDate(pv_strStartDate)< cDate(pv_strEndDate)
                blnValidEndStartDate = True
            End If 
    End Function

    Function Date_Validation(ByVal pv_strInputDate As String, ByVal pv_blnIsShortDate As Boolean) As String
        Dim objSysCfgDs As New Object
        Dim objActualDate As New Object
        Dim strDateFormat As String
        Dim strParam As String
        Dim intErrNo As Integer

        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"

        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CB_DEPOSIT_GET_CONFIG&errmesg=" & Exp.Message & "&redirect=CB/trx/CB_StdRpt_TreasuryCashFlow.aspx")
        End Try

        strDateFormat = objSysCfg.mtdGetDateFormat(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt").Trim())

        If pv_blnIsShortDate Then
            Date_Validation = objGlobal.GetShortDate(strDateFormat, pv_strInputDate)
        Else
            If objGlobal.mtdValidInputDate(strDateFormat, _
                                           pv_strInputDate, _
                                           strAcceptFormat, _
                                           objActualDate) = True Then
                Date_Validation = objActualDate
            Else
                Date_Validation = ""
            End If
        End If
    End Function

End Class
