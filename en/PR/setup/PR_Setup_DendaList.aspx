<%@ Page Language="vb" src="../../../include/PR_Setup_DendaList.aspx.vb" Inherits="PR_Setup_DendaList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRSetup" src="../../menu/menu_PRsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Denda List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
<%--	<Preference:PrefHdl id=PrefHdl runat="server" />--%>
		<body>
		    <form id="Route" runat="server" class="main-modul-bg-app-list-pu">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:Label id="lblErrEnter" visible="false" text="Please enter " runat="server" />
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:label id="blnUpdate" Visible="False" Runat="server" />
			<asp:label id="sortcol" Visible="False" Runat="server" />
			<asp:Label id="lblCode" visible="false" text=" Code" runat="server" />
			<asp:Label id="lblRate" visible="false" text=" Rate" runat="server" />



		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuPRSetup id=menuPR runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>DENDA LIST</strong><hr style="width :100%" />   
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
								<td width="20%" height="26">Denda Code :<br><asp:TextBox id="srchDendaCode" width="100%" maxlength="8" runat="server"/></td>
								<td width="35%" height="26"><asp:label id="lblDesc" runat="server" /> :<br><asp:TextBox id="srchDescription" width="100%" maxlength="128" runat="server"/></td>
								<td width="15%" height="26">Status :<br><asp:DropDownList id="srchStatus" width="100%" runat="server"/></td>
								<td width="20%" height="26">Last Updated By :<br><asp:TextBox id="srchUpdBy" width="100%" maxlength="128" runat="server"/></td>
								<td width="10%" height="26" valign=bottom align=right><asp:Button ID="Button1" Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
								</tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
						            <asp:DataGrid id=EventData runat=server
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
					                    <asp:BoundColumn Visible=False HeaderText="Denda Code" DataField="DendaCode" />					
					                    <asp:HyperLinkColumn HeaderText="Denda Code" ItemStyle-Width="21%" HeaderStyle-Width="15%"
						                    SortExpression="D.DendaCode" 
						                    DataNavigateUrlField="DendaCode" 
						                    DataNavigateUrlFormatString="PR_Setup_DendaDet.aspx?dendacode={0}" 
						                    DataTextField="DendaCode" />						
					                    <asp:HyperLinkColumn HeaderText="Description" ItemStyle-Width="30%" HeaderStyle-Width="30%"
						                    SortExpression="D.Description" 
						                    DataNavigateUrlField="DendaCode" 
						                    DataNavigateUrlFormatString="PR_Setup_DendaDet.aspx?dendacode={0}" 
						                    DataTextField="Description" />
					                    <asp:TemplateColumn HeaderText="Last Update" SortExpression="D.UpdateDate">
						                    <ItemTemplate>
							                    <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
						                    </ItemTemplate>
						                    <EditItemTemplate >
							                    <asp:TextBox id="UpdateDate" Readonly=TRUE size=8 
								                    Visible=False Text='<%# objGlobal.GetLongDate(Now()) %>'
								                    runat="server"/>
							                    <asp:TextBox id="CreateDate" Visible=False
								                    Text='<%# Container.DataItem("CreateDate") %>'
								                    runat="server"/>
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>
					                    <asp:TemplateColumn HeaderText="Status" SortExpression="D.Status">
						                    <ItemTemplate>
							                    <%# objPR.mtdGetDendaStatus(Container.DataItem("Status")) %>
						                    </ItemTemplate>
						                    <EditItemTemplate>
							                    <asp:DropDownList Visible=False id="StatusList" size=1 runat=server />
							                    <asp:TextBox id="Status" Readonly=TRUE Visible = False
								                    Text='<%# Container.DataItem("Status")%>'
								                    runat="server"/>
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>
					                    <asp:TemplateColumn HeaderText="Updated By" SortExpression="Usr.UserName">
						                    <ItemTemplate>
							                    <%# Container.DataItem("UserName") %>
						                    </ItemTemplate>
						                    <EditItemTemplate >
							                    <asp:TextBox id="UserName" Readonly=TRUE size=8 
								                    Text='<%# Session("SS_USERID") %>'
								                    Visible=False runat="server"/>
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>		
					                    <asp:TemplateColumn ItemStyle-HorizontalAlign=Center>										
						                    <ItemTemplate>
							                    <!--
							                    <asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" runat="server"/>							
							                    -->
							                    <asp:LinkButton id="lbDelete" CommandName="Delete" Text="Delete" runat="server" />
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
					            <asp:ImageButton id=ibNew OnClick="DEDR_Add" imageurl="../../images/butt_new.gif" AlternateText="New Denda" runat="server"/>
				        		<asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print visible=false runat="server"/>
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
