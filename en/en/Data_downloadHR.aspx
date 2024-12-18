<%@ Page Language="vb" src="../include/Data_downloadHR.aspx.vb" Inherits="Data_downloadHR" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="include/preference/preference_handler.ascx"%>
<html>
	<head>
		<Preference:PrefHdl id=PrefHdl runat="server" />
        <title>Upload Reference File</title>
	</head>
	<body>
		<table border="0" width="100%" cellpadding="1" cellspacing="0">
			<tr>
				<td width="100%">
					<form id="frmMain" runat=server>
					<table id="tblDownload" border="0" cellpadding="0" cellspacing="0" width="100%" runat=server>
						<tr>
							<td class="mt-h" width="100%" colspan="4">DOWNLOAD TRANSACTION FILE</td>
						</tr>
						<tr>
							<td colspan=5><hr size="1" noshade></td>
						</tr>
						<tr>
							<td width="100%" colspan="5">Transaction Data</td>
						</tr>
						<tr>
							<td width="100%" colspan="5">&nbsp;</td>
						</tr>
						<TR>
							<TD width="100%" colSpan="5">Steps:</TD>
						</TR>
						<TR>
							<TD width="100%" colSpan="5">1.&nbsp; select data to export.</TD>
						</TR>
						<TR>
							<TD width="100%" colSpan="5">2.&nbsp; Click "Generate" button to generate the file.</TD>
						</TR>
						<tr>
							<td colspan="5">&nbsp;</td>
						</tr>
		
						<tr>
						    <td>&nbsp;Data:</td>
						    <td colspan="2">
                                &nbsp;</td>
					         <td>&nbsp;<asp:DropDownList width="75%" id=ddlTable runat=server>
								       <asp:ListItem value="0" Selected>Tipe Karyawan</asp:ListItem>
			                           <asp:ListItem value="1" >Group Jabatan</asp:ListItem>
			                           <asp:ListItem value="2" >Jabatan</asp:ListItem>
			                           <asp:ListItem value="3" >Setup Year</asp:ListItem>
			                           <asp:ListItem value="4" >Data Pegawai</asp:ListItem>
			                           <asp:ListItem value="5" >Foto Pegawai</asp:ListItem>
			                           <asp:ListItem value="6" >Data Alamat Pegawai</asp:ListItem>
			                           <asp:ListItem value="7" >Data Keluarga Pegawai</asp:ListItem>
			                           <asp:ListItem value="8" >Data Gaji</asp:ListItem>
			                           <asp:ListItem value="9" >Riwayat Pekerjaan</asp:ListItem>
			                           <asp:ListItem value="10" >Riwayat Promosi/Demosi</asp:ListItem>
			                   	       <asp:ListItem value="11" >Riwayat Mutasi</asp:ListItem>
			                   	       <asp:ListItem value="12" >Riwayat Gaji</asp:ListItem>
			                           <asp:ListItem value="13" >Riwayat Kesehatan</asp:ListItem>
			                           <asp:ListItem value="14" >Riwayat Pendidikan</asp:ListItem>
			                           <asp:ListItem value="15" >Workshop/Pelatihan</asp:ListItem>			                         	
			                           <asp:ListItem value="16" >Mandor</asp:ListItem>
			                           <asp:ListItem value="17" >Mandor LN</asp:ListItem>
			                           <asp:ListItem value="18" >Karyawan Berhenti</asp:ListItem>
			                           <asp:ListItem value="19" >Divisi</asp:ListItem>
			                           <asp:ListItem value="20" >Tahun Tanam</asp:ListItem>
			                           <asp:ListItem value="21" >Blok</asp:ListItem>
			                           <asp:ListItem value="22" >Bjr</asp:ListItem>
			                           <asp:ListItem value="23" >Daftar Gol SKU-Bulanan</asp:ListItem>
			                           <asp:ListItem value="24" >Status Gol Karyawan,UMP</asp:ListItem>
			                   	       <asp:ListItem value="25" >Kode Tanggungan</asp:ListItem>       
			                   	       <asp:ListItem value="26" >Harga Beras</asp:ListItem>
			                           <asp:ListItem value="27" >Premi Beras</asp:ListItem>
			                           <asp:ListItem value="28" >Astek</asp:ListItem>
			                           <asp:ListItem value="29" >Cut Off Gaji</asp:ListItem>
     			                       <asp:ListItem value="30" >Cut Of Gaji LN</asp:ListItem>
			                           <asp:ListItem value="31" >Kode Absensi</asp:ListItem>
			                           <asp:ListItem value="32" >Setting Kode Absensi</asp:ListItem>
			                           <asp:ListItem value="33" >Hari Libur</asp:ListItem>
			                           <asp:ListItem value="34" >Tarif Lembur</asp:ListItem>
			                           <asp:ListItem value="35" >Tabel Basis Premi dan Panen</asp:ListItem>
			                           <asp:ListItem value="36" >Denda Panen</asp:ListItem>
			                           <asp:ListItem value="37" >Premi Kerajinan</asp:ListItem>
			                           <asp:ListItem value="38" >Premi Supir</asp:ListItem>
			                           <asp:ListItem value="39" >Premi Mandor</asp:ListItem>
			                            <asp:ListItem value="40" >Premi Mandor LN</asp:ListItem>
			                           <asp:ListItem value="41" >Prem Deres</asp:ListItem>
			                           <asp:ListItem value="42" >Jenis Kendaraan</asp:ListItem>
			                           <asp:ListItem value="43" >Kategori Aktiviti</asp:ListItem>
			                           <asp:ListItem value="44" >Sub Kategori Aktiviti</asp:ListItem>
			                           <asp:ListItem value="45" >Aktiviti</asp:ListItem>
			                           <asp:ListItem value="46" >Borongan</asp:ListItem>
			                           <asp:ListItem value="47" >Komponen Gaji</asp:ListItem>
			                           <asp:ListItem value="48" >PPH 21</asp:ListItem>
			                           <asp:ListItem value="49" >PPH 21 LN</asp:ListItem>
					                </asp:DropDownList></td>
					         <td>&nbsp;</td>
						</tr>
						<tr>
							<td colspan="5">&nbsp;</td>
						</tr>
						
				        <tr>
				            <td style="width: 15%">&nbsp;</td>
					        <td colspan="4">
                                &nbsp;<asp:Textbox id="txtDate" width="100px" maxlength=10 runat=server Visible="False"/>
						        <a href="javascript:PopCal('txtDate');"><asp:Image id="btnDate" runat="server" ImageUrl="images/calendar.gif" Visible="False"/></a>&nbsp;&nbsp;&nbsp; &nbsp;<asp:Textbox id="txtDateTo" width="100px" maxlength=10 runat=server Visible="False"/>
						        <a href="javascript:PopCal('txtDateTo');"><asp:Image id="btnDateTo" runat="server" ImageUrl="images/calendar.gif" Visible="False"/></a>
					        </td>		
				        </tr>	
				        <tr>
							<td colspan="5">&nbsp;<asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label></td>
						</tr>
						
						<tr>
							<td colspan="5"><asp:ImageButton id=btnGenerate onclick=btnGenerate_Click imageurl="images/butt_generate.gif" alternatetext="Generate" runat=server />
                                </td>
						</tr>
					</table>
					
					<asp:Label id="lblErrMesage" visible="false" Text="Error while initiating component." runat="server" />
					<asp:Label id="lblDownloadfile" visible="true" runat="server" />
					</form>
				</td>
			</tr>
		</table>
	</body>
</html>
