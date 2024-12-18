<%@ Page Language="vb" src="../../../include/AP_MthEnd_Process.aspx.vb" Inherits="AP_mthend_Process" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuAPMthEnd" src="../../menu/menu_APMthEnd.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Account Payable Month End Process</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
	<body>
		<form id=frmMain runat=server class="main-modul-bg-app-list-pu" >

        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">


		<table class="font9Tahoma" border="0" cellspacing="3" cellpadding="1" width="100%">
			<tr>
				<td colspan=5 align=center><UserControl:MenuAPMthEnd id=MenuAPMthEnd runat="server" /></td>
			</tr>
			<tr>
				<td colspan=5><strong>ACCOUNT PAYABLE MONTH END PROCESS</strong> </td>
			</tr>
			<tr>
				<td colspan=5>
                    <hr style="width :100%" />
                </td>
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
			<tr>
				<td colspan=5 height=25>
					<asp:Label id=lblErrNotClose visible=false forecolor=red text="There are some modules waiting for month end process before Account Payable can be processed." runat=server/>
					<asp:Label id=lblErrProcess visible=false forecolor=red text="There are some errors when processing month end." runat=server/>
					<asp:Label id=lblErrInvoiceRcv visible=false forecolor=red text="There are some pending invoice receive documents waiting to confirm." runat=server/>
					<asp:Label id=lblErrDebitNote visible=false forecolor=red text="There are some pending debit note documents waiting to confirm." runat=server/>
					<asp:Label id=lblErrCreditNote visible=false forecolor=red text="There are some pending credit note documents waiting to confirm." runat=server/>
					<asp:Label id=lblErrPayment visible=false forecolor=red text="There are some pending payment documents waiting to confirm." runat=server/>
					<asp:Label id=lblErrCreditorJournal visible=false forecolor=red text="There are some pending creditor journal documents waiting to confirm." runat=server/>
				    <asp:Label id=lblErrCurr visible=false forecolor=red text="Transaction use foreign currency, please setting last month currency rate." runat=server/>
				&nbsp;</td>
			</tr>
			<tr>
				<td colspan=5>
					<asp:ImageButton id=btnProceed imageurl="../../images/butt_proceed_mthend.gif" AlternateText="Proceed with month end process" OnClick="btnProceed_Click" runat="server" />
				    <asp:Button ID="btnProceed2" runat="server" class="button-small" 
                        Text="Proceed with month end process" Width="216px" />
                </td>
			</tr>
			<tr>
				<td colspan=5>
					&nbsp;</td>
			</tr>
		</table>
		<asp:Label id=lblErrMesage visible=false Text="Error while initiating component." runat=server />


        <br />
        </div>
        </td>
        </tr>
        </table>

		</form>
	</body>
</html>
