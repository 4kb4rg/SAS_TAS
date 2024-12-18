<%@ Page Language="vb" src="../../include/reports/AP_StdRpt_MutasiAccountPayableCurr.aspx.vb" Inherits="AP_StdRpt_MutasiAccountPayableCurr" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="AP_STDRPT_SELECTION_CTRL" src="../include/reports/AP_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Account Payable - Summary A/P Outstanding In Different Foreign Exchange Rate</title>
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
					<td class="font9Tahoma" colspan="3"><strong> ACCOUNT PAYABLE - SUMMARY A/P OUTSTANDING IN DIFFERENT FOREIGN EXCHANGE RATE REPORT</strong></td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6"><hr style="width :100%" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6"><UserControl:AP_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>			
				<tr>
					<td colspan="6"><hr style="width :100%" /></td>
				</tr>
			</table>
			<table width=100% border="0" cellspacing="2" cellpadding="1" ID="Table2" class="font9Tahoma" runat=server>	
			
				
				<tr>
					<td width=17%>Accounting Period : </td>  
					<td width=39%>
						<asp:DropDownList id="ddlSrchAccMonthFrom" size=1 width=10% runat=server />
						<asp:DropDownList id="ddlSrchAccYearFrom" size=1 width=10% autopostback=true onselectedindexchanged=OnIndexChage_FromAccPeriod runat=server />
					</td>
				</tr>		
				<tr>
				 <td width=17%>Report Criteria: </td>  
				 <td width=39%>
				 <asp:DropDownList width=50% id=ddlRtpType runat=server>
							<asp:ListItem value="0" Selected>All Sort By Supplier</asp:ListItem>
						    <asp:ListItem value="1">All Sort By Currency</asp:ListItem> 
						    <asp:ListItem value="2">Advance Payment Without Receive</asp:ListItem> 
					</asp:DropDownList></td>
				  </tr>
											
				<tr>
					<td colspan=4>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="4">
                       <asp:CheckBox id="cbExcel" text=" Export To Excel" checked="false" runat="server" /></td>
				</tr>								
				<tr>
					<td colspan=4>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=4 align=left><asp:ImageButton id="PrintPrev" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" />&nbsp;<asp:Button 
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
