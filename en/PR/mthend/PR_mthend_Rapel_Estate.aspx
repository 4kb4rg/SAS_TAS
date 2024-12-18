<%@ Page Language="vb" src="../../../include/PR_MthEnd_Rapel_Estate.aspx.vb" Inherits="PR_mthend_Rapel_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRMthEnd" src="../../menu/menu_PRMthEnd.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Payroll Rapel Process</title>
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
	<body>
		<form id=frmMain runat=server>
		<table border="0" width="100%" cellpadding="1" cellspacing="0">
			<%--<tr>
				<td colspan=5 align=center><UserControl:MenuPRMthEnd id=MenuPRMthEnd runat="server" /></td>
			</tr>--%>
			<tr>
				<td class="mt-h" colspan=5>
                    RAPEL UPAH</td>
			</tr>
			<tr>
				<td colspan=5><hr size="1" noshade></td>
			</tr>
			<tr>
				<td colspan=5 height=25>&nbsp;</td>
			</tr>
			<tr>
				<td height=25 width=20%>Period :</td>
					<td width=50%>	<asp:DropDownList id=ddlyear width="20%" maxlength="4" runat="server" /></td>
					<td colspan=2>&nbsp;</td>
			</tr>
			<tr>
				<td width=20% height=25></td> 
				<td width=50%></td>
				<td colspan=2>&nbsp; &nbsp;</td>
			</tr>
			<tr>
				<td colspan=4 style="height: 19px">&nbsp;<asp:Label ID="lblNoRecord" runat="server" ForeColor="red" Text="No Record Created"
                            Visible="false"></asp:Label><asp:Label ID="lblSuccess" runat="server" ForeColor="blue"
                                Text="Process Success" Visible="false"></asp:Label><asp:Label ID="lblFailed" runat="server"
                                    ForeColor="red" Text="Process Failed" Visible="false"></asp:Label>&nbsp;
                 </td>
			</tr>
			<tr>
				<td colspan=5>
					<asp:ImageButton id=btnProceed onclick=btnProceed_Click imageurl="../../images/butt_process.gif" alternatetext="Generate" runat=server />
				</td>
			</tr>
			<tr>
				<td colspan=5>&nbsp;</td>
			</tr>
			<tr>
				<td colspan=6>
					<asp:DataGrid id=dgValidasi
						AutoGenerateColumns="false" width="100%" runat="server"
						GridLines=none
						Cellpadding="1"
						Pagerstyle-Visible="False"
						AllowSorting="false">	
						<HeaderStyle CssClass="mr-h"/>
						<ItemStyle CssClass="mr-l"/>
						<AlternatingItemStyle CssClass="mr-r"/>
						<Columns>
							<asp:TemplateColumn HeaderText="Warning">
							    <ItemStyle/> 
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("Ket") %> runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
						</Columns>
					</asp:DataGrid>
				</td>
			</tr>	
							
		</table>
		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." ForeColor=red runat=server />
		</form>
	</body>
</html>
