<%@ Page Language="vb" trace=false src="../../include/reports/WM_StdRpt_PK_Billing_Report.aspx.vb" Inherits="WM_StdRpt_PKBillRpt" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="WM_STDRPT_SELECTION_CTRL" src="../include/reports/WM_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Weighing Management - PK Billing Report</title>
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
			<asp:Label id="lblVehicleTag" visible="false" runat="server" />
			<asp:Label id="lblCode" text=" Code" visible="false" runat="server" />
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong> WEIGHING MANAGEMENT - PK BILLING REPORT</strong></td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">
                        <hr style="width :100%" /> 
                    </td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6"><UserControl:WM_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>		
				<tr>
					<td colspan="6"><asp:Label id="lblDate" forecolor=red visible="false" text="Incorrect Date Format. Date Format is " runat="server" />
									<asp:Label id="lblDateFormat" forecolor=red visible="false" runat="server" /></td>				
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>							
				<tr>
					<td colspan="6">
                        <hr style="width :100%" /> 
                    </td>
				</tr>
			</table>
			<table width="100%" border="0" cellspacing="1" cellpadding="1" ID="Table1" class="font9Tahoma">		
				<tr>
					<td width=17%>Delivery Date From :</td>
					<td width=39%><asp:TextBox id="txtDelDateFrom" size="12" width=50% maxlength="10" runat="server"/>
  								  <a href="javascript:PopCal('txtDelDateFrom');">
  								  <asp:Image id="btnSelDelDateFrom" runat="server" ImageUrl="../Images/calendar.gif"/></a></td>						
					<td width=4%>To :</td>
					<td width=40%><asp:TextBox id="txtDelDateTo" size="12" width=50% maxlength="10" runat="server"/>
  								  <a href="javascript:PopCal('txtDelDateTo');">
  								  <asp:Image id="btnSelDelDateTo" runat="server" ImageUrl="../Images/calendar.gif"/></a></td>										
				</tr>		
				<tr>
					<td><asp:label id=lblBillPartyTag runat=server /> :</td>
					<td><asp:textbox id="txtBillPartyCode" width="50%" maxlength=8 runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>								
				</tr>	
				<tr>
					<td colspan=4>&nbsp;</td>
				</tr>											
				<tr>
					<td colspan=4><asp:ImageButton id="PrintPrev" ImageUrl="../images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" />&nbsp;</td>
				</tr>				
			</table>
        </div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</HTML>
