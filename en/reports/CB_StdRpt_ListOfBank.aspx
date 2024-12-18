<%@ Page Language="vb" src="../../include/reports/CB_StdRpt_ListOfBank.aspx.vb" Inherits="CB_StdRpt_ListOfBank" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="CB_STDRPT_SELECTION_CTRL" src="../include/reports/CB_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Cash And Bank - List of Bank</title>
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
					<td class="font9Tahoma" colspan="3"><strong> CASH AND BANK - LIST OF BANK</strong></td>
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
					<td>Date :</td>
					<td>    
						<asp:Textbox id="txtDate" width=50% maxlength=10 runat=server/>
						<a href="javascript:PopCal('txtDate');"><asp:Image id="btnDate" runat="server" ImageUrl="../images/calendar.gif"/></a>
					</td>					
					<td></td>
					<td></td>							
				</tr>	
			
				<tr>
					<td></td>
					<td></td>					
					<td></td>
					<td></td>							
				</tr>	
				
				<tr>
					<td>Status :</td>
					<td>
					   <asp:DropDownList id="ddlStatus"  width="50%" runat="server" > 
					    
					    <asp:ListItem value="0">All</asp:ListItem>
					    <asp:ListItem value="1">Active</asp:ListItem>
						<asp:ListItem value="2">Pasive</asp:ListItem>
					
					   </asp:DropDownList>					
					</td>
					<td></td>
					<td></td>					
				</tr>
				
				<tr>
					<td></td>
					<td></td>					
					<td></td>
					<td></td>							
				</tr>	
				
				<tr>
					<td width="10%">Bank Code :</td>
					<td width="40%">
					   <asp:DropDownList id="ddlBankCodeFrom"  width="80%" runat="server" > 
					   </asp:DropDownList>					
					</td>
					<td width="10%">To :</td>
					<td width="40%">
						<asp:DropDownList id="ddlBankCodeTo"  width="80%" runat="server" > 
					   </asp:DropDownList>
					</td>					
				</tr>
					
				
				<tr>
					<td colspan=4> </td>		
				</tr>
				<tr>
					<td colspan=4>
						<asp:Label id=lblErrDate visible=false forecolor=red runat=server/>
					</td>
				</tr>
				
				<tr>
					<td colspan=4><asp:ImageButton id="PrintPrev" AlternateText="Print Preview" imageurl="../images/butt_print_preview.gif" onClick="btnPrintPrev_Click" runat="server" />&nbsp;<asp:Button 
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
