<%@ Page Language="vb" src="../../../include/AP_trx_CJList.aspx.vb" Inherits="ap_trx_CJList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuAP" src="../../menu/menu_aptrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
<head>
	<title>Creditor Journal List</title>
	<Preference:PrefHdl id=PrefHdl runat="server" />
</head>
	<body>
	    <form id=frmCJList runat="server">
			<asp:Label id=lblErrMesage visible=false Text="Error while initiating component." runat=server />			
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:Label id=SortCol Visible=False Runat="server" />
			<table border="0" cellspacing="1" cellpadding="1" width="100%">
				<tr>
					<td colspan="6"><UserControl:MenuAP id=MenuAP runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="3">CREDITOR JOURNAL LIST</td>
					<td colspan="3" align=right><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<td colspan=6 width=100% class="mb-c">
						<table width="100%" cellspacing="0" cellpadding="3" border="0">
							<tr class="mb-t">
								<td valign=bottom width=20%>Creditor Journal ID :<BR><asp:TextBox id=txtCJID width=100% maxlength="32" runat="server" /></td>
								<td valign=bottom width=25%>Supplier Code :<BR><asp:TextBox id=txtSupplier width=100% maxlength="20" runat="server"/></td>
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
								<td valign=bottom width=15%>Status :<BR><asp:DropDownList id="ddlStatus" width=100% runat=server /></td>
								<td valign=bottom width=20%>Last Updated By :<BR><asp:TextBox id=txtLastUpdate width=100% maxlength="128" runat="server"/></td>
								<td valign=bottom width=10% align=right><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click runat="server"/></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan=6>					
						<asp:DataGrid id=dgCreditorJournal
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
								<asp:BoundColumn Visible=False HeaderText="Creditor Journal ID" DataField="CreditJrnID" />
								<asp:HyperLinkColumn HeaderText="Creditor Journal ID" 
													 SortExpression="CJ.CreditJrnID" 
													 DataNavigateUrlField="CreditJrnID" 
													 DataNavigateUrlFormatString="ap_trx_CJDet.aspx?cjid={0}" 
													 DataTextField="CreditJrnID" />
								
								<asp:HyperLinkColumn HeaderText="Supplier Code" 
													 SortExpression="SUPP.SupplierCode" 
													 DataNavigateUrlField="CreditJrnID" 
													 DataNavigateUrlFormatString="ap_trx_CJDet.aspx?cjid={0}" 
													 DataTextField="SupplierCode" />
													 
								<asp:TemplateColumn HeaderText="Supplier Name" SortExpression="SUPP.SupplierName">
									<ItemTemplate>
										<%# Container.DataItem("SupplierName") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Type" SortExpression="CJ.JrnType">
									<ItemTemplate>
										<%# objAPTrx.mtdGetCreditorJournalType(Container.DataItem("JrnType"))%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Last Update" SortExpression="CJ.UpdateDate">
									<ItemTemplate>
										<%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Status" SortExpression="CJ.Status">
									<ItemTemplate>
										<%# objAPTrx.mtdGetCreditorJournalStatus(Container.DataItem("Status")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Updated By" SortExpression="USR.UserName">
									<ItemTemplate>
										<%# Container.DataItem("UserName") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemTemplate>
										<asp:label id=idCJId visible="false" text=<%# Container.DataItem("CreditJrnID")%> runat="server" />
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
						<asp:ImageButton id=NewCJBtn UseSubmitBehavior="false" onClick=NewCJBtn_Click imageurl="../../images/butt_new.gif" AlternateText="New Creditor Journal" runat=server/>
						<asp:ImageButton id=ibPrint UseSubmitBehavior="false" imageurl="../../images/butt_print.gif" AlternateText=Print visible=false runat=server/>
					</td>
				</tr>
			</table>
		</FORM>
	</body>
</html>
