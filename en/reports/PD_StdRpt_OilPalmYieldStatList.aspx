<%@ Page Language="vb" src="../../include/reports/PD_StdRpt_OilPalmYieldStatList.aspx.vb" Inherits="PD_StdRpt_OilPalmYieldStatList" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="PD_STDRPT_SELECTION_CTRL" src="../include/reports/PD_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Production - Oil Palm Yield Statistic Listing Report</title>
             <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu"  ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
        <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">

			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1" class="font9Tahoma">
				<tr>
					<td class="font9Tahoma">PRODUCTION - OIL PALM YIELD STATISTIC LISTING REPORT</td>
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
					<td colspan=2><asp:Label id="lblDate" forecolor=red visible="false" text="Incorrect Date Format. Date Format is " runat="server" /></td>
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
					<td>Document No From : </td>
					<td><asp:Textbox id=txtDocNoFrom maxlength=20 width=50% runat=server/> (leave blank for all)</td>			
					<td>To : </td>	
					<td><asp:Textbox id=txtDocNoTo maxlength=20 width=50% runat=server/> (leave blank for all)</td>
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
					<td width=15%>Suppress zero balance : </td>
					<td width=40%>
						<asp:RadioButton id="rbSuppYes" text="Yes" GroupName="rbSupp" runat="server" />&nbsp;			
						<asp:RadioButton id="rbSuppNo" text="No" checked="true" GroupName="rbSupp" runat="server" />
					</td>
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
