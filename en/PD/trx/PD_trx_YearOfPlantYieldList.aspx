<%@ Page Language="vb" src="../../../include/PD_trx_YearOfPlantYieldList.aspx.vb" Inherits="PD_trx_YearOfPlantYieldList" %>
<%@ Register TagPrefix="UserControl" Tagname="menu_PD_Trx" src="../../menu/menu_PDTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Year of Planting Yield List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
<%--		<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	</head>
	<body>
		<form id=frmMain runat="server" class="main-modul-bg-app-list-pu">
    		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="SortExpression" Visible="False" Runat="server" />


		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:menu_PD_Trx id=menu_PD_Trx runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>YEAR OF PLANTING YIELD LIST</strong><hr style="width :100%" />   
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
								<td width="25%" height="26">Year of Planting : <br><asp:textbox id=txtYearOfPlant width=100% runat="server" /></td>
								<td width="30%" height="26">Group Reference : <br><asp:textbox id=txtGroupRef width=100% runat="server" /></td>
								<td width="15%" height="26">Status :<br><asp:dropdownlist id="ddlStatus" width=100% runat=server/></td>
								<td width="20%" height="26">Last Updated By :<br><asp:textbox id=txtLastUpdate width=100% maxlength=128 runat="server"/></td>
								<td width="10%" height="26" valign=bottom align=right><asp:button id=SearchBtn Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
								</tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
						            <asp:DataGrid id=dgLine 
							            runat=server
							            AutoGenerateColumns=false 
							            width=100% 
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
								            <asp:HyperLinkColumn 
									            HeaderText="Year"
									            SortExpression="yld.Year" 
									            DataNavigateUrlField="CompositKey" 
									            DataNavigateUrlFormatString="PD_trx_YearOfPlantYieldDet.aspx?tbcode={0}" 
									            DataTextField="Year" />
									
								            <asp:HyperLinkColumn 
									            HeaderText="Group Reference"
									            SortExpression="yld.GroupRef" 
									            DataNavigateUrlField="CompositKey" 
									            DataNavigateUrlFormatString="PD_trx_YearOfPlantYieldDet.aspx?tbcode={0}" 
									            DataTextField="GroupRef" />
																	
								            <asp:TemplateColumn HeaderText="Rate/Weight" SortExpression="yld.Rate">
									            <ItemTemplate>
										            <%# Container.DataItem("Rate") %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								
								            <asp:TemplateColumn HeaderText="Total Weight(MT)" SortExpression="yld.TotalWeight">
									            <ItemTemplate>
										            <%# Container.DataItem("TotalWeight") %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								
								            <asp:TemplateColumn HeaderText="Last Update" SortExpression="yld.UpdateDate">
									            <ItemTemplate>
										            <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
									
								            <asp:TemplateColumn HeaderText="Status" SortExpression="yld.Status">
									            <ItemTemplate>
										            <%# objPDTrx.mtdGetYearOfPlantYieldStatus(Container.DataItem("Status")) %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
									
								            <asp:TemplateColumn HeaderText="Updated By" SortExpression="usr.UserName">
									            <ItemTemplate>
										            <%# Container.DataItem("UserName") %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								
								            <asp:TemplateColumn ItemStyle-Width="15%" ItemStyle-HorizontalAlign=center>
									            <ItemTemplate>
										            <asp:label id=lblCompositKey visible=false text=<%# Container.DataItem("CompositKey") %> runat=server />
										            <asp:label id=lblYearOfPlant visible=false text=<%# Container.DataItem("Year") %> runat=server />
										            <asp:label id=lblGroupRef visible=false text=<%# Container.DataItem("GroupRef") %> runat=server />
										            <asp:label id=lblRefDate visible=false text=<%# Container.DataItem("RefDate") %> runat=server />
										            <asp:label id="lblStatus" text='<%# Trim(Container.DataItem("Status")) %>' visible="False" runat=server />
										            <asp:linkbutton id=lbDelete CommandName=Delete Text=Delete runat=server />
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
					            <asp:ImageButton id=NewTBBtn OnClick="NewTBBtn_Click" imageurl="../../images/butt_new.gif" AlternateText="New Year of Planting Yield" runat="server"/>
						        <asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print visible="false" runat="server"/>
						        <asp:Label id=SortCol visible=False text=asc runat="server" />
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
