<%@ Page Language="vb" src="../../../include/PU_trx_PODet.aspx.vb" Inherits="PU_PODet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPU" src="../../menu/menu_putrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>


<html>
	<head>
		<title>Purchase Order Details</title>		
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<form id=frmPODet runat=server>
			<asp:Label id=lblHidStatus visible=false runat=server />
			<asp:Label id=lblErrMessage visible=False Text="Error while initiating component." runat=server />
			<asp:label id=lblSelectListLoc visible=false text="Please Select Purchase Requisition Ref " runat="server"/>
			<asp:label id=lblSelectListItem visible=false text="Please Select " runat="server" />
			<asp:label id=lblPR visible=false text="PR " runat="server" />
			<asp:label id=lblPRRef visible=false text="PR Ref. " runat="server" />
			<input type=hidden id=hidOrgQtyOrder runat=server />			 	
			<table border="0" cellspacing="0" cellpadding="1" width="100%">
				<tr>
					<td colspan="6"><UserControl:MenuPU id=menuPU runat="server" /></td>
				</tr>			
				<tr>
					<td class="mt-h" colspan="6">PURCHASE ORDER DETAILS</td>
				</tr>
				<tr>
					<td colspan="6"><hr size="1" noshade></td>
				</tr>
				<tr>
					<td width="20%" height="25">Purchase Order ID :</td>
					<td width="30%"><asp:TextBox id=txtPOID width=100% maxlength=20 runat=server />
					                <asp:Label id=lblPOID text="Please insert PO Number" forecolor=red visible=false runat=server />
					                <asp:RequiredFieldValidator id="revPOID" 
													runat="server" 
													ErrorMessage="Field cannot be blank" 
													ControlToValidate="lblPOID" 
													display="dynamic"/>	
					</td>
					<td width="5%">&nbsp;</td>
					<td width="15%">Period :</td>
					<td width="25%"><asp:Label id=lblAccPeriod runat=server />&nbsp;</td>
					<td width="5%">&nbsp;</td>
				</tr>
				<tr>
					<td height="25">Purchase Order Type :</td>
					<td><asp:label id=lblPOTypeName runat=server />
						<asp:label id=lblPOType visible=false runat=server /></td>
					<td>&nbsp;</td>
					<td>Status :</td>
					<td><asp:Label id=lblStatus runat=server /></td>
					<td width="5%">&nbsp;</td>
				</tr>	
				<tr>
					<td height="25">Dept Code :*</td>
					<td><asp:DropDownList id="ddlDeptCode" width=100% runat=server />
						<asp:Label id=lblDeptCode text="Please Select Dept Code" forecolor=red visible=false runat=server />
						<asp:RequiredFieldValidator id="validateDeptCode" 
													runat="server" 
													ErrorMessage="Please Specify Dept Code" 
													ControlToValidate="ddlDeptCode" 
													display="dynamic"/>	
					</td>								
					<td>&nbsp;</td>
					<td>Date Created :</td>
					<td><asp:Label id=lblCreateDate runat=server /></td>
					<td width="5%">&nbsp;</td>
				</tr>			
				<tr>
					<td height="25">Supplier Code :*</td>
					<td><asp:DropDownList id=ddlSuppCode width=100% runat=server />
						<asp:Label id=lblSuppCode text="Please Select Supplier Code" forecolor=red visible=false runat=server />
						<asp:RequiredFieldValidator id="validateSuppCode" 
													runat="server" 
													ErrorMessage="Please Specify Supplier Code" 
													ControlToValidate="ddlSuppCode" 
													display="dynamic"/></td>	
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td width="5%">&nbsp;</td>
				</tr>	
				<tr>
					<td height="25">PO Issued Location :*</td>
					<td><asp:DropDownList id="ddlPOIssued" width=100% runat=server /> 
						<asp:Label id=lblPOIssued text="Please Select PO Issued Location" forecolor=red visible=false runat=server />
						<asp:RequiredFieldValidator id="validatePOIssued" 
													runat="server" 
													ErrorMessage="Please Specify PO Issued Location" 
													ControlToValidate="ddlPOIssued" 
													display="dynamic"/></td>	
					<td>&nbsp;</td>
					<td>Updated By :</td>
					<td><asp:Label id=lblUpdateBy runat=server /></td>				
					<td width="5%">&nbsp;</td>
				</tr>			
				<tr>
					<td height="25">Centralized :</td>
					<td><asp:CheckBox id="chkCentralized" Text="  Yes" checked=true AutoPostBack=true OnCheckedChanged=Centralized_Type runat=server /></td>
					<td>&nbsp;</td>
					<td>Last Update :</td>
					<td><asp:Label id=lblUpdateDate runat=server /></td>		
					<td width="5%">&nbsp;</td>
				</tr>			
				<tr>
					<td height="25">&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>				
					<td width="5%">&nbsp;</td>
				</tr>			
				<tr>
					<td height="25">&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>Print Date :</td>
					<td><asp:Label id=lblPrintDate runat=server />&nbsp;</td>		
					<td width="5%">&nbsp;</td>
				</tr>
				<tr>
					<td colspan=5>&nbsp;</td>				
					<td width="4%">&nbsp;</td>
				</tr>								
				<tr>
					<td colspan="6">
						<table id=tblPOLine width="100%" class="mb-c" cellspacing="0" cellpadding="4" border="0" align="center" runat=server >
							<tr>						
								<td>
									<table border="0" width="100%" cellspacing="0" cellpadding="1">
										<tr>
											<td width="20%" height="25">&nbsp;</td>
											<td width="80%"><asp:Label id=lblErrManySelectDoc forecolor=black visible=true text="Note : Please select Purchase Requisition ID to add the line items or enter Purchase Requisition Ref. Information" runat=server/></td>
										</tr>	
										<tr id=Centralized_Yes runat="server">
											<td height="25">Purchase Requisition ID :</td>
											<td ><asp:DropDownList id=ddlPRId width=100% runat=server AutoPostBack="True" onSelectedIndexChanged="PRIndexChanged"/></td>
										</tr>
										<tr id=Centralized_No Visible = false runat="server">
											<td height="25">Purchase Requisition ID :*</td>
											<td ><asp:TextBox id=txtPRID width=50% maxlength=20 runat=server /></td>
										</tr>
										<tr>
											<td height="25">Purchase Requisition <asp:label id="lblLocCode" runat="server" /> :</td>
											<td ><asp:Label id=lblPRLocCode runat=server /></td>
										</tr>
										<tr>
											<td height="25">Purchase Requisition Ref. No. :</td>
											<td ><asp:TextBox id=txtPRRefId width=50% maxlength=20 runat=server /></td>
										</tr>
										<tr>
											<td height="25">Purchase Requisition Ref. <asp:label id="lblLocation" runat="server" /> :</td>
											<td ><asp:DropDownList id=ddlPRRefLocCode width=100% runat=server AutoPostBack="True" onSelectedIndexChanged="LocIndexChanged" /></td>
										</tr>
										<tr>
											<td height="25">&nbsp;</td>
											<td ><asp:Label id=lblErrRef forecolor=red visible=false runat=server /></td>
										</tr>	
										<tr>
											<td height="25">&nbsp;</td>
											<td ></td>
										</tr>	
										<tr>
											<td height="25"><asp:label id="lblStockItem" runat="server" /> :*</td>
											<td ><asp:DropDownList id=ddlItemCode width=100% runat=server AutoPostBack="True" onSelectedIndexChanged="ItemIndexChanged" />
												<asp:Label id=lblErrItem forecolor=red text="Please select one Stock Item" runat=server />
												<asp:Label id=lblErrItemExist forecolor=red text="Stock Item already exist." runat=server />
											</td>
										</tr>
										<tr>
											<td height="25">Quantity Order :*</td>
											<td ><asp:TextBox id=txtQtyOrder width=25% maxlength=15 runat=server />
												<asp:RegularExpressionValidator id="RegularExpressionValidatorQtyOrder" 
													ControlToValidate="txtQtyOrder"
													ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"
													Display="Dynamic"
													text = "Numerics of length (9.5) only"
													runat="server"/>
												<asp:RequiredFieldValidator 
													id="validateQtyOrder" 
													runat="server" 
													ErrorMessage="Please Specify Quantity To Order" 
													ControlToValidate="txtQtyOrder" 
													display="dynamic"/>
												<asp:label id=lblErrQtyOrder text="Number generated is too big!" Visible=False forecolor=red Runat="server" />
												<asp:label id=lblValidationQtyOrder text="The PO Quantity Order value can not greather than the PR Quantity Request value !" Visible=False forecolor=red Runat="server" />
											</td>
											<td width="1%">&nbsp;</td>
											<td>&nbsp;</td>	
											<td>&nbsp;</td>	
										</tr>
										<tr>
											<td height="25">Unit Cost :*</td>
											<td><asp:TextBox id=txtCost width=25% maxlength=19 runat=server />
												<asp:RegularExpressionValidator id="RegularExpressionValidatorCost" 
													ControlToValidate="txtCost"
													ValidationExpression="\d{1,19}\.\d{0,0}|\d{1,19}"
													Display="Dynamic"
													text = "Maximum length 19 digits."
													runat="server"/>
												<asp:RequiredFieldValidator 
													id="validateCost" 
													runat="server" 
													ErrorMessage="Please Specify Unit Cost" 
													ControlToValidate="txtCost" 
													display="dynamic"/>
												<asp:label id=lblErrCost text="Number generated is too big!" Visible=False forecolor=red Runat="server" />
												&nbsp;&nbsp;&nbsp;
												<asp:Label id=lblPPN runat=server text="PPN"/>
												<asp:CheckBox id="chkPPN" checked=true runat="server"></asp:CheckBox>
											</td>
										</tr>
										<tr>
											<td colspan=2 height="25"><asp:Imagebutton id="btnAdd" OnClick="btnAdd_Click" ImageURL="../../images/butt_add.gif" AlternateText=Add Runat="server" /></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>	
				</tr>
				<tr>
					<td colspan=6>&nbsp;</td>				
				</tr>				
				<tr>
					<td colspan=6>
						<asp:DataGrid id=dgPODet
							AutoGenerateColumns="false" width="100%" runat="server"
							GridLines=none
							Cellpadding="1"
							Pagerstyle-Visible="False"
							OnDeleteCommand="DEDR_Delete"
							OnCancelCommand="DEDR_Cancel"
							AllowSorting="True">	
							<HeaderStyle CssClass="mr-h"/>
							<ItemStyle CssClass="mr-l"/>
							<AlternatingItemStyle CssClass="mr-r"/>
							<Columns>
								<asp:BoundColumn Visible=False DataField="POLnId" />
								<asp:BoundColumn Visible=False DataField="ItemCode" />
								<asp:BoundColumn Visible=False DataField="QtyOrder" />
								<asp:BoundColumn Visible=False DataField="QtyReceive" />
								<asp:BoundColumn Visible=False DataField="PRLocCode" />
								<asp:BoundColumn Visible=False DataField="PRRefLocCode" />
								<asp:TemplateColumn HeaderText="PR ID">
									<ItemStyle Width="8%"/>								
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("PRID") %> id="lblPRId" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemStyle Width="8%"/>								
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("PRLocCode") %> id="lblPRLocCode" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="PR Ref. No.">
									<ItemStyle Width="8%"/>								
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("PRRefID") %> id="lblPRRefId" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemStyle Width="8%"/>								
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("PRRefLocCode") %> id="lblPRRefLocCode" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Item">
									<ItemStyle Width="12%"/>								
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("Description") %> id="lblItemCode" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Purchase UOM">
									<ItemStyle Width="7%"/>								
									<ItemTemplate> 
										<asp:Label Text=<%# Container.DataItem("PurchaseUOM") %> id="lblUOMCode" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Order Quantity">
									<HeaderStyle HorizontalAlign="Right" /> 
									<ItemStyle Width="7%" HorizontalAlign="Right" /> 
									<ItemTemplate>
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("QtyOrder"), 5), 5) %> id="lblQtyOrder" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Receive Quantity">
									<HeaderStyle HorizontalAlign="Right" /> 
									<ItemStyle Width="7%" HorizontalAlign="Right" /> 
									<ItemTemplate>
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("QtyReceive"), 5), 5) %> id="lblQtyReceive" runat="server" />							
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Unit Cost">
									<HeaderStyle HorizontalAlign="Right" /> 
									<ItemStyle Width="7%" HorizontalAlign="Right" /> 
									<ItemTemplate>
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("Cost"), 0)) %> id="lblCost" runat="server" />
									</ItemTemplate>							
								</asp:TemplateColumn>							
								<asp:TemplateColumn HeaderText="Amount">
									<HeaderStyle HorizontalAlign="Right" /> 
									<ItemStyle Width="7%" HorizontalAlign="Right" /> 
									<ItemTemplate>
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("NetAmount"), 2), 2) %> id="lblAmount" runat="server" />
									</ItemTemplate>							
								</asp:TemplateColumn>	
								<asp:TemplateColumn HeaderText="PPN">
									<HeaderStyle HorizontalAlign="Right" /> 
									<ItemStyle Width="7%" HorizontalAlign="Right" /> 
									<ItemTemplate>
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PPNAmount"), 2), 2) %> id="lblPPN" runat="server" />
									</ItemTemplate>							
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Total Amount">
									<HeaderStyle HorizontalAlign="Right" /> 
									<ItemStyle Width="7%" HorizontalAlign="Right" /> 
									<ItemTemplate>
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("Amount"), 0)) %> id="lblTotalAmount" runat="server" />
									</ItemTemplate>							
								</asp:TemplateColumn>									
								<asp:TemplateColumn>		
									<ItemStyle Width="5%" HorizontalAlign="Right" /> 
									<ItemTemplate>
										<asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" CausesValidation=False runat="server"/>
									</ItemTemplate>
								</asp:TemplateColumn>						
							</Columns>										
						</asp:DataGrid>
					</td>	
				</tr>
				<TR>
					<TD colspan=3></TD>
					<TD colspan=2 height=25><hr size="1" noshade></TD>
					<td>&nbsp;</td>					
				</TR>
				<TR>
					<td colspan=3></td>
					<TD height=25 align=left>Total Amount :</TD>
					<TD Align=right><asp:Label ID=lblTotalAmount Runat=server />&nbsp;</TD>
				</TR>
				<tr>
					<td height="25">Remarks :</td>	
					<td colspan="5"><asp:TextBox id="txtRemark" maxlength="128" width=100% runat="server" /></td>
				</tr>
				<tr>
					<td colspan=6 height="25">&nbsp;</td>
				</tr>	
				<tr>
					<td align="left" colspan="6">
						<asp:ImageButton id="btnSave" onClick="btnSave_Click" ImageUrl="../../images/butt_save.gif" AlternateText=Save CausesValidation=False runat="server" />
						<asp:ImageButton id="btnConfirm" onClick="btnConfirm_Click" ImageUrl="../../images/butt_confirm.gif" AlternateText=Confirm CausesValidation=False runat="server" />
						<asp:ImageButton id="btnPrint" onClick="btnPreview_Click" ImageUrl="../../images/butt_print.gif" AlternateText=Print CausesValidation=False runat="server" />
						<asp:ImageButton id="btnDelete" onClick="btnDelete_Click" ImageUrl="../../images/butt_delete.gif" AlternateText=Delete CausesValidation=False runat="server" />
						<asp:ImageButton id="btnUndelete" onClick="btnUnDelete_Click" ImageUrl="../../images/butt_undelete.gif" AlternateText=Undelete CausesValidation=False runat="server" />
						<asp:ImageButton id="btnBack" onClick="btnBack_Click" ImageUrl="../../images/butt_back.gif" AlternateText=Back CausesValidation=False runat="server" />
					</td>
				</tr>		
			</table>
		</form>
	</body>
</html>
