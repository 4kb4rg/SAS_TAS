
Imports System
Imports System.Data
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports System.Globalization

Public Class PM_KernelLoss_Det : Inherits Page

    Dim objPMTrx As New agri.PM.clsTrx()
    Dim objPMSetup As New agri.PM.clsSetup()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblDupMsg As Label
    Protected WithEvents lblFmt As Label
    Protected WithEvents lblDate As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents lblOER As Label
    Protected WithEvents lblKER As Label
    Protected WithEvents lblPeriod As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblCreateDate As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdateBy As Label
    Protected WithEvents lblTotalDirt As TextBox

    Protected WithEvents txtdate As TextBox
    Protected WithEvents ddlProcessingLnNo As DropDownList

    Protected WithEvents Save As ImageButton
    Protected WithEvents Delete As ImageButton
    Protected WithEvents btnSelDateFrom As Image
    Dim strTransDate As String
    Dim strEdit As String
    Dim strProcessingLnNo As String

    Dim strParam As String
    Dim objDataSet As New DataSet()
    Dim objProcessingLine As New DataSet()
    Dim objLangCapDs As New Object()
    Dim intErrNo As Integer

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intPMAR As Integer
    Dim strDateFormat As String
    Protected WithEvents lblTracker As System.Web.UI.WebControls.Label
    Protected WithEvents rfvDate As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents rfvProcessingLnNo As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents Back As System.Web.UI.WebControls.ImageButton

    Protected WithEvents txtSampelGrLTDS1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revSampelGrLTDS1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvSampelGrLTDS1 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtSampelPsLTDS1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revSampelPsLTDS1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvSampelPsLTDS1 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtNutUtuhGrLTDS1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revNutUtuhGrLTDS1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvNutUtuhGrLTDS1 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtNutUtuhPsLTDS1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revNutUtuhPsLTDS1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvNutUtuhPsLTDS1 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtNutPecahGrLTDS1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revNutPecahGrLTDS1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvNutPecahGrLTDS1 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtNutPecahPsLTDS1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revNutPecahPsLTDS1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvNutPecahPsLTDS1 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtKernelBulatGrLTDS1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revKernelBulatGrLTDS1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvKernelBulatGrLTDS1 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtKernelBulatPsLTDS1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revKernelBulatPsLTDS1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvKernelBulatPsLTDS1 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtKernelPecahGrLTDS1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revKernelPecahGrLTDS1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvKernelPecahGrLTDS1 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtKernelPecahPsLTDS1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revKernelPecahPsLTDS1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvKernelPecahPsLTDS1 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtKernelRugiGrLTDS1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revKernelRugiGrLTDS1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvKernelRugiGrLTDS1 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtKernelRugiPsLTDS1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revKernelRugiPsLTDS1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvKernelRugiPsLTDS1 As System.Web.UI.WebControls.RangeValidator


    Protected WithEvents txtSampelGrLTDS2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revSampelGrLTDS2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvSampelGrLTDS2 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtSampelPsLTDS2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revSampelPsLTDS2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvSampelPsLTDS2 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtNutUtuhGrLTDS2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revNutUtuhGrLTDS2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvNutUtuhGrLTDS2 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtNutUtuhPsLTDS2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revNutUtuhPsLTDS2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvNutUtuhPsLTDS2 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtNutPecahGrLTDS2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revNutPecahGrLTDS2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvNutPecahGrLTDS2 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtNutPecahPsLTDS2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revNutPecahPsLTDS2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvNutPecahPsLTDS2 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtKernelBulatGrLTDS2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revKernelBulatGrLTDS2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvKernelBulatGrLTDS2 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtKernelBulatPsLTDS2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revKernelBulatPsLTDS2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvKernelBulatPsLTDS2 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtKernelPecahGrLTDS2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revKernelPecahGrLTDS2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvKernelPecahGrLTDS2 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtKernelPecahPsLTDS2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revKernelPecahPsLTDS2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvKernelPecahPsLTDS2 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtKernelRugiGrLTDS2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revKernelRugiGrLTDS2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvKernelRugiGrLTDS2 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtKernelRugiPsLTDS2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revKernelRugiPsLTDS2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvKernelRugiPsLTDS2 As System.Web.UI.WebControls.RangeValidator


    Protected WithEvents txtSampelGrCB As System.Web.UI.WebControls.TextBox
    Protected WithEvents revSampelGrCB As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvSampelGrCB As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtSampelPsCB As System.Web.UI.WebControls.TextBox
    Protected WithEvents revSampelPsCB As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvSampelPsCB As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtNutUtuhGrCB As System.Web.UI.WebControls.TextBox
    Protected WithEvents revNutUtuhGrCB As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvNutUtuhGrCB As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtNutUtuhPsCB As System.Web.UI.WebControls.TextBox
    Protected WithEvents revNutUtuhPsCB As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvNutUtuhPsCB As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtNutPecahGrCB As System.Web.UI.WebControls.TextBox
    Protected WithEvents revNutPecahGrCB As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvNutPecahGrCB As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtNutPecahPsCB As System.Web.UI.WebControls.TextBox
    Protected WithEvents revNutPecahPsCB As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvNutPecahPsCB As System.Web.UI.WebControls.RangeValidator

  
    Protected WithEvents txtKernelBulatGrCB As System.Web.UI.WebControls.TextBox
    Protected WithEvents revKernelBulatGrCB As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvKernelBulatGrCB As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtKernelBulatPsCB As System.Web.UI.WebControls.TextBox
    Protected WithEvents revKernelBulatPsCB As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvKernelBulatPsCB As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtKernelPecahGrCB As System.Web.UI.WebControls.TextBox
    Protected WithEvents revKernelPecahGrCB As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvKernelPecahGrCB As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtKernelPecahPsCB As System.Web.UI.WebControls.TextBox
    Protected WithEvents revKernelPecahPsCB As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvKernelPecahPsCB As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtKernelRugiGrCB As System.Web.UI.WebControls.TextBox
    Protected WithEvents revKernelRugiGrCB As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvKernelRugiGrCB As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtKernelRugiPsCB As System.Web.UI.WebControls.TextBox
    Protected WithEvents revKernelRugiPsCB As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvKernelRugiPsCB As System.Web.UI.WebControls.RangeValidator


    Protected WithEvents txtSampelGrPC1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revSampelGrPC1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvSampelGrPC1 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtSampelPsPC1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revSampelPsPC1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvSampelPsPC1 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtNutUtuhGrPC1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revNutUtuhGrPC1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvNutUtuhGrPC1 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtNutUtuhPsPC1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revNutUtuhPsPC1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvNutUtuhPsPC1 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtNutPecahGrPC1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revNutPecahGrPC1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvNutPecahGrPC1 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtNutPecahPsPC1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revNutPecahPsPC1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvNutPecahPsPC1 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtKernelBulatGrPC1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revKernelBulatGrPC1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvKernelBulatGrPC1 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtKernelBulatPsPC1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revKernelBulatPsPC1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvKernelBulatPsPC1 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtKernelPecahGrPC1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revKernelPecahGrPC1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvKernelPecahGrPC1 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtKernelPecahPsPC1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revKernelPecahPsPC1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvKernelPecahPsPC1 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtCangkangGrPC1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revCangkangGrPC1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvCangkangGrPC1 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtCangkangPsPC1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revCangkangPsPC1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvCangkangPsPC1 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtPecahPerTotalGrPC1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revPecahPerTotalGrPC1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvPecahPerTotalGrPC1 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtPecahPerTotalPsPC1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revPecahPerTotalPsPC1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvPecahPerTotalPsPC1 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtTotalPerSampelGrPC1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revTotalPerSampelGrPC1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvTotalPerSampelGrPC1 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtTotalPerSampelPsPC1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revTotalPerSampelPsPC1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvTotalPerSampelPsPC1 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtSampelGrPC2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revSampelGrPC2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvSampelGrPC2 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtSampelPsPC2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revSampelPsPC2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvSampelPsPC2 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtNutUtuhGrPC2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revNutUtuhGrPC2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvNutUtuhGrPC2 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtNutUtuhPsPC2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revNutUtuhPsPC2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvNutUtuhPsPC2 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtNutPecahGrPC2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revNutPecahGrPC2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvNutPecahGrPC2 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtNutPecahPsPC2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revNutPecahPsPC2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvNutPecahPsPC2 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtKernelBulatGrPC2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revKernelBulatGrPC2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvKernelBulatGrPC2 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtKernelBulatPsPC2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revKernelBulatPsPC2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvKernelBulatPsPC2 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtKernelPecahGrPC2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revKernelPecahGrPC2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvKernelPecahGrPC2 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtKernelPecahPsPC2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revKernelPecahPsPC2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvKernelPecahPsPC2 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtCangkangGrPC2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revCangkangGrPC2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvCangkangGrPC2 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtCangkangPsPC2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revCangkangPsPC2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvCangkangPsPC2 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtPecahPerTotalGrPC2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revPecahPerTotalGrPC2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvPecahPerTotalGrPC2 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtPecahPerTotalPsPC2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revPecahPerTotalPsPC2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvPecahPerTotalPsPC2 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtTotalPerSampelGrPC2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revTotalPerSampelGrPC2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvTotalPerSampelGrPC2 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtTotalPerSampelPsPC2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revTotalPerSampelPsPC2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvTotalPerSampelPsPC2 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtSampelGrPC3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revSampelGrPC3 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvSampelGrPC3 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtSampelPsPC3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revSampelPsPC3 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvSampelPsPC3 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtNutUtuhGrPC3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revNutUtuhGrPC3 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvNutUtuhGrPC3 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtNutUtuhPsPC3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revNutUtuhPsPC3 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvNutUtuhPsPC3 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtNutPecahGrPC3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revNutPecahGrPC3 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvNutPecahGrPC3 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtNutPecahPsPC3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revNutPecahPsPC3 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvNutPecahPsPC3 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtKernelBulatGrPC3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revKernelBulatGrPC3 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvKernelBulatGrPC3 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtKernelBulatPsPC3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revKernelBulatPsPC3 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvKernelBulatPsPC3 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtKernelPecahGrPC3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revKernelPecahGrPC3 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvKernelPecahGrPC3 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtKernelPecahPsPC3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revKernelPecahPsPC3 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvKernelPecahPsPC3 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtCangkangGrPC3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revCangkangGrPC3 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvCangkangGrPC3 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtCangkangPsPC3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revCangkangPsPC3 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvCangkangPsPC3 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtPecahPerTotalGrPC3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revPecahPerTotalGrPC3 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvPecahPerTotalGrPC3 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtPecahPerTotalPsPC3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revPecahPerTotalPsPC3 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvPecahPerTotalPsPC3 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtTotalPerSampelGrPC3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revTotalPerSampelGrPC3 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvTotalPerSampelGrPC3 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtTotalPerSampelPsPC3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revTotalPerSampelPsPC3 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvTotalPerSampelPsPC3 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtSampelGrPC4 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revSampelGrPC4 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvSampelGrPC4 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtSampelPsPC4 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revSampelPsPC4 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvSampelPsPC4 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtNutUtuhGrPC4 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revNutUtuhGrPC4 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvNutUtuhGrPC4 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtNutUtuhPsPC4 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revNutUtuhPsPC4 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvNutUtuhPsPC4 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtNutPecahGrPC4 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revNutPecahGrPC4 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvNutPecahGrPC4 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtNutPecahPsPC4 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revNutPecahPsPC4 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvNutPecahPsPC4 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtKernelBulatGrPC4 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revKernelBulatGrPC4 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvKernelBulatGrPC4 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtKernelBulatPsPC4 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revKernelBulatPsPC4 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvKernelBulatPsPC4 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtKernelPecahGrPC4 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revKernelPecahGrPC4 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvKernelPecahGrPC4 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtKernelPecahPsPC4 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revKernelPecahPsPC4 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvKernelPecahPsPC4 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtCangkangGrPC4 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revCangkangGrPC4 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvCangkangGrPC4 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtCangkangPsPC4 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revCangkangPsPC4 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvCangkangPsPC4 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtPecahPerTotalGrPC4 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revPecahPerTotalGrPC4 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvPecahPerTotalGrPC4 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtPecahPerTotalPsPC4 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revPecahPerTotalPsPC4 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvPecahPerTotalPsPC4 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtTotalPerSampelGrPC4 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revTotalPerSampelGrPC4 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvTotalPerSampelGrPC4 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtTotalPerSampelPsPC4 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revTotalPerSampelPsPC4 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvTotalPerSampelPsPC4 As System.Web.UI.WebControls.RangeValidator


    Protected WithEvents txtSampelGrRM1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revSampelGrRM1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvSampelGrRM1 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtSampelPsRM1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revSampelPsRM1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvSampelPsRM1 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtNutUtuhGrRM1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revNutUtuhGrRM1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvNutUtuhGrRM1 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtNutUtuhPsRM1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revNutUtuhPsRM1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvNutUtuhPsRM1 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtNutPecahGrRM1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revNutPecahGrRM1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvNutPecahGrRM1 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtNutPecahPsRM1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revNutPecahPsRM1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvNutPecahPsRM1 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtKernelBulatGrRM1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revKernelBulatGrRM1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvKernelBulatGrRM1 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtKernelBulatPsRM1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revKernelBulatPsRM1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvKernelBulatPsRM1 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtKernelPecahGrRM1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revKernelPecahGrRM1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvKernelPecahGrRM1 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtKernelPecahPsRM1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revKernelPecahPsRM1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvKernelPecahPsRM1 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtCangkangGrRM1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revCangkangGrRM1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvCangkangGrRM1 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtCangkangPsRM1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revCangkangPsRM1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvCangkangPsRM1 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtEffisiensiGrRM1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revEffisiensiGrRM1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvEffisiensiGrRM1 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtEffisiensiPsRM1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revEffisiensiPsRM1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvEffisiensiPsRM1 As System.Web.UI.WebControls.RangeValidator


    Protected WithEvents txtSampelGrRM2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revSampelGrRM2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvSampelGrRM2 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtSampelPsRM2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revSampelPsRM2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvSampelPsRM2 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtNutUtuhGrRM2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revNutUtuhGrRM2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvNutUtuhGrRM2 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtNutUtuhPsRM2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revNutUtuhPsRM2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvNutUtuhPsRM2 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtNutPecahGrRM2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revNutPecahGrRM2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvNutPecahGrRM2 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtNutPecahPsRM2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revNutPecahPsRM2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvNutPecahPsRM2 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtKernelBulatGrRM2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revKernelBulatGrRM2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvKernelBulatGrRM2 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtKernelBulatPsRM2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revKernelBulatPsRM2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvKernelBulatPsRM2 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtKernelPecahGrRM2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revKernelPecahGrRM2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvKernelPecahGrRM2 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtKernelPecahPsRM2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revKernelPecahPsRM2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvKernelPecahPsRM2 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtCangkangGrRM2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revCangkangGrRM2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvCangkangGrRM2 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtCangkangPsRM2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revCangkangPsRM2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvCangkangPsRM2 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtEffisiensiGrRM2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revEffisiensiGrRM2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvEffisiensiGrRM2 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtEffisiensiPsRM2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revEffisiensiPsRM2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvEffisiensiPsRM2 As System.Web.UI.WebControls.RangeValidator


    Protected WithEvents txtSampelGrRM3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revSampelGrRM3 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvSampelGrRM3 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtSampelPsRM3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revSampelPsRM3 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvSampelPsRM3 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtNutUtuhGrRM3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revNutUtuhGrRM3 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvNutUtuhGrRM3 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtNutUtuhPsRM3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revNutUtuhPsRM3 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvNutUtuhPsRM3 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtNutPecahGrRM3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revNutPecahGrRM3 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvNutPecahGrRM3 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtNutPecahPsRM3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revNutPecahPsRM3 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvNutPecahPsRM3 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtKernelBulatGrRM3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revKernelBulatGrRM3 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvKernelBulatGrRM3 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtKernelBulatPsRM3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revKernelBulatPsRM3 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvKernelBulatPsRM3 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtKernelPecahGrRM3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revKernelPecahGrRM3 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvKernelPecahGrRM3 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtKernelPecahPsRM3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revKernelPecahPsRM3 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvKernelPecahPsRM3 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtCangkangGrRM3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revCangkangGrRM3 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvCangkangGrRM3 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtCangkangPsRM3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revCangkangPsRM3 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvCangkangPsRM3 As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtEffisiensiGrRM3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revEffisiensiGrRM3 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvEffisiensiGrRM3 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtEffisiensiPsRM3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revEffisiensiPsRM3 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvEffisiensiPsRM3 As System.Web.UI.WebControls.RangeValidator

    Dim strOppCd_KernelLosses_GET As String = "PM_CLSTRX_KERNEL_LOSSES_GET"

    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String

    Private Sub InitializeComponent()

    End Sub

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PMACCMONTH")
        strAccYear = Session("SS_PMACCYEAR")
        intPMAR = Session("SS_PMAR")
        strDateFormat = Session("SS_DATEFMT")

        strLocType = Session("SS_LOCTYPE")
        strTransDate = Request.QueryString("TransDate")
        strEdit = Request.QueryString("Edit")
        strProcessingLnNo = Request.QueryString("ProcessingLnNo")


        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMKernelLoss), intPMAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()

            If Not Page.IsPostBack Then
                BindProcessingLineList()
                If Not (Request.QueryString("LocCode") = "" And Request.QueryString("TransDate") = "") Then
                    strTransDate = Request.QueryString("TransDate")
                    strEdit = Request.QueryString("Edit")
                End If

                If strEdit = "True" Then
                    DisplayData()
                    blnUpdate.Text = False
                    txtdate.Enabled = False
                    ddlProcessingLnNo.Enabled = False
                    btnSelDateFrom.Visible = False
                Else
                    blnUpdate.Text = True
                    txtdate.Enabled = True
                    ddlProcessingLnNo.Enabled = True
                    btnSelDateFrom.Visible = True
                End If
            End If
        End If
    End Sub

    Sub onload_GetLangCap()
    End Sub

    Sub GetEntireLangCap()
        Dim strOpCode_BussTerm As String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
        Dim strParam As String
        Dim intErrNo As Integer

        strParam = strLangCode
        Try
            intErrNo = objLangCap.mtdGetBussTerm(strOpCode_BussTerm, _
                                                 strCompany, _
                                                 strLocation, _
                                                 strUserId, _
                                                 strAccMonth, _
                                                 strAccYear, _
                                                 objLangCapDs, _
                                                 strParam)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_OIL_LOSS_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/pm_trx_KernelLoss_list.aspx")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

        For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If strLocType = objAdminLoc.EnumLocType.Mill Then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                Else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                End If
                Exit For
            End If
        Next
    End Function

    Protected Function LoadData() As DataSet

        strParam = "||TransDate||" & _
                   strTransDate & "||" & strProcessingLnNo

        Try
            intErrNo = objPMTrx.mtdGetKernelLosses(strOppCd_KernelLosses_GET, strLocation, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_KernelLoss_GET&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/PM_trx_KernelLoss_List.aspx")
        End Try

        Return objDataSet
    End Function

    Sub DisableControl(ByVal pv_strstatus)
        Dim strView As Boolean

        If pv_strstatus = objPMTrx.EnumKernelLossStatus.Deleted Then
            strView = False

        ElseIf pv_strstatus = objPMTrx.EnumKernelLossStatus.Active Then
            strView = True
        End If

        txtdate.Enabled = strView
        Save.Visible = strView

    End Sub

    Sub DisplayData()

        Dim dsKernelLosses As DataSet = LoadData()

        If dsKernelLosses.Tables(0).Rows.Count > 0 Then
            txtdate.Text = objGlobal.GetShortDate(strDateFormat, Trim(dsKernelLosses.Tables(0).Rows(0).Item("TransDate")))
            ddlProcessingLnNo.SelectedItem.Text = Trim(dsKernelLosses.Tables(0).Rows(0).Item("ProcessLnNo"))
            ddlProcessingLnNo.SelectedItem.Value = Trim(dsKernelLosses.Tables(0).Rows(0).Item("ProcessLnNo"))

            With dsKernelLosses.Tables(0).Rows(0)
                txtSampelGrLTDS1.Text = .Item("SampelGrLTDS1")
                txtSampelPsLTDS1.Text = .Item("SampelPsLTDS1")
                txtNutUtuhGrLTDS1.Text = .Item("NutUtuhGrLTDS1")
                txtNutUtuhPsLTDS1.Text = .Item("NutUtuhPsLTDS1")
                txtNutPecahGrLTDS1.Text = .Item("NutPecahGrLTDS1")
                txtNutPecahPsLTDS1.Text = .Item("NutPecahPsLTDS1")
                txtKernelBulatGrLTDS1.Text = .Item("KernelBulatGrLTDS1")
                txtKernelBulatPsLTDS1.Text = .Item("KernelBulatPsLTDS1")
                txtKernelPecahGrLTDS1.Text = .Item("KernelPecahGrLTDS1")
                txtKernelPecahPsLTDS1.Text = .Item("KernelPecahPsLTDS1")
                txtKernelRugiGrLTDS1.Text = .Item("KernelRugiGrLTDS1")
                txtKernelRugiPsLTDS1.Text = .Item("KernelRugiPsLTDS1")
                txtSampelGrLTDS2.Text = .Item("SampelGrLTDS2")
                txtSampelPsLTDS2.Text = .Item("SampelPsLTDS2")
                txtNutUtuhGrLTDS2.Text = .Item("NutUtuhGrLTDS2")
                txtNutUtuhPsLTDS2.Text = .Item("NutUtuhPsLTDS2")
                txtNutPecahGrLTDS2.Text = .Item("NutPecahGrLTDS2")
                txtNutPecahPsLTDS2.Text = .Item("NutPecahPsLTDS2")
                txtKernelBulatGrLTDS2.Text = .Item("KernelBulatGrLTDS2")
                txtKernelBulatPsLTDS2.Text = .Item("KernelBulatPsLTDS2")
                txtKernelPecahGrLTDS2.Text = .Item("KernelPecahGrLTDS2")
                txtKernelPecahPsLTDS2.Text = .Item("KernelPecahPsLTDS2")
                txtKernelRugiGrLTDS2.Text = .Item("KernelRugiGrLTDS2")
                txtKernelRugiPsLTDS2.Text = .Item("KernelRugiPsLTDS2")
                txtSampelGrCB.Text = .Item("SampelGrCB")
                txtSampelPsCB.Text = .Item("SampelPsCB")
                txtNutUtuhGrCB.Text = .Item("NutUtuhGrCB")
                txtNutUtuhPsCB.Text = .Item("NutUtuhPsCB")
                txtNutPecahGrCB.Text = .Item("NutPecahGrCB")
                txtNutPecahPsCB.Text = .Item("NutPecahPsCB")
                txtKernelBulatGrCB.Text = .Item("KernelBulatGrCB")
                txtKernelBulatPsCB.Text = .Item("KernelBulatPsCB")
                txtKernelPecahGrCB.Text = .Item("KernelPecahGrCB")
                txtKernelPecahPsCB.Text = .Item("KernelPecahPsCB")
                txtKernelRugiGrCB.Text = .Item("KernelRugiGrCB")
                txtKernelRugiPsCB.Text = .Item("KernelRugiPsCB")
                txtSampelGrPC1.Text = .Item("SampelGrPC1")
                txtSampelPsPC1.Text = .Item("SampelPsPC1")
                txtNutUtuhGrPC1.Text = .Item("NutUtuhGrPC1")
                txtNutUtuhPsPC1.Text = .Item("NutUtuhPsPC1")
                txtNutPecahGrPC1.Text = .Item("NutPecahGrPC1")
                txtNutPecahPsPC1.Text = .Item("NutPecahPsPC1")
                txtKernelBulatGrPC1.Text = .Item("KernelBulatGrPC1")
                txtKernelBulatPsPC1.Text = .Item("KernelBulatPsPC1")
                txtKernelPecahGrPC1.Text = .Item("KernelPecahGrPC1")
                txtKernelPecahPsPC1.Text = .Item("KernelPecahPsPC1")
                txtCangkangGrPC1.Text = .Item("CangkangGrPC1")
                txtCangkangPsPC1.Text = .Item("CangkangPsPC1")
                txtPecahPerTotalGrPC1.Text = .Item("PecahPerTotalGrPC1")
                txtPecahPerTotalPsPC1.Text = .Item("PecahPerTotalPsPC1")
                txtTotalPerSampelGrPC1.Text = .Item("TotalPerSampelGrPC1")
                txtTotalPerSampelPsPC1.Text = .Item("TotalPerSampelPsPC1")
                txtSampelGrPC2.Text = .Item("SampelGrPC2")
                txtSampelPsPC2.Text = .Item("SampelPsPC2")
                txtNutUtuhGrPC2.Text = .Item("NutUtuhGrPC2")
                txtNutUtuhPsPC2.Text = .Item("NutUtuhPsPC2")
                txtNutPecahGrPC2.Text = .Item("NutPecahGrPC2")
                txtNutPecahPsPC2.Text = .Item("NutPecahPsPC2")
                txtKernelBulatGrPC2.Text = .Item("KernelBulatGrPC2")
                txtKernelBulatPsPC2.Text = .Item("KernelBulatPsPC2")
                txtKernelPecahGrPC2.Text = .Item("KernelPecahGrPC2")
                txtKernelPecahPsPC2.Text = .Item("KernelPecahPsPC2")
                txtCangkangGrPC2.Text = .Item("CangkangGrPC2")
                txtCangkangPsPC2.Text = .Item("CangkangPsPC2")
                txtPecahPerTotalGrPC2.Text = .Item("PecahPerTotalGrPC2")
                txtPecahPerTotalPsPC2.Text = .Item("PecahPerTotalPsPC2")
                txtTotalPerSampelGrPC2.Text = .Item("TotalPerSampelGrPC2")
                txtTotalPerSampelPsPC2.Text = .Item("TotalPerSampelPsPC2")
                txtSampelGrPC3.Text = .Item("SampelGrPC3")
                txtSampelPsPC3.Text = .Item("SampelPsPC3")
                txtNutUtuhGrPC3.Text = .Item("NutUtuhGrPC3")
                txtNutUtuhPsPC3.Text = .Item("NutUtuhPsPC3")
                txtNutPecahGrPC3.Text = .Item("NutPecahGrPC3")
                txtNutPecahPsPC3.Text = .Item("NutPecahPsPC3")
                txtKernelBulatGrPC3.Text = .Item("KernelBulatGrPC3")
                txtKernelBulatPsPC3.Text = .Item("KernelBulatPsPC3")
                txtKernelPecahGrPC3.Text = .Item("KernelPecahGrPC3")
                txtKernelPecahPsPC3.Text = .Item("KernelPecahPsPC3")
                txtCangkangGrPC3.Text = .Item("CangkangGrPC3")
                txtCangkangPsPC3.Text = .Item("CangkangPsPC3")
                txtPecahPerTotalGrPC3.Text = .Item("PecahPerTotalGrPC3")
                txtPecahPerTotalPsPC3.Text = .Item("PecahPerTotalPsPC3")
                txtTotalPerSampelGrPC3.Text = .Item("TotalPerSampelGrPC3")
                txtTotalPerSampelPsPC3.Text = .Item("TotalPerSampelPsPC3")
                txtSampelGrPC4.Text = .Item("SampelGrPC4")
                txtSampelPsPC4.Text = .Item("SampelPsPC4")
                txtNutUtuhGrPC4.Text = .Item("NutUtuhGrPC4")
                txtNutUtuhPsPC4.Text = .Item("NutUtuhPsPC4")
                txtNutPecahGrPC4.Text = .Item("NutPecahGrPC4")
                txtNutPecahPsPC4.Text = .Item("NutPecahPsPC4")
                txtKernelBulatGrPC4.Text = .Item("KernelBulatGrPC4")
                txtKernelBulatPsPC4.Text = .Item("KernelBulatPsPC4")
                txtKernelPecahGrPC4.Text = .Item("KernelPecahGrPC4")
                txtKernelPecahPsPC4.Text = .Item("KernelPecahPsPC4")
                txtCangkangGrPC4.Text = .Item("CangkangGrPC4")
                txtCangkangPsPC4.Text = .Item("CangkangPsPC4")
                txtPecahPerTotalGrPC4.Text = .Item("PecahPerTotalGrPC4")
                txtPecahPerTotalPsPC4.Text = .Item("PecahPerTotalPsPC4")
                txtTotalPerSampelGrPC4.Text = .Item("TotalPerSampelGrPC4")
                txtTotalPerSampelPsPC4.Text = .Item("TotalPerSampelPsPC4")
                txtSampelGrRM1.Text = .Item("SampelGrRM1")
                txtSampelPsRM1.Text = .Item("SampelPsRM1")
                txtNutUtuhGrRM1.Text = .Item("NutUtuhGrRM1")
                txtNutUtuhPsRM1.Text = .Item("NutUtuhPsRM1")
                txtNutPecahGrRM1.Text = .Item("NutPecahGrRM1")
                txtNutPecahPsRM1.Text = .Item("NutPecahPsRM1")
                txtKernelBulatGrRM1.Text = .Item("KernelBulatGrRM1")
                txtKernelBulatPsRM1.Text = .Item("KernelBulatPsRM1")
                txtKernelPecahGrRM1.Text = .Item("KernelPecahGrRM1")
                txtKernelPecahPsRM1.Text = .Item("KernelPecahPsRM1")
                txtCangkangGrRM1.Text = .Item("CangkangGrRM1")
                txtCangkangPsRM1.Text = .Item("CangkangPsRM1")
                txtEffisiensiGrRM1.Text = .Item("EffisiensiGrRM1")
                txtEffisiensiPsRM1.Text = .Item("EffisiensiPsRM1")
                txtSampelGrRM2.Text = .Item("SampelGrRM2")
                txtSampelPsRM2.Text = .Item("SampelPsRM2")
                txtNutUtuhGrRM2.Text = .Item("NutUtuhGrRM2")
                txtNutUtuhPsRM2.Text = .Item("NutUtuhPsRM2")
                txtNutPecahGrRM2.Text = .Item("NutPecahGrRM2")
                txtNutPecahPsRM2.Text = .Item("NutPecahPsRM2")
                txtKernelBulatGrRM2.Text = .Item("KernelBulatGrRM2")
                txtKernelBulatPsRM2.Text = .Item("KernelBulatPsRM2")
                txtKernelPecahGrRM2.Text = .Item("KernelPecahGrRM2")
                txtKernelPecahPsRM2.Text = .Item("KernelPecahPsRM2")
                txtCangkangGrRM2.Text = .Item("CangkangGrRM2")
                txtCangkangPsRM2.Text = .Item("CangkangPsRM2")
                txtEffisiensiGrRM2.Text = .Item("EffisiensiGrRM2")
                txtEffisiensiPsRM2.Text = .Item("EffisiensiPsRM2")
                txtSampelGrRM3.Text = .Item("SampelGrRM3")
                txtSampelPsRM3.Text = .Item("SampelPsRM3")
                txtNutUtuhGrRM3.Text = .Item("NutUtuhGrRM3")
                txtNutUtuhPsRM3.Text = .Item("NutUtuhPsRM3")
                txtNutPecahGrRM3.Text = .Item("NutPecahGrRM3")
                txtNutPecahPsRM3.Text = .Item("NutPecahPsRM3")
                txtKernelBulatGrRM3.Text = .Item("KernelBulatGrRM3")
                txtKernelBulatPsRM3.Text = .Item("KernelBulatPsRM3")
                txtKernelPecahGrRM3.Text = .Item("KernelPecahGrRM3")
                txtKernelPecahPsRM3.Text = .Item("KernelPecahPsRM3")
                txtCangkangGrRM3.Text = .Item("CangkangGrRM3")
                txtCangkangPsRM3.Text = .Item("CangkangPsRM3")
                txtEffisiensiGrRM3.Text = .Item("EffisiensiGrRM3")
                txtEffisiensiPsRM3.Text = .Item("EffisiensiPsRM3")
            End With

            lblPeriod.Text = Trim(dsKernelLosses.Tables(0).Rows(0).Item("AccMonth")) & "/" & Trim(dsKernelLosses.Tables(0).Rows(0).Item("AccYear"))
            lblStatus.Text = objPMTrx.mtdGetKernelLossStatus(Trim(dsKernelLosses.Tables(0).Rows(0).Item("Status")))
            lblCreateDate.Text = objGlobal.GetLongDate(Trim(dsKernelLosses.Tables(0).Rows(0).Item("CreateDate")))
            lblLastUpdate.Text = objGlobal.GetLongDate(Trim(dsKernelLosses.Tables(0).Rows(0).Item("UpdateDate")))
            lblUpdateBy.Text = Trim(dsKernelLosses.Tables(0).Rows(0).Item("Username"))

            DisableControl(Trim(dsKernelLosses.Tables(0).Rows(0).Item("Status")))
            Delete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Delete.Visible = True
        End If
    End Sub


    Sub UpdateData(ByVal strAction As String)
        Dim strOppCd_KernelLosses_ADD As String = "PM_CLSTRX_KERNEL_LOSSES_ADD"
        Dim strOppCd_KernelLosses_UPD As String = "PM_CLSTRX_KERNEL_LOSSES_UPD"
        Dim blnDupKey As Boolean = False
        Dim blnUpd As Boolean = False
        Dim strStatus As String
        Dim strDate As String = CheckDate()
        Dim strProcessingLnNo As String
        Dim strParam1 As String
        Dim objDataSet1 As New DataSet
        Dim strSampelGrLTDS1 As String
        Dim strSampelPsLTDS1 As String
        Dim strNutUtuhGrLTDS1 As String
        Dim strNutUtuhPsLTDS1 As String
        Dim strNutPecahGrLTDS1 As String
        Dim strNutPecahPsLTDS1 As String
        Dim strKernelBulatGrLTDS1 As String
        Dim strKernelBulatPsLTDS1 As String
        Dim strKernelPecahGrLTDS1 As String
        Dim strKernelPecahPsLTDS1 As String
        Dim strKernelRugiGrLTDS1 As String
        Dim strKernelRugiPsLTDS1 As String
        Dim strSampelGrLTDS2 As String
        Dim strSampelPsLTDS2 As String
        Dim strNutUtuhGrLTDS2 As String
        Dim strNutUtuhPsLTDS2 As String
        Dim strNutPecahGrLTDS2 As String
        Dim strNutPecahPsLTDS2 As String
        Dim strKernelBulatGrLTDS2 As String
        Dim strKernelBulatPsLTDS2 As String
        Dim strKernelPecahGrLTDS2 As String
        Dim strKernelPecahPsLTDS2 As String
        Dim strKernelRugiGrLTDS2 As String
        Dim strKernelRugiPsLTDS2 As String
        Dim strSampelGrCB As String
        Dim strSampelPsCB As String
        Dim strNutUtuhGrCB As String
        Dim strNutUtuhPsCB As String
        Dim strNutPecahGrCB As String
        Dim strNutPecahPsCB As String
        Dim strKernelBulatGrCB As String
        Dim strKernelBulatPsCB As String
        Dim strKernelPecahGrCB As String
        Dim strKernelPecahPsCB As String
        Dim strKernelRugiGrCB As String
        Dim strKernelRugiPsCB As String
        Dim strSampelGrPC1 As String
        Dim strSampelPsPC1 As String
        Dim strNutUtuhGrPC1 As String
        Dim strNutUtuhPsPC1 As String
        Dim strNutPecahGrPC1 As String
        Dim strNutPecahPsPC1 As String
        Dim strKernelBulatGrPC1 As String
        Dim strKernelBulatPsPC1 As String
        Dim strKernelPecahGrPC1 As String
        Dim strKernelPecahPsPC1 As String
        Dim strCangkangGrPC1 As String
        Dim strCangkangPsPC1 As String
        Dim strPecahPerTotalGrPC1 As String
        Dim strPecahPerTotalPsPC1 As String
        Dim strTotalPerSampelGrPC1 As String
        Dim strTotalPerSampelPsPC1 As String
        Dim strSampelGrPC2 As String
        Dim strSampelPsPC2 As String
        Dim strNutUtuhGrPC2 As String
        Dim strNutUtuhPsPC2 As String
        Dim strNutPecahGrPC2 As String
        Dim strNutPecahPsPC2 As String
        Dim strKernelBulatGrPC2 As String
        Dim strKernelBulatPsPC2 As String
        Dim strKernelPecahGrPC2 As String
        Dim strKernelPecahPsPC2 As String
        Dim strCangkangGrPC2 As String
        Dim strCangkangPsPC2 As String
        Dim strPecahPerTotalGrPC2 As String
        Dim strPecahPerTotalPsPC2 As String
        Dim strTotalPerSampelGrPC2 As String
        Dim strTotalPerSampelPsPC2 As String
        Dim strSampelGrPC3 As String
        Dim strSampelPsPC3 As String
        Dim strNutUtuhGrPC3 As String
        Dim strNutUtuhPsPC3 As String
        Dim strNutPecahGrPC3 As String
        Dim strNutPecahPsPC3 As String
        Dim strKernelBulatGrPC3 As String
        Dim strKernelBulatPsPC3 As String
        Dim strKernelPecahGrPC3 As String
        Dim strKernelPecahPsPC3 As String
        Dim strCangkangGrPC3 As String
        Dim strCangkangPsPC3 As String
        Dim strPecahPerTotalGrPC3 As String
        Dim strPecahPerTotalPsPC3 As String
        Dim strTotalPerSampelGrPC3 As String
        Dim strTotalPerSampelPsPC3 As String
        Dim strSampelGrPC4 As String
        Dim strSampelPsPC4 As String
        Dim strNutUtuhGrPC4 As String
        Dim strNutUtuhPsPC4 As String
        Dim strNutPecahGrPC4 As String
        Dim strNutPecahPsPC4 As String
        Dim strKernelBulatGrPC4 As String
        Dim strKernelBulatPsPC4 As String
        Dim strKernelPecahGrPC4 As String
        Dim strKernelPecahPsPC4 As String
        Dim strCangkangGrPC4 As String
        Dim strCangkangPsPC4 As String
        Dim strPecahPerTotalGrPC4 As String
        Dim strPecahPerTotalPsPC4 As String
        Dim strTotalPerSampelGrPC4 As String
        Dim strTotalPerSampelPsPC4 As String
        Dim strSampelGrRM1 As String
        Dim strSampelPsRM1 As String
        Dim strNutUtuhGrRM1 As String
        Dim strNutUtuhPsRM1 As String
        Dim strNutPecahGrRM1 As String
        Dim strNutPecahPsRM1 As String
        Dim strKernelBulatGrRM1 As String
        Dim strKernelBulatPsRM1 As String
        Dim strKernelPecahGrRM1 As String
        Dim strKernelPecahPsRM1 As String
        Dim strCangkangGrRM1 As String
        Dim strCangkangPsRM1 As String
        Dim strEffisiensiGrRM1 As String
        Dim strEffisiensiPsRM1 As String
        Dim strSampelGrRM2 As String
        Dim strSampelPsRM2 As String
        Dim strNutUtuhGrRM2 As String
        Dim strNutUtuhPsRM2 As String
        Dim strNutPecahGrRM2 As String
        Dim strNutPecahPsRM2 As String
        Dim strKernelBulatGrRM2 As String
        Dim strKernelBulatPsRM2 As String
        Dim strKernelPecahGrRM2 As String
        Dim strKernelPecahPsRM2 As String
        Dim strCangkangGrRM2 As String
        Dim strCangkangPsRM2 As String
        Dim strEffisiensiGrRM2 As String
        Dim strEffisiensiPsRM2 As String
        Dim strSampelGrRM3 As String
        Dim strSampelPsRM3 As String
        Dim strNutUtuhGrRM3 As String
        Dim strNutUtuhPsRM3 As String
        Dim strNutPecahGrRM3 As String
        Dim strNutPecahPsRM3 As String
        Dim strKernelBulatGrRM3 As String
        Dim strKernelBulatPsRM3 As String
        Dim strKernelPecahGrRM3 As String
        Dim strKernelPecahPsRM3 As String
        Dim strCangkangGrRM3 As String
        Dim strCangkangPsRM3 As String
        Dim strEffisiensiGrRM3 As String
        Dim strEffisiensiPsRM3 As String


        strProcessingLnNo = ddlProcessingLnNo.SelectedItem.Text
        strSampelGrLTDS1 = txtSampelGrLTDS1.Text
        strSampelPsLTDS1 = txtSampelPsLTDS1.Text
        strNutUtuhGrLTDS1 = txtNutUtuhGrLTDS1.Text
        strNutUtuhPsLTDS1 = txtNutUtuhPsLTDS1.Text
        strNutPecahGrLTDS1 = txtNutPecahGrLTDS1.Text
        strNutPecahPsLTDS1 = txtNutPecahPsLTDS1.Text
        strKernelBulatGrLTDS1 = txtKernelBulatGrLTDS1.Text
        strKernelBulatPsLTDS1 = txtKernelBulatPsLTDS1.Text
        strKernelPecahGrLTDS1 = txtKernelPecahGrLTDS1.Text
        strKernelPecahPsLTDS1 = txtKernelPecahPsLTDS1.Text
        strKernelRugiGrLTDS1 = txtKernelRugiGrLTDS1.Text
        strKernelRugiPsLTDS1 = txtKernelRugiPsLTDS1.Text
        strSampelGrLTDS2 = txtSampelGrLTDS2.Text
        strSampelPsLTDS2 = txtSampelPsLTDS2.Text
        strNutUtuhGrLTDS2 = txtNutUtuhGrLTDS2.Text
        strNutUtuhPsLTDS2 = txtNutUtuhPsLTDS2.Text
        strNutPecahGrLTDS2 = txtNutPecahGrLTDS2.Text
        strNutPecahPsLTDS2 = txtNutPecahPsLTDS2.Text
        strKernelBulatGrLTDS2 = txtKernelBulatGrLTDS2.Text
        strKernelBulatPsLTDS2 = txtKernelBulatPsLTDS2.Text
        strKernelPecahGrLTDS2 = txtKernelPecahGrLTDS2.Text
        strKernelPecahPsLTDS2 = txtKernelPecahPsLTDS2.Text
        strKernelRugiGrLTDS2 = txtKernelRugiGrLTDS2.Text
        strKernelRugiPsLTDS2 = txtKernelRugiPsLTDS2.Text
        strSampelGrCB = txtSampelGrCB.Text
        strSampelPsCB = txtSampelPsCB.Text
        strNutUtuhGrCB = txtNutUtuhGrCB.Text
        strNutUtuhPsCB = txtNutUtuhPsCB.Text
        strNutPecahGrCB = txtNutPecahGrCB.Text
        strNutPecahPsCB = txtNutPecahPsCB.Text
        strKernelBulatGrCB = txtKernelBulatGrCB.Text
        strKernelBulatPsCB = txtKernelBulatPsCB.Text
        strKernelPecahGrCB = txtKernelPecahGrCB.Text
        strKernelPecahPsCB = txtKernelPecahPsCB.Text
        strKernelRugiGrCB = txtKernelRugiGrCB.Text
        strKernelRugiPsCB = txtKernelRugiPsCB.Text
        strSampelGrPC1 = txtSampelGrPC1.Text
        strSampelPsPC1 = txtSampelPsPC1.Text
        strNutUtuhGrPC1 = txtNutUtuhGrPC1.Text
        strNutUtuhPsPC1 = txtNutUtuhPsPC1.Text
        strNutPecahGrPC1 = txtNutPecahGrPC1.Text
        strNutPecahPsPC1 = txtNutPecahPsPC1.Text
        strKernelBulatGrPC1 = txtKernelBulatGrPC1.Text
        strKernelBulatPsPC1 = txtKernelBulatPsPC1.Text
        strKernelPecahGrPC1 = txtKernelPecahGrPC1.Text
        strKernelPecahPsPC1 = txtKernelPecahPsPC1.Text
        strCangkangGrPC1 = txtCangkangGrPC1.Text
        strCangkangPsPC1 = txtCangkangPsPC1.Text
        strPecahPerTotalGrPC1 = txtPecahPerTotalGrPC1.Text
        strPecahPerTotalPsPC1 = txtPecahPerTotalPsPC1.Text
        strTotalPerSampelGrPC1 = txtTotalPerSampelGrPC1.Text
        strTotalPerSampelPsPC1 = txtTotalPerSampelPsPC1.Text
        strSampelGrPC2 = txtSampelGrPC2.Text
        strSampelPsPC2 = txtSampelPsPC2.Text
        strNutUtuhGrPC2 = txtNutUtuhGrPC2.Text
        strNutUtuhPsPC2 = txtNutUtuhPsPC2.Text
        strNutPecahGrPC2 = txtNutPecahGrPC2.Text
        strNutPecahPsPC2 = txtNutPecahPsPC2.Text
        strKernelBulatGrPC2 = txtKernelBulatGrPC2.Text
        strKernelBulatPsPC2 = txtKernelBulatPsPC2.Text
        strKernelPecahGrPC2 = txtKernelPecahGrPC2.Text
        strKernelPecahPsPC2 = txtKernelPecahPsPC2.Text
        strCangkangGrPC2 = txtCangkangGrPC2.Text
        strCangkangPsPC2 = txtCangkangPsPC2.Text
        strPecahPerTotalGrPC2 = txtPecahPerTotalGrPC2.Text
        strPecahPerTotalPsPC2 = txtPecahPerTotalPsPC2.Text
        strTotalPerSampelGrPC2 = txtTotalPerSampelGrPC2.Text
        strTotalPerSampelPsPC2 = txtTotalPerSampelPsPC2.Text
        strSampelGrPC3 = txtSampelGrPC3.Text
        strSampelPsPC3 = txtSampelPsPC3.Text
        strNutUtuhGrPC3 = txtNutUtuhGrPC3.Text
        strNutUtuhPsPC3 = txtNutUtuhPsPC3.Text
        strNutPecahGrPC3 = txtNutPecahGrPC3.Text
        strNutPecahPsPC3 = txtNutPecahPsPC3.Text
        strKernelBulatGrPC3 = txtKernelBulatGrPC3.Text
        strKernelBulatPsPC3 = txtKernelBulatPsPC3.Text
        strKernelPecahGrPC3 = txtKernelPecahGrPC3.Text
        strKernelPecahPsPC3 = txtKernelPecahPsPC3.Text
        strCangkangGrPC3 = txtCangkangGrPC3.Text
        strCangkangPsPC3 = txtCangkangPsPC3.Text
        strPecahPerTotalGrPC3 = txtPecahPerTotalGrPC3.Text
        strPecahPerTotalPsPC3 = txtPecahPerTotalPsPC3.Text
        strTotalPerSampelGrPC3 = txtTotalPerSampelGrPC3.Text
        strTotalPerSampelPsPC3 = txtTotalPerSampelPsPC3.Text
        strSampelGrPC4 = txtSampelGrPC4.Text
        strSampelPsPC4 = txtSampelPsPC4.Text
        strNutUtuhGrPC4 = txtNutUtuhGrPC4.Text
        strNutUtuhPsPC4 = txtNutUtuhPsPC4.Text
        strNutPecahGrPC4 = txtNutPecahGrPC4.Text
        strNutPecahPsPC4 = txtNutPecahPsPC4.Text
        strKernelBulatGrPC4 = txtKernelBulatGrPC4.Text
        strKernelBulatPsPC4 = txtKernelBulatPsPC4.Text
        strKernelPecahGrPC4 = txtKernelPecahGrPC4.Text
        strKernelPecahPsPC4 = txtKernelPecahPsPC4.Text
        strCangkangGrPC4 = txtCangkangGrPC4.Text
        strCangkangPsPC4 = txtCangkangPsPC4.Text
        strPecahPerTotalGrPC4 = txtPecahPerTotalGrPC4.Text
        strPecahPerTotalPsPC4 = txtPecahPerTotalPsPC4.Text
        strTotalPerSampelGrPC4 = txtTotalPerSampelGrPC4.Text
        strTotalPerSampelPsPC4 = txtTotalPerSampelPsPC4.Text
        strSampelGrRM1 = txtSampelGrRM1.Text
        strSampelPsRM1 = txtSampelPsRM1.Text
        strNutUtuhGrRM1 = txtNutUtuhGrRM1.Text
        strNutUtuhPsRM1 = txtNutUtuhPsRM1.Text
        strNutPecahGrRM1 = txtNutPecahGrRM1.Text
        strNutPecahPsRM1 = txtNutPecahPsRM1.Text
        strKernelBulatGrRM1 = txtKernelBulatGrRM1.Text
        strKernelBulatPsRM1 = txtKernelBulatPsRM1.Text
        strKernelPecahGrRM1 = txtKernelPecahGrRM1.Text
        strKernelPecahPsRM1 = txtKernelPecahPsRM1.Text
        strCangkangGrRM1 = txtCangkangGrRM1.Text
        strCangkangPsRM1 = txtCangkangPsRM1.Text
        strEffisiensiGrRM1 = txtEffisiensiGrRM1.Text
        strEffisiensiPsRM1 = txtEffisiensiPsRM1.Text
        strSampelGrRM2 = txtSampelGrRM2.Text
        strSampelPsRM2 = txtSampelPsRM2.Text
        strNutUtuhGrRM2 = txtNutUtuhGrRM2.Text
        strNutUtuhPsRM2 = txtNutUtuhPsRM2.Text
        strNutPecahGrRM2 = txtNutPecahGrRM2.Text
        strNutPecahPsRM2 = txtNutPecahPsRM2.Text
        strKernelBulatGrRM2 = txtKernelBulatGrRM2.Text
        strKernelBulatPsRM2 = txtKernelBulatPsRM2.Text
        strKernelPecahGrRM2 = txtKernelPecahGrRM2.Text
        strKernelPecahPsRM2 = txtKernelPecahPsRM2.Text
        strCangkangGrRM2 = txtCangkangGrRM2.Text
        strCangkangPsRM2 = txtCangkangPsRM2.Text
        strEffisiensiGrRM2 = txtEffisiensiGrRM2.Text
        strEffisiensiPsRM2 = txtEffisiensiPsRM2.Text
        strSampelGrRM3 = txtSampelGrRM3.Text
        strSampelPsRM3 = txtSampelPsRM3.Text
        strNutUtuhGrRM3 = txtNutUtuhGrRM3.Text
        strNutUtuhPsRM3 = txtNutUtuhPsRM3.Text
        strNutPecahGrRM3 = txtNutPecahGrRM3.Text
        strNutPecahPsRM3 = txtNutPecahPsRM3.Text
        strKernelBulatGrRM3 = txtKernelBulatGrRM3.Text
        strKernelBulatPsRM3 = txtKernelBulatPsRM3.Text
        strKernelPecahGrRM3 = txtKernelPecahGrRM3.Text
        strKernelPecahPsRM3 = txtKernelPecahPsRM3.Text
        strCangkangGrRM3 = txtCangkangGrRM3.Text
        strCangkangPsRM3 = txtCangkangPsRM3.Text
        strEffisiensiGrRM3 = txtEffisiensiGrRM3.Text
        strEffisiensiPsRM3 = txtEffisiensiPsRM3.Text

        If strEdit = "True" Then
            blnUpd = True
        Else
            blnUpd = False
            strParam1 = "||TransDate||" & _
                      strDate & "||" & strProcessingLnNo

            Try
                intErrNo = objPMTrx.mtdGetKernelLosses(strOppCd_KernelLosses_GET, strLocation, strParam1, objDataSet1)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_KernelLoss_GET&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/PM_trx_KernelLoss_List.aspx")
            End Try

            If objDataSet1.Tables(0).Rows.Count > 0 Then
                blnDupKey = True
                lblDupMsg.Visible = True
                Exit Sub
            Else
                blnDupKey = False
            End If
        End If

        strStatus = IIf(lblStatus.Text = objPMTrx.mtdGetKernelLossStatus(objPMTrx.EnumKernelLossStatus.Deleted), _
                        objPMTrx.EnumKernelLossStatus.Deleted, _
                        objPMTrx.EnumKernelLossStatus.Active)

        strParam = strDate & "|" & _
                    strProcessingLnNo & "|" & _
                    strSampelGrLTDS1 & "|" & _
                    strSampelPsLTDS1 & "|" & _
                    strNutUtuhGrLTDS1 & "|" & _
                    strNutUtuhPsLTDS1 & "|" & _
                    strNutPecahGrLTDS1 & "|" & _
                    strNutPecahPsLTDS1 & "|" & _
                    strKernelBulatGrLTDS1 & "|" & _
                    strKernelBulatPsLTDS1 & "|" & _
                    strKernelPecahGrLTDS1 & "|" & _
                    strKernelPecahPsLTDS1 & "|" & _
                    strKernelRugiGrLTDS1 & "|" & _
                    strKernelRugiPsLTDS1 & "|" & _
                    strSampelGrLTDS2 & "|" & _
                    strSampelPsLTDS2 & "|" & _
                    strNutUtuhGrLTDS2 & "|" & _
                    strNutUtuhPsLTDS2 & "|" & _
                    strNutPecahGrLTDS2 & "|" & _
                    strNutPecahPsLTDS2 & "|" & _
                    strKernelBulatGrLTDS2 & "|" & _
                    strKernelBulatPsLTDS2 & "|" & _
                    strKernelPecahGrLTDS2 & "|" & _
                    strKernelPecahPsLTDS2 & "|" & _
                    strKernelRugiGrLTDS2 & "|" & _
                    strKernelRugiPsLTDS2 & "|" & _
                    strSampelGrCB & "|" & _
                    strSampelPsCB & "|" & _
                    strNutUtuhGrCB & "|" & _
                    strNutUtuhPsCB & "|" & _
                    strNutPecahGrCB & "|" & _
                    strNutPecahPsCB & "|" & _
                    strKernelBulatGrCB & "|" & _
                    strKernelBulatPsCB & "|" & _
                    strKernelPecahGrCB & "|" & _
                    strKernelPecahPsCB & "|" & _
                    strKernelRugiGrCB & "|" & _
                    strKernelRugiPsCB & "|" & _
                    strSampelGrPC1 & "|" & _
                    strSampelPsPC1 & "|" & _
                    strNutUtuhGrPC1 & "|" & _
                    strNutUtuhPsPC1 & "|" & _
                    strNutPecahGrPC1 & "|" & _
                    strNutPecahPsPC1 & "|" & _
                    strKernelBulatGrPC1 & "|" & _
                    strKernelBulatPsPC1 & "|" & _
                    strKernelPecahGrPC1 & "|" & _
                    strKernelPecahPsPC1 & "|" & _
                    strCangkangGrPC1 & "|" & _
                    strCangkangPsPC1 & "|" & _
                    strPecahPerTotalGrPC1 & "|" & _
                    strPecahPerTotalPsPC1 & "|" & _
                    strTotalPerSampelGrPC1 & "|" & _
                    strTotalPerSampelPsPC1 & "|" & _
                    strSampelGrPC2 & "|" & _
                    strSampelPsPC2 & "|" & _
                    strNutUtuhGrPC2 & "|" & _
                    strNutUtuhPsPC2 & "|" & _
                    strNutPecahGrPC2 & "|" & _
                    strNutPecahPsPC2 & "|" & _
                    strKernelBulatGrPC2 & "|" & _
                    strKernelBulatPsPC2 & "|" & _
                    strKernelPecahGrPC2 & "|" & _
                    strKernelPecahPsPC2 & "|" & _
                    strCangkangGrPC2 & "|" & _
                    strCangkangPsPC2 & "|" & _
                    strPecahPerTotalGrPC2 & "|" & _
                    strPecahPerTotalPsPC2 & "|" & _
                    strTotalPerSampelGrPC2 & "|" & _
                    strTotalPerSampelPsPC2 & "|" & _
                    strSampelGrPC3 & "|" & _
                    strSampelPsPC3 & "|" & _
                    strNutUtuhGrPC3 & "|" & _
                    strNutUtuhPsPC3 & "|" & _
                    strNutPecahGrPC3 & "|" & _
                    strNutPecahPsPC3 & "|" & _
                    strKernelBulatGrPC3 & "|" & _
                    strKernelBulatPsPC3 & "|" & _
                    strKernelPecahGrPC3 & "|" & _
                    strKernelPecahPsPC3 & "|" & _
                    strCangkangGrPC3 & "|" & _
                    strCangkangPsPC3 & "|" & _
                    strPecahPerTotalGrPC3 & "|" & _
                    strPecahPerTotalPsPC3 & "|" & _
                    strTotalPerSampelGrPC3 & "|" & _
                    strTotalPerSampelPsPC3 & "|" & _
                    strSampelGrPC4 & "|" & _
                    strSampelPsPC4 & "|" & _
                    strNutUtuhGrPC4 & "|" & _
                    strNutUtuhPsPC4 & "|" & _
                    strNutPecahGrPC4 & "|" & _
                    strNutPecahPsPC4 & "|" & _
                    strKernelBulatGrPC4 & "|" & _
                    strKernelBulatPsPC4 & "|" & _
                    strKernelPecahGrPC4 & "|" & _
                    strKernelPecahPsPC4 & "|" & _
                    strCangkangGrPC4 & "|" & _
                    strCangkangPsPC4 & "|" & _
                    strPecahPerTotalGrPC4 & "|" & _
                    strPecahPerTotalPsPC4 & "|" & _
                    strTotalPerSampelGrPC4 & "|" & _
                    strTotalPerSampelPsPC4 & "|" & _
                    strSampelGrRM1 & "|" & _
                    strSampelPsRM1 & "|" & _
                    strNutUtuhGrRM1 & "|" & _
                    strNutUtuhPsRM1 & "|" & _
                    strNutPecahGrRM1 & "|" & _
                    strNutPecahPsRM1 & "|" & _
                    strKernelBulatGrRM1 & "|" & _
                    strKernelBulatPsRM1 & "|" & _
                    strKernelPecahGrRM1 & "|" & _
                    strKernelPecahPsRM1 & "|" & _
                    strCangkangGrRM1 & "|" & _
                    strCangkangPsRM1 & "|" & _
                    strEffisiensiGrRM1 & "|" & _
                    strEffisiensiPsRM1 & "|" & _
                    strSampelGrRM2 & "|" & _
                    strSampelPsRM2 & "|" & _
                    strNutUtuhGrRM2 & "|" & _
                    strNutUtuhPsRM2 & "|" & _
                    strNutPecahGrRM2 & "|" & _
                    strNutPecahPsRM2 & "|" & _
                    strKernelBulatGrRM2 & "|" & _
                    strKernelBulatPsRM2 & "|" & _
                    strKernelPecahGrRM2 & "|" & _
                    strKernelPecahPsRM2 & "|" & _
                    strCangkangGrRM2 & "|" & _
                    strCangkangPsRM2 & "|" & _
                    strEffisiensiGrRM2 & "|" & _
                    strEffisiensiPsRM2 & "|" & _
                    strSampelGrRM3 & "|" & _
                    strSampelPsRM3 & "|" & _
                    strNutUtuhGrRM3 & "|" & _
                    strNutUtuhPsRM3 & "|" & _
                    strNutPecahGrRM3 & "|" & _
                    strNutPecahPsRM3 & "|" & _
                    strKernelBulatGrRM3 & "|" & _
                    strKernelBulatPsRM3 & "|" & _
                    strKernelPecahGrRM3 & "|" & _
                    strKernelPecahPsRM3 & "|" & _
                    strCangkangGrRM3 & "|" & _
                    strCangkangPsRM3 & "|" & _
                    strEffisiensiGrRM3 & "|" & _
                    strEffisiensiPsRM3 & "|" & _
                    strStatus

        Try
            intErrNo = objPMTrx.mtdUpdKernelLosses(strOppCd_KernelLosses_ADD, _
                                                    strOppCd_KernelLosses_UPD, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strAccMonth, _
                                                    strAccYear, _
                                                    strParam, _
                                                    blnUpd)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_OIL_LOSS_UPDATE&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/pm_trx_KernelLoss_list.aspx")
        End Try

        Select Case blnUpd
            Case True
                Response.Redirect("PM_trx_KernelLoss_Det.aspx?LocCode=" & strLocation & "&TransDate=" & strTransDate & "&Edit=True")
            Case False
                Response.Redirect("PM_trx_KernelLoss_List.aspx")
        End Select

    End Sub

    Protected Function CheckDate() As String
        Dim strDateSetting As String
        Dim objSysCfgDs As DataSet
        Dim objDateFormat As String
        Dim strValidDate As String
        If Not txtdate.Text = "" Then
            If objGlobal.mtdValidInputDate(strDateFormat, txtdate.Text, objDateFormat, strValidDate) = True Then
                Return strValidDate
            Else
                lblFmt.Text = objDateFormat & "."
                lblDate.Visible = True
                lblFmt.Visible = True

            End If
        End If

    End Function

    Sub btnSave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        UpdateData("Save")
    End Sub

    Sub btnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOppCd_DEL As String = "PM_CLSTRX_KERNEL_LOSSES_DEL"
        Dim strDate As String = CheckDate()
        Dim strParam As String

        strParam = strLocation & "|" & strDate & "|" & ddlProcessingLnNo.SelectedItem.Text.Trim
        Try
            intErrNo = objPMTrx.mtdPhysicalDelete(strOppCd_DEL, _
                                                  strParam, _
                                                  objPMTrx.EnumTransType.KernelLosses)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_KERNEL_LOSSES_DEL&errmesg=" & lblErrMessage.Text & "&redirect=PM/Trx/PM_trx_KernelLoss_List.aspx")
        End Try

        Response.Redirect("PM_trx_KernelLoss_List.aspx")
    End Sub

    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PM_trx_KernelLoss_List.aspx")
    End Sub

    Sub BindProcessingLineList()
        Dim strOpCode = "PM_CLSSETUP_PROCESSINGLINE_GET"
        Dim strParam = "ORDER BY PL.ProcessingLnNo| AND PL.LocCode='" & Session("SS_LOCATION") & "'"
        Dim intCnt As Integer

        objPMSetup.mtdGetProcessingLine(strOpCode, strParam, objProcessingLine)
        ddlProcessingLnNo.Items.Add(New ListItem("Please select Processing Line No.", ""))
        For intCnt = 0 To objProcessingLine.Tables(0).Rows.Count - 1
            objProcessingLine.Tables(0).Rows(intCnt).Item("ProcessingLnNo") = Trim(objProcessingLine.Tables(0).Rows(intCnt).Item("ProcessingLnNo"))
            ddlProcessingLnNo.Items.Add(New ListItem(objProcessingLine.Tables(0).Rows(intCnt).Item("ProcessingLnNo")))
        Next
    End Sub


End Class
