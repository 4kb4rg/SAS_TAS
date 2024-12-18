<%@ Page Language="vb" src="../../../include/CM_Trx_ContractMatchDet.aspx.vb" Inherits="CM_Trx_ContractMatchDet" %>
<%@ Register TagPrefix="UserControl" Tagname="menu_CM_Trx" src="../../menu/menu_CMTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Contract Matching Details</title>
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmSeller runat="server">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id=lblPleaseSelect visible=false text="Please select " runat=server />
			<asp:label id=lblSelect visible=false text="Select " runat=server />
			<asp:label id=lblCode visible=false text=" Code" runat=server />
			<table border=0 cellspacing=0 cellpadding=2 width=100%>
				<tr>
					<td colspan="6"><UserControl:menu_CM_Trx id=menu_CM_Trx runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="6">CONTRACT MATCHING DETAILS</td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<td height=25 width=30%>Contract Matching ID : * </td>
					<td width=30%><asp:label id="lblMatchingId" width=50% runat=server/></td>
					<td width=5%>&nbsp;</td>
					<td width=10%>Period : </td>
					<td width=20%><asp:Label id=lblPeriod runat=server /></td>
					<td width=5%>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Product :*</td>
					<td><asp:dropdownlist id=ddlProduct width=50% autopostback=true OnSelectedIndexChanged=onChange_Qty runat=server />
						<asp:RequiredFieldValidator 
							id=rfvProduct
							display=dynamic 
							runat=server
							ControlToValidate=ddlProduct
							text="<br>Please select Product." />
					</td>
					<td>&nbsp;</td>
					<td>Status : </td>
					<td><asp:Label id=lblStatus runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id=lblBillParty runat=server /> :*</td>
					<td><asp:dropdownlist id=ddlBuyer width=100% autopostback=true OnSelectedIndexChanged=onChange_Qty runat=server/>
						<asp:RequiredFieldValidator 
							id=rfvBuyer
							display=dynamic 
							runat=server
							ControlToValidate=ddlBuyer />
					</td>
					<td>&nbsp;</td>
					<td>Date Created : </td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
					<td height=25><asp:label id=lblDONo runat=server /></td>
					<td><asp:DropDownList id=ddlDONo width=100% autopostback=true  runat=server/>
						<asp:RequiredFieldValidator 
							id=rfvDONo
							display=dynamic 
							runat=server
							ControlToValidate=ddlDONo />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>

				<tr>
					<td height=25>Balance B/D From Last Matching (MT) : </td>
					<td><asp:label id=lblBDQty runat=server /></td>
					<td>&nbsp;</td>
					<td>Last Update : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Dispatch Quantity To Be Matched (MT) : </td>
					<td><asp:label id=lblDispatchQty runat=server /></td>
					<td>&nbsp;</td>
					<td>Updated By : </td>
					<td><asp:Label id=lblUpdatedBy runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Total Quantity To Be Matched (MT) : </td>
					<td><asp:label id=lblTotalMatchedQty runat=server /></td>
					<td colspan=4>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Total Quantity Matched With Contract (MT) : </td>
					<td><asp:label id=lblMatchedQty runat=server /></td>
					<td colspan=4>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Balance C/F To Next Matching (MT) : </td>
					<td><asp:label id=lblCFQty runat=server /></td>
					<td colspan=4>&nbsp;</td>
				</tr>
				
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>
				
				<tr>
					<td colspan="6">
						<asp:DataGrid id=dgDispatchDet
							AutoGenerateColumns=false width="100%" runat=server
							GridLines=none
							Cellpadding=2
							Pagerstyle-Visible=False
							AllowSorting="True">
							
							<HeaderStyle CssClass="mr-h"/>
							<ItemStyle CssClass="mr-l"/>
							<AlternatingItemStyle CssClass="mr-r"/>
							<Columns>						
								<asp:TemplateColumn HeaderText="Delivery Date">
									<ItemTemplate>
										<asp:Label Text=<%# objGlobal.GetLongDate(Container.DataItem("DateReceived")) %> id="lblDelDate" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Delivery Note">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("DeliveryNoteNo") %> id="lblDelNote" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Buyer Doc">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("CustomerDocNo") %> id="lblBuyerDoc" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Vehicle">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("VehicleCode") %> id="lblVehCode" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Transporter">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("TransporterCode") %> id="lblTransCode" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Our Net Weight(MT)">
									<ItemTemplate>
										<asp:Label id="lblOurWeight" runat="server"										
										Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("NetWeight"),2),2) %>'
										width=100% />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Buyer Net Weight(MT)">
									<ItemTemplate>
										<asp:Label id="lblBuyerWeight" runat="server"										
										Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("BuyerNetWeight"),2),2) %>'
										width=100% />
									</ItemTemplate>
								</asp:TemplateColumn>

								<asp:TemplateColumn ItemStyle-Width="10%" HeaderText="Matched" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
									<ItemTemplate>
										<asp:CheckBox id=cbMatch autopostback=True OnCheckedChanged="onChecked_Match" runat=server />
										<asp:label id=lblTicketNo text=<%#Container.DataItem("TicketNo")%> visible=false runat=server />
										<asp:label id=lblMatchId text=<%#Container.DataItem("MatchingId")%> visible=false runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>	
							</Columns>
						</asp:DataGrid>
					</td>
				</tr>
				
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>
				
				<tr>
					<td colspan="6">
						<asp:DataGrid id=dgContractDet
							AutoGenerateColumns=false width="100%" runat=server
							GridLines=none
							Cellpadding=2
							Pagerstyle-Visible=False
							AllowSorting="True">
							
							<HeaderStyle CssClass="mr-h"/>
							<ItemStyle CssClass="mr-l"/>
							<AlternatingItemStyle CssClass="mr-r"/>
							<Columns>						
								<asp:TemplateColumn HeaderText="Contract No.">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("ContractNo") %> id="lblContractNo" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Delivery Month">
									<ItemTemplate>
										<asp:Label Text=<%#Trim(Container.DataItem("DelivMonth"))%> id="lblDelMonth" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Contract Quantity">
									<ItemTemplate>
										<asp:Label id="lblContractQty" runat="server"										
										Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("ContractQty"),2),2) %>'
										width=100% />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Contract Balance Quantity">
									<ItemTemplate>
										<asp:Label id="lblContractBalQty" runat="server"										
										Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("ContractBalQty"),2),2) %>'
										width=100% />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Extra Quantity">
									<ItemTemplate>
										<asp:Label id="lblExtraQty" runat="server"										
										Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("ExtraQty"),2),2) %>'
										width=100% />&nbsp;
										<asp:Label Text=<%# Container.DataItem("ExtraQtyType") %> id="lblExtraQtyTypeDesc" runat="server" />
										<asp:Label visible=false Text=<%# Container.DataItem("ExtraQtyType")%> id="lblExtraQtyType" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Price">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("Price") %> id="lblPrice" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Quantity Matched">
									<ItemTemplate>
										<asp:Label id="lblMatchedQty" runat="server"										
										Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("MatchedQty"),2),2) %>'
										width=100% />
										<asp:Textbox Text=<%# Container.DataItem("MatchedQty") %> id="txtMatchedQty" autopostback=true ontextchanged="txtChange_MatchedQty" maxlength=15 runat="server" />
										<asp:label id=lblErrMatchedQty visible=false forecolor=red text="<br>Matched Quantity has exceeded Balance Quantity" runat=server />
										<asp:label id=lblErrMatchedQtyFmt visible=false forecolor=red text="<br>Invalid range or format of Matched Quantity" runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Invoice No.">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("InvoiceId") %> id="lblInvoiceNo" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Invoice Date">
									<ItemTemplate>
										<asp:Textbox Text=<%# objGlobal.GetShortDate(Session("SS_DATEFMT"), Container.DataItem("InvoiceDate")) %> id="txtInvoiceDate" maxlength=10 enabled=false runat="server" />
										<asp:CustomValidator id="CustomValidator1"
										ControlToValidate=txtInvoiceDate
										Display=dynamic
										ErrorMessage="Invalid date format."
										OnServerValidate=DateValidation
										runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>

							</Columns>
						</asp:DataGrid>
					</td>
				</tr>
				
				<tr>
					<td colspan="6">
						<asp:label id=lblDate Text="<br>Invoice Date Entered should be in the format" forecolor=red Visible=false Runat="server"/> 
						<asp:label id=lblFmt forecolor=red Visible=false Runat="server"/>
						<asp:label id=lblDateError Text ="<br>Please enter Invoice Date." forecolor=red Visible = false Runat="server"/>
					</td>
				</tr>
				
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>
				
				<tr>
					<td colspan="6">
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn AlternateText=" Delete "  CausesValidation=False imageurl="../../images/butt_delete.gif" onclick=Button_Click CommandArgument=Del runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
						<asp:ImageButton id=GenInvoiceBtn AlternateText="  Generate Invoice  " CausesValidation=False imageurl="../../images/butt_gen_invoice.gif" onclick=Button_GenInvoice runat=server />
					</td>
				</tr>
				<asp:label id=lblLastMatchingId visible=false runat=server/>
				<asp:label id=lblHiddenSts visible=false text="0" runat=server/>
				<asp:label id=lblHasDispatch visible=false text="no" runat=server />
				<asp:label id=lblShowGenInvoice visible=false text="no" runat=server />
				<asp:label id=lblParamTicketNo visible=false runat=server />
				<asp:label id=lblParamContractNo visible=false runat=server />
				<asp:label id=lblParamPrice visible=false runat=server />
				<asp:label id=lblParamMatchedQty visible=false runat=server />
				<asp:label id=lblParamPrevMatchedQty visible=false runat=server />
			</table>
			<Input Type=Hidden id=tbcode runat=server />
		</form>
	</body>
</html>
