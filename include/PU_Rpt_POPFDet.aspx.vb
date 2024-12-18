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
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.PWSystem.clsLangCap


Public Class PU_Rpt_POPFDet : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents dgResult As DataGrid
    Protected WithEvents lblErrMesage As Label

    Dim objPU As New agri.PU.clsTrx()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objLangCapDs As New Object()
    Dim rdCrystalViewer As New ReportDocument()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String

    Dim strStatus As String
    Dim strPOId As String
    Dim strPrintDate As String
    Dim strSortLine As String
    Dim batchPrint As String
    Dim strReprintedID As String
    Dim arrReprintedID As Array
    Dim intCnt2 As Integer

    Dim strCompName As String
    Dim strLocName As String
    Dim strUserName As String

    Dim strRptId As String
    Dim strRptName As String
    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim intSelDecimal As Integer
    Dim strSelSupress As String
    Dim strUserLoc As String
    Dim strPONoFrom As String
    Dim strPONoTo As String
    Dim strPeriodeFrom As String
    Dim strPeriodeTo As String
    Dim strPIC1 As String
    Dim strJabatan1 As String
    Dim strPIC2 As String
    Dim strJabatan2 As String
    Dim strCatatan As String
    Dim strSearchExp As String = ""
    Dim strLokasi As String
    Dim strSyaratBayar As String
    Dim strCompanyAddress As String
    Dim strSentPeriod As String


    Public Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PUACCMONTH")
        strAccYear = Session("SS_PUACCYEAR")
        strCompName = Session("SS_COMPANYNAME")
        strLocName = Session("SS_LOCATIONNAME")
        strUserName = Session("SS_USERNAME")
        
        crvView.Visible = False  

        strUserLoc = Trim(Request.QueryString("Location"))
        strRptId = Trim(Request.QueryString("RptId"))
        strRptName = Trim(Request.QueryString("RptName"))
        intSelDecimal = CInt(Request.QueryString("Decimal"))

        strPONoFrom = Trim(Request.QueryString("PONoFrom"))
        strPONoTo = Trim(Request.QueryString("PONoTo"))
        strPeriodeFrom = Trim(Request.QueryString("PeriodeFrom"))
        strPeriodeTo = Trim(Request.QueryString("PeriodeTo"))
        strPIC1 = Trim(Request.QueryString("PIC1"))
        strJabatan1 = Trim(Request.QueryString("Jabatan1"))
        strPIC2 = Trim(Request.QueryString("PIC2"))
        strJabatan2 = Trim(Request.QueryString("Jabatan2"))
        strCatatan = Trim(Request.QueryString("Catatan"))
        strSyaratBayar = Trim(Request.QueryString("SyaratBayar"))
        strLokasi = Request.QueryString("Lokasi")
        strSentPeriod = iif(Request.QueryString("SentPeriod") = "", "SEGERA", Request.QueryString("SentPeriod"))

        Bind_ITEM(True)

    End Sub

    Sub Bind_ITEM(ByVal blnIsPDFFormat As Boolean)
        Dim crLogonInfo As CrystalDecisions.Shared.TableLogOnInfo
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim crExportOptions As ExportOptions
        Dim objRptDs As New Dataset()
        Dim objMapPath As New Object()
        Dim strOpCd_Get As String = ""
        Dim strOpCd_GetLine As String = ""
        Dim strOpCd_GetLocInfo As String = "" 
        Dim strOpCodes As String
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strReportName As String
        Dim strPDFName As String
        Dim totalrp As Double
        Dim objRptDs1 As New DataSet
        Dim strBilang As String

        strOpCd_Get = "PU_STDRPT_PURCHASEORDERPF"

        if strPONOFrom <> "" then 
            strParam = " and po.poid = '" & trim(strPONoFrom) & "'"
        end if 
       
        strReportName = "PU_StdRpt_PurchaseOrderPF.rpt"


        Try
            intErrNo = objPU.mtdGetPOPFDocRpt(strOpCd_Get, _
                                            strParam, _
                                            strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strAccMonth, _
                                            strAccYear, _
                                            objRptDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=&errmesg=" & lblErrMesage.Text & "&redirect=")        
        End Try

        If objRptDs.Tables(0).Rows.Count > 0 Then
            If strCatatan = "" Then
                strCatatan = objRptDs.Tables(0).Rows(0).Item("Catatan")
            End If
            For intCnt = 0 To objRptDs.Tables(0).Rows.Count - 1
                'totalrp = Trim(CStr(objRptDs.Tables(0).Rows(intCnt).Item("TotalAmountToReport")))
                'totalrp = Trim(CStr(FormatNumber(objRptDs.Tables(0).Rows(intCnt).Item("TotalAmountToReport"), 2)))
                totalrp = totalrp + Trim(CStr(FormatNumber(objRptDs.Tables(0).Rows(intCnt).Item("TotalAmountCurrency"), 2)))

                If UCase(Trim(objRptDs.Tables(0).Rows(intCnt).Item("CurrencyCode"))) = "IDR" Then
                    objRptDs.Tables(0).Rows(intCnt).Item("Terbilang") = objGlobal.TerbilangDesimal(totalrp, "Rupiah")
                Else
                    objRptDs.Tables(0).Rows(intCnt).Item("Terbilang") = ConvertCurrencyToEnglish(FormatNumber(totalrp, 2))
                    'Replace(objGlobal.ConvertNo2WordsDecimal(FormatNumber(totalrp, 2)), "Zero", "")
                    strBilang = ConvertCurrencyToEnglish(FormatNumber(totalrp, 2))
                    'Replace(objGlobal.ConvertNo2WordsDecimal(FormatNumber(totalrp, 2)), "Zero", "")
                End If
            Next intCnt
        End If

        Try
            intErrNo = objAdmin.mtdGetBasePath(objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=&errmesg=" & lblErrMesage.Text & "&redirect=")
        End Try

        rdCrystalViewer.Load(objMapPath & "Web\EN\PU\Reports\Crystal\" & strReportName, OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        crvView.Visible = False
        crvView.ReportSource = rdCrystalViewer
        crvView.DataBind()

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\PU_Rpt_POPFDet.pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        rdCrystalViewer.Close()
        rdCrystalViewer.Dispose()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../../ftp/PU_Rpt_POPFDet.pdf"">")

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


        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("ParamUserLoc")
        paramField2 = paramFields.Item("ParamRptID")
        paramField3 = paramFields.Item("ParamRptName")
        paramField4 = paramFields.Item("ParamPIC1")
        paramField5 = paramFields.Item("ParamPrintedBy")
        paramField6 = paramFields.Item("ParamSelDecimal")
        paramField7 = paramFields.Item("ParamJabatan1")
        paramField8 = paramFields.Item("ParamCatatan")
        paramField9 = paramFields.Item("ParamPIC2")
        paramField10 = paramFields.Item("ParamJabatan2")
        paramField11 = paramFields.Item("ParamLokasi")
        paramField12 = paramFields.Item("ParamSentPeriod")
        paramField13 = paramFields.Item("ParamUserName")
        paramField14 = paramFields.Item("ParamSyaratBayar")

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

        ParamDiscreteValue1.Value = Request.QueryString("Location")
        ParamDiscreteValue2.Value = Request.QueryString("RptID")
        ParamDiscreteValue3.Value = Request.QueryString("RptName")
        ParamDiscreteValue4.Value = Request.QueryString("PIC1")
        ParamDiscreteValue5.Value = Session("SS_USERNAME")
        ParamDiscreteValue6.Value = Request.QueryString("Decimal")
        ParamDiscreteValue7.Value = Request.QueryString("Jabatan1")
        ParamDiscreteValue8.Value = strCatatan 'Request.QueryString("Catatan")
        ParamDiscreteValue9.Value = Request.QueryString("PIC2")
        ParamDiscreteValue10.Value = Request.QueryString("Jabatan2")
        ParamDiscreteValue11.Value = strLokasi 'Request.QueryString("Lokasi")
        ParamDiscreteValue12.Value = strSentPeriod
        ParamDiscreteValue13.Value = strUserName
        ParamDiscreteValue14.Value = strSyaratBayar


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
    End Sub

    Public Function ConvertCurrencyToEnglish(ByVal MyNumber As Double) As String
        Dim Temp As String
        Dim Dollars, Cents As String
        Dim DecimalPlace, Count As Integer
        Dim Place(9) As String
        Dim Numb As String
        Place(2) = " Thousand " : Place(3) = " Million " : Place(4) = " Billion " : Place(5) = " Trillion "
        ' Convert Numb to a string, trimming extra spaces.
        Numb = Trim(Str(MyNumber))
        ' Find decimal place.
        DecimalPlace = InStr(Numb, ".")
        ' If we find decimal place...
        If DecimalPlace > 0 Then
            ' Convert cents
            Temp = Left(Mid(Numb, DecimalPlace + 1) & "00", 2)
            Cents = ConvertTens(Temp)
            ' Strip off cents from remainder to convert.
            Numb = Trim(Left(Numb, DecimalPlace - 1))
        End If
        Count = 1
        Do While Numb <> ""
            ' Convert last 3 digits of Numb to English dollars.
            Temp = ConvertHundreds(Right(Numb, 3))
            If Temp <> "" Then Dollars = Temp & Place(Count) & Dollars
            If Len(Numb) > 3 Then
                ' Remove last 3 converted digits from Numb.
                Numb = Left(Numb, Len(Numb) - 3)
            Else
                Numb = ""
            End If
            Count = Count + 1
        Loop

        ' Clean up dollars.
        Select Case Dollars
            Case "" : Dollars = "No "
            Case "One" : Dollars = "One"
            Case Else : Dollars = Dollars & ""
        End Select

        ' Clean up cents.
        Select Case Cents
            Case "" : Cents = ""
            Case "One" : Cents = " And One Cent"
            Case Else : Cents = " And Cents " & Cents & ""
        End Select
        ConvertCurrencyToEnglish = Dollars & Cents
    End Function

    Private Function ConvertHundreds(ByVal MyNumber As String) As String
        Dim Result As String
        ' Exit if there is nothing to convert.
        If Val(MyNumber) = 0 Then Exit Function
        ' Append leading zeros to number.
        MyNumber = Right("000" & MyNumber, 3)
        ' Do we have a hundreds place digit to convert?
        If Left(MyNumber, 1) <> "0" Then
            Result = ConvertDigit(Left(MyNumber, 1)) & " Hundred "
        End If
        ' Do we have a tens place digit to convert?
        If Mid(MyNumber, 2, 1) <> "0" Then
            Result = Result & ConvertTens(Mid(MyNumber, 2))
        Else
            ' If not, then convert the ones place digit.
            Result = Result & ConvertDigit(Mid(MyNumber, 3))
        End If
        ConvertHundreds = Trim(Result)
    End Function

    Private Function ConvertTens(ByVal MyTens As String) As String
        Dim Result As String
        ' Is value between 10 and 19?
        If Val(Left(MyTens, 1)) = 1 Then
            Select Case Val(MyTens)
                Case 10 : Result = "Ten"
                Case 11 : Result = "Eleven"
                Case 12 : Result = "Twelve"
                Case 13 : Result = "Thirteen"
                Case 14 : Result = "Fourteen"
                Case 15 : Result = "Fifteen"
                Case 16 : Result = "Sixteen"
                Case 17 : Result = "Seventeen"
                Case 18 : Result = "Eighteen"
                Case 19 : Result = "Nineteen"
                Case Else
            End Select
        Else
            ' .. otherwise it's between 20 and 99.
            Select Case Val(Left(MyTens, 1))
                Case 2 : Result = "Twenty "
                Case 3 : Result = "Thirty "
                Case 4 : Result = "Forty "
                Case 5 : Result = "Fifty "
                Case 6 : Result = "Sixty "
                Case 7 : Result = "Seventy "
                Case 8 : Result = "Eighty "
                Case 9 : Result = "Ninety "
                Case Else
            End Select
            ' Convert ones place digit.
            Result = Result & ConvertDigit(Right(MyTens, 1))
        End If
        ConvertTens = Result
    End Function

    Private Function ConvertDigit(ByVal MyDigit As String) As String
        Select Case Val(MyDigit)
            Case 1 : ConvertDigit = "One"
            Case 2 : ConvertDigit = "Two"
            Case 3 : ConvertDigit = "Three"
            Case 4 : ConvertDigit = "Four"
            Case 5 : ConvertDigit = "Five"
            Case 6 : ConvertDigit = "Six"
            Case 7 : ConvertDigit = "Seven"
            Case 8 : ConvertDigit = "Eight"
            Case 9 : ConvertDigit = "Nine"
            Case Else : ConvertDigit = ""
        End Select
    End Function
End Class

