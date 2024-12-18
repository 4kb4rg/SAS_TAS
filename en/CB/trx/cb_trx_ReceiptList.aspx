<%@ Page Language="vb" src="../../../include/cb_trx_ReceiptList.aspx.vb" Inherits="cb_trx_ReceiptList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuCB" src="../../menu/menu_cbtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
<head>
<title>Account Receivables - Receipt List</title>
    <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
<Preference:PrefHdl id=PrefHdl runat="server" />
</head>
	<body>
	    <form id=frmMain runat="server" class="main-modul-bg-app-list-pu">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />			
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:Label id=SortCol Visible=False Runat="server" />
			<asp:label id="lblCode" Visible="False" text=" Code" Runat="server" />

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
							<td><strong>RECEIPT LIST</strong><hr style="width :100%" />   
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
								<td valign=bottom width=20%>Receipt ID :<BR><asp:TextBox id=txtReceiptID width=100% maxlength="32" CssClass="fontObject" runat="server" /></td>
								<td valign=bottom width=25%><asp:label id=lblBillPartyTag runat=server /> : <BR><asp:TextBox id=txtBillParty width=100% maxlength="20"  CssClass="fontObject"  runat="server" /></td>
								<td valign=bottom width=20%>Doc ID :<BR><asp:TextBox id=txtDocId width=100% maxlength="32" runat="server" /></td>
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
								    <asp:DropDownList id="lstAccYear" width=100% CssClass="fontObject"  runat=server>
									</asp:DropDownList>
								<td valign=bottom width=12%>Status :<BR><asp:DropDownList id="ddlStatus" width=100% CssClass="fontObject"  runat=server /></td>
								<td valign=bottom width=20%><BR><asp:TextBox id=txtLastUpdate width=100% maxlength="128" visible=false runat="server"/></td>
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
                             OnItemDataBound="dgLine_BindGrid"  
							Cellpadding=2 
							AllowPaging=True 
							Allowcustompaging=False 
							Pagesize=15 
							OnPageIndexChanged=OnPageChanged 
							Pagerstyle-Visible=False 
							OnDeleteCommand=DEDR_Delete 
							OnSortCommand=Sort_Grid  
							AllowSorting=True >
<HeaderStyle CssClass="mr-h"/>
<ItemStyle CssClass="mr-l"/>
<AlternatingItemStyle CssClass="mr-r"/>
							<Columns>
								<asp:HyperLinkColumn HeaderText="Receipt ID" 
													 SortExpression="ReceiptID" 
													 DataNavigateUrlField="ReceiptID" 
													 DataNavigateUrlFormatString="CB_trx_ReceiptDet.aspx?receiptid={0}" 
													 DataTextField="ReceiptID" />
								<asp:TemplateColumn HeaderText="Date" SortExpression="RC.CreateDate">
									<ItemTemplate>
										<%#objGlobal.GetLongDate(Container.DataItem("CreateDate"))%>
									</ItemTemplate>
								</asp:TemplateColumn>		
								<asp:HyperLinkColumn HeaderText="Customer" SortExpression="Name" 
													 DataNavigateUrlField="ReceiptID" 
													 DataNavigateUrlFormatString="CB_trx_ReceiptDet.aspx?receiptid={0}" 
													 DataTextField="Name" />
							    <asp:TemplateColumn HeaderText="Bank/Cash" SortExpression="BankCode">
									<ItemTemplate>
										<%#Container.DataItem("BankCode")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Doc Amount" SortExpression="TotalAmountToDisplay" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
									<ItemTemplate>
										<%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("TotalAmountToDisplay"), 2), 2)%>
									</ItemTemplate>
								</asp:TemplateColumn>
														
								<asp:TemplateColumn HeaderText="Status" SortExpression="RC.Status">
									<ItemTemplate>
										<%# objCBTrx.mtdGetReceiptStatus(Container.DataItem("Status")) %>
									</ItemTemplate>
								</asp:TemplateColumn>								
								<asp:TemplateColumn HeaderText="Updated By" SortExpression="USR.UserName">
									<ItemTemplate>
										<%# Container.DataItem("UserName") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemTemplate>
										<asp:label id=lblReceiptId visible="false" text=<%# Container.DataItem("ReceiptID")%> runat="server" />
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
									<td><asp:DropDownList ID="lstDropList" CssClass="fontObject" runat="server" AutoPostBack="True" onSelectedIndexChanged="PagingIndexChanged" /></td>
									<td><asp:ImageButton ID="btnNext" runat="server" alternatetext="Next" commandargument="next" imageurl="../../../images/btnext.png" onClick="btnPrevNext_Click" /></td>
									<td><img height="18px" src="../../../images/btlast.png" width="18px" class="button" /></td>
								</tr>
							</table>
							</td>
						</tr>
						<tr>
							<td>
						        <asp:ImageButton id=NewBtn onClick=NewBtn_Click imageurl="../../images/butt_new.gif" AlternateText="New Receipt" runat=server/>
						        <!--<asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print visible=false/>
						        <a href="#" onclick="javascript:popwin(200, 400, 'BI_trx_PrintDocs.aspx?doctype=1')"><asp:Image id="ibPrintDoc" runat="server" ImageUrl="../../images/butt_print_doc.gif"/></a>-->
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
