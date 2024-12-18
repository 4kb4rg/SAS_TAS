Imports System
Imports System.Data
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports agri.GlobalHdl

Public Class defaultPage : Inherits Page

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim strUserId As String

    Sub Page_Load(Sender As Object, E As EventArgs)
        Dim intADAR As Integer = Session("SS_ADAR")
        Dim intUserAR As Integer = objAR.mtdGetAccessRights(clsAccessRights.EnumADAccessRights.ADUser)
        Dim intSysCfgAR As Integer = objAR.mtdGetAccessRights(clsAccessRights.EnumADAccessRights.ADSystemConfig)
        Dim intLangCapAR As Integer = objAR.mtdGetAccessRights(clsAccessRights.EnumADAccessRights.ADLanguageCaption)

        strUserId = Session("SS_USERID")

        If strUserId <> "" Then
            If ((intUserAR And intADAR) = intUserAR) Or _
                ((intSysCfgAR And intADAR) = intSysCfgAR) Or _
                ((intLangCapAR And intADAR) = intLangCapAR) Then
            Else
            End If
        Else
        End If
    End Sub

End Class
