<%@ Page Language="vb" src="../../include/reports/PD_StdRpt_ProdStat.aspx.vb" Inherits="PD_StdRpt_ProdStat" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="PD_STDRPT_SELECTION_CTRL" src="../include/reports/PD_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Production - Production Statistic Report</title>
         <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server"  class="main-modul-bg-app-list-pu" ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
        <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">

			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1" class="font9Tahoma">
				<tr>
					<td class="font9Tahoma">PRODUCTION - PRODUCTION STATISTIC REPORT</td>
					<td align="right"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan=2><hr style="width :100%" /></td>
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
					<td>Date From : </td>
					<td><asp:TextBox id="txtDateFrom" width="50%" maxlength="10" runat="server"/>
  					    <a href="javascript:PopCal('txtDateFrom');">
						<asp:Image id="btnSelDateFrom" runat="server" ImageUrl="../Images/calendar.gif"/></a> 
						<asp:Label id="lblDateFrom" visible="false" forecolor=red runat="server" />
					</td>						
					<td>To : </td>
					<td><asp:TextBox id="txtDateTo" width="50%" maxlength="10"  runat="server"/>
  					    <a href="javascript:PopCal('txtDateTo');">
						<asp:Image id="btnSelDateTo" runat="server" ImageUrl="../Images/calendar.gif"/></a> 
						<asp:Label id="lblDateTo" visible="false" forecolor=red runat="server" /></td>
				</tr>
				<tr>
					<td width=25%><asp:label id="lblLevel" runat="server" /> Level : </td>
					<td colspan=3>
						<asp:DropDownList id="ddlLevel" width="45%" autopostback=true runat="server" />						
					</td>			
				</tr>
				<tr id=TrBlkGrp runat=server visible=false>
					<td width=25%><asp:label id="lblBlkGrp" runat="server" /> : </td>
					<td colspan=3>
						<asp:DropDownList id="ddlBlkGrp" width="45%" runat="server"/> (blank for all)
					</td>			
				</tr>	
				<tr id=TrBlk runat=server visible=false>
					<td width=25%><asp:label id="lblBlk" runat="server" /> : </td>
					<td colspan=3>
						<asp:DropDownList id="ddlBlk" width="45%" runat="server"/> (blank for all)
					</td>			
				</tr>		
				<tr id=TrSubBlk runat=server visible=false>
					<td width=25%><asp:label id="lblSubBlk" runat="server" /> : </td>
					<td colspan=3>
						<asp:DropDownList id="ddlSubBlk" width="45%" runat="server"/> (blank for all)
					</td>			
				</tr>																
				<tr>
					<td colspan=4><asp:Label id="lblLocation" visible="false" runat="server" /></td>
				</tr>
				<tr>
					<td colspan=4><asp:ImageButton id="PrintPrev" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" />&nbsp;<asp:Button 
                            ID="Issue6" runat="server" class="button-small" Text="Print Preview" />
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
