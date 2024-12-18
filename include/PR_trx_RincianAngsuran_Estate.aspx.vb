Imports System
Imports System.Data
Imports System.Collections 
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction

Imports agri.HR
Imports agri.GlobalHdl.clsGlobalHdl

Public Class PR_trx_RincianAngsuran_Estate : Inherits Page

    Protected WithEvents dgEmpList As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList

    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents NewEmpBtn As ImageButton
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblCurrentIndex As Label
    Protected WithEvents lblPageCount As Label

    Protected WithEvents txtEmpCode As TextBox
    Protected WithEvents txtEmpName As TextBox
    Protected WithEvents ddlEmpMonth As DropDownList
    Protected WithEvents ddlyear As DropDownList

    Dim ObjOk As New agri.GL.ClsTrx
    Protected objHR As New agri.HR.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objGLRpt As New agri.GL.clsReport()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intHRAR As Long
    Dim blnCanDelete As Boolean = False
    Dim objEmpDs As New Object()

    Dim objEmpDivDs As New Object()
    Dim cnt As Double

    Public Sub UserMsgBox(ByVal F As Object, ByVal sMsg As String)
        Dim sb As New StringBuilder()
        Dim oFormObject As System.Web.UI.Control = Nothing
        Try
            sMsg = sMsg.Replace("'", "\'")
            sMsg = sMsg.Replace(Chr(34), "\" & Chr(34))
            sMsg = sMsg.Replace(vbCrLf, "\n")
            sMsg = "<script language='javascript'>alert('" & sMsg & "');</script>"
            sb = New StringBuilder()
            sb.Append(sMsg)
            For Each oFormObject In F.Controls
                If TypeOf oFormObject Is HtmlForm Then
                    Exit For
                End If
            Next
            oFormObject.Controls.AddAt(oFormObject.Controls.Count, New LiteralControl(sb.ToString()))
        Catch ex As Exception

        End Try
    End Sub

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intHRAR = Session("SS_PRAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxWorkPerformance), intHRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.Text = "" Then
                SortExpression.Text = "BKMDate,divisi"
            End If

            If Not Page.IsPostBack Then
                BindAccYear(Session("SS_SELACCYEAR"))
                ddlEmpMonth.SelectedIndex = CInt(Session("SS_SELACCMONTH")) - 1


                'If Session("BKM") <> "" Then
                '    Dim prevset As String
                '    Dim ary As Array
                '    prevset = Session("BKM")
                '    ary = Split(prevset, "|")
                '    ddlEmpMonth.SelectedValue = ary(0)
                '    BindAccYear(ary(1))
                '    txtEmpName.Text = ary(2)
                '    ddlEmpDiv.SelectedValue = ary(3)
                '    lblCurrentIndex.Text = ary(6)
                'End If
                BindGrid()
            End If
        End If
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblCurrentIndex.Text = 0
        dgEmpList.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub BindAccYear(ByVal pv_strAccYear As String)
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim intSelectedIndex As Integer
        Dim objAccYearDs As New Object
        Dim strOpCd As String = "ADMIN_CLSACCPERIOD_CONFIG_GET"

        strParamName = "LOCCODE|SEARCHSTR|SORTEXP"
        strParamValue = strLocation & "||Order By HD.AccYear"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objAccYearDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & Exp.Message & "&redirect=")
        End Try

        For intCnt = 0 To objAccYearDs.Tables(0).Rows.Count - 1
            objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear") = Trim(objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear"))
            objAccYearDs.Tables(0).Rows(intCnt).Item("UserName") = Trim(objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear"))
            If objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear") = pv_strAccYear Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        ddlyear.DataSource = objAccYearDs.Tables(0)
        ddlyear.DataValueField = "AccYear"
        ddlyear.DataTextField = "UserName"
        ddlyear.DataBind()
        ddlyear.SelectedIndex = intSelectedIndex - 1
    End Sub


    Sub BindGrid()
        Dim intCnt As Integer
        Dim lbButton As LinkButton
        Dim lbstatus As Label

        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadData()
        PageCount = objGlobal.mtdGetPageCount(Session("SS_PAGINGCOUNT"), dgEmpList.PageSize)
        If PageCount < 1 Then
            PageCount = 1
        End If
        If lblCurrentIndex.Text >= PageCount Then
            If PageCount = 0 Then
                lblCurrentIndex.Text = 0
            Else
                lblCurrentIndex.Text = PageCount - 1
                dsData = LoadData()
            End If
        End If

        dgEmpList.DataSource = dsData
        dgEmpList.DataBind()
        lblPageCount.Text = PageCount
        BindPageList(PageCount)
        PageNo = lblCurrentIndex.Text + 1
    End Sub

    Protected Function LoadData() As DataSet
        Dim strOpCd_Get As String = "PR_PR_TRX_ANGSURAN_RINCIAN_GET_LIST"
        Dim strSrchMonth As String
        Dim strSrchYear As String
        Dim strSrchBKM As String
        Dim strSearch As String
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer

        Session("SS_PAGING") = lblCurrentIndex.Text

        If txtEmpCode.Text.Trim() <> "" Then
            strSearch = "AND A.CODEEMP LIKE '%" & Trim(txtEmpCode.Text) & "%' "
        End If
        If txtEmpName.Text.Trim() <> "" Then
            strSearch = strSearch & "AND E.EMPNAME LIKE '%" & Trim(txtEmpName.Text) & "%' "
        End If

        strSrchMonth = ddlEmpMonth.SelectedItem.Value.Trim()
        strSrchYear = ddlyear.SelectedItem.Value.Trim()

        strParamName = "LOCCODE|ACCMONTH|ACCYEAR|STRSEARCH"
        strParamValue = strLocation & "|" & strSrchMonth & "|" & strSrchYear & "|" & strSearch

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_Get, strParamName, strParamValue, objEmpDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_ANGSURAN_RINCIAN_GET_LIST&errmesg=" & Exp.Message)
        End Try

        Return objEmpDs
    End Function

    Sub BindPageList(ByVal cnt As String)
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = lblPageCount.Text + 1
            arrDList.Add("Page " & count & " of " & cnt)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = lblCurrentIndex.Text
    End Sub

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                lblCurrentIndex.Text = 0
            Case "prev"
                lblCurrentIndex.Text = _
                Math.Max(0, lblCurrentIndex.Text - 1)
            Case "next"
                lblCurrentIndex.Text = _
                Math.Min(lblPageCount.Text - 1, lblCurrentIndex.Text + 1)
            Case "last"
                lblCurrentIndex.Text = lblPageCount.Text - 1
        End Select
        lstDropList.SelectedIndex = lblCurrentIndex.Text
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            lblCurrentIndex.Text = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        lblCurrentIndex.Text = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        lblCurrentIndex.Text = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub PreviewBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim intCnt As Integer
        Dim objMapPath As String
        Dim objFTPFolder As String
        Dim strAccYear As String
        Dim strPeriod As String = IIf(Len(ddlEmpMonth.SelectedItem.Value) = 1, "0" & Trim(ddlyear.SelectedItem.Value), Trim(ddlEmpMonth.SelectedItem.Value))
        strAccYear = ddlyear.SelectedItem.Value

        Dim strOpCd_Get As String = "PR_PR_TRX_ANGSURAN_RINCIAN_GET_LIST"
        Dim strSrchMonth As String
        Dim strSrchYear As String
        Dim strSrchBKM As String
        Dim strSearch As String
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim ObjJurnalDs As DataSet

        strSrchBKM = IIf(txtEmpCode.Text.Trim() = "", "", txtEmpCode.Text.Trim())

        If txtEmpCode.Text.Trim() <> "" Then
            strSearch = "AND A.CODEEMP LIKE '%" & Trim(txtEmpCode.Text) & "%' "
        End If
        If txtEmpName.Text.Trim() <> "" Then
            strSearch = strSearch & "AND E.EMPNAME LIKE '%" & Trim(txtEmpName.Text) & "%' "
        End If

        strSrchMonth = ddlEmpMonth.SelectedItem.Value.Trim()
        strSrchYear = ddlyear.SelectedItem.Value.Trim()

        strParamName = "LOCCODE|ACCMONTH|ACCYEAR|STRSEARCH"
        strParamValue = strLocation & "|" & strSrchMonth & "|" & strSrchYear & "|" & strSearch

        Try
            intErrNo = objGLRpt.mtdGetReportDataCommon(strOpCd_Get, strParamName, strParamValue, ObjJurnalDs, objMapPath, objFTPFolder)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_ANGSURAN_RINCIAN_GET_LIST&errmesg=" & Exp.Message)
        End Try

        Dim MyCSVFile As String = objFTPFolder & "RincianAngsuran-" & Trim(strCompany) & Trim(strPeriod) & Trim(strAccYear) & ".csv"
        If My.Computer.FileSystem.FileExists(MyCSVFile) = True Then
            My.Computer.FileSystem.DeleteFile(MyCSVFile)
        End If
        Dim dataToWrite As IO.StreamWriter = My.Computer.FileSystem.OpenTextFileWriter(MyCSVFile, True)

        dataToWrite.WriteLine("NIK,Nama,Divisi,Jabatan,Jns.Pinjaman,Tot.Pinjaman,Tot.Byr.SD.Bln.Lalu,Angsuran,Tot.Byr.SD.Bln.Ini,Sisa Pinjaman,Awal Angsuran")

        If ObjJurnalDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To ObjJurnalDs.Tables(0).Rows.Count - 1
                dataToWrite.WriteLine(Trim(ObjJurnalDs.Tables(0).Rows(intCnt).Item("CodeEmp")) & "," & Trim(ObjJurnalDs.Tables(0).Rows(intCnt).Item("EmpName")) & "," & _
                Trim(ObjJurnalDs.Tables(0).Rows(intCnt).Item("IDDiv")) & "," & Trim(ObjJurnalDs.Tables(0).Rows(intCnt).Item("Jabatan")) & "," & Trim(ObjJurnalDs.Tables(0).Rows(intCnt).Item("Tipe")) & "," & _
                Trim(ObjJurnalDs.Tables(0).Rows(intCnt).Item("TotalPinjaman")) & "," & Trim(ObjJurnalDs.Tables(0).Rows(intCnt).Item("TotalBayarSDBlnLalu")) & "," & Trim(ObjJurnalDs.Tables(0).Rows(intCnt).Item("Angsuran")) & "," & _
                Trim(ObjJurnalDs.Tables(0).Rows(intCnt).Item("TotalBayarSDBlnIni")) & "," & Trim(ObjJurnalDs.Tables(0).Rows(intCnt).Item("TotalSisa")) & "," & Trim(ObjJurnalDs.Tables(0).Rows(intCnt).Item("PeriodeMulai")))
            Next

            lblErrMessage.Visible = True
            lblErrMessage.Text = "Rincian angsuran created in " & Trim(MyCSVFile)
        End If
        dataToWrite.Close()

    End Sub
End Class
