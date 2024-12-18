<%@ Page Language="vb" src="../../../include/FA_mthend_GenDepr.aspx.vb" Inherits="FA_mthend_GenDepr" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuFAMthEnd" src="../../menu/menu_FAMthEnd.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Generate Depreciation</title>
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<form id=frmProcess runat=server>
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<table border="0" cellpadding="0" cellspacing="0" width="100%">
				<tr>
					<td colspan=4>
						<UserControl:MenuFAMthEnd id=MenuFAMthEnd runat="server" />
					</td>
				</tr>
				<tr>
					<td class="mt-h" width="100%" colspan="4">GENERATE DEPRECIATION</td>
				</tr>
				<tr>
					<td colspan=4><hr size="1" noshade></td>
				</tr>
				<tr>
					<td colspan="4">
						- Click GENERATE button to create all the depreciation records for this accounting period.<br>
						- Click ROLLBACK button to rollback Depreciation generated.
					</td>
				</tr>
				<tr>
					<td colspan=4 height=25>&nbsp;</td>
				</tr>
				<tr>
					<td height=25 width=15%>Period :</td>
					<td width=15%><asp:Label id=lblPeriod runat=server/></td>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=4>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=4>
						<asp:Label id=lblNoProcessed text="There is no asset to be generated. Please ensure you have asset for depreciation before try to Generate." visible=false forecolor=red runat=server />
						<asp:Label id=lblProcessed text="All asset depreciation transactions successfully generated. You can now proceed with Confirm button if there is no amendment to the generated depreciation. " visible=false forecolor=blue runat=server />
						<asp:Label id=lblRollBack text="All asset depreciation transactions successfully confirmed. You can proceed to Month End Process." visible=false forecolor=blue runat=server />
						<asp:Label id=lblErr text="There are some errors when generating depreciation. Kindly contact system administrator for assistance." visible=false forecolor=red runat=server />
					</td>
				</tr>
				<tr>
					<td colspan=4>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=4><asp:ImageButton id=btnGenerate onclick=btnGenerate_Click imageurl="../../images/butt_generate.gif" alternatetext="Generate" runat=server />
						<asp:ImageButton id=btnRollBack onclick=btnRollBack_Click imageurl="../../images/butt_rollback.gif" alternatetext="Rollback" runat=server />
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
