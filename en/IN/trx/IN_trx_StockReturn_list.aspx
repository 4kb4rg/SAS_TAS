<%@ Page Language="vb" src="../../../include/IN_Trx_StockReturn_List.aspx.vb" Inherits="IN_StockReturn" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuINTrx" src="../../menu/menu_INTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Stock Return List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>

	<Preference:PrefHdl id=PrefHdl runat="server" />
		<body>
		    <form runat="server" class="main-modul-bg-app-list-pu">
			<asp:label id="SQLStatement" Visible="False" Runat="server" />
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:label id="blnUpdate" Visible="False" Runat="server" />
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="curStatus" Visible="False" Runat="server" />
			<asp:label id="sortcol" Visible="False" Runat="server" />

		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuINTrx id=menuIN runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>STOCK RETURN LIST</strong><hr style="width :100%" />   
                            </td>
                            
						</tr>
                        <tr>
                            <td align="right"><asp:label id="lblTracker" runat="server" /></td> 
                        </tr>
						<tr>
							<td style="background-color:#FFCC00">
							
							<table cellpadding="4" cellspacing="0" style="width: 100%">
								<tr class="font9Tahoma">
								<td width="25%" valign=bottom height=26>Stock Return ID :<BR><asp:TextBox id=srchStockTxID width=100% maxlength="32" runat="server"/></td>
								<td width="30%" valign=bottom height=26>&nbsp;</td>
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
								<td width="15%" valign=bottom height=26>Status :<BR><asp:DropDownList id="srchStatusList" width=100% runat=server /></td>
								<td width="20%" valign=bottom height=26><BR><asp:TextBox id=srchUpdateBy width=100% maxlength="128" Visible=false runat="server"/></td>
								<td width="10%" valign=bottom height=26 align=right ><asp:Button ID="Button1" Text="Search" OnClick=srchBtn_Click runat="server" class="button-small" /></td>
								</tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
					<asp:DataGrid id="dgStockTx"
						AutoGenerateColumns="false" width="100%" runat="server"
						OnDeleteCommand="DEDR_Delete"
						GridLines = none
						Cellpadding = "2"
						AllowPaging="True" class="font9Tahoma"
						Allowcustompaging="False"
						Pagesize="15" 
						OnPageIndexChanged="OnPageChanged"
						Pagerstyle-Visible="False"
						OnSortCommand="Sort_Grid" 
                         OnItemDataBound="dgLine_BindGrid" 
						AllowSorting="True">
						<HeaderStyle CssClass="mr-h" BackColor="#CCCCCC" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<ItemStyle CssClass="mr-l" BackColor="#FEFEFE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>						
						<Columns>
							<asp:HyperLinkColumn
									HeaderText="Stock Return ID"
									DataNavigateUrlField="StockRtnID"
									DataNavigateUrlFormatString="IN_Trx_StockReturn_Details.aspx?id={0}"
									DataTextField="StockRtnID"
									DataTextFormatString="{0:c}"
									SortExpression="StockRtnID"/>
							<asp:TemplateColumn HeaderText="Total Amount" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
								<ItemTemplate>
									<%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("TtlAmount"), 2), 2)%>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Last Update" SortExpression="Rtn.UpdateDate" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
								<ItemTemplate>
									<%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Status" SortExpression="Rtn.Status" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
								<ItemTemplate>
									<%# objINtx.mtdGetStocktransferStatus(Container.DataItem("Status")) %>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Updated By" SortExpression="UserName" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
								<ItemTemplate>
									<%# Container.DataItem("UserName") %>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn>
								<ItemTemplate>
									<asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" Runat="server"/>
									<asp:label id="lblTxID" Text=<%# Container.DataItem("StockRtnID") %> Visible="False" Runat="server"></asp:label>
									<asp:label id="lblStatus" Text=<%# Container.DataItem("Status") %> Visible="False" Runat="server"></asp:label>
								</ItemTemplate>
							</asp:TemplateColumn>

						</Columns>
					</asp:DataGrid>
					<BR><asp:label id=lblUnDel text="Insufficient stock in Inventory to perform operation!" Visible=False forecolor=red Runat="server" />
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
							<table cellpadding="2" cellspacing="0" style="width: 100%">
								<tr>
									<td style="width: 100%">&nbsp;</td>
									<td><img height="18px" src="../../../images/btfirst.png" width="18px" class="button" /></td>
									<td><asp:ImageButton ID="btnPrev" runat="server" alternatetext="Previous" commandargument="prev" imageurl="../../../images/btprev.png" onClick="btnPrevNext_Click" /></td>
									<td><asp:DropDownList ID="lstDropList" runat="server" AutoPostBack="True" onSelectedIndexChanged="PagingIndexChanged" /></td>
									<td><asp:ImageButton ID="btnNext" runat="server" alternatetext="Next" commandargument="next" imageurl="../../../images/btnext.png" onClick="btnPrevNext_Click" /></td>
									<td><img height="18px" src="../../../images/btlast.png" width="18px" class="button" /></td>
								</tr>
							</table>
							</td>
						</tr>
						<tr>
							<td>
						        <asp:ImageButton id=btnreturn UseSubmitBehavior="false" imageurl="../../images/butt_new.gif" OnClick=btnNewItm_Click AlternateText="New Stock Issue" runat="server"/>
						        <asp:ImageButton id=ibPrint UseSubmitBehavior="false" AlternateText=Print imageurl="../../images/butt_print.gif" visible="false" runat="server"/>
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
				<td>
		<table cellpadding="0" cellspacing="0" style="width: 20px">
			<tr>
				<td>&nbsp;</td>
			</tr>
		</table>
				</td>
			</tr>
		</table>
		</Form>
		</body>
</html>
