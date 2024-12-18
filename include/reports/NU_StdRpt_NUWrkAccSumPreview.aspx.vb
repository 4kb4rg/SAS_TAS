Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information
Imports System.Xml
Imports System.Web.Services
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web

Public Class NU_StdRpt_NUWrkAccSumPreview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents EventData As DataGrid

    Dim objNU As New agri.NU.clsReport()
    Dim objNUSetup As New agri.NU.clsSetup()
    Dim objNUTrx As New agri.NU.clsTrx()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfigSetting As Integer
    Dim strUserLoc As String
    Dim intDDLAccMthFrom As Integer
    Dim intDDLAccYrFrom As Integer
    Dim intDDLAccMthTo As Integer
    Dim intDDLAccYrTo As Integer
    Dim tempLoc As String
    Dim strAccPeriod As String
    Dim strDecimal As String
    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_NUACCMONTH")
        strAccYear = Session("SS_NUACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigSetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
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

            BindReport()
        End If
    End Sub


    Sub BindReport()
        Dim objRptDsSub1 As New DataSet()
        Dim objRptDsSub2 As New DataSet()
        Dim objRptDsSub3 As New DataSet()
        Dim objRptDsSub4 As New DataSet()
        Dim objRptDsSub5 As New DataSet()
        Dim objMapPath As String

        Dim strParam As String
        Dim strAccMonth As String
        Dim strAccYear As String
        Dim strBlkCode As String
        Dim strNBStatus As String
        Dim strSPStatus As String
        Dim strSTStatus As String
        Dim strADStatus As String
        Dim strCTStatus As String
        Dim strNCStatus As String
        Dim strPreNurseryStage As String
        Dim strMainNurseryStage As String

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOpCdSub1_GET As String = "NU_STDRPT_NUWRKACCSUM_SUB1_SP_GET"
        Dim strOpCdSub2_GET As String = "NU_STDRPT_NUWRKACCSUM_SUB2_SP_GET"
        Dim strOpCdSub3_GET As String = "NU_STDRPT_NUWRKACCSUM_SUB3_SP_GET"
        Dim strOpCdSub4_GET As String = "NU_STDRPT_NUWRKACCSUM_SUB4_SP_GET"
        Dim strOpCdSub5_GET As String = "NU_STDRPT_NUWRKACCSUM_SP_GET"

        If Not Request.QueryString("BlkCode") = "" Then
            strBlkCode = Request.QueryString("BlkCode")
        Else
            strBlkCode = "%"
        End If

        strAccMonth = Request.QueryString("DDLAccMth")
        strAccYear = Request.QueryString("DDLAccYr")
        strNBStatus = objNUSetup.EnumNurseryBatchStatus.Active
        strSPStatus = objNUTrx.EnumSeedPlantStatus.Deleted
        strSTStatus = objNUTrx.EnumSeedTransplantStatus.Deleted
        strADStatus = objNUSetup.EnumAccDistStatus.Active
        strCTStatus = objNUSetup.EnumCullTypeStatus.Active
        strNCStatus = objNUTrx.EnumCullStatus.Deleted
        strPreNurseryStage = objNUSetup.EnumNurseryStage.PreNursery
        strMainNurseryStage = objNUSetup.EnumNurseryStage.MainNursery

        strParam = strLocation & "|" & _
                    strAccMonth & "|" & _
                    strAccYear & "|" & _
                    strBlkCode & "|" & _
                    strNBStatus & "|" & _
                    strSPStatus & "|" & _
                    strSTStatus & "|" & _
                    strADStatus & "|" & _
                    strCTStatus & "|" & _
                    strNCStatus & "|" & _
                    strPreNurseryStage & "|" & _
                    strMainNurseryStage & "|" & _
                    Convert.ToString(intConfigSetting)
        Try
            intErrNo = objNU.mtdGetReport_NUWrkAccSum(strOpCdSub1_GET, strOpCdSub2_GET, strOpCdSub3_GET, strOpCdSub4_GET, strOpCdSub5_GET, strParam, objRptDsSub1, objRptDsSub2, objRptDsSub3, objRptDsSub4, objRptDsSub5, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_NU_NUWRKACCSUM_REPORT&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        objRptDsSub1.Tables(0).TableName = "NU_StdRpt_NUWRKACCSUM_Sub1"
        objRptDsSub1.Tables.Add(objRptDsSub2.Tables(0).Copy())
        objRptDsSub1.Tables(1).TableName = "NU_StdRpt_NUWRKACCSUM_Sub2"
        objRptDsSub1.Tables.Add(objRptDsSub3.Tables(0).Copy())
        objRptDsSub1.Tables(2).TableName = "NU_StdRpt_NUWRKACCSUM_Sub3"
        objRptDsSub1.Tables.Add(objRptDsSub4.Tables(0).Copy())
        objRptDsSub1.Tables(3).TableName = "NU_StdRpt_NUWRKACCSUM_Sub4"
        objRptDsSub1.Tables.Add(objRptDsSub5.Tables(0).Copy())
        objRptDsSub1.Tables(4).TableName = "NU_StdRpt_NUWRKACCSUM"


        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\NU_StdRpt_NUWrkAccSum.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDsSub1)

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3

        crvView.Visible = False                         
        crvView.ReportSource = rdCrystalViewer          
        crvView.DataBind()                              

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\NU_StdRpt_NUWrkAccSum.pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/NU_StdRpt_NUWrkAccSum.pdf"">")

        objRptDsSub1 = Nothing
        objRptDsSub2 = Nothing
        objRptDsSub3 = Nothing
        objRptDsSub4 = Nothing
        objRptDsSub5 = Nothing
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

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("ParamCompanyName")
        paramField2 = paramFields.Item("ParamLocation")
        paramField3 = paramFields.Item("ParamUserName")
        paramField4 = paramFields.Item("ParamRptID")
        paramField5 = paramFields.Item("ParamRptName")
        paramField6 = paramFields.Item("ParamAccMonth")
        paramField7 = paramFields.Item("ParamAccYear")
        paramField8 = paramFields.Item("ParamBlkCode")
        paramField9 = paramFields.Item("lblBlkCode")
        paramField10 = paramFields.Item("lblLocation")

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

        ParamDiscreteValue1.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue2.Value = Session("SS_LOC")
        ParamDiscreteValue3.Value = Session("SS_USERNAME")
        ParamDiscreteValue4.Value = Request.QueryString("RptID")
        ParamDiscreteValue5.Value = Request.QueryString("RptName")
        ParamDiscreteValue6.Value = Request.QueryString("DDLAccMth")
        ParamDiscreteValue7.Value = Request.QueryString("DDLAccYr")
        ParamDiscreteValue8.Value = Request.QueryString("BlkCode")
        ParamDiscreteValue9.Value = Request.QueryString("lblBlkCode")
        ParamDiscreteValue10.Value = Request.QueryString("lblLocation")

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

        crvView.ParameterFieldInfo = paramFields
    End Sub
End Class
