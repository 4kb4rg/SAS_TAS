<%@ Page Language="vb" trace="False" src="../../../include/BD_setup_areaStatement.aspx.vb" Inherits="BD_AreaStatement" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuBDsetup" src="../../menu/menu_BDSetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Stock Analysis List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<%--<Preference:PrefHdl id=PrefHdl runat="server" />--%>
		<body>
		    <form runat="server" ID="Form1" class="main-modul-bg-app-list-pu">
			<asp:label id="SQLStatement" Visible="False" Runat="server"></asp:label>
			<asp:label id="SortExpression" Visible="False" Runat="server"></asp:label>
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="blnUpdate" Visible="False" Runat="server"></asp:label>
			<asp:label id="sortcol" Visible="False" Runat="server"></asp:label>
			<asp:label id="curStatus" Visible="False" Runat="server"></asp:label>
			<asp:Label id="ErrorMessage" runat="server" />

			<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuBDsetup id=menuIN runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>Budgeting Periods</strong><hr style="width :100%" />   
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
								<td width="18%" height="26" valign=bottom>Area :<BR>
									<asp:TextBox id=srchProdTypeCode width=100% maxlength="8" runat="server"/>
								</td>
								<td width="40%" height="26" valign=bottom>Area Size :<BR>
									<asp:TextBox id=srchDesc width=100% maxlength="64" runat="server"/>
								</td>
								<td width="10%" height="26" valign=bottom>Status :<BR>
									<asp:DropDownList id="srchStatusList" width=100% runat=server>
									</asp:DropDownList>
								</td>
								<td width="10%" height="26" valign=bottom>Last Update By :<BR><asp:TextBox id=srchUpdateBy width=100% maxlength="10" runat="server"/></td>
								<td width="8%" height="26" valign=bottom align=right><asp:Button ID="Button1" Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
								</tr>
							</table>
							
							</td>
						</tr>
                        <tr class="font9Tahoma">
					        <td colspan="4" width=60%><asp:label id="lblLocTag" text="Budgeting Location" runat="server"/> : <asp:label id="lblLocCode" runat="server"/></td>
					        
				        </tr>
				        <tr class="font9Tahoma">
					        <td colspan="4" width=60%><asp:label id="lblBgtTag" text="Budgeting Period"  runat="server"/> : <asp:label id="lblBgtPeriod" runat="server"/></td>
					         
				        </tr>
				        <tr class="font9Tahoma">
					        <td colspan="4" width=60%>&nbsp;</td>
					        
				        </tr>
				        <tr>
					        <td colspan=6><hr size="1" noshade></td>
				        </tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
						            <asp:DataGrid id="TitleData"
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
					
					<asp:TemplateColumn HeaderText ="Area" >
						<ItemTemplate>
							<%# Container.DataItem("BGTPeriod") %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox id="txtBGTPeriod" MaxLength="8" width=95%
									Text='<%# trim(Container.DataItem("BGTPeriod")) %>'
									runat="server"/>
							<BR>
							<asp:label id="lblDupMsg"  Text="Code already exist" Visible = false forecolor=red Runat="server"/>
							<asp:label id="lblPeriodID" Text='<%# Container.DataItem("PeriodID") %>'  Visible = false Runat="server"/>
							<asp:RequiredFieldValidator id=validateCode display=dynamic runat=server 
									ControlToValidate=txtBGTPeriod />
							<asp:RegularExpressionValidator id=revCode 
								ControlToValidate="txtBGTPeriod"
								ValidationExpression="[a-zA-Z0-9\-]{1,8}"
								Display="Dynamic"
								text="Alphanumeric without any space in between only."
								runat="server"/>
						</EditItemTemplate>
					</asp:TemplateColumn>
	
					<asp:TemplateColumn HeaderText="Start Month" >
						<ItemTemplate>
							<asp:label id="lblStartMonth" Text='<%# Container.DataItem("StartMonth") %>'  Runat="server"/>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:DropDownList id="ddlStartMonth" runat=server >
								<asp:ListItem value="1">1</asp:ListItem>
								<asp:ListItem value="2">2</asp:ListItem>
								<asp:ListItem value="3">3</asp:ListItem>
								<asp:ListItem value="4">4</asp:ListItem>
								<asp:ListItem value="5">5</asp:ListItem>
								<asp:ListItem value="6">6</asp:ListItem>
								<asp:ListItem value="7">7</asp:ListItem>
								<asp:ListItem value="8">8</asp:ListItem>
								<asp:ListItem value="9">9</asp:ListItem>
								<asp:ListItem value="10">10</asp:ListItem>
								<asp:ListItem value="11">11</asp:ListItem>
								<asp:ListItem value="12">12</asp:ListItem>
							</asp:DropDownList>

							<asp:RequiredFieldValidator id=validateStrtMth display=dynamic runat=server 
									ControlToValidate=ddlStartMonth />															
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Start Year" SortExpression="StartYear">
						<ItemTemplate>
							<asp:label id="lblStartYear" Text='<%# Container.DataItem("StartYear") %>'  Runat="server"/>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox id="txtStartYear" width=100% MaxLength="4"
								Text='<%# trim(Container.DataItem("StartYear")) %>'
								runat="server"/>
							<asp:RequiredFieldValidator id=validateDesc display=dynamic runat=server 
									ControlToValidate=txtStartYear />															
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="End Month" SortExpression="EndMonth">
						<ItemTemplate>
							<asp:label id="lblEndMonth" Text='<%# Container.DataItem("EndMonth") %>'  Runat="server"/>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:DropDownList id="ddlEndMonth" runat=server >
								<asp:ListItem value="1">1</asp:ListItem>
								<asp:ListItem value="2">2</asp:ListItem>
								<asp:ListItem value="3">3</asp:ListItem>
								<asp:ListItem value="4">4</asp:ListItem>
								<asp:ListItem value="5">5</asp:ListItem>
								<asp:ListItem value="6">6</asp:ListItem>
								<asp:ListItem value="7">7</asp:ListItem>
								<asp:ListItem value="8">8</asp:ListItem>
								<asp:ListItem value="9">9</asp:ListItem>
								<asp:ListItem value="10">10</asp:ListItem>
								<asp:ListItem value="11">11</asp:ListItem>
								<asp:ListItem value="12">12</asp:ListItem>
							</asp:DropDownList>
						
							<asp:RequiredFieldValidator id=validateEndMth display=dynamic runat=server 
									ControlToValidate=ddlEndMonth />															
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="End Year" SortExpression="EndYear">
						<ItemTemplate>
							<asp:label id="lblEndYear" Text='<%# Container.DataItem("EndYear") %>'  Runat="server"/>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox id="txtEndYr" width=100% MaxLength="64"
								Text='<%# trim(Container.DataItem("EndYear")) %>'
								runat="server"/>
							<asp:RequiredFieldValidator id=validateEndYr display=dynamic runat=server 
									ControlToValidate=txtEndYr />															
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Status" >
						<ItemTemplate>
							<%# objBD.mtdGetPeriodStatus(Container.DataItem("Status")) %>
						</ItemTemplate>
						<EditItemTemplate>
						</EditItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Last Update" >
						<ItemTemplate>
							<%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
						</ItemTemplate>
						<EditItemTemplate >
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Updated By" >
						<ItemTemplate>
							<%# Container.DataItem("UserName") %>
						</ItemTemplate>
						<EditItemTemplate >
						</EditItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn>
					
						<ItemTemplate>
						<asp:LinkButton id="Edit" CommandName="Edit"   Text="Edit"
							runat="server"/>
						</ItemTemplate>
						
						<EditItemTemplate>
						<asp:LinkButton id="Update" CommandName="Update" Text="Save"
							runat="server"/>
						<asp:LinkButton id="Delete" CommandName="Delete" Text="Delete"
							runat="server"/>
						<asp:LinkButton id="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation=False
							runat="server"/>
						</EditItemTemplate>
					</asp:TemplateColumn>

					</Columns>
					</asp:DataGrid>
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
					            <asp:label id="lblPeriodErr" Text='Budget period may not overlap !' forecolor=red Visible=False Runat="server"/>
							</td>
						</tr>
                        <tr>
                            <td>
                                <asp:ImageButton id=ibNew OnClick="DEDR_Add" imageurl="../../images/butt_.gif" AlternateText="New Period" runat="server"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
 					            <asp:Button ID="ibNew2" class="button-small" runat="server" Text="New"  />&nbsp;
                             
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
				</FORM>

		</body>
</html>
