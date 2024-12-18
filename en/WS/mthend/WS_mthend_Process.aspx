<%@ Page Language="vb" src="../../../include/WS_MthEnd_Process.aspx.vb" Inherits="WS_mthend_Process" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuWSMthEnd" src="../../menu/menu_WSMthEnd.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Workshop Month End Process</title>
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
	<body>
		<form id=frmMain runat=server>
		<table border="0" width="100%" cellpadding="1" cellspacing="0">
			<tr>
				<td colspan=5 align=center><UserControl:MenuWSMthEnd id=MenuWSMthEnd runat="server" /></td>
			</tr>
			<tr>
				<td class="mt-h" colspan=5>WORKSHOP MONTH END PROCESS</td>
			</tr>
			<tr>
				<td colspan=5><hr size="1" noshade></td>
			</tr>
			<tr>
				<td colspan=5 height=25>
					<font color=red>Important notes before month end process:</font><p>
					1. Please backup up the database before proceed.<br>
					2. Ensure no user is in the system.<br>
					3. All the required pre-month end reports have been generated.
				</td>
			</tr>
			<tr>
				<td colspan=5 height=25>&nbsp;</td>
			</tr>
			<tr>
				<td width=20% height=25>Month End Accounting Period :</td> 
				<td width=30%><asp:Label id=lblAccPeriod runat=server/></td>
				<td width=5%>&nbsp;</td>
				<td width=15%>&nbsp;</td>
				<td width=30%>&nbsp;</td>
			</tr>
			<tr>
				<td width=20% height=25>Status :</td> 
				<td width=30%><asp:Label id=lblStatus runat=server/></td>
				<td width=5%>&nbsp;</td>
				<td width=15%>&nbsp;</td>
				<td width=30%>&nbsp;</td>
			</tr>
			<tr>
				<td height=25>Last Processed Date :</td> 
				<td><asp:Label id=lblLastProcessDate runat=server/></td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td colspan=5 height=25>
					<asp:Label id=lblActionResult visible=false forecolor=red text="" runat=server/>
				&nbsp;</td>
			</tr>
			<tr>
				<td colspan=5>
					<asp:ImageButton id=btnProceed imageurl="../../images/butt_proceed_mthend.gif" AlternateText="Proceed with month end process" OnClick="btnProceed_Click" runat="server" />
				</td>
			</tr>
		</table>
		<asp:Label id=lblErrMesage visible=false Text="Error while initiating component." runat=server />
		<asp:Label id=lblCode visible=false text=" Code" runat=server/>
		<asp:Label id=lblBlkCodeTag visible=false runat=server/>
		<asp:Label id=lblAccCodeTag visible=false runat=server/>
		<asp:Label id=lblVehCodeTag visible=false runat=server/>
		<asp:Label id=lblVehExpCodeTag visible=false runat=server/>
		<asp:Label id=lblBillPartyCode visible=false runat=server/>
		<asp:Label id=lblWorkCodeTag visible=false runat=server/>
		</form>
	</body>
</html>
