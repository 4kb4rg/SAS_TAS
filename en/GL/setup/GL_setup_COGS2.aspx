<%@ Page Language="vb" src="../../../include/GL_setup_COGS2.aspx.vb" Inherits="GL_setup_COGS2" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLSetup" src="../../menu/menu_glsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Cost Of Good Sold Account Setup Page 2</title>
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
					<td class="mt-h" colspan=5>Cost Of Good Sold Account SETUP - Page 2</td>
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
								<td width=22%><U><B>PERSEDIAAN AWAL</B></U></td>
								<td width=30%></td>
								<td width=4%></td>
								<td width=44%></td>
							</tr>				
							<tr>
								<td width=22%>- CRUDE PALM OIL :</td>
								<td width=30%>
									<asp:DropDownList id="ddlPersediaanAwalCrudePalmOilAccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlPersediaanAwalCrudePalmOilAccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%>- INTI SAWIT :</td>
								<td width=30%>
									<asp:DropDownList id="ddlPersediaanAwalIntiSawitAccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlPersediaanAwalIntiSawitAccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td colspan=4>&nbsp;</td>
							</tr>				
							<tr>
								<td width=22%><U><B>PEMAKAIAN BARANG JADI</B></U></td>
								<td width=30%></td>
								<td width=4%></td>
								<td width=44%></td>
							</tr>				
							<tr>
								<td width=22%>- CRUDE PALM OIL :</td>
								<td width=30%>
									<asp:DropDownList id="ddlPemakaianBarangJadiCrudePalmOilAccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlPemakaianBarangJadiCrudePalmOilAccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%>- INTI SAWIT :</td>
								<td width=30%>
									<asp:DropDownList id="ddlPemakaianBarangJadiIntiSawitAccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlPemakaianBarangJadiIntiSawitAccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td colspan=4>&nbsp;</td>
							</tr>				
							<tr>
								<td width=22%><U><B>BAHAN LANGSUNG YANG DIJUAL</B></U></td>
								<td width=30%></td>
								<td width=4%></td>
								<td width=44%></td>
							</tr>				
							<tr>
								<td width=22%>- CRUDE PALM OIL :</td>
								<td width=30%>
									<asp:DropDownList id="ddlBahanLangsungYangDijualCrudePalmOilAccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlBahanLangsungYangDijualCrudePalmOilAccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%>- INTI SAWIT :</td>
								<td width=30%>
									<asp:DropDownList id="ddlBahanLangsungYangDijualIntiSawitAccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlBahanLangsungYangDijualIntiSawitAccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td colspan=4>&nbsp;</td>
							</tr>							
							<tr>
								<td width=22%>BEBAN POKOK PRODUK UTK DIJUAL</td>
								<td width=30%></td>
								<td width=4%></td>
								<td width=44%></td>
							</tr>
							<tr>
								<td width=22%>- CRUDE PALM OIL :</td>
								<td width=30%>
									<asp:DropDownList id="ddlBebanPokokProdukUTKDijualCrudePalmOil1AccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlBebanPokokProdukUTKDijualCrudePalmOil1AccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%></td>
								<td width=30%>
									<asp:DropDownList id="ddlBebanPokokProdukUTKDijualCrudePalmOil2AccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlBebanPokokProdukUTKDijualCrudePalmOil2AccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%></td>
								<td width=30%>
									<asp:DropDownList id="ddlBebanPokokProdukUTKDijualCrudePalmOil3AccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlBebanPokokProdukUTKDijualCrudePalmOil3AccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%></td>
								<td width=30%>
									<asp:DropDownList id="ddlBebanPokokProdukUTKDijualCrudePalmOil4AccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlBebanPokokProdukUTKDijualCrudePalmOil4AccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%></td>
								<td width=30%>
									<asp:DropDownList id="ddlBebanPokokProdukUTKDijualCrudePalmOil5AccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlBebanPokokProdukUTKDijualCrudePalmOil5AccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%></td>
								<td width=30%>
									<asp:DropDownList id="ddlBebanPokokProdukUTKDijualCrudePalmOil6AccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlBebanPokokProdukUTKDijualCrudePalmOil6AccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%></td>
								<td width=30%>
									<asp:DropDownList id="ddlBebanPokokProdukUTKDijualCrudePalmOil7AccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlBebanPokokProdukUTKDijualCrudePalmOil7AccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td colspan=4>&nbsp;</td>
							</tr>	
							<tr>
								<td width=22%>- INTI SAWIT :</td>
								<td width=30%>
									<asp:DropDownList id="ddlBebanPokokProdukUTKDijualIntiSawit1AccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlBebanPokokProdukUTKDijualIntiSawit1AccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%></td>
								<td width=30%>
									<asp:DropDownList id="ddlBebanPokokProdukUTKDijualIntiSawit2AccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlBebanPokokProdukUTKDijualIntiSawit2AccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%></td>
								<td width=30%>
									<asp:DropDownList id="ddlBebanPokokProdukUTKDijualIntiSawit3AccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlBebanPokokProdukUTKDijualIntiSawit3AccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td colspan=4>&nbsp;</td>
							</tr>				
							<tr>
								<td width=22%><U><B>PERSEDIAAN AKHIR</B></U></td>
								<td width=30%></td>
								<td width=4%></td>
								<td width=44%></td>
							</tr>				
							<tr>
								<td width=22%>- CRUDE PALM OIL :</td>
								<td width=30%>
									<asp:DropDownList id="ddlPersediaanAkhirCrudePalmOilAccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlPersediaanAkhirCrudePalmOilAccTo" size=1 width=60% runat=server />
								</td>
							</tr>	
							<tr>
								<td width=22%>- INTI SAWIT :</td>
								<td width=30%>
									<asp:DropDownList id="ddlPersediaanAkhirIntiSawitAccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlPersediaanAkhirIntiSawitAccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td colspan=4>&nbsp;</td>
							</tr>				
							<tr>
								<td width=22%><U><B>BEBAN POKOK PENJUALAN</B></U></td>
								<td width=30%></td>
								<td width=4%></td>
								<td width=44%></td>
							</tr>
							<tr>
								<td width=22%>- TBS :</td>
								<td width=30%>
									<asp:DropDownList id="ddlBebanPokokPenjualanTBS1AccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlBebanPokokPenjualanTBS1AccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%></td>
								<td width=30%>
									<asp:DropDownList id="ddlBebanPokokPenjualanTBS2AccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlBebanPokokPenjualanTBS2AccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%></td>
								<td width=30%>
									<asp:DropDownList id="ddlBebanPokokPenjualanTBS3AccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlBebanPokokPenjualanTBS3AccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%></td>
								<td width=30%>
									<asp:DropDownList id="ddlBebanPokokPenjualanTBS4AccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlBebanPokokPenjualanTBS4AccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%></td>
								<td width=30%>
									<asp:DropDownList id="ddlBebanPokokPenjualanTBS5AccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlBebanPokokPenjualanTBS5AccTo" size=1 width=60% runat=server />
								</td>
							</tr>				
							<tr>
								<td colspan=4>&nbsp;</td>
							</tr>
							<tr>
								<td width=22%>- CRUDE PALM OIL :</td>
								<td width=30%>
									<asp:DropDownList id="ddlBebanPokokPenjualanCrudePalmOil1AccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlBebanPokokPenjualanCrudePalmOil1AccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%></td>
								<td width=30%>
									<asp:DropDownList id="ddlBebanPokokPenjualanCrudePalmOil2AccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlBebanPokokPenjualanCrudePalmOil2AccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%></td>
								<td width=30%>
									<asp:DropDownList id="ddlBebanPokokPenjualanCrudePalmOil3AccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlBebanPokokPenjualanCrudePalmOil3AccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%></td>
								<td width=30%>
									<asp:DropDownList id="ddlBebanPokokPenjualanCrudePalmOil4AccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlBebanPokokPenjualanCrudePalmOil4AccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%></td>
								<td width=30%>
									<asp:DropDownList id="ddlBebanPokokPenjualanCrudePalmOil5AccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlBebanPokokPenjualanCrudePalmOil5AccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%></td>
								<td width=30%>
									<asp:DropDownList id="ddlBebanPokokPenjualanCrudePalmOil6AccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlBebanPokokPenjualanCrudePalmOil6AccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%></td>
								<td width=30%>
									<asp:DropDownList id="ddlBebanPokokPenjualanCrudePalmOil7AccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlBebanPokokPenjualanCrudePalmOil7AccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%></td>
								<td width=30%>
									<asp:DropDownList id="ddlBebanPokokPenjualanCrudePalmOil8AccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlBebanPokokPenjualanCrudePalmOil8AccTo" size=1 width=60% runat=server />
								</td>
							</tr>				
							<tr>
								<td colspan=4>&nbsp;</td>
							</tr>
							<tr>
								<td width=22%>- INTI SAWIT :</td>
								<td width=30%>
									<asp:DropDownList id="ddlBebanPokokPenjualanIntiSawit1AccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlBebanPokokPenjualanIntiSawit1AccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%></td>
								<td width=30%>
									<asp:DropDownList id="ddlBebanPokokPenjualanIntiSawit2AccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlBebanPokokPenjualanIntiSawit2AccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%></td>
								<td width=30%>
									<asp:DropDownList id="ddlBebanPokokPenjualanIntiSawit3AccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlBebanPokokPenjualanIntiSawit3AccTo" size=1 width=60% runat=server />
								</td>
							</tr>
							<tr>
								<td width=22%></td>
								<td width=30%>
									<asp:DropDownList id="ddlBebanPokokPenjualanIntiSawit4AccFrom" size=1 width=80% runat=server />
								</td>
								<td width=4%>To :</td>
								<td width=44%>
									<asp:DropDownList id="ddlBebanPokokPenjualanIntiSawit4AccTo" size=1 width=60% runat=server />
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
