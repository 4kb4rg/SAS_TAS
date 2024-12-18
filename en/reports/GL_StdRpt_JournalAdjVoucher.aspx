<%@ Page Language="vb" src="../../include/reports/GL_StdRpt_JournalAdjVoucher.aspx.vb" Inherits="GL_StdRpt_JournalAdjVoucher" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="GL_STDRPT_SELECTION_CTRL" src="../include/reports/GL_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>General Ledger - Journal Adjustment Voucher</title>
                 <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu"  ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
           <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 

			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1">
				<tr>
					<td class="font9Tahoma" colspan="3">GENERAL LEDGER - JOURNAL ADJUSTMENT VOUCHER</td>
					<td align="right" colspan="3"></td>
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
					<td colspan="6"><UserControl:GL_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
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
			<table width=100% border="0" cellspacing="1" cellpadding="1" ID="Table2"  class="font9Tahoma" runat=server>	
				<tr>
					<td>Journal Adjustment ID From :</td>
					<td><asp:textbox id="txtJournalAdjIDFrom" width="50%" runat="server" /> (blank for all)</td>
					<td>To :</td>
					<td><asp:textbox ID="txtJournalAdjIDTo" width="50%" Runat=server /> (blank for all)</td>
				</tr>			
				<tr>
					<td>Accounting Period :</td>
					<td colspan=3><asp:DropDownList id="ddlAccMonth" runat="server" /> / <asp:DropDownList id="ddlAccYear" AutoPostBack=true OnSelectedIndexChanged=OnIndexChage_ReloadAccPeriod runat="server" /></td>
				</tr>			
				<tr>
					<td width=15%>Status :</td>
					<td width=35%><asp:DropDownList id="lstStatus" size="1" width="50%" runat="server" /></td>
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
					<td><asp:ImageButton id="PrintPrev" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" />&nbsp;<asp:Button 
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
	</body>
</HTML>
