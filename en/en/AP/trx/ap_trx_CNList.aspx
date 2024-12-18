<%@ Page Language="vb" src="../../../include/AP_trx_CNList.aspx.vb" Inherits="ap_trx_CNList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuAP" src="../../menu/menu_aptrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
<head>
	<title>Supplier Credit Note List</title>
	<Preference:PrefHdl id=PrefHdl runat="server" />
</head>
	<body>
	    <form id=frmCNList runat="server">
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<table border="0" cellspacing="1" cellpadding="1" width="100%">
				<tr>
					<td colspan="6"><UserControl:MenuAP id=MenuAP runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="3">SUPPLIER CREDIT NOTE LIST</td>
					<td colspan="3" align=right><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<td colspan=6 width=100% class="mb-c">
						<table width="100%" cellspacing="0" cellpadding="3" border="0" align="center">
							<tr class="mb-t">
								<td valign=bottom width=12%>Credit Note ID :<BR><asp:TextBox id=txtCNID width=100% maxlength="32" runat="server" /></td>
								<td valign=bottom width=18%>Credit Note Ref. No. :<BR><asp:TextBox id=txtCNRefNoID width=100% maxlength="20" runat="server" /></td>
								<td valign=bottom width=20%>Supplier Code :<BR><asp:TextBox id=txtSupplier width=100% maxlength="20" runat="server"/></td>
								<td valign=bottom width=18%><asp:Label id=lblInvoiceRcvRefNo runat=server /> :<BR><asp:TextBox id=txtInvoiceRcvRefNo maxlength="32" width=100%  runat="server"/></td>
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
								<td valign=bottom width=12%>Status :<BR><asp:DropDownList id="ddlStatus" width=100% runat=server /></td>
								<td valign=bottom width=18%><BR><asp:TextBox id=txtLastUpdate width=100% maxlength="128" Visible=false runat="server"/></td>
								<td valign=bottom width=10% align=right><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click runat="server"/></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan=6>					
						<asp:DataGrid id=dgCreditNote
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
								<asp:BoundColumn Visible=False HeaderText="Credit Note ID" DataField="CreditNoteID" />
								<asp:HyperLinkColumn HeaderText="Credit Note ID" 
													 SortExpression="CN.CreditNoteID" 
													 DataNavigateUrlField="CreditNoteID" 
													 DataNavigateUrlFormatString="ap_trx_CNDet.aspx?cnid={0}" 
													 DataTextField="CreditNoteID" />

								<asp:HyperLinkColumn HeaderText="Credit Note Ref. No." 
													 SortExpression="CN.SupplierDocRefNo" 
													 DataNavigateUrlField="CreditNoteID" 
													 DataNavigateUrlFormatString="ap_trx_CNDet.aspx?cnid={0}" 
													 DataTextField="SupplierDocRefNo" />
								
								<asp:TemplateColumn HeaderText="Supplier Code" SortExpression="SUPP.SupplierCode">
									<ItemTemplate>
										<%# Container.DataItem("SupplierCode") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Supplier Name" SortExpression="SUPP.SupplierName">
									<ItemTemplate>
										<%# Container.DataItem("SupplierName") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn SortExpression="CN.InvoiceRcvRefNo">
									<ItemTemplate>
										<%# Container.DataItem("InvoiceRcvRefNo") %>
									</ItemTemplate>
								</asp:TemplateColumn>
							
								<asp:TemplateColumn HeaderText="Last Update" SortExpression="CN.UpdateDate">
									<ItemTemplate>
										<%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Status" SortExpression="CN.Status">
									<ItemTemplate>
										<%# objAPTrx.mtdGetCreditNoteStatus(Container.DataItem("Status")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Updated By" SortExpression="USR.UserName">
									<ItemTemplate>
										<%# Container.DataItem("UserName") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemTemplate>
										<asp:label id=idCNId visible="false" text=<%# Container.DataItem("CreditNoteID")%> runat="server" />
										<asp:label id="lblStatus" Text='<%# Trim(Container.DataItem("Status")) %>' Visible="False" Runat="server" />
										<asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>	
							</Columns>
						</asp:DataGrid><br>
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
						<asp:ImageButton id=NewCNBtn UseSubmitBehavior="false" onClick=NewCNBtn_Click imageurl="../../images/butt_new.gif" AlternateText="New Credit Note" runat=server/>
						<asp:ImageButton id=ibPrint UseSubmitBehavior="false" imageurl="../../images/butt_print.gif" AlternateText=Print visible=false runat=server/>
						<asp:Label id=SortCol Visible=False Runat="server" />
						<asp:Label id=lblErrMesage visible=false Text="Error while initiating component." runat=server />
					</td>
				</tr>
			</table>
		</FORM>
	</body>
</html>
