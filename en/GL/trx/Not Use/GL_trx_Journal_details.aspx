<%@ Page Language="vb" trace="False" src="../../../include/GL_Trx_Journal_Details.aspx.vb" Inherits="GL_Journal_Det" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLTrx" src="../../menu/menu_GLtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Journal Details</title>		
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<script language="javascript">
			function gotFocusCR() {
			    var doc = document.frmMain;
			    var dbAmt = parseFloat(doc.hidTtlDBAmt.value);
			    var crAmt = parseFloat(doc.hidTtlCRAmt.value);
			    var dbDBCR = doc.hidDBCR.value;
			    var diffAmt = dbAmt+crAmt;
			    doc.txtDRTotalAmount.value = '';
                
                if (dbDBCR == '')
                    if (diffAmt > 0) 
                        doc.txtCRTotalAmount.value = round(doc.hidCRAmt.value, 2) + round(doc.hidCtrlAmtFig.value, 2);
                    else
                        doc.txtCRTotalAmount.value = round(doc.hidCRAmt.value, 2);
                else
                    if (dbDBCR == 'CR') 
                         doc.txtCRTotalAmount.value = round(doc.hidCRAmt.value, 2) + round(doc.hidCtrlAmtFig.value, 2);
                    else
                        doc.txtCRTotalAmount.value = round(doc.hidDBAmt.value, 2);
                    
	        }
	        function gotFocusDR() {
			    var doc = document.frmMain;
			    var dbAmt = parseFloat(doc.hidTtlDBAmt.value);
			    var crAmt = parseFloat(doc.hidTtlCRAmt.value);
			    var dbDBCR = doc.hidDBCR.value;
			    var diffAmt = dbAmt+crAmt;
			    doc.txtCRTotalAmount.value = '';
			    
			    if (dbDBCR == '') 
	                if (diffAmt > 0) 
	                    doc.txtDRTotalAmount.value = round(doc.hidDBAmt.value, 2);
	                else
                        doc.txtDRTotalAmount.value = round(doc.hidDBAmt.value, 2) + (round(doc.hidCtrlAmtFig.value, 2)*-1);
                else
                    if (dbDBCR == 'DR') 
                        doc.txtDRTotalAmount.value = round(doc.hidDBAmt.value, 2) + (round(doc.hidCtrlAmtFig.value, 2)*-1);
                    else
                        doc.txtDRTotalAmount.value = round(doc.hidCRAmt.value, 2);
	        }
	        
	        function lostFocusCR() {
			    var doc = document.frmMain;
			    var x = doc.txtCRTotalAmount.value;
			    var dbAmt = parseFloat(x.toString().replace(/,/gi,""));
			    doc.txtCRTotalAmount.value = toCurrency(round(dbAmt, 2));
	        }
	        
	        function lostFocusDR() {
			    var doc = document.frmMain;
			    var x = doc.txtDRTotalAmount.value;
			    var dbAmt = parseFloat(x.toString().replace(/,/gi,""));
			    doc.txtDRTotalAmount.value = toCurrency(round(dbAmt, 2));
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
		<asp:label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
		<asp:label id="issueType" Visible="False" Runat="server" />
		<asp:label id="lblStsHid" Visible="False" Runat="server"/>
		<asp:label id="blnShortCut" Visible="False" Runat="server"/>
		<asp:label id=lblCode visible=false text=" Code" runat=server />
		<asp:label id=lblSelect visible=false text="Select " runat=server />
		<asp:label id=lblPleaseSelect visible=false text="Please select " runat=server />
		<asp:label id="blnUpdate" Visible="False" Runat="server"/>
		<asp:label id=lblTxLnID visible=false runat=server />

		<table border=0 width="100%" cellspacing="0" cellpadding="1">
			<tr>
				<td colspan=6><UserControl:MenuGLTrx EnableViewState=False id=menuIN runat="server" /></td>
			</tr>			
			<tr>
				<td class="mt-h" colspan=6>JOURNAL DETAILS</td>
			</tr>
			<tr>
				<td colspan=6><hr size="1" noshade></td>
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
				<td width="20%" height=25>Journal ID :</td>
				<td width="40%"><asp:label id=lblTxID Runat="server"/></td>
				<td width="5%">&nbsp;</td>
				<td width="15%">Period : </td>
				<td width="20%"><asp:Label id=lblAccPeriod runat=server /></td>
				<td width="5%">&nbsp;</td>
			</tr>
			<tr>
				<td height=25>Description :*</td>
				<td><asp:Textbox id="txtDesc" Width=100% MaxLength=128 runat=server />
					<asp:RequiredFieldValidator 
						id="validateDesc" 
						runat="server" 
						ErrorMessage="Please enter Journal Description" 
						EnableClientScript="True"
						ControlToValidate="txtDesc" 
						display="dynamic"/>
				</td>
				<td>&nbsp;</td>
				<td>Status : </td>
				<td><asp:Label id=Status runat=server /></td>
				<td>&nbsp;</td>
			</tr>			
			<tr>
				<td height=25>Transaction Type :</td>
				<td><asp:DropDownList id="lstTxType" Width=50% AutoPostBack=true OnSelectedIndexChanged="TxType_Change" runat=server />
				</td>
				<td>&nbsp;</td>
			    <td>Date Created : </td>
				<td><asp:Label id=CreateDate runat=server /></td>
				<td>&nbsp;</td>
			</tr>			
			<tr>
				<td height=25>Received From :</td>
				<td><asp:DropDownList id="ddlReceiveFrom" Width=50% AutoPostBack=true OnSelectedIndexChanged="RcvFrom_Change"  runat=server>
						<asp:ListItem value="1">Head Quarter</asp:ListItem>
						<asp:ListItem value="2">Others</asp:ListItem>
						<asp:ListItem value="3">Data Transfer</asp:ListItem>
						<asp:ListItem value="4">Hutang Dagang</asp:ListItem>
					</asp:DropDownList>
				</td>
				<td>&nbsp;</td>	
				<td>Last Update :</td>
				<td><asp:Label id=UpdateDate runat=server /></td>	
				<td>&nbsp;</td>	
			</tr>
			<tr>
				<td height=25>Document Ref. No. :*</td>
				<td><asp:Textbox id="txtRefNo" maxlength="32" Width=100% runat=server />	
					<asp:RequiredFieldValidator 
						id="validateRef" 
						runat="server" 
						ErrorMessage="Please enter document reference number" 
						EnableClientScript="True"
						ControlToValidate="txtRefNo" 
						display="dynamic"/>
				</td>
				<td>&nbsp;</td>
				<td>Updated By :</td>
				<td><asp:Label id=UpdateBy runat=server /></td>				
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td>Document Ref. Date :* </td>
				<td><asp:TextBox id=txtDate Width=50% maxlength=10 runat=server />
					<a href="javascript:PopCal('txtDate');"><asp:Image id="btnSelDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a> 
					<asp:RequiredFieldValidator 
						id="validateDate" 
						runat="server" 
						ErrorMessage="<br>Please enter document date" 
						EnableClientScript="True"
						ControlToValidate="txtDate" 
						display="dynamic"/>
					<asp:label id=lblDate Text ="<BR>Date entered should be in the format " forecolor=red Visible = false Runat="server"/> 
					<asp:label id=lblFmt  forecolor=red Visible = false Runat="server"/>
				</td>
				<td>&nbsp;</td>
				<td>Print Date :</td>
				<td><asp:Label id=lblPrintDate visible="false" runat=server /></td>				
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td>Document Amount :*</td>
				<td><asp:Textbox id="txtAmt" width=100% maxlength=22 runat=server />	
					<asp:RegularExpressionValidator 
						id="RegularExpressionValidatorAmt" 
						ControlToValidate="txtAmt"
						ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
						Display="Dynamic"
						text = "<BR>Maximum length 19 digits and 2 decimal points"
						runat="server"/>
					<asp:RangeValidator 
						id="RangetxtAmt"
						ControlToValidate="txtAmt"
						MinimumValue="0.00"
						MaximumValue="9999999999999999999.99"
						Type="double"
						EnableClientScript="True"
						Text="<BR>The value must be from 0!"
						runat="server" display="dynamic"/>
				</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>				
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25><asp:label id="lblSupplier" text="Supplier Code :*" Visible=false Runat="server"/></TD>
				<td><asp:DropDownList width=90% id=ddlSupplier onSelectedIndexChanged=onSelect_Change Visible=false runat=server />
				    <input type=button value=" ... " id="btnFind" onclick="javascript:PopSupplier('frmMain', 'XXX', 'ddlSupplier', 'True');" CausesValidation=False visible=false runat=server />
					<asp:Label id=lblErrSupplier forecolor=red Text="Please select Supplier Code" visible=false runat=server/>
				</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>				
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25><asp:label id="lblFromLocCode" text="From Location :*" Visible=false Runat="server"/></TD>
				<td><asp:DropDownList width=100% id=ddlFromLocCode Visible=false runat=server />
					<asp:Label id=lblErrFromLocCode forecolor=red visible=false text="Please select Location Code"  runat=server/>
				</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>				
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>				
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td colspan="6">
					<table id="tblAdd" border="0" width="100%" cellspacing="0" cellpadding="4" runat="server">
						<tr class="mb-c">
							<td height=25 style="width: 134px">Line Description :*</td>
							<td colspan=4><asp:TextBox id=txtDescLn Width=100% maxlength=128 runat=server />	
											<asp:label id=lblDescErr text="Please enter Line Description" Visible=False forecolor=red Runat="server" /></td>
						</tr>
						<tr id="RowChargeTo" class="mb-c">
							<td height=25 style="width: 134px">Charge To :*</td>
							<td colspan=4>
								<asp:DropDownList id="ddlLocation" Width=100% AutoPostBack=True OnSelectedIndexChanged=ddlLocation_OnSelectedIndexChanged runat=server /> 
								<asp:label id=lblLocationErr Visible=False forecolor=red Runat="server" />
							</td>
						</tr>
						<tr class="mb-c">
							<td height=25 style="width: 134px"><asp:label id="lblAccCodeTag" Runat="server"/></td>
							<td colspan=4><asp:DropDownList id="lstAccCode" Width=90% AutoPostBack=True OnSelectedIndexChanged=CallCheckVehicleUse runat=server /> 
							              <input type=button value=" ... " id="Find" onclick="javascript:PopCOA('frmMain', '', 'lstAccCode', 'True');" CausesValidation=False runat=server />  
									   	  <asp:label id=lblAccCodeErr Visible=False forecolor=red Runat="server" />
							</td>
						</tr>						
						<tr id="RowChargeLevel" class="mb-c">
							<td height="25" style="width: 134px">Charge Level :* </td>
							<td colspan=4><asp:DropDownList id="ddlChargeLevel" Width=100% AutoPostBack=True OnSelectedIndexChanged=ddlChargeLevel_OnSelectedIndexChanged runat=server /> </td>
						</tr>
						<tr id="RowPreBlk" class="mb-c">
							<td style="width: 134px; height: 49px"><asp:label id=lblPreBlkTag Runat="server"/> </td>
							<td colspan=4 style="height: 49px"><asp:DropDownList id="ddlPreBlock" Width=100% runat=server />
										  <asp:label id=lblPreBlockErr Visible=False forecolor=red Runat="server" /></td>
						</tr>
						<tr id="RowBlk" class="mb-c">
							<td style="width: 134px; height: 49px"><asp:label id=lblBlkTag Runat="server"/></td>
							<td colspan=4 style="height: 49px"><asp:DropDownList id="lstBlock" Width=100% runat=server />
										  <asp:label id=lblBlockErr Visible=False forecolor=red Runat="server" /></td>
						</tr>		
						<tr class="mb-c">
							<td height=25 style="width: 134px"><asp:label id="lblVehTag" Runat="server"/></td>
							<td colspan=4><asp:DropDownList id="lstVehCode" Width=100% runat=server />
										  <asp:label id=lblVehCodeErr Visible=False forecolor=red Runat="server" /></td>
						</tr>
						<tr class="mb-c">
							<td height=25 style="width: 134px"><asp:label id="lblVehExpTag" Runat="server"/></td>
							<td colspan=4><asp:DropDownList id="lstVehExp" Width=100% runat=server />
										  <asp:label id=lblVehExpCodeErr Visible=False forecolor=red Runat="server" /></td>
						</tr>
						<tr id="RowPO" visible=false class="mb-c">
							<td height=25 style="width: 134px">Purchase Order ID</td>
							<td colspan=4><asp:DropDownList id="lstPOID" Width=100% runat=server />
							          <asp:label id=lblPOIDErr Visible=False forecolor=red Runat="server" />
						    </td>
						</tr>
						<tr id="RowTax" visible=false class="mb-c">
							<td height=25 style="width: 134px"><asp:label id=lblTaxObject Runat="server"/></td>
							<td colspan=4><asp:DropDownList id="lstTaxObject" Width=100% AutoPostBack=True OnSelectedIndexChanged=lstTaxObject_OnSelectedIndexChanged runat=server />
									  <asp:label id=lblTaxObjectErr Visible=False forecolor=red Runat="server" />
						    </td>
						</tr>
						<tr id="RowTaxAmt" visible=false class="mb-c">
						    <td style="width: 134px">DPP Amount (DR) : </td>
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
							<td style="width: 134px">Total Amount (DR) :</td>
							<td width="30%"><asp:Textbox id="txtDRTotalAmount" Width=100%  maxlength=22 runat=server />
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
							<td width="15%">Total Amount (CR) :</td>
							<td width="30%"><asp:Textbox id="txtCRTotalAmount" Width=100% maxlength=22  runat=server />
								<asp:RegularExpressionValidator 
									id="RegularExpressionValidatorAmtCR" 
									ControlToValidate="txtCRTotalAmount"
									ValidationExpression="\d{1,25}\.\d{1,2}|\d{1,25}"
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
									Text="<BR>The value must be not be negative value"
									runat="server" display="dynamic"/>
							</td>
						</tr>
						<tr class="mb-c">
							<td style="width: 134px">&nbsp;</td>						
							<td Colspan=2>
								<asp:label id=lblerror text="<br>Number generated is too big!" Visible=False forecolor=red Runat="server" />
								<asp:label id=lblStock text="<br>Not enough quantity in hand!" Visible=False forecolor=red Runat="server" />
								<asp:label id=lbleither text="<br>Please key in either Meter Reading OR Quantity to issue" Visible=False forecolor=red Runat="server" />
							</td>
							<td>&nbsp;</td>						
							<td>&nbsp;</td>						
						</tr>
						<tr class="mb-c">
							<td Colspan=3><asp:ImageButton AlternateText="Add" id="Add" ImageURL="../../images/butt_add.gif" OnClick="btnAdd_Click" UseSubmitBehavior="false" Runat="server" /> &nbsp;
										  <asp:ImageButton AlternateText="Save" id="Update" visible=false ImageURL="../../images/butt_save.gif" OnClick="btnAdd_Click" Runat="server" /></td>
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
				<td colspan=6><asp:label id=lblConfirmErr text="<BR>Document must contain transaction to Confirm!" Visible=False forecolor=red Runat="server" /></td>
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
						OnCancelCommand="DEDR_Cancel"								
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
							<asp:label text=<%# Container.DataItem("Description") %> id="lblDesc" runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Charge To">
						<ItemStyle width="10%"/>
						<ItemTemplate>
							<asp:label text=<%# Container.DataItem("LocCode") %> id="lblLocCode" runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
						<ItemStyle width="10%"/>
						<ItemTemplate>
							<asp:label text=<%# Container.DataItem("AccCode") %> id="lblAccCode" runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Acc Descr">
						<ItemStyle width="17%"/>
						<ItemTemplate>
							<asp:label text=<%# Container.DataItem("AccDescr") %> id="lblAccDescr" runat="server" /><br />
							<asp:label text= '<%# Container.DataItem("TaxObject") %>' id="lblTaxObject" runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
						<ItemStyle width="8%"/>
						<ItemTemplate>
							<asp:label text=<%# Container.DataItem("BlkCode") %> id="lblBlkCode" runat="server" /><br />
                            <asp:label text=<%# Container.DataItem("BlkDesc") %> id="LblBlkDesc" runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
						<ItemStyle width="10%"/>
						<ItemTemplate>
							<asp:label text=<%# Container.DataItem("VehCode") %> id="lblVehCode" runat="server" /><br />
							<asp:label text=<%# Container.DataItem("VehExpCode") %> id="lblVehExpCode" runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>
				
					<asp:TemplateColumn HeaderText="Total">
						<HeaderStyle HorizontalAlign="Right" />			
						<ItemStyle width="10%" HorizontalAlign="Right" />						
						<ItemTemplate>
							<asp:label id="lblAmount" text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Total"), 2), 2) %>  runat="server" />
							<asp:label id="lblAccTx" runat="server" />
							<asp:label id="lblAmt" text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Total"), 2), 2) %> visible=false runat="server" />
						</ItemTemplate>							
					</asp:TemplateColumn>										
					<asp:TemplateColumn>		
						<ItemStyle width="5%" HorizontalAlign="Center" />							
						<ItemTemplate>
							<asp:label text=<%# Container.DataItem("JrnLineID") %> Visible=False id="lblID" runat="server" />
							<asp:label text= '<%# Container.DataItem("TaxLnID") %>' Visible=False id="lblTaxLnID" runat="server" />
							<asp:label text= '<%# Container.DataItem("TaxRate") %>' Visible=False id="lblTaxRate" runat="server" />
							<asp:label text= '<%# Container.DataItem("DPPAmount") %>' Visible=False id="lblDPPAmount" runat="server" />
							<asp:LinkButton id="Edit" CommandName="Edit" Text="Edit" CausesValidation =False runat="server" />
							<asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" CausesValidation =False runat="server" />
						</ItemTemplate>
						<EditItemTemplate>
							<asp:label text=<%# Container.DataItem("JrnLineID") %> Visible=False id="lblID" runat="server" />
							<asp:LinkButton id="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation=False runat="server"/>
						</EditItemTemplate>
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
			<tr>
				<td colspan=6>&nbsp;</td>
			</tr>
			<tr>
				<td ColSpan="6">
					<asp:label id=lblBPErr text="Unable to add record, please create a Bill Party for " Visible=False forecolor=red Runat="server" />
					<asp:label id=lblLocCodeErr text="" Visible=False forecolor=red Runat="server" />
				</td>
			</tr>
			<tr>
				<td align="left" colspan="6">
				    <asp:ImageButton id="NewBtn"  UseSubmitBehavior="false" onClick=NewBtn_Click     imageurl="../../images/butt_new.gif"     CausesValidation=False  AlternateText="New"     runat=server/>
					<asp:ImageButton id="Save"    UseSubmitBehavior="false" onClick=btnSave_Click    ImageURL="../../images/butt_save.gif"                            AlternateText="Save"    runat="server"  visible=False />
					<asp:ImageButton id="Delete"  UseSubmitBehavior="false" onClick=btnDelete_Click  ImageURL="../../images/butt_delete.gif"  CausesValidation=False  AlternateText="Delete"  runat="server"  />
					<asp:ImageButton id="Undelete" UseSubmitBehavior="false" onClick="btnUnDelete_Click" ImageUrl="../../images/butt_undelete.gif" CausesValidation=False AlternateText="Undelete"  runat="server"  visible=False />
					<asp:ImageButton id="Print"   UseSubmitBehavior="false" onClick=btnPrint_Click   ImageURL="../../images/butt_print.gif"   CausesValidation=False  AlternateText="Print"   runat="server"  visible=False />
					<asp:ImageButton id="Back"    UseSubmitBehavior="false" onClick=btnBack_Click    ImageURL="../../images/butt_back.gif"    CausesValidation=False  AlternateText="Back"    runat="server" />
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
			<Input type=hidden id=hidBlockCharge value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>
			<Input type=hidden id=hidCtrlAmtFig value="" runat=server/>
			<Input type=hidden id=hidDBCR value="" runat=server/>
			<Input type=hidden id=hidTtlDBAmt value="0" runat=server/>
			<Input type=hidden id=hidTtlCRAmt value="0" runat=server/>
			<Input type=hidden id=hidDBAmt value="0" runat=server/>
			<Input type=hidden id=hidCRAmt value="0" runat=server/>
			
			<Input type=hidden id=hidNPWPNo value="" runat=server />
			<Input type=hidden id=hidTaxObjectRate value=0 runat=server />
			<Input type=hidden id=hidCOATax value=0 runat=server />
			<Input type=hidden id=hidTaxStatus value=1 runat=server />
			<Input type=hidden id=hidHadCOATax value=0 runat=server />
			<Input type=hidden id=hidFFBSpl value="0" runat=server />
		</form>
	</body>
</html>
