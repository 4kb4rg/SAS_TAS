<%@ Page Language="vb" src="../../../include/IN_Trx_StockTransfer_Details.aspx.vb" Inherits="IN_StockTransfer" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuINTrx" src="../../menu/menu_INtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Stock Transfer Details</title>		
		<Preference:PrefHdl id=PrefHdl runat="server" />
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	    <style type="text/css">
            .style1
            {
                width: 198px;
                height: 19px;
            }
            .style2
            {
                height: 19px;
            }
            .style3
            {
                width: 100%;
            }
            </style>
	</head>
	<body>
		<form id=frmMain class="main-modul-bg-app-list-pu"  runat=server>
         <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		<tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">  
   		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
		<asp:Label id=lblTo visible=false text="To " runat=server />
		<asp:Label id=lblCode visible=false text=" Code " runat=server />
		<asp:Label id=lblPleaseSelectOne visible=false text="Please select one " runat=server />
		<asp:Label id=lblSelect visible=false text="Select " runat=server />
		<asp:label id="lblStatusHid" Visible="False" Runat="server" />
		<asp:label id=lblDocTitle visible=false text="Stock Transfer" runat=server />
        		
		<table border=0 width="100%" cellspacing="0" cellpading="1" class="font9Tahoma">
			<tr>
				<td colspan=6><UserControl:MenuINTrx enableviewstate=false id=menuIN runat="server" /></td>
			</tr>			
			<tr>
				<td class="mt-h" colspan=6>
                    <table cellspacing="1" class="style3">
                        <tr>
                            <td class="font9Tahoma">
                             <strong>STOCK TRANSFER DETAILS</strong> </td>
                            <td class="font9Header" style="text-align: right">
                                &nbsp;</td>
                        </tr>
                    </table>
                       <hr style="width :100%" />
                </td>
			</tr>
			<tr>
				<td height=25 style="width: 20%">Stock Transfer ID :</td>
				<td width="40%"><asp:label id=lblStckTxID Runat="server"/></td>
				<td width="5%">&nbsp;</td>
				<td width="15%">&nbsp;</td>
				<td width="20%"><asp:Label id=lblReprint  Text="<B>( R E P R I N T )</B><br>" Visible=False forecolor=Red runat=server />
				</td>
				<td width="5%">&nbsp;</td>
			</tr>
			<tr>
				<td height=25 style="width: 198px">Stock Transfer Date :*</td>
				<td><asp:TextBox id=txtDate CssClass="fontObject" Width="45%" maxlength=10 runat=server />
					<a href="javascript:PopCal('txtDate');">
					<asp:Image id="btnSelDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a> 
					<asp:RequiredFieldValidator 
						id="validateDate" 
						runat="server" 
						ErrorMessage="<br>Please specify reference date!" 
						EnableClientScript="True"
						ControlToValidate="txtDate" 
						display="dynamic"/>
					<asp:label id=lblDate Text="Date Entered should be in the format" forecolor=red Visible=false Runat="server"/> 
					<asp:label id=lblFmt  forecolor=red Visible = false Runat="server"/> 
				</td>
				<td>&nbsp;</td>	
				<td>Period : </td>
				<td> <asp:Label id=lblAccPeriod runat=server /></td>
				<td>&nbsp;</td>
			</tr>
            <tr>
                <td height="25" style="width: 198px">
                    Description:</td>
                <td>
                    <asp:TextBox id=txtDesc CssClass="fontObject" width="100%" maxlength="128" Runat="server"/></td>
                <td>
                </td>
                <td>
                    Status :</td>
                <td>
                    <asp:Label id=Status runat=server />
                </td>
                <td>
                </td>
            </tr>
			<tr><td height=25 style="width: 198px">Stock Transfer From :*</td>
				<td><asp:DropDownList id="ddlInventoryBin" CssClass="fontObject" Width=100% runat=server/>
				    <asp:Label id=lblInventoryBin text="Please Select Inventory Bin" forecolor=red visible=false runat=server /></td>
				<td>&nbsp;</td>
				<td>Document Received ID :</td>
				<td> 
                    <asp:Label ID="lblStockReceiveID" runat="server"></asp:Label>
                </td>
				<td>&nbsp;</td>
			</tr>			
			<tr>
				<td height=25 style="width: 198px"><asp:label id=ToLocTag Runat="server"/> </td>
				<td><asp:DropDownList id="lstToLoc" CssClass="fontObject" Width=100% runat=server AutoPostBack="True" OnSelectedIndexchanged=RebindPOList />
					<asp:label id=lblToLocErr Visible=False forecolor=red Runat="server" /></td>
				<td>&nbsp;</td>
				<td>Date Created : </td>
				<td> <asp:Label id=CreateDate runat=server />
                                </td>	
				<td>&nbsp;</td>
			</tr>			
			<tr>
				<td height=25 style="width: 198px">
                    Transporter :</td>
				<td><asp:TextBox id=txtTransporter CssClass="fontObject" width="45%" maxlength="128" Runat="server"/></td>
				<td>&nbsp;</td>
				<td>Last Update :</td>
				<td> <asp:Label id=UpdateDate runat=server />
                                </td>				
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25 style="width: 198px">No. Kendaraan :</td>
				<td><asp:TextBox id=txtVehicle CssClass="fontObject" width="45%" maxlength="16" Runat="server"/></td>
				<td>&nbsp;</td>
				<td> Update By : </td>
				<td> <asp:Label id=UpdateBy runat=server /></td>				
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25 style="width: 198px">
                    Pengemudi :</td>
				<td><asp:TextBox id=txtDriverName CssClass="fontObject" width="100%" maxlength="128" Runat="server"/></td>
				<td>&nbsp;</td>
				<td> <asp:Label id=lblPDateTag visible=False Text="Print Date :" runat=Server />&nbsp;&nbsp;&nbsp;</td>
				<td> <asp:Label id=lblPrintDate  runat=server /></td>				
				<td>&nbsp;</td>
			</tr>
            <tr>
                <td height="25" style="width: 198px">
                    No. Sim :</td>
                <td><asp:TextBox id=txtDriverIC CssClass="fontObject" width="45%" maxlength="64" Runat="server"/></td>
                <td>
                </td>
                <td>
                    <asp:Label id=lblDNIDTag visible=False Text="Debit Note ID :" runat=Server />&nbsp;&nbsp;</td>
                <td>
                    <asp:Label id=lblDNNoteID  runat=server />
                </td>
                <td>
                </td>
            </tr>
            </table>
        <table border=0 width="100%" cellspacing="0" cellpadding="2" runat="server">
            <tr>
            <td>
			<tr>
				<td colspan=6>
				<table id="tblAdd" class="sub-Add"  border=0 width="100%" cellspacing="0" cellpadding="2" runat="server">
                    <tr class="mb-c">
                        <td width="20%">
                        </td>
                        <td width="80%">
                        </td>
                    </tr>
                    <tr class="mb-c">
                        <td width="20%">
                            PO/PR ID</td>
                        <td width="80%">
                            <asp:DropDownList ID="lstPR" CssClass="fontObject" runat="server" AutoPostBack="True"  OnSelectedIndexchanged=RebindItemList
                                Width="90%">
                            </asp:DropDownList></td>
                    </tr>
					<tr class="mb-c">
						<td width="20%">Item Code :*</td>
						<td width="80%"><asp:DropDownList id="lstItem" CssClass="fontObject" AutoPostBack=true OnSelectedIndexchanged=RebindItemDetail Width=90% runat=server />
							<input type=button value=" ... " id="FindIN" onclick="javascript:PopItem('frmMain', '', 'lstItem', 'False');" CausesValidation=False runat=server visible="false" />
							<asp:label id=lblItemCodeErr text="<br>Please select one Item" Visible=False forecolor=red Runat="server" />&nbsp;
                            <asp:label id=lblSelectedItemCode Runat="server"/></td>
					</tr>
                    <tr class="mb-c">
                        <td>
                            Qty PO :</td>
                        <td>
                            <asp:textbox id="txtQtyPO" CssClass="fontObject" Width="22%" maxlength=15 EnableViewState="False" Runat="server" ReadOnly="True" /></td>
                    </tr>

					<tr class="mb-c">
						<td>Quantity to Transfer :*</td>
						<td><asp:textbox id="txtQty"  CssClass="fontObject" Width="22%" maxlength=15 EnableViewState="False" Runat="server" />
			                <asp:RegularExpressionValidator id="RegularExpressionValidatorQtyReq" 
								ControlToValidate="txtQty"
								ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"
								Display="Dynamic"
								text = "Maximum length 9 digits and 5 decimal points"
								runat="server"/>
							<asp:RequiredFieldValidator 
								id="validateQty" 
								runat="server" 
								ErrorMessage="Please specify quantity to transfer!" 
								ControlToValidate="txtQty" 
								display="dynamic"/>
							<asp:RangeValidator id="Range1"
								ControlToValidate="txtQty"
								MinimumValue="0"
								MaximumValue="999999999999999"
								Type="double"
								EnableClientScript="True"
								Text="The value is out of acceptable range!"
								runat="server" display="dynamic"/>
							<asp:label id=lblerror text="Number generated is too big!" Visible=False forecolor=red Runat="server" />
							<asp:label id=lblStock text="Not enough stock in hand!" Visible=False forecolor=red Runat="server" />&nbsp;
                            <asp:label id=lblErrQty text="Qty Must Less Than or Equal Qty PO" Visible=False forecolor=Red Runat="server" /></td>
					</tr>

                    <tr class="mb-c" style="visibility:hidden">
                        <td>
                            Cost :</td>
                        <td>
                            <asp:textbox id="txtCost" CssClass="fontObject" Width="22%" maxlength=15 EnableViewState="False" Runat="server" ReadOnly="True" /></td>
                    </tr>

					<tr class="mb-c">
						<td colspan=2><asp:ImageButton text="Add" id="btnAdd" ImageURL="../../images/butt_add.gif" OnClick="btnAdd_Click" UseSubmitBehavior="false" Runat="server" />&nbsp;</td>
					</tr>
				</table>
				</td>		
			</tr>
			<tr>
				<td colspan=6><asp:label id=lblConfirmErr text="Document must contain transaction to Confirm!" Visible=False forecolor=red Runat="server" />
							  <asp:label id=lblUnDel text="Insufficient stock in Inventory to perform operation!" Visible=False forecolor=red Runat="server" /></td>
			</tr>
			<tr>
				<td colspan=6> 
					<asp:DataGrid ID="dgStkTx" runat="server" AllowSorting="True" 
                        AutoGenerateColumns="False" Cellpadding="2" GridLines="None" 
                        OnDeleteCommand="DEDR_Delete" OnItemCreated="DataGrid_ItemCreated" 
                        OnItemDataBound="dgLine_BindGrid"
                        Pagerstyle-Visible="False" width="100%">
                        <HeaderStyle CssClass="mr-h" />
                        <ItemStyle CssClass="mr-l" />
                        <AlternatingItemStyle CssClass="mr-r" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="Document ID">
                                <ItemStyle Width="25%" />
                                <ItemTemplate>
                                    <asp:Label ID="DOCID" runat="server" text='<%# Container.DataItem("DOCID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Document Receive ID">
                                <ItemStyle HorizontalAlign="left" Width="20%" />
                                <ItemTemplate>
                                    <asp:Label ID="ReceiveID" runat="server" 
                                        text='<%# Container.DataItem("StockReceiveID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Item">
                                <ItemStyle Width="40%" />
                                <ItemTemplate>
                                    <asp:Label ID="ItemCode" runat="server" 
                                        text='<%# Container.DataItem("ItemCode") %>'></asp:Label>
                                    ( <%# Container.DataItem("Description") %> )
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Quantity Transfered">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" Width="20%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblQtyTrx" runat="server" 
                                        text='<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("Qty"),5) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Unit Cost">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" Width="15%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblUnitCost" runat="server" 
                                        text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Cost"), 2), 2) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Amount">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" Width="20%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblAmount" runat="server" 
                                        text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Amount"), 2), 2) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemStyle HorizontalAlign="Right" Width="5%" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="Delete" runat="server" CausesValidation="False" 
                                        CommandName="Delete" Text="Delete"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle Visible="False" />
                    </asp:DataGrid>
				</td>	
			</tr>
			<tr>
				<td colspan=3>&nbsp;</td>
				<td colspan=2 height=25><hr style="width :100%" /></td>
				<td>&nbsp;</td>			
			</tr>				
			<tr class="font9Tahoma">
				<td colspan=3>&nbsp;</td>			
				<td height=25>Total Amount :</td>
				<td align=right><asp:label id="lblTotAmtFig" CssClass="font9Tahoma" runat="server" /></td>						
				<td align="right">&nbsp;</td>						
			</tr>
			<tr>
				<td colspan=6 style="height: 23px">&nbsp;</td>				
			</tr>
			<tr Class="font9Tahoma">
				<td class="style1">Remarks :</td>	
				<td colspan="5" class="style2"><asp:textbox id="txtRemarks"   CssClass="fontObject" width="98%" maxlength="128" runat="server" /></td>
			</tr>
			<tr>
				<td colspan=6>&nbsp;</td>				
			</tr>
			<tr>
				<td align="left" colspan="6">
					<asp:ImageButton id="btnNew"    UseSubmitBehavior="false" AlternateText="New"        onClick="btnNew_Click"       ImageURL="../../images/butt_new.gif"       CausesValidation=False runat="server" />
					<asp:ImageButton id="Save"      UseSubmitBehavior="false" AlternateText="Save"       onClick="btnSave_Click"      ImageURL="../../images/butt_save.gif"      CausesValidation=False runat="server" />
					<asp:ImageButton ID="btnEdited" runat="server" AlternateText="Edit" 
                        CausesValidation="False" ImageUrl="../../images/butt_edit.gif" 
                        onclick="btnEdit_Click" UseSubmitBehavior="false" />
					<asp:ImageButton id="Confirm"   UseSubmitBehavior="false" AlternateText="Confirm"    onClick="btnConfirm_Click"   ImageURL="../../images/butt_confirm.gif"   CausesValidation=False runat="server" />
					<asp:ImageButton id="Cancel"    UseSubmitBehavior="false" AlternateText="Cancel"     onClick="btnCancel_Click"    ImageURL="../../images/butt_Cancel.gif"    CausesValidation=False runat="server" />
					<asp:ImageButton id="Print"     UseSubmitBehavior="false" AlternateText="Print"      onClick="btnPreview_Click"   ImageURL="../../images/butt_print.gif"     CausesValidation=False runat="server" />
					<asp:ImageButton id="PRDelete"  UseSubmitBehavior="false" AlternateText="Delete"     onClick="btnDelete_Click"    ImageURL="../../images/butt_delete.gif"    CausesValidation=False runat="server" />
					<asp:ImageButton id="DebitNote" UseSubmitBehavior="false" AlternateText="Debit Note" onClick="btnDebitNote_Click" ImageURL="../../images/butt_debitnote.gif" CausesValidation=False runat="server" />
					<asp:ImageButton id="Back"      UseSubmitBehavior="false" AlternateText="Back"       onClick="btnBack_Click"      ImageURL="../../images/butt_back.gif"      CausesValidation=False runat="server" />
				</td>
			</tr>		
			
			
				
			<tr>
				<td colspan=5>
                    &nbsp;</td>
			</tr>
			<tr id=TrLink runat=server>
				<td colspan=5>
					<asp:LinkButton id=lbViewJournal  CssClass="font9Tahoma" text="View Journal Predictions" causesvalidation=false runat=server /> 
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
                        OnItemDataBound="dgLine_BindGrid"
						Pagerstyle-Visible="False"
						AllowSorting="false" >	
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
			<tr class="font9Tahoma">
			    <td style="width: 198px">&nbsp;</td>								
			    <td height=25 align=right><asp:Label id=lblTotalViewJournal CssClass="font9Tahoma" Visible=false runat=server /> </td>
			    <td>&nbsp;</td>	
			    <td align=right><asp:label id="lblTotalDB" CssClass="font9Tahoma"  text="0" Visible=false runat="server" /></td>						
			    <td>&nbsp;</td>		
			    <td align=right><asp:label id="lblTotalCR" CssClass="font9Tahoma"  text="0" Visible=false runat="server" /></td>				
		    </tr>
            </td>
            </tr>
            
		</table>
			<Input type=hidden id=hidBlockCharge value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>
        </div>
        </td>
        </tr>
        </table>

		</form>
	</body>
</html>
