<%@ Page Language="vb" src="../../include/reports/PU_StdRpt_BAPP_Pembayaran.aspx.vb" Inherits="PU_StdRpt_BAPP_Pembayaran" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="PR_STDRPT_SELECTION_CTRL" src="../include/reports/PR_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Purchasing - BAPP & Permohonan Pembayaran</title>
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
					<td class="font9Tahoma" colspan="3">PURCHASING - BAPP & PERMOHONAN PEMBAYARAN</td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6"><hr size="1" noshade></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6"><UserControl:PR_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6" style="height: 21px">&nbsp;</td>
				</tr>			
				<tr>
					<td colspan="6"><hr size="1" noshade></td>
				</tr>
			</table>
			
			<table width=100% border="0" cellspacing="1" cellpadding="1" class="font9Tahoma" ID="Table2" runat=server>	
				
				
				<tr>
					<td colspan="3"> </td>		
				</tr>
				
				<tr>
					<td>
                        Report </td>
					<td>
                        :&nbsp;<asp:DropDownList ID="ddlRpt" runat="server" Width="30%">
                            <asp:ListItem value="1">BAPP</asp:ListItem>
                            <asp:ListItem value="2">Rekap BAPP</asp:ListItem>
                            <asp:ListItem value="3">BAPP Panen</asp:ListItem>
			    <asp:ListItem value="4">BAPP Traksi</asp:ListItem>	
                        </asp:DropDownList></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>
                        Type </td>
					<td>
                        :&nbsp;<asp:DropDownList ID="ddlRptType" runat="server" Width="30%">
                            <asp:ListItem value="1">Document</asp:ListItem>
                        </asp:DropDownList></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>
                        Supplier Code </td>
					<td>
                        : <asp:TextBox ID="txtSplCode"  Width=30% runat="server"></asp:TextBox>&nbsp;
                        <input id="Find2" runat="server" causesvalidation="False" onclick="javascript:PopSupplier_New('frmMain', '', 'txtSplCode','','','','', 'False');"
                            type="button" value=" ... " />
                        ( blank for all )</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Document No </td>
					<td>: <asp:textbox id="txtDoc" width="30%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td>&nbsp;</td>
				</tr>
				<tr>
				    <td><u><b>Potongan</b></u></td>
				</tr>
				<tr>
					<td>Uang Muka / Down Payment (Rp)</td>
					<td>: <asp:textbox id="txtPotDP" Text=0 width="30%" runat="server" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Potongan Pemakaian Material (Rp)</td>
					<td>: <asp:textbox id="txtPotMaterial" Text=0 width="30%" runat="server" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Potongan Pinjaman BAPP (Rp)</td>
					<td>: <asp:textbox id="txtPotPinjaman" Text=0 width="30%" runat="server" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=4>&nbsp;<asp:CheckBox ID="cbExcel" runat="server" Checked="false" Text=" Export To Excel" /></td>
				</tr>	
				<tr>
				    <td>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="3"><asp:ImageButton id="PrintPrev" AlternateText="Print Preview" imageurl="../images/butt_print_preview.gif" onClick="btnPrintPrev_Click" runat="server" /></td>						
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
