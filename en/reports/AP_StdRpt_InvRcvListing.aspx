<%@ Page Language="vb" src="../../include/reports/AP_StdRpt_InvRcvListing.aspx.vb" Inherits="AP_StdRpt_InvRcvListing" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="AP_STDRPT_SELECTION_CTRL" src="../include/reports/AP_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Accounts Payable - Invoice Receive Listing </title>
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
					<td class="mt-h" colspan="3"><asp:label id=lblPageTitle runat=server/></td>
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
			<table width=100% border="0" cellspacing="1" cellpadding="1" ID="Table2" class="font9Tahoma" runat=server>	
				<tr>
					<td>Document No From :</td>
					<td><asp:textbox id="txtDocNoFrom" maxlength=20 width="50%" runat="server" /> (blank for all) </td>
					<td>To :</td>
					<td><asp:textbox id="txtDocNoTo" maxlength=20 width="50%" runat="server" /> (blank for all) </td>
				</tr>
				<tr>
					<td width="15%"><asp:label id=lblInvRcvRefNo runat=server/> :</td>
					<td width="35%"><asp:textbox id="txtInvRcvNo"  maxlength=32 width="50%" runat="server" /> (blank for all) </td>
					<TD width="15%"><asp:label id=lblInvRcvRefDateFrom runat=server/> :</TD>
					<TD valign="top" width="35%">
					<asp:TextBox ID=txtInvRcvRefDateFrom maxlength="10" width="50%" Runat=server />
					<a href="javascript:PopCal('txtInvRcvRefDateFrom');"><asp:Image id="btnSelDateFrom" runat="server" ImageUrl="../Images/calendar.gif"/></a>
					</TD>
				</tr>
				<tr>
					<td>Purchase Order ID :</td>
					<td><asp:textbox id="txtPOID"  maxlength=20 width="50%" runat="server" /> (blank for all)</td>
					<TD><asp:label id=lblInvRcvRefDateTo runat=server/> :</TD>
					<TD valign="top">
					<asp:TextBox ID=txtInvRcvRefDateTo maxlength="10" width="50%" Runat=server />
					<a href="javascript:PopCal('txtInvRcvRefDateTo');"><asp:Image id="btnSelDateTo" runat="server" ImageUrl="../Images/calendar.gif"/></a>
					</TD>	
				</tr>
				<tr>
					<td>Supplier Code:</td>
					<td><asp:textbox id="txtSuppCode" maxlength=8  width="50%" runat="server" /> 
					    <input type=button value=" ... " id="Find" onclick="javascript:PopSupplier('frmMain', '', 'txtSuppCode', 'False');" CausesValidation=False runat=server /> (blank for all)</td>
					<TD vAlign="top" height=25>Credit Term Type :</TD>
					<TD vAlign="top">
					<asp:DropDownList id="ddlTermType" width="50%"  runat="server" />
					</TD>						
				</tr>
				<tr>
					<td><asp:label id=lblProdTypeCode runat=server /></td>
					<td><asp:textbox id="txtProdType" maxlength=8 width="50%"  runat="server" /> (blank for all)</td>
					<td>Credit Term :</td>
					<td ><asp:textbox id="txtCreditTerm"  maxlength=3 width="50%"  runat="server" /> (blank for all)</td>
				</tr>
				<tr>
					<td><asp:label id=lblProdBrandCode runat=server /></td>
					<td><asp:textbox id="txtProdBrand" maxlength=8 width="50%"  runat="server" /> (blank for all)</td>
					<td><asp:label id="lblVehicleType" runat="server" /> Code: </td>
					<td ><asp:textbox id="txtVehicleType"  maxlength=8 width="50%" runat="server" /> (blank for all)</td>
				</tr>			
				<tr>
					<td><asp:label id=lblProdModelCode runat=server /></td>
					<td><asp:textbox id="txtProdModel" maxlength=8  width="50%" runat="server" /> (blank for all)</td>
					<td><asp:label id="lblVehicle" runat="server" /> Code: </td>
					<td ><asp:textbox id="txtVehicle"  maxlength=8 width="50%" runat="server" /> (blank for all)</td>
				</tr>								
				<tr>
					<td><asp:label id=lblProdCatCode runat=server /></td>
					<td><asp:textbox id="txtProdCat" maxlength=8  width="50%" runat="server" /> (blank for all)</td>
					<td><asp:label id="lblVehicleExp" runat="server" /> Code: </td>
					<td ><asp:textbox id="txtVehicleExp"  maxlength=8 width="50%" runat="server" /> (blank for all)</td>
				</tr>
				<tr>
					<td><asp:label id=lblProdMatCode runat=server /></td>
					<td><asp:textbox id="txtProdMaterial" maxlength=8  width="50%" runat="server" /> (blank for all)</td>
					<td> Invoice Type: </td>
					<td ><asp:dropdownlist id="ddlInvoiceType" width="50%" maxlength=8 runat="server" /> </td>	
				</tr>			
				<tr>
					<td><asp:label id=lblStkAnaCode runat=server /></td>
					<td><asp:textbox id="txtStkAna" maxlength=8 width="50%"  runat="server" /> (blank for all)</td>
					<td> Status: </td>
					<td ><asp:dropdownlist id="ddlStatus" width="50%" maxlength=8 runat="server" /> </td>	
				</tr>	
				<tr>
					<td>Item Code :</td>
					<td ><asp:textbox id="txtItemCode"  maxlength=20 width="50%" runat="server" /> (blank for all)</td>	
					<td>&nbsp;</td>					
				</tr>
				<tr>
					<td> <asp:label id="lblCOACode" runat="server" /> Code :</td>
					<td ><asp:textbox id="txtCOACode"  maxlength=32 width="50%" runat="server" /> 
					    <input type="button" id=btnFind1 value=" ... " onclick="javascript:PopCOA('frmMain', '', 'txtCOACode', 'False');" runat=server/> (blank for all)</td>	
					<td>&nbsp;</td>						
				</tr>
				<tr>
					<td><asp:label id="lblBlkType" runat=server /> Type:</td>
					<td ><asp:DropDownList id="lstBlkType" width="50%" AutoPostBack=true  runat="server" /></td>
					<td>&nbsp;</td>
				</tr>	
				<tr id=TrBlkGrp>
					<td><asp:label id="lblBlkGrp" runat=server /></td>
					<td ><asp:textbox id="txtBlkGrp" maxlength="8"  width="50%" runat="server" /> (blank for all)</td>	
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
					<td colspan=3><asp:Label id="lblLocation" visible="false" runat="server" /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=3><asp:ImageButton id="PrintPrev" AlternateText="Print Preview" imageurl="../images/butt_print_preview.gif" onClick="btnPrintPrev_Click" runat="server" />&nbsp;<asp:Button 
                            ID="Issue6" runat="server" class="button-small" Text="Print Preview" 
                            Visible="False" />
                    </td>						
				</tr>
				<tr>
					<td colspan=3>
						<asp:label id=lblAccPayable text="ACCOUNT PAYABLES" visible=false runat=server/>
						<asp:label id=lblListing text=" LISTING" visible=false runat=server/>
						<asp:label id=lblDash text = " - " visible=false runat=server/>
						<asp:label id=lblInvRcv visible=false runat=server/>
						<asp:label id=lblRefNo text=" Ref No" visible=false runat=server/>
						<asp:label id=lblRefDateFrom text=" Ref Date From" visible=false runat=server/>
						<asp:label id=lblRefDateTo text=" Ref Date To" visible=false runat=server/>
					</td>
				</tr>			
			</table>
                </div>
            </td>
            </tr>
            </table>
		</form>
	</body>
</HTML>
