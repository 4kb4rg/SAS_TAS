<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="MenuPDSetup" src="../../menu/menu_PDSetup.ascx"%>
<%@ Page Language="vb" src="../../../include/PM_Setup_MachineCriteria.aspx.vb" Inherits="PM_Setup_MachineCriteria" %>
<HTML>
	<HEAD>
		<title>Machine Criteria List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
<%--		<Preference:PrefHdl id="PrefHdl" runat="server" />--%>
	</HEAD>
	<body>
		<form runat="server" ID="Form1" class="main-modul-bg-app-list-pu">
			<asp:Label id="lblErrMessage" visible="false" Text="Error while initiating component." runat="server" />
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:label id="blnUpdate" Visible="False" Runat="server" />
			<asp:Label id="lblCode" visible="false" text=" Code" runat="server" />
			<asp:label id="lblList" visible="false" text=" LIST" runat="server" />
			<asp:label id="sortcol" Visible="False" Runat="server" />


		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuPDSetup id="menuPD" runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>MACHINE CRITERIA LIST</strong><hr style="width :100%" />   
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
								<td valign="bottom" width="15%">Station :<BR>
									<asp:DropDownList id="ddlStation" width="100%" runat="server" /></td>
								<td valign="bottom" width="15%">Machine :<BR>
									<asp:DropDownList id="ddlMachine" width="100%" runat="server" /></td>
								<td valign="bottom" width="12%">Criteria Field :<BR>
									<asp:TextBox id="txtCriteriaField" width="100%" maxlength="32" runat="server" /></td>
								<td valign="bottom" width="10%">Field Type :<BR>
									<asp:DropDownList id="ddlType" width="100%" runat="server" /></td>
								<td valign="bottom" width="12%">UsedFor :<BR>
									<asp:DropDownList id="ddlUsedFor" width="100%" runat="server" /></td>
								<td valign="bottom" width="10%">Status :<BR>
									<asp:DropDownList id="ddlStatus" width="100%" runat="server" /></td>
								<td valign="bottom" width="15%">Last Updated By :<BR>
									<asp:TextBox id="txtLastUpdate" width="100%" maxlength="128" runat="server" /></td>
								<td valign="bottom" width="10%" align="right"><asp:Button id="SearchBtn" Text="Search" OnClick="srchBtn_Click" runat="server" class="button-small"/></td>
								</tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
						            <asp:DataGrid id="EventData" AutoGenerateColumns="false" width="100%" runat="server" GridLines="none"
							            Cellpadding="2" OnEditCommand="DEDR_Edit" OnUpdateCommand="DEDR_Update" OnCancelCommand="DEDR_Cancel"
							            OnDeleteCommand="DEDR_Delete" AllowPaging="True" Allowcustompaging="False" Pagesize="15" OnPageIndexChanged="OnPageChanged"
							            Pagerstyle-Visible="False" OnSortCommand="Sort_Grid" AllowSorting="True"
                                                    class="font9Tahoma">
								
							                        <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                        <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                        <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
							            <Columns>
								            <asp:TemplateColumn HeaderText="Station" SortExpression="M.Station">
									            <ItemStyle Width="10%" />
									            <ItemTemplate>
										            <%# Container.DataItem("Station") %>
									            </ItemTemplate>
									            <EditItemTemplate>
										            <asp:DropDownList id="ddlEdtStation" AutoPostback="True" OnSelectedIndexChanged=ddlEdtStation_Select runat="server" />
										            <asp:Label id=lblEdtStation visible=false text= '<%# Container.DataItem("Station") %>' runat=server/>
										            <asp:RequiredFieldValidator id="rfvStation" display="Dynamic" runat="server" ErrorMessage="Please enter Station."
											            ControlToValidate="ddlEdtStation" />
									            </EditItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="Machine" SortExpression="M.Machine">
									            <ItemStyle Width="10%" />
									            <ItemTemplate>
										            <%# Container.DataItem("Machine") %>
									            </ItemTemplate>
									            <EditItemTemplate>
										            <asp:DropDownList id="ddlEdtMachine" AutoPostback="False" runat="server" />
										            <asp:Label id=lblEdtMachine visible=false text='<%# Container.DataItem("Machine") %>' runat=server/>
										            <asp:RequiredFieldValidator id="rfvMachine" display="Dynamic" runat="server" ErrorMessage="Please enter Machine."
											            ControlToValidate="ddlEdtMachine" />
									            </EditItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="Criteria Field" SortExpression="M.CriteriaField">
									            <ItemStyle Width="20%" />
									            <ItemTemplate>
										            <%# Container.DataItem("CriteriaField") %>
									            </ItemTemplate>
									            <EditItemTemplate>
										            <asp:TextBox id="txtEdtCriteriaField" width=100% MaxLength="32" Text= '<%# trim(Container.DataItem("CriteriaField")) %>' runat="server"/>
										            <asp:RequiredFieldValidator id="rfvCriteriaField" display="Dynamic" runat="server" ErrorMessage="Please enter Criteria Field."
											            ControlToValidate="txtEdtCriteriaField" />
									            </EditItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="Field Type" SortExpression="M.FieldType">
									            <ItemStyle Width="10%" />
									            <ItemTemplate>
										            <%# objPM.mtdGetFieldType(Val(Container.DataItem("FieldType"))) %>
									            </ItemTemplate>
									            <EditItemTemplate>
										            <asp:DropDownList id="ddlEdtFieldType" AutoPostback="False" runat="server" />
										            <asp:Label id="lblEdFieldType" Text= '<%#Container.DataItem("FieldType")%>' visible=false runat="server" />
										            <asp:RequiredFieldValidator id="rfvFieldType" display="Dynamic" runat="server" ErrorMessage="Please enter Field Type."
											            ControlToValidate="ddlEdtFieldType" />
									            </EditItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="Used For" SortExpression="M.UsedFor">
									            <ItemStyle Width="15%" />
									            <ItemTemplate>
										            <%# objPM.mtdGetMachineCriteriaFor(Val(Container.DataItem("UsedFor"))) %>
									            </ItemTemplate>
									            <EditItemTemplate>
										            <asp:DropDownList id="ddlEdtUsedFor" AutoPostback="False" runat="server" />
										            <asp:Label id=lblEdtUsedFor visible=false text='<%# Container.DataItem("UsedFor") %>' runat=server/>
										            <asp:RequiredFieldValidator id="rfvUsedFor" display="Dynamic" runat="server" ErrorMessage="Please enter Criteria Used For."
											            ControlToValidate="ddlEdtUsedFor" />
									            </EditItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="Last Update" SortExpression="M.UpdateDate">
									            <ItemStyle Width="10%" />
									            <ItemTemplate>
										            <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									            </ItemTemplate>
									            <EditItemTemplate>
										            <asp:TextBox id="txtUpdateDate" Readonly= TRUE MaxLength=8 Visible=False Text= '<%# objGlobal.GetLongDate(Now()) %>' runat="server"/>
										            <asp:TextBox id="txtCreateDate" Visible=False Text= '<%# Container.DataItem("UpdateDate") %>' runat="server"/>
									            </EditItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="Status" SortExpression="M.Status">
									            <ItemStyle Width="10%" />
									            <ItemTemplate>
										            <%# objPM.mtdGetMachineCriteriaStatus(Container.DataItem("Status")) %>
									            </ItemTemplate>
									            <EditItemTemplate>
										            <asp:DropDownList Visible="False" id="StatusList" runat="server" />
										            <asp:TextBox id="txtEdtStatus" Readonly=TRUE Visible = False Text= '<%# Container.DataItem("Status")%>' runat="server"/>
									            </EditItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="Updated By" SortExpression="Usr.UserName">
									            <ItemStyle Width="15%" />
									            <ItemTemplate>
										            <%# Container.DataItem("UserName") %>
									            </ItemTemplate>
									            <EditItemTemplate>
										            <asp:TextBox id="txtEdtUserName" Readonly=TRUE MaxLength=8 Text= '<%# Session("SS_USERID") %>' Visible=False runat="server"/>
										            <asp:Label id=lblCriteriaID visible=false text='<%# Container.DataItem("CriteriaID") %>' runat=server/>
									            </EditItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn ItemStyle-HorizontalAlign="Center">
									            <ItemTemplate>
										            <asp:LinkButton id="Edit" CommandName="Edit" Text="Edit" runat="server" />
									            </ItemTemplate>
									            <EditItemTemplate>
										            <asp:LinkButton id="Update" CommandName="Update" Text="Save" runat="server" />
										            <asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" runat="server" />
										            <asp:LinkButton id="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation="False" runat="server" />
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
					            <asp:ImageButton id="ibNew" imageurl="../../images/butt_new.gif" OnClick="DEDR_Add" AlternateText="New Machine Code" runat="server" />
							</td>
						</tr>
                        <tr>
                            <td>
 					            &nbsp;</td>
                        </tr>
                        <tr>
					        <td align="left" width="80%" ColSpan="6">
						        <asp:Label id=lblMsg visible=false text="Criteria Already Exist" forecolor=red runat=server/>
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


		</form>
	</body>
</HTML>
