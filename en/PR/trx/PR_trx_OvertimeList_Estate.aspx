<%@ Page Language="vb" src="../../../include/PR_trx_OvertimeList_Estate.aspx.vb" Inherits="PR_OvertimeList"%> 
<%@ Register TagPrefix="UserControl" Tagname="MenuPRTrx" src="../../menu/menu_prtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<html>
	<head>
		<title>Employee Overtime List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<%--<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	</head>
	<body onload="javascript:document.frmMain.txtEmpCode.focus();">
	    <form id=frmMain runat=server  class="main-modul-bg-app-list-pu">
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
							<td><strong>DAFTAR LEMBUR KARYAWAN</strong><hr style="width :100%" />   
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
								<td height="26" width="20%">NIK :<BR><asp:TextBox id=txtEmpCode width=100% maxlength="20" runat="server" /></td>
								<td height="26" width="20%">Nama :<BR><asp:TextBox id=txtEmpName width=100% maxlength="128" runat="server"/></td>
                                <td height="26" width="15%">Divisi :<BR><asp:DropDownList id="ddlEmpDiv" width=100% runat=server /></td>
                                <td height="26" width="18%">Tipe :<BR><asp:DropDownList id="ddlEmpType" width=100% runat=server/></td>
                                <td height="26" width="25%">Periode :<BR />
                                    <asp:DropDownList ID="ddlEmpMonth" runat="server" Width="55%">
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
                                    </asp:DropDownList>
								<asp:DropDownList id="ddlyear" width="40%" runat=server></asp:DropDownList></td>
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
						                    OnItemDataBound=KeepRunningSum
						                    OnSortCommand=Sort_grid
						                    ShowFooter=True
						                    AllowSorting=True
                                                        class="font9Tahoma">
								
							                            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
							                <Columns>
														
								                <asp:BoundColumn Visible=False HeaderText="No.Lembur" DataField="OtID" />
								                <asp:HyperLinkColumn HeaderText="No.Lembur" 
													                 SortExpression="A.OtID" 
													                 DataNavigateUrlField="OtID" 
													                 DataNavigateUrlFormatString="PR_trx_OvertimeDet_Estate.aspx?OtID={0}" 
													                 DataTextField="OtID" />
													 
								
								                <asp:TemplateColumn HeaderText="NIK" SortExpression="A.EmpCode">
									                <ItemTemplate>
										                <asp:Label id=lblEmpCode text='<%# Container.DataItem("EmpCode") %>'  runat=server/>
										                <asp:Label id=lblOtID text='<%# Container.DataItem("OtID") %>' visible=false runat=server/>
									                </ItemTemplate>
								                </asp:TemplateColumn>
							
								                <asp:TemplateColumn HeaderText="Nama" SortExpression="B.EmpName">
									                <ItemTemplate>
										                <asp:Label id=lblEmpName text='<%# Container.DataItem("EmpName") %>'  runat=server/>
									                </ItemTemplate>
								                </asp:TemplateColumn>
								
								                  <asp:TemplateColumn HeaderText="Divisi" SortExpression="B.Description">
                                	                <ItemTemplate>
										                <asp:Label id=lblEmpDiv text='<%# Container.DataItem("Description") %>'  runat=server/>
									                </ItemTemplate>
								                </asp:TemplateColumn>
								
                                                <asp:TemplateColumn HeaderText="Tipe" SortExpression="B.CodeEmpTy">
                                	                <ItemTemplate>
										                <asp:Label id=lblEmpType text='<%# Container.DataItem("CodeEmpTy") %>'  runat=server/>
									                </ItemTemplate>
								                </asp:TemplateColumn>
                                
                                                <asp:TemplateColumn HeaderText="Tot.Lembur(Jam)" SortExpression="TotalHours" HeaderStyle-HorizontalAlign="Right">
                                	                <ItemTemplate>
										                <asp:Label id=lblTotalJam text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("TotalHours"),2),2) %>'  runat=server/>
									                </ItemTemplate>
								                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateColumn>
								
                                                <asp:TemplateColumn HeaderText="Tot.Lembur(Rp)" SortExpression="TotalAmount" HeaderStyle-HorizontalAlign="Right">
                                	                <ItemTemplate>
										                <asp:Label id=lblTotalAmount text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("TotalAmount"),2),0) %>'  runat=server/>
									                </ItemTemplate>
									                <FooterTemplate >
									                    <asp:Label ID=lbTotal runat=server />
									                 </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />
								                </asp:TemplateColumn>
								
								                <asp:TemplateColumn HeaderText="Tgl update" HeaderStyle-HorizontalAlign="Center" SortExpression="UpdateDate">
									                <ItemTemplate>
										                <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									                </ItemTemplate>
									                <ItemStyle HorizontalAlign="Center" />
								                </asp:TemplateColumn>
								
								                <asp:TemplateColumn HeaderText="Diupdate" HeaderStyle-HorizontalAlign="Center" SortExpression="UserName">
									                <ItemTemplate>
										                <%# Container.DataItem("UserName") %>
									                </ItemTemplate>
								                <ItemStyle HorizontalAlign="Center" />
								                </asp:TemplateColumn>
								                <asp:TemplateColumn>
									                <ItemTemplate>
										                <asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server />
									                </ItemTemplate>
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
						<tr>
							<td>
					            <asp:ImageButton id=NewEmpBtn OnClick="NewEmpBtn_Click" imageurl="../../images/butt_new.gif" AlternateText="New Employee Overtime" runat="server"/>
						        <%--<asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print visible=false/>--%>
						        <asp:Label id=lblRedirect visible=false runat=server/>
							</td>
						</tr>
                        <tr>
                            <td>
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
