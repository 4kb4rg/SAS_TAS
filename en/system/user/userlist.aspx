<%@ Page Language="vb" src="../../../include/system_user_userlist.aspx.vb" Inherits="system_user_userlist" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuSYS" src="../../menu/menu_sys.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>

<head>
<title>User List</title>
    <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
<Preference:PrefHdl id=PrefHdl runat="server" />
</head>
	<body>
	    <form id=frmUserList class="main-modul-bg-app-list-pu" runat="server">
            <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma" >
		    <tr>
             <td style="width: 100%; height: 800px" valign="top" class="font9Tahoma" >
			    <div class="kontenlist"> 

			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<table border="0" cellspacing="1" cellpadding="2" bordercolor="#111111" width="100%" style="border-collapse: collapse" class="font9Tahoma" >
				<tr>
					<td colspan="6">
						<UserControl:MenuSYS id=MenuSYS runat="server" />
					</td>
				</tr>
				<tr>
					<td class="font9Tahoma" colspan="3"><strong>APPLICATION USER LIST</strong> </td>
					<td colspan="3" align=right><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6><hr style="width :100%" /></td>
				</tr>
				<tr>
					<td colspan=6 width=100% class="mb-c">
						<table width="100%" cellspacing="0" cellpadding="3" border="0" align="center" class="font9Tahoma" >
							<tr style="background-color:#FFCC00" >
								<td valign=bottom align=left width=15%>User ID :<BR><asp:TextBox id=txtUserId width=100% maxlength="20" CssClass="font9Tahoma" runat="server" /></td>
								<td valign=bottom align=left width=45%>Name :<BR><asp:TextBox id=txtName width=100% maxlength="128" CssClass="font9Tahoma"  runat="server"/></td>
								<td valign=bottom align=left width=10%>Status :<BR>
									<asp:DropDownList id="ddlStatus" width=100%  CssClass="font9Tahoma"  runat=server>
										<asp:ListItem value="0" >All</asp:ListItem>
										<asp:ListItem value="1" Selected>Active</asp:ListItem>
										<asp:ListItem value="2">Inactive</asp:ListItem>
										<asp:ListItem value="3">Deleted</asp:ListItem>
									</asp:DropDownList>
								</td>
								<td valign=bottom align=left width=20%>LastUpdate By :<BR><asp:TextBox id=txtLastUpdate width=100% maxlength="10"  CssClass="font9Tahoma" runat="server"/></td>
								<td valign=bottom align=left width=10%><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click CssClass="button-small" runat="server"/></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan=6>					
						<asp:DataGrid id=dgUserList
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
							AllowSorting=True class="font9Tahoma">							
					        
                            <HeaderStyle  BackColor="#CCCCCC" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<ItemStyle BackColor="#FEFEFE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<AlternatingItemStyle BackColor="#EEEEEE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>	
							
							<Columns>
							
								<asp:BoundColumn Visible=False HeaderText="User ID" DataField="UserID" />
								<asp:HyperLinkColumn HeaderText="User ID" 
													 SortExpression="UserID" 
													 DataNavigateUrlField="UserID" 
													 DataNavigateUrlFormatString="userdet.aspx?userid={0}" 
													 DataTextField="UserID" />
								
								<asp:HyperLinkColumn HeaderText="Name" 
													 SortExpression="UserName" 
													 DataNavigateUrlField="UserID" 
													 DataNavigateUrlFormatString="userdet.aspx?userid={0}" 
													 DataTextField="UserName" />
							
							    <asp:TemplateColumn HeaderText="Department" SortExpression="DeptCode">
									<ItemTemplate>
										<%# Container.DataItem("DeptCode") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Last Update" SortExpression="UpdateDate">
									<ItemTemplate>
										<%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="User Status" SortExpression="Status">
									<ItemTemplate>
										<%# objSysCfg.mtdGetUserStatus(Container.DataItem("Status")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Updated By" SortExpression="UpdateID">
									<ItemTemplate>
										<%# Container.DataItem("UpdateID") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn ItemStyle-HorizontalAlign=Right>
									<ItemTemplate>
										<asp:Label id=lblUserId visible=false Text='<%# Trim(Container.DataItem("UserId")) %>' runat=server/>
										<asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server />
										<asp:Label ID="lblStatus" Text=<%# Container.DataItem("Status") %> Visible="False" RunAt="Server" />
									</ItemTemplate>
								</asp:TemplateColumn>	
							</Columns>
						</asp:DataGrid>
					</td>
				</tr>
				<tr>
					<td align=right colspan="6">
						<asp:ImageButton id="btnPrev" runat="server" 
                            imageurl="../../../images/btprev.png" alternatetext="Previous" 
                            commandargument="prev" onClick="btnPrevNext_Click" />
						<asp:DropDownList id="lstDropList" AutoPostBack="True" onSelectedIndexChanged="PagingIndexChanged" CssClass="font9Tahoma"  runat="server" />
			         	<asp:Imagebutton id="btnNext" runat="server"  
                            imageurl="../../../images/btnext.png" alternatetext="Next" 
                            commandargument="next" onClick="btnPrevNext_Click" />
					</td>
				</tr>
				<tr>
					<td align="left" width="100%" ColSpan=6>
						<asp:ImageButton id=NewUserBtn onClick=NewUserBtn_Click imageurl="../../images/butt_new.gif" alternatetext="New Application User" runat="server"/>
						<asp:ImageButton id=PrintBtn Visible=False imageurl="../../images/butt_print.gif" alternatetext=Print runat="server"/>
						<asp:Label id=SortCol Visible=False Runat="server" />
						<asp:Label id=lblErrMesage visible=false Text="Error while initiating component." runat=server />
					</td>
				</tr>
			</table>
                </div>
            </td>
            </tr>
            </table>
		</FORM>
	</body>
</html>
