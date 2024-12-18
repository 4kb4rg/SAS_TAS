<%@ Page Language="vb" src="../../../include/PR_Setup_ADgroup.aspx.vb" Inherits="PR_Setup_ADgroup" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRSetup" src="../../menu/menu_PRsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Allowance and Deduction Group List</title>
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
		<body>
		    <form id="ADgroup" runat="server">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:label id="blnUpdate" Visible="False" Runat="server" />
			<asp:label id="sortcol" Visible="False" Runat="server" />
			
			<table border="0" cellspacing="1" cellpadding="1" width="100%">
				<tr>
					<td colspan="6"><UserControl:MenuPRSetup id=menuPR runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="5">ALLOWANCE AND DEDUCTION GROUP LIST</td>
					<td align="right" colspan="1" ><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<td colspan=6 width=100% class="mb-c">
						<table width="100%" cellspacing="0" cellpadding="3" border="0" align="center">
							<tr class="mb-t">
								<td width="20%" height="26">AD Group Code :<br><asp:TextBox id="srchADGrpCd" width="100%" maxlength="8" runat="server"/></td>
								<td width="35%" height="26">Description :<br><asp:TextBox id="srchDescription" width="100%" maxlength="128" runat="server"/></td>
								<td width="15%" height="26">Status :<br><asp:DropDownList id="srchStatus" width="100%" runat="server"/></td>
								<td width="20%" height="26">Last Updated By :<br><asp:TextBox id="srchUpdBy" width="100%" maxlength="128" runat="server"/></td>
								<td width="10%" height="26" valign=bottom align=right><asp:Button Text="Search" OnClick=srchBtn_Click runat="server"/></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<TD colspan = 6 >					
					<asp:DataGrid id="EventData"
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
					<asp:TemplateColumn HeaderText="AD Group Code" ItemStyle-Width="21%" SortExpression="ADGrp.ADGrpCode">
						<ItemTemplate>
							<%# Container.DataItem("ADGrpCode") %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox id="ADGrpCode" MaxLength="8" width=95%
									Text='<%# trim(Container.DataItem("ADGrpCode")) %>'
									runat="server"/>
							<asp:RequiredFieldValidator id=validateCode display=dynamic runat=server 
									ErrorMessage="Please Enter Product Type Code"
									ControlToValidate="ADGrpCode" />
							<asp:RegularExpressionValidator id=revCode 
								ControlToValidate="ADGrpCode"
								ValidationExpression="[a-zA-Z0-9\-]{1,8}"
								Display="Dynamic"
								text="Alphanumeric without any space in between only."
								runat="server"/>
							<asp:label id="lblDupMsg"  Text="Code already exist" Visible = false forecolor=red Runat="server"/>

						</EditItemTemplate>
					</asp:TemplateColumn>	
					<asp:TemplateColumn HeaderText="Description" SortExpression="ADGrp.Description">
						<ItemTemplate>
							<%# Container.DataItem("Description") %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox id="Description" width=100% MaxLength="64"
								Text='<%# trim(Container.DataItem("Description")) %>'
								runat="server"/>
							<asp:RequiredFieldValidator id=validateDesc display=Dynamic runat=server 
									ErrorMessage="Please Enter Product Type Description"
									ControlToValidate="Description" />															
						</EditItemTemplate>
					</asp:TemplateColumn>					
					<asp:TemplateColumn HeaderText="Last Update" SortExpression="ADGrp.UpdateDate">
						<ItemTemplate>
							<%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
						</ItemTemplate>
						<EditItemTemplate >
							<asp:TextBox id="UpdateDate" Readonly=TRUE size=8 
								Visible=False Text='<%# objGlobal.GetLongDate(Now()) %>'
								runat="server"/>
							<asp:TextBox id="CreateDate" Visible=False
								Text='<%# Container.DataItem("CreateDate") %>'
								runat="server"/>
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Status" SortExpression="ADGrp.Status">
						<ItemTemplate>
							<%# objPR.mtdGetADGrpStatus(Container.DataItem("Status")) %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:DropDownList Visible=False id="StatusList" size=1 runat=server />
							<asp:TextBox id="Status" Readonly=TRUE Visible = False
								Text='<%# Container.DataItem("Status")%>'
								runat="server"/>
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Updated By" SortExpression="Usr.UserName">
						<ItemTemplate>
							<%# Container.DataItem("UserName") %>
						</ItemTemplate>
						<EditItemTemplate >
							<asp:TextBox id="UserName" Readonly=TRUE size=8 
								Text='<%# Session("SS_USERID") %>'
								Visible=False runat="server"/>
						</EditItemTemplate>
					</asp:TemplateColumn>					
					<asp:TemplateColumn ItemStyle-HorizontalAlign=Center>					
						<ItemTemplate>
							<asp:LinkButton id="Edit" CommandName="Edit" Text="Edit" runat="server"/>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:LinkButton id="Update" CommandName="Update" Text="Save" runat="server"/>
							<asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" runat="server"/>
							<asp:LinkButton id="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation=False runat="server"/>
						</EditItemTemplate>
					</asp:TemplateColumn>
					</Columns>
					</asp:DataGrid><BR>
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
						<asp:ImageButton id=ibNew OnClick="DEDR_Add" imageurl="../../images/butt_new.gif" AlternateText="New AD Group" runat="server"/>
						<asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print visible=false runat="server"/>
					</td>
				</tr>
			</table>
			</FORM>
		</body>
</html>
