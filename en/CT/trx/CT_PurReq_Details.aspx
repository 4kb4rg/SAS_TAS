<%@ Page Language="vb" src="../../../include/CT_PurReq_Details.aspx.vb" Inherits="CT_PurReqDetails" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuCTTrx" src="../../menu/menu_CTtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Purchase Requisition Details</title>		
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<form id=frmPurReqDet runat=server>
		<input type=hidden id=hidPQID runat=server NAME="hidPQID"/>
		<asp:label id="SortExpression" Visible="False" Runat="server"></asp:label>
		<asp:Label id=lblErrMesage visible=false Text="Error while initiating component." runat=server />
		<asp:label id=lblStatus visible=false runat=server />
		<asp:label id=lblPrintDate visible=false runat=server />				
		<table border="0" width="100%" cellspacing="0" cellpadding="1">
			<tr>
				<td colspan="6"><UserControl:MenuCTTrx id=menuCT runat="server" /></td>
			</tr>
			<tr>
				<td class="mt-h" colspan="6">PURCHASE REQUISITION DETAILS</td>
			</tr>
			<tr>
				<td colspan=6><hr size="1" noshade></td>
			</tr>			
			<tr>
				<td width=20% height="25">Purchase Requisition ID :</td>
				<td width=30%><asp:label id=lblPurReqID Runat="server"/></td>
				<td width=5%>&nbsp;</td>
				<td width=15%>Status :</td>
				<td width=25%><asp:Label id=Status runat=server /></td>
				<td width=5%>&nbsp;</td>
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
				<td colspan="6">
					<table id="tblLine" width="100%" class="mb-c" cellspacing="0" cellpadding="4" border="0" runat=server>
						<tr>
							<td width=20% height="25">Canteen Item Code :*</td>
							<td width=80%><asp:DropDownList id="lstCanteen" width=90% runat=server />
										    <input type=button value=" ... " id="FindCT" onclick="javascript:findcode('frmPurReqDet','','','','','','','','','','','lstCanteen','','','','',hidBlockCharge.value,hidChargeLocCode.value);" CausesValidation=False runat=server />
											<asp:RequiredFieldValidator 
												id="validateQty" 
												runat="server" 
												ErrorMessage="<BR>Please select one Item" 
												ControlToValidate="lstCanteen" 
												display="dynamic"/>
							</td>
						</tr>
						<tr>
							<td height="25">Quantity Request :*</td>
							<td><asp:textbox id="QtyReq" width=50% maxlength=20 Runat="server" />
								<asp:RegularExpressionValidator id="RegularExpressionValidatorQtyReq" 
									ControlToValidate="QtyReq"
									ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"
									Display="Dynamic"
									text = "Maximum length 9 digits and 5 decimal points"
									runat="server"/>	
								<asp:RequiredFieldValidator 
									id="validateQtyReq" 
									runat="server" 
									ErrorMessage="Please specify quantity to request" 
									ControlToValidate="QtyReq" 
									display="dynamic"/>
								<asp:RangeValidator id="RangeQtyReq"
									ControlToValidate="QtyReq"
									MinimumValue="1"
									MaximumValue="999999999999999"
									Type="double"
									EnableClientScript="True"
									Text="The value must be from 1!"
									runat="server" display="dynamic"/>						
							</td>
						</tr>
						<tr>
							<td height="25">Unit Cost :</td>
							<td><asp:textbox id="UnitCost" width=50% maxlength=19 Runat="server" />
								<asp:RegularExpressionValidator id="RegularExpressionValidatorUnitCost" 
									ControlToValidate="UnitCost"
									ValidationExpression="\d{1,19}"
									Display="Dynamic"
									text = "Maximum length 19 digits and 0 decimal points"
									runat="server"/>													
								<asp:RangeValidator id="RangeUnitCost"
									ControlToValidate="UnitCost"
									MinimumValue="1"
									MaximumValue="999999999999999"
									Type="double"
									EnableClientScript="True"
									Text="The value must be from 1!"
									runat="server" display="dynamic"/>
							</td>
						</tr>
						<tr>
							<td colspan=2><asp:ImageButton text="Add" id="Add" ImageURL="../../images/butt_add.gif" OnClick="btnAdd_Click" Runat="server" /></td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td colspan=6>&nbsp;</td>				
			</tr>
			<tr>
				<td colspan="6"> 
				<table id="PRLnTable" border="0" width="100%" cellspacing="0" cellpadding="2" runat="server">
					<tr>
						<td width=100%>
							<asp:DataGrid id="dgPRLn"
								AutoGenerateColumns="false" width="100%" runat="server"
								GridLines = none
								Cellpadding = "2"
								Pagerstyle-Visible="False"
								OnDeleteCommand="DEDR_Delete"
								OnCancelCommand="DEDR_Cancel"
								AllowSorting="True">	
								<HeaderStyle CssClass="mr-h" />							
								<ItemStyle CssClass="mr-l" />
								<AlternatingItemStyle CssClass="mr-r" />						
								<Columns>
									<asp:TemplateColumn HeaderText="Item">
										<ItemStyle Width="22%"/> 																								
										<ItemTemplate>
											<asp:label text=<%# Container.DataItem("ItemCode") %> id="ItemCode" runat="server" />
											(<%# Container.DataItem("ItemDesc") %>)				
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Stock UOM">
										<ItemStyle Width="8%"/> 																								
										<ItemTemplate>
											<%# Container.DataItem("UOMCode") %>
											<asp:label text=<%# objCT.mtdGetPurReqLnStatus(Container.DataItem("Status")) %> visible="false" id="hidStatus" runat="server" />			
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right HeaderText="Quantity Requested">
										<ItemStyle Width="15%"/> 																								
										<ItemTemplate>
											<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("QtyReq"),5) %> id="lblQtyReq" runat="server" />							
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Quantity Received" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
										<ItemStyle Width="15%"/> 																								
										<ItemTemplate>
											<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("QtyRcv"),5) %> id="lblQtyRcv" runat="server" />
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Quantity Outstanding" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
										<ItemStyle Width="15%"/> 																								
										<ItemTemplate>
											<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("QtyOutstanding"),5) %> id="lblQtyOutstanding" runat="server" />
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Unit Cost" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
										<ItemStyle Width="10%"/> 																								
										<ItemTemplate>
											<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator(Container.DataItem("Cost")) %> id="lblUnitCost" runat="server" />
										</ItemTemplate>							
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Amount" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
										<ItemStyle Width="10%"/> 																								
										<ItemTemplate>
											<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator(Container.DataItem("Amount")) %> id="lblAmount" runat="server" />
										</ItemTemplate>							
									</asp:TemplateColumn>		
									<asp:TemplateColumn ItemStyle-HorizontalAlign=Right>		
										<ItemStyle Width="5%"/> 																								
										<ItemTemplate>
											<asp:LinkButton id="Delete" CommandName="Delete" Text="Delete"  CausesValidation="false" runat="server"/>
											<asp:LinkButton id="Cancel" CommandName="Cancel" Text="Cancel"  CausesValidation="false" runat="server"/>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>										
							</asp:DataGrid>
						</td>	
					</tr>
				</table>				
			</tr>
			<tr>
				<td colspan=3>&nbsp;</td>
				<td colspan=2 height=25><hr size="1" noshade></td>
				<td width="5%">&nbsp;</td>					
			</tr>
			<tr>
				<td colspan=3>&nbsp;</td>
				<TD height=25>Total Amount :</TD>
				<TD Align=right><asp:Label ID=lblTotAmtFigDisplay Runat=server /><asp:Label ID=lblTotAmtFig visible="false" Runat=server />&nbsp;</TD>
				<td>&nbsp;</td>
			</TR>
			<tr>
				<td><asp:label id="Remarks" text="Remarks :" runat="server" /></td>	
				<td colspan="5"><asp:textbox id="txtRemarks" width=100% maxlength="256" runat="server" /></td>
			</tr>
			<tr>
				<td colspan=6>&nbsp;</td>
			</tr>
			<tr>
				<td align="left" colspan="6">
					<asp:ImageButton id="Save"		ImageURL="../../images/butt_save.gif"	onClick="btnSave_Click"  CausesValidation="false" AlternateText="Save" runat="server" />
					<asp:ImageButton id="Confirm"	ImageURL="../../images/butt_confirm.gif" onClick="btnConfirm_Click"  CausesValidation="false" AlternateText="Confirm" runat="server" />
					<asp:ImageButton id="Cancel"	ImageURL="../../images/butt_cancel.gif" onClick="btnCancel_click"  CausesValidation="false"  AlternateText="Cancel" visible=false runat="server" />
					<asp:ImageButton id="Print"		ImageURL="../../images/butt_print.gif" AlternateText="Print"  CausesValidation="false"  runat="server" onClick="btnPreview_Click"/>
					<asp:ImageButton id="PRDelete"	ImageURL="../../images/butt_delete.gif" CausesValidation="false"  onClick="btnPRDelete_Click" AlternateText="Delete" runat="server" />
					<asp:ImageButton id="Undelete"	ImageURL="../../images/butt_undelete.gif" onClick="btnPRUnDelete_Click"  CausesValidation="false" AlternateText="Undelete" runat="server" />
					<asp:ImageButton id="Back"		ImageURL="../../images/butt_back.gif"	onClick="btnBack_Click"  CausesValidation="false" AlternateText="Back" runat="server" />
				</td>
			</tr>		
		</table>
			<Input type=hidden id=hidBlockCharge value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>
		</form>
	</body>
</html>
