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

Public Class AR_StdRpt_BillPartyListPreview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objAR As New agri.BI.clsReport()
    Dim objGLSetup As New agri.GL.clsSetup()   
    Dim objARTrx As New agri.BI.clsTrx()
    Dim objAdmin As New agri.Admin.clsShare()

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
    Dim strDecimal As String

    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
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

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_ARACCMONTH")
        strAccYear = Session("SS_ARACCYEAR")
        strLangCode = Session("SS_LANGCODE")

        Session("SS_Decimal") = Request.QueryString("Decimal")
        Session("SS_RptID") = Request.QueryString("RptID")
        Session("SS_RptName") = Request.QueryString("RptName")
        Session("SS_Name") = Request.QueryString("Name")        
        Session("SS_Status") = Request.QueryString("Status")
        Session("SS_UpdatedBy") = Request.QueryString("UpdatedBy")

        Session("SS_BillPartyCode") = Request.QueryString("BillPartyCode")
        Session("SS_ContactPerson") = Request.QueryString("ContactPerson")
        Session("SS_Town") = Request.QueryString("Town")
        Session("SS_State") = Request.QueryString("State")
        Session("SS_PostCode") = Request.QueryString("PostCode")
        Session("SS_Country") = Request.QueryString("Country")
        Session("SS_TelNo") = Request.QueryString("TelNo")
        Session("SS_FaxNo") = Request.QueryString("FaxNo")
        Session("SS_Email") = Request.QueryString("Email")
        Session("SS_CreditTerm") = Request.QueryString("CreditTerm")
        Session("SS_CreditTermType") = Request.QueryString("CreditTermType")
        Session("SS_CreditLimit") = Request.QueryString("CreditLimit")
        Session("SS_LblCOA") = Request.QueryString("LblCOA")
        Session("SS_COA") = Request.QueryString("COA")
        Session("SS_lblLocation") = Request.QueryString("lblLocation")
        Session("SS_LblBillParty") = Request.QueryString("lblBillParty")


        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            BindReport()
        End If
    End Sub


    Sub BindReport()

        Dim objRptDs As New DataSet()
        Dim objMapPath As String

        Dim SearchStr As String
        Dim strParam As String

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOpCdPR_GET As String = "AR_STDRPT_BILLPARTY_LIST"



        If Request.QueryString("Status") = "All" Then
            SearchStr = SearchStr & "AND GLB.Status LIKE '%' "
        ElseIf Request.QueryString("Status") = "Active" Then
            SearchStr = SearchStr & "AND GLB.Status = '" & objGLSetup.EnumBillPartyStatus.Active & "'"
        ElseIf Request.QueryString("Status") = "Deleted" Then
            SearchStr = SearchStr & "AND GLB.Status = '" & objGLSetup.EnumBillPartyStatus.Deleted & "'"
        End If

        If Not Request.QueryString("BillPartyCode") = "" Then
            SearchStr = SearchStr & " AND GLB.BillPartyCode LIKE '" & Request.QueryString("BillPartyCode") & "'"
        End If

        If Not Request.QueryString("Name") = "" Then
            SearchStr = SearchStr & " AND RTRIM(GLB.Name) LIKE '" & Request.QueryString("Name") & "'"
        End If

        If Not Request.QueryString("ContactPerson") = "" Then
            SearchStr = SearchStr & " AND RTRIM(GLB.ContactPerson) LIKE '" & Request.QueryString("ContactPerson") & "'"
        End If

        If Not Request.QueryString("Town") = "" Then
            SearchStr = SearchStr & " AND RTRIM(GLB.Town) LIKE '" & Request.QueryString("Town") & "'"
        End If

        If Not Request.QueryString("State") = "" Then
            SearchStr = SearchStr & " AND RTRIM(GLB.State) LIKE '" & Request.QueryString("State") & "'"
        End If

        If Not Request.QueryString("PostCode") = "" Then
            SearchStr = SearchStr & " AND GLB.PostCode LIKE '" & Request.QueryString("PostCode") & "'"
        End If

        If Not Request.QueryString("PostCode") = "" Then
            SearchStr = SearchStr & " AND GLB.PostCode LIKE '" & Request.QueryString("PostCode") & "'"
        End If

        If Not Request.QueryString("Country") = "" Then
            SearchStr = SearchStr & " AND GLB.CountryCode LIKE '" & Request.QueryString("Country") & "'"
        End If

        If Not Request.QueryString("TelNo") = "" Then
            SearchStr = SearchStr & " AND GLB.TelNo LIKE '" & Request.QueryString("TelNo") & "'"
        End If

        If Not Request.QueryString("FaxNo") = "" Then
            SearchStr = SearchStr & " AND GLB.FaxNo LIKE '" & Request.QueryString("FaxNo") & "'"
        End If

        If Not Request.QueryString("Email") = "" Then
            SearchStr = SearchStr & " AND GLB.Email LIKE '" & Request.QueryString("Email") & "'"
        End If

        If Not Request.QueryString("CreditTerm") = "" Then
            SearchStr = SearchStr & " AND GLB.CreditTerm LIKE '" & Request.QueryString("CreditTerm") & "'"
        End If

        If Not Request.QueryString("CreditTermType") = "" Then
            SearchStr = SearchStr & " AND GLB.TermType LIKE '" & Request.QueryString("CreditTermType") & "'"
        End If

        If Not Request.QueryString("CreditLimit") = "" Then
            SearchStr = SearchStr & " AND GLB.CreditLimit LIKE '" & Request.QueryString("CreditLimit") & "'"
        End If

        If Not Request.QueryString("COA") = "" Then
            SearchStr = SearchStr & " AND GLB.AccCode LIKE '" & Request.QueryString("COA") & "'"
        End If


            strParam = "||||" & SearchStr




        Try
            intErrNo = objAR.mtdGetReport_BillPartyList(strOpCdPR_GET, strParam, objRptDs, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_WM_Seller_MASTER_LIST_REPORT&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try



        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\AR_StdRpt_BillPartyList.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3

        crvView.Visible = False                         
        crvView.ReportSource = rdCrystalViewer          
        crvView.DataBind()                              

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\AR_StdRpt_BillPartyList.pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/AR_StdRpt_BillPartyList.pdf"">")
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
        Dim paramField25 As New ParameterField()
        Dim paramField26 As New ParameterField()


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
        Dim ParamDiscreteValue25 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue26 As New ParameterDiscreteValue()


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
        Dim crParameterValues25 As ParameterValues
        Dim crParameterValues26 As ParameterValues


        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("ParamCompanyName")
        paramField2 = paramFields.Item("ParamLocation")
        paramField3 = paramFields.Item("ParamUserName")
        paramField4 = paramFields.Item("ParamDecimal")
        paramField5 = paramFields.Item("ParamBillPartyCode")
        paramField6 = paramFields.Item("ParamContactPerson")        
        paramField7 = paramFields.Item("ParamRptName")
        paramField8 = paramFields.Item("ParamStatus")
        paramField9 = paramFields.Item("ParamTown")
        paramField10 = paramFields.Item("ParamName")
        paramField11 = paramFields.Item("ParamState")
        paramField12 = paramFields.Item("ParamPostCode")
        paramField13 = paramFields.Item("ParamRptID")
        paramField14 = paramFields.Item("ParamAccMth")
        paramField15 = paramFields.Item("ParamAccYear")
        paramField16 = paramFields.Item("ParamTelNo")
        paramField17 = paramFields.Item("ParamFaxNo")
        paramField18 = paramFields.Item("ParamEmail")
        paramField19 = paramFields.Item("ParamCreditTerm")
        paramField20 = paramFields.Item("ParamCreditTermType")
        paramField21 = paramFields.Item("ParamCreditLimit")
        paramField22 = paramFields.Item("ParamlblCOA")
        paramField23 = paramFields.Item("ParamCOA")
        paramField24 = paramFields.Item("ParamlblLocation")
        paramField25 = paramFields.Item("ParamCountry")
        paramField26 = paramFields.Item("ParamLblBillParty")


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
        crParameterValues25 = paramField25.CurrentValues
        crParameterValues26 = paramField26.CurrentValues


        ParamDiscreteValue1.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue2.Value = Session("SS_LOC")
        ParamDiscreteValue3.Value = Session("SS_USERNAME")
        ParamDiscreteValue4.Value = 2
        ParamDiscreteValue5.Value = Session("SS_BillPartyCode")
        ParamDiscreteValue6.Value = Session("SS_ContactPerson")
        ParamDiscreteValue7.Value = Session("SS_RPTNAME")
        ParamDiscreteValue8.Value = Session("SS_STATUS")
        ParamDiscreteValue9.Value = Session("SS_Town")
        ParamDiscreteValue10.Value = Session("SS_NAME")
        ParamDiscreteValue11.Value = Session("SS_State")
        ParamDiscreteValue12.Value = Session("SS_PostCode")
        ParamDiscreteValue13.Value = Session("SS_RPTID")
        ParamDiscreteValue14.Value = strAccMonth
        ParamDiscreteValue15.Value = strAccYear
        ParamDiscreteValue16.Value = Session("SS_TelNo")
        ParamDiscreteValue17.Value = Session("SS_FaxNo")
        ParamDiscreteValue18.Value = Session("SS_Email")
        ParamDiscreteValue19.Value = Session("SS_CreditTerm")
        ParamDiscreteValue20.Value = Session("SS_CreditTermType")
        ParamDiscreteValue21.Value = Session("SS_CreditLimit")
        ParamDiscreteValue22.Value = Session("SS_lblCOA")
        ParamDiscreteValue23.Value = Session("SS_COA")
        ParamDiscreteValue24.Value = Session("SS_lblLocation")
        ParamDiscreteValue25.Value = Session("SS_Country")
        ParamDiscreteValue26.Value = Session("SS_LblBillParty")


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
        crParameterValues25.Add(ParamDiscreteValue25)
        crParameterValues26.Add(ParamDiscreteValue26)


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
        PFDefs(24).ApplyCurrentValues(crParameterValues25)
        PFDefs(25).ApplyCurrentValues(crParameterValues26)

        crvView.ParameterFieldInfo = paramFields
    End Sub
End Class
