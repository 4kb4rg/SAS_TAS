<%@ Page Language="vb" trace="False" SmartNavigation="True" src="../../../include/BD_trx_VehRunDist_Details.aspx.vb" Inherits="BD_VehRunDist_Det" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuBDtrx" src="../../menu/menu_BDTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Vehicle Running</title>
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
		<body>
		    <form runat="server" ID="Form1">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id=lblExpenditure visible=false text=" Expenditure" runat=server />
			<asp:label id=lblBudgetingErr visible=false text="No active budgeting period." runat=server />
			<asp:label id="lblVeh" Visible="False" Runat="server" />
			<asp:label id="lblVehType" Visible="False" Runat="server" />
			<asp:label id=lblPeriod visible=false runat=server />
			<asp:Label id=lblBgtStatus Visible="False"  runat="server" />
			<asp:label id=lblCode visible=false text=" Code" runat=server />
			<table border="0" cellspacing="1" cellpadding="1" width="100%">
				<tr>
					<td colspan="6"><UserControl:MenuBDtrx id=menuBD runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="4" width=60%><asp:label id=lblVRATitle runat=server /></td>
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
					<td colspan="3" width=60%><asp:label id="lblVehTag" runat="server"/> : <asp:label id="lblVehCode" runat="server"/></td>
					<td colspan="3" width=40%><asp:label id="lblDescTag" runat="server"/> : <asp:label id="lblDesc" runat="server"/></td>
				</tr>
				<tr>
					<td colspan="3" width=60%><asp:label id="lblPdateTag" text="Purchase Date " runat="server"/> : <asp:label id="lblPDate" runat="server"/></td>
					<td colspan="3" width=40%><asp:label id="lblHPCCTag" text="HP/CC " runat="server"/> : <asp:label id="lblHPCC" runat="server"/></td>
				</tr>
				<tr>
					<td colspan="3" width=60%><asp:label id="lblModelTag" text="Model " runat="server"/> : <asp:label id="lblModel" runat="server"/></td>
					<td colspan="3" width=40%><asp:label id="lblUOMTag" text="UOM Code " runat="server"/> : <asp:label id="lblUOM" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="4" width=60%><asp:label id="lblTitle" runat="server" /></td>
					<td align="right" colspan="2" width=40%><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="4" width=60%>&nbsp;</td>
					<td align="right" colspan="2" width=40%><asp:Button id="lbtn_Recalc" Text="Calculate Formula" runat="server"/></td>
				</tr>
				<tr>
					<td align="left" ColSpan=6>
						<asp:Label id=lblOvrMsgTop visible=false Text="Calculation Error please Check Data" Forecolor=red runat=server />
					</td>
				</tr>				
				<tr>
					<TD colspan = 6 >					
					<asp:DataGrid id="dgVehicle"
						AutoGenerateColumns="false" width="100%" runat="server"
						OnItemDataBound="DataGrid_ItemDataBound" 
						Cellpadding = "2"
						OnEditCommand="DEDR_Edit" >
						<HeaderStyle CssClass="mr-h" font-bold=true />							
						<Columns>			
							<asp:TemplateColumn HeaderText="No.">
								<HeaderStyle HorizontalAlign="Left"  CssClass="mr-h" />							
								<ItemTemplate>
									<asp:label width=10px id="lblIdx" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>

							<asp:TemplateColumn >
								<ItemStyle Width="150px" />
								<ItemTemplate>
									<asp:Label id="lblVehExp" Width="150px" Text=<%# Container.DataItem("VehExpenseCode") %> runat="server" /> 
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Item Description" >
								<ItemStyle Width="200px" />
								<ItemTemplate>
									<asp:label width=200px id="lblItem" text=<%# Container.DataItem("ItemDescription") %> Runat="server" />
									<asp:label id="lblDispType" text=<%# Trim(Container.DataItem("ItemdisplayType")) %> Visible=False Runat="server" />
									<asp:label id="lblVehRunDistID" text=<%# Container.DataItem("VehRunDistID") %> visible=false Runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>	
							<asp:TemplateColumn HeaderText="Year Budget">
								<HeaderStyle HorizontalAlign="Right" />							
								<ItemStyle Width=100px HorizontalAlign="Right" />						
								<ItemTemplate>
									<asp:label width=100px text='<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("YearBudget"), 0, True, False, False)) %>' runat=server />
									<asp:label id="lblCost" width=100px Text='<%# FormatNumber(Container.DataItem("YearBudget"),0, True, False, False) %>' visible=false runat=server />
								</ItemTemplate>
							</asp:TemplateColumn>												
							<asp:TemplateColumn >
								<HeaderStyle HorizontalAlign="Center" />			
								<ItemStyle Width=25px HorizontalAlign="Center" />						
								<ItemTemplate>
									<asp:LinkButton width=25px id="Edit" CommandName="Edit" Text="Edit" runat="server" />
								</ItemTemplate>							
							</asp:TemplateColumn>
						</Columns>
					</asp:DataGrid>
					</td>
				</tr>
				<tr>
					<td align="left" ColSpan=6>
						<asp:Label id=lblOvrMsg visible=false Text="Calculation Error please Check Data" Forecolor=red runat=server />
					</td>
				</tr>
			</table>
		</FORM>
	</body>
</html>
