<%@ Page Language="vb" src="../../include/reports/CM_StdRpt_ContractMatchingTransactionList.aspx.vb" Inherits="CM_StdRpt_ContractMatchingTransactionList" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="CM_STDRPT_SELECTION_CTRL" src="../include/reports/CM_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Contract Management - Contract Matching Transaction Listing</title>
          <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu"  ID="frmMain">
       		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
           <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 

			<!--<input type=hidden id=hidUserLocPX runat="server" NAME="hidUserLocPX"/>
			<input type=hidden id=hidAccMonthPX runat="server" NAME="hidAccMonthPX"/>
			<input type=hidden id=hidAccYearPX runat="server" NAME="hidAccYearPX"/>-->

			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1"  class="font9Tahoma">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong> CONTRACT MANAGEMENT - CONTRACT MATCHING TRANSACTION LISTING</strong></td>
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
					<td colspan="6"><UserControl:CM_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
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
					<td colspan="6">
                        <hr style="width :100%" /> 
                    </td>
				</tr>
			</table>
			<table width="100%" border="0" cellspacing="1" cellpadding="1" ID="Table1"  class="font9Tahoma">
			
				<tr>
					<td width="17%">Contract Matching ID From : </td>
					<td width="39%"><asp:textbox id="txtContractIDFrom" width="50%" maxlength=20  runat="server" /> (blank for all)</td>
					<td width="4%">To : </td>
					<td width="40%"><asp:textbox id="txtContractIDTo" width="50%" maxlength=20  runat="server" /> (blank for all)</td>
				</tr>
				<tr>
					<td>Product : </td>
					<td><asp:DropDownList id="ddlProduct"  width="50%" runat="server" /> </td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>			
				<tr>
					<td><asp:label id="lblBillParty" runat=server/> Code : </td>
					<td><asp:textbox id="txtBuyer" maxlength=8  width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>								
				<tr>
					<td>Contract No : </td>
					<td><asp:textbox id="txtContractNo" maxlength=20  width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>	
				<tr>
					<td>Invoice No : </td>
					<td><asp:textbox id="txtInvoiceNo" maxlength=20  width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr ID="trInvoiceDate" >
					<td>Invoice Date From : </td>
					<td><asp:TextBox id="txtDateFrom" maxlength="10"  width="50%" runat="server"/>
  								  <a href="javascript:PopCal('txtDateFrom');">
								  <asp:Image id="btnSelDateFrom" runat="server"  ImageUrl="../Images/calendar.gif"/></a>
					</td>				
					<td> To : </td>
					<td>
								  <asp:TextBox id="txtDateTo"  maxlength="10" width="50%" runat="server"/>
  								  <a href="javascript:PopCal('txtDateTo');">
								  <asp:Image id="btnSelDateTo" runat="server" ImageUrl="../Images/calendar.gif"/></a></td>
					</td>
				</tr>
				<tr>
					<td>Debit/ Credit Note ID : </td>
					<td><asp:textbox id="txtDbCrID" maxlength=20 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr ID="trInvoiceDate" >
					<td>Debit/ Credit Note Date From : </td>
					<td><asp:TextBox id="txtDbCrDateFrom" maxlength="10" width="50%" runat="server"/>
  								  <a href="javascript:PopCal('txtDbCrDateFrom');">
								  <asp:Image id="btnSelDbCrDateFrom" runat="server"  ImageUrl="../Images/calendar.gif"/></a>
					 </td>				
					<td> To: </td>
					<td>
								  <asp:TextBox id="txtDbCrDateTo"  maxlength="10"  width="50%" runat="server"/>
  								  <a href="javascript:PopCal('txtDbCrDateTo');">
								  <asp:Image id="btnSelDbCrDateTo" runat="server" ImageUrl="../Images/calendar.gif"/></a>
					</td>
				</tr>								
				<tr>
					<td>Status :</td>
					<td><asp:DropDownList id="lstStatus"  width="50%" runat="server" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Sort by :</td>
					<td><asp:DropDownList id="ddlOrderBy"  width="50%" runat="server" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>	
				<tr>
					<td colspan=4><asp:Label id="lblLocation" visible="false" runat="server" /></td>
				</tr>
											
				<tr>
					<td colspan=4><asp:ImageButton id="PrintPrev" ImageUrl="../images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" />&nbsp;<asp:Button 
                            ID="Issue6" runat="server" class="button-small" Text="Print Preview" 
                            Height="26px" Visible="False" />
                    </td>
				</tr>
								<tr ID="trUpdateBy" >
					<td></td>
					<td><asp:TextBox id="TxtUpdatedBy"  maxlength="20" visible=false runat="server" />  </td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td> </td>
					<td><asp:textbox id="txtPriceChangedInd" maxlength=1  visible=false runat="server" /> </td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
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
