<%@ Page Language="vb" src="../../../include/PR_mthend_dailyrollback.aspx.vb" Inherits="PR_mthend_dailyrollback" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRMthEnd" src="../../menu/menu_PRMthEnd.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Daily Process Rollback</title>
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<form id=frmMain runat=server>
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<table border="0" cellpadding="0" cellspacing="0" width="100%">
				<tr>
					<td colspan=4>
						<UserControl:MenuPRMthEnd id=MenuPRMthEnd runat="server" />
					</td>
				</tr>
				<tr>
					<td class="mt-h" width="100%" colspan="4">DAILY PROCESS ROLLBACK</td>
				</tr>
				<tr>
					<td colspan=4><hr size="1" noshade></td>
				</tr>
				<tr>
					<td width=20%>&nbsp;</td>
					<td width=30%>&nbsp;</td>
					<td width=25%>&nbsp;</td>
					<td width=25%>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="4">Note:&nbsp; Daily Process Rollback will rollback Harvester Incentive and Premi kerajinan.</td>
				</tr>
				<tr>
					<td colspan=4>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Period :</td>
					<td><asp:Label id=lblPeriod runat=server/></td>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr valign=Top>				
					<td>Process Date :*</td>
					<td>
						<asp:Textbox id=txtProDate width=50% maxlength=10 runat=server/>
						<a href="javascript:PopCal('txtProDate');"><asp:Image id="btnSelDate" runat="server" ImageUrl="../../images/calendar.gif"/></a>
						<asp:Label id=lblErrProcessDate visible=false forecolor=red runat=server/>
						<asp:Label id=lblErrProcessDateDesc visible=false forecolor=red text="<br>Date format " runat=server/>						
					</td>
				</tr>
				<tr>
					<td colspan=4>
						<asp:Label id=lblProcessed text="All the calculated data has been rollback to its original source. You can now continue editing transactions and run the Payroll Process again." visible=false forecolor=blue runat=server />
						<asp:Label id=lblErrProcessed text="There were some errors when system trying to rollback the calculated data. Kindly contact system administrator for assistance." visible=false forecolor=red runat=server />
					</td>
				</tr>
				<tr>
					<td colspan=4>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=4><asp:ImageButton id=btnGenerate onclick=btnGenerate_Click imageurl="../../images/butt_rollback.gif" alternatetext="Generate" runat=server /></td>
				</tr>
			</table>
		</form>
	</body>
</html>
