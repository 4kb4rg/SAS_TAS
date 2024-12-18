<%@ Page Language="vb" src="../../../include/CM_Trx_Contract_Monitor.aspx.vb" Inherits="CM_Trx_Contract_Monitor" %>
    <%@ Register TagPrefix="UserControl" Tagname="MenuWMTrx" src="../../menu/menu_WMtrx.ascx" %>
        <%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx" %>
            <%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v7.3" Namespace="Infragistics.WebUI.UltraWebTab"
                TagPrefix="igtab" %>

                <script runat="server">

                </script>

                <html>

                <head>
                    <title>CONTRACT MONITORING</title>
                    <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
                </head>
                <Preference:PrefHdl id=PrefHdl runat="server" />

                <body>
                    <form runat="server" class="main-modul-bg-app-list-pu" ID="Form1">
                        <table border="0" cellspacing="1" cellpadding="1" width="100%">
                            <tr>
                                <td style="width: 100%; height: 800px" valign="top">
                                    <div class="kontenlist">

                                        <table border="0" cellspacing="1" cellpadding="1" width="100%">
                                            <tr>
                                                <td class="font9Tahoma" colspan="3" style="height: 21px"><strong>
                                                        CONTRACT MONITORING</strong></td>
                                            </tr>
                                            <tr>
                                                <td colspan=6 style="height: 11px">
                                                    <hr style="width :100%" />
                                                </td>
                                            </tr>
                                        </table>

                                        <table border="0" cellspacing="1" cellpadding="1" width="100%"
                                            class="font9Tahoma">
                                            <tr>
                                                <td colspan="6">
                                                    <UserControl:MenuWMTrx id=MenuWMTrx runat="server" />
                                                </td>
                                            </tr>

                                            <tr>
                                                <td colspan=6 width=99%>
                                                    <table width="99%" bgcolor=white cellspacing="0" cellpadding="3"
                                                        border="0" align="center" style="height: 40px"
                                                        class="font9Tahoma">
                                                        <tr style="background-color:#FFCC00">
                                                            <td valign=bottom width=20% style="height: 39px">
                                                                Contract No :<BR>
                                                                <asp:TextBox id="srchContractNo" width=100%
                                                                    runat="server" />
                                                            </td>
                                                            <td valign=bottom width=20% style="height: 39px">
                                                                DO No :<BR>
                                                                <asp:TextBox id="srchDO" width=100% runat="server" />
                                                            </td>
                                                            <td valign=bottom width=20% style="height: 39px">
                                                                Customer :
                                                                <asp:TextBox id="srchCust" width=100% runat="server" />
                                                            </td>
                                                            <td colspan="2">
                                                                <br />
                                                            </td>
                                                            <td valign=bottom width="10%">Product :<BR>
                                                                <asp:dropdownlist id=srchProductList width=100%
                                                                    CssClass="fontObject" runat="server" />
                                                            </td>
                                                            <td valign="bottom" width=5% style="height: 39px">
                                                                Period :<asp:DropDownList id="lstAccMonth" width=100%
                                                                    CssClass="fontObject" runat=server>
                                                                    <asp:ListItem value="">ALL</asp:ListItem>
                                                                    <asp:ListItem value="1">1</asp:ListItem>
                                                                    <asp:ListItem value="2">2</asp:ListItem>
                                                                    <asp:ListItem value="3">3</asp:ListItem>
                                                                    <asp:ListItem value="4">4</asp:ListItem>
                                                                    <asp:ListItem value="5">5</asp:ListItem>
                                                                    <asp:ListItem value="6">6</asp:ListItem>
                                                                    <asp:ListItem value="7">7</asp:ListItem>
                                                                    <asp:ListItem value="8">8</asp:ListItem>
                                                                    <asp:ListItem value="9">9</asp:ListItem>
                                                                    <asp:ListItem value="10">10</asp:ListItem>
                                                                    <asp:ListItem value="11">11</asp:ListItem>
                                                                    <asp:ListItem value="12">12</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td valign=bottom width=8% style="height: 39px">
                                                                <asp:DropDownList id="lstAccYear" width=100%
                                                                    CssClass="fontObject" runat=server>
                                                                </asp:DropDownList>
                                                            <td valign=bottom width="12%" style="height: 39px">Status
                                                                :<BR>
                                                                <asp:DropDownList id="srchStatusList" width=100%
                                                                    CssClass="fontObject" runat=server>
                                                                    <asp:ListItem value="0" Selected>All</asp:ListItem>
                                                                    <asp:ListItem value="1">Outstanding Contract
                                                                    </asp:ListItem>
                                                                    <asp:ListItem value="2">Full Received</asp:ListItem>
                                                                    <asp:ListItem value="3">Closed Contract
                                                                    </asp:ListItem>
                                                                    <asp:ListItem value="4">Outstanding Receipt
                                                                    </asp:ListItem>
                                                                    <asp:ListItem value="5">Period Dispatch/Not Invoice
                                                                    </asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td valign=bottom width=12% style="height: 39px"><BR>
                                                            </td>
                                                            <td valign=bottom width=10% align=right
                                                                style="height: 39px">
                                                                <asp:Button Text="Search" OnClick=srchBtn_Click
                                                                    runat="server" CssClass="button-small"
                                                                    ID="BtnSearch" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>

                                        <table border=0 cellspacing=1 cellpadding=1 width=100%>
                                            <tr>
                                                <td style="height: 24px;" colspan="5">
                                                    <igtab:UltraWebTab ID="TABBK" ThreeDEffect="False"
                                                        TabOrientation="TopLeft" SelectedTab="0" Font-Names="Tahoma"
                                                        Font-Size="8pt" ForeColor=black runat="server">
                                                        <DefaultTabStyle Height="22px">
                                                        </DefaultTabStyle>
                                                        <HoverTabStyle CssClass="ContentTabHover">
                                                        </HoverTabStyle>
                                                        <RoundedImage LeftSideWidth="6" RightSideWidth="6"
                                                            SelectedImage="../../images/thumbs/ig_tab_winXP1.gif"
                                                            NormalImage="../../images/thumbs/ig_tab_winXP3.gif"
                                                            HoverImage="../../images/thumbs/ig_tab_winXP2.gif"
                                                            FillStyle="LeftMergedWithCenter"></RoundedImage>
                                                        <SelectedTabStyle CssClass="ContentTabSelected">
                                                        </SelectedTabStyle>
                                                        <Tabs>
                                                            <igtab:Tab Key="CONTRACT" Text="ON CONTRACT"
                                                                Tooltip="ON CONTRACT">
                                                                <ContentPane>
                                                                    <table border="0" cellspacing="1" cellpadding="1"
                                                                        width="99%">
                                                                        <tr>
                                                                            <td colspan="5">
                                                                                <div id="div1"
                                                                                    style="height:500px;width:1100px;overflow:auto;">
                                                                                    <asp:DataGrid ID="dgContractList"
                                                                                        runat="server"
                                                                                        AutoGenerateColumns="False"
                                                                                        GridLines=none Cellpadding=2
                                                                                        CellSpacing=1
                                                                                        OnItemDataBound="dgContractList_BindGrid"
                                                                                        Width="240%">
                                                                                        <AlternatingItemStyle
                                                                                            CssClass="mr-r" />
                                                                                        <ItemStyle CssClass="mr-l" />
                                                                                        <HeaderStyle CssClass="mr-h" />
                                                                                        <Columns>
                                                                                            <asp:TemplateColumn
                                                                                                HeaderText="No">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label
                                                                                                        ID="lblContractNo"
                                                                                                        runat="server"
                                                                                                        Text='<%# Container.DataItem("ContractView") %>'>
                                                                                                    </asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle
                                                                                                    HorizontalAlign="Center" />
                                                                                                <ItemStyle Width="6%" />
                                                                                            </asp:TemplateColumn>
                                                                                            <asp:TemplateColumn
                                                                                                HeaderText="Date">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label
                                                                                                        ID="lblContractDate"
                                                                                                        Text='<%#objGlobal.GetLongDate(Container.DataItem("ContractDate"))%>'
                                                                                                        runat="server" />
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle
                                                                                                    HorizontalAlign="Center" />
                                                                                                <ItemStyle
                                                                                                    HorizontalAlign="Center"
                                                                                                    Width="3%" />
                                                                                            </asp:TemplateColumn>
                                                                                            <asp:TemplateColumn
                                                                                                HeaderText="Customer"
                                                                                                HeaderStyle-HorizontalAlign=Center>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label
                                                                                                        ID="lblCustomer"
                                                                                                        runat="server"
                                                                                                        Text='<%# Container.DataItem("Name") %>'>
                                                                                                    </asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle
                                                                                                    HorizontalAlign="Left"
                                                                                                    Width="8%" />
                                                                                            </asp:TemplateColumn>
                                                                                            <asp:TemplateColumn
                                                                                                HeaderText="Product">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label
                                                                                                        ID="lblProductCode"
                                                                                                        Text='<%# objWMTrx.mtdGetWeighBridgeTicketProduct(Container.DataItem("ProductCode")) %>'
                                                                                                        runat="server" />
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle
                                                                                                    HorizontalAlign="Center" />
                                                                                                <ItemStyle Width="3%" />
                                                                                            </asp:TemplateColumn>
                                                                                            <asp:TemplateColumn
                                                                                                HeaderText="Quantity">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label
                                                                                                        ID="lblQtyContract"
                                                                                                        runat="server"
                                                                                                        Text='<%# FormatNumber(Container.DataItem("ContractQTYView"), 0) %>'
                                                                                                        Visible="True">
                                                                                                    </asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle
                                                                                                    HorizontalAlign="Center" />
                                                                                                <ItemStyle
                                                                                                    HorizontalAlign="Right"
                                                                                                    Width="3%" />
                                                                                            </asp:TemplateColumn>
                                                                                            <asp:TemplateColumn
                                                                                                HeaderText="Price">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label
                                                                                                        ID="lblPrice"
                                                                                                        runat="server"
                                                                                                        Text='<%# FormatNumber(Container.DataItem("Price"), 2) %>'
                                                                                                        Visible="True">
                                                                                                    </asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle
                                                                                                    HorizontalAlign="Center" />
                                                                                                <ItemStyle
                                                                                                    HorizontalAlign="Right"
                                                                                                    Width="2%" />
                                                                                            </asp:TemplateColumn>
                                                                                            <asp:TemplateColumn
                                                                                                HeaderText="Amount">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label
                                                                                                        ID="lblAmount"
                                                                                                        runat="server"
                                                                                                        Text='<%# FormatNumber(Container.DataItem("ContractAmount"), 0) %>'
                                                                                                        Visible="True">
                                                                                                    </asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle
                                                                                                    HorizontalAlign="Center" />
                                                                                                <ItemStyle
                                                                                                    HorizontalAlign="Right"
                                                                                                    Width="4%" />
                                                                                            </asp:TemplateColumn>

                                                                                            <asp:TemplateColumn
                                                                                                HeaderText="No">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label
                                                                                                        ID="lblDONo"
                                                                                                        runat="server"
                                                                                                        Text='<%# Container.DataItem("DONo") %>'>
                                                                                                    </asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle
                                                                                                    HorizontalAlign="Center" />
                                                                                                <ItemStyle Width="6%" />
                                                                                            </asp:TemplateColumn>
                                                                                            <asp:TemplateColumn
                                                                                                HeaderText="Transporter"
                                                                                                HeaderStyle-HorizontalAlign=Center>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label
                                                                                                        ID="lblTransporter"
                                                                                                        runat="server"
                                                                                                        Text='<%# Container.DataItem("TransporterName") %>'>
                                                                                                    </asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle Width="8%" />
                                                                                            </asp:TemplateColumn>
                                                                                            <asp:TemplateColumn
                                                                                                HeaderText="Quantity">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label
                                                                                                        ID="lblDOQty"
                                                                                                        runat="server"
                                                                                                        Text='<%# FormatNumber(Container.DataItem("DOQty"), 0) %>'
                                                                                                        Visible="True">
                                                                                                    </asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle
                                                                                                    HorizontalAlign="Center" />
                                                                                                <ItemStyle
                                                                                                    HorizontalAlign="Right"
                                                                                                    Width="3%" />
                                                                                            </asp:TemplateColumn>

                                                                                            <asp:TemplateColumn
                                                                                                HeaderText="Delivery">
                                                                                                <ItemTemplate>
                                                                                                    <%#Container.DataItem("TermOfDelivery")%>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle
                                                                                                    HorizontalAlign="Center" />
                                                                                                <ItemStyle Width="3%" />
                                                                                            </asp:TemplateColumn>
                                                                                            <asp:TemplateColumn
                                                                                                HeaderText="Weighing">
                                                                                                <ItemTemplate>
                                                                                                    <%#Container.DataItem("TermOfWeighing")%>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle
                                                                                                    HorizontalAlign="Center" />
                                                                                                <ItemStyle Width="3%" />
                                                                                            </asp:TemplateColumn>

                                                                                            <asp:TemplateColumn
                                                                                                HeaderText="This Month">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label
                                                                                                        ID="lblConDisp"
                                                                                                        runat="server"
                                                                                                        Text='<%# FormatNumber(Container.DataItem("QtyConDisp"), 0) %>'
                                                                                                        Visible="True">
                                                                                                    </asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle
                                                                                                    HorizontalAlign="Center" />
                                                                                                <ItemStyle
                                                                                                    HorizontalAlign="Right"
                                                                                                    Width="3%" />
                                                                                            </asp:TemplateColumn>
                                                                                            <asp:TemplateColumn
                                                                                                HeaderText="Total">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label
                                                                                                        ID="lblConDispTD"
                                                                                                        runat="server"
                                                                                                        Text='<%# FormatNumber(Container.DataItem("QtyConDispTD"), 0) %>'
                                                                                                        Visible="True">
                                                                                                    </asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle
                                                                                                    HorizontalAlign="Center" />
                                                                                                <ItemStyle
                                                                                                    HorizontalAlign="Right"
                                                                                                    Width="3%" />
                                                                                            </asp:TemplateColumn>
                                                                                            <asp:TemplateColumn
                                                                                                HeaderText="Balance">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label
                                                                                                        ID="lblConDispBal"
                                                                                                        runat="server"
                                                                                                        Text='<%# FormatNumber(Container.DataItem("QtyConDispTD") - Container.DataItem("ContractQTY"), 0) %>'
                                                                                                        Visible="True">
                                                                                                    </asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle
                                                                                                    HorizontalAlign="Center" />
                                                                                                <ItemStyle
                                                                                                    HorizontalAlign="Right"
                                                                                                    Width="3%" />
                                                                                            </asp:TemplateColumn>

                                                                                            <asp:TemplateColumn
                                                                                                HeaderText="This Month">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label
                                                                                                        ID="lblBuyerQty"
                                                                                                        runat="server"
                                                                                                        Text='<%# FormatNumber(Container.DataItem("QtyBuyer"), 0) %>'>
                                                                                                    </asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle
                                                                                                    HorizontalAlign="Center" />
                                                                                                <ItemStyle
                                                                                                    HorizontalAlign="Right"
                                                                                                    Width="3%" />
                                                                                            </asp:TemplateColumn>
                                                                                            <asp:TemplateColumn
                                                                                                HeaderText="Total">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label
                                                                                                        ID="lblBuyerQtyTD"
                                                                                                        runat="server"
                                                                                                        Text='<%# FormatNumber(Container.DataItem("QtyBuyerTD"), 0) %>'>
                                                                                                    </asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle
                                                                                                    HorizontalAlign="Center" />
                                                                                                <ItemStyle
                                                                                                    HorizontalAlign="Right"
                                                                                                    Width="3%" />
                                                                                            </asp:TemplateColumn>
                                                                                            <asp:TemplateColumn
                                                                                                HeaderText="Balance">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label
                                                                                                        ID="lblBuyerQtyBal"
                                                                                                        runat="server"
                                                                                                        Text='<%# FormatNumber(Container.DataItem("QtyBuyerTD") - Container.DataItem("ContractQTY"), 0) %>'>
                                                                                                    </asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle
                                                                                                    HorizontalAlign="Center" />
                                                                                                <ItemStyle
                                                                                                    HorizontalAlign="Right"
                                                                                                    Width="3%" />
                                                                                            </asp:TemplateColumn>

                                                                                            <asp:TemplateColumn
                                                                                                HeaderText="This Month">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label
                                                                                                        ID="lblAmountConBI"
                                                                                                        runat="server"
                                                                                                        Text='<%# FormatNumber(Container.DataItem("AmountConBI"), 0) %>'>
                                                                                                    </asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle
                                                                                                    HorizontalAlign="Center" />
                                                                                                <ItemStyle
                                                                                                    HorizontalAlign="Right"
                                                                                                    Width="4%" />
                                                                                            </asp:TemplateColumn>
                                                                                            <asp:TemplateColumn
                                                                                                HeaderText="Total">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label
                                                                                                        ID="lblAmountConSBI"
                                                                                                        runat="server"
                                                                                                        Text='<%# FormatNumber(Container.DataItem("AmountConSBI"), 0) %>'>
                                                                                                    </asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle
                                                                                                    HorizontalAlign="Center" />
                                                                                                <ItemStyle
                                                                                                    HorizontalAlign="Right"
                                                                                                    Width="4%" />
                                                                                            </asp:TemplateColumn>
                                                                                            <asp:TemplateColumn
                                                                                                HeaderText="Balance">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label
                                                                                                        ID="lblAmountConBal"
                                                                                                        runat="server"
                                                                                                        Text='<%# FormatNumber(Container.DataItem("AmountConBalance"), 0) %>'>
                                                                                                    </asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle
                                                                                                    HorizontalAlign="Center" />
                                                                                                <ItemStyle
                                                                                                    HorizontalAlign="Right"
                                                                                                    Width="4%" />
                                                                                            </asp:TemplateColumn>

                                                                                            <asp:TemplateColumn
                                                                                                HeaderText="Tonase">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label
                                                                                                        ID="lblClaimQty"
                                                                                                        runat="server"
                                                                                                        Text='<%# FormatNumber(Container.DataItem("ClaimQty"), 0) %>'>
                                                                                                    </asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle
                                                                                                    HorizontalAlign="Center" />
                                                                                                <ItemStyle
                                                                                                    HorizontalAlign="Right"
                                                                                                    Width="4%" />
                                                                                            </asp:TemplateColumn>
                                                                                            <asp:TemplateColumn
                                                                                                HeaderText="Amount">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label
                                                                                                        ID="lblClaimAmount"
                                                                                                        runat="server"
                                                                                                        Text='<%# FormatNumber(Container.DataItem("ClaimAmount"), 0) %>'>
                                                                                                    </asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle
                                                                                                    HorizontalAlign="Center" />
                                                                                                <ItemStyle
                                                                                                    HorizontalAlign="Right"
                                                                                                    Width="4%" />
                                                                                            </asp:TemplateColumn>

                                                                                            <asp:TemplateColumn
                                                                                                HeaderText="Delivered">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label
                                                                                                        ID="lblLastDelivered"
                                                                                                        Text='<%#objGlobal.GetLongDate(Container.DataItem("LastDelivered"))%>'
                                                                                                        runat="server" />
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle
                                                                                                    HorizontalAlign="Center" />
                                                                                                <ItemStyle
                                                                                                    HorizontalAlign="Center"
                                                                                                    Width="3%" />
                                                                                            </asp:TemplateColumn>
                                                                                            <asp:TemplateColumn
                                                                                                HeaderText="Received">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label
                                                                                                        ID="lblLastReceived"
                                                                                                        Text='<%#objGlobal.GetLongDate(Container.DataItem("LastReceived"))%>'
                                                                                                        runat="server" />
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle
                                                                                                    HorizontalAlign="Center" />
                                                                                                <ItemStyle
                                                                                                    HorizontalAlign="Center"
                                                                                                    Width="3%" />
                                                                                            </asp:TemplateColumn>

                                                                                            <asp:TemplateColumn
                                                                                                HeaderText="OA">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label
                                                                                                        ID="lblOA"
                                                                                                        runat="server"
                                                                                                        Text='<%# FormatNumber(Container.DataItem("HargaOA"), 0) %>'>
                                                                                                    </asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle
                                                                                                    HorizontalAlign="Center" />
                                                                                                <ItemStyle
                                                                                                    HorizontalAlign="Right"
                                                                                                    Width="3%" />
                                                                                            </asp:TemplateColumn>


                                                                                        </Columns>
                                                                                    </asp:DataGrid>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan=5>&nbsp;</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="3">
                                                                                <asp:CheckBox id="cbExcelContract"
                                                                                    text=" Export To Excel"
                                                                                    checked="false" Visible=false
                                                                                    runat="server" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan=5>&nbsp;</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan=5>
                                                                                <asp:ImageButton
                                                                                    id="ContractListPreview"
                                                                                    Visible=false
                                                                                    AlternateText="Print Preview"
                                                                                    imageurl="../../images/butt_print_preview.gif"
                                                                                    OnClick="ContractListPreview_onClick"
                                                                                    runat="server" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="3">
                                                                                <asp:Label id=lblErrRefresh
                                                                                    ForeColor=red Font-Italic=true
                                                                                    visible=false runat=server />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ContentPane>
                                                            </igtab:Tab>

                                                            <%--DO--%>
                                                                <igtab:Tab Key="DO" Text="ON DELIVERY ORDER"
                                                                    Tooltip="ON DELIVERY ORDER">
                                                                    <ContentPane>
                                                                        <table border="0" cellspacing="1"
                                                                            cellpadding="1" width="99%">
                                                                            <tr>
                                                                                <td colspan="5">
                                                                                    <div id="div2"
                                                                                        style="height:500px;width:1100;overflow:auto;">
                                                                                        <asp:DataGrid ID="dgDOList"
                                                                                            runat="server"
                                                                                            AutoGenerateColumns="False"
                                                                                            GridLines=none Cellpadding=2
                                                                                            CellSpacing=1
                                                                                            OnItemDataBound="dgDOList_BindGrid"
                                                                                            Width="220%">
                                                                                            <AlternatingItemStyle
                                                                                                CssClass="mr-r" />
                                                                                            <ItemStyle
                                                                                                CssClass="mr-l" />
                                                                                            <HeaderStyle
                                                                                                CssClass="mr-h" />
                                                                                            <Columns>
                                                                                                <asp:TemplateColumn
                                                                                                    HeaderText="No">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label
                                                                                                            ID="lblDONo"
                                                                                                            runat="server"
                                                                                                            Text='<%# Container.DataItem("DONo") %>'>
                                                                                                        </asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle
                                                                                                        HorizontalAlign="Center" />
                                                                                                    <ItemStyle
                                                                                                        Width="6%" />
                                                                                                </asp:TemplateColumn>
                                                                                                <asp:TemplateColumn
                                                                                                    HeaderText="Date">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label
                                                                                                            ID="lblDODate"
                                                                                                            Text='<%#objGlobal.GetLongDate(Container.DataItem("DODate"))%>'
                                                                                                            runat="server" />
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle
                                                                                                        HorizontalAlign="Center" />
                                                                                                    <ItemStyle
                                                                                                        HorizontalAlign="Center"
                                                                                                        Width="4%" />
                                                                                                </asp:TemplateColumn>
                                                                                                <asp:TemplateColumn
                                                                                                    HeaderText="Transporter"
                                                                                                    HeaderStyle-HorizontalAlign=Center>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label
                                                                                                            ID="lblTransporter"
                                                                                                            runat="server"
                                                                                                            Text='<%# Container.DataItem("TransporterName") %>'>
                                                                                                        </asp:Label>
                                                                                                        <br />
                                                                                                        <asp:Label
                                                                                                            ID="lblTransporterCode"
                                                                                                            Visible=false
                                                                                                            runat="server"
                                                                                                            Text='<%# Container.DataItem("TransporterCode") %>'>
                                                                                                        </asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle
                                                                                                        Width="8%" />
                                                                                                </asp:TemplateColumn>
                                                                                                <asp:TemplateColumn
                                                                                                    HeaderText="Product">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label
                                                                                                            ID="lblProductCode"
                                                                                                            Text='<%# objWMTrx.mtdGetWeighBridgeTicketProduct(Container.DataItem("ProductCode")) %>'
                                                                                                            runat="server" />
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle
                                                                                                        HorizontalAlign="Center" />
                                                                                                    <ItemStyle
                                                                                                        Width="3%" />
                                                                                                </asp:TemplateColumn>
                                                                                                <asp:TemplateColumn
                                                                                                    HeaderText="Quantity">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label
                                                                                                            ID="lblDOQty"
                                                                                                            runat="server"
                                                                                                            Text='<%# FormatNumber(Container.DataItem("DOQty"), 0) %>'
                                                                                                            Visible="True">
                                                                                                        </asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle
                                                                                                        HorizontalAlign="Center" />
                                                                                                    <ItemStyle
                                                                                                        HorizontalAlign="Right"
                                                                                                        Width="4%" />
                                                                                                </asp:TemplateColumn>
                                                                                                <asp:TemplateColumn
                                                                                                    HeaderText="Tolerance">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label
                                                                                                            ID="lblToleransi"
                                                                                                            runat="server"
                                                                                                            Text='<%# FormatNumber(Container.DataItem("Toleransi"), 0) %>'
                                                                                                            Visible="True">
                                                                                                        </asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle
                                                                                                        HorizontalAlign="Center" />
                                                                                                    <ItemStyle
                                                                                                        HorizontalAlign="Right"
                                                                                                        Width="3%" />
                                                                                                </asp:TemplateColumn>

                                                                                                <asp:TemplateColumn
                                                                                                    HeaderText="This Month">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label
                                                                                                            ID="lblConDisp"
                                                                                                            runat="server"
                                                                                                            Text='<%# FormatNumber(Container.DataItem("QtyConDisp"), 0) %>'
                                                                                                            Visible="True">
                                                                                                        </asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle
                                                                                                        HorizontalAlign="Center" />
                                                                                                    <ItemStyle
                                                                                                        HorizontalAlign="Right"
                                                                                                        Width="4%" />
                                                                                                </asp:TemplateColumn>
                                                                                                <asp:TemplateColumn
                                                                                                    HeaderText="Total">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label
                                                                                                            ID="lblConDispTD"
                                                                                                            runat="server"
                                                                                                            Text='<%# FormatNumber(Container.DataItem("QtyConDispTD"), 0) %>'
                                                                                                            Visible="True">
                                                                                                        </asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle
                                                                                                        HorizontalAlign="Center" />
                                                                                                    <ItemStyle
                                                                                                        HorizontalAlign="Right"
                                                                                                        Width="4%" />
                                                                                                </asp:TemplateColumn>
                                                                                                <asp:TemplateColumn
                                                                                                    HeaderText="Balance">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label
                                                                                                            ID="lblConDispBal"
                                                                                                            runat="server"
                                                                                                            Text='<%# FormatNumber(Container.DataItem("QtyConDispTD") - Container.DataItem("ContractQTY"), 0) %>'
                                                                                                            Visible="True">
                                                                                                        </asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle
                                                                                                        HorizontalAlign="Center" />
                                                                                                    <ItemStyle
                                                                                                        HorizontalAlign="Right"
                                                                                                        Width="4%" />
                                                                                                </asp:TemplateColumn>

                                                                                                <asp:TemplateColumn
                                                                                                    HeaderText="This Month">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label
                                                                                                            ID="lblBuyerQty"
                                                                                                            runat="server"
                                                                                                            Text='<%# FormatNumber(Container.DataItem("QtyBuyer"), 0) %>'>
                                                                                                        </asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle
                                                                                                        HorizontalAlign="Center" />
                                                                                                    <ItemStyle
                                                                                                        HorizontalAlign="Right"
                                                                                                        Width="4%" />
                                                                                                </asp:TemplateColumn>
                                                                                                <asp:TemplateColumn
                                                                                                    HeaderText="Total">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label
                                                                                                            ID="lblBuyerQtyTD"
                                                                                                            runat="server"
                                                                                                            Text='<%# FormatNumber(Container.DataItem("QtyBuyerTD"), 0) %>'>
                                                                                                        </asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle
                                                                                                        HorizontalAlign="Center" />
                                                                                                    <ItemStyle
                                                                                                        HorizontalAlign="Right"
                                                                                                        Width="4%" />
                                                                                                </asp:TemplateColumn>
                                                                                                <asp:TemplateColumn
                                                                                                    HeaderText="Balance">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label
                                                                                                            ID="lblBuyerQtyBal"
                                                                                                            runat="server"
                                                                                                            Text='<%# FormatNumber(Container.DataItem("QtyBuyerTD") - Container.DataItem("ContractQTY"), 0) %>'>
                                                                                                        </asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle
                                                                                                        HorizontalAlign="Center" />
                                                                                                    <ItemStyle
                                                                                                        HorizontalAlign="Right"
                                                                                                        Width="4%" />
                                                                                                </asp:TemplateColumn>

                                                                                                <asp:TemplateColumn
                                                                                                    HeaderText="Rp/Kg">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label
                                                                                                            ID="lblDOPrice"
                                                                                                            runat="server"
                                                                                                            Text='<%# FormatNumber(Container.DataItem("HargaOA"), 0) %>'>
                                                                                                        </asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle
                                                                                                        HorizontalAlign="Center" />
                                                                                                    <ItemStyle
                                                                                                        HorizontalAlign="Right"
                                                                                                        Width="3%" />
                                                                                                </asp:TemplateColumn>
                                                                                                <asp:TemplateColumn
                                                                                                    HeaderText="This Month">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label
                                                                                                            ID="lblAmountOABI"
                                                                                                            runat="server"
                                                                                                            Text='<%# FormatNumber(Container.DataItem("AmountOABI"), 0) %>'>
                                                                                                        </asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle
                                                                                                        HorizontalAlign="Center" />
                                                                                                    <ItemStyle
                                                                                                        HorizontalAlign="Right"
                                                                                                        Width="4%" />
                                                                                                </asp:TemplateColumn>
                                                                                                <asp:TemplateColumn
                                                                                                    HeaderText="Total">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label
                                                                                                            ID="lblAmountOASBI"
                                                                                                            runat="server"
                                                                                                            Text='<%# FormatNumber(Container.DataItem("AmountOASBI"), 0) %>'>
                                                                                                        </asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle
                                                                                                        HorizontalAlign="Center" />
                                                                                                    <ItemStyle
                                                                                                        HorizontalAlign="Right"
                                                                                                        Width="4%" />
                                                                                                </asp:TemplateColumn>
                                                                                                <asp:TemplateColumn
                                                                                                    HeaderText="Balance">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label
                                                                                                            ID="lblAmountOABal"
                                                                                                            runat="server"
                                                                                                            Text='<%# FormatNumber(Container.DataItem("AmountOABalance"), 0) %>'>
                                                                                                        </asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle
                                                                                                        HorizontalAlign="Center" />
                                                                                                    <ItemStyle
                                                                                                        HorizontalAlign="Right"
                                                                                                        Width="4%" />
                                                                                                </asp:TemplateColumn>

                                                                                                <asp:TemplateColumn
                                                                                                    visible=false>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label
                                                                                                            ID="lblPOID"
                                                                                                            runat="server"
                                                                                                            Text='<%# Container.DataItem("POID") %>'>
                                                                                                        </asp:Label>
                                                                                                        <br />
                                                                                                        <asp:Button
                                                                                                            Text="Generate"
                                                                                                            OnClick=GRGenerate_Click
                                                                                                            runat="server"
                                                                                                            ID="btnGenerate"
                                                                                                            Font-Size="8pt"
                                                                                                            Width="100%"
                                                                                                            Height="26px"
                                                                                                            ToolTip="click generate gr" />
                                                                                                        <br />
                                                                                                        <asp:Label
                                                                                                            ID="lblGRID"
                                                                                                            Visible=false
                                                                                                            runat="server"
                                                                                                            Text='<%# Container.DataItem("GRID") %>'>
                                                                                                        </asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle
                                                                                                        HorizontalAlign="Left"
                                                                                                        Width="5%" />
                                                                                                </asp:TemplateColumn>

                                                                                                <asp:TemplateColumn
                                                                                                    HeaderText="No">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label
                                                                                                            ID="lblContractNo"
                                                                                                            runat="server"
                                                                                                            Text='<%# Container.DataItem("ContractNo") %>'>
                                                                                                        </asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle
                                                                                                        HorizontalAlign="Center" />
                                                                                                    <ItemStyle
                                                                                                        Width="6%" />
                                                                                                </asp:TemplateColumn>
                                                                                                <asp:TemplateColumn
                                                                                                    HeaderText="Customer"
                                                                                                    HeaderStyle-HorizontalAlign=Center>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label
                                                                                                            ID="lblCustomer"
                                                                                                            runat="server"
                                                                                                            Text='<%# Container.DataItem("Name") %>'>
                                                                                                        </asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle
                                                                                                        Width="8%" />
                                                                                                </asp:TemplateColumn>
                                                                                                <asp:TemplateColumn
                                                                                                    HeaderText="Quantity">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label
                                                                                                            ID="lblQtyContract"
                                                                                                            runat="server"
                                                                                                            Text='<%# FormatNumber(Container.DataItem("ContractQTY"), 0) %>'
                                                                                                            Visible="True">
                                                                                                        </asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle
                                                                                                        HorizontalAlign="Center" />
                                                                                                    <ItemStyle
                                                                                                        HorizontalAlign="Right"
                                                                                                        Width="4%" />
                                                                                                </asp:TemplateColumn>
                                                                                                <asp:TemplateColumn
                                                                                                    HeaderText="Price">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label
                                                                                                            ID="lblPrice"
                                                                                                            runat="server"
                                                                                                            Text='<%# FormatNumber(Container.DataItem("Price"), 2) %>'
                                                                                                            Visible="True">
                                                                                                        </asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle
                                                                                                        HorizontalAlign="Center" />
                                                                                                    <ItemStyle
                                                                                                        HorizontalAlign="Right"
                                                                                                        Width="4%" />
                                                                                                </asp:TemplateColumn>
                                                                                                <asp:TemplateColumn
                                                                                                    HeaderText="Amount">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label
                                                                                                            ID="lblAmount"
                                                                                                            runat="server"
                                                                                                            Text='<%# FormatNumber(Container.DataItem("ContractAmount"), 0) %>'
                                                                                                            Visible="True">
                                                                                                        </asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle
                                                                                                        HorizontalAlign="Center" />
                                                                                                    <ItemStyle
                                                                                                        HorizontalAlign="Right"
                                                                                                        Width="4%" />
                                                                                                </asp:TemplateColumn>
                                                                                                <asp:TemplateColumn
                                                                                                    HeaderText="Terms">
                                                                                                    <ItemTemplate>
                                                                                                        <%#Container.DataItem("TermOfDelivery")%>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle
                                                                                                        HorizontalAlign="Center" />
                                                                                                    <ItemStyle
                                                                                                        Width="4%" />
                                                                                                </asp:TemplateColumn>
                                                                                            </Columns>
                                                                                        </asp:DataGrid>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan=5>&nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="3">
                                                                                    <asp:CheckBox id="cbExcelDO"
                                                                                        text=" Export To Excel"
                                                                                        checked="false" Visible=false
                                                                                        runat="server" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan=5>&nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan=5>
                                                                                    <asp:ImageButton id="DOListPreview"
                                                                                        Visible=false
                                                                                        AlternateText="Print Preview"
                                                                                        imageurl="../../images/butt_print_preview.gif"
                                                                                        OnClick="DOListPreview_onClick"
                                                                                        runat="server" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="3">
                                                                                    <asp:Label id=lblErrGenerate
                                                                                        ForeColor=red Font-Italic=true
                                                                                        visible=false runat=server />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </ContentPane>
                                                                </igtab:Tab>

                                                                <%--RECEIPT--%>
                                                                    <igtab:Tab Key="RECEIPT" Text="ON RECEIPT"
                                                                        Tooltip="ON RECEIPT">
                                                                        <ContentPane>
                                                                            <table border="0" cellspacing="1"
                                                                                cellpadding="1" width="99%">
                                                                                <tr>
                                                                                    <td colspan="5">
                                                                                        <div id="div4"
                                                                                            style="height:500px;width:1300;overflow:auto;">
                                                                                            <asp:DataGrid ID="dgReceive"
                                                                                                runat="server"
                                                                                                AutoGenerateColumns="False"
                                                                                                GridLines="Both"
                                                                                                Cellpadding=2
                                                                                                CellSpacing=3
                                                                                                Width="150%"
                                                                                                CssClass="font9Tahoma">
                                                                                                <AlternatingItemStyle
                                                                                                    CssClass="mr-r" />
                                                                                                <ItemStyle
                                                                                                    CssClass="mr-l" />
                                                                                                <HeaderStyle
                                                                                                    CssClass="mr-h" />

                                                                                                <Columns>
                                                                                                    <asp:TemplateColumn
                                                                                                        HeaderText="Customer"
                                                                                                        HeaderStyle-HorizontalAlign=Center>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label
                                                                                                                ID="lblRcvCustomer"
                                                                                                                runat="server"
                                                                                                                Text='<%# Container.DataItem("CustNameView") %>'>
                                                                                                            </asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle
                                                                                                            HorizontalAlign="Left"
                                                                                                            Width="15%" />
                                                                                                    </asp:TemplateColumn>

                                                                                                    <asp:TemplateColumn
                                                                                                        HeaderText="Contract No">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label
                                                                                                                ID="lblRcvContractNo"
                                                                                                                runat="server"
                                                                                                                Text='<%# Container.DataItem("ContractView") %>'>
                                                                                                            </asp:Label>                                                                                               
                                                                                                        </ItemTemplate>
                                                                                                        <HeaderStyle
                                                                                                            HorizontalAlign="Center" />
                                                                                                        <ItemStyle
                                                                                                            Width="5%" />
                                                                                                    </asp:TemplateColumn>
                                                                                                    <asp:TemplateColumn
                                                                                                        HeaderText="Contract Date">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label
                                                                                                                ID="lblRcvContractDate"
                                                                                                                Text='<%#objGlobal.GetLongDate(Container.DataItem("ContractDateView"))%>'
                                                                                                                runat="server" />
                                                                                                        </ItemTemplate>
                                                                                                        <HeaderStyle
                                                                                                            HorizontalAlign="Center" />
                                                                                                        <ItemStyle
                                                                                                            HorizontalAlign="Center"
                                                                                                            Width="5%" />
                                                                                                    </asp:TemplateColumn>

                                                                                                    <asp:TemplateColumn
                                                                                                        HeaderText="Contract <Br>Quantity">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label
                                                                                                                ID="lblRcvQtyContract"
                                                                                                                runat="server"
                                                                                                                Text='<%# FormatNumber(Container.DataItem("ContractQtyView"), 0) %>'
                                                                                                                Visible="True">
                                                                                                            </asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <HeaderStyle
                                                                                                            HorizontalAlign="Center" />
                                                                                                        <ItemStyle
                                                                                                            HorizontalAlign="Right"
                                                                                                            Width="5%" />
                                                                                                    </asp:TemplateColumn>                                                                                               
                                                                                                    
                                                                                                    <asp:TemplateColumn
                                                                                                        HeaderText="Price  <Br>(Rp)">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label
                                                                                                                ID="lblContractPrice"
                                                                                                                runat="server"
                                                                                                                Text='<%# FormatNumber(Container.DataItem("ContractPrice"), 0) %>'
                                                                                                                Visible="True">
                                                                                                            </asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <HeaderStyle
                                                                                                            HorizontalAlign="Center" />
                                                                                                        <ItemStyle
                                                                                                            HorizontalAlign="Right"
                                                                                                            Width="5%" />
                                                                                                    </asp:TemplateColumn>      

                                                                                                    <asp:TemplateColumn
                                                                                                        HeaderText="Contract <Br> Amount (Rp)">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label
                                                                                                                ID="lblContractAmount"
                                                                                                                runat="server"
                                                                                                                Text='<%# FormatNumber(Container.DataItem("ContractAmount"), 0) %>'
                                                                                                                Visible="True">
                                                                                                            </asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <HeaderStyle
                                                                                                            HorizontalAlign="Center" />
                                                                                                        <ItemStyle
                                                                                                            HorizontalAlign="Right"
                                                                                                            Width="5%" />
                                                                                                    </asp:TemplateColumn>
                                                                                                     
                                                                                                    <asp:TemplateColumn
                                                                                                        HeaderText="Qty <Br> Dispatch (Kg)">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label
                                                                                                                ID="lblQtyDispatch"
                                                                                                                runat="server"
                                                                                                                Text='<%# FormatNumber(Container.DataItem("QtyWeightBridge"), 0) %>'
                                                                                                                Visible="True">
                                                                                                            </asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <HeaderStyle
                                                                                                            HorizontalAlign="Center" />
                                                                                                        <ItemStyle
                                                                                                            HorizontalAlign="Right"
                                                                                                            Width="5%" />
                                                                                                    </asp:TemplateColumn>

                                                                                                       <asp:TemplateColumn
                                                                                                        HeaderText="%">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label
                                                                                                                ID="lblQtyPersenDispatch"
                                                                                                                runat="server"
                                                                                                                Text='<%# FormatNumber(Container.DataItem("QtyPersenDispatch"), 0) %>'
                                                                                                                Visible="True">
                                                                                                            </asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <HeaderStyle
                                                                                                            HorizontalAlign="Center" />
                                                                                                        <ItemStyle
                                                                                                            HorizontalAlign="Center"
                                                                                                            Width="5%" />
                                                                                                    </asp:TemplateColumn>

                                                                                                    <asp:TemplateColumn
                                                                                                        HeaderText="Invoice No"
                                                                                                        HeaderStyle-HorizontalAlign=Center>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label
                                                                                                                ID="lblRcvInvoiceID"
                                                                                                                runat="server"
                                                                                                                Text='<%# Container.DataItem("InvoiceView") %>'>
                                                                                                            </asp:Label>
                                                                                                            <asp:Label
                                                                                                                ID="lblRcvInvoiceHiden"
                                                                                                                runat="server"
                                                                                                                Text='<%# Container.DataItem("InvoiceNo") %>'
                                                                                                                Visible="false">
                                                                                                            </asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle
                                                                                                            Width="5%" />
                                                                                                    </asp:TemplateColumn>

                                                                                                    <asp:TemplateColumn
                                                                                                        HeaderText="Invoice Date">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label
                                                                                                                ID="lblRcvInvoiceDate"
                                                                                                                Text='<%#objGlobal.GetLongDate(Container.DataItem("InvoiceDate"))%>'
                                                                                                                runat="server" />
                                                                                                        </ItemTemplate>
                                                                                                        <HeaderStyle
                                                                                                            HorizontalAlign="Center" />
                                                                                                        <ItemStyle
                                                                                                            HorizontalAlign="Center"
                                                                                                            Width="5%" />
                                                                                                    </asp:TemplateColumn>

                                                                                                    <asp:TemplateColumn
                                                                                                        HeaderText="PPN"
                                                                                                        HeaderStyle-HorizontalAlign=Center>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label
                                                                                                                ID="lblRcvInvoicePPN"
                                                                                                                runat="server"
                                                                                                                Text='<%# Container.DataItem("PPN") %>'>
                                                                                                            </asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <HeaderStyle
                                                                                                            HorizontalAlign="Center" />
                                                                                                        <ItemStyle
                                                                                                            HorizontalAlign="Center"
                                                                                                            Width="5%" />                                                                              
                                                                                                    </asp:TemplateColumn>

                                                                                                    <asp:TemplateColumn
                                                                                                        HeaderText="Invoice <Br>Amount">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label
                                                                                                                ID="lblRcvINvAmt"
                                                                                                                runat="server"
                                                                                                                Text='<%# FormatNumber(Container.DataItem("InvoiceAmmountView"), 2S) %>'
                                                                                                                Visible="True">
                                                                                                            </asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <HeaderStyle
                                                                                                            HorizontalAlign="Center" />
                                                                                                        <ItemStyle
                                                                                                            HorizontalAlign="Right"
                                                                                                            Width="5%" />
                                                                                                    </asp:TemplateColumn>

                                                                                                    <asp:TemplateColumn
                                                                                                        HeaderText="Receipt No"
                                                                                                        HeaderStyle-HorizontalAlign=Center>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label
                                                                                                                ID="lblRcvID"
                                                                                                                runat="server"
                                                                                                                Text='<%# Container.DataItem("ReceiptID") %>'>
                                                                                                            </asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle
                                                                                                            Width="5%" />
                                                                                                    </asp:TemplateColumn>

                                                                                                    <asp:TemplateColumn
                                                                                                        HeaderText="Receipt Date">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label
                                                                                                                ID="lblRcvDate"
                                                                                                                Text='<%#objGlobal.GetLongDate(Container.DataItem("ReceiptDate"))%>'
                                                                                                                runat="server" />
                                                                                                        </ItemTemplate>
                                                                                                        <HeaderStyle
                                                                                                            HorizontalAlign="Center" />
                                                                                                        <ItemStyle
                                                                                                            HorizontalAlign="Center"
                                                                                                            Width="5%" />
                                                                                                    </asp:TemplateColumn>

                                                                                                    <asp:TemplateColumn
                                                                                                        HeaderText="Receipt Ammount">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label
                                                                                                                ID="lblRcvAmount"
                                                                                                                runat="server"
                                                                                                                Text='<%# FormatNumber(Container.DataItem("ReceiptAmount"), 2) %>'
                                                                                                                Visible="True">
                                                                                                            </asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <HeaderStyle
                                                                                                            HorizontalAlign="Center" />
                                                                                                        <ItemStyle
                                                                                                            HorizontalAlign="Right"
                                                                                                            Width="5%" />
                                                                                                    </asp:TemplateColumn>

                                                                                                    <asp:TemplateColumn
                                                                                                        HeaderText="Outstanding Receipt">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label
                                                                                                                ID="lblRcvOSt"
                                                                                                                runat="server"
                                                                                                                Text='<%# FormatNumber(Container.DataItem("Balance"), 2) %>'
                                                                                                                Visible="True">
                                                                                                            </asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <HeaderStyle
                                                                                                            HorizontalAlign="Center" />
                                                                                                        <ItemStyle
                                                                                                            HorizontalAlign="Right"
                                                                                                            Width="10%" />
                                                                                                    </asp:TemplateColumn>

                                                                                                </Columns>
                                                                                            </asp:DataGrid>
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan=5>&nbsp;</td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="3">
                                                                                        <asp:CheckBox id="chkExpRcv"
                                                                                            text=" Export To Excel"
                                                                                            checked="True" Visible=True
                                                                                            runat="server" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan=5>&nbsp;</td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan=5>
                                                                                        <asp:ImageButton id="ExportRcv"
                                                                                            Visible=True
                                                                                            AlternateText="Print Preview"
                                                                                            imageurl="../../images/butt_print_preview.gif"
                                                                                            OnClick="ExportRcv_onClick"
                                                                                            runat="server" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="3">
                                                                                        <asp:Label id=Label2
                                                                                            ForeColor=red
                                                                                            Font-Italic=true
                                                                                            visible=false
                                                                                            runat=server />
                                                                                        <asp:Label id=lblErrMessage
                                                                                            ForeColor=red
                                                                                            Font-Italic=true
                                                                                            visible=false
                                                                                            runat=server />
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
                                                <td colspan=5>&nbsp;</td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </form>
                </body>

                </html>