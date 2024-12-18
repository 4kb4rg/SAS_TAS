<%@ Page Language="vb" src="../../../include/PR_trx_RKHList_Estate.aspx.vb" Inherits="PR_RKHList"%> 
<%@ Register TagPrefix="UserControl" Tagname="MenuPRTrx" src="../../menu/menu_prtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<html>
	<head>
		<title>Buku Rencana kerja Harian List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<%--<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	</head>
	<body onload="javascript:document.frmMain.txtRKH.focus();">
	    <form id=frmMain runat=server class="main-modul-bg-app-list-pu">
			<asp:Label id=SortExpression visible=false runat=server />
			<asp:Label id=SortCol visible=false runat=server />
			<asp:Label id=lblErrMessage visible=false text="Error while initiating component." runat=server />


       
		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuPRTrx id=MenuPRTrx runat=server />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>DAFTAR RENCANA KERJA HARIAN</strong><hr style="width :100%" />   
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
								<td height="26" width="15%">
					     		No.RKH :<BR><asp:TextBox id=txtRKH width=100% maxlength="25" runat="server"/>
					     		</td>
								<td height="26" width="15%">
                                    Divisi :<BR><asp:DropDownList id="ddlEmpDiv" width=100% runat=server /></td>
                                <td height="26" style="width: 15%">
                                    Periode : <br />
                                    <asp:DropDownList ID="ddlEmpMonth" runat="server" Width="58%">
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
                                    </asp:DropDownList><asp:DropDownList id="ddlyear" width="40%" runat=server></asp:DropDownList></td>
								<td width="10%" height="26" valign=bottom>Status :<BR>
									<asp:DropDownList id="ddlStatus" width=100% runat=server>
										<asp:ListItem value="0">All</asp:ListItem>
										<asp:ListItem value="1" selected>Active</asp:ListItem>
										<asp:ListItem value="2">Deleted</asp:ListItem>								
									</asp:DropDownList>
								</td>
								<td height="26" width="10%"  valign=bottom align=right><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/>
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
						            <asp:DataGrid id=dgEmpList
							            AutoGenerateColumns=False width=100% runat=server
							            GridLines=None 
							            Cellpadding=2 
							            AllowPaging=True 
							            Pagesize=15 
							            OnPageIndexChanged=OnPageChanged 
							            Pagerstyle-Visible=False 
							            OnItemCommand=BKMLink_Click 
							            OnSortCommand=Sort_Grid  
							            OnDeleteCommand=DEDR_Delete 
						   	            AllowSorting=True
                                                    class="font9Tahoma">
								
							                        <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                        <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                        <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
							
							            <Columns>
							            <%--<asp:BoundColumn Visible=False HeaderText="No.BKM" DataField="BKMCode" />
								            <asp:HyperLinkColumn HeaderText="No.BKM" 
													             SortExpression="A.BKMCode" 
													             DataNavigateUrlField="BKMCode" 
													             DataNavigateUrlFormatString="PR_trx_BKMDet_New_Estate.aspx?BKMCode={0}" 
													             DataTextField="BKMCode" />--%>
													 
						                    <asp:TemplateColumn HeaderText="No.RKH" SortExpression="A.RKHCode">
									            <ItemTemplate>
						                          <asp:LinkButton id=lnkBKM  Text='<%# Container.DataItem("RKHCode") %>' runat=server /> 
						                          <asp:HiddenField Id=hidbkm Value='<%# Container.DataItem("RKHCode") %>' runat=server />
									            </ItemTemplate>
								            </asp:TemplateColumn>
						
						 
						                    <asp:TemplateColumn HeaderText="Tanggal" SortExpression="RKHDate">
									            <ItemTemplate>
										            <asp:Label id=lblDate text='<%# objGlobal.GetLongDate(Container.DataItem("RKHDate")) %>'  runat=server/>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								
								
						                    <asp:TemplateColumn HeaderText="Divisi" SortExpression="divisi">
                                	            <ItemTemplate>
										            <asp:Label id=lblEmpDiv text='<%# Container.DataItem("divisi") %>'  runat=server/>
									            </ItemTemplate>
								            </asp:TemplateColumn>
																													 
																
								            <asp:TemplateColumn HeaderText="Tgl Update" SortExpression="UpdateDate">
									            <ItemTemplate>
										            <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								
								            <asp:TemplateColumn HeaderText="Diupdate" SortExpression="UserName">
									            <ItemTemplate>
										            <%# Container.DataItem("UserName") %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								
								            <asp:TemplateColumn>
									            <ItemTemplate>
									                <asp:Label id=lblBKMid text='<%# Container.DataItem("RKHCode") %>'  Visible=false runat=server/>
										            <asp:Label id=lblstatus text='<%# Container.DataItem("Status") %>'  Visible=false runat=server/>
										            <asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server />
									            </ItemTemplate>
									            <ItemStyle HorizontalAlign="Center" />
							     
                                            </asp:TemplateColumn>											
							            </Columns>
							             <PagerStyle Visible="False" />
          				            </asp:DataGrid></td>
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
								</tr>
							</table>
							</td>
						</tr>
						<tr>
							<td>
					            <asp:ImageButton id=NewEmpBtn OnClick="NewEmpBtn_Click" imageurl="../../images/butt_new.gif" AlternateText="Rencana Kerja Harian" runat="server"/>
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



		</form>
	</body>
</html>
