<%@ Page Language="vb" src="../../include/reports/PR_StdRpt_AdvPayment.aspx.vb" Inherits="PR_StdRpt_AdvPayment" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="PR_StdRpt_Selection_Ctrl" src="../include/reports/PR_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Payroll - Detailed Employee Payslip</title>
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
					<td class="mt-h" colspan="3">PAYROLL - ADVANCE PAYMENT LISTING</td>
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
			<table width=100% border="0" cellspacing="1" cellpadding="1" ID="Table2" class="font9Tahoma" runat=server>	
				<tr>
					<td width=20%>Department Code :</td>
					<td width=35%><asp:DropDownList id="lstDeptCode" AutoPostBack=true width="100%" runat="server" /></td>
					<td width=5%>&nbsp;</td>
					<td width=5%>&nbsp;</td>
					<td width=35%>&nbsp;</td>
				</tr>
				<tr>
					<td>Employee Code From :</td>
					<td><asp:DropDownList id="lstEmpCodeFrom" maxlength=20 width="100%" runat="server" /></td>
					<td>&nbsp;</td>
					<td>To :</td>
					<td><asp:DropDownList ID="lstEmpCodeTo" maxlength=20 width="100%" runat=server /></td>
				</tr>
				<tr>
					<td>Gang Code :</td>
					<td><asp:TextBox id=txtGangCode width=50% maxlength=8 runat=server /> (leave blank for all)</td>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr>
					<td>Preview Format :</td>
					<td><asp:DropDownList ID="lstpreFormat" maxlength=20 width="100%" runat=server>
							<asp:ListItem text="Portable Document Format (PDF)" value="pdf" />									
							<asp:ListItem text="Web Page (HTML)" value="html" />
						</asp:DropDownList>
					</td>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=5>
						<asp:Label id="lblLocation" visible="false" runat="server" />
						<asp:Label id=lblBlock visible=false runat=server />
						<asp:Label id=lblSubBlk visible=false runat=server />
					</td>
				</tr>
				<tr>
					<td><asp:ImageButton id="PrintPrev" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" /></td>					
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
