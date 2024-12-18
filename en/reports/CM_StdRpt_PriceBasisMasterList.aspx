<%@ Page Language="vb" src="../../include/reports/CM_StdRpt_PriceBasisMasterList.aspx.vb" Inherits="CM_StdRpt_PriceBasisMasterList" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="CM_STDRPT_SELECTION_CTRL" src="../include/reports/CM_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Contract Management - Price Basis Master Listing</title>
                <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
           <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 

			<!--<input type=hidden id=hidUserLocPX runat="server" NAME="hidUserLocPX"/>
			<input type=hidden id=hidAccMonthPX runat="server" NAME="hidAccMonthPX"/>
			<input type=hidden id=hidAccYearPX runat="server" NAME="hidAccYearPX"/>-->

			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1" class="font9Tahoma">
				<tr>
					<td class="font9Tahoma" colspan="3">CONTRACT MANAGEMENT - PRICE BASIS MASTER LISTING</td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6"><hr style="width :100%" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6"><UserControl:CM_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
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
					<td colspan="6"><hr size="1" noshade></td>
				</tr>
			</table>
			<table width="100%" border="0" cellspacing="1" cellpadding="1" ID="Table1" class="font9Tahoma">
				<tr>
					<td>Price Basis Code: </td>
					<td><asp:textbox id="txtPriceBasisCode" maxlength=8  runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Description: </td>
					<td><asp:textbox id="txtName" maxlength=128  runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>				
				<tr>
					<td>Status :</td>
					<td><asp:DropDownList id="lstStatus"  width="39%" size="1" runat="server" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				<tr ID="trUpdateDate" >
					<td>Updated Date: </td>
					<td><asp:TextBox id="txtDateFrom" maxlength="10"  runat="server"/>
  								  <a href="javascript:PopCal('txtDateFrom');">
								  <asp:Image id="btnSelDateFrom" runat="server" ImageUrl="../Images/calendar.gif"/></a></td>				
					<td colspan=2>To : &nbsp;
								  <asp:TextBox id="txtDateTo" maxlength="10"  runat="server"/>
  								  <a href="javascript:PopCal('txtDateTo');">
								  <asp:Image id="btnSelDateTo" runat="server" ImageUrl="../Images/calendar.gif"/></a></td>
				</tr>
				<tr ID="trUpdateBy" >
					<td>Updated By :</td>
					<td><asp:TextBox id="TxtUpdatedBy"  maxlength="20" runat="server" /> (blank for all) </td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				<tr>
					<td colspan=4><asp:Label id="lblLocation" visible="false" runat="server" /></td>
				</tr>
				<tr>
					<td colspan=4>&nbsp;</td>
				</tr>											
				<tr>
					<td colspan=4><asp:ImageButton id="PrintPrev" ImageUrl="../images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" />&nbsp;<asp:Button 
                            ID="Issue6" runat="server" class="button-small" Text="Print Preview" 
                            Height="26px" Visible="False" />
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
