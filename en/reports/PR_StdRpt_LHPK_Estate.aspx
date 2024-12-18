<%@ Page Language="vb" src="../../include/reports/PR_StdRpt_LHPK_Estate.aspx.vb" Inherits="PR_StdRpt_LHPK_Estate" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="PR_StdRpt_Selection_Ctrl" src="../include/reports/PR_StdRpt_Selection_Ctrl_Estate.ascx"%>
<HTML>
	<HEAD>
		<title>Estate - LHPK</title>
                <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
        <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">

			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1"  class="font9Tahoma">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong> LHPK - Laporan Harian Produksi Karet</strong></td>
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
			<table width=100% border="0" cellspacing="1" cellpadding="1" ID="Table2"  class="font9Tahoma" runat=server>	
			<tr>
					<td colspan=4><asp:Label id="lblLocation" visible="false" runat="server" /></td>
				</tr>
				<tr>
					<td width=17% style="height: 24px">
                        Divisi :</td>
					<td width=39% style="height: 24px">
                        <GG:AutoCompleteDropDownList ID="ddldivisi" runat="server" Width="100%" OnSelectedIndexChanged="ddldivisi_OnSelectedIndexChanged" AutoPostBack=true /></td>					
					<td width=4% style="height: 24px"></td>	
					<td width=40% style="height: 24px"></td>					
				</tr>
				<tr>
					<td width=17%>Mandor :</td>
					<td width=39%><GG:AutoCompleteDropDownList id="ddlmandor" width="100%" runat=server /></td>					
					<td width=4%></td>	
					<td width=40%></td>					
				</tr>
				<tr>
					<td width=17% style="height: 27px">
                        Tanggal :</td>
					<td width=39% style="height: 27px">
                        <asp:TextBox ID="txtTrxDate" runat="server" MaxLength="30" Width="35%"></asp:TextBox>
                        <a href="javascript:PopCal('txtTrxDate');"><asp:Image ID="btnFrom" runat="server" ImageUrl="../Images/calendar.gif" /></a>&nbsp;
					</td>					
					<td width=4% style="height: 27px"></td>	
					<td width=40% style="height: 27px"></td>					
				</tr>
								
				<%--<tr>
					<td width=17%>
                        Sub Kategori :</td>
					<td width=39%>
                        <GG:AutoCompleteDropDownList id="ddlsubcat" width="100%" runat=server />
                       
					</td>					
					<td width=4%></td>	
					<td width=40%></td>					
				</tr>--%>
				
				<tr>
					<td><asp:ImageButton id="PrintPrev" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" /></td>					
				</tr>				
			</table>
            </div>
        </td>
        </tr>
        </table>
		</form>
		<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" ForeColor=red/>
	</body>
</HTML>
