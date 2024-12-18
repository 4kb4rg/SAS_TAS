<%@ Page Language="vb" src="../../../include/HR_trx_EmployeeCPList.aspx.vb" Inherits="HR_EmployeeCPList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHR" src="../../menu/menu_hrtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<html>
	<head>
		<title>Employee Career Progress List</title>
         <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
	    <form id=frmEmpCPList runat=server class="main-modul-bg-app-list-pu">
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
							<td><strong><asp:label id="lblTitle" runat="server"/> LIST</strong><hr style="width :100%" />   
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
								    <td width=42% height=26>Employee Code : <asp:Label id=lblEmpCode width=50% runat="server" /></td>
								    <!--<td width=25%>&nbsp;</td>-->
								    <td width=6%>&nbsp;</td>
								    <td width=42%>Name : <asp:Label id=lblEmpName width=50% runat="server"/></td>
								    <!--<td width=25%>&nbsp;</td>-->
                                </tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
						            <<asp:DataGrid id=dgEmpCPList
							            AutoGenerateColumns=false width=100% runat=server
							            GridLines=none 
							            Cellpadding=2 
							            AllowPaging=True 
							            Allowcustompaging=False 
							            Pagesize=15 
							            OnPageIndexChanged=OnPageChanged 
							            Pagerstyle-Visible=False 
							            OnSortCommand=Sort_Grid
							            OnEditCommand=DEDR_CPDet
							            AllowSorting=True
                                                                    class="font9Tahoma">
								
							                                        <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                                        <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                                        <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
							
							            <Columns>
								            <asp:TemplateColumn HeaderText="Transaction ID" SortExpression="CPID">
									            <ItemTemplate>
										            <asp:Label id=lblCPID text='<%# Container.DataItem("CPID") %>' visible=false runat=server/>
										            <asp:LinkButton id=lbCPID text='<%# Container.DataItem("CPID") %>' CommandName=Edit runat=server />
									            </ItemTemplate>
								            </asp:TemplateColumn>

								            <asp:TemplateColumn SortExpression="CPCode">
									            <ItemTemplate>
										            <%# Container.DataItem("CPDesc") %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
																
								            <asp:TemplateColumn HeaderText="Category" SortExpression="Category">									
									            <ItemTemplate>
										            <comment>Modified By BHL</comment>
										            <%# Container.DataItem("SalSchemeCode") %>
										            <comment>End Modified</comment>
									            </ItemTemplate>									
								            </asp:TemplateColumn>	
															
								            <asp:TemplateColumn HeaderText="Location" SortExpression="Location" ItemStyle-HorizontalAlign=Center HeaderStyle-HorizontalAlign=Center>
									            <ItemTemplate>
										            <%# Container.DataItem("LocCode") %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								
								            <asp:TemplateColumn HeaderText="Evaluation" SortExpression="EvalCode" ItemStyle-HorizontalAlign=Center HeaderStyle-HorizontalAlign=Center>
									            <ItemTemplate>
										            <%# Container.DataItem("EvalCode") %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								
								            <asp:TemplateColumn HeaderText="Remark" SortExpression="Remark">
									            <ItemTemplate>
										            <%# Container.DataItem("Remark") %>
									            </ItemTemplate>
								            </asp:TemplateColumn>

								            <asp:TemplateColumn>
									            <ItemTemplate>
										            <asp:Label id=lblEmpCode text='<%# Container.DataItem("EmpCode") %>' visible=false runat=server/>
										            <asp:LinkButton id=lbEmpCode text='<%# Container.DataItem("EmpCode") %>' visible=false runat=server />
									            </ItemTemplate>
								            </asp:TemplateColumn>

								            <asp:TemplateColumn>
									            <ItemTemplate>
										            <asp:Label id=lblEmpName text='<%# Container.DataItem("EmpName") %>' visible=false runat=server/>
										            <asp:LinkButton id=lbEmpName text='<%# Container.DataItem("EmpName") %>' visible=false runat=server />
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
					            <asp:ImageButton id=NewEmpCPBtn imageurl="../../images/butt_new.gif" onClick=NewEmpCPBtn_Click AlternateText="New Career Progress" runat="server"/>
						        <asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print visible=false runat="server"/>
						        <asp:ImageButton id=btnBack imageurl="../../images/butt_back.gif" CausesValidation=False AlternateText="Back" onclick=btnBack_Click runat=server />
							</td>
						</tr>
                        <tr>
                            <td>
 					            &nbsp;
                                &nbsp;
                          
                            </td>
                        </tr>
                        <tr id=TrLink runat=server>
					        <td colspan=5>
						        <asp:LinkButton id=lbDetails text="Employee Details" causesvalidation=false runat=server /> |
						        <asp:LinkButton id=lbPayroll text="Employee Payroll" causesvalidation=false runat=server /> |
						        <asp:LinkButton id=lbEmployment text="Employee Employment" causesvalidation=false runat=server /> |
						        <asp:LinkButton id=lbStatutory text="Employee Statutory" causesvalidation=false runat=server /> |
						        <asp:LinkButton id=lbFamily text="Employee Family" causesvalidation=false runat=server /> |
						        <asp:LinkButton id=lbQualific text="Employee Qualification" causesvalidation=false runat=server /> |
						        <asp:LinkButton id=lbSkill text="Employee Skill" causesvalidation=false runat=server />
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



			<asp:Label id=lblRedirect visible=false runat=server/>
			<asp:label id=lblEmpStatus visible=false runat=server />
		</form>
	</body>
</html>
