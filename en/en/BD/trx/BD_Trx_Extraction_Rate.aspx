<%@ Page Language="vb" src="../../../include/BD_Trx_Extraction_Rate.aspx.vb" Inherits="BD_Extraction_Rate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuBDTrx" src="../../menu/menu_BDTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Extraction Rate Transaction List</title>
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
		<body>
		    <form runat="server">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:label id="blnUpdate" Visible="False" Runat="server" />
			<asp:Label id=lblCode visible=false text=" Code" runat=server />
			<asp:label id=lblPleaseEnter visible=false text="Please enter " runat=server />
			<asp:label id=lblList visible=false text=" LIST" runat=server />
			<asp:label id="sortcol" Visible="False" Runat="server" />

			<table border="0" cellspacing="1" cellpadding="1" width="100%">
				<tr>
					<td colspan="6"><UserControl:MenuBDTrx id=menuBD runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="4" width=60%><asp:label id="lblTitle" runat="server" /></td>
					<td align="right" colspan="2" width=40%><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<td colspan=6 width=100% class="mb-c">
						<table width="100%" cellspacing="0" cellpadding="3" border="0" align="center">
							<tr class="mb-t">
								<td width="20%" height="26" valign=bottom>
									<asp:label id="lblMonthYear" runat="server" /> :<BR>
									<asp:TextBox id=srchMonthYear width=100% maxlength="7" runat="server"/>
								</td>
								<td width="32%" height="26" valign=bottom>&nbsp;</td>
								<td width="20%" height="26" valign=bottom>Status :<BR>
									<asp:DropDownList id="srchStatusList" width=100% runat=server />
								</td>
								<td width="20%" height="26" valign=bottom>Last Updated By :<BR><asp:TextBox id=srchUpdateBy width=100% maxlength="128" runat="server"/></td>
								<td width="8%" height="26" valign=bottom align=right><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click runat="server"/></td>
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
					<asp:TemplateColumn SortExpression="AccYear, AccMonth">
						<ItemStyle Width="15%" />
						<ItemTemplate>
							<%# Container.DataItem("MonthYear") %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox id="txtMonthYear" MaxLength="7" width=95%
									Text='<%# trim(Container.DataItem("MonthYear")) %>'
									runat="server"/>
							<BR>
							<asp:RequiredFieldValidator id=validateMonthYear display=dynamic runat=server 
									ControlToValidate=txtMonthYear />
							<asp:RegularExpressionValidator id=revMonthYear 
								ControlToValidate="txtMonthYear"
								ValidationExpression="\d{2}\/\d{4}"
								Display="Dynamic"
								text="2 digits month and 4 digits year separated with slash."
								runat="server"/>
							<asp:RegularExpressionValidator id=revMonthYear1
								ControlToValidate="txtMonthYear"
								ValidationExpression="([0][123456789]|[1][012])\/\d{4}"
								Display="Dynamic"
								text="Invalid month."
								runat="server"/>
							<asp:RegularExpressionValidator id=revMonthYear2
								ControlToValidate="txtMonthYear"
								ValidationExpression="\d{2}\/([1][9]|[23456789][0123456789])\d{2}"
								Display="Dynamic"
								text="Year must be at least 1900."
								runat="server"/>
							<asp:label id="lblDupMsg"  Text="Period already exist" Visible = false forecolor=red Runat="server"/>
						</EditItemTemplate>
					</asp:TemplateColumn>	
					<asp:TemplateColumn HeaderText="OER (%)" SortExpression="OER">
						<ItemStyle Width="15%" />
						<ItemTemplate>
							<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("OER"), 0, True, False, False)) %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox id="txtOER" width=100% MaxLength="3"
								Text='<%# FormatNumber(Container.DataItem("OER"),0, True, False, False) %>'
								runat="server"/>
							<asp:RequiredFieldValidator id=validateOER display=Dynamic runat=server 
									ControlToValidate=txtOER text="Please enter OER"/>															
							<asp:RegularExpressionValidator id="RegularExpressionValidatorOER" 
								ControlToValidate="txtOER"
								ValidationExpression="\d{1,3}"
								Display="Dynamic"
								text = "Maximum length 3 digits and 0 decimal points"
								runat="server"/>
						</EditItemTemplate>
					</asp:TemplateColumn>					
					<asp:TemplateColumn HeaderText="KER (%)" SortExpression="KER">
						<ItemStyle Width="15%" />
						<ItemTemplate>
							<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("KER"), 0, True, False, False)) %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox id="txtKER" width=100% MaxLength="3"
								Text='<%# FormatNumber(Container.DataItem("KER"),0, True, False, False) %>'
								runat="server"/>
							<asp:RequiredFieldValidator id=validateKER display=Dynamic runat=server 
									ControlToValidate=txtKER text="Please enter KER"/>															
							<asp:RegularExpressionValidator id="RegularExpressionValidatorKER" 
								ControlToValidate="txtKER"
								ValidationExpression="\d{1,3}"
								Display="Dynamic"
								text = "Maximum length 3 digits and 0 decimal points"
								runat="server"/>
						</EditItemTemplate>
					</asp:TemplateColumn>					
					<asp:TemplateColumn HeaderText="Last Update" SortExpression="BD.UpdateDate">
						<ItemStyle Width="15%" />
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
					<asp:TemplateColumn HeaderText="Status" SortExpression="BD.Status">
						<ItemStyle Width="15%" />
						<ItemTemplate>
							<%# objBD.mtdGetExtractRateStatus(Container.DataItem("Status")) %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:DropDownList Visible=False id="StatusList" size=1 runat=server />
							<asp:TextBox id="Status" Readonly=TRUE Visible = False
								Text='<%# Container.DataItem("Status")%>'
								runat="server"/>
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Updated By" SortExpression="UserName">
						<ItemStyle Width="15%" />
						<ItemTemplate>
							<%# Container.DataItem("UserName") %>
						</ItemTemplate>
						<EditItemTemplate >
							<asp:TextBox id="UserName" Readonly=TRUE size=8 
								Text='<%# Session("SS_USERID") %>'
								Visible=False runat="server"/>
						</EditItemTemplate>
					</asp:TemplateColumn>					
					<asp:TemplateColumn>					
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
					<td align="left" width="80%" ColSpan=6>
						<asp:ImageButton id=ibNew imageurl="../../images/butt_new.gif" OnClick="DEDR_Add" AlternateText="Add New Item" runat="server"/>
						<asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print onClick="btnPreview_Click" runat="server"/>
					</td>
				</tr>
			</table>
				</FORM>
		</body>
</html>
