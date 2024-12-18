<%@ Page Language="vb" src="../../../include/IN_trx_StockReserv_details.aspx.vb" Inherits="IN_STOCKRESERVDET" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuINTrx" src="../../menu/menu_INtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Stock Reserv Details</title>	
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />	
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<form id=frmMain runat=server class="main-modul-bg-app-list-pu">

        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">


   		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
		<asp:Label id=lblTo visible=false text="To " runat=server />
		<asp:Label id=lblCode visible=false text=" Code " runat=server />
		<asp:Label id=lblPleaseSelectOne visible=false text="Please select one " runat=server />
		<asp:Label id=lblSelect visible=false text="Select " runat=server />
		<asp:label id="lblStatusHid" Visible="False" Runat="server" />
		<asp:label id=lblDocTitle visible=false text="Stock Reserv" runat=server />			
		<table border=0 width="100%" cellspacing="0" cellpading="1" class="font9Tahoma">
			<tr>
				<td colspan=6 style="height: 42px"><UserControl:MenuINTrx enableviewstate=false id=menuIN runat="server" /></td>
			</tr>			
			<tr>
				<td colspan=6>RESERVATION STOCK DETAILS</td>
			</tr>
			<tr>
				<td colspan=6><hr style="width :100%" /></td>
			</tr>			
			<tr>
				<td>&nbsp;<asp:Label id=lblReprint  Text="<B>( R E P R I N T )</B><br>" Visible=False forecolor=Red runat=server /></td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>
                    &nbsp;</td>				
			</tr>		
			<tr>
				<td width="20%" height=25>Stock Reserv ID :</td>
				<td width="40%"><asp:label id=lblStckTxID Runat="server"/></td>
				<td width="5%">&nbsp;</td>
				<td width="15%">Period : </td>
				<td width="20%"><asp:Label id=lblAccPeriod runat=server /></td>
				<td width="5%">&nbsp;</td>
			</tr>
			<tr>
				<td height=25>Stock Reserv Date :*</td>
				<td><asp:TextBox id=txtDate Width=50% maxlength=10 runat=server />
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
				<td>Status :</td>
				<td><asp:Label id=Status runat=server /></td>
				<td>&nbsp;</td>
			</tr>
			<tr><td height=25>Stock Reserv From :*</td>
				<td><asp:DropDownList id="ddlInventoryBin" Width=100% runat=server/>
				    <asp:Label id=lblInventoryBin text="Please Select Division" forecolor=red visible=false runat=server /></td>
				<td>&nbsp;</td>
				<td>Date Created :</td>
				<td><asp:Label id=CreateDate runat=server /></td>
				<td>&nbsp;</td>
			</tr>			
			<tr>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td style="height: 40px">Last Update :</td>
				<td style="height: 40px"><asp:Label id=UpdateDate runat=server /></td>	
				<td style="height: 40px">&nbsp;</td>
			</tr>			
			<tr>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>Updated By :</td>
				<td><asp:Label id=UpdateBy runat=server /></td>				
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25>&nbsp;<asp:label id=lblToLocErr Visible=False forecolor=red Runat="server" /></td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td><asp:Label id=lblPDateTag visible=False Text="Print Date :" runat=Server /></td>
				<td><asp:Label id=lblPrintDate  runat=server Visible="False" /></td>				
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td style="height: 25px">&nbsp;</td>
				<td style="height: 25px">&nbsp;</td>
				<td style="height: 25px">&nbsp;</td>
				<td style="height: 25px"><asp:Label id=lblDNIDTag visible=False Text="Debit Note ID :" runat=Server /></td>
				<td style="height: 25px"><asp:Label id=lblDNNoteID  runat=server Visible="False" /></td>				
				<td style="height: 25px">&nbsp;</td>
			</tr>
            </table>

            <table width="99%" id="tblDetail" class="sub-Add" runat="server" >
			<tr>
				<td colspan=6>
				<table id="tblAdd" border=0 width="100%" cellspacing="0" cellpadding="4" runat="server">
					<tr class="mb-c">
						<td width="20%">Item Code :*</td>
						<td width="80%">
                            <asp:TextBox ID="txtItemCode" runat="server" AutoPostBack="True" MaxLength="15" Width="20%"></asp:TextBox>
                            <input id="FindIN" runat="server" causesvalidation="False" onclick="javascript:PopItem_New('frmMain', '', 'txtItemCode','TxtItemName','UnitCost', 'False');"
                                type="button" value=" ... " visible="true" />
                            <asp:TextBox ID="TxtItemName" runat="server" BackColor="Transparent" BorderStyle="None"
                                ForeColor="White" MaxLength="10" Width="60%"></asp:TextBox>
                            &nbsp;&nbsp;
							<asp:label id=lblItemCodeErr text="<br>Please select one Item" Visible=False forecolor=red Runat="server" />
						</td>
					</tr>
					<tr class="mb-c">
						<td>Quantity :*</td>
						<td><asp:textbox id="txtQty" Width="10%" maxlength=15 EnableViewState="False" Runat="server" />
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
							<asp:label id=lblStock text="Not enough stock in hand!" Visible=False forecolor=red Runat="server" />
						</td>
					</tr>
					<tr class="mb-c">
						<td colspan=2><asp:ImageButton text="Add" id="btnAdd" ImageURL="../../images/butt_add.gif" OnClick="btnAdd_Click" UseSubmitBehavior="false" Runat="server" />
                                            </td>
					</tr>
				</table>
				</td>		
			</tr>
            </table>

            <table style="width: 100%" class="font9Tahoma">
			<tr>
				<td colspan=6>&nbsp;</td>				
			</tr>
			<tr>
				<td colspan=6><asp:label id=lblConfirmErr text="Document must contain transaction to Confirm!" Visible=False forecolor=red Runat="server" />
							  <asp:label id=lblUnDel text="Insufficient stock in Inventory to perform operation!" Visible=False forecolor=red Runat="server" /></td>
			</tr>
			<tr>
				<td colspan=6> 
					<asp:DataGrid id="dgStkTx"
						AutoGenerateColumns="False" width="100%" runat="server"
						OnItemCreated="DataGrid_ItemCreated" 
						GridLines = None
						Cellpadding = "2"
						Pagerstyle-Visible="False"
						OnDeleteCommand="DEDR_Delete"
						AllowSorting="True"
                            class="font9Tahoma">	
							 
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
						<asp:TemplateColumn HeaderText="Item">
							<ItemStyle Width="20%"/> 																								
							<ItemTemplate>
								<asp:label text=<%# Container.DataItem("ItemCode") %> id="ItemCode" runat="server" />				
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Description">
							<ItemStyle Width="60%"/> 																								
							<ItemTemplate>
								<asp:label text=<%# Container.DataItem("Description") %> id="Description" runat="server" />			
							</ItemTemplate>
						</asp:TemplateColumn>						
						<asp:TemplateColumn HeaderText="Quantity">
							<HeaderStyle HorizontalAlign="Right" />			
							<ItemStyle Width="20%" HorizontalAlign="Right" />			
							<ItemTemplate>
								<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("Qty"),5) %> id="lblQtyTrx" runat="server" />							
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn>		
							<ItemStyle Width="5%" HorizontalAlign="Right" />							
							<ItemTemplate>
								<asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" CausesValidation =False runat="server" />
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
			<tr>
				<td colspan=3>&nbsp;</td>			
				<td height=25>Total Amount :</td>
				<td align=right><asp:label id="lblTotAmtFig" runat="server" /></td>						
				<td align="right">&nbsp;</td>						
			</tr>
			<tr>
				<td colspan=6>&nbsp;</td>				
			</tr>
			<tr>
				<td height=25>Remarks :</td>	
				<td colspan="5"><asp:textbox id="txtRemarks" width="100%" maxlength="128" runat="server" /></td>
			</tr>
			<tr>
				<td colspan=6>&nbsp;</td>				
			</tr>
			<tr>
				<td align="left" colspan="6" style="height: 26px">
					<asp:ImageButton id="btnNew"    UseSubmitBehavior="false" AlternateText="New"        onClick="btnNew_Click"       ImageURL="../../images/butt_new.gif"       CausesValidation=False runat="server" />
					<asp:ImageButton id="Save"      UseSubmitBehavior="false" AlternateText="Save"       onClick="btnSave_Click"      ImageURL="../../images/butt_save.gif"      CausesValidation=False runat="server" />
					<asp:ImageButton id="Print"     UseSubmitBehavior="false" AlternateText="Print"      onClick="btnPreview_Click"   ImageURL="../../images/butt_print.gif"     CausesValidation=False runat="server" />
					<asp:ImageButton id="PRDelete"  UseSubmitBehavior="false" AlternateText="Delete"     onClick="btnDelete_Click"    ImageURL="../../images/butt_delete.gif"    CausesValidation=False runat="server" />
					<asp:ImageButton id="Back"      UseSubmitBehavior="false" AlternateText="Back"       onClick="btnBack_Click"      ImageURL="../../images/butt_back.gif"      CausesValidation=False runat="server" />
					<asp:Button id="Approved"  Text="Approved"  onClick="btnApproved_Click"   runat="server" />
					<asp:Button id="Verified"  Text="Verified"  onClick="btnVerified_Click"   runat="server" />
					<asp:Button id="Confirm"   Text="Confirm"    onClick="btnConfirm_Click"   runat="server" />

				</td>
			</tr>		
			
			
				
			<tr>
				<td align="left" colspan="6" style="height: 26px">
                                            &nbsp;</td>
			</tr>		
			
			
				
			<tr>
				<td colspan=5 style="height: 21px">&nbsp;</td>
			</tr>
			<tr id=TrLink runat=server>
				<td colspan=5>
					<asp:LinkButton id=lbViewJournal text="View Journal Predictions" causesvalidation=false runat=server Visible="False" /> 
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
						AllowSorting="false"
                            class="font9Tahoma">	
							 
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
                        <PagerStyle Visible="False" />
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
            <asp:textbox id="UnitCost" Width="1%" maxlength=15 EnableViewState="False" Runat="server" BackColor="Black" BorderStyle="None" />



        <br />
        </div>
        </td>
        </tr>
        </table>


		</form>
	</body>
</html>
