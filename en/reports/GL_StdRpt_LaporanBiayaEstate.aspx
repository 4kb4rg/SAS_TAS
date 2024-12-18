<%@ Page Language="vb" src="../../include/reports/GL_StdRpt_LaporanBiayaEstate.aspx.vb" Inherits="GL_StdRpt_LaporanBiayaEstate" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="GL_STDRPT_SELECTION_CTRL" src="../include/reports/GL_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>General Ledger - Laporan Biaya Estate</title>
                 <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
           <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"

			<input type=Hidden id=hidUserLocPX runat="server" NAME="hidUserLocPX"/>
			<input type=Hidden id=hidAccMonthPX runat="server" NAME="hidAccMonthPX"/>
			<input type=Hidden id=hidAccYearPX runat="server" NAME="hidAccYearPX"/>
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1" class="font9Tahoma">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong>GENERAL LEDGER - LAPORAN BIAYA KEBUN</strong> </td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">
                        <hr style="width :100%" /> 
                    </td>
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
					<td colspan="6">
                        <hr style="width :100%" /> 
                    </td>
				</tr>
			</table>
			<table width=100% border="0" cellspacing="2" cellpadding="1" ID="Table2" class="font9Tahoma" runat=server>
				<tr>
					<td width="15%">Jenis Laporan : </td>
					<td colspan="4"> 
					 <asp:DropDownList width=50% id=ddlRptType runat=server AutoPostBack="true" OnSelectedIndexChanged=SelectBy>
					                    <asp:ListItem value="99">Summary Biaya Kebun</asp:ListItem>
										<%--<asp:ListItem value="24">Areal Statement</asp:ListItem>--%>
										<%--<asp:ListItem value="33">Rekap Statistik Panen</asp:ListItem>--%>										
										<%--<asp:ListItem value="12">Rekap Biaya Kebun</asp:ListItem>--%>										
										<asp:ListItem value="29">Rekap Biaya Panen & Perawatan</asp:ListItem>									
										<asp:ListItem value="34">Rekap Biaya Panen</asp:ListItem>										
										<asp:ListItem value="25">Rekap Biaya Pemeliharaan TM</asp:ListItem>
										<asp:ListItem value="26">Rekap Biaya Pemeliharaan TBM</asp:ListItem>
										<asp:ListItem value="27">Rekap Biaya LC</asp:ListItem>
										<asp:ListItem value="28">Rekap Biaya Bibitan</asp:ListItem>										
										<asp:ListItem value="30">Rekap Biaya Umum</asp:ListItem>
										<asp:ListItem value="13">Rekap Biaya Transit Kendaraan</asp:ListItem>
										<asp:ListItem value="31">Rekap Pemakaian Bahan</asp:ListItem>
										<asp:ListItem value="32">Rekap Pemupukan</asp:ListItem>
										<%--<asp:ListItem value="9">Detail Statistik Panen</asp:ListItem>--%>
										<asp:ListItem value="14">Detail Biaya Panen & Perawatan</asp:ListItem>	
								        <asp:ListItem value="0">Detail Biaya Panen</asp:ListItem>									
						                <asp:ListItem value="1">Detail Biaya Pemeliharaan TM</asp:ListItem>
						                <asp:ListItem value="2">Detail Biaya Pemeliharaan TBM</asp:ListItem>									
						                <asp:ListItem value="3">Detail Biaya LC</asp:ListItem>
						                <asp:ListItem value="4">Detail Biaya Bibitan</asp:ListItem>
						                <asp:ListItem value="5">Detail Biaya Umum</asp:ListItem>
										<asp:ListItem value="8">Detail Biaya Transit Kendaraan</asp:ListItem>																				
										<asp:ListItem value="23">Detail Pemakaian Bahan</asp:ListItem>
										<asp:ListItem value="22">Detail Pemupukan</asp:ListItem>
										<%--	<asp:ListItem value="6">Biaya Panen Karet</asp:ListItem> --%>
										<%--	<asp:ListItem value="7">Biaya Pemeliharaan Karet</asp:ListItem> --%>
										<%--	<asp:ListItem value="10">Biaya Pemeliharaan Jati</asp:ListItem> --%>
										<%--	<asp:ListItem value="11">Statistik Panen Karet</asp:ListItem> --%>
										<%--	<asp:ListItem value="20">Transit Upah Payroll</asp:ListItem> --%>
										<%--	<asp:ListItem value="21">Alokasi Upah Payroll</asp:ListItem> --%>
										
										</asp:DropDownList>
							</td>
					<td width="5%">&nbsp;</td>
					
				</tr>
				<tr  id="ttrBy" runat=server hidden>
					<td width="15%">By : </td>
					<td colspan="4"> 
					 <asp:DropDownList width=50% id=ddlBy runat=server AutoPostBack="True" OnSelectedIndexChanged=SelectType>
								        <asp:ListItem value="0" Selected=True>All</asp:ListItem>
						                <asp:ListItem value="1">Divisi</asp:ListItem>
						                <asp:ListItem value="2">Tahun Tanam</asp:ListItem>
										<asp:ListItem value="3">Blok</asp:ListItem>						                
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
					<td><asp:ImageButton id="PrintPrev" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" />&nbsp;<asp:Button 
                            ID="Issue6" runat="server" class="button-small" Text="Print Preview" 
                            Visible="False" />
                    </td>					
				</tr>				
			</table>
                </div>
            </td>
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
