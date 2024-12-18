<%@ Page Language="vb" src="../../include/reports/AR_StdRpt_ContractInvoiceExport.aspx.vb" Inherits="AR_StdRpt_ContractInvoiceExport" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="AR_STDRPT_SELECTION_CTRL" src="../include/reports/AR_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Account Receivables- Contract Invoice Export</title>
                 <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
           <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 

			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1">
				<tr>
					<td class="font9Tahoma" colspan="3">ACCOUNT RECEIVABLES - INVOICE EXPORT</td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6"><hr style="width :100%" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6"><UserControl:AR_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>

				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>					
				<tr>
					<td colspan=6>
						<asp:Label id="lblDate" forecolor=red visible="false" text="Incorrect Date Format. Date Format is " runat="server" />
						<asp:Label id="lblDateFormat" forecolor=red visible="false" runat="server" />
					</td>
				</tr>		
				<tr>
					<td colspan="6"><hr style="width :100%" /></td>
				</tr>
			</table>
			<table width=100% border="0" cellspacing="2" cellpadding="1" ID="Table2" class="font9Tahoma" runat=server>
				<tr id=TrSupp visible=false runat=server>
					<td>Suppress zero balance : </td>
					<td>
						<asp:RadioButton id="rbSuppYes" text="Yes" GroupName="rbSupp" runat="server" />			
						<asp:RadioButton id="rbSuppNo" text="No" checked="true" GroupName="rbSupp" runat="server" />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td width=17%>Invoice No. From : </td>
					<td width=39%><asp:dropdownlist id=ddlFromInvoiceId width=100% AutoPostBack="True" onSelectedIndexChanged="InvoiceIndexChanged" runat=server />			
					              <asp:Label id=lblErrItem forecolor=red visible=false text="Please select one Invoice" runat=server />
					</td>
				</tr>			
				<tr>
					<td width=17%>Invoice Date From : </td>
					<td width=39%>
						<asp:Textbox id=txtFromInvoiceDate maxlength=30 width=50% runat=server/>
						<a href="javascript:PopCal('txtFromInvoiceDate');">
						<asp:Image id="btnFromInvoiceDate" runat="server" ImageUrl="../Images/calendar.gif"/></td>			
					<td width=4%>To : </td>	
					<td width=40%>
						<asp:Textbox id=txtToInvoiceDate maxlength=30 width=50% runat=server/>
						<a href="javascript:PopCal('txtToInvoiceDate');">
						<asp:Image id="btnToInvoiceDate" runat="server" ImageUrl="../Images/calendar.gif"/></td>
				</tr>	
				<tr>
					<td width=17%>Beneficiary Address : </td>
					<td width=39%>
						<asp:TextBox id=txtNotify size=1 width=100% Rows=4 textmode="Multiline" runat=server /></td>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr>
					<td width=17%><asp:label id=lblBillParty runat=server /> : </td>
					<td width=39%><asp:textbox id=txtBuyer maxlength=128 width=100% runat=server /></td>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr>
					<td width=17%><asp:label id=lblBillPartyAddress runat=server /> : </td>
					<td width=39%>
						<asp:TextBox id=txtBillPartyAddress size=1 width=100% Rows=4 textmode="Multiline" runat=server /></td>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr>
					<td width=17%>Billing Type : </td>
					<td width=39%><asp:dropdownlist id=ddlInvDocType width=100% runat=server /></td>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr>
					<td width=17%>Shipped for your account and risk per : </td>
					<td width=39%>
						<asp:textbox id=txtRisk maxlength=128 width=100% runat=server />						
					</td>
					<td colspan=2>&nbsp;</td>
				</tr>	
				<tr>
					<td width=17%>Delivery Date : </td>
					<td width=39%>
						<asp:Textbox id=txtDeliveryDate maxlength=30 width=50% runat=server/>
						<a href="javascript:PopCal('txtDeliveryDate');">
						<asp:Image id="btnDeliveryDate" runat="server" ImageUrl="../Images/calendar.gif"/></td>																
				</tr>	
				<tr>
					<td width=17%>FOB : </td>
					<td width=39%>
						<asp:textbox id=txtFOB maxlength=128 width=100% runat=server />						
					</td>
					<td colspan=2>&nbsp;</td>
				</tr>	
				<tr>
					<td width=17%>Destination : </td>
					<td width=39%>
						<asp:textbox id=txtDestination maxlength=128 width=100% runat=server />						
					</td>
					<td colspan=2>&nbsp;</td>
				</tr>	
				<tr>
				    <td>L/C :</td>
				    <td><asp:CheckBox id="ChkLC" Text="  No" checked=false AutoPostBack=true OnCheckedChanged=LC_Type runat=server /></td>
				    <td>&nbsp;</td>				    
				</tr>
				<tr id="WLC1">
					<td width=17%>L/C No. : </td>
					<td width=39%>
						<asp:textbox id=txtLCNo maxlength=128 width=100% runat=server />						
					</td>
				</tr>	
				<tr id="WLC2">
					<td width=17%>Date of Issue : </td>
					<td width=39%>
						<asp:Textbox id=txtIssueDate maxlength=30 width=50% runat=server/>
					</td>
				</tr>	
				<tr id="WLC3">
					<td width=17%>From : </td>
					<td width=39%>
						<asp:Textbox id=txtFrom size=1 width=100% Rows=3 textmode="Multiline" runat=server/>
					</td>
				</tr>	
				<tr id="WOLC1">
					<td width=17%>Advising Bank : </td>
					<td width=39%>
						<asp:DropDownList id="ddlBankCode" runat="server" Width="100%"></asp:DropDownList></td>
				</tr>		
				<tr>
					<td width=17%>Undersigned Name : </td>
					<td width=39%>
						<asp:textbox id=txtUndName maxlength=128 width=100% runat=server />
						<asp:label id=lblUndName visible=false runat=server />
					</td>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr>
					<td width=17%>Undersigned Post : </td>
					<td width=39%>
						<asp:textbox id=txtUndPost maxlength=128 width=100% runat=server />
						<asp:label id=lblUndPost visible=false runat=server />
					</td>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr>
					<td width=17%>Specification : </td>
					<td width=39%>
						<asp:TextBox id=txtSpecification size=1 width=100% Rows=4 textmode="Multiline" runat=server /></td>
					<td colspan=2>&nbsp;</td>
				</tr>	
					
				<tr>
					<td colspan=4>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=4><asp:ImageButton id="PrintPrev" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" />&nbsp;<asp:Button 
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
