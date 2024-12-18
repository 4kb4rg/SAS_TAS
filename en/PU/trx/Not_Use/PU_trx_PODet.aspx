<%@ Page Language="vb" src="../../../include/PU_trx_PODet.aspx.vb" Inherits="PU_PODet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPU" src="../../menu/menu_putrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>


<html>
	<head>
		<title>Purchase Order Details</title>		
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<script language="javascript">	
	        function ChkCreditTerm() {
				var doc = document.frmMain;
				var arrSupplier, sngSupplier, sngEnterSupplier;
				var strDisplay = "none";
				//var x = doc.txtCost.value;
				//sngEnterUnitCost = parseFloat(x.toString().replace(/,/gi,""));
				
				if (doc.ddlSuppCode.selectedIndex > 0) {
				    arrSupplier = doc.ddlSuppCode.options[doc.ddlSuppCode.selectedIndex].text.split(" - ");
				    doc.txtCreditTerm.value = arrSupplier[1].replace(" days","");
				}
				
				//errUnmatchCost.style.display = strDisplay;
				
				//unitcostArr = Split(doc.lstItem.SelectedItem.Text, " $");
				//SelPOUnitCost = parseFloat(Trim(unitcostArr(1)));

				//If (parseFloat(Trim(doc.txtCost.Text)) <> SelPOUnitCost) 
				//doc.errUnmatchCost.Visible = True;
			}		
			function calUnitCost() {
				var doc = document.frmMain;
				var a = parseFloat(doc.txtQtyOrder.value);
				var b = parseFloat(doc.txtTtlCost.value);				
				doc.txtCost.value = b / a;
				if (doc.txtCost.value == 'NaN')
					doc.txtCost.value = '';
				else
					doc.txtCost.value = round(doc.txtCost.value, 2);
			}			
			function calTtlCost() {
				var doc = document.frmMain;
				var a = parseFloat(doc.txtQtyOrder.value);
				var b = parseFloat(doc.txtCost.value);
			    doc.txtTtlCost.value = (a * b);
				if (doc.txtTtlCost.value == 'NaN')
					doc.txtTtlCost.value = '';
				else
					doc.txtTtlCost.value = round(doc.txtTtlCost.value, 2);
			}
			function calTtlQtyCost() {
				var doc = document.frmMain;
				var a = parseFloat(doc.txtQtyOrder.value);
				var b = parseFloat(doc.txtCost.value);				
				doc.txtTtlCost.value = a * b;
				if (doc.txtTtlCost.value == 'NaN')
					doc.txtTtlCost.value = '';
				else
					doc.txtTtlCost.value = format_number(round(doc.txtTtlCost.value, 2),2);
			}	
			
			function calAddDiscount() {
				var doc = document.frmMain;
				var a = parseFloat(doc.hidTtlAmount.value);
				var b = parseFloat(doc.txtAddDisc.value);
				doc.txtTtlAftDisc.value = a - b;
				if (doc.txtAddDisc.value == 'NaN')
				    doc.txtTtlAftDisc.value = doc.hidTtlAmount.value
				else
				    doc.txtTtlAftDisc.value = round(doc.txtTtlAftDisc.value, 2);
			}
			function format_number(pnumber,decimals){
	            if (isNaN(pnumber)) { return 0};
	            if (pnumber=='') { return 0};
            	
	            var snum = new String(pnumber);
	            var sec = snum.split('.');
	            var whole = parseFloat(sec[0]);
	            var result = '';
            	
	            if(sec.length > 1){
		            var dec = new String(sec[1]);
		            dec = String(parseFloat(sec[1])/Math.pow(10,(dec.length - decimals)));
		            dec = String(whole + Math.round(parseFloat(dec))/Math.pow(10,decimals));
		            var dot = dec.indexOf('.');
		            if(dot == -1){
			            dec += '.'; 
			            dot = dec.indexOf('.');
		            }
		            while(dec.length <= dot + decimals) { dec += '0'; }
		            result = dec;
	            } else{
		            var dot;
		            var dec = new String(whole);
		            dec += '.';
		            dot = dec.indexOf('.');		
		            while(dec.length <= dot + decimals) { dec += '0'; }
		            result = dec;
	            }	
	            return result;
            }
            
	</script>
	<body>
		<form id=frmMain runat=server>
			<asp:Label id=lblHidStatus visible=false runat=server />
			<asp:Label id=lblHidStatusEdited visible=false runat=server />
			<asp:Label id=lblHidPOLnID visible=false runat=server />
			<asp:Label id=lblErrMessage visible=False Text="Error while initiating component." runat=server />
			<asp:label id=lblSelectListLoc visible=false text="Please Select Purchase Requisition Ref " runat="server"/>
			<asp:label id=lblSelectListItem visible=false text="Please Select " runat="server" />
			<asp:label id=lblPR visible=false text="PR " runat="server" />
			<asp:label id=lblPRRef visible=false text="PR Ref. " runat="server" />
			<input type=hidden id=hidOrgQtyOrder runat=server />
			<input type=hidden id=hidTtlAmount runat=server/>	
			<input type=hidden id=hidAddDisc runat=server/>					 					 	
            <input type=hidden id=hidTtlAmtAftDisc runat=server/>		
            <input type=hidden id=hidOriCost value=0 runat=server/>		
            <input type=hidden id=hidOriCostOA value=0 runat=server/>		
            <input type=hidden id=hidPPN value=0 runat=server/>	
            <input type=hidden id=hidPPNOA value=0 runat=server/>	            
            <input type=hidden id=hidGetRefNo value=0 runat=server/>	            
            			 				 	
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
					<td width="40%"><asp:Label id=lblPOId runat=server /></td>
					<td width="5%">&nbsp;</td>
					<td width="15%">Period :</td>
					<td width="20%"><asp:Label id=lblAccPeriod runat=server />&nbsp;</td>
					
				</tr>
				<tr>
				    <td height=25>Purchase Order Date :</td>
		            <td><asp:TextBox id=txtDateCreated width=50% maxlength="10" runat="server"/>
				        <a href="javascript:PopCal('txtDateCreated');"><asp:Image id="btnDateCreated" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
				        <asp:RequiredFieldValidator	id="rfvDateCreated" runat="server"  ControlToValidate="txtDateCreated" text = "Please enter Date Created" display="dynamic"/>
				        <asp:label id=lblDate Text ="<br>Date Entered should be in the format " forecolor=red Visible = false Runat="server"/> 
				        <asp:label id=lblFmt  forecolor=red Visible = false Runat="server"/> 
			        </td>
					<td>&nbsp;</td>
					<td>Status :</td>
					<td><asp:Label id=lblStatus runat=server /></td>
					
				</tr>	
				<tr>
					<td height="25">Purchase Order Type :</td>
					<td><asp:label id=lblPOTypeName runat=server />
						<asp:label id=lblPOType visible=false runat=server /></td>
			        <td>&nbsp;</td>
					<td>Date Created :</td>
					<td><asp:Label id=lblCreateDate runat=server /></td>
					
				</tr>			
				<tr>
				    <td height="25">Dept Code :*</td>
					<td><asp:DropDownList id="ddlDeptCode" width=100% runat=server />
						<asp:Label id=lblDeptCode text="Please Select Dept Code" forecolor=red visible=false runat=server />
					</td>								
					<td>&nbsp;</td>
					<td>Updated By :</td>
					<td><asp:Label id=lblUpdateBy runat=server /></td>	
					
				</tr>	
				<tr>
				    <td height="25">Supplier Code :*</td>
					<td><asp:DropDownList id=ddlSuppCode width=90% AutoPostBack=true OnSelectedIndexChanged="onSelect_Supp" runat=server />
						<input type=button value=" ... " id="Find" onclick="javascript:PopSupplier('frmMain', '', 'ddlSuppCode', 'True');" CausesValidation=False runat=server />
						<asp:Label id=lblSuppCode text="Please Select Supplier Code" forecolor=red visible=false runat=server />
						<asp:RequiredFieldValidator id="validateSuppCode" 
													runat="server" 
													ErrorMessage="Please Specify Supplier Code" 
													ControlToValidate="ddlSuppCode" 
													display="dynamic"/></td>	
					<td>&nbsp;</td>
					<td>Last Update :</td>
					<td><asp:Label id=lblUpdateDate runat=server /></td>			
					
				</tr>			
				<tr>
				    <td height="25">Credit Term :*</td>
					<td><asp:TextBox id="txtCreditTerm" width=30% runat=server /> Day(s)
						<asp:Label id=lblErrCreditTerm text="Credit Term cannot be empty" forecolor=red visible=false runat=server />
					</td>	
					<td>&nbsp;</td>
					<td>Print Date :</td>
					<td><asp:Label id=lblPrintDate runat=server />&nbsp;</td>
				</tr>
				<tr>
				    <td height="25">PO Issued Location :*</td>
					<td><asp:DropDownList id="ddlPOIssued" width=100% runat=server /> 
						<asp:Label id=lblPOIssued text="Please Select PO Issued Location" forecolor=red visible=false runat=server />
					</td>	
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>		
				</tr>			
				<tr><td height="25">Centralized :</td>
					<td><asp:CheckBox id="chkCentralized" Text="  No" checked=false AutoPostBack=true OnCheckedChanged=Centralized_Type runat=server /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>				
					
				</tr>			
				<tr>
				    <td height="25">Currency Code :</td>
					<td><asp:DropDownList id=ddlCurrency width=100% AutoPostBack="True" onSelectedIndexChanged="CurrencyChanged" runat=server/></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					
				</tr>
				<tr>
				    <td height="25">Exchange Rate :</td>
					<td><asp:TextBox id=txtExRate text="1" width=30% maxlength=20 runat=server />
					    <asp:Label id=lblErrExRate text="Exchange rate for this date has not been created." forecolor=red visible=false runat=server />
					</td>
					<td>&nbsp;</td>
				    <td>&nbsp;</td>
				</tr>						
				<tr>
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
											<td height="25">Purchase Requisition ID :*</td>
											<td ><asp:DropDownList id=ddlPRId width=100% runat=server AutoPostBack="True" onSelectedIndexChanged="PRIndexChanged"/>
											    <asp:Label id=lblErrDdlPRID forecolor=red text="Please select one PRID" runat=server /></td>
										</tr>
										<tr id=Centralized_No Visible = false runat="server">
											<td height="25">Purchase Requisition ID :*</td>
											<td ><asp:TextBox id=txtPRID width=50% maxlength=32 runat=server />
											     <asp:Label id=lblErrTxtPRID forecolor=red text="Please insert PRID Or Invalid PRID" runat=server /></td>
										</tr>
										<tr>
											<td height="25">Purchase Requisition <asp:label id="lblLocCode" runat="server" /> :</td>
											<td ><asp:Label id=lblPRLocCode runat=server /></td>
										</tr>
										<tr>
											<td height="25">PR/PO Ref. No. :</td>
											<td><asp:TextBox id=txtPRRefId width=50% maxlength=32 runat=server />
											    <asp:ImageButton ImageAlign=AbsBottom ID=btnGetRef onclick=GetRefNoBtn_Click CausesValidation=False ImageUrl="../../images/icn_next.gif" AlternateText="Get Data PO" Visible=false Runat=server />  
							                    <asp:label id=lblRefNoErr Visible=False forecolor=red Runat="server" /></td>
										</tr>
										<tr>
											<td height="25">Purchase Requisition Ref. <asp:label id="lblLocation" runat="server" /> :</td>
											<td><asp:DropDownList id=ddlPRRefLocCode width=100% runat=server AutoPostBack="False" onSelectedIndexChanged="LocIndexChanged" />
											     <asp:Label id=lblErrPRRef forecolor=red text="Please select PR ref. location" runat=server /></td>
											
										</tr>
										<tr>
											<td height="25">&nbsp;</td>
											<td><asp:Label id=lblErrRef forecolor=red visible=false runat=server /></td>
										</tr>	
										<tr>
											<td height="25">&nbsp;</td>
											<td ></td>
										</tr>	
										<tr>
											<td style="height: 70px"><asp:label id="lblStockItem" runat="server" /> :*</td>
											<td style="height: 70px" ><asp:DropDownList id=ddlItemCode width=95% runat=server AutoPostBack="True" onSelectedIndexChanged="ItemIndexChanged" />
											    <input type=button value=" ... " id="FindIN" Visible=false onclick="javascript:PopItem('frmMain', '', 'ddlItemCode', 'True');" CausesValidation=False runat=server />
                                                <br />
                                                <asp:TextBox ID="txtItemCode" runat="server" AutoPostBack="False" MaxLength="15"
                                                    Width="25%"></asp:TextBox>
                                                <input id="FindTxt" runat="server" causesvalidation="False"
                                                        onclick="javascript:PopItem_New('frmMain', '', 'txtItemCode','TxtItemName','txtCost', 'False');"
                                                        type="button" value=" ... " /><asp:TextBox ID="TxtItemName" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Bold="True" ForeColor="White" MaxLength="10" Width="60%"></asp:TextBox><br />
    										    <input type=button value=" ... " id="FindDC" Visible=False onclick="javascript:PopItem('frmMain', '', 'ddlItemCode', 'True');" CausesValidation=False runat=server />
    										    <input type=button value=" ... " id="FindWS" Visible=False onclick="javascript:PopItem('frmMain', '', 'ddlItemCode', 'True');" CausesValidation=False runat=server />
    										    <input type=button value=" ... " id="FindNU" Visible=False onclick="javascript:PopItem('frmMain', '', 'ddlItemCode', 'True');" CausesValidation=False runat=server />
												<asp:Label id=lblErrItem forecolor=red text="Please select one Stock Item" runat=server />
												<asp:Label id=lblErrItemExist forecolor=red text="Stock Item already exist." runat=server />
											</td>
										</tr>
										<tr>
											<td height="25">Quantity Order :*</td>
											<td ><asp:TextBox id=txtQtyOrder width=25% maxlength=15 OnKeyUp="javascript:calTtlQtyCost();" runat=server />
												<asp:RegularExpressionValidator id="RegularExpressionValidatorQtyOrder" 
													ControlToValidate="txtQtyOrder"
													ValidationExpression="\d{1,9}\.\d{0,5}|\d{1,9}"
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
											<td><asp:TextBox id=txtCost width=25% maxlength=21 OnKeyUp="javascript:calTtlCost();" runat=server />
												<asp:RegularExpressionValidator id="RegularExpressionValidatorCost" 
													ControlToValidate="txtCost"
													ValidationExpression="\d{1,19}\.\d{0,2}|\d{1,19}"
													Display="Dynamic"
													text = "Maximum length 21 digits."
													runat="server"/>
												<asp:RequiredFieldValidator 
													id="validateCost" 
													runat="server" 
													ErrorMessage="Please Specify Unit Cost" 
													ControlToValidate="txtCost" 
													display="dynamic"/>
												<asp:label id=lblErrCost text="Number generated is too big!" Visible=False forecolor=red Runat="server" />
											</td>
										</tr>
								        <tr>
											<td height="25">Total Cost :*</td>
											<td><asp:TextBox id=txtTtlCost width=25% maxlength=18 OnKeyUp="javascript:calUnitCost();" runat=server />
												<asp:RegularExpressionValidator id="RegularExpressionValidatorTtlCost" 
													ControlToValidate="txtTtlCost"
													ValidationExpression="\d{1,15}\.\d{0,2}|\d{1,15}"
													Display="Dynamic"
													text = "Maximum length 18 digits."
													runat="server"/>
												<asp:RequiredFieldValidator 
													id="validateTtlCost" 
													runat="server" 
													ErrorMessage="Please Specify Total Cost" 
													ControlToValidate="txtTtlCost" 
													display="dynamic"/>
												<asp:label id=lblErrTtlCost text="Number generated is too big!" Visible=False forecolor=red Runat="server" />
												&nbsp;&nbsp;&nbsp;
											</td>
										</tr>
								        <tr>
										    <td>Potongan/Discount :</td>
					                        <td><asp:TextBox id=txtDiscount width=10% maxlength=5 runat=server />
					                            <asp:label id=lblDiscount text="%" Runat="server" />
					                            <asp:RegularExpressionValidator id="RegularExpressionValidatorDiscount" 
													ControlToValidate="txtDiscount"
													ValidationExpression="\d{1,5}\.\d{0,2}|\d{1,5}"
													Display="Dynamic"
													text = "Maximum length 5 digits."
													runat="server"/>
												<asp:RequiredFieldValidator 
													id="ValidateDiscount" 
													runat="server" 
													ErrorMessage="Please Specify Potongan/Discount" 
													ControlToValidate="txtDiscount" 
													display="dynamic"/>
												<asp:label id=lblErrDiscount text="Number generated is too big!" Visible=False forecolor=red Runat="server" />												
												&nbsp;&nbsp;&nbsp;
												
												<asp:CheckBox id="chkPPN" checked=true runat="server"></asp:CheckBox>
												&nbsp;<asp:Label id=lblPPN runat=server text="PPN"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
												<asp:CheckBox id="chkSurat" checked=false AutoPostBack=true OnCheckedChanged=chkSuratChanged runat="server"></asp:CheckBox>
												&nbsp;<asp:Label id=lblSurat runat=server text="Surat/Pajak/BBN Kendaraan Bermotor"/>
					                        </td>
										</tr>
										<tr>
											<td>PBB-KB :*</td>
											<td><asp:TextBox id=txtPBBKB width=10% maxlength=5 runat=server />
											    <asp:label id=lblPercent text="%" Runat="server" />
												<asp:RegularExpressionValidator id="RegularExpressionValidatorPBBKB" 
													ControlToValidate="txtPBBKB"
													ValidationExpression="\d{1,5}\.\d{0,2}|\d{1,5}"
													Display="Dynamic"
													text = "Maximum length 5 digits."
													runat="server"/>
												<asp:RequiredFieldValidator 
													id="validatePBB" 
													runat="server" 
													ErrorMessage="Please Specify PBB-KB" 
													ControlToValidate="txtPBBKB" 
													display="dynamic"/>
												&nbsp;&nbsp;&nbsp;
												<asp:Label id=lblPBBKBRate runat=server text="Rate"/> 
												&nbsp;<asp:TextBox id=txtPBBKBRate width=8% maxlength=5 runat=server />
											    <asp:label id=Label1 text="% (Sumatera 5%, Kalimantan 7.5%)" Runat="server" />
											    <asp:RegularExpressionValidator id="RegularExpressionValidator1" 
													ControlToValidate="txtPBBKBRate"
													ValidationExpression="\d{1,5}\.\d{0,2}|\d{1,5}"
													Display="Dynamic"
													text = "Maximum length 5 digits."
													runat="server"/>
												<asp:RequiredFieldValidator 
													id="RequiredFieldValidator1" 
													runat="server" 
													ErrorMessage="Please Specify PBB-KB Rate" 
													ControlToValidate="txtPBBKBRate" 
													display="dynamic"/>
												<asp:label id=lblErrPBBKB text="Number generated is too big!" Visible=False forecolor=red Runat="server" />												
											</td>
										</tr>
										<tr>
											<td style="height: 10px">PPh 21 :*</td>
											<td style="height: 10px"><asp:TextBox id=txtPPH21 width=10% maxlength=5 runat=server />
											    <asp:label id=Label7 text="%" Runat="server" />
												<asp:RegularExpressionValidator id="RegularExpressionValidator6" 
													ControlToValidate="txtPPH21"
													ValidationExpression="\d{1,5}\.\d{0,2}|\d{1,5}"
													Display="Dynamic"
													text = "Maximum length 5 digits."
													runat="server"/>
												<asp:RequiredFieldValidator 
													id="RequiredFieldValidator6" 
													runat="server" 
													ErrorMessage="Please Specify PPh 22" 
													ControlToValidate="txtPPH21" 
													display="dynamic"/>
												<asp:label id=lblerrPPH21 text="Number generated is too big!" Visible=False forecolor=red Runat="server" />		
												&nbsp;&nbsp;&nbsp;
												<asp:CheckBox id="chkGrossUp21" checked=false AutoPostBack=true OnCheckedChanged=chkGrossUP21_Changed runat="server"></asp:CheckBox>
												&nbsp;<asp:Label id=Label9 runat=server text="Gross Up"/>										
											</td>
										</tr>
								        <tr>
											<td>PPh 22 :*</td>
											<td><asp:TextBox id=txtPPN22 width=10% maxlength=5 runat=server />
											    <asp:label id=lblPercent2 text="%" Runat="server" />
												<asp:RegularExpressionValidator id="RegularExpressionValidator3" 
													ControlToValidate="txtPPN22"
													ValidationExpression="\d{1,5}\.\d{0,2}|\d{1,5}"
													Display="Dynamic"
													text = "Maximum length 5 digits."
													runat="server"/>
												<asp:RequiredFieldValidator 
													id="RequiredFieldValidator3" 
													runat="server" 
													ErrorMessage="Please Specify PPh 22" 
													ControlToValidate="txtPPN22" 
													display="dynamic"/>
												<asp:label id=lblErrPPN223 text="Number generated is too big!" Visible=False forecolor=red Runat="server" />												
											</td>
										</tr>
										<tr>
											<td>PPh 23 :*</td>
											<td><asp:TextBox id=txtPPH23 width=10% maxlength=5 runat=server />
											    <asp:label id=Label2 text="%" Runat="server" />
												<asp:RegularExpressionValidator id="RegularExpressionValidator2" 
													ControlToValidate="txtPPH23"
													ValidationExpression="\d{1,5}\.\d{0,2}|\d{1,5}"
													Display="Dynamic"
													text = "Maximum length 5 digits."
													runat="server"/>
												<asp:RequiredFieldValidator 
													id="RequiredFieldValidator2" 
													runat="server" 
													ErrorMessage="Please Specify PPh 23" 
													ControlToValidate="txtPPH23" 
													display="dynamic"/>
												<asp:label id=Label3 text="Number generated is too big!" Visible=False forecolor=red Runat="server" />
												<asp:Label id=lblerrPPH23 ForeColor=red Visible=false runat=server text="Please insert PPH 23"/>												
												&nbsp;&nbsp;&nbsp;
												<asp:CheckBox id="chkGrossUp" checked=false AutoPostBack=true OnCheckedChanged=chkGrossUP_Changed runat="server"></asp:CheckBox>
												&nbsp;<asp:Label id=Label4 runat=server text="Gross Up"/>
											</td>
										</tr>
										<tr>
										    <td>Additional Note :</td>
					                        <td><textarea rows=6 id=txtAddNote cols=50 runat=server></textarea></td>
										</tr>
										<tr>
										    <td>Purchase UOM :</td>
					                        <td><asp:DropDownList id=ddlUOM width=25% runat=server /></td>
										</tr>
										<tr>
										    <td>&nbsp;</td>
										</tr>
										<tr>
										    <td>&nbsp;</td>
										</tr>
										<tr>
										    <td><u><b>Ongkos Angkut</b></u></td>
										</tr>
										<tr>
											<td>Supplier/Transporter :</td>
											<td><asp:DropDownList id=ddlTransporter width=50% AutoPostBack="True" OnSelectedIndexChanged="onSelect_Transporter" runat=server/>
											    <input type=button value=" ... " id="Find1" onclick="javascript:PopSupplier('frmMain', '', 'ddlTransporter', 'True');" CausesValidation=False runat=server />
												<asp:Label id=lblErrTransporter forecolor=red text="Please select one transporter" runat=server />												
											</td>								
										</tr>
										<tr>
										    <td width="20%">Amount :</td>
					                        <td width="50%"><asp:TextBox id=txtAmtTransportFee width=25% runat=server />
					                            &nbsp;&nbsp;&nbsp;
												<asp:CheckBox id="chkPPNTransport" checked=false runat="server"></asp:CheckBox>
												&nbsp;<asp:Label id=lblPPNTransport runat=server text="PPN"/>
					                        </td>
										</tr>
										<tr>
											<td>PPh 23 :*</td>
											<td><asp:TextBox id=txtPPH23OA width=10% maxlength=5 runat=server />
											    <asp:label id=Label5 text="%" Runat="server" />
												<asp:RegularExpressionValidator id="RegularExpressionValidator5" 
													ControlToValidate="txtPPH23OA"
													ValidationExpression="\d{1,5}\.\d{0,2}|\d{1,5}"
													Display="Dynamic"
													text = "Maximum length 5 digits."
													runat="server"/>
												<asp:RequiredFieldValidator 
													id="RequiredFieldValidator5" 
													runat="server" 
													ErrorMessage="Please Specify PPh 23" 
													ControlToValidate="txtPPH23OA" 
													display="dynamic"/>
												<asp:label id=Label6 text="Number generated is too big!" Visible=False forecolor=red Runat="server" />												
												<asp:Label id=lblerrPPH23OA ForeColor=red Visible=false runat=server text="Please insert PPH 23"/>						
												&nbsp;&nbsp;&nbsp;						
												
												<asp:CheckBox id="chkGrossUpOA" checked=false AutoPostBack=true OnCheckedChanged=chkGrossUPOA_Changed runat="server"></asp:CheckBox>
												&nbsp;<asp:Label id=Label8 runat=server text="Gross Up"/>
											</td>
										</tr>
					                    <tr>
					                        <td colspan=6>&nbsp;</td>				
				                        </tr>		
										<tr>
											<td colspan=2 height="25"><asp:Imagebutton id="btnAdd" OnClick="btnAdd_Click" ImageURL="../../images/butt_add.gif" AlternateText=Add UseSubmitBehavior="false" Runat="server" /></td>
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
							Cellpadding="2"
							Pagerstyle-Visible="False"
							OnDeleteCommand="DEDR_Delete"
							OnCancelCommand="DEDR_Cancel"
							OnEditCommand="DEDR_Edit"
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
								<asp:TemplateColumn HeaderText="PR ID <br>PR Location">
									<ItemStyle Width="10%"/>								
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("PRID") %> id="lblPRId" runat="server" /> <br>
										<asp:Label Text=<%# Container.DataItem("PRLocCode") %> id="lblPRLocCode" runat="server" />
										<asp:Label Text=<%# Container.DataItem("POLnId") %> id="lblPOLnId" visible=false runat="server" />
										<asp:Label Text=<%# Container.DataItem("PPN") %> id="lblPPNStatus" visible=false runat="server" />
										<asp:Label Text=<%# Container.DataItem("PPNTransport") %> id="lblPPNTransporter" visible=false runat="server" />
										<asp:Label Text=<%# Container.DataItem("GetRefNo") %> id="lblGetRefNo" visible=false runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="PR/PO Ref. No <br>PR Ref. Loc">
									<ItemStyle Width="8%"/>								
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("PRRefID") %> id="lblPRRefId" runat="server" /><br>
										<asp:Label Text=<%# Container.DataItem("PRRefLocCode") %> id="lblPRRefLocCode" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Item <br>Add. Note">
									<ItemStyle Width="14%"/>								
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("Description") %> id="lblItemCode" runat="server" /><br>
										<asp:Label Text=<%# Container.DataItem("AdditionalNote") %> id="lblAddNote" runat="server" />
										<asp:Label Text=<%# Container.DataItem("ItemCode") %> id="lblItem" visible=false runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Order Qty <br>Purch. UOM">
									<HeaderStyle HorizontalAlign="Right" /> 
									<ItemStyle Width="8%"  HorizontalAlign="Right" />								
									<ItemTemplate>
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("QtyOrder"), 5), 5) %> id="lblQtyOrder" runat="server" /><br>
										<asp:Label Text=<%# Container.DataItem("PurchaseUOM") %> id="lblUOMCode" runat="server" /><br>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Unit Cost <br>Discount">
								    <HeaderStyle HorizontalAlign="Right" /> 
									<ItemStyle Width="8%"  HorizontalAlign="Right" />								
									<ItemTemplate>
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("CostToDisplay"), 2), 2) %> id="lblCost" runat="server" /><br>
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("Discount"), 2) %> id="lblDiscount" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="PBBKB <br>PPH 22">
									<HeaderStyle HorizontalAlign="Right" /> 
									<ItemStyle Width="5%"  HorizontalAlign="Right" />								
									<ItemTemplate> 
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("PBBKB"), 2) %> id="lblPBBKB" runat="server" /><br>
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("PPN22"), 2) %> id="lblPPN22" runat="server" />
										<asp:Label Text=<%# Container.DataItem("PBBKBRate") %> id="lblPBBKBRate" visible=false runat="server" /><br>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Item Amount <br>Item PPN">
									<HeaderStyle HorizontalAlign="Right" /> 
									<ItemStyle Width="10%" HorizontalAlign="Right" /> 
									<ItemTemplate>
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("ItemNetAmountToDisplay"), 2), 2) %> id="lblAmount" runat="server" /><br>
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("ItemPPNAmountToDisplay"), 2), 2) %> id="lblPPN" runat="server" />
									</ItemTemplate>							
								</asp:TemplateColumn>	
								<asp:TemplateColumn HeaderText="OA Amount <br> OA PPN">
								    <HeaderStyle HorizontalAlign="Right" /> 
									<ItemStyle Width="8%" HorizontalAlign="Right"/> 
									<ItemTemplate>
									    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("AmtTransportFee"), 2) %> id="lblTransportFee" runat="server" /><br>
									    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("PPNAmtTransportFee"), 2) %> id="lblPPNTransportFee" runat="server" /><br>
									    <asp:Label Text=<%# Container.DataItem("Transporter") %> id="lblTransporter" Visible="false" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Total Amount">
									<HeaderStyle HorizontalAlign="Right" /> 
									<ItemStyle Width="10%" HorizontalAlign="Right" /> 
									<ItemTemplate>
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("AmountToDisplay"), 2), 2) %> id="lblTotalAmount" runat="server" /><br />
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("NetAmountToEdit"), 2), 2) %> id="lblAmountToEdit" Visible=false runat="server" />
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PPNAmountToEdit"), 2), 2) %> id="lblPPNToEdit" Visible=false runat="server" />
										<asp:Label Text=<%# Container.DataItem("Status") %> id="lblPOLnStatus" runat="server" /><br>
									</ItemTemplate>							
								</asp:TemplateColumn>									
								<asp:TemplateColumn>		
									<ItemStyle Width="5%" HorizontalAlign="Right" /> 
									<ItemTemplate>
									    <asp:label text= '<%# objGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("PPH23"), 2) %>' Visible=False id="lblPPH23" runat="server" />
							            <asp:label text= '<%# Container.DataItem("GrossUp") %>' Visible=False id="lblGrossUp" runat="server" />
							            <asp:label text= '<%# objGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("PPH23Transport"), 2) %>' Visible=False id="lblPPH23OA" runat="server" />
							            <asp:label text= '<%# Container.DataItem("GrossUpTransport") %>' Visible=False id="lblGrossUpOA" runat="server" />
							            <asp:label text= '<%# objGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("PPH21"), 2) %>' Visible=False id="lblPPH21" runat="server" />
							            <asp:label text= '<%# Container.DataItem("GrossUp21") %>' Visible=False id="lblGrossUp21" runat="server" />
										<asp:LinkButton id="Edit" CommandName="Edit" Text="Edit" CausesValidation=False runat="server"/>
										<asp:LinkButton id="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation=False runat="server"/>
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
				<TR>
					<td colspan=3></td>
					<TD height=25 align=left>Additional Discount :</TD>
					<td Align=right>&nbsp;<asp:TextBox id=txtAddDisc text="0" maxlength=18 style="text-align:right" ReadOnly=true OnKeyUp="javascript:calAddDiscount();" runat=server />
						<asp:RegularExpressionValidator id="RegularExpressionValidator4" 
							ControlToValidate="txtAddDisc"
							ValidationExpression="\d{1,15}\.\d{0,2}|\d{1,15}"
							Display="Dynamic"
							text = "Maximum length 18 digits."
							runat="server"/>
						<asp:RequiredFieldValidator 
							id="RequiredFieldValidator4" 
							runat="server" 
							ErrorMessage="Please Specify Additional Discount" 
							ControlToValidate="txtAddDisc" 
							display="dynamic"/>
						<asp:label id=lblerrAddDisc text="Number generated is too big!" Visible=False forecolor=red Runat="server" />
					</td>
				</TR>
				<TR>
					<td colspan=3></td>
					<TD height=25 align=left>Total After Discount :</TD>
					<TD Align=right><asp:TextBox ID=txtTtlAftDisc text="0" ForeColor=black style="text-align:right" ReadOnly=true Runat=server /></TD>
				</TR>
				<tr>
					<td height="25">Remarks :</td>	
					<td colspan="5"><asp:TextBox id="txtRemark" maxlength="128" width=100% runat="server" /></td>
				</tr>
				<tr>
					<td colspan=6 height="25">&nbsp;</td>
				</tr>	
				<tr>
				    <td colspan=2 height="25">
				        <asp:Label id=lblErrGR visible=True Text="All/Some item or POID already have received." forecolor=red runat=server />
				    </td>
				</tr>
				<tr>
					<td colspan=2 height="25">
				        <asp:CheckBox id="chkPrinted" Text="  Mark as printed" checked=false runat=server />
				    </td>		
				</tr>	
				<tr>
					<td colspan=6 height="25">&nbsp;</td>
				</tr>		
				<tr>
					<td align="left" colspan="6">
					    <asp:ImageButton id="btnNew" UseSubmitBehavior="false" onClick="btnNew_Click" imageurl="../../images/butt_new.gif" AlternateText="New" runat=server/>
						<asp:ImageButton id="btnSave" UseSubmitBehavior="false" onClick="btnSave_Click" ImageUrl="../../images/butt_save.gif" AlternateText=Save CausesValidation=False runat="server" />
						<asp:ImageButton id="btnConfirm" UseSubmitBehavior="false" onClick="btnConfirm_Click" ImageUrl="../../images/butt_confirm.gif" AlternateText=Confirm CausesValidation=False runat="server" />
						<asp:ImageButton id="btnPrint" UseSubmitBehavior="false" onClick="btnPreview_Click" ImageUrl="../../images/butt_print.gif" AlternateText=Print CausesValidation=False runat="server" />
						<asp:ImageButton id="btnCancel" UseSubmitBehavior="false" onclick="btnCancel_Click" ImageUrl="../../images/butt_cancel.gif" Text=" Cancel " Runat=server />
						<asp:ImageButton id="btnDelete" UseSubmitBehavior="false" onClick="btnDelete_Click" ImageUrl="../../images/butt_delete.gif" AlternateText=Delete CausesValidation=False runat="server" />
						<asp:ImageButton id="btnUndelete" UseSubmitBehavior="false" onClick="btnUnDelete_Click" ImageUrl="../../images/butt_undelete.gif" AlternateText=Undelete CausesValidation=False runat="server" />
						<asp:ImageButton id="btnBack" UseSubmitBehavior="false" onClick="btnBack_Click" ImageUrl="../../images/butt_back.gif" AlternateText=Back CausesValidation=False runat="server" />
						<asp:ImageButton id="btnEdited" UseSubmitBehavior="false" onClick="btnEdited_Click" ImageUrl="../../images/butt_edit.gif" AlternateText=Edit CausesValidation=False runat="server" />
						<asp:ImageButton id="btnAddendum" UseSubmitBehavior="false" onClick="btnAddendum_Click" ImageUrl="../../images/butt_gen_addendum.gif" AlternateText="Generate Addendum" CausesValidation=False runat="server" />
					</td>
					
				</tr>		
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				<tr id="tblSPK" visible=false>
					<td colspan=6>
						<asp:DataGrid id=dgSPK
							AutoGenerateColumns="false" width="30%" runat="server"
							GridLines=none
							Cellpadding="1"
							Pagerstyle-Visible="False"
							AllowSorting="false">	
							<HeaderStyle CssClass="mr-h"/>
							<ItemStyle CssClass="mr-l"/>
							<AlternatingItemStyle CssClass="mr-r"/>
							<Columns>
								<asp:HyperLinkColumn HeaderText="SPK Created" 
									SortExpression="POID" 
									DataNavigateUrlField="POID" 
									DataNavigateUrlFormatString="PU_trx_PODet.aspx?POID={0}"
									DataTextFormatString="{0:c}"
									DataTextField="POID" />	
							</Columns>
						</asp:DataGrid>
					</td>
				</tr>	
			</table>
		</form>
	</body>
</html>
