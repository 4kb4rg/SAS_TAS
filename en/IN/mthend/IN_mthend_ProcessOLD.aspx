<%@ Page Language="vb" src="../../../include/IN_MthEnd_Process.aspx.vb" Inherits="IN_mthend_Process" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuINMthEnd" src="../../menu/menu_INMthEnd.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Inventory Month End Process</title>
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
	<body>
		<form id=frmMain runat=server>
		<table border="0" width="100%" cellpadding="1" cellspacing="0">
			<tr>
				<td colspan=5 align=center><UserControl:MenuINMthEnd id=MenuINMthEnd runat="server" /></td>
			</tr>
			<tr>
				<td class="mt-h" colspan=5>INVENTORY MONTH END PROCESS</td>
			</tr>
			<tr>
				<td colspan=5><hr size="1" noshade></td>
			</tr>
			<tr>
				<td colspan=5 height=25>
					<font color=red>Important notes before month end process:</font><p>
					1. Please backup up the database before proceed.<br>
					2. Ensure no user is in the system.<br>
					3. All the required pre-month end reports have been generated.<br>
					4. Automate the confirmation process for documents.
				</td>
			</tr>
			<tr>
				<td colspan=5 height=25>&nbsp;</td>
			</tr>
			<!--tr>
				<td colspan=5 height=25>
					<asp:CheckBox id=cbSR text=" Automatic confirming all active Stock Receive" runat=server /><br>
					<asp:CheckBox id=cbSRA text=" Automatic confirming all active Stock Return Advice" runat=server /><br>
					<asp:CheckBox id=cbSA text=" Automatic confirming all active Stock Adjustment" runat=server /><br>
					<asp:CheckBox id=cbST text=" Automatic confirming all active Stock Transfer" runat=server /><br>
					<asp:CheckBox id=cbSI text=" Automatic confirming all active Stock Issue" runat=server /><br>
					<asp:CheckBox id=cbSRT text=" Automatic confirming all active Stock Return" runat=server /><br>
					<asp:CheckBox id=cbFI text=" Automatic confirming all active Fuel Issue" runat=server />
				</td> 
			</tr>
			<tr>
				<td colspan=5 height=25>&nbsp;</td>
			</tr-->
			<tr>
				<td width=20% height=25>Month End Accounting Period :</td> 
				<td width=30%><asp:Label id=lblAccPeriod runat=server/></td>
				<td width=5%>&nbsp;</td>
				<td width=15%>&nbsp;</td>
				<td width=30%>&nbsp;</td>
			</tr>
			<tr>
				<td height=25>Status :</td> 
				<td><asp:Label id=lblStatus runat=server/></td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25>Last Processed Date :</td> 
				<td><asp:Label id=lblLastProcessDate runat=server/></td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
			    <td align="left">Closing Method?</td>
			    <td align="left">
					<asp:RadioButton id="CloseInd_Final" 
						Checked="False"
						GroupName="CloseInd"
						Text="Final"
						TextAlign="Right"
						AutoPostBack="True"
						oncheckedchanged=CloseInd_OnCheckChange 
						Enabled=false
						runat="server" /></td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td>&nbsp;</td>
				<td>
					<asp:RadioButton id="CloseInd_Temporary" 
						Checked="True"
						GroupName="CloseInd"
						Text="Temporary"
						TextAlign="Right"
						AutoPostBack="True"
						oncheckedchanged=CloseInd_OnCheckChange 
						runat="server" /></td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td>&nbsp;</td>
				<td>
					<asp:RadioButton id="CloseInd_RollBack" 
						Checked="false"
						GroupName="CloseInd"
						Text="Rollback Temporary"
						TextAlign="Right"
						AutoPostBack="True"
						oncheckedchanged=CloseInd_OnCheckChange 
						runat="server" /></td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<!--start tengkar fileset-->
			<tr>
				<td colspan=5 height=25>
					<asp:Label id=lblErrNotClose visible=false forecolor=red text="There are some modules waiting for month end process before Inventory can be processed." runat=server/>
					<asp:Label id=lblErrProcess visible=false forecolor=red text="There are some errors when processing month end." runat=server/>
					<asp:Label id=lblErrStockReceive visible=false forecolor=red text="There are some pending stock receive documents waiting to confirm." runat=server/>
					<asp:Label id=lblErrStockReturnAdvice visible=false forecolor=red text="There are some pending stock return advice documents waiting to confirm." runat=server/>
					<asp:Label id=lblErrStockAdjustment visible=false forecolor=red text="There are some pending stock adjustment documents waiting to confirm." runat=server/>
					<asp:Label id=lblErrStockIssue visible=false forecolor=red text="There are some pending stock issue documents waiting to confirm." runat=server/>
					<asp:Label id=lblErrStockReturn visible=false forecolor=red text="There are some pending stock return documents waiting to confirm." runat=server/>
					<asp:Label id=lblErrFuelIssue visible=false forecolor=red text="There are some pending fuel issue documents waiting to confirm." runat=server/>
					<asp:Label id=lblErrNoItemAcc visible=false forecolor=red text="There are some items have no account code." runat=server/>
					<asp:Label id=lblErrNoLocAcc visible=false forecolor=red text="There are some locations have no account code." runat=server/>
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
