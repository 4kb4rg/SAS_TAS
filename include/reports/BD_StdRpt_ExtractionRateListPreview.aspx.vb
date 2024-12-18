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
Imports System.XML
Imports System.Web.Services
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web

Imports agri.BD.clsTrx
Imports agri.Admin.clsShare
Imports agri.Admin.clsComp
Imports agri.Admin.clsLoc
Imports agri.GlobalHdl.clsGlobalHdl

Public Class BD_StdRpt_ExtractionRateListPreview : inherits page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCriteria As HtmlTable
    Protected WithEvents tblCrystal As HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents dgResult As DataGrid

    Dim objBD As New agri.BD.clsTrx()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objComp As New agri.Admin.clsComp()
    Dim objLoc As New agri.Admin.clsLoc()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim rdCrystalViewer As New ReportDocument()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String

    Dim strMonthFrom As String
    Dim strMonthTo As String
    Dim strUpdatedDateFrom As String
    Dim strUpdatedDateTo As String
    Dim strUpdatedBy As String
    Dim strStatus As String
    Dim strParamStatus As String
    Dim strLocType As String

    Public Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strLocType = Session("SS_LOCTYPE")
        
        crvView.Visible = False  

        strMonthFrom = Trim(Request.QueryString("MonthFrom"))
        strMonthTo = Trim(Request.QueryString("MonthTo"))
        strUpdatedDateFrom = Trim(Request.QueryString("UpdatedDateFrom"))
        strUpdatedDateTo = Trim(Request.QueryString("UpdatedDateTo"))
        strUpdatedBy = Trim(Request.QueryString("UpdatedBy"))
        strStatus = Trim(Request.QueryString("Status"))
        strParamStatus = objBD.mtdGetExtractRateStatus(strStatus)
        Bind_ITEM(TRUE)

    End Sub

    Sub Bind_ITEM(ByVal blnIsPDFFormat As Boolean)
        Dim crLogonInfo As CrystalDecisions.Shared.TableLogOnInfo
        Dim objRptDs As New Dataset()
        Dim objMapPath As New Object()
        Dim objCompDs As New Dataset()
        Dim objLocDs As New Dataset()
        Dim strOpCd As String = "BD_CLSTRX_EXTRACTRATE_LIST_GET_FOR_REPORT"
        Dim strOpCd_Comp As String = "ADMIN_CLSCOMP_COMPANY_DETAILS_GET"
        Dim strOpCod_Loc As String = "ADMIN_CLSLOC_LOCATION_DETAILS_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim strSearch As String
        Dim strSortItem As String
        Dim intCnt As Integer
        Dim strCompName As String
        Dim strLocName As String
        Dim dr As DataRow


        If NOT strMonthFrom = "" Then
            strSearch = strSearch & " AND ((AccYear='" & strMonthFrom.SubString(3, 4) & "' AND AccMonth>='" & strMonthFrom.SubString(0, 2) & "') OR AccYear>'" & strMonthFrom.SubString(3, 4) & "')"
        End If
        
        If NOT strMonthTo = "" Then
            strSearch = strSearch & " AND ((AccYear='" & strMonthTo.SubString(3, 4) & "' AND AccMonth<='" & strMonthTo.SubString(0, 2) & "') OR AccYear<'" & strMonthTo.SubString(3, 4) & "')"
        End If
        
        If NOT strUpdatedDateFrom = "" Then
            strSearch = strSearch & " AND (DateDiff(Day, '" & strUpdatedDateFrom & "', BD.UpdateDate) >= 0)"
        End If
        
        If NOT strUpdatedDateTo = "" Then
            strSearch = strSearch & " And (DateDiff(Day, '" & strUpdatedDateTo & "', BD.UpdateDate) <= 0)"
        End If
        
        If NOT strUpdatedBy = "" Then
            strSearch = strSearch & " AND usr.Username like '" & _
                        strUpdatedBy & "' "
        End If

        If NOT strStatus = objBD.EnumExtractRateStatus.All Then
            strSearch = strSearch & " AND BD.Status = '" & strStatus & "'"
        End If

        strSortItem = "ORDER BY AccYear, AccMonth"
        
        strParam =  strSortItem & "|" & strSearch

        Try
            intErrNo = objBD.mtdGetExtractionRateList(strOpCd, strParam, objRptDs)
            
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        Try
            intErrNo = objAdmin.mtdGetBasePath(objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        Try
            intErrNo = objComp.mtdGetComp(strOpCd_Comp, strCompany, objCompDs, True)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        strCompName = Trim(objCompDs.Tables(0).Rows(0).Item("CompName"))

        Try
            intErrNo = objLoc.mtdGetLocDetail(strOpCod_Loc, "", "", "", objLocDs, strLocation)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        strLocName = Trim(objLocDs.Tables(0).Rows(0).Item("Description"))

        For intCnt=0 To objRptDs.Tables(0).Rows.Count - 1
            objRptDs.Tables(0).Rows(intCnt).Item(0) = Trim(objRptDs.Tables(0).Rows(intCnt).Item(0))
            objRptDs.Tables(0).Rows(intCnt).Item(1) = Trim(objRptDs.Tables(0).Rows(intCnt).Item(1))
            objRptDs.Tables(0).Rows(intCnt).Item(2) = Trim(objRptDs.Tables(0).Rows(intCnt).Item(2))
            objRptDs.Tables(0).Rows(intCnt).Item(3) = Trim(objRptDs.Tables(0).Rows(intCnt).Item(3))
            objRptDs.Tables(0).Rows(intCnt).Item(4) = Trim(objRptDs.Tables(0).Rows(intCnt).Item(4))
            objRptDs.Tables(0).Rows(intCnt).Item(5) = Trim(objRptDs.Tables(0).Rows(intCnt).Item(5))
            objRptDs.Tables(0).Rows(intCnt).Item(6) = Trim(objRptDs.Tables(0).Rows(intCnt).Item(6))
            objRptDs.Tables(0).Rows(intCnt).Item(7) = Trim(objRptDs.Tables(0).Rows(intCnt).Item(7))
            objRptDs.Tables(0).Rows(intCnt).Item(8) = Trim(objRptDs.Tables(0).Rows(intCnt).Item(8))
        Next
        
        For intCnt=0 To objRptDs.Tables(0).Rows.Count - 1
            objRptDs.Tables(0).Rows(intCnt).Item("Status") = objBD.mtdGetExtractRateStatus(objRptDs.Tables(0).Rows(intCnt).Item("Status"))
            objRptDs.Tables(0).Rows(intCnt).Item("UpdateDate") = objGlobal.GetLongDate(objRptDs.Tables(0).Rows(intCnt).Item("UpdateDate"))
        Next

        If objRptDs.Tables(0).Rows.Count > 0 Then
            objRptDs.Tables(0).Rows(0).Item("CompName") = strCompName
            objRptDs.Tables(0).Rows(0).Item("LocName") = strLocName
        Else
            dr = objRptDs.Tables(0).NewRow()
            dr("CompName") = strCompName
            dr("LocName") = strLocName
            objRptDs.Tables(0).Rows.InsertAt(dr, 0)
        End If

        rdCrystalViewer.Load(objMapPath & "Web\" & strLangCode & "\Reports\Crystal\BD_StdRpt_ExtractRateList.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        If Not blnIsPDFFormat Then
            crvView.Visible = True     
            crvView.ReportSource = rdCrystalViewer
            crvView.DataBind()
            PassParam()
        Else
            crvView.Visible = False 
            crvView.ReportSource = rdCrystalViewer
            crvView.DataBind()
            PassParam()
            Dim DiskOpts As CrystalDecisions.Shared.DiskFileDestinationOptions = New CrystalDecisions.Shared.DiskFileDestinationOptions()
            rdCrystalViewer.ExportOptions.ExportDestinationType = CrystalDecisions.[Shared].ExportDestinationType.DiskFile
            rdCrystalViewer.ExportOptions.ExportFormatType = CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat
            DiskOpts.DiskFileName = objMapPath & "web\ftp\BD_StdRpt_ExtractionRateList.pdf"
            rdCrystalViewer.ExportOptions.DestinationOptions = DiskOpts
            rdCrystalViewer.Export()

            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/BD_StdRpt_ExtractionRateList.pdf"">")
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

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        ParamFields = crvView.ParameterFieldInfo
     
        paramField1 = ParamFields.Item("ParamCompanyName") 
        paramField2 = ParamFields.Item("ParamRptID")
        paramField3 = ParamFields.Item("ParamRptName")
        paramField4 = ParamFields.Item("ParamUserName")
        paramField5 = ParamFields.Item("ParamDecimal")
        paramField6 = ParamFields.Item("ParamLocation")
        paramField7 = ParamFields.Item("lblLocation") 
        paramField8 = ParamFields.Item("ParamPeriodFrom")
        paramField9 = ParamFields.Item("ParamPeriodTo")
        paramField10 = ParamFields.Item("ParamUpdDateFrom")
        paramField11 = ParamFields.Item("ParamUpdDateTo")
        paramField12 = ParamFields.Item("ParamUpdatedBy")
        paramField13 = ParamFields.Item("ParamStatus")

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

        ParamDiscreteValue1.value = Session("SS_COMPANYNAME")
        ParamDiscreteValue2.value = Request.QueryString("RptID")
        ParamDiscreteValue3.value = Request.QueryString("RptName")
        ParamDiscreteValue4.value = Session("SS_USERNAME")
        ParamDiscreteValue5.value = Request.QueryString("Decimal")
        ParamDiscreteValue6.value = Session("SS_LOCATION")
        ParamDiscreteValue7.value = Trim(Request.QueryString("lblLocationTag"))
        ParamDiscreteValue8.value = strMonthFrom
        ParamDiscreteValue9.value = strMonthTo
        ParamDiscreteValue10.value = strUpdatedDateFrom
        ParamDiscreteValue11.value = strUpdatedDateTo
        ParamDiscreteValue12.value = strUpdatedBy
        ParamDiscreteValue13.value = strParamStatus

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

        crvView.ParameterFieldInfo = paramFields

    End Sub

End Class

