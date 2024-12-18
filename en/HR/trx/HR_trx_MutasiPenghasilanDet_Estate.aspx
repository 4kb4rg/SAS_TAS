<%@ Page Language="vb" src="../../../include/HR_trx_MutasiPenghasilanDet_Estate.aspx.vb" Inherits="HR_trx_MutasiPenghasilanDet_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHRSetup" src="../../menu/menu_hrsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Mutasi Details</title>
                   <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	    <style type="text/css">
            .style1
            {
                width: 100%;
            }
            </style>
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain class="main-modul-bg-app-list-pu" runat="server">
			<asp:Label id="lblErrMessage" visible="false" Text="Error while initiating component." ForeColor=red runat="server" />
                    <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
            <tr>
            <td style="width: 100%; height: 1500px" valign="top">
            <div class="kontenlist"> 
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="5">
						<UserControl:MenuHRSetup id=MenuHRSetup runat="server" />
					</td>
				</tr>
				<tr>                    
					<td class="font9Tahoma" colspan="5">
                        <table cellpadding="0" cellspacing="0" class="style1">
                            <tr>
                                <td class="font9Tahoma">
                      <strong>  DETAIL MUTASI/KOREKSI PENGAHASILAN KARYAWAN</strong></td>
                                <td class="font9Header" style="text-align: right">
                                    Tgl Buat : <asp:Label id=lblDateCreated runat=server />&nbsp;| Status : <asp:Label id=lblStatus runat=server />&nbsp;| Tgl Update : <asp:Label id=lblLastUpdate runat=server />&nbsp;| Diupdate : <asp:Label id=lblUpdatedBy runat=server />
                                </td>
                            </tr>
                        </table>
                        <hr style="width :100%" />
                    </td>
				</tr>
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>
										
				<tr>
					<td align="left" style="height: 26px; width: 181px;">
                        Kode Mutasi/Koreksi</td>
					<td align="left" style="height: 26px; width: 461px;">
                        <asp:label id="lblidM" runat="server" /></td>
					<td style="width: 30px; height: 26px;">&nbsp;</td>
					<td width=20% style="height: 26px">&nbsp;</td>
					<td width=25% style="height: 26px">&nbsp;</td>											
				</tr>				
				
				<tr>
					<td align="left" style="height: 26px; width: 181px;">
                        Divisi</td>
					<td align="left" style="height: 26px; width: 461px;">
						<GG:AutoCompleteDropDownList id=ddldivisicode width="45%" runat=server OnSelectedIndexChanged="ddldivisicode_OnSelectedIndexChanged" AutoPostBack=true/>
						<asp:label id="lbldivisilama" runat="server" Visible=False/>
                        <asp:label id="lbljabatanlama" runat="server"/></td>
					<td style="width: 30px; height: 26px;">&nbsp;</td>
					<td width=20% style="height: 26px">&nbsp;</td>
					<td width=25% style="height: 26px">&nbsp;</td>											
				</tr>	
				
				<tr>
					<td align="left" style="height: 26px; width: 181px;">
                        NIK*</td>
					<td align="left" style="height: 26px; width: 461px;">
					<GG:AutoCompleteDropDownList id=ddlempcode width="100%" runat=server OnSelectedIndexChanged="ddlempcode_OnSelectedIndexChanged" AutoPostBack=true/>
						<asp:label id="lblempcode" runat="server" Visible=False /></td>
					<td style="width: 30px; height: 26px;">&nbsp;</td>
					<td width=20% style="height: 26px">&nbsp;</td>
					<td width=25% style="height: 26px">&nbsp;</td>			
				</tr>
				<tr style="display:none;">
					<td align="left" style="height: 26px; width: 181px;">
                        Divisi / Jabatan Baru</td>
					<td align="left" style="height: 26px; width: 461px;">
					<GG:AutoCompleteDropDownList id=ddldivisibaru width="45%" runat=server/> <GG:AutoCompleteDropDownList id=ddljabatanbaru width="50%" runat=server/>
                    <asp:label id="lbldivisibaru" runat="server" Visible=False/> <asp:label id="lbljabatanbaru" runat="server" Visible=False/></td>
					<td style="width: 30px; height: 26px;">&nbsp;</td>
					<td width=20% style="height: 26px">&nbsp;</td>
					<td width=25% style="height: 26px">&nbsp;</td>			
				</tr>
				
				<tr>
					<td align="left" style="height: 26px; width: 181px;">
                        Tgl. Mutasi/Koreksi*</td>
					<td align="left" style="height: 26px; width: 461px;">
                        <asp:TextBox ID="txtefektifdate" runat="server" Width="35%"></asp:TextBox>
                        <a href="javascript:PopCal('txtefektifdate');"><asp:Image id="btnSelDOJ" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
                        </td>
					<td style="width: 30px; height: 26px;">&nbsp;</td>
					<td width=20% style="height: 26px">&nbsp;</td>
					<td width=25% style="height: 26px">&nbsp;</td>	
				</tr>
				<tr>
					<td height=25>Tipe Mutasi/Koreksi*</td>
					<td width=30%><asp:DropDownList id=ddlType width=45% runat=server OnSelectedIndexChanged="ddlType_OnSelectedIndexChanged" AutoPostBack="true" runat="server">
					                    <asp:ListItem value="" Selected>Pilih Tipe</asp:ListItem>
								        <asp:ListItem value="1">Mutasi</asp:ListItem>
						                <asp:ListItem value="2">Koreksi</asp:ListItem>
					                </asp:DropDownList>
					<asp:Label id=lblErrTrxType visible=false forecolor=red text=" Please select Type." runat=server />                
					</td>
					<td style="width: 30px; height: 26px;">&nbsp;</td>
					<td width=20% style="height: 26px">&nbsp;</td>
					<td width=25% style="height: 26px">&nbsp;</td>	
				</tr>
				<tr>
					<td align="left" style="height: 26px; width: 181px;">
                       Dari Lokasi*</td>
					<td align="left" style="height: 26px; width: 461px;">
                        <asp:TextBox ID="txtdoc" runat="server" Width="100%"></asp:TextBox>
                     </td>
					<td style="width: 30px; height: 26px;">&nbsp;</td>
					<td width=20% style="height: 26px">&nbsp;</td>
					<td width=25% style="height: 26px">&nbsp;</td>	
				</tr>
				<tr>
					<td align="left" style="height: 26px; width: 181px;">
                       Periode SPT *</td>
					<td align="left" style="height: 26px; width: 461px;">
                        <asp:DropDownList ID="ddlAccMonth" width="20%" runat="server">
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>
                            <asp:ListItem>6</asp:ListItem>
                            <asp:ListItem>7</asp:ListItem>
                            <asp:ListItem>8</asp:ListItem>
                            <asp:ListItem>9</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>11</asp:ListItem>
                            <asp:ListItem>12</asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlAccYear" width="20%" runat="server">
                        </asp:DropDownList>
                     </td>
					<td style="width: 30px; height: 26px;">&nbsp;</td>
					<td width=20% style="height: 26px">&nbsp;</td>
					<td width=25% style="height: 26px">&nbsp;</td>	
				</tr>
				<tr style="display:none;">
					<td align="left" style="height: 26px; width: 181px;">
                        Keterangan</td>
					<td align="left" style="height: 26px; width: 461px;">
                        <asp:TextBox ID="txtket" runat="server" Width="100%"></asp:TextBox>
                     </td>
					<td style="width: 30px; height: 26px;">&nbsp;</td>
					<td width=20% style="height: 26px">&nbsp;</td>
					<td width=25% style="height: 26px">&nbsp;</td>	
				</tr>
				<div id="divkoreksi" visible="False" runat="server">
				<tr>
					<td align="left" style="height: 26px; width: 181px;">
                       Gaji Netto*</td>
					<td align="left" style="height: 26px; width: 461px;">
                        <asp:TextBox ID="txtgajinetto" runat="server" Width="100%"></asp:TextBox>
                     </td>
					<td style="width: 30px; height: 26px;">&nbsp;</td>
					<td width=20% style="height: 26px">&nbsp;</td>
					<td width=25% style="height: 26px">&nbsp;</td>	
				</tr>
				</div>
				<div id="divmutasi" visible="False" runat="server">
				<tr>
					<td align="left" style="height: 26px; width: 181px;">
                       Tot.Gaji Pokok*</td>
					<td align="left" style="height: 26px; width: 461px;">
                        <asp:TextBox ID="txtgajipokok" runat="server" Width="100%"></asp:TextBox>
                     </td>
					<td style="width: 30px; height: 26px;">&nbsp;</td>
					<td width=20% style="height: 26px">&nbsp;</td>
					<td width=25% style="height: 26px">&nbsp;</td>	
				</tr>
				<tr>
					<td align="left" style="height: 26px; width: 181px;">
                       Tot.Premi*</td>
					<td align="left" style="height: 26px; width: 461px;">
                        <asp:TextBox ID="txtpremi" runat="server" Width="100%"></asp:TextBox>
                     </td>
					<td style="width: 30px; height: 26px;">&nbsp;</td>
					<td width=20% style="height: 26px">&nbsp;</td>
					<td width=25% style="height: 26px">&nbsp;</td>	
				</tr>
				<tr>
					<td align="left" style="height: 26px; width: 181px;">
                       Tot.Premi Tepat*</td>
					<td align="left" style="height: 26px; width: 461px;">
                        <asp:TextBox ID="txtpremitetap" runat="server" Width="100%"></asp:TextBox>
                     </td>
					<td style="width: 30px; height: 26px;">&nbsp;</td>
					<td width=20% style="height: 26px">&nbsp;</td>
					<td width=25% style="height: 26px">&nbsp;</td>	
				</tr>
				<tr>
					<td align="left" style="height: 26px; width: 181px;">
                       Tot.Premi Lain*</td>
					<td align="left" style="height: 26px; width: 461px;">
                        <asp:TextBox ID="txtpremilain" runat="server" Width="100%"></asp:TextBox>
                     </td>
					<td style="width: 30px; height: 26px;">&nbsp;</td>
					<td width=20% style="height: 26px">&nbsp;</td>
					<td width=25% style="height: 26px">&nbsp;</td>	
				</tr>
				<tr>
					<td align="left" style="height: 26px; width: 181px;">
                       Tot.Lembur*</td>
					<td align="left" style="height: 26px; width: 461px;">
                        <asp:TextBox ID="txtlembur" runat="server" Width="100%"></asp:TextBox>
                     </td>
					<td style="width: 30px; height: 26px;">&nbsp;</td>
					<td width=20% style="height: 26px">&nbsp;</td>
					<td width=25% style="height: 26px">&nbsp;</td>	
				</tr>
				<tr>
					<td align="left" style="height: 26px; width: 181px;">
                       Tot.Tunj Lain*</td>
					<td align="left" style="height: 26px; width: 461px;">
                        <asp:TextBox ID="txttlain" runat="server" Width="100%"></asp:TextBox>
                     </td>
					<td style="width: 30px; height: 26px;">&nbsp;</td>
					<td width=20% style="height: 26px">&nbsp;</td>
					<td width=25% style="height: 26px">&nbsp;</td>	
				</tr>
				<tr>
					<td align="left" style="height: 26px; width: 181px;">
                       Tot.Astek*</td>
					<td align="left" style="height: 26px; width: 461px;">
                        <asp:TextBox ID="txtastek" runat="server" Width="100%"></asp:TextBox>
                     </td>
					<td style="width: 30px; height: 26px;">&nbsp;</td>
					<td width=20% style="height: 26px">&nbsp;</td>
					<td width=25% style="height: 26px">&nbsp;</td>	
				</tr>
				<tr>
					<td align="left" style="height: 26px; width: 181px;">
                       Tot.CatuBeras*</td>
					<td align="left" style="height: 26px; width: 461px;">
                        <asp:TextBox ID="txtcatuberas" runat="server" Width="100%"></asp:TextBox>
                     </td>
					<td style="width: 30px; height: 26px;">&nbsp;</td>
					<td width=20% style="height: 26px">&nbsp;</td>
					<td width=25% style="height: 26px">&nbsp;</td>	
				</tr>
				<tr>
					<td align="left" style="height: 26px; width: 181px;">
                       Tot.Rapel*</td>
					<td align="left" style="height: 26px; width: 461px;">
                        <asp:TextBox ID="txtRapel" runat="server" Width="100%"></asp:TextBox>
                     </td>
					<td style="width: 30px; height: 26px;">&nbsp;</td>
					<td width=20% style="height: 26px">&nbsp;</td>
					<td width=25% style="height: 26px">&nbsp;</td>	
				</tr>
				<tr>
					<td align="left" style="height: 26px; width: 181px;">
                       Tot.THR-Bonus*</td>
					<td align="left" style="height: 26px; width: 461px;">
                        <asp:TextBox ID="txtthrbonus" runat="server" Width="100%"></asp:TextBox>
                     </td>
					<td style="width: 30px; height: 26px;">&nbsp;</td>
					<td width=20% style="height: 26px">&nbsp;</td>
					<td width=25% style="height: 26px">&nbsp;</td>	
				</tr>
				<tr>
					<td align="left" style="height: 26px; width: 181px;">
                       Tot.Pot.JBT</td>
					<td align="left" style="height: 26px; width: 461px;">
                        <asp:TextBox ID="txtpotjbt" runat="server" Width="100%"></asp:TextBox>
                     </td>
					<td style="width: 30px; height: 26px;">&nbsp;</td>
					<td width=20% style="height: 26px">&nbsp;</td>
					<td width=25% style="height: 26px">&nbsp;</td>	
				</tr>
				<tr>
					<td align="left" style="height: 26px; width: 181px;">
                       Tot.Pot.JHT/JP*</td>
					<td align="left" style="height: 26px; width: 461px;">
                        <asp:TextBox ID="txtpotjht" runat="server" Width="100%"></asp:TextBox>
                     </td>
					<td style="width: 30px; height: 26px;">&nbsp;</td>
					<td width=20% style="height: 26px">&nbsp;</td>
					<td width=25% style="height: 26px">&nbsp;</td>	
				</tr>
				<tr>
					<td align="left" style="height: 26px; width: 181px;">
                       Tot.Pot.Lain*</td>
					<td align="left" style="height: 26px; width: 461px;">
                        <asp:TextBox ID="txtpotlain" runat="server" Width="100%"></asp:TextBox>
                     </td>
					<td style="width: 30px; height: 26px;">&nbsp;</td>
					<td width=20% style="height: 26px">&nbsp;</td>
					<td width=25% style="height: 26px">&nbsp;</td>	
				</tr>
				<tr>
					<td align="left" style="height: 26px; width: 181px;">
                       Tot.Pot.PPH21*</td>
					<td align="left" style="height: 26px; width: 461px;">
                        <asp:TextBox ID="txtpph21" runat="server" Width="100%"></asp:TextBox>
                     </td>
					<td style="width: 30px; height: 26px;">&nbsp;</td>
					<td width=20% style="height: 26px">&nbsp;</td>
					<td width=25% style="height: 26px">&nbsp;</td>	
				</tr>
				</div>
				
				
				

				<td colspan="5" style="height: 23px">&nbsp;</td>
				<tr>
					<td colspan="5" style="height: 28px">
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=SaveBtn_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn AlternateText=" Delete " imageurl="../../images/butt_delete.gif" CausesValidation=False onclick=DelBtn_Click CommandArgument=Del runat=server />&nbsp;
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					    <br />
					</td>
				</tr>
				
				<tr>
					<td colspan=6>
                        </td>
				</tr>
                &nbsp;<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>								
			</table>
			<Input type=hidden id=idMDR value="" runat=server/>
			<Input type=hidden id=isNew runat=server/>
			<Input type=hidden id=idWH runat=server/>
            </div>
            </td>
            </tr>
            </table>
		</form>
	</body>
</html>
