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
Imports Microsoft.VisualBasic

Imports agri.GlobalHdl.clsAccessRights

Public Class Data_downloadMM : Inherits Page

    Protected WithEvents tblDownload As HtmlTable
    Protected WithEvents tblSave As HtmlTable

    Protected WithEvents ddlTable As DropDownList
    Protected WithEvents ddlFType As DropDownList

    Protected WithEvents lblDownloadfile As Label
    Protected WithEvents lblErrMesage As Label
    Protected WithEvents btnGenerate As ImageButton
    Protected WithEvents txtDate As TextBox
    Protected WithEvents txtDateTo As TextBox
    Protected WithEvents DataGrid1 As DataGrid

    Protected WithEvents txtSupplier As TextBox
    Protected WithEvents txtDocNoFrom As TextBox
    Protected WithEvents txtDocNoTo As TextBox
    Protected WithEvents txtProdType As TextBox
    Protected WithEvents txtProdBrand As TextBox
    Protected WithEvents txtProdModel As TextBox
    Protected WithEvents txtProdCat As TextBox
    Protected WithEvents txtProdMaterial As TextBox
    Protected WithEvents txtStkAna As TextBox
    Protected WithEvents txtItemCode As TextBox
    Protected WithEvents txtItemDesc As TextBox
    Protected WithEvents lstPOType As DropDownList
    Protected WithEvents lstStatus As DropDownList
    Protected WithEvents rbItem As RadioButton
    Protected WithEvents rbSupp As RadioButton
    Protected WithEvents txtAddNote As TextBox


    Protected objAR As New agri.GlobalHdl.clsAccessRights()
    Protected objSysCfg As New agri.PWSystem.clsConfig()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objPUTrx As New agri.PU.clsTrx()


    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intCBAR As Integer
    Dim strAcceptFormat As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intCBAR = Session("SS_CBAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBCashBank), intCBAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        End If
        If Not Page.IsPostBack Then
            txtDate.Text = Format(DateAdd(DateInterval.Day, -8, Now()), "dd/MM/yyyy")
            txtDateTo.Text = Format(DateAdd(DateInterval.Day, -1, Now()), "dd/MM/yyyy")
            BindPOType()
            BindStatus()
        End If

    End Sub

    Sub BindPOType()

        lstPOType.Items.Add(New ListItem(objPUTrx.mtdGetPOType(objPUTrx.EnumPOType.All), objPUTrx.EnumPOType.All))
        lstPOType.Items.Add(New ListItem(objPUTrx.mtdGetPOType(objPUTrx.EnumPOType.Canteen), objPUTrx.EnumPOType.Canteen))
        lstPOType.Items.Add(New ListItem(objPUTrx.mtdGetPOType(objPUTrx.EnumPOType.DirectCharge), objPUTrx.EnumPOType.DirectCharge))
        lstPOType.Items.Add(New ListItem(objPUTrx.mtdGetPOType(objPUTrx.EnumPOType.FixedAsset), objPUTrx.EnumPOType.FixedAsset))
        lstPOType.Items.Add(New ListItem("Stock / Workshop", objPUTrx.EnumPOType.Stock))


    End Sub

    Sub BindStatus()

        lstStatus.Items.Add(New ListItem(objPUTrx.mtdGetPOStatus(objPUTrx.EnumPOStatus.All), objPUTrx.EnumPOStatus.All))
        lstStatus.Items.Add(New ListItem(objPUTrx.mtdGetPOStatus(objPUTrx.EnumPOStatus.Active), objPUTrx.EnumPOStatus.Active))
        lstStatus.Items.Add(New ListItem(objPUTrx.mtdGetPOStatus(objPUTrx.EnumPOStatus.Cancelled), objPUTrx.EnumPOStatus.Cancelled))
        lstStatus.Items.Add(New ListItem(objPUTrx.mtdGetPOStatus(objPUTrx.EnumPOStatus.Closed), objPUTrx.EnumPOStatus.Closed))
        lstStatus.Items.Add(New ListItem(objPUTrx.mtdGetPOStatus(objPUTrx.EnumPOStatus.Confirmed), objPUTrx.EnumPOStatus.Confirmed))
        lstStatus.Items.Add(New ListItem(objPUTrx.mtdGetPOStatus(objPUTrx.EnumPOStatus.Deleted), objPUTrx.EnumPOStatus.Deleted))
        lstStatus.Items.Add(New ListItem(objPUTrx.mtdGetPOStatus(objPUTrx.EnumPOStatus.Invoiced), objPUTrx.EnumPOStatus.Invoiced))
        lstStatus.SelectedIndex = 4

    End Sub


    Sub btnGenerate_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strSupplier As String
        Dim strDocNoFrom As String
        Dim strDocNoTo As String
        Dim strProdType As String
        Dim strProdBrand As String
        Dim strProdModel As String
        Dim strProdCat As String
        Dim strProdMat As String
        Dim strStkAna As String
        Dim strItemCode As String
        Dim strItemDesc As String
        Dim strPOType As String
        Dim strStatus As String
        Dim strLnStatus As String

        Dim strddlAccMth As String
        Dim strddlAccYr As String
        Dim strHistoryBy As String
        Dim strAddNote As String


        Dim strParam As String = ""
        Dim strQuery As String = ""
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim strDateTo As String = Date_Validation(txtDateTo.Text, False)

        strddlAccMth = ""
        strddlAccYr = ""
        
        If txtSupplier.Text = "" Then
            strSupplier = ""
        Else
            strSupplier = Trim(txtSupplier.Text)
        End If

        If txtDocNoFrom.Text = "" Then
            strDocNoFrom = ""
        Else
            strDocNoFrom = Trim(txtDocNoFrom.Text)
        End If

        If txtDocNoTo.Text = "" Then
            strDocNoTo = ""
        Else
            strDocNoTo = Trim(txtDocNoTo.Text)
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

        If txtItemCode.Text = "" Then
            strItemCode = ""
        Else
            strItemCode = Trim(txtItemCode.Text)
        End If

        If txtItemDesc.Text = "" Then
            strItemDesc = ""
        Else
            strItemDesc = Trim(txtItemDesc.Text)
        End If
        strAddNote = txtAddNote.Text.Trim


        strPOType = Trim(lstPOType.SelectedItem.Value)
        strStatus = Trim(lstStatus.SelectedItem.Value)

        strSupplier = txtSupplier.Text.Trim()
        strProdType = txtProdType.Text.Trim()
        strProdBrand = txtProdBrand.Text.Trim()
        strProdModel = txtProdModel.Text.Trim()
        strProdCat = txtProdCat.Text.Trim()
        strProdMat = txtProdMaterial.Text.Trim()
        strStkAna = txtStkAna.Text.Trim()
        strItemCode = txtItemCode.Text.Trim()
        strItemDesc = txtItemDesc.Text.Trim()
        If rbItem.Checked = True Then
            strHistoryBy = "1"
        Else
            strHistoryBy = "2"
        End If
        strAddNote = txtAddNote.Text.Trim()

        strQuery = "CompName=" & ddlTable.SelectedItem.Value.Trim() & _
                   "&Location=" & strLocation & _
                   "&Supplier=" & strSupplier & _
                   "&DDLAccMth=" & strddlAccMth & _
                   "&DDLAccYr=" & strddlAccYr & _
                   "&DocNoFrom=" & strDocNoFrom & _
                   "&DocNoTo=" & strDocNoTo & _
                   "&DocDateFrom=" & strDate & _
                   "&DocDateTo=" & strDateTo & _
                   "&ProdType=" & strProdType & _
                   "&ProdBrand=" & strProdBrand & _
                   "&ProdModel=" & strProdModel & _
                   "&ProdCat=" & strProdCat & _
                   "&ProdMat=" & strProdMat & _
                   "&StkAna=" & strStkAna & _
                   "&ItemCode=" & strItemCode & _
                   "&ItemDesc=" & strItemDesc & _
                   "&POType=" & strPOType & _
                   "&Status=" & strStatus & _
                   "&LnStatus=" & strLnStatus & _
                   "&HistoryBy=" & strHistoryBy & _
                   "&AddNote=" & strAddNote & _
                   "&DownloadID=" & ddlTable.SelectedItem.Value.Trim() & _
                   "&Ftype=" & ddlFType.SelectedItem.Value.Trim()

        Response.Redirect("Data_download_savefileMM.aspx?" & strQuery)

    End Sub

    Function Date_Validation(ByVal pv_strInputDate As String, ByVal pv_blnIsShortDate As Boolean) As String
        Dim objSysCfgDs As New Object
        Dim objActualDate As New Object
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
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CB_DAILY_GET&errmesg=" & Exp.Message & "&redirect=CB_StdRpt_BankTransaction.aspx")
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

End Class
