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


Public Class WS_Rpt_ServTypeList : inherits page

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

    Dim strServTypeTag As String
    Dim strDescTag As String
    Dim strTitleTag As String

    Dim strStatus As String
    Dim strServType As String
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

        strServTypeTag = Trim(Request.QueryString("strServTypeTag"))  
        strDescTag = Trim(Request.QueryString("strDescTag")) 
        strTitleTag = Trim(Request.QueryString("strTitleTag"))

        strStatus = Trim(Request.QueryString("strStatus"))
        strServType = Trim(Request.QueryString("strServType"))
        strDescription = Trim(Request.QueryString("strDescription"))
        strUpdateBy = Trim(Request.QueryString("strUpdateBy"))
        strSortExp = Trim(Request.QueryString("strSortExp"))
        strSortCol = Trim(Request.QueryString("strSortCol"))

        Bind_ITEM(TRUE)

    End Sub

    Sub Bind_ITEM(ByVal blnIsPDFFormat As Boolean)
        Dim crLogonInfo As CrystalDecisions.Shared.TableLogOnInfo
        Dim objRptDs As New Dataset()
        Dim objMapPath As New Object()
        Dim strOpCd As String = "WS_CLSSETUP_SERVTYPE_LIST_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim strSearch As String
        Dim strSortItem As String
        Dim intCnt As Integer
        Dim dr As DataRow

        strParam = strServType & "|" & _
                   strDescription & "|" & _
                   strStatus & "|" & _
                   strUpdateBy & "|" & _
                   strSortExp & "|" & _
                   strSortCol & "|"

        Try
            intErrNo = objWS.mtdGetServType(strOpCd, _
                                            strParam, _
                                            objRptDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=SERVTYPELIST_GET_SERVTYPE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        Try
           intErrNo = objAdmin.mtdGetBasePath(objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_SETUP_RPT_SERVTYPE_GET_MAP_PATH&errmesg=" & lblErrMesage.Text & "&redirect=")
        End Try


        For intCnt=0 To objRptDs.Tables(0).Rows.Count - 1
            objRptDs.Tables(0).Rows(intCnt).Item("ServTypeCode") = Trim(objRptDs.Tables(0).Rows(intCnt).Item("ServTypeCode"))
            objRptDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objRptDs.Tables(0).Rows(intCnt).Item("Description"))
            objRptDs.Tables(0).Rows(intCnt).Item("Status") = Trim(objRptDs.Tables(0).Rows(intCnt).Item("Status"))
            objRptDs.Tables(0).Rows(intCnt).Item("CreateDate") = Trim(objRptDs.Tables(0).Rows(intCnt).Item("CreateDate"))
            objRptDs.Tables(0).Rows(intCnt).Item("UpdateDate") = Trim(objRptDs.Tables(0).Rows(intCnt).Item("UpdateDate"))
            objRptDs.Tables(0).Rows(intCnt).Item("UpdateId") = Trim(objRptDs.Tables(0).Rows(intCnt).Item("UpdateId"))
        Next
        
        For intCnt=0 To objRptDs.Tables(0).Rows.Count - 1
            objRptDs.Tables(0).Rows(intCnt).Item("Status") = objWS.mtdGetStatus(objRptDs.Tables(0).Rows(intCnt).Item("Status"))
            objRptDs.Tables(0).Rows(intCnt).Item("UpdateDate") = objGlobal.GetLongDate(objRptDs.Tables(0).Rows(intCnt).Item("UpdateDate"))
        Next


        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\WS\Reports\Crystal\WS_Rpt_ServTypeList.rpt", OpenReportMethod.OpenReportByTempCopy)
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
            DiskOpts.DiskFileName = objMapPath & "web\ftp\WS_Rpt_ServTypeList.pdf"
            rdCrystalViewer.ExportOptions.DestinationOptions = DiskOpts
            rdCrystalViewer.Export()

            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../../ftp/WS_Rpt_ServTypeList.pdf"">")
        End If

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


        Dim crParameterValues1 As ParameterValues
        Dim crParameterValues2 As ParameterValues
        Dim crParameterValues3 As ParameterValues
        Dim crParameterValues4 As ParameterValues
        Dim crParameterValues5 As ParameterValues


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

        crParameterValues1 = paramField1.CurrentValues
        crParameterValues2 = paramField2.CurrentValues
        crParameterValues3 = paramField3.CurrentValues
        crParameterValues4 = paramField4.CurrentValues
        crParameterValues5 = paramField5.CurrentValues


        ParamDiscreteValue1.value = Session("SS_COMPANYNAME")
        ParamDiscreteValue2.value = Session("SS_LOCATIONNAME")
        ParamDiscreteValue3.value = strServTypeTag
        ParamDiscreteValue4.value = strDescTag
        ParamDiscreteValue5.value = UCase(strTitleTag)
  
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

