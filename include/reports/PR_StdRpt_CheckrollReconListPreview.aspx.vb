Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic
Imports System.Xml
Imports System.Web.Services
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web

Public Class PR_StdRpt_CheckRollReconList_Preview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objPR As New agri.PR.clsReport()
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objPWSysConfig As New agri.PWSystem.clsConfig()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strUserLoc As String

    Dim strDDLAccMth As String
    Dim strDDLAccYr As String
    Dim strAccMth As String
    Dim strAccYr As String

    Dim rdCrystalViewer As ReportDocument
    Dim intErrNo As Integer
    Dim dr As DataRow

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        crvView.Visible = False

        If Right(Request.QueryString("Location"), 1) = "," Then
            strUserLoc = Left(Request.QueryString("Location"), Len(Request.QueryString("Location")) - 1)
        Else
            strUserLoc = Trim(Request.QueryString("Location"))
        End If

        strDDLAccMth = Request.QueryString("DDLAccMth")
        strDDLAccYr = Request.QueryString("DDLAccYr")

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLangCode = Session("SS_LANGCODE")

        Session("SS_LBLLOCATION") = Request.QueryString("lblLocation")
        Session("SS_AccMth") = Request.QueryString("DDLAccMth")
        Session("SS_AccYr") = Request.QueryString("DDLAccYr")
        Session("SS_Decimal") = Request.QueryString("Decimal")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            BindReport()
        End If
    End Sub

    Sub BindReport()

        Dim objRptNameDs As New DataSet()
        Dim objRptUserLoc As New DataSet()
        Dim objMapPath As String

        Dim objRptDsAllow As New DataSet()
        Dim objRptDsPayBF As New DataSet()
        Dim objRptDsDeduct As New DataSet()
        Dim objDSCopy As New DataSet()

        Dim objRptDsCRReconAllowAmt As New DataSet()

        Dim objRptDsCRReconPayBFAmt As New DataSet()
        Dim objRptDsCRReconDeductAmt As New DataSet()

        Dim arrParamMth As Array
        Dim arrParamYr As Array

        Dim arrLocCode As Array

        Dim intCnt As Integer
        Dim intUserLoc As Integer
        Dim intCntAccPeriod As Integer
        Dim intDecimal As Integer
        Dim intCntWhole As Integer

        Dim strUSAccMonth As String
        Dim strUSAccYear As String
        Dim strUSLocName As String
        Dim strUSAccPeriod As String

        Dim tempUSAccMonth As String
        Dim tempUSAccYear As String
        Dim tempLoc As String
        Dim tempUserLocName As String

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOpCd_CRRecon_Allow_GET As String = "PR_STDRPT_CHECKROLLRECON_ALLOW_GET"
        Dim strOpCd_CRRecon_Deduct_GET As String = "PR_STDRPT_CHECKROLLRECON_DEDUCT_GET"

        Dim strOpCd_CRRecon_Allow_Amt_GET As String = "PR_STDRPT_CHECKROLLRECON_ALLOW_AMT_GET"
        Dim strOpCd_CRRecon_Deduct_Amt_GET As String = "PR_STDRPT_CHECKROLLRECON_DEDUCT_AMT_GET"

        Dim strOpCd_RptName_GET As String = "PR_STDRPT_NAME_GET"
        Dim strOppCd_UserLoc_GET As String = "PR_STDRPT_USERLOCATION_GET"

        Dim strParamRptName As String
        Dim strParamUserLoc As String

        Dim strParamCRReconAllow As String
        Dim strParamCRReconPayDeduct As String

        Dim strParamCRReconAllowAmt As String
        Dim strParamCRReconPayBFAmt As String
        Dim strParamCRReconDeductAmt As String

        Dim intCntLoc As Integer
        Dim intCntAllow As Integer
        Dim intCntPayBF As Integer
        Dim intCntDeduct As Integer

        Dim strADCodeAllow As String
        Dim strADCodePayBF As String
        Dim strADCodeDeduct As String

        Dim dblAllowTotalAmt As Decimal
        Dim dblPayBFTotalAmt As Decimal
        Dim dblDeductTotalAmt As Decimal
        Dim dblGrandTotalAmt As Decimal

        Dim AllowStr As String = "AND AD.ADType = '" & objPRSetup.EnumADType.Allowance & "'"
        Dim PayDeductStr As String = "AND AD.ADType = '" & objPRSetup.EnumADType.Deduction & "'"

        tempLoc = Request.QueryString("Location")
        If Right(tempLoc, 1) = "," Then
            Session("SS_LOC") = Left(tempLoc, Len(tempLoc) - 1)
        Else
            Session("SS_LOC") = tempLoc.Replace("','", ", ")
        End If

        arrLocCode = Split(Request.QueryString("Location"), "','")
        For intCntLoc = 0 To arrLocCode.GetUpperBound(0)

            strParamCRReconAllow = "|" & arrLocCode(intCntLoc) & "|" & strDDLAccMth & "|" & strDDLAccYr & "|" & AllowStr
            Try
                intErrNo = objPR.mtdGetReport_CheckRollReconList(strOpCd_CRRecon_Allow_GET, _
                                                                strCompany, _
                                                                strLocation, _
                                                                strUserId, _
                                                                strAccMonth, _
                                                                strAccYear, _
                                                                strParamCRReconAllow, _
                                                                objRptDsAllow, _
                                                                objMapPath)
            Catch Exp As System.Exception
                Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_CHECKROLLRECON_ALLOW_LIST_REPORT&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try


            For intCntAllow = 0 To objRptDsAllow.Tables(0).Rows.Count - 1
                strADCodeAllow = Trim(objRptDsAllow.Tables(0).Rows(intCntAllow).Item("ADCode"))

                strParamCRReconAllowAmt = strADCodeAllow & "|" & arrLocCode(intCntLoc) & "|" & strDDLAccMth & "|" & strDDLAccYr & "|" & AllowStr
                Try
                    intErrNo = objPR.mtdGetReport_CheckRollReconList(strOpCd_CRRecon_Allow_Amt_GET, _
                                                                    strCompany, _
                                                                    strLocation, _
                                                                    strUserId, _
                                                                    strAccMonth, _
                                                                    strAccYear, _
                                                                    strParamCRReconAllowAmt, _
                                                                    objRptDsCRReconAllowAmt, _
                                                                    objMapPath)
                Catch Exp As System.Exception
                    Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_CHECKROLLRECON_ALLOW_AMOUNT_LIST_REPORT&errmesg=" & lblErrMessage.Text & "&redirect=")
                End Try
                objRptDsAllow.Tables(0).Rows(intCntAllow).Item("AllowADAmount") = Trim(objRptDsCRReconAllowAmt.Tables(0).Rows(0).Item("AllowADAmount"))
                dblAllowTotalAmt += objRptDsAllow.Tables(0).Rows(intCntAllow).Item("AllowADAmount")
            Next

            dr = objRptDsAllow.Tables(0).NewRow()
            dr("ADCode") = "ALLOWANCE :-"
            dr("ADDesc") = ""
            dr("LocCode") = arrLocCode(intCntLoc)
            dr("AllowADAmount") = 0
            dr("ReportID") = ""
            dr("RptName") = ""
            objRptDsAllow.Tables(0).Rows.InsertAt(dr, 0)

            dr = objRptDsAllow.Tables(0).NewRow()
            dr("ADCode") = "GRAND TOTAL :"
            dr("ADDesc") = ""
            dr("LocCode") = arrLocCode(intCntLoc)
            dr("AllowADAmount") = dblAllowTotalAmt
            dr("ReportID") = ""
            dr("RptName") = ""
            objRptDsAllow.Tables(0).Rows.Add(dr)

            dr = objRptDsAllow.Tables(0).NewRow()
            dr("ADCode") = ""
            dr("ADDesc") = ""
            dr("LocCode") = arrLocCode(intCntLoc)
            dr("AllowADAmount") = 0
            dr("ReportID") = ""
            dr("RptName") = ""
            objRptDsAllow.Tables(0).Rows.Add(dr)

            dr = objRptDsAllow.Tables(0).NewRow()
            dr("ADCode") = "DEDUCTION :-"
            dr("ADDesc") = ""
            dr("LocCode") = arrLocCode(intCntLoc)
            dr("AllowADAmount") = 0
            dr("ReportID") = ""
            dr("RptName") = ""
            objRptDsAllow.Tables(0).Rows.Add(dr)

            strParamCRReconPayDeduct = "|" & arrLocCode(intCntLoc) & "|" & strDDLAccMth & "|" & strDDLAccYr & "|" & PayDeductStr
            Try
                intErrNo = objPR.mtdGetReport_CheckRollReconList(strOpCd_CRRecon_Deduct_GET, _
                                                                 strCompany, _
                                                                 strLocation, _
                                                                 strUserId, _
                                                                 strAccMonth, _
                                                                 strAccYear, _
                                                                 strParamCRReconPayDeduct, _
                                                                 objRptDsDeduct, _
                                                                 objMapPath)
            Catch Exp As System.Exception
                Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_CHECKROLLRECON_DEDUCT_LIST_REPORT&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try

            For intCntDeduct = 0 To objRptDsDeduct.Tables(0).Rows.Count - 1
                strADCodeDeduct = Trim(objRptDsDeduct.Tables(0).Rows(intCntDeduct).Item("ADCode"))

                strParamCRReconDeductAmt = strADCodeDeduct & "|" & arrLocCode(intCntLoc) & "|" & strDDLAccMth & "|" & strDDLAccYr & "|" & PayDeductStr
                Try
                    intErrNo = objPR.mtdGetReport_CheckRollReconList(strOpCd_CRRecon_Deduct_Amt_GET, _
                                                                     strCompany, _
                                                                     strLocation, _
                                                                     strUserId, _
                                                                     strAccMonth, _
                                                                     strAccYear, _
                                                                     strParamCRReconDeductAmt, _
                                                                     objRptDsCRReconDeductAmt, _
                                                                     objMapPath)
                Catch Exp As System.Exception
                    Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_CHECKROLLRECON_DEDUCT_AMOUNT_LIST_REPORT&errmesg=" & lblErrMessage.Text & "&redirect=")
                End Try

                If Not IsDBNull(objRptDsCRReconDeductAmt.Tables(0).Rows(0).Item("DeductADAmount")) Then
                    objRptDsDeduct.Tables(0).Rows(intCntDeduct).Item("DeductADAmount") = Trim(objRptDsCRReconDeductAmt.Tables(0).Rows(0).Item("DeductADAmount"))
                Else
                    objRptDsDeduct.Tables(0).Rows(intCntDeduct).Item("DeductADAmount") = 0
                End If
                dblDeductTotalAmt += objRptDsDeduct.Tables(0).Rows(intCntDeduct).Item("DeductADAmount")

                dr = objRptDsAllow.Tables(0).NewRow()
                dr("ADCode") = Trim(objRptDsDeduct.Tables(0).Rows(intCntDeduct).Item("ADCode"))
                dr("ADDesc") = Trim(objRptDsDeduct.Tables(0).Rows(intCntDeduct).Item("ADDesc"))
                dr("LocCode") = Trim(objRptDsDeduct.Tables(0).Rows(intCntDeduct).Item("LocCode"))
                dr("AllowADAmount") = objRptDsDeduct.Tables(0).Rows(intCntDeduct).Item("DeductADAmount")
                dr("ReportID") = ""
                dr("RptName") = ""
                objRptDsAllow.Tables(0).Rows.Add(dr)
            Next

            dr = objRptDsAllow.Tables(0).NewRow()
            dr("ADCode") = "TOTAL DEDUCTION :"
            dr("ADDesc") = ""
            dr("LocCode") = arrLocCode(intCntLoc)
            dr("AllowADAmount") = dblDeductTotalAmt
            dr("ReportID") = ""
            dr("RptName") = ""
            objRptDsAllow.Tables(0).Rows.Add(dr)

            dr = objRptDsAllow.Tables(0).NewRow()
            dr("ADCode") = ""
            dr("ADDesc") = ""
            dr("LocCode") = arrLocCode(intCntLoc)
            dr("AllowADAmount") = 0
            dr("ReportID") = ""
            dr("RptName") = ""
            objRptDsAllow.Tables(0).Rows.Add(dr)

            dr = objRptDsAllow.Tables(0).NewRow()
            dr("ADCode") = "BALANCE OF PAY :"
            dr("ADDesc") = ""
            dr("LocCode") = arrLocCode(intCntLoc)
            dr("AllowADAmount") = dblAllowTotalAmt - dblDeductTotalAmt
            dr("ReportID") = ""
            dr("RptName") = ""
            objRptDsAllow.Tables(0).Rows.Add(dr)

            dr = objRptDsAllow.Tables(0).NewRow()
            dr("ADCode") = ""
            dr("ADDesc") = ""
            dr("LocCode") = arrLocCode(intCntLoc)
            dr("AllowADAmount") = 0
            dr("ReportID") = ""
            dr("RptName") = ""
            objRptDsAllow.Tables(0).Rows.Add(dr)

            dr = objRptDsAllow.Tables(0).NewRow()
            dr("ADCode") = ""
            dr("ADDesc") = ""
            dr("LocCode") = arrLocCode(intCntLoc)
            dr("AllowADAmount") = 0
            dr("ReportID") = ""
            dr("RptName") = ""
            objRptDsAllow.Tables(0).Rows.Add(dr)

            dr = objRptDsAllow.Tables(0).NewRow()
            dr("ADCode") = "TOTAL PAYMENT :"
            dr("ADDesc") = ""
            dr("LocCode") = arrLocCode(intCntLoc)
            dr("AllowADAmount") = dblAllowTotalAmt - dblDeductTotalAmt
            dr("ReportID") = ""
            dr("RptName") = ""
            objRptDsAllow.Tables(0).Rows.Add(dr)

            dr = objRptDsAllow.Tables(0).NewRow()
            dr("ADCode") = ""
            dr("ADDesc") = ""
            dr("LocCode") = arrLocCode(intCntLoc)
            dr("AllowADAmount") = 0
            dr("ReportID") = ""
            dr("RptName") = ""
            objRptDsAllow.Tables(0).Rows.Add(dr)

            dr = objRptDsAllow.Tables(0).NewRow()
            dr("ADCode") = "DIFFERENCE :"
            dr("ADDesc") = ""
            dr("LocCode") = arrLocCode(intCntLoc)
            dr("AllowADAmount") = 0
            dr("ReportID") = ""
            dr("RptName") = ""
            objRptDsAllow.Tables(0).Rows.Add(dr)

            If intCntLoc = 0 Then
                objDSCopy = objRptDsAllow.Copy()
            Else
                For intCntWhole = 0 To objRptDsAllow.Tables(0).Rows.Count - 1
                    dr = objDSCopy.Tables(0).NewRow()
                    dr("ADCode") = Trim(objRptDsAllow.Tables(0).Rows(intCntWhole).Item("ADCode"))
                    dr("ADDesc") = Trim(objRptDsAllow.Tables(0).Rows(intCntWhole).Item("ADDesc"))
                    dr("LocCode") = Trim(objRptDsAllow.Tables(0).Rows(intCntWhole).Item("LocCode"))
                    dr("AllowADAmount") = objRptDsAllow.Tables(0).Rows(intCntWhole).Item("AllowADAmount")
                    dr("ReportID") = ""
                    dr("RptName") = ""
                    objDSCopy.Tables(0).Rows.Add(dr)
                Next
            End If

            dblAllowTotalAmt = 0
            dblPayBFTotalAmt = 0
            dblDeductTotalAmt = 0
            dblGrandTotalAmt = 0
        Next

        strParamRptName = "WHERE ReportID = '" & Trim(Request.QueryString("RptName")) & "'"
        Try
            intErrNo = objAdmin.mtdGetStdRptName(strOpCd_RptName_GET, strParamRptName, objRptNameDs, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_STDRPT_NAME_FOR_CHECKROLLRECON&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try








        If objDSCopy.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objDSCopy.Tables(0).Rows.Count - 1
                objDSCopy.Tables(0).Rows(intCnt).Item("ReportID") = objRptNameDs.Tables(0).Rows(0).Item("ReportID")
                objDSCopy.Tables(0).Rows(intCnt).Item("RptName") = objRptNameDs.Tables(0).Rows(0).Item("RptName")
            Next intCnt
        End If

        rdCrystalViewer = New ReportDocument()

        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\PR_StdRpt_CheckRollReconList.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objDSCopy.Tables(0))

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3



        crvView.Visible = False                          
        crvView.ReportSource = rdCrystalViewer           
        crvView.DataBind()                              

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\PR_StdRpt_CheckRollReconList.pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/PR_StdRpt_CheckRollReconList.pdf"">")
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

        Dim ParameterValues1 As New ParameterValues()
        Dim ParameterValues2 As New ParameterValues()
        Dim ParameterValues3 As New ParameterValues()
        Dim ParameterValues4 As New ParameterValues()
        Dim ParameterValues5 As New ParameterValues()
        Dim ParameterValues6 As New ParameterValues()
        Dim ParameterValues7 As New ParameterValues()

        Dim ParamDiscreteValue1 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue2 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue3 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue4 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue5 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue6 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue7 As New ParameterDiscreteValue()

        ParamDiscreteValue1.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue2.Value = Session("SS_LOC")
        ParamDiscreteValue3.Value = Session("SS_USERNAME")
        ParamDiscreteValue4.Value = Session("SS_AccMth")
        ParamDiscreteValue5.Value = Session("SS_AccYr")
        ParamDiscreteValue6.Value = Session("SS_DECIMAL")
        ParamDiscreteValue7.Value = Session("SS_LBLLOCATION")

        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        ParamFieldDef1 = ParamFieldDefs.Item("ParamCompanyName")
        ParamFieldDef2 = ParamFieldDefs.Item("ParamLocation")
        ParamFieldDef3 = ParamFieldDefs.Item("ParamUserName")
        ParamFieldDef4 = ParamFieldDefs.Item("ParamAccMonth")
        ParamFieldDef5 = ParamFieldDefs.Item("ParamAccYear")
        ParamFieldDef6 = ParamFieldDefs.Item("ParamDecimal")
        ParamFieldDef7 = ParamFieldDefs.Item("lblLocation")

        ParameterValues1 = ParamFieldDef1.CurrentValues
        ParameterValues2 = ParamFieldDef2.CurrentValues
        ParameterValues3 = ParamFieldDef3.CurrentValues
        ParameterValues4 = ParamFieldDef4.CurrentValues
        ParameterValues5 = ParamFieldDef5.CurrentValues
        ParameterValues6 = ParamFieldDef6.CurrentValues
        ParameterValues7 = ParamFieldDef7.CurrentValues

        ParameterValues1.Add(ParamDiscreteValue1)
        ParameterValues2.Add(ParamDiscreteValue2)
        ParameterValues3.Add(ParamDiscreteValue3)
        ParameterValues4.Add(ParamDiscreteValue4)
        ParameterValues5.Add(ParamDiscreteValue5)
        ParameterValues6.Add(ParamDiscreteValue6)
        ParameterValues7.Add(ParamDiscreteValue7)

        ParamFieldDef1.ApplyCurrentValues(ParameterValues1)
        ParamFieldDef2.ApplyCurrentValues(ParameterValues2)
        ParamFieldDef3.ApplyCurrentValues(ParameterValues3)
        ParamFieldDef4.ApplyCurrentValues(ParameterValues4)
        ParamFieldDef5.ApplyCurrentValues(ParameterValues5)
        ParamFieldDef6.ApplyCurrentValues(ParameterValues6)
        ParamFieldDef7.ApplyCurrentValues(ParameterValues7)

    End Sub
End Class
