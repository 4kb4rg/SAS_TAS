<%@ Page Language="vb" src="../../../include/IN_Trx_ItemToMachine_list.aspx.vb" Inherits="IN_ItemToMachineList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuINTrx" src="../../menu/menu_intrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Assign Item To Machine List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<%--<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	</head>
	<body>
		<form id=frmSubBlock runat="server" class="main-modul-bg-app-list-pu">
 
		    <table cellpadding="0" cellspacing="0" style="width: 100%">
				    <tr>
					    <td colspan="6">
						    <UserControl:MenuINTrx id=MenuINTrx runat="server" />
					    </td>
				    </tr>
			    <tr>
				    <td style="width: 100%; height: 800px" valign="top">
				    <div class="kontenlist">
					    <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						    <tr>
							    <td><strong>ASSIGN ITEM TO MACHINE LIST</strong><hr style="width :100%" />   
                                </td>
                            
						    </tr>
                            <tr>
                                <td align="right"><asp:label id="lblTracker" runat="server" /></td> 
                            </tr>
						    <tr>
							    <td style="background-color:#FFCC00">
							
							    <table cellpadding="4" cellspacing="0" style="width: 100%">
								    <tr class="font9Tahoma">
								    <td width="20%" height="26"><asp:label id="lblSubBlkCode" runat="server" /> :<BR>
									    <asp:TextBox id=srchSubBlockCode width=100% maxlength="8" runat="server" />
								    </td>
								    <td width="35%" height="26"><asp:label id="lblDescription" runat="server" /> :<BR>
									    <asp:TextBox id=srchDescription width=100% maxlength="128" runat="server" />
								    </td>
								    <td width="15%" height="26">Status :<BR>
									    <asp:DropDownList id="srchStatus" width=100% runat=server>
										    <asp:ListItem value="0">All</asp:ListItem>
										    <asp:ListItem value="1" selected>Active</asp:ListItem>
										    <asp:ListItem value="2">Deleted</asp:ListItem>
									    </asp:DropDownList>
								    </td>
								    <td width="20%" height="26">Last Updated By :<BR><asp:TextBox id=txtLastUpdate width=100% maxlength="128" runat="server"/></td>
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
						    <asp:DataGrid id=dgLine runat=server
							    AutoGenerateColumns=False width=100% 
							    GridLines=None 
							    Cellpadding=2 
							    AllowPaging=True  class="font9Tahoma"
							    Pagesize=15 
							    OnPageIndexChanged=OnPageChanged 
							    Pagerstyle-Visible=False 
							    OnDeleteCommand=DEDR_Delete 
							    OnSortCommand=Sort_Grid  
							    AllowSorting=True>
								
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
								    <asp:HyperLinkColumn
									    SortExpression="ITM.SubBlkCode" 
									    DataNavigateUrlField="SubBlkCode" 
									    DataNavigateUrlFormatString="IN_Trx_ItemToMachine_details.aspx?tbcode={0}" 
									    DataTextField="SubBlkCode" >
                                        <ItemStyle Width="15%" />
                                    </asp:HyperLinkColumn>
									
								    <asp:HyperLinkColumn 
									    SortExpression="SUB.Description" 
									    DataNavigateUrlField="SubBlkCode" 
									    DataNavigateUrlFormatString="IN_Trx_ItemToMachine_details.aspx?tbcode={0}" 
									    DataTextField="Description" NavigateUrl="IN_Trx_ItemToMachine_details.aspx?tbcode={0}" >
                                        <ItemStyle Width="35%" />
                                    </asp:HyperLinkColumn>
									
								    <asp:TemplateColumn HeaderText="Block" Visible=False>
									    <ItemTemplate>
										    <asp:Label Text=<%# Container.DataItem("BlkCode") %>  id="BlkCode" visible=false runat="server" />
									    </ItemTemplate>
                                        <ItemStyle Width="25%" />
								    </asp:TemplateColumn>
								    <asp:TemplateColumn HeaderText="Last Update" SortExpression="sub.UpdateDate">
									    <ItemTemplate>
										    <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									    </ItemTemplate>
                                        <ItemStyle Width="10%" />
								    </asp:TemplateColumn>
									
								    <asp:TemplateColumn HeaderText="Status" SortExpression="sub.Status">
									    <ItemTemplate>
										    <%# objINtrx.mtdGetItemToMachineStatus(Container.DataItem("Status")) %>
									    </ItemTemplate>
                                        <ItemStyle Width="10%" />
								    </asp:TemplateColumn>
									
								    <asp:TemplateColumn HeaderText="Updated By" SortExpression="usr.UserName">
									    <ItemTemplate>
										    <%# Container.DataItem("UserName") %>
									    </ItemTemplate>
                                        <ItemStyle Width="15%" />
								    </asp:TemplateColumn>
								
								    <asp:TemplateColumn>
									    <ItemTemplate>
										    <asp:Label id=lblSubBlkCode Visible=False text=<%# Container.DataItem("SubBlkCode")%> Runat="server" />
										    <asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server />
										    <asp:Label id=lblStatus Text='<%# Trim(Container.DataItem("Status")) %>' Visible=False runat=server />
									    </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="15%" />
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
						            <asp:ImageButton id=NewTBBtn onClick=NewTBBtn_Click imageurl="../../images/butt_new.gif" AlternateText="New Sub Block" runat=server/>
						            <asp:Label id=SortCol Visible=False Runat="server" />
						            <asp:Label id=lblErrLicSize visible=false forecolor=red text="<br>Access denied for hectarage license." runat=server/>
							    </td>
						    </tr>
                            <tr>
                                <td>
 					                <asp:Button ID="NewTBBtn2" class="button-small" runat="server" Text="New Sub Block"/>&nbsp;                     
                                </td>
                            </tr>
					    </table>
				    </div>
				    </td>
				    <td>
		    <table cellpadding="0" cellspacing="0" style="width: 20px">
			    <tr>
				    <td>&nbsp;</td>
			    </tr>
		    </table>
		</Form>
	</body>
</html>
