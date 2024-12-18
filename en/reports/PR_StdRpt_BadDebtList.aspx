<%@ Page Language="vb" src="../../include/reports/PR_StdRpt_BadDebtList.aspx.vb" Inherits="PR_StdRpt_BadDebtList" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="PR_STDRPT_SELECTION_CTRL" src="../include/reports/PR_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Payroll - Bad Debts Listing</title>
                <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
        <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">

			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1" class="font9Tahoma">
				<tr>
					<td class="mt-h">PAYROLL - BAD DEBTS LISTING</td>
					<td align="right"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="2">
                        <hr style="width :100%" /> 
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
					<td colspan="6"><asp:Label id="lblDate" forecolor=red visible="false" text="Incorrect Date Format. Date Format is " runat="server" /></td>
									<asp:Label id="lblDateFormat" forecolor=red visible="false" runat="server" />
				</tr>
				<tr>
					<td colspan="2">
                        <hr style="width :100%" /> 
                    </td>
				</tr>
			</table>
			<table width="100%" border="0" cellspacing="1" cellpadding="1" ID="Table2" class="font9Tahoma" runat="server">
				<tr>
					<td width=17%>Employee Code From : </td>
					<td width=39%><asp:Textbox id=txtEmpCodeFrom maxlength=20 width=50% runat=server/> (leave blank for all)</td>			
					<td width=4%>To : </td>	
					<td width=40%><asp:Textbox id=txtEmpCodeTo maxlength=20 width=50% runat=server/> (leave blank for all)</td>
				</tr>	
				
				<tr>
					<td>Gang Code : </td>
					<td><asp:Textbox id=txtGangCode maxlength=8 width=50% runat=server/> (leave blank for all)</td>			
					<td>&nbsp;</td>	
					<td>&nbsp;</td>
				</tr>
				
				<tr>
					<td>Sorting By : </td>
					<td><asp:DropDownList id=lstSort width=50% runat=server>
							<asp:ListItem text="Employee Code" value="1" />
							<asp:ListItem text="Employee Name" value="2" />
							<asp:ListItem text="Appointment Date" value="3" />
							<asp:ListItem text="Termination Date" value="4" />
						</asp:DropDownList>&nbsp;</td>			
					<td>&nbsp;</td>	
					<td>&nbsp;</td>
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
            </div>
        </td>
        </tr>
        </table>
		</form>
		<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" />
	</body>
</HTML>
