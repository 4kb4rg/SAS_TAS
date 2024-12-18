<%@ Page Language="vb" src="../../../include/PU_setup_UserGroupList.aspx.vb" Inherits="PU_setup_UserGroupList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPU" src="../../menu/menu_pusetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<html>
	<head>
		<title>Supplier List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<%--<Preference:PrefHdl id=PrefHdl runat="server" />--%>
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
							<td><strong>SUPPLIER USER LIST</strong><hr style="width :100%" />   
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
								<td width="15%" height="26">
                                    User ID &nbsp;:<BR><asp:TextBox id=txtUserCode width=100% maxlength="20" runat="server" /></td>
								<td width="35%" height="26">
                                    User Name Name :<BR><asp:TextBox id=txtName width=100% maxlength="128" runat="server"/></td>
								<td width="15%" height="26">
                                    Type Produk<asp:TextBox id=txtTypeProduk width=100% maxlength="128" runat="server"/></td>
								<td width="10%" height="26">Status :<BR>
									<asp:DropDownList id="ddlStatus" width=100% runat=server>
										<asp:ListItem value="0" >All</asp:ListItem>
										<asp:ListItem value="1" Selected>Active</asp:ListItem>
										<asp:ListItem value="2">Deleted</asp:ListItem>
									</asp:DropDownList>
								</td>
								<td width="15%" height="26"></td>
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
							            AutoGenerateColumns=False width=100% runat=server
							            GridLines=None 
							            Cellpadding=2 
							            AllowPaging=True 
							            Pagesize=100 
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
								            <asp:BoundColumn Visible=False HeaderText="UserID" DataField="UserID" />

								            <asp:HyperLinkColumn HeaderText="User ID" 
									            SortExpression="ID" 
									            DataNavigateUrlField="ID" 
									            DataNavigateUrlFormatString="PU_setup_UserGroup.aspx?SuppCode={0}" 
									            DataTextField="ID" />
								
								            <asp:HyperLinkColumn HeaderText="User Name" 
									            SortExpression="IDName" 
									            DataTextField="IDName" />
									
							                <asp:TemplateColumn HeaderText="Type Produk" SortExpression="SuppType">
									            <ItemTemplate>
										            <%#Container.DataItem("ProdTypeCode")%>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="Description" SortExpression="TypeName">
									            <ItemTemplate>
										            <%#Container.DataItem("TypeName")%>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="Last Update" SortExpression="UDate">
									            <ItemTemplate>
										            <%#objGlobal.GetLongDate(Container.DataItem("UDate"))%>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								
								            <asp:TemplateColumn HeaderText="Updated By" SortExpression="UpdateID">
									            <ItemTemplate>
										            <%#Container.DataItem("UpdateID")%>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="Status" SortExpression="Status">
									            <ItemTemplate>
										            <%#Container.DataItem("Status")%>
									            </ItemTemplate>
								            </asp:TemplateColumn>				
							            </Columns>
                                        <PagerStyle Visible="False" />
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
					            <asp:ImageButton id=NewSuppBtn onClick=NewSuppBtn_Click imageurl="../../images/butt_new.gif" AlternateText="New Supplier" runat=server/>&nbsp;
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
