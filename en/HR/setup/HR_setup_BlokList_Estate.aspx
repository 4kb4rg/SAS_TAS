<%@ Page Language="vb" src="../../../include/HR_setup_BlokList_Estate.aspx.vb" Inherits="HR_setup_BlokList_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHRSetup" src="../../menu/menu_hrsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Division List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<%--<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	    <style type="text/css">
            .style1
            {
                height: 22px;
            }
        </style>
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
							<td><strong><asp:label id="lblTitle" runat="server" >BLOCK CODE</asp:label> LIST</strong><hr style="width :100%" />   
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
								<td width="15%" height="26" valign=bottom><asp:label id="lblDepartment" runat="server" >Block</asp:label> Code :<BR><asp:TextBox id=txtBlokCode width=100% maxlength="8" runat="server" /></td>
								<td height="26" valign=bottom style="width: 31%">Description :<BR><asp:TextBox id=txtDescription width=100% maxlength="15" runat="server" /></td>
								<td height="26" valign=bottom style="width: 17%">Division&nbsp; :<BR><asp:TextBox id=txtDivID width=100% maxlength="15" runat="server" /></td>
								<td width="15%" height="26" valign=bottom>Status :<BR>
									<asp:DropDownList id="ddlStatus" width=100% runat=server>
										<asp:ListItem value="0">All</asp:ListItem>
										<asp:ListItem value="1" selected>Active</asp:ListItem>
										<asp:ListItem value="2">Deleted</asp:ListItem>
									</asp:DropDownList>
								</td>
								<td width="20%" height="26" valign=bottom>Last Updated By :<BR><asp:TextBox id=txtLastUpdate width=100% maxlength="128" runat="server"/></td>
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
							            OnItemDataBound=KeepRunningSum
							            AllowSorting=True
							            ShowFooter=True
                                                    class="font9Tahoma">
								
							                        <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                        <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                        <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
							            <Columns>
								            <asp:BoundColumn Visible=False HeaderText="Block Code" DataField="BlokCode" />								
								            <asp:HyperLinkColumn HeaderText="Block Code" SortExpression="BlokCode" 
									            DataNavigateUrlField="BlokCode" 
									            DataNavigateUrlFormatString="HR_setup_Blokdet_Estate.aspx?BlokCode={0}" 
									            DataTextField="BlokCode" />								
								            <asp:TemplateColumn HeaderText="Description" SortExpression="Description">
									            <ItemTemplate>
										            <%#Container.DataItem("Description")%>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								
								            <asp:TemplateColumn HeaderText="Divisi" SortExpression="Division">
									            <ItemTemplate>
										            <asp:Label ID = IDDiv Text='<%#Container.DataItem("Division")%>' Visible=true runat=server/>										
									            </ItemTemplate>
								            </asp:TemplateColumn>		
								
								            <asp:TemplateColumn HeaderText="Tahun Tanam" SortExpression="YearPlan">
									            <ItemTemplate>
										            <asp:Label ID = YearPlan Text='<%#Container.DataItem("YearPlan")%>' Visible=true runat=server/>										
									            </ItemTemplate>
								            </asp:TemplateColumn>	
								
								            <asp:TemplateColumn HeaderText="Luas" SortExpression="Luas">
									            <ItemTemplate>
										            <asp:Label ID = Luas Text='<%#Container.DataItem("Luas")%>' Visible=true runat=server/>										
									            </ItemTemplate>
									            <ItemStyle HorizontalAlign="Right" />
									            <HeaderStyle HorizontalAlign="Right" />
									            <FooterTemplate >
									                <asp:Label ID=lbLuas runat=server />
									             </FooterTemplate>
									             <FooterStyle BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />
								            </asp:TemplateColumn>	
								
								            <asp:TemplateColumn HeaderText="Total Pokok" SortExpression="TotPKK" >
									            <ItemTemplate >
										            <asp:Label ID = TotPKK Text='<%#Container.DataItem("TotPKK")%>' Visible=true runat=server/>										
									            </ItemTemplate>
									            <ItemStyle HorizontalAlign="Right" />
									            <HeaderStyle HorizontalAlign="Right" />
									            <FooterTemplate >
									                <asp:Label ID=lbtpkk runat=server />
									             </FooterTemplate>
									             <FooterStyle BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />
								            </asp:TemplateColumn>		
								
								            <asp:TemplateColumn HeaderText="BJR">
									            <ItemTemplate >
										            <asp:Label ID =Bjr Text='<%#Container.DataItem("BJR")%>' Visible=true runat=server/>										
									            </ItemTemplate>
									            <ItemStyle HorizontalAlign="Right" />
									            <HeaderStyle HorizontalAlign="Right" />
								            </asp:TemplateColumn>		
															
								            <asp:TemplateColumn HeaderText="Last Update" SortExpression="UpdateDate">
									            <ItemTemplate>
										            <%#objGlobal.GetLongDate(Container.DataItem("UpdateDate"))%>
									            </ItemTemplate>
									            <ItemStyle HorizontalAlign="Center" />
									            <HeaderStyle HorizontalAlign="Center" />
								            </asp:TemplateColumn>									
								            <asp:TemplateColumn HeaderText="Status" SortExpression="Status">
									            <ItemTemplate>
										            <asp:Label ID =Status Text='<%#objHRSetup.mtdGetDeptStatus(Container.DataItem("Status"))%>' Visible=true runat=server/>
									            </ItemTemplate>
									            <ItemStyle HorizontalAlign="Center" />
									            <HeaderStyle HorizontalAlign="Center" />
								            </asp:TemplateColumn>									
								            <asp:TemplateColumn HeaderText="Updated By" SortExpression="UserName">
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
							<td class="style1">
					            <asp:ImageButton id=NewDeptBtn onClick=NewDeptBtn_Click imageurl="../../images/butt_new.gif" AlternateText="New Block" runat="server"/>
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
