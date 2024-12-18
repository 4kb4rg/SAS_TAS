<%@ Page Language="vb" src="../../../include/PR_mthend_riceration_estate.aspx.vb" Inherits="PR_mthend_riceration" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRMthEnd" src="../../menu/menu_PRMthEnd.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Rice Ration</title>
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<form id=frmProcess runat=server>
			<asp:Label id=lblErrMessage visible=False Text="Error while initiating component." runat=server  ForeColor="Red" />
			<table border="0" cellpadding="0" cellspacing="0" width="100%">
				<tr>
					<td colspan=4 class="mt-h" width="100%" >CATU BERAS</td>
				</tr>
				<tr>
					<td colspan=4><hr size="1" noshade></td>
				</tr>
				
				<tr>
					<td colspan=4 height=25>&nbsp;</td>
				</tr>
				
				<tr valign=top>
					<td height=25 width=20%>Periode : *</td>
					<td style="width: 50%">
						<asp:DropDownList id="ddlMonth" width="20%" runat=server OnSelectedIndexChanged="ddltahap_OnSelectedIndexChanged"  AutoPostBack=true>
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
									<asp:DropDownList id=ddlyear width="20%" runat="server" />
					 </td>
					<td colspan=2>&nbsp;</td>
				</tr>
				
				<tr valign=top>
					<td height=25 width=20%></td>
					<td style="width: 50%">
					<asp:TextBox id=txt_dtstart width="20%" readonly=true maxlength="20" CssClass="mr-h" runat="server" />
                        s/d
                    <asp:TextBox id=txt_dtend width="20%" ReadOnly=true maxlength="20" CssClass="mr-h" runat="server" />
					</td>				                    
				</tr>
				
				<tr valign=top>
					<td height=25 width=20%>Process Type :</td>
					<td style="width: 30%">
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
					<td height=25 width=20%>Employee Code :</td>
					<td style="width: 50%"><GG:AutoCompleteDropDownList id=ddlEmployee enabled=false width=40% runat=server/></td>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=4>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=4>																
						<asp:ImageButton id=btnGenerate onclick=btnGenerate_Click imageurl="../../images/butt_process.gif" alternatetext="Generate" runat=server />
					</td>
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
								    <td width="10%" height="26" valign=bottom>Employee Code :<BR><asp:TextBox id=txtEmpCode width=100% maxlength="8" runat="server" /></td>
								    <td width="10%" height="26" valign=bottom>Employee name :<BR><asp:TextBox id=txtEmpName width=100% maxlength="15" runat="server" /></td>
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
								<asp:TemplateColumn HeaderText="Tgng">
									<ItemTemplate>
										<%#Container.DataItem("Tg")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Divisi">
									<ItemTemplate>
										<%#Container.DataItem("divname")%>
									</ItemTemplate>
								</asp:TemplateColumn>
							   <asp:TemplateColumn HeaderText="Tot.Hk">
									<ItemTemplate>
										<%#Container.DataItem("TotHk")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Tot.KG">
									<ItemTemplate>
										<%#Container.DataItem("TotKg")%>
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
