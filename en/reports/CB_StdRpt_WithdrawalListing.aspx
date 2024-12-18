<%@ Page Language="vb" src="../../include/reports/CB_StdRpt_WithdrawalListing.aspx.vb" Inherits="CB_StdRpt_WithdrawalListing" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="CB_STDRPT_SELECTION_CTRL" src="../include/reports/CB_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Cash And Bank - Withdrawal Listing </title>
        <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
           <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 

			<table border="0" cellspacing="1" cellpadding="1" width="100%" class="font9Tahoma" ID="Table1">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong> CASH AND BANK - WITHDRAWAL LISTING</strong></td>
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
					<td colspan="6">
                        <hr style="width :100%" /> 
                    </td>
				</tr>
			</table>
			<table width=100% border="0" cellspacing="1" cellpadding="1" ID="Table2" class="font9Tahoma" runat=server>	
				<tr>
					<td width="17%">Withdrawal Code From :</td>
					<td width="39%"><asp:textbox id="txtWithdrawalCodeFrom" width="50%"  maxlength=20 runat="server" /> (blank for all) </td>
					<td width="4%">To :</td>
					<td width="40%"><asp:textbox id="txtWithdrawalCodeTo" width="50%"  maxlength=20 runat="server" /> (blank for all) </td>
				</tr>
				<tr>
					<td>Description :</td>
					<td><asp:textbox id="txtDescription" maxlength=8 width="50%"  runat="server" /> (blank for all)</td>					
					<td>&nbsp;</td>
					<td>&nbsp;</td>							
				</tr>	
				<tr>
					<td>Bank Code :</td>
					<td><asp:DropDownList id="ddlBankCode" width="50%" runat="server" />
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
					<td colspan=3><asp:Label id="lblLocation" visible="false" runat="server" /></td>
					<td>&nbsp;</td>
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
		<asp:label id=lblWithdrawal visible=false runat=server/>
	</body>
</HTML>
