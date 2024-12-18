<%@ Page Language="vb" src="../../../include/ap_trx_invrcv_wm_det.aspx.vb" Inherits="ap_trx_invrcv_wm_det" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<html>
	<head>
		<title>Weighing Credit Invoice Details</title>		
		<Preference:PrefHdl id="PrefHdl" runat="server" />
          <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	    <style type="text/css">
            .style1
            {
                width: 100%;
            }
            </style>
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
		<form id="frmMain" class="main-modul-bg-app-list-pu"  runat="server">
	        <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		<tr>
             <td style="width: 80%; height: 620px" valign="top">
			    <div class="kontenlist">  
                	
<table id="tblHeader" cellspacing="0" cellpadding="2" width="100%" border="0" class="font9Tahoma">
        <tr>
        <td>

		    <asp:Label id="lblStatusHidden" visible="false" text="0" runat="server" />
			<tr>
				<td  colspan="6">
                    <table cellpadding="0" cellspacing="0" class="style1">
                        <tr>
                            <td class="font9Tahoma">
                              <strong>  WEIGHING CREDIT INVOICE DETAILS</strong></td>
                            <td class="font9Header" style="text-align: right">
                                Status : <asp:Label id="lblStatus" runat="server" />&nbsp;|&nbsp; Last Update : <asp:Label ID="lblLastUpdate" runat="server" />&nbsp;| Date Created : <asp:Label id="lblDateCreated" runat="server" />&nbsp;| Updated By : <asp:Label ID="lblUpdatedBy" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
			</tr>
			<tr>
				<td colspan="5">
                    <hr style="width :100%" />
                </td>
			</tr>
			<tr style="height:25px">
				<td style="width:20%; height: 25px;">
                    Weighing Crd Inv ID*:</td>
				<td style="width:40%; height: 25px;">
                    <asp:TextBox ID="txtInvID" CssClass="font9Tahoma"  MaxLength="20" enabled="false" runat="server" Width="100%"></asp:TextBox>
                    
                    </td>
				<td style="width:5%; height: 25px;">&nbsp;</td>
				<td style="width:15%; height: 25px;">&nbsp;</td>
				<td style="width:20%; height: 25px;">&nbsp;</td>
				
			</tr>
			<tr style="height:25px" >
				<td style="width:20%; height: 25px;">
                    Weighing
                    Ref No* :</td>
				<td style="width:40%; height: 25px;">
                    <asp:TextBox ID="txtRefNo" CssClass="font9Tahoma"  MaxLength="20" runat="server" Width="100%"></asp:TextBox>
                    <asp:RequiredFieldValidator 
							id="ReqRefNo" 
							runat="server" 
							ErrorMessage="Please fill required field" 
							ControlToValidate="txtRefNo" 
							display="dynamic"/>
                    </td>
				<td style="width:5%; height: 25px;">&nbsp;</td>
				<td style="width:15%; height: 25px;">&nbsp;</td>
				<td style="width:20%; height: 25px;">&nbsp;</td>
			</tr>
		
			<tr style="height:25px">
				<td >
                    Transaction Date*:</td>
				<td>
                    <asp:TextBox ID="txtRefDate" CssClass="font9Tahoma"  runat="server" MaxLength="10" Width="50%"></asp:TextBox>
                    <a href="javascript:PopCal('txtRefDate');"><asp:Image id="btnSelDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
                        <asp:Label
                            ID="lblErrRefDate" runat="server" ForeColor="red" Text="Date format "></asp:Label></td>
				<td>&nbsp;</td>
				<td >&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr style="height:25px">
				<td>
                    Supplier Code*:</td>
				<td>
                    <asp:DropDownList ID="ddlSuppCode" CssClass="font9Tahoma"  runat="server" AutoPostBack="true" OnSelectedIndexChanged="onSelect_Supp"
                        Width="100%">
                    </asp:DropDownList>
                    <asp:Label id="lblErrSuppCode" runat="server" visible="false" text="Please select Supplier Code" forecolor="red"></asp:Label></td>
				<td>&nbsp;</td>
				<td >&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr style="height:25px">
				<td >
                    Account Code*:</td>
				<td >
                    <asp:DropDownList ID="ddlAccCode" CssClass="font9Tahoma"  runat="server" Width="100%"> </asp:DropDownList>
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
                    </td>
        </tr>
            </table>        
			<table  border="0" width="100%" cellspacing="0" cellpadding="4" runat="server">
            <tr>
            <td>

			<tr>
				<td colspan="5">
					<table id="tblSelection" border="0" width="100%" cellspacing="0" cellpadding="4" runat="server"  class="sub-Add">
						
						<tr style="height:25px" class="mb-c">
							<td style="width:20%; height: 25px;">
                                Date :*</td>
							<td colspan="4" style="height: 25px">
                                <asp:DropDownList id="ddlTicket" Width="50%" CssClass="font9Tahoma" AutoPostBack="true" OnSelectedIndexChanged="onSelect_Ticket" runat="server" /> 
								<asp:label id="lblErrTicket" Visible="False" forecolor="Red" Runat="server" >Please Select Date.</asp:label></td>
						</tr>
						
						<tr style="height:25px" class="mb-c">
							<td style="width:20%; height: 25px;">
                                Unit Price:*</td>
							<td colspan="4">
                                <asp:Textbox id="txtUnitPrice" CssClass="font9Tahoma"  Width="50%" maxlength="22" OnKeyUp="javascript:calTotalCost();" runat="server" />
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
				                <asp:TextBox ID="txtTotalWeight" CssClass="font9Tahoma" runat="server" Width="50%" ReadOnly="true"></asp:TextBox>
                            </td>
			            </tr>
						
						<tr style="height:25px" class="mb-c">
							<td style="width:20%; height: 25px;">
                                Amount:*</td>
							<td colspan="4">
                                <asp:Textbox id="txtAmount"  CssClass="font9Tahoma" Width="50%" maxlength="22" OnKeyUp="javascript:calUnitCost();" runat="server" />
                                <asp:label id="lblErrAmount" CssClass="font9Tahoma" Visible="False" forecolor="Red" Runat="server" >Please fill Amount</asp:label>
                                 
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
						OnItemDataBound="dgTicketList_BindGrid"						
						AllowSorting="True" class="font9Tahoma">	
                                                      <HeaderStyle  BackColor="#CCCCCC" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<ItemStyle BackColor="#FEFEFE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<AlternatingItemStyle BackColor="#EEEEEE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>	    				
						<AlternatingItemStyle CssClass="mr-r" />		
					<Columns>
					
					<asp:TemplateColumn HeaderText="No.">
						<ItemStyle width="5%"/>
						<ItemTemplate>
							<asp:Label Text='<%# Container.DataItem("NoUrut") %>' id="lblNoUrut" runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Ticket No">
						<ItemStyle width="16%"/>
						<ItemTemplate>
							<%# Container.DataItem("TicketNo") %>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="SPB No">
						<ItemStyle width="16%"/>
						<ItemTemplate>
							<%# Container.DataItem("SPBkirim") %>
						</ItemTemplate>
					</asp:TemplateColumn>					
					<asp:TemplateColumn HeaderText="Date">
						<ItemStyle width="10%"/>
						<ItemTemplate>
							<asp:Label Text='<%# objGlobal.GetLongDate(Container.DataItem("InDate")) %>' id="lblInDate" runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Product">
						<ItemStyle width="5%"/>
						<ItemTemplate>
							<asp:Label Text='<%# objWMTrx.mtdGetWeighBridgeTicketProduct(Container.DataItem("ProductCode")) %>' id="lblProdCode" runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Year">
						<ItemStyle width="5%"/>
						<ItemTemplate>
							<%# Container.DataItem("BlkCode") %>
						</ItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Net Weight">
						<ItemStyle width="10%"/>
						<ItemTemplate>
							<%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("NetWeight"), 0), 0)%>
						</ItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Unit Price">
						<ItemStyle width="10%"/>
						<ItemTemplate>
							<asp:Label Text='<%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Price"), 2), 2)%>' id="lblPrice" runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Amount">
						<ItemStyle width="10%"/>
						<ItemTemplate>
							<%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Amount"), 0), 0)%>
						</ItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="PPN">
						<ItemStyle width="10%"/>
						<ItemTemplate>
							<%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PPN"), 0), 0)%>
						</ItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="PPh">
						<ItemStyle width="10%"/>
						<ItemTemplate>
							<%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PPh"), 0), 0)%>
						</ItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Ongkos Bongkar">
						<ItemStyle width="10%"/>
						<ItemTemplate>
							<%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("OngkosBongkar"), 0), 0)%>
						</ItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Ongkos Lapangan">
						<ItemStyle width="10%"/>
						<ItemTemplate>
							<%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("OngkosLapangan"), 0), 0)%>
						</ItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Right">
								<ItemTemplate>
								    <asp:label id=lnid visible="false" text=<%# Container.DataItem("LineID")%> runat="server" />
								    <asp:label id=lnamount visible="false" text=<%# Container.DataItem("Amount")%> runat="server" />
						            <asp:label id=lnweight visible="false" text=<%# Container.DataItem("NetWeight")%> runat="server" />

									<asp:LinkButton id="lbDelete" CommandName="Delete" Text="Delete" CausesValidation="False" runat="server" />
								    
								</ItemTemplate>

<ItemStyle HorizontalAlign="Right" Width="5%"></ItemStyle>
							</asp:TemplateColumn>	
					</Columns>										
						<HeaderStyle CssClass="mr-h" />							
						<ItemStyle CssClass="mr-l" />

<PagerStyle Visible="False"></PagerStyle>
					</asp:DataGrid>
				</td>	
			</tr>
			
			<tr visible=false runat=server>
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
					<asp:ImageButton ID="PrintBtn" onclick="PrintBtn_Click" ImageUrl="../../images/butt_print.gif" AlternateText="Print" Runat="server" />
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
			<tr  class="font9Tahoma">
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
			    <td>&nbsp;</td>								
			    <td height=25 align=right><asp:Label id=lblTotalViewJournal  CssClass="font9Tahoma" Visible=false runat=server /> </td>
			    <td>&nbsp;</td>	
			    <td align=right><asp:label id="lblTotalDB"  CssClass="font9Tahoma" text="0" Visible=false runat="server" /></td>						
			    <td>&nbsp;</td>		
			    <td align=right><asp:label id="lblTotalCR"  CssClass="font9Tahoma" text="0" Visible=false runat="server" /></td>				
		    </tr>
		</table>
		    </div>
            </td>
            </tr>
            </table>
		</form>
	</body>
</html>
