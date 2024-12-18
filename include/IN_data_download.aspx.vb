Imports System
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page

Imports agri.GlobalHdl.clsAccessRights

Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic


Public Class IN_data_download : Inherits Page

    Protected WithEvents tblDownload As HtmlTable
    Protected WithEvents tblSave As HtmlTable

    Protected WithEvents cbProdType As CheckBox
    Protected WithEvents cbProdBrand As CheckBox
    Protected WithEvents cbProdModel As CheckBox
    Protected WithEvents cbProdCat As CheckBox
    Protected WithEvents cbProdMat As CheckBox
    Protected WithEvents cbStockAnalysis As CheckBox
    Protected WithEvents cbStockItem As CheckBox
    Protected WithEvents cbDirectChargeItem As CheckBox
    Protected WithEvents cbPrevMaintenance As CheckBox
    Protected WithEvents cbMiscItem As CheckBox
    Protected WithEvents lblErrGenerate As Label
    Protected WithEvents lblDownloadfile As Label
    Protected WithEvents lblErrMesage As Label
    Protected WithEvents btnGenerate As ImageButton
    Protected WithEvents lnkSaveTheFile As Hyperlink

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intINAR As Integer

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intINAR = Session("SS_INAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INDataTransfer), intINAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If Not Page.IsPostBack Then
                tblDownload.Visible = True
                tblSave.Visible = False
            Else
                tblDownload.Visible = True
                tblSave.Visible = False
            End If
        End If
    End Sub


    Sub btnGenerate_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strParam As String = ""
        Dim strQuery As String = ""

        If cbProdType.Checked Then strParam = strParam & "cbProdType"
        If cbProdBrand.Checked Then strParam = strParam & "cbProdBrand"
        If cbProdModel.Checked Then strParam = strParam & "cbProdModel"
        If cbProdCat.Checked Then strParam = strParam & "cbProdCat"
        If cbProdMat.Checked Then strParam = strParam & "cbProdMat"
        If cbStockAnalysis.Checked Then strParam = strParam & "cbStockAnalysis"
        If cbStockItem.Checked Then strParam = strParam & "cbStockItem"
        If cbDirectChargeItem.Checked Then strParam = strParam & "cbDirectChargeItem"
        If cbPrevMaintenance.Checked Then strParam = strParam & "cbPrevMaintenance"
        
        If strParam = "" Then
            lblErrGenerate.Visible = True
        Else
            strQuery = "prodtype=" & cbProdType.Checked & "&" & _
                    "prodbrand=" & cbProdBrand.Checked & "&" & _
                    "prodmodel=" & cbProdModel.Checked & "&" & _
                    "prodcat=" & cbProdCat.Checked & "&" & _
                    "prodmat=" & cbProdMat.Checked & "&" & _
                    "stockanalysis=" & cbStockAnalysis.Checked & "&" & _
                    "stockitem=" & cbStockItem.Checked & "&" & _
                    "directchargeitem=" & cbDirectChargeItem.Checked & "&" & _
                    "preventivemaintenance=" & cbPrevMaintenance.Checked & "&" 
            
            Response.Redirect("IN_data_download_savefile.aspx?" & strQuery)
        End If

    End Sub



    Sub btnGenerate_Click_Temp()





    









    End Sub

End Class
