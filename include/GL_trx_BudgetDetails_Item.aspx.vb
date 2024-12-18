
Imports System
Imports System.Data
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports System.Globalization
Imports System.Math 


Public Class GL_trx_BudgetDetails_Item : Inherits Page

    Dim objGLTrx As New agri.GL.clsTrx()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Protected WithEvents lblPleaseSelect As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents ddlAccount As DropDownList
    Protected WithEvents ddlItem As DropDownList
    Protected WithEvents lblErrAccount As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblBlock As Label
    Protected WithEvents lblErrBlock As Label
    Protected WithEvents lblAccount As Label
    Protected WithEvents lblCreateDate As Label
    Protected WithEvents lblVehicle As Label
    Protected WithEvents lblErrVehicle As Label
    Protected WithEvents lblLastUpdate As Label

    Protected WithEvents txtYearBudget As TextBox
    Protected WithEvents lblUpdateBy As Label

    Protected WithEvents txtAmount As TextBox
    Protected WithEvents txtTtlFisik As TextBox
    Protected WithEvents lblErrAmount As Label
    Protected WithEvents lblCode As Label

	Protected WithEvents txtDiv As TextBox
    Protected WithEvents txtTT As TextBox
	
    Protected WithEvents lblValueError As Label

    Protected WithEvents txtRp1b As TextBox
    Protected WithEvents txtRp2b As TextBox
    Protected WithEvents txtRp3b As TextBox
    Protected WithEvents txtRp4b As TextBox
    Protected WithEvents txtRp5b As TextBox
    Protected WithEvents txtRp6b As TextBox
    Protected WithEvents txtRp7b As TextBox
    Protected WithEvents txtRp8b As TextBox
    Protected WithEvents txtRp9b As TextBox
    Protected WithEvents txtRp10b As TextBox
    Protected WithEvents txtRp11b As TextBox
    Protected WithEvents txtRp12b As TextBox


    Protected WithEvents txtRp1f As TextBox
    Protected WithEvents txtRp2f As TextBox
    Protected WithEvents txtRp3f As TextBox
    Protected WithEvents txtRp4f As TextBox
    Protected WithEvents txtRp5f As TextBox
    Protected WithEvents txtRp6f As TextBox
    Protected WithEvents txtRp7f As TextBox
    Protected WithEvents txtRp8f As TextBox
    Protected WithEvents txtRp9f As TextBox
    Protected WithEvents txtRp10f As TextBox
    Protected WithEvents txtRp11f As TextBox
    Protected WithEvents txtRp12f As TextBox

    Protected WithEvents btnSave As ImageButton
    Protected WithEvents btnDelete As ImageButton
    Protected WithEvents btnBack As ImageButton

    Protected WithEvents hidTrxID As HtmlInputHidden

    Dim objAccDs As New Dataset()
    Dim objBlkDs As New Dataset()
    Dim objVehDs As New Dataset()

    Dim strParam As String
    Dim objDataSet As New DataSet()
    Dim objLangCapDs As New Object()
    Dim intErrNo As Integer

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intGLAR As Integer

    Dim intConfigSetting As Integer

    Dim strBdgYear As String
    Dim strBdgAcc As String
    Dim strLocType As String
    Dim BlockTag As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        intGLAR = Session("SS_ADAR")
        intConfigSetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")

     If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
		ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADBudgeting), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
       Else
            onload_GetLangCap()

            lblErrBlock.Visible = False
            'lblErrVehicle.Visible = False
            lblValueError.Visible = False

            If Not IsPostBack Then
                If Not Request.QueryString("TrxID") = "" Then
                    hidTrxID.Value = Request.QueryString("TrxID")
                End If

                If Not hidTrxID.Value = "" Then
                    DisplayData(hidTrxID.Value)
                Else
                    BindAccount("")
                    BindItemCode("")
                    EnableControl()
                    btnSave.Visible = True
                    btnDelete.Visible = False
                End If
            End If
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        lblAccount.Text = GetCaption(objLangCap.EnumLangCap.Account) & lblCode.Text


        lblErrAccount.Text = "<BR>" & "Please Select " & lblAccount.Text
       

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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=FA_CLSTRX_AssetAddDETAILS_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=FA/trx/FA_trx_AssetAddDetails.aspx")
        End Try
    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

        For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If strLocType = objAdminLoc.EnumLocType.Mill Then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                Else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                End If
                Exit For
            End If
        Next
    End Function

    Protected Function LoadData(ByVal vstrTrxID As String) As DataSet

        Dim strOpCode As String = "GL_CLSTRX_BUDGET_ITEM_GET"
        Dim intErrNo As Integer
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        strParamName = "TRXID"
        strParamValue = vstrTrxID

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BUDGET_GET_DATA&errmesg=" & lblErrMessage.Text & "&redirect=gl/trx/gl_trx_budget_list.aspx")
        End Try

        Return objDataSet

    End Function

    Sub DisableControl()

        txtRp1b.Enabled = False
        txtRp2b.Enabled = False
        txtRp3b.Enabled = False
        txtRp4b.Enabled = False
        txtRp5b.Enabled = False
        txtRp6b.Enabled = False
        txtRp7b.Enabled = False
        txtRp8b.Enabled = False
        txtRp9b.Enabled = False
        txtRp10b.Enabled = False
        txtRp11b.Enabled = False
        txtRp12b.Enabled = False
        txtAmount.Enabled = False

        txtRp1f.Enabled = False
        txtRp2f.Enabled = False
        txtRp3f.Enabled = False
        txtRp4f.Enabled = False
        txtRp5f.Enabled = False
        txtRp6f.Enabled = False
        txtRp7f.Enabled = False
        txtRp8f.Enabled = False
        txtRp9f.Enabled = False
        txtRp10f.Enabled = False
        txtRp11f.Enabled = False
        txtRp12f.Enabled = False
        txtTtlFisik.Enabled = False

    End Sub

    Sub EnableControl()
        txtRp1b.Enabled = True
        txtRp2b.Enabled = True
        txtRp3b.Enabled = True
        txtRp4b.Enabled = True
        txtRp5b.Enabled = True
        txtRp6b.Enabled = True
        txtRp7b.Enabled = True
        txtRp8b.Enabled = True
        txtRp9b.Enabled = True
        txtRp10b.Enabled = True
        txtRp11b.Enabled = True
        txtRp12b.Enabled = True
        txtAmount.Enabled = True

        txtRp1f.Enabled = True
        txtRp2f.Enabled = True
        txtRp3f.Enabled = True
        txtRp4f.Enabled = True
        txtRp5f.Enabled = True
        txtRp6f.Enabled = True
        txtRp7f.Enabled = True
        txtRp8f.Enabled = True
        txtRp9f.Enabled = True
        txtRp10f.Enabled = True
        txtRp11f.Enabled = True
        txtRp12f.Enabled = True
        txtTtlFisik.Enabled = True

    End Sub

    Sub DisplayData(ByVal vstrTrxID As String)

        Dim dsTx As DataSet = LoadData(vstrTrxID)
        Dim strAccCode As String
        Dim strItemCode As String

        If dsTx.Tables(0).Rows.Count > 0 Then

            lblStatus.Text = objGLSetup.mtdGetActSatus(Trim(dsTx.Tables(0).Rows(0).Item("Status")))
            lblCreateDate.Text = Trim(objGlobal.GetLongDate(dsTx.Tables(0).Rows(0).Item("CreateDate")))
            lblLastUpdate.Text = Trim(objGlobal.GetLongDate(dsTx.Tables(0).Rows(0).Item("UpdateDate")))
            lblUpdateBy.Text = Trim(dsTx.Tables(0).Rows(0).Item("Username"))


            txtYearBudget.Text = Trim(dsTx.Tables(0).Rows(0).Item("AccYear"))
            txtAmount.Text = dsTx.Tables(0).Rows(0).Item("TotalAmount")
            txtTtlFisik.Text = dsTx.Tables(0).Rows(0).Item("TotalFisik")

			txtDiv.Text = dsTx.Tables(0).Rows(0).Item("CodeBlkGrp")
			txtTT.Text   = dsTx.Tables(0).Rows(0).Item("CodeBlk")

            txtRp1b.Text = dsTx.Tables(0).Rows(0).Item("Rp1")
            txtRp2b.Text = dsTx.Tables(0).Rows(0).Item("Rp2")
            txtRp3b.Text = dsTx.Tables(0).Rows(0).Item("Rp3")
            txtRp4b.Text = dsTx.Tables(0).Rows(0).Item("Rp4")
            txtRp5b.Text = dsTx.Tables(0).Rows(0).Item("Rp5")
            txtRp6b.Text = dsTx.Tables(0).Rows(0).Item("Rp6")
            txtRp7b.Text = dsTx.Tables(0).Rows(0).Item("Rp7")
            txtRp8b.Text = dsTx.Tables(0).Rows(0).Item("Rp8")
            txtRp9b.Text = dsTx.Tables(0).Rows(0).Item("Rp9")
            txtRp10b.Text = dsTx.Tables(0).Rows(0).Item("Rp10")
            txtRp11b.Text = dsTx.Tables(0).Rows(0).Item("Rp11")
            txtRp12b.Text = dsTx.Tables(0).Rows(0).Item("Rp12")

            txtRp1f.Text = dsTx.Tables(0).Rows(0).Item("FS1")
            txtRp2f.Text = dsTx.Tables(0).Rows(0).Item("FS2")
            txtRp3f.Text = dsTx.Tables(0).Rows(0).Item("FS3")
            txtRp4f.Text = dsTx.Tables(0).Rows(0).Item("FS4")
            txtRp5f.Text = dsTx.Tables(0).Rows(0).Item("FS5")
            txtRp6f.Text = dsTx.Tables(0).Rows(0).Item("FS6")
            txtRp7f.Text = dsTx.Tables(0).Rows(0).Item("FS7")
            txtRp8f.Text = dsTx.Tables(0).Rows(0).Item("FS8")
            txtRp9f.Text = dsTx.Tables(0).Rows(0).Item("FS9")
            txtRp10f.Text = dsTx.Tables(0).Rows(0).Item("FS10")
            txtRp11f.Text = dsTx.Tables(0).Rows(0).Item("FS11")
            txtRp12f.Text = dsTx.Tables(0).Rows(0).Item("FS12")

            strAccCode = Trim(dsTx.Tables(0).Rows(0).Item("AccCode"))
            strItemCode = Trim(dsTx.Tables(0).Rows(0).Item("ItemCode"))
            BindAccount(strAccCode)
            BindItemCode(strItemCode)

            Select Case Trim(lblStatus.Text)
                Case objGLSetup.mtdGetActSatus(objGLSetup.EnumActStatus.Active)
                    EnableControl()
                    btnSave.Visible = True
                    btnDelete.Visible = True
                    btnDelete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Case objGLSetup.mtdGetActSatus(objGLSetup.EnumActStatus.Deleted)
                    DisableControl()
                    btnSave.Visible = False
                    btnDelete.Visible = False
            End Select
        End If
    End Sub

    Sub BindAccount(ByVal pv_strAccCode As String)
        Dim strOpCd As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        Dim strParam As String = "Order By ACC.AccCode|And ACC.Status = '" & objGLSetup.EnumAccountCodeStatus.Active & "'"
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer = 0

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.AccountCode, _
                                                   objAccDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=FA_TRX_ASSETADD_ACCCODE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objAccDs.Tables(0).Rows.Count - 1
            If objAccDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(pv_strAccCode) Then
                intSelectedIndex = intCnt + 1
                Exit For
            End If
        Next

        dr = objAccDs.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("_Description") = lblPleaseSelect.Text & lblAccount.Text
        objAccDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlAccount.DataSource = objAccDs.Tables(0)
        ddlAccount.DataValueField = "AccCode"
        ddlAccount.DataTextField = "_Description"
        ddlAccount.DataBind()
        ddlAccount.SelectedIndex = intSelectedIndex
    End Sub


    Sub BindItemCode(ByVal pv_strItemCode As String)
        Dim strOpCd As String = "GL_CLSTRX_DDL_ITEM_SEARCH"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer = 0

        strParamName = "LocCode"
        strParamValue = strLocation

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objAccDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=FA_TRX_ASSETADD_ACCCODE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objAccDs.Tables(0).Rows.Count - 1
            objAccDs.Tables(0).Rows(intCnt).Item("Description") = objAccDs.Tables(0).Rows(intCnt).Item("Description").Trim()
            If objAccDs.Tables(0).Rows(intCnt).Item("ItemCode") = pv_strItemCode Then
                intSelectedIndex = intCnt + 1
                Exit For
            End If
        Next

        dr = objAccDs.Tables(0).NewRow()
        dr("ItemCode") = ""
        dr("Description") = "Please Select Item Code"
        objAccDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlItem.DataSource = objAccDs.Tables(0)
        ddlItem.DataValueField = "ItemCode"
        ddlItem.DataTextField = "Description"
        ddlItem.DataBind()
        ddlItem.SelectedIndex = intSelectedIndex
    End Sub



    Sub btnSave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)

        Dim strOpCode As String
        Dim intErrNo As Integer
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim objRslSet As New DataSet()

        'validasi 
        txtAmount.Text = Val(txtRp1b.Text) + Val(txtRp2b.Text) + Val(txtRp3b.Text) + Val(txtRp4b.Text) + _
                                  Val(txtRp5b.Text) + Val(txtRp6b.Text) + Val(txtRp7b.Text) + Val(txtRp8b.Text) + _
                                  Val(txtRp9b.Text) + Val(txtRp10b.Text) + Val(txtRp11b.Text) + Val(txtRp12b.Text) 

      

        txtTtlFisik.Text = Val(txtRp1f.Text) + Val(txtRp2f.Text) + Val(txtRp3f.Text) + Val(txtRp4f.Text) + _
                                 Val(txtRp5f.Text) + Val(txtRp6f.Text) + Val(txtRp7f.Text) + Val(txtRp8f.Text) + _
                                 Val(txtRp9f.Text) + Val(txtRp10f.Text) + Val(txtRp11f.Text) + Val(txtRp12f.Text) 




        If Trim(lblStatus.Text) <> "" Then
            strOpCode = "GL_CLSTRX_BUDGET_ITEM_UPDATE"
        Else
            strOpCode = "GL_CLSTRX_BUDGET_ITEM_ADD"
        End If

        strParamName = "TRXID|LOCCODE|ACCYEAR|ACCCODE|AMOUNT|TTLFISIK|USERID|STATUS|ITEMCODE|"
        strParamName = strParamName & "RP1B|RP2B|RP3B|RP4B|RP5B|RP6B|"
        strParamName = strParamName & "RP7B|RP8B|RP9B|RP10B|RP11B|RP12B|"
        strParamName = strParamName & "FS1|FS2|FS3|FS4|FS5|"
        strParamName = strParamName & "FS6|FS7|FS8|FS9|FS10|FS11|FS12"


        strParamValue = hidTrxID.Value & "|" & strLocation & "|" & txtYearBudget.Text & "|" & ddlAccount.SelectedValue & "|"
        strParamValue = strParamValue & txtAmount.Text & "|" & txtTtlFisik.Text & "|" & strUserId & "|" & objGLSetup.EnumActStatus.Active & "|" & ddlItem.SelectedValue & "|"
        strParamValue = strParamValue & txtRp1b.Text & "|" & txtRp2b.Text & "|" & txtRp3b.Text & "|"
        strParamValue = strParamValue & txtRp4b.Text & "|" & txtRp5b.Text & "|" & txtRp6b.Text & "|"
        strParamValue = strParamValue & txtRp7b.Text & "|" & txtRp8b.Text & "|" & txtRp9b.Text & "|"
        strParamValue = strParamValue & txtRp10b.Text & "|" & txtRp11b.Text & "|" & txtRp12b.Text & "|"
        strParamValue = strParamValue & txtRp1f.Text & "|" & txtRp2f.Text & "|" & txtRp3f.Text & "|"
        strParamValue = strParamValue & txtRp4f.Text & "|" & txtRp5f.Text & "|" & txtRp6f.Text & "|"
        strParamValue = strParamValue & txtRp7f.Text & "|" & txtRp8f.Text & "|" & txtRp9f.Text & "|"
        strParamValue = strParamValue & txtRp10f.Text & "|" & txtRp11f.Text & "|" & txtRp12f.Text


        Try

            If Trim(lblStatus.Text) <> "" Then
                intErrNo = objGLTrx.mtdInsertDataCommon(strOpCode, _
                                                       strParamName, _
                                                       strParamValue)

            Else

                intErrNo = objGLTrx.mtdGetDataCommon(strOpCode, _
                                                    strParamName, _
                                                    strParamValue, _
                                                    objRslSet)



                hidTrxID.Value = objRslSet.Tables(0).Rows(0).Item("ID")

            End If

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_BUDGET_UPDATE&errmesg=" & lblErrMessage.Text & "&redirect=gl/trx/GL_trx_Budget_list")

        Finally

            DisplayData(hidTrxID.Value)

        End Try


    End Sub


    Sub btnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)

        Dim strOpCode As String = "GL_CLSTRX_BUDGET_ITEM_DEL"
        Dim intErrNo As Integer
        Dim strParamName As String = ""
        Dim strParamValue As String = ""


        strParamName = "TRXID|STATUS|USERID"
        strParamValue = hidTrxID.Value & "|" & objGLSetup.EnumActStatus.Deleted & "|" & strUserId


        Try
            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCode, _
                                                   strParamName, _
                                                   strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_BUDGET_DELETED&errmesg=" & lblErrMessage.Text & "&redirect=gl/trx/gl_trx_budget_list")

        Finally

            DisplayData(hidTrxID.Value)

        End Try


    End Sub

	
	Sub btnGenAmount_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim Tmp As Double
		Dim Sisa As Double
        Dim Mo As Double
		
		'Amount
        Tmp = round(txtAmount.Text / 12,2)
		Mo = txtAmount.Text mod 12
		if Mo <> 0 then
		Sisa = (txtAmount.Text-(12*Tmp ))
		else
		Sisa = 0
		end if
		
		txtRp1b.Text = Tmp
		txtRp2b.Text = Tmp
		txtRp3b.Text = Tmp
		txtRp4b.Text = Tmp
		txtRp5b.Text = Tmp
		txtRp6b.Text = Tmp
		txtRp7b.Text = Tmp
		txtRp8b.Text = Tmp
		txtRp9b.Text = Tmp
		txtRp10b.Text = Tmp
		txtRp11b.Text = Tmp
		txtRp12b.Text = Tmp + Sisa
		
		'Fisik
		Tmp = round(txtTtlFisik.Text / 12,2)
		Mo = txtTtlFisik.Text mod 12
		if Mo <> 0 then
		Sisa = (txtTtlFisik.Text-(12*Tmp ))
		else
		Sisa = 0
		end if
        
		txtRp1f.Text = Tmp
		txtRp2f.Text = Tmp
		txtRp3f.Text = Tmp
		txtRp4f.Text = Tmp
		txtRp5f.Text = Tmp
		txtRp6f.Text = Tmp
		txtRp7f.Text = Tmp
		txtRp8f.Text = Tmp
		txtRp9f.Text = Tmp
		txtRp10f.Text = Tmp
		txtRp11f.Text = Tmp
		txtRp12f.Text = Tmp + Sisa
		
	
	End Sub
	
    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)

        Response.Redirect("GL_trx_Budget_Item_list.aspx")

    End Sub

End Class
