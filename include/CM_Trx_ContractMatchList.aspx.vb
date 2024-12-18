
Imports System
Imports System.Data
Imports System.Collections 
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic



Public Class CM_Trx_ContractMatchList : Inherits Page
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objCMTrx As New agri.CM.clsTrx()
    Protected objWMTrx As New agri.WM.clsTrx()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGLtrx As New agri.GL.ClsTrx()
    Dim objOk As New agri.GL.ClsTrx()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intCMAR As Integer

    Dim objMatchDs As New Object()
    Dim objSummary As New Object()
    Dim objOST As New Object()
    Dim objBPDs As New Object()
    Dim objLangCapDs As New Object()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String

    Dim strSelAccMonth As String
    Dim strSelAccYear As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_ARACCMONTH")
        strAccYear = Session("SS_ARACCYEAR")
        intCMAR = Session("SS_CMAR")
        strLocType = Session("SS_LOCTYPE")
        strSelAccMonth = Session("SS_SELACCMONTH")
        strSelAccYear = Session("SS_SELACCYEAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMContractMatching), intCMAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            If SortExpression.Text = "" Then
                SortExpression.Text = "ma.MatchingId"
            End If
            If Not Page.IsPostBack Then
                srchDateOst.SelectedDate = Date.Now
                BindBuyerList()
                BindProductList()

                BindGrid()
                BindPageList()
            End If
        End If
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgLine.CurrentPageIndex = 0
        dgLine.EditItemIndex = -1
        BindGrid()
        BindPageList()
    End Sub

    Sub onChanged_BillParty()
        BindContractNoList(ddlBuyer.SelectedValue, "")
    End Sub


    Sub BindProductList()
        Dim objdsST As New DataSet
        Dim strParamName As String
        Dim strParamValue As String
        Dim sSQLKriteria As String
        Dim intErrNo As Integer

        Dim dr As DataRow

        Dim strOpCd_Get As String = "IN_CLSTRX_GLOBAL_UPD"
        sSQLKriteria = "SELECT ProductCode,ProductName FROM WM_STP_PRODUCT Where Status='1' Order By ProductCode ASC"

        strParamName = "STRSEARCH"
        strParamValue = sSQLKriteria

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd_Get,
                                         strParamName,
                                         strParamValue,
                                         objdsST)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CM_TRX_CONTRACTREGDET_SELLERLIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        dr = objdsST.Tables(0).NewRow()
        dr("ProductCode") = ""
        dr("ProductName") = "Please Select Product"
        objdsST.Tables(0).Rows.InsertAt(dr, 0)

        ddlProduct.DataSource = objdsST.Tables(0)
        ddlProduct.DataValueField = "ProductCode"
        ddlProduct.DataTextField = "ProductName"
        ddlProduct.DataBind()
    End Sub

    Sub BindBuyerList()
        Dim strOpCd_Get As String = "GL_CLSSETUP_BILLPARTY_GET"
        Dim strSrchStatus As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow

        strSrchStatus = objGLSetup.EnumBillPartyStatus.Active

        strParam = "" & "|" & _
                   "" & "|" & _
                   strSrchStatus & "|" & _
                   "" & "|" & _
                   "BP.BillPartyCode" & "|" & _
                   "asc" & "|"

        Try
            intErrNo = objGLSetup.mtdGetBillParty(strOpCd_Get, strParam, objBPDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CM_TRX_CONTRACT_REG_BILLPARTYLIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        'For intCnt = 0 To objBPDs.Tables(0).Rows.Count - 1
        '    objBPDs.Tables(0).Rows(intCnt).Item("BillPartyCode") = Trim(objBPDs.Tables(0).Rows(intCnt).Item("BillPartyCode"))
        '    objBPDs.Tables(0).Rows(intCnt).Item("Name") = objBPDs.Tables(0).Rows(intCnt).Item("BillPartyCode") & " (" & Trim(objBPDs.Tables(0).Rows(intCnt).Item("Name")) & ")"
        'Next

        dr = objBPDs.Tables(0).NewRow()
        'dr("BillPartyCode") = ""
        dr("Name") = "Please Select Billparty"
        objBPDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlBuyer.DataSource = objBPDs.Tables(0)
        ddlBuyer.DataValueField = "BillPartyCode"
        ddlBuyer.DataTextField = "Name"
        ddlBuyer.DataBind()

    End Sub

    Sub BindContractNoList(ByVal pv_strBuyer As String, ByVal pv_strContNo As String)
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer
        Dim objContractDs As New Object()
        Dim strParamName As String
        Dim strParamValue As String
        Dim strOppCode_Get As String = "IN_CLSTRX_GLOBAL_UPD"
        Dim sSQLKriteria As String = ""
        Dim nSimbol As String = "|"

        sSQLKriteria = "Select ContractNo,BuyerNO,(rTrim(ContractNo) + ' - ' +  rTrim(BuyerNO)) As ContractNoDescr FROM CM_Contract Where BuyerCode='" & pv_strBuyer & "' AND Status NOT IN ('2') Order By ContractNO ASC"
        strParamName = "STRSEARCH"
        strParamValue = sSQLKriteria

        Try
            intErrNo = objOk.mtdGetDataCommon(strOppCode_Get, strParamName, strParamValue, objContractDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=wm_ticket&errmesg=" & Exp.Message & "&redirect=")
        End Try

        dr = objContractDs.Tables(0).NewRow()
        'dr("ContractNo") = ""
        dr("ContractNoDescr") = "Please Select Contract No"
        objContractDs.Tables(0).Rows.InsertAt(dr, 0)

        radcmbCtr.DataSource = objContractDs.Tables(0)
        radcmbCtr.DataValueField = "ContractNo"
        radcmbCtr.DataTextField = "ContractNoDescr"
        radcmbCtr.DataBind()
        radcmbCtr.SelectedIndex = intSelectedIndex
    End Sub

    Sub BindGrid()
 
        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet


        dsData = LoadData()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgLine.PageSize)

        dgLine.DataSource = dsData
        dgLine.DataBind()

        BindPageList()
        PageNo = dgLine.CurrentPageIndex + 1
        lblTracker.Text = "Page " & PageNo & " of " & dgLine.PageCount


        dsData = LoadData_Summary()
        dgSummary.DataSource = dsData
        dgSummary.DataBind()


        dsData = LoadData_OutStanding()
        dgOst.DataSource = dsData
        dgOst.DataBind()

    End Sub

    Sub BindPageList()
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgLine.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgLine.CurrentPageIndex
    End Sub

    Protected Function LoadData() As DataSet
        Dim strOppCode_Get As String = "CM_CLSTRX_CONTRACT_MATCH_GET"
        Dim strSearch As String = ""
        Dim strSort As String = ""
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer


        strParamName = "PRD|CUST|CTR|DONO|TRANSPORT|DATEFR|DATETO"
        strParamValue = ddlProduct.SelectedItem.Value & "|" &
                        ddlBuyer.SelectedValue & "|" &
                        radcmbCtr.SelectedValue &
                        "|" & "" &
                        "|" & "" &
                        "|" & "2022-11-01" &
                        "|" & "2022-12-31"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOppCode_Get, strParamName, strParamValue, objMatchDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=wm_ticket&errmesg=" & Exp.Message & "&redirect=")
        End Try

        Return objMatchDs
    End Function

    Protected Function LoadData_Summary() As DataSet
        Dim strOppCode_Get As String = "CM_CLSTRX_CONTRACT_MATCH_SUMMARY_GET"
        Dim strSearch As String = ""
        Dim strSort As String = ""
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer


        strParamName = "PRD|CUST|CTR|DONO|TRANSPORT|DATEFR|DATETO"
        strParamValue = ddlProduct.SelectedItem.Value & "|" &
                        ddlBuyer.SelectedValue & "|" &
                        radcmbCtr.SelectedValue &
                        "|" & "" &
                        "|" & "" &
                        "|" & "2022-11-01" &
                        "|" & "2022-12-31"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOppCode_Get, strParamName, strParamValue, objSummary)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=wm_ticket&errmesg=" & Exp.Message & "&redirect=")
        End Try

        Return objSummary
    End Function

    Protected Function LoadData_OutStanding() As DataSet
        Dim strOppCode_Get As String = "CM_CLSTRX_CONTRACT_MATCH_OUTSTANDINGDO_GET"
        Dim strSearch As String = ""
        Dim strSort As String = ""
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer


        strParamName = "LOC|PRD|CUST|TGL"
        strParamValue = strLocation & "|" &
                        ddlProduct.SelectedItem.Value & "|" &
                        ddlBuyer.SelectedValue & "|" &
                        Format(srchDateOst.SelectedDate, "yyyy-MM-dd")

        Try
            intErrNo = objOk.mtdGetDataCommon(strOppCode_Get, strParamName, strParamValue, objOST)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=wm_ticket&errmesg=" & Exp.Message & "&redirect=")
        End Try

        Return objOST
    End Function

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgLine.CurrentPageIndex = 0
            Case "prev"
                dgLine.CurrentPageIndex = _
                    Math.Max(0, dgLine.CurrentPageIndex - 1)
            Case "next"
                dgLine.CurrentPageIndex = _
                    Math.Min(dgLine.PageCount - 1, dgLine.CurrentPageIndex + 1)
            Case "last"
                dgLine.CurrentPageIndex = dgLine.PageCount - 1
        End Select
        lstDropList.SelectedIndex = dgLine.CurrentPageIndex
        BindGrid()
    End Sub


    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgLine.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgLine.CurrentPageIndex = e.NewPageIndex
        BindGrid()

    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "asc", "desc", "asc")
        dgLine.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub



    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblBillParty.Text = GetCaption(objLangCap.EnumLangCap.BillParty) & lblCode.Text

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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CM_TRX_CONTRACTREG_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=CM/Setup/CM_Trx_ContractRegList.aspx")
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

End Class
