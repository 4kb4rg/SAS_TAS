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

Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Imports agri.PWSystem.clsConfig
Imports agri.GlobalHdl
Imports agri.GL.clsData
Imports agri.IN.clsMthEnd
Imports agri.BI.clsMthEnd

Public Class GL_data_trx_upload : Inherits Page

    Protected WithEvents flUpload As HtmlInputFile
    Protected WithEvents rbIN As RadioButton
    Protected WithEvents rbCT As RadioButton
    Protected WithEvents rbWS As RadioButton
    Protected WithEvents rbPU As RadioButton
    Protected WithEvents rbAP As RadioButton
    Protected WithEvents rbPR As RadioButton
    Protected WithEvents rbBI As RadioButton
    Protected WithEvents rbGL As RadioButton
    Protected WithEvents lblOKIN As Label
    Protected WithEvents lblOKCT As Label
    Protected WithEvents lblOKWS As Label
    Protected WithEvents lblOKPU As Label
    Protected WithEvents lblOKAP As Label
    Protected WithEvents lblOKPR As Label
    Protected WithEvents lblOKBI As Label
    Protected WithEvents lblOKGL As Label
    Protected WithEvents lblErrNoFile As Label
    Protected WithEvents lblErrUpload As Label
    Protected WithEvents lblErrMesage As Label
    Protected WithEvents lblErrTrxType As Label

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objGLData As New agri.GL.clsData()
    Dim objAdmShare As New agri.Admin.clsShare()
    Dim objINMthEnd As New agri.IN.clsMthEnd()
    Dim objBIMthEnd As New agri.BI.clsMthEnd()
    Dim objGLMthEnd As New agri.GL.clsMthEnd()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intGLAR As Integer
    Dim intConfig As Integer
    Dim intModuleActivate As Integer

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intGLAR = Session("SS_GLAR")
        intConfig = Session("SS_AUTOGLPOSTING")
        intModuleActivate = Session("SS_MODULEACTIVATE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLUpload), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblOKIN.Visible = False
            lblOKCT.Visible = False
            lblOKWS.Visible = False
            lblOKPU.Visible = False
            lblOKAP.Visible = False
            lblOKPR.Visible = False
            lblOKBI.Visible = False
            lblOKGL.Visible = False
            lblErrNoFile.Visible = False
            lblErrTrxType.Visible = False
            If Not Page.IsPostBack Then
                CheckModuleLicense()
            End If
        End If
    End Sub

    Sub CheckModuleLicense()
        Dim objMthEndDs As New Dataset()
        Dim strOpCd As String = "ADMIN_CLSSHARE_MTHEND_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        
        If objAdmShare.mtdModuleCheck(intModuleActivate, objGlobal.mtdGetModuleCode(objGlobal.EnumModule.Inventory)) Then
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetAutoDataTransfer(objSysCfg.EnumAutoDataTransfer.Inventory), intConfig) Then
                rbIN.Enabled = False
            Else
                rbIN.Enabled = True
            End If
        Else
            rbIN.Enabled = False
        End If
        
        If objAdmShare.mtdModuleCheck(intModuleActivate, objGlobal.mtdGetModuleCode(objGlobal.EnumModule.Canteen)) Then
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetAutoDataTransfer(objSysCfg.EnumAutoDataTransfer.Canteen), intConfig) Then
                rbCT.Enabled = False
            Else
                rbCT.Enabled = True
            End If
        Else
            rbCT.Enabled = False
        End If

        If objAdmShare.mtdModuleCheck(intModuleActivate, objGlobal.mtdGetModuleCode(objGlobal.EnumModule.Workshop)) Then
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetAutoDataTransfer(objSysCfg.EnumAutoDataTransfer.Workshop), intConfig) = True Then
                rbWS.Enabled = False
            Else
                rbWS.Enabled = True
            End If
        Else
            rbWS.Enabled = False
        End If

        If objAdmShare.mtdModuleCheck(intModuleActivate, objGlobal.mtdGetModuleCode(objGlobal.EnumModule.Purchasing)) Then
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetAutoDataTransfer(objSysCfg.EnumAutoDataTransfer.Purchasing), intConfig) Then
                rbPU.Enabled = False
            Else
                rbPU.Enabled = True
            End If
        Else
            rbPU.Enabled = False
        End If

        If objAdmShare.mtdModuleCheck(intModuleActivate, objGlobal.mtdGetModuleCode(objGlobal.EnumModule.AccountPayable)) Then
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetAutoDataTransfer(objSysCfg.EnumAutoDataTransfer.AccountPayable), intConfig) Then
                rbAP.Enabled = False
            Else
                rbAP.Enabled = True
            End If
        Else
            rbAP.Enabled = False
        End If

        If objAdmShare.mtdModuleCheck(intModuleActivate, objGlobal.mtdGetModuleCode(objGlobal.EnumModule.Payroll)) Then
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetAutoDataTransfer(objSysCfg.EnumAutoDataTransfer.Payroll), intConfig) Then
                rbPR.Enabled = False
            Else
                rbPR.Enabled = True
            End If
        Else
            rbPR.Enabled = False
        End If

        If objAdmShare.mtdModuleCheck(intModuleActivate, objGlobal.mtdGetModuleCode(objGlobal.EnumModule.Billing)) Then
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetAutoDataTransfer(objSysCfg.EnumAutoDataTransfer.Billing), intConfig) Then
                rbBI.Enabled = False
            Else
                rbBI.Enabled = True
            End If
        Else
            rbBI.Enabled = False
        End If

        If objAdmShare.mtdModuleCheck(intModuleActivate, objGlobal.mtdGetModuleCode(objGlobal.EnumModule.GeneralLedger)) Then
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetAutoDataTransfer(objSysCfg.EnumAutoDataTransfer.GeneralLedger), intConfig) Then
                rbGL.Enabled = False
            Else
                rbGL.Enabled = True
            End If
        Else
            rbGL.Enabled = False
        End If

        Try
            strParam = objGlobal.EnumModule.Inventory & "','" & _
                       objGlobal.EnumModule.Canteen & "','" & _
                       objGlobal.EnumModule.Workshop & "','" & _
                       objGlobal.EnumModule.Purchasing & "','" & _
                       objGlobal.EnumModule.AccountPayable & "','" & _
                       objGlobal.EnumModule.Payroll & "','" & _
                       objGlobal.EnumModule.Production & "','" & _
                       objGlobal.EnumModule.Billing & "','" & _
                       objGlobal.EnumModule.GeneralLedger
            intErrNo = objAdmShare.mtdMonthEnd(strCompany, _
                                               strLocation, _
                                               strUserId, _
                                               strAccMonth, _
                                               strAccYear, _
                                               strOpCd, _
                                               strParam, _
                                               True, _
                                               objMthEndDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_DATA_TRX_GET_MTHEND&errmesg=" & lblErrMesage.Text & "&redirect=gl/data/gl_data_trx_upload.aspx")
        End Try

        If objMthEndDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objMthEndDs.Tables(0).Rows.Count - 1
                Select Case CInt(objMthEndDs.Tables(0).Rows(intCnt).Item("ModuleCode"))
                    Case objGlobal.EnumModule.Inventory
                        If CInt(objMthEndDs.Tables(0).Rows(intCnt).Item("PostedInd")) = objAdmShare.EnumMthEndPost.Yes Then
                            lblOKIN.Visible = True
                            rbIN.Enabled = False
                        End If
                    Case objGlobal.EnumModule.Canteen
                        If CInt(objMthEndDs.Tables(0).Rows(intCnt).Item("PostedInd")) = objAdmShare.EnumMthEndPost.Yes Then
                            lblOKCT.Visible = True
                            rbCT.Enabled = False
                        End If
                    Case objGlobal.EnumModule.Workshop
                        If CInt(objMthEndDs.Tables(0).Rows(intCnt).Item("PostedInd")) = objAdmShare.EnumMthEndPost.Yes Then
                            lblOKWS.Visible = True
                            rbWS.Enabled = False
                        End If
                    Case objGlobal.EnumModule.Purchasing
                        If CInt(objMthEndDs.Tables(0).Rows(intCnt).Item("PostedInd")) = objAdmShare.EnumMthEndPost.Yes Then
                            lblOKPU.Visible = True
                            rbPU.Enabled = False
                        End If
                    Case objGlobal.EnumModule.AccountPayable
                        If CInt(objMthEndDs.Tables(0).Rows(intCnt).Item("PostedInd")) = objAdmShare.EnumMthEndPost.Yes Then
                            lblOKAP.Visible = True
                            rbAP.Enabled = False
                        End If
                    Case objGlobal.EnumModule.Payroll
                        If CInt(objMthEndDs.Tables(0).Rows(intCnt).Item("PostedInd")) = objAdmShare.EnumMthEndPost.Yes Then
                            lblOKPR.Visible = True
                            rbPR.Enabled = False
                        End If
                    Case objGlobal.EnumModule.Billing
                        If CInt(objMthEndDs.Tables(0).Rows(intCnt).Item("PostedInd")) = objAdmShare.EnumMthEndPost.Yes Then
                            lblOKBI.Visible = True
                            rbBI.Enabled = False
                        End If
                    Case objGlobal.EnumModule.GeneralLedger
                        If CInt(objMthEndDs.Tables(0).Rows(intCnt).Item("PostedInd")) = objAdmShare.EnumMthEndPost.Yes Then
                            lblOKGL.Visible = True
                            rbGL.Enabled = False
                        End If
                End Select
            Next
        End If        

        objMthEndDs = Nothing
    End Sub

    Sub UploadBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Dim objStreamReader As StreamReader
        Dim objSysLocDs As New Object()
        Dim strOpCd_INMthEnd_Get As String = "IN_CLSMTHEND_MONTHENDTRX_GET"
        Dim strOpCd_BIMthEnd_Get As String = "BI_CLSMTHEND_MONTHENDTRX_GET"
        Dim strOpCd_GLMthEnd_Get As String = "GL_CLSMTHEND_MONTHENDTRX_GET"
        Dim strOpCd_SHMthEndTrx_Add As String = "ADMIN_CLSSHARE_MTHENDTRX_ADD"
        Dim strZipPath As String = ""
        Dim strXmlPath As String = ""
        Dim arrZipPath As Array
        Dim strZipName As String = ""
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intFreeFile As Integer
        Dim intModule As Integer
        Dim strFtpPath As String
        Dim strXmlEncrypted As String = ""
        Dim objXmlDecrypted As New Object()

        If  rbIN.Checked = False And _
            rbCT.Checked = False And _
            rbWS.Checked = False And _
            rbPU.Checked = False And _
            rbAP.Checked = False And _
            rbPR.Checked = False And _
            rbBI.Checked = False And _
            rbGL.Checked = False Then
            lblErrTrxType.Visible = True
            Exit sub
        End If
        
        Try
            intErrNo = objSysCfg.mtdGetFtpPath(strFtpPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_DATA_TRX_GET_FTPPATH&errmesg=" & lblErrMesage.Text & "&redirect=gl/data/gl_data_trx_upload.aspx")
        End Try        

        strZipPath = flUpload.PostedFile.FileName
        arrZipPath = Split(strZipPath, "\")
        strZipName = arrZipPath(UBound(arrZipPath))
        strZipPath = strFtpPath & strZipName

        Try
            strXmlPath = strFtpPath & Mid(strZipName, 1, Len(strZipName) - 3) & "xml"
        Catch Exp As System.Exception
            lblErrNoFile.Visible = True
            Exit sub
        End Try

        Dim Xmlfile As New FileInfo(strXmlPath)

        If Xmlfile.Exists Then
            File.Delete(strXmlPath)
        End If

        Try
            flUpload.PostedFile.SaveAs(strZipPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_DATA_TRX_SAVEAS&errmesg=" & lblErrUpload.Text & "&redirect=gl/data/gl_data_trx_upload.aspx")
        End Try


        objStreamReader = File.OpenText(strZipPath)
        strXmlEncrypted = objStreamReader.ReadToEnd()
        objStreamReader.Close()

        Try
            intErrNo = objSysCfg.mtdDecryptRef(strXmlEncrypted, objXmlDecrypted)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_DATA_TRX_DECRYPT_REF&errmesg=" & lblErrMesage.Text & "&redirect=gl/data/gl_data_trx_upload.aspx")
        End Try

        intFreeFile = FreeFile()
        FileOpen(intFreeFile, strXmlPath, 8)  
        Print(intFreeFile, objXmlDecrypted)
        FileClose(intFreeFile)

        Try
            intErrNo = objGLData.mtdUploadRef(strXmlPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_DATA_TRX_UPLOAD_REF&errmesg=" & lblErrMesage.Text & "&redirect=gl/data/gl_data_trx_upload.aspx")
        End Try

        Try
            If rbIN.Checked Then
                strAccMonth = Session("SS_INACCMONTH")
                strAccYear = Session("SS_INACCYEAR")
                strParam = objGlobal.EnumModule.Inventory & "|FALSE"
                intErrNo = objINMthEnd.mtdCloseMonthEnd(strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strOpCd_INMthEnd_Get & "|" & strOpCd_SHMthEndTrx_Add, _
                                                        strParam)
                rbIN.Checked = False
            ElseIf rbCT.Checked Then
                strAccMonth = Session("SS_INACCMONTH")
                strAccYear = Session("SS_INACCYEAR")
                intModule = objGlobal.EnumModule.Canteen
                rbCT.Checked = False
            ElseIf rbWS.Checked Then
                strAccMonth = Session("SS_INACCMONTH")
                strAccYear = Session("SS_INACCYEAR")
                intModule = objGlobal.EnumModule.Workshop
                rbWS.Checked = False
            ElseIf rbPU.Checked Then
                strAccMonth = Session("SS_PUACCMONTH")
                strAccYear = Session("SS_PUACCYEAR")
                intModule = objGlobal.EnumModule.Purchasing
                rbPU.Checked = False
            ElseIf rbAP.Checked Then
                strAccMonth = Session("SS_APACCMONTH")
                strAccYear = Session("SS_APACCYEAR")
                intModule = objGlobal.EnumModule.AccountPayable
                rbAP.Checked = False
            ElseIf rbPR.Checked Then
                strAccMonth = Session("SS_PRACCMONTH")
                strAccYear = Session("SS_PRACCYEAR")
                intModule = objGlobal.EnumModule.Payroll
                rbPR.Checked = False
            ElseIf rbBI.Checked Then
                strAccMonth = Session("SS_GLACCMONTH")
                strAccYear = Session("SS_GLACCYEAR")
                strParam = objGlobal.EnumModule.Billing & "|FALSE"
                intErrNo = objBIMthEnd.mtdCloseMonthEnd(strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strOpCd_BIMthEnd_Get & "|" & strOpCd_SHMthEndTrx_Add, _
                                                        strParam)
                rbBI.Checked = False
            ElseIf rbGL.Checked Then
                strAccMonth = Session("SS_GLACCMONTH")
                strAccYear = Session("SS_GLACCYEAR")
                strParam = objGlobal.EnumModule.GeneralLedger & "|FALSE"
                intErrNo = objGLMthEnd.mtdCloseMonthEnd(strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strOpCd_GLMthEnd_Get & "|" & strOpCd_SHMthEndTrx_Add, _
                                                        strParam)
                rbGL.Checked = False
            End If
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_DATA_TRX_CLOSED_UPD&errmesg=&redirect=")
        End Try

        CheckModuleLicense()
    End Sub


End Class
