Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports System.Xml
Imports System.Web.Services
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web

Public Class GL_StdRpt_JournalList_Preview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objGL As New agri.GL.clsReport()
    Dim objGLTrx As New agri.GL.clsTrx()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strUserLoc As String
    Dim strExportToExcel As String

    Dim strBlkType As String
    Dim intDecimal As Integer

    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer

    Sub Page_Load(Sender As Object, E As EventArgs)
        crvView.Visible = False  
 
        If Right(Request.QueryString("Location"), 1) = "," Then
            strUserLoc = Left(Request.QueryString("Location"), Len(Request.QueryString("Location")) - 1)
        Else
            strUserLoc = Trim(Request.QueryString("Location"))
        End If

        intDecimal = Request.QueryString("Decimal")
        strBlkType = Request.QueryString("BlkType")

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Request.QueryString("strAccMonth")
        strAccYear = Request.QueryString("strAccYear")
        strLangCode = Session("SS_LANGCODE")

        Session("SS_LBLLOCATION") = Request.QueryString("lblLocation")
        Session("SS_lblAccCode") = Request.QueryString("lblAccCode")
        Session("SS_lblVehTypeCode") = Request.QueryString("lblVehTypeCode")
        Session("SS_lblVehCode") = Request.QueryString("lblVehCode")
        Session("SS_lblVehExpCode") = Request.QueryString("lblVehExpCode")
        Session("SS_lblBlkType") = Request.QueryString("lblBlkType")
        Session("SS_lblBlkGrp") = Request.QueryString("lblBlkGrp")
        Session("SS_lblBlkCode") = Request.QueryString("lblBlkCode")
        Session("SS_lblSubBlkCode") = Request.QueryString("lblSubBlkCode")

        Session("SS_Decimal") = Request.QueryString("Decimal")
        Session("SS_RptID") = Request.QueryString("RptID")
        Session("SS_RptName") = Request.QueryString("RptName")
        Session("SS_DateFrom") = Request.QueryString("DateFrom")
        Session("SS_DateTo") = Request.QueryString("DateTo")
        Session("SS_JournalIDFrom") = Request.QueryString("JournalIDFrom")
        Session("SS_JournalIDTo") = Request.QueryString("JournalIDTo")
        Session("SS_RefNo") = Request.QueryString("RefNo")
        Session("SS_TransType") = Request.QueryString("TransType")
        Session("SS_UpdatedBy") = Request.QueryString("UpdatedBy")
        Session("SS_Status") = Request.QueryString("Status")
        Session("SS_AccCode") = Request.QueryString("AccCode")
        Session("SS_VehTypeCode") = Request.QueryString("VehTypeCode")
        Session("SS_VehCode") = Request.QueryString("VehCode")
        Session("SS_VehExpCode") = Request.QueryString("VehExpCode")
        Session("SS_BlkType") = Request.QueryString("BlkType")
        Session("SS_BlkGrp") = Request.QueryString("BlkGrp")
        Session("SS_BlkCode") = Request.QueryString("BlkCode")
        Session("SS_SubBlkCode") = Request.QueryString("SubBlkCode")
        strExportToExcel = Trim(Request.QueryString("ExportToExcel"))

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            BindReport()
        End If
    End Sub

    Sub BindReport()

        Dim objRptDs As New DataSet()
        Dim objMapPath As String

        Dim intCnt As Integer
        Dim strFileName As String

        Dim SearchStr As String
        Dim vehSelectStr As String
        Dim blkSelectStr As String
        Dim vehSearchStr As String
        Dim blkSearchStr As String
        Dim strOpCdJournal As String

        Dim tempLoc As String
        Dim strParam As String

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOpCd_Journal_BlkGrp_And_Blk_GET As String = "GL_STDRPT_JOURNAL_AND_JOURNALLN_BLKGRP_AND_BLK_GET"
        Dim strOpCd_Journal_SubBlk_GET As String = "GL_STDRPT_JOURNAL_AND_JOURNALLN_SUBBLK_GET"

        Dim WildStr As String = "AND JN.JournalID *= JNLN.JournalID AND "
        Dim NormStr As String = "AND JN.JournalID = JNLN.JournalID AND "
        Dim objFTPFolder As String

        tempLoc = Request.QueryString("Location")
        If Right(tempLoc, 1) = "," Then
            session("SS_LOC") = Left(tempLoc, Len(tempLoc) - 1)
        else
            session("SS_LOC") = tempLoc.Replace("'", "")
        End If

        If strBlkType = "BlkGrp" Then
            If Not Request.QueryString("BlkGrp") = "" Then
                blkSelectStr = "BLK.BlkGrpCode LIKE '" & Request.QueryString("BlkGrp") & "'"
            Else
                blkSelectStr = "BLK.BlkGrpCode LIKE '%'"
                blkSearchStr = "OR ( JNLN.BlkCode = '' )"
            End If
            Session("SS_BLKGRPHEADER") = "BlkGrp"
            strOpCdJournal = strOpCd_Journal_BlkGrp_And_Blk_GET

        ElseIf strBlkType = "BlkCode" Then
            If Not Request.QueryString("BlkCode") = "" Then
                blkSelectStr = blkSelectStr & "JNLN.BlkCode LIKE '" & Request.QueryString("BlkCode") & "' AND "
            Else
                blkSelectStr = blkSelectStr & "JNLN.BlkCode LIKE '%' AND "
                blkSearchStr = "OR ( JNLN.BlkCode = '' )"
            End If
            Session("SS_BLKHEADER") = "BlkCode"
            strOpCdJournal = strOpCd_Journal_BlkGrp_And_Blk_GET

        ElseIf strBlkType = "SubBlkCode" Then
            If Not Request.QueryString("SubBlkCode") = "" Then
                blkSelectStr = blkSelectStr & "JNLN.BlkCode LIKE '" & Request.QueryString("SubBlkCode") & "' AND "
            Else
                blkSelectStr = blkSelectStr & "JNLN.BlkCode LIKE '%' AND "
                blkSearchStr = "OR ( JNLN.BlkCode = '' )"
            End If
            Session("SS_SUBBLKHEADER") = "SubBlkCode"
            strOpCdJournal = strOpCd_Journal_SubBlk_GET
        End If

        If Not (Request.QueryString("DateFrom") = "" And Request.QueryString("DateTo") = "") Then
            SearchStr = "(DateDiff(Day, '" & Session("SS_DATEFROM") & "', JN.DocDate) >= 0) And (DateDiff(Day, '" & Session("SS_DATETO") & "', JN.DocDate) <= 0) AND "
        End If

        If Not (Request.QueryString("JournalIDFrom") = "" And Request.QueryString("JournalIDTo") = "") Then
            SearchStr = SearchStr & "JN.JournalID IN (SELECT SUBJN.JournalID FROM GL_JOURNAL SUBJN WHERE SUBJN.JournalID >= '" & Request.QueryString("JournalIDFrom") & _
                        "' AND SUBJN.JournalID <= '" & Request.QueryString("JournalIDTo") & "') AND "
        End If

        If Not Request.QueryString("RefNo") = "" Then
            SearchStr = SearchStr & "JN.RefNo = '" & Request.QueryString("RefNo") & "' AND "
        End If

        If Not Request.QueryString("TransType") = objGLTrx.EnumJournalTransactType.All Then
            SearchStr = SearchStr & "JN.TransactType = '" & Request.QueryString("TransType") & "' AND "
        Else
            SearchStr = SearchStr & "JN.TransactType LIKE '%' AND "
        End If

        If Not Request.QueryString("UpdatedBy") = "" Then
            SearchStr = SearchStr & "JN.UpdateID LIKE '" & Request.QueryString("UpdatedBy") & "' AND "
        Else
            SearchStr = SearchStr & "JN.UpdateID LIKE '%' AND "
        End If

        If Not Request.QueryString("AccCode") = "" Then
            SearchStr = SearchStr & "JNLN.AccCode LIKE '" & Request.QueryString("AccCode") & "' AND "
        Else
            SearchStr = SearchStr & "JNLN.AccCode LIKE '%' AND "
        End If

        If Not Request.QueryString("VehTypeCode") = "" Then
            vehSelectStr = vehSelectStr & "VEH.VehTypeCode LIKE '" & Request.QueryString("VehTypeCode") & "' AND "
        Else
            vehSelectStr = vehSelectStr & "VEH.VehTypeCode LIKE '%' AND "
        End If

        If Request.QueryString("VehCode") = "" And Request.QueryString("VehTypeCode") = "" Then
            vehSearchStr = "OR ( JNLN.VehCode = '' )"
        End If

        If Not Request.QueryString("VehCode") = "" Then
            SearchStr = SearchStr & "JNLN.VehCode LIKE '" & Request.QueryString("VehCode") & "' AND "
        Else
            SearchStr = SearchStr & "JNLN.VehCode LIKE '%' AND "
        End If

        If Not Request.QueryString("VehExpCode") = "" Then
            SearchStr = SearchStr & "JNLN.VehExpCode LIKE '" & Request.QueryString("VehExpCode") & "' AND "
        Else
            SearchStr = SearchStr & "JNLN.VehExpCode LIKE '%' AND "
        End If


        If Not Request.QueryString("Status") = "" Then
            If Not Request.QueryString("Status") = objGLTrx.EnumJournalStatus.All Then
                SearchStr = SearchStr & "JN.Status = '" & Request.QueryString("Status") & "' AND "
            Else
                SearchStr = SearchStr & "JN.Status LIKE '%' AND "
            End If
        End If

        If Not SearchStr = "" Or Not vehSelectStr = "" Or Not blkSelectStr = "" Then
            If Right(SearchStr, 4) = "AND " Then
                SearchStr = Left(SearchStr, Len(SearchStr) - 4)
            End If

            If Right(vehSelectStr, 4) = "AND " Then
                vehSelectStr = Left(vehSelectStr, Len(vehSelectStr) - 4)
            End If

            If Right(blkSelectStr, 4) = "AND " Then
                blkSelectStr = Left(blkSelectStr, Len(blkSelectStr) - 4)
            End If

            If Not Request.QueryString("AccCode") = "" Or Not Request.QueryString("VehTypeCode") = "" Or Not Request.QueryString("VehCode") = "" Or Not Request.QueryString("VehExpCode") = "" Or Not _
                   Request.QueryString("BlkGrp") = "" Or Not Request.QueryString("BlkCode") = "" Or Not Request.QueryString("SubBlkCode") = "" Then
                strParam = strUserLoc & "|" & strAccMonth & "|" & strAccYear & "|" & vehSelectStr & "|" & vehSearchStr & "|" & blkSelectStr & "|" & blkSearchStr & "|" & NormStr & SearchStr
            Else
                strParam = strUserLoc & "|" & strAccMonth & "|" & strAccYear & "|" & vehSelectStr & "|" & vehSearchStr & "|" & blkSelectStr & "|" & blkSearchStr & "|" & WildStr & SearchStr
            End If

        End If

        Try
            intErrNo = objGL.mtdGetReport_JournalList(strOpCdJournal, strParam, objRptDs, objMapPath, objFTPFolder)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_JOURNAL_LIST_REPORT&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try



        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\GL_StdRpt_JournalList.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3

        crvView.Visible = False                          
        crvView.ReportSource = rdCrystalViewer           
        crvView.DataBind()                               

        PassParam()

        'crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        'crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\GL_StdRpt_JournalList.pdf"

        'crExportOptions = rdCrystalViewer.ExportOptions
        'With crExportOptions
        '    .DestinationOptions = crDiskFileDestinationOptions
        '    .ExportDestinationType = ExportDestinationType.DiskFile
        '    .ExportFormatType = ExportFormatType.PortableDocFormat
        'End With

        'rdCrystalViewer.Export()
        'rdCrystalViewer.Close()
        'rdCrystalViewer.Dispose()
        'Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/GL_StdRpt_JournalList.pdf"">")

        strFileName = "GL_StdRpt_JournalList"

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        If strExportToExcel = "0" Then
            'crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strFileName & ".pdf"
            crDiskFileDestinationOptions.DiskFileName = objFTPFolder & strFileName & ".pdf"
        Else
            'crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strFileName & ".xls"
            crDiskFileDestinationOptions.DiskFileName = objFTPFolder & strFileName & ".xls"
        End If
        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            If strExportToExcel = "0" Then
                .ExportFormatType = ExportFormatType.PortableDocFormat
            Else
                .ExportFormatType = ExportFormatType.Excel
            End If
        End With

        rdCrystalViewer.Export()
        rdCrystalViewer.Close()
        rdCrystalViewer.Dispose()

        Dim strUrl As String
        strUrl = objFTPFolder.Substring(InStr(objFTPFolder, "ftp") - 1, Len(objFTPFolder) - InStr(objFTPFolder, "ftp") + 1)
        strUrl = Replace(strUrl, "\", "/")

        If strExportToExcel = "0" Then
            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../" & strUrl & strFileName & ".pdf"">")
            'Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strFileName & ".pdf"">")
        Else
            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../" & strUrl & strFileName & ".xls"">")
            'Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strFileName & ".xls"">")
        End If


    End Sub

    Sub PassParam()
        Dim paramFields As New ParameterFields()
        Dim paramField1 As New ParameterField()
        Dim paramField2 As New ParameterField()
        Dim paramField3 As New ParameterField()
        Dim paramField4 As New ParameterField()
        Dim paramField5 As New ParameterField()
        Dim paramField6 As New ParameterField()
        Dim paramField7 As New ParameterField()
        Dim paramField8 As New ParameterField()
        Dim paramField9 As New ParameterField()
        Dim paramField10 As New ParameterField()
        Dim paramField11 As New ParameterField()
        Dim paramField12 As New ParameterField()
        Dim paramField13 As New ParameterField()
        Dim paramField14 As New ParameterField()
        Dim paramField15 As New ParameterField()
        Dim paramField16 As New ParameterField()
        Dim paramField17 As New ParameterField()
        Dim paramField18 As New ParameterField()
        Dim paramField19 As New ParameterField()
        Dim paramField20 As New ParameterField()
        Dim paramField21 As New ParameterField()
        Dim paramField22 As New ParameterField()
        Dim paramField23 As New ParameterField()
        Dim paramField24 As New ParameterField()
        Dim paramField25 As New ParameterField()
        Dim paramField26 As New ParameterField()
        Dim paramField27 As New ParameterField()
        Dim paramField28 As New ParameterField()
        Dim paramField29 As New ParameterField()
        Dim paramField30 As New ParameterField()
        Dim paramField31 As New ParameterField()
        Dim paramField32 As New ParameterField()
        Dim paramField33 As New ParameterField()
        Dim paramField34 As New ParameterField()

        Dim ParamDiscreteValue1 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue2 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue3 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue4 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue5 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue6 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue7 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue8 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue9 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue10 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue11 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue12 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue13 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue14 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue15 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue16 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue17 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue18 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue19 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue20 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue21 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue22 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue23 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue24 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue25 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue26 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue27 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue28 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue29 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue30 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue31 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue32 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue33 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue34 As New ParameterDiscreteValue()

        Dim crParameterValues1 As ParameterValues
        Dim crParameterValues2 As ParameterValues
        Dim crParameterValues3 As ParameterValues
        Dim crParameterValues4 As ParameterValues
        Dim crParameterValues5 As ParameterValues
        Dim crParameterValues6 As ParameterValues
        Dim crParameterValues7 As ParameterValues
        Dim crParameterValues8 As ParameterValues
        Dim crParameterValues9 As ParameterValues
        Dim crParameterValues10 As ParameterValues
        Dim crParameterValues11 As ParameterValues
        Dim crParameterValues12 As ParameterValues
        Dim crParameterValues13 As ParameterValues
        Dim crParameterValues14 As ParameterValues
        Dim crParameterValues15 As ParameterValues
        Dim crParameterValues16 As ParameterValues
        Dim crParameterValues17 As ParameterValues
        Dim crParameterValues18 As ParameterValues
        Dim crParameterValues19 As ParameterValues
        Dim crParameterValues20 As ParameterValues
        Dim crParameterValues21 As ParameterValues
        Dim crParameterValues22 As ParameterValues
        Dim crParameterValues23 As ParameterValues
        Dim crParameterValues24 As ParameterValues
        Dim crParameterValues25 As ParameterValues
        Dim crParameterValues26 As ParameterValues
        Dim crParameterValues27 As ParameterValues
        Dim crParameterValues28 As ParameterValues
        Dim crParameterValues29 As ParameterValues
        Dim crParameterValues30 As ParameterValues
        Dim crParameterValues31 As ParameterValues
        Dim crParameterValues32 As ParameterValues
        Dim crParameterValues33 As ParameterValues
        Dim crParameterValues34 As ParameterValues

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("ParamCompanyName")
        paramField2 = paramFields.Item("ParamLocation")
        paramField3 = paramFields.Item("ParamUserName")
        paramField4 = paramFields.Item("ParamDecimal")
        paramField5 = paramFields.Item("ParamRptID")
        paramField6 = paramFields.Item("ParamRptName")
        paramField7 = paramFields.Item("ParamAccMonth")
        paramField8 = paramFields.Item("ParamAccYear")
        paramField9 = paramFields.Item("ParamDateFrom")
        paramField10 = paramFields.Item("ParamDateTo")
        paramField11 = paramFields.Item("ParamJournalIDFrom")
        paramField12 = paramFields.Item("ParamJournalIDTo")
        paramField13 = paramFields.Item("ParamRefNo")
        paramField14 = paramFields.Item("ParamTransType")
        paramField15 = paramFields.Item("ParamUpdatedBy")
        paramField16 = paramFields.Item("ParamStatus")
        paramField17 = paramFields.Item("ParamAccCode")
        paramField18 = paramFields.Item("ParamVehCode")
        paramField19 = paramFields.Item("ParamVehExpCode")
        paramField20 = paramFields.Item("ParamBlkCode")
        paramField21 = paramFields.Item("ParamSubBlkCode")
        paramField22 = paramFields.Item("ParamBlkOrSubBlk")
        paramField23 = paramFields.Item("lblAccCode")
        paramField24 = paramFields.Item("lblVehCode")
        paramField25 = paramFields.Item("lblVehExpCode")
        paramField26 = paramFields.Item("lblBlkCode")
        paramField27 = paramFields.Item("lblSubBlkCode")
        paramField28 = paramFields.Item("lblLocation")
        paramField29 = paramFields.Item("ParamBlkType")
        paramField30 = paramFields.Item("ParamBlkGrp")
        paramField31 = paramFields.Item("lblBlkGrp")
        paramField32 = paramFields.Item("lblVehTypeCode")
        paramField33 = paramFields.Item("ParamVehTypeCode")
        paramField34 = paramFields.Item("ParamInterEstateEnabled")

        crParameterValues1 = paramField1.CurrentValues
        crParameterValues2 = paramField2.CurrentValues
        crParameterValues3 = paramField3.CurrentValues
        crParameterValues4 = paramField4.CurrentValues
        crParameterValues5 = paramField5.CurrentValues
        crParameterValues6 = paramField6.CurrentValues
        crParameterValues7 = paramField7.CurrentValues
        crParameterValues8 = paramField8.CurrentValues
        crParameterValues9 = paramField9.CurrentValues
        crParameterValues10 = paramField10.CurrentValues
        crParameterValues11 = paramField11.CurrentValues
        crParameterValues12 = paramField12.CurrentValues
        crParameterValues13 = paramField13.CurrentValues
        crParameterValues14 = paramField14.CurrentValues
        crParameterValues15 = paramField15.CurrentValues
        crParameterValues16 = paramField16.CurrentValues
        crParameterValues17 = paramField17.CurrentValues
        crParameterValues18 = paramField18.CurrentValues
        crParameterValues19 = paramField19.CurrentValues
        crParameterValues20 = paramField20.CurrentValues
        crParameterValues21 = paramField21.CurrentValues
        crParameterValues22 = paramField22.CurrentValues
        crParameterValues23 = paramField23.CurrentValues
        crParameterValues24 = paramField24.CurrentValues
        crParameterValues25 = paramField25.CurrentValues
        crParameterValues26 = paramField26.CurrentValues
        crParameterValues27 = paramField27.CurrentValues
        crParameterValues28 = paramField28.CurrentValues
        crParameterValues29 = paramField29.CurrentValues
        crParameterValues30 = paramField30.CurrentValues
        crParameterValues31 = paramField31.CurrentValues
        crParameterValues32 = paramField32.CurrentValues
        crParameterValues33 = paramField33.CurrentValues
        crParameterValues34 = paramField34.CurrentValues

        ParamDiscreteValue1.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue2.Value = Session("SS_LOC")
        ParamDiscreteValue3.Value = Session("SS_USERNAME")
        ParamDiscreteValue4.Value = Session("SS_DECIMAL")
        ParamDiscreteValue5.Value = Session("SS_RPTID")
        ParamDiscreteValue6.Value = Session("SS_RPTNAME")
        ParamDiscreteValue7.Value = strAccMonth
        ParamDiscreteValue8.Value = strAccYear
        ParamDiscreteValue9.Value = Session("SS_DATEFROM")
        ParamDiscreteValue10.Value = Session("SS_DATETO")
        ParamDiscreteValue11.Value = Session("SS_JOURNALIDFROM")
        ParamDiscreteValue12.Value = Session("SS_JOURNALIDTO")
        ParamDiscreteValue13.Value = Session("SS_REFNO")
        ParamDiscreteValue14.Value = Session("SS_TRANSTYPE")
        ParamDiscreteValue15.Value = Session("SS_UPDATEDBY")
        ParamDiscreteValue16.Value = Session("SS_STATUS")
        ParamDiscreteValue17.Value = Session("SS_ACCCODE")
        ParamDiscreteValue18.Value = Session("SS_VEHCODE")
        ParamDiscreteValue19.Value = Session("SS_VEHEXPCODE")
        ParamDiscreteValue20.Value = Session("SS_BLKCODE")
        ParamDiscreteValue21.Value = Session("SS_SUBBLKCODE")

        If strBlkType = "BlkGrp" Then
            ParamDiscreteValue22.Value = Session("SS_BLKGRPHEADER")
        ElseIf strBlkType = "BlkCode" Then
            ParamDiscreteValue22.Value = Session("SS_BLKHEADER")
        ElseIf strBlkType = "SubBlkCode" Then
            ParamDiscreteValue22.Value = Session("SS_SUBBLKHEADER")
        End If

        ParamDiscreteValue23.Value = Session("SS_LBLACCCODE")
        ParamDiscreteValue24.Value = Session("SS_LBLVEHCODE")
        ParamDiscreteValue25.Value = Session("SS_LBLVEHEXPCODE")
        ParamDiscreteValue26.Value = Session("SS_LBLBLKCODE")
        ParamDiscreteValue27.Value = Session("SS_LBLSUBBLKCODE")
        ParamDiscreteValue28.Value = Session("SS_LBLLOCATION")
        ParamDiscreteValue29.Value = Session("SS_BLKTYPE")
        ParamDiscreteValue30.Value = Session("SS_BLKGRP")
        ParamDiscreteValue31.Value = Session("SS_LBLBLKGRP")
        ParamDiscreteValue32.Value = Session("SS_LBLVEHTYPECODE")
        ParamDiscreteValue33.Value = Session("SS_VEHTYPECODE")
        ParamDiscreteValue34.Value = IIf(Session("SS_INTER_ESTATE_CHARGING"), "true", "false")

        crParameterValues1.Add(ParamDiscreteValue1)
        crParameterValues2.Add(ParamDiscreteValue2)
        crParameterValues3.Add(ParamDiscreteValue3)
        crParameterValues4.Add(ParamDiscreteValue4)
        crParameterValues5.Add(ParamDiscreteValue5)
        crParameterValues6.Add(ParamDiscreteValue6)
        crParameterValues7.Add(ParamDiscreteValue7)
        crParameterValues8.Add(ParamDiscreteValue8)
        crParameterValues9.Add(ParamDiscreteValue9)
        crParameterValues10.Add(ParamDiscreteValue10)
        crParameterValues11.Add(ParamDiscreteValue11)
        crParameterValues12.Add(ParamDiscreteValue12)
        crParameterValues13.Add(ParamDiscreteValue13)
        crParameterValues14.Add(ParamDiscreteValue14)
        crParameterValues15.Add(ParamDiscreteValue15)
        crParameterValues16.Add(ParamDiscreteValue16)
        crParameterValues17.Add(ParamDiscreteValue17)
        crParameterValues18.Add(ParamDiscreteValue18)
        crParameterValues19.Add(ParamDiscreteValue19)
        crParameterValues20.Add(ParamDiscreteValue20)
        crParameterValues21.Add(ParamDiscreteValue21)
        crParameterValues22.Add(ParamDiscreteValue22)
        crParameterValues23.Add(ParamDiscreteValue23)
        crParameterValues24.Add(ParamDiscreteValue24)
        crParameterValues25.Add(ParamDiscreteValue25)
        crParameterValues26.Add(ParamDiscreteValue26)
        crParameterValues27.Add(ParamDiscreteValue27)
        crParameterValues28.Add(ParamDiscreteValue28)
        crParameterValues29.Add(ParamDiscreteValue29)
        crParameterValues30.Add(ParamDiscreteValue30)
        crParameterValues31.Add(ParamDiscreteValue31)
        crParameterValues32.Add(ParamDiscreteValue32)
        crParameterValues33.Add(ParamDiscreteValue33)
        crParameterValues34.Add(ParamDiscreteValue34)

        PFDefs(0).ApplyCurrentValues(crParameterValues1)
        PFDefs(1).ApplyCurrentValues(crParameterValues2)
        PFDefs(2).ApplyCurrentValues(crParameterValues3)
        PFDefs(3).ApplyCurrentValues(crParameterValues4)
        PFDefs(4).ApplyCurrentValues(crParameterValues5)
        PFDefs(5).ApplyCurrentValues(crParameterValues6)
        PFDefs(6).ApplyCurrentValues(crParameterValues7)
        PFDefs(7).ApplyCurrentValues(crParameterValues8)
        PFDefs(8).ApplyCurrentValues(crParameterValues9)
        PFDefs(9).ApplyCurrentValues(crParameterValues10)
        PFDefs(10).ApplyCurrentValues(crParameterValues11)
        PFDefs(11).ApplyCurrentValues(crParameterValues12)
        PFDefs(12).ApplyCurrentValues(crParameterValues13)
        PFDefs(13).ApplyCurrentValues(crParameterValues14)
        PFDefs(14).ApplyCurrentValues(crParameterValues15)
        PFDefs(15).ApplyCurrentValues(crParameterValues16)
        PFDefs(16).ApplyCurrentValues(crParameterValues17)
        PFDefs(17).ApplyCurrentValues(crParameterValues18)
        PFDefs(18).ApplyCurrentValues(crParameterValues19)
        PFDefs(19).ApplyCurrentValues(crParameterValues20)
        PFDefs(20).ApplyCurrentValues(crParameterValues21)
        PFDefs(21).ApplyCurrentValues(crParameterValues22)
        PFDefs(22).ApplyCurrentValues(crParameterValues23)
        PFDefs(23).ApplyCurrentValues(crParameterValues24)
        PFDefs(24).ApplyCurrentValues(crParameterValues25)
        PFDefs(25).ApplyCurrentValues(crParameterValues26)
        PFDefs(26).ApplyCurrentValues(crParameterValues27)
        PFDefs(27).ApplyCurrentValues(crParameterValues28)
        PFDefs(28).ApplyCurrentValues(crParameterValues29)
        PFDefs(29).ApplyCurrentValues(crParameterValues30)
        PFDefs(30).ApplyCurrentValues(crParameterValues31)
        PFDefs(31).ApplyCurrentValues(crParameterValues32)
        PFDefs(32).ApplyCurrentValues(crParameterValues33)
        PFDefs(33).ApplyCurrentValues(crParameterValues34)

        crvView.ParameterFieldInfo = paramFields
    End Sub
End Class
