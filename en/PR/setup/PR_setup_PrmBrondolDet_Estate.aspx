<%@ Page Language="vb" src="../../../include/PR_setup_PrmBrondolDet_Estate.aspx.vb" Inherits="PR_setup_PrmBrondolDet_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRSetup" src="../../menu/menu_prsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Premi Brondol Details</title>
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
            &nbsp;&nbsp;
		
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="5">
						<UserControl:MenuPRSetup id=MenuPRSetup runat="server" />
					</td>
				</tr>
				<tr>                    
					<td class="mt-h" colspan="5">
                        PREMI BRONDOLAN DETAILS</td>
				</tr>
				<tr>
					<td colspan=5><hr style="width :100%" /> </td>
				</tr>
				<tr>
					<td style="width: 320px; height: 25px;">
                        ID :</td>
					<td style="width: 296px; height: 25px;">                        
                        <asp:TextBox ID="txtgol" CssClass="mr-h" ReadOnly=true runat="server" MaxLength="64" Width="78%"></asp:TextBox>
                    &nbsp;<td width=5% style="height: 25px">&nbsp;</td>
					<td width=20% style="height: 25px"> Status :</td>
					<td width=25% style="height: 25px"><asp:Label id=lblStatus runat=server /></td>								
				</tr>		
				<tr>
					<td style="width: 320px; height: 25px;">
                        Periode Start-End (mmyyyy) :</td>
					<td style="width: 296px; height: 25px;">                        
                        <asp:TextBox ID="txtpstart" runat="server" MaxLength="6" Width="45%" onkeypress="javascript:return isNumberKey(event)"></asp:TextBox>-<asp:TextBox
                            ID="txtpend" runat="server" MaxLength="6" Width="50%" onkeypress="javascript:return isNumberKey(event)"></asp:TextBox><td width=5% style="height: 25px">&nbsp;</td>
					<td width=20% style="height: 25px"> Date Created : </td>
					<td width=25% style="height: 25px"><asp:Label id=lblDateCreated runat=server /></td>								
				</tr>		
				<tr>
					<td style="width: 320px; height: 42px;">
                        Gol :></td>
					<td style="width: 296px; height: 42px;">                        
                        <asp:DropDownList ID="ddlGolId" runat="server" Width="154px">
                        </asp:DropDownList>
                        <asp:Label id=lblGolId forecolor=Red visible=False runat=server/><td width=5% style="height: 42px">&nbsp;</td>
					<td width=20% style="height: 42px"> Last Updated :</td>
					<td width=25% style="height: 42px"><asp:Label id=lblLastUpdate runat=server /></td>								
				</tr>
				<tr>
					<td align="left" style="width: 320px; height: 12px;">
                        Premi Brondolan</td>
					<td align="left" style="width: 296px; height: 12px;">
                        <asp:TextBox ID="txtPrmiBrondol" runat="server" MaxLength="64" Width="78%" onkeypress="javascript:return isNumberKey(event)"></asp:TextBox>
                        <asp:Label id=lblPrmi forecolor=Red visible=False runat=server/></td>
					<td style="height: 12px">&nbsp;</td>
					<td style="height: 12px">Update By :</td>
					<td style="height: 12px"><asp:Label id=lblUpdatedBy runat=server /></td>
				</tr>							
				<tr>
					<td style="height: 32px; width: 320px;"></td>
					<td align="left" style="width: 296px; height: 32px;">
					</td>
					<td width=5% style="height: 32px">&nbsp;</td>
					<td style="height: 32px"></td>
					<td style="height: 32px"></td>								
				</tr>
				<tr>
					<td colspan="5" style="height: 28px">
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn AlternateText=" Delete " imageurl="../../images/butt_delete.gif" CausesValidation=False onclick=Button_Click CommandArgument=Del runat=server />
						<asp:ImageButton id=UnDelBtn AlternateText="Undelete" imageurl="../../images/butt_undelete.gif" onclick=Button_Click CommandArgument=UnDel runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					</td>
				</tr>
				<Input Type=Hidden id=GolId runat=server />
				<Input Type=Hidden id=isNew runat=server />
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
