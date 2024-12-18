
Imports System
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction
Imports Microsoft.VisualBasic.DateAndTime
Imports Microsoft.VisualBasic

Imports System.Data
Imports System.Data.SqlClient

Imports agri.PR
Imports agri.GlobalHdl
Imports agri.PWSystem

Public Class PR_mthend_Transfer : Inherits Page

    Protected WithEvents rbPro1 As RadioButton
    Protected WithEvents rbPro2 As RadioButton
    Protected WithEvents rbPro3 As RadioButton
    Protected WithEvents ddlMonth As DropDownList
    Protected WithEvents ddlYear As DropDownList

    Protected WithEvents txtCompanyCode As TextBox
    Protected WithEvents txtRegBCA As TextBox
    Protected WithEvents txtBankAcc As TextBox
    Protected WithEvents btnGenerate As ImageButton

    Protected WithEvents txtProDate As TextBox
    Protected WithEvents lblErrProcessDate As Label
    Protected WithEvents lblErrProcessDateDesc As Label
    Protected WithEvents btnSelDate As Image

    Dim objPR As New agri.PR.clsMthEnd()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objAdmin As New agri.Admin.clsAccPeriod()    
    
    
    Dim objPayDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intPRAR As Long
    Dim intConfig As Integer
    Dim strDateFmt As String

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        intPRAR = Session("SS_PRAR")
        intConfig = Session("SS_CONFIGSETTING")
        strDateFmt = Session("SS_DATEFMT")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthTransfer), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            
            If Not Page.IsPostBack Then
                txtProDate.Text = objGlobal.GetShortDate(strDateFmt, Now())
                BindAccMonthList(BindAccYearList(strLocation, strAccYear))
            End If
        End If
    End Sub

    
    Sub btnGenerate_Click(Sender As Object, E As ImageClickEventArgs)
         Dim strTxtFileName As String = ""
         Dim strParam As string = ""
         Dim strOpCd As String = "" 
         Dim objFormatDate As String
         Dim objActualDate As String
         
         Dim arrDate As Array
         Dim strDay As String
         Dim strMonth As String
         Dim strYear As String


         If objGlobal.mtdValidInputDate(strDateFmt, _
                                       txtProDate.Text, _
                                       objFormatDate, _
                                       objActualDate) = False Then
            lblErrProcessDateDesc.Text = "Date format :" & objFormatDate
            lblErrProcessDateDesc.Visible = True
            Exit Sub
         End If
         
         arrDate = Split(txtProDate.Text, "/")
         IF objFormatDate = "DD/MM/YYYY" THEN
                strDay = Format(Val(arrDate(0)), "00")
                strMonth = Format(Val(arrDate(1)), "00")
                strYear = arrDate(2)
         ELSE 
                strMonth = Format(Val(arrDate(0)), "00")
                strDay = Format(Val(arrDate(1)), "00")
                strYear = arrDate(2)
         END IF

         strTxtFileName = "FL_" &  Trim(txtCompanyCode.Text)  & ".txt"
         strParam =  ddlMonth.SelectedItem.Text & "|" & ddlYear.SelectedItem.Text & "|" & txtCompanyCode.Text & "|" & txtRegBCA.Text & _
                      "|" & strDay & "|" & strMonth & "|" & strYear & _ 
                      "|" & txtBankAcc.Text & "|" & strTxtFileName

        
         IF rbPro1.Checked= True then strOpCd = "PR_STDRPT_BCA_TRANS_BSC"
         IF rbPro2.Checked= True then strOpCd = "PR_STDRPT_BCA_TRANS_ALW"
         IF rbPro3.Checked= True then strOpCd = "PR_STDRPT_BCA_TRANS"

         Response.Redirect("PR_mthend_Transfer_savefile.aspx?strParam=" & strParam & "&strOpCd=" & strOpCd & "&strTxtFileName=" & strTxtFileName)   

    End Sub

    Sub BindAccMonthList(ByVal pv_intMaxMonth As Integer)
        Dim intCnt As Integer
        Dim intSelIndex As Integer = 0

        ddlMonth.Items.Clear
        For intCnt = 1 To pv_intMaxMonth
            ddlMonth.Items.Add(intCnt)
            If intCnt = Convert.ToInt16(strAccMonth) Then
                intSelIndex = intCnt - 1
            End If
        Next

        ddlMonth.SelectedIndex = intSelIndex
    End Sub

    Function BindAccYearList(ByVal pv_strLocation As String, _
                             ByVal pv_strAccYear As String) As Integer
        Dim strOpCd_Max_Get As String = "ADMIN_CLSACCPERIOD_CONFIG_ALLLOC_MAXPERIOD_GET"
        Dim strOpCd_Dist_Get As String = "ADMIN_CLSACCPERIOD_CONFIG_ACCYEAR_DISTINCT_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intAccYear As Integer
        Dim intMaxPeriod As Integer
        Dim intCnt As Integer
        Dim intSelIndex As Integer
        Dim objAccCfg As New Dataset()

        If pv_strLocation = "" Then
            pv_strLocation = strLocation
        Else
            If Left(pv_strLocation, 3) = "','" Then
                pv_strLocation = Right(pv_strLocation, Len(pv_strLocation) - 3)
            ElseIf Right(pv_strLocation, 3) = "','" Then
                pv_strLocation = Left(pv_strLocation, Len(pv_strLocation) - 3)
            ElseIf Left(pv_strLocation, 1) = "," Then
                pv_strLocation = Right(pv_strLocation, Len(pv_strLocation) - 1)
            ElseIf Right(pv_strLocation, 1) = "," Then
                pv_strLocation = Left(pv_strLocation, Len(pv_strLocation) - 1)
            End If
        End If

        Try
            strParam = "||"
            intErrNo = objAdmin.mtdGetAccPeriodCfg(strOpCd_Dist_Get, _
                                                    strCompany, _
                                                    pv_strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    objAccCfg)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=AP_STDRPT_CTRL_ACCCFG_DIST_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        intSelIndex = 0 
        ddlYear.Items.Clear

        If objAccCfg.Tables(0).Rows.Count > 0 Then      
            For intCnt = 0 To objAccCfg.Tables(0).Rows.Count - 1    
                ddlYear.Items.Add(objAccCfg.Tables(0).Rows(intCnt).Item("AccYear"))

                If objAccCfg.Tables(0).Rows(intCnt).Item("AccYear") = pv_strAccYear Then
                    intSelIndex = intCnt    
                End If
            Next

            ddlYear.SelectedIndex = intSelIndex
            intAccYear = ddlYear.SelectedItem.Value
            Try
                strParam = "||" & intAccYear             
                intErrNo = objAdmin.mtdGetAccPeriodCfg(strOpCd_Max_Get, _
                                                        strCompany, _
                                                        pv_strLocation, _
                                                        strUserId, _
                                                        strParam, _
                                                        objAccCfg)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=AP_STDRPT_CTRL_ACCCFG_MAX_GET&errmesg=" & Exp.ToString() & "&redirect=")
            End Try

            Try
                intMaxPeriod = Convert.ToInt16(objAccCfg.Tables(0).Rows(0).Item("MaxPeriod"))
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=AP_STDRPT_CTLR_ACCCFG_MAXPERIOD&errmesg=System required period configuration to process your request. Please set period configuration for the year of " & Convert.ToString(intAccYear) & "&redirect=")
            End Try

        Else
            ddlYear.Items.Add(strAccYear)    
            ddlYear.SelectedIndex = intSelIndex
            intMaxPeriod = Convert.ToInt16(strAccMonth) 
        End If

        objAccCfg = Nothing
        Return intMaxPeriod
    End Function



End Class
