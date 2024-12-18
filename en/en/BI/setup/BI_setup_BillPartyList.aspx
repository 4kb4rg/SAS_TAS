<%@ Page Language="vb" src="../../../include/BI_setup_BillPartyList.aspx.vb" Inherits="BI_setup_BillParty" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuBI" src="../../menu/menu_bisetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<html>
	<head>
		<title>Bill Party List</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
	    <form id=frmMain runat=server >
			<asp:Label id=lblErrMessage visible=false text="Error while initiating component." runat=server />
			<asp:Label id=SortExpression visible=false runat=server />
			<asp:Label id=SortCol visible=false runat=server />
			<asp:label id=lblCode visible=false text=" Code" runat="server" />
			<table border=0 cellspacing=1 cellpadding=1 width=100% >
				<tr>
					<td colspan=6><UserControl:MenuBI id=MenuBI runat=server /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="3"><asp:label id="lblTitle" runat="server" /> LIST</td>
					<td colspan="3" align=right><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<td colspan=6 width=100% class="mb-c">
						<table width="100%" cellspacing="0" cellpadding="3" border="0" align="center">
							<tr class="mb-t">
								<td width="20%" valign=bottom><asp:label id="lblBillParty" runat="server" /> Code :<BR><asp:TextBox id=txtBillPartyCode width=100% maxlength="20" runat="server" /></td>
								<td width="35%" valign=bottom><asp:label id="lblBillPartyName" runat="server" /> :<BR><asp:TextBox id=txtName width=100% maxlength="128" runat="server"/></td>
								<td width="15%" valign=bottom>Status :<BR>
									<asp:DropDownList id="ddlStatus" width=100% runat=server>
										<asp:ListItem value="0" >All</asp:ListItem>
										<asp:ListItem value="1" Selected>Active</asp:ListItem>
										<asp:ListItem value="2">Deleted</asp:ListItem>
									</asp:DropDownList>
								</td>
								<td width="20%" valign=bottom>Last Updated By :<BR><asp:TextBox id=txtLastUpdate width=100% maxlength=128 runat="server"/></td>
								<td width="10%" valign=bottom align=right><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click runat="server"/></td>
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
								<asp:HyperLinkColumn
									SortExpression="BP.BillPartyCode" 
									DataNavigateUrlField="BillPartyCode" 
									DataNavigateUrlFormatString="BI_setup_BillPartyDet.aspx?bpcode={0}" 
									DataTextField="BillPartyCode" />
								
								<asp:HyperLinkColumn
									SortExpression="BP.Name" 
									DataNavigateUrlField="BillPartyCode" 
									DataNavigateUrlFormatString="BI_setup_BillPartyDet.aspx?bpcode={0}" 
									DataTextField="Name" />
							
								<asp:TemplateColumn HeaderText="PPN" ItemStyle-Width="7%" SortExpression="PPNDescr">
									<ItemTemplate>
										<%# Container.DataItem("PPNDescr") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Last Update" SortExpression="BP.UpdateDate">
									<ItemTemplate>
										<%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Status" SortExpression="BP.Status">
									<ItemTemplate>
										<%# objGLSetup.mtdGetBillPartyStatus(Container.DataItem("Status")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Updated By" SortExpression="USR.UserName">
									<ItemTemplate>
										<%# Container.DataItem("UserName") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemTemplate>
										<asp:label id=idBPCode visible="false" text=<%# Container.DataItem("BillPartyCode")%> runat="server" />
										<asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server />
										<asp:Label id=lblStatus Text='<%# Trim(Container.DataItem("Status")) %>' Visible=False runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>	
							</Columns>
						</asp:DataGrid>
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
						<asp:ImageButton id=NewBillPartyBtn onClick=NewBillPartyBtn_Click imageurl="../../images/butt_new.gif" AlternateText="New Bill Party" runat="server"/>
						<asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print visible=false runat="server"/>
					</td>
				</tr>
			</table>
		</FORM>
	</body>
</html>
