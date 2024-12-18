<%@ Page Language="vb" trace="false" SmartNavigation="True" src="../../../include/BD_trx_VehTypeDist_Details.aspx.vb" Inherits="BD_VehTypeDist_Details" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuBDtrx" src="../../menu/menu_BDTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Vehicle Type Distribution</title>
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
		<body>
		    <form runat="server" ID="frmMain" >
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:label id="sortcol" Visible="False" Runat="server" />
			<asp:label id="lbloper" Visible="False" Runat="server" />
			<asp:label id=lblBudgeting visible=false text="Budgeting " runat=server />
			<asp:label id=lblYrPlant visible=false runat=server />
			<asp:label id=lblBlkTotalArea visible=false runat=server />
			<asp:label id=lblTotalFFB visible=false runat=server />
			<asp:Label id=lblBgtStatus Visible="False"  runat="server" />
			<asp:label id="lblCode" text=" Code" Visible="False" Runat="server" />
			<table border="0" cellspacing="1" cellpadding="1" width="100%" >
				<tr>
					<td colspan="6"><UserControl:MenuBDTrx id=menuBD runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="4" width=60%>VEHICLE TYPE DISTRIBUTION</td>
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
					<td colspan="4" width=60%>&nbsp;</td>
					<td align="right" colspan="2" width=40%>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<TD colspan = 6 ></td>
				</tr>
				<tr>
					<TD colspan = 6 ><asp:label id=lblErrUsage visible=false forecolor=red text="Vehicle Usage cannot be zero !" runat=server /></td>
				</tr>
			</table>
			<asp:DataGrid id="dgVehTypeDist"
				AutoGenerateColumns="false" runat="server"
				OnItemDataBound="DataGrid_ItemDataBound" 
				GridLines = both
				Cellpadding = "2"
				OnEditCommand="DEDR_Edit"	
				OnUpdateCommand="DEDR_Update"
				OnCancelCommand="DEDR_Cancel"											
				Pagerstyle-Visible="False"
				AllowSorting="True">
				<HeaderStyle CssClass="mr-h" />							
				<ItemStyle CssClass="mr-l" />
				<AlternatingItemStyle CssClass="mr-r" />										
				<Columns>
					<asp:TemplateColumn HeaderText="Activity">
						<ItemStyle Width="800px" />						
						<ItemTemplate  >
							<asp:label id="lblID" width=25px visible=false Text='<%# trim(Container.DataItem("VehTypeDistSetID")) %>' runat="server"/>
							<asp:label id="lblActVeh" width=150px Text='<%# Container.DataItem("Activity") %>' runat="server"/>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:label id="lblID" width=25px visible=false Text='<%# trim(Container.DataItem("VehTypeDistSetID")) %>' runat="server"/>
							<asp:label id="lblAct" width=150px Text='<%# trim(Container.DataItem("Activity")) %>' runat="server"/>
							<asp:label id="lblVehTypeCode" width=25px visible=false Text='<%# trim(Container.DataItem("VehTypeCode")) %>' runat="server"/>
						</EditItemTemplate>
					</asp:TemplateColumn>																																		
					<asp:TemplateColumn HeaderText="Vehicle">
						<ItemStyle Width=800px />						
						<ItemTemplate>
							<asp:label id="lblVehType" width=150px Text='<%# Container.DataItem("VehType") %>' runat="server"/>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:label id="lblVeh" width=150px Text='<%# trim(Container.DataItem("VehType")) %>' runat="server"/>
						</EditItemTemplate>
					</asp:TemplateColumn>																																		
					<asp:TemplateColumn HeaderText="Cost Parameter">
						<ItemStyle Width=80px HorizontalAlign=Right/>						
						<ItemTemplate>
							<asp:label id="lblParam" width=80px text='<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("Parameter"), 0, True, False, False))%>' runat=server />
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox id="txtParam" width=80px  MaxLength="19"
								Text='<%# FormatNumber(Container.DataItem("Parameter"),0, True, False, False) %>'	
								runat="server"/>
							<asp:RequiredFieldValidator id=validateParam display=dynamic runat=server 
									text = "This field is required"
									ControlToValidate=txtParam />															
							<asp:RegularExpressionValidator id="RegExpValParam" 
								ControlToValidate="txtParam"
								ValidationExpression="\d{1,19}"
								Display="Dynamic"
								text = "Maximum length 19 digits and 0 decimal points"
								runat="server"/>
							<asp:RangeValidator id="RangeParam"
								ControlToValidate="txtParam"
								MinimumValue="0.00001"
								MaximumValue="9999999999999999999"
								Type="double"
								EnableClientScript="True"
								Text="The value is out of range !"
								runat="server" display="dynamic"/><BR>
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn >
						<ItemStyle Width=100px />						
						<ItemStyle HorizontalAlign="Center" />						
						<ItemTemplate>
							<asp:LinkButton id="Edit" CommandName="Edit" Text="Edit" runat="server"/>
						</ItemTemplate>						
						<EditItemTemplate>						
							<asp:LinkButton id="Update" CommandName="Update" Text="Save & Next"	runat="server"/>
							<asp:LinkButton id="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation=False runat="server"/>							
						</EditItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:DataGrid>
			<asp:label id=lblErrMessage2 visible=false forecolor=red text="Error in updating database (overflow) !" runat=server />
		</FORM>
	</body>
</html>
