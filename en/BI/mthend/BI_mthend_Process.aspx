<%@ Page Language="vb" src="../../../include/BI_MthEnd_Process.aspx.vb" Inherits="BI_mthend_Process" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuBIMthEnd" src="../../menu/menu_BIMthEnd.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Account Receivables Month End Process</title>
           <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
	<body>
		<form id=frmMain class="main-modul-bg-app-list-pu" runat=server>
           <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 

		<table border="0" width="100%" cellpadding="1" cellspacing="0" class="font9Tahoma">
			<tr>
				<td colspan=5 align=center><UserControl:MenuBIMthEnd id=MenuBIMthEnd runat="server" /></td>
			</tr>
			<tr>
				<td  colspan=5><strong>ACCOUNT RECEIVABLES MONTH END PROCESS</strong> </td>
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
			<!--tr>
				<td colspan=5 height=25>
					<asp:CheckBox id=cbIV text=" Automatic confirming all active Invoice" runat=server /><br>
					<asp:CheckBox id=cbDN text=" Automatic confirming all active Debit Note" runat=server /><br>
					<asp:CheckBox id=cbCN text=" Automatic confirming all active Credit Note" runat=server /><br>
					<asp:CheckBox id=cbRC text=" Automatic confirming all active Receipt" runat=server /><br>
					<asp:CheckBox id=cbDJ text=" Automatic confirming all active Debtor Journal" runat=server /><br>
				</td> 
			</tr-->
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
					<asp:Label id=lblErrNotClose visible=false forecolor=red text="There are some modules waiting for month end process before Account Receivables can be processed." runat=server/>
					<asp:Label id=lblErrProcess visible=false forecolor=red text="There are some errors when processing month end." runat=server/>
					<asp:Label id=lblErrInvoice visible=false forecolor=red text="There are some pending invoice documents waiting to confirm/print." runat=server/>
					<asp:Label id=lblErrDebitNote visible=false forecolor=red text="There are some pending debit note documents waiting to confirm/print." runat=server/>
					<asp:Label id=lblErrCreditNote visible=false forecolor=red text="There are some pending credit note documents waiting to confirm/print." runat=server/>
					<asp:Label id=lblErrReceipt visible=false forecolor=red text="There are some pending receipt documents waiting to confirm/print." runat=server/>
					<asp:Label id=lblErrDebtorJournal visible=false forecolor=red text="There are some pending debtor journal documents waiting to confirm/print." runat=server/>
				</td>
			</tr>
			<tr>
				<td colspan=5>
					<asp:ImageButton id=btnProceed imageurl="../../images/butt_proceed_mthend.gif" AlternateText="Proceed with month end process" OnClick="btnProceed_Click" runat="server" />
				&nbsp;</td>
			</tr>
		</table>
		<asp:Label id=lblErrMesage visible=false Text="Error while initiating component." runat=server />
            </div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</html>
