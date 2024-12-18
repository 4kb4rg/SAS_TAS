<%@ Page Language="vb" src="../../../include/PR_setup_BoronganDet_Estate.aspx.vb" Inherits="PR_setup_BoronganDet_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHRSetup" src="../../menu/menu_hrsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Borongan Details</title>
             <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	    <style type="text/css">
            .style1
            {
                width: 160px;
                height: 23px;
            }
            .style2
            {
                width: 250px;
                height: 23px;
            }
            .style3
            {
                width: 41px;
                height: 23px;
            }
            .style4
            {
                height: 23px;
            }
            .style5
            {
                width: 100%;
            }
            </style>
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain class="main-modul-bg-app-list-pu"  runat="server">
           <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">

			<asp:Label id="lblErrMessage" visible="false" Text="Error while initiating component." runat="server" />
           <table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">>
				<tr>
					<td colspan="5">
						<UserControl:MenuHRSetup id=MenuHRSetup runat="server" />
					</td>
				</tr>
				<tr>                    
					<td  colspan="5">
                        <table cellpadding="0" cellspacing="0" class="style5">
                            <tr>
                                <td class="font9Tahoma">
                                 <strong>   DETAIL AKTIVITI BORONGAN</strong></td>
                                <td class="font9Header" style="text-align: right">
                                    Tgl buat : <asp:Label id=lblDateCreated runat=server />&nbsp;| Status : <asp:Label id=lblStatus runat=server />&nbsp;| Tgl Update : <asp:Label id=lblLastUpdate runat=server />&nbsp;| Diupdate : <asp:Label id=lblUpdatedBy runat=server />
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
					<td align="left" style="width: 160px">
                        Kategori :</td>
					<td align="left" style="width: 250px">
					<GG:AutoCompleteDropDownList ID="ddlkat" CssClass="font9Tahoma" runat="server" Width="100%" OnSelectedIndexChanged="ddlkat_OnSelectedIndexChanged" AutoPostBack=true/>
                    </td>
					<td style="width: 41px">&nbsp;</td>
					<td width=20%>&nbsp;</td>
					<td width=25%>&nbsp;</td>								
				</tr>	
				
				<tr>
					<td align="left" style="height: 22px; width: 160px;">
                        Sub Kategori :</td>
					<td align="left" style="height: 22px; width: 250px;">
                    <GG:AutoCompleteDropDownList ID="ddlsubkat" CssClass="font9Tahoma"  runat="server" Width="100%" OnSelectedIndexChanged="ddlsubkat_OnSelectedIndexChanged" AutoPostBack=true/>
                    </td>
					<td style="width: 41px; height: 22px;">&nbsp;</td>
					<td style="height: 22px">&nbsp;</td>
					<td style="height: 22px">&nbsp;</td>
				</tr>
				<tr>
					<td align="left" style="height: 26px; width: 160px;">
                        Kode Aktiviti :</td>
					<td align="left" style="width: 250px; height: 26px;">
					<GG:AutoCompleteDropDownList ID="ddlaktiviti" CssClass="font9Tahoma"  runat="server" Width="100%" />
                    </td>
					<td style="width: 41px; height: 26px;">&nbsp;</td>
					<td style="height: 26px">&nbsp;</td>
					<td style="height: 26px">&nbsp;</td>
				</tr>
				<tr>
					<td style="width: 160px">
                        Kode Tahun Tanam :</td>
					<td style="width: 250px">                        
					<GG:AutoCompleteDropDownList ID="ddltahun" CssClass="font9Tahoma"  runat="server" Width="100%" />
                    <td style="width: 41px">&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td align="left" style="width: 160px">
                        Periode Start-End</td>
					<td align="left" style="width: 250px">
                        <asp:Textbox id=txtpstart maxlength=6 width="30%" onkeypress="javascript:return isNumberKey(event)" CssClass="font9Tahoma"  runat=server></asp:Textbox>-<asp:TextBox
                            ID="txtpend" runat="server" MaxLength="6" onkeypress="javascript:return isNumberKey(event)"
                            Width="30%"></asp:TextBox></td>
					<td style="width: 41px">&nbsp;</td>					
					<td></td>
					<td></td>
				</tr>
				
				
				<tr>
					<td align="left" style="width: 160px">
                        Rate</td>
					<td align="left" style="width: 250px">
                        <asp:Textbox id=txtrate maxlength=64 width="50%" onkeypress="javascript:return isNumberKey(event)" CssClass="font9Tahoma"  runat=server></asp:Textbox>
					</td>
					<td style="width: 41px">&nbsp;</td>					
					<td></td>
					<td></td>
				</tr>
				
				<tr>
					<td align="left" style="width: 160px">
                        Type Karyawan</td>
					<td align="left" style="width: 250px">
                   		<asp:DropDownList ID="ddltype" runat="server" Width="50%">
                     	</asp:DropDownList>															
				
					</td>
					<td style="width: 41px">&nbsp;</td>					
					<td></td>
					<td></td>
				</tr>
				
				<tr>
					<td align="left" style="width: 160px">
                        Basis SKU</td>
					<td align="left" style="width: 250px">
                        <asp:Textbox id=txtbasis maxlength=64 width="50%" onkeypress="javascript:return isNumberKey(event)" CssClass="font9Tahoma"  runat=server></asp:Textbox>
					</td>
					<td style="width: 41px">&nbsp;</td>					
					<td></td>
					<td></td>
				</tr>
				
				<tr>
					<td align="left" style="height: 28px; width: 160px;">
                        UOM</td>
					<td align="left" style="width: 250px; height: 28px">
                        <asp:Textbox id=txtuom maxlength=8 width="50%" CssClass="font9Tahoma"   runat=server></asp:Textbox>
					</td>
					<td style="width: 41px; height: 28px;">&nbsp;</td>					
					<td style="height: 28px"></td>
					<td style="height: 28px"></td>
				</tr>
				
				<tr>
					<td align="left" style="height: 28px; width: 160px;">
                        Norma</td>
					<td align="left" style="width: 250px; height: 28px">
                        <asp:Textbox id=txtnorma maxlength=64 width="100%"  CssClass="font9Tahoma"  runat=server></asp:Textbox>
					</td>
					<td style="width: 41px; height: 28px;">&nbsp;</td>					
					<td style="height: 28px"></td>
					<td style="height: 28px"></td>
				</tr>
				
				<tr>
					<td align="left" class="style1">
                        COA Transit</td>
					<td align="left" class="style2">
					<GG:AutoCompleteDropDownList ID="ddltransit" CssClass="font9Tahoma"  runat="server" Width="100%" />
                    </td>
					<td class="style3"></td>					
					<td class="style4"></td>
					<td class="style4"></td>
				</tr>

				<tr>
					<td align="left" style="width: 160px">
                        COA Alokasi</td>
					<td align="left" style="width: 250px">
					<GG:AutoCompleteDropDownList ID="ddlalokasi" CssClass="font9Tahoma"  runat="server" Width="100%" />
                    </td>
					<td style="width: 41px">&nbsp;</td>					
					<td></td>
					<td></td>
				</tr>

				<td colspan="5" style="height: 23px">&nbsp;</td>
				<tr>
					<td colspan="5" style="height: 28px">
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn AlternateText=" Delete " imageurl="../../images/butt_delete.gif" CausesValidation=False onclick=Button_Click CommandArgument=Del runat=server />
						<asp:ImageButton id=UnDelBtn AlternateText="Undelete" imageurl="../../images/butt_undelete.gif" onclick=Button_Click CommandArgument=UnDel runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					    <br />
					</td>
				</tr>
				<Input Type=Hidden id=BlokCode runat=server />
				<Input Type=Hidden id=JobCode runat=server />
				<Input Type=Hidden id=isNew runat=server />
				<Input Type=Hidden id=txtpstartold runat=server />
				<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>								
			</table>
    	</div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</html>
