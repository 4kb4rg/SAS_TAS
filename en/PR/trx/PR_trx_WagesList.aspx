<%@ Page Language="vb" src="../../../include/PR_trx_WagesList.aspx.vb" Inherits="PR_trx_WagesList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRTrx" src="../../menu/menu_prtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Wages Payment List</title>
         <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
<%--		<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	</head>
	<body>
		<form id=frmMain runat="server" class="main-modul-bg-app-list-pu">
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:Label id=SortCol Visible=False Runat="server" />			


		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuPRTrx id=MenuPRTrx runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>WAGES PAYMENT LIST</strong><hr style="width :100%" />   
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
								<td width="20%">Employee Code :<BR><asp:TextBox id=txtEmpCode width=100% maxlength="20" runat="server" /></td>
								<td width="25%">Name :<BR><asp:TextBox id=txtName width=100% maxlength="64" runat="server" /></td>
								<td width="15%">Cheque No :<BR><asp:TextBox id=txtChequeNo width=100% maxlength="10" runat="server" /></td>
								<td width="15%">Wages Period :<BR><asp:TextBox id=txtWagesPeriod width=100% maxlength="7" runat="server" /></td>
								<td width="15%">Status :<BR><asp:DropDownList id="ddlStatus" width=100% runat=server/></td>
								<td width="10%"valign=bottom align=right><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
								</tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
						            <asp:DataGrid id=dgLine runat=server
							            AutoGenerateColumns=false width=100% 
							            GridLines=none 
							            Cellpadding=2 
							            AllowPaging=True 
							            Allowcustompaging=False 
							            Pagesize=15 
							            OnPageIndexChanged=OnPageChanged 
							            Pagerstyle-Visible=False 
							            OnItemCommand=Link_Click 
							            OnSortCommand=Sort_Grid  
							            AllowSorting=True
                                                    class="font9Tahoma">
								
							                        <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                        <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                        <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
							            <Columns>									
								            <asp:TemplateColumn HeaderText="Employee Code" ItemStyle-Width="20%" SortExpression="E.EmpCode">
									            <ItemTemplate>
										            <asp:LinkButton id=lbCode CommandName=Item text='<%# Container.DataItem("EmpCode") %>' runat=server />
									            </ItemTemplate>
								            </asp:TemplateColumn>
								
								            <asp:TemplateColumn HeaderText="Name" ItemStyle-Width="40%" SortExpression="E.EmpName">
									            <ItemTemplate>
										            <asp:LinkButton id=lbName CommandName=Item text='<%# Container.DataItem("EmpName") %>' runat=server />
									            </ItemTemplate>
								            </asp:TemplateColumn>

								            <asp:TemplateColumn HeaderText="Cheque No" ItemStyle-Width="20%" SortExpression="E.ChequeNo">
									            <ItemTemplate>
										            <%# Container.DataItem("ChequeNo") %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
									
								            <asp:TemplateColumn HeaderText="Wages Period" ItemStyle-Width="20%" SortExpression="E.AccPeriod">
									            <ItemTemplate>
										            <%# Container.DataItem("AccPeriod") %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
									
								            <asp:TemplateColumn HeaderText="Status" ItemStyle-Width="15%" SortExpression="E.Status">
									            <ItemTemplate>
										            <asp:Label id=lblWagesID text='<%# Container.DataItem("WagesID") %>' visible=false runat=server/>
										            <asp:Label id=lblAccMonth text='<%# Container.DataItem("AccMonth") %>' visible=false runat=server/>
										            <asp:Label id=lblAccYear text='<%# Container.DataItem("AccYear") %>' visible=false runat=server/>
										            <asp:Label id=lblCompCode text='<%# Container.DataItem("CompCode") %>' visible=false runat=server/>
										            <asp:Label id=lblLocCode text='<%# Container.DataItem("LocCode") %>' visible=false runat=server/>
										            <asp:Label id=lblEmpCode text='<%# Container.DataItem("EmpCode") %>' visible=false runat=server/>
										            <%# objPRTrx.mtdGetWagesStatus(Container.DataItem("Status")) %>
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
					            <asp:ImageButton id=btnPaidAll AlternateText="  Paid All " imageurl="../../images/butt_paid.gif" onclick=btnPaidAll_Click Visible =False runat=server />
							</td>
						</tr>
                        <tr>
                            <td>
 					            &nbsp;</td>
                        </tr>
                        <tr>
					        <td colspan=6>
							        <asp:Label id=lblErrProcess Visible=False forecolor=red Runat="server" />
					        </td>
				        </tr>
				        <tr>
					        <td colspan=6>
							        <asp:Label id=lblSelection Visible=False Runat="server" />
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
