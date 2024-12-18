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


Public Class WM_Setup_FFBPriceDet : Inherits Page
      
     

    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objAdminComp As New agri.Admin.clsComp()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim ObjOK As New agri.GL.ClsTrx
    Dim objGLTrx As New agri.GL.clsTrx()

    Dim objJobCode As New Object()
    Dim objDeptDs As New Object()
    Dim objDeptCodeDs As New Object()
    Dim objCompDs As New Object()
    Dim objDivHeadDs As New Object()
    Dim objLocDs As New Object()
    Dim objEmpDs As New Object()
    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strLocation As String

    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intPRAR As Long
    Dim intLevel As Integer

    Dim strSelectedSalCode As String = ""
    Dim intStatus As Integer
    Dim strLocType As String
    Dim ParamNama As String
    Dim ParamValue As String
    Dim Prefix As String = "SLC"

    Function getCode() As String
        Dim strOpCd_GetID As String = "ALL_ALL_GETID"
        Dim objNewID As New Object
        Dim intErrNo As Integer

        Prefix = "SC" & strLocation

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_GetID, "PREFIX|LOCID|LEN", Prefix & "||8", objNewID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ALL_ALL_GETID&errmesg=" & Exp.Message & "&redirect=")
        End Try

        Return Trim(objNewID.Tables(0).Rows(0).Item("CURID"))
    End Function

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")

        strUserId = Session("SS_USERID")
        intPRAR = Session("SS_PRAR")
        intLevel = Session("SS_USRLEVEL")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ' ElseIf (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRPaySetup), intPRAR) = False) And (intLevel < 2) Then
        '     Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            strSelectedSalCode = Trim(IIf(Request.QueryString("SalaryCode") <> "", Request.QueryString("SalaryCode"), Request.Form("SalaryCode")))
            intStatus = CInt(lblHiddenSts.Text)

            If Not IsPostBack Then
                BindSubLocation("")
                If strSelectedSalCode <> "" Then
                    SalCode.Value = strSelectedSalCode
                    onLoad_Display()
                Else
                    onLoad_BindButton()
                End If
            End If
        End If

    End Sub

    Sub BindSubLocation(ByVal pKode As string)
        Dim strParamName As String
        Dim strParamValue As String
        Dim strOpCd_Get As String = "IN_CLSTRX_GLOBAL_UPD"

        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim sSQLKriteria As String = ""
        Dim objdsST As New DataSet
        Dim intSelectedIndex As Integer
 
        sSQLKriteria="SELECT SubLocCode,SubDescription From SH_LOCATIONSUB  Where LocCode='" & strlocation & "'"
        strParamName = "STRSEARCH"
        strParamValue = sSQLKriteria

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd_Get, _
                                         strParamName, _
                                         strParamValue, _
                                         objdsST)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=STOCK_ADJUSTMENT_UPDATE_FAILED&errmesg=&redirect=IN/Trx/IN_trx_StockAdj_list.aspx")
        End Try
 
         For intCnt = 0 To objdsST.Tables(0).Rows.Count - 1
            objdsST.Tables(0).Rows(intCnt).Item("SubLocCode") = Trim(objdsST.Tables(0).Rows(intCnt).Item("SubLocCode"))
            objdsST.Tables(0).Rows(intCnt).Item("SubDescription") = objdsST.Tables(0).Rows(intCnt).Item("SubDescription") & " (" & Trim(objdsST.Tables(0).Rows(intCnt).Item("SubLocCode")) & ")"
            If objdsST.Tables(0).Rows(intCnt).Item("SubLocCode") = Trim(pKode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objdsST.Tables(0).NewRow()
        dr("SubLocCode") = "" 
        dr("SubDescription") = "Please Select Location" 
        objdsST.Tables(0).Rows.InsertAt(dr, 0)

        srchlocation.DataSource = objdsST.Tables(0)
        srchlocation.DataValueField = "SubLocCode"
        srchlocation.DataTextField = "SubDescription"
        srchlocation.DataBind()
        srchlocation.SelectedIndex = intSelectedIndex
 
    End Sub

    Sub onLoad_Display()
        Dim strOpCd_Get As String = "WM_CLSSETUP_FFB_PRICE_LIST"
        Dim intErrNo As Integer
        Dim strSearch As String
        Dim sortitem As String
        Dim strDate As String = Date_Validation(txtDateCreated.Text, False)

        strSearch = "Where h.SubLocCode ='" & srchlocation.SelectedItem.Value & "' And h.InDate='" & strDate & "'"
        ParamNama = "STRSEARCH"
        ParamValue = strSearch

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_Get, ParamNama, ParamValue, objDeptDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_DEPT_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objDeptDs.Tables(0).Rows.Count > 0 Then 
            lblDateCreated.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("UpdateID"))         
        End If
    End Sub

    Sub onLoad_BindButton()

        
        SaveBtn.Visible = False
        DelBtn.Visible = False
        UnDelBtn.Visible = False
        
        Select Case intStatus
            Case "1"
             
                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case "2"
                UnDelBtn.Visible = True
            Case Else
               
                SaveBtn.Visible = True
        End Select

    End Sub
      
    Function Date_Validation(ByVal pv_strInputDate As String, ByVal pv_blnIsShortDate As Boolean) As String
        Dim objSysCfgDs As New Object
        Dim objActualDate As New Object
        Dim strDateFormat As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim strAcceptFormat As String

        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"

        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CB_DEPOSIT_GET_CONFIG&errmesg=" & Exp.Message & "&redirect=CB/trx/cb_trx_DepositList.aspx")
        End Try

        strDateFormat = objSysCfg.mtdGetDateFormat(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt").Trim())

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

    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "WM_CLSSETUP_FFB_PRICE_INS"
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim intErrNo As Integer
 
        Dim strstatus As String = ""
        Dim objID As New Object()
 
        Dim strGiroDate As String = Date_Validation(txtDateCreated.Text, False)

         
        ParamNama = "TGL|BB|BS|BK|UI|LOC|BRD|S1|S2|S3|SUBLOC"
        ParamValue = strGiroDate & "|" & txtBBesar.Text & "|" & txtBSedang.Text & "|" & txtBKecil.Text & "|" & strUserId & "|" & _
                     strLocation & "|" & 0 & "|" & 0 & "|" & 0 & "|" & 0 & "|" & srchlocation.SelectedItem.Value

        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOpCd_Upd, ParamNama, ParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WM_CLSSETUP_FFB_PRICE_INS&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        onLoad_Display
    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("WM_Setup_FFBPriceList.aspx")
    End Sub

End Class
