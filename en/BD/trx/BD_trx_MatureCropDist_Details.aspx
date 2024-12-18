<%@ Page Language="vb" trace="false" SmartNavigation="True" src="../../../include/BD_trx_MatureCropDist_Details.aspx.vb" Inherits="BD_MatureCropDist_Details" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuBDtrx" src="../../menu/menu_BDTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Mature Crop Activity Calenderisation</title>
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
		<body>
		    <form runat="server" ID="frmMain" >
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id=lblBudgetingErr visible=false text="No active budgeting period." runat=server />
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:label id="sortcol" Visible="False" Runat="server" />
			<asp:label id="lbloper" Visible="False" Runat="server" />
			<asp:label id=lblBudgeting visible=false text="Budgeting " runat=server />
			<asp:label id=lblPeriod visible=false runat=server />
			<asp:label id=lblCode visible=false text=" Code" runat=server />
			<Input type=hidden id=hidBlkCode value="" runat=server />
			<table border="0" cellspacing="1" cellpadding="1" width="100%" runat=server >
				<tr>
					<td colspan="6"><UserControl:MenuBDTrx id=menuBD runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="4" width=60%>MATURE CROP ACTIVITY CALENDERISATION</td>
					<td align="right" colspan="2" width=40%><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<td colspan="4" width=60%><asp:label id="lblLocTag" runat="server"/> : <asp:label id="lblLocCode" runat="server"/></td>
					<td align="right" colspan="2" width=40%>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="4" width=60%><asp:label id="lblBgtTag" text="Budgeting Period"  runat="server"/> : <asp:label id="lblBgtPeriod" runat="server"/></td>
					<td align="right" colspan="2" width=40%>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="4" width=60%><asp:label id="lblBlockTag" runat="server"/> : <asp:label id="lblBlkCode" runat="server"/></td>
					<td align="right" colspan="2" width=40%></td>
				</tr>		
				<tr id="RowSubBlk">
					<td colspan="4" width=60%><asp:label id="lblSubBlkTag" runat="server"/> : <asp:label id="lblSubBlkCode" runat="server" /></td>
					<td align="right" colspan="2" width=40%>&nbsp;</td>
				</tr>
				<tr id="RowPlantingYr">
					<td colspan="4" width=60%><asp:label id="lblPlantYrTag" text="Planting Year" runat="server"/> : <asp:label id="lblYearPlanted" runat="server"/></td>
					<td align="right" colspan="2" width=40%>&nbsp;</td>
				</tr>								
				<tr>
					<td colspan="4" width=60%>&nbsp;</td>
					<td align="right" colspan="2" width=40%>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<TD colspan = 6 ></td>
				</tr>
			</table>
			<table cellpadding="2" cellspacing="0" runat="server">
				<tr id="trPeriodTitle" runat="server">
					<td style="width:100px;" nowrap>&nbsp;</td>
					<td style="width:200px;" nowrap>&nbsp;</td>
					<td style="width:100px;" nowrap>&nbsp;</td>
					<td style="width:100px;" nowrap>&nbsp;</td>
					<td style="width:50px;" nowrap>&nbsp;</td>
				</tr>
			</table>
			<asp:DataGrid id="dgMatureCropDist"
				AutoGenerateColumns="false" runat="server"
				GridLines = both
				OnEditCommand="DEDR_Edit"	
				Cellpadding = "2"
				Pagerstyle-Visible="False"
				AllowSorting="True">
				<HeaderStyle font-bold=true CssClass="mr-h" />							
				<ItemStyle CssClass="mr-l" />
				<AlternatingItemStyle CssClass="mr-r" />										
				<Columns>
					<asp:TemplateColumn>
						<ItemStyle Width="300px" />						
						<ItemTemplate>
							<asp:label id="lblAccCode" width="100px" Text='<%# trim(Container.DataItem("AccCode")) %>' runat="server"/>
							<asp:label id="lblMatureCropDistID" visible="false" Text='<%# trim(Container.DataItem("MatureCropDistID")) %>' runat="server"/>
						</ItemTemplate>
					</asp:TemplateColumn>																																		
					<asp:TemplateColumn HeaderText="Description">
						<ItemStyle Width="800px" />						
						<ItemTemplate>
							<asp:label id="lblDesc" width="200px" text='<%# Container.DataItem("AccDesc") %>' runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>																																		
					<asp:TemplateColumn HeaderText="Year Budget" HeaderStyle-HorizontalAlign="Right">
						<ItemStyle Width="80px" HorizontalAlign="Right" />						
						<ItemTemplate>
							<asp:label id="lblCost" width="100px" text='<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("YearBudget"), 0, True, False, False)) %>' runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Total Add Vote" HeaderStyle-HorizontalAlign="Right">
						<ItemStyle Width="80px" HorizontalAlign="Right" />						
						<ItemTemplate>
							<asp:label id="lblAddVote" width="100px" text='<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("TotalAddVote"), 0, True, False, False)) %>' runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn >
						<ItemStyle Width="100px" />						
						<ItemStyle HorizontalAlign="Center" />						
						<ItemTemplate>
							<asp:LinkButton width="25px" id="Edit" CommandName="Edit" Text="Edit" runat="server"/>
						</ItemTemplate>						
					</asp:TemplateColumn>					
				</Columns>
			</asp:DataGrid>		
		</FORM>
	</body>
</html>
