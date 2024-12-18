<%@ Page Language="vb" src="../../../include/PR_trx_MandorPremiList_Estate.aspx.vb" Inherits="PR_MandorPremiList"%> 
<%@ Register TagPrefix="UserControl" Tagname="MenuPRTrx" src="../../menu/menu_prtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<html>
	<head>
		<title>Premi Mandor List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<%--<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	</head>
	<body onload="javascript:document.frmMain.txtEmpCode.focus();">
	    <form id=frmMain runat=server class="main-modul-bg-app-list-pu">
			<asp:Label id=SortExpression visible=false runat=server />
			<asp:Label id=SortCol visible=false runat=server />
			<asp:Label id=lblErrMessage visible=false text="Error while initiating component." runat=server />


		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuPRTrx id=MenuPRTrx runat=server />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>LIST PREMI MANDOR</strong><hr style="width :100%" />   
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
								<td height="26" width="20%">Employee Code :<BR><asp:TextBox id=txtEmpCode width=100% maxlength="20" runat="server" /></td>
								<td height="26" width="20%">Name :<BR><asp:TextBox id=txtEmpName width=100% maxlength="128" runat="server"/></td>
                                <td height="26" width="15%">Division :<BR><asp:DropDownList id="ddlEmpDiv" width=100% runat=server /></td>
                                <td height="26" width="18%"></td>
                                <td height="26" width="15%">Month<BR />
                                    <asp:DropDownList ID="ddlEmpMonth" runat="server" Width="100%">
                                        <asp:ListItem Value="">Select Month</asp:ListItem>
                                        <asp:ListItem Value="01">January</asp:ListItem>
                                        <asp:ListItem Value="02">February</asp:ListItem>
                                        <asp:ListItem Value="03">March</asp:ListItem>
                                        <asp:ListItem Value="04">April</asp:ListItem>
                                        <asp:ListItem Value="05">May</asp:ListItem>
                                        <asp:ListItem Value="06">June</asp:ListItem>
                                        <asp:ListItem Value="07">July</asp:ListItem>
                                        <asp:ListItem Value="08">August</asp:ListItem>
                                        <asp:ListItem Value="09">September</asp:ListItem>
                                        <asp:ListItem Value="10">October</asp:ListItem>
                                        <asp:ListItem Value="11">November</asp:ListItem>
                                        <asp:ListItem Value="12">December</asp:ListItem>
                                    </asp:DropDownList></td>
								<td height="26" width="20%" >Year<BR /><asp:TextBox ID="ddlEmpYear" runat="server" Width="100%"></asp:TextBox></td>
								<td height="26" width="10%"  valign=bottom align=right><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/>
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
						            <asp:DataGrid id=dgEmpList
							            AutoGenerateColumns=False width=100% runat=server
							            GridLines=None 
							            Cellpadding=2 
							            AllowPaging=True 
							            Pagesize=15 
							            OnPageIndexChanged=OnPageChanged 
							            Pagerstyle-Visible=False 
							            OnItemDataBound=KeepRunningSum
						                OnSortCommand=Sort_grid
						                ShowFooter=True
						    
							            AllowSorting=True
                                                    class="font9Tahoma">
								
							                        <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                        <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                        <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
							
							            <Columns>
					
		                		            <asp:TemplateColumn HeaderText="ID" SortExpression="A.PrmMdrID">
									            <ItemTemplate>
										            <asp:Label id=lblid text='<%# Container.DataItem("PrmMdrID") %>'  runat=server/>
									            </ItemTemplate>
								            </asp:TemplateColumn>	 
								
								            <asp:TemplateColumn HeaderText="Employee Code" SortExpression="A.EmpCode">
									            <ItemTemplate>
										            <asp:Label id=lblEmpCode text='<%# Container.DataItem("EmpCode") %>'  runat=server/>
									            </ItemTemplate>
								            </asp:TemplateColumn>
							
								            <asp:TemplateColumn HeaderText="Name" SortExpression="B.EmpName">
									            <ItemTemplate>
										            <asp:Label id=lblEmpName text='<%# Container.DataItem("EmpName") %>'  runat=server/>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								
								              <asp:TemplateColumn HeaderText="Division" SortExpression="B.Description">
                                	            <ItemTemplate>
										            <asp:Label id=lblEmpDiv text='<%# Container.DataItem("Description") %>'  runat=server/>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								
                                                           
                                            <asp:TemplateColumn HeaderText="Total Premi" SortExpression="TotalPremi" HeaderStyle-HorizontalAlign="Right">
                                	            <ItemTemplate>
										            <asp:Label id=lblTotalAmount text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("TotalPremi"),2),0) %>'  runat=server/>
									            </ItemTemplate>
									            <FooterTemplate >
									                <asp:Label ID=lbTotal runat=server />
									             </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />
								            </asp:TemplateColumn>
								
								            <asp:TemplateColumn HeaderText="Last Update" HeaderStyle-HorizontalAlign="Center" SortExpression="UpdateDate">
									            <ItemTemplate>
										            <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									            </ItemTemplate>
									            <ItemStyle HorizontalAlign="Center" />
								            </asp:TemplateColumn>
								
								            <asp:TemplateColumn HeaderText="Updated By" HeaderStyle-HorizontalAlign="Center" SortExpression="UserName">
									            <ItemTemplate>
										            <%# Container.DataItem("UserName") %>
									            </ItemTemplate>
								            <ItemStyle HorizontalAlign="Center" />
								            </asp:TemplateColumn>
							            </Columns>
                                        <PagerStyle Visible="False" />
						            </asp:DataGrid></td>
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
