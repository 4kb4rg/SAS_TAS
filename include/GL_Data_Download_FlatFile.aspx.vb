
Imports System
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page

Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Imports agri.GlobalHdl.clsAccessRights


Public Class GL_Data_Download_FlatFile : Inherits Page

    Protected WithEvents tblDownload As HtmlTable
    Protected WithEvents tblSave As HtmlTable

    Protected WithEvents cbHQORD As CheckBox
    Protected WithEvents cbHQORC As CheckBox
    Protected WithEvents cbHQGLD As CheckBox
    Protected WithEvents cbHQGLC As CheckBox
    Protected WithEvents cbHQXTD As CheckBox
    Protected WithEvents cbHQXTC As CheckBox
    Protected WithEvents cbHQXRD As CheckBox
    Protected WithEvents cbHQXRC As CheckBox
    Protected WithEvents cbHQOBD As CheckBox
    Protected WithEvents cbHQOBC As CheckBox
    Protected WithEvents cbAll As CheckBox
    Protected WithEvents txtCompCode As TextBox
    Protected WithEvents txtCropCode As TextBox
    Protected WithEvents txtCurrency As TextBox
    Protected WithEvents lblErrGenerate As Label
    Protected WithEvents lblDownloadfile As Label
    Protected WithEvents lblErrMesage As Label
    Protected WithEvents lblErrCompCode As Label
    Protected WithEvents lblErrCropCode As Label
    Protected WithEvents lblErrCurrency As Label
    Protected WithEvents ddlAccMonth As DropDownList
    Protected WithEvents ddlAccYear As DropDownList
    Protected WithEvents btnGenerate As ImageButton
    Protected WithEvents lnkSaveTheFile As Hyperlink

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGLData As New agri.GL.clsData()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intGLAR As Integer
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intConfigsetting As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intGLAR = Session("SS_GLAR")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        intConfigsetting = Session("SS_CONFIGSETTING")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLUpload), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else

            If Not Page.IsPostBack Then
                tblDownload.Visible = True
                BindAccPeriod()
                Exit Sub
            Else
                tblDownload.Visible = True
            End If
            lblErrCompCode.Visible = False
            lblErrCropCode.Visible = False
            lblErrCurrency.Visible = False
        End If
    End Sub


    Sub btnGenerate_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim SelAccMonth As String = ""
        Dim SelAccYear As String = ""
        Dim SelCompCode As String = ""
        Dim SelCropCode As String = ""
        Dim SelCurrency As String = ""
        Dim strParam As String = ""
        Dim strQuery As String = ""


        If txtCompCode.Text = "" Then
            lblErrCompCode.Visible = True
            Exit Sub
        ElseIf txtCropCode.Text = "" Then
            lblErrCropCode.Visible = True
            Exit Sub
        ElseIf txtCurrency.Text = "" Then
            lblErrCurrency.Visible = True
            Exit Sub
        End If

        SelAccMonth = ddlAccMonth.SelectedItem.Value
        SelAccYear = ddlAccYear.SelectedItem.Value
        SelCompCode = txtCompCode.Text
        SelCropCode = txtCropCode.Text
        SelCurrency = txtCurrency.Text

        If cbHQORD.Checked Then strParam = strParam & "cbHQORD"
        If cbHQORC.Checked Then strParam = strParam & "cbHQORC"
        If cbHQGLD.Checked Then strParam = strParam & "cbHQGLD"
        If cbHQGLC.Checked Then strParam = strParam & "cbHQGLC"
        If cbHQXTD.Checked Then strParam = strParam & "cbHQXTD"
        If cbHQXTC.Checked Then strParam = strParam & "cbHQXTC"
        If cbHQXRD.Checked Then strParam = strParam & "cbHQXRD"
        If cbHQXRC.Checked Then strParam = strParam & "cbHQXRC"
        If cbHQOBD.Checked Then strParam = strParam & "cbHQOBD"
        If cbHQOBC.Checked Then strParam = strParam & "cbHQOBC"

        If strParam = "" Then
            lblErrGenerate.Visible = True
            Exit Sub
        Else
            strQuery = "HQORD=" & cbHQORD.Checked & "&"
            strQuery += "HQORC=" & cbHQORC.Checked & "&"
            strQuery += "HQGLD=" & cbHQGLD.Checked & "&"
            strQuery += "HQGLC=" & cbHQGLC.Checked & "&"
            strQuery += "HQXTD=" & cbHQXTD.Checked & "&"
            strQuery += "HQXTC=" & cbHQXTC.Checked & "&"
            strQuery += "HQXRD=" & cbHQXRD.Checked & "&"
            strQuery += "HQXRC=" & cbHQXRC.Checked & "&"
            strQuery += "HQOBD=" & cbHQOBD.Checked & "&"
            strQuery += "HQOBC=" & cbHQOBC.Checked & "&"
            strQuery += "SelAccMonth=" & SelAccMonth & "&"
            strQuery += "SelAccYear=" & SelAccYear & "&"
            strQuery += "SelCompCode=" & SelCompCode & "&"
            strQuery += "SelCropCode=" & SelCropCode & "&"
            strQuery += "SelCurrency=" & SelCurrency & "&"
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
                strQuery += "SelStatus=" & objGLSetup.EnumBlockStatus.Active & "&"
                strQuery += "SelBlockStatus=YES" & "&"
                strQuery += "SelSubBlockStatus="
            Else
                strQuery += "SelStatus=" & objGLSetup.EnumSubBlockStatus.Active & "&"
                strQuery += "SelBlockStatus=" & "&"
                strQuery += "SelSubBlockStatus=YES"
            End If

            Response.Redirect("GL_Data_Download_FlatFile_Save.aspx?" & strQuery)
        End If
    End Sub

    Sub BindAccPeriod()
        Dim intYear As Integer
        Dim intMth As Integer
        Dim intErrNo As Integer
        Dim MaxAccMth As String
        Dim objAccPeriod As Object
        Dim OpCode As String = "GL_CLSDATA_GET_ACCPERIOD"

        intErrNo = objGLData.mtdGetAccPeriod(OpCode, _
                                             strLocation, _
                                             objAccPeriod)

        For intYear = 0 To objAccPeriod.Tables(0).Rows.Count - 1

            ddlAccYear.Items.Add(New ListItem(objAccPeriod.tables(0).Rows(intYear).item("AccYear")))

            If objAccPeriod.tables(0).Rows(intYear).item("AccYear") = strAccYear Then
                ddlAccYear.SelectedIndex = intYear
            End If

            If objAccPeriod.tables(0).rows(intYear).item("MaxPeriod") > MaxAccMth Then
                MaxAccMth = objAccPeriod.tables(0).rows(intYear).item("MaxPeriod")
                ddlAccMonth.Items.Clear()

                For intMth = 1 To MaxAccMth
                    ddlAccMonth.Items.Add(New ListItem(intMth))
                    If intMth = strAccMonth Then
                        ddlAccMonth.SelectedIndex = intMth - 1
                    End If
                Next

            End If
        Next

    End Sub

End Class
