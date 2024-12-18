<%@ Page Language="vb" src="../../../include/PR_setup_PrmiKrjnn_Estate.aspx.vb" Inherits="PR_setup_PrmiKrjnn_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRSetup" src="../../menu/menu_prsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>


<html>
	<head>
		<title>ASTEK Details</title>
         <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain runat="server" class="main-modul-bg-app-list-pu">

        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">


			<asp:Label id="lblErrMessage" ForeColor=red visible="false" Text="Error while initiating component." runat="server" />
            &nbsp;&nbsp;
			
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="5">
						<UserControl:MenuPRSetup id=MenuPRSetup runat="server" />
					</td>
				</tr>
				<tr>                    
					<td class="mt-h" colspan="5"><strong> DETAIL PREMI KERAJINAN</strong> </td>
				</tr>
				<tr>
					<td colspan=5><hr style="width :100%" /></td>
				</tr>
				<tr>
					<td style="width: 320px">Periode Start : </td>
					<td style="width: 296px">                        
                        <asp:TextBox ID=txtpstart runat="server" MaxLength="6"  Width="40%" onkeypress="javascript:return isNumberKey(event)"></asp:TextBox>&nbsp;
                        <td width=5%></td>
					<td width=20%>Status : </td>
					<td width=25%><asp:Label id=lblStatus runat=server /></td>								
				</tr>		
				<tr>
					<td style="width: 320px">Periode End : </td>
					<td style="width: 296px">                        
                        <asp:TextBox ID=txtpend runat="server" MaxLength="6" Width="40%" onkeypress="javascript:return isNumberKey(event)"></asp:TextBox>&nbsp;
                        <td width=5%>&nbsp;</td>
					<td width=20%>Tgl buat :</td>
					<td width=25%><asp:Label id=lblDateCreated runat=server /></td>								
				</tr>		
				<tr>
					<td style="width: 320px">Min HK : </td>
					<td style="width: 296px">                        
                        <asp:TextBox ID=txtMinHK runat="server" MaxLength="64" Width="40%" onkeypress="javascript:return isNumberKey(event)"></asp:TextBox>&nbsp;
                        <td width=5%>&nbsp;</td>
					<td width=20%>Tgl update :&nbsp;</td>
					<td width=25%><asp:Label id=lblLastUpdate runat=server /></td>								
				</tr>		
				<tr>
					<td style="width: 320px">
                        Min Kg :</td>
					<td style="width: 296px">                        
                        <asp:TextBox ID=txtMinKg runat="server" MaxLength="64" Width="40%" onkeypress="javascript:return isNumberKey(event)"></asp:TextBox><td width=5%>&nbsp;</td>
					<td width=20%>Diupdate :</td>
					<td width=25%><asp:Label id=lblUpdatedBy runat=server /></td>								
				</tr>				
				<tr>
					<td style="width: 320px;">
                        Premi Rate :
                    </td>
					<td style="width: 296px">
                        <asp:TextBox ID="txtPrmRt" runat="server" MaxLength="64" onkeypress="javascript:return isNumberKey(event)"
                            Width="40%"></asp:TextBox></td>
                    <td width=5%>&nbsp;</td>
					<td width=20%></td>
					<td width=25%></td>								
				</tr>
				<tr>
					<td align="left" style="width: 320px">Overtime Rate :</td>
					<td align="left" style="width: 296px">
                        <asp:TextBox ID="txtOvRt" runat="server" MaxLength="64" onkeypress="javascript:return isNumberKey(event)"
                            Width="40%"></asp:TextBox></td>
					<td width=5%>&nbsp;</td>
					<td></td>
					<td></td>
				</tr>				
				<tr>
					<td align="left" style="width: 320px">
                        Premi Kerajinan Deres :</td>
					<td align="left" style="width: 296px">
                        <asp:TextBox ID="txtprmdrs" runat="server" MaxLength="64" onkeypress="javascript:return isNumberKey(event)"
                            Width="40%"></asp:TextBox></td>
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
					</td>
				</tr>
                &nbsp;&nbsp;
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
