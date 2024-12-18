<%@ Page Language="vb" src="../../include/reports/AR_StdRpt_ContractCreditNote.aspx.vb" Inherits="AR_StdRpt_ContractCreditNote" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="AR_STDRPT_SELECTION_CTRL" src="../include/reports/AR_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Account Receivables- Credit Note</title>
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
					<td class="font9Tahoma" colspan="3"><strong>ACCOUNT RECEIVABLES - CREDIT NOTE</strong> </td>
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
					<td width=17%>Credit Note ID From : </td>
					<td width=39%><asp:Textbox id=txtFromCreditNoteId maxlength=20 width=50% runat=server/> (blank for all)</td>			
					<td width=4%>To : </td>	
					<td width=40%><asp:Textbox id=txtToCreditNoteId maxlength=20 width=50% runat=server/> (blank for all)</td>
				</tr>
				<tr>
					<td width=17%>Credit Note Date From : </td>
					<td width=39%>
						<asp:Textbox id=txtFromCreditNoteDate maxlength=20 width=50% runat=server/>
						<a href="javascript:PopCal('txtFromCreditNoteDate');">
						<asp:Image id="btnFromCreditNoteDate" runat="server" ImageUrl="../Images/calendar.gif"/></td>			
					<td width=4%>To : </td>	
					<td width=40%>
						<asp:Textbox id=txtToCreditNoteDate maxlength=20 width=50% runat=server/>
						<a href="javascript:PopCal('txtToCreditNoteDate');">
						<asp:Image id="btnToCreditNoteDate" runat="server" ImageUrl="../Images/calendar.gif"/></td>
				</tr>	
				<tr>
					<td width=17%><asp:label id=lblBillParty runat=server /> : </td>
					<td width=39%><asp:textbox id=txtBuyer maxlength=128 width=50% runat=server /> (blank for all)</td>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr>
					<td width=17%>Billing Type : </td>
					<td width=39%><asp:dropdownlist id=ddlCNDocType width=50% runat=server /></td>
					<td colspan=2>&nbsp;</td>
				</tr>			
				<tr>
					<td width=17%>Undersigned Name : </td>
					<td width=39%>
						<asp:textbox id=txtUndName maxlength=128 width=50% runat=server />
						<asp:label id=lblUndName visible=false runat=server />
					</td>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr>
					<td width=17%>Undersigned Post : </td>
					<td width=39%>
						<asp:textbox id=txtUndPost maxlength=128 width=50% runat=server />
						<asp:label id=lblUndPost visible=false maxlength=20 width=50% runat=server />
					</td>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr id=TrExcPrintedDoc>
					<td width=17%>Exclude Printed Document : </td>
					<td width=39%>
						<asp:Checkbox id=cbExcPrintedDoc text=" Yes" runat=server />
					</td>
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
