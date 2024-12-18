<%@ Page Language="vb" Trace="false" src="../../../include/IN_Trx_StockReceive_Details.aspx.vb" Inherits="IN_StockReceive" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuINTrx" src="../../menu/menu_INtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

 
<html>
	<head>
		<title>Stock Receive Details</title>		
	    <style type="text/css">
            .style2
            {
                width: 318px;
            }
            .style4
            {
                width: 208px;
            }
        </style>
        		<Preference:PrefHdl id=PrefHdl runat="server" />
         <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>

		<script language="javascript">
			
			function calAmount(i) {
				var doc = document.frmMain;
				var a = parseFloat(doc.txtQty.value);
				var b = parseFloat(doc.txtCost.value);
				var c = parseFloat(doc.txtAmount.value);
				var g = parseFloat(doc.lblPBBKB1.value);
				var d = doc.txtCost.value.toString();
				var e = doc.txtAmount.value.toString();
				var f = doc.lblPBBKB1.value.toString();

				if ((i == '1') || (i == '2')) {
					if ((doc.txtQty.value != '') && (doc.txtCost.value != ''))						
						doc.txtAmount.value = round(a * b, 2) + (round(a * b, 2) * round(g / 100, 2));
					else
						doc.txtAmount.value = '';
				}

                if (i == '3') {
					if ((doc.txtQty.value != '') && (doc.txtAmount.value != ''))
						doc.txtCost.value = round(c / a, 2);
					else
						doc.txtCost.value = '';
				}
				
//				if (((i == '1') || (i == '2')) && (d.indexOf('.') == -1 && e.indexOf('.') == -1)) {
//					if ((doc.txtQty.value != '') && (doc.txtCost.value != ''))						
//						doc.txtAmount.value = Math.round(a * b) + Math.round((a * b) * (g/100));
//					else
//						doc.txtAmount.value = '';
//				}
//				if (i == '3' && d.indexOf('.') == -1 && e.indexOf('.') == -1) {
//					if ((doc.txtQty.value != '') && (doc.txtAmount.value != ''))
//						doc.txtCost.value = Math.round(c / a);
//					else
//						doc.txtCost.value = '';
//				}

				if (doc.txtQty.value == 'NaN')
					doc.txtQty.value = '';

				if (doc.txtCost.value == 'NaN')
					doc.txtCost.value = '';
					
				if (doc.txtAmount.value == 'NaN')
					doc.txtAmount.value = '';
					
				if (doc.lblPBBKB1.value == 'NaN')
					doc.lblPBBKB1.value = '';

			}
		</script>		

	<body>
		<form id=frmMain class="main-modul-bg-app-list-pu"  runat=server>
        <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">  

   		        <asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
   		        <asp:Label id=lblCode visible=false text=" Code" runat=server />
   		        <asp:Label id=lblSelect visible=false text="Select " runat=server />
   		        <asp:Label id=lblPleaseSelect visible=false text="Please select " runat=server />
		        <asp:label id="SortExpression" Visible="False" Runat="server" />
		        <asp:label id="lblStatusHid" Visible="False" Runat="server" />

		        <table border=0 width="100%" cellspacing="0" cellpadding="1" class="font9Tahoma">
			        <tr>
				        <td colspan=6><UserControl:MenuINTrx enableviewstate=false id=menuIN runat="server" /></td>
			        </tr>			
			        <tr>
				        <td  colspan=6>
                            <table cellpadding="0" cellspacing="0" class="font9Tahoma" style="width: 100%" >
                                <tr>
                                    <td class="style2">
                                     <strong>  STOCK RECEIVE DETAILS</strong></td>
                                    <td style="text-align: right" class="font9Header">
                                        Period : <asp:Label id=lblAccPeriod runat=server />| Status : <asp:Label id=Status runat=server />| Date Created : <asp:Label id=CreateDate runat=server />| Last Update : <asp:Label id=UpdateDate runat=server />| Update By : <asp:Label id=UpdateBy runat=server />| Print Date : <asp:Label id=lblPrintDate visible=false runat=server /> 
                                    </td>
                                </tr>
                            </table>
                            <hr style="width :100%" />
                        </td>
			        </tr>
			        <tr>
				        <td height=25 style="width: 200px">Stock Receive ID :</td>
				        <td style="width: 510px"><asp:label id=lblStckTxID Runat="server"/></td>
				        <td width="5%">&nbsp;</td>
				        <td style="width: 119px">&nbsp;</td>
				        <td width="20%">&nbsp;</td>
				        <td width="5%"><asp:Label id=lblReprint  Text="<B>( R E P R I N T )</B><br>" Visible=False forecolor=Red runat=server /></td>
			        </tr>
			        <tr>
				        <td height=25 style="width: 158px">Stock Receive Type :</td>
				        <td style="width: 510px"><asp:DropDownList id="lstRecDoc" CssClass="fontObject" Width=100% runat=server AutoPostBack=True OnSelectedIndexchanged=Set_Focus /></td>
				        <td>&nbsp;</td>
				        <td style="width: 119px">&nbsp;</td>
				        <td>&nbsp;</td>
				        <td>&nbsp;</td>
			        </tr>			
			        <tr>
				        <td style="width: 158px;">Stock Receive Ref. No. :*</td>
				        <td style="width: 510px;"><asp:TextBox id=txtRefNo CssClass="fontObject" width="100%" maxlength=32    Runat="server"/>
					        <asp:RequiredFieldValidator 
						        id="validateRefNo" 
						        runat="server" 
						        ErrorMessage="Please specify reference no.!" 
						        ControlToValidate="txtRefNo" 
						        EnableClientScript="True"
						        display="dynamic"/>
						        <asp:label id=lblErrRefNo Text="Please specify reference no.!" forecolor=red Visible = false Runat="server"/> 
				        </td>
				        <td>&nbsp;</td>
				        <td style="width: 119px;">&nbsp;</td>
				        <td>&nbsp;</td>		
				        <td>&nbsp;</td>
			        </tr>			
			        <tr>
				        <td height=25 style="width: 158px">Stock Receive Ref. Date :*</td>
				        <td style="width: 510px"><asp:TextBox id=txtDate CssClass="fontObject"  Width="30%" maxlength=10  runat=server />
					        <a href="javascript:PopCal('txtDate');">
					        <asp:Image id="btnSelDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a> 
					        <asp:RequiredFieldValidator 
						        id="validateDate" 
						        runat="server" 
						        ErrorMessage="Please specify reference date!" 
						        EnableClientScript="True"
						        ControlToValidate="txtDate" 
						        display="dynamic"/>
					        <asp:label id=lblDate Text="Date Entered should be in the format" forecolor=red Visible=false Runat="server"/> 
					        <asp:label id=lblFmt  forecolor=red Visible = false Runat="server"/> 
				        </td>
				        <td>&nbsp;</td>
				        <td style="width: 119px">&nbsp;</td>
				        <td>&nbsp;</td>			
				        <td>&nbsp;</td>
			        </tr>
			        <tr>
				        <td height=25 style="width: 158px">Stock Receive Into :*</td>
				        <td style="width: 510px"><asp:DropDownList id="ddlInventoryBin" CssClass="fontObject" Width=100% runat=server/>
				            <asp:Label id=lblInventoryBin text="Please Select Inventory Bin" forecolor=red visible=false runat=server /></td>
				        <td>&nbsp;</td>
				        <td style="width: 119px">&nbsp;</td>
				        <td>&nbsp;</td>				
				        <td>&nbsp;</td>
			        </tr>

			        <tr>
				        <td height=25 class="style1">Warehouse Into :*</td>
				        <td><asp:DropDownList id="lstStorage"  CssClass="fontObject" Width="100%" runat=server/>
				            <asp:Label id=lblstoragemsg text="Please Select Storage" forecolor=red visible=false runat=server /></td>
                    </tr>
             
			        <tr>
				        <td style="width: 158px; height: 25px;">&nbsp;</td>
				        <td style="height: 25px; width: 510px;"><asp:CheckBox
                                    ID="chkCentralized" runat="server" AutoPostBack="true" Checked="True" OnCheckedChanged="Centralized_Type"
                                    Text="Centralized" Visible="False" /></td>
				        <td style="height: 25px">&nbsp;</td>
				        <td style="height: 25px; width: 119px;">&nbsp;</td>
				        <td style="height: 25px">&nbsp;</td>		
				        <td style="height: 25px">&nbsp;</td>
			        </tr>
			        <!--<tr>
				        <td height=25>&nbsp;</td>
				        <td>&nbsp;</td>
				        <td>&nbsp;</td>
				        <td><asp:Label id=lblDNIDTag visible=False Text="Debit Note ID :" runat=Server /></td>
				        <td><asp:Label id=lblDNNoteID  runat=server /></td>				
				        <td>&nbsp;</td>
			        </tr>-->
			        <tr>
				        <td colspan=6>&nbsp;<asp:label id=lblStockReceiveID Runat="server" Visible="False" /></td>		
			        </tr>
	            </table>
		
            <table border=0 width="100%" cellspacing="0" cellpadding="2" class="font9Tahoma" runat="server" >
                <tr>
                <td class="style4">
	
			<tr>
				<td colspan=6>
					<table id="tblPR" class="sub-Add" border=0 width="100%" cellspacing="0" cellpadding="4" runat="server" Visible=False>
						<tr class="mb-c" >
							<td width="20%" height="25"><asp:label id="lblSRType" Runat="server"/> </td>
							<td width="80%">
                                <asp:DropDownList id="lstPR" Width=50% CssClass="fontObject" AutoPostBack=True OnSelectedIndexchanged=RebindItemList runat=server />
							                &nbsp;<asp:ImageButton ImageAlign=AbsBottom ID=btnAddAllItem UseSubmitBehavior="false" onclick=BtnAddAllItem_Click CausesValidation=False ImageUrl="../../images/icn_next.gif" AlternateText="Add all items" Runat=server />  		
							                &nbsp;<asp:label id=lblErrDoc text="Please select one document" Visible=False forecolor=Red Runat="server" />
							</td>
						</tr>
					</table>
					
					<table id="tblAdd" class="sub-Add"  border="0" width="100%" cellspacing="0" cellpadding="2" runat="server">
						<tr class="mb-c">
							<td width="20%" valign="top">Item Code :*</td>
							<td width="80%"><asp:DropDownList id="lstItem" CssClass="fontObject" Width="50%" AutoPostBack=True OnSelectedIndexChanged=DisplayPRCost runat=server />
                                <asp:TextBox id=txtItemCode Width="25%" maxlength=15 CssClass="fontObject" AutoPostBack=True runat=server />
                                &nbsp;<asp:TextBox ID="TxtItemName" runat="server" Font-Bold="True" 
                                    ForeColor="Black" maxlength="10" Width="63%"></asp:TextBox>
                                &nbsp;<input type=button value=" ... " id="FindIN_Txt" class="button-small"  onclick="javascript:PopItem_New('frmMain', '', 'txtItemCode','TxtItemName','txtCost', 'False');" CausesValidation=False runat=server visible="true" />
                                <asp:label id=lblItemCodeErr text="Please select one Item" Visible=False forecolor=Red Runat="server" />
								<asp:label id=lblFertInd Visible=False Runat="server" /></td>
						</tr>
						<tr id=RowChargeTo class="mb-c">
							<td width="20%" height="25">Charge To :*</td>
							<td width="80%">
							    <asp:DropDownList id="ddlChargeTo" CssClass="fontObject" Width="95%" 
                                    AutoPostBack=True OnSelectedIndexChanged=ddlChargeTo_OnSelectedIndexChanged 
                                    runat=server /> 
            					<asp:label id=lblChargeToErr Visible=False forecolor=red Runat="server" />
			    		    </td>
						</tr>
						<tr id="RowAcc" class="mb-c">
							<td width="20%" height="25"><asp:label id="lblAccTag" Runat="server"/> </td>
							<td width="80%">
                                <asp:TextBox ID="txtAccCode" runat="server" AutoPostBack="True" CssClass="fontObject" maxlength="15" 
                                    Width="25%"></asp:TextBox>
                                <asp:TextBox ID="txtAccName" CssClass="fontObject" runat="server" Font-Bold="True" ForeColor="Black" 
                                    maxlength="10" Width="64%"></asp:TextBox>
&nbsp;<input id="Find" class="button-small" o runat="server" causesvalidation="False" 
                                    onclick="javascript:PopCOA_Desc('frmMain', '', 'txtAccCode', 'txtAccName', 'False');" 
                                    type="button" value=" ... " />&nbsp;
                                <BR><asp:label id=lblAccCodeErr Visible=False forecolor=red Runat="server" /></td>
						</tr>
						<tr id="RowChargeLevel" class="mb-c">
							<td height="25">Charge Level :* </td>
							<td><asp:DropDownList id="lstChargeLevel" CssClass="fontObject" Width="95%"  
                                    AutoPostBack=True OnSelectedIndexChanged=lstChargeLevel_OnSelectedIndexChanged 
                                    runat=server /> </td>
						</tr>
						<tr id="RowPreBlk" class="mb-c">
							<td height="25"><asp:label id=lblPreBlkTag Runat="server"/> </td>
							<td><asp:DropDownList id="lstPreBlock" CssClass="fontObject" Width="95%" 
                                    runat=server />
								<asp:label id=lblPreBlockErr Visible=False forecolor=red Runat="server" /></td>
						</tr>
						<tr id="RowBlk" class="mb-c">
							<td height="25"><asp:label id=lblBlkTag Runat="server"/> </td>
							<td><asp:DropDownList id="lstBlock" CssClass="fontObject" Width="95%" 
                                    runat=server />
								<asp:label id=lblBlockErr Visible=False forecolor=red Runat="server" /></td>
						</tr>
						<tr id="RowVeh" class="mb-c">
							<td><asp:label id="lblVehTag" Runat="server"/> </td>
							<td><asp:DropDownList id="lstVehCode" CssClass="fontObject" Width="95%" 
                                    runat=server />
								<asp:label id=lblVehCodeErr Visible=False forecolor=red Runat="server" /></td>
						</tr>		
						<tr id="RowVehExp" class="mb-c">
							<td><asp:label id="lblVehExpTag" Runat="server"/> </td>
							<td><asp:DropDownList id="lstVehExp" CssClass="fontObject" Width="95%" 
                                    runat=server />
								<asp:label id=lblVehExpCodeErr Visible=False forecolor=red Runat="server" /></td>
						</tr>																										
						<tr id="RowFromLoc" class="mb-c">
							<td height="25">Transfer From :* </td>
							<td><asp:DropDownList id="lstFromLoc" CssClass="fontObject" Width=50% runat=server /><br>
								<asp:label id=lblFromLocErr Visible=False forecolor=red Runat="server" />
								</td>
						</tr>										
						<tr class="mb-c">
							<td width="20%" height="25"><asp:label text="Quantity Received" EnableViewState="False" Runat="server" /> :*</td>
							<td width="80%">
								<asp:textbox id="txtQty" CssClass="fontObject" Width="25%" OnKeyUp="javascript:calAmount('1');" maxlength=20 Runat="server" />
								<asp:RegularExpressionValidator id="RegularExpressionValidatorQtyReq" 
									ControlToValidate="txtQty"
									ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
									Display="Dynamic"
									text = "Maximum length 15 digits and 5 decimal points"
									runat="server"/>
								<asp:RangeValidator id="Range1"
									ControlToValidate="txtQty"
									MinimumValue="0.00001"
									MaximumValue="999999999999999.99999"
									Type="double"
									EnableClientScript="True"
									Text="The value is out of acceptable range!"
									runat="server" display="dynamic"/>
									<asp:label id=lblErrQty text="<br>Quantity received cannot be blank!" Visible=False forecolor=red Runat="server" />								
							</td>
						</tr>
						<tr class="mb-c" id="RowCost" runat="server" >
							<td height="25">Unit Cost :*</td>
							<td>
								<asp:textbox id="txtCost" CssClass="fontObject" Width="25%" OnKeyUp="javascript:calAmount('2');" maxlength=19 Runat="server" />
								<asp:RegularExpressionValidator id="RegExpCost" 
									ControlToValidate="txtCost"
									ValidationExpression="\d{1,19}\.\d{0,2}|\d{1,19}"
									Display="Dynamic"
									text = "Maximum length 19 digits and 2 decimal points"
									runat="server"/>
									<asp:label id=lblErrUnitCost text="<br>Unit Cost cannot be blank!" Visible=False forecolor=red Runat="server" />								
							</td>
						</tr>
                        <tr class="mb-c" style="visibility:hidden">
                            <td height="25">
                                Ongkos Angkut</td>
                            <td>
                                <asp:textbox id="TxtNetTransPortFee" CssClass="fontObject" Width="25%" OnKeyUp="javascript:calAmount('2');" maxlength=19 Runat="server" Enabled="False" ReadOnly="True" />
                                <asp:RegularExpressionValidator id="RegularExpressionValidator1" 
									ControlToValidate="txtCost"
									ValidationExpression="\d{1,19}\.\d{0,2}|\d{1,19}"
									Display="Dynamic"
									text = "Maximum length 19 digits and 2 decimal points"
									runat="server"/></td>
                        </tr>
						<tr class="mb-c" runat="server" id="RowAMt">
							<td height="25"><asp:label text="Total Amount" EnableViewState="False" Runat="server" /> :*</td>
							<td>
								<asp:textbox id="txtAmount" CssClass="fontObject" Width="25%" OnKeyUp="javascript:calAmount('3');" maxlength=19 Runat="server" />
								<asp:RegularExpressionValidator id="RegExpamt" 
									ControlToValidate="txtAmount"
									ValidationExpression="\d{1,19}\.\d{0,2}|\d{1,19}"
									Display="Dynamic"
									text = "Maximum length 19 digits and 2 decimal points"
									runat="server"/>
									<asp:label id=lblErrTotalAmt text="Total amount cannot be blank!" Visible=False forecolor=red Runat="server" />																	
							</td>
						</tr>
						<tr class="mb-c">
							<td height="25">&nbsp;</td>
							<td>
								<asp:label id=lblerror text="Number generated is too big!" Visible=False forecolor=red Runat="server" />
								<asp:label id=lblStock text="Quantity oustanding in PR does not tally!" Visible=False forecolor=red Runat="server" />
								<asp:label id=lblPR text="Item not found in selected PR!" Visible=False forecolor=red Runat="server" />
							</td>
						</tr>
						<tr class="mb-c">
							<td colspan=2><asp:ImageButton text="Add" id="btnAdd" ImageURL="../../images/butt_add.gif" OnClick="btnAdd_Click" UseSubmitBehavior="false" Runat="server" />&nbsp;<asp:ImageButton
                                    ID="btnUpdate" runat="server" ImageUrl="../../images/butt_save.gif" OnClick="btnUpdate_Click"
                                    text="Save" Visible="false" />
                                <br />
                            </td>
						</tr>					
					</table>
				</td>
			</tr>
			<tr>
				<td colspan=6><asp:label id=lblConfirmErr text="Document must contain transaction to confirm!" Visible=False forecolor=red Runat="server" />
							  <asp:Label id=lblTxError visible=false Text="PR not found or Insufficient Quantity in PR to confirm!" forecolor=red runat=server /></td>				
			</tr>	
			<tr>
				<td colspan="6"> 
					<asp:DataGrid id="dgStkTx"
						AutoGenerateColumns="False" width="100%" runat="server"
						OnItemDataBound="DataGrid_ItemCreated" 
						GridLines = None
						Cellpadding = "2"
						Pagerstyle-Visible="False"
						OnDeleteCommand="DEDR_Delete"
						OnEditCommand="DEDR_Edit"
						OnCancelCommand="DEDR_Cancel"
       
						AllowSorting="True"  >	
						<HeaderStyle HorizontalAlign="Left" CssClass="mr-h" />							
						<ItemStyle CssClass="mr-l" />
						<AlternatingItemStyle CssClass="mr-r" />			
 				
					<Columns>
					<asp:TemplateColumn HeaderText="DA ID">
						<ItemStyle Width="8%"/> 																								
						<ItemTemplate>
							<asp:label text=<%# Trim(Container.DataItem("DocID")) %>  id="DocID" runat="server" />
							<asp:label text=<%# Container.DataItem("StockReceiveLnID") %> id="RecvLnID" visible=false runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Item">
						<ItemStyle Width="10%"/> 																								
						<ItemTemplate>
							<asp:label text=<%# Container.DataItem("ItemCode") %> id="ItemCode" visible=false runat="server" />
							<asp:label text=<%# Container.DataItem("Description") %> id="ItemDesc" runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Charge To">
						<ItemStyle Width="8%"/> 																								
						<ItemTemplate>
							<%# Container.DataItem("ChargeLocCode") %>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
						<ItemStyle Width="8%"/> 																								
						<ItemTemplate>
							<%# Container.DataItem("AccCode") %>
                            <asp:Label ID="AccCode" runat="server" Text='<%# Container.DataItem("AccCode") %>'
                                Visible="false"></asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
						<ItemStyle Width="8%"/> 																								
						<ItemTemplate>
							<%# Container.DataItem("BlkCode") %>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
						<ItemStyle Width="8%"/> 																								
						<ItemTemplate>
							<%# Container.DataItem("VehCode") %>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
						<ItemStyle Width="8%"/> 																								
						<ItemTemplate>
							<%# Container.DataItem("VehExpCode") %>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
						<ItemStyle Width="8%"/> 																								
						<ItemTemplate>
							<%# Container.DataItem("FromLocCode") %>
						</ItemTemplate>
					</asp:TemplateColumn>							
					<asp:TemplateColumn HeaderText="Quantity Received">
						<HeaderStyle HorizontalAlign="Right" />			
						<ItemStyle HorizontalAlign="Right" Width="8%"/>			
						<ItemTemplate>
							<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("Qty"),5) %> id="lblQtyTrx" runat="server" />							
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Unit Cost" Visible="False">
						<HeaderStyle HorizontalAlign="Right" />			
						<ItemStyle HorizontalAlign="Right" Width="8%" />							
						<ItemTemplate>
							<asp:label text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Cost"), 2), 2) %> id="lblUnitCost" runat="server" />
						</ItemTemplate>							
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Amount" Visible="False">
						<HeaderStyle HorizontalAlign="Right" />			
						<ItemStyle HorizontalAlign="Right" Width="8%" />						
						<ItemTemplate>
							<asp:label text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Amount"), 2), 2) %> id="lblAmount" runat="server" />
						</ItemTemplate>							
					</asp:TemplateColumn>		
					<asp:TemplateColumn>		
						<ItemStyle HorizontalAlign="Right" Width="5%"/>							
						<ItemTemplate>
							<asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" CausesValidation =False runat="server" />
							<asp:LinkButton id="Cancel" CommandName="Cancel" Text="Cancel" visible=False CausesValidation =False runat="server" />												
						    <asp:LinkButton id="Edit" CommandName="Edit" Text="Edit" CausesValidation =False runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>
					</Columns>										
                        <PagerStyle Visible="False" />
					</asp:DataGrid>				
				</td>
			</tr>	
			<tr style="visibility:hidden">
				<td colspan=3>&nbsp;</td>
				<td colspan=2 height=25><hr style="width :100%" /></td>
				<td width="5%">&nbsp;</td>					
			</tr>
			<tr style="visibility:hidden">
				<td colspan=3>&nbsp;</td>
				<td height=25 style="width: 119px">Total Amount : </td>
				<td align="right"><asp:label id="lblTotAmtFig" runat="server" /></td>						
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25 class="style4"><asp:label id="Remarks" text="Remarks :" runat="server" /></td>	
				<td colspan="5"><asp:textbox id="txtRemarks" CssClass="font9Tahoma" width="98%" maxlength="512" runat="server" /></td>
			</tr>
			<tr>
				<td colspan=6>&nbsp;</td>
			</tr>
			<tr>
				<td colspan=6><asp:label id=lblUnDel text="Insufficient Stock In Inventory to Perform Operation!" Visible=False forecolor=red Runat="server" />
								<asp:label id=lblBillPartyErr text="Please create a Bill Party for " Visible=False forecolor=red Runat="server" />
								<asp:label id=lblLocCodeErr text="" Visible=False forecolor=red Runat="server" />
								<asp:label id=lblPBBKB forecolor=Red runat=server Visible="False" />
				</td>				
			</tr>
			<tr>
				<td align="left" colspan="6">
					<asp:ImageButton id="btnNew"   UseSubmitBehavior="false" AlternateText="New"     onClick="btnNew_Click"     ImageURL="../../images/butt_new.gif"     CausesValidation=False runat="server" />
					<asp:ImageButton id="Save"     UseSubmitBehavior="false" AlternateText="Save"    onClick="btnSave_Click"    ImageURL="../../images/butt_save.gif"                           runat="server" />
					<asp:ImageButton id="Confirm"  UseSubmitBehavior="false" AlternateText="Confirm" onClick="btnConfirm_Click" ImageURL="../../images/butt_confirm.gif" CausesValidation=False runat="server" />
 					<asp:ImageButton id="Print"    UseSubmitBehavior="false" AlternateText="Print"   onClick="btnPrint_Click" ImageURL="../../images/butt_print.gif"   CausesValidation=False runat="server" />
					<asp:ImageButton id="PRDelete" UseSubmitBehavior="false" AlternateText="Delete"  onClick="btnDelete_Click"  ImageURL="../../images/butt_delete.gif"  CausesValidation=False runat="server" />
					<asp:ImageButton id="Back"     UseSubmitBehavior="false" AlternateText="Back"    onClick="btnBack_Click"    ImageURL="../../images/butt_back.gif"    CausesValidation=False runat="server"  />
				</td>
			</tr>		
			
			<tr>
				<td align="left" colspan="6">
					&nbsp;</td>
			</tr>		
		
				
			<tr>
				<td colspan=5>&nbsp;</td>
			</tr>
			<tr id=TrLink runat=server style="visibility:hidden">
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
						AllowSorting="false" class="font9Tahoma">	
						<HeaderStyle CssClass="mr-h"/>
						<ItemStyle CssClass="mr-l"/>
						<AlternatingItemStyle CssClass="mr-r"/>
                          <HeaderStyle  BackColor="#CCCCCC" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<ItemStyle BackColor="#FEFEFE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<AlternatingItemStyle BackColor="#EEEEEE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>	
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
			    <td class="style4">&nbsp;</td>								
			    <td height=25 align=right style="width: 510px"><asp:Label id=lblTotalViewJournal Visible=false runat=server /> </td>
			    <td>&nbsp;</td>	
			    <td align=right style="width: 119px"><asp:label id="lblTotalDB" text="0" Visible=false runat="server" /></td>						
			    <td>&nbsp;</td>		
			    <td align=right><asp:label id="lblTotalCR" text="0" Visible=false runat="server" /></td>				
		    </tr>

            </td>
            </tr>	
		    </table>
			<Input type=hidden id=hidBlockCharge value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>
			<Input type=hidden id=Real_DDL_Search value=",lstItem," runat=server/>
			<Input type=hidden id=lblPBBKB1 value=0 runat=server/>
            </div>
            </td>
        </tr>
       </table>
          
		</form>
	</body>
</html>
