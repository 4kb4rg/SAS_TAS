<%@ Page Language="vb" src="../../../include/CT_trx_CRADetails.aspx.vb" Inherits="CT_CRADetails" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuCTTrx" src="../../menu/menu_CTtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Canteen Return Advice Details</title>		
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<form id=frmMain runat=server>
   		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
		<asp:label id=lblDocTitle visible=false text="Canteen Return Advice" runat=server />			
		<asp:label id="lblStatusHid" Visible="False" Runat="server" />

		<table border=0 width="100%" cellspacing="0" cellpadding="0" >
			<tr>
				<td colspan=6><UserControl:MenuCTTrx enableviewstate=false id=menuCT runat="server" /></td>
			</tr>			
			<tr>
				<td class="mt-h" colspan=6>CANTEEN RETURN ADVICE DETAILS</td>
			</tr>
			<tr>
				<td colspan=6><hr size="1" noshade></td>
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
				<td width="20%" height=25>Canteen Return Advice ID :</td>
				<td width="30%"><asp:label id=lblStckTxID Runat="server"/></td>
				<td width="5%">&nbsp;</td>
				<td width="15%">Status :</td>
				<td width="25%"><asp:Label id=Status runat=server /></td>
				<td width="5%">&nbsp;</td>
			</tr>
			<tr>
				<td height=25>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>Date Created :</td>
				<td><asp:Label id=CreateDate runat=server /></td>
				<td>&nbsp;</td>
			</tr>			
			<tr>
				<td height=25>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>Last Update :</td>
				<td><asp:Label id=UpdateDate runat=server /></td>		
				<td>&nbsp;</td>
			</tr>			
			<tr>
				<td height=25>&nbsp;</td>
				<td>&nbsp;</td>
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
				<td colspan=6>
				<table id="tblAdd" border=0 width="100%" cellspacing="0" cellpadding="4" runat="server">
					<tr class="mb-c">
						<td width="20%" height=25>Canteen Receive ID :</td>
						<td width="80%"><asp:DropDownList id=lstDoc Width=100% AutoPostBack=True OnSelectedIndexchanged=RebindItemList runat=server /></td>
					</tr>
					<tr class="mb-c">
						<td height=25>Item Code :*</td>
						<td><asp:DropDownList id="lstItem" Width=90% runat=server EnableViewState=True />
							<input type=button value=" ... " id="FindCT"  onclick="javascript:findcode('frmMain','','','','','','','','','','','lstItem','','','','',hidBlockCharge.value,hidChargeLocCode.value);" CausesValidation=False runat=server />
							<asp:label id=lblItemCodeErr text="<BR>Please select one Item" Visible=False forecolor=red Runat="server" />
						</td>
					</tr>
					<tr class="mb-c">
						<td height=25>Quantity to Return :*</td>
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
								ErrorMessage="Please specify quantity to return" 
								ControlToValidate="txtQty" 
								display="dynamic"/>
							<asp:RangeValidator id="Range1"
								ControlToValidate="txtQty"
								MinimumValue="1"
								MaximumValue="999999999999999"
								Type="double"
								EnableClientScript="True"
								Text="The value must be from 1 !"
								runat="server" display="dynamic"/>
						</td>
					</tr>
					<tr class="mb-c">
						<td height=25>&nbsp;</td>
						<td><asp:label id=lblerror text="Number generated is too big!" Visible=False forecolor=red Runat="server" />
							<asp:label id=lblStock text="Quantity to return cannot exceed quantity received!" Visible=False forecolor=red Runat="server" />
							<asp:label id=lblPR text="Item not found in selected document!" Visible=False forecolor=red Runat="server" /></td>
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
				<td colspan="6"><asp:Label id=lblTxError visible=false Text="Cannot perform operation, please check data!" forecolor=red runat=server />
								<asp:label id=lblUnDel text="Insufficient stock in Inventory to perform operation!" Visible=False forecolor=red Runat="server" /></td>				
			</tr>					
			<tr>
				<td colspan=6> 
					<asp:DataGrid id="dgStkTx"
						AutoGenerateColumns="false" width="100%" runat="server"
						OnItemDataBound="DataGrid_ItemCreated" 
						GridLines = none
						Cellpadding = "2"
						Pagerstyle-Visible="False"
						OnDeleteCommand="DEDR_Delete"
						AllowSorting="True">	
						<HeaderStyle CssClass="mr-h" />							
						<ItemStyle CssClass="mr-l" />
						<AlternatingItemStyle CssClass="mr-r" />						
						<Columns>
						<asp:TemplateColumn HeaderText="Canteen Receive Ref. No.">
							<ItemStyle Width="15%"/> 																								
							<ItemTemplate>
								<%# Container.DataItem("DispAdvID") %>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Item">
							<ItemStyle Width="35%"/> 																								
							<ItemTemplate>
								<asp:label text=<%# Container.DataItem("Description") %> id="ItemDesc" runat="server" />
								<asp:label text=<%# Container.DataItem("ItemCode") %> id="ItemCode" visible=false runat="server" />
								<asp:label text=<%# "(" & Trim(Container.DataItem("DispAdvID")) & ")" %> visible=false id="DocID" runat="server" />
								<asp:label text=<%# Container.DataItem("ItemRetAdvlnID") %> id="RtnAdvLnID" visible=false runat="server" />
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Quantity To Return">
							<HeaderStyle HorizontalAlign="Right" />			
							<ItemStyle Width="15%" HorizontalAlign="Right" />			
							<ItemTemplate>
								<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("Qty"),5) %> id="lblQtyTrx" runat="server" />							
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Unit Cost">
							<HeaderStyle HorizontalAlign="Right" />			
							<ItemStyle Width="15%" HorizontalAlign="Right" />							
							<ItemTemplate>
								<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator(System.Math.Round(Container.DataItem("Cost"),0)) %> id="lblUnitCost" runat="server" />
							</ItemTemplate>							
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Amount">
							<HeaderStyle HorizontalAlign="Right" />			
							<ItemStyle Width="15%" HorizontalAlign="Right" />						
							<ItemTemplate>
								<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator(System.Math.Round(Container.DataItem("Amount"),0)) %> id="lblAmount" runat="server" />
							</ItemTemplate>							
						</asp:TemplateColumn>		
						<asp:TemplateColumn>		
							<ItemStyle Width="5%" HorizontalAlign="right" />							
							<ItemTemplate>
								<asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" CausesValidation=False runat="server" />
							</ItemTemplate>
						</asp:TemplateColumn>
						</Columns>										
					</asp:DataGrid>
				</td>	
			</tr>
			<tr>
				<td colspan=3>&nbsp;</td>
				<td colspan=2 height=25><hr size="1" noshade></td>
				<td width="5%">&nbsp;</td>					
			</tr>	
			<tr>
				<td colspan=3>&nbsp;</td>
				<td height=25>Total Amount :</td>
				<td align="right"><asp:label id="lblTotAmtFig" runat="server" /></td>						
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25>Remarks :</td>	
				<td colspan="5"><asp:textbox id="txtRemarks" width="100%" maxlength="256" runat="server" /></td>
			</tr>
			<tr>
				<td colspan=6>&nbsp;</td>				
			</tr>
			<tr>
				<td align="left" colspan="6">
					<asp:ImageButton id="Save"     AlternateText="Save"    onClick="btnSave_Click"    ImageURL="../../images/butt_save.gif"    CausesValidation=False runat="server" />
					<asp:ImageButton id="Confirm"  AlternateText="Confirm" onClick="btnConfirm_Click" ImageURL="../../images/butt_confirm.gif" CausesValidation=False runat="server" />
 					<asp:ImageButton id="Print"    AlternateText="Print"   onClick="btnPreview_Click" ImageURL="../../images/butt_print.gif"   CausesValidation=False runat="server" />
					<asp:ImageButton id="PRDelete" AlternateText="Delete"  onClick="btnDelete_Click"  ImageURL="../../images/butt_delete.gif"  CausesValidation=False runat="server" />
					<asp:ImageButton id="btnNew"   AlternateText="New"     onClick="btnNew_Click"     ImageURL="../../images/butt_new.gif"     CausesValidation=False runat="server" />
					<asp:ImageButton id="Back"     AlternateText="Back"    onClick="btnBack_Click"    ImageURL="../../images/butt_back.gif"    CausesValidation=False runat="server" />
					<asp:label id=lblConfirmErr text="<BR>Document Must Contain Transaction To Confirm !" Visible=False forecolor=red Runat="server" />
				</td>
			</tr>		
		</table>
			<Input type=hidden id=hidBlockCharge value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>
		</form>
	</body>
</html>
