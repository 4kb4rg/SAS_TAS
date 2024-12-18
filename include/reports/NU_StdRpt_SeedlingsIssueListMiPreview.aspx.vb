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

Public Class NU_StdRpt_SeedlingsIssueListMiPreview : Inherits Page

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
        Dim strWhere As String
        Dim strWhereSub As String
        Dim strOrderBy As String
        
        Dim intCnt As Integer

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOpCd_GET As String = "NU_STDRPT_SEEDLINGS_ISSUE_LIST_MI_GET_SP"
        
        strOrderBy = " SI.IssueID, SI.IssueLnID "

        strWhere = " SI.LocCode IN('" & strUserLoc & "')" & vbCrLf


        If Not Request.QueryString("BatchNo") = "" Then
            strWhere = strWhere & " AND SI_LN.BatchNo = '" & Replace(Request.QueryString("BatchNo"), "'", "''") & "'" & vbCrLf
        End If
                
        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
            blnCostAtBlock = True
            lblBlockSubBlockCode = Request.QueryString("lblBlkCode")
        Else
            blnCostAtBlock = False
            lblBlockSubBlockCode = Request.QueryString("lblSubBlkCode")
        End If
        
        strBlkType = Request.QueryString("BlkType")
        
        If strBlkType = "BlkCode" Then
            lblBlockCode = Request.QueryString("lblBlkCode")
            strBlockCode = Request.QueryString("BlkCode")
            If Not Request.QueryString("BlkCode") = "" Then
                If blnCostAtBlock = True Then
                    strWhere = strWhere & " AND SI.BlkCode LIKE '" & Request.QueryString("BlkCode") & "'" & vbCrLf
                Else
                    strWhere = strWhere & " AND SI.BlkCode IN (SELECT DISTINCT SubBlkCode FROM GL_SUBBLK WHERE BlkCode LIKE '" & Request.QueryString("BlkCode") & "')" & vbCrLf
                End If
            End If
        ElseIf strBlkType = "SubBlkCode" Then
            lblBlockCode = Request.QueryString("lblSubBlkCode")
            strBlockCode = Request.QueryString("SubBlkCode")
            If Not Request.QueryString("SubBlkCode") = "" Then
                If blnCostAtBlock = True Then
                    strWhere = strWhere & " AND SI.BlkCode IN (SELECT DISTINCT BlkCode FROM GL_SUBBLK WHERE SubBlkCode LIKE '" & Request.QueryString("SubBlkCode") & "')" & vbCrLf
                Else
                    strWhere = strWhere & " AND SI.BlkCode LIKE '" & Request.QueryString("SubBlkCode") & "'" & vbCrLf
                End If
            End If
        End If
        

        strWhereSub = strWhere & " AND SI.AccMonth = " & intDDLAccMthFrom & " AND SI.AccYear ='" & intDDLAccYrFrom & "'"
        strWhere = strWhere & " AND SI.AccMonth <=" & intDDLAccMthFrom & " AND SI.AccYear ='" & intDDLAccYrFrom & "'"
   
        If Not Request.QueryString("PlantMaterial") = "" Then
            strWhere = strWhere & " AND BT.PlantMaterial = '" & Replace(Request.QueryString("PlantMaterial"), "'", "''") & "'" & vbCrLf
        End If

        strWhere = strWhere & " AND SI.Status = '2' "

        strWhereSub = replace(strWhereSub, "'", "''")        
        strWhere = replace(strWhere, "'", "''") 
        Try
            intErrNo = objNU.mtdGetReport_SeedlingsIssueListMi(strOpCd_GET, _    
                                                            strLocation, _                                                        
                                                            strWhereSub, _
                                                            strWhere, _
                                                            strOrderBy, _
                                                            objRptDs, _
                                                            objMapPath)

            objRptDs.Tables(0).TableName = "NU_StdRpt_SeedlingsIssueListMi"
            objRptDs.Tables(1).TableName = "NU_StdRpt_SeedlingsIssueListMiDesc"
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=NU_STDRPT_SEEDLINGS_ISSUE_LIST_GET&errmesg=" & Server.UrlEncode(lblErrMessage.Text) & "&redirect=")
        End Try
        
        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\NU_StdRpt_SeedlingsIssueListMi.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs)

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3

        crvView.Visible = False                         
        crvView.ReportSource = rdCrystalViewer          
        crvView.DataBind()                              

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\NU_StdRpt_SeedlingsIssueListMi1.pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/NU_StdRpt_SeedlingsIssueListMi1.pdf"">")
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

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("ParamLocation")
        paramField2 = paramFields.Item("ParamCompanyName")
        paramField3 = paramFields.Item("ParamUserName")
        paramField4 = paramFields.Item("ParamRptID")
        paramField5 = paramFields.Item("ParamRptName")
        paramField6 = paramFields.Item("ParamAccPeriodFrom")
        paramField7 = paramFields.Item("ParamBatchNo")
        paramField8 = paramFields.Item("ParamBlockType")
        paramField9 = paramFields.Item("ParamBlockCode")
        paramField10 = paramFields.Item("lblLocation")
        paramField11 = paramFields.Item("lblBatchNo")
        paramField12 = paramFields.Item("lblBlockType")
        paramField13 = paramFields.Item("lblBlock")
        paramField14 = paramFields.Item("PlantMaterial")

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

        ParamDiscreteValue1.Value = Session("SS_LOC")
        ParamDiscreteValue2.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue3.Value = Session("SS_USERNAME")
        ParamDiscreteValue4.Value = Request.QueryString("RptID")
        ParamDiscreteValue5.Value = Request.QueryString("RptName")
        ParamDiscreteValue6.Value = Request.QueryString("DDLAccMth") & "/" & Request.QueryString("DDLAccYr")       
        ParamDiscreteValue7.Value = Request.QueryString("BatchNo")
        ParamDiscreteValue8.Value = lblBlockCode 'Request.QueryString("BlkType")
        ParamDiscreteValue9.Value = strBlockCode
        ParamDiscreteValue10.Value = Request.QueryString("lblLocation")
        ParamDiscreteValue11.Value = Request.QueryString("lblBatchNo")
        ParamDiscreteValue12.Value = Request.QueryString("lblBlkType")
        ParamDiscreteValue13.Value = lblBlockCode
        ParamDiscreteValue14.Value = Request.QueryString("PlantMaterial")

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

        crvView.ParameterFieldInfo = paramFields
    End Sub
End Class
