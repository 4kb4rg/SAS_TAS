<%@ Page Language="vb" src="../../include/reports/PR_StdRpt_DaftarMuatPupuk_Estate.aspx.vb" Inherits="PR_StdRpt_DaftarMuatPupuk_Estate" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="PR_STDRPT_SELECTION_CTRL" src="../include/reports/PR_StdRpt_Selection_Ctrl_Estate.ascx"%>
<HTML>
	<HEAD>
		<title>Payroll - Daftar Absensi Report </title>
                <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
        <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">

			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1" class="font9Tahoma">
				<tr>
					<td class="mt-h" colspan="3">PAYROLL - DAFTAR B/M PUPUK REPORT</td>
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
					<td colspan="6"><UserControl:PR_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
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
			<table width=100% border="0" cellspacing="1" cellpadding="1" ID="Table2" class="font9Tahoma" runat=server>	
				<tr>
					<td colspan=4><asp:Label id="lblLocation" visible="false" runat="server" /></td>
				</tr>
				<tr>
					<td width=17%>Divisi :</td>
					<td width=39%><GG:AutoCompleteDropDownList id="ddldivisi" width="100%" runat=server /></td>					
					<td width=4%></td>	
					<td width=40%></td>					
				</tr>
				<tr>
					<td width=17%>Mandor :</td>
					<td width=39%><GG:AutoCompleteDropDownList id="ddlmandor" width="100%" runat=server /></td>					
					<td width=4%></td>	
					<td width=40%></td>					
				</tr>
				<tr>
					<td width=17%>Karyawan :</td>
					<td width=39%><GG:AutoCompleteDropDownList id="ddlempcode" width="100%" runat=server /></td>					
					<td width=4%></td>	
					<td width=40%></td>					
				</tr>
				<tr>
					<td width=17%>Periode :</td>
					<td width=39%><asp:DropDownList id="ddlMonth" width="30%" runat=server>
										<asp:ListItem value="01">Januari</asp:ListItem>
										<asp:ListItem value="02">Februari</asp:ListItem>
										<asp:ListItem value="03">Maret</asp:ListItem>
										<asp:ListItem value="04">April</asp:ListItem>
										<asp:ListItem value="05">Mei</asp:ListItem>
										<asp:ListItem value="06">Juni</asp:ListItem>
										<asp:ListItem value="07">Juli</asp:ListItem>
										<asp:ListItem value="08">Augustus</asp:ListItem>
										<asp:ListItem value="09">September</asp:ListItem>
										<asp:ListItem value="10">October</asp:ListItem>
										<asp:ListItem value="11">November</asp:ListItem>
										<asp:ListItem value="12">Desember</asp:ListItem>
									</asp:DropDownList>
									<asp:DropDownList id="ddlyear" width="20%" runat=server></asp:DropDownList>
					</td>					
					<td width=4%></td>	
					<td width=40%></td>					
				</tr>
				<tr>
					<td width=17%>Divisi Kerja:</td>
					<td width=39%><GG:AutoCompleteDropDownList id="ddldivisikerja" width="100%" runat=server /></td>					
					<td width=4%></td>	
					<td width=40%></td>					
				</tr>
				<tr>
					<td>
						<asp:Label id="lblErrDate" visible="false" forecolor=red text="Please Choose Date From and Date To !" runat="server" />
						<asp:Label id=lblvaldate visible=false forecolor=red text="Invalid date format  (DD/MM/YYYY)" runat=server/>
					</td>
				</tr>
				<tr>
					<td><asp:ImageButton id="PrintPrev" AlternateText="Print Preview" imageurl="../images/butt_print_preview.gif" onClick="btnPrintPrev_Click" runat="server" />&nbsp;<asp:Button 
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
	</body>
</HTML>
