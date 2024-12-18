<%@ Page Language="vb" src="../../../include/HR_trx_ResignList_Estate.aspx.vb" Inherits="HR_trx_ResignList_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHRSetup" src="../../menu/menu_hrsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Resign List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<%--<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	</head>
	<body>
		<form id=frmLocList runat="server" class="main-modul-bg-app-list-pu">
    		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
    		<asp:label id="lblCode" visible="false" text=" Code" runat="server" />
			<asp:label id="SortExpression" Visible="False" Runat="server" />


		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuHRSetup id=MenuHRSetup runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>DAFTAR KARYAWAN BERHENTI</strong><hr style="width :100%" />   
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
								<td width="15%" height="26" valign=bottom>
                                    NIK :<BR><asp:TextBox id=txtEmpCode width=100% maxlength="30" runat="server" /></td>
								<td height="26" valign=bottom style="width: 17%">
								Nama :<BR><asp:TextBox id=txtEmpName width=100% maxlength="30" runat="server" /></td>
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
                                        OnItemDataBound="dgLine_BindGrid"
							            Pagerstyle-Visible=False 
							            OnDeleteCommand=DEDR_Delete 
							            OnSortCommand=Sort_Grid  
							            AllowSorting=True
                                                    class="font9Tahoma">
								
							                        <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                        <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                        <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
							            <Columns>
								            <asp:BoundColumn Visible=False HeaderText="Kode Berhenti" DataField="ResignCode" />								
								            <asp:HyperLinkColumn HeaderText="Kode Berhenti" SortExpression="ResignCode" 
									            DataNavigateUrlField="ResignCode" 
									            DataNavigateUrlFormatString="HR_trx_ResignDet_Estate.aspx?ResignCode={0}" 
									            DataTextField="ResignCode" />			
								
								            <asp:TemplateColumn HeaderText="NIK" SortExpression="EmpCode">
									            <ItemTemplate>
										            <asp:Label ID =lbecode Text='<%#Container.DataItem("EmpCode")%>' Visible=true runat=server/>										
									            </ItemTemplate>
								            </asp:TemplateColumn>	
								
								            <asp:TemplateColumn HeaderText="Nama" SortExpression="EmpName">
									            <ItemTemplate>
										            <asp:Label ID =lbename Text='<%#Container.DataItem("EmpName")%>' Visible=true runat=server/>										
									            </ItemTemplate>
								            </asp:TemplateColumn>	
								
								            <asp:TemplateColumn HeaderText="Tgl.Efektif" SortExpression="EfektifDate">
									            <ItemTemplate>
										            <asp:Label ID =lbedate Text='<%#Container.DataItem("EfektifDate")%>' Visible=true runat=server/>										
									            </ItemTemplate>
								            </asp:TemplateColumn>
								
								            <asp:TemplateColumn HeaderText="Kategori" SortExpression="ResignType">
									            <ItemTemplate>
										            <asp:Label ID =lbrtype Text='<%#Kategori(Container.DataItem("ResignType"))%>' Visible=true runat=server/>										
									            </ItemTemplate>
								            </asp:TemplateColumn>
								
																
								            <asp:TemplateColumn HeaderText="Status" SortExpression="Status">
									            <ItemTemplate>
										            <asp:Label ID=lstatus Text='<%#Active(Container.DataItem("Status"))%>' Visible=true runat=server/>
									            </ItemTemplate>
									            <ItemStyle HorizontalAlign="Center" />
									            <HeaderStyle HorizontalAlign="Center" />
								            </asp:TemplateColumn>	
													
								            <asp:TemplateColumn HeaderText="Tgl Update" SortExpression="UpdateDate">
									            <ItemTemplate>
										            <%#objGlobal.GetLongDate(Container.DataItem("UpdateDate"))%>
									            </ItemTemplate>
									            <ItemStyle HorizontalAlign="Center" />
									            <HeaderStyle HorizontalAlign="Center" />
								            </asp:TemplateColumn>		
															
											
								            <asp:TemplateColumn HeaderText="Diupdate" SortExpression="UName">
									            <ItemTemplate>
										            <%#Container.DataItem("UName")%>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								
								            <asp:TemplateColumn>
									            <ItemTemplate>
										            <asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server />
										            <asp:Label ID=lbStatus Text='<%#Container.DataItem("Status")%>' Visible=false runat=server/>
						
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
					            <asp:ImageButton id=NewBtn onClick=NewBtn_Click imageurl="../../images/butt_new.gif" AlternateText="New Block" runat="server"/>
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
