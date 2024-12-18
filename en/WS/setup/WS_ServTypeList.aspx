<%@ Page Language="vb" src="../../../include/WS_setup_servtypelist.aspx.vb" Inherits="WS_ServTypeList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuWS" src="../../menu/menu_wssetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<html>
	<head>
		<title>Service Type List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
<%--		<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	</head>
	<body>
		<form id=frmServTypeList runat="server" class="main-modul-bg-app-list-pu">
			<asp:label id="blnUpdate" Visible="False" Runat="server"></asp:label>
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:Label id=lblDuplicateCode visible=false Text="Duplicate Service Type." runat=server />
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:Label id=SortCol Visible=False Runat="server" />


    
		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuWS id=menuWS runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong><asp:label id="lblTitle" runat=server /> LIST</strong><hr style="width :100%" />   
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
								<td width="20%" height="26"><asp:label id=lblServType runat=server /> :<BR><asp:TextBox id=txtServType width=100% maxlength="8" runat="server" /></td>
								<td width="37%" height="26"><asp:label id=lblServTypeDesc runat=server /> :<BR><asp:TextBox id=txtDescription width=100% maxlength="128" runat="server"/></td>
								<td width="15%" height="26">Status :<BR>
									<asp:DropDownList id="ddlStatus" width=100% runat=server>
										<asp:ListItem value="0">All</asp:ListItem>
										<asp:ListItem value="1" selected>Active</asp:ListItem>
										<asp:ListItem value="2">Deleted</asp:ListItem>
									</asp:DropDownList>
								</td>
								<td width="20%" height="26">Last Updated By :<BR><asp:TextBox id=txtLastUpdate width=100% maxlength="128" runat="server"/></td>
								<td width="8%" height="26" valign=bottom align=right><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
								</tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
						            <asp:DataGrid id=dgServType runat=server
							            AutoGenerateColumns=false width=100% 
							            GridLines=none 
							            Cellpadding=2
							            OnEditCommand="DEDR_Edit"
							            OnUpdateCommand="DEDR_Update"
							            OnCancelCommand="DEDR_Cancel"
							            OnDeleteCommand=DEDR_Delete 
							            AllowPaging=True 
							            Allowcustompaging=False 
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
								            <asp:BoundColumn Visible=False HeaderText="Service Type" DataField="ServTypeCode" />
								
								            <asp:TemplateColumn SortExpression="ServTypeCode">
									            <ItemStyle width=15% />
									            <ItemTemplate>
										            <%# Container.DataItem("ServTypeCode") %>
									            </ItemTemplate>
									            <EditItemTemplate>
										            <asp:TextBox id="ServTypeCode" width=95% MaxLength="8"
											            Text='<%# trim(Container.DataItem("ServTypeCode")) %>'
											            runat="server"/>
										            <asp:RequiredFieldValidator id=validateServTypeCode display=Dynamic runat=server 
												            ControlToValidate=ServTypeCode />
										            <asp:RegularExpressionValidator id=svrtypeCode 
												            ControlToValidate="ServTypeCode"
												            ValidationExpression="[a-zA-Z0-9\-]{1,8}"
												            Display="Dynamic"
												            text="Alphanumeric without any space in between only."
												            runat="server"/>
										            <asp:label id="lblEmptryServType"  Text="Please enter Service Type" Visible = false forecolor=red Runat="server"/>					
										            <asp:label id="lblErrDupST"	Text="" Visible = false forecolor=red Runat="server"/>
									            </EditItemTemplate>
								            </asp:TemplateColumn>									
								            <asp:TemplateColumn SortExpression="Description">
									            <ItemStyle width=15% />
									            <ItemTemplate>
										            <%# Container.DataItem("Description") %>
									            </ItemTemplate>
									            <EditItemTemplate>
										            <asp:TextBox id="Description" width=100% MaxLength="128"
											            Text='<%# trim(Container.DataItem("Description")) %>'
											            runat="server"/>
										            <asp:RequiredFieldValidator id=validateDesc display=Dynamic runat=server 
												            ControlToValidate=Description />															
									            </EditItemTemplate>
								            </asp:TemplateColumn>					
								            <asp:TemplateColumn HeaderText="Last Update" SortExpression="UpdateDate">
									            <ItemStyle width=15% />														
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
								            <asp:TemplateColumn HeaderText="Status" SortExpression="Status">
									            <ItemStyle width=10% />						
									            <ItemTemplate>
										            <%# objWS.mtdGetStatus(Container.DataItem("Status")) %>
									            </ItemTemplate>
									            <EditItemTemplate>
										            <asp:DropDownList Visible=False id="StatusList" size=1 runat=server />
										            <asp:TextBox id="Status" Readonly=TRUE Visible = False
											            Text='<%# Container.DataItem("Status")%>'
											            runat="server"/>
									            </EditItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="Updated By" SortExpression="UpdateID">
									            <ItemStyle width=20% />														
									            <ItemTemplate>
										            <%# Container.DataItem("UserName") %>
									            </ItemTemplate>
									            <EditItemTemplate >
										            <asp:TextBox id="UserName" Readonly=TRUE size=8 
											            Text='<%# Session("SS_USERID") %>'
											            Visible=False runat="server"/>
									            </EditItemTemplate>
								            </asp:TemplateColumn>								
								            <asp:TemplateColumn>					
									            <ItemTemplate>
										            <asp:LinkButton id="Edit" CommandName="Edit"   Text="Edit" runat="server"/>
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
					            <asp:ImageButton id=NewServTypeBtn onClick="DEDR_Add" imageurl="../../images/butt_new.gif" AlternateText="New Service Type" runat=server/>
				        		<asp:ImageButton id=ibPrint onClick="btnPreview_Click" imageurl="../../images/butt_print.gif" AlternateText=Print runat="server"/>
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
