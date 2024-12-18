<%@ Page Language="vb" Trace="False" src="../../../include/GL_trx_NotaDebet_list.aspx.vb" Inherits="GL_trx_NotaDebet_list" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLTrx" src="../../menu/menu_GLTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Nota Debet</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
<%--	<Preference:PrefHdl id=PrefHdl runat="server" />--%>
		<body>
		    <form runat="server" class="main-modul-bg-app-list-pu">
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="sortcol" Visible="False" Runat="server" />

		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuGLTrx id=menuGL runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>NOTA DEBET</strong><hr style="width :100%" />   
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
								<td width="10%" height="26">Doc ID :<BR><asp:TextBox id="srchDocID" width=100% maxlength="32" CssClass="fontObject" runat="server"/></td>
								<td width="20%" height="26">Description :<BR><asp:TextBox id="srchDescr" width=100% maxlength="128" CssClass="fontObject" runat="server"/></td>
								<td width="15%" height="26">Location :<BR><asp:DropDownList id="ddlLocation" width=100% CssClass="fontObject" runat="server"/></td>
								<td width=8%>Period :<BR>
								    <asp:DropDownList id="lstAccMonth" width=100% CssClass="fontObject" runat=server>
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
								<td width=10%><BR>
								    <asp:DropDownList id="lstAccYear" width=100% CssClass="fontObject" runat=server>
									</asp:DropDownList>
								<td width="10%" height="26" valign=bottom align=right><asp:Button Text="Search"  id="SearchBtn" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
								</tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
					    <asp:DataGrid id="dgTx"
						    AutoGenerateColumns="false" width="100%" runat="server"
						    GridLines="Both"
						    Cellpadding = "2"
						    Allowcustompaging="False"
						    Pagerstyle-Visible="False"
						    OnSortCommand="Sort_Grid" 
						    OnItemDataBound="dgTx_BindGrid" 
						    AllowSorting="True"  >
<HeaderStyle CssClass="mr-h"/>
<ItemStyle CssClass="mr-l"/>
<AlternatingItemStyle CssClass="mr-r"/>					
					    <Columns>
						    <asp:TemplateColumn HeaderText="No." ItemStyle-Width="3%" >
							    <ItemTemplate>
								    <asp:label id="lblNoUrut" Text=<%# Container.DataItem("NoUrut")%> Runat="server"></asp:label>
								    <asp:label id="lblLocFrom" Text=<%# Container.DataItem("LocFrom")%> Visible=false Runat="server"></asp:label>
								    <asp:label id="lblLocTo" Text=<%# Container.DataItem("LocTo")%> Visible=false Runat="server"></asp:label>
							    </ItemTemplate>
						    </asp:TemplateColumn>	
						    <asp:TemplateColumn HeaderText="Tanggal" ItemStyle-Width="6%" >
							    <ItemTemplate>
								    <asp:label id="lblDocDateFrom" Text=<%# objGlobal.GetLongDate(Container.DataItem("DocDateFrom")) %> Runat="server"></asp:label>
							    </ItemTemplate>
						    </asp:TemplateColumn>	
						    <asp:TemplateColumn HeaderText="No. Bukti" ItemStyle-Width="8%" >
							    <ItemTemplate>
								    <%#Container.DataItem("DocIDFrom")%>
								    <asp:label id="lblDocIDFrom" Text=<%# Container.DataItem("DocIDFrom") %> Visible="False" Runat="server"></asp:label>
							    </ItemTemplate>
						    </asp:TemplateColumn>
						    <asp:TemplateColumn HeaderText="Keterangan" ItemStyle-Width="15%" >
							    <ItemTemplate>
								    <asp:label id="lblDescrFrom" Text=<%# Container.DataItem("DescrFrom")%> Runat="server"></asp:label>
							    </ItemTemplate>
						    </asp:TemplateColumn>
						    <asp:TemplateColumn HeaderText="Jumlah" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
							    <ItemTemplate>
								    <asp:label id="lblAmountFrom" Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("AmountFrom"), 2), 2) %> Runat="server"></asp:label>
							    </ItemTemplate>
						    </asp:TemplateColumn>
    						
						    <asp:TemplateColumn HeaderText="Tanggal" ItemStyle-Width="6%" >
							    <ItemTemplate>
								    <asp:Label ID="lblDocDateTo" Text='<%# objGlobal.GetLongDate(Container.DataItem("DocDateTo")) %>' runat="server" />
							    </ItemTemplate>
						    </asp:TemplateColumn>
						    <asp:TemplateColumn HeaderText="No. Bukti" ItemStyle-Width="8%" >
							    <ItemTemplate>
								    <%#Container.DataItem("DocIDTo")%>
								    <asp:label id="lblDocIDTo" Text=<%# Container.DataItem("DocIDTo") %> Visible="False" Runat="server"></asp:label>
							    </ItemTemplate>
						    </asp:TemplateColumn>
						    <asp:TemplateColumn HeaderText="Keterangan" ItemStyle-Width="15%" >
							    <ItemTemplate>
								    <asp:label id="lblDescrTo" Text=<%# Container.DataItem("DescrTo")%> Runat="server"></asp:label>
							    </ItemTemplate>
						    </asp:TemplateColumn>
						    <asp:TemplateColumn HeaderText="Jumlah" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
							    <ItemTemplate>
								    <asp:label id="lblAmountTo" Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("AmountTo"), 2), 2) %> Runat="server"></asp:label>
							    </ItemTemplate>
						    </asp:TemplateColumn>
    						
					    </Columns>
					    </asp:DataGrid><BR>
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
						<tr>
							<td>
					            <asp:ImageButton id=ibPrint UseSubmitBehavior="false" AlternateText=Print imageurl="../../images/butt_print.gif" onClick="btnPreview_Click" visible=true runat="server"/>
							</td>
						</tr>
                        <tr>
                            <td>
                         
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
