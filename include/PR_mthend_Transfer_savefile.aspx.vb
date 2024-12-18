Imports System
Imports System.Data
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction
Imports Microsoft.VisualBasic.FileSystem
Imports Microsoft.VisualBasic
Imports agri.PWSystem.clsConfig
Imports agri.Admin.clsShare
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.GlobalHdl.clsAccessRights
Imports agri.PR.clsData

Public Class PR_mthend_Transfer_savefile : Inherits Page

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdmShare As New agri.Admin.clsShare()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objPR As New agri.PR.clsMthEnd()

    Dim strUserId As String
    Dim strLangCode As String
    Dim intPRAR As Long
    Dim strParam As String
    Dim strOpCd As String
    Dim strTxtFileName As String
    
    Sub Page_Load(Sender As Object, E As EventArgs)
       
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intPRAR = Session("SS_PRAR")

        strParam = Request.QueryString("strParam")
        strOpCd = Request.QueryString("strOpCd")
        strTxtFileName = Request.QueryString("strTxtFileName")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthTransfer), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            SaveFile()
        End If
    End Sub

    Sub SaveFile()
         
         Dim intErrNo As Integer
         Dim ObjTxtFile  AS String = ""

         Try
               intErrNo = objPR.mtdGenerateBCATransFile(strOpCd, strParam, ObjTxtFile)
         Catch Exp As System.Exception
               Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GENERATE_BCA_TRANSFILE&errmesg=&redirect=")
         End Try      
        
         Response.WriteFile(ObjTxtFile)
         Response.ContentType = "application/txt"
         Response.AddHeader("Content-Disposition", _
                                "attachment; filename=""" & strTxtFileName & """")
         

    End Sub

End Class

