<%@ Page Language="vb" src="../../include/reports/WM_StdRpt_FFBAssess_Listing.aspx.vb" Inherits="WM_StdRpt_FFBAssess_Listing" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="WM_STDRPT_SELECTION_CTRL" src="../include/reports/WM_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Weighing Management - FFB Assessment Listing</title>
                <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" ID="frmMain">
			<asp:Label id="lblLocation" visible="false" runat="server" />		
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1" class="font9Tahoma">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong>WEIGHING MANAGEMENT - FFB ASSESSMENT LISTING</strong> </td>
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
					<td>Ticket No From : </td>
					<td><asp:textbox id="txtTicketNoFrom" maxlength="20" width=50% runat="server" /> (blank for all)</td>
					<td>To : </td> 
					<td><asp:textbox id="txtTicketNoTo" maxlength="20" width=50% runat="server" /> (blank for all)</td>
				</tr>						
				<tr>
					<td width="17%">Date Inspected From : </td>
					<td width="39%"><asp:TextBox id="txtInspDate" maxlength="10" width=50% runat="server"/>
  					    <a href="javascript:PopCal('txtInspDate');">
						<asp:Image id="btnSelDateInFrom" runat="server" ImageUrl="../Images/calendar.gif"/></a></td>
					<td width="4%">To : </td>
					<td width="40%"><asp:TextBox id="txtInspDateTo" maxlength="10" width=50% runat="server"/>
  					    <a href="javascript:PopCal('txtInspDateTo');">
						<asp:Image id="btnSelDateInTo" runat="server" ImageUrl="../Images/calendar.gif"/></a></td>
				</tr>
				<tr>
					<td>Ripe : </td>
					<td><asp:TextBox id="txtRipe" maxlength="9" width=50% runat="server"/> </td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Overripe : </td>
					<td><asp:TextBox id="txtOverRipe" runat="server" width=50% maxlength="9"/></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Underripe : </td>
					<td><asp:TextBox id="txtUnderRipe" runat="server" width=50% maxlength="9" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Unripe : </td>
					<td><asp:TextBox id="txtUnripe" runat="server" width=50% maxlength="9" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Empty : </td>
					<td><asp:TextBox id="txtEmpty" runat="server" width=50% maxlength="9" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>	
				<tr>
					<td>Rotten : </td>
					<td><asp:TextBox id="txtRotten" runat="server" width=50% maxlength="9" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Poor : </td>
					<td><asp:TextBox id="txtPoor"  runat="server" width=50% maxlength="9" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>								
				<tr>
					<td>Small : </td>
					<td><asp:TextBox id="txtSmall"  runat="server" width=50% maxlength="9" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>								
				<tr>
					<td>Long Stalk : </td>
					<td><asp:TextBox id="txtLongStalk"  runat="server" width=50% maxlength="9" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Contamination : </td>
					<td><asp:TextBox id="txtContamination"  runat="server" width=50% maxlength="9" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Total : </td>
					<td><asp:TextBox id="txtTotal"  runat="server" width=50% maxlength="9" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Graded Percent : </td>
					<td><asp:TextBox id="txtGradedPercent"  runat="server" width=50% maxlength="5" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>								
				<tr>
					<td>Ungradable Bunch : </td>
					<td><asp:TextBox id="txtUngradableBunch"  runat="server" width=50% maxlength="5" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>	
				
				<tr>
					<td>Others : </td>
					<td><asp:TextBox id="txtOthers"  runat="server" width=50% maxlength="5" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>	
																
				<tr>
					<td>Status:</td>
					<td><asp:DropDownList id="lstStatus" width=50% size="1" runat="server" /></td>					
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
		<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" />
	</body>
</HTML>
