<%@ Page Language="vb" SmartNavigation="True" trace="false" src="../../../include/BD_setup_VehRunning.aspx.vb" Inherits="BD_VehRunning_Format" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuBDsetup" src="../../menu/menu_BDSetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Vehicle Usage</title>
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
		<body>
		    <form runat="server" ID="Form1">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:label id="sortcol" Visible="False" Runat="server" />
			<asp:label id="lbloper" Visible="False" Runat="server" />
			<asp:label id=lblCode visible=false text=" Code" runat=server />
			<asp:label id=lblSelect visible=false text="Select " runat=server />
			<asp:label id=lblBudgeting visible=false text="Budgeting " runat=server />
			<asp:label id=lblVehExpCode visible=false runat=server />
			<asp:Label id=lblPleaseSelect visible=false text="Please select " runat=server />

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
					<asp:DataGrid id="VehSetup"
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

					<asp:TemplateColumn HeaderText ="Seq." >
						<ItemStyle Width="4%" />
						<ItemTemplate>
							<asp:Label id="lblSeq" Text='<%# trim(Container.DataItem("DispSeq")) %>' runat="server" />
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox id="txtDispSeq" MaxLength="4" width=90% Text='<%# trim(Container.DataItem("DispSeq")) %>' runat="server"/>
							<asp:RequiredFieldValidator id=rfvDispSeq 
								display=dynamic runat=server 
								text="Please enter Sequence."
								ControlToValidate=txtDispSeq />
							<asp:RangeValidator id="RgDispSeq"
								ControlToValidate="txtDispSeq"
								MinimumValue="0"
								MaximumValue="9999"
								Type="Integer"
								EnableClientScript="True"
								Text="Please enter round number which is not less than 0."
								runat="server" 
								display="dynamic"/>	 
							<asp:Label id="lblSeq" Text="Sequence Exist !" forecolor="red" Visible=false  runat="server" />
						</EditItemTemplate>
					</asp:TemplateColumn>

					<asp:TemplateColumn >
						<ItemStyle Width="30%" />
						<ItemTemplate>
							<asp:Label id="lblVehExp" Text=<%# Container.DataItem("VehExpense") %> runat="server" /> 
							<asp:Label id="lblVehExpCode" Text=<%# Container.DataItem("VehExpenseCode") %> visible=false runat="server" /> 
						</ItemTemplate>
						<EditItemTemplate>
							<asp:DropDownList id="ddlVehExp" width=90% runat=server ></asp:DropDownList>
							<asp:RequiredFieldValidator id=validateCode display=dynamic runat=server 
								text="Please select a Code !" ControlToValidate=ddlVehExp />
						</EditItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText ="Item" >
						<ItemStyle Width="20%" />
						<ItemTemplate>							
							<%# Container.DataItem("ItemDescription") %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:Label id="lblTxID" Text=<%# trim(Container.DataItem("VehRunSetID")) %> visible=false runat="server" />
							<asp:TextBox id="txtItemDesc" MaxLength="128" width=90%
									Text='<%# trim(Container.DataItem("ItemDescription")) %>'
									runat="server"/>
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText ="Display Type" >
						<ItemStyle Width="7%" />
						<ItemTemplate>
							<%# objBD.mtdGetFormatItem(Container.DataItem("ItemDisplayType")) %>
							<asp:Label id="lblDisp" Text=<%# Container.DataItem("ItemDisplayType") %> Visible=false  runat="server" />
						</ItemTemplate>
						<EditItemTemplate>
							<asp:Label id="lblDisp" Text=<%# Container.DataItem("ItemDisplayType") %> Visible=false  runat="server" />
							<asp:DropDownList id="ddlDispType" AutoPostback=True OnSelectedIndexChanged=ddlCheckType runat=server ></asp:DropDownList>
							<asp:RequiredFieldValidator id=validateDisp display=dynamic runat=server 
								 text="Please select display type"	ControlToValidate=ddlDispType />
						</EditItemTemplate>
					</asp:TemplateColumn>

					<asp:TemplateColumn HeaderText ="Reference">
						<ItemStyle Width="8%" />
						<ItemTemplate>
							<%# Container.DataItem("FormulaRef").trim %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox id="txtFormulaRef" text=<%# Trim(Container.DataItem("FormulaRef")) %> MaxLength="8" width=95% runat="server" />
							<asp:Label id="lblRef" Text="reference duplicated !" forecolor="red" Visible=false  runat="server" />
						</EditItemTemplate>
					</asp:TemplateColumn>
	
					<asp:TemplateColumn HeaderText ="Formula" >
						<ItemStyle Width="13%" />
						<ItemTemplate>
							<asp:Label id="lblForm" text=<%# Trim(Container.DataItem("ItemCalcFormula")) %>  visible=False runat="server" />						
							<asp:Label id="lblForm1" runat="server" />
							<asp:Label id="lblForm2" runat="server" />
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox id="txtFormula" text=<%# Trim(Container.DataItem("ItemCalcFormula")) %> MaxLength="256" width=95% visible=false runat="server" />
							<asp:TextBox id="txtFormula1" MaxLength="256" Tooltip="Unit" width=95% visible=false runat="server" /> 
							<asp:TextBox id="txtFormula2" MaxLength="256" Tooltip="Cost" width=95% visible=false runat="server" />
						</EditItemTemplate>
					</asp:TemplateColumn>
	
					<asp:TemplateColumn HeaderText ="Display Column">
						<ItemStyle Width="9%" />
						<ItemTemplate>
							<%# objBD.mtdGetItemColumn(Container.DataItem("ItemDisplayCol")) %>
							<asp:Label id="lblCol" Text=<%# Container.DataItem("ItemDisplayCol") %> Visible=false  runat="server" />
						</ItemTemplate>
						<EditItemTemplate>
							<asp:Label id="lblCol" Text=<%# Container.DataItem("ItemDisplayCol") %> Visible=false  runat="server" />
							<asp:DropDownList id="ddlDispCol" runat=server ></asp:DropDownList>
							<asp:RequiredFieldValidator id=validateDispCol display=dynamic runat=server 
								 text="Please select display type"	ControlToValidate=ddlDispType />
						</EditItemTemplate>
					</asp:TemplateColumn>
						
					<asp:TemplateColumn>
						<ItemStyle Width="5%" HorizontalAlign=Right/>
						<ItemTemplate >
							<asp:LinkButton id="Edit" CommandName="Edit" Text="Edit" runat="server" />
						</ItemTemplate>						
						<EditItemTemplate>
							<asp:LinkButton id="Update" CommandName="Update" Text="Save" runat="server" />
							<asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" CausesValidation=False runat="server"/>
							<asp:LinkButton id="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation=False runat="server"/>
						</EditItemTemplate>
					</asp:TemplateColumn>

					</Columns>
					</asp:DataGrid>
					</td>
				</tr>
				<tr>
					<td align="left" ColSpan=6>
						<asp:ImageButton id=ibNew OnClick="DEDR_Add" imageurl="../../images/butt_new.gif" AlternateText="New Penggunaan Kenderaan" runat="server"/>
					</td>
				</tr>								
			</table>
		</FORM>
	</body>
</html>
