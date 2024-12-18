<%@ Page Language="vb" Trace="false" codefile="../../../include/CB_trx_SaldoBank_list.aspx.vb"
    Inherits="CB_trx_SaldoBank_list" %>

<%@ Register TagPrefix="Preference" TagName="PrefHdl" Src="../../include/preference/preference_handler.ascx" %>
<html>
<head>
    <title>Bank Balance</title>
    <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
       <Preference:PrefHdl ID="PrefHdl" runat="server" />
</head>
<body>
    <form id="frmMain" runat="server" class="main-modul-bg-app-list-pu">
    <asp:Label ID="SortExpression" Visible="false" runat="server" />
    <asp:Label ID="SortCol" Visible="false" runat="server" />
    <input type="hidden" id="hidInit" value="" runat="server" />
    <asp:Label ID="lblErrMessage" Visible="false" Text="Error while initiating component."
        runat="server" />
    <table cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td colspan="6">
            </td>
        </tr>
        <tr>
            <td style="width: 100%; height: 800px" valign="top">
                <div class="kontenlist">
                    <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
                        <tr>
                            <td>
                                <strong>BANK BALANCE</strong><hr style="width: 100%" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="lblTracker" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <%-- <td colspan=6><hr size="1" noshade></td>--%>
                        </tr>
                        <tr>
                            <td style="background-color: #FFCC00">
                                <table cellpadding="4" cellspacing="0" style="width: 100%">
                                    <tr class="font9Tahoma">
                                        <td height="26" style="width: 14%">
                                            Date :<a href="javascript:PopCal('srcTgl');"><br>
                                                <asp:TextBox ID="srcTgl" Width="70%" MaxLength="20" CssClass="fontObject" runat="server" />
                                                <asp:Image ID="btnDateCreated" ImageAlign="AbsMiddle" runat="server" ImageUrl="../../Images/calendar.gif" />
                                             
                                        </td>
                                        <td height="26" style="width: 14%">
                                            Date :<a href="javascript:PopCal('srcTglTo');"><br>
                                                <asp:TextBox ID="srcTglTo" Width="70%" MaxLength="20" CssClass="fontObject" runat="server" />
                                                <asp:Image ID="Image1" ImageAlign="AbsMiddle" runat="server" ImageUrl="../../Images/calendar.gif" />
                                        </td>
                                        <td width="20%">
                                            Location :<asp:DropDownList ID="ddlLocation" CssClass="fontObject" runat="server" Width="100%">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="20%">
                                            Doc Status :<asp:DropDownList ID="ddlDocStatus" CssClass="fontObject" runat="server" Width="100%">
                                                <asp:ListItem Value="1" Selected>-All-</asp:ListItem>
                                                <asp:ListItem Value="2">Active/Verified excluded</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td width="20%" height="26">
                                            <br />
                                            <asp:Button ID="SearchBtn" Text="Search" OnClick="srchBtn_Click" runat="server" class="button-small" />
                                        </td>
                                        <td width="15%">
                                        </td>
                                        <td width="10%" valign="bottom" align="right">
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
                                            <asp:DataGrid ID="dgList" AutoGenerateColumns="false" Width="100%" runat="server"
                                                GridLines="Both" CellPadding="2" AllowCustomPaging="False"                                                          
                                                CellSpacing="1"
                                                AllowPaging="true"
                                                PageSize="15"
                                                OnItemCommand="MenuRefLink_Click"                      
                                                PagerStyle-Visible="false"
                                                OnSortCommand="Sort_Grid"
                                                 OnItemDataBound="dgList_BindGrid"
                                                AllowSorting="True" CssClass="font9Tahoma">
                                                <HeaderStyle CssClass="mr-h" />

                                                <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC" />
                                                <ItemStyle CssClass="mr-l" BackColor="#FEFEFE" />
                                                <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE" />
                                                <Columns>
                                                    
                                                    <asp:TemplateColumn HeaderText="Bank/Cash" SortExpression="Description">
                                                        <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDetail" CommandName="Detail" Text="View| "  runat="server" ToolTip="click to view bank detail" />
                                                            <asp:Label   ID="lblDescr" runat="server" Text='<%#Container.DataItem("Description")%>'></asp:Label>                                                            
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="COA" SortExpression="AccCode">
                                                        <ItemTemplate>
                                                            <asp:Label  ID="lblAccCode" runat="server" Text='<%#Container.DataItem("AccCode")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                        
                                                    <asp:TemplateColumn HeaderText="Begin" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBegin"   runat="server" Text='<%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("SaldoAwal"), 2), 2)%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Debit" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldebit"  runat="server" Text='<%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Debit"), 2), 2)%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Credit" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcredit"  runat="server" Text='<%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Credit"), 2), 2)%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="End" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblend"   runat="server" Text='<%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Saldo"), 2), 2)%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="font9Tahoma">
                                Transaction List
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" width="100%" class="mb-c">
                                <table width="100%" cellspacing="0" cellpadding="3" border="0" align="center">
                                    <tr class="font9Tahoma" style="background-color: #FFCC00">
 
                                        <td width="20%" height="26">
                                            <br />                                             
                                        </td>
                                        <td width="15%">
                                        </td>
                                        <td width="15%">
                                        </td>
                                        <td width="10%" valign="bottom" align="right">
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
                                            <asp:DataGrid ID="dgTrx" AutoGenerateColumns="false" Width="100%" runat="server"
                                                GridLines="Both" CellPadding="2" AllowCustomPaging="False" 
                                                PagerStyle-Visible="False"
                                                OnSortCommand="Sort_Grid" 
                                                OnItemCommand="DetailRefLink_Click"     
                                                OnItemDataBound="dgTrx_BindGrid" AllowSorting="false"
                                                class="font9Tahoma">
                                                <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC" />
                                                <ItemStyle CssClass="mr-l" BackColor="#FEFEFE" />
                                                <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE" />
                                                <Columns>
 
                                                    <asp:TemplateColumn HeaderText="Doc ID" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="center">
                                                    <ItemStyle Width="15%" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDetail" CommandName="Detail" Text='<%#Container.DataItem("NoVoucher")%>'  runat="server" ToolTip="click to view bank detail" />
                                                            <asp:Label ID="lblDocID"  runat="server" Visible="false" Text='<%#Container.DataItem("NoVoucher")%>'></asp:Label>
                                                            <asp:Label ID="lblpage"  runat="server" Visible="false" Text='<%#Container.DataItem("TransPage")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Payment To" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="center">
                                                    <ItemStyle Width="20%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPaymentTo" runat="server" Text='<%#Container.DataItem("FromTo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                   
                                                    <asp:TemplateColumn HeaderText="Date" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="center">
                                                    <ItemStyle Width="10%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDate"  runat="server" Text='<%#objGlobal.GetLongDate(Container.DataItem("TrxDate"))%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Remark" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="center">
                                                    <ItemStyle Width="40%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblremark"  runat="server" Text='<%#Container.DataItem("Keterangan")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Amount" HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemStyle Width="15%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAmount"  runat="server" Text='<%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Saldo"), 2), 2)%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid><br>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ImageButton ID="ibPrint" ImageUrl="../../images/butt_print.gif"  Visible="false" AlternateText="Print"
                                    runat="server" OnClick="btnPreview_Click" />
   
                                <asp:ImageButton ID="btnClear" ImageUrl="../../images/butt_clear.gif" Visible="false" AlternateText="Clear assignment"
                                    runat="server" OnClick="btnClear_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <table cellpadding="0" cellspacing="0" style="width: 20px">
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
      
        </tr>
    </table>
    </form>
</body>
</html>
