<%@ Page Language="vb" src="../../include/reports/PR_StdRpt_TaxDeductList.aspx.vb" Inherits="PR_StdRpt_TaxDeductList" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="PR_STDRPT_SELECTION_CTRL" src="../include/reports/PR_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Payroll - Tax Deduction Listing</title>
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" ID="frmMain">
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1">
				<tr>
					<td class="mt-h">PAYROLL - TAX DEDUCTION LISTING</td>
					<td align="right"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="2"><hr size="1" noshade></td>
				</tr>
				<tr>
					<td colspan="2">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="2"><UserControl:PR_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="2>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6"><asp:Label id="lblDate" forecolor=red visible="false" text="Incorrect Date Format. Date Format is " runat="server" /></td>
				</tr>
				<tr>
					<td colspan="2"><hr size="1" noshade></td>
				</tr>
			</table>
			<table width="100%" border="0" cellspacing="1" cellpadding="1" ID="Table2" runat="server">
				<tr>
					<td width=25%>Employee Code From : </td>
					<td width=39%><asp:Textbox id=txtEmpCodeFrom maxlength=20 width=50% runat=server/> (leave blank for all)</td>			
					<td width=4%>To : </td>	
					<td width=40%><asp:Textbox id=txtEmpCodeTo maxlength=20 width=50% runat=server/> (leave blank for all)</td>
				</tr>
				<tr>
					<td>Gang Code : </td>
					<td><asp:textbox id=txtGangCode maxlength=50 width=50% runat=server /></td>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr>
					<td >Suppress Zero Tax Amount : </td>
					<td ><asp:RadioButton id="rbSuppYes" text="Yes" GroupName="rbSupp" runat="server" />		
						 <asp:RadioButton id="rbSuppNo" text="No" checked="true" GroupName="rbSupp" runat="server" /></td>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td colspan=3><asp:Label id="lblLocation" visible="false" runat="server" /></td>
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td colspan=3>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="4"><asp:ImageButton id="PrintPrev" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" /></td>
				</tr>
			</table>
		</form>
		<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" />
	</body>
</HTML>
