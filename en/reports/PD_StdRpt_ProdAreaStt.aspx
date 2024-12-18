<%@ Page Language="vb" src="../../include/reports/PD_StdRpt_ProdAreaStt.aspx.vb" Inherits="PD_StdRpt_ProdAreaStt" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="PD_STDRPT_SELECTION_CTRL" src="../include/reports/PD_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Production - Production Area Statement</title>
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
					<td class="font9Tahoma" colspan="3"><strong> PRODUCTION - PRODUCTION AREA STATEMENT</strong></td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6"><hr style="width :100%" /></td>
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
					<td colspan="6"><hr size="1" noshade></td>
				</tr>
			</table>
			<table width=100% border="0" cellspacing="2" cellpadding="1" ID="Table2"  class="font9Tahoma" runat=server>
				<tr id=TrDivision>
					<td><asp:label id=lblBlkGrp runat=server /> </td>
					<td colspan=3>
						<asp:textbox id="txtBlkGrp" maxlength="8" width="25%" runat="server" /> (blank for all)
					</td>
				</tr>	
				<tr id=trYrofPlanting>
					<td><asp:label id=lblBlkCode runat=server /> </td>
					<td colspan=3><asp:textbox id="txtBlkCode" maxlength=8 width="25%" runat="server" /> (blank for all)</td>
				</tr>									
				<tr>
					<td colspaN=3>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=4><asp:Label id="lblLocation" visible="false" runat="server" /></td>
				</tr>
				<tr>
					<td><asp:ImageButton id="PrintPrev" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" />&nbsp;<asp:Button 
                            ID="Issue6" runat="server" class="button-small" Text="Print Preview" />
                    </td>					
				</tr>				
			</table>
            </div>
        </td>
        </tr>
        </table>
		</form>
		<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" />
		<asp:label id="lblCode" visible="false" text=" Code" runat="server" />
	</body>
</HTML>
