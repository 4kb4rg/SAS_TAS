<%@ Page Language="vb" src="../../../include/BI_trx_DNList.aspx.vb" Inherits="BI_trx_DNList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuBI" src="../../menu/menu_bitrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
<head>
<title>Accounts Receivables - Debit Note List</title>
    <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
<%--<Preference:PrefHdl id=PrefHdl runat="server" />--%>
</head>
	<body>
	    <form id=frmMain runat="server" class="main-modul-bg-app-list-pu">
			<asp:Label id=lblErrMesage visible=false Text="Error while initiating component." runat=server />	    
			<asp:Label id=SortCol Visible=False Runat="server" />
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:label id="lblCode" Visible="False" text=" Code" Runat="server" />

		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuBI id=MenuBI runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>DEBIT NOTE LIST</strong><hr style="width :100%" />   
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
<td valign=bottom width=20%>Debit Note ID :<BR><asp:TextBox id=txtDNID width=100% maxlength="32" runat="server" /></td>
								<td valign=bottom width=15%><asp:label id=lblBillParty runat=server /> :<br><asp:textbox id=txtBillParty width=100% maxlength=20 runat=server /></td>
								<td valign=bottom width=20%>Billing Type :<br><asp:dropdownlist id=ddlDebitNoteType CssClass="font9Tahoma" runat=server /></td>
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
								<td valign=bottom width=15%>Status :<BR><asp:DropDownList id="ddlStatus" width=100% CssClass="font9Tahoma" runat=server /></td>
								<td valign=bottom width=20%><BR><asp:TextBox id=txtLastUpdate width=100% maxlength=128 Visible=false runat="server"/></td>
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
				<asp:DataGrid id=dgLine
							AutoGenerateColumns=false width=100% runat=server
							GridLines=none 
							Cellpadding=2 
							AllowPaging=True 
							Allowcustompaging=False 
							Pagesize=15 
							OnPageIndexChanged=OnPageChanged 
                            OnItemDataBound="dgLine_BindGrid"
							Pagerstyle-Visible=False 
							OnDeleteCommand=DEDR_Delete 
							OnSortCommand=Sort_Grid  
							AllowSorting=True
                                        class="font9Tahoma">
								
							            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
							<Columns>
								<asp:BoundColumn Visible=False HeaderText="Debit Note ID" DataField="DebitNoteID" />
								<asp:HyperLinkColumn HeaderText="Debit Note ID" 
													 SortExpression="DN.DebitNoteID" 
													 DataNavigateUrlField="DebitNoteID" 
													 DataNavigateUrlFormatString="BI_trx_DNDet.aspx?dbnid={0}" 
													 DataTextField="DebitNoteID" />
													 
								<asp:TemplateColumn SortExpression="DN.BillPartyCode">
									<ItemTemplate>
										<%# Container.DataItem("BillPartyCode") %>
									</ItemTemplate>
								</asp:TemplateColumn>
							    <asp:TemplateColumn HeaderText="Customer Name" SortExpression="BI.Name">
									<ItemTemplate>
										<%#Container.DataItem("CustName")%>
									</ItemTemplate>
								</asp:TemplateColumn>	
								<asp:TemplateColumn HeaderText="Billing Type" SortExpression="DN.DocType">
									<ItemTemplate>
										<%# objBITrx.mtdGetDebitNoteDocType(Container.DataItem("DocType")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
							
								<asp:TemplateColumn HeaderText="Last Update" SortExpression="DN.UpdateDate">
									<ItemTemplate>
										<%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Status" SortExpression="DN.Status">
									<ItemTemplate>
										<%# objBITrx.mtdGetDebitNoteStatus(Container.DataItem("Status")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Updated By" SortExpression="UpdateID">
									<ItemTemplate>
										<%# Container.DataItem("UpdateID") %>
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
						        <asp:ImageButton id=NewDNBtn onClick=NewDNBtn_Click imageurl="../../images/butt_new.gif" AlternateText="New Debit Note" runat=server/>
						        <asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print visible=false runat=server/>
						        <a href="#" onclick="javascript:popwin(200, 400, 'BI_trx_PrintDocs.aspx?doctype=1')"><asp:Image id="ibPrintDoc" runat="server" visible=false ImageUrl="../../images/butt_print_doc.gif"/></a>
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
