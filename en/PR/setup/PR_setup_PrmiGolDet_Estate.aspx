<%@ Page Language="vb" src="../../../include/PR_setup_PrmiGolDet_Estate.aspx.vb" Inherits="PR_setup_PrmiGolDet_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRSetup" src="../../menu/menu_prsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Premi Golongan Details</title>
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
					<td class="mt-h" colspan="5"><strong> <asp:label id="lblTitle" runat="server" />
                        DETAIL PREMI BASIS PANEN</strong> </td>
				</tr>
				<tr>
					<td colspan=5><hr style="width :100%" /></td>
				</tr>
				<tr>
					<td style="width: 320px">
                        <asp:label id="lblGolonganId" runat="server" Width="158px" >ID :</asp:label></td>
					<td style="width: 296px">                        
                        <asp:TextBox ID="txtGolID" ReadOnly=true runat="server" MaxLength="64" Width="78%"></asp:TextBox>
                        <asp:Label id=lblGolId forecolor=Red visible=False runat=server/>                        
					<td width=5%>&nbsp;</td>
					<td width=20%>&nbsp;</td>
					<td width=25%>&nbsp;</td>								
				</tr>		
				<tr>
					<td style="width: 320px; height: 12px;">
                        <asp:label id="lblDeptHead" runat="server" Width="158px" >Golongan :</asp:label></td>
					<td style="width: 296px; height: 5412pxpx;">                        
                        <asp:TextBox ID="txtGol" runat="server" MaxLength="2" Width="78%"></asp:TextBox>
                    <td width=5% style="height: 12px">&nbsp;</td>
					<td width=20% style="height: 12px"> Status :</td>
					<td width=25% style="height: 12px"><asp:Label id=lblStatus runat=server /></td>								
				</tr>
				<tr>
					<td align="left" style="width: 320px; height: 12px;">
                        Periode Start-End (mmyyyy)</td>
					<td align="left" style="width: 296px; height: 12px;">
                        <asp:TextBox ID="txtpstart" runat="server" MaxLength="6" Width="40%"></asp:TextBox>-
                        <asp:TextBox ID="txtpend" runat="server" MaxLength="6" Width="40%"></asp:TextBox></td>
					<td style="height: 12px">&nbsp;</td>
					<td style="height: 12px">Tgl buat : </td>
					<td style="height: 12px"><asp:Label id=lblDateCreated runat=server /></td>
				</tr>							
				<tr>
					<td style="height: 32px; width: 320px;">
                        <asp:Label id=Label1 Text="Umur Tanam :" Font-Underline=True runat=server /></td>
					<td align="left" style="width: 296px; height: 32px;">
					</td>
					<td width=5% style="height: 32px">&nbsp;</td>
					<td style="height: 32px">Tgl update: </td>
					<td style="height: 32px"><asp:Label id=lblLastUpdate runat=server /></td>								
				</tr>
					
				<tr>
				    <td align="left" style="width: 320px">
                        Min&nbsp; :</td>
					<td align="left" style="width: 296px">
					<asp:Textbox id=txtUTMin maxlength=64 width="80%" runat=server>0</asp:Textbox></td>
					<td>&nbsp;</td>					
					<td>Diupdate :</td>
					<td><asp:Label id=lblUpdatedBy runat=server /></td>
				</tr>
				<tr>
				    <td align="left" style="height: 28px; width: 320px;">
                        Max :</td>
					<td align="left" style="height: 28px; width: 296px;">
                        <asp:Textbox id=txtUTMax maxlength=64 width="80%" runat=server>0</asp:Textbox></td>
					<td>&nbsp;</td>					
					
				</tr>			
				<tr>
					<td align="left" style="width: 320px">
                        <asp:Label id=Label2 Text="BTGS :" Font-Underline=True runat=server /></td>
					<td align="left" style="width: 296px">
                        &nbsp;</td>
					<td>&nbsp;</td>
					
				</tr>
				<tr>
					<td align="left" style="width: 320px">
                        KG&nbsp; :</td>
					<td align="left" style="width: 296px">
                        <asp:Textbox id=txtBTGsKG maxlength=64 width="80%" runat=server>0</asp:Textbox></td>
					<td>&nbsp;</td>
					
				</tr>
				<tr>
					<td align="left" style="width: 320px">
                        BJR :</td>
					<td align="left" style="width: 296px">
                        <asp:TextBox ID="txtBTGsBJR" runat="server" MaxLength="64" Width="80%">0</asp:TextBox></td>
					<td>&nbsp;</td>
					
				</tr>				
				<tr>
					<td align="left" style="width: 320px">
                        JJG&nbsp; :</td>
					<td align="left" style="width: 296px">
                        <asp:TextBox ID="txtBTGsJJG" runat="server" MaxLength="64" Width="80%">0</asp:TextBox></td>
					<td>&nbsp;</td>					
				</tr>
				<tr>
					<td align="left" style="width: 320px">
                        <asp:Label id=Label3 Text="BPrm :" Font-Underline=True runat=server /></td>
					<td align="left" style="width: 296px">
                        &nbsp;</td>
					<td>&nbsp;</td>					
				</tr>
				<tr>
					<td align="left" style="width: 320px">
                        KG&nbsp; :</td>
					<td align="left" style="width: 296px">
                        <asp:TextBox ID="txtBprmKG" runat="server" MaxLength="64" Width="80%">0</asp:TextBox></td>
					<td>&nbsp;</td>					
				</tr>
				<tr>
					<td align="left" style="width: 320px">
                        JJG&nbsp; :</td>
					<td align="left" style="width: 296px">
                        <asp:TextBox ID="txtBPrmJJG" runat="server" MaxLength="64" Width="80%">0</asp:TextBox></td>
					<td>&nbsp;</td>					
				</tr>
				<tr>
					<td align="left" style="width: 320px">
                        <asp:Label id=Label4 Text="Premi Overtime :" Font-Underline=True runat=server /></td>
					<td align="left" style="width: 296px">
                        </td>
					<td>&nbsp;</td>					
				</tr>
				<tr>
					<td align="left" style="width: 320px">
                        1.</td>
					<td align="left" style="width: 296px">
                        <asp:TextBox ID="txtPrmOt1" runat="server" MaxLength="64" Width="80%">0</asp:TextBox></td>
					<td>&nbsp;</td>					
				</tr>
				<tr>
					<td align="left" style="width: 320px">
                        2.</td>
					<td align="left" style="width: 296px">
                        <asp:TextBox ID="txtPrmOt2" runat="server" MaxLength="64" Width="80%">0</asp:TextBox></td>
					<td>&nbsp;</td>					
				</tr>

				<tr>
					<td align="left" style="width: 320px">
                        3.</td>
					<td align="left" style="width: 296px">
                        <asp:TextBox ID="txtPrmOt3" runat="server" MaxLength="64" Width="80%">0</asp:TextBox></td>
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
				<Input Type=Hidden id=GolId runat=server />
				<asp:Label id=lblNoRecord visible=false text="Premi Beras details not found." runat=server/>
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
