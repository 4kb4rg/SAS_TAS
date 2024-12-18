<%@ Page Language="vb" trace="false" SmartNavigation="True"  src="../../../include/BD_setup_periods.aspx.vb" Inherits="BD_Periods" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuBDsetup" src="../../menu/menu_BDSetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Budget Periods</title>
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
		<body>
		    <form runat="server" ID="Form1">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="SortExpression" Visible="False" Runat="server"/>
			<asp:label id=lblBudgeting visible=false text="Budgeting " runat=server />
			<asp:label id="blnUpdate" Visible="False" Runat="server"/>
			<asp:label id="sortcol" Visible="False" Runat="server"/>
			<asp:label id="lblStartMthCtrl" Visible = false Runat="server"/>
			<asp:label id="lblStartYrCtrl" Visible = false Runat="server"/>
			<asp:label id="lblEndMthCtrl" Visible = false Runat="server"/>
			<asp:label id="lblEndYrCtrl" Visible = false Runat="server"/>
			<asp:label id="lblCode" text=" Code" Visible = false Runat="server"/>

			<table border="0" cellspacing="1" cellpadding="1" width="100%">
				<tr>
					<td colspan="6"><UserControl:MenuBDsetup id=menuIN runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="4" width=60%>Budgeting Periods</td>
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
					<td colspan=6><asp:label id="lblErrClose" forecolor=red text="Cannot close period when there is no future periods." Visible = false Runat="server"/></td>
				</tr>				
				<tr>
					<td colspan=6><asp:label id="lblErrCopyTemp" forecolor=red text="Error occuring while copying setup templates to next period." Visible = false Runat="server"/></td>
				</tr>				
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<TD colspan = 6 >					
					<asp:DataGrid id="PeriodData"
						AutoGenerateColumns="false" width="100%" runat="server"
						OnItemDataBound="DataGrid_ItemDataBound" 
						GridLines = none
						Cellpadding = "2"
						OnEditCommand="DEDR_Edit"
						OnUpdateCommand="DEDR_Update"
						OnCancelCommand="DEDR_Cancel"
						OnDeleteCommand="DEDR_Delete" >
						<HeaderStyle CssClass="mr-h" />							
						<ItemStyle CssClass="mr-l" />
						<AlternatingItemStyle CssClass="mr-r" />						
					<Columns>
					
					<asp:TemplateColumn HeaderText ="Budgeting Period Name" SortExpression="BGTPeriod">
						<ItemTemplate>
							<%# Container.DataItem("BGTPeriod") %>
							<asp:label id="lblPerID" Text='<%# Container.DataItem("PeriodID") %>'  Visible = false Runat="server"/>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox id="txtBGTPeriod" MaxLength="8" width=95%
									Text='<%# trim(Container.DataItem("BGTPeriod")) %>'
									runat="server"/>
							<BR>
							<asp:label id="lblDupMsg"  Text="Code already exist" Visible = false forecolor=red Runat="server"/>
							<asp:label id="lblPeriodID" Text='<%# Container.DataItem("PeriodID") %>'  Visible = false Runat="server"/>
							<asp:RequiredFieldValidator id=validateCode display=dynamic runat=server 
									ControlToValidate=txtBGTPeriod />
							<asp:RegularExpressionValidator id=revCode 
								ControlToValidate="txtBGTPeriod"
								ValidationExpression="[a-zA-Z0-9\-]{1,8}"
								Display="Dynamic"
								text="Alphanumeric without any space in between only."
								runat="server"/>
						</EditItemTemplate>
					</asp:TemplateColumn>
	
					<asp:TemplateColumn HeaderText="Budgeting Year" SortExpression="StartYear">
						<ItemTemplate>
							<asp:label id="lblStartYear" Text='<%# Container.DataItem("StartYear") %>'  Runat="server"/>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:label id="lblStartYear" Text='<%# Container.DataItem("StartYear") %>' Visible="False" Runat="server"/>
							<asp:DropDownList id="ddlYear" runat=server >
							</asp:DropDownList>
							<asp:RequiredFieldValidator id=validateDesc display=dynamic runat=server 
									ControlToValidate=ddlYear />		
						</EditItemTemplate>
					</asp:TemplateColumn>

					<asp:TemplateColumn HeaderText="Start Month" SortExpression="StartMonth">
						<ItemTemplate>
							<asp:label id="lblStartMonth" Text='<%# Container.DataItem("StartMonth") %>'  Runat="server"/>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox id="txtStartMonth" width=100% MaxLength="4"
								Text='<%# trim(Container.DataItem("StartMonth")) %>'
								runat="server"/>
							<asp:RequiredFieldValidator id=validateStrtMth display=dynamic runat=server 
									ControlToValidate=txtStartMonth />															
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="End Month" SortExpression="EndMonth">
						<ItemTemplate>
							<asp:label id="lblEndMonth" Text='<%# Container.DataItem("EndMonth") %>'  Runat="server"/>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox id="txtEndMonth" width=100% MaxLength="4"
								Text='<%# trim(Container.DataItem("EndMonth")) %>'
								runat="server"/>
							<asp:RequiredFieldValidator id=validateEndMth display=dynamic runat=server 
									ControlToValidate=txtEndMonth />															
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Status" >
						<ItemTemplate>
							<asp:label id="lblStatustxt" Text='<%# objBD.mtdGetPeriodStatus(Container.DataItem("Status")) %>' Runat="server"/>
							<asp:label id="lblStatus" Text='<%# Container.DataItem("Status") %>'  Visible = false Runat="server"/>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:label id="lblStatus" Text='<%# Container.DataItem("Status") %>'  Visible = false Runat="server"/>
						</EditItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Last Update" >
						<ItemTemplate>
							<%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
						</ItemTemplate>
						<EditItemTemplate >
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Updated By" >
						<ItemTemplate>
							<%# Container.DataItem("UserName") %>
						</ItemTemplate>
						<EditItemTemplate >
						</EditItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn>
					
						<ItemTemplate>
							<asp:LinkButton id="AddVote" OnClick="btnAddVote_Click" Visible=False Text="AddVote" runat="server"/>
							<asp:LinkButton id="Edit" CommandName="Edit" Visible=False Text="Edit" runat="server"/>
						</ItemTemplate>
						
						<EditItemTemplate>
						<asp:LinkButton id="Update" CommandName="Update" Text="Save"
							runat="server"/>
						<asp:LinkButton id="Delete" CommandName="Delete" Text="Delete"
							runat="server"/>
						<asp:LinkButton id="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation=False
							runat="server"/>
						<asp:label id="lblPeriodErr" Text='<BR>Inconsistency in budgeting period<BR> please check data!' forecolor=red Visible=False Runat="server"/>

						</EditItemTemplate>
					</asp:TemplateColumn>

					</Columns>
					</asp:DataGrid>
					</td>
					</tr>
				<tr>
				<td align=right colspan="6">
				</td>
				</tr>
				<tr>
					<td align="left" ColSpan=6>
						<asp:ImageButton id=ibNew OnClick="DEDR_Add" imageurl="../../images/butt_new.gif" AlternateText="New Period" runat="server"/>
						<asp:ImageButton id=ibClose OnClick="DEDR_Close" imageurl="../../images/butt_close.gif" AlternateText="Close Budgeting Period" runat="server"/>
					</td>
				</tr>
			</table>
			</FORM>
		</body>
</html>
