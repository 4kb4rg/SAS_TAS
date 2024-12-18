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
Imports System.Xml
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

Public Class PM_Rpt_StorageTypeMasterDetail : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objPMSetup As New agri.PM.clsSetup()
    Dim objAdmin As New agri.Admin.clsShare()

    Dim strCompany As String
    Dim strLocation As String
    Dim strCompName As String
    Dim strLocName As String
    Dim strUserId As String
    Dim strLangCode As String

    Dim strStorageTypeCode As String
    Dim strDescription As String
    Dim strStatus As String
    Dim strUpdateBy As String
    
    Dim strSortExp As String
    Dim strSortCol As String

    Dim TitleTag As String
    Dim StorageTypeCodeTag As String
    Dim DescriptionTag As String

    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        
        crvView.Visible = False  

        strStorageTypeCode = Trim(Request.QueryString("strStorageTypeCode"))
        strDescription = Trim(Request.QueryString("strDescription"))
        strStatus = Trim(Request.QueryString("strStatus"))
        strUpdateBy = Trim(Request.QueryString("strUpdateBy"))
        strSortExp = Trim(Request.QueryString("strSortExp"))
        strSortCol = Trim(Request.QueryString("strSortCol"))
        TitleTag = Trim(Request.QueryString("TitleTag"))
        StorageTypeCodeTag = Trim(Request.QueryString("StorageTypeCodeTag"))
        DescriptionTag = Trim(Request.QueryString("DescriptionTag"))
        
        BindReport
    End Sub

    Sub BindReport()
        Dim objRptDs As New DataSet()
        Dim objMapPath As String

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOpCd As String = "PM_RPT_STORAGETYPEMASTERDETAIL_GET"
        Dim strFileName As String = "PM_Rpt_StorageTypeMasterDetail"

        Dim strParam As String
        Dim strSearch As String
        Dim strSortItem As String
        Dim I As Long

        strSearch =  ""
         
        If NOT strStatus = "" Then
            strSearch =  strSearch & " AND ST.Status like '" & strStatus & "' "
        End If
        
        If NOT strStorageTypeCode = "" Then
            strSearch =  strSearch & " AND ST.StorageTypeCode like '" & strStorageTypeCode & "%' "
        End If
        
        If NOT strDescription = "" Then
            strSearch = strSearch & " AND ST.Description like '" & strDescription & "%' "
        End If
        
        If NOT strUpdateBy = "" Then
            strSearch = strSearch & " AND usr.UserName like '" & strUpdateBy & "%' "
        End If
        
        If LCase(Trim(strSortExp)) = LCase("UserName") Then
            strSortExp = "usr." & Trim(strSortExp)
        ElseIf UCase(Left(strSortExp, 3)) <> "ST." Then
            strSortExp = "ST." & Trim(strSortExp)
        End If
        
        strSortItem = "ORDER BY " & strSortExp & " " & strSortCol
        
        strParam =  strSortItem & "|" & strSearch

        Try
            intErrNo = objPMSetup.mtdGetStorageType(strOpCd, _
                                                strParam, _
                                                objRptDs)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PM_RPT_UVTABEL_MASTER_LIST&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        
        Try
            intErrNo = objAdmin.mtdGetBasePath(objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        
        For I = 0 To objRptDs.Tables(0).Rows.Count - 1
            Select Case Trim(objRptDs.Tables(0).Rows(I).Item("FormulaType"))
                Case objPMSetup.EnumFormulaType.UllageVolumeTable, _
                     objPMSetup.EnumFormulaType.UllageAverageCapacityTable, _
                     objPMSetup.EnumFormulaType.CPOPropertiesTable
                    objRptDs.Tables(0).Rows(I).Item("TableCode") = Trim(objRptDs.Tables(0).Rows(I).Item("TableCode"))
                Case Else
                    objRptDs.Tables(0).Rows(I).Item("TableCode") = ""
            End Select
            
            Select Case Trim(objRptDs.Tables(0).Rows(I).Item("OperandType1"))
                Case objPMSetup.EnumOperandType.MatchedUllage, objPMSetup.EnumOperandType.LineResult
                    objRptDs.Tables(0).Rows(I).Item("Flag1") = "TEXT"
                    objRptDs.Tables(0).Rows(I).Item("strOperandValue1") = CInt(objRptDs.Tables(0).Rows(I).Item("OperandValue1"))
                Case objPMSetup.EnumOperandType.Value
                    objRptDs.Tables(0).Rows(I).Item("Flag1") = "VALUE"
                Case Else
                    objRptDs.Tables(0).Rows(I).Item("Flag1") = "TEXT"
                    objRptDs.Tables(0).Rows(I).Item("strOperandValue1") = ""
            End Select
            objRptDs.Tables(0).Rows(I).Item("OperandType1") = objPMSetup.mtdGetOperandType(Trim(objRptDs.Tables(0).Rows(I).Item("OperandType1")))
            
            Select Case Trim(objRptDs.Tables(0).Rows(I).Item("FormulaType"))
                Case objPMSetup.EnumFormulaType.CPOPropertiesTable
                    objRptDs.Tables(0).Rows(I).Item("OperandType2") = ""
                    objRptDs.Tables(0).Rows(I).Item("Flag2") = "TEXT"
                    objRptDs.Tables(0).Rows(I).Item("strOperandValue2") = ""
                Case Else
                    Select Case Trim(objRptDs.Tables(0).Rows(I).Item("OperandType2"))
                        Case objPMSetup.EnumOperandType.MatchedUllage, objPMSetup.EnumOperandType.LineResult
                            objRptDs.Tables(0).Rows(I).Item("Flag2") = "TEXT"
                            objRptDs.Tables(0).Rows(I).Item("strOperandValue2") = CInt(objRptDs.Tables(0).Rows(I).Item("OperandValue2"))
                        Case objPMSetup.EnumOperandType.Value
                            objRptDs.Tables(0).Rows(I).Item("Flag2") = "VALUE"
                        Case objPMSetup.EnumOperandType.MatchType
                            objRptDs.Tables(0).Rows(I).Item("Flag2") = "TEXT"
                            objRptDs.Tables(0).Rows(I).Item("strOperandValue2") = objPMSetup.mtdGetMatchType(objRptDs.Tables(0).Rows(I).Item("OperandValue2"))
                        Case Else
                            objRptDs.Tables(0).Rows(I).Item("Flag2") = "TEXT"
                            objRptDs.Tables(0).Rows(I).Item("strOperandValue2") = ""
                    End Select
                    objRptDs.Tables(0).Rows(I).Item("OperandType2") = objPMSetup.mtdGetOperandType(Trim(objRptDs.Tables(0).Rows(I).Item("OperandType2")))
            End Select
            objRptDs.Tables(0).Rows(I).Item("Status") = objPMSetup.mtdGetStorageTypeFormulaStatus(objRptDs.Tables(0).Rows(I).Item("Status"))
            objRptDs.Tables(0).Rows(I).Item("FormulaType") = objPMSetup.mtdGetFormulaType(Trim(objRptDs.Tables(0).Rows(I).Item("FormulaType")))
        Next
        

        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\PM\Reports\Crystal\" & strFileName & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Portrait
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3

        crvView.Visible = False                         
        crvView.ReportSource = rdCrystalViewer          
        crvView.DataBind()                              

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strFileName & ".pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../../ftp/" & strFileName & ".pdf"">")
    End Sub

    Sub PassParam()
        Dim paramFields As New ParameterFields()
        Dim paramField1 As New ParameterField()
        Dim paramField2 As New ParameterField()
        Dim paramField3 As New ParameterField()
        Dim paramField4 As New ParameterField()
        Dim paramField5 As New ParameterField()
        Dim paramField6 As New ParameterField()
        
        Dim ParamDiscreteValue1 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue2 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue3 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue4 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue5 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue6 As New ParameterDiscreteValue()

        Dim crParameterValues1 As ParameterValues
        Dim crParameterValues2 As ParameterValues
        Dim crParameterValues3 As ParameterValues
        Dim crParameterValues4 As ParameterValues
        Dim crParameterValues5 As ParameterValues
        Dim crParameterValues6 As ParameterValues

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        ParamFields = crvView.ParameterFieldInfo
     
        paramField1 = ParamFields.Item("CompanyTag") 
        paramField2 = ParamFields.Item("LocationTag")
        paramField3 = ParamFields.Item("TitleTag")
        paramField4 = ParamFields.Item("StorageTypeCodeTag")
        paramField5 = ParamFields.Item("DescriptionTag")
        paramField6 = ParamFields.Item("ParamUserName")

        crParameterValues1 = paramField1.CurrentValues
        crParameterValues2 = paramField2.CurrentValues
        crParameterValues3 = paramField3.CurrentValues
        crParameterValues4 = paramField4.CurrentValues
        crParameterValues5 = paramField5.CurrentValues
        crParameterValues6 = paramField6.CurrentValues

        ParamDiscreteValue1.value = Session("SS_COMPANYNAME")
        ParamDiscreteValue2.value = Session("SS_LOCATION") 
        ParamDiscreteValue3.value = TitleTag
        ParamDiscreteValue4.value = StorageTypeCodeTag
        ParamDiscreteValue5.value = DescriptionTag
        ParamDiscreteValue6.value = Session("SS_USERNAME")

        crParameterValues1.Add(ParamDiscreteValue1)
        crParameterValues2.Add(ParamDiscreteValue2)
        crParameterValues3.Add(ParamDiscreteValue3)
        crParameterValues4.Add(ParamDiscreteValue4)
        crParameterValues5.Add(ParamDiscreteValue5)
        crParameterValues6.Add(ParamDiscreteValue6)

        PFDefs(0).ApplyCurrentValues(crParameterValues1)
        PFDefs(1).ApplyCurrentValues(crParameterValues2)
        PFDefs(2).ApplyCurrentValues(crParameterValues3)
        PFDefs(3).ApplyCurrentValues(crParameterValues4)
        PFDefs(4).ApplyCurrentValues(crParameterValues5)
        PFDefs(5).ApplyCurrentValues(crParameterValues6)

        crvView.ParameterFieldInfo = paramFields
    End Sub


End Class
