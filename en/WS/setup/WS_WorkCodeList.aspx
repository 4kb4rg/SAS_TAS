<%@ Page Language="vb" src="../../../include/WS_setup_workcodelist.aspx.vb" Inherits="WS_WorkCodeList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuWS" src="../../menu/menu_wssetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Work Code List</title>
         <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	<%--	<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	</head>
	<body>
		<form id=frmWorkCodeList runat="server"  class="main-modul-bg-app-list-pu">
    		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
    		<asp:label id=lblCode visible=false text=" Code" runat=server />
			<asp:label id="SortExpression" Visible="False" Runat="server" />

  
		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuWS id=menuWS runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong><asp:label id="lblTitle" runat="server" /> CODE LIST</strong><hr style="width :100%" />   
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
								<td width="20%" height="26"><asp:label id=lblWork runat=server /> Code :<BR><asp:TextBox id=txtWorkCode width=100% maxlength="8" runat="server" /></td>
								<td width="37%" height="26"><asp:label id=lblWorkDesc runat=server /> :<BR><asp:TextBox id=txtDescription width=100% maxlength="256" runat="server"/></td>
								<td width="15%" height="26">Status :<BR>
									<asp:DropDownList id="ddlStatus" width=100% runat=server>
										<asp:ListItem value="0">All</asp:ListItem>
										<asp:ListItem value="1" selected>Active</asp:ListItem>
										<asp:ListItem value="2">Deleted</asp:ListItem>
									</asp:DropDownList>
								</td>
								<td width="20%" height="26">Last Updated By :<BR><asp:TextBox id=txtLastUpdate width=100% maxlength="128" runat="server"/></td>
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
						             <asp:DataGrid id=dgWorkCode runat=server
							            AutoGenerateColumns=false width=100% 
							            GridLines=none 
							            Cellpadding=2 
							            AllowPaging=True 
							            Allowcustompaging=False 
							            Pagesize=15 
							            OnPageIndexChanged=OnPageChanged 
							            Pagerstyle-Visible=False 							 
							            OnSortCommand=Sort_Grid  
							            AllowSorting=True
                                                    class="font9Tahoma">
								
							                        <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                        <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                        <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
							            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC" Font-Bold="False" 
                                            Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                            Font-Underline="False"/>
							            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE" Font-Bold="False" 
                                            Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                            Font-Underline="False"/>
							            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE" Font-Bold="False" 
                                            Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                            Font-Underline="False"/>
							            <Columns>
								            <asp:BoundColumn Visible=False HeaderText="Work Code" DataField="WorkCode" />
								            <asp:HyperLinkColumn  
									            SortExpression="WorkCode" 
									            DataNavigateUrlField="WorkCode" 
									            DataNavigateUrlFormatString="WS_WorkCodeDet.aspx?WorkCode={0}" 
									            DataTextField="WorkCode" >
									            <ItemStyle width=15% />						
								            </asp:HyperLinkColumn > 
								
								            <asp:HyperLinkColumn
									            SortExpression="Description" 
									            DataNavigateUrlField="WorkCode" 
									            DataNavigateUrlFormatString="WS_WorkCodeDet.aspx?WorkCode={0}" 
									            DataTextField="Description" >
									            <ItemStyle width=30% />						
								            </asp:HyperLinkColumn > 
									
								            <asp:TemplateColumn HeaderText="Last Update" SortExpression="UpdateDate">
									            <ItemStyle width=15% />						
									            <ItemTemplate>
										            <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="Status" SortExpression="Status">
									            <ItemStyle width=10% />						
									            <ItemTemplate>
										            <%# objWS.mtdGetStatus(Container.DataItem("Status")) %>
									            </ItemTemplate>
								            </asp:TemplateColumn>							
								            <asp:TemplateColumn HeaderText="Updated By" SortExpression="UpdateID">
									            <ItemStyle width=20% />						
									            <ItemTemplate>
										            <%# Container.DataItem("UserName") %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn>
									            <ItemStyle width=10% />						
									            <ItemTemplate>
										
										            <asp:Label id=lblStatus Text='<%# Trim(Container.DataItem("Status")) %>' Visible=False runat=server />
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
					            <asp:ImageButton id=NewWorkCodeBtn onClick=NewWorkCodeBtn_Click imageurl="../../images/butt_new.gif" AlternateText="New Work Code" runat=server/>
						        <asp:ImageButton id=ibPrint onClick="btnPreview_Click" imageurl="../../images/butt_print.gif" AlternateText=Print runat="server"/>
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
