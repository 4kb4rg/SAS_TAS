<%@ Page Language="vb" src="../../include/reports/AP_StdRpt_CreditorJournalList.aspx.vb" Inherits="AP_StdRpt_CreditorJournalList" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="AP_STDRPT_SELECTION_CTRL" src="../include/reports/AP_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Account Payables - Creditor Journal Listing </title>
                 <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
           <td style="width: 100%; height: 1000px" valign="top">
			    <div class="kontenlist"> 

			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1" class="font9Tahoma">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong>ACCOUNT PAYABLES - CREDITOR JOURNAL LISTING</strong> </td>
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
			<table width=100% border="0" cellspacing="1" cellpadding="1" ID="Table2" class="font9Tahoma" runat=server>	
				<tr>
					<td width=17%>Creditor Journal ID From :</td>
					<td width=39%><asp:textbox id="txtCreditJrnIDFrom"  maxlength=32 width="50%" runat="server" /> (blank for all) </td>
					<TD width=4%>To :</TD>
					<TD width=40%><asp:TextBox ID=txtCreditJrnIDTo maxlength="32" width="50%" Runat=server /> (blank for all) </TD>
				</tr>

				<tr>
					<td width=17%>Supplier Code :</td>
					<td width=39%><asp:textbox id="txtSupplier"  maxlength=20 width="50%" runat="server" /> 
					              <input type=button value=" ... " id="Find" onclick="javascript:PopSupplier('frmMain', '', 'txtSupplier', 'False');" CausesValidation=False runat=server /> (blank for all)</td>
					<TD width=4%>&nbsp;</TD>
					<TD width=40%>&nbsp;</TD>
				</tr>
				<tr>
					<td width=17%>Journal Type :</td>
					<td width=39%><asp:DropDownList id="ddlJournalType" width="50%" runat="server" /></td>					
					<TD width=4%>&nbsp;</TD>
					<TD width=40%>&nbsp;</TD>
				</tr>
				<tr>
					<td width=17%>Status :</td>
					<td width=39%><asp:DropDownList id="ddlStatus" width="50%" runat="server" /></td>					
					<TD width=4%>&nbsp;</TD>
					<TD width=40%>&nbsp;</TD>
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
		<asp:Label id="BlockTag" visible="false" runat="server" />
		<asp:Label id="AccountTag" visible="false" runat="server" />
		<asp:Label id="VehTag" visible="false" runat="server" />
		<asp:Label id="VehExpenseTag" visible="false" runat="server" />
		<asp:Label id="LocTag" visible="false" runat="server" />
	</body>
</HTML>
