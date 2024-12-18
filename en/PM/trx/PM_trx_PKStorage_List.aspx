<%@ Page Language="vb" trace="false" codefile="../../../include/PM_Trx_PKStorage_List.aspx.vb" Inherits="PM_Trx_PKStorage_List" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPDTrx" src="../../menu/menu_PDtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v7.3" Namespace="Infragistics.WebUI.UltraWebTab"
    TagPrefix="igtab" %>
<html>
	<head>
		<title>PK Storage Transaction List</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
	    <form id=frmMain class="main-modul-bg-app-list-pu" runat=server >
			<asp:Label id=SortExpression visible=false runat=server />
			<asp:Label id=SortCol visible=false runat=server />
			<asp:Label id=lblErrMessage visible=false text="Error while initiating component." runat=server />

		<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
				<tr>
					<td colspan="6">
						<UserControl:MenuPDTrx id=MenuPDTrx runat=server />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">

			    <table border=0 cellspacing=1 cellpadding=1 width=100%  class="font9Tahoma">
				 
				<tr>
					<td class="mt-h" colspan="3" NOWRAP>PK STORAGE LIST</td>
					<td colspan="3" align=right><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>
				<tr>
					<td style="background-color:#FFCC00" >
						<table width="100%" cellspacing="0" cellpadding="3" border="0" align="center" class="font9Tahoma">
							<tr class="mb-t">
								<td width="25%">Bunker Silo Code:<BR>
									<asp:TextBox id=srchTransDate width=50% maxlength="10" runat="server"/>
								  <BR>
									<asp:label id=lblDate Text ="Date Entered should be in the format " forecolor=red Visible = false Runat="server"/> 
									<asp:label id=lblFmt  forecolor=red Visible = false Runat="server"/> 
								</td>
								<td width="15%">
                                    Period :<br />
                                    <asp:DropDownList ID="lstAccMonth" runat="server" Width="30%">
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
                                    </asp:DropDownList>&nbsp;<asp:DropDownList ID="lstAccYear" runat="server" Width="50%">
                                    </asp:DropDownList></td>
								<td width="30%">&nbsp;</td>														
								<td width="20%"><asp:TextBox id=srchUpdateBy width="20%" maxlength="128" runat="server" Visible="False"/>
                                    <asp:TextBox id=srchStorageArea width="20%" maxlength="8" runat="server" Visible="False"/></td>
								<td width="10%" height="26" valign=bottom align=right><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click CssClass="button-small" runat="server"/></td>
							</tr>
						</table>
					</td>
				</tr>
				
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
                            <igtab:Tab Key="Drying" Text="DRYING & NUT SILO" Tooltip="DRYING & NUT SILO">                      
                            <ContentPane>
                         <table border="0" cellspacing="1" cellpadding="1" width="99%">                                    
				            <tr>	
					        <td colspan=6 style="width:1024px">
					            <div id="div4" style="height:350px;width:95%;overflow:auto;">								
						            <asp:DataGrid id=EventData
							            AutoGenerateColumns=false width=100% runat=server
							            GridLines=none 
							            Cellpadding=2 
							            AllowPaging=False 
							            Allowcustompaging=False 
						 
							            OnPageIndexChanged=OnPageChanged 
							            Pagerstyle-Visible=False 
							            AllowSorting=True>
							            <HeaderStyle CssClass="mr-h" />
							            <ItemStyle CssClass="mr-l" />
							            <AlternatingItemStyle CssClass="mr-r" />
							            <Columns>
								            <asp:TemplateColumn HeaderText="Tgl.Produksi">
									            <ItemTemplate>
										            <%#objGlobal.GetLongDate(Container.DataItem("DateProd"))%>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="Drying Silo">
									            <ItemTemplate>
										            <%#Container.DataItem("DriyingSiloCode")%>
									            </ItemTemplate>
								            </asp:TemplateColumn>
             
 								            <asp:TemplateColumn HeaderText="Sounding(Cm)">
									            <ItemTemplate>
										            <%#FormatNumber(Container.DataItem("SoundingCM"), 2)%>
									            </ItemTemplate>
								            </asp:TemplateColumn>

 								            <asp:TemplateColumn HeaderText="Sounding(mm)">
									            <ItemTemplate>
										            <%#FormatNumber(Container.DataItem("Soundingmm"), 2)%>
									            </ItemTemplate>
								            </asp:TemplateColumn>
 
 								            <asp:TemplateColumn HeaderText="Stock Akhir (Kg)">
									            <ItemTemplate>
										            <%#FormatNumber(Container.DataItem("Kg"), 2)%>
									            </ItemTemplate>
								            </asp:TemplateColumn>
            																																									
							            </Columns>
						            </asp:DataGrid>
						        </div>
					        </td>
				            </tr>
				           </table>
				             </ContentPane>
                            </igtab:Tab>
                                  
                            <igtab:Tab Key="Bulk" Text="BUNKER SILO" Tooltip="BULKING">                      
                            <ContentPane>
                            <table border="0" cellspacing="1" cellpadding="1" width="99%">
				            <tr>
				            <br />
					            <td colspan=6 style="width:1024px">
					            <div id="div1" style="height:350px;width:95%;overflow:auto;">								
						            <asp:DataGrid id=EvenBulk
							            AutoGenerateColumns=false width=100% runat=server
							            GridLines=none 
							            Cellpadding=2 
							            AllowPaging=False 
							            Allowcustompaging=False 
							            Pagesize=15 
					                    Pagerstyle-Visible=False 
							            AllowSorting=True>
							            <HeaderStyle CssClass="mr-h" />
							            <ItemStyle CssClass="mr-l" />
							            <AlternatingItemStyle CssClass="mr-r" />
										<Columns>
								            <asp:TemplateColumn HeaderText="Tgl.Produksi">
									            <ItemTemplate>
										            <%#objGlobal.GetLongDate(Container.DataItem("DateProd"))%>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="Drying Silo">
									            <ItemTemplate>
										            <%#Container.DataItem("DriyingSiloBulkCode")%>
									            </ItemTemplate>
								            </asp:TemplateColumn>

											<asp:TemplateColumn HeaderText="Sounding(Cm)">
									            <ItemTemplate>
										            <%#FormatNumber(Container.DataItem("Sounding"), 2)%>
									            </ItemTemplate>
								            </asp:TemplateColumn>

 								            <asp:TemplateColumn HeaderText="Adjustment">
									            <ItemTemplate>
										            <%#FormatNumber(Container.DataItem("PK_Adjust"), 2)%>
									            </ItemTemplate>
								            </asp:TemplateColumn>

 								            <asp:TemplateColumn HeaderText="Stock Akhir (Kg)">
									            <ItemTemplate>
										            <%#FormatNumber(Container.DataItem("Kg"), 2)%>
									            </ItemTemplate>
								            </asp:TemplateColumn>																										
							            </Columns>
						            </asp:DataGrid>
						        </div>
					        </td>
				            </tr>
				            </table>
				             </ContentPane>
                            </igtab:Tab>
                             
                            <igtab:Tab Key="GUDANG" Text="GUDANG SILO" Tooltip="GUDANG">                      
                            <ContentPane>
                            <table border="0" cellspacing="1" cellpadding="1" width="99%">
				            <tr>
				            <br />
					            <td colspan=6 style="width:1024px">
					            <div id="div2" style="height:350px;width:95%;overflow:auto;">								
						            <asp:DataGrid id=dgGudang
							            AutoGenerateColumns=false width=100% runat=server
							            GridLines=none 
							            Cellpadding=2 
							    
							            Allowcustompaging=False 
							            
					                    Pagerstyle-Visible=False 
							            AllowSorting=True>
							            <HeaderStyle CssClass="mr-h" />
							            <ItemStyle CssClass="mr-l" />
							            <AlternatingItemStyle CssClass="mr-r" />
							            <Columns>
								            <asp:TemplateColumn HeaderText="Tgl.Produksi">
									            <ItemTemplate>
										            <%#objGlobal.GetLongDate(Container.DataItem("DateProd"))%>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="Keterangan">
									            <ItemTemplate>
										            <%#Container.DataItem("Description")%>
									            </ItemTemplate>
								            </asp:TemplateColumn>

 								            <asp:TemplateColumn HeaderText="Ukuran">
									            <ItemTemplate>
										            <%#FormatNumber(Container.DataItem("Ukuran"), 2)%>
									            </ItemTemplate>
								            </asp:TemplateColumn>

 								            <asp:TemplateColumn HeaderText="Qty">
									            <ItemTemplate>
										            <%#FormatNumber(Container.DataItem("Qty"), 2)%>
									            </ItemTemplate>
								            </asp:TemplateColumn>                                            
                                                                                                     
 								            <asp:TemplateColumn HeaderText="Stock Akhir (Kg)">
									            <ItemTemplate>
										            <%#FormatNumber(Container.DataItem("Total"), 2)%>
									            </ItemTemplate>
								            </asp:TemplateColumn>

 								            <asp:TemplateColumn HeaderText="FFA">
									            <ItemTemplate>
										            <%#FormatNumber(Container.DataItem("Pecahan"), 2)%>
									            </ItemTemplate>
								            </asp:TemplateColumn>

 								            <asp:TemplateColumn HeaderText="Moisture">
									            <ItemTemplate>
										            <%#FormatNumber(Container.DataItem("Moisture"), 2)%>
									            </ItemTemplate>
								            </asp:TemplateColumn>
 
 								            <asp:TemplateColumn HeaderText="Dirt">
									            <ItemTemplate>
										            <%#FormatNumber(Container.DataItem("Dirt"), 2)%>
									            </ItemTemplate>
								            </asp:TemplateColumn>
                																																									
							            </Columns>
						            </asp:DataGrid>
						        </div>
					        </td>
				            </tr>
				            </table>
				             </ContentPane>
                            </igtab:Tab>                             
                        </Tabs>
                    </igtab:UltraWebTab>
                <tr>
                    <td align="right" colspan="6" style="text-align: left">
                        <table>
                            <tr>
                                <td style="width: 138px">
                                    Nut Dilantai (Kg) :</td>
                                <td style="width: 165px">
                                    <asp:TextBox id=TextBox1 width="100%" maxlength="10" runat="server"/></td>
                                <td style="width: 233px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 138px">
                                    Total&nbsp; (Kg):</td>
                                <td style="width: 165px">
                                    <asp:TextBox id=TextBox2 width="100%" maxlength="10" runat="server"/></td>
                                <td style="width: 233px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 138px">
                                </td>
                                <td style="width: 165px">
                                </td>
                                <td style="width: 233px">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
				<tr>
					<td align=right colspan="6">
						<asp:ImageButton id="btnPrev" runat="server" imageurl="../../images/icn_prev.gif" alternatetext="Previous" commandargument="prev" onClick="btnPrevNext_Click" Visible="False" />
						<asp:DropDownList id="lstDropList" AutoPostBack="True" onSelectedIndexChanged="PagingIndexChanged" runat="server" Visible="False" />
			         	<asp:Imagebutton id="btnNext" runat="server"  imageurl="../../images/icn_next.gif" alternatetext="Next" commandargument="next" onClick="btnPrevNext_Click" Visible="False" />
					</td>
				</tr>
				<tr>
					<td align="left" width="100%" ColSpan=6>
						<asp:ImageButton id=NewBtn onClick=NewBtn_Click imageurl="../../images/butt_new.gif" AlternateText="New" runat=server Visible="False"/>
						<asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print visible=false runat="server"/>
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
