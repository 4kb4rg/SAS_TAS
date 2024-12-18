<%@ Page Language="vb" src="../../../include/cb_trx_PayList.aspx.vb" Inherits="cb_trx_PayList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuCB" src="../../menu/menu_cbtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<script runat="server">

Protected Sub ibPrint_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

End Sub
</script>

<html>
<head>
	<title>Payment List</title>
    <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	<Preference:PrefHdl id=PrefHdl runat="server" />
</head>
	<body>
	    <form id=frmPayList runat="server" class="main-modul-bg-app-list-pu">
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
							<td><strong>PAYMENT LIST</strong><hr style="width :100%" />   
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
	<td valign=bottom width=15%>Payment ID :<BR><asp:TextBox id=txtPayID width=100% maxlength="32" CssClass="fontObject" runat="server" /></td>
								<td valign=bottom width=15%>Supplier Code :<BR><asp:TextBox id=txtSupplier width=100% maxlength="20" CssClass="fontObject"  runat="server"/></td>
								<td valign=bottom width=15%>Doc ID :<BR><asp:TextBox id=txtDocId width=100% maxlength="32" CssClass="fontObject"  runat="server" /></td>
								<TD valign=bottom width=10%>Payment By :<BR>
								    <asp:DropDownList width=100% id=ddlPayType runat=server>
								        <asp:ListItem value="9" Selected>All</asp:ListItem>
						                <asp:ListItem value="0">Cheque</asp:ListItem>
						                <asp:ListItem value="1">Cash</asp:ListItem>
						                <asp:ListItem value="2">Need Verification</asp:ListItem>
						                <asp:ListItem value="3">Bilyet Giro</asp:ListItem>
					                </asp:DropDownList></TD>
					            <td valign=bottom width=10%>Cheque/Giro No :<BR><asp:TextBox id=txtChequeNo width=100% maxlength="100" CssClass="fontObject"  runat="server"/></td>
								<td valign=bottom width=8%>Period :<BR>
								    <asp:DropDownList id="lstAccMonth" CssClass="fontObject"  width=100% runat=server>
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
								<td valign=bottom width=15%><BR><asp:TextBox id=txtLastUpdate width=100% maxlength="128" Visible=false CssClass="fontObject"  runat="server"/></td>
								<td valign=bottom width=5% align=right><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
								</tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
					<asp:DataGrid id=dgPayment
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
                            OnItemDataBound="dgLine_BindGrid"
							AllowSorting=True >
<HeaderStyle CssClass="mr-h"/>
<ItemStyle CssClass="mr-l"/>
<AlternatingItemStyle CssClass="mr-r"/>
							<Columns>
								<asp:BoundColumn Visible=False HeaderText="Payment ID" DataField="PaymentID" />
								<asp:HyperLinkColumn HeaderText="Payment ID" 
													 SortExpression="PAY.PaymentID" 
													 DataNavigateUrlField="PaymentID" 
													 DataNavigateUrlFormatString="cb_trx_PayDet.aspx?payid={0}" 
													 DataTextField="PaymentID" />
								<asp:TemplateColumn HeaderText="Date" SortExpression="PAY.CreateDate">
									<ItemTemplate>
										<%# objGlobal.GetLongDate(Container.DataItem("CreateDate")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
													 
								<asp:TemplateColumn HeaderText="Name" SortExpression="SUPP.SupplierName">
									<ItemTemplate>
										<%# Container.DataItem("SupplierName") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Payment By" SortExpression="PaymentType">
									<ItemTemplate>
										<%#Container.DataItem("PaymentType")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Bank/Cash" SortExpression="BankCode">
									<ItemTemplate>
										<%#Container.DataItem("BankCode")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Doc Amount" SortExpression="DocAmt" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
									<ItemTemplate>
										<%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("DocAmt"), 2), 2)%>
									</ItemTemplate>
								</asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Remark" SortExpression="Remark">
									<ItemTemplate>
										<%#Container.DataItem("Remark")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Period" SortExpression="PAY.AccMonth">
									<ItemTemplate>
										<%#Month(Container.DataItem("CreateDate"))%>/<%#Year(Container.DataItem("CreateDate"))%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Status" SortExpression="PAY.Status">
									<ItemTemplate>
										<%# objCBTrx.mtdGetPaymentStatus(Container.DataItem("Status")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Updated By" SortExpression="USR.UserName">
									<ItemTemplate>
										<%# Container.DataItem("UserName") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemTemplate>
										<asp:label id=idPayId visible="false" text=<%# Container.DataItem("PaymentID")%> runat="server" />
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
					             <asp:ImageButton id=NewPayBtn UseSubmitBehavior="false" onClick=NewPayBtn_Click imageurl="../../images/butt_new.gif" AlternateText="New Payment" runat=server/>
						        <a href="#" onclick="javascript:popwin(400, 700, 'CB_trx_PrintDocs.aspx?doctype=2')"><asp:Image id="ibPrintDoc" runat="server" ImageUrl="../../images/butt_print_doc.gif"/></a>
						        <asp:ImageButton id=ibPrint UseSubmitBehavior="false" imageurl="../../images/butt_print.gif" AlternateText=Print visible=false runat=server OnClick="ibPrint_Click"/>
						        <asp:ImageButton ID=ForwardBtn UseSubmitBehavior="false" CausesValidation=False onclick=ForwardBtn_Click ImageUrl="../../images/butt_move_forward.gif" AlternateText="Move Forward" Runat=server />
						        <asp:ImageButton ID="PostingBtn" UseSubmitBehavior="false"  AlternateText="Posting" onclick="PostingBtn_Click"  ImageUrl="../../images/butt_post.gif"  CausesValidation=False Runat=server />
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

			<asp:Label id=lblErrMesage visible=false ForeColor=red Text="Error while initiating component." runat=server />			
		</FORM>
	</body>
</html>
