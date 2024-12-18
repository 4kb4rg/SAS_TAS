<%@ Page Language="vb" src="../../../include/PR_trx_FingerException_Estate.aspx.vb" Inherits="PR_trx_FingerException_Estate"%> 
<%@ Register TagPrefix="UserControl" Tagname="MenuPRTrx" src="../../menu/menu_prtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Exception Attendace List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
	<%--<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	    <form id=frmMain runat=server >
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
				<td style="width: 100%; height: 400px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>PERLU EXCEPTION ?</strong><hr style="width :100%" />   
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
								<td height="26" style="width: 25%">
									Tgl :<BR>
									<asp:TextBox id=txtAttdDate  Font-Bold="True" width="79%" maxlength="10" runat="server"  />
									<a href="javascript:PopCal('txtAttdDate');"><asp:Image ID="btnSelDOB" runat="server" ImageUrl="../../Images/calendar.gif" /></a>
									</td>
									<td height="26" style="width: 20%">Divisi :<BR><asp:DropDownList id="ddlEmpDiv" width=100% runat="server" /></td>
									<td height="26" style="width: 20%">Type :<BR><asp:DropDownList id="ddlEmpType" width=100% runat="server" /></td>
									<td height="26" style="width: 20%">NIK :<BR><asp:textbox id="txtnik" maxlength="20" width=100% runat="server" /></td>
									<td height="26" style="width: 20%">Nama :<BR><asp:textbox id="txtnama" maxlength="20" width=100% runat="server" /></td>
									<td height="26" width="60%"  valign=bottom align=right><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
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
										AllowPaging=False 
										Pagerstyle-Visible=False 
                                        				class="font9Tahoma">
								
							            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
										
										<Columns>
											<asp:TemplateColumn HeaderText="Tanggal ">
												<ItemTemplate>
												<asp:Label id=lbltgl text='<%# objGlobal.GetLongDate(Container.DataItem("Datein")) %>'  runat=server/>
												</ItemTemplate>
											</asp:TemplateColumn>
											
											<asp:TemplateColumn HeaderText="NIK">
												<ItemTemplate>
													<asp:Label id=lblnik text='<%# Container.DataItem("EmpCode") %>'  runat=server/>
												</ItemTemplate>
											</asp:TemplateColumn>
										
											<asp:TemplateColumn HeaderText="Nama" >
												<ItemTemplate>
													<asp:Label id=lblnama text='<%# Container.DataItem("EmpName")%>' runat=server/>
												</ItemTemplate>
											</asp:TemplateColumn>
											
											<asp:TemplateColumn HeaderText="In.1" >
												<ItemTemplate>
													<asp:Label id=lblin1 text='<%# format(Container.DataItem("in1"),"HH:mm") %>'  runat=server/>
												</ItemTemplate>
											</asp:TemplateColumn>
											
											 <asp:TemplateColumn HeaderText="Out.1" >
												<ItemTemplate>
													<asp:Label id=lblout1 text='<%# format(Container.DataItem("out1"),"HH:mm") %>'  runat=server/>
												</ItemTemplate>
											</asp:TemplateColumn>
											
											<asp:TemplateColumn HeaderText="In.2" >
												<ItemTemplate>
													<asp:Label id=lblin2 text='<%# format(Container.DataItem("in2"),"HH:mm") %>'  runat=server/>
												</ItemTemplate>
											</asp:TemplateColumn>
											
											 <asp:TemplateColumn HeaderText="Out.2" >
												<ItemTemplate>
													<asp:Label id=lblout2 text='<%# format(Container.DataItem("out2"),"HH:mm") %>'  runat=server/>
												</ItemTemplate>
											</asp:TemplateColumn>
											
											<asp:TemplateColumn HeaderText="In.3" >
												<ItemTemplate>
													<asp:Label id=lblin3 text='<%# format(Container.DataItem("in3"),"HH:mm") %>'  runat=server/>
												</ItemTemplate>
											</asp:TemplateColumn>
											
											 <asp:TemplateColumn HeaderText="Out.3" >
												<ItemTemplate>
													<asp:Label id=lblout3 text='<%# format(Container.DataItem("out3"),"HH:mm") %>'  runat=server/>
												</ItemTemplate>
											</asp:TemplateColumn>
											
											<asp:TemplateColumn HeaderText="Alasan" >
												<ItemTemplate>
													<asp:textbox id=txtalasan runat=server/>
												</ItemTemplate>
											</asp:TemplateColumn>
											
											<asp:TemplateColumn HeaderText="Di Setujui Oleh :" >
												<ItemTemplate>
													<asp:textbox id=txtpea runat=server/>
												</ItemTemplate>
											</asp:TemplateColumn>
											
											<asp:TemplateColumn HeaderText="Approve">
											<ItemTemplate>
												<asp:CheckBox id="Absent"  runat="server"/>	
											</ItemTemplate>
											</asp:TemplateColumn>
																			
										</Columns>
										<PagerStyle Visible="False" />
									</asp:DataGrid>
                                    </td>
                                    </tr>
								</table>
							</td>
						</tr>
                        <tr>
                            <td>
                                    <asp:ImageButton ID="btnSave" OnClick="Btnsave_Click" runat="server" AlternateText="Save" ImageUrl="../../images/butt_save.gif" />
                                    &nbsp;
                            </td>
                        </tr>
						<tr>
							<td>
							    &nbsp;</td>
						</tr>
                        <tr>
							<td><strong>DAFTAR EXCEPTION</strong>  
                            </td>
                            
						</tr>
                        <tr>
							<td style="background-color:#FFCC00" >
							<table cellpadding="4" cellspacing="0" style="width: 100%">
								<tr class="font9Tahoma">
								    <td height="26" style="width: 25%">
									Periode  :<BR>
									<asp:DropDownList ID="ddlEmpMonth" runat="server" Width="65%">
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
                                    </asp:DropDownList><asp:DropDownList id="ddlyear" width="30%" runat=server></asp:DropDownList>
									</td>
									<td height="26" style="width: 20%">Divisi :<BR><asp:DropDownList id="ddlEmpDivlst" width=100% runat="server" /></td>
									<td height="26" style="width: 15%">Type :<BR><asp:DropDownList id="ddlEmpTypelst" width=100% runat="server" /></td>
									<td height="26" style="width: 20%">NIK :<BR><asp:textbox id="txtniklst" maxlength="20" width=100% runat="server" /></td>
									<td height="26" style="width: 20%">Nama :<BR><asp:textbox id="txtnamalst" maxlength="20" width=100% runat="server" /></td>
									<td height="26" width="60%"  valign=bottom align=right><asp:Button id=SearchBtnlst Text="Search" OnClick=srchBtn_Clicklst runat="server" class="button-small"/></td>
								</tr>
							</table>
							
							</td>
						</tr>
                        <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
                                        <asp:DataGrid id=dgExpList
										    AutoGenerateColumns=False width=100% runat=server
										    GridLines=None 
										    Cellpadding=2 
										    AllowPaging=False 
										    Pagerstyle-Visible=False 
										    OnDeleteCommand=dgExpList_Delete 
										
                                        					    class="font9Tahoma">
								
							                <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
										
										    <Columns>
											    <asp:TemplateColumn HeaderText="Tanggal ">
												    <ItemTemplate>
												    <asp:Label id=lbltgllst text='<%# objGlobal.GetLongDate(Container.DataItem("AttDate")) %>'  runat=server/>
												    </ItemTemplate>
											    </asp:TemplateColumn>
											
											    <asp:TemplateColumn HeaderText="NIK">
												    <ItemTemplate>
													    <asp:Label id=lblniklst text='<%# Container.DataItem("EmpCode") %>'  runat=server/>
												    </ItemTemplate>
											    </asp:TemplateColumn>
										
											    <asp:TemplateColumn HeaderText="Nama" >
												    <ItemTemplate>
													    <asp:Label id=lblnamalst text='<%# Container.DataItem("EmpName")%>' runat=server/>
												    </ItemTemplate>
											    </asp:TemplateColumn>
											
											    <asp:TemplateColumn HeaderText="Jam in" >
												    <ItemTemplate>
													    <asp:Label id=lblinlst text='<%# format(Container.DataItem("jamin"),"HH:mm") %>'  runat=server/>
												    </ItemTemplate>
											    </asp:TemplateColumn>
											
											     <asp:TemplateColumn HeaderText="Jam Out" >
												    <ItemTemplate>
													    <asp:Label id=lbloutlst text='<%# format(Container.DataItem("jamout"),"HH:mm") %>'  runat=server/>
												    </ItemTemplate>
											    </asp:TemplateColumn>
											
											    <asp:TemplateColumn HeaderText="Alasan" >
												    <ItemTemplate>
													    <asp:Label id=txtalasanlst text='<%# Container.DataItem("Ket")%>' runat=server/>
												    </ItemTemplate>
											    </asp:TemplateColumn>
											
											    <asp:TemplateColumn HeaderText="Di Setujui Oleh :" >
												    <ItemTemplate>
													    <asp:Label id=txtpealst text='<%# Container.DataItem("PEA")%>' runat=server/>
												    </ItemTemplate>
											    </asp:TemplateColumn>
											
											    <asp:TemplateColumn HeaderText="Tgl Update" >
												    <ItemTemplate>
													    <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
												    </ItemTemplate>
											    </asp:TemplateColumn>
											
											    <asp:TemplateColumn HeaderText="Update By" >
												    <ItemTemplate>
													    <%# Container.DataItem("Updateid") %>
												    </ItemTemplate>
											    </asp:TemplateColumn>
											
											    <asp:TemplateColumn>
												    <ItemTemplate>
													    <asp:LinkButton id=dgkoreksi_lbDelete CommandName=Delete Text=Delete runat=server />
												    </ItemTemplate>
											    </asp:TemplateColumn>	
																			
										    </Columns>
										    <PagerStyle Visible="False" />
									    </asp:DataGrid>
                                    </td>
                                    </tr>
                                </table>
                                </td>
                        </tr>
						<tr>
							<td>
					            <asp:ImageButton ID="btnprint" OnClick="Btnprint_Click" runat="server" AlternateText="Save" ImageUrl="../../images/butt_save.gif"/>
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