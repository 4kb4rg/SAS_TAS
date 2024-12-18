<%@ Page Language="vb" src="../../../include/HR_trx_EmployeeList.aspx.vb" Inherits="HR_EmployeeList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHR" src="../../menu/menu_hrtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<html>
	<head>
		<title>Employee List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />

<%--		<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	</head>
	<body onload="javascript:document.frmEmpList.txtEmpCode.focus();">
	    <form id=frmEmpList runat=server  class="main-modul-bg-app-list-pu">
			<asp:Label id=SortExpression visible=false runat=server />
			<asp:Label id=SortCol visible=false runat=server />
			<asp:Label id=lblErrMessage visible=false text="Error while initiating component." runat=server />


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
							<td><strong>EMPLOYEE LIST</strong><hr style="width :100%" />   
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
								<td width="15%" height="26">Employee Code :<BR><asp:TextBox id=txtEmpCode width=100% maxlength="20" runat="server" /></td>
								<td width="20%" height="26">Name :<BR><asp:TextBox id=txtEmpName width=100% maxlength="128" runat="server"/></td>
								<td wdith="20%" height="26">Gang Code :<BR><asp:TextBox id=txtGangCode width=100% maxlength="8" runat="server" /><td>
								<td width="15%" height="26">Status :<BR>
									<asp:DropDownList id="ddlStatus" width=100% runat=server>
										<asp:ListItem value="0" >All</asp:ListItem>
										<asp:ListItem value="1" Selected>Active</asp:ListItem>
										<asp:ListItem value="2">Deleted</asp:ListItem>
										<asp:ListItem value="3">Pending</asp:ListItem>
									</asp:DropDownList>
								</td>
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
						            <asp:DataGrid id=dgEmpList
							                AutoGenerateColumns=false width=100% runat=server
							                GridLines=none 
							                Cellpadding=2 
							                AllowPaging=True 
							                Allowcustompaging=False 
							                Pagesize=15 
							                OnPageIndexChanged=OnPageChanged 
							                Pagerstyle-Visible=False 
							                OnDeleteCommand=DEDR_Delete 
							                OnSortCommand=Sort_Grid  
							                OnItemCommand=EmpLink_Click 
							                AllowSorting=True
                                         
                                                        class="font9Tahoma">
								
							                            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
							
							                <Columns>
								
								                <asp:TemplateColumn HeaderText="Employee Code" SortExpression="EmpCode">
									                <ItemTemplate>
										                <asp:Label id=lblEmpCode text='<%# Container.DataItem("EmpCode") %>' visible=false runat=server/>
										                <asp:LinkButton id=lbEmpCode CommandName=Item text='<%# Container.DataItem("EmpCode") %>' runat=server />
									                </ItemTemplate>
								                </asp:TemplateColumn>
							
								                <asp:TemplateColumn HeaderText="Name" SortExpression="EmpName">
									                <ItemTemplate>
										                <asp:Label id=lblEmpName text='<%# Container.DataItem("EmpName") %>' visible=false runat=server/>
										                <asp:LinkButton id=lbEmpName CommandName=Item text='<%# Container.DataItem("EmpName") %>' runat=server />
									                </ItemTemplate>
								                </asp:TemplateColumn>
								
								                <asp:TemplateColumn HeaderText="Last Update" SortExpression="UpdateDate">
									                <ItemTemplate>
										                <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									                </ItemTemplate>
								                </asp:TemplateColumn>
								
								                <asp:TemplateColumn HeaderText="Status" SortExpression="Status">
									                <ItemTemplate>
										                <asp:label id=lblEmpStatus text=<%# Container.DataItem("Status") %> visible=false runat=server />
										                <%# objHR.mtdGetEmpStatus(Container.DataItem("Status")) %>
									                </ItemTemplate>
								                </asp:TemplateColumn>
								
								                <asp:TemplateColumn HeaderText="Updated By" SortExpression="UserName">
									                <ItemTemplate>
										                <%# Container.DataItem("UserName") %>
									                </ItemTemplate>
								                </asp:TemplateColumn>
								                <asp:TemplateColumn ItemStyle-HorizontalAlign=center>
									                <ItemTemplate>
										                <asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server />
										                <asp:label id=lblEmpMarital text=<%# Container.DataItem("MaritalStatus") %> visible=false runat=server />
									                </ItemTemplate>
								                </asp:TemplateColumn>																
								                <asp:TemplateColumn ItemStyle-HorizontalAlign=center>									
									                <ItemTemplate>
										                <comment>Modified By BHL</comment>										
										                <asp:label id=lblEmpGender text=<%# Container.DataItem("Gender") %> visible=false runat=server />
										                <comment>End Modified</comment>
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
                                    <asp:Label id=lblCurrentIndex visible=false text=0 runat=server/>
						            <asp:Label id=lblPageCount visible=false text=1 runat=server/>
								</tr>
							</table>
							</td>
						</tr>
						<tr>
							<td>
					            <asp:ImageButton id=NewEmpBtn OnClick="NewEmpBtn_Click" imageurl="../../images/butt_new.gif" AlternateText="New Employee" runat="server"/>
						        <asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print visible=false runat=server/>
						        <asp:Label id=lblRedirect visible=false runat=server/>
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


		</form>
	</body>
</html>
