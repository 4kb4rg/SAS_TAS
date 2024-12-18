<%@ Page Language="vb" src="../../include/reports/CT_StdRpt_MthlyIssueForPayrollBillList.aspx.vb" Inherits="CT_StdRpt_MthlyIssueForPayrollBillList" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="CT_STDRPT_SELECTION_CTRL" src="../include/reports/CT_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Canteen - Monthly Canteen Issue Transfer For Payroll/Billing Listing</title>
                 <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" ID="frmMain">
       		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
           <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">

			<asp:Label id="lblLocation" visible="false" runat="server" />
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1">
				<tr>
					<td class="font9Tahoma" colspan="3">CANTEEN - MONTHLY CANTEEN ISSUE TRANSFER FOR PAYROLL/BILLING LISTING</td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6"><hr size="1" noshade></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6"><UserControl:CT_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
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
					<td width=15%>Display details by :</td>
					<td width=35%><asp:dropdownlist id="lstDisplay" width="50%" AutoPostBack=true runat="server" /></td>
					<td width=15%>&nbsp;</td>
					<td width=35%>&nbsp;</td>
				</tr>		
				<tr>
					<td>Order By :</td>
					<td><asp:dropdownlist id="lstOrderBy" width="50%" runat="server" >
						<asp:listitem value=ASC text="Ascending" />
						<asp:listitem value=DESC text="Descending" />
						</asp:dropdownlist>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>																								
				<tr id="trEmp">
					<td><asp:label id=lblEmployeeCode text="Employee Code : " runat=server /></td>
					<td><asp:textbox id="txtEmpCode" maxlength=20 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr id="trBillParty">
					<td><asp:label id=lblBillPartyCode runat=server /></td>
					<td><asp:textbox id="txtBillPartyCode" maxlength=8 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>													
				<tr>
					<td colspan=4>&nbsp;</td>
				</tr>									
				<tr>
					<td colspan=4><asp:ImageButton id="PrintPrev" ImageUrl="../images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" /></td>					
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
