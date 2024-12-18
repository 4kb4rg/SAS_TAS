<%@ Page Language="vb" Src="../../../include/ap_trx_InvRcvNoteList.aspx.vb" Inherits="ap_trx_InvRcvNoteList" %>

<%@ Register TagPrefix="UserControl" TagName="MenuAP" Src="../../menu/menu_aptrx.ascx" %>
<%@ Register TagPrefix="Preference" TagName="PrefHdl" Src="../../include/preference/preference_handler.ascx" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v7.3" Namespace="Infragistics.WebUI.UltraWebTab"
    TagPrefix="igtab" %>
<html>
<head>
    <title>Invoice Receive List</title>
    <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
    <Preference:PrefHdl ID="PrefHdl" runat="server" />
</head>
<body>
    <form id="frmInvoiceList" class="main-modul-bg-app-list-pu" runat="server">
        <asp:Label ID="SortExpression" Visible="False" runat="server" />
        <asp:Label ID="SortCol" Visible="False" runat="server" />
        <asp:Label ID="lblErrMesage" Visible="false" Text="Error while initiating component." runat="server" />
        <asp:Label ID="lblID" Visible="false" Text=" ID" runat="server" />

        <table border="0" cellspacing="1" cellpadding="1" width="100%" class="font9Tahoma">
            <tr>
                <td colspan="6">
                    <UserControl:MenuAP ID="MenuAP" runat="server" />
                </td>
            </tr>

            <tr>
                <td style="width: 100%; height: 800px" valign="top">
                    <div class="kontenlist">
                        <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">

                            <tr>
                                <td class="font9Tahoma" colspan="3" style="width: 1133px"><strong>
                                    <asp:Label ID="lblTitle" runat="server" />
                                    LIST</strong> </td>
                                <td colspan="3" align="right" style="width: 4px"></td>
                            </tr>
                            <tr>
                                <td colspan="6" style="text-align: right; height: 6px;">
                                    <hr style="width: 100%" />
                                    &nbsp;<asp:Label ID="lblTracker" runat="server" /></td>
                            </tr>
                            <tr class="font9Tahoma">
                                <td colspan="6" width="100%" style="background-color: #FFCC00" style="height: 40px">
                                    <table width="100%" cellspacing="0" cellpadding="3" border="0" align="center">
                                        <tr class="font9Tahoma">
                                            <td valign="bottom" width="15%" style="height: 49px">
                                                <asp:Label ID="lblInvReceiveID" runat="server" />
                                                :<br>
                                                <asp:TextBox ID="txtInvoiceID" Width="100%" MaxLength="32" CssClass="fontObject" runat="server" /></td>
                                            <td valign="bottom" width="20%" style="height: 49px">POID :<br>
                                                <asp:TextBox ID="txtPOID" Width="100%" MaxLength="32" CssClass="fontObject" runat="server" /></td>
                                            <td valign="bottom" width="25%" style="height: 49px">Supplier Code :<br>
                                                <asp:TextBox ID="txtSupplier" Width="100%" MaxLength="20" CssClass="fontObject" runat="server" /></td>
                                            <td style="height: 49px">&nbsp</td>
                                            <td valign="bottom" width="10%" style="height: 49px">Invoice Type :<br>
                                                <asp:DropDownList ID="ddlInvoiceType" Width="100%" CssClass="fontObject" runat="server" /></td>
                                            <td valign="bottom" width="10%" style="height: 49px">Period :<br>
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
                                            <td valign="bottom" width="10%" style="height: 49px">
                                                <br>
                                                <asp:DropDownList ID="lstAccYear" Width="100%" CssClass="fontObject" runat="server">
                                                </asp:DropDownList>
                                            <td valign="bottom" width="12%" style="height: 49px">Status :<br>
                                                <asp:DropDownList ID="ddlStatus" Width="100%" CssClass="fontObject" runat="server" /></td>
                                            <td valign="bottom" width="15%" style="height: 49px">
                                                <br>
                                                <asp:TextBox ID="txtLastUpdate" Width="100%" MaxLength="128" Visible="false" runat="server" /></td>
                                            <td valign="bottom" width="8%" align="right" style="height: 49px">
                                                <asp:Button ID="SearchBtn" Text="Search" OnClick="srchBtn_Click" CssClass="button-small" runat="server" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                            <tr>
                                <td style="width: 100%">
                                    <igtab:UltraWebTab ID="TABBK" ThreeDEffect="False" TabOrientation="TopLeft"
                                        SelectedTab="0" Font-Names="Tahoma" Font-Size="8pt" ForeColor="black" runat="server">
                                        <DefaultTabStyle Height="22px">
                                        </DefaultTabStyle>
                                        <HoverTabStyle CssClass="ContentTabHover">
                                        </HoverTabStyle>
                                        <RoundedImage LeftSideWidth="6" RightSideWidth="6" SelectedImage="../../images/thumbs/ig_tab_winXP1.gif"
                                            NormalImage="../../images/thumbs/ig_tab_winXP3.gif" HoverImage="../../images/thumbs/ig_tab_winXP2.gif"
                                            FillStyle="LeftMergedWithCenter"></RoundedImage>
                                        <SelectedTabStyle CssClass="ContentTabSelected">
                                        </SelectedTabStyle>
                                        <Tabs>
                                            <igtab:Tab Key="Invoice" Text="INVOICE RECEIVE LIST" Tooltip="WB TICKET">
                                                <ContentPane>
                                                    <table border="0" cellspacing="1" cellpadding="1" width="99%">
                                                        <tr>
                                                            <td colspan="6">
                                                                <div id="div1" style="height: 380px; width: 1200px; overflow: auto;">
                                                                    <asp:DataGrid ID="dgInvList"
                                                                        AutoGenerateColumns="false" Width="100%" runat="server"
                                                                        GridLines="Both"
                                                                        CellPadding="2"
                                                                        AllowPaging="True"
                                                                        AllowCustomPaging="False"
                                                                        PageSize="15"
                                                                        OnPageIndexChanged="OnPageChanged"
                                                                        OnItemDataBound="dgLine_BindGrid"
                                                                        PagerStyle-Visible="False"
                                                                        OnDeleteCommand="DEDR_Delete"
                                                                        OnSortCommand="Sort_Grid"
                                                                        AllowSorting="True">
                                                                        <HeaderStyle CssClass="mr-h" />
                                                                        <ItemStyle CssClass="mr-l" />
                                                                        <AlternatingItemStyle CssClass="mr-r" />
                                                                        <Columns>
                                                                            <asp:HyperLinkColumn SortExpression="INV.InvoiceRcvID"
                                                                                DataNavigateUrlField="InvoiceRcvID"
                                                                                DataNavigateUrlFormatString="ap_trx_InvRcvNoteDet.aspx?inrid={0}"
                                                                                DataTextField="InvoiceRcvID" />
                                                                            <asp:HyperLinkColumn HeaderText="POID"
                                                                                SortExpression="LN.POID"
                                                                                DataNavigateUrlField="InvoiceRcvID"
                                                                                DataNavigateUrlFormatString="ap_trx_InvRcvNoteDet.aspx?inrid={0}"
                                                                                DataTextField="POID" />

                                                                            <asp:TemplateColumn HeaderText="Supplier Code" SortExpression="SupplierCode">
                                                                                <ItemStyle Width="12%" />
                                                                                <ItemTemplate>
                                                                                    <%#Container.DataItem("SupplierCode")%><br />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>

                                                                            <asp:TemplateColumn HeaderText="Supplier Name" SortExpression="SupplierName">
                                                                                <ItemStyle Width="25%" />
                                                                                <ItemTemplate>
                                                                                    <%# Container.DataItem("SupplierName") %><br />
                                                                                    <asp:Label ID="lbSplCode" Visible="false" Text='<%# Container.DataItem("SupplierCode") %>' runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>

                                                                            <asp:TemplateColumn HeaderText="Invoice Type" SortExpression="InvoiceType">
                                                                                <ItemStyle Width="10%" />
                                                                                <ItemTemplate>
                                                                                    <%# objAPTrx.mtdGetInvoiceType(Container.DataItem("InvoiceType")) %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="Due Date" SortExpression="InvoiceDueDate">
                                                                                <ItemStyle Width="15%" />
                                                                                <ItemTemplate>
                                                                                    <%#objGlobal.GetLongDate(Container.DataItem("InvoiceDueDate"))%>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="Remark" SortExpression="Remark">
                                                                                <ItemStyle Width="30%" />
                                                                                <ItemTemplate>
                                                                                    <%#Container.DataItem("Remark")%>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="Last Update" SortExpression="INV.UpdateDate">
                                                                                <ItemStyle Width="10%" />
                                                                                <ItemTemplate>
                                                                                    <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="Status" SortExpression="INV.Status">
                                                                                <ItemStyle Width="8%" />
                                                                                <ItemTemplate>
                                                                                    <%#objAPTrx.mtdGetInvoiceRcvNoteStatus(Container.DataItem("Status"))%>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="Updated By" SortExpression="USR.UserName">
                                                                                <ItemStyle Width="10%" />
                                                                                <ItemTemplate>
                                                                                    <%# Container.DataItem("UserName") %>
                                                                                    <asp:Label ID="idInvoiceRcvId" Visible="false" Text='<%# Container.DataItem("InvoiceRcvID")%>' runat="server" />
                                                                                    <asp:Label ID="lblStatus" Text='<%# Trim(Container.DataItem("Status")) %>' Visible="False" runat="server" />
                                                                                    <asp:LinkButton ID="lbDelete" CommandName="Delete" Text="Delete" runat="server" />

                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                        </Columns>
                                                                    </asp:DataGrid>
                                                                </div>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td style="width: 100%; text-align: right">
                                                                <asp:ImageButton ID="btnPrev" runat="server" ImageUrl="../../images/icn_prev.gif" AlternateText="Previous" CommandArgument="prev" OnClick="btnPrevNext_Click" />
                                                                <asp:DropDownList ID="lstDropList" AutoPostBack="True" OnSelectedIndexChanged="PagingIndexChanged" CssClass="fontObject" runat="server" />
                                                                <asp:ImageButton ID="btnNext" runat="server" ImageUrl="../../images/icn_next.gif" AlternateText="Next" CommandArgument="next" OnClick="btnPrevNext_Click" />
                                                            </td>
                                                        </tr>

                                                    </table>
                                                </ContentPane>
                                            </igtab:Tab>

                                            <igtab:Tab Key="InvoiceOst" Text="INVOICE OUTSTANDING LIST" Tooltip="WB TICKET">
                                                <ContentPane>
                                                    <table border="0" cellspacing="1" cellpadding="1" width="99%">
                                                        <tr>
                                                            <td colspan="6">
                                                                <div id="div2" style="height: 380px; width: 1200px; overflow: auto;">
                                                                    <asp:DataGrid ID="dgInvOst"
                                                                        AutoGenerateColumns="false" Width="100%" runat="server"
                                                                        GridLines="none"
                                                                        CellPadding="2"
                                                                        PagerStyle-Visible="False"
                                                                        OnItemCommand="EmpLink_Click"
                                                                        OnItemDataBound="dgLine_BindGrid"
                                                                        ShowFooter="true"
                                                                        AllowSorting="True">
                                                                        <HeaderStyle CssClass="mr-h" />
                                                                        <ItemStyle CssClass="mr-l" />
                                                                        <AlternatingItemStyle CssClass="mr-r" />
                                                                        <Columns>

                                                                            <asp:TemplateColumn HeaderText="No">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblNo" Visible="true" runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>

                                                                            <asp:TemplateColumn HeaderText="Year">
                                                                                <ItemTemplate>
                                                                                    <%# Container.DataItem("AccYear") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>

                                                                            <asp:TemplateColumn HeaderText="Month"  ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <%# Container.DataItem("AccMonth") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>

                                                                            <asp:TemplateColumn HeaderText="Purchase Order ID" ItemStyle-HorizontalAlign="left">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkEmpCode" CommandName="Item" Text='<%# Container.DataItem("Poid") %>' runat="server" />
                                                                                    <asp:Label ID="lblEmpCode" Text='<%# Container.DataItem("POid") %>' Visible="False" runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>

                                                                            <asp:TemplateColumn HeaderText="Goods Receive ID" ItemStyle-HorizontalAlign="left">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lblGrCode" CommandName="Sort" Text='<%# Container.DataItem("GoodsRcvID") %>' runat="server" />
                                                                                    <asp:Label ID="lblGRID" Text='<%# Container.DataItem("GoodsRcvID") %>' Visible="False" runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>

                                                                            <asp:TemplateColumn HeaderText="Receive Date" ItemStyle-HorizontalAlign="left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPOID" Visible="false" Text='<%# Container.DataItem("POID") %>' runat="server" />
                                                                                    <%#objGlobal.GetLongDate(Container.DataItem("GoodsRcvRefDate"))%>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>

                                                                            <asp:TemplateColumn HeaderText="Supplier Code" SortExpression="SupplierCode">
                                                                                <ItemTemplate>
                                                                                    <%#Container.DataItem("SupplierCode")%><br />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>

                                                                            <asp:TemplateColumn HeaderText="Supplier Name" SortExpression="SupplierName">
                                                                                <ItemTemplate>
                                                                                    <%# Container.DataItem("SupplierName") %><br />
                                                                                    <asp:Label ID="lbSplCode" Visible="false" Text='<%# Container.DataItem("SupplierCode") %>' runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="Credit Term" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <%# Container.DataItem("SuppCreditTerm") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn ItemStyle-HorizontalAlign="Right">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCurr" Visible="true" Text='<%# Container.DataItem("CURRENCYCODE") %>' runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>

                                                                            <asp:TemplateColumn HeaderText="DPP Amount" ItemStyle-HorizontalAlign="Right">
                                                                                <ItemTemplate>                                                                                               
                                                                                    <asp:Label ID="lblDPPAmount" Text='<%# FormatNumber(Container.DataItem("NetAmount"),2) %>' runat="server" />
                                                                                </ItemTemplate>

                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="lblTotDPPAmount" runat="server" />
                                                                                </FooterTemplate>
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                                <FooterStyle Font-Size="Smaller" BorderWidth="1" BorderStyle="Outset" BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />

                                                                            </asp:TemplateColumn>

                                                                           <asp:TemplateColumn HeaderText="Tax Amount" ItemStyle-HorizontalAlign="Right">
                                                                                <ItemTemplate>                                                                                     
                                                                                    <asp:Label ID="lblTaxAMount" Text='<%# FormatNumber(Container.DataItem("PPNAmount"), 2) %>' runat="server" />
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="lblTotTaxAmount" runat="server" />
                                                                                </FooterTemplate>
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                                <FooterStyle Font-Size="Smaller" BorderWidth="1" BorderStyle="Outset" BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />

                                                                            </asp:TemplateColumn>

                                                                            <asp:TemplateColumn HeaderText="Amount" ItemStyle-HorizontalAlign="Right">
                                                                                <ItemTemplate>                                                                                    
                                                                                    <asp:Label ID="lblAmount" Text='<%# FormatNumber(Container.DataItem("AmountToDisplay"), 2) %>' runat="server" />
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="lblTotAmount" runat="server" />
                                                                                </FooterTemplate>
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                                <FooterStyle Font-Size="Smaller" BorderWidth="1" BorderStyle="Outset" BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />

                                                                            </asp:TemplateColumn>

                                                                        </Columns>
                                                                    </asp:DataGrid><br>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentPane>
                                            </igtab:Tab>
                                        </Tabs>
                                    </igtab:UltraWebTab>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" width="100%" colspan="6">
                                    <asp:ImageButton ID="NewInvRcv" UseSubmitBehavior="false" OnClick="NewInvRcv_Click" ImageUrl="../../images/butt_new.gif" AlternateText="New Invoice Reception" runat="server" />
                                    <asp:ImageButton ID="ibPrint" UseSubmitBehavior="false" ImageUrl="../../images/butt_print.gif" AlternateText="Print" Visible="false" runat="server" />
                                    <a href="#" onclick="javascript:popwin(200, 600, 'AP_trx_PrintDocs.aspx?TrxID=')">
                                        <asp:Image ID="ibPrintDoc" Visible="False" runat="server" ImageUrl="../../images/butt_print_doc.gif" /></a>
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
