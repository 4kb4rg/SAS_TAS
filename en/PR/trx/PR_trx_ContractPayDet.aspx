	<%@ Page Language="vb" src="../../../include/PR_trx_ContractPayDet.aspx.vb" Inherits="PR_trx_ContractPayDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRTrx" src="../../menu/menu_prtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Contract Payment Details</title>
         <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain  class="main-modul-bg-app-list-pu" runat="server">
                    <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 

			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:Label id="lblSelect" visible="false" text="Select " runat="server" />
			<asp:Label id="lblErrSelect" visible="false" text="Please select one " runat="server" />
			<asp:Label id="lblCode" visible="false" text=" Code" runat="server" />
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
            <tr>
            <td>

				<tr>
					<td colspan="6"><UserControl:MenuPRTrx id=MenuPRTrx runat="server" /></td>
				</tr>
				<tr>
				<td  colspan="6"><strong>CONTRACT PAYMENT DETAILS 
                    <br />
                    </strong>                   
                    </td>
				</tr>
				<tr>
					<td colspan=6> <hr style="width :100%" /></td>
				</tr>
				<tr>
					<td width=15% height=25>Contract ID : </td>
					<td width=50%><asp:Label id=lblContractID runat=server/></td>
					<td width=3%>&nbsp;</td>
					<td width=15%>Period : </td>
					<td width=25%><asp:Label id=lblPeriod runat=server /></td>
					
				</tr>
				<tr>
					<td height=25><asp:label id="lblAccount" runat="server" /> (DR) :* </td>
					<td><asp:DropDownList id=ddlAccount width=93% onselectedindexchanged=onSelect_Account autopostback=true runat=server/> 
						<input type="button" id=btnFind1 value=" ... " onclick="javascript:findcode('frmMain','','ddlAccount','9','ddlBlock','','ddlVehCode','ddlVehExpCode','','','','','','','','',hidBlockCharge.value,hidChargeLocCode.value);" runat=server/>
						<asp:Label id=lblErrAccount visible=false forecolor=red runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Status : </td>
					<td><asp:Label id=lblStatus runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id="lblBlock" runat="server" /> :</td>
					<td><asp:DropDownList id=ddlBlock width=100% runat=server/>
						<asp:Label id=lblErrBlock visible=false forecolor=red runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Date Created : </td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id="lblVehicle" runat="server" /> :</td>
					<td><asp:Dropdownlist id=ddlVehCode width=100% runat=server/>
						<asp:Label id=lblErrVehicle visible=false forecolor=red runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Last Updated : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id="lblVehExpense" runat="server" /> :</td>
					<td><asp:Dropdownlist id=ddlVehExpCode width=100% runat=server/>
						<asp:Label id=lblErrVehExp visible=false forecolor=red runat=server/>												
					</td>
					<td>&nbsp;</td>
					<td>Updated By : </td>
					<td><asp:Label id=lblUpdatedBy runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>Print Date : </td>
					<td><asp:Label id=lblPrintDate runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Quantity Contracted :*</td>
					<td>
						<!-- Based on HoiYee, this Qty is not calculated, so is ok if the datatype in table is nchar -->
						<!-- but , the question is I will ask, why in this form the data type is DOUBLE data type ? -->
						
						<asp:Textbox id=txtQty width=100% maxlength=64 runat=server/>
						<!-- quick fix by dian on 2/11/2006 to add validation for Quantity Contracted -->
						<asp:RequiredFieldValidator id=validateQtyContr display=dynamic runat=server 
									ControlToValidate=txtQty ErrorMessage="Please insert Quantity Contracted" />
						<asp:CompareValidator id="cvQty" display=dynamic runat="server" 
							ControlToValidate="txtQty" Text="<br>The value must whole number or with decimal. " 
							Type="Double" Operator="DataTypeCheck"/>
						<asp:RegularExpressionValidator id=revQty 
							ControlToValidate="txtQty"
							ValidationExpression="\d{1,62}\.\d{1,2}|\d{1,64}"
							Display="Dynamic"
							text = "Maximum 2 decimal points. "
							runat="server"/>
					</td>
					<td>&nbsp;</td>
					
					<!-- Based on HoiYee, this Qty is not calculated, so is ok if the datatype in table is nchar -->
					<!-- but , the question is I will ask, why in this form the data type is DOUBLE data type ? -->
					<td>Quantity Completed :</td>
					<td><asp:Textbox id=txtQtyCompleted width=100% maxlength=64 runat=server/>
						<asp:CompareValidator id="cvQtyCompleted" display=dynamic runat="server" 
							ControlToValidate="txtQtyCompleted" Text="<br>The value must whole number or with decimal. " 
							Type="Double" Operator="DataTypeCheck"/>
						<asp:RegularExpressionValidator id=revQtyCompleted 
							ControlToValidate="txtQtyCompleted"
							ValidationExpression="\d{1,62}\.\d{1,2}|\d{1,64}"
							Display="Dynamic"
							text = "Maximum 2 decimal points. "
							runat="server"/>
						<asp:Label id=lblErrQtyClose visible=false forecolor=red text="<br>Please enter Quantity Completed." runat=server/>
					</td>
					<td>&nbsp;</td>
				</tr>						
				<tr>
					<td height=25>Price Contracted :*</td>
					<!-- Modified BY ALIM maxlength=22 -->
					<td><asp:Textbox id=txtAmt width=100% maxlength=22 runat=server/>
						<asp:CompareValidator id="cvAmt" display=dynamic runat="server" 
							ControlToValidate="txtAmt" Text="<br>The value must whole number or with decimal. " 
							Type="Double" Operator="DataTypeCheck"/>
						<!-- Modified BY ALIM -->	
						<asp:RegularExpressionValidator id=revAmt 
							ControlToValidate="txtAmt"
							ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "Maximum length 19 digits and 2 decimal points. "
							runat="server"/>
						<asp:Label id=lblErrNull visible=false forecolor=red text="<br>Please enter Quantity or Price." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Actual Payment :</td>
					<!-- Modified BY ALIM maxlength=22 -->
					<td><asp:Textbox id=txtPayAmt width=100% maxlength=22 runat=server/>
						<asp:CompareValidator id="cvPayAmt" display=dynamic runat="server" 
							ControlToValidate="txtPayAmt" Text="<br>The value must whole number or with decimal. " 
							Type="Double" Operator="DataTypeCheck"/>
						<!-- Modified BY ALIM -->		
						<asp:RegularExpressionValidator id=revPayAmt 
							ControlToValidate="txtPayAmt"
							ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "Maximum length 19 digits and 2 decimal points. "
							runat="server"/>
						<asp:Label id=lblErrAmtClose visible=false forecolor=red text="<br>Please enter Contract Price and Actual Amount." runat=server/>
					</td>
					<td>&nbsp;</td>
				</tr>
                        </td>
            </tr>
                </table>
            <table  width="100%" class="mb-c" cellspacing="0" cellpadding="4" border="0" align="center" runat=server>
            <tr>
            <td>
 
				<tr >
					<td colspan=6>
						<table id="tblSelection" width="100%" class="sub-add"cellspacing="0" cellpadding="4" border="0" align="center" runat=server>
							<tr>						
								<td>
									<table border=0 cellpadding=2 cellspacing=0 width=100% class="font9Tahoma">
										<tr>
											<td height=25 width=20%>Contractor :*</td>
											<td width=80%>
												<asp:DropDownList id=ddlContractor width=100% runat=server/>
												<asp:Label id=lblErrContractor visible=false forecolor=red text="<br>Please select one contractor." runat=server/>
											</td>
										</tr>
										<tr>
											<td height=25 width=20%>Amount :</td>
											<td width=80%>
												<!-- Modified BY ALIM maxlength=22 -->
												<asp:Textbox id=txtAmount width=50% maxlength=22 runat=server/>
												<asp:CompareValidator id="cvAmount" display=dynamic runat="server" 
													ControlToValidate="txtAmount" Text="<br>The value must whole number or with decimal. " 
													Type="Double" Operator="DataTypeCheck"/>
												<!-- Modified BY ALIM -->
												<asp:RegularExpressionValidator id=revAmount 
													ControlToValidate="txtAmount"
													ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
													Display="Dynamic"
													text = "Maximum length 19 digits and 2 decimal points. "
													runat="server"/>
											</td>
										</tr>
										<tr class="mb-c">
											<TD colspan=2 height=25><asp:ImageButton id=btnAdd imageurl="../../images/butt_add.gif" alternatetext=Add onclick=btnAdd_Click runat=server />&nbsp;</TD>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="6">
						<asp:DataGrid id=dgLineDet
							AutoGenerateColumns=false width="100%" runat=server
							GridLines=none
							Cellpadding=2
							OnDeleteCommand=DEDR_Delete 
							Pagerstyle-Visible=False
							AllowSorting="True" CssClass="font9Tahoma" >
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
								<asp:TemplateColumn ItemStyle-Width="25%" HeaderText="Contractor Code">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("ContractorCode") %> visible=true id="lblContractorCode" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn ItemStyle-Width="40%" HeaderText="Name">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("ContractorName") %> id="lblContractorName" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn ItemStyle-Width="30%" HeaderText="Amount" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
									<ItemTemplate>
										<asp:Label Text=<%# ObjGlobal.GetIDDecimalSeparator(Container.DataItem("Amount")) %> id="lblAmount" runat="server" /> <!-- Modified BY ALIM -->									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn ItemStyle-Width="5%" ItemStyle-HorizontalAlign=Right>
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("ContractLnId") %> visible=false id="lblContractLnId" runat="server" />
										<asp:Label Text=<%# Container.DataItem("PayrollInd") %> visible=false id="lblPayrollInd" runat="server" />
										<asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>	
							</Columns>
						</asp:DataGrid>
					</td>
				</tr>
				<tr>
					<td colspan=3>&nbsp;</td>
					<td colspan=2 height=25><hr style="width :100%" /> </td>
					<td width="5%">&nbsp;</td>					
				</tr>	
				<tr>
					<td colspan=3>&nbsp;</td>			
					<td height=25>Contractors Amount :</td>
					<td align=right><asp:label id="lblTotalAmount" runat="server" />							
									<asp:Label id=lblErrTotalAmount visible=false forecolor=red text="<br>The contractors amount and the actual amount is not same." runat=server/>
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=6>&nbsp;</td>				
				</tr>						
				<tr>
					<td height=25>Remarks :</td>
					<td colspan="5"><asp:Textbox id=txtRemark value="" width=100% runat=server/></td>
				</tr>				
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6">
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=CancelBtn AlternateText=" Cancel " imageurl="../../images/butt_cancel.gif" onclick=Button_Click CommandArgument=Cancel runat=server />
						<asp:ImageButton id=CloseBtn AlternateText=" Close " imageurl="../../images/butt_close.gif" onclick=Button_Click CommandArgument=Close runat=server />						
						<asp:ImageButton id="PrintBtn" Text="Print" onClick=btnPreview_Click ImageURL="../../images/butt_print.gif" CausesValidation=false runat="server" />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					    <br />
					</td>
				</tr>
				<Input Type=Hidden id=payid runat=server />
				<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
				<asp:Label id=lblVehicleOption visible=false text=false runat=server/>
			</table>
			<Input type=hidden id=hidBlockCharge value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>
			<Input type=hidden id=hidAllowClose value="no" runat=server />
        </div>
        </td>
        </tr>
                   </td>
            </tr>
        </table>
		</form>
	</body>
</html>
