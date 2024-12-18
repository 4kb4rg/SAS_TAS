Imports System
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page

Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Imports agri.GlobalHdl.clsAccessRights


Public Class PR_data_wages_download : Inherits Page

    Protected WithEvents tblDownload As HtmlTable
    Protected WithEvents lblDownloadfile As Label
    Protected WithEvents lblErrMesage As Label
    Protected WithEvents lblWagesPeriod As Label

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objAdmAcc As New agri.Admin.clsAccPeriod()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intAccMonth As Integer
    Dim intAccYear As Integer
    Dim strLangCode As String
    Dim intPRAR As Long

    Sub Page_Load(Sender As Object, E As EventArgs)
        Dim objAccCfg As New Data.Dataset()
        Dim strParam As String
        Dim intErrNo As Integer
        Dim strOpCd_AccCfg_Get As String = "ADMIN_CLSACCPERIOD_CONFIG_ACCYEAR_GET"
        Dim intMaxPeriod As Integer

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intPRAR = Session("SS_PRAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDownloadWages), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            intAccMonth = Convert.ToInt16(strAccMonth)
            intAccYear = Convert.ToInt16(strAccYear)
            intAccMonth -= 1
            If intAccMonth = 0 Then
                intAccYear -= 1

                Try
                    strParam = "||" & Convert.ToString(intAccYear)
                    intErrNo = objAdmAcc.mtdGetAccPeriodCfg(strOpCd_AccCfg_Get, _
                                                            strCompany, _
                                                            strLocation, _
                                                            strUserId, _
                                                            strParam, _
                                                            objAccCfg)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_DATA_WAGES_DOWNLOAD&errmesg=" & Exp.ToString() & "&redirect=")
                End Try

                Try
                    intAccMonth = Convert.ToInt16(objAccCfg.Tables(0).Rows(0).Item("MaxPeriod"))
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_DATA_WAGES_DOWNLOAD_MAXPERIOD&errmesg=System required period configuration to process your request. Please set period configuration for the year of " & Convert.ToString(intAccYear) & "&redirect=")
                End Try
                objAccCfg = Nothing
            End If

            lblWagesPeriod.Text = Convert.ToString(intAccMonth) & "/" & Convert.ToString(intAccYear)

            If Not Page.IsPostBack Then
                tblDownload.Visible = True
            Else
                tblDownload.Visible = True
            End If
        End If
    End Sub

    Sub btnGenerate_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("PR_data_wages_savefile.aspx?accmonth=" & Convert.ToString(intAccMonth) & "&accyear=" & Convert.ToString(intAccYear))
    End Sub

End Class
