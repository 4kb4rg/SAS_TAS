<%@ Page Language="vb" trace="False" src="../../../include/CT_Trx_StockReturn_Details.aspx.vb" Inherits="CT_ReturnDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuCTTrx" src="../../menu/menu_CTtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Canteen Return Details</title>		
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	
	<body>
		<form runat=server>
   		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
   		<asp:Label id=lblHidStatus visible=false runat=server />
		<asp:label id="issueType" Visible="False" Runat="server"></asp:label>
		<asp:label id=lblCode visible=false text=" Code" runat=server />
		<asp:label id=lblDocTitle visible=false text="Canteen Return" runat=server />

		<table border=0 width="100%" cellspacing="0" cellpadding="1">
			<tr>
				<td colspan=6><UserControl:MenuCTTrx EnableViewState=False id=menuCT runat="server" /></td>
			</tr>			
			<tr>
				<td class="mt-h" colspan=6>CANTEEN RETURN DETAILS</td>
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
				<td width="20%" height=25>Canteen Return ID :</td>
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
				<td><asp:Label id=lblPDateTag Text="Print Date :" runat=Server /></td>
				<td><asp:Label id=lblPrintDate  runat=server /></td>				
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td colspan=6>
				<table id="tblAdd" border=0 width="100%" cellspacing="0" cellpadding="4" runat="server">
					<tr class="mb-c">
						<td width="20%" height=25>Canteen Issue ID :*</td>
						<td width="80%"><asp:DropDownList id="lstStckIss" Width=100% AutoPostBack=True OnSelectedIndexChanged=CallLoadIssueDetails runat=server />
										<asp:label id=lblStckIssErr text="Please select one Canteen Issue ID" Visible=False forecolor=red Runat="server" /></td>
					</tr>
					<tr class="mb-c">
						<td height=25>Item Code :*</td>
						<td><asp:DropDownList id="lstItem" Width=100% AutoPostBack=True OnSelectedIndexChanged=ShowIssuedLnDetails runat=server EnableViewState=True />
							<asp:label id=lblItemCodeErr text="Please select one Item" Visible=False forecolor=red Runat="server" /></td>
					</tr>
					<tr id="RowAcc" class="mb-c">
						<td height=25><asp:label id="lblAccTag" Runat="server"/> </td>
						<td><asp:label id="lblAccCode" Width=100% runat=server /></td>
					</tr>
					<tr id="RowBlk" class="mb-c">
						<td height=25><asp:label id=lblBlkTag Runat="server"/>	</td>
						<td><asp:label id="lblBlock" Width=100% runat=server /></td>
					</tr>
					<tr id="RowVeh" class="mb-c">
						<td height=25><asp:label id="lblVehTag" Runat="server"/> </td>
						<td><asp:label id="lblVehCode" Width=100% runat=server /></td>
					</tr>
					<tr id="RowVehExp" class="mb-c">
						<td height=25><asp:label id="lblVehExpTag" Runat="server"/> </td>
						<td><asp:label id="lblVehExp" width=100% runat=server /></td>
					</tr>			
					<tr class="mb-c">
						<td height=25>Quantity to return :*</td>
						<td><asp:textbox id="txtQty" Width=50% maxlength=20 EnableViewState=False Runat="server" />
			                <asp:RegularExpressionValidator id="RegularExpressionValidatorQtyReq" 
								ControlToValidate="txtQty"
								ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
								Display="Dynamic"
								text = "<br>Maximum length 15 digits and 5 decimal points"
								runat="server"/>
							<asp:RequiredFieldValidator 
								id="validateQty" 
								runat="server" 
								ErrorMessage="<br>Please specify quantity to return" 
								ControlToValidate="txtQty" 
								display="dynamic"/>
							<asp:RangeValidator id="Range1"
								ControlToValidate="txtQty"
								MinimumValue="0.00001"
								MaximumValue="999999999999999.99999"
								Type="double"
								EnableClientScript="True"
								Text="<br>The value must be from 0.00001!"
								runat="server" display="dynamic"/>
							<asp:label id=lblerror text="Number generated is too big!" Visible=False forecolor=red Runat="server" />
							<asp:label id=lblStock text="Returned quantity cannot larger than issued quantity!" Visible=False forecolor=red Runat="server" />						
						</td>					
					</tr>
					<tr class="mb-c">
						<td colspan=2><asp:ImageButton id="Add" ImageURL="../../images/butt_add.gif" OnClick="btnAdd_Click" Runat="server" /></td>
					</tr>
				</table>
				</td>		
			</tr>
			<tr>
				<td colspan=6>&nbsp;</td>				
			</tr>
			<tr>
				<td colspan=6><asp:label id=lblUnDel text="<BR>Insufficient quantity to perform operation!" Visible=False forecolor=red Runat="server" />
	  						  <asp:label id=lblConfirmErr text="<BR>Document must contain transaction to Confirm !" Visible=False forecolor=red Runat="server" />
							  <asp:Label id=lblTxError visible=false Text="<BR>Cannot perform operation, please check data!" forecolor=red runat=server />			
				</td>				
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
						<asp:TemplateColumn HeaderText="Canteen Issue ID">
							<ItemStyle Width="8%"/> 																								
							<ItemTemplate>
								<%# Container.DataItem("StockIssueID") %>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Item">
							<ItemStyle Width="15%"/> 																								
							<ItemTemplate>
								<asp:label text=<%# Container.DataItem("ItemCode") %> id="ItemCode" runat="server" />
								(<%# Container.DataItem("Description")%>)		
								<asp:label text=<%# Container.DataItem("StockRtnLnID") %> id="RtnLnID" visible=false runat="server" />
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn>
							<ItemStyle Width="8%"/> 																								
							<ItemTemplate>
								<asp:label id="AccCode" text=<%# Container.DataItem("AccCode") %> runat="server" />
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn>
							<ItemStyle Width="8%"/> 																								
							<ItemTemplate>
								<%# Container.DataItem("BlkCode") %>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn>
							<ItemStyle Width="8%"/> 																								
							<ItemTemplate>
								<%# Container.DataItem("VehCode") %>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn>
							<ItemStyle Width="8%"/> 																								
							<ItemTemplate>
								<%# Container.DataItem("VehExpCode") %>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Quantity Returned">
							<HeaderStyle HorizontalAlign="Right" />			
							<ItemStyle Width="8%" HorizontalAlign="Right" />			
							<ItemTemplate>
								<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("Qty"),5) %> id="lblQtyTrxDisplay" runat="server" />							
								<asp:label text=<%# FormatNumber(Container.DataItem("Qty"),5) %> id="lblQtyTrx"  visible = "false" runat="server" />							
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Unit Cost">
							<HeaderStyle HorizontalAlign="Right" />			
							<ItemStyle Width="8%" HorizontalAlign="Right" />							
							<ItemTemplate>
								<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator(Container.DataItem("Cost")) %> id="lblUnitCostDisplay" runat="server" />
								<asp:label text=<%# FormatNumber(Container.DataItem("Cost"),5) %> id="lblUnitCost"  visible = "false" runat="server" />
							</ItemTemplate>							
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Cost Amount">
							<HeaderStyle HorizontalAlign="Right" />			
							<ItemStyle Width="8%" HorizontalAlign="Right" />						
							<ItemTemplate>
								<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator(Container.DataItem("Amount")) %> id="lblAmountDisplay" runat="server" />
								<asp:label text=<%# FormatNumber(Container.DataItem("Amount"),2) %> id="lblAmount"  visible = "false" runat="server" />
							</ItemTemplate>							
						</asp:TemplateColumn>		
						<asp:TemplateColumn HeaderText="Unit Price">
							<HeaderStyle HorizontalAlign="Right" />			
							<ItemStyle Width="8%" HorizontalAlign="Right" />							
							<ItemTemplate>
								<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator(Container.DataItem("Price")) %> id="lblUnitPriceDisplay" runat="server" />
								<asp:label text=<%# FormatNumber(Container.DataItem("Price"),5) %> id="lblUnitPrice"  visible = "false" runat="server" />
							</ItemTemplate>							
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Price Amount">
							<HeaderStyle HorizontalAlign="Right" />			
							<ItemStyle Width="8%" HorizontalAlign="Right" />						
							<ItemTemplate>
								<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator(Container.DataItem("PriceAmount")) %> id="lblPriceAmountDisplay" runat="server" />
								<asp:label text=<%# FormatNumber(Container.DataItem("PriceAmount"),2) %> id="lblPriceAmount"  visible = "false" runat="server" />
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
				<td colspan=3 height=25><hr size="1" noshade></td>
			</tr>
			<tr>
				<td colspan=3>&nbsp;</td>			
				<td colspan=3>
					<table width=100% border=0>
						<tr>
							<td width=18% height=25>Total Cost : </td>
							<td width=24% align=right> <asp:label id="lblTotAmtFig" runat="server" /></td>
							<td width=10%>&nbsp;</td>
							<td width=18% height=25>Total Price :</td>
							<td width=24% align=right> <asp:label id="lblTotPriceFig" runat="server" /></td>
							<td width=6%>&nbsp;</td>
						</tr>
					</table>id=srchUpdateBy 
				</td>						
			</tr>							
			<tr>
				<td colspan=6>&nbsp;</td>				
			</tr>
			<tr>
				<td>Remarks :</td>	
				<td colspan=5><asp:textbox id="txtRemarks" width="100%" maxlength="128" runat="server" /></td>
			</tr>
			<tr>
				<td colspan=6>&nbsp;</td>				
			</tr>
			<tr>
				<td align="left" colspan="6">
					<asp:ImageButton id="Save"     AlternateText="Save"    onClick="btnSave_Click"    ImageURL="../../images/butt_save.gif"    CausesValidation=False               runat="server" />
					<asp:ImageButton id="Confirm"  AlternateText="Confirm" onClick="btnConfirm_Click" ImageURL="../../images/butt_confirm.gif" CausesValidation=False               runat="server" />
					<asp:ImageButton id="Cancel"   AlternateText="Cancel"  onClick="btnCancel_Click"  ImageURL="../../images/butt_Cancel.gif"  CausesValidation=False Visible=False runat="server" />
					<asp:ImageButton id="Print"    AlternateText="Print"   onClick="btnPreview_Click" ImageURL="../../images/butt_print.gif"   CausesValidation=False               runat="server" />
					<asp:ImageButton id="PRDelete" AlternateText="Delete"  onClick="btnDelete_Click"  ImageURL="../../images/butt_delete.gif"  CausesValidation=False               runat="server" />
					<asp:ImageButton id="btnNew"   AlternateText="New"     onClick="btnNew_Click"     ImageURL="../../images/butt_new.gif"     CausesValidation=False               runat="server" />
					<asp:ImageButton id="Back"     AlternateText="Back"    onClick="btnBack_Click"    ImageURL="../../images/butt_back.gif"    CausesValidation=False               runat="server" />
				</td>
			</tr>		
		</table>
		</form>
	</body>
</html>
