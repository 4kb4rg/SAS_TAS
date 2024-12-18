
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

Public Class menu_mmtrx : Inherits Page


    Dim strUserId As String
    Dim strLangCode As String
    Dim strCompany As String
    Dim strLocation As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intLocType As Integer
    Dim intModuleActivate As Integer
    Dim strLocTag As String

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objLangCapVw As New DataView

   
    Dim intINAR As Long
    Dim intPUAR As Long
   

    Const C_ADMIN = "ADMIN"


    Protected WithEvents tblINHead As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tblIN As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tblDATAHead As System.Web.UI.HtmlControls.HtmlTable

    Protected WithEvents tblSubMenuTrx As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents TblRight As System.Web.UI.HtmlControls.HtmlTableCell

    Protected WithEvents lnkIN01 As System.Web.UI.WebControls.HyperLink
	Protected WithEvents lnkIN01_APP As System.Web.UI.WebControls.HyperLink
	Protected WithEvents lnkIN01_REV As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkIN02 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkIN03 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkIN04 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkIN05 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkIN06 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkIN07 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkIN08 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkIN09 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkIN10 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents LinkButton1 As System.Web.UI.WebControls.LinkButton


    Protected WithEvents tblSpc1 As System.Web.UI.HtmlControls.HtmlTable

    Protected WithEvents tblPUHead As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tblPU As System.Web.UI.HtmlControls.HtmlTable

    Protected WithEvents lnkPU01 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkPU02 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkPU03 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkPU04 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkPU05 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkPU06 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkPU07 As System.Web.UI.WebControls.HyperLink


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)


        strUserId = Session("SS_USERID")

        intModuleActivate = Session("SS_MODULEACTIVATE")
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strLangCode = Session("SS_LANGCODE")
        intLocType = Session("SS_LOCTYPE")
        strAccMonth = Session("SS_FAACCMONTH")
        strAccYear = Session("SS_FAACCYEAR")

        If strUserId = "" Then
            Response.Write("<Script language=""Javascript"">parent.right.location.href = '/Login.aspx'</Script>")
        Else



            intINAR = Session("SS_INAR")
            intPUAR = Session("SS_PUAR")

            onLoad_Display()


        End If
 
    End Sub

   Sub onLoad_Display()

        If Session("SS_LOCATION") = "" Or Session("SS_MAXPERIOD") = 0 Then
            Call DisableAllHead()
        Else

            Call TrxINRight()
            Call TrxPURight()

        End If

    End Sub


    Private Sub DisableAllHead()

        tblINHead.Visible = False
        tblSpc1.Visible = False
        tblPUHead.Visible = False
       
    End Sub


   Private Sub TrxINRight()

        Dim blnIN As Boolean
        blnIN = False

        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Inventory), intModuleActivate) Then


            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INPurchaseRequest), intINAR) = True) Then
                tblIN.Rows(0).Visible = True
                blnIN = True
            Else
                tblIN.Rows(0).Visible = False
            End If

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INPurchaseRequest), intINAR) = True) Then
                tblIN.Rows(1).Visible = True
                blnIN = True
            Else
                tblIN.Rows(1).Visible = False
            End If

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INPurchaseRequest), intINAR) = True) Then
                tblIN.Rows(2).Visible = False
                blnIN = True
            Else
                tblIN.Rows(2).Visible = False
            End If

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockReceive), intINAR) = True) Then
                tblIN.Rows(3).Visible = True
                blnIN = True
            Else
                tblIN.Rows(3).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockReturnAdvice), intINAR) = True) Then
                tblIN.Rows(4).Visible = True
                blnIN = True
            Else
                tblIN.Rows(4).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockAdjustment), intINAR) = True) Then
                tblIN.Rows(5).Visible = True
                blnIN = True
            Else
                tblIN.Rows(5).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockTransfer), intINAR) = True) Then
                tblIN.Rows(6).Visible = True
                blnIN = True
            Else
                tblIN.Rows(6).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockTransfer), intINAR) = True) Then
                tblIN.Rows(7).Visible = True
                blnIN = True
            Else
                tblIN.Rows(7).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockIssue), intINAR) = True) Then
                tblIN.Rows(8).Visible = True
                blnIN = True
            Else
                tblIN.Rows(8).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockReturn), intINAR) = True) Then
                tblIN.Rows(9).Visible = True
                blnIN = True
            Else
                tblIN.Rows(9).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INFuelIssue), intINAR) = True) Then
                tblIN.Rows(10).Visible = True
                blnIN = True
            Else
                tblIN.Rows(10).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INItemToMachine), intINAR) = True) Then
                tblIN.Rows(11).Visible = True
                blnIN = True
            Else
                tblIN.Rows(11).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockTransferInternal), intINAR) = True) Then
                tblIN.Rows(12).Visible = True
                blnIN = True
            Else
                tblIN.Rows(12).Visible = False
            End If

            'If blnIN = False Then
            '    tblINHead.Visible = False
            '    tblIN.Visible = False
            '    tblSpc1.Visible = False
            'End If

        Else
            tblINHead.Visible = False
            tblIN.Visible = False
            tblSpc1.Visible = False
        End If
    End Sub

    Private Sub TrxPURight()
        Dim blnPU As Boolean

        blnPU = False

        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Purchasing), intModuleActivate) Then

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUPelimpahan), intPUAR) = True) Then
                tblPU.Rows(0).Visible = True
                blnPU = True
                tblPU.Rows(1).Visible = True
                blnPU = True
                tblPU.Rows(2).Visible = True
                blnPU = True
            Else
                tblPU.Rows(0).Visible = False
                tblPU.Rows(1).Visible = False
                tblPU.Rows(2).Visible = False
            End If

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PURPH), intPUAR) = True) Then
                tblPU.Rows(3).Visible = True
                blnPU = True
                tblPU.Rows(4).Visible = True
                blnPU = True
            Else
                tblPU.Rows(3).Visible = False
                tblPU.Rows(4).Visible = False
            End If

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUPurchaseOrder), intPUAR) = True) Then
                tblPU.Rows(5).Visible = True
                blnPU = True
                'tblPU.Rows(6).Visible = True
                'blnPU = True
            Else
                tblPU.Rows(5).Visible = False
                'tblPU.Rows(6).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUGoodsReceive), intPUAR) = True) Then
                tblPU.Rows(6).Visible = True
                blnPU = True
            Else
                tblPU.Rows(6).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUGoodsReturnNote), intPUAR) = True) Then
                tblPU.Rows(7).Visible = True
                blnPU = True
            Else
                tblPU.Rows(7).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUDispatchAdvice), intPUAR) = True) Then
                tblPU.Rows(8).Visible = True
                blnPU = True
            Else
                tblPU.Rows(8).Visible = False
            End If

            'If blnPU = False Then
            '    tblPUHead.Visible = False
            '    tblPU.Visible = False
            'End If
        Else
            tblPUHead.Visible = False
            tblPU.Visible = False
        End If
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

    Sub lnkCollection_Click(sender As Object, e As System.EventArgs) Handles LinkButton1.Click
        'TblRight.Visible = True

        'tblSubMenuTrx.Visible = False

    End Sub


End Class
