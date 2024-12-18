<%@ Page Language="vb" src="../../include/reports/GL_StdRpt_HKEfektifSKUH.aspx.vb" Inherits="GL_StdRpt_HKEfektifSKUH" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="GL_STDRPT_SELECTION_CTRL" src="../include/reports/GL_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>General Ledger - Managerial Report (Hari Kerja Efektif SKU Harian) Report</title>
                 <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
           <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">

			<input type=Hidden id=hidUserLocPX runat="server" NAME="hidUserLocPX"/>
			<input type=Hidden id=hidAccMonthPX runat="server" NAME="hidAccMonthPX"/>
			<input type=Hidden id=hidAccYearPX runat="server" NAME="hidAccYearPX"/>
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong> GENERAL LEDGER - MANAGERIAL REPORT (HARI KERJA EFEKTIF SKU HARIAN)</strong></td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6"><hr style="width :100%" /></td>
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
					<td width=17%>Month From : </td>  
					<td width=39%>
						<asp:DropDownList id="ddlPhyMonthFrom" size=1 width=20% runat=server />
						<asp:TextBox id="txtPhyYearFrom" size=1 width=20% runat=server />
					</td>
					<td width=17%>To : </td>  
					<td width=39%>
						<asp:DropDownList id="ddlPhyMonthTo" size=1 width=20% runat=server />
						<asp:TextBox id="txtPhyYearTo" size=1 width=20% runat=server />
					</td>
				</tr>
				<tr>
					<td width=17%>Remarks : </td>  
					<td width=39%>
						<asp:TextBox id="txtRemarks" size=1 width=100% textmode="Multiline" runat=server />
					</td>					
				</tr>
				<tr>
					<td colspa=3>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:ImageButton id="PrintPrev" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" />&nbsp;<asp:Button 
                            ID="Issue6" runat="server" class="button-small" Text="Print Preview" />
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
	</body>
</HTML>
