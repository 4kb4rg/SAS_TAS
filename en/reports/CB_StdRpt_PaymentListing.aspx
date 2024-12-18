<%@ Page Language="vb" src="../../include/reports/CB_StdRpt_PaymentListing.aspx.vb" Inherits="CB_StdRpt_PaymentListing" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="CB_STDRPT_SELECTION_CTRL" src="../include/reports/CB_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Accounts Payable - Payment Listing </title>
         <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
           <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 

			<table border="0" cellspacing="1" cellpadding="1" width="100%"  class="font9Tahoma" ID="Table1">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong> CASH BANK - PAYMENT LISTING</strong></td>
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
					<td colspan="6"><UserControl:CB_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6"><asp:Label id="lblDate" forecolor=red visible="false" text="Incorrect Date Format. Date Format is " runat="server" />
									<asp:Label id="lblDateFormat" forecolor=red visible="false" runat="server" />				
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
					<td width="17%">Payment ID From :</td>
					<td width="39%"><asp:textbox id="txtPaymentIDFrom" width="50%"  maxlength=32 runat="server" /> (blank for all) </td>
					<td width="4%">To :</td>
					<td width="40%"><asp:textbox id="txtPaymentIDTo" width="50%"  maxlength=32 runat="server" /> (blank for all) </td>
				</tr>
				<tr>
					<td>Date From:</td>
					<td>    
						<asp:Textbox id="txtDateFrom" width=50% maxlength=10 runat=server/>
						<a href="javascript:PopCal('txtDateFrom');"><asp:Image id="btnDateFrom" runat="server" ImageUrl="../images/calendar.gif"/></a>
					</td>					
					<td>Date To:</td>
					<td>
						<asp:Textbox id="txtDateTo" width=50% maxlength=10 runat=server/>
						<a href="javascript:PopCal('txtDateTo');"><asp:Image id="btnDateTo" runat="server" ImageUrl="../images/calendar.gif"/></a>
					</td>							
				</tr>	
				<tr>
					<td>Supplier Code:</td>
					<td><asp:textbox id="txtSuppCode" maxlength=20 width="50%"  runat="server" /> 
					    <input type=button value=" ... " id="Find" onclick="javascript:PopSupplier('frmMain', '', 'txtSuppCode', 'False');" CausesValidation=False runat=server /> (blank for all)</td>			
					<td>&nbsp;</td>
					<td>&nbsp;</td>							
				</tr>	
				<tr>
					<td>Payment Type :</td>
					<td>
					   <asp:DropDownList id="ddlPaymentType"  width="50%" runat="server" > 
					      <asp:ListItem value="0">All</asp:ListItem>
					      <asp:ListItem value="1">Cheque</asp:ListItem>
						  <asp:ListItem value="2">Cash</asp:ListItem>
						  <asp:ListItem value="3">Need Verification</asp:ListItem>
					   </asp:DropDownList>					
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
				</tr>
				<tr>
					<td>Bank Code :</td>
					<td><asp:DropDownList id="ddlBankCode" width="50%" runat="server" /> (blank for all) </td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
				</tr>
				<tr>
					<td>Cheque No :</td>
					<td><asp:TextBox id="txtChequeNo"  maxlength=20 width="50%" runat="server" /> (blank for all) </td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
				</tr>				
				<tr>
					<td>Document ID :</td>
					<td><asp:TextBox id="txtDocumentID"  maxlength=200 width="50%" runat="server" /> (blank for all) </td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
				</tr>								
				<tr>
					<td><asp:label id="lblCOACode" runat="server" /> Code :</td>
					<td><asp:textbox id="txtCOACode"  maxlength=32 width="50%"  runat="server" /> 
					    <input type="button" id=btnFind1 value=" ... " onclick="javascript:PopCOA('frmMain', '', 'txtCOACode', 'False');" runat=server/> (blank for all)</td>
					<td>&nbsp;</td>						
				</tr>	
				<tr>
				
				<td>Document Type :</td>
					<td>
					   <asp:DropDownList id="ddlDocumentType"  width="50%" runat="server" > 
					      <asp:ListItem value="0">All</asp:ListItem>
					      <asp:ListItem value="1">Invoice</asp:ListItem>
						  <asp:ListItem value="2">Debit Note</asp:ListItem>
						  <asp:ListItem value="3">Credit Note</asp:ListItem>
						  <asp:ListItem value="4">Payment</asp:ListItem>
						  <asp:ListItem value="5">Creditor Jurnal</asp:ListItem>
						  <asp:ListItem value="6">Advance Payment</asp:ListItem>
					   </asp:DropDownList>	
					   	
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>		
				</tr>	
																		
				<tr>
					<td> Status: </td>
					<td ><asp:dropdownlist id="ddlStatus" width="50%"  runat="server" /> </td>	
					<td>&nbsp;</td>
					<td>&nbsp;</td>	
				</tr>	
				
				<tr>
					<td width=25%>Verification By : </td>
					<td colspan=2><asp:RadioButton id="rbAcc" text=" Accounting" checked="true" GroupName="rbVer" runat="server" />
						<asp:RadioButton id="rbFin" text=" Finance" GroupName="rbVer" runat="server" />
					</td>			
					<td colspan=3>&nbsp;</td>
				</tr>
				<tr>
					<td width=25%>Print For : </td>
					<td colspan=2><asp:RadioButton id="rbPLis" text=" Listing" checked="true" GroupName="rbPrint" runat="server" />
					    <asp:RadioButton id="rbPFin" text=" Finance" GroupName="rbPrint" runat="server" />
						<asp:RadioButton id="rbPPjk" text=" Tax" GroupName="rbPrint" runat="server" />
					</td>			
					<td colspan=3>&nbsp;</td>
				</tr>
				<tr>
					<td width=25%>Include Other Payment : </td>
					<td colspan=2>
					    <asp:RadioButton id="rbOPYes" text=" Yes" checked="true" GroupName="rbOtherPayment" runat="server" />
						<asp:RadioButton id="rbOPNo" text=" No" GroupName="rbOtherPayment" runat="server" />
					</td>			
					<td colspan=3>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=3><asp:Label id="lblLocation" visible="false" runat="server" /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
				</tr>
				 <tr>
					<td colspan="5">
                       <asp:CheckBox id="cbExcel" text=" Export To Excel" checked="false" runat="server" /></td>
				</tr>
				<tr>
					<td colspan=4>
						<asp:Label id=lblErrDate visible=false forecolor=red runat=server/>
					</td>
				</tr>
				<tr>
					<td colspan=3><asp:ImageButton id="PrintPrev" AlternateText="Print Preview" imageurl="../images/butt_print_preview.gif" onClick="btnPrintPrev_Click" runat="server" />&nbsp;<asp:Button 
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
		<asp:label id=lblInvRcv visible=false runat=server/>
	</body>
</HTML>
