Imports System
Imports System.Data
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports System.Globalization

Public Class IN_Approval_Level : Inherits Page
  
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objGLTrx As New agri.GL.clsTrx()
    Dim objDataSet As New DataSet()

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblCreateDate As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdateBy As Label

    Protected WithEvents txtLevel1From As TextBox
    Protected WithEvents txtLevel1To As TextBox
    Protected WithEvents txtLevel2From As TextBox
    Protected WithEvents txtLevel2To As TextBox
    Protected WithEvents txtLevel3From As TextBox
    Protected WithEvents txtLevel3To As TextBox
    Protected WithEvents txtLevel4From As TextBox
    Protected WithEvents txtLevel4To As TextBox

    Protected WithEvents Save As ImageButton
    Dim intErrNo As Integer

    Dim strLocation As String
    Dim strUserId As String
    

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
    
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then

                DisplayData()
            End If
        End If
    End Sub

    Protected Function LoadData() As DataSet

        Dim strOpCode As String = "IN_APPROVAL_LEVEL_GET"
        Dim intErrNo As Integer
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        strParamName = "LOCCODE"
        strParamValue = strLocation

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BUDGET_GET_DATA&errmesg=" & lblErrMessage.Text & "&redirect=gl/trx/gl_trx_budget_list.aspx")
        End Try

        Return objDataSet

    End Function


    Sub DisplayData()

        Dim dsData As DataSet = LoadData()

        If dsData.Tables(0).Rows.Count > 0 Then

            txtLevel1From.Text = dsData.Tables(0).Rows(0).Item("Level1From")
            txtLevel1To.Text = dsData.Tables(0).Rows(0).Item("Level1To")
            txtLevel2From.Text = dsData.Tables(0).Rows(0).Item("Level2From")
            txtLevel2To.Text = dsData.Tables(0).Rows(0).Item("Level2To")
            txtLevel3From.Text = dsData.Tables(0).Rows(0).Item("Level3From")
            txtLevel3To.Text = dsData.Tables(0).Rows(0).Item("Level3To")
            txtLevel4From.Text = dsData.Tables(0).Rows(0).Item("Level4From")
            txtLevel4To.Text = dsData.Tables(0).Rows(0).Item("Level4To")

            lblCreateDate.Text = objGlobal.GetLongDate(Trim(dsData.Tables(0).Rows(0).Item("CreateDate")))
            lblLastUpdate.Text = objGlobal.GetLongDate(Trim(dsData.Tables(0).Rows(0).Item("UpdateDate")))
            lblUpdateBy.Text = Trim(dsData.Tables(0).Rows(0).Item("Username"))

        End If
    End Sub

    Sub btnSave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        
        Dim strOpCode As String = "IN_APPROVAL_LEVEL_INSERT"
        Dim intErrNo As Integer
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        strParamName = "LOCCODE|USERID|FROM1|TO1|FROM2|TO2|FROM3|TO3|FROM4|TO4"
        strParamValue = strLocation & "|" & strUserId & "|" & _
                        txtLevel1From.Text & "|" & _
                        txtLevel1To.Text & "|" & _
                        txtLevel2From.Text & "|" & _
                        txtLevel2To.Text & "|" & _
                        txtLevel3From.Text & "|" & _
                        txtLevel3To.Text & "|" & _
                        txtLevel4From.Text & "|" & _
                        txtLevel4To.Text

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BUDGET_GET_DATA&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objDataSet.Tables(0).Rows.Count > 0 Then

            txtLevel1From.Text = objDataSet.Tables(0).Rows(0).Item("Level1From")
            txtLevel1To.Text = objDataSet.Tables(0).Rows(0).Item("Level1To")
            txtLevel2From.Text = objDataSet.Tables(0).Rows(0).Item("Level2From")
            txtLevel2To.Text = objDataSet.Tables(0).Rows(0).Item("Level2To")
            txtLevel3From.Text = objDataSet.Tables(0).Rows(0).Item("Level3From")
            txtLevel3To.Text = objDataSet.Tables(0).Rows(0).Item("Level3To")
            txtLevel4From.Text = objDataSet.Tables(0).Rows(0).Item("Level4From")
            txtLevel4To.Text = objDataSet.Tables(0).Rows(0).Item("Level4To")

            lblCreateDate.Text = objGlobal.GetLongDate(Trim(objDataSet.Tables(0).Rows(0).Item("CreateDate")))
            lblLastUpdate.Text = objGlobal.GetLongDate(Trim(objDataSet.Tables(0).Rows(0).Item("UpdateDate")))
            lblUpdateBy.Text = Trim(objDataSet.Tables(0).Rows(0).Item("Username"))

        End If

    End Sub

   
End Class
