<%@ Page Language="vb" trace="False" codefile="../../../include/in_trx_StockAdj_Details.aspx.vb" Inherits="IN_StockAdjust_Det" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuINTrx" src="../../menu/menu_INtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="qsf" Namespace="Telerik.QuickStart" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<html>
	<head>
		<title>Stock Adjustment Details</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
         <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	    <style type="text/css">
            .style1
            {
                width: 24%;
            }
            .style2
            {
                height: 32px;
                width: 24%;
            }
            .style3
            {
                width: 24%;
                height: 19px;
            }
            .style4
            {
                height: 19px;
            }
        </style>
	</head>
	<script language="javascript">
		function clearText(oper) {
			var doc = document.frmMain;
			if (oper == 'Amt')
			{	doc.txtQty.value = '';
				doc.txtCost.value = ''; 
			}
			else if (oper == 'Qty')
				doc.txtAmt.value = '';
		}
	</script>
	<body>
		<form id=frmMain class="main-modul-bg-app-list-pu" runat=server>

           		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>

             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">  

			<asp:label id=lblCode visible=false text=" Code" runat=server />
			<asp:label id=lblSelect visible=false text="Select " runat=server />
			<asp:label id=lblPleaseSelect visible=false text="Please select " runat=server />
			<table border=0 width="100%" cellspacing=0 cellpading=1 class="font9Tahoma" runat=server>
				<tr>
					<td colspan=6><UserControl:MenuINTrx id=menuIN EnableViewState=False runat="server" /></td>
				</tr>
				<tr>
					<td class="font9Tahoma" colspan=6> <strong> STOCK ADJUSTMENT DETAILS</strong></td>
				</tr>
				<tr>
					<td colspan=6><hr style="width :100%" />   
                            </td>
				</tr>
				<tr>
					<td width="20%" height=25>Stock Adjustment ID :</td>
					<td width="40%"><asp:Label id=lblStockAdjId runat="server"/></td>
					<td width="5%">&nbsp;</td>
					<td width="15%">Period :</td>
					<td width="20%"><asp:Label id=lblAccPeriod runat=server /></td>
					<td width="5%">&nbsp;</td>
				</tr>
				<tr>
				<td height=25>Adjustment Date :*</td>
				<td><asp:TextBox id=txtDate Width=50% maxlength=10 class="font9Tahoma" runat=server />
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
				<td><asp:Label id=lblStatus runat=server /><asp:Label id=lblHidStatus Visible=False runat=server /></td>
				<td>&nbsp;</td>
			</tr>
				<tr>
					<td height=25>Adjustment Type :*</td>
					<td><asp:DropDownList id="ddlAdjType" class="font9Tahoma" Width="100%" AutoPostBack=True OnSelectedIndexChanged="ddlAdjType_OnSelectedIndexChanged" runat=server /></td>
					<td>&nbsp;</td>
					<td>Date Created :</td>
					<td><asp:Label id=lblCreateDate runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Transaction Type :*</td>
					<td><asp:DropDownList id="ddlTransType" Width="100%" class="font9Tahoma" AutoPostBack=True OnSelectedIndexChanged="ddlTransType_OnSelectedIndexChanged" runat=server /></td>
					<td>&nbsp;</td>
					<td>Last Update :</td>
					<td><asp:Label id=lblUpdateDate runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Remarks :</td>
					<td><asp:textbox id=txtRemark width="100%" MaxLength=512 class="font9Tahoma" Runat="server" /></td>
					<td>&nbsp;</td>
					<td>Updated By :</td>
					<td><asp:Label id=lblUpdateID  runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Adjust Into :*</td>
				    <td><asp:DropDownList id="ddlInventoryBin" class="font9Tahoma" Width=100% runat=server/>
				        <asp:Label id=lblInventoryBin text="Please Select Inventory Bin" forecolor=red visible=false runat=server /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
			<tr>
				<td height=25 class="style1">Warehouse Into :*</td>
				<td><asp:DropDownList id="lstStorage"  CssClass="fontObject" Width="100%" runat=server/>
				    <asp:Label id=lblstoragemsg text="Please Select Storage" forecolor=red visible=false runat=server /></td>
            </tr>
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=6>
						<table id="tblAdd" border=0 width="100%" cellspacing="0" cellpadding="4" class="font9Tahoma" runat="server">
							<tr id="trItemCode" class="mb-c">
								<td class="style3">Item Code :*</td>
								<td width="100%" colspan=4 class="style4">
									<telerik:RadComboBox   CssClass="fontObject" ID="RadComboBoxProduct"  AutoPostBack="true"
								  	HighlightTemplatedItems="true"  OnSelectedIndexChanged=GetItemBtn_Click
									Runat="server" AllowCustomText="True" 
									EmptyMessage="Please Select " Height="150" Width="100%" 
									 Filter="Contains" Sort="Ascending" 
									EnableVirtualScrolling="True">
									<CollapseAnimation Type="InQuart" />
								</telerik:RadComboBox>	<BR>									                                   
                            				<asp:label id=lblItemCodeErr Text="Please select an Item code." Visible=False forecolor=red Runat="server" />
                                    </td>
							</tr>
							<tr id="trAccCode" class="font9Tahoma" >
								<td class="style1"><asp:label id="lblAccCodeTag" Runat="server" /> :*</td>
								<td width="80%" colspan=4>
                                    <asp:TextBox ID="txtAccCode" class="font9Tahoma" runat="server" AutoPostBack="True" MaxLength="15" Width="25%"></asp:TextBox>
                                    <input id="Button2" class="button-small"  runat="server" causesvalidation="False" onclick="javascript:PopCOA_Desc('frmMain', '', 'txtAccCode', 'txtAccName', 'False');"
                                        type="button" value=" ... " />&nbsp;
                                    <asp:Button ID="BtnGetCOA" class="button-small"   runat="server" Font-Bold="False"
                                                OnClick="BtnGetCOA_Click" Text="Please Click Here" ToolTip="Click For Refresh COA " Width="128px" />
                                    <asp:TextBox ID="txtAccName" class="font9Tahoma" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Bold="True" MaxLength="10" Width="49%"></asp:TextBox>
												<br><asp:label id=lblAccCodeErr Visible=False forecolor=red Runat="server" /></td>
							</tr>
							<tr id="trChargeLevel" class="mb-c" >
								<td height=25 class="style1">Charge To :*</td>
								<td width="80%" colspan=4><asp:DropDownList id="ddlChargeLevel" Width=100% AutoPostBack=True OnSelectedIndexChanged=ddlChargeLevel_OnSelectedIndexChanged runat=server /> </td>
							</tr>
							<tr id="trPreBlkCode" class="mb-c" >
								<td height=25 class="style1"><asp:label id=lblPreBlkCodeTag Runat="server" /> :</td>
								<td width="80%" colspan=4><asp:DropDownList id="ddlPreBlkCode" Width=100% runat=server />
									<asp:label id=lblPreBlkCodeErr Visible=False forecolor=red Runat="server" /></td>
							</tr>
							<tr id="trBlkCode" class="mb-c" >
								<td height=25 class="style1"><asp:label id=lblBlkCodeTag Runat="server" /> :</td>
								<td width="80%" colspan=4><asp:DropDownList id="ddlBlkCode" Width=100% runat=server />
									<asp:label id=lblBlkCodeErr Visible=False forecolor=red Runat="server" /></td>
							</tr>
							<tr id="trVehCode" class="mb-c" >
								<td height=25 class="style1"><asp:label id="lblVehCodeTag" Runat="server" /> :</td>
								<td width="80%" colspan=4><asp:DropDownList id="ddlVehCode" Width=100% runat=server />
									<asp:label id=lblVehCodeErr Visible=False forecolor=red Runat="server" /></td>
							</tr>
							<tr id="trVehExpCode" class="mb-c" >
								<td class="style2"><asp:label id="lblVehExpCodeTag" Runat="server" /> :</td>
								<td width="80%" colspan=4 style="height: 32px"><asp:DropDownList id="ddlVehExpCode" Width=100% runat=server />
									<asp:label id=lblVehExpCodeErr Visible=False forecolor=red Runat="server" /></td>
							</tr>
							<tr class="mb-c">
								<td height=25 class="style1">Document Ref :</td>
								<td width="30%"><asp:textbox id="txtAdjDocRef" width=100% maxlength=32 EnableViewState="False" Runat="server" /></td>
								<td width="50%" colspan=3>&nbsp;</td>
							</tr>
							<tr class="mb-c">
								<td height=25 class="style1"><asp:label id="lblQuantityTag" Text = "Quantity :*" Runat="server" />&nbsp;</td>
								<td width="30%" id="tdQty"><asp:TextBox id="txtQty" width="100%" maxlength=15 Runat="server" />
												<asp:RegularExpressionValidator id="revQty" 
													ControlToValidate="txtQty"
													ValidationExpression="^[-]?\d{1,9}\.\d{1,5}|^[-]?\d{1,9}"
													Display="Dynamic"
													text = "Maximum length 9 digits and 5 decimal points"
													runat="server"/>
												<asp:label id=lblQtyErr text="Please enter numeric value only!" Visible=False forecolor=red Runat="server" />
								</td>
								<td width="5%">&nbsp;</td>
								<td width="30%">Original Quantity : <asp:Label id=lblOriginalQtyDisplay visible=true runat="server" /><asp:Label id=lblOriginalQty visible=false runat="server" />
                                    <asp:Label id=lblOriginalQtyOnHand Visible=False runat="server" /><asp:Label id=lblOriginalQtyOnHold Visible=False runat="server" />
                                </td>
								<td width="15%">&nbsp;</td>
							</tr>
							<tr class="mb-c">
								<td height=25 class="style1"><asp:label id="lblAverageCostTag" Text = "Average Cost :*" Runat="server" />&nbsp;</td>
								<td width="30%" id="tdAverageCost"><asp:TextBox id="txtAverageCost" width="100%" maxlength=22 Runat="server" />
												<asp:RegularExpressionValidator id="revAverageCost" 
													ControlToValidate="txtAverageCost"
													ValidationExpression="\d{1,9}\.\d{1,2}|\d{1,9}"
													Display="Dynamic"
													text = "Maximum length 19 digits and 2 decimal points"
													runat="server"/>
												<asp:label id=lblAverageCostErr text="Please enter numeric value only!" Visible=False forecolor=red Runat="server" />
												<asp:label id=lblActionDesc1 Visible=True Runat="server" />
								</td>
								<td width="5%">&nbsp;</td>
								<td width="30%">Original Average Cost : <asp:Label id=lblOriginalAverageCostDisplay visible=true runat="server" />
																		<asp:Label id=lblOriginalAverageCost visible=false runat="server" /></td>
								<td width="15%">&nbsp;</td>
							</tr>
							<tr class="mb-c">
								<td height=25 class="style1">&nbsp;</td>
								<td width="30%" id="tdDiffAverageCost"><asp:label id=lblActionDesc2 Visible=True Runat="server" />&nbsp;</td>
								<td width="5%">&nbsp;</td>
								<td width="30%">Original Difference Average Cost : <asp:Label id=lblOriginalDiffAverageCostDisplay runat="server" />
																				   <asp:Label id=lblOriginalDiffAverageCost visible=false runat="server" /></td>
								<td width="15%">&nbsp;</td>
							</tr>
							<tr class="mb-c">
								<td height=25 class="style1"><asp:label id="lblTotalCostTag" Text = "Total Cost :*" Runat="server" />&nbsp;</td>
								<td width="30%" id="tdTotalCost"><asp:textbox id="txtTotalCost" width="100%" maxlength=22 Runat="server" />
												<asp:RegularExpressionValidator id="revTotalCost" 
													ControlToValidate="txtTotalCost"
													ValidationExpression="^[-]?\d{1,9}\.\d{1,2}|^[-]?\d{1,9}"
													Display="Dynamic"
													text = "Maximum length 19 digits and 2 decimal points"
													runat="server"/>
												<asp:label id=lblTotalCostErr text="Please enter numeric value only!" Visible=False forecolor=red Runat="server" />
								</td>
								<td width="5%">&nbsp;</td>
								<td width="30%">Original Total Cost : <asp:Label id=lblOriginalTotalCostDisplay runat="server" />
																	 <asp:Label id=lblOriginalTotalCost visible=false runat="server" /></td>
								<td width="15%">&nbsp;</td>
							</tr>
							<tr class="mb-c">
								<td height=25 class="style1">&nbsp;</td>
								<td width="30%" id="tdActionDesc"><asp:label id=lblActionDesc3 Visible=True Runat="server" /></td>
								<td width="5%">&nbsp;</td>
								<td width="30%">&nbsp;</td>
								<td width="15%">&nbsp;</td>
							</tr>
							<tr class="mb-c">
								<td colspan=5><asp:ImageButton text="Add" id="ibAdd" ImageURL="../../images/butt_add.gif" OnClick="ibAdd_OnClick" UseSubmitBehavior="false" Runat="server" /></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>
				<tr id="trDataGrid1">
					<td colspan=6> 
						<asp:DataGrid id="dgLines"
							OnItemDataBound="dgLines_OnItemDataBound"
							AutoGenerateColumns="false" width="100%" runat="server"
							GridLines = none
							Cellpadding = "2"
							Pagerstyle-Visible="False"
							OnDeleteCommand="dgLines_OnDeleteCommand"
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
							<Columns>
								<asp:TemplateColumn HeaderText="Item Code">
									<ItemStyle Width="15%"/>
									<ItemTemplate>
										<%# Container.DataItem("ItemCode") %>( <%# Container.DataItem("ItemDesc") %> )
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemStyle Width="9%"/>
									<ItemTemplate>
										<%# Container.DataItem("AccCode") %> 
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemStyle Width="9%"/>
									<ItemTemplate>
										<%# Container.DataItem("BlkCode") %> 
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemStyle Width="9%"/>
									<ItemTemplate>
										<%# Container.DataItem("VehCode") %> 
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemStyle Width="9%"/>
									<ItemTemplate>
										<%# Container.DataItem("VehExpCode") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Document Ref">
									<ItemStyle Width="9%"/>
									<ItemTemplate>
										<%# Container.DataItem("AdjRefNo") %> 
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Original Quantity">
									<HeaderStyle HorizontalAlign="Right" />
									<ItemStyle Width="9%" HorizontalAlign="Right" />
									<ItemTemplate>
										<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("Quantity"), 5) %> id="lblQuantity" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Original Average Cost">
									<HeaderStyle HorizontalAlign="Right" />
									<ItemStyle Width="9%" HorizontalAlign="Right" />
									<ItemTemplate>
										<asp:label text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("AverageCost"), 2), 2) %> id="lblAverageCost" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Original Total Cost">
									<HeaderStyle HorizontalAlign="Right" />
									<ItemStyle Width="9%" HorizontalAlign="Right" />
									<ItemTemplate>
										<asp:label text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("TotalCost"), 2), 2) %> id="lblTotalCost" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Entered Quantity">
									<HeaderStyle HorizontalAlign="Right" />
									<ItemStyle Width="9%" HorizontalAlign="Right" />
									<ItemTemplate>
										<asp:label text=<%# Container.DataItem("N_Quantity") %> id="lblN_Quantity" visible = "false" runat="server" />
										<asp:label text=<%# ObjGlobal.DisplayQuantityFormat(Container.DataItem("N_Quantity")) %> id="lblN_QuantityDisplay" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Entered Average Cost">
									<HeaderStyle HorizontalAlign="Right" />
									<ItemStyle Width="9%" HorizontalAlign="Right" />
									<ItemTemplate>
										<asp:label text=<%# Container.DataItem("N_AverageCost") %> id="lblN_AverageCost" visible = "false" runat="server" />
										<asp:label text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("N_AverageCost"), 2), 2) %> id="lblN_AverageCostDisplay" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Entered Total Cost">
									<HeaderStyle HorizontalAlign="Right" />
									<ItemStyle Width="9%" HorizontalAlign="Right" />
									<ItemTemplate>
										<asp:label text=<%# Container.DataItem("N_TotalCost") %> id="lblN_TotalCost" visible = "false" runat="server" />
										<asp:label text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("N_TotalCost"), 2), 2) %> id="lblN_TotalCostDisplay" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Entered Quantity">
									<HeaderStyle HorizontalAlign="Right" />
									<ItemStyle Width="9%" HorizontalAlign="Right" />
									<ItemTemplate>
										<asp:label text=<%# Container.DataItem("D_Quantity") %> id="lblD_Quantity" visible = "false" runat="server" />
										<asp:label text=<%# ObjGlobal.DisplayQuantityFormat(Container.DataItem("D_Quantity")) %> id="lblD_QuantityDisplay" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Entered Average Cost">
									<HeaderStyle HorizontalAlign="Right" />
									<ItemStyle Width="9%" HorizontalAlign="Right" />
									<ItemTemplate>
										<asp:label text=<%# Container.DataItem("D_AverageCost") %> id="lblD_AverageCost" visible = "false" runat="server" />
										<asp:label text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("D_AverageCost"), 2), 2) %> id="lblD_AverageCostDisplay" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Entered Total Cost">
									<HeaderStyle HorizontalAlign="Right" />
									<ItemStyle Width="9%" HorizontalAlign="Right" />
									<ItemTemplate>
										<asp:label text=<%# Container.DataItem("D_TotalCost") %> id="lblD_TotalCost" visible = "false" runat="server" />
										<asp:label text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("D_TotalCost"), 2), 2) %> id="lblD_TotalCostDisplay" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemStyle HorizontalAlign="right" Width="5%"/>
									<ItemTemplate>
										<asp:label text=<%# Trim(Container.DataItem("StockAdjLnID")) %> id="lblStockAdjLNID" visible=false runat="server" />
										<asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" CausesValidation=False runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:DataGrid>
					</td>
				</tr>
				<tr id="trDataGrid2">
					<td colspan=3>&nbsp;</td>
					<td colspan=3 height=25><hr style="width :100%" />   
                            </td>
				</tr>
				<tr id="trDataGrid3">
					<td colspan=3>&nbsp;</td>
					<td colspan=3>
						<table border=0 width="100%" class="font9Tahoma" runat=server>
							<tr>
								<td height="25" align="left">Total : </td>
								<td width="25%" align="right"><asp:label id="lblTotal1" runat="server" /></td>
								<td width="25%" align="right"><asp:label id="lblTotal2" runat="server" /></td>
								<td id="tdDummy" width="12%">&nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan=6><asp:label id=lblActionResult Visible=False forecolor=red Runat="server" />&nbsp;</td>
				</tr>
				<tr>
					<td align="left" colspan="6">
					    <asp:ImageButton id="ibNew"      UseSubmitBehavior="false" AlternateText="New"     onClick="ibNew_OnClick"     ImageURL="../../images/butt_new.gif"     CausesValidation=False runat="server" />
						<asp:ImageButton id="ibSave"     UseSubmitBehavior="false" AlternateText="Save"    onClick="ibSave_OnClick"    ImageURL="../../images/butt_save.gif"    CausesValidation=False runat="server" />
						<asp:ImageButton id="ibConfirm"  UseSubmitBehavior="false" AlternateText="Confirm" onClick="ibConfirm_OnClick" ImageURL="../../images/butt_confirm.gif" CausesValidation=False runat="server" />
						<asp:ImageButton id="ibCancel"   UseSubmitBehavior="false" AlternateText="Cancel"  onClick="ibCancel_OnClick"  ImageURL="../../images/butt_cancel.gif"  CausesValidation=False runat="server" />
						<asp:ImageButton id="ibDelete"   UseSubmitBehavior="false" AlternateText="Delete"  onClick="ibDelete_OnClick"  ImageURL="../../images/butt_delete.gif"  CausesValidation=False runat="server" />
						<asp:ImageButton id="ibBack"     UseSubmitBehavior="false" AlternateText="Back"    onClick="ibBack_OnClick"    ImageURL="../../images/butt_back.gif"    CausesValidation=False runat="server" />
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
							AllowSorting="false" class="font9Tahoma">	
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
					<td colspan=5></td>
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
			<Input type=hidden id=Real_DDL_Search value=",ddlItemCode," runat=server/>
                                    <asp:TextBox ID="txtcost" runat="server" AutoPostBack="True" MaxLength="15" Width="5%" BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>
        </div>
        </td>
        </tr>
        </table>
		 <asp:ScriptManager ID="ScriptManager1" runat="server">
		</asp:ScriptManager>				
		</form>
	</body>
</html>
