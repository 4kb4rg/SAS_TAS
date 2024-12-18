<%@ Page Language="vb" src="../../../include/PU_trx_HistoricalPrices.aspx.vb" Inherits="PU_HistoricalPrices" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPU" src="../../menu/menu_putrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Purchase Order List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<form id=frmPOList runat="server"  class="main-modul-bg-app-list-pu"> 
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:Label id=SortCol Visible=False Runat="server" />
		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuPU id=menuPU runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>HISTORICAL PRICES LIST</strong><hr style="width :100%" />   
                            </td>
                            
						</tr>
                        <tr>
                            <td align="right"><asp:label id="lblTracker" runat="server" /></td> 
                        </tr>
						<tr>
							<td style="background-color:#FFCC00">
							
							<table cellpadding="4" cellspacing="0" style="width: 100%">
								<tr class="font9Tahoma">
									<td valign="bottom">Item Code/Name<br /><asp:TextBox id=txtItemCode width=200px  runat="server"/></td>
                                    <td valign="bottom"><input type=button value=" ... " id="FindIN" onclick="javascript:PopItem('frmPOList', '', 'txtItemCode', 'False');" CausesValidation=False runat=server /></td>
									<td valign="bottom">Services<br /><asp:TextBox id=txtService width=100px runat="server"/></td>
									<td valign="bottom">Product Category<br /><asp:DropDownList id="ddlProdCat" width=200px runat=server /></td>
									<td valign="bottom">Supplier Code/Name<br /><asp:TextBox id=txtSuppCode width=200px runat="server"/></td>
                                    <td valign="bottom"><input type=button value=" ... " id="Find" onclick="javascript:PopSupplier('frmPOList', '', 'txtSuppCode', 'False');" CausesValidation=False runat=server /></td>
									<td valign="bottom" class="cell-right" style="width:100%"><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
								</tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
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
							OnSortCommand=Sort_Grid  class="font9Tahoma"
							AllowSorting=True>
								
							<HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							<ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							<AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
							
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
									<td><img height="18px" src="images/btfirst.png" width="18px" class="button" /></td>
						            <td><asp:ImageButton id="btnPrev" runat="server" imageurl="../../images/icn_prev.gif" alternatetext="Previous" commandargument="prev" onClick="btnPrevNext_Click" /></td>
						            <td><asp:DropDownList id="lstDropList" AutoPostBack="True" onSelectedIndexChanged="PagingIndexChanged" runat="server" /></td>
			         	            <td><asp:Imagebutton id="btnNext" runat="server"  imageurl="../../images/icn_next.gif" alternatetext="Next" commandargument="next" onClick="btnPrevNext_Click" /></td>
									<td><img height="18px" src="images/btlast.png" width="18px" class="button" /></td>



								</tr>
							</table>
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
		</FORM>
	</body>
</html>
