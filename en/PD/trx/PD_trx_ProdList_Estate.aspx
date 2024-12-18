<%@ Page Language="vb" src="../../../include/PD_trx_ProdList_Estate.aspx.vb" Inherits="PD_trx_ProdList_Estate"%> 
<%@ Register TagPrefix="UserControl" Tagname="MenuPDTrx" src="../../menu/menu_PDtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<html>
	<head>
		<title>Daftar Pengiriman TBS</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
<%--		<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	</head>
	<body onload="javascript:document.frmMain.txtEmpCode.focus();">
	    <form id=frmMain runat=server class="main-modul-bg-app-list-pu">
			<asp:Label id=SortExpression visible=false runat=server />
			<asp:Label id=SortCol visible=false runat=server />
			<asp:Label id=lblErrMessage visible=false text="Error while initiating component." runat=server />



		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuPDTrx id=MenuPDTrx runat=server />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>DAFTAR PENGIRIMAN TBS</strong><hr style="width :100%" />   
                            </td>
                            
						</tr>
                        <tr>
                            <td align="right"><asp:label id="lblTracker" runat="server" /></td> 
                        </tr>
				        <tr>
					       <%-- <td colspan=6><hr size="1" noshade></td>--%>
				        </tr>
						<tr>
							<td style="background-color:#FFCC00" >
							<table cellpadding="4" cellspacing="0" style="width: 100%">
								<tr class="font9Tahoma">
								<td height="26" width="15%">No.SPB :<BR><asp:TextBox id=txtEmpCode width=100% maxlength="20" runat="server" /></td>
								<td height="26" width="15%">Divisi :<BR />
                                    <asp:DropDownList ID="ddldivisi" runat="server" Width="100%">
                                    </asp:DropDownList>
								</td>
								<td height="26" width="15%">Pabrik :<BR><asp:DropDownList id="ddlEmpDiv" width=100% runat=server /></td>
                                <td height="26" width="25%">Periode :<BR />
                                    <asp:DropDownList ID="ddlEmpMonth" runat="server" Width="40%">
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
                                    </asp:DropDownList>
								<asp:DropDownList id="ddlyear" width="20%" runat=server></asp:DropDownList></td>
								<td height="26" width="10%"  valign=bottom align=right><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/>
								</td>
								</tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
						            <asp:DataGrid id=dgEmpList
							                AutoGenerateColumns=False width=100% runat=server
							                GridLines=None 
							                Cellpadding=2 
							                AllowPaging=False 
							                Pagerstyle-Visible=False 
							                OnSortCommand=Sort_grid
						                    AllowSorting=True
							                ShowFooter=True 
							                OnItemDataBound=KeepRunningSum 
			
                                                        class="font9Tahoma">
								
							                            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
							
							                <Columns>
														
								
													 
								                <asp:TemplateColumn HeaderText="Tanggal" SortExpression="SPBDate">
									                <ItemTemplate>
										                <asp:Label id=lblDate text='<%# objGlobal.GetLongDate(Container.DataItem("SPBDate")) %>'  runat=server/>
									                </ItemTemplate>
								                </asp:TemplateColumn>
								
								                <asp:BoundColumn Visible=False HeaderText="No.SPB" DataField="SPBCode" />
								                <asp:HyperLinkColumn HeaderText="No.SPB" 
													                 SortExpression="A.SPBCode" 
													                 DataNavigateUrlField="SPBCode" 
													                 DataNavigateUrlFormatString="PD_trx_ProdDet_Estate.aspx?SPB={0}" 
													                 DataTextField="SPBCode" />
							
								                <asp:TemplateColumn HeaderText="No.Ref">
									                <ItemTemplate>
										                <asp:Label id=lblrefno text='<%# Container.DataItem("RefNo") %>'  runat=server/>
									                </ItemTemplate>
								                </asp:TemplateColumn>
								
								                <asp:TemplateColumn HeaderText="Divisi">
									                <ItemTemplate>
										                <asp:Label id=lbldiv text='<%# Container.DataItem("Divisi") %>'  runat=server/>
									                </ItemTemplate>
								                </asp:TemplateColumn>
													
								                <asp:TemplateColumn HeaderText="No.Polisi">
									                <ItemTemplate>
										                <asp:Label id=lblEmpName text='<%# Container.DataItem("NoPolisi") %>'  runat=server/>
									                </ItemTemplate>
								                </asp:TemplateColumn>
								
								                  <asp:TemplateColumn HeaderText="Supir">
                                	                <ItemTemplate>
										                <asp:Label id=lblEmpDiv text='<%# Container.DataItem("Supir") %>'  runat=server/>
									                </ItemTemplate>
								                </asp:TemplateColumn>
								
                                                <asp:TemplateColumn HeaderText="Pabrik">
                                	                <ItemTemplate>
										                <asp:Label id=lblEmpType text='<%# Container.DataItem("MillDesc") %>'  runat=server/>
										                <asp:Label id=lblMType text='<%# Container.DataItem("MillType") %>'  runat=server visible=false/>
									                </ItemTemplate>
								                </asp:TemplateColumn>
								
                                                <asp:TemplateColumn HeaderText="Bruto"  HeaderStyle-HorizontalAlign="Right">
                                	                <ItemTemplate>
										                <asp:Label id=lblBruto text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("WB_Bruto2"),2),0) %>'  runat=server/>
									                </ItemTemplate>
								                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateColumn>
								
								                <asp:TemplateColumn HeaderText="Tarra"  HeaderStyle-HorizontalAlign="Right">
                                	                <ItemTemplate>
										                <asp:Label id=lblTara text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("WB_Tara2"),2),0) %>'  runat=server/>
									                </ItemTemplate>
								                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateColumn>
								
								                <asp:TemplateColumn HeaderText="Tot. Potongan"  HeaderStyle-HorizontalAlign="Right">
                                	                <ItemTemplate>
										                <asp:Label id=lblPot text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PotonganKg"),2),0) %>'  runat=server/>
									                </ItemTemplate>
								                    <ItemStyle HorizontalAlign="Right" />
									                <FooterTemplate >
									                    <asp:Label ID=lbTotal runat=server />
									                </FooterTemplate>
									                <FooterStyle BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />
                                                </asp:TemplateColumn>

								                <asp:TemplateColumn HeaderText="Netto Final"  HeaderStyle-HorizontalAlign="Right">
                                	                <ItemTemplate>
										                <asp:Label id=lblNetto text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("WB_NettoFinal"),2),0) %>'  runat=server/>
									                </ItemTemplate>
								                    <ItemStyle HorizontalAlign="Right" />
									                <FooterTemplate >
									                    <asp:Label ID=lbTotal runat=server />
									                </FooterTemplate>
									                <FooterStyle BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />
                                                </asp:TemplateColumn>
								
								                <asp:TemplateColumn HeaderText="Tot.JJG"  HeaderStyle-HorizontalAlign="Right">
                                	                <ItemTemplate>
										                <asp:Label id=lbljjg text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("TotalJJG"),2),0) %>'  runat=server/>
									                </ItemTemplate>
								                    <ItemStyle HorizontalAlign="Right" />
									                <FooterTemplate >
									                    <asp:Label ID=lbTotaljjg runat=server />
									                </FooterTemplate>
									                <FooterStyle BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />
                                                </asp:TemplateColumn>
								
								                <asp:TemplateColumn HeaderText="Tot.Brd"  HeaderStyle-HorizontalAlign="Right">
                                	                <ItemTemplate>
										                <asp:Label id=lblbrd text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("brondolan"),2),0) %>'  runat=server/>
									                </ItemTemplate>
								                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateColumn>
                                
								                <asp:TemplateColumn HeaderText="BJR"  HeaderStyle-HorizontalAlign="Right">
                                	                <ItemTemplate>
										                <asp:Label id=lblbjr text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("BJR"),2),2) %>'  runat=server/>
									                </ItemTemplate>
								                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateColumn>
								
								                <asp:TemplateColumn HeaderText="Tgl update" HeaderStyle-HorizontalAlign="Center">
									                <ItemTemplate>
										                <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									                </ItemTemplate>
									                <ItemStyle HorizontalAlign="Center" />
								                </asp:TemplateColumn>
								
								                <asp:TemplateColumn HeaderText="Diupdate" HeaderStyle-HorizontalAlign="Center" >
									                <ItemTemplate>
										                <%# Container.DataItem("uName") %>
									                </ItemTemplate>
								                <ItemStyle HorizontalAlign="Center" />
								                </asp:TemplateColumn>
												
							                </Columns>
                		                </asp:DataGrid>
                                    </td>
                                    </tr>
								</table>
							</td>
						</tr>
						<tr>
							<td>
							    &nbsp;</td>
						</tr>
						  
						<tr>
							<td>
					            <asp:ImageButton id=NewEmpBtn OnClick="NewEmpBtn_Click" imageurl="../../images/butt_new.gif" AlternateText="New SPB" runat="server"/>
						        <asp:ImageButton ID="GenBtn" OnClick="GenBtn_Click" AlternateText="  Generate BJR/KG Blok  " CausesValidation="False" ImageUrl="../../images/butt_generate.gif" runat="server" hidden/>
						        <%--<asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print visible=false/>--%>
						        <asp:Label id=lblRedirect visible=false runat=server/>
							</td>
						</tr>
                        <tr>
                            <td>
 					            &nbsp;</td>
                        </tr>
					</table>
				</div>
				</td>
		        <table cellpadding="0" cellspacing="0" style="width: 20px">
			        <tr>
				        <td>&nbsp;</td>
			        </tr>
		        </table>
				</td>
			</tr>
		</table>



		</form>
	</body>
</html>
