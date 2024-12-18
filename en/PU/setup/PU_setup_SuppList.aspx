<%@ Page Language="vb" src="../../../include/PU_setup_SuppList.aspx.vb" Inherits="PU_SuppList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPU" src="../../menu/menu_pusetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<html>
	<head>
		<title>Supplier List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
<%--		<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	</head>
	<body>
	    <form id=frmSuppList runat=server class="main-modul-bg-app-list-pu">
			<asp:Label id=SortExpression visible=false runat=server />
			<asp:Label id=SortCol visible=false runat=server />
			<asp:Label id=lblErrMessage visible=false text="Error while initiating component." runat=server />

		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuPU id=MenuPU runat=server />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>SUPPLIER LIST</strong><hr style="width :100%" />   
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
								<td width="15%" height="26">Supplier Code :<BR><asp:TextBox id=txtSuppCode width=100% maxlength="20" CssClass="fontObject" runat="server" /></td>
								<td width="35%" height="26">Name :<BR><asp:TextBox id=txtName width=100% maxlength="128" CssClass="fontObject" runat="server"/></td>
								<td width="15%" height="26">Type :<BR>
									<asp:DropDownList id="ddlSuppType" width=100% CssClass="fontObject" runat=server>
										<asp:ListItem value="0" Selected>All</asp:ListItem>
										<asp:ListItem value="1">Internal</asp:ListItem>
										<asp:ListItem value="2">External</asp:ListItem>
										<asp:ListItem value="3">Associate</asp:ListItem>
										<asp:ListItem value="4">Contractor</asp:ListItem>
										<asp:ListItem value="5">FFB Supplier</asp:ListItem>
									</asp:DropDownList>
								</td>
								<td width="10%" height="26">Status :<BR>
									<asp:DropDownList id="ddlStatus" width=100% CssClass="fontObject" runat=server>
										<asp:ListItem value="0" >All</asp:ListItem>
										<asp:ListItem value="1" Selected>Active</asp:ListItem>
										<asp:ListItem value="2">Deleted</asp:ListItem>
									</asp:DropDownList>
								</td>
								<td width="15%" height="26">Last Updated By :<BR><asp:TextBox id=txtLastUpdate width=100% maxlength="128" CssClass="fontObject" runat="server"/></td>
								<td width="8%" height="26" valign=bottom align=right><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
								</tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
						            <asp:DataGrid id=dgSuppList
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
							                AllowSorting=True >
<HeaderStyle CssClass="mr-h"/>
<ItemStyle CssClass="mr-l"/>
<AlternatingItemStyle CssClass="mr-r"/>	
							                <Columns>
								                <asp:BoundColumn Visible=False HeaderText="Supplier Code" DataField="SupplierCode" />

								                <asp:HyperLinkColumn HeaderText="Supplier Code" 
									                SortExpression="SupplierCode" 
									                DataNavigateUrlField="SupplierCode" 
									                DataNavigateUrlFormatString="PU_setup_SuppDet.aspx?SuppCode={0}" 
									                DataTextField="SupplierCode" />
								
								                <asp:HyperLinkColumn HeaderText="Name" 
									                SortExpression="Name" 
									                DataNavigateUrlField="SupplierCode" 
									                DataNavigateUrlFormatString="PU_setup_SuppDet.aspx?SuppCode={0}" 
									                DataTextField="Name" />

									
							                    <asp:TemplateColumn HeaderText="Type" SortExpression="SuppType">
									                <ItemTemplate>
										                <%#objPU.mtdGetSupplierType(Container.DataItem("SuppType"))%>
									                </ItemTemplate>
								                </asp:TemplateColumn>
								                <asp:TemplateColumn HeaderText="PPN" SortExpression="PPNDescr">
									                <ItemTemplate>
										                <%#Container.DataItem("PPNDescr")%>
									                </ItemTemplate>
								                </asp:TemplateColumn>
								                <asp:TemplateColumn HeaderText="NPWP No" SortExpression="NPWPNo">
									                <ItemTemplate>
										                <%#Container.DataItem("NPWPNo")%>
									                </ItemTemplate>
								                </asp:TemplateColumn>
								                <asp:TemplateColumn HeaderText="NPWP Name" SortExpression="NPWPName">
									                <ItemTemplate>
										                <%#Container.DataItem("NPWPName")%>
									                </ItemTemplate>
								                </asp:TemplateColumn>
								                <asp:TemplateColumn HeaderText="Last Update" SortExpression="UpdateDate">
									                <ItemTemplate>
										                <%#objGlobal.GetLongDate(Container.DataItem("UpdateDate"))%>
									                </ItemTemplate>
								                </asp:TemplateColumn>
								
								                <asp:TemplateColumn HeaderText="Status" SortExpression="Status">
									                <ItemTemplate>
										                <%# objPU.mtdGetSuppStatus(Container.DataItem("Status")) %>
									                </ItemTemplate>
								                </asp:TemplateColumn>
								
								                <asp:TemplateColumn HeaderText="Updated By" SortExpression="UpdateID">
									                <ItemTemplate>
										                <%# Container.DataItem("UserName") %>
									                </ItemTemplate>
								                </asp:TemplateColumn>
								                <asp:TemplateColumn>
									                <ItemTemplate>
										                <asp:Label id="lblStatus" visible="False"
											                Text='<%# trim(Container.DataItem("Status")) %>'   runat="server"/>
										                <asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" runat="server"/>
									                </ItemTemplate>
								                </asp:TemplateColumn>
							                </Columns>
						                </asp:DataGrid><br />
                                        <asp:CheckBox ID="cbExcel" CssClass="font9Tahoma"  runat="server" Checked="false" Text=" Export To Excel" /><BR>
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
									<td><asp:DropDownList ID="lstDropList" runat="server" AutoPostBack="True" CssClass="fontObject" onSelectedIndexChanged="PagingIndexChanged" /></td>
									<td><asp:ImageButton ID="btnNext" runat="server" alternatetext="Next" commandargument="next" imageurl="../../../images/btnext.png" onClick="btnPrevNext_Click" /></td>
									<td><img height="18px" src="../../../images/btlast.png" width="18px" class="button" /></td>
								</tr>
							</table>
							</td>
						</tr>
						<tr>
							<td>
					            <asp:ImageButton id=NewSuppBtn onClick=NewSuppBtn_Click imageurl="../../images/butt_new.gif" AlternateText="New Supplier" runat=server/>
					        	<asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print onClick="btnPreview_Click" runat="server"/>
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
