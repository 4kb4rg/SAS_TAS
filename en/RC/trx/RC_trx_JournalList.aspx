<%@ Page Language="vb" src="../../../include/RC_trx_JournalList.aspx.vb" Inherits="RC_trx_JournalList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuRC" src="../../menu/menu_rctrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
<head>
<title>Reconciliation Journal List</title>
<link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
<%--<Preference:PrefHdl id=PrefHdl runat="server" />--%>
</head>
	<body>
	    <form id=frmMain runat="server" class="main-modul-bg-app-list-pu">
			<asp:label id="SortExpression" Visible="False" Runat="server" />


		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuRC id=MenuRC runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>RECONCILIATION JOURNAL LIST</strong><hr style="width :100%" />   
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
								<td valign=bottom width=20% align=left>Journal ID :<BR><asp:TextBox id=txtJournalID width=100% maxlength="20" runat="server" /></td>
								<td valign=bottom width=20%>Journal Ref ID :<BR><asp:TextBox id=txtJournalRefID width=100% maxlength="20" runat="server" /></td>
								<td width=15%>To <asp:label id=lblLocation runat=server /> :<BR><asp:TextBox id=txtLocCode width=100% maxlength="20" runat="server" /></td>
								<td valign=bottom width=15% align=left>Status :<BR>
									<asp:DropDownList id="ddlStatus" width=100% runat=server>
										<asp:ListItem value="0" >All</asp:ListItem>
										<asp:ListItem value="1" Selected>Active</asp:ListItem>
										<asp:ListItem value="2">Confirmed</asp:ListItem>										
										<asp:ListItem value="3">Deleted</asp:ListItem>
										<asp:ListItem value="4">Cancelled</asp:ListItem>
									</asp:DropDownList>
								</td>
								<td valign=bottom width=20% align=left>LastUpdate By :<BR><asp:TextBox id=txtLastUpdate width=100% maxlength="10" runat="server"/></td>
								<td valign=bottom width=10% align=left><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
								</tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
						            <asp:DataGrid id=dgLine
							                AutoGenerateColumns=false width=100% runat=server
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
								                <asp:BoundColumn Visible=False HeaderText="Journal ID" DataField="JournalID" />
								                <asp:HyperLinkColumn HeaderText="Journal ID" 
													                 SortExpression="JRN.JournalID" 
													                 DataNavigateUrlField="JournalID" 
													                 DataNavigateUrlFormatString="RC_trx_JournalDet.aspx?jrnid={0}" 
													                 DataTextField="JournalID" />

								                <asp:TemplateColumn HeaderText="Journal Ref No" SortExpression="JRN.DocRefNo">
									                <ItemTemplate>
										                <%# Container.DataItem("DocRefNo") %>
									                </ItemTemplate>
								                </asp:TemplateColumn>

								                <asp:TemplateColumn SortExpression="JRN.ToLocCode">
									                <ItemTemplate>
										                <%# Container.DataItem("ToLocCode") %>
									                </ItemTemplate>
								                </asp:TemplateColumn>
							
								                <asp:TemplateColumn HeaderText="Last Update" SortExpression="JRN.UpdateDate">
									                <ItemTemplate>
										                <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									                </ItemTemplate>
								                </asp:TemplateColumn>
								
								                <asp:TemplateColumn HeaderText="Status" SortExpression="JRN.Status">
									                <ItemTemplate>
										                <%# objRCTrx.mtdGetDispatchAdviceStatus(Container.DataItem("Status")) %>
									                </ItemTemplate>
								                </asp:TemplateColumn>
								
								                <asp:TemplateColumn HeaderText="Updated By" SortExpression="UpdateID">
									                <ItemTemplate>
										                <%# Container.DataItem("UpdateID") %>
									                </ItemTemplate>
								                </asp:TemplateColumn>
								                <asp:TemplateColumn>
									                <ItemTemplate>
										                <asp:label id=idJrnId visible="false" text=<%# Container.DataItem("JournalID")%> runat="server" />
										                <asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server />
									                </ItemTemplate>
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
						<tr>
							<td>
					            <asp:ImageButton id=NewJrnBtn onClick=NewJrnBtn_Click imageurl="../../images/butt_new.gif" AlternateText="New Journal" runat=server/>
						        <asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print visible=false/>
						        <asp:Label id=SortCol Visible=False Runat="server" />
						        <asp:Label id=lblErrMesage visible=false Text="Error while initiating component." runat=server />
						        <asp:Label id=lblTo visible=false text="To " runat=server />
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
