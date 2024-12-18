<%@ Page Language="vb" src="../../include/reports/PR_StdRpt_EPFBorangA.aspx.vb" Inherits="PR_StdRpt_EPFBorangA" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="PR_StdRpt_Selection_Ctrl" src="../include/reports/PR_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Payroll - EPF Borang A </title>
                <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server"  class="main-modul-bg-app-list-pu"  ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
        <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">

			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1">
				<tr>
					<td class="font9Tahoma" colspan="3">PAYROLL - EPF BORANG A</td>
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
					<td width=20%>Cheque No. :</td>
					<td width=50%><asp:TextBox id="txtChequeNo" maxlength="100" width="30%" runat="server" />
								  <asp:RequiredFieldValidator 
										id="validChequeNo" 
										ControlToValidate="txtChequeNo"
										ErrorMessage="Cheque No is a required field."
										ForeColor="Red" 
										runat="server" />
								  </td>
					<td width=10%>&nbsp;</td>
					<td width=10%>&nbsp;</td>
					<td width=10%>&nbsp;</td>
				</tr>
				<tr>
					<td>Employee Code :</td>
					<td colspan=4><asp:DropDownList id="lstEmpCode" AutoPostBack=true onSelectedIndexChanged="EmpIndexChanged" width="50%" runat="server" /></td>
				</tr>
				<tr>
					<td>Signature Name : </td>
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
        </div>
        </td>
        </tr>
        </table>
		</form>
		<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" />
	</body>
</HTML>
