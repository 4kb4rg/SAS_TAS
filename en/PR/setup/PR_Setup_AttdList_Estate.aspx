<%@ Page Language="vb" src="../../../include/PR_Setup_AttdList_Estate.aspx.vb" Inherits="PR_Setup_AttdList_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRSetup" src="../../menu/menu_prsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Attendance List</title>
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
						<UserControl:MenuPRSetup id=MenuPRSetup runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>DAFTAR SETTING KODE ABSENSI</strong><hr style="width :100%" />   
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
								<td width="20%" height="26">ID :<BR><asp:TextBox id=txtAttdId width=100% maxlength="8" runat="server" /></td>
								<td height="26" style="width: 25%">Tipe Karyawan :<BR><asp:TextBox id=txtEmpTy width=100% maxlength="128" runat="server" /></td>
								<td height="26" style="width: 25%">Kode Absen :<BR><asp:TextBox id=txtAttCd width=100% maxlength="128" runat="server" /></td>
								<td height="26" style="width: 13%">Status :<BR>
									<asp:DropDownList id="ddlStatus" width=100% runat=server>
										<asp:ListItem value="0">All</asp:ListItem>
										<asp:ListItem value="1" selected=True>Active</asp:ListItem>
										<asp:ListItem value="2">Deleted</asp:ListItem>
									</asp:DropDownList>
								</td>
								<td width="20%" height="26">Diupdate :<BR><asp:TextBox id=txtLastUpdate width=100% maxlength="128" runat="server"/></td>
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
							            AutoGenerateColumns=false width=100% 
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
								            <asp:BoundColumn Visible=False HeaderText="ID" DataField="AttID" />
								            <asp:HyperLinkColumn HeaderText="ID" ItemStyle-Width="7%"
									            SortExpression="A.AttID" 
									            DataNavigateUrlField="AttID" 
									            DataNavigateUrlFormatString="PR_setup_AttdDet_Estate.aspx?AttID={0}" 
									            DataTextField="AttID" />
									
								            <asp:HyperLinkColumn HeaderText="Tipe Karyawan" ItemStyle-Width="10%" 
									            SortExpression="A.CodeEmpTy" 
									            DataNavigateUrlField="AttID" 
									            DataNavigateUrlFormatString="PR_setup_AttdDet_Estate.aspx?AttID={0}" 
									            DataTextField="DescEmp" />
								
								            <asp:TemplateColumn HeaderText="Kode Absen">
									            <ItemTemplate>
										            <%#Container.DataItem("CodeAtt")%>
									            </ItemTemplate>
									            <ItemStyle Width="7%" />
								            </asp:TemplateColumn>
								
								            <asp:TemplateColumn HeaderText="Pot.Upah">
									            <ItemTemplate>
										            <%#Container.DataItem("PotUpah")%>
									            </ItemTemplate>
									            <ItemStyle Width="7%" />
								            </asp:TemplateColumn>
								
								            <asp:TemplateColumn HeaderText="Pot.Beras" >
									            <ItemTemplate>
										            <%#Container.DataItem("PotBeras")%>
									            </ItemTemplate>
									            <ItemStyle Width="7%" />
								            </asp:TemplateColumn>
								
								            <asp:TemplateColumn HeaderText="Pot.Cuti">
									            <ItemTemplate>
										            <%#Container.DataItem("PotCuti")%>
									            </ItemTemplate>
									            <ItemStyle Width="7%" />
								            </asp:TemplateColumn>
								
								            <asp:TemplateColumn HeaderText="Pot.Minggu">
									            <ItemTemplate>
										            <%#Container.DataItem("PotWeekend")%>
									            </ItemTemplate>
									            <ItemStyle Width="7%" />
								            </asp:TemplateColumn>
								
								            <asp:TemplateColumn HeaderText="Tgl update" SortExpression="A.UpdateDate">
									            <ItemTemplate>
										            <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									            </ItemTemplate>
									            <ItemStyle Width="7%" />
								            </asp:TemplateColumn>
									
								            <asp:TemplateColumn HeaderText="Status" SortExpression="A.Status">
									            <ItemTemplate>
										            <asp:Label ID =Status Text='<%# objPRSetup.mtdGetAttListStatus(Container.DataItem("Status")) %>' Visible=true runat=server/>
									            </ItemTemplate>
									            <ItemStyle Width="7%" />
								            </asp:TemplateColumn>
									
								            <asp:TemplateColumn HeaderText="Diupdate" SortExpression="UserName">
									            <ItemTemplate>
										            <%# Container.DataItem("UserName") %>
									            </ItemTemplate>
									            <ItemStyle Width="7%" />
								            </asp:TemplateColumn>
								
								            <asp:TemplateColumn ItemStyle-Width="10%" ItemStyle-HorizontalAlign=center>
									            <ItemTemplate>
										            <asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server />
										            <asp:Label id=lblStatus Text='<%# Trim(Container.DataItem("Status")) %>' Visible=False runat=server />
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
					            <asp:ImageButton id=NewTBBtn OnClick="NewTBBtn_Click" imageurl="../../images/butt_new.gif" AlternateText="New Attendance Code" runat="server"/>
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
