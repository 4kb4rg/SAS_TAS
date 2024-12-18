<%@ Page Language="vb" src="../../include/reports/PR_StdRpt_StmtTaxDeduct.aspx.vb" Inherits="PR_StdRpt_StmtTaxDeduct" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="PR_StdRpt_Selection_Ctrl" src="../include/reports/PR_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>EA - Statement of Tax Deduction</title>
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" ID="frmMain">
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1">
				<tr>
					<td class="mt-h" colspan="3">EA - STATEMENT OF TAX DEDUCTION</td>
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
					<td colspan="6"><asp:Label id="lblDate" forecolor=red visible="false" text="Incorrect Date Format. Date Format is " runat="server" /></td>
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
					<td width=20%>EA Year :</td>
					<td width=35%><asp:DropDownList id="lstEAYear" width="30%" runat="server" /></td>
					<td width=5%>&nbsp;</td>
					<td width=5%>&nbsp;</td>
					<td width=35%>&nbsp;</td>
				</tr>																				
				<tr>
					<td width=20%>Tax Branch :</td>
					<td width=35%><asp:DropDownList id="lstTaxBranch" width="100%" runat="server" /></td>
					<td width=5%>&nbsp;</td>
					<td width=5%>&nbsp;</td>
					<td width=35%>&nbsp;</td>
				</tr>		
				<tr>
					<td>Financial Year Ends On :</td>
					<td><asp:textbox id="txtPeriodEnd" width="50%" runat="server" />
  						<a href="javascript:PopCal('txtPeriodEnd');">
						<asp:Image id="btnSelPeriodEnd" runat="server" ImageUrl="../Images/calendar.gif"/></a></td>								
					<td colspan=3>&nbsp;</td>
				</tr>
				<tr>
					<td>Income Tax Reference No :</td>
					<td colspan=4><asp:TextBox id="txtRefNo" maxlength="100" width="50%" runat="server" /></td>
				</tr>
				<tr>
					<td>Nature of Business :</td>
					<td colspan=4><asp:TextBox id="txtNatureBuss" maxlength="100" width="50%" runat="server" /></td>
				</tr>
				<tr>
					<td>Signature Name :</td>
					<td colspan=4><asp:TextBox id="txtSignName" maxlength="100" width="50%" runat="server" /></td>
				</tr>
				<tr>
					<td>Signature NRIC :</td>
					<td colspan=4><asp:TextBox id="txtSignNRIC" maxlength="100" width="50%" runat="server" /></td>
				</tr>
				<tr>
					<td>Signature Designation :</td>
					<td colspan=4><asp:TextBox id="txtSignDesignation" maxlength="100" width="50%" runat="server" /></td>
				</tr>
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:ImageButton id="PrintPrev" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" /></td>					
				</tr>				
			</table>
		</form>
	</body>
</HTML>
