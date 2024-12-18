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

Public Class PU_StdRpt_MthStockReceived_Preview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents EventData As DataGrid

    Dim objPU As New agri.PU.clsReport()
    Dim objPUTrx As New agri.PU.clsTrx()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strUserLoc As String

    Dim tempLoc As String
    Dim strDDLAccMth As String
    Dim strDDLAccYr As String
    
    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_PUACCMONTH")
        strAccYear = Session("SS_PUACCYEAR")
        strLangCode = Session("SS_LANGCODE")

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

            strDDLAccMth = Request.QueryString("DDLAccMth")
            strDDLAccYr = Request.QueryString("DDLAccYr")

            Session("SS_lblAccCode") = Request.QueryString("lblAccCode")
            Session("SS_lblVehCode") = Request.QueryString("lblVehCode")
            Session("SS_lblVehExpCode") = Request.QueryString("lblVehExpCode")
            Session("SS_lblBlkCode") = Request.QueryString("lblBlkCode")
                      
            BindReport()
        End If
    End Sub

    Sub BindReport()

        Dim objRptDs As New DataSet()
        Dim objMapPath As String
        Dim objItem As New DataSet()

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOpCd_StockReceived_GET As String = "PU_STDRPT_STOCKRECEIVE_GET"
       
        Dim strParam As String
        Dim strParamItm As String
        Dim strItemCode As String
        Dim SearchStr As String
       
        Dim intCnt As Integer
        Dim intCntRem As Integer

        If Not (Request.QueryString("RcvNoteDateFrom") = "" And Request.QueryString("RcvNoteDateTo") = "") Then
            SearchStr = SearchStr & "AND (DateDiff(Day, '" & Request.QueryString("RcvNoteDateFrom") & "', GR.CreateDate) >= 0) And (DateDiff(Day, '" & Request.QueryString("RcvNoteDateTo") & "', GR.CreateDate) <= 0) "
        End If

        If Not Request.QueryString("Supplier") = "" Then
            SearchStr = SearchStr & "AND GR.SupplierCode LIKE '" & Request.QueryString("Supplier") & "' "
        End If

        If (Len(Request.QueryString("RcvNoteIDfrom")) > 0) And (Len(Request.QueryString("RcvNoteIDTo")) > 0) Then
            SearchStr = SearchStr & "AND GR.GoodsRcvID >= '" & Request.QueryString("RcvNoteIDfrom") & "' AND GR.GoodsRcvID <= '" & Request.QueryString("RcvNoteIDTo") & "' "
        ElseIf (Len(Request.QueryString("RcvNoteIDfrom")) > 0) Then
            SearchStr = SearchStr & "AND GR.GoodsRcvID LIKE '" & Request.QueryString("RcvNoteIDfrom") & "' "
        End If

        If (Len(Request.QueryString("FromItemCode")) > 0) And (Len(Request.QueryString("ToItemCode")) > 0) Then
            SearchStr = SearchStr & "AND GRLN.ItemCode >= '" & Request.QueryString("FromItemCode") & "' AND GRLN.ItemCode <= '" & Request.QueryString("ToItemCode") & "' "
        ElseIf (Len(Request.QueryString("FromItemCode")) > 0) Then
            SearchStr = SearchStr & "AND GRLN.ItemCode LIKE '" & Request.QueryString("FromItemCode") & "' "
        End If
        
        If Not Request.QueryString("Status") = "" Then
            If Not Request.QueryString("Status") = objPUTrx.EnumGRStatus.All Then
                SearchStr = SearchStr & "AND GR.Status = '" & Request.QueryString("Status") & "' "
            End If
        End If

        If Not Request.QueryString("ItemType") = "" Then
            If Not Request.QueryString("ItemType") = 0 Then
                SearchStr = SearchStr & "AND ITM.ItemType = '" & Request.QueryString("ItemType") & "' "
            End If
        End If


        strParam = strUserLoc & "|" & strDDLAccMth & "|" & strDDLAccYr & "|" & SearchStr
     

        Try
            intErrNo = objPU.mtdGetReport_StockReceived(strOpCd_StockReceived_GET, strParam, objRptDs, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_STDRPT_GRTN_LIST&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        
        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\PU_StdRpt_StockReceived.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        crvView.Visible = False                         
        crvView.ReportSource = rdCrystalViewer          
        crvView.DataBind()                              

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\PU_StdRpt_StockReceived.pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        rdCrystalViewer.Close()
        rdCrystalViewer.Dispose()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/PU_StdRpt_StockReceived.pdf"">")

        objRptDs = Nothing
        objItem = Nothing
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

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("ParamLocation")
        paramField2 = paramFields.Item("ParamRptID")
        paramField3 = paramFields.Item("ParamRptName")
        paramField4 = paramFields.Item("ParamCompanyName")
        paramField5 = paramFields.Item("ParamUserName")
        paramField6 = paramFields.Item("ParamDecimal")
        paramField7 = paramFields.Item("ParamAccMonth")
        paramField8 = paramFields.Item("ParamAccYear")
        paramField9 = paramFields.Item("ParamSupplier")
        paramField10 = paramFields.Item("ParamDocDateFrom")        
        paramField11 = paramFields.Item("ParamDocDateTo")
        paramField12 = paramFields.Item("lblLocation")
        paramField13 = paramFields.Item("lblAccCode")
        paramField14 = paramFields.Item("lblVehCode")
        paramField15 = paramFields.Item("lblVehExpCode")        
        paramField16 = paramFields.Item("ParamReceiveIDFrom")
        paramField17 = paramFields.Item("ParamReceiveIDTo")
        paramField18 = paramFields.Item("ParamReceiveDateFrom")
        paramField19 = paramFields.Item("ParamReceiveDateTo")
        paramField20 = paramFields.Item("ParamItemCodeFrom")
        paramField21 = paramFields.Item("ParamItemCodeTo")  
        paramField22 = paramFields.Item("lblBlkCode")
        paramField23 = paramFields.Item("ParamStatus")           
        paramField24 = paramFields.Item("ParamItemType")           

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

        ParamDiscreteValue1.Value = Session("SS_LOC")
        ParamDiscreteValue2.Value = Request.QueryString("RptID")
        ParamDiscreteValue3.Value = Request.QueryString("RptName")
        ParamDiscreteValue4.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue5.Value = Session("SS_USERNAME")
        ParamDiscreteValue6.Value = Request.QueryString("Decimal")
        ParamDiscreteValue7.Value = Request.QueryString("DDLAccMth")
        ParamDiscreteValue8.Value = Request.QueryString("DDLAccYr")
        ParamDiscreteValue9.Value = Request.QueryString("Supplier")
        ParamDiscreteValue10.Value = Request.QueryString("DocDateFrom")
        ParamDiscreteValue11.Value = Request.QueryString("DocDateTo")
        ParamDiscreteValue12.Value = Request.QueryString("lblLocation")
        ParamDiscreteValue13.Value = Request.QueryString("lblAccCode")
        ParamDiscreteValue14.Value = Request.QueryString("lblVehCode")
        ParamDiscreteValue15.Value = Request.QueryString("lblVehExpCode")        
        ParamDiscreteValue16.Value = Request.QueryString("RcvNoteIDFrom")
        ParamDiscreteValue17.Value = Request.QueryString("RcvNoteIDTo")
        ParamDiscreteValue18.Value = Request.QueryString("RcvNoteDateFrom")
        ParamDiscreteValue19.Value = Request.QueryString("RcvNoteDateTo")
        ParamDiscreteValue20.Value = Request.QueryString("FromItemCode")
        ParamDiscreteValue21.Value = Request.QueryString("ToItemCode")
        ParamDiscreteValue22.Value = Request.QueryString("lblBlkCode")
        ParamDiscreteValue23.Value = Request.QueryString("Status")        
        ParamDiscreteValue24.Value = Request.QueryString("ItemType")        

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
                 
    End Sub
End Class
