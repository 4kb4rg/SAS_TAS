<%@ Page Language="vb" src="../../../include/PR_setup_OTDet_Estate.aspx.vb" Inherits="PR_setup_OTDet_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRSetup" src="../../menu/menu_prsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>OVERTIME Details</title>
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
					<td class="mt-h" colspan="5">DETAIL SETTING TARIF LEMBUR</td>
				</tr>
				<tr>
					<td colspan=5><hr style="width :100%" /> </td>
				</tr>
				<tr>
					<td style="width: 280px">
                        Kode Lembur</td>
					<td style="width: 320px">                        
                        <asp:TextBox ID="txtOtCode" runat="server" MaxLength="64" Width="181px" ReadOnly="True"></asp:TextBox>&nbsp;
					<td width=5%>&nbsp;</td>
					<td width=20%>Status :</td>
					<td width=25%><asp:Label id=lblStatus runat=server /></td>								
				</tr>		
				<tr>
					<td style="width: 280px">
                        Harga Beras</td>
					<td style="width: 320px">                        
                        <asp:DropDownList ID="ddlBrate" runat="server" Width="181px">
                        </asp:DropDownList>&nbsp;
                    <td width=5%>&nbsp;</td>
					<td width=20%>Tgl buat : </td>
					<td width=25%><asp:Label id=lblDateCreated runat=server /></td>								
				</tr>
				<tr>
					<td align="left" style="width: 280px">
                        Tipe Hari</td>
					<td align="left" style="width: 320px">
					    <asp:DropDownList ID="ddliswkd" runat="server" Width="181px">
                        <asp:ListItem Selected="True" Value="R">Hari Biasa</asp:ListItem>
                        <asp:ListItem Value="M">Hari Minggu</asp:ListItem>
                        <asp:ListItem Value="B">Hari Besar</asp:ListItem>
                    </asp:DropDownList></td>
					<td width=5%>&nbsp;</td>
					<td width=20%>Tgl Update : </td>
					<td width=25%><asp:Label id=lblLastUpdate runat=server /></td>
				</tr>							
					
				<tr>
				    <td align="left" style="width: 280px"></td>
				    <td align="left" style="width: 320px">
                        1.
                        <asp:TextBox ID="txtmin1" runat="server" MaxLength="64" Width="20px">0</asp:TextBox>
                        s/d
                        <asp:TextBox ID="txtmax1" runat="server" MaxLength="64" Width="20px">0</asp:TextBox>
                        jam = &nbsp;<asp:TextBox ID="txtPsn1" runat="server" MaxLength="64" Width="40px">0</asp:TextBox>
                        %</td>
					<td>&nbsp;</td>					
					<td>Diupdate :</td>
					<td><asp:Label id=lblUpdatedBy runat=server /></td>
				</tr>
				<tr>
				    <td align="left" style="width: 280px"></td>
				    <td align="left" style="width: 320px">
                        2.
                        <asp:TextBox ID="txtmin2" runat="server" MaxLength="64" Width="20px">0</asp:TextBox>
                        s/d
                        <asp:TextBox ID="txtmax2" runat="server" MaxLength="64" Width="20px">0</asp:TextBox>
                        jam = &nbsp;<asp:TextBox ID="txtpsn2" runat="server" MaxLength="64" Width="40px">0</asp:TextBox>
                        %</td>
					<td>&nbsp;</td>					
					<td> </td>
					<td></td>
				</tr>			
				<tr>
					<td align="left" style="width: 280px"></td>
				    <td align="left" style="width: 320px">
                        3.
                        <asp:TextBox ID="txtmin3" runat="server" MaxLength="64" Width="20px">0</asp:TextBox>
                        s/d
                        <asp:TextBox ID="txtmax3" runat="server" MaxLength="64" Width="20px">0</asp:TextBox>
                        jam = &nbsp;<asp:TextBox ID="txtpsn3" runat="server" MaxLength="64" Width="40px">0</asp:TextBox>
                        %</td>
					<td style="height: 23px">&nbsp;</td>
					<td style="height: 23px"></td>
					<td style="height: 23px"></td>
				</tr>
				<tr>
					<td align="left" style="width: 280px"></td>
				    <td align="left" style="width: 320px">
                        4.
                        <asp:TextBox ID="txtmin4" runat="server" MaxLength="64" Width="20px">0</asp:TextBox>
                        s/d
                        <asp:TextBox ID="txtmax4" runat="server" MaxLength="64" Width="20px">0</asp:TextBox>
                        jam = &nbsp;<asp:TextBox ID="txtpsn4" runat="server" MaxLength="64" Width="40px">0</asp:TextBox>
                        %</td>
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
				<asp:Label id=lblNoRecord visible=false text="Premi Beras details not found." runat=server/><asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
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
