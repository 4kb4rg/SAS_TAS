<%@ Page Language="vb" Src="../../../include/cb_trx_PayDet.aspx.vb" Inherits="cb_trx_PayDet" %>

<%@ Register TagPrefix="UserControl" TagName="MenuCB" Src="../../menu/menu_cbtrx.ascx" %>
<%@ Register TagPrefix="Preference" TagName="PrefHdl" Src="../../include/preference/preference_handler.ascx" %>
<%@ Register TagPrefix="qsf" Namespace="Telerik.QuickStart" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<html>
<head>
    <title>Payment Details</title>
    <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
    <Preference:PrefHdl ID="PrefHdl" runat="server" />
    <script language="javascript">
        function fnPosNeg(str) {
            var hidField = document.getElementById("hidCreditJrnValue");
            var iEnd, iLen = str.length;

            if (13 + 1 > iLen)
                iEnd = iLen;
            else
                iEnd = 13 + 1;

            hidField.value = str.substring(13, iEnd);
            //alert(hidField.value);
            return;
        }
        function calTaxPrice() {
            var doc = document.frmMain;
            var a = parseFloat(doc.txtDPPAmount.value);
            var b = parseFloat(doc.hidTaxObjectRate.value);
            var c = (a * (b / 100));
            var newnumber = new Number(c + '').toFixed(parseInt(0));

            doc.txtAmount.value = newnumber;
            if (doc.txtAmount.value == 'NaN')
                doc.txtAmount.value = '';
            else
                doc.txtAmount.value = doc.txtAmount.value;
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
            width: 158px;
        }
        .style3
        {
            width: 20%;
        }
        .style5
        {
            width: 4%;
        }
        .style6
        {
            width: 269px;
        }
        .style7
        {
            width: 112px;
        }
        .style8
        {
            width: 151px;
        }
        .style9
        {
            width: 667px;
        }
        .style10
        {
            width: 25%;
        }
    </style>
</head>
<body>
    <form id="frmMain" class="main-modul-bg-app-list-pu" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
        <tr>
            <td style="width: 100%; height: 800px" valign="top">
                <div class="kontenlist">
                    <table id="tblHeader" cellspacing="0" cellpadding="2" width="100%" class="font9Tahoma"
                        border="0">
                        <tr>
                            <td colspan="6">
                                <UserControl:MenuCB ID="MenuCB" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="mt-h" colspan="6">
                                <strong>PAYMENT DETAILS</strong>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <hr style="width: 100%" />
                            </td>
                        </tr>
                        <tr>
                            <td height="20" class="style6">
                                &nbsp;
                            </td>
                            <td width="40%">
                                &nbsp;
                            </td>
                            <td class="style5">
                                &nbsp;
                            </td>
                            <td class="style7">
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
                            <td height="20" class="style6">
                                Payment ID :
                            </td>
                            <td width="40%">
                                <asp:Label ID="lblPaymentID" runat="server" />
                            </td>
                            <td class="style5">
                                &nbsp;
                            </td>
                            <td class="style7">
                                Period :
                            </td>
                            <td class="style3">
                                <asp:Label ID="lblAccPeriod" runat="server" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td height="25" class="style6">
                                Transaction Date :
                            </td>
                            <td>
                                <asp:TextBox ID="txtDateCreated" Width="25%" MaxLength="10" CssClass="fontObject"
                                    runat="server" />
                                <a href="javascript:PopCal('txtDateCreated');">
                                    <asp:Image ID="btnDateCreated" ImageAlign="AbsMiddle" runat="server" ImageUrl="../../Images/calendar.gif" /></a>
                                <asp:RequiredFieldValidator ID="rfvDateCreated" runat="server" ControlToValidate="txtDateCreated"
                                    Text="Please enter Date Created" Display="dynamic" />
                                <asp:Label ID="lblDate" Text="<br>Date Entered should be in the format " ForeColor="red"
                                    Visible="false" runat="server" />
                                <asp:Label ID="lblFmt" ForeColor="red" Visible="false" runat="server" />
                            </td>
                            <td class="style5">
                                &nbsp;
                            </td>
                            <td class="style7">
                                Status :
                            </td>
                            <td class="style3">
                                <asp:Label ID="lblStatus" runat="server" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td height="25" align="justify" class="style6">
                                Supplier Code :*
                            </td>
                            <td>
                                <asp:TextBox ID="txtSupCode" runat="server" AutoPostBack="False" CssClass="fontObject"
                                    MaxLength="15" Width="50%"></asp:TextBox>
                                <input type="button" value=" ... " id="Find" class="button-small" onclick="javascript:PopSupplier_New('frmMain','','txtSupCode','txtSupName','txtCreditTerm','txtPPN','txtPPNInit', 'False');"
                                    causesvalidation="False" runat="server" />
                                <asp:ImageButton ID="btnGet" runat="server" AlternateText="Get Data" ToolTip="Click For Get Data"
                                    OnClick="GetSupplierBtn_Click" CausesValidation="False" ImageAlign="AbsBottom"
                                    ImageUrl="../../images/icn_next.gif" /><br />
                                <asp:TextBox ID="txtSupName" CssClass="fontObject" runat="server" BackColor="Transparent"
                                    BorderStyle="None" Font-Bold="True" MaxLength="10" Width="99%"></asp:TextBox><asp:RequiredFieldValidator
                                        ID="validateSupp" runat="server" ErrorMessage="Please select Supplier Code" ControlToValidate="txtSupCode"
                                        Display="dynamic" />
                            </td>
                            <td class="style5">
                                &nbsp;
                            </td>
                            <td class="style7">
                                Date Created :
                            </td>
                            <td class="style3">
                                <asp:Label ID="lblDateCreated" runat="server" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td height="25" class="style6">
                                Payment Type :*
                            </td>
                            <td>
                                <asp:DropDownList Width="100%" ID="ddlPayType" AutoPostBack="true" CssClass="fontObject"
                                    runat="server" OnSelectedIndexChanged="onSelect_PayType">
                                    <asp:ListItem Value="0">Cheque</asp:ListItem>
                                    <asp:ListItem Value="1" Selected>Cash</asp:ListItem>
                                    <asp:ListItem Value="3">Bilyet Giro</asp:ListItem>
                                    <asp:ListItem Value="4">Others</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="lblErrPayType" ForeColor="red" Visible="false" Text="Please select Payment Type"
                                    runat="server" />
                            </td>
                            <td class="style5">
                                &nbsp;
                            </td>
                            <td class="style7">
                                Last Update :
                            </td>
                            <td class="style3">
                                <asp:Label ID="lblLastUpdate" runat="server" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="style6">
                                Bilyet Giro Date :
                            </td>
                            <td>
                                <asp:TextBox ID="txtGiroDate" Width="25%" MaxLength="10" CssClass="fontObject" runat="server" />
                                <a href="javascript:PopCal('txtGiroDate');">
                                    <asp:Image ID="btnGiroDate" ImageAlign="AbsMiddle" runat="server" ImageUrl="../../Images/calendar.gif" /></a>
                                <asp:Label ID="lblDateGiro" Text="<br>Date Entered should be in the format " ForeColor="red"
                                    Visible="false" runat="server" />
                                <asp:Label ID="lblFmtGiro" ForeColor="red" Visible="false" runat="server" />
                            </td>
                            <td class="style5">
                                &nbsp;
                            </td>
                            <td class="style7">
                                Print Date :
                            </td>
                            <td class="style3">
                                <asp:Label ID="lblPrintDate" runat="server" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td height="25" class="style6">
                                Bank From *:
                            </td>
                            <td>
                                <asp:DropDownList Width="100%" ID="ddlBank" CssClass="fontObject" AutoPostBack="true"
                                    OnSelectedIndexChanged="onSelect_Bank" runat="server" />
                                <asp:Label ID="lblErrBank" ForeColor="red" Visible="false" Text="Please select Bank Code"
                                    runat="server" />
                            </td>
                            <td class="style5">
                                &nbsp;
                            </td>
                            <td class="style7">
                                Cheque Print Date :
                            </td>
                            <td class="style3">
                                <asp:Label ID="lblChequePrintDate" runat="server" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <!--
			<tr>
				<TD height=25>Cheque/Bilyet Giro No. :</td>
				<td><asp:Textbox id=txtChequeNo width=100% maxlength=32 runat=server />
					<asp:Label id=lblErrCheque forecolor=red visible=false text="Please enter Cheque No." runat=server/></td>
				<td>&nbsp;</td>
				<td>Updated By :</td>
				<td><asp:Label ID=lblUpdatedBy runat=server /></td>
				<td>&nbsp;</td>
			</tr>
			-->
                        <tr>
                            <td height="25" class="style6">
                                Supplier Bank Account No. :
                            </td>
                            <td>
                                <asp:DropDownList Width="100%" ID="ddlSplBankAccNo" CssClass="fontObject" AutoPostBack="true"
                                    OnSelectedIndexChanged="onSelect_SplBankAccNo" runat="server" />
                                <asp:TextBox ID="txtSplBankAccNo" Visible="false" Width="0%" MaxLength="32" CssClass="fontObject"
                                    runat="server" />
                                <asp:Label ID="lblErrBankAccNo" ForeColor="red" Visible="false" Text="Please enter Bank Account No."
                                    runat="server" />
                            </td>
                            <td class="style5">
                                &nbsp;
                            </td>
                            <td class="style7">
                                <asp:Label ID="lblTaxStatus" Text="Tax Status :" runat="server" />
                            </td>
                            <td class="style3">
                                <asp:Label ID="lblTaxStatusDesc" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td height="25" class="style6">
                                Currency Code :
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCurrCode" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="CurrCodeChanged"
                                    CssClass="fontObject" runat="server" />
                            </td>
                            <td class="style5">
                                &nbsp;
                            </td>
                            <td class="style7">
                                <asp:Label ID="lblTaxUpdate" Text="Tax Updated By :" runat="server" />
                            </td>
                            <td class="style3">
                                <asp:Label ID="lblTaxUpdateDesc" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td height="25" class="style6">
                                Exchange Rate :
                            </td>
                            <td>
                                <asp:TextBox ID="txtExchangeRate" Text="1" Width="25%" MaxLength="20" CssClass="fontObject"
                                    runat="server" />
                                <asp:Label ID="lblErrExchangeRate" Text="Exchange rate for this date has not been created."
                                    ForeColor="red" Visible="false" runat="server" />
                            </td>
                            <td class="style5">
                                &nbsp;
                            </td>
                            <td class="style7">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td height="25" class="style6">
                                Reference No/Staff Advance :
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlRefNo" Width="100%" CssClass="fontObject" runat="server" />
                                <asp:Label ID="lblRefNoErr" Visible="False" ForeColor="red" runat="server" />
                            </td>
                            <td class="style5">
                                &nbsp;
                            </td>
                            <td class="style7">
                                &nbsp;<asp:Label ID="lblSKBStartDate" runat="server" Visible="False" />
                            </td>
                            <td class="style3">
                                &nbsp;<asp:Label ID="LblIsSKBActive" runat="server" Visible="False" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td height="25" class="style6">
                                Print Cheque/Bilyet Giro as Cash :
                            </td>
                            <td>
                                <asp:CheckBox ID="chkChequeCash" Text="" runat="server" />
                            </td>
                            <td class="style5">
                                &nbsp;
                            </td>
                            <td class="style7">
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
                            <td height="25" class="style6">
                                Cheque/Bilyet Giro Handover :
                            </td>
                            <td>
                                <asp:DropDownList Width="100%" ID="ddlChequeHandOver" AutoPostBack="false" CssClass="fontObject"
                                    runat="server">
                                    <asp:ListItem Value="0" Selected>N/A</asp:ListItem>
                                    <asp:ListItem Value="1">Pick-up</asp:ListItem>
                                    <asp:ListItem Value="2">Bank Transfer</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="style5">
                                &nbsp;
                            </td>
                            <td class="style7">
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
                            <td height="25" colspan="5">
                                <asp:Label ID="lblErrPrintCheque" ForeColor="red" Visible="false" Text="Cheque format not found! Please check your Bank Details again."
                                    runat="server" />
                            </td>
                        </tr>
                    </table>
                    <table id="TABLE1" cellspacing="0" cellpadding="2" width="100%" border="0" class="font9Tahoma">
                        <tr>
                            <td colspan="6">
                                <table id="tblSelection" cellspacing="0" cellpadding="4" width="100%" border="0"
                                    class="sub-Add" runat="server">
                                    <tr class="mb-c">
                                        <td colspan="6">
                                            <asp:Label ID="Label10" Text="Please select one of these option:" Font-Bold="true"
                                                runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="mb-c">
                                        <td height="25" class="style10">
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1.
                                            <asp:Label ID="lblInvoiceRcvIdTag" runat="server" />
                                            :
                                        </td>
                                        <td colspan="5">
                                            <asp:DropDownList ID="ddlInvoiceRcv" Width="95%" AutoPostBack="true" OnSelectedIndexChanged="onSelect_InvRcv"
                                                CssClass="fontObject" runat="server" />
                                            <asp:Label ID="lblFindINVPOPPH23" ForeColor="red" Visible="false" Text="This Credited Invoice/Purchase Order has PPH23."
                                                runat="server" />
                                            <asp:Label ID="lblFindINVPOPPH21" ForeColor="red" Visible="false" Text="This Credited Invoice/Purchase Order has PPH21."
                                                runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="mb-c">
                                        <td height="25" class="style10">
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2. Debit Note ID :
                                        </td>
                                        <td colspan="5">
                                            <asp:DropDownList Width="95%" ID="ddlDebitNote" CssClass="fontObject" runat="server"
                                                AutoPostBack="true" OnSelectedIndexChanged="onSelect_DbtNote" />
                                        </td>
                                    </tr>
                                    <tr class="mb-c">
                                        <td height="25" class="style10">
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3. Credit Note ID :
                                        </td>
                                        <td colspan="5">
                                            <asp:DropDownList Width="95%" ID="ddlCreditNote" CssClass="fontObject" runat="server"
                                                AutoPostBack="true" OnSelectedIndexChanged="onSelect_CrNote" />
                                        </td>
                                    </tr>
                                    <tr class="mb-c">
                                        <td height="25" class="style10">
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4. Creditor Journal ID :
                                        </td>
                                        <td colspan="5">
                                            <asp:DropDownList Width="95%" ID="ddlCreditorJournal" CssClass="fontObject" runat="server"
                                                AutoPostBack="true" OnSelectedIndexChanged="onSelect_CrJrn">
                                            </asp:DropDownList>
                                            <input type="hidden" id="hidCreditJrnValue" runat="server">
                                        </td>
                                    </tr>
                                    <tr class="mb-c">
                                        <td height="25" class="style10">
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;5. Other :
                                        </td>
                                        <td colspan="5">
                                            <asp:TextBox ID="txtAccCodeOther" CssClass="fontObject" runat="server" AutoPostBack="True"
                                                MaxLength="15" OnTextChanged="onSelect_StrAccCodeOther" Width="25%"></asp:TextBox>&nbsp;
                                            <input id="FindAcc" class="button-small" runat="server" causesvalidation="False"
                                                onclick="javascript:PopCOA_Desc('frmMain', '', 'txtAccCodeOther', 'txtAccOtherName', 'False');"
                                                type="button" value=" ... " />
                                            <asp:Button ID="CoaChangeButton" CssClass="button-small" runat="server" Text="Click Here"
                                                OnClick="onSelect_StrAccCodeOther" Font-Bold="True" ToolTip="Click For Refresh And View Tax Object Rate"
                                                Width="65px" />&nbsp;
                                            <asp:TextBox ID="txtAccOtherName" CssClass="fontObject" runat="server" BackColor="Transparent"
                                                BorderStyle="None" MaxLength="10" Width="55%" Font-Bold="True"></asp:TextBox><br />
                                            <asp:Label ID="lblAccCodeOtherErr" Visible="False" ForeColor="red" runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="mb-c">
                                        <td height="25" class="style10">
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6. PO ID (Advance Payment) :
                                        </td>
                                        <td colspan="5">
                                            <asp:DropDownList Width="95%" ID="ddlPOID" CssClass="fontObject" runat="server" AutoPostBack="true"
                                                OnSelectedIndexChanged="onSelect_PO" />
                                            <asp:Label ID="lblErrNoSelectDoc" ForeColor="red" Visible="false" Text="Please select one document."
                                                runat="server" />
                                            <asp:Label ID="lblErrManySelectDoc" ForeColor="red" Visible="false" Text="Please select ONLY one document."
                                                runat="server" />
                                            <asp:Label ID="lblErrValidPPNHRate" ForeColor="red" Visible="false" Text="Please select invoice with the same PPN and PPH 23/26 Rate."
                                                runat="server" />
                                            <asp:Label ID="lblErrOtherDoc" ForeColor="red" Visible="false" Text="Please select invoice before other payment."
                                                runat="server" />
                                            <asp:Label ID="lblFindPOPPH23" ForeColor="red" Visible="false" Text="This Credited Invoice/Purchase Order has PPH23."
                                                runat="server" />
                                            <asp:Label ID="lblFindPOPPH21" ForeColor="red" Visible="false" Text="This Credited Invoice/Purchase Order has PPH21."
                                                runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="mb-c">
                                        <td colspan="6">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr class="mb-c">
                                        <td height="25" class="style10">
                                            <asp:Label ID="lblAccount" runat="server" />
                                            (CR) :*
                                        </td>
                                        <td colspan="5">
                                            <asp:TextBox ID="txtAccCode" CssClass="fontObject" runat="server" AutoPostBack="True"
                                                MaxLength="15" OnTextChanged="onSelect_StrAccCode" Width="25%"></asp:TextBox>&nbsp;
                                            <input id="FindAcc2" class="button-small" runat="server" causesvalidation="False"
                                                onclick="javascript:PopCOA_Desc('frmMain', '', 'txtAccCode', 'txtAccName', 'False');"
                                                type="button" value=" ... " />
                                            <asp:Button ID="CoaChangeButton2" CssClass="button-small" runat="server" Text="Click Here"
                                                OnClick="onSelect_StrAccCodeOther" Font-Bold="True" ToolTip="Click For Refresh COA "
                                                Width="65px" />&nbsp;
                                            <asp:TextBox ID="txtAccName" CssClass="fontObject" runat="server" BackColor="Transparent"
                                                BorderStyle="None" MaxLength="10" Width="55%" Font-Bold="True"></asp:TextBox><asp:Label
                                                    ID="lblAccCodeErr" Visible="False" ForeColor="red" runat="server" />
                                            <asp:Label ID="lblErrAccCode" Visible="false" ForeColor="red" runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="mb-c">
                                        <td height="25" class="style10">
                                            Currency Code :
                                        </td>
                                        <td colspan="5">
                                            <asp:DropDownList ID="ddlCurrency" Width="25%" AutoPostBack="true" OnSelectedIndexChanged="CurrencyChanged"
                                                CssClass="fontObject" runat="server" />
                                            <asp:Label ID="lblErrCurrencyCode" Text="" ForeColor="red" Visible="false" runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="mb-c">
                                        <td height="25" class="style10">
                                            Exchange Rate :
                                        </td>
                                        <td colspan="5">
                                            <asp:TextBox ID="txtExRate" Text="1" Width="15%" MaxLength="20" CssClass="fontObject"
                                                runat="server" />
                                            <asp:Label ID="lblErrExRate" Text="Exchange rate for this date has not been created."
                                                ForeColor="red" Visible="false" runat="server" />
                                        </td>
                                    </tr>
                                    <tr id="RowTax" visible="false" class="mb-c">
                                        <td height="25" class="style10">
                                            Tax Object :
                                        </td>
                                        <td colspan="5">
                                            <asp:DropDownList ID="lstTaxObject" CssClass="fontObject" Width="95%" AutoPostBack="True"
                                                OnSelectedIndexChanged="lstTaxObject_OnSelectedIndexChanged" runat="server" />
                                            <asp:Label ID="lblTaxObjectErr" Visible="False" ForeColor="red" runat="server" />
                                        </td>
                                    </tr>
                                    <tr id="RowTaxAmt" visible="false" class="mb-c">
                                        <td height="25" class="style10">
                                            DPP Amount :
                                        </td>
                                        <td width="30%">
                                            <asp:TextBox ID="txtDPPAmount" CssClass="fontObject" Style="text-align: right" Width="41%"
                                                MaxLength="22" OnKeyUp="javascript:calTaxPrice();" runat="server" />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtDPPAmount"
                                                ValidationExpression="^(\-|)\d{1,19}(\.\d{1,2}|\.|)$" Display="Dynamic" Text="<BR>Maximum length 19 digits and 2 decimal points"
                                                runat="server" />
                                            <asp:Label ID="lblErrAmountDPP" Visible="false" ForeColor="red" Text="<BR>Please enter DPP amount"
                                                runat="server" />
                                        </td>
                                        <td width="5%">
                                        </td>
                                        <td width="15%">
                                        </td>
                                        <td width="10%">
                                        </td>
                                        <td width="30%">
                                        </td>
                                    </tr>
                                    <tr class="mb-c">
                                        <td height="25" class="style10">
                                            Amount :*
                                        </td>
                                        <td width="30%">
                                            <asp:TextBox ID="txtAmount" Style="text-align: right" Width="41%" MaxLength="22"
                                                CssClass="fontObject" runat="server" />
                                            <asp:Label ID="lblErrReqAmount" Visible="false" ForeColor="red" Text="Please enter amount."
                                                runat="server" />
                                            <asp:Label ID="lblErrNegAmt" Visible="false" ForeColor="red" Text="Please enter a negative amount."
                                                runat="server" />
                                            <asp:Label ID="lblErrPosAmt" Visible="false" ForeColor="red" Text="Please enter a positive amount."
                                                runat="server" />
                                            <asp:Label ID="lblErrExceeded" Visible="false" ForeColor="red" Text="The payment amount is exceeded outstanding amount for document."
                                                runat="server" />
                                        </td>
                                        <td width="5%">
                                            <asp:Label ID="lblPPN" Visible="false" Text='PPN :' runat="server" />
                                        </td>
                                        <td width="15%">
                                            <asp:CheckBox ID="cbPPN" Visible="false" Text=" Yes" runat="server" />
                                        </td>
                                        <td width="10%">
                                            <asp:Label ID="lblPPH" Visible="false" Text='PPh 23/26 Rate :' runat="server" />
                                        </td>
                                        <td width="30%">
                                            <asp:TextBox ID="txtPPHRate" Visible="false" Width="50%" MaxLength="5" runat="server" />
                                            <asp:Label ID="lblPercen" Visible="false" Text='%' runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="mb-c">
                                        <td height="25" class="style10">
                                            Cheque/Giro No :
                                        </td>
                                        <td colspan="5">
                                            <asp:DropDownList Width="95%" ID="ddlGiroNo" CssClass="fontObject" runat="server"
                                                AutoPostBack="false" />
                                        </td>
                                    </tr>
                                    <tr class="mb-c">
                                        <td height="25" valign="top" class="style10">
                                            Additional Note :
                                        </td>
                                        <td colspan="5">
                                            <textarea rows="4" id="txtAddNote" cols="100" style='width: 95%;' 
                                                cssclass="fontObject" runat="server"></textarea>
                                        </td>
                                    </tr>
                                    <tr class="mb-c">
                                        <td height="25" colspan="6">
                                            <asp:ImageButton ID="AddDtlBtn" ImageUrl="../../images/butt_add.gif" AlternateText="Add"
                                                OnClick="AddBtn_Click" UseSubmitBehavior="false" runat="server" />
                                            &nbsp;
                                            <asp:ImageButton ID="SaveDtlBtn" Visible="false" ImageUrl="../../images/butt_save.gif"
                                                OnClick="AddBtn_Click" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table id="TABLE2" cellspacing="0" cellpadding="1" width="100%" border="0" class="font9Tahoma">
                        <tr>
                            <td colspan="6">
                                <asp:Label ID="lblErrConfirmNotFulFil" Visible="false" ForeColor="red" runat="server" />
                                <asp:Label ID="lblErrConfirmNotFulFilText" Visible="false" Text="The payment amount is exceeded outstanding amount for document "
                                    runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:DataGrid ID="dgLineDet" AutoGenerateColumns="false" Width="100%" runat="server"
                                    GridLines="none" CellPadding="2"  OnItemCommand="MenuLink_Click" OnDeleteCommand="DEDR_Delete" OnEditCommand="DEDR_Edit"
                                    OnItemDataBound="dgLine_BindGrid" PagerStyle-Visible="False" AllowSorting="True">
                                    <HeaderStyle CssClass="mr-h" />
                                    <ItemStyle CssClass="mr-l" />
                                    <AlternatingItemStyle CssClass="mr-r" />
                                    <Columns>
                                        <asp:TemplateColumn ItemStyle-Width="15%" HeaderText="Document ID">
                                            <ItemTemplate>

                                                <asp:Label Text='<%# Container.DataItem("DocId") %>' ID="lblDocId" Visible="false" runat="server" />
                                                <asp:LinkButton ID="lblLinkDocID" CommandName="Item" Text='<%# Container.DataItem("DocId") %>'
                                                    runat="server" ToolTip="click for display document reference" /><br />
                                                <asp:Label Text='<%# Container.DataItem("TaxObject") %>' ID="lblTaxObject" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>

                                        <asp:TemplateColumn ItemStyle-Width="10%" HeaderText="Document Type">
                                            <ItemTemplate>
                                                <asp:Label Text='<%# objCBTrx.mtdGetPaymentDocType(Container.DataItem("DocType")) %>'
                                                    ID="lblDocType" runat="server" />
                                                <asp:Label Text='<%#Container.DataItem("DocType")%>' ID="lblEnumDocType" Visible="false"
                                                    runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label Text='<%# Container.DataItem("AccCode") %>' ID="lblAccCode" runat="server" />
                                                <asp:Label Text='<%# Container.DataItem("CBCurrencyCode") %>' ID="lblCurrCode" Visible="false"
                                                    runat="server" />
                                                <asp:Label Text='<%# Container.DataItem("CBExchangeRate") %>' ID="lblExRate" Visible="false"
                                                    runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn ItemStyle-Width="17%" HeaderText="COA Descr">
                                            <ItemTemplate>
                                                <asp:Label Text='<%# Container.DataItem("AccDescr") %>' ID="lblAccDescr" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn ItemStyle-Width="15%" HeaderText="Additional Note">
                                            <ItemTemplate>
                                                <asp:Label Text='<%# Container.DataItem("AdditionalNote") %>' ID="lblAddNote" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Cheque/Giro No" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label Text='<%# Container.DataItem("GiroNo") %>' ID="lblGiroNo" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="" ItemStyle-Width="1%" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("NetAmountToDisplay"), 2), 2) %>'
                                                    ID="lblViewNetAmount" Visible="false" runat="server" />
                                                <asp:Label Text='<%# FormatNumber(Container.DataItem("NetAmount"), 2) %>' ID="lblNetAmount"
                                                    Visible="False" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="" ItemStyle-Width="1%" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <itemstyle />
                                                <asp:Label Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PPNAmountToDisplay"), 2), 2) %>'
                                                    ID="lblIDPPNAmount" Visible="false" runat="server" />
                                                <asp:Label Text='<%# FormatNumber(Container.DataItem("PPNAmount"), 2) %>' ID="lblPPNAmount"
                                                    Visible="False" runat="server" />
                                                </ItemStyle>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="" ItemStyle-Width="1%" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <itemstyle />
                                                <asp:Label Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PPHAmountToDisplay"), 2), 2) %>'
                                                    ID="lblIDPPHAmount" Visible="false" runat="server" />
                                                <asp:Label Text='<%# FormatNumber(Container.DataItem("PPHAmount"), 2) %>' ID="lblPPHAmount"
                                                    Visible="False" runat="server" />
                                                </ItemStyle>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Total Amount" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("AmountToDisplay"), 2), 2) %>'
                                                    ID="lblViewAmount" runat="server" />
                                                <asp:Label Text='<%# FormatNumber(Container.DataItem("AmountToDisplay"), 2) %>' ID="lblAmountToDisplay"
                                                    Visible="false" runat="server" />
                                                <asp:Label Text='<%# FormatNumber(Container.DataItem("Amount"), 2) %>' ID="lblAmount"
                                                    Visible="False" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="cnlnid" Visible="false" Text='<%# Container.DataItem("PaymentLnId")%>'
                                                    runat="server" />
                                                <asp:Label Text='<%# Container.DataItem("TaxLnID") %>' Visible="False" ID="lblTaxLnID"
                                                    runat="server" />
                                                <asp:Label Text='<%# Container.DataItem("TaxRate") %>' Visible="False" ID="lblTaxRate"
                                                    runat="server" />
                                                <asp:Label Text='<%# Container.DataItem("DPPAmount") %>' Visible="False" ID="lblDPPAmount"
                                                    runat="server" />
                                                <asp:Label Text='<%# Container.DataItem("PO_PPH23") %>' Visible="False" ID="lblPO_PPH23"
                                                    runat="server" />
                                                <asp:Label Text='<%# Container.DataItem("PO_PPH21") %>' Visible="False" ID="lblPO_PPH21"
                                                    runat="server" />
                                                <asp:LinkButton ID="lbEdit" CommandName="Edit" Text="Edit" CausesValidation="False"
                                                    Visible="false" runat="server" />
                                                <asp:LinkButton ID="lbDelete" CommandName="Delete" Text="Delete" Visible="false"
                                                    runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                &nbsp;
                            </td>
                            <td colspan="3" height="25">
                                <hr style="width: 100%" />
                            </td>
                        </tr>
                        <tr class="font9Tahoma">
                            <td colspan="3">
                                &nbsp;
                            </td>
                            <td height="25" class="style8">
                                <strong>Total Payment Amount : </strong>
                            </td>
                            <td align="right" style="width: 145px">
                                <strong>
                                    <asp:Label ID="lblCurrency" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblViewTotalPaymentAmount"
                                        runat="server" />&nbsp;</strong>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblTotalPaymentAmount" Visible="False" runat="server" />&nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                &nbsp;
                            </td>
                            <td class="style8">
                                &nbsp;
                            </td>
                            <td align="right" style="width: 145px">
                                &nbsp;&nbsp;&nbsp;<asp:Label ID="lblShowTotalAmount" Visible="False" runat="server" />&nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td height="25" class="style1">
                                Remarks :
                            </td>
                            <td colspan="5">
                                <asp:TextBox ID="txtRemark" MaxLength="256" Width="100%" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:ImageButton ID="NewBtn" OnClick="NewBtn_Click" ImageUrl="../../images/butt_new.gif"
                                    AlternateText="New" runat="server" />
                                <asp:ImageButton ID="SaveBtn" OnClick="SaveBtn_Click" ImageUrl="../../images/butt_save.gif"
                                    AlternateText="Save" runat="server" />
                                <!--
					<asp:ImageButton ID=RefreshBtn CausesValidation=False onclick=RefreshBtn_Click ImageUrl="../../images/butt_refresh.gif" AlternateText=Refresh Runat=server />
					-->
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
                                <asp:ImageButton ID="BackBtn" UseSubmitBehavior="false" CausesValidation="False"
                                    OnClick="BackBtn_Click" ImageUrl="../../images/butt_back.gif" AlternateText="Back"
                                    runat="server" />
                                <input type="hidden" id="payid" value="" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:ImageButton ID="PrintBtn" UseSubmitBehavior="false" OnClick="btnPreview_Click"
                                    CausesValidation="false" ImageUrl="../../images/butt_print.gif" AlternateText="Print"
                                    runat="server" />
                                <asp:ImageButton ID="PrintChequeBtn" UseSubmitBehavior="false" OnClick="btnPreviewCheque_Click"
                                    CausesValidation="false" ImageUrl="../../images/butt_print_cheque.gif" AlternateText="Print Cheque"
                                    runat="server" />
                                <asp:ImageButton ID="PrintSlipBtn" UseSubmitBehavior="false" OnClick="btnPreviewSlip_Click"
                                    CausesValidation="false" ImageUrl="../../images/butt_print_slip.gif" AlternateText="Print Slip Setoran"
                                    runat="server" />
                                <asp:ImageButton ID="PrintTransferBtn" UseSubmitBehavior="false" OnClick="btnPreviewTransfer_Click"
                                    CausesValidation="false" ImageUrl="../../images/butt_print_slip_transfer.gif"
                                    AlternateText="Print Slip Transfer" runat="server" />
                            </td>
                        </tr>
                        <asp:Label ID="lblPayType" Visible="false" Text="0" runat="server" /><asp:Label ID="lblCode"
                            Visible="false" Text=" Code" runat="server" /><asp:Label ID="lblPleaseSelect" Visible="false"
                                Text="Please select " runat="server" /><asp:Label ID="lblPleaseSelectOne" Visible="false"
                                    Text="Please select one " runat="server" /><asp:Label ID="lblID" Visible="false"
                                        Text=" ID" runat="server" /><asp:Label ID="lblInvoiceRcvTag" Visible="false" runat="server" /><asp:Label
                                            ID="lblPPHRateHidden" Visible="false" runat="server" /><asp:Label ID="lblPPNHidden"
                                                Visible="false" runat="server" /><asp:Label ID="lblStatusHidden" Visible="false"
                                                    runat="server" /><asp:Label ID="lblProgramPath" Visible="false" runat="server" /><asp:Label
                                                        ID="lblInvTypeHidden" Visible="false" runat="server" /><asp:Label ID="lblCurrentPeriod"
                                                            Visible="false" runat="server" /><asp:Label ID="lblTxLnID" Visible="false" runat="server" /><tr>
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
                            <td colspan="6" style="height: 149px">
                                <asp:DataGrid ID="dgViewJournal" AutoGenerateColumns="false" Width="100%" runat="server"
                                    GridLines="none" CellPadding="1" PagerStyle-Visible="False" AllowSorting="false">
                                    <HeaderStyle CssClass="mr-h" />
                                    <ItemStyle CssClass="mr-l" />
                                    <AlternatingItemStyle CssClass="mr-r" />
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
                                <br />
                                <telerik:RadInputManager ID="RadInputManager1" runat="server">
                                    <telerik:NumericTextBoxSetting BehaviorID="NumericBehavior2" EmptyMessage="please type">
                                        <TargetControls>
                                            <telerik:TargetInput ControlID="txtAmount" />
                                        </TargetControls>
                                    </telerik:NumericTextBoxSetting>
                                </telerik:RadInputManager>
                                <telerik:RadInputManager ID="RadInputManager2" runat="server">
                                    <telerik:NumericTextBoxSetting BehaviorID="NumericBehavior2" EmptyMessage="please type">
                                        <TargetControls>
                                            <telerik:TargetInput ControlID="txtDPPAmount" />
                                        </TargetControls>
                                    </telerik:NumericTextBoxSetting>
                                </telerik:RadInputManager>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                &nbsp;
                            </td>
                            <td height="25" align="right">
                                <asp:Label ID="lblTotalViewJournal" Visible="false" runat="server" />
                            </td>
                            <td class="style9">
                                &nbsp;
                            </td>
                            <td align="right" class="style8">
                                <asp:Label ID="lblTotalDB" Text="0" Visible="false" runat="server" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td align="right">
                                <asp:Label ID="lblTotalCR" Text="0" Visible="false" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <input type="hidden" id="hidBlockCharge" value="" runat="server" />
                    <input type="hidden" id="hidChargeLocCode" value="" runat="server" />
                    <input type="hidden" id="HidPOCurrency" value="IDR" runat="server" />
                    <input type="hidden" id="hidPOExRate" value="1" runat="server" />
                    <input type="hidden" id="HidInvAmount" value="1" runat="server" />
                    <input type="hidden" id="hidOutstandingAmount" value="0" runat="server" />
                    <input type="hidden" id="hidOutstandingAmountKonversi" value="0" runat="server" />
                    <input type="hidden" id="hidUserID" value="" runat="server" />
                    <input type="hidden" id="hidNPWPNo" value="" runat="server" />
                    <input type="hidden" id="hidTaxObjectRate" value="0" runat="server" />
                    <input type="hidden" id="hidCOATax" value="0" runat="server" />
                    <input type="hidden" id="hidFindPOPPH23" value="0" runat="server" />
                    <input type="hidden" id="hidPOPPH23" value="0" runat="server" />
                    <input type="hidden" id="hidTaxStatus" value="1" runat="server" />
                    <input type="hidden" id="hidHadCOATax" value="0" runat="server" />
                    <input type="hidden" id="hidDocID" value="" runat="server" />
                    <input type="hidden" id="hidFindPOPPH21" value="0" runat="server" />
                    <input type="hidden" id="hidPOPPH21" value="0" runat="server" />
                    <input type="hidden" id="hidPrevID" value="" runat="server" />
                    <input type="hidden" id="hidPrevDate" value="0" runat="server" />
                    <input type="hidden" id="hidNextID" value="" runat="server" />
                    <input type="hidden" id="hidNextDate" value="0" runat="server" />&nbsp;<br />
                    <asp:TextBox ID="txtPPN" runat="server" Width="9%" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                    <asp:TextBox ID="txtCreditTerm" runat="server" Width="9%" BackColor="Transparent"
                        BorderStyle="None"></asp:TextBox>
                    <asp:TextBox ID="txtPPNInit" runat="server" Width="9%" BackColor="Transparent" BorderStyle="None"></asp:TextBox>&nbsp;
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
