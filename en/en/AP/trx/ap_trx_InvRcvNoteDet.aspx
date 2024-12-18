<%@ Page Language="vb" src="../../../include/ap_trx_InvRcvNoteDet.aspx.vb" Inherits="ap_trx_InvRcvNoteDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuAP" src="../../menu/menu_aptrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<html>
	<head>
		<title>Invoice Receive Details</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<script language="javascript">
		function calTransDate() {
			    var doc = document.frmMain;
			    doc.txtInvoiceRcvRefDate.value = doc.txtTransDate.value;
	        }
	</script>	
	</head>
	<body>
		<form id=frmMain runat=server>
		<table id="tblHeader" cellSpacing="0" cellPadding="2" width="100%" border="0">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server /><asp:Label id=lblStatusHidden visible=false runat=server /><asp:label id=lblPleaseSelect visible=false text="Please select " runat=server /><asp:Label id=lblVehicleOption visible=false text=false runat=server/><asp:Label id=lblCode visible=false text=" Code" runat=server/><asp:label id=lblID visible=false text=" ID" runat=server/><asp:label id=lblRefNo visible=false text=" Ref. No." runat=server/><asp:label id=lblRefDate visible=false text=" Ref. Date" runat=server/><tr>
				<td colspan="6"><UserControl:MenuAP id=MenuAP runat="server" /></td>
			</tr>
			<tr>
				<td class="mt-h" colspan="6"><asp:label id=lblTitle runat=server/> DETAILS</td>
			</tr>
			<tr>
				<td colspan="6"><hr size="1" noshade></td>
			</tr>
			<tr>
				<td width="20%" height=25><asp:label id=lblInvoiceRcvIDTag runat=server /> :</td>
				<td width="45%"><asp:Label id=lblInvoiceRcvID runat=server /></td>
				<td width="5%">&nbsp;</td>
				<td width="15%">Period : </td>
				<td width="15%"><asp:Label id=lblAccPeriod runat=server /></td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25>Transaction Date :</td>
				<td><asp:TextBox ID=txtTransDate maxlength=10 width=25% OnKeyUp ="javascript:calTransDate();" Runat=server />
					<a href="javascript:PopCal('txtTransDate');"><asp:Image id="btnSelTransDate" ImageAlign=absMiddle runat="server" ImageUrl="../../Images/calendar.gif"/></a>
					<asp:Label id=lblErrTransDate forecolor=red text="Date format " runat=server />
					<asp:label id=lblFmtTransDate  forecolor=red Visible = false Runat="server"/> 
				</td>
				<td>&nbsp;</td>
				<td>Status :</td>
				<td style="width: 278px"><asp:Label id=lblStatus runat=server /></td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25>Supplier Code :*</td>
				<td>
                    <asp:TextBox ID="txtSupCode" runat="server" AutoPostBack="False" MaxLength="15" Width="50%"></asp:TextBox>
                    <input type=button value=" ... " id="FindSpl" onclick="javascript:PopSupplier_New('frmMain','','txtSupCode','txtSupName','txtCreditTerm','txtPPN','txtPPNInit', 'False');" CausesValidation=False runat=server />
                    <asp:ImageButton ID="btnGet" runat="server" AlternateText="Get Data" CausesValidation="False"
                        ImageAlign="AbsBottom" ImageUrl="../../images/icn_next.gif" OnClick="GetSupplierBtn_Click"
                        ToolTip="Click For Get Data" /><br />
                    <asp:TextBox ID="txtSupName" runat="server" BackColor="Transparent" BorderStyle="None"
                        Font-Bold="True" ForeColor="White" MaxLength="10" Width="99%"></asp:TextBox><br />
					<asp:Label id=lblErrSuppCode visible=false forecolor=red text="Please select Supplier Code" runat=server/>
				</td>
				<td>&nbsp;</td>
				<td height=25>Date Created : </td>
				<td style="width: 278px"><asp:Label id=lblDateCreated runat=server /></td>
				<td>&nbsp;</td>
			</tr>
			
			<tr>
				<td height=25>Receive From : </td>
				<td>
					<asp:TextBox ID=txtReceiveFrom maxlength=75 width=90% Runat=server />
				</td>
				<td>&nbsp;</td>
				<td>Last Update :</td>
				<td style="width: 278px"><asp:Label ID=lblLastUpdate runat=server /></td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25><asp:label id=lblInvoiceRcvRefNoTag runat=server/> : </td>
				<td>
					<asp:TextBox ID=txtInvoiceRcvRefNo maxlength=32 width=90% Runat=server />
				</td>
				<td>&nbsp;</td>
				<td>Print Date:</td>
				<td style="width: 278px"><asp:Label ID=lblPrintDate runat=server /></td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25><asp:label id=lblInvoiceRcvRefDateTag runat=server/> :</td>
				<td><asp:TextBox ID=txtInvoiceRcvRefDate maxlength=10 width=25% Runat=server />
					<a href="javascript:PopCal('txtInvoiceRcvRefDate');"><asp:Image id="btnSelDate" ImageAlign=AbsMiddle runat="server" ImageUrl="../../Images/calendar.gif"/></a>
					<asp:Label id=lblErrInvRcvRefDate forecolor=red text="Date format " runat=server />
					<asp:label id=lblFmtInvRcvRefDate  forecolor=red Visible = false Runat="server"/> 
				</td>
				<td>&nbsp;</td>
				<td>Updated By : </td>
				<td style="width: 278px"><asp:Label ID=lblUpdatedBy runat=server /></td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25>Invoice Type :*</td>
				<td><asp:RadioButton id=rbSPO text="" checked=true autopostback=true oncheckedchanged=InvoiceType_OnCheckChange groupname="InvoiceType" runat=server/></td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25>&nbsp;</td>
				<td><asp:RadioButton id=rbAdvPay text="" autopostback=true oncheckedchanged=InvoiceType_OnCheckChange groupname="InvoiceType" runat=server/></td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25>&nbsp;</td>
				<td><asp:RadioButton id=rbOTE text="" autopostback=true oncheckedchanged=InvoiceType_OnCheckChange groupname="InvoiceType" runat=server/></td>
				<td>&nbsp;</td>
			</tr>
			<tr>
			    <td height=25>&nbsp;</td>
				<td><asp:RadioButton id=rbFFB text="" autopostback=true oncheckedchanged=InvoiceType_OnCheckChange groupname="InvoiceType" runat=server/></td>
				<td>&nbsp;</td>
			</tr>
			<tr>
			    <td height=25>&nbsp;</td>
				<td><asp:RadioButton id=rbTransportFee text="" autopostback=true oncheckedchanged=InvoiceType_OnCheckChange groupname="InvoiceType" runat=server/></td>
				<td>&nbsp;</td>
			</tr>
			
			<tr>
			    <td height=25>Credit Term Type :</td>
				<td><asp:DropDownList id="ddlTermType" width="50%" runat="server" /></td>
				<td>&nbsp;</td>
			</tr>
			<tr>
			    <td height=25>Credited Invoice Due Date : </td>
				<td><asp:TextBox ID=txtInvDueDate maxlength=10 width=25% Runat=server /> 
				    <a href="javascript:PopCal('txtInvDueDate');"><asp:Image id="btnSelInvDueDate" ImageAlign=AbsMiddle runat="server" ImageUrl="../../Images/calendar.gif"/></a>
				    <asp:Label id=lblerrInvDueDate forecolor=red text="Date format " runat=server />
				    <asp:label id=lblFmtInvDueDate  forecolor=red Visible = false Runat="server"/> 
				</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25>Via Bank Transfer : </td>
				<td><asp:RadioButton id="rbViaBTNo" text=" No" checked="true" GroupName="rbViaBT" runat="server" />
					<asp:RadioButton id="rbViaBTYes" text=" Yes" GroupName="rbViaBT" runat="server" />
				</td>			
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td colspan=6>&nbsp;</td>
			</tr>
			<tr>
				<td colSpan="6">
					<!--<table width="100%" class="mb-c" cellspacing="0" cellpadding="4" border="0">
						<tr>						
							<td>-->
								<table id="tblSelection" cellSpacing="0" cellPadding="2" width="100%" border="0" runat=server>
									<tr class="mb-c">
									    <td width="20%" height=25>Purchase Order ID :*</td>
										<td colSpan="5"><asp:DropDownList id=ddlPO width=95%  autopostback=true onSelectedIndexChanged=onSelect_Change runat=server />
										                <asp:Label id=lblErrPO visible=false forecolor=red text="Please select Purchase Order ID" runat=server/>
										</td>						
									</tr>
									<tr class="mb-c">
									    <td width="20%" height=25>Advance Payment : </td>
			                            <td colSpan="5"><asp:Label ID=lblAdvancePayment Text="0" runat=server /></td>
									</tr>
									<tr class="mb-c">
									    <td width="20%" height=25>DN/CN Amount : </td>
			                            <td colSpan="5"><asp:Label ID=lblAdjAmount Text="0" runat=server /></td>
									</tr>
									<tr class="mb-c">
			                            <td width="20%" height=25>Supplier Invoice Amount : </td>
			                            <td colSpan="5"><asp:TextBox ID=txtSplInvAmt width=20% text=0 style="text-align:right" Runat=server /> 
			                                <asp:RegularExpressionValidator id=RegularExpressionValidator1 												
					                            ControlToValidate="txtSplInvAmt"												
					                            ValidationExpression="\d{1,19}\.\d{0,2}|\d{1,19}"
					                            Display="Dynamic"
					                            text="Maximum length 21 digits and 2 decimal points."
					                            runat="server"/>
				                            <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" 
					                            display="dynamic" 
					                            ControlToValidate="txtSplInvAmt"
					                            Text="Please enter cost" />
				                            <asp:Label id=lblErrSplInvAmt visible=false forecolor=red text=" Please enter supplier invoice amount." runat=server />
			                            </td>
			                       </tr>
			                       <tr class="mb-c">
			                            <td width="20%" height=25>Credit Term : </td>
				                        <td colSpan="5"><asp:TextBox ID=txtCreditTerm maxlength=3 width=10% Runat=server /> Days
					                        <asp:CompareValidator id="validateCreditTerm" display=dynamic runat="server" 
						                        ControlToValidate="txtCreditTerm" Text="<br>The value must whole number. " 
						                        Type="integer" Operator="DataTypeCheck"/>				
						                   <asp:Label id=lblErrCreditTerm visible=false forecolor=red text=" Please enter credit term." runat=server />     
				                        </td>
			                        </tr>
			                         <tr class="mb-c">
			                            <td width="20%" height=25>PO Due Date : </td>
				                        <td colSpan="5"><asp:TextBox ID=txtPODueDate maxlength=10 width=10% Enabled=false Runat=server /></td>
			                        </tr>
			                         <tr class="mb-c">
			                            <td width="20%" style="height: 28px">Due Date : </td>
				                        <td colSpan="5" style="height: 28px"><asp:TextBox ID=txtDueDate maxlength=10 width=10% Enabled=false Runat=server /></td>
			                        </tr>
			                       
			              
			                        <tr class="mb-c">
			                            <td height=25>Tax Invoice/Faktur Pajak No. : </td>
				                        <td colSpan="5"><asp:TextBox ID=txtFakturPjkNo maxlength=19 width=20% Runat=server /> 
				                                        <asp:Label id=lblErrFakturPjk visible=false forecolor=red runat=server />     
				                        </td>
			                        </tr>
			                        <tr class="mb-c">
			                            <td width="20%" height=25>Tax Amount/Nilai Faktur Pajak : </td>
			                            <td colSpan="5"><asp:TextBox ID=txtSplTaxAmt maxlength=10 width=20% text=0 style="text-align:right" Runat=server /> 
			                                <asp:RegularExpressionValidator id=RegularExpressionValidator2 												
					                            ControlToValidate="txtSplTaxAmt"												
					                            ValidationExpression="\d{1,19}\.\d{0,2}|\d{1,19}"
					                            Display="Dynamic"
					                            text="Maximum length 21 digits and 2 decimal points."
					                            runat="server"/> 
			                            </td>
			                       </tr>
			                        
			                        <tr class="mb-c">
			                            <td height=25>Tax Date/Faktur Pajak Date : </td>
				                        <td colSpan="5"><asp:TextBox ID=txtFakturPjkDate maxlength=10 width=10% Runat=server /> 
				                        <a href="javascript:PopCal('txtFakturPjkDate');"><asp:Image id="btnSelFakturPjkDate" ImageAlign=AbsMiddle runat="server" ImageUrl="../../Images/calendar.gif"/></a>
				                        <asp:Label id=lblerrFakturPjkDate forecolor=red text="Date format " runat=server />
				                        <asp:label id=lblFmtFakturPjkDate  forecolor=red Visible = false Runat="server"/> 
				                        </td>
			                        </tr>
			                        
			                        
			                          <tr class="mb-c">
			                            <td width="20%" height=25>Additional Note : </td>
				                        <td colSpan="5"><asp:TextBox ID=txtAddNote maxlength=256 width=95% Enabled=true Runat=server /></td>
			                        </tr>
			                        
			                        <tr class="mb-c">
				                        <td colSpan="6">&nbsp;</td>
			                        </tr>	
									<tr class="mb-c">
										<td height=25 colspan=2>
											<asp:ImageButton id=AddBtn imageurl="../../images/butt_add.gif" alternatetext=Add CausesValidation=True onclick=AddBtn_Click UseSubmitBehavior="false" runat=server /> 									
										</td>
										<td>&nbsp;</td>
										<td>&nbsp;</td>
										<td>&nbsp;</td>
										<td>&nbsp;</td>
									</tr>
								</table>
							<!--</td>
						</tr>
					</table>-->
				</td>
			</tr>
			<tr>
				<td colSpan="6">
					<asp:DataGrid id=dgLineDet
						AutoGenerateColumns="false" width="100%" runat="server"
						GridLines=none
						Cellpadding="2"
						Pagerstyle-Visible="False"
						OnEditCommand="DEDR_Edit"
						OnUpdateCommand="DEDR_Update"
						OnCancelCommand="DEDR_Cancel"
						AllowSorting="True">
						<HeaderStyle CssClass="mr-h"/>
						<ItemStyle CssClass="mr-l"/>
						<AlternatingItemStyle CssClass="mr-r"/>
						<Columns>	
							<asp:TemplateColumn HeaderText="POID" ItemStyle-Width="15%">
								<ItemTemplate>
									<asp:Label visible=false Text=<%# Container.DataItem("InvoiceRcvLnID") %> id="lblInvoiceRcvLnID" runat="server" />
									<asp:Label visible=true Text=<%# Container.DataItem("POID") %> id="lblPOID" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							
							<asp:TemplateColumn HeaderText="Faktur Pajak No." ItemStyle-Width="10%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
								<ItemTemplate>
								    <asp:label text=<%# Container.DataItem("FakturPajakNo") %> id="lblFakturPajakNo" runat="server" />
									<asp:TextBox id="lstFakturPajakNo" Visible=false Text='<%# trim(Container.DataItem("FakturPajakNo")) %>'
										        runat="server"/>	
								</ItemTemplate>
							</asp:TemplateColumn>	
							
							<asp:TemplateColumn HeaderText=" Faktur Amount" HeaderStyle-HorizontalAlign=Right ItemStyle-Width="8%" ItemStyle-HorizontalAlign=Right>
								<ItemTemplate> 
									<ItemStyle />
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("FakturAmount"), 2), 2) %> id="lblIDFakturAmount" runat="server" />
										<asp:TextBox id="lblFakturAmount" visible = False Text=<%# Container.DataItem("FakturAmount") %> runat="server" />
									</ItemStyle>
								</ItemTemplate>
							</asp:TemplateColumn>		
							
					
							<asp:TemplateColumn HeaderText="Faktur Pajak Date" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
								<ItemTemplate>
								    <asp:label text=<%# objGlobal.GetLongDate(Container.DataItem("FakturPajakDate")) %> id="lblFakturPajakDate" runat="server" />
									<asp:TextBox id="lstFakturPajakDate" Visible=false Text='<%# trim(Container.DataItem("FakturPajakDate")) %>'
										        runat="server"/>	
								</ItemTemplate>
							</asp:TemplateColumn>	
														
							
							<asp:TemplateColumn HeaderText="Due Date" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
								<ItemTemplate>
								    <asp:label text=<%# objGlobal.GetLongDate(Container.DataItem("DueDate")) %> id="lblDueDate" runat="server" />
									<asp:TextBox id="lstDueDate" Visible=false Text='<%# trim(Container.DataItem("DueDate")) %>'
										        runat="server"/>	
								</ItemTemplate>
							</asp:TemplateColumn>
									
							<asp:TemplateColumn HeaderText=" Amount" HeaderStyle-HorizontalAlign=Right ItemStyle-Width="8%" ItemStyle-HorizontalAlign=Right>
								<ItemTemplate> 
									<ItemStyle />
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("SupplierInvAmount"), 2), 2) %> id="lblIDAmount" runat="server" />
										<asp:Label Text=<%# FormatNumber(Container.DataItem("SupplierInvAmount"), 2) %> id="lblAmount" visible = False runat="server" />
									</ItemStyle>
								</ItemTemplate>
							</asp:TemplateColumn>		
							<asp:TemplateColumn ItemStyle-Width="5%" ItemStyle-HorizontalAlign=Right>
								<ItemTemplate>
									<asp:LinkButton id=lbDelete CommandName="Delete" Text="Delete" CausesValidation=False runat=server />
									<asp:LinkButton id=lbEdit CommandName="Edit" Text="Edit" CausesValidation=False  runat="server"/>
								    <asp:LinkButton id=lbUpdate CommandName="Update" Text="Update" CausesValidation=False  runat="server"/>
									<asp:LinkButton id=lbCancel CommandName="Cancel" Text="Cancel" CausesValidation=False  runat="server"/>
								</ItemTemplate>
							</asp:TemplateColumn>	
					</Columns>										
					</asp:DataGrid>
				</td>
			</tr>
			<tr>
				<td colspan=3>&nbsp;</td>
				<td colspan=2 height=25><hr size="1" noshade></td>
				<td>&nbsp;</td>
			</tr>		
			<tr>				
				<td colspan=3>&nbsp;</td>
				<td height=25>Total Invoice Amount :</td>
				<td align=right><asp:Label ID=lblCurrency1 Runat=server />&nbsp;&nbsp;&nbsp;&nbsp;
				<asp:Label ID=lblIDInvoiceAmount Runat=server />
				<asp:Label ID=lblInvoiceAmount VISIBLE = FALSE Runat=server />&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
		
			<tr>
				<td vAlign="top">Remarks :</td>
				<td colSpan="5"><asp:TextBox ID=txtRemark maxlength=256 width=100% Runat=server /></td>
			</tr>
			<tr>
				<td colSpan="6">&nbsp;</td>
			</tr>			
			<tr>
				<td colSpan="6">
					<asp:Label id=lblErrUnDel visible=false forecolor=red text="Not enough quantity to perform Undelete" runat=server />
					<asp:Label id=lblErrCurrency visible=false forecolor=red text="Only one Currency allowed." runat=server /> 
					<P>
					<asp:ImageButton ID="NewBtn"     UseSubmitBehavior="false" AlternateText="New"     onclick="NewBtn_Click"     ImageUrl="../../images/butt_new.gif"     CausesValidation=False Runat=server /> 
					<asp:ImageButton ID="SaveBtn"     UseSubmitBehavior="false" AlternateText="Save"     onclick="SaveBtn_Click"     ImageUrl="../../images/butt_save.gif"     CausesValidation=False Runat=server /> 
					<asp:ImageButton ID="GenInvBtn"  UseSubmitBehavior="false" AlternateText="Generate Invoice"  onclick="GenInvBtn_Click"  ImageUrl="../../images/butt_gen_invoice.gif"  CausesValidation=False Runat=server />
					<asp:ImageButton ID="ConfirmBtn"  UseSubmitBehavior="false" AlternateText="Confirm"  onclick="ConfirmBtn_Click"  ImageUrl="../../images/butt_confirm.gif"  CausesValidation=False Runat=server />
					<asp:ImageButton ID="PrintBtn"    UseSubmitBehavior="false" AlternateText="Print"    onclick="PrintBtn_Click"    ImageUrl="../../images/butt_print.gif"             visible=False Runat=server />
					<asp:ImageButton ID="CancelBtn"   UseSubmitBehavior="false" AlternateText="Cancel"   onclick="CancelBtn_Click"   ImageUrl="../../images/butt_cancel.gif"   CausesValidation=False Runat=server />
					<asp:ImageButton ID="DeleteBtn"   UseSubmitBehavior="false" AlternateText="Delete"   onclick="DeleteBtn_Click"   ImageUrl="../../images/butt_delete.gif"   CausesValidation=False Runat=server />
					<asp:ImageButton ID="UnDeleteBtn" UseSubmitBehavior="false" AlternateText="Undelete" onclick="UnDeleteBtn_Click" ImageUrl="../../images/butt_undelete.gif" CausesValidation=False Runat=server />
					<asp:ImageButton ID="BackBtn"     UseSubmitBehavior="false" AlternateText="Back"     onclick="BackBtn_Click"     ImageUrl="../../images/butt_back.gif"     CausesValidation=False Runat=server />
					
					<Input type=hidden id=inrid value="" runat=server />
					<Input type=hidden id=idSuppCode value="" runat=server />
					<Input type=hidden id=hidBlockCharge value="" runat=server/>
					<Input type=hidden id=hidChargeLocCode value="" runat=server/>
					<Input type=hidden id=HidCurrencyCode value="" runat=server/>
					<Input type=hidden id=HidExchangeRate value="" runat=server/>
					<Input type=hidden id=hidTermType value="" runat=server />
					<Input type=hidden id=hidPOItem value="" runat=server />
					<Input type=hidden id=hidPOAmount value=0 runat=server />
					<Input type=hidden id=hidAdvAmount value=0 runat=server />
					<Input type=hidden id=hidAdjAmount value=0 runat=server />
					<Input type=hidden id=hidPPNInit value="" runat=server />
				</td>
			</tr>
			<tr>
				<td colSpan="6">&nbsp;<asp:TextBox ID="txtPPN" runat="server" BackColor="Transparent" BorderStyle="None"
                        Width="9%"></asp:TextBox>
                    <asp:TextBox ID="txtPPNInit" runat="server" BackColor="Transparent" BorderStyle="None"
                        Width="9%"></asp:TextBox></td>
			</tr>	
			<tr>
				<td colspan=6>
					<asp:DataGrid id=dgAPNote
						AutoGenerateColumns="false" width="30%" runat="server"
						GridLines=none
						Cellpadding="1"
						Pagerstyle-Visible="False"
						AllowSorting="false">	
						<HeaderStyle CssClass="mr-h"/>
						<ItemStyle CssClass="mr-l"/>
						<AlternatingItemStyle CssClass="mr-r"/>
						<Columns>
							<asp:HyperLinkColumn HeaderText="Credited Invoice No." 
								SortExpression="InvoiceRcvID" 
								DataNavigateUrlField="InvoiceRcvID" 
								DataNavigateUrlFormatString="ap_trx_InvRcvDet.aspx?inrid={0}"
								DataTextFormatString="{0:c}"
								DataTextField="InvoiceRcvID" />									
						</Columns>
					</asp:DataGrid>
				</td>
			</tr>
		</table>
		</form>
	</body>
</html>
