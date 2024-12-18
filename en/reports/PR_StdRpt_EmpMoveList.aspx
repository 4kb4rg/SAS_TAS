<%@ Page Language="vb" src="../../include/reports/PR_StdRpt_EmpMoveList.aspx.vb" Inherits="PR_StdRpt_EmpMoveList" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="PR_STDRPT_SELECTION_CTRL" src="../include/reports/PR_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Payroll - Listing of Payroll Master for Employee Movement</title>
                <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
        <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">

			<!--<input type=text id=hidAccMonth  runat="server" NAME="AccMonth"/>
			<input type=text id=hidAccYear  runat="server" NAME="AccYear"/>-->
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1" class="font9Tahoma">
				<tr>
					<td class="font9Tahoma" colspan="3">PAYROLL - LISTING OF PAYROLL MASTER FOR EMPLOYEE MOVEMENT</td>
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
				<!--<tr>
					<td colspan="6"><asp:Label id="lblDate" forecolor=red visible="false" text="Incorrect Date Format. Date Format is " runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>	-->	
				<tr>
					<td colspan="6"><hr size="1" noshade></td>
				</tr>
			</table>
			<table width=100% border="0" cellspacing="1" cellpadding="1" ID="Table2">
				<tr>
					<td width=15%>&nbsp;</td>
					<td width=35%>&nbsp;</td>
					<td width=15%>&nbsp;</td>
					<td width=35%>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=4><asp:Label id="lblLocation" visible="false" runat="server" /></td>
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
