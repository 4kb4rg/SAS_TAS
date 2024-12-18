<%@ Page Language="vb" src="../../include/reports/BD_StdRpt_MatureCrop.aspx.vb" Inherits="BD_StdRpt_MatureCrop" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="BD_STDRPT_SELECTION_CTRL" src="../include/reports/BD_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Budgeting - Mature Crop Activity</title>
                 <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" ID="frmMain">	
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
           <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 

			<asp:Label id="lblCrop" visible="false" runat="server" />			
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1" class="font9Tahoma">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong><asp:label id="lblTitle" runat="server" /></strong> </td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6"><hr style="width :100%" />
					</td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6"><UserControl:BD_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6"><hr style="width :100%" />
					</td>
				</tr>
			</table>
			<table width="100%" border="0" cellspacing="1" cellpadding="1" ID="Table2" class="font9Tahoma" runat="server">
				<tr id=TrBlk>
					<td width=15%><asp:label id=lblBlkCodeTag runat=server /></td>
					<td width=35%><asp:textbox id="txtBlkCode" maxlength=8 width="50%" runat="server" /> (blank for all)</td>
					<td width=15%>&nbsp;</td>
					<td width=35%>&nbsp;</td>					
				</tr>
				<tr id=TrSubBlk>
					<td width=15% ><asp:label id=lblSubBlkCodeTag runat=server /></td>
					<td width=35%><asp:textbox id="txtSubBlkCode" maxlength=8 width="50%" runat="server" /> (blank for all)</td>			
					<td width=15% >&nbsp;</td>
					<td width=35%>&nbsp;</td>				
				</tr>			
				<tr>
					<td colspan=4>&nbsp;</td>
				</tr>						
				<!--<tr>
					<td colspan=5><asp:Label id="lblLocation" visible="false" runat="server" /></td>
				</tr>-->
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
		<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" />
		<asp:Label id="lblAccCodeTag" visible="false" runat="server" />
	</body>
</HTML>
