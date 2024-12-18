<%@ Page Language="vb" trace=false src="../../../include/HR_setup_StaffList.aspx.vb" Inherits="HR_setup_StaffList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHR" src="../../menu/menu_hrsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<html>
	<head>
		<title>Human Resource - Staff List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
<%--		<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	</head>
	<body>
	    <form id=frmTransporterList runat=server  class="main-modul-bg-app-list-pu">
			<asp:Label id=lblErrMessage visible=false text="Error while initiating component." runat=server />
			<asp:Label id=SortExpression visible=false runat=server />
			<asp:Label id=SortCol visible=false runat=server />

		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuHR id=MenuHR runat=server />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>STAFF LIST</strong><hr style="width :100%" />   
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
								<td width="35%" height="26">Name :<BR><asp:TextBox id=srchName width=100% maxlength="128" runat="server"/></td>
								<td valign=bottom width=10%>Type :<BR>
								    <asp:DropDownList width=100% id=ddlType runat=server>
								        <asp:ListItem value="0" Selected>All</asp:ListItem>
						                <asp:ListItem value="1">Internal</asp:ListItem>
						                <asp:ListItem value="1">External</asp:ListItem>
					                </asp:DropDownList></td>
								<td width="15%" height="26">Status :<BR><asp:DropDownList id="srchStatusList" width=100% runat=server /></td>
								<td width="20%" height="26">Last Updated By :<BR><asp:TextBox id=srchUpdateBy width=100% maxlength="128" runat="server"/></td>
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
                                    <div id="divgd" style="width:99%;height:350px;overflow: auto;">  
						            <asp:DataGrid id=dgList
							                AutoGenerateColumns=false width=100% runat=server
							                GridLines=none 
							                Cellpadding=2 
							                Pagerstyle-Visible=False 
							                OnDeleteCommand=DEDR_Delete 
							                OnSortCommand=Sort_Grid  
							                AllowSorting=True
                                                        class="font9Tahoma">
								
							                            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
							
							                <Columns>
							                    <asp:HyperLinkColumn HeaderText="Staff ID" 
									                SortExpression="StaffID" 
									                DataNavigateUrlField="StaffID" 
									                DataNavigateUrlFormatString="HR_setup_StaffDet.aspx?staffid={0}" 
									                DataTextField="StaffID" />
							    
								                <asp:HyperLinkColumn HeaderText="Name" 
									                SortExpression="Name" 
									                DataNavigateUrlField="StaffID" 
									                DataNavigateUrlFormatString="HR_setup_StaffDet.aspx?staffid={0}" 
									                DataTextField="Name" />
							    
							                    <asp:TemplateColumn HeaderText="NIK" SortExpression="NIK">
									                <ItemTemplate>
										                <%# Container.DataItem("NIK") %>
									                </ItemTemplate>
								                </asp:TemplateColumn>
								
								                <asp:TemplateColumn HeaderText="Bank" SortExpression="BankCode">
									                <ItemTemplate>
										                <%# Container.DataItem("BankAccDescr") %>
									                </ItemTemplate>
								                </asp:TemplateColumn>
								
								                <asp:TemplateColumn HeaderText="Last Update" SortExpression="UpdateDate">
									                <ItemTemplate>
										                <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									                </ItemTemplate>
								                </asp:TemplateColumn>
								
								                <asp:TemplateColumn HeaderText="Status" SortExpression="TRA.Status">
									                <ItemTemplate>
										                <asp:Label id=lblStatus text='<%# objWM.mtdGetTransporterStatus(Container.DataItem("Status")) %>' runat=server/>		
									                </ItemTemplate>
								                </asp:TemplateColumn>
								
								                <asp:TemplateColumn HeaderText="Updated By" SortExpression="USR.UserName">
									                <ItemTemplate>
										                <%# Container.DataItem("UserName") %>
									                </ItemTemplate>
								                </asp:TemplateColumn>
								                <asp:TemplateColumn ItemStyle-HorizontalAlign=Center>
									                <ItemTemplate>
										                <asp:Label id=lblStaffID visible=false text='<%# Container.DataItem("StaffID") %>' runat=server/>									
										                <asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" runat="server"/>
									                </ItemTemplate>
								                </asp:TemplateColumn>
							                </Columns>
						                </asp:DataGrid><BR>
                                    </div>
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
					            <asp:ImageButton id=NewBtn onClick=NewTransBtn_Click imageurl="../../images/butt_new.gif" AlternateText="New Staff" runat=server/>
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
