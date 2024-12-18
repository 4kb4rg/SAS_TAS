<%@ Page Language="vb" src="../../include/reports/PR_StdRpt_TAXBorangCP39.aspx.vb" Inherits="PR_StdRpt_TAXBorangCP39" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="PR_StdRpt_Selection_Ctrl" src="../include/reports/PR_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Payroll - TAX Borang CP39 </title>
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" ID="frmMain">
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1">
				<tr>
					<td class="mt-h" colspan="3">PAYROLL - TAX BORANG CP39</td>
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
					<td width=17%>Cheque No. :</td>
					<td width=39%><asp:TextBox id="txtChequeNo" maxlength="100" width="50%" runat="server" />
								  <asp:RequiredFieldValidator 
										id="validChequeNo" 
										ControlToValidate="txtChequeNo"
										ErrorMessage="Cheque No is a required field."
										ForeColor="Red" 
										runat="server" />
								  </td>
					<td width=4%>&nbsp;</td>
					<td width=40%>&nbsp;</td>
				</tr>
				<tr>		
					<td>Tax Branch :</td>
					<td><asp:DropDownList id=ddlTaxBranch width=50% runat=server />
								  <asp:RequiredFieldValidator 
										id="validTaxBranch" 
										ControlToValidate="ddlTaxBranch"
										ErrorMessage="Please select a Tax Branch."
										ForeColor="Red" 
										runat="server" />
					</td>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:ImageButton id="PrintPrev" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" /></td>					
				</tr>				
			</table>
		</form>
		<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" />
	</body>
</HTML>
