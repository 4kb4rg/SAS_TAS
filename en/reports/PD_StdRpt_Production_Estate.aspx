<%@ Page Language="vb" src="../../include/reports/PD_StdRpt_Production_Estate.aspx.vb" Inherits="PD_StdRpt_Production_Estate" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="PD_STDRPT_SELECTION_CTRL" src="../include/reports/PD_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Production Report</title>
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" ID="frmMain">
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1">
				<tr>
					<td class="mt-h">PRODUCTION - ESTATE REPORT</td>
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
			<table width=100% border="0" cellspacing="1" cellpadding="1" ID="Table2" runat=server>	
			   <tr>
					<td>Tangga; : </td>
					<td><asp:TextBox id="txtDateFrom" width="50%" maxlength="10" runat="server"/>
  					    <a href="javascript:PopCal('txtDateFrom');">
						<asp:Image id="btnSelDateFrom" runat="server" ImageUrl="../Images/calendar.gif"/></a> 
						<asp:Label id="lblDateFrom" visible="false" forecolor=red runat="server" />
					</td>						
					<td>s/d : </td>
					<td><asp:TextBox id="txtDateTo" width="50%" maxlength="10"  runat="server"/>
  					    <a href="javascript:PopCal('txtDateTo');">
						<asp:Image id="btnSelDateTo" runat="server" ImageUrl="../Images/calendar.gif"/></a> 
						<asp:Label id="lblDateTo" visible="false" forecolor=red runat="server" /></td>
				</tr>
				<tr>
					<td width=25%>Tipe : </td>
					<td colspan=3>
						<asp:DropDownList id="ddlTipe" width="45%" runat="server" >	
						<asp:ListItem value="D" Selected=True>Harian</asp:ListItem>						
						<asp:ListItem value="W" >Mingguan</asp:ListItem>						
						<asp:ListItem value="M" >Bulanan</asp:ListItem>						
						<asp:ListItem value="Y" >Tahunan</asp:ListItem>						
						</asp:DropDownList>
					</td>			
				</tr>
				
				<tr>
					<td colspan="4">
                       <asp:CheckBox id="cbExcel" text=" Export To Excel" checked="false" runat="server" /></td>
				</tr>	
				<tr>
					<td colspan=4><asp:Label id="lblLocation" visible="false" runat="server" /></td>
				</tr>
				<tr>
					<td colspan=4><asp:ImageButton id="PrintPrev" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" /></td>					
				</tr>				
			</table>
		</form>
	</body>
</HTML>
