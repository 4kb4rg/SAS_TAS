<%@ Page Language="vb" src="../../include/reports/PU_StdRpt_MthStockReceived.aspx.vb" Inherits="PU_StdRpt_MthStockReceived" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="PU_STDRPT_SELECTION_CTRL" src="../include/reports/PU_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Purchasing - Monthly Goods Received Report</title>
                <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
        <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">

			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1" class="font9Tahoma" >
				<tr>
					<td class="font9Tahoma" colspan="3"><strong>PURCHASING - MONTHLY GOODS RECEIVED REPORT</strong> </td>
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
					<td colspan="6"><UserControl:PU_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>			
				<tr>
					<td colspan="6"><asp:Label id="lblDate" forecolor=red visible="false" text="Incorrect Date Format. Date Format is " runat="server" />				
									<asp:Label id="lblDateFormat" forecolor=red visible="false" runat="server" /></td>				
				</tr>
				<tr>
					<td colspan="6"><asp:Label id="lblError" forecolor=red visible="false" runat="server" /></td>				
				</tr>
				<tr>
					<td colspan="6">
                        <hr style="width :100%" /> 
                    </td>
				</tr>
			</table>
			<table width=100% border="0" cellspacing="1" cellpadding="1" ID="Table2" class="font9Tahoma"  runat=server>	
				<tr>
					<td>Supplier :</td>
					<td><asp:textbox id="txtSupplier" width="50%" maxlength=8 runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>								
				</tr>
				<tr>
					<td>Receive Note ID :</td>
					<td><asp:textbox id="txtRcvNoteIDFrom" width="50%" maxlength=20 runat="server" /> (blank for all)</td>
					<td>To :</td>
					<td><asp:textbox id="txtRcvNoteIDTo" width="50%" maxlength=20 runat="server" /> (blank for all)</td>
				</tr>			
				<tr>
					<td>Receive Note Date :</td>
					<td><asp:textbox id="txtRcvNoteDateFrom" width="50%" maxlength=20 runat="server" /><a href="javascript:PopCal('txtRcvNoteDateFrom');"><asp:Image id="btnSelFromDate" runat="server" ImageUrl="../../EN/Images/calendar.gif"/></a>
					 (blank for all)</td>
					
					<td>To :</td>
					<td><asp:textbox id="txtRcvNoteDateTo" width="50%" maxlength=20 runat="server" /><a href="javascript:PopCal('txtRcvNoteDateTo');"><asp:Image id="btnSelToDate" runat="server" ImageUrl="../../EN/Images/calendar.gif"/></a>
					 (blank for all)</td>
				</tr>	
				<tr>
					<td>Item Code :</td>
					<td><asp:textbox id="txtFromItemCode" width="50%" runat="server" /> (blank for all)</td>
					<td>To :</td>
					<td><asp:textbox id="txtToItemCode" width="50%" runat="server" /> (blank for all)</td>
				</tr>	
				<tr>
					<td width=15%>Status :</td>
					<td width=35%><asp:DropDownList id="lstStatus" size="1" width="50%" runat="server" /></td>
					<td width=15%>&nbsp;</td>
					<td width=35%>&nbsp;</td>
				</tr>	
				<tr>
					<td width=15%>Item Type :</td>
					<td width=35%><asp:DropDownList id="lstItemType" size="1" width="50%" runat="server" /></td>
					<td width=15%>&nbsp;</td>
					<td width=35%>&nbsp;</td>
				</tr>																								
				<tr>
					<td colspan=4><asp:Label id="lblLocation" visible="false" runat="server" /></td>
					<td><asp:label id="lblAccCode" visible="false" runat=server /></td>
					<td><asp:label id="lblVehCode" visible="false" runat=server /></td>
					<td><asp:label id="lblVehExpCode" visible="false" runat=server /></td>
					<td><asp:label id="lblBlkCode" visible="false" runat=server /></td>
				</tr>
				<tr>
					<td><asp:ImageButton id="PrintPrev" AlternateText="Print Preview" imageurl="../images/butt_print_preview.gif" onClick="btnPrintPrev_Click" runat="server" />&nbsp;<asp:Button 
                            ID="Issue6" runat="server" class="button-small" Text="Print Preview" 
                            Visible="False" />
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
