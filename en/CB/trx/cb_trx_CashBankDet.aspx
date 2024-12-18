<%@ Page Language="vb" CodeFile="../../../include/cb_trx_CashBankDet.aspx.vb" Inherits="cb_trx_CashBankDet" %>

<%@ Register TagPrefix="UserControl" TagName="MenuCB" Src="../../menu/menu_cbtrx.ascx" %>
<%@ Register TagPrefix="Preference" TagName="PrefHdl" Src="../../include/preference/preference_handler.ascx" %>
<%@ Register TagPrefix="qsf" Namespace="Telerik.QuickStart" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<html>
<head>
    <title>Cash Bank Details</title>
    <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
    <Preference:PrefHdl ID="PrefHdl" runat="server" />
    <style type="text/css">
        body
        {
            font-family: "segoe ui" ,arial,sans-serif;
            font-size: 12px;
        }
        
        a img
        {
            border: 0;
        }
    </style>
    <script language="javascript">

        function calTaxPriceCR() {
            var doc = document.frmMain;
            var a = parseFloat(doc.txtDPPAmountCR.value);
            var b = parseFloat(doc.hidTaxObjectRate.value);
            var c = (a * (b / 100));
            var d = (a * (10 / 100));
            var newnumber = new Number(c + '').toFixed(parseInt(0));
            var newnumberPPN = new Number(d + '').toFixed(parseInt(0));

            if (doc.hidTaxPPN == '0')
                doc.txtCRTotalAmount.value = newnumber;
            else
                doc.txtCRTotalAmount.value = newnumberPPN;

            if (doc.txtCRTotalAmount.value == 'NaN')
                doc.txtCRTotalAmount.value = '';
            else
                doc.txtCRTotalAmount.value = doc.txtCRTotalAmount.value;
        }

        function calTaxPriceDR() {
            var doc = document.frmMain;
            var a = parseFloat(doc.txtDPPAmountDR.value);
            var b = parseFloat(doc.hidTaxObjectRate.value);
            var c = (a * (b / 100));
            var d = (a * (10 / 100));
            var newnumber = new Number(c + '').toFixed(parseInt(0));
            var newnumberPPN = new Number(d + '').toFixed(parseInt(0));

            if (doc.hidTaxPPN.value == '0')
                doc.txtDRTotalAmount.value = newnumber;
            else
                doc.txtDRTotalAmount.value = newnumberPPN;

            if (doc.txtDRTotalAmount.value == 'NaN')
                doc.txtDRTotalAmount.value = '';
            else
                doc.txtDRTotalAmount.value = doc.txtDRTotalAmount.value;
        }
        function gotFocusDPPCR() {
            var doc = document.frmMain;
            doc.txtDPPAmountDR.value = '';
            doc.txtDRTotalAmount.value = '';
        }
        function gotFocusDPPDR() {
            var doc = document.frmMain;
            doc.txtDPPAmountCR.value = '';
            doc.txtCRTotalAmount.value = '';
        }

        (function (global, undefined) {
            var demo = {};

            function populateValue() {
                $get(demo.label).innerHTML = $get(demo.textBox).value;
                //the RadWindow's content template is an INaming container and the server code block is needed
                $find(demo.contentTemplateID).close();
            }

            function openWinContentTemplate() {
                $find(demo.templateWindowID).show();
            }
            function openWinNavigateUrl() {
                $find(demo.urlWindowID).show();
            }

            global.$windowContentDemo = demo;
            global.populateValue = populateValue;
            global.openWinContentTemplate = openWinContentTemplate;
            global.openWinNavigateUrl = openWinNavigateUrl;
        })(window);

    </script>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style3
        {
            width: 12%;
        }
        .style4
        {
            height: 1px;
        }
        .style5
        {
            width: 12%;
            height: 1px;
        }
        .style6
        {
            width: 166px;
            height: 25px;
        }
        .style7
        {
            height: 25px;
        }
    </style>
</head>
<body>
    <form id="frmMain" class="main-modul-bg-app-list-pu" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Fieldset" />
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" BackgroundPosition="None" />
    <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
        <tr>
            <td style="width: 100%; height: 1500px" valign="top">
                <div class="kontenlist">
                    <table id="tblHeader" cellspacing="0" cellpadding="2" width="100%" border="0" class="font9Tahoma">
                        <tr>
                            <td colspan="5">
                                <UserControl:MenuCB ID="MenuCB" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <table cellpadding="0" cellspacing="0" class="style1" class="font9Tahoma">
                                    <tr class="font9Tahoma">
                                        <td class="font9Tahoma" style="text-align: left">
                                            <strong>CASH BANK DETAILS</strong>
                                        </td>
                                        <td class="font9Header" style="text-align: right">
                                            Period :
                                            <asp:Label ID="lblAccPeriod" runat="server" />&nbsp;|Status :
                                            <asp:Label ID="lblStatus" runat="server" />
                                            | Last Update :
                                            <asp:Label ID="lblLastUpdate" runat="server" />| Print Date :
                                            <asp:Label ID="lblPrintDate" runat="server" />&nbsp;| Cheque Print Date :
                                            <asp:Label ID="lblChequePrintDate" runat="server" />&nbsp;| Date Created :
                                            <asp:Label ID="lblDateCreated" runat="server" />
                                            |
                                            <asp:Label ID="lblTaxStatus" Text="Tax Status :" runat="server" /><asp:Label ID="lblTaxStatusDesc"
                                                runat="server" />&nbsp;|
                                            <asp:Label ID="lblTaxUpdate" Text="Tax Updated By :" runat="server" /><asp:Label
                                                ID="lblTaxUpdateDesc" runat="server" />&nbsp;|
                                            <asp:Label ID="lblSKBStartDate" runat="server" Visible="False"></asp:Label>&nbsp;:
                                            <asp:Label ID="LblIsSKBActive" runat="server" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <hr style="width: 100%" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td height="20" width="20%">
                                <asp:Label ID="lblPaymentIDTag" Text="Payment ID :" runat="server" />
                            </td>
                            <td width="40%">
                                <asp:Label ID="lblPaymentID" runat="server" />
                            </td>
                            <td width="5%">
                                &nbsp;
                            </td>
                            <td class="style3">
                                &nbsp;
                            </td>
                            <td width="20%">
                                &nbsp;
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td height="25" width="20%">
                                Transaction Date :
                            </td>
                            <td>
                                <asp:TextBox ID="txtDateCreated" CssClass="font9Tahoma" Width="25%" MaxLength="10"
                                    runat="server" />
                                <a href="javascript:PopCal('txtDateCreated');">
                                    <asp:Image ID="btnDateCreated" ImageAlign="AbsMiddle" runat="server" ImageUrl="../../Images/calendar.gif" /></a>
                                <asp:ImageButton ImageAlign="AbsBottom" ID="GetSaldoBankBtn" OnClick="GetSaldoBankBtn_Click"
                                    CausesValidation="False" ImageUrl="../../images/icn_next.gif" AlternateText="Get Saldo Bank"
                                    ToolTip="Get Saldo Bank" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDateCreated"
                                    Text="Please enter Date Created" Display="dynamic" />
                                <asp:Label ID="lblDate" Text="<br>Date Entered should be in the format " ForeColor="red"
                                    Visible="false" runat="server" />
                                <asp:Label ID="lblFmt" ForeColor="red" Visible="false" runat="server" />
                            </td>
                            <td width="5%">
                                &nbsp;
                            </td>
                            <td class="style3">
                                &nbsp;
                            </td>
                            <td width="15%">
                                &nbsp;
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td height="25" width="20%">
                                Cash Bank Type* :
                            </td>
                            <td width="40%">
                                <asp:RadioButtonList ID="rblCashBankType" TextAlign="Right" RepeatColumns="3" RepeatLayout="Flow"
                                    AutoPostBack="true" OnSelectedIndexChanged="CashBankType_Change" runat="server" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td class="style3">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td height="25" width="20%">
                                <asp:Label ID="lblPayTypeTag" Text="Payment Type :*" runat="server" />
                            </td>
                            <td>
                                <asp:DropDownList Width="95%" ID="ddlPayType" CssClass="font9Tahoma" AutoPostBack="true"
                                    runat="server" OnSelectedIndexChanged="onSelect_PayType">
                                    <asp:ListItem Value="0">Cheque</asp:ListItem>
                                    <asp:ListItem Value="1" Selected="True">Cash</asp:ListItem>
                                    <asp:ListItem Value="3">Bilyet Giro</asp:ListItem>
                                    <asp:ListItem Value="4">Others</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="lblErrPayType" ForeColor="red" Visible="false" Text="Please select Payment Type"
                                    runat="server" />&nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td class="style3">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                Bilyet Giro Date :
                            </td>
                            <td>
                                <asp:TextBox ID="txtGiroDate" Width="25%" CssClass="font9Tahoma" MaxLength="10" runat="server" />
                                <a href="javascript:PopCal('txtGiroDate');">
                                    <asp:Image ID="btnGiroDate" ImageAlign="AbsMiddle" runat="server" ImageUrl="../../Images/calendar.gif" /></a>
                                <br />
                                <asp:Label ID="lblDateGiro" Text="Date Entered should be in the format" ForeColor="Red"
                                    Visible="False" runat="server" />
                                <asp:Label ID="lblFmtGiro" ForeColor="red" Visible="false" runat="server" />
                                <br />
                            </td>
                            <td>
                            </td>
                            <td class="style3">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td height="25" width="20%">
                                <asp:Label ID="lblBankFrom" Text="Bank From :" runat="server" />
                            </td>
                            <td>
                                <asp:DropDownList Width="95%" CssClass="font9Tahoma" ID="ddlBank" AutoPostBack="true"
                                    OnSelectedIndexChanged="onSelect_Bank" runat="server" />
                                <asp:Label ID="lblErrBank" ForeColor="red" Visible="false" Text="Please select Bank Code"
                                    runat="server" />&nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td class="style3">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <!--
			<TR>
				<TD height=25 width="20%">Cheque/Bilyet Giro No. :</TD>
                <td><asp:Textbox id=txtChequeNo width="95%" maxlength=32 runat=server /><asp:Label id=lblErrCheque forecolor=red visible=false text="Please enter Cheque/Bilyet Giro No." runat=server/>&nbsp;</td>
				<td>&nbsp;</td>
				<TD>Updated By :</TD>
				<td><asp:Label ID=lblUpdatedBy runat=server /></TD>
			</TR>
			-->
                        <tr>
                            <td width="20%" class="style4">
                                <asp:Label ID="lblPayToTag" Text="Payment To :*" runat="server" />
                            </td>
                            <td class="style4">
                                <asp:TextBox ID="txtPaymentTo" CssClass="font9Tahoma" Width="80%" MaxLength="100"
                                    AutoPostBack="false" runat="server" />
                                <input type="button" value=" ... " id="FindSpl" onclick="javascript:PopSupplier_New('frmMain','','txtSupCode','txtPaymentTo','txtCreditTerm','txtPPN','txtPPNInit', 'False');"
                                    causesvalidation="False" class="button-small" runat="server" />
                                <asp:ImageButton ImageAlign="AbsBottom" ID="ImageButton1" OnClick="GetDataBtn_Click"
                                    CausesValidation="False" ImageUrl="../../images/icn_next.gif" AlternateText="Get Data"
                                    ToolTip="Click For View Data" runat="server" />
                                <asp:TextBox ID="txtSupCode" CssClass="font9Tahoma" runat="server" BackColor="Transparent"
                                    BorderStyle="None" Width="32%" ForeColor="Transparent"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPaymentTo" runat="server" ErrorMessage="Please fill required field"
                                    ControlToValidate="txtPaymentTo" Display="dynamic" />
                                <td class="style4">
                                </td>
                                <td class="style5">
                                </td>
                        </tr>
                        <tr>
                            <td height="25" width="20%">
                                Bank Account No. :
                            </td>
                            <td>
                                <asp:DropDownList Width="95%" ID="ddlSplBankAccNo" CssClass="font9Tahoma" AutoPostBack="true"
                                    OnSelectedIndexChanged="onSelect_SplBankAccNo" runat="server" />
                                <asp:TextBox ID="txtSplBankAccNo" Visible="false" Width="0%" MaxLength="32" CssClass="font9Tahoma"
                                    runat="server" />
                                <asp:ImageButton ImageAlign="AbsBottom" ID="btnGet" Visible="false" OnClick="GetSupplierBtn_Click"
                                    CausesValidation="False" ImageUrl="../../images/icn_next.gif" AlternateText="Get Data"
                                    runat="server" />
                                <asp:Label ID="lblErrBankAccNo" ForeColor="red" Visible="false" Text="Please enter Bank Account No."
                                    runat="server" />&nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td class="style3">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td height="25" width="20%">
                                <asp:Label ID="lblBankTo" Text="Bank To :" runat="server" />
                            </td>
                            <td>
                                <asp:DropDownList Width="95%" ID="ddlBankTo" AutoPostBack="true" OnSelectedIndexChanged="onSelect_Bank"
                                    CssClass="font9Tahoma" runat="server" />
                                <asp:Label ID="lblErrBankTo" ForeColor="red" Visible="false" Text="Please select Bank Code"
                                    runat="server" />&nbsp; &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td class="style3">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td height="25">
                                Print Cheque/Bilyet Giro as Cash :
                            </td>
                            <td>
                                <asp:CheckBox ID="chkChequeCash" Text="" runat="server" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td class="style3">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td height="25">
                                Cheque/Bilyet Giro Handover :
                            </td>
                            <td>
                                <asp:DropDownList Width="95%" ID="ddlChequeHandOver" AutoPostBack="false" CssClass="font9Tahoma"
                                    runat="server">
                                    <asp:ListItem Value="0" Selected>N/A</asp:ListItem>
                                    <asp:ListItem Value="1">Pick-up</asp:ListItem>
                                    <asp:ListItem Value="2">Bank Transfer</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td class="style3">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" style="height: 23px">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <table id="tblSelection" border="0" width="100%" cellspacing="0" cellpadding="2"
                                    runat="server" class="sub-Add">
                                    <tr class="mb-c">
                                        <td height="25">
                                            Reference No/Staff Advance :
                                        </td>
                                        <td colspan="4">
                                            <telerik:RadComboBox CssClass="fontObject" ID="ddlRefNo" OnSelectedIndexChanged="OnSelect_RefNo"
                                                runat="server" AllowCustomText="True" EmptyMessage="Reference No/Staff Advance "
                                                Height="200" Width="95%" ExpandDelay="50" Filter="Contains" Sort="Ascending"
                                                EnableVirtualScrolling="True">
                                                <CollapseAnimation Type="InQuart" />
                                            </telerik:RadComboBox>
                                            <asp:ImageButton ImageAlign="AbsBottom" ID="btnGetRef" OnClick="GetRefNoBtn_Click"
                                                CausesValidation="False" ImageUrl="../../images/icn_next.gif" AlternateText="Get Data"
                                                runat="server" />
                                            <asp:Label ID="lblRefNoErr" Visible="False" ForeColor="red" runat="server" />
                                        </td>
                                    </tr>
                                    <tr id="RowChargeTo" class="font9Tahoma" hidden>
                                        <td height="25">
                                            Charge To :*
                                        </td>
                                        <td colspan="4">
                                            <asp:DropDownList ID="ddlLocation" Enabled="false" Width="95%" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlLocation_OnSelectedIndexChanged" CssClass="font9Tahoma"
                                                runat="server" />
                                            <asp:Label ID="lblLocationErr" Visible="False" ForeColor="red" runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="mb-c">
                                        <td style="height: 52px">
                                            <asp:Label ID="lblAccCodeTag" runat="server" />
                                        </td>
                                        <td colspan="4">
                                            <telerik:RadComboBox CssClass="fontObject" ID="radcmbCOA" AutoPostBack="true" OnSelectedIndexChanged="CallCheckVehicleUse"
                                                runat="server" AllowCustomText="True" EmptyMessage="Please Select COA" Height="200"
                                                Width="95%" ExpandDelay="50" Filter="Contains" Sort="Ascending" EnableVirtualScrolling="True">
                                                <CollapseAnimation Type="InQuart" />
                                            </telerik:RadComboBox>
                                            <asp:Label ID="lblAccCodeErr" Visible="False" ForeColor="red" Text="Please select one Account Code"
                                                runat="server" />
                                        </td>
                                    </tr>
                                    <tr id="RowChargeLevel" class="mb-c">
                                        <td height="25">
                                            Charge Level :*
                                        </td>
                                        <td colspan="4">
                                            <asp:DropDownList ID="ddlChargeLevel" CssClass="font9Tahoma" Width="95%" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlChargeLevel_OnSelectedIndexChanged" runat="server" />
                                        </td>
                                    </tr>
                                    <tr id="RowPreBlk" class="mb-c">
                                        <td height="25">
                                            <asp:Label ID="lblPreBlkTag" runat="server" />
                                        </td>
                                        <td colspan="4">
                           
                                            <telerik:RadComboBox CssClass="fontObject" ID="radlstPreBlok"
                                                runat="server" AllowCustomText="True" EmptyMessage="Please Select"
                                                Height="200" Width="95%" ExpandDelay="50" Filter="Contains" Sort="Ascending"
                                                EnableVirtualScrolling="True">
                                                <CollapseAnimation Type="InQuart" />
                                            </telerik:RadComboBox>
                                            <asp:Label ID="lblPreBlockErr" Visible="False" ForeColor="red" runat="server" />
                                        </td>
                                    </tr>
                                    <tr id="RowBlk" class="mb-c">
                                        <td height="25">
                                            <asp:Label ID="lblBlkTag" runat="server" />
                                        </td>
                                        <td colspan="4">
                                            
                                            <telerik:RadComboBox CssClass="fontObject" ID="radlstBlock"
                                                runat="server" AllowCustomText="True" EmptyMessage="Please Select"
                                                Height="200" Width="95%" ExpandDelay="50" Filter="Contains" Sort="Ascending"
                                                EnableVirtualScrolling="True">
                                                <CollapseAnimation Type="InQuart" />
                                            </telerik:RadComboBox>
                                            <asp:Label ID="lblBlockErr" Visible="False" ForeColor="red" runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="mb-c">
                                        <td height="25">
                                            <asp:Label ID="lblVehTag" runat="server" />
                                        </td>
                                        <td colspan="4">
                                            <telerik:RadComboBox CssClass="fontObject" ID="lstVehCode" runat="server" AllowCustomText="True"
                                                EmptyMessage="Please Select Vehicle" Height="200" Width="95%" ExpandDelay="50"
                                                Filter="Contains" Sort="Ascending" EnableVirtualScrolling="True">
                                                <CollapseAnimation Type="InQuart" />
                                            </telerik:RadComboBox>
                                            <asp:Label ID="lblVehCodeErr" Visible="False" ForeColor="red" runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="mb-c" hidden>
                                        <td height="25">
                                            <asp:Label ID="lblVehExpTag" runat="server" />
                                        </td>
                                        <td colspan="4">
                                            <asp:DropDownList ID="lstVehExp" Width="95%" runat="server" />
                                            <asp:Label ID="lblVehExpCodeErr" Visible="False" ForeColor="red" runat="server" />
                                        </td>
                                    </tr>
                                    <tr id="RowFP" visible="false" class="mb-c">
                                        <td class="style6">
                                            Tax Invoice/Faktur Pajak No. :
                                        </td>
                                        <td colspan="4" class="style7">
                                            <asp:TextBox ID="txtFakturPjkNo" CssClass="font9Tahoma" Width="22%" MaxLength="19"
                                                runat="server" />
                                            <asp:Label ID="lblErrFakturPjk" Text="Please enter tax invoice/faktur pajak no."
                                                Visible="False" ForeColor="red" runat="server" />
                                        </td>
                                    </tr>
                                    <tr id="RowFPDate" visible="false" class="mb-c">
                                        <td height="25" style="width: 166px">
                                            Tax Invoice Date/Tgl. Faktur Pajak :
                                        </td>
                                        <td colspan="4">
                                            <asp:TextBox ID="txtFakturDate" Width="22%" CssClass="font9Tahoma" MaxLength="10"
                                                runat="server" />
                                            <a href="javascript:PopCal('txtFakturDate');">
                                                <asp:Image ID="Image1" ImageAlign="AbsMiddle" runat="server" ImageUrl="../../Images/calendar.gif" /></a>
                                            <asp:Label ID="lblDateFaktur" Text="<br>Date Entered should be in the format " ForeColor="red"
                                                Visible="false" runat="server" />
                                            <asp:Label ID="lblFmtFaktur" ForeColor="red" Visible="false" runat="server" />
                                        </td>
                                    </tr>
                                    <tr id="RowTax" visible="false" class="mb-c">
                                        <td height="25">
                                            Tax Object :
                                        </td>
                                        <td colspan="4">
                                            <asp:DropDownList ID="lstTaxObject" CssClass="font9Tahoma" Width="95%" AutoPostBack="True"
                                                OnSelectedIndexChanged="lstTaxObject_OnSelectedIndexChanged" runat="server" />
                                            <asp:Label ID="lblTaxObjectErr" Visible="False" ForeColor="red" runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="mb-c">
                                        <td height="25" width="20%">
                                            Line Description :*
                                        </td>
                                        <td colspan="4">
                                            <textarea rows="4" id="txtDescLn" cols="100" style='width: 95%;' cssclass="fontObject"
                                                runat="server"></textarea>
                                            <asp:Label ID="lblDescErr" Text="Please enter Line Description" Visible="False" ForeColor="red"
                                                runat="server" />
                                        </td>
                                    </tr>
                                    <tr id="RowTaxAmt" visible="false" class="mb-c">
                                        <td width="15%">
                                            DPP Amount (DR) :
                                        </td>
                                        <td width="30%">
                                            <telerik:RadNumericTextBox ID="txtDPPAmountDR" OnKeyUp="javascript:calTaxPriceDR();"
                                                CssClass="font9Tahoma" runat="server" LabelWidth="64px">
                                                <NumberFormat ZeroPattern="n"></NumberFormat>
                                                <EnabledStyle HorizontalAlign="Right" />
                                            </telerik:RadNumericTextBox>
                                            <asp:Label ID="lblTwoAmountDPP" Visible="false" ForeColor="red" Text="<BR>Please enter either DR or CR DPP amount"
                                                runat="server" />
                                        </td>
                                        <td width="10%">
                                            &nbsp;
                                        </td>
                                        <td width="15%">
                                            DPP Amount (CR) :
                                        </td>
                                        <td width="30%">
                                            <telerik:RadNumericTextBox ID="txtDPPAmountCR" OnKeyUp="javascript:calTaxPriceDR();"
                                                CssClass="font9Tahoma" runat="server" LabelWidth="64px">
                                                <NumberFormat ZeroPattern="n"></NumberFormat>
                                                <EnabledStyle HorizontalAlign="Right" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr class="mb-c">
                                        <td width="15%">
                                            Amount (DR) :
                                        </td>
                                        <td width="30%">
                                            <telerik:RadNumericTextBox ID="txtDRTotalAmount" CssClass="font9Tahoma" runat="server"
                                                LabelWidth="64px">
                                                <NumberFormat ZeroPattern="n"></NumberFormat>
                                                <EnabledStyle HorizontalAlign="Right" />
                                            </telerik:RadNumericTextBox>
                                            <asp:Label ID="lblTwoAmount" Visible="false" ForeColor="red" Text="<BR>Please enter either DR or CR total amount"
                                                runat="server" />
                                        </td>
                                        <td width="10%">
                                            &nbsp;
                                        </td>
                                        <td width="10%">
                                            Amount (CR) :
                                        </td>
                                        <td width="30%">
                                            <telerik:RadNumericTextBox ID="txtCRTotalAmount" CssClass="font9Tahoma" runat="server">
                                                <NumberFormat ZeroPattern="n"></NumberFormat>
                                                <EnabledStyle HorizontalAlign="Right" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr class="mb-c">
                                        <td height="25" width="20%">
                                            Cheque/Giro No :
                                        </td>
                                        <td width="30%">
                                            <asp:DropDownList Width="68%" ID="ddlGiroNo" CssClass="font9Tahoma" runat="server"
                                                AutoPostBack="false" />
                                            <asp:Label ID="lblErrGiroNo" Text="Please enter Line Description" Visible="False"
                                                ForeColor="red" runat="server" />
                                        </td>
                                        <td width="10%">
                                            &nbsp;
                                        </td>
                                        <td width="10%">
                                            Name :
                                        </td>
                                        <td width="30%">
                                            <asp:TextBox ID="txtGiroName" CssClass="font9Tahoma" Width="84%" MaxLength="128"
                                                runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="mb-c">
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td colspan="2">
                                            <asp:Label ID="lblerror" Text="<br>Number generated is too big!" Visible="False"
                                                ForeColor="red" runat="server" />
                                            <asp:Label ID="lblStock" Text="<br>Not enough quantity in hand!" Visible="False"
                                                ForeColor="red" runat="server" />
                                            <asp:Label ID="lbleither" Text="<br>Please key in either Meter Reading OR Quantity to issue"
                                                Visible="False" ForeColor="red" runat="server" />
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr class="mb-c">
                                        <td colspan="3">
                                            <asp:ImageButton ID="AddDtlBtn" ImageUrl="../../images/butt_add.gif" OnClick="AddDtlBtn_Click"
                                                UseSubmitBehavior="false" runat="server" />
                                            <asp:ImageButton ID="SaveDtlBtn" Visible="false" ImageUrl="../../images/butt_save.gif"
                                                OnClick="AddDtlBtn_Click" runat="server" />
                                            <br />
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:Label ID="lblConfirmErr" Text="<BR>control amount must equal to zero and have data transaction"
                                    Visible="False" ForeColor="red" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:DataGrid ID="dgTx" AutoGenerateColumns="false" Width="100%" runat="server" OnItemDataBound="DataGrid_ItemCreated"
                                    GridLines="none" CellPadding="2" PagerStyle-Visible="False" OnDeleteCommand="DEDR_Delete"
                                    OnEditCommand="DEDR_Edit" AllowSorting="True" class="font9Tahoma">
                                    <HeaderStyle CssClass="mr-h" />
                                    <ItemStyle CssClass="mr-l" />
                                    <AlternatingItemStyle CssClass="mr-r" />
                                    <HeaderStyle BackColor="#CCCCCC" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" />
                                    <ItemStyle BackColor="#FEFEFE" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" />
                                    <AlternatingItemStyle BackColor="#EEEEEE" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="No.">
                                            <ItemStyle Width="3%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdx" runat="server" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblIdx" runat="server" />
                                            </EditItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Line Description">
                                            <ItemStyle Width="16%" />
                                            <ItemTemplate>
                                                <asp:Label Text='<%# Container.DataItem("Description") %>' ID="lblDesc" runat="server" /><br />
                                                <asp:Label Text='<%# Container.DataItem("SPLInfo") %>' ID="Label1" runat="server" />
                                                <asp:Label Text='<%# Container.DataItem("SPLCode") %>' ID="lblSPLCode" Visible="false"
                                                    runat="server" />
                                                <asp:Label Text='<%# Container.DataItem("SPLName") %>' ID="lblSPLName" Visible="false"
                                                    runat="server" />
                                                <asp:Label Text='<%# Container.DataItem("SPLFaktur") %>' ID="lblSPLFaktur" Visible="false"
                                                    runat="server" />
                                                <asp:Label Text='<%# Container.DataItem("SPLFakturDate") %>' ID="lblSPLFakturDate"
                                                    Visible="false" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <ItemStyle Width="10%" />
                                            <ItemTemplate>
                                                <asp:Label Text='<%# Container.DataItem("AccCode") %>' ID="lblAccCode" runat="server" /><br />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="COA Descr">
                                            <ItemStyle Width="20%" />
                                            <ItemTemplate>
                                                <asp:Label Text='<%# Container.DataItem("AccDescr") %>' ID="lblAccDescr" runat="server" /><br />
                                                <asp:Label Text='<%# Container.DataItem("TaxObject") %>' ID="lblTaxObject" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <ItemStyle Width="12%" />
                                            <ItemTemplate>
                                                <asp:Label Text='<%# Container.DataItem("LocCode") %>' ID="lblLocCode" runat="server" /><br>
                                                <asp:Label Text='<%# Container.DataItem("BlkCode") %>' ID="lblBlkCode" runat="server" />-
                                                <asp:Label Text='<%# Container.DataItem("BlkDESC") %>' ID="lblBLKDesc" runat="server"
                                                    Width="82px" />
                                                <br />
                                                <asp:Label Text='<%# Container.DataItem("VehCode") %>' ID="lblVehCodeTr" runat="server" /><br />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <ItemStyle Width="10%" />
                                            <ItemTemplate>
                                                <asp:Label Text='<%# Container.DataItem("GiroNo") %>' ID="lblGiroNo" runat="server" /><br />
                                                <asp:Label Text='<%# Container.DataItem("GiroName") %>' ID="lblGiroName" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Quantity" Visible="False">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle Width="8%" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <%# objGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("Quantity"),5) %>
                                                <asp:Label ID="lblQtyTrx" Text='<%# Container.DataItem("Quantity") %>' Visible="false"
                                                    runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Amount">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle Width="8%" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("UnitPrice"), 2), 2)%>
                                                <asp:Label ID="lblUnitCost" Text='<%# Container.DataItem("UnitPrice") %>' Visible="false"
                                                    runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Total Amount">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle Width="10%" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblAmount" Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Total"), 2), 2) %>'
                                                    runat="server" />
                                                <asp:Label ID="lblAccTx" runat="server" />
                                                <asp:Label ID="lblAmt" Text='<%# FormatNumber(Container.DataItem("Total"), 2) %>'
                                                    Visible="false" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <ItemStyle Width="5%" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label Text='<%# Container.DataItem("CashBankLnID") %>' Visible="False" ID="lblID"
                                                    runat="server" />
                                                <asp:Label Text='<%# Container.DataItem("TaxLnID") %>' Visible="False" ID="lblTaxLnID"
                                                    runat="server" />
                                                <asp:Label Text='<%# Container.DataItem("TaxRate") %>' Visible="False" ID="lblTaxRate"
                                                    runat="server" />
                                                <asp:Label Text='<%# Container.DataItem("DPPAmount") %>' Visible="False" ID="lblDPPAmount"
                                                    runat="server" />
                                                <asp:Label Text='<%# Container.DataItem("VehCode") %>' Visible="False" ID="lblVehCode"
                                                    runat="server" />
                                                <br>
                                                <asp:Label Text='<%# Container.DataItem("VehExpCode") %>' Visible="False" ID="lblVehExpCode"
                                                    runat="server" />
                                                <asp:Label Text='<%# Container.DataItem("RefNo") %>' Visible="False" ID="lblRefNo"
                                                    runat="server" />
                                                <asp:Label Text='<%# Container.DataItem("StaffAdvDoc") %>' Visible="False" ID="lblStaffAdvDoc"
                                                    runat="server" />
                                                <asp:LinkButton ID="lbEdit" CommandName="Edit" Text="Edit" CausesValidation="False"
                                                    Visible="false" runat="server" />
                                                <asp:LinkButton ID="lbDelete" CommandName="Delete" Text="Delete" CausesValidation="False"
                                                    Visible="false" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <PagerStyle Visible="False" />
                                </asp:DataGrid>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                &nbsp;
                            </td>
                            <td height="25" align="right" style="width: 184px">
                                Total Amount :
                                <asp:Label ID="lblTotAmtFig" Text="0" runat="server" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                &nbsp;
                            </td>
                            <td align="right" style="width: 184px">
                                Control Amount :
                                <asp:Label ID="lblCtrlAmtFig" runat="server" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td height="25" width="20%">
                                Remarks :
                            </td>
                            <td colspan="5">
                                <asp:TextBox ID="txtRemark" CssClass="font9Tahoma" MaxLength="256" Width="100%" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td width="5%">
                                &nbsp;
                            </td>
                            <td class="style3">
                                &nbsp;
                            </td>
                            <td width="15%">
                                <asp:Label ID="lblReprint" Text="<B>( R E P R I N T )</B><br>" Visible="False" ForeColor="Red"
                                    runat="server" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                &nbsp;
                                <asp:Label ID="lblLocCodeErr" Text="" Visible="False" ForeColor="red" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:ImageButton ID="NewCBBtn" UseSubmitBehavior="false" OnClick="NewCBBtn_Click"
                                    ImageUrl="../../images/butt_new.gif" AlternateText="New" runat="server" />
                                <asp:ImageButton ID="SaveBtn" UseSubmitBehavior="false" OnClick="SaveBtn_Click" ImageUrl="../../images/butt_save.gif"
                                    AlternateText="Save" runat="server" />
                                <asp:ImageButton ID="VerifiedBtn" UseSubmitBehavior="false" OnClick="VerifiedBtn_Click"
                                    ImageUrl="../../images/butt_verified.gif" AlternateText="Verified" runat="server" />
                                <asp:ImageButton ID="ConfirmBtn" UseSubmitBehavior="false" OnClick="ConfirmBtn_Click"
                                    ImageUrl="../../images/butt_confirm.gif" AlternateText="Confirm" runat="server" />
                                <asp:ImageButton ID="ForwardBtn" UseSubmitBehavior="false" CausesValidation="False"
                                    OnClick="ForwardBtn_Click" ImageUrl="../../images/butt_move_forward.gif" AlternateText="Move Forward"
                                    runat="server" />
                                <asp:ImageButton ID="DeleteBtn" UseSubmitBehavior="false" OnClick="DeleteBtn_Click"
                                    CausesValidation="false" ImageUrl="../../images/butt_delete.gif" AlternateText="Delete"
                                    runat="server" />
                                <asp:ImageButton ID="UnDeleteBtn" UseSubmitBehavior="false" OnClick="UnDeleteBtn_Click"
                                    ImageUrl="../../images/butt_undelete.gif" AlternateText="Undelete" runat="server" />
                                <asp:ImageButton ID="EditBtn" UseSubmitBehavior="false" OnClick="EditBtn_Click" ImageUrl="../../images/butt_edit.gif"
                                    AlternateText="Edit" CausesValidation="False" runat="server" />
                                <asp:ImageButton ID="CancelBtn" UseSubmitBehavior="false" OnClick="CancelBtn_Click"
                                    ImageUrl="../../images/butt_cancel.gif" AlternateText="Cancel" CausesValidation="False"
                                    runat="server" />
                                <asp:ImageButton ID="BackBtn" UseSubmitBehavior="false" CausesValidation="False"
                                    OnClick="BackBtn_Click" ImageUrl="../../images/butt_back.gif" AlternateText="Back"
                                    runat="server" />
                                <input type="hidden" id="payid" value="" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:ImageButton ID="PrintBtn" UseSubmitBehavior="false" OnClick="PreviewBtn_Click"
                                    CausesValidation="false" ImageUrl="../../images/butt_print.gif" AlternateText="Print"
                                    runat="server" />
                                <asp:ImageButton ID="PrintChequeBtn" UseSubmitBehavior="false" OnClick="PreviewChequeBtn_Click"
                                    CausesValidation="false" ImageUrl="../../images/butt_print_cheque.gif" AlternateText="Print Cheque"
                                    runat="server" />
                                <asp:ImageButton ID="PrintSlipBtn" UseSubmitBehavior="false" OnClick="PreviewSlipBtn_Click"
                                    CausesValidation="false" ImageUrl="../../images/butt_print_slip.gif" AlternateText="Print Slip"
                                    runat="server" />
                                <asp:ImageButton ID="PrintTransferBtn" UseSubmitBehavior="false" OnClick="PreviewTransferBtn_Click"
                                    CausesValidation="false" ImageUrl="../../images/butt_print_slip_transfer.gif"
                                    AlternateText="Print Slip Transfer" runat="server" />
                                <asp:ImageButton ID="PrintKwitansiBtn" UseSubmitBehavior="false" OnClick="PreviewKwitansiBtn_Click"
                                    CausesValidation="false" ImageUrl="../../images/butt_print_kwitansi.gif" AlternateText="Print Kwitansi"
                                    runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                &nbsp;
                            </td>
                        </tr>
                        <tr id="TrLink" runat="server">
                            <td colspan="5">
                                <asp:LinkButton ID="lbViewJournal" Text="View Journal Predictions" CausesValidation="false"
                                    runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:DataGrid ID="dgViewJournal" AutoGenerateColumns="false" Width="100%" runat="server"
                                    GridLines="none" CellPadding="1" PagerStyle-Visible="False" AllowSorting="false"
                                    class="font9Tahoma">
                                    <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC" />
                                    <ItemStyle CssClass="mr-l" BackColor="#FEFEFE" />
                                    <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="COA Code">
                                            <ItemStyle Width="20%" />
                                            <ItemTemplate>
                                                <asp:Label Text='<%# Container.DataItem("ActCode") %>' ID="lblCOACode" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Description">
                                            <ItemStyle Width="40%" />
                                            <ItemTemplate>
                                                <asp:Label Text='<%# Container.DataItem("Description") %>' ID="lblCOADescr" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Debet">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" Width="20%" />
                                            <ItemTemplate>
                                                <asp:Label Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("AmountDB"), 2), 2) %>'
                                                    ID="lblAmountDB" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Credit">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" Width="20%" />
                                            <ItemTemplate>
                                                <asp:Label Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("AmountCR"), 2), 2) %>'
                                                    ID="lblAmountCR" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                &nbsp;
                            </td>
                            <td height="25" align="right">
                                <asp:Label ID="lblTotalViewJournal" Visible="false" runat="server" />
                            </td>
                            <td width="5%">
                                &nbsp;
                            </td>
                            <td align="right" class="style3">
                                <asp:Label ID="lblTotalDB" Text="0" Visible="false" runat="server" />
                            </td>
                            <td width="15%">
                                &nbsp;
                            </td>
                            <td align="right">
                                <asp:Label ID="lblTotalCR" Text="0" Visible="false" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <asp:Label ID="lblPayType" Visible="false" Text="0" runat="server" />
                    <asp:Label ID="lblErrMessage" Visible="false" Text="Error while initiating component."
                        runat="server" />
                    <asp:Label ID="issueType" Visible="False" runat="server" />
                    <asp:Label ID="lblStsHid" Visible="False" runat="server" />
                    <asp:Label ID="blnShortCut" Visible="False" runat="server" />
                    <asp:Label ID="lblCode" Visible="false" Text=" Code" runat="server" />
                    <asp:Label ID="lblSelect" Visible="false" Text="Select " runat="server" />
                    <asp:Label ID="lblPleaseSelect" Visible="false" Text="Please select " runat="server" />
                    <asp:Label ID="blnUpdate" Visible="False" runat="server" />
                    <asp:Label ID="lblTxLnID" Visible="false" runat="server" />
                    <asp:Label ID="lblStatusInput" Visible="false" runat="server" />
                    <asp:Label ID="lblCurrentPeriod" Visible="false" runat="server" />
                    <input type="hidden" id="hidBlockCharge" value="" runat="server" />
                    <input type="hidden" id="hidChargeLocCode" value="" runat="server" />
                    <input type="hidden" id="hidUserID" value="" runat="server" />
                    <input type="hidden" id="hidNPWPNo" value="" runat="server" />
                    <input type="hidden" id="hidTaxObjectRate" value="0" runat="server" />
                    <input type="hidden" id="hidCOATax" value="0" runat="server" />
                    <input type="hidden" id="hidTaxStatus" value="1" runat="server" />
                    <input type="hidden" id="hidHadCOATax" value="0" runat="server" />
                    <input type="hidden" id="hidPrevID" value="" runat="server" />
                    <input type="hidden" id="hidPrevDate" value="0" runat="server" />
                    <input type="hidden" id="hidNextID" value="" runat="server" />
                    <input type="hidden" id="hidNextDate" value="0" runat="server" />
                    <input type="hidden" id="hidFFBSpl" value="0" runat="server" />
                    <input type="hidden" id="hidSplCode" value="" runat="server" />
                    <input type="hidden" id="hidTaxPPN" value="0" runat="server" />
                    <br />
                    <asp:TextBox ID="txtPPN" CssClass="font9Tahoma" runat="server" BackColor="Transparent"
                        BorderStyle="None" Width="9%"></asp:TextBox><asp:TextBox ID="txtCreditTerm" runat="server"
                            BackColor="Transparent" BorderStyle="None" Width="9%"></asp:TextBox><asp:TextBox
                                ID="txtPPNInit" runat="server" BackColor="Transparent" BorderStyle="None" Width="9%"></asp:TextBox>
                    <br />
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
