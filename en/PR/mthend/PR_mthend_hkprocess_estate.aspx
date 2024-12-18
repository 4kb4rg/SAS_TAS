<%@ Page Language="vb" src="../../../include/PR_mthend_hkprocess_estate.aspx.vb" Inherits="PR_mthend_hkprocess" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRMthEnd" src="../../menu/menu_PRMthEnd.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Hk Process</title>
		
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<form id=frmProcess runat=server>
            &nbsp;<table border="0" cellpadding="0" cellspacing="0" width="100%">
				<%--<tr>
					<td colspan=4>
						<UserControl:MenuPRMthEnd id=MenuPRMthEnd runat="server" />
					</td>
				</tr>--%>
				<tr>
					<td colspan=4 class="mt-h" width="100%" >PROSES PERHITUNGAN HK</td>
				</tr>
				<tr>
					<td colspan=4><hr size="1" noshade></td>
				</tr>
				
				<tr>
					<td colspan=4 height=25>&nbsp;</td>
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
					<td colspan="5">
					<asp:CheckBox id="cbGenIssue" text=" Auto Generate Stock Issue <i>(Please make sure this location has inventory bin)</i> " checked="false" runat="server" /></td>
				</tr>
				<tr>
					<td colspan=4 style="height: 19px">&nbsp;</td>
				</tr>
				<tr>
					<td colspan=4>&nbsp;<asp:ImageButton id=btnGenerate onclick=btnGenerate_Click imageurl="../../images/butt_process.gif" alternatetext="Generate" runat=server />
					&nbsp;<asp:ImageButton id="btnIssue" UseSubmitBehavior="false" onClick="btnIssue_Click" ImageUrl="../../images/butt_gen_issue.gif" AlternateText="Generate Stock Issue" CausesValidation=False runat="server" />
					&nbsp;<asp:Button id=GenGR_SPKBtn Text="Generate GR SPK" OnClick="GenGR_SPKBtn_OnClick" runat="server"/>					
					</td>
				</tr>
				<tr>
                    <td colspan="4">
			        <asp:Label id=lblErrMessage visible=false Text="Error while initiating component."  runat=server />
			        </td>
                </tr>
                <tr>
                    <td colspan="4">
       				     <div id="divprocess" visible="False" runat="server">
                        </div>
                      </td>
                </tr>
				<tr>
				<td colspan=7>
     			 <div id="divgd" style="width:1050px;height:400px;overflow: auto;">

					<asp:DataGrid id=dgViewCek
						AutoGenerateColumns="false" width="100%" runat="server"
						GridLines=none
						Cellpadding="1"
						Pagerstyle-Visible="False"
						AllowSorting="false">	
						<HeaderStyle CssClass="mr-h"/>
						<ItemStyle CssClass="mr-l"/>
						<AlternatingItemStyle CssClass="mr-r"/>
						<Columns>
							<asp:TemplateColumn HeaderText="Divisi">
							    <ItemTemplate>
									<%# Container.DataItem("Div_out") %> 
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Item">
							    <ItemTemplate>
									<%# Container.DataItem("Item_out") %>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Nama">
							    <ItemTemplate>
									<%# Container.DataItem("Description") %> 
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Qty.Awal">
							    <ItemTemplate>
									<%# Container.DataItem("qtyawl") %> 
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Qty.In">
							    <ItemTemplate>
									<%# Container.DataItem("qty_in") %> 
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Qty.Out">
							    <ItemTemplate>
									<%# Container.DataItem("qty_out") %>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Qty.Selisih">
							    <ItemTemplate>
									<%# Container.DataItem("selisih") %>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Doc.id">
							    <ItemTemplate>
									<%# Container.DataItem("Docid") %> 
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Tgl">
							    <ItemTemplate>
									<asp:Label id=lblDate text='<%# objGlobal.GetLongDate(Container.DataItem("Docdate")) %>'  runat=server/>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Blok">
							    <ItemTemplate>
									<%# Container.DataItem("Docblok") %>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Qty">
							    <ItemTemplate>
									<%# Container.DataItem("Qty") %> 
								</ItemTemplate>
							</asp:TemplateColumn>
						</Columns>
					</asp:DataGrid>
				</div>
				 <asp:Button id=SubmitBtn Text="Export Excel"  OnClick="ExportBtn_OnClick" runat="server"/>
				</td>
			</tr>	

			</table>
		</form>
	</body>
</html>
