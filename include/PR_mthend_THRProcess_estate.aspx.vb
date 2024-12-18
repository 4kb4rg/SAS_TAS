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

Public Class PR_mthend_THRProcess_estate : Inherits Page

    Protected WithEvents rbAllEmp As RadioButton
    Protected WithEvents rbSelectedEmp As RadioButton
    Protected WithEvents ddlEmployee As DropDownList
    Protected WithEvents btnGenerate As ImageButton
    Protected WithEvents lblCompleteSetup As Label

    Protected WithEvents ddlMonth As DropDownList
    Protected WithEvents ddlyear As DropDownList

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents dgprocess As DataGrid

    
  
    Protected WithEvents txthrdate As TextBox
    Protected WithEvents txtdaging As TextBox

    Protected WithEvents ddlreligion As DropDownList
  

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

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")

        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        intPRAR = Session("SS_PRAR")

        strLocType = Session("SS_LOCTYPE")
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        'ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthRice), intPRAR) = False Then
        '    Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrMessage.Text = ""
            If Not Page.IsPostBack Then
                ddlMonth.SelectedIndex = Month(Now()) - 1
                BindAccYear(Year(Now()))
                BindReligion()
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

    Function Date_Validation(ByVal pv_strInputDate As String, ByVal pv_blnIsShortDate As Boolean) As String
        Dim objSysCfgDs As New Object()
        Dim objActualDate As New Object()
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
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GR_GET_CONFIG&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_GRList.aspx")
        End Try

        strDateFormat = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))

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

    

    Sub BindReligion()
        Dim strOpCd_Religion As String = "HR_CLSSETUP_RELIGION_LIST_GET"
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objDs As New Object()
        Dim intReligionIndex As Integer = 0
        Dim dr As DataRow


        ParamName = "SEARCHSTR|SORTEXP"
        ParamValue = "AND REL.Status='1'|ORDER By ReligionCode"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_Religion, ParamName, ParamValue, objDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_CLSSETUP_RELIGION_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objDs.Tables(0).Rows.Count - 1
                objDs.Tables(0).Rows(intCnt).Item("ReligionCode") = Trim(objDs.Tables(0).Rows(intCnt).Item("ReligionCode"))
                objDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objDs.Tables(0).Rows(intCnt).Item("ReligionCode")) & " (" & _
                                                                           Trim(objDs.Tables(0).Rows(intCnt).Item("Description")) & ")"

            Next
        End If

        dr = objDs.Tables(0).NewRow()
        dr("ReligionCode") = ""
        dr("Description") = "Pilih Agama"
        objDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlReligion.DataSource = objDs.Tables(0)
        ddlReligion.DataTextField = "Description"
        ddlReligion.DataValueField = "ReligionCode"
        ddlReligion.DataBind()
        ddlReligion.SelectedIndex = intReligionIndex
    End Sub

    Sub ShowHideDiv()
        Dim found As Control = Me.FindControl("divprocess")
        If Not found Is Nothing Then
            Dim cast As HtmlGenericControl = CType(found, HtmlGenericControl)
            If Not cast Is Nothing Then
                cast.Visible = True
            Else
                cast.Visible = False
            End If
        End If
    End Sub

    Sub btnGenerate_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCdSP As String = "PR_PR_MTHEND_THR_PROCESS_SP"
        Dim objDataSet As New Object()
        Dim intErrNo As Integer
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
		Dim strRel as string
        Dim strMn As String
        Dim strYr As String
        Dim strEc As String = ""
		Dim strPDate as String
		Dim strHDate as string
		Dim strdaging as string

        
        strMn = ddlMonth.SelectedItem.Value.Trim
        strYr = ddlyear.SelectedItem.Value.Trim
        strEc = ""
 
	    strrel = ddlreligion.SelectedItem.Value.Trim()
		    If strrel = "" Then
                lblErrMessage.Text = "Silakan pilih agama"
                lblErrMessage.Visible = True
                Exit Sub
            End If
		 
		
		strHDate = Date_Validation(txthrdate.text.trim(),False)
			if strHDate = "" Then
			    lblErrMessage.Text = "Silakan isi tanggal cut off"
                lblErrMessage.Visible = True
                Exit Sub
            End If
		
		
		strdaging = iif(txtdaging.text.trim()="","0",txtdaging.text.trim())
		
        ParamName = "HEADER|ACCMONTH|ACCYEAR|LOC|UI|RL|DG|HD|EC"
        ParamValue =  "THR/" & strLocation & "/|" & _ 
		              strMn & "|" & _
					  strYr & "|" & _
					  strLocation & "|" & _
					  strUserId & "|" & _
					  strrel & "|" & _
					  strdaging & "|" & _
					  strHDate & "|" & _
					  strEc

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCdSP, ParamName, ParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_MTHEND_PAYROLL_PROCESS_SP&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objDataSet.Tables(0).Rows.Count = 0 Then
            lblErrMessage.Text = "No Record Created"
        ElseIf objDataSet.Tables(0).Rows.Count > 0 Then
            lblErrMessage.Text = "Process Success"
        Else
            lblErrMessage.Text = "Process Failed"
        End If

        UserMsgBox(Me, lblErrMessage.Text)

        If objDataSet.Tables(0).Rows.Count > 0 Then
            ShowHideDiv()
             HitView()
        End If


    End Sub

    Sub HitView()
        Dim strOpCdSP As String = "PR_PR_STDRPT_THR_REPORT"
        Dim objDataSet As New Object()
        Dim intErrNo As Integer
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim strMn As String
        Dim strYr As String

        If ddlMonth.SelectedItem.Value < 10 Then
            strMn = "0" & ddlMonth.SelectedItem.Value.Trim
        Else
            strMn = ddlMonth.SelectedItem.Value.Trim
        End If

        strYr = ddlyear.SelectedItem.Value.Trim

        ParamName = "LOC|AY|REL"
        ParamValue = strlocation & "|" & _
					 strYr & "|" & _
					 ddlreligion.SelectedItem.Value.Trim() 

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCdSP, ParamName, ParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_MTHEND_RICERATION_PROCESS_SP&errmesg=" & Exp.Message )
        End Try

        dgprocess.DataSource = Nothing
        dgprocess.DataBind()
        dgprocess.DataSource = objDataSet.Tables(0)
        dgprocess.DataBind()

    End Sub

   
End Class
