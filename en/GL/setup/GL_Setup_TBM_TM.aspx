<%@ Page Language="vb" src="../../../include/GL_Setup_TBM_TM.aspx.vb" Inherits="GL_Setup_TBM_TM" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLSetup" src="../../menu/menu_GLsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Block Group List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<%--<Preference:PrefHdl id=PrefHdl runat="server" />--%>
		<body>
		    <form id="frmBlockGrp" runat="server" class="main-modul-bg-app-list-pu">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id=lblErrEnter visible=false text="Please enter " runat=server />
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:label id="blnUpdate" Visible="False" Runat="server" />
			<asp:label id=lblCode visible=false text=" Code" runat=server />
			<asp:label id="sortcol" Visible="False" Runat="server" />
			
			
            		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuGLSetup id=menuGL runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong><asp:label id="lblTitle" runat="server" /> MUTASI TBM - TM</strong><hr style="width :100%" />   
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
								<td width="20%" height="26"></td>
								<td width="35%" height="26"></td>
								<td width="15%" height="26"></td>
								<td width="20%" height="26">Status :<br><asp:DropDownList id="srchStatus" width="100%" runat="server">
								<asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
								<asp:ListItem Value="2" >Delete</asp:ListItem>
								</asp:DropDownList>	
								</td>
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
						            <asp:DataGrid id="EventData"
						AutoGenerateColumns="false" width="100%" runat="server"
						GridLines = none
						Cellpadding = "2"
						OnEditCommand="DEDR_Edit"
						OnUpdateCommand="DEDR_Update"
						OnCancelCommand="DEDR_Cancel"
						OnDeleteCommand="DEDR_Delete"
						AllowPaging="True" 
						Allowcustompaging="False"
						Pagesize="15" 
						OnPageIndexChanged="OnPageChanged"
						Pagerstyle-Visible="False"
						OnSortCommand="Sort_Grid" 
						AllowSorting="True"
                                        class="font9Tahoma">
								
							            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>		
					<Columns>		
					
					<asp:TemplateColumn ItemStyle-Width="7%"  HeaderText="ID">
						<ItemTemplate>
							<%#Container.DataItem("ID")%>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox id="txtID" width=100% MaxLength="8"  Text='<%# Container.DataItem("ID") %>' runat="server" Enabled="False"/>
						</EditItemTemplate>
					</asp:TemplateColumn>

					
					<asp:TemplateColumn ItemStyle-Width="10%"  HeaderText="Periode">
						<ItemTemplate>
							<%#Container.DataItem("AccYear") * 100 +  Container.DataItem("AccMonth")%> 
						</ItemTemplate>
						<EditItemTemplate>
							<asp:DropDownList id="ddlAccMonth" width=49% runat=server>
								<asp:ListItem Value="1" >Jan</asp:ListItem>
								<asp:ListItem Value="2" >Feb</asp:ListItem>
								<asp:ListItem Value="3" >Mar</asp:ListItem>
								<asp:ListItem Value="4" >Apr</asp:ListItem>
								<asp:ListItem Value="5" >May</asp:ListItem>
								<asp:ListItem Value="6" >Jun</asp:ListItem>
								<asp:ListItem Value="7" >Jul</asp:ListItem>
								<asp:ListItem Value="8" >Aug</asp:ListItem>
								<asp:ListItem Value="9" >Sep</asp:ListItem>
								<asp:ListItem Value="10" >Oct</asp:ListItem>
								<asp:ListItem Value="11" >Nov</asp:ListItem>
								<asp:ListItem Value="12" >Dec</asp:ListItem>
							</asp:DropDownList>	
							<asp:TextBox id="txtAccYear" width=45% MaxLength="6" Text='<%# Container.DataItem("AccYear") %>' runat="server"/>
						    <asp:TextBox id="tmpAccMonth" width=100% Visible=False	Text='<%# Container.DataItem("AccMonth") %>' runat="server"/>
							<asp:TextBox id="tmpAccYear" width=100% Visible=False	Text='<%# Container.DataItem("AccYear") %>' runat="server"/>
						</EditItemTemplate>
					</asp:TemplateColumn>
					
					
					<asp:TemplateColumn ItemStyle-Width="27%"  HeaderText="Blok TBM">
							<ItemTemplate>
							   <%#Container.DataItem("TBMSubblkCode")%>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:DropDownList id="ddlTBM" width=98% runat=server>
								</asp:DropDownList>	
                               	<asp:Label id="lblTBM" visible=False Text='<%# trim(Container.DataItem("TBMSubblkCode")) %>' runat=server/>							
							</EditItemTemplate>
						</asp:TemplateColumn>		
						
					<asp:TemplateColumn ItemStyle-Width="27%"  HeaderText="Blok TM">
							<ItemTemplate>
							   <%#Container.DataItem("TMSubblkCode")%>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:DropDownList id="ddlTM" width=98% runat=server>
								</asp:DropDownList>	
                               	<asp:Label id="lblTM" visible=False Text='<%# trim(Container.DataItem("TMSubblkCode")) %>' runat=server/>							
							</EditItemTemplate>
						</asp:TemplateColumn>		
						
					<asp:TemplateColumn HeaderText="Last Update" SortExpression="blk.UpdateDate">
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
					
					<asp:TemplateColumn HeaderText="Status" SortExpression="blk.Status">
						<ItemTemplate>
							 <%#IIf(Trim(Container.DataItem("Status")) = "1", "Active", "Delete")%>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:DropDownList Visible=False id="StatusList" size=1 runat=server />
							<asp:TextBox id="Status" Readonly=TRUE Visible = False
								Text='<%# Container.DataItem("Status")%>'
								runat="server"/>
						</EditItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Updated By" ItemStyle-Width="15%" SortExpression="usr.UserName">
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
							<asp:LinkButton id="Edit" CommandName="Edit" Text="Edit" runat="server"/>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:LinkButton id="Update" CommandName="Update" Text="Save" runat="server"/>
							<asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" runat="server"/>
							<asp:LinkButton id="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation=False runat="server"/>
						</EditItemTemplate>
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
					            <asp:ImageButton id=NewBlkGrp onClick=DEDR_Add imageurl="../../images/butt_new.gif" AlternateText="Mutasi TBM-TM" runat=server/>
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



			 <input id="isNew" type="hidden" runat="server" />
			</FORM>
		</body>
</html>
