<%@ Page Language="vb" src="../../../include/ap_trx_InvRcvDet.aspx.vb" Inherits="ap_trx_InvRcvDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuAP" src="../../menu/menu_aptrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<html>
	<head>
		<title>Invoice Receive Details</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<script language="javascript">
			
			function ChkUnmatchUnitCost() {
				var doc = document.frmMain;
				var arrUnitCost, sngUnitCost, sngEnterUnitCost;
				var strDisplay = "none";
				//var x = doc.txtCost.value;
				//sngEnterUnitCost = parseFloat(x.toString().replace(/,/gi,""));
				
				if (doc.lstItem.selectedIndex > 0 && doc.rbSPO.checked) {
				    arrUnitCost = doc.lstItem.options[doc.lstItem.selectedIndex].text.split("Rp. ");
				    sngUnitCost = parseFloat(arrUnitCost[1]);
				    if(doc.txtCost.value.replace(" ", "") != "") {
				        sngEnterUnitCost = parseFloat(doc.txtCost.value);
				        if(sngUnitCost != sngEnterUnitCost) {
				            strDisplay = "";
				        }
				    }
				}
				errUnmatchCost.style.display = strDisplay;
				
				//unitcostArr = Split(doc.lstItem.SelectedItem.Text, " $");
				//SelPOUnitCost = parseFloat(Trim(unitcostArr(1)));

				//If (parseFloat(Trim(doc.txtCost.Text)) <> SelPOUnitCost) 
				//doc.errUnmatchCost.Visible = True;
			}
			function calAdjAmount() {
				var doc = document.frmMain;
				var a = doc.hidOutPayAmount.value;
				var b = doc.txtUsedDNCNAmount.value;
				var y = parseFloat(a.toString().replace(/,/gi,""));
				var z = parseFloat(b.toString().replace(/,/gi,""));
				var dbAmt = y + z;
				if (doc.txtUsedDNCNAmount.value == 'NaN')
				    doc.txtOutPayAmount.value = doc.txtUsedDNCNAmount.value;
				else
				    doc.txtOutPayAmount.value = round(dbAmt, 2);
			}
			
			function lostFocusAdjAmount() {
			    var doc = document.frmMain;
			    var x = doc.txtUsedDNCNAmount.value;
			    var dbAmt = parseFloat(x.toString().replace(/,/gi,""));
			    doc.txtUsedDNCNAmount.value = round(dbAmt, 2);
	        }
			function lostFocusSplAmt() {
			    var doc = document.frmMain;
			    var x = doc.txtSplInvAmt.value;
			    var dbAmt = parseFloat(x.toString().replace(/,/gi,""));
			    doc.txtSplInvAmt.value = round(dbAmt, 2);
	        }
		    function lostFocusUnitCost() {
			    var doc = document.frmMain;
			    var x = doc.txtCost.value;
			    var dbAmt = parseFloat(x.toString().replace(/,/gi,""));
			    doc.txtCost.value = round(dbAmt, 2);
	        }
	        
	        function toCurrency(num) {
              num = num.toString().replace(/\$|\,/g, '')
              if (isNaN(num)) num = "0";
              sign = (num == (num = Math.abs(num)));
              num = Math.floor(num * 100 + 0.50000000001);
              cents = num % 100;
              num = Math.floor(num / 100).toString();
              if (cents < 10) cents = '0' + cents;

              for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++) {
                num = num.substring(0, num.length - (4 * i + 3)) + ',' + num.substring(num.length - (4 * i + 3))
              }

              return (((sign) ? '' : '-') + num + '.' + cents)
            }
			
			function FormatCurrency(objNum)
               {
                    var num = objNum.value.replace('$','');
                    var ent, dec, dot;
                    if (num != '' && num != objNum.oldvalue)
                    {
                         num = MoneyToNumber(num);
                         if (!isNaN(num))
                         {
                              var ev = (navigator.appName.indexOf('Netscape') != -1)?Event:event;
                        ent = num.split('.')[0];
                        dec = num.split('.')[1];
                        if (dec || ev.keyCode == 190)
                        {
                             dot = '.';
                             if (dec.toString().length > 2) dec = dec.toString().substr(0,2);
                        }
                        else
                        {
                             dec = '';
                          dot = '';
                        }
                              objNum.value = AddCommas(ent) + dot + dec;
                              objNum.oldvalue = objNum.value;
                         }
                      objNum.value = '' + objNum.oldvalue;
                    }
               }
             
                function MoneyToNumber(num)
               {
                    return (num.replace(/,/g, ''));
             
               }
             
                function AddCommas(num)
               {
                    numArr=new String(num).split('').reverse();
                    for (i=3;i<numArr.length;i+=3)
                    {
                         numArr[i]+=',';
                    }
                    return numArr.reverse().join('');
               }
             
              function number_onblur(objNum)
              {
                   var num = objNum.oldvalue;
                if (num.charAt(num.toString().length-1) == '.') num = num.replace('.','');
                objNum.value = "" + num;
              }
              
              function calTransDate() {
			    var doc = document.frmMain;
			    doc.txtInvoiceRcvRefDate.value = doc.txtTransDate.value;
	        }
	</script>		
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
				<td><asp:TextBox ID=txtTransDate maxlength=10 width=50% OnKeyUp ="javascript:calTransDate();" Runat=server />
					<a href="javascript:PopCal('txtTransDate');"><asp:Image id="btnSelTransDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
					<asp:Label id=lblErrTransDate forecolor=red text="Date format " runat=server />
					<asp:label id=lblFmtTransDate  forecolor=red Visible = false Runat="server"/> 
				</td>
				<td>&nbsp;</td>
				<td>Status :</td>
				<td style="width: 278px"><asp:Label id=lblStatus runat=server /></td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25><asp:label id=lblInvoiceRcvRefNoTag runat=server/> :* </td>
				<td>
					<asp:TextBox ID=txtInvoiceRcvRefNo maxlength=32 width=85% Runat=server />
					<asp:Label id=lblErrInvoiceRcvRefNo visible=false forecolor=red text="Please key in a reference number" runat=server/>
					<asp:Label id=lblUnqErrInvRcvRefNo visible=false text="Ref. No is not unique" forecolor=red runat=server/>
				</td>
				<td>&nbsp;</td>
				<td height=25>Date Created : </td>
				<td style="width: 278px"><asp:Label id=lblDateCreated runat=server /></td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25><asp:label id=lblInvoiceRcvRefDateTag runat=server/> :</td>
				<td><asp:TextBox ID=txtInvoiceRcvRefDate maxlength=10 width=50% Runat=server />
					<a href="javascript:PopCal('txtInvoiceRcvRefDate');"><asp:Image id="btnSelDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
					<asp:Label id=lblErrInvRcvRefDate forecolor=red text="Date format " runat=server />
					<asp:label id=lblFmtInvRcvRefDate  forecolor=red Visible = false Runat="server"/> 
				</td>
				<td>&nbsp;</td>
				<td>Last Update :</td>
				<td style="width: 278px"><asp:Label ID=lblLastUpdate runat=server /></td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25>Invoice Type :*</td>
				<td><asp:RadioButton id=rbSPO text="" checked=true autopostback=true oncheckedchanged=InvoiceType_OnCheckChange groupname="InvoiceType" runat=server/></td>
				<td>&nbsp;</td>
				<td>Print Date:</td>
				<td style="width: 278px"><asp:Label ID=lblPrintDate runat=server /></td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25>&nbsp;</td>
				<td><asp:RadioButton id=rbCWO text="" autopostback=true oncheckedchanged=InvoiceType_OnCheckChange groupname="InvoiceType" runat=server/></td>
				<td>&nbsp;</td>
				<td>Updated By : </td>
				<td style="width: 278px"><asp:Label ID=lblUpdatedBy runat=server /></td>
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
			    <td height=25>Purchase Order ID :*</td>
				<td><asp:DropDownList id=ddlPO width=85%  autopostback=true onSelectedIndexChanged=onSelect_Change runat=server />
				    <input type=button value=" ... " id="Find" onclick="javascript:PopPO('frmMain', '', 'ddlPO', 'True');" CausesValidation=False runat=server />
				    <asp:ImageButton ImageAlign=AbsBottom ID=btnAddAllItem UseSubmitBehavior="false" onclick=BtnAddAllItem_Click CausesValidation=False ImageUrl="../../images/icn_next.gif" AlternateText="Add all items" Runat=server />  
					<asp:Label id=lblErrPO visible=false forecolor=red text="Please select Purchase Order ID" runat=server/>
				</td>
				<td>&nbsp;</td>
				
			</tr>			
			<tr>
				<td height=25>Supplier Code :*</td>
				<td><!-- <asp:Label ID=lblSupplier runat=server /> -->
					<asp:DropDownList id=ddlSuppCode width=85% autopostback=true onSelectedIndexChanged=onSelect_Supp runat=server />
					<asp:Label id=lblErrSuppCode visible=false forecolor=red text="Please select Supplier Code" runat=server/>
				</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
			    <td height=25>Credit Term : </td>
				<td><asp:TextBox ID=txtCreditTerm maxlength=3 width=10% Runat=server /> Days
					<asp:CompareValidator id="validateCreditTerm" display=dynamic runat="server" 
						ControlToValidate="txtCreditTerm" Text="<br>The value must whole number. " 
						Type="integer" Operator="DataTypeCheck"/>				
				</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
			    <td height=25>Credit Term Type :</td>
				<td><asp:DropDownList id="ddlTermType" width="50%" runat="server" /></td>
				<td>&nbsp;</td>
			</tr>
			<tr>
			    <td height=25>Credited Invoice Due Date : </td>
				<td><asp:TextBox ID=txtInvDueDate maxlength=10 width=50% Runat=server /> 
				    <asp:Label id=lblerrInvDueDate forecolor=red text="Date format " runat=server />
				    <asp:label id=lblFmtInvDueDate  forecolor=red Visible = false Runat="server"/>
				</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
			    <td height=25>Supplier Invoice Amount : </td>
				<td><asp:TextBox ID=txtSplInvAmt maxlength=10 width=50% text=0 Runat=server /> 
				    <asp:RegularExpressionValidator id=RegularExpressionValidator1 												
						ControlToValidate="txtSplInvAmt"												
						ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"
						Display="Dynamic"
						text="Maximum length 9 digits and 5 decimal points. "
						runat="server"/>
						<comment>End Modified</comment>
					<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" 
						display="dynamic" 
						ControlToValidate="txtSplInvAmt"
						Text="Please enter cost" />
					<asp:Label id=lblErrSplInvAmt visible=false forecolor=red text="Please enter supplier invoice amount." runat=server />
				</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
	            <td height=25>Advance Payment Amount : </td>
	            <td><asp:Label ID=lblCurrency4 Runat=server />&nbsp;&nbsp;
	                <asp:Label id=lblAdvPaymentAmount runat=server /></td>
		    </tr>	
		    <tr>
			    <td height=25>DN/CN Amount : </td>
                <td><asp:Label ID=lblCurrency5 Runat=server />&nbsp;&nbsp;
                    <asp:Label ID=lblDNCNAmount runat=server /></td>
			</tr>
			<tr>
				<td height=25></td>
				<td>
					<asp:DropDownList id=ddlTTRefNo width=100% visible=false runat=server />
				</td>
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
										<td width="20%" height=25><asp:label id=lblItemCode text="Item Code :*" Runat="server" /></td>
										<td colSpan="5"><asp:DropDownList id="lstItem" Width=90% runat=server EnableViewState=True onSelectedIndexChanged=ItemIndexChanged autopostback=true onchange="javascript:ChkUnmatchUnitCost();" />
													  <asp:RequiredFieldValidator id=validateItem display=dynamic runat=server 
														text='<br>Please select Item Code' ControlToValidate=lstItem />
											<asp:label id=lblItemCodeErr text="Please select Item Code" Visible=False forecolor=red Runat="server" />
										</td>						
									</tr>
									<tr class="mb-c">
										<td style="height: 28px"><asp:label id=lblAccount runat=server /> (DR) :* </td>
										<td colSpan="5" style="height: 28px"><asp:DropDownList id=ddlAccCode width=90% onSelectedIndexChanged=onSelect_Account autopostback=true runat=server /> 
										    <input type="button" id=btnFind1 value=" ... " onclick="javascript:PopCOA('frmMain', '', 'ddlAccCode', 'True');" runat=server/>
											<asp:Label id=lblErrAccCode visible=false forecolor=red runat=server/>
										</td>
									</tr>
									<tr id="RowChargeLevel" class="mb-c">
										<td height="25">Charge Level :* </td>
										<td colSpan="5"><asp:DropDownList id="ddlChargeLevel" Width=90% AutoPostBack=True OnSelectedIndexChanged=ddlChargeLevel_OnSelectedIndexChanged runat=server /> </td>
									</tr>
									<tr id="RowPreBlk" class="mb-c">
										<td height="25"><asp:label id=lblPreBlkTag Runat="server"/> </td>
										<td colSpan="5"><asp:DropDownList id="ddlPreBlock" Width=90% runat=server />
											<asp:label id=lblPreBlockErr Visible=False forecolor=red Runat="server" /></td>
									</tr>			
									<tr id="RowBlk" class="mb-c">
										<td height=25><asp:label id=lblBlock runat=server /> :</td>
										<td colSpan="5"><asp:DropDownList id=ddlBlock width=90% runat=server/>
											<asp:Label id=lblErrBlock visible=false forecolor=red runat=server/>
										</td>
									</tr>
									<tr class="mb-c">
										<td height=25><asp:label id=lblVehicle runat=server /> :</td>
										<td colSpan="5"><asp:Dropdownlist id=ddlVehCode width=90% runat=server/>
											<asp:Label id=lblErrVehicle visible=false forecolor=red runat=server/>
										</td>
									</tr>
									<tr class="mb-c">
										<td height=25><asp:label id=lblVehExpense runat=server /> :</td>
										<td colSpan="5"><asp:Dropdownlist id=ddlVehExpCode width=90% runat=server/>
											<asp:Label id=lblErrVehicleExp visible=false forecolor=red runat=server/>
										</td>
									</tr>
									<tr class="mb-c">
										<td width="20%" height=25>Invoice Quantity :*</td>
										<td width="30%"><asp:TextBox id=txtQty width=100% maxlength=22 runat=server />
											<asp:RegularExpressionValidator id=ValidateQty
												ControlToValidate="txtQty"
												ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"
												Display="Dynamic"
												text="Maximum length 9 digits and 5 decimal points. "
												runat="server"/>
											<asp:RequiredFieldValidator id="rfvQty" runat="server" 
												display="dynamic" 
												ControlToValidate="txtQty"
												Text="Please enter quantity." />
											<asp:Label id=errReqQty visible=false forecolor=red text="Please enter Quantity" runat=server />
											<asp:Label id=errOverQty visible=false forecolor=red text="Quantity Invoiced is more than uninvoiced item." runat=server />
										</td>
										<td width="5%">&nbsp;</td>
										<td width="15%"><asp:Label id=lblPPN visible=false text='PPN :' runat=server/></td>
										<td width="25%"><asp:CheckBox ID=cbPPN visible=false text=" Yes" Runat=server />
										</td>	
										<td width="5%">&nbsp;</td>				
									</tr>
									<tr class="mb-c">
										<td height=25>Invoice Unit Cost :*</td>
										<td><asp:TextBox id=txtCost width=100% maxlength=21 runat=server OnKeyUp ="javascript:ChkUnmatchUnitCost();"  />
											<comment>Modified By BHL</comment>
											<asp:RegularExpressionValidator id=ValidateCost 												
												ControlToValidate="txtCost"												
												ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"
									            Display="Dynamic"
									            text = "Only number and 2 decimal points"
												runat="server"/>
												<comment>End Modified</comment>
											<asp:RequiredFieldValidator id="rfvCost" runat="server" 
												display="dynamic" 
												ControlToValidate="txtCost"
												Text="Please enter cost" />
											<asp:Label id=errReqCost visible=false forecolor=red text="Please enter Cost." runat=server />
											<span id=errUnmatchCost style="display:none;color:red;">Note: Invoice Unit Cost is different with Unit Cost in Purchase Order.</span>
											<asp:Label id=errUnmatchCost Visible=false forecolor=red Text="Note: Invoice Unit Cost is different with Unit Cost in Purchase Order." runat=server />
										</td>
										<td>&nbsp;</td>
										<td height=25><asp:Label id=lblPPH visible=false text='PPh 22/23/26 Rate :' runat=server/></td>
										<td><asp:Textbox id=txtPPHRate visible=false width=50% maxlength=5 runat=server/>
										<asp:Label id=lblPercen visible=false text='%' runat=server/>
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
									</tr>
									
									<tr class="mb-c">
										<td height=25>PBB-KB : </td>
										<td><asp:TextBox id=txtPBBKB width=100% maxlength=21 runat=server />
										    <asp:RegularExpressionValidator id="RegularExpressionValidatorPBBKB"
												ControlToValidate="txtPBBKB"												
												ValidationExpression="\d{1,19}\.\d{0,2}|\d{1,19}"
												Display="Dynamic"
												text="Maximum length 21 digits and 2 decimal points."
												runat="server"/>
												<comment>End Modified</comment>
											<asp:RequiredFieldValidator id="validatePBBKB" runat="server" 
												display="dynamic" 
												ControlToValidate="txtPBBKB"
												Text="Please Specify PBB-KB" />
										</td>
										<td>&nbsp;</td>
										<td><asp:Label id=LblAmtTransportFee visible=false text='Ongkos Angkut :' runat=server/></td>
										<td><asp:TextBox id=txtAmtTransportFee visible=false width=50% maxlength=21 runat=server /></td>
										<td>&nbsp;</td>
									</tr>
									<tr class="mb-c">
										<td height=25>DO No. :</td>
										<td><asp:TextBox id=txtDONo width=100% maxlength=20 runat=server /></td>
										<td>&nbsp;</td>
										<td>&nbsp;</td>
										<td>&nbsp;</td>
										<td>&nbsp;</td>
									</tr>
									<tr class="mb-c">
										<td height=25>Other Invoice No. :</td>
										<td><asp:TextBox id=txtOtherInvNo width=100% maxlength=20 runat=server /></td>
										<td>&nbsp;</td>
										<td>&nbsp;</td>
										<td>&nbsp;</td>
										<td>&nbsp;</td>
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
						AllowSorting="True">
						<HeaderStyle CssClass="mr-h"/>
						<ItemStyle CssClass="mr-l"/>
						<AlternatingItemStyle CssClass="mr-r"/>
						<Columns>	
							<asp:TemplateColumn HeaderText="Item" ItemStyle-Width="15%">
								<ItemTemplate>
									<asp:Label visible=false Text=<%# Container.DataItem("InvoiceRcvLnID") %> id="lblInvoiceLnID" runat="server" />
									<asp:Label visible=false Text=<%# Container.DataItem("ItemCode") %> id="lblItemCode" runat="server" />
									<asp:Label Text=<%# Container.DataItem("_Description") %> id="lblItemDesc" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>							
							<asp:TemplateColumn ItemStyle-Width="10%">
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("Acccode") %> id="lblAccCode" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn ItemStyle-Width="9%">
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("BlkCode") %> id="lblBlkCode" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn ItemStyle-Width="8%">
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("VehCode") %> id="lblVehCode" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn ItemStyle-Width="8%">
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("VehexpenseCode") %> id="lblVehExpenseCode" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="DO No." ItemStyle-Width="8%">
								<ItemTemplate> 
									<ItemStyle />
										<asp:Label Text=<%# Container.DataItem("DONo") %> id="lblDONo" runat="server" />
									</ItemStyle>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Other Invoice No." ItemStyle-Width="8%">
								<ItemTemplate> 
									<ItemStyle />
										<asp:Label Text=<%# Container.DataItem("OtherInvNo") %> id="lblOtherInvNo" runat="server" />
									</ItemStyle>
								</ItemTemplate>
							</asp:TemplateColumn>							
							<asp:TemplateColumn HeaderText="Purchase UOM" ItemStyle-Width="5%">
								<ItemTemplate> 
									<asp:Label Text=<%# Container.DataItem("UOMCode") %> id="lblUOMCode" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>					
							<asp:TemplateColumn HeaderText="Quantity Received" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right >
								<ItemTemplate>
									<comment>Modified by BHL</comment>									
									<asp:Label Text=<%# iif(trim(Container.DataItem("QtyReceive")) ="",Container.DataItem("QtyReceive"), objGlobal.GetIDDecimalSeparator_FreeDigit(iif(isnumeric(Container.DataItem("QtyReceive")),Container.DataItem("QtyReceive"),0), 5)) %> 
									id="lblIDQtyReceived" runat="server" />
									<asp:Label Text=<%# Container.DataItem("QtyReceive") %> id="lblQtyReceived" visible = False runat="server" />																
								</ItemTemplate>
							</asp:TemplateColumn>						
							<asp:TemplateColumn HeaderText="Quantity Invoice" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right >
								<ItemTemplate>
									<comment>Modified By BHL</comment>
									<asp:Label id=txtIDQtyInvoiced runat="server"										
										Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("QtyInvoice"),5),5) %>'
										width=100% />
									<asp:Label id=txtQtyInvoiced visible = False runat="server"										
										Text='<%# Container.DataItem("QtyInvoice") %>'
										width=100% />
								</ItemTemplate>							
							</asp:TemplateColumn>						
							<asp:TemplateColumn HeaderText="Unit Price" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right >
								<ItemTemplate>
									<comment>Modified By BHL</comment>									
									<asp:Label id=txtIDUnitPrice runat="server"									              
										Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("CostToDisplay"), 2), 2) %>'
										width=100% />
									<asp:Label id=txtUnitPrice visible = False runat="server"
										Text='<%# FormatNumber(Container.DataItem("CostToDisplay"), 2) %>'
										width=100% />
								</ItemTemplate>							
							</asp:TemplateColumn>									
							<asp:TemplateColumn HeaderText="Amount" HeaderStyle-HorizontalAlign=Right ItemStyle-Width="8%" ItemStyle-HorizontalAlign=Right>
								<ItemTemplate> 
									<ItemStyle />
										<comment>Modified By BHL </comment>
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("NetAmountToDisplay"), 2), 2) %> id="lblIDNetAmount" runat="server" />
										<asp:Label Text=<%# FormatNumber(Container.DataItem("NetAmount"), 2) %> id="lblNetAmount" visible = False runat="server" />
									</ItemStyle>
								</ItemTemplate>
							</asp:TemplateColumn>	
							<asp:TemplateColumn HeaderText="PPN" HeaderStyle-HorizontalAlign=Right ItemStyle-Width="8%" ItemStyle-HorizontalAlign=Right>
								<ItemTemplate> 
									<ItemStyle />
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PPNAmountToDisplay"), 2), 2) %> id="lblIDPPNAmount" runat="server" />
										<asp:Label Text=<%# FormatNumber(Container.DataItem("PPNAmount"), 2) %> id="lblPPNAmount" visible = False runat="server" />
									</ItemStyle>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="PPH 23/26" HeaderStyle-HorizontalAlign=Right ItemStyle-Width="8%" ItemStyle-HorizontalAlign=Right>
								<ItemTemplate> 
									<ItemStyle />
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PPHAmountToDisplay"), 2), 2) %> id="lblIDPPHAmount" runat="server" />
										<asp:Label Text=<%# FormatNumber(Container.DataItem("PPHAmount"), 2) %> id="lblPPHAmount" visible = False runat="server" />
									</ItemStyle>
								</ItemTemplate>
							</asp:TemplateColumn>	
							<asp:TemplateColumn HeaderText="Total Amount" HeaderStyle-HorizontalAlign=Right ItemStyle-Width="8%" ItemStyle-HorizontalAlign=Right>
								<ItemTemplate> 
									<ItemStyle />
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("AmountToDisplay"), 2), 2) %> id="lblIDAmount" runat="server" />
										<asp:Label Text=<%# FormatNumber(Container.DataItem("Amount"), 2) %> id="lblAmount" visible = False runat="server" />
									</ItemStyle>
								</ItemTemplate>
							</asp:TemplateColumn>				
							<asp:TemplateColumn ItemStyle-Width="5%" ItemStyle-HorizontalAlign=Right>
								<ItemTemplate>
									<asp:label id=POlnid visible="false" text=<%# Container.DataItem("POlnid")%> runat="server" />
									<asp:LinkButton id=lbDelete CommandName=Delete Text=Delete CausesValidation=False runat=server />
								</ItemTemplate>
							</asp:TemplateColumn>	
					</Columns>										
					</asp:DataGrid>
				</td>
			</tr>
			<tr>
				<td colspan=3>&nbsp;</td>
				<td colspan=3 height=25><hr size="1" noshade></td>
				<td>&nbsp;</td>
			</tr>		
			<tr>				
				<td colspan=3>&nbsp;</td>
				<td height=25>Total Invoice Amount :</td>
				<td align=middle><asp:Label ID=lblCurrency1 Runat=server /></td>
				<td align=right>
				<asp:Label ID=lblIDInvoiceAmount Runat=server />
				<asp:Label ID=lblInvoiceAmount Visible = FALSE Runat=server /></td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td colspan=3>&nbsp;</td>
				<td height=25>Used Payment Amount :</td>
				<td align=middle><asp:Label ID=lblCurrency3 Runat=server /></td>
				<td align=right>
				<asp:Label ID=lblIDUsedAdvPayAmount Runat=server />
				<asp:Label ID=lblUsedAdvPayAmount Visible = False Runat=server /></td>
				<td>&nbsp;</td>
			</tr>
			<tr id=TrAdj runat=server>
				<td colspan=3>&nbsp;</td>
				<td height=25>DN/CN Allocation Amount :</td>
				<td align=middle><asp:Label ID=lblCurrency6 Runat=server /></td>
				<td align=right>
				&nbsp;  <asp:TextBox id=txtUsedDNCNAmount text="0" style="text-align:right" OnKeyUp="javascript:calAdjAmount();" maxlength=18 runat=server />
						<asp:RegularExpressionValidator id="RegularExpressionValidator4" 
							ControlToValidate="txtUsedDNCNAmount"
							ValidationExpression="^(\-|)\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"
							Display="Dynamic"
							text = "Only number and 2 decimal points"
							runat="server"/>
						<asp:RequiredFieldValidator 
							id="RequiredFieldValidator4" 
							runat="server" 
							ErrorMessage="Please Specify Additional Discount" 
							ControlToValidate="txtUsedDNCNAmount" 
							display="dynamic"/>
				</td>
			</tr>
			<tr>
				<td colspan=3>&nbsp;</td>
				<td height=25>Outstanding Payment Amount : </td>
				<td align=middle><asp:Label ID=lblCurrency2 Runat=server /></td>
				<td align=right>
				<asp:Label ID=lblIDOutPayAmount style="text-align:right" Runat=server />
				<asp:Label ID=lblOutPayAmount Visible=False Runat=server />
				<asp:TextBox id=txtOutPayAmount text="0" style="text-align:right" ReadOnly=true Visible=false runat=server /></td>
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
					<asp:Label id=lblErrQty visible=false forecolor=red text="Quantity Invoice must not greater than quantity received." runat=server />
					<P>
					<asp:ImageButton ID="NewBtn"     UseSubmitBehavior="false" AlternateText="New"     onclick="NewBtn_Click"     ImageUrl="../../images/butt_new.gif"     CausesValidation=False Runat=server /> 
					<asp:ImageButton ID="SaveBtn"     UseSubmitBehavior="false" AlternateText="Save"     onclick="SaveBtn_Click"     ImageUrl="../../images/butt_save.gif"     CausesValidation=False Runat=server /> 
					<asp:ImageButton ID="RefreshBtn"  UseSubmitBehavior="false" AlternateText="Refresh"  onclick="RefreshBtn_Click"  ImageUrl="../../images/butt_refresh.gif"  CausesValidation=False Runat=server />
					<asp:ImageButton ID="ConfirmBtn"  UseSubmitBehavior="false" AlternateText="Confirm"  onclick="ConfirmBtn_Click"  ImageUrl="../../images/butt_confirm.gif"  CausesValidation=False Runat=server />
					<asp:ImageButton ID="PrintBtn"    UseSubmitBehavior="false" AlternateText="Print"                                ImageUrl="../../images/butt_print.gif"             visible=False Runat=server />
					<asp:ImageButton ID="CancelBtn"   UseSubmitBehavior="false" AlternateText="Cancel"   onclick="CancelBtn_Click"   ImageUrl="../../images/butt_cancel.gif"   CausesValidation=False Runat=server />
					<asp:ImageButton ID="DeleteBtn"   UseSubmitBehavior="false" AlternateText="Delete"   onclick="DeleteBtn_Click"   ImageUrl="../../images/butt_delete.gif"   CausesValidation=False Runat=server />
					<asp:ImageButton ID="UnDeleteBtn" UseSubmitBehavior="false" AlternateText="Undelete" onclick="UnDeleteBtn_Click" ImageUrl="../../images/butt_undelete.gif" CausesValidation=False Runat=server />
					<asp:ImageButton ID="EditBtn"     UseSubmitBehavior="false" AlternateText="Edit"        onClick="EditBtn_Click"       ImageUrl="../../images/butt_edit.gif" CausesValidation=False runat="server" />
					<asp:ImageButton ID="PrintDocBtn" UseSubmitBehavior="false" AlternateText="Print Cheque" onclick="PrintDocBtn_Click" ImageUrl="../../images/butt_print_doc.gif" CausesValidation=false visible=False Runat=server />
					<asp:ImageButton ID="BackBtn"     UseSubmitBehavior="false" AlternateText="Back"     onclick="BackBtn_Click"     ImageUrl="../../images/butt_back.gif"     CausesValidation=False Runat=server />
					
					<Input type=hidden id=inrid value="" runat=server />
					<Input type=hidden id=idSuppCode value="" runat=server />
					<Input type=hidden id=hidBlockCharge value="" runat=server/>
					<Input type=hidden id=hidChargeLocCode value="" runat=server/>
					<Input type=hidden id=HidCurrencyCode value="" runat=server/>
					<Input type=hidden id=HidExchangeRate value="1" runat=server/>
					<Input type=hidden id=hidTermType value="" runat=server />
					<Input type=hidden id=hidPaymentID value="" runat=server />
					<Input type=hidden id=hidAdvAmount value=0 runat=server />
					<Input type=hidden id=hidUsedAdvAmount value=0 runat=server />
					<Input type=hidden id=hidInvAmount value=0 runat=server />
					<Input type=hidden id=hidAdvExchangeRate value=0 runat=server />
					<Input type=hidden id=hidAdvPayment value=0 runat=server />
					<Input type=hidden id=hidPOCurrencyCode value="" runat=server/>
					<Input type=hidden id=hidAdvCurrencyCode value="" runat=server/>
					<Input type=hidden id=hidUnitCost value=0 runat=server/>
					<Input type=hidden id=hidOutPayAmount value=0 runat=server/>
					<Input type=hidden id=hidDNCNID value="" runat=server/>
					<Input type=hidden id=hidAPCJID value="" runat=server/>
					<Input type=hidden id=hidDNCNAmount value=0 runat=server/>
					<Input type=hidden id=hidHasPaymentID value="" runat=server />
				</td>
			</tr>
			<tr>
				<td colSpan="6">&nbsp;</td>
			</tr>	
			<tr>
				<td colspan=6>
					<asp:DataGrid id=dgAPCJ
						AutoGenerateColumns="false" width="30%" runat="server"
						GridLines=none
						Cellpadding="1"
						Pagerstyle-Visible="False"
						AllowSorting="false">	
						<HeaderStyle CssClass="mr-h"/>
						<ItemStyle CssClass="mr-l"/>
						<AlternatingItemStyle CssClass="mr-r"/>
						<Columns>
							<asp:HyperLinkColumn HeaderText="Creditor Journal ID" 
								SortExpression="CreditJrnID" 
								DataNavigateUrlField="CreditJrnID" 
								DataNavigateUrlFormatString="ap_trx_CJDet.aspx?cjid={0}"
								DataTextFormatString="{0:c}"
								DataTextField="CreditJrnID" />									
						</Columns>
					</asp:DataGrid>
				</td>
			</tr>
			<tr>
				<td colSpan="6">&nbsp;</td>
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
							<asp:HyperLinkColumn HeaderText="Tanda Terima Tagihan No." 
								SortExpression="TrxID" 
								DataNavigateUrlField="TrxID" 
								DataNavigateUrlFormatString="javascript:popwin(200, 600, 'AP_trx_PrintDocs.aspx?TrxID={0}')"
								DataTextFormatString="{0:c}"
								DataTextField="TrxID" />									
						</Columns>
					</asp:DataGrid>
				</td>
			</tr>
			
			
				
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
		</table>
		</form>
	</body>
</html>
