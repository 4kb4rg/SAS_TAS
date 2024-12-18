<%@ Page Language="vb" trace="false" codefile="../../../include/PM_trx_KernelQuality_List.aspx.vb" Inherits="PM_KernelQualityList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPDTrx" src="../../menu/menu_PDtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<html>
	<head>
		<title>KERNEL QUALITY TRANSACTION</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
<%--		<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	</head>
	<body>
	    <form id=frmMain runat=server class="main-modul-bg-app-list-pu">
			<asp:Label id=lblErrMessage visible=false text="Error while initiating component." runat=server />
			<asp:Label id=SortExpression visible=false runat=server />
			<asp:Label id=SortCol visible=false runat=server />


		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuPDTrx id=MenuPDTrx runat=server />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>KERNEL QUALITY LIST</strong><hr style="width :100%" />   
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
								<td width="25%"> Transaction Date : <BR>
                                    <asp:DropDownList ID="lstAccMonth" CssClass="fontObject" runat="server" Width="30%">
                                        <asp:ListItem Selected="True" Value="1">1</asp:ListItem>
                                        <asp:ListItem Value="2">2</asp:ListItem>
                                        <asp:ListItem Value="3">3</asp:ListItem>
                                        <asp:ListItem Value="4">4</asp:ListItem>
                                        <asp:ListItem Value="5">5</asp:ListItem>
                                        <asp:ListItem Value="6">6</asp:ListItem>
                                        <asp:ListItem Value="7">7</asp:ListItem>
                                        <asp:ListItem Value="8">8</asp:ListItem>
                                        <asp:ListItem Value="9">9</asp:ListItem>
                                        <asp:ListItem Value="10">10</asp:ListItem>
                                        <asp:ListItem Value="11">11</asp:ListItem>
                                        <asp:ListItem Value="12">12</asp:ListItem>
                                    </asp:DropDownList>		
                                    <asp:DropDownList ID="lstAccYear" CssClass="fontObject"  runat="server" Width="50%"></asp:DropDownList>													
									<asp:label id=lblDate Text ="<br>Date Entered should be in the format " forecolor=red Visible = false Runat="server"/> 
									<asp:label id=lblFmt  forecolor=red Visible = false Runat="server"/> 
									<asp:label id="lblDupMsg"  Text="This transaction already exist" Visible=false forecolor=red Runat="server"/>								
								</td>  		
								<td width="45%">&nbsp;</td>
								<!--<td width="15%">&nbsp;<BR><asp:DropDownList id="srchStatusList1" visible="false" width=100% runat=server /></td>-->
								<!--<td width="5%"><BR><asp:DropDownList id="srchStatusList" visible="false" width=100% runat=server /></td>-->
							 
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
						            <asp:DataGrid id=dgKernelQualityList
							            AutoGenerateColumns=false width=100% runat=server
							            GridLines=none 
							            Cellpadding=2 
							            AllowPaging=True 
							            Allowcustompaging=False 
							            Pagesize=15 
							            OnPageIndexChanged=OnPageChanged 
							            Pagerstyle-Visible=False 
							            OnDeleteCommand=DEDR_Delete
							            OnEditCommand=DEDR_Edit 							
							            OnSortCommand=Sort_Grid  
							            AllowSorting=True
                                                    class="font9Tahoma">
								
							                        <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                        <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                        <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
							
							            <Columns>
								            <asp:TemplateColumn HeaderText="Transaction Date"   >
									            <ItemTemplate>			
                                                     <%# objGlobal.GetLongDate(Container.DataItem("Indate")) %>						
										            <%--<asp:LinkButton id="Edit" CommandName="Edit" Text='<%# objGlobal.GetLongDate(Container.DataItem("Indate")) %>' runat="server"/>--%>
									            </ItemTemplate>
								            </asp:TemplateColumn>

								            <asp:TemplateColumn HeaderText="Bunker <br> Silo Code"  >
									            <ItemTemplate>
										            <%# Container.DataItem("DriyingSiloBulkCode")%>
									            </ItemTemplate>
								            </asp:TemplateColumn>

								            <asp:TemplateColumn HeaderText="Bunker <br> Silo Name"  >
									            <ItemTemplate>
										            <%# Container.DataItem("BunkerName")%>
									            </ItemTemplate>
								            </asp:TemplateColumn>
                                                                                         
			                                <asp:TemplateColumn HeaderText="Sounding (Cm)"  >
									            <ItemTemplate>
										            <%# FormatNumber(Container.DataItem("SoundingCM"),2)%>
									            </ItemTemplate>
								            </asp:TemplateColumn>

                                            
			                                <asp:TemplateColumn HeaderText="Isi Kerucut <br> (Kg)"  >
									            <ItemTemplate>
										            <%# FormatNumber(Container.DataItem("ChuteKg"), 2)%>
									            </ItemTemplate>
								            </asp:TemplateColumn>

			                                <asp:TemplateColumn HeaderText="Massa Jenis"  >
									            <ItemTemplate>
										            <%# Container.DataItem("BeratJenis")%>
									            </ItemTemplate>
								            </asp:TemplateColumn>

			                                <asp:TemplateColumn HeaderText="FFA (%)"  >
									            <ItemTemplate>
										            <%# FormatNumber(Container.DataItem("Pecahan"), 2)%>
									            </ItemTemplate>
								            </asp:TemplateColumn>

			                                    <asp:TemplateColumn HeaderText="Kadar Air (%)"  >
									            <ItemTemplate>
										            <%# FormatNumber(Container.DataItem("Moisture"),2) %>
									            </ItemTemplate>
								            </asp:TemplateColumn>

			                                    <asp:TemplateColumn HeaderText="Dirt (%)"  >
									            <ItemTemplate>
										            <%# FormatNumber(Container.DataItem("Dirt"), 2)%>
									            </ItemTemplate>
								            </asp:TemplateColumn>

                                                                                                                                                                                                                                                                        																																																			
								            <asp:TemplateColumn HeaderText="Last Update" SortExpression="UpdateDate">
									            <ItemTemplate>
										            <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								
								            <asp:TemplateColumn HeaderText="Status" SortExpression="Status">
									            <ItemTemplate>
										            <%# objPMTrx.mtdGetOilQualityStatus(Container.DataItem("Status"))%>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								
								            <asp:TemplateColumn HeaderText="Updated By" SortExpression="UpdateID">
									            <ItemTemplate>
										            <%# Container.DataItem("UpdateID") %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn ItemStyle-HorizontalAlign=Center>
									            <ItemTemplate>
										            <asp:Label id="lblLocCode" visible="false" text='<%# Container.DataItem("LocCode") %>' runat=server/>																		
										            <asp:Label id="lblTransDate" visible="false" text='<%# Container.DataItem("Indate") %>' runat=server/>										
										            <asp:LinkButton id="Delete" CommandName="Delete" Text="" runat="server"/>
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
					            <asp:ImageButton id=NewBtn onClick=NewBtn_Click imageurl="../../images/butt_new.gif" AlternateText="New" runat=server/>
				        		<asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print visible=false runat="server"/>
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
