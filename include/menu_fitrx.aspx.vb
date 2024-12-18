
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls


Imports agri.GlobalHdl
Imports agri.PWSystem.clsLangCap
Imports agri.PWSystem.clsConfig

Public Class menu_fitrx : Inherits Page


    Dim strUserId As String
    Dim strLangCode As String
    Dim strCompany As String
    Dim strLocation As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intLocType As Integer
    Dim intModuleActivate As Integer
    Dim strLocTag As String
    Dim intLocLevel As Integer

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objLangCapVw As New DataView

    Dim intGLAR As Integer
    Dim intBIAR As Integer
    Dim intAPAR As Integer
    Dim intCBAR As Integer
    Dim intFAAR As Integer
    Dim intPUAR As Integer
    Dim intHRAR As Long
    Dim intTXAR As Long

    Const C_ADMIN = "ADMIN"



    Protected WithEvents lnkGL01 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkGL02 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkGL03 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkGL04 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkGL05 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkGL06 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkGL07 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkGL08 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkGL09 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkGL10 As System.Web.UI.WebControls.HyperLink

    Protected WithEvents lnkAP01 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkAP02 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkAP03 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkAP04 As System.Web.UI.WebControls.HyperLink

    Protected WithEvents lnkBill01 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkBill02 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkBill03 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkBill04 As System.Web.UI.WebControls.HyperLink

    Protected WithEvents lnkCB01 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkCB02 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkCB03 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkCB04 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkCB05 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkCB06 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkCB07 As System.Web.UI.WebControls.HyperLink

    Protected WithEvents lnkFA01 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkFA02 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkFA03 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkFA04 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkFA05 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkFA06 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkFA07 As System.Web.UI.WebControls.HyperLink

    Protected WithEvents lnkTX01 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkTX02 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkTX03 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkTX04 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkTX05 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkTX06 As System.Web.UI.WebControls.HyperLink

    Protected WithEvents tblGLHead As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tblGL As System.Web.UI.HtmlControls.HtmlTable

    Protected WithEvents tblSpc1 As System.Web.UI.HtmlControls.HtmlTable

    Protected WithEvents tblAPHead As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tblAP As System.Web.UI.HtmlControls.HtmlTable

    Protected WithEvents tblSpc2 As System.Web.UI.HtmlControls.HtmlTable

    Protected WithEvents tblBillHead As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tblBill As System.Web.UI.HtmlControls.HtmlTable

    Protected WithEvents tblSpc3 As System.Web.UI.HtmlControls.HtmlTable

    Protected WithEvents tblCBHead As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tblCB As System.Web.UI.HtmlControls.HtmlTable

    Protected WithEvents tblSpc4 As System.Web.UI.HtmlControls.HtmlTable
 
    Protected WithEvents tblFAHead As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tblFA As System.Web.UI.HtmlControls.HtmlTable

    Protected WithEvents tblSpc5 As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tblTXHead As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tblTX As System.Web.UI.HtmlControls.HtmlTable
   


   Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strUserId = Session("SS_USERID")
        intModuleActivate = Session("SS_MODULEACTIVATE")
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strLangCode = Session("SS_LANGCODE")
        intLocType = Session("SS_LOCTYPE")
        intLocLevel = Session("SS_LOCLEVEL")
        strAccMonth = Session("SS_FAACCMONTH")
        strAccYear = Session("SS_FAACCYEAR")

        If strUserId = "" Then

            Response.Write("<Script language=""Javascript"">parent.right.location.href = '/Login.aspx'</Script>")

        Else

           intGLAR = Session("SS_GLAR")
           intBIAR = Session("SS_BIAR")
           intAPAR = Session("SS_APAR")
           intCBAR = Session("SS_CBAR")
           intFAAR = Session("SS_FAAR")
           intPUAR = Session("SS_PUAR")
           intHRAR = Session("SS_HRAR")
            'tax dari canteen
            intTXAR = Session("SS_CTAR")
      
           GetEntireLangCap()

            onLoad_Display()

        End If

    End Sub

    Sub onLoad_Display()

        If Session("SS_LOCATION") = "" Or Session("SS_MAXPERIOD") = 0 Then

            Call DisableAllHead()

        Else
            
          
            Call TrxGLRight()

            Call TrxARRight()

            Call TrxAPRight()

            Call TrxCBRight()

            Call TrxFARight()
           
            Call TrxTXRight()
        End If

    End Sub


    Private Sub TrxFARight()
        Dim blnFA As Boolean
        blnFA = False

        Dim strAssetAddTag As String
        Dim strAssetDeprTag As String
        Dim strAssetDispTag As String
        Dim strAssetWOTag As String

        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.FixAsset), intModuleActivate) Then

            strAssetAddTag = GetCaption(objLangCap.EnumLangCap.AssetAdd)
            strAssetDeprTag = GetCaption(objLangCap.EnumLangCap.AssetDepr)
            strAssetDispTag = GetCaption(objLangCap.EnumLangCap.AssetDisp)
            strAssetWOTag = GetCaption(objLangCap.EnumLangCap.AssetWO)

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAAddition), intFAAR) = True) Then
                tblFA.Rows(0).Visible = True
                lnkFA01.Text = strAssetAddTag
                blnFA = True
            Else
                tblFA.Rows(0).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FADepreciation), intFAAR) = True) Then
                tblFA.Rows(1).Visible = True
                lnkFA02.Text = strAssetDeprTag
                blnFA = True
            Else
                tblFA.Rows(1).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FADisposal), intFAAR) = True) Then
                tblFA.Rows(2).Visible = True
                lnkFA03.Text = strAssetDispTag
                blnFA = True
            Else
                tblFA.Rows(2).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAWriteOff), intFAAR) = True) Then
                tblFA.Rows(3).Visible = True
                lnkFA04.Text = strAssetWOTag
                blnFA = True
            Else
                tblFA.Rows(3).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAWriteOff), intFAAR) = True) Then
                tblFA.Rows(4).Visible = False
                'lnkFA05.Text = strAssetWOTag
                blnFA = True
            Else
                tblFA.Rows(4).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FADepreciation), intFAAR) = True) Then
                tblFA.Rows(5).Visible = False
                blnFA = True
            Else
                tblFA.Rows(5).Visible = False
            End If
	    If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FADepreciation), intFAAR) = True) Then
                tblFA.Rows(6).Visible = False
                blnFA = True
            Else
                tblFA.Rows(6).Visible = False
            End If	
            If blnFA = False Then
                tblFAHead.Visible = False
                tblFA.Visible = False
               
            End If
        Else
            tblFAHead.Visible = False
            tblFA.Visible = False
           
        End If
    End Sub


    Private Sub TrxCBRight()
        Dim blnCB As Boolean
        blnCB = False

        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.CashAndBank), intModuleActivate) Then

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBPayment), intCBAR) = True) Then
                tblCB.Rows(0).Visible = True
                blnCB = True
            Else
                tblCB.Rows(0).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBReceipt), intCBAR) = True) Then
                tblCB.Rows(1).Visible = True
                blnCB = True
            Else
                tblCB.Rows(1).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBCashBank), intCBAR) = True) Then
                tblCB.Rows(2).Visible = True
                blnCB = True
            Else
                tblCB.Rows(2).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBDeposit), intCBAR) = True) Then
                tblCB.Rows(3).Visible = True
                blnCB = True
            Else
                tblCB.Rows(3).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBInterAdj), intCBAR) = True) Then
                tblCB.Rows(4).Visible = True
                blnCB = True
            Else
                tblCB.Rows(4).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBWithdrawal), intCBAR) = True) Then
                tblCB.Rows(5).Visible = False
                blnCB = True
            Else
                tblCB.Rows(5).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBPayment), intCBAR) = True) Then
                tblCB.Rows(6).Visible = False
                blnCB = True
            Else
                tblCB.Rows(6).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBCashBank), intCBAR) = True) Then
                tblCB.Rows(7).Visible = False
                blnCB = True
            Else
                tblCB.Rows(7).Visible = False
            End If
	     If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBCashBank), intCBAR) = True) Then
                tblCB.Rows(8).Visible = False
                blnCB = True
            Else
                tblCB.Rows(8).Visible = False
            End If	
            If blnCB = False Then
                tblCBHead.Visible = False
                tblCB.Visible = False
                tblSpc4.Visible = False
            End If

        Else
            tblCBHead.Visible = False
            tblCB.Visible = False
            tblSpc4.Visible = False
        End If
    End Sub


  Private Sub TrxGLRight()
        Dim blnGL As String

        blnGL = False

        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.GeneralLedger), intModuleActivate) Then

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLJournal), intGLAR) = True) Then
                tblGL.Rows(0).Visible = True
                blnGL = True
            Else
                tblGL.Rows(0).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLPosting), intGLAR) = True) Then
                tblGL.Rows(1).Visible = True
                blnGL = True
            Else
                tblGL.Rows(1).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLJournalAdj), intGLAR) = True) Then
                tblGL.Rows(2).Visible = True
                blnGL = True
            Else
                tblGL.Rows(2).Visible = False
            End If

            If intLocLevel = 3 Or intLocLevel = 2 Then
                tblGL.Rows(4).Visible = False
                tblGL.Rows(5).Visible = False
                tblGL.Rows(6).Visible = False
                tblGL.Rows(7).Visible = False
                tblGL.Rows(8).Visible = False
                tblGL.Rows(9).Visible = False
                tblGL.Rows(10).Visible = False
            End If

            If blnGL = False Then
                tblGLHead.Visible = False
                tblGL.Visible = False
                tblSpc1.Visible = False
            End If

        Else
            tblGLHead.Visible = False
            tblGL.Visible = False
            tblSpc1.Visible = False
        End If

    End Sub


   Private Sub TrxARRight()
        Dim blnAR As Boolean

        blnAR = False

        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Billing), intModuleActivate) Then


            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BIInvoice), intBIAR) = True) Then
                tblBill.Rows(0).Visible = True
                blnAR = True
            Else
                tblBill.Rows(0).Visible = False
            End If

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BINote), intBIAR) = True) Then
                tblBill.Rows(1).Visible = True
                blnAR = True
            Else
                tblBill.Rows(1).Visible = False
            End If

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BICreditNote), intBIAR) = True) Then
                tblBill.Rows(2).Visible = True
                blnAR = True
            Else
                tblBill.Rows(2).Visible = False
            End If

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BIJournal), intBIAR) = True) Then
                tblBill.Rows(3).Visible = True
                blnAR = True
            Else
                tblBill.Rows(3).Visible = False
            End If

            If blnAR = False Then
                tblBillHead.Visible = False
                tblBill.Visible = False
                tblSpc3.Visible = False
            End If

        Else
            tblBillHead.Visible = False
            tblBill.Visible = False
            tblSpc3.Visible = False
        End If

     End Sub


       Private Sub TrxAPRight()

        Dim blnAP As Boolean
        blnAP = False

        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.AccountPayable), intModuleActivate) Then

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APInvoiceReceive), intAPAR) = True) Then
                tblAP.Rows(0).Visible = True
                blnAP = True
            Else
                tblAP.Rows(0).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APDebitNote), intAPAR) = True) Then
                tblAP.Rows(1).Visible = True
                blnAP = True
            Else
                tblAP.Rows(1).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APCreditNote), intAPAR) = True) Then
                tblAP.Rows(2).Visible = True
                blnAP = True
            Else
                tblAP.Rows(2).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APCreditorJournal), intAPAR) = True) Then
                tblAP.Rows(3).Visible = True
                blnAP = True
            Else
                tblAP.Rows(3).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APInvoiceReceive), intAPAR) = True) Then
                tblAP.Rows(4).Visible = True
                blnAP = True
            Else
                tblAP.Rows(4).Visible = False
            End If

            If blnAP = False Then
                tblAPHead.Visible = False
                tblAP.Visible = False
                tblSpc2.Visible = False
            End If
        Else
            tblAPHead.Visible = False
            tblAP.Visible = False
            tblSpc2.Visible = False
        End If
    End Sub



  Private Sub DisableAllHead()


        tblGLHead.Visible = False
        tblSpc1.Visible = False
        tblAPHead.Visible = False
        tblSpc2.Visible = False
        tblBillHead.Visible = False
        tblSpc3.Visible = False
        tblCBHead.Visible = False
        tblSpc4.Visible = False
        tblFAHead.Visible = False
        tblSpc5.Visible = False
        tblTXHead.Visible = False

    End Sub



    Sub GetEntireLangCap()
        Dim objLangCapDs As New DataSet()
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

            objLangCapVw = New DataView(objLangCapDs.Tables(0))

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=MENU_FASETUP_GET_LANGCAP&errmesg=&redirect=")
        End Try

    End Sub


    Function GetCaption(ByVal pv_TermCode As Integer) As String

        Dim intIndex As Integer

        objLangCapVw.Sort = "TermCode"

        intIndex = objLangCapVw.Find(pv_TermCode)

        If intIndex = -1 Then
            Return ""
        Else
            If intLocType = 4 Then
                Return Trim(objLangCapVw(intIndex).Item("BusinessTermMW"))
            Else
                Return Trim(objLangCapVw(intIndex).Item("BusinessTerm"))
            End If
        End If

    End Function

    
    Private Sub TrxTXRight()
        Dim blnTX As Boolean
        blnTX = False


        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Canteen), intModuleActivate) Then

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTPurchaseRequest), intTXAR) = True) Then
                tblTX.Rows(0).Visible = False
                lnkTX01.Text = "Tax Verification"
                blnTX = True
            Else
                tblTX.Rows(0).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTReceive), intTXAR) = True) Then
                tblTX.Rows(1).Visible = True
                lnkTX02.Text = "Income Tax Slip"
                blnTX = True
            Else
                tblTX.Rows(1).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTReceive), intTXAR) = True) Then
                tblTX.Rows(2).Visible = True
                lnkTX03.Text = "VAT Number"
                blnTX = True
            Else
                tblTX.Rows(2).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTReceive), intTXAR) = True) Then
                tblTX.Rows(3).Visible = True
                lnkTX04.Text = "VAT Listing"
                blnTX = True
            Else
                tblTX.Rows(3).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTReceive), intTXAR) = True) Then
                tblTX.Rows(4).Visible = True
                lnkTX05.Text = "VAT IN"
                blnTX = True
            Else
                tblTX.Rows(4).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTReceive), intTXAR) = True) Then
                tblTX.Rows(5).Visible = True
                lnkTX06.Text = "VAT OUT"
                blnTX = True
            Else
                tblTX.Rows(5).Visible = False
            End If

            'If blnTX = False Then
            '    tblTXHead.Visible = False
            '    tblTX.Visible = False

            'End If
        Else
            tblTXHead.Visible = False
            tblTX.Visible = False

        End If
    End Sub


End Class
