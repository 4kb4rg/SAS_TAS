<%@ Page Language="vb" src="../../../include/cb_trx_PayDet.aspx.vb" Inherits="cb_trx_PayDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuCB" src="../../menu/menu_cbtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<html>
	<head>
		<title>Payment Details</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<script language="javascript">
			function fnPosNeg(str)
			{
				var hidField = document.getElementById("hidCreditJrnValue");
				var iEnd, iLen = str.length;
				
                if (13 + 1 > iLen)
                        iEnd = iLen;
                else
                        iEnd = 13 + 1;

				hidField.value = str.substring(13, iEnd);
				//alert(hidField.value);
				return;
			}
			function calTaxPrice() {
				var doc = document.frmMain;
				var a = parseFloat(doc.txtDPPAmount.value);
				var b = parseFloat(doc.hidTaxObjectRate.value);		
				var c = (a * (b/100));		    
				var newnumber = new Number(c+'').toFixed(parseInt(0));
				
				doc.txtAmount.value = newnumber;
				if (doc.txtAmount.value == 'NaN')
					doc.txtAmount.value = '';
				else
					doc.txtAmount.value = doc.txtAmount.value;
			}
			
		</script>
	</head>
	<body>
		<form id=frmMain runat=server>
		<TABLE id="tblHeader" cellSpacing="0" cellPadding="2" width="100%" border="0">
			<tr>
				<td colspan="6"><UserControl:MenuCB id=MenuCB runat="server" /></td>
			</tr>
			<tr>
				<td class="mt-h" colspan="6">PAYMENT DETAILS</td>
			</tr>
			<tr>
				<td colspan=6><hr size="1" noshade></td>
			</tr>
			<TR>
				<TD height=25 width="20%">Payment ID :</TD>
				<TD width="40%"><asp:Label id=lblPaymentID runat=server /></TD>
				<TD width="5%">&nbsp;</TD>
				<TD width="15%">Period :</TD>
				<TD width="20%"><asp:Label id=lblAccPeriod runat=server /></TD>
				<TD width="5%">&nbsp;</TD>
			</TR>
			<tr>
			    <TD height=25>Transaction Date :</TD>
			    <td><asp:TextBox id=txtDateCreated width=50% maxlength="10" runat="server"/>
					<a href="javascript:PopCal('txtDateCreated');"><asp:Image id="btnDateCreated" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
					<asp:RequiredFieldValidator	id="rfvDateCreated" runat="server"  ControlToValidate="txtDateCreated" text = "Please enter Date Created" display="dynamic"/>
					<asp:label id=lblDate Text ="<br>Date Entered should be in the format " forecolor=red Visible = false Runat="server"/> 
					<asp:label id=lblFmt  forecolor=red Visible = false Runat="server"/> 
				</td>
				<TD>&nbsp;</TD>
				<TD>Status :</TD>
				<TD><asp:Label id=lblStatus runat=server /></TD>
				<TD width="5%">&nbsp;</TD>
			</tr>
			<TR>
				<TD height=25>Supplier Code :*</TD>
				<TD><asp:DropDownList width=90% id=ddlSupplier onSelectedIndexChanged=onSelect_Change runat=server />
				    <input type=button value=" ... " id="Find" onclick="javascript:PopSupplier('frmMain', '', 'ddlSupplier', 'True');" CausesValidation=False runat=server />
					<asp:RequiredFieldValidator 
						id="validateSupp" 
						runat="server" 
						ErrorMessage="Please select Supplier Code" 
						ControlToValidate="ddlSupplier" 
						display="dynamic"/>
				</TD>
				<TD>&nbsp;</TD>
				<TD>Date Created :</TD>
				<TD><asp:Label id=lblDateCreated runat=server /></TD>
				<TD width="5%">&nbsp;</TD>
			</TR>
			<TR>
				<TD height=25>Payment Type :*</TD>
				<TD><asp:DropDownList width=100% id=ddlPayType autopostback=false runat=server onSelectedIndexChanged=onSelect_PayType>
						<asp:ListItem value="0">Cheque</asp:ListItem>
						<asp:ListItem value="1">Cash</asp:ListItem>
						<asp:ListItem value="2" Selected>Need Verification</asp:ListItem>
						<asp:ListItem value="3">Bilyet Giro</asp:ListItem>
						<asp:ListItem value="4">Others</asp:ListItem>
					</asp:DropDownList>
				    <asp:Label id=lblErrPayType forecolor=red visible=false text="Please select Payment Type"  runat=server/></TD>
				<TD>&nbsp;</TD>
                <TD>Last Update :</TD>
				<TD><asp:Label ID=lblLastUpdate runat=server /></TD>
				<TD width="5%">&nbsp;</TD>
			</TR>
			
			<tr>
			    <TD>Bilyet Giro Date :</TD>
				<td><asp:TextBox id=txtGiroDate width=50% maxlength="10" runat="server"/>
					<a href="javascript:PopCal('txtGiroDate');"><asp:Image id="btnGiroDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
					<asp:label id=lblDateGiro Text ="<br>Date Entered should be in the format " forecolor=red Visible = false Runat="server"/> 
					<asp:label id=lblFmtGiro  forecolor=red Visible = false Runat="server"/> 
				</td>
				<TD>&nbsp;</TD>
				<TD>Print Date :</TD>
				<TD><asp:Label ID=lblPrintDate runat=server /></TD>
				<TD width="5%">&nbsp;</TD>
			</tr>
			<TR>
				<TD height=25>Bank Code *:</TD>
				<TD><asp:DropDownList width=100% id=ddlBank autopostback=true onSelectedIndexChanged=onSelect_Bank runat=server />
					<asp:Label id=lblErrBank forecolor=red visible=false text="Please select Bank Code"  runat=server/></TD>
				<TD>&nbsp;</TD>
				<TD>Cheque Print Date :</TD>
				<TD><asp:Label ID=lblChequePrintDate runat=server /></TD>
				<TD width="5%">&nbsp;</TD>
			</TR>
			<TR>
				<TD height=25>Cheque/Bilyet Giro No. :</TD>
				<TD><asp:Textbox id=txtChequeNo width=100% maxlength=32 runat=server />
					<asp:Label id=lblErrCheque forecolor=red visible=false text="Please enter Cheque No." runat=server/></TD>
				<TD>&nbsp;</TD>
				<TD>Updated By :</TD>
				<TD><asp:Label ID=lblUpdatedBy runat=server /></TD>
				<TD width="5%">&nbsp;</TD>
			</TR>
			<TR>
				<TD height=25>Bank Account No. :</TD>
				<TD><asp:DropDownList width=100% id=ddlSplBankAccNo AutoPostBack=true OnSelectedIndexChanged=onSelect_SplBankAccNo runat=server /> 
				    <asp:Textbox id=txtBankAccNo Visible=false width=0% maxlength=32 runat=server />
				    <asp:Label id=lblErrBankAccNo forecolor=red visible=false text="Please enter Bank Account No." runat=server/></TD>
				<TD>&nbsp;</TD>
				<TD><asp:Label id=lblTaxStatus Text = "Tax Status :" runat=server /></TD>
				<TD><asp:Label ID=lblTaxStatusDesc runat=server /></TD>
			</TR>
			<tr>
			    <td height="25">Currency Code :</td>
				<td><asp:DropDownList id=ddlCurrCode width=100% AutoPostBack="True" onSelectedIndexChanged="CurrCodeChanged" runat=server/></td>
				<td>&nbsp;</td>
				<TD><asp:Label id=lblTaxUpdate Text = "Tax Updated By :" runat=server /></TD>
				<TD><asp:Label ID=lblTaxUpdateDesc runat=server /></TD>
			</tr>
			<tr>
			    <td height="25">Exchange Rate :</td>
				<td><asp:TextBox id=txtExchangeRate text="1" width=30% maxlength=20 runat=server />
				    <asp:Label id=lblErrExchangeRate text="Exchange rate for this date has not been created." forecolor=red visible=false runat=server />
				</td>
				<td>&nbsp;</td>
			    <td>&nbsp;</td>
			</tr>				
			<TR>
				<td height="25">
				<TD>&nbsp;</TD>
				<TD>&nbsp;</TD>
				<TD>&nbsp;</TD>
				<TD>&nbsp;</TD>
				<TD>&nbsp;</TD>
			</TR>
			<!--<TR>
				<TD height="25" colspan="5">&nbsp;</TD>
			</TR>-->
			<TR>
				<TD height="25" colspan="5">
					<asp:Label id="lblErrPrintCheque" forecolor="red" visible="false" text="Cheque format not found! Please check your Bank Details again." runat="server"/>
				</TD>
			</TR>
			<TR>
				<TD colSpan="6">
					<TABLE id="tblSelection" cellSpacing="0" cellPadding="4" width="100%" border="0" runat=server>
						<TR class="mb-c">
							<TD height=25 width=20%><asp:label id=lblInvoiceRcvIdTag runat=server/> :</TD>
							<TD colSpan="5"><asp:DropDownList id=ddlInvoiceRcv width=100% autopostback=true onSelectedIndexChanged=onSelect_InvRcv runat=server />
							                <asp:Label id=lblFindINVPOPPH23 forecolor=red visible=false text="This Credited Invoice/Purchase Order has PPH23." runat=server/>
							                <asp:Label id=lblFindINVPOPPH21 forecolor=red visible=false text="This Credited Invoice/Purchase Order has PPH21." runat=server/>
							</TD>
						</TR>
						<TR class="mb-c">
							<TD height=25>Debit Note ID :</TD>
							<TD colSpan="5"><asp:DropDownList width=100% id=ddlDebitNote runat=server autopostback=true onSelectedIndexChanged=onSelect_DbtNote /></TD>
						</TR>
						<TR class="mb-c">
							<TD height=25>Credit Note ID :</TD>
							<TD colSpan="5"><asp:DropDownList width=100% id=ddlCreditNote runat=server autopostback=true onSelectedIndexChanged=onSelect_CrNote /></TD>
						</TR>
						<tr class="mb-c">
							<td height="25">Creditor Journal ID :</td>
							<td colSpan="5">
								<asp:DropDownList width="100%" id="ddlCreditorJournal" runat="server" autopostback=true onSelectedIndexChanged=onSelect_CrJrn ></asp:DropDownList>
								<input type="hidden" id="hidCreditJrnValue" runat="server">
							</td>
						</tr>
						<TR class="mb-c">
							<TD height=25>Other :</TD>
							<TD colSpan="5"><asp:DropDownList width="90%" id=ddlOther runat="server" autopostback="true" onSelectedIndexChanged=onSelect_Other />
							<input type="button" id=btnFind2 value=" ... " onclick="javascript:PopCOA('frmMain', '', 'ddlOther', 'True');" runat=server/>
							</TD>
						</TR>
						<TR class="mb-c">
							<TD height=25>PO ID (Advance Payment) :</TD>
							<TD colSpan="5"><asp:DropDownList width="100%" id=ddlPOID runat="server" autopostback=true onSelectedIndexChanged=onSelect_PO />
							    <asp:Label id=lblErrNoSelectDoc forecolor=red visible=false text="Please select one document." runat=server/>
								<asp:Label id=lblErrManySelectDoc forecolor=red visible=false text="Please select ONLY one document." runat=server/>
								<asp:Label id=lblErrValidPPNHRate forecolor=red visible=false text="Please select invoice with the same PPN and PPH 23/26 Rate." runat=server/>
							    <asp:Label id=lblErrOtherDoc forecolor=red visible=false text="Please select invoice before other payment." runat=server/>
							    <asp:Label id=lblFindPOPPH23 forecolor=red visible=false text="This Credited Invoice/Purchase Order has PPH23." runat=server/>
							    <asp:Label id=lblFindPOPPH21 forecolor=red visible=false text="This Credited Invoice/Purchase Order has PPH21." runat=server/>
							</TD>
						</TR>
						<TR class="mb-c">
							<TD height=25><asp:label id=lblAccount runat=server /> (CR) :* </TD>
							<TD colSpan="5"><asp:Dropdownlist width=90% id=ddlAccCode runat=server /> 
								<input type="button" id=btnFind1 value=" ... " onclick="javascript:PopCOA('frmMain', '', 'ddlAccCode', 'True');" runat=server/>
								<asp:Label id=lblErrAccCode visible=false forecolor=red runat=server/>
							</TD>
						</TR>
						<tr class="mb-c">
						    <td height=25>Currency Code :</td>
	                        <td colSpan="5"><asp:DropDownList id=ddlCurrency width=100% AutoPostBack=true OnSelectedIndexChanged=CurrencyChanged runat=server/>
	                        <asp:Label id=lblErrCurrencyCode text="" forecolor=red visible=false runat=server /></td>	
						</tr>
						<tr class="mb-c">
						    <td height=25>Exchange Rate :</td>
	                        <td colSpan="5"><asp:TextBox id=txtExRate text="1" width=22% maxlength=20 runat=server />
	                        <asp:Label id=lblErrExRate text="Exchange rate for this date has not been created." forecolor=red visible=false runat=server /></td>
	                    </tr>
	                    <tr id="RowTax" visible=false class="mb-c">
							<td height=25>Tax Object :</td>
							<td colSpan="5"><asp:DropDownList id="lstTaxObject" Width=100% AutoPostBack=True OnSelectedIndexChanged=lstTaxObject_OnSelectedIndexChanged runat=server />
									  <asp:label id=lblTaxObjectErr Visible=False forecolor=red Runat="server" />
						    </td>
						</tr>
						<tr id="RowTaxAmt" visible=false class="mb-c">
						    <td height=25 width="20%">DPP Amount : </td>
						    <td width="30%"><asp:Textbox id="txtDPPAmount"  Width=60% maxlength=22 OnKeyUp="javascript:calTaxPrice();" runat=server />
								<asp:RegularExpressionValidator 
									id="RegularExpressionValidator2" 
									ControlToValidate="txtDPPAmount"
									ValidationExpression="^(\-|)\d{1,19}(\.\d{1,2}|\.|)$"
									Display="Dynamic"
									text = "<BR>Maximum length 19 digits and 2 decimal points"
									runat="server"/>
								<asp:Label id=lblErrAmountDPP visible=false forecolor=red text="<BR>Please enter DPP amount" runat=server/>
						    </td>
						    <TD width="5%"></td>
							<TD width="15%"></TD>	
							<TD width="10%"></td>
							<TD width="30%"></td>
						</tr>
						<TR class="mb-c">
							<TD height=25 width="20%">Amount :*</TD>
							<TD width="30%"><asp:TextBox id=txtAmount width=60% maxlength=22 runat=server />
								<asp:RegularExpressionValidator id=ValidateAmount 
									ControlToValidate="txtAmount"
									ValidationExpression="^(\-|)\d{1,19}(\.\d{1,2}|\.|)$"
									Display="Dynamic"
									text = "Maximum length 19 digits and 2 decimal points. "
									runat="server"/>
								<asp:Label id=lblErrReqAmount visible=false forecolor=red text="Please enter amount." runat=server />
								<asp:Label id=lblErrNegAmt visible="false" forecolor="red" text="Please enter a negative amount." runat="server" />
								<asp:Label id=lblErrPosAmt visible="false" forecolor="red" text="Please enter a positive amount." runat="server" />
								<asp:Label id=lblErrExceeded visible="false" forecolor="red" text="The payment amount is exceeded outstanding amount for document." runat="server" />
							</TD>
							<TD width="5%"><asp:Label id=lblPPN visible=false text='PPN :' runat=server/></td>
							<TD width="15%"><asp:CheckBox ID=cbPPN visible=false text=" Yes" Runat=server />
							</TD>	
							<TD width="10%"><asp:Label id=lblPPH visible=false text='PPh 23/26 Rate :' runat=server/></td>
							<TD width="30%"><asp:Textbox id=txtPPHRate visible=false width=50% maxlength=5 runat=server/>
							<asp:Label id=lblPercen visible=false text='%' runat=server/>
							</td>
						</TR>
						<tr class="mb-c">
					        <td height=25 valign=top>Additional Note :</td>
                            <td colSpan="5"><textarea rows=6 id=txtAddNote cols=60 runat=server></textarea></td>
					    </tr>
						<TR class="mb-c">
							<TD height="25" colspan="6">
								<asp:ImageButton id="AddDtlBtn" imageurl="../../images/butt_add.gif" alternatetext="Add" onclick="AddBtn_Click"  UseSubmitBehavior="false" runat="server" /> &nbsp;
							    <asp:ImageButton  id="SaveDtlBtn" visible=false ImageURL="../../images/butt_save.gif" OnClick="AddBtn_Click" Runat="server" />
							</TD>
						</TR>
					</TABLE>
				</TD>
			</TR>
			<TR>
				<TD colspan=6>
					<asp:Label id=lblErrConfirmNotFulFil visible=false forecolor=red runat=server/>
					<asp:Label id=lblErrConfirmNotFulFilText visible=false text="The payment amount is exceeded outstanding amount for document " runat=server/>
				</TD>
			</TR>
			<TR>
				<TD colSpan="6" >
					<asp:DataGrid id=dgLineDet
						AutoGenerateColumns=false width="100%" runat=server
						GridLines=none
						Cellpadding=2
						OnDeleteCommand=DEDR_Delete 
						OnEditCommand=DEDR_Edit
						Pagerstyle-Visible=False
						AllowSorting="True">
						
						<HeaderStyle CssClass="mr-h"/>
						<ItemStyle CssClass="mr-l"/>
						<AlternatingItemStyle CssClass="mr-r"/>
						<Columns>						
							<asp:TemplateColumn ItemStyle-Width="15%" HeaderText="Document ID">
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("DocId") %> id="lblDocId" runat="server" /><br />
									<asp:label text= '<%# Container.DataItem("TaxObject") %>' id="lblTaxObject" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>							
							<asp:TemplateColumn ItemStyle-Width="10%" HeaderText="Document Type">
								<ItemTemplate>
									<asp:Label Text=<%# objCBTrx.mtdGetPaymentDocType(Container.DataItem("DocType")) %> id="lblDocType" runat="server" />
									<asp:label text=<%#Container.DataItem("DocType")%> id="lblEnumDocType" visible=false runat=server />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn ItemStyle-Width="10%">
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("AccCode") %> id="lblAccCode" runat="server" />
									<asp:Label Text=<%# Container.DataItem("CBCurrencyCode") %> id="lblCurrCode" visible=false runat="server" />
									<asp:Label Text=<%# Container.DataItem("CBExchangeRate") %> id="lblExRate" visible=false runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn ItemStyle-Width="17%" HeaderText="COA Descr">
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("AccDescr") %> id="lblAccDescr" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn ItemStyle-Width="15%" HeaderText="Additional Note">
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("AdditionalNote") %> id="lblAddNote" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="" ItemStyle-Width="1%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
								<ItemTemplate>
									
									<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("NetAmountToDisplay"), 2), 2) %> id="lblViewNetAmount" visible=false runat="server" />
									<asp:Label Text=<%# FormatNumber(Container.DataItem("NetAmount"), 2) %> id="lblNetAmount" visible = False runat="server" />
									
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="" ItemStyle-Width="1%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
								<ItemTemplate> 
									<ItemStyle />
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PPNAmountToDisplay"), 2), 2) %> id="lblIDPPNAmount" visible=false runat="server" />
										<asp:Label Text=<%# FormatNumber(Container.DataItem("PPNAmount"), 2) %> id="lblPPNAmount" visible = False runat="server" />
									</ItemStyle>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="" ItemStyle-Width="1%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
								<ItemTemplate> 
									<ItemStyle />
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PPHAmountToDisplay"), 2), 2) %> id="lblIDPPHAmount" visible=false runat="server" />
										<asp:Label Text=<%# FormatNumber(Container.DataItem("PPHAmount"), 2) %> id="lblPPHAmount" visible = False runat="server" />
									</ItemStyle>
								</ItemTemplate>
							</asp:TemplateColumn>	
							<asp:TemplateColumn HeaderText="Total Amount" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
								<ItemTemplate>
									 
									<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("AmountToDisplay"), 2), 2) %> id="lblViewAmount" runat="server" />
									<asp:Label Text= <%# FormatNumber(Container.DataItem("AmountToDisplay"), 2) %> id="lblAmountToDisplay" visible=false runat="server" />
									<asp:Label Text=<%# FormatNumber(Container.DataItem("Amount"), 2) %> id="lblAmount" visible = False runat="server" />
									
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn ItemStyle-Width="5%" ItemStyle-HorizontalAlign=Right>
								<ItemTemplate>
									<asp:label id=cnlnid visible="false" text=<%# Container.DataItem("PaymentLnId")%> runat="server" />
									<asp:label text= '<%# Container.DataItem("TaxLnID") %>' Visible=False id="lblTaxLnID" runat="server" />
							        <asp:label text= '<%# Container.DataItem("TaxRate") %>' Visible=False id="lblTaxRate" runat="server" />
							        <asp:label text= '<%# Container.DataItem("DPPAmount") %>' Visible=False id="lblDPPAmount" runat="server" />
							        <asp:label text= '<%# Container.DataItem("PO_PPH23") %>' Visible=False id="lblPO_PPH23" runat="server" />
							        <asp:label text= '<%# Container.DataItem("PO_PPH21") %>' Visible=False id="lblPO_PPH21" runat="server" />
									<asp:LinkButton id=lbEdit CommandName="Edit" Text="Edit" CausesValidation =False Visible=false runat="server" />
									<asp:LinkButton id=lbDelete CommandName=Delete Text=Delete Visible=false runat=server />
								</ItemTemplate>
							</asp:TemplateColumn>	
						</Columns>
					</asp:DataGrid>
				</TD>
			</TR>
			<tr>
				<td colspan=3>&nbsp;</td>
				<td colspan=2 height=25><hr size="1" noshade></td>
				<TD>&nbsp;</TD>
			</tr>		
			<TR>
				<td colspan=3>&nbsp;</td>
				<TD height=25>Total Payment Amount : </TD>
				<TD Align=right><asp:Label ID=lblCurrency Runat=server />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID=lblViewTotalPaymentAmount Runat=server />&nbsp;</TD>
				<TD Align=right><asp:Label ID=lblTotalPaymentAmount Visible = False Runat=server />&nbsp;</TD>
				<TD>&nbsp;</TD>
			</TR>			
			<tr>
			    <td colspan=3>&nbsp;</td>
			    <TD>&nbsp;</TD>
				<TD Align=right>&nbsp;&nbsp;&nbsp;<asp:Label ID=lblShowTotalAmount Visible = False Runat=server />&nbsp;</TD>
				<TD>&nbsp;</TD>
			</tr>
			<TR>
				<TD height=25>Remarks :</TD>
				<TD colspan="5"><asp:TextBox ID=txtRemark maxlength=256 width=100% Runat=server /></TD>
			</TR>
			<TR>
				<TD colSpan="6">&nbsp;</TD>
			</TR>			
			<TR>
				<TD colSpan="6">
				    <asp:ImageButton id=NewBtn onClick=NewBtn_Click imageurl="../../images/butt_new.gif" AlternateText="New" runat=server/>
					<asp:ImageButton ID=SaveBtn onclick=SaveBtn_Click ImageUrl="../../images/butt_save.gif" AlternateText=Save Runat=server /> 
					<!--
					<asp:ImageButton ID=RefreshBtn CausesValidation=False onclick=RefreshBtn_Click ImageUrl="../../images/butt_refresh.gif" AlternateText=Refresh Runat=server />
					-->
					<asp:ImageButton ID=VerifiedBtn UseSubmitBehavior="false" onclick=VerifiedBtn_Click ImageUrl="../../images/butt_verified.gif" AlternateText=Verified Runat=server />
					<asp:ImageButton ID=ConfirmBtn UseSubmitBehavior="false" onclick=ConfirmBtn_Click ImageUrl="../../images/butt_confirm.gif" AlternateText=Confirm Runat=server />
					<asp:ImageButton ID=ForwardBtn UseSubmitBehavior="false" CausesValidation=False onclick=ForwardBtn_Click ImageUrl="../../images/butt_move_forward.gif" AlternateText="Move Forward" Runat=server />
					<asp:ImageButton ID=DeleteBtn UseSubmitBehavior="false" onclick=DeleteBtn_Click CausesValidation=false ImageUrl="../../images/butt_delete.gif" AlternateText=Delete Runat=server />
					<asp:ImageButton ID=UnDeleteBtn UseSubmitBehavior="false" onclick=UnDeleteBtn_Click ImageUrl="../../images/butt_undelete.gif" AlternateText=Undelete Runat=server />
					<asp:ImageButton ID=EditBtn UseSubmitBehavior="false" onClick=EditBtn_Click ImageUrl="../../images/butt_edit.gif" AlternateText=Edit CausesValidation=False runat="server" />
					<asp:ImageButton ID=BackBtn UseSubmitBehavior="false" CausesValidation=False onclick=BackBtn_Click ImageUrl="../../images/butt_back.gif" AlternateText=Back Runat=server />
					<Input type=hidden id=payid value="" runat=server />
				</TD>
			</TR>
			<TR>
				<TD colSpan="6">
			        <asp:ImageButton ID=PrintBtn UseSubmitBehavior="false" onclick=btnPreview_Click CausesValidation=false ImageUrl="../../images/butt_print.gif" AlternateText=Print Runat=server />
					<asp:ImageButton ID=PrintChequeBtn UseSubmitBehavior="false" onclick=btnPreviewCheque_Click CausesValidation=false ImageUrl="../../images/butt_print_cheque.gif" AlternateText="Print Cheque" Runat=server />
					<asp:ImageButton ID=PrintSlipBtn UseSubmitBehavior="false" onclick=btnPreviewSlip_Click CausesValidation=false ImageUrl="../../images/butt_print_slip.gif" AlternateText="Print Slip Setoran" Runat=server />
					<asp:ImageButton ID=PrintTransferBtn UseSubmitBehavior="false" onclick=btnPreviewTransfer_Click CausesValidation=false ImageUrl="../../images/butt_print_slip_transfer.gif" AlternateText="Print Slip Transfer" Runat=server />
				</TD>
			</TR>
			<asp:label id=lblPayType visible=false text=0 runat=server />
			<asp:label id=lblCode visible=false text=" Code" runat=server />
			<asp:label id=lblPleaseSelect visible=false text="Please select " runat=server />
			<asp:label id=lblPleaseSelectOne visible=false text="Please select one " runat=server />
			<asp:label id=lblID visible=false text=" ID" runat=server/>
			<asp:label id=lblInvoiceRcvTag visible=false runat=server/>
			<asp:Label id=lblPPHRateHidden visible=false runat=server />
			<asp:Label id=lblPPNHidden visible=false runat=server />
			<asp:Label id=lblStatusHidden visible=false runat=server />
			<asp:label id=lblProgramPath visible=false runat=server />
			<asp:Label id=lblInvTypeHidden visible=false runat=server />
			<asp:label id=lblCurrentPeriod visible=false runat=server />
			<asp:label id=lblTxLnID visible=false runat=server />
			
			
			<tr>
				<td colspan=5>&nbsp;</td>
			</tr>
			<tr id=TrLink runat=server>
				<td colspan=5>
					<asp:LinkButton id=lbViewJournal text="View Journal Predictions" causesvalidation=false runat=server /> 
				</td>
			</tr>
			<tr>
				<td colspan=5>&nbsp;</td>
			</tr>
			<tr>
				<td colspan=6>
					<asp:DataGrid id=dgViewJournal
						AutoGenerateColumns="false" width="100%" runat="server"
						GridLines=none
						Cellpadding="1"
						Pagerstyle-Visible="False"
						AllowSorting="false">	
						<HeaderStyle CssClass="mr-h"/>
						<ItemStyle CssClass="mr-l"/>
						<AlternatingItemStyle CssClass="mr-r"/>
						<Columns>
							<asp:TemplateColumn HeaderText="COA Code">
							    <ItemStyle Width="20%" /> 
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("ActCode") %> id="lblCOACode" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Description">
							     <ItemStyle Width="40%" /> 
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("Description") %> id="lblCOADescr" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Debet">
							    <HeaderStyle HorizontalAlign="Right" /> 
								<ItemStyle HorizontalAlign="Right" Width="20%" /> 
							    <ItemTemplate>
								    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("AmountDB"), 2), 2) %> id="lblAmountDB" runat="server" />
							    </ItemTemplate>
						    </asp:TemplateColumn>									
						    <asp:TemplateColumn HeaderText="Credit">
						        <HeaderStyle HorizontalAlign="Right" /> 
								<ItemStyle HorizontalAlign="Right" Width="20%" /> 
								<ItemTemplate>
								    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("AmountCR"), 2), 2) %> id="lblAmountCR" runat="server" />
							    </ItemTemplate>
						    </asp:TemplateColumn>		
						    <asp:TemplateColumn>		
								<ItemStyle HorizontalAlign="Right" /> 									
								<ItemTemplate>
									
								</ItemTemplate>
							</asp:TemplateColumn>							
						</Columns>
					</asp:DataGrid>
				</td>
			</tr>	
			<tr>
				<td colspan=5>&nbsp;</td>
			</tr>
			<tr>
			    <td>&nbsp;</td>								
			    <td height=25 align=right><asp:Label id=lblTotalViewJournal Visible=false runat=server /> </td>
			    <td>&nbsp;</td>	
			    <td align=right><asp:label id="lblTotalDB" text="0" Visible=false runat="server" /></td>						
			    <td>&nbsp;</td>		
			    <td align=right><asp:label id="lblTotalCR" text="0" Visible=false runat="server" /></td>				
		    </tr>
		</TABLE>
		<Input type=hidden id=hidBlockCharge value="" runat=server/>
		<Input type=hidden id=hidChargeLocCode value="" runat=server/>
		<Input type=hidden id=HidPOCurrency value="IDR" runat=server/>
		<Input type=hidden id=hidPOExRate value="1" runat=server/>
		<Input type=hidden id=HidInvAmount value="1" runat=server/>
		<Input type=hidden id=hidOutstandingAmount value="0" runat=server/>
		<Input type=hidden id=hidOutstandingAmountKonversi value="0" runat=server/>
		<Input type=hidden id=hidUserID value="" runat=server/>
		<Input type=hidden id=hidNPWPNo value="" runat=server />
		<Input type=hidden id=hidTaxObjectRate value=0 runat=server />
		<Input type=hidden id=hidCOATax value=0 runat=server />
		<Input type=hidden id=hidFindPOPPH23 value=0 runat=server />
		<Input type=hidden id=hidPOPPH23 value=0 runat=server />
		<Input type=hidden id=hidTaxStatus value=1 runat=server />
		<Input type=hidden id=hidHadCOATax value=0 runat=server />
		<Input type=hidden id=hidDocID value="" runat=server />
		<Input type=hidden id=hidFindPOPPH21 value=0 runat=server />
		<Input type=hidden id=hidPOPPH21 value=0 runat=server />
		
		<Input type=hidden id=hidPrevID value="" runat=server />
		<Input type=hidden id=hidPrevDate value=0 runat=server />
		<Input type=hidden id=hidNextID value="" runat=server />
		<Input type=hidden id=hidNextDate value=0 runat=server />
		</form>
	</body>
</html>