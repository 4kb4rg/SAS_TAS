<%@ Page Language="vb" trace="False" src="../../../include/IN_Trx_StockIssue_Details.aspx.vb" Inherits="IN_IssueDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuINTrx" src="../../menu/menu_INtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Stock Issue Details</title>		
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<form id=frmMain runat=server>
   		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
		<asp:label id=lblCode visible=false text=" Code" runat=server />
		<asp:label id=lblSelect visible=false text="Select " runat=server />
		<asp:label id=lblPleaseSelect visible=false text="Please select " runat=server />
		<asp:label id=lblTxLnID visible=false runat=server />
		<asp:label id=lblOldQty visible=false runat=server />
		<asp:label id=lblOldItemCode visible=false runat=server />
				
		<table border=0 width="100%" cellspacing="0" cellpading="1">
			<tr>
				<td colspan=6><UserControl:MenuINTrx EnableViewState=False id=menuIN runat="server" /></td>
			</tr>			
			<tr>
				<td class="mt-h" colspan=6><asp:label id="lblStkName" runat="server"/></td>
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
				<td width="20%" height=25><asp:label id="lblStkID" runat="server"/> :</td>
				<td width="40%"><asp:label id=lblStckTxID Runat="server"/></td>
				<td width="5%">&nbsp;</td>
				<td width="15%">Period : </td>
				<td width="20%"><asp:Label id=lblAccPeriod runat=server /></td>
				<td width="5%">&nbsp;</td>
			</tr>
			<tr>
				<td height=25>
					<asp:label id=lblBillTo text="Bill To :*" Visible=False Runat="server"/>
					<asp:label id=lblBPartyTag Visible=False Runat="server"/>
					<asp:label id=lblLocationTag Text="Charge To :*" Visible=False Runat="server"/>
				</td>
				<td>
					<asp:DropDownList id="lstBillTo" Width=100% Visible=False runat=server />
					<asp:DropDownList id="lstBillParty" Width=100% Visible=False runat=server />
					<asp:DropDownList id="ddlLocation" Width=100% Visible=False AutoPostBack=True OnSelectedIndexChanged=ddlLocation_OnSelectedIndexChanged runat=server /> 
					<asp:label id=lblBillPartyErr Visible=False forecolor=red Runat="server" />
					<asp:label id=lblLocationErr Visible=False forecolor=red Runat="server" />
				</td>			
				<td>&nbsp;</td>
				<td>Status : </td>
				<td><asp:Label id=Status runat=server /></td>
				<td>&nbsp;</td>
			</tr>			
			<tr>
				<td height=25><asp:label id=lblChargeMarkUp visible=False text="Charge :" runat=server /></td>
				<td><asp:CheckBox id=chkMarkUp visible=false Checked =False Runat="server"/></td>
				<td>&nbsp;</td>
			    <td>Date Created : </td>
				<td><asp:Label id=CreateDate runat=server /></td>
				<td>&nbsp;</td>
			</tr>			
			
			<tr>
				<td height=25>Issue Date :*</td>
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
				<td>Last Update :</td>
				<td><asp:Label id=UpdateDate runat=server /></td>	
				<td>&nbsp;</td>
			</tr>
			
			<tr>
				<td height=25><asp:label visible=false text="Stock Issue Type :" runat=server /> </td>
				<td><asp:label id=lblStkIssType visible=false Runat="server"/></td>
				<td>&nbsp;</td>
				<td>Updated By :</td>
				<td><asp:Label id=UpdateBy runat=server /></td>				
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25>Issue From :*</td>
				<td><asp:DropDownList id="ddlInventoryBin" Width=100% runat=server AutoPostBack=false OnSelectedIndexChanged="ddlInventoryBin_OnSelectedIndexChanged"/>
				    <asp:Label id=lblInventoryBin text="Please Select Inventory Bin" forecolor=red visible=false runat=server /></td>
				<td>&nbsp;</td>
				<td>Print Date :</td>
				<td><asp:Label id=lblPrintDate runat=server /></td>				
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td><asp:Label id=lblDNIDTag visible=False Text="Debit Note ID :" runat=Server /></td>
				<td><asp:Label id=lblDNNoteID  runat=server /></td>				
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td colspan=6>
				<table id="tblAdd" border=0 width="100%" cellspacing="0" cellpadding="4" runat="server">
					<tr class="mb-c">
						<td width="20%" height=25>Item Code :*</td>
						<td width="80%"><asp:DropDownList id="lstItem" Width=90% AutoPostBack=True OnSelectedIndexChanged=ValidateBlkBatch runat=server EnableViewState=True />
										<input type=button value=" ... " id="Find" onclick="javascript:PopItem('frmMain', '', 'lstItem', 'True');" CausesValidation=False runat=server />
										<asp:label id=lblItemCodeErr text="<br>Please select one Item" Visible=False forecolor=red Runat="server" />
						</td>
					</tr>
					<tr id="RowEmp" class="mb-c">
						<td width="20%" height=25>Employee Code (DR) :*</td>
						<td width="80%"><asp:DropDownList id="lstEmpID" Width=90% runat=server />
										<input type=button value=" ... " id="FindEmp" onclick="javascript:findcode('frmMain','','','','','','','','lstEmpID','','','','','','','',hidBlockCharge.value,hidChargeLocCode.value);" CausesValidation=False runat=server />
										<asp:label id=lblEmpCodeErr text="<br>Please select one Employee Code" Visible=False forecolor=red Runat="server" />
						</td>
					</tr>
					<tr id="RowAcc" class="mb-c" >
						<td width="20%" height=25 ><asp:label id="lblAccTag" Runat="server"/></td>
						<td width="80%"><asp:DropDownList id="lstAccCode" Width=90% AutoPostBack=True OnSelectedIndexChanged=CallCheckVehicleUse runat=server />
						                <input type=button value=" ... " id="FindAcc" onclick="javascript:PopCOA('frmMain', '', 'lstAccCode', 'True');" CausesValidation=False runat=server />  
										<asp:label id=lblAccCodeErr text="<br>Please select one Account Code" Visible=False forecolor=red Runat="server" />
						</td>
					</tr>
					<tr id="RowChargeLevel" class="mb-c">
						<td width="20%" height="25">Charge Level :* </td>
						<td width="80%"><asp:DropDownList id="lstChargeLevel" Width=100% AutoPostBack=True OnSelectedIndexChanged=lstChargeLevel_OnSelectedIndexChanged runat=server /> 
						</td>
					</tr>
					<tr id="RowPreBlk" class="mb-c">
						<td width="20%" height="25"><asp:label id=lblPreBlkTag Runat="server"/> </td>
						<td width="80%"><asp:DropDownList id="lstPreBlock" Width=100% AutoPostBack=True OnSelectedIndexChanged=BindPreBlkBatchNo runat=server />
							<asp:label id=lblPreBlockErr text="<br>Please select one Block Code" Visible=False forecolor=red Runat="server" /></td>
					</tr>
					<tr id="RowBlk" class="mb-c" >
						<td width="20%" height=25><asp:label id=lblBlkTag Runat="server"/></td>
						<td width="80%"><asp:DropDownList id="lstBlock" Width=100% AutoPostBack=True OnSelectedIndexChanged=BindBlkBatchNo runat=server />
										<asp:label id=lblBlockErr text="<br>Please select one Block Code" Visible=False forecolor=red Runat="server" />
						</td>
					</tr>
					<tr id="RowBatchNo" class="mb-c">
						<td Width="20%" Height="25"><asp:Label ID="lblBatchNoTag" Runat="Server" /> :*</td>
						<td width="80%"><asp:DropDownList id="lstBatchNo" Width=100% runat=server />
							<asp:label id=lblBatchNoErr text="<br>Please select one Block Code" Visible=False forecolor=red Runat="server" /></td>
					</tr>			
					<tr id="RowVeh" class="mb-c" >
						<td width="20%" height=25><asp:label id="lblVehTag" Runat="server"/> </td>
						<td width="80%"><asp:DropDownList id="lstVehCode" Width=100% runat=server />
										<asp:label id=lblVehCodeErr Visible=False forecolor=red Runat="server" />
						</td>
					</tr>
					<tr id="RowVehExp" class="mb-c" >
						<td width="20%" height=25><asp:label id="lblVehExpTag" Runat="server"/> </td>
						<td width="80%"><asp:DropDownList id="lstVehExp" Width=100% runat=server />
										<asp:label id=lblVehExpCodeErr Visible=False forecolor=red Runat="server" />
						</td>
					</tr>
					<tr class="mb-c">
						<td width="20%" height="25">Quantity to Issue :*</td>
						<td width="80%">
							<asp:textbox id="txtQty" Width=50% maxlength=15 Runat="server" />
			                <asp:RegularExpressionValidator id="RegularExpressionValidatorQtyReq" 
								ControlToValidate="txtQty"
								ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"
								Display="Dynamic"
								text = "<br>Maximum length 9 digits and 5 decimal points"
								runat="server"/>
							<asp:RequiredFieldValidator 
								id="validateQty" 
								runat="server" 
								ErrorMessage="<br>Please specify quantity to issue" 
								ControlToValidate="txtQty" 
								display="dynamic"/>
							<asp:RangeValidator 
								id="Range1"
								ControlToValidate="txtQty"
								MinimumValue="0.00001"
								MaximumValue="999999999999999"
								Type="double"
								EnableClientScript="True"
								Text="The value is out of acceptable range"
								runat="server" display="dynamic"/>								
							<asp:label id=lblErrNum text="Number generated is too big!" Visible=False forecolor=red Runat="server" />
							<asp:label id=lblErrStock text="Not Enough Stock in Hand!" Visible=False forecolor=red Runat="server" />						
						</td>
					</tr>
					<tr id="RowCost" class="mb-c">
						<td height="25">Unit Cost :*</td>
						<td><asp:TextBox id=txtCost width=50% maxlength=21 runat=server />
							<asp:RegularExpressionValidator id="RegularExpressionValidatorCost" 
								ControlToValidate="txtCost"
								ValidationExpression="\d{1,19}\.\d{0,2}|\d{1,19}"
								Display="Dynamic"
								text = "Maximum length 21 digits."
								runat="server"/>
							<asp:RequiredFieldValidator 
								id="validateCost" 
								runat="server" 
								ErrorMessage="Please Specify Unit Cost" 
								ControlToValidate="txtCost" 
								display="dynamic"/>
							<asp:label id=lblErrCost text="Number generated is too big!" Visible=False forecolor=red Runat="server" />
						</td>
					</tr>
					<tr class="mb-c">
					    <td valign=top>Additional Note :</td>
                        <td><textarea rows=6 id=txtAddNote cols=50 runat=server></textarea></td>
					</tr>
					<tr class="mb-c">
						<td colspan=2><asp:ImageButton text="btnAdd" id="btnAdd" ImageURL="../../images/butt_add.gif" OnClick="btnAdd_Click" UseSubmitBehavior="false" Runat="server" />&nbsp;
						 <asp:ImageButton text="Save" id="btnUpdate" visible=false ImageURL="../../images/butt_save.gif" OnClick="btnUpdate_Click" Runat="server" /></td>
					</tr>
				</table>
				</td>		
			</tr>
			<tr>
				<td colspan=6>&nbsp;</td>				
			</tr>
			<tr>
				<td colspan=6><asp:label id=lblConfirmErr text="<BR>Document must contain transaction to Confirm!" Visible=False forecolor=red Runat="server" />
							  <asp:label id=lblUnDel text="<BR>Insufficient stock in Inventory to perform operation!" Visible=False forecolor=red Runat="server" /></td>
			</tr>			
			<tr>
				<td colspan=6> 
					<asp:DataGrid id="dgStkTx"
						AutoGenerateColumns="false" width="100%" runat="server"
						OnItemDataBound="DataGrid_ItemCreated" 
						GridLines = none
						Cellpadding = "2"
						Pagerstyle-Visible="False"
						OnDeleteCommand="DEDR_Delete"
						OnEditCommand="DEDR_Edit"
						OnCancelCommand="DEDR_Cancel"
						AllowSorting="True">	
						<HeaderStyle HorizontalAlign="Left" CssClass="mr-h" />							
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
					<asp:TemplateColumn HeaderText="Item &lt;br&gt; Additional Note">
							<ItemTemplate>
							<asp:label text=<%# Container.DataItem("StockIssueLnID") %> Visible=false id="LnID" runat="server" />
							<asp:label text=<%# Container.DataItem("ItemCode") %> id="ItemCode" runat="server" />
							<br />( <%# Container.DataItem("Description") %> )
							<br />
							<asp:Label Text=<%# Container.DataItem("AdditionalNote") %> id="lblAddNote" runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>							
					<asp:TemplateColumn>
						<ItemTemplate>
							<asp:label text=<%# Container.DataItem("AccCode") %> Visible=True id="AccCode" runat="server" />
							<asp:label text=<%# Container.DataItem("PsEmpCode") %> Visible=True id="PsEmpCode" runat="server" /><br />
							<asp:label text=<%# Container.DataItem("AccDescr") %> Visible=True id="AccDescr" runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>					
					<asp:TemplateColumn>
						<ItemTemplate>
							<asp:label text=<%# Container.DataItem("BlkCode") %> id="lblBlkCode" runat="server" /><br />
							<asp:label text=<%# Container.DataItem("BlkDesc") %> Visible=True id="BlkDescr" runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
						<ItemTemplate>
							<asp:label text=<%# Container.DataItem("BatchNo") %> id="lblBatchNo" runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>	
					<asp:TemplateColumn>
						<ItemTemplate>
							<asp:label text=<%# Container.DataItem("VehCode") %> id="lblVehCode" runat="server" /><br />
							<asp:label text=<%# Container.DataItem("VehExpCode") %> id="lblVehExpCode" runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Quantity Issued">
						<ItemStyle HorizontalAlign="Right" />			
						<HeaderStyle HorizontalAlign="Right" />			
						<ItemTemplate>
							<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("Qty"),5) %> id="lblQtyTrxDisplay" runat="server" />							
							<asp:label text=<%# Container.DataItem("Qty") %> id="lblQtyTrx" visible = "false" runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Unit Cost">
						<HeaderStyle HorizontalAlign="Right" />			
						<ItemStyle HorizontalAlign="Right" />							
						<ItemTemplate>
							<asp:label text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Cost"), 2), 2)%> id="lblIDUnitCost" runat="server" />
							<asp:label text=<%# Container.DataItem("Cost")%> id="lblUnitCost" visible=false runat="server" />
						</ItemTemplate>							
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Cost Amount">
						<HeaderStyle HorizontalAlign="Right" />			
						<ItemStyle HorizontalAlign="Right" />						
						<ItemTemplate>
							<asp:label text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Amount"), 2), 2)%> id="lblIDAmount" runat="server" />
							<asp:label text=<%# Container.DataItem("Amount")%> id="lblAmount" visible=false runat="server" />
						</ItemTemplate>							
					</asp:TemplateColumn>		
					<asp:TemplateColumn HeaderText="Unit Price" Visible=False>
						<HeaderStyle HorizontalAlign="Right" />			
						<ItemStyle HorizontalAlign="Right" />							
						<ItemTemplate>
							<asp:label text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Price"), 2), 2) %> id="lblIDUnitPrice" Visible=false runat="server" />
							<asp:label text=<%# Container.DataItem("Price")%> id="lblUnitPrice" visible=false runat="server" />
						</ItemTemplate>							
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Price Amount" Visible=False>
						<HeaderStyle HorizontalAlign="Right" />			
						<ItemStyle HorizontalAlign="Right" />						
						<ItemTemplate>
							<asp:label text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PriceAmount"), 2), 2) %> id="lblIDPriceAmount" Visible=false runat="server" />
							<asp:label text=<%# Container.DataItem("PriceAmount")%> id="lblPriceAmount" visible=false runat="server" />
						</ItemTemplate>							
					</asp:TemplateColumn>		
					<asp:TemplateColumn HeaderText="To Charge" Visible = False>
						<ItemTemplate>
						<asp:label text=<%# Container.DataItem("ToCharge") %> id="lblToCharge" runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>		
						<ItemStyle HorizontalAlign="Right" Width="5%" />							
						<ItemTemplate>
							<asp:LinkButton id="Edit" CommandName="Edit" Text="Edit" visible=false CausesValidation =False runat="server" />												
							<asp:LinkButton id="Cancel" CommandName="Cancel" Text="Cancel" visible=false CausesValidation =False runat="server" />												
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
				<td colspan=3 height=25><hr size="1" noshade></td>
			</tr>				
			<tr>
				<td colspan=3>&nbsp;</td>
				<td colspan=3>
					<table border=0 width="100%" cellspacing="0" cellpadding="1" runat="server">
						<tr>
							<td width="20%" height="25">&nbsp;</td>
							<td width="15%" align=right><asp:label id="lblTotCost" Runat="server"/></td>
							<td width="15%" align=center><asp:label id="lblTotAmtFig" runat="server" /></td>
							<td width="20%">&nbsp;</td>
							<td width="15%" align=right>Total Price : </td>
							<td width="15%" align=center><asp:label id="lblTotPriceFig" runat="server" /></td>					
						</tr>
					</table>
				</td>						
			</tr>
			<tr>
				<td colspan=6>&nbsp;</td>				
			</tr>
			<tr>
				<td height=25>SIS Reference Remark :</td>	
				<td colspan="5"><asp:textbox id=txtRefSIS width="50%" wrap=true maxlength="50" runat="server" /></td>
			</tr>
			<tr>
				<td height=25>SIS Date Remark :</td>	
				<td colspan="5"><asp:textbox id=txtRefDate width="50%" wrap=true maxlength="10" runat="server" /></td>
			</tr>
			<tr>
				<td height=25>General Remarks :</td>	
				<td colspan="5"><asp:textbox id="txtRemarks" width="100%" wrap=true maxlength="256" runat="server" /></td>
			</tr>
			<tr>
				<td colspan=6>&nbsp;</td>				
			</tr>
			<tr>
				<td colspan="6">
					<asp:checkboxlist id="cblDisplayCost" runat="server">
						<asp:listitem id=option1 value="Display Unit Price in Stock Issue Slip." selected runat="server"/>
					</asp:checkboxlist>
				</td>
			</tr>
			<tr>
				<td colspan=6><comment>Minamas FS 2.2 - Loo 28/09/2005 - START</comment>
								<asp:label id=lblBPErr text="Unable to generate Debit Note, please create a Bill Party for " Visible=False forecolor=red Runat="server" />
								<asp:label id=lblLocCodeErr text="" Visible=False forecolor=red Runat="server" />
								<comment>Minamas FS 2.2 - Loo 28/09/2005 - START</comment>
				</td>				
			</tr>
			<tr>
				<td align="left" colspan="6">
				    <asp:ImageButton id="btnNew"       UseSubmitBehavior="false" AlternateText="New"        onclick="btnNew_Click"       ImageURL="../../images/butt_new.gif"       CausesValidation=False runat="server" />
					<asp:ImageButton id="btnSave"      UseSubmitBehavior="false" AlternateText="Save"       onClick="btnSave_Click"      ImageURL="../../images/butt_save.gif"      CausesValidation=False runat="server" />
					<asp:ImageButton id="btnConfirm"   UseSubmitBehavior="false" AlternateText="Confirm"    onClick="btnConfirm_Click"   ImageURL="../../images/butt_confirm.gif"   CausesValidation=False runat="server" />
					<asp:ImageButton id="btnCancel"	   UseSubmitBehavior="false" AlternateText="Cancel"     onClick="btnCancel_Click"    ImageURL="../../images/butt_Cancel.gif"    CausesValidation=False runat="server" />
					<asp:ImageButton id="btnPrint"     UseSubmitBehavior="false" AlternateText="Print"      onClick="btnPreview_Click"   ImageURL="../../images/butt_print.gif"     CausesValidation=False runat="server" />
					<asp:ImageButton id="btnDelete"    UseSubmitBehavior="false" AlternateText="Delete"     onClick="btnDelete_Click"    ImageURL="../../images/butt_delete.gif"    CausesValidation=False runat="server" />
					<asp:ImageButton id="btnDebitNote" UseSubmitBehavior="false" AlternateText="Debit Note" onClick="btnDebitNote_Click" ImageURL="../../images/butt_debitnote.gif" CausesValidation=False runat="server" />
					<asp:ImageButton id="btnBack"      UseSubmitBehavior="false" AlternateText="Back"       onClick="btnBack_Click"      ImageURL="../../images/butt_back.gif"      CausesValidation=False runat="server" />
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
		    
			<input type=hidden id=hidPQID runat=server />
			<asp:label id="SortExpression" Visible="False" Runat="server"></asp:label>
			<asp:label id="AccountCode" Visible="False" Text= "Account Code" Runat="server"></asp:label>
			<asp:label id="BillParty" Visible="False" Text= "Bill Party" Runat="server"></asp:label>
			<asp:label id="EmployeeCode" Visible="False" Text= "Employee Code" Runat="server"></asp:label>
			<asp:label id="issueType" Visible="false" Runat="server"></asp:label>
			<asp:Label id=lblErrMesage visible=false Text="Error while initiating component." runat=server />			
			<asp:label id="lblStatusHid" Visible="False" Runat="server"></asp:label>
			<asp:Label id="lblBlockHidden" visible=false runat=server />
			<asp:Label id="lblBatchHidden" visible=false runat=server />
		</table>
			<Input type=hidden id=hidBlockCharge value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>
		</form>
	</body>
</html>
