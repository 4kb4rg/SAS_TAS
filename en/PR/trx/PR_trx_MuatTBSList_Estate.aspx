<%@ Page Language="vb" src="../../../include/PR_trx_MuatTBSList_Estate.aspx.vb" Inherits="PR_MuatTBSList"%> 
<%@ Register TagPrefix="UserControl" Tagname="MenuPRTrx" src="../../menu/menu_prtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<html>
	<head>
		<title>BUKU TRANSPORT MUAT TBS</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
<%--		<Preference:PrefHdl id=PrefHdl runat="server" />--%>
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
							<td><strong>LIST BUKU TRANSPORT MUAT TBS</strong><hr style="width :100%" />   
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
								<td height="26" width="20%">
								Employee Code :<BR><asp:TextBox id=txtEmpCode width=100% maxlength="20" runat="server" /></td>
								<td height="26" width="20%">Name :<BR><asp:TextBox id=txtEmpName width=100% maxlength="128" runat="server"/></td>
                                <td height="26" width="15%">Division :<BR><asp:DropDownList id="ddlEmpDiv" width=100% runat="server" /></td>
                                <td height="26" width="15%">Block :<BR><asp:DropDownList id="ddlEmpBlock" width=100% runat=server /></td>
                                <td height="26" width="15%">
                                    Month<br />
                                    <asp:DropDownList ID="ddlMonth" runat="server" Width="100%">
                                        <asp:ListItem Value="1">January</asp:ListItem>
                                        <asp:ListItem Value="2">February</asp:ListItem>
                                        <asp:ListItem Value="3">March</asp:ListItem>
                                        <asp:ListItem Value="4">April</asp:ListItem>
                                        <asp:ListItem Value="5">May</asp:ListItem>
                                        <asp:ListItem Value="6">June</asp:ListItem>
                                        <asp:ListItem Value="7">July</asp:ListItem>
                                        <asp:ListItem Value="8">August</asp:ListItem>
                                        <asp:ListItem Value="9">September</asp:ListItem>
                                        <asp:ListItem Value="10">October</asp:ListItem>
                                        <asp:ListItem Value="11">November</asp:ListItem>
                                        <asp:ListItem Value="12">December</asp:ListItem>
                                    </asp:DropDownList></td>
								<td height="26" width="10%" >
                                    Year<br />
                                    <asp:TextBox ID="ddlYear" runat="server" Width="100%" />
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
							            OnDeleteCommand=DEDR_Delete 
						                OnSortCommand=Sort_Grid  
						                OnItemDataBound=KeepRunningSum
							            AllowSorting=True 
							            ShowFooter=True
                                                    class="font9Tahoma">
								
							                        <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                        <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                        <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
							            <Columns>
							            <asp:BoundColumn Visible=False HeaderText="MTBS ID" DataField="MtbID" />
								            <asp:HyperLinkColumn HeaderText="MTBS ID" 
													             SortExpression="A.MtbID" 
													             DataNavigateUrlField="MtbID" 
													             DataNavigateUrlFormatString="PR_trx_MuatTBSDet_Estate.aspx?MtbID={0}" 
													             DataTextField="MtbID" />
													 
							                    								
								            <asp:TemplateColumn HeaderText="Employee Code" SortExpression="A.EmpCode">
									            <ItemTemplate>
										            <asp:Label id=lblEmpCode text='<%# Container.DataItem("EmpCode") %>'  runat=server/>
										            <%--<asp:LinkButton id=lbEmpCode CommandName=Item text='<%# Container.DataItem("EmpCode") %>' runat=server />--%>
									            </ItemTemplate>
								            </asp:TemplateColumn>
							
								            <asp:TemplateColumn HeaderText="Name" SortExpression="B.EmpName">
									            <ItemTemplate>
										            <asp:Label id=lblEmpName text='<%# Container.DataItem("EmpName") %>' runat=server/>
										            <%--<asp:LinkButton id=lbEmpName CommandName=Item text='<%# Container.DataItem("EmpName") %>' runat=server />--%>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								
								            <asp:TemplateColumn HeaderText="Block" SortExpression="Blok" >
									            <ItemTemplate>
										            <asp:Label id=lblBlok text='<%# Container.DataItem("Blok") %>' runat=server/>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								
								            <asp:TemplateColumn HeaderText="Tot.HK" SortExpression="TotalHK" HeaderStyle-HorizontalAlign="Right">
									            <ItemTemplate>
										            <asp:Label id=lblHK text='<%# Container.DataItem("TotalHK") %>' runat=server/>
									            </ItemTemplate>
									            <ItemStyle HorizontalAlign="Right" />
								            </asp:TemplateColumn>
								
								            <asp:TemplateColumn HeaderText="Tot.Kg" SortExpression="TotalKg" HeaderStyle-HorizontalAlign="Right">
									            <ItemTemplate>
										            <asp:Label id=lblJJG text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("TotalKg"),2),0) %>' runat=server/>
									            </ItemTemplate>
									            <ItemStyle HorizontalAlign="Right" />
								            </asp:TemplateColumn>
								
								            <asp:TemplateColumn HeaderText="Denda" SortExpression="Denda" HeaderStyle-HorizontalAlign="Right">
									            <ItemTemplate>
										            <asp:Label id=lblDenda text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Denda"),2),0) %>' runat=server/>
									            </ItemTemplate>
									            <ItemStyle HorizontalAlign="Right" />
								            </asp:TemplateColumn>
								
								            <asp:TemplateColumn HeaderText="Total" SortExpression="Total" HeaderStyle-HorizontalAlign="Right">
									            <ItemTemplate >
										            <asp:Label id=lblTotal text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Total"),2),0) %>' runat=server/>
									            </ItemTemplate>
									            <FooterTemplate >
									                <asp:Label ID=lbTotal runat=server />
									             </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                 <FooterStyle BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />
								            </asp:TemplateColumn>
								
                             	            <asp:TemplateColumn HeaderText="Last Update" SortExpression="UpdateDate" HeaderStyle-HorizontalAlign="Center">
									            <ItemTemplate >
										            <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									            </ItemTemplate>
									            <ItemStyle HorizontalAlign="Center" />
								            </asp:TemplateColumn>
								
								            <asp:TemplateColumn HeaderText="Updated By" SortExpression="UserName">
									            <ItemTemplate>
										            <%# Container.DataItem("UserName") %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								
								            <asp:TemplateColumn>
									            <ItemTemplate>
										            <asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server />
									            </ItemTemplate>
                                            </asp:TemplateColumn>				
													
							            </Columns>
                                        <PagerStyle Visible="False" />
                                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" />
                                        <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" />
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
						        <%--<asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print visible=false/>--%>
						        <asp:Label id=lblRedirect visible=false runat=server/>
						        <!--a href="#" onclick="javascript:popwin(400, 600, 'PU_trx_PrintDocs.aspx?doctype=1&TrxID=')"><asp:Image id="ibPrintDoc" runat="server" ImageUrl="../../images/butt_print_doc.gif"/></a-->
							</td>
						</tr>
                        <tr>
                            <td>
 					            <asp:Button ID="NewEmpBtn2" class="button-small" runat="server" Text="New Employee"  />&nbsp;
                                                      
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
