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

Imports agri.WS.clsSetup
Imports agri.Admin.clsShare
Imports agri.GlobalHdl.clsGlobalHdl


Public Class WS_Rpt_WorkCodeList : inherits page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents lblErrMesage As Label
    Protected WithEvents EventData As DataGrid
    Protected WithEvents lblErrMessage As Label

    Dim objWS As New agri.WS.clsSetup()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim rdCrystalViewer As New ReportDocument()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String

    Dim strWorkCodeTag As String
    Dim strDescTag As String
    Dim strTitleTag As String
    Dim strServTypeTag As String
    Dim strAccCodeTag As String
    Dim strBlkCodeTag As String
    
    Dim strStatus As String
    Dim strWorkCode As String
    Dim strDescription As String
    Dim strUpdateBy As String
    Dim strSortExp As String
    Dim strSortCol As String
    

    Public Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        
        crvView.Visible = False  

        strWorkCodeTag = Trim(Request.QueryString("strWorkCodeTag"))  
        strDescTag = Trim(Request.QueryString("strDescTag")) 
        strTitleTag = Trim(Request.QueryString("strTitleTag"))

        strStatus = Trim(Request.QueryString("strStatus"))
        strWorkCode = Trim(Request.QueryString("strWorkCode"))
        strDescription = Trim(Request.QueryString("strDescription"))
        strUpdateBy = Trim(Request.QueryString("strUpdateBy"))
        strSortExp = Trim(Request.QueryString("strSortExp"))
        strSortCol = Trim(Request.QueryString("strSortCol"))

        strServTypeTag =  Trim(Request.QueryString("strServTypeTag"))  
        strAccCodeTag =  Trim(Request.QueryString("strAccCodeTag"))  
        strBlkCodeTag =  Trim(Request.QueryString("strBlkCodeTag"))  

        Bind_ITEM(TRUE)

    End Sub

    Sub Bind_ITEM(ByVal blnIsPDFFormat As Boolean)
        Dim crLogonInfo As CrystalDecisions.Shared.TableLogOnInfo
        Dim objRptDs As New Dataset()
        Dim objMapPath As New Object()
        Dim strOpCd As String = "WS_CLSSETUP_WORKCODE_LIST_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim strSearch As String
        Dim strSortItem As String
        Dim intCnt As Integer
        Dim dr As DataRow

        strParam = strWorkCode & "|" & _
                   strDescription & "|" & _
                   strStatus & "|" & _
                   strUpdateBy & "|" & _
                   strSortExp & "|" & _
                   strSortCol & "|"

        Try
            intErrNo = objWS.mtdGetWorkCode(strOpCd, _
                                            strParam, _
                                            objRptDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WORKCODELIST_GET_WORKCODE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        Try
           intErrNo = objAdmin.mtdGetBasePath(objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_SETUP_RPT_WORKCODE_GET_MAP_PATH&errmesg=" & lblErrMesage.Text & "&redirect=")
        End Try


        For intCnt=0 To objRptDs.Tables(0).Rows.Count - 1
            objRptDs.Tables(0).Rows(intCnt).Item("WorkCode") = Trim(objRptDs.Tables(0).Rows(intCnt).Item("WorkCode"))
            objRptDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objRptDs.Tables(0).Rows(intCnt).Item("Description"))
            objRptDs.Tables(0).Rows(intCnt).Item("Status") = Trim(objRptDs.Tables(0).Rows(intCnt).Item("Status"))
            objRptDs.Tables(0).Rows(intCnt).Item("CreateDate") = Trim(objRptDs.Tables(0).Rows(intCnt).Item("CreateDate"))
            objRptDs.Tables(0).Rows(intCnt).Item("UpdateDate") = Trim(objRptDs.Tables(0).Rows(intCnt).Item("UpdateDate"))
            objRptDs.Tables(0).Rows(intCnt).Item("UpdateId") = Trim(objRptDs.Tables(0).Rows(intCnt).Item("UpdateId"))
            objRptDs.Tables(0).Rows(intCnt).Item("ServTypeCode") = Trim(objRptDs.Tables(0).Rows(intCnt).Item("ServTypeCode"))
            objRptDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(objRptDs.Tables(0).Rows(intCnt).Item("AccCode"))
            objRptDs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(objRptDs.Tables(0).Rows(intCnt).Item("BlkCode"))
        Next
        
        For intCnt=0 To objRptDs.Tables(0).Rows.Count - 1
            objRptDs.Tables(0).Rows(intCnt).Item("Status") = objWS.mtdGetStatus(objRptDs.Tables(0).Rows(intCnt).Item("Status"))
            objRptDs.Tables(0).Rows(intCnt).Item("UpdateDate") = objGlobal.GetLongDate(objRptDs.Tables(0).Rows(intCnt).Item("UpdateDate"))
        Next


        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\WS\Reports\Crystal\WS_Rpt_WorkCodeList.rpt", OpenReportMethod.OpenReportByTempCopy)
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
            DiskOpts.DiskFileName = objMapPath & "web\ftp\WS_Rpt_WorkCodeList.pdf"
            rdCrystalViewer.ExportOptions.DestinationOptions = DiskOpts
            rdCrystalViewer.Export()

            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../../ftp/WS_Rpt_WorkCodeList.pdf"">")
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

        Dim ParamDiscreteValue1 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue2 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue3 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue4 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue5 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue6 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue7 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue8 As New ParameterDiscreteValue()

        Dim crParameterValues1 As ParameterValues
        Dim crParameterValues2 As ParameterValues
        Dim crParameterValues3 As ParameterValues
        Dim crParameterValues4 As ParameterValues
        Dim crParameterValues5 As ParameterValues
        Dim crParameterValues6 As ParameterValues
        Dim crParameterValues7 As ParameterValues
        Dim crParameterValues8 As ParameterValues

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        ParamFields = crvView.ParameterFieldInfo
     
        paramField1 = ParamFields.Item("strCompName") 
        paramField2 = ParamFields.Item("strLocName") 
        paramField3 = ParamFields.Item("strServTypeTag")
        paramField4 = ParamFields.Item("strDescTag")
        paramField5 = ParamFields.Item("strTitleTag")
        paramField6 = ParamFields.Item("strWorkCodeTag")
        paramField7 = ParamFields.Item("strAccCodeTag")
        paramField8 = ParamFields.Item("strBlkCodeTag")

        crParameterValues1 = paramField1.CurrentValues
        crParameterValues2 = paramField2.CurrentValues
        crParameterValues3 = paramField3.CurrentValues
        crParameterValues4 = paramField4.CurrentValues
        crParameterValues5 = paramField5.CurrentValues
        crParameterValues6 = paramField6.CurrentValues
        crParameterValues7 = paramField7.CurrentValues
        crParameterValues8 = paramField8.CurrentValues

        ParamDiscreteValue1.value = Session("SS_COMPANYNAME")
        ParamDiscreteValue2.value = Session("SS_LOCATIONNAME")
        ParamDiscreteValue3.value = strServTypeTag
        ParamDiscreteValue4.value = strDescTag
        ParamDiscreteValue5.value = UCase(strTitleTag)
        ParamDiscreteValue6.value = strWorkCodeTag
        ParamDiscreteValue7.value = strAccCodeTag
        ParamDiscreteValue8.value = strBlkCodeTag

        crParameterValues1.Add(ParamDiscreteValue1)
        crParameterValues2.Add(ParamDiscreteValue2)
        crParameterValues3.Add(ParamDiscreteValue3)
        crParameterValues4.Add(ParamDiscreteValue4)
        crParameterValues5.Add(ParamDiscreteValue5)
        crParameterValues6.Add(ParamDiscreteValue6)
        crParameterValues7.Add(ParamDiscreteValue7)
        crParameterValues8.Add(ParamDiscreteValue8)

        PFDefs(0).ApplyCurrentValues(crParameterValues1)
        PFDefs(1).ApplyCurrentValues(crParameterValues2)
        PFDefs(2).ApplyCurrentValues(crParameterValues3)
        PFDefs(3).ApplyCurrentValues(crParameterValues4)
        PFDefs(4).ApplyCurrentValues(crParameterValues5)
        PFDefs(5).ApplyCurrentValues(crParameterValues6)
        PFDefs(6).ApplyCurrentValues(crParameterValues7)
        PFDefs(7).ApplyCurrentValues(crParameterValues8)


        crvView.ParameterFieldInfo = paramFields
    End Sub

End Class

