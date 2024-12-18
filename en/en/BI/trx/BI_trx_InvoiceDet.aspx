<%@ Page Language="vb" src="../../../include/BI_trx_InvoiceDet.aspx.vb" Inherits="BI_trx_InvoiceDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuBI" src="../../menu/menu_bitrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<html>
	<head>
		<title>Invoice Details</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<script language="javascript">
			function calAmount(i) {
				var doc = document.frmMain;
				var a = parseFloat(doc.txtTotalUnits.value);
				var b = parseFloat(doc.txtRate.value);
				var c = parseFloat(doc.txtAmount.value);

				if ((i == '1') || (i == '2')) {
					if ((doc.txtTotalUnits.value != '') && (doc.txtRate.value != ''))
						doc.txtAmount.value = round(a * b, 2);
					else
						doc.txtAmount.value = '';
				}
				
				if (i == '3') {
					if ((doc.txtTotalUnits.value != '') && (doc.txtAmount.value != ''))
						doc.txtRate.value = round(c / a, 2);
					else
						doc.txtRate.value = '';
				}

				if (doc.txtTotalUnits.value == 'NaN')
					doc.txtTotalUnits.value = '';

				if (doc.txtRate.value == 'NaN')
					doc.txtRate.value = '';
					
				if (doc.txtAmount.value == 'NaN')
					doc.txtAmount.value = '';
			}
		</script>		
	</head>
	<body>
		<form id=frmMain runat=server>
		<table id="tblHeader" cellSpacing="0" cellPadding="2" width="100%" border="0">
			<tr>
				<td colspan="6">
					<UserControl:MenuBI id=MenuBI runat="server" />
				</td>
			</tr>
			<tr>
				<td class="mt-h" colspan="6">INVOICE DETAILS</td>
			</tr>
			<tr>
				<td colspan=6><hr size="1" noshade></td>
			</tr>
			<tr>
				<td height=25 width="20%">Invoice ID :</td>
				<td width="40%"><asp:Label id=lblInvoiceID runat=server /></td>
				<td width="5%">&nbsp;</td>
				<td width="15%">Period :</td>
				<td width="20%"><asp:Label id=lblAccPeriod runat=server /></td>
				<td width="5%">&nbsp;</td>
			</tr>
			<tr>
			    <td height=25>Transaction Date :</td>
				<td><asp:TextBox id=txtDateCreated width=25% maxlength="10" runat="server"/>
					<a href="javascript:PopCal('txtDateCreated');"><asp:Image id="btnDateCreated" ImageAlign=AbsMiddle runat="server" ImageUrl="../../Images/calendar.gif"/></a>
					<asp:RequiredFieldValidator	id="rfvDateCreated" runat="server"  ControlToValidate="txtDateCreated" text = "Please enter Date Created" display="dynamic"/>
					<asp:label id=lblDate Text ="<br>Date Entered should be in the format " forecolor=red Visible = false Runat="server"/> 
					<asp:label id=lblFmt  forecolor=red Visible = false Runat="server"/> 
				</td>
				<td>&nbsp;</td>
				<td>Status :</td>
				<td><asp:Label id=lblStatus runat=server /></td>
			</tr>
			<tr>
				<td height=25><asp:label id="lblBillParty" runat="server" /> :*</td>
				<td><asp:DropDownList id=ddlBillParty width=100% autopostback=true onselectedindexchanged=onChanged_BillParty runat=server/>
					<asp:Label id=lblErrBillParty visible=false forecolor=red runat=server/></td>
				<td>&nbsp;</td>
				<td>Date Created :</td>
				<td><asp:Label id=lblDateCreated runat=server /></td>
			</tr>
			<tr>
				<td height=25><asp:Label id=lblDocType Text="Billing Type " runat=server/>:*</td>
				<td><asp:Dropdownlist id=ddlDocType width=100% runat=server/></td>
				<td>&nbsp;</td>
				<td>Last Update :</td>
				<td><asp:Label ID=lblLastUpdate runat=server /></td>
			</tr>
			<tr>
				<td height=25>Customer Reference :</td>
				<td><asp:Textbox id=txtCustRef width=100% maxlength=256 runat=server/>
				<asp:Label id=lblCRErrRefNo visible=false text="Customer Reference is not unique" forecolor=red runat=server/></td>
				<td>&nbsp;</td>
				<td>Print Date :</td>
				<td><asp:Label ID=lblPrintDate runat=server /></td>
			</tr>
			<tr>
				<td height=25>Delivery Reference :</td>
				<td><asp:Textbox id=txtDevRef width=100% maxlength=128 runat=server/>
				<asp:Label id=lblDLErrRefNo visible=false text="Delivery Reference is not unique" forecolor=red runat=server/></td>
				<td>&nbsp;</td>
				<td>Updated By :</td>
				<td><asp:Label ID=lblUpdatedBy runat=server /></td>
			</tr>
			<tr>
			    <td height=25>Seller :</td>
				<td><asp:Textbox id=txtSeller width=100% maxlength=128 runat=server/>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25>Contract No. :</td>
                <td><asp:DropDownList id=ddlContract width=100% autopostback=true onselectedindexchanged=onChanged_ContractNo runat=server/></td>	
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25>Currency Code :</td>
                <td><asp:DropDownList id=ddlCurrency width=100% runat=server/></td>	
				<td>&nbsp;</td>
			</tr>
			<tr>
			    <td height=25>Exchange Rate :</td>
                <td><asp:TextBox id=txtExRate text="1" width=20% maxlength=20 runat=server /></td>				
            </tr>
            <tr>
			    <td height=25>Advance Payment (excl. vat) :</td>
                <td><asp:TextBox id=txtAdvAmount text="0" width=50% style="text-align:right" runat=server />
                    <asp:RegularExpressionValidator id=RegularExpressionValidator1 												
						ControlToValidate="txtAdvAmount"												
						ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
						Display="Dynamic"
						text="Maximum length 19 digits and 2 decimal points. "
						runat="server"/>
					<asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" 
						display="dynamic" 
						ControlToValidate="txtAdvAmount"
						Text="Please enter advance amount" />
                </td>				
            </tr>
			<tr>
			    <td height=25>VAT Exempted</td>
				<td><asp:CheckBox id=chkVATExempted text="" runat=server/></td>
				<td>&nbsp;</td>
			</tr>
			
            <tr>
				<td colspan=6 style="height: 23px">&nbsp;</td>
			</tr>
			<tr>
				<td colSpan="6">
					<!--<table width="100%" class="mb-c" cellspacing="0" cellpadding="6" border="0" align="center">
						<tr>						
							<td>-->
								<table id="tblSelection" cellSpacing="0" cellPadding="4" width="100%" border="0" runat=server>
									<tr class="mb-c">
										<td style="height: 28px"><asp:label id=lblAccount runat=server /> :* </td>
										<td colSpan="5" style="height: 28px">
                                            <asp:TextBox ID="txtAccCode" runat="server" AutoPostBack="True" MaxLength="15" OnTextChanged="onSelect_StrAccCode"
                                                Width="23%"></asp:TextBox>
                                            <input type="button" id=btnFind1 value=" ... " onclick="javascript:PopCOA_Desc('frmMain', '', 'txtAccCode', 'txtAccName', 'False');" runat=server/>
                                            <asp:Button ID="CoaChangeButton2" runat="server" Font-Bold="True" OnClick="onSelect_Account"
                                                Text="Get" ToolTip="Click For Refresh COA " Width="40px" />
                                            <asp:TextBox ID="txtAccName" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Bold="True" ForeColor="White" MaxLength="10" Width="56%"></asp:TextBox><br />
											<asp:Label id=lblErrAccCode visible=false forecolor=red runat=server/>
										</td>
									</tr>
									<tr id="RowChargeLevel" class="mb-c">
										<td height="25">Charge Level :* </td>
										<td colspan=5><asp:DropDownList id="ddlChargeLevel" Width=95% AutoPostBack=True OnSelectedIndexChanged=ddlChargeLevel_OnSelectedIndexChanged runat=server /> </td>
									</tr>
									<tr id="RowPreBlk" class="mb-c">
										<td height="25"><asp:label id=lblPreBlkTag Runat="server"/> </td>
										<td colspan=5><asp:DropDownList id="ddlPreBlock" Width=95% runat=server />
											<asp:label id=lblPreBlockErr Visible=False forecolor=red Runat="server" /></td>
									</tr>
									<tr id="RowBlk" class="mb-c">
										<td height=25><asp:label id="lblBlock" runat="server" /> :</td>
										<td colspan=5>
											<asp:DropDownList id=ddlBlock width=95% runat=server/>
											<asp:Label id=lblErrBlock visible=false forecolor=red runat=server/>
										</td>
									</tr>
									<tr class="mb-c">
										<td height=25><asp:label id="lblVehicle" runat="server" /> :</td>
										<td colspan=5>
											<asp:Dropdownlist id=ddlVehCode width=95% runat=server/>
											<asp:Label id=lblErrVehicle visible=false forecolor=red runat=server/>
										</td>
									</tr>
									<tr class="mb-c">
										<td height=25><asp:label id="lblVehExpense" runat="server" /> :</td>
										<td colspan=5>
											<asp:Dropdownlist id=ddlVehExpCode width=95% runat=server/>
											<asp:Label id=lblErrVehicleExp visible=false forecolor=red runat=server/>
										</td>
									</tr>
									<tr class="mb-c">
										<td height=25>Item Description :*</td>
										<td colspan=5>
											<asp:Textbox id=txtDescription width=95% maxlength=128 runat=server />
											<asp:Label id=lblErrDesc visible=false forecolor=red text="Please enter Item description." runat=server/>
										</td>
									</tr>
									
									<tr class="mb-c">
										<td height=25 width=20%>Quantity :*</td>
										<td width=20%>
											<asp:Textbox id=txtTotalUnits OnKeyUp="javascript:calAmount('1');" width=70% maxlength=15 runat=server/>
											<asp:CompareValidator id="cvTotalUnits" display=dynamic runat="server" 
												ControlToValidate="txtTotalUnits" Text="<br>The value must whole number or with decimal. " 
												Type="Double" Operator="DataTypeCheck"/>
											<asp:RegularExpressionValidator id=revTotalUnits 
												ControlToValidate="txtTotalUnits"
												ValidationExpression="^(\-|)\d{1,9}(\.\d{1,5}|\.|)$"
												Display="Dynamic"
												text = "Maximum length 9 digits and 5 decimal points. "
												runat="server"/>
											<asp:Label id=lblErrTotalUnits visible=false forecolor=red text="Please enter Quantity" runat=server/>
										</td>
										<td height=25 width=10% align=center> X Unit Price *</td>
										<td width=20%>
											<asp:Textbox id=txtRate width=50% OnKeyUp="javascript:calAmount('2');" maxlength=22 runat=server/>
											<asp:CompareValidator id="cvRate" display=dynamic runat="server" 
												ControlToValidate="txtRate" Text="<br>The value must whole number or with decimal. " 
												Type="Double" Operator="DataTypeCheck"/>												
											<asp:RegularExpressionValidator id=revRate 
												ControlToValidate="txtRate"
												ValidationExpression="^(\-|)\d{1,16}(\.\d{1,5}|\.|)$"
												Display="Dynamic"
												text = "Maximum length 16 digits and 5 decimal points. "
												runat="server"/>
											<asp:Label id=lblErrRate visible=false forecolor=red text="Please enter Rate" runat=server/>
										</td>
										<td height=25 width=10% align=center> = Amount *</td>
										<td width=20%>
											<asp:Textbox id=txtAmount width=80% OnKeyUp="javascript:calAmount('3');" maxlength=22 runat=server/>
											<asp:CompareValidator id="cvAmount" display=dynamic runat="server" 
												ControlToValidate="txtAmount" Text="<br>The value must whole number or with decimal" 
												Type="Double" Operator="DataTypeCheck"/>
											<asp:RegularExpressionValidator id=revAmount 
												ControlToValidate="txtAmount"
												ValidationExpression="^(\-|)\d{1,19}(\.\d{1,2}|\.|)$"
												Display="Dynamic"
												text = "Maximum length 19 digits and 2 decimal points. "
												runat="server"/>
											<asp:Label id=lblErrAmount visible=false forecolor=red text="Please enter Amount" runat=server/>
										</td>
									</tr>
									<tr class="mb-c">
										<td height=25>PPN :</td>
										<td><asp:CheckBox ID=cbPPN text=" Yes"  OnCheckedChanged=reCalculate_Amount  AutoPostBack=true Runat=server /></td>
										<td>PPh 23/26 Rate :</td>
										<td><asp:Textbox id=txtPPHRate width=50% maxlength=5 runat=server/>
										<asp:Label id=lblPercen text='%' runat=server/>
										<asp:CompareValidator id="cvPPHRate" display=dynamic runat="server" 
												ControlToValidate="txtPPHRate" Text="<br>The value must whole number or with decimal. " 
												Type="Double" Operator="DataTypeCheck"/>
										<asp:RegularExpressionValidator id=revPPHRate 
												ControlToValidate="txtPPHRate"
												ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
												Display="Dynamic"
												text = "<br>Maximum length 2 digits and 2 decimal points. "
												runat="server"/>
										</td>
										<td>&nbsp;</td>	
										<td>&nbsp;</td>	
									</tr>
									<tr class="mb-c">
										<td height=25 colspan=6>
											<asp:ImageButton id=AddBtn imageurl="../../images/butt_add.gif" alternatetext=Add onclick=AddBtn_Click runat=server /> 
										</td>
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
						AutoGenerateColumns=false width="100%" runat=server
						GridLines=none
						Cellpadding=2
						OnDeleteCommand=DEDR_Delete 
						Pagerstyle-Visible=False
						AllowSorting="True">
						
						<HeaderStyle CssClass="mr-h"/>
						<ItemStyle CssClass="mr-l"/>
						<AlternatingItemStyle CssClass="mr-r"/>
						<Columns>						
							<asp:TemplateColumn ItemStyle-Width=10%>
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("AccCode") %> id="lblAccCode" runat="server" />
									<asp:Label visible=false Text=<%# Container.DataItem("InvoiceLnID") %> id="lblInvoiceLnID" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Acc Description" ItemStyle-Width=15%>
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("AccDescr") %> id="lblAccDescr" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn ItemStyle-Width=10%>
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("BlkCode") %> id="lblBlkCode" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Item Description" ItemStyle-Width=15%>
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("Description") %> id="lblDescription" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Quantity" ItemStyle-Width=8% HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
								<ItemTemplate> 
									<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Unit"), 2),2) %> id="lblViewUnit" runat="server" />
									<asp:Label Text=<%# Container.DataItem("Unit") %> id="lblUnit" visible = False runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Unit Price" ItemStyle-Width=8% HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
								<ItemTemplate> 
									<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("CostToDisplay"), 2), 2) %> id="lblViewCost" runat="server" />
									<asp:Label Text=<%# Container.DataItem("Cost") %> id="lblCost" Visible = False runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Amount" ItemStyle-Width=10% HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
								<ItemTemplate> 
									<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("NetAmountToDisplay"), 2), 2) %> id="lblViewNetAmount" runat="server" />
									<asp:Label Text=<%# Container.DataItem("NetAmount") %> id="lblNetAmount" visible = False runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="PPN" ItemStyle-Width=8% HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
								<ItemTemplate> 
									<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PPNAmountToDisplay"), 2), 2) %> id="lblViewPPNAmount" runat="server" />
									<asp:Label Text=<%# FormatNumber(Container.DataItem("PPNAmount"), 2) %> id="lblPPNAmount" visible = False runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="PPH 23/26" ItemStyle-Width=8% HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
								<ItemTemplate> 
									<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PPHAmountToDisplay"), 2), 2) %> id="lblViewPPHAmount" runat="server" />
									<asp:Label Text=<%# FormatNumber(Container.DataItem("PPHAmount"), 2) %> id="lblPPHAmount" visible = False runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Total Amount" ItemStyle-Width=8% HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
								<ItemTemplate> 
									<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("AmountToDisplay"), 2), 2) %> id="lblViewAmount" runat="server" />
									<asp:Label Text=<%# FormatNumber(Container.DataItem("Amount"), 2) %> id="lblAmount" visible = False runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn ItemStyle-Width=5% ItemStyle-HorizontalAlign=Right>
								<ItemTemplate>
									<asp:label id=IVlnid visible="false" text=<%# Container.DataItem("InvoiceLNID")%> runat="server" />
									<asp:LinkButton id=lbEdit CommandName=Edit Text=Edit runat=server />
									<asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server />
									<asp:LinkButton id=lbCancel CommandName="Cancel" Text="Cancel" CausesValidation=False  runat="server"/>
									<asp:Label Text=<%# Container.DataItem("VehCode") %> id="lblVehCode" Visible=false runat="server" />
									<asp:Label Text=<%# Container.DataItem("VehExpenseCode") %> id="lblVehExpenseCode" Visible=false runat="server" />
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
				<td height=25>Total Amount : </td>
				<td Align=right><asp:Label ID=lblCurrency Runat=server />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID=lblViewTotalAmount Runat=server />&nbsp;</td>
				<td Align=right><asp:Label ID=lblTotalAmount Visible = False Runat=server />&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td heigth=25>Remarks:</td>
				<td colSpan="5"><asp:TextBox ID=txtRemark maxlength=256 width=100% Runat=server /></td>
			</tr>
			
			<tr>
			    <td colspan=6>&nbsp;</td>
		    </tr>
			<tr>
				<td height=25 valign=top>Undersigned Name :*</td>
				<td><asp:Textbox id=txtUnderName Text="" maxlength=128 width=50% runat=server/></td>	
				<td>&nbsp;</td>
			</tr>	
			<tr>
				<td height=25 valign=top>Undersigned Post :*</td>
				<td><asp:Textbox id=txtUnderPost text="" maxlength=128 width=50% runat=server/></td>		
				<td>&nbsp;</td>
			</tr>	
			<tr>
			    <td colspan=6>&nbsp;</td>
		    </tr>
			<tr visible=False>
				<td height=25 valign=top>Faktur Pajak No. :*</td>
				<td><asp:Textbox id=txtFakturNo Text="" maxlength=128 width=50% ReadOnly=true runat=server/></td>	
				<td>&nbsp;</td>
			</tr>	
			<tr visible=False>
				<td height=25 valign=top>Faktur Pajak Date :*</td>
				<td><asp:TextBox id=txtFakturDate width=25% maxlength="10" ReadOnly=true runat="server"/>
					<a href="javascript:PopCal('txtFakturDate');"><asp:Image id="Image1" ImageAlign=AbsMiddle runat="server" ImageUrl="../../Images/calendar.gif"/></a>
					<asp:label id=lblDateFaktur Text ="<br>Date Entered should be in the format " forecolor=red Visible = false Runat="server"/> 
					<asp:label id=lblFmtFaktur  forecolor=red Visible = false Runat="server"/> 
				</td>		
				<td>&nbsp;</td>
			</tr>	
			<tr visible=False>
			    <td> </td>
			    <td colspan=6><asp:RadioButton id="Opt1" Enabled=false runat="server" TextAlign="Right" Text=" Lembar ke-1   :   Untuk pembeli BKP/penerima JKP sebagai bukti pajak masukan" GroupName="Option" Checked="True"/><br>
                    <asp:RadioButton id="Opt2" Enabled=false runat="server" TextAlign="Right" Text=" Lembar ke-2   :   Untuk PKP yang menerbitkan Faktur Pajak Standar sebagai bukti pajak keluaran" GroupName="Option"/><br>
                    <asp:RadioButton id="Opt3" Enabled=false runat="server" TextAlign="Right" Text=" Lembar ke-3   :   Arsip" GroupName="Option"/></td>
			    <td>&nbsp;</td>				    
			</tr>
			<tr>
				<td colspan=6>&nbsp;</td>
			</tr>
			<tr>
                <td colspan="3">
                <asp:CheckBox id="cbClosed" text=" Set Closed" checked="false" AutoPostBack=true OnCheckedChanged="Closed_Changed" runat="server" /></td>
            </tr>
			<tr>
                <td colspan="3">
                <asp:CheckBox id="cbExcel" text=" Export To Excel" checked="false" runat="server" /></td>
            </tr>
            <tr>
				<td colspan=6>&nbsp;</td>
			</tr>			
			<tr>
				<td colSpan="6">
					<asp:Label id=lblErrTotal forecolor=red visible=false text="There was no amount to bill.<br>" runat=server/>
					<asp:ImageButton ID=SaveBtn onclick=SaveBtn_Click ImageUrl="../../images/butt_save.gif" AlternateText=Save Runat=server /> 
					<asp:ImageButton ID=ConfirmBtn onclick=ConfirmBtn_Click ImageUrl="../../images/butt_confirm.gif" AlternateText=Confirm Runat=server />
					<asp:ImageButton ID=PrintBtn onclick=PrintBtn_Click ImageUrl="../../images/butt_print.gif" AlternateText=Print Runat=server />
					<asp:ImageButton ID=PrintFaktur onclick=PrintFaktur_Click ImageUrl="../../images/butt_print_faktur2.gif" AlternateText=Print Runat=server />
					<asp:ImageButton ID=PrintKwitansiBtn Visible=false UseSubmitBehavior="false" onclick=PreviewKwitansiBtn_Click CausesValidation=false ImageUrl="../../images/butt_print_kwitansi.gif" AlternateText="Print Kwitansi" Runat=server />
					<asp:ImageButton ID="EditBtn" UseSubmitBehavior="false" AlternateText="Edit"        onClick="EditBtn_Click"       ImageUrl="../../images/butt_edit.gif" CausesValidation=False runat="server" />
					<asp:ImageButton ID=CancelBtn onclick=CancelBtn_Click ImageUrl="../../images/butt_cancel.gif" Text=" Cancel " Runat=server />
					<asp:ImageButton ID=DeleteBtn CausesValidation=False onclick=DeleteBtn_Click ImageUrl="../../images/butt_delete.gif" AlternateText=Delete Runat=server />
					<asp:ImageButton ID=UnDeleteBtn onclick=UnDeleteBtn_Click ImageUrl="../../images/butt_undelete.gif" AlternateText=Undelete Runat=server />
					<asp:ImageButton ID=BackBtn CausesValidation=False onclick=BackBtn_Click ImageUrl="../../images/butt_back.gif" AlternateText=Back Runat=server />
					<Input type=hidden id=IVid value="" runat=server />
					<Input type=hidden id=hidPPN value="" runat=server />
					
				</td>
			</tr>
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:Label id=lblCode visible=false text=" Code" runat=server />
			<asp:Label id=lblPleaseSelect visible=false text="Please select " runat=server />
			<asp:Label id=lblSelect visible=false text="Select " runat=server />
			<asp:Label id=lblStatusHidden visible=false runat=server />
			<asp:Label id=lblDocTypeHidden visible=false runat=server />
			<asp:Label id=lblVehicleOption visible=false text=false runat=server/>
			<asp:Label id=lblReferer visible=false text="" runat=server/>
			
			
				
			<tr>
				<td colspan=5>&nbsp;</td>
			</tr>
			<tr id=TrLink visible=false runat=server>
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

		</table>
			<Input type=hidden id=hidBlockCharge value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>
			<Input type=hidden id=hidProdCode value="" runat=server/>
			<Input type=hidden id=hidProdType value="" runat=server/>
			<Input type=hidden id=hidIRLnID value="" runat=server />
			<Input type=hidden id=hidPPNValue value=0 runat=server/>
			<Input type=hidden id=hidReceiptID value="" runat=server />
		</form>
	</body>
</html>
