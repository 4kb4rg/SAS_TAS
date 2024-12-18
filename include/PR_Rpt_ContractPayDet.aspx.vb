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

Imports agri.IN.clsTrx
Imports agri.Admin.clsShare
Imports agri.Admin.clsComp
Imports agri.Admin.clsLoc
Imports agri.GlobalHdl.clsGlobalHdl


Public Class PR_Rpt_ContractPayDet : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents dgResult As DataGrid
    Protected WithEvents lblErrMesage As Label

    Dim objPR As New agri.PR.clsTrx()
    Dim objAdmin As New agri.Admin.clsShare()
    Protected  objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim rdCrystalViewer As New ReportDocument()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String

    Dim strStatus As String
    Dim strContractId As String
    Dim strPrintDate As String
    Dim strSortLine As String

    Dim strCompName As String
    Dim strLocName As String
    Dim strUserName As String
    Dim AccountTag As String
    Dim BlockTag As String
    Dim VehicleTag As String
    Dim VehExpenseCodeTag As String
    
    Public Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strCompName = Session("SS_COMPANYNAME")
        strLocName = Session("SS_LOCATIONNAME")
        strUserName = Session("SS_USERNAME")
        
        crvView.Visible = False  

        strStatus = Trim(Request.QueryString("strStatus"))
        strContractId = Trim(Request.QueryString("strContractId"))
        strPrintDate = Trim(Request.QueryString("strPrintDate"))
        strSortLine = Trim(Request.QueryString("strSortLine"))
        AccountTag = Trim(Request.QueryString("AccountTag"))
        BlockTag = Trim(Request.QueryString("BlockTag"))
        VehicleTag = Trim(Request.QueryString("VehicleTag"))
        VehExpenseCodeTag = Trim(Request.QueryString("VehExpenseCodeTag"))

        Bind_ITEM(True)

    End Sub

    Sub Bind_ITEM(ByVal blnIsPDFFormat As Boolean)
        Dim crLogonInfo As CrystalDecisions.Shared.TableLogOnInfo
        Dim objRptDs As New Dataset()
        Dim objMapPath As New Object()
        Dim strOpCd_Get As String = "PR_CLSTRX_CONTRACTPAY_GET" & "|" & "ContractPay"
        Dim strOpCd_GetLine As String = "PR_CLSTRX_CONTRACTPAY_LINE_GET" & "|" & "ContractPayLn"
        Dim strOpCodes As String
        Dim strSortItem As String
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer

        
        strOpCodes = strOpCd_Get & chr(9) & strOpCd_GetLine
        strParam = strContractId

        Try
            intErrNo = objPR.mtdGetContractPayReport(strOpCodes, _
                                                     strCompany, _
                                                     strLocation, _
                                                     strUserId, _
                                                     strAccMonth, _
                                                     strAccYear, _
                                                     strParam, _
                                                     objRptDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=&errmesg=" & lblErrMesage.Text & "&redirect=")        
        End Try
 


        Dim myDS as New DataSet   
        Dim myTable As DataTable
        Dim DR, myRow As DataRow
        Dim i,j as int16
    
        myDS = ObjRptDs.Clone()
        
        myDS.Tables("ContractPay").Columns("qtyReq").DataType = System.Type.GetType("System.String")
        myDS.Tables("ContractPay").Columns("AmountReq").DataType = System.Type.GetType("System.String")
        myDS.Tables("ContractPay").Columns("qtyComplete").DataType = System.Type.GetType("System.String")
        myDS.Tables("ContractPay").Columns("AmountComplete").DataType = System.Type.GetType("System.String")
        myDS.Tables("ContractPayLn").Columns("Amount").DataType = System.Type.GetType("System.String")
        myDS.Tables("ContractPayLn").Columns("TotalAmount").DataType = System.Type.GetType("System.String")
        
     
        For i = 0 To objRptDs.Tables.Count - 1
            For Each DR In objRptDs.Tables(i).Rows
                myRow = myDS.Tables(i).NewRow()
                For j  = 0 To objRptDs.Tables(i).Columns.Count - 1
                         If (objRptDs.Tables(i).Columns(j).ColumnName.Trim.ToUpper = "QTYREQ") OR (objRptDs.Tables(i).Columns(j).ColumnName.Trim.ToUpper = "AMOUNTREQ") OR (objRptDs.Tables(i).Columns(j).ColumnName.Trim.ToUpper = "QTYCOMPLETE") OR (objRptDs.Tables(i).Columns(j).ColumnName.Trim.ToUpper = "AMOUNTCOMPLET") OR (objRptDs.Tables(i).Columns(j).ColumnName.Trim.ToUpper = "AMOUNT") OR (objRptDs.Tables(i).Columns(j).ColumnName.Trim.ToUpper = "TOTALAMOUNT") THEN       
                            myRow(j) = objGlobal.GetIDDecimalSeparator(DR(j))
                         Else
                            myRow(j) = DR(j)
                         End if
                Next j
                myDS.Tables(i).Rows.Add(myRow)
            Next DR
        Next i
 
        
         Try
            intErrNo = objAdmin.mtdGetBasePath(objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=&errmesg=" & lblErrMesage.Text & "&redirect=")
        End Try

        rdCrystalViewer.Load(objMapPath & "Web\EN\PR\Reports\Crystal\PR_Rpt_ContractPayDet.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(myDS) 
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
            DiskOpts.DiskFileName = objMapPath & "web\ftp\PR_Rpt_ContractPayDet.pdf"
            rdCrystalViewer.ExportOptions.DestinationOptions = DiskOpts
            rdCrystalViewer.Export()

            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../../ftp/PR_Rpt_ContractPayDet.pdf"">")
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

        Dim ParamDiscreteValue1 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue2 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue3 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue4 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue5 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue6 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue7 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue8 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue9 As New ParameterDiscreteValue()

        Dim crParameterValues1 As ParameterValues
        Dim crParameterValues2 As ParameterValues
        Dim crParameterValues3 As ParameterValues
        Dim crParameterValues4 As ParameterValues
        Dim crParameterValues5 As ParameterValues
        Dim crParameterValues6 As ParameterValues
        Dim crParameterValues7 As ParameterValues
        Dim crParameterValues8 As ParameterValues
        Dim crParameterValues9 As ParameterValues

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        ParamFields = crvView.ParameterFieldInfo
     
        ParamField1 = ParamFields.Item("strPrintDate")  
        ParamField2 = ParamFields.Item("strPrintedBy")
        ParamField3 = ParamFields.Item("strCompName")
        ParamField4 = ParamFields.Item("strLocName")
        ParamField5 = ParamFields.Item("strStatus")
        ParamField6 = ParamFields.Item("AccountTag")
        ParamField7 = ParamFields.Item("BlockTag")
        ParamField8 = ParamFields.Item("VehicleTag")
        ParamField9 = ParamFields.Item("VehExpCodeTag")

        crParameterValues1 = ParamField1.CurrentValues
        crParameterValues2 = ParamField2.CurrentValues
        crParameterValues3 = ParamField3.CurrentValues
        crParameterValues4 = ParamField4.CurrentValues
        crParameterValues5 = ParamField5.CurrentValues
        crParameterValues6 = ParamField6.CurrentValues
        crParameterValues7 = ParamField7.CurrentValues
        crParameterValues8 = ParamField8.CurrentValues
        crParameterValues9 = ParamField9.CurrentValues

        ParamDiscreteValue1.value = strPrintDate
        ParamDiscreteValue2.value = strUserName
        ParamDiscreteValue3.value = strCompName
        ParamDiscreteValue4.value = strLocName
        ParamDiscreteValue5.value = strStatus
        ParamDiscreteValue6.value = AccountTag
        ParamDiscreteValue7.value = BlockTag
        ParamDiscreteValue8.value = VehicleTag
        ParamDiscreteValue9.value = VehExpenseCodeTag

        crParameterValues1.Add(ParamDiscreteValue1)
        crParameterValues2.Add(ParamDiscreteValue2)
        crParameterValues3.Add(ParamDiscreteValue3)
        crParameterValues4.Add(ParamDiscreteValue4)
        crParameterValues5.Add(ParamDiscreteValue5)
        crParameterValues6.Add(ParamDiscreteValue6)
        crParameterValues7.Add(ParamDiscreteValue7)
        crParameterValues8.Add(ParamDiscreteValue8)
        crParameterValues9.Add(ParamDiscreteValue9)

        PFDefs(0).ApplyCurrentValues(crParameterValues1)
        PFDefs(1).ApplyCurrentValues(crParameterValues2)
        PFDefs(2).ApplyCurrentValues(crParameterValues3)
        PFDefs(3).ApplyCurrentValues(crParameterValues4)
        PFDefs(4).ApplyCurrentValues(crParameterValues5)
        PFDefs(5).ApplyCurrentValues(crParameterValues6)
        PFDefs(6).ApplyCurrentValues(crParameterValues7)
        PFDefs(7).ApplyCurrentValues(crParameterValues8)
        PFDefs(8).ApplyCurrentValues(crParameterValues9)

        crvView.ParameterFieldInfo = paramFields
    End Sub



End Class

