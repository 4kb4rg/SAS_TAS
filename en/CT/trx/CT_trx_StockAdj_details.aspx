<%@ Page Language="vb" Trace="False" src="../../../include/CT_trx_StockAdj_details.aspx.vb" Inherits="CT_StockAdjust_Det" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuCTTrx" src="../../menu/menu_CTtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Canteen Adjustment Details</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<script language="javascript">
		function clearText(oper) {
			var doc = document.frmMain;
			if (oper == 'Amt')
			{	doc.txtQty.value = '';
				doc.txtCost.value = ''; 
			}
			else if (oper == 'Qty')
				doc.txtAmt.value = '';
		}
	</script>
	<body>
		<form id=frmMain runat=server>
			<asp:label id=lblCode visible=false text=" Code" runat=server />
			<asp:label id=lblSelect visible=false text="Select " runat=server />
			<asp:label id=lblPleaseSelect visible=false text="Please select " runat=server />
			<table border=0 width="100%" cellspacing=0 cellpading=1 runat=server>
				<tr>
					<td colspan=6><UserControl:MenuCTTrx id=menuCT EnableViewState=False runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan=6>CANTEEN ADJUSTMENT DETAILS</td>
				</tr>
				<tr>
					<td colspan=6><hr size=1 noshade></td>
				</tr>
				<tr>
					<td width="20%" height=25>Canteen Adjustment ID :</td>
					<td width="30%"><asp:Label id=lblStockAdjId runat="server"/></td>
					<td width="5%">&nbsp;</td>
					<td width="15%">Period :</td>
					<td width="25%"><asp:Label id=lblPeriod runat=server /></td>
					<td width="5%">&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Adjustment Type :*</td>
					<td><asp:DropDownList id="ddlAdjType" Width="100%" AutoPostBack=True OnSelectedIndexChanged="ddlAdjType_OnSelectedIndexChanged" runat=server /></td>
					<td>&nbsp;</td>
					<td>Status :</td>
					<td><asp:Label id=lblStatus runat=server /><asp:Label id=lblHidStatus Visible=False runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Transaction Type :*</td>
					<td><asp:DropDownList id="ddlTransType" Width="100%" AutoPostBack=True OnSelectedIndexChanged="ddlTransType_OnSelectedIndexChanged" runat=server /></td>
					<td>&nbsp;</td>
					<td>Date Created :</td>
					<td><asp:Label id=lblCreateDate runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Remarks :</td>
					<td><asp:textbox id=txtRemark width="100%" MaxLength=512 Runat="server" /></td>
					<td>&nbsp;</td>
					<td>Last Update :</td>
					<td><asp:Label id=lblUpdateDate runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>Updated By :</td>
					<td><asp:Label id=lblUpdateID  runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=6>
						<table id="tblAdd" border=0 width="100%" cellspacing="0" cellpadding="4" runat="server">
							<tr id="trItemCode" class="mb-c">
								<td width="20%" height=25>Item Code :*</td>
								<td width="80%" colspan=4><asp:DropDownList id="ddlItemCode" Width=90% AutoPostBack=True OnSelectedIndexChanged="ddlItemCode_OnSelectedIndexChanged" runat=server />
												<input type=button value=" ... " id="btnFindItemCode" onclick="javascript:findcode('frmMain','','','','','','','','','4','ddlItemCode','','','','','',hidBlockCharge.value,hidChargeLocCode.value);" CausesValidation=False runat=server />
												<br><asp:label id=lblItemCodeErr Text="Please select an Item code." Visible=False forecolor=red Runat="server" />
								</td>
							</tr>
							<tr id="trAccCode" class="mb-c" >
								<td width="20%" height=25><asp:label id="lblAccCodeTag" Runat="server" /> :*</td>
								<td width="80%" colspan=4><asp:DropDownList id="ddlAccCode" Width=90% AutoPostBack=True OnSelectedIndexChanged="ddlAccCode_OnSelectedIndexChanged" runat=server />
												<input type=button value=" ... " id="btnFindAccCode" onclick="javascript:findcode('frmMain','','ddlAccCode','9',(hidBlockCharge.value==''? 'ddlBlkCode': 'ddlPreBlkCode'),'','ddlVehCode','ddlVehExpCode','','','','','','','','',hidBlockCharge.value,hidChargeLocCode.value);" CausesValidation=False runat=server />
												<br><asp:label id=lblAccCodeErr Visible=False forecolor=red Runat="server" /></td>
							</tr>
							<tr id="trChargeLevel" class="mb-c" >
								<td width="20%" height=25>Charge To :*</td>
								<td width="80%" colspan=4><asp:DropDownList id="ddlChargeLevel" Width=100% AutoPostBack=True OnSelectedIndexChanged=ddlChargeLevel_OnSelectedIndexChanged runat=server /> </td>
							</tr>
							<tr id="trPreBlkCode" class="mb-c" >
								<td width="20%" height=25><asp:label id=lblPreBlkCodeTag Runat="server" /> :</td>
								<td width="80%" colspan=4><asp:DropDownList id="ddlPreBlkCode" Width=100% runat=server />
									<asp:label id=lblPreBlkCodeErr Visible=False forecolor=red Runat="server" /></td>
							</tr>
							<tr id="trBlkCode" class="mb-c" >
								<td width="20%" height=25><asp:label id=lblBlkCodeTag Runat="server" /> :</td>
								<td width="80%" colspan=4><asp:DropDownList id="ddlBlkCode" Width=100% runat=server />
									<asp:label id=lblBlkCodeErr Visible=False forecolor=red Runat="server" /></td>
							</tr>
							<tr id="trVehCode" class="mb-c" >
								<td width="20%" height=25><asp:label id="lblVehCodeTag" Runat="server" /> :</td>
								<td width="80%" colspan=4><asp:DropDownList id="ddlVehCode" Width=100% runat=server />
									<asp:label id=lblVehCodeErr Visible=False forecolor=red Runat="server" /></td>
							</tr>
							<tr id="trVehExpCode" class="mb-c" >
								<td width="20%" height=25><asp:label id="lblVehExpCodeTag" Runat="server" /> :</td>
								<td width="80%" colspan=4><asp:DropDownList id="ddlVehExpCode" Width=100% runat=server />
									<asp:label id=lblVehExpCodeErr Visible=False forecolor=red Runat="server" /></td>
							</tr>
							<tr class="mb-c">
								<td width="20%" height=25>Document Ref :</td>
								<td width="30%"><asp:textbox id="txtAdjDocRef" width=100% maxlength=32 EnableViewState="False" Runat="server" /></td>
								<td width="50%" colspan=3>&nbsp;</td>
							</tr>
							<tr class="mb-c">
								<td width="20%" height=25><asp:label id="lblQuantityTag" Text = "Quantity :*" Runat="server" />&nbsp;</td>
								<td width="30%" id="tdQty"><asp:TextBox id="txtQty" width="100%" maxlength=21 Runat="server" />
												<asp:RegularExpressionValidator id="revQty" 
													ControlToValidate="txtQty"
													ValidationExpression="^[-]?\d{1,15}\.\d{1,5}|^[-]?\d{1,15}"
													Display="Dynamic"
													text = "Maximum length 15 digits and 5 decimal points"
													runat="server"/>
												<asp:label id=lblQtyErr text="Please enter numeric value only!" Visible=False forecolor=red Runat="server" />
								</td>
								<td width="5%">&nbsp;</td>
								<td width="30%">Original Quantity : <asp:Label id=lblOriginalQtyDisplay runat="server" /><asp:Label id=lblOriginalQty Visible=False runat="server" />
                                    <asp:Label id=lblOriginalQtyOnHand Visible=False runat="server" /><asp:Label id=lblOriginalQtyOnHold Visible=False runat="server" />
                                </td>
								<td width="15%">&nbsp;</td>
							</tr>
							<tr class="mb-c">
								<td width="20%" height=25><asp:label id="lblAverageCostTag" Text = "Average Cost :*" Runat="server" />&nbsp;</td>
								<td width="30%" id="tdAverageCost"><asp:TextBox id="txtAverageCost" width="100%" maxlength=21 Runat="server" />
												<asp:RegularExpressionValidator id="revAverageCost" 
													ControlToValidate="txtAverageCost"
													ValidationExpression="^[-]?\d{1,15}\.\d{1,5}|^[-]?\d{1,15}"
													Display="Dynamic"
													text = "Maximum length 15 digits and 5 decimal points"
													runat="server"/>
												<asp:label id=lblAverageCostErr text="Please enter numeric value only!" Visible=False forecolor=red Runat="server" />
												<asp:label id=lblActionDesc1 Visible=True Runat="server" />
								</td>
								<td width="5%">&nbsp;</td>
								<td width="30%">Original Average Cost : <asp:Label id=lblOriginalAverageCostDisplay runat="server" /><asp:Label id=lblOriginalAverageCost visible=False runat="server" /></td>
								<td width="15%">&nbsp;</td>
							</tr>
							<tr class="mb-c">
								<td width="20%" height=25>&nbsp;</td>
								<td width="30%" id="tdDiffAverageCost"><asp:label id=lblActionDesc2 Visible=True Runat="server" />&nbsp;</td>
								<td width="5%">&nbsp;</td>
								<td width="30%">Original Difference Average Cost : <asp:Label id=lblOriginalDiffAverageCostDisplay runat="server" /><asp:Label id=lblOriginalDiffAverageCost visible=False runat="server" /></td>
								<td width="15%">&nbsp;</td>
							</tr>
							<tr class="mb-c">
								<td width="20%" height=25><asp:label id="lblTotalCostTag" Text = "Total Cost :*" Runat="server" />&nbsp;</td>
								<td width="30%" id="tdTotalCost"><asp:textbox id="txtTotalCost" width="100%" maxlength=18 Runat="server" />
												<asp:RegularExpressionValidator id="revTotalCost" 
													ControlToValidate="txtTotalCost"
													ValidationExpression="^[-]?\d{1,15}\.\d{1,2}|^[-]?\d{1,15}"
													Display="Dynamic"
													text = "Maximum length 15 digits and 2 decimal points"
													runat="server"/>
												<asp:label id=lblTotalCostErr text="Please enter numeric value only!" Visible=False forecolor=red Runat="server" />
								</td>
								<td width="5%">&nbsp;</td>
								<td width="30%">Original Total Cost : <asp:Label id=lblOriginalTotalCostDisplay runat="server" /><asp:Label id=lblOriginalTotalCost Visible=False runat="server" /></td>
								<td width="15%">&nbsp;</td>
							</tr>
							<tr class="mb-c">
								<td width="20%" height=25>&nbsp;</td>
								<td width="30%" id="tdActionDesc"><asp:label id=lblActionDesc3 Visible=True Runat="server" /></td>
								<td width="5%">&nbsp;</td>
								<td width="30%">&nbsp;</td>
								<td width="15%">&nbsp;</td>
							</tr>
							<tr class="mb-c">
								<td colspan=5><asp:ImageButton text="Add" id="ibAdd" ImageURL="../../images/butt_add.gif" OnClick="ibAdd_OnClick" Runat="server" /></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>
				<tr id="trDataGrid1">
					<td colspan=6> 
						<asp:DataGrid id="dgLines"
							OnItemDataBound="dgLines_OnItemDataBound"
							AutoGenerateColumns="false" width="100%" runat="server"
							GridLines = none
							Cellpadding = "2"
							Pagerstyle-Visible="False"
							OnDeleteCommand="dgLines_OnDeleteCommand"
							AllowSorting="True">
							<HeaderStyle HorizontalAlign="Left" CssClass="mr-h" />
							<ItemStyle CssClass="mr-l" />
							<AlternatingItemStyle CssClass="mr-r" />
							<Columns>
								<asp:TemplateColumn HeaderText="Item Code">
									<ItemStyle Width="15%"/>
									<ItemTemplate>
										  <%# Container.DataItem("ItemDesc") %>  (<%# Container.DataItem("ItemDesc") %>) 
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemStyle Width="9%"/>
									<ItemTemplate>
										<%# Container.DataItem("AccCode") %> 
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemStyle Width="9%"/>
									<ItemTemplate>
										<%# Container.DataItem("BlkCode") %> 
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemStyle Width="9%"/>
									<ItemTemplate>
										<%# Container.DataItem("VehCode") %> 
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemStyle Width="9%"/>
									<ItemTemplate>
										<%# Container.DataItem("VehExpCode") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Document Ref">
									<ItemStyle Width="9%"/>
									<ItemTemplate>
										<%# Container.DataItem("AdjRefNo") %> 
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Original Quantity">
									<HeaderStyle HorizontalAlign="Right" />
									<ItemStyle Width="9%" HorizontalAlign="Right" />
									<ItemTemplate>
										<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("Quantity"), 5) %> id="lblQuantity" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Original Average Cost">
									<HeaderStyle HorizontalAlign="Right" />
									<ItemStyle Width="9%" HorizontalAlign="Right" />
									<ItemTemplate>
										<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator(System.Math.Round(Container.DataItem("AverageCost"),0)) %> id="lblAverageCost" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Original Total Cost">
									<HeaderStyle HorizontalAlign="Right" />
									<ItemStyle Width="9%" HorizontalAlign="Right" />
									<ItemTemplate>
										<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator(System.Math.Round(Container.DataItem("TotalCost"),0)) %> id="lblTotalCost" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Entered Quantity">
									<HeaderStyle HorizontalAlign="Right" />
									<ItemStyle Width="9%" HorizontalAlign="Right" />
									<ItemTemplate>
										<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("N_Quantity"), 5) %> id="lblN_Quantity" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Entered Average Cost">
									<HeaderStyle HorizontalAlign="Right" />
									<ItemStyle Width="9%" HorizontalAlign="Right" />
									<ItemTemplate>
										<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator(System.Math.Round(Container.DataItem("N_AverageCost"),0)) %> id="lblN_AverageCost" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Entered Total Cost">
									<HeaderStyle HorizontalAlign="Right" />
									<ItemStyle Width="9%" HorizontalAlign="Right" />
									<ItemTemplate>
										<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator(System.Math.Round(Container.DataItem("N_TotalCost"),0)) %> id="lblN_TotalCost" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Entered Quantity">
									<HeaderStyle HorizontalAlign="Right" />
									<ItemStyle Width="9%" HorizontalAlign="Right" />
									<ItemTemplate>
										<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator(System.Math.Round(Container.DataItem("D_Quantity"),0)) %> id="lblD_Quantity" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Entered Average Cost">
									<HeaderStyle HorizontalAlign="Right" />
									<ItemStyle Width="9%" HorizontalAlign="Right" />
									<ItemTemplate>
										<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator(System.Math.Round(Container.DataItem("D_AverageCost"),0)) %> id="lblD_AverageCost" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Entered Total Cost">
									<HeaderStyle HorizontalAlign="Right" />
									<ItemStyle Width="9%" HorizontalAlign="Right" />
									<ItemTemplate>
										<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator(System.Math.Round(Container.DataItem("D_TotalCost"),0)) %> id="lblD_TotalCost" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemStyle HorizontalAlign="right" Width="5%"/>
									<ItemTemplate>
										<asp:label text=<%# Trim(Container.DataItem("StockAdjLnID")) %> id="lblStockAdjLNID" visible=false runat="server" />
										<asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" CausesValidation=False runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:DataGrid>
					</td>
				</tr>
				<tr id="trDataGrid2">
					<td colspan=3>&nbsp;</td>
					<td colspan=3 height=25><hr size="1" noshade></td>
				</tr>
				<tr id="trDataGrid3">
					<td colspan=3>&nbsp;</td>
					<td colspan=3>
						<table border=0 width="100%" runat=server>
							<tr>
								<td height="25" align="left">Total : </td>
								<td width="25%" align="right"><asp:label id="lblTotal1" runat="server" /></td>
								<td width="25%" align="right"><asp:label id="lblTotal2" runat="server" /></td>
								<td id="tdDummy" width="12%">&nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan=6><asp:label id=lblActionResult Visible=False forecolor=red Runat="server" />&nbsp;</td>
				</tr>
				<tr>
					<td align="left" colspan="6">
						<asp:ImageButton id="ibSave"     AlternateText="Save"    onClick="ibSave_OnClick"    ImageURL="../../images/butt_save.gif"    CausesValidation=False runat="server" />
						<asp:ImageButton id="ibConfirm"  AlternateText="Confirm" onClick="ibConfirm_OnClick" ImageURL="../../images/butt_confirm.gif" CausesValidation=False runat="server" />
						<asp:ImageButton id="ibDelete"   AlternateText="Delete"  onClick="ibDelete_OnClick"  ImageURL="../../images/butt_delete.gif"  CausesValidation=False runat="server" />
						<asp:ImageButton id="ibNew"      AlternateText="New"     onClick="ibNew_OnClick"     ImageURL="../../images/butt_new.gif"     CausesValidation=False runat="server" />
						<asp:ImageButton id="ibBack"     AlternateText="Back"    onClick="ibBack_OnClick"    ImageURL="../../images/butt_back.gif"    CausesValidation=False runat="server" />
					</td>
				</tr>
			</table>
			<Input type=hidden id=hidBlockCharge value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>
			<Input type=hidden id=Real_DDL_Search value=",ddlItemCode," runat=server/>
		</form>
	</body>
</html>
