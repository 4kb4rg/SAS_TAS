<%@ Page Language="vb" src="../../../include/PR_setup_ComponentGajiDet_Estate.aspx.vb" Inherits="PR_setup_ComponentGajiDet_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHRSetup" src="../../menu/menu_hrsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>komponen gaji Details</title>
                <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
                <Preference:PrefHdl id=PrefHdl runat="server" />
	    <style type="text/css">
            .style1
            {
                width: 100%;
            }
            </style>
	</head>
	<body>
		
		<Form id=frmMain class="main-modul-bg-app-list-pu"  runat="server">

           <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">

			<asp:Label id="lblErrMessage" visible="false" Text="Error while initiating component." runat="server" />
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
                                 <strong> DETAIL KOMPONEN GAJI</strong>  </td>
                                <td class="font9Header"  style="text-align: right">
                                    Tgl buat : <asp:Label id=lblDateCreated runat=server />&nbsp;| Status : <asp:Label id=lblStatus runat=server />&nbsp;| Tgl update : <asp:Label id=lblLastUpdate runat=server />&nbsp;| 
                                    Update By : <asp:Label id=lblUpdatedBy runat=server />
                                </td>
                            </tr>
                        </table>
                        <hr style="width :100%" />

                    </td>
				</tr>
				<tr>
					<td colspan=6> </td>
				</tr>
										
				<tr>
					<td width=20% >
                        ID</td>
					<td style="width: 281px" >
						<asp:Textbox id=txtidx maxlength=8 width="50%" CssClass="font9Tahoma" runat=server ReadOnly="True" />
					<td style="width: 79px">&nbsp;</td>
					<td >&nbsp;</td>
					<td >&nbsp;</td>								
				</tr>				
				
				<tr>
					<td align="left">
                        Divisi </td>
					<td align="left" style="width: 281px"><GG:AutoCompleteDropDownList ID="ddldivisi" CssClass="font9Tahoma"  runat="server" Width="100%" />
                    </td>
					<td style="width: 79px">&nbsp;</td>
					<td width=20%>&nbsp;</td>
					<td width=25%>&nbsp;</td>								
				</tr>	
				
				<tr>
					<td align="left" style="height: 22px">
                        Tipe</td>
					<td align="left" style="height: 22px; width: 281px;">
                        <GG:AutoCompleteDropDownList ID="ddltype" runat="server" Width="100%" />
                    </td>
					<td style="width: 79px; height: 22px;">&nbsp;</td>
					<td style="height: 22px">&nbsp;</td>
					<td style="height: 22px">&nbsp;</td>
				</tr>
				<tr>
					<td align="left">
                        COA</td>
					<td align="left" style="width: 281px"><GG:AutoCompleteDropDownList ID="ddlcoa" CssClass="font9Tahoma"  runat="server" Width="100%" />
                    </td>
					<td style="width: 79px">&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>

				<td colspan="5" style="height: 23px">&nbsp;</td>
				<tr>
					<td colspan="5" style="height: 28px">
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn AlternateText=" Delete " imageurl="../../images/butt_delete.gif" CausesValidation=False onclick=Button_Click CommandArgument=Del runat=server />
						<asp:ImageButton id=UnDelBtn AlternateText="Undelete" imageurl="../../images/butt_undelete.gif" onclick=Button_Click CommandArgument=UnDel runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					    <br />
                        <br />
					</td>
				</tr>
				<Input Type=Hidden id=BlokCode runat=server />
				<Input Type=Hidden id=isNew runat=server />
				<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>								
			</table>
 	</div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</html>
