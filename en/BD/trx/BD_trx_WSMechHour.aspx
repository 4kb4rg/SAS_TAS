<%@ Page Language="vb" src="../../../include/BD_trx_WSMechHour.aspx.vb" Inherits="BD_trx_WSMechHour" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuBDTrx" src="../../menu/menu_BDTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Workshop Mechanic Hour List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<%--<Preference:PrefHdl id="PrefHdl" runat="server" />--%>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu">
		<asp:label id="SortExpression" Visible="False" Runat="server"></asp:label>
		<input type="hidden" id="hidIsUpdate" runat="server">
		<input type="hidden" id="hidMechHourOriValue" runat="server">
		<input type="hidden" id="hidAddVoteOriValue" runat="server">
		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
		<asp:Label id=lblCode visible=false text=" Code" runat=server />
		<asp:label id="sortcol" Visible="False" Runat="server"></asp:label>
		<asp:Label id="lblBgtStatus" visible="false" runat="server" />
		<asp:Label id="lblPeriodID" visible="false" runat="server" />

		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuBDTrx id="menuBD" runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong><asp:label id="lblTitle" runat="server" /></strong><hr style="width :100%" />   
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
								<td width="45%" height="26" valign="bottom">
								    Employee Code :<BR>
								    <asp:TextBox id="txtSrchEmpCode" width="100%" maxlength="8" runat="server"/>
							    </td>
							    <td width="45%" height="26" valign="bottom"> 
								    Work Code :<BR>
								    <asp:TextBox id="txtSrchWorkCode" width="100%" maxlength="9" runat="server"/>
							    </td>
							    <td width="10%" height="26" valign="bottom" align="right">
								    <asp:Button id=srchUpdateBy Text="Search" OnClick="srchBtn_Click" runat="server" class="button-small"/>
							    </td>
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
						                <asp:TemplateColumn HeaderText="Employee Code" SortExpression="BD.EmpCode">
							                <ItemStyle Width="15%" />						
							                <ItemTemplate>					
								                <asp:label id="lblEmpCode" Text='<%# Container.DataItem("EmpCode") %>' Runat="server"/>
							                </ItemTemplate>
							                <EditItemTemplate>
								                <asp:label id="lblEmpCode" Text='<%# Container.DataItem("EmpCode") %>' visible="false" Runat="server"/>
								                <asp:DropDownList id="lstEmpCode" width="100%" runat="server"/>
								                <asp:TextBox id="txtEmpCode" width="100%" runat="server" Text='<%# Trim(Container.DataItem("EmpCode")) %>'></asp:TextBox>
							                </EditItemTemplate>
						                </asp:TemplateColumn>					

						                <asp:TemplateColumn HeaderText="Work Code" SortExpression="BD.WorkCode">
							                <ItemStyle Width="15%" />						
							                <ItemTemplate>					
								                <asp:label id="lblWorkCode" Text='<%# Container.DataItem("WorkCode") %>' Runat="server"/>
							                </ItemTemplate>
							                <EditItemTemplate>
								                <asp:label id="lblWorkCode" Text='<%# Container.DataItem("WorkCode") %>' visible="false" Runat="server"/>
								                <asp:DropDownList id="lstWorkCode" width="100%" runat="server"/>
								                <asp:TextBox id="txtWorkCode" width="100%" runat="server" Text='<%# Trim(Container.DataItem("WorkCode")) %>'></asp:TextBox>
							                </EditItemTemplate>
						                </asp:TemplateColumn>

						                <asp:TemplateColumn HeaderText="Mechanic Hour" SortExpression="BD.MechHour">
							                <ItemStyle Width="10%" />
							                <ItemTemplate>
								                <%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("MechHour"), 0, True, False, False)) %>
							                </ItemTemplate>
							                <EditItemTemplate>
								                <asp:TextBox id="txtMechHour" width="100%" MaxLength="10" Text='<%# FormatNumber(Container.DataItem("MechHour"),0, True, False, False) %>' runat="server"/>								
							                </EditItemTemplate>
						                </asp:TemplateColumn>

						                <asp:TemplateColumn HeaderText="Add Vote" SortExpression="BD.AddVote">
							                <ItemStyle Width="10%" />
							                <ItemTemplate>
								                <%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("AddVote"), 0, True, False, False)) %>
							                </ItemTemplate>
							                <EditItemTemplate>
								                <asp:TextBox id="txtAddVote" width="100%" MaxLength="10" Text='<%# FormatNumber(Container.DataItem("AddVote"),0, True, False, False) %>' runat="server"/>								
							                </EditItemTemplate>
						                </asp:TemplateColumn>

						                <asp:TemplateColumn HeaderText="Create Date" SortExpression="BD.CreateDate">
							                <ItemStyle Width="15%" />
							                <ItemTemplate>
								                <%# Container.DataItem("CreateDate") %>
							                </ItemTemplate>
							                <ItemTemplate>
								                <%# objGlobal.GetLongDate(Container.DataItem("CreateDate")) %>
							                </ItemTemplate>
							                <EditItemTemplate >
								                <asp:TextBox id="CreateDate" Visible=False Text='<%# Container.DataItem("CreateDate") %>' runat="server"/>
							                </EditItemTemplate>
						                </asp:TemplateColumn>

						                <asp:TemplateColumn HeaderText="Last Update" SortExpression="BD.UpdateDate">
							                <ItemStyle Width="10%" />
							                <ItemTemplate>
								                <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
							                </ItemTemplate>
							                <EditItemTemplate >
								                <asp:TextBox id="UpdateDate" Readonly=TRUE  Visible=False Text='<%# objGlobal.GetLongDate(Now()) %>' runat="server"/>
							                </EditItemTemplate>
						                </asp:TemplateColumn>

						                <asp:TemplateColumn HeaderText="Updated By" SortExpression="Usr.UserID">
							                <ItemStyle Width="15%" />
							                <ItemTemplate>
								                <%# Container.DataItem("UserName") %>
							                </ItemTemplate>
							                <EditItemTemplate>
								                <asp:TextBox id="UserID" width=100% MaxLength="6" Visible=False Text='<%# trim(Container.DataItem("UserName")) %>' runat="server"/>
							                </EditItemTemplate>
						                </asp:TemplateColumn>	
				
						                <asp:TemplateColumn>					
							                <ItemStyle Width="20%" />
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
					            <asp:ImageButton id=ibNew imageurl="../../images/butt_new.gif" OnClick="DEDR_Add" AlternateText="Add New Item" runat="server"/>
					            <asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print onClick="btnPreview_Click" runat="server"/>
							</td>
						</tr>
                        <tr>
                            <td>
 					            &nbsp;
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


		</FORM>
	</body>
</html>
