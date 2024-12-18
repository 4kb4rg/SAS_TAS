<%@ Page Language="vb" Trace="false" CodeFile="../../../include/ap_trx_invrcv_wm_list.aspx.vb" Inherits="ap_trx_invrcv_wm_list" %>

<%@ Register TagPrefix="Preference" TagName="PrefHdl" Src="../../include/preference/preference_handler.ascx" %>

<%@ Register TagPrefix="qsf" Namespace="Telerik.QuickStart" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<html>
<head>
    <title>Weighing Credit Invoice List</title>
    <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
    <Preference:PrefHdl ID="PrefHdl" runat="server" />
</head>
<body style="margin: 0" onload="loadContent('InvoiceListing')" onsubmit="ShowLoading()">
    <form id="frmMain" runat="server" class="main-modul-bg-app-list-pu">
        <div class="kontenlist">
            <asp:Label ID="SortExpression" Visible="false" runat="server" />
            <asp:Label ID="SortCol" Visible="false" runat="server" />
            <asp:Label ID="lblErrMessage" Visible="false" Text="Error while initiating component." runat="server" />
            <input type="hidden" id="hidSearch" value="" runat="server" />
            <table border="0" cellspacing="1" cellpadding="1" width="100%" class="font9Tahoma">
                <tr>
                    <td><strong>WEIGHING CREDIT INVOICE LIST</strong><hr style="width: 100%" />
                    </td>

                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblTracker" runat="server" /></td>
                </tr>
                <tr>
                    <td colspan="6" width="100%" class="mb-c">
                        <table width="100%" cellspacing="0" cellpadding="3" border="0" align="center">
                            <tr class="font9Tahoma" style="background-color: #FFCC00">
                                <td width="14%" height="26">Location. :<br>
                                    <asp:DropDownList ID="srchlocation" Width="100%" runat="server" class="font9Tahoma" /></td>
                                <td width="14%" height="26">Weighing Inv ID. :<br>
                                    <asp:TextBox ID="srchTrxID" Width="100%" MaxLength="32" runat="server" /></td>
                                <td style="width: 15%">Weighing Ref No. :<br>
                                    <asp:TextBox ID="srchRefNo" Width="100%" MaxLength="20" runat="server" /></td>
                                <td valign="top" height="26" style="width: 16%">From :
                                    <telerik:RadDatePicker ID="srchDate" runat="server" Culture="en-US">
                                        <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>
                                        <DateInput DisplayDateFormat="dd/MMM/yyyy" DateFormat="dd/MMM/yyyy" EnableSingleInputRendering="True" LabelWidth="64px"></DateInput>
                                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                    </telerik:RadDatePicker>

                                    <br>
                                    To :<telerik:RadDatePicker ID="srchDateTo" runat="server" Culture="en-US">
                                        <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>
                                        <DateInput DisplayDateFormat="dd/MMM/yyyy" DateFormat="dd/MMM/yyyy" EnableSingleInputRendering="True" LabelWidth="64px"></DateInput>
                                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                    </telerik:RadDatePicker>
                                    <br />
                                </td>
                                <td width="20%">Supplier Code :<br>
                                    <asp:TextBox ID="srchSupplier" Width="100%" MaxLength="128" runat="server" /></td>
                                <td width="10%">Status :<br>
                                    <asp:DropDownList ID="srchStatusList" Width="100%" runat="server" /></td>
                                <td width="15%">Last Updated By :<br>
                                    <asp:TextBox ID="srchUpdateBy" Width="100%" MaxLength="128" runat="server" /></td>
                                <td width="10%" align="right" style="height: 56px">
                                    <asp:Button ID="SearchBtn" Text="Search" OnClick="srchBtn_Click" runat="server" class="button-small" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>

            </table>

            <table border="0" cellspacing="1" cellpadding="1" width="100%">
                <tr>
                    <td style="height: 24px;" colspan="5">
                        <ul class="tab">
                            <li>
                                <table cellpadding="0" cellspacing="0" style="width: 100%; background-color: #EEEEEE">
                                    <tr>
                                        <td><a href="#" class="tablinks" onclick="openContent(event, 'InvoiceListing')">Invoice Listing</a></td>
                                    </tr>
                                </table>
                            </li>
                            <li>
                                <table cellpadding="0" cellspacing="0" style="width: 100%; background-color: #EEEEEE">
                                    <tr>
                                        <td><a href="#" class="tablinks" onclick="openContent(event, 'InvoiceSummary')">Invoice Summary</a></td>
                                    </tr>
                                </table>
                            </li>
                            <li>
                                <table cellpadding="0" cellspacing="0" style="width: 100%; background-color: #EEEEEE">
                                    <tr>
                                        <td><a href="#" class="tablinks" onclick="openContent(event, 'InvoicePayment')">Invoice Payment</a></td>
                                    </tr>
                                </table>
                            </li>
                            <li>
                                <table cellpadding="0" cellspacing="0" style="width: 100%; background-color: #EEEEEE">
                                    <tr>
                                        <td><a href="#" class="tablinks" onclick="openContent(event, 'CoaListing')">COA Setting</a></td>
                                    </tr>
                                </table>
                            </li>
                        </ul>

                        <div id="InvoiceListing" class="tabcontent">
                            <table border="0" cellspacing="1" cellpadding="1" style="width: 100%; background-color: #EEEEEE">
                                <tr>
                                    <td colspan="6">
                                        <div id="div1" style="height: 400px; width: 100%; overflow: auto;">
                                            <asp:DataGrid ID="dgList"
                                                AutoGenerateColumns="false" Width="100%" runat="server"
                                                GridLines="none"
                                                CellPadding="4"
                                                CellSpacing="0"
                                                AllowCustomPaging="False"
                                                OnPageIndexChanged="OnPageChanged"
                                                OnItemDataBound="dgTicketList_BindGrid"
                                                PagerStyle-Visible="False"
                                                OnEditCommand="DEDR_Edit"
                                                OnUpdateCommand="DEDR_Update"
                                                OnDeleteCommand="DEDR_Delete"
                                                OnCancelCommand="DEDR_Cancel"
                                                OnSortCommand="Sort_Grid"
                                                class="font9Tahoma">

                                                <HeaderStyle CssClass="mr-h" Font-Bold="True" />
                                                <ItemStyle CssClass="mr-l" />
                                                <AlternatingItemStyle CssClass="mr-r" />

                                                <Columns>
                                                    <asp:HyperLinkColumn HeaderText="INVOICE ID"
                                                        SortExpression="TrxID"
                                                        DataNavigateUrlField="TrxID"
                                                        DataNavigateUrlFormatString="ap_trx_invrcv_wm_det.aspx?trxID={0}"
                                                        DataTextField="TrxID" />

                                                    <asp:TemplateColumn HeaderText="SUPPLIER" ItemStyle-Width="17%" SortExpression="SupplierCode">
                                                        <ItemTemplate>
                                                            <%#Container.DataItem("SupplierName")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>

                                                    <asp:TemplateColumn HeaderText="DATE <br> FROM" ItemStyle-Width="7%" SortExpression="DateParamFr">
                                                        <ItemTemplate>
                                                            <%# objGlobal.GetLongDate(Container.DataItem("DateParamFr")) %>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="DATE <br> TO" ItemStyle-Width="7%" SortExpression="DateParamTo">
                                                        <ItemTemplate>
                                                            <%# objGlobal.GetLongDate(Container.DataItem("DateParamTo")) %>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>

                                                    <asp:TemplateColumn HeaderText="QTY <br> (Kg)" ItemStyle-Width="5%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("NetWeight"), 0), 0)%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>


                                                    <asp:TemplateColumn HeaderText="DPP" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("DPPAmount"), 0), 0)%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>

                                                    <asp:TemplateColumn HeaderText="PPN" ItemStyle-Width="6%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PPNAmount"), 0), 0)%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>

                                                    <asp:TemplateColumn HeaderText="PPH" ItemStyle-Width="6%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PPHAmount"), 0), 0)%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>

                                                    <asp:TemplateColumn HeaderText="TOTAL" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("TotalAmount"), 0), 0)%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>

                                                    <asp:TemplateColumn HeaderText="STATUS" ItemStyle-Width="3%" SortExpression="W.Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStatus" Text='<%# objAPTrx.mtdGetInvoiceRcvStatus(Container.DataItem("Status")) %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>

                                                    <asp:TemplateColumn HeaderText="UPDATED BY" ItemStyle-Width="8%" SortExpression="U.UserName">
                                                        <ItemTemplate>
                                                            <%# Container.DataItem("UserName") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTrxID" Visible="false" Text='<%# Container.DataItem("TrxID") %>' runat="server" />
                                                            <asp:LinkButton ID="Edit" CommandName="Edit" Text="Edit" CausesValidation="False" runat="server" />
                                                            <asp:LinkButton ID="Update" CommandName="Update" Text="Update" CausesValidation="False" runat="server" />
                                                            <asp:LinkButton ID="Delete" CommandName="Delete" Text="Delete" CausesValidation="False" runat="server" />
                                                            <asp:LinkButton ID="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation="False" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderText="ACTION">
                                                        <ItemTemplate>
                                                            <asp:Button Text="Confirm" OnClick="BtnConfirm_Click" runat="server" class="button-small" ID="BtnConfirm" Font-Size="7pt" Width="56px" Height="26px" ToolTip="click confirm" />
                                                            <asp:Button Text="Cancel" OnClick="BtnCancel_Click" runat="server" class="button-small" ID="BtnCancel" Font-Size="7pt" Width="56px" Visible="False" Height="26px" ToolTip="click cancel" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid><br>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="6">
                                        <asp:ImageButton ID="btnPrev" Visible="false" runat="server" ImageUrl="../../images/icn_prev.gif" AlternateText="Previous" CommandArgument="prev" OnClick="btnPrevNext_Click" />
                                        <asp:DropDownList ID="lstDropList" Visible="false" AutoPostBack="True" OnSelectedIndexChanged="PagingIndexChanged" runat="server" />
                                        <asp:ImageButton ID="btnNext" Visible="false" runat="server" ImageUrl="../../images/icn_next.gif" AlternateText="Next" CommandArgument="next" OnClick="btnPrevNext_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:CheckBox ID="cbDetailByInvoice" class="font9Tahoma" Text=" Detail by Invoice" Checked="false" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:CheckBox ID="cbExcelListRekap" class="font9Tahoma" Text=" Preview Rekap" Checked="false" runat="server"  /></td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:CheckBox ID="cbExcelList" class="font9Tahoma" Text=" Export To Excel" Checked="true" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td colspan="5">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="left" width="100%" colspan="6">
                                        <asp:ImageButton ID="NewBtn" OnClick="NewBtn_Click" ImageUrl="../../images/butt_new.gif" AlternateText="New Ticket" runat="server" />
                                        <asp:ImageButton ID="GenInvBtn" ImageUrl="../../images/butt_generate.gif" AlternateText="Generate invoice" OnClick="BtnGenInv_Click" runat="server" />
                                        <asp:ImageButton ID="ibPrint" ImageUrl="../../images/butt_print.gif" AlternateText="Print" OnClick="btnPreview_Click" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="lblErrGenInv" ForeColor="red" Font-Italic="true" Visible="false" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div id="InvoiceSummary" class="tabcontent">
                            <table border="0" cellspacing="1" cellpadding="1" style="width: 100%; background-color: #EEEEEE">
                                <tr>
                                    <td colspan="5">
                                        <div id="div2" style="height: 300px; width: 100%; overflow: auto;">
                                            <asp:DataGrid ID="dgListSUM" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                CellPadding="2" Width="100%">
                                                <AlternatingItemStyle CssClass="mr-r" />
                                                <ItemStyle CssClass="mr-l" />
                                                <HeaderStyle CssClass="mr-h" />
                                                <Columns>
                                                    <asp:TemplateColumn HeaderText="No" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label Text='<%# Container.DataItem("NoUrut") %>' ID="lblNoUrut" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Name" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label Text='<%# Container.DataItem("NamaSupplier") %>' ID="lblNamaSpl" runat="server" />
                                                            <asp:Label Text='<%# Container.DataItem("KodeSupplier") %>' ID="lblKodeSpl" Visible="false" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Category" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label Text='<%# Container.DataItem("StatusSupplier") %>' ID="lblStatusSpl" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Tonase" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("NetWeight"), 2), 0) %>' ID="lblNetWeight" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="DPP" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("DPP"), 2), 0) %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="PPN" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PPN"), 2), 0) %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="PPh" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PPH"), 2), 0) %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Ongkos Bongkar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("OngkosBongkar"), 2), 0) %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Ongkos Lapangan" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("OngkosLapangan"), 2), 0) %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Total" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("TotalPembayaran"), 2), 0) %>' ID="lblTotalDiBayar" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:CheckBox ID="cbExcel" Text=" Export To Excel" Checked="false" Visible="false" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td colspan="5">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:ImageButton ID="PrintPrev" Visible="false" AlternateText="Print Preview" ImageUrl="../../images/butt_print_preview.gif" OnClick="btnPrintPrev_Click" runat="server" />
                                        <asp:ImageButton ID="btnGenerate" ToolTip="Generate journal" UseSubmitBehavior="false" OnClick="btnGenerate_Click" runat="server" ImageUrl="../../images/butt_generate.gif" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="lblErrGenerate" ForeColor="red" Font-Italic="true" Visible="false" runat="server" />
                                    </td>
                                </tr>

                            </table>
                        </div>

                        <div id="InvoicePayment" class="tabcontent">
                            <table border="0" cellspacing="1" cellpadding="1" style="width: 100%; background-color: #EEEEEE">
                                <tr>
                                    <td colspan="5">
                                        <div id="div3" style="height: 300px; width: 100%; overflow: auto;">
                                            <asp:DataGrid ID="dgListPay" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                CellPadding="2" Width="100%">
                                                <AlternatingItemStyle CssClass="mr-r" />
                                                <ItemStyle CssClass="mr-l" />
                                                <HeaderStyle CssClass="mr-h" />
                                                <Columns>
                                                    <asp:TemplateColumn HeaderText="No" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label Text='<%# Container.DataItem("NoUrut") %>' ID="lblNoUrut" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Nama" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label Text='<%# Container.DataItem("NamaSupplier") %>' ID="lblNamaSpl" runat="server" />
                                                            <asp:Label Text='<%# Container.DataItem("KodeSupplier") %>' ID="lblKodeSpl" Visible="false" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="NPWP" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label Text='<%# Container.DataItem("NPWPNO") %>' ID="lblNPWPNo" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Nama Rekening" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label Text='<%# Container.DataItem("BankAccName") %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="No. Rekening" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label2" Text='<%# Container.DataItem("BankAccNo") %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Bank" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label3" Text='<%# Container.DataItem("BankName") %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="DPP" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("DPP"), 2), 0) %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="PPN" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label4" Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PPN"), 2), 0) %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="PPh" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PPH"), 2), 0) %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Ongkos Bongkar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label5" Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("OngkosBongkar"), 2), 0) %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Ongkos Lapangan" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label6" Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("OngkosLapangan"), 2), 0) %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Pembayaran TBS <br> excl. PPN" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("TotalDiBayar"), 2), 0) %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Pembayaran TBS <br> incl. PPN" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("TotalPembayaran"), 2), 0) %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:CheckBox ID="cbExcelPay" Text=" Export To Excel" Checked="false" Visible="false" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td colspan="5">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:ImageButton ID="PrintPrevPay" Visible="false" AlternateText="Print Preview" ImageUrl="../../images/butt_print_preview.gif" OnClick="btnPrintPrevPay_Click" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="Label1" ForeColor="red" Font-Italic="true" Visible="false" runat="server" />
                                    </td>
                                </tr>

                            </table>
                        </div>

                        <div id="CoaListing" class="tabcontent">
                            <table border="0" cellspacing="1" cellpadding="1" style="width: 100%; background-color: #EEEEEE" class="font9Tahoma">
                                <tr>
                                    <td>
                                        <div id="font9Tahoma">
                                            <tr>
                                                <td height="25" width="15%">Pembelian TBS (Pemilik Kebun) :</td>
                                                <td width="50%">
                                                    <telerik:RadComboBox CssClass="fontObject" ID="radTbsPemilik"
                                                        runat="server" AllowCustomText="True"
                                                        EmptyMessage="Please Select Chart of Account" Height="200" Width="100%"
                                                        ExpandDelay="50" Filter="Contains" Sort="Ascending"
                                                        EnableVirtualScrolling="True">
                                                        <CollapseAnimation Type="InQuart" />
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="25">Pembelian TBS (Agen) :</td>
                                                <td>
                                                    <telerik:RadComboBox CssClass="fontObject" ID="radTbsAgen"
                                                        runat="server" AllowCustomText="True"
                                                        EmptyMessage="Please Select Chart of Account" Height="200" Width="100%"
                                                        ExpandDelay="50" Filter="Contains" Sort="Ascending"
                                                        EnableVirtualScrolling="True">
                                                        <CollapseAnimation Type="InQuart" />
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="25">PPN :</td>
                                                <td>
                                                    <telerik:RadComboBox CssClass="fontObject" ID="radTbsPPN"
                                                        runat="server" AllowCustomText="True"
                                                        EmptyMessage="Please Select Chart of Account" Height="200" Width="100%"
                                                        ExpandDelay="50" Filter="Contains" Sort="Ascending"
                                                        EnableVirtualScrolling="True">
                                                        <CollapseAnimation Type="InQuart" />
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="25">PPH :</td>
                                                <td>
                                                    <telerik:RadComboBox CssClass="fontObject" ID="radTbsPPH"
                                                        runat="server" AllowCustomText="True"
                                                        EmptyMessage="Please Select Chart of Account" Height="200" Width="100%"
                                                        ExpandDelay="50" Filter="Contains" Sort="Ascending"
                                                        EnableVirtualScrolling="True">
                                                        <CollapseAnimation Type="InQuart" />
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="25">Hutang SPTI Ongkos Bongkar TBS :</td>
                                                <td>
                                                    <telerik:RadComboBox CssClass="fontObject" ID="radTBSOBongkar"
                                                        runat="server" AllowCustomText="True"
                                                        EmptyMessage="Please Select Chart of Account" Height="200" Width="100%"
                                                        ExpandDelay="50" Filter="Contains" Sort="Ascending"
                                                        EnableVirtualScrolling="True">
                                                        <CollapseAnimation Type="InQuart" />
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="25">Hutang SPTI Ongkos Lapangan TBS :</td>
                                                <td>
                                                    <telerik:RadComboBox CssClass="fontObject" ID="radTbsOLapangan"
                                                        runat="server" AllowCustomText="True"
                                                        EmptyMessage="Please Select Chart of Account" Height="200" Width="100%"
                                                        ExpandDelay="50" Filter="Contains" Sort="Ascending"
                                                        EnableVirtualScrolling="True">
                                                        <CollapseAnimation Type="InQuart" />
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td height="25" style="width: 185px">
                                                    <asp:ImageButton ID="btnSaveSetting" ImageUrl="../../images/butt_save.gif" AlternateText="Save setting" OnClick="btnSaveSetting_Click" runat="server" /></td>
                                            </tr>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>

                    </td>
                </tr>
            </table>
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    </form>
</body>
<script type="text/javascript">  
    function ShowLoading(e) {
        var div = document.createElement('div');
        var img = document.createElement('img');
        img.src = '/../en/images/load2.gif';
        div.innerHTML = "<br />";
        div.style.cssText = 'position: fixed; top: 0%; left: 0%; z-index: auto; width: 100%; height: 100%; text-align: center; background-color:rgba(255, 255, 255, 0.8);';
        div.appendChild(img);
        document.body.appendChild(div);
        return true;
        // These 2 lines cancel form submission, so only use if needed.  
        //window.event.cancelBubble = true;  
        //e.stopPropagation();  
    }
</script>

<script>
    function loadContent(contentName) {
        var i, tabcontent, tablinks;
        tabcontent = document.getElementsByClassName("tabcontent");
        tabcontent[0].style.display = "block";
        tablinks = document.getElementsByClassName("tablinks");
        tablinks[0].className = tablinks[0].className.replace(" active", "");
        document.getElementById(contentName).style.display = "block";
    }

    function openContent(evt, contentName) {
        var i, tabcontent, tablinks;
        tabcontent = document.getElementsByClassName("tabcontent");

        for (i = 0; i < tabcontent.length; i++) {
            tabcontent[i].style.display = "none";
        }

        tablinks = document.getElementsByClassName("tablinks");

        for (i = 0; i < tablinks.length; i++) {
            tablinks[i].className = tablinks[i].className.replace(" active", "");
        }

        document.getElementById(contentName).style.display = "block";
        evt.currentTarget.className += " active";
    }
</script>

</html>
