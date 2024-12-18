<%@ Page Language="vb" src="../../../include/PR_setup_AttdDet_Estate.aspx.vb" Inherits="PR_setup_AttdDet_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRSetup" src="../../menu/menu_prsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>ATTENDACE Details</title>
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
		<Form id="frmMain" class="main-modul-bg-app-list-pu"  runat="server">
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
                        <table cellpadding="0" cellspacing="0" class="style1">
                            <tr>
                                <td class="font9Tahoma">
                               <strong>    <asp:label id="lblTitle" runat="server" />
                        DETAIL SETTING KODE ABSENSI </strong>  </td>
                                <td class="font9Header" style="text-align: right">
                                    Status : <asp:Label id=lblStatus runat=server />&nbsp;| Tgl buat : <asp:Label id=lblDateCreated runat=server />&nbsp;| Tgl update : <asp:Label id=lblLastUpdate runat=server />&nbsp;| Diupdate : <asp:Label id=lblUpdatedBy runat=server />
                                </td>
                            </tr>
                        </table>
                    </td>
				</tr>
				<tr>
					<td colspan=6>
                    <hr style="width :100%" />
                    </td>
				</tr>
				<tr>
					<td style="width: 320px">
                        ID</td>
					<td style="width: 296px">                        
                        <asp:TextBox ID="txtAttID" runat="server" MaxLength="64" Width="152px"></asp:TextBox>
                        <asp:Label id=lblAttId forecolor=Red visible=False runat=server/>                        
					<td width=5%>&nbsp;</td>
					<td width=20%>&nbsp;</td>
					<td width=25%>&nbsp;</td>								
				</tr>		
				<tr>
					<td style="width: 320px">
                        Kode Absen</td>
					<td style="width: 296px">
                        <asp:DropDownList ID="ddlAttCd" runat="server" Width="152px">
                        </asp:DropDownList>
                        <asp:Label id=lblAttCd forecolor=Red visible=False runat=server/><td width=5%>&nbsp;</td>
					<td width=20%>&nbsp;</td>
					<td width=25%>&nbsp;</td>								
				</tr>				
				<tr>
					<td style="width: 320px;">Tipe Karyawan</td>
					<td align="left" style="width: 296px">
                        <asp:DropDownList ID="ddlEmpTyCd" runat="server" Width="152px">
                        </asp:DropDownList>
                        <asp:Label id=lblEmp forecolor=Red visible=False runat=server/></td>
					<td width=5% >&nbsp;</td>
					<td >&nbsp;</td>
					<td >&nbsp;</td>								
				</tr>
				<tr>
					<td align="left" style="width: 320px"><asp:Label id=Label1 Text="Potongan :" Font-Underline=true runat=server /></td>
					<td align="left" style="width: 296px">&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>				
				<tr>
				    <td align="left" style="width: 320px">
                        Potongan Upah :</td>
					<td align="left" style="width: 296px">
                       <asp:RadioButtonList ID="Option1" CssClass="font9Tahoma" runat="server" RepeatDirection="Horizontal" Width="66px" BackColor="Transparent" BorderColor="Transparent">
                            <asp:ListItem Selected="True" Value="Ya">Ya</asp:ListItem>
                            <asp:ListItem Value="Tidak">Tidak</asp:ListItem>
                        </asp:RadioButtonList>
                        &nbsp;
                    </td>
					<td >&nbsp;</td>					
					<td >&nbsp;</td>
					<td >&nbsp;</td>
				</tr>
				<tr>
				    <td align="left" style="width: 320px">
                        Potongan Beras :</td>
					<td align="left" style="width: 296px">
                        &nbsp; &nbsp;
                        <asp:RadioButtonList ID="Option2"  CssClass="font9Tahoma"  runat="server" RepeatDirection="Horizontal" Width="66px" BackColor="Transparent">
                            <asp:ListItem Selected="True" Value="Ya">Ya</asp:ListItem>
                            <asp:ListItem Value="Tidak">Tidak</asp:ListItem>
                        </asp:RadioButtonList></td>
					<td>&nbsp;</td>					
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>			
				<tr>
					<td align="left" style="width: 320px">
                        Potongan Cuti :</td>
					<td align="left" style="width: 296px">
                        &nbsp; &nbsp;
                        <asp:RadioButtonList ID="Option3"  CssClass="font9Tahoma"  runat="server" RepeatDirection="Horizontal" Width="66px" BackColor="Transparent">
                            <asp:ListItem Selected="True" Value="Ya">Ya</asp:ListItem>
                            <asp:ListItem Value="Tidak">Tidak</asp:ListItem>
                        </asp:RadioButtonList></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td align="left" style="width: 320px">
                        Potongan Hari Minggu</td>
					<td align="left" style="width: 296px">
                        &nbsp;<asp:RadioButtonList ID="Option4"  CssClass="font9Tahoma"  runat="server" RepeatDirection="Horizontal" Width="66px" BackColor="Transparent">
                            <asp:ListItem Selected="True" Value="Ya">Ya</asp:ListItem>
                            <asp:ListItem Value="Tidak">Tidak</asp:ListItem>
                        </asp:RadioButtonList></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td align="left" style="width: 320px">
                        </td>
					<td align="left" style="width: 296px">
                        &nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>				
				<tr>
					<td align="left" style="width: 320px"></td>
					<td align="left" style="width: 296px">
                        &nbsp;</td>
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
					&nbsp;</td>
				</tr>
				<Input Type=Hidden id=AttId runat=server />
				<asp:Label id=lblNoRecord visible=false text="Premi Beras details not found." runat=server/>
				<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>								
			</table>
        </div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</html>
