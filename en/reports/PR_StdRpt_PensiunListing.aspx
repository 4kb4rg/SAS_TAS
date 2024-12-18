<%@ Page Language="vb" src="../../include/reports/PR_StdRpt_PensiunListing.aspx.vb" Inherits="PR_StdRpt_PensiunListing" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="PR_STDRPT_SELECTION_CTRL" src="../include/reports/PR_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Payroll - Daftar Iuran Dana Pensiun Manulife </title>
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" ID="frmMain">
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1">
				<tr>
					<td class="mt-h" colspan="3">PAYROLL - DAFTAR IURAN DANA PENSIUN MANULIFE</td>
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
					<td width=50% >Accounting Period : &nbsp;										
						<asp:DropDownList id="lstAccMonthFrom" size=1 width=20% runat=server />&nbsp;
						<asp:DropDownList id="lstAccYearFrom" size=1 width=20% autopostback=true onselectedindexchanged=OnIndexChage_FromAccPeriod runat=server /></td>
					<td width=50% align=left>To : &nbsp;						
						<asp:DropDownList id="lstAccMonthTo" size=1 width=20% runat=server />
						<asp:DropDownList id="lstAccYearTo" size=1 width=20% autopostback=true onselectedindexchanged=OnIndexChage_FromAccPeriod runat=server />					
					</td>
				</tr>
				<tr><td colspan=2 width=100%>&nbsp;</td></tr>
				<tr><td colspan=2 width=100%><asp:CheckBox id="cbExcel" runat=server Text="Transfer to Excel" autopostback=true oncheckedchanged=OnChecked_Transfer /></td></tr>
				<tr><td colspan=2 width=100%>&nbsp;</td></tr>
				<tr>
					<td colspan=2 width=100%><asp:Label id="lblLocation" visible="false" runat="server" /></td>
				</tr>
				<tr>
					<td colspan=2 width=100%><asp:ImageButton id="PrintPrev"  AlternateText="Print Preview" imageurl="../images/butt_print_preview.gif" onClick="btnPrintPrev_Click" runat="server" /></td>					
				</tr>	
				<tr><td colspan=2 width=100%>&nbsp;</td></tr>
				<tr><td colspan=2 width=100%><asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" /></td></tr>			
			</table>
		</form>
		
	</body>
</HTML>
