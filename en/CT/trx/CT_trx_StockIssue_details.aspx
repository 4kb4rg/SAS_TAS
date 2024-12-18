<%@ Page Language="vb" trace="False" src="../../../include/CT_Trx_StockIssue_Details.aspx.vb" Inherits="CT_IssueDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuCTTrx" src="../../menu/menu_CTtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Canteen Issue Details</title>		
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<form id=frmMain runat=server>
   		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<input type=hidden id=hidPQID runat=server />
			<asp:label id="AccountCode" Visible="False" Text= "Account Code" Runat="server" />
			<asp:label id="BillParty" Visible="False" Text= "Bill Party" Runat="server"/>
			<asp:label id="EmployeeCode" Visible="False" Text= "Employee Code" Runat="server"/>
			<asp:label id="issueType" Visible="false" Runat="server"/>
			<asp:label id=lblCode visible=false text=" Code" runat=server />
			<asp:label id=lblSelect visible=false text="Select " runat=server />
			<asp:label id=lblPleaseSelectOne visible=false text="Please select one " runat=server />			
			<asp:label id="lblStatusHid" Visible="False" Runat="server"/>

		<table border=0 width="100%" cellspacing="0" cellpading="1">
			<tr>
				<td colspan=6><UserControl:MenuCTTrx EnableViewState=False id=menuCT runat="server" /></td>
			</tr>			
			<tr>
				<td class="mt-h" colspan=6>CANTEEN ISSUE DETAILS</td>
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
				<td width="20%" height=25>Stock Issue ID :</td>
				<td width="30%"><asp:label id=lblStckTxID Runat="server"/></td>
				<td width="5%">&nbsp;</td>
				<td width="15%">Status :</td>
				<td width="25%"><asp:Label id=Status runat=server /></td>
				<td width="5%">&nbsp;</td>
			</tr>
			<tr>
				<td height=25>
					<asp:label id=lblEmpTag text="Employee Code :*" Visible=False Runat="server"/>
					<asp:label id=lblBPartyTag Visible=False Runat="server"/>
				</td>
				<td>
					<asp:DropDownList id="lstBillParty" Width=100% Visible=False runat=server />
					<asp:DropDownList id="lstEmpID" Width=85% Visible=False runat=server />
					<input type=button value=" ... " id="FindEmp" Visible=False onclick="javascript:findcode('frmMain','','','','','','','','lstEmpID','','','','','','','',hidBlockCharge.value,hidChargeLocCode.value);" CausesValidation=False runat=server />
					<asp:label id=lblEmpErr text="Please select one Employee Code" Visible=False forecolor=red Runat="server" />
					<asp:label id=lblBillPartyErr Visible=False forecolor=red Runat="server" />
				</td>
				<td>&nbsp;</td>
				<td>Date Created :</td>
				<td><asp:Label id=CreateDate runat=server /></td>
				<td>&nbsp;</td>
			</tr>			
			<tr>
				<td height=25><asp:label id=lblBillTo text="Bill To :*" Visible=False Runat="server"/></td>
				<td><asp:DropDownList id="lstBillTo" Width=100% Visible=False runat=server /></td>
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
				<td>Print Date :</td>
				<td><asp:Label id=lblPrintDate runat=server /></td>				
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td><asp:Label id=lblDNIDTag visible=False Text="Debit Note ID :" runat=Server /></td>
				<td><asp:Label id=lblDNNoteID  runat=server /></td>				
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td colspan=6>
				<table id="tblAdd" border=0 width="100%" cellspacing="0" cellpadding="4" runat="server">
					<tr class="mb-c">
						<td width="20%" height=25>Item Code :*</td>
						<td width="80%"><asp:DropDownList id="lstItem" Width=90% runat=server EnableViewState=True />
										<input type=button value=" ... " id="Find"  onclick="javascript:findcode('frmMain','','','','','','','','','','','lstItem','','','','',hidBlockCharge.value,hidChargeLocCode.value);" CausesValidation=False runat=server />
										<asp:label id=lblItemCodeErr text="<br>Please select one Item Code" Visible=False forecolor=red Runat="server" /></td>
					</tr>
					<tr class="mb-c">
						<td height=25>Issue Reference :</td>
						<td><asp:Textbox id=txtItemRef width=50% maxlength=64 runat=server /></td>
					</tr>
					<tr id="RowAcc" class="mb-c">
						<td height=25><asp:label id="lblAccCodeTag" Runat="server"/> </td>
						<td><asp:DropDownList id="lstAccCode" Width=90% AutoPostBack=True OnSelectedIndexChanged=CallCheckVehicleUse runat=server />
							<input type=button value=" ... " id="FindAcc" onclick="javascript:findcode('frmMain','','lstAccCode','9',(hidBlockCharge.value==''? 'lstBlock': 'lstPreBlock'),'','lstVehCode','lstVehExp','','','','','','','','',hidBlockCharge.value,hidChargeLocCode.value);" CausesValidation=False runat=server />
							<asp:label id=lblAccCodeErr Visible=False forecolor=red Runat="server" /></td>
					</tr>
					<tr id="RowChargeLevel" class="mb-c">
						<td height="25">Charge Level :* </td>
						<td><asp:DropDownList id="lstChargeLevel" Width=100% AutoPostBack=True OnSelectedIndexChanged=lstChargeLevel_OnSelectedIndexChanged runat=server /> </td>
					</tr>
					<tr id="RowPreBlk" class="mb-c">
						<td height="25"><asp:label id=lblPreBlkTag Runat="server"/> </td>
						<td><asp:DropDownList id="lstPreBlock" Width=100% runat=server />
							<asp:label id=lblPreBlockErr Visible=False forecolor=red Runat="server" /></td>
					</tr>
					<tr id="RowBlk" class="mb-c" >
						<td height=25><asp:label id=lblBlkTag Runat="server"/></td>
						<td><asp:DropDownList id="lstBlock" Width=100% runat=server />
							<asp:label id=lblBlockErr Visible=False forecolor=red Runat="server" /></td>
					</tr>
					<tr id="RowVeh" class="mb-c">
						<td height=25><asp:label id="lblVehTag" Runat="server"/></td>
						<td><asp:DropDownList id="lstVehCode" Width=100% runat=server />
							<asp:label id=lblVehCodeErr Visible=False forecolor=red Runat="server" /></td>
					</tr>
					<tr id="RowVehExp" class="mb-c">
						<td><asp:label id="lblVehExpTag" Runat="server"/></td>
						<td><asp:DropDownList id="lstVehExp" Width=100% runat=server />
							<asp:label id=lblVehExpCodeErr Visible=False forecolor=red Runat="server" /></td>
					</tr>
					<tr class="mb-c">
						<td height=25>Quantity to Issue :*</td>
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
								ErrorMessage="<br>Please specify Quantity to Issue" 
								ControlToValidate="txtQty" 
								display="dynamic"/>
							<asp:RangeValidator id="Range1"
								ControlToValidate="txtQty"
								MinimumValue="0"
								MaximumValue="999999999999999"
								Type="double"
								EnableClientScript="True"
								Text="<br>The value must be from 1!"
								runat="server" display="dynamic"/>
							<asp:label id=lblerror text="Number generated is too big!" Visible=False forecolor=red Runat="server" />
							<asp:label id=lblStock text="Not enough stock in hand!" Visible=False forecolor=red Runat="server" />
						</td>
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
				<td colspan=6><asp:label id=lblConfirmErr text="<BR>Document must contain transaction to Confirm!" Visible=False forecolor=red Runat="server" />
							  <asp:label id=lblUnDel text="<BR>Insufficient stock in Inventory to perform operation!" Visible=False forecolor=red Runat="server" /></td>
			</tr>			
			<tr>
				<td colspan=6> 
					<asp:DataGrid id="dgStkTx"
						AutoGenerateColumns="false" width="100%" runat="server"
						OnItemDataBound="DataGrid_ItemCreated" 
						GridLines=none
						Cellpadding="2"
						Pagerstyle-Visible="False"
						OnDeleteCommand="DEDR_Delete"
						AllowSorting="True">	
						<HeaderStyle CssClass="mr-h" />							
						<ItemStyle CssClass="mr-l" />
						<AlternatingItemStyle CssClass="mr-r" />						
						<Columns>
						<asp:TemplateColumn HeaderText="Item">
							<ItemStyle Width="15%"/>
							<ItemTemplate>
								<asp:label text=<%# Container.DataItem("StockIssueLnID") %> Visible=false id="LnID" runat="server" />
								<asp:label text=<%# Container.DataItem("ItemCode") %> id="ItemCode" runat="server" />
								( <%# Container.DataItem("Description") %> )					
							</ItemTemplate>
						</asp:TemplateColumn>						
						<asp:TemplateColumn HeaderText="Issue Reference">
							<ItemStyle Width="8%"/>
							<ItemTemplate>
								<asp:label text=<%# Container.DataItem("ItemRef") %> id="ItemRef" runat="server" />
							</ItemTemplate>
						</asp:TemplateColumn>						
						<asp:TemplateColumn>
							<ItemStyle Width="8%"/>
							<ItemTemplate>
								<asp:label text=<%# Container.DataItem("AccCode") %> Visible=false id="AccCode" runat="server" />
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
						<asp:TemplateColumn HeaderText="Quantity Issued">
							<HeaderStyle HorizontalAlign="Right" />			
							<ItemStyle Width="8%" HorizontalAlign="Right" />			
							<ItemTemplate>
								<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("Qty"),5) %> id="lblQtyTrxDisplay" runat="server" />	
								<asp:label text=<%# FormatNumber(Container.DataItem("Qty"),5) %> id="lblQtyTrx" visible = "false" runat="server" />							
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Unit Cost">
							<HeaderStyle HorizontalAlign="Right" />			
							<ItemStyle Width="8%" HorizontalAlign="Right" />							
							<ItemTemplate>
								<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator(Container.DataItem("Cost")) %> id="lblUnitCostDisplay" runat="server" />
								<asp:label text=<%# FormatNumber(Container.DataItem("Cost"),5) %> id="lblUnitCost" visible = "false" runat="server" />
							</ItemTemplate>							
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Amount">
							<HeaderStyle HorizontalAlign="Right" />			
							<ItemStyle Width="8%" HorizontalAlign="Right" />						
							<ItemTemplate>
								<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator(Container.DataItem("Amount")) %> id="lblAmountDisplay" runat="server" />
								<asp:label text=<%# FormatNumber(Container.DataItem("Amount"),2) %> id="lblAmount" visible = "false" runat="server" />
							</ItemTemplate>							
						</asp:TemplateColumn>		
						<asp:TemplateColumn HeaderText="Unit Price">
							<HeaderStyle HorizontalAlign="Right" />			
							<ItemStyle Width="8%" HorizontalAlign="Right" />							
							<ItemTemplate>
								<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator(Container.DataItem("Price")) %> id="lblUnitPriceDisplay" runat="server" />
								<asp:label text=<%# FormatNumber(Container.DataItem("Price"),5) %> id="lblUnitPrice" visible = "false" runat="server" />
							</ItemTemplate>							
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Price Amount">
							<HeaderStyle HorizontalAlign="Right" />			
							<ItemStyle Width="8%" HorizontalAlign="Right" />						
							<ItemTemplate>
								<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator(Container.DataItem("PriceAmount")) %> id="lblPriceAmountDisplay" runat="server" />
								<asp:label text=<%# FormatNumber(Container.DataItem("PriceAmount"),2) %> id="lblPriceAmount" visible = "false" runat="server" />
							</ItemTemplate>							
						</asp:TemplateColumn>		
						<asp:TemplateColumn>		
							<ItemStyle Width="5%" HorizontalAlign="right" />							
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
					<table border=0 width="100%" cellspacing="0" cellpadding="1" runat="server">
						<tr>
							<td width="20%" height="25">&nbsp;</td>
							<td width="15%" align=right>Total Cost : </td>
							<td width="15%" align=right><asp:label id="lblTotAmtFig" runat="server" /></td>
							<td width="20%">&nbsp;</td>
							<td width="15%" align=right>Total Price : </td>
							<td width="15%" align=right><asp:label id="lblTotPriceFig" runat="server" /></td>					
						</tr>
					</table>
				</td>						
			</tr>
			<tr>
				<td colspan=6>&nbsp;</td>				
			</tr>					
			<tr>
				<td heigth=25>Chit Reference Remark :</td>	
				<td colspan="5"><asp:textbox id=txtRefSIS width="20%" wrap=true maxlength="20" runat="server" /></td>
			</tr>
			<tr>
				<td heigth=25>Chit Date Remark :</td>	
				<td colspan="5"><asp:textbox id=txtRefDate width="20%" wrap=true maxlength="10" runat="server" /></td>
			</tr>
			<tr>
				<td heigth=25>Remarks :</td>	
				<td colspan="5"><asp:textbox id="txtRemarks" width="100%" maxlength="256" runat="server" /></td>
			</tr>
			<tr>
				<td colspan=6>&nbsp;</td>
			</tr>
			<tr>
				<td colspan="6">
					<asp:checkboxlist id="cblDisplayCost" runat="server">
						<asp:listitem id=option1 value="Display Unit Price in Canteen Issue Slip." selected runat="server"/>
					</asp:checkboxlist>
				</td>
			</tr>
			<tr>
				<td colspan=6>&nbsp;</td>
			</tr>
			<tr>
				<td align="left" colspan="5">
					<asp:ImageButton id="Save"      AlternateText="Save"       onClick="btnSave_Click"      ImageURL="../../images/butt_save.gif"      CausesValidation=False               runat="server" />
					<asp:ImageButton id="Confirm"   AlternateText="Confirm"    onClick="btnConfirm_Click"   ImageURL="../../images/butt_confirm.gif"   CausesValidation=False               runat="server" />
					<asp:ImageButton id="Cancel"    AlternateText="Cancel"     onClick="btnCancel_Click"    ImageURL="../../images/butt_Cancel.gif"    CausesValidation=False Visible=False runat="server" />
					<asp:ImageButton id="Print"     AlternateText="Print"      onClick="btnPreview_Click"   ImageURL="../../images/butt_print.gif"     CausesValidation=False               runat="server" />
					<asp:ImageButton id="PRDelete"  AlternateText="Delete"     onClick="btnDelete_Click"    ImageURL="../../images/butt_delete.gif"    CausesValidation=False               runat="server" />
					<asp:ImageButton id="DebitNote" AlternateText="Debit Note" onClick="btnDebitNote_Click" ImageURL="../../images/butt_debitnote.gif" CausesValidation=False Visible=False runat="server" />
					<asp:ImageButton id="btnNew"    AlternateText="New"        onClick="btnNew_Click"       ImageURL="../../images/butt_new.gif"       CausesValidation=False               runat="server" />
					<asp:ImageButton id="Back"      AlternateText="Back"       onClick="btnBack_Click"      ImageURL="../../images/butt_back.gif"      CausesValidation=False               runat="server" />
				</td>
			</tr>		
		</table>
			<Input type=hidden id=hidBlockCharge value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>
		</form>
	</body>
</html>
