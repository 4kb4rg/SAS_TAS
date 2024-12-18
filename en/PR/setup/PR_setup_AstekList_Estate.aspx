<%@ Page Language="vb" src="../../../include/PR_setup_AstekList_Estate.aspx.vb" Inherits="PR_setup_AstekList_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRSetup" src="../../menu/menu_prsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>ASTEK List</title>
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
						<UserControl:MenuPRSetup id=MenuPRSetup runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>DAFTAR JAMSOSTEK</strong><hr style="width :100%" />   
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
								<td height="26" valign=bottom style="width: 21%">Kode Astek :<BR><asp:TextBox id=txtAstekCode width=100% maxlength="8" runat="server" /></td>								
								<td height="26" valign=bottom style="width: 20%">Deskripsi :<BR><asp:TextBox id=txtDesc width=100% maxlength="15" runat="server" /></td>
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
								                <asp:BoundColumn Visible=False HeaderText="Kode Astek" DataField="AstekCode" />								
								                <asp:HyperLinkColumn HeaderText="Kode Astek" SortExpression="AstekCode" 
									                DataNavigateUrlField="AstekCode" 
									                DataNavigateUrlFormatString="PR_setup_ASTEKDet_Estate.aspx?AstekCode={0}" 
									                DataTextField="AstekCode" />								
								                <asp:TemplateColumn HeaderText="Deskripsi" SortExpression="Description">
									                <ItemTemplate>
										                <%#Container.DataItem("Description")%>
									                </ItemTemplate>
								                </asp:TemplateColumn>
								                <asp:TemplateColumn HeaderText="Periode Start" SortExpression="PeriodeStart">
									                <ItemTemplate>
										                <%#Container.DataItem("PeriodeStart")%>
									                </ItemTemplate>
								                </asp:TemplateColumn>		
								
								                <asp:TemplateColumn HeaderText="Periode End" SortExpression="PeriodeEnd">
									                <ItemTemplate>
										                <%#Container.DataItem("PeriodeEnd")%>
									                </ItemTemplate>
								                </asp:TemplateColumn>		
																					
								                <asp:TemplateColumn HeaderText="Tgl Update" SortExpression="A.UpdateDate">
									                <ItemTemplate>
										                <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									                </ItemTemplate>
								                </asp:TemplateColumn>									
								                <asp:TemplateColumn HeaderText="Status" SortExpression="A.Status">
									                <ItemTemplate>
										                <asp:Label ID =Status Text='<%#objHRSetup.mtdGetDeptStatus(Container.DataItem("Status"))%>' Visible=true runat=server/>
									                </ItemTemplate>
								                </asp:TemplateColumn>									
								                <asp:TemplateColumn HeaderText="Diupdate" SortExpression="UserName">
									                <ItemTemplate>
										                <%# Container.DataItem("UserName") %>
									                </ItemTemplate>
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
					            <asp:ImageButton id=NewSalBtn onClick=NewAstekBtn_Click imageurl="../../images/butt_new.gif" AlternateText="New ASTEK Code" runat="server"/>
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
