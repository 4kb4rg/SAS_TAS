<%@ Page Language="vb" src="../../include/reports/AP_StdRpt_InvRcvNoteListing.aspx.vb" Inherits="AP_StdRpt_InvRcvNoteListing" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="AP_STDRPT_SELECTION_CTRL" src="../include/reports/AP_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Accounts Payable - Invoice Reception Listing </title>
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" ID="frmMain">
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1">
				<tr>
					<td class="mt-h" colspan="3"><asp:label id=lblPageTitle runat=server/></td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6"><hr size="1" noshade></td>
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
					<td colspan="6"><hr size="1" noshade></td>
				</tr>
			</table>
			<table width=100% border="0" cellspacing="1" cellpadding="1" ID="Table2" runat=server>	
				<tr>
					<td>Document No From :</td>
					<td><asp:textbox id="txtDocNoFrom" maxlength=20 width="50%" runat="server" /> (blank for all) </td>
					<td>To :</td>
					<td><asp:textbox id="txtDocNoTo" maxlength=20 width="50%" runat="server" /> (blank for all) </td>
				</tr>
				<tr>
					<td width="15%"><asp:label id=lblInvRcvRefNo runat=server/> :</td>
					<td width="35%"><asp:textbox id="txtInvRcvNo"  maxlength=32 width="50%" runat="server" /> (blank for all) </td>
					<TD width="15%"><asp:label id=lblInvRcvRefDateFrom runat=server/> :</td>
					<td valign="top" width="35%">
					<asp:TextBox ID=txtInvRcvRefDateFrom maxlength="10" width="50%" Runat=server />
					<a href="javascript:PopCal('txtInvRcvRefDateFrom');"><asp:Image id="btnSelDateFrom" runat="server" ImageUrl="../Images/calendar.gif"/></a>
					</td>
				</tr>
				<tr>
					<td>Purchase Order ID :</td>
					<td><asp:textbox id="txtPOID"  maxlength=20 width="50%" runat="server" /> (blank for all)</td>
					<td><asp:label id=lblInvRcvRefDateTo runat=server/> :</td>
					<td valign="top">
					<asp:TextBox ID=txtInvRcvRefDateTo maxlength="10" width="50%" Runat=server />
					<a href="javascript:PopCal('txtInvRcvRefDateTo');"><asp:Image id="btnSelDateTo" runat="server" ImageUrl="../Images/calendar.gif"/></a>
					</td>	
				</tr>
				<tr>
					<td>Supplier Code:</td>
					<td><asp:textbox id="txtSuppCode" maxlength=8  width="50%" runat="server" /> 
					    <input type=button value=" ... " id="Find" onclick="javascript:PopSupplier('frmMain', '', 'txtSuppCode', 'False');" CausesValidation=False runat=server /> (blank for all)</td>
					<TD vAlign="top" height=25>Credit Term Type :</TD>
					<TD vAlign="top">
					<asp:DropDownList id="ddlTermType" width="50%"  runat="server" />
					</TD>						
				</tr>
				<tr>
					<td>Credit Term :</td>
					<td ><asp:textbox id="txtCreditTerm"  maxlength=3 width="50%"  runat="server" /> (blank for all)</td>
					<td>Invoice Due Date From :</td>
					<td valign="top">
					<asp:TextBox ID=txtInvDueDateFrom maxlength="10" width="50%" Runat=server />
					<a href="javascript:PopCal('txtInvDueDateFrom');"><asp:Image id="btnSelDueDateFrom" runat="server" ImageUrl="../Images/calendar.gif"/></a>
					</td>	
				</tr>
				<tr>
					<td> Invoice Type: </td>
					<td ><asp:dropdownlist id="ddlInvoiceType" width="50%" maxlength=8 runat="server" /> </td>	
					<td>Invoice Due Date To :</td>
					<td valign="top">
					<asp:TextBox ID=txtInvDueDateTo maxlength="10" width="50%" Runat=server />
					<a href="javascript:PopCal('txtInvDueDateTo');"><asp:Image id="btnSelDueDateTo" runat="server" ImageUrl="../Images/calendar.gif"/></a>
					</td>
				</tr>			
				<tr>
					<td> Status: </td>
					<td ><asp:dropdownlist id="ddlStatus" width="50%" maxlength=8 runat="server" /> </td>	
				</tr>	
				<tr>
					<td colspan=3><asp:Label id="lblLocation" visible="false" runat="server" /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=3><asp:ImageButton id="PrintPrev" AlternateText="Print Preview" imageurl="../images/butt_print_preview.gif" onClick="btnPrintPrev_Click" runat="server" /></td>						
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
		</form>
	</body>
</HTML>
