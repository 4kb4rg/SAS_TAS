<%@ Page Language="vb" src="../../../include/PR_trx_ContractPayList.aspx.vb" Inherits="PR_trx_ContractPayList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRTrx" src="../../menu/menu_prtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Contract Payment List</title>
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
						<UserControl:MenuPRTrx id=MenuPRTrx runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>CONTRACT PAYMENT LIST</strong><hr style="width :100%" />   
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
								<td width="15%">Contract ID :<BR><asp:TextBox id=txtContractID width=100% maxlength="20" runat="server" /></td>
								<td width="15%"><asp:label id="lblBlock" runat="server" /> Code :<BR><asp:TextBox id=txtBlock width=100% maxlength="8" runat="server" /></td>
								<td width="25%"><asp:label id="lblAccount" runat="server" /> Code :<BR><asp:TextBox id=txtAccount width=100% maxlength="32" runat="server" /></td>
								<td width="15%">Status :<BR>
									<asp:DropDownList id="ddlStatus" width=100% runat=server>
										<asp:ListItem value="">All</asp:ListItem>
										<asp:ListItem value="1" selected>Active</asp:ListItem>
										<asp:ListItem value="3">Cancelled</asp:ListItem>
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
								                <asp:BoundColumn Visible=False HeaderText="Contract ID" DataField="ContractID" />
								                <asp:HyperLinkColumn HeaderText="Contract ID" ItemStyle-Width="15%"
									                SortExpression="PAY.ContractID" 
									                DataNavigateUrlField="ContractID" 
									                DataNavigateUrlFormatString="PR_trx_ContractPayDet.aspx?payid={0}" 
									                DataTextField="ContractID" />
									
								                <asp:HyperLinkColumn ItemStyle-Width="15%" 
									                SortExpression="PAY.BlkCode" 
									                DataNavigateUrlField="ContractID" 
									                DataNavigateUrlFormatString="PR_trx_ContractPayDet.aspx?payid={0}" 
									                DataTextField="BlkCode" />
								
								                <asp:HyperLinkColumn ItemStyle-Width="25%" 
									                SortExpression="PAY.AccCode" 
									                DataNavigateUrlField="ContractID" 
									                DataNavigateUrlFormatString="PR_trx_ContractPayDet.aspx?payid={0}" 
									                DataTextField="AccCode" />
									
								                <asp:TemplateColumn HeaderText="Last Update" ItemStyle-Width="10%" SortExpression="PAY.UpdateDate">
									                <ItemTemplate>
										                <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									                </ItemTemplate>
								                </asp:TemplateColumn>
									
								                <asp:TemplateColumn HeaderText="Status" ItemStyle-Width="10%" SortExpression="PAY.Status">
									                <ItemTemplate>
										                <%# objPRTrx.mtdGetContractPayStatus(Container.DataItem("Status")) %>
									                </ItemTemplate>
								                </asp:TemplateColumn>
									
								                <asp:TemplateColumn HeaderText="Updated By" ItemStyle-Width="10%" SortExpression="USR.UserName">
									                <ItemTemplate>
										                <%# Container.DataItem("UserName") %>
									                </ItemTemplate>
								                </asp:TemplateColumn>
								                <asp:TemplateColumn ItemStyle-Width="15%" ItemStyle-HorizontalAlign=center>
									                <ItemTemplate>
										                <asp:label id=lblId visible="false" text=<%# Container.DataItem("ContractID")%> runat="server" />
										                <asp:label id="lblStatus" Text='<%# Trim(Container.DataItem("Status")) %>' Visible="False" Runat="server" />
										                <asp:LinkButton id=lbDelete CommandName=Delete Text=Cancel runat=server />
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
					            <asp:ImageButton id=NewPayBtn OnClick="NewPayBtn_Click" imageurl="../../images/butt_new.gif" AlternateText="New Contract Payment" runat="server"/>
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
