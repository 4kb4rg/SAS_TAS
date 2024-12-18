<%@ Page Language="vb" src="../../../include/PR_mthend_saldoawalpinjaman_estate.aspx.vb" Inherits="PR_mthend_saldoawalpinjaman_estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRMthEnd" src="../../menu/menu_PRMthEnd.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Pinjaman Process</title>
		
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<form id=frmProcess runat=server>
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." ForeColor="red" runat=server />
			<table border="0" cellpadding="0" cellspacing="0" width="100%">
				<%--<tr>
					<td colspan=4>
						<UserControl:MenuPRMthEnd id=MenuPRMthEnd runat="server" />
					</td>
				</tr>--%>
				<tr>
					<td colspan=4 class="mt-h" width="100%" >SALDO AWAL GAJI KECIL</td>
				</tr>
				<tr>
					<td colspan=4><hr size="1" noshade></td>
				</tr>
				
				<tr>
					<td colspan=4 height=25>&nbsp;</td>
				</tr>
				<tr>
                    <td colspan="4">
       				      <table border="0" cellspacing="1" cellpadding="1" width="100%">
			        	  <tr>
					      <td colspan=6 width=100% class="mb-c">
						        <table width="100%" cellspacing="0" cellpadding="3" border="0" align="center">
							    <tr class="mb-t">
							        <td width="10%" height="26" valign=bottom>Periode :<BR>
							        <asp:DropDownList id="ddlMonth" width="55%" OnSelectedIndexChanged = "bindemp_OnSelectedIndexChanged" autopostback="true" runat=server>
										<asp:ListItem value="1">January</asp:ListItem>
										<asp:ListItem value="2">February</asp:ListItem>
										<asp:ListItem value="3">March</asp:ListItem>
										<asp:ListItem value="4">April</asp:ListItem>
										<asp:ListItem value="5">May</asp:ListItem>
										<asp:ListItem value="6">June</asp:ListItem>
										<asp:ListItem value="7">July</asp:ListItem>
										<asp:ListItem value="8">Augustus</asp:ListItem>
										<asp:ListItem value="9">September</asp:ListItem>
										<asp:ListItem value="10">October</asp:ListItem>
										<asp:ListItem value="11">November</asp:ListItem>
										<asp:ListItem value="12">December</asp:ListItem>
									</asp:DropDownList>
									<asp:DropDownList id=ddlyear width="40%" runat="server" />
							        </td>
								    <td width="5%" height="26" valign=bottom>Divisi :<BR><asp:DropDownList id="ddlEmpDiv" width=100% OnSelectedIndexChanged = "bindemp_OnSelectedIndexChanged" autopostback="true" runat="server" /></td>
									<td width="15%" height="26" valign=bottom>Employee :<BR><asp:DropDownList id=ddlEmpCode width=100% maxlength="8" runat="server" /></td>
								    <td width="10%" height="26" valign=bottom>Mandoran :<BR><asp:DropDownList id="ddlmandor" width=100% runat="server" /></td>
								    <td width="8%" height="26" valign=bottom>
								    <asp:Button id=SearchBtn Text="View" OnClick="SearchBtn_OnClick" runat="server"/>
								    <asp:Button id=SubmitBtn Text="Submit" OnClick="SubmitBtn_OnClick" runat="server"/>
								    </td>
							    </tr>
						        </table>
					       </td>
				           </tr>
				           <tr>
					       <td colspan=7>	
					       		
					       <div id="div1" style="height: 360px;width:99%;overflow: auto;">	
					       
					          <asp:DataGrid ID="dgProcess" runat="server" 
                               CellPadding="2" GridLines="None" Width="100%" AutoGenerateColumns=false>
                              <AlternatingItemStyle CssClass="mr-r" />
                              <ItemStyle CssClass="mr-l" />
                              <HeaderStyle CssClass="mr-h" />
                              <Columns>
							    <asp:TemplateColumn HeaderText="Divisi">
									<ItemTemplate>
										<asp:label ID="dgProcess_lbl_dv" Width="70px" Text='<%#Container.DataItem("divname")%>' runat="server" /> 
									</ItemTemplate>
								</asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="NIK">
									<ItemTemplate>
										<asp:label ID="dgProcess_lbl_ec" Width="100px" Text='<%#Container.DataItem("empcode")%>' runat="server" /> 
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Nama">
									<ItemTemplate>
									<asp:label ID="dgProcess_lbl_nm" Width="150px" Text='<%#Container.DataItem("empname")%>' runat="server" /> 
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Type">
									<ItemTemplate>
									<asp:label ID="dgProcess_lbl_ty" Width="40px" Text='<%#Container.DataItem("emptype")%>' runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="HKD" HeaderStyle-HorizontalAlign=Right>
									<ItemTemplate>
									<asp:TextBox ID="dgProcess_lbl_hkd" Text='<%# Container.DataItem("Hkd") %>' Width="50px" style="text-align:right;" onkeypress="javascript:return isNumberKey(event)" runat="server" />
									</ItemTemplate>
									<ItemStyle HorizontalAlign="Right" Width="9%" />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Pinjaman" HeaderStyle-HorizontalAlign=Right>
									<ItemTemplate>
									<asp:TextBox ID="dgProcess_lbl_pjm" Text='<%# Container.DataItem("Smallpay") %>' Width="70px" style="text-align:right;" onkeypress="javascript:return isNumberKey(event)" runat="server" />
									</ItemTemplate>
									<ItemStyle HorizontalAlign="Right" Width="9%" />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Lembur" HeaderStyle-HorizontalAlign=Right>
									<ItemTemplate>
									<asp:TextBox ID="dgProcess_lbl_lbr" Text='<%# Container.DataItem("Lembur") %>' Width="70px" style="text-align:right;" onkeypress="javascript:return isNumberKey(event)" runat="server" />
								    </ItemTemplate>
								    <ItemStyle HorizontalAlign="Right" Width="9%" />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Premi" HeaderStyle-HorizontalAlign=Right>
									<ItemTemplate>
									<asp:TextBox ID="dgProcess_lbl_prm" Text='<%# Container.DataItem("Premi") %>' Width="70px" style="text-align:right;" onkeypress="javascript:return isNumberKey(event)" runat="server" />
									</ItemTemplate>
									<ItemStyle HorizontalAlign="Right" Width="9%" />
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Denda" HeaderStyle-HorizontalAlign=Right>
									<ItemTemplate>
									<asp:TextBox ID="dgProcess_lbl_dnd" Text='<%# Container.DataItem("Denda") %>' Width="70px" style="text-align:right;" onkeypress="javascript:return isNumberKey(event)" runat="server" />
									</ItemTemplate>
									<ItemStyle HorizontalAlign="Right" Width="9%" />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Total" HeaderStyle-HorizontalAlign=Right>
									<ItemTemplate>
									<asp:label ID="dgProcess_lbl_tot"  Text='<%#Container.DataItem("TotSmallpay")%>' Width="100px" runat="server" /> 
									</ItemTemplate>
									<ItemStyle HorizontalAlign="Right" Width="9%" />
								</asp:TemplateColumn>
								</Columns>
                            </asp:DataGrid>
                            </div>
					        </td>
				            </tr>
			                </table>   				
       				  </td>
                </tr>
			</table>
		</form>
	</body>
</html>
