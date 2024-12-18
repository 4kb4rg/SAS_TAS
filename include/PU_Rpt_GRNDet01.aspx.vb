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


Public Class PU_Rpt_GRNDet : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents dgResult As DataGrid
    Protected WithEvents lblErrMesage As Label

    Dim objPU As New agri.PU.clsTrx()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objLangCapDs As New Object()
    Dim rdCrystalViewer As New ReportDocument()
    Dim objGLtrx As New agri.GL.ClsTrx()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String

    Dim strStatus As String
    Dim strGoodsRetId As String
    Dim strPrintDate As String
    Dim strSortLine As String
    Dim batchPrint As String
    Dim strReprintedID As String
    Dim arrReprintedID As Array
    Dim intCnt2 As Integer

    Dim strCompName As String
    Dim strLocName As String
    Dim strUserName As String

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

        strStatus = Trim(Request.QueryString("strStatus"))
        strGoodsRetId = Trim(Request.QueryString("strGoodsRetId"))
        strPrintDate = Trim(Request.QueryString("strPrintDate"))
        strSortLine = Trim(Request.QueryString("strSortLine"))
        batchPrint = Trim(Request.QueryString("batchPrint"))
        strReprintedID = Trim(Request.QueryString("reprintId"))

        Bind_ITEM(True)

    End Sub

    Sub Bind_ITEM(ByVal blnIsPDFFormat As Boolean)
        Dim crLogonInfo As CrystalDecisions.Shared.TableLogOnInfo
        Dim objRptDs As New Dataset()
        Dim objMapPath As New Object()
        Dim strOpCd_Get As String = ""
        Dim strOpCd_GetLine As String = ""
        Dim strOpCd_GetLocInfo As String = ""
        Dim strOpCd_GetLinePO As String = ""
        Dim strOpCodes As String
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strReportName As String
        Dim strPDFName As String
        Dim totalrp As Double
        Dim totalppn As Double

        strOpCd_Get = "PU_CLSTRX_GOODSRETURN_DETAIL_GET_DOCRPT" & "|" & "GoodsReturn"
        strOpCd_GetLine = "PU_CLSTRX_GOODSRETURN_DETAIL_LINE_GET_DOCRPT" & "|" & "GoodsReturnLine"
        strOpCd_GetLocInfo = "PU_CLSTRX_PO_GET_LOCATIONINFO_FOR_DOCRPT" & "|" & "LocInfo" 

        strReportName = "PU_Rpt_GoodsReturn.rpt"

        strOpCodes = strOpCd_Get & Chr(9) & strOpCd_GetLine & Chr(9) & strOpCd_GetLocInfo 
        strParam = strGoodsRetId & "||"

        Try
            intErrNo = objPU.mtdGetGRNDocRpt(strOpCodes, _
                                             strParam, _
                                             strCompany, _
                                             strLocation, _
                                             strUserId, _
                                             strAccMonth, _
                                             strAccYear, _
                                             objRptDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=&errmesg=" & lblErrMesage.Text & "&redirect=")
        End Try

        If objRptDs.Tables("GoodsReturn").Rows.Count > 0 Then
            For intCnt=0 To objRptDs.Tables("GoodsReturn").Rows.Count - 1
                objRptDs.Tables("GoodsReturn").Rows(intCnt).Item("GoodsRetType") = objPU.mtdGetGRNType(CInt(objRptDs.Tables("GoodsReturn").Rows(intCnt).Item("GoodsRetType")))
            Next
        End If


        If objRptDs.Tables(0).Rows.Count > 0 Then
            For intCnt=0 To objRptDs.Tables(0).Rows.Count - 1
                If batchPrint = "yes" Then
                    objRptDs.Tables(0).Rows(intCnt).Item("PrintStatus") = objPU.mtdGetGRNStatus(CInt(objRptDs.Tables(0).Rows(intCnt).Item("Status")))
                    
                Else
                    objRptDs.Tables(0).Rows(intCnt).Item("PrintStatus") = strStatus
                End If
            Next
        End If

        If objRptDs.Tables(0).Rows.Count > 0 Then
            If batchPrint = "yes" Then
                If InStr(strReprintedID, "|") <> 0 Then
                    arrReprintedID = Split(strReprintedID, "|")
                    For intCnt=0 To objRptDs.Tables(0).Rows.Count - 1
                        For intCnt2=0 To UBound(arrReprintedID)
                            If Trim(objRptDs.Tables(0).Rows(intCnt).Item("GoodsRetId")) = Trim(arrReprintedID(intCnt2)) Then
                                objRptDs.Tables(0).Rows(intCnt).Item("PrintStatus") = objPU.mtdGetGRNStatus(CInt(objRptDs.Tables(0).Rows(intCnt).Item("Status"))) & " (re-printed)"                                
                            End If
                        Next
                    Next
                End If
            End If
        End If

        

        If objRptDs.Tables("GoodsReturnLine").Rows.Count > 0 Then
            Dim objGetPO As New Object()
            Dim strParamName As String = "LOCCODE|POID"
            Dim strParamValue As String = strLocation & "|" & objRptDs.Tables("GoodsReturnLine").Rows(0).Item("POID")
            strOpCd_GetLinePO = "PU_CLSTRX_GOODSRETURN_DETAIL_GET_PO_DOCRPT"
            Try
                intErrNo = objGLtrx.mtdGetDataCommon(strOpCd_GetLinePO, _
                                                    strParamName, _
                                                    strParamValue, _
                                                    objGetPO)

            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=&errmesg=" & lblErrMesage.Text & "&redirect=")
            End Try

            For intCnt = 0 To objRptDs.Tables("GoodsReturnLine").Rows.Count - 1
                If objRptDs.Tables("GoodsReturnLine").Rows(intCnt).Item("PPN") = "1" Then
                    If objGetPO.Tables(0).Rows(0).Item("PPNInd") = 2 Then
                        totalppn = FormatNumber((objRptDs.Tables("GoodsReturnLine").Rows(intCnt).Item("QtyReturn") * objRptDs.Tables("GoodsReturnLine").Rows(intCnt).Item("Cost")) * 0.1, 2)
                    Else
                        totalppn = FormatNumber((objRptDs.Tables("GoodsReturnLine").Rows(intCnt).Item("QtyReturn") * objRptDs.Tables("GoodsReturnLine").Rows(intCnt).Item("Cost")) * 0.1, 0)
                    End If
                    'totalppn = FormatNumber((objRptDs.Tables("GoodsReturnLine").Rows(intCnt).Item("QtyReturn") * objRptDs.Tables("GoodsReturnLine").Rows(intCnt).Item("Cost")) * 0.1, 2)
                Else
                    totalppn = 0
                End If
                totalrp = totalrp + (CDbl(objRptDs.Tables("GoodsReturnLine").Rows(intCnt).Item("QtyReturn")) * CDbl(objRptDs.Tables("GoodsReturnLine").Rows(intCnt).Item("Cost")) + totalppn) / objRptDs.Tables("GoodsReturnLine").Rows(intCnt).Item("ExchangeRate")
                totalrp = Trim(CStr(FormatNumber(totalrp, 2)))

                If UCase(Trim(objRptDs.Tables("GoodsReturnLine").Rows(intCnt).Item("CurrencyCode"))) = "IDR" Then
                    objRptDs.Tables("GoodsReturnLine").Rows(intCnt).Item("Terbilang") = objGlobal.TerbilangDesimal(totalrp, "Rupiah")
                Else
                    objRptDs.Tables("GoodsReturnLine").Rows(intCnt).Item("Terbilang") = ConvertCurrencyToEnglish(FormatNumber(totalrp, 2))
                    'objRptDs.Tables("GoodsReturnLine").Rows(intCnt).Item("Terbilang") = Replace(objGlobal.ConvertNo2WordsDecimal(FormatNumber(totalrp, 2)), "Zero", "")
                End If
            Next
        End If

        Try
            intErrNo = objAdmin.mtdGetBasePath(objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=&errmesg=" & lblErrMesage.Text & "&redirect=")
        End Try


        rdCrystalViewer.Load(objMapPath & "Web\EN\PU\Reports\Crystal\" & strReportName, OpenReportMethod.OpenReportByTempCopy)
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
            DiskOpts.DiskFileName = objMapPath & "web\ftp\PU_Rpt_GRNDet.pdf"
            rdCrystalViewer.ExportOptions.DestinationOptions = DiskOpts
            rdCrystalViewer.Export()
            rdCrystalViewer.Close()
            rdCrystalViewer.Dispose()
            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../../ftp/PU_Rpt_GRNDet.pdf"">")
        End If

    End Sub

    Sub PassParam()
        Dim paramFields As New ParameterFields()
        Dim paramField1 As New ParameterField()
        Dim paramField2 As New ParameterField()
        Dim paramField3 As New ParameterField()
        Dim paramField4 As New ParameterField()

        Dim ParamDiscreteValue1 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue2 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue3 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue4 As New ParameterDiscreteValue()

        Dim crParameterValues1 As ParameterValues
        Dim crParameterValues2 As ParameterValues
        Dim crParameterValues3 As ParameterValues
        Dim crParameterValues4 As ParameterValues

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        ParamFields = crvView.ParameterFieldInfo

        ParamField1 = ParamFields.Item("strPrintedBy")
        paramField2 = paramFields.Item("strStatus")
        paramField3 = paramFields.Item("strLocName")
        paramField4 = paramFields.Item("strCompName")

        crParameterValues1 = ParamField1.CurrentValues
        crParameterValues2 = ParamField2.CurrentValues
        crParameterValues3 = ParamField3.CurrentValues
        crParameterValues4 = ParamField4.CurrentValues

        ParamDiscreteValue1.value = strUserName
        ParamDiscreteValue2.Value = strStatus
        ParamDiscreteValue3.value = strLocName
        ParamDiscreteValue4.Value = strCompName

        crParameterValues1.Add(ParamDiscreteValue1)
        crParameterValues2.Add(ParamDiscreteValue2)
        crParameterValues3.Add(ParamDiscreteValue3)
        crParameterValues4.Add(ParamDiscreteValue4)

        PFDefs(0).ApplyCurrentValues(crParameterValues1)
        PFDefs(1).ApplyCurrentValues(crParameterValues2)
        PFDefs(2).ApplyCurrentValues(crParameterValues3)
        PFDefs(3).ApplyCurrentValues(crParameterValues4)

        crvView.ParameterFieldInfo = paramFields
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

