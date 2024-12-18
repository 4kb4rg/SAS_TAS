<%@ Page Language="vb" src="../../../include/ap_trx_InvRcvList.aspx.vb" Inherits="ap_trx_InvRcvList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuAP" src="../../menu/menu_aptrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
<head>
	<title>Invoice Receive List</title>
	<Preference:PrefHdl id=PrefHdl runat="server" />
</head>
	<body>
	    <form id=frmInvoiceList runat="server">
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:Label id=SortCol Visible=False Runat="server" />
			<asp:Label id=lblErrMesage visible=false Text="Error while initiating component." runat=server />
			<asp:label id=lblID visible=false text=" ID" runat=server />
			<table border="0" cellspacing="1" cellpadding="1" width="100%">
				<tr>
					<td colspan="6"><UserControl:MenuAP id=MenuAP runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="3"><asp:label id=lblTitle runat=server/> LIST</td>
					<td colspan="3" align=right><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<td colspan=6 width=100% class="mb-c">
						<table width="100%" cellspacing="0" cellpadding="3" border="0" align="center">
							<tr class="mb-t">
								<td valign=bottom width=15%><asp:label id=lblInvReceiveID runat=server/> :<BR><asp:TextBox id=txtInvoiceID width=100% maxlength="32" runat="server" /></td>
								<td valign=bottom width=20%>POID :<BR><asp:TextBox id=txtPOID width=100% maxlength="32" runat="server"/></td>
								<td valign=bottom width=25%>Supplier :<BR><asp:TextBox id=txtSupplier width=100% maxlength="20" runat="server"/></td>
								<td valign=bottom width=10%>Invoice Type :<BR><asp:DropDownList id="ddlInvoiceType" width=100% runat=server /></td>
								<td valign=bottom width=10%>Period :<BR>
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
								<td valign=bottom width=12%>Status :<BR><asp:DropDownList id="ddlStatus" width=100% runat=server /></td>
								<td valign=bottom width=15%><BR><asp:TextBox id=txtLastUpdate width=100% maxlength="128" Visible=false runat="server"/></td>
								<td valign=bottom width=8% align=right><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click runat="server"/></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan=6>					
						<asp:DataGrid id=dgInvList
							AutoGenerateColumns=false width=100% runat=server
							GridLines=none 
							Cellpadding=2 
							AllowPaging=True 
							Allowcustompaging=False 
							Pagesize=15 
							OnPageIndexChanged=OnPageChanged 
							Pagerstyle-Visible=False 
							OnDeleteCommand=DEDR_Delete 
							OnSortCommand=Sort_Grid  
							AllowSorting=True>
							<HeaderStyle CssClass="mr-h" />
							<ItemStyle CssClass="mr-l" />
							<AlternatingItemStyle CssClass="mr-r" />
							<Columns>
								<asp:HyperLinkColumn SortExpression="INV.InvoiceRcvID" 
													 DataNavigateUrlField="InvoiceRcvID" 
													 DataNavigateUrlFormatString="ap_trx_InvRcvDet.aspx?inrid={0}" 
													 DataTextField="InvoiceRcvID" />
								<asp:HyperLinkColumn HeaderText="POID" 
													 SortExpression="INV.POID" 
													 DataNavigateUrlField="InvoiceRcvID" 
													 DataNavigateUrlFormatString="ap_trx_InvRcvDet.aspx?inrid={0}" 
													 DataTextField="POID" />
								<asp:TemplateColumn HeaderText="Supplier Name" SortExpression="SUPP.SupplierName">
									<ItemTemplate>
										<%# Container.DataItem("SupplierName") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="TotalAmount" SortExpression="TotalAmount" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
									<ItemTemplate>
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("TotalAmount"), 2), 2) %> id="lblTotalAmount" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Invoice Type" SortExpression="INV.InvoiceType">
									<ItemTemplate>
										<%# objAPTrx.mtdGetInvoiceType(Container.DataItem("InvoiceType")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Due Date" SortExpression="INV.DueDate">
									<ItemTemplate>
										<%#objGlobal.GetLongDate(Container.DataItem("DueDate"))%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Tagihan No." SortExpression="NT.TrxID">
									<ItemTemplate>
										<%#Container.DataItem("TrxID")%>
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
										<asp:label id=idInvoiceRcvId visible="false" text=<%# Container.DataItem("InvoiceRcvID")%> runat="server" />
										<asp:label id=idPOID visible="false" text=<%# Container.DataItem("POID")%> runat="server" />
										<%# Container.DataItem("UserName") %>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:DataGrid><BR>
					</td>
				</tr>
				<tr>
					<td align=right colspan="6">
						<asp:ImageButton id="btnPrev" runat="server" imageurl="../../images/icn_prev.gif" alternatetext="Previous" commandargument="prev" onClick="btnPrevNext_Click" />
						<asp:DropDownList id="lstDropList" AutoPostBack="True" onSelectedIndexChanged="PagingIndexChanged" runat="server" />
			         	<asp:Imagebutton id="btnNext" runat="server"  imageurl="../../images/icn_next.gif" alternatetext="Next" commandargument="next" onClick="btnPrevNext_Click" />
					</td>
				</tr>
				<tr>
					<td align="left" width="100%" ColSpan=6>
						<asp:ImageButton id=NewInvRcv Visible=false UseSubmitBehavior="false" onClick=NewInvRcv_Click imageurl="../../images/butt_new.gif" AlternateText="New Invoice Receive" runat=server/>
						<asp:ImageButton id=ibPrint UseSubmitBehavior="false" imageurl="../../images/butt_print.gif" AlternateText=Print visible=false runat=server/>
						<a href="#" onclick="javascript:popwin(200, 600, 'AP_trx_PrintDocs.aspx?TrxID=')"><asp:Image id="ibPrintDoc" visible=false runat="server" ImageUrl="../../images/butt_print_doc.gif"/></a>
						<!--
						<asp:ImageButton ID="ConfirmBtn"  AlternateText="Confirm"  onclick="ConfirmBtn_Click"  ImageUrl="../../images/butt_confirm.gif"  CausesValidation=False Runat=server />
						-->
					</td>
				</tr>
			</table>
		</FORM>
	</body>
</html>
