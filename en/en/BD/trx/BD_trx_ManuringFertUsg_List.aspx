<%@ Page Language="vb" trace="False" src="../../../include/BD_trx_ManuringFertUsg_List.aspx.vb" Inherits="BD_ManuringFertUsg_List" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuBDtrx" src="../../menu/menu_BDTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Manuring Schedule Summary</title>
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
		<body>
		    <form runat="server" ID="Form1">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id=lblCode text=" Code" visible=false runat=server />
			
			<table border="0" cellspacing="1" cellpadding="1" width="100%">
				<tr>
					<td colspan="6"><UserControl:MenuBDtrx id=menuBD runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="4" width=60%><asp:label id=lblTitle runat=server />MANURING SCHEDULE SUMMARY</td>
					<td align="right" colspan="2" width=40%>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="4" width=60%><asp:label id="lblLocTag" runat="server"/> : <asp:label id="lblLocCode" runat="server"/></td>
					<td align="right" colspan="2" width=40%>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="4" width=60%><asp:label id="lblBgtTag" text="Budgeting Period " runat="server"/> : <asp:label id="lblBgtPeriod" runat="server"/></td>
					<td align="right" colspan="2" width=40%>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="4" width=60%><asp:label id="lblyrTag" text="Planting Year" runat="server"/> : <asp:label id="lblYearPlanted" runat="server"/></td>
					<td align="right" colspan="2" width=40%></td>
				</tr>				
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<TD colspan = 6 align=center>					
							<asp:Label id=lblPeriodErrTop Text="No active Budgeting Period" Forecolor=Red Visible=False runat=server />
					</td>				
				</tr>
				<tr>
					<TD colspan = 6 align=center>					
					<asp:DataGrid id="dgCode"
						AutoGenerateColumns="false" width="60%" runat="server"
						GridLines = none
						Cellpadding = "2"
						Pagerstyle-Visible="False">
						<HeaderStyle CssClass="mr-h" />							
						<ItemStyle CssClass="mr-l" />
						<AlternatingItemStyle CssClass="mr-r" />						
					<Columns>
					
					<asp:TemplateColumn HeaderText="Fertilizer Item Code">
						<ItemTemplate>
							<asp:LinkButton id="lbFertCode" Text=<%# Container.DataItem("FertItemCode") %> CommandArgument=<%# Container.DataItem("FertItemCode") %> onClick=btnFert_Click runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>
	
					<asp:TemplateColumn HeaderText="Description">
						<ItemTemplate>
							<asp:LinkButton id="lbFertDesc" Text=<%# Container.DataItem("Description") %> CommandArgument=<%# Container.DataItem("FertItemCode") %> onClick=btnFert_Click runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>
					</Columns>
					</asp:DataGrid>
					</td>
					</tr>
					<tr>
						<td ColSpan=6>&nbsp;</td>
					</tr>
                <tr>
					<TD colspan = 6 align=center>					
							<asp:Label id=lblPeriodErr Text="No active Budgeting Period" Forecolor=Red Visible=False runat=server />
					</td>
				</tr>						
			</table>
			<Input Type=Hidden id=hidCode runat=server />	
		</FORM>
	</body>
</html>
