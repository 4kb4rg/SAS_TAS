<%@ Page Language="vb" src="../../include/reports/PR_StdRpt_ContractCheckrollAllow.aspx.vb" Inherits="ContractCheckrollAllow" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="PR_STDRPT_SELECTION_CTRL" src="../include/reports/PR_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Payroll - Contractor Checkroll Allowances</title>
                <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
        <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">
			<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" />
			<asp:Label id="lblLocation" visible="false" runat="server" />											
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1">
				<tr>
					<td class="font9Tahoma" colspan="3">PAYROLL - CONTRACTOR CHECKROLL ALLOWANCES</td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6"><hr size="1" noshade>
					</td>
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
					<td colspan="6"><asp:Label id=lblDate visible=False forecolor=red text="<br>Incorrect Date Format. Date Format is " runat=server />
									<asp:Label id="lblDateFormat" forecolor=red visible="false" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6"><hr size="1" noshade>
					</td>
				</tr>
			</table>
			<table width="100%" border="0" cellspacing="1" cellpadding="1" ID="Table2" class="font9Tahoma" runat="server">
				<tr>
					<td width=15%>Supplier Code : </td>
					<td width=35%><asp:textbox id="txtSuppCode" width="50%" maxlength=8 runat="server" /> (blank for all)</td>
					<td width=15%><!--To :--></td>
					<td width=35%><!--<asp:textbox id="txtSuppCodeTo" width="50%" maxlength=8 runat="server" /> (blank for all)--></td>
				</tr>			
				<tr>
					<td>Attendance Date From :* </td>
					<td><asp:textbox id="txtAttdDateFrom" width="50%" maxlength=10 runat="server" />
						<a href="javascript:PopCal('txtAttdDateFrom');"><asp:Image id="btnSelDateFr" runat="server" ImageUrl="../Images/calendar.gif"/></a>
						<asp:RequiredFieldValidator id=validateAttdDateFrom display=Dynamic runat=server 
							ErrorMessage="<BR>Please enter Attendance Date From"
							ControlToValidate=txtAttdDateFrom /></td>					
					<td>To :*</td>
					<td><asp:textbox id="txtAttdDateTo" width="50%" maxlength=10 runat="server" />
						<a href="javascript:PopCal('txtAttdDateTo');"><asp:Image id="btnSelDateTo" runat="server" ImageUrl="../Images/calendar.gif"/></a>
						<asp:RequiredFieldValidator id=validateAttdDateTo display=Dynamic runat=server 
							ErrorMessage="<BR>Please enter Attendance Date To"
							ControlToValidate=txtAttdDateTo /></td>
				</tr>			
				<tr>
					<td colspan="5"><asp:ImageButton id="PrintPrev" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" /></td>
				</tr>
			</table>
            </div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</HTML>
