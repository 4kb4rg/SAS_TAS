<%@ Page Language="vb" src="../../../include/ap_trx_invrcv_wm_det.aspx.vb" Inherits="ap_trx_invrcv_wm_det" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<html>
	<head>
		<title>Weighing Credit Invoice Details</title>		
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</head>
	
	<script language="javascript">			
			function calTotalCost() {
				var doc = document.frmMain;
				var a = parseFloat(doc.txtUnitPrice.value);
				var b = parseFloat(doc.txtTotalWeight.value);
				doc.txtAmount.value = a * b;
				if (doc.txtAmount.value == 'NaN')
					doc.txtAmount.value = '';
				else
					doc.txtAmount.value = round(doc.txtAmount.value, 0);
			}			
			function calUnitCost() {
				var doc = document.frmMain;
				var a = parseFloat(doc.txtAmount.value);
				var b = parseFloat(doc.txtTotalWeight.value);
				doc.txtUnitPrice.value = a / b;
				if (doc.txtUnitPrice.value == 'Infinity')
					doc.txtUnitPrice.value = '';
				else
					doc.txtUnitPrice.value = round(doc.txtUnitPrice.value, 0);
			}
			
	</script>	
	
	
	<body >
		<form id="frmMain" runat="server">
		
		<table id="tblHeader" cellspacing="0" cellpadding="2" width="100%" border="0">
		    <asp:Label id="lblStatusHidden" visible="false" text="0" runat="server" /><tr>
			</tr>
			<tr>
				<td class="mt-h" colspan="6">WEIGHING CREDIT INVOICE DETAILS</td>
			</tr>
			<tr>
				<td colspan="5"><hr size="1" noshade > </td>
			</tr>
			<tr style="height:25px">
				<td style="width:20%; height: 25px;">
                    Weighing Crd Inv ID*:</td>
				<td style="width:40%; height: 25px;">
                    <asp:TextBox ID="txtInvID" MaxLength="20" enabled="false" runat="server" Width="100%"></asp:TextBox>
                    
                    </td>
				<td style="width:5%; height: 25px;">&nbsp;</td>
				<td style="width:15%; height: 25px;">Status :</td>
				<td style="width:20%; height: 25px;"><asp:Label id="lblStatus" runat="server" /></td>
				
			</tr>
			<tr style="height:25px" >
				<td style="width:20%; height: 25px;">
                    Weighing
                    Ref No* :</td>
				<td style="width:40%; height: 25px;">
                    <asp:TextBox ID="txtRefNo" MaxLength="20" runat="server" Width="100%"></asp:TextBox>
                    <asp:RequiredFieldValidator 
							id="ReqRefNo" 
							runat="server" 
							ErrorMessage="Please fill required field" 
							ControlToValidate="txtRefNo" 
							display="dynamic"/>
                    </td>
				<td style="width:5%; height: 25px;">&nbsp;</td>
				<td style="width:15%; height: 25px;">Last Update :</td>
				<td style="width:20%; height: 25px;"><asp:Label ID="lblLastUpdate" runat="server" /></td>
			</tr>
		
			<tr style="height:25px">
				<td >
                    Transaction Date*:</td>
				<td>
                    <asp:TextBox ID="txtRefDate" runat="server" MaxLength="10" Width="50%"></asp:TextBox>
                    <a href="javascript:PopCal('txtRefDate');"><asp:Image id="btnSelDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
                        <asp:Label
                            ID="lblErrRefDate" runat="server" ForeColor="red" Text="Date format "></asp:Label></td>
				<td>&nbsp;</td>
				<td >Date Created :</td>
				<td><asp:Label id="lblDateCreated" runat="server" /></td>
			</tr>
			<tr style="height:25px">
				<td>
                    Supplier Code*:</td>
				<td>
                    <asp:DropDownList ID="ddlSuppCode" runat="server" AutoPostBack="true" OnSelectedIndexChanged="onSelect_Supp"
                        Width="100%">
                    </asp:DropDownList>
                    <asp:Label id="lblErrSuppCode" runat="server" visible="false" text="Please select Supplier Code" forecolor="red"></asp:Label></td>
				<td>&nbsp;</td>
				<td >Updated By :</td>
				<td><asp:Label ID="lblUpdatedBy" runat="server" /></td>
			</tr>
			<tr style="height:25px">
				<td >
                    Account Code*:</td>
				<td >
                    <asp:DropDownList ID="ddlAccCode" runat="server" Width="100%"> </asp:DropDownList>
                    <asp:Label id="lblErrAccCode" runat="server" visible="false" text="Please select account code" forecolor="red"></asp:Label>
                    </td>
				
				<td></td>
			</tr>
			<tr>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>				
			</tr>
			<tr>
				<td colspan="5">
					<table id="tblSelection" border="0" width="100%" cellspacing="0" cellpadding="4" runat="server">
						
						<tr style="height:25px" class="mb-c">
							<td style="width:20%; height: 25px;">
                                Date :*</td>
							<td colspan="4" style="height: 25px">
                                <asp:DropDownList id="ddlTicket" Width="50%" AutoPostBack="true" OnSelectedIndexChanged="onSelect_Ticket" runat="server" /> 
								<asp:label id="lblErrTicket" Visible="False" forecolor="Red" Runat="server" >Please Select Date.</asp:label></td>
						</tr>
						
						<tr style="height:25px" class="mb-c">
							<td style="width:20%; height: 25px;">
                                Unit Price:*</td>
							<td colspan="4">
                                <asp:Textbox id="txtUnitPrice"  Width="50%" maxlength="22" OnKeyUp="javascript:calTotalCost();" runat="server" />
                                <asp:label id="lblErrUnitPrice" Visible="False" forecolor="Red" Runat="server" >Please fill Unit Price</asp:label>
                                  <asp:RegularExpressionValidator id="revtxtUnitPrice" 
							        ControlToValidate="txtUnitPrice"
							        ValidationExpression="\d{1,21}\.\d{1,4}|\d{1,21}"
							        Display="Dynamic"
							        text = "Maximum length 21 digits and 4 decimal points"
							        runat="server"/>
                                
                                </td>
						</tr>
							
						 <tr style="height:25px" class="mb-c">
				            <td >
                                Total Weight*:</td>
				            <td colspan="4">
				                <asp:TextBox ID="txtTotalWeight" runat="server" Width="50%" ReadOnly="true"></asp:TextBox>
                            </td>
			            </tr>
						
						<tr style="height:25px" class="mb-c">
							<td style="width:20%; height: 25px;">
                                Amount:*</td>
							<td colspan="4">
                                <asp:Textbox id="txtAmount"  Width="50%" maxlength="22" OnKeyUp="javascript:calUnitCost();" runat="server" />
                                <asp:label id="lblErrAmount" Visible="False" forecolor="Red" Runat="server" >Please fill Amount</asp:label>
                                 
                                 <asp:RegularExpressionValidator id="revtxtAmount" 
							        ControlToValidate="txtAmount"
							        ValidationExpression="\d{1,21}\.\d{1,2}|\d{1,21}"
							        Display="Dynamic"
							        text = "Maximum length 21 digits and 2 decimal points"
							        runat="server"/>
                               
                                
                                </td>
                                
                            
						</tr>						
						<tr style="height:25px" class="mb-c">
							<td colspan="3">
							    <asp:ImageButton  id="AddDtlBtn" ImageURL="../../images/butt_add.gif" OnClick="AddDtlBtn_Click" Runat="server" />
							</td>
							<td>&nbsp;</td>						
							<td>&nbsp;</td>	
						</tr>
					</table>
				</td>		
			</tr>
			<tr>
			    <td colspan="5" style="height: 42px">&nbsp;<asp:label id="lblErrMessage" visible="false" forecolor="Red" Text="Error while initiating component." runat="server" /></td>	    
				<td colspan="5" style="height: 42px">&nbsp;<asp:label id="lblConfirmErr" text="<BR>Cannot Confirm The Transaction Please Check The Data" Visible="False" forecolor="red" Runat="server" /></td>
			</tr>
			<tr>
				<td colspan="5"> 
					<asp:DataGrid id="dgLineDet"
						AutoGenerateColumns="false" width="100%" runat="server"
						GridLines = "none"
						Cellpadding = "2"
						Pagerstyle-Visible="False"
						OnDeleteCommand="DEDR_Delete"							
						AllowSorting="True">	
						<HeaderStyle CssClass="mr-h" />							
						<ItemStyle CssClass="mr-l" />
						<AlternatingItemStyle CssClass="mr-r" />						
					<Columns>
					
					<asp:TemplateColumn HeaderText="Ticket No">
						<ItemStyle width="16%"/>
						<ItemTemplate>
							<%# Container.DataItem("TicketNo") %>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Date">
						<ItemStyle width="10%"/>
						<ItemTemplate>
							<%# objGlobal.GetLongDate(Container.DataItem("InDate")) %>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Product">
						<ItemStyle width="10%"/>
						<ItemTemplate>
							<%# objWMTrx.mtdGetWeighBridgeTicketProduct(Container.DataItem("ProductCode")) %>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Year">
						<ItemStyle width="10%"/>
						<ItemTemplate>
							<%# Container.DataItem("BlkCode") %>
						</ItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Net Weight">
						<ItemStyle width="10%"/>
						<ItemTemplate>
							<%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("NetWeight"), 5), 5)%>
						</ItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Unit Price">
						<ItemStyle width="10%"/>
						<ItemTemplate>
							<%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Price"), 2), 2)%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Amount">
						<ItemStyle width="10%"/>
						<ItemTemplate>
							<%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Amount"), 0), 0)%>
						</ItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Right">
								<ItemTemplate>
								    <asp:label id=lnid visible="false" text=<%# Container.DataItem("LineID")%> runat="server" />
								    <asp:label id=lnamount visible="false" text=<%# Container.DataItem("Amount")%> runat="server" />
						            <asp:label id=lnweight visible="false" text=<%# Container.DataItem("NetWeight")%> runat="server" />

									<asp:LinkButton id="lbDelete" CommandName="Delete" Text="Delete" CausesValidation="False" runat="server" />
								    
								</ItemTemplate>
							</asp:TemplateColumn>	
					</Columns>										
					</asp:DataGrid>
				</td>	
			</tr>
			
			<tr>
				<td colspan="3">&nbsp;</td>								
				<td align="right" >Total :
                </td>						
				<td>&nbsp;<asp:label id="lblTotAmt" text="0" runat="server" /></td>					
			</tr>
			
			<tr>
				<td colspan="5">&nbsp;</td>
			</tr>
			
			
			<tr>
				<td colspan="5">
					<asp:ImageButton ID="SaveBtn" onclick="SaveBtn_Click" ImageUrl="../../images/butt_save.gif" AlternateText="Save" Runat="server" /> 
					<asp:ImageButton ID="ConfirmBtn" onclick="ConfirmBtn_Click" ImageUrl="../../images/butt_confirm.gif" AlternateText="Confirm" Runat="server" />
					<asp:ImageButton ID="PrintBtn" onclick="PrintBtn_Click" ImageUrl="../../images/butt_print.gif" AlternateText="Print" visible="false" Runat="server" />
					<asp:ImageButton ID="DeleteBtn" onclick="DeleteBtn_Click" CausesValidation=false ImageUrl="../../images/butt_delete.gif" AlternateText="Delete" Runat="server" />
					<asp:ImageButton ID="BackBtn" CausesValidation="False" onclick="BackBtn_Click" ImageUrl="../../images/butt_back.gif" AlternateText="Back" Runat="server" />
				    <input type="hidden" id="inrid" value="" runat="server" />
				</td>
			</tr>
			
			
				
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
		    
		</form>
	</body>
</html>
