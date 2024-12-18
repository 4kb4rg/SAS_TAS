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
Imports agri.GL.clsReport
Imports agri.Admin.clsShare
Imports agri.Admin.clsComp
Imports agri.Admin.clsLoc
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.PWSystem.clsLangCap


Public Class GL_Rpt_PDODet : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents dgResult As DataGrid
    Protected WithEvents lblErrMesage As Label

    Dim objIn As New agri.IN.clsTrx()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim rdCrystalViewer As New ReportDocument()
    Dim objLangCapDs As New Object()
    Dim objOk As New agri.GL.clsReport

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String

    Dim strStatus As String
    Dim strStockTxId As String
    Dim strPrintDate As String
    Dim strSortLine As String
    Dim strDocTitle As String
    Dim strRptType As String

    Dim strCompName As String
    Dim strLocName As String
    Dim strUserName As String
    Dim strLocationTag As String
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String
    Dim objFTPFolder As String


    Public Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        strCompName = Session("SS_COMPANYNAME")
        strLocName = Session("SS_LOCATIONNAME")
        strUserName = Session("SS_USERNAME")
        strLocType = Session("SS_LOCTYPE")
        crvView.Visible = False

        strStatus = Trim(Request.QueryString("strStatus"))
        strStockTxId = Trim(Request.QueryString("TRXID"))
        strPrintDate = Trim(Request.QueryString("strPrintDate"))
        strSortLine = Trim(Request.QueryString("strSortLine"))
        strDocTitle = Trim(Request.QueryString("strDocTitle"))
        strRptType = Trim(Request.QueryString("RPTTYPE"))

        If strRptType = "PDO" Then
            Bind_ITEMPDO(True)
        Else
            Bind_ITEMPDOLPJ(True)
        End If
    End Sub

    Sub Bind_ITEMPDO(ByVal blnIsPDFFormat As Boolean)
        Dim crLogonInfo As CrystalDecisions.Shared.TableLogOnInfo
        Dim objRptDs As New DataSet()
        Dim objRptSubDs As New DataSet()
        Dim objMapPath As String
        Dim strOpCd_Get As String = "GL_STDRPT_PDO_GET_FOR_REPORT"
        Dim strOpCdDet_Get As String = "GL_STDRPT_PDO_DETAIL_GET_FOR_REPORT"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer

        strParamName = "LOC|TRID"
        strParamValue = strLocation & "|" & _
                        strStockTxId

        Try
            intErrNo = objOk.mtdGetReportDataCommon(strOpCd_Get, strParamName, strParamValue, objRptDs, objMapPath, objFTPFolder)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=&errmesg=" & Exp.Message & "&redirect=")
        End Try

        Try
            intErrNo = objOk.mtdGetReportDataCommon(strOpCdDet_Get, strParamName, strParamValue, objRptSubDs, objMapPath, objFTPFolder)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=&errmesg=" & Exp.Message & "&redirect=")
        End Try

        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\GL\reports\Crystal\GL_Rpt_PDD.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        rdCrystalViewer.Subreports.Item("PDDET").SetDataSource(objRptSubDs.Tables(0))

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
            DiskOpts.DiskFileName = objMapPath & "web\ftp\GL_Rpt_PDD.pdf"
            rdCrystalViewer.ExportOptions.DestinationOptions = DiskOpts
            rdCrystalViewer.Export()

            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../../ftp/GL_Rpt_PDD.pdf"">")
        End If

    End Sub

    Sub Bind_ITEMPDOLPJ(ByVal blnIsPDFFormat As Boolean)
        Dim crLogonInfo As CrystalDecisions.Shared.TableLogOnInfo
        Dim objRptDs As New DataSet()
        Dim objRptSubDs As New DataSet()
        Dim objMapPath As String
        Dim strOpCd_Get As String = "GL_STDRPT_PDO_LPJ_GET_FOR_REPORT"

        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer

        strParamName = "LOC|TRID"
        strParamValue = strLocation & "|" & _
                        strStockTxId

        Try
            intErrNo = objOk.mtdGetReportDataCommon(strOpCd_Get, strParamName, strParamValue, objRptDs, objMapPath, objFTPFolder)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=&errmesg=" & Exp.Message & "&redirect=")
        End Try
         
        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\GL\reports\Crystal\GL_Rpt_PDDLPJ.rpt", OpenReportMethod.OpenReportByTempCopy)
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
            DiskOpts.DiskFileName = objMapPath & "web\ftp\GL_Rpt_PDDLPJ.pdf"
            rdCrystalViewer.ExportOptions.DestinationOptions = DiskOpts
            rdCrystalViewer.Export()

            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../../ftp/GL_Rpt_PDDLPJ.pdf"">")
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


        Dim crParameterValues2 As ParameterValues
        Dim crParameterValues3 As ParameterValues
 

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        onload_GetLangCap()

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo


        paramField2 = paramFields.Item("strLocName")
        paramField3 = paramFields.Item("strCompName")
        

        crParameterValues2 = paramField2.CurrentValues
        crParameterValues3 = paramField3.CurrentValues
        
        ParamDiscreteValue2.Value = strLocName
        ParamDiscreteValue3.Value = strCompName
     

        crParameterValues2.Add(ParamDiscreteValue2)
        crParameterValues3.Add(ParamDiscreteValue3)
       


        PFDefs(0).ApplyCurrentValues(crParameterValues2)
        PFDefs(1).ApplyCurrentValues(crParameterValues3)
      
        crvView.ParameterFieldInfo = paramFields
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        strLocationTag = GetCaption(objLangCap.EnumLangCap.Location)
    End Sub

    Sub GetEntireLangCap()
        Dim strOpCode_BussTerm As String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
        Dim strParam As String
        Dim intErrNo As Integer

        strParam = strLangCode
        Try
            intErrNo = objLangCap.mtdGetBussTerm(strOpCode_BussTerm, _
                                                 strCompany, _
                                                 strLocation, _
                                                 strUserId, _
                                                 strAccMonth, _
                                                 strAccYear, _
                                                 objLangCapDs, _
                                                 strParam)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=&errmesg=" & lblErrMesage.Text & "&redirect=")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

        For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If strLocType = objAdminLoc.EnumLocType.Mill Then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                Else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                End If
                Exit For
            End If
        Next
    End Function

End Class

