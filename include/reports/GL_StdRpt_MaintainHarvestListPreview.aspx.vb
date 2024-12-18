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
Imports System.Collections

Public Class GL_StdRpt_MaintainHarvestListPreview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objGLRpt As New agri.GL.clsReport()
    Dim objPDTrx As New agri.PD.clsTrx()

    Dim strUserId As String
    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strLocationName As String
    Dim strPrintedBy As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfigsetting As Integer

    Dim strRptId As String
    Dim strRptName As String
    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim intSelDecimal As Integer
    Dim strSelSuppress As String
    Dim strSummarize As String
    Dim strSelLocation As String

    Dim strSrchActGrpCode As String
    Dim strSrchBlkType As String
    Dim strSrchBlkGrpCode As String
    Dim strSrchBlkCode As String
    Dim strSrchSubBlkCode As String
    Dim strCostIsBlock As String
    Dim strYieldIsBlock As String

    Dim strFieldActGrpCode As String
    Dim strValueActGrpCode As String
    Dim strFieldBlkCode As String
    Dim strValueBlkCode As String
    Dim strBlkTag As String


    Dim rdCrystalViewer As ReportDocument
    Dim intErrNo As Integer

    Dim tempActGrpCode As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strUserId = Session("SS_USERID")
        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strLocationName = Session("SS_LOCATIONNAME")
        strPrintedBy = Session("SS_USERNAME")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            crvView.Visible = False
            strRptId = Trim(Request.QueryString("RptId"))
            strRptName = Trim(Request.QueryString("RptName"))
            strSelAccMonth = Request.QueryString("DDLAccMth")
            strSelAccYear = Request.QueryString("DDLAccYr")
            intSelDecimal = CInt(Request.QueryString("Decimal"))
            strSelSuppress = Request.QueryString("Supp")
            strSummarize = Trim(Request.QueryString("Sum"))

            If Right(Request.QueryString("Location"), 1) = "," Then
                strSelLocation = Left(Request.QueryString("Location"), Len(Request.QueryString("Location")) - 1)
            Else
                strSelLocation = Trim(Request.QueryString("Location"))
            End If

            strSrchActGrpCode = Trim(Request.QueryString("ActGrpCode"))
            strSrchBlkType = Trim(Request.QueryString("BlkType"))
            strSrchBlkGrpCode = Trim(Request.QueryString("BlkGrp"))
            strSrchBlkCode = Trim(Request.QueryString("BlkCode"))
            strSrchSubBlkCode = Trim(Request.QueryString("SubBlkCode"))
            strCostIsBlock = Trim(Request.QueryString("CostIsBlock"))
            strYieldIsBlock = Trim(Request.QueryString("YieldIsBlock"))

            Session("SS_lblBlkType") = Trim(Request.QueryString("lblBlkType"))
            Session("SS_lblBlkGrp") = Trim(Request.QueryString("lblBlkGrp"))
            Session("SS_lblBlkCode") = Trim(Request.QueryString("lblBlkCode"))
            Session("SS_lblSubBlkCode") = Trim(Request.QueryString("lblSubBlkCode"))
            Session("SS_lblActGrpCode") = Trim(Request.QueryString("lblActGrpCode"))
            Session("SS_lblLocation") = Trim(Request.QueryString("lblLocation"))

            strFieldActGrpCode = Session("SS_lblActGrpCode")

            If LCase(strSrchBlkType) = "subblkCode" Then
                strFieldBlkCode = Session("SS_lblSubBlkCode")
                strValueBlkCode = strSrchSubBlkCode
            ElseIf LCase(strSrchBlkType) = "blkcode" Then
                strFieldBlkCode = Session("SS_lblBlkCode")
                strValueBlkCode = strSrchBlkCode
            Else
                strFieldBlkCode = Session("SS_lblBlkGrp")
                strValueBlkCode = strSrchBlkGrpCode
            End If

            If LCase(strCostIsBlock) = "false" Then
                strBlkTag = Trim(Request.QueryString("SubBlkTag"))
            Else
                strBlkTag = Trim(Request.QueryString("BlkTag"))
            End If

            BindReport()
        End If

    End Sub

    Sub BindReport()
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim crExportOptions As ExportOptions
        Dim strRptPrefix As String
        Dim objRptDs As New DataSet()
        Dim objMapPath As String
        Dim strOpCd As String
        Dim strOpCdMainSum As String = "GL_STDRPT_MAINT_HARV_SUMM_GETSP"
        Dim strOpCdMainDet As String = "GL_STDRPT_MAINT_HARV_DET_GETSP"
        Dim strOpCdSub As String = "GL_STDRPT_MAINT_HARV_SUMM_SUB_GETSP"
        Dim strParam As String

        Dim strRptTableName As String
        Dim strTableName As String
        Dim objFTPFolder As String

        strParam = strSelLocation & "|" & _
                    strSelAccMonth & "|" & _
                    strSelAccYear & "|" & _
                    strSrchBlkGrpCode & "|" & _
                    strSrchBlkCode & "|" & _
                    strSrchSubBlkCode & "|" & _
                    strCostIsBlock & "|" & _
                    Convert.ToString(objPDTrx.EnumEstateYieldStatus.Closed) & "|" & _
                    strSrchBlkType & "|" & _
                    strSrchActGrpCode & "|" & _
                    strYieldIsBlock & "|" & _
                    Convert.ToString(intConfigSetting)

        If LCase(strSummarize) = "yes" Then
            strOpCd = strOpCdMainSum & "|MaintHarvSum" & Chr(9) & _
                      strOpCdSub & "|MaintHarvSumSub"
            strRptPrefix = "GL_StdRpt_MaintHarvSumm"
            strRptTableName = "MaintHarvSum"
            strTableName = "GL_MAINTHARVSUM_RPT"
        Else
            strOpCd = strOpCdMainDet & "|MaintHarvDet" & Chr(9) & _
                      strOpCdSub & "|MaintHarvSumSub"
            strRptPrefix = "GL_StdRpt_MaintHarvDet"
            strRptTableName = "MaintHarvDet"
            strTableName = "GL_MAINTHARVDET_RPT"
        End If

        Try
            intErrNo = objGLRpt.mtdGetReport_MaintHarv(strOpCd, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strParam, _
                                                        objRptDs, _
                                                        objMapPath, objFTPFolder)

        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_STDRPT_MAINT_HARV_SUMM_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try


        rdCrystalViewer = New ReportDocument()
        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strRptPrefix & ".rpt", OpenReportMethod.OpenReportByTempCopy)

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3

        PassParam()
        Dim myTable As CrystalDecisions.CrystalReports.Engine.Table
        Dim myLogin As CrystalDecisions.Shared.TableLogOnInfo

        Dim MySection As CrystalDecisions.CrystalReports.Engine.Section
        Dim repObject As CrystalDecisions.CrystalReports.Engine.ReportObject

        For Each myTable In rdCrystalViewer.Database.Tables
            If myTable.Name = strRptTableName Then
            myLogin = myTable.LogOnInfo
            Try
                intErrNo = objGLRpt.mtdGet_DBConnObj(myLogin)
            Catch Exp As System.Exception
                Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_MAINTHARV_RPT_GET&errmesg=" & Exp.ToString() & "&redirect=")
            End Try

            myTable.ApplyLogOnInfo(myLogin)
            myTable.Location = myLogin.ConnectionInfo.DatabaseName & ".dbo." & strTableName
            ElseIf myTable.Name = "MaintHarvSumSub" Then
                myLogin = myTable.LogOnInfo
            Try
                intErrNo = objGLRpt.mtdGet_DBConnObj(myLogin)
            Catch Exp As System.Exception
                Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_MAINTHARV_RPT_GET&errmesg=" & Exp.ToString() & "&redirect=")
            End Try

            myTable.ApplyLogOnInfo(myLogin)
            myTable.Location = myLogin.ConnectionInfo.DatabaseName & ".dbo.GL_MAINTHARVSUMSUB_RPT"
            End If
        Next

        For Each MySection in rdCrystalViewer.ReportDefinition.Sections
            For Each repObject in MySection.ReportObjects
                If repObject.Kind = ReportObjectKind.SubreportObject
			        Dim subReport As SubreportObject = CType(repObject, SubreportObject)
			        Dim subDocument As ReportDocument = subReport.OpenSubreport(subReport.SubreportName)

                    For Each myTable In subDocument.Database.Tables

                        If myTable.Name = strRptTableName Then
                            myLogin = myTable.LogOnInfo
                            Try
                                intErrNo = objGLRpt.mtdGet_DBConnObj(myLogin)
                            Catch Exp As System.Exception
                                Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_MAINTHARV_RPT_GET&errmesg=" & Exp.ToString() & "&redirect=")
                            End Try

                            myTable.ApplyLogOnInfo(myLogin)
                            myTable.Location = myLogin.ConnectionInfo.DatabaseName & ".dbo." & strTableName
                        ElseIf myTable.Name = "MaintHarvSumSub" Then
                            myLogin = myTable.LogOnInfo
                            Try
                                intErrNo = objGLRpt.mtdGet_DBConnObj(myLogin)
                            Catch Exp As System.Exception
                                Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_MAINTHARV_RPT_GET&errmesg=" & Exp.ToString() & "&redirect=")
                            End Try

                            myTable.ApplyLogOnInfo(myLogin)
                            myTable.Location = myLogin.ConnectionInfo.DatabaseName & ".dbo.GL_MAINTHARVSUMSUB_RPT"
                        End If
                    Next
                Else

                End If
            Next
        Next

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        'crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strRptPrefix & ".pdf"
        crDiskFileDestinationOptions.DiskFileName = objFTPFolder & strRptPrefix & ".pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        rdCrystalViewer.Close()
        rdCrystalViewer.Dispose()

        Dim strUrl As String
        strUrl = objFTPFolder.Substring(InStr(objFTPFolder, "ftp") - 1, Len(objFTPFolder) - InStr(objFTPFolder, "ftp") + 1)
        strUrl = Replace(strUrl, "\", "/")
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../" & strUrl & strRptPrefix & ".pdf"">")
        'Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strRptPrefix & ".pdf"">")

        objRptDs = Nothing
    End Sub

    Sub PassParam()

        Dim ParamFieldDefs As ParameterFieldDefinitions
        Dim ParamFieldDef1 As ParameterFieldDefinition
        Dim ParamFieldDef2 As ParameterFieldDefinition
        Dim ParamFieldDef3 As ParameterFieldDefinition
        Dim ParamFieldDef4 As ParameterFieldDefinition
        Dim ParamFieldDef5 As ParameterFieldDefinition
        Dim ParamFieldDef6 As ParameterFieldDefinition
        Dim ParamFieldDef7 As ParameterFieldDefinition
        Dim ParamFieldDef8 As ParameterFieldDefinition
        Dim ParamFieldDef9 As ParameterFieldDefinition
        Dim ParamFieldDef10 As ParameterFieldDefinition
        Dim ParamFieldDef11 As ParameterFieldDefinition
        Dim ParamFieldDef12 As ParameterFieldDefinition
        Dim ParamFieldDef13 As ParameterFieldDefinition
        Dim ParamFieldDef14 As ParameterFieldDefinition
        Dim ParamFieldDef15 As ParameterFieldDefinition
        Dim ParamFieldDef16 As ParameterFieldDefinition
        Dim ParamFieldDef17 As ParameterFieldDefinition
        Dim ParamFieldDef18 As ParameterFieldDefinition

        Dim ParameterValues1 As New ParameterValues()
        Dim ParameterValues2 As New ParameterValues()
        Dim ParameterValues3 As New ParameterValues()
        Dim ParameterValues4 As New ParameterValues()
        Dim ParameterValues5 As New ParameterValues()
        Dim ParameterValues6 As New ParameterValues()
        Dim ParameterValues7 As New ParameterValues()
        Dim ParameterValues8 As New ParameterValues()
        Dim ParameterValues9 As New ParameterValues()
        Dim ParameterValues10 As New ParameterValues()
        Dim ParameterValues11 As New ParameterValues()
        Dim ParameterValues12 As New ParameterValues()
        Dim ParameterValues13 As New ParameterValues()
        Dim ParameterValues14 As New ParameterValues()
        Dim ParameterValues15 As New ParameterValues()
        Dim ParameterValues16 As New ParameterValues()
        Dim ParameterValues17 As New ParameterValues()
        Dim ParameterValues18 As New ParameterValues()

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

        strSelLocation = Replace(strSelLocation, "','", ", ")
        strValueActGrpCode = Replace(strSrchActGrpCode, "','", ", ")

        ParamDiscreteValue1.Value = strSelLocation
        ParamDiscreteValue2.Value = strSelAccMonth
        ParamDiscreteValue3.Value = strSelAccYear
        ParamDiscreteValue4.Value = intSelDecimal
        ParamDiscreteValue5.Value = strSummarize
        ParamDiscreteValue6.Value = strSelSuppress
        ParamDiscreteValue7.Value = strRptId
        ParamDiscreteValue8.Value = strRptName
        ParamDiscreteValue9.Value = strCompanyName
        ParamDiscreteValue10.Value = strPrintedBy
        ParamDiscreteValue11.Value = Session("SS_LBLLOCATION")
        ParamDiscreteValue12.Value = UCase(strBlkTag)
        ParamDiscreteValue13.Value = strFieldBlkCode
        ParamDiscreteValue14.Value = strFieldActGrpCode
        ParamDiscreteValue15.Value = strValueBlkCode
        ParamDiscreteValue16.Value = strValueActGrpCode
        ParamDiscreteValue17.Value = intSelDecimal
        ParamDiscreteValue18.Value = strSelSuppress

        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        ParamFieldDef1 = ParamFieldDefs.Item("ParamLocation")
        ParamFieldDef2 = ParamFieldDefs.Item("ParamAccMonth")
        ParamFieldDef3 = ParamFieldDefs.Item("ParamAccYear")
        ParamFieldDef4 = ParamFieldDefs.Item("ParamDecimal")
        ParamFieldDef5 = ParamFieldDefs.Item("ParamSummarize")
        ParamFieldDef6 = ParamFieldDefs.Item("ParamSuppress")
        ParamFieldDef7 = ParamFieldDefs.Item("ParamRptID")
        ParamFieldDef8 = ParamFieldDefs.Item("ParamRptName")
        ParamFieldDef9 = ParamFieldDefs.Item("ParamCompanyName")
        ParamFieldDef10 = ParamFieldDefs.Item("ParamUserName")
        ParamFieldDef11 = ParamFieldDefs.Item("lblLocation")
        ParamFieldDef12 = ParamFieldDefs.Item("lblBlock")
        ParamFieldDef13 = ParamFieldDefs.Item("FieldBlkCode")
        ParamFieldDef14 = ParamFieldDefs.Item("FieldActGrpCode")
        ParamFieldDef15 = ParamFieldDefs.Item("ValueBlkCode")
        ParamFieldDef16 = ParamFieldDefs.Item("ValueActGrpCode")
        ParamFieldDef17 = ParamFieldDefs.Item("SubDecimal")
        ParamFieldDef18 = ParamFieldDefs.Item("SubSuppress")

        ParameterValues1 = ParamFieldDef1.CurrentValues
        ParameterValues2 = ParamFieldDef2.CurrentValues
        ParameterValues3 = ParamFieldDef3.CurrentValues
        ParameterValues4 = ParamFieldDef4.CurrentValues
        ParameterValues5 = ParamFieldDef5.CurrentValues
        ParameterValues6 = ParamFieldDef6.CurrentValues
        ParameterValues7 = ParamFieldDef7.CurrentValues
        ParameterValues8 = ParamFieldDef8.CurrentValues
        ParameterValues9 = ParamFieldDef9.CurrentValues
        ParameterValues10 = ParamFieldDef10.CurrentValues
        ParameterValues11 = ParamFieldDef11.CurrentValues
        ParameterValues12 = ParamFieldDef12.CurrentValues
        ParameterValues13 = ParamFieldDef13.CurrentValues
        ParameterValues14 = ParamFieldDef14.CurrentValues
        ParameterValues15 = ParamFieldDef15.CurrentValues
        ParameterValues16 = ParamFieldDef16.CurrentValues
        ParameterValues17 = ParamFieldDef17.CurrentValues
        ParameterValues18 = ParamFieldDef18.CurrentValues

        ParameterValues1.Add(ParamDiscreteValue1)
        ParameterValues2.Add(ParamDiscreteValue2)
        ParameterValues3.Add(ParamDiscreteValue3)
        ParameterValues4.Add(ParamDiscreteValue4)
        ParameterValues5.Add(ParamDiscreteValue5)
        ParameterValues6.Add(ParamDiscreteValue6)
        ParameterValues7.Add(ParamDiscreteValue7)
        ParameterValues8.Add(ParamDiscreteValue8)
        ParameterValues9.Add(ParamDiscreteValue9)
        ParameterValues10.Add(ParamDiscreteValue10)
        ParameterValues11.Add(ParamDiscreteValue11)
        ParameterValues12.Add(ParamDiscreteValue12)
        ParameterValues13.Add(ParamDiscreteValue13)
        ParameterValues14.Add(ParamDiscreteValue14)
        ParameterValues15.Add(ParamDiscreteValue15)
        ParameterValues16.Add(ParamDiscreteValue16)
        ParameterValues17.Add(ParamDiscreteValue17)
        ParameterValues18.Add(ParamDiscreteValue18)

        ParamFieldDef1.ApplyCurrentValues(ParameterValues1)
        ParamFieldDef2.ApplyCurrentValues(ParameterValues2)
        ParamFieldDef3.ApplyCurrentValues(ParameterValues3)
        ParamFieldDef4.ApplyCurrentValues(ParameterValues4)
        ParamFieldDef5.ApplyCurrentValues(ParameterValues5)
        ParamFieldDef6.ApplyCurrentValues(ParameterValues6)
        ParamFieldDef7.ApplyCurrentValues(ParameterValues7)
        ParamFieldDef8.ApplyCurrentValues(ParameterValues8)
        ParamFieldDef9.ApplyCurrentValues(ParameterValues9)
        ParamFieldDef10.ApplyCurrentValues(ParameterValues10)
        ParamFieldDef11.ApplyCurrentValues(ParameterValues11)
        ParamFieldDef12.ApplyCurrentValues(ParameterValues12)
        ParamFieldDef13.ApplyCurrentValues(ParameterValues13)
        ParamFieldDef14.ApplyCurrentValues(ParameterValues14)
        ParamFieldDef15.ApplyCurrentValues(ParameterValues15)
        ParamFieldDef16.ApplyCurrentValues(ParameterValues16)
        ParamFieldDef17.ApplyCurrentValues(ParameterValues17)
        ParamFieldDef18.ApplyCurrentValues(ParameterValues18)
    End Sub
End Class
