<%@ Page Language="vb" src="../../../include/GL_mthend_GCDist.aspx.vb" Inherits="GL_mthend_GCDist" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLMthEnd" src="../../menu/menu_glmthend.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>General Charges Distribution</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
         <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<form id=frmVehicle runat="server">
         <div class="kontenlist">
    		<asp:label id=lblCode visible=false text=" Code" runat=server />
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<table border="0" cellspacing="1" width="100%" class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuGLMthEnd id=MenuGLMthEnd runat="server" /></td>
				</tr>
				<tr>
					<td colspan="3"><strong> GENERAL CHARGES DISTRIBUTION</strong></td>
					<td colspan="3" align=right><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6>
                    <hr style="width :100%" />
                    </td>
				</tr>
				<tr>
					<td colspan=6 width=100% class="mb-c">
						<table width="100%" cellspacing="0" cellpadding="3" border="0" align="center" class="font9Tahoma">
							<tr class="mb-t">
								<td width="20%" height="26">Accounting Period :<BR>
									<asp:TextBox id=srchAccPeriod width=100% maxlength="7" runat="server" />
								</td>
								<td width="20%" height="26">Mature Allocation (%) :<BR>
									<asp:TextBox id=srchMature width=100% maxlength="3" runat="server" />
								</td>
								<td width="20%" height="26">Immature Allocation (%) :<BR>
									<asp:TextBox id=srchImmature width=100% maxlength="3" runat="server" />
								</td>
								<td width="15%" height="26">Status :<BR>
									<asp:DropDownList id="srchStatus" width=100% runat=server>
										<asp:ListItem value="">All</asp:ListItem>
										<asp:ListItem value="1" selected>Active</asp:ListItem>
										<asp:ListItem value="2">Distributed</asp:ListItem>
									</asp:DropDownList>
								</td>
								<td width="15%" height="26" align=left>Last Updated By :<BR><asp:TextBox id=txtLastUpdate width=100% maxlength="128" runat="server"/></td>
								<td width="10%" height="26" valign=bottom align=right><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click runat="server"/></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan=6>					
						<asp:DataGrid id=dgResult runat=server
							AutoGenerateColumns=false width=100% 
							GridLines=none 
							Cellpadding=2 
							AllowPaging=True 
							Allowcustompaging=False 
							Pagesize=15 
							OnPageIndexChanged=OnPageChanged 
							Pagerstyle-Visible=False 
							OnUpdateCommand=DEDR_Update 
							OnSortCommand=Sort_Grid  
							OnItemDataBound=dgResult_OnItemDataBound
							AllowSorting=True CssClass="font9Tahoma">
                        <HeaderStyle  BackColor="#CCCCCC" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<ItemStyle BackColor="#FEFEFE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<AlternatingItemStyle BackColor="#EEEEEE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>	
							<Columns>
								<asp:HyperLinkColumn HeaderText="Accounting Period" 
									ItemStyle-Width="15%"
									SortExpression="HD.AccYear, HD.AccMonth" 
									DataNavigateUrlField="AccPeriod" 
									DataNavigateUrlFormatString="GL_mthend_GCDist_Loc.aspx?accperiod={0}" 
									DataTextField="AccPeriod" />

								<asp:TemplateColumn HeaderText="Mature Allocation" ItemStyle-Width="20%" SortExpression="HD.MaturePercent">
									<ItemTemplate>
										<%# ObjGlobal.GetIDDecimalSeparator(Container.DataItem("MaturePercent")) %>
									</ItemTemplate>
								</asp:TemplateColumn>

								<asp:TemplateColumn HeaderText="Immature Allocation" ItemStyle-Width="20%" SortExpression="HD.ImmaturePercent">
									<ItemTemplate>
										<%# ObjGlobal.GetIDDecimalSeparator(Container.DataItem("ImmaturePercent")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
									
								<asp:TemplateColumn HeaderText="Last Update" ItemStyle-Width="10%" SortExpression="HD.UpdateDate">
									<ItemTemplate>
										<%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
									
								<asp:TemplateColumn HeaderText="Status" ItemStyle-Width="10%" SortExpression="HD.Status">
									<ItemTemplate>
										<%# objGLMthEnd.mtdGetGCDistributeStatus(Container.DataItem("Status")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
									
								<asp:TemplateColumn HeaderText="Updated By" ItemStyle-Width="15%" SortExpression="USR.UserName">
									<ItemTemplate>
										<%# Container.DataItem("UserName") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn ItemStyle-Width="10%" ItemStyle-HorizontalAlign=center>
									<ItemTemplate>
										<asp:LinkButton id=lblDistribute CommandName=Update Text=Distribute runat=server />
										<asp:Label id=lblStatus Text='<%# Container.DataItem("Status") %>' Visible=False runat=server />
										<asp:Label id=lblAccPeriod Visible=False text=<%# Container.DataItem("AccPeriod")%> Runat="server" />
										<asp:Label id=lblAccMonth Visible=False text=<%# Container.DataItem("AccMonth")%> Runat="server" />
										<asp:Label id=lblAccYear Visible=False text=<%# Container.DataItem("AccYear")%> Runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>	
							</Columns>
						</asp:DataGrid><BR>
					</td>
				</tr>
				<tr>
					<td align=right colspan="6">
						<asp:ImageButton id="btnPrev" runat="server" imageurl="../../images/icn_prev.gif" alternatetext="Previous" commandargument="prev" onClick="btnPrevNext_Click" />
						<asp:DropDownList id="lstDropList" runat="server"
							AutoPostBack="True" 
							onSelectedIndexChanged="PagingIndexChanged" />
			         	<asp:Imagebutton id="btnNext" runat="server"  imageurl="../../images/icn_next.gif" alternatetext="Next" commandargument="next" onClick="btnPrevNext_Click" />
					</td>
				</tr>
				<tr>
					<td align="left" ColSpan=6>
						<asp:Label id=lblGCSuccess visible=false forecolor=red text="General changes is successfully distributed.<br>" runat=server/>
						<asp:Label id=lblErrGCFail visible=false forecolor=red text="Distribution process has been terminated because there are some errors when distributing general charges.<br>" runat=server/>
						<asp:Label id=lblErrGCNoAllocation visible=false forecolor=red text="Distribution process has been terminated when distributing general charges. Kindly define the general charges allocation for mature / immature before try again.<br>" runat=server/>
						<asp:Label id=lblErrGCNoLocation visible=false forecolor=red text="Distribution process has been terminated when distributing general charges. Kindly define the location(s) to which the general charges will be distributed.<br>" runat=server/>
						<asp:ImageButton id=btnNew onClick=btnNew_Click imageurl="../../images/butt_new.gif" AlternateText="New" runat=server/>
						&nbsp;<asp:Label id=SortCol Visible=False Runat="server" />
					</td>
				</tr>
			</table>
        </div>
		</FORM>
	</body>
</html>
