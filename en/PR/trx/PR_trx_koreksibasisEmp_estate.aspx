<%@ Page Language="vb" src="../../../include/PR_trx_koreksibasisEmp_estate.aspx.vb" Inherits="PR_trx_koreksibasisEmp_estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRTrx" src="../../menu/menu_prtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<html>
	<head>
		<title>Koreksi Basis Panen</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		
	</head>
	<body>
		<%--<Preference:PrefHdl id=PrefHdl runat="server" />--%>
		<form id=frmProcess runat=server class="main-modul-bg-app-list-pu">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." ForeColor="red" runat=server />



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
							<td><strong>KOREKSI BASIS PANEN</strong><hr style="width :100%" />   
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
								<td width="10%" height="26" valign=bottom>Periode :<BR>
							        <asp:DropDownList id="ddlMonth" width="55%" runat=server>
										<asp:ListItem value="01">January</asp:ListItem>
										<asp:ListItem value="02">February</asp:ListItem>
										<asp:ListItem value="03">March</asp:ListItem>
										<asp:ListItem value="04">April</asp:ListItem>
										<asp:ListItem value="05">May</asp:ListItem>
										<asp:ListItem value="06">June</asp:ListItem>
										<asp:ListItem value="07">July</asp:ListItem>
										<asp:ListItem value="08">Augustus</asp:ListItem>
										<asp:ListItem value="09">September</asp:ListItem>
										<asp:ListItem value="10">October</asp:ListItem>
										<asp:ListItem value="11">November</asp:ListItem>
										<asp:ListItem value="12">December</asp:ListItem>
									</asp:DropDownList>
									<asp:DropDownList id=ddlyear width="40%" runat="server" />
							        </td>
								    <td width="5%" height="26" valign=bottom>Divisi :<BR><asp:DropDownList id="ddlEmpDiv" width=100% OnSelectedIndexChanged = "bindemp_OnSelectedIndexChanged" autopostback="true" runat="server" /></td>
									<td width="15%" height="26" valign=bottom>Employee :<BR><asp:DropDownList id=ddlEmpCode width=100% maxlength="8" runat="server" /></td>
								    <td width="8%" height="26" valign=bottom>
								    <asp:Button id=SearchBtn Text="Search" OnClick="SearchBtn_OnClick" runat="server" class="button-small"/>
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
						            <asp:DataGrid ID="dgProcess" runat="server" 
                                           CellPadding="2" GridLines="None" Width="100%" AutoGenerateColumns=False OnItemDataBound="dgpay_BindGrid" 
                                                    class="font9Tahoma">
								
							                        <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                        <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                        <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
                                          <Columns>
							                <asp:TemplateColumn HeaderText="Periode">
									            <ItemTemplate>
										            <asp:label ID="dgProcessAM" Text='<%#Container.DataItem("AMonth")%>' runat="server" /> 
										            <asp:label ID="dgProcessAY" Text='<%#Container.DataItem("AYear")%>' runat="server" /> 
									            </ItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="Divisi">
									            <ItemTemplate>
										            <asp:label ID="dgProcessDV" Text='<%#Container.DataItem("iddiv")%>' runat="server" /> 
									            </ItemTemplate>
								            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="NIK">
									            <ItemTemplate>
										            <asp:label ID="dgProcessEC"  Text='<%#Container.DataItem("empcode")%>' runat="server" /> 
									            </ItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="Nama">
									            <ItemTemplate>
									            <asp:label ID="dgProcessNM"  Text='<%#Container.DataItem("empname")%>' runat="server" /> 
									            </ItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="Tanggal">
									            <ItemTemplate>
									            <asp:Label id=dgProcessDt text='<%# objGlobal.GetLongDate(Container.DataItem("dt")) %>'  runat=server/>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="JJG" HeaderStyle-HorizontalAlign=Right>
									            <ItemTemplate>
									            <asp:label ID="dgProcessJJG" Text='<%# Container.DataItem("Jjg") %>' style="text-align:right;" runat="server" />
									            </ItemTemplate>
									            <ItemStyle HorizontalAlign="Right" />
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="BTugas" HeaderStyle-HorizontalAlign=Right>
									            <ItemTemplate>
									            <asp:label ID="dgProcessBTugas" Text='<%# Container.DataItem("BTugas") %>' style="text-align:right;"  runat="server" />
									            </ItemTemplate>
									            <ItemStyle HorizontalAlign="Right" />
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="Basis" HeaderStyle-HorizontalAlign=Right>
									            <ItemTemplate>
									            <asp:label ID="dgProcessIsBasis" Text='<%# Container.DataItem("isBasis") %>' style="text-align:right;" runat="server" />
								                </ItemTemplate>
								                <ItemStyle HorizontalAlign="Right" />
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="Koreksi">
									            <ItemTemplate>
									            <asp:Checkbox ID="dgProcessKoreksi"  runat="server" />
								                </ItemTemplate>
								                <ItemStyle HorizontalAlign="Right" />
								            </asp:TemplateColumn>
								            </Columns>
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
							<TD colspan=6 >
								<asp:ImageButton id=btnAdd imageurl="../../images/butt_add.gif" alternatetext=Add onclick=btnAdd_Click runat=server />
                                &nbsp;
                            </TD>
						</tr>
						<tr>
						<TD colspan=6 >
							List Koreksi
                        </TD>
						</tr>
                        <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
                                        <asp:DataGrid ID="dgProcessKoreksi" runat="server" 
                                           CellPadding="2" GridLines="None" Width="100%" AutoGenerateColumns=False OnItemDataBound="dgkoreksi_BindGrid" OnDeleteCommand=DEDR_Delete   
                                                                class="font9Tahoma">
								
							                                    <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                                    <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                                    <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
                                          <Columns>
							                <asp:TemplateColumn HeaderText="Periode">
									            <ItemTemplate>
										            <asp:label ID="dgProcessKoreksiAM" Text='<%#Container.DataItem("AccMonth")%>' runat="server" /> 
										            <asp:label ID="dgProcessKoreksiAY" Text='<%#Container.DataItem("AccYear")%>' runat="server" /> 
									            </ItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="Divisi">
									            <ItemTemplate>
										            <asp:label ID="dgProcessKoreksiDV" Text='<%#Container.DataItem("iddiv")%>' runat="server" /> 
									            </ItemTemplate>
								            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="NIK">
									            <ItemTemplate>
										            <asp:label ID="dgProcessKoreksiEC"  Text='<%#Container.DataItem("empcode")%>' runat="server" /> 
									            </ItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="Nama">
									            <ItemTemplate>
									            <asp:label ID="dgProcessKoreksiNM"  Text='<%#Container.DataItem("empname")%>' runat="server" /> 
									            </ItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="Tanggal">
									            <ItemTemplate>
									            <asp:Label id=dgProcessKoreksiDt text='<%# objGlobal.GetLongDate(Container.DataItem("Pdate")) %>'  runat=server/>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="JJG" HeaderStyle-HorizontalAlign=Right>
									            <ItemTemplate>
									            <asp:label ID="dgProcessKoreksiJJG" Text='<%# Container.DataItem("Jjg") %>' style="text-align:right;" runat="server" />
									            </ItemTemplate>
									            <ItemStyle HorizontalAlign="Right" />
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="BTugas" HeaderStyle-HorizontalAlign=Right>
									            <ItemTemplate>
									            <asp:label ID="dgProcessKoreksiBTugas" Text='<%# Container.DataItem("BTugas") %>' style="text-align:right;"  runat="server" />
									            </ItemTemplate>
									            <ItemStyle HorizontalAlign="Right" />
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="Basis" HeaderStyle-HorizontalAlign=Right>
									            <ItemTemplate>
									            <asp:label ID="dgProcessKoreksiIsBasis" Text='<%# Container.DataItem("isBasis") %>' style="text-align:right;" runat="server" />
								                </ItemTemplate>
								                <ItemStyle HorizontalAlign="Right" />
								            </asp:TemplateColumn>
								            <asp:TemplateColumn >
									            <ItemTemplate>
									            <asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server />
								                </ItemTemplate>
								            </asp:TemplateColumn>
								            </Columns>
                                        </asp:DataGrid>

                                    </td>
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
