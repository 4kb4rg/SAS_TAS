Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports System.Xml
Imports System.Web.Services
             
Public Class GL_StdRpt_LabourHourReconciliation : Inherits Page
    Protected RptSelect As UserControl

    Dim objGL As New agri.GL.clsReport()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label

    Protected WithEvents lblLangCapBlock As Label
    Protected WithEvents lblLangCapSubBlock As Label
    Protected WithEvents lblLangCapLocation As Label

    Protected WithEvents lblActGrp As Label
    Protected WithEvents lblErrActGrp As Label
    Protected WithEvents lblCode As Label
    
    Protected WithEvents ibPrintPrevious As ImageButton

    Protected WithEvents rbSuppYes As RadioButton
    Protected WithEvents rbSuppNo As RadioButton

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim tempActGrpText As String
    Dim tempActGrpCode As String
    Dim intConfigsetting As Integer
    Dim intErrNo As Integer

    Dim dsLangCap As New DataSet
	
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String
	

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")


        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
                InitializeLangCap()                
            End If
        End If
    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim htrTemp As HtmlTableRow

        htrTemp = RptSelect.FindControl("TrMthYr")
        htrTemp.Visible = True
        htrTemp = RptSelect.FindControl("TrCheckLoc")
        htrTemp.Visible = True
        htrTemp = RptSelect.FindControl("TrRadioLoc")
        htrTemp.Visible = False
    End Sub
    
    
    
    Function GetLanguageCaptionDS() As DataSet
        Dim strOpCode_BussTerm As String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
        Dim dsTemp As DataSet
        Dim strParam As String
        Dim intErrNo As Integer
        
        strParam = strLangCode
        Try
            intErrNo = objLangCap.mtdGetBussTerm(strOpCode_BussTerm, _
                                                 strCompany, _
                                                 strLocation, _
                                                 strUserId, _
                                                 "", _
                                                 "", _
                                                 dsTemp, _
                                                 strParam)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_STDRPT_COST_SUMMARY_LANGCAP_GET&errmesg=&redirect=")
        End Try
        
        Return dsTemp
        If Not dsTemp Is Nothing Then
            dsTemp = Nothing
        End If
    End Function    
    
    Sub ibPrintPreview_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strActGrpCode As String
        Dim strActGrpName As String

        Dim strddlAccMth As String
        Dim strddlAccYr As String
        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String
        Dim strSupp As String
        Dim blnSel As Boolean

        Dim tempAccMth As DropDownList
        Dim tempAccYr As DropDownList
        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden

        Dim intCnt As Integer

        tempAccMth = RptSelect.FindControl("lstAccMonth")
        strddlAccMth = Trim(tempAccMth.SelectedItem.Value)

        tempAccYr = RptSelect.FindControl("lstAccYear")
        strddlAccYr = Trim(tempAccYr.SelectedItem.Value)

        tempRpt = RptSelect.FindControl("lstRptName")
        strRptID = Trim(tempRpt.SelectedItem.Value)
        strRptName = Trim(tempRpt.SelectedItem.Text)

        tempUserLoc = RptSelect.FindControl("hidUserLoc")
        strUserLoc = Trim(tempUserLoc.Value)

        tempDec = RptSelect.FindControl("lstDecimal")
        strDec = Trim(tempDec.SelectedItem.Value)

        If strUserLoc = "" Then
            templblUL = RptSelect.FindControl("lblUserLoc")
            templblUL.Visible = True
            Exit Sub
        Else
            If Left(strUserLoc, 3) = "','" Then
                strUserLoc = Right(strUserLoc, Len(strUserLoc) - 3)
            ElseIf Right(strUserLoc, 3) = "','" Then
                strUserLoc = Left(strUserLoc, Len(strUserLoc) - 3)
            End If
        End If
        
        If rbSuppYes.Checked Then
            strSupp = rbSuppYes.Text
        ElseIf rbSuppNo.Checked Then
            strSupp = rbSuppNo.Text
        End If
        

        Response.Write("<Script Language=""JavaScript"">window.open(""GL_StdRpt_LabourHourReconciliationPreview.aspx?Type=Print&CompName=" & strCompany & _
                       "&SelLocation=" & strUserLoc & _
                       "&RptID=" & strRptID & _
                       "&RptName=" & strRptName & _
                       "&Decimal=" & strDec & _
                       "&DDLAccMth=" & strddlAccMth & _
                       "&DDLAccYr=" & strddlAccYr & _
                       "&Supp=" & strSupp & _                       
                       """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

    End Sub    
    
    Sub InitializeLangCap() 
        dsLangCap = GetLanguageCaptionDS()
        lblLangCapLocation.Text = GetCaption(objLangCap.EnumLangCap.Location)
        lblLangCapBlock.Text = GetCaption(objLangCap.EnumLangCap.Block)
        lblLangCapSubBlock.Text = GetCaption(objLangCap.EnumLangCap.SubBlock)        
    End Sub
    

    Function GetCaption(ByVal pv_TermCode) As String
            Dim count As Integer

            For count = 0 To dsLangCap.Tables(0).Rows.Count - 1
                If Trim(pv_TermCode) = Trim(dsLangCap.Tables(0).Rows(count).Item("TermCode")) Then
                    If strLocType = objAdminLoc.EnumLocType.Mill then
                        Return Trim(dsLangCap.Tables(0).Rows(count).Item("BusinessTermMW"))
                    else
                        Return Trim(dsLangCap.Tables(0).Rows(count).Item("BusinessTerm"))
                    end if
                    Exit For
                End If
            Next
        End Function
End Class
