<%@ Page Language="vb" trace="false" src="../../../include/PM_trx_DailyProd_List.aspx.vb" Inherits="PM_DailyProdList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPDTrx" src="../../menu/menu_PDtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v7.3" Namespace="Infragistics.WebUI.UltraWebTab"
    TagPrefix="igtab" %>
    <html>
	<head>
		<title>Mill Production - Production List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
	    <form id=frmMain class="main-modul-bg-app-list-pu"  runat=server >
			<asp:Label id=lblErrMessage visible=false text="Error while initiating component." runat=server />
			<asp:Label id=SortExpression visible=false runat=server />
			<asp:Label id=SortCol visible=false runat=server />

            <table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<td colspan=6><UserControl:MenuPDTrx id=MenuPDTrx runat=server />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
                			
			<table border=0 cellspacing=1 cellpadding=1 width=100%>
				            <tr>
					</td>
				</tr>
				<tr>
					<td class="font12Tahoma" colspan="3" style="height: 21px"><strong> DAILY PRODUCTION LIST</strong></td>
					<td colspan="3" align=right style="height: 21px"><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6 style="height: 36px"><hr style="width :100%" />   
                 
                            </td>
				</tr>
			</table>
				
			<table border=0 cellspacing=1 cellpadding=1 width=100%>
            <igtab:UltraWebTab ID="UltraWebTab1" ThreeDEffect="False" TabOrientation="TopLeft"
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
                    <igtab:Tab Key="FFB" Text="DAILY PRODUCTION" Tooltip="DAILY PRODUCTION">                      
                        <ContentPane>
                            <table border="0" cellspacing="1" cellpadding="1" width="99%">
                                <tr>
                                    <td colspan="5">                                  	                                        			
				<tr>				
					<td colspan=6 width=100% style="background-color:#FFCC00">
						<table width="100%" cellspacing="0" cellpadding="3" border="0" align="center" class="font9Tahoma" >
							<tr class="mb-t">
								<td height="26" style="width: 21%">
                                    Period :<br />
                                    <asp:DropDownList ID="lstAccMonth" CssClass="fontObject" runat="server" Width="30%">
                                        <asp:ListItem Selected="True" Value="1">1</asp:ListItem>
                                        <asp:ListItem Value="2">2</asp:ListItem>
                                        <asp:ListItem Value="3">3</asp:ListItem>
                                        <asp:ListItem Value="4">4</asp:ListItem>
                                        <asp:ListItem Value="5">5</asp:ListItem>
                                        <asp:ListItem Value="6">6</asp:ListItem>
                                        <asp:ListItem Value="7">7</asp:ListItem>
                                        <asp:ListItem Value="8">8</asp:ListItem>
                                        <asp:ListItem Value="9">9</asp:ListItem>
                                        <asp:ListItem Value="10">10</asp:ListItem>
                                        <asp:ListItem Value="11">11</asp:ListItem>
                                        <asp:ListItem Value="12">12</asp:ListItem>
                                    </asp:DropDownList>&nbsp;<asp:DropDownList ID="lstAccYear" CssClass="fontObject"  runat="server" Width="50%">
                                    </asp:DropDownList></td>	
                                <td height="26" style="width: 19%">
                                    Last Updated By :<BR>
                                <asp:TextBox id=srchUpdateBy width=100% maxlength="128" CssClass="fontObject"  runat="server"/></td>
                                <td height="26" width="25%">
                                </td>
								<td width="45%"></td>						
								<td width="20%"></td>
								<td width="10%" height="26" valign=middle align=right><asp:Button id=SearchBtn CssClass="button-small" Text="Search" OnClick=srchBtn_Click runat="server"/></td>
							</tr>
						</table>
						
						
					</td>
				</tr>
		                          
				<tr>
					<td colspan=6>					
						<asp:DataGrid id=dgDailyProdList
							AutoGenerateColumns=False width=100% runat=server 
							Cellpadding=2 
							AllowPaging=True 
							PageSize=10
							OnPageIndexChanged=OnPageChanged 
							OnItemDataBound="dgLine_BindGrid" 
							Pagerstyle-Visible=False 
							OnItemCommand=EmpLink_Click
						    OnSortCommand=Sort_Grid  
                            ShowFooter=true
							AllowSorting=True  >
								
                            <HeaderStyle CssClass="mr-h"/>
                            <ItemStyle CssClass="mr-l"/>
                            <AlternatingItemStyle CssClass="mr-r"/>
							
							<Columns>
				                <asp:TemplateColumn HeaderText="Tgl.Produksi">
				                <ItemStyle Width="8%" HorizontalAlign="Left" /> 
				                    <ItemTemplate>
				                        <asp:LinkButton id=lblTransDate CommandName=Item text='<%# objGlobal.GetLongDate(Container.DataItem("DateProd")) %>' runat=server />
				                        <asp:Label id=lblEditTransDate text='<%# objGlobal.GetLongDate(Container.DataItem("DateProd")) %>'  visible=False runat=server />
				                    </ItemTemplate>
                                    <FooterTemplate>
                                        Total :
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateColumn>
	
								<asp:TemplateColumn HeaderText="Tgl. Sounding">
								<ItemStyle Width="6%" />
									<ItemTemplate>									
										<asp:Label id=lblSoundDate visible=true text='<%# objGlobal.GetLongDate(Container.DataItem("DateSounding")) %>' runat=server/>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Restan Semalam <br> (Kg)">
								<ItemStyle Width="6%" HorizontalAlign="Right"/>
									<ItemTemplate>
										<%#FormatNumber(Container.DataItem("BroughtForward"), 0)%>
									</ItemTemplate>									
                                    <HeaderStyle HorizontalAlign="Center" />
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="TBS Gross(Kg)">
								<ItemStyle Width="6%" HorizontalAlign="Right" />
									<ItemTemplate>
                                    <asp:Label id=lblSheetFFBBGross visible=false text='<%# Container.DataItem("TotalReceivedBruto") %>'   runat=server/>
										<%#FormatNumber(Container.DataItem("TotalReceivedBruto"),0)%>
									</ItemTemplate>
                                <FooterTemplate >
                                    <asp:Label ID=lblTotSheetFFBBGross runat=server />
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Size=Smaller BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />

                                    <HeaderStyle HorizontalAlign="Center" />
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="TBS Siap Olah <br> (Kg)">
								<ItemStyle Width="6%" HorizontalAlign="Right" />
									<ItemTemplate>
										<%#FormatNumber(Container.DataItem("AvailableBruto"), 0)%>
                                        <asp:Label id=lblSheetFFBBAvl visible=false text='<%# Container.DataItem("AvailableBruto") %>'   runat=server/>
									</ItemTemplate>

                                    <FooterTemplate >
                                        <asp:Label ID=lblTotSheetFFBBAvl runat=server />
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Size=Smaller BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />
                                    <HeaderStyle HorizontalAlign="Center" />
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="TBS Olah <br> (Kg)">
								<ItemStyle Width="6%" HorizontalAlign="Right" />
									<ItemTemplate>
                                        <asp:Label id=lblSheetFFBProc visible=false text='<%# Container.DataItem("FFBProcesedBruto") %>'   runat=server/>
										<%#FormatNumber(Container.DataItem("FFBProcesedBruto"), 0)%>
									</ItemTemplate>

                                    <FooterTemplate >
                                        <asp:Label ID=lblTotSheetFFBProc runat=server />
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Size=Smaller BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />

                                    <HeaderStyle HorizontalAlign="Center" />
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Restan Olah <br> (Kg)">
								<ItemStyle Width="6%" HorizontalAlign="Right" />
									<ItemTemplate>
										<%#FormatNumber(Container.DataItem("FFBProcesed_Restan_Bruto"), 0)%>
									</ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="S.Awal CPO <br> (Kg)">
								<ItemStyle Width="6%" HorizontalAlign="Right" />
									<ItemTemplate>
										<%#FormatNumber(Container.DataItem("CPO_SAwal"), 0)%>
									</ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="CPO Proses <br> (Kg)">
								<ItemStyle Width="6%" HorizontalAlign="Right" />
									<ItemTemplate>
										<%#FormatNumber(Container.DataItem("CPO_Procesed"), 0)%>
                                        <asp:Label id=lblSheetCPOProc visible=false text='<%# Container.DataItem("CPO_Procesed") %>'   runat=server/>
									</ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />

                                <FooterTemplate >
                                    <asp:Label ID=lblTotSheetCPOProc runat=server />
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Size=Smaller BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />

								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="CPO Kirim <br> (Kg)">
								<ItemStyle Width="6%" HorizontalAlign="Right" />
									<ItemTemplate>
                                        <asp:Label id=lblDespatchCPO visible=false text='<%# Container.DataItem("CPO_Despatch") %>'   runat=server/>
										<%#FormatNumber(Container.DataItem("CPO_Despatch"), 0)%>
									</ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />

                                <FooterTemplate >
                                    <asp:Label ID=lblTotSheetCPODesp runat=server />
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Size=Smaller BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />
    							</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Rend CPO <br> (%)">
								<ItemStyle Width="4%" HorizontalAlign="Right" />
									<ItemTemplate>
										<%#FormatNumber(Container.DataItem("CPO_ExtrakBruto"), 2)%>
									</ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
								</asp:TemplateColumn>

								<asp:TemplateColumn HeaderText="S.Akhir CPO <br> (Kg)">
								<ItemStyle Width="6%" HorizontalAlign="Right" />
									<ItemTemplate>
										<%#FormatNumber(Container.DataItem("CPO_TotStock"), 0)%>
									</ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="S.Awal PK <br> (Kg)">
								<ItemStyle Width="6%" HorizontalAlign="Right" />
									<ItemTemplate>
										<%#FormatNumber(Container.DataItem("PK_SAwal"),0)%>
									</ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="PK Proses <br> (Kg)"> 
								<ItemStyle Width="6%" HorizontalAlign="Right" />
									<ItemTemplate>
                                        <asp:Label id=lblSheetPKProc visible=false text='<%# Container.DataItem("PK_Procesed") %>'   runat=server/>
										<%#FormatNumber(Container.DataItem("PK_Procesed"),0)%>
									</ItemTemplate>

                                <FooterTemplate >
                                    <asp:Label ID=lblTotSheetPKProc runat=server />
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Size=Smaller BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />

                                    <HeaderStyle HorizontalAlign="Center" />
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="PK Kirim <br> (Kg)">
								<ItemStyle Width="6%" HorizontalAlign="Right" />
									<ItemTemplate>
                                        <asp:Label id=lblDespatchPK visible=false text='<%# Container.DataItem("PK_Despatch") %>'   runat=server/>
										<%#FormatNumber(Container.DataItem("PK_Despatch"),0)%>
									</ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                <FooterTemplate >
                                    <asp:Label ID=lblTotDespatchPK runat=server />
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Size=Smaller BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />

								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Rend. PK <br> (%)">
								<ItemStyle Width="4%" HorizontalAlign="Right" />
									<ItemTemplate>
										<%#FormatNumber(Container.DataItem("PK_ExtrakBruto"), 2)%>
									</ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
								</asp:TemplateColumn>

								<asp:TemplateColumn HeaderText="S.Akhir PK <br> (Kg)">
								<ItemStyle Width="8%" HorizontalAlign="Right" />
									<ItemTemplate>
										<%#FormatNumber(Container.DataItem("PK_TotStock"), 0)%>
									</ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
								</asp:TemplateColumn>
							</Columns>
                            <PagerStyle Visible="False" />
                            <FooterStyle BackColor="#FFFFFF" Font-Bold="True" Font-Italic="False" Font-Names="Arial Narrow"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#000000" />
							
						</asp:DataGrid>
                        <br />
                          <asp:CheckBox ID="chkIsAll" Text="Tampilkan Data 1 Bulan" BackColor="Black" Font-Bold="true"   OnCheckedChanged="Check_CheckedChanged" AutoPostBack="true"    CssClass="font9Tahoma" ForeColor="White" runat="server" />
                    </td>
				</tr>
                                       
				<tr>
					<td align=right colspan="6" style="height: 26px">
                   
						<asp:ImageButton id="btnPrev" runat="server" imageurl="../../images/icn_prev.gif" alternatetext="Previous" commandargument="prev" onClick="btnPrevNext_Click" />
						<asp:DropDownList id="lstDropList" AutoPostBack="True" onSelectedIndexChanged="PagingIndexChanged" runat="server" />
			         	<asp:Imagebutton id="btnNext" runat="server"  imageurl="../../images/icn_next.gif" alternatetext="Next" commandargument="next" onClick="btnPrevNext_Click" />
					</td>
				</tr>
				<tr>
					<td align="left" width="100%" ColSpan=6>
						<asp:ImageButton id=NewBtn onClick=NewBtn_Click imageurl="../../images/butt_new.gif" AlternateText="New" runat=server Visible="False"/>
						<asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print visible=False runat="server"/>
					</td>
				</tr>
                <tr>
                    <td align="left" colspan="6" width="100%">
                    </td>
                </tr>
                                                                                         
                <tr>
                    <td align="left" colspan="6" width="100%">                    
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
                                <igtab:Tab Key="FFB" Text="PENERIMAAN TBS" Tooltip="PENERIMAAN TBS">                      
                                    <ContentPane>
                                         <table border="0" cellspacing="1" cellpadding="1" width="99%">                                        
                                            <tr>
                                                <td colspan="5">
                                                    <div id="div4" style="height:250px;width:98%;overflow:auto;">				
                                                        <asp:DataGrid ID="dgFFBSupp" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                         OnItemDataBound="dgFFBSupp_BindGrid" 
                                                          ShowFooter=True
                                                            CellPadding="2" Width="100%" 	>							
                                                        <HeaderStyle CssClass="mr-h"/>
                                                        <ItemStyle CssClass="mr-l"/>
                                                        <AlternatingItemStyle CssClass="mr-r"/>
                                                            <Columns>
                                                                <asp:TemplateColumn HeaderText="KODE SUPPLIER" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
                                                                <ItemStyle Width="14%"  />
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# Container.DataItem("CustomerCode") %>' id="lblCustCode" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>                                    

                                                                <asp:TemplateColumn HeaderText="NAMA SUPPLIER" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
                                                                    <ItemStyle Width="25%"  />
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# Container.DataItem("Name") %>' id="lblCustName" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>     

                                                                <asp:TemplateColumn HeaderText="JJG" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                   <ItemStyle Width="8%"  />
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# FormatNumber(Container.DataItem("QtyJanjang"),0) %>' id="lblJJG" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>  
                                                                                                                                                                                              
                                                                <asp:TemplateColumn HeaderText="BRUTO (Kg)" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                   <ItemStyle Width="8%"  />
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# FormatNumber(Container.DataItem("FirstWeight"),0) %>' id="lblFweight" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>    

                                                                <asp:TemplateColumn HeaderText="TARRA (Kg)" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                   <ItemStyle Width="8%"  />
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# FormatNumber(Container.DataItem("SecondWeight"),0) %>' id="lblSweight" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>   

                                                                <asp:TemplateColumn HeaderText="GROSS (Kg)" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                    <ItemStyle Width="8%"  />
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# FormatNumber(Container.DataItem("GrossWeight"),0) %>' id="lblGweight" runat="server" />
                                                                    </ItemTemplate>
                                                		            <FooterTemplate >
									                                    <asp:Label ID=lbTotFFBGross runat=server />
									                                </FooterTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <FooterStyle Font-Size=Smaller BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />

                                                                </asp:TemplateColumn>   

                                                                <asp:TemplateColumn HeaderText="POT (Kg)" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                    <ItemStyle Width="8%"  />
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# FormatNumber(Container.DataItem("PotKG"),0) %>' id="lblPotweight" runat="server" />
                                                                    </ItemTemplate>

                                                		            <FooterTemplate >
									                                    <asp:Label ID=lblTotFFBPot runat=server />
									                                </FooterTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <FooterStyle Font-Size=Smaller BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />

                                                                  
                                                                </asp:TemplateColumn>   

                                                                <asp:TemplateColumn HeaderText="POT (%)" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                    <ItemStyle Width="8%"  />
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# FormatNumber(Container.DataItem("PotPsn"),2) %>' id="lblPotPsn" runat="server" />
                                                                    </ItemTemplate>
                               
                                                                </asp:TemplateColumn>  
                                                                                                                                                                                                                                                    
                                                                <asp:TemplateColumn HeaderText="NETTO (Kg)" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                    <ItemStyle Width="8%"  />
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# FormatNumber(Container.DataItem("NetWeight"),0) %>' id="lblNweight" runat="server" />
                                                                    </ItemTemplate>

                                                		            <FooterTemplate >
									                                    <asp:Label ID=lbTotFFBNetto CssClass="font9Tahoma" runat=server />
									                                </FooterTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <FooterStyle Font-Size=Smaller BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />

                                                                </asp:TemplateColumn>   
                                                                                                                                                                                                                                                       
                                                            </Columns>
                                                        </asp:DataGrid>
                                                     </div>
                                                </td>
                                            </tr>
                                                                                
                            <tr>
			                <td>	
<br />
                        <igtab:UltraWebTab ID="UltraWebTab3" ThreeDEffect="False" TabOrientation="TopLeft"
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
                                <igtab:Tab Key="FFB" Text="PENERIMAAN TBS SUMMARY BY TAHUN TANAM " Tooltip="PENERIMAAN TBS BERDASARKAN TAHUN TANAM">                      
                                    <ContentPane>                                    	                       		
				                         <table border="0" cellspacing="1" cellpadding="1" width="99%">
                                            <tr>
                                                <td colspan="5">
                                                    <div id="div6" style="height:250px;width:95%;overflow:auto;">				
                                                        <asp:DataGrid ID="dgFFBTTanam" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                        ShowFooter=true
                                                        
                                                            CellPadding="2" Width="100%"  >
                                                            <HeaderStyle CssClass="mr-h"/>
                                                            <ItemStyle CssClass="mr-l"/>
                                                            <AlternatingItemStyle CssClass="mr-r"/>
                                                            <Columns>
                                                             
                                                                <asp:TemplateColumn HeaderText="KODE SUPPLIER" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
                                                                <ItemStyle Width="16%"  />
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# Container.DataItem("CustomerCode") %>' id="lblCustCode" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>                                    

                                                                <asp:TemplateColumn HeaderText="NAMA SUPPLIER" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
                                                                    <ItemStyle Width="25%"  />
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# Container.DataItem("SupName") %>' id="lblCustName" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>     

                                                                <asp:TemplateColumn HeaderText="T.TANAM" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
                                                                    <ItemStyle Width="25%"  />
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# Container.DataItem("BlkCode") %>' id="lblBlkCode" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>     
  

                                                                <asp:TemplateColumn HeaderText="JJG" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                   <ItemStyle Width="8%"  />
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# FormatNumber(Container.DataItem("QtyJanjang"),0) %>' id="lblJJG" runat="server" />
                                                                    </ItemTemplate>

                                                                <FooterTemplate >
                                                                    <asp:Label ID=lbTotFFBJJg runat=server />
                                                                    </FooterTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle Font-Size=Smaller BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />


                                                                </asp:TemplateColumn>  
                                                                                                                                                                                                                                                                                                                              
                                                                <asp:TemplateColumn HeaderText="BRUTO (Kg)" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                   <ItemStyle Width="8%"  />
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# FormatNumber(Container.DataItem("FirstWeight"),0) %>' id="lblFweight" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>    

                                                                <asp:TemplateColumn HeaderText="TARRA (Kg)" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                   <ItemStyle Width="8%"  />
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# FormatNumber(Container.DataItem("SecondWeight"),0) %>' id="lblSweight" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>   

                                                                <asp:TemplateColumn HeaderText="GROSS (Kg)" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                    <ItemStyle Width="8%"  />
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# FormatNumber(Container.DataItem("GrossWeight"),0) %>' id="lblGweight" runat="server" />
                                                                    </ItemTemplate>
                                                                <FooterTemplate >
                                                                    <asp:Label ID=lbTotFFBTTGros runat=server />
                                                                    </FooterTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle Font-Size=Smaller BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />

                                                                </asp:TemplateColumn>   

                                                                <asp:TemplateColumn HeaderText="POT (Kg)" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                    <ItemStyle Width="8%"  />
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# FormatNumber(Container.DataItem("PotKG"),0) %>' id="lblPotweight" runat="server" />
                                                                    </ItemTemplate>
                                                                    <FooterTemplate >
                                                                    <asp:Label ID=lbTotFFBTTPot runat=server />
                                                                    </FooterTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle Font-Size=Smaller BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />

                                                                </asp:TemplateColumn>   
                                                                                                                                                                                                                                                    
                                                                <asp:TemplateColumn HeaderText="NETTO (Kg)" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                    <ItemStyle Width="8%"  />
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# FormatNumber(Container.DataItem("NetWeight"),0) %>' id="lblNweight" runat="server" />
                                                                    </ItemTemplate>
                                                                <FooterTemplate >
                                                                    <asp:Label ID=lbTotFFBTTNettto runat=server />
                                                                    </FooterTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle Font-Size=Smaller BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />


                                                                </asp:TemplateColumn>   
                                                                                                                                                                                                                                                       
                                                            </Columns>
                                                        </asp:DataGrid>
                                                     </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan=5>&nbsp;</td>
                                            </tr>
                                        </table>
                                     </ContentPane>
                                     </igtab:Tab>
                             </Tabs>
                             </igtab:UltraWebTab>
                                           
                                            
                                        </table>
                                    </ContentPane>
                                </igtab:Tab>

                                <igtab:Tab Key="DISP" Text="PENJUALAN" Tooltip="PENJUALAN">                      
                                    <ContentPane>
                                        <table border="0" cellspacing="1" cellpadding="1" width="99%">
                                         
                                            <tr>
                                                <td colspan="5">
                                               
                                                    <div id="div1" style="height:300px;width:95%;overflow:auto;">				
                                                        <asp:DataGrid ID="dgDisp" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                         OnItemDataBound="dgLine_BindGrid" 
                                                         ShowFooter=true
                                                            CellPadding="2" Width="100%" >
                                                            <HeaderStyle CssClass="mr-h"/>
                                                            <ItemStyle CssClass="mr-l"/>
                                                            <AlternatingItemStyle CssClass="mr-r"/>	
                                                            <Columns>
                                                                <asp:TemplateColumn HeaderText="KODE CUSTOMER" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# Container.DataItem("CustomerCode") %>' id="lblCustCode" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>                                    

                                                                <asp:TemplateColumn HeaderText="NAMA CUSTOMER" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# Container.DataItem("Name") %>' id="lblCustName" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>     

                                                                <asp:TemplateColumn HeaderText="PRODUK" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# Container.DataItem("ProductCode") %>' id="lblproduct" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>     
                                                                                                                             
                                                                <asp:TemplateColumn HeaderText="BRUTO (Kg)" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# FormatNumber(Container.DataItem("FirstWeight"),0) %>' id="lblFweight" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>    

                                                                <asp:TemplateColumn HeaderText="TARRA (Kg)" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# FormatNumber(Container.DataItem("SecondWeight"),0) %>' id="lblSweight" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>   
                                                                                                                                                                                                                                                    
                                                                <asp:TemplateColumn HeaderText="NETTO (Kg)" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# FormatNumber(Container.DataItem("NetWeight"),0) %>' id="lblNweight" runat="server" />
                                                                    </ItemTemplate>
                                                                <FooterTemplate >
                                                                    <asp:Label ID=lbTotDisp runat=server />
                                                                    </FooterTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle Font-Size=Smaller BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />

                                                                </asp:TemplateColumn>   
                                                                                                                                                                                                                                                       
                                                            </Columns>
                                                        </asp:DataGrid>
                                                     </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan=5>&nbsp;</td>
                                            </tr>
                                        </table>
                                    </ContentPane>
                                 </igtab:Tab>      
                                 
                                 <igtab:Tab Key="OTH" Text="TIMBANGAN LAINNYA" Tooltip="TIMBANGAN LAIN-LAIN">                      
                                    <ContentPane>
                                        <table border="0" cellspacing="1" cellpadding="1" width="99%">
                                         
                                            <tr>
                                                <td colspan="5">
                                               
                                                    <div id="div7" style="height:300px;width:95%;overflow:auto;">				
                                                        <asp:DataGrid ID="dgWeightOth" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                         OnItemDataBound="dgLine_BindGrid" 
                                                            CellPadding="2" Width="100%" >
								
                                                            <HeaderStyle CssClass="mr-h"/>
                                                            <ItemStyle CssClass="mr-l"/>
                                                            <AlternatingItemStyle CssClass="mr-r"/>
                                                            <Columns>
                                                                <asp:TemplateColumn HeaderText="KODE CUSTOMER" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# Container.DataItem("CustomerCode") %>' id="lblCustCode" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>                                    

                                                                <asp:TemplateColumn HeaderText="NAMA CUSTOMER" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# Container.DataItem("Name") %>' id="lblCustName" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>     

                                                                <asp:TemplateColumn HeaderText="PRODUK" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# Container.DataItem("ProductCode") %>' id="lblproduct" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>     
                                                                                                                             
                                                                <asp:TemplateColumn HeaderText="TIMBANG 1 (Kg)" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# FormatNumber(Container.DataItem("Weight1"),0) %>' id="lblFweight" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>    

                                                                <asp:TemplateColumn HeaderText="TIMBANG 2 (Kg)" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# FormatNumber(Container.DataItem("Weight2"),0) %>' id="lblSweight" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>   
                                                                                                                                                                                                                                                    
                                                                <asp:TemplateColumn HeaderText="NETTO (Kg)" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# FormatNumber(Container.DataItem("NetWeight"),0) %>' id="lblNweight" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>   
                                                                                                                                                                                                                                                       
                                                            </Columns>
                                                        </asp:DataGrid>
                                                     </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan=5>&nbsp;</td>
                                            </tr>
                                        </table>
                                    </ContentPane>
                                 </igtab:Tab>                                                         
                                                       
                            </Tabs>
                        </igtab:UltraWebTab>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="6" width="100%">
                    </td>
                </tr>
          
               </td>
               </tr>
               </table>
               </ContentPane>
               </igtab:Tab>
                             
               <igtab:Tab Key="PRODMONTHLY" Text="MONTHLY SUMMARY" Tooltip="PRODUCTION SUMMARY">                      
                <ContentPane>
                     <table border="0" cellspacing="1" cellpadding="1" width="99%">
                                <tr>
                                    <td colspan="5">                                  	                                        			
				<tr>				
					<td colspan=6 width=100% class="mb-c">
					
					</td>
				</tr>
				<tr>
					<td colspan=6>					
						<asp:DataGrid id=dgMonthlyProdList
							AutoGenerateColumns=False width=100% runat=server 
							 OnItemDataBound="dgLine_BindGrid" 
							Cellpadding=2 
							AllowPaging=True 
							Pagesize=16
                            ShowFooter=true
							AllowSorting=True >
                            <HeaderStyle CssClass="mr-h"/>
                            <ItemStyle CssClass="mr-l"/>
                            <AlternatingItemStyle CssClass="mr-r"/>
							
							<Columns>
				                <asp:TemplateColumn HeaderText="Tahun">
				                <ItemStyle Width="8%" HorizontalAlign="Left" /> 
				                    <ItemTemplate>
				                     <%# Container.DataItem("AccYear") %>
				                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateColumn>
	
				                <asp:TemplateColumn HeaderText="Bulan">
				                <ItemStyle Width="8%" HorizontalAlign="Left" /> 
				                    <ItemTemplate>
				                     <%# Container.DataItem("AccMonth") %>
				                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Restan Kemarin">
								<ItemStyle Width="6%" HorizontalAlign="Right"/>
									<ItemTemplate>
										<%#FormatNumber(Container.DataItem("BroughtForward"), 0)%>
									</ItemTemplate>									
                                    <HeaderStyle HorizontalAlign="Center" />
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="TBS Gross(Kg)">
								<ItemStyle Width="6%" HorizontalAlign="Right" />
									<ItemTemplate>
                                        <asp:Label Text='<%# FormatNumber(Container.DataItem("TotalReceivedBruto"),0) %>' id="lblSheetSumFFB" Visible="false" runat="server" />
										<%#FormatNumber(Container.DataItem("TotalReceivedBruto"),0)%>
									</ItemTemplate>

                                    <FooterTemplate >
                                        <asp:Label ID=lblSheetTotSumFFB runat=server />
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Size=Smaller BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />

                                    <HeaderStyle HorizontalAlign="Center" />
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="TBS Siap Olah(Kg)">
								<ItemStyle Width="6%" HorizontalAlign="Right" />
									<ItemTemplate>
                                        <asp:Label Text='<%# FormatNumber(Container.DataItem("AvailableNetto"),0) %>' id="lblSheetSumFFBAVl" Visible="false" runat="server" />
										<%#FormatNumber(Container.DataItem("AvailableNetto"), 0)%>
									</ItemTemplate>
                                    <FooterTemplate >
                                        <asp:Label ID=lblSheetTotSumFFBAvl runat=server />
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Size=Smaller BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />

                                    <HeaderStyle HorizontalAlign="Center" />
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="TBS Olah(Kg)">
								<ItemStyle Width="6%" HorizontalAlign="Right" />
									<ItemTemplate>
                                        <asp:Label Text='<%# FormatNumber(Container.DataItem("FFBProcesedBruto"),0) %>' id="lblSheetSumFFBProc" Visible="false" runat="server" />
										<%#FormatNumber(Container.DataItem("FFBProcesedBruto"), 0)%>
									</ItemTemplate>
                                    <FooterTemplate >
                                        <asp:Label ID=lblSheetTotSumFFBProc runat=server />
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Size=Smaller BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />

                                    <HeaderStyle HorizontalAlign="Center" />
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Restan Olah(Kg)">
								<ItemStyle Width="6%" HorizontalAlign="Right" />
									<ItemTemplate>
										<%#FormatNumber(Container.DataItem("FFBProcesed_Restan"), 0)%>
									</ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="S.Awal CPO">
								<ItemStyle Width="6%" HorizontalAlign="Right" />
									<ItemTemplate>
										<%#FormatNumber(Container.DataItem("CPO_SAwal"), 0)%>
									</ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="CPO Proses">
								<ItemStyle Width="6%" HorizontalAlign="Right" />
									<ItemTemplate>
                                        <asp:Label Text='<%# FormatNumber(Container.DataItem("CPO_Procesed"),0) %>' id="lblSheetSumCPOProc" Visible="false" runat="server" />
										<%#FormatNumber(Container.DataItem("CPO_Procesed"), 0)%>
									</ItemTemplate>
                                   <FooterTemplate >
                                        <asp:Label ID=lblSheetTotSumCPOProc runat=server />
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Size=Smaller BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />

                                    <HeaderStyle HorizontalAlign="Center" />
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="CPO Kirim">
								<ItemStyle Width="6%" HorizontalAlign="Right" />
									<ItemTemplate>
                                        <asp:Label Text='<%# FormatNumber(Container.DataItem("CPO_Despatch"),0) %>' id="lblSheetSumCPODesp" Visible="false" runat="server" />
										<%#FormatNumber(Container.DataItem("CPO_Despatch"), 0)%>
									</ItemTemplate>
                                   <FooterTemplate >
                                        <asp:Label ID=lblSheetTotSumCPODesp runat=server />
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Size=Smaller BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />

                                    <HeaderStyle HorizontalAlign="Center" />
 
                                <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle Font-Size=Smaller BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />


								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Rend CPO">
								<ItemStyle Width="4%" HorizontalAlign="Right" />
									<ItemTemplate>
										<%#FormatNumber(Container.DataItem("CPO_ExtrakBruto"), 2)%>
									</ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                   <FooterTemplate >
                                        <asp:Label ID=lblSheetMonthlyCPO runat=server />
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Size=Smaller BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />
                                       
								</asp:TemplateColumn>

								<asp:TemplateColumn HeaderText="S.Akhir CPO">
								<ItemStyle Width="6%" HorizontalAlign="Right" />
									<ItemTemplate>
										<%#FormatNumber(Container.DataItem("CPO_TotStock"), 0)%>
									</ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="S.Awal PK">
								<ItemStyle Width="6%" HorizontalAlign="Right" />
									<ItemTemplate>
										<%#FormatNumber(Container.DataItem("PK_SAwal"),0)%>
									</ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="PK Proses"> 
								<ItemStyle Width="6%" HorizontalAlign="Right" />
									<ItemTemplate>
                                        <asp:Label Text='<%# FormatNumber(Container.DataItem("PK_Procesed"),0) %>' id="lblSheetSumPKProc" Visible="false" runat="server" />
										<%#FormatNumber(Container.DataItem("PK_Procesed"),0)%>
									</ItemTemplate>
                                   <FooterTemplate >
                                        <asp:Label ID=lblSheetTotSumPKProc runat=server />
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Size=Smaller BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />

                                    <HeaderStyle HorizontalAlign="Center" />
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="PK Kirim">
								<ItemStyle Width="6%" HorizontalAlign="Right" />
									<ItemTemplate>
                                        <asp:Label Text='<%# FormatNumber(Container.DataItem("PK_Despatch"),0) %>' id="lblSheetSumPKDesp" Visible="false" runat="server" />
										<%#FormatNumber(Container.DataItem("PK_Despatch"),0)%>
									</ItemTemplate>
                                     <HeaderStyle HorizontalAlign="Center" />
                                   <FooterTemplate >
                                        <asp:Label ID=lblSheetTotSumPKDesp runat=server />
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Size=Smaller BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />
                                                                       
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Rend PK">
								<ItemStyle Width="4%" HorizontalAlign="Right" />
									<ItemTemplate>
										<%#FormatNumber(Container.DataItem("PK_ExtrakBruto"),2)%>
									</ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                   <FooterTemplate >
                                        <asp:Label ID=lblSheetMonthlyPK runat=server />
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Size=Smaller BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />
                                               
								</asp:TemplateColumn>

								<asp:TemplateColumn HeaderText="S.Akhir PK">
								<ItemStyle Width="8%" HorizontalAlign="Right" />
									<ItemTemplate>
										<%#FormatNumber(Container.DataItem("PK_TotStock"), 0)%>
									</ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
								</asp:TemplateColumn>
							</Columns>
                            <PagerStyle Visible="False" />
                            <FooterStyle BackColor="#FFFFFF" Font-Bold="True" Font-Italic="False" Font-Names="Arial Narrow"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#000000" />
							
						</asp:DataGrid></td>
				</tr>
				<tr>
					<td align=right colspan="6" style="height: 26px">
						<asp:ImageButton id="ImageButton1" runat="server" imageurl="../../images/icn_prev.gif" alternatetext="Previous" commandargument="prev" onClick="btnPrevNext_Click" />
						<asp:DropDownList id="DropDownList3" AutoPostBack="True" onSelectedIndexChanged="PagingIndexChanged" runat="server" />
			         	<asp:Imagebutton id="Imagebutton2" runat="server"  imageurl="../../images/icn_next.gif" alternatetext="Next" commandargument="next" onClick="btnPrevNext_Click" />
					</td>
				</tr>
				<tr>
					<td align="left" width="100%" ColSpan=6>
						<asp:ImageButton id=ImageButton3 onClick=NewBtn_Click imageurl="../../images/butt_new.gif" AlternateText="New" runat=server Visible="False"/>
						<asp:ImageButton id=ImageButton4 imageurl="../../images/butt_print.gif" AlternateText=Print visible=False runat="server"/>
					</td>
				</tr>
                <tr>
                    <td align="left" colspan="6" width="100%">
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="6" width="100%">                    
                        <igtab:UltraWebTab ID="UltraWebTab2" ThreeDEffect="False" TabOrientation="TopLeft"
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
                                <igtab:Tab Key="FFB" Text="PENERIMAAN TBS" Tooltip="PENERIMAAN TBS">                      
                                    <ContentPane>
                                        <table border="0" cellspacing="1" cellpadding="1" width="99%">
                                            <tr>
                                                <td colspan="5">
                                                    <div id="div2" style="height:300px;width:98%;overflow:auto;">				
                                                        <asp:DataGrid ID="dgFFBMonthly" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                           
                                                         ShowFooter=true
                                                            CellPadding="2" Width="100%"  >
                                                            <HeaderStyle CssClass="mr-h"/>
                                                            <ItemStyle CssClass="mr-l"/>
                                                            <AlternatingItemStyle CssClass="mr-r"/>	
                                                                                                                        
                                                          <Columns>
                                                               <asp:TemplateColumn HeaderText="s.d BULAN INI" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
                                                                <ItemStyle Width="14%"  />
                                                                    <ItemTemplate>
                                                                        <asp:Label Text= '<%# Container.DataItem("pSDBulan") %>' id="lblAccMonthTD" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>         
                                                                
                                                                                                                            
                                                                <asp:TemplateColumn HeaderText="KODE SUPPLIER" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
                                                                <ItemStyle Width="14%"  />
                                                                    <ItemTemplate>
                                                                        <asp:Label Text= '<%# Container.DataItem("CustomerCode") %>' id="lblCustCode" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>                                    

                                                                <asp:TemplateColumn HeaderText="NAMA SUPPLIER" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
                                                                    <ItemStyle Width="25%"  />
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# Container.DataItem("Name") %>' id="lblCustName" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>     

                                                                                                                                                                                              
                                                                <asp:TemplateColumn HeaderText="BRUTO (Kg)" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                   <ItemStyle Width="8%"  />
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# FormatNumber(Container.DataItem("FirstWeight"),0) %>' id="lblFweight" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>    

                                                                <asp:TemplateColumn HeaderText="TARRA (Kg)" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                   <ItemStyle Width="8%"  />
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# FormatNumber(Container.DataItem("SecondWeight"),0) %>' id="lblSweight" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>   

                                                                <asp:TemplateColumn HeaderText="GROSS (Kg)" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                    <ItemStyle Width="8%"  />
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# FormatNumber(Container.DataItem("GrossWeight"),0) %>' id="lblGweight" runat="server" />
                                                                    </ItemTemplate>

                                                                <FooterTemplate >
                                                                    <asp:Label ID=lblFFBSumGross runat=server />
                                                                    </FooterTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle Font-Size=Smaller BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />


                                                                </asp:TemplateColumn>   

                                                                <asp:TemplateColumn HeaderText="POT (Kg)" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                    <ItemStyle Width="8%"  />
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# FormatNumber(Container.DataItem("PotKG"),0) %>' id="lblPotweight" runat="server" />
                                                                    </ItemTemplate>
                                                                    
                                                                <FooterTemplate >
                                                                    <asp:Label ID=lblFFBSumPot runat=server />
                                                                    </FooterTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle Font-Size=Smaller BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />


                                                                </asp:TemplateColumn>   

                                                                <asp:TemplateColumn HeaderText="POT (%)" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                    <ItemStyle Width="8%"  />
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# FormatNumber(Container.DataItem("PotPsn"),2) %>' id="lblPotPsn" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>  
                                                                                                                                                                                                                                                    
                                                                <asp:TemplateColumn HeaderText="NETTO (Kg)" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                    <ItemStyle Width="8%"  />
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# FormatNumber(Container.DataItem("NetWeight"),0) %>' id="lblNweight" runat="server" />
                                                                    </ItemTemplate>

                                                                <FooterTemplate >
                                                                    <asp:Label ID=lblFFBSumNet runat=server />
                                                                    </FooterTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle Font-Size=Smaller BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />


                                                                </asp:TemplateColumn>   
                                                                                                                                                                                                                                                       
                                                            </Columns>
                                                        </asp:DataGrid>
                                                     </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan=5>&nbsp;</td>
                                            </tr>
                                        </table>
                                    </ContentPane>
                                </igtab:Tab>

                                <igtab:Tab Key="DISP" Text="PENJUALAN" Tooltip="PENJUALAN">                      
                                    <ContentPane>
                                        <table border="0" cellspacing="1" cellpadding="1" width="99%">
                                            <tr>
                                                <td colspan="5">
                                                    <div id="div3" style="height:300px;width:95%;overflow:auto;">				
                                                        <asp:DataGrid ID="dgSumDisp" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                         OnItemDataBound="dgLine_BindGrid" 
                                                         ShowFooter=true
                                                            CellPadding="2" Width="100%" >
                                                            <HeaderStyle CssClass="mr-h"/>
                                                            <ItemStyle CssClass="mr-l"/>
                                                            <AlternatingItemStyle CssClass="mr-r"/>	
                                                            <Columns>
                                                                <asp:TemplateColumn HeaderText="KODE CUSTOMER" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# Container.DataItem("CustomerCode") %>' id="lblCustCode" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>                                    

                                                                <asp:TemplateColumn HeaderText="NAMA CUSTOMER" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# Container.DataItem("Name") %>' id="lblCustName" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>     

                                                                <asp:TemplateColumn HeaderText="PRODUK" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# Container.DataItem("ProductCode") %>' id="lblproduct" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>     
                                                                                                                             
                                                                <asp:TemplateColumn HeaderText="BRUTO (Kg)" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# FormatNumber(Container.DataItem("FirstWeight"),0) %>' id="lblFweight" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>    

                                                                <asp:TemplateColumn HeaderText="TARRA (Kg)" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# FormatNumber(Container.DataItem("SecondWeight"),0) %>' id="lblSweight" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>   
                                                                                                                                                                                                                                                    
                                                                <asp:TemplateColumn HeaderText="NETTO (Kg)" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# FormatNumber(Container.DataItem("NetWeight"),0) %>' id="lblMonthlyNweight" runat="server" />
                                                                    </ItemTemplate>
                                                                <FooterTemplate >
                                                                    <asp:Label ID=lblSumMonthlyNweight runat=server />
                                                                    </FooterTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle Font-Size=Smaller BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />

                                                                </asp:TemplateColumn>   
                                                                                                                                                                                                                                                       
                                                            </Columns>
                                                        </asp:DataGrid>
                                                     </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan=5>&nbsp;</td>
                                            </tr>
                                        </table>
                                    </ContentPane>
                                 </igtab:Tab>                            
                            </Tabs>
                        </igtab:UltraWebTab>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="6" width="100%">
                    </td>
                </tr>
          
               </td>
               </tr>
               </table>
                </ContentPane>
               </igtab:Tab>
               
               
               </Tabs>                                  
               </igtab:UltraWebTab>
                
                
			</table>
			
                </div>
                </td>
            </tr>
            </table>
		</FORM>
	</body>
</html>
