Imports System
Imports System.Data
Imports System.Collections 
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.GL
Imports agri.PWSystem


Public Class PR_setup_AttdList : Inherits Page

    Protected WithEvents lblTracker As Label
    Protected WithEvents txtAttdCode As TextBox
    Protected WithEvents txtqty As TextBox
    Protected WithEvents txtDescription As TextBox
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents SearchBtn As Button
    Protected WithEvents btnSave As ImageButton
    Protected WithEvents txtdt1 As TextBox
    Protected WithEvents txtdt2 As TextBox
    Protected WithEvents txtdt3 As TextBox
    Protected WithEvents txtdt4 As TextBox
    Protected WithEvents txtdt5 As TextBox
    Protected WithEvents txtdt6 As TextBox
    Protected WithEvents txtdt7 As TextBox
    Protected WithEvents txtdt8 As TextBox
    Protected WithEvents txtdt9 As TextBox
    Protected WithEvents txtdt10 As TextBox
    Protected WithEvents txtdt11 As TextBox
    Protected WithEvents txtdt12 As TextBox

    Protected WithEvents txtyr1 As TextBox
    Protected WithEvents txtyr2 As TextBox
    Protected WithEvents txtyr3 As TextBox
    Protected WithEvents txtyr4 As TextBox
    Protected WithEvents txtyr5 As TextBox
    Protected WithEvents txtyr6 As TextBox
    Protected WithEvents txtyr7 As TextBox
    Protected WithEvents txtyr8 As TextBox
    Protected WithEvents txtyr9 As TextBox
    Protected WithEvents txtyr10 As TextBox
    Protected WithEvents txtyr11 As TextBox
    Protected WithEvents txtyr12 As TextBox



    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim ObjOK As New agri.GL.ClsTrx
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objSysCfg As New agri.PWSystem.clsConfig()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intPRAR As Long
    Dim strAcceptFormat As String
    Dim StrCoid As String = ""
	Dim intLevel As Integer
    Dim Prefix As String = "COF"



    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intPRAR = Session("SS_PRAR")
		intLevel = Session("SS_USRLEVEL")
		SearchBtn.Attributes("onclick") = "setall()"
            

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
         ElseIf (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRPaySetup), intPRAR) = False) and (intLevel < 2) Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.Text = "" Then
                SortExpression.Text = "Attd.AttCode"
            End If
			If Not Page.IsPostBack Then
				if txtAttdCode.text.trim() = "" then
					txtAttdCode.Text = Year(Now())
				end if
		        load_data()
			End If
			
			
        End If

		
    End Sub

    Function getCode() As String
        Dim strOpCd_GetID As String = "ALL_ALL_GETID"
        Dim objNewID As New Object
        Dim intErrNo As Integer

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_GetID, "PREFIX|LOCID|LEN", Prefix & "||8", objNewID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ALL_ALL_GETID&errmesg=" & Exp.Message & "&redirect=")
        End Try

        Return Trim(objNewID.Tables(0).Rows(0).Item("CURID"))
    End Function



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

    Sub load_data()
        Dim strOpcd_get As String = "PR_PR_STP_CUTOFF_GET"
        Dim strOpcd_getln As String = "PR_PR_STP_CUTOFF_GETLN"

        Dim objCutOff As New Object()
        Dim objCutOffln As New Object()
        Dim intErrNo As Integer

        Dim ParamNama As String
        Dim ParamValue As String
        Dim intcnt As Integer

        ParamNama = "SEARCH"
        ParamValue = "AND COYear='" & Trim(txtAttdCode.Text) & "' AND LocCode='" & strLocation & "'"

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpcd_get, ParamNama, ParamValue, objCutOff)

        Catch ex As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_CUTOFF_GET&errmesg=" & ex.ToString() & "&redirect=PR/Setup/PR_SETUP_CUTOFF_ESTATE.aspx")

        End Try

        If objCutOff.Tables(0).Rows.Count > 0 Then
            StrCoid = objCutOff.Tables(0).Rows(intcnt).Item("COid").Trim()
            txtAttdCode.Text = objCutOff.Tables(0).Rows(intcnt).Item("COYear").Trim()
            txtqty.Text = objCutOff.Tables(0).Rows(intcnt).Item("Qty").ToString
            ParamNama = "ID"
            ParamValue = StrCoid

            Try
                intErrNo = ObjOK.mtdGetDataCommon(strOpcd_getln, ParamNama, ParamValue, objCutOffln)

            Catch ex As Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_CUTOFF_GETLN&errmesg=" & ex.ToString() & "&redirect=PR/Setup/PR_SETUP_CUTOFF_ESTATE.aspx")

            End Try

            If objCutOffln.Tables(0).Rows.Count > 0 Then
                For intcnt = 0 To objCutOffln.Tables(0).Rows.Count - 1
                    Select Case intcnt
                        Case 0 : txtyr1.Text = objCutOffln.Tables(0).Rows(intcnt).Item("COYear").Trim()
                            txtdt1.Text = Format(objCutOffln.Tables(0).Rows(intcnt).Item("DateCO"), "dd/MM/yyyy")
                        Case 1 : txtyr2.Text = objCutOffln.Tables(0).Rows(intcnt).Item("COYear").Trim()
                            txtdt2.Text = Format(objCutOffln.Tables(0).Rows(intcnt).Item("DateCO"), "dd/MM/yyyy")
                        Case 2 : txtyr3.Text = objCutOffln.Tables(0).Rows(intcnt).Item("COYear").Trim()
                            txtdt3.Text = Format(objCutOffln.Tables(0).Rows(intcnt).Item("DateCO"), "dd/MM/yyyy")
                        Case 3 : txtyr4.Text = objCutOffln.Tables(0).Rows(intcnt).Item("COYear").Trim()
                            txtdt4.Text = Format(objCutOffln.Tables(0).Rows(intcnt).Item("DateCO"), "dd/MM/yyyy")
                        Case 4 : txtyr5.Text = objCutOffln.Tables(0).Rows(intcnt).Item("COYear").Trim()
                            txtdt5.Text = Format(objCutOffln.Tables(0).Rows(intcnt).Item("DateCO"), "dd/MM/yyyy")
                        Case 5 : txtyr6.Text = objCutOffln.Tables(0).Rows(intcnt).Item("COYear").Trim()
                            txtdt6.Text = Format(objCutOffln.Tables(0).Rows(intcnt).Item("DateCO"), "dd/MM/yyyy")
                        Case 6 : txtyr7.Text = objCutOffln.Tables(0).Rows(intcnt).Item("COYear").Trim()
                            txtdt7.Text = Format(objCutOffln.Tables(0).Rows(intcnt).Item("DateCO"), "dd/MM/yyyy")
                        Case 7 : txtyr8.Text = objCutOffln.Tables(0).Rows(intcnt).Item("COYear").Trim()
                            txtdt8.Text = Format(objCutOffln.Tables(0).Rows(intcnt).Item("DateCO"), "dd/MM/yyyy")
                        Case 8 : txtyr9.Text = objCutOffln.Tables(0).Rows(intcnt).Item("COYear").Trim()
                            txtdt9.Text = Format(objCutOffln.Tables(0).Rows(intcnt).Item("DateCO"), "dd/MM/yyyy")
                        Case 9 : txtyr10.Text = objCutOffln.Tables(0).Rows(intcnt).Item("COYear").Trim()
                            txtdt10.Text = Format(objCutOffln.Tables(0).Rows(intcnt).Item("DateCO"), "dd/MM/yyyy")
                        Case 10 : txtyr11.Text = objCutOffln.Tables(0).Rows(intcnt).Item("COYear").Trim()
                            txtdt11.Text = Format(objCutOffln.Tables(0).Rows(intcnt).Item("DateCO"), "dd/MM/yyyy")
                        Case 11 : txtyr12.Text = objCutOffln.Tables(0).Rows(intcnt).Item("COYear").Trim()
                            txtdt12.Text = Format(objCutOffln.Tables(0).Rows(intcnt).Item("DateCO"), "dd/MM/yyyy")

                    End Select
                Next
            End If
        End If
    End Sub


    Sub btnSave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim intErrNo As Integer
        Dim ParamNama As String
        Dim ParamValue As String
        Dim strOpCd_Up As String = "PR_PR_STP_CUTOFF_UPD"
        Dim objNewID As New Object

        StrCoid = getCode()
        ParamNama = "ID|YR|QTY|COMP|LOC|CD|UD|UI|D1|D2|D3|D4|D5|D6|D7|D8|D9|D10|D11|D12"
        ParamValue = StrCoid & "|" & Trim(txtAttdCode.Text) & "|" & Trim(txtqty.Text) & "|" & strCompany & "|" & strLocation & "|" & Format(Now(), "yyyyMMdd hh:mm:ss") & "|" & Format(Now(), "yyyyMMdd hh:mm:ss") & "|" & strUserId & "|" & _
                     Date_Validation(txtdt1.Text, False) & "|" & _
                     Date_Validation(txtdt2.Text, False) & "|" & _
                     Date_Validation(txtdt3.Text, False) & "|" & _
                     Date_Validation(txtdt4.Text, False) & "|" & _
                     Date_Validation(txtdt5.Text, False) & "|" & _
                     Date_Validation(txtdt6.Text, False) & "|" & _
                     Date_Validation(txtdt7.Text, False) & "|" & _
                     Date_Validation(txtdt8.Text, False) & "|" & _
                     Date_Validation(txtdt9.Text, False) & "|" & _
                     Date_Validation(txtdt10.Text, False) & "|" & _
                     Date_Validation(txtdt11.Text, False) & "|" & _
                     Date_Validation(txtdt12.Text, False)

        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOpCd_Up, ParamNama, ParamValue)
        Catch ex As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_CUTOFF_UPD&errmesg=" & ex.ToString() & "&redirect=PR/Setup/PR_SETUP_CUTOFF_ESTATE.aspx")
        End Try




    End Sub

	
End Class
