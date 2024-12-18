<%@ Page Language="vb" src="../../../include/PD_MthEnd_Process.aspx.vb" Inherits="PD_mthend_Process" %>
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
			<tr>
				<td colspan=5 align=center><UserControl:MenuPDMthEnd id=MenuPDMthEnd runat="server" /></td>
			</tr>
			<tr>
				<td class="mt-h" colspan=5>PRODUCTION MONTH END PROCESS</td>
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
					<asp:Label id=lblErrNotClose visible=false forecolor=red text="There are some modules waiting for month end process before Production can be processed." runat=server/>
					<asp:Label id=lblErrProcess visible=false forecolor=red text="There are some errors when processing month end." runat=server/>
					<asp:Label id=lblErrPlantingRate visible=false forecolor=red text="There are some errors when processing month end. Please check Year of Planting Yield transaction for duplicated rate given in same year of planting; same group reference; or same date given." runat=server/>
					<asp:Label id=lblErrPlantingYear visible=false forecolor=red text="There are some errors when processing month end. Some data inconsistency found betwen Payroll Piece Rate Payment and Production Year of Planting Yield. i.e. some costing in Piece Rate Payment has no Year of Planting rate, or some Year of Planting rate has no Piece Rate Payment costing." runat=server/>
				&nbsp;</td>
			</tr>
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
