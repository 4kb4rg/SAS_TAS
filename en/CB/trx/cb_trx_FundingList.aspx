<%@ Page Language="vb" src="../../../include/cb_Trx_FundingList.aspx.vb" Inherits="cb_Trx_FundingList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuCB" src="../../menu/menu_cbtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
<head>
	<title>Bank Loan List</title>
    <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	<%--<Preference:PrefHdl id=PrefHdl runat="server" />--%>
</head>
	<body>
	    <form id=frmPayList runat="server" class="main-modul-bg-app-list-pu">
			<asp:label id="SortExpression" Visibl e="False" Runat="server" />
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
							<td><strong>BANK LOAN LIST</strong><hr style="width :100%" />   
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
								<td valign=bottom width=15%>ID :<BR><asp:TextBox id=srchTrxID width=100% maxlength="32" runat="server" /></td>
								<td valign=bottom width=15%>Description :<BR><asp:TextBox id=srchDescription width=100% maxlength="100" runat="server"/></td>
								<TD valign=bottom width=10%>Facility :<BR>
								    <asp:DropDownList width=100% id=ddlTrxType runat=server>
								        <asp:ListItem value="0" Selected>All</asp:ListItem>
						                <asp:ListItem value="1">PRK</asp:ListItem>
						                <asp:ListItem value="2">Term Loan</asp:ListItem>
					                </asp:DropDownList></TD>
					            <td valign=bottom width=8%>Period :<BR>
								    <asp:DropDownList id="lstAccMonth" width=100% runat=server>
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
								<td valign=bottom width=10%><BR>
								    <asp:DropDownList id="lstAccYear" width=100% runat=server>
									</asp:DropDownList>
								<td valign=bottom width=12%>Status :<BR><asp:DropDownList id="srchStatus" width=100% runat=server />
									</asp:DropDownList></td>
								<td valign=bottom width=10%><BR><asp:TextBox id="txtLastUpdate" width=100% maxlength="128" visible=false runat="server"/></td>
								<td valign=bottom width=5% align=right><asp:Button id="SearchBtn" Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
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
								<asp:BoundColumn Visible=False HeaderText="ID" DataField="TrxID" />
								<asp:HyperLinkColumn  HeaderText="TrxID" 
													 SortExpression="TrxID" 
													 DataNavigateUrlField="TrxID" 
													 DataNavigateUrlFormatString="cb_trx_FundingDet.aspx?trxid={0}" 
													 DataTextField="TrxID" />
								<asp:HyperLinkColumn  HeaderText="Description" 
													 SortExpression="AccDescr" 
													 DataNavigateUrlField="TrxID" 
													 DataNavigateUrlFormatString="cb_trx_FundingDet.aspx?trxid={0}" 
													 DataTextField="AccDescr" />
													 
								<asp:TemplateColumn HeaderText="Type" SortExpression="TrxTypeDescr">
									<ItemTemplate>
										<%#Container.DataItem("TrxTypeDescr")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Last Update" SortExpression="A.UpdateDate">
									<ItemTemplate>
										<%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Status" SortExpression="A.Status">
									<ItemTemplate>
										<%# objGLTrx.mtdGetLeaseFinanceStatus(Container.DataItem("Status")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Updated By" SortExpression="USR.UserName">
									<ItemTemplate>
										<%# Container.DataItem("UserName") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemTemplate>
										<asp:label id=lblTrxId visible="false" text= <%# Container.DataItem("TrxID")%> runat="server" />
										<asp:label id=lblTrxType visible="false" text= <%# Container.DataItem("TrxType")%> runat="server" />
										<asp:label id="lblStatus" Text= <%# Trim(Container.DataItem("Status")) %> Visible="False" Runat="server" />
										<asp:LinkButton id=lbDelete CommandName=Delete Text=Delete Visible=False runat=server />
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
					            <asp:ImageButton id=NewBtn UseSubmitBehavior="false" onClick=NewBtn_Click imageurl="../../images/butt_new.gif" AlternateText="New" runat=server/>&nbsp;
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


			<asp:Label id=lblErrMesage visible=false ForeColor=red Text="Error while initiating component." runat=server />			
		</FORM>
	</body>
</html>
