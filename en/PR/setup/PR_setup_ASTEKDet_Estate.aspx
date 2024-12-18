<%@ Page Language="vb" src="../../../include/PR_setup_ASTEKDet_Estate.aspx.vb" Inherits="PR_setup_ASTEKDet_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRSetup" src="../../menu/menu_prsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head> 
		<title>ASTEK Details</title>
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
					<td  colspan="5">
                        &nbsp;<table cellpadding="0" cellspacing="0" class="style1">
                            <tr>
                                <td class="font9Tahoma">
                                  <strong> ASTEK DETAILS</strong> </td>
                                <td class="font9Header" style="text-align: right">
                                    Status : <asp:Label id=lblStatus runat=server />&nbsp;| Date Created : <asp:Label id=lblDateCreated runat=server />&nbsp;| Last Updated : <asp:Label id=lblLastUpdate runat=server />&nbsp;| 
                                    Update By : <asp:Label id=lblUpdatedBy runat=server />
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
					<td style="width: 320px">
                        Astek Code</td>
					<td style="width: 296px">                        
                        <asp:TextBox ID="txtAstekCd" CssClass="font9Tahoma" runat="server" MaxLength="64" Width="80%"></asp:TextBox>
                        <asp:Label id=lblAstekCd forecolor=Red visible=False runat=server/>                        
					<td width=5%>&nbsp;</td>
					<td width=20%>&nbsp;</td>
					<td width=25%>&nbsp;</td>								
				</tr>		
				<tr>
					<td style="width: 320px">
                        Deskripsi</td>
					<td style="width: 296px">                        
                        <asp:TextBox ID="txtDesc" CssClass="font9Tahoma"  runat="server" MaxLength="64" Width="100%"></asp:TextBox>
                        <asp:Label id=lblDesc forecolor=Red visible=False runat=server/>
					<td width=5%>&nbsp;</td>
					<td width=20%>&nbsp;</td>
					<td width=25%>&nbsp;</td>								
				</tr>				
				<tr>
					<td style="height: 45px; width: 320px;">
                        Periode Start-End : (mmyyyy )</td>
					<td align="left" style="width: 296px">
                        <asp:Textbox id=txtPStart maxlength=64 width="40%" CssClass="font9Tahoma"  runat=server></asp:Textbox>-<asp:TextBox
                            ID="txtPEnd" CssClass="font9Tahoma"  runat="server" MaxLength="64" Width="40%"></asp:TextBox></td>
					<td width=5% style="height: 45px">&nbsp;</td>
					<td style="height: 45px">&nbsp;</td>
					<td style="height: 45px">&nbsp;</td>								
				</tr>
				<tr>
					<td align="left" style="width: 320px"><asp:Label id=Label1 Text="Presentase Potongan :" Font-Underline=true runat=server /></td>
					<td align="left" style="width: 296px">&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>				
				<tr>
				    <td align="left" style="width: 320px">Jaminan Kematian (JKM) :</td>
					<td align="left" style="width: 296px"><asp:Textbox id=txtJKM maxlength=64 width="80%" CssClass="font9Tahoma"  runat=server>0</asp:Textbox></td>
					<td>&nbsp;</td>					
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td align="left" style="height: 28px; width: 320px;">
                        Jaminan Kecelakaan Kerja (JKK) :</td>
					<td align="left" style="height: 28px; width: 296px;">
                        <asp:Textbox id=txtJKK maxlength=64 width="80%" CssClass="font9Tahoma"  runat=server>0</asp:Textbox>
					</td>
					<td>&nbsp;</td>					
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>			
				<tr>
					<td align="left" style="width: 320px">
                        Jaminan Hari Tua (JHT):</td>
					<td align="left" style="width: 296px">
                        <asp:Textbox id=txtJHT maxlength=64 width="80%" CssClass="font9Tahoma"  runat=server>0</asp:Textbox>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td align="left" style="width: 320px"> </td>
					<td align="left" style="width: 296px">
                        <asp:CheckBox ID="chkjpk" runat="server" Text=" JPK dalam persen (%)" Enabled="True" />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td align="left" style="width: 320px">
                        Jaminan Pemeliharaan Kesehatan (JPK):</td>
					<td align="left" style="width: 296px">
                        <asp:Textbox id=txtJPK maxlength=64 width="80%" CssClass="font9Tahoma"  runat=server>0</asp:Textbox>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td align="left" style="width: 320px">
                        Jaminan Pemeliharaan Kesehatan Keluarga <br />(JPK Keluarga):</td>
					<td align="left" style="width: 296px">
                        <asp:Textbox id=txtJPKKwn maxlength=64 width="80%" CssClass="font9Tahoma"  runat=server>0</asp:Textbox>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>		
				<tr>
					<td align="left" style="width: 320px">
                        Jaminan Pensiun :</td>
					<td align="left" style="width: 296px">
                        <asp:Textbox id=txtJP maxlength=64 width="80%" CssClass="font9Tahoma"  runat=server>0</asp:Textbox>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>						
				<tr>
					<td align="left" style="width: 320px">Potongan Kary JHT :</td>
					<td align="left" style="width: 296px">
                        <asp:Textbox id=txtPotHJT maxlength=64 width="80%" CssClass="font9Tahoma"  runat=server>0</asp:Textbox>
					</td>
					<td>&nbsp;</td>
					<td></td>
					<td></td>
				</tr>
				<tr>
					<td align="left" style="width: 320px">Potongan Kary JPK/BPJS :</td>
					<td align="left" style="width: 296px">
                        <asp:Textbox id=txtPotJPK maxlength=64 width="80%" CssClass="font9Tahoma"  runat=server>0</asp:Textbox>
					</td>
					<td>&nbsp;</td>
					<td></td>
					<td></td>
				</tr>
				<tr>
					<td align="left" style="width: 320px">Potongan Kary Pensiun :</td>
					<td align="left" style="width: 296px">
                        <asp:Textbox id=txtPotJP maxlength=64 width="80%" CssClass="font9Tahoma"  runat=server>0</asp:Textbox>
					</td>
					<td>&nbsp;</td>
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
				<Input Type=Hidden id=AstekCd runat=server />
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
