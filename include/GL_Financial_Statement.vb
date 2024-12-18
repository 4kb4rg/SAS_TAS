Imports System
Imports System.Data
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic
Imports System.Web.UI.HtmlControls

Imports System.Web.UI.Control
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.GlobalHdl.clsAccessRights
Imports agri.GL.clsMthEnd


Public Class GL_Financial_Statement : Inherits Page

    Dim objAR As New agri.GlobalHdl.clsAccessRights
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl
    Dim objAdmAcc As New agri.Admin.clsAccPeriod()
    Dim objGLTrx As New agri.GL.ClsTrx

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String = ""
    Dim strAccYear As String = ""
    Dim intConfig As Integer
    Dim intGLAR As Integer
    Dim objLangCapDs As New DataSet
    Dim objFTPFolder As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intConfig = Session("SS_CONFIGSETTING")
        intGLAR = Session("SS_GLAR")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLMonthEnd), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else

            lblErrProcess.Visible = False

            If Not Page.IsPostBack Then
                trLRKomparatif.Visible = False
                trNeracaKomparatif.Visible = False
                OnLoad_Display("")
                lSetReportName()
            End If
        End If
    End Sub

#Region "Event & Component"
    Sub btnGenerate_Click(ByVal Sender As Object, ByVal E As System.EventArgs)

        Dim strTipeReport As String
        Dim strTemplate As String

        If optGroup.Checked = True Then
            strTemplate = "1"
        ElseIf optExpand.Checked = True Then
            strTemplate = "2"
        Else
            lblErrProcess.Text = "Template Report Not Found"
            Exit Sub
        End If

        trLRKomparatif.Visible = False
        trNeracaKomparatif.Visible = False

        dgFS1.DataSource = Nothing
        dgFS2.DataSource = Nothing
        dgFS3.DataSource = Nothing

        strTipeReport = lGetReportype(Trim(ddlRptType.SelectedItem.Value))

        If strTipeReport = "1" Then
            trNeracaKomparatif.Visible = True
            BtnExportNeraca.Visible = True
            BtnExportTmpl2.Visible = False
        ElseIf strTipeReport = "2" Then
            trLRKomparatif.Visible = True
            BtnExportNeraca.Visible = False
            BtnExportTmpl2.Visible = True
        ElseIf strTipeReport = "4" Then
            BtnExportNeraca.Visible = False
            BtnExportTmpl2.Visible = True
        End If

        ReportFSTemplate1(strTemplate, strTipeReport)  '' CURRENT PERIOD GRID
        ReportFSTemplate2(strTemplate, strTipeReport)  '' KOMPARATIF
        ReportFSTemplate3(strTemplate, strTipeReport)  '' KOMPREHENSIF

        ReportFSTemplateNameofTabulasi(strTipeReport)
    End Sub
    Sub btnPrint_Click(ByVal Sender As Object, ByVal E As System.EventArgs)

        Dim strTemplate As String = ""
        Dim strAccMonth As String = ""
        Dim strAccYear As String = ""

        If optGroup.Checked = True Then
            strTemplate = "1"
        ElseIf optExpand.Checked = True Then
            strTemplate = "2"
        Else
            lblErrProcess.Text = "Template Report Not Found"
            Exit Sub
        End If

        Dim strReportName As String = ddlRptPrint.SelectedValue
        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/GL_Rpt_FSReport.aspx?doctype=1&CompName=" & strCompany &
                        "&strRptName=" & strReportName &
                        "&strTemplate=" & strTemplate &
                        "&strAccMonth=" & ddlAccMonth.SelectedItem.Value &
                        "&strAccYear=" & ddlAccYear.SelectedItem.Value &
                        """,null ,""status=yes, height=450, width=800, top=180, left=220, resizable=no, scrollbars=no, toolbar=no, location=no"");</Script>")

    End Sub

    Sub btnExcelTemplate1_Click(ByVal Sender As Object, ByVal E As System.EventArgs)
        ExportGridToExcel(dgFS1, TABBK.Tabs.Item(0).Text)
    End Sub

    Sub btnExcelTemplate2_Click(ByVal Sender As Object, ByVal E As System.EventArgs)
        ExportGridToExcel(dgFS2, TABBK.Tabs.Item(1).Text)
    End Sub

    Sub btnExcelTemplateNeraca_Click(ByVal Sender As Object, ByVal E As System.EventArgs)
        ExportGridToExcel(dgFSNrcDetail, TABBK.Tabs.Item(1).Text)
    End Sub

    Sub btnExcelTemplate3_CLick(ByVal Sender As Object, ByVal E As System.EventArgs)
        ExportGridToExcel(dgFS3, TABBK.Tabs.Item(2).Text)
    End Sub

    Sub optGroup_CheckedChanged(sender As Object, e As EventArgs)
        If optGroup.Checked = True Then
            optExpand.Checked = False
            optGroup.Checked = True
        End If
    End Sub
    Sub optExpand_CheckedChanged(sender As Object, e As EventArgs)
        If optExpand.Checked = True Then
            optExpand.Checked = True
            optGroup.Checked = False
        End If
    End Sub


#End Region

#Region "LOCAL & PROCEDURE"

    Sub OnLoad_Display(ByVal pv_strAccYear As String)
        Dim objAccCfg As New DataSet()
        Dim strOpCd_AccCfg_Get As String = "ADMIN_CLSACCPERIOD_CONFIG_ACCYEAR_GET"

        Dim intAccMonth As Integer
        Dim _strAccYear As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strParam As String

        If pv_strAccYear = "" Then
            ddlAccYear.Items.Clear()

            For intCnt = (Convert.ToInt16(strAccYear) - 1) To IIf(UCase(Trim(strCompany)) = "STA", Convert.ToInt16(strAccYear) + 2, Convert.ToInt16(strAccYear) + 1)
                ddlAccYear.Items.Add(New ListItem(intCnt, intCnt))
            Next

            'ddlAccYear.SelectedIndex = ddlAccYear.Items.Count - 2
            ddlAccYear.Text = strAccYear
            _strAccYear = ddlAccYear.SelectedItem.Value
        Else
            _strAccYear = pv_strAccYear
        End If

        ddlAccMonth.Items.Clear()
        If _strAccYear = strAccYear Then
            intAccMonth = 12
            For intCnt = 1 To intAccMonth
                ddlAccMonth.Items.Add(New ListItem(intCnt, intCnt))
            Next
            'ddlAccMonth.SelectedIndex = Val(strAccMonth)
            ddlAccMonth.Text = Val(strAccMonth)
        Else
            Try
                strParam = "||" & _strAccYear
                intErrNo = objAdmAcc.mtdGetAccPeriodCfg(strOpCd_AccCfg_Get,
                                                        strCompany,
                                                        strLocation,
                                                        strUserId,
                                                        strParam,
                                                        objAccCfg)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_TRX_JOURNALADJ_DETAIL_ACCCFG_GET&errmesg=" & Exp.ToString() & "&redirect=GL/trx/GL_trx_JournalAdj_List.aspx")
            End Try

            Try
                btnProceed.Visible = True
                intAccMonth = Convert.ToInt16(objAccCfg.Tables(0).Rows(0).Item("MaxPeriod"))
            Catch Exp As System.Exception
                lblErrProcess.Visible = True
                btnProceed.Visible = False
            End Try
            objAccCfg = Nothing

            For intCnt = 1 To 12
                ddlAccMonth.Items.Add(New ListItem(intCnt, intCnt))
            Next
            'ddlAccMonth.SelectedIndex = Val(strAccMonth)
            ddlAccMonth.Text = Val(strAccMonth)
        End If
    End Sub
    Sub lSetReportName()
        Dim strOpCd_Get As String = "IN_CLSTRX_GLOBAL_UPD"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer

        Dim dr As DataRow
        Dim sSQLKriteria As String = ""
        Dim objdsST As New DataSet
        Dim intSelectedIndex As Integer


        sSQLKriteria = "Select ReportCode, StmtType, Name, (RTrim(Name) + ' (' + rtrim(Description) + ') ') AS Description From GL_FSTEMPLATE "

        strParamName = "STRSEARCH"
        strParamValue = sSQLKriteria

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd_Get,
                                         strParamName,
                                         strParamValue,
                                         objdsST)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CM_TRX_CONTRACTREGDET_SELLERLIST_GET&errmesg=" & lblErrProcess.Text & "&redirect=")
        End Try

        dr = objdsST.Tables(0).NewRow()
        dr("ReportCode") = ""
        dr("Description") = "Please Select Report"
        objdsST.Tables(0).Rows.InsertAt(dr, 0)

        ddlRptType.DataSource = objdsST.Tables(0)
        ddlRptType.DataValueField = "ReportCode"
        ddlRptType.DataTextField = "Description"
        ddlRptType.DataBind()
        ddlRptType.SelectedIndex = intSelectedIndex

    End Sub
    Function lGetReportype(ByVal pReportCode As String) As String
        Dim strOpCd_Get As String = "IN_CLSTRX_GLOBAL_UPD"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim strReportType As String = ""
        Dim sSQLKriteria As String = ""
        Dim objdsST As New DataSet

        sSQLKriteria = "Select ReportCode,StmtType,Name,(rtrim(Name) + ' (' + rtrim(Description) + ') ') AS Description From GL_FSTEMPLATE Where ReportCode='" & pReportCode & "'"

        strParamName = "STRSEARCH"
        strParamValue = sSQLKriteria

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd_Get,
                                         strParamName,
                                         strParamValue,
                                         objdsST)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CM_TRX_CONTRACTREGDET_SELLERLIST_GET&errmesg=" & lblErrProcess.Text & "&redirect=")
        End Try

        If objdsST.Tables(0).Rows.Count > 0 Then
            strReportType = Trim(objdsST.Tables(0).Rows(0).Item("StmtType"))
        End If

        Return strReportType
    End Function
    Sub ReportFSTemplateNameofTabulasi(ByVal pRptType As String)
        TABBK.Tabs.Item(0).Text = ""
        TABBK.Tabs.Item(1).Text = ""
        TABBK.Tabs.Item(2).Text = ""

        Select Case pRptType
            Case "1"
                trLRKomparatif.Visible = False
                trNeracaKomparatif.Visible = True
                TABBK.Tabs.Item(0).Text = "NERACA"
                TABBK.Tabs.Item(1).Text = "NERACA KOMPARATIF"
                TABBK.Tabs.Item(2).Text = ""
            Case "2"
                trLRKomparatif.Visible = True
                trNeracaKomparatif.Visible = False
                TABBK.Tabs.Item(0).Text = "LABA/RUGI"
                TABBK.Tabs.Item(1).Text = "LABA/RUGI KOMPARATIF"
                TABBK.Tabs.Item(2).Text = "KOMPREHENSIF"
            Case "3"
                trLRKomparatif.Visible = False
                trNeracaKomparatif.Visible = False
                TABBK.Tabs.Item(0).Text = ddlRptType.Text.Trim
                TABBK.Tabs.Item(1).Text = ""
                TABBK.Tabs.Item(2).Text = ""
            Case Else
                trLRKomparatif.Visible = True
                trNeracaKomparatif.Visible = False
                TABBK.Tabs.Item(0).Text = ddlRptType.Text.Trim
                TABBK.Tabs.Item(1).Text = "KOMPARATIF"
        End Select
    End Sub
    Sub ReportFSTemplate1(ByVal pTemplate As String, ByVal pRptType As String)
        Dim strReportCode As String = ""
        Dim strOpCd_Get As String = ""
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer

        Dim objdsST As New DataSet

        Select Case pRptType
            Case "1", "2"
                strOpCd_Get = "GL_FS_REPORT_DETAIL"
            Case "3"
                strOpCd_Get = "GL_FS_REPORT_COGS"
            Case Else
                strOpCd_Get = "GL_FS_REPORT_OTHER_DETAIL"
        End Select

        strParamName = "ACCYEAR|ACCMONTH|RPTCODE|RPTTYPE"
        strParamValue = ddlAccYear.SelectedItem.Value & "|" & ddlAccMonth.SelectedItem.Value & "|" & Trim(ddlRptType.SelectedItem.Value) & "|" & pTemplate

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd_Get,
                                         strParamName,
                                         strParamValue,
                                         objdsST)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CM_TRX_CONTRACTREGDET_SELLERLIST_GET&errmesg=" & lblErrProcess.Text & "&redirect=")
        End Try

        dgFS1.Visible = True
        dgFS1.DataSource = Nothing
        dgFS1.DataSource = objdsST.Tables(0)
        dgFS1.DataBind()

        Dim vSpace As String = ""

        For intCnt = 0 To dgFS1.Items.Count - 1
            If CType(dgFS1.Items(intCnt).FindControl("lblFBold"), Label).Text = "1" Then
                dgFS1.Items(intCnt).Font.Bold = True
            End If

            vSpace = lGetSpace(CType(dgFS1.Items(intCnt).FindControl("lblSpace"), Label).Text)

            CType(dgFS1.Items(intCnt).FindControl("lblDescription"), Label).Text = vSpace & CType(dgFS1.Items(intCnt).FindControl("lblDescription"), Label).Text
        Next
    End Sub
    Sub ReportFSTemplate2(ByVal pTemplate As String, ByVal pRptType As String)
        Dim strOpCd_Get As String
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objdsST As New DataSet
        Dim vSpace As String

        Select Case pRptType
            Case "1"
                strOpCd_Get = "GL_FS_REPORT_DETAIL_NERACA_KOMPARATIF"
                strParamName = "ACCYEAR|ACCMONTH|RPTCODE|RPTTYPE"
                strParamValue = ddlAccYear.SelectedItem.Value & "|" & ddlAccMonth.SelectedItem.Value & "|" & Trim(ddlRptType.SelectedItem.Value) & "|" & pTemplate

                Try
                    intErrNo = objGLTrx.mtdGetDataCommon(strOpCd_Get,
                                                strParamName,
                                                strParamValue,
                                                objdsST)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CM_TRX_CONTRACTREGDET_SELLERLIST_GET&errmesg=" & lblErrProcess.Text & "&redirect=")
                End Try

                dgFS2.Visible = False
                dgFSNrcDetail.Visible = True
                dgFSNrcDetail.DataSource = Nothing
                dgFSNrcDetail.DataSource = objdsST.Tables(0)
                dgFSNrcDetail.DataBind()

                For intCnt = 0 To dgFSNrcDetail.Items.Count - 1
                    If CType(dgFSNrcDetail.Items(intCnt).FindControl("lblFBold"), Label).Text = "1" Then
                        dgFSNrcDetail.Items(intCnt).Font.Bold = True
                    End If

                    vSpace = lGetSpace(CType(dgFSNrcDetail.Items(intCnt).FindControl("lblSpace"), Label).Text)

                    CType(dgFSNrcDetail.Items(intCnt).FindControl("lblDescription"), Label).Text = vSpace & CType(dgFSNrcDetail.Items(intCnt).FindControl("lblDescription"), Label).Text
                Next

            Case "2"

                strOpCd_Get = "GL_FS_REPORT_DETAIL_BYLOCATION"
                strParamName = "ACCYEAR|ACCMONTH|RPTCODE|RPTTYPE"
                strParamValue = ddlAccYear.SelectedItem.Value & "|" & ddlAccMonth.SelectedItem.Value & "|" & Trim(ddlRptType.SelectedItem.Value) & "|" & pTemplate

                Try
                    intErrNo = objGLTrx.mtdGetDataCommon(strOpCd_Get,
                                                strParamName,
                                                strParamValue,
                                                objdsST)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CM_TRX_CONTRACTREGDET_SELLERLIST_GET&errmesg=" & lblErrProcess.Text & "&redirect=")
                End Try

                dgFS2.Visible = True
                dgFSNrcDetail.Visible = False
                dgFS2.DataSource = Nothing
                dgFS2.DataSource = objdsST.Tables(0)
                dgFS2.DataBind()


            Case "4"
                '''--- OTher (Tampil di Grid L/R Komparatif)

                strOpCd_Get = "GL_FS_REPORT_OTHER_DETAIL_COMPARE"
                strParamName = "ACCYEAR|ACCMONTH|RPTCODE|RPTTYPE"
                strParamValue = ddlAccYear.SelectedItem.Value & "|" & ddlAccMonth.SelectedItem.Value & "|" & Trim(ddlRptType.SelectedItem.Value) & "|" & pTemplate

                Try
                    intErrNo = objGLTrx.mtdGetDataCommon(strOpCd_Get,
                                                strParamName,
                                                strParamValue,
                                                objdsST)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CM_TRX_CONTRACTREGDET_SELLERLIST_GET&errmesg=" & lblErrProcess.Text & "&redirect=")
                End Try

                dgFS2.Visible = True
                dgFSNrcDetail.Visible = False
                dgFS2.DataSource = Nothing
                dgFS2.DataSource = objdsST.Tables(0)
                dgFS2.DataBind()

                For intCnt = 0 To dgFS2.Items.Count - 1
                    If CType(dgFS2.Items(intCnt).FindControl("lblFBold"), Label).Text = "1" Then
                        dgFS2.Items(intCnt).Font.Bold = True
                    End If

                    vSpace = lGetSpace(CType(dgFS2.Items(intCnt).FindControl("lblSpace"), Label).Text)

                    CType(dgFS2.Items(intCnt).FindControl("lblDescription"), Label).Text = vSpace & CType(dgFS2.Items(intCnt).FindControl("lblDescription"), Label).Text
                Next

        End Select
    End Sub
    Sub ReportFSTemplate3(ByVal pTemplate As String, ByVal pRptType As String)
        Dim strOpCd_Get As String = "GL_FS_REPORT_DETAIL_BYLOCATION"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objdsST As New DataSet

        Dim vSpace As String
        Dim vStrAccMonthPrev As String = lGetBulanLalu(ddlAccMonth.SelectedItem.Value)
        Dim vStrAccYearPrev As String = lGetTahunLalu(ddlAccMonth.SelectedItem.Value, ddlAccYear.SelectedItem.Value)
        Dim vNamaBulanLalu As String = lGetNamaBulan(vStrAccMonthPrev)
        Dim vNamaBulanBerjalan As String = lGetNamaBulan(ddlAccMonth.SelectedItem.Value)


        Select Case pRptType
            Case "2"
                strOpCd_Get = "GL_FS_REPORT_PROFIT_LOSS_KOMPREHENSIF"
                strParamName = "ACCYEAR|ACCMONTH|RPTCODE|RPTTYPE"
                strParamValue = ddlAccYear.SelectedItem.Value & "|" & ddlAccMonth.SelectedItem.Value & "|" & Trim(ddlRptType.SelectedItem.Value) & "|" & pTemplate

                Try
                    intErrNo = objGLTrx.mtdGetDataCommon(strOpCd_Get,
                                                 strParamName,
                                                 strParamValue,
                                                 objdsST)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CM_TRX_CONTRACTREGDET_SELLERLIST_GET&errmesg=" & lblErrProcess.Text & "&redirect=")
                End Try

                dgFS3.Columns(2).HeaderText = vNamaBulanLalu & "<br>" & vStrAccYearPrev
                dgFS3.Columns(3).HeaderText = vNamaBulanBerjalan & "<br>" & ddlAccYear.SelectedItem.Value
                dgFS3.Visible = True
                dgFS3.DataSource = Nothing
                dgFS3.DataSource = objdsST.Tables(0)
                dgFS3.DataBind()

                For intCnt = 0 To dgFS3.Items.Count - 1
                    If CType(dgFS3.Items(intCnt).FindControl("lblFBold"), Label).Text = "1" Then
                        dgFS3.Items(intCnt).Font.Bold = True
                    End If

                    vSpace = lGetSpace(CType(dgFS3.Items(intCnt).FindControl("lblSpace"), Label).Text)

                    CType(dgFS3.Items(intCnt).FindControl("lblDescription"), Label).Text = vSpace & CType(dgFS3.Items(intCnt).FindControl("lblDescription"), Label).Text
                Next
        End Select
    End Sub
    Function lGetSpace(ByVal pJumlah As String) As String
        Dim vSpace As String = ""
        Select Case pJumlah
            Case "1"
                vSpace = "&nbsp&nbsp"
            Case "2"
                vSpace = "&nbsp&nbsp&nbsp&nbsp"
            Case "3"
                vSpace = "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp"
            Case "4"
                vSpace = "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp"
            Case "5"
                vSpace = "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp"
            Case "6"
                vSpace = "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp"
            Case "7"
                vSpace = "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp"
            Case Else
                vSpace = ""
        End Select

        Return vSpace
    End Function
    Function lGetNamaBulan(ByVal pBulan As String) As String
        Dim vNamaBulan As String
        Select Case pBulan
            Case "1"
                vNamaBulan = "JANUARI"
            Case "2"
                vNamaBulan = "FEBRUARI"
            Case "3"
                vNamaBulan = "MARET"
            Case "4"
                vNamaBulan = "APRIL"
            Case "5"
                vNamaBulan = "MEI"
            Case "6"
                vNamaBulan = "JUNI"
            Case "7"
                vNamaBulan = "JULI"
            Case "8"
                vNamaBulan = "AGUSTUS"
            Case "9"
                vNamaBulan = "SEPTEMBER"
            Case "10"
                vNamaBulan = "OKTOBER"
            Case "11"
                vNamaBulan = "NOVEMBER"
            Case "12"
                vNamaBulan = "DESEMBER"
        End Select

        Return vNamaBulan
    End Function
    Function lGetBulanLalu(ByVal pBulan As String) As String
        Dim strBulanLalu As String
        If pBulan = "1" Then
            strBulanLalu = "12"
        Else
            strBulanLalu = CInt(pBulan) - 1
        End If

        Return strBulanLalu
    End Function
    Function lGetTahunLalu(ByVal pBulan As String, ByVal pTahun As String) As String
        Dim strTahunLalu As String
        If pBulan = "1" Then
            strTahunLalu = CInt(pTahun) - 1
        Else
            strTahunLalu = pTahun
        End If

        Return strTahunLalu
    End Function

    Private Sub ExportGridToExcel(grid As WebControls.DataGrid, fileNamePrefix As String)
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=" & fileNamePrefix & ".xls")
        Response.Charset = ""
        Response.ContentType = "application/vnd.ms-excel"
        Using sw As New System.IO.StringWriter()
            Dim hw As New HtmlTextWriter(sw)
            grid.RenderControl(hw)
            Response.Output.Write(sw.ToString())
            Response.Flush()
            Response.End()
        End Using
    End Sub

    Sub ExportExcel(ByVal pRptType As String)

        Dim stringWrite As New System.IO.StringWriter()
        Dim htmlWrite As New HtmlTextWriter(stringWrite)


        ''---Template 1 -- Semua Jenis Report 
        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=" & TABBK.Tabs.Item(0).Text & "-" & Trim(strLocation) & ".xls")
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.xls"

        dgFS1.RenderControl(htmlWrite)
        Response.Write(stringWrite.ToString())
        Response.ClearContent() ' Clear content, but keep response open for the next export



        Select Case Trim(pRptType)
            Case "1"     ''NERACA
                Response.Clear()
                Response.AddHeader("content-disposition", "attachment;filename=" & TABBK.Tabs.Item(1).Text & "-" & Trim(strLocation) & ".xls")
                Response.Charset = ""
                Response.Cache.SetCacheability(HttpCacheability.NoCache)
                Response.ContentType = "application/vnd.xls"
                trNeracaKomparatif.RenderControl(htmlWrite)
                Response.Write(stringWrite.ToString())
                Response.End()

            Case "2" ''LABA RUGI

                ''---Export content of the second grid
                stringWrite = New System.IO.StringWriter() ' Reset the StringWriter
                htmlWrite = New HtmlTextWriter(stringWrite)

                Response.AddHeader("content-disposition", "attachment;filename=" & TABBK.Tabs.Item(1).Text & "-" & Trim(strLocation) & ".xls")
                Response.Charset = ""
                Response.Cache.SetCacheability(HttpCacheability.NoCache)
                Response.ContentType = "application/vnd.xls"

                dgFS2.RenderControl(htmlWrite)
                Response.Write(stringWrite.ToString())
                Response.End() ' End the response after exporting the second grid

                Response.Clear()
                Response.AddHeader("content-disposition", "attachment;filename=" & TABBK.Tabs.Item(2).Text & "-" & Trim(strLocation) & ".xls")
                Response.Charset = ""
                Response.Cache.SetCacheability(HttpCacheability.NoCache)
                Response.ContentType = "application/vnd.xls"
                dgFS3.RenderControl(htmlWrite)
                Response.Write(stringWrite.ToString())
                Response.End()

            Case "4" ''' OTHER
                Response.Clear()
                Response.AddHeader("content-disposition", "attachment;filename=" & TABBK.Tabs.Item(1).Text & "-" & Trim(strLocation) & ".xls")
                Response.Charset = ""
                Response.Cache.SetCacheability(HttpCacheability.NoCache)
                Response.ContentType = "application/vnd.xls"
                trLRKomparatif.RenderControl(htmlWrite)
                Response.Write(stringWrite.ToString())
                Response.End()

        End Select

    End Sub

#End Region

End Class
