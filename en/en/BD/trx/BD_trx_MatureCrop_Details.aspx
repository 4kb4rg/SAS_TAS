<%@ Page Language="vb" SmartNavigation="True" trace="false" src="../../../include/BD_trx_MatureCrop_Details.aspx.vb" Inherits="BD_MatureCrop_Det" %>
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
			<asp:Label id=lblBgtStatus Visible="False"  runat="server" />
			<Input type=hidden id=hidBlkCode value="" runat=server />
			
			<table border="0" cellspacing="1" cellpadding="1" width="100%" runat=server>
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
				<tr id="RowSubBlk">
					<td colspan="4" width=60%><asp:label id="lblSubBlkTag" runat="server"/> : <asp:label id="lblSubBlkCode" runat="server" /></td>
					<td align="right" colspan="2" width=40%>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="4" width=60%><asp:label id="lblPlantYrTag" text="Planting Year" runat="server"/> : <asp:label id="lblYearPlanted" runat="server"/></td>
					<td align="right" colspan="2" width=40%>&nbsp;</td>
				</tr>								
				<tr>
					<td colspan="4" width=60%>Total Area Size : <asp:label id="lblTotalAreaFig" runat="server"/></td>
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
					<td class="mt-h" colspan="4" width=60%>&nbsp;</td>
					<td align="right" colspan="2" width=40%><asp:Button id="lbtn_Recalc" Text="Calculate Formula" runat="server"/></td>
				</tr>
				<tr>
					<td align="left" ColSpan=6>
						<asp:Label id=lblOvrMsgTop visible=false Text="Calculation Error please Check Data" Forecolor=red runat=server />
						<asp:Label id=lblNoRecordTop visible=false Text="Total seed cannot be zero !" Forecolor=red runat=server />
					</td>
				</tr>								
				<tr>
					<TD colspan = 6 >					
					<asp:DataGrid id="dgMatureCropDet"
						AutoGenerateColumns="false" width="100%" runat="server"
						OnItemDataBound="DataGrid_ItemDataBound" 
						Cellpadding = "2"
						OnEditCommand="DEDR_Edit"
						OnUpdateCommand="DEDR_Update"
						OnCancelCommand="DEDR_Cancel">
						<HeaderStyle font-bold=true CssClass="mr-h" />							
						<Columns>	
							<asp:TemplateColumn HeaderText="No.">
								<ItemStyle Width="3%" />
								<HeaderStyle HorizontalAlign="Left"  CssClass="mr-h" />							
								<ItemTemplate>
									<asp:label  id="lblIdx" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							
							<asp:TemplateColumn ItemStyle-Width="10%"  >
								<ItemTemplate>
									<asp:label id="lblAcc" Width="8%" text=<%# Trim(Container.DataItem("Acc")) %> Runat="server"></asp:label> 
								</ItemTemplate>
								<EditItemTemplate>
									<asp:label id="lblAcc" Width="8%" text=<%# Trim(Container.DataItem("Acc")) %> Runat="server"></asp:label> 										
								</EditItemTemplate>
							</asp:TemplateColumn>
							
							<asp:TemplateColumn HeaderText="Items" ItemStyle-Width="20%" >
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
										
							<asp:TemplateColumn HeaderText="Freq." HeaderStyle-HorizontalAlign="Right" ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Right" >
								<ItemTemplate>
									<asp:Label id=lblFreq Text=<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("Frequency"), 2, True, False, False)) %> runat=server />
								</ItemTemplate>
								<EditItemTemplate>
									<asp:TextBox id="txtFreq" width=100% MaxLength="25"
										Text='<%# FormatNumber(Container.DataItem("Frequency"),2, True, False, False) %>'
										runat="server"/>
									<asp:RequiredFieldValidator id=validateFreq display=dynamic runat=server 
										text="Please enter Frequency"
										ControlToValidate=txtFreq />															
									<asp:RegularExpressionValidator id="RegExpValFreq" 
										ControlToValidate="txtFreq"
										ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
										Display="Dynamic"
										text = "Maximum length 15 digits and 5 decimal points"
										runat="server"/>
									<asp:RangeValidator id="RangeFreq"
										ControlToValidate="txtFreq"
										MinimumValue="0"
										MaximumValue="999999999999999"
										Type="double"
										EnableClientScript="True"
										Text="The value is out of range !"
										runat="server" display="dynamic"/>
								</EditItemTemplate>
							</asp:TemplateColumn>
							
							<asp:TemplateColumn HeaderText="Unit per Freq." HeaderStyle-HorizontalAlign="Right" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Right" >
								<ItemTemplate>
									<asp:Label id=lblUnit Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Unit"), 5, True, False, False),5) %> runat=server />
								</ItemTemplate>
								<EditItemTemplate>
									<asp:TextBox id="txtUnit" width=100% MaxLength="15"
										Text='<%# FormatNumber(Container.DataItem("Unit"),5, True, False, False) %>'
										runat="server"/>
									<asp:RequiredFieldValidator id=validateUnit display=dynamic runat=server 
										text = "Please enter Unit"
										ControlToValidate=txtUnit />															
									<asp:RegularExpressionValidator id="RegExpValUnit" 
										ControlToValidate="txtUnit"
										ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"
										Display="Dynamic"
										text = "Maximum length 9 digits and 5 decimal points"
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
										
							<asp:TemplateColumn HeaderText="Unit Cost" HeaderStyle-HorizontalAlign="Right" ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Right" >
								<ItemTemplate>
									<asp:Label id=lblUnitCost Text=<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("UnitCost"), 0, True, False, False)) %> runat=server />
								</ItemTemplate>
								<EditItemTemplate>
									<asp:TextBox id="txtUnitCost" width=100% MaxLength="19"
										Text='<%# FormatNumber(Container.DataItem("UnitCost"),0, True, False, False) %>'
										runat="server"/>
									<asp:RequiredFieldValidator id=validateUnitCost display=dynamic runat=server 
										text = "Please enter Unit Cost"
										ControlToValidate=txtUnitCost />															
									<asp:RegularExpressionValidator id="RegExpValUnitCost" 
										ControlToValidate="txtUnitCost"
										ValidationExpression="\d{1,19}"
										Display="Dynamic"
										text = "Maximum length 19 digits and 0 decimal points"
										runat="server"/>
									<asp:RangeValidator id="RangeUnitCost"
										ControlToValidate="txtUnitCost"
										MinimumValue="-9999999999999999999"
										MaximumValue="9999999999999999999"
										Type="double"
										EnableClientScript="True"
										Text="The value is out of range !"
										runat="server" display="dynamic"/>
								</EditItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Mandays" HeaderStyle-HorizontalAlign="Right" ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Right" >
								<ItemTemplate>
									<asp:Label id=lblMandays Text=<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("Mandays"), 0, True, False, False)) %> runat=server />
								</ItemTemplate>
								<EditItemTemplate>
									<asp:TextBox id="txtMandays" width=100% MaxLength="25"
										Text='<%# FormatNumber(Container.DataItem("Mandays"),5, True, False, False) %>'
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
							<asp:TemplateColumn HeaderText="Others" HeaderStyle-HorizontalAlign="Right" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Right" >
								<ItemTemplate>
									<asp:Label id=lblOtherCost Text=<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("OtherCost"), 0, True, False, False)) %> runat=server />
								</ItemTemplate>
								<EditItemTemplate>
									<asp:Label id=lblOtherCost Text=<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("OtherCost"), 0, True, False, False)) %> runat=server />
									<asp:TextBox id="txtOtherCost" width=100% MaxLength="25"
										Text='<%# FormatNumber(Container.DataItem("OtherCost"),0, True, False, False) %>'
										visible=false runat="server"/>
								</EditItemTemplate>
							</asp:TemplateColumn>	
							<asp:TemplateColumn HeaderText="Materials" HeaderStyle-HorizontalAlign="Right" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Right" >			
								<ItemTemplate>
									<asp:Label id=lblMaterialCost Text=<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("MaterialCost"), 0, True, False, False)) %> runat=server />
								</ItemTemplate>
								<EditItemTemplate>
									<asp:Label id=lblMaterialCost Text=<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("MaterialCost"), 0, True, False, False)) %> runat=server />
									<asp:TextBox id="txtMaterialCost" width=100% MaxLength="25"
										Text='<%# FormatNumber(Container.DataItem("MaterialCost"),0, True, False, False) %>'
										visible=false runat="server"/>
								</EditItemTemplate>
							</asp:TemplateColumn>	
							<asp:TemplateColumn HeaderText="Labour" HeaderStyle-HorizontalAlign="Right" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Right" >
								<ItemTemplate>
									<asp:Label id=lblLabourCost Text=<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("LabourCost"), 0, True, False, False)) %> runat=server />
								</ItemTemplate>
								<EditItemTemplate>
									<asp:Label id=lblLabourCost Text=<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("LabourCost"), 0, True, False, False)) %> runat=server />
									<asp:TextBox id="txtLabourCost" width=100% MaxLength="25"
										Text='<%# FormatNumber(Container.DataItem("LabourCost"),0, True, False, False) %>'
										visible=false runat="server"/>
								</EditItemTemplate>
							</asp:TemplateColumn>	
								<asp:TemplateColumn HeaderText="Add. Vote" HeaderStyle-HorizontalAlign="Right" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Right" >	
								<ItemTemplate>
									<asp:Label id=lblAddVote Text=<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("AddVote"), 0, True, False, False)) %>  runat=server />
								</ItemTemplate>
								<EditItemTemplate>
									<asp:Label id=lblAddVote Text=<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("AddVote"), 0, True, False, False)) %> runat=server />
									<asp:TextBox id="txtAddVote" width=100% MaxLength="25"
										Text='<%# FormatNumber(Container.DataItem("AddVote"),0, True, False, False) %>'
										visible=false runat="server"/>
								</EditItemTemplate>
							</asp:TemplateColumn>	
							<asp:TemplateColumn HeaderText="Cost per Area" HeaderStyle-HorizontalAlign="Right" ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Right">		
								<ItemTemplate>
									<asp:Label id=lblCostperArea Text=<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("CostperArea"), 0, True, False, False)) %> runat=server />
								</ItemTemplate>
								<EditItemTemplate>
									<asp:Label id=lblCostperArea Text=<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("CostperArea"), 0, True, False, False)) %> runat=server />
									<asp:TextBox id="txtCostperArea" width=100% MaxLength="25"
										Text='<%# FormatNumber(Container.DataItem("CostperArea"),0, True, False, False) %>'
										visible=false runat="server"/>
								</EditItemTemplate>
							</asp:TemplateColumn>								
							<asp:TemplateColumn HeaderText="Cost per Weight" HeaderStyle-HorizontalAlign="Right" ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Right" >			
								<ItemTemplate>
									<asp:Label id=lblCostperWeight Text=<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("CostperWeight"), 0, True, False, False)) %> runat=server />
								</ItemTemplate>
								<EditItemTemplate>
									<asp:Label id=lblCostperWeight Text=<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("CostperWeight"), 0, True, False, False)) %> runat=server />
									<asp:TextBox id="txtCostperWeight" width=100% MaxLength="25"
										Text='<%# FormatNumber(Container.DataItem("CostperWeight"),0, True, False, False) %>'
										visible=false runat="server"/>
								</EditItemTemplate>
							</asp:TemplateColumn>								
																											
							<asp:TemplateColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" >
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
			</table>
		</FORM>
	</body>
</html>
