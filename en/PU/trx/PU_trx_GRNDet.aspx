<%@ Page Language="vb" trace=false src="../../../include/PU_trx_GRNDet.aspx.vb" Inherits="PU_GRNDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPU" src="../../menu/menu_putrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Goods Return Details</title>		
		<Preference:PrefHdl id=PrefHdl runat="server" />
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	    <style type="text/css">
            .style1
            {
                height: 16px;
            }
            .style2
            {
                width: 100%;
            }
            .style3
            {
                height: 4px;
            }
            .style4
            {
                width: 21%;
            }
            .style5
            {
                height: 4px;
                width: 21%;
            }
            </style>
	</head>
	<body>
		<form id=frmMain class="main-modul-bg-app-list-pu"  runat=server>
			<asp:Label id=lblErrMessage visible=False Text="Error while initiating component." runat=server />	
			<asp:label id=lblCode visible=false text=" Code" runat=server />
			<asp:label id=lblID visible=false text=" ID" runat=server/>
			<asp:label id=lblPleaseSelect visible=false text="Please select " runat="server" />
			<asp:label id=lblPleaseSelectOne visible=false text="Please select one " runat=server />	
			<asp:label id=lblDocument visible=false text="/Goods Return Advice ID/Goods Receive ID/Item Code for item listing." runat=server/>	
			<asp:label id=lblErrOnHand visible=false text="Insufficient quantity on hand" runat=server />		
			<asp:label id=lblErrOnHold visible=false text="Insufficient quantity on hold" runat=server />		
			<asp:Label id=lblVehicleOption visible=false text=false runat=server />
			<asp:label id=lblInvoiceRcvTag visible=false text=false runat=server/>
			<asp:Label id=lblHidStatus visible=false runat=server />
			<input type=hidden id=GoodsRetId runat=server />
			<input type=hidden id=hidItemType runat=server />

             <table cellpadding="0" cellspacing="0" style="width: 100%">
		     <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">  

			<table border="0" cellspacing="0" cellpadding="1" width="100%" class="font9Tahoma" >
				<tr>
					<td colspan="6"><UserControl:MenuPU id=menuPU runat="server" /></td>
				</tr>			
				<tr>
					<td class="style1" colspan="6">
                        <table cellpadding="0" cellspacing="0" class="style2">
                            <tr>
                                <td class="font9Tahoma">
                                 <strong>GOODS RETURN DETAILS</strong> </td>
                                <td style="text-align: right" class="font9Header">
                                    Period: <asp:Label id=lblAccPeriod runat=server />|Status :<asp:Label id=lblStatus runat=server />|Date Created 
                                    : <asp:Label id=lblCreateDate runat=server />|Last Update : <asp:Label id=lblUpdateDate runat=server />|Update By 
                                    :&nbsp; <asp:Label id=lblUpdateBy runat=server />&nbsp;|Print Date : <asp:Label id=lblPrintDate runat=server /> </td>
                            </tr>
                        </table>
                    </td>
				</tr>
				<tr>
					<td colspan="6"><hr style="width :100%" />   
                            </td>
				</tr>
				<tr>
					<td height="25" class="style4">Goods Return ID :</td>
					<td width="40%"><asp:Label id=lblGoodsRetId runat=server /></td>
					<td width="5%">&nbsp;</td>
					<td width="15%">&nbsp;</td>
					<td width="20%">&nbsp;</td>
					<td width="5%">&nbsp;</td>					
				</tr>
				<tr>
					<td height="25" class="style4">Goods Return Date :*</td>
					<td><asp:TextBox id=txtGoodsRetDate class="fontObject" width=30% maxlength=10 runat=server />
						<a href="javascript:PopCal('txtGoodsRetDate');"><asp:Image id="btnSelDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>						
						<asp:label id=lblDate Text ="<br>Date Entered should be in the format " forecolor=red Visible = false Runat="server"/> 
				        <asp:label id=lblFmt  forecolor=red Visible = false Runat="server"/> 
						<asp:RequiredFieldValidator 
							id="rfvGoodsRetDate" 
							runat="server" 
							ErrorMessage="<br>Please key in Goods Return Date" 
							ControlToValidate="txtGoodsRetDate" 
							display="dynamic"/></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td width="5%">&nbsp;</td>
				</tr>	
				<tr>
					<td height="25" class="style4">Goods Return Type :</td>
					<td><asp:Label id=lblGoodsRetType visible=false runat=server />
						<asp:Label id=lblGoodsRet runat=server /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td width="5%">&nbsp;</td>
				</tr>				
				<tr>
					<td class="style5">Supplier Code :*</td>
					<td class="style3">
                        <asp:TextBox ID="txtSupCode" class="fontObject" runat="server" AutoPostBack="False" MaxLength="15" 
                            Width="30%"></asp:TextBox>
                        <input id="Button1"  class="button-small" runat="server" causesvalidation="False" 
                            onclick="javascript:PopSupplier_New('frmMain','','txtSupCode','txtSupName','txtCreditTerm','txtPPN','txtPPNInit', 'False');" 
                            type="button" value=" ... " visible="true" /> 
                        <asp:Button ID="BtnGetData" 
                           runat="server" Font-Bold="True"  onclick="GetSupplierBtn_Click" Text="Click Here" 
                            ToolTip="Click For Refresh COA " Width="68px" CssClass="button-small" />
                        <asp:Label ID="lblSuppCode" runat="server" forecolor="Red" 
                            text="Please select Supplier Code" visible="False"></asp:Label>
                        <asp:TextBox ID="txtSupName" class="fontObject" runat="server" BackColor="Transparent" 
                            BorderStyle="None" Font-Bold="True" MaxLength="10" Width="86%"></asp:TextBox>
&nbsp;</td>
					<td class="style3"></td>
					<td class="style3"><asp:DropDownList id="ddlInvRcvId" class="fontObject" width=100% 
                            runat=server AutoPostBack="True" onSelectedIndexChanged="InvRcvIndexChanged" 
                            Visible="False"/></td>
					<td class="style3"><asp:label id=lblInvoiceRcvIDTag runat=server Visible="False"/> </td>
					<td width="5%" class="style3"></td>
				</tr>						
				<tr>
				    <td height=25 class="style4">Goods Return From :*</td>
				    <td><asp:DropDownList id="ddlInventoryBin" class="fontObject" Width=100% runat=server/>
				        <asp:Label id=lblInventoryBin text="Please Select Inventory Bin" forecolor=red visible=false runat=server /></td>
    				<td>&nbsp;</td>
				    <td>&nbsp;</td>
				    <td>&nbsp;</td>
				    <td>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=6>&nbsp;</td>				
				</tr>

                </table>
            <table class="font9Tahoma" border="0" width="100%" cellspacing="0" cellpadding="4" runat=server>					
				<tr>
					<td colspan="6">
						<table id=tblLine class="sub-Add" border="0" width="100%" cellspacing="0" cellpadding="4" runat=server>
							<tr>						
								<td>
									<table id=tblDoc class="font9Tahoma" border="0" width="100%" cellspacing="0" cellpadding="4" runat=server>
										<tr >
											<td width=20% height="25">&nbsp;</td>
											<td width=80%><asp:Label id=lblErrManySelectDoc forecolor=black visible=true text="Note : Please select Invoice Receive ID/Goods Return Advice ID/Goods Receive ID/Item Code for item listing." runat=server/></td>
										</tr>	
										<tr>
											<td height="25">Stock Return Advice ID :</td>
											<td><asp:DropDownList id="ddlRetAdvId" CssClass="font9Tahoma" width=100% runat=server AutoPostBack="True" onSelectedIndexChanged="RetAdvIndexChanged"/></td>
										</tr>
										<tr>
											<td height="25">Goods Received ID :</td>
											<td><asp:DropDownList id="ddlGoodsRcvId" CssClass="font9Tahoma" width=100% runat=server AutoPostBack="True" onSelectedIndexChanged="GoodsRcvIndexChanged"/></td>
										</tr>	
	                                    <tr>
											<td height="25">Dispatch Advice ID :</td>
											<td><asp:DropDownList id="ddlDispAdvID" CssClass="font9Tahoma" width=100% runat=server AutoPostBack="True" onSelectedIndexChanged="DispAdvIndexChanged"/></td>
										</tr>	
										<tr>
											<td height="25">Item Code :</td>
											<td><asp:DropDownList id="ddlItemCode" width=100% CssClass="fontObject" runat=server AutoPostBack="True" onSelectedIndexChanged="ItemIndexChanged"/>
											    <asp:Label id=lblQtyDisp text="This item already dispatched, please create Stock Return Advice on Inventory Module. After Stock Return Advice have been created, please create new Goods Return and use Stock Return Advice as reference." Visible=False forecolor=red Runat="server" />
											</td>
										</tr>
										
										
									</table>
									<table id="tblFACode" class="font9Tahoma" border="0" width="100%" cellspacing="0" cellpadding="4" visible = false runat=server>
										<tr>
											<td height="20">Fixed Asset Code : </td>
											<td width=80%><asp:Label id="lblAssetCode" runat="server" /></td>
										</tr>
									</table>
									<table id="tblDoc1" class="mb-c" border="0" width="100%" cellspacing="0" cellpadding="4" runat=server>
										<tr>
											<td height="25">Quantity On Hand :</td>
											<td width=80%><asp:Label id=lblIDQtyOnHand runat=server /><asp:Label id=lblQtyOnHand visible=false runat=server /></td>
										</tr>
										<tr>
											<td height="25">Quantity Return :*</td>
											<td width=80%><asp:TextBox id="txtQty" width="16%" maxlength=20 
                                                    CssClass="fontObject" runat="server" />
												<asp:RegularExpressionValidator id="RegularExpressionValidatorQty" 
													ControlToValidate="txtQty"
													ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"
													Display="Dynamic"
													text="Maximum length 9 digits and 5 decimal points"
													runat="server"/>
												<asp:RequiredFieldValidator 
													id="validateQty" 
													runat="server" 
													ErrorMessage="Please Specify Quantity To Return" 
													ControlToValidate="txtQty" 
													display="dynamic"/>
												<asp:Label id=lblQty text="Number generated is too big!" Visible=False forecolor=red Runat="server" />
												<asp:Label id=lblErrQtyReturn forecolor="red" text="Quantity Return cannot be greater than Quantity On Hand." Visible=False runat="server" />
												<asp:Label id=lblErrRcvQty text="Quantity Return cannot exceeds Quantity of Goods Receive!" Visible=False forecolor=red Runat="server" />
												<asp:Label id=lblRcvQty Visible=False forecolor=red Runat="server" />
											</td>
										</tr>
										<tr>
											<td height="25">Unit Cost :*</td>
											<td width=80%><asp:TextBox id="txtCost" width="16%" maxlength=19 
                                                    class="fontObject" runat="server" />
												<asp:RegularExpressionValidator id="RegularExpressionValidatorCost" 
													ControlToValidate="txtCost"
													ValidationExpression="\d{1,19}\.\d{0,0}|\d{1,19}"
													Display="Dynamic"
													text="Maximum length 19 digits."
													runat="server"/>
												<asp:RequiredFieldValidator 
													id="validateCost" 
													runat="server" 
													ErrorMessage="Please Specify Unit Cost" 
													ControlToValidate="txtCost" 
													display="dynamic"/>
												<asp:Label id=lblCost text="Number generated is too big!" Visible=False forecolor=red Runat="server" />
												<asp:Label id=lblInvisibleCost visible=false runat=server />
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td>
									<table id=tblAcc class="font9Tahoma" border="0" width="100%" cellspacing="0" cellpadding="4" runat=server>									
										<tr>
											<td width=20% height=25><asp:label id="lblAccount" runat="server" /> (CR) :* </td>
											<td width=80%><asp:DropDownList id=ddlAccount width=90% onselectedindexchanged=onSelect_Account autopostback=true CssClass="fontObject" runat=server/>
												<input type=button value=" ... " id="FindAcc" onclick="javascript:PopCOA('frmMain', '', 'ddlAccount', 'True');" CausesValidation=False class="button-small" runat=server />  
												<asp:Label id=lblErrAccount visible=false forecolor=red runat=server/></td>
										</tr>
										<tr id="RowChargeLevel" class="mb-c">
											<td height="25">Charge Level :* </td>
											<td><asp:DropDownList id="ddlChargeLevel" Width=100% AutoPostBack=True OnSelectedIndexChanged=ddlChargeLevel_OnSelectedIndexChanged CssClass="fontObject" runat=server /> </td>
										</tr>
										<tr id="RowPreBlk" class="mb-c">
											<td height="25"><asp:label id=lblPreBlkTag Runat="server"/> </td>
											<td><asp:DropDownList id="ddlPreBlock" CssClass="fontObject" Width=100% runat=server />
												<asp:label id=lblPreBlockErr Visible=False forecolor=red Runat="server" /></td>
										</tr>	
										<tr id="RowBlk" class="mb-c">
											<td height=25><asp:label id="lblBlock" runat="server" /> :</td>
											<td><asp:DropDownList id=ddlBlock  CssClass="fontObject" width=100% runat=server/>
												<asp:Label id=lblErrBlock visible=false forecolor=red runat=server/></td>
										</tr>
										<tr>
											<td height=25><asp:label id="lblVehicle" runat="server" /> :</td>
											<td><asp:Dropdownlist id=ddlVehCode width=100%  CssClass="fontObject" runat=server/>
												<asp:Label id=lblErrVehicle visible=false forecolor=red runat=server/></td>
										</tr>
										<tr>
											<td height=25><asp:label id="lblVehExpense" runat="server" /> :</td>
											<td><asp:Dropdownlist id=ddlVehExpCode width=100% CssClass="fontObject" runat=server/>
												<asp:Label id=lblErrVehExp visible=false forecolor=red runat=server/></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td colspan="2" height="26"><asp:Imagebutton id="btnAdd" OnClick="btnAdd_Click" ImageURL="../../images/butt_add.gif" AlternateText=Add UseSubmitBehavior="false"   Runat="server" />
                                    <br />
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
						<asp:DataGrid id=dgGRNDet
							AutoGenerateColumns="false" width="100%" runat="server"
							GridLines=none
							Cellpadding="2"
							Pagerstyle-Visible="False"
							OnDeleteCommand="DEDR_Delete"
							AllowSorting="True"  >	
							<HeaderStyle CssClass="mr-h"/>
							<ItemStyle CssClass="mr-l"/>
							<AlternatingItemStyle CssClass="mr-r"/>	
 
							<Columns>				
								<asp:BoundColumn Visible=False DataField="GoodsRetLnID" />
								<asp:BoundColumn Visible=False DataField="ItemCode" />
								<asp:BoundColumn Visible=False DataField="QtyReturn" />
								<asp:TemplateColumn HeaderText="Doc ID">
									<ItemStyle Width="10%"/>								
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("DocID") %> id="lblDocID" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>															
								<asp:TemplateColumn HeaderText="Item">
									<ItemStyle Width="15%"/>								
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("Description") %> id="lblItem" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>					
								<asp:TemplateColumn>
									<ItemStyle Width="10%"/>								
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("AccCode") %> id="lblAccCode" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemStyle Width="10%"/>								
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("BlkCode") %> id="lblBlkCode" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemStyle Width="10%"/>								
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("VehCode") %> id="lblVehCode" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemStyle Width="10%"/>								
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("VehExpenseCode") %> id="lblVehExpCode" runat="server" />
									</ItemTemplate>																					
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Purchase UOM">
									<ItemStyle Width="5%"/>								
									<ItemTemplate> 
										<asp:Label Text=<%# Container.DataItem("PurchaseUOM") %> id="lblPurchaseUOM" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>																												
								<asp:TemplateColumn HeaderText="Quantity Return">
									<HeaderStyle HorizontalAlign="Right" /> 
									<ItemStyle Width="5" HorizontalAlign="Right" /> 
									<ItemTemplate>
										
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("QtyReturn"), 5), 5) %> id="lblQtyReturn" runat="server" />							
									</ItemTemplate>
								</asp:TemplateColumn>																			
								<asp:TemplateColumn HeaderText="Unit Cost">
									<HeaderStyle HorizontalAlign="Right" /> 
									<ItemStyle Width="10%" HorizontalAlign="Right" /> 
									<ItemTemplate>
										
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("OriCost"), 2), 2)%> id="lblOriCost" runat="server" />							
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Cost"), 2), 2)%> id="lblCost" visible=false runat="server" />							
									</ItemTemplate>
								</asp:TemplateColumn>								
								<asp:TemplateColumn HeaderText="Amount">
									<HeaderStyle HorizontalAlign="Right" /> 
									<ItemStyle Width="10%" HorizontalAlign="Right" /> 
									<ItemTemplate>
										
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("OriAmount"), 2), 2) %> id="lblOriAmount" runat="server" />							
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Amount"), 2), 2) %> id="lblAmount" Visible=false runat="server" />							
									</ItemTemplate>
								</asp:TemplateColumn>									
								<asp:TemplateColumn>		
									<ItemStyle Width="5%" HorizontalAlign="Right" /> 								
									<ItemTemplate>
										<asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server CausesValidation=False/>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>										
						</asp:DataGrid>
					</td>
				</tr>
				<tr>
					<td colspan=3></td>
					<td colspan=2 height=25><hr style="width :100%" /> </td>
					<td width="5%">&nbsp;</td>
				</tr>
				<tr>
					<td colspan=3>&nbsp;</td>
					<td height=25>Total Amount :</td>
					<td align=right><asp:Label ID=lblIDTotalAmount Runat=server /><asp:Label ID=lblTotalAmount Visible=False Runat=server />&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25"><asp:Label id="Remarks" text="Remarks :" runat="server" /></td>	
					<td colspan="5"><asp:TextBox id="txtRemark" width=100% maxlength="128" CssClass="fontObject" runat="server" /></td>
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
						<asp:ImageButton id="btnDelete" UseSubmitBehavior="false" onClick="btnDelete_Click" ImageUrl="../../images/butt_delete.gif" AlternateText=Delete CausesValidation=False runat="server" />
						<asp:ImageButton id="btnUndelete" UseSubmitBehavior="false" onClick="btnUnDelete_Click" ImageUrl="../../images/butt_undelete.gif" AlternateText=Undelete CausesValidation=False runat="server" />
						<asp:ImageButton id="btnBack" UseSubmitBehavior="false" onClick="btnBack_Click" ImageUrl="../../images/butt_back.gif" AlternateText=Back CausesValidation=False runat="server" />
						<asp:ImageButton id="btnCancel" UseSubmitBehavior="false" onClick="btnCancel_Click" ImageUrl="../../images/butt_cancel.gif" AlternateText=Cancel CausesValidation=False runat="server" />
					</td>
				</tr>		
				
				
				<tr>
					<td colspan=5>
                        &nbsp;</td>
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
				    <td>
                        <asp:TextBox ID="txtPPN" runat="server" BackColor="Transparent" 
                            BorderStyle="None" ForeColor="Transparent" Width="9%"></asp:TextBox>
                        <asp:TextBox ID="txtCreditTerm" runat="server" BackColor="Transparent" 
                            ForeColor="Transparent" BorderStyle="None" Width="9%"></asp:TextBox>
                        <asp:TextBox ID="txtPPNInit" runat="server" BackColor="Transparent" 
                            ForeColor="Transparent" BorderStyle="None" Width="9%"></asp:TextBox>
                    </td>								
				    <td height=25 align=right><asp:Label id=lblTotalViewJournal Visible=false runat=server /> </td>
				    <td>&nbsp;</td>	
				    <td align=right><asp:label id="lblTotalDB" text="0" Visible=false runat="server" /></td>						
				    <td>&nbsp;</td>		
				    <td align=right><asp:label id="lblTotalCR" text="0" Visible=false runat="server" /></td>				
			    </tr>
			</table>
			<Input type=hidden id=hidBlockCharge value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>
			<Input type=hidden id=hidOriCost value=0 runat=server/>
			<Input type=hidden id=hidPOLNID value="" runat=server/>
            </div>
            </td>
            </tr>
            </table>
		</form>
	</body>
</html>
