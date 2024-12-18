<%@ Page Language="vb" src="../../../include/CM_Trx_FFBContractRegList.aspx.vb" Inherits="CM_Trx_FFBContractRegList" %>
<%@ Register TagPrefix="UserControl" Tagname="menu_CM_Trx" src="../../menu/menu_CMTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>FFB Contract Registration List</title>
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
					<td class="font9Tahoma" colspan="3"><strong> FFB CONTRACT REGISTRATION LIST</strong></td>
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
								</td>
								<td valign=top width="25%" height="26">
								    Seller:
									<asp:TextBox id=txtSeller width=100% maxlength="30" CssClass="fontObject" runat="server" />
								</td>
								<td valign=top width="15%" height="26">Product :<BR>
									<asp:DropDownList id="ddlProduct" CssClass="fontObject" width=100% runat=server/>
								</td>
								<td valign=top width=8%>Period :<BR>
								    <asp:DropDownList id="lstAccMonth" width=100%  CssClass="fontObject" runat=server>
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
								    <asp:DropDownList id="lstAccYear" width=100% CssClass="fontObject" runat=server>
									</asp:DropDownList>
								</td>
                                <td height="26" valign="top" width="10%">
                                    Pricing Mtd<br />
                                    <asp:DropDownList id="ddlPricingMtd" width=100% CssClass="fontObject" runat=server/></td>
								<td valign=top width="10%" height="26">Status :<br>
									<asp:DropDownList id="ddlStatus" width=100% CssClass="fontObject" runat=server/>
								</td>
								<td width="5%" valign=bottom height="26" align=right>
									<asp:Button id=Searchbtn Text="Search" OnClick=srchBtn_Click CssClass="button-small" runat="server"/>
								</td>
							</tr>
							<tr>
								<td valign=top colspan=9>
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
							AutoGenerateColumns=False 
							width=100% 
							GridLines=None 
							Cellpadding=2 
							AllowPaging=True 
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
									SortExpression="ContractNo" 
									DataNavigateUrlField="ContractNo" 
									DataNavigateUrlFormatString="CM_Trx_FFBContractRegDet.aspx?tbcode={0}" 
									DataTextField="ContractNo" >
                                    <ItemStyle Width="13%" />
                                </asp:HyperLinkColumn>

								<asp:TemplateColumn HeaderText="Date" Visible=False SortExpression="ContractDate">
									<ItemTemplate>
									    <asp:Label Text=<%# Container.DataItem("ContractNo") %> id="lblContractNo"  runat="server" />
									</ItemTemplate>
                                    <ItemStyle Width="1%" />
								</asp:TemplateColumn>
																	
								<asp:TemplateColumn HeaderText="Contract Date" SortExpression="ContractDate">
									<ItemTemplate>
										<%# objGlobal.GetLongDate(Container.DataItem("ContractDate")) %> <br />
									</ItemTemplate>
                                    <ItemStyle Width="7%" />
								</asp:TemplateColumn>

								<asp:TemplateColumn HeaderText="Start Date">
									<ItemTemplate>
										<%#objGlobal.GetLongDate(Container.DataItem("Period1"))%> <br />
									</ItemTemplate>
                                    <ItemStyle Width="6%" />
								</asp:TemplateColumn>

								<asp:TemplateColumn HeaderText="To">
									<ItemTemplate>
										<%#objGlobal.GetLongDate(Container.DataItem("Period2"))%> <br />
									</ItemTemplate>
                                    <ItemStyle Width="6%" />
								</asp:TemplateColumn>
																								
                                <asp:TemplateColumn HeaderText="Supplier">
	                                <ItemTemplate>
		                                <%#Container.DataItem("Name")%>
	                                </ItemTemplate>
                                    <ItemStyle Width="12%" />
                                </asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Pricing Mtd">
									<ItemTemplate>
										<%#Container.DataItem("PricingDescr")%>
									</ItemTemplate>
                                    <ItemStyle Width="8%" />
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Quantity">
									<ItemTemplate>
										<asp:Label id=txtIDContractQty runat="server"										
										Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Qty"),2),2) %>'
										width=100% />
									</ItemTemplate>
                                    <ItemStyle Width="5%" />
								</asp:TemplateColumn>
									
								<asp:TemplateColumn HeaderText="Price">
									<ItemTemplate>
										<%# Container.DataItem("CurrencyCode")%> 
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Price"), 2), 2) %> id="lblIDPrice" runat="server" />
									</ItemTemplate>
                                    <ItemStyle Width="8%" />
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Fee(A)">
									<ItemTemplate>
										<%# Container.DataItem("CurrencyCode")%> 
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("AddFee"), 2), 2) %> id="lblFeeGrA" runat="server" />
									</ItemTemplate>
                                    <ItemStyle Width="5%" />
								</asp:TemplateColumn>	

								<asp:TemplateColumn HeaderText="Fee(B)">
									<ItemTemplate>
										<%# Container.DataItem("CurrencyCode")%> 
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("AddFee2"), 2), 2) %> id="lblFeeGrB" runat="server" />
									</ItemTemplate>
                                    <ItemStyle Width="5%" />
								</asp:TemplateColumn>	

								<asp:TemplateColumn HeaderText="Fee(C)">
									<ItemTemplate>
										<%# Container.DataItem("CurrencyCode")%> 
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("AddFee3"), 2), 2) %> id="lblFeeGrC" runat="server" />
									</ItemTemplate>
                                    <ItemStyle Width="5%" />
								</asp:TemplateColumn>	

								<asp:TemplateColumn HeaderText="Fee BRD">
									<ItemTemplate>
										<%# Container.DataItem("CurrencyCode")%> 
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("BRDFEE"), 2), 2) %> id="lblFeeBrd" runat="server" />
									</ItemTemplate>
                                    <ItemStyle Width="5%" />
								</asp:TemplateColumn>	
								
	                            <asp:TemplateColumn HeaderText="Term Of Weighing">
									<ItemTemplate>
										<%#Container.DataItem("TermOfWeighingX")%>
									</ItemTemplate>
                                    <ItemStyle Width="4%" />
								</asp:TemplateColumn>
								
				                <asp:TemplateColumn HeaderText="Additional Note">
									<ItemTemplate>
										<%#Container.DataItem("AdditionalNote")%>
									</ItemTemplate>
                                    <ItemStyle Width="19%" />
								</asp:TemplateColumn>
																																
								<asp:TemplateColumn HeaderText="Status">
									<ItemTemplate>
										<%# objCMTrx.mtdGetContractStatus(Container.DataItem("Status")) %>
									</ItemTemplate>
                                    <ItemStyle Width="5%" />
								</asp:TemplateColumn>
								
								
								<asp:TemplateColumn>
									<ItemTemplate>
										<asp:LinkButton id=lbClose CommandName=Update Text=Close runat=server />
										<asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server />
										<asp:Label id=lblLnId Visible=False text=<%# Container.DataItem("ContractNo") %> Runat="server" />
										<asp:label id=lblContractDate visible=false text=<%# Container.DataItem("ContractDate") %> runat=server />
										<asp:label id=lblSellerCode visible=false text=<%# Container.DataItem("SupplierCode") %> runat=server />
										<asp:label id=lblContractQty visible=false text=<%# Container.DataItem("Qty") %> runat=server />
										<asp:label id=lblCurrencyCode visible=false text=<%# Container.DataItem("CurrencyCode") %> runat=server />
										<asp:label id=lblPrice visible=false text=<%# Container.DataItem("Price") %> runat=server />
										<asp:label id=lblDelMonth visible=false text=<%# Container.DataItem("AccMonth") %> runat=server />
										<asp:label id=lblDelYear visible=false text=<%# Container.DataItem("AccYear") %> runat=server />
										<asp:label id=lblRemarks visible=false text=<%# Container.DataItem("Remarks") %> runat=server />
										<asp:label id="lblStatus" visible=false text=<%# Trim(Container.DataItem("Status")) %> runat="server" />
										
									</ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" Width="3%" />
								</asp:TemplateColumn>	
							</Columns>
                            <PagerStyle Visible="False" />
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
						&nbsp;<asp:ImageButton ID="PrintBtn" runat="server" AlternateText="Print" 
                            CausesValidation="False" ImageUrl="../../images/butt_print.gif" 
                            onclick="PrintBtn_Click" />
&nbsp;<!--<asp:ImageButton id=ibPrint OnClick="stBtn_Click" imageurl="../../images/butt_print.gif" AlternateText=Print runat="server"/>--><asp:Label id=SortCol Visible=False Runat="server" />
					</td>
				</tr>
                </table>
                </div>
                </td>
            </tr>
			</table>
		</FORM>
	</body>
</html>
