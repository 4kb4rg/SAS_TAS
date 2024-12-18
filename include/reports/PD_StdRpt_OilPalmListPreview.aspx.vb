Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports System.Xml
Imports System.Web.Services
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web

Public Class PD_StdRpt_OilPalmList_Preview : Inherits Page
    Dim objPD As New agri.PD.clsReport()
    Dim objPDTrx As New agri.PD.clsTrx()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objSysCfg As New agri.PWSystem.clsConfig()

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents EventData As DataGrid

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfigsetting As Integer
    Dim strUserLoc As String
    Dim strBlkType As String
    Dim strAccMth As String
    Dim strAccYr As String
    Dim strDDLAccMth As String
    Dim strDDLAccYr As String
    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer
    Dim intDecimal As Integer
    Dim strDateFrom As String
    Dim strDateTo As String
    Dim strStatus As String
    Dim strStatusValue As String
    Dim strBlockTag As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_PDACCMONTH")
        strAccYear = Session("SS_PDACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            crvView.Visible = False

            If Right(Request.QueryString("Location"), 1) = "," Then
                strUserLoc = Left(Request.QueryString("Location"), Len(Request.QueryString("Location")) - 1)
            Else
                strUserLoc = Trim(Request.QueryString("Location"))
            End If

            strDDLAccMth = Request.QueryString("DDLAccMth")
            strDDLAccYr = Request.QueryString("DDLAccYr")
            strBlkType = Request.QueryString("BlkType")
            intDecimal = Request.QueryString("Decimal")
            Session("SS_lblBlkType") = Request.QueryString("lblBlkType")
            Session("SS_lblBlkGrp") = Request.QueryString("lblBlkGrp")
            Session("SS_lblBlkCode") = Request.QueryString("lblBlkCode")
            Session("SS_lblSubBlkCode") = Request.QueryString("lblSubBlkCode")
            Session("SS_lblLocation") = Request.QueryString("lblLocation")
            Session("SS_RptID") = Request.QueryString("RptID")
            Session("SS_RptName") = Request.QueryString("RptName")
            Session("SS_AccMth") = Request.QueryString("DDLAccMth")
            Session("SS_AccYr") = Request.QueryString("DDLAccYr")
            Session("SS_YieldIDFrom") = Request.QueryString("YieldIDFrom")
            Session("SS_YieldIDTo") = Request.QueryString("YieldIDTo")
            Session("SS_BlkType") = Request.QueryString("BlkType")
            Session("SS_BlkGrp") = Request.QueryString("BlkGrp")
            Session("SS_BlkCode") = Request.QueryString("BlkCode")
            Session("SS_SubBlkCode") = Request.QueryString("SubBlkCode")
            Session("SS_Decimal") = Request.QueryString("Decimal")

            strDateFrom = Trim(Request.QueryString("DateFrom"))
            strDateTo = Trim(Request.QueryString("DateTo"))
            strStatus = Trim(Request.QueryString("Status"))
            strStatusValue = Trim(Request.QueryString("StatusValue"))
            strBlockTag = Trim(Request.QueryString("BlockTag"))

            BindReport()
        End If
    End Sub


    Sub BindReport()
        Dim objMapPath As String
        Dim objRptDs As New DataSet()
        Dim intCnt As Integer
        Dim objDsYTDAccPeriod As String
        Dim intCntBlk As Integer
        Dim SearchStr As String
        Dim tempLoc As String
        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim strOpCd_OilPalmList_BlkGrp_GET As String = "PD_STDRPT_OILPALMLIST_BLKGRP_GET"
        Dim strOpCd_OilPalmList_BlkYeild_GET As String = "PD_STDRPT_OILPALMLIST_BLKYIELD_GET"
        Dim strOpCd_OilPalmList_Blk_GET As String = "PD_STDRPT_OILPALMLIST_BLOCK_GET"
        Dim strOpCd_OilPalmList_SubBlk_GET As String = "PD_STDRPT_OILPALMLIST_SUBBLOCK_GET"
        Dim strParam As String
        Dim strOpCd As String
        Dim strLocCode As String
        Dim strBlkCode As String
        Dim strEmpCode As String
        Dim NormStr As String = "AND EY.LocCode IN ('" & strUserLoc & "') AND EY.AccMonth = '" & strDDLAccMth & "' AND EY.AccYear = '" & strDDLAccYr & "' "

        tempLoc = Request.QueryString("Location")
        If Right(tempLoc, 1) = "," Then
            Session("SS_LOC") = Left(tempLoc, Len(tempLoc) - 1)
        Else
            Session("SS_LOC") = tempLoc.Replace("','", ", ")
        End If

        If strDateFrom <> "" Then
            NormStr += "AND DateDiff(Day, '" & strDateFrom & "', EY.YieldDate) >= 0 "
        End If
        If strDateTo <> "" Then
            NormStr += "AND DateDiff(Day, '" & strDateTo & "', EY.YieldDate) <= 0 "
        End If

        If (Len(Trim(Request.QueryString("YieldIDFrom"))) > 0) And (Len(Trim(Request.QueryString("YieldIDTo"))) > 0) Then
            NormStr += "AND EY.EstateYieldID Between '" & Request.QueryString("YieldIDFrom") & "' AND '" & Request.QueryString("YieldIDTo") & "' "
        ElseIf (Len(Trim(Request.QueryString("YieldIDFrom"))) > 0) Then
            NormStr += "AND EY.EstateYieldID LIKE '" & Request.QueryString("YieldIDFrom") & "' "
        End If

        If strStatusValue <> "0"
           NormStr += "AND EY.Status = '" & strStatusValue & "' "
        End If

        If strBlkType = "BlkGrp" Then
            If Not Request.QueryString("BlkGrp") = "" Then
                SearchStr = "AND BLK.BlkGrpCode LIKE '" & Request.QueryString("BlkGrp") & "' "
            End If
            Session("SS_BLKGRPHEADER") = "BlkGrp"
            strOpCd = strOpCd_OilPalmList_BlkGrp_GET
        ElseIf strBlkType = "BlkCode" Then
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockYieldLevel), intConfigSetting) = True Then
                If Not Request.QueryString("BlkCode") = "" Then
                    SearchStr = "AND EY.BlkCode LIKE '" & Request.QueryString("BlkCode") & "' "
                End If
                strOpCd = strOpCd_OilPalmList_BlkYeild_GET
            Else
                If Not Request.QueryString("BlkCode") = "" Then                    
                    SearchStr = "AND BLK.BlkCode LIKE '" & Request.QueryString("BlkCode") & "' "
                End If
                strOpCd = strOpCd_OilPalmList_Blk_GET
            End If
            Session("SS_BLKHEADER") = "Block"
            
        ElseIf strBlkType = "SubBlkCode" Then
            If Not Request.QueryString("SubBlkCode") = "" Then
                SearchStr = "AND EY.BlkCode LIKE '" & Request.QueryString("SubBlkCode") & "' "
            End If
            Session("SS_SUBBLKHEADER") = "SubBlock"
            strOpCd = strOpCd_OilPalmList_SubBlk_GET
        End If

        strParam = NormStr & SearchStr

       

        Try
            intErrNo = objPD.mtdGetReport_OilPalmList(strOpCd, strParam, objRptDs, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_OILPALM_LIST_REPORT&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

    
        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\PD_StdRpt_OilPalmList.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3

        crvView.Visible = False                          
        crvView.ReportSource = rdCrystalViewer           
        crvView.DataBind()                              

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\PD_StdRpt_OilPalmList.pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/PD_StdRpt_OilPalmList.pdf"">")

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
        Dim ParamFieldDef19 As ParameterFieldDefinition
        Dim ParamFieldDef20 As ParameterFieldDefinition
        Dim ParamFieldDef21 As ParameterFieldDefinition
        Dim ParamFieldDef22 As ParameterFieldDefinition
        Dim ParamFieldDef23 As ParameterFieldDefinition

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
        Dim ParameterValues19 As New ParameterValues()
        Dim ParameterValues20 As New ParameterValues()
        Dim ParameterValues21 As New ParameterValues()
        Dim ParameterValues22 As New ParameterValues()
        Dim ParameterValues23 As New ParameterValues()

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

        ParamDiscreteValue1.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue2.Value = Session("SS_LOC")
        ParamDiscreteValue3.Value = Session("SS_USERNAME")
        ParamDiscreteValue4.Value = Session("SS_AccMth")
        ParamDiscreteValue5.Value = Session("SS_AccYr")
        ParamDiscreteValue6.Value = Session("SS_Decimal")
        ParamDiscreteValue7.Value = Session("SS_BlkGrp")
        ParamDiscreteValue8.Value = Session("SS_BlkCode")
        ParamDiscreteValue9.Value = Session("SS_SubBlkCode")

        If strBlkType = "BlkGrp" Then
            ParamDiscreteValue10.Value = Session("SS_BLKGRPHEADER")
        ElseIf strBlkType = "BlkCode" Then
            ParamDiscreteValue10.Value = Session("SS_BLKHEADER")
        ElseIf strBlkType = "SubBlkCode" Then
            ParamDiscreteValue10.Value = Session("SS_SUBBLKHEADER")
        End If

        ParamDiscreteValue11.Value = strStatus

        ParamDiscreteValue12.Value = Session("SS_RPTID")
        ParamDiscreteValue13.Value = Session("SS_RPTNAME")
        ParamDiscreteValue14.Value = Session("SS_LBLLOCATION")
        ParamDiscreteValue15.Value = Session("SS_BLKTYPE")
        ParamDiscreteValue16.Value = Session("SS_LBLBLKGRP")
        ParamDiscreteValue17.Value = Session("SS_LBLBLKCODE")
        ParamDiscreteValue18.Value = Session("SS_LBLSUBBLKCODE")
        ParamDiscreteValue19.Value = Session("SS_YieldIDFrom")
        ParamDiscreteValue20.Value = Session("SS_YieldIDTo")
        ParamDiscreteValue21.Value = strDateFrom
        ParamDiscreteValue22.Value = strDateTo
        ParamDiscreteValue23.Value = strBlockTag
        

        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        ParamFieldDef1 = ParamFieldDefs.Item("ParamCompanyName")
        ParamFieldDef2 = ParamFieldDefs.Item("ParamLocation")
        ParamFieldDef3 = ParamFieldDefs.Item("ParamUserName")
        ParamFieldDef4 = ParamFieldDefs.Item("ParamAccMonth")
        ParamFieldDef5 = ParamFieldDefs.Item("ParamAccYear")
        ParamFieldDef6 = ParamFieldDefs.Item("ParamDecimal")
        ParamFieldDef7 = ParamFieldDefs.Item("ParamBlkGrp")
        ParamFieldDef8 = ParamFieldDefs.Item("ParamBlkCode")
        ParamFieldDef9 = ParamFieldDefs.Item("ParamSubBlkCode")
        ParamFieldDef10 = ParamFieldDefs.Item("ParamBlkOrSubBlk")
        ParamFieldDef11 = ParamFieldDefs.Item("ParamStatus")
        ParamFieldDef12 = ParamFieldDefs.Item("ParamRptID")
        ParamFieldDef13 = ParamFieldDefs.Item("ParamRptName")
        ParamFieldDef14 = ParamFieldDefs.Item("lblLocation")
        ParamFieldDef15 = ParamFieldDefs.Item("ParamBlkType")
        ParamFieldDef16 = ParamFieldDefs.Item("lblBlkGrp")
        ParamFieldDef17 = ParamFieldDefs.Item("lblBlkCode")
        ParamFieldDef18 = ParamFieldDefs.Item("lblSubBlkCode")
        ParamFieldDef19 = ParamFieldDefs.Item("ParamYieldIDFrom")
        ParamFieldDef20 = ParamFieldDefs.Item("ParamYieldIDTo")
        ParamFieldDef21 = ParamFieldDefs.Item("ParamDateFrom")
        ParamFieldDef22 = ParamFieldDefs.Item("ParamDateTo")
        ParamFieldDef23 = ParamFieldDefs.Item("lblBlockTag")


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
        ParameterValues19 = ParamFieldDef19.CurrentValues
        ParameterValues20 = ParamFieldDef20.CurrentValues
        ParameterValues21 = ParamFieldDef21.CurrentValues
        ParameterValues22 = ParamFieldDef22.CurrentValues
        ParameterValues23 = ParamFieldDef23.CurrentValues

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
        ParameterValues19.Add(ParamDiscreteValue19)
        ParameterValues20.Add(ParamDiscreteValue20)
        ParameterValues21.Add(ParamDiscreteValue21)
        ParameterValues22.Add(ParamDiscreteValue22)
        ParameterValues23.Add(ParamDiscreteValue23)

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
        ParamFieldDef19.ApplyCurrentValues(ParameterValues19)
        ParamFieldDef20.ApplyCurrentValues(ParameterValues20)
        ParamFieldDef21.ApplyCurrentValues(ParameterValues21)
        ParamFieldDef22.ApplyCurrentValues(ParameterValues22)
        ParamFieldDef23.ApplyCurrentValues(ParameterValues23)

    End Sub
End Class
