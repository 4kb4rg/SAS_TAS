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

Imports agri.BD.clsReport
Imports agri.Admin.clsShare
Imports agri.Admin.clsComp
Imports agri.Admin.clsLoc
Imports agri.GlobalHdl.clsGlobalHdl

Public Class BD_StdRpt_FFBProductionDetPreview : inherits page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCriteria As HtmlTable
    Protected WithEvents tblCrystal As HtmlTable
    Protected WithEvents txtInvoiceRcvID As TextBox
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents dgResult As DataGrid

    Dim objBD As New agri.BD.clsReport()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objComp As New agri.Admin.clsComp()
    Dim objLoc As New agri.Admin.clsLoc()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim rdCrystalViewer As New ReportDocument()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String

    Dim strYear As String
    Dim strLocType As String
    

    Public Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strLocType = Session("SS_LOCTYPE")
        
        crvView.Visible = False  

        strYear = Trim(Request.QueryString("Year"))
        
        Bind_ITEM(TRUE)
    End Sub

    Sub Bind_ITEM(ByVal blnIsPDFFormat As Boolean)
        Dim crLogonInfo As CrystalDecisions.Shared.TableLogOnInfo
        Dim objRptDs As New Dataset()
        Dim objRptDs2 As New Dataset()
        Dim objRptDs3 As New Dataset()
        Dim objMapPath As New Object()
        Dim objCompDs As New Dataset()
        Dim objLocDs As New Dataset()
        Dim strOpCd As String = "BD_STDRPT_FFB_PRODUCTION_BUDGET_DETAIL"
        Dim strOpCd_Comp As String = "ADMIN_CLSCOMP_COMPANY_DETAILS_GET"
        Dim strOpCod_Loc As String = "ADMIN_CLSLOC_LOCATION_DETAILS_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim strSearch As String
        Dim strSortItem As String
        Dim intCnt As Integer
        Dim I As Integer, J As Integer, K As Integer
        Dim strCompName As String
        Dim strLocName As String
        Dim dr As DataRow


        
        strParam =  strLocation & "|" & strYear & "|1"   
        
        Try
            intErrNo = objBD.mtdGetReport_FFBProductionBudgetDetail(strOpCd, strParam, objRptDs, objMapPath)
            
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        strParam =  strLocation & "|" & strYear & "|3"   
        Try
            intErrNo = objBD.mtdGetReport_FFBProductionBudgetDetail(strOpCd, strParam, objRptDs2, objMapPath)
            
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        strParam =  strLocation & "|" & strYear & "|2"   
        Try
            intErrNo = objBD.mtdGetReport_FFBProductionBudgetDetail(strOpCd, strParam, objRptDs3, objMapPath)
            
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

        
        
        

        objRptDs.Tables(0).TableName = "BD_StdRpt_FFBProductionDetInt"
        objRptDs.Tables.Add(objRptDs2.Tables(0).Copy())
        objRptDs.Tables(1).TableName = "BD_StdRpt_FFBProductionDetAss"
        objRptDs.Tables.Add(objRptDs3.Tables(0).Copy())
        objRptDs.Tables(2).TableName = "BD_StdRpt_FFBProductionDetExt"
        
        For I = 0 To 2
            For J = 0 To objRptDs.Tables(I).Rows.Count - 1
                For K = 2 To 13
                    If IsDBNull(objRptDs.Tables(I).Rows(J).Item(K)) Then
                        objRptDs.Tables(I).Rows(J).Item(K) = 0.0
                    End If
                Next
            Next
        Next

        rdCrystalViewer.Load(objMapPath & "Web\" & strLangCode & "\Reports\Crystal\BD_StdRpt_FFBProductionDet.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs)

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
            DiskOpts.DiskFileName = objMapPath & "web\ftp\BD_StdRpt_FFBProductionDet.pdf"
            rdCrystalViewer.ExportOptions.DestinationOptions = DiskOpts
            rdCrystalViewer.Export()

            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/BD_StdRpt_FFBProductionDet.pdf"">")
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
        Dim paramField14 As New ParameterField()
        Dim paramField15 As New ParameterField()
        Dim paramField16 As New ParameterField()
        Dim paramField17 As New ParameterField()
        Dim paramField18 As New ParameterField()
        Dim paramField19 As New ParameterField()
        Dim paramField20 As New ParameterField()

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

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        ParamFields = crvView.ParameterFieldInfo
     
        paramField1 = ParamFields.Item("ParamCompanyName") 
        paramField2 = ParamFields.Item("ParamLocation")
        paramField3 = ParamFields.Item("ParamDecimal")
        paramField4 = ParamFields.Item("ParamRptID")
        paramField5 = ParamFields.Item("ParamRptName")
        paramField6 = ParamFields.Item("ParamUserName")
        paramField7 = ParamFields.Item("ParamJan") 
        paramField8 = ParamFields.Item("ParamFeb")
        paramField9 = ParamFields.Item("ParamMar")
        paramField10 = ParamFields.Item("ParamApr")
        paramField11 = ParamFields.Item("ParamMay")
        paramField12 = ParamFields.Item("ParamJun")
        paramField13 = ParamFields.Item("ParamJul") 
        paramField14 = ParamFields.Item("ParamAug")
        paramField15 = ParamFields.Item("ParamSep")
        paramField16 = ParamFields.Item("ParamOct")
        paramField17 = ParamFields.Item("ParamNov")
        paramField18 = ParamFields.Item("ParamDec")
        paramField19 = ParamFields.Item("ParamYear")
        paramField20 = ParamFields.Item("lblLocationTag")

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

        ParamDiscreteValue1.value = Session("SS_COMPANYNAME")
        ParamDiscreteValue2.value = Session("SS_LOCATION")
        ParamDiscreteValue3.value = Request.QueryString("Decimal")
        ParamDiscreteValue4.value = Request.QueryString("RptID")
        ParamDiscreteValue5.value = Request.QueryString("RptName")
        ParamDiscreteValue6.value = Session("SS_USERNAME")
        ParamDiscreteValue7.value = "01/" & strYear
        ParamDiscreteValue8.value = "02/" & strYear
        ParamDiscreteValue9.value = "03/" & strYear
        ParamDiscreteValue10.value = "04/" & strYear
        ParamDiscreteValue11.value = "05/" & strYear
        ParamDiscreteValue12.value = "06/" & strYear
        ParamDiscreteValue13.value = "07/" & strYear
        ParamDiscreteValue14.value = "08/" & strYear
        ParamDiscreteValue15.value = "09/" & strYear
        ParamDiscreteValue16.value = "10/" & strYear
        ParamDiscreteValue17.value = "11/" & strYear
        ParamDiscreteValue18.value = "12/" & strYear
        ParamDiscreteValue19.value = strYear
        ParamDiscreteValue20.value = Request.QueryString("lblLocationTag")

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

        crvView.ParameterFieldInfo = paramFields

    End Sub

End Class

