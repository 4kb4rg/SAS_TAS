<%@ Page Language="vb" trace=false codefile="../../../include/PU_trx_GRDet.aspx.vb" Inherits="PU_GRDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPU" src="../../menu/menu_putrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="qsf" Namespace="Telerik.QuickStart" %>
 
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<script runat="server">

</script>

<html>
	<head>
		<title>Goods Receive Details</title>		
		<Preference:PrefHdl id=PrefHdl runat="server" />
         <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	    <style type="text/css">
            .style1
            {
                width: 396px;
            }
            .style3
            {
                height: 49px;
                width: 190px;
            }
            .style4
            {
                width: 20%;
            }
            .style5
            {
                height: 26px;
                width: 298px;
            }
            .style6
            {
                width: 298px;
            }
            .style7
            {
                width: 332px;
            }
            .style8
            {
                width: 100%;
            }
            </style>
	</head>
	<script language="javascript">			
			function calUnitCost() {
				var doc = document.frmMain;
				var a = parseFloat(doc.txtReceiveQty.value);
				var b = parseFloat(doc.txtTtlCost.value);				
				doc.txtCost.value = b / a;
				if (doc.txtCost.value == 'NaN')
					doc.txtCost.value = '';
				else
					doc.txtCost.value = round(doc.txtCost.value, 2);
			}			
			function calTtlCost() {
				var doc = document.frmMain;
				var a = parseFloat(doc.txtReceiveQty.value);
				var b = parseFloat(doc.txtCost.value);
				doc.txtTtlCost.value = (a * b);
				if (doc.txtTtlCost.value == 'NaN')
					doc.txtTtlCost.value = '';
				else
					doc.txtTtlCost.value = round(doc.txtTtlCost.value, 2);
			}
	</script>
	<body>
		<form id=frmMain class="main-modul-bg-app-list-pu" runat=server >
			<asp:Label id=lblErrMessage visible=False Text="Error while initiating component." runat=server />	
			<asp:label id=lblCode visible=false text=" Code" runat=server />	
			<asp:label id=lblPleaseSelect visible=false text="Please select " runat=server />
			<asp:label id=lblPleaseSelectOne visible=false text="Please select one " runat=server />	
			<asp:label id=lblLocCode visible=false runat=server />
			<asp:label id=lblErrOnHand visible=false text="Insufficient quantity on hand" runat=server />		
			<asp:label id=lblErrOnHold visible=false text="Insufficient quantity on hold" runat=server />		
			<asp:Label id=lblVehicleOption visible=false text=false runat=server/>
			<asp:Label id=lblHidStatus visible=false runat=server />
			<input type=hidden id=GoodsRcvId runat=server />
			<input type=hidden id=hidItemType runat=server />
        <table cellpadding="0" cellspacing="0" style="width: 100%">
		<tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">  

			    <table border="0" cellspacing="0" cellpadding="1" width="100%" class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuPU id=menuPU runat="server" /></td>
				</tr>			
				<tr>
					<td  colspan="6"><table cellpadding="0" 
                            cellspacing="0" class="style8">
                        <tr>
                            <td class="font9Tahoma">
                             <strong>GOODS RECEIVE DETAIL</strong></td>
                            <td class="font9Header" style="text-align: right">
                                Period :<asp:Label id=lblAccPeriod runat=server />&nbsp;| Status :&nbsp;<asp:Label id=lblStatus runat=server />&nbsp;| Date Created :<asp:Label id=lblCreateDate runat=server />| Last Update :|<asp:Label id=lblUpdateDate runat=server />&nbsp;|Updated By :<asp:Label id=lblUpdateBy runat=server />&nbsp;</td>
                        </tr>
                        </table>
                        <hr style="width :100%" /></td>
                    </td>
				</tr>
				<tr>
					<td height="25" class="style1">Goods Receive ID :</td>
					<td width="45%"><asp:Label id=lblGoodsRcvId runat=server /></td>
					<td style="width: 147px">&nbsp;</td>
					<td style="width: 267px">&nbsp;</td>
					<td width="15%">&nbsp;</td>
					<td width="5%">&nbsp;</td>
				</tr>
				<tr>
					<td height="25" class="style1" >Goods Receive Ref. No :</td>
					<td><asp:TextBox id=txtGoodsRcvRefNo width="75%" maxlength=32 CssClass="fontObject" runat=server  />
					<asp:Label id=lblErrGRRefNo visible=False text="Ref. No is not unique" forecolor=red runat=server/></td>
					<td style="width: 147px">&nbsp;</td>
					<td style="width: 267px">&nbsp;</td>
					<td>&nbsp;</td>
					<td width="5%">&nbsp;</td>
				</tr>
				<tr>
					<td class="style1" >Goods Receive Ref. Date :*</td>
					<td style="height: 39px"><asp:TextBox id=txtGoodsRcvRefDate width=30% maxlength=10 CssClass="fontObject" runat=server />
						<a href="javascript:PopCal('txtGoodsRcvRefDate');"><asp:Image id="btnSelDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>						
						<asp:label id=lblDate Text ="Date Entered should be in the format " forecolor=Red Visible = False Runat="server"/> 
				        <asp:label id=lblFmt  forecolor=red Visible = false Runat="server"/> 
						<asp:RequiredFieldValidator 
							id="rfvGoodsRcvRefDate" 
							runat="server" 
							ErrorMessage="Please key in Goods Receive Ref. Date" 
							ControlToValidate="txtGoodsRcvRefDate" 
							display="dynamic"/></td>
					<td style="width: 147px; height: 39px;">&nbsp;</td>
					<td style="width: 267px; height: 39px;">&nbsp;</td>
					<td style="height: 39px">&nbsp;</td>
					<td width="5%" style="height: 39px"></td>
				</tr>			
				<tr>
					<td height="25" class="style1">Supplier Code :*</td>
					<td>
						<telerik:RadComboBox   CssClass="fontObject" ID="radSupplier"  autopostback=true
							OnSelectedIndexChanged="GetSupplierBtn_Click"
							Runat="server" AllowCustomText="True" 
							EmptyMessage="Plese Select Supplier " Height="200" Width="75%" 
							ExpandDelay="50" Filter="Contains" Sort="Ascending" 
							EnableVirtualScrolling="True">
							<CollapseAnimation Type="InQuart" />
						</telerik:RadComboBox>						
						<asp:Label id=lblErrBillParty visible=false forecolor=red runat=server/> </td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>							
				<tr>
					<td height="25" class="style1">Purchase Order ID :*</td>
					<td><asp:DropDownList id=ddlPOId width="76%" runat=server AutoPostBack="True" 
                            onSelectedIndexChanged="POIndexChanged" CssClass="fontObject"/>&nbsp;
					    <asp:ImageButton ImageAlign=AbsBottom ID=btnAddAllItem 
                            UseSubmitBehavior="false" onclick=BtnAddAllItem_Click CausesValidation=False 
                            ImageUrl="../../../images/btnext.png" AlternateText="Add all items" 
                            Runat=server style="visibility:hidden;"/>  		
					</td>
					<td style="width: 147px">&nbsp;</td>
					<td style="width: 267px">&nbsp;</td>
					<td>&nbsp;</td>				
					<td width="5%">&nbsp;</td>					
				</tr>
				<tr>
				    <td height=25 class="style1">Goods Receive Into :*</td>
				    <td><asp:DropDownList id="ddlInventoryBin" CssClass="fontObject" Width="76%" runat=server/>
				        <asp:Label id=lblInventoryBin text="Please Select Inventory Bin" forecolor=red visible=false runat=server /></td>
			        <tr>
				        <td height=25 class="style1">Warehouse Into :*</td>
				        <td><asp:DropDownList id="lstStorage"  CssClass="fontObject" Width="76%" runat=server/>
				            <asp:Label id=lblstoragemsg text="Please Select Storage" forecolor=red visible=false runat=server /></td>
                    </tr>
    				<td style="width: 147px">&nbsp;</td>
				    <td style="width: 267px">&nbsp;</td>
				    <td>&nbsp;</td>
				    <td>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=6><asp:Label id=lblPoLocCode forecolor=Red runat=server Visible="False"/></td>				
				</tr>

                </table>
                <table width="100%" class="font9Tahoma" cellspacing="0" cellpadding="1" border="0" align="center" runat=server >										
				<tr>
					<td colspan="6">
						<table id=tblLine class="sub-Add" border="0" width="100%" cellspacing="0" cellpadding="0" runat=server>
							<tr>						
								<td>
									<table id="tblDoc" class="font9Tahoma" border="0" width="100%" cellspacing="0" cellpadding="0" runat=server>
										<tr>
											<td height="25" style="width: 20%">Item Code :*</td>
											<td width=80%><asp:DropDownList id=ddlItemCode width="95%" runat=server AutoPostBack="True" onSelectedIndexChanged="ItemIndexChanged" CssClass="fontObject"/><BR>
								    						<asp:label id=lblErrItemCode forecolor="red" visible=false text="Please Select Item Code" runat=server /></td>
										</tr>
										<tr visible=false>
											<td height="25" style="width: 20%"><asp:Label id=lblSelectedItemCode width=100% visible=false runat=server /></td>
											<td><asp:Label id=lblItemLocCode visible=false runat=server /></td>									    
										</tr>
										
									</table>
									<table id="tblFACode" border="0" width="100%" cellspacing="0" cellpadding="4" runat=server>
										<tr>
											<td class="style3"><asp:Label id=lblFA runat=server /></td>
											<td width=80% style="height: 49px"><asp:DropDownList id=ddlFACode width="95%" autopostback=true onSelectedIndexChanged="FACodeIndexChanged" CssClass="fontObject" runat=server/>
														  <asp:Label id=lblErrFACode forecolor="red" visible=false runat=server/></td>
										</tr>
									</table>
									<table id="tblDoc1" class="font9Tahoma"  border="0" width="100%" cellspacing="0" cellpadding="4" runat=server>

										<tr>
											<td height="25" class="style4">Quantity Ordered :</td>
											<td width=80%><asp:Label id=lblQtyOrder runat=server /></td>
										</tr>
										<tr>
											<td height="25" class="style4">Purchase UOM :</td>
											<td width=80%><asp:Label id=lblPurchaseUOM runat=server /></td>
										</tr>
										<tr>
											<td height="25" class="style4">Stock UOM :</td>
											<td><asp:Label id=lblUOMCode runat=server /></td>
										</tr>
										<tr>
											<td height="25" class="style4">Quantity Outstanding :</td>
											<td><asp:Label id=lblIDQtyOutStanding runat=server /><asp:Label id=lblQtyOutStanding visible=false runat=server /></td>
										</tr>
										<tr>
											<td height="25" class="style4">Quantity Received :*</td>
											<td width=80%><asp:TextBox id=txtReceiveQty width="17%" maxlength=15 CssClass="fontObject" runat=server />
												<asp:RegularExpressionValidator id="revReceiveQty" 
													ControlToValidate="txtReceiveQty"
													ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"
													Display="Dynamic"
													text = "Maximum length 9 digits and 5 decimal points"
													runat="server"/>
												<asp:RequiredFieldValidator 
													id="rfvReceiveQty" 
													runat="server" 
													ErrorMessage="Please Specify Quantity To Receive" 
													ControlToValidate="txtReceiveQty" 
													display="dynamic"/>
												<asp:Label id=lblReceiveQty text="Number generated is too big!" Visible=False forecolor=red Runat="server" />
												<asp:Label id=lblErrReceiveQty forecolor="red" text="Quantity Receive cannot be greater than Quantity Outstanding." runat="server" />
												<asp:Label id=lblErrReceiveQtyZero forecolor="red" visible=false text="<br>Quantity Received must be greater than zero" runat="server" />
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td>
									<table id="tblAcc" class="mb-c" border="0" width="100%" cellspacing="0" cellpadding="4" runat=server>
									    <tr>
											<td height="25" class="style7">Unit Cost :*</td>
											<td><asp:TextBox id=txtCost width="17%" maxlength=21 OnKeyUp="javascript:calTtlCost();" CssClass="fontObject" runat=server />
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
										<tr>
											<td height="25" class="style7">Total Cost :*</td>
											<td><asp:TextBox id=txtTtlCost width="17%" maxlength=18 OnKeyUp="javascript:calUnitCost();" CssClass="fontObject" runat=server />
												<asp:RegularExpressionValidator id="RegularExpressionValidatorTtlCost" 
													ControlToValidate="txtTtlCost"
													ValidationExpression="\d{1,15}\.\d{0,2}|\d{1,15}"
													Display="Dynamic"
													text = "Maximum length 18 digits."
													runat="server"/>
												<asp:RequiredFieldValidator 
													id="validateTtlCost" 
													runat="server" 
													ErrorMessage="Please Specify Total Cost" 
													ControlToValidate="txtTtlCost" 
													display="dynamic"/>
												<asp:label id=lblErrTtlCost text="Number generated is too big!" Visible=False forecolor=red Runat="server" />
												&nbsp;&nbsp;&nbsp;
											</td>
										</tr>
										<tr>
											<td height=25 class="style7"><asp:label id="lblAccount" runat="server" /> (DR) :* </td>
											<td width=80%>										
                                                <telerik:RadComboBox CssClass="fontObject" ID="RadAccCode" AutoPostBack="true"
                                                    OnSelectedIndexChanged="onSelect_Account"
                                                    runat="server" AllowCustomText="True"
                                                    EmptyMessage="Plese Select Account " Height="200" Width="95%"
                                                    ExpandDelay="50" Filter="Contains" Sort="Ascending"
                                                    EnableVirtualScrolling="True">
                                                    <CollapseAnimation Type="InQuart" />
                                                </telerik:RadComboBox>
												&nbsp;<asp:Label ID="lblErrAccount" Visible="false" ForeColor="red" runat="server" />
											</td>
										</tr>
										<tr id="RowChargeLevel" class="mb-c">
											<td height="25" class="style7">Charge Level :* </td>
											<td><asp:DropDownList id="ddlChargeLevel" Width="95%" AutoPostBack=True OnSelectedIndexChanged=ddlChargeLevel_OnSelectedIndexChanged CssClass="fontObject" runat=server /> </td>
										</tr>
										<tr id="RowPreBlk" class="mb-c">
											<td height="25" class="style7"><asp:label id=lblPreBlkTag Runat="server"/> </td>
											<td><asp:DropDownList id="ddlPreBlock" Width="95%" CssClass="fontObject" runat=server />
												<asp:label id=lblPreBlockErr Visible=False forecolor=red Runat="server" /></td>
										</tr>			
										<tr id="RowBlk" class="mb-c">
											<td height=25 class="style7"><asp:label id="lblBlock" runat="server" /> :</td>
											<td><asp:DropDownList id=ddlBlock width="95%" CssClass="fontObject" runat=server/>
												<asp:Label id=lblErrBlock visible=false forecolor=red runat=server/></td>
										</tr>
										<tr>
											<td height=25 class="style7"><asp:label id="lblVehicle" runat="server" /> :</td>
											<td><asp:Dropdownlist id=ddlVehCode width="95%" CssClass="fontObject" runat=server/>
												<asp:Label id=lblErrVehicle visible=false forecolor=red runat=server/></td>
										</tr>
										<tr>
											<td height=25 class="style7"><asp:label id="lblVehExpense" runat="server" /> :</td>
											<td><asp:Dropdownlist id=ddlVehExpCode width="95%" CssClass="fontObject" runat=server/>
												<asp:Label id=lblErrVehExp visible=false forecolor=red runat=server/></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td colspan="2" height="25"><asp:Imagebutton id="btnAdd" OnClick="btnAdd_Click" ImageURL="../../images/butt_add.gif" AlternateText=Add UseSubmitBehavior="false" Runat="server" /> &nbsp;  
								    			  <asp:ImageButton  id="SaveDtlBtn" visible=false ImageURL="../../images/butt_save.gif" OnClick="btnAdd_Click" Runat="server" />
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
						<asp:DataGrid id=dgGRDet
							AutoGenerateColumns="False" width="100%" runat="server"
							GridLines=None
							Cellpadding="2"
							Pagerstyle-Visible="False"
							OnDeleteCommand="DEDR_Delete"
							OnEditCommand="DEDR_Edit"
                            OnItemDataBound="dgLine_BindGrid"
							AllowSorting="True"  >	
							<HeaderStyle CssClass="mr-h"/>
							<ItemStyle CssClass="mr-l"/>
							<AlternatingItemStyle/>
 
							<Columns>
								<asp:BoundColumn Visible=False DataField="GoodsRcvLnId" />
								<asp:BoundColumn Visible=False DataField="POLnID" />
								<asp:BoundColumn Visible=False DataField="ItemCode" />
								<asp:BoundColumn Visible=False DataField="ReceiveQty" />
								<asp:BoundColumn Visible=False DataField="StockQty" />
								<asp:TemplateColumn Visible=False >
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("LocCode") %> id="lblPRLocCode" runat="server" />
										<asp:Label Text=<%# Container.DataItem("GoodsRcvLnId") %> id="lblGRLnId" visible=false runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Item &lt;br&gt;Add. Note">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("Description") %> id="lblItemCode" runat="server" /><br>
										<asp:Label Text=<%# Container.DataItem("AdditionalNote") %> id="lblAddNote" runat="server" /><br>
										<asp:Label Text=<%# Container.DataItem("ItemCode") %> id="lblItem" visible=false runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("AccCode") %> id="lblAccCode" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("BlkCode") %> id="lblBlkCode" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("VehCode") %> id="lblVehCode" runat="server" /> <br>
										<asp:Label Text=<%# Container.DataItem("VehExpenseCode") %> id="lblVehExpCode" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Unit Cost" Visible="False">
									<ItemTemplate>
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("AmountCurrency"), 2), 2) %> id="lblAmountCurrency" runat="server" />
										<asp:Label Text=<%# Container.DataItem("AmountCurrency") %> id="lblCost" Visible=false runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>		
								<asp:TemplateColumn HeaderText="Quantity Received">
									<HeaderStyle HorizontalAlign="Right" /> 
									<ItemStyle HorizontalAlign="Right" /> 
									<ItemTemplate>
										
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("ReceiveQty"), 5), 5) %> id="lblReceiveQty" runat="server" />							
									</ItemTemplate>
								</asp:TemplateColumn>							
								<asp:TemplateColumn HeaderText="Receive UOM">
									<ItemTemplate> 
										<asp:Label Text=<%# Container.DataItem("ReceiveUOM") %> id="lblReceiveUOM" runat="server" />
										<asp:Label Text=<%# Container.DataItem("QtyOutstanding") %> id="hidQtyOutstanding" Visible=false runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>							
								<asp:TemplateColumn HeaderText="Stock Quantity">
									<HeaderStyle HorizontalAlign="Right" /> 
									<ItemStyle HorizontalAlign="Right" /> 
									<ItemTemplate>
										
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("StockQty"), 5), 5) %> id="lblStockQty" runat="server" />							
									</ItemTemplate>
								</asp:TemplateColumn>						
								<asp:TemplateColumn HeaderText="Stock UOM">
									<ItemTemplate> 
										<asp:Label Text=<%# Container.DataItem("StockUOM") %> id="lblStockUOM" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>							
															
								<asp:TemplateColumn>		
									<ItemStyle HorizontalAlign="Right" /> 									
									<ItemTemplate>
										<asp:LinkButton id="lbDelete" CommandName="Delete" Text="Delete" CausesValidation=False runat="server"/>
										<asp:LinkButton id="Edit" CommandName="Edit" Text="Edit" CausesValidation =False runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>							
							</Columns>										
                            <PagerStyle Visible="False" />
						</asp:DataGrid>
					</td>	
				</tr>
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>	
				<tr>
					<td class="style5"><asp:Label id="Remarks" text="Remarks :" runat="server" /></td>	
					<td colspan="5" style="height: 26px"><asp:TextBox id="txtRemark" width="95%" maxlength="256" CssClass="fontObject" runat="server" /></td>
				</tr>
				<tr id=TransferVia Visible = false runat="server">
					<td class="style5">Transfer Via :</td>
					<td colspan="5" style="height: 26px"><asp:TextBox id=txtTransferVia width="95%" maxlength=256 CssClass="fontObject" runat=server /></td>
				</tr>
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>	
				<tr>
					<td align="left" colspan="6">
						<asp:Label id=lblErrConfirm visible=false forecolor=red text="Cannot confirm this Goods Receive because the Quantity exceed Quantity On Hand." runat=server />
						<P>
						<asp:ImageButton id="btnNew" UseSubmitBehavior="false" onClick="btnNew_Click" imageurl="../../images/butt_new.gif" AlternateText="New" runat=server/>
						<asp:ImageButton id="btnSave" UseSubmitBehavior="false" onClick="btnSave_Click" ImageUrl="../../images/butt_save.gif" AlternateText=Save CausesValidation=true runat="server" />
						<asp:ImageButton id="btnConfirm" UseSubmitBehavior="false" onClick="btnConfirm_Click" ImageUrl="../../images/butt_confirm.gif" AlternateText=Confirm CausesValidation=False runat="server" />
						<asp:ImageButton id="btnPrint" UseSubmitBehavior="false" onClick="btnPreview_Click" ImageUrl="../../images/butt_print.gif" AlternateText=Print CausesValidation=False runat="server" />
						<asp:ImageButton id="btnDelete" UseSubmitBehavior="false" onClick="btnDelete_Click" ImageUrl="../../images/butt_delete.gif" AlternateText=Delete CausesValidation=False runat="server" />
						<asp:ImageButton id="btnUndelete" UseSubmitBehavior="false" onClick="btnUnDelete_Click" ImageUrl="../../images/butt_undelete.gif" AlternateText=Undelete CausesValidation=False runat="server" />
						<asp:ImageButton id="btnCancel" UseSubmitBehavior="false" onClick="btnCancel_Click" ImageUrl="../../images/butt_cancel.gif" AlternateText=Cancel CausesValidation=False runat="server" />
						<asp:ImageButton id="btnBack" UseSubmitBehavior="false" onClick="btnBack_Click" ImageUrl="../../images/butt_back.gif" AlternateText=Back CausesValidation=False runat="server" />
						<asp:ImageButton id="btnDispatch" UseSubmitBehavior="false" onClick="btnDispatch_Click" ImageUrl="../../images/butt_gen_dispatch.gif" AlternateText=Dispatch CausesValidation=False runat="server" />
						<asp:ImageButton id="btnIssue" UseSubmitBehavior="false" onClick="btnIssue_Click" ImageUrl="../../images/butt_gen_issue.gif" AlternateText=Dispatch CausesValidation=False runat="server" />
					    <P>
						<asp:DataGrid id=dgDA
							AutoGenerateColumns="false" width="30%" runat="server"
							GridLines=none
							Cellpadding="1"
							Pagerstyle-Visible="False"
							AllowSorting="false" >	
							<HeaderStyle CssClass="mr-h"/>
							<ItemStyle CssClass="mr-l"/>
							<AlternatingItemStyle CssClass="mr-r"/>                             
							<Columns>
								<asp:HyperLinkColumn HeaderText="Dispatch Created" 
									SortExpression="DispAdvID" 
									DataNavigateUrlField="DispAdvID" 
									DataNavigateUrlFormatString="PU_trx_DADet.aspx?DispAdvID={0}"
									DataTextFormatString="{0:c}"
									DataTextField="DispAdvID" />	

							</Columns>
						</asp:DataGrid>
						<asp:DataGrid id=dgSI
							AutoGenerateColumns="false" width="30%" runat="server"
							GridLines=none
							Cellpadding="1"
							Pagerstyle-Visible="False"
							AllowSorting="false" >	
							<HeaderStyle CssClass="mr-h"/>
							<ItemStyle CssClass="mr-l"/>
							<AlternatingItemStyle CssClass="mr-r"/>
 	
							<Columns>
								<asp:HyperLinkColumn HeaderText="Issue Created" 
									SortExpression="StockIssueID" 
									DataNavigateUrlField="StockIssueID" 
									DataNavigateUrlFormatString="..\..\IN\trx\IN_trx_stockissue_details.aspx?id={0}"
									DataTextFormatString="{0:c}"
									DataTextField="StockIssueID" />	
							</Columns>
						</asp:DataGrid>
					    <P>
						    &nbsp;</td>
				</tr>	
				<tr id="tblDA" visible=false>
					<td colspan=6>
						&nbsp;</td>
				</tr>	
				<tr id="tblSI" visible=false>
					<td colspan=6 style="text-align: left">
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
							AllowSorting="false"  class="font9Tahoma">	
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
				    <td class="style6">&nbsp;</td>								
				    <td height=25 align=right><asp:Label id=lblTotalViewJournal Visible=false runat=server /> </td>
				    <td style="width: 147px">&nbsp;</td>	
				    <td align=right style="width: 267px"><asp:label id="lblTotalDB" text="0" Visible=false runat="server" /></td>						
				    <td>&nbsp;</td>		
				    <td align=right><asp:label id="lblTotalCR" text="0" Visible=false runat="server" /></td>				
			    </tr>
			</table>
			    <Input type=hidden id=hidBlockCharge value="" runat=server/>
			    <Input type=hidden id=hidChargeLocCode value="" runat=server/>
			    <asp:label id=lblHidStatusEdited value="" visible=false runat=server />
			    <asp:label id=lblTxLnID visible=false runat=server />
                <br />
                <asp:TextBox ID="txtPPN" runat="server" BackColor="Transparent" BorderStyle="None"
                    Width="9%" CssClass="font9Tahoma" ForeColor="Transparent"  ></asp:TextBox><asp:TextBox ID="txtCreditTerm" runat="server" ForeColor="Transparent"  BackColor="Transparent"
                        BorderStyle="None" Width="9%"></asp:TextBox><asp:TextBox ID="txtPPNInit" ForeColor="Transparent" runat="server"
                            BackColor="Transparent" BorderStyle="None" Width="9%"></asp:TextBox>
                            </div>
            </td>
            </tr>
            </table>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>			
		</form>
	</body>
</html>
