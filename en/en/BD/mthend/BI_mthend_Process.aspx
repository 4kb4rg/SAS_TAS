<%@ Page Language="vb" src="../../../include/BI_MthEnd_Process.aspx.vb" Inherits="BI_mthend_Process" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuBIMthEnd" src="../../menu/menu_BIMthEnd.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Billing Month End Process</title>
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
	<body>
		<form id=frmMain runat=server>
		<table border="0" width="100%" cellpadding="1" cellspacing="0">
			<tr>
				<td colspan=5 align=center><UserControl:MenuBIMthEnd id=MenuBIMthEnd runat="server" /></td>
			</tr>
			<tr>
				<td class="mt-h" colspan=5>BILLING MONTH END PROCESS</td>
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
				<td colspan=5 height=25>
					<asp:CheckBox id=cbDN text=" Automatic confirming all active Debit Note" checked runat=server /><br>
					<asp:CheckBox id=cbCN text=" Automatic confirming all active Credit Note" checked runat=server /><br>
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
			<!--open tengkar fileset-->
			<tr>
				<td colspan=5 height=25>
					<asp:Label id=lblErrNotClose visible=false forecolor=red text="There are some modules waiting for month end process before Billing can be processed." runat=server/>
					<asp:Label id=lblErrProcess visible=false forecolor=red text="There are some errors when processing month end." runat=server/>
					<asp:Label id=lblErrDebitNote visible=false forecolor=red text="There are some pending debit note documents waiting to confirm." runat=server/>
					<asp:Label id=lblErrCreditNote visible=false forecolor=red text="There are some pending credit note documents waiting to confirm." runat=server/>
				</td>
			</tr>
			<!--close tengkar fileset-->
			<tr>
				<td colspan=5>
					<asp:ImageButton id=btnProceed imageurl="../../images/butt_proceed_mthend.gif" AlternateText="Proceed with month end process" OnClick="btnProceed_Click" runat="server" />
				</td>
			</tr>
		</table>
		<asp:Label id=lblErrMesage visible=false Text="Error while initiating component." runat=server />
		</form>
	</body>
</html>
