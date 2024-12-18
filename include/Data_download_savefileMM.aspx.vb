Imports System
Imports System.Data
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction
Imports Microsoft.VisualBasic.FileSystem
Imports Microsoft.VisualBasic
Imports System.Data.OleDb
Imports agri.PWSystem.clsConfig
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.GlobalHdl.clsAccessRights
Imports agri.WM.clsData

Public Class Data_download_savefile : Inherits page

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objPU As New agri.PU.clsReport()
    Dim objPUTrx As New agri.PU.clsTrx()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objWMData As New agri.WM.clsData()
    Dim objGLTrx As New agri.GL.clsTrx()
    Dim dsResult As New DataSet()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intCBAR As Integer
    Dim strUserLoc As String
    Dim strLocLevel As String
    Dim tempLoc As String
    Dim intErrNo As Integer
    Dim strDDLAccMth As String
    Dim strDDLAccYr As String



    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strDDLAccMth = Request.QueryString("DDLAccMth")
        strDDLAccYr = Request.QueryString("DDLAccYr")

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PDACCMONTH")
        strAccYear = Session("SS_PDACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intCBAR = Session("SS_CBAR")
        Dim strUserLoc As String

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


        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
            'ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUDownload), intCBAR) = False Then
            '    Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            SaveFile()
        End If
    End Sub

    Private Function GetData_ItemHistory() As DataSet
        Dim objRptDs As New DataSet()
        Dim objMapPath As String
        Dim objItem As New DataSet()

        Dim strOpCdPO_GET As String = "DOWNLOAD_PU_ITEM_GET"
        Dim strOpCdItem_GET As String = "PU_STDRPT_ITEM_GET"

        Dim strParam As String
        Dim strParamItm As String
        Dim strItemCode As String
        Dim strItemDesc As String
        Dim SearchStr As String
        Dim itemSelectStr As String
        Dim itemSearchStr As String
        Dim strUserLoc1 As String
        Dim MyPos As Integer
        Dim intCnt As Integer
        Dim intCntRem As Integer

        Dim WildStr As String = " FROM PU_PO PO left outer join  PU_POLN POLN on PO.POID = POLN.POID left join in_pr pr on poln.prid = pr.prid "
        Dim NormStr As String = " FROM PU_PO PO inner join  PU_POLN POLN on PO.POID = POLN.POID left join in_pr pr on poln.prid = pr.prid "

        If Not (Request.QueryString("DocDateFrom") = "" And Request.QueryString("DocDateTo") = "") Then
            SearchStr = SearchStr & "(DateDiff(Day, '" & Request.QueryString("DocDateFrom") & "', PO.CreateDate) >= 0) And (DateDiff(Day, '" & Request.QueryString("DocDateTo") & "', PO.CreateDate) <= 0) AND "
        End If

        If Not (Request.QueryString("DocNoFrom") = "" And Request.QueryString("DocNoTo") = "") Then
            SearchStr = SearchStr & "PO.POID IN (SELECT SUBPO.POID FROM PU_PO SUBPO WHERE SUBPO.POID >= '" & Request.QueryString("DocNoFrom") & _
                        "' AND SUBPO.POID <= '" & Request.QueryString("DocNoTo") & "') AND "
        End If

        If Not Request.QueryString("Supplier") = "" Then
            SearchStr = SearchStr & "(PO.SupplierCode LIKE '%" & Request.QueryString("Supplier") & "%' OR SPL.Name LIKE '%" & Request.QueryString("Supplier") & "%') AND "
        Else
            SearchStr = SearchStr & "PO.SupplierCode LIKE '%' AND "
        End If

        If Not Request.QueryString("AddNote") = "" Then
            SearchStr = SearchStr & "(POLN.AdditionalNote LIKE '%" & Request.QueryString("AddNote") & "%' OR POLN.Catatan LIKE '%" & Request.QueryString("AddNote") & "%') AND "
        End If

        If Not Request.QueryString("ProdType") = "" Then
            itemSelectStr = "ITM.ProdTypeCode LIKE '" & Request.QueryString("ProdType") & "' AND "
        Else
            itemSelectStr = "ITM.ProdTypeCode LIKE '%' AND "
        End If

        If Not Request.QueryString("ProdBrand") = "" Then
            itemSelectStr = itemSelectStr & "ITM.ProdBrandCode LIKE '" & Request.QueryString("ProdBrand") & "' AND "
        Else
            itemSelectStr = itemSelectStr & "ITM.ProdBrandCode LIKE '%' AND "
        End If

        If Not Request.QueryString("ProdModel") = "" Then
            itemSelectStr = itemSelectStr & "ITM.ProdModelCode LIKE '" & Request.QueryString("ProdModel") & "' AND "
        Else
            itemSelectStr = itemSelectStr & "ITM.ProdModelCode LIKE '%' AND "
        End If

        If Not Request.QueryString("ProdCat") = "" Then
            itemSelectStr = itemSelectStr & "ITM.ProdCatCode LIKE '" & Request.QueryString("ProdCat") & "' AND "
        Else
            itemSelectStr = itemSelectStr & "ITM.ProdCatCode LIKE '%' AND "
        End If

        If Not Request.QueryString("ProdMat") = "" Then
            itemSelectStr = itemSelectStr & "ITM.ProdMatCode LIKE '" & Request.QueryString("ProdMat") & "' AND "
        Else
            itemSelectStr = itemSelectStr & "ITM.ProdMatCode LIKE '%' AND "
        End If

        If Not Request.QueryString("StkAna") = "" Then
            itemSelectStr = itemSelectStr & "ITM.StockAnalysisCode LIKE '" & Request.QueryString("StkAna") & "' AND "
        Else
            itemSelectStr = itemSelectStr & "ITM.StockAnalysisCode LIKE '%' AND "
        End If

        If Not Request.QueryString("ItemCode") = "" Then
            itemSelectStr = itemSelectStr & "ITM.ItemCode LIKE '" & Request.QueryString("ItemCode") & "' AND "
        Else
            itemSelectStr = itemSelectStr & "ITM.ItemCode LIKE '%' AND "
        End If

        If Not Request.QueryString("ItemDesc") = "" Then
            itemSelectStr = itemSelectStr & "ITM.Description LIKE '%" & Request.QueryString("ItemDesc") & "%' AND "
        Else
            itemSelectStr = itemSelectStr & "ITM.Description LIKE '%' AND "
        End If

        If Request.QueryString("ProdType") = "" And Request.QueryString("ProdBrand") = "" And Request.QueryString("ProdModel") = "" And Request.QueryString("ProdCat") = "" And Request.QueryString("ProdMat") = "" And Request.QueryString("StkAna") = "" And Request.QueryString("ItemCode") = "" And Request.QueryString("ItemDesc") = "" Then
            itemSearchStr = "OR ( POLN.ItemCode like '%' )"
        End If

        If Not Request.QueryString("POType") = "" Then
            If Not Request.QueryString("POType") = objPUTrx.EnumPOType.All Then
                SearchStr = SearchStr & "PO.POType = '" & Request.QueryString("POType") & "' AND "
            Else
                SearchStr = SearchStr & "PO.POType LIKE '%' AND "
            End If
        End If

        If Not Request.QueryString("Status") = "" Then
            If Not Request.QueryString("Status") = objPUTrx.EnumPOStatus.All Then
                SearchStr = SearchStr & "PO.Status = '" & Request.QueryString("Status") & "' AND "
            Else
                SearchStr = SearchStr & "PO.Status LIKE '%' AND "
            End If
        End If


        MyPos = InStr(strUserLoc, strLocation)
        If MyPos > 0 Then
            If strLocLevel = "1" Then
                strUserLoc1 = " WHERE PO.LocCode IN ('" & strUserLoc & "') "
            End If
            If strLocLevel = "2" Then
                strUserLoc1 = " where PR.LocLevel in ('1', '2') "
            End If
            If strLocLevel = "3" Then
                strUserLoc1 = " where PR.LocLevel in ('1', '2', '3') "
            End If
        Else
            strUserLoc1 = " WHERE PO.LocCode IN ('" & strUserLoc & "') "
        End If

        'all lokasi
        strUserLoc1 = " WHERE PO.LocCode ='MDN' "

        If Not SearchStr = "" Or Not itemSelectStr = "" Then
            If Right(SearchStr, 4) = "AND " Then
                SearchStr = Left(SearchStr, Len(SearchStr) - 4)
            End If

            If Right(itemSelectStr, 4) = "AND " Then
                itemSelectStr = Left(itemSelectStr, Len(itemSelectStr) - 4)
            End If

            If Not Request.QueryString("ItemCode") = "" Then
                strParam = strUserLoc1 & "|" & strDDLAccMth & "|" & strDDLAccYr & "|" & itemSelectStr & "|" & itemSearchStr & "|" & SearchStr & "|" & NormStr
            Else
                strParam = strUserLoc1 & "|" & strDDLAccMth & "|" & strDDLAccYr & "|" & itemSelectStr & "|" & itemSearchStr & "|" & SearchStr & "|" & WildStr
            End If

        End If

        Try
            intErrNo = objPU.mtdGetReport_POList(strOpCdPO_GET, strParam, objRptDs, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_STDRPT_PO_LIST&errmesg=" & Exp.Message & "&redirect=")
        End Try

        For intCnt = 0 To objRptDs.Tables(0).Rows.Count - 1
            If Not IsDBNull(objRptDs.Tables(0).Rows(intCnt).Item("KODE_BRG")) Then
                strItemCode = Trim(objRptDs.Tables(0).Rows(intCnt).Item("KODE_BRG"))
                strParamItm = strItemCode & "|"
                Try
                    intErrNo = objPU.mtdGetItem(strOpCdItem_GET, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strAccMonth, _
                                                strAccYear, _
                                                strParamItm, _
                                                objItem)
                Catch Exp As System.Exception
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_ITEM_FOR_POLIST_REPORT&errmesg=" & Exp.Message & "&redirect=")
                End Try

                If objItem.Tables(0).Rows.Count > 0 Then
                    objRptDs.Tables(0).Rows(intCnt).Item("NAMA_BRG") = Trim(objItem.Tables(0).Rows(0).Item("Description"))
                    'objRptDs.Tables(0).Rows(intCnt).Item("PurchaseUOM") = Trim(objItem.Tables(0).Rows(0).Item("PurchaseUOM"))
                End If
            End If
        Next intCnt

        'If Not Request.QueryString("ItemCode") = "" Or Not Request.QueryString("ItemDesc") = "" Or Not Request.QueryString("ProdType") = "" Or Not Request.QueryString("ProdBrand") = "" Or Not _
        '    Request.QueryString("ProdModel") = "" Or Not Request.QueryString("ProdCat") = "" Or Not Request.QueryString("ProdMat") = "" Or Not Request.QueryString("StkAna") = "" Then
        '    If objRptDs.Tables(0).Rows.Count > 0 Then
        '        Do
        '            If IsDBNull(objRptDs.Tables(0).Rows(intCntRem).Item("POLnID")) Then
        '                objRptDs.Tables(0).Rows.RemoveAt(intCntRem)

        '                If intCntRem <> 0 Then
        '                    intCntRem = intCntRem - 1
        '                Else
        '                    intCntRem = 0
        '                End If
        '            Else
        '                intCntRem = intCntRem + 1
        '            End If
        '        Loop While intCntRem <= objRptDs.Tables(0).Rows.Count - 1
        '    End If
        'Else
        '    If objRptDs.Tables(0).Rows.Count > 0 Then
        '        Do
        '            If IsDBNull(objRptDs.Tables(0).Rows(intCntRem).Item("POLnID")) Then
        '                objRptDs.Tables(0).Rows.RemoveAt(intCntRem)

        '                If intCntRem <> 0 Then
        '                    intCntRem = intCntRem - 1
        '                Else
        '                    intCntRem = 0
        '                End If
        '            Else
        '                intCntRem = intCntRem + 1
        '            End If
        '        Loop While intCntRem <= objRptDs.Tables(0).Rows.Count - 1
        '    End If
        'End If


        'Dim strHistoryBy As String
        'Dim strRptPrefix As String

        'strHistoryBy = Request.QueryString("HistoryBy")
        'If strHistoryBy = "1" Then
        '    strRptPrefix = "PU_StdRpt_HistoricalItemPriceByItem"
        'Else
        '    strRptPrefix = "PU_StdRpt_HistoricalItemPriceBySupplier"
        'End If

        Return objRptDs

    End Function

    Sub SaveFile()

        Dim objStreamReader As StreamReader
        Dim strTable As String = Trim(Request.QueryString("DownloadID"))
        Dim strFType As String = Trim(Request.QueryString("FType"))
        Dim strFtpPath As String
        Dim strXmlPath As String = ""
        Dim strFileName As String = ""
        Dim intErrNo As Integer
        Dim strXmlb4Encrypt As String = ""
        Dim ObjXmlEncrypted As New Object()
        Dim ObjXmlDecrypted As New Object()
        Dim intCnt As Integer

        Select Case strTable
            Case "0"
                strFileName = "SPB2"
                dsResult = GetData_ItemHistory()
        End Select


        Try
            intErrNo = objSysCfg.mtdGetFtpPath(strFtpPath)
            strXmlPath = strFtpPath & strFileName
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WM_GET_FTPPATH&errmesg=&redirect=")
        End Try

        Select Case strFType
            Case "0" ' dbase
                'Connec t to dbf
                Dim sConnectionString As String

                sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" & _
                "Data Source=" & strFtpPath & ";Extended Properties=dBase IV"
                Dim objConn As New System.Data.OleDb.OleDbConnection(sConnectionString)
                objConn.Open()

                'check db exists
                Dim cmd As OleDbCommand = objConn.CreateCommand
                Dim file As System.IO.FileInfo = New System.IO.FileInfo(strXmlPath & ".dbf") '-- if the file exists on the server  
                If file.Exists Then
                    cmd.CommandText = "DELETE FROM " & strFileName
                Else
                    cmd.CommandText = "CREATE TABLE " & strFileName & " (TGL_SPB varchar(8), " & _
                                                                           "NAMA_BRG varchar(50), " & _
                                                                           "NAMA_SPL varchar(50), " & _
                                                                           "HARGA Double, " & _
                                                                           "MATAUANG varchar(3), " & _
                                                                           "AHARGA Double, " & _
                                                                           "NO_SPB varchar(25), " & _
                                                                           "NO_BO varchar(25), " & _
                                                                           "KODE_BRG varchar(12), " & _
                                                                           "KWANTITAS Double, " & _
                                                                           "KODE_SPL varchar(12), " & _
                                                                           "PERUSAHAAN varchar(6))"
                End If
                cmd.ExecuteNonQuery()

                Dim add_cmd As OleDbCommand = objConn.CreateCommand
                Dim Query As String = "INSERT INTO " & strFileName & " (TGL_SPB,NAMA_BRG,NAMA_SPL,HARGA,MATAUANG,AHARGA,NO_SPB,NO_BO,KODE_BRG,KWANTITAS,KODE_SPL,PERUSAHAAN) " & _
                                                               "VALUES (?,?,?,?,?,?,?,?,?,?,?,?)"
                For intCnt = 0 To dsResult.Tables(0).Rows.Count - 1
                    add_cmd.CommandType = CommandType.Text
                    add_cmd.CommandText = Query
                    add_cmd.Parameters.Clear()
                    add_cmd.Parameters.AddWithValue("TGL_SPB", Left(Trim(dsResult.Tables(0).Rows(intCnt).Item("TGL_SPB")), 8))
                    add_cmd.Parameters.AddWithValue("NAMA_BRG", Left(Trim(dsResult.Tables(0).Rows(intCnt).Item("NAMA_BRG")), 50))
                    add_cmd.Parameters.AddWithValue("NAMA_SPL", Left(Trim(dsResult.Tables(0).Rows(intCnt).Item("NAMA_SPL")), 50))
                    add_cmd.Parameters.AddWithValue("HARGA", dsResult.Tables(0).Rows(intCnt).Item("HARGA"))
                    add_cmd.Parameters.AddWithValue("MATAUANG", Left(Trim(dsResult.Tables(0).Rows(intCnt).Item("MATAUANG")), 3))
                    add_cmd.Parameters.AddWithValue("AHARGA", dsResult.Tables(0).Rows(intCnt).Item("AHARGA"))
                    add_cmd.Parameters.AddWithValue("NO_SPB", Left(Trim(dsResult.Tables(0).Rows(intCnt).Item("NO_SPB")), 25))
                    add_cmd.Parameters.AddWithValue("NO_BO", Left(Trim(dsResult.Tables(0).Rows(intCnt).Item("NO_BO")), 25))
                    add_cmd.Parameters.AddWithValue("KODE_BRG", Left(Trim(dsResult.Tables(0).Rows(intCnt).Item("KODE_BRG")), 12))
                    add_cmd.Parameters.AddWithValue("KWANTITAS", dsResult.Tables(0).Rows(intCnt).Item("KWANTITAS"))
                    add_cmd.Parameters.AddWithValue("KODE_SPL", Left(Trim(dsResult.Tables(0).Rows(intCnt).Item("KODE_SPL")), 12))
                    add_cmd.Parameters.AddWithValue("PERUSAHAAN", Left(Trim(dsResult.Tables(0).Rows(intCnt).Item("PERUSAHAAN")), 6))



                'add_cmd.CommandText = "INSERT INTO " & strFileName & " (TGL_SPB,NAMA_BRG,NAMA_SPL,HARGA,MATAUANG,AHARGA,NO_SPB,NO_BO,KODE_BRG,KWANTITAS,KODE_SPL,PERUSAHAAN) " & _
                '                                      "VALUES ('" & Left(Trim(dsResult.Tables(0).Rows(intCnt).Item("TGL_SPB")), 8) & "','" & _
                    '                                                    Left(Trim(dsResult.Tables(0).Rows(intCnt).Item("NAMA_BRG")), 50) & "','" & _
                '                                                    Left(Trim(dsResult.Tables(0).Rows(intCnt).Item("NAMA_SPL")), 50) & "'," & _
                '                                                   dsResult.Tables(0).Rows(intCnt).Item("HARGA") & ",'" & _
                '                                                   Left(Trim(dsResult.Tables(0).Rows(intCnt).Item("MATAUANG")), 3) & "'," & _
                '                                                   dsResult.Tables(0).Rows(intCnt).Item("AHARGA") & ",'" & _
                '                                                   Left(Trim(dsResult.Tables(0).Rows(intCnt).Item("NO_SPB")), 25) & "','" & _
                '                                                   Left(Trim(dsResult.Tables(0).Rows(intCnt).Item("NO_BO")), 25) & "','" & _
                '                                                   Left(Trim(dsResult.Tables(0).Rows(intCnt).Item("KODE_BRG")), 12) & "'," & _
                '                                                   dsResult.Tables(0).Rows(intCnt).Item("KWANTITAS") & ",'" & _
                '                                                   Left(Trim(dsResult.Tables(0).Rows(intCnt).Item("KODE_SPL")), 12) & "','" & _
                '                                                   Left(Trim(dsResult.Tables(0).Rows(intCnt).Item("PERUSAHAAN")), 6) & "')"


                    Try
                        add_cmd.ExecuteNonQuery()
                    Catch Exp As System.Data.OleDb.OleDbException
                        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WM_GET_FTPPATH&errmesg=" & Exp.Message & " Qty:" & add_cmd.CommandText & "&redirect=")
                    End Try

                Next intCnt

                

                objConn.Close()
                objConn = Nothing

                Dim filedbf As System.IO.FileInfo = New System.IO.FileInfo(strXmlPath & ".dbf") '-- if the file exists on the server  
                If filedbf.Exists Then
                    Response.Clear()
                    Response.AddHeader("Content-Disposition", "attachment; filename=" & filedbf.Name)
                    Response.AddHeader("Content-Length", filedbf.Length.ToString())
                    Response.ContentType = "application/octet-stream"
                    Response.WriteFile(filedbf.FullName)
                    Response.End()
                Else
                    Response.Write("This file does not exist.")
                End If

            Case "1" ' xml
                dsResult.Tables(0).TableName = strFileName
                dsResult.WriteXml(strXmlPath & ".xml", XmlWriteMode.WriteSchema)

                Dim file As System.IO.FileInfo = New System.IO.FileInfo(strXmlPath & ".xml") '-- if the file exists on the server  
                If file.Exists Then
                    Response.Clear()
                    Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
                    Response.AddHeader("Content-Length", file.Length.ToString())
                    Response.ContentType = "application/octet-stream"
                    Response.WriteFile(file.FullName)
                    Response.End()
                Else
                    Response.Write("This file does not exist.")
                End If
        End Select

    End Sub



End Class

