<%@ Page Language="vb" trace="False" src="../../../include/BD_trx_MatureCropDist_YearList.aspx.vb" Inherits="BD_MatureCropDist_Year" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuBDtrx" src="../../menu/menu_BDTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Mature Crop Calenderisation</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
<%--	<Preference:PrefHdl id=PrefHdl runat="server" />--%>
		<body>
		    <form runat="server" ID="Form1" class="main-modul-bg-app-list-pu">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:label id="sortcol" Visible="False" Runat="server" />
			<asp:label id=BlockTag visible=false runat=server />
			<asp:label id=lblNoOf visible=false text="No. of " runat=server/>
			<asp:Label id=lblBgtStatus Visible="False"  runat="server" />
			<asp:label id=lblCode visible=false text=" Code" runat=server/>

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
					        <td class="mt-h" colspan="4" width=60%><asp:Label id="lblTitle" runat="server" /></td>
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
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr align="center">
                                    <td>
						            <asp:DataGrid id="BlockList"
						                AutoGenerateColumns="false" width="60%" runat="server"
						                GridLines = none
						                Cellpadding = "2"
						                Pagerstyle-Visible="False"
                                                        class="font9Tahoma">
								
							                            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>					
						                <Columns>	
							                <asp:TemplateColumn>
								                <ItemTemplate>
									                <asp:LinkButton id="lbblkcode" Text=<%# Container.DataItem("OriBlkCode") %> CommandArgument=<%# Container.DataItem("YearPlanted") %> CommandName=<%# Container.DataItem("BlkCode") %> onClick="btnBlockYear_Click" runat="server" />
								                </ItemTemplate>
							                </asp:TemplateColumn>
						
							                <asp:TemplateColumn>
								                <ItemTemplate>
									                <asp:LinkButton id="lbsubblkcode" Text=<%# Container.DataItem("BlkCode") %> CommandArgument=<%# Container.DataItem("YearPlanted") %> CommandName=<%# Container.DataItem("OriBlkCode") %> onClick="btnSubBlockYear_Click" runat="server" />
								                </ItemTemplate>
							                </asp:TemplateColumn>
											
							                <asp:TemplateColumn HeaderText="Planting Year" >
								                <ItemTemplate>
									                <%# Container.DataItem("YearPlanted") %>
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
					        <TD colspan = 6 align=center>					
							        <asp:Label id=lblPeriodErr Text="No active Budgeting Period" Forecolor=Red Visible=False runat=server />
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
