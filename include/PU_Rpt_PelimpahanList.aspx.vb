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

Imports agri.PU.clsTrx
Imports agri.Admin.clsShare
Imports agri.Admin.clsComp
Imports agri.Admin.clsLoc
Imports agri.GlobalHdl.clsGlobalHdl


Public Class PU_Rpt_PelimpahanList : inherits page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCriteria As HtmlTable
    Protected WithEvents tblCrystal As HtmlTable
    Protected WithEvents lblErrMesage As Label
    Protected WithEvents dgResult As DataGrid

    Dim objPU As New agri.PU.clsTrx()
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
    Dim strPelimpahanID  As String
    Dim strPRID  As String
    Dim strPelimpahanType As String
    Dim strUpdateBy As String
    Dim strSortExp As String
    Dim strSortCol As String

    Dim DocTitleTag As String

    Public Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        
        crvView.Visible = False  

        strStatus = Trim(Request.QueryString("strStatus"))
        strPelimpahanID  = Trim(Request.QueryString("strPelimpahanID "))
        strPRID  = Trim(Request.QueryString("strPRID "))
        strPelimpahanType = Trim(Request.QueryString("strPelimpahanType"))
        strUpdateBy = Trim(Request.QueryString("strUpdateBy"))
        strSortExp = Trim(Request.QueryString("strSortExp"))
        strSortCol = Trim(Request.QueryString("strSortCol"))
        DocTitleTag = Trim(Request.QueryString("DocTitleTag"))
        
        Bind_ITEM(TRUE)

    End Sub

    Sub Bind_ITEM(ByVal blnIsPDFFormat As Boolean)
        Dim crLogonInfo As CrystalDecisions.Shared.TableLogOnInfo
        Dim objRptDs As New Dataset()
        Dim objMapPath As New Object()
        Dim objCompDs As New Dataset()
        Dim objLocDs As New Dataset()
        Dim strOpCd As String = "PU_CLSTRX_PELIMPAHAN_GET"
        Dim strOpCd_Comp As String = "ADMIN_CLSCOMP_COMPANY_DETAILS_GET"
        Dim strOpCod_Loc As String = "ADMIN_CLSLOC_LOCATION_DETAILS_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim strSearch As String
        Dim strSortItem As String
        Dim intCnt As Integer
        Dim strCompName As String
        Dim dr As DataRow

        strSearch =  " AND A.Status like '" & IIF(Not strStatus = "", strStatus, "%" ) &"' "

        If NOT strPelimpahanID  = "" Then
            strSearch =  strSearch & " AND A.PelimpahanID like '" & strPelimpahanID  & "%' "
        End If

        If NOT strPRID  = "" Then
            strSearch =  strSearch & " AND A.PRID like '" & strPRID & "%' "
        End If
        
        If NOT strPelimpahanType = "" Then
            strSearch = strSearch & " AND A.PelimpahanType like '" & _
                        strPelimpahanType &"%' "
        End If
        
        If NOT strUpdateBy = "" Then
            strSearch = strSearch & " AND usr.Username like '" & _
                        strUpdateBy &"%' "
        End If

        strSortItem = "ORDER BY " & strSortExp & " " & strSortCol
        
        strParam = strPelimpahanID & "|" & _
                   strLocation & "|" & _
                   strPRID & "|" & _ 
                   strPelimpahanID & "|" & _
                   strStatus & "||" & _
                   strSortExp & "|" & _
                   strSortCol

        Try
            intErrNo = objPU.mtdGetPelimpahan(strOpCd, _
                                                     strParam, _
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

        For intCnt=0 To objRptDs.Tables(0).Rows.Count - 1
            objRptDs.Tables(0).Rows(intCnt).Item(0) = Trim(objRptDs.Tables(0).Rows(intCnt).Item(0))
            objRptDs.Tables(0).Rows(intCnt).Item(1) = Trim(objRptDs.Tables(0).Rows(intCnt).Item(1))
            objRptDs.Tables(0).Rows(intCnt).Item(2) = Trim(objRptDs.Tables(0).Rows(intCnt).Item(2))
            objRptDs.Tables(0).Rows(intCnt).Item(3) = Trim(objRptDs.Tables(0).Rows(intCnt).Item(3))
            objRptDs.Tables(0).Rows(intCnt).Item(4) = Trim(objRptDs.Tables(0).Rows(intCnt).Item(4))
            objRptDs.Tables(0).Rows(intCnt).Item(5) = Trim(objRptDs.Tables(0).Rows(intCnt).Item(5))
        Next
        
        For intCnt=0 To objRptDs.Tables(0).Rows.Count - 1
            objRptDs.Tables(0).Rows(intCnt).Item("Status") = objPU.mtdGetPelimpahanStatus(objRptDs.Tables(0).Rows(intCnt).Item("Status"))
            objRptDs.Tables(0).Rows(intCnt).Item("UpdateDate") = objGlobal.GetLongDate(objRptDs.Tables(0).Rows(intCnt).Item("UpdateDate"))
        Next

        If objRptDs.Tables(0).Rows.Count > 0 Then
        Else
            dr = objRptDs.Tables(0).NewRow()
            dr("CompName") = strCompName
            objRptDs.Tables(0).Rows.InsertAt(dr, 0)
        End If

        rdCrystalViewer.Load(objMapPath & "Web\EN\PU\Reports\Crystal\PU_Rpt_PelimpahanList.rpt", OpenReportMethod.OpenReportByTempCopy)
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
            DiskOpts.DiskFileName = objMapPath & "web\ftp\PU_Rpt_PelimpahanList.pdf"
            rdCrystalViewer.ExportOptions.DestinationOptions = DiskOpts
            rdCrystalViewer.Export()

            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../../ftp/PU_Rpt_PelimpahanList.pdf"">")
        End If

    End Sub


    Sub PassParam()
        Dim paramFields As New ParameterFields()
        Dim paramField1 As New ParameterField()
        
        Dim ParamDiscreteValue1 As New ParameterDiscreteValue()
        
        Dim crParameterValues1 As ParameterValues
        
        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        ParamFields = crvView.ParameterFieldInfo
     
        paramField1 = ParamFields.Item("DocTitleTag") 

        crParameterValues1 = paramField1.CurrentValues
        
        ParamDiscreteValue1.value = DocTitleTag
        
        crParameterValues1.Add(ParamDiscreteValue1)
        
        PFDefs(0).ApplyCurrentValues(crParameterValues1)
        
        crvView.ParameterFieldInfo = paramFields

    End Sub


End Class

