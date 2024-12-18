<%@ Page Language="vb" src="../../include/reports/AP_StdRpt_SuppDebitNoteListing.aspx.vb" Inherits="AP_StdRpt_SuppDebitNoteListing" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="AP_STDRPT_SELECTION_CTRL" src="../include/reports/AP_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Accounts Payable - Supplier Debit Note Listing </title>
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
					<td class="font9Tahoma" colspan="3"><strong>ACCOUNTS PAYABLE - SUPPLIER DEBIT NOTE LISTING</strong> </td>
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
					<td colspan="6"><asp:Label id="lblDate" forecolor=red visible="false" text="Incorrect Date Format. Date Format is " runat="server" />
									<asp:Label id="lblDateFormat" forecolor=red visible="false" runat="server" />				
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>			
				<tr>
					<td colspan="6"><hr style="width :100%" /></td>
				</tr>
			</table>
			<table width=100% border="0" cellspacing="1" cellpadding="1" ID="Table2" class="font9Tahoma"  runat=server>	
				<tr>
					<td width="17%">Debit Note ID From :</td>
					<td width="39%"><asp:textbox id="txtSuppDebitNoteIDFrom"  width="50%" maxlength=20 runat="server" /> (blank for all) </td>
					<td width="4%">To :</td>
					<td width="40%"><asp:textbox id="txtSuppDebitNoteIDTo"  width="50%" maxlength=20 runat="server" /> (blank for all) </td>
				</tr>
				<tr>
					<td>Debit Note Ref No :</td>
					<td><asp:textbox id="txtSuppDebitNoteRefNo" width="50%"  maxlength=20 runat="server" /> (blank for all) </td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
				</tr>

				<tr>
					<TD>Debit Note Ref Date From :</TD>
					<TD valign="top">
					<asp:TextBox ID=txtSuppDebitNoteRefDateFrom maxlength="10" width="50%" Runat=server />
					<a href="javascript:PopCal('txtSuppDebitNoteRefDateFrom');"><asp:Image id="btnSelDateFrom" runat="server" ImageUrl="../Images/calendar.gif"/></a>
					</TD>
					<TD>To :</TD>
					<TD valign="top">
					<asp:TextBox ID=txtSuppDebitNoteRefDateTo maxlength="10" width="50%" Runat=server />
					<a href="javascript:PopCal('txtSuppDebitNoteRefDateTo');"><asp:Image id="btnSelDateTo" runat="server" ImageUrl="../Images/calendar.gif"/></a>
					</TD>	
				</tr>
				<tr>
					<td>Supplier Code :</td>
					<td><asp:textbox id="txtSuppCode" maxlength=8  width="50%" runat="server" /> 
					    <input type=button value=" ... " id="Find" onclick="javascript:PopSupplier('frmMain', '', 'txtSuppCode', 'False');" CausesValidation=False runat=server /> (blank for all)</td>					
					<td>&nbsp;</td>
					<td>&nbsp;</td>							
				</tr>
				<tr>
					<td><asp:Label id=lblInvoiceRcvRefNo runat="server" /> :</td>
					<td><asp:textbox id="txtInvoiceRcvRefNo" maxlength=32  width="50%" runat="server" /> (blank for all)</td>					
					<td>&nbsp;</td>
				    <td>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:label id="lblCOACode" runat="server" /> Code :</td>
					<td><asp:textbox id="txtCOACode"  maxlength=32 width="50%" runat="server" /> 
					    <input type="button" id=btnFind1 value=" ... " onclick="javascript:PopCOA('frmMain', '', 'txtCOACode', 'False');" runat=server/> (blank for all)</td>	
					<td>&nbsp;</td>						
				</tr>
				<tr>
					<td><asp:label id="lblBlkType" runat=server /> Type :</td>
					<td ><asp:DropDownList id="lstBlkType" AutoPostBack=true width="50%"  runat="server" /></td>
					<td>&nbsp;</td>
				</tr>	
				<tr id=TrBlkGrp>
					<td><asp:label id="lblBlkGrp" runat=server /></td>
					<td ><asp:textbox id="txtBlkGrp" maxlength="8" width="50%"  runat="server" /> (blank for all)</td>	
					<td>&nbsp;</td>							
				</tr>																						
				<tr id=TrBlk>
					<td><asp:label id="lblBlkCode" runat=server /></td>
					<td ><asp:textbox id="txtBlkCode" maxlength="8" width="50%"  runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>				
				</tr>
				<tr id=TrSubBlk>
					<td><asp:label id="lblSubBlkCode" runat=server /></td>
					<td ><asp:textbox id="txtSubBlkCode" maxlength="8" width="50%"  runat="server" /> (blank for all)</td>	
					<td>&nbsp;</td>					
				</tr>	
								<tr>
					<td><asp:label id="lblVehicleType" runat="server" /> Code : </td>
					<td ><asp:textbox id="txtVehicleType"  maxlength=8 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>	
				</tr>
				<tr>
					<td><asp:label id="lblVehicle" runat="server" /> Code : </td>
					<td ><asp:textbox id="txtVehicle"  maxlength=8 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>			
				<tr>
					<td><asp:label id="lblVehicleExp" runat="server" /> Code : </td>
					<td ><asp:textbox id="txtVehicleExp"  maxlength=8 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>								
				
				<tr>
					<td> Status: </td>
					<td ><asp:dropdownlist id="ddlStatus" width="50%" maxlength=8 runat="server" /> </td>	
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
	</body>
</HTML>
