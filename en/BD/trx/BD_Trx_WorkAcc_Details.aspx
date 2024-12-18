<%@ Page Language="vb" trace="false" SmartNavigation="True" src="../../../include/BD_Trx_WorkAcc_Details.aspx.vb" Inherits="BD_WorkAcc_Details" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuBDtrx" src="../../menu/menu_BDTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Working Account Details</title>
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain runat="server">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="lblOper" Visible="False" Runat="server" />
			<asp:Label id=lblBgtStatus Visible="False"  runat="server" />
			<table border=0 cellspacing=0 cellpadding=2 width=100%>
				<tr>
					<td colspan="5">
						<UserControl:MenuBDtrx id=MenuBDtrx runat="server" />
					</td>
				</tr>
				<tr>
					<td class="mt-h" colspan="5">WORKING ACCOUNT DETAILS</td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<td height=25><asp:label id="lblWorkAccIDTag" Text="Working Account ID" runat="server" /> :</td>
					<td><asp:Label id=lblWorkAccID runat=server/></td>
					<td>&nbsp;</td>
					<td><asp:label id="lblBgtTag" text="Budgeting Period" runat="server"/> :</td>
					<td><asp:Label id=lblBgtPeriod runat=server /></td>
				</tr>
				<tr>
					<td height=25>Name :*</td>
					<td>
						<asp:Textbox id=txtWorkAccName width=100% maxlength=32 runat=server/>
						<asp:RequiredFieldValidator id=rfvWorkAccName display=Dynamic runat=server 
								ErrorMessage="<br>Please Enter Working Account Name."
								ControlToValidate=txtWorkAccName />
					</td>
					<td>&nbsp;</td>
					<td>Status : </td>
					<td><asp:Label id=lblStatus runat=server /></td>
				</tr>
				<tr>
					<td height=25 width=20%>&nbsp;</td>
					<td width=30%>&nbsp;</td>
					<td width=5%>&nbsp;</td>
					<td width=20%>Date Created : </td>
					<td width=25%><asp:Label id=lblDateCreated runat=server/></td>
				</tr>
				<tr>
					<td height=25>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>Last Updated : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
				</tr>
				<tr>
					<td height=25>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>Updated By : </td>
					<td><asp:Label id=lblUpdatedBy runat=server /></td>
				</tr>
				<tr>
					<td height=25 colspan="5">&nbsp;</td>
				</tr>				
				<tr>
					<TD colspan = 5 >					
					<asp:Label id=lblErrMessage2 visible=false forecolor=red Text="Error updating database (overflow)" runat=server />
					<asp:DataGrid id="dgWorkAccLn"
						AutoGenerateColumns="false" width="100%" runat="server"
						OnItemDataBound="DataGrid_ItemDataBound" 
						GridLines = none
						Cellpadding = "2"
						OnEditCommand="DEDR_Edit"
						OnUpdateCommand="DEDR_Update"
						OnCancelCommand="DEDR_Cancel"
						OnDeleteCommand="DEDR_Delete"
						Pagerstyle-Visible="False"
						OnSortCommand="Sort_Grid" 
						AllowSorting="false">
						<HeaderStyle font-bold=true CssClass="mr-h" />							
						<ItemStyle CssClass="mr-l" />
						<AlternatingItemStyle CssClass="mr-r" />						
					<Columns>
					
					<asp:TemplateColumn HeaderText="Details" SortExpression="Details">
						<ItemStyle Width="25%" />
						<ItemTemplate>
							<%# Container.DataItem("Details") %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:Label id="lblWorkAccLnID" Text='<%# Container.DataItem("WorkAccLnID") %>' Visible=False runat="server" />
							<asp:TextBox id="txtDet" MaxLength="64" width=95%
									Text='<%# trim(Container.DataItem("Details")) %>'
									runat="server"/>
							<BR>
							<asp:RequiredFieldValidator id=validateDet display=dynamic runat=server 
								text = "Please enter details"
								ControlToValidate=txtDet />
						</EditItemTemplate>
					</asp:TemplateColumn>
	
					<asp:TemplateColumn HeaderText="Labour Cost" SortExpression="LabourCost">
						<ItemStyle Width="15%" />
						<ItemTemplate>
							<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("LabourCost"), 0, True, False, False)) %>

						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox id="txtLabourCost" width=100% MaxLength="19"
								Text='<%# FormatNumber(Container.DataItem("LabourCost"), 0, True, False, False) %>'
								runat="server"/>
							<asp:RequiredFieldValidator id=validateLabourCost display=dynamic runat=server 
									text = "Please enter Labour Cost"
									ControlToValidate=txtLabourCost />															
							<asp:RegularExpressionValidator id="RegExpValLabCost" 
								ControlToValidate="txtLabourCost"
								ValidationExpression="\d{1,19}"
								Display="Dynamic"
								text = "Maximum length 19 digits and 0 decimal points"
								runat="server"/>
							<asp:RangeValidator id="RangeLabourCost"
								ControlToValidate="txtLabourCost"
								MinimumValue="0"
								MaximumValue="9999999999999999999"
								Type="double"
								EnableClientScript="True"
								Text="The value is out of range !"
								runat="server" display="dynamic"/>
						</EditItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Material Cost" SortExpression="MaterialCost">
						<ItemStyle Width="15%" />
						<ItemTemplate>
							<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("MaterialCost"), 0, True, False, False)) %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox id="txtMatCost" width=100% MaxLength="19"
								Text='<%# FormatNumber(Container.DataItem("MaterialCost"), 0, True, False, False) %>'
								runat="server"/>
							<asp:RequiredFieldValidator id=validateMatCost display=dynamic runat=server 
									text = "Please enter Material Cost"
									ControlToValidate=txtMatCost />															
							<asp:RegularExpressionValidator id="RegExpValMatCost" 
								ControlToValidate="txtMatCost"
								ValidationExpression="\d{1,19}"
								Display="Dynamic"
								text = "Maximum length 19 digits and 0 decimal points"
								runat="server"/>
							<asp:RangeValidator id="RangeMatCost"
								ControlToValidate="txtMatCost"
								MinimumValue="0"
								MaximumValue="9999999999999999999"
								Type="double"
								EnableClientScript="True"
								Text="The value is out of range !"
								runat="server" display="dynamic"/>
						</EditItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="VRA Cost" SortExpression="VRACost">
					<ItemStyle Width="15%" />
					<ItemTemplate>
							<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("VRACost"), 0, True, False, False)) %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox id="txtVRACost" width=100% MaxLength="19"
								Text='<%# FormatNumber(Container.DataItem("VRACost"), 0, True, False, False) %>'
								runat="server"/>
							<asp:RequiredFieldValidator id=validateVRACost display=dynamic runat=server 
									text = "Please enter VRA cost"
									ControlToValidate=txtVRACost />															
							<asp:RegularExpressionValidator id="RegExpValVRACost" 
								ControlToValidate="txtVRACost"
								ValidationExpression="\d{1,19}"
								Display="Dynamic"
								text = "Maximum length 19 digits and 0 decimal points"
								runat="server"/>
							<asp:RangeValidator id="RangeVRACost"
								ControlToValidate="txtVRACost"
								MinimumValue="0"
								MaximumValue="9999999999999999999"
								Type="double"
								EnableClientScript="True"
								Text="The value is out of range !"
								runat="server" display="dynamic"/>
					</EditItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="OverHead Cost" SortExpression="OverHeadCost">
						<ItemStyle Width="15%" />
						<ItemTemplate>
							<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("OverHeadCost"), 0, True, False, False)) %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox id="txtOHCost" width=100% MaxLength="19"
								Text='<%# FormatNumber(Container.DataItem("OverHeadCost"), 0, True, False, False) %>'
								runat="server"/>
							<asp:RequiredFieldValidator id=validateOHCost display=dynamic runat=server 
									text = "Please enter Overhead cost"
									ControlToValidate=txtOHCost />															
							<asp:RegularExpressionValidator id="RegExpValOHCost" 
								ControlToValidate="txtOHCost"
								ValidationExpression="\d{1,19}"
								Display="Dynamic"
								text = "Maximum length 19 digits and 0 decimal points"
								runat="server"/>
							<asp:RangeValidator id="RangeOHCost"
								ControlToValidate="txtOHCost"
								MinimumValue="0"
								MaximumValue="9999999999999999999"
								Type="double"
								EnableClientScript="True"
								Text="The value is out of range !"
								runat="server" display="dynamic"/>
					</EditItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn visible=false HeaderText="Add Vote" SortExpression="AddVote">
					<ItemStyle Width="15%" />
					<ItemTemplate>
							<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("AddVote"), 0, True, False, False)) %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox id="txtAddVote" width=100% MaxLength="25"
								Text='<%# FormatNumber(Container.DataItem("AddVote"), 0, True, False, False) %>'
								runat="server"/>
							<asp:RequiredFieldValidator id=validateAddVote display=dynamic runat=server 
									text = "Please enter Additional Vote"
									ControlToValidate=txtAddVote />															
							<asp:RegularExpressionValidator id="RegExpValAddVote" 
								ControlToValidate="txtAddVote"
								ValidationExpression="\d{1,19}"
								Display="Dynamic"
								text = "Maximum length 19 digits and 0 decimal points"
								runat="server"/>
							<asp:RangeValidator id="RangeAddVote"
								ControlToValidate="txtAddVote"
								MinimumValue="0"
								MaximumValue="9999999999999999999"
								Type="double"
								EnableClientScript="True"
								Text="The value is out of range !"
								runat="server" display="dynamic"/>
					</EditItemTemplate>
					</asp:TemplateColumn>
										
					<asp:TemplateColumn HeaderText="Total" SortExpression="Total">
						<ItemStyle Width="15%" />
						<ItemTemplate>
							<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("Total"), 0, True, False, False)) %>
						</ItemTemplate>
						<EditItemTemplate>
						</EditItemTemplate>
					</asp:TemplateColumn>
											
					<asp:TemplateColumn>
						<ItemTemplate>
							<asp:LinkButton id="lbEdit" CommandName="Edit" Text="Edit" runat="server"/>
						</ItemTemplate>
						
						<EditItemTemplate>						
							<asp:LinkButton id="lbUpdate" CommandName="Update" Text="Save" runat="server"/>
							<asp:LinkButton id="lbDelete" CommandName="Delete" Text="Delete" runat="server"/>
							<asp:LinkButton id="lbCancel" CommandName="Cancel" Text="Cancel" CausesValidation=False runat="server"/>							
						</EditItemTemplate>
					</asp:TemplateColumn>

					</Columns>
					</asp:DataGrid>
					</td>
				</tr>	
				<tr>
					<td align="left" ColSpan=5>
						<table width="100%" cellspacing="0" cellpadding="3" border="0" align="center">
							<tr class="mb-t">
								<td width="24%" height="26" valign=bottom>
									<B>TOTAL</B>
								</td>
								<td width="15%" height="26" valign=bottom>
									<B><asp:Label id=lblTotLabourCost width=100%  runat="server"/></B>
								</td>
								<td width="15%" height="26" valign=bottom>
									<B><asp:Label id=lblTotMatCost width=100% runat="server"/></B>
								</td>
								<td width="14%" height="26" valign=bottom>
									<B><asp:Label id=lblTotVRACost width=100% runat="server"/></B>
								</td>
								<td width="15%" height="26" valign=bottom>
									<B><asp:Label id=lblTotOHCost width=100% runat="server"/></B>
								</td>
								<!--<td width="15%" height="26" valign=bottom visible=false >
									<B><asp:Label id=lblTotAVCost width=100% visible=false runat="server"/></B>
								</td>-->
								<td width="17%" height="26" valign=bottom>
									<B><asp:Label id=lblTot width=100% runat="server"/></B>
								</td>
							 </tr>
						</table>
					</td>
				</tr>							
				<tr>
					<td colspan="5"><asp:ImageButton id=ibNew OnClick="DEDR_Add" imageurl="../../images/butt_new.gif" AlternateText="New Details" runat="server"/>
									<asp:ImageButton id=ibAlloc OnClick="BtnAllocate_Click" imageurl="../../images/butt_allocate.gif" AlternateText="Allocate" runat="server"/></td>
				</tr>								
				<tr>
					<td colspan="5">&nbsp;</td>
				</tr>								
				<tr>
					<td colspan="5">
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn AlternateText=" Delete " CausesValidation=False imageurl="../../images/butt_delete.gif" onclick=Button_Click CommandArgument=Del runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					</td>
				</tr>
				<Input Type=Hidden id=HidWorkAccID runat=server />
				<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
			</table>
		</form>
	</body>
</html>
