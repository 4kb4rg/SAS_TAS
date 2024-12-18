<%@ Page Language="vb" AutoEventWireup="false" src="../../include/reports/PR_StdRpt_Selection.aspx.vb" Inherits="PR_StdRpt_Selection" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="PR_StdRpt_Selection_Ctrl" src="../include/reports/PR_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Payroll Standard Reports</title>
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" ID="frmMain">
			<input type=Hidden id=hidUserLocPX runat="server" name="hidUserLocPX"/>
			<input type=Hidden id=hidAccMonthPX runat="server" name="hidAccMonthPX"/>			
			<input type=Hidden id=hidAccYearPX runat="server" name="hidAccYearPX"/>
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1">
				<tr>
					<td class="mt-h" colspan="3">PAYROLL STANDARD REPORTS</td>
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
			<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" /> 
		</form>
	</body>
</HTML>
