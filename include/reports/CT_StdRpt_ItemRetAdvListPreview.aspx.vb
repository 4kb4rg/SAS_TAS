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


Public Class CT_StdRpt_ItemRetAdvList_Preview : Inherits Page
    Protected objAdmin As New agri.Admin.clsShare()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label

    Dim objCT As New agri.CT.clsReport()
    Dim objCTTrx As New agri.CT.clsTrx()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth as String
    Dim strAccYear As String
    Dim strLangCode As String

    Dim strUserLoc as string
    Dim strAccMth As String
    Dim strAccYr As String

    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer

    Sub Page_Load(Sender As Object, E As EventArgs)
        crvView.Visible = False  
 
        If Left(Request.QueryString("AccMonth"), 3) = "','" Then
            strAccMth = Right(Request.QueryString("AccMonth"), Len(Request.QueryString("AccMonth")) - 3)
        else
            strAccMth = Request.QueryString("AccMonth")
        end if

        If Left(Request.QueryString("AccYear"), 3) = "','" Then
            strAccYr = Right(Request.QueryString("AccYear"), Len(Request.QueryString("AccYear")) - 3)
        else
            strAccYr = Request.QueryString("AccYear")
        end if

        If Right(Request.QueryString("Location"), 1) = "," Then
            strUserLoc = Left(Request.QueryString("Location"), Len(Request.QueryString("Location")) - 1)
        else
            strUserLoc = trim(Request.QueryString("Location"))
        end if

        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")       
        strLangCode = Session("SS_LANGCODE")

        session("SS_DateFrom") = Request.QueryString("DateFrom")
        session("SS_DateTo") = Request.QueryString("DateTo")
        session("SS_DocNoFrom") = Request.QueryString("DocNoFrom")
        session("SS_DocNoTo") = Request.QueryString("DocNoTo")
        session("SS_ItemCode") = Request.QueryString("ItemCode")       
        session("SS_Status") = Request.QueryString("Status")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        else
            BindReport()
        end if
    End Sub

    Sub BindReport()

        Dim objRptDs As New DataSet()
        Dim objRptNameDs as New DataSet()
        Dim objRptUserLoc As New DataSet()
        Dim objMapPath As String
        Dim objItem as New DataSet()

        Dim arrParamMth as Array
        Dim arrParamYr as Array

        Dim intCnt as Integer
        Dim intUserLoc As Integer
        Dim intCntAccPeriod as Integer

        Dim SearchStr As String
        Dim strUSAccMonth As String = ""
        Dim strUSAccYear As String = ""
        Dim strUSLocName as String
        Dim strUSAccPeriod as String

        Dim tempUSAccMonth As String
        Dim tempUSAccYear As String
        Dim tempLoc As String
        Dim tempUserLocName as String
        
        Dim strParam As String
        Dim strParamRptName as String
        Dim strParamUserLoc as String
        Dim strParamItm as String
        Dim strItemCode as String

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOpCdItemRetAdv_GET As String = "CT_STDRPT_ITEMRETADV_AND_ITEMRETADVLN_GET"
        Dim strOpCdRptName_GET as String = "CT_STDRPT_NAME_GET"
        Dim strOpCdItem_GET as String = "CT_STDRPT_ITEM_GET"
        Dim strOppCd_UserLoc_GET As String = "CT_STDRPT_USERLOCATION_GET"

        Dim WildStr As String = "AND IRA.ItemRetAdvID *= IRALn.ItemRetAdvID AND IRA.ItemDocType = '" & objCTTrx.EnumItemDocType.CanRetAdv & "' AND IRA.LocCode IN ('" & strUserLoc & "') AND "
        Dim NormStr as string = "AND IRA.ItemRetAdvID = IRALn.ItemRetAdvID AND IRA.ItemDocType = '" & objCTTrx.EnumItemDocType.CanRetAdv & "' AND IRA.LocCode IN ('" & strUserLoc & "') AND "

        tempLoc = Request.QueryString("Location")
        If Right(tempLoc, 1) = "," Then
            session("SS_LOC") = Left(tempLoc, Len(tempLoc) - 1)
        else
            session("SS_LOC") = tempLoc.Replace("'", "")
        End If

        if not (Request.QueryString("DateFrom") = "" and Request.QueryString("DateTo") = "") then
            SearchStr = "(DateDiff(Day, '" & session("SS_DATEFROM") & "', IRA.CreateDate) >= 0) And (DateDiff(Day, '" & session("SS_DATETO") & "', IRA.CreateDate) <= 0) AND "
        end if

        if not (Request.QueryString("DocNoFrom") = "" and Request.QueryString("DocNoTo") = "") then
            SearchStr = SearchStr & "IRA.ItemRetAdvID IN (SELECT SUBIRA.ItemRetAdvID FROM IN_ITEMRETADV SUBIRA WHERE SUBIRA.ItemRetAdvID >= '" & Request.QueryString("DocNoFrom") & _
                        "' AND SUBIRA.ItemRetAdvID <= '" & Request.QueryString("DocNoTo") & "') AND "
        end if

        If Not Request.QueryString("ItemCode") = "" Then
            SearchStr = SearchStr & "IRALN.ItemCode = '" & Request.QueryString("ItemCode") & "' AND "
        End If

        If Not Request.QueryString("Status") = "" Then
            if not Request.QueryString("Status") = objCTTrx.EnumCanRetAdvStatus.All then
                SearchStr = SearchStr & "IRA.Status = '" & Request.QueryString("Status") & "' AND "
            else
                SearchStr = SearchStr & "IRA.Status LIKE '%' AND "
            end if
        End If


        If Not SearchStr = "" Then
            If Right(SearchStr, 4) = "AND " Then
                SearchStr = Left(SearchStr, Len(SearchStr) - 4)
            End If
        
            if not (Request.QueryString("DateFrom") = "" and Request.QueryString("DateTo") = "") then
                strParam = WildStr & searchStr
            elseif not (Request.QueryString("DocNoFrom") = "" and Request.QueryString("DocNoTo") = "") then
                strParam = WildStr & searchStr
            elseif not Request.QueryString("ItemCode") = ""  then
                strParam = NormStr & searchStr
            elseif not Request.QueryString("Status") = "" then
                strParam = WildStr & SearchStr
            else 
                strParam = WildStr & SearchStr
            end if
        End If

        Try
            intErrNo = objCT.mtdGetReport_CanteenReturnListing(strOpCdItemRetAdv_GET, strParam, objRptDs, objMapPath)
        Catch Exp As Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_CT_ITEMRETADV_LIST_REPORT&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objRptDs.Tables(0).Rows.Count - 1
            if not isdbnull(objRptDs.Tables(0).Rows(intCnt).Item("ItemCode"))
                strItemCode = Trim(objRptDs.Tables(0).Rows(intCnt).Item("ItemCode"))
                strParamItm = "WHERE ITM.ItemCode = '" & strItemCode & "'"
                Try
                    intErrNo = objCT.mtdGetItem(strOpCdItem_GET, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strAccMonth, _
                                                strAccYear, _
                                                strParamItm, _
                                                objItem)
                Catch Exp As Exception
                    Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_ITEM_FOR_CT_ITEMRETADV_REPORT&errmesg=" & lblErrMessage.Text & "&redirect=")
                End Try

                if objItem.Tables(0).Rows.Count > 0 then
                    objRptDs.Tables(0).Rows(intCnt).Item("Item") = Trim(objRptDs.Tables(0).Rows(0).Item("ItemCode")) & " (" & _
                                                                   Trim(objItem.Tables(0).Rows(0).Item("Description")) & ")"
                end if
            end if
        next intCnt

        strParamRptName = "WHERE Description = '" & trim(Request.QueryString("RptName")) & "'"
        Try 
            intErrNo = objAdmin.mtdGetStdRptName(strOpCdRptName_GET, strParamRptName, objRptNameDs, objMapPath)
        Catch Exp As Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_ITEMRETADV_STDRPT_NAME&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        strParamUserLoc = "AND SYSLOC.LocCode IN ('" & strUserLoc & "')"
        Try 
            intErrNo = objAdmin.mtdGetUserLocation(strOppCd_UserLoc_GET, strParamUserLoc, objRptUserLoc, objMapPath)
        Catch Exp As Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_CT_ITEMRETADV_USERLOC_REPORT&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        for intUserLoc = 0 to objRptUserLoc.Tables(0).Rows.Count - 1
            tempUserLocName = trim(objRptUserLoc.Tables(0).Rows(intUserLoc).Item("LocName"))

            if objRptUserLoc.Tables(0).Rows.Count > 0 then
                strUSLocName = strUSLocName & ", " & tempUserLocName
            else
                strUSLocName = tempUserLocName
            end if    
        next intUserLoc

        arrParamMth = Split(strAccMth, "','")
        arrParamYr = Split(strAccYr, "','")

        for intCntAccPeriod = 0 to arrParamMth.getupperBound(0)
            tempUSAccMonth = arrParamMth(intCntAccPeriod) 
            tempUSAccYear = arrParamYr(intCntAccPeriod) 
            strUSAccPeriod = strUSAccPeriod & tempUSAccMonth & "/" & tempUSAccYear & ", " 
        next intCntAccPeriod

        If Left(strUSLocName, 2) = ", " Then
            strUSLocName = Right(strUSLocName, Len(strUSLocName) - 2)
        end if

        If Right(strUSAccPeriod, 2) = ", " Then
            strUSAccPeriod = Left(strUSAccPeriod, Len(strUSAccPeriod) - 2)
        end if


        if objRptDs.Tables(0).Rows.Count > 0 then
            for intCnt = 0 to objRptDs.Tables(0).Rows.Count - 1 
                objRptDs.Tables(0).Rows(intCnt).Item("ReportID") = objRptNameDs.Tables(0).Rows(0).Item("ReportID")
                objRptDs.Tables(0).Rows(intCnt).Item("RptName") = objRptNameDs.Tables(0).Rows(0).Item("RptName")
                objRptDs.Tables(0).Rows(intCnt).Item("CompName") = strCompanyName
                objRptDs.Tables(0).Rows(intCnt).Item("LocName") = strUSLocName
                objRptDs.Tables(0).Rows(intCnt).Item("AccPeriod") = strUSAccPeriod
                objRptDs.Tables(0).Rows(intCnt).Item("PrintedBy") = strUserName
            next intCnt
        end if   
     
        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\CT_StdRpt_ItemRetAdvList.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        crvView.Visible = false                          
        crvView.ReportSource = rdCrystalViewer          
        crvView.DataBind()                              

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\CT_StdRpt_ItemRetAdvList.pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With
        rdCrystalViewer.Export()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/CT_StdRpt_ItemRetAdvList.pdf"">")
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

        Dim ParamDiscreteValue1 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue2 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue3 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue4 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue5 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue6 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue7 As New ParameterDiscreteValue()

        Dim crParameterValues1 as ParameterValues
        Dim crParameterValues2 as ParameterValues
        Dim crParameterValues3 as ParameterValues
        Dim crParameterValues4 as ParameterValues
        Dim crParameterValues5 as ParameterValues
        Dim crParameterValues6 as ParameterValues
        Dim crParameterValues7 as ParameterValues

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        ParamFields = crvView.ParameterFieldInfo
     
        ParamField1 = ParamFields.Item("ParamLocation")    
        ParamField2 = ParamFields.Item("ParamDateFrom")
        ParamField3 = ParamFields.Item("ParamDateTo")
        ParamField4 = ParamFields.Item("ParamDocNoFrom")
        ParamField5 = ParamFields.Item("ParamDocNoTo")
        ParamField6 = ParamFields.Item("ParamItemCode")
        ParamField7 = ParamFields.Item("ParamStatus")
        
        crParameterValues1 = ParamField1.CurrentValues
        crParameterValues2 = ParamField2.CurrentValues
        crParameterValues3 = ParamField3.CurrentValues
        crParameterValues4 = ParamField4.CurrentValues
        crParameterValues5 = ParamField5.CurrentValues   
        crParameterValues6 = ParamField6.CurrentValues
        crParameterValues7 = ParamField7.CurrentValues

        ParamDiscreteValue1.Value = session("SS_LOC")
        ParamDiscreteValue2.Value = session("SS_DateFrom")
        ParamDiscreteValue3.Value = session("SS_DateTo") 
        ParamDiscreteValue4.Value = session("SS_DocNoFrom")
        ParamDiscreteValue5.Value = session("SS_DocNoTo")
        ParamDiscreteValue6.Value = session("SS_ItemCode")
        ParamDiscreteValue7.Value = session("SS_Status")

        crParameterValues1.Add(ParamDiscreteValue1)
        crParameterValues2.Add(ParamDiscreteValue2)
        crParameterValues3.Add(ParamDiscreteValue3)
        crParameterValues4.Add(ParamDiscreteValue4)
        crParameterValues5.Add(ParamDiscreteValue5)
        crParameterValues6.Add(ParamDiscreteValue6)
        crParameterValues7.Add(ParamDiscreteValue7)

        PFDefs(0).ApplyCurrentValues(crParameterValues1)
        PFDefs(1).ApplyCurrentValues(crParameterValues2)
        PFDefs(2).ApplyCurrentValues(crParameterValues3)
        PFDefs(3).ApplyCurrentValues(crParameterValues4)
        PFDefs(4).ApplyCurrentValues(crParameterValues5)
        PFDefs(5).ApplyCurrentValues(crParameterValues6)
        PFDefs(6).ApplyCurrentValues(crParameterValues7)

        crvView.ParameterFieldInfo = paramFields
    End Sub
End Class
