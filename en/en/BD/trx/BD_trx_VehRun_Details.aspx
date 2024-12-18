<%@ Page Language="vb" trace="false" SmartNavigation="True" src="../../../include/BD_trx_VehRun_Details.aspx.vb" Inherits="BD_VehRun_Det" %>
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
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:label id=lblExpenditure visible=false text=" Expenditure" runat=server />
			<asp:label id="sortcol" Visible="False" Runat="server" />
			<asp:label id="lblVeh" Visible="False" Runat="server" />
			<asp:label id="lblVehType" Visible="False" Runat="server" />
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
						OnEditCommand="DEDR_Edit"
						OnUpdateCommand="DEDR_Update"
						OnCancelCommand="DEDR_Cancel">
						<HeaderStyle CssClass="mr-h" />							
					<Columns>
						<asp:TemplateColumn HeaderText="No.">
							<HeaderStyle HorizontalAlign="Left"  CssClass="mr-h" />							
							<ItemTemplate>
								<asp:label  id="lblIdx" runat="server" />
							</ItemTemplate>
							<EditItemTemplate>
								<asp:label  id="lblIdx" runat="server" />
							</EditItemTemplate>								
						</asp:TemplateColumn>
						<asp:TemplateColumn >
							<ItemStyle Width="24%" />
							<ItemTemplate>
								<asp:Label id="lblVehExp" Text=<%# Container.DataItem("VehExpenseCode") %> runat="server" /> 
							</ItemTemplate>
							<EditItemTemplate>
								<asp:Label id="lblVehExp" Text=<%# Container.DataItem("VehExpenseCode") %> runat="server" /> 
								<asp:label id="lblDispType" text=<%# Trim(Container.DataItem("ItemdisplayType")) %> Visible=false Runat="server"></asp:label>
								<asp:label id="lblSetID" text=<%# Trim(Container.DataItem("VehRunSetID")) %> Visible=false Runat="server"></asp:label>
								<asp:label id="lblDispCol" text=<%# Trim(Container.DataItem("ItemDisplayCol")) %> Visible=False Runat="server"></asp:label>																
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Item Description" >
						<ItemStyle Width="30%" />
						<ItemTemplate>
							<asp:label id="lblItem" text= <%# Container.DataItem("ItemDescription") %> Runat="server"></asp:label>
							<asp:label id="lblDispType" text=<%# Trim(Container.DataItem("ItemdisplayType")) %> Visible=false Runat="server"></asp:label>
							<asp:label id="lblDispCol" text=<%# Trim(Container.DataItem("ItemDisplayCol")) %> Visible=False Runat="server"></asp:label>																
						</ItemTemplate>
						<EditItemTemplate>
							<asp:label id="lblItem" text=<%# Container.DataItem("ItemDescription") %> Runat="server"></asp:label>
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Unit" >
						<HeaderStyle HorizontalAlign="Right" />			
						<ItemStyle  Width="12%" HorizontalAlign="Right" />						
						<ItemTemplate >
							<asp:Label id=lblQty Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("RunQty"),5) %> runat=server />
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox id="txtQty" width=100% MaxLength="15"
								Text='<%# FormatNumber(Container.DataItem("RunQty"),5, True, False, False) %>'
								runat="server"/>
							<asp:RequiredFieldValidator id=validateCurr display=dynamic runat=server 
									text = "Please enter budgeted units"
									ControlToValidate=txtQty />															
							<asp:RegularExpressionValidator id="RegExpValCurr" 
								ControlToValidate="txtQty"
								ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"
								Display="Dynamic"
								text = "Maximum length 9 digits and 5 decimal points"
								runat="server"/>
							<asp:RangeValidator id="RangeCurr"
								ControlToValidate="txtQty"
								MinimumValue="0"
								MaximumValue="999999999999999"
								Type="double"
								EnableClientScript="True"
								Text="The value is out of range !"
								runat="server" display="dynamic"/>
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Cost" >
						<HeaderStyle HorizontalAlign="Right" />			
						<ItemStyle  Width="12%" HorizontalAlign="Right" />						
						<ItemTemplate>
							<asp:Label id=lblCost Text=<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("RunCost"), 0, True, False, False)) %> runat=server />
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox id="txtCost" width=100% MaxLength="19"
								Text='<%# FormatNumber(Container.DataItem("RunCost"),0, True, False, False) %>'
								runat="server"/>
							<asp:RequiredFieldValidator id=validateCost display=dynamic runat=server 
									text = "Please enter budgeted units"
									ControlToValidate=txtCost />															
							<asp:RegularExpressionValidator id="RegExpValCost" 
								ControlToValidate="txtCost"
								ValidationExpression="\d{1,19}"
								Display="Dynamic"
								text = "Maximum length 19 digits and 0 decimal points"
								runat="server"/>
							<asp:RangeValidator id="RangeCost"
								ControlToValidate="txtCost"
								MinimumValue="-9999999999999999999"
								MaximumValue="9999999999999999999"
								Type="double"
								EnableClientScript="True"
								Text="The value is out of range !"
								runat="server" display="dynamic"/>
						</EditItemTemplate>
					</asp:TemplateColumn>					
					<asp:TemplateColumn HeaderText="Add. Vote" >
						<HeaderStyle HorizontalAlign="Right" />			
						<ItemStyle  Width="12%" HorizontalAlign="Right" />						
						<ItemTemplate>
							<asp:Label id=lblAddVote Text=<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("AddVote"), 0, True, False, False)) %> runat=server />
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox id="txtAddVote" width=100% MaxLength="25"
								Text='<%# FormatNumber(Container.DataItem("AddVote"),0, True, False, False) %>'
								runat="server"/>
							<asp:RequiredFieldValidator id=validateAddVote display=dynamic runat=server 
									text = "Please enter budgeted Cost"
									ControlToValidate=txtAddVote />															
							<asp:RegularExpressionValidator id="RegExpValAddVote" 
								ControlToValidate="txtAddVote"
								ValidationExpression="\d{1,19}"
								Display="Dynamic"
								text = "Maximum length 19 digits and 0 decimal points"
								runat="server"/>
							<asp:RangeValidator id="RangeAddVote"
								ControlToValidate="txtAddVote"
								MinimumValue="-9999999999999999999"
								MaximumValue="9999999999999999999"
								Type="double"
								EnableClientScript="True"
								Text="The value is out of range !"
								runat="server" display="dynamic"/>
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn >
						<HeaderStyle HorizontalAlign="Center" />			
						<ItemStyle  Width="10%" HorizontalAlign="Center" />						
						<ItemTemplate>
						<asp:LinkButton id="Edit" CommandName="Edit"   Text="Edit" CausesValidation=False
							runat="server"/>
						</ItemTemplate>
						<EditItemTemplate>
						<asp:LinkButton id="Update" CommandName="Update" Text="Save & Next"
							runat="server"/>
						<asp:LinkButton id="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation=False
							runat="server"/>
						</EditItemTemplate>
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
