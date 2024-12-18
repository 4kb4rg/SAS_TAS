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

Imports agri.PM.clsSetup
Imports agri.Admin.clsShare
Imports agri.Admin.clsComp
Imports agri.Admin.clsLoc
Imports agri.GlobalHdl.clsGlobalHdl


Public Class PM_Rpt_UllageVolumeConversionMasterList : inherits page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCriteria As HtmlTable
    Protected WithEvents tblCrystal As HtmlTable
    Protected WithEvents txtInvoiceRcvID As TextBox
    Protected WithEvents lblErrMesage As Label
    Protected WithEvents dgResult As DataGrid

    Dim objPM As New agri.PM.clsSetup()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objComp As New agri.Admin.clsComp()
    Dim objLoc As New agri.Admin.clsLoc()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim rdCrystalViewer As New ReportDocument()

    Dim strCompany As String
    Dim strLocation As String
    Dim strCompName As String
    Dim strLocName As String
    Dim strUserId As String
    Dim strLangCode As String

    Dim strTableCode As String
    Dim strUllage As String
    Dim strVolume As String
    Dim strStatus As String
    Dim strUpdateBy As String
    
    Dim strSortExp As String
    Dim strSortCol As String

    Dim TitleTag As String
    Dim TableCodeTag As String
    Dim UllageTag As String
    Dim VolumeTag As String

    Public Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        
        crvView.Visible = False  

        strTableCode = Trim(Request.QueryString("strTableCode"))
        strUllage = Trim(Request.QueryString("strUllage"))
        strVolume = Trim(Request.QueryString("strVolume"))
        strStatus = Trim(Request.QueryString("strStatus"))
        strUpdateBy = Trim(Request.QueryString("strUpdateBy"))
        strSortExp = Trim(Request.QueryString("strSortExp"))
        strSortCol = Trim(Request.QueryString("strSortCol"))
        TitleTag = Trim(Request.QueryString("TitleTag"))
        TableCodeTag = Trim(Request.QueryString("TableCodeTag"))
        UllageTag = Trim(Request.QueryString("UllageTag"))
        VolumeTag = Trim(Request.QueryString("VolumeTag"))

        Bind_ITEM(TRUE)

    End Sub

    Sub Bind_ITEM(ByVal blnIsPDFFormat As Boolean)
        Dim crLogonInfo As CrystalDecisions.Shared.TableLogOnInfo
        Dim objRptDs As New Dataset()
        Dim objMapPath As New Object()
        Dim objCompDs As New Dataset()
        Dim objLocDs As New Dataset()
        Dim strOpCd As String = "PM_CLSSETUP_UVCONVERSION_GET"
        Dim strOpCd_Comp As String = "ADMIN_CLSCOMP_COMPANY_DETAILS_GET"
        Dim strOpCod_Loc As String = "ADMIN_CLSLOC_LOCATION_DETAILS_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim strSearch As String
        Dim strSortItem As String
        Dim intCnt As Integer
        
        Dim dr As DataRow

        strSearch =  " AND UVC.Status like '" & IIF(Not strStatus = "", strStatus, "%" ) & "' "

        If NOT strTableCode = "" Then
            strSearch =  strSearch & " AND UVC.UVTableCode like '" & strTableCode & "%' "
        End If
        
        If NOT strUllage = "" Then
            strSearch = strSearch & " AND UVC. like '" & strUllage & "%' "
        End If
        
        If NOT strVolume = "" Then
            strSearch = strSearch & " AND UVC. like '" & strVolume & "%' "
        End If
        
        If NOT strUpdateBy = "" Then
            strSearch = strSearch & " AND usr.UserName like '" & strUpdateBy & "%' "
        End If
        
        strSortItem = "ORDER BY " & strSortExp & " " & strSortCol
        
        strParam =  strSortItem & "|" & strSearch

        Try
            intErrNo = objPM.mtdGetUllageVolumeTable(strOpCd, strParam, objRptDs)
            
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=&errmesg=" & lblErrMesage.Text & "&redirect=")
        End Try

        Try
            intErrNo = objAdmin.mtdGetBasePath(objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=&errmesg=" & lblErrMesage.Text & "&redirect=")
        End Try

        Try
            intErrNo = objComp.mtdGetComp(strOpCd_Comp, strCompany, objCompDs, True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=&errmesg=" & lblErrMesage.Text & "&redirect=")
        End Try
        strCompName = Trim(objCompDs.Tables(0).Rows(0).Item("CompName"))

        Try
            intErrNo = objLoc.mtdGetLocDetail(strOpCod_Loc, "", "", "", objLocDs, strLocation)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=&errmesg=" & lblErrMesage.Text & "&redirect=")
        End Try
        strLocName = Trim(objLocDs.Tables(0).Rows(0).Item("Description"))

        For intCnt=0 To objRptDs.Tables(0).Rows.Count - 1
            objRptDs.Tables(0).Rows(intCnt).Item(0) = Trim(objRptDs.Tables(0).Rows(intCnt).Item(0))
        Next
        
        For intCnt=0 To objRptDs.Tables(0).Rows.Count - 1
            objRptDs.Tables(0).Rows(intCnt).Item("Status") = objPM.mtdGetUllageVolumeConversionStatus(objRptDs.Tables(0).Rows(intCnt).Item("Status"))
            objRptDs.Tables(0).Rows(intCnt).Item("UpdateDate") = objGlobal.GetLongDate(objRptDs.Tables(0).Rows(intCnt).Item("UpdateDate"))
        Next

        rdCrystalViewer.Load(objMapPath & "Web\EN\PM\Reports\Crystal\PM_Rpt_UllageVolumeConversionMasterList.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Portrait
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA4

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
            DiskOpts.DiskFileName = objMapPath & "web\ftp\PM_Rpt_UllageVolumeConversionMasterList.pdf"
            rdCrystalViewer.ExportOptions.DestinationOptions = DiskOpts
            rdCrystalViewer.Export()

            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../../ftp/PM_Rpt_UllageVolumeConversionMasterList.pdf"">")
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
        
        Dim ParamDiscreteValue1 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue2 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue3 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue4 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue5 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue6 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue7 As New ParameterDiscreteValue()

        Dim crParameterValues1 As ParameterValues
        Dim crParameterValues2 As ParameterValues
        Dim crParameterValues3 As ParameterValues
        Dim crParameterValues4 As ParameterValues
        Dim crParameterValues5 As ParameterValues
        Dim crParameterValues6 As ParameterValues
        Dim crParameterValues7 As ParameterValues

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        ParamFields = crvView.ParameterFieldInfo
     
        paramField1 = ParamFields.Item("CompanyTag") 
        paramField2 = ParamFields.Item("LocationTag")
        paramField3 = ParamFields.Item("TitleTag")
        paramField4 = ParamFields.Item("TableCodeTag")
        paramField5 = ParamFields.Item("UllageTag")
        paramField6 = ParamFields.Item("VolumeTag")
        paramField7 = ParamFields.Item("ParamUserName")

        crParameterValues1 = paramField1.CurrentValues
        crParameterValues2 = paramField2.CurrentValues
        crParameterValues3 = paramField3.CurrentValues
        crParameterValues4 = paramField4.CurrentValues
        crParameterValues5 = paramField5.CurrentValues
        crParameterValues6 = paramField6.CurrentValues
        crParameterValues7 = paramField7.CurrentValues

        ParamDiscreteValue1.value = strCompName
        ParamDiscreteValue2.value = Session("SS_LOCATION") 
        ParamDiscreteValue3.value = TitleTag
        ParamDiscreteValue4.value = TableCodeTag
        ParamDiscreteValue5.value = UllageTag
        ParamDiscreteValue6.value = VolumeTag
        ParamDiscreteValue7.value = Session("SS_USERNAME")

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

