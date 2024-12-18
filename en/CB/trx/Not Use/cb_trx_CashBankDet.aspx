<%@ Page Language="vb" src="../../../include/cb_trx_CashBankDet.aspx.vb" Inherits="cb_trx_CashBankDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuCB" src="../../menu/menu_cbtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<html>
	<head>
		<title>Cash Bank Details</title>		
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<script language="javascript">
			
			function calTaxPriceCR() {
				var doc = document.frmMain;
				var a = parseFloat(doc.txtDPPAmountCR.value);
				var b = parseFloat(doc.hidTaxObjectRate.value);		
				var c = (a * (b/100));		    
				var newnumber = new Number(c+'').toFixed(parseInt(0));
				
				doc.txtCRTotalAmount.value = newnumber;
				if (doc.txtCRTotalAmount.value == 'NaN')
					doc.txtCRTotalAmount.value = '';
				else
					doc.txtCRTotalAmount.value = doc.txtCRTotalAmount.value;
			}
			
			function calTaxPriceDR() {
				var doc = document.frmMain;
				var a = parseFloat(doc.txtDPPAmountDR.value);
				var b = parseFloat(doc.hidTaxObjectRate.value);		
				var c = (a * (b/100));		    
				var newnumber = new Number(c+'').toFixed(parseInt(0));
				
				doc.txtDRTotalAmount.value = newnumber;
				if (doc.txtDRTotalAmount.value == 'NaN')
					doc.txtDRTotalAmount.value = '';
				else
					doc.txtDRTotalAmount.value = doc.txtDRTotalAmount.value;
			}
			function gotFocusDPPCR() {
			    var doc = document.frmMain;
			    doc.txtDPPAmountDR.value = '';
			    doc.txtDRTotalAmount.value = '';
	        }
	        function gotFocusDPPDR() {
			    var doc = document.frmMain;
			    doc.txtDPPAmountCR.value = '';
			    doc.txtCRTotalAmount.value = '';
	        }
		</script>		
	</head>
	
	<body >
		<form id=frmMain runat=server>
		<TABLE id="tblHeader" cellSpacing="0" cellPadding="2" width="100%" border="0">
			<tr>
				<td colspan="6"><UserControl:MenuCB id=MenuCB runat="server" /></td>
			</tr>
			<tr>
				<td class="mt-h" colspan="6">CASH BANK DETAILS</td>
			</tr>
			<tr>
				<td colspan=6><hr size="1" noshade></td>
			</tr>
			<TR>
				<TD height=25 width="20%"><asp:Label id=lblPaymentIDTag Text = "Payment ID :" runat=server /></TD>
				<TD width="40%"><asp:Label id=lblPaymentID runat=server /></TD>
				<TD width="5%">&nbsp;</TD>
				<TD width="15%">Period :</TD>
				<TD width="20%"><asp:Label id=lblAccPeriod runat=server /></TD>
				<TD width="5%"></TD>
			</TR>
			<tr>
			    <TD height=25>Transaction Date :</TD>
			    <td><asp:TextBox id=txtDateCreated width=50% maxlength="10" runat="server"/>
					<a href="javascript:PopCal('txtDateCreated');"><asp:Image id="btnDateCreated" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
					<asp:RequiredFieldValidator	id="RequiredFieldValidator1" runat="server"  ControlToValidate="txtDateCreated" text = "Please enter Date Created" display="dynamic"/>
					<asp:label id=lblDate Text ="<br>Date Entered should be in the format " forecolor=red Visible = false Runat="server"/> 
					<asp:label id=lblFmt  forecolor=red Visible = false Runat="server"/> 
				</td>
				<TD>&nbsp;</TD>
				<TD width="15%">Status :</TD>
				<TD width="25%"><asp:Label id=lblStatus runat=server /></TD>
				<TD width="5%"></TD>
			</tr>
			<TR>
				<TD height=25 width="20%">Cash Bank Type* :</TD>
				<TD width="30%"><asp:radiobuttonlist id=rblCashBankType TextAlign="Right" RepeatColumns="2" RepeatLayout="Flow" AutoPostBack=true OnSelectedIndexChanged=CashBankType_Change runat=server /></TD>
				<TD width="5%">&nbsp;</TD>
				<TD>Last Update :</TD>
				<TD><asp:Label ID=lblLastUpdate runat=server /></TD>
			</TR>
			<TR>
				<TD height=25><asp:Label id=lblPayTypeTag Text = "Payment Type :*" runat=server /> </TD>
				<TD><asp:DropDownList width=100% id=ddlPayType autopostback=false runat=server onSelectedIndexChanged=onSelect_PayType>
						<asp:ListItem value="0">Cheque</asp:ListItem>
						<asp:ListItem value="1">Cash</asp:ListItem>
						<asp:ListItem value="2" Selected>Need Verification</asp:ListItem>
						<asp:ListItem value="3">Bilyet Giro</asp:ListItem>
						<asp:ListItem value="4">Others</asp:ListItem>
					</asp:DropDownList>
				    <asp:Label id=lblErrPayType forecolor=red visible=false text="Please select Payment Type"  runat=server/></TD>
				<TD>&nbsp;</TD>
				<TD>Print Date :</TD>
				<TD><asp:Label ID=lblPrintDate runat=server /></TD>
			</TR>
			<tr>
			    <TD>Bilyet Giro Date :</TD>
				<td><asp:TextBox id=txtGiroDate width=50% maxlength="10" runat="server"/>
					<a href="javascript:PopCal('txtGiroDate');"><asp:Image id="btnGiroDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
					<asp:label id=lblDateGiro Text ="<br>Date Entered should be in the format " forecolor=red Visible = false Runat="server"/> 
					<asp:label id=lblFmtGiro  forecolor=red Visible = false Runat="server"/> 
				</td>
				<TD>&nbsp;</TD>
				<TD>Cheque Print Date :</TD>
				<TD><asp:Label ID=lblChequePrintDate runat=server /></TD>
			</tr>
			<TR>
				<TD height=25><asp:Label id=lblBankFrom Text = "Bank From :" runat=server /> </TD>
				<TD><asp:DropDownList width=100% id=ddlBank autopostback=true onSelectedIndexChanged=onSelect_Bank runat=server />
					<asp:Label id=lblErrBank forecolor=red visible=false text="Please select Bank Code"  runat=server/></TD>
				<TD>&nbsp;</TD>
				<TD >Date Created :</TD>
				<TD><asp:Label id=lblDateCreated runat=server /></TD>
			</TR>
			<TR>
				<TD height=25>Cheque/Bilyet Giro No. :</TD>
				<TD><asp:Textbox id=txtChequeNo width=100% maxlength=32 runat=server />
					<asp:Label id=lblErrCheque forecolor=red visible=false text="Please enter Cheque/Bilyet Giro No." runat=server/></TD>
				<TD>&nbsp;</TD>
				<TD>Updated By :</TD>
				<TD><asp:Label ID=lblUpdatedBy runat=server /></TD>
			</TR>
			<TR>
				<TD valign = "top" height=25> <asp:Label id=lblPayToTag Text = "Payment To :*" runat=server /> </TD>
				<TD rowspan = 2><asp:Textbox id=txtPaymentTo width=90% maxlength=100 height=50 AutoPostBack=false TextMode="MultiLine" runat=server />
				            <input type=button value=" ... " id="FindSpl" onclick="javascript:PopSupplier('frmMain', '', 'ddlSupplier', 'True');" CausesValidation=False runat=server />
				            <asp:RequiredFieldValidator 
							id="rfvPaymentTo" 
							runat="server" 
							ErrorMessage="Please fill required field" 
							ControlToValidate="txtPaymentTo" 
							display="dynamic"/>
				</TD>
				<TD>&nbsp;</TD>
				<TD><asp:Label id=lblTaxStatus Text = "Tax Status :" runat=server /></TD>
				<TD><asp:Label ID=lblTaxStatusDesc runat=server /></TD>
			</TR>
			
			<TR>
				<TD height=25><asp:DropDownList width=0% id=ddlSupplier visible=true AutoPostBack=true OnSelectedIndexChanged=onSelect_Supplier runat=server /></TD>
				<TD>&nbsp;</TD>
				<TD><asp:Label id=lblTaxUpdate Text = "Tax Updated By :" runat=server /></TD>
				<TD><asp:Label ID=lblTaxUpdateDesc runat=server /></TD>
			</TR>
			<TR>
				<TD height=25>Bank Account No. :</TD>
				<TD><asp:DropDownList width=100% id=ddlSplBankAccNo AutoPostBack=true OnSelectedIndexChanged=onSelect_SplBankAccNo runat=server /> 
				    <asp:Textbox id=txtBankAccNo Visible=false width=0% maxlength=32 runat=server />
				    <asp:ImageButton ImageAlign=AbsBottom ID=btnGet Visible=false onclick=GetSupplierBtn_Click CausesValidation=False ImageUrl="../../images/icn_next.gif" AlternateText="Get Data" Runat=server />  
					<asp:Label id=lblErrBankAccNo forecolor=red visible=false text="Please enter Bank Account No." runat=server/></TD>
				<TD>&nbsp;</TD>
				<TD>&nbsp;</TD>
				<TD>&nbsp;</TD>
				<TD>&nbsp;</TD>
			</TR>
			<tr>
				<TD height=25><asp:Label id=lblBankTo Text = "Bank To :" runat=server /> </TD>
				<TD><asp:DropDownList width=100% id=ddlBankTo autopostback=true onSelectedIndexChanged=onSelect_Bank runat=server />
					<asp:Label id=lblErrBankTo forecolor=red visible=false text="Please select Bank Code"  runat=server/></TD>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>				
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td colspan=6>&nbsp;</td>
			</tr>
			<tr>
				<td colspan="6">
					<table id="tblSelection" border="0" width="100%" cellspacing="0" cellpadding="4" runat="server">
					    <tr class="mb-c">
							<td height=25>Reference No. :</td>
							<td colspan=4><asp:DropDownList id="ddlRefNo" Width=90% AutoPostBack=true OnSelectedIndexChanged=OnSelect_RefNo runat=server />
							              <asp:ImageButton ImageAlign=AbsBottom ID=btnGetRef onclick=GetRefNoBtn_Click CausesValidation=False ImageUrl="../../images/icn_next.gif" AlternateText="Get Data" Runat=server />  
							              <asp:label id=lblRefNoErr Visible=False forecolor=red Runat="server" /></td>
						</tr>
						<tr class="mb-c">
							<td height=25 width=20%>Line Description :*</td>
							<td colspan=4><asp:TextBox id=txtDescLn Width=100% runat=server />	
											<asp:label id=lblDescErr text="Please enter Line Description" Visible=False forecolor=red Runat="server" /></td>
						</tr>
						<tr id="RowChargeTo" class="mb-c">
							<td height=25>Charge To :*</td>
							<td colspan=4>
								<asp:DropDownList id="ddlLocation" Width=100% AutoPostBack=True OnSelectedIndexChanged=ddlLocation_OnSelectedIndexChanged runat=server /> 
								<asp:label id=lblLocationErr Visible=False forecolor=red Runat="server" />
							</td>
						</tr>
						<tr class="mb-c">
							<td height=25><asp:label id="lblAccCodeTag" Runat="server"/></td>
							<td colspan=4><asp:DropDownList id="lstAccCode" Width=90% AutoPostBack=True OnSelectedIndexChanged=CallCheckVehicleUse runat=server /> 
									   	  <input type=button value=" ... " id="Find" onclick="javascript:PopCOA('frmMain', '', 'lstAccCode', 'True');" CausesValidation=False runat=server />
										  <asp:label id=lblAccCodeErr Visible=False forecolor=red Runat="server" />
							</td>
						</tr>						
						<tr id="RowChargeLevel" class="mb-c">
							<td height="25">Charge Level :* </td>
							<td colspan=4><asp:DropDownList id="ddlChargeLevel" Width=100% AutoPostBack=True OnSelectedIndexChanged=ddlChargeLevel_OnSelectedIndexChanged runat=server /> </td>
						</tr>
						<tr id="RowPreBlk" class="mb-c">
							<td height="25"><asp:label id=lblPreBlkTag Runat="server"/> </td>
							<td colspan=4><asp:DropDownList id="ddlPreBlock" Width=100% runat=server />
										  <asp:label id=lblPreBlockErr Visible=False forecolor=red Runat="server" /></td>
						</tr>
						<tr id="RowBlk" class="mb-c">
							<td height=25><asp:label id=lblBlkTag Runat="server"/></td>
							<td colspan=4><asp:DropDownList id="lstBlock" Width=100% runat=server />
										  <asp:label id=lblBlockErr Visible=False forecolor=red Runat="server" /></td>
						</tr>		
						<tr class="mb-c">
							<td height=25><asp:label id="lblVehTag" Runat="server"/></td>
							<td colspan=4><asp:DropDownList id="lstVehCode" Width=100% runat=server />
										  <asp:label id=lblVehCodeErr Visible=False forecolor=red Runat="server" /></td>
						</tr>
						<tr class="mb-c">
							<td height=25><asp:label id="lblVehExpTag" Runat="server"/></td>
							<td colspan=4><asp:DropDownList id="lstVehExp" Width=100% runat=server />
										  <asp:label id=lblVehExpCodeErr Visible=False forecolor=red Runat="server" /></td>
						</tr>
						
						<tr id="RowTax" visible=false class="mb-c">
							<td height=25>Tax Object :</td>
							<td colspan=4><asp:DropDownList id="lstTaxObject" Width=100% AutoPostBack=True OnSelectedIndexChanged=lstTaxObject_OnSelectedIndexChanged runat=server />
									  <asp:label id=lblTaxObjectErr Visible=False forecolor=red Runat="server" />
						    </td>
						</tr>
						<tr id="RowTaxAmt" visible=false class="mb-c">
						    <td width="15%">DPP Amount (DR) : </td>
						    <td width="30%"><asp:Textbox id="txtDPPAmountDR"  Width=100% maxlength=22 OnKeyUp="javascript:calTaxPriceDR();" runat=server />
								<asp:RegularExpressionValidator 
									id="RegularExpressionValidator2" 
									ControlToValidate="txtDPPAmountDR"
									ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
									Display="Dynamic"
									text = "<BR>Maximum length 19 digits and 2 decimal points"
									runat="server"/>
								<asp:RangeValidator 
									id="RangeValidator2"
									ControlToValidate="txtDPPAmountDR"
									MinimumValue="0.01"
									MaximumValue="9999999999999999999.99"
									Type="double"
									EnableClientScript="True"
									Text="<BR>The value must be not be negative value"
									runat="server" display="dynamic"/>
								<asp:Label id=lblTwoAmountDPP visible=false forecolor=red text="<BR>Please enter either DR or CR DPP amount" runat=server/>
						    </td>
						    <td width="10%">&nbsp;</td>
							<td width="15%">DPP Amount (CR) :</td>
							<td width="30%"><asp:Textbox id="txtDPPAmountCR"  Width=100% maxlength=22 OnKeyUp="javascript:calTaxPriceCR();" runat=server />
								<asp:RegularExpressionValidator 
									id="RegularExpressionValidator1" 
									ControlToValidate="txtDPPAmountCR"
									ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
									Display="Dynamic"
									text = "<BR>Maximum length 19 digits and 2 decimal points"
									runat="server"/>
								<asp:RangeValidator 
									id="RangeValidator1"
									ControlToValidate="txtDPPAmountCR"
									MinimumValue="0.01"
									MaximumValue="9999999999999999999.99"
									Type="double"
									EnableClientScript="True"
									Text="<BR>The value must be not be negative value"
									runat="server" display="dynamic"/>
							</td>
						</tr>
						<tr class="mb-c">
							<td width="15%">Amount (DR) :</td>
							<td width="30%"><asp:Textbox id="txtDRTotalAmount" Width=100% maxlength=22 runat=server />
								<asp:RegularExpressionValidator 
									id="RegularExpressionValidatorAmtDR" 
									ControlToValidate="txtDRTotalAmount"
									ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
									Display="Dynamic"
									text = "<BR>Maximum length 19 digits and 2 decimal points"
									runat="server"/>
								<asp:RangeValidator 
									id="RangetxtAmtDR"
									ControlToValidate="txtDRTotalAmount"
									MinimumValue="0.01"
									MaximumValue="9999999999999999999.99"
									Type="double"
									EnableClientScript="True"
									Text="<BR>The value must be not be negative value"
									runat="server" display="dynamic"/>
								<asp:Label id=lblTwoAmount visible=false forecolor=red text="<BR>Please enter either DR or CR total amount" runat=server/>
							</td>
							<td width="10%">&nbsp;</td>
							<td width="15%">Amount (CR) :</td>
							<td width="30%"><asp:Textbox id="txtCRTotalAmount"  Width=100% maxlength=22 runat=server />
								<asp:RegularExpressionValidator 
									id="RegularExpressionValidatorAmtCR" 
									ControlToValidate="txtCRTotalAmount"
									ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
									Display="Dynamic"
									text = "<BR>Maximum length 19 digits and 2 decimal points"
									runat="server"/>
								<asp:RangeValidator 
									id="RangetxtAmtCR"
									ControlToValidate="txtCRTotalAmount" 
									MinimumValue="0.01"
									MaximumValue="9999999999999999999.99"
									Type="double"
									EnableClientScript="True"
									Text="<BR>The value must not be negative value"
									runat="server" display="dynamic"/>
							</td>
						</tr>
						<tr class="mb-c">
							<td>&nbsp;</td>						
							<td Colspan=2>
								<asp:label id=lblerror text="<br>Number generated is too big!" Visible=False forecolor=red Runat="server" />
								<asp:label id=lblStock text="<br>Not enough quantity in hand!" Visible=False forecolor=red Runat="server" />
								<asp:label id=lbleither text="<br>Please key in either Meter Reading OR Quantity to issue" Visible=False forecolor=red Runat="server" />
							</td>
							<td>&nbsp;</td>						
							<td>&nbsp;</td>						
						</tr>
						<tr class="mb-c">
							<td Colspan=3><asp:ImageButton  id="AddDtlBtn" ImageURL="../../images/butt_add.gif" OnClick="AddDtlBtn_Click" UseSubmitBehavior="false" Runat="server" /> &nbsp;
										  <asp:ImageButton  id="SaveDtlBtn" visible=false ImageURL="../../images/butt_save.gif" OnClick="AddDtlBtn_Click" Runat="server" /></td>
							<td>&nbsp;</td>						
							<td>&nbsp;</td>	
						</tr>
					</table>
				</td>		
			</tr>
			<tr>
				<td colspan=6>&nbsp;</td>
			</tr>
			<tr>
				<td colspan=6><asp:label id=lblConfirmErr text="<BR>control amount must equal to zero and have data transaction" Visible=False forecolor=red Runat="server" /></td>
			</tr>
			<tr>
				<td colspan="6"> 
					<asp:DataGrid id="dgTx"
						AutoGenerateColumns="false" width="100%" runat="server"
						OnItemDataBound="DataGrid_ItemCreated" 
						GridLines = none
						Cellpadding = "2"
						Pagerstyle-Visible="False"
						OnDeleteCommand="DEDR_Delete"
						OnEditCommand="DEDR_Edit"
						AllowSorting="True">	
						<HeaderStyle CssClass="mr-h" />							
						<ItemStyle CssClass="mr-l" />
						<AlternatingItemStyle CssClass="mr-r" />						
					<Columns>
					<asp:TemplateColumn HeaderText="No.">
						<ItemStyle width="3%"/>
						<ItemTemplate>
							<asp:label id="lblIdx" runat="server" />
						</ItemTemplate>
						<EditItemTemplate>
							<asp:label id="lblIdx" runat="server" />
						</EditItemTemplate>							
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Line Description">
						<ItemStyle width="16%"/>
						<ItemTemplate>
							<asp:label text= '<%# Container.DataItem("Description") %>' id="lblDesc" runat="server" /> 
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
						<ItemStyle width="10%"/>
						<ItemTemplate>
							<asp:label text= '<%# Container.DataItem("AccCode") %>' id="lblAccCode" runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="COA Descr">
						<ItemStyle width="15%" />
						<ItemTemplate>
							<asp:label text= '<%# Container.DataItem("AccDescr") %>' id="lblAccDescr" runat="server" /><br />
							<asp:label text= '<%# Container.DataItem("TaxObject") %>' id="lblTaxObject" runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
						<ItemStyle width="20%"/>
						<ItemTemplate>
							<asp:label text= '<%# Container.DataItem("LocCode") %>' id="lblLocCode" runat="server" /><br>
							<asp:label text= '<%# Container.DataItem("BlkCode") %>' id="lblBlkCode" runat="server" />-
                            <asp:label text= '<%# Container.DataItem("BlkDESC") %>' id="lblBLKDesc" runat="server" Width="82px" />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
						<ItemStyle width="10%"/>
						<ItemTemplate>
							<asp:label text= '<%# Container.DataItem("VehCode") %>' id="lblVehCode" runat="server" /> <br>
							<asp:label text= '<%# Container.DataItem("VehExpCode") %>' id="lblVehExpCode" runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Quantity" Visible = False>
						<HeaderStyle HorizontalAlign="Right" />			
						<ItemStyle width="8%" HorizontalAlign="Right" />			
						<ItemTemplate>
							<%# objGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("Quantity"),5) %> 				
							<asp:label id="lblQtyTrx" text= '<%# Container.DataItem("Quantity") %>' visible=false runat="server" />							
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Amount">
						<HeaderStyle HorizontalAlign="Right" />			
						<ItemStyle width="8%" HorizontalAlign="Right" />							
						<ItemTemplate>
							<%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("UnitPrice"), 2), 2)%> 
							<asp:label id="lblUnitCost" text= '<%# Container.DataItem("UnitPrice") %>' visible=false runat="server" />
						</ItemTemplate>							
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Total Amount">
						<HeaderStyle HorizontalAlign="Right" />			
						<ItemStyle width="10%" HorizontalAlign="Right" />						
						<ItemTemplate>
							<asp:Label id="lblAmount" Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Total"), 2), 2) %> runat="server" />
							<asp:label id="lblAccTx" runat="server" />
							<asp:label id="lblAmt" text= '<%# FormatNumber(Container.DataItem("Total"), 2) %>' visible=false runat="server" />
						</ItemTemplate>							
					</asp:TemplateColumn>										
					<asp:TemplateColumn>		
						<ItemStyle width="5%" HorizontalAlign="Center" />							
						<ItemTemplate>
							<asp:label text= '<%# Container.DataItem("CashBankLnID") %>' Visible=False id="lblID" runat="server" />
							<asp:label text= '<%# Container.DataItem("TaxLnID") %>' Visible=False id="lblTaxLnID" runat="server" />
							<asp:label text= '<%# Container.DataItem("TaxRate") %>' Visible=False id="lblTaxRate" runat="server" />
							<asp:label text= '<%# Container.DataItem("DPPAmount") %>' Visible=False id="lblDPPAmount" runat="server" />
							<asp:LinkButton id="lbEdit" CommandName="Edit" Text="Edit" CausesValidation =False Visible=false runat="server" />
							<asp:LinkButton id="lbDelete" CommandName="Delete" Text="Delete" CausesValidation =False Visible=false runat="server" />
						</ItemTemplate>
						
					</asp:TemplateColumn>
					</Columns>										
                        <PagerStyle Visible="False" />
					</asp:DataGrid>
				</td>	
			</tr>
			<tr>
				<td colspan=6>&nbsp;</td>
			</tr>
			<tr>
				<td colspan=4>&nbsp;</td>								
				<td height=25 align=right>Total Amount : <asp:label id="lblTotAmtFig" text="0" runat="server" /></td>						
				<td>&nbsp;</td>					
			</tr>
			<tr>
				<td colspan=4>&nbsp;</td>								
				<td align=right>Control Amount : <asp:label id="lblCtrlAmtFig" runat="server" /></td>	
				<td>&nbsp;</td>					
			</tr>
			<TR>
				<TD height=25>Remarks :</TD>
				<TD colspan="5"><asp:TextBox ID=txtRemark maxlength=256 width=100% Runat=server /></TD>
			</TR>
			<tr>
				<td colspan=6>&nbsp;</td>
			</tr>
			<tr>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td><asp:Label id=lblReprint  Text="<B>( R E P R I N T )</B><br>" Visible=False forecolor=Red runat=server />
				</td>				
			</tr>
			<tr>
				<td ColSpan="6">&nbsp;
					<asp:label id=lblLocCodeErr text="" Visible=False forecolor=red Runat="server" />
					
				</td>
			</tr>
			<TR>
				<TD colSpan="6">
				    <asp:ImageButton id=NewCBBtn UseSubmitBehavior="false" onClick=NewCBBtn_Click imageurl="../../images/butt_new.gif" AlternateText="New" runat=server/>
					<asp:ImageButton ID=SaveBtn UseSubmitBehavior="false" onclick=SaveBtn_Click ImageUrl="../../images/butt_save.gif" AlternateText=Save Runat=server /> 
					<asp:ImageButton ID=VerifiedBtn UseSubmitBehavior="false" onclick=VerifiedBtn_Click ImageUrl="../../images/butt_verified.gif" AlternateText=Verified Runat=server />
					<asp:ImageButton ID=ConfirmBtn UseSubmitBehavior="false" onclick=ConfirmBtn_Click ImageUrl="../../images/butt_confirm.gif" AlternateText=Confirm Runat=server />
					<asp:ImageButton ID=ForwardBtn UseSubmitBehavior="false" CausesValidation=False onclick=ForwardBtn_Click ImageUrl="../../images/butt_move_forward.gif" AlternateText="Move Forward" Runat=server />
					<asp:ImageButton ID=DeleteBtn UseSubmitBehavior="false" onclick=DeleteBtn_Click CausesValidation=false ImageUrl="../../images/butt_delete.gif" AlternateText=Delete Runat=server />
					<asp:ImageButton ID=UnDeleteBtn UseSubmitBehavior="false" onclick=UnDeleteBtn_Click ImageUrl="../../images/butt_undelete.gif" AlternateText=Undelete Runat=server />
					<asp:ImageButton ID=EditBtn UseSubmitBehavior="false" onClick=EditBtn_Click ImageUrl="../../images/butt_edit.gif" AlternateText=Edit CausesValidation=False runat="server" />
					<asp:ImageButton id=CancelBtn UseSubmitBehavior="false" onClick="CancelBtn_Click" ImageUrl="../../images/butt_cancel.gif" AlternateText=Cancel CausesValidation=False runat="server" />
					<asp:ImageButton ID=BackBtn UseSubmitBehavior="false" CausesValidation=False onclick=BackBtn_Click ImageUrl="../../images/butt_back.gif" AlternateText=Back Runat=server />
					<Input type=hidden id=payid value="" runat=server />
					
				</TD>
			</TR>
			<tr>
			    <TD colSpan="6">
			        <asp:ImageButton ID=PrintBtn UseSubmitBehavior="false" onclick=PreviewBtn_Click CausesValidation=false ImageUrl="../../images/butt_print.gif" AlternateText=Print Runat=server />
					<asp:ImageButton ID=PrintChequeBtn UseSubmitBehavior="false" onclick=PreviewChequeBtn_Click CausesValidation=false ImageUrl="../../images/butt_print_cheque.gif" AlternateText="Print Cheque" Runat=server />
					<asp:ImageButton ID=PrintSlipBtn UseSubmitBehavior="false" onclick=PreviewSlipBtn_Click CausesValidation=false ImageUrl="../../images/butt_print_slip.gif" AlternateText="Print Slip" Runat=server />
					<asp:ImageButton ID=PrintTransferBtn UseSubmitBehavior="false" onclick=PreviewTransferBtn_Click CausesValidation=false ImageUrl="../../images/butt_print_slip_transfer.gif" AlternateText="Print Slip Transfer" Runat=server />
					<asp:ImageButton ID=PrintKwitansiBtn UseSubmitBehavior="false" onclick=PreviewKwitansiBtn_Click CausesValidation=false ImageUrl="../../images/butt_print_kwitansi.gif" AlternateText="Print Kwitansi" Runat=server />
			    </TD>
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
		    <asp:label id="lblPayType" visible="false" text="0" runat="server" />
		    <asp:label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="issueType" Visible="False" Runat="server" />
			<asp:label id="lblStsHid" Visible="False" Runat="server"/>
			<asp:label id="blnShortCut" Visible="False" Runat="server"/>
			<asp:label id=lblCode visible=false text=" Code" runat=server />
			<asp:label id=lblSelect visible=false text="Select " runat=server />
			<asp:label id=lblPleaseSelect visible=false text="Please select " runat=server />
			<asp:label id="blnUpdate" Visible="False" Runat="server"/>
			<asp:label id=lblTxLnID visible=false runat=server />
		    <asp:label id=lblStatusInput visible=false runat=server />
		    <asp:label id=lblCurrentPeriod visible=false runat=server />
		    
			<Input type=hidden id=hidBlockCharge value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>
			<Input type=hidden id=hidUserID value="" runat=server/>
			<Input type=hidden id=hidNPWPNo value="" runat=server />
			<Input type=hidden id=hidTaxObjectRate value=0 runat=server />
			<Input type=hidden id=hidCOATax value=0 runat=server />
			<Input type=hidden id=hidTaxStatus value=1 runat=server />
			<Input type=hidden id=hidHadCOATax value=0 runat=server />
			
			<Input type=hidden id=hidPrevID value="" runat=server />
			<Input type=hidden id=hidPrevDate value=0 runat=server />
			<Input type=hidden id=hidNextID value="" runat=server />
			<Input type=hidden id=hidNextDate value=0 runat=server />
			
			<Input type=hidden id=hidFFBSpl value="0" runat=server />
			<Input type=hidden id=hidSplCode value="" runat=server />
		</form>
	</body>
</html>
