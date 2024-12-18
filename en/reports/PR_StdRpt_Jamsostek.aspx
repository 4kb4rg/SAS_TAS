<%@ Page Language="vb" src="../../include/reports/PR_StdRpt_Jamsostek.aspx.vb" Inherits="PR_StdRpt_Jamsostek" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="PR_STDRPT_SELECTION_CTRL" src="../include/reports/PR_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Payroll - Daftar Jamsostek Karyawan </title>
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
					<td class="font9Tahoma" colspan="3"><strong>PAYROLL - DAFTAR JAMSOSTEK KARYAWAN</strong> </td>
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
					<td width=40% colspan=3>Accounting Period : &nbsp;										
						<asp:DropDownList id="lstAccMonthFrom" size=1 width=20% runat=server />&nbsp;
						<asp:DropDownList id="lstAccYearFrom" size=1 width=20% autopostback=true onselectedindexchanged=OnIndexChage_FromAccPeriod runat=server /></td>
					<td width=40% align=left colspan=3>To : &nbsp;						
						<asp:DropDownList id="lstAccMonthTo" size=1 width=20% runat=server />
						<asp:DropDownList id="lstAccYearTo" size=1 width=20% autopostback=true onselectedindexchanged=OnIndexChage_FromAccPeriod runat=server />					
					</td>
					
				</tr>
				<tr><td colspan=6 width=100%>&nbsp;</td></tr>
				<tr>
					<td>Employee Type :</td>
					<td align=left> 
						<asp:CheckBoxList id=cblType runat=server RepeatColumns="1" RepeatDirection="Vertical"> 
						<asp:ListItem text="Staff/Non Staff" value="1" />
						<asp:ListItem text="SKU" value="2" />						
						</asp:CheckBoxList></td>					
					<td>&nbsp;</td>	
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>		
				</tr>
				<tr><td colspan=6 width=100%>&nbsp;</td></tr>
				<tr>
					<td colspan=6 width=100%><asp:Label id="lblLocation" visible="false" runat="server" /></td>
				</tr>
				<tr>
					<td colspan=6 width=100%><asp:ImageButton id="PrintPrev"  AlternateText="Print Preview" imageurl="../images/butt_print_preview.gif" onClick="btnPrintPrev_Click" runat="server" />&nbsp;<asp:Button 
                            ID="Issue6" runat="server" class="button-small" Text="Print Preview" />
                    </td>					
				</tr>	
				<tr><td colspan=6 width=100%>&nbsp;</td></tr>
				<tr><td colspan=6 width=100%><asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" /></td></tr>			
			</table>
            </div>
        </td>
        </tr>
        </table>
		</form>
		
	</body>
</HTML>
