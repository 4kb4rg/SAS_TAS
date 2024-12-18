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

Public Class FA_StdRpt_AssetActPreview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objFA As New agri.FA.clsReport()
    Dim objFASetup As New agri.FA.clsSetup()
    Dim objFATrx As New agri.FA.clsTrx()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objAdmin As New agri.Admin.clsShare()

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

    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer

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

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_FAACCMONTH")
        strAccYear = Session("SS_FAACCYEAR")
        strLangCode = Session("SS_LANGCODE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            BindReport()
        End If
    End Sub


    Sub BindReport()

        Dim objRptDs As New DataSet()
        Dim objMapPath As String
        Dim SearchStr As String

        Dim strParam As String
        Dim strAccMonth As String
        Dim strAccYear As String
        Dim strAssetCategory As String
        Dim strAssetCategoryValue As String
        Dim strAssetHeaderCode As String
        Dim strAssetCode As String
        Dim strAADStatus As String
        Dim strADEStatus As String
        Dim strADIStatus As String
        Dim strAWOStatus As String

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOpCd_GET_AssetClass As String = "FA_STDRPT_ASSETACT_SP_GET_ASSETCLASS"
        Dim strOpCd_GET_AssetGrp As String = "FA_STDRPT_ASSETACT_SP_GET_ASSETGRP"

        strAssetCategoryValue = Request.QueryString("AssetCategoryValue")

        If Not Request.QueryString("AssetCategory") = "" Then
            strAssetCategory = Request.QueryString("AssetCategory")
        Else
            strAssetCategory = "%"
        End If

        If Not Request.QueryString("AssetHeaderCode") = "" Then
            strAssetHeaderCode = Request.QueryString("AssetHeaderCode")
        Else
            strAssetHeaderCode = "%"
        End If

        If Not Request.QueryString("AssetCode") = "" Then
            strAssetCode = Request.QueryString("AssetCode")
        Else
            strAssetCode = "%"
        End If

        strAccMonth = Request.QueryString("DDLAccMth")
        strAccYear = Request.QueryString("DDLAccYr")
        strAADStatus = objFATrx.EnumAssetAddStatus.Closed
        strADEStatus = objFATrx.EnumAssetDeprStatus.Closed
        strADIStatus = objFATrx.EnumAssetDispStatus.Closed
        strAWOStatus = objFATrx.EnumAssetWOStatus.Closed

        strParam = strLocation & "|" & _
                    strAccMonth & "|" & _
                    strAccYear & "|" & _
                    strAssetCategory & "|" & _
                    strAssetHeaderCode & "|" & _
                    strAssetCode & "|" & _
                    strAADStatus & "|" & _
                    strADEStatus & "|" & _
                    strADIStatus & "|" & _
                    strAWOStatus

        Try
            If strAssetCategoryValue = objFASetup.EnumAssetCategory.AssetClass Then
                intErrNo = objFA.mtdGetReport_AssetAct(strOpCd_GET_AssetClass, strParam, objRptDs, objMapPath)
            Else
                intErrNo = objFA.mtdGetReport_AssetAct(strOpCd_GET_AssetGrp, strParam, objRptDs, objMapPath)
            End If
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_FA_ASSETACT_REPORT&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try



        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\FA_StdRpt_AssetAct.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3

        crvView.Visible = False                         
        crvView.ReportSource = rdCrystalViewer          
        crvView.DataBind()                              

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\FA_StdRpt_AssetAct.pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/FA_StdRpt_AssetAct.pdf"">")
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
        paramField8 = paramFields.Item("lblLocation")
        paramField9 = paramFields.Item("lblAssetCategory")
        paramField10 = paramFields.Item("ParamAssetCategory")
        paramField11 = paramFields.Item("ParamDecimal")
        paramField12 = paramFields.Item("lblAssetHeaderCode")
        paramField13 = paramFields.Item("ParamAssetHeaderCode")
        paramField14 = paramFields.Item("lblAssetCode")
        paramField15 = paramFields.Item("ParamAssetCode")

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

        ParamDiscreteValue1.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue2.Value = Session("SS_LOC")
        ParamDiscreteValue3.Value = Session("SS_USERNAME")
        ParamDiscreteValue4.Value = Request.QueryString("RptID")
        ParamDiscreteValue5.Value = Request.QueryString("RptName")
        ParamDiscreteValue6.Value = Request.QueryString("DDLAccMth")
        ParamDiscreteValue7.Value = Request.QueryString("DDLAccYr")
        ParamDiscreteValue8.Value = Request.QueryString("lblLocation")
        ParamDiscreteValue9.Value = Request.QueryString("lblAssetCategory")
        ParamDiscreteValue10.Value = Request.QueryString("AssetCategory")
        ParamDiscreteValue11.Value = Request.QueryString("Decimal")
        ParamDiscreteValue12.Value = Request.QueryString("lblAssetHeaderCode")
        ParamDiscreteValue13.Value = Request.QueryString("AssetHeaderCode")
        ParamDiscreteValue14.Value = Request.QueryString("lblAssetCode")
        ParamDiscreteValue15.Value = Request.QueryString("AssetCode")

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

        crvView.ParameterFieldInfo = paramFields
    End Sub
End Class
