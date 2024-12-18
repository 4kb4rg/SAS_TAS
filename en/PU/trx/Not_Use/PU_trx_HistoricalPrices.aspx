<%@ Page Language="vb" src="../../../include/PU_trx_HistoricalPrices.aspx.vb" Inherits="PU_HistoricalPrices" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPU" src="../../menu/menu_putrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Purchase Order List</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<form id=frmPOList runat="server">
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:Label id=SortCol Visible=False Runat="server" />
			<table border="0" cellspacing="1" cellpadding="1" width="100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuPU id=menuPU runat="server" />
					</td>
				</tr>
				<tr>
					<td class="mt-h" colspan="3">HISTORICAL PRICES LIST</td>
					<td align="right" colspan="3" ><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<td colspan=6 width=100% class="mb-c">
						<table width="100%" cellspacing="0" cellpadding="3" border="0" align="center">
							<tr class="mb-t">
								<td width="20%" height="26">Item Code/Name :<BR><asp:TextBox id=txtItemCode width=90%  runat="server"/>
								        <input type=button value=" ... " id="FindIN" onclick="javascript:PopItem('frmPOList', '', 'txtItemCode', 'False');" CausesValidation=False runat=server /></td>
								<td width="15%" height="26">Services :<BR><asp:TextBox id=txtService width=100% runat="server"/></td>
								<td width="15%" height="26">Product Category :<BR><asp:DropDownList id="ddlProdCat" width=100% runat=server /></td>
								<td width="20%" height="26">Supplier Code/Name :<BR><asp:TextBox id=txtSuppCode width=90% runat="server"/>
								        <input type=button value=" ... " id="Find" onclick="javascript:PopSupplier('frmPOList', '', 'txtSuppCode', 'False');" CausesValidation=False runat=server /></td>
								<td width="8%" height="26" valign=bottom align=right><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click runat="server"/></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan=6>
						<asp:DataGrid id=dgPOList runat=server
							AutoGenerateColumns=false width=100% 
							GridLines=none 
							Cellpadding=2 
							AllowPaging=True 
							Allowcustompaging=False 
							Pagesize=15 
							OnEditCommand="PreviewPO"
							OnPageIndexChanged=OnPageChanged 
							Pagerstyle-Visible=False 
							OnSortCommand=Sort_Grid  
							AllowSorting=True>
								
							<HeaderStyle CssClass="mr-h"/>
							<ItemStyle CssClass="mr-l"/>
							<AlternatingItemStyle CssClass="mr-r"/>
							
							<Columns>
								<asp:BoundColumn Visible=False DataField="POId" />
								
								<asp:TemplateColumn HeaderText="PO Date">
									<ItemTemplate>
										<%#objGlobal.GetLongDate(Container.DataItem("PODate"))%> 
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="POID">		
									<ItemTemplate>
										<asp:LinkButton id="POID" CommandName="Edit" Text=<%#Container.DataItem("POID")%> CausesValidation=False runat="server" />
										<asp:Label Text=<%# Container.DataItem("POID") %> id="lblPOID" Visible=false runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>	
								<asp:TemplateColumn HeaderText="Supplier">
									<ItemTemplate>
										<%#Container.DataItem("SupplierName")%>  <br />
										<%#"(" & Trim(Container.DataItem("SupplierCode")) & ")"%> 
										<asp:Label Text=<%# Container.DataItem("SupplierCode") %> id="lblSupplierCode" Visible=false runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Item">
									<ItemTemplate>
										<%#Container.DataItem("ItemDesc")%> <br />
										<%#"(" & Trim(Container.DataItem("ItemCode")) & ")"%>
										<asp:Label Text=<%# Container.DataItem("ItemCode") %> id="lblItemCode" Visible=false runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Unit Cost">
								    <HeaderStyle HorizontalAlign="Right" /> 
								    <ItemStyle HorizontalAlign="Right" />			
									<ItemTemplate>
										<%#objGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("CostToDisplay"), 2)%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Rate">
								    <HeaderStyle HorizontalAlign="Right" /> 
								    <ItemStyle HorizontalAlign="Right" />			
									<ItemTemplate>
									    <%#Container.DataItem("CurrencyCode")%> <%# " (" & objGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("ExchangeRate"), 2) & ")" %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Quantity">
								    <HeaderStyle HorizontalAlign="Right" /> 
								    <ItemStyle HorizontalAlign="Right" />			
									<ItemTemplate>
										<%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("QtyOrder"), 2), 2)%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="UOM">
									<ItemTemplate>
										<%#Container.DataItem("PurchaseUOM")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Service">
									<ItemTemplate>
										<%#Container.DataItem("Catatan")%>
										<asp:Label Text=<%# Container.DataItem("Catatan") %> id="lblService" Visible=false runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Service Item">
									<ItemTemplate>
										<%#Container.DataItem("AdditionalNote")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Location">
									<ItemTemplate>
										<%#Container.DataItem("CompLoc")%>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:DataGrid>
						<BR><asp:Label id=lblErrQtyReceive visible=false forecolor=red Text="Quantity received for this PO." runat=server />
					</td>
				</tr>
				<tr>
					<td align=right colspan="6">
						<asp:ImageButton id="btnPrev" runat="server" imageurl="../../images/icn_prev.gif" alternatetext="Previous" commandargument="prev" onClick="btnPrevNext_Click" />
						<asp:DropDownList id="lstDropList" AutoPostBack="True" onSelectedIndexChanged="PagingIndexChanged" runat="server" />
			         	<asp:Imagebutton id="btnNext" runat="server"  imageurl="../../images/icn_next.gif" alternatetext="Next" commandargument="next" onClick="btnPrevNext_Click" />
					</td>
				</tr>
			</table>
		</FORM>
	</body>
</html>
