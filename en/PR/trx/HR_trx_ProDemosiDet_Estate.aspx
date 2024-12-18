<%@ Page Language="vb" src="../../../include/HR_trx_ProDemosiDet_Estate.aspx.vb" Inherits="HR_trx_ProDemosiDet_Estate" %>
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
            <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
            <tr>
            <td style="width: 100%; height: 1500px" valign="top">
            <div class="kontenlist"> 
			<asp:Label id="lblErrMessage" visible="false" Text="Error while initiating component." ForeColor=red runat="server" />
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="5">
						<UserControl:MenuHRSetup id=MenuHRSetup runat="server" />
					</td>
				</tr>
				<tr>                    
					<td class="mt-h" colspan="5">
                        <table cellpadding="0" cellspacing="0" class="style1">
                            <tr>
                                <td class="font9Tahoma">
                    <strong>   DETAIL PROMOSI/DEMOSI KARYAWAN </strong> </td>
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
                        Kode Promosi/Demosi</td>
					<td align="left" style="height: 26px; width: 461px;">
                        <asp:label id="lblidM" runat="server" /></td>
					<td style="width: 30px"></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>								
				</tr>				
				
				<tr>
					<td align="left" style="height: 26px; width: 181px;">
                        Divisi</td>
					<td align="left" style="height: 26px; width: 461px;">
					<GG:AutoCompleteDropDownList id=ddlempdivisi width="100%" runat=server OnSelectedIndexChanged="ddlempdivisi_OnSelectedIndexChanged" AutoPostBack=true CssClass="font9Tahoma"/></td>
					<td style="width: 30px; height: 26px;"></td>
					<td width=20% style="height: 26px">&nbsp;</td>
					<td width=25% style="height: 26px">&nbsp;</td>								
				</tr>	
				
				<tr>
					<td align="left" style="height: 26px; width: 181px;">
                        Nama</td>
					<td align="left" style="height: 26px; width: 461px;">
					<GG:AutoCompleteDropDownList id=ddlempcode width="100%" runat=server OnSelectedIndexChanged="ddlempcode_OnSelectedIndexChanged"  AutoPostBack=true CssClass="font9Tahoma"/></td>
					<td style="width: 30px; height: 26px;"></td>
					<td style="height: 26px">&nbsp;</td>
					<td style="height: 26px">&nbsp;</td>
				</tr>
				<tr>
					<td align="left" style="height: 26px; width: 181px;">
                        Tipe Karyawan</td>
					<td align="left" style="height: 26px; width: 461px;">
                        <asp:TextBox ID="txtemptype" runat="server" Width="25%" ReadOnly="True" CssClass="font9Tahoma"></asp:TextBox></td>
					<td style="width: 30px ;height: 26px;"></td>
					<td style="height: 26px">&nbsp;</td>
					<td style="height: 26px">&nbsp;</td>
				</tr>
				
				<tr>
					<td align="left" style="height: 26px; width: 181px;">
                        Golongan</td>
					<td align="left" style="height: 26px; width: 461px;">
                        <asp:CheckBox ID="chkempgol" runat="server" Text=" " Enabled="False" />
                        <asp:TextBox ID="txtempgol" runat="server" Width="30%" ReadOnly="True" CssClass="font9Tahoma"></asp:TextBox></td>
					<td style="width: 30px ;height: 26px;"></td>
					<td style="height: 26px"></td>
					<td style="height: 26px"></td>
				</tr>
				<tr>
					<td align="left" style="height: 26px; width: 181px;" valign="top">
                        Jabatan</td>
					<td align="left" style="height: 26px; width: 461px;">
                        <asp:TextBox ID="txtempjabatan" runat="server" Width="100%" ReadOnly="True" CssClass="font9Tahoma"></asp:TextBox></td>
					<td style="width: 30px ;height: 26px;"></td>
					<td style="height: 26px"></td>
					<td style="height: 26px"></td>
				</tr>

                <tr>
                <td colspan="6"></td>
                </tr>
			

                <tr>
                <td colspan="6">
				<table id="TabPDmosi" class="mb-c" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
				<tr>
				    <td style="height: 22px"></td>
					<td style="width: 116px; height: 22px;">
                        Promosi / Demosi
                    </td>
					<td style="width: 218px; height: 22px;"><asp:DropDownList ID="ddlstatus" runat="server" Width="100%" CssClass="font9Tahoma">
                        <asp:ListItem Selected="True" Value="1">Promosi</asp:ListItem>
                        <asp:ListItem Value="2">Demosi</asp:ListItem>
                    </asp:DropDownList>
					
                    </td>
                     
					<td style="width: 24px; height: 22px;">&nbsp;</td>
					<td style="width: 124px; height: 22px;">
                    </td>
					<td style="width: 157px; height: 22px;"></td>
				</tr>
				
				<tr>
				<td  style="height: 22px"></td>
					<td style="width: 116px; height: 22px;">
                        Tipe Karyawan Baru :</td>
					<td style="width: 218px; height: 22px;"><GG:AutoCompleteDropDownList ID="ddlemptype_baru" CssClass="font9Tahoma" runat="server" Width="100%" OnSelectedIndexChanged="ddlemptype_baru_OnSelectedIndexChanged" AutoPostBack=true/>
                    </td>
                     
					<td style="width: 24px; height: 22px;">&nbsp;</td>
					<td style="width: 124px; height: 22px;">
                        Kode Gaji :
                    </td>
					<td style="width: 157px; height: 22px;">
                        <asp:TextBox ID="txtempsalary_baru" runat="server" MaxLength="18" oldvalue="" onkeyup="FormatCurrency(this);"
                            Width="100%" CssClass="font9Tahoma" ReadOnly="True" ></asp:TextBox></td>
				</tr>
				<tr>
				<td  style="height: 19px"></td>
					<td style="width: 116px; height: 19px;">
                        Jabatan Baru</td>
					<td style="width: 218px; height: 19px;"><GG:AutoCompleteDropDownList ID="ddlempjabatan_baru" runat="server" Width="100%" CssClass="font9Tahoma"/></td>
					<td style="width: 24px; height: 19px;"></td>
					<td style="width: 124px; height: 19px;"></td>
					<td style="width: 157px; height: 19px;">
					<asp:CheckBox ID="chkempgol_baru" runat="server" Text="Sesuai Golongan" Enabled="False" /></td>
				</tr>
				<tr>
				<td  style="height: 20px"></td>
					<td style="width: 116px; height: 20px;">
                        No.Dokument</td>
					<td style="width: 218px; height: 20px;"><asp:TextBox id=txtempdoc_baru width="100%" maxlength=64 runat=server Height="23px" CssClass="font9Tahoma"/></td>
					<td style="width: 24px; height: 20px;">&nbsp;</td>
					<td style="width: 124px; height: 20px;">
                        Kode Golongan :</td>
					<td style="width: 157px; height: 20px;"><GG:AutoCompleteDropDownList ID="ddlempgol_baru" runat="server" Width="100%" Enabled="False" OnSelectedIndexChanged="ddlempgol_baru_OnSelectedIndexChanged" AutoPostBack=true CssClass="font9Tahoma"/></td>
				</tr>
				<tr>
				<td  style="height: 24px"></td>
					<td style="width: 116px; height: 24px;">
                        Tgl. Efektif</td>
					<td style="width: 218px; height: 24px;">
					    <asp:TextBox id=txtemptgl_baru width="75%" maxlength=64 runat=server Height="23px" CssClass="font9Tahoma"/>
                        <a href="javascript:PopCal('txtemptgl_baru');"><asp:Image id="btnSelDOJ" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
                     </td>
					<td style="width: 24px; height: 24px;">&nbsp;</td>
					<td style="width: 124px; height: 24px;">
                        Gaji Tetap</td>
					<td style="width: 157px; height: 24px;">
                        <asp:TextBox ID="txtempgaji_baru" runat="server" MaxLength="18" oldvalue="" onkeyup="FormatCurrency(this);"
                            Width="100%" CssClass="font9Tahoma" ReadOnly="True" ></asp:TextBox></td>
				</tr>
                     <tr>
                         <td style="height: 24px" width="20">
                         </td>
                         <td style="width: 116px; height: 24px">
                             Keterangan</td>
                         <td style="width: 218px;" rowspan="7" valign="top">
                             <asp:TextBox ID="txtempket_baru" runat="server" Height="141px" TextMode="MultiLine" Width="100%" CssClass="font9Tahoma"></asp:TextBox></td>
                         <td style="width: 24px; height: 24px">
                         </td>
                         <td style="width: 124px; height: 24px">
                             Premi Tetap</td>
                         <td style="width: 157px; height: 24px">
                             <asp:TextBox ID="txtemppremi_baru" CssClass="font9Tahoma" runat="server" MaxLength="18" oldvalue="" onkeyup="FormatCurrency(this);"
                                 Width="100%"></asp:TextBox></td>
                     </tr>
                     <tr>
                         <td style="height: 24px" width="20">
                         </td>
                         <td style="width: 116px; height: 24px">
                         </td>
                         <td style="width: 24px; height: 24px">
                         </td>
                         <td style="width: 124px; height: 24px">
                             Tunjangan</td>
                         <td style="width: 157px; height: 24px">
                             <asp:TextBox ID="txtemptunj_baru" CssClass="font9Tahoma" runat="server" MaxLength="18" oldvalue="" onkeyup="FormatCurrency(this);"
                                 Width="100%"></asp:TextBox></td>
                     </tr>
                     <tr>
                         <td style="height: 23px" width="20">
                         </td>
                         <td style="width: 116px; height: 23px">
                             </td>
                         <td style="width: 24px; height: 23px">
                         </td>
                         <td style="width: 124px; height: 23px">
                             Upah / Hari
                         </td>
                         <td style="width: 157px; height: 23px">
                             <asp:TextBox ID="txtempupah_baru" runat="server" MaxLength="18" oldvalue="" onkeyup="FormatCurrency(this);"
                                 Width="100%" CssClass="font9Tahoma" ReadOnly="True"></asp:TextBox></td>

                         <td style="width: 124px; height: 22px">
                             &nbsp;</td>
                         <td style="width: 157px; height: 22px">
                             &nbsp;</td>
                     </tr>
                     <tr>
                         <td style="height: 23px" width="20">
                             &nbsp;</td>
                         <td style="width: 116px; height: 23px">
                             &nbsp;</td>
                         <td style="width: 24px; height: 23px">
                             &nbsp;</td>
                         <td style="width: 124px; height: 23px">
                             Uang Makan </td>
                         <td style="width: 157px; height: 23px">
                             <asp:TextBox ID="txtempplain_baru" CssClass="font9Tahoma" runat="server" MaxLength="18" oldvalue="" onkeyup="FormatCurrency(this);"
                                 Width="100%"></asp:TextBox></td>
                     </tr>
                     <tr>
                         <td style="height: 23px" width="20">
                         </td>
                         <td style="width: 116px; height: 23px">
                             </td>
                         <td style="width: 24px; height: 23px">
                         </td>
                         <td style="width: 124px; height: 23px">
                             Gaji Kecil/Pinjaman</td>
                         <td style="width: 157px; height: 23px">
                             <asp:TextBox ID="txtemppjmm_baru" runat="server" MaxLength="18" oldvalue="" onkeyup="FormatCurrency(this);"
                                 Width="100%" CssClass="font9Tahoma" ></asp:TextBox></td>
                     </tr>
                     <tr>
                         <td style="height: 23px" width="20">
                         </td>
                         <td style="width: 116px; height: 23px">
                         </td>
                         <td style="width: 24px; height: 23px">
                         </td>
                         <td style="width: 124px; height: 23px">
                        Min HK</td>
                         <td style="width: 157px; height: 23px">
                             <asp:TextBox ID="txtempmhk_baru" runat="server" MaxLength="18" oldvalue="" onkeyup="FormatCurrency(this);"
                                 Width="100%" CssClass="font9Tahoma" ReadOnly="True"></asp:TextBox></td>
                     </tr>
                     <tr>
                         <td style="height: 22px" width="20">
                         </td>
                         <td style="width: 116px; height: 22px">
                             </td>
                         <td style="width: 24px; height: 22px">
                         </td>
                         <td style="width: 124px; height: 22px">
                             Potongan SPSI</td>
                         <td style="width: 157px; height: 22px">
                             <asp:TextBox ID="txtempspsi_baru" CssClass="font9Tahoma" runat="server" MaxLength="18" oldvalue="" onkeyup="FormatCurrency(this);"
                                 Width="100%"></asp:TextBox></td>
                     </tr>
				
				     <tr>
                         <td style="height: 22px" width="20">
                         </td>
                         <td style="width: 116px; height: 22px">
                             </td>
                         <td style="width: 218px; height: 22px"></td>
                         <td style="width: 24px; height: 22px">
                         </td>
                         <td style="width: 124px; height: 22px">
                             Harga Beras</td>
                         <td style="width: 157px; height: 22px">
                             <asp:TextBox ID="txtempberas_baru" runat="server" MaxLength="18" oldvalue="" onkeyup="FormatCurrency(this);"
                                 Width="100%" CssClass="font9Tahoma" ReadOnly="True"></asp:TextBox></td>
                     </tr>
				
				     <tr>
                         <td style="height: 22px" width="20">
                         </td>
                         <td style="width: 116px; height: 22px">
                             </td>
                         <td style="width: 218px; height: 22px"></td>
                         <td style="width: 24px; height: 22px">
                         </td>
                         <td style="width: 124px; height: 22px">
                             Lembur/Hari</td>
                         <td style="width: 157px; height: 22px">
                             <asp:TextBox ID="txtempovt_baru" runat="server" MaxLength="18" oldvalue="" onkeyup="FormatCurrency(this);"
                                 Width="100%" CssClass="font9Tahoma" ReadOnly="True"></asp:TextBox></td>
                     </tr>
                    <tr>
                        <td style="height: 22px" width="20">
                        </td>
                        <td style="width: 116px; height: 22px">
                        </td>
                        <td style="width: 218px; height: 22px">
                        </td>
                        <td style="width: 24px; height: 22px">
                        </td>
                        <td colspan="2" style="height: 22px">
                             <asp:CheckBox ID="chkempcatu_baru" runat="server" Text=" Catu Beras " />
                             <asp:CheckBox ID="chkempspsi_baru"  runat="server" Enabled="True" Text=" SPSI" />
                             <asp:CheckBox ID="chkempbonus_baru" runat="server" Text=" Bonus " /></td>
                    </tr>
				
				     <tr>
                         <td style="height: 22px" width="20">
                         </td>
                         <td style="width: 116px; height: 22px">
                             </td>
                         <td style="width: 218px; height: 22px"></td>
                         <td style="width: 24px; height: 22px">
                         </td>
                         <td colspan="2" style="height: 22px">
                             <asp:CheckBox ID="chkempastek_baru" runat="server" Enabled="True" Text=" BPJS-JKK" />&nbsp;
                             <asp:CheckBox ID="chkempastekJKM_baru" runat="server" Enabled="True" Text=" BPJS-JKM" />&nbsp;
                             <asp:CheckBox ID="chkempastekJHT_baru" runat="server" Enabled="True" Text=" BPJS-JHT" />&nbsp;
							 <asp:CheckBox ID="chkempbpjs_baru" runat="server" Enabled="True" Text=" BPJS-JPK" />&nbsp;
							 <asp:CheckBox ID="chkempjp_baru" runat="server" Enabled="True" Text=" BPJS-JP" />&nbsp;&nbsp;
                             &nbsp;
                          </td>
                     </tr>
				
            </table>
				</td>
				</tr>
				
				<tr>
					<td colspan="5" style="height: 28px">
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " OnClick="SaveBtn_Click" imageurl="../../images/butt_save.gif" CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn AlternateText=" Delete " OnClick="DelBtn_Click" imageurl="../../images/butt_delete.gif" CausesValidation=False CommandArgument=Del runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " OnClick="BackBtn_Click" imageurl="../../images/butt_back.gif" CausesValidation=False runat=server />
					    <br />
					</td>
				</tr>
				
				<tr>
					<td colspan=6>
                        </td>
				</tr>
                &nbsp;<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>								
			</table>
			<Input type=hidden id=idPayHist value="" runat=server/>
			<Input type=hidden id=idWrkHist value="" runat=server/>
			<Input type=hidden id=isNew value="" runat=server/>
        </div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</html>
