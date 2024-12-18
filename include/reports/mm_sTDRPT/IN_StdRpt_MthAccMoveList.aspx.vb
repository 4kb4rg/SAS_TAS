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

Public Class IN_StdRpt_MthAccMoveList : Inherits Page

    Protected RptSelect As UserControl

    Dim objIN As New agri.IN.clsReport()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label

    Protected WithEvents lstAnaGrp As DropDownList
    Protected WithEvents lblProdTypeCode As Label
    Protected WithEvents lblProdBrandCode As Label
    Protected WithEvents lblProdModelCode As Label
    Protected WithEvents lblProdCatCode As Label
    Protected WithEvents lblProdMatCode As Label
    Protected WithEvents lblStkAnaCode As Label
    Protected WithEvents lblAccCode As Label
    Protected WithEvents lblLocation As Label

    Protected WithEvents txtProdType As TextBox
    Protected WithEvents txtProdBrand As TextBox
    Protected WithEvents txtProdModel As TextBox
    Protected WithEvents txtProdCat As TextBox
    Protected WithEvents txtProdMaterial As TextBox
    Protected WithEvents txtStkAna As TextBox
    Protected WithEvents txtAccCode As TextBox

    Protected WithEvents rbSuppYes As RadioButton
    Protected WithEvents rbSuppNo As RadioButton

    Protected WithEvents PrintPrev As ImageButton
    Protected WithEvents hidlblBlk As HtmlInputHidden
    Protected WithEvents hidlblVeh As HtmlInputHidden

    Protected WithEvents TrProdType As HtmlTableRow
    Protected WithEvents TrProdBrand As HtmlTableRow
    Protected WithEvents TrProdModel As HtmlTableRow
    Protected WithEvents TrProdCat As HtmlTableRow
    Protected WithEvents TrProdMat As HtmlTableRow
    Protected WithEvents TrStkAna As HtmlTableRow
    Protected WithEvents ckWS As Checkbox
    Protected WithEvents cbExcel As CheckBox

    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String

    Dim intCnt As Integer
    Dim intErrNo As Integer
    Dim strWS As String
	
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String
	
    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        strLocType = Session("SS_LOCTYPE")
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else

            If Not Page.IsPostBack Then
                onload_GetLangCap()
                AnalysisGrpList()
            End If

            If lstAnaGrp.SelectedItem.Value = "PType" Then
                TrProdType.Visible = True
                TrProdBrand.Visible = False
                TrProdModel.Visible = False
                TrProdCat.Visible = False
                TrProdMat.Visible = False
                TrStkAna.Visible = False
            ElseIf lstAnaGrp.SelectedItem.Value = "PBrand" Then
                TrProdType.Visible = False
                TrProdBrand.Visible = True
                TrProdModel.Visible = False
                TrProdCat.Visible = False
                TrProdMat.Visible = False
                TrStkAna.Visible = False
            ElseIf lstAnaGrp.SelectedItem.Value = "PModel" Then
                TrProdType.Visible = False
                TrProdBrand.Visible = False
                TrProdModel.Visible = True
                TrProdCat.Visible = False
                TrProdMat.Visible = False
                TrStkAna.Visible = False
            ElseIf lstAnaGrp.SelectedItem.Value = "PCat" Then
                TrProdType.Visible = False
                TrProdBrand.Visible = False
                TrProdModel.Visible = False
                TrProdCat.Visible = True
                TrProdMat.Visible = False
                TrStkAna.Visible = False
            ElseIf lstAnaGrp.SelectedItem.Value = "PMat" Then
                TrProdType.Visible = False
                TrProdBrand.Visible = False
                TrProdModel.Visible = False
                TrProdCat.Visible = False
                TrProdMat.Visible = True
                TrStkAna.Visible = False
            ElseIf lstAnaGrp.SelectedItem.Value = "StkAna" Then
                TrProdType.Visible = False
                TrProdBrand.Visible = False
                TrProdModel.Visible = False
                TrProdCat.Visible = False
                TrProdMat.Visible = False
                TrStkAna.Visible = True
            End If

        End If
    End Sub


    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim ucTrMthYr As HtmlTableRow

        ucTrMthYr = RptSelect.FindControl("TrMthYr")
        ucTrMthYr.Visible = True

    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        lblProdTypeCode.Text = GetCaption(objLangCap.EnumLangCap.ProdType) & " Code :"
        lblProdBrandCode.Text = GetCaption(objLangCap.EnumLangCap.ProdBrand) & " Code :"
        lblProdModelCode.Text = GetCaption(objLangCap.EnumLangCap.ProdModel) & " Code :"
        lblProdCatCode.Text = GetCaption(objLangCap.EnumLangCap.ProdCat) & " Code :"
        lblProdMatCode.Text = GetCaption(objLangCap.EnumLangCap.ProdMat) & " Code :"
        lblStkAnaCode.Text = GetCaption(objLangCap.EnumLangCap.StockAnalysis) & " Code :"
        lblAccCode.Text = GetCaption(objLangCap.EnumLangCap.Account) & " Code :"
        hidlblBlk.Value = GetCaption(objLangCap.EnumLangCap.Block)
        hidlblVeh.Value = GetCaption(objLangCap.EnumLangCap.Vehicle)
        lblLocation.Text = GetCaption(objLangCap.EnumLangCap.Location)
    End Sub

    Sub GetEntireLangCap()
        Dim strOpCode_BussTerm As String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
        Dim strParam As String
        Dim intErrNo As Integer

        strParam = strLangCode
        Try
            intErrNo = objLangCap.mtdGetBussTerm(strOpCode_BussTerm, _
                                                 strCompany, _
                                                 strLocation, _
                                                 strUserId, _
                                                 strAccMonth, _
                                                 strAccYear, _
                                                 objLangCapDs, _
                                                 strParam)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PWSYSTEM_CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/IN_StdRpt_Selection.aspx")
        End Try

    End Sub

    
    Function GetCaption(ByVal pv_TermCode) As String
            Dim count As Integer

            For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
                If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                    If strLocType = objAdminLoc.EnumLocType.Mill then
                        Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                    else
                        Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                    end if
                    Exit For
                End If
            Next
        End Function

    Sub AnalysisGrpList()

        Dim strProdType As String
        Dim strProdBrand As String
        Dim strProdModel As String
        Dim strProdCat As String
        Dim strProdMat As String
        Dim strStkAna As String

        strProdType = Left(lblProdTypeCode.Text, Len(lblProdTypeCode.Text) - 2)
        strProdBrand = Left(lblProdBrandCode.Text, Len(lblProdBrandCode.Text) - 2)
        strProdModel = Left(lblProdModelCode.Text, Len(lblProdModelCode.Text) - 2)
        strProdCat = Left(lblProdCatCode.Text, Len(lblProdCatCode.Text) - 2)
        strProdMat = Left(lblProdMatCode.Text, Len(lblProdMatCode.Text) - 2)
        strStkAna = Left(lblStkAnaCode.Text, Len(lblStkAnaCode.Text) - 2)

        lstAnaGrp.Items.Add(New ListItem(strProdType, "PType"))
        lstAnaGrp.Items.Add(New ListItem(strProdBrand, "PBrand"))
        lstAnaGrp.Items.Add(New ListItem(strProdModel, "PModel"))
        lstAnaGrp.Items.Add(New ListItem(strProdCat, "PCat"))
        lstAnaGrp.Items.Add(New ListItem(strProdMat, "PMat"))
        lstAnaGrp.Items.Add(New ListItem(strStkAna, "StkAna"))

    End Sub

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strAnaGrp As String
        Dim strProdType As String
        Dim strProdBrand As String
        Dim strProdModel As String
        Dim strProdCat As String
        Dim strProdMat As String
        Dim strStkAna As String
        Dim strAccCode As String

        Dim strddlAccMth As String
        Dim strddlAccYr As String
        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String
        Dim strSupp As String

        Dim tempAccMth As DropDownList
        Dim tempAccYr As DropDownList
        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden
        Dim strExportToExcel As String

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


        strExportToExcel = IIf(cbExcel.Checked = True, "1", "0")

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

        strAnaGrp = Trim(lstAnaGrp.SelectedItem.Value)

        If txtProdType.Text = "" Then
            strProdType = ""
        Else
            strProdType = Trim(txtProdType.Text)
        End If

        If txtProdBrand.Text = "" Then
            strProdBrand = ""
        Else
            strProdBrand = Trim(txtProdBrand.Text)
        End If

        If txtProdModel.Text = "" Then
            strProdModel = ""
        Else
            strProdModel = Trim(txtProdModel.Text)
        End If

        If txtProdCat.Text = "" Then
            strProdCat = ""
        Else
            strProdCat = Trim(txtProdCat.Text)
        End If

        If txtProdMaterial.Text = "" Then
            strProdMat = ""
        Else
            strProdMat = Trim(txtProdMaterial.Text)
        End If

        If txtStkAna.Text = "" Then
            strStkAna = ""
        Else
            strStkAna = Trim(txtStkAna.Text)
        End If

        If txtAccCode.Text = "" Then
            strAccCode = ""
        Else
            strAccCode = Trim(txtAccCode.Text)
        End If

        If rbSuppYes.Checked Then
            strSupp = rbSuppYes.Text
        ElseIf rbSuppNo.Checked Then
            strSupp = rbSuppNo.Text
        End If

        If ckWS.Checked Then
           strWS = "Yes"
        Else
           strWS = "No"
        End If

        strProdType = Server.UrlEncode(strProdType)
        strProdBrand = Server.UrlEncode(strProdBrand)
        strProdModel = Server.UrlEncode(strProdModel)
        strProdCat = Server.UrlEncode(strProdCat)
        strProdMat = Server.UrlEncode(strProdMat)
        strStkAna = Server.UrlEncode(strStkAna)
        strAccCode = Server.UrlEncode(strAccCode)

        Response.Write("<Script Language=""JavaScript"">window.open(""IN_StdRpt_MthAccMoveListPreview.aspx?Type=Print&Location=" & strUserLoc & _
                       "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & strDec & "&Supp=" & strSupp & "&DDLAccMth=" & strddlAccMth & _
                       "&DDLAccYr=" & strddlAccYr & "&lblLocation=" & lblLocation.Text & "&lblProdTypeCode=" & lblProdTypeCode.Text & _
                       "&lblProdBrandCode=" & lblProdBrandCode.Text & "&lblProdModelCode=" & lblProdModelCode.Text & "&WS=" & strWS & _
                       "&lblProdCatCode=" & lblProdCatCode.Text & "&lblProdMatCode=" & lblProdMatCode.Text & "&lblStkAnaCode=" & lblStkAnaCode.Text & _
                       "&lblAccCode=" & lblAccCode.Text & "&lblBlk=" & hidlblBlk.Value & "&lblVeh=" & hidlblVeh.Value & "&AnaGrp=" & strAnaGrp & _
                       "&ProdType=" & strProdType & "&ProdBrand=" & strProdBrand & "&ProdModel=" & strProdModel & "&ProdCat=" & strProdCat & _
                       "&ExportToExcel=" & strExportToExcel & _
                       "&ProdMat=" & strProdMat & "&StkAna=" & strStkAna & "&AccCode=" & strAccCode & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

    End Sub

End Class
