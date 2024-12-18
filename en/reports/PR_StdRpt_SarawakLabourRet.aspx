<%@ Page Language="vb" src="../../include/reports/PR_StdRpt_SarawakLabourRet.aspx.vb" Inherits="PR_StdRpt_SarawakLabourRet" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="PR_STDRPT_SELECTION_CTRL" src="../include/reports/PR_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Payroll - Borang JB SWK2.0 FOR SARAWAK LABOUR DEPARTMENT</title>
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" ID="frmMain">
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1">
				<tr>
					<td class="mt-h">PAYROLL - BORANG JB SWK2.0 FOR SARAWAK LABOUR DEPARTMENT</td>
					<td align="right"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="2"><hr size="1" noshade>
					</td>
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
					<td colspan="6"><asp:Label id=lblDate visible=False forecolor=red text="<br>Incorrect Date Format. Date Format is " runat=server />
									<asp:Label id="lblDateFormat" forecolor=red visible="false" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="2"><hr size="1" noshade>
					</td>
				</tr>
			</table>
			<table width="100%" border="0" cellspacing="1" cellpadding="1" ID="Table2" runat="server">
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
					<td width=17%>Employee ID From : </td>
					<td width=39%><asp:Textbox id=txtFromEmp maxlength=20 width=50% runat=server/> (leave blank for all)</td>			
					<td width=4%>To : </td>	
					<td width=40%><asp:Textbox id=txtToEmp maxlength=20 width=50% runat=server/> (leave blank for all)</td>
				</tr>	
				<tr>
					<td>Employee Status : </td>
					<td><asp:dropdownlist id=lstStatus width=50% runat=server>
							<asp:ListItem text="All" value="All" />
							<asp:ListItem text="Active" value="1" />
							<asp:ListItem text="Terminated" value="2" />
						</asp:DropDownList>&nbsp;</td>			
					<td>&nbsp;</td>	
					<td>&nbsp;</td>
				</tr>
											
				<tr>
					<td>&nbsp;</td>
					<td colspan=3><asp:Label id="lblLocation" visible="false" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="4"><asp:ImageButton id="PrintPrev" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" /></td>
				</tr>
			</table>
		</form>
		<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" />
	</body>
</HTML>
