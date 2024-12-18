<%@ Page Language="vb" src="../../../include/WS_setup_ItemPart.aspx.vb" Inherits="WS_ItemPart" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuWS" src="../../menu/menu_wssetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<html>
	<head>
		<title>Workshop Item Part</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
<%--	<Preference:PrefHdl id=PrefHdl runat="server" />--%>
		<body>
		    <form runat="server" ID="Form1" class="main-modul-bg-app-list-pu">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id=lblPleaseEnter visible=false text="Please enter " runat=server />
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:label id="blnUpdate" Visible="False" Runat="server"/>
			<asp:label id=lblCode visible=false text=" Code" runat=server />
			<asp:label id="sortcol" Visible="False" Runat="server"/>



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
							<td><strong>WORKSHOP ITEM PART</strong><hr style="width :100%" />   
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
								<td width="20%" height="26">Item Code :<BR><asp:TextBox id=srchItemCode width=100% maxlength="20" runat="server"/></td>
								<td width="27%" height="26">Description :<BR><asp:TextBox id=srchDesc width=100% maxlength="128" runat="server"/></td>
								<td width="15%" height="26">Part No :<BR><asp:TextBox id=srchPartNo width=100% maxlength="20" runat="server"/></td>
								<td width="10%" height="26">Status :<BR><asp:DropDownList id="srchStatusList" width=100% runat=server /></td>
								<td width="20%" height="26">Last Updated By :<BR><asp:TextBox id=srchUpdateBy width=100% maxlength="128" runat="server"/></td>
								<td width="8%" height="26" valign=bottom align=right><asp:Button ID="Button1" Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
								</tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
						            <asp:DataGrid id="dgItem"
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
							                <asp:TemplateColumn HeaderText="Item Code" SortExpression="ITMP.ItemCode">
								                <ItemStyle width=15% />						
								                <ItemTemplate>
									                <%# Container.DataItem("ItemCode") %>
								                </ItemTemplate>
								                <EditItemTemplate>
									                <asp:TextBox id="txtItemCode" width=100% visible="False"
										                Text='<%# trim(Container.DataItem("ItemCode")) %>'
										                runat="server"/>								
									                <asp:DropDownList id="ddlItemCode" width=100% runat="server" /><BR>
									                <asp:RequiredFieldValidator id=rfvItemCode display=dynamic runat=server 
										                ControlToValidate=ddlItemCode 
										                text="Please select Item Code" />															
								                </EditItemTemplate>
							                </asp:TemplateColumn>
							                <asp:TemplateColumn HeaderText="Description" SortExpression="Description">
								                <ItemStyle width=22% />						
								                <ItemTemplate>
									                <%# Container.DataItem("Description") %>
								                </ItemTemplate>
								                <EditItemTemplate>
									                <asp:TextBox id="txtDescription" width=100%
										                Text='<%# trim(Container.DataItem("Description")) %>'
										                runat="server"/>								
								                </EditItemTemplate>
							                </asp:TemplateColumn>
							                <asp:TemplateColumn HeaderText="Part No" SortExpression="PartNo">
								                <ItemStyle width=15% />						
								                <ItemTemplate>
									                <%# Container.DataItem("PartNo") %>
								                </ItemTemplate>
								                <EditItemTemplate>
									                <asp:label id=lblPartNo visible=false 
										                text='<%# Container.DataItem("PartNo") %>' 
										                runat=server />
									                <asp:TextBox id="txtPartNo" MaxLength="20" width=95%
										                Text='<%# Trim(Container.DataItem("PartNo")) %>'
										                runat="server"/><BR>
									                <asp:label id="lblDupMsg" Text="Part No has been used in this application" Visible=false forecolor=red Runat="server"/>
									                <asp:RequiredFieldValidator id=rfvPartNo display=dynamic runat=server 
										                ControlToValidate=txtPartNo 
										                text="Please enter Part No" />															
									                <asp:RegularExpressionValidator id=revPartNo 
										                ControlToValidate="txtPartNo"
										                ValidationExpression="[a-zA-Z0-9\-]{1,20}"
										                Display="Dynamic"
										                text="Alphanumeric without any space in between only"
										                runat="server"/>
								                </EditItemTemplate>
							                </asp:TemplateColumn>
							                <asp:TemplateColumn HeaderText="Last Updated" SortExpression="ITMP.UpdateDate">
								                <ItemStyle width=10% />						
								                <ItemTemplate>
									                <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
								                </ItemTemplate>
								                <EditItemTemplate >
									                <asp:TextBox id="txtUpdateDate" visible=false  
										                Text='<%# objGlobal.GetLongDate(Now()) %>'
										                runat="server"/>
									                <asp:TextBox id="txtCreateDate" Visible=False
										                Text='<%# Container.DataItem("CreateDate") %>'
										                runat="server"/>
								                </EditItemTemplate>
							                </asp:TemplateColumn>
							                <asp:TemplateColumn HeaderText="Status" SortExpression="ITMP.Status">
								                <ItemStyle width=10% />						
								                <ItemTemplate>
									                <%# objWS.mtdGetItemPartStatus(Container.DataItem("Status")) %>
								                </ItemTemplate>
								                <EditItemTemplate>
									                <asp:DropDownList id="ddlStatus" visible=false width=100% runat=server />
									                <asp:TextBox id="txtStatus" Visible=False
										                Text='<%# Container.DataItem("Status")%>'
										                runat="server"/>
								                </EditItemTemplate>
							                </asp:TemplateColumn>
							                <asp:TemplateColumn HeaderText="Updated By" SortExpression="UserName">
								                <ItemStyle width=20% />						
								                <ItemTemplate>
									                <%# Container.DataItem("UserName") %>
								                </ItemTemplate>
								                <EditItemTemplate >
									                <asp:TextBox id="txtUserName" visible=false width=100%  
										                Text='<%# Container.DataItem("UserName") %>'
										                runat="server"/>
								                </EditItemTemplate>
							                </asp:TemplateColumn>					
							                <asp:TemplateColumn ItemStyle-HorizontalAlign=Center>		
								                <ItemStyle width=8% />						
								                <ItemTemplate>
									                <asp:LinkButton id="Edit" CommandName="Edit" Text="Edit" runat="server"/>
								                </ItemTemplate>						
								                <EditItemTemplate>				
									                <asp:LinkButton id="Update" CommandName="Update" Text="Save" runat="server"/>
									                <asp:LinkButton id="Delete" CommandName="Delete" Text="Disable" runat="server"/>
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
					            <asp:ImageButton id=ibNew onClick=DEDR_Add imageurl="../../images/butt_new.gif" AlternateText="New Workshop Item Part" runat=server/>
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
