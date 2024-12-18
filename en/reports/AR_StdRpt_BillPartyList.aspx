<%@ Page Language="vb" src="../../include/reports/AR_StdRpt_BillPartyList.aspx.vb" Inherits="AR_StdRpt_BillPartyList" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="AR_STDRPT_SELECTION_CTRL" src="../include/reports/AR_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Account Receivables - Bill Party Listing</title>
             <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
           <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 

			<!--<input type=hidden id=hidUserLocPX runat="server" NAME="hidUserLocPX"/>
			<input type=hidden id=hidAccMonthPX runat="server" NAME="hidAccMonthPX"/>
			<input type=hidden id=hidAccYearPX runat="server" NAME="hidAccYearPX"/>-->

			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong> ACCOUNT RECEIVABLES - BILL PARTY LISTING</strong></td>
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
					<td colspan="6"><asp:Label id="lblDate" forecolor=red visible="false" text="Incorrect Date Format. Date Format is " runat="server" />
									<asp:Label id="lblDateFormat" forecolor=red visible="false" runat="server" />	</td>			
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>		
				<tr>
					<td colspan="6"><hr style="width :100%" /></td>
				</tr>
			</table>
			<table width="100%" border="0" cellspacing="1" cellpadding="1" ID="Table1" class="font9Tahoma" >
				<tr>
					<td width="17%"><asp:Label id="lblBillParty" runat="server"/> :  </td>
					<td width="39%"><asp:textbox id="txtBillPartyCode" maxlength=8 width="50%"  runat="server" /> (blank for all)</td>
					<td width="4%">&nbsp;</td>
					<td width="40%">&nbsp;</td>
				</tr>
				<tr>
					<td>Name : </td>
					<td><asp:textbox id="txtName" maxlength=128 width="50%"  runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Contact Person : </td>
					<td><asp:textbox id="txtContactPerson" maxlength=128 width="50%"  runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>	
				<tr>
					<td>Town : </td>
					<td><asp:textbox id="txtTown" maxlength=128 width="50%"  runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>State : </td>
					<td><asp:textbox id="txtState" maxlength=128 width="50%"  runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Post Code : </td>
					<td><asp:textbox id="txtPostCode" maxlength=128 width="50%"  runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>													
				<tr>
					<td>Country : </td>
					<td><asp:DropDownList id="ddlCountry" width="50%"  runat="server" /> </td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Tel No : </td>
					<td><asp:textbox id="txtTelNo" maxlength=128 width="50%"  runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>						
				<tr>
					<td>Fax No : </td>
					<td><asp:textbox id="txtFaxNo" maxlength=128 width="50%"  runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>					
				<tr>
					<td>Email : </td>
					<td><asp:textbox id="txtEmail" maxlength=128 width="50%"  runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>					
				<tr>
					<td>Credit Term : </td>
					<td><asp:textbox id="txtCreditTerm" maxlength=128 width="50%"  runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>				
				<tr>
					<td>Credit Term Type :</td>
					<td><asp:DropDownList id="ddlCreditTermType" width="50%"  runat=server>
							<asp:ListItem value="" selected>All</asp:ListItem>
							<asp:ListItem value="0">Days after Acceptance Date</asp:ListItem>
							<asp:ListItem value="1">Days after Bill of Lading</asp:ListItem>
							<asp:ListItem value="2">Days after Date of Draft</asp:ListItem>
							<asp:ListItem value="3">Days after Delivery Order Date</asp:ListItem>
							<asp:ListItem value="4">Days after Date of Invoice</asp:ListItem>
							<asp:ListItem value="5">Days after Shipment Date</asp:ListItem>
							<asp:ListItem value="6">Days Sight</asp:ListItem>
							<asp:ListItem value="7">Days from Acceptance Date</asp:ListItem>
							<asp:ListItem value="8">Days from Bill of Lading</asp:ListItem>
							<asp:ListItem value="9">Days from Date of Draft</asp:ListItem>
							<asp:ListItem value="10">Days from Delivery Order Date</asp:ListItem>
							<asp:ListItem value="11" >Days from Date of Invoice</asp:ListItem>
							<asp:ListItem value="12">Sight</asp:ListItem>
						</asp:DropDownList>
					</td>					
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Credit Limit : </td>
					<td><asp:textbox id="txtCreditLimit" maxlength=128 width="50%"   runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>	
				<tr>
					<td><asp:label id="lblCOA" runat="server"/> : </td>
					<td><asp:textbox id="txtCOA" maxlength=32 width="50%"   runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>	
				<tr>
					<td>Status :</td>
					<td><asp:DropDownList id="lstStatus"  width="50%" runat="server" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>																											
				</tr>
					<td colspan=4><asp:Label id="lblLocation" visible="false" runat="server" /></td>
				</tr>
											
				<tr>
					<td colspan=4><asp:ImageButton id="PrintPrev" ImageUrl="../images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" />&nbsp;<asp:Button 
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
