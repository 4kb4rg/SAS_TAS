<%@ Page Language="vb" src="../../include/reports/PU_StdRpt_PengirimanBarangInternal.aspx.vb" Inherits="PU_StdRpt_PengirimanBarangInternal" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="PU_STDRPT_SELECTION_CTRL" src="../include/reports/PU_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Purchasing - Surat Pengiriman Barang Internal</title>
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
					<td class="font9Tahoma" colspan="3"><strong>PURCHASING - SURAT PENGIRIMAN BARANG INTERNAL</strong> </td>
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
					<td colspan="6"><UserControl:PU_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>			
				<tr>
					<td colspan="6"><asp:Label id="lblDate" forecolor=Red 
                            text="Incorrect Date Format. Date Format is " runat="server" />				
									<asp:Label id="lblDateFormat" forecolor=red visible="false" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">
                        <hr style="width :100%" /> 
                    </td>
				</tr>
			</table>
			<table width=100% border="0" cellspacing="1" cellpadding="1" ID="Table2" class="font9Tahoma" runat=server>	
				<tr>
					<td>No. Dispatch Advice :</td>
					<td><asp:DropDownList id="ddlDANoFrom" width="100%" maxlength=50 runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>				
					<td>To :</td>
					<td><asp:DropDownList id="ddlDANoTo" width="100%" maxlength=50 runat="server" /> (blank for all)</td>
				</tr>		
				<tr>
					<td>Pengirim : </td>
					<td><asp:textbox id="txtSender" maxlength=100 width="50%" runat="server" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Expedisi : </td>
					<td><asp:textbox id="txtExpedition" maxlength=100 width="50%" runat="server" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>											
				<tr>
					<td>Penerima : </td>
					<td><asp:textbox id="txtReceiver" maxlength=100 width="50%" runat="server" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>			
				<tr>
					<td>Remarks : </td>
					<td><asp:textbox id="txtRemarks" maxlength=100 width="100%" textmode="Multiline" runat="server" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>			
				<tr>
					<td colspan=4><asp:Label id="lblLocation" visible="false" runat="server" /></td>
				</tr>
				<tr>
					<td><asp:ImageButton id="PrintPrev" AlternateText="Print Preview" imageurl="../images/butt_print_preview.gif" onClick="btnPrintPrev_Click" runat="server" />&nbsp;<asp:Button 
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
	</body>
</HTML>
