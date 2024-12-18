<%@ Page Language="vb" trace="false" SmartNavigation="True" src="../../../include/BD_trx_WSMechHourDist_Details.aspx.vb" Inherits="BD_trx_WSMechHourDist_Details" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuBDtrx" src="../../menu/menu_BDTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Workshop Mechanic Hour Calenderisation</title>
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
		<body>
		    <form runat="server" ID="frmMain" >
		    <asp:label id="lblCode" visible="false" text=" Code" runat="server" />
		    <asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:label id="SortCol" Visible="False" Runat="server" />
			<asp:label id="lblPeriod" visible="false" runat="server" />
			<asp:Label id="lblBgtStatus" visible="false" runat="server" />
			<table border="0" cellspacing="1" cellpadding="1" width="100%" runat="server" >
				<tr>
					<td colspan="6"><UserControl:MenuBDTrx id=menuBD runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="4" width=60%>WORKSHOP MECHANIC HOUR CALENDERISATION</td>
					<td align="right" colspan="2" width=40%>&nbsp;</td>
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
					<td colspan="4" width="60%">&nbsp;</td>
					<td align="right" colspan="2" width="40%">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6"><hr size="1" noshade></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
			</table>
			<table cellpadding="2" cellspacing="0" runat="server">
				<tr id="trPeriodTitle" runat="server">
					<td style="width:125px;" nowrap="true">&nbsp; <!-- Account Code --></td>
					<td style="width:125px;" nowrap="true">&nbsp; <!-- Description --></td>
					<td style="width:100px;" nowrap="true">&nbsp; <!-- Year Budget --></td>
					<td style="width:100px;" nowrap="true">&nbsp; <!-- Total Add Vote --></td>
					<td style="width:50px;" nowrap="true">&nbsp; <!-- Edit --></td>
				</tr>
			</table>
			<asp:DataGrid id="dgWSMechHourDist"
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
					<asp:TemplateColumn HeaderText="Employee Code">
						<ItemStyle Width="400px" />
						<ItemTemplate>
							<asp:label id="lblEmpCode" width="125px" text='<%# Container.DataItem("EmpCode") %>' runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Work Code">
						<ItemStyle Width="400px" />						
						<ItemTemplate>
							<asp:label id="lblWorkCode" width="125px" text='<%# Container.DataItem("WorkCode") %>' runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>																																	
					<asp:TemplateColumn HeaderText="Year Budget" HeaderStyle-HorizontalAlign="Right">
						<ItemStyle Width="80px" HorizontalAlign="Right" />						
						<ItemTemplate>
							<asp:label id="lblMechHour" width="100px" text='<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("MechHour"), 0, True, False, False)) %>' runat="server" />
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
