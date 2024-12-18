<%@ Page Language="vb" codefile="~/include/PR_trx_OutOfDutyList.vb" Inherits="PR_trx_OutOfDutyList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRSetup" src="../../menu/menu_prsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Premi Lain List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
<%--		<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	</head>
	<body style="margin: 0" onload="loadContent('Detail')">
		<form id=frmLocList runat="server" class="main-modul-bg-app-list-pu">

    		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
    		<asp:label id="lblCode" visible="false" text=" Code" runat="server" />
			<asp:label id="SortExpression" Visible="False" Runat="server" />

			<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuPRSetup id=MenuPRSetup runat="server" />
					</td>
				</tr>
				<tr>
					<td style="width: 100%; height: 800px" valign="top">
					<div class="kontenlist">

						<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
							<tr>
								<td><strong>PERMINTAAN TUNJANGAN OPERASIONAL LIST</strong><hr style="width :100%" />   
								</td>
								
							</tr>
							<tr>
								<td align="right"><asp:label id="lblTracker" runat="server" /></td> 
							</tr>
							<tr>
								<td colspan=6><hr style="width :100%" /></td>					       
							</tr>
							<tr>
								<td colspan=6 width=100% class="font9Tahoma">
                                    <table width="100%" cellspacing="0" cellpadding="3" border="0" align="center" class="font9Tahoma">
										<tr style="background-color:#FFCC00">
                              								
											<td width=10% valign=bottom>Period :<BR>
												<asp:DropDownList id="ddlMonth" width=100% runat=server>
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
											<td width=10% valign=bottom><BR>
												<asp:DropDownList id="ddlyear" width=100% runat=server>
												</asp:DropDownList>
											<td width="15%" height="26" valign=bottom>Status :<BR>
												<asp:DropDownList id="ddlStatus" width=100% runat=server>
													<asp:ListItem value="0">All</asp:ListItem>
													<asp:ListItem value="1" selected>Active</asp:ListItem>
													<asp:ListItem value="2">Delete</asp:ListItem>
													<asp:ListItem value="3">Confirmed</asp:ListItem>
													
												</asp:DropDownList>
											</td>
											<td width="15%"valign=bottom align=right><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click CssClass="button-small" runat="server"/></td>
										</tr>
									</table>                                                    
								</td>
							</tr>						 
							<tr>
									<td>
									<table cellpadding="4" cellspacing="1" style="width: 100%">
										<tr>
										    <td>
                                                <ul class="tab">
                                                  <li><table cellpadding="0" cellspacing="0" style="width: 100%; background-color: #EEEEEE"><tr><td><a href="#" class="tablinks" onclick="openContent(event, 'Detail')">Detail Permintaan Dana</a></td></tr></table></li>
                                                  <li><table cellpadding="0" cellspacing="0" style="width: 100%; background-color: #EEEEEE"><tr><td><a href="#" class="tablinks" onclick="openContent(event, 'Summary')">Summary</a></td></tr></table></li>
                                                  
                                                </ul>

                                                <div id="Detail" class="tabcontent">
                                                            <asp:DataGrid id=dgLine runat=server
												                    AutoGenerateColumns=False width=100% 
												                    GridLines=None 
												                    Cellpadding=4 
												                    AllowPaging=True 
												                    Pagesize=15 
												                    OnPageIndexChanged=OnPageChanged 
												                    Pagerstyle-Visible=False 
												                    OnSortCommand=Sort_Grid  
												                    AllowSorting=True
												                    class="font9Tahoma">								
												                    <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
												                    <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
												                    <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
												                    <Columns>
													                    <asp:BoundColumn Visible=False HeaderText="Document ID" DataField="DocID" />								
													                    <asp:HyperLinkColumn HeaderText="Document ID" 
														                    DataNavigateUrlField="DocID" 
														                    DataNavigateUrlFormatString="PR_Trx_OutOfDuty_EstateDet.aspx?DocID={0}" 
														                    DataTextField="DocID" />		
													                    <asp:TemplateColumn HeaderText="N.I.K">
															                    <ItemTemplate>
																                    <%#Container.DataItem("EmpCode")%>
															                    </ItemTemplate>
                                                                                <ItemStyle Width="8%" />
													                    </asp:TemplateColumn>	
													                    <asp:TemplateColumn HeaderText="Nama karyawan">
														                    <ItemTemplate>
															                    <%#Container.DataItem("EmpName")%>
														                    </ItemTemplate>
                                                                            <ItemStyle Width="25%" />
													                    </asp:TemplateColumn>	
													                    <asp:TemplateColumn HeaderText="Periode">
														                    <ItemTemplate>
															                    <%# objGlobal.GetLongDate(Container.DataItem("DateFr")) %>
														                    </ItemTemplate>
                                                                            <ItemStyle Width="8%" />
													                    </asp:TemplateColumn>			
													                    <asp:TemplateColumn HeaderText="Sampai">
														                    <ItemTemplate>
															                    <%# objGlobal.GetLongDate(Container.DataItem("DateTo")) %>
														                    </ItemTemplate>
                                                                            <ItemStyle Width="8%" />
													                    </asp:TemplateColumn>
													                    <asp:TemplateColumn HeaderText="Tujuan">
														                    <ItemTemplate>
															                    <%#Container.DataItem("DutyDesc")%>
														                    </ItemTemplate>
                                                                            <ItemStyle Width="10%" />
													                    </asp:TemplateColumn>		
													                    <asp:TemplateColumn HeaderText="Keperluan">
														                    <ItemTemplate>
															                    <%#Container.DataItem("Description")%>
														                    </ItemTemplate>
                                                                            <ItemStyle Width="23%" />
													                    </asp:TemplateColumn>																																																																											
													                    <asp:TemplateColumn HeaderText="Last Update" SortExpression="UpdateDate">
														                    <ItemTemplate>
															                    <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
														                    </ItemTemplate>
                                                                            <ItemStyle Width="8%" />
													                    </asp:TemplateColumn>	

													                    <asp:TemplateColumn HeaderText="Total Klaim (Rp)" HeaderStyle-HorizontalAlign=Right>
														                    <ItemStyle HorizontalAlign="Right" />		
														                    <ItemTemplate>
															                    <%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("ClaimAmmount"), 2), 2)%> 													
														                    </ItemTemplate>
                                                                            <ItemStyle Width="10%" />
													                    </asp:TemplateColumn>

													                    <asp:TemplateColumn HeaderText="Status" SortExpression="Status">
														                    <ItemTemplate>
															                    <%#Container.DataItem("StatusDesc")%>
														                    </ItemTemplate>
                                                                            <ItemStyle Width="10%" />
													                    </asp:TemplateColumn>									

													                    <asp:TemplateColumn>
														                    <ItemTemplate>															
															                    <asp:Label id=lblStatus Text='<%# Trim(Container.DataItem("Status")) %>' Visible=False runat=server />
														                    </ItemTemplate>
														                    <ItemStyle HorizontalAlign="Center" />
													                    </asp:TemplateColumn>	
												                    </Columns>
												                    <PagerStyle Visible="False" />
											                    </asp:DataGrid>                                                
                                                </div>

                                                <div id="Summary" class="tabcontent">
				                                    <asp:DataGrid id=dgSum runat=server
												    AutoGenerateColumns=False width=100% 
												    GridLines=None 
												    Cellpadding=2 
												    AllowPaging="False"
												    
												    Pagerstyle-Visible=False 
												    AllowSorting=True
												    class="font9Tahoma">								
												    <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
												    <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
												    <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
												    <Columns>													                           
													    <asp:TemplateColumn HeaderText="N.I.K">
															    <ItemTemplate>
																    <%#Container.DataItem("EmpCode")%>
															    </ItemTemplate>
													    </asp:TemplateColumn>	

													    <asp:TemplateColumn HeaderText="Nama karyawan">
														    <ItemTemplate>
															    <%#Container.DataItem("EmpName")%>
														    </ItemTemplate>
													    </asp:TemplateColumn>	
												
													    <asp:TemplateColumn HeaderText="Total Klaim (Rp)" HeaderStyle-HorizontalAlign=Right>
														    <ItemStyle HorizontalAlign="Right" />		
														    <ItemTemplate>
															    <%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("ClaimAmmount"), 2), 2)%> 													
														    </ItemTemplate>
													    </asp:TemplateColumn>

												    </Columns>
												    <PagerStyle Visible="False" />
											    </asp:DataGrid>

                                                </div>

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
									<asp:ImageButton id=NewSalBtn onClick=NewAstekBtn_Click imageurl="../../images/butt_new.gif" AlternateText="New" runat="server"/>
									<asp:ImageButton id=GenBtn onClick=GenBtn_Click imageurl="../../images/butt_generate.gif" AlternateText="Generate" runat="server"/>
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

        <script>
            function loadContent(contentName) {
                var i, tabcontent, tablinks;
                tabcontent = document.getElementsByClassName("tabcontent");
                tabcontent[0].style.display = "block";
                tablinks = document.getElementsByClassName("tablinks");
                tablinks[0].className = tablinks[0].className.replace(" active", "");
                document.getElementById(contentName).style.display = "block";
            }

            function openContent(evt, contentName) {
                var i, tabcontent, tablinks;
                tabcontent = document.getElementsByClassName("tabcontent");

                for (i = 0; i < tabcontent.length; i++) {
                    tabcontent[i].style.display = "none";
                }

                tablinks = document.getElementsByClassName("tablinks");

                for (i = 0; i < tablinks.length; i++) {
                    tablinks[i].className = tablinks[i].className.replace(" active", "");
                }

                document.getElementById(contentName).style.display = "block";
                evt.currentTarget.className += " active";
            }
        </script>

		</FORM>
	</body>
</html>
