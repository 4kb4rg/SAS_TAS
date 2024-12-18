<%@ Page Language="vb" src="../../include/reports/AP_StdRpt_MutasiAccountPayableCurr.aspx.vb" Inherits="AP_StdRpt_MutasiAccountPayableCurr" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="AP_STDRPT_SELECTION_CTRL" src="../include/reports/AP_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Account Payable - Summary A/P Outstanding In Different Foreign Exchange Rate</title>
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" ID="frmMain">
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1">
				<tr>
					<td class="mt-h" colspan="3">ACCOUNT PAYABLE - SUMMARY A/P OUTSTANDING IN DIFFERENT FOREIGN EXCHANGE RATE REPORT</td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6"><hr size="1" noshade></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6"><UserControl:AP_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>			
				<tr>
					<td colspan="6"><hr size="1" noshade></td>
				</tr>
			</table>
			<table width=100% border="0" cellspacing="2" cellpadding="1" ID="Table2" runat=server>	
			
				
				<tr>
					<td width=17%>Accounting Period : </td>  
					<td width=39%>
						<asp:DropDownList id="ddlSrchAccMonthFrom" size=1 width=10% runat=server />
						<asp:DropDownList id="ddlSrchAccYearFrom" size=1 width=10% autopostback=true onselectedindexchanged=OnIndexChage_FromAccPeriod runat=server />
					</td>
				</tr>		
				<tr>
				 <td width=17%>Report Criteria: </td>  
				 <td width=39%>
				 <asp:DropDownList width=50% id=ddlRtpType runat=server>
							<asp:ListItem value="0" Selected>All Sort By Supplier</asp:ListItem>
						    <asp:ListItem value="1">All Sort By Currency</asp:ListItem> 
						    <asp:ListItem value="2">Advance Payment Without Receive</asp:ListItem> 
					</asp:DropDownList></td>
				  </tr>
											
				<tr>
					<td colspan=4>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="4">
                       <asp:CheckBox id="cbExcel" text=" Export To Excel" checked="false" runat="server" /></td>
				</tr>								
				<tr>
					<td colspan=4>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=4 align=left><asp:ImageButton id="PrintPrev" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" /></td>					
				</tr>				
			</table>
		</form>
	</body>
</HTML>
