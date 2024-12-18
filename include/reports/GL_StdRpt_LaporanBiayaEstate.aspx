<%@ Page Language="vb" src="../../include/reports/GL_StdRpt_LaporanBiayaEstate.aspx.vb" Inherits="GL_StdRpt_LaporanBiayaEstate" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="GL_STDRPT_SELECTION_CTRL" src="../include/reports/GL_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>General Ledger - Laporan Biaya Estate</title>
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" ID="frmMain">
			<input type=Hidden id=hidUserLocPX runat="server" NAME="hidUserLocPX"/>
			<input type=Hidden id=hidAccMonthPX runat="server" NAME="hidAccMonthPX"/>
			<input type=Hidden id=hidAccYearPX runat="server" NAME="hidAccYearPX"/>
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1">
				<tr>
					<td class="mt-h" colspan="3">GENERAL LEDGER - LAPORAN BIAYA KEBUN</td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6"><hr size="1" noshade></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6"><UserControl:GL_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>			
				<tr>
					<td colspan="6"><hr size="1" noshade></td>
				</tr>
			</table>
			<table width=100% border="0" cellspacing="2" cellpadding="1" ID="Table2" runat=server>
				<tr>
					<td width="15%">Jenis Laporan : </td>
					<td colspan="4"> 
					 <asp:DropDownList width=50% id=ddlRptType runat=server AutoPostBack="true" OnSelectedIndexChanged=SelectBy>
								        <asp:ListItem value="0" Selected=True>Biaya Panen</asp:ListItem>
						                <asp:ListItem value="1">Biaya Pemeliharaan TM</asp:ListItem>
						                <asp:ListItem value="2">Biaya Pemeliharaan TBM</asp:ListItem>
						                <asp:ListItem value="3">Biaya LC</asp:ListItem>
						                <asp:ListItem value="4">Biaya Bibitan</asp:ListItem>
						                <asp:ListItem value="5">Biaya Umum</asp:ListItem>
					                </asp:DropDownList>
							</td>
					<td width="5%">&nbsp;</td>
					
				</tr>
				<tr  id="ttrBy" runat=server>
					<td width="15%">By : </td>
					<td colspan="4"> 
					 <asp:DropDownList width=50% id=ddlBy runat=server AutoPostBack="True" OnSelectedIndexChanged=SelectType>
								        <asp:ListItem value="0" Selected=True>All</asp:ListItem>
						                <asp:ListItem value="1">Divisi</asp:ListItem>
						                <asp:ListItem value="2">Tahun Tanam</asp:ListItem>						                
					                </asp:DropDownList>
							</td>
					<td width="5%">&nbsp;</td>
					
				</tr>
				<tr visible="false" id="ttrType" runat=server >
					<td width="15%">
                        <asp:Label ID="lblTipe" runat="server" Text="" Width="168px"></asp:Label></td>
					<td colspan="4"> 
					 <asp:DropDownList width=50% id=ddlType runat=server AutoPostBack="false">        					                
					                </asp:DropDownList>
							</td>
					<td width="5%">&nbsp;</td>
					
				</tr>
				

				<tr>
					<td colspan="4">
                       <asp:CheckBox id="cbExcel" text=" Export To Excel" checked="false" runat="server" /></td>
				</tr>								
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:ImageButton id="PrintPrev" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" /></td>					
				</tr>				
			</table>
		</form>
		<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" />
		<asp:label id="lblCode" visible="false" text=" Code" runat="server" />
		<asp:label id="lblType" visible="false" text=" Type" runat="server" />
		<asp:label id="lblLocation" visible="false" runat="server" />
		<asp:label id="lblAccDesc" visible="false" runat="server" />
	</body>
</HTML>
