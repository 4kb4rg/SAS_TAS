<%@ Page Language="vb" trace=false CodeFile="~/include/WM_trx_WeighBridgeTicketList.aspx.vb" Inherits="WM_WeighBridgeTicketList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuWMTrx" src="../../menu/menu_wmtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="qsf" Namespace="Telerik.QuickStart" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>    

<html>
	<head>
		<title>Weighing Management - WeighBridge Ticket List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
	    <form id=frmMain class="main-modul-bg-app-list-pu"  runat=server >
        <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma" >
		<tr>
             <td style="width: 100%; height: 800px" valign="top">
			    <div class="kontenlist">
					<asp:Label id=SortExpression visible=false runat=server />
					<asp:Label id=SortCol visible=false runat=server />
					<asp:Label id=lblErrMessage visible=false text="Error while initiating component." runat=server />
					<table border=0 cellspacing=1 cellpadding=1 width=100% class="font9Tahoma" >
					<tr>
						<td colspan=6><UserControl:MenuWMTrx id=MenuWMTrx runat=server /></td>
					</tr>
					<tr>
						<td  colspan="3"><strong> WEIGHBRIDGE TICKET LIST</strong></td>
						<td colspan="3" align=right><asp:label id="lblTracker" runat="server"/></td>
					</tr>
					<tr>
						<td colspan=6><hr style="width :100%" />   </td>
					</tr>
					<tr>
						<td colspan=6 width=100% style="background-color:#FFCC00">
							<table width="100%" cellspacing="0" cellpadding="3" border="0" class="font9Tahoma" align="center">
								<tr class="mb-t">
									<td width="14%" height="26">Ticket No. :<BR><asp:TextBox id=srchTicketNo width=100% maxlength="20" CssClass="fontObject" runat="server" /></td>
									<td width="14%">Contract No. :<BR><asp:TextBox id=srchContractNo width=100% maxlength="20" CssClass="fontObject" runat="server"/></td>
									<td width="14%">DO No. :<BR><asp:TextBox id=srchDeliveryNo width=100% maxlength="20" CssClass="fontObject" runat="server"/></td>
									<td valign=top width="15%" height="26" align=right>
										<br>From:  <telerik:RadDatePicker ID="srchDateIn" Runat="server" Culture="en-US"> 
														<Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>
														<DateInput DisplayDateFormat="dd/MMM/yyyy" DateFormat="dd/MMM/yyyy" EnableSingleInputRendering="True" LabelWidth="64px"></DateInput>
														<DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
													</telerik:RadDatePicker> 

										<br>To:     <telerik:RadDatePicker ID="srchDateTo" Runat="server" Culture="en-US"> 
																<Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>
																<DateInput DisplayDateFormat="dd/MMM/yyyy" DateFormat="dd/MMM/yyyy" EnableSingleInputRendering="True" LabelWidth="64px"></DateInput>
																<DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
													</telerik:RadDatePicker> 
									</td>								
									<td width="15%">Customer :<BR><asp:TextBox id=srchCustomer width=100% maxlength="20" CssClass="fontObject" runat="server"/></td>	
									<td width="10%">Product :<BR><asp:dropdownlist id=srchProductList width=100% CssClass="fontObject" runat="server"/></td>																							
									<td width="12%">Status :<BR><asp:DropDownList id="srchStatusList" width=100% CssClass="fontObject" runat=server /></td>
									<td width="12%"></td>
									<td width="10%" align=right><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click CssClass="button-small" runat="server"/></td>
								</tr>
							</table>
						</td>
					</tr>
					
					</table>

					<table border=0 cellspacing=1 cellpadding=1 width=100%>
					<tr>
						<td style="height: 24px;" colspan="5">
								<table border="0" cellspacing="1" cellpadding="1" style="Width:100%" >
									<tr>
										<td colspan=6>	
											<div id="div1" style="height:450px;width:100%;overflow:auto;">				
											<asp:DataGrid id=dgTicketList
												AutoGenerateColumns=false width=100% runat=server
												GridLines=both 
												Cellpadding=2 
												Pagerstyle-Visible=False 
												OnDeleteCommand=DEDR_Delete 
												OnSortCommand=Sort_Grid  
												ShowFooter="true"
												AllowSorting=True>
												
												<HeaderStyle CssClass="mr-h" />
												<ItemStyle CssClass="mr-l" />
												<AlternatingItemStyle CssClass="mr-r" />
												
												<Columns>
													<asp:HyperLinkColumn HeaderText="Ticket No." 
														DataNavigateUrlField="TicketNo" 
														DataNavigateUrlFormatString="WM_trx_WeighBridgeTicketDet.aspx?TicketNo={0}" 
														DataTextField="TicketNo" />
													<asp:TemplateColumn HeaderText="Date" >
														<ItemTemplate>
															<%# objGlobal.GetLongDate(Container.DataItem("InDate")) %>
														</ItemTemplate>
														<HeaderStyle HorizontalAlign="Center" />
														<ItemStyle HorizontalAlign="Center" Width="5%" />
													</asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="Product" >
														<ItemTemplate>
															<%# objWMTrx.mtdGetWeighBridgeTicketProduct(Container.DataItem("ProductCode")) %>
														</ItemTemplate>
														<ItemStyle HorizontalAlign="LEFT" Width="5%" />
													</asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="Customer" >
														<ItemTemplate>
															<%#Container.DataItem("CustomerName")%><br />
															<%#Container.DataItem("ContractNo")%>
														</ItemTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="Transporter" >
														<ItemTemplate>
															<%#Container.DataItem("TransporterName")%><br />
															<%#Container.DataItem("DeliveryNoteNo")%>
														</ItemTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="Bruto" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
														<ItemTemplate>															
															<asp:Label ID="lblSellerSecondWeight" runat="server" Text='<%# FormatNumber(Container.DataItem("SecondWeight"), 0) %>' ></asp:Label>
														</ItemTemplate>
														<HeaderStyle HorizontalAlign="Center" />
														<ItemStyle HorizontalAlign="Right" Width="5%" />														
														<FooterTemplate >
																<asp:Label ID=lblTotSecondWeight runat=server />
														</FooterTemplate>                                                                    
                                                        <FooterStyle Font-Size=Smaller BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />
													</asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="Tarra" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
														<ItemTemplate>															
															<asp:Label ID="lblSellerFirstWeight" runat="server" Text='<%# FormatNumber(Container.DataItem("FirstWeight"), 0) %>' ></asp:Label>
														</ItemTemplate>
														<HeaderStyle HorizontalAlign="Center" />
														<ItemStyle HorizontalAlign="Right" Width="5%" />
														<FooterTemplate >
																<asp:Label ID=lblTotSellerFirstWeight runat=server />
														</FooterTemplate>                                                                    
                                                        <FooterStyle Font-Size=Smaller BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />
													</asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="Netto" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
														<ItemTemplate>
															<asp:Label ID="lblSellerNetWeight" runat="server" Text='<%# FormatNumber(Container.DataItem("NetWeight"), 0) %>' ></asp:Label>															
														</ItemTemplate>
														<HeaderStyle HorizontalAlign="Center" />
														<ItemStyle HorizontalAlign="Right" Width="5%" />
														<FooterTemplate >
																<asp:Label ID=lblTotSellerNetWeight runat=server />
														</FooterTemplate>                                                                    
                                                        <FooterStyle Font-Size=Smaller BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />
													</asp:TemplateColumn>
													
													<asp:TemplateColumn HeaderText="Date">
														<ItemTemplate>
															<asp:Label ID="lblDateReceived" runat="server" Text='<%# ObjGlobal.GetLongDate(Container.DataItem("DateReceived")) %>'
																Visible="True"></asp:Label> 
															<asp:TextBox ID="TxtDateReceived" Visible=false runat="server" Text='<%# Container.DataItem("DateReceived") %>' ></asp:TextBox>
														</ItemTemplate>
														<HeaderStyle HorizontalAlign="Center" />
														<ItemStyle HorizontalAlign="Center" Width="5%" />
													</asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="Bruto" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
														<ItemTemplate>															
															<asp:Label ID="lblBuyerFirstWeight" runat="server" Text='<%# FormatNumber(Container.DataItem("BuyerFirstWeight"), 0) %>' ></asp:Label>
														</ItemTemplate>
														<HeaderStyle HorizontalAlign="Center" />
														<ItemStyle HorizontalAlign="Right" Width="5%" />
														<FooterTemplate >
																<asp:Label ID=lblTotBuyerFirstWeight runat=server />
														</FooterTemplate>                                                                    
                                                        <FooterStyle Font-Size=Smaller BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />
													</asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="Tarra" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
														<ItemTemplate>															
															<asp:Label ID="lblBuyerSecondWeight" runat="server" Text='<%# FormatNumber(Container.DataItem("BuyerSecondWeight"), 0) %>' ></asp:Label>
														</ItemTemplate>
														<HeaderStyle HorizontalAlign="Center" />
														<ItemStyle HorizontalAlign="Right" Width="5%" />
														<FooterTemplate >
																<asp:Label ID=lblTotBuyerSecondWeight runat=server />
														</FooterTemplate>                                                                    
                                                        <FooterStyle Font-Size=Smaller BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />
													</asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="Netto" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
														<ItemTemplate>															
															<asp:Label ID="lblBuyerNetWeight" runat="server" Text='<%# FormatNumber(Container.DataItem("BuyerNetWeight"), 0) %>' ></asp:Label>
														</ItemTemplate>
														<HeaderStyle HorizontalAlign="Center" />
														<ItemStyle HorizontalAlign="Right" Width="5%" />
														<FooterTemplate >
																<asp:Label ID=lblTotBuyerNetWeight runat=server />
														</FooterTemplate>                                                                    
                                                        <FooterStyle Font-Size=Smaller BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />
													</asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="Difference">
														<ItemTemplate>
															<asp:Label ID="lblSelisih" runat="server" Text='<%# FormatNumber(Container.DataItem("Selisih"),0) %>' ></asp:Label>
														</ItemTemplate>
														<HeaderStyle HorizontalAlign="Center" />
														<ItemStyle HorizontalAlign="Right" Width="5%" />
										 
														<FooterTemplate >
																<asp:Label ID=lblTotSelisih runat=server />
														</FooterTemplate>                                                                    
                                                        <FooterStyle Font-Size=Smaller BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />
													</asp:TemplateColumn>
													<asp:TemplateColumn ItemStyle-HorizontalAlign=Center>
														<ItemTemplate>
															<asp:Label id=lblTicketNo visible=false text='<%# Container.DataItem("TicketNo") %>' runat=server/>																		
														</ItemTemplate>
													</asp:TemplateColumn>
												</Columns>
											</asp:DataGrid><BR>
											</div>
										</td>
									</tr>
									<tr>
										<td colspan=5>&nbsp;</td>
									</tr>
									<tr>
										<td colspan="3">
										<asp:CheckBox id="cbExcelTicket" text=" Export To Excel" checked="false" Visible=false runat="server" /></td>
									</tr>
									<tr>
										<td colspan=5>&nbsp;</td>
									</tr>
									<tr>
										<td align="left" width="100%" ColSpan=6>
											<asp:ImageButton id=NewTicketBtn onClick=NewTicketBtn_Click imageurl="../../images/butt_new.gif" AlternateText="New Ticket" visible=True runat=server/>
											<asp:ImageButton id=TicketPrintPrev imageurl="../../images/butt_print_preview.gif" AlternateText=Print onClick="btnTicketPrintPrev_Click" runat="server"/>
										</td>
									</tr>
								</table>									
						</td>
					</tr>	
					</table>    
                </div>
            </td>
            </tr>
        </table>
         <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>                                    
		</FORM>
	</body>
</html>
