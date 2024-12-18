<%@ Page Language="vb" src="../../../include/PR_trx_AttdList.aspx.vb" Inherits="PR_trx_AttdList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRTrx" src="../../menu/menu_prtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Attendance List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<%--<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	</head>
	<body>
		<form id=frmMain runat="server" class="main-modul-bg-app-list-pu">
    		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />


<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuPRTrx id=MenuPRTrx runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>ATTENDANCE LIST</strong><hr style="width :100%" />   
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
								<td width="15%" height="26">Gang Code :<BR><asp:DropDownList id=ddlGang width=100% maxlength="20" runat="server" /></td>
								<td width="15%" height="26">Employee Code :<BR><asp:TextBox id=txtEmpID width=100% maxlength="20" runat="server" /></td>
								<td width="20%">Name :<BR><asp:TextBox id=txtName width=100% maxlength="64" runat="server" /></td>
								<td width="25%"><asp:Label id="lblDepartment" runat="server" /> :<BR><asp:DropDownList id="ddlDept" width=100% runat=server/></td>
								<td width="12%">Month :<BR>
									<asp:DropDownList id="ddlMonth" width=100% runat=server>
										<asp:ListItem value="1">January</asp:ListItem>
										<asp:ListItem value="2">February</asp:ListItem>
										<asp:ListItem value="3">March</asp:ListItem>
										<asp:ListItem value="4">April</asp:ListItem>
										<asp:ListItem value="5">May</asp:ListItem>
										<asp:ListItem value="6">June</asp:ListItem>
										<asp:ListItem value="7">July</asp:ListItem>
										<asp:ListItem value="8">August</asp:ListItem>
										<asp:ListItem value="9">September</asp:ListItem>
										<asp:ListItem value="10">October</asp:ListItem>
										<asp:ListItem value="11">November</asp:ListItem>
										<asp:ListItem value="12">December</asp:ListItem>
									</asp:DropDownList>
								</td>
								<td width="10%">Year :<BR><asp:DropDownList id=ddlYear width=100% runat=server/></td>
								<td width="10%" valign=bottom align=right><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
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
							OnItemCommand=OnCommand_Redirect 
							OnPageIndexChanged=OnPageChanged 
							Pagerstyle-Visible=False
                                        class="font9Tahoma">
								
							            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>		
							<Columns>								
								<asp:TemplateColumn HeaderText="Gang Code" ItemStyle-Width="10%">
									<ItemTemplate>
										<asp:label id=lblGangCode visible="false" text=<%# Container.DataItem("GangCode")%> runat="server" />
										<%# Container.DataItem("GangCode") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn Visible=False HeaderText="Employee Code" DataField="EmpCode" />
								<asp:TemplateColumn HeaderText="Employee Code" ItemStyle-Width="10%">
									<ItemTemplate>
										<%# Container.DataItem("EmpCode") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Name" ItemStyle-Width="28%">
									<ItemTemplate>
										<%# Container.DataItem("EmpName") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="1" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_1 CommandArgument=_1 Text='<%# Container.DataItem("_1") %>' runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="2" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_2 CommandArgument=_2  Text='<%# Container.DataItem("_2") %>' runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="3" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_3 CommandArgument=_3  Text='<%# Container.DataItem("_3") %>' runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="4" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_4 CommandArgument=_4  Text='<%# Container.DataItem("_4") %>' runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="5" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_5 CommandArgument=_5  Text='<%# Container.DataItem("_5") %>' runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="6" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_6 CommandArgument=_6  Text='<%# Container.DataItem("_6") %>' runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="7" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_7 CommandArgument=_7  Text='<%# Container.DataItem("_7") %>' runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="8" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_8 CommandArgument=_8  Text='<%# Container.DataItem("_8") %>' runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="9" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_9 CommandArgument=_9  Text='<%# Container.DataItem("_9") %>' runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="10" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_10 CommandArgument=_10  Text='<%# Container.DataItem("_10") %>' runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="11" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_11 CommandArgument=_11  Text='<%# Container.DataItem("_11") %>' runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="12" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_12 CommandArgument=_12  Text='<%# Container.DataItem("_12") %>' runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="13" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_13 CommandArgument=_13  Text='<%# Container.DataItem("_13") %>' runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="14" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_14 CommandArgument=_14  Text='<%# Container.DataItem("_14") %>' runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="15" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_15 CommandArgument=_15  Text='<%# Container.DataItem("_15") %>' runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="16" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_16 CommandArgument=_16  Text='<%# Container.DataItem("_16") %>' runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="17" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_17 CommandArgument=_17  Text='<%# Container.DataItem("_17") %>' runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="18" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_18 CommandArgument=_18  Text='<%# Container.DataItem("_18") %>' runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="19" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_19 CommandArgument=_19  Text='<%# Container.DataItem("_19") %>' runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="20" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_20 CommandArgument=_20 Text='<%# Container.DataItem("_20") %>' runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="21" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_21 CommandArgument=_21  Text='<%# Container.DataItem("_21") %>' runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="22" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_22 CommandArgument=_22  Text='<%# Container.DataItem("_22") %>' runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="23" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_23 CommandArgument=_23  Text='<%# Container.DataItem("_23") %>' runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="24" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_24 CommandArgument=_24  Text='<%# Container.DataItem("_24") %>' runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="25" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_25 CommandArgument=_25  Text='<%# Container.DataItem("_25") %>' runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="26" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_26 CommandArgument=_26  Text='<%# Container.DataItem("_26") %>' runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="27" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_27 CommandArgument=_27  Text='<%# Container.DataItem("_27") %>' runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="28" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_28 CommandArgument=_28  Text='<%# Container.DataItem("_28") %>' runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="29" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_29 CommandArgument=_29  Text='<%# Container.DataItem("_29") %>' runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="30" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_30 CommandArgument=_30  Text='<%# Container.DataItem("_30") %>' runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="31" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_31 CommandArgument=_31  Text='<%# Container.DataItem("_31") %>' runat=server />
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
                                    <asp:Label id=lblCurrentIndex visible=false text=0 runat=server/>
			         	            <asp:Label id=lblPageCount visible=false text=1 runat=server/>	
			         	            <asp:label id=lblTotalDept visible=false text=0 runat=server />	
								</tr>
							</table>
							</td>
						</tr>
						<tr>
							<td>
					            <asp:ImageButton id=AttdBtn OnClick="AttdBtn_Click" imageurl="../../images/butt_daily_attendance.gif" AlternateText="Daily Attendance" runat="server"/>
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
