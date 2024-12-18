<%@ Page Language="vb" src="../../../include/IN_Trx_StockTransferInternal_Details.aspx.vb" Inherits="IN_StockTransferInternal" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuINTrx" src="../../menu/menu_INtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Stock Transfer Details</title>		
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<form id=frmMain runat=server>
   		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
		<asp:Label id=lblTo visible=false text="To " runat=server />
		<asp:Label id=lblCode visible=false text=" Code " runat=server />
		<asp:Label id=lblPleaseSelectOne visible=false text="Please select one " runat=server />
		<asp:Label id=lblSelect visible=false text="Select " runat=server />
		<asp:label id="lblStatusHid" Visible="False" Runat="server" />
		<asp:label id=lblDocTitle visible=false text="Stock Transfer" runat=server />			
		<table border=0 width="100%" cellspacing="0" cellpading="1">
			<tr>
				<td colspan=6><UserControl:MenuINTrx enableviewstate=false id=menuIN runat="server" /></td>
			</tr>			
			<tr>
				<td class="mt-h" colspan=6>INTERNAL STOCK TRANSFER DETAILS</td>
			</tr>
			<tr>
				<td colspan=6><hr style="width :100%" /></td>
			</tr>			
			<tr>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td><asp:Label id=lblReprint  Text="<B>( R E P R I N T )</B><br>" Visible=False forecolor=Red runat=server />
				</td>				
			</tr>		
			<tr>
				<td width="20%" height=25>Stock Transfer ID :</td>
				<td width="30%"><asp:label id=lblStckTxID Runat="server"/></td>
				<td width="5%">&nbsp;</td>
				<td width="15%">Status :</td>
				<td width="24%"><asp:Label id=Status runat=server /></td>
				<td width="6%">&nbsp;</td>
			</tr>
			<tr><td height=25>Transfer From :*</td>
				<td><asp:DropDownList id="ddlInventoryBin1" Width=100% runat=server/>
				    <asp:Label id=lblInventoryBin1 text="Please Select Inventory Bin" forecolor=red visible=false runat=server /></td>
				<td>&nbsp;</td>
				<td>Date Created :</td>
				<td><asp:Label id=CreateDate runat=server /></td>
				<td>&nbsp;</td>
			</tr>			
			<tr><td height=25>Transfer To :*</td>
				<td><asp:DropDownList id="ddlInventoryBin2" Width=100% runat=server/>
				    <asp:Label id=lblInventoryBin2 text="Please Select Inventory Bin" forecolor=red visible=false runat=server /></td>
				<td>&nbsp;</td>
				<td>Last Update :</td>
				<td><asp:Label id=UpdateDate runat=server /></td>		
				<td>&nbsp;</td>
			</tr>			
			<tr>
				<td height=25>Description :</td>
				<td><asp:TextBox id=txtDesc size=1 width=100% textmode="Multiline" Runat="server"/></td>
				<td>&nbsp;</td>
				<td>Updated By :</td>
				<td><asp:Label id=UpdateBy runat=server /></td>				
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td><asp:Label id=lblPDateTag visible=False Text="Print Date :" runat=Server /></td>
				<td><asp:Label id=lblPrintDate  runat=server /></td>				
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>	
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td colspan=6>
				<table id="tblAdd" border=0 width="100%" cellspacing="0" cellpadding="4" runat="server">
					<tr class="mb-c">
						<td width="20%">Item Code :*</td>
						<td width="80%"><asp:DropDownList id="lstItem" Width=90% runat=server />
							<input type=button value=" ... " id="FindIN"  onclick="javascript:findcode('frmMain','','','','','','','','','4','lstItem','','','','','',hidBlockCharge.value,hidChargeLocCode.value);" CausesValidation=False runat=server />
							<asp:label id=lblItemCodeErr text="<br>Please select one Item" Visible=False forecolor=red Runat="server" />
						</td>
					</tr>
					<tr class="mb-c">
						<td>Quantity to Transfer :*</td>
						<td><asp:textbox id="txtQty" Width=50% maxlength=15 EnableViewState="False" Runat="server" />
			                <asp:RegularExpressionValidator id="RegularExpressionValidatorQtyReq" 
								ControlToValidate="txtQty"
								ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"
								Display="Dynamic"
								text = "Maximum length 9 digits and 5 decimal points"
								runat="server"/>
							<asp:RequiredFieldValidator 
								id="validateQty" 
								runat="server" 
								ErrorMessage="Please specify quantity to transfer!" 
								ControlToValidate="txtQty" 
								display="dynamic"/>
							<asp:RangeValidator id="Range1"
								ControlToValidate="txtQty"
								MinimumValue="0"
								MaximumValue="999999999999999"
								Type="double"
								EnableClientScript="True"
								Text="The value is out of acceptable range!"
								runat="server" display="dynamic"/>
							<asp:label id=lblerror text="Number generated is too big!" Visible=False forecolor=red Runat="server" />
							<asp:label id=lblStock text="Not enough stock in hand!" Visible=False forecolor=red Runat="server" />
						</td>
					</tr>
					<tr class="mb-c">
					    <td>Additional Note :</td>
				        <td><asp:TextBox id=txtAddNote size=1 width=50% textmode="Multiline" Runat="server"/></td>
					</tr>
					<tr class="mb-c">
						<td colspan=2><asp:ImageButton text="Add" id="Add" ImageURL="../../images/butt_add.gif" OnClick="btnAdd_Click" Runat="server" /></td>
					</tr>
				</table>
				</td>		
			</tr>
			<tr>
				<td colspan=6>&nbsp;</td>				
			</tr>
			<tr>
				<td colspan=6><asp:label id=lblConfirmErr text="Document must contain transaction to Confirm!" Visible=False forecolor=red Runat="server" />
							  <asp:label id=lblUnDel text="Insufficient stock in Inventory to perform operation!" Visible=False forecolor=red Runat="server" /></td>
			</tr>
			<tr>
				<td colspan=6> 
					<asp:DataGrid id="dgStkTx"
						AutoGenerateColumns="false" width="100%" runat="server"
						OnItemCreated="DataGrid_ItemCreated" 
						GridLines = none
						Cellpadding = "2"
						Pagerstyle-Visible="False"
						OnDeleteCommand="DEDR_Delete"
						AllowSorting="True">	
						<HeaderStyle CssClass="mr-h" />							
						<ItemStyle CssClass="mr-l" />
						<AlternatingItemStyle CssClass="mr-r" />						
						<Columns>
						<asp:TemplateColumn HeaderText="Item">
							<ItemStyle Width="40%"/> 																								
							<ItemTemplate>
								<asp:label text=<%# Container.DataItem("ItemCode") %> id="ItemCode" runat="server" />
								( <%# Container.DataItem("Description") %> )					
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Quantity Transfered">
							<HeaderStyle HorizontalAlign="Right" />			
							<ItemStyle Width="20%" HorizontalAlign="Right" />			
							<ItemTemplate>
								<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("Qty"),5) %> id="lblQtyTrx" runat="server" />							
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Additional Note">
							<HeaderStyle HorizontalAlign="Right" />			
							<ItemStyle Width="20%" HorizontalAlign="Right" />			
							<ItemTemplate>
								<asp:label text=<%# Container.DataItem("AdditionalNote") %> id="AddNote" runat="server" />							
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn>		
							<ItemStyle Width="5%" HorizontalAlign="Right" />							
							<ItemTemplate>
								<asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" CausesValidation =False runat="server" />
							</ItemTemplate>
						</asp:TemplateColumn>
						</Columns>										
					</asp:DataGrid>
				</td>	
			</tr>
			<tr>
				<td colspan=3>&nbsp;</td>
				<td colspan=2 height=25><hr style="width :100%" /></td>
				<td>&nbsp;</td>			
			</tr>				
			
			<tr>
				<td colspan=6>&nbsp;</td>				
			</tr>
			<tr>
				<td height=25>Remarks :</td>	
				<td colspan="5"><asp:textbox id="txtRemarks" width="98%" maxlength="128" runat="server" /></td>
			</tr>
			<tr>
				<td colspan=6>&nbsp;</td>				
			</tr>
			<tr>
				<td align="left" colspan="6">
					<asp:ImageButton id="Save"      AlternateText="Save"       onClick="btnSave_Click"      ImageURL="../../images/butt_save.gif"      CausesValidation=False runat="server" />
					<asp:ImageButton id="Confirm"   AlternateText="Confirm"    onClick="btnConfirm_Click"   ImageURL="../../images/butt_confirm.gif"   CausesValidation=False runat="server" />
					<asp:ImageButton id="Cancel"    AlternateText="Cancel"     onClick="btnCancel_Click"    ImageURL="../../images/butt_Cancel.gif"    CausesValidation=False runat="server" />
					<asp:ImageButton id="Print"     AlternateText="Print"      onClick="btnPreview_Click"   ImageURL="../../images/butt_print.gif"     CausesValidation=False runat="server" />
					<asp:ImageButton id="PRDelete"  AlternateText="Delete"     onClick="btnDelete_Click"    ImageURL="../../images/butt_delete.gif"    CausesValidation=False runat="server" />
					<asp:ImageButton id="btnNew"    AlternateText="New"        onClick="btnNew_Click"       ImageURL="../../images/butt_new.gif"       CausesValidation=False runat="server" />
					<asp:ImageButton id="Back"      AlternateText="Back"       onClick="btnBack_Click"      ImageURL="../../images/butt_back.gif"      CausesValidation=False runat="server" />
				</td>
			</tr>		
		</table>
			<Input type=hidden id=hidBlockCharge value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>
		</form>
	</body>
</html>
