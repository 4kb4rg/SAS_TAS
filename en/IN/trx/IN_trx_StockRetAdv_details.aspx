<%@ Page Language="vb" Trace="false" src="../../../include/IN_Trx_StockRetAdv_Details.aspx.vb" Inherits="IN_StockRetAdv" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuINTrx" src="../../menu/menu_INtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<html>
	<head>
		<title>Stock Return Advice Details</title>		
		<Preference:PrefHdl id=PrefHdl runat="server" />
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	    <style type="text/css">
            .style1
            {
                width: 100%;
            }
            .style2
            {
                width: 357px;
            }
        </style>
	</head>

	<body>
		<form id=frmMain class="main-modul-bg-app-list-pu" runat=server>
        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">  

   		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
		<asp:label id="lblStatusHid" Visible="false" Runat="server"></asp:label>
		<!-- BFR_WTK00041 Item 41 START -->
		<asp:Label id=lblVehicleOption visible=false text=false runat=server />
		<input type=hidden id=hidItemType runat=server />
		<!-- BFR_WTK00041 Item 41 END -->

		    <table border=0 width="100%" cellspacing="0" cellpadding="1" class="font9Tahoma">
			<tr>
				<td colspan=6><UserControl:MenuINTrx enableviewstate=false id=menuIN runat="server" /></td>
			</tr>			
			<tr>
				<td class="mt-h" colspan=6>
                    <table cellspacing="1" class="style1">
                        <tr class="font9Tahoma">
                            <td class="style2">
                                <strong>STOCK RETURN ADVICE DETAILS </strong></td>
                            <td  class="font9Header"  style="text-align: right">
                                Period : <asp:Label id=lblAccPeriod runat=server />&nbsp;| Status : <asp:Label id=Status runat=server />&nbsp;| 
                                Date Created : <asp:Label id=CreateDate runat=server />| Last Update : <asp:Label id=UpdateDate runat=server />&nbsp;| 
                                Update By : <asp:Label id=UpdateBy runat=server />&nbsp;|
                                <asp:Label ID="lblPDateTag" runat="Server" Text="Print Date :" visible="False"></asp:Label>
&nbsp;: <asp:Label id=lblPrintDate  runat=server />
                            </td>
                        </tr>
                    </table>
                     <hr style="width :100%" />
                </td>
			</tr>
			<tr>
				<td colspan=6>&nbsp;</td>
			</tr>			
			<tr>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td><asp:Label id=lblReprint  Text="<B>( R E P R I N T )</B><br>" Visible=False forecolor=Red runat=server />
				</td>
				<td>&nbsp;</td>				
			</tr>		
			<tr>
				<td width="20%" height=25>Stock Return Advice ID :</td>
				<td width="40%"><asp:label id=lblStckTxID Runat="server"/></td>
				<td width="5%">&nbsp;</td>
				<td width="15%">&nbsp;</td>
				<td width="15%">&nbsp;</td>
				<td width="5%">&nbsp;</td>
			</tr>
			<tr>
				<td height=25>Stock Return Advice Date :*</td>
				<td><asp:TextBox id=txtDate CssClass="font9Tahoma"  Width=50% maxlength=10 runat=server />
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
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td width="5%">&nbsp;</td>
			</tr>
			<tr>
				<td height=25>Stock Return Advice From :*</td>
				<td><asp:DropDownList id="ddlInventoryBin" CssClass="font9Tahoma"  Width=100% runat=server/>
				    <asp:Label id=lblInventoryBin text="Please Select Inventory Bin" forecolor=red visible=false runat=server /></td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>	
			<tr>
				<td height=25>Warehouse From :*</td>
				<td><asp:DropDownList id="lstStorage"  CssClass="fontObject" Width="100%" runat=server/>
				    <asp:Label id=lblstoragemsg text="Please Select Warehouse" forecolor=red visible=false runat=server /></td>
            </tr>            		
			<tr>
				<td height=25>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>		
				<td width="5%">&nbsp;</td>
			</tr>	
			</table>

        <table border=0 width="100%" cellspacing="0" cellpadding="2" class="font9Tahoma" runat="server">
            <tr>
            <td> 
			<tr>
				<td colspan=6>
				<table id="tblAdd" class="sub-Add" border=0 width="100%" cellspacing="0" cellpadding="4" runat="server">
					<tr class="mb-c">
						<td width="20%" height="25">Stock Receive ID :</td>
						<td width="80%"><asp:DropDownList id=lstDoc Width="90%" CssClass="font9Tahoma" 
                                AutoPostBack=True OnSelectedIndexchanged=RebindItemList runat=server /></td>
					</tr>
					<tr class="mb-c">
						<td height="25">Item Code :*</td>
						<td><asp:DropDownList id="lstItem" Width=90% CssClass="font9Tahoma"  runat=server EnableViewState=True AutoPostBack="True" onSelectedIndexChanged="ItemIndexChanged"/> <!-- BFR_WTK00041 Item 3 -->
							<input type=button value=" ... " id="FindIN" 
                                onclick="javascript:PopItem('frmMain', '', 'lstItem', 'True');" 
                                CausesValidation=False runat=server visible="False" />
							<asp:label id=lblItemCodeErr text="<br>Please select one Item" Visible=False forecolor=red Runat="server" />
						</td>
					</tr>
					<tr class="mb-c">
						<td height="25">Quantity to Return :*</td>
						<td><asp:textbox id="txtQty" CssClass="font9Tahoma"  Width=50% maxlength=15 EnableViewState="False" Runat="server" />
							<asp:RegularExpressionValidator id="RegularExpressionValidatorQtyReq" 
								ControlToValidate="txtQty"
								ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"
								Display="Dynamic"
								text = "<br>Maximum length 9 digits and 5 decimal points"
								runat="server"/>
							<asp:RequiredFieldValidator 
								id="validateQty" 
								runat="server" 
								ErrorMessage="<br>Please specify quantity to return" 
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
						</td>
					</tr>
					<!-- BFR_WTK00041 Item 3 START -->
					<tr class="mb-c">
						<td colspan=6>
							<table id=tblAcc class="mb-c" border="0" width="100%" cellspacing="0" cellpadding="4" runat=server>									
								<tr>
									<td width=20% height=25><asp:label id="lblAccount" runat="server" /> (CR) :* </td>
									<td width=80%><asp:DropDownList id=ddlAccount  CssClass="font9Tahoma"  width=90% onselectedindexchanged=onSelect_Account autopostback=true runat=server/>
									    <input type=button value=" ... " id="FindAcc" 
                                            onclick="javascript:PopCOA('frmMain', '', 'ddlAccount', 'False');" 
                                            CausesValidation=False runat=server visible="False" />  
										<asp:Label id=lblErrAccount visible=false forecolor=red runat=server/></td>
								</tr>
								<tr id="RowChargeLevel" class="mb-c" visible=false>
									<td height="25">Charge Level :* </td>
									<td><asp:DropDownList id="ddlChargeLevel" CssClass="font9Tahoma"  Width="90%" 
                                            AutoPostBack=True OnSelectedIndexChanged=ddlChargeLevel_OnSelectedIndexChanged 
                                            runat=server /> </td>
								</tr>
								<tr id="RowPreBlk" class="mb-c">
									<td height="25"><asp:label id=lblPreBlkTag Runat="server"/> </td>
									<td><asp:DropDownList id="ddlPreBlock" CssClass="font9Tahoma"  Width="90%" 
                                            runat=server />
										<asp:label id=lblPreBlockErr Visible=False forecolor=red Runat="server" /></td>
								</tr>	
								<tr id="RowBlk" class="mb-c">
									<td height=25><asp:label id="lblBlock" runat="server" /> :</td>
									<td><asp:DropDownList id=ddlBlock CssClass="font9Tahoma"  width="90%" runat=server/>
										<asp:Label id=lblErrBlock visible=false forecolor=red runat=server/></td>
								</tr>
								<tr>
									<td height=25><asp:label id="lblVehicle" runat="server" /> :</td>
									<td><asp:Dropdownlist id=ddlVehCode CssClass="font9Tahoma"  width="90%" 
                                            runat=server/>
										<asp:Label id=lblErrVehicle visible=false forecolor=red runat=server/></td>
								</tr>
								<tr>
									<td height=25><asp:label id="lblVehExpense" runat="server" /> :</td>
									<td><asp:Dropdownlist id=ddlVehExpCode  CssClass="font9Tahoma"  width="90%" 
                                            runat=server/>
										<asp:Label id=lblErrVehExp visible=false forecolor=red runat=server/></td>
								</tr>
							</table>
						</td>
					</tr>
					<!-- BFR_WTK00041 Item 3 END -->
					<tr class="mb-c">
						<td>&nbsp;</td>
						<td><asp:label id=lblerror text="Number generated is too big!" Visible=False forecolor=red Runat="server" />
							<asp:label id=lblStock text="Returning quantity more than received!" Visible=False forecolor=red Runat="server" />
							<asp:label id=lblPR text="Item not found in selected Document!" Visible=False forecolor=red Runat="server" />
						</td>
					</tr>
					<tr class="mb-c">
						<td colspan=2><asp:ImageButton id="btnAdd" text="Add" ImageURL="../../images/butt_add.gif" OnClick="btnAdd_Click" UseSubmitBehavior="false" Runat="server" /></td>
					</tr>
				</table>
				</td>		
			</tr>			
			<tr>
				<td colspan=6>&nbsp;</td>				
			</tr>
			<tr>
				<td colspan=6><asp:label id=lblConfirmErr text="Document must contain transaction to confirm!" Visible=False forecolor=red Runat="server" /></td>				
			</tr>
			<tr>
				<td colspan=6> <!-- BFR_WTK00041 Item 3 START -->
					<asp:DataGrid id="dgStkTx"
						AutoGenerateColumns="false" width="100%" runat="server"
						OnItemDataBound="DataGrid_ItemCreated" 
						GridLines = none
						Cellpadding = "2"
						Pagerstyle-Visible="False"
						OnDeleteCommand="DEDR_Delete"
						AllowSorting="True" class="font9Tahoma">	
						<HeaderStyle CssClass="mr-h" />							
						<ItemStyle CssClass="mr-l" />
						<AlternatingItemStyle CssClass="mr-r" />	
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
					<asp:TemplateColumn HeaderText="Stock Receive Ref. No.">
						<ItemStyle Width="15%"/> 																								
						<ItemTemplate>
							<%# Container.DataItem("DispAdvID") %>
						</ItemTemplate>
					</asp:TemplateColumn>					
					<asp:TemplateColumn HeaderText="Item">
						<ItemStyle Width="15%"/> 																								
						<ItemTemplate>
							<asp:label text=<%# Container.DataItem("Description") %> id="ItemDesc" runat="server" />
							<asp:label text=<%# Container.DataItem("ItemCode") %> id="ItemCode" visible=false runat="server" />
							<asp:label text=<%# "(" & Trim(Container.DataItem("DispAdvID")) & ")" %> visible=false id="DocID" runat="server" />
							<asp:label text=<%# Container.DataItem("ItemRetAdvlnID") %> id="RtnAdvLnID" visible=false runat="server" />
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
					<asp:TemplateColumn HeaderText="Quantity To Return">
						<HeaderStyle HorizontalAlign="Right" />			
						<ItemStyle Width="10%" HorizontalAlign="Right" />			
						<ItemTemplate>
							<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("Qty"),5) %> id="lblQtyTrx" runat="server" />							
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Unit Cost">
						<HeaderStyle HorizontalAlign="Right" />			
						<ItemStyle Width="10%" HorizontalAlign="Right" />							
						<ItemTemplate>
							<asp:label text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Cost"), 2), 2)  %> id="lblUnitCost" runat="server" />
						</ItemTemplate>							
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Amount">
						<HeaderStyle HorizontalAlign="Right" />			
						<ItemStyle Width="10%" HorizontalAlign="Right" />						
						<ItemTemplate>
							<asp:label text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Amount"), 2), 2)  %> id="lblAmount" runat="server" />
						</ItemTemplate>							
					</asp:TemplateColumn>		
					<asp:TemplateColumn>		
						<ItemStyle Width="5%" HorizontalAlign="Right" />							
						<ItemTemplate>
							<asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" CausesValidation=False runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>
					</Columns>										
					</asp:DataGrid>
				</td><!-- BFR_WTK00041 Item 3 END -->
			</tr>					
			<tr>
				<td colspan=3>&nbsp;</td>
				<td colspan=2 height=25><hr style="width :100%" /></td>
				<td width="5%">&nbsp;</td>					
			</tr>	
			<tr>
				<td colspan=3>&nbsp;</td>
				<td height=25>Total Amount :</td>
				<td align="right"><asp:label id="lblTotAmtFig" runat="server" /></td>						
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25>Remarks :</td>	
				<td colspan="5"><asp:textbox id="txtRemarks" CssClass="font9Tahoma"   width="98%" maxlength="256" runat="server" /></td>
			</tr>
			<tr>
				<td colspan=6>&nbsp;</td>				
			</tr>
			<tr>
				<td colspan=6><asp:label id=lblUnDel class="font9Tahoma" text="Insufficient Stock In Inventory to Perform Operation !" Visible=False forecolor=red Runat="server" />
				</td>				
			</tr>
			<tr>
				<td align="left" colspan="6">
					<asp:ImageButton id="btnSave"    UseSubmitBehavior="false" AlternateText="Save"    onClick="btnSave_Click"    ImageURL="../../images/butt_save.gif"    CausesValidation=False runat="server" />
					<asp:ImageButton id="btnConfirm" UseSubmitBehavior="false" AlternateText="Confirm" onClick="btnConfirm_Click" ImageURL="../../images/butt_confirm.gif" CausesValidation=False runat="server" />
 					<asp:ImageButton id="btnPrint"   UseSubmitBehavior="false" AlternateText="Print"   onClick="btnPreview_Click" ImageURL="../../images/butt_print.gif"   CausesValidation=False runat="server" />
					<asp:ImageButton id="btnDelete"  UseSubmitBehavior="false" AlternateText="Delete"  onClick="btnDelete_Click"  ImageURL="../../images/butt_delete.gif"  CausesValidation=False runat="server" />
					<asp:ImageButton id="btnNew"     UseSubmitBehavior="false" AlternateText="New"     onClick="btnNew_Click"     ImageURL="../../images/butt_new.gif"     CausesValidation=False runat="server" />
					<asp:ImageButton id="btnBack"    UseSubmitBehavior="false" AlternateText="Back"    onClick="btnBack_Click"    ImageURL="../../images/butt_back.gif"    CausesValidation=False runat="server" />
				</td>
			</tr>		
			<tr>
				<td align="left" colspan="6">
                    &nbsp;</td>
			</tr>		
			<tr>
				<td align="left" colspan="5">
					<asp:Label id=lblTxError visible=false Text="Cannot perform operation, please check data!" forecolor=red runat=server />			
				</td>
				<asp:Label id=lblDocTitle visible=false text="Stock Return Advice" runat=server /></tr>	
			
			
				
			<tr>
				<td colspan=5>&nbsp;</td>
			</tr>
			<tr id=TrLink runat=server style="visibility:hidden">
				<td colspan=5 style="height: 18px">
					<asp:LinkButton id=lbViewJournal text="View Journal Predictions" causesvalidation=false runat=server /> 
				</td>
			</tr>
			<tr>
				<td colspan=5>&nbsp;</td>
			</tr>
			<tr>
				<td colspan=6 style="height: 114px">
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
			    <td height=25 align=right><asp:Label id=lblTotalViewJournal Visible=false class="font9Tahoma" runat=server /> </td>
			    <td>&nbsp;</td>	
			    <td align=right><asp:label id="lblTotalDB" text="0" class="font9Tahoma" Visible=false runat="server" /></td>						
			    <td>&nbsp;</td>		
			    <td align=right><asp:label id="lblTotalCR" text="0" class="font9Tahoma" Visible=false runat="server" /></td>				
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
