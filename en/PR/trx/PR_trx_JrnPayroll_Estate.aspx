<%@ Page Language="vb" src="../../../include/PR_trx_JrnPayroll_Estate.aspx.vb" Inherits="PR_trx_JrnPayroll_Estate"%> 
<%@ Register TagPrefix="UserControl" Tagname="MenuPRTrx" src="../../menu/menu_prtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v7.3" Namespace="Infragistics.WebUI.UltraWebTab"
    TagPrefix="igtab" %>
	
<html>
	<head>
		<title>Buku Kerja Mandor List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body onload="javascript:document.frmMain.txtEmpName.focus();">
	    <form id=frmMain class="main-modul-bg-app-list-pu" runat=server >
                              <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 
			<asp:Label id=SortExpression visible=false runat=server />
			<asp:Label id=SortCol visible=false runat=server />
			
			<table border=0 cellspacing=0 cellpadding=2 width=99% class="font9Tahoma">
				<tr>
					<td colspan=6><UserControl:MenuPRTrx id=MenuPRTrx runat=server /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="3"><strong>ANALISA JURNAL PAYROLL</strong> </td>
					<td colspan="3" align=right><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6>
                    <hr style="width :100%" />
                    </td>
				</tr>
				<tr>
					<td colspan=6 width=100% class="mb-c" style="height: 50px">
						<table width="100%" cellspacing="0" cellpadding="1" border="0" class="font9Tahoma">
							<tr class="mb-t">
	     					    <td height="26" width="10%">No.Jurnal :<BR><asp:TextBox id=txtBKM width=100% maxlength="45" runat="server"/></td>
	     						<td height="26" width="10%">Blok :<BR><asp:TextBox id=txtBlok width=100% runat="server"/></td>
                                <td height="26" width="15%">Job :<BR><asp:TextBox id=txtJob width=100% runat="server"/></td>
                                <td height="26" width="15%" >
                                    Periode : <br />
                                    <asp:DropDownList ID="ddlEmpMonth" runat="server" Width="73%">
                                        <asp:ListItem Value="01">January</asp:ListItem>
                                        <asp:ListItem Value="02">February</asp:ListItem>
                                        <asp:ListItem Value="03">March</asp:ListItem>
                                        <asp:ListItem Value="04">April</asp:ListItem>
                                        <asp:ListItem Value="05">May</asp:ListItem>
                                        <asp:ListItem Value="06">June</asp:ListItem>
                                        <asp:ListItem Value="07">July</asp:ListItem>
                                        <asp:ListItem Value="08">August</asp:ListItem>
                                        <asp:ListItem Value="09">September</asp:ListItem>
                                        <asp:ListItem Value="10">October</asp:ListItem>
                                        <asp:ListItem Value="11">November</asp:ListItem>
                                        <asp:ListItem Value="12">December</asp:ListItem>
                                    </asp:DropDownList><asp:DropDownList id="ddlyear" width="26%" runat=server></asp:DropDownList>
								</td>
								<td height="26" width="10%"  valign=bottom align=right><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click class="button-small"  runat="server"/>
								</td>
							</tr>
						</table>
				   </td>
				</tr>
				<tr>
					<td style="height: 24px;" colspan="5">
						<igtab:UltraWebTab ID="TABBK" ThreeDEffect="False" TabOrientation="TopLeft"
							SelectedTab="0" Font-Names="Tahoma" Font-Size="8pt" runat="server">
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
								<igtab:Tab Key="PAYROLL" Text="ANALISA JURNAL PAYROLL" Tooltip="ANALISA JURNAL PAYROLL">
									<ContentPane>
										<table border="0" cellspacing="1" cellpadding="1" width="73%">
											<tr>
												<td colspan="5">
													<div id="div1" style="height:500px;width:1040;overflow:auto;">					
														<asp:DataGrid id=dgEmpList
															AutoGenerateColumns=False width=100% runat=server
															GridLines=None 
															Cellpadding=2 
															AllowPaging=True 
															Pagesize=15 
															OnPageIndexChanged=OnPageChanged 
															Pagerstyle-Visible=False 
															OnItemCommand=BKMLink_Click  
															OnDeleteCommand=JOBLink_Click 
															OnSortCommand=Sort_Grid  
															AllowSorting=true CssClass="font9Tahoma">
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
																<asp:TemplateColumn HeaderText="No. Jurnal">
																	<ItemTemplate>
																		<asp:Label id=lblNoJurnal text='<%# Container.DataItem("JournalID") %>'  runat=server/>
																	</ItemTemplate>
																</asp:TemplateColumn>
																
																<asp:TemplateColumn HeaderText="No. BKM">
																	<ItemTemplate>
																	  <asp:LinkButton id=lnkBKM  Text='<%# Container.DataItem("BKMCode") %>' runat=server /> 
																	  <asp:HiddenField Id=hidbkm Value='<%# Container.DataItem("BKMCode") %>' runat=server />
																	</ItemTemplate>
																</asp:TemplateColumn>
														 
																<asp:TemplateColumn HeaderText="Tanggal">
																	<ItemTemplate>
																		<asp:Label id=lblDate text='<%# objGlobal.GetLongDate(Container.DataItem("BKMDate")) %>'  runat=server/>
																	</ItemTemplate>
																</asp:TemplateColumn>
																
																<asp:TemplateColumn HeaderText="Divisi">
																	<ItemTemplate>
																		<asp:Label id=lblEmpDiv text='<%# Container.DataItem("DivCode") %>'  runat=server/>
																	</ItemTemplate>
																</asp:TemplateColumn>
																
																<asp:TemplateColumn HeaderText="Blok">
																	<ItemTemplate>
																		<asp:Label id=lblBlok text='<%# Container.DataItem("SubBlkCode") %>'  runat=server/>
																	</ItemTemplate>
																</asp:TemplateColumn>
																
																<asp:TemplateColumn HeaderText="Kode Aktifiti">
																	<ItemTemplate>
																		<asp:LinkButton id=lnkJOB CommandName="Delete" CausesValidation=False Text='<%# Container.DataItem("CodeAloJob") %>' runat=server /> 
																		<asp:HiddenField Id=hidjob Value='<%# Container.DataItem("CodeAloJob") %>' runat=server />
																		<asp:Label id=lblJOBid text='<%# Container.DataItem("CodeAloJob") %>' Visible=false runat=server/>
																	</ItemTemplate>
																</asp:TemplateColumn>
																
																<asp:TemplateColumn HeaderText="Deskripsi">
																	<ItemTemplate>
																		<asp:Label id=lblDeskripsi text='<%# Container.DataItem("Description") %>'  runat=server/>
																	</ItemTemplate>
																</asp:TemplateColumn>
																
																<asp:TemplateColumn>
																	<ItemTemplate>
																		<asp:Label id=lblBKMid text='<%# Container.DataItem("BKMCode") %>'  Visible=false runat=server/>
																	</ItemTemplate>
																	<ItemStyle HorizontalAlign="Center" />
																 
																</asp:TemplateColumn>											
															</Columns>
															 <PagerStyle Visible="False" />
														</asp:DataGrid>
													</div>
												</td>
											</tr>
											
											<tr>					
												<td align="left" width=50% style="height: 26px">
													<asp:ImageButton id=PreviewBtn OnClick="PreviewBtn_Click" imageurl="../../images/butt_print_preview.gif" AlternateText="Preview" runat="server"/>
												</td>
												
												<td align=right width=50% style="height: 26px">
													<asp:ImageButton id="btnPrev" runat="server" imageurl="../../images/icn_prev.gif" alternatetext="Previous" commandargument="prev" onClick="btnPrevNext_Click" />
													<asp:DropDownList id="lstDropList" AutoPostBack="True" onSelectedIndexChanged="PagingIndexChanged" runat="server" />
													<asp:Imagebutton id="btnNext" runat="server"  imageurl="../../images/icn_next.gif" alternatetext="Next" commandargument="next" onClick="btnPrevNext_Click" />
													<asp:Label id=lblCurrentIndex visible=false text=0 runat=server/>
													<asp:Label id=lblPageCount visible=false text=1 runat=server/>
												</td>
											</tr>
										</table>
								</ContentPane>
							</igtab:Tab>  
							
							<igtab:Tab Key="ALOKASI" Text="JURNAL ALOKASI PAYROLL" Tooltip="JURNAL ALOKASI PAYROLL">
                                <ContentPane>
                                    <table width="75%" cellspacing="0" cellpadding="0" border="0" align="center">
                                        <tr>
                                            <td colspan="5">
                                                <div id="div2" style="height:300px;width:1040;overflow:auto;">	
                                                    <asp:DataGrid id=dgJrnAlokasi
                                                        AutoGenerateColumns=false width="90%" runat=server CssClass="font9Tahoma"
                                                        GridLines=none Cellpadding=2>
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
															<asp:TemplateColumn HeaderText="No. Jurnal" HeaderStyle-Font-Bold=true >
                                                                <ItemTemplate>
                                                                    <asp:Label Text=<%# Container.DataItem("JournalID") %> id="lblJournalID" runat="server" /><br>
																	<asp:Label Text=<%# Container.DataItem("Description") %> id="lblDescRec" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>															
                                                            <asp:TemplateColumn HeaderText="KETERANGAN" HeaderStyle-Font-Bold=true>
                                                                <ItemTemplate>
                                                                    <asp:Label Text=<%# Container.DataItem("DescrDet") %> id="lblDescrDet" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
															<asp:TemplateColumn ItemStyle-Width="10%" HeaderText="AKUN" HeaderStyle-Font-Bold=true >
                                                                <ItemTemplate>
                                                                    <asp:Label Text=<%# Container.DataItem("AccCode") %> id="lblAccCodeRec" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
															<asp:TemplateColumn ItemStyle-Width="10%" HeaderText="BLOK" HeaderStyle-Font-Bold=true >
                                                                <ItemTemplate>
                                                                    <asp:Label Text=<%# Container.DataItem("BlkCode") %> id="lblBlkCode" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="JUMLAH" HeaderStyle-Font-Bold=true>
                                                                <HeaderStyle HorizontalAlign="Center" /> 
								                                <ItemStyle HorizontalAlign="Right"/> 
                                                                <ItemTemplate>
                                                                    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Total"), 2), 2) %> id="lblTotal" runat="server" />
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
		
				
			</table>
			<asp:Label id=lblErrMessage visible=false text="Error while initiating component." runat=server />
        </div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</html>
