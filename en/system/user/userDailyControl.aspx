<%@ Page Language="vb" src="../../../include/System_user_DailyControl.aspx.vb" Inherits="System_user_DailyControl" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuSYS" src="../../menu/menu_sys.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
<head>
<title>User List</title>
<Preference:PrefHdl id=PrefHdl runat="server" />
          <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
<script language="javascript" type="text/javascript">
// <!CDATA[

function HR1_onclick() {

}

// ]]>
</script>
</head>
	<body>
	    <form id=frmUserList class="main-modul-bg-app-list-pu" runat="server">
            <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma" >
		    <tr>
             <td style="width: 100%; height: 800px" valign="top" class="font9Tahoma" >
			    <div class="kontenlist"> 

			    <table border="0" cellspacing="1" cellpadding="2" bordercolor="#111111" width="100%" style="border-collapse: collapse">
				<tr>
					<td colspan="6" style="height: 23px">
						<UserControl:MenuSYS id=MenuSYS runat="server" />
			<asp:label id="SortExpression" Visible="False" Runat="server" /></td>
				</tr>
				<tr>
					<td class="font12Tahoma" colspan="3">USER DAILY CONTROL</td>
					<td colspan="3" align=right><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6><hr style="width :100%" /></td>
				</tr>
				<tr style="background-color:#FFCC00" >
					<td colspan=6 width=100%  >
						<table width="100%" cellspacing="0" cellpadding="3" border="0" align="center" class="font9Tahoma">
							<tr>
								<td valign=bottom align=left width=15% style="height: 44px">User Level :<BR>
                                    <asp:DropDownList ID="ddlUser" runat="server" Width="80%">
                                    </asp:DropDownList></td>
								<td valign=bottom align=left width=45% style="height: 44px"><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click CssClass="button-small" runat="server"/></td>
								<td valign=bottom align=left width=10% style="height: 44px">
								</td>
								<td valign=bottom align=left width=20% style="height: 44px"></td>
								<td valign=bottom align=left width=10% style="height: 44px"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan=6>					
						<asp:DataGrid id=dgUserList
							AutoGenerateColumns=False width=80% runat=server
							GridLines=None 
							Cellpadding=2 
							AllowPaging=True 
							Pagesize=15 
							OnPageIndexChanged=OnPageChanged
							Pagerstyle-Visible=False 
							OnDeleteCommand=DEDR_Delete 
							OnSortCommand=Sort_Grid  
                             OnItemDataBound="dgLine_BindGrid"  
							AllowSorting=True>
							
							<HeaderStyle CssClass="mr-h"/>
							
							<ItemStyle CssClass="mr-l"/>
							
							<AlternatingItemStyle CssClass="mr-r"/>
							
							<Columns>
							
								<asp:TemplateColumn HeaderText="User Level" SortExpression="UsrLevel" Visible="False">
									<ItemTemplate>
										<%#Container.DataItem("UsrLevel")%>
										<asp:label text= '<%# Container.DataItem("UsrLevel") %>' id="lblUserLevel" runat="server" /><br>
									</ItemTemplate>
								</asp:TemplateColumn>
								
											<asp:HyperLinkColumn HeaderText="UsrLevel" 
													 SortExpression="UsrLevel" 
													 DataNavigateUrlFormatString="userDailyControl_Det.aspx?UsrLevel={0}" 
													 DataTextField="UsrLevel" />											 
								 <asp:TemplateColumn HeaderText="Location" SortExpression="LocCode">
									<ItemTemplate>
										<%#Container.DataItem("LocCode")%>
										<asp:label text= '<%# Container.DataItem("LocCode") %>' id="lblLocCode" runat="server" Visible="False" /><br>
               									</ItemTemplate>
								</asp:TemplateColumn>
	                                <asp:TemplateColumn HeaderText="Maximum Day" SortExpression="MaximumDay">
									<ItemTemplate>
										<%#Container.DataItem("MaximumDay")%>
									</ItemTemplate>
								</asp:TemplateColumn>
							
								<asp:TemplateColumn HeaderText="Last Update" SortExpression="UpdateDate">
									<ItemTemplate>
										<%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Updated By" SortExpression="UpdateID">
									<ItemTemplate>
										<%# Container.DataItem("UpdateID") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemTemplate>
										<asp:Label id=lblUserId visible=false Text='<%# Trim(Container.DataItem("UsrLevel")) %>' runat=server/>
										<asp:LinkButton id=lblEdit CommandName=Delete Text=Edit runat=server />
										</ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
								</asp:TemplateColumn>	
							</Columns>
                            <PagerStyle Visible="False" />
						</asp:DataGrid>
                        <asp:Label ID="lblErrMessage" runat="server" Text="Error while initiating component."
                            Visible="false"></asp:Label></td>
				</tr>
				<tr>
					<td align=right colspan="6">
                        &nbsp;<asp:ImageButton id="btnPrev" runat="server" imageurl="../../images/icn_prev.gif" alternatetext="Previous" commandargument="prev" onClick="btnPrevNext_Click" />
						<asp:DropDownList id="lstDropList" AutoPostBack="True" onSelectedIndexChanged="PagingIndexChanged" runat="server" />
			         	<asp:Imagebutton id="btnNext" runat="server"  imageurl="../../images/icn_next.gif" alternatetext="Next" commandargument="next" onClick="btnPrevNext_Click" />
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
