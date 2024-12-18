<%@ Page Language="vb" codefile="../../../include/CM_Trx_ContractMatchList.aspx.vb" Inherits="CM_Trx_ContractMatchList" %>
<%@ Register TagPrefix="UserControl" Tagname="menu_CM_Trx" src="../../menu/menu_CMTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v7.3" Namespace="Infragistics.WebUI.UltraWebTab"
    TagPrefix="igtab" %>

<%@ Register TagPrefix="qsf" Namespace="Telerik.QuickStart" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %> 

<html>
	<head>
		<title>Contract Matching List</title>
		<link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		
	    <style type="text/css">
            .auto-style1 {
                height: 41px;
            }
            .auto-style2 {
                width: 100%;
            }
        </style>
	</head>
	<body>
		<form id=frmSeller class="main-modul-bg-app-list-pu"  runat=server >
			
			<div class="kontenlist">
						<table border="0" cellspacing="1" cellpadding=0 width="100%" class="font9Tahoma">
							
							<tr>
								<td class="mt-h"> <strong>CONTRACT MATCHING LIST</strong> </td>
								<td align=right><asp:label id="lblTracker" runat="server"/></td>
								   
								<td colspan="2" class="auto-style1"><UserControl:menu_CM_Trx id=menu_CM_Trx runat="server" /></td>
							</tr>
	
							<tr>
								<td colspan=2 width=100% class="mb-c">

									<table width="100%" cellspacing="0" cellpadding="3" border="0" align="center" class="font9Tahoma">
										<tr  style="background-color:#FFCC00">
											<td width="10%" height="26">Product :<BR><asp:DropDownList id="ddlProduct" CssClass="fontObject" width=100% runat=server/></td>	
											<td width="25%" height="26"><asp:label id=lblBillParty runat=server/> :<BR>
												<telerik:RadComboBox   CssClass="fontObject" ID="ddlBuyer"    AutoPostBack="true"  
												OnSelectedIndexChanged=onChanged_BillParty
                                                Runat="server" AllowCustomText="True" 
                                                EmptyMessage="Please Select Customer" Height="200" Width="100%" 
                                                ExpandDelay="50" Filter="Contains" Sort="Ascending" 
                                                EnableVirtualScrolling="True" auto> 
                                                <CollapseAnimation Type="InQuart" />
	                                            </telerik:RadComboBox>
										</td>
											<td height="26">Contract No :<BR>
												<telerik:RadComboBox   CssClass="fontObject" ID="radcmbCtr"  autopostback=False
													
													Runat="server" AllowCustomText="True" 
													EmptyMessage="Plese Select Contract No " Height="200" Width="100%" 
													ExpandDelay="50" Filter="Contains" Sort="Ascending" 
													EnableVirtualScrolling="True">
													<CollapseAnimation Type="InQuart" />
												</telerik:RadComboBox>

											</td>
											<td width="20%" height="26">&nbsp;</td>
											<td width="10%" height="26" align=right valign=bottom><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click runat="server" CssClass="button-small"/></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td colspan=2>	
													<igtab:UltraWebTab ID="TABBK" ThreeDEffect="False" TabOrientation="TopLeft"
														SelectedTab="0" Font-Names="Tahoma" Font-Size="8pt" ForeColor=black runat="server">
														<DefaultTabStyle Height="22px">
														</DefaultTabStyle>
														<HoverTabStyle CssClass="ContentTabHover">
														</HoverTabStyle>
														<RoundedImage LeftSideWidth="6" RightSideWidth="6" SelectedImage="../../images/thumbs/ig_tab_winXP1.gif"
														NormalImage="../../images/thumbs/ig_tab_winXP3.gif" HoverImage="../../images/thumbs/ig_tab_winXP2.gif"
														FillStyle="LeftMergedWithCenter"></RoundedImage>
														<SelectedTabStyle CssClass="ContentTabSelected">
														</SelectedTabStyle>
														<Tabs>
															
															<igtab:Tab Key="HISTORY" Text="HISTORY WEIGHING" Tooltip="WB TICKET">
																<ContentPane>
																	<div id="div1" style="height:520px;width:1100;overflow:auto;">
																		<asp:DataGrid id=dgLine 
																			runat=server
																			AutoGenerateColumns=false 
																			width=100% 
																			GridLines=none 
																			Cellpadding=2 												 
																			CellSpacing=1
																			Pagerstyle-Visible=False 
																			AllowSorting=True >
								
																			<HeaderStyle CssClass="mr-h" />
																			<ItemStyle CssClass="mr-l" />
																			<AlternatingItemStyle CssClass="mr-r" />
							
																			<Columns>														 
																				<asp:TemplateColumn HeaderText="DO No" ItemStyle-Width="10%">
																					<ItemTemplate>
																						<%#Container.DataItem("DoShow")%>
																					</ItemTemplate>
																				</asp:TemplateColumn>

																				<asp:TemplateColumn HeaderText="Contract No" ItemStyle-Width="15%">
																					<ItemTemplate>
																						<%#Container.DataItem("ContractDesc")%>
																					</ItemTemplate>
																				</asp:TemplateColumn>
								 															
																				<asp:TemplateColumn HeaderText="Date" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
																					<ItemTemplate>
																						<%# objGlobal.GetLongDate(Container.DataItem("OutDate")) %>
																					</ItemTemplate>
																				</asp:TemplateColumn>
									  
																				<asp:TemplateColumn HeaderText="Ticket No" ItemStyle-Width="12%">
																					<ItemTemplate>
																						<%#Container.DataItem("TicketNo")%>
																					</ItemTemplate>
																				</asp:TemplateColumn>

																				<asp:TemplateColumn HeaderText="Customer" ItemStyle-Width="25%">
																					<ItemTemplate>
																						<%#Container.DataItem("CustName")%>
																					</ItemTemplate>
																				</asp:TemplateColumn>

																				<asp:TemplateColumn HeaderText="Vehicle" ItemStyle-Width="8%">
																					<ItemTemplate>
																						<%#Container.DataItem("VehicleCode")%>
																					</ItemTemplate>
																				</asp:TemplateColumn>

																				<asp:TemplateColumn HeaderText="Qty DO <br> (Kg/Trip)">
																					<HeaderStyle HorizontalAlign="Center" /> 
																					<ItemStyle HorizontalAlign="Right" Width="8%" /> 
																					<ItemTemplate>
																						<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("QtyDO"), 0), 0) %> id="lblAmountDB" runat="server" />
																					</ItemTemplate>
																				</asp:TemplateColumn>	

																				<asp:TemplateColumn HeaderText="Apply Qty <br> (Kg/Trip)">
																					<HeaderStyle HorizontalAlign="Center" /> 
																					<ItemStyle HorizontalAlign="Right" Width="8%" /> 
																					<ItemTemplate>
																						<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("ApplyQty"), 0), 0) %> id="lblAmountDB" runat="server" />
																					</ItemTemplate>
																				</asp:TemplateColumn>	


																				<asp:TemplateColumn HeaderText="To Date <br> (Kg/Trip)">
																					<HeaderStyle HorizontalAlign="Center" /> 
																					<ItemStyle HorizontalAlign="Right" Width="8%" /> 
																					<ItemTemplate>
																						<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("ToDate"), 0), 0) %> id="lblAmountDB" runat="server" />
																					</ItemTemplate>
																				</asp:TemplateColumn>

																				<asp:TemplateColumn HeaderText="Balance <br> (Kg/Trip)">
																					<HeaderStyle HorizontalAlign="Center" /> 
																					<ItemStyle HorizontalAlign="Right" Width="8%" /> 
																					<ItemTemplate>
																						<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Balance"), 0), 0) %> id="lblAmountDB" runat="server" />
																					</ItemTemplate>
																				</asp:TemplateColumn>

								
																			</Columns>
																		</asp:DataGrid><BR>
																	</div>
																</ContentPane>
															</igtab:Tab>
															<igtab:Tab Key="SUMMARY" Text="DO SUMMARY" Tooltip="WB TICKET">
																<ContentPane>
																	<div id="div2" style="height:520px;width:1100;overflow:auto;">
																		<asp:DataGrid id=dgSummary																runat=server
																			AutoGenerateColumns=false 
																			width=100% 
																			GridLines=none 
																			Cellpadding=2 	
																			CellSpacing="2"
																			Pagerstyle-Visible=False 
																			AllowSorting=True >								
																			<HeaderStyle CssClass="mr-h" />
																			<ItemStyle CssClass="mr-l" />
																			<AlternatingItemStyle CssClass="mr-r" />
							
																			<Columns>
														 						<asp:TemplateColumn HeaderText="Product" ItemStyle-Width="5%">
																					<ItemTemplate>
																						<%#Container.DataItem("ProductCode")%>
																					</ItemTemplate>
																				</asp:TemplateColumn>

																				<asp:TemplateColumn HeaderText="Contract No" ItemStyle-Width="28%">
																					<ItemTemplate>
																						<%#Container.DataItem("ContractDesc")%>
																					</ItemTemplate>
																				</asp:TemplateColumn>

																				<asp:TemplateColumn HeaderText="DO No" ItemStyle-Width="10%">
																					<ItemTemplate>
																						<%#Container.DataItem("DONo")%>
																					</ItemTemplate>
																				</asp:TemplateColumn>
								 															 
									  
																				<asp:TemplateColumn HeaderText="Customer Code" ItemStyle-Width="10%">
																					<ItemTemplate>
																						<%#Container.DataItem("Customer")%>
																					</ItemTemplate>
																				</asp:TemplateColumn>

																				<asp:TemplateColumn HeaderText="Customer Name" ItemStyle-Width="20%">
																					<ItemTemplate>
																						<%#Container.DataItem("CustName")%>
																					</ItemTemplate>
																				</asp:TemplateColumn>
																	 
																				<asp:TemplateColumn HeaderText="Qty DO">
																					<HeaderStyle HorizontalAlign="Right" /> 
																					<ItemStyle HorizontalAlign="Right" Width="8%" /> 
																					<ItemTemplate>
																						<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("DOQty"), 0), 0) %> id="lblAmountDB" runat="server" />
																					</ItemTemplate>
																				</asp:TemplateColumn>	

																				<asp:TemplateColumn HeaderText="Apply Qty">
																					<HeaderStyle HorizontalAlign="Right" /> 
																					<ItemStyle HorizontalAlign="Right" Width="8%" /> 
																					<ItemTemplate>
																						<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("ApplyQty"), 0), 0) %> id="lblAmountDB" runat="server" />
																					</ItemTemplate>
																				</asp:TemplateColumn>	 

																				<asp:TemplateColumn HeaderText="Balance">
																					<HeaderStyle HorizontalAlign="Right" /> 
																					<ItemStyle HorizontalAlign="Right" Width="8%" /> 
																					<ItemTemplate>
																						<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Balance"), 0), 0) %> id="lblAmountDB" runat="server" />
																					</ItemTemplate>
																				</asp:TemplateColumn>

								
																			</Columns>
																		</asp:DataGrid><BR>
																	</div>
																</ContentPane>
															</igtab:Tab>
															<igtab:Tab Key="OUTSTANDING" Text="DO OUTSTANDING" Tooltip="WB TICKET">
																<ContentPane>
																	<div id="dvOst" style="height:520px;width:1100;overflow:auto;">
																			    <table width="100%" cellspacing="0" cellpadding="3" border="0" align="center" class="font9Tahoma">
																				<tr>
																					<td>To Date :</td>
																					<td>								
																						<telerik:RadDatePicker ID="srchDateOst" Runat="server" Culture="en-US" AutoPostBack="true" OnSelectedDateChanged="srchBtn_Click"> 
																						<Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>
																						<DateInput DisplayDateFormat="dd/MMM/yyyy" DateFormat="dd/MMM/yyyy" EnableSingleInputRendering="True" LabelWidth="64px"></DateInput>
																						<DatePopupButton ImageUrl="" HoverImageUrl=""  ></DatePopupButton>
																						</telerik:RadDatePicker>
																					</td>
																				</tr>
																				<tr>
																					<td colspan="2">
																						<asp:DataGrid id=dgOst																runat=server
																						AutoGenerateColumns=false 
																						width=100% 
																						GridLines=none 
																						Cellpadding=2 												 
																						Pagerstyle-Visible=False 
																						AllowSorting=True >								
																						<HeaderStyle CssClass="mr-h" />
																						<ItemStyle CssClass="mr-l" />
																						<AlternatingItemStyle CssClass="mr-r" />
							
																						<Columns>
														 									<asp:TemplateColumn HeaderText="Product" ItemStyle-Width="5%">
																								<ItemTemplate>
																									<%#Container.DataItem("ProductCode")%>
																								</ItemTemplate>
																							</asp:TemplateColumn>								

																							<asp:TemplateColumn HeaderText="DO No" ItemStyle-Width="10%">
																								<ItemTemplate>
																									<%#Container.DataItem("DONo")%>
																								</ItemTemplate>
																							</asp:TemplateColumn>
																				
																							<asp:TemplateColumn HeaderText="DO Date" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
																								<ItemTemplate>
																									<%# objGlobal.GetLongDate(Container.DataItem("TglDo")) %>
																								</ItemTemplate>
																							</asp:TemplateColumn>
																				
																							<asp:TemplateColumn HeaderText="Contract No" ItemStyle-Width="25%">
																								<ItemTemplate>
																									<%#Container.DataItem("ContractNo")%>
																								</ItemTemplate>
																							</asp:TemplateColumn>
																				
																							<asp:TemplateColumn HeaderText="Customer Name" ItemStyle-Width="18%">
																								<ItemTemplate>
																									<%#Container.DataItem("CustName")%>
																								</ItemTemplate>
																							</asp:TemplateColumn>
																	 
																							<asp:TemplateColumn HeaderText="Qty DO">
																								<HeaderStyle HorizontalAlign="Right" /> 
																								<ItemStyle HorizontalAlign="Right" Width="7%" /> 
																								<ItemTemplate>
																									<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("QtyDO"), 0), 0) %> id="lblAmountDB" runat="server" />
																								</ItemTemplate>
																							</asp:TemplateColumn>	

																							<asp:TemplateColumn HeaderText="Apply Qty">
																								<HeaderStyle HorizontalAlign="Right" /> 
																								<ItemStyle HorizontalAlign="Right" Width="7%" /> 
																								<ItemTemplate>
																									<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("QtyTimbang"), 0), 0) %> id="lblAmountDB" runat="server" />
																								</ItemTemplate>
																							</asp:TemplateColumn>	 

																							<asp:TemplateColumn HeaderText="Balance">
																								<HeaderStyle HorizontalAlign="Right" /> 
																								<ItemStyle HorizontalAlign="Right" Width="7%" /> 
																								<ItemTemplate>
																									<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Balance"), 0), 0) %> id="lblAmountDB" runat="server" />
																								</ItemTemplate>
																							</asp:TemplateColumn>

								
																						</Columns>
																					</asp:DataGrid><BR>
																					</td>
																				</tr>
																			</table>																		
																	</div>
																</ContentPane>
															</igtab:Tab>
														</Tabs>
													</igtab:UltraWebTab>
								</td>
							</tr>
							<tr>
								<td align=right colspan="2">
									<asp:ImageButton id="btnPrev" runat="server" imageurl="../../images/icn_prev.gif" alternatetext="Previous" commandargument="prev" onClick="btnPrevNext_Click" />
									<asp:DropDownList id="lstDropList" runat="server"
										AutoPostBack="True" 
										onSelectedIndexChanged="PagingIndexChanged" />
			         				<asp:Imagebutton id="btnNext" runat="server"  imageurl="../../images/icn_next.gif" alternatetext="Next" commandargument="next" onClick="btnPrevNext_Click" />
								</td>
							</tr>
							<tr>
								<td align="left" ColSpan=2>						
									<asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print visible="false" runat="server"/>
									<asp:Label id=SortCol Visible=False Runat="server" />
								</td>
							</tr>
						
						</table>
					
						<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
						<asp:label id="SortExpression" Visible="False" Runat="server" />
						<asp:label id="lblCode" Visible="False" text=" Code" Runat="server" />
						 <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>  
		 </div>
		</FORM>

	</body>
</html>
