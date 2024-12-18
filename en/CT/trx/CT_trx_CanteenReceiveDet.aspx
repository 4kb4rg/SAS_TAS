<%@ Page Language="vb" Trace="False" src="../../../include/CT_Trx_CanteenReceiveDet.aspx.vb" Inherits="CT_CanteenReceive" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuCTTrx" src="../../menu/menu_CTtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<html>
	<head>
		<title>Canteen Receive Details</title>		
	</head>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<script language="javascript">
			function calAmount(i) {
				var doc = document.frmMain;
				var a = parseFloat(doc.txtQty.value);
				var b = parseFloat(doc.txtCost.value);
				var c = parseFloat(doc.txtAmount.value);

				if (b.toString().indexOf(".") != -1 || c.toString().indexOf(".") != -1)
					return;
				
				if ((i == '1') || (i == '2')) {
					if ((doc.txtQty.value != '') && (doc.txtCost.value != ''))
						doc.txtAmount.value = Math.round(a * b, 0);
					else
						doc.txtAmount.value = '';
				}
				
				if (i == '3') {
					if ((doc.txtQty.value != '') && (doc.txtAmount.value != ''))
						doc.txtCost.value = Math.round(c / a, 0);
					else
						doc.txtCost.value = '';
				}

				if (doc.txtQty.value == 'NaN')
					doc.txtQty.value = '';

				if (doc.txtCost.value == 'NaN')
					doc.txtCost.value = '';
					
				if (doc.txtAmount.value == 'NaN')
					doc.txtAmount.value = '';
		}
		</script>		
	<body>
		<form id=frmMain runat=server>
   		<asp:Label id=lblErrMessage visible=false text="Error while initiating component." runat=server />
		<asp:label id=lblCode visible=false text=" Code" runat=server />
		<asp:label id=lblSelect visible=false text="Select " runat=server />
		<asp:label id=lblPleaseSelect visible=false text="Please select " runat=server />	
     	<asp:label id="lblStatusHid" Visible="False" Runat="server"></asp:label>	
		<table border=0 width="100%" cellspacing="0" cellpadding="1" >
			<tr>
				<td colspan=6><UserControl:MenuCTTrx enableviewstate=false id=menuCT runat="server" /></td>
			</tr>			
			<tr>
				<td class="mt-h" colspan=6>CANTEEN RECEIVE DETAILS</td>
			</tr>
			<tr>
				<td colspan=6><hr size="1" noshade></td>
			</tr>			
	
			<tr>
				<td width="20%" height=25>Canteen Receive ID :</td>
				<td width="30%"><asp:Label id=lblCanteenTxID runat="server"/></td>
				<td width="5%">&nbsp;</td>
				<td width="15%">Status :</td>
				<td width="25%"><asp:Label id=Status runat="server"/></td>
				<td width="5%">&nbsp;</td>
			</tr>
			<tr>
				<td height=25>Canteen Receive Doc. Type :*</td>
				<td><asp:DropDownList id=lstRecDoc width=100% runat=server autopostback=true/></td>
				<td>&nbsp;</td>
				<td>Date Created :</td>
				<td><asp:Label id=CreateDate runat="server"/></td>
				<td>&nbsp;</td>
			</tr>			
			<tr>
				<td height=25>Canteen Receive Ref. No. :*</td>
				<td><asp:TextBox id=txtRefNo width="100%" maxlength=32 runat="server"/>
					<asp:RequiredFieldValidator id="validateRefNo" runat="server" 
						ErrorMessage="Please specify document reference number!" 
						ControlToValidate="txtRefNo" 
						EnableClientScript="True"
						Display="dynamic"/></td>
				<td>&nbsp;</td>
				<td>Last Update :</td>
				<td><asp:Label id=UpdateDate runat=server /></td>		
				<td>&nbsp;</td>
			</tr>			
			<tr>
				<td height=25>Canteen Receive Ref. Date :*</td>
				<td valign=top height=26><asp:TextBox id=txtDate width=50% maxlength=10 runat=server /> 
				 <a href="javascript:PopCal('txtDate');"><asp:Image id="btnSelDate" runat="server" causesvalidation=false ImageUrl="../../images/calendar.gif"/></a>
					<asp:RequiredFieldValidator id="validateDate" runat="server" 
						ErrorMessage="<br>Please specify document date!" 
						EnableClientScript="True"
						ControlToValidate="txtDate" 
						Display="dynamic"/>
					<asp:Label id=lblDate text="<br>Date entered should be in the format " display=dynamic forecolor=red visible=false runat="server"/> 
					<asp:Label id=lblFmt display=dynamic forecolor=red visible=false runat="server"/> 
				</td>
				<td>&nbsp;</td>
				<td>Updated By :</td>
				<td><asp:Label id=UpdateBy runat=server /></td>				
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td colspan=6>&nbsp;</td>		
			</tr>
			<tr>
				<td colspan=6>
					<table id="tblPR" border=0 width="100%" cellspacing="0" cellpadding="4" runat="server" visible=False>
 						<tr class="mb-c" >
							<td width="20%" height=25>Purchase Requisition ID :</td>
							<td width="80%"><asp:DropDownList id="lstPR" width=50% AutoPostBack=True OnSelectedIndexChanged=RebindItemList runat="server"/></td>
						</tr>
					</table>
					<table id="tblAdd" border=0 width="100%" cellspacing="0" cellpadding="4" runat="server">
						<tr class="mb-c">
							<td width="20%" height=25>Item Code :*</td>
							<td width="80%"><asp:DropDownList id="lstItem" Width=90% runat=server EnableViewState=True />
			    				<input type=button value=" ... " id="FindCT" onclick="javascript:findcode('frmMain','1','','','','','','','','6','','lstItem','','','','',hidBlockCharge.value,hidChargeLocCode.value);" CausesValidation=False runat=server />
								<asp:label id=lblItemCodeErr text="<br>Please select one Item" Visible=False forecolor=red Runat="server" /></td>
						</tr>												
						<tr id="RowAcc" class="mb-c" >
							<td width="20%" height=25><asp:label id="lblAccTag" Runat="server"/> :* </td>
							<td width="80%"><asp:DropDownList id="lstAccCode" Width=90% AutoPostBack=True OnSelectedIndexChanged=CallCheckVehicleUse runat=server />
								<input type=button value=" ... " id="Find" onclick="javascript:findcode('frmMain','','lstAccCode','9',(hidBlockCharge.value==''? 'lstBlock': 'lstPreBlock'),'','lstVehCode','lstVehExp','','','','','','','','',hidBlockCharge.value,hidChargeLocCode.value);" CausesValidation=False runat=server />
								<asp:label id=lblAccCodeErr forecolor=red Runat="server" /></td>
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
							<td height=25><asp:label id=lblBlkTag Runat="server"/> : </td>
							<td><asp:DropDownList id="lstBlock" Width=100% runat=server />
								<asp:label id=lblBlockErr Visible=False forecolor=red Runat="server" /></td>
						</tr>
						<tr id="RowVeh" class="mb-c" >
							<td height=25><asp:label id="lblVehTag" Runat="server"/> : </td>
							<td><asp:DropDownList id="lstVehCode" Width=100% runat=server />
								<asp:label id=lblVehCodeErr forecolor=red Runat="server" /></td>
						</tr>
						<tr id="RowVehExp" class="mb-c" >
							<td height=25><asp:label id="lblVehExpTag" Runat="server"/> : </td>
							<td><asp:DropDownList id="lstVehExp" Width=100% runat=server />
								<asp:label id=lblVehExpCodeErr Visible=False forecolor=red Runat="server" /></td>
						</tr>
						<tr id="RowFromLoc" class="mb-c">
							<td height=25>Transfer From :* </td>
							<td><asp:DropDownList id="lstFromLoc" Width=100% runat=server />
								<asp:RequiredFieldValidator id=rfvFromLocCode display=Dynamic runat=server ControlToValidate=lstFromLoc /></td>
						</tr>															
						<tr class="mb-c">
							<td height=25>Quantity Received :*</td>
							<td><asp:textbox id="txtQty" Width=50% maxlength=15 OnKeyUp="javascript:calAmount('1');" EnableViewState="False" Runat="server" />
								<asp:RegularExpressionValidator id="RegularExpressionValidatorQtyReq" 
									ControlToValidate="txtQty"
									ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"
									Display="Dynamic"
									text = "<br>Maximum length 9 digits and 5 decimal points"
									runat="server"/>
								<asp:RangeValidator id="Range1"
									ControlToValidate="txtQty"
									MinimumValue="0.00001"
									MaximumValue="999999999999999.99999"
									Type="double"
									EnableClientScript="True"
									Text="<br>The value is out of acceptable range !"
									runat="server" display="dynamic"/>
									<asp:label id=lblErrQty text="<br>Quantity received cannot be blank!" Visible=False forecolor=red Runat="server" />																	
							</td>
						</tr>
						<tr class="mb-c">
							<td height=25>Unit Cost :*</td>
							<td><asp:textbox id="txtCost" Width=50% maxlength=19 OnKeyUp="javascript:calAmount('2');" EnableViewState="False" Runat="server" />
								<asp:RegularExpressionValidator id="RegExpCost" 
									ControlToValidate="txtCost"
									ValidationExpression="\d{1,19}"
									Display="Dynamic"
									text = "<br>Maximum length 19 digits and 0 decimal points"
									runat="server"/>
								<asp:RangeValidator id="RangsCost"
									ControlToValidate="txtCost"
									MinimumValue="0"
									MaximumValue="999999999999999"
									Type="double"
									EnableClientScript="True"
									Text="<br>The value is out of acceptable range !"
									runat="server" display="dynamic"/>
									<asp:label id=lblErrUnitCost text="<br>Unit Cost cannot be blank!" Visible=False forecolor=red Runat="server" />																	
							</td>
						</tr>
						<tr class="mb-c">
							<td height=25>Total Amount :*</td>
							<td><asp:textbox id="txtAmount" Width=50% maxlength=19 OnKeyUp="javascript:calAmount('3');" EnableViewState="False" Runat="server" />
								<asp:RegularExpressionValidator id="RegExpamt" 
									ControlToValidate="txtAmount"
									ValidationExpression="\d{1,19}"
									Display="Dynamic"
									text = "<br>Maximum length 19 digits and 0 decimal points"
									runat="server"/>
								<asp:RangeValidator id="RangeAmt"
									ControlToValidate="txtAmount"
									MinimumValue="0"
									MaximumValue="999999999999999"
									Type="double"
									EnableClientScript="True"
									Text="<br>The value is out of acceptable range !"
									runat="server" display="dynamic"/>
									<asp:label id=lblErrTotalAmt text="Total amount cannot be blank!" Visible=False forecolor=red Runat="server" />																										
							</td>
						</tr>
						<tr class="mb-c">
							<td height=25>&nbsp;</td>
							<td><asp:label id=lblerror text="Number generated is too big!" Visible=False forecolor=red Runat="server" />
								<asp:label id=lblStock text="Quantity oustanding in PR does not tally!" Visible=False forecolor=red Runat="server" />
								<asp:label id=lblQty text="Quantity and Cost cannot be empty!" Visible=False forecolor=red Runat="server" />
								<asp:label id=lblPR text="Item not found in selected PR!" Visible=False forecolor=red Runat="server" /></td>
						</tr>
						<tr class="mb-c">
							<td height=25 colspan=2><asp:ImageButton text="Add" id="Add" ImageURL="../../images/butt_add.gif" OnClick="btnAdd_Click" Runat="server" /></td>
						</tr>
					</table>
				</td>		
			</tr>
			<tr>
				<td colspan=6>&nbsp;</td>				
			</tr>		
			<tr>
				<td colspan=6><asp:label id=lblConfirmErr text="Document must contain transaction to Confirm !" Visible=False forecolor=red Runat="server" />
							  <asp:Label id=lblTxError visible=false Text="Cannot perform operation, please check data !" forecolor=red runat=server />			
							  <asp:label id=lblUnDel text="Insufficient stock in Inventory to perform operation!" Visible=False forecolor=red Runat="server" /></td>	
			</tr>
			<tr>
				<td colspan=6> 
					<asp:DataGrid id="dgCanteenTx"
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
							<asp:TemplateColumn HeaderText="PR ID">
								<ItemStyle Width="10%"/> 																																
								<ItemTemplate>
									<asp:label text=<%# Trim(Container.DataItem("DocID")) %>  id="DocID" runat="server" />
									<asp:label text=<%# Container.DataItem("CanteenRcvLnID") %> id="RecvLnID" visible=false runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
						
							<asp:TemplateColumn HeaderText="Item">
								<ItemStyle Width="16%"/> 																																
								<ItemTemplate>
									<asp:label text=<%# Container.DataItem("ItemCode") %> id="ItemCode" visible=false runat="server" />
									<asp:label text=<%# Container.DataItem("Description") %> id="ItemDesc" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn>
								<ItemStyle Width="10%"/> 																																
								<ItemTemplate>
									<%# Container.DataItem("AccCode") %>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn>
								<ItemStyle Width="10%"/> 																																
								<ItemTemplate>
									<%# Container.DataItem("BlkCode") %>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn>
								<ItemStyle Width="10%"/> 																																
								<ItemTemplate>
									<%# Container.DataItem("VehCode") %>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn>
								<ItemStyle Width="10%"/> 																																
								<ItemTemplate>
									<%# Container.DataItem("VehExpCode") %>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn>
								<ItemStyle Width="5%"/> 																																
								<ItemTemplate>
									<%# Container.DataItem("FromLocCode") %>
								</ItemTemplate>
							</asp:TemplateColumn>							
							<asp:TemplateColumn HeaderText="Quantity Received">
								<HeaderStyle HorizontalAlign="Right" />			
								<ItemStyle Width="8%" HorizontalAlign="Right" />			
								<ItemTemplate>
									<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("Qty"),5) %> id="lblQtyTrx" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Unit Cost">
								<HeaderStyle HorizontalAlign="Right" />			
								<ItemStyle Width="8%" HorizontalAlign="Right" />							
								<ItemTemplate>
									<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator(System.Math.Round(Container.DataItem("Cost"),0)) %> id="lblUnitCost" runat="server" />
								</ItemTemplate>							
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Amount">
								<HeaderStyle HorizontalAlign="Right" />			
								<ItemStyle Width="8%" HorizontalAlign="Right" />						
								<ItemTemplate>
									<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator(System.Math.Round(Container.DataItem("Amount"),0)) %> id="lblAmount" runat="server" />
								</ItemTemplate>							
							</asp:TemplateColumn>		
								<asp:TemplateColumn>		
								<ItemStyle width=5% HorizontalAlign="Right" />							
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
				<td colspan=6>&nbsp;</td>				
			</tr>
			<tr>
				<td>Remarks :</td>	
				<td colspan="5"><asp:textbox id="txtRemarks" width="100%" maxlength="128" runat="server" /></td>
			</tr>
			<tr>
				<td colspan=6>&nbsp;</td>				
			</tr>
			<tr>
				<td align="left" colspan="6">
					<asp:ImageButton id="Save"     AlternateText="Save"    onClick="btnSave_Click"    ImageURL="../../images/butt_save.gif"                           runat="server" />
					<asp:ImageButton id="Confirm"  AlternateText="Confirm" onClick="btnConfirm_Click" ImageURL="../../images/butt_confirm.gif" CausesValidation=False runat="server" />
					<asp:ImageButton id="PRDelete" AlternateText="Delete"  onClick="btnDelete_Click"  ImageURL="../../images/butt_delete.gif"  CausesValidation=False runat="server" />
					<asp:ImageButton id="btnNew"   AlternateText="New"     onClick="btnNew_Click"     ImageURL="../../images/butt_new.gif"     CausesValidation=False runat="server" />
					<asp:ImageButton id="Back"     AlternateText="Back"    onClick="btnBack_Click"    ImageURL="../../images/butt_back.gif"    CausesValidation=False runat="server" />
				</td>
			</tr>		
			</table>
			<Input type=hidden id=hidBlockCharge value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>
		</form>
	</body>
</html>
