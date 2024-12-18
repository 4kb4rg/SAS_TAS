<%@ Page Language="vb" CodeFile="../../../include/IN_trx_StockPosition_list.aspx.vb"
    Inherits="IN_StockPosisi" %>

<%@ Register TagPrefix="UserControl" TagName="MenuINTrx" Src="../../menu/menu_INTrx.ascx" %>
<%@ Register TagPrefix="Preference" TagName="PrefHdl" Src="../../include/preference/preference_handler.ascx" %>
<%@ Register TagPrefix="qsf" Namespace="Telerik.QuickStart" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<html>
<head>
    <title>Stock Issue List</title>
    <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
</head>
<Preference:PrefHdl ID="PrefHdl" runat="server" />
<script language="javascript">
    function ChkTrxType() {
        var doc = document.frmMain;
        var strDisplay = doc.Request.QueryString("type");
    }
</script>

<body>
    <form runat="server" id="frmMain" class="main-modul-bg-app-list-pu">
    <asp:Label ID="SQLStatement" Visible="False" runat="server"></asp:Label>
    <asp:Label ID="SortExpression" Visible="False" runat="server"></asp:Label>
    <asp:Label ID="blnUpdate" Visible="False" runat="server"></asp:Label>
    <asp:Label ID="lblErrMessage" Visible="false" Text="Error while initiating component."
        runat="server" />
    <asp:Label ID="curStatus" Visible="False" runat="server"></asp:Label>
    <asp:Label ID="sortcol" Visible="False" runat="server"></asp:Label>
    <table cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <%--<td class="sub-bg"  style="width: 100%; height: 800px" valign="top">
				<div class="konten">--%>
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td colspan="6">
                        <UserControl:MenuINTrx ID="menuIN" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; height: 800px" valign="top">
                        <div class="kontenlist">
                            <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
                                <tr>
                                    <td>
                                        <strong>
                                            <asp:Label ID="lblStkName" runat="server" /></strong><hr style="width: 100%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblTracker" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="background-color: #FFCC00">
                                        <table cellpadding="4" cellspacing="0" style="width: 100%">
                                            <tr class="font9Tahoma">
                                                <td width="15%" height="26">
                                                    Warehouse :<br>
                                                    <asp:DropDownList ID="lstStorage" CssClass="fontObject" Width="100%" runat="server" />
                                                </td>
                                                <td width="15%" height="26">
                                                    Item Code/ Name<br>
                                                    <asp:TextBox ID="srchItemCode" CssClass="fontObject" Width="80%" MaxLength="128"
                                                        Visible="true" runat="server" />
                                                    <input type="button" value=" ... " id="Find2" onclick="javascript:PopItem('frmMain', '', 'srchItemCode', 'False');"
                                                        causesvalidation="False" runat="server" />
                                                </td>
                                                <td width="15%" height="26">
                                                    Period :
                                                    <br />
                                                    <telerik:RadDatePicker ID="srchDateFr" runat="server" Culture="en-US">
                                                        <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x">
                                                        </Calendar>
                                                        <DateInput DisplayDateFormat="dd/MMM/yyyy" DateFormat="dd/MMM/yyyy" EnableSingleInputRendering="True"
                                                            LabelWidth="64px">
                                                        </DateInput>
                                                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                                    </telerik:RadDatePicker>
                                                </td>
                                                <td width="8%">
                                                    To :
                                                    <br />
                                                    <telerik:RadDatePicker ID="srchDateTo" runat="server" Culture="en-US">
                                                        <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x">
                                                        </Calendar>
                                                        <DateInput DisplayDateFormat="dd/MMM/yyyy" DateFormat="dd/MMM/yyyy" EnableSingleInputRendering="True"
                                                            LabelWidth="64px">
                                                        </DateInput>
                                                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                                    </telerik:RadDatePicker>
                                                </td>
                                    </td>
                                    <td width="10%">
                                    </td>
                                    <td width="15%" height="26">
                                    </td>
                                    <td width="1%" height="26">
                                        <br>
                                        <asp:TextBox ID="srchUpdateBy" Width="100%" MaxLength="128" Visible="false" runat="server" />
                                    </td>
                                    <td width="10%" height="26" valign="bottom" align="right">
                                        <asp:Button ID="Button1" Text="Search" OnClick="srchBtn_Click" runat="server" class="button-small" />
                                    </td>
                                </tr>
                            </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellpadding="4" cellspacing="1" style="width: 100%">
                            <tr>
                                <td>
                                    Saldo Awal :
                                    <asp:Label ID="lblSawal" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DataGrid ID="dgStockTx" 
                                        AutoGenerateColumns="False" Width="100%" 
                                        runat="server" CellPadding="2"
                                        PagerStyle-Visible="False" 
                                        OnItemDataBound="dgLine_BindGrid" AllowSorting="True"
                                        class="font9Tahoma">
                                        <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC" Font-Bold="False" Font-Italic="False"
                                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        <ItemStyle CssClass="mr-l" BackColor="#FEFEFE" Font-Bold="False" Font-Italic="False"
                                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE" Font-Bold="False" Font-Italic="False"
                                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="Document ID">
                                                <ItemTemplate>
                                                    <%#Container.DataItem("DocId")%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Doc Date">
                                                <ItemTemplate>
                                                    <%# objGlobal.GetLongDate(Container.DataItem("DocDate"))%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="U.O.M">
                                                <ItemTemplate>
                                                    <%# Container.DataItem("UomCode")%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Qty In">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("QtyIn"), 2), 2)%>'
                                                        ID="lblQtyIn" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Qty Out">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("QtyOut"), 2), 2)%>'
                                                        ID="lblQtyOut" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Balance">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("QtyEnd"), 2), 2)%>'
                                                        ID="lblQtyBalance" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>

<PagerStyle Visible="False"></PagerStyle>
                                    </asp:DataGrid>
                                    <br>
                                    <asp:Label ID="lblUnDel" Text="Insufficient Stock In Inventory to Perform Operation !"
                                        Visible="False" ForeColor="red" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellpadding="2" cellspacing="0" style="width: 100%">
                            <tr>
                                <td style="width: 100%">
                                    &nbsp;
                                </td>
                                <td>
                                    <img height="18px" src="../../../images/btfirst.png" width="18px" class="button" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnPrev" runat="server" AlternateText="Previous" CommandArgument="prev"
                                        ImageUrl="../../../images/btprev.png" OnClick="btnPrevNext_Click" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="lstDropList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="PagingIndexChanged" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnNext" runat="server" AlternateText="Next" CommandArgument="next"
                                        ImageUrl="../../../images/btnext.png" OnClick="btnPrevNext_Click" />
                                </td>
                                <td>
                                    <img height="18px" src="../../../images/btlast.png" width="18px" class="button" />
                                </td>
                                <asp:Label ID="lblCurrentIndex" Visible="false" Text="0" runat="server" />
                                <asp:Label ID="lblPageCount" Visible="false" Text="1" runat="server" />
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
            <%--		</div>
				</td>--%>
            <td>
                <table cellpadding="0" cellspacing="0" style="width: 20px">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
    </table>
    </div> </td>
    <td>
        <table cellpadding="0" cellspacing="0" style="width: 20px">
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </td>
    </tr> </table>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    </form>
</body>
</html>
