<%@ Page Language="vb" src="../../../include/PR_setup_NormaDet_Estate.aspx.vb" Inherits="PR_setup_NormaDet_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRSetup" src="../../menu/menu_prsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Norma Details</title>
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
					<td class="mt-h" colspan="5">
                        &nbsp;NORMA DETAILS</td>
				</tr>
				<tr>
					<td colspan=5><hr style="width :100%" /> </td>
				</tr>
				<tr>
					<td style="width: 224px; height: 23px;">
                        Norma Code:</td>
					<td style="width: 346px; height: 23px;">                        <asp:Label id=lblnormacode runat=server /><td style="width: 16px; height: 23px;">&nbsp;</td>
					<td style="width: 169px; height: 23px;">Status : </td>
					<td width=25% style="height: 23px"><asp:Label id=lblStatus runat=server /></td>								
				</tr>				
				<tr>
					<td style="width: 224px;">
                        Sub Kategori:</td>
					<td style="width: 346px;">
                        <asp:DropDownList ID="ddlsubcat" runat="server" Width="75%" OnSelectedIndexChanged="ddlsubcat_OnSelectedIndexChanged" AutoPostBack=true>
                        </asp:DropDownList></td>
					<td style="width: 16px;">&nbsp;</td>
					<td style="width: 169px;">Date Created : </td>
					<td ><asp:Label id=lblDateCreated runat=server /></td>								
				</tr>				
				<tr>
					<td align="left" style="width: 224px; height: 28px;">
                        Aktiviti :</td>
					<td align="left" style="width: 346px; height: 28px;">
                        <asp:DropDownList ID="ddljob" runat="server" Width="75%">
                        </asp:DropDownList></td>
					<td style="width: 16px; height: 28px;">&nbsp;</td>
					<td style="width: 169px; height: 28px;">Last Updated : </td>
					<td style="height: 28px"><asp:Label id=lblLastUpdate runat=server /></td>
				</tr>
				<tr>
					<td align="left" style="width: 224px">
                        Aktiviti Note :</td>
					<td align="left" style="width: 346px">
                        <asp:Textbox id=txtJobNote maxlength=64 width="75%" runat=server onkeypress="javascript:return isNumberKey(event)"></asp:Textbox></td>
					<td style="width: 16px">&nbsp;</td>
					<td style="width: 169px">Updated By : </td>
					<td><asp:Label id=lblUpdatedBy runat=server /></td>
				</tr>
				<tr>
					<td align="left" style="width: 224px">
                        Umur Min-Max</td>
					<td align="left" style="width: 346px">
                        <asp:Textbox id=txtMinUmr maxlength=64 width="35%" runat=server onkeypress="javascript:return isNumberKey(event)"></asp:Textbox>-<asp:TextBox
                            ID="txtMaxUmr" runat="server" MaxLength="64" onkeypress="javascript:return isNumberKey(event)"
                            Width="35%"></asp:TextBox></td>
					<td style="width: 16px">&nbsp;</td>
					<td style="width: 169px">&nbsp;</td>
				</tr>
				<tr>
					<td align="left" style="width: 224px; height: 22px">
                        Rot/Thn :</td>
					<td align="left" style="width: 346px; height: 22px">
                        <asp:TextBox ID="txtrotthn" runat="server" MaxLength="64" onkeypress="javascript:return isNumberKey(event)"
                            Width="35%"></asp:TextBox></td>
					<td style="width: 16px; height: 22px">&nbsp;</td>
					<td style="width: 169px; height: 22px">&nbsp;</td>
				</tr>
				<tr>
					<td align="left" style="width: 224px">
                        Hk/Ha/Rot :</td>
					<td align="left" style="width: 346px">
                        <asp:Textbox id=txthkharot maxlength=64 width="35%" runat=server onkeypress="javascript:return isNumberKey(event)"></asp:Textbox></td>
					<td style="width: 16px">&nbsp;</td>	
					<td style="width: 169px">&nbsp;</td>					
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
				<Input Type=Hidden id=NorCode runat=server />
				<asp:Label id=lblNoRecord visible=false text="Blok details not found." runat=server/>
				<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>								
				<tr>
					<td colspan="5" style="height: 28px">
                                            &nbsp;</td>
				</tr>
				</table>
			<Input type=hidden id=hidBlockCharge value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>

        <br />
        </div>
        </td>
        </tr>
        </table>


		</form>
	</body>
</html>
