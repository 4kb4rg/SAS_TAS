<%@ Page Language="vb" src="../../../include/CT_MthEnd_Process.aspx.vb" Inherits="CT_mthend_Process" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuCTMthEnd" src="../../menu/menu_CTMthEnd.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Canteen Month End Process</title>
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
	<body>
		<form id=frmMain runat=server>
		<table border="0" width="100%" cellpadding="1" cellspacing="0">
			<tr>
				<td colspan=5 align=center><UserControl:MenuCTMthEnd id=MenuCTMthEnd runat="server" /></td>
			</tr>
			<tr>
				<td class="mt-h" colspan=5>CANTEEN MONTH END PROCESS</td>
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
			<!--start tengkar fileset-->
			<tr>
				<td colspan=5 height=25>
					<asp:Label id=lblErrNotClose visible=false forecolor=red text="There are some modules waiting for month end process before Canteen can be processed." runat=server/>
					<asp:Label id=lblErrProcess visible=false forecolor=red text="There are some errors when processing month end." runat=server/>
					<asp:Label id=lblErrStockReceive visible=false forecolor=red text="There are some pending canteen receive documents waiting to confirm." runat=server/>
					<asp:Label id=lblErrStockReturnAdvice visible=false forecolor=red text="There are some pending canteen return advice documents waiting to confirm." runat=server/>
					<asp:Label id=lblErrStockAdjustment visible=false forecolor=red text="There are some pending canteen adjustment documents waiting to confirm." runat=server/>
					<asp:Label id=lblErrStockIssue visible=false forecolor=red text="There are some pending canteen issue documents waiting to confirm." runat=server/>
					<asp:Label id=lblErrStockReturn visible=false forecolor=red text="There are some pending canteen return documents waiting to confirm." runat=server/>
					<asp:Label id=lblErrNoItemAcc visible=false forecolor=red text="There are some items have no account code." runat=server/>
					<asp:Label id=lblErrNoDblEntryAcc visible=false forecolor=red text="There are some GL double entry setup have no account code. Please complete the setup in GL Setup page." runat=server/>
				</td>
			</tr>
			<!--end tengkar fileset-->
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
