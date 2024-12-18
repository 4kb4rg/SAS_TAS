<%@ Page Language="vb" src="../../include/reports/GL_StdRpt_LaporanMREstate.aspx.vb" Inherits="GL_StdRpt_LaporanMREstate" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="GL_STDRPT_SELECTION_CTRL" src="../include/reports/GL_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>General Ledger - Managerial Report Estate</title>
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
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1">
				<tr>
					<td class="font9Tahoma" colspan="3">GENERAL LEDGER - LAPORAN MANAGERIAL KEBUN</td>
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
			<table width=100% border="0" cellspacing="2" cellpadding="1" ID="Table2" class="font9Tahoma" runat=server>
				<tr>
					<td width="15%">Jenis Laporan : </td>
					<td colspan="4"> 
					 <asp:DropDownList width=50% id=ddlRptType runat=server>
								        <asp:ListItem value="0">I.Areal Statement - I.1	Hektar Statement</asp:ListItem>
										<asp:ListItem value="1">I.Areal Statement - I.2	Jumlah Pokok</asp:ListItem>
										<asp:ListItem value="38">I.Areal Statement - I.3 Detail</asp:ListItem>
										<asp:ListItem value="2">II.Personalia & Penduduk - II.1 Komposisi Tenaga Kerja</asp:ListItem>
										<asp:ListItem value="3">II.Personalia & Penduduk - II.2 Mutasi Tanaga Kerja</asp:ListItem>
										<asp:ListItem value="4">II.Personalia & Penduduk - II.3 Hari Kerja Efektif</asp:ListItem>										
										<asp:ListItem value="5">III.Data Curah Hujan - III.1 Harian</asp:ListItem>
										<asp:ListItem value="6">III.Data Curah Hujan - III.2 PerBulan Dalam 5 Tahun Terakhir</asp:ListItem>
										<asp:ListItem value="7">IV.Produksi TBS - IV.1.a Statistik Produksi PerBlok</asp:ListItem>
										<asp:ListItem value="8">IV.Produksi TBS - IV.1.b Statistik Produksi Yield</asp:ListItem>
										<asp:ListItem value="9">IV.Produksi TBS - IV.1.c Statistik Produksi Harian Panen PerBlok</asp:ListItem>
										<asp:ListItem value="39">IV.Produksi TBS - IV.1.d Statistik Produksi Rekap</asp:ListItem>
										<asp:ListItem value="40">IV.Produksi TBS - IV.1.e Statistik Produksi Detail</asp:ListItem>
										<asp:ListItem value="10">IV.Produksi TBS - IV.2.a Analisa Output Tenaga Kerja Pemanen</asp:ListItem>
										<asp:ListItem value="11">IV.Produksi TBS - IV.2.b Analisa Output Tenaga Kerja Pembrondol</asp:ListItem>
										<asp:ListItem value="12">IV.Produksi TBS - IV.3 Laporan Restan</asp:ListItem>
										<asp:ListItem value="13">IV.Produksi TBS - IV.4 Laporan Pengiriman TBS</asp:ListItem>
										<asp:ListItem value="14">IV.Produksi TBS - IV.5a Biaya Panen</asp:ListItem>
										<asp:ListItem value="15">IV.Produksi TBS - IV.56 Biaya Transport Buah</asp:ListItem>
										<asp:ListItem value="16">V.Pemeliharaan Tanaman - V.1.a Rekap Pemeliharaan Tanaman TM</asp:ListItem>
										<asp:ListItem value="17">V.Pemeliharaan Tanaman - V.1.b Rekap Pemeliharaan Tanaman TBM3</asp:ListItem>
										<asp:ListItem value="18">V.Pemeliharaan Tanaman - V.1.c Rekap Pemeliharaan Tanaman TBM2</asp:ListItem>
										<asp:ListItem value="19">V.Pemeliharaan Tanaman - V.1.d Rekap Pemeliharaan Tanaman TBM1</asp:ListItem>
						                <asp:ListItem value="20">V.Pemeliharaan Tanaman - V.2.a Rencana & Realisasi Pemupukan PerBlok TM</asp:ListItem>
										<asp:ListItem value="21">V.Pemeliharaan Tanaman - V.2.b Rencana & Realisasi Pemupukan PerBlok TBM</asp:ListItem>
										<asp:ListItem value="24">V.Pemeliharaan Tanaman - V.3.a Rekap Aplikasi Pemupukan TM</asp:ListItem>
										<asp:ListItem value="25">V.Pemeliharaan Tanaman - V.3.b Rekap Aplikasi Pemupukan TBM</asp:ListItem>
										<asp:ListItem value="26">V.Pemeliharaan Tanaman - V.4.a Rekap Aplikasi Pemupukan Extra TM</asp:ListItem>
										<asp:ListItem value="27">V.Pemeliharaan Tanaman - V.4.b Rekap Aplikasi Pemupukan Extra TBM</asp:ListItem>
										<asp:ListItem value="28">VI.LC dan Tanam - VI.1 Laporan Kemajuan LC dan Tanam Baru</asp:ListItem>
										<asp:ListItem value="29">VI.LC dan Tanam - VI.2 Laporan Thining OUT dan Tanam Sisip</asp:ListItem>
										<asp:ListItem value="30">VII.Bibitan - VII.1 Persediaan Bibit</asp:ListItem>
										<asp:ListItem value="31">VII.Bibitan - VII.2 Seleksi Bibit</asp:ListItem>
										<asp:ListItem value="32">VIII.Traksi & Transport - VIII.1 Utility Unit</asp:ListItem>
										<asp:ListItem value="33">VIII.Traksi & Transport - VIII.2 Hari Efektif Unit</asp:ListItem>
										<asp:ListItem value="34">IX.SPK</asp:ListItem>
										<asp:ListItem value="35">X.Gudang - X.1 Stock Pupuk dan AgroChemical</asp:ListItem>
										<asp:ListItem value="36">X.Gudang - X.2 Distribusi Pupuk dan AgroChemical</asp:ListItem>
										<asp:ListItem value="37">X.Gudang - X.3 Rincian Persediaan</asp:ListItem>
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
