<%@ Page Language="vb" src="../../include/reports/PR_StdRpt_DaftarAbsensi.aspx.vb" Inherits="PR_StdRpt_DaftarAbsensi" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="PR_STDRPT_SELECTION_CTRL" src="../include/reports/PR_StdRpt_Selection_Ctrl.ascx"%>
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
					<td class="font9Tahoma" colspan="3"><strong>PAYROLL - DAFTAR ABSENSI REPORT</strong> </td>
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
					<td width=17%>Date From :</td>
					<td width=39%><asp:Textbox id=txtDateFrom text="" maxlength=128 width=50% runat=server/>
						<a href="javascript:PopCal('txtDateFrom');">
						<asp:Image id="btnDateFrom" runat="server" ImageUrl="../Images/calendar.gif"/></a>
						<asp:Label id=lblDateFromFmt display=dynamic forecolor=red visible=false runat="server"/> 
						<asp:label id=lblErrAttdDate visible=false text="<br>Invalid range of Date." forecolor=red runat=server/>
						<asp:label id=lblErrMaxDate visible=false  text="<br>Maximum range of date is 31 days." forecolor=red runat=server/>
						<asp:label id=lblErrAttdDateDesc visible=false text="<br>Date format is " runat=server/>												
					</td>					
					<td width=4%>To : </td>	
					<td width=40%><asp:Textbox id=txtDateTo maxlength=128 width=50% runat=server/>
						<a href="javascript:PopCal('txtDateTo');">
						<asp:Image id="btnDateTo" runat="server" ImageUrl="../Images/calendar.gif"/></a>
						<asp:Label id=lblDateToFmt display=dynamic forecolor=red visible=false runat="server"/> 										
					</td>					
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
