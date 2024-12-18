<%@ Page Language="vb" src="../../include/reports/CB_StdRpt_PaymentSchedPerBank.aspx.vb" Inherits="CB_StdRpt_PaymentSchedPerBank" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="CB_STDRPT_SELECTION_CTRL" src="../include/reports/CB_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Cash And Bank - Payment Schedule Per Bank</title>
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
					<td class="font9Tahoma" colspan="3"><strong>CASH AND BANK - PAYMENT SCHEDULE PER BANK</strong></td>
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
					<td colspan="6">&nbsp;</td>
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
					<td>Period :</td>
					<td>    
						<asp:Textbox id="txtDateFrom" width=50% maxlength=10 runat=server/>
						<a href="javascript:PopCal('txtDateFrom');"><asp:Image id="btnDateFrom" runat="server" ImageUrl="../images/calendar.gif"/></a>
					</td>					
					<td>to :</td>
					<td>    
						<asp:Textbox id="txtDateTo" width=50% maxlength=10 runat=server/>
						<a href="javascript:PopCal('txtDateTo');"><asp:Image id="btnDateTo" runat="server" ImageUrl="../images/calendar.gif"/></a>
					</td>							
				</tr>	
			
				<tr>
					<td></td>
					<td></td>					
					<td></td>
					<td></td>							
				</tr>	
			
				<tr>
					<td>Bank :</td>
					<td>
					   <asp:DropDownList id="ddlBank"  width="50%" runat="server" > 
					   </asp:DropDownList>					
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
				</tr>
				
				<tr>
					<td colspan=3>
						<asp:Label id=lblErrDateFrom visible=false forecolor=red runat=server/>
						<asp:Label id=lblErrDateTo visible=false forecolor=red runat=server/>
					</td>			
				</tr>
				
				
				<tr>
					<td colspan=3> </td>		
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
		
	</body>
</HTML>
