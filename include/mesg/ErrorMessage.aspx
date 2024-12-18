<%@ Page Language="vb" src="../mesg_error_message.aspx.vb" Inherits="error_message" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuError" src="../../en/menu/menu_error.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../en/include/preference/preference_handler.ascx"%>
<HTML>
	<HEAD>
		<title>Error Message</title>
	</HEAD>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
			<form id="frmError" runat="server">
				<table border=0 cellspacing="1" width="100%">
					<tr>
						<td colspan=2>
							<UserControl:MenuError id=MenuError runat="server" />
						</td>
					</tr>
					<tr>
						<td class="BigBold" align="left"><asp:Label id=lblErrHeader runat=server/></td>
						<td>&nbsp;</td>
					</tr>
					<tr>
						<td colspan=2><hr size="1" noshade></td>
					</tr>
					<tr>
						<td>&nbsp;</td>
						<td>&nbsp;</td>
					</tr>
					<tr>
						<td colspan=2 align=left>
							<table width=100% border=0 cellpadding=0>
								<tr>
									<td valign=top class="NormalBold" align="left" width=20%><asp:Label id=lblErrCodeName runat=server/> </td>
									<td valign=top align="left" width=20%><asp:Label id=lblErrCode forecolor=red runat=server /></td>
								</tr>
								<tr>
									<td colspan=2>&nbsp;</td>
								</tr>
								<tr>
									<td valign=top class="NormalBold" align="left"><asp:Label id=lblErrMesgName runat=server/> </td>
									<td valign=top align="left" width=80%><asp:Label id=lblErrMessage forecolor=red runat=server /></td>
								</tr>
								<tr>
									<td colspan=2>&nbsp;</td>
								</tr>
								<tr>
									<td></td>
									<td align=left><asp:Button Text="  OK  " ID="redirectBtn" OnClick="redirectBtn_Click" Runat="server" /></td>
								</tr>
							</table>
						</td>
					</tr>					
					<tr>
						<td>&nbsp;</td>
						<td>&nbsp;</td>
					</tr>
					<tr>
						<td>&nbsp;</td>
						<td>&nbsp;</td>						
					</tr>
				</table>
			</form>
	</body>
</HTML>

