<%@ Page Language="vb" src="../../../include/PR_setup_PrmKaretDet_Estate.aspx.vb" Inherits="PR_setup_PrmKaretDet_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHRSetup" src="../../menu/menu_hrsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Block of Division Details</title>
                <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
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
					<td   colspan="5"><strong> DETAIL PREMI DERES</strong></td>
				</tr>
				<tr>
					<td colspan=6 style="height: 38px">
                    <hr style="width :100%" />
                    </td>
				</tr>
				
				<tr>
					<td align="left" style="width: 163px">
                        Periode Start</td>
					<td align="left">
                        <asp:Textbox id=txtpstart maxlength=6 width="40%" runat=server onkeypress="javascript:return isNumberKey(event)"></asp:Textbox></td>
					<td style="width: 79px">&nbsp;</td>
					<td style="width: 191px">Tgl buat : </td>
					<td width=25%><asp:Label id=lblDateCreated runat=server /></td>								
				</tr>	
				
				<tr>
					<td align="left" style="height: 22px; width: 163px;">
                        Periode End</td>
					<td align="left" style="height: 22px">
                        <asp:TextBox ID="txtpend" runat="server" MaxLength="6" Width="40%" onkeypress="javascript:return isNumberKey(event)"></asp:TextBox></td>
					<td style="width: 79px; height: 22px;">&nbsp;</td>
					<td style="height: 22px; width: 191px;">Status : </td>
					<td style="height: 22px"><asp:Label id=lblStatus runat=server /></td>
				</tr>
				<tr>
					<td align="left" style="width: 163px; height: 28px;">
                        Basis Borong KKK</td>
					<td align="left" style="height: 28px">
                        <asp:TextBox ID="txt_kkk_bss" runat="server" MaxLength="64" onkeypress="javascript:return isNumberKey(event)"
                            Width="20%"></asp:TextBox>
                        Kg</td>
					<td style="width: 79px; height: 28px;">&nbsp;</td>
					<td style="height: 28px; width: 191px;">Tgl Update : </td>
					<td style="height: 28px"><asp:Label id=lblLastUpdate runat=server /></td>
				</tr>
				<tr>
					<td style="width: 163px">
                        Basis Borong LM</td>
					<td>                        
                        <asp:TextBox ID="txt_lm_bss" runat="server" MaxLength="64" onkeypress="javascript:return isNumberKey(event)"
                            Width="20%"></asp:TextBox>
                        Kg<td style="width: 79px">&nbsp;</td>
					<td style="width: 191px">Diupdate : </td>
					<td><asp:Label id=lblUpdatedBy runat=server /></td>
				</tr>
				<tr>
					<td align="left" style="height: 23px; width: 163px;">
                        Over Basis KKK</td>
					<td align="left" style="height: 23px">
                        <asp:TextBox ID="txt_KKK_ob" runat="server" MaxLength="64" onkeypress="javascript:return isNumberKey(event)"
                            Width="20%"></asp:TextBox>
                        Rp</td>
					<td style="width: 79px; height: 23px;">&nbsp;</td>					
					<td style="height: 23px; width: 191px;"></td>
					<td style="height: 23px"></td>
				</tr>

                <tr>
					<td align="left" style="height: 23px; width: 163px;">
                        Over Basis LM</td>
					<td align="left" style="height: 23px">
                        <asp:TextBox ID="txt_LM_ob" runat="server" MaxLength="64" onkeypress="javascript:return isNumberKey(event)"
                            Width="20%"></asp:TextBox>
                        Rp</td>
					<td style="width: 79px; height: 23px;">&nbsp;</td>					
					<td style="height: 23px; width: 191px;"></td>
					<td style="height: 23px"></td>
				</tr>
				
				<tr>
					<td align="left" style="height: 23px; width: 163px;">
                        Over Basis GT</td>
					<td align="left" style="height: 23px">
                        <asp:TextBox ID="txt_gt_ob" runat="server" MaxLength="64" onkeypress="javascript:return isNumberKey(event)"
                            Width="20%"></asp:TextBox>
                        Rp</td>
					<td style="width: 79px; height: 23px;">&nbsp;</td>					
					<td style="height: 23px; width: 191px;"></td>
					<td style="height: 23px"></td>
				</tr>
				
				<tr>
					<td align="left" style="height: 23px; width: 163px;">
                       <%-- Penyusutan KKK<br />
                        (Kadar Karet Kering)--%>
                    </td>
					<td align="left" style="height: 23px">
                        <asp:TextBox ID="txt_kkk_psn" runat="server" MaxLength="6" Visible=false onkeypress="javascript:return isNumberKey(event)"
                            Width="20%">0</asp:TextBox>
                        <%--%--%></td>
					<td style="width: 79px; height: 23px;">&nbsp;</td>					
					<td style="height: 23px; width: 191px;"></td>
					<td style="height: 23px"></td>
				</tr>
				
				<tr>
					<td align="left" style="height: 23px; width: 163px;">
                        <%--Penyusutan LM<br />
                        (Low Mangkok)--%>
                    </td>
					<td align="left" style="height: 23px">
                        <asp:Textbox id=txt_lm_psn maxlength=64 width="20%" Visible=false onkeypress="javascript:return isNumberKey(event)" runat=server>0</asp:Textbox>
                        <%--%--%></td>
					<td style="width: 79px; height: 23px;">&nbsp;</td>					
					<td style="height: 23px; width: 191px;"></td>
					<td style="height: 23px"></td>
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
				<Input Type=Hidden id=BlokCode runat=server />&nbsp;
				<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>								
			</table>
 	</div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</html>
