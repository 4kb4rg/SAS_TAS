<%@ Page Language="vb" src="../../../include/PR_setup_PPH21Det_Estate.aspx.vb" Inherits="PR_setup_PPH21Det_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHRSetup" src="../../menu/menu_hrsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Pph21 Details</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain runat="server" class="main-modul-bg-app-list-pu">

        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">


			<asp:Label id="lblErrMessage" visible="false" Text="Error while initiating component." runat="server" />
           <table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="5">
						<UserControl:MenuHRSetup id=MenuHRSetup runat="server" />
					</td>
				</tr>
				<tr>                    
					<td class="mt-h" colspan="5">DETAIL PPH21</td>
				</tr>
				<tr>
					<td colspan=5><hr style="width :100%" /> </td>
				</tr>
										
				<tr>
					<td width=20% >
                        ID</td>
					<td style="width: 281px" >
						<asp:Textbox id=txtNoBlok maxlength=8 width="60%" runat=server />
					<td style="width: 79px">&nbsp;</td>
					<td >Tgl buat : </td>
					<td ><asp:Label id=lblDateCreated runat=server /></td>								
				</tr>				
				
				<tr>
					<td align="left">
                        Tipe Karyawan</td>
					<td align="left" style="width: 281px"><asp:DropDownList ID="ddtype" runat="server" Width="100%">
                    </asp:DropDownList></td>
					<td style="width: 79px">&nbsp;</td>
					<td width=20%>Status : </td>
					<td width=25%><asp:Label id=lblStatus runat=server /></td>								
				</tr>	
				<tr>
					<td align="left">
                        Biaya Jabatan (%)</td>
					<td align="left" style="width: 281px">
                        <asp:Textbox id=txtbjabatan_psn maxlength=64 width="30%" runat=server onkeypress="javascript:return isNumberKey(event)"></asp:Textbox>&nbsp;<a href="javascript:PopCal('txtdtplant');"></a>
                    </td>
					<td style="width: 79px">&nbsp;</td>
					<td>Tgl update : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
				</tr>
				<tr>
					<td align="left">
                        Biaya Jabatan Max</td>
					<td align="left" style="width: 281px">
                        <asp:Textbox id=txtbjabatan_max maxlength=64 width="60%" onkeypress="javascript:return isNumberKey(event)" runat=server></asp:Textbox>
					</td>
					<td style="width: 79px">&nbsp;</td>					
					<td>Diupdate : </td>
					<td><asp:Label id=lblUpdatedBy runat=server /></td>
				</tr>
				
				<tr>
					<td align="left" style="height: 28px">
                        Biaya THR %</td>
					<td align="left" style="width: 281px; height: 28px">
                        <asp:Textbox id=txtbthr_psn maxlength=64 width="30%" onkeypress="javascript:return isNumberKey(event)" runat=server></asp:Textbox>
					</td>
					<td style="width: 79px; height: 28px;">&nbsp;</td>					
					<td style="height: 28px"></td>
					<td style="height: 28px"></td>
				</tr>
				
				<tr>
					<td align="left">
                        Periode Start</td>
					<td align="left" style="width: 281px">
                        <asp:Textbox id=txtpstart maxlength=6 width="60%" onkeypress="javascript:return isNumberKey(event)" runat=server></asp:Textbox></td>
					<td style="width: 79px">&nbsp;</td>					
					<td></td>
					<td></td>
				</tr>

				<tr>
					<td align="left">
                        Periode End</td>
					<td align="left" style="width: 281px">
                        <asp:TextBox ID="txtpend" runat="server" MaxLength="6" onkeypress="javascript:return isNumberKey(event)"
                            Width="60%"></asp:TextBox></td>
					<td style="width: 79px">&nbsp;</td>					
					<td></td>
					<td></td>
				</tr>

                    <tr>
					<td align="left" style="height: 28px">
                        Level 1</td>
					<td align="left" style="height: 28px;" colspan="3">
                        min
                        <asp:TextBox ID="txt_min1" runat="server" MaxLength="64" onkeypress="javascript:return isNumberKey(event)"
                            Width="20%"></asp:TextBox>
                        max
                        <asp:TextBox ID="txt_max1" runat="server" MaxLength="64" onkeypress="javascript:return isNumberKey(event)"
                            Width="20%"></asp:TextBox>&nbsp; tax
                        <asp:TextBox ID="txt_tax1" runat="server" MaxLength="6" onkeypress="javascript:return isNumberKey(event)"
                            Width="10%"></asp:TextBox></td>
					<td style="height: 28px"></td>
				</tr>
				<tr>
					<td align="left">
                        Level 2</td>
					<td align="left" colspan="3">
                        min
                        <asp:TextBox ID="txt_min2" runat="server" MaxLength="64" onkeypress="javascript:return isNumberKey(event)"
                            Width="20%"></asp:TextBox>
                        max
                        <asp:TextBox ID="txt_max2" runat="server" MaxLength="64" onkeypress="javascript:return isNumberKey(event)"
                            Width="20%"></asp:TextBox>&nbsp; tax
                        <asp:TextBox ID="txt_tax2" runat="server" MaxLength="6" onkeypress="javascript:return isNumberKey(event)"
                            Width="10%"></asp:TextBox></td>
					<td></td>
				</tr>
				<tr>
					<td align="left">
                        Level 3</td>
					<td align="left" colspan="3">
                        min
                        <asp:TextBox ID="txt_min3" runat="server" MaxLength="64" onkeypress="javascript:return isNumberKey(event)"
                            Width="20%"></asp:TextBox>
                        max
                        <asp:TextBox ID="txt_max3" runat="server" MaxLength="64" onkeypress="javascript:return isNumberKey(event)"
                            Width="20%"></asp:TextBox>&nbsp; tax
                        <asp:TextBox ID="txt_tax3" runat="server" MaxLength="6" onkeypress="javascript:return isNumberKey(event)"
                            Width="10%"></asp:TextBox></td>
					<td></td>
				</tr>
				<tr>
					<td align="left">
                        Level 4</td>
					<td align="left" colspan="3">
                        min
                        <asp:TextBox ID="txt_min4" runat="server" MaxLength="64" onkeypress="javascript:return isNumberKey(event)"
                            Width="20%"></asp:TextBox>
                        max
                        <asp:TextBox ID="txt_max4" runat="server" MaxLength="64" onkeypress="javascript:return isNumberKey(event)"
                            Width="20%"></asp:TextBox>&nbsp; tax
                        <asp:TextBox ID="txt_tax4" runat="server" MaxLength="6" onkeypress="javascript:return isNumberKey(event)"
                            Width="10%"></asp:TextBox></td>
					<td></td>
				</tr>
				
				<td colspan="5" style="height: 23px">&nbsp;</td>
				<tr>
					<td colspan="5" style="height: 28px">
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn AlternateText=" Delete " imageurl="../../images/butt_delete.gif" CausesValidation=False onclick=Button_Click CommandArgument=Del runat=server />
						<asp:ImageButton id=UnDelBtn AlternateText="Undelete" imageurl="../../images/butt_undelete.gif" onclick=Button_Click CommandArgument=UnDel runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					</td>
				</tr>
				<Input Type=Hidden id=BlokCode runat=server />
				<Input Type=Hidden id=isNew runat=server />
				<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>								
				<tr>
					<td colspan="5" style="height: 28px">
                                            &nbsp;</td>
				</tr>
				</table>

        <br />
        </div>
        </td>
        </tr>
        </table>


		</form>
	</body>
</html>
