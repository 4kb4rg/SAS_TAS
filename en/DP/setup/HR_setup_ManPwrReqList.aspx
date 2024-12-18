<%@ Page Language="vb" src="../../../include/HR_setup_ManPwrReqList.aspx.vb" Inherits="HR_setup_ManPwrReqList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHRSetup" src="../../menu/menu_hrsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Man Power Requirement List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<%--<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	</head>
	<body>
		<form id=frmList runat="server" class="main-modul-bg-app-list-pu">
    		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="SortExpression" Visible="False" Runat="server" />


		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuHRSetup id=MenuHRSetup runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>MAN POWER REQUIREMENT LIST</strong><hr style="width :100%" />   
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
								<td width="15%" height="26" valign=bottom>M.P.Requirement Code :<BR><asp:TextBox id=txtManPwrReqCode width=100% maxlength="8" runat="server" /></td>
								<td width="40%" height="26" valign=bottom>Description :<BR><asp:TextBox id=txtDescription width=100% maxlength="8" runat="server" /></td>
								<td width="15%" height="26" valign=bottom>Status :<BR>
									<asp:DropDownList id="ddlStatus" width=100% runat=server>
										<asp:ListItem value="0">All</asp:ListItem>
										<asp:ListItem value="1" selected>Active</asp:ListItem>
										<asp:ListItem value="2">Deleted</asp:ListItem>
									</asp:DropDownList>
								</td>
								<td width="20%" height="26" align=left valign=bottom>LastUpdate By :<BR><asp:TextBox id=txtLastUpdate width=100% maxlength="10" runat="server"/></td>
								<td width="10%" height="26" valign=bottom align=left><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
								</tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
						            <asp:DataGrid id=dgLine runat=server
							            AutoGenerateColumns=false width=100% 
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
                                                    class="font9Tahoma">
								
							                        <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                        <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                        <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
							
							            <Columns>
								            <asp:BoundColumn Visible=False HeaderText="M.P.Requirement Code" DataField="ManPwrReqCode" />
								            <asp:HyperLinkColumn HeaderText="M.P.Requirement Code" ItemStyle-Width="15%" HeaderStyle-Width="15%"
									            SortExpression="MPR.ManPwrReqCode" 
									            DataNavigateUrlField="ManPwrReqCode" 
									            DataNavigateUrlFormatString="HR_setup_ManPwrReqDet.aspx?mprcode={0}" 
									            DataTextField="ManPwrReqCode" />
									
								            <asp:HyperLinkColumn HeaderText="Description" ItemStyle-Width="40%" HeaderStyle-Width="40%"
									            SortExpression="MPR.Description" 
									            DataNavigateUrlField="ManPwrReqCode" 
									            DataNavigateUrlFormatString="HR_setup_ManPwrReqDet.aspx?mprcode={0}" 
									            DataTextField="Description" />
								
								            <asp:TemplateColumn HeaderText="Last Update" ItemStyle-Width="10%" HeaderStyle-Width="10%" SortExpression="MPR.UpdateDate">
									            <ItemTemplate>
										            <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
									
								            <asp:TemplateColumn HeaderText="Status" ItemStyle-Width="10%" HeaderStyle-Width="10%" SortExpression="MPR.Status">
									            <ItemTemplate>
										            <%# objHRSetup.mtdGetDeptStatus(Container.DataItem("Status")) %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
									
								            <asp:TemplateColumn HeaderText="Updated By" ItemStyle-Width="10%" HeaderStyle-Width="10%" SortExpression="USR.UserName">
									            <ItemTemplate>
										            <%# Container.DataItem("UserName") %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn ItemStyle-Width="15%" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign=Right>
									            <ItemTemplate>
										            <asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server />
									            </ItemTemplate>
								            </asp:TemplateColumn>	
							            </Columns>
						            </asp:DataGrid>
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
					            <asp:ImageButton id=NewMPRBtn OnClick="NewMPRBtn_Click" imageurl="../../images/butt_new.gif" AlternateText="New Man Power Requirement" runat="server"/>
						        <asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print visible=false runat="server"/>
						        <asp:Label id=SortCol Visible=False Runat="server" />
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
