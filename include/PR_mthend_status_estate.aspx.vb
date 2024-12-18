Imports System
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic

Imports System.Data
Imports System.Data.SqlClient


Imports agri.HR
Imports agri.PR
Imports agri.GlobalHdl
Imports agri.PWSystem

Public Class PR_mthend_status_estate : Inherits Page

    Protected WithEvents ddlMonth As DropDownList
    Protected WithEvents ddlyear As DropDownList
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents dgprocess As DataGrid
	Protected WithEvents btnGenerate As ImageButton

    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()


    Dim objOk As New agri.GL.ClsTrx()
    Dim objHR As New agri.HR.clsTrx()
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objPR As New agri.PR.clsMthEnd()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objLangCapDs As New Object()
    Dim objEmpDs As New Object()

    Dim strCompany As String
    Dim strLocation As String

    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intPRAR As Long
    Dim strAcceptFormat As String

    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String
	Dim intLevel As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")

        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_SELACCMONTH")
        strAccYear = Session("SS_SELACCYEAR")
        intPRAR = Session("SS_PRAR")
		intLevel = Session("SS_USRLEVEL")

        strLocType = Session("SS_LOCTYPE")
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthPayroll), intPRAR) = False Or intLevel = 0 Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            btnGenerate.Attributes("onclick") = "javascript:return confirm('Anda yakin akan confirm data payroll ?');"
            lblErrMessage.Text = ""
            If Not Page.IsPostBack Then
                ddlMonth.SelectedIndex = Cint(Session("SS_SELACCMONTH")) - 1
                BindAccYear(Session("SS_SELACCYEAR"))
				HitView()
            End If
        End If
    End Sub

	Public Sub UserMsgBox(ByVal F As Object, ByVal sMsg As String)
        Dim sb As New StringBuilder()
        Dim oFormObject As System.Web.UI.Control = Nothing
        Try
            sMsg = sMsg.Replace("'", "\'")
            sMsg = sMsg.Replace(Chr(34), "\" & Chr(34))
            sMsg = sMsg.Replace(vbCrLf, "\n")
            sMsg = "<script language='javascript'>alert('" & sMsg & "');</script>"
            sb = New StringBuilder()
            sb.Append(sMsg)
            For Each oFormObject In F.Controls
                If TypeOf oFormObject Is HtmlForm Then
                    Exit For
                End If
            Next
            oFormObject.Controls.AddAt(oFormObject.Controls.Count, New LiteralControl(sb.ToString()))
        Catch ex As Exception

        End Try
    End Sub

    Sub BindAccYear(ByVal pv_strAccYear As String)
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim intSelectedIndex As Integer
        Dim objAccYearDs As New Object
        Dim strOpCd As String = "ADMIN_CLSACCPERIOD_CONFIG_GET"

        strParamName = "LOCCODE|SEARCHSTR|SORTEXP"
        strParamValue = strLocation & "||Order By HD.AccYear"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objAccYearDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & Exp.Message & "&redirect=")
        End Try

        For intCnt = 0 To objAccYearDs.Tables(0).Rows.Count - 1
            objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear") = Trim(objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear"))
            objAccYearDs.Tables(0).Rows(intCnt).Item("UserName") = Trim(objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear"))
            If objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear") = pv_strAccYear Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        ddlyear.DataSource = objAccYearDs.Tables(0)
        ddlyear.DataValueField = "AccYear"
        ddlyear.DataTextField = "UserName"
        ddlyear.DataBind()
        ddlyear.SelectedIndex = intSelectedIndex - 1
    End Sub

	Sub HitView()
        Dim strOpCdSP As String = "PR_PR_TRX_MTHEND_GET"
        Dim objDataSet As New Object()
        Dim intErrNo As Integer
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim strYr As String = ddlyear.SelectedItem.Value.Trim

       

        ParamName = "YR|LOC"
        ParamValue = strYr & "|" & _
		             strlocation 
        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCdSP, ParamName, ParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_MTHEND_GET&errmesg=" & Exp.Message )
        End Try

        dgprocess.DataSource = Nothing
        dgprocess.DataBind()
        dgprocess.DataSource = objDataSet.Tables(0)
        dgprocess.DataBind()

    End Sub
   	
    
    Sub btnGenerate_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd As String = "PR_PR_TRX_MTHEND_CONF"
        Dim strMn As String
        Dim strYr As String
       
        Dim i As Integer
        Dim ParamName As String
        Dim ParamValue As String
        Dim intErrNo As Integer
		Dim objDataSet As New Object()

        strMn = ddlMonth.SelectedItem.Value.Trim
        strYr = ddlyear.SelectedItem.Value.Trim

        ParamName = "MN|YR|LOC"
        ParamValue = strMn & "|" & _
                     strYr & "|" & _
                     strLocation 

                 Try
                    intErrNo = objOk.mtdGetDataCommon(strOpCd, ParamName, ParamValue, objDataSet)
					UserMsgBox(Me,objDataSet.Tables(0).Rows(0).Item("Msg"))
					HitView()
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_MTHEND_CONF&errmesg=" & Exp.Message & "&redirect=")
                End Try
    End Sub
	
	Sub ddlyear_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
       HitView()
    End Sub



End Class
