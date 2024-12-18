<%@ Page Language="vb" src="../../../include/PD_MthEnd_Process_Estate.aspx.vb" Inherits="PD_mthend_Process_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPDMthEnd" src="../../menu/menu_PDMthEnd.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Production Month End Process</title>
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
	<body>
		<form id=frmMain runat=server>
		<table border="0" width="100%" cellpadding="1" cellspacing="0">
			<%--<tr>
				<td colspan=5 align=center><UserControl:MenuPDMthEnd id=MenuPDMthEnd runat="server" /></td>
			</tr>--%>
			<tr>
				<td class="mt-h" colspan=5>
                    REKAP PRODUKSI KEBUN</td>
			</tr>
			<tr>
				<td colspan=5><hr size="1" noshade></td>
			</tr>
			<tr>
				<td colspan=5 height=25>&nbsp;</td>
			</tr>
			<tr>
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
									<asp:DropDownList id=ddlyear width="20%" maxlength="4" runat="server" /></td>
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
					<asp:ImageButton id=btnProceed imageurl="../../images/butt_proceed_mthend.gif" AlternateText="Proceed with month end process" OnClick="btnProceed_Click" runat="server" />
				</td>
			</tr>
		</table>
		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." ForeColor=red runat=server />
		</form>
	</body>
</html>
