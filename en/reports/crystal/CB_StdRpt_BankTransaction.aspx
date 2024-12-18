<%@ Page Language="vb" src="../../include/reports/CB_StdRpt_BankTransaction.aspx.vb" Inherits="CB_StdRpt_BankTransaction" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="CB_STDRPT_SELECTION_CTRL" src="../include/reports/CB_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Cash And Bank - Bank Transaction</title>
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" ID="frmMain">
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1">
				<tr>
					<td class="mt-h" colspan="3">CASH AND BANK - BANK TRANSACTION</td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6"><hr size="1" noshade></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6"><UserControl:CB_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
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
					<td width="15%">Date From:</td>
					<td width="30%">    
						<asp:Textbox id="txtDate" width=60% maxlength=10 runat=server/>
						<a href="javascript:PopCal('txtDate');"><asp:Image id="btnDate" runat="server" ImageUrl="../images/calendar.gif"/></a>
					</td>	
					<td width="5%">&nbsp;</td>
					<td width="15%">Date To:</td>
					<td width="30%">    
						<asp:Textbox id="txtDateTo" width=60% maxlength=10 runat=server/>
						<a href="javascript:PopCal('txtDateTo');"><asp:Image id="btnDateTo" runat="server" ImageUrl="../images/calendar.gif"/></a>
					</td>		
					<td>&nbsp;</td>
					<td>&nbsp;</td>			
				</tr>	
				<tr>
					<td width="15%">Bank (Acc Code):</td>
					<td width="30%">    
					 <asp:Textbox id="txtAccCode" text = "110.11.01" width=50% maxlength=32 runat=server/>
					 <asp:RequiredFieldValidator 
							id="rfvAccCode" 
							runat="server"  
							ControlToValidate="txtAccCode" 
							text = "Please Fill Bank (Acc Code)"
							display="dynamic"/>
					</td>	
					<td>&nbsp;</td>	
					<td>&nbsp;</td>	
				</tr>	
				
				<tr>
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
				</tr>
				<tr>
					<td colspan="5">
						<asp:Label id="lblErrDate" visible="false" forecolor="red" runat="server"/>
					</td>			
				</tr>
				
				
				<tr>
					<td colspan="5"> </td>		
				</tr>
				
				
				<tr>
					<td colspan="5"><asp:ImageButton id="PrintPrev" AlternateText="Print Preview" imageurl="../images/butt_print_preview.gif" onClick="btnPrintPrev_Click" runat="server" /></td>						
				</tr>				
			</table>
		</form>
		<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" />
		
	</body>
</HTML>
