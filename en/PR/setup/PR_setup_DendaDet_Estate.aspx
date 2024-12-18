<%@ Page Language="vb" src="../../../include/PR_setup_DendaDet_Estate.aspx.vb" Inherits="PR_setup_DendaDet_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRSetup" src="../../menu/menu_prsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Denda Details</title>
                <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain runat="server">
                   <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">
			<asp:Label id="lblErrMessage" visible="false" ForeColor=red Text="Error while initiating component." runat="server" />
            &nbsp;&nbsp;
			
			<table border=0 cellspacing=0 cellpadding=2 width=100%>
				<tr>
					<td colspan="5">
						<UserControl:MenuPRSetup id=MenuPRSetup runat="server" />
					</td>
				</tr>
				<tr>                    
					<td class="mt-h" colspan="5">DETAIL DENDA PANEN</td>
				</tr>
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>
				<tr>
					<td style="width: 177px">
                        Kode Denda : </td>
					<td style="width: 220px">                        
                        <asp:TextBox CssClass="mr-h" ReadOnly=true ID="txtDendaCd"  runat="server" MaxLength="8" Width="78%"></asp:TextBox>
                        <asp:Label id=lblDndCode forecolor=Red visible=False runat=server/>                        
					<td width=5%>&nbsp;</td>
					<td width=20%>Status :</td>
					<td width=25%>&nbsp;<asp:Label id=lblStatus runat=server /></td>								
				</tr>
				
				<tr>
					<td style="width: 177px">
                        Sub Kategori : </td>
					<td style="width: 220px">                        
                        <asp:DropDownList ID="ddlsubsubcat" runat="server" Width="78%"></asp:DropDownList>              
					<td width=5%>&nbsp;</td>
					<td width=20%>Tgl buat : </td>
					<td width=25%>&nbsp;<asp:Label id=lblDateCreated runat=server /></td>								
				</tr>
					
				<tr>
					<td style="width: 177px">
                        Periode Start-End (mmyyyy)</td>
					<td style="width: 220px">
                        <asp:TextBox ID="txtpstart" runat="server" MaxLength="6" Width="45%" onkeypress="javascript:return isNumberKey(event)"></asp:TextBox>-<asp:TextBox
                            ID="txtpend" runat="server" MaxLength="6" Width="45%" onkeypress="javascript:return isNumberKey(event)"></asp:TextBox><td width=5%>&nbsp;</td>
					<td width=20%>Tgl Update : </td>
					<td width=25%>&nbsp;<asp:Label id=lblLastUpdate runat=server /></td>								
				</tr>	
				<tr>
					<td style="width: 177px; height: 54px;">
                        Deskripsi :</td>
					<td style="width: 220px; height: 54px;">                        
					<asp:DropDownList ID="txtDesc" runat="server" Width="100%">
                        <asp:ListItem Value="0">Buah Mentah Gol 0</asp:ListItem>
                        <asp:ListItem Value="1">Buah Mentah Gol 1</asp:ListItem>
                        <asp:ListItem Value="2">Buah Mentah Gol 2</asp:ListItem>
                        <asp:ListItem Value="3">Buah Mentah Gol 3</asp:ListItem>
                        <asp:ListItem Value="4">Buah Tinggal Gol 0</asp:ListItem>
                        <asp:ListItem Value="5">Buah Tinggal Gol 1</asp:ListItem>
                        <asp:ListItem Value="6">Buah Tinggal Gol 2</asp:ListItem>
                        <asp:ListItem Value="7">Buah Tinggal Gol 3</asp:ListItem>
                        <asp:ListItem Value="8">Buah Tdk TPH Gol 0</asp:ListItem>
                        <asp:ListItem Value="9">Buah Tdk TPH Gol 1</asp:ListItem>
                        <asp:ListItem Value="10">Buah Tdk TPH Gol 2</asp:ListItem>
                        <asp:ListItem Value="11">Buah Tdk TPH Gol 3</asp:ListItem>
                        <asp:ListItem Value="12">Tangkai Panjang Gol 0</asp:ListItem>
                        <asp:ListItem Value="13">Tangkai Panjang Gol 1</asp:ListItem>
                        <asp:ListItem Value="14">Tangkai Panjang Gol 2</asp:ListItem>
                        <asp:ListItem Value="15">Tangkai Panjang Gol 3</asp:ListItem>
                        <asp:ListItem Value="16">Pelepah Sengkleh Gol 0</asp:ListItem>
                        <asp:ListItem Value="17">Pelepah Sengkleh Gol 1</asp:ListItem>
                        <asp:ListItem Value="18">Pelepah Sengkleh Gol 2</asp:ListItem>
                        <asp:ListItem Value="19">Pelepah Sengkleh Gol 3</asp:ListItem>
                        <asp:ListItem Value="20">Tidak Dapat Basis</asp:ListItem>
                        <asp:ListItem Value="21">Brondolan Kotor</asp:ListItem>
                        <asp:ListItem Value="22">Brondolan Tdk TPH</asp:ListItem>
                        <asp:ListItem Value="23">Brondolan Kutip Kotor</asp:ListItem>          
                    </asp:DropDownList>
					<asp:Label id=lblDesc forecolor=Red visible=False runat=server/><td width=5% style="height: 54px">&nbsp;</td>
					<td width=20% style="height: 54px">Diupdate :</td>
					<td width=25% style="height: 54px"><asp:Label id=lblUpdatedBy runat=server /></td>								
				</tr>
				<tr>
					<td align="left" style="width: 177px; height: 12px;">
                        Golongan : </td>
					<td align="left" style="width: 220px; height: 12px;">
                        <asp:TextBox ID="txtgol" runat="server" MaxLength="64" Width="20%"></asp:TextBox></td>
					<td style="height: 12px">&nbsp;</td>
					<td style="height: 12px"></td>
					<td style="height: 12px"></td>
				</tr>							
				<tr>
					<td style="height: 12px; width: 177px;">
                        Satuan :</td>
					<td align="left" style="width: 220px; height: 12px;">
                        <asp:Textbox id=txtsatuan maxlength=64 width="20%" runat=server></asp:Textbox></td>
					<td width=5% style="height: 12px">&nbsp;</td>
					<td style="height: 12px"> </td>
					<td style="height: 12px"></td>								
				</tr>
					
				<tr>
				    <td align="left" style="width: 177px; height: 12px;">
                        Denda Rate :</td>
					<td align="left" style="width: 220px; height: 12px;">
                        <asp:Textbox id=txtDRate maxlength=64 width="50%" onkeypress="javascript:return isNumberKey(event)" runat=server></asp:Textbox></td>
					<td style="height: 12px">&nbsp;</td>					
					<td style="height: 12px"></td>
					<td style="height: 12px"></td>
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
				<Input Type=Hidden id=DendaCode runat=server />&nbsp;
				<Input Type=Hidden id=isNew runat=server />&nbsp;
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
