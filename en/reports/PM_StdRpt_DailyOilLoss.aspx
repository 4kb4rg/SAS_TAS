<%@ Page Language="vb" src="../../include/reports/PM_StdRpt_DailyOilLoss.aspx.vb" Inherits="PM_StdRpt_DailyOilLoss" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="PD_STDRPT_SELECTION_CTRL" src="../include/reports/PD_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Production Management - Daily Oil Loss Report</title>
                <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server"  class="main-modul-bg-app-list-pu" ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
        <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">

			<!--<input type=hidden id=hidUserLocPX runat="server" NAME="hidUserLocPX"/>
			<input type=hidden id=hidAccMonthPX runat="server" NAME="hidAccMonthPX"/>
			<input type=hidden id=hidAccYearPX runat="server" NAME="hidAccYearPX"/>-->

			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1" class="font9Tahoma">
				<tr>
					<td class="mt-h" colspan="3">PRODUCTION MANAGEMENT - DAILY OIL LOSS REPORT</td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6"><hr size="1" noshade></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6"><UserControl:PD_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>			
				<tr>
					<td colspan="6"><asp:Label id="lblDate" forecolor=red visible="false" text="Incorrect Date Format. Date Format is " runat="server" />
									<asp:Label id="lblDateFormat" forecolor=red visible="false" runat="server" />	</td>			
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>		
				<tr>
					<td colspan="6"><hr size="1" noshade></td>
				</tr>
			</table>
			<table width="100%" border="0" cellspacing="1" cellpadding="1" ID="Table1" class="font9Tahoma">
				<tr>
					<td>Transaction Date From : </td>
					<td><asp:TextBox id="txtDateFrom" width="50%" maxlength="10" runat="server"/>
  					    <a href="javascript:PopCal('txtDateFrom');">
						<asp:Image id="btnSelDateFrom" runat="server" ImageUrl="../Images/calendar.gif"/></a></td>
					<td>To : </td>
					<td><asp:TextBox id="txtDateTo" width="50%" maxlength="10"  runat="server"/>
  					    <a href="javascript:PopCal('txtDateTo');">
						<asp:Image id="btnSelDateTo" runat="server" ImageUrl="../Images/calendar.gif"/></a></td>
				</tr>
				<tr>
					<td>Test Sample Code :</td>
					<td valign=center><asp:DropDownList id="ddlTestSampleCode" runat="server" width="50%" />                       
						<asp:RequiredFieldValidator 
							id="rfvTestSampleCode" 
							runat="server"  
							ControlToValidate="ddlTestSampleCode" 
							text = "Field cannot be blank"
							display="dynamic"/>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>		
				<tr>
					<td width=17%>Machine :</td>
					<td width=39%><asp:DropDownList id="ddlMachine" runat="server" width="50%" />                       
						<asp:RequiredFieldValidator 
							id="rfvMachine" 
							runat="server"  
							ControlToValidate="ddlMachine" 
							text = "Field cannot be blank"
							display="dynamic"/>						
						<asp:label id="lblDupMsg2"  Text="This transaction already exist" Visible=false forecolor=red Runat="server"/>
					</td>
					<td width=4%>&nbsp;</td>
					<td width=40%>&nbsp;</td>
				</tr>										
				<tr>
					<td colspan=4><asp:Label id="lblLocation" visible="false" runat="server" /></td>
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
