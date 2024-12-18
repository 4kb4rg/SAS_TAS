ASPX.VB excerpt

Public Class BD_MatureCropDist_Details : Inherits Page
    Protected WithEvents dgMatureCropDist As DataGrid
    Protected WithEvents lblPeriod As Label
    Dim objBD As New iTech.BD.clsTrx()
    Dim strOppCd_MatureCropDist_Format_GET As String = "BD_CLSTRX_MATURECROPDIST_FORMAT_GET"
    Dim objDataSet As New DataSet()
    Dim intErrNo As Integer
    Dim strParam As String = ""

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        InitializeBoundColumns()
    End Sub
    Private Sub InitializeBoundColumns()
        Dim strOppCd_MatureCropDist_AccPeriod_GET As String = "BD_CLSTRX_MATURECROPDIST_ACCPERIOD_GET"
        Dim intCntPeriod As Integer
        Dim strAccPeriod As String
        Dim intYear As Integer
        Dim intCntIns As Integer
        Dim strPeriod As String
        strParam = "||||||"
        Try
            intErrNo = objBD.mtdGetMatureCropDist(strOppCd_MatureCropDist_AccPeriod_GET, _
                                                  strParam, _
                                                  dsPeriod)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=BD_TRX_MATURECROPDIST_ACCPERIOD_GET&errmesg=&redirect=BD/Trx/BD_trx_MatureCropDist_Details.aspx")
        End Try
        '----------------------- A/C Period column ---------------------------------
        For intCntPeriod = 0 To dsPeriod.Tables(0).Rows.Count - 1
            strAccPeriod = Trim(dsPeriod.Tables(0).Rows(intCntPeriod).Item("AccMonth")) & "/" & Trim(dsPeriod.Tables(0).Rows(intCntPeriod).Item("AccYear"))
            If intCntPeriod = 0 Then
                intCntIns = 4
            Else
                intCntIns += 1
            End If
            Dim bcInsert As TemplateColumn = New TemplateColumn()
            bcInsert.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
            bcInsert.ItemTemplate = New DataGridTemplate(ListItemType.Item, strAccPeriod)
            dgMatureCropDist.Columns.AddAt(intCntIns, bcInsert)
        Next
    End Sub
    Private Class DataGridTemplate
        Implements ITemplate
        Dim templateType As ListItemType
        Dim colname As String
        Dim colID As String
        Sub New(ByVal type As ListItemType, ByVal pv_colname As String)
            templateType = type
            colname = pv_colname
        End Sub
        Sub InstantiateIn(ByVal container As Control) Implements ITemplate.InstantiateIn
            Dim lc As New Literal()
            Dim lb As Label
            Select Case templateType
                Case ListItemType.Item
                    AddHandler lc.DataBinding, AddressOf TemplateControl_DataBinding
                    container.Controls.Add(lc)
            End Select
        End Sub
        Private Sub TemplateControl_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim lc As Literal
            Dim container As DataGridItem
            lc = CType(sender, Literal)
            container = CType(lc.NamingContainer, DataGridItem)
            lc.Text = FormatNumber(DataBinder.Eval(container.DataItem, colname), 2)
        End Sub
    End Class
End Class

COM+ Excerpt 
<TransactionAttribute(TransactionOption.Disabled), Guid("C11E84E9-524D-4247-916F-F30D41662433")> _
Public Class clsCrypto
    Inherits ServicedComponent
    Public Sub mtdReadSource()
        Dim objStmReader As System.IO.StreamReader
        Try
            objStmReader = New System.IO.StreamReader(getPath, True)
            strSource = objStmReader.ReadToEnd()
        Catch exp As System.Exception
            Throw exp
        Finally
            objStmReader.Close()
            objStmReader = Nothing
        End Try
    End Sub

    Public Function CryptoService(ByVal Mode As CryptoMode, _
                                  ByVal pv_intEncryptType As Integer, _
                                  ByVal pv_strSource As String, _
                                  ByRef pr_strResult As Object) As Integer

        Dim cryptoProvider As New TripleDESCryptoServiceProvider()
        Dim arrIV() As Byte = {&H1, &H3, &H6, &H9, &HB, &H12, &H18, &H1E}
        Dim keyLength As Integer = 16
        Dim inputByteArray() As Byte
        Dim byteKey() As Byte = {}
        Dim ms As New MemoryStream()
        Dim cs As CryptoStream
        Dim objErrHdl As New clsErrHdl()
        Dim strKey As String = ""

        Try
            Select Case pv_intEncryptType
                Case 1
                    strKey = ENCRYPT_KEY_DATATRANSFER   '--Data Transfer Key
                Case 2
                    strKey = ENCRYPT_KEY_PASSWORD       '--Password Key
                Case Else
                    strKey = ENCRYPT_KEY_MISC2          '--Miscellaneous Key 2
            End Select
            ReDim inputByteArray(pv_strSource.Length)
            byteKey = System.Text.Encoding.UTF8.GetBytes(Left(strKey, keyLength))
            If Mode = CryptoMode.Encrypt Then
                inputByteArray = System.Text.Encoding.Unicode.GetBytes(pv_strSource)
                cs = New CryptoStream(ms, cryptoProvider.CreateEncryptor(byteKey, arrIV), CryptoStreamMode.Write)
            Else
                inputByteArray = Convert.FromBase64String(pv_strSource)
                cs = New CryptoStream(ms, cryptoProvider.CreateDecryptor(byteKey, arrIV), CryptoStreamMode.Write)
            End If
            cs.Write(inputByteArray, 0, inputByteArray.Length)
            cs.FlushFinalBlock()
            If Mode = CryptoMode.Encrypt Then
                pr_strResult = Convert.ToBase64String(ms.ToArray())
            Else
                pr_strResult = System.Text.Encoding.Unicode.GetString(ms.ToArray())
            End If
            ContextUtil.SetComplete()

        Catch exp As System.Exception
            ContextUtil.SetAbort()
            objErrHdl.mtdErrLog(168, "Library.clsCrypto.CryptoService::" & exp.Source & "::", exp.Message)
            Throw exp
        Finally
            cs.Close()
            ms.Close()
            cs = Nothing
            ms = Nothing
            cryptoProvider = Nothing
            objErrHdl = Nothing
        End Try
    End Function
End Class
