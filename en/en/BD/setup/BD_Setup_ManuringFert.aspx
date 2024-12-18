<%@ Page Language="vb" trace="False" src="../../../include/BD_setup_ManuringFert.aspx.vb" Inherits="BD_ManuringFert" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuBDsetup" src="../../menu/menu_BDSetup.ascx"%>
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
			<asp:label id="lbloper" Visible="False" Runat="server" />
			<asp:label id=lblBudgeting visible=false text="Budgeting " runat=server />
			<asp:label id="blnUpdate" Visible="False" Runat="server" />
			<asp:label id="lblCode" text=" Code" Visible = false Runat="server"/>

			<table border="0" cellspacing="1" cellpadding="1" width="100%">
				<tr>
					<td colspan="6"><UserControl:MenuBDsetup id=menuBD runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="4" width=60%>MANURING SCHEDULE SUMMARY</td>
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
								<td width="20%" height="26" valign=bottom>Item Code :<BR>
									<asp:TextBox id=srchFertItemCode width=100% maxlength="8" runat="server"/>
   							    </td>
								<td width="37%" height="26" valign=bottom>Description :<BR>
									<asp:TextBox id=srchDesc width=100% maxlength="64" runat="server"/>
								</td>
								<td width="15%" height="26" valign=bottom>Status :<BR>
									<asp:DropDownList id="srchStatusList" width=100% runat=server />
								</td>								
								<td width="20%" height="26" valign=bottom>Last Updated By :<BR><asp:TextBox id=srchUpdateBy width=100% maxlength="128" runat="server"/></td>
								<td width="8%" height="26" valign=bottom align=right><asp:Button Text="Search" OnClick=srchBtn_Click runat="server"/></td>
							</tr>
						</table>
					</td>
				</tr>

				<tr>
					<TD colspan = 6 >					
					<asp:DataGrid id="dgManuringFert"
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
					<asp:TemplateColumn HeaderText="Item Code" SortExpression="FertItemCode">
						<ItemStyle Width="20%" />
						<ItemTemplate>
							<%# Container.DataItem("FertItemCode") %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox id="txtItemCode" width=100% MaxLength="8" visible="False" 
							Text='<%# trim(Container.DataItem("FertItemCode")) %>'
							runat="server"/>
							<asp:DropDownList id=ddlFertItemCode width=100% runat=server size="1"/>
							<asp:Label id=lblDupMsg forecolor=red visible = false text="Code already exist" runat=server /></td>
		   			    </EditItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Description" Sortexpression="Description" >
						<ItemStyle Width="40%" />
						<ItemTemplate>
							<%# Container.DataItem("Description") %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox id="txtDesc" MaxLength="64" width=95%
									Text='<%# trim(Container.DataItem("Description")) %>'
									runat="server"/>						
							<asp:RequiredFieldValidator id=validateDesc display=Dynamic runat=server 
									ControlToValidate=txtDesc 
									text="<br>Description cannot be blank." />																							
						</EditItemTemplate>
					</asp:TemplateColumn>
							
					<asp:TemplateColumn HeaderText="Status" Sortexpression="MS.Status" >
						<ItemStyle Width="10%" />
						<ItemTemplate>
							<%# objBD.mtdGetPeriodStatus(Container.DataItem("Status")) %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:DropDownList Visible=False id="StatusList" size=1 runat=server />
							<asp:TextBox id="txtStatus" Readonly=TRUE Visible = False
								Text='<%# Container.DataItem("Status")%>'
								runat="server"/>
						</EditItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Last Update"  Sortexpression="MS.UpdateDate">
						<ItemStyle Width="10%" />
						<ItemTemplate>
							<%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
						</ItemTemplate>
						<EditItemTemplate >
							<asp:TextBox id="txtUpdateDate" Readonly=TRUE size=8 
								Visible=False Text='<%# objGlobal.GetLongDate(Now()) %>'
								runat="server"/>
						</EditItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Updated By"  Sortexpression="UserName">
						<ItemStyle Width="10%" />
						<ItemTemplate>
							<%# Container.DataItem("UserName") %>
						</ItemTemplate>
						<EditItemTemplate >
							<asp:TextBox id="txtUserName" Readonly=TRUE size=8 
								Text='<%# Session("SS_USERID") %>'
								Visible=False runat="server"/>
						</EditItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn>					
						<ItemTemplate>
							<asp:LinkButton id="Edit" CommandName="Edit"   Text="Edit" runat="server"/>
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
					<td align=right colspan="6">
						<asp:ImageButton id="btnPrev" runat="server" imageurl="../../images/icn_prev.gif" alternatetext="Previous" commandargument="prev" onClick="btnPrevNext_Click" />
						<asp:DropDownList id="lstDropList" AutoPostBack="True" onSelectedIndexChanged="PagingIndexChanged" runat="server" />
						<asp:Imagebutton id="btnNext" runat="server"  imageurl="../../images/icn_next.gif" alternatetext="Next" commandargument="next" onClick="btnPrevNext_Click" />
					</td>
					</tr>
					<tr>
						<td>&nbsp;</td>
						<td>&nbsp;</td>
						<td>&nbsp;</td>
					</tr>
					<td align="left" ColSpan=6>
						<asp:ImageButton id=ibNew OnClick="DEDR_Add" imageurl="../../images/butt_new.gif" AlternateText="New Manuring" runat="server"/>
					</td>
				</tr>
			</table>
		</FORM>
	</body>
</html>
