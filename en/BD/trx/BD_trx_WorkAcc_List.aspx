<%@ Page Language="vb" trace="False" src="../../../include/BD_trx_WorkAcc_List.aspx.vb" Inherits="BD_WorkAcc_List" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuBDtrx" src="../../menu/menu_BDTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Working Accounts </title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<%--<Preference:PrefHdl id=PrefHdl runat="server" />--%>
		<body>
		    <form runat="server" ID="Form1" class="main-modul-bg-app-list-pu">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:label id="sortcol" Visible="False" Runat="server" />
			<asp:label id="lblOper" Visible="False" Runat="server" />
			<asp:Label id=lblBgtStatus Visible="False"  runat="server" />
			<asp:Label id=lblCode text=" Code" Visible="False" runat="server" />


		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuBDtrx id=menuBD runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
                            <td class="font9Tahoma"><table cellpadding="4" cellspacing="0" style="width: 100%">
					        <td class="mt-h" colspan="4" width=60%>WORKING ACCOUNTS</td>
					        <td align="right" colspan="2" width=40%>&nbsp;</td>
                            </table></td>
				        </tr>
				        <tr>
                            <td class="font9Tahoma"><table cellpadding="4" cellspacing="0" style="width: 100%">
					        <td colspan="4" width=60%><asp:label id="lblLocTag" text="Location " runat="server"/> : <asp:label id="lblLocCode" runat="server"/></td>
					        <td align="right" colspan="2" width=40%>&nbsp;</td>
                            </table></td>
				        </tr>
				        <tr>
                            <td class="font9Tahoma"><table cellpadding="4" cellspacing="0" style="width: 100%">
					        <td colspan="4" width=60%><asp:label id="lblBgtTag" text="Budgeting Period " runat="server"/> : <asp:label id="lblBgtPeriod" runat="server"/></td>
					        <td align="right" colspan="2" width=40%>&nbsp;</td>
                            </table></td>
				        </tr>
				        <tr>
                            <td class="font9Tahoma"><table cellpadding="4" cellspacing="0" style="width: 100%">
					        <td colspan=6><hr size="1" noshade></td>
                            </table></td>
				        </tr>
						<tr>
							<td style="background-color:#FFCC00" >
							<table cellpadding="4" cellspacing="0" style="width: 100%">
								<tr class="font9Tahoma">
								<td width="40%" height="26" valign=bottom><asp:label id="lblWorkDesc" Text="Working Account Name" runat="server" /> :<BR>
									<asp:TextBox id=srchDesc width=100% maxlength="64" runat="server"/>
								</td>
								<td width="15%" height="26" valign=bottom>Status :<BR>
									<asp:DropDownList id="srchStatusList" width=100% runat=server />
								</td>
								<td width="17%" height=26 valign=bottom>&nbsp;</td>
								<td width="20%" height="26" valign=bottom>Last Updated By :<BR><asp:TextBox id=srchUpdateBy width=100% maxlength="128" runat="server"/></td>
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
						            <asp:DataGrid id="dgWorkAccList"
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
							                HeaderText="Working Account Name" 
							                DataNavigateUrlField="WorkAccID"
							                DataNavigateUrlFormatString="BD_Trx_WorkAcc_Details.aspx?id={0}"
							                DataTextField="WorkAccName"
							                DataTextFormatString="{0:c}"
							                SortExpression="WorkAccName" />						
						
					                <asp:TemplateColumn HeaderText="Last Update" SortExpression="WA.UpdateDate">
						                <ItemTemplate>
							                <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
						                </ItemTemplate>
					                </asp:TemplateColumn>					
					                <asp:TemplateColumn HeaderText="Status" SortExpression="WA.Status">
						                <ItemTemplate>
							                <%# objBD.mtdGetWorkAccStatus(Container.DataItem("Status")) %>
						                </ItemTemplate>
					                </asp:TemplateColumn>					
					                <asp:TemplateColumn HeaderText="Updated By" SortExpression="UserName">
						                <ItemTemplate>
							                <%# Container.DataItem("UserName") %>
						                </ItemTemplate>
					                </asp:TemplateColumn>
					                <asp:TemplateColumn >
						                <ItemTemplate>
							                <asp:label id="lblWorkAccID" Text=<%# Container.DataItem("WorkAccID") %> Visible="False" Runat="server"></asp:label>
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
                            <td class="font9Tahoma"><table cellpadding="4" cellspacing="0" style="width: 100%">
					        <td class="mt-h" colspan="4" width=60%><asp:label id="lblTitle" runat="server" /></td>
					        <td align="right" colspan="2" width=40%><asp:label id="lblTracker" runat="server"/></td>
                            </table></td>
				        </tr>
				        <tr>
					        <TD colspan=6>					
					
					        </td>
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
					        <td align="left" ColSpan=6>
						        <asp:ImageButton id=ibNew OnClick="DEDR_Add" imageurl="../../images/butt_new.gif" AlternateText="New Working Account" runat="server"/>
						        <!--<asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print runat="server"/>-->
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
