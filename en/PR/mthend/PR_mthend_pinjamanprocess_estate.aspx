<%@ Page Language="vb" src="../../../include/PR_mthend_pinjamanprocess_estate.aspx.vb" Inherits="PR_mthend_pinjamanprocess" %>
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
					<td colspan=4 class="mt-h" width="100%" >Potongan Lain - Pinjaman</td>
				</tr>
				<tr>
					<td colspan=4><hr size="1" noshade></td>
				</tr>
				<td height=25 width=20%>Period :</td>
					<td width=50%>	<asp:DropDownList id="ddlMonthpot" width="20%" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlpotdivisi_OnSelectedIndexChanged" runat=server>
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
									<asp:DropDownList id=ddlyearpot width="20%" runat="server" /></td>
					<td colspan=2>&nbsp;</td>
				<tr valign=top>
					<td height=25>Divisi :</td>
					<td><GG:AutoCompleteDropDownList id=ddlpotdivisi width="50%" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlpotdivisi_OnSelectedIndexChanged" runat=server/></td>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr valign=top>
					<td height=25>Employee Code :</td>
					<td><GG:AutoCompleteDropDownList id=ddlpotempcode width="50%" runat=server/>
					</td>
					<td colspan=2>&nbsp;</td>
				</tr>
				
				<tr valign=top>
					<td height=25 width=20%>Jenis Potongan :</td>
					<td style="width: 30%">
					    <asp:RadioButton id="rbPotPjm" 
							Checked="True"
							GroupName="AllPot"
							Text=" Pinjaman Tidak Diambil"
							TextAlign="Right"
							runat="server" /><br>
						<asp:RadioButton id="rbPotLain" 
							Checked="False"
							GroupName="AllPot"
							Text=" Potongan Lain"
							TextAlign="Right"
							runat="server" />
											
					</td>
					<td colspan=2>&nbsp;</td>
				</tr>
				
				<tr valign=top>
					<td height=25>Potongan :</td>
					<td><asp:TextBox ID="txtpot" runat="server" onkeypress="return isNumberKey(event)"  Width="90px"></asp:TextBox>
					</td>
					<td colspan=2>&nbsp;</td>
				</tr>
				
				
				
				<tr valign=top>
					<td height=25>Keterangan :</td>
					<td><asp:TextBox ID="txtpotket" runat="server" MaxLength="100" Width="50%"></asp:TextBox>
					&nbsp;<asp:ImageButton id=btnputadd onclick=btnAddpot_onClick imageurl="../../images/butt_add.gif" alternatetext="Add" runat=server />
					</td>
					<td colspan=2>&nbsp;</td>
				</tr>
				
	            <tr>
					<td colspan=4 style="height: 5px">&nbsp;</td>
				</tr>
				
                <tr>
					<td colspan="4">
					<div id="divpot" style="height: 130px;width:70%;overflow: auto;">
					 <asp:DataGrid ID="dgpot" runat="server" 
                               CellPadding="2" GridLines="None" Width="100%" AutoGenerateColumns=false OnDeleteCommand=DEDR_Delete  >
                              <AlternatingItemStyle CssClass="mr-r" />
                              <ItemStyle CssClass="mr-l" />
                              <HeaderStyle CssClass="mr-h" />
                              <Columns>
                                <asp:TemplateColumn HeaderText="NIK">
									<ItemTemplate>
										<asp:label ID="dgPot_lbl_ec" Width="100px" Text='<%#Container.DataItem("empcode")%>' runat="server" /> 
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Nama">
									<ItemTemplate>
									<asp:label ID="dgPot_lbl_nm" Width="100px" Text='<%#Container.DataItem("empname")%>' runat="server" /> 
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Type">
									<ItemTemplate>
									<asp:label ID="dgPot_lbl_ty" Width="50px" Text='<%#Container.DataItem("Codeempty")%>' runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Divisi">
									<ItemTemplate>
									<asp:label ID="dgPot_lbl_div" Width="50px" Text='<%#Container.DataItem("iddiv")%>' runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Potongan Lain">
									<ItemTemplate>
									<asp:label ID="dgpot_lbl_denda" Text='<%# Container.DataItem("Potong") %>' Width="100px" runat="server" />
									<asp:label ID="dgpot_lbl_AccMonth" Text='<%# Container.DataItem("AccMonth") %>' visible=false Width="100px" runat="server" />
									<asp:label ID="dgpot_lbl_AccYear" Text='<%# Container.DataItem("AccYear") %>' visible=false Width="100px" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Keterangan">
									<ItemTemplate>
									<asp:label ID="dgPot_lbl_ket" Width="150px" Text='<%#Container.DataItem("Notes")%>' runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Type">
									<ItemTemplate>
									<asp:label ID="dgPot_lbl_t" Width="30px" Text='<%#Container.DataItem("Ty")%>' runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								
								<asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="Delete" CommandName="Delete" Text="Delete" runat="server" />  
										    </ItemTemplate>
											<ItemStyle HorizontalAlign="Right" />		
                                </asp:TemplateColumn>
										
								</Columns>
                            </asp:DataGrid>
							</div>
						</td>
				</tr>
				<tr>
                    <td colspan="4" height=25>
                    </td>
                </tr>
				
				<tr>
					<td colspan=4 class="mt-h" width="100%" >PROSES PINJAMAN</td>
				</tr>
				<tr>
					<td colspan=4><hr size="1" noshade></td>
				</tr>
				
				<tr>
					<td colspan=4 height=10>&nbsp;</td>
				</tr>
				<tr valign=top>
					<td height=25 width=20%>Period :</td>
					<td width=50%>	<asp:DropDownList id="ddlMonth" width="20%" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlMonth_OnSelectedIndexChanged" runat=server>
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
									<asp:DropDownList id=ddlyear width="20%" runat="server" /></td>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr valign=top>
					<td height=25>Process Type :</td>
					<td>
						<asp:RadioButton id="rbAllEmp" 
							Checked="True"
							GroupName="AllEmp"
							Text="All Employees"
							AutoPostBack=True
							OnCheckedChanged=Check_Clicked
							TextAlign="Right"
							runat="server" /><br>
						<asp:RadioButton id="rbSelectedEmp" 
							Checked="False"
							GroupName="AllEmp"
							Text="Selected Employee"
							AutoPostBack=True
							OnCheckedChanged=Check_Clicked
							TextAlign="Right"
							runat="server" />					
					</td>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=4 style="height: 19px">&nbsp;</td>
				</tr>
				<tr valign=top>
					<td height=25>Employee Code :</td>
					<td><GG:AutoCompleteDropDownList id=ddlEmployee enabled=false width="50%" runat=server/></td>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=4 style="height: 19px">&nbsp;</td>
				</tr>
				
                
				<tr>
					<td colspan=4>&nbsp;<asp:ImageButton id=btnGenerate onclick=btnGenerate_Click imageurl="../../images/butt_process.gif" alternatetext="Proses Pinjaman" runat=server /></td>
				</tr>
				<tr>
                    <td colspan="4">
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
       				     <div id="divprocess" visible="False" runat="server">
       				     <table border="0" cellspacing="1" cellpadding="1" width="100%">
			        	  <tr>
					      <td colspan=6 width=100% class="mb-c">
						        <table width="100%" cellspacing="0" cellpadding="3" border="0" align="center">
							    <tr class="mb-t">
								    <td width="10%" height="26" valign=bottom>NIK :<BR><asp:TextBox id=txtEmpCode width=100% maxlength="25" runat="server" /></td>
								    <td width="10%" height="26" valign=bottom>Divisi :<BR><asp:DropDownList id="ddlEmpDiv" width=100% runat="server" /></td>
							    	<td width="20%" height="26" valign=bottom>Mandoran :<BR><asp:DropDownList id="ddlmandor" width=100% runat="server" /></td>
								    <td width="10%" height="26" valign=bottom>
								    <asp:Button id=SearchBtn Text="View" OnClick="SearchBtn_OnClick" runat="server"/>
								    <asp:Button id=PrintBtn Text="Print" OnClick="PrintBtn_OnClick" runat="server"/>
								    </td>
							    </tr>
						        </table>
					       </td>
				           </tr>
				           <tr>
					       <td colspan=6 height="10%">			
					       <div id="div1" style="height: 200px;width:800;overflow: auto;">				
					          <asp:DataGrid ID="dgProcess" runat="server" 
                               CellPadding="2" GridLines="None" Width="100%" AutoGenerateColumns=false>
                              <AlternatingItemStyle CssClass="mr-r" />
                              <ItemStyle CssClass="mr-l" />
                              <HeaderStyle CssClass="mr-h" />
                              <Columns>
                               <asp:TemplateColumn HeaderText="Periode" >
									<ItemTemplate>
									    <%#Container.DataItem("accmonth")& "/" & Container.DataItem("accyear")%>
									</ItemTemplate>
								 </asp:TemplateColumn>
								 <asp:TemplateColumn HeaderText="NIK">
									<ItemTemplate>
										<%#Container.DataItem("empcode")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Nama">
									<ItemTemplate>
										<%#Container.DataItem("empname")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Type">
									<ItemTemplate>
										<%#Container.DataItem("emptype")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Divisi">
									<ItemTemplate>
										<%#Container.DataItem("divname")%>
									</ItemTemplate>
								</asp:TemplateColumn>
							   <asp:TemplateColumn HeaderText="HKD">
									<ItemTemplate>
										<%#Container.DataItem("Hkd")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Lembur">
									<ItemTemplate>
										<%#Container.DataItem("Lembur")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Premi">
									<ItemTemplate>
										<%#Container.DataItem("Premi")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Kerajinan">
									<ItemTemplate>
										<%#Container.DataItem("Kerajinan")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Premi Lain">
									<ItemTemplate>
										<%#Container.DataItem("Premilain")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Pinjaman">
									<ItemTemplate>
										<%#Container.DataItem("Smallpay")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Denda">
									<ItemTemplate>
										<%#Container.DataItem("Denda")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Total">
									<ItemTemplate>
										<%#Container.DataItem("TotSmallpay")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								</Columns>
                            </asp:DataGrid>
                            </div>
					        </td>
				            </tr>
			                </table>   				
       				        
					    
                        </div>
                      </td>
                </tr>
			</table>
		</form>
	</body>
</html>
