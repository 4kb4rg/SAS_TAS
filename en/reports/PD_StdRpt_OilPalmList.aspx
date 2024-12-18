<%@ Page Language="vb" src="../../include/reports/PD_StdRpt_OilPalmList.aspx.vb" Inherits="PD_StdRpt_OilPalmList" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="PD_STDRPT_SELECTION_CTRL" src="../include/reports/PD_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Production - Oil Palm Listing Report</title>
           <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" ID="frmMain">
           		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
        <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">

			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1" class="font9Tahoma">
				<tr>
					<td class="font9Tahoma">PRODUCTION - OIL PALM LISTING REPORT</td>
					<td align="right"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan=2><hr size="1" noshade></td>
				</tr>
				<tr>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=2><UserControl:PD_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan=2>&nbsp;</td>
				</tr>							
				<tr>
					<td colspan=2>&nbsp;</td>
				</tr>		
				<tr>
					<td colspan=2><hr size="1" noshade></td>
				</tr>
			</table>
			<table width=100% border="0" cellspacing="2" cellpadding="1" ID="Table2" class="font9Tahoma" runat=server>	
			   <tr>
					<td>Transaction Date From : </td>
					<td><asp:TextBox id="txtDateFrom" width="50%" maxlength="10" runat="server"/>
  					    <a href="javascript:PopCal('txtDateFrom');">
						<asp:Image id="btnSelDateFrom" runat="server" ImageUrl="../Images/calendar.gif"/></a> (leave blank for all) 
						<asp:Label id="lblDateFrom" visible="false" forecolor=red runat="server" />
					</td>						
					<td>To : </td>
					<td><asp:TextBox id="txtDateTo" width="50%" maxlength="10"  runat="server"/>
  					    <a href="javascript:PopCal('txtDateTo');">
						<asp:Image id="btnSelDateTo" runat="server" ImageUrl="../Images/calendar.gif"/></a> (leave blank for all) 
						<asp:Label id="lblDateTo" visible="false" forecolor=red runat="server" /></td>
				</tr>
				<tr>
					<td>Oil Palm Yield ID From : </td>
					<td><asp:Textbox id=txtYieldIDFrom maxlength=20 width=50% runat=server/> (leave blank for all)</td>			
					<td>To : </td>	
					<td><asp:Textbox id=txtYieldIDTo maxlength=20 width=50% runat=server/> (leave blank for all)</td>
				</tr>	
				<tr>
					<td><asp:label id=lblBlkType runat=server /></td>
					<td colspan=3><asp:DropDownList id="lstBlkType" AutoPostBack=true width="25%" runat="server" /></td>
				</tr>			
				<tr id=TrBlkGrp>
					<td><asp:label id=lblBlkGrp runat=server /></td>
					<td colspan=3><asp:textbox id="txtBlkGrp" maxlength="8" width="25%" runat="server" /> (blank for all)</td>
				</tr>				
				<tr id=TrBlk>
					<td><asp:label id=lblBlkCode runat=server /></td>
					<td colspan=3><asp:textbox id="txtBlkCode" maxlength=8 width="25%" runat="server" /> (blank for all)</td>
				</tr>
				<tr id=TrSubBlk>
					<td><asp:label id=lblSubBlkCode runat=server /></td>
					<td colspan=3><asp:textbox id="txtSubBlkCode" maxlength=8 width="25%" runat="server" /> (blank for all)</td>
				</tr>	
				<tr>
					<td width=15%>Status : </td>
					<td><asp:dropdownlist id=lstStatus width=50% runat=server>
							<asp:ListItem text="All" value="0" />
							<asp:ListItem text="Active" value="1" />
							<asp:ListItem text="Deleted" value="2" />
							<asp:ListItem text="Closed" value="3" />
						</asp:DropDownList>&nbsp;</td>			
					<td width=5%></td>
					<td width=40%>&nbsp;</td>
				</tr>																	
				<tr>
					<td colspan=4><asp:Label id="lblLocation" visible="false" runat="server" /></td>
				</tr>
				<tr>
					<td colspan=4><asp:ImageButton id="PrintPrev" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" /></td>					
				</tr>				
			</table>
        </div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</HTML>
