<%@ Page Language="vb" Trace="False" src="../../../include/GL_Trx_RunningHour_List.aspx.vb" Inherits="GL_RunningHour_List" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLTrx" src="../../menu/menu_GLTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Actual Running Hour List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
 
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
		<body>
		    <form id=frmMain runat="server" class="main-modul-bg-app-list-pu">
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
							<td><strong>ACTUAL STATION RUNNING HOUR LIST</strong><hr style="width :100%" />   
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
								    <td width="20%" height="26">Act. Running Hour ID :<BR>
									    <asp:TextBox id=srchRunHourID width=100% maxlength="20" runat="server"/>
								    </td>
								    <td width="15%" height="26">Transaction Date :<BR>
									    <asp:TextBox id=srchTrxDate width=65% maxlength="20" runat="server"/>
									    <a href="javascript:PopCal('srchTrxDate');"><asp:Image id="btnTrxDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>							
								    </td>
								    <td width="20%" height="26">
									    <asp:label id="lblBlkCode" runat="server" /> :<BR>
									    <asp:TextBox id=srchBlkCode width=100% maxlength="8" runat="server"/>
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
								    <td width="15%" height="26">
									    Last Updated By :<BR><asp:TextBox id=srchUpdateBy width=100% maxlength="128" runat="server"/>
								    </td>
								    <td width="10%" height="26" valign=bottom align=right><asp:Button  id="SearchBtn" Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
							    </tr>
							    <tr  class="font9Tahoma">
								    <td valign=top colspan=8>
									    <asp:label id=lblErrDate Text ="Date entered should be in the format " forecolor=red visible=false runat="server"/>
									    <asp:label id=lblDateFormat forecolor=red visible=false runat=server />
								    </td>
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
						                <asp:HyperLinkColumn HeaderText="Act.Running Hour ID" ItemStyle-Width="30%" HeaderStyle-Width="13%"
								                DataNavigateUrlField="RunHourID"
								                DataNavigateUrlFormatString="GL_Trx_RunningHour_Details.aspx?id={0}"
								                DataTextField="RunHourID"
								                DataTextFormatString="{0:c}"
								                SortExpression="RH.RunHourID"/>
						
						                <asp:TemplateColumn HeaderText="Transaction Date" SortExpression="RH.TransactDate">
							                <ItemTemplate>
								                <%#objGlobal.GetLongDate(Container.DataItem("TransactDate"))%>
							                </ItemTemplate>
						                </asp:TemplateColumn>
						                <asp:TemplateColumn HeaderText="Last Update" SortExpression="RH.UpdateDate">
							                <ItemTemplate>
								                <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
							                </ItemTemplate>
						                </asp:TemplateColumn>
						                <asp:TemplateColumn HeaderText="Status" SortExpression="RH.Status">
							                <ItemTemplate>
								                <%# objGLtx.mtdGetVehicleUsageStatus(Container.DataItem("Status")) %>
							                </ItemTemplate>
						                </asp:TemplateColumn>
						                <asp:TemplateColumn HeaderText="Updated By" SortExpression="UserName">
							                <ItemTemplate>
								                <%# Container.DataItem("UserName") %>
								                <asp:label id="lblTxID" Text=<%# Container.DataItem("RunHourID") %> Visible="False" Runat="server"></asp:label>
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
					            <asp:ImageButton id=Usage OnClick=btnNewItm_Click imageurl="../../images/butt_new.gif" AlternateText="New Vehicle Usage" runat="server"/>
				        		<asp:ImageButton id=ibPrint AlternateText=Print imageurl="../../images/butt_print.gif" visible=false runat="server"/>
                                <a href="javascript:PopSetStationRH('frmMain', '', 'lstItem', 'True');"><asp:Image id="btnSelDate" runat="server" ImageUrl="~/en/images/butt_change.gif"/></a>
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
