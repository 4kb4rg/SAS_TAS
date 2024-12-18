<%@ Page Language="vb" src="../../../include/PR_setup_PrmiGolList_Estate.aspx.vb" Inherits="PR_setup_PrmiGolList_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRSetup" src="../../menu/menu_prsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Premi Golongan List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
<%--		<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	</head>
	<body>
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
							<td><strong>DAFTAR PREMI BASIS PANEN</strong><hr style="width :100%" />   
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
								<td height="26" valign=bottom style="width: 21%">Golongan :<BR><asp:TextBox id=txtGolID width=100% maxlength="8" runat="server" /></td>								
								<td height="26" valign=bottom style="width: 20%">
                                    Periode :<BR>
                                    <asp:DropDownList ID="srcpmonth" runat="server" Width="50%">
                                        <asp:ListItem Selected="" Value="">All</asp:ListItem>
                                        <asp:ListItem Value="01">January</asp:ListItem>
                                        <asp:ListItem Value="02">February</asp:ListItem>
                                        <asp:ListItem Value="03">March</asp:ListItem>
                                        <asp:ListItem Value="04">April</asp:ListItem>
                                        <asp:ListItem Value="05">May</asp:ListItem>
                                        <asp:ListItem Value="06">June</asp:ListItem>
                                        <asp:ListItem Value="07">July</asp:ListItem>
                                        <asp:ListItem Value="08">August</asp:ListItem>
                                        <asp:ListItem Value="09">September</asp:ListItem>
                                        <asp:ListItem Value="10">October</asp:ListItem>
                                        <asp:ListItem Value="11">November</asp:ListItem>
                                        <asp:ListItem Value="12">December</asp:ListItem>
                                    </asp:DropDownList><asp:DropDownList ID="srcpyear" runat="server" Width="40%">
                                    </asp:DropDownList></td>
								<td width="15%" height="26" valign=bottom>Status :<BR>
									<asp:DropDownList id="ddlStatus" width=100% runat=server>
										<asp:ListItem value="0">All</asp:ListItem>
										<asp:ListItem value="1" selected>Active</asp:ListItem>
										<asp:ListItem value="2">Deleted</asp:ListItem>
									</asp:DropDownList>
								</td>
								<td width="20%" height="26" valign=bottom>Diupdate :<BR><asp:TextBox id=txtLastUpdate width=100% maxlength="128" runat="server"/></td>
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
							                AllowPaging=True 
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
								                <asp:BoundColumn Visible=False HeaderText="ID" DataField="GolId" />								
								                <asp:HyperLinkColumn HeaderText="ID" SortExpression="GolId" 
									                DataNavigateUrlField="GolId" 
									                DataNavigateUrlFormatString="PR_setup_PrmiGolDet_Estate.aspx?GolId={0}" 
									                DataTextField="GolId" ItemStyle-HorizontalAlign="Left"/>	
										
							                    <asp:TemplateColumn HeaderText="Periode Start">
									                <ItemTemplate>
										                <%#Container.DataItem("periodestart")%>
									                </ItemTemplate>
								                </asp:TemplateColumn>	
								
								                <asp:TemplateColumn HeaderText="Periode End">
									                <ItemTemplate>
										                <%#Container.DataItem("periodeend")%>
									                </ItemTemplate>
								                </asp:TemplateColumn>	
								
								                <asp:TemplateColumn HeaderText="Umur (>=) " >
									                <ItemTemplate>
										                <%#Container.DataItem("UmrTnmMin")%>
									                </ItemTemplate>
									                <HeaderStyle HorizontalAlign=Center />
									                <ItemStyle HorizontalAlign="Center" />
								                </asp:TemplateColumn>	
								
								                <asp:TemplateColumn HeaderText="Umur (<) " >
									                <ItemTemplate>
										                <%#Container.DataItem("UmrTnmMax")%>
									                </ItemTemplate>
									                <HeaderStyle HorizontalAlign=Center />
									                <ItemStyle HorizontalAlign="Center" />
								                </asp:TemplateColumn>	
								
								                <asp:TemplateColumn HeaderText="Gol" >
									                <ItemTemplate>
										                <%#Container.DataItem("Gol")%>
									                </ItemTemplate>
									                <HeaderStyle HorizontalAlign=Center />
									                <ItemStyle HorizontalAlign="Center" />
								                </asp:TemplateColumn>	
								
															
								                <asp:TemplateColumn HeaderText="Basis Tugas(Kg)" >
									                <ItemTemplate>
										                <%#Container.DataItem("BTgsKg")%>
									                </ItemTemplate>
									                <HeaderStyle HorizontalAlign=Right />
									                <ItemStyle HorizontalAlign="Right" />
								                </asp:TemplateColumn>	
								
								                <asp:TemplateColumn HeaderText="Basis Tugas(Bjr)" >
									                <ItemTemplate>
										                <%#Container.DataItem("BTgsBjr")%>
									                </ItemTemplate>
									                <HeaderStyle HorizontalAlign=Right />
									                <ItemStyle HorizontalAlign="Right" />
								                </asp:TemplateColumn>	
								
								                <asp:TemplateColumn HeaderText="Basis Tugas(Jjg)" >
									                <ItemTemplate>
										                <%#Container.DataItem("BTgsJjg")%>
									                </ItemTemplate>
									                <HeaderStyle HorizontalAlign=Right />
									                <ItemStyle HorizontalAlign="Right" />
								                </asp:TemplateColumn>	
													
								                <asp:TemplateColumn HeaderText="Basis Premi(Jjg)" >
									                <ItemTemplate>
										                <%#Container.DataItem("BPrmJJG")%>
									                </ItemTemplate>
									                <HeaderStyle HorizontalAlign=Right />
									                <ItemStyle HorizontalAlign="Right" />
								                </asp:TemplateColumn>	
								
														
								                <asp:TemplateColumn HeaderText="Basis Tugas(Kg)" >
									                <ItemTemplate>
										                <%#Container.DataItem("BPrmKg")%>
									                </ItemTemplate>		
									                <HeaderStyle HorizontalAlign=Right />
									                <ItemStyle HorizontalAlign="Right" />							
								                </asp:TemplateColumn>		
								
								                <asp:TemplateColumn HeaderText="Over Basis 1(Rp)" >
									                <ItemTemplate>
										                <%#Container.DataItem("PrmOvr1")%>
									                </ItemTemplate>
									                <HeaderStyle HorizontalAlign=Right />
									                <ItemStyle HorizontalAlign="Right" />
								                </asp:TemplateColumn>	
								
														
								                <asp:TemplateColumn HeaderText="Over Basis 2(Rp)" >
									                <ItemTemplate>
										                <%#Container.DataItem("PrmOvr2")%>
									                </ItemTemplate>		
									                <HeaderStyle HorizontalAlign=Right />
									                <ItemStyle HorizontalAlign="Right" />							
								                </asp:TemplateColumn>		
								
								                <asp:TemplateColumn HeaderText="Over Basis 3(Rp)" >
									                <ItemTemplate>
										                <%#Container.DataItem("PrmOvr3")%>
									                </ItemTemplate>		
									                <HeaderStyle HorizontalAlign=Right />
									                 <ItemStyle HorizontalAlign="Right" />							
								                </asp:TemplateColumn>		
														
												
								
								                <asp:TemplateColumn>
									                <ItemTemplate>
										                <asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server />
										                <asp:Label id=lblStatus Text='<%# Trim(Container.DataItem("Status")) %>' Visible=False runat=server />
									                </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
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
					            <asp:ImageButton id=NewSalBtn onClick=NewAstekBtn_Click imageurl="../../images/butt_new.gif" AlternateText="New Overtime Code" runat="server"/>
						        <asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print visible=false runat="server"/>
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
