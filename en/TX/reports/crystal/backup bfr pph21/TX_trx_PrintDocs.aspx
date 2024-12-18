<%@ Page Language="vb" src="../../../include/TX_trx_PrintDocs.aspx.vb" Inherits="TX_trx_PrintDocs" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<head>
    <title>Print Documents</title> 
    <Script Language="Javascript">
		function keypress() {
			if (event.keyCode == 27)	//escape key press
				window.close();			//close window
		}
    </Script>
</head>
<Preference:PrefHdl id=PrefHdl runat="server" />

<body leftmargin="2" topmargin="2">
    <form id=frmMain runat="server">
		<table id=tblMain width=100% border=0 cellspacing="1" cellpadding="1"  runat=server>
			<tr>
				<td colspan=2 class="mt-h">Tax Printed</td>
			</tr>
			<tr>
				<td colspan=6><hr size="1" noshade></td>
			</tr>
			<tr>
			    <td width=20% height=25>Print Options :</td>
			    <td width=50% >
                    <asp:DropDownList id="ddlPrintOpt" width=90% AutoPostBack=true OnSelectedIndexChanged="PrintOpt_Changed" runat=server>
                        <asp:ListItem value="0" Selected>Please select print options</asp:ListItem>
                        <asp:ListItem value="1">Surat Pemberitahuan (SPT) Masa</asp:ListItem>
                        <asp:ListItem value="2">Daftar Bukti Pemotongan</asp:ListItem>
                        <asp:ListItem value="3">Bukti Pemotongan/Pemungutan</asp:ListItem>
                        <asp:ListItem value="5">Surat Setoran Pajak (SSP)</asp:ListItem>										
                        <asp:ListItem value="4">Tax Verified Listing</asp:ListItem>		
                    </asp:DropDownList>
                </td>
			</tr>
			<tr>
			    <td width=20%>Tax Object Group :</td>
			    <td width=50% valign=top>
                    <asp:DropDownList id="ddlTaxObjectGrp"  width=90% runat=server>
                    </asp:DropDownList>
                </td>
			</tr>
			<tr>
			    <td width=20%>KPP Location :</td>
			    <td width=50% valign=top>
                    <asp:DropDownList id="ddlKPP" width=90%  runat=server>
                    </asp:DropDownList>
                </td>
			</tr>
			<tr id="RowSSP" visible=false>
			    <td width=20%>Kode Akun Pajak - Jenis Setoran:</td>
			    <td width=50% valign=top>
                    <asp:DropDownList id="ddlSSPAkunPjk" width=45% AutoPostBack=true OnSelectedIndexChanged="SSPAkunPjk_Changed" runat=server></asp:DropDownList>
                    -
                    <asp:DropDownList id="ddlSSPJnsStr" width=43% runat=server></asp:DropDownList>
                </td>
			</tr>
			<tr id="RowSSPLembar" visible=false>
			    <td width=20%>Lembar ke- :</td>
			    <td width=50% valign=top>
                    <asp:DropDownList id="ddlSSPLembar" width=90% runat=server>
                        <asp:ListItem value="0" Selected>Please select printed no.</asp:ListItem>
                        <asp:ListItem value="1">1. Untuk Arsip Wajib Pajak</asp:ListItem>
                        <asp:ListItem value="2">2. Untuk KPPN</asp:ListItem>
                        <asp:ListItem value="3">3. Untuk Dilaporkan oleh Wajib Pajak ke KPP</asp:ListItem>
                        <asp:ListItem value="4">4. Untuk Bank Persepsi/Kantor Pos dan Giro</asp:ListItem>										
                        <asp:ListItem value="5">5. Untuk Arsip</asp:ListItem>		
                    </asp:DropDownList>
                </td>
			</tr>
			<tr>
				<td width=20%>Doc ID : </td>
				<td width=50%><asp:TextBox id=txtTrxId width=90% ReadOnly=true maxlength=25 runat=server />
					<asp:Label id=lblErrCodeFrom visible=false forecolor=red text="Invalid code given.<br>" runat=server/></td>		
			</tr>	
			<tr>
				<td width=20%>Supplier : </td>
				<td width=50%><asp:TextBox id=txtSupplier width=90% ReadOnly=true maxlength=25 runat=server /></td>		
			</tr>	
			
			
			<tr>
			    <td height=25 align=left>Create Date</td>
				<td><asp:TextBox ID=txtTransDate maxlength=10 width=25% Runat=server />
				    <a href="javascript:PopCal('txtTransDate');"><asp:Image id="btnSelTransDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
				    <asp:Label id=lblErrTransDate forecolor=red Visible = false text="Date format " runat=server />
				    <asp:label id=lblFmtTransDate  forecolor=red Visible = false Runat="server"/> 
				</td>
			</tr>
			<tr>
			    <td height=25>SPT Pembetulan Ke-</td>
			    <td><asp:TextBox id=txtSPTRev width=25% maxlength="3" runat="server" />
			    &nbsp;&nbsp;<i>*If empty then SPT Normal</i>
			    </td>
			</tr>
			<tr>
			    <td height=25>Surat Setoran Pajak (SSP)</td>
			    <td><asp:TextBox id=txtQtySSP1 width=25% maxlength="3" runat="server" />&nbsp;&nbsp;&nbsp;lembar
			    &nbsp;&nbsp;<i>*PPh23/PPh26, PPh21/PPh26, PPh4 Ayat(2)</i>
			    </td>
			</tr>
			<tr>
			    <td height=25>Bukti Pemotongan</td>
			    <td><asp:TextBox id=txtBuktiPtg width=25% maxlength="3" runat="server" />&nbsp;&nbsp;&nbsp;lembar
			    &nbsp;&nbsp;<i>*PPh23/PPh26, PPh21/PPh26, PPh4 Ayat(2)</i>
			    </td>
			</tr>
			<tr>
			    <td height=25>Surat Setoran Pajak (SSP) yang disetor oleh importir atau Pembeli Barang</td>
			    <td><asp:TextBox id=txtQtySSP2 width=25% maxlength="3" runat="server" />&nbsp;&nbsp;&nbsp;lembar
			    &nbsp;&nbsp;<i>*PPh22</i>
			    </td>
			</tr>
			<tr>
			    <td height=25>Surat Setoran Pajak (SSP) yang disetor oleh Pemungut Pajak</td>
			    <td><asp:TextBox id=txtQtySSP3 width=25% maxlength="3" runat="server" />&nbsp;&nbsp;&nbsp;lembar
			    &nbsp;&nbsp;<i>*PPh22</i>
			    </td>
			</tr>
			<tr>
			    <td width=20% height=25>Pemotong/Kuasa Wajib Pajak :</td>
			    <td width=50% >
                    <asp:DropDownList id="ddlPelapor" width=90% runat=server>
                        <asp:ListItem value="0">Please select Pemotong/Kuasa Wajib Pajak</asp:ListItem>
                        <asp:ListItem value="246762793113000" Selected>BUTIANO</asp:ListItem>
                        <asp:ListItem value="080569890116000">MOSFLY ANG</asp:ListItem>
                    </asp:DropDownList>
                </td>
			</tr>
			<tr>
			    <td height=25>Pelapor</td>
			    <td><asp:TextBox id=txtPelapor width=90% Text="RUDY YOHNWEIN" maxlength="3" runat="server" />
			    </td>
			</tr>
			<!-- End -->
			<tr>
				<td height=25></td>
                <td><asp:CheckBox id="cbExcel" text=" Export To Excel" checked="false" runat="server" /></td>
			</tr>
			<tr><td colspan=2>&nbsp;</td></tr>
			<tr>
				<td colspan=2 align=center>
					<asp:ImageButton id=ibConfirm onClick="btnPreview_Click" alternatetext="Preview" imageurl="../../images/butt_print_preview.gif" runat=server/> 
					<input type=image src="../../images/butt_cancel.gif" alt=Cancel onclick="javascript:window.close();" width="58" height="20">
					<asp:Label id=lblPO visible=false text="Purchase Order" runat=server/>
					<asp:Label id=lblGRN visible=false text="Goods Return" runat=server/>
					<asp:Label id=lblDispAdv visible=false text="Dispatch Advise" runat=server/>
					<asp:Label id=lblRPH visible=false text="RPH" runat=server/>
				</td>
			</tr>
		</table>
		<asp:Label id=lblErrMessage visible=false ForeColor=red Text="Error while initiating component." runat=server />	
		<Input Type=Hidden id=hidTaxInit value="" runat=server />	
		<Input Type=Hidden id=hidKPPInit value="" runat=server />		
    </form>
</body>
</html>
