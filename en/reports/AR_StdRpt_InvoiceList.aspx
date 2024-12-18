<%@ Page Language="vb" src="../../include/reports/AR_StdRpt_InvoiceList.aspx.vb" Inherits="AR_StdRpt_InvoiceList" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="AR_STDRPT_SELECTION_CTRL" src="../include/reports/AR_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Account Receivables - Invoice Listing </title>
        
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
					<td class="font9Tahoma" colspan="3"><strong>ACCOUNT RECEIVABLES - INVOICE LISTING</strong> </td>
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
					<td colspan="6"><UserControl:AR_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6"><asp:Label id="lblDate" forecolor=red visible="false" text="Incorrect Date Format. Date Format is " runat="server" />
									<asp:Label id="lblDateFormat" forecolor=red visible="false" runat="server" />	</td>			
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
					<td  width="17%">Invoice ID From :</td>
					<td width="39%"><asp:textbox id="txtInvNoFrom"  width="50%" maxlength=32 runat="server" /> (blank for all) </td>
					<TD width="4%"> To :</TD>
					<TD width="40%"><asp:TextBox ID=txtInvNoTo maxlength="32" width="50%" Runat=server /> (blank for all) </TD>
				</tr>

				<tr>
					<td><asp:Label id="lblBillParty" runat="server" /> :</td>
					<td><asp:textbox id="txtBillParty"  maxlength=20 width="50%" runat="server" /> (blank for all)</td>
					
				</tr>
				<tr>
					<td>Billing Type :</td>
					<td><asp:DropDownList id="ddlInvoiceType" width="50%" runat="server" /></td>					
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
				</tr>
				<tr>
					<td>Customer Reference :</td>
					<td><asp:textbox id="txtCustomerRef"   width="50%" runat="server" /> (blank for all) </td>					
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
				</tr>
				<tr>
					<td>Delivery Reference :</td>
					<td><asp:textbox id="txtDeliveryRef"  width="50%"  runat="server" /> (blank for all) </td>					
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
				</tr>
				<tr>
					<td><asp:Label id="lblCOA" runat="server" /> Code :</td>
					<td><asp:textbox id="txtCOA" maxlength=32 width="50%"  runat="server" /> (blank for all) </td>					
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
				</tr>	
				<tr>
					<td><asp:label id="lblBlkType" runat=server /> Type:</td>
					<td ><asp:DropDownList id="lstBlkType" width="50%" AutoPostBack=true  runat="server" /></td>
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
					<td><asp:Label id="lblVehicle" runat="server" /> Code :</td>
					<td><asp:textbox id="txtVehicle" width="50%"  runat="server" /> (blank for all) </td>					
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
				</tr>					
				<tr>
					<td><asp:Label id="lblVehicleExpCode" runat="server" /> Code :</td>
					<td><asp:textbox id="txtVehicleExpCode" width="50%"  runat="server" /> (blank for all) </td>					
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
				</tr>	
				<tr>
					<td>Item Description :</td>
					<td><asp:textbox id="txtItemDesc" width="50%"  runat="server" /> (blank for all) </td>					
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
				</tr>
				<tr>
					<td>Status :</td>
					<td><asp:DropDownList id="ddlStatus"  width="50%" runat="server" /></td>					
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
				</tr>																
				<tr>
					<td colspan=3><asp:Label id="lblLocation" visible="false" runat="server" /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=3><asp:ImageButton id="PrintPrev" AlternateText="Print Preview" imageurl="../images/butt_print_preview.gif" onClick="btnPrintPrev_Click" runat="server" /></td>						
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
