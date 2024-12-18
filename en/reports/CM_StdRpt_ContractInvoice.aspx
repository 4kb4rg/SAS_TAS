<%@ Page Language="vb" src="../../include/reports/CM_StdRpt_ContractInvoice.aspx.vb" Inherits="CM_StdRpt_ContractInvoice" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="CM_STDRPT_SELECTION_CTRL" src="../include/reports/CM_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Mill Contract - Contract Invoice</title>
             <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
           <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 

			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong>MILL CONTRACT - CONTRACT INVOICE</strong> </td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6"><hr style="width :100%" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6"><UserControl:CM_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>			
				<tr>
					<td colspan="6"><hr size="1" noshade></td>
				</tr>
			</table>
			<table width=100% border="0" cellspacing="2" cellpadding="1" ID="Table2" class="font9Tahoma" runat=server>
				<tr>
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
					<td width=39%><asp:Textbox id=txtFromInvoiceId maxlength=20 width=50% runat=server/> (leave blank for all)</td>			
					<td width=4%>To : </td>	
					<td width=40%><asp:Textbox id=txtToInvoiceId maxlength=20 width=50% runat=server/> (leave blank for all)</td>
				</tr>
				<tr>
					<td width=17%>Invoice Date. From : </td>
					<td width=39%><asp:Textbox id=txtFromInvoiceDate maxlength=20 width=50% runat=server/> (leave blank for all)</td>			
					<td width=4%>To : </td>	
					<td width=40%><asp:Textbox id=txtToInvoiceDate maxlength=20 width=50% runat=server/> (leave blank for all)</td>
				</tr>	
				<tr>
					<td width=17%><asp:label id=lblBillParty runat=server /> : </td>
					<td width=39%><asp:dropdownlist id=ddlBuyer runat=server /></td>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr>
					<td width=17%>Invoice Type : </td>
					<td width=39%><asp:dropdownlist id=ddlInvoiceType runat=server /></td>
					<td colspan=2>&nbsp;</td>
				</tr>						
				<tr>
					<td colspan=4>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=4><asp:ImageButton id="PrintPrev" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" />&nbsp;<asp:Button 
                            ID="Issue6" runat="server" class="button-small" Text="Print Preview" 
                            Height="26px" Visible="False" />
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
