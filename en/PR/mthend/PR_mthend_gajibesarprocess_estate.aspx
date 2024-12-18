<%@ Page Language="vb" src="../../../include/PR_mthend_gajibesarprocess_estate.aspx.vb" Inherits="PR_mthend_gajibesarprocess" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRMthEnd" src="../../menu/menu_PRMthEnd.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Gaji Process</title>
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<form id=frmMain runat=server>
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." ForeColor=red runat=server />
			<table border="0" cellpadding="0" cellspacing="0" width="100%">
				<tr>
					<td colspan=4 class="mt-h" width="100%" >
                        PROSES GAJI BESAR</td>
				</tr>
				<tr>
					<td colspan=4><hr size="1" noshade></td>
				</tr>
				
				<tr>
					<td colspan=4 height=25>
                        <asp:RadioButton id="RdoMonthProcess" 
							Checked="True"
							GroupName="AllProc"
							Text="Proses Gaji Bulanan"
							AutoPostBack=True
							OnCheckedChanged=Check_Gaji_Clicked
							TextAlign="Right"
							runat="server" Font-Bold="True" />
                        <asp:RadioButton id="RdoDayProcess" 
							Checked="False"
							GroupName="AllProc"
							Text="Proses Pinjaman Gaji Besar"
							AutoPostBack=True
							OnCheckedChanged=Check_Gaji_Clicked
							TextAlign="Right"
							runat="server" Font-Bold="True" /></td>
				</tr>
                <tr valign="top">
                    <td style="height: 25px" width="20%">
                    </td>
                    <td style="height: 25px" width="50%">
                    </td>
                    <td colspan="2" style="height: 25px">
                    </td>
                </tr>
				<tr valign=top>
					<td height=25 width=20%>Period :</td>
					<td width=50%>	<asp:DropDownList id="ddlMonth" width="20%" OnSelectedIndexChanged="ddlEmpDiv_OnSelectedIndexChanged" AutoPostBack=true runat=server>
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
									<asp:DropDownList id=ddlyear width="20%" maxlength="20" runat="server" /></td>
					<td colspan=2>&nbsp;</td>
				</tr>
                <tr valign="top">
                    <td height="25">
                    </td>
                    <td>
                        <asp:TextBox ID="txtDateFrom" runat="server" MaxLength="10" Visible="False" Width="15%"></asp:TextBox>
                        <a href="javascript:PopCal('txtDateFrom');"><asp:Image id="btnDateFrom" runat="server" ImageUrl="../../Images/calendar.gif" Visible="False"/></a>&nbsp;
                        <asp:Label ID="lblNote" runat="server" Text="s/d" Visible="False"></asp:Label>&nbsp;
                        <asp:TextBox ID="txtDateTo" runat="server" MaxLength="10" Visible="False" Width="15%"></asp:TextBox>
                        <a href="javascript:PopCal('txtDateTo');"><asp:Image ID="btnDateTo" runat="server" ImageUrl="../../Images/calendar.gif" Visible="False" /></a>
                        <asp:RequiredFieldValidator ID="rfvDateCreated" runat="server" ControlToValidate="txtDateFrom"
                            Display="dynamic" Text="Please enter Date Created"></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="rfvDateCreatedTo" runat="server" ControlToValidate="txtDateTo"
                            Display="dynamic" Text="Please enter Date Created"></asp:RequiredFieldValidator>&nbsp;<br />
                        <asp:Label ID="lblDate" runat="server" ForeColor="red" Text="<br>Date Entered should be in the format "
                            Visible="false"></asp:Label>
                        <asp:Label ID="lblFmt" runat="server" ForeColor="red" Visible="false"></asp:Label></td>
                    <td colspan="2">
                    </td>
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
					<td colspan=4>&nbsp;</td>
				</tr>
				<tr valign=top>
					<td height=25>Employee Code :</td>
					<td><GG:AutoCompleteDropDownList id=ddlEmployee enabled=false width="50%" runat=server/></td>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=4 style="height: 19px"></td>
				</tr>
                <tr>
                    <td colspan="4" style="height: 19px">
                        <asp:CheckBox ID="chkDelDaily" runat="server" AutoPostBack="true" Checked="false"
                            OnCheckedChanged="Check_Del_DailyProcess" Text=" Hapus hasil proses pinjaman gaji besar" Font-Underline="True" ForeColor="Red" /></td>
                </tr>
                <tr>
                    <td colspan="4" style="height: 19px">
                    </td>
                </tr>
				<tr>
					<td colspan=4>&nbsp;<asp:ImageButton id=btnGenerate onclick=btnGenerate_Click imageurl="../../images/butt_process.gif" alternatetext="Generate" runat=server /></td>
				</tr>
				    <tr>
                    <td colspan="4">
                    </td>
                </tr>
				
				<tr>
                    <td colspan="4">
       				     <div id="divmsg" visible="False" runat="server">
       				     <table border="0" cellspacing="1" cellpadding="1" width="100%">
			        	  <tr>
					      <td colspan=6 width=100% >
							<asp:Label id=lblmsg visible=true Text="[Warning] !!! " ForeColor=red runat=server />
						  </td>
				           </tr>
				           <tr>
					       <td colspan=6 height="10%">			
					       <div id="div1" style="height: 200px;width:800;overflow: auto;">				
					          <asp:DataGrid ID="dgmsg" runat="server" 
                               CellPadding="2" GridLines="None" Width="100%" AutoGenerateColumns=false>
                              <AlternatingItemStyle CssClass="mr-r" />
                              <ItemStyle CssClass="mr-l" />
                              <HeaderStyle CssClass="mr-h" />
                              <Columns>
                                <asp:TemplateColumn HeaderText="Keterangan">
									<ItemTemplate>
										<%#Container.DataItem("Ket")%>
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
				
				<tr>
                    <td colspan="4">
       				     <div id="divnotmatch" visible="False" runat="server">
       				     <table border="0" cellspacing="1" cellpadding="1" width="100%">
			        	  <tr>
					      <td colspan=6 width=100% >
							<asp:Label id=lblnotmatch visible=true Text="[Warning] HK Absensi dengan HK BKM berbeda, Silakan dicek !!! " ForeColor=red runat=server />
						  </td>
				           </tr>
				           <tr>
					       <td colspan=6 height="10%">			
					       <div id="div1" style="height: 200px;width:800;overflow: auto;">				
					          <asp:DataGrid ID="dgnotmatch" runat="server" 
                               CellPadding="2" GridLines="None" Width="100%" AutoGenerateColumns=false>
                              <AlternatingItemStyle CssClass="mr-r" />
                              <ItemStyle CssClass="mr-l" />
                              <HeaderStyle CssClass="mr-h" />
                              <Columns>
                                <asp:TemplateColumn HeaderText="NIK">
									<ItemTemplate>
										<%#Container.DataItem("CodeEmp")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Nama">
									<ItemTemplate>
										<%#Container.DataItem("EmpName")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Divisi">
									<ItemTemplate>
										<%#Container.DataItem("IDDIV")%>
									</ItemTemplate>
								</asp:TemplateColumn>
							  
								<asp:TemplateColumn HeaderText="HK BKM T1">
									<ItemTemplate>
										<%#Container.DataItem("T1HKBKM")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								 <asp:TemplateColumn HeaderText="HK ABSENSI T1">
									<ItemTemplate>
										<%#Container.DataItem("T1HKABSEN")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="HK BKM T2">
									<ItemTemplate>
										<%#Container.DataItem("T2HKBKM")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="HK ABSENSI T2">
									<ItemTemplate>
										<%#Container.DataItem("T2HKABSEN")%>
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
				
                <tr>
                    <td colspan="4">
       				     <div id="divprocess" visible="False" runat="server">
       				     <table border="0" cellspacing="1" cellpadding="1" width="100%">
			        	  <tr>
					      <td colspan=6 width=100% class="mb-c">
						        <table width="100%" cellspacing="0" cellpadding="3" border="0" align="center">
							    <tr class="mb-t">
								    <td width="10%" height="26" valign=bottom>NIK :<BR><asp:TextBox id=txtEmpCode width=100% maxlength="50" runat="server" /></td>
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
								<asp:TemplateColumn HeaderText="GaPok">
									<ItemTemplate>
										<%#Container.DataItem("Gapok")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Premi">
									<ItemTemplate>
										<%#Container.DataItem("Premi")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Tunj">
									<ItemTemplate>
										<%#Container.DataItem("tunjangan")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="SubGaji">
									<ItemTemplate>
										<%#Container.DataItem("SubGaji")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Astek">
									<ItemTemplate>
										<%#Container.DataItem("astektg")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="JHT">
									<ItemTemplate>
										<%#Container.DataItem("jhttg")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Gaji.Kotot">
									<ItemTemplate>
										<%#Container.DataItem("gajikotor")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Pot.Pinj" HeaderStyle-ForeColor=red>
									<ItemTemplate>
										<%#Container.DataItem("potgajikecil")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Pot.Astek" HeaderStyle-ForeColor=red>
									<ItemTemplate>
										<%#Container.DataItem("potastek")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Pot.Spsi" HeaderStyle-ForeColor=red>
									<ItemTemplate>
										<%#Container.DataItem("potspsi")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Pot.Angsur" HeaderStyle-ForeColor=red>
									<ItemTemplate>
										<%#Container.DataItem("potlain")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Pot.Tot" HeaderStyle-ForeColor=red>
									<ItemTemplate>
										<%#Container.DataItem("totpot")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Total">
									<ItemTemplate>
										<%#Container.DataItem("totgaji")%>
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
