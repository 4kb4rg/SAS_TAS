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


Public Class CT_StdRpt_ItemSummaryList_Preview : Inherits Page
    Protected objAdmin As New agri.Admin.clsShare()

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData as Datagrid

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objCT As New agri.CT.clsReport()
    Dim objCTTrx As New agri.CT.clsTrx()
    Dim objCTSetup As New agri.CT.clsSetup()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strUserLoc as string

    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer

    Sub Page_Load(Sender As Object, E As EventArgs)
        crvView.Visible = False  
 
        trace.warn(Request.QueryString("Location"))
        If Left(Request.QueryString("Location"), 3) = "','" Then
            strUserLoc = Right(Request.QueryString("Location"), Len(Request.QueryString("Location")) - 3)
        else if Right(Request.QueryString("Location"), 1) = "," Then
            strUserLoc = Left(Request.QueryString("Location"), Len(Request.QueryString("Location")) - 1)
        else
            strUserLoc = trim(Request.QueryString("Location"))
        end if

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        strLangCode = Session("SS_LANGCODE")

        session("SS_DateFrom") = Request.QueryString("DateFrom")
        session("SS_DateTo") = Request.QueryString("DateTo")
        session("SS_ItemCode") = Request.QueryString("ItemCode")
        session("SS_ItemType") = Request.QueryString("ItemType")
        session("SS_ItemStatus") = Request.QueryString("ItemStatus")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        else
            BindReport()
        end if
    End Sub

    Sub BindReport()

        'Dim objRptDs As New DataSet()
        'Dim objRptNameDs as New DataSet()
        'Dim objRptCompLocPeriod as New DataSet()
        'Dim objRptUserLoc As New DataSet()
        'Dim objRptLocName As New DataSet()
        'Dim objMapPath As String

        'Dim intCnt as Integer
        'Dim intUserLoc As Integer

        'Dim SearchStr As String
        'Dim strUSAccMonth as String
        'Dim strUSAccYear As String
        'Dim strUserLocName as String

        'Dim tempAccMonth As String
        'Dim tempAccYear As String
        'Dim tempLoc As String
        'Dim tempUserLocName as String

        'Dim crExportOptions As ExportOptions
        'Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        'Dim strOpCdItem As String = "CT_STDRPT_ITEM_GET"
        'Dim strOpCdRptName as String = "CT_STDRPT_NAME_GET"
        'Dim strOpCdRptCompLocPeriod as String = "CT_STDRPT_COMP_LOC_PERIOD_GET"
        'Dim strOppCd_UserLoc_GET As String = "CT_STDRPT_USERLOCATION_GET"

        'Dim strParam As String
        'Dim strParamRptName as String
        'Dim strParamCompLocPeriod as String
        'Dim strParamUserLoc as String
        'Dim strParamLocName As String

        'Dim NormStr as string = "WHERE ITM.UpdateID = Usr.UserID AND ITM.ItemType = '" & objCTSetup.EnumInventoryItemType.CanteenItem & "' AND ITM.LocCode IN ('" & strUserLoc & "') AND "

        'tempLoc = Request.QueryString("Location")
        'If Right(tempLoc, 1) = "," Then
        '    session("SS_LOC") = Left(tempLoc, Len(tempLoc) - 1)
        'else
        '    session("SS_LOC") = tempLoc.Replace("'", "")
        'End If

        'if not (Request.QueryString("DateFrom") = "" and Request.QueryString("DateTo") = "") then
        '    SearchStr = "(DateDiff(Day, '" & session("SS_DATEFROM") & "', ITM.CreateDate) >= 0) And (DateDiff(Day, '" & session("SS_DATETO") & "', ITM.CreateDate) <= 0) AND "
        'end if

        'If Not Request.QueryString("ItemCode") = "" Then
        '    SearchStr = SearchStr & "ITM.ItemCode = '" & Request.QueryString("ItemCode") & "' AND "
        'End If

        'if not Request.QueryString("ItemStatus") = objCTSetup.EnumStockItemStatus.All then
        '    SearchStr = SearchStr & "ITM.Status = '" & Request.QueryString("ItemStatus") & "' AND "
        'else
        '    SearchStr = SearchStr & "ITM.Status LIKE '%' AND "
        'end if

        'If Not SearchStr = "" Then
        '    If Right(SearchStr, 4) = "AND " Then
        '        SearchStr = Left(SearchStr, Len(SearchStr) - 4)
        '    End If

        '    if not (Request.QueryString("DateFrom") = "" and Request.QueryString("DateTo") = "") then
        '        strParam = NormStr & searchStr
        '    elseif not Request.QueryString("ItemCode") = "" then
        '        strParam = NormStr & SearchStr
        '    elseif not Request.QueryString("ItemType") = "" then
        '        strParam = NormStr & SearchStr
        '    elseif not Request.QueryString("ItemStatus") = "" then
        '        strParam = NormStr & SearchStr
        '    else 
        '        strParam = NormStr & SearchStr
        '    end if
        'End If

        'Try
        '    intErrNo = objCT.mtdGetReport_ItemList(strOpCdItem, _
        '                                           strCompany, _
        '                                           strLocation, _
        '                                           strUserId, _
        '                                           strAccMonth, _
        '                                           strAccYear, _
        '                                           strParam, _
        '                                           objRptDs, _
        '                                           objMapPath)
        'Catch Exp As Exception
        '    Response.Redirect("../../mesg/ErrorMessage.aspx?errcode=GET_ITEM_SUMMARY_LIST_REPORT&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/CT_StdRpt_Selection.aspx")
        'End Try

        'strParamRptName = "WHERE Description = '" & trim(Request.QueryString("RptName")) & "'"
        'Try 
        '    intErrNo = objAdmin.mtdGetStdRptName(strOpCdRptName, strParamRptName, objRptNameDs, objMapPath)
        'Catch Exp As Exception
        '    Response.Redirect("../../mesg/ErrorMessage.aspx?errcode=GET_STDRPT_NAME&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/CT_StdRpt_Selection.aspx")
        'End Try

        'strParamUserLoc = "AND SYSLOC.LocCode IN ('" & strUserLoc & "')"
        'Try 
        '    intErrNo = objAdmin.mtdGetUserLocation(strOppCd_UserLoc_GET, strParamUserLoc, objRptUserLoc, objMapPath)
        'Catch Exp As Exception
        '    Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_ITEMSUMMARY_USERLOC_REPORT&errmesg=" & lblErrMessage.Text & "&redirect=")
        'End Try

        'for intUserLoc = 0 to objRptUserLoc.Tables(0).Rows.Count - 1
        '    tempUserLocName = trim(objRptUserLoc.Tables(0).Rows(intUserLoc).Item("LocName"))
        '    if objRptUserLoc.Tables(0).Rows.Count > 0 then
        '        strUserLocName = strUserLocName & ", " & tempUserLocName
        '    else
        '        strUserLocName = tempUserLocName
        '    end if                
        'next intUserLoc

        'If Left(strUserLocName, 2) = ", " Then
        '    strUserLocName = Right(strUserLocName, Len(strUserLocName) - 2)
        'end if

        'for intUserLoc = 0 to objRptUserLoc.Tables(0).Rows.Count - 1
        '    tempAccMonth = trim(objRptUserLoc.Tables(0).Rows(intUserLoc).Item("INAccMonth"))
        '    tempAccYear = trim(objRptUserLoc.Tables(0).Rows(intUserLoc).Item("INAccYear"))

        '    if objRptUserLoc.Tables(0).Rows.Count > 0 and strUSAccMonth <> "" then
        '        strUSAccMonth = strUSAccMonth & "','" & tempAccMonth
        '        strUSAccYear = strUSAccYear & "','" & tempAccYear
        '    else
        '        strUSAccMonth = tempAccMonth
        '        strUSAccYear = tempAccYear
        '    end if

        '    strParamCompLocPeriod = "AND LOC.LocCode IN ('" & strUserLoc & "') AND COMP.CompCode = '" & Request.QueryString("CompName") & _
        '                            "' AND SYSLOC.INAccMonth IN ('" & strUSAccMonth & "') AND SYSLOC.INAccYear IN ('" & strUSAccYear & "')"
        '    Try
        '        intErrNo = objCT.mtdGetCompLocPeriod(strOpCdRptCompLocPeriod, strParamCompLocPeriod, objRptCompLocPeriod, objMapPath)
        '    Catch Exp As Exception
        '        Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_ITEMSUMMARY_COMP_LOC_PERIOD_REPORT&errmesg=" & lblErrMessage.Text & "&redirect=")
        '    End Try
        'next intUserLoc


        'if objRptDs.Tables(0).Rows.Count > 0 then
        '    for intCnt = 0 to objRptDs.Tables(0).Rows.Count - 1 
        '        objRptDs.Tables(0).Rows(intCnt).Item("ReportID") = objRptNameDs.Tables(0).Rows(0).Item("ReportID")
        '        objRptDs.Tables(0).Rows(intCnt).Item("RptName") = objRptNameDs.Tables(0).Rows(0).Item("RptName")
        '        objRptDs.Tables(0).Rows(intCnt).Item("CompName") = objRptCompLocPeriod.Tables(0).Rows(0).Item("CompName")
        '        objRptDs.Tables(0).Rows(intCnt).Item("LocName") = strUserLocName
        '        objRptDs.Tables(0).Rows(intCnt).Item("AccMonth") = strUSAccMonth
        '        objRptDs.Tables(0).Rows(intCnt).Item("AccYear") = strUSAccYear
        '    next intCnt     
        'end if   

        'eventData.datasource = objRptDs
        'eventdata.databind()

        'rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\CT_StdRpt_ItemSummaryList.rpt", OpenReportMethod.OpenReportByTempCopy)
        'rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        'crvView.ReportSource = rdCrystalViewer          
        'crvView.DataBind()                              

        'PassParam()

        'crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        'crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\CT_StdRpt_ItemSummaryList.pdf"

        'crExportOptions = rdCrystalViewer.ExportOptions
        'With crExportOptions
        '    .DestinationOptions = crDiskFileDestinationOptions
        '    .ExportDestinationType = ExportDestinationType.DiskFile
        '    .ExportFormatType = ExportFormatType.PortableDocFormat
        'End With

        'rdCrystalViewer.Export()
    End Sub

    Sub PassParam()
        Dim paramFields As New ParameterFields()
        Dim paramField1 As New ParameterField()
        Dim paramField2 As New ParameterField()
        Dim paramField3 As New ParameterField()
        Dim paramField4 As New ParameterField()
        Dim paramField5 As New ParameterField()

        Dim ParamDiscreteValue1 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue2 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue3 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue4 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue5 As New ParameterDiscreteValue()

        Dim crParameterValues1 as ParameterValues
        Dim crParameterValues2 as ParameterValues
        Dim crParameterValues3 as ParameterValues
        Dim crParameterValues4 as ParameterValues
        Dim crParameterValues5 as ParameterValues

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        ParamFields = crvView.ParameterFieldInfo
     
        ParamField1 = ParamFields.Item("ParamLocation")  
        ParamField2 = ParamFields.Item("ParamDateFrom")
        ParamField3 = ParamFields.Item("ParamDateTo")
        ParamField4 = ParamFields.Item("ParamItemCode")
        ParamField5 = ParamFields.Item("ParamItemStatus")
     
        crParameterValues1 = ParamField1.CurrentValues
        crParameterValues2 = ParamField2.CurrentValues
        crParameterValues3 = ParamField3.CurrentValues
        crParameterValues4 = ParamField4.CurrentValues
        crParameterValues5 = ParamField5.CurrentValues

        ParamDiscreteValue1.Value = session("SS_LOC")
        ParamDiscreteValue2.Value = session("SS_DateFrom")
        ParamDiscreteValue3.Value = session("SS_DateTo") 
        ParamDiscreteValue4.Value = session("SS_ItemCode")
        ParamDiscreteValue5.Value = session("SS_ItemStatus")

        crParameterValues1.Add(ParamDiscreteValue1)
        crParameterValues2.Add(ParamDiscreteValue2)
        crParameterValues3.Add(ParamDiscreteValue3)
        crParameterValues4.Add(ParamDiscreteValue4)
        crParameterValues5.Add(ParamDiscreteValue5)

        PFDefs(0).ApplyCurrentValues(crParameterValues1)
        PFDefs(1).ApplyCurrentValues(crParameterValues2)
        PFDefs(2).ApplyCurrentValues(crParameterValues3)
        PFDefs(3).ApplyCurrentValues(crParameterValues4)
        PFDefs(4).ApplyCurrentValues(crParameterValues5)

        crvView.ParameterFieldInfo = paramFields
    End Sub
End Class
