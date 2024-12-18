<%@ Page Language="vb" src="../../include/reports/AP_StdRpt_OutstandingPayment.aspx.vb" Inherits="AP_StdRpt_OutstandingPayment" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="AP_STDRPT_SELECTION_CTRL" src="../include/reports/AP_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Accounts Payable - Outstanding Payment </title>
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
					<td class="font9Tahoma" colspan="3"><strong> ACCOUNTS PAYABLE - OUTSTANDING PAYMENT</strong></td>
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
					<td colspan="6"><asp:Label id="lblDate" forecolor=red visible="false" text="Incorrect Date Format. Date Format is " runat="server" />
									<asp:Label id="lblDateFormat" forecolor=red visible="false" runat="server" />				
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>			
				<tr>
					<td colspan="6"><hr style="width :100%" /></td>
				</tr>
			</table>
			<table width=100% border="0" cellspacing="1" cellpadding="1" ID="Table2"   class="font9Tahoma" runat=server>	
				<tr>
					<td width="15%">Supplier Code:</td>
					<td width="35%"><asp:textbox id="txtSuppCode" maxlength=20 width="50%"  runat="server" /> (blank for all)</td>					
					<td>&nbsp;</td>
					<td>&nbsp;</td>							
				</tr>
				<tr>
					<td>Payment Status :</td>
					<td><asp:DropDownList width=50% id=ddlSortBy runat=server>
					<asp:ListItem value="0">All</asp:ListItem>
					<asp:ListItem value="1" Selected >Outstanding</asp:ListItem>
					<asp:ListItem value="2">Paymented</asp:ListItem>
					</asp:DropDownList></td>
				</tr>

				<tr>
					<td colspan=3><asp:Label id="lblLocation" visible="false" runat="server" /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=3><asp:ImageButton id="PrintPrev" AlternateText="Print Preview" imageurl="../images/butt_print_preview.gif" onClick="btnPrintPrev_Click" runat="server" />&nbsp;<asp:Button 
                            ID="Issue6" runat="server" class="button-small" Text="Print Preview" 
                            Visible="False" />
                    </td>						
				</tr>
				<tr>
					<td colspan=3>
						<asp:label id=lblAccPayable text="ACCOUNT PAYABLES" visible=false runat=server/>
						<asp:label id=lblListing text=" LISTING" visible=false runat=server/>
						<asp:label id=lblDash text = " - " visible=false runat=server/>
						<asp:label id=lblInvRcv visible=false runat=server/>
						<asp:label id=lblRefNo text=" Ref No" visible=false runat=server/>
						<asp:label id=lblRefDateFrom text=" Ref Date From" visible=false runat=server/>
						<asp:label id=lblRefDateTo text=" Ref Date To" visible=false runat=server/>
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
