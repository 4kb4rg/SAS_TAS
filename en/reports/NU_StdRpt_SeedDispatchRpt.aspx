<%@ Page Language="vb" src="../../include/reports/NU_StdRpt_SeedDispatchRpt.aspx.vb" Inherits="NU_StdRpt_SeedDispatchRpt" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="NU_StdRpt_Selection_Ctrl" src="../include/reports/NU_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Nursery - Seedlings Dispatch By Estate</title>
                <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu"  ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
        <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">

			<!--<input type=Hidden id=hidUserLocPX runat="server" NAME="hidUserLocPX"/>
			<input type=Hidden id=hidAccMonthPX runat="server" NAME="hidAccMonthPX"/>
			<input type=Hidden id=hidAccYearPX runat="server" NAME="hidAccYearPX"/>-->

			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1" class="font9Tahoma">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong>NURSERY - SEEDLINGS DISPATCH BY ESTATE</strong> </td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6"><hr style="width :100%" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6"><UserControl:NU_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>			
				<tr>
					<td colspan="6"><asp:Label id="lblDate" forecolor=red visible="false" text="Incorrect Date Format. Date Format is " runat="server" />
									<asp:Label id="lblDateFormat" forecolor=red visible="false" runat="server" />									
									<asp:Label id="lblErrAccMonth" forecolor=red visible="false" text="Accounting Month from cannot be bigger than Accounting Month To if same year." runat="server" />
									<asp:Label id="lblErrAccYear" forecolor=red visible="false" text="Accounting Year from cannot be bigger than Accounting Year To." runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>		
				<tr>
					<td colspan="6"><hr style="width :100%" /></td>
				</tr>
			</table>
			<table width=100% border="0" cellspacing="1" cellpadding="1" ID="Table2" class="font9Tahoma" runat=server>	
				<tr>
					<td>Dispatch ID From :</td>
					<td><asp:textbox id="txtTxIDFrom" width="50%" runat="server" /> (blank for all)</td>
					<td>To :</td>
					<td><asp:textbox ID="txtTxIDTo" width="50%" Runat=server /> (blank for all)</td>
				</tr>			
				<tr>
					<td>Document Ref No :</td>
					<td><asp:textbox id="txtDocRefNoFrom" width="50%" runat="server" /> (blank for all)</td>
					<td width=15%>&nbsp;</td>
					<td width=35%>&nbsp;</td>					
				</tr>			
				<tr>
					<td>Dispatch Date From :</td>
					<td><asp:TextBox id="txtTxDateFrom" maxlength="10" width=50% runat="server"/>
  								  <a href="javascript:PopCal('txtTxDateFrom');">
								  <asp:Image id="btnSelTxDateFrom" runat="server" ImageUrl="../Images/calendar.gif"/></a></td>				
					<td>To :</td>
					<td><asp:TextBox id="txtTxDateTo" maxlength="10" width=50% runat="server"/>
  								  <a href="javascript:PopCal('txtTxDateTo');">
								  <asp:Image id="btnSelTxDateTo" runat="server" ImageUrl="../Images/calendar.gif"/></a></td>
				</tr>			
				<tr>
					<td><asp:label id=lblBlkCode runat=server /> :</td>
					<td><asp:textbox id="txtBlkCode" maxlength=8 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
				</tr>
				<tr>
					<td><asp:label id=lblBatchNo runat=server /> :</td>
					<td><asp:textbox id="txtBatchNo" maxlength=2 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
				</tr>
				<tr>
					<td width=15%><asp:label id=lblBillPartyCode runat=server /> :</td>
					<td width=35%><asp:textbox id="txtBillPartyCode" maxlength=8 width="50%" runat="server" /> (blank for all)</td>
					<td width=15%>&nbsp;</td>
					<td width=35%>&nbsp;</td>					
				</tr>
				<tr>
					<td colspan=4><asp:Label id="lblLocation" visible="false" runat="server" /></td>
				</tr>
				<tr>
					<td colspan=4><asp:Label id="lblPlantMaterial" visible="false" runat="server" /></td>
				</tr>
				<tr>
					<td colspan=4>&nbsp;</td>
				</tr>							
				<tr>
					<td><asp:ImageButton id="PrintPrev" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" />&nbsp;<asp:Button 
                            ID="Issue6" runat="server" class="button-small" Text="Print Preview" />
                    </td>					
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
