<%@ Page Language="vb" src="../../../include/PR_setup_Saldet_Estate.aspx.vb" Inherits="PR_setup_Saldet_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRSetup" src="../../menu/menu_prsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Setting Upah Details</title>
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
		<Form id=frmMain class="main-modul-bg-app-list-pu"  runat="server">
<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
<tr>
	<td style="width: 100%; height: 1500px" valign="top">
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
                        <br />
                    
                        <table cellpadding="0" cellspacing="0" class="style1">
                            <tr>
                                <td class="font9Tahoma">
                               <strong>   DETAIL SETTING UPAH KARYAWAN</strong>  </td>
                                <td class="font9Header" style="text-align: right">
                                    Status : <asp:Label id=lblStatus runat=server />&nbsp;| Tgl buat : <asp:Label id=lblDateCreated runat=server />&nbsp;| Tgl update : <asp:Label id=lblLastUpdate runat=server />&nbsp;| Diupdate : <asp:Label id=lblUpdatedBy runat=server />
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
					<td style="width: 224px">
                        <asp:label id="lblDeptHead" runat="server" Width="158px" >Tipe Karyawan : </asp:label></td>
					<td style="width: 346px">                        
						<asp:DropDownList id=ddlEmptyCode width="75%" CssClass="font9Tahoma"  runat=server/> <asp:Label id=lblEmptyCode forecolor=Red visible=False runat=server/>
					<td style="width: 16px">&nbsp;</td>
					<td style="width: 169px">&nbsp;</td>
					<td width=25%>&nbsp;</td>								
				</tr>				
				<tr>
					<td style="width: 224px;">
                        Periode Start - End (mmyyyy):</td>
					<td style="width: 346px;">
                        <asp:Textbox ID="txtpstart" maxlength=6 width="36%" CssClass="font9Tahoma" runat="server"></asp:Textbox>-
                        <asp:TextBox ID="txtpend"   maxlength=6 Width="36%" CssClass="font9Tahoma" runat="server"></asp:TextBox></td>
					<td style="width: 16px;">&nbsp;</td>
					<td style="width: 169px;">&nbsp;</td>
					<td >&nbsp;</td>								
				</tr>				
				<tr>
					<td align="left" style="width: 224px">UMP :</td>
					<td align="left" style="width: 346px">
                        <asp:Textbox id=txtUMP maxlength=64 width="75%" CssClass="font9Tahoma"  runat=server onkeypress="javascript:return isNumberKey(event)"></asp:Textbox></td>
					<td style="width: 16px">&nbsp;</td>
					<td style="width: 169px">&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td align="left" style="width: 224px">
                        Upah Harian :</td>
					<td align="left" style="width: 346px">
                        <asp:Textbox id=txtHK maxlength=64 width="75%" CssClass="font9Tahoma"  runat=server onkeypress="javascript:return isNumberKey(event)"></asp:Textbox></td>
					<td style="width: 16px">&nbsp;</td>
					<td style="width: 169px">&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td align="left" style="width: 224px">
                        Min HK :</td>
					<td align="left" style="width: 346px">
                        <asp:Textbox id=txtMinHK maxlength=64 width="75%" runat=server onkeypress="javascript:return isNumberKey(event)"></asp:Textbox></td>
					<td style="width: 16px">&nbsp;</td>
					<td style="width: 169px">&nbsp;</td>
				</tr>
				<tr>
					<td align="left" style="width: 224px; height: 22px">Option :</td>
					<td align="left" style="width: 346px; height: 22px">
                        <asp:CheckBox ID="ChkGol" Text=" Golongan" runat="server" />
                        <asp:CheckBox ID="ChkAstek" Text=" ASTEK" runat="server" />
                        <asp:CheckBox ID="ChkBeras" Text=" Beras" runat="server" />
						<asp:CheckBox ID="ChkMakan" Text=" Makan" runat="server" />
						<asp:CheckBox ID="ChkTrans" Text=" Transport" runat="server" /></td>
					<td style="width: 16px; height: 22px">&nbsp;</td>
					<td style="width: 169px; height: 22px">&nbsp;</td>
				</tr>
				<tr>
					<td align="left" style="width: 224px">
                        Pinjaman/Gajian Kecil :</td>
					<td align="left" style="width: 346px">
                        <asp:Textbox id=txtSmalPay maxlength=64 width="75%" runat=server onkeypress="javascript:return isNumberKey(event)"></asp:Textbox></td>
					<td style="width: 16px">&nbsp;</td>	
					<td style="width: 169px">&nbsp;</td>					
				</tr>
				<tr>
					<td align="left" style="width: 224px">
                        Makan - Transport / Hari :</td>
					<td align="left" style="width: 346px">
                        <asp:Textbox id=txtMakan maxlength=64 width="36%" runat=server onkeypress="javascript:return isNumberKey(event)"></asp:Textbox>-
						<asp:Textbox id=txtTrans maxlength=64 width="36%" runat=server onkeypress="javascript:return isNumberKey(event)"></asp:Textbox>
						</td>
					<td style="width: 16px">&nbsp;</td>	
					<td style="width: 169px">&nbsp;</td>					
				</tr>
				<tr>
					<td align="left" style="width: 224px">
                        1 HK  :</td>
					<td align="left" style="width: 346px">
                        <asp:Textbox id=txthkjam maxlength=5 width="75%" runat=server onkeypress="javascript:return isNumberKey(event)"></asp:Textbox></td>
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
					    <br />
					</td>
				</tr>
				<Input Type=Hidden id=SalCode runat=server />
				<asp:Label id=lblNoRecord visible=false text="Blok details not found." runat=server/>
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
