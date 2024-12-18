<%@ Page Language="vb" src="../../../include/PR_trx_RKBList.aspx.vb" Inherits="PR_RKBList"%> 
<%@ Register TagPrefix="UserControl" Tagname="MenuPRTrx" src="../../menu/menu_prtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<html>
	<head>
		<title>Rencana kerja Bulanan List</title>
		<link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<%--<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	</head>
	<body onload="javascript:document.frmMain.txtRKH.focus();">
	    <form id=frmMain runat=server  class="main-modul-bg-app-list-pu">
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
					<td><strong>Rencana Kerja Bulanan</strong><hr style="width :100%" /></td>  
                    <td colspan="3" align=right><asp:label id="lblTracker" runat="server"/></td>   
				</tr>
                <tr>
					<td colspan=6>
                        <hr style="width :100%" />
                    </td>
				</tr>
					<td colspan=6 width=100% class="mb-c" style="height: 56px" class="font9Tahoma">	
						<table width="100%" cellspacing="0" cellpadding="3" border="0" class="font9Tahoma">
							<tr style="background-color:#FFCC00">
					     		<td height="26" width="15%">
					     		No.RKB :<BR><asp:TextBox id=txtRKH width=100% maxlength="25" runat="server"/>
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
										<asp:ListItem value="">All</asp:ListItem>
										<asp:ListItem value="1">Confirm</asp:ListItem>
										<asp:ListItem value="3">Active</asp:ListItem>
										<asp:ListItem value="4">Deleted</asp:ListItem>								
									</asp:DropDownList>
								</td>
								<td height="26" width="10%"  valign=bottom align=right><asp:Button id=SearchBtn CssClass="button-small" Text="Search" OnClick=srchBtn_Click runat="server"/>
								</td>
							</tr>
						</table>
				   </td>
				</tr>
				<tr>
					<td colspan=6>					
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
							AllowSorting=True>
							<HeaderStyle CssClass="mr-h" />
							<ItemStyle CssClass="mr-l" />
							<AlternatingItemStyle CssClass="mr-r" />
							
							<Columns>												 
						        <asp:TemplateColumn HeaderText="No.RKB" >
									<ItemTemplate>
						              <asp:LinkButton id=lnkBKM  Text='<%# Container.DataItem("RKBCode") %>' runat=server /> 
						              <asp:HiddenField Id=hidbkm Value='<%# Container.DataItem("RKBCode") %>' runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>
						
						 		
						        <asp:TemplateColumn HeaderText="Divisi" >
                                	<ItemTemplate>
										<asp:Label id=lblEmpDiv text='<%# Container.DataItem("divisi") %>'  runat=server/>
									</ItemTemplate>
								</asp:TemplateColumn>
													
                                 <asp:TemplateColumn HeaderText="Notes" >
                                	<ItemTemplate>
										<asp:Label id=lblNotes text='<%# Container.DataItem("Notes") %>'  runat=server/>
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
								
								<asp:TemplateColumn HeaderText="Status" SortExpression="UserName">
									<ItemTemplate>
										<%# getstat(Container.DataItem("Status")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn>
									<ItemTemplate>
									    <asp:Label id=lblBKMid text='<%# Container.DataItem("RKBCode") %>'  Visible=false runat=server/>
										<asp:Label id=lblstatus text='<%# Container.DataItem("Status") %>'  Visible=false runat=server/>
									</ItemTemplate>
									<ItemStyle HorizontalAlign="Center" />
							     
                                </asp:TemplateColumn>											
							</Columns>
							 <PagerStyle Visible="False" />
          				</asp:DataGrid></td>
				</tr>
				<tr>
				    <td align="left" width=50% style="height: 26px">
						<asp:ImageButton id=NewEmpBtn OnClick="NewEmpBtn_Click" imageurl="../../images/butt_new.gif" AlternateText="Rencana Kerja Bulanan" runat="server"/>
					</td>
					<td align=right width=50% style="height: 26px">
						<asp:ImageButton id="btnPrev" runat="server" imageurl="../../images/icn_prev.gif" alternatetext="Previous" commandargument="prev" onClick="btnPrevNext_Click" />
						<asp:DropDownList id="lstDropList" AutoPostBack="True" onSelectedIndexChanged="PagingIndexChanged" runat="server" />
			         	<asp:Imagebutton id="btnNext" runat="server"  imageurl="../../images/icn_next.gif" alternatetext="Next" commandargument="next" onClick="btnPrevNext_Click" />
					 	<asp:Label id=lblCurrentIndex visible=false text=0 runat=server/>
						<asp:Label id=lblPageCount visible=false text=1 runat=server/>
					</td>
				</tr>
			</table>
			</div>
			</td>
			</table>
		</form>
	</body>
</html>
