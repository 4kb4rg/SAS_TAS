<%@ Page Language="vb" codefile="../../../include/CM_Trx_ContractRegList.aspx.vb" Inherits="CM_Trx_ContractRegList" %>
<%@ Register TagPrefix="UserControl" Tagname="menu_CM_Trx" src="../../menu/menu_CMTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<%@ Register TagPrefix="qsf" Namespace="Telerik.QuickStart" %>
 
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<html>
	<head>
		<title>Contract Registration List</title>
             <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<form id=frmMain class="main-modul-bg-app-list-pu" runat="server">
    		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<table border="0" cellspacing="1" cellpadding=1 width="100%">
				<tr>
					<td colspan="6"><UserControl:menu_CM_Trx id=menu_CM_Trx runat="server" /></td>
				</tr>

			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
				<table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">

				<tr>
					<td class="font9Tahoma" colspan="3"> <strong> CONTRACT REGISTRATION LIST</strong></td>
					<td colspan="3" align=right><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6><hr style="width :100%" /></td>
				</tr>
				<tr>
					<td colspan=6 width=100% style="background-color:#FFCC00">
						<table width="100%" cellspacing="0" cellpadding="3" border="0" align="center" class="font9Tahoma">
							<tr class="mb-t">
								<td valign=top width="15%" height="26">Contract No :<BR>
									<asp:TextBox id=txtContractNo width=100% maxlength="30" CssClass="fontObject" runat="server" /><br>
									Contract Type:
									<asp:dropdownlist id=ddlContractType width=100% CssClass="fontObject" runat=server />
								</td>
								<td valign=top width="25%" height="26">
									<asp:label id=lblBillParty runat=server /> :<BR>
								 
									<telerik:RadComboBox   CssClass="fontObject" ID="ddlBuyer"    AutoPostBack="true"  
												 
                                                Runat="server" AllowCustomText="True" 
                                                EmptyMessage="Please Select Customer" Height="200" Width="100%" 
                                                ExpandDelay="50" Filter="Contains" Sort="Ascending" 
                                                EnableVirtualScrolling="True" auto> 
                                                <CollapseAnimation Type="InQuart" />
                                            </telerik:RadComboBox><br>
									Seller:
									<asp:dropdownlist id="ddlSeller" width=100% CssClass="fontObject"  runat=server />
								</td>
								<td valign=top width="15%" height="26">Product :<BR>
									<asp:DropDownList id="ddlProduct" width=100% CssClass="fontObject"  runat=server/>
									Price Basis Code:<BR>
									<asp:DropDownList id="ddlPriceBasis" width=100% runat=server/>
								</td>
								<td valign=top width=8%>Period :<BR>
								    <asp:DropDownList id="lstAccMonth" width=100%  CssClass="fontObject"  runat=server>
										<asp:ListItem value="0" Selected>All</asp:ListItem>
										<asp:ListItem value="1">1</asp:ListItem>
										<asp:ListItem value="2">2</asp:ListItem>										
										<asp:ListItem value="3">3</asp:ListItem>
										<asp:ListItem value="4">4</asp:ListItem>
										<asp:ListItem value="5">5</asp:ListItem>
										<asp:ListItem value="6">6</asp:ListItem>
										<asp:ListItem value="7">7</asp:ListItem>
										<asp:ListItem value="8">8</asp:ListItem>
										<asp:ListItem value="9">9</asp:ListItem>
										<asp:ListItem value="10">10</asp:ListItem>
										<asp:ListItem value="11">11</asp:ListItem>
										<asp:ListItem value="12">12</asp:ListItem>
									</asp:DropDownList>
								</td>
								<td valign=top width=10%><BR>
								    <asp:DropDownList id="lstAccYear" width=100% CssClass="fontObject"  runat=server>
									</asp:DropDownList>
								</td>
								<td valign=top width="10%" height="26">Status :<br>
									<asp:DropDownList id="ddlStatus" width=100% CssClass="fontObject"  runat=server/>
								</td>
								<td valign=top width="20%" height="26" align=right>
									<br><asp:TextBox id=srchCtrDateFrom width=50% maxlength="30" Visible=false runat="server" />
									<a href="javascript:PopCal('srchCtrDateFrom');">
										<asp:Image id="btnCtrDateFrom" runat="server" Visible=false ImageUrl="../../Images/calendar.gif"/>
									</a>
									<br><asp:TextBox id=srchCtrDateTo width=50% maxlength="30" Visible=false runat="server" />
									<a href="javascript:PopCal('srchCtrDateTo');">
										<asp:Image id="btnCtrDateTo" runat="server" Visible=false ImageUrl="../../Images/calendar.gif"/>
									</a>
								</td>
								<td valign=top width="15%" height="26" align=right>
									<asp:TextBox id=srchDelvPeriodFrom width=65% maxlength="30" Visible=false CssClass="fontObject"  runat="server" />
									<asp:TextBox id=srchDelvPeriodTo width=65% maxlength="30" Visible=false CssClass="fontObject"  runat="server" /><br>
								</td>
								<td width="5%" valign=bottom height="26" align=right>
									<asp:Button id=Searchbtn Text="Search" OnClick=srchBtn_Click CssClass="button-small" runat="server"/>
								</td>
							</tr>
							<tr>
								<td valign=top colspan=8>
									<asp:label id=lblErrCtrDate Text ="Date entered should be in the format " 
										forecolor=red visible=false runat="server"/>
									<asp:label id=lblDateFormat forecolor=red visible=false runat=server />
									<asp:label id=lblErrDelvPeriod Text ="Invalid format of Delivery Period. " 
										forecolor=red visible=false runat="server"/>  
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan=6>					
						<asp:DataGrid id=dgLine 
							runat=server
							AutoGenerateColumns=false 
							width=100% 
							GridLines=none 
							Cellpadding=2 
							AllowPaging=True 
							Allowcustompaging=False 
							Pagesize=15 
							OnPageIndexChanged=OnPageChanged 
							Pagerstyle-Visible=False 
							OnDeleteCommand=DEDR_Delete 
							OnUpdateCommand=DEDR_Close 
							OnSortCommand=Sort_Grid  
                            OnItemDataBound="dgLine_BindGrid" 
							AllowSorting=True CssClass="font9Tahoma">
								
							<HeaderStyle CssClass="mr-h" />
							<ItemStyle CssClass="mr-l" />
							<AlternatingItemStyle CssClass="mr-r" />
							
							<Columns>
								<asp:HyperLinkColumn 
									HeaderText="Contract No."
									ItemStyle-Width="15%"
									SortExpression="ctr.ContractNo" 
									DataNavigateUrlField="ContractNo" 
									DataNavigateUrlFormatString="CM_Trx_ContractRegDet.aspx?tbcode={0}" 
									DataTextField="ContractNo" />
									
								<asp:TemplateColumn HeaderText="Date" ItemStyle-Width="7%" SortExpression="ctr.ContractDate">
									<ItemTemplate>
										<%# objGlobal.GetLongDate(Container.DataItem("ContractDate")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								
                                <asp:TemplateColumn HeaderText="Customer" ItemStyle-Width="25%" SortExpression="BuyerCode">
	                                <ItemTemplate>
		                                <%#Container.DataItem("BuyerName")%>
	                                </ItemTemplate>
                                </asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="PPN" ItemStyle-Width="5%" SortExpression="ctr.PPNDescr">
									<ItemTemplate>
										<%# Container.DataItem("PPNDescr") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Product" ItemStyle-Width="8%" SortExpression="ctr.ProductCode">
									<ItemTemplate>
										<%#objWMTrx.mtdGetWeighBridgeTicketProduct(Container.DataItem("ProductCode"))%>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Quantity" ItemStyle-Width="8%" SortExpression="ctr.ContractQty">
									<ItemTemplate>
										<asp:Label id=txtIDContractQty runat="server"										
										Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("ContractQty"),2),2) %>'
										width=100% />
									</ItemTemplate>
								</asp:TemplateColumn>
									
								<asp:TemplateColumn HeaderText="Price" ItemStyle-Width="8%" SortExpression="ctr.Price">
									<ItemTemplate>
										<%# Container.DataItem("CurrencyCode")%> 
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Price"), 2), 2) %> id="lblIDPrice" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Other No." ItemStyle-Width="10%" SortExpression="ctr.BuyerNo">
									<ItemTemplate>
										<%# Container.DataItem("BuyerNo") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Status" ItemStyle-Width="6%" SortExpression="ctr.Status">
									<ItemTemplate>
										<%# objCMTrx.mtdGetContractStatus(Container.DataItem("Status")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn ItemStyle-Width="3%" ItemStyle-HorizontalAlign=Right>
									<ItemTemplate>
										<asp:LinkButton id=lbClose CommandName=Update Text=Close runat=server />
										<asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server />
										<asp:Label id=lblLnId Visible=False text=<%# Container.DataItem("ContractNo") %> Runat="server" />
										<asp:label id=lblContractType visible=false text=<%# Container.DataItem("ContractType") %> runat=server />
										<asp:label id=lblContractDate visible=false text=<%# Container.DataItem("ContractDate") %> runat=server />
										<asp:label id=lblProductCode visible=false text=<%# Container.DataItem("ProductCode") %> runat=server />
										<asp:label id=lblSellerCode visible=false text=<%# Container.DataItem("SellerCode") %> runat=server />
										<asp:label id=lblBuyerCode visible=false text=<%# Container.DataItem("BuyerCode") %> runat=server />
										<asp:label id=lblContractQty visible=false text=<%# Container.DataItem("ContractQty") %> runat=server />
										<asp:label id=lblExtraQty visible=false text=<%# Container.DataItem("ExtraQty") %> runat=server />
										<asp:label id=lblExtraQtyType visible=false text=<%# Container.DataItem("ExtraQtyType") %> runat=server />
										<asp:label id=lblMatchedQty visible=false text=<%# Container.DataItem("MatchedQty") %> runat=server />
										<asp:label id=lblCurrencyCode visible=false text=<%# Container.DataItem("CurrencyCode") %> runat=server />
										<asp:label id=lblPrice visible=false text=<%# Container.DataItem("Price") %> runat=server />
										<asp:label id=lblPriceBasisCode visible=false text=<%# Container.DataItem("PriceBasisCode") %> runat=server />
										<asp:label id=lblDelMonth visible=false text=<%# Container.DataItem("DelMonth") %> runat=server />
										<asp:label id=lblDelYear visible=false text=<%# Container.DataItem("DelYear") %> runat=server />
										<asp:label id=lblRemarks visible=false text=<%# Container.DataItem("Remarks") %> runat=server />
										<asp:label id=lblAccCode visible=false text=<%# Container.DataItem("AccCode") %> runat=server />
										<asp:label id=lblBlkCode visible=false text=<%# Container.DataItem("BlkCode") %> runat=server />
										<asp:label id="lblStatus" visible=false text=<%# Trim(Container.DataItem("Status")) %> runat="server" />
										<asp:label id="lblActiveMatch" visible=false text = <%#Container.DataItem("MatchingId")%> runat=server />
										<asp:label id="lblContType" visible=false text = <%#Container.DataItem("ContType")%> runat=server />
										<asp:label id="lblContCategory" visible=false text = <%#Container.DataItem("ContCategory")%> runat=server />
										<asp:label id="lblTerm" visible=false text = <%#Container.DataItem("TermOfDelivery")%> runat=server />
										<asp:label id="lblBankCode" visible=false text = <%#Container.DataItem("BankCode")%> runat=server />
										<asp:label id="lblBankAccNo" visible=false text = <%#Container.DataItem("BankAccNo")%> runat=server />
										<asp:label id="lblDelivMonth" visible=false text = <%#Container.DataItem("DelivMonth")%> runat=server />
										
										<asp:label id="lblBankCode2" visible=false text ='<%#Container.DataItem("BankCode2")%>' runat=server />
										<asp:label id="lblBankAccNo2" visible=false text ='<%#Container.DataItem("BankAccNo2")%>' runat=server />
										<!-- Start Issue UAT 23 Nov 2006 PRM --> 
										<asp:label id=lblProductQuality visible=false text ='<%#Container.DataItem("ProductQuality")%>' runat=server />
										<asp:label id=lblTermOfPayment visible=false text=<%# Container.DataItem("TermOfPayment") %> runat=server />
										<!-- End -->
										
									</ItemTemplate>
								</asp:TemplateColumn>	
							</Columns>
						</asp:DataGrid><BR>
					</td>
				</tr>
				<tr>
					<td align=right colspan="6">
						<asp:ImageButton id="btnPrev" runat="server" imageurl="../../images/icn_prev.gif" alternatetext="Previous" commandargument="prev" onClick="btnPrevNext_Click" />
						<asp:DropDownList id="lstDropList" runat="server"
							AutoPostBack="True" 
							onSelectedIndexChanged="PagingIndexChanged" />
			         	<asp:Imagebutton id="btnNext" runat="server"  imageurl="../../images/icn_next.gif" alternatetext="Next" commandargument="next" onClick="btnPrevNext_Click" />
					</td>
				</tr>
				<tr>
					<td align="left" ColSpan=6>
						<asp:ImageButton id=NewTBBtn OnClick="NewTBBtn_Click" imageurl="../../images/butt_new.gif" AlternateText="New Contract Registration" runat="server"/>
						<asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print visible="false" runat="server"/>
						<asp:Label id=SortCol Visible=False Runat="server" />
					</td>
				</tr>
                </table>
                </div>
            </td>
            </tr>
			</table>
			<asp:ScriptManager ID="ScriptManager1" runat="server">    </asp:ScriptManager>
		</FORM>
	</body>
</html>
