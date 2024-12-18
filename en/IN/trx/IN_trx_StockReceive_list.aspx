<%@ Page Language="vb" src="../../../include/IN_Trx_StockReceive_List.aspx.vb" Inherits="IN_Receive" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuINTrx" src="../../menu/menu_INTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v7.3" Namespace="Infragistics.WebUI.UltraWebTab"
    TagPrefix="igtab" %>

<html>
	<head>
		<title>Stock Receive List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
		<body>
		    <form runat="server" class="main-modul-bg-app-list-pu">
			<asp:label id="SQLStatement" Visible="False" Runat="server"></asp:label>
			<asp:label id="SortExpression" Visible="False" Runat="server"></asp:label>
			<asp:label id="blnUpdate" Visible="False" Runat="server"></asp:label>
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="curStatus" Visible="False" Runat="server"></asp:label>
			<asp:label id="sortcol" Visible="False" Runat="server"></asp:label>
		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
						<tr>
							<td><strong>STOCK RECEIVE LIST</strong><hr style="width :100%" />   
                            </td>
                            
						</tr>
                        <tr>
                            <td align="right"><asp:label id="lblTracker" runat="server" /></td> 
                        </tr>
						<tr>
							<td style="background-color:#FFCC00">
							
							<table cellpadding="4" cellspacing="0" style="width: 100%">
								<tr class="font9Tahoma">
								<td width="20%" valign=bottom>Stock Receive ID :<BR><asp:TextBox id=srchStockTxID width=100% maxlength="32" runat="server"/></td>
								<td width="20%" valign=bottom>Stock Receive Ref. No :<BR><asp:TextBox id=srchRef width=100% maxlength="32" runat="server"/></td>
								<td width="20%" valign=bottom>Stock Receive Type :<BR><asp:DropDownList id=srchStockRcvType width=100% runat="server"/></td>
								<td valign=bottom width=8%>Period :<BR>
								    <asp:DropDownList id="lstAccMonth" width=100% runat=server>
										<asp:ListItem value="0" Selected>All</asp:ListItem>
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
								<td valign=bottom width=10%><BR>
								    <asp:DropDownList id="lstAccYear" width=100% runat=server>
									</asp:DropDownList>
								<td width="10%" valign=bottom>Status :<BR><asp:DropDownList id="srchStatusList" width=100% runat=server/></td>
								<td width="20%" valign=bottom><BR><asp:TextBox id=srchUpdateBy width=100% maxlength="128" Visible=false runat="server"/></td>
								<td width="10%" valign=bottom align=right><asp:Button ID="Button1"  Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
								</tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
                                        <igtab:UltraWebTab ID="TABBK" runat="server" 
                                            __designer:errorcontrol="Unable to locate license assembly." 
                                            Font-Names="Tahoma" Font-Size="8pt" ForeColor="black" SelectedTab="0" 
                                            TabOrientation="TopLeft" ThreeDEffect="False">
                                            <DefaultTabStyle Height="22px">
                                            </DefaultTabStyle>
                                            <HoverTabStyle CssClass="ContentTabHover">
                                            </HoverTabStyle>
                                            <RoundedImage FillStyle="LeftMergedWithCenter" 
                                                HoverImage="../../images/thumbs/ig_tab_winXP2.gif" LeftSideWidth="6" 
                                                NormalImage="../../images/thumbs/ig_tab_winXP3.gif" RightSideWidth="6" 
                                                SelectedImage="../../images/thumbs/ig_tab_winXP1.gif" />
                                            <SelectedTabStyle CssClass="ContentTabSelected">
                                            </SelectedTabStyle>
                                            <Tabs>
                                                <igtab:Tab Key="StockReceive" Text="STOCK RECEIVE LIST" 
                                                    Tooltip="STOCK RECEIVE LIST">
                                                    <ContentPane>
                                                        <table border="0" cellpadding="1" cellspacing="1" width="99%">
                                                            <tr>
                                                                <td colspan="6">
                                                                    <div ID="div1" style="height:300px;width:1100;overflow:auto;">
                                                                        <asp:DataGrid ID="dgStockTX" runat="server" Allowcustompaging="False" 
                                                                            AllowPaging="True" AllowSorting="True" AutoGenerateColumns="false" 
                                                                            Cellpadding="2" GridLines="none" OnDeleteCommand="DEDR_Delete" 
                                                                            OnPageIndexChanged="OnPageChanged" OnSortCommand="Sort_Grid" 
                                                                            OnItemDataBound="dgLine_BindGrid"
                                                                            Pagerstyle-Visible="False" Pagesize="15" width="100%" class="font9Tahoma" >
                                                                                <HeaderStyle  BackColor="#CCCCCC" Font-Bold="False" 
                                                                                    Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                                                                    Font-Underline="False"/>
							                                                    <ItemStyle BackColor="#FEFEFE" Font-Bold="False" 
                                                                                    Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                                                                    Font-Underline="False"/>
							                                                    <AlternatingItemStyle BackColor="#EEEEEE" Font-Bold="False" 
                                                                                    Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                                                                    Font-Underline="False"/>	
                                                                            <Columns>
                                                                                <asp:HyperLinkColumn DataNavigateUrlField="StockReceiveID" 
                                                                                    DataNavigateUrlFormatString="IN_Trx_StockReceive_Details.aspx?id={0}" 
                                                                                    DataTextField="StockReceiveID" DataTextFormatString="{0:c}" 
                                                                                    HeaderText="Stock Receive ID" SortExpression="StockReceiveID">
                                                                                </asp:HyperLinkColumn>
                                                                                <asp:HyperLinkColumn DataNavigateUrlField="StockReceiveID" 
                                                                                    DataNavigateUrlFormatString="IN_Trx_StockReceive_Details.aspx?id={0}" 
                                                                                    DataTextField="StockRefNo" DataTextFormatString="{0:c}" 
                                                                                    HeaderText="Stock Receive Ref. No" SortExpression="StockRefNo">
                                                                                </asp:HyperLinkColumn>
                                                                                <asp:HyperLinkColumn DataNavigateUrlField="StockReceiveID" 
                                                                                    DataNavigateUrlFormatString="IN_Trx_StockReceive_Details.aspx?id={0}" 
                                                                                    DataTextField="DocID" DataTextFormatString="{0:c}" HeaderText="Doc ID" 
                                                                                    SortExpression="DocID"></asp:HyperLinkColumn>
                                                                                <asp:TemplateColumn HeaderText="Stock Receive Type" 
                                                                                    SortExpression="tx.StockDocType">
                                                                                    <ItemTemplate>
									<%# objINtx.mtdGetStockReceiveDocType(Container.DataItem("StockDocType")) %>
								                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn HeaderText="Last Update" SortExpression="tx.UpdateDate">
                                                                                    <ItemTemplate>
									<%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
								                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn HeaderText="Status" SortExpression="tx.Status">
                                                                                    <ItemTemplate>
									<%# objINtx.mtdGetStocktransferStatus(Container.DataItem("Status")) %>
								                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn HeaderText="Updated By" SortExpression="UserName">
                                                                                    <ItemTemplate>
									<%# Container.DataItem("UserName") %>
								                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblTxID" Runat="server" 
                                                                                            Text='<%# Container.DataItem("StockReceiveID") %>' Visible="False"></asp:Label>
                                                                                        <asp:Label ID="lblStatus" Runat="server" 
                                                                                            Text='<%# Container.DataItem("Status") %>' Visible="False"></asp:Label>
                                                                                        <asp:LinkButton ID="Delete" Runat="server" CommandName="Delete" Text="Delete"></asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                            </Columns>
                                                                        </asp:DataGrid>
                                                                        <br><asp:Label ID="lblUnDel0" Runat="server" forecolor="red" 
                                                                            text="Insufficient Stock In Inventory to Perform Operation!" Visible="False"></asp:Label>
					   </br>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" colspan="6">
                                                                    <asp:ImageButton ID="btnPrev" runat="server" alternatetext="Previous" 
                                                                        commandargument="prev" imageurl="../../images/icn_prev.gif" 
                                                                        onClick="btnPrevNext_Click" />
                                                                    <asp:DropDownList ID="lstDropList" runat="server" AutoPostBack="True" 
                                                                        onSelectedIndexChanged="PagingIndexChanged">
                                                                    </asp:DropDownList>
                                                                    <asp:ImageButton ID="btnNext" runat="server" alternatetext="Next" 
                                                                        commandargument="next" imageurl="../../images/icn_next.gif" 
                                                                        onClick="btnPrevNext_Click" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" ColSpan="6">
                                                                    <asp:ImageButton ID="ibNew" runat="server" AlternateText="New Stock Receive" 
                                                                        imageurl="../../images/butt_new.gif" onClick="btnNewItm_Click" 
                                                                        UseSubmitBehavior="false" />
                                                                    <asp:ImageButton ID="ibPrint0" AlternateText="Print" 
                                                                        imageurl="../../images/butt_print.gif" onClick="btnPreview_Click" 
                                                                        UseSubmitBehavior="false" visible="false" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentPane>
                                                </igtab:Tab>
                                                <igtab:Tab Key="StockReceiveOst" Text="STOCK RECEIVE OUTSTANDING LIST" 
                                                    Tooltip="STOCK RECEIVE OUTSTANDING LIST">
                                                    <ContentPane>
                                                        <table border="0" cellpadding="1" cellspacing="1" width="99%">
                                                            <tr>
                                                                <td colspan="6">
                                                                    <h4>
                                                                        DISPATCH ADVICE</h6>
                                                                    <hr noshade size="1">
                                                                    <div ID="div2" style="height:200px;width:1100;overflow:auto;">
                                                                        <asp:DataGrid ID="dgStockRcvAdvOst" runat="server" AllowSorting="True" 
                                                                        OnItemDataBound="dgLine_BindGrid"
                                                                            AutoGenerateColumns="false" GridLines="none" width="100%" class="font9Tahoma" >
                                                                            <HeaderStyle  BackColor="#CCCCCC" Font-Bold="False" 
                                                                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                                                                Font-Underline="False"/>
							                                                <ItemStyle BackColor="#FEFEFE" Font-Bold="False" 
                                                                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                                                                Font-Underline="False"/>
							                                                <AlternatingItemStyle BackColor="#EEEEEE" Font-Bold="False" 
                                                                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                                                                Font-Underline="False"/>	
                                                                            <Columns>
                                                                                <asp:TemplateColumn HeaderText="Dispatch ID">
                                                                                    <ItemTemplate>
					                                                    <%#Container.DataItem("DispAdvID")%>
				                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="16%" />
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn HeaderText="Dispatch Date">
                                                                                    <ItemTemplate>
					                                                    <%#objGlobal.GetLongDate(Container.DataItem("DispDate"))%>
				                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="6%" />
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn HeaderText="Purchase Order ID">
                                                                                    <ItemTemplate>
					                                                    <%#Container.DataItem("POID")%>
				                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="15%" />
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn HeaderText="Purchase Requitition ID">
                                                                                    <ItemTemplate>
					                                                    <%#Container.DataItem("PRID")%>
				                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="15%" />
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn HeaderText="Item Code">
                                                                                    <ItemTemplate>
					                                                    <%#Container.DataItem("ItemCode")%>
				                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="8%" />
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn HeaderText="Description">
                                                                                    <ItemTemplate>
					                                                    <%#Container.DataItem("Description")%>
				                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="17%" />
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn HeaderText="Qty">
                                                                                    <ItemTemplate>
					                                                    <%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("QtyPurchase"), 2), 0)%>
				                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="4%" />
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn HeaderText="UOM">
                                                                                    <ItemTemplate>
					                                                    <%#Container.DataItem("UOMCode")%>
				                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="4%" />
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn HeaderText="Cost" Visible="false">
                                                                                    <ItemTemplate>
					                                                    <%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("AverageCost"), 2), 0)%>
				                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="5%" />
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn HeaderText="Update By">
                                                                                    <ItemTemplate>
					                                                    <%#Container.DataItem("UserName")%>
				                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="10%" />
                                                                                </asp:TemplateColumn>
                                                                            </Columns>
                                                                        </asp:DataGrid>
                                                                    </div>
                                                                    </hr>
                                                                </td>
                                                            </tr>
                                                        </table>
					           <br  />
					           <br  />
                                                        <table border="0" cellpadding="1" cellspacing="1" width="99%">
                                                            <tr>
                                                                <td colspan="6">
                                                                    <h4>
                                                                        STOCK TRANSFER</h6>
                                                                    <hr noshade size="1">
                                                                    <div ID="div3" style="height:200px;width:1100;overflow:auto;">
                                                                        <asp:DataGrid ID="dgStockTransOst" runat="server" AllowSorting="True" 
                                                                        OnItemDataBound="dgLine_BindGrid"  
                                                                            AutoGenerateColumns="false" GridLines="none" width="100%" class="font9Tahoma" >
                                                                                    <HeaderStyle  BackColor="#CCCCCC" Font-Bold="False" 
                                                                                        Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                                                                        Font-Underline="False"/>
							                                                        <ItemStyle BackColor="#FEFEFE" Font-Bold="False" 
                                                                                        Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                                                                        Font-Underline="False"/>
							                                                        <AlternatingItemStyle BackColor="#EEEEEE" Font-Bold="False" 
                                                                                        Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                                                                        Font-Underline="False"/>	
                                                                            <Columns>
                                                                                <asp:TemplateColumn HeaderText="Stock Transfer ID">
                                                                                    <ItemTemplate>
									<%#Container.DataItem("StockTransferID")%>
								                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="16%" />
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn HeaderText="Transfer Date">
                                                                                    <ItemTemplate>
									<%#objGlobal.GetLongDate(Container.DataItem("CreateDate"))%>
								                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="6%" />
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn HeaderText="Item Code">
                                                                                    <ItemTemplate>
									<%#Container.DataItem("ItemCode")%>
								                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="8%" />
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn HeaderText="Description">
                                                                                    <ItemTemplate>
									<%#Container.DataItem("Description")%>
								                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="17%" />
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn HeaderText="Qty">
                                                                                    <ItemTemplate>
									<%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Qty"), 2), 0)%>
								                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="4%" />
                                                                                </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="UOM">
                                                        <ItemTemplate>
                                                                        <%#Container.DataItem("UOMCode")%>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="4%" />
                                                        </asp:TemplateColumn>
                                                                                <asp:TemplateColumn HeaderText="Cost" Visible="False">
                                                                                    <ItemTemplate>
									<%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Cost"), 2), 0)%>
								                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="5%" />
                                                                                </asp:TemplateColumn>

                                                                                <asp:TemplateColumn HeaderText="Amount" Visible="False">
                                                                                    <ItemTemplate>
									<%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Amount"), 2), 0)%>
								                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="5%" />
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn HeaderText="Update By">
                                                                                    <ItemTemplate>
									<%#Container.DataItem("UserName")%>
								                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="10%" />
                                                                                </asp:TemplateColumn>
                                                                            </Columns>
                                                                        </asp:DataGrid>
                                                                    </div>
                                                                    </hr>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentPane>
                                                </igtab:Tab>
                                            </Tabs>
                                        </igtab:UltraWebTab>
                                    </td>
                                    </tr>
								</table>
							</td>
						</tr>
						<tr>
							<td>
							    &nbsp;</td>
						</tr>
						<tr>
							<td>
							<table cellpadding="2" cellspacing="0" style="width: 100%" class="font9Tahoma">
								<tr>
									<td style="width: 100%"><asp:label id=lblUnDel text="Insufficient Stock In Inventory to Perform Operation!" Visible=False forecolor=red Runat="server" />
                                    </td>
									<td>&nbsp;</td>
									<td>&nbsp;</td>
									<td>&nbsp;</td>
									<td>&nbsp;</td>
									<td>&nbsp;</td>
								</tr>
							</table>
							</td>
						</tr>
						<tr>
							<td>
						        &nbsp;<asp:ImageButton id=ibPrint UseSubmitBehavior="false" imageurl="../../images/butt_print.gif" AlternateText=Print onClick="btnPreview_Click" visible=false runat="server"/>
							</td>
						</tr>
                        <tr>
                            <td>
 					            &nbsp;</td>
                        </tr>
					</table>
				</div>
				</td>
				<td>
		<table cellpadding="0" cellspacing="0" style="width: 20px">
			<tr>
				<td>&nbsp;</td>
			</tr>
		</table>
				</td>
			</tr>
		</table>
		</FORM>
		</body>
</html>
