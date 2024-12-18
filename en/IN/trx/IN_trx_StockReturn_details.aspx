<%@ Page Language="vb" trace="false" src="../../../include/IN_Trx_StockReturn_Details.aspx.vb" Inherits="IN_ReturnDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuINTrx" src="../../menu/menu_INtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Stock Return Details</title>		
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
            .font9Header
            {
                text-align: right;
            }
        </style>
	</head>	
	<body>
		<form id=frmMain class="main-modul-bg-app-list-pu" runat=server>
            <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma" >
		<tr>
            <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">  
   		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
		<asp:label id=lblCode visible=false text=" Code" runat=server />
		<asp:label id="lblStatusHid" Visible="False" Runat="server"></asp:label>
		<asp:label id=lblDocTitle text="Stock Return" visible=false runat=server/>
		<asp:label id=lblTxLnID visible=false runat=server />
		<asp:label id=lblOldQty visible=false runat=server />
		<asp:label id=lblOldItemCode visible=false runat=server />

    
         

		<table border=0 width="100%" class="font9Tahoma" cellspacing="0" cellpading="1">
			<tr>
				<td colspan=6><UserControl:MenuINTrx EnableViewState=False id=menuIN runat="server" /></td>
			</tr>			
			<tr>
				<td  colspan=6>
                    <table cellspacing="1" class="style2">
                        <tr>
                            <td class="font9Tahoma">
                                <strong> STOCK RETURN DETAILS</strong>
                             </td>
                            <td class="font9Header">
                                Period: <asp:Label id=lblAccPeriod runat=server />| Status : <asp:Label id=Status runat=server />| Date Created : <asp:Label id=CreateDate runat=server />| Last Update : <asp:Label id=UpdateDate runat=server />| Update By : <asp:Label id=UpdateBy runat=server />
                                |
                                <asp:Label ID="lblPDateTag" runat="Server" Text="Print Date :"></asp:Label>
&nbsp;<asp:Label id=lblPrintDate  runat=server /> 
                            </td>
                        </tr>
                    </table>
                    <hr style="width :100%" />
                </td>
		 </tr>
  
			<tr>
				<td colspan=6 class="style1"></td>
			</tr>			
			<tr>
				<td>&nbsp;</td>		
			</tr>		
			<tr>
				<td width="20%" height=25>Stock Return ID :</td>
				<td width="40%"><asp:label id=lblStckTxID Runat="server"/></td>
				<td width="20%"><asp:Label id=lblReprint  Text="<B>( R E P R I N T )</B><br>" Visible=False forecolor=Red runat=server />
				</td>
				<td width="5%">&nbsp;</td>
			</tr>
			<tr>
				<td height=25>Stock Return Date :*</td>
				<td><asp:TextBox id=txtDate  CssClass= "font9Tahoma" Width=50% maxlength=10 runat=server />
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
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25>Stock Return Into :*</td>
				<td><asp:DropDownList id="ddlInventoryBin" CssClass= "font9Tahoma" Width=100% runat=server/>
				    <asp:Label id=lblInventoryBin text="Please Select Inventory Bin" forecolor=red visible=false runat=server /></td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>	
			<tr>
				<td height=25 class="style1">Warehouse Into :*</td>
				<td><asp:DropDownList id="lstStorage"  CssClass="fontObject" Width="100%" runat=server/>
				    <asp:Label id=lblstoragemsg text="Please Select Storage" forecolor=red visible=false runat=server /></td>
            </tr>            		
			</table>
            
        <table   border=0 width="100%" cellspacing="0" cellpadding="2" runat="server">
            <tr>
                <td>

			<tr>
				<td colspan=6>
				<table id="tblAdd" class="font9Tahoma" border=0 width="100%" cellspacing="0" cellpadding="4" runat="server">
					<tr class="mb-c">
						<td width="20%" height=25>Stock Issue ID :*</td>
						<td width="80%"><asp:DropDownList id="lstStckIss"  CssClass= "font9Tahoma" Width=90% AutoPostBack=True OnSelectedIndexChanged=CallLoadIssueDetails runat=server />
						                <input type=button value=" ... " id="Find" 
                                onclick="javascript:PopSI('frmMain', '', 'lstStckIss', 'True');" 
                                CausesValidation=False runat=server visible="False" />
										<asp:label id=lblStckIssErr text="Please select Stock Issue ID" Visible=False forecolor=red Runat="server" /></td>
					</tr>
					<tr class="mb-c">
						<td width="20%" height=25>Item Code :*</td>
						<td width="80%"><asp:DropDownList id="lstItem" CssClass= "font9Tahoma" Width=90% AutoPostBack=True OnSelectedIndexChanged=ShowIssuedLnDetails runat=server EnableViewState=True />
						                <input type=button value=" ... " id="FindIN" 
                                onclick="javascript:PopItem('frmMain', '', 'lstItem', 'True');" 
                                CausesValidation=False runat=server visible="False" />
										<asp:label id=lblItemCodeErr text="<br>Please select Item Code" Visible=False forecolor=red Runat="server" /></td>
					</tr>
					<tr id=RowChargeTo class="mb-c">
						<td width="20%" height="25">Charge To :</td>
						<td width="80%"><asp:label id=lblChargeTo Width=100% Runat="server" /></td>
					</tr>
					<tr id="RowAcc" class="mb-c" >
						<td width="20%" height=25><asp:label id="lblAccTag" Runat="server"/> </td>
						<td width="80%"><asp:label id="lblAccCode"  Width=100% runat=server /></td>
					</tr>
					<tr id="RowBlk" class="mb-c" >
						<td height=25><asp:label id=lblBlkTag Runat="server"/>	</td>
						<td><asp:label id="lblBlock" Width=100% runat=server /></td>
					</tr>
					<tr id="RowVeh" class="mb-c" >
						<td height=25><asp:label id="lblVehTag" Runat="server"/> </td>
						<td><asp:label id="lblVehCode" Width=100% runat=server /></td>				
					</tr>
					<tr id="RowVehExp" class="mb-c" >
						<td height=25><asp:label id="lblVehExpTag" Runat="server"/></td>
						<td><asp:label id="lblVehExp" Width=100% runat=server /></td>					
					</tr>
					<tr class="mb-c">
						<td height=25>Quantity to Return :*</td>
						<td><asp:textbox id="txtQty" CssClass= "font9Tahoma" Width=40% maxlength=15 EnableViewState=False Runat="server" />
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
								MinimumValue="0.00001"
								MaximumValue="999999999999999.99999"
								Type="double"
								EnableClientScript="True"
								Text="<br>The value must be from 0.00001!"
								runat="server" display="dynamic"/>
							<asp:label id=lblerror text="Number generated is too big!" Visible=False forecolor=red Runat="server" />
							<asp:label id=lblStock text="Returned quantity cannot larger than issued quantity!" Visible=False forecolor=red Runat="server" />
						</td>				
					</tr>
					<tr class="mb-c">
					    <td valign=top>Additional Note :</td>
                        <td><textarea rows=6 id=txtAddNote cols=50 runat=server></textarea></td>
					</tr>
					<tr class="mb-c">
						<td colspan=2><asp:ImageButton text="Add" id="btnAdd" ImageURL="../../images/butt_add.gif" OnClick="btnAdd_Click" UseSubmitBehavior="false" Runat="server" />&nbsp;
						 <asp:ImageButton text="Save" id="btnUpdate" visible=false ImageURL="../../images/butt_save.gif" OnClick="btnUpdate_Click" Runat="server" />
                            <br />
                        </td>
					</tr>
				</table>
				</td>		
			</tr>
			<tr>
				<td colspan=6><asp:label id=lblConfirmErr CssClass="font9Tahoma" text="<BR>Document must contain transaction to Confirm!" Visible=False forecolor=red Runat="server" />
							  <asp:Label id=lblTxError CssClass="font9Tahoma" visible=false Text="<BR>Cannot perform operation, please check data!" forecolor=red runat=server />
							  <asp:label id=lblUnDel CssClass="font9Tahoma" text="<BR>Insufficient quantity to perform operation!" Visible=False forecolor=red Runat="server" /></td>
			</tr>
			<tr>
				<td colspan=6> 
					<asp:DataGrid id="dgStkTx"
						AutoGenerateColumns="false" width="100%" runat="server"
						OnItemCreated="DataGrid_ItemCreated" 
						GridLines = none
						Cellpadding = "2"
						Pagerstyle-Visible="False"
						OnDeleteCommand="DEDR_Delete"
						OnEditCommand="DEDR_Edit"
						OnCancelCommand="DEDR_Cancel"
						AllowSorting="True" class="font9Tahoma"> 	
						<HeaderStyle HorizontalAlign="Left" CssClass="mr-h" />							
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
						<asp:TemplateColumn HeaderText="Stock Issue ID">
							<ItemStyle Width="8%"/> 																								
							<ItemTemplate>
								<asp:label text=<%# Container.DataItem("StockIssueID") %> id="stkIssueID" runat="server" />
							</ItemTemplate>
						</asp:TemplateColumn>						
						<asp:TemplateColumn HeaderText="Item">
							<ItemStyle Width="8%"/> 																								
							<ItemTemplate>
								<asp:label text=<%# Container.DataItem("ItemCode") %> id="ItemCode" runat="server" />
								( <%# Container.DataItem("Description") %> )		
								<asp:label text=<%# Container.DataItem("StockRtnLnID") %> id="RtnLnID" visible=false runat="server" />
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
								<asp:label text=<%# Container.DataItem("AccCode") %> Visible=True id="AccCode" runat="server" />
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
						
						<asp:TemplateColumn HeaderText="Employee Code">
							<ItemStyle Width="8%"/> 																								
							<ItemTemplate>
								<%# Container.DataItem("PsEmpCode") %>
							</ItemTemplate>
						</asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Additional Note">
						    <ItemTemplate>
							    <asp:Label Text=<%# Container.DataItem("AdditionalNote") %> id="lblAddNote" runat="server" />
						    </ItemTemplate>
					    </asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Quantity Returned">
							<HeaderStyle HorizontalAlign="Right" />			
							<ItemStyle Width="8%" HorizontalAlign="Right" />			
							<ItemTemplate>
							    <asp:label text=<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("Qty"),5) %> id="lblQtyTrxDisplay" runat="server" />							
								<asp:label text=<%# Container.DataItem("Qty") %> id="lblQtyTrx" visible = "false" runat="server" />							
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Unit Cost">
							<HeaderStyle HorizontalAlign="Right" />			
							<ItemStyle Width="8%" HorizontalAlign="Right" />							
							<ItemTemplate>
								<asp:label text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Cost"), 2), 2) %> id="lblUnitCost" runat="server" />
							</ItemTemplate>							
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Cost Amount">
							<HeaderStyle HorizontalAlign="Right" />			
							<ItemStyle Width="8%" HorizontalAlign="Right" />						
							<ItemTemplate>
								<asp:label text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Amount"), 2), 2) %> id="lblAmount" runat="server" />
							</ItemTemplate>							
						</asp:TemplateColumn>		
						<asp:TemplateColumn HeaderText="Unit Price">
							<HeaderStyle HorizontalAlign="Right" />			
							<ItemStyle Width="8%" HorizontalAlign="Right" />						
							<ItemTemplate>
								<asp:label text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Price"), 2), 2) %> id="lblPrice" runat="server" />
							</ItemTemplate>							
						</asp:TemplateColumn>		
						<asp:TemplateColumn HeaderText="Price Amount">
							<HeaderStyle HorizontalAlign="Right" />			
							<ItemStyle Width="8%" HorizontalAlign="Right" />						
							<ItemTemplate>
								<asp:label text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PriceAmount"), 2), 2) %> id="lblPriceAmount" runat="server" />
							</ItemTemplate>							
						</asp:TemplateColumn>		
						<asp:TemplateColumn>		
							<ItemStyle HorizontalAlign="right" Width="4%" />							
							<ItemTemplate>
							    <asp:LinkButton id="Edit" CommandName="Edit" Text="Edit" CausesValidation =False runat="server" />												
							    <asp:LinkButton id="Cancel" CommandName="Cancel" Text="Cancel" visible=false CausesValidation =False runat="server" />												
								<asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" visible=false CausesValidation =False runat="server" />
							</ItemTemplate>
						</asp:TemplateColumn>
						</Columns>										
					</asp:DataGrid>
				</td>	
			</tr>
			<tr>
				<td colspan=3>&nbsp;</td>
				<td colspan=3 height=25><hr style="width :100%" /></td>
			</tr>				
			<tr>
				<td colspan=3>&nbsp;</td>
				<td colspan=3 height=25>
					<table width=100% border=0 class="font9Tahoma">
						<tr class="font9Tahoma">
							<td width=18% height=25>Total Cost : </td>
							<td width=24% align=right> <asp:label id="lblTotAmtFig" CssClass="font9Tahoma" runat="server" /> </td>
							<td width=10%>&nbsp;</td>
							<td width=18% height=25>Total Price : </td>
							<td width=24% align=right> <asp:label id="lblTotPriceFig" CssClass="font9Tahoma" runat="server" /> </td>
							<td witdh=6%>&nbsp;</td>
						</tr>
					</table>
				</td>
				
			</tr>
			<tr>
				<td colspan=6>&nbsp;</td>				
			</tr>
			<tr>
				<td>Remarks :</td>	
				<td colspan=5><asp:textbox id="txtRemarks" CssClass= "font9Tahoma" width="98%" maxlength="128" runat="server" /></td>
			</tr>
			<tr>
				<td colspan=6>
								<asp:label id=lblBPErr class="font9Tahoma" text="Unable to generate Debit Note, please create a Bill Party for " Visible=False forecolor=red Runat="server" />
								<asp:label id=lblLocCodeErr class="font9Tahoma" text="" Visible=False forecolor=red Runat="server" />
								
				</td>				
			</tr>
			<tr>
				<td align="left" colspan="6">
					<asp:ImageButton id="btnNew"   UseSubmitBehavior="false" AlternateText="New"     onClick="btnNew_Click"     ImageURL="../../images/butt_new.gif"     CausesValidation=False runat="server" />
					<asp:ImageButton id="Save"     UseSubmitBehavior="false" AlternateText="Save"    onClick="btnSave_Click"    ImageURL="../../images/butt_save.gif"    CausesValidation=False runat="server" />
					<asp:ImageButton id="Confirm"  UseSubmitBehavior="false" AlternateText="Confirm" onClick="btnConfirm_Click" ImageURL="../../images/butt_confirm.gif" CausesValidation=False runat="server" />
					<asp:ImageButton id="Cancel"   UseSubmitBehavior="false" AlternateText="Cancel"  onClick="btnCancel_Click"  ImageURL="../../images/butt_Cancel.gif"  CausesValidation=False runat="server" />
					<asp:ImageButton id="Print"    UseSubmitBehavior="false" AlternateText="Print"   onClick="btnPreview_Click" ImageURL="../../images/butt_print.gif"   CausesValidation=False runat="server" />
					<asp:ImageButton id="PRDelete" UseSubmitBehavior="false" AlternateText="Delete"  onClick="btnDelete_Click"  ImageURL="../../images/butt_delete.gif"  CausesValidation=False runat="server" />
					<asp:ImageButton id="Back"     UseSubmitBehavior="false" AlternateText="Back"    onClick="btnBack_Click"    ImageURL="../../images/butt_back.gif"    CausesValidation=False runat="server" />
				</td>
			</tr>		
			
			
				
			<tr>
				<td align="left" colspan="6">
					&nbsp;</td>
			</tr>		
			
			
				
			<tr>
				<td colspan=5>&nbsp;</td>
			</tr>
			<tr id=TrLink runat=server>
				<td colspan=5>
					<asp:LinkButton id=lbViewJournal class="font9Tahoma" text="View Journal Predictions" causesvalidation=false runat=server /> 
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
			    <td>&nbsp;</td>								
			    <td height=25 align=right><asp:Label CssClass="font9Tahoma" id=lblTotalViewJournal Visible=false runat=server /> </td>
			    <td>&nbsp;</td>	
			    <td align=right><asp:label id="lblTotalDB" CssClass="font9Tahoma" text="0" Visible=false runat="server" /></td>						
			    <td>&nbsp;</td>		
			    <td align=right><asp:label id="lblTotalCR" CssClass="font9Tahoma" text="0" Visible=false runat="server" /></td>				
		    </tr>
                            </td>
            </tr>
		</table>
                </div>
            </td>
        </tr>
        </table>

			<Input type=hidden id=hidBlockCharge value="BC" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="CLC" runat=server/>
		</form>
	</body>
</html>
