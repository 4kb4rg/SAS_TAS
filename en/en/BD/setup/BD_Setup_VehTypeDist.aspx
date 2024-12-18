<%@ Page Language="vb"  SmartNavigation="True"  trace="false" src="../../../include/BD_Setup_VehTypeDist.aspx.vb" Inherits="BD_VehTypeDist" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuBDsetup" src="../../menu/menu_BDSetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Vehicle Type Distribution</title>
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
	<body>
		<Form id=frmMain runat="server">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="lblOper" Visible="False" Runat="server" />
			<asp:label id="lblCode" text=" Code" Visible = false Runat="server"/>
			
			<table border=0 cellspacing=0 cellpadding=2 width=100%>
				<tr>
					<td colspan="5"><UserControl:MenuBDsetup id=menuBD runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="5">VEHICLE TYPE DISTRIBUTION</td>
				</tr>
				<tr>
					<td colspan=5><hr size="1" noshade></td>
				</tr>
				<tr>
					<td height=25><asp:label id="lblActTag" runat="server" /> :*</td>
					<td><asp:dropdownlist id=ddlAct width=100% runat=server/>
						<asp:RequiredFieldValidator id=rfvAct display=Dynamic runat=server 
								ErrorMessage="<br>Please select Activity Code."
								ControlToValidate=ddlAct />					
					</td>
					<td>&nbsp;</td>
					<td><asp:label id="lblBgtTag" text="Budgeting Period" runat="server" /> :</td>
					<td><asp:Label id=lblBgtPeriod runat=server /></td>
				</tr>
				<tr>
					<td><asp:label id="lblVehTypeTag" runat="server" /> :*</td>
					<td><asp:dropdownlist id=ddlVehType width=100% runat=server/>
						<asp:RequiredFieldValidator id=rfvVehType display=Dynamic runat=server 
								ErrorMessage="<br>Please select Vehicle Type Code."
								ControlToValidate=ddlVehType />
					</td>
					<td>&nbsp;</td>
					<td><asp:label id="lblLocTag" runat="server"/> :</td>
					<td><asp:label id="lblLocCode" runat="server"/></td>
				</tr>
				<tr>
					<td width=15%>&nbsp;</td>
					<td width=30%>&nbsp;</td>
					<td width=5%>&nbsp;</td>
					<td width=15%>&nbsp;</td>
					<td width=35% align=left>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=5><asp:label id="lblDupMsg" Visible = false forecolor=red Runat="server"/></td>
				</tr>				
				<tr>
					<TD colspan = 5 >					
					<asp:DataGrid id="dgVehDist"
						AutoGenerateColumns="false" width="100%" runat="server"
						GridLines = none
						Cellpadding = "2"
						OnEditCommand="DEDR_Edit"
						OnCancelCommand="DEDR_Cancel"
						OnDeleteCommand="DEDR_Delete"
						Pagerstyle-Visible="False"
						OnSortCommand="Sort_Grid" 
						AllowSorting="false">
						<HeaderStyle CssClass="mr-h" />							
						<ItemStyle CssClass="mr-l" />
						<AlternatingItemStyle CssClass="mr-r" />						
						<Columns>						
							<asp:TemplateColumn HeaderStyle-HorizontalAlign=Left ItemStyle-HorizontalAlign=Left SortExpression="ActCode">
								<ItemStyle Width="45%" />
								<ItemTemplate>
									<%# Container.DataItem("Act") %>
								</ItemTemplate>
								<EditItemTemplate>
									<asp:Label id="lblActCode" Text='<%# Container.DataItem("ActCode") %>' visible=false runat="server" />
									<asp:Label Text='<%# Container.DataItem("Act") %>' runat="server" />
								</EditItemTemplate>
							</asp:TemplateColumn>
			
							<asp:TemplateColumn HeaderStyle-HorizontalAlign=Left ItemStyle-HorizontalAlign=Left SortExpression="VehCode">
								<ItemStyle Width="45%" />
								<ItemTemplate>
									<%# Container.DataItem("Veh") %> 
								</ItemTemplate>
								<EditItemTemplate>
									<asp:Label id="lblVehTypeCode" Text='<%# Container.DataItem("VehTypeCode") %>' visible=false runat="server" />
									<asp:Label Text='<%# Container.DataItem("Veh") %>' runat="server" />
								</EditItemTemplate>
							</asp:TemplateColumn>
																		
							<asp:TemplateColumn>
								<ItemStyle Width="10%" />
								<ItemTemplate>
									<asp:LinkButton id="lbEdit" CommandName="Edit" Text="Edit" CausesValidation=False runat="server"/>
								</ItemTemplate>						
								<EditItemTemplate>						
									<asp:LinkButton id="lbDelete" CommandName="Delete" Text="Delete" CausesValidation=False runat="server"/>
									<asp:LinkButton id="lbCancel" CommandName="Cancel" Text="Cancel" CausesValidation=False runat="server"/>							
								</EditItemTemplate>
							</asp:TemplateColumn>
						</Columns>
					</asp:DataGrid>
					</td>
				</tr>					
				<tr>
					<td colspan="5"><asp:ImageButton id=ibAdd OnClick="DEDR_Add" imageurl="../../images/butt_add.gif" AlternateText="Add" runat="server"/></td>
				</tr>								
				<tr>
					<td colspan="5">&nbsp;</td>
				</tr>								
			</table>
		</form>
	</body>
</html>
