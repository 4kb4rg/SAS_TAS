<%@ Page Language="vb" trace="False" SmartNavigation="False" src="../../../include/BD_trx_TitleArea.aspx.vb" Inherits="BD_TitleArea" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuBDTrx" src="../../menu/menu_BDTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Budgeting - Title Area</title>
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
			<asp:Label id=lblCode text=" Code" Visible="False" runat="server" />

			<table border="0" cellspacing="1" cellpadding="1" width="100%">
				<tr>
					<td colspan="6"><UserControl:MenuBDTrx id=menuBD runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="4" width=60%>TITLE AREA</td>
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
					<td colspan=6 width=100% class="mb-c">
						<table width="100%" cellspacing="0" cellpadding="3" border="0" align="center">
							<tr class="mb-t">
								<td width="20%" height="25" valign=bottom>Area :<BR>
									<asp:TextBox id=srchAreaCode width=100% maxlength="8" runat="server"/>
								</td>
								<td width="35%" height="25" valign=bottom>Description :<BR>
									<asp:TextBox id=srchDesc width=100% maxlength="64" runat="server"/>
								</td>
								<td width="17%" height="25" valign=bottom>Title Area :<BR>
									<asp:TextBox id=srchSize width=100% maxlength="64" runat="server"/>
								</td>
								<td width="20%" height="25" valign=bottom>Last Updated By :<BR><asp:TextBox id=srchUpdateBy width=100% maxlength="128" runat="server"/></td>
								<td width="8%" height="25" valign=bottom align=right><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click runat="server"/></td>
							</tr>
						</table>
					</td>
				</tr>

				<tr>
					<TD colspan = 6 >					
					<asp:DataGrid id="TitleData"
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
					
						<asp:TemplateColumn HeaderText ="Area" Sortexpression="AreaCode" >
							<ItemStyle width=10% />
							<ItemTemplate>
								<%# Container.DataItem("AreaCode") %>
							</ItemTemplate>
							<EditItemTemplate>
							<asp:label id="lblAreaCode" Visible=False Text='<%# trim(Container.DataItem("AreaCode")) %>' runat="server"/>
								<asp:TextBox id="txtArea" MaxLength="8" width=95%
										Text='<%# trim(Container.DataItem("AreaCode")) %>'
										runat="server"/>
								<BR>
								<asp:label id="lblDupMsg"  Text="Code already exist" Visible = false forecolor=red Runat="server"/>
								<asp:label id="lblPeriodID" Text='<%# Container.DataItem("PeriodID") %>'  Visible = false Runat="server"/>
								<asp:RequiredFieldValidator id=validateCode 
									display=dynamic 
									runat=server 
									ControlToValidate=txtArea />
								<asp:RegularExpressionValidator id=revCode 
									ControlToValidate="txtArea"
									ValidationExpression="[a-zA-Z0-9\-]{1,8}"
									Display="Dynamic"
									text="Alphanumeric without any space in between only."
									runat="server"/>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText ="Description" Sortexpression="Description">
							<ItemStyle width=25% />
							<ItemTemplate>
								<%# Container.DataItem("Description") %>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox id="txtDesc" MaxLength="32" width=95%
									Text='<%# trim(Container.DataItem("Description")) %>'
									runat="server"/>
							</EditItemTemplate>
						</asp:TemplateColumn>
		
						<asp:TemplateColumn HeaderText ="Title Area" Sortexpression="AreaSize">
							<ItemStyle width=12% />
							<ItemTemplate>
								<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("AreaSize"), 0, True, False, False)) %>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox id="txtSize" MaxLength="22" width=95%
									Text='<%# FormatNumber(Container.DataItem("AreaSize"), 0, True, False, False) %>'
									runat="server"/>
								<asp:RegularExpressionValidator id="RegularExpressionValidatorQtyReq" 
									ControlToValidate="txtSize"
									ValidationExpression="\d{1,19}"
									Display="Dynamic"
									text = "Maximum length 19 digits and 0 decimal points"
									runat="server"/>
								<asp:RequiredFieldValidator 
									id="validateQty" 
									runat="server" 
									ErrorMessage="Please Specify Quantity To Transfer" 
									ControlToValidate="txtSize" 
									display="dynamic"/>
								<asp:RangeValidator id="Range1"
									ControlToValidate="txtSize"
									MinimumValue="1"
									MaximumValue="9999999999999999999"
									Type="Double"
									EnableClientScript="True"
									Text="The value must be from 1 !"
									runat="server" display="dynamic"/>
									
								<asp:label id=lblerror text="Number generated is too big !" Visible=False forecolor=red Runat="server" />
										
							</EditItemTemplate>
						</asp:TemplateColumn>
		
						<asp:TemplateColumn HeaderText="Status" >
							<ItemStyle width=10% />
							<ItemTemplate>
								<%# objBD.mtdGetPeriodStatus(Container.DataItem("Status")) %>
							</ItemTemplate>
							<EditItemTemplate>
							</EditItemTemplate>
						</asp:TemplateColumn>
						
						<asp:TemplateColumn HeaderText="Last Update"  Sortexpression="UpdateDate">
							<ItemStyle width=10% />
							<ItemTemplate>
								<%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
							</ItemTemplate>
							<EditItemTemplate >
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Updated By"  Sortexpression="UserName">
							<ItemStyle width=15% />
							<ItemTemplate>
								<%# Container.DataItem("UserName") %>
							</ItemTemplate>
							<EditItemTemplate >
							</EditItemTemplate>
						</asp:TemplateColumn>
						
						<asp:TemplateColumn>
							<ItemStyle width=10% horizontalalign=right />				
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
					</asp:DataGrid>
					</td>
					</tr>
					<tr>
						<td height=25 colspan =2><hr size="1" noshade></td>
						<td>&nbsp;</td>
						<td>&nbsp;</td>
					</tr>
					<tr>
						<td width="30%" align="right" colspan="1"><asp:label id="lblTotalArea" runat="server" /></td>
						<td width="60%" align="Left"><asp:label id="lblTotAmtFig" runat="server" /></td>						
						<td width="10%">&nbsp;</td>
					</tr>
					<tr>
					<td align="left" ColSpan=6>
						<asp:ImageButton id=ibNew OnClick="DEDR_Add" imageurl="../../images/butt_new.gif" AlternateText="New Area" runat="server"/>
					</td>
				    </tr>
			</table>
			</FORM>
		</body>
</html>
