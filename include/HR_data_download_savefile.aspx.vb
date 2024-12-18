
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information
Imports Microsoft.VisualBasic.Interaction
Imports Microsoft.VisualBasic

Imports agri.PWSystem.clsConfig
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.GlobalHdl.clsAccessRights
Imports agri.HR.clsData

Public Class HR_data_download_savefile : inherits page

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objHRData As New agri.HR.clsData()
    Dim objDownloadDs As New DataSet()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intHRAR As Long

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intHRAR = Session("SS_HRAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRDataTransfer), intHRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            SaveFile()
        End If
    End Sub


    Sub SaveFile()
        Dim objStreamReader As StreamReader

        Dim strEmpCode As String = lcase(Request.QueryString("empcode"))
        Dim strEmpCodeLoc As String = Request.QueryString("empcodeloc")
        Dim strDeptCode As String = lcase(Request.QueryString("deptcode"))
        Dim strNation As String = lcase(Request.QueryString("nation"))
        Dim strFunction As String = lcase(Request.QueryString("function"))
        Dim strLevel As String = lcase(Request.QueryString("level"))
        Dim strReligion As String = lcase(Request.QueryString("religion"))
        Dim strICType As String = lcase(Request.QueryString("ictype"))
        Dim strRace As String = lcase(Request.QueryString("race"))
        Dim strSkill As String = lcase(Request.QueryString("skill"))
        Dim strShift As String = lcase(Request.QueryString("shift"))
        Dim strQualification As String = lcase(Request.QueryString("qualification"))
        Dim strSubject As String = lcase(Request.QueryString("subject"))
        Dim strEvaluation As String = lcase(Request.QueryString("evaluation"))
        Dim strCp As String = lcase(Request.QueryString("cp"))
        Dim strSalScheme As String = lcase(Request.QueryString("salscheme"))
        Dim strBank As String = lcase(Request.QueryString("bank"))
        Dim strEpf As String = lcase(Request.QueryString("epf"))
        Dim strTax As String = lcase(Request.QueryString("tax"))
        Dim strSocso As String = lcase(Request.QueryString("socso"))
        Dim strHoliday As String = lcase(Request.QueryString("holiday"))
        Dim strMPOBPrice As String = lcase(Request.QueryString("mpob"))

        Dim strOpCodeUpd_EmpCode As String = "HR_CLSDATA_EMPCODE_UPD"
        Dim strOpCode_EmpCode As String = "HR_CLSDATA_EMPCODE_DOWNLOAD"
        Dim strOpCode_DeptCode As String = "HR_CLSDATA_DEPTCODE_DOWNLOAD"
        Dim strOpCode_Dept As String = "HR_CLSDATA_DEPT_DOWNLOAD"
        Dim strOpCode_Nation As String = "HR_CLSDATA_NATIONALITY_DOWNLOAD"
        Dim strOpCode_Function As String = "HR_CLSDATA_FUNCTION_DOWNLOAD"
        Dim strOpCode_Position As String = "HR_CLSDATA_POSITION_DOWNLOAD"
        Dim strOpCode_Level As String = "HR_CLSDATA_LEVEL_DOWNLOAD"
        Dim strOpCode_Religion As String = "HR_CLSDATA_RELIGION_DOWNLOAD"
        Dim strOpCode_ICType As String = "HR_CLSDATA_ICTYPE_DOWNLOAD"
        Dim strOpCode_Race As String = "HR_CLSDATA_RACE_DOWNLOAD"
        Dim strOpCode_Skill As String = "HR_CLSDATA_SKILL_DOWNLOAD"
        Dim strOpCode_Shift As String = "HR_CLSDATA_SHIFT_DOWNLOAD"
        Dim strOpCode_ShiftLn As String = "HR_CLSDATA_SHIFTLN_DOWNLOAD"
        Dim strOpCode_Qualification As String = "HR_CLSDATA_QUALIFICATION_DOWNLOAD"
        Dim strOpCode_Subject As String = "HR_CLSDATA_SUBJECT_DOWNLOAD"
        Dim strOpCode_Evaluation As String = "HR_CLSDATA_EVALUATION_DOWNLOAD"
        Dim strOpCode_Cp As String = "HR_CLSDATA_CAREERPROGRESS_DOWNLOAD"
        Dim strOpCode_SalScheme As String = "HR_CLSDATA_SALSCHEME_DOWNLOAD"
        Dim strOpCode_SalGrade As String = "HR_CLSDATA_SALGRADE_DOWNLOAD"
        Dim strOpCode_BankFormat As String = "HR_CLSDATA_BANKFORMAT_DOWNLOAD"
        Dim strOpCode_Bank As String = "HR_CLSDATA_BANK_DOWNLOAD"
        Dim strOpCode_Epf As String = "HR_CLSDATA_EPF_DOWNLOAD"
        Dim strOpCode_Tax As String = "HR_CLSDATA_TAX_DOWNLOAD"
        Dim strOpCode_TaxBranch As String = "HR_CLSDATA_TAXBRANCH_DOWNLOAD"
        Dim strOpCode_Socso As String = "HR_CLSDATA_SOCSO_DOWNLOAD"
        Dim strOpCode_SocsoLn As String = "HR_CLSDATA_SOCSOLN_DOWNLOAD"
        Dim strOpCode_GPH As String = "HR_CLSDATA_GPH_DOWNLOAD"
        Dim strOpCode_HS As String = "HR_CLSDATA_HS_DOWNLOAD"
        Dim strOpCode_HSLn As String = "HR_CLSDATA_HSLN_DOWNLOAD"
        Dim strOpCode_MPOB As String = "HR_CLSDATA_MPOBPRICE_DOWNLOAD"
        Dim strOpCode_MPOBLn As String = "HR_CLSDATA_MPOBPRICELN_DOWNLOAD"

        Dim blnIsEmpCode As Boolean
        Dim strFtpPath As String
        Dim strXmlPath As String = ""
        Dim strXmlFileName As String = objGlobal.mtdGenerateDownloadFile(objGlobal.EnumDataTransferFileType.HR_HumanResourceReferenceData, False)
        Dim strZipFileName As String = Mid(strXmlFileName, 1, Len(strXmlFileName) - 3) & "zip"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim strXmlb4Encrypt As String = ""
        Dim ObjXmlEncrypted As New Object()
        Dim ObjXmlDecrypted As New Object()

        Try
            intErrNo = objSysCfg.mtdGetFtpPath(strFtpPath)
            strXmlPath = strFtpPath & strXmlFileName
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HRDATA_GET_FTPPATH&errmesg=&redirect=")
        End Try

        If strEmpCode = "true" Then 
            strParam = strParam & strOpCode_EmpCode & "|" & _
                       objHRData.mtdGetDownloadTable(objHRData.EnumDownloadType.EmpCode) & Chr(9)
            blnIsEmpCode = True
        Else
            blnIsEmpCode = False
        End If
        If strDeptCode = "true" Then strParam = strParam & strOpCode_DeptCode & "|" & _
                                    objHRData.mtdGetDownloadTable(objHRData.EnumDownloadType.DeptCode) & Chr(9)
        If strDeptCode = "true" Then strParam = strParam & strOpCode_Dept & "|" & _
                                    objHRData.mtdGetDownloadTable(objHRData.EnumDownloadType.Dept) & Chr(9)
        If strNation = "true" Then strParam = strParam & strOpCode_Nation & "|" & _
                                    objHRData.mtdGetDownloadTable(objHRData.EnumDownloadType.Nationality) & Chr(9)
        If strFunction = "true" Then strParam = strParam & strOpCode_Function & "|" & _
                                    objHRData.mtdGetDownloadTable(objHRData.EnumDownloadType.Func) & Chr(9)
        If strFunction = "true" Then strParam = strParam & strOpCode_Position & "|" & _
                                    objHRData.mtdGetDownloadTable(objHRData.EnumDownloadType.Position) & Chr(9)
        If strLevel = "true" Then strParam = strParam & strOpCode_Level & "|" & _
                                    objHRData.mtdGetDownloadTable(objHRData.EnumDownloadType.Level) & Chr(9)
        If strReligion = "true" Then strParam = strParam & strOpCode_Religion & "|" & _
                                    objHRData.mtdGetDownloadTable(objHRData.EnumDownloadType.Religion) & Chr(9)
        If strICType = "true" Then strParam = strParam & strOpCode_ICType & "|" & _
                                    objHRData.mtdGetDownloadTable(objHRData.EnumDownloadType.ICType) & Chr(9)
        If strRace = "true" Then strParam = strParam & strOpCode_Race & "|" & _
                                    objHRData.mtdGetDownloadTable(objHRData.EnumDownloadType.Race) & Chr(9)
        If strSkill = "true" Then strParam = strParam & strOpCode_Skill & "|" & _
                                    objHRData.mtdGetDownloadTable(objHRData.EnumDownloadType.Skill) & Chr(9)
        If strShift = "true" Then strParam = strParam & strOpCode_Shift & "|" & _
                                    objHRData.mtdGetDownloadTable(objHRData.EnumDownloadType.Shift) & Chr(9)
        If strShift = "true" Then strParam = strParam & strOpCode_ShiftLn & "|" & _
                                    objHRData.mtdGetDownloadTable(objHRData.EnumDownloadType.ShiftLn) & Chr(9)
        If strQualification = "true" Then strParam = strParam & strOpCode_Qualification & "|" & _
                                    objHRData.mtdGetDownloadTable(objHRData.EnumDownloadType.Qualification) & Chr(9)
        If strSubject = "true" Then strParam = strParam & strOpCode_Subject & "|" & _
                                    objHRData.mtdGetDownloadTable(objHRData.EnumDownloadType.Subject) & Chr(9)
        If strEvaluation = "true" Then strParam = strParam & strOpCode_Evaluation & "|" & _
                                    objHRData.mtdGetDownloadTable(objHRData.EnumDownloadType.Evaluation) & Chr(9)
        If strCp = "true" Then strParam = strParam & strOpCode_Cp & "|" & _
                                    objHRData.mtdGetDownloadTable(objHRData.EnumDownloadType.CareerProgress) & Chr(9)
        If strSalScheme = "true" Then strParam = strParam & strOpCode_SalScheme & "|" & _
                                    objHRData.mtdGetDownloadTable(objHRData.EnumDownloadType.SalScheme) & Chr(9)
        If strSalScheme = "true" Then strParam = strParam & strOpCode_SalGrade & "|" & _
                                    objHRData.mtdGetDownloadTable(objHRData.EnumDownloadType.SalGrade) & Chr(9)
        If strBank = "true" Then strParam = strParam & strOpCode_BankFormat & "|" & _
                                    objHRData.mtdGetDownloadTable(objHRData.EnumDownloadType.BankFormat) & Chr(9)
        If strBank = "true" Then strParam = strParam & strOpCode_Bank & "|" & _
                                    objHRData.mtdGetDownloadTable(objHRData.EnumDownloadType.Bank) & Chr(9)
        If strEpf = "true" Then strParam = strParam & strOpCode_Epf & "|" & _
                                    objHRData.mtdGetDownloadTable(objHRData.EnumDownloadType.EPF) & Chr(9)
        If strTax = "true" Then strParam = strParam & strOpCode_TaxBranch & "|" & _
                                    objHRData.mtdGetDownloadTable(objHRData.EnumDownloadType.TaxBranch) & Chr(9)
        If strTax = "true" Then strParam = strParam & strOpCode_Tax & "|" & _
                                    objHRData.mtdGetDownloadTable(objHRData.EnumDownloadType.Tax) & Chr(9)
        If strSocso = "true" Then strParam = strParam & strOpCode_Socso & "|" & _
                                    objHRData.mtdGetDownloadTable(objHRData.EnumDownloadType.Socso) & Chr(9)
        If strSocso = "true" Then strParam = strParam & strOpCode_SocsoLn & "|" & _
                                    objHRData.mtdGetDownloadTable(objHRData.EnumDownloadType.SocsoLn) & Chr(9)
        If strHoliday = "true" Then strParam = strParam & strOpCode_GPH & "|" & _
                                    objHRData.mtdGetDownloadTable(objHRData.EnumDownloadType.GPH) & Chr(9)
        If strHoliday = "true" Then strParam = strParam & strOpCode_HS & "|" & _
                                    objHRData.mtdGetDownloadTable(objHRData.EnumDownloadType.HS) & Chr(9)
        If strHoliday = "true" Then strParam = strParam & strOpCode_HSLn & "|" & _
                                    objHRData.mtdGetDownloadTable(objHRData.EnumDownloadType.HSLn) & Chr(9)
        If strMPOBPrice = "true" Then strParam = strParam & strOpCode_MPOB & "|" & _
                                    objHRData.mtdGetDownloadTable(objHRData.EnumDownloadType.MPOBPrice) & Chr(9)
        If strMPOBPrice = "true" Then strParam = strParam & strOpCode_MPOBLn & "|" & _
                                    objHRData.mtdGetDownloadTable(objHRData.EnumDownloadType.MPOBPriceLn) & Chr(9)
    
        If strParam <> "" Then
            strParam = Mid(strParam, 1, Len(strParam) - 1)
            Try
                intErrNo = objHRData.mtdDownloadRef(strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strAccMonth, _
                                                    strAccYear, _
                                                    strParam, _
                                                    strEmpCodeLoc, _
                                                    strOpCodeUpd_EmpCode, _
                                                    objDownloadDs, _
                                                    blnIsEmpCode)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HRDATA_DOWNLOAD_REF&errmesg=&redirect=")
            End Try


            objDownloadDs.WriteXml(strXmlPath, XmlWriteMode.WriteSchema)

            objStreamReader = File.OpenText(strXmlPath)
            strXmlb4Encrypt = objStreamReader.ReadToEnd()
            objStreamReader.Close()

            Try
                intErrNo = objSysCfg.mtdEncryptRef(strXmlb4Encrypt, objXmlEncrypted)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HRDATA_ENCRYPT_REF&errmesg=&redirect=")
            End Try

            Response.Write(objXmlEncrypted)
            Response.ContentType = "application/zip"
            Response.AddHeader("Content-Disposition", _
                                "attachment; filename=""" & strZipFileName & """")
        End If
    End Sub


End Class

