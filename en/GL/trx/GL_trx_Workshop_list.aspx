<%@ Page Language="vb" Trace="False" src="../../../include/GL_Trx_Workshop_List.aspx.vb" Inherits="GL_Workshop_List" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLTrx" src="../../menu/menu_GLTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Vehicle Usage List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
		<body>
		    <form runat="server" class="main-modul-bg-app-list-pu">
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="sortcol" Visible="False" Runat="server" />


            <table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuGLTrx id=menuGL runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong><asp:label id="lblTitle" runat="server" /> LIST</strong><hr style="width :100%" />   
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
								    <td width="30%" height="26">
									    <asp:label id="lblVehUsageId" runat="server" /> :<BR>
									    <asp:TextBox id=srchVehUsageID width=100% maxlength="20" runat="server"/>
								    </td>
								    <td width="25%" height="26">
									    <asp:label id="lblVehCode" runat="server" /> :<BR>
									    <asp:TextBox id=srchVehCode width=100% maxlength="8" runat="server"/>
								    </td>
								    <td width=8%>Period :<BR>
								        <asp:DropDownList id="lstAccMonth" width=100% runat=server>
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
								        <asp:DropDownList id="lstAccYear" width=100% runat=server>
								    </asp:DropDownList>
								    <td width="15%" height="26">
									    Status :<BR><asp:DropDownList id="srchStatusList" width=100% runat=server/>
								    </td>
								    <td width="20%" height="26">
									    Last Updated By :<BR><asp:TextBox id=srchUpdateBy width=100% maxlength="128" runat="server"/>
								    </td>
								
								    <td width="10%" height="26" valign=bottom align=right><asp:Button id="SearchBtn" Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
                                </tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
						            <asp:DataGrid id="dgTx"
						                AutoGenerateColumns="false" width="100%" runat="server"
						                GridLines = none
						                Cellpadding = "2"
						                AllowPaging="True" 
						                Allowcustompaging="False"
						                Pagesize="15" 
						                OnPageIndexChanged="OnPageChanged"
						                Pagerstyle-Visible="False"
						                OnSortCommand="Sort_Grid" 
						                AllowSorting="True"
                                                                        class="font9Tahoma">
								
							                                            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                                            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                                            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>			
					                <Columns>
						                <asp:HyperLinkColumn
								                DataNavigateUrlField="VehUsageID"
								                DataNavigateUrlFormatString="GL_Trx_Workshop_Details.aspx?id={0}"
								                DataTextField="VehUsageID"
								                DataTextFormatString="{0:c}"
								                SortExpression="VehU.VehUsageID"/>
						                <asp:HyperLinkColumn
								                DataNavigateUrlField="VehUsageID"
								                DataNavigateUrlFormatString="GL_Trx_Workshop_Details.aspx?id={0}"
								                DataTextField="VehCode"
								                DataTextFormatString="{0:c}"
								                SortExpression="VehU.VehCode"/>
						                <asp:TemplateColumn SortExpression="Veh.Description">
							                <ItemTemplate>
								                <%# Container.DataItem("Description")%>
							                </ItemTemplate>
						                </asp:TemplateColumn>
						                <asp:TemplateColumn HeaderText="Last Update" SortExpression="VehU.UpdateDate">
							                <ItemTemplate>
								                <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
							                </ItemTemplate>
						                </asp:TemplateColumn>
						                <asp:TemplateColumn HeaderText="Status" SortExpression="VehU.Status">
							                <ItemTemplate>
								                <%# objGLtx.mtdGetVehicleUsageStatus(Container.DataItem("Status")) %>
							                </ItemTemplate>
						                </asp:TemplateColumn>
						                <asp:TemplateColumn HeaderText="Updated By" SortExpression="UserName">
							                <ItemTemplate>
								                <%# Container.DataItem("UserName") %>
								                <asp:label id="lblTxID" Text=<%# Container.DataItem("VehUsageID") %> Visible="False" Runat="server"></asp:label>
								                <asp:label id="lblStatus" Text=<%# Container.DataItem("Status") %> Visible="False" Runat="server"></asp:label>
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
					            <asp:ImageButton id=Usage OnClick=btnNewItm_Click imageurl="../../images/butt_new.gif" AlternateText="New Workshop" runat="server"/>
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
