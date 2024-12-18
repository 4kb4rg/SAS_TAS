
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

Imports agri.HR.clsSetup
Imports agri.Admin.clsShare
Imports agri.Admin.clsComp
Imports agri.Admin.clsLoc
Imports agri.GlobalHdl.clsGlobalHdl


Public Class HR_Rpt_DeptCode : inherits page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCriteria As HtmlTable
    Protected WithEvents tblCrystal As HtmlTable
    Protected WithEvents txtInvoiceRcvID As TextBox
    Protected WithEvents lblErrMesage As Label
    Protected WithEvents dgResult As DataGrid

    Dim objHR As New agri.HR.clsSetup()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objComp As New agri.Admin.clsComp()
    Dim objLoc As New agri.Admin.clsLoc()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim rdCrystalViewer As New ReportDocument()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String

    Dim strStatus As String
    Dim strDeptCode As String
    Dim strDescription As String
    Dim strUpdateBy As String
    Dim strSortExp As String
    Dim strSortCol As String

    Dim DocTitleTag As String
    Dim DeptCodeTag As String
    Dim DeptDescTag As String

    Public Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        
        crvView.Visible = False  

        strStatus = Trim(Request.QueryString("strStatus"))
        strDeptCode = Trim(Request.QueryString("strDeptCode"))
        strDescription = Trim(Request.QueryString("strDescription"))
        strUpdateBy = Trim(Request.QueryString("strUpdateBy"))
        strSortExp = Trim(Request.QueryString("strSortExp"))
        strSortCol = Trim(Request.QueryString("strSortCol"))
        DocTitleTag = Trim(Request.QueryString("DocTitleTag"))
        DeptCodeTag = Trim(Request.QueryString("DeptCodeTag"))
        DeptDescTag = Trim(Request.QueryString("DeptDescTag"))

        Bind_ITEM(TRUE)

    End Sub

    Sub Bind_ITEM(ByVal blnIsPDFFormat As Boolean)
        Dim crLogonInfo As CrystalDecisions.Shared.TableLogOnInfo
        Dim objRptDs As New Dataset()
        Dim objMapPath As New Object()
        Dim objCompDs As New Dataset()
        Dim objLocDs As New Dataset()
        Dim strOpCd As String = "HR_CLSSETUP_DEPTCODE_GET_FOR_REPORT"
        Dim strOpCd_Comp As String = "ADMIN_CLSCOMP_COMPANY_DETAILS_GET"
        Dim strOpCod_Loc As String = "ADMIN_CLSLOC_LOCATION_DETAILS_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim strSearch As String
        Dim strSortItem As String
        Dim intCnt As Integer
        Dim strCompName As String
        Dim strLocName As String
        Dim dr As DataRow

        strSearch =  " AND DCode.Status like '" & IIF(Not strStatus = "", strStatus, "%" ) &"' "

        If NOT strDeptCode = "" Then
            strSearch =  strSearch & " AND DCode.ProdTypeCode like '" & strDeptCode & "%' "
        End If
        
        If NOT strDescription = "" Then
            strSearch = strSearch & " AND DCode.Description like '" & _
                        strDescription &"%' "
        End If
        
        If NOT strUpdateBy = "" Then
            strSearch = strSearch & " AND usr.Username like '" & _
                        strUpdateBy &"%' "
        End If

        strSortItem = "ORDER BY " & strSortExp & " " & strSortCol
        
        strParam =  strSortItem & "|" & strSearch

        Try
            intErrNo = objHR.mtdGetMasterList(strOpCd, _
                                              strParam, _
                                              EnumHRMasterType.DeptCode, _
                                              objRptDs)
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
            objRptDs.Tables(0).Rows(intCnt).Item(1) = Trim(objRptDs.Tables(0).Rows(intCnt).Item(1))
            objRptDs.Tables(0).Rows(intCnt).Item(2) = Trim(objRptDs.Tables(0).Rows(intCnt).Item(2))
            objRptDs.Tables(0).Rows(intCnt).Item(3) = Trim(objRptDs.Tables(0).Rows(intCnt).Item(3))
            objRptDs.Tables(0).Rows(intCnt).Item(4) = Trim(objRptDs.Tables(0).Rows(intCnt).Item(4))
            objRptDs.Tables(0).Rows(intCnt).Item(5) = Trim(objRptDs.Tables(0).Rows(intCnt).Item(5))

            objRptDs.Tables(0).Rows(intCnt).Item("Status") = objHR.mtdGetDeptCodeStatus(objRptDs.Tables(0).Rows(intCnt).Item("Status"))
            objRptDs.Tables(0).Rows(intCnt).Item("UpdateDate") = objGlobal.GetLongDate(objRptDs.Tables(0).Rows(intCnt).Item("UpdateDate"))
        Next
        

        If objRptDs.Tables(0).Rows.Count > 0 Then
            objRptDs.Tables(0).Rows(0).Item("CompName") = strCompName
            objRptDs.Tables(0).Rows(0).Item("LocName") = strLocName
        Else
            dr = objRptDs.Tables(0).NewRow()
            dr("CompName") = strCompName
            dr("LocName") = strLocName
            objRptDs.Tables(0).Rows.InsertAt(dr, 0)
        End If

        rdCrystalViewer.Load(objMapPath & "Web\EN\HR\Reports\Crystal\HR_Rpt_DeptCode.rpt", OpenReportMethod.OpenReportByTempCopy)
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
            DiskOpts.DiskFileName = objMapPath & "web\ftp\HR_Rpt_DeptCode.pdf"
            rdCrystalViewer.ExportOptions.DestinationOptions = DiskOpts
            rdCrystalViewer.Export()

            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../../ftp/HR_Rpt_DeptCode.pdf"">")
        End If

    End Sub


    Sub PassParam()
        Dim paramFields As New ParameterFields()
        Dim paramField1 As New ParameterField()
        Dim paramField2 As New ParameterField()
        Dim paramField3 As New ParameterField()

        Dim ParamDiscreteValue1 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue2 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue3 As New ParameterDiscreteValue()

        Dim crParameterValues1 As ParameterValues
        Dim crParameterValues2 As ParameterValues
        Dim crParameterValues3 As ParameterValues

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        ParamFields = crvView.ParameterFieldInfo
     
        paramField1 = ParamFields.Item("DocTitleTag") 
        paramField2 = ParamFields.Item("DeptCodeTag")
        paramField3 = ParamFields.Item("DeptDescTag")

        crParameterValues1 = paramField1.CurrentValues
        crParameterValues2 = paramField2.CurrentValues
        crParameterValues3 = paramField3.CurrentValues

        ParamDiscreteValue1.value = DocTitleTag
        ParamDiscreteValue2.value = DeptCodeTag
        ParamDiscreteValue3.value = DeptDescTag

        crParameterValues1.Add(ParamDiscreteValue1)
        crParameterValues2.Add(ParamDiscreteValue2)
        crParameterValues3.Add(ParamDiscreteValue3)

        PFDefs(0).ApplyCurrentValues(crParameterValues1)
        PFDefs(1).ApplyCurrentValues(crParameterValues2)
        PFDefs(2).ApplyCurrentValues(crParameterValues3)

        crvView.ParameterFieldInfo = paramFields

    End Sub



End Class

