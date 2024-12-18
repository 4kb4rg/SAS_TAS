<%@ Page Language="vb" trace="False"  SmartNavigation="True" src="../../../include/BD_setup_UnMatureCrop.aspx.vb" Inherits="BD_UnMatureCrop_Format" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuBDsetup" src="../../menu/menu_BDSetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Immature Crop Activities</title>
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
		<body>
		    <form runat="server" ID="Form1">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="lblOper" Visible="False" Runat="server" />
			<asp:label id=lblBudgeting visible=false text="Budgeting " runat=server />
			<asp:label id="lblCode" text=" Code" Visible = false Runat="server"/>

			<table border="0" cellspacing="1" cellpadding="1" width="100%">
				<tr>
					<td colspan="6"><UserControl:MenuBDsetup id=menuBD runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="4" width=60%><asp:label id=lblTitle runat=server /></td>
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
					<td colspan="4" width=60%>&nbsp;</td>
					<td align="right" colspan="2" width=40%>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<TD colspan = 6 >					
					<asp:DataGrid id="dgUnMatureCropSetup"
						AutoGenerateColumns="false" width="100%" runat="server"
						GridLines = none
						Cellpadding = "2"
						OnEditCommand="DEDR_Edit"
						OnUpdateCommand="DEDR_Update"
						OnCancelCommand="DEDR_Cancel"
						OnDeleteCommand="DEDR_Delete">
						<HeaderStyle CssClass="mr-h" />							
						<ItemStyle CssClass="mr-l" />
						<AlternatingItemStyle CssClass="mr-r" />						
						<Columns>
					
							<asp:TemplateColumn HeaderText ="No." >
								<ItemStyle Width="4%" />
								<ItemTemplate>
									<asp:Label id="lblIdx" runat="server" />
								</ItemTemplate>
								<EditItemTemplate>
									<asp:Label id="lblIdx" runat="server" />
								</EditItemTemplate>
							</asp:TemplateColumn>
							
							<asp:TemplateColumn HeaderText ="Seq." ItemStyle-Width="4%">
								<ItemTemplate>
									<asp:Label id="lblSeq" Text='<%# trim(Container.DataItem("DispSeq")) %>' runat="server" />
								</ItemTemplate>
								<EditItemTemplate>
									<asp:TextBox id="txtDispSeq" MaxLength="4" width=90%
										Text='<%# trim(Container.DataItem("DispSeq")) %>'
										runat="server"/>
									<asp:Label id="lblSeq" Text="Sequence Exist !" forecolor="red" Visible=false runat="server" />
								</EditItemTemplate>
							</asp:TemplateColumn>

							<asp:TemplateColumn ItemStyle-Width="15%" >
								<ItemTemplate>
									<asp:Label id="lblAccCode" Text=<%# Container.DataItem("AccCode") %> runat="server" /><BR>
								</ItemTemplate>
								<EditItemTemplate>
									<asp:Label id="lblAccCode" Text=<%# Container.DataItem("AccCode") %> visible=false runat="server" /><BR>
									<asp:DropDownList id="ddlAccCode" width=90% runat=server />
									<asp:RequiredFieldValidator id=validateAcc display=dynamic runat=server 
										text="Please select Account Code" ControlToValidate=ddlAccCode />
								</EditItemTemplate>
							</asp:TemplateColumn>

							<asp:TemplateColumn HeaderText ="Item" ItemStyle-Width="29%">
								<ItemTemplate>
									<%# Container.DataItem("Description") %>
									<%# trim(Container.DataItem("ItemDescription")) %>
								</ItemTemplate>
								<EditItemTemplate>
									<asp:Label id="lblTxID" Text=<%# trim(Container.DataItem("UnMatureCropSetID")) %> visible=false runat="server" />
									<asp:TextBox id="txtItemDesc" MaxLength="128" 
										Text='<%# trim(Container.DataItem("ItemDescription")) %>'
										runat="server"/>
								</EditItemTemplate>
							</asp:TemplateColumn>
							
							<asp:TemplateColumn HeaderText ="Display Type" ItemStyle-Width="10%" >
								<ItemTemplate>
									<%# objBD.mtdGetFormatItem(Container.DataItem("ItemDisplayType")) %>
									<asp:Label id="lblDisp" Text=<%# Container.DataItem("ItemDisplayType") %> Visible=false  runat="server" />
								</ItemTemplate>
								<EditItemTemplate>
									<asp:Label id="lblDisp" Text=<%# Container.DataItem("ItemDisplayType") %> Visible=false  runat="server" />
									<asp:DropDownList id="ddlDispType" AutoPostback=True OnSelectedIndexChanged=ddlCheckType runat=server />
									<asp:RequiredFieldValidator id=validateDisp display=dynamic runat=server 
										text="Please select display type" ControlToValidate=ddlDispType />
								</EditItemTemplate>
							</asp:TemplateColumn>
			
							<asp:TemplateColumn HeaderText ="Reference" ItemStyle-Width="5%" >
								<ItemTemplate>
									<%# Container.DataItem("FormulaRef").trim %>
								</ItemTemplate>
								<EditItemTemplate>
									<asp:TextBox id="txtFormulaRef" text=<%# Trim(Container.DataItem("FormulaRef")) %> MaxLength="8" width=95% runat="server" />
									<asp:Label id="lblRef" Text="Reference duplicated !" forecolor="red" Visible=false  runat="server" />
								</EditItemTemplate>
							</asp:TemplateColumn>
							
							<asp:TemplateColumn HeaderText ="Formula" ItemStyle-Width="18%" >
								<ItemTemplate>
									<asp:Label id="lblForm" text=<%# Trim(Container.DataItem("ItemCalcFormula")) %>  visible=False runat="server" />
									<asp:Label id="lblForm1"  runat="server" />
									<asp:Label id="lblForm2"  runat="server" />
									<asp:Label id="lblForm3"  runat="server" />
								</ItemTemplate>
								<EditItemTemplate>
									<asp:TextBox id="txtFormula" text=<%# Trim(Container.DataItem("ItemCalcFormula")) %> MaxLength="1024" width=95% visible=false runat="server" /> 
									<asp:TextBox id="txtFormula1"  MaxLength="256" Tooltip="Others" width=95% visible=false runat="server" /> 
									<asp:TextBox id="txtFormula2"  MaxLength="256" Tooltip="Material" width=95% visible=false runat="server" />
									<asp:TextBox id="txtFormula3"  MaxLength="256" Tooltip="Labour" width=95% visible=false runat="server" />
								</EditItemTemplate>
							</asp:TemplateColumn>
							
							<asp:TemplateColumn HeaderText ="Display Column" ItemStyle-Width="10%">
								<ItemTemplate>
									<asp:Label id="lblColName" text=<%# objBD.mtdGetItemColumn(Container.DataItem("ItemDisplayCol")) %> runat="server" />
									<asp:Label id="lblCol" Text=<%# Container.DataItem("ItemDisplayCol") %> Visible=false  runat="server" />
								</ItemTemplate>
								<EditItemTemplate>
									<asp:Label id="lblCol" Text=<%# Container.DataItem("ItemDisplayCol") %> Visible=false  runat="server" />
									<asp:DropDownList id="ddlDispCol" runat=server ></asp:DropDownList>
									<asp:RequiredFieldValidator id=validateDispCol display=dynamic runat=server 
										text="Please select display type"	ControlToValidate=ddlDispType />
								</EditItemTemplate>
							</asp:TemplateColumn>
								
							<asp:TemplateColumn ItemStyle-Width="5%" >
								<ItemTemplate>
									<asp:LinkButton id="Edit" CommandName="Edit"   Text="Edit"	runat="server"/>
								</ItemTemplate>								
								<EditItemTemplate>
									<asp:LinkButton id="Update" CommandName="Update" Text="Save" runat="server"/>
									<asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" runat="server"/>
									<asp:LinkButton id="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation=False runat="server"/>
								</EditItemTemplate>
							</asp:TemplateColumn>
						</Columns>
					</asp:DataGrid>
					</td>
				</tr>
				<tr>
					<td align="left" ColSpan=6>
						<asp:ImageButton id=ibNew OnClick="DEDR_Add" imageurl="../../images/butt_new.gif" AlternateText="Immature Crop Activity" runat="server"/>
					</td>
				</tr>
			</table>
		</FORM>
	</body>
</html>
