<%@ Page Language="vb" trace=false src="../../../include/BI_trx_JournalList.aspx.vb" Inherits="BI_trx_JournalList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuBI" src="../../menu/menu_bitrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
<head>
<title>Accounts Receivables - Debtor Journal List</title>
<Preference:PrefHdl id=PrefHdl runat="server" />
</head>
	<body>
	    <form id=frmMain runat="server">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />			
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:Label id=SortCol Visible=False Runat="server" />
			<asp:label id="lblCode" Visible="False" text=" Code" Runat="server" />
			<table border="0" cellspacing="1" cellpadding="1" width="100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuBI id=MenuBI runat="server" />
					</td>
				</tr>
				<tr>
					<td class="mt-h" colspan="3">DEBTOR JOURNAL LIST</td>
					<td colspan="3" align=right><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<td colspan=6 width=100% class="mb-c">
						<table width="100%" cellspacing="0" cellpadding="3" border="0" align="center">
							<tr class="mb-t">
								<td valign=bottom width=20%>Debtor Journal ID :<BR><asp:TextBox id=txtDebJournalID width=100% maxlength="32" runat="server" /></td>
								<td valign=bottom width=20%><asp:label id=lblBillPartyTag runat=server /> : <BR><asp:TextBox id=txtBillParty width=100% maxlength="20" runat="server" /></td>
								<td valign=bottom width=15%>Journal Type :<BR><asp:DropDownList id="ddlJournalType" width=100% runat=server /></td>
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
								<td valign=bottom width=15%>Status :<BR><asp:DropDownList id="ddlStatus" width=100% runat=server /></td>
								<td valign=bottom width=20%><BR><asp:TextBox id=txtLastUpdate width=100% maxlength="128" Visible=false runat="server"/></td>
								<td valign=bottom width=10% align=right><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click runat="server"/></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan=6>					
						<asp:DataGrid id=dgLine
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
								<asp:HyperLinkColumn HeaderText="Debtor Journal ID" 
													 SortExpression="DebtorJrnID" 
													 DataNavigateUrlField="DebtorJrnID" 
													 DataNavigateUrlFormatString="BI_trx_JournalDet.aspx?jrnid={0}" 
													 DataTextField="DebtorJrnID" />
								<asp:HyperLinkColumn SortExpression="BillPartyCode" 
													 DataNavigateUrlField="DebtorJrnID" 
													 DataNavigateUrlFormatString="BI_trx_JournalDet.aspx?jrnid={0}" 
													 DataTextField="BillPartyCode" />
								<asp:TemplateColumn HeaderText="Customer Name" SortExpression="BI.Name">
									<ItemTemplate>
										<%#Container.DataItem("CustName")%>
									</ItemTemplate>
								</asp:TemplateColumn>					 
								<asp:TemplateColumn HeaderText="Journal Type" SortExpression="DJ.JrnType">
									<ItemTemplate>
										<asp:Label Text=<%# objBITrx.mtdGetDebtorJournalType(Container.DataItem("JrnType")) %> id="lblJrnType" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
							
								<asp:TemplateColumn HeaderText="Last Update" SortExpression="DJ.UpdateDate">
									<ItemTemplate>
										<%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Status" SortExpression="DJ.Status">
									<ItemTemplate>
										<%# objBITrx.mtdGetDebtorJournalStatus(Container.DataItem("Status")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Updated By" SortExpression="USR.UserName">
									<ItemTemplate>
										<%# Container.DataItem("UserName") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemTemplate>
										<asp:label id=lblDebtorJrnID visible="false" text=<%# Container.DataItem("DebtorJrnID")%> runat="server" />
										<asp:label id=lblJournalType visible="false" text=<%# Container.DataItem("JrnType")%> runat="server" />
										<asp:label id="lblStatus" Text='<%# Trim(Container.DataItem("Status")) %>' Visible="False" Runat="server" />
										<asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server />
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
					<td align="left" width="80%" ColSpan=6>
						<asp:ImageButton id=NewBtn onClick=NewBtn_Click imageurl="../../images/butt_new.gif" AlternateText="New Debtor Journal" runat=server/>
						<!--<asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print visible=false/>
						<a href="#" onclick="javascript:popwin(200, 400, 'BI_trx_PrintDocs.aspx?doctype=1')"><asp:Image id="ibPrintDoc" runat="server" ImageUrl="../../images/butt_print_doc.gif"/></a>-->
					</td>
				</tr>
			</table>
		</FORM>
	</body>
</html>
