<%@ Page Language="vb" src="../../include/reports/PR_StdRpt_MonthlyTaxRpt.aspx.vb" Inherits="PR_StdRpt_MonthlyTaxRpt" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="PR_STDRPT_SELECTION_CTRL" src="../include/reports/PR_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Payroll - Daftar Pajak Karyawan </title>
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
					<td class="font9Tahoma" colspan="3"><strong> PAYROLL - DAFTAR PAJAK KARYAWAN </strong></td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6"><hr size="1" noshade></td>
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
					<td colspan="6"><hr size="1" noshade></td>
				</tr>
			</table>
			<table width=100% border="0" cellspacing="1" cellpadding="1" ID="Table2" class="font9Tahoma" runat=server>	
				<tr>
					<td width=17%>Physical Period : </td>  
					<td width=39%>
						<asp:DropDownList id="ddlPhyMonthFrom" size=1 width=20% runat=server />
						<asp:TextBox id="txtPhyYearFrom" size=1 width=20% runat=server />
					</td>
					<td>&nbsp;</td>
					<td width=17%>To : </td> 
					<td width=39%>
						<asp:DropDownList id="ddlPhyMonthTo" size=1 width=20% visible=true runat=server />
						<asp:TextBox id="txtPhyYearTo" size=1 width=20% visible=true runat=server />
					</td>

				</tr>
				<tr>
					<td width=17%>Employee Type : </td>  
					<td width=39%>
						<asp:DropDownList id="ddlEmpCategory" size=1 width=80% visible=true runat=server />
					</td>					
				</tr>				
				<tr>
					<td colspan=4><asp:Label id="lblLocation" visible="false" runat="server" /></td>
				</tr>
				<tr>
					<td><asp:ImageButton id="PrintPrev" AlternateText="Print Preview" imageurl="../images/butt_print_preview.gif" onClick="btnPrintPrev_Click" runat="server" />&nbsp;<asp:Button 
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
	</body>
</HTML>
