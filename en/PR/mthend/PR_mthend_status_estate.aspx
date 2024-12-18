<%@ Page Language="vb" src="../../../include/PR_mthend_status_estate.aspx.vb" Inherits="PR_mthend_status_estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRMthEnd" src="../../menu/menu_PRMthEnd.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Confirm Proses Payroll</title>
		
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
					<td colspan=4 class="mt-h" width="100%" >Confirm Proses Payroll</td>
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
							        <td width="10%" height="26" valign=bottom>Periode :
							        <asp:DropDownList id="ddlMonth" width="10%" runat=server>
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
									<asp:DropDownList id=ddlyear width="10%" OnSelectedIndexChanged="ddlyear_OnSelectedIndexChanged"  AutoPostBack=true runat="server" />
							        <asp:ImageButton id=btnGenerate onclick=btnGenerate_Click imageurl="../../images/butt_confirm.gif" alternatetext="Confirm" runat=server />
								    </td>
							    </tr>
						        </table>
					       </td>
				           </tr>
				           <tr>
					       <td colspan=7>	
					          <asp:DataGrid ID="dgProcess" runat="server" 
                               CellPadding="2" GridLines="None" Width="100%" AutoGenerateColumns=false>
                              <AlternatingItemStyle CssClass="mr-r" />
                              <ItemStyle CssClass="mr-l" />
                              <HeaderStyle CssClass="mr-h" />
                              <Columns>
							    <asp:TemplateColumn HeaderText="Bulan">
									<ItemTemplate>
										<asp:label ID="dgProcess_lbl_dv" Width="70px" Text='<%#Container.DataItem("AccMonth")%>' runat="server" /> 
									</ItemTemplate>
								</asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Tahun">
									<ItemTemplate>
										<asp:label ID="dgProcess_lbl_ec" Width="70px" Text='<%#Container.DataItem("AccYear")%>' runat="server" /> 
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Gaji Besar">
									<ItemTemplate>
									<asp:label ID="dgProcess_lbl_nm" Width="70px" Text='<%#Container.DataItem("StatusGaji")%>' runat="server" /> 
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Catu Beras">
									<ItemTemplate>
									<asp:label ID="dgProcess_lbl_ty" Width="70px" Text='<%#Container.DataItem("StatusCatu")%>' runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Pinjaman">
									<ItemTemplate>
									<asp:label ID="dgProcess_lbl_hkd" Width="70px" Text='<%# Container.DataItem("StatusPinjaman") %>' runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Tutup Buku Payroll" >
									<ItemTemplate>
									<asp:label ID="dgProcess_lbl_pjm" Width="70px" Text='<%# Container.DataItem("StatusTutupBuku") %>' runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Tgl Update" >
									<ItemTemplate>
									<asp:label ID="dgProcess_lbl_pjm" Text='<%# Container.DataItem("UpdateDate") %>' runat="server" />
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
		</form>
	</body>
</html>
