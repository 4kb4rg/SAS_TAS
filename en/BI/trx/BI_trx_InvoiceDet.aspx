<%@ Page Language="vb" CodeFile="../../../include/BI_trx_InvoiceDet.aspx.vb" Inherits="BI_trx_InvoiceDet" %>

<%@ Register TagPrefix="UserControl" TagName="MenuBI" Src="../../menu/menu_bitrx.ascx" %>
<%@ Register TagPrefix="Preference" TagName="PrefHdl" Src="../../include/preference/preference_handler.ascx" %>
<%@ Register TagPrefix="qsf" Namespace="Telerik.QuickStart" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<html>
<head>
    <title>Invoice Details</title>

    <Preference:PrefHdl ID="PrefHdl" runat="server" />
    <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
    <script language="javascript">

        function pageLoad() {
            nqty = $find("<%=txtTotalUnits.ClientID%>");
            nharga = $find("<%=txtRate.ClientID%>");
            nsubtotal = $find("<%=txtAmount.ClientID%>");
            nppnamount = $find("<%=txtppnamount.ClientID%>");
            ntotal = $find("<%=txttotal.ClientID%>");

        }

        ; (function () {
            var hitung = window.hitung = {};

            hitung.valueChanged = function (sender, args) {
                var subtotal = nqty.get_value() * nharga.get_value()
                nsubtotal.set_value(subtotal);

                if (!chkVATExempted.checked) {
                    var taxMultiplier = 1.11;
                    var ppnamount = subtotal * (taxMultiplier - 1);
                    nppnamount.set_value(ppnamount);
                }
                else {
                    var ppnamount = 0;
                    nppnamount.set_value(ppnamount);
                }

                var total = ppnamount + subtotal;
                ntotal.set_value(total);
            }

            var bagi = window.bagi = {};
            var ppnkoreksi = window.ppnkoreksi = {};

            ppnkoreksi.valueChanged = function (sender, args) {
                var totalkoreksi = nppnamount.get_value() + nsubtotal.get_value();
                ntotal.set_value(totalkoreksi);
            }

            bagi.valueChanged = function (sender, args) {
                var unitcost = nsubtotal.get_value() / nqty.get_value()
                nharga.set_value(unitcost);
            }

        })();

        function calAmount(i) {
            var doc = document.frmMain;
            var a = parseFloat(doc.txtTotalUnits.value);
            var b = parseFloat(doc.txtRate.value);
            var c = parseFloat(doc.txtAmount.value);

            if ((i == '1') || (i == '2')) {
                if ((doc.txtTotalUnits.value != '') && (doc.txtRate.value != ''))
                    doc.txtAmount.value = round(a * b, 2);
                else
                    doc.txtAmount.value = '';
            }

            if (i == '3') {
                if ((doc.txtTotalUnits.value != '') && (doc.txtAmount.value != ''))
                    doc.txtRate.value = round(c / a, 4);
                else
                    doc.txtRate.value = '';
            }

            if (doc.txtTotalUnits.value == 'NaN')
                doc.txtTotalUnits.value = '';

            if (doc.txtRate.value == 'NaN')
                doc.txtRate.value = '';

            if (doc.txtAmount.value == 'NaN')
                doc.txtAmount.value = '';
        }
    </script>
    <style type="text/css">
        .style1 {
            width: 100%;
        }

        .style8 {
            width: 93px;
        }

        .style9 {
            width: 76px;
        }

        .style10 {
            width: 214px;
        }

        .style18 {
            width: 22%;
        }

        .auto-style1 {
            height: 28px;
            width: 22%;
        }

        .auto-style2 {
            width: 22%;
        }
    </style>
</head>
<body>
    <form id="frmMain" runat="server">

        <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
            <tr>
                <td style="width: 100%; height: 1500px" valign="top">
                    <div class="kontenlist">
                        <table id="tblHeader" cellspacing="0" cellpadding="2" width="100%" border="0" class="font9Tahoma">
                            <tr>
                                <td colspan="6">
                                    <UserControl:MenuBI ID="MenuBI" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <table cellpadding="0" cellspacing="0" class="style1">
                                        <tr>
                                            <td class="font9Tahoma">
                                                <strong>INVOICE DETAILS</strong></td>
                                            <td class="font9Header" style="text-align: right">Period :
                                                <asp:Label ID="lblAccPeriod" runat="server" />&nbsp;| Status :
                                                <asp:Label ID="lblStatus" runat="server" />&nbsp;| Date Created :
                                                <asp:Label ID="lblDateCreated" runat="server" />&nbsp;| Last Update :
                                                <asp:Label ID="lblLastUpdate" runat="server" />&nbsp;| Print Date :
                                                <asp:Label ID="lblPrintDate" runat="server" />&nbsp;| Updated By :<asp:Label ID="lblUpdatedBy" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                    <hr style="width: 100%" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">&nbsp;</td>
                            </tr>
                            <tr>
                                <td height="15" class="style18">Invoice ID :</td>
                                <td width="40%">
                                    <asp:Label ID="lblInvoiceID" runat="server" /></td>
                                <td width="5%">&nbsp;</td>
                                <td width="15%">&nbsp;</td>
                                <td width="20%">&nbsp;</td>
                                <td width="5%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td height="15" class="style18">Transaction Date :</td>
                                <td>
                                    <asp:TextBox ID="txtDateCreated" Width="25%" MaxLength="10" runat="server" CssClass="fontObject" />
                                    <a href="javascript:PopCal('txtDateCreated');">
                                        <asp:Image ID="btnDateCreated" ImageAlign="AbsMiddle" runat="server" ImageUrl="../../Images/calendar.gif" /></a>
                                    <asp:RequiredFieldValidator ID="rfvDateCreated" runat="server" ControlToValidate="txtDateCreated" Text="Please enter Date Created" Display="dynamic" />
                                    <asp:Label ID="lblDate" Text="<br>Date Entered should be in the format " ForeColor="red" Visible="false" runat="server" />
                                    <asp:Label ID="lblFmt" ForeColor="red" Visible="false" runat="server" />
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td height="15" class="style18">
                                    <asp:Label ID="lblBillParty" runat="server" />
                                    :*</td>
                                <td>
                                    <telerik:RadComboBox CssClass="fontObject" ID="radcmbCust" AutoPostBack="true"
                                        OnSelectedIndexChanged="onChanged_BillParty"
                                        runat="server" AllowCustomText="True"
                                        EmptyMessage="Plese Select Customer " Height="200" Width="100%"
                                        ExpandDelay="50" Filter="Contains" Sort="Ascending"
                                        EnableVirtualScrolling="True">
                                        <CollapseAnimation Type="InQuart" />
                                    </telerik:RadComboBox>
                                    <asp:Label ID="lblErrBillParty" Visible="false" ForeColor="red" runat="server" />
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td height="15" class="style18">
                                    <asp:Label ID="lblDocType" Text="Billing Type " runat="server" />:*</td>
                                <td>
                                    <asp:DropDownList ID="ddlDocType" Width="100%" runat="server" CssClass="fontObject" /></td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td height="15" class="style18">Customer Reference :</td>
                                <td>
                                    <asp:TextBox ID="txtCustRef" Width="100%" MaxLength="256" runat="server" CssClass="fontObject" />
                                    <asp:Label ID="lblCRErrRefNo" Visible="false" Text="Customer Reference is not unique" ForeColor="red" runat="server" /></td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>

                            <tr>
                                <td height="15" class="style18">Seller :</td>
                                <td>
                                    <asp:TextBox ID="txtSeller" Width="100%" MaxLength="128" runat="server" CssClass="fontObject" />
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td height="15" class="style18">Currency Code :</td>
                                <td>
                                    <asp:DropDownList ID="ddlCurrency" Width="100%" runat="server" CssClass="fontObject" /></td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td height="15" class="style18">Exchange Rate :</td>
                                <td>
                                    <asp:TextBox ID="txtExRate" Text="1" Width="20%" MaxLength="20" runat="server" CssClass="fontObject" /></td>
                            </tr>
                            <tr>
                                <td height="15" class="style18">Contract No. :</td>
                                <td>
                                    <asp:DropDownList ID="ddlContract" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="onChanged_ContractNo" runat="server" CssClass="fontObject" /></td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td height="15" class="style18">Advance Received Reference :</td>
                                <td>
                                    <asp:TextBox ID="txtDevRef" Width="20%" MaxLength="128" runat="server" CssClass="fontObject" />
                                    <asp:CheckBox ID="chkIsAdvanceRcv" Checked="false" runat="server" AutoPostBack="true" OnCheckedChanged="AdvanceReceive_Change" />
                                    <asp:LinkButton ID="LnkAdvanceRcvNo" runat="server"></asp:LinkButton>
                                    <asp:Label ID="lblDLErrRefNo" Visible="false" Text="Delivery Reference is not unique" ForeColor="red" runat="server" /></td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>

                            <tr>
                                <td height="15" class="style18">VAT Exemption (Pengecualian Pajak)</td>
                                <td>
                                    <asp:CheckBox ID="chkVATExempted" Text="" onclick="hitung.valueChanged();" runat="server" /></td>
                                <td>&nbsp;</td>
                            </tr>
                             

                            <tr>
                                <td height="15" class="style18">Invoice Type :</td>
                                <td>
                                    <asp:DropDownList ID="ddlInvoicetype" Width="100%" OnSelectedIndexChanged="Closed_Changed" AutoPostBack="true" runat="server" CssClass="fontObject">
										<asp:ListItem value="2">Advance Payment</asp:ListItem>
                                        <asp:ListItem value="3"  Selected>Sales</asp:ListItem>
										<asp:ListItem value="1">Set Closed</asp:ListItem> 
                                    </asp:DropDownList>    
                                 </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr style="visibility: hidden">
                                <td height="15" class="style18">Advance Payment (excl. vat) :</td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtAdvAmount" CssClass="fontObject" Width="35%" runat="server" LabelWidth="64px">
                                        <NumberFormat ZeroPattern="n"></NumberFormat>
                                        <EnabledStyle HorizontalAlign="Right" />
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6" style="height: 23px">&nbsp;</td>
                            </tr>

                        </table>
                        <table cellspacing="0" cellpadding="2" width="100%" border="0" runat="server">
                            <tr>
                                <td colspan="5">
                                    <table id="tblSelection" cellspacing="0" cellpadding="4" width="100%" border="0" runat="server" class="sub-Add">
                                        <tr class="mb-c">
                                            <td class="auto-style1">
                                                <asp:Label ID="lblAccount" runat="server" />
                                                :* </td>
                                            <td colspan="5" style="height: 28px">
                                                <telerik:RadComboBox CssClass="fontObject" ID="radcmbCOA" AutoPostBack="true"
                                                    OnSelectedIndexChanged="onSelect_Account"
                                                    runat="server" AllowCustomText="True"
                                                    EmptyMessage="Please Select COA" Height="200" Width="95%"
                                                    ExpandDelay="50" Filter="Contains" Sort="Ascending"
                                                    EnableVirtualScrolling="True" auto>
                                                    <CollapseAnimation Type="InQuart" />
                                                </telerik:RadComboBox>

                                                <asp:Label ID="lblErrAccCode" Visible="false" ForeColor="red" runat="server" />
                                            </td>
                                        </tr>
                                        <tr id="RowChargeLevel" class="mb-c">
                                            <td height="25" class="auto-style2">Charge Level :* </td>
                                            <td colspan="5">
                                                <asp:DropDownList ID="ddlChargeLevel" CssClass="fontObject" Width="95%" AutoPostBack="True" OnSelectedIndexChanged="ddlChargeLevel_OnSelectedIndexChanged" runat="server" />
                                            </td>
                                        </tr>
                                        <tr id="RowPreBlk" class="mb-c">
                                            <td height="25" class="auto-style2">
                                                <asp:Label ID="lblPreBlkTag" runat="server" />
                                            </td>
                                            <td colspan="5">
                                                <asp:DropDownList ID="ddlPreBlock" Width="95%" CssClass="fontObject" runat="server" />
                                                <asp:Label ID="lblPreBlockErr" Visible="False" ForeColor="red" runat="server" /></td>
                                        </tr>
                                        <tr id="RowBlk" class="mb-c">
                                            <td height="25" class="auto-style2">
                                                <asp:Label ID="lblBlock" runat="server" />
                                                :</td>
                                            <td colspan="5">
                                                <asp:DropDownList ID="ddlBlock" Width="95%" CssClass="fontObject" runat="server" />
                                                <asp:Label ID="lblErrBlock" Visible="false" ForeColor="red" runat="server" />
                                            </td>
                                        </tr>
                                        <tr class="mb-c">
                                            <td height="25" class="auto-style2">
                                                <asp:Label ID="lblVehicle" runat="server" />
                                                :</td>
                                            <td colspan="5">
                                                <asp:DropDownList ID="ddlVehCode" Width="95%" CssClass="fontObject" runat="server" />
                                                <asp:Label ID="lblErrVehicle" Visible="false" ForeColor="red" runat="server" />
                                            </td>
                                        </tr>
                                        <tr class="mb-c">
                                            <td height="25" class="auto-style2">
                                                <asp:Label ID="lblVehExpense" runat="server" />
                                                :</td>
                                            <td colspan="5">
                                                <asp:DropDownList ID="ddlVehExpCode" Width="95%" CssClass="fontObject" runat="server" />
                                                <asp:Label ID="lblErrVehicleExp" Visible="false" ForeColor="red" runat="server" />
                                            </td>
                                        </tr>
                                        <tr class="mb-c">
                                            <td height="25" class="auto-style2">Item Description :*</td>
                                            <td colspan="5">
                                                <asp:TextBox ID="txtDescription" Width="95%" MaxLength="128" CssClass="fontObject" runat="server" />
                                                <asp:Label ID="lblErrDesc" Visible="false" ForeColor="red" Text="Please enter Item description." runat="server" />
                                            </td>
                                        </tr>
                                        <tr class="mb-c">
                                            <td height="25" class="auto-style2">Quantity (KG) :*</td>
                                            <td width="20%">
                                                <telerik:RadNumericTextBox ID="txtTotalUnits" CssClass="fontObject" Width="70%" ClientEvents-OnValueChanged="hitung.valueChanged" runat="server" LabelWidth="64px">
                                                    <NumberFormat ZeroPattern="n"></NumberFormat>
                                                    <EnabledStyle HorizontalAlign="Right" />
                                                </telerik:RadNumericTextBox>
                                                <asp:Label ID="lblErrTotalUnits" Visible="false" ForeColor="red" Text="Please enter Quantity" runat="server" />
                                            </td>
                                            <td height="25" width="10%" align="center">X Unit Price *</td>
                                            <td width="20%">
                                                <telerik:RadNumericTextBox ID="txtRate" CssClass="fontObject" Width="70%" ClientEvents-OnValueChanged="hitung.valueChanged" runat="server" LabelWidth="64px">
                                                    <NumberFormat ZeroPattern="n"></NumberFormat>
                                                    <EnabledStyle HorizontalAlign="Right" />
                                                </telerik:RadNumericTextBox>
                                                <asp:Label ID="lblErrRate" Visible="false" ForeColor="red" Text="Please enter Rate" runat="server" />
                                            </td>
                                            <td height="25" width="10%" align="center">Sub Total *</td>
                                            <td width="20%">
                                                <telerik:RadNumericTextBox ID="txtAmount" CssClass="fontObject" Width="77%" ReadOnly="true" runat="server" LabelWidth="64px">
                                                    <NumberFormat ZeroPattern="n"></NumberFormat>
                                                    <EnabledStyle HorizontalAlign="Right" />
                                                </telerik:RadNumericTextBox>
                                                <asp:Label ID="lblErrAmount" Visible="false" ForeColor="red" Text="Please enter Amount" runat="server" />
                                            </td>
                                        </tr>
                                        <tr class="mb-c">
                                            <td height="25" class="auto-style2"></td>
                                            <td width="20%"></td>
                                            <td height="25" width="10%" align="center"></td>
                                            <td width="20%"></td>
                                            <td height="25" width="10%" align="center">PPN Amount </td>
                                            <td width="20%">
                                                <telerik:RadNumericTextBox ID="txtppnamount" CssClass="fontObject" Width="77%" ClientEvents-OnValueChanged="ppnkoreksi.valueChanged" runat="server" LabelWidth="64px">
                                                    <NumberFormat ZeroPattern="n"></NumberFormat>
                                                    <EnabledStyle HorizontalAlign="Right" />
                                                </telerik:RadNumericTextBox>
                                            </td>
                                        </tr>
                                        <tr class="mb-c">
                                            <td height="25" class="auto-style2"></td>
                                            <td width="20%"></td>
                                            <td height="25" width="10%" align="center"></td>
                                            <td width="20%"></td>
                                            <td height="25" width="10%" align="center">Total Amount *</td>
                                            <td width="20%">
                                                <telerik:RadNumericTextBox ID="txttotal" CssClass="fontObject" Font-Bold="true" Width="77%" ReadOnly="true" runat="server" LabelWidth="64px">
                                                    <NumberFormat ZeroPattern="n"></NumberFormat>
                                                    <EnabledStyle HorizontalAlign="Right" />
                                                </telerik:RadNumericTextBox>
                                            </td>
                                        </tr>
                                        <!--<tr class="mb-c">
															<td height=25>PPN :</td>
															<td><asp:CheckBox ID=cbPPN text=" Yes"  OnCheckedChanged=reCalculate_Amount  AutoPostBack=true Runat=server /></td>
															<td  style="visibility:hidden;">PPh 23/26 Rate :</td>
															<td  style="visibility:hidden;"><asp:Textbox id=txtPPHRate width=50% maxlength=5 runat=server/>
															<asp:Label id=lblPercen text='%' runat=server/>
															<asp:CompareValidator id="cvPPHRate" display=dynamic runat="server" 
																	ControlToValidate="txtPPHRate" Text="<br>The value must whole number or with decimal. " 
																	Type="Double" Operator="DataTypeCheck"/>
															<asp:RegularExpressionValidator id=revPPHRate 
																	ControlToValidate="txtPPHRate"
																	ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
																	Display="Dynamic"
																	text = "<br>Maximum length 2 digits and 2 decimal points. "
																	runat="server"/>
															</td>
															<td>&nbsp;</td>	
															<td>&nbsp;</td>	
														</tr>-->
                                        <tr class="mb-c">
                                            <td height="25" colspan="6">
                                                <asp:ImageButton ID="AddBtn" ImageUrl="../../images/butt_add.gif" AlternateText="Add" OnClick="AddBtn_Click" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5">
                                    <asp:DataGrid ID="dgLineDet"
                                        AutoGenerateColumns="false" Width="100%" runat="server"
                                        GridLines="none"
                                        CellPadding="2"
                                        OnEditCommand="DEDR_Edit"
                                        OnDeleteCommand="DEDR_Delete"
                                        PagerStyle-Visible="False"
                                        AllowSorting="True">

                                        <HeaderStyle CssClass="mr-h" />
                                        <ItemStyle CssClass="mr-l" />
                                        <AlternatingItemStyle CssClass="mr-r" />
                                        <Columns>
                                            <asp:TemplateColumn ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# Container.DataItem("AccCode") %>' ID="lblAccCode" runat="server" />
                                                    <asp:Label Visible="false" Text='<%# Container.DataItem("InvoiceLnID") %>' ID="lblInvoiceLnID" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Acc Description" ItemStyle-Width="15%">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# Container.DataItem("AccDescr") %>' ID="lblAccDescr" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# Container.DataItem("BlkCode") %>' ID="lblBlkCode" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Item Description" ItemStyle-Width="15%">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# Container.DataItem("Description") %>' ID="lblDescription" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Quantity" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Unit"), 2), 2) %>' ID="lblViewUnit" runat="server" />
                                                    <asp:Label Text='<%# Container.DataItem("Unit") %>' ID="lblUnit" Visible="False" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Unit Price" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("CostToDisplay"), 2), 2) %>' ID="lblViewCost" runat="server" />
                                                    <asp:Label Text='<%# Container.DataItem("Cost") %>' ID="lblCost" Visible="False" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Amount" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("NetAmountToDisplay"), 2), 2) %>' ID="lblViewNetAmount" runat="server" />
                                                    <asp:Label Text='<%# Container.DataItem("NetAmount") %>' ID="lblNetAmount" Visible="False" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="PPN" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PPNAmountToDisplay"), 2), 2) %>' ID="lblViewPPNAmount" runat="server" />
                                                    <asp:Label Text='<%# FormatNumber(Container.DataItem("PPNAmount"), 2) %>' ID="lblPPNAmount" Visible="False" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="PPH 23/26" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PPHAmountToDisplay"), 2), 2) %>' ID="lblViewPPHAmount" runat="server" />
                                                    <asp:Label Text='<%# FormatNumber(Container.DataItem("PPHAmount"), 2) %>' ID="lblPPHAmount" Visible="False" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Total Amount" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("AmountToDisplay"), 2), 2) %>' ID="lblViewAmount" runat="server" />
                                                    <asp:Label Text='<%# FormatNumber(Container.DataItem("Amount"), 2) %>' ID="lblAmount" Visible="False" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="IVlnid" Visible="false" Text='<%# Container.DataItem("InvoiceLNID")%>' runat="server" />
                                                    <asp:LinkButton ID="lbEdit" CommandName="Edit" Text="Edit" runat="server" />
                                                    <asp:LinkButton ID="lbDelete" CommandName="Delete" Text="Delete" runat="server" />
                                                    <asp:LinkButton ID="lbCancel" CommandName="Cancel" Text="Cancel" CausesValidation="False" runat="server" />
                                                    <asp:Label Text='<%# Container.DataItem("VehCode") %>' ID="lblVehCode" Visible="false" runat="server" />
                                                    <asp:Label Text='<%# Container.DataItem("VehExpenseCode") %>' ID="lblVehExpenseCode" Visible="false" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" class="style9">&nbsp;</td>
                                <td colspan="3" height="25">
                                    <hr style="width: 100%" />
                                </td>
                            </tr>
                            <tr class="font9Tahoma">
                                <td colspan="2" class="style9">&nbsp;</td>
                                <td height="25">Total Amount : </td>
                                <td align="right">
                                    <asp:Label ID="lblCurrency" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblViewTotalAmount" runat="server" />&nbsp;</td>
                                <td align="right">
                                    <asp:Label ID="lblTotalAmount" Visible="False" runat="server" />&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr class="font9Tahoma">
                                <td heigth="25" class="style8">Remarks:</td>
                                <td colspan="4">
                                    <asp:TextBox ID="txtRemark" TextMode="MultiLine" MaxLength="256" Width="100%" runat="server" CssClass="fontObject" /></td>
                            </tr>

                            <tr>
                                <td colspan="5">&nbsp;</td>
                            </tr>


                        </table>
                        <table cellspacing="0" cellpadding="2" width="100%" border="0" class="font9Tahoma">
                            <tr>
                                <td class="style10">
                                <tr>
                                    <td height="25" valign="top" class="style10">Undersigned Name :*</td>
                                    <td>
                                        <asp:TextBox ID="txtUnderName" Text="" MaxLength="128" Width="50%" runat="server" CssClass="fontObject" /></td>
                                    <td>&nbsp;</td>
                                </tr>
                            <tr>
                                <td height="25" valign="top" class="style10">Undersigned Post :*</td>
                                <td>
                                    <asp:TextBox ID="txtUnderPost" Text="" MaxLength="128" Width="50%" runat="server" CssClass="fontObject" /></td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="6">&nbsp;</td>
                            </tr>
                            <tr visible="False">
                                <td height="25" valign="top" class="style10">Faktur Pajak No. :*</td>
                                <td>
                                    <asp:TextBox ID="txtFakturNo" Text="" MaxLength="128" Width="50%" runat="server" CssClass="fontObject" /></td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr visible="False">
                                <td height="25" valign="top" class="style10">Faktur Pajak Date :*</td>
                                <td>
                                    <asp:TextBox ID="txtFakturDate" Width="25%" MaxLength="10" ReadOnly="true" runat="server" CssClass="fontObject" />
                                    <a href="javascript:PopCal('txtFakturDate');">
                                        <asp:Image ID="Image1" ImageAlign="AbsMiddle" runat="server" ImageUrl="../../Images/calendar.gif" /></a>
                                    <asp:Label ID="lblDateFaktur" Text="<br>Date Entered should be in the format " ForeColor="red" Visible="false" runat="server" />
                                    <asp:Label ID="lblFmtFaktur" ForeColor="red" Visible="false" runat="server" />
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr visible="False">
                                <td class="style10"></td>
                                <td colspan="5">
                                    <asp:RadioButton ID="Opt1" Enabled="false" runat="server" TextAlign="Right" Text=" Lembar ke-1   :   Untuk pembeli BKP/penerima JKP sebagai bukti pajak masukan" GroupName="Option" Checked="True" /><br>
                                    <asp:RadioButton ID="Opt2" Enabled="false" runat="server" TextAlign="Right" Text=" Lembar ke-2   :   Untuk PKP yang menerbitkan Faktur Pajak Standar sebagai bukti pajak keluaran" GroupName="Option" /><br>
                                    <asp:RadioButton ID="Opt3" Enabled="false" runat="server" TextAlign="Right" Text=" Lembar ke-3   :   Arsip" GroupName="Option" /></td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="6">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:CheckBox ID="cbExcel" Text=" Export To Excel" Checked="false" runat="server" /></td>
                            </tr>
                            <tr>
                                <td colspan="6">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <asp:Label ID="lblErrTotal" ForeColor="red" Visible="false" Text="There was no amount to bill.<br>" runat="server" />
                                    <asp:ImageButton ID="NewBtn" runat="server" AlternateText="New Invoice" ImageUrl="../../images/butt_new.gif"
                                        OnClick="NewBtn_Click" />
                                    <asp:ImageButton ID="SaveBtn" OnClick="SaveBtn_Click" ImageUrl="../../images/butt_save.gif" AlternateText="Save" runat="server" />
                                    <asp:ImageButton ID="ConfirmBtn" OnClick="ConfirmBtn_Click" ImageUrl="../../images/butt_confirm.gif" AlternateText="Confirm" runat="server" />
                                    <asp:ImageButton ID="PrintBtn" OnClick="PrintBtn_Click" ImageUrl="../../images/butt_print.gif" AlternateText="Print" runat="server" />
                                    <asp:ImageButton ID="PrintFaktur" OnClick="PrintFaktur_Click" ImageUrl="../../images/butt_print_faktur2.gif" AlternateText="Print" runat="server" />
                                    <asp:ImageButton ID="PrintKwitansiBtn" UseSubmitBehavior="false" OnClick="PreviewKwitansiBtn_Click" CausesValidation="false" ImageUrl="../../images/butt_print_kwitansi.gif" AlternateText="Print Kwitansi" runat="server" />
                                    <asp:ImageButton ID="EditBtn" UseSubmitBehavior="false" AlternateText="Edit" OnClick="EditBtn_Click" ImageUrl="../../images/butt_edit.gif" CausesValidation="False" runat="server" />
                                    <asp:ImageButton ID="CancelBtn" OnClick="CancelBtn_Click" ImageUrl="../../images/butt_cancel.gif" Text=" Cancel " runat="server" />
                                    <asp:ImageButton ID="DeleteBtn" CausesValidation="False" OnClick="DeleteBtn_Click" ImageUrl="../../images/butt_delete.gif" AlternateText="Delete" runat="server" />
                                    <asp:ImageButton ID="UnDeleteBtn" OnClick="UnDeleteBtn_Click" ImageUrl="../../images/butt_undelete.gif" AlternateText="Undelete" runat="server" />
                                    <asp:ImageButton ID="BackBtn" CausesValidation="False" OnClick="BackBtn_Click" ImageUrl="../../images/butt_back.gif" AlternateText="Back" runat="server" />
                                    <input type="hidden" id="IVid" value="" runat="server" />
                                    <input type="hidden" id="hidPPN" value="" runat="server" />

                                </td>
                            </tr>
                            <asp:Label ID="lblErrMessage" Visible="false" Text="Error while initiating component." runat="server" /><asp:Label ID="lblCode" Visible="false" Text=" Code" runat="server" /><asp:Label ID="lblPleaseSelect" Visible="false" Text="Please select " runat="server" /><asp:Label ID="lblSelect" Visible="false" Text="Select " runat="server" /><asp:Label ID="lblStatusHidden" Visible="false" runat="server" /><asp:Label ID="lblDocTypeHidden" Visible="false" runat="server" /><asp:Label ID="lblVehicleOption" Visible="false" Text="false" runat="server" /><asp:Label ID="lblReferer" Visible="false" Text="" runat="server" /><tr>
                                <td colspan="5">&nbsp;</td>
                            </tr>
                            <tr id="TrLink" visible="false" runat="server">
                                <td colspan="5">
                                    <asp:LinkButton ID="lbViewJournal" Text="View Journal Predictions" CausesValidation="false" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <asp:DataGrid ID="dgViewJournal"
                                        AutoGenerateColumns="false" Width="100%" runat="server"
                                        GridLines="none"
                                        CellPadding="1"
                                        PagerStyle-Visible="False"
                                        AllowSorting="false">
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
                                                    <asp:Label Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("AmountDB"), 2), 2) %>' ID="lblAmountDB" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Credit">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" Width="20%" />
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("AmountCR"), 2), 2) %>' ID="lblAmountCR" runat="server" />
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
                                <td colspan="5">&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td height="25" align="right">
                                    <asp:Label ID="lblTotalViewJournal" Visible="false" runat="server" />
                                </td>
                                <td>&nbsp;</td>
                                <td align="right">
                                    <asp:Label ID="lblTotalDB" Text="0" Visible="false" runat="server" /></td>
                                <td>&nbsp;</td>
                                <td align="right">
                                    <asp:Label ID="lblTotalCR" Text="0" Visible="false" runat="server" /></td>
                            </tr>

                        </table>
                        <input type="hidden" id="hidBlockCharge" value="" runat="server" />
                        <input type="hidden" id="hidChargeLocCode" value="" runat="server" />
                        <input type="hidden" id="hidProdCode" value="" runat="server" />
                        <input type="hidden" id="hidProdType" value="" runat="server" />
                        <input type="hidden" id="hidIRLnID" value="" runat="server" />
                        <input type="hidden" id="hidPPNValue" value="0" runat="server" />
                        <input type="hidden" id="hidReceiptID" value="" runat="server" />
                        <input type="hidden" id="hidRate" value="0" runat="server" />
                    </div>
                </td>
            </tr>
        </table>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    </form>
</body>
</html>
