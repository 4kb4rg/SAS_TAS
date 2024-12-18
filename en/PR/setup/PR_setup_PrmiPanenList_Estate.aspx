<%@ Page Language="vb" src="../../../include/PR_setup_PrmiPanenList_Estate.aspx.vb" Inherits="PR_setup_PrmiPanenList_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRSetup" src="../../menu/menu_prsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Basis Premi Panen List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
<%--		<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	</head>
	<body>
		<form id=frmLocList runat="server" class="main-modul-bg-app-list-pu">
    		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
    		<asp:label id="lblCode" visible="false" text=" Code" runat="server" />
			<asp:label id="SortExpression" Visible="False" Runat="server" />


<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuPRSetup id=MenuPRSetup runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>DAFTAR PREMI BASIS PANEN</strong><hr style="width :100%" />   
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
								<td width="20%" height="26" valign=bottom>Blok :<BR><asp:TextBox id=txtLastUpdate width=100% maxlength="20" runat="server"/></td>
								<td height="26" valign=bottom style="width: 20%">
                                    Periode :<BR>
                                    <asp:DropDownList ID="srcpmonth" runat="server" Width="50%">
                                        <asp:ListItem Value="1">January</asp:ListItem>
                                        <asp:ListItem Value="2">February</asp:ListItem>
                                        <asp:ListItem Value="3">March</asp:ListItem>
                                        <asp:ListItem Value="4">April</asp:ListItem>
                                        <asp:ListItem Value="5">May</asp:ListItem>
                                        <asp:ListItem Value="6">June</asp:ListItem>
                                        <asp:ListItem Value="7">July</asp:ListItem>
                                        <asp:ListItem Value="8">August</asp:ListItem>
                                        <asp:ListItem Value="9">September</asp:ListItem>
                                        <asp:ListItem Value="10">October</asp:ListItem>
                                        <asp:ListItem Value="11">November</asp:ListItem>
                                        <asp:ListItem Value="12">December</asp:ListItem>
                                    </asp:DropDownList><asp:DropDownList ID="srcpyear" runat="server" Width="40%">
                                    </asp:DropDownList></td>
								<td height="26" valign=bottom style="width: 21%"></td>
								<td width="15%" height="26" valign=bottom>Status :<BR>
									<asp:DropDownList id="ddlStatus" width=100% runat=server>
										<asp:ListItem value="0">All</asp:ListItem>
										<asp:ListItem value="1" selected>Active</asp:ListItem>
										<asp:ListItem value="2">Deleted</asp:ListItem>
									</asp:DropDownList>
								</td>
								
								<td width="10%" height="26" valign=bottom align=right><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
								</tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
						            <asp:DataGrid id=dghist
											AutoGenerateColumns=false width="100%" runat=server
											GridLines=none
											Cellpadding=2
											Pagerstyle-Visible=False
											AllowPaging="True" 
											Allowcustompaging="False" 
											OnPageIndexChanged="OnPageChanged"
											OnEditCommand="dghist_Edit"
											OnUpdateCommand="dghist_Update"
											OnCancelCommand="dghist_Cancel" 
                                        class="font9Tahoma">
								
							            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
											<Columns>						
											<asp:TemplateColumn ItemStyle-Width="5%" HeaderText="Blok">
											<ItemTemplate>
											<%# Container.DataItem("SubBlkCode") %>
											</ItemTemplate>
											<EditItemTemplate>
											<asp:TextBox ID="id1" runat="server" MaxLength="8" Enabled=false Text='<%# trim(Container.DataItem("SubBlkCode")) %>'
											Width="100%"></asp:TextBox>
											</EditItemTemplate>
											</asp:TemplateColumn>
								
											<asp:TemplateColumn ItemStyle-Width="3%" HeaderText="Bulan">
											<ItemTemplate>
											<%# Container.DataItem("AccMonth") %>
											</ItemTemplate>
											<EditItemTemplate>
											<asp:TextBox ID="id2" runat="server" MaxLength="2" Enabled=false Text='<%# trim(Container.DataItem("AccMonth")) %>'
											Width="100%"></asp:TextBox>
											</EditItemTemplate>
											</asp:TemplateColumn>

											<asp:TemplateColumn ItemStyle-Width="3%" HeaderText="Tahun">
											<ItemTemplate>
											<%# Container.DataItem("AccYear") %>
											</ItemTemplate>
											<EditItemTemplate>
											<asp:TextBox ID="id3" runat="server" MaxLength="4" Enabled=false Text='<%# trim(Container.DataItem("AccYear")) %>'
											Width="100%"></asp:TextBox>
											</EditItemTemplate>
											</asp:TemplateColumn>
											
											<asp:TemplateColumn ItemStyle-Width="5%" HeaderText="BJR">
											<ItemTemplate>
											<%# Container.DataItem("BJR") %>
											</ItemTemplate>
											<EditItemTemplate>
											<asp:TextBox ID="id4" runat="server" Text='<%# trim(Container.DataItem("BJR")) %>'
											Width="100%"></asp:TextBox>
											</EditItemTemplate>
											</asp:TemplateColumn>
											
											<asp:TemplateColumn ItemStyle-Width="10%" HeaderText="Basis Harian(KG/JJG)">
											<ItemTemplate>
											<%# Container.DataItem("Basis") %>
											</ItemTemplate>
											<EditItemTemplate>
											<asp:TextBox ID="id5" runat="server" Text='<%# trim(Container.DataItem("Basis")) %>'
											Width="100%"></asp:TextBox>
											</EditItemTemplate>
											</asp:TemplateColumn>
											
											<asp:TemplateColumn ItemStyle-Width="10%" HeaderText="Basis Hari Jumat (Jjg)">
											<ItemTemplate>
											    <%# Container.DataItem("Prod1Kg") %>
											</ItemTemplate>
											<EditItemTemplate>
											<asp:TextBox ID="id14" runat="server" Text='<%# trim(Container.DataItem("Prod1Kg")) %>'
											Width="100%"></asp:TextBox>
											</EditItemTemplate>
											</asp:TemplateColumn>
											
											<asp:TemplateColumn ItemStyle-Width="10%" HeaderText="Basis Harian(HA)">
											<ItemTemplate>
											<%# Container.DataItem("BasisHA") %>
											</ItemTemplate>
											<EditItemTemplate>
											<asp:TextBox ID="id5b" runat="server" Text='<%# trim(Container.DataItem("BasisHA")) %>'
											Width="100%"></asp:TextBox>
											</EditItemTemplate>
											</asp:TemplateColumn>
											
											<asp:TemplateColumn ItemStyle-Width="10%" HeaderText="Premi Over Basis(Rp)">
											<ItemTemplate>
											    <%# Container.DataItem("Ovrbasis") %>
											</ItemTemplate>
											<EditItemTemplate>
											<asp:TextBox ID="id6" runat="server" Text='<%# trim(Container.DataItem("Ovrbasis")) %>'
											Width="100%"></asp:TextBox>
											</EditItemTemplate>
											</asp:TemplateColumn>
											
											<asp:TemplateColumn ItemStyle-Width="10%" HeaderText="Premi Over Basis 2 (Rp)">
											<ItemTemplate>
											    <%# Container.DataItem("Ovrbasis2") %>
											</ItemTemplate>
											<EditItemTemplate>
											<asp:TextBox ID="id13" runat="server" Text='<%# trim(Container.DataItem("Ovrbasis2")) %>'
											Width="100%"></asp:TextBox>
											</EditItemTemplate>
											</asp:TemplateColumn>
											
											<asp:TemplateColumn ItemStyle-Width="10%" HeaderText="Produktivitas 1(Rp)">
											<ItemTemplate>
											    <%# Container.DataItem("Prod1Rp") %>
											</ItemTemplate>
											<EditItemTemplate>
											<asp:TextBox ID="id15" runat="server" Text='<%# trim(Container.DataItem("Prod1Rp")) %>'
											Width="100%"></asp:TextBox>
											</EditItemTemplate>
											</asp:TemplateColumn>
											
											<asp:TemplateColumn ItemStyle-Width="10%" HeaderText="Produktivitas 2(KG/JJG)">
											<ItemTemplate>
											    <%# Container.DataItem("Prod2Kg") %>
											</ItemTemplate>
											<EditItemTemplate>
											<asp:TextBox ID="id16" runat="server" Text='<%# trim(Container.DataItem("Prod2Kg")) %>'
											Width="100%"></asp:TextBox>
											</EditItemTemplate>
											</asp:TemplateColumn>
											
											<asp:TemplateColumn ItemStyle-Width="10%" HeaderText="Produktivitas 2(Rp)">
											<ItemTemplate>
											    <%# Container.DataItem("Prod2Rp") %>
											</ItemTemplate>
											<EditItemTemplate>
											<asp:TextBox ID="id17" runat="server" Text='<%# trim(Container.DataItem("Prod2Rp")) %>'
											Width="100%"></asp:TextBox>
											</EditItemTemplate>
											</asp:TemplateColumn>
											
											<asp:TemplateColumn ItemStyle-Width="10%" HeaderText="Pokok Tinggi(Rp)">
											<ItemTemplate>
											    <%# Container.DataItem("PokokRp") %>
											</ItemTemplate>
											<EditItemTemplate>
											<asp:TextBox ID="id18" runat="server" Text='<%# trim(Container.DataItem("PokokRp")) %>'
											Width="100%"></asp:TextBox>
											</EditItemTemplate>
											</asp:TemplateColumn>
											
											
										
											<asp:TemplateColumn ItemStyle-Width="10%" HeaderText="Tgl.Update">
											<ItemTemplate>
												<%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
											</ItemTemplate>
											<EditItemTemplate>
                                            <asp:TextBox ID="id7" runat="server" ReadOnly="TRUE" size="8" Text='<%# objGlobal.GetLongDate(Now()) %>'
												Visible="False"></asp:TextBox>
                                            </EditItemTemplate>
											</asp:TemplateColumn>
											
											<asp:TemplateColumn ItemStyle-Width="10%" HeaderText="DiUpdate">
											<ItemTemplate>
												<%# Container.DataItem("UpdateId") %>
											</ItemTemplate>
											<EditItemTemplate>
												<asp:TextBox ID="id8" runat="server" ReadOnly="TRUE" size="8" Text='<%# Session("SS_USERID") %>' Visible="False"></asp:TextBox>																				  
												<asp:TextBox ID="id9" runat="server" ReadOnly="TRUE" size="8" Text='<%# Container.DataItem("TotalArea") %>' Visible="False"></asp:TextBox>
												<asp:TextBox ID="id10" runat="server" ReadOnly="TRUE" size="8" Text='<%# Container.DataItem("TotalStand") %>' Visible="False"></asp:TextBox>
												<asp:TextBox ID="id11" runat="server" ReadOnly="TRUE" size="8" Text='<%# Container.DataItem("StdPerArea") %>' Visible="False"></asp:TextBox>
												<asp:TextBox ID="id12" runat="server" ReadOnly="TRUE" size="8" Text='<%# Container.DataItem("TransferBlok") %>' Visible="False"></asp:TextBox>
												<asp:TextBox ID="idT1" runat="server" ReadOnly="TRUE" Text='<%# Container.DataItem("ANT1") %>' Visible="False"></asp:TextBox>
												<asp:TextBox ID="idT2" runat="server" ReadOnly="TRUE" Text='<%# Container.DataItem("ANT2") %>' Visible="False"></asp:TextBox>
												<asp:TextBox ID="idT3" runat="server" ReadOnly="TRUE" Text='<%# Container.DataItem("ANT3") %>' Visible="False"></asp:TextBox>
												<asp:TextBox ID="idT4" runat="server" ReadOnly="TRUE" Text='<%# Container.DataItem("ANT4") %>' Visible="False"></asp:TextBox>
												<asp:TextBox ID="idT5" runat="server" ReadOnly="TRUE" Text='<%# Container.DataItem("ANT5") %>' Visible="False"></asp:TextBox>
												<asp:TextBox ID="ddlapl" runat="server" ReadOnly="TRUE" Text='<%# Container.DataItem("subtype") %>' Visible="False"></asp:TextBox>			
												<asp:TextBox ID="id20" runat="server" ReadOnly="TRUE" size="8" Text='<%# Container.DataItem("ProdStand") %>' Visible="False"></asp:TextBox>												
											</EditItemTemplate>
											</asp:TemplateColumn>
										
											<asp:TemplateColumn ItemStyle-Width=12%>
											<ItemTemplate >
											<asp:LinkButton ID="Edit" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
											</ItemTemplate>
											<EditItemTemplate>
											<asp:LinkButton ID="Update" runat="server" CommandName="Update" Text="Save"></asp:LinkButton>
											<asp:LinkButton ID="Cancel" runat="server" CausesValidation="False" CommandName="Cancel"  Text="Cancel"></asp:LinkButton>
											</EditItemTemplate>
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
							<table cellpadding="2" cellspacing="0" style="width: 100%">
								<tr>
									<td style="width: 100%">&nbsp;</td>
									<td><img height="18px" src="../../../images/btfirst.png" width="18px" class="button" /></td>
									<td><asp:ImageButton ID="btnPrev" runat="server" alternatetext="Previous" commandargument="prev" imageurl="../../../images/btprev.png" onClick="btnPrevNext_Click" /></td>
									<td><asp:DropDownList ID="lstDropList" runat="server" AutoPostBack="True" onSelectedIndexChanged="PagingIndexChanged" /></td>
									<td><asp:ImageButton ID="btnNext" runat="server" alternatetext="Next" commandargument="next" imageurl="../../../images/btnext.png" onClick="btnPrevNext_Click" /></td>
									<td><img height="18px" src="../../../images/btlast.png" width="18px" class="button" /></td>
								</tr>
							</table>
							</td>
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



		</FORM>
	</body>
</html>
