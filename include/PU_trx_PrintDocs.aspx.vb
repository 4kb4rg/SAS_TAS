Imports System
Imports System.Data
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic


Public Class PU_trx_PrintDocs : Inherits Page

    Protected WithEvents txtFromId As TextBox
    Protected WithEvents txtToId As TextBox
    Protected WithEvents ddlPOType As DropDownList
    Protected WithEvents ddlGRNType As DropDownList
    Protected WithEvents ddlDAType As DropDownList
    Protected WithEvents TrPOType As HTMLTableRow
    Protected WithEvents TrGRNType As HTMLTableRow
    Protected WithEvents TrDAType As HTMLTableRow
    Protected WithEvents TrPOPIC1 As HTMLTableRow
    Protected WithEvents TrPOJbt1 As HTMLTableRow
    Protected WithEvents TrPOPIC2 As HTMLTableRow
    Protected WithEvents TrPOJbt2 As HTMLTableRow
    Protected WithEvents TrPOCat As HTMLTableRow
    Protected WithEvents txtPIC1 As TextBox
    Protected WithEvents txtJabatan1 As TextBox
    Protected WithEvents txtPIC2 As TextBox
    Protected WithEvents txtJabatan2 As TextBox
    Protected WithEvents txtCatatan As TextBox
    Protected WithEvents ibConfirm As ImageButton
    Protected WithEvents lblDocName As Label
    Protected WithEvents lblPO As Label
    Protected WithEvents lblGRN As Label
    Protected WithEvents lblDispAdv As Label
    Protected WithEvents lblRPH As Label
    Protected WithEvents TrRPHType As HTMLTableRow
    Protected WithEvents ddlRPHType As DropDownList
    Protected WithEvents lblErrCode As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents TrPOLok As HtmlTableRow
    Protected WithEvents txtLokasi As TextBox
    Protected WithEvents TxtSyaratBayar As TextBox
    Protected WithEvents lblErrCodeFrom As Label
    Protected WithEvents ddlSentLoc As DropDownList
    Protected WithEvents TrPOLokDet As HtmlTableRow
    Protected WithEvents TrPOPeriod As HtmlTableRow
    Protected WithEvents txtSentPeriod As TextBox

    Protected objAdmPU As New agri.Admin.clsLoc()
    Protected objPU As New agri.PU.clsTrx()

    Dim objPUTrx As New agri.PU.clsTrx()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objGLtrx As New agri.GL.ClsTrx()

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCapDs As New Dataset()
    Dim objRptDs As New DataSet()

    Const PO = 1    
    Const GRN = 2   
    Const DA = 3    
    Const RPH = 4   

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim intConfig As integer
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intDocType As Integer
    Dim strTrxID As String

    Dim strAccTag As String
    Dim strBlkTag As String
    Dim strVehTag As String
    Dim strVehExpCodeTag As String
    Dim strLangCode As String
    Dim strLocType as String
    Dim strLocLevel As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        intConfig = Session("SS_CONFIGSETTING")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PUACCMONTH")
        strAccYear = Session("SS_PUACCYEAR")
        strLocType = Session("SS_LOCTYPE")
        strLocLevel = Session("SS_LOCLEVEL")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        End If

        If Request.QueryString("doctype") = "" Then
            intDocType = 0
        Else
            intDocType = Request.QueryString("doctype")
        End If

        If Request.QueryString("TrxID") = "" Then
            'strTrxID = ""
            'txtFromId.Text = strTrxID
            'txtToId.Text = strTrxID
        Else
            strTrxID = Request.QueryString("TrxID")
            txtFromId.Text = strTrxID
            txtToId.Text = strTrxID
        End If

        lblErrCode.Visible = False
        lblErrCodeFrom.Visible = False


        If Not IsPostBack Then
            PICSetting()
            Select Case CInt(intDocType)
                Case PO
                    lblDocName.Text = lblPO.Text
                    BindPOType()
                    TrPOType.Visible = True
                    TrPOPIC1.Visible = True
                    TrPOJbt1.Visible = True
                    TrPOPIC2.Visible = True
                    TrPOJbt2.Visible = True
                    TrPOCat.Visible = True
                    TrPOLok.Visible = True
                    TrPOLokDet.Visible = True
                    TrPOPeriod.Visible = True
                    BindSentLoc()
                    BindLoc()

                Case GRN
                    lblDocName.Text = lblGRN.Text
                    BindGRNType()
                    TrGRNType.Visible = True
                Case DA
                    lblDocName.Text = lblDispAdv.Text
                    BindDAType()
                    TrDAType.Visible = True
                Case RPH
                    lblDocName.Text = lblRPH.Text
                    BindRPHType()
                    TrRPHType.Visible = True
            End Select
        End If
        'If Page.IsPostBack Then
        'Select Case CInt(intDocType)
        '    Case PO
        '        onsubmit_printPO()
        '    Case GRN
        '        onsubmit_printGRN()
        '    Case DA
        '        onsubmit_printDA()
        '    Case RPH
        '        onsubmit_printRPH()
        'End Select
        'Else
        '    Select Case CInt(intDocType)
        '        Case PO
        '            lblDocName.Text = lblPO.Text
        '            BindPOType()
        '            TrPOType.Visible = True
        '            TrPOPIC1.Visible = True
        '            TrPOJbt1.Visible = True
        '            TrPOPIC2.Visible = True
        '            TrPOJbt2.Visible = True
        '            TrPOCat.Visible = True
        '            TrPOLok.Visible = True
        '            BindSentLoc()
        '        Case GRN
        '            lblDocName.Text = lblGRN.Text
        '            BindGRNType()
        '            TrGRNType.Visible = True
        '        Case DA
        '            lblDocName.Text = lblDispAdv.Text
        '            BindDAType()
        '            TrDAType.Visible = True
        '        Case RPH
        '            lblDocName.Text = lblRPH.Text
        '            BindRPHType()
        '            TrRPHType.Visible = True
        '    End Select
        'End If
    End Sub

    
    Sub BindPOType()
        ddlPOType.Items.Add(New ListItem("All", ""))
        ddlPOType.Items.Add(New ListItem("Stock / Workshop", objPUTrx.EnumPOType.Stock)) 
        ddlPOType.Items.Add(New ListItem(objPUTrx.mtdGetPOType(objPUTrx.EnumPOType.DirectCharge), objPUTrx.EnumPOType.DirectCharge))
        ddlPOType.Items.Add(New ListItem(objPUTrx.mtdGetPOType(objPUTrx.EnumPOType.Canteen), objPUTrx.EnumPOType.Canteen))
        ddlPOType.Items.Add(New ListItem(objPUTrx.mtdGetPOType(objPUTrx.EnumPOType.FixedAsset), objPUTrx.EnumPOType.FixedAsset))
    End Sub

    Sub BindGRNType()
        ddlGRNType.Items.Add(New ListItem("All", ""))
        ddlGRNType.Items.Add(New ListItem("Stock / Workshop", objPUTrx.EnumGRNType.Stock)) 
        ddlGRNType.Items.Add(New ListItem(objPUTrx.mtdGetGRNType(objPUTrx.EnumGRNType.DirectCharge), objPUTrx.EnumGRNType.DirectCharge))
        ddlGRNType.Items.Add(New ListItem(objPUTrx.mtdGetGRNType(objPUTrx.EnumGRNType.Canteen), objPUTrx.EnumGRNType.Canteen))
        ddlGRNType.Items.Add(New ListItem(objPUTrx.mtdGetGRNType(objPUTrx.EnumGRNType.FixedAsset), objPUTrx.EnumGRNType.FixedAsset))
    End Sub

    Sub BindDAType()
        ddlDAType.Items.Add(New ListItem("All", ""))
        ddlDAType.Items.Add(New ListItem("Stock / Workshop", objPUTrx.EnumDAType.Stock)) 
        ddlDAType.Items.Add(New ListItem(objPUTrx.mtdGetDAType(objPUTrx.EnumDAType.DirectCharge), objPUTrx.EnumDAType.DirectCharge))
        ddlDAType.Items.Add(New ListItem(objPUTrx.mtdGetDAType(objPUTrx.EnumDAType.Canteen), objPUTrx.EnumDAType.Canteen))
        ddlDAType.Items.Add(New ListItem(objPUTrx.mtdGetDAType(objPUTrx.EnumDAType.FixedAsset), objPUTrx.EnumDAType.FixedAsset))
    End Sub

    Sub BindRPHType()
        ddlRPHType.Items.Add(New ListItem("All", ""))
        ddlRPHType.Items.Add(New ListItem("Stock / Workshop", objPUTrx.EnumRPHType.Stock)) 
        ddlRPHType.Items.Add(New ListItem(objPUTrx.mtdGetRPHType(objPUTrx.EnumRPHType.DirectCharge), objPUTrx.EnumRPHType.DirectCharge))
        ddlRPHType.Items.Add(New ListItem(objPUTrx.mtdGetRPHType(objPUTrx.EnumRPHType.FixedAsset), objPUTrx.EnumRPHType.FixedAsset))
        ddlRPHType.Items.Add(New ListItem(objPUTrx.mtdGetRPHType(objPUTrx.EnumRPHType.Nursery), objPUTrx.EnumRPHType.Nursery))
    End Sub

    Sub BindLoc()
        Dim strOpCd As String = "ADMIN_CLSLOC_LOCATION_LIST_GET"
        Dim objLocDs As New Object()
        Dim strPRRefLocCode As String
        Dim strParam As String = ""
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer


        strParam = strPRRefLocCode & "|" & objAdmPU.EnumLocStatus.Active & "|LocCode|"

        Try
            intErrNo = objPU.mtdGetLoc(strOpCd, strParam, objLocDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LOC&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_polist.aspx")
        End Try

        For intCnt = 0 To objLocDs.Tables(0).Rows.Count - 1
            objLocDs.Tables(0).Rows(intCnt).Item("LocCode") = objLocDs.Tables(0).Rows(intCnt).Item("LocCode").Trim()
            objLocDs.Tables(0).Rows(intCnt).Item("Description") = objLocDs.Tables(0).Rows(intCnt).Item("LocCode") & " (" & objLocDs.Tables(0).Rows(intCnt).Item("Description").Trim() & ")"

            'If objLocDs.Tables(0).Rows(intCnt).Item("LocCode") = strSelectedPRRefLocCode Then
            '    intSelectedIndex = intCnt + 1
            'End If
        Next intCnt

        dr = objLocDs.Tables(0).NewRow()
        dr("LocCode") = ""
        'dr("Description") = lblSelectListLoc.Text & lblLocation.Text
        objLocDs.Tables(0).Rows.InsertAt(dr, 0)

        'ddlSentLoc.DataSource = objLocDs.Tables(0)
        'ddlSentLoc.DataValueField = "LocCode"
        'ddlSentLoc.DataTextField = "Description"
        'ddlSentLoc.DataBind()
        ''ddlSentLoc.SelectedIndex = intSelectedIndex
 
    End Sub

    Sub onsubmit_printPO()
        Dim strOpCd_Get As String = "PU_CLSTRX_PO_GETRANGE" & "|" & "PurchaseOrder"
        Dim strOpCodePrint As String = "ADMIN_SHARE_UPD_PRINTDATE"
        Dim strTable As String = "PU_PO"
        Dim strUpdString As String = ""
        Dim strParam As String
        Dim SearchStr As String = ""
        Dim SortStr As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intStatus As Integer
        Dim strPrintDate As String
        Dim strPOId As String
        Dim strIDRange As String = ""
        Dim strReprintedID As String = ""
        Dim strSortLine As String

        Dim strFromID As String
        Dim strToID As String
        Dim strPOType As String
        Dim strStatus As String

        strFromID = txtFromId.Text.Trim

        'If strFromID = "" Then
        '    lblErrCodeFrom.Visible = True
        '    Exit Sub
        'End If

        strToID = txtToId.Text.Trim
        strPOType = Trim(ddlPOType.SelectedItem.Value)
        strStatus = CStr(objPUTrx.EnumPOStatus.Active) & "','" & CStr(objPUTrx.EnumPOStatus.Confirmed)

        If strPOType = "" Then
            strPOType = objPUTrx.EnumPOType.Stock & "','" & _
                        objPUTrx.EnumPOType.DirectCharge & "','" & _
                        objPUTrx.EnumPOType.Canteen & "','" & _
                        objPUTrx.EnumPOType.FixedAsset
        End If

        SearchStr = "and POID >= '" & strFromID & "' " & _
                    "and POID <= '" & strToID & "' " & _
                    "and POType in ('" & strPOType & "') " & _
                    "and Status in ('" & strStatus & "') "

        SortStr = "order by POID"

        strParam = "|" & SearchStr & "|" & SortStr

        Try
            intErrNo = objPUTrx.mtdGetPODocRpt(strOpCd_Get, _
                                               strParam, _
                                               strCompany, _
                                               strLocation, _
                                               strUserId, _
                                               strAccMonth, _
                                               strAccYear, _
                                               objRptDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_PRINTDOCS_GETPORANGE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objRptDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objRptDs.Tables(0).Rows.Count - 1

                intStatus = CInt(Trim(objRptDs.Tables(0).Rows(intCnt).Item("Status")))
                strPrintDate = objGlobal.GetLongDate(objRptDs.Tables(0).Rows(intCnt).Item("PrintDate"))
                strPOId = Trim(objRptDs.Tables(0).Rows(intCnt).Item("POID"))
                strIDRange = strIDRange & strPOId & "','"

                strUpdString = "where POID = '" & strPOId & "' " & _
                               "and LocCode = '" & strLocation & "' "

                If intStatus = objPUTrx.EnumPOStatus.Confirmed Then
                    If strPrintDate = "" Then
                        Try
                            intErrNo = objAdmin.mtdUpdPrintDate(strOpCodePrint, _
                                                                strUpdString, _
                                                                strTable, _
                                                                strCompany, _
                                                                strLocation, _
                                                                strUserId)
                        Catch Exp As System.Exception
                            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_PRINTDOCS_UPDPRINTDATE_PO&errmesg=" & lblErrMessage.Text & "&redirect=")
                        End Try
                    Else
                        strReprintedID = strReprintedID & strPOId & "|"
                    End If
                End If
            Next
            strIDRange = Left(strIDRange, Len(strIDRange) - 3)
        Else
            lblErrCode.Visible = True
            Exit Sub
        End If

        onload_GetLangCap()

        If txtCatatan.Text.Trim <> "" Then
            Dim strParamName As String = ""
            Dim strParamValue As String = ""
            Dim strOpCode_POLnUpd As String = "PU_CLSTRX_PO_LINE_UPD"
            strParamName = "STRUPDATE"
            strParamValue = "SET CATATAN = '" & txtCatatan.Text.Trim & "' WHERE POID = '" & strFromID & "' "

            Try
                intErrNo = objGLtrx.mtdInsertDataCommon(strOpCode_POLnUpd, _
                                                        strParamName, _
                                                        strParamValue)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_POLN&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_PODet.aspx")
            End Try
        End If

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/PU_Rpt_POPFDet.aspx?Type=Print&CompName=" & strCompany & _
                       "&Location=" & strLocation & _
                       "&RptId=" & "" & _
                       "&RptName=" & "" & _
                       "&Decimal=" & 0 & _
                       "&PONoFrom=" & strFromID & _
                       "&PONoTo=" & "" & _
                       "&PeriodeFrom=" & "" & _
                       "&PeriodeTo=" & "" & _
                       "&PIC1=" & Server.UrlEncode(Trim(txtPIC1.Text)) & _
                       "&Jabatan1=" & Server.UrlEncode(Trim(txtJabatan1.Text)) & _
                       "&PIC2=" & Server.UrlEncode(Trim(txtPIC2.Text)) & _
                       "&Jabatan2=" & Server.UrlEncode(Trim(txtJabatan2.Text)) & _
                       "&Catatan=" & Server.UrlEncode(Trim(txtCatatan.Text)) & _
                       "&SyaratBayar=" & Server.UrlEncode(TxtSyaratBayar.Text) & _
                       "&Lokasi=" & Server.UrlEncode(txtLokasi.Text) & _
                       "&SentPeriod=" & Server.UrlEncode(txtSentPeriod.Text) & _
                       """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

        'Response.Write("<Script Language=""JavaScript"">window.open(""../reports/PU_Rpt_PODet.aspx?strPOId=" & strIDRange & _
        '               "&strStatus=" & strStatus & _
        '               "&strDocType=" & strPOType & _
        '               "&AccountTag=" & strAccTag & _
        '               "&BlockTag=" & strBlkTag & _
        '               "&VehicleTag=" & strVehTag & _
        '               "&VehExpenseTag=" & strVehExpCodeTag & _
        '               "&batchPrint=yes" & _
        '               "&reprintId=" & strReprintedID & _
        '               "&PIC1=" & Server.UrlEncode(Trim(txtPIC1.Text)) & _
        '               "&Jabatan1=" & Server.UrlEncode(Trim(txtJabatan1.Text)) & _
        '               "&PIC2=" & Server.UrlEncode(Trim(txtPIC2.Text)) & _
        '               "&Jabatan2=" & Server.UrlEncode(Trim(txtJabatan2.Text)) & _
        '               "&Catatan=" & Server.UrlEncode(Trim(txtCatatan.Text)) & _
        '               "&Lokasi=" & Server.UrlEncode(Trim(txtLokasi.Text)) & _
        '               """, null,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

    End Sub


    Sub onsubmit_printGRN()
        Dim strOpCd_Get As String = "PU_CLSTRX_GRN_GETRANGE" & "|" & "GoodsReturn"
        Dim strOpCodePrint As String = "ADMIN_SHARE_UPD_PRINTDATE"
        Dim strTable As String = "PU_GOODSRET"
        Dim strUpdString As String = ""
        Dim strParam As String
        Dim SearchStr As String = ""
        Dim SortStr As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intStatus As Integer
        Dim strPrintDate As String
        Dim strGRNId As String
        Dim strIDRange As String = ""
        Dim strReprintedID As String = ""
        Dim strSortLine As String

        Dim strFromID As String
        Dim strToID As String
        Dim strGRNType As String
        Dim strStatus As String

        strFromID = txtFromId.Text.Trim
        strToID = txtToId.Text.Trim
        strGRNType = Trim(ddlGRNType.SelectedItem.value)
        strStatus = CStr(objPUTrx.EnumPOStatus.Confirmed)

        If strGRNType = "" Then
            strGRNType = objPUTrx.EnumGRNType.Stock & "','" & _
                         objPUTrx.EnumGRNType.DirectCharge & "','" & _
                         objPUTrx.EnumGRNType.Canteen & "','" & _
                         objPUTrx.EnumGRNType.FixedAsset
        End If

        SearchStr = "and GoodsRetId >= '" & strFromID & "' " & _
                    "and GoodsRetId <= '" & strToID & "' " & _
                    "and GoodsRetType in ('" & strGRNType & "') " & _
                    "and Status in ('" & strStatus & "') "

        SortStr = "order by GoodsRetId"

        strParam = "|" & SearchStr & "|" & SortStr

        Try
            intErrNo = objPUTrx.mtdGetGRNDocRpt(strOpCd_Get, _
                                                strParam, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strAccMonth, _
                                                strAccYear, _
                                                objRptDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_PRINTDOCS_GETGRNRANGE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objRptDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objRptDs.Tables(0).Rows.Count - 1

                intStatus = CInt(Trim(objRptDs.Tables(0).Rows(intCnt).Item("Status")))
                strPrintDate = objGlobal.GetLongDate(objRptDs.Tables(0).Rows(intCnt).Item("PrintDate"))
                strGRNId = Trim(objRptDs.Tables(0).Rows(intCnt).Item("GoodsRetId"))
                strIDRange = strIDRange & strGRNId & "','"

                strUpdString = "where GoodsRetId = '" & strGRNId & "' " & _
                               "and LocCode = '" & strLocation & "' "

                If intStatus = objPUTrx.EnumGRNStatus.Confirmed Then
                    If strPrintDate = "" Then
                        Try
                            intErrNo = objAdmin.mtdUpdPrintDate(strOpCodePrint, _
                                                                strUpdString, _
                                                                strTable, _
                                                                strCompany, _
                                                                strLocation, _
                                                                strUserId)
                        Catch Exp As System.Exception
                            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_PRINTDOCS_UPDPRINTDATE_GRN&errmesg=" & lblErrMessage.Text & "&redirect=")
                        End Try
                    Else
                        strReprintedID = strReprintedID & strGRNId & "|"
                    End If
                End If
            Next
            strIDRange = Left(strIDRange, Len(strIDRange) - 3)
        Else
            lblErrCode.Visible = True
            Exit Sub
        End If

        onload_GetLangCap()

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/PU_Rpt_GRNDet.aspx?strGoodsRetId=" & strIDRange & _
                       "&strStatus=" & strStatus & _
                       "&strDocType=" & strGRNType & _
                       "&AccountTag=" & strAccTag & _
                       "&BlockTag=" & strBlkTag & _
                       "&VehicleTag=" & strVehTag & _
                       "&VehExpenseTag=" & strVehExpCodeTag & _
                       "&batchPrint=yes" & _
                       "&reprintId=" & strReprintedID & _
                       """, null,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");window.close();</Script>")

    End Sub


    Sub onsubmit_printDA()
        Dim strOpCd_Get As String = "PU_CLSTRX_DISPADV_GETRANGE" & "|" & "DispAdv"
        Dim strOpCodePrint As String = "ADMIN_SHARE_UPD_PRINTDATE"
        Dim strTable As String = "PU_DISPADV"
        Dim strUpdString As String = ""
        Dim strParam As String
        Dim SearchStr As String = ""
        Dim SortStr As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intStatus As Integer
        Dim strPrintDate As String
        Dim strDispAdvId As String
        Dim strIDRange As String = ""
        Dim strReprintedID As String = ""
        Dim strSortLine As String

        Dim strFromID As String
        Dim strToID As String
        Dim strDAType As String
        Dim strStatus As String

        strFromID = txtFromId.Text.Trim
        strToID = txtToId.Text.Trim
        strDAType = Trim(ddlDAType.SelectedItem.value)
        strStatus = CStr(objPUTrx.EnumDAStatus.Confirmed)

        If strDAType = "" Then
            strDAType = objPUTrx.EnumDAType.Stock & "','" & _
                         objPUTrx.EnumDAType.DirectCharge & "','" & _
                         objPUTrx.EnumDAType.Canteen & "','" & _
                         objPUTrx.EnumDAType.FixedAsset
        End If

        SearchStr = "and DispAdvID >= '" & strFromID & "' " & _
                    "and DispAdvID <= '" & strToID & "' " & _
                    "and DispAdvType in ('" & strDAType & "') " & _
                    "and Status in ('" & strStatus & "') "

        SortStr = "order by DispAdvID"

        strParam = "|" & SearchStr & "|" & SortStr

        Try
            intErrNo = objPUTrx.mtdGetDADocRpt(strOpCd_Get, _
                                               strParam, _
                                               strCompany, _
                                               strLocation, _
                                               strUserId, _
                                               strAccMonth, _
                                               strAccYear, _
                                               objRptDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_PRINTDOCS_GETDARANGE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objRptDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objRptDs.Tables(0).Rows.Count - 1

                intStatus = CInt(Trim(objRptDs.Tables(0).Rows(intCnt).Item("Status")))
                strPrintDate = objGlobal.GetLongDate(objRptDs.Tables(0).Rows(intCnt).Item("PrintDate"))
                strDispAdvId = Trim(objRptDs.Tables(0).Rows(intCnt).Item("DispAdvID"))
                strIDRange = strIDRange & strDispAdvId & "','"

                strUpdString = "where DispAdvID = '" & strDispAdvId & "' " & _
                               "and LocCode = '" & strLocation & "' "

                If intStatus = objPUTrx.EnumDAStatus.Confirmed Then
                    If strPrintDate = "" Then
                        Try
                            intErrNo = objAdmin.mtdUpdPrintDate(strOpCodePrint, _
                                                                strUpdString, _
                                                                strTable, _
                                                                strCompany, _
                                                                strLocation, _
                                                                strUserId)
                        Catch Exp As System.Exception
                            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_PRINTDOCS_UPDPRINTDATE_DA&errmesg=" & lblErrMessage.Text & "&redirect=")
                        End Try
                    Else
                        strReprintedID = strReprintedID & strDispAdvId & "|"
                    End If
                End If
            Next
            strIDRange = Left(strIDRange, Len(strIDRange) - 3)
        Else
            lblErrCode.Visible = True
            Exit Sub
        End If

        onload_GetLangCap()

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/PU_Rpt_DADet.aspx?strDispAdvId=" & strIDRange & _
                       "&strStatus=" & strStatus & _
                       "&strDocType=" & strDAType & _
                       "&AccountTag=" & strAccTag & _
                       "&BlockTag=" & strBlkTag & _
                       "&VehicleTag=" & strVehTag & _
                       "&VehExpenseTag=" & strVehExpCodeTag & _
                       "&batchPrint=yes" & _
                       "&reprintId=" & strReprintedID & _
                       """, null,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");window.close();</Script>")

    End Sub

    Sub onsubmit_printRPH()
        Dim strOpCd_Get As String = "PU_CLSTRX_RPH_GETRANGE" & "|" & "PurchaseOrder"
        Dim strOpCodePrint As String = "ADMIN_SHARE_UPD_PRINTDATE"
        Dim strTable As String = "PU_RPH"
        Dim strUpdString As String = ""
        Dim strParam As String
        Dim SearchStr As String = ""
        Dim SortStr As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intStatus As Integer
        Dim strPrintDate As String
        Dim strRPHId As String
        Dim strIDRange As String = ""
        Dim strReprintedID As String = ""
        Dim strSortLine As String

        Dim strFromID As String
        Dim strToID As String
        Dim strRPHType As String
        Dim strStatus As String

        strFromID = txtFromId.Text.Trim
        strToID = txtToId.Text.Trim
        strRPHType = Trim(ddlRPHType.SelectedItem.value)
        strStatus = CStr(objPUTrx.EnumRPHStatus.Confirmed)

        If strRPHType = "" Then
            strRPHType = objPUTrx.EnumPOType.Stock & "','" & _
                        objPUTrx.EnumPOType.DirectCharge & "','" & _
                        objPUTrx.EnumPOType.Nursery & "','" & _
                        objPUTrx.EnumPOType.FixedAsset
        End If

        

        SearchStr = "and RPHID >= '" & strFromID & "' " & _
                    "and RPHID <= '" & strToID & "' " & _
                    "and RPHType in ('" & strRPHType & "') "
                                
        SortStr = "order by RPHID"

        strParam = "|" & SearchStr & "|" & SortStr & "|" & strAccYear

        Try
            intErrNo = objPUTrx.mtdGetRPHDocRpt(strOpCd_Get, _
                                               strParam, _
                                               strCompany, _
                                               strLocation, _
                                               strUserId, _
                                               strAccMonth, _
                                               strAccYear, _
                                               objRptDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_PRINTDOCS_GETRPHRANGE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objRptDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objRptDs.Tables(0).Rows.Count - 1

                intStatus = CInt(Trim(objRptDs.Tables(0).Rows(intCnt).Item("Status")))
                strPrintDate = objGlobal.GetLongDate(objRptDs.Tables(0).Rows(intCnt).Item("PrintDate"))
                strRPHId = Trim(objRptDs.Tables(0).Rows(intCnt).Item("RPHID"))
                strIDRange = strIDRange & strRPHId & "','"

                strUpdString = "where RPHID = '" & strRPHId & "' " & _
                               "and LocCode = '" & strLocation & "' "

                If intStatus = objPUTrx.EnumRPHStatus.Confirmed Then
                    If strPrintDate = "" Then
                        Try
                            intErrNo = objAdmin.mtdUpdPrintDate(strOpCodePrint, _
                                                                strUpdString, _
                                                                strTable, _
                                                                strCompany, _
                                                                strLocation, _
                                                                strUserId)
                        Catch Exp As System.Exception
                            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_PRINTDOCS_UPDPRINTDATE_PO&errmesg=" & lblErrMessage.Text & "&redirect=")
                        End Try
                    Else
                        strReprintedID = strReprintedID & strRPHId & "|"
                    End If
                End If
            Next
            strIDRange = Left(strIDRange, Len(strIDRange) - 3)
        Else
            lblErrCode.Visible = True
            Exit Sub
        End If

        onload_GetLangCap()

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/PU_Rpt_RPHDet.aspx?strRPHId=" & strIDRange & _
                       "&strStatus=" & strStatus & _
                       "&strDocType=" & strRPHType & _
                       "&AccountTag=" & strAccTag & _
                       "&BlockTag=" & strBlkTag & _
                       "&VehicleTag=" & strVehTag & _
                       "&VehExpenseTag=" & strVehExpCodeTag & _
                       "&batchPrint=yes" & _
                       "&reprintId=" & strReprintedID & _
                       """, null,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");window.close();</Script>")

    End Sub
    Sub close_window()
        Response.Write("<Script Language=""JavaScript"">window.close();</Script>")
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        Try
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfig) = True Then
               strBlkTag = GetCaption(objLangCap.EnumLangCap.Block)
            Else
               strBlkTag = GetCaption(objLangCap.EnumLangCap.SubBlock)
            End If
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BI_TRX_PRINTDOCS_GETLANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        strAccTag = GetCaption(objLangCap.EnumLangCap.Account)
        strVehTag = GetCaption(objLangCap.EnumLangCap.Vehicle)
        strVehExpCodeTag = GetCaption(objLangCap.EnumLangCap.VehExpense)
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_PRINTDOCS_GETENTIRELANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer
        
        For count=0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode"))
                If strLocType = objAdminLoc.EnumLocType.Mill then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                end if
                Exit For
            End If
        Next
    End Function

    Sub btnPreview_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Select Case CInt(intDocType)
            Case PO
                onsubmit_printPO()
            Case GRN
                onsubmit_printGRN()
            Case DA
                onsubmit_printDA()
            Case RPH
                onsubmit_printRPH()
        End Select
    End Sub

    Sub BindSentLoc()
        ddlSentLoc.Items.Add(New ListItem("N/A", ""))
        ddlSentLoc.Items.Add(New ListItem("Lokasi PMKS", "1"))
        'ddlSentLoc.Items.Add(New ListItem("Lokasi Estate", "2"))
        ddlSentLoc.Items.Add(New ListItem("Lokasi HO", "3"))
        'ddlSentLoc.Items.Add(New ListItem("Lokasi PWK", "4"))

        'Dim strOpCd As String = "PU_STDRPT_PURCHASEORDERPF"
        'Dim strParamName As String = ""
        'Dim strParamValue As String = ""
        'Dim intErrNo As Integer
        'Dim objPOLnDs As Object
        'strParamName = "STRSEARCH"
        'strParamValue = "AND PO.POID = '" & txtFromId.Text.Trim & "' AND PLN.Catatan <> ''"
        'Try
        '    intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
        '                                        strParamName, _
        '                                        strParamValue, _
        '                                        objPOLnDs)
        'Catch Exp As System.Exception
        '    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_PO&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_POdet")
        'End Try

        'If objPOLnDs.Tables(0).Rows.Count > 0 Then
        '    txtCatatan.Text = objPOLnDs.Tables(0).Rows(0).Item("Catatan").Trim()
        'End If
    End Sub

    Sub SentLocChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
            Select Case ddlSentLoc.SelectedItem.Value
                Case "1"
                    txtLokasi.Text = "PKS - PT. Mitra Agro Sembada " & vbcrlf & _
                                    "Jalan Raya Puding Besar, Kotawaringin, Labu, Puding Besar " & vbcrlf & _
                                    "Kab Bangka, Kepulauan Bangka Belitung"
        '        Case "2"
        '            txtLokasi.Text = "Kebun Negeri Lama - PT. Cisadane Sawit Raya " & vbCrLf & _
        '                            "Kec. Bilah Hilir, Kab. Labuhan Batu " & vbcrlf & _
        '                            "Sumatera Utara"
                Case "3"
                    txtLokasi.Text = "PT. Mitra Agro Sembada " & vbCrLf & _
                                    "Graha 28 lantai 5, Jl. Yos Sudarso No. 84, Sunter Jaya " & vbCrLf & _
                                    "Tanjung Priok, Jakarta Utara"
        '        Case "4"
        '            txtLokasi.Text = "Kantor Perwakilan Medan " 
                Case Else
                    txtLokasi.Text = ""
            End Select
    End Sub

    Sub PICSetting()
        Dim strOpCode_GetSysLoc As String = "PWSYSTEM_CLSCONFIG_SYSLOC_DETAILS_GET"
        Dim objSysLocDs As New DataSet()
        Dim objAccCfg As New DataSet()
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strLocCode As String

        Try
            strParam = strCompany & "|" & strLocation & "|" & strUserId
            intErrNo = objSysCfg.mtdGetSysLocInfo(strOpCode_GetSysLoc, _
                                                  strCompany, _
                                                  strLocCode, _
                                                  strUserId, _
                                                  objSysLocDs, _
                                                  strParam)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_PO&errmesg=" & Exp.ToString() & "&redirect=system/user/setlocation.aspx")
        End Try

        If objSysLocDs.Tables(0).Rows.Count > 0 Then
            If strLocLevel = objAdminLoc.EnumLocLevel.Estate Then
                txtPIC1.Text = "Hijri Yanda"
                txtJabatan1.Text = "Logistik"
                txtPIC2.Text = objSysLocDs.Tables(0).Rows(0).Item("Manager").Trim()
                txtJabatan2.Text = "-"
            ElseIf strLocLevel = objAdminLoc.EnumLocLevel.Perwakilan Then
                txtPIC1.Text = ""
                txtJabatan1.Text = ""
                txtPIC2.Text = ""
                txtJabatan2.Text = ""
            ElseIf strLocLevel = objAdminLoc.EnumLocLevel.HQ Then
                txtPIC1.Text = ""
                txtJabatan1.Text = ""
                txtPIC2.Text = "Hijri Yanda"
                txtJabatan2.Text = "Logistik"
            End If
        End If
    End Sub
End Class
