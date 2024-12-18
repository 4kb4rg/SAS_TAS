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

Public Class CB_StdRpt_DailyBank : Inherits Page

    Protected RptSelect As UserControl
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objSysCfg As New agri.PWSystem.clsConfig()
    Protected objLangCap As New agri.PWSystem.clsLangCap()
    Protected objGLTrx As New agri.GL.ClsTrx()


    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents txtDate As TextBox
    Protected WithEvents txtDateTo As TextBox
    Protected WithEvents lblErrDate As Label
    Protected WithEvents PrintPrev As ImageButton
    Protected WithEvents btnDate As Image
    Protected WithEvents cbExcel As CheckBox
    Protected WithEvents cbKomparatif As CheckBox
    Protected WithEvents cbSaldo As CheckBox

    Protected WithEvents lblErrBank As Label
    Protected WithEvents ddlBank As DropDownList

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

    Dim dr As DataRow
    Dim intCnt As Integer
    Dim intErrNo As Integer

    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
      
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

            lblErrDate.Visible = False
            lblErrMessage.Visible = False

            If Not Page.IsPostBack Then
    
                txtDate.Text = "01/" & Month(Now()) & "/" & Year(Now()) 'objGlobal.GetShortDate(strDateFmt, Now())
                txtDateTo.Text = "01/" & Month(Now()) & "/" & Year(Now())
                BindBankCode("")
               
            End If   
        End If
    End Sub


    Sub BindBankCode(ByVal pv_strBankCode As String)
        Dim strOpCodeBank As String = "HR_CLSSETUP_BANK_LOCATION_GET_ONLY"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objBankDs As New DataSet()
        Dim intSelectedBankIndex As Integer = 0
        Dim strParamName As String
        Dim strParamValue As String

        strParamName = "LOCCODE"
        strParamValue = strLocation

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCodeBank, _
                                                strParamName, _
                                                strParamValue, _
                                                objBankDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ID_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objBankDs.Tables(0).Rows.Count - 1
            If Trim(objBankDs.Tables(0).Rows(intCnt).Item("BankCode")) = Trim(pv_strBankCode) Then
                intSelectedBankIndex = intCnt + 1
                Exit For
            End If
        Next intCnt

        Dim dr As DataRow
        dr = objBankDs.Tables(0).NewRow()
        dr("BankCode") = ""
        dr("_Description") = "Please select Bank"
        objBankDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlBank.DataSource = objBankDs.Tables(0)
        ddlBank.DataValueField = "BankCode"
        ddlBank.DataTextField = "_Description"
        ddlBank.DataBind()
        ddlBank.SelectedIndex = intSelectedBankIndex
    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
       

        

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
       
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim strDateTo As String = Date_Validation(txtDateTo.Text, False)
        Dim strAccCode As String
   
        Dim intType  As Integer
        Dim strUserLoc As String
        Dim tempUserLoc As HtmlInputHidden
        Dim templblUL As Label
        Dim ddl As Dropdownlist
        Dim strExportToExcel As String
        Dim strKomparatif As String
        Dim strSaldo As String
        Dim strRptInit As String

        ddl = RptSelect.FindControl("lstRptname")
        strRptID = ddl.SelectedItem.Value
       
        ddl = RptSelect.FindControl("lstRptname")
        strRptTitle = ddl.SelectedItem.Text

        ddl = RptSelect.FindControl("lstDecimal")
        strDecimal = Trim(ddl.SelectedItem.Value)

        tempUserLoc = RptSelect.FindControl("hidUserLoc")
        strUserLoc = Trim(tempUserLoc.Value)

        If Trim(txtDate.Text) <> "" Then
            If Trim(strDate) = "" Then
                    lblErrDate.Visible = True
                    lblErrDate.Text = "Invalid date format." & strAcceptFormat
                    Exit Sub
            End If
        End If

        If Trim(txtDateTo.Text) <> "" Then
            If Trim(strDateTo) = "" Then
                lblErrDate.Visible = True
                lblErrDate.Text = "Invalid date format." & strAcceptFormat
                Exit Sub
            End If
            If blnValidEndStartDate(Trim(strDateTo), Trim(strDate)) = False Then
                lblErrDate.Visible = True
                lblErrDate.Text = "Ending Date must greater then Starting Date."
                Exit Sub
            End If
        End If
        
        Dim strBank As String
        Dim strBankAccCode As String
        Dim strBankAccNo As String
        Dim arrParam As Array

        If cbKomparatif.Checked = True And cbSaldo.Checked = True Then
            lblErrMessage.Text = "Please select one option only"
            lblErrMessage.Visible = True
            Exit Sub
        End If

        If cbKomparatif.Checked = True Then
            strRptInit = "Komparatif"
        ElseIf cbSaldo.Checked = True Then
            strRptInit = "Saldo"
        Else
            strRptInit = "Daily"
        End If

        If cbKomparatif.Checked = False And cbSaldo.Checked = False Then
            If ddlBank.SelectedItem.Value = "" Then
                strBank = ""
                strBankAccCode = ""
                strBankAccNo = ""
                lblErrBank.Visible = True
                Exit Sub
            Else
                arrParam = Split(Trim(ddlBank.SelectedItem.Value), "|")
                strBank = Trim(arrParam(0))
                strBankAccCode = Trim(arrParam(1))
                strBankAccNo = Trim(arrParam(2))
            End If
        Else
            strBank = ""
            strBankAccCode = ""
            strBankAccNo = ""
        End If
        
        strAccCode = Trim(strBankAccCode)
      
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

        strExportToExcel = IIF(cbExcel.Checked = True, "1", "0")
        strKomparatif = IIf(cbKomparatif.Checked = True, "1", "0")
        strSaldo = IIf(cbSaldo.Checked = True, "1", "0")


        Response.Write("<Script Language=""JavaScript"">window.open(""CB_StdRpt_DailyBankPreview.aspx?Type=Print&CompName=" & strCompany & _
                       "&RptID=" & strRptID & _
                       "&RptTitle=" & strRptTitle & _
                       "&Decimal=" & strDecimal & _
                       "&Date=" & strDate & _
                       "&DateTo=" & strDateTo & _
                       "&AccCode=" & strAccCode & _
                       "&Location=" & strUserLoc & _
                       "&Komparatif=" & strKomparatif & _
                       "&Saldo=" & strSaldo & _
                       "&RptInit=" & strRptInit & _
                       "&ExportToExcel=" & strExportToExcel & _
                       """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

    End Sub

    Function blnValidEndStartDate(ByVal pv_strEndDate As String, ByVal pv_strStartDate As String) As Boolean
        blnValidEndStartDate = False
        If CDate(pv_strStartDate) <= CDate(pv_strEndDate) Then
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CB_DAILY_GET&errmesg=" & Exp.Message & "&redirect=CB_StdRpt_DailyBank.aspx")
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
