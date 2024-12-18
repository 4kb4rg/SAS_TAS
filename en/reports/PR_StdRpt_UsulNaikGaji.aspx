<%@ Page Language="vb" src="../../include/reports/PR_StdRpt_UsulNaikGaji.aspx.vb" Inherits="PR_StdRpt_UsulNaikGaji" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="PR_STDRPT_SELECTION_CTRL" src="../include/reports/PR_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Payroll - Usulan Kenaikan Gaji </title>
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" ID="frmMain">
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1">
				<tr>
					<td class="mt-h" colspan="3">PAYROLL - USULAN KENAIKAN GAJI</td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6"><hr size="1" noshade></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6"><UserControl:PR_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>			
				<tr>
					<td colspan="6"><hr size="1" noshade></td>
				</tr>
			</table>
			<table width=100% border="0" cellspacing="1" cellpadding="1" ID="Table2" runat=server>	
				<tr>
					<td colspan=4><asp:Label id="lblLocation" visible="false" runat="server" />
					<asp:Label id="lblDept" visible="false" runat="server" />
					<asp:Label id="lblCurrYear" visible="false" runat="server" />
					</td>
				</tr>
				<tr>
					<td width="25%">Year</td>
					<td width="5%">:</td>
					<td colspan="2"><asp:TextBox id="tbxYear" columns="4" maxlength="4" runat="server" />
					<asp:Label id="lblErrYear" visible="false" forecolor=red Text="Please insert year" runat="server" />
					</td>					
				</tr>
				<tr>
					<td width="25%">Person in Charge 1</td>
					<td width="5%">:</td>
					<td colspan="2"><asp:TextBox id="tbxPIC1" width="60%" runat="server" /></td>					
				</tr>
				<tr>
					<td width="25%">Designation 1</td>
					<td width="5%">:</td>
					<td colspan="2"><asp:TextBox id="tbxDes1" width="60%" runat="server" /></td>					
				</tr>
				<tr>
					<td width="25%">Person in Charge 2</td>
					<td width="5%">:</td>
					<td colspan="2"><asp:TextBox id="tbxPIC2" width="60%" runat="server" /></td>					
				</tr>
				<tr>
					<td width="25%">Designation 2</td>
					<td width="5%">:</td>
					<td colspan="2"><asp:TextBox id="tbxDes2" width="60%" runat="server" /></td>					
				</tr>
				<tr>
					<td colspan=4>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:ImageButton id="PrintPrev" AlternateText="Print Preview" imageurl="../images/butt_print_preview.gif" onClick="btnPrintPrev_Click" runat="server" /></td>					
				</tr>				
			</table>
		</form>
		<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" />
	</body>
</HTML>
