<%@ Page Language="vb" trace="False" SmartNavigation="True" src="../../../include/BD_trx_CropProd_Details.aspx.vb" Inherits="BD_CropProd_Det" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuBDtrx" src="../../menu/menu_BDTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>CROP PRODUCTION</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
		<body>
		    <form runat="server" ID="Form1" class="main-modul-bg-app-list-pu">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:label id=lblCode visible=false text=" Code" runat=server />
			<asp:label id=lblBlock visible=false runat=server />
			<asp:label id=lblSubBlock visible=false runat=server />
			<asp:label id="sortcol" Visible="False" Runat="server" />
			<asp:Label id=lblBgtStatus Visible="False"  runat="server" />

        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">



			<table border="0" cellspacing="1" cellpadding="1" width="100%" runat=server class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuBDtrx id=menuBD runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="4" width=60%><asp:Label id="lblTitle" runat="server" /></td>
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
					<td colspan="4" width=60%>Planting Year : <asp:label id="lblYear" Runat="server" /></td>
					<td align="right" colspan="2" width=40%>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<!--<tr id="trTransNote">
					<td colspan=6><asp:label id=lblTransNote forecolor=red runat=server /></td>
				</tr>
				<tr id="trAreaNote">
					<td colspan=6><asp:label id=lblAreaNote forecolor=red runat=server /></td>
				</tr>-->
				<tr>
					<td colspan="6">
						<asp:Button id=btnDistByBlock onClick="btnDistByBlock_Click" text="Distribute by " runat="server"  />					
					</td>
				</tr>
				<tr>
					<td><asp:label id="lblOvrMsgTop" text='Number too big' Visible=False Forecolor=Red Runat="server" /></td>
				</tr>
				<tr>
					<td width=15%>&nbsp;</td>
					<td width=15%>&nbsp;</td>
					<td colspan="3" align="Center" width=55% > ------------------------------------- YIELD PER AREA ------------------------------------- </td>
					<td width=15%>&nbsp;</td>
				</tr>
                </table>

                <table style="width: 100%" class="font9Tahoma">
				<tr>
					<TD colspan = 6 >	
					
					<asp:DataGrid id="dgCropProd"
						AutoGenerateColumns="false" width="100%" runat="server"
						OnItemDataBound="DataGrid_ItemDataBound" 
						GridLines = both
						Cellpadding = "2"
						OnEditCommand="DEDR_Edit"
						OnUpdateCommand="DEDR_Update"
						OnCancelCommand="DEDR_Cancel"
						OnSortCommand="Sort_Grid" 
						AllowSorting="True"
                        class="font9Tahoma">	
							 
							<HeaderStyle  BackColor="#CCCCCC" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<ItemStyle BackColor="#FEFEFE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<AlternatingItemStyle BackColor="#EEEEEE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>					
					<Columns>
					<asp:TemplateColumn SortExpression="OriBlkCode">
						<ItemStyle Width="10%" />
						<ItemTemplate>
							<%# Container.DataItem("OriBlkCode") %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:label id="lblOriBlkCode" Text =<%# Trim(Container.DataItem("OriBlkCode")) %> runat="server"/>
						</EditItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn SortExpression="BD.BlkCode">
						<ItemStyle Width="10%" />
						<ItemTemplate>
							<%# Container.DataItem("BlkCode") %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:label id="lblBlkCode" Text =<%# Trim(Container.DataItem("BlkCode")) %> runat="server"/>
						</EditItemTemplate>
					</asp:TemplateColumn>
	
					<asp:TemplateColumn HeaderText="Planting Year" SortExpression="YearPlanted">
						<ItemStyle Width="5%" />
						<ItemTemplate>
							<%# Container.DataItem("YearPlanted") %>
						</ItemTemplate>
						<EditItemTemplate>
							<%# Container.DataItem("YearPlanted") %>
						</EditItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Harvest Date" SortExpression="HarvestDate">
						<ItemStyle Width="5%" />
						<ItemTemplate>
							<%# Container.DataItem("HarvestDate") %>
						</ItemTemplate>
						<EditItemTemplate>
							<%# Container.DataItem("HarvestDate") %>
						</EditItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="SPH" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right SortExpression="SPH">
						<ItemStyle Width="5%" />
						<ItemTemplate>
							<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("SPH"), 0, True, False, False)) %>
						</ItemTemplate>
						<EditItemTemplate>
							<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("SPH"), 0, True, False, False)) %>
						</EditItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Planted Area" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right SortExpression="PlantedArea">
						<ItemStyle Width="7%" />
						<ItemTemplate>
							<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("PlantedArea"), 0, True, False, False)) %>
						</ItemTemplate>
						<EditItemTemplate>
							<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("PlantedArea"), 0, True, False, False)) %>
						</EditItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Prev Year 2" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right SortExpression="YieldHist1">
						<ItemStyle Width="7%" />
						<ItemTemplate>
							<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("YieldHist1"), 0, True, False, False)) %>
						</ItemTemplate>
						<EditItemTemplate>
							<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("YieldHist1"), 0, True, False, False)) %>
						</EditItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Prev Year 1" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right SortExpression="YieldHist2">
						<ItemStyle Width="7%" />
						<ItemTemplate>
							<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("YieldHist2"), 0, True, False, False)) %>
						</ItemTemplate>
						<EditItemTemplate>
							<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("YieldHist2"), 0, True, False, False)) %>
						</EditItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="ToDate <BR> (A)" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right SortExpression="YieldToMonth">
						<ItemStyle Width="7%" />
						<ItemTemplate>	
							<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("YieldToMonth"), 0, True, False, False)) %>
						</ItemTemplate>
						<EditItemTemplate>
							<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("YieldToMonth"), 0, True, False, False)) %>
						</EditItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Forecasted (B)" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right SortExpression="ForecastYield">
						<ItemStyle Width="7%" />
						<ItemTemplate>
							<asp:label id="lblForecastYield" text=<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("ForecastYield"), 0, True, False, False)) %> Runat="server"></asp:label>
						</ItemTemplate>
						<EditItemTemplate>							
							<asp:TextBox id="txtCurr" width=100% MaxLength="19"
								Text='<%# FormatNumber(Container.DataItem("ForecastYield"), 0, True, False, False) %>' runat="server"/>
							<asp:RequiredFieldValidator id=validateCurr display=dynamic runat=server 
									text = "Please enter budgeted units"
									ControlToValidate=txtCurr />															
							<asp:RegularExpressionValidator id="RegExpValCurr" 
								ControlToValidate="txtCurr"
								ValidationExpression="\d{1,19}"
								Display="Dynamic"
								text = "Maximum length 19 digits and 0 decimal points"
								runat="server"/>
							<asp:RangeValidator id="RangeCurr"
								ControlToValidate="txtCurr"
								MinimumValue="0"
								MaximumValue="9999999999999999999"
								Type="double"
								EnableClientScript="True"
								Text="The value is out of range !"
								runat="server" display="dynamic"/>
						</EditItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Current Period (A + B)" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right SortExpression="YieldHist3">
						<ItemStyle Width="8%" />
						<ItemTemplate>
							<asp:label id="lblCurrYield" text=<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("YieldHist3"), 0, True, False, False)) %> Runat="server"></asp:label>
						</ItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Budgeted Next Period" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right SortExpression="YieldPerArea">
						<ItemStyle Width="7%" />
						<ItemTemplate>
							<asp:label id="lblBGTYield" text=<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("YieldPerArea"), 0, True, False, False)) %> Runat="server"></asp:label>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox id="txtNextYield" width=100% MaxLength="19"
								Text='<%# FormatNumber(Container.DataItem("YieldperArea"),0, True, False, False) %>'
								runat="server"/>
							<asp:RequiredFieldValidator id=validateNext display=dynamic runat=server 
									text = "Please enter budgeted units"
									ControlToValidate=txtNextYield />															
							<asp:RegularExpressionValidator id="RegExpValNext" 
								ControlToValidate="txtNextYield"
								ValidationExpression="\d{1,19}"
								Display="Dynamic"
								text = "Maximum length 19 digits and 0 decimal points"
								runat="server"/>
							<asp:RangeValidator id="RangeNext"
								ControlToValidate="txtNextYield"
								MinimumValue="0"
								MaximumValue="9999999999999999999"
								Type="double"
								EnableClientScript="True"
								Text="The value is out of range !"
								runat="server" display="dynamic"/>
					</EditItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Total Yield" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right SortExpression="BudgetYield">
						<ItemStyle Width="10%" />
						<ItemTemplate>
							<asp:label id="lblFFB" Text='<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("BudgetYield"), 0, True, False, False)) %>' Runat="server"></asp:label>
						</ItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn>
						<ItemStyle Width="5%" horizontalalign=right />					
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
					</td>
				</tr>
				<tr>
					<td align="left" ColSpan=6>
						<table width="100%" cellspacing="0" cellpadding="1" border="0" align="center">
							<tr class="mb-t">
								<td width="5%" height="26" valign=bottom>
									<B>TOTAL</B>
								</td>
								<td width="5%" height="26" valign=bottom>
									&nbsp;
								</td>
								<td width="5%" height="26" valign=bottom>
									&nbsp;
								</td>
								<td width="5%" height="26" valign=bottom>
									&nbsp;
								</td>
								<td width="5%" align=right height="26" valign=bottom>
									<B><asp:Label id=lblAvrgstand width=100%  runat="server"/></B>
								</td>
								<td width="7%" align=right height="26" valign=bottom>
									<B><asp:Label id=lblPlanted width=100% runat="server"/></B>
								</td>
								<td width="7%" align=right height="26" valign=bottom>
									<B><asp:Label id=lblHist1 width=100% runat="server"/></B>
								</td>
								<td width="7%" align=right height="26" valign=bottom>
									<B><asp:Label id=lblHist2 width=100% runat="server"/></B>
								</td>
								<td width="7%" align=right height="26" valign=bottom>
									<B><asp:Label id=lblCurr width=100% runat="server"/></B>
								</td>
								<td width="7%" align=right height="26" valign=bottom>
									<B><asp:Label id=lblForecast width=100% runat="server"/></B>
								</td>
								<td width="8%" align=right height="26" valign=bottom>
									<B><asp:Label id=lblYield width=100% runat="server"/></B>
								</td>
								<td width="7%" align=right height="26" valign=bottom>
									<B><asp:Label id=lblBGT width=100% runat="server"/></B>
								</td>
								<td width="10%" align=right height="26" valign=bottom>
									<B><asp:Label id=lblFFB width=100% runat="server"/></B>
								</td>
								<td width="5%" align=right height="26" valign=bottom>&nbsp;</td>
							</tr>
						</table>
						<asp:label id="lblOvrMsg" text='Number too big' Visible=False Forecolor=Red Runat="server"></asp:label>
					</td>
				</tr>
			</table>


        <br />
        </div>
        </td>
        </tr>
        </table>


		</FORM>
	</body>
</html>
