<%@ Page Language="vb" Src="../../../include/ap_trx_InvRcvList.aspx.vb" Inherits="ap_trx_InvRcvList" %>

<%@ Register TagPrefix="UserControl" TagName="MenuAP" Src="../../menu/menu_aptrx.ascx" %>
<%@ Register TagPrefix="Preference" TagName="PrefHdl" Src="../../include/preference/preference_handler.ascx" %>
<html>
<head>
    <title>Invoice Receive List</title>
    <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
    <Preference:PrefHdl ID="PrefHdl" runat="server" />
    <style type="text/css">
        .auto-style1 {
            height: 41px;
        }
    </style>
</head>
<body>
    <form id="frmInvoiceList" class="main-modul-bg-app-list-pu" runat="server">
        <asp:Label ID="SortExpression" Visible="False" runat="server" />
        <asp:Label ID="SortCol" Visible="False" runat="server" />
        <asp:Label ID="lblErrMesage" Visible="false" Text="Error while initiating component." runat="server" />
        <asp:Label ID="lblID" Visible="false" Text=" ID" runat="server" />
        <table border="0" cellspacing="1" cellpadding="1" width="100%" class="font9Tahoma">
            <tr>
                <td colspan="6" class="auto-style1">
                    <UserControl:MenuAP ID="MenuAP" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 100%; height: 800px" valign="top">
                    <div class="kontenlist">
                        <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">

                            <tr>
                                <td class="mt-h" colspan="3"><strong>
                                    <asp:Label ID="lblTitle" runat="server" />
                                    LIST</strong>  </td>
                                <td colspan="3" align="right">
                                    <asp:Label ID="lblTracker" runat="server" /></td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <hr style="width: 100%" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6" width="100%" class="mb-c">
                                    <table width="100%" cellspacing="0" cellpadding="3" border="0" align="center" class="font9Tahoma" style="background-color: #FFCC00">
                                        <tr class="mb-t">
                                            <td valign="bottom" width="15%">
                                                <asp:Label ID="lblInvReceiveID" runat="server" />
                                                :<br>
                                                <asp:TextBox ID="txtInvoiceID" Width="100%" MaxLength="32" CssClass="fontObject" runat="server" /></td>
                                            <td valign="bottom" width="20%">POID :<br>
                                                <asp:TextBox ID="txtPOID" Width="100%" MaxLength="32" CssClass="fontObject" runat="server" /></td>
                                            <td valign="bottom" width="25%">Supplier :<br>
                                                <asp:TextBox ID="txtSupplier" Width="100%" MaxLength="20" CssClass="fontObject" runat="server" /></td>
                                            <td valign="bottom" width="10%">Invoice Type :<br>
                                                <asp:DropDownList ID="ddlInvoiceType" Width="100%" CssClass="fontObject" runat="server" /></td>
                                            <td valign="bottom" width="10%">Period :<br>
                                                <asp:DropDownList ID="lstAccMonth" Width="100%" CssClass="fontObject" runat="server">
                                                    <asp:ListItem Value="0" Selected>All</asp:ListItem>
                                                    <asp:ListItem Value="1">1</asp:ListItem>
                                                    <asp:ListItem Value="2">2</asp:ListItem>
                                                    <asp:ListItem Value="3">3</asp:ListItem>
                                                    <asp:ListItem Value="4">4</asp:ListItem>
                                                    <asp:ListItem Value="5">5</asp:ListItem>
                                                    <asp:ListItem Value="6">6</asp:ListItem>
                                                    <asp:ListItem Value="7">7</asp:ListItem>
                                                    <asp:ListItem Value="8">8</asp:ListItem>
                                                    <asp:ListItem Value="9">9</asp:ListItem>
                                                    <asp:ListItem Value="10">10</asp:ListItem>
                                                    <asp:ListItem Value="11">11</asp:ListItem>
                                                    <asp:ListItem Value="12">12</asp:ListItem>
                                                </asp:DropDownList>
                                            <td valign="bottom" width="10%">
                                                <br>
                                                <asp:DropDownList ID="lstAccYear" Width="100%" CssClass="fontObject" runat="server">
                                                </asp:DropDownList>
                                            <td valign="bottom" width="12%">Status :<br>
                                                <asp:DropDownList ID="ddlStatus" Width="100%" CssClass="fontObject" runat="server" /></td>
                                            <td valign="bottom" width="15%">
                                                <br>
                                                <asp:TextBox ID="txtLastUpdate" Width="100%" MaxLength="128" Visible="false" CssClass="fontObject" runat="server" /></td>
                                            <td valign="bottom" width="8%" align="right">
                                                <asp:Button ID="SearchBtn" Text="Search" OnClick="srchBtn_Click" CssClass="button-small" runat="server" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <asp:DataGrid ID="dgInvList"
                                        AutoGenerateColumns="false" Width="100%" runat="server"
                                        GridLines="none"
                                        CellPadding="3"
                                        CellSpacing="1"
                                        AllowPaging="true"
                                        AllowCustomPaging="False"
                                        PageSize="15"
                                        OnItemCommand="MenuRefLink_Click"
                                        OnPageIndexChanged="OnPageChanged"
                                        PagerStyle-Visible="false"
                                        OnSortCommand="Sort_Grid"
                                        AllowSorting="True" CssClass="font9Tahoma">
                                        <HeaderStyle CssClass="mr-h" />
                                        <ItemStyle CssClass="mr-l" />
                                        <AlternatingItemStyle CssClass="mr-r" />
                                        <Columns>

                                            <asp:HyperLinkColumn SortExpression="INV.InvoiceRcvID"
                                                DataNavigateUrlField="InvoiceRcvID"
                                                DataNavigateUrlFormatString="ap_trx_InvRcvDet.aspx?inrid={0}"
                                                DataTextField="InvoiceRcvID" />
                                            <asp:TemplateColumn HeaderText="Supplier Name" SortExpression="SUPP.SupplierName">
                                                <ItemTemplate>
                                                    <%# Container.DataItem("SupplierName") %>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="Purchase Ref No <br> Goods Rcv Ref No" ItemStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkPOID" CommandName="POID" Text='<%# Container.DataItem("Poid") %>' runat="server" ToolTip="click to view purchase detail" />
                                                    <asp:Label ID="lblPOID" Text='<%# Container.DataItem("POid") %>' Visible="False" runat="server" /><br />
                                                    <asp:LinkButton ID="lnkGRID" CommandName="GRID" Text='<%# Container.DataItem("GRID") %>' runat="server" ToolTip="click to view purchase detail" />
                                                    <asp:Label ID="lblGRID" Text='<%# Container.DataItem("GRID") %>' Visible="False" runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Invoice Ref No" SortExpression="NT.TrxID">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkInvoiceRef" CommandName="INVREF" Text='<%# Container.DataItem("TrxID") %>' runat="server" ToolTip="click to view detail invoice reception" />
                                                    <asp:Label ID="lblInvoiceRef" Text='<%# Container.DataItem("TrxID") %>' Visible="False" runat="server" />                                             
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="TotalAmount" SortExpression="TotalAmount" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("TotalAmount"), 2), 2) %>' ID="lblTotalAmount" runat="server" />
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Right"></HeaderStyle>

                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="Invoice Type <br> Due Date" SortExpression="INV.DueDate">
                                                <ItemTemplate>
                                                     <%# objAPTrx.mtdGetInvoiceType(Container.DataItem("InvoiceType")) %><br />
                                                    <%#objGlobal.GetLongDate(Container.DataItem("DueDate"))%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Payment Ref No" SortExpression="NT.TrxID">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkPaymentID" CommandName="PAYMENTID" Text='<%# Container.DataItem("PaymentID") %>' runat="server" ToolTip="click to view detail payment" />
                                                    <asp:Label ID="lblPaymentID" Text='<%# Container.DataItem("PaymentID") %>' Visible="False" runat="server" />                                                                                                       
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Last Update" SortExpression="INV.UpdateDate">
                                                <ItemTemplate>
                                                    <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Status" SortExpression="INV.Status">
                                                <ItemTemplate>
                                                    <%# objAPTrx.mtdGetInvoiceRcvStatus(Container.DataItem("Status")) %>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Updated By" SortExpression="USR.UserName">
                                                <ItemTemplate>
                                                    <asp:Label ID="idInvoiceRcvId" Visible="false" Text='<%# Container.DataItem("InvoiceRcvID")%>' runat="server" />
                                                    <asp:Label ID="idPOID" Visible="false" Text='<%# Container.DataItem("POID")%>' runat="server" />
                                                    <%# Container.DataItem("UserName") %>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>

                                        <PagerStyle Visible="False"></PagerStyle>
                                    </asp:DataGrid><br>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="6">
                                    <asp:ImageButton ID="btnPrev" runat="server" ImageUrl="../../images/icn_prev.gif" AlternateText="Previous" CommandArgument="prev" OnClick="btnPrevNext_Click" />
                                    <asp:DropDownList ID="lstDropList" AutoPostBack="True" OnSelectedIndexChanged="PagingIndexChanged" CssClass="fontObject" runat="server" />
                                    <asp:ImageButton ID="btnNext" runat="server" ImageUrl="../../images/icn_next.gif" AlternateText="Next" CommandArgument="next" OnClick="btnPrevNext_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" width="100%" colspan="6">
                                    <asp:ImageButton ID="NewInvRcv" Visible="false" UseSubmitBehavior="false" OnClick="NewInvRcv_Click" ImageUrl="../../images/butt_new.gif" AlternateText="New Invoice Receive" runat="server" />
                                    <asp:ImageButton ID="ibPrint" UseSubmitBehavior="false" ImageUrl="../../images/butt_print.gif" AlternateText="Print" Visible="false" runat="server" />
                                    <a href="#" onclick="javascript:popwin(200, 600, 'AP_trx_PrintDocs.aspx?TrxID=')">
                                        <asp:Image ID="ibPrintDoc" Visible="false" runat="server" ImageUrl="../../images/butt_print_doc.gif" /></a>
                                    <!--
						<asp:ImageButton ID="ConfirmBtn"  AlternateText="Confirm"  onclick="ConfirmBtn_Click"  ImageUrl="../../images/butt_confirm.gif"  CausesValidation=False Runat=server />
						-->
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
