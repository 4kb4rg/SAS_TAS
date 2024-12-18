<%@ Page Language="vb" src="../../../include/AP_trx_DNList.aspx.vb" Inherits="ap_trx_DNList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuAP" src="../../menu/menu_aptrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
<head>
	<title>Supplier Debit Note List</title>
    <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />

<%--	<Preference:PrefHdl id=PrefHdl runat="server" />--%>
</head>
	<body>
	    <form id=frmDNList runat="server" class="main-modul-bg-app-list-pu">
			<asp:label id="SortExpression" Visible="False" Runat="server" />

		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuAP id=MenuAP runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>SUPPLIER DEBIT NOTE LIST</strong><hr style="width :100%" />   
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
								<td valign=bottom width=12%>Debit Note ID :<BR><asp:TextBox id=txtDNID width=100% maxlength="32" CssClass="fontObject" runat="server" /></td>
								<td valign=bottom width=18%>Debit Note Ref. No. :<BR><asp:TextBox id=txtDNRefNoID width=100% maxlength="20" CssClass="fontObject" runat="server" /></td>
								<td valign=bottom width=20%>Supplier Code:<BR><asp:TextBox id=txtSupplier width=100% maxlength="20" CssClass="fontObject" runat="server"/></td>
								<td valign=bottom width=18%><asp:Label id=lblInvoiceRcvRefNo runat=server /> :<BR><asp:TextBox id=txtInvoiceRcvRefNo maxlength="32" width=100% CssClass="fontObject" runat="server"/></td>
								<td valign=bottom width=8%>Period :<BR>
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
								<td valign=bottom width=10%><BR>
								    <asp:DropDownList id="lstAccYear" width=100% CssClass="fontObject" runat=server>
									</asp:DropDownList>
								<td valign=bottom width=12%>Status :<BR><asp:DropDownList id="ddlStatus" width=100% CssClass="fontObject" runat=server /></td>
								<td valign=bottom width=18%><BR><asp:TextBox id=txtLastUpdate width=100% maxlength="128" Visible=false CssClass="fontObject" runat="server"/></td>
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
						<asp:DataGrid id=dgDebitNote
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
                            OnItemDataBound="dgLine_BindGrid"
                            class="font9Tahoma">
								
                            <HeaderStyle CssClass="mr-h"  />
                            <ItemStyle CssClass="mr-l" />
                            <AlternatingItemStyle CssClass="mr-r"  />
							<Columns>
								<asp:BoundColumn Visible=False HeaderText="Debit Note ID" DataField="DebitNoteID" />
								<asp:HyperLinkColumn HeaderText="Debit Note ID" 
													 SortExpression="DN.DebitNoteID" 
													 DataNavigateUrlField="DebitNoteID" 
													 DataNavigateUrlFormatString="ap_trx_DNDet.aspx?dbnid={0}" 
													 DataTextField="DebitNoteID" />

								<asp:HyperLinkColumn HeaderText="Debit Note Ref. No." 
													 SortExpression="DN.SupplierDocRefNo" 
													 DataNavigateUrlField="DebitNoteID" 
													 DataNavigateUrlFormatString="ap_trx_DNDet.aspx?dbnid={0}" 
													 DataTextField="SupplierDocRefNo" />
								
								<asp:TemplateColumn HeaderText="Supplier Code" SortExpression="SUPP.SupplierCode">
									<ItemTemplate>
										<%# Container.DataItem("SupplierCode") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Supplier Name" SortExpression="SUPP.SupplierName">
									<ItemTemplate>
										<%# Container.DataItem("SupplierName") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="DN.InvoiceRcvRefNo">
									<ItemTemplate>
										<%# Container.DataItem("InvoiceRcvRefNo") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Last Update" SortExpression="DN.UpdateDate">
									<ItemTemplate>
										<%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Status" SortExpression="DN.Status">
									<ItemTemplate>
										<%# objAPTrx.mtdGetDebitNoteStatus(Container.DataItem("Status")) %>
									</ItemTemplate>
								</asp:TemplateColumn>								
								<asp:TemplateColumn HeaderText="Updated By" SortExpression="USR.UserName">
									<ItemTemplate>
										<%# Container.DataItem("UserName") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemTemplate>
										<asp:label id=idDNId visible="false" text=<%# Container.DataItem("DebitNoteID")%> runat="server" />
										<asp:label id="lblStatus" Text='<%# Trim(Container.DataItem("Status")) %>' Visible="False" Runat="server" />
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
						        <asp:ImageButton id=NewDNBtn UseSubmitBehavior="false" onClick=NewDNBtn_Click imageurl="../../images/butt_new.gif" AlternateText="New Debit Note" runat=server/>
						        <asp:ImageButton id=ibPrint UseSubmitBehavior="false" imageurl="../../images/butt_print.gif" AlternateText=Print visible=false runat=server/>
						        <asp:Label id=SortCol Visible=False Runat="server" />
						        <asp:Label id=lblErrMesage visible=false Text="Error while initiating component." runat=server />
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
