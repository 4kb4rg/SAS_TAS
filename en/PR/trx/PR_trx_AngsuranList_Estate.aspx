<%@ Page Language="vb" src="../../../include/PR_trx_AngsuranList_Estate.aspx.vb" Inherits="PR_trx_AngsuranList_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHRSetup" src="../../menu/menu_hrsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Angsuran List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<%--<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	</head>
	<body>
		<form id=frmLocList runat="server" class="main-modul-bg-app-list-pu">
    		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
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
							<td><strong>DAFTAR ANGSURAN KARYAWAN</strong><hr style="width :100%" />   
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
								NIK :<BR><asp:TextBox id=txtEmpCode width=100% maxlength="20" runat="server" /></td>
								<td height="26" valign=bottom style="width: 17%">
								Nama :<BR><asp:TextBox id=txtEmpName width=100% maxlength="20" runat="server" /></td>
								<td height="26" width="15%">Divisi :<BR><asp:DropDownList id="ddlEmpDiv" width=100% runat=server /></td>
								<td width="15%" height="26" valign=bottom>Status :<BR>
									<asp:DropDownList id="ddlStatus" width=100% runat=server>
										<asp:ListItem value="0">All</asp:ListItem>
										<asp:ListItem value="1" selected>Active</asp:ListItem>
										<asp:ListItem value="2">Deleted</asp:ListItem>
									</asp:DropDownList>
								</td>
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
							            OnSortCommand=Sort_Grid  
							            AllowSorting=True
                                                    class="font9Tahoma">
								
							                        <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                        <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                        <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
							            <Columns>
								            <asp:BoundColumn Visible=False HeaderText="NIK" DataField="CodeEmp" />								
								            <asp:HyperLinkColumn HeaderText="NIK" SortExpression="X.CodeEmp" 
									            DataNavigateUrlField="CodeEmp" 
									            DataNavigateUrlFormatString="PR_trx_AngsuranDet_Estate.aspx?CodeEmp={0}" 
									            DataTextField="CodeEmp" />	
							
								            <asp:TemplateColumn HeaderText="Nama" SortExpression="EmpName">
									            <ItemTemplate>
										            <asp:Label ID =lbename Text='<%#Container.DataItem("EmpName")%>' runat=server/>										
									            </ItemTemplate>
								            </asp:TemplateColumn>	
								
								            <asp:TemplateColumn HeaderText="Divisi">
									            <ItemTemplate>
										            <asp:Label ID =lbedate Text='<%#Container.DataItem("Divisi")%>' runat=server/>										
									            </ItemTemplate>
								            </asp:TemplateColumn>
								
												
								            <asp:TemplateColumn HeaderText="Jabatan" >
									            <ItemTemplate>
										            <asp:Label ID =lbket Text='<%#Container.DataItem("jabatan")%>' runat=server/>										
									            </ItemTemplate>
								            </asp:TemplateColumn>
								
								            <asp:TemplateColumn HeaderText="Tot.Angsuran" >
									            <ItemTemplate>
										            <asp:Label ID =lbtotagsr Text='<%#Container.DataItem("Totalpinjaman")%>' runat=server/>										
									            </ItemTemplate>
								            </asp:TemplateColumn>
								
								            <asp:TemplateColumn HeaderText="Tot.Bayar" >
									            <ItemTemplate>
										            <asp:Label ID =lbtempo Text='<%#Container.DataItem("Totalbayar")%>' runat=server/>										
									            </ItemTemplate>
								            </asp:TemplateColumn>
								
								            <asp:TemplateColumn HeaderText="Tot.Sisa" >
									            <ItemTemplate>
										            <asp:Label ID =lbrcicilan Text='<%#Container.DataItem("TotalSisa")%>' runat=server/>										
									            </ItemTemplate>
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
