<%@ Page Language="vb" trace="false" src="../../../include/BD_trx_ManuringFertUsg_Details.aspx.vb" Inherits="BD_ManuringFertUsg_Det" %>
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
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:label id="sortcol" Visible="False" Runat="server" />
			<asp:label id="lblCode" text=" Code" Visible="False" Runat="server" />
			<asp:Label id=lblBgtStatus Visible="False"  runat="server" />

			<table border="0" cellspacing="1" cellpadding="1" width="100%">
				<tr>
					<td colspan="6"><UserControl:MenuBDtrx id=menuDB runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="4" width=60%>MANURING SCHEDULE SUMMARY</td>
					<td align="right" colspan="2" width=40%>&nbsp;</td>
				</tr>
			
				<tr>
					<td width=20% height=20><asp:label id="lblLocTag" runat="server"/> : </td>
					<td width=30% colspan="5"><asp:label id="lblLocCode" runat="server"/></td>
				</tr>
				<tr>
					<td width=20% height=20><asp:label id="lblBgtTag" text="Budgeting Period " runat="server"/> : </td>
					<td width=30% colspan="5"><asp:label id="lblBgtPeriod" runat="server"/></td>
				</tr>
				<tr>
					<td width=20% height=20><asp:label id="lblyrTag" text="Planting Year" runat="server"/> : </td>
					<td width=30% colspan="5"><asp:label id="lblYearPlanted" runat="server"/></td>
				</tr>
				<tr>
					<td width=20% height=20><asp:label id="lblblItemTag" text="Fertilizer Code " runat="server"/> : </td>
					<td width=30% colspan="5"><asp:label id="lblFertCode" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<td colspan="4" width=60%><asp:label id="lblPrice" text="Price " runat="server"/> : <asp:textbox id="txtPrice" maxlength="19" runat="server"/>
						<asp:RequiredFieldValidator id=validatePrice display=dynamic runat=server 
								text = "Please enter price"
								ControlToValidate=txtPrice />															
						<asp:RegularExpressionValidator id="RegExpValPrice" 
							ControlToValidate="txtPrice"
							ValidationExpression="\d{1,19}"
							Display="Dynamic"
							text = "Maximum length 19 digits and 0 decimal points"
							runat="server"/>
						<asp:RangeValidator id="RangePrice"
							ControlToValidate="txtPrice"
							MinimumValue="0"
							MaximumValue="9999999999999999999"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>					
					</td>
					<td align="right" colspan="2" width=40%>&nbsp;</td>
				</tr>
				<tr>
					<TD colspan = 6 >					
					<asp:DataGrid id="dgFertUsgLine"
						AutoGenerateColumns="false" width="100%" runat="server"
						OnItemDataBound="DataGrid_ItemDataBound" 
						GridLines = none
						Cellpadding = "2" >
						<HeaderStyle font-bold=true CssClass="mr-h" />							
						<ItemStyle CssClass="mr-l" />
						<AlternatingItemStyle CssClass="mr-r" />						
						<Columns>						
							<asp:TemplateColumn HeaderText="Bugdeting Month" >
								<ItemStyle Width="33%" />
								<ItemTemplate>									
									<asp:label id="lblAccMonth" text=<%# Container.DataItem("AccMonth") %> runat="server"/> /
									<asp:label id="lblAccYear" text=<%# Container.DataItem("AccYear") %> runat="server"/>
									<asp:label id="lblPlantedArea" text=<%# Container.DataItem("PlantedArea") %> visible=false runat="server"/>
									<asp:label id="lblManuringFertUsgLnID" text=<%# Container.DataItem("ManuringFertUsgLnID") %> visible=false runat="server"/>
									<asp:label id="lblManuringFertUsgID" text=<%# Container.DataItem("ManuringFertUsgID") %> visible=false runat="server"/>
								</ItemTemplate>
							</asp:TemplateColumn>
										
							<asp:TemplateColumn HeaderText="Cost" >
								<ItemStyle Width="33%" />
								<ItemTemplate>	
									<asp:label id="lblOriCost" Text='<%# FormatNumber(Container.DataItem("Cost"),0, True, False, False) %>' runat="server"/>						
								</ItemTemplate>
							</asp:TemplateColumn>
					
							<asp:TemplateColumn HeaderText="Quantity" >
								<ItemStyle Width="33%" />
								<ItemTemplate>		
									<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Qty"), 5, True, False, False),5) %>
									<asp:label id="lblQty" text=<%# Container.DataItem("Qty") %> visible=false runat="server"/>
								</ItemTemplate>
							</asp:TemplateColumn>
														
						</Columns>
					</asp:DataGrid>
					</td>
				</tr>
				<tr>
					<td align="left" ColSpan=6>
						<table width="100%" cellspacing="0" cellpadding="3" border="0" align="center">
							<tr class="mb-t">
								<td width="33%" height="26" valign=bottom><B>TOTAL</B></td>
								<td width="33%" height="26" valign=bottom><B><asp:label id="lblTotalCost" runat="server"/></B></td>
								<td width="33%" height="26" valign=bottom><B><asp:label id="lblTotalQty" runat="server"/></B></td>
							 </tr>
						</table>
					</td>
				<tr>
					<td align="left" ColSpan=6>
						<table width="100%" cellspacing="0" cellpadding="3" border="0" align="center">
							<tr class="mb-t">	
								<td width="20%" height="26" valign=bottom><B>TOTAL COST PER AREA </B></td>																										
								<td width="20%" height="26" valign=bottom><B><asp:label id="lblCostPerArea" runat="server"/></B></td>
								<td width="20%" height="26" valign=bottom>&nbsp;</td>
							 </tr>
						</table>
					</td>
				</tr>
				<tr>
					<td align="left" ColSpan=6>
						<table width="100%" cellspacing="0" cellpadding="3" border="0" align="center">
							<tr class="mb-t">	
								<td width="20%" height="26" valign=bottom><B>TOTAL COST PER WEIGHT </B></td>																																				
								<td width="20%" height="26" valign=bottom><B><asp:label id="lblCostPerMT" runat="server"/></B></td>
								<td width="20%" height="26" valign=bottom>&nbsp;</td>
							 </tr>
						</table>
					</td>
				</tr>
				<tr>
					<td ColSpan=6>&nbsp;</td>
				</tr>
				<tr>
					<td align="left" ColSpan=6>
						<asp:ImageButton id="Calculate"	AlternateText="  Calculate  " onclick=Button_Click  CommandArgument=calculate ImageURL="../../images/butt_calculate.gif" runat="server" />
						<asp:ImageButton id="Save"	AlternateText="  Save  " onclick=Button_Click  CommandArgument=save ImageURL="../../images/butt_save.gif" runat="server" />
						<asp:ImageButton id=BackBtn AlternateText="  Back  "  onclick=BackBtn_Click imageurl="../../images/butt_back.gif" CausesValidation=False runat=server />
					</td>
				</tr>				
				<tr>
					<td align="left" ColSpan=6>
						<asp:Label id=lblPeriodErr Text="No active Budgeting Period" Forecolor=Red Visible=False runat=server />
					</td>
				</tr>
			</table>
		</FORM>
	</body>
</html>
