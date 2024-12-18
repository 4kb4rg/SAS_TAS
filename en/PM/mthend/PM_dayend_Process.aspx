<%@ Page Language="vb" src="../../../include/PM_DayEnd_Process.aspx.vb" Inherits="PM_dayend_Process" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPMMthEnd" src="../../menu/menu_PMMthEnd.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Mill Production Day End Process</title>
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
	<body>
		<form id=frmMain runat=server>
		<table border="0" width="100%" cellpadding="1" cellspacing="0">
			<tr>
				<td colspan=5 align=center><UserControl:MenuPMMthEnd id=MenuPMMthEnd runat="server" /></td>
			</tr>
			<tr>
				<td class="mt-h" colspan=5>MILL PRODUCTION DAY END PROCESS</td>
			</tr>
			<tr>
				<td colspan=5><hr size="1" noshade></td>
			</tr>
			<tr>
				<td colspan=5 height=25>
					<font color=red>Important notes before day end process:</font><p>
					1. Please backup up the database before proceed.<br>
					2. Ensure no user is in the system.<br>
					3. All the required pre-day end reports have been generated.
				</td>
			</tr>
			<tr>
				<td colspan=5 height=25>&nbsp;</td>
			</tr>
			<tr>
				<td width=30% height=25>Current Accounting Period :</td> 
				<td width=20%><asp:Label id=lblAccPeriod runat=server/></td>
				<td width=5%>&nbsp;</td>
				<td width=15%>&nbsp;</td>
				<td width=30%>&nbsp;</td>
			</tr>
			<tr>
				<td height=25>Today :</td> 
				<td><asp:label id=lbltoday runat=server/></td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25>Date to be processed :</td> 
				<td>
					<asp:TextBox id=txtDayEnd width=50% maxlength=12 runat=server/>
					<asp:RequiredFieldValidator id=rfvDayEnd display=dynamic runat=server text="<br>Please enter processing date." 
						ControlToValidate=txtDayEnd />
					<asp:Label id=lblErrDayEnd visible=false forecolor=red runat=server/>
					<asp:Label id=lblErrDate visible=false forecolor=red text="<br>You are not allowed to run a future day end process because there is no ticket being generated." runat=server/>
					<asp:Label id=lblSuccess visible=false forecolor=blue text="<br>Day end processed successfully." runat=server/>
				</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td colspan=5 height=25>
					<asp:Label id=lblErrProcess visible=false forecolor=red text="There are some errors when processing day end." runat=server/>
				&nbsp;</td>
			</tr>
			<tr>
				<td colspan=5>
					<asp:ImageButton id=btnProceed imageurl="../../images/butt_proceed_dayend.gif" AlternateText="Proceed with month end process" OnClick="btnProceed_Click" runat="server" />
				</td>
			</tr>
		</table>
		<asp:Label id=lblErrMesage visible=false Text="Error while initiating component." runat=server />
		</form>
	</body>