<%@ Page Language="vb" src="../../../include/PU_trx_GRList.aspx.vb" Inherits="PU_GRList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPU" src="../../menu/menu_putrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<script runat="server">


</script>

<html>
	<head>
		<title>Goods Receive List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
<%--		<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	</head>
	<body>
		<form id=frmGRList runat="server" class="main-modul-bg-app-list-pu">
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id=lblErrOnHand visible=false text="Insufficient Quantity On Hand" runat=server />		
			<asp:label id=lblErrOnHold visible=false text="Insufficient Quantity On Hold" runat=server />		

		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuPU id=menuPU runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>GOODS RECEIVE LIST</strong><hr style="width :100%" />   
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
								<td width="15%" height="26">Goods Receive ID :<BR><asp:TextBox id=txtGoodsRcvId width=100% maxlength="32" CssClass="fontObject" runat="server" /></td>
								<td width="15%" height="26">POID :<BR><asp:TextBox id=txtPOID width=100% maxlength="32" CssClass="fontObject" runat="server"/></td>
								<td width="10%" height="26">PO Type :<BR><asp:DropDownList id="ddlPOType" width=100% CssClass="fontObject" runat=server /></td>
								<td width="13%" height="26">Supplier :<BR><asp:TextBox id=txtSuppCode width=100% maxlength="20" CssClass="fontObject" runat="server"/></td>
								<td valign=bottom width="15%" height="26">PR Location :<BR><asp:DropDownList id="ddlLocation" width=100% CssClass="fontObject" runat="server"/></td>
								<td valign=bottom width="15%" height="26">GR Location :<BR><asp:DropDownList id="ddlGRLocation" width=100% CssClass="fontObject" runat="server"/></td>
								<td width=5% height="26">Period :<BR>
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
								<td width=7%><BR>
								    <asp:DropDownList id="lstAccYear" width=100% CssClass="fontObject" runat=server>
									</asp:DropDownList>
								<td width="12%" height="26">Status :<BR>
									<asp:DropDownList id="ddlStatus" width=100% CssClass="fontObject" runat=server>
										<asp:ListItem value="0">All</asp:ListItem>
										<asp:ListItem value="1">Active</asp:ListItem>
										<asp:ListItem value="2">Confirmed</asp:ListItem>
										<asp:ListItem value="3">Deleted</asp:ListItem>
										<asp:ListItem value="4">Cancelled</asp:ListItem>
										<asp:ListItem value="5">Closed</asp:ListItem>
									</asp:DropDownList>
								</td>
								<td width="15%" height="26" align=left><BR><asp:TextBox id=txtLastUpdate width=100% maxlength="128" visible=false CssClass="fontObject" runat="server"/></td>
								<td width="10%" height="26" valign=bottom align=right><asp:Button id=Button1 Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
								<td width="0%"><asp:TextBox id=txtGoodsRcvRefNo width=0% BorderStyle=None BackColor=transparent maxlength="32" CssClass="fontObject" runat="server" /></td>
								</tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
						<asp:DataGrid id=dgGRList runat=server
							AutoGenerateColumns=false width=100% 
							GridLines=none 
							Cellpadding=2 
							AllowPaging=True 
							Allowcustompaging=False 
							Pagesize=15 
							OnPageIndexChanged=OnPageChanged 
							Pagerstyle-Visible=False 
							OnDeleteCommand=DEDR_Delete 
							OnEditCommand=DEDR_Edit
                             OnItemDataBound="dgLine_BindGrid" 
							OnSortCommand=Sort_Grid  
							AllowSorting=True >
								
							<HeaderStyle CssClass="mr-h"/>
							<ItemStyle CssClass="mr-l"/>
							<AlternatingItemStyle CssClass="mr-r"/>
 
                                							
							<Columns>
								<asp:BoundColumn Visible=False DataField="GoodsRcvId" />
								<asp:BoundColumn Visible=False DataField="Status" />
								<asp:HyperLinkColumn HeaderText="Goods Receive ID" 
									SortExpression="A.GoodsRcvId" 
									DataNavigateUrlField="GoodsRcvId" 
									DataNavigateUrlFormatString="PU_trx_GRDet.aspx?GoodsRcvId={0}" 
									DataTextField="GoodsRcvId" />
								
								<asp:TemplateColumn HeaderText="Date" SortExpression="A.GoodsRcvRefDate">
									<ItemTemplate>
										<%# objGlobal.GetLongDate(Container.DataItem("GoodsRcvRefDate")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:HyperLinkColumn HeaderText="POID" 
									SortExpression="A.POID" 
									DataNavigateUrlField="GoodsRcvId" 
									DataNavigateUrlFormatString="PU_trx_GRDet.aspx?GoodsRcvId={0}" 
									DataTextField="POID" />
								
								<asp:TemplateColumn HeaderText="Supplier Name" SortExpression="Name">
									<ItemTemplate>
										<%# Container.DataItem("SupplierName") %>
										<asp:label id="lblSplCode" Text='<%# Trim(Container.DataItem("SupplierCode")) %>' Visible="False" Runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>

								<asp:TemplateColumn HeaderText="PO Issued" SortExpression="A.LocCode">
									<ItemTemplate>
										<%#Container.DataItem("LocCode")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="GR Loc" SortExpression="GRLocCode">
									<ItemTemplate>
										<%#Container.DataItem("GRLocCode")%>
									</ItemTemplate>
								</asp:TemplateColumn>
																
								<asp:TemplateColumn HeaderText="Dispatch/Issue ID" SortExpression="Name">
									<ItemTemplate>
										<%#Container.DataItem("DispAdvID")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Last Update" SortExpression="A.UpdateDate">
									<ItemTemplate>
										<%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
									
								<asp:TemplateColumn HeaderText="Status" SortExpression="A.Status">
									<ItemTemplate>
										<%# objPU.mtdGetGRStatus(Container.DataItem("Status")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
									
								<asp:TemplateColumn HeaderText="Updated By" SortExpression="C.UserName">
									<ItemTemplate>
										<%# Container.DataItem("UserName") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemTemplate>
										<asp:label id="lblStatus" Text='<%# Trim(Container.DataItem("Status")) %>' Visible="False" Runat="server" />
										<asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server/>
										<asp:LinkButton id=lbUndelete CommandName=Edit Text=Undelete runat=server/>
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
						            <td><asp:ImageButton id="btnPrev" runat="server" imageurl="../../../images/btprev.png" alternatetext="Previous" commandargument="prev" onClick="btnPrevNext_Click" /></td>
						            <td><asp:DropDownList id="lstDropList" AutoPostBack="True" onSelectedIndexChanged="PagingIndexChanged" CssClass="fontObject" runat="server" /></td>
			         	            <td><asp:Imagebutton id="btnNext" runat="server"  imageurl="../../../images/btnext.png" alternatetext="Next" commandargument="next" onClick="btnPrevNext_Click" /></td>
									<td><img height="18px" src="../../../images/btlast.png" width="18px" class="button" /></td>
								</tr>
							</table>
							</td>
						</tr>
						<tr>
							<td>
						        <asp:ImageButton id=NewGRBtn UseSubmitBehavior="false" onClick=NewGRBtn_Click imageurl="../../images/butt_new.gif" AlternateText="New Goods Receive" runat=server/>
						        <asp:Label id=SortCol Visible=False Runat="server" />
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

		</Form>
	</body>
</html>
