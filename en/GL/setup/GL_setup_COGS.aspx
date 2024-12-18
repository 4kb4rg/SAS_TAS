<%@ Page Language="vb" src="../../../include/GL_setup_COGS.aspx.vb" Inherits="GL_setup_COGS" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLSetup" src="../../menu/menu_glsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Cost Of Good Sold Account Setup Page 1</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<%--<Preference:PrefHdl id=PrefHdl runat="server" />--%>
		<Form id=frmMain runat="server" class="main-modul-bg-app-list-pu"> 
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id=lblSelect visible=false text="Select " runat=server />
			<asp:label id=lblCode text=" Code" visible=false runat=server/>
			<asp:label id=lblPleaseSelect visible=false text="Please select " runat=server />
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan=5>
						<UserControl:MenuGLSetup id=MenuGLSetup runat="server" />
					</td>
				</tr>
				<tr>
					<td class="mt-h" colspan=5>Cost Of Good Sold Account SETUP - Page 1</td>
				</tr>
				<tr>
					<td colspan=5><hr size="1" noshade></td>
				</tr>
				<tr>
					<td width=20% height=25>&nbsp;</td>
					<td width=30%>&nbsp;</td>
					<td width=5%>&nbsp;</td>
					<td width=15%>Last Updated : </td>
					<td width=30%><asp:Label id=lblLastUpdate runat=server /></td>
				</tr>
				<tr>
					<td colspan=3 height=25>&nbsp;</td>
					<td>Updated By : </td>
					<td><asp:Label id=lblUpdatedBy runat=server /></td>
				</tr>
				<tr>
					<td colspan=5>
						<table id=tblGL border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma" runat=server>
							<tr>
								<td width=22%><U><B>BIAYA LANGSUNG</B></U></td>
								<td width=30%></td>
								<td width=4%></td>
								<td width=44%></td>
							</tr>
							<tr>
								<td width=22%>PANEN DAN PENGUMPULAN :</td>
								<td width=30%>
									<asp:DropDownList id="ddlPanenDanPengumpulanAccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlPanenDanPengumpulanAccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%>PEMELIHARAAN TM :</td>
								<td width=30%>
									<asp:DropDownList id="ddlPemeliharaanTMAccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlPemeliharaanTMAccTo" size=1 width=60% runat=server />
								</td>
							</tr>				
							<tr>
								<td width=22%>PEMUPUKAN TM :</td>
								<td width=30%>
									<asp:DropDownList id="ddlPemupukanTMAccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlPemupukanTMAccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%>PENGOLAHAN PABRIK :</td>
								<td width=30%>
									<asp:DropDownList id="ddlPengolahanPabrikAccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlPengolahanPabrikAccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%>PEMELIHARAAN PABRIK :</td>
								<td width=30%>
									<asp:DropDownList id="ddlPemeliharaanPabrikAccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlPemeliharaanPabrikAccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%>PEMBELIAN TBS EXTERN :</td>
								<td width=30%>
									<asp:DropDownList id="ddlPembelianTBSExternAccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlPembelianTBSExternAccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%>PEMAKAIAN TBS INTERN :</td>
								<td width=30%>
									<asp:DropDownList id="ddlPemakaianInternAccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlPemakaianInternAccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%>SAHAM LANGSUNG YANG DIJUAL :</td>
								<td width=30%>
									<asp:DropDownList id="ddlSahamLangsungYangDijualAccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlSahamLangsungYangDijualAccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td colspan=4>&nbsp;</td>
							</tr>				
							<tr>
								<td width=22%><U><B>BIAYA TIDAK LANGSUNG</B></U></td>
								<td width=30%></td>
								<td width=4%></td>
								<td width=44%></td>
							</tr>				
							<tr>
								<td width=22%>PENYUSUK TM :</td>
								<td width=30%>
									<asp:DropDownList id="ddlPenyusukTMAccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlPenyusukTMAccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%>PENYUSUK AKT. Non TNM :</td>
								<td width=30%>
									<asp:DropDownList id="ddlPenyusukAKTAccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlPenyusukAKTAccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%>ALOKASI S.PENYUSUTAN :</td>
								<td width=30%>
									<asp:DropDownList id="ddlAlokasiSPenyusutanAccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlAlokasiSPenyusutanAccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%>KARYAWAN :</td>
								<td width=30%>
									<asp:DropDownList id="ddlKaryawanAccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlKaryawanAccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%>ADMINISTRASI KANTOR :</td>
								<td width=30%>
									<asp:DropDownList id="ddlAdministrasiKantorAccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlAdministrasiKantorAccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%>PEMERLIHARAAN :</td>
								<td width=30%>
									<asp:DropDownList id="ddlPemerliharaanAccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlPemerliharaanAccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%>PENGEMBANGAN KARYAWAN :</td>
								<td width=30%>
									<asp:DropDownList id="ddlPengembanganKaryawanAccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlPengembanganKaryawanAccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%>PERJALANAN DINAS :</td>
								<td width=30%>
									<asp:DropDownList id="ddlPerjalananDinasAccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlPerjalananDinasAccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%>UNIT LABORATORIUM :</td>
								<td width=30%>
									<asp:DropDownList id="ddlUnitLaboratoriumAccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlUnitLaboratoriumAccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%>RISET :</td>
								<td width=30%>
									<asp:DropDownList id="ddlRisetAccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlRisetAccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%>TRANSPORTASI :</td>
								<td width=30%>
									<asp:DropDownList id="ddlTransportasi1AccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlTransportasi1AccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%></td>
								<td width=30%>
									<asp:DropDownList id="ddlTransportasi2AccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlTransportasi2AccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%>UMUM LAINNYA :</td>
								<td width=30%>
									<asp:DropDownList id="ddlUmumLainyaAccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlUmumLainyaAccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%>OVERHEAD YANG DIALOKASI :</td>
								<td width=30%>
									<asp:DropDownList id="ddlOverheadYangDialokasiAccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlOverheadYangDialokasiAccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%>ELIMINASI PEMAKAIAN TBS INTERN :</td>
								<td width=30%>
									<asp:DropDownList id="ddlEliminasiPemakaianTBSInternAccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlEliminasiPemakaianTBSInternAccTo" size=1 width=60% runat=server />
								</td>
							</tr>				
							<tr>
								<td colspan=4>&nbsp;</td>
							</tr>					
							<tr>
								<td width=22%><U><B>BIAYA PRODUKSI</B></U></td>
								<td width=30%></td>
								<td width=4%></td>
								<td width=44%></td>
							</tr>				
							<tr>
								<td width=22%>TBS :</td>
								<td width=30%>
									<asp:DropDownList id="ddlBiayaProduksiTBS1AccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlBiayaProduksiTBS1AccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%></td>
								<td width=30%>
									<asp:DropDownList id="ddlBiayaProduksiTBS2AccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlBiayaProduksiTBS2AccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%></td>
								<td width=30%>
									<asp:DropDownList id="ddlBiayaProduksiTBS3AccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlBiayaProduksiTBS3AccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%></td>
								<td width=30%>
									<asp:DropDownList id="ddlBiayaProduksiTBS4AccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlBiayaProduksiTBS4AccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%></td>
								<td width=30%>
									<asp:DropDownList id="ddlBiayaProduksiTBS5AccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlBiayaProduksiTBS5AccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td colspan=4>&nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan=5>
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" CommandArgument=Save onclick=Button_Click runat=server />
						<asp:Label id=lblHasRecord visible=false text=false runat=server/>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
