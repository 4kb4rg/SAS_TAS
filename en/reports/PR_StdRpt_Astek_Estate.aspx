<%@ Page Language="vb" src="../../include/reports/PR_StdRpt_Astek_Estate.aspx.vb" Inherits="PR_StdRpt_Astek_Estate" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="PR_StdRpt_Selection_Ctrl" src="../include/reports/PR_StdRpt_Selection_Ctrl_Estate.ascx"%>
<HTML>
	<HEAD>
		<title>Payroll - Astek</title>
		<Preference:PrefHdl id="PrefHdl" runat="server" />
                <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu"  ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
        <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">

			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1" class="font9Tahoma">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong>PAYROLL - ASTEK</strong> </td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">
                        <hr style="width :100%" /> 
                    </td>
				</tr>
				<tr>
					<td colspan="6"></td>
				</tr>
				<tr>
					<td colspan="6"><UserControl:PR_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>			
				<tr>
					<td colspan="6"><asp:Label id="lblDate" forecolor=red visible="false" text="Incorrect Date Format. Date Format is " runat="server" /></td>
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
					<td width=17% style="height: 24px">Divisi :</td>
					<td width=39% style="height: 24px"><GG:AutoCompleteDropDownList id="ddldivisi" width="100%" runat=server/></td>					
					<td width=4% style="height: 24px"></td>	
					<td width=40% style="height: 24px"></td>					
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
					<td width=4% style="height: 24px"></td>	
					<td width=40% style="height: 24px"></td>	
				</tr>					
				<tr>
					<td width=17%>Kategori :</td>
					<td width=39%><asp:DropDownList id="ddlcat" width="30%" runat=server>
										<asp:ListItem value="1">BPJS Ketenagakerja</asp:ListItem>
										<asp:ListItem value="2">BPJS Kesehatan</asp:ListItem>
									</asp:DropDownList>
					</td>									
					<td width=4%>
					</td>	
					<td width=40%></td>					
				</tr>
				<tr>
					<td width=17%><asp:CheckBox id="cbExcel" text=" Export To Excel" checked="false" runat="server" /></td>
					<td width=39%></td>									
					<td width=4%></td>	
					<td width=40%></td>					
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
	</body>
</HTML>
