<%@ Page Language="vb" src="../../../include/HR_trx_MutasiDet_Estate.aspx.vb" Inherits="HR_trx_MutasiDet_Estate" %>
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
                      <strong>  DETAIL MUTASI</strong></td>
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
                        Kode Mutasi</td>
					<td align="left" style="height: 26px; width: 461px;">
                        <asp:label id="lblidM" runat="server" /></td>
					<td style="width: 30px"></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>								
				</tr>				
				
				<tr>
					<td align="left" style="height: 26px; width: 181px;">
                        Divisi / Jabatan Lama</td>
					<td align="left" style="height: 26px; width: 461px;">
						<GG:AutoCompleteDropDownList id=ddldivisicode width="45%" runat=server OnSelectedIndexChanged="ddldivisicode_OnSelectedIndexChanged" AutoPostBack=true/><asp:label id="lbldivisilama" runat="server" Visible=False/>
                        <asp:label id="lbljabatanlama" runat="server"/></td>
					<td style="width: 30px; height: 26px;"></td>
					<td width=20% style="height: 26px">&nbsp;</td>
					<td width=25% style="height: 26px">&nbsp;</td>								
				</tr>	
				
				<tr>
					<td align="left" style="height: 26px; width: 181px;">
                        NIK</td>
					<td align="left" style="height: 26px; width: 461px;">
					<GG:AutoCompleteDropDownList id=ddlempcode width="100%" runat=server OnSelectedIndexChanged="ddlempcode_OnSelectedIndexChanged" AutoPostBack=true/>
						<asp:label id="lblempcode" runat="server" Visible=False /></td>
					<td style="width: 30px; height: 26px;"></td>
					<td style="height: 26px">&nbsp;</td>
					<td style="height: 26px">&nbsp;</td>
				</tr>
				<tr>
					<td align="left" style="height: 26px; width: 181px;">
                        Divisi / Jabatan Baru</td>
					<td align="left" style="height: 26px; width: 461px;">
					<GG:AutoCompleteDropDownList id=ddldivisibaru width="45%" runat=server/> <GG:AutoCompleteDropDownList id=ddljabatanbaru width="50%" runat=server/>
                    <asp:label id="lbldivisibaru" runat="server" Visible=False/> <asp:label id="lbljabatanbaru" runat="server" Visible=False/></td>
					<td style="width: 30px ;height: 26px;"></td>
					<td style="height: 26px">&nbsp;</td>
					<td style="height: 26px">&nbsp;</td>
				</tr>
				
				<tr>
					<td align="left" style="height: 26px; width: 181px;">
                        Tgl.Efektif</td>
					<td align="left" style="height: 26px; width: 461px;">
                        <asp:TextBox ID="txtefektifdate" runat="server" Width="35%"></asp:TextBox>
                        <a href="javascript:PopCal('txtefektifdate');"><asp:Image id="btnSelDOJ" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
                        </td>
					<td style="width: 30px ;height: 26px;"></td>
					<td style="height: 26px"></td>
					<td style="height: 26px"></td>
				</tr>
				<tr>
					<td align="left" style="height: 26px; width: 181px;">
                       No.SK *</td>
					<td align="left" style="height: 26px; width: 461px;">
                        <asp:TextBox ID="txtdoc" runat="server" Width="100%"></asp:TextBox>
                     </td>
					<td style="width: 30px ;height: 26px;"></td>
					<td style="height: 26px"></td>
					<td style="height: 26px"></td>
				</tr>
				<tr>
					<td align="left" style="height: 26px; width: 181px;">
                        Keterangan</td>
					<td align="left" style="height: 26px; width: 461px;">
                        <asp:TextBox ID="txtket" runat="server" Width="100%"></asp:TextBox>
                     </td>
					<td style="width: 30px ;height: 26px;"></td>
					<td style="height: 26px"></td>
					<td style="height: 26px"></td>
				</tr>

				<td colspan="5" style="height: 23px">&nbsp;</td>
				<tr>
					<td colspan="5" style="height: 28px">
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=SaveBtn_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn AlternateText=" Delete " imageurl="../../images/butt_delete.gif" CausesValidation=False onclick=DelBtn_Click CommandArgument=Del runat=server />
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
