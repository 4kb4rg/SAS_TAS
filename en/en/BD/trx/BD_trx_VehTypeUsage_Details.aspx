<%@ Page Language="vb" trace="False" SmartNavigation="True" src="../../../include/BD_trx_VehTypeUsage_Details.aspx.vb" Inherits="BD_VehTypeUsage_Details" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuBDtrx" src="../../menu/menu_BDTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Vehicle Type Usage</title>
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
		<body>
		    <form runat="server" ID="Form1">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:label id="sortcol" Visible="False" Runat="server" />
			<asp:label id="lbloper" Visible="False" Runat="server" />
			<asp:label id=lblBudgeting visible=false text="Budgeting " runat=server />
			<asp:Label id=lblBgtStatus Visible="False"  runat="server" />
			<asp:label id="lblCode" text=" Code" Visible="False" Runat="server" />

			<table border="0" cellspacing="1" cellpadding="1" width="100%">
				<tr>
					<td colspan="6"><UserControl:MenuBDTrx id=menuBD runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="4" width=60%>VEHICLE TYPE USAGE</td>
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
					<td colspan=6 width=100% class="mb-c">
						<table width="100%" cellspacing="0" cellpadding="3" border="0" align="center">
							<tr class="mb-t">
								<td width="20%" height="26" valign=bottom><asp:label id="lblVehTypeCodeTag" runat="server"/> :<BR>
									<asp:TextBox id=srchCode width=100% maxlength="8" runat="server"/>
								</td>
								<td width="52%" height="26" valign=bottom>Description :<BR>
									<asp:TextBox id=srchDesc width=100% maxlength="64" runat="server"/>
								</td>
								<td width="20%" height="26" valign=bottom>Last Updated By :<BR><asp:TextBox id=srchUpdateBy width=100% maxlength="128" runat="server"/></td>
								<td width="8%" height="26" valign=bottom align=right><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click runat="server"/></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<TD colspan = 6 >					
					<asp:DataGrid id="dgVehTypeUsg"
						AutoGenerateColumns="false" width="100%" runat="server"
						GridLines = none
						OnItemDataBound="DataGrid_ItemDataBound" 
						Cellpadding = "2"
						OnEditCommand="DEDR_Edit"
						OnUpdateCommand="DEDR_Update"
						OnCancelCommand="DEDR_Cancel"
						AllowPaging="True" 
						Allowcustompaging="False"
						Pagesize="15" 
						OnPageIndexChanged="OnPageChanged"
						Pagerstyle-Visible="False"
						OnSortCommand="Sort_Grid" 
						AllowSorting="True">
						<HeaderStyle CssClass="mr-h" />							
						<ItemStyle CssClass="mr-l" />
						<AlternatingItemStyle CssClass="mr-r" />						
						<Columns>
							<asp:TemplateColumn SortExpression="BD.VehTypeCode">
								<ItemStyle Width="20%" />
								<ItemTemplate>
									<%# Container.DataItem("VehTypeCode") %>
								</ItemTemplate>
								<EditItemTemplate>
									<asp:label id="lblVehTypeCode" Text='<%# trim(Container.DataItem("VehTypeCode")) %>' runat="server"/>
								</EditItemTemplate>
							</asp:TemplateColumn>
							
							<asp:TemplateColumn HeaderText="Description" Sortexpression="Description" >
								<ItemStyle Width="30%" />
								<ItemTemplate>
									<%# Container.DataItem("Description") %>
								</ItemTemplate>
								<EditItemTemplate>
									<asp:label id="lblDesc" Text='<%# trim(Container.DataItem("Description")) %>' runat="server"/>						
								</EditItemTemplate>
							</asp:TemplateColumn>

							<asp:TemplateColumn HeaderText ="Usage" HeaderStyle-HorizontalAlign=Left ItemStyle-HorizontalAlign=Left >
								<ItemStyle Width="10%" />
								<ItemTemplate>
									<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("VehUsage"), 5, True, False, False),5) %>
								</ItemTemplate>
								<EditItemTemplate>
									<asp:TextBox id="txtUsage" MaxLength="15" width=100%
											Text='<%# FormatNumber(Container.DataItem("VehUsage"),5, True, False, False) %>'
											runat="server"/>
									<asp:RegularExpressionValidator id="revUsage" 
										ControlToValidate="txtUsage"
										ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"
										Display="Dynamic"
										text = "Maximum length 9 digits and 5 decimal points"
										runat="server"/>
									<asp:RequiredFieldValidator 
										id="rfvUsage" 
										ErrorMessage="Please Specify Usage" 
										ControlToValidate="txtUsage" 
										runat="server" 
										display="dynamic"/>
									<asp:RangeValidator id="RangeUsage"
										ControlToValidate="txtUsage"
										MinimumValue="0.00001"
										MaximumValue="999999999999999"
										Type="double"
										EnableClientScript="True"
										Text="The value must be from 0.00001 !"
										runat="server" display="dynamic"/>
									<asp:label id=lblErrUsage text="Number generated is too big !" Visible=False forecolor=red Runat="server" />											
								</EditItemTemplate>
							</asp:TemplateColumn>
									
							<asp:TemplateColumn HeaderText="Cost" HeaderStyle-HorizontalAlign=Left ItemStyle-HorizontalAlign=Left >
								<ItemStyle Width="10%" />
								<ItemTemplate>
									<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("Cost"), 0, True, False, False)) %>
								</ItemTemplate>
								<EditItemTemplate>
									<asp:TextBox id="txtCost" MaxLength="19" width=100%
											Text='<%# FormatNumber(Container.DataItem("Cost"),0, True, False, False) %>'
											runat="server"/>
									<asp:RegularExpressionValidator id="revCost" 
										ControlToValidate="txtCost"
										ValidationExpression="\d{1,19}"
										Display="Dynamic"
										text = "Maximum length 19 digits and 0 decimal points"
										runat="server"/>
									<asp:RequiredFieldValidator 
										id="rfvCost" 
										ErrorMessage="Please Specify Cost" 
										ControlToValidate="txtCost" 
										runat="server" 
										display="dynamic"/>
									<asp:RangeValidator id="RangeCost"
										ControlToValidate="txtCost"
										MinimumValue="0.00001"
										MaximumValue="9999999999999999999"
										Type="double"
										EnableClientScript="True"
										Text="The value must be from 0.00001 !"
										runat="server" display="dynamic"/>
									<asp:label id=lblErrCost text="Number generated is too big !" Visible=False forecolor=red Runat="server" />											
								</EditItemTemplate>
							</asp:TemplateColumn>
																								
							<asp:TemplateColumn HeaderText="Last Update" Sortexpression="BD.UpdateDate">
								<ItemStyle Width="10%" />
								<ItemTemplate>
									<%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
								</ItemTemplate>
								<EditItemTemplate >
									<asp:textbox id="txtUpdateDate" Readonly=TRUE Text='<%# objGlobal.GetLongDate(Now()) %>' Visible=False runat="server"/>						
								</EditItemTemplate>
							</asp:TemplateColumn>
							
							<asp:TemplateColumn HeaderText="Updated By"  Sortexpression="UserName">
								<ItemStyle Width="10%" />
								<ItemTemplate>
									<%# Container.DataItem("UserName") %>
								</ItemTemplate>
								<EditItemTemplate >
									<asp:textbox id="txtUserName" Readonly=TRUE Text='<%# Session("SS_USERID") %>' Visible=False runat="server"/>
								</EditItemTemplate>
							</asp:TemplateColumn>
							
							<asp:TemplateColumn>					
								<ItemStyle Width="10%" />
								<ItemTemplate>
									<asp:LinkButton id="Edit" CommandName="Edit"   Text="Edit" runat="server"/>
								</ItemTemplate>
								
								<EditItemTemplate>
									<asp:LinkButton id="Update" CommandName="Update" Text="Save" runat="server"/>
									<asp:LinkButton id="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation=False runat="server"/>
								</EditItemTemplate>
							</asp:TemplateColumn>
						</Columns>
					</asp:DataGrid>
					</td>
				</tr>
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>		
				<tr>
					<td width=20% colspan=2 >&nbsp;</td>
					<td width=40% align="right" ><B><asp:label id="Total" text="Total" runat="server" /></B></td>
					<td width=10%>&nbsp;</td>						
					<td width=10% align="Left" colspan=3><B><asp:label id="lblTotalCost" runat="server" /></B> </td>
				</tr>				
				<tr>
					<td align=right colspan="6">
						<asp:ImageButton id="btnPrev" runat="server" imageurl="../../images/icn_prev.gif" alternatetext="Previous" commandargument="prev" onClick="btnPrevNext_Click" />
						<asp:DropDownList id="lstDropList" AutoPostBack="True" onSelectedIndexChanged="PagingIndexChanged" runat="server" />
						<asp:Imagebutton id="btnNext" runat="server"  imageurl="../../images/icn_next.gif" alternatetext="Next" commandargument="next" onClick="btnPrevNext_Click" />
					</td>
				</tr>
			</table>
		</FORM>
	</body>
</html>
