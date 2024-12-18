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
Imports Microsoft.VisualBasic.Information
Imports System.Xml
Imports System.Web.Services
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web

Public Class NU_StdRpt_SeedlingsIssueListPreview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objNU As New agri.NU.clsReport()
    Dim objNUSetup As New agri.NU.clsSetup()
    Dim objNUTrx As New agri.NU.clsTrx()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strUserLoc As String

    Dim intDDLAccMthFrom As Integer
    Dim intDDLAccYrFrom As Integer
    Dim intDDLAccMthTo As Integer
    Dim intDDLAccYrTo As Integer

    Dim tempLoc As String
    Dim strAccPeriod As String
    Dim strDecimal As String

    Dim intConfigsetting As Integer
    Dim strBlkType As String
    Dim blnCostAtBlock As Boolean
    Dim strBlockCode As String
    Dim lblBlockType As String
    Dim lblBlockCode As String
    Dim lblBlockSubBlockCode As String
    Dim strStatus As String

    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        crvView.Visible = False

        If Right(Request.QueryString("Location"), 1) = "," Then
            strUserLoc = Left(Request.QueryString("Location"), Len(Request.QueryString("Location")) - 1)
        Else
            strUserLoc = Trim(Request.QueryString("Location"))
        End If

        tempLoc = Request.QueryString("Location")
        If Right(tempLoc, 1) = "," Then
            Session("SS_LOC") = Left(tempLoc, Len(tempLoc) - 1)
        Else
            Session("SS_LOC") = tempLoc.Replace("','", ", ")
        End If

        intDDLAccMthFrom = Request.QueryString("DDLAccMth")
        intDDLAccYrFrom = Request.QueryString("DDLAccYr")
        intDDLAccMthTo = Request.QueryString("DDLAccMthTo")
        intDDLAccYrTo = Request.QueryString("DDLAccYrTo")
        
        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_NUACCMONTH")
        strAccYear = Session("SS_NUACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            BindReport()
        End If
    End Sub


    Sub BindReport()
        Dim objRptDs As New DataSet()
        Dim objMapPath As String
        
        Dim strSelect As String
        Dim strFrom As String
        Dim strWhere As String
        Dim strOrderBy As String
        
        Dim intCnt As Integer

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOpCd_GET As String = "NU_STDRPT_SEEDLINGS_ISSUE_LIST_GET"
        strSelect = ""
        strFrom = ""
        strOrderBy = " SI.IssueID, SI.IssueLnID "

        strWhere = " SI.LocCode IN('" & strUserLoc & "')" & vbCrLf
        
        If Not (Request.QueryString("DateFrom") = "" And Request.QueryString("DateTo") = "") Then
            strWhere = strWhere & " AND (DateDiff(Day, '" & Request.QueryString("DateFrom") & "', SI.IssueDate) >= 0) And (DateDiff(Day, '" & Request.QueryString("DateTo") & "', SI.IssueDate) <= 0)" & vbCrLf
        End If

        If Not (Request.QueryString("IssueIDFrom") = "" And Request.QueryString("IssueIDTo") = "") Then
            strWhere = strWhere & " AND (SI.IssueID >= '" & Replace(Request.QueryString("IssueIDFrom"), "'", "''") & "' AND SI.IssueID <= '" & Replace(Request.QueryString("IssueIDTo"), "'", "''") & "')" & vbCrLf
        End If
        
        If Not Request.QueryString("NUBlockCode") = "" Then
            strWhere = strWhere & " AND SI.BlkCode LIKE '" & Replace(Request.QueryString("NUBlockCode"), "'", "''") & "'" & vbCrLf
        End If
        
        If Not Request.QueryString("Status") = "" Then
            If Not Request.QueryString("Status") = objNUTrx.EnumSeedlingsIssueStatus.All Then
                strWhere = strWhere & " AND SI.Status = '" & Request.QueryString("Status") & "'" & vbCrLf
            End If
        End If
        strStatus = objNUTrx.mtdGetSeedlingsIssueStatus(Request.QueryString("Status"))
        
        If Not Request.QueryString("BatchNo") = "" Then
            strWhere = strWhere & " AND SIL.BatchNo LIKE '" & Replace(Request.QueryString("BatchNo"), "'", "''") & "'" & vbCrLf
        End If
        
        If Not Request.QueryString("AccCode") = "" Then
            strWhere = strWhere & " AND SIL.AccCode LIKE '" & Replace(Request.QueryString("AccCode"), "'", "''") & "'" & vbCrLf
        End If

        If Not Request.QueryString("VehCode") = "" Then
            strWhere = strWhere & " AND SIL.VehCode LIKE '" & Replace(Request.QueryString("VehCode"), "'", "''") & "'" & vbCrLf
        End If

        If Not Request.QueryString("VehTypeCode") = "" Then
            strWhere = strWhere & " AND SIL.VehCode IN (SELECT DISTINCT VehCode FROM GL_VEHICLE WHERE VehTypeCode LIKE '" & Replace(Request.QueryString("VehTypeCode"), "'", "''") & "')" & vbCrLf
        End If

        If Not Request.QueryString("VehExpCode") = "" Then
            strWhere = strWhere & " AND SIL.VehExpCode LIKE '" & Replace(Request.QueryString("VehExpCode"), "'", "''") & "'" & vbCrLf
        End If
        
        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
            blnCostAtBlock = True
            lblBlockSubBlockCode = Request.QueryString("lblBlkCode")
        Else
            blnCostAtBlock = False
            lblBlockSubBlockCode = Request.QueryString("lblSubBlkCode")
        End If
        
        strBlkType = Request.QueryString("BlkType")
        If strBlkType = "BlkGrp" Then
            lblBlockCode = Request.QueryString("lblBlkGrp")
            strBlockCode = Request.QueryString("BlkGrp")
            If Not Request.QueryString("BlkGrp") = "" Then
                If blnCostAtBlock = True Then
                    strWhere = strWhere & " AND SIL.BlkCode IN (SELECT DISTINCT BlkCode FROM GL_BLOCK WHERE BlkGrpCode LIKE '" & Request.QueryString("BlkGrp") & "')" & vbCrLf
                Else
                    strWhere = strWhere & " AND SIL.BlkCode IN (SELECT DISTINCT SubBlkCode FROM GL_SUBBLK WHERE BlkCode IN (SELECT DISTINCT BlkCode FROM GL_BLOCK WHERE BlkGrpCode LIKE '" & Request.QueryString("BlkGrp") & "')" & vbCrLf
                End If
            End If
        ElseIf strBlkType = "BlkCode" Then
            lblBlockCode = Request.QueryString("lblBlkCode")
            strBlockCode = Request.QueryString("BlkCode")
            If Not Request.QueryString("BlkCode") = "" Then
                If blnCostAtBlock = True Then
                    strWhere = strWhere & " AND SIL.BlkCode LIKE '" & Request.QueryString("BlkCode") & "'" & vbCrLf
                Else
                    strWhere = strWhere & " AND SIL.BlkCode IN (SELECT DISTINCT SubBlkCode FROM GL_SUBBLK WHERE BlkCode LIKE '" & Request.QueryString("BlkCode") & "')" & vbCrLf
                End If
            End If
        ElseIf strBlkType = "SubBlkCode" Then
            lblBlockCode = Request.QueryString("lblSubBlkCode")
            strBlockCode = Request.QueryString("SubBlkCode")
            If Not Request.QueryString("SubBlkCode") = "" Then
                If blnCostAtBlock = True Then
                    strWhere = strWhere & " AND SIL.BlkCode IN (SELECT DISTINCT BlkCode FROM GL_SUBBLK WHERE SubBlkCode LIKE '" & Request.QueryString("SubBlkCode") & "')" & vbCrLf
                Else
                    strWhere = strWhere & " AND SIL.BlkCode LIKE '" & Request.QueryString("SubBlkCode") & "'" & vbCrLf
                End If
            End If
        End If
        
        Try
            intErrNo = objNU.mtdGetAccPeriod(intDDLAccMthFrom, intDDLAccYrFrom, intDDLAccMthTo, intDDLAccYrTo, strAccPeriod)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=NU_STDRPT_SEEDLINGS_ISSUE_LIST_CHECKACCPERIOD&errmesg=" & Server.UrlEncode(lblErrMessage.Text) & "&redirect=")
        End Try
        
        strWhere = strWhere & Replace(strAccPeriod, "NU.", "SI.")


        Try
            intErrNo = objNU.mtdGetReport_SeedlingsIssueList(strOpCd_GET, _
                                                            strSelect, _
                                                            strFrom, _
                                                            strWhere, _
                                                            strOrderBy, _
                                                            objRptDs, _
                                                            objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=NU_STDRPT_SEEDLINGS_ISSUE_LIST_GET&errmesg=" & Server.UrlEncode(lblErrMessage.Text) & "&redirect=")
        End Try
        
        For intCnt = 0 To objRptDs.Tables(0).Rows.Count - 1
            objRptDs.Tables(0).Rows(intCnt).Item("Status") = objNUTrx.mtdGetSeedlingsIssueStatus(objRptDs.Tables(0).Rows(intCnt).Item("Status"))
        Next

        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\NU_StdRpt_SeedlingsIssueList.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3

        crvView.Visible = False                         
        crvView.ReportSource = rdCrystalViewer          
        crvView.DataBind()                              

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\NU_StdRpt_SeedlingsIssueList.pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/NU_StdRpt_SeedlingsIssueList.pdf"">")
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

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("ParamLocation")
        paramField2 = paramFields.Item("ParamCompanyName")
        paramField3 = paramFields.Item("ParamUserName")
        paramField4 = paramFields.Item("ParamDecimal")
        paramField5 = paramFields.Item("ParamRptID")
        paramField6 = paramFields.Item("ParamRptName")
        paramField7 = paramFields.Item("ParamAccPeriodFrom")
        paramField8 = paramFields.Item("ParamAccPeriodTo")
        paramField9 = paramFields.Item("ParamIssueIDFrom")
        paramField10 = paramFields.Item("ParamIssueIDTo")
        paramField11 = paramFields.Item("ParamDocRefNo")
        paramField12 = paramFields.Item("ParamIssueDateFrom")
        paramField13 = paramFields.Item("ParamIssueDateTo")
        paramField14 = paramFields.Item("ParamNurseryBlockCode")
        paramField15 = paramFields.Item("ParamBatchNo")
        paramField16 = paramFields.Item("ParamAccCode")
        paramField17 = paramFields.Item("ParamBlockType")
        paramField18 = paramFields.Item("ParamBlockCode")
        paramField19 = paramFields.Item("ParamVehCode")
        paramField20 = paramFields.Item("ParamVehExpCode")
        paramField21 = paramFields.Item("ParamStatus")
        paramField22 = paramFields.Item("lblLocation")
        paramField23 = paramFields.Item("lblNurseryBlock")
        paramField24 = paramFields.Item("lblBatchNo")
        paramField25 = paramFields.Item("lblAccount")
        paramField26 = paramFields.Item("lblBlockType")
        paramField27 = paramFields.Item("lblBlock")
        paramField28 = paramFields.Item("lblVehicle")
        paramField29 = paramFields.Item("lblVehicleExpense")
        paramField30 = paramFields.Item("lblBlockCode")
        paramField31 = paramFields.Item("ParamVehType")
        paramField32 = paramFields.Item("lblVehType")
        paramField33 = paramFields.Item("ParamInterEstateEnabled")

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

        ParamDiscreteValue1.Value = Session("SS_LOC")
        ParamDiscreteValue2.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue3.Value = Session("SS_USERNAME")
        ParamDiscreteValue4.Value = Request.QueryString("Decimal")
        ParamDiscreteValue5.Value = Request.QueryString("RptID")
        ParamDiscreteValue6.Value = Request.QueryString("RptName")
        ParamDiscreteValue7.Value = Request.QueryString("DDLAccMth") & "/" & Request.QueryString("DDLAccYr")
        ParamDiscreteValue8.Value = Request.QueryString("DDLAccMthTo") & "/" & Request.QueryString("DDLAccYrTo")
        ParamDiscreteValue9.Value = Request.QueryString("IssueIDFrom")
        ParamDiscreteValue10.Value = Request.QueryString("IssueIDTo")
        ParamDiscreteValue11.Value = Request.QueryString("DocRefNo")
        ParamDiscreteValue12.Value = Request.QueryString("DateFrom")
        ParamDiscreteValue13.Value = Request.QueryString("DateTo")
        ParamDiscreteValue14.Value = Request.QueryString("NUBlockCode")
        ParamDiscreteValue15.Value = Request.QueryString("BatchNo")
        ParamDiscreteValue16.Value = Request.QueryString("AccCode")
        ParamDiscreteValue17.Value = lblBlockCode 'Request.QueryString("BlkType")
        ParamDiscreteValue18.Value = strBlockCode
        ParamDiscreteValue19.Value = Request.QueryString("VehCode")
        ParamDiscreteValue20.Value = Request.QueryString("VehExpCode")
        ParamDiscreteValue21.Value = strStatus
        ParamDiscreteValue22.Value = Request.QueryString("lblLocation")
        ParamDiscreteValue23.Value = Request.QueryString("lblNUBlockCode")
        ParamDiscreteValue24.Value = Request.QueryString("lblBatchNo")
        ParamDiscreteValue25.Value = Request.QueryString("lblAccCode")
        ParamDiscreteValue26.Value = Request.QueryString("lblBlkType")
        ParamDiscreteValue27.Value = lblBlockCode
        ParamDiscreteValue28.Value = Request.QueryString("lblVehCode")
        ParamDiscreteValue29.Value = Request.QueryString("lblVehExpCode")
        ParamDiscreteValue30.Value = lblBlockSubBlockCode
        ParamDiscreteValue31.Value = Request.QueryString("VehTypeCode")
        ParamDiscreteValue32.Value = Request.QueryString("lblVehTypeCode")
        If Session("SS_INTER_ESTATE_CHARGING") = True Then
            ParamDiscreteValue33.Value = "true"
        Else
            ParamDiscreteValue33.Value = "false"
        End If

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

        crvView.ParameterFieldInfo = paramFields
    End Sub
End Class
