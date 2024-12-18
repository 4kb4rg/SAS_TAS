Imports System
Imports System.Data
Imports System.IO 
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction
Imports Microsoft.VisualBasic
Imports agri.Admin
Imports agri.HR
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.PWSystem.clsLangCap
Imports agri.GL


Public Class PR_Trx_OutOfDuty_EstateDet : Inherits Page


    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objAdminComp As New agri.Admin.clsComp()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim ObjOK As New agri.GL.ClsTrx


    Dim objDeptDs As New Object()


    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intHRAR As Long

    Dim strId As String = ""

    Dim intStatus As Integer
    Dim strLocType As String
    Dim ParamNama As String
    Dim ParamValue As String
    Dim sTrDocID AS String

#Region "LOCAL & PROCEDURE"
    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        intHRAR = Session("SS_HRAR")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        'ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRCompany), intHRAR) = False Then
        '    Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            sTrDocID = Trim(IIf(Request.QueryString("DocID") <> "", Request.QueryString("DocID"), Request.Form("DocID")))   
                                 
            If Not IsPostBack Then
                dtDateFr.SelectedDate=Date.Now
                dtDateTo.SelectedDate=Date.Now
                dtDateFr.Enabled=True
                dtDateTo.Enabled=True
                lblErrMessage.Visible = False
                BindEmployee()                                
                onLoadButton("")        
                If sTrDocID <> "" Then
                    txtid.text=sTrDocID                     
                    onLoad_Display(sTrDocID)
                    BindGrid()
                End IF                
            End If
        End If
    End Sub

    Sub NewBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_Trx_OutOfDuty_EstateDet.aspx")
    End Sub
 
    Sub btnAdd_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)	
      IF lSaveData("1")=True Then
            UserMsgBox(ME,"Data Telah Tersimpan")
            onLoad_Display(txtid.text)
      End IF
    End Sub
    
    Sub btnEdit_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)	
      IF lSaveData("1")=True Then
            UserMsgBox(ME,"Edit Berhasil")
            onLoad_Display(txtid.text)
      End IF
    End Sub

    Sub Delete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        IF lSaveData("2")=True Then
            UserMsgBox(ME,"Data Telah Terhapus")
            Response.Redirect("PR_Trx_OutOfDutyList.aspx")
        End IF
    End Sub

    Sub btnConfirmed_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)	
      IF lSaveData("3")=True Then
            UserMsgBox(ME,"Confirmed Berhasil")
            onLoad_Display(txtid.text)
      End IF
    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_Trx_OutOfDutyList.aspx")
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOpCd_Upd As String = "IN_CLSTRX_GLOBAL_UPD"
        
        Dim lbl As Label
        Dim intErrNo As Integer
        Dim strClaimId As String = ""
        Dim strDocLnId As String = ""
        Dim objID As New Object()

        lbl=E.Item.FindControl("lblClaimID")
        strClaimId=lbl.Text.Trim
        lbl=E.Item.FindControl("lblDocLNID")
        strDocLnId=lbl.Text.Trim
        	
        ParamNama = "STRSEARCH"
        ParamValue = "DELETE FROM  PR_TRX_OUTDUTYLN Where DocId='" & txtid.text & "' And DocLNID='" & strDocLnId & "' And ClaimID='" & strClaimid & "' And LocCode='" & strlocation & "' "
        
        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOpCd_Upd, ParamNama, ParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PREMI_KERAJINAN_DEL_LINE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        BindGrid()
    End Sub
	
	 Sub dgLineDet_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim Updbutton As LinkButton
            Updbutton = CType(e.Item.FindControl("lbDelete"), LinkButton)
            Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
        End If
    End Sub

#End Region 

#Region "LOCAL & PROCEDURE"
  Sub UserMsgBox(ByVal F As Object, ByVal sMsg As String)
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

    Sub BindEmployee()
        Dim strOpCd_EmpDiv As String = "HR_HR_TRX_EMPLOYEE_LIST_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim objEmpCodeDs As New Object()
        Dim dr As DataRow
		Dim SM As String = Month(dtDateFr.SelectedDate)
        Dim SY As String = Year(dtDateTo.SelectedDate)


        strParamName = "LOC|AM|AY|SEARCH|SORT"
        strParamValue = strLocation & "|" & SM & "|" & SY & "|AND  A.LocCode = '" & strLocation & "' AND A.Status='1'|ORDER BY A.EmpName"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_TRX_EMPLOYEE_LIST_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        dr = objEmpCodeDs.Tables(0).NewRow()
        dr("EmpCode") = ""        
        objEmpCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        radcmbEmp.DataSource = objEmpCodeDs.Tables(0)
        radcmbEmp.DataTextField = "_Description"
        radcmbEmp.DataValueField = "EmpCode"
        radcmbEmp.DataBind()        

    End Sub

    Sub onLoad_Display(ByVal pNoBukti As String)
        Dim strOpCd_Get As String = "PR_PR_TRX_OUTOFDUTY_GET"
        Dim intErrNo As Integer
        Dim strSearch As String
        Dim sortitem As String
        Dim sTrStatus AS String

        strSearch = "AND P.LocCode='" & strLocation & "' AND P.DocID='" & pNoBukti & "'"
        sortitem = ""
        ParamNama = "STRSEARCH"
        ParamValue = strSearch

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_Get, ParamNama, ParamValue, objDeptDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PREMI_MANDOR_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        sTrStatus=""
        If objDeptDs.Tables(0).Rows.Count > 0 Then
            txtid.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("DocID"))
            ddlTujuan.SelectedValue= Trim(objDeptDs.Tables(0).Rows(0).Item("DutyType"))
            radcmbEmp.SelectedValue= Trim(objDeptDs.Tables(0).Rows(0).Item("EmpCode"))
            dtDateFr.SelectedDate=objDeptDs.Tables(0).Rows(0).Item("DateFr")
            dtDateTo.SelectedDate=objDeptDs.Tables(0).Rows(0).Item("DateTo")
			txtNote.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("Description"))
            txtVehCode.text= Trim(objDeptDs.Tables(0).Rows(0).Item("OtherVehicle"))
            txtOtherVeh.text= Trim(objDeptDs.Tables(0).Rows(0).Item("Description"))
            lblStatus.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("StatusDesc"))
            lblDateCreated.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("UpdateID"))
            sTrStatus= Trim(objDeptDs.Tables(0).Rows(0).Item("Status"))
            dtDateFr.Enabled=False
            dtDateTo.Enabled=False
        Else
            dtDateFr.Enabled=True
            dtDateTo.Enabled=True
        End If
        
        onLoadButton(sTrStatus)
    End Sub

    Sub onLoadButton(ByVal strStatus AS String)    
        Select Case sTrStatus
            Case "1"
                btnConf.Visible=True
                DelBtn.Visible=True
                BtnEdit.Visible=False
            Case "2"
                BtnAdd.Visible=False
                DelBtn.Visible=False
                btnConf.Visible=False
                BtnEdit.Visible=False
            Case "3" 
                btnConf.Visible=False
                BtnEdit.Visible=True
                btnAdd.Visible=False
                DelBtn.Visible=False
            Case Else
                btnConf.Visible=False
                BtnEdit.Visible=False
                btnAdd.Visible=True
                DelBtn.Visible=False
        End Select
    End SUb

    Function lSaveData(ByVal pStatus As String) AS Boolean 
        Dim strOpCd As String = "PR_PR_TRX_OUTOFDUTY_INS"        
        Dim intErrNo As Integer
        Dim idln As String = ""
		Dim strstatus As String
        Dim bSave AS Boolean
        Dim objID As New Object()

        If radcmbEmp.SelectedValue = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Silakan Pilih Nama Karyawan"
            bSave=False
            Exit Function
        End If

		If txtNote.Text.Trim = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Silakan isi Keterangan"
            bSave=False
            Exit Function
        End If

		bSave=False
        ParamNama = "DOCID|LOC|EMPCODE|DUTY|DTFR|DTTO|DESC|VEH|OTHVEH|STS|CRUI|UPUI"
        ParamValue = txtid.Text.Trim & "|" & _
                     strLocation & "|" & _
                     radcmbEmp.SelectedValue & "|" & _
                     ddlTujuan.SelectedValue & "|" & _
                     Format(dtDateFr.SelectedDate,"yyyy-MM-dd")  & "|" & _
                     Format(dtDateTo.SelectedDate,"yyyy-MM-dd")  & "|" & _
                     txtNote.Text  & "|" & _
                     txtVehCode.Text  & "|" & _
                     txtOtherVeh.Text  & "|" & _
                     pStatus   & "|" & _
                     strUserId   & "|" & _
                     strUserId

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd, ParamNama, ParamValue,objID)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PREMI_MANDOR_UPD&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try		 

        If objID.Tables(0).Rows.Count > 0 Then
            txtid.text = Trim(objID.Tables(0).Rows(0).Item("DocID"))
            bSave=True            
        END If

        Return bSave        
    End Function

    Sub BindGrid()
        Dim dsData As DataSet
        Dim intCnt As Integer    
        dsData = LoadData()
        dgLineDet.DataSource = dsData.Tables(0)
        dgLineDet.DataBind()
    End Sub

    Protected Function LoadData() As DataSet
        Dim strOpCd_Get As String = "PR_PR_TRX_OUTOFDUTY_DETAIL_GET"
        Dim intErrNo As Integer
        Dim strSearch As String
        Dim sortitem As String

        strSearch = "Where DocID='" & txtid.text & "' And LocCode='" & strlocation & "'"        
        ParamNama = "STRSEARCH"
        ParamValue = strSearch

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_Get, ParamNama, ParamValue, objDeptDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PREMI_LAIN_GET_LINE&errmesg=" & lblErrMessage.Text )
        End Try

        Return objDeptDs
    End Function

	 Sub getitem(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lbl As Label
            Dim hid As HiddenField
            Dim objCodeDs As New Object()
 
			' hid = dgLineDet.Items.Item(CInt(e.Item.ItemIndex)).FindControl("hidjrate")
			' txtrate.Text = hid.value.trim() 
			
        End If
    End Sub

#End Region
   
End Class
