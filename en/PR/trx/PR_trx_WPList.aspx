<%@ Page Language="vb" src="../../../include/PR_Trx_WPList.aspx.vb" Inherits="PR_Trx_WPList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRTrx" src="../../menu/menu_PRTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Work Performance Entry List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<%--<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	</head>
	<body>
		<form id=frmList runat="server" class="main-modul-bg-app-list-pu">
    		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="SortExpression" Visible="False" Runat="server" />



		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuPRTrx id=MenuPRTrx runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>WORK PERFORMANCE ENTRY LIST</strong><hr style="width :100%" />   
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
								<td width="20%" height="26">Work Performance Code :<BR><asp:TextBox id=txtWPTrxId width=100% maxlength="20" runat="server" /></td>
								<td width="30%">Description :<BR><asp:TextBox id=txtDescription width=100% maxlength="128" runat="server" /></td>
								<td width="15%">Gang Code :<BR><asp:TextBox id=txtGangCode width=100% maxlength="20" runat="server" /></td>
								<td width="15%">Status :<BR>
									<asp:DropDownList id="ddlStatus" width=100% runat=server>
										<asp:ListItem value="0">All</asp:ListItem>
										<asp:ListItem value="1" selected>Active</asp:ListItem>
										<asp:ListItem value="3">Deleted</asp:ListItem>
										<asp:ListItem value="2">Closed</asp:ListItem>
									</asp:DropDownList>
								</td>
								<td width="20%">Last Updated By :<BR><asp:TextBox id=txtLastUpdate width=100% maxlength="128" runat="server"/></td>
								<td width="10%" valign=bottom align=right><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
								</tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
						            <asp:DataGrid id=dgLine runat=server
							            AutoGenerateColumns=false width=100% 
							            GridLines=none 
							            Cellpadding=2 
							            AllowPaging=True 
							            Allowcustompaging=False 
							            Pagesize=15 
							            OnPageIndexChanged=OnPageChanged 
							            Pagerstyle-Visible=False 
							            OnDeleteCommand=DEDR_Delete 
							            OnSortCommand=Sort_Grid  
							            AllowSorting=True
                                                    class="font9Tahoma">
								
							                        <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                        <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                        <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
							            <Columns>
								            <asp:BoundColumn Visible=False HeaderText="Work Performance Code" DataField="WPTrxId" />
								            <asp:HyperLinkColumn HeaderText="Work Performance Code" ItemStyle-Width="20%" HeaderStyle-Width="19%"
									            SortExpression="WP.WPTrxId" 
									            DataNavigateUrlField="WPTrxId" 
									            DataNavigateUrlFormatString="PR_Trx_WPDet.aspx?WPTrxId={0}" 
									            DataTextField="WPTrxId" />

								            <asp:HyperLinkColumn HeaderText="Description" ItemStyle-Width="30%" HeaderStyle-Width="28%"
									            SortExpression="WP.Description" 
									            DataNavigateUrlField="WPTrxId" 
									            DataNavigateUrlFormatString="PR_Trx_WPDet.aspx?WPTrxId={0}" 
									            DataTextField="Description" />
									
								            <asp:HyperLinkColumn HeaderText="Gang Code" ItemStyle-Width="15%" HeaderStyle-Width="14%"
									            SortExpression="WP.GangCode" 
									            DataNavigateUrlField="WPTrxId" 
									            DataNavigateUrlFormatString="PR_Trx_WPDet.aspx?WPTrxId={0}" 
									            DataTextField="GangCode" />
									
								            <asp:TemplateColumn HeaderText="Last Update" ItemStyle-Width="10%" HeaderStyle-Width="10%" SortExpression="WP.UpdateDate">
									            <ItemTemplate>
										            <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
									
								            <asp:TemplateColumn HeaderText="Status" ItemStyle-Width="10%" HeaderStyle-Width="10%" SortExpression="WP.Status">
									            <ItemTemplate>
										            <%# objPRTrx.mtdGetWPTrxStatus(Container.DataItem("Status")) %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
									
								            <asp:TemplateColumn HeaderText="Updated By" ItemStyle-Width="10%" HeaderStyle-Width="10%" SortExpression="USR.UserName">
									            <ItemTemplate>
										            <%# Container.DataItem("UserName") %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn ItemStyle-Width="15%" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign=center>
									            <ItemTemplate>
										            <asp:label id="lblStatus" Text='<%# Trim(Container.DataItem("Status")) %>' Visible="False" Runat="server" />
										            <asp:label id="lblCountCloseLn" text=<%# Container.DataItem("CountCloseLn") %> visible=false runat=server/>
										            <asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server />
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
                                    <asp:Label id=lblCurrentIndex visible=false text=0 runat=server/>
			                      	<asp:Label id=lblPageCount visible=false text=1 runat=server/>		
								</tr>
							</table>
							</td>
						</tr>
						<tr>
							<td>
					            <asp:ImageButton id=NewWPBtn OnClick="NewWPBtn_Click" imageurl="../../images/butt_new.gif" AlternateText="New Work Performance Entry" runat="server"/>
						        <asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print visible=false/>
						        <asp:Label id=SortCol Visible=False Runat="server" />
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




		</FORM>
	</body>
</html>
