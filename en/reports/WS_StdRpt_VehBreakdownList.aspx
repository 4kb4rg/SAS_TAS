<%@ Page Language="vb" src="../../include/reports/WS_StdRpt_VehBreakdownList.aspx.vb" Inherits="WS_StdRpt_VehBreakdownList" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="WS_STDRPT_SELECTION_CTRL" src="../include/reports/WS_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Workshop - Vehicle Breakdown Report</title>
                <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
        <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">

			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1">
				<tr>
					<td class="font9Tahoma" colspan="3">WORKSHOP - VEHICLE BREAKDOWN REPORT</td>
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
					<td colspan="6"><UserControl:WS_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>			
				<tr>
					<td colspan="6"><asp:Label id="lblDate" forecolor=red visible="false" text="Incorrect Date Format. Date Format is " runat="server" /></td>
									<asp:Label id="lblDateFormat" forecolor=red visible="false" runat="server" />				
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
			<table width=100% border="0" cellspacing="1" cellpadding="1" ID="Table2" class="font9Tahoma" runat=server>
				<tr>
					<td><asp:label id=lblVehRegNo runat=server /></td>
					<td><asp:textbox id="txtVehRegNo" maxlength=8 width=50% runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>		
				<tr>
					<td>Job Create Date From :</td>
					<td><asp:TextBox id="txtJobCreateDateFrom" size="12" width=50% maxlength="10" runat="server"/>
  								  <a href="javascript:PopCal('txtJobCreateDateFrom');">
  								  <asp:Image id="btnSelDateFrom" runat="server" ImageUrl="../Images/calendar.gif"/></a></td>						
					<td>To :</td>
					<td><asp:TextBox id="txtJobCreateDateTo" size="12" width=50% maxlength="10" runat="server"/>
  								  <a href="javascript:PopCal('txtJobCreateDateTo');">
  								  <asp:Image id="btnSelDateTo" runat="server" ImageUrl="../Images/calendar.gif"/></a></td>										
				</tr>
				<tr>
					<td>Date In From: </td>
					<td><asp:TextBox id="txtDateInFrom" size="12" width=50% maxlength="10" runat="server"/>
  								  <a href="javascript:PopCal('txtDateInFrom');">
  								  <asp:Image id="btnSelDateInFrom" runat="server" ImageUrl="../Images/calendar.gif"/></a></td>						
					<td>To :</td>
					<td><asp:TextBox id="txtDateInTo" size="12" width=50% maxlength="10" runat="server"/>
  								  <a href="javascript:PopCal('txtDateInTo');">
  								  <asp:Image id="btnSelDateInTo" runat="server" ImageUrl="../Images/calendar.gif"/></a></td>										
				</tr>														
				<tr>
					<td>Job ID From :</td>
					<td><asp:textbox id="txtJobIDFrom" maxlength=20 width="50%" runat="server" /> (blank for all)</td>
					<td>To :</td>
					<td><asp:textbox ID="txtJobIDTo" maxlength=20 width="50%" Runat=server /> (blank for all)</td>
				</tr>
											
				<tr>
					<td>Mechanic ID From :</td>
					<td><asp:textbox id="txtMechIDFrom" maxlength=20 width="50%" runat="server" /> (blank for all)</td>
					<td>To :</td>
					<td><asp:textbox ID="txtMechIDTo" maxlength=20 width="50%" Runat=server /> (blank for all)</td>
				</tr>				
				<tr>
					<td><asp:label id=lblBillPartyCode runat=server /></td>
					<td><asp:TextBox id="txtBillPartyCode" maxlength=8 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
				</tr>										
				<tr>
					<td><asp:label id=lblTypeOfVeh runat=server /></td>
					<td><asp:dropdownlist id="lstTypeOfVeh" width=50% runat="server" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:label id=lblVehExpCode runat=server /></td>
					<td><asp:textbox id="txtVehExpCode" maxlength=8 width=50% runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>																														
				<tr>
					<td><asp:label id=lblWorkCode runat=server /></td>
					<td><asp:textbox id="txtWorkCode" maxlength=8 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>				
				</tr>
				<tr>
					<td><asp:label id=lblServTypeCode runat=server /></td>
					<td><asp:textbox id="txtServTypeCode" maxlength=8 width=50% runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>	
				<tr>
					<td width=15%>Status :</td>
					<td width=35%><asp:DropDownList id="lstStatus" width="50%" size="1" runat="server" /></td>
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
		<asp:Label id="lblVeh" visible="false" runat="server" />
		<asp:Label id="lblCompany" visible="false" runat="server" />
	</body>
</HTML>
