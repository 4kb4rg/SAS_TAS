<%@ Page Language="vb" src="../../include/reports/GL_StdRpt_LaporanBiayaPabrik.aspx.vb" Inherits="GL_StdRpt_LaporanBiayaPabrik" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="GL_STDRPT_SELECTION_CTRL" src="../include/reports/GL_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>General Ledger - Laporan Biaya Pabrik</title>
        <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>

             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 
			<input type=Hidden id=hidUserLocPX runat="server" NAME="hidUserLocPX"/>
			<input type=Hidden id=hidAccMonthPX runat="server" NAME="hidAccMonthPX"/>
			<input type=Hidden id=hidAccYearPX runat="server" NAME="hidAccYearPX"/>
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1"  class="font9Tahoma">
				<tr>
					<td class="mt-h" colspan="3"><strong> GENERAL LEDGER - LAPORAN BIAYA PABRIK</strong></td>
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
			<table width=100% border="0" cellspacing="2" cellpadding="1" ID="Table2"  class="font9Tahoma" runat=server>
				<tr>
					<td width="15%">Jenis Laporan : </td>
					<td colspan="4"> 
					 <asp:DropDownList width=50% id=ddlRptType runat=server>
						                <asp:ListItem value="13" Selected>Executive Summary</asp:ListItem>
						                <asp:ListItem value="1">Biaya Pengolahan</asp:ListItem>
						                <asp:ListItem value="2">Biaya Consumable</asp:ListItem>
						                <asp:ListItem value="3">Biaya Peralatan Kerja</asp:ListItem>
						                <asp:ListItem value="4">Biaya Maintenance</asp:ListItem>
						                <asp:ListItem value="10">Rekap Biaya Maintenance</asp:ListItem>
						                <asp:ListItem value="5">Biaya Workshop</asp:ListItem>
						                <asp:ListItem value="6">Biaya Alokasi</asp:ListItem>
						                <asp:ListItem value="7">Biaya Umum Dan Administrasi</asp:ListItem>
						                <asp:ListItem value="8">Capital Expenditure</asp:ListItem>
						                <asp:ListItem value="9">Laporan Biaya Transit</asp:ListItem>
						                <asp:ListItem value="11">Laporan Biaya Station</asp:ListItem>
						                <asp:ListItem value="12">Laporan Biaya Station Detail</asp:ListItem>
										<asp:ListItem value="0">Rekapitulasi Biaya</asp:ListItem>
					                </asp:DropDownList>
							</td>
					<td width="5%">&nbsp;</td>
					
				</tr>
				

				<tr>
					<td colspan="4">
                       <asp:CheckBox id="cbExcel" text=" Export To Excel" checked="false" runat="server" /></td>
				</tr>								
				<tr>
					<td colspan=6>&nbsp;</td>
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
		<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" />
		<asp:label id="lblCode" visible="false" text=" Code" runat="server" />
		<asp:label id="lblType" visible="false" text=" Type" runat="server" />
		<asp:label id="lblLocation" visible="false" runat="server" />
		<asp:label id="lblAccDesc" visible="false" runat="server" />
	</body>
</HTML>
