<%@ Page Language="vb" src="../../../include/HR_trx_ResignDet_Estate.aspx.vb" Inherits="HR_trx_ResignDet_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHRSetup" src="../../menu/menu_hrsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>ReSign Details</title>
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
					<td class="font9Tahoma" colspan="5">
                        <table cellpadding="0" cellspacing="0" class="style1">
                            <tr>
                                <td class="font9Tahoma">
                     <strong>  DETAIL KARYAWAN BERHENTI</strong> </td>
                                <td  class="font9Header" style="text-align: right">
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
                        Kode Berhenti</td>
					<td align="left" style="height: 26px; width: 461px;">
                        <asp:label id="lblidM" runat="server" /></td>
					<td style="width: 30px"></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>								
				</tr>				
				
				<tr>
					<td align="left" style="height: 26px; width: 181px;">
                        Divisi </td>
					<td align="left" style="height: 26px; width: 461px;">
						<GG:AutoCompleteDropDownList id=ddldivisicode width="45%"  CssClass="font9Tahoma" runat=server OnSelectedIndexChanged="ddldivisicode_OnSelectedIndexChanged" AutoPostBack=true/><asp:label id="lbldivisilama" runat="server" Visible=False/>
                        </td>
					<td style="width: 30px; height: 26px;"></td>
					<td width=20% style="height: 26px">&nbsp;</td>
					<td width=25% style="height: 26px">&nbsp;</td>								
				</tr>	
				
				<tr>
					<td align="left" style="height: 26px; width: 181px;">
                        NIK</td>
					<td align="left" style="height: 26px; width: 461px;"><GG:AutoCompleteDropDownList id=ddlempcode width="100%"  CssClass="font9Tahoma" runat=server />
						<asp:label id="lblempcode" runat="server" Visible=False /></td>
					<td style="width: 30px; height: 26px;"></td>
					<td style="height: 26px">&nbsp;</td>
					<td style="height: 26px">&nbsp;</td>
				</tr>
				<tr>
					<td align="left" style="height: 26px; width: 181px;">
                        Kategori</td>
					<td align="left" style="height: 26px; width: 461px;">
					<asp:DropDownList id=ddlberhenti width="45%" OnSelectedIndexChanged="ddlberhenti_OnSelectedIndexChanged" AutoPostBack=true  CssClass="font9Tahoma" runat=server />
					<div id="divpt" visible="False" runat="server" class="mb-c">
                    <table border="0" cellspacing="1" cellpadding="1" width="100%">
                     <tr>
                        <td style="width: 30%">PT : </td>
						<td style="width: 70%"><asp:DropDownList ID="ddlpt" Width="100%" OnSelectedIndexChanged="ddlpt_OnSelectedIndexChanged" AutoPostBack=true  CssClass="font9Tahoma" runat="server"></asp:DropDownList></td>
                     </tr>
                     <tr>
                        <td style="width: 30%">Lokasi : </td>
						<td style="width: 100%"><asp:DropDownList ID="ddllokasi" Width="100%"  CssClass="font9Tahoma" runat="server"></asp:DropDownList></td>
                     </tr>
                     <tr>
                        <td style="width: 30%"></td><td style="width: 70%"></td>
                     </tr>
                     <tr>
                        <td style="width: 30%"></td><td style="width: 70%"></td>
                     </tr>
                     </table>
                    </div>     
					</td>
					<td style="width: 30px ;height: 26px;"></td>
					<td style="height: 26px">&nbsp;</td>
					<td style="height: 26px">&nbsp;</td>
				</tr>
				
				<tr>
					<td align="left" style="height: 26px; width: 181px;">
                        Keterangan</td>
					<td align="left" style="height: 26px; width: 461px;">
                        <asp:TextBox ID="txtket" runat="server" Width="100%" CssClass="font9Tahoma" />
                    </td>
					<td style="width: 30px ;height: 26px;"></td>
					<td style="height: 26px"></td>
					<td style="height: 26px"></td>
				</tr>

				<tr>
					<td align="left" style="height: 26px; width: 181px;">
                        Tgl Efektif</td>
					<td align="left" style="height: 26px; width: 461px;">
                        <asp:TextBox ID="txtefektifdate"  CssClass="font9Tahoma" runat="server" Width="35%"></asp:TextBox>
                        <a href="javascript:PopCal('txtefektifdate');"><asp:Image id="btnSelDOJ" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
                        </td>
					<td style="width: 30px ;height: 26px;"></td>
					<td style="height: 26px"></td>
					<td style="height: 26px"></td>
				</tr>

				<td colspan="5" style="height: 23px">&nbsp;</td>
				<tr>
					<td colspan="5" style="height: 28px">
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=SaveBtn_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn AlternateText=" Delete " visible="False" imageurl="../../images/butt_delete.gif" CausesValidation=False onclick=DelBtn_Click CommandArgument=Del runat=server />
						<asp:ImageButton id=UnDelBtn AlternateText=" Delete " visible="False" imageurl="../../images/butt_undelete.gif" CausesValidation=False onclick=DelBtn_Click CommandArgument=Del runat=server />
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
			<Input type=hidden id=idStat value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>
			<Input type=hidden id=isNew runat=server/>
            </div>
            </td>
            </tr>
            </table>
		</form>
	</body>
</html>
