<%@ Page Language="vb" src="../../../include/PR_setup_RiceRationList.aspx.vb" Inherits="PR_setup_RiceRationList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRSetup" src="../../menu/menu_prsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Rice Ration List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<%--<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	</head>
	<body>
		<form id=frmRiceRationList runat="server" class="main-modul-bg-app-list-pu">
    		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:Label id=lblCode text=" Code" visible=false runat=server />		


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
							<td><strong><asp:label id="lblTitle" runat="server" /> LIST</strong><hr style="width :100%" />   
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
								<td width="20%" height="26"><asp:label id="lblRiceCode" runat="server" /> :<BR><asp:TextBox id=txtRiceRationCode width=100% maxlength="8" runat="server" /></td>
								<td width="35%" height="26"><asp:label id="lblRiceDesc" runat="server" /> :<BR><asp:TextBox id=txtDescription width=100% maxlength="128" runat="server" /></td>
								<td width="15%" height="26">Status :<BR><asp:DropDownList id="ddlStatus" width=100% runat=server /></td>
								<td width="20%" height="26">Last Updated By :<BR><asp:TextBox id=txtLastUpdate width=100% maxlength="128" runat="server"/></td>
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
								                <asp:BoundColumn Visible=False DataField="RiceRationCode" />
								                <asp:HyperLinkColumn HeaderText="Rice Ration Code" ItemStyle-Width="15%"
									                SortExpression="Rice.RiceRationCode" 
									                DataNavigateUrlField="RiceRationCode" 
									                DataNavigateUrlFormatString="PR_setup_RiceRationDet.aspx?tbcode={0}" 
									                DataTextField="RiceRationCode" />
									
								                <asp:HyperLinkColumn ItemStyle-Width="40%" 
									                SortExpression="Rice.Description" 
									                DataNavigateUrlField="RiceRationCode" 
									                DataNavigateUrlFormatString="PR_setup_RiceRationDet.aspx?tbcode={0}" 
									                DataTextField="Description" />
								
								                <asp:TemplateColumn HeaderText="Last Update" ItemStyle-Width="10%" SortExpression="Rice.UpdateDate">
									                <ItemTemplate>
										                <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									                </ItemTemplate>
								                </asp:TemplateColumn>
									
								                <asp:TemplateColumn HeaderText="Status" ItemStyle-Width="10%" SortExpression="Rice.Status">
									                <ItemTemplate>
										                <%# objPRSetup.mtdGetAttListStatus(Container.DataItem("Status")) %>
									                </ItemTemplate>
								                </asp:TemplateColumn>
									
								                <asp:TemplateColumn HeaderText="Updated By" ItemStyle-Width="10%" SortExpression="Usr.UserName">
									                <ItemTemplate>
										                <%# Container.DataItem("UserName") %>
									                </ItemTemplate>
								                </asp:TemplateColumn>
								                <asp:TemplateColumn ItemStyle-Width="15%" ItemStyle-HorizontalAlign=center>
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
					            <asp:ImageButton id=NewRiceBtn OnClick="NewRiceRationBtn_Click" imageurl="../../images/butt_new.gif" AlternateText="New Rice Ration Code" runat="server"/>
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
