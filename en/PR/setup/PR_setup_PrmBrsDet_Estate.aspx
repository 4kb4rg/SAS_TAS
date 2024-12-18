<%@ Page Language="vb" src="../../../include/PR_setup_PrmBrsDet_Estate.aspx.vb" Inherits="PR_setup_PrmBrsDet_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRSetup" src="../../menu/menu_prsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Premi Beras Details</title>
           <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain class="main-modul-bg-app-list-pu" runat="server">
    
        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">

			<asp:Label id="lblErrMessage" visible="false" Text="Error while initiating component." runat="server" />
			<asp:Label id="lblCode" visible="false" text=" Code" runat="server" />
			<asp:Label id="lblSelect" visible="false" text="Please select " runat="server" />
			<asp:Label id="lblList" visible="false" text="Select " runat="server" />
			
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="5">
						<UserControl:MenuPRSetup id=MenuPRSetup runat="server" />
					</td>
				</tr>
				<tr>                    
					<td class="mt-h" colspan="5">DETAIL PREMI BERAS</td>
				</tr>
				<tr>
					<td colspan=6><hr style="width :100%" /></td>
				</tr>
				<tr>
					<td style="width: 208px">
                        <asp:label id="lblDeptHead" runat="server" Width="158px" >Tanggungan : </asp:label></td>
					<td>                        
						<asp:DropDownList id=ddlTunjangan width="75%" runat=server/> <asp:Label id=lblTunj forecolor=Red visible=False runat=server/>
					<td width=5%>&nbsp;</td>
					<td width=20%>Status : </td>
					<td width=25%><asp:Label id=lblStatus runat=server /></td>								
				</tr>				
				<tr>
					<td style="height: 45px; width: 208px;">Harga Beras : </td>
					<td width=30% style="height: 45px">
						<asp:DropDownList id=ddlBerasRt width="75%" runat=server/>
						<asp:Label id=lblNoBrsRt forecolor=Red visible=False runat=server Width="48px"/>						
					</td>
					<td width=5% style="height: 45px">&nbsp;</td>
					<td style="height: 45px">Date Created : </td>
					<td style="height: 45px"><asp:Label id=lblDateCreated runat=server /></td>								
				</tr>				
				<tr>
					<td align="left" >Jumlah Anak :</td>
					<td align="left" >
                        <asp:Textbox id=txtJmlAnk maxlength=64 width="75%" runat=server onkeypress="return isNumberKey(event)"></asp:Textbox></td>
					<td >&nbsp;</td>
					<td >Last Updated : </td>
					<td ><asp:Label id=lblLastUpdate runat=server /></td>
				</tr>
				<tr>
					<td align="left" style="width: 208px"><asp:Label id=Label2 Text="Jatah Premi Beras (KG) :" Font-Underline=true runat=server /></td>
					<td align="left">
                        &nbsp;</td>
					<td>&nbsp;</td>
					<td>Updated By : </td>
					<td><asp:Label id=lblUpdatedBy runat=server /></td>				
				</tr>
				<tr>
					<td align="left" style="width: 208px">Karyawan :</td>
					<td align="left">
                        <asp:Textbox id=txtKry maxlength=64 width="75%"  onkeypress="return isNumberKey(event)" runat=server ></asp:Textbox></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td align="left" style="width: 208px">Istri :</td>
					<td align="left">
                        <asp:Textbox id=txtIstri maxlength=64 width="75%" runat=server onkeypress="return isNumberKey(event)" ></asp:Textbox></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td align="left" style="width: 208px">
                        Anak :</td>
					<td align="left">
                        <asp:TextBox ID="txtAnk" runat="server" MaxLength="64" onkeypress="return isNumberKey(event)"
                            Width="75%"></asp:TextBox></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td align="left" style="width: 208px; height: 28px;">
                        Periode Start - End</td>
					<td align="left" style="height: 28px">
                        <asp:Textbox id="txtpstart" maxlength="6" width="35%" runat=server onkeypress="return isNumberKey(event)"></asp:Textbox>-
                        <asp:TextBox ID="txtpend"  MaxLength="6" Width="35%" runat="server" onkeypress="return isNumberKey(event)" ></asp:TextBox></td>
					<td style="height: 28px">&nbsp;</td>
					<td style="height: 28px">&nbsp;</td>
					<td style="height: 28px">&nbsp;</td>
				</tr>
				<tr>
					<td align="left" style="width: 208px; height: 28px;">
                        Catu Beras berupa</td>
					<td align="left" style="height: 28px">
                    <asp:DropDownList id=ddlfisik width="75%" runat=server>
						 <asp:ListItem Value="1">Fisik Beras</asp:ListItem>
						 <asp:ListItem Value="0">Uang</asp:ListItem>
					</asp:DropDownlist>
					</td>
					<td style="height: 28px">&nbsp;</td>
					<td style="height: 28px">&nbsp;</td>
					<td style="height: 28px">&nbsp;</td>
				</tr>
				<tr>
					<td align="left" style="width: 208px"><asp:Label id=Label1 Text="Potongan Tidak Kena Pajak :" Font-Underline=true runat=server /></td>
					<td align="left">&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
					<td align="left" style="width: 208px">PTKP Rate :</td>
					<td align="left">
                        <asp:Textbox id=txtPTKPRt maxlength=64 width="75%" runat=server onkeypress="return isNumberKey(event)" ></asp:Textbox>
					</td>
					<td>&nbsp;</td>
					<td></td>
					<td></td>
				</tr>
                 <tr>
					<td align="left" style="width: 208px">
                        Periode PTKP Start - End</td>
					<td align="left">
                        <asp:Textbox id="txtptkp_pstart" maxlength="6" width="35%" runat=server onkeypress="return isNumberKey(event)"></asp:Textbox>-
                        <asp:TextBox ID="txtptkp_pend"  MaxLength="6" Width="35%" runat="server" onkeypress="return isNumberKey(event)" ></asp:TextBox></td>
					<td>&nbsp;</td>	
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
					</td>
				</tr>
				<Input Type=Hidden id=PMBCd runat=server />
				<asp:Label id=lblNoRecord visible=false text="Premi Beras details not found." runat=server/>
				<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>								
			</table>
			<Input type=hidden id=hidBlockCharge value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>
        </div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</html>
