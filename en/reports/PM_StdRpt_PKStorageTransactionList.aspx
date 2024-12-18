<%@ Page Language="vb" src="../../include/reports/PM_StdRpt_PKStorageTransactionList.aspx.vb" Inherits="PM_StdRpt_PKStorageTransactionList" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="PD_STDRPT_SELECTION_CTRL" src="../include/reports/PD_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>PRODUCTION MANAGEMENT - PK STORAGE TRANSACTION LISTING</title>
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
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1" class="font9Tahoma">
				<tr>
					<td class="mt-h" colspan="3">PRODUCTION MANAGEMENT - PK STORAGE TRANSACTION LISTING</td>
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
					<td colspan="6"><UserControl:PD_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
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
			<table width="100%" border="0" cellspacing="1" cellpadding="1" ID="Table2" class="font9Tahoma" runat="server">
				<tr>
					<td width=17%>Transaction Date From :</td>
					<td width=39%><asp:TextBox id="txtDateFrom" maxlength="10" width=50% runat="server"/>
  								  <a href="javascript:PopCal('txtDateFrom');">
								  <asp:Image id="btnDateFrom" runat="server" ImageUrl="../Images/calendar.gif"/></a>
								  <asp:Label id="lblDateFrom" visible="false" forecolor=red runat="server" />
					</td>				
					<td width=4% align=center>To :</td>
					<td width=40%><asp:TextBox id="txtDateTo" maxlength="10" width=50% runat="server"/>
  								  <a href="javascript:PopCal('txtDateTo');">
								  <asp:Image id="btnDateTo" runat="server" ImageUrl="../Images/calendar.gif"/></a> 
								  <asp:Label id="lblDateTo" visible="false" forecolor=red runat="server" />
					</td>
				</tr>
				<tr>
					<td width=17%>Storage Area Code : </td>
					<td width=39%><asp:TextBox id="txtStorageAreaCode" maxlength="8" width=50% runat="server"/> (blank for all)</td>
					<td width=4%>&nbsp;</td>
					<td width=40%>&nbsp;</td>					
				</tr>
				<tr>
					<td colspan="4"><asp:ImageButton id="PrintPrev" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" />&nbsp;<asp:Button 
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
	</body>
</HTML>
