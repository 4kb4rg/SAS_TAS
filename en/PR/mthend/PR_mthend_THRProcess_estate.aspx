<%@ Page Language="vb" src="../../../include/PR_mthend_THRProcess_estate.aspx.vb" Inherits="PR_mthend_THRProcess_estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRMthEnd" src="../../menu/menu_PRMthEnd.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Pinjaman Process</title>
		
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<form id=frmMain runat=server>
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." ForeColor="red" runat=server />
			<table border="0" cellpadding="0" cellspacing="0" width="100%">
				<%--<tr>
					<td colspan=4>
						<UserControl:MenuPRMthEnd id=MenuPRMthEnd runat="server" />
					</td>
				</tr>--%>
				<tr>
					<td colspan=4 class="mt-h" width="100%" >PROSES T.H.R</td>
				</tr>
				<tr>
					<td colspan=4><hr size="1" noshade></td>
				</tr>
				
				<tr>
					<td colspan=4 height=25>&nbsp;</td>
				</tr>
				<tr valign=top>
					<td height=25 width=20%>Period :</td>
					<td width=50%>	<asp:DropDownList id="ddlMonth" width="20%" runat=server>
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
									<asp:DropDownList id=ddlyear width="20%" runat="server" /></td>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr valign=top>
					<td height=25 width=20%>Agama :</td>
					<td width=50%>
					<asp:DropDownList id="ddlreligion" width="20%" runat=server /></td>
					<td colspan=2>&nbsp;</td>
				</tr>
							
				<tr valign=top>
					<td height=25 width=20%>
                        Tgl Cut Off :</td>
					<td width=50%>	
  					    <asp:TextBox ID="txthrdate" runat=server MaxLength="10" Width="20%"></asp:TextBox>
                        <a href="javascript:PopCal('txthrdate');"><asp:Image id="Image1" runat="server" ImageUrl="../../images/calendar.gif"/></a>
					</td>
					<td colspan=2>&nbsp;</td>
				</tr>
				
				<tr valign=top>
					<td height=25 width=20%>Uang Daging :</td>
					<td width=50%>	
                        <asp:TextBox ID="txtdaging" runat="server" Width="20%" onkeypress="javascript:return isNumberKey(event)"></asp:TextBox></td>
					<td colspan=2>&nbsp;</td>
				</tr>
				
				<tr>
					<td colspan=4 style="height: 19px">&nbsp;</td>
				</tr>
				<tr>
					<td colspan=4>&nbsp;<asp:ImageButton id=btnGenerate onclick=btnGenerate_Click imageurl="../../images/butt_process.gif" alternatetext="Generate" runat=server /></td>
				</tr>
				<tr>
                    <td colspan="4">
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
       				     <div id="divprocess" visible="False" runat="server">
       				     <table border="0" cellspacing="1" cellpadding="1" width="100%">
				           <tr>
					       <td colspan=6 height="10%">			
					       <div id="div1" style="height: 200px;width:817px;overflow: auto;">				
					          <asp:DataGrid ID="dgProcess" runat="server" 
                               CellPadding="2" GridLines="None" Width="100%" AutoGenerateColumns=false>
                              <AlternatingItemStyle CssClass="mr-r" />
                              <ItemStyle CssClass="mr-l" />
                              <HeaderStyle CssClass="mr-h" />
                              <Columns>
                               <asp:TemplateColumn HeaderText="ID" >
									<ItemTemplate>
									    <%#Container.DataItem("THRId")%>
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
							
								<asp:TemplateColumn HeaderText="Divisi">
									<ItemTemplate>
										<%#Container.DataItem("Divname")%>
									</ItemTemplate>
								</asp:TemplateColumn>
							
								<asp:TemplateColumn HeaderText="Status">
									<ItemTemplate>
										<%#Container.DataItem("TyEmp")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Tanggungan">
									<ItemTemplate>
										<%#Container.DataItem("TyTunj")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="TMK">
									<ItemTemplate>
										<%#Container.DataItem("tmk")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								
							   <asp:TemplateColumn HeaderText="Gapok">
									<ItemTemplate>
										<%#Container.DataItem("gapok")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Catu Beras">
									<ItemTemplate>
										<%#Container.DataItem("Berasrp")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="P.Tetap/Tunjangan">
									<ItemTemplate>
										<%#Container.DataItem("Premi")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="THR">
									<ItemTemplate>
										<%#Container.DataItem("thr")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Daging">
									<ItemTemplate>
										<%#Container.DataItem("daging")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Total Terima">
									<ItemTemplate>
										<%#Container.DataItem("Total")%>
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
