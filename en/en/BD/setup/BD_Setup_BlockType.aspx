<%@ Page Language="vb" trace="False" src="../../../include/BD_setup_BlockType.aspx.vb" Inherits="BD_Blocktype" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuBDsetup" src="../../menu/menu_BDSetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Block Type</title>
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
		<body>
		    <form runat="server" ID="Form1">
			<asp:label id="SQLStatement" Visible="False" Runat="server"></asp:label>
			<asp:label id="SortExpression" Visible="False" Runat="server"></asp:label>
			<asp:label id="sortcol" Visible="False" Runat="server"></asp:label>
			<asp:label id="lbloper" Visible="False" Runat="server"></asp:label>
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id=lblCode visible=false text=" Code" runat=server/>
			<asp:label id=lblBudgeting visible=false text="Budgeting " runat=server />
			<asp:label id="blnUpdate" Visible="False" Runat="server"></asp:label>
			<asp:label id="curStatus" Visible="False" Runat="server"></asp:label>
			<asp:Label id="ErrorMessage" runat="server" />

			<table border="0" cellspacing="1" cellpadding="1" width="100%">
				<tr>
					<td colspan="6"><UserControl:MenuBDsetup id=menuBD runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="4" width=60%>Budgeting Block Setup</td>
					<td align="right" colspan="2" width=40%>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<td colspan="4" width=60%><asp:label id="lblLocTag" text="Budgeting Location" runat="server"/> : <asp:label id="lblLocCode" runat="server"/></td>
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
								<td width="20%" height="26" valign=bottom><asp:label id=lblBlkCode runat=server /> :<BR>
									<asp:TextBox id=srchBlock width=100% maxlength="8" runat="server"/>
								</td>
								<td width="20%" height="26" valign=bottom>Planting Year :<BR>
									<asp:TextBox id=srchYear width=100% maxlength="64" runat="server"/>
								</td>
								<td width="15%" height="26" valign=bottom>Crop Type :<BR>
									<asp:DropDownList id="ddlCropType" runat=server >
									</asp:DropDownList>
								</td>
								<td width="10%" height="26" valign=bottom colspan =2>Last Update By :<BR><asp:TextBox id=srchUpdateBy width=100% maxlength="10" runat="server"/></td>
								<td width="25%" height="26" valign=bottom align=right><asp:Button Text="Search" OnClick=srchBtn_Click runat="server"/></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="mt-h" colspan="4" width=60%><asp:label id="lblTitle" runat="server" /></td>
					<td align="right" colspan="2" width=40%><asp:label id="lblTracker" runat="server"/></td>
				</tr>

				<tr>
					<TD colspan = 6 >					
					<asp:DataGrid id="BlockCrop"
						AutoGenerateColumns="false" width="100%" runat="server"
						GridLines = none
						Cellpadding = "2"
						OnEditCommand="DEDR_Edit"
						OnUpdateCommand="DEDR_Update"
						OnCancelCommand="DEDR_Cancel"
						OnDeleteCommand="DEDR_Delete"
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
					
					<asp:TemplateColumn HeaderText ="Block" Sortexpression="BlkCode" >
						<ItemStyle Width="20%" />
						<ItemTemplate>
							<%# Container.DataItem("BlkCode") %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:DropDownList id="ddlBlock" runat=server >
							</asp:DropDownList>
							<asp:Label id="lblBlkCode" Text='<%# Container.DataItem("BlkCode") %>' Visible=False runat="server" />
							<asp:RequiredFieldValidator id=validateBlock display=dynamic runat=server 
								 text="Please Select a code"	ControlToValidate=ddlBlock />
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText ="Planting Year" Sortexpression="PlantingYear">
						<ItemStyle Width="20%" />
						<ItemTemplate>
							<%# Container.DataItem("PlantingYear") %>
						</ItemTemplate>
						<EditItemTemplate>
							<%# Container.DataItem("PlantingYear") %>
						</EditItemTemplate>
					</asp:TemplateColumn>
	
					<asp:TemplateColumn HeaderText ="Crop Type" Sortexpression="CropType">
						<ItemStyle Width="15%" />
						<ItemTemplate>
							<%# objBD.mtdGetCropType(Container.DataItem("CropType")) %>
							<asp:Label id="lblCropType" Text='<%# Container.DataItem("CropType") %>' Visible=False runat="server" />

						</ItemTemplate>
						<EditItemTemplate>
							<asp:DropDownList id="ddlCrop" runat=server >
							</asp:DropDownList>
							<asp:RequiredFieldValidator id=validateCrop display=dynamic runat=server 
									text="<BR>Please Select a Type" ControlToValidate=ddlCrop />
						</EditItemTemplate>
					</asp:TemplateColumn>
	
					<asp:TemplateColumn HeaderText="Last Update"  Sortexpression="UpdateDate">
						<ItemStyle Width="10%" />
						<ItemTemplate>
							<%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
						</ItemTemplate>
						<EditItemTemplate >
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Updated By"  Sortexpression="UserName">
						<ItemStyle Width="10%" />
						<ItemTemplate>
							<%# Container.DataItem("UserName") %>
						</ItemTemplate>
						<EditItemTemplate >
						</EditItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn>
						<ItemStyle Width="20%" />
						<ItemTemplate>
						<asp:LinkButton id="Edit" CommandName="Edit"   Text="Edit"
							runat="server"/>
						</ItemTemplate>
						
						<EditItemTemplate>
						<asp:LinkButton id="Update" CommandName="Update" Text="Save"
							runat="server"/>
						<asp:LinkButton id="Delete" CommandName="Delete" Text="Delete"
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
					<td align=right colspan="6">
						<asp:ImageButton id="btnPrev" runat="server" imageurl="../../images/icn_prev.gif" alternatetext="Previous" commandargument="prev" onClick="btnPrevNext_Click" />
						<asp:DropDownList id="lstDropList" AutoPostBack="True" onSelectedIndexChanged="PagingIndexChanged" runat="server" />
						<asp:Imagebutton id="btnNext" runat="server"  imageurl="../../images/icn_next.gif" alternatetext="Next" commandargument="next" onClick="btnPrevNext_Click" />
					</td>
				</tr>

				<tr>
					<td align="left" ColSpan=6>
						<asp:ImageButton id=ibNew OnClick="DEDR_Add" imageurl="../../images/butt_new.gif" AlternateText="Add Block" runat="server"/>
					</td>
				</tr>
			</table>
				</FORM>

		</body>
</html>
