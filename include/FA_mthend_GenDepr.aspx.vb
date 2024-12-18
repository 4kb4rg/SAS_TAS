Imports System
Imports System.Data
Imports System.IO 
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information
Imports Microsoft.VisualBasic.FileSystem
Imports Microsoft.VisualBasic

Imports agri.HR
Imports agri.PR
Imports agri.GlobalHdl
Imports agri.PWSystem

Public Class FA_mthend_GenDepr : Inherits Page

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblProcessed As Label
    Protected WithEvents lblRollBack As Label
    Protected WithEvents lblNoProcessed As Label
    Protected WithEvents lblErr As Label

    Protected WithEvents lblPeriod As Label

    Protected WithEvents btnGenerate As ImageButton
    Protected WithEvents btnRollBack As ImageButton

    Dim objFA As New agri.FA.clsMthEnd()
    Dim objFASetup As New agri.FA.clsSetup()
    Dim objFATrx As New agri.FA.clsTrx()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objEmpDs As New Object()


    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intFAAR As Integer
    Dim intConfig As Integer
    Dim blnAutoIncentive As Boolean = False

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        'strAccMonth = Session("SS_FAACCMONTH")
        'strAccYear = Session("SS_FAACCYEAR")
        intFAAR = Session("SS_FAAR")
        intConfig = Session("SS_CONFIGSETTING")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAGenDepreciation), intFAAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else

            'get current monthend status
            lblNoProcessed.Visible = False
            lblErr.Visible = False
            lblProcessed.Visible = False
            lblRollBack.Visible = False

            Call GetPeriode()

        End If
    End Sub

    Private Sub GetPeriode()

        Dim intErrNo As Integer
        Dim strParamName As String
        Dim strParamValue As String
        Dim strOpCode As String
        Dim objPrdSet As DataSet

        strOpCode = "FA_CLSMTHEND_GET"
        strParamName = "LOCCODE|MODULECODE"
        strParamValue = strLocation & "|21"

        Try
            intErrNo = objFATrx.mtdGetDataCommon(strOpCode, _
                                               strParamName, _
                                               strParamValue, _
                                               objPrdSet)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=FA_MTHEND_GENDEPR&errmesg=&redirect=")
        End Try

        If objPrdSet.Tables(0).Rows.Count > 0 Then

            strAccMonth = Trim(objPrdSet.Tables(0).Rows(0).Item("CurrAccMonth"))
            strAccYear = Trim(objPrdSet.Tables(0).Rows(0).Item("CurrAccYear"))

            lblPeriod.Text = Trim(objPrdSet.Tables(0).Rows(0).Item("CurrAccMonth")) & "/" & Trim(objPrdSet.Tables(0).Rows(0).Item("CurrAccYear"))
            If Trim(objPrdSet.Tables(0).Rows(0).Item("PostedInd")) = "2" Then
                btnGenerate.Visible = False
                btnRollBack.Visible = True
            Else
                btnGenerate.Visible = True
                btnRollBack.Visible = False
            End If
        Else
            btnGenerate.Visible = False
            btnRollBack.Visible = False
            lblErr.Text = "No Akun Periode Defined, Please Contact System Administartor"
            lblErr.Visible = True
        End If

    End Sub

    Sub btnGenerate_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim intErrNo As Integer
        Dim strParamName As String
        Dim strParamValue As String
        Dim strOpCode As String
        Dim objDataSet As DataSet
        Dim strPeriode As String

        strOpCode = "FA_GENERATE_DEPRESIATION"
        strParamName = "PERIODE|ACCMONTH|ACCYEAR|LOCCODE|MODIFYBY"

        If Len(strAccMonth) = 1 Then
            strPeriode = strAccYear & "0" & strAccMonth
        Else
            strPeriode = strAccYear & strAccMonth
        End If

        strParamValue = strPeriode & "|" & strAccMonth & "|" & strAccYear & "|" & strLocation & "|" & strUserId

        Try
            intErrNo = objFATrx.mtdGetDataCommon(strOpCode, _
                                               strParamName, _
                                               strParamValue, _
                                               objDataSet)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=FA_MTHEND_GENDEPR&errmesg=&redirect=")
        End Try


        If objDataSet.Tables(0).Rows(0).Item("Msg") = "OK" Then
            lblProcessed.Text = "Generate Depreciation Succeed.."
            lblProcessed.Visible = True
        Else
            lblErr.Text = objDataSet.Tables(0).Rows(0).Item("Msg")
            lblErr.Visible = True
        End If

        Call GetPeriode()


    End Sub

    Sub btnRollBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)

        Dim intErrNo As Integer
        Dim strParamName As String
        Dim strParamValue As String
        Dim strOpCode As String
        Dim objDataSet As DataSet

        strOpCode = "FA_GENERATE_ROLLBACK"
        strParamName = "LOCCODE|ACCMONTH|ACCYEAR"
        strParamValue = strLocation & "|" & strAccMonth & "|" & strAccYear

        Try
            intErrNo = objFATrx.mtdGetDataCommon(strOpCode, _
                                               strParamName, _
                                               strParamValue, _
                                               objDataSet)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=FA_MTHEND_GENDEPR&errmesg=&redirect=")
        End Try


        If objDataSet.Tables(0).Rows(0).Item("Msg") = "OK" Then
            lblRollBack.Text = "Rollback Succeed.."
            lblRollBack.Visible = True
        Else
            lblErr.Text = objDataSet.Tables(0).Rows(0).Item("Msg")
            lblErr.Visible = True
        End If

        Call GetPeriode()
        
    End Sub

End Class
