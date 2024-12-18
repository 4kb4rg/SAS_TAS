<%@ Page Language="vb" src="../../../include/FA_mthend_Process.aspx.vb" Inherits="FA_mthend_Process" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Fixed Asset Month End Process</title>
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
	<body>
		<form id=frmMain runat=server>
		<table border="0" width="100%" cellpadding="1" cellspacing="0">
			
			<tr>
				<td class="mt-h" colspan=5>FIXED ASSET MONTH END PROCESS</td>
			</tr>
			<tr>
				<td colspan=5><hr size="1" noshade></td>
			</tr>
			<tr>
				<td colspan=5 height=25>
					<font color=red>Important notes before month end process:</font><p>
					- Click PROCEED FOR MONTH END CLOSE to generate Depresiation, History and Journal.<br>
					- Click ROLLBACK to rollback Month End Process.<br>
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
					<asp:Label id=lblErrNotClose visible=false forecolor=red text="There are some modules waiting for month end process before Fixed Asset can be proceeded." runat=server/>
					<asp:Label id=lblErrProcess visible=false forecolor=red text="There are some errors when processing month end. Kindly contact system administrator for assistance." runat=server/>
					<asp:Label id=lblErrDocument visible=false forecolor=red text="Month end process terminated due to some document yet to be confirmed. Kindly confirm all depreciation transaction and try again." runat=server/>
				&nbsp;</td>
			</tr>
			<tr>
				<td colspan=5>
					<asp:ImageButton id=btnProceed imageurl="../../images/butt_proceed_mthend.gif" AlternateText="Proceed with month end process" OnClick="btnProceed_Click" runat="server" />
				    <asp:ImageButton id=btnRollBack onclick=btnRollBack_Click imageurl="../../images/butt_rollback.gif" alternatetext="Rollback" runat=server />
				</td>
			</tr>
		</table>
		<asp:Label id=lblErrMesage visible=false Text="Error while initiating component." runat=server />
		</form>
	</body>
</html>
