<%@ Page Language="vb" SmartNavigation="True" trace="false" src="../../../include/BD_trx_MatureCrop_Det_DistByBlock.aspx.vb" Inherits="BD_MatureCrop_Det_DistByBlock" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuBDtrx" src="../../menu/menu_BDTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Mature Crop</title>
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
		<body>
		    <form runat="server" ID="Form1">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id=lblAccCodeCol visible=false text=" Expenditure" runat=server />
			<asp:label id=lblCode visible=false text=" Code" runat=server />
			<Input type=hidden id=hidBlkCode value="" runat=server />
			<Input type=hidden id=hidSubBlkCode value="" runat=server />
			
			<table border="0" cellspacing="1" cellpadding="1" width="100%">
				<tr>
					<td colspan="6"><UserControl:MenuBDtrx id=menuBD runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="4" width=60%><asp:label id=lblTitleMatureCrop text="MATURE CROP ACTIVITY" runat=server /></td>
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
					<td colspan="4" width=60%><asp:label id="lblBlkTag" runat="server"/> : <asp:label id="lblBlkCode" runat="server"/></td>
					<td align="right" colspan="2" width=40%>&nbsp;</td>
				</tr>
				<!--<tr>
					<td colspan="4" width=60%><asp:label id="lblPlantYrTag" text="Planting Year" runat="server"/> : <asp:label id="lblYearPlanted" runat="server"/></td>
					<td align="right" colspan="2" width=40%>&nbsp;</td>
				</tr>-->			
				<tr>
					<td colspan="4" width=60%>Total Title Area Size : <asp:label id="lblTotalAreaFig" runat="server"/></td>
					<td align="right" colspan="2" width=40%>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="4" width=60%>Total Production : <asp:label id="lblTotalFFBFig" runat="server"/></td>
					<td align="right" colspan="2" width=40%>&nbsp;</td>
				</tr>												
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="4" width=60%><asp:label id="lblTitle" runat="server" /></td>
					<td align="right" colspan="2" width=40%><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<TD colspan = 6 >					
					<asp:DataGrid id="dgMatureCropDetDist"
						AutoGenerateColumns="false" width="100%" runat="server"
						OnItemDataBound="DataGrid_ItemDataBound" 
						Cellpadding = "2"
						OnEditCommand="DEDR_Edit"
						OnUpdateCommand="DEDR_Update"
						OnCancelCommand="DEDR_Cancel">
						<HeaderStyle font-bold=true CssClass="mr-h" />							
						<Columns>	
							<asp:TemplateColumn HeaderText="No.">
								<ItemStyle Width="5%" />
								<HeaderStyle HorizontalAlign="Left"  CssClass="mr-h" />							
								<ItemTemplate>
									<asp:label id="lblIdx" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn >
								<ItemStyle Width="15%" />
								<ItemTemplate>
									<asp:label id="lblAcc" Width="8%" text=<%# Trim(Container.DataItem("Acc")) %> Runat="server"></asp:label> 
								</ItemTemplate>
								<EditItemTemplate>
									<asp:label id="lblAcc" Width="8%" text=<%# Trim(Container.DataItem("Acc")) %> Runat="server"></asp:label> 										
								</EditItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Items">
								<ItemStyle Width="30%" />
								<ItemTemplate>
									<asp:label id="lblItem" text=<%# Trim(Container.DataItem("Item")) %> Runat="server"></asp:label> 
									<asp:label id="lblMatureCropSetID" text=<%# Container.DataItem("MatureCropSetID") %> Visible=False Runat="server"></asp:label>
									<asp:label id="lblDispType" text=<%# Trim(Container.DataItem("ItemdisplayType")) %> Visible=False Runat="server"></asp:label>																
									<asp:label id="lblDispCol" text=<%# Trim(Container.DataItem("ItemDisplayCol")) %> Visible=False Runat="server"></asp:label>																
								</ItemTemplate>
								<EditItemTemplate>
									<asp:label id="lblItem" text=<%# Trim(Container.DataItem("Item")) %> Runat="server"></asp:label> 
									<asp:label id="lblMatureCropSetID" text=<%# Container.DataItem("MatureCropSetID") %> Visible=False Runat="server"></asp:label>
									<asp:label id="lblDispType" text=<%# Trim(Container.DataItem("ItemdisplayType")) %> Visible=False Runat="server"></asp:label>																
									<asp:label id="lblDispCol" text=<%# Trim(Container.DataItem("ItemDisplayCol")) %> Visible=False Runat="server"></asp:label>																
								</EditItemTemplate>
							</asp:TemplateColumn>
										
							<asp:TemplateColumn HeaderText="Frequency" >
								<HeaderStyle HorizontalAlign="Right" />			
								<ItemStyle  Width="10%" HorizontalAlign="Right" />						
								<ItemTemplate>
									<asp:Label id=lblFreq Text=<%#  formatNumber(Container.DataItem("Frequency"),2) %> runat=server />
								</ItemTemplate>
								<EditItemTemplate>
									<asp:TextBox id="txtFreq" width=100% MaxLength="25"
										Text='<%# Container.DataItem("Frequency") %>'
										runat="server"/>
									<asp:RequiredFieldValidator id=validateFreq display=dynamic runat=server 
											text = "Please enter frequency"
											ControlToValidate=txtFreq />															
									<asp:RegularExpressionValidator id="RegExpValFreq" 
										ControlToValidate="txtFreq"
										ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
										Display="Dynamic"
										text = "Maximum length 15 digits and 5 decimal points"
										runat="server"/>
									<asp:RangeValidator id="RangeFreq"
										ControlToValidate="txtFreq"
										MinimumValue="1"
										MaximumValue="999999999999999"
										Type="double"
										EnableClientScript="True"
										Text="The value is out of range !"
										runat="server" display="dynamic"/>
								</EditItemTemplate>
							</asp:TemplateColumn>
							
							<asp:TemplateColumn HeaderText="Unit per Frequency" >
								<HeaderStyle HorizontalAlign="Right" />			
								<ItemStyle  Width="10%" HorizontalAlign="Right" />						
								<ItemTemplate>
									<asp:Label id=lblUnit Text=<%#  formatNumber(Container.DataItem("Unit"),2) %> runat=server />
								</ItemTemplate>
								<EditItemTemplate>
									<asp:TextBox id="txtUnit" width=100% MaxLength="25"
										Text='<%# Container.DataItem("Unit") %>'
										runat="server"/>
									<asp:RequiredFieldValidator id=validateUnit display=dynamic runat=server 
											text = "Please enter Unit"
											ControlToValidate=txtUnit />															
									<asp:RegularExpressionValidator id="RegExpValUnit" 
										ControlToValidate="txtUnit"
										ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
										Display="Dynamic"
										text = "Maximum length 15 digits and 5 decimal points"
										runat="server"/>
									<asp:RangeValidator id="RangeUnit"
										ControlToValidate="txtUnit"
										MinimumValue="0"
										MaximumValue="999999999999999"
										Type="double"
										EnableClientScript="True"
										Text="The value is out of range !"
										runat="server" display="dynamic"/>
								</EditItemTemplate>
							</asp:TemplateColumn>
										
							<asp:TemplateColumn HeaderText="Unit Cost" >
								<HeaderStyle HorizontalAlign="Right" />			
								<ItemStyle  Width="10%" HorizontalAlign="Right" />						
								<ItemTemplate>
									<asp:Label id=lblUnitCost Text=<%#  formatNumber(Container.DataItem("UnitCost"),2) %> runat=server />
								</ItemTemplate>
								<EditItemTemplate>
									<asp:TextBox id="txtUnitCost" width=100% MaxLength="25"
										Text='<%# Container.DataItem("UnitCost") %>'
										runat="server"/>
									<asp:RequiredFieldValidator id=validateUnitCost display=dynamic runat=server 
											text = "Please enter Unit Cost"
											ControlToValidate=txtUnitCost />															
									<asp:RegularExpressionValidator id="RegExpValUnitCost" 
										ControlToValidate="txtUnitCost"
										ValidationExpression="^[-]?\d{1,15}\.\d{1,5}|^[-]?\d{1,15}"
										Display="Dynamic"
										text = "Maximum length 15 digits and 5 decimal points"
										runat="server"/>
									<asp:RangeValidator id="RangeUnitCost"
										ControlToValidate="txtUnitCost"
										MinimumValue="-999999999999999"
										MaximumValue="999999999999999"
										Type="double"
										EnableClientScript="True"
										Text="The value is out of range !"
										runat="server" display="dynamic"/>
								</EditItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Mandays" >
								<HeaderStyle HorizontalAlign="Right" />			
								<ItemStyle  Width="10%" HorizontalAlign="Right" />						
								<ItemTemplate>
									<asp:Label id=lblMandays Text=<%#  formatNumber(Container.DataItem("Mandays"),2) %> runat=server />
								</ItemTemplate>
								<EditItemTemplate>
									<asp:TextBox id="txtMandays" width=100% MaxLength="25"
										Text='<%# Container.DataItem("Mandays") %>'
										runat="server"/>
									<asp:RequiredFieldValidator id=validateMandays display=dynamic runat=server 
											text = "Please enter Mandays"
											ControlToValidate=txtUnitCost />															
									<asp:RegularExpressionValidator id="RegExpValMandays" 
										ControlToValidate="txtMandays"
										ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
										Display="Dynamic"
										text = "Maximum length 15 digits and 5 decimal points"
										runat="server"/>
									<asp:RangeValidator id="RangeMandays"
										ControlToValidate="txtMandays"
										MinimumValue="0"
										MaximumValue="999999999999999"
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
									<asp:LinkButton id="Edit" CommandName="Edit" Text="Edit" CausesValidation=False  runat="server"/>
								</ItemTemplate>						
								<EditItemTemplate>						
									<asp:LinkButton id="Update" CommandName="Update" Text="Save & Next"	runat="server"/>
									<asp:LinkButton id="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation=False runat="server"/>							
								</EditItemTemplate>
							</asp:TemplateColumn>
						</Columns>
					</asp:DataGrid>
					</td>
				</tr>
				<tr>
					<td align="left" ColSpan=6>
						<asp:Label id=lblOvrMsg visible=false Text="Calculation Error please Check Data" Forecolor=red runat=server />
						<asp:Label id=lblNoRecord visible=false Text="Area Size or Crop Production Budgeted cannot be zero !" Forecolor=red runat=server />
					</td>
				</tr>
				<!--<tr>
					<td align="left" ColSpan=6>
						<asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print runat="server"/>
					</td>
				</tr>-->
			</table>
		</FORM>
	</body>
</html>
