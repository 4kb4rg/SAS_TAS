<%@ Page Language="vb" src="../../../include/PU_trx_GRNList.aspx.vb" Inherits="PU_GRNList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPU" src="../../menu/menu_putrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Goods Return List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<%--<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	</head>
	<body>
		<form id=frmPOList runat="server" class="main-modul-bg-app-list-pu">
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:Label id=SortCol Visible=False Runat="server" />

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
							<td><strong>GOODS RETURN LIST</strong><hr style="width :100%" />   
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
								<td width="15%" height="26">Goods Return ID :<BR><asp:TextBox id=txtGoodsRetId width=100% maxlength="32" CssClass="fontObject" runat="server" /></td>
								<td width="17%" height="26">Goods Return Type :<BR><asp:DropDownList id="ddlGRNType" width=100% CssClass="fontObject" runat=server /></td>
								<td width="13%" height="26">Supplier Code :<BR><asp:TextBox id=txtSupplierCode width=100% maxlength="20" CssClass="fontObject" runat="server"/></td>
								<td width="17%" height="26">Supplier Name :<BR><asp:TextBox id=txtName width=100% maxlength="128" CssClass="fontObject" runat="server"/></td>
								<td width=8%>Period :<BR>
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
								<td width=10%><BR>
								    <asp:DropDownList id="lstAccYear" width=100% CssClass="fontObject" runat=server>
									</asp:DropDownList>
								<td width="10%" height="26">Status :<BR>
									<asp:DropDownList id="ddlStatus" width=100% CssClass="fontObject" runat=server>
										<asp:ListItem value="0">All</asp:ListItem>
										<asp:ListItem value="1">Active</asp:ListItem>
										<asp:ListItem value="4">Cancelled</asp:ListItem>
										<asp:ListItem value="2">Confirmed</asp:ListItem>
										<asp:ListItem value="3">Deleted</asp:ListItem>
									</asp:DropDownList>
								</td>
								<td width="15%" height="26" ><BR><asp:TextBox id=txtLastUpdate width=100% maxlength="128" visible=false CssClass="fontObject" runat="server"/></td>
								<td width="10%" height="26" valign=bottom align=right><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
								</tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
						<asp:DataGrid id=dgGRNList runat=server
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
							OnSortCommand=Sort_Grid 
                             OnItemDataBound="dgLine_BindGrid" 
                            class="font9Tahoma" 
							AllowSorting=True>
<HeaderStyle CssClass="mr-h"/>
<ItemStyle CssClass="mr-l"/>
<AlternatingItemStyle CssClass="mr-r"/>
														
							<Columns>
								<asp:BoundColumn Visible=False DataField="GoodsRetId" />
								<asp:BoundColumn Visible=False DataField="Status" />
								
								<asp:HyperLinkColumn HeaderText="Goods Return ID" 
									SortExpression="GoodsRetId" 
									DataNavigateUrlField="GoodsRetId" 
									DataNavigateUrlFormatString="PU_trx_GRNDet.aspx?GoodsRetId={0}" 
									DataTextField="GoodsRetId" />
									
								<asp:HyperLinkColumn HeaderText="PO ID" 
									SortExpression="D.POID" 
									DataNavigateUrlField="GoodsRetId" 
									DataNavigateUrlFormatString="PU_trx_GRNDet.aspx?GoodsRetId={0}" 
									DataTextField="POID" />
									
								<asp:HyperLinkColumn HeaderText="Supplier Code" 
									SortExpression="A.SupplierCode" 
									DataNavigateUrlField="GoodsRetId" 
									DataNavigateUrlFormatString="PU_trx_GRNDet.aspx?GoodsRetId={0}" 
									DataTextField="SupplierCode" />
									
								<asp:TemplateColumn HeaderText="Supplier Name" SortExpression="B.SupplierName">
									<ItemTemplate>
										<%# Container.DataItem("SupplierName") %>
									</ItemTemplate>
								</asp:TemplateColumn>

								<asp:TemplateColumn HeaderText="Goods Return Type" SortExpression="A.GoodsRetType">
									<ItemTemplate>
										<%# Container.DataItem("GoodsRetType") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Last Update" SortExpression="A.UpdateDate">
									<ItemTemplate>
										<%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
									
								<asp:TemplateColumn HeaderText="Status" SortExpression="A.Status">
									<ItemTemplate>
										<%# objPU.mtdGetGRNStatus(Container.DataItem("Status")) %>
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
						        <asp:ImageButton id=NewINGRNBtn UseSubmitBehavior="false" onClick=NewINGRNBtn_Click imageurl="../../images/butt_new_stockgr.gif" AlternateText="New Stock/Workshop Goods Return" runat=server/>
						        <asp:ImageButton id=NewDCGRNBtn UseSubmitBehavior="false" onClick=NewDCGRNBtn_Click imageurl="../../images/butt_new_directchargegr.gif" AlternateText="New Direct Charge Goods Return" runat=server/>
						        <asp:ImageButton id=NewFAGRNBtn UseSubmitBehavior="false" onClick=NewFAGRNBtn_Click imageurl="../../images/butt_new_fixedassetgr.gif" AlternateText="New Fixed Asset Goods Return" runat=server/>
						        <a href="#" onclick="javascript:popwin(200, 400, 'PU_trx_PrintDocs.aspx?doctype=2')"><asp:Image id="ibPrintDoc" runat="server" ImageUrl="../../images/butt_print_doc.gif"/></a>
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
