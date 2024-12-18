<%@ Page Language="vb" src="../../../include/CB_trx_WithdrawalList.aspx.vb" Inherits="CB_trx_WithdrawalList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuCB" src="../../menu/menu_cbtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
<head>
	<title>Deposit List</title>
    <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
<%--	<Preference:PrefHdl id=PrefHdl runat="server" />--%>
</head>
	<body>
	    <form id=frmWithdrawalList runat="server" class="main-modul-bg-app-list-pu">
			<asp:Label id=lblErrMesage visible=false Text="Error while initiating component." runat=server />			
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:Label id=SortCol Visible=False Runat="server" />


		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuCB id=MenuCB runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>WITHDRAWAL LIST</strong><hr style="width :100%" />   
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
																<td valign=bottom width=12%>Withdrawal Code :<BR><asp:TextBox id=txtWdrCode width=100% maxlength="20" runat="server" /></td>
								<td valign=bottom width=23%>Description :<BR><asp:TextBox id=txtDesc width=100% maxlength="32" runat="server"/></td>
								<td valign=bottom width=12%>Deposit Code :<BR><asp:TextBox id=txtDepCode width=100% maxlength="20" runat="server"/></td>
								<td valign=bottom width=18%>Bilyet No :<BR><asp:TextBox id=txtBilyetNo width=100% maxlength="32" runat="server"/></td>
								<td valign=bottom width=10%>Status :<BR><asp:DropDownList id="ddlStatus" width=100% runat=server /></td>
								<td valign=bottom width=15%>Last Updated By :<BR><asp:TextBox id=txtLastUpdate width=100% maxlength="128" runat="server"/></td>
								<td valign=bottom width=10% align=right><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
								</tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
						            <asp:DataGrid id=dgWithdrawal
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
								<asp:BoundColumn Visible=False HeaderText="Withdrawal Code" DataField="WithdrawalCode" />
								<asp:HyperLinkColumn HeaderText="Withdrawal Code" 
													 SortExpression="WDR.WithdrawalCode" 
													 DataNavigateUrlField="WithdrawalCode" 
													 DataNavigateUrlFormatString="cb_trx_WithdrawalDet.aspx?WithdrawalCode={0}" 
													 DataTextField="WithdrawalCode" />
								<asp:HyperLinkColumn HeaderText="Description" 
													 SortExpression="WDR.Description" 
													 DataNavigateUrlField="WithdrawalCode" 
													 DataNavigateUrlFormatString="cb_trx_WithdrawalDet.aspx?WithdrawalCode={0}" 
													 DataTextField="Description" />
								<asp:HyperLinkColumn HeaderText="Deposit Code" 
													 SortExpression="WDR.DepositCode" 
													 DataNavigateUrlField="WithdrawalCode" 
													 DataNavigateUrlFormatString="cb_trx_WithdrawalDet.aspx?WithdrawalCode={0}" 
													 DataTextField="DepositCode" />			 
								<asp:HyperLinkColumn HeaderText="Bilyet No" 
													 SortExpression="DEP.BilyetNo" 
													 DataNavigateUrlField="WithdrawalCode" 
													 DataNavigateUrlFormatString="cb_trx_WithdrawalDet.aspx?WithdrawalCode={0}" 
													 DataTextField="BilyetNo" />	
								
								
								<asp:TemplateColumn HeaderText="Last Update" SortExpression="WDR.UpdateDate">
									<ItemTemplate>
										<%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Status" SortExpression="WDR.Status">
									<ItemTemplate>
										<%# objCBTrx.mtdGetWithdrawalStatus(Container.DataItem("Status")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Updated By" SortExpression="USR.UserName">
									<ItemTemplate>
										<%# Container.DataItem("UserName") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn>
									<ItemTemplate>
										<asp:label id=idWdrCode visible="false" text=<%# Container.DataItem("WithdrawalCode")%> runat="server" />
										<asp:label id="lblStatus" Text='<%# Trim(Container.DataItem("Status")) %>' Visible="False" Runat="server" />
										<asp:label id="idDepCode" Text='<%# Trim(Container.DataItem("DepositCode")) %>' Visible="False" Runat="server" />
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
								</tr>
							</table>
							</td>
						</tr>
						<tr>
							<td>
					            <asp:ImageButton id=NewWdrBtn onClick=NewWdrBtn_Click imageurl="../../images/butt_new.gif" AlternateText="New Withdrawal" runat=server/>
				        		<asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print visible=false runat=server/>
							</td>
						</tr>
                        <tr>
                            <td>
 					            &nbsp;
                              
                          
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
