
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


Public Class GL_trx_BudgetProd_Karet_Estate_Det : Inherits Page

    Dim objGLTrx As New agri.GL.clsTrx()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Protected WithEvents lblPleaseSelect As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblCreateDate As Label

    Protected WithEvents ddlVehCode As DropDownList
	Protected WithEvents ddlGroupCOA As DropDownList
	Protected WithEvents ddlSubBlok As DropDownList
	
    Protected WithEvents lblErrVehicle As Label
    Protected WithEvents lblLastUpdate As Label

    Protected WithEvents txtYearBudget As TextBox
    Protected WithEvents lblUpdateBy As Label

    Protected WithEvents lblCode As Label

    Protected WithEvents lblValueError As Label

    Protected WithEvents BLX01 As TextBox
	Protected WithEvents BKKK01 As TextBox
	Protected WithEvents BSL01 As TextBox
	Protected WithEvents BLM01 As TextBox
	Protected WithEvents BGS01 As TextBox
	Protected WithEvents BLX02 As TextBox
	Protected WithEvents BKKK02 As TextBox
	Protected WithEvents BSL02 As TextBox
	Protected WithEvents BLM02 As TextBox
	Protected WithEvents BGS02 As TextBox
	Protected WithEvents BLX03 As TextBox
	Protected WithEvents BKKK03 As TextBox
	Protected WithEvents BSL03 As TextBox
	Protected WithEvents BLM03 As TextBox
	Protected WithEvents BGS03 As TextBox
	Protected WithEvents BLX04 As TextBox
	Protected WithEvents BKKK04 As TextBox
	Protected WithEvents BSL04 As TextBox
	Protected WithEvents BLM04 As TextBox
	Protected WithEvents BGS04 As TextBox
	Protected WithEvents BLX05 As TextBox
	Protected WithEvents BKKK05 As TextBox
	Protected WithEvents BSL05 As TextBox
	Protected WithEvents BLM05 As TextBox
	Protected WithEvents BGS05 As TextBox
	Protected WithEvents BLX06 As TextBox
	Protected WithEvents BKKK06 As TextBox
	Protected WithEvents BSL06 As TextBox
	Protected WithEvents BLM06 As TextBox
	Protected WithEvents BGS06 As TextBox
	Protected WithEvents BLX07 As TextBox
	Protected WithEvents BKKK07 As TextBox
	Protected WithEvents BSL07 As TextBox
	Protected WithEvents BLM07 As TextBox
	Protected WithEvents BGS07 As TextBox
	Protected WithEvents BLX08 As TextBox
	Protected WithEvents BKKK08 As TextBox
	Protected WithEvents BSL08 As TextBox
	Protected WithEvents BLM08 As TextBox
	Protected WithEvents BGS08 As TextBox
	Protected WithEvents BLX09 As TextBox
	Protected WithEvents BKKK09 As TextBox
	Protected WithEvents BSL09 As TextBox
	Protected WithEvents BLM09 As TextBox
	Protected WithEvents BGS09 As TextBox
	Protected WithEvents BLX10 As TextBox
	Protected WithEvents BKKK10 As TextBox
	Protected WithEvents BSL10 As TextBox
	Protected WithEvents BLM10 As TextBox
	Protected WithEvents BGS10 As TextBox
	Protected WithEvents BLX11 As TextBox
	Protected WithEvents BKKK11 As TextBox
	Protected WithEvents BSL11 As TextBox
	Protected WithEvents BLM11 As TextBox
	Protected WithEvents BGS11 As TextBox
	Protected WithEvents BLX12 As TextBox
	Protected WithEvents BKKK12 As TextBox
	Protected WithEvents BSL12 As TextBox
	Protected WithEvents BLM12 As TextBox
	Protected WithEvents BGS12 As TextBox

    Protected WithEvents btnSave As ImageButton
    Protected WithEvents btnDelete As ImageButton
    Protected WithEvents btnBack As ImageButton

    Protected WithEvents hidAccYear As HtmlInputHidden
    Protected WithEvents hidVehCode As HtmlInputHidden

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
            lblErrVehicle.Visible = False
            lblValueError.Visible = False

            If Not IsPostBack Then
                If Not Request.QueryString("Idx") = "" Then
                    hidAccYear.Value = Mid(Request.QueryString("Idx"), 1, 4)
                    hidVehCode.Value = Mid(Request.QueryString("Idx"), 5)
                End If

                If Not hidAccYear.Value = "" Then
                    DisplayData(hidAccYear.Value)
					txtyearBudget.text = hidAccYear.Value
                    txtYearBudget.Enabled = False
					
                    ddlGroupCOA.Enabled = False
					ddlSubBlok.Enabled = False
					
                Else
				    txtyearBudget.text = year(datetime.now)
                    BindGroupCoa("")
                    btnSave.Visible = True

                End If
            End If
        End If
    End Sub


    Protected Function LoadData(ByVal vstrAccYear As String) As DataSet

        Dim strOpCode As String = "GL_CLSTRX_BUDGET_PROD_KARET_GET"
        Dim intErrNo As Integer
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        strParamName = "LOC|AY|BLOK"
        strParamValue = strLocation  & "|" & vstrAccYear & "|" &  hidVehCode.Value

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

    Sub DisplayData(ByVal vstrAccYear As String)

        Dim dsTx As DataSet = LoadData(vstrAccYear)
        Dim strVehCode As String

        
		 
		 
        If dsTx.Tables(0).Rows.Count > 0 Then
		BindGroupCoa(trim(dsTx.Tables(0).Rows(0).Item("GroupCoa")) )
		BindSubBlok(trim(dsTx.Tables(0).Rows(0).Item("GroupCoa")),hidVehCode.Value)
            
            BLX01.Text = 	dsTx.Tables(0).Rows(0).Item("BLX01") 
			BKKK01.Text = 	dsTx.Tables(0).Rows(0).Item("BKKK01") 
			BSL01.Text = 	dsTx.Tables(0).Rows(0).Item("BSL01") 
			BLM01.Text = 	dsTx.Tables(0).Rows(0).Item("BLM01") 
			BGS01.Text = 	dsTx.Tables(0).Rows(0).Item("BGS01") 
			
			BLX02.Text = 	dsTx.Tables(0).Rows(0).Item("BLX02") 
			BKKK02.Text = 	dsTx.Tables(0).Rows(0).Item("BKKK02") 
			BSL02.Text = 	dsTx.Tables(0).Rows(0).Item("BSL02") 
			BLM02.Text = 	dsTx.Tables(0).Rows(0).Item("BLM02") 
			BGS02.Text = 	dsTx.Tables(0).Rows(0).Item("BGS02") 
			
			BLX03.Text = 	dsTx.Tables(0).Rows(0).Item("BLX03") 
			BKKK03.Text = 	dsTx.Tables(0).Rows(0).Item("BKKK03") 
			BSL03.Text = 	dsTx.Tables(0).Rows(0).Item("BSL03") 
			BLM03.Text = 	dsTx.Tables(0).Rows(0).Item("BLM03") 
			BGS03.Text = 	dsTx.Tables(0).Rows(0).Item("BGS03") 
			
			BLX04.Text = 	dsTx.Tables(0).Rows(0).Item("BLX04") 
			BKKK04.Text = 	dsTx.Tables(0).Rows(0).Item("BKKK04") 
			BSL04.Text = 	dsTx.Tables(0).Rows(0).Item("BSL04") 
			BLM04.Text = 	dsTx.Tables(0).Rows(0).Item("BLM04") 
			BGS04.Text = 	dsTx.Tables(0).Rows(0).Item("BGS04") 
			
			BLX05.Text = 	dsTx.Tables(0).Rows(0).Item("BLX05") 
			BKKK05.Text = 	dsTx.Tables(0).Rows(0).Item("BKKK05") 
			BSL05.Text = 	dsTx.Tables(0).Rows(0).Item("BSL05") 
			BLM05.Text = 	dsTx.Tables(0).Rows(0).Item("BLM05") 
			BGS05.Text = 	dsTx.Tables(0).Rows(0).Item("BGS05") 
			
			BLX06.Text = 	dsTx.Tables(0).Rows(0).Item("BLX06") 
			BKKK06.Text = 	dsTx.Tables(0).Rows(0).Item("BKKK06") 
			BSL06.Text = 	dsTx.Tables(0).Rows(0).Item("BSL06") 
			BLM06.Text = 	dsTx.Tables(0).Rows(0).Item("BLM06") 
			BGS06.Text = 	dsTx.Tables(0).Rows(0).Item("BGS06") 
			
			BLX07.Text = 	dsTx.Tables(0).Rows(0).Item("BLX07") 
			BKKK07.Text = 	dsTx.Tables(0).Rows(0).Item("BKKK07") 
			BSL07.Text = 	dsTx.Tables(0).Rows(0).Item("BSL07") 
			BLM07.Text = 	dsTx.Tables(0).Rows(0).Item("BLM07") 
			BGS07.Text = 	dsTx.Tables(0).Rows(0).Item("BGS07") 
			
			BLX08.Text = 	dsTx.Tables(0).Rows(0).Item("BLX08") 
			BKKK08.Text = 	dsTx.Tables(0).Rows(0).Item("BKKK08") 
			BSL08.Text = 	dsTx.Tables(0).Rows(0).Item("BSL08") 
			BLM08.Text = 	dsTx.Tables(0).Rows(0).Item("BLM08") 
			BGS08.Text = 	dsTx.Tables(0).Rows(0).Item("BGS08") 
			
			BLX09.Text = 	dsTx.Tables(0).Rows(0).Item("BLX09") 
			BKKK09.Text = 	dsTx.Tables(0).Rows(0).Item("BKKK09") 
			BSL09.Text = 	dsTx.Tables(0).Rows(0).Item("BSL09") 
			BLM09.Text = 	dsTx.Tables(0).Rows(0).Item("BLM09") 
			BGS09.Text = 	dsTx.Tables(0).Rows(0).Item("BGS09") 
			
			BLX10.Text = 	dsTx.Tables(0).Rows(0).Item("BLX10") 
			BKKK10.Text = 	dsTx.Tables(0).Rows(0).Item("BKKK10") 
			BSL10.Text = 	dsTx.Tables(0).Rows(0).Item("BSL10") 
			BLM10.Text = 	dsTx.Tables(0).Rows(0).Item("BLM10") 
			BGS10.Text = 	dsTx.Tables(0).Rows(0).Item("BGS10") 
			
			BLX11.Text = 	dsTx.Tables(0).Rows(0).Item("BLX11") 
			BKKK11.Text = 	dsTx.Tables(0).Rows(0).Item("BKKK11") 
			BSL11.Text = 	dsTx.Tables(0).Rows(0).Item("BSL11") 
			BLM11.Text = 	dsTx.Tables(0).Rows(0).Item("BLM11") 
			BGS11.Text = 	dsTx.Tables(0).Rows(0).Item("BGS11") 
			
			BLX12.Text = 	dsTx.Tables(0).Rows(0).Item("BLX12") 
			BKKK12.Text = 	dsTx.Tables(0).Rows(0).Item("BKKK12") 
			BSL12.Text = 	dsTx.Tables(0).Rows(0).Item("BSL12") 
			BLM12.Text = 	dsTx.Tables(0).Rows(0).Item("BLM12") 
			BGS12.Text = 	dsTx.Tables(0).Rows(0).Item("BGS12") 

			
            lblCreateDate.Text = Trim(objGlobal.GetLongDate(dsTx.Tables(0).Rows(0).Item("CreateDate")))
            lblUpdateBy.Text = Trim(dsTx.Tables(0).Rows(0).Item("UpdateId"))
           
		   hidVehCode.Value = Trim(dsTx.Tables(0).Rows(0).Item("CodeSubBlok"))
        End If
    End Sub


	 Sub BindGroupCoa(ByVal pv_strGroupCoa As String)
        Dim strOpCd As String = "GL_CLSSETUP_BLOCK_LIST_GET"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer = 0

        strParamName = "STRSEARCH"
        strParamValue = "and blk.LocCode = '" & strlocation & "' and blk.Status = '1'  order by blk.BlkCode asc" 

		
        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objAccDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=FA_TRX_ASSETADD_ACCCODE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objAccDs.Tables(0).Rows.Count - 1 
		    objAccDs.Tables(0).Rows(intCnt).Item("Description") = trim(objAccDs.Tables(0).Rows(intCnt).Item("BlkCode")) & " (" & objAccDs.Tables(0).Rows(intCnt).Item("Description").Trim() & ")"
            If trim(objAccDs.Tables(0).Rows(intCnt).Item("BlkCode")) = Trim(pv_strGroupCoa) Then
                intSelectedIndex = intCnt + 1
            End If
        Next
		
		
        dr = objAccDs.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("Description") = "Pilih Tahun Tanam"
        objAccDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlGroupCOA.DataSource = objAccDs.Tables(0)
        ddlGroupCOA.DataValueField = "BlkCode"
        ddlGroupCOA.DataTextField = "Description"
        ddlGroupCOA.DataBind()
        ddlGroupCOA.SelectedIndex = intSelectedIndex
    End Sub

	Sub BindSubBlok(ByVal pv_strGroupCoa As String,ByVal slc As String)
        Dim strOpCd As String = "GL_CLSSETUP_SUBBLOCK_LIST_GET"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer = 0

        strParamName = "STRSEARCH"
        strParamValue = "and sub.BlkCode = '" & pv_strGroupCoa  & "' and sub.LocCode = '" & strlocation & "' and sub.Status = '1'  order by sub.SubBlkCode asc" 

		
        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objAccDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=FA_TRX_ASSETADD_ACCCODE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objAccDs.Tables(0).Rows.Count - 1 
		    objAccDs.Tables(0).Rows(intCnt).Item("Description") = trim(objAccDs.Tables(0).Rows(intCnt).Item("BlkCode")) & " (" & objAccDs.Tables(0).Rows(intCnt).Item("Description").Trim() & ")"
			If trim(objAccDs.Tables(0).Rows(intCnt).Item("BlkCode")) = Trim(slc) Then
                intSelectedIndex = intCnt + 1
            End If
        Next
		
		
        dr = objAccDs.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("Description") = "Pilih Blok"
        objAccDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlSubBlok.DataSource = objAccDs.Tables(0)
        ddlSubBlok.DataValueField = "BlkCode"
        ddlSubBlok.DataTextField = "Description"
        ddlSubBlok.DataBind()
        ddlSubBlok.SelectedIndex = intSelectedIndex
    End Sub

	Protected Sub ddlGroupCOA_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        BindSubBlok(ddlGroupCOA.SelectedItem.Value.Trim(),"")
    End Sub
	
    Sub btnSave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)

        Dim strOpCode As String = "GL_CLSTRX_BUDGET_PROD_KARET_SAVE"
        Dim intErrNo As Integer
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim objRslSet As New DataSet()


       If ddlGroupCOA.SelectedValue = "" or ddlSubBlok.Text = "" Then
	        lblErrVehicle.text = "Silakan isi Tahun Tanam dan Blok"
            lblErrVehicle.Visible = True
            Exit Sub
        End If

        



        strParamName = 	"LOC|AY|TT|BLOK|"& _
						"BLX01|BKKK01|BSL01|BLM01|BGS01|" & _
						"BLX02|BKKK02|BSL02|BLM02|BGS02|" & _
						"BLX03|BKKK03|BSL03|BLM03|BGS03|" & _
						"BLX04|BKKK04|BSL04|BLM04|BGS04|" & _
						"BLX05|BKKK05|BSL05|BLM05|BGS05|" & _
						"BLX06|BKKK06|BSL06|BLM06|BGS06|" & _
						"BLX07|BKKK07|BSL07|BLM07|BGS07|" & _
						"BLX08|BKKK08|BSL08|BLM08|BGS08|" & _
						"BLX09|BKKK09|BSL09|BLM09|BGS09|" & _
						"BLX10|BKKK10|BSL10|BLM10|BGS10|" & _
						"BLX11|BKKK11|BSL11|BLM11|BGS11|" & _
						"BLX12|BKKK12|BSL12|BLM12|BGS12|UI"


        strParamValue = strLocation & "|" & txtYearBudget.Text & "|" & ddlGroupCOA.SelectedItem.Value.Trim() & "|" & ddlSubBlok.SelectedItem.Value.Trim() & "|" & _
                        BLX01.Text & "|" &  BKKK01.Text & "|" &  BSL01.Text & "|" &  BLM01.Text & "|" &  BGS01.Text & "|" & _ 
						BLX02.Text & "|" &  BKKK02.Text & "|" &  BSL02.Text & "|" &  BLM02.Text & "|" &  BGS02.Text & "|" & _  
						BLX03.Text & "|" &  BKKK03.Text & "|" &  BSL03.Text & "|" &  BLM03.Text & "|" &  BGS03.Text & "|" & _ 
						BLX04.Text & "|" &  BKKK04.Text & "|" &  BSL04.Text & "|" &  BLM04.Text & "|" &  BGS04.Text & "|" & _  
						BLX05.Text & "|" &  BKKK05.Text & "|" &  BSL05.Text & "|" &  BLM05.Text & "|" &  BGS05.Text & "|" & _  
						BLX06.Text & "|" &  BKKK06.Text & "|" &  BSL06.Text & "|" &  BLM06.Text & "|" &  BGS06.Text & "|" & _  
						BLX07.Text & "|" &  BKKK07.Text & "|" &  BSL07.Text & "|" &  BLM07.Text & "|" &  BGS07.Text & "|" & _  
						BLX08.Text & "|" &  BKKK08.Text & "|" &  BSL08.Text & "|" &  BLM08.Text & "|" &  BGS08.Text & "|" & _  
						BLX09.Text & "|" &  BKKK09.Text & "|" &  BSL09.Text & "|" &  BLM09.Text & "|" &  BGS09.Text & "|" & _  
						BLX10.Text & "|" &  BKKK10.Text & "|" &  BSL10.Text & "|" &  BLM10.Text & "|" &  BGS10.Text & "|" & _  
						BLX11.Text & "|" &  BKKK11.Text & "|" &  BSL11.Text & "|" &  BLM11.Text & "|" &  BGS11.Text & "|" & _  
						BLX12.Text & "|" &  BKKK12.Text & "|" &  BSL12.Text & "|" &  BLM12.Text & "|" &  BGS12.Text & "|" & _  
						strUserId

        Try


            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCode, _
                                                   strParamName, _
                                                   strParamValue)



        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_BUDGET_SAVE&errmesg=" & lblErrMessage.Text & "&redirect=gl/trx/GL_trx_BudgetVeh_list")

        Finally

            txtYearBudget.Enabled = False
            ddlGroupCOA.Enabled = False
			ddlSubBlok.Enabled = False
            DisplayData(txtYearBudget.Text)

        End Try


    End Sub


    
    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)

        Response.Redirect("GL_trx_BudgetProd_Karet_Estate_list.aspx")

    End Sub

End Class
